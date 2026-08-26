using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-089 Cosmic Rate First Audit. Tests whether H can be treated as a primitive rate from
/// which time, scale factor, redshift and a₀ emerge. Result: H and a(t) are information-
/// equivalent (bijective up to normalization), so "rate-first" is a tautological re-parameterization
/// of FLRW — the reconstruction is exact but adds no new physics or prediction.
/// </summary>
public static class RateFirstAnalyzer
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);

    public static RateFirstReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        double[] zs = { 0.0, 0.5, 1.0, 2.0, 4.0 };
        var points = CosmicRateModel.Points(zs);

        WritePointsCsv(Path.Combine(outDir, "RateFirstModels.csv"), points);
        WriteTimeCsv(Path.Combine(outDir, "EmergentTimeFromRate.csv"), zs);
        WriteExpansionCsv(Path.Combine(outDir, "EmergentExpansionFromRate.csv"), points);
        WriteAccelerationCsv(Path.Combine(outDir, "AccelerationFromRate.csv"));

        PlotRate(Path.Combine(outDir, "RateVsScaleFactor.png"), points);

        return new RateFirstReport(
            BuildA(points),
            BuildB(points),
            BuildC(zs),
            BuildD(),
            BuildE(),
            BuildF(),
            BuildG(),
            points, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(CosmicRatePoint[] points)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rate-first formalism: R(t) fundamental; time t = ∫ dR⁻¹; a and redshift emerge.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,5} {1,11} {2,10} {3,10}", "z", "R=H [s⁻¹]", "a=1/(1+z)", "t [Gyr]"));
        foreach (var p in points)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,5:F1} {1,11:E2} {2,10:F3} {3,10:F2}", p.Z, p.Rate, p.ScaleFactor, p.TimeGyr));
        sb.AppendLine();
        sb.AppendLine("  R = H = d ln a/dt and a = 1/(1+z) are BIDIRECTIONALLY reconstructible: H and a");
        sb.AppendLine("  carry identical information (up to the a(0)=1 normalization).");
        return sb.ToString();
    }

    private static string BuildB(CosmicRatePoint[] points)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Does H contain more information than a(t) or t?");
        sb.AppendLine();
        sb.AppendLine("  H(t) = d ln a/dt  ⇒  a(t) = exp(∫ H dt) (up to a(0)=1).");
        sb.AppendLine("  Given a(t), H = d ln a/dt.  Given H(t), a = exp(∫ H dt).");
        sb.AppendLine("  ⇒ H and a are BIJECTIVE (information-equivalent). t is the parameter of both.");
        sb.AppendLine();
        sb.AppendLine("  H contains NO more information than a(t) or t. 'Rate-first' is not a deeper");
        sb.AppendLine("  description — it is FLRW with H as the given function.");
        return sb.ToString();
    }

    private static string BuildC(double[] zs)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Emergent time: t = ∫ dR⁻¹ = ∫ dN/R, N = ∫ R dt.");
        sb.AppendLine();
        sb.AppendLine("  This is a TAUTOLOGY: it inverts the definition N = ∫ R dt. R(t) already requires t,");
        sb.AppendLine("  so 'time emerges from the rate' is circular. Reconstruction is exact by construction:");
        foreach (double z in zs)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "    z={0,3:F1}  t_reconstructed = {1,6:F2} Gyr  (= age, exact: {2})",
                z, EmergentTime.ReconstructedTimeGyr(z), EmergentTime.ReconstructionIsExact(z)));
        sb.AppendLine();
        sb.AppendLine("  Time does NOT emerge from H; H is a rate WITH RESPECT TO time.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Redshift from rate: 1+z = exp(∫ R dt) = a_obs/a_emit.");
        sb.AppendLine();
        sb.AppendLine("  Since R = d ln a/dt, this is IDENTICAL to the FLRW redshift. Rate-driven redshift is");
        sb.AppendLine("  equivalent to metric-expansion redshift (no new observable).");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("a₀ from the rate: a₀ = c·R.");
        sb.AppendLine();
        sb.AppendLine($"  a₀ = c·H     = {RateDerivedAcceleration.CH():E2} m/s²  (the 'cH class', no 2π)");
        sb.AppendLine($"  g† = c·H/2π  = {RateDerivedAcceleration.Gdagger():E2} m/s²  (the angular-frequency rate)");
        sb.AppendLine();
        sb.AppendLine("  The rate-first origin reproduces the SAME cH vs cH/2π ambiguity as QG-084/085 — it");
        sb.AppendLine("  does not resolve the 2π, and gives a₀ only at order-of-magnitude.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Links to thermal time, causal sets, information/computational/relational time.");
        sb.AppendLine();
        sb.AppendLine("  - Thermal time (Connes-Rovelli): time = modular flow of a thermal state; equivalent");
        sb.AppendLine("    to coordinate time in the relevant regimes.");
        sb.AppendLine("  - Causal sets: growth = element count (event-count, QG-087).");
        sb.AppendLine("  - Relational time (Rovelli): time = correlations with a clock; a gauge choice.");
        sb.AppendLine("  - Computational universe: rate ~ Mc²/ħ (mass-set), NOT H.");
        sb.AppendLine();
        sb.AppendLine("  None of these makes H a new fundamental; they re-derive time as a gauge/clock.");
        return sb.ToString();
    }

    private static string BuildG()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (rate-first formalism)         : PASS");
        sb.AppendLine("  Level 2 (time reconstructed)           : PASS but TAUTOLOGICAL");
        sb.AppendLine("  Level 3 (redshift reconstructed)       : PASS but = FLRW");
        sb.AppendLine("  Level 4 (a₀ emerges)                  : PARTIAL — a₀ = cH (no 2π)");
        sb.AppendLine("  Level 5 (prediction ≠ FLRW)            : FAIL");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: the true cosmological degree of freedom is NOT a rate. A");
        sb.AppendLine("  rate is a rate of something WITH RESPECT TO time; 'H fundamental, time emerges' is either");
        sb.AppendLine("  circular (H(t) needs t) or equivalent to event-count (H(N) needs N = a). Rate-first is");
        sb.AppendLine("  FLRW with H as the given function — no new physics, no new prediction. This CLOSES the");
        sb.AppendLine("  entire QG-080–089 reinterpretation program: every 'X-first' alternative collapses to FLRW.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WritePointsCsv(string path, CosmicRatePoint[] points)
    {
        var sb = new StringBuilder();
        sb.AppendLine("z,Rate_s,ScaleFactor,AgeGyr");
        foreach (var p in points)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:F1},{1:E2},{2:F4},{3:F2}",
                p.Z, p.Rate, p.ScaleFactor, p.TimeGyr));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteTimeCsv(string path, double[] zs)
    {
        var sb = new StringBuilder();
        sb.AppendLine("z,ReconstructedTimeGyr,Exact");
        foreach (double z in zs)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:F1},{1:F2},{2}",
                z, EmergentTime.ReconstructedTimeGyr(z), EmergentTime.ReconstructionIsExact(z)));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteExpansionCsv(string path, CosmicRatePoint[] points)
    {
        var sb = new StringBuilder();
        sb.AppendLine("z,ScaleFactor,RedshiftFromRate");
        foreach (var p in points)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:F1},{1:F4},{2:F4}",
                p.Z, p.ScaleFactor, RateDrivenRedshift.RedshiftFromRate(p.Z)));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteAccelerationCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rate,A0_m_s2");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "H,{0:E2}", RateDerivedAcceleration.CH()));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "H/2pi,{0:E2}", RateDerivedAcceleration.Gdagger()));
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotRate(string path, CosmicRatePoint[] points)
    {
        RARPlotter.PlotBars(path, points.Select(p => $"z={p.Z:F1}").ToArray(),
            points.Select(p => p.Rate * 1e18).ToArray(), Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record RateFirstReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    CosmicRatePoint[] Points, string OutDir);
