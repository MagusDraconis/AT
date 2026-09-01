using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_070_OnsetOfAttraction : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 700514893;
    private const int TotalIters = 3000;
    private const int SeedsPerLaw = 3;

    public AT_070_OnsetOfAttraction(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_070_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-070 Onset of Attraction");

        sb.AppendLine("AT-070: When Does Attraction First Emerge During Synchronization?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-062-069 established:");
        sb.AppendLine("    - Attraction exists (AT-062).");
        sb.AppendLine("    - Not driven by error minimization (AT-063).");
        sb.AppendLine("    - Not explained by curvature (AT-068).");
        sb.AppendLine("    - Not fully explained by static function");
        sb.AppendLine("      properties (AT-069).");
        sb.AppendLine();
        sb.AppendLine("  This suggests attraction is a DYNAMICAL phenomenon.");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: Attraction emerges at a specific");
        sb.AppendLine("  synchronization stage — it is not present initially");
        sb.AppendLine("  and does not require full synchronization.");
        sb.AppendLine();

        // ── Section 2: Experimental Design ───────────────────────────
        Sec(sb, "2. Experimental Design");
        sb.AppendLine("  Coupling laws: cos(Δθ), cos²(Δθ), exp(-|Δθ|)");
        sb.AppendLine($"  Seeds per law: {SeedsPerLaw}");
        sb.AppendLine($"  Total runs: {3 * SeedsPerLaw}");
        sb.AppendLine($"  N = {NPerGroup * 2}, K = {K}, λ = {Lambda}");
        sb.AppendLine($"  Iterations: {TotalIters}, tracked EVERY timestep");
        sb.AppendLine($"  Position step: 0.001");
        sb.AppendLine();
        sb.AppendLine("  Onset detection algorithm:");
        sb.AppendLine("    1. Smooth separation with 5-point moving average");
        sb.AppendLine("    2. Find first timestep where separation < 99.5% initial");
        sb.AppendLine("    3. Verify sustained decrease over next 50 timesteps");
        sb.AppendLine("    4. Record R, coherence, phase variance at onset");
        sb.AppendLine("    5. Compare onset time vs synchronization time");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var (diagrams, report) = AttractionOnsetAnalyzer.RunFullOnsetAnalysis(
            K, Lambda, NPerGroup, SeedsPerLaw, TotalIters, BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed {diagrams.Count} runs in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine($"  Total timesteps recorded: {diagrams.Sum(d => d.Profiles.Count):N0}");
        sb.AppendLine();

        // ── Section 3: Temporal Evolution ────────────────────────────
        Sec(sb, "3. Temporal Evolution (Sample Trajectory)");

        // Show evolution for the first cos run.
        var sample = diagrams.FirstOrDefault(d => d.LawName == "cos") ?? diagrams.First();
        sb.AppendLine($"  Coupling law: {sample.LawName}, seed: {sample.Seed}");
        sb.AppendLine($"  Initial separation: {sample.InitialSeparation:F4}");
        sb.AppendLine($"  Final separation: {sample.FinalSeparation:F4}");
        sb.AppendLine();

        // Show key milestones.
        sb.AppendLine("  Timestep │ R      │ R_A    R_B  │ Separation │ Velocity │ AttrScore");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        // Show first 200 timesteps densely, then every 200.
        int[] milestones = Enumerable.Range(0, 200).Concat(
            Enumerable.Range(0, sample.Profiles.Count / 200).Select(i => i * 200)
        ).Where(t => t < sample.Profiles.Count).Distinct().OrderBy(t => t).ToArray();

        // Limit output.
        int shown = 0;
        foreach (int t in milestones)
        {
            if (shown >= 25) break;
            var p = sample.Profiles[t];
            sb.AppendLine($"  {p.Timestep,7} │ {p.R,5:F3} │ {p.R_A,5:F3} {p.R_B,5:F3} │ {p.Separation,9:F4} │ {p.Velocity,7:F5} │ {p.AttractionScore,8:P1}");
            shown++;
        }

        // Last point.
        var lastP = sample.Profiles[^1];
        sb.AppendLine($"  {lastP.Timestep,7} │ {lastP.R,5:F3} │ {lastP.R_A,5:F3} {lastP.R_B,5:F3} │ {lastP.Separation,9:F4} │ {lastP.Velocity,7:F5} │ {lastP.AttractionScore,8:P1}");
        sb.AppendLine();

        // ── Section 4: Attraction Onset ──────────────────────────────
        Sec(sb, "4. Attraction Onset Results");

        sb.AppendLine("  Law       │ Seed │ Onset t │ R_at   │ Coh_at │ SepFrac │ Δt(Sync) │ Lead?");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var d in diagrams.OrderBy(d => d.LawName).ThenBy(d => d.Seed))
        {
            var o = d.Onset;
            if (o == null)
            {
                sb.AppendLine($"  {d.LawName,-7} │ {d.Seed,4} │ NO ONSET DETECTED");
                continue;
            }
            string lead = o.BeforeFullSync ? $"\u25B6 +{o.SyncLead:F0}" : $"\u25C0 {o.SyncLead:F0}";
            sb.AppendLine($"  {d.LawName,-7} │ {d.Seed,4} │ {o.Timestep,7} │ {o.RAtOnset,5:F3} │ {o.LocalCoherenceAtOnset,5:F3} │ {o.SepFractionAtOnset,7:F4} │ {lead,9} │ {(o.BeforeFullSync ? "YES" : "no "),4}");
        }
        sb.AppendLine();

        // ── Section 5: Synchronization Analysis ──────────────────────
        Sec(sb, "5. Synchronization Analysis");

        // Sync timing for each run.
        sb.AppendLine("  Law       │ Seed │ Sync t  │ Onset t │ Δt      │ Attr First?");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var d in diagrams.OrderBy(d => d.LawName).ThenBy(d => d.Seed))
        {
            int syncT = d.SyncTimestep;
            int onsetT = d.Onset?.Timestep ?? -1;
            double dt = onsetT >= 0 ? syncT - onsetT : double.NaN;
            string first = dt > 0 ? "YES (+" + dt.ToString("F0") + ")" :
                           dt < 0 ? "no (" + dt.ToString("F0") + ")" : "SAME";
            sb.AppendLine($"  {d.LawName,-7} │ {d.Seed,4} │ {syncT,7} │ {onsetT,7} │ {dt,7:F0} │ {first}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: At what R does attraction first appear?");
        sb.AppendLine($"    Mean R at onset: {report.MeanOnsetR:F4}");
        sb.AppendLine($"    Range: [{diagrams.Where(d=>d.Onset!=null).Min(d=>d.Onset!.RAtOnset):F4}, {diagrams.Where(d=>d.Onset!=null).Max(d=>d.Onset!.RAtOnset):F4}]");
        sb.AppendLine();

        sb.AppendLine("  Q2: Does attraction begin before synchronization?");
        int beforeSync = diagrams.Count(d => d.Onset?.BeforeFullSync == true);
        sb.AppendLine($"    {beforeSync}/{diagrams.Count(d => d.Onset != null)} runs show attraction before full sync");
        sb.AppendLine($"    Fraction: {report.FractionBeforeSync:P0}");
        sb.AppendLine($"    {(report.FractionBeforeSync > 0.5 ? "YES — Attraction typically precedes synchronization" : "NO — Synchronization typically precedes attraction")}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does attraction require a critical coherence?");
        sb.AppendLine($"    Mean local coherence at onset: {report.MeanOnsetLocalCoh:F4}");
        sb.AppendLine($"    Mean phase variance at onset: {report.MeanOnsetPhaseVar:F4}");
        double critCoh = report.MeanOnsetLocalCoh;
        sb.AppendLine($"    {(critCoh < 0.3 ? "NO — Attraction begins at very low coherence" : critCoh < 0.6 ? "PARTIALLY — Moderate coherence needed" : "YES — High coherence required")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Is there a phase transition in attraction?");
        // Check if separation vs R shows a sharp transition.
        var sampleProfiles = sample.Profiles;
        var rSorted = sampleProfiles.Select(p => (p.R, p.Separation))
            .OrderBy(p => p.R).ToList();

        // Find maximum rate of change in separation vs R.
        double maxSlope = 0; int slopeAt = 0;
        for (int i = 5; i < rSorted.Count - 5; i++)
        {
            double slope = Math.Abs(rSorted[i + 5].Separation - rSorted[i - 5].Separation) /
                           Math.Max(rSorted[i + 5].R - rSorted[i - 5].R, 0.001);
            if (slope > maxSlope) { maxSlope = slope; slopeAt = i; }
        }
        double sharpness = maxSlope / sample.InitialSeparation;
        sb.AppendLine($"    Max separation sensitivity to R: {sharpness:F4} per ΔR=0.01");
        sb.AppendLine($"    {(sharpness > 0.5 ? "YES — Sharp transition detected" : sharpness > 0.1 ? "WEAK — Gradual transition" : "NO — Continuous, no phase transition")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can attraction emerge while the system is unsynchronized?");
        var lowSyncOnset = diagrams.Where(d =>
            d.Onset != null && d.Onset.RAtOnset < 0.3).ToList();
        sb.AppendLine($"    Unsynchronized onsets (R<0.3): {lowSyncOnset.Count}/{diagrams.Count(d=>d.Onset!=null)}");
        sb.AppendLine($"    {(lowSyncOnset.Count > 0 ? "YES — Attraction can begin before significant synchronization" : "NO — Attraction requires at least partial synchronization")}");
        sb.AppendLine();

        // ── Section 6: Threshold Search ──────────────────────────────
        Sec(sb, "6. Threshold Search");

        // Bin R values and compute mean attraction score per bin.
        var allProfiles = diagrams.SelectMany(d => d.Profiles).ToList();
        int nBins = 20;
        var rBins = new double[nBins];
        var attrBins = new double[nBins];
        var countBins = new int[nBins];

        for (int b = 0; b < nBins; b++)
        {
            double rLo = (double)b / nBins, rHi = (double)(b + 1) / nBins;
            var inBin = allProfiles.Where(p => p.R >= rLo && p.R < rHi).ToList();
            if (inBin.Count > 0)
            {
                rBins[b] = (rLo + rHi) / 2;
                attrBins[b] = inBin.Average(p => p.AttractionScore);
                countBins[b] = inBin.Count;
            }
        }

        sb.AppendLine("  R-bin  │ Mean R   │ Mean Attr │ Count");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        for (int b = 0; b < nBins; b++)
        {
            if (countBins[b] == 0) continue;
            sb.AppendLine($"  {b,5}  │ {rBins[b],7:F3} │ {attrBins[b],8:P1} │ {countBins[b],5}");
        }
        sb.AppendLine();

        // Find the R threshold where attraction becomes positive.
        int thresholdBin = -1;
        for (int b = 0; b < nBins; b++)
        {
            if (attrBins[b] > 0.01 && countBins[b] > 0)
            { thresholdBin = b; break; }
        }
        if (thresholdBin >= 0)
            sb.AppendLine($"  Attraction becomes positive at R ≈ {rBins[thresholdBin]:F3}");
        sb.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        Sec(sb, "7. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  Physical meaning:");
        sb.AppendLine($"    Mean onset R: {report.MeanOnsetR:F4}");
        sb.AppendLine($"    Mean onset coherence: {report.MeanOnsetLocalCoh:F4}");
        sb.AppendLine($"    Mean onset phase variance: {report.MeanOnsetPhaseVar:F4}");
        sb.AppendLine($"    Mean onset separation fraction: {report.MeanOnsetSepFrac:F4}");
        sb.AppendLine($"    Fraction before full sync: {report.FractionBeforeSync:P0}");
        sb.AppendLine();

        if (report.FractionBeforeSync > 0.5)
        {
            sb.AppendLine("    Attraction EMERGES DURING synchronization — it does");
            sb.AppendLine("    not wait for full phase alignment. This suggests");
            sb.AppendLine("    that spatial motion and phase synchronization are");
            sb.AppendLine("    COUPLED DYNAMICAL PROCESSES that co-evolve, rather");
            sb.AppendLine("    than one causing the other. The system self-organizes");
            sb.AppendLine("    simultaneously in phase space and position space.");
        }
        else
        {
            sb.AppendLine("    Attraction FOLLOWS synchronization — phase alignment");
            sb.AppendLine("    is a prerequisite for spatial convergence. Without");
            sb.AppendLine("    sufficient phase coherence, the coupling forces");
            sb.AppendLine("    cancel out and produce no net spatial motion. Only");
            sb.AppendLine("    after oscillators synchronize can they exert coherent");
            sb.AppendLine("    forces on each other's positions.");
        }
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Mean R at onset: {report.MeanOnsetR:F4}");
        sb.AppendLine($"  C3. Mean coherence at onset: {report.MeanOnsetLocalCoh:F4}");
        sb.AppendLine($"  C4. Mean phase variance at onset: {report.MeanOnsetPhaseVar:F4}");
        sb.AppendLine($"  C5. Fraction attraction before sync: {report.FractionBeforeSync:P0}");
        sb.AppendLine($"  C6. Mean sync lead: {report.MeanSyncLead:F0} timesteps");
        sb.AppendLine();
        sb.AppendLine($"  C7. Attraction onset: {report.Classification}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-070 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
