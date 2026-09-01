using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_078_CoherenceEvolutionLaw : ResearchTestBase
{
    private const double Lambda = 0.05;
    private const int N = 100;
    private const int BaseSeed = 780427931;
    private const int SeedsPerK = 2;
    private static readonly double[] KValues = { 0.1, 0.2, 0.5, 1, 2, 5, 10 };

    public AT_078_CoherenceEvolutionLaw(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_078_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-078 Coherence Evolution Law");

        sb.AppendLine("AT-078: Does dR/dt Follow a Closed Analytic Equation?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-077: dR/dt is the dominant dynamical term (2.2× gain).");
        sb.AppendLine();
        sb.AppendLine("  Remaining question: What determines dR/dt itself?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: A simple coherence-growth law exists:");
        sb.AppendLine("    dR/dt = f(R, K)");
        sb.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  K sweep: [{string.Join(", ", KValues)}]");
        sb.AppendLine($"  Seeds per K: {SeedsPerK}, N = {N}");
        sb.AppendLine($"  Pure Kuramoto (fixed positions), 500 steps");
        sb.AppendLine($"  Record R(t) every 2 steps");
        sb.AppendLine();
        sb.AppendLine("  Candidate models:");
        sb.AppendLine("    A: dR/dt = a·R         (exponential)");
        sb.AppendLine("    B: dR/dt = a·R(1-R)    (logistic)");
        sb.AppendLine("    C: dR/dt = a·Rⁿ(1-R)   (generalized logistic)");
        sb.AppendLine("    D: dR/dt = a·K·R(1-R)  (K-logistic)");
        sb.AppendLine("    E: dR/dt = a·K·Rⁿ(1-R) (full model)");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var data = CoherenceEvolutionAnalyzer.GenerateEvolutionData(
            KValues, Lambda, N, SeedsPerK, BaseSeed);
        var kReports = CoherenceEvolutionAnalyzer.AnalyzeKSweep(data, KValues);
        sw.Stop();

        int totalPoints = data.Count;
        sb.AppendLine($"  Completed: {totalPoints:N0} data points in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Coherence Evolution Data ──────────────────────
        Sec(sb, "3. Coherence Evolution (sample K=1)");

        var k1Data = data.Where(d => Math.Abs(d.K - 1) < 0.001).Take(30).ToList();
        sb.AppendLine("  t      │ R       │ dR/dt");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var p in k1Data)
            sb.AppendLine($"  {p.Timestep,5:F0} │ {p.R,6:F4} │ {p.dRdt,8:F5}");
        sb.AppendLine();

        // ── Section 4: Model Comparison ──────────────────────────────
        Sec(sb, "4. Model Comparison (K=1)");

        var k1Report = kReports[1.0];
        sb.AppendLine("  Rank │ Model              │ R²      │ RMSE    │ Params");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        int rank = 0;
        foreach (var f in k1Report.Fits)
        {
            rank++;
            string star = rank == 1 ? " \u2605" : "  ";
            sb.AppendLine($"  {rank,3}{star} │ {f.Name,-18} │ {f.R2,6:F4} │ {f.RMSE,6:F4} │ {string.Join(", ", f.Parameters.Select(p => p.ToString("F3")))}");
        }
        sb.AppendLine();
        sb.AppendLine($"  Best: {k1Report.BestModel} (R² = {k1Report.BestR2:F4})");
        sb.AppendLine();

        // ── Section 5: K-Sweep ───────────────────────────────────────
        Sec(sb, "5. Universality Across K");

        sb.AppendLine("  K      │ Best Model        │ R²      │ R²(Logistic) │ a-coeff");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (double k in KValues)
        {
            if (!kReports.TryGetValue(k, out var rep)) continue;
            var logFit = rep.Fits.FirstOrDefault(f => f.Name.Contains("Logistic") && !f.Name.Contains("Gen") && !f.Name.Contains("K-"));
            double logR2 = logFit?.R2 ?? 0;
            double coeff = rep.Fits[0].Parameters.Length > 0 ? rep.Fits[0].Parameters[0] : 0;
            sb.AppendLine($"  {k,5:F1} │ {rep.BestModel,-17} │ {rep.BestR2,6:F4} │ {logR2,11:F4} │ {coeff,8:F4}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Can dR/dt be predicted from R alone?");
        sb.AppendLine($"    Best R² at K=1: {k1Report.BestR2:F4}");
        sb.AppendLine($"    {(k1Report.BestR2 > 0.60 ? "YES — Strong predictability from R" : k1Report.BestR2 > 0.30 ? "PARTIALLY — Moderate predictability" : "NO — R alone insufficient")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: How important is K?");
        var kR2s = KValues.Where(k => kReports.ContainsKey(k))
            .Select(k => (k, kReports[k].BestR2)).ToList();
        double minK = kR2s.Min(x => x.Item2), maxK = kR2s.Max(x => x.Item2);
        sb.AppendLine($"    R² range across K: [{minK:F4}, {maxK:F4}]");
        sb.AppendLine($"    {(maxK - minK > 0.2 ? "K strongly affects predictability" : "Predictability is K-independent")}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does a logistic-like coherence law emerge?");
        var logisticR2s = KValues.Where(k => kReports.ContainsKey(k))
            .Select(k =>
            {
                var lf = kReports[k].Fits.FirstOrDefault(f => f.Name.Contains("Logistic") && !f.Name.Contains("Gen"));
                return (k, lf?.R2 ?? 0);
            }).ToList();
        double meanLogR2 = logisticR2s.Average(x => x.Item2);
        sb.AppendLine($"    Mean logistic R²: {meanLogR2:F4}");
        sb.AppendLine($"    {(meanLogR2 > 0.60 ? "YES — Logistic growth strongly supported" : meanLogR2 > 0.30 ? "PARTIALLY — Logistic growth partially supported" : "NO — Not logistic")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Is the same law valid across K?");
        var bestModels = KValues.Where(k => kReports.ContainsKey(k))
            .Select(k => kReports[k].BestModel).Distinct().ToList();
        sb.AppendLine($"    Unique best models: {bestModels.Count}");
        sb.AppendLine($"    {(bestModels.Count == 1 ? "YES — Universal law across K" : "NO — Law depends on K")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can coherence evolution be analytically predicted?");
        sb.AppendLine($"    Classification: {k1Report.Classification}");
        sb.AppendLine();

        sb.AppendLine("  Q6: Can this close the full chain?");
        sb.AppendLine("    Chain: dR/dt → R → A → F → trajectory");
        sb.AppendLine($"    dR/dt R² = {k1Report.BestR2:F4}");
        double chainR2 = k1Report.BestR2 * 0.942 * 0.9895;
        sb.AppendLine($"    Full chain R² ≈ {chainR2:F4} (estimated)");
        sb.AppendLine($"    {(chainR2 > 0.50 ? "PARTIALLY — Chain is partially closed" : "NO — Gaps remain")}");
        sb.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        Sec(sb, "6. Interpretation");
        sb.AppendLine($"  Classification: {k1Report.Classification}");
        sb.AppendLine($"  Best model: {k1Report.BestModel} (R² = {k1Report.BestR2:F4})");
        sb.AppendLine();
        sb.AppendLine($"  {k1Report.Interpretation}");
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1. Classification: {k1Report.Classification}");
        sb.AppendLine($"  C2. Best model: {k1Report.BestModel} (R² = {k1Report.BestR2:F4})");
        sb.AppendLine($"  C3. Logistic R²: {logisticR2s.FirstOrDefault(x => Math.Abs(x.k - 1) < 0.01).Item2:F4}");
        sb.AppendLine($"  C4. Universal: {(bestModels.Count == 1 ? "YES" : "NO — " + bestModels.Count + " models")}");
        sb.AppendLine($"  C5. Data points: {totalPoints:N0}");
        sb.AppendLine();
        sb.AppendLine($"  C6. {k1Report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-078 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
