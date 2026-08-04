using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_100_PhysicsCandidateValidation : ResearchTestBase
{
    private const int BaseSeed = 100_000_001;

    public TQM_100_PhysicsCandidateValidation(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_100_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-100 Physics Candidate Validation — Hostile Review");

        sb.AppendLine("TQM-100: Attempting to FALSIFY the {R, M} minimal theory.");
        sb.AppendLine("         Acting as hostile reviewer. The theory must survive.");
        sb.AppendLine();

        // ── Section 1: The Theory Under Attack ───────────────────────
        Sec(sb, "1. Theory Under Attack");
        sb.AppendLine("  State = {R, M}");
        sb.AppendLine("  Derived: A = R², F_net = A × ⟨f⟩");
        sb.AppendLine("  Governing equations:");
        sb.AppendLine("    dR/dt = β₀ + β₁·R + β₂·M");
        sb.AppendLine("    dM/dt = β₀ + β₁·R + β₂·M");
        sb.AppendLine();
        sb.AppendLine("  Trained on: N=100, K=2.0, λ=0.05, exp(-d/λ) coupling,");
        sb.AppendLine("    6 topology types, R≈0.09, M≈0.10");
        sb.AppendLine();
        sb.AppendLine("  WE WILL ATTACK THIS THEORY FROM 8 DIRECTIONS.");
        sb.AppendLine();

        // ── Section 2: Adversarial Attack Vectors ────────────────────
        Sec(sb, "2. Attack Vectors");

        sb.AppendLine("  Attack │ Vector                    │ Hypothesis");
        sb.AppendLine("  " + new string('─', 70));
        sb.AppendLine("    1    │ Extreme Coherence         │ Theory fails at R≈0 and R≈1");
        sb.AppendLine("    2    │ Extreme Mean Coupling     │ Theory fails at M→0 and M>>1");
        sb.AppendLine("    3    │ Mixed Topologies          │ Theory overfits specific topologies");
        sb.AppendLine("    4    │ Different Coupling Laws   │ Theory fails with non-exp coupling");
        sb.AppendLine("    5    │ High Phase Noise          │ Noise destroys predictability");
        sb.AppendLine("    6    │ Large-N Scaling (N=500)   │ Theory fails at larger systems");
        sb.AppendLine("    7    │ Small-N Scaling (N=10)    │ Theory fails at smaller systems");
        sb.AppendLine("    8    │ Out-of-Distribution       │ Theory fails at untrained (K,λ)");
        sb.AppendLine();

        // ── Section 3: Run Validation ────────────────────────────────
        Sec(sb, "3. Validation Results");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = PhysicsCandidateValidator.RunFullValidation(BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // Per-attack results.
        sb.AppendLine("  Attack                  │ Points │ R²(dR/dt) │ MAE      │ Verdict");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var test in report.StressTests)
        {
            string verdict = test.TheoryFailed
                ? "⚠ FAILED"
                : "✓ PASSED";
            sb.AppendLine($"  {test.Name,-22} │ {test.NumPoints,5}  │ {test.R2_dRdt,8:F4} │ {test.MeanAbsError,7:F5} │ {verdict}");
        }
        sb.AppendLine();

        // Training baseline.
        var model = PhysicsCandidateValidator.TrainOnStandardData(200, BaseSeed);
        sb.AppendLine($"  Training R²(dR/dt): {model.R2_DRdt:F4}");
        sb.AppendLine();

        // ── Section 4: Failure Analysis ──────────────────────────────
        Sec(sb, "4. Failure Analysis");

        if (report.Failures.Count == 0)
        {
            sb.AppendLine("  ★ NO FAILURES DETECTED ★");
            sb.AppendLine("  The {R, M} theory survived all 8 attack vectors.");
            sb.AppendLine();
        }
        else
        {
            sb.AppendLine($"  {report.Failures.Count} failure(s) detected:");
            sb.AppendLine("  Attack                  │ Severity    │ Train R² │ Test R²  │ ΔR²      │ Analysis");
            sb.AppendLine("  " + new string('─', 95));
            foreach (var f in report.Failures)
            {
                double delta = f.ObservedR2 - f.ExpectedR2;
                sb.AppendLine($"  {f.Scenario,-22} │ {f.Severity,-11} │ {f.ExpectedR2,7:F4} │ {f.ObservedR2,7:F4} │ {delta,+7:F4} │ {f.Interpretation}");
            }
            sb.AppendLine();
        }

        // ── Section 5: Detailed Per-Attack Breakdown ─────────────────
        Sec(sb, "5. Detailed Per-Attack Breakdown");

        foreach (var test in report.StressTests)
        {
            sb.AppendLine($"  ── {test.Name} ──");
            sb.AppendLine($"  Hypothesis: {test.Hypothesis}");
            sb.AppendLine($"  R²(dR/dt): {test.R2_dRdt:F4}  MAE: {test.MeanAbsError:F5}");

            if (test.Data.Count > 0)
            {
                double minR = test.Data.Min(d => d.R);
                double maxR = test.Data.Max(d => d.R);
                double minM = test.Data.Min(d => d.M);
                double maxM = test.Data.Max(d => d.M);
                sb.AppendLine($"  R range: [{minR:F4}, {maxR:F4}]  M range: [{minM:F6}, {maxM:F6}]");
            }

            string status = test.TheoryFailed ? "⚠ THEORY FAILED" : "✓ THEORY HOLDS";
            sb.AppendLine($"  Verdict: {status}");
            sb.AppendLine($"  Failure mode: {test.FailureMode}");
            sb.AppendLine();
        }

        // ── Section 6: Research Questions ────────────────────────────
        Sec(sb, "6. Research Questions");

        sb.AppendLine("  Q1: Where does the minimal theory break?");
        if (report.Failures.Count == 0)
            sb.AppendLine("    NOWHERE — the theory survived all 8 attack vectors.");
        else
        {
            foreach (var f in report.Failures)
                sb.AppendLine($"    {f.Scenario}: {f.Interpretation}");
        }
        sb.AppendLine();

        sb.AppendLine("  Q2: What phenomena remain unexplained?");
        sb.AppendLine("    The theory predicts dR/dt from {R, M}. It does NOT predict:");
        sb.AppendLine("    • dM/dt itself (M is assumed given, or predicted weakly at R²≈0.30)");
        sb.AppendLine("    • Identity dynamics (independent dimension, TQM-047)");
        sb.AppendLine("    • Energy dynamics (independent dimension, TQM-047)");
        sb.AppendLine("    • Condensate interactions (identity exclusion, TQM-050)");
        sb.AppendLine("    • Spatial motion (coupling-driven, TQM-062)");
        sb.AppendLine();

        sb.AppendLine("  Q3: Is a third state variable required?");
        if (report.FailuresDetected >= 3)
            sb.AppendLine("    YES — the failure pattern suggests a missing variable.");
        else if (report.FailuresDetected >= 1)
            sb.AppendLine("    POSSIBLY — isolated failures hint at missing physics in specific regimes.");
        else
            sb.AppendLine("    NO — {R, M} is sufficient across all tested regimes.");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does the theory generalize outside its training regime?");
        double oodScore = report.StressTests
            .Where(t => t.Name is "Different Coupling Laws" or "Out-of-Distribution"
                         or "Large-N Scaling" or "Small-N Scaling")
            .Average(t => t.TheoryFailed ? 0.0 : 1.0);
        sb.AppendLine($"    Out-of-distribution survival rate: {oodScore:P0}");
        sb.AppendLine($"    {(oodScore >= 0.75 ? "YES — strong generalization." : oodScore >= 0.50 ? "PARTIALLY — moderate generalization." : "NO — theory is regime-specific.")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can the theory survive deliberate falsification attempts?");
        sb.AppendLine($"    Attacks survived: {report.TotalAttackVectors - report.FailuresDetected}/{report.TotalAttackVectors}");
        sb.AppendLine($"    Generalization score: {report.GeneralizationScore:P0}");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine();

        // ── Section 7: Verdict ───────────────────────────────────────
        Sec(sb, "7. Verdict");

        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();
        sb.AppendLine("  ── Detailed Assessment ──");

        // Model coefficients for reference.
        sb.AppendLine($"  Training R²(dR/dt): {model.R2_DRdt:F4}");
        sb.AppendLine($"  dR/dt = {model.DRdtCoeffs[0]:+0.0000;-0.0000} " +
                   $"+ {model.DRdtCoeffs[1]:+0.0000;-0.0000}·R " +
                   $"+ {model.DRdtCoeffs[2]:+0.0000;-0.0000}·M");
        sb.AppendLine();

        // Score breakdown.
        sb.AppendLine("  ── Decision Matrix ──");
        sb.AppendLine("  Criterion                              │ Pass?");
        sb.AppendLine("  " + new string('─', 65));
        sb.AppendLine($"  Generalizes to extreme R               │ {(report.StressTests[0].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Generalizes to extreme M               │ {(report.StressTests[1].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Generalizes across topologies          │ {(report.StressTests[2].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Generalizes to different coupling laws │ {(report.StressTests[3].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Robust to phase noise                  │ {(report.StressTests[4].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Scales to larger N                     │ {(report.StressTests[5].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Scales to smaller N                    │ {(report.StressTests[6].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine($"  Generalizes OOD                        │ {(report.StressTests[7].TheoryFailed ? "FAIL" : "PASS")}");
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1.  Attacks: {report.TotalAttackVectors}");
        sb.AppendLine($"  C2.  Survived: {report.TotalAttackVectors - report.FailuresDetected}");
        sb.AppendLine($"  C3.  Failed: {report.FailuresDetected}");
        sb.AppendLine($"  C4.  Generalization: {report.GeneralizationScore:P0}");
        sb.AppendLine($"  C5.  Classification: {report.Classification}");
        sb.AppendLine($"  C6.  Training R²: {model.R2_DRdt:F4}");
        sb.AppendLine();

        if (report.FailuresDetected == 0)
        {
            sb.AppendLine("  C7.  CONCLUSION: THE THEORY IS ROBUST.");
            sb.AppendLine("       The {R, M} minimal theory successfully survived");
            sb.AppendLine("       deliberate hostile review across 8 attack vectors.");
            sb.AppendLine("       This is strong evidence that {R, M} captures");
            sb.AppendLine("       the essential physics of the TQM system.");
        }
        else
        {
            sb.AppendLine($"  C7.  CONCLUSION: {report.FailuresDetected} GAPS FOUND.");
            sb.AppendLine("       The {R, M} theory has known failure modes that");
            sb.AppendLine("       must be addressed before it can be considered");
            sb.AppendLine("       a complete candidate for emergent physics.");
        }
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-100 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
