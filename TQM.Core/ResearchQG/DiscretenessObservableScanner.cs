using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-094 Observable Consequences Of Causal Discreteness Audit. Catalogs causal-set effects,
/// estimates signal amplitudes, and compares against detector sensitivities. Result: every
/// Planck-suppressed signature (Λ fluctuation, GW phase noise, photon delay, LSS excess) has
/// S/N ≪ 1 — unobservable. The CMB low-l cosmic variance is O(1) but is NOT a unique discreteness
/// signature (continuum cosmic variance is identical). Only Λ itself (already observed) survives.
/// </summary>
public static class DiscretenessObservableScanner
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);
    static readonly Rgb24 Orange = new(230, 140, 20);

    public static DiscretenessObservableReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var signals = SignalDetectabilityAnalyzer.Signals();
        var ranked = SignalDetectabilityAnalyzer.Ranked();

        WriteSignalsCsv(Path.Combine(outDir, "CausalDiscretenessSignals.csv"), signals);
        WriteRankingCsv(Path.Combine(outDir, "ObservableRanking.csv"), ranked);
        WriteLambdaCsv(Path.Combine(outDir, "LambdaFluctuationAnalysis.csv"));
        WriteContinuumCsv(Path.Combine(outDir, "DiscretenessVsContinuum.csv"), signals);

        PlotSignals(Path.Combine(outDir, "DiscretenessSignalStrength.png"), ranked);
        PlotSensitivity(Path.Combine(outDir, "DetectorSensitivityComparison.png"), signals);
        PlotLambda(Path.Combine(outDir, "LambdaFluctuationSpectrum.png"));

        return new DiscretenessObservableReport(
            BuildA(), BuildB(signals), BuildC(ranked), BuildD(ranked), BuildE(), BuildF(),
            signals, ranked, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Discreteness scale: l_P = 1.62e-35 m; N = (R_H/l_P)⁴ ≈ 5.2e243.");
        sb.AppendLine("Known causal-set effects: ever-present Λ, Poisson (√N) fluctuations, swerving,");
        sb.AppendLine("stochastic geometry / phase noise.");
        sb.AppendLine();
        sb.AppendLine("  Two suppression scales: (1) 1/√N ~ 1e-122 for cosmological fluctuations;");
        sb.AppendLine("  (2) l_P/λ ~ 1e-28 for propagation effects. Both are astronomically small.");
        return sb.ToString();
    }

    private static string BuildB(DiscretenessSignal[] signals)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Signal amplitudes vs detector sensitivities.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-22} {1,12} {2,12} {3,10}", "channel", "amplitude", "sensitivity", "S/N"));
        foreach (var s in signals)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-22} {1,12:E1} {2,12:E1} {3,10:F0}",
                s.Channel, s.Amplitude, s.DetectorSensitivity, s.SignalToNoise));
        sb.AppendLine();
        sb.AppendLine("  S/N ≪ 1 for every Planck-suppressed channel — unobservable.");
        return sb.ToString();
    }

    private static string BuildC(DiscretenessSignal[] ranked)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Ranking by detectability (S/N descending).");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,4} {1,-22} {2,10} {3,8}", "rank", "channel", "S/N", "note"));
        for (int i = 0; i < ranked.Length; i++)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,4} {1,-22} {2,10:F0} {3}", i + 1, ranked[i].Channel, ranked[i].SignalToNoise,
                ranked[i].Channel.StartsWith("dark") || ranked[i].Channel.StartsWith("CMB") ? "(already observed / not unique)" : ""));
        sb.AppendLine();
        sb.AppendLine("  The only O(1) entries are dark energy (already observed) and the CMB low-l variance");
        sb.AppendLine("  (O(1) but identical in continuum cosmology — not a unique discreteness signature).");
        return sb.ToString();
    }

    private static string BuildD(DiscretenessSignal[] ranked)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Detection feasibility.");
        sb.AppendLine();
        sb.AppendLine("  - Λ fluctuation (δΛ/Λ ~ 1e-122): needs a 'dark-energy interferometer' with ~1e122");
        sb.AppendLine("    dynamic range — impossible.");
        sb.AppendLine("  - GW phase noise (~l_P·f/c ~ 1e-36): LIGO phase precision ~1e-22, ET/CE ~1e-23 →");
        sb.AppendLine("    ~13 orders of magnitude short.");
        sb.AppendLine("  - Photon time-delay (~l_P/c ~ 1e-43 s): best pulsar timing ~1e-6 s → ~37 orders short.");
        sb.AppendLine("  - LSS correlation excess (~1/√N): below cosmic-variance floor by ~1e-122.");
        sb.AppendLine();
        sb.AppendLine("  NO current or next-generation instrument (JWST, Rubin, Euclid, LISA, ET, CE) can");
        sb.AppendLine("  detect any Planck-suppressed discreteness signature.");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Hostile audit — assume spacetime is continuous.");
        sb.AppendLine();
        sb.AppendLine("  Every candidate signature has a continuum explanation:");
        sb.AppendLine("    - Λ: a bare cosmological constant (or quintessence).");
        sb.AppendLine("    - CMB low-l variance: ordinary cosmic variance (identical in continuum).");
        sb.AppendLine("    - GW/photon noise: below all noise floors; no signal to explain.");
        sb.AppendLine("    - LSS excess: continuum cosmic variance.");
        sb.AppendLine();
        sb.AppendLine("  Continuum cosmology explains ALL candidate signatures with equal economy — there is");
        sb.AppendLine("  no observation that discreteness explains better. Explanatory power is TIED.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (effects cataloged)            : PASS");
        sb.AppendLine("  Level 2 (amplitudes estimated)         : PASS");
        sb.AppendLine("  Level 3 (most promising observable)    : PASS — none (all S/N ≪ 1)");
        sb.AppendLine("  Level 4 (detection feasibility)        : PASS — quantified as impossible");
        sb.AppendLine("  Level 5 (realistic experiment)         : FAIL — no instrument reaches l_P");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: causal discreteness leaves NO observable trace beyond Λ.");
        sb.AppendLine("  Every Planck-suppressed signature (Λ fluctuation ~1e-122, GW phase noise ~1e-36, photon");
        sb.AppendLine("  delay ~1e-43, LSS excess ~1e-122) is many orders of magnitude below all detector");
        sb.AppendLine("  sensitivities. The CMB low-l variance is O(1) but not unique. So Λ ~ 1/√N remains a");
        sb.AppendLine("  genuine but UNTESTABLE postdiction — its only observable consequence is Λ itself.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteSignalsCsv(string path, DiscretenessSignal[] signals)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Channel,Effect,Amplitude,DetectorSensitivity,SignalToNoise,Observable");
        foreach (var s in signals)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1},{2:E1},{3:E1},{4:F0},{5}",
                s.Channel, Escape(s.Effect), s.Amplitude, s.DetectorSensitivity, s.SignalToNoise, s.Observable ? "1" : "0"));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteRankingCsv(string path, DiscretenessSignal[] ranked)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rank,Channel,SignalToNoise,Observable");
        for (int i = 0; i < ranked.Length; i++)
            sb.AppendLine($"{i + 1},{ranked[i].Channel},{ranked[i].SignalToNoise:F0},{ranked[i].Observable}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteLambdaCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Quantity,Value");
        sb.AppendLine($"MeanLambdaPlanck,{LambdaFluctuationAnalyzer.MeanLambdaPlanck:E2}");
        sb.AppendLine($"RelativeFluctuation,{LambdaFluctuationAnalyzer.FluctuationRelative:E2}");
        sb.AppendLine($"Conclusion,{Escape(LambdaFluctuationAnalyzer.Conclusion)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteContinuumCsv(string path, DiscretenessSignal[] signals)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Signature,ContinuumExplanation");
        sb.AppendLine("Lambda,bare cosmological constant or quintessence");
        sb.AppendLine("CMB low-l variance,ordinary cosmic variance (identical in continuum)");
        sb.AppendLine("GW phase noise,below all noise floors (no signal)");
        sb.AppendLine("photon time-delay,below all timing precision (no signal)");
        sb.AppendLine("LSS correlation excess,continuum cosmic variance");
        File.WriteAllText(path, sb.ToString());
    }

    // ---------------------------------------------------------------------
    // Plots
    // ---------------------------------------------------------------------

    private static void PlotSignals(string path, DiscretenessSignal[] ranked)
    {
        RARPlotter.PlotBars(path, ranked.Select(s => s.Channel).ToArray(),
            ranked.Select(s => Math.Log10(Math.Max(s.SignalToNoise, 1e-120)) + 120).ToArray(), Blue);
    }

    private static void PlotSensitivity(string path, DiscretenessSignal[] signals)
    {
        // Shift log10(amplitude) by +130 to make bars positive (PlotBars requires positive values).
        RARPlotter.PlotBars(path, signals.Select(s => s.Channel).ToArray(),
            signals.Select(s => Math.Log10(s.Amplitude) + 130.0).ToArray(), Green);
    }

    private static void PlotLambda(string path)
    {
        // Shift by +130 to make positive.
        RARPlotter.PlotBars(path,
            new[] { "mean Λ", "δΛ (fluctuation)" },
            new[] { Math.Log10(LambdaFluctuationAnalyzer.MeanLambdaPlanck) + 130.0,
                    Math.Log10(LambdaFluctuationAnalyzer.MeanLambdaPlanck * LambdaFluctuationAnalyzer.FluctuationRelative) + 130.0 }, Orange);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record DiscretenessObservableReport(
    string SA, string SB, string SC, string SD, string SE, string SF,
    DiscretenessSignal[] Signals, DiscretenessSignal[] Ranked, string OutDir);
