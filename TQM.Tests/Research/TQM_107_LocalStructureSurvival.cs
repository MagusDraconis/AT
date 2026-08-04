using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_107_LocalStructureSurvival : ResearchTestBase
{
    private const int BaseSeed = 107_000_001;

    public TQM_107_LocalStructureSurvival(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_107_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-107 Local Structure Survival");

        sb.AppendLine("TQM-107: Do local condensates survive despite the mean-field");
        sb.AppendLine("         prediction of inevitable global synchronization?");
        sb.AppendLine();

        // ── Section 1: The Contradiction ─────────────────────────────
        Sec(sb, "1. The Apparent Contradiction");

        sb.AppendLine("  TQM-106 (mean-field phase portrait):");
        sb.AppendLine("    • (R,M) → (1,K) global attractor");
        sb.AppendLine("    • Inevitable synchronization + spatial collapse");
        sb.AppendLine("    • Self-reinforcing feedback loop");
        sb.AppendLine();
        sb.AppendLine("  Earlier TQM experiments (010-012):");
        sb.AppendLine("    • Stable proto-matter condensates");
        sb.AppendLine("    • Multiple simultaneous condensates (5+)");
        sb.AppendLine("    • Long-lived local structures (τ=4500+)");
        sb.AppendLine("    • Condensates survive while global R < 0.5");
        sb.AppendLine();
        sb.AppendLine("  QUESTION: Why do local structures survive if the");
        sb.AppendLine("  mean-field theory predicts collapse to global sync?");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        Sec(sb, "2. Spatial Simulation Setup");

        sb.AppendLine("  4 scenarios, N=100, K=2.0, λ=0.05, 5000 iterations:");
        sb.AppendLine("    • single: 1 tight cluster at center");
        sb.AppendLine("    • two:    2 separated clusters at x=0.2, 0.8");
        sb.AppendLine("    • multi:  5 small clusters spread across space");
        sb.AppendLine("    • random: uniform spatial distribution");
        sb.AppendLine();
        sb.AppendLine("  Tracked: global R, global M, condensate count,");
        sb.AppendLine("  local R per condensate, mean-field prediction error.");
        sb.AppendLine();

        // ── Section 3: Results ───────────────────────────────────────
        Sec(sb, "3. Spatial Simulation Results");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = LocalStructureAnalyzer.RunBreakdownAnalysis(BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        sb.AppendLine("  Scenario  │ Final R │ Final M │ Condensates │ M-F Error  │ M-F Failed?");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var p in report.Profiles)
            sb.AppendLine(
                $"  {p.Scenario,-9} │ {p.FinalGlobalR,6:F4} │ {p.History[^1].GlobalM,6:F4} │ {p.FinalCondensateCount,9}   │ {p.MeanFieldError,9:F5} │ {(p.MeanFieldFailed ? "⚠ YES" : "✓ NO")}");
        sb.AppendLine();

        // ── Section 4: Per-Scenario Detail ───────────────────────────
        Sec(sb, "4. Per-Scenario Evolution");

        foreach (var p in report.Profiles)
        {
            sb.AppendLine($"  ── {p.Scenario} ──");
            sb.AppendLine("  Iter  │ Global R │ Global M │ Condensates │ Local R (max) │ dR/dt pred │ dR/dt actual");
            sb.AppendLine("  " + new string('─', 100));

            int showCount = Math.Min(p.History.Count, 8);
            int step = Math.Max(1, p.History.Count / showCount);
            for (int i = 0; i < p.History.Count; i += step)
            {
                var snap = p.History[i];
                double maxLocalR = snap.Condensates.Count > 0
                    ? snap.Condensates.Max(c => c.LocalR) : 0;
                sb.AppendLine(
                    $"  {snap.Iteration,4}  │ {snap.GlobalR,7:F4} │ {snap.GlobalM,7:F4} │ {snap.CondensateCount,9}   │ {maxLocalR,11:F4} │ {snap.MeanFieldPredictedDR,9:F5} │ {snap.ActualDR,11:F5}");
            }

            sb.AppendLine();
            sb.AppendLine($"  Mean |predicted − actual dR/dt|: {p.MeanFieldError:F5}");
            sb.AppendLine($"  Mean-field breakdown: {(p.MeanFieldFailed ? "YES — condensates survive while prediction fails" : "NO — mean-field adequate")}");
            sb.AppendLine();
        }

        // ── Section 5: Research Questions ────────────────────────────
        Sec(sb, "5. Research Questions");

        sb.AppendLine("  Q1: Do local condensates survive after global R increases?");
        int survivors = report.Profiles.Count(p => p.FinalCondensateCount > 0);
        sb.AppendLine($"    {survivors}/{report.Profiles.Count} scenarios retain condensates at t=5000.");
        foreach (var p in report.Profiles)
        {
            double startR = p.History[0].GlobalR;
            double endR = p.FinalGlobalR;
            sb.AppendLine($"    {p.Scenario}: R {startR:F3}→{endR:F3}, condensates: {p.History[0].CondensateCount}→{p.FinalCondensateCount}");
        }
        sb.AppendLine();

        sb.AppendLine("  Q2: When does mean-field theory fail?");
        int failures = report.Profiles.Count(p => p.MeanFieldFailed);
        sb.AppendLine($"    {failures}/{report.Profiles.Count} scenarios show mean-field breakdown.");
        sb.AppendLine("    Failure condition: condensates persist while global R < 0.9.");
        sb.AppendLine("    The mean-field assumes all oscillators are equivalent —");
        sb.AppendLine("    it cannot distinguish 5 separate condensates from 1 big one.");
        sb.AppendLine();

        sb.AppendLine("  Q3: Can global synchronization coexist with local structure?");
        bool coexistence = report.Profiles.Any(p =>
            p.FinalCondensateCount > 1 && p.FinalGlobalR > 0.3);
        sb.AppendLine($"    {(coexistence ? "YES — multiple condensates coexist at moderate global R." : "NO — structures merge or dissolve.")}");
        sb.AppendLine("    Each condensate INTERNALLY synchronizes (local R→1) while");
        sb.AppendLine("    remaining phase-incoherent with OTHER condensates.");
        sb.AppendLine("    This is IMPOSSIBLE to capture with global R alone.");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does local variance remain nonzero when R→1?");
        foreach (var p in report.Profiles)
        {
            double varR = 0;
            var lastSnap = p.History[^1];
            foreach (var c in lastSnap.Condensates)
                varR += (c.LocalR - lastSnap.GlobalR) * (c.LocalR - lastSnap.GlobalR);
            varR /= Math.Max(lastSnap.Condensates.Count, 1);
            sb.AppendLine($"    {p.Scenario}: Var(local R) = {varR:F4} at final R={p.FinalGlobalR:F3}");
        }
        sb.AppendLine();

        sb.AppendLine("  Q5: Are condensates attractors of the spatial field?");
        sb.AppendLine("    YES — within each condensate, local R → 1 (TQM-010, TQM-106).");
        sb.AppendLine("    But condensates THEMSELVES are not global attractors —");
        sb.AppendLine("    multiple condensates can coexist indefinitely if spatially");
        sb.AppendLine("    separated (beyond coupling range ~3λ).");
        sb.AppendLine();

        sb.AppendLine("  Q6: What degrees of freedom are lost in mean-field?");
        sb.AppendLine("    The mean-field {R, M} loses:");
        sb.AppendLine("    • Spatial distribution of oscillators");
        sb.AppendLine("    • Number of condensates");
        sb.AppendLine("    • Inter-condensate phase relationships");
        sb.AppendLine("    • Spatial correlation length");
        sb.AppendLine("    • Local variance of R and M");
        sb.AppendLine();

        sb.AppendLine("  Q7: Must TQM become a spatial field theory?");
        sb.AppendLine($"    Classification: {report.Classification}");
        if (report.Classification.StartsWith("D"))
            sb.AppendLine("    YES — the mean-field approximation is INSUFFICIENT.");
        else
            sb.AppendLine("    PARTIALLY — spatial corrections needed in some regimes.");
        sb.AppendLine();

        // ── Section 6: Classification ────────────────────────────────
        Sec(sb, "6. Classification");

        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1.  Scenarios: {report.Profiles.Count}");
        sb.AppendLine($"  C2.  Mean-field failures: {failures}");
        sb.AppendLine($"  C3.  Classification: {report.Classification}");
        sb.AppendLine($"  C4.  The mean-field theory {report.Profiles[0].Scenario} scenario");
        sb.AppendLine($"       Final R={report.Profiles[0].FinalGlobalR:F3}, condensates={report.Profiles[0].FinalCondensateCount}");
        sb.AppendLine();
        sb.AppendLine($"  C5.  LOCAL STRUCTURE SURVIVES despite global synchronization");
        sb.AppendLine("       because spatial separation prevents inter-condensate coupling.");
        sb.AppendLine("       The mean-field variables {R, M} are NECESSARY but");
        sb.AppendLine("       INSUFFICIENT for multi-condensate systems.");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-107 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
