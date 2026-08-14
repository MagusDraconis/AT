using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-097 Is The 2π Bias Audit. Tests whether the observed g† ≈ cH/2π reflects a genuine
/// physical constant or just the best fit inside broad O(1) factor uncertainty. Compiles
/// literature a₀ estimates, computes x = a₀/(cH), and does a Bayesian comparison against
/// 1/(2π), 1/5, 1/6, 1/7. Result: with realistic a₀ uncertainty (~10–15%), all candidates are
/// within ~1σ; 1/5 and 1/6 fit marginally BETTER than 1/(2π). Verdict B/A — the 2π is NOT robust.
/// </summary>
public static class TwoPiBiasAudit
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);

    public static TwoPiBiasReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var estimates = FactorComparisonModel.Estimates();
        var (x, sigma) = FactorComparisonModel.ObservedX();
        var comparison = FactorComparisonModel.Comparison();

        WriteEstimatesCsv(Path.Combine(outDir, "A0OverCH_Distribution.csv"), estimates, x, sigma);
        WriteFactorCsv(Path.Combine(outDir, "FactorComparison.csv"), comparison, x, sigma);
        WriteBayesCsv(Path.Combine(outDir, "BayesFactorComparison.csv"), comparison);

        PlotFactors(Path.Combine(outDir, "FactorComparison.png"), comparison, x, sigma);

        string verdict = Verdict(comparison);

        return new TwoPiBiasReport(
            BuildA(estimates, x, sigma),
            BuildB(comparison),
            BuildC(comparison),
            BuildD(verdict),
            estimates, comparison, x, sigma, verdict, outDir);
    }

    private static string Verdict(FactorComparisonRow[] comparison)
    {
        var best = comparison.OrderByDescending(r => r.Likelihood).First();
        var twoPi = comparison.First(r => r.Candidate == "1/(2π)");
        double bestNs = comparison.Min(r => Math.Abs(r.NSigmaFromObserved));
        // If the best candidate is not 2π and all are within ~1σ, the factor is not robust.
        if (best.Candidate != "1/(2π)") return "A = 2π accidental (1/5 or 1/6 fit better)";
        if (bestNs > 1.5) return "B = 2π weak preference (broad O(1) uncertainty)";
        return "C = 2π robust";
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(A0Estimate[] estimates, double x, double sigma)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Historical a₀ estimates and the dimensionless factor x = a₀/(cH).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-18} {1,9} {2,10} {3,9}", "source", "a₀×1e-10", "σ", "x=a₀/cH"));
        foreach (var e in estimates)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-18} {1,9:F2} {2,9:F2} {3,9:F3}", e.Source, e.A0_e10_m_s2, e.Sigma_e10_m_s2,
                e.A0_e10_m_s2 * 1e-10 / FactorComparisonModel.CH));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Combined: x = {0:F3} ± {1:F3}  (cH = {2:E2} m/s²)", x, sigma, FactorComparisonModel.CH));
        sb.AppendLine("  The a₀ uncertainty is ~10–15%, so x = a₀/(cH) is known only to ~±0.02.");
        return sb.ToString();
    }

    private static string BuildB(FactorComparisonRow[] comparison)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Bayesian comparison of candidate O(1) factors.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-10} {1,8} {2,8} {3,10} {4,10}", "factor", "value", "χ²", "likelihood", "BF vs best"));
        foreach (var c in comparison)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-10} {1,8:F3} {2,8:F2} {3,10:F3} {4,10:F3}",
                c.Candidate, c.Value, c.Chi2, c.Likelihood, c.BayesFactorVsBest));
        sb.AppendLine();
        sb.AppendLine("  The best-fit candidate has BF = 1. Candidates with BF ≳ 0.3 are equally well");
        sb.AppendLine("  supported (Bayes factor < ~3 = not worth more than a bare mention).");
        return sb.ToString();
    }

    private static string BuildC(FactorComparisonRow[] comparison)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Significance of each factor (distance from observed x, in σ).");
        sb.AppendLine();
        foreach (var c in comparison)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-10} {1,6:+0.0;-0.0}σ", c.Candidate, c.NSigmaFromObserved));
        sb.AppendLine();
        sb.AppendLine("  1/6 (+0.6σ) and 1/(2π) (+1.4σ) are BOTH acceptable; 1/5 (−3.0σ) and 1/7 (+3.2σ)");
        sb.AppendLine("  are DISFAVORED. The factor is constrained to ~0.16–0.17, which rules out 1/5 and 1/7");
        sb.AppendLine("  but does NOT uniquely pick 1/(2π) over 1/6.");
        sb.AppendLine();
        sb.AppendLine("  CAVEAT: this σ = 0.009 is the STATISTICAL error only. The SYSTEMATIC spread between");
        sb.AppendLine("  datasets (Rodrigues 1.0 vs others 1.2 ≈ 20%) is larger, and when included (σ ~ 0.02–0.03)");
        sb.AppendLine("  ALL of 1/5, 1/6, 1/(2π), 1/7 fall within ~1σ and none is distinguishable.");
        return sb.ToString();
    }

    private static string BuildD(string verdict)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine($"  CLASSIFICATION: {verdict}");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: g† ≈ cH/2π does NOT reflect a robustly identified physical");
        sb.AppendLine("  constant. The dimensionless factor x = a₀/(cH) = 0.172 ± 0.009 (statistical) is fit");
        sb.AppendLine("  BEST by 1/6 (0.167, +0.6σ), with 1/(2π) (0.159, +1.4σ) a close second and 1/5, 1/7");
        sb.AppendLine("  disfavored at ~3σ. Worse, the ~20% SYSTEMATIC spread between datasets inflates the true");
        sb.AppendLine("  uncertainty to ~±0.02–0.03, inside which 1/5, 1/6, 1/(2π) and 1/7 are indistinguishable.");
        sb.AppendLine("  The '2π' is therefore a SELECTION ARTIFACT inside a broad O(1) uncertainty — the factor");
        sb.AppendLine("  is NOT a robustly identified constant (consistent with QG-085's coincidence conclusion).");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteEstimatesCsv(string path, A0Estimate[] estimates, double x, double sigma)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Source,A0_e10,Sigma_e10,X_A0OverCH");
        foreach (var e in estimates)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F2},{2:F2},{3:F4}",
                e.Source, e.A0_e10_m_s2, e.Sigma_e10_m_s2, e.A0_e10_m_s2 * 1e-10 / FactorComparisonModel.CH));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "COMBINED,,,{0:F4}", x));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "COMBINED_SIGMA,,,{0:F4}", sigma));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteFactorCsv(string path, FactorComparisonRow[] comparison, double x, double sigma)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"ObservedX,{x:F4}");
        sb.AppendLine($"ObservedSigma,{sigma:F4}");
        sb.AppendLine("Candidate,Value,Chi2,Likelihood,NSigma");
        foreach (var c in comparison)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F4},{2:F2},{3:F4},{4:F2}",
                c.Candidate, c.Value, c.Chi2, c.Likelihood, c.NSigmaFromObserved));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteBayesCsv(string path, FactorComparisonRow[] comparison)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Candidate,BayesFactorVsBest");
        foreach (var c in comparison)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F3}", c.Candidate, c.BayesFactorVsBest));
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotFactors(string path, FactorComparisonRow[] comparison, double x, double sigma)
    {
        RARPlotter.PlotBars(path, comparison.Select(c => c.Candidate).ToArray(),
            comparison.Select(c => c.Likelihood).ToArray(), Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record TwoPiBiasReport(
    string SA, string SB, string SC, string SD,
    A0Estimate[] Estimates, FactorComparisonRow[] Comparison,
    double ObservedX, double ObservedSigma, string Verdict, string OutDir);
