using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-098 Cosmological Rate Emergence Audit. Determines whether H is itself emergent from
/// causal discreteness, so that Λ ~ 1/√N and a₀ ~ cH both derive from a single emergent rate.
/// Result: H is NOT emergent — it is the INPUT (N = (R_H/l_P)⁴ uses R_H = c/H). Λ and a₀ are two
/// projections of the SAME (input) rate H, but H must be put in. Levels 2–3 pass; Level 1 and 5 fail.
/// </summary>
public static class CosmologicalRateEmergenceAudit
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);

    public static CosmologicalRateEmergenceReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        double n = EmergentRateModel.NFromH();
        double lambdaPred = EmergentRateModel.LambdaFromN();
        double a0Pred = EmergentRateModel.A0FromRate();

        WriteRateModelsCsv(Path.Combine(outDir, "EmergentRateModels.csv"), n, lambdaPred, a0Pred);
        WriteLambdaCsv(Path.Combine(outDir, "LambdaRateConnections.csv"));
        WriteAccelerationCsv(Path.Combine(outDir, "AccelerationRateConnections.csv"));

        PlotScales(Path.Combine(outDir, "EmergentRateScales.png"));

        return new CosmologicalRateEmergenceReport(
            BuildA(n, lambdaPred, a0Pred), BuildB(), BuildC(), BuildD(), BuildE(),
            n, lambdaPred, a0Pred, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(double n, double lambdaPred, double a0Pred)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Deriving Λ and a₀ from causal order + local finiteness + N (no FLRW).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  N = (R_H/l_P)⁴ = {0:E2}  (R_H = c/H, so H is the INPUT)", n));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Λ = 1/√N / l_P² = {0:E2} m⁻²  (observed Λ = 1.1e-52)", lambdaPred));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  a₀ = c·H = {0:E2} m/s²  (observed a₀ = 1.2e-10, order of magnitude)", a0Pred));
        sb.AppendLine();
        sb.AppendLine("  Λ ~ H²/c² and a₀ ~ cH — both are powers of the SAME rate H. But H is the INPUT:");
        sb.AppendLine("  N is DEFINED by H (via R_H = c/H), so H does not EMERGE from N.");
        return sb.ToString();
    }

    private static string BuildB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Does a natural rate R emerge from N without inserting H?");
        sb.AppendLine();
        sb.AppendLine("  NO. The only dimensionless quantities from N alone are N and 1/√N. To get a RATE");
        sb.AppendLine("  (s⁻¹) from N one must divide by a time scale — and the only natural time is 1/H,");
        sb.AppendLine("  which is H again. So R = H is INSERTED, not derived. The 'rate from N' is circular:");
        sb.AppendLine("  N = (c/H l_P)⁴ already requires H.");
        sb.AppendLine();
        sb.AppendLine("  ⇒ H is the INPUT; Λ and a₀ are two different projections of it (dimensional relations).");
        return sb.ToString();
    }

    private static string BuildC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("All natural scales from the rate R = H.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  cR = cH = {0:E2} m/s² (a₀, order of magnitude)", EmergentRateModel.CH));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  R²/c² = H²/c² = {0:E2} m⁻² (Λ, the 'why now' coincidence)", EmergentRateModel.H0PerS * EmergentRateModel.H0PerS / (EmergentRateModel.C * EmergentRateModel.C)));
        sb.AppendLine();
        sb.AppendLine("  a₀ and Λ are DIFFERENT powers of the SAME rate H: a₀ ~ c·H (linear), Λ ~ H²/c² (square).");
        sb.AppendLine("  They are projections of one input, not two independent emergences.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Hostile audit — assume Λ and a₀ are unrelated.");
        sb.AppendLine();
        sb.AppendLine("  Independent model: Λ (bare constant, 1 param) + a₀ (MOND constant, 1 param) = 2 params,");
        sb.AppendLine("  no relation. Common-rate model: 1 param (H) gives both Λ ~ H²/c² and a₀ ~ cH = 0 extra");
        sb.AppendLine("  params (given H). The common-rate model is more ECONOMICAL by one parameter, but it");
        sb.AppendLine("  does not EXPLAIN H — it just re-expresses Λ and a₀ in terms of the observed H.");
        sb.AppendLine();
        sb.AppendLine("  The economy is real but shallow: H is the observed expansion rate, not a derived");
        sb.AppendLine("  principle. The common-rate model is a compact notation, not a deeper theory.");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (rate derived from N)                 : FAIL — H is the input, not derived");
        sb.AppendLine("  Level 2 (Λ recovered)                         : PASS (Λ ~ H²/c² = 1/√N)");
        sb.AppendLine("  Level 3 (a₀ recovered, order of magnitude)    : PASS (a₀ ~ cH)");
        sb.AppendLine("  Level 4 (single emergent-rate picture)        : PARTIAL — single INPUT rate H, not emergent");
        sb.AppendLine("  Level 5 (distinct observational consequence)  : FAIL");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: Λ ~ 1/√N and a₀ ~ cH are consequences of the SAME rate H,");
        sb.AppendLine("  but H is NOT emergent from causal discreteness — it is the input that defines N. The");
        sb.AppendLine("  'single cosmic rate' is the observed H, re-expressing Λ and a₀ as its square and linear");
        sb.AppendLine("  projections. No distinct prediction emerges.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteRateModelsCsv(string path, double n, double lambdaPred, double a0Pred)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Quantity,Value,Note");
        sb.AppendLine($"N,{n:E2},(R_H/l_P)^4 with R_H=c/H (H input)");
        sb.AppendLine($"Lambda_pred,{lambdaPred:E2},= 1/sqrt(N)/l_P^2");
        sb.AppendLine($"A0_pred,{a0Pred:E2},= cH (order of magnitude)");
        sb.AppendLine($"HEmergesFromN,{EmergentRateModel.HEmergesFromN},");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteLambdaCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Relation,Value");
        sb.AppendLine($"Lambda~1/sqrt(N),{EmergentRateModel.LambdaFromN():E2}");
        sb.AppendLine($"Lambda~H2/c2,{EmergentRateModel.H0PerS * EmergentRateModel.H0PerS / (EmergentRateModel.C * EmergentRateModel.C):E2}");
        sb.AppendLine($"ObservedLambda,{EmergentRateModel.Lambda:E2}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteAccelerationCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Relation,Value");
        sb.AppendLine($"a0~cH,{EmergentRateModel.A0FromRate():E2}");
        sb.AppendLine($"ObservedA0,{EmergentRateModel.A0:E2}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotScales(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "a₀~cH", "a₀(obs)", "Λ~H²/c²", "Λ(obs)" },
            new[] { EmergentRateModel.A0FromRate(), EmergentRateModel.A0,
                    EmergentRateModel.H0PerS * EmergentRateModel.H0PerS / (EmergentRateModel.C * EmergentRateModel.C),
                    EmergentRateModel.Lambda }.Select(v => Math.Log10(Math.Max(v, 1e-200)) + 20).ToArray(), Blue);
    }
}

public sealed record CosmologicalRateEmergenceReport(
    string SA, string SB, string SC, string SD, string SE,
    double N, double LambdaPred, double A0Pred, string OutDir);
