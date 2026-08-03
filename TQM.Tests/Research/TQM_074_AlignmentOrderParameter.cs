using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_074_AlignmentOrderParameter : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 740819653;
    private const double RMin = 0.0;
    private const double RMax = 1.0;
    private const double RStep = 0.02;
    private const int SeedsPerPoint = 2;
    private static readonly string[] Laws = { "cos", "cos²", "exp(-|x|)", "1/(1+|x|)" };

    public TQM_074_AlignmentOrderParameter(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_074_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-074 Alignment Order Parameter");

        sb.AppendLine("TQM-074: Is Alignment the True Order Parameter for Attraction?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  TQM-072: F_net almost perfectly predicted by alignment (r=0.986).");
        sb.AppendLine("  TQM-073: F_net fit by empirical functions of R, but no universal law.");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: Coherence does not directly generate force.");
        sb.AppendLine("  Instead: Coherence → Alignment → Net Force.");
        sb.AppendLine("  Alignment is the TRUE order parameter.");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  R sweep: [{RMin:F2}, {RMax:F2}], step={RStep:F2}");
        sb.AppendLine($"  Laws: {string.Join(", ", Laws)}");
        sb.AppendLine($"  Seeds: {SeedsPerPoint}, N={NPerGroup * 2}");
        sb.AppendLine();
        sb.AppendLine("  Models compared:");
        sb.AppendLine("    A: F = ΣaᵢRⁱ (Coherence Poly5)");
        sb.AppendLine("    B: F = a · A  (Alignment Direct)");
        sb.AppendLine("    C: F = a · A · ⟨f⟩  (Align × MeanForce)");
        sb.AppendLine("    D: F = a · A · Area⁺  (Align × PositiveArea)");
        sb.AppendLine("    E: F = a₀ + a₁·A  (Alignment + offset)");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var profiles = AlignmentOrderParameterAnalyzer.GenerateProfiles(
            RMin, RMax, RStep, Laws, K, Lambda, NPerGroup, SeedsPerPoint, BaseSeed);
        var report = AlignmentOrderParameterAnalyzer.Analyze(profiles);
        var perLaw = AlignmentOrderParameterAnalyzer.AnalyzePerLaw(profiles);
        sw.Stop();

        sb.AppendLine($"  Completed {profiles.Count} profiles in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Alignment Measurements ────────────────────────
        Sec(sb, "3. Alignment vs Coherence");

        // Per-R averages.
        var byR = profiles.GroupBy(p => Math.Round(p.R, 2)).OrderBy(g => g.Key).ToList();
        sb.AppendLine("  R      │ Alignment │ NetForce │ ⟨f⟩      │ Canc Ratio");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var g in byR.Take(12))
            sb.AppendLine($"  {g.Key,5:F2} │ {g.Average(p => p.Alignment),8:F4} │ {g.Average(p => p.NetForce),7:F4} │ {g.Average(p => p.MeanLocalForce),7:F4} │ {g.Average(p => p.Cancellation),8:F4}");
        sb.AppendLine($"  ... ({byR.Count} R-levels) ...");
        sb.AppendLine();

        sb.AppendLine("  Observations:");
        double lowAlign = byR.First().Average(p => p.Alignment);
        double highAlign = byR.Last().Average(p => p.Alignment);
        sb.AppendLine($"    Alignment grows from {lowAlign:F4} (R=0) to {highAlign:F4} (R=1)");
        sb.AppendLine();

        // ── Section 4: Model Comparison ──────────────────────────────
        Sec(sb, "4. Model Comparison (All Laws Combined)");

        sb.AppendLine("  Rank │ Model                │ R²      │ RMSE    │ ΔR² vs R  │ AIC      │ Params");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        double baseR2 = report.Comparisons.First(c => c.ModelName.Contains("Coherence")).R2;
        int rank = 0;
        foreach (var c in report.Comparisons)
        {
            rank++;
            string star = rank == 1 ? " \u2605" : "  ";
            double delta = c.R2 - baseR2;
            string ds = delta >= 0 ? $"+{delta:F4}" : $"{delta:F4}";
            sb.AppendLine($"  {rank,3}{star} │ {c.ModelName,-20} │ {c.R2,6:F4} │ {c.RMSE,6:F4} │ {ds,8} │ {c.AIC,8:F1} │ {c.ParamCount}");
        }
        sb.AppendLine();

        sb.AppendLine($"  Best model: {report.BestModel} (R² = {report.BestR2:F4})");
        sb.AppendLine($"  Improvement over coherence: ΔR² = {report.Improvement:+0.0000}");
        sb.AppendLine();

        // Show coefficients of best model.
        var best = report.Comparisons[0];
        sb.AppendLine($"  Best model coefficients: [{string.Join(", ", best.Coefficients.Select(p => p.ToString("F4")))}]");
        sb.AppendLine();

        // ── Section 5: Force Reconstruction ──────────────────────────
        Sec(sb, "5. Force Reconstruction from Alignment");

        // Compare F_obs vs F_pred for alignment model.
        var alignModel = report.Comparisons.First(c => c.ModelName == "Alignment Direct");
        double aCoeff = alignModel.Coefficients[0];
        sb.AppendLine($"  Alignment model: F_pred = {aCoeff:F4} · Alignment");
        sb.AppendLine();

        // Show sample predictions.
        sb.AppendLine("  R      │ F_obs    │ F_pred   │ Residual │ A·⟨f⟩    │ A·Area⁺");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        var sample = profiles.Where((_, i) => i % 10 == 0).Take(10).ToList();
        foreach (var p in sample)
        {
            double pred = aCoeff * p.Alignment;
            double res = p.NetForce - pred;
            double amf = p.Alignment * p.MeanLocalForce;
            double apa = p.Alignment * p.PositiveArea;
            sb.AppendLine($"  {p.R,5:F2} │ {p.NetForce,7:F4} │ {pred,7:F4} │ {res,7:F4} │ {amf,7:F4} │ {apa,7:F4}");
        }
        sb.AppendLine();

        // ── Section 6: Universality Analysis ─────────────────────────
        Sec(sb, "6. Universality Across Coupling Laws");

        sb.AppendLine("  Law          │ Best Model          │ R²      │ Align R² │ Δ vs Coh");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var law in Laws)
        {
            var lr = perLaw[law];
            var lBest = lr.Comparisons[0];
            var lCoh = lr.Comparisons.First(c => c.ModelName.Contains("Coherence"));
            var lAlign = lr.Comparisons.First(c => c.ModelName == "Alignment Direct");
            double delta = lBest.R2 - lCoh.R2;
            string ds = delta >= 0 ? $"+{delta:F4}" : $"{delta:F4}";
            sb.AppendLine($"  {law,-11} │ {lBest.ModelName,-19} │ {lBest.R2,6:F4} │ {lAlign.R2,7:F4} │ {ds,7}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        var cohR2Al = report.Comparisons.First(c => c.ModelName.Contains("Coherence")).R2;
        var alignR2 = report.Comparisons.First(c => c.ModelName == "Alignment Direct").R2;

        sb.AppendLine("  Q1: Is alignment better than coherence for predicting F_net?");
        sb.AppendLine($"    Coherence R²: {cohR2Al:F4}");
        sb.AppendLine($"    Alignment R²: {alignR2:F4}");
        sb.AppendLine($"    ΔR²: {report.Improvement:+0.0000}");
        sb.AppendLine($"    {(report.Improvement > 0.05 ? "YES — Alignment is significantly better" : report.Improvement > 0 ? "WEAKLY — Alignment is marginally better" : "NO — Coherence is better")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: Can force be reconstructed from alignment alone?");
        sb.AppendLine($"    R²: {alignR2:F4}");
        sb.AppendLine($"    {(alignR2 > 0.90 ? "YES — Near-perfect reconstruction from alignment" : alignR2 > 0.70 ? "PARTIALLY — Good reconstruction" : "NO — Alignment alone insufficient")}");
        sb.AppendLine();

        // Check if alignment-vs-R is monotonic.
        sb.AppendLine("  Q3: Is coherence merely a precursor to alignment?");
        double r_RA = Pearson(profiles.Select(p => p.R).ToList(),
                              profiles.Select(p => p.Alignment).ToList());
        sb.AppendLine($"    r(R, Alignment) = {r_RA:F4}");
        sb.AppendLine($"    {(r_RA > 0.9 ? "YES — Coherence strongly drives alignment (causal chain confirmed)" : "NO — Coherence and alignment are not strongly coupled")}");
        sb.AppendLine();

        // Check alignment model universality.
        var alignR2s = Laws.Select(law =>
        {
            var lr = perLaw[law];
            var la = lr.Comparisons.First(c => c.ModelName == "Alignment Direct");
            return (law, la.R2);
        }).ToList();
        sb.AppendLine("  Q4: Does alignment unify all coupling laws?");
        sb.AppendLine("    Alignment R² per law:");
        foreach (var (law, r2) in alignR2s)
            sb.AppendLine($"      {law}: R² = {r2:F4}");
        double minAlignR2 = alignR2s.Min(x => x.R2);
        sb.AppendLine($"    {(minAlignR2 > 0.85 ? "YES — Alignment unifies all laws" : minAlignR2 > 0.60 ? "PARTIALLY — Alignment works for most laws" : "NO — Alignment is law-dependent")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can a universal attraction equation be derived from alignment?");
        sb.AppendLine($"    Best model: {report.BestModel} (R² = {report.BestR2:F4})");
        sb.AppendLine($"    {(report.BestR2 > 0.95 ? "YES — F = a·A is a universal attraction equation" : report.BestR2 > 0.85 ? "PARTIALLY — Strong but not universal" : "NO — No simple universal equation")}");
        sb.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        Sec(sb, "7. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  Causal chain analysis:");
        sb.AppendLine($"    R → Alignment: r = {r_RA:F4}");
        sb.AppendLine($"    Alignment → F_net: R² = {alignR2:F4}");
        sb.AppendLine($"    R → F_net (direct): R² = {cohR2Al:F4}");
        sb.AppendLine();

        if (alignR2 > cohR2Al)
        {
            sb.AppendLine($"    Alignment MEDIATES the R → F_net relationship.");
            sb.AppendLine($"    F_net(R) ≈ F_net(Alignment(R)). The indirect path");
            sb.AppendLine($"    R → Alignment → F_net explains MORE variance than");
            sb.AppendLine($"    the direct path R → F_net.");
            sb.AppendLine();
            sb.AppendLine("    Physical picture:");
            sb.AppendLine("    Coherence does not create force. Coherence creates");
            sb.AppendLine("    ALIGNMENT of local force vectors. Alignment then");
            sb.AppendLine("    SUMS coherently into net macroscopic force.");
            sb.AppendLine();
            sb.AppendLine("    This is the mediation hypothesis confirmed:");
            sb.AppendLine("    Coherence → (force alignment) → Net Attraction.");
        }
        else
        {
            sb.AppendLine("    Alignment does not mediate the R → F_net relationship.");
            sb.AppendLine("    Both variables carry independent predictive power.");
        }
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Best model: {report.BestModel} (R² = {report.BestR2:F4})");
        sb.AppendLine($"  C3. Alignment R²: {alignR2:F4}");
        sb.AppendLine($"  C4. Coherence R²: {cohR2Al:F4}");
        sb.AppendLine($"  C5. Improvement: ΔR² = {report.Improvement:+0.0000}");
        sb.AppendLine($"  C6. r(R, Alignment): {r_RA:F4}");
        sb.AppendLine($"  C7. Total profiles: {profiles.Count}");
        sb.AppendLine();

        sb.AppendLine($"  C8. {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-074 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static double Pearson(List<double> x, List<double> y)
    {
        double mx = x.Average(), my = y.Average();
        double cov = 0, vx = 0, vy = 0;
        for (int i = 0; i < x.Count; i++)
        { double dx = x[i] - mx, dy = y[i] - my; cov += dx * dy; vx += dx * dx; vy += dy * dy; }
        return cov / Math.Sqrt(Math.Max(vx, 1e-15) * Math.Max(vy, 1e-15));
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
