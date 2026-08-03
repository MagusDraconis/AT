using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_071_CriticalCoherenceThreshold : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 710517239;
    private const double RMin = 0.70;
    private const double RMax = 1.00;
    private const double RStep = 0.01;
    private const int SeedsPerPoint = 3;
    private static readonly double[] Betas = { 0.0, 0.5, 2.0 };

    public TQM_071_CriticalCoherenceThreshold(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_071_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-071 Critical Coherence Threshold");

        sb.AppendLine("TQM-071: Is There a Universal Critical Coherence for Attraction?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  TQM-070: Attraction appears abruptly near R ≈ 0.85–0.90.");
        sb.AppendLine("  Below this: forces largely cancel.");
        sb.AppendLine("  Above this: net attraction rapidly emerges.");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: A critical coherence threshold R_crit exists.");
        sb.AppendLine("  Attraction emerges as a phase transition when coherence");
        sb.AppendLine("  exceeds R_crit.");
        sb.AppendLine();
        sb.AppendLine("  Method: Prepare states with CONTROLLED coherence via");
        sb.AppendLine("  von Mises phase distributions, then measure instantaneous");
        sb.AppendLine("  spatial attraction force.");
        sb.AppendLine();

        // ── Section 2: Experimental Design ───────────────────────────
        int nTargets = (int)((RMax - RMin) / RStep) + 1;
        Sec(sb, "2. Experimental Design");
        sb.AppendLine($"  R sweep: [{RMin:F2}, {RMax:F2}], step = {RStep:F2} ({nTargets} points)");
        sb.AppendLine($"  Coupling laws: cos(Δθ), cos²(Δθ), exp(-|Δθ|)");
        sb.AppendLine($"  Seeds per R-point: {SeedsPerPoint}");
        sb.AppendLine($"  Total measurements lvl 1: {nTargets * 3 * SeedsPerPoint}");
        sb.AppendLine($"  N = {NPerGroup * 2}, K = {K}, λ = {Lambda}");
        sb.AppendLine($"  Measurement: 30-step instantaneous attraction probe");
        sb.AppendLine();
        sb.AppendLine("  Coherence control: von Mises(μ=0, κ(R)) distribution");
        sb.AppendLine("  κ computed via Newton inversion of R = I₁(κ)/I₀(κ).");
        sb.AppendLine("  Random variates: Best-Fisher (1979) acceptance-rejection.");
        sb.AppendLine();

        // ── Run main sweep ───────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var (profiles, report) = CriticalCoherenceAnalyzer.RunFullCoherenceScan(
            RMin, RMax, RStep, K, Lambda, NPerGroup, SeedsPerPoint, BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Main sweep: {profiles.Count} measurements in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Coherence Scan ────────────────────────────────
        Sec(sb, "3. Coherence Scan Results");

        // Per-R summary.
        var byR = profiles.GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();
        sb.AppendLine("  Target R │ Actual R  │ Attr Prob │ Mean Force │ Mean ΔSep  │ Attracts?");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var g in byR)
        {
            double actR = g.Average(p => p.ActualR);
            double prob = (double)g.Count(p => p.Attracts) / g.Count();
            double mf = g.Average(p => p.AttractionForce);
            double ms = g.Average(p => p.SeparationChange);
            string att = prob > 0.5 ? "\u25BC YES" : prob > 0 ? "\u25B2 partial" : "\u25B2 no";
            sb.AppendLine($"  {g.Key,7:F2} │ {actR,8:F4} │ {prob,8:P0} │ {mf,9:F5} │ {ms,9:F5} │ {att}");
        }
        sb.AppendLine();

        // ── Section 4: Threshold Detection ───────────────────────────
        Sec(sb, "4. Threshold Detection");

        sb.AppendLine($"  Critical R (50% attraction): {(double.IsNaN(report.CriticalR) ? "NOT FOUND" : report.CriticalR.ToString("F4"))}");
        sb.AppendLine($"  Transition width (10%–90%):  {report.TransitionWidth:F4}");
        sb.AppendLine($"  Transition type:             {report.TransitionType}");
        sb.AppendLine($"  Maximum force:               {report.MaximumForce:F5}");
        sb.AppendLine($"  R at max force:              {report.RAtMaxForce:F4}");
        sb.AppendLine();

        // Per-law thresholds.
        sb.AppendLine("  Per-law critical thresholds:");
        sb.AppendLine("  Law         │ R_crit    │ Transition Width");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (law, rCrit) in report.LawThresholds)
        {
            var lawProfs = profiles.Where(p => p.LawName == law && p.Attracts).ToList();
            var lawByR = profiles.Where(p => p.LawName == law)
                .GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();
            var lawP = lawByR.Select(g => (double)g.Count(p => p.Attracts) / g.Count()).ToList();
            int i10l = lawP.FindIndex(p => p >= 0.1);
            int i90l = lawP.FindLastIndex(p => p <= 0.9);
            double wl = i90l > i10l && i10l >= 0
                ? lawByR[Math.Min(i90l + 1, lawByR.Count - 1)].Key - lawByR[i10l].Key : 0;
            sb.AppendLine($"  {law,-10} │ {rCrit,8:F4} │ {wl,8:F4}");
        }
        sb.AppendLine();

        sb.AppendLine($"  Universality: {(report.IsUniversal ? "YES — Same threshold across all laws" : "NO — Threshold depends on coupling law")}");
        sb.AppendLine();

        // ── Section 5: Universality Analysis (β-sweep) ───────────────
        Sec(sb, "5. β-Dependence Analysis");

        // Run quick β-sweep for cos law only.
        var sw2 = System.Diagnostics.Stopwatch.StartNew();
        var (betaProfs, betaReport) = CriticalCoherenceAnalyzer.RunBetaSweep(
            0.75, 1.00, 0.05, Betas, "cos", K, Lambda, NPerGroup, 2, BaseSeed + 500000);
        sw2.Stop();

        sb.AppendLine($"  β sweep: {Betas.Length} β values × {betaProfs.Count / (Betas.Length * 2)} R-points");
        sb.AppendLine($"  Completed in {sw2.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // Per-beta critical thresholds.
        sb.AppendLine("  β      │ R_crit    │ Attr at R=0.90 │ Attr at R=0.95");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (double beta in Betas)
        {
            var sub = betaProfs.Where(p => Math.Abs(p.Beta - beta) < 0.01).ToList();
            var subByR = sub.GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();
            var subP = subByR.Select(g =>
                (double)g.Count(p => p.Attracts) / g.Count()).ToList();
            var subR = subByR.Select(g => g.Key).ToList();

            // Find R_crit for this beta.
            double rCritB = double.NaN;
            for (int i = 1; i < subP.Count; i++)
            {
                if (subP[i - 1] < 0.5 && subP[i] >= 0.5)
                {
                    double t = (0.5 - subP[i - 1]) / Math.Max(subP[i] - subP[i - 1], 1e-10);
                    rCritB = subR[i - 1] + t * (subR[i] - subR[i - 1]);
                    break;
                }
            }

            double at90 = sub.Where(p => Math.Abs(p.TargetR - 0.90) < 0.01).ToList()
                .Let(l => l.Count > 0 ? (double)l.Count(p => p.Attracts) / l.Count : 0);
            double at95 = sub.Where(p => Math.Abs(p.TargetR - 0.95) < 0.01).ToList()
                .Let(l => l.Count > 0 ? (double)l.Count(p => p.Attracts) / l.Count : 0);

            sb.AppendLine($"  {beta,5:F1} │ {rCritB,8:F4} │ {at90,14:P0} │ {at95,14:P0}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Does a sharp R_crit exist?");
        sb.AppendLine($"    {(double.IsNaN(report.CriticalR) ? "NO — No sharp threshold detected" : $"YES — R_crit ≈ {report.CriticalR:F4}")}");
        sb.AppendLine($"    Transition width: {report.TransitionWidth:F4}");
        sb.AppendLine($"    {(report.TransitionWidth < 0.1 ? "SHARP — Abrupt onset of attraction" : "GRADUAL — Continuous increase")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: Is R_crit independent of coupling law?");
        var thresholds = report.LawThresholds.Values.Where(v => !double.IsNaN(v)).ToList();
        if (thresholds.Count >= 2)
            sb.AppendLine($"    Range: [{thresholds.Min():F4}, {thresholds.Max():F4}]");
        sb.AppendLine($"    {(report.IsUniversal ? "YES — Universal threshold across laws" : "NO — Threshold varies by law")}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Is R_crit independent of β?");
        var betaThresholds = Betas.Select(b =>
        {
            var sub = betaProfs.Where(p => Math.Abs(p.Beta - b) < 0.01).ToList();
            var sbr = sub.GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();
            var sp = sbr.Select(g => (double)g.Count(p => p.Attracts) / g.Count()).ToList();
            var sr = sbr.Select(g => g.Key).ToList();
            double rc = double.NaN;
            for (int i = 1; i < sp.Count; i++)
                if (sp[i - 1] < 0.5 && sp[i] >= 0.5)
                {
                    double t = (0.5 - sp[i - 1]) / Math.Max(sp[i] - sp[i - 1], 1e-10);
                    rc = sr[i - 1] + t * (sr[i] - sr[i - 1]); break;
                }
            return rc;
        }).Where(v => !double.IsNaN(v)).ToList();

        if (betaThresholds.Count >= 2)
        {
            double bRange = betaThresholds.Max() - betaThresholds.Min();
            sb.AppendLine($"    β-threshold range: {bRange:F4}");
            sb.AppendLine($"    {(bRange < 0.05 ? "YES — Threshold is β-independent" : "NO — Threshold depends on β")}");
        }
        sb.AppendLine();

        sb.AppendLine("  Q4: Is the transition continuous or discontinuous?");
        sb.AppendLine($"    Transition type: {report.TransitionType}");
        sb.AppendLine($"    {(report.TransitionType.Contains("Discontinuous") ? "DISCONTINUOUS — Jump in attraction at R_crit" : "CONTINUOUS — Smooth onset")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can attraction be predicted purely from R?");
        // Check R² of linear regression of attraction probability vs R.
        if (byR.Count >= 2)
        {
            var rList = byR.Select(g => g.Key).ToList();
            var pList = byR.Select(g => (double)g.Count(p => p.Attracts) / g.Count()).ToList();
            double rMean = rList.Average(), pMean = pList.Average();
            double cov = 0, vR = 0;
            for (int i = 0; i < rList.Count; i++)
            { cov += (rList[i] - rMean) * (pList[i] - pMean); vR += (rList[i] - rMean) * (rList[i] - rMean); }
            double slope = cov / Math.Max(vR, 1e-15);
            double intercept = pMean - slope * rMean;
            double ssRes = 0, ssTot = 0;
            for (int i = 0; i < rList.Count; i++)
            { double pred = slope * rList[i] + intercept; ssRes += (pList[i] - pred) * (pList[i] - pred); ssTot += (pList[i] - pMean) * (pList[i] - pMean); }
            double r2 = 1.0 - ssRes / Math.Max(ssTot, 1e-15);
            sb.AppendLine($"    R²(attraction ~ R) = {r2:F4}");
            sb.AppendLine($"    {(r2 > 0.8 ? "YES — R strongly predicts attraction" : r2 > 0.5 ? "PARTIALLY — R predicts attraction moderately" : "NO — R alone insufficient")}");
        }
        sb.AppendLine();

        sb.AppendLine("  Q6: Is coherence the true order parameter?");
        sb.AppendLine($"    {(report.Classification.StartsWith("D:") ? "YES — Coherence is the order parameter for spatial attraction" : "PARTIALLY — Coherence is necessary but not sufficient")}");
        sb.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        Sec(sb, "6. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Critical R: {(double.IsNaN(report.CriticalR) ? "N/A" : report.CriticalR.ToString("F4"))}");
        sb.AppendLine($"  Transition width: {report.TransitionWidth:F4}");
        sb.AppendLine($"  Transition type: {report.TransitionType}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  Physical interpretation:");
        if (!double.IsNaN(report.CriticalR))
        {
            sb.AppendLine($"    At R < {report.CriticalR:F3}: oscillator phases are too dispersed.");
            sb.AppendLine("    Coupling forces point in random directions and cancel.");
            sb.AppendLine($"    At R > {report.CriticalR:F3}: phase alignment is sufficient for");
            sb.AppendLine("    coherent spatial forces to emerge. The system undergoes a");
            sb.AppendLine($"    {report.TransitionType.ToLower()} in its spatial dynamics.");
        }
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. R_crit: {(double.IsNaN(report.CriticalR) ? "N/A" : report.CriticalR.ToString("F4"))}");
        sb.AppendLine($"  C3. Transition width: {report.TransitionWidth:F4}");
        sb.AppendLine($"  C4. Transition type: {report.TransitionType}");
        sb.AppendLine($"  C5. Universality: {(report.IsUniversal ? "YES" : "NO")}");
        sb.AppendLine($"  C6. Total measurements: {profiles.Count}");
        sb.AppendLine();

        // Per-law summary.
        sb.AppendLine("  Per-law R_crit values:");
        foreach (var (law, rc) in report.LawThresholds.OrderBy(kv => kv.Value))
        {
            string rcStr = double.IsNaN(rc) ? "N/A" : rc.ToString("F4");
            sb.AppendLine($"    {law}: R_crit = {rcStr}");
        }
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-071 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}

file static class ListExtensions
{
    public static TOut Let<T, TOut>(this List<T> list, Func<List<T>, TOut> fn) => fn(list);
}
