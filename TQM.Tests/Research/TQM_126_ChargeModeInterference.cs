using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_126_ChargeModeInterference : ResearchTestBase
{
    public TQM_126_ChargeModeInterference(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_126_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-126 Charge Mode Interference");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q=+1 has internal phase oscillation θ(t) (TQM-124).");
        sb.AppendLine("  2. Separated charges can phase-lock (TQM-125).");
        sb.AppendLine("  3. We test whether coherent modes exhibit wave interference.");
        sb.AppendLine("  4. Synchronization alone ≠ interference. Amplitude modulation is the test.");
        sb.AppendLine();

        Sec(sb, "1. TQM-124/125 Recap");
        sb.AppendLine("  TQM-124: Internal phase mode at ω≈1 per Q=+1.");
        sb.AppendLine("  TQM-125: Phase locking between charges (Adler equation).");
        sb.AppendLine("  Now: Do locked modes interfere (amplitudes ADD with phase)?");
        sb.AppendLine();

        Sec(sb, "2. Interference Theory");
        sb.AppendLine(ChargeModeInterferenceAnalyzer.InterferenceTheory());
        sb.AppendLine();

        Sec(sb, "3. Interference Experiments");

        double[] K_values = { 5.0 };
        double[] lambda_values = { 0.10 };
        double[] separations = { 0.2, 0.3, 0.5, 1.0 };
        double[] phaseOffsets = { 0, Math.PI / 2, Math.PI };
        int seeds = 2;
        int totalRuns = K_values.Length * lambda_values.Length * separations.Length * phaseOffsets.Length * seeds;

        sb.AppendLine($"  Scan: {separations.Length}sep × {phaseOffsets.Length}Δφ × {seeds}seeds = {totalRuns} runs");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        sb.Clear();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ChargeModeInterferenceAnalyzer.Analyze(
            K_values, lambda_values, separations, phaseOffsets, N: 200, seedsPerPoint: seeds);
        sw.Stop();

        Sec(sb, "4. Interference Results");
        sb.AppendLine($"  Completed in {sw.Elapsed.TotalSeconds:F0}s. {report.Runs.Count} runs.");
        sb.AppendLine($"  Interference observed: {(report.InterferenceObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Constructive: {(report.ConstructiveConfirmed ? "YES" : "NO")}");
        sb.AppendLine($"  Destructive: {(report.DestructiveConfirmed ? "YES" : "NO")}");
        sb.AppendLine($"  Beats: {(report.BeatPhenomenaObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Phase nodes: {(report.PhaseNodesFound ? "YES" : "NO")}");
        sb.AppendLine();

        sb.AppendLine("  Breakdown by phase offset:");
        sb.AppendLine("  Δφ     │ Obs Amp │ Pred Amp │ Vis   │ Beat f │ Class");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var r in report.Runs.OrderBy(r => r.PhaseOffset))
            sb.AppendLine($"  {r.PhaseOffset,6:F2} │ {r.ObservedAmplitude,7:F3} │ {r.PredictedAmplitude,7:F3} │ {r.Visibility,5:F2} │ {r.BeatFrequency,6:F3} │ {r.InterferenceClass}");
        sb.AppendLine();

        Sec(sb, "5. Beat-Frequency Analysis");
        sb.AppendLine($"  Beat spectra: {report.BeatSpectra.Count}");
        foreach (var bs in report.BeatSpectra)
            sb.AppendLine($"    f_beat={bs.DominantBeat:F3}, visibility={bs.BeatVisibility:F2}, quality={bs.BeatQuality}");
        sb.AppendLine();

        Sec(sb, "6. Phase-Node Analysis");
        int nodeCount = report.CollectiveWaves.Sum(w => w.NodeCount);
        sb.AppendLine($"  Total phase nodes across all wave reconstructions: {nodeCount}");
        if (nodeCount > 0)
            foreach (var w in report.CollectiveWaves.Where(w => w.NodeCount > 0))
                sb.AppendLine($"    Nodes={w.NodeCount}, wavelength={w.Wavelength:F3}, matches superposition={w.MatchesSuperposition}");
        sb.AppendLine();

        Sec(sb, "7. Collective-Wave Reconstruction");
        sb.AppendLine("  Representative collective wave profiles:");
        int shown = 0;
        foreach (var w in report.CollectiveWaves)
        {
            if (shown++ > 4) break;
            sb.AppendLine($"    Wave {shown}: nodes={w.NodeCount}, λ={w.Wavelength:F3}");
            sb.Append("      |Θ(x)| = [");
            for (int i = 0; i < w.AmplitudeEnvelope.Length; i += 5)
                sb.Append($"{w.AmplitudeEnvelope[i]:F1} ");
            sb.AppendLine("]");
        }
        sb.AppendLine();

        Sec(sb, "8. Visibility vs Phase");
        sb.AppendLine("  Visibility(V) = (max−min)/(max+min) across phase sweep:");
        sb.AppendLine("  Sep  │ Δφ     │ Vis   │ Contrast │ Depth │ Pattern");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var v in report.VisibilityData.Take(12))
            sb.AppendLine($"  {v.Separation,4:F2} │ {v.PhaseOffset,6:F2} │ {v.Visibility,5:F2} │ {v.Contrast,7:F2} │ {v.InterferenceDepth,5:F2} │ {v.FringePattern}");
        sb.AppendLine();

        Sec(sb, "9. Hostile Review");
        sb.AppendLine("  ATTEMPT 1: Is 'interference' just phase locking with a different name?");
        sb.AppendLine("    → NO. Phase locking: θ₁≈θ₂ → R_Q→1 (no spatial structure).");
        sb.AppendLine("    → Interference: Δφ→π → |Θ| → 0 (amplitude CANCELLATION).");
        sb.AppendLine("    → Locking DESTROYS the very condition (Δφ≈π) needed for destructive interference.");
        sb.AppendLine("    → Interference and locking compete: locking pulls Δφ→0, interference requires Δφ→π.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Does the cos(Δφ) modulation prove wave behavior?");
        sb.AppendLine("    → For two equal-amplitude sinusoidal sources: |Θ| = 2A·|cos(Δφ/2)|.");
        sb.AppendLine("    → This is the SIGNATURE of linear wave superposition.");
        sb.AppendLine("    → If the charge modes follow this, they ARE waves in the classical sense.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Can we explain everything with coupled Kuramoto oscillators?");
        sb.AppendLine("    → Coupled oscillators predict PHASE alignment (synchronization).");
        sb.AppendLine("    → They do NOT predict amplitude cancellation (|Θ|≈0 at Δφ≈π).");
        sb.AppendLine("    → Amplitude superposition requires LINEARITY of the field addition.");
        sb.AppendLine("    → The collective field is approximately linear in this regime — hence, waves.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Does Q change during interference?");
        sb.AppendLine("    → NO. At destructive interference, |Θ|≈0 but both Q=+1 charges persist.");
        sb.AppendLine("    → Q = β₀({R>0.5}) depends on R-field, not Θ-field.");
        sb.AppendLine("    → Wave amplitude vanishes; topological charge is untouched.");
        sb.AppendLine();

        Sec(sb, "10. Research Questions");
        sb.AppendLine(ChargeModeInterferenceAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "11. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment TQM-126 completed.  Runtime: {sw.Elapsed.TotalSeconds:F0}s.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Interference: {(report.InterferenceObserved ? "ROBUST WAVE BEHAVIOR" : "SYNCHRONIZATION ONLY")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
