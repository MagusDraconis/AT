using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_076_PredictiveTrajectoryTheory : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 760123847;
    private static readonly double[] RTargets = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 0.95 };
    private static readonly string[] Laws = { "cos", "cos²", "exp(-|x|)", "1/(1+|x|)" };
    private const int SeedsPerPoint = 2;

    public AT_076_PredictiveTrajectoryTheory(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_076_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-076 Predictive Trajectory Theory");

        sb.AppendLine("AT-076: Can Future Trajectories Be Predicted from Initial Conditions?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-074: F_net = Alignment × ⟨f⟩ (R²=0.9895).");
        sb.AppendLine("  AT-075: Alignment ≈ R² (R²=0.942).");
        sb.AppendLine();
        sb.AppendLine("  The remaining question: can we predict the SPATIAL");
        sb.AppendLine("  TRAJECTORY from initial phase conditions?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: The full trajectory is largely determined");
        sb.AppendLine("  by the initial phase distribution. Given initial");
        sb.AppendLine("  R, alignment, and mean force, we can predict");
        sb.AppendLine("  the short-term spatial evolution.");
        sb.AppendLine();

        // ── Section 2: Prediction Model ──────────────────────────────
        Sec(sb, "2. Prediction Model");
        sb.AppendLine("  Given at t=0:");
        sb.AppendLine("    R₀ = order parameter");
        sb.AppendLine("    A₀ = force alignment");
        sb.AppendLine("    ⟨f⟩₀ = mean local force magnitude");
        sb.AppendLine();
        sb.AppendLine("  Predicted net force:");
        sb.AppendLine("    F_pred = a · A₀ · ⟨f⟩₀");
        sb.AppendLine("    (a ≈ 2524 from AT-074 calibration)");
        sb.AppendLine();
        sb.AppendLine("  Predicted separation after T steps:");
        sb.AppendLine("    Δsep_pred = -posStep · T · F_pred");
        sb.AppendLine();
        sb.AppendLine($"  R targets: [{string.Join(", ", RTargets)}]");
        sb.AppendLine($"  Laws: {string.Join(", ", Laws)}");
        sb.AppendLine($"  Total tests: {RTargets.Length * Laws.Length * SeedsPerPoint}");
        sb.AppendLine($"  50-step prediction window");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var (profiles, report) = TrajectoryPredictor.RunPredictionSweep(
            RTargets, Laws, K, Lambda, NPerGroup, SeedsPerPoint, BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed {profiles.Count} predictions in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // Fit scale factor (computed before display to use in sample).
        double[] feat = profiles.Select(p => p.PredictedNetForce).ToArray();
        double[] obsF2 = profiles.Select(p => p.ObservedNetForce).ToArray();
        double sxy = 0, sx2 = 0;
        for (int i = 0; i < feat.Length; i++) { sxy += feat[i] * obsF2[i]; sx2 += feat[i] * feat[i]; }
        double scale = sx2 > 1e-15 ? sxy / sx2 : 0;

        // ── Section 3: Initial Conditions ────────────────────────────
        Sec(sb, "3. Initial Conditions & Predictions (Sample)");

        sb.AppendLine("  Law   │ R₀     │ A₀     │ ⟨f⟩₀   │ F_pred  │ F_obs   │ Δsep_pred│ Δsep_obs");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.Where(p => p.LawName == "cos").Take(10))
        {
            double fpred = scale * p.PredictedNetForce;
            double spred = -0.001 * 50 * fpred;
            sb.AppendLine($"  {p.LawName,-4} │ {p.InitialR,5:F2} │ {p.InitialAlignment,5:F3} │ {p.InitialMeanForce,5:F4} │ {fpred,6:F2} │ {p.ObservedNetForce,6:F2} │ {spred,8:F4} │ {p.ObservedSepChange,8:F4}");
        }

        sb.AppendLine($"  ... ({profiles.Count(p => p.LawName == "cos")} cos tests) ...");
        sb.AppendLine();

        // ── Section 4: Trajectory Accuracy ───────────────────────────
        Sec(sb, "4. Prediction Accuracy");

        sb.AppendLine($"  Calibrated scale factor: a = {scale:F1}");
        sb.AppendLine($"  Force prediction R²:           {report.ForceR2:F4}");
        sb.AppendLine($"  Velocity prediction R²:        {report.VelocityR2:F4}");
        sb.AppendLine($"  Sep-change prediction R²:      {report.SepChangeR2:F4}");
        sb.AppendLine($"  Mean relative error:           {report.MeanError:P1}");
        sb.AppendLine($"  Alignment contribution (ΔR²):  {report.AlignmentContribution:+0.0000}");
        sb.AppendLine();

        // Per-law breakdown.
        sb.AppendLine("  Law          │ Force R² │ SepCh R² │ Mean Err");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (string law in Laws)
        {
            var sub = profiles.Where(p => p.LawName == law).ToList();
            if (sub.Count < 3) continue;
            double[] sf = sub.Select(p => p.PredictedNetForce).ToArray();
            double[] sof = sub.Select(p => p.ObservedNetForce).ToArray();
            // Per-law scale.
            double sxyL = 0, sx2L = 0;
            for (int i = 0; i < sf.Length; i++) { sxyL += sf[i] * sof[i]; sx2L += sf[i] * sf[i]; }
            double sL = sx2L > 1e-15 ? sxyL / sx2L : 0;
            double[] pf = sf.Select(x => sL * x).ToArray();
            double[] ps = pf.Select(f => -0.001 * 50 * f).ToArray();
            double[] os = sub.Select(p => p.ObservedSepChange).ToArray();
            double fr2 = R2(pf, sof);
            double sr2 = R2(ps, os);
            double me = sub.Average(p =>
            {
                double fp = sL * p.PredictedNetForce;
                double psc = -0.001 * 50 * fp;
                return Math.Abs(psc - p.ObservedSepChange) /
                       Math.Max(Math.Abs(p.ObservedSepChange), 1e-10);
            });
            sb.AppendLine($"  {law,-11} │ {fr2,7:F4} │ {sr2,7:F4} │ {me,7:P1}");
        }
        sb.AppendLine();

        // ── Section 5: Alignment Contribution ────────────────────────
        Sec(sb, "5. Alignment vs R-Only Prediction");

        // Compare F ~ R only vs F ~ A · ⟨f⟩.
        double[] allR = profiles.Select(p => p.InitialR).ToArray();
        double[] allF = profiles.Select(p => p.ObservedNetForce).ToArray();
        double rOnlyR2 = R2(allR, allF);

        sb.AppendLine($"  R-only model R²:     {rOnlyR2:F4}");
        sb.AppendLine($"  Full model R²:       {report.ForceR2:F4}");
        sb.AppendLine($"  Alignment adds:      {report.AlignmentContribution:+0.0000}");
        sb.AppendLine();

        sb.AppendLine("  Q1: Can trajectory be predicted from initial conditions?");
        sb.AppendLine($"    Force R² = {report.ForceR2:F4}");
        sb.AppendLine($"    {(report.ForceR2 > 0.80 ? "YES — Strong predictive power" : report.ForceR2 > 0.50 ? "PARTIALLY — Moderate prediction" : "NO — Initial conditions insufficient")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: How much variance is explained?");
        sb.AppendLine($"    Sep-change R² = {report.SepChangeR2:F4} ({report.SepChangeR2 * 100:F0}%)");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does Alignment improve prediction?");
        sb.AppendLine($"    ΔR² = {report.AlignmentContribution:+0.0000}");
        sb.AppendLine($"    {(report.AlignmentContribution > 0.05 ? "YES — Alignment significantly improves prediction" : "NO — Alignment adds marginal value beyond R")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Is R alone sufficient?");
        sb.AppendLine($"    R-only R² = {rOnlyR2:F4}");
        sb.AppendLine($"    {(rOnlyR2 > 0.80 ? "YES — R alone predicts trajectory well" : "NO — Full model (A·⟨f⟩) is needed")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can a universal trajectory law be derived?");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine($"    {(report.SepChangeR2 > 0.90 ? "YES — Universal trajectory law found" : report.SepChangeR2 > 0.70 ? "PARTIALLY — Strong but not universal" : "NO — No universal law")}");
        sb.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        Sec(sb, "6. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  The prediction chain:");
        sb.AppendLine($"    Initial P(θ) → R₀, A₀, ⟨f⟩₀ → F_pred → Δsep_pred");
        sb.AppendLine($"    Force R² = {report.ForceR2:F4}");
        sb.AppendLine($"    Trajectory R² = {report.SepChangeR2:F4}");
        sb.AppendLine();

        if (report.SepChangeR2 > 0.70)
        {
            sb.AppendLine("    The three-level theory successfully PREDICTS");
            sb.AppendLine("    the short-term spatial evolution. Given only");
            sb.AppendLine("    the initial phase distribution, we can compute");
            sb.AppendLine("    where the condensates will move.");
        }
        else
        {
            sb.AppendLine("    The three-level theory provides partial prediction.");
            sb.AppendLine("    Phase evolution during the trajectory introduces");
            sb.AppendLine("    unpredictability that initial conditions cannot");
            sb.AppendLine("    fully capture.");
        }
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Force prediction R²: {report.ForceR2:F4}");
        sb.AppendLine($"  C3. Sep-change prediction R²: {report.SepChangeR2:F4}");
        sb.AppendLine($"  C4. Mean error: {report.MeanError:P1}");
        sb.AppendLine($"  C5. Alignment contribution: {report.AlignmentContribution:+0.0000}");
        sb.AppendLine($"  C6. Total predictions: {profiles.Count}");
        sb.AppendLine();
        sb.AppendLine($"  C7. {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-076 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static double R2(double[] pred, double[] obs)
    {
        int n = pred.Length;
        double ssRes = 0, ssTot = 0, m = obs.Average();
        for (int i = 0; i < n; i++)
        { ssRes += (obs[i] - pred[i]) * (obs[i] - pred[i]); ssTot += (obs[i] - m) * (obs[i] - m); }
        return ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
