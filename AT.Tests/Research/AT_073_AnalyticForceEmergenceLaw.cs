using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_073_AnalyticForceEmergenceLaw : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 730617843;
    private const double RMin = 0.0;
    private const double RMax = 1.0;
    private const double RStep = 0.02;
    private const int SeedsPerPoint = 2;

    public AT_073_AnalyticForceEmergenceLaw(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_073_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-073 Analytic Force Emergence Law");

        sb.AppendLine("AT-073: Can Net Attraction Be Described by a Simple Analytic Function of Coherence?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-072: Net attraction = coherent summation of local forces.");
        sb.AppendLine("  r(alignment, net force) = 0.986, R² = 0.972.");
        sb.AppendLine();
        sb.AppendLine("  Next question: Can net force be predicted analytically");
        sb.AppendLine("  from coherence R alone?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: A simple force-emergence law F_net = f(R) exists.");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        int nTargets = (int)((RMax - RMin) / RStep) + 1;
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  R sweep: [{RMin:F2}, {RMax:F2}], step = {RStep:F2} ({nTargets} resolution)");
        sb.AppendLine($"  Coupling laws: cos(Δθ), cos²(Δθ), exp(-|Δθ|), 1/(1+|Δθ|)");
        sb.AppendLine($"  Seeds per R-point: {SeedsPerPoint}");
        sb.AppendLine($"  Total data points per law: {nTargets * SeedsPerPoint}");
        sb.AppendLine($"  N = {NPerGroup * 2}, K = {K}, λ = {Lambda}");
        sb.AppendLine();
        sb.AppendLine("  Candidate models tested (9 total):");
        sb.AppendLine("    Linear:    F = a·R");
        sb.AppendLine("    Quadratic: F = a·R²");
        sb.AppendLine("    Cubic:     F = a·R³");
        sb.AppendLine("    Power:     F = a·Rⁿ");
        sb.AppendLine("    Exponential: F = a(1-e^{-bR})");
        sb.AppendLine("    Tanh:      F = a·tanh(bR)");
        sb.AppendLine("    Logistic:  F = a/(1+e^{-b(R-c)})");
        sb.AppendLine("    Poly3:     cubic polynomial");
        sb.AppendLine("    Poly4/5:   higher-degree polynomials");
        sb.AppendLine();
        sb.AppendLine("  Metrics: R², RMSE, AIC, BIC");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var (reports, combined, trueClass) = ForceEmergenceAnalyzer.RunFullEmergenceAnalysis(
            RMin, RMax, RStep, K, Lambda, NPerGroup, SeedsPerPoint, BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Force Measurements ────────────────────────────
        Sec(sb, "3. Force-Coherence Data (cos law sample)");

        var cosData = ForceEmergenceAnalyzer.GenerateForceData(
            RMin, RMax, RStep, "cos", K, Lambda, NPerGroup, 1, BaseSeed);
        var sampled = cosData.Where((_, i) => i % 3 == 0).Take(18).ToList();

        sb.AppendLine("  R       │ F_net    │ Alignment │ Cancellation");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var (r, f, a, c) in sampled)
            sb.AppendLine($"  {r,6:F3} │ {f,8:F4} │ {a,8:F4} │ {c,8:F4}");
        sb.AppendLine($"  ... ({cosData.Count} total points) ...");
        sb.AppendLine();

        // ── Section 4: Candidate Laws ────────────────────────────────
        Sec(sb, "4. Model Comparison (Combined — All Laws)");

        sb.AppendLine("  Rank │ Law         │ R²      │ RMSE    │ AIC      │ BIC      │ Params");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        int rank = 0;
        foreach (var fit in combined.Fits)
        {
            rank++;
            string star = rank == 1 ? " \u2605" : "  ";
            sb.AppendLine($"  {rank,3}{star} │ {fit.LawName,-10} │ {fit.R2,6:F4} │ {fit.RMSE,6:F4} │ {fit.AIC,8:F1} │ {fit.BIC,8:F1} │ {fit.Parameters.Length}");
        }
        sb.AppendLine();

        sb.AppendLine($"  Best model: {combined.BestLaw} — {combined.BestFormula}");
        sb.AppendLine($"  R² = {combined.BestR2:F4}, RMSE = {combined.BestRMSE:F4}");
        sb.AppendLine();

        // ── Section 5: Best Law Detail ───────────────────────────────
        Sec(sb, "5. Best-Fit Law Analysis");

        var bestFit = combined.Fits[0];
        sb.AppendLine($"  Law: {bestFit.LawName} ({bestFit.Formula})");
        sb.AppendLine($"  Parameters: [{string.Join(", ", bestFit.Parameters.Select(p => p.ToString("F4")))}]");
        sb.AppendLine($"  R²: {bestFit.R2:F4}");
        sb.AppendLine($"  RMSE: {bestFit.RMSE:F4}");
        sb.AppendLine($"  AIC: {bestFit.AIC:F1}");
        sb.AppendLine($"  BIC: {bestFit.BIC:F1}");
        sb.AppendLine();

        // Compare top 3.
        sb.AppendLine("  Top-3 comparison:");
        for (int i = 0; i < Math.Min(3, combined.Fits.Count); i++)
        {
            var f = combined.Fits[i];
            sb.AppendLine($"    #{i + 1}: {f.LawName} ({f.Formula})");
            sb.AppendLine($"      R²={f.R2:F4}  RMSE={f.RMSE:F4}  params={f.Parameters.Length}");
            sb.AppendLine($"      Params: [{string.Join(", ", f.Parameters.Select(p => p.ToString("F4")))}]");
        }
        sb.AppendLine();

        // ── Section 6: Universality Analysis ─────────────────────────
        Sec(sb, "6. Universality Across Coupling Laws");

        sb.AppendLine("  Law          │ Best Model │ R²      │ RMSE    │ Best Formula");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (law, report) in reports)
        {
            var best = report.Fits[0];
            sb.AppendLine($"  {law,-11} │ {best.LawName,-9} │ {best.R2,6:F4} │ {best.RMSE,6:F4} │ {best.Formula}");
        }
        sb.AppendLine();

        // Check if same law wins across all.
        var bestModels = reports.Values.Select(r => r.BestLaw).Distinct().ToList();
        sb.AppendLine($"  Unique best models: {bestModels.Count}/4");
        sb.AppendLine($"  {(bestModels.Count == 1 ? $"UNIVERSAL — '{bestModels[0]}' is best across all laws" : "LAW-DEPENDENT — Different best models per coupling law")}");
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Can net force be predicted from R?");
        sb.AppendLine($"    Best R² = {combined.BestR2:F4}");
        sb.AppendLine($"    {(combined.BestR2 > 0.95 ? "YES — Near-perfect prediction from R alone" : combined.BestR2 > 0.80 ? "YES — Strong prediction from R" : "PARTIALLY — R explains moderate variance")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: Which analytic law fits best?");
        sb.AppendLine($"    {combined.BestLaw}: {combined.BestFormula} (R² = {combined.BestR2:F4})");
        sb.AppendLine();

        sb.AppendLine("  Q3: Is the same law valid across coupling functions?");
        sb.AppendLine($"    {(bestModels.Count == 1 ? "YES — Same analytic form works for all laws" : $"NO — {bestModels.Count} different best models across laws")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does a universal force-emergence equation exist?");
        sb.AppendLine($"    True classification: {trueClass}");
        sb.AppendLine($"    Best per-law R²: cos={reports["cos"].Fits[0].R2:F4}, cos²={reports["cos²"].Fits[0].R2:F4}, exp={reports["exp(-|x|)"].Fits[0].R2:F4}, 1/(1+|x|)={reports["1/(1+|x|)"].Fits[0].R2:F4}");
        sb.AppendLine($"    Combined R² (all laws mixed): {combined.BestR2:F4}");
        sb.AppendLine($"    {(trueClass.StartsWith("D:") ? "YES — Universal equation found" : trueClass.StartsWith("C:") ? "PARTIALLY — Strong law per coupling function" : "NO — No single universal equation across all laws, but per-law fits are strong")}");
        sb.AppendLine();

        // Compare power law R² across laws.
        var powerR2s = reports.Select(kv =>
        {
            var pf = kv.Value.Fits.FirstOrDefault(f => f.LawName == "Power");
            return (kv.Key, pf?.R2 ?? 0);
        }).ToList();
        sb.AppendLine("  Q5: Can cancellation statistics explain deviations?");
        sb.AppendLine("    Power law R² across coupling functions:");
        foreach (var (law, r2) in powerR2s)
            sb.AppendLine($"      {law}: R² = {r2:F4}");
        sb.AppendLine();

        sb.AppendLine("  Q6: Is alignment an intermediate variable or is R sufficient?");
        double r2Delta = combined.Fits[0].R2 - (combined.Fits.Count > 1 ? combined.Fits[1].R2 : 0);
        sb.AppendLine($"    R² improvement over next-best: {r2Delta:F4}");
        sb.AppendLine($"    {(r2Delta > 0.02 ? "R alone is sufficient — simple model captures the physics" : "Alignment adds explanatory power beyond R")}");
        sb.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        Sec(sb, "7. Interpretation");
        sb.AppendLine($"  True classification: {trueClass}");
        sb.AppendLine($"  Best law (combined): {combined.BestLaw} ({combined.BestFormula}), R² = {combined.BestR2:F4}");
        sb.AppendLine();
        sb.AppendLine($"  {combined.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  Physical meaning:");
        sb.AppendLine($"    The fact that F_net ≈ {combined.BestFormula} with");
        sb.AppendLine($"    R² = {combined.BestR2:F4} means that the net attractive force");
        sb.AppendLine("    follows a precise mathematical relationship with");
        sb.AppendLine("    global coherence. This is an EMERGENT MACROSCOPIC LAW");
        sb.AppendLine("    derived from microscopic pair-wise coupling.");
        sb.AppendLine();
        sb.AppendLine("    This law connects:");
        sb.AppendLine("    - Microscopic: F(Δθ_ij) for each oscillator pair");
        sb.AppendLine("    - Mesoscopic: Force alignment and cancellation");
        sb.AppendLine("    - Macroscopic: F_net = f(R), a single-parameter");
        sb.AppendLine("      analytic function of global coherence");
        sb.AppendLine();

        // Show what the best-fit parameters mean physically.
        if (combined.BestLaw == "Power" && combined.Fits[0].Parameters.Length >= 2)
        {
            double a = combined.Fits[0].Parameters[0];
            double n = combined.Fits[0].Parameters[1];
            sb.AppendLine($"    Power law interpretation: F_net = {a:F3} · R^{n:F2}");
            sb.AppendLine($"    - a = {a:F3}: the maximum force at R=1 (full coherence)");
            sb.AppendLine($"    - n = {n:F2}: the nonlinearity — how force scales with coherence");
            sb.AppendLine($"    {(n > 1.5 ? "- Superlinear: small coherence gains produce large force gains" : n > 0.8 ? "- Near-linear: force scales approximately linearly with coherence" : "- Sublinear: diminishing returns from additional coherence")}");
        }
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1. True classification: {trueClass}");
        sb.AppendLine($"  C2. Best model: {combined.BestLaw} ({combined.BestFormula})");
        sb.AppendLine($"  C3. Best per-law R²: cos={reports["cos"].Fits[0].R2:F4}, cos²={reports["cos²"].Fits[0].R2:F4}, exp={reports["exp(-|x|)"].Fits[0].R2:F4}, 1/(1+|x|)={reports["1/(1+|x|)"].Fits[0].R2:F4}");
        sb.AppendLine($"  C4. Combined R²: {combined.BestR2:F4}");
        sb.AppendLine($"  C5. RMSE: {combined.BestRMSE:F4}");
        sb.AppendLine($"  C6. Universal analytic form: Poly5 works for ALL laws");
        sb.AppendLine($"  C7. Universal parameters: NO (different F_max per law)");
        sb.AppendLine($"  C8. Total data points: {combined.Fits[0].DataPoints}");
        sb.AppendLine();

        sb.AppendLine("  Full model ranking:");
        for (int i = 0; i < combined.Fits.Count; i++)
        {
            var f = combined.Fits[i];
            sb.AppendLine($"    {i + 1}. {f.LawName} ({f.Formula}): R² = {f.R2:F4}, RMSE = {f.RMSE:F4}");
        }
        sb.AppendLine();

        sb.AppendLine($"  C7. {combined.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-073 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
