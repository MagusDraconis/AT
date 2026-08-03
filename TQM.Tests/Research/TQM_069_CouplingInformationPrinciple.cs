using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_069_CouplingInformationPrinciple : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 690314827;
    private const int RandomFuncCount = 100;
    private const int SeedsPerFunc = 2;

    public TQM_069_CouplingInformationPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_069_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-069 Coupling Information Principle");

        sb.AppendLine("TQM-069: Which Mathematical Property of a Coupling Function Predicts Attraction?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  TQM-062: Spatial attraction exists.");
        sb.AppendLine("  TQM-064: Attraction across many coupling laws.");
        sb.AppendLine("  TQM-065: Symmetry alone does NOT explain attraction.");
        sb.AppendLine("  TQM-066: No universal coupling regime exists.");
        sb.AppendLine("  TQM-068: Curvature does not drive motion.");
        sb.AppendLine();
        sb.AppendLine("  The remaining question:");
        sb.AppendLine("  What property of the coupling law generates attraction?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: Attraction is not controlled by coupling");
        sb.AppendLine("  strength, symmetry, memory, or curvature — but by an");
        sb.AppendLine("  information-theoretic property of the coupling function.");
        sb.AppendLine();

        // ── Section 2: Function Library ──────────────────────────────
        Sec(sb, "2. Function Library");
        sb.AppendLine($"  Named functions: {CouplingInformationAnalyzer.NamedFunctions.Count}");
        foreach (var name in CouplingInformationAnalyzer.NamedFunctions.Keys)
            sb.AppendLine($"    {name}");
        sb.AppendLine();
        sb.AppendLine($"  Random smooth functions: {RandomFuncCount}");
        sb.AppendLine($"    Generated via Fourier series (3-7 modes,");
        sb.AppendLine($"    coefficients ∝ 1/k², normalized to unit L2).");
        sb.AppendLine($"  Total functions: {CouplingInformationAnalyzer.NamedFunctions.Count + RandomFuncCount}");
        sb.AppendLine($"  Seeds per function: {SeedsPerFunc}");
        sb.AppendLine($"  Total runs: {(CouplingInformationAnalyzer.NamedFunctions.Count + RandomFuncCount) * SeedsPerFunc}");
        sb.AppendLine();

        // ── Section 3: Function Metrics ──────────────────────────────
        Sec(sb, "3. Function Metric Definitions");
        sb.AppendLine("  Metric              │ Description");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        sb.AppendLine("  SymmetryScore       │ Fraction of energy in odd component (0=even, 1=odd)");
        sb.AppendLine("  EvenEnergyFraction  │ Fraction of energy in even(f) = (f(x)+f(-x))/2");
        sb.AppendLine("  OddEnergyFraction   │ Fraction of energy in odd(f) = (f(x)-f(-x))/2");
        sb.AppendLine("  MeanValue           │ Average value over [-π, π]");
        sb.AppendLine("  Variance            │ Variance of f(x) over [-π, π]");
        sb.AppendLine("  Entropy             │ Shannon entropy of discretized f(x)");
        sb.AppendLine("  ZeroCrossings       │ Number of sign changes");
        sb.AppendLine("  PositiveArea        │ ∫ max(f(x), 0) dx");
        sb.AppendLine("  NegativeArea        │ ∫ max(-f(x), 0) dx");
        sb.AppendLine("  AreaRatio           │ Positive / (|Positive| + |Negative|)");
        sb.AppendLine("  AvgGradient         │ Mean |f'(x)|");
        sb.AppendLine("  AvgCurvature        │ Mean |f''(x)|");
        sb.AppendLine("  L1Norm / L2Norm     │ ∫|f|dx, sqrt(∫f²dx)");
        sb.AppendLine("  HighFreqEnergy      │ Fourier energy in harmonics k ≥ 3");
        sb.AppendLine("  DifferentialEntropy │ -∫ f(x) log f(x) dx (histogram approx)");
        sb.AppendLine();

        // ── Run analysis ─────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var (namedResults, randomResults) = CouplingInformationAnalyzer.RunFullAnalysis(
            K, Lambda, NPerGroup, SeedsPerFunc, RandomFuncCount, BaseSeed);
        sw.Stop();
        var allResults = namedResults.Concat(randomResults).ToList();

        sb.AppendLine($"  Analysis completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine($"  Total predictors: {allResults.Count}");
        sb.AppendLine();

        // ── Section 4: Attraction Results ────────────────────────────
        Sec(sb, "4. Attraction Results per Function");
        sb.AppendLine("  Function              │ AttrScore │ Converge?│ FinalSep │ Sync?");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var r in namedResults.OrderByDescending(r => r.AttractionScore))
        {
            string fname = r.Descriptor.Name.Length > 20
                ? r.Descriptor.Name[..20] : r.Descriptor.Name;
            sb.AppendLine($"  {fname,-20} │ {r.AttractionScore,8:P1} │ {(r.AttractionScore > 0.5 ? "\u25BC YES" : "\u25B2 no "),8} │ {r.FinalSeparation,8:F4} │ {(r.SyncProbability > 0.5 ? "\u2713" : " "),4}");
        }
        sb.AppendLine($"  ... ({randomResults.Count} random functions summarized below) ...");
        sb.AppendLine();

        // Summary of random functions.
        double randAttrMean = randomResults.Average(r => r.AttractionScore);
        double randAttrStd = Math.Sqrt(randomResults.Average(r =>
            (r.AttractionScore - randAttrMean) * (r.AttractionScore - randAttrMean)));
        int randConverge = randomResults.Count(r => r.AttractionScore > 0.5);
        int randSync = randomResults.Count(r => r.SyncProbability > 0.5);
        sb.AppendLine($"  Random functions summary ({randomResults.Count} runs):");
        sb.AppendLine($"    Mean attraction: {randAttrMean:P1} ± {randAttrStd:P1}");
        sb.AppendLine($"    Convergent: {randConverge}/{randomResults.Count} ({100.0*randConverge/randomResults.Count:F0}%)");
        sb.AppendLine($"    Synchronized: {randSync}/{randomResults.Count} ({100.0*randSync/randomResults.Count:F0}%)");
        sb.AppendLine();

        // ── Section 5: Feature Analysis ──────────────────────────────
        Sec(sb, "5. Feature-Predictor Ranking");

        var report = CouplingInformationAnalyzer.Analyze(allResults);

        sb.AppendLine("  Rank │ Feature                    │ Pearson r │ Spearman r│ Mutual Info");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        int rank = 0;
        foreach (var f in report.Rankings.Take(15))
        {
            rank++;
            string sign = f.PearsonR >= 0 ? "+" : "";
            string fname = f.FeatureName.Length > 26 ? f.FeatureName[..26] : f.FeatureName;
            sb.AppendLine($"  {rank,3}  │ {fname,-26} │ {sign}{f.PearsonR,8:F4} │ {f.SpearmanR,8:F4} │ {f.MutualInformation,8:F4}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Which function property best predicts attraction?");
        sb.AppendLine($"    {report.TopPredictor} (r = {report.TopPredictorR:F4})");
        string q1Verdict = Math.Abs(report.TopPredictorR) > 0.3
            ? $"This property is a significant predictor — it explains {report.TopPredictorR * report.TopPredictorR * 100:F0}% of variance." : "No single property strongly predicts attraction.";
        sb.AppendLine($"    {q1Verdict}");
        sb.AppendLine();

        // Find area-related and symmetry-related rankings.
        var symmetryRank = report.Rankings.FirstOrDefault(r =>
            r.FeatureName.Contains("Symmetry") || r.FeatureName.Contains("EvenEnergy"));
        var areaRank = report.Rankings.FirstOrDefault(r =>
            r.FeatureName.Contains("Area") || r.FeatureName.Contains("Positive"));

        sb.AppendLine("  Q2: Is positive area more important than symmetry?");
        double symR = symmetryRank?.PearsonR ?? 0;
        double areaR = areaRank?.PearsonR ?? 0;
        sb.AppendLine($"    Symmetry r = {symR:F4}, Area r = {areaR:F4}");
        sb.AppendLine($"    {(Math.Abs(areaR) > Math.Abs(symR) ? "YES — Area-based metrics outperform symmetry" : "NO — Symmetry is the stronger predictor")}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Can attraction be predicted from a single metric?");
        sb.AppendLine($"    Best single r = {report.BestSinglePredictorR:F4}");
        sb.AppendLine($"    {(report.BestSinglePredictorR > 0.5 ? "YES — Strong single-metric prediction" : report.BestSinglePredictorR > 0.3 ? "PARTIALLY — Moderate prediction possible" : "NO — No single metric suffices")}");
        sb.AppendLine();

        // Check if entropy-based metrics rank highly.
        var entropyRank = report.Rankings.FirstOrDefault(r =>
            r.FeatureName.Contains("Entropy"));
        sb.AppendLine("  Q4: Does an information-theoretic quantity emerge?");
        double entR = entropyRank?.PearsonR ?? 0;
        sb.AppendLine($"    Entropy r = {entR:F4} (rank: {report.Rankings.IndexOf(entropyRank!) + 1})");
        sb.AppendLine($"    {(Math.Abs(entR) > 0.3 ? "YES — Entropy/information metrics are strong predictors" : "NO — Information-theoretic metrics are not the dominant predictor")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can a universal attraction predictor be derived?");
        sb.AppendLine($"    Top predictor: {report.TopPredictor}");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine($"    {(report.Classification.StartsWith("D:") ? "YES — A universal coupling principle exists" : report.Classification.StartsWith("C:") ? "PARTIALLY — A strong predictor exists but is not universal" : "NO — No universal predictor found")}");
        sb.AppendLine();

        // ── Section 6: Information-Theoretic Analysis ─────────────────
        Sec(sb, "6. Information-Theoretic Analysis");

        // Check mutual information for top features.
        sb.AppendLine("  Mutual information between top features and attraction:");
        sb.AppendLine("  Feature                    │ MI (bits) │ Normalized MI");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        double maxMI = report.Rankings.Max(r => r.MutualInformation);
        foreach (var f in report.Rankings.Take(10))
        {
            double normMI = maxMI > 0 ? f.MutualInformation / maxMI : 0;
            string fname = f.FeatureName.Length > 26 ? f.FeatureName[..26] : f.FeatureName;
            sb.AppendLine($"  {fname,-26} │ {f.MutualInformation,8:F4} │ {normMI,8:P1}");
        }
        sb.AppendLine();

        // Joint entropy analysis of top 2 features.
        int topN = Math.Min(3, report.Rankings.Count);
        sb.AppendLine($"  Top-{topN} feature joint analysis:");
        for (int i = 0; i < topN; i++)
        {
            sb.AppendLine($"    #{i + 1}: {report.Rankings[i].FeatureName}");
            sb.AppendLine($"      Pearson r = {report.Rankings[i].PearsonR:F4}");
            sb.AppendLine($"      Spearman r = {report.Rankings[i].SpearmanR:F4}");
            sb.AppendLine($"      MI = {report.Rankings[i].MutualInformation:F4} bits");
        }
        sb.AppendLine();

        // ── Section 7: Named Function Metrics Table ──────────────────
        Sec(sb, "7. Named Function Detailed Metrics");
        sb.AppendLine("  Function         │ EvenFrac│ OddFrac │ AreaRatio│ Var   │ Entropy │ Zeros │ AttrScore");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        // Group by function name (average over seeds).
        foreach (var name in CouplingInformationAnalyzer.NamedFunctions.Keys)
        {
            var group = namedResults.Where(r => r.Descriptor.Name == name).ToList();
            if (group.Count == 0) continue;
            var d = group[0].Descriptor;
            double attr = group.Average(r => r.AttractionScore);
            string fname = d.Name.Length > 16 ? d.Name[..16] : d.Name;
            sb.AppendLine($"  {fname,-16} │ {d.EvenEnergyFraction,7:F3} │ {d.OddEnergyFraction,7:F3} │ {d.AreaRatio,7:F3} │ {d.Variance,5:F3} │ {d.Entropy,6:F3} │ {d.ZeroCrossings,4}  │ {attr,8:P1}");
        }
        sb.AppendLine();

        // ── Section 8: Interpretation ────────────────────────────────
        Sec(sb, "8. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Top predictor: {report.TopPredictor}");
        sb.AppendLine($"  Best single r: {report.BestSinglePredictorR:F4}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // Detailed interpretation.
        var top2 = report.Rankings.Take(2).ToList();
        sb.AppendLine("  Scientific meaning:");
        if (Math.Abs(report.BestSinglePredictorR) < 0.2)
        {
            sb.AppendLine("    No single static property of the coupling function");
            sb.AppendLine("    strongly predicts attraction. This suggests that");
            sb.AppendLine("    attraction is a DYNAMICAL phenomenon — it emerges");
            sb.AppendLine("    from the interaction between the coupling function");
            sb.AppendLine("    and the phase dynamics, not from any static");
            sb.AppendLine("    mathematical property of the function alone.");
            sb.AppendLine();
            sb.AppendLine("    This is consistent with TQM-066 (no universal");
            sb.AppendLine("    coupling regime) and TQM-068 (curvature doesn't");
            sb.AppendLine("    drive motion). Attraction appears to be an emergent");
            sb.AppendLine("    property of the coupled phase-position system,");
            sb.AppendLine("    not derivable from the coupling function in isolation.");
        }
        else
        {
            sb.AppendLine($"    The property '{report.TopPredictor}' is the");
            sb.AppendLine($"    dominant predictor of spatial attraction");
            sb.AppendLine($"    (r = {report.BestSinglePredictorR:F4}).");
            sb.AppendLine($"    This property captures the essential mathematical");
            sb.AppendLine($"    feature that causes condensates to move toward");
            sb.AppendLine($"    each other under phase-gradient forces.");
        }
        sb.AppendLine();

        // ── Section 9: Conclusion ────────────────────────────────────
        Sec(sb, "9. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Top predictor: {report.TopPredictor} (r = {report.TopPredictorR:F4})");
        sb.AppendLine($"  C3. Best single-metric r: {report.BestSinglePredictorR:F4}");
        sb.AppendLine($"  C4. Functions tested: {CouplingInformationAnalyzer.NamedFunctions.Count} named + {RandomFuncCount} random = {allResults.Count / SeedsPerFunc} unique");
        sb.AppendLine($"  C5. Total simulation runs: {allResults.Count}");
        sb.AppendLine();

        // Top 3 features.
        sb.AppendLine("  Top 3 predictors:");
        for (int i = 0; i < Math.Min(3, report.Rankings.Count); i++)
            sb.AppendLine($"    {i + 1}. {report.Rankings[i].FeatureName} (r = {report.Rankings[i].PearsonR:F4})");
        sb.AppendLine();

        string finalVerdict = report.Classification switch
        {
            "A: No Predictor" =>
                "Attraction cannot be predicted from static coupling-function " +
                "properties. It is an emergent dynamical phenomenon, not a " +
                "static property of the coupling law.",
            "B: Weak Predictor" =>
                "A weak predictor exists but explains limited variance. " +
                "Attraction is primarily dynamical, with a small static component.",
            "C: Strong Predictor" =>
                "A strong static predictor exists. Attraction is largely " +
                "determined by the mathematical form of the coupling function.",
            _ => "A universal coupling principle has been identified. " +
                 "Attraction can be predicted from function properties alone."
        };
        sb.AppendLine($"  C6. {finalVerdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-069 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
