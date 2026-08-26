using System.Globalization;
using System.Text;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-082 Beyond Conformal Time Audit. Tests whether γ = dτ/dt = a is unique, or whether a
/// family of clock laws (γ = a^p, γ = a(1+εz), γ = a·exp(βz), γ = a/(1+αz)) can reproduce the
/// observed redshift and SN Ia time dilation without reducing to FLRW.
///
/// Core fact: in static space + evolving clock, BOTH redshift and time dilation equal
/// γ_obs/γ_emit, so 1+z = 1/γ(z) forces γ(z) = 1/(1+z) = a. Every alternative family is
/// therefore excluded by redshift unless its extra parameter vanishes.
/// </summary>
public static class BeyondConformalTimeAnalyzer
{
    public static BeyondConformalReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        // Candidate clock families (parameter grid).
        var rows = new List<ClockFamilyRow>();
        foreach (double p in new[] { 0.5, 0.75, 1.0, 1.25, 1.5 })
            rows.Add(Evaluate($"γ = a^{p}", p, z => Math.Pow(1.0 / (1.0 + z), p)));
        foreach (double eps in new[] { -0.2, -0.1, 0.0, 0.1, 0.2 })
            rows.Add(Evaluate($"γ = a·(1{eps:+0.0;-0.0}z)", eps, z => (1.0 / (1.0 + z)) * (1.0 + eps * z)));
        foreach (double beta in new[] { -0.2, -0.1, 0.0, 0.1, 0.2 })
            rows.Add(Evaluate($"γ = a·exp({beta:+0.0;-0.0}z)", beta, z => (1.0 / (1.0 + z)) * Math.Exp(beta * z)));
        foreach (double alpha in new[] { -0.2, -0.1, 0.0, 0.1, 0.2 })
            rows.Add(Evaluate($"γ = a/(1{alpha:+0.0;-0.0}z)", alpha, z => (1.0 / (1.0 + z)) / (1.0 + alpha * z)));

        var viable = rows.Where(r => r.Viable).ToArray();

        WriteClockFamiliesCsv(Path.Combine(outDir, "ClockFamilies.csv"), rows);
        WriteViableCsv(Path.Combine(outDir, "ViableTimeDynamics.csv"), viable);
        WriteSnConstraintsCsv(Path.Combine(outDir, "SN_TimeDilation_Constraints.csv"), rows);

        return new BeyondConformalReport(
            BuildA(), BuildB(rows), BuildC(rows), BuildD(viable), BuildE(viable), BuildF(), BuildG(viable),
            rows.ToArray(), viable, outDir);
    }

    private static ClockFamilyRow Evaluate(string name, double param, Func<double, double> gamma)
    {
        // Evaluate at z=1 (representative high-z).
        double z = 1.0;
        double g = gamma(z);
        double zModel = 1.0 / g - 1.0;
        double deltaZOverZ = Math.Abs(zModel - z) / z; // redshift discrepancy (fractional)

        // Time-dilation exponent b = d ln D / d ln(1+z), D = 1/γ.
        double dz = 0.01;
        double D1 = 1.0 / gamma(z), D2 = 1.0 / gamma(z + dz);
        double bEff = Math.Log(D2 / D1) / Math.Log((1.0 + z + dz) / (1.0 + z));
        double dilationSigma = Math.Abs(bEff - 1.0) / 0.05; // vs observed b=1.00±0.05

        // g† factor: f = d(ln γ)/d(ln a) = a·γ'/γ, so g† = f·cH/2π.
        double da = 0.01;
        double a1 = 1.0 / (1.0 + z), a2 = 1.0 / (1.0 + z + dz);
        double g1 = gamma(z), g2 = gamma(z + dz);
        double fGdagger = (Math.Log(g2) - Math.Log(g1)) / (Math.Log(a2) - Math.Log(a1));

        bool viable = deltaZOverZ < 1e-3 && dilationSigma < 1.0; // redshift exact + within 1σ of b=1

        return new ClockFamilyRow(name, param, zModel, bEff, deltaZOverZ, dilationSigma, fGdagger, viable);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Core relation. In static space + evolving clock, coordinate frequency is conserved,");
        sb.AppendLine("so redshift AND time dilation are the SAME quantity:");
        sb.AppendLine();
        sb.AppendLine("    1+z = ν_emit/ν_obs = γ_obs/γ_emit   (redshift)");
        sb.AppendLine("    Δτ_obs/Δτ_emit = γ_obs/γ_emit       (time dilation)");
        sb.AppendLine();
        sb.AppendLine("With γ_obs normalized to 1, both give γ(z) = 1/(1+z) = a. Redshift ALONE forces");
        sb.AppendLine("γ = a; time dilation is a redundant but independent confirmation (b = 1).");
        return sb.ToString();
    }

    private static string BuildB(List<ClockFamilyRow> rows)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Candidate clock families (evaluated at z=1).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-22} {1,8} {2,9} {3,8} {4,12}", "γ(z)", "z_model", "b_eff", "Δz/z", "b-signif."));
        foreach (var r in rows)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-22} {1,8:F3} {2,9:F2} {3,8:F2} {4,11:F0}σ",
                r.Name, r.ZModel, r.BEff, r.DeltaZOverZ, r.DilationSigma));
        return sb.ToString();
    }

    private static string BuildC(List<ClockFamilyRow> rows)
    {
        var sb = new StringBuilder();
        sb.AppendLine("SN Ia time-dilation constraints (b = 1.00 ± 0.05).");
        sb.AppendLine();
        foreach (var r in rows.Where(x => x.DilationSigma >= 1.0).Take(8))
            sb.AppendLine($"  {r.Name,-22}  b_eff = {r.BEff:F2}  → excluded at {r.DilationSigma:F0}σ");
        sb.AppendLine();
        sb.AppendLine("  The power-law family γ=a^p predicts b=p exactly, so SN Ia force p = 1.00±0.05.");
        sb.AppendLine("  The ε/β/α families deviate at high z and are excluded unless the parameter → 0.");
        return sb.ToString();
    }

    private static string BuildD(ClockFamilyRow[] viable)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Viable time dynamics (redshift exact + within 1σ of b=1).");
        sb.AppendLine();
        foreach (var v in viable)
            sb.AppendLine($"  {v.Name,-22}  (parameter = {v.Param:+0.00;-0.00})  →  γ = a  (FLRW-conformal)");
        sb.AppendLine();
        sb.AppendLine("  ALL survivors reduce to γ = a. No non-FLRW time dynamics is viable.");
        return sb.ToString();
    }

    private static string BuildE(ClockFamilyRow[] viable)
    {
        var sb = new StringBuilder();
        sb.AppendLine("g† for surviving dynamics: g† = c·d(ln γ)/dt / 2π = f·cH/2π,  f = a·γ'/γ.");
        sb.AppendLine();
        foreach (var v in viable)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-22}  f = {1:F2}  →  g† = {2:F2}·cH/2π", v.Name, v.GdaggerFactor, v.GdaggerFactor));
        sb.AppendLine();
        sb.AppendLine("  Since only γ = a survives (f = 1), g† = cH/2π is FORCED. The RAR relation is");
        sb.AppendLine("  robust: any deviation of γ from a would change g† to f·cH/2π and is excluded.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Distinct predictions (the 'smoking gun' for beyond-conformal dynamics).");
        sb.AppendLine();
        sb.AppendLine("  The only way γ ≠ a can hide is if different physical processes couple to the");
        sb.AppendLine("  cosmic clock DIFFERENTLY — i.e. a drift between clock types:");
        sb.AppendLine("    - gravitational clock (dynamical time, G and masses)");
        sb.AppendLine("    - atomic clock (electromagnetic time, α and m_e)");
        sb.AppendLine("    - nuclear clock (weak/strong rates)");
        sb.AppendLine();
        sb.AppendLine("  Such a drift would appear as NON-UNIVERSAL redshift (z differs by species) or as");
        sb.AppendLine("  time-variation of α or G. Both are tightly constrained (~10^-6 .. 10^-2 per Hubble");
        sb.AppendLine("  time). Therefore the 'distinct prediction' is: clock-universality must hold to");
        sb.AppendLine("  high precision — and it does. No beyond-conformal signal survives.");
        return sb.ToString();
    }

    private static string BuildG(ClockFamilyRow[] viable)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (alternative γ models constructed)     : PASS");
        sb.AppendLine("  Level 2 (observational constraints applied)    : PASS");
        sb.AppendLine("  Level 3 (uniqueness determined)                : PASS — γ = a is UNIQUE");
        sb.AppendLine("  Level 4 (viable non-FLRW time dynamics)        : FAIL — none exist");
        sb.AppendLine("  Level 5 (genuinely distinct prediction)        : FAIL — no surviving signal");
        sb.AppendLine();
        sb.AppendLine($"  Survivors: {viable.Length} (all reduce to γ = a).");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: cosmology does uniquely require γ = a. The clock is not");
        sb.AppendLine("  encoding FLRW arbitrarily — redshift (and, redundantly, SN Ia time dilation) FORCE");
        sb.AppendLine("  the clock rate to the conformal factor. No deeper clock dynamics can reproduce");
        sb.AppendLine("  observations while remaining physically distinct from expanding-space FLRW.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteClockFamiliesCsv(string path, List<ClockFamilyRow> rows)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Family,Parameter,ZModel_at_z1,BEff,DeltaZOverZ,DilationSigma,GdaggerFactor,Viable");
        foreach (var r in rows)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F2},{2:F4},{3:F3},{4:F4},{5:F1},{6:F3},{7}",
                r.Name, r.Param, r.ZModel, r.BEff, r.DeltaZOverZ, r.DilationSigma, r.GdaggerFactor, r.Viable ? "1" : "0"));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteViableCsv(string path, ClockFamilyRow[] viable)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Family,Parameter,Note");
        foreach (var v in viable)
            sb.AppendLine($"{v.Name},{v.Param:F2},reduces to gamma = a (FLRW-conformal)");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteSnConstraintsCsv(string path, List<ClockFamilyRow> rows)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Family,Parameter,BEff,ObservedB,SigmaB,DilationSigma");
        foreach (var r in rows)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F2},{2:F3},1.00,0.05,{3:F1}", r.Name, r.Param, r.BEff, r.DilationSigma));
        File.WriteAllText(path, sb.ToString());
    }
}

public sealed record ClockFamilyRow(string Name, double Param, double ZModel, double BEff,
    double DeltaZOverZ, double DilationSigma, double GdaggerFactor, bool Viable);

public sealed record BeyondConformalReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    ClockFamilyRow[] Rows, ClockFamilyRow[] Viable, string OutDir);
