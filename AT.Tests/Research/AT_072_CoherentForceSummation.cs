using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_072_CoherentForceSummation : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 720615439;
    private const double RMin = 0.0;
    private const double RMax = 1.0;
    private const double RStep = 0.1;
    private const int SeedsPerPoint = 2;

    public AT_072_CoherentForceSummation(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_072_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-072 Coherent Force Summation");

        sb.AppendLine("AT-072: Does Net Attraction Emerge from Coherent Summation of Local Forces?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-071: Attraction exists at all R, but grows with coherence.");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: At low coherence, local force vectors CANCEL.");
        sb.AppendLine("  At high coherence, local force vectors ALIGN.");
        sb.AppendLine("  Net attraction emerges from coherent vector summation.");
        sb.AppendLine();

        // ── Section 2: Experimental Design ───────────────────────────
        int nTargets = (int)((RMax - RMin) / RStep) + 1;
        Sec(sb, "2. Experimental Design");
        sb.AppendLine($"  R sweep: [{RMin:F1}, {RMax:F1}], step = {RStep:F1} ({nTargets} points)");
        sb.AppendLine($"  Coupling laws: cos(Δθ), sin(Δθ), cos²(Δθ), exp(-|Δθ|)");
        sb.AppendLine($"  Seeds per R-point: {SeedsPerPoint}");
        sb.AppendLine($"  Total measurements: {nTargets * 4 * SeedsPerPoint}");
        sb.AppendLine($"  N = {NPerGroup * 2}, K = {K}, λ = {Lambda}");
        sb.AppendLine($"  Pairs analyzed per state: {NPerGroup * NPerGroup} = {NPerGroup * NPerGroup:N0}");
        sb.AppendLine();
        sb.AppendLine("  For each state, compute ALL pair-wise local forces,");
        sb.AppendLine("  then analyze vector alignment, cancellation, and net force.");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var (profiles, report) = ForceSummationAnalyzer.RunFullForceAnalysis(
            RMin, RMax, RStep, K, Lambda, NPerGroup, SeedsPerPoint, BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed {profiles.Count} force profiles in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine($"  Total pair forces analyzed: {profiles.Sum(p => p.TotalPairs):N0}");
        sb.AppendLine();

        // ── Section 3: Local Force Analysis ──────────────────────────
        Sec(sb, "3. Local Force Analysis (per R, averaged over laws)");

        var byR = profiles.GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();
        sb.AppendLine("  R      │ Align   │ CancRatio│ NetForce │ Σ|f|     │ Mean|f| │ AttrFrac│ Aligned%");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var g in byR)
        {
            double align = g.Average(p => p.AlignmentScore);
            double canc = g.Average(p => p.CancellationRatio);
            double nf = g.Average(p => p.NetForceMagnitude);
            double sumM = g.Average(p => p.SumPairMagnitudes);
            double meanM = g.Average(p => p.MeanPairMagnitude);
            double attrF = g.Average(p => p.AttractiveFraction);
            double alFrac = g.Average(p => p.AlignedFraction);
            sb.AppendLine($"  {g.Key,5:F1} │ {align,6:F3} │ {canc,8:F5} │ {nf,7:F4} │ {sumM,8:F4} │ {meanM,7:F4} │ {attrF,7:P0} │ {alFrac,7:P0}");
        }
        sb.AppendLine();

        // ── Section 4: Force Alignment ───────────────────────────────
        Sec(sb, "4. Force Alignment Analysis");

        sb.AppendLine($"  Alignment-R correlation:      r = {report.AlignmentAttenuationR:F4}");
        sb.AppendLine($"  Cancellation-R correlation:   r = {report.CancellationGrowthR:F4}");
        sb.AppendLine($"  NetForce-Alignment correlation: r = {report.NetForceAlignmentR:F4}");
        sb.AppendLine();

        // Show alignment breakdown for key R values.
        sb.AppendLine("  Alignment detail (cos law, seed 0):");
        var cosProfiles = profiles.Where(p => p.LawName == "cos" && p.Seed == BaseSeed)
            .OrderBy(p => p.TargetR).ToList();
        sb.AppendLine("  R      │ Net Dir  │ Align    │ Canc    │ Attr Pairs │ Rep Pairs");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var p in cosProfiles)
        {
            double dirDeg = p.NetForceDirection * 180 / Math.PI;
            sb.AppendLine($"  {p.TargetR,5:F1} │ {dirDeg,7:F1}° │ {p.AlignmentScore,7:F3} │ {p.CancellationRatio,7:F5} │ {p.AttractivePairs,9} │ {p.RepulsivePairs,9}");
        }
        sb.AppendLine();

        // ── Section 5: Cancellation Statistics ───────────────────────
        Sec(sb, "5. Cancellation vs Coherence");

        sb.AppendLine("  Cancellation ratio = |Σf| / Σ|f|");
        sb.AppendLine("  0 = perfect cancellation, 1 = perfect alignment.");
        sb.AppendLine();

        // Per-law cancellation.
        sb.AppendLine("  R      │ cos CanR │ sin CanR │ cos² CanR│ exp CanR");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var g in byR)
        {
            double cCos = g.Where(p => p.LawName == "cos").DefaultIfEmpty().Average(p => p?.CancellationRatio ?? 0);
            double cSin = g.Where(p => p.LawName == "sin").DefaultIfEmpty().Average(p => p?.CancellationRatio ?? 0);
            double cCos2 = g.Where(p => p.LawName == "cos²").DefaultIfEmpty().Average(p => p?.CancellationRatio ?? 0);
            double cExp = g.Where(p => p.LawName == "exp(-|x|)").DefaultIfEmpty().Average(p => p?.CancellationRatio ?? 0);
            sb.AppendLine($"  {g.Key,5:F1} │ {cCos,8:F5} │ {cSin,8:F5} │ {cCos2,8:F5} │ {cExp,8:F5}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Do local attractive forces exist at low R?");
        double lowRAttr = byR.FirstOrDefault()?.Average(p => p.AttractiveFraction) ?? 0;
        sb.AppendLine($"    Attractive fraction at R={RMin:F1}: {lowRAttr:P0}");
        sb.AppendLine($"    {(lowRAttr > 0.4 ? "YES — Significant attractive forces even at low R" : "NO — Forces are predominantly repulsive/random at low R")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: How much cancellation occurs?");
        double lowCanc = byR.FirstOrDefault()?.Average(p => p.CancellationRatio) ?? 0;
        double highCanc = byR.LastOrDefault()?.Average(p => p.CancellationRatio) ?? 0;
        sb.AppendLine($"    Cancellation at R={RMin:F1}: {lowCanc:F5} ({lowCanc*100:F0}% of potential force survives)");
        sb.AppendLine($"    Cancellation at R={RMax:F1}: {highCanc:F5} ({highCanc*100:F0}% of potential force survives)");
        sb.AppendLine($"    Cancellation reduction factor: {(lowCanc > 1e-10 ? highCanc / lowCanc : double.PositiveInfinity):F1}x");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does net attraction scale with force alignment?");
        sb.AppendLine($"    r(alignment, net force) = {report.NetForceAlignmentR:F4}");
        sb.AppendLine($"    {(report.NetForceAlignmentR > 0.5 ? "YES — Alignment strongly predicts net force" : "NO — Alignment is not the dominant factor")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Can attraction be explained purely by vector summation?");
        sb.AppendLine($"    r(alignment, R) = {report.AlignmentAttenuationR:F4}");
        sb.AppendLine($"    r(cancellation, R) = {report.CancellationGrowthR:F4}");
        bool explained = report.AlignmentAttenuationR > 0.5 && report.CancellationGrowthR > 0.5;
        sb.AppendLine($"    {(explained ? "YES — Attraction is primarily a vector summation effect" : "NO — Other mechanisms contribute to attraction growth")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Is coherence simply a force-alignment mechanism?");
        sb.AppendLine($"    Alignment grows from {lowCanc:F4} to {highCanc:F4} with coherence.");
        if (report.AlignmentAttenuationR > 0.7)
            sb.AppendLine("    YES — Coherence aligns force vectors, reducing cancellation.");
        else if (report.AlignmentAttenuationR > 0.3)
            sb.AppendLine("    PARTIALLY — Coherence contributes to alignment but other factors matter.");
        else
            sb.AppendLine("    NO — Coherence does not primarily act through force alignment.");
        sb.AppendLine();

        sb.AppendLine("  Q6: Can the attraction curve be predicted from alignment statistics?");
        double r2 = report.NetForceAlignmentR * report.NetForceAlignmentR;
        sb.AppendLine($"    R²(net force ~ alignment) = {r2:F4} ({r2*100:F0}% variance explained)");
        sb.AppendLine();

        // ── Section 6: Direction Histogram Sample ────────────────────
        Sec(sb, "6. Force Direction Distribution (cos law)");

        // Show direction distribution for R=0, R=0.5, R=1.0.
        foreach (double r in new[] { 0.0, 0.5, 1.0 })
        {
            var fp = profiles.FirstOrDefault(p =>
                Math.Abs(p.TargetR - r) < 0.01 && p.LawName == "cos");
            if (fp == null) continue;

            sb.AppendLine($"  R = {r:F1}: Net force direction = {fp.NetForceDirection * 180 / Math.PI:F1}°");
            sb.AppendLine($"  Net force: ({fp.NetForceX:F5}, {fp.NetForceY:F5}), |F| = {fp.NetForceMagnitude:F5}");
            sb.AppendLine($"  Alignment score: {fp.AlignmentScore:F4}");
            sb.AppendLine($"  Cancellation ratio: {fp.CancellationRatio:F5}");
            sb.AppendLine($"  Attractive: {fp.AttractivePairs}/{fp.TotalPairs} ({fp.AttractiveFraction:P0})");
            sb.AppendLine();

            // Show histogram as ASCII bars.
            sb.AppendLine("  Direction histogram (10° bins, scaled to max):");
            double maxCount = fp.DirectionHistogram.Max();
            foreach (int dIdx in new[] { 0, 9, 18, 27 })
            {
                // Show bins around these directions.
                for (int b = dIdx; b < Math.Min(dIdx + 9, 36); b++)
                {
                    double deg = b * 10;
                    double frac = maxCount > 0 ? fp.DirectionHistogram[b] / maxCount : 0;
                    int barLen = (int)(frac * 20);
                    sb.AppendLine($"    {deg,5:F0}°: {new string('#', barLen)}{new string(' ', 20 - barLen)} {fp.DirectionHistogram[b],5:F0}");
                }
                sb.AppendLine();
            }
        }

        // ── Section 7: Interpretation ────────────────────────────────
        Sec(sb, "7. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  Physical picture:");
        sb.AppendLine($"    At low R: {lowCanc*100:F0}% of potential net force survives cancellation.");
        sb.AppendLine($"    At high R: {highCanc*100:F0}% of potential net force survives cancellation.");
        sb.AppendLine();
        sb.AppendLine("    Each pair of oscillators exerts a force F(Δθ) along their");
        sb.AppendLine("    separation vector. When phases are random (low R), Δθ");
        sb.AppendLine("    varies randomly across pairs → forces point in random");
        sb.AppendLine("    directions → vector sum is small (cancellation).");
        sb.AppendLine();
        sb.AppendLine("    When phases are synchronized (high R), Δθ ≈ 0 for most");
        sb.AppendLine("    pairs → F(0) is consistently positive → forces all point");
        sb.AppendLine("    toward the other group → vector sum is large (alignment).");
        sb.AppendLine();
        sb.AppendLine("    Coherence is a FORCE-ALIGNMENT mechanism: it makes the");
        sb.AppendLine("    local coupling forces point in the same direction.");
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. r(alignment, R): {report.AlignmentAttenuationR:F4}");
        sb.AppendLine($"  C3. r(cancellation, R): {report.CancellationGrowthR:F4}");
        sb.AppendLine($"  C4. r(net force, alignment): {report.NetForceAlignmentR:F4}");
        sb.AppendLine($"  C5. Low-R cancellation: {lowCanc:F5} ({lowCanc*100:F0}%)");
        sb.AppendLine($"  C6. High-R cancellation: {highCanc:F5} ({highCanc*100:F0}%)");
        sb.AppendLine($"  C7. Total pair forces analyzed: {profiles.Sum(p => p.TotalPairs):N0}");
        sb.AppendLine();
        sb.AppendLine($"  C8. {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-072 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
