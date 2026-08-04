using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_101_TheoryRepairProgram : ResearchTestBase
{
    private const int BaseSeed = 101_000_001;

    public TQM_101_TheoryRepairProgram(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_101_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-101 Theory Repair Program");

        sb.AppendLine("TQM-101: Repairing the rejected {R, M} theory.");
        sb.AppendLine("         Preserve state variables. Repair equations.");
        sb.AppendLine();

        // ── Section 1: The Problem ───────────────────────────────────
        Sec(sb, "1. The Rejected Theory (TQM-083 / TQM-100)");
        sb.AppendLine("  State = {R, M}");
        sb.AppendLine("  Original equation: dR/dt = α₀ + α₁·R + α₂·M");
        sb.AppendLine("  TQM-100 result: REJECTED — 5/8 attacks broke the theory.");
        sb.AppendLine();
        sb.AppendLine("  Critical failures:");
        sb.AppendLine("    1. R≈0 / R≈1 — linear model doesn't saturate");
        sb.AppendLine("    2. N=10 / N=500 — no N-dependence");
        sb.AppendLine("    3. Phase noise — no noise model");
        sb.AppendLine("    4. OOD parameters — parameter extrapolation fails");
        sb.AppendLine();
        sb.AppendLine("  Surviving strengths:");
        sb.AppendLine("    • M is universal across coupling laws (R²=0.78)");
        sb.AppendLine("    • Topology independence confirmed (R²=0.78)");
        sb.AppendLine("    • {R, M} state variables are CORRECT");
        sb.AppendLine();

        // ── Section 2: Repair Candidates ─────────────────────────────
        Sec(sb, "2. Repair Candidates");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var trainData = TheoryRepairAnalyzer.GenerateTrainData(BaseSeed);
        var candidates = TheoryRepairAnalyzer.GenerateRepairedCandidates(trainData);
        sw.Stop();

        sb.AppendLine($"  Training data: {trainData.Count} points (N=10..500, K=0.5..5, λ=0.03..0.20)");
        sb.AppendLine($"  Generated in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();
        sb.AppendLine("  Model │ Equation                              │ Train R² │ Description");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var c in candidates)
            sb.AppendLine($"  {c.Name,-5} │ {c.Equation,-38} │ {c.TrainingR2,7:F4} │ {c.Description.Split('.')[0]}");
        sb.AppendLine();

        // ── Section 3: Validation ────────────────────────────────────
        Sec(sb, "3. Full Validation (TQM-100 Attack Vectors)");

        var report = TheoryRepairAnalyzer.RunRepairPipeline(BaseSeed);

        // Per-candidate survival matrix.
        sb.AppendLine("  Attack vector              │ M0(base) │ A    │ B    │ C    │ D    │ E    │ F");
        sb.AppendLine("  " + new string('─', 95));
        var attackNames = report.PerCandidateResults[report.BestCandidate.Name]
            .Select(r => r.AttackName).ToList();
        foreach (var atkName in attackNames)
        {
            sb.Append($"  {atkName,-25} │");
            foreach (var cand in candidates)
            {
                var res = report.PerCandidateResults[cand.Name]
                    .First(r => r.AttackName == atkName);
                string marker = res.Passed ? $" {res.R2,5:F2}✓" : $" {res.R2,5:F2}✗";
                sb.Append(marker);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        // Survival rates.
        sb.AppendLine("  ── Survival Summary ──");
        sb.AppendLine("  Model │ Passed │ Survival │ Mean R²  │ Verdict");
        sb.AppendLine("  " + new string('─', 60));
        var scored = candidates.Select(c =>
        {
            var results = report.PerCandidateResults[c.Name];
            int p = results.Count(r => r.Passed);
            double surv = (double)p / results.Count;
            double mR2 = results.Average(r => r.R2);
            string verdict = surv >= 0.875 ? "★ REPAIRED" :
                             surv >= 0.625 ? "✓ Improved" :
                             surv >= 0.375 ? "~ Partial" : "✗ Still broken";
            return (c.Name, Passed: p, Survival: surv, MeanR2: mR2, Verdict: verdict);
        }).OrderByDescending(x => x.Survival).ThenByDescending(x => x.MeanR2).ToList();

        foreach (var s in scored)
            sb.AppendLine($"  {s.Name,-5} │ {s.Passed,4}/8  │ {s.Survival,6:P0}   │ {s.MeanR2,7:F3} │ {s.Verdict}");
        sb.AppendLine();

        // ── Section 4: Best Repaired Theory ──────────────────────────
        Sec(sb, "4. Best Repaired Theory");

        var best = report.BestCandidate;
        sb.AppendLine($"  Model {best.Name}: {best.Equation}");
        sb.AppendLine($"  {best.Description}");
        sb.AppendLine();
        sb.AppendLine($"  Training R²: {best.TrainingR2:F4}");
        sb.AppendLine($"  Training MAE: {best.TrainingMAE:F6}");
        sb.AppendLine();

        // Per-attack detail for best.
        sb.AppendLine("  ── Per-Attack Detail ──");
        sb.AppendLine("  Attack                  │ R²       │ MAE      │ Pass?");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var r in report.PerCandidateResults[best.Name])
            sb.AppendLine($"  {r.AttackName,-22} │ {r.R2,7:F4} │ {r.MAE,7:F5} │ {(r.Passed ? "✓" : "✗ FAIL")}");
        sb.AppendLine();

        // ── Section 5: Improvement Analysis ──────────────────────────
        Sec(sb, "5. Improvement Over Baseline");

        var baseline = report.Baseline;
        sb.AppendLine($"  Baseline (M0) survival: {report.PerCandidateResults["M0"].Count(r => r.Passed)}/8");
        sb.AppendLine($"  Repaired ({best.Name}) survival: {report.BestSurvivalRate * 8:F0}/8");
        sb.AppendLine();

        sb.AppendLine("  Attack                  │ M0 R²    │ Best R²  │ ΔR²      │ Fixed?");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var atkName in attackNames)
        {
            double m0r2 = report.PerCandidateResults["M0"].First(r => r.AttackName == atkName).R2;
            double bestR2 = report.PerCandidateResults[best.Name].First(r => r.AttackName == atkName).R2;
            double delta = bestR2 - m0r2;
            string fixed_ = delta > 0.05 ? "✓ YES" : delta > 0 ? "~ Partial" : "✗ NO";
            sb.AppendLine($"  {atkName,-22} │ {m0r2,7:F4} │ {bestR2,7:F4} │ {delta,+7:F4} │ {fixed_}");
        }
        sb.AppendLine();

        // ── Section 6: Research Questions ────────────────────────────
        Sec(sb, "6. Research Questions");

        sb.AppendLine("  Q1: Can the rejected theory be repaired?");
        sb.AppendLine($"    Baseline survival: {report.PerCandidateResults["M0"].Count(r => r.Passed)}/8");
        sb.AppendLine($"    Best survival:     {report.BestSurvivalRate * 8:F0}/8");
        if (report.BestSurvivalRate >= 0.75)
            sb.AppendLine("    YES — the theory has been substantially repaired.");
        else if (report.BestSurvivalRate >= 0.50)
            sb.AppendLine("    PARTIALLY — significant improvement but gaps remain.");
        else
            sb.AppendLine("    BARELY — fundamental issues remain unresolved.");
        sb.AppendLine();

        sb.AppendLine("  Q2: Is R·(1-R) required?");
        // Compare Model A (logistic without N) vs M0 (linear).
        double aR2 = report.PerCandidateResults["A"].Average(r => r.R2);
        double m0R2avg = report.PerCandidateResults["M0"].Average(r => r.R2);
        sb.AppendLine($"    Model A (logistic) mean R²: {aR2:F3} vs M0 (linear): {m0R2avg:F3}");
        sb.AppendLine($"    {(aR2 > m0R2avg ? "YES — logistic form improves generalization." : "NO — logistic alone is insufficient.")}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Is N dependence required?");
        double bR2 = report.PerCandidateResults["B"].Average(r => r.R2);
        sb.AppendLine($"    Model B (N·M) mean R²: {bR2:F3} vs A (M only): {aR2:F3}");
        sb.AppendLine($"    {(bR2 > aR2 + 0.05 ? "YES — N·M scaling is essential for N-generalization." : "NO — N scaling does not help significantly.")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Can all TQM-100 failures be eliminated?");
        int remaining = report.PerCandidateResults[best.Name].Count(r => !r.Passed);
        sb.AppendLine($"    Remaining failures: {remaining}/8");
        if (remaining == 0)
            sb.AppendLine("    YES — all attack vectors now pass.");
        else
        {
            sb.AppendLine("    NO — some failures persist:");
            foreach (var r in report.PerCandidateResults[best.Name].Where(r => !r.Passed))
                sb.AppendLine($"      • {r.AttackName}: R²={r.R2:F3}");
        }
        sb.AppendLine();

        sb.AppendLine("  Q5: What is the minimal repaired theory?");
        sb.AppendLine($"    Model {best.Name}: {best.Equation}");
        sb.AppendLine($"    Survival: {report.BestSurvivalRate:P0}, Mean R²: {report.PerCandidateResults[best.Name].Average(r => r.R2):F3}");
        sb.AppendLine();

        sb.AppendLine("  Q6: Does the repaired theory survive hostile review?");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine($"    {(report.Classification.StartsWith("D") ? "YES — the repaired theory is robust against all attacks." : report.Classification.StartsWith("C") ? "MOSTLY — robust with minor gaps." : "PARTIALLY — significant gaps remain.")}");
        sb.AppendLine();

        // ── Section 7: Classification ────────────────────────────────
        Sec(sb, "7. Final Classification");

        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  Repaired equation: {best.Equation}");
        sb.AppendLine($"  Original: dR/dt = α₀ + α₁·R + α₂·M  (rejected, TQM-100)");
        sb.AppendLine($"  Repaired: {best.Equation}  (TQM-101)");
        sb.AppendLine();

        // Summary matrix.
        sb.AppendLine("  ── Repair Summary Matrix ──");
        sb.AppendLine("  Failure Mode          │ Before (M0) │ After (Best) │ Repaired?");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var atkName in attackNames)
        {
            double before = report.PerCandidateResults["M0"].First(r => r.AttackName == atkName).R2;
            double after = report.PerCandidateResults[best.Name].First(r => r.AttackName == atkName).R2;
            bool repaired = report.PerCandidateResults[best.Name].First(r => r.AttackName == atkName).Passed;
            sb.AppendLine($"  {atkName,-22} │ {before,9:F3}   │ {after,10:F3}  │ {(repaired ? "✓ YES" : "✗ NO")}");
        }
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1.  Baseline (M0) attacks passed: {report.PerCandidateResults["M0"].Count(r => r.Passed)}/8");
        sb.AppendLine($"  C2.  Repaired attacks passed:      {report.BestSurvivalRate * 8:F0}/8");
        sb.AppendLine($"  C3.  Best model: {best.Name} — {best.Equation}");
        sb.AppendLine($"  C4.  Classification: {report.Classification}");
        sb.AppendLine($"  C5.  Training R²: {best.TrainingR2:F4}");
        sb.AppendLine($"  C6.  Remaining failures: {report.PerCandidateResults[best.Name].Count(r => !r.Passed)}");
        sb.AppendLine();

        if (report.Classification.StartsWith("D"))
        {
            sb.AppendLine("  C7.  THE THEORY IS REPAIRED.");
            sb.AppendLine("       The repaired equation survives all TQM-100 attacks.");
            sb.AppendLine($"       {best.Equation}");
            sb.AppendLine("       is a candidate for emergent physics.");
        }
        else if (report.Classification.StartsWith("C"))
        {
            sb.AppendLine("  C7.  THE THEORY IS SUBSTANTIALLY REPAIRED.");
            sb.AppendLine($"       Most TQM-100 attacks now pass. {report.PerCandidateResults[best.Name].Count(r => !r.Passed)} remain.");
            sb.AppendLine($"       {best.Equation}");
            sb.AppendLine("       is a robust effective theory.");
        }
        else
        {
            sb.AppendLine("  C7.  THE THEORY REMAINS INCOMPLETE.");
            sb.AppendLine("       Further repair iteration needed.");
        }
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-101 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
