using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_068_CurvatureCouplingToSpatialDynamics : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 680514839;
    private static readonly double[] Betas = { 0.0, 0.05, 0.10, 0.20, 0.50, 1.00, 2.00 };
    private const int SeedsPerBeta = 2;
    private const int TotalIters = 3000;
    private const int SnapshotInterval = 200;

    public AT_068_CurvatureCouplingToSpatialDynamics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_068_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-068 Curvature Coupling to Spatial Dynamics");

        sb.AppendLine("AT-068: Does Memory-Generated Curvature Influence Spatial Motion?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-059: Memory strongly predicts curvature.");
        sb.AppendLine("  AT-062: Spatial attraction arises from position dynamics.");
        sb.AppendLine("  AT-067: Synchronization remains stable at high K.");
        sb.AppendLine();
        sb.AppendLine("  Unresolved: Does curvature affect spatial motion?");
        sb.AppendLine("  Or are curvature and motion independent?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: If curvature has physical significance,");
        sb.AppendLine("  increasing curvature should measurably alter");
        sb.AppendLine("  attraction strength, convergence speed,");
        sb.AppendLine("  trajectory geometry, and acceleration.");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  \u03B2 sweep: [{string.Join(", ", Betas)}]");
        sb.AppendLine($"  Seeds per \u03B2: {SeedsPerBeta}");
        sb.AppendLine($"  Total simulations: {Betas.Length * SeedsPerBeta}");
        sb.AppendLine($"  N = {NPerGroup * 2}, K = {K}, \u03BB = {Lambda}");
        sb.AppendLine($"  Iterations: {TotalIters}, snapshots every {SnapshotInterval}");
        sb.AppendLine($"  Group A: center (0.3, 0.5), Group B: center (0.7, 0.5)");
        sb.AppendLine($"  Spatial coupling law: K\u00b7exp(-d/\u03BB) — FIXED for all \u03B2");
        sb.AppendLine($"  Identical initial positions, identical K, identical histories.");
        sb.AppendLine($"  Only memory strength \u03B2 varies.");
        sb.AppendLine();
        sb.AppendLine("  Curvature measured at each snapshot via");
        sb.AppendLine("  geodesic deviation (2 perturbations, 200-step recovery).");
        sb.AppendLine();

        // ── Run simulations ──────────────────────────────────────────
        var bag = new ConcurrentBag<(List<CurvatureMotionAnalyzer.MotionProfile> Profiles, double Beta)>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, Betas.Length * SeedsPerBeta, idx =>
        {
            int bi = idx / SeedsPerBeta, si = idx % SeedsPerBeta;
            double beta = Betas[bi];
            int seed = BaseSeed + idx * 7919;
            bag.Add(CurvatureMotionAnalyzer.RunProfile(
                beta, K, Lambda, NPerGroup, seed, TotalIters, SnapshotInterval));
        });

        sw.Stop();
        var allProfiles = bag.ToList();
        sb.AppendLine($"  Completed {allProfiles.Count} simulations in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Correlation analysis ─────────────────────────────────────
        var corr = CurvatureMotionAnalyzer.Analyze(allProfiles);

        // ── Section 3: Curvature Measurements ────────────────────────
        Sec(sb, "3. Curvature Measurements");
        sb.AppendLine("  \u03B2      │ Mean Curv │ Curv Std  │ Max Curv  │ Min Curv");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (double beta in Betas)
        {
            var group = allProfiles.Where(p => Math.Abs(p.Beta - beta) < 0.001)
                .SelectMany(p => p.Profiles.Where(m => m.Iteration > 0)).ToList();
            double mc = group.Average(m => m.Curvature);
            double sc = group.Count > 1 ? Math.Sqrt(group.Average(m =>
                (m.Curvature - mc) * (m.Curvature - mc))) : 0;
            double maxC = group.Max(m => m.Curvature);
            double minC = group.Min(m => m.Curvature);
            sb.AppendLine($"  {beta,5:F2} │ {mc,8:F4} │ {sc,8:F4} │ {maxC,8:F4} │ {minC,8:F4}");
        }
        sb.AppendLine();
        sb.AppendLine($"  \u03B2-curvature correlation: r = {corr.BetaCurvatureR:F4}");
        sb.AppendLine();

        // ── Section 4: Motion Measurements ───────────────────────────
        Sec(sb, "4. Motion Measurements");
        sb.AppendLine("  \u03B2      │ Mean Vel  │ Max Vel   │ Mean Accel│ Final Sep │ Conv Rate │ Drift A   │ Drift B");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (double beta in Betas)
        {
            var group = allProfiles.Where(p => Math.Abs(p.Beta - beta) < 0.001).ToList();
            var snapshots = group.SelectMany(p =>
                p.Profiles.Where(m => m.Iteration > 0)).ToList();

            double mv = snapshots.Average(m => (m.VelocityA + m.VelocityB) / 2);
            double maxV = snapshots.Max(m => (m.VelocityA + m.VelocityB) / 2);
            double ma = snapshots.Average(m => (m.AccelerationA + m.AccelerationB) / 2);

            // Final separation: average across seeds.
            double fs = group.Average(g =>
            {
                var last = g.Profiles.Where(m => m.Iteration > 0).ToList();
                return last.Any() ? last[^1].Separation : 0;
            });

            double cr = snapshots.Average(m => m.ConvergenceRate);

            // Drift: displacement from first to last snapshot.
            double driftA = group.Average(g =>
            {
                var sn = g.Profiles.Where(m => m.Iteration > 0).ToList();
                if (sn.Count < 2) return 0;
                return Math.Sqrt(Math.Pow(sn[^1].CenterX_A - sn[0].CenterX_A, 2) +
                                 Math.Pow(sn[^1].CenterY_A - sn[0].CenterY_A, 2));
            });
            double driftB = group.Average(g =>
            {
                var sn = g.Profiles.Where(m => m.Iteration > 0).ToList();
                if (sn.Count < 2) return 0;
                return Math.Sqrt(Math.Pow(sn[^1].CenterX_B - sn[0].CenterX_B, 2) +
                                 Math.Pow(sn[^1].CenterY_B - sn[0].CenterY_B, 2));
            });

            sb.AppendLine($"  {beta,5:F2} │ {mv,8:F4} │ {maxV,8:F4} │ {ma,8:F4} │ {fs,8:F4} │ {cr,8:F4} │ {driftA,8:F4} │ {driftB,8:F4}");
        }
        sb.AppendLine();
        sb.AppendLine($"  \u03B2-drift correlation: r = {corr.BetaDriftR:F4}");
        sb.AppendLine();

        // ── Section 5: Correlation Analysis ──────────────────────────
        Sec(sb, "5. Correlation Analysis");
        sb.AppendLine($"  r(curvature, velocity):     {corr.CurvatureVelocityR,8:F4}");
        sb.AppendLine($"  r(curvature, acceleration): {corr.CurvatureAccelerationR,8:F4}");
        sb.AppendLine($"  r(curvature, convergence):  {corr.CurvatureConvergenceR,8:F4}");
        sb.AppendLine($"  r(\u03B2, curvature):          {corr.BetaCurvatureR,8:F4}");
        sb.AppendLine($"  r(\u03B2, drift):              {corr.BetaDriftR,8:F4}");
        sb.AppendLine();

        // Per-beta detail.
        sb.AppendLine("  \u03B2      │ Curvature │ Velocity  │ Accel     │ Final Sep │ Conv Rate");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var s in corr.Summary)
            sb.AppendLine($"  {s.Beta,5:F2} │ {s.MeanCurvature,8:F4} │ {s.MeanVelocity,8:F4} │ {s.MeanAccel,8:F4} │ {s.FinalSeparation,8:F4} │ {s.ConvergenceRate,8:F4}");
        sb.AppendLine();

        // Research questions.
        sb.AppendLine("  Q1: Does curvature predict spatial motion?");
        sb.AppendLine($"    r(curvature, velocity) = {corr.CurvatureVelocityR:F4}");
        sb.AppendLine($"    {(Math.Abs(corr.CurvatureVelocityR) > 0.15 ? "YES — Curvature correlates with spatial velocity" : "NO — Curvature does not predict velocity")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: Does higher curvature increase attraction?");
        sb.AppendLine($"    r(curvature, convergence) = {corr.CurvatureConvergenceR:F4}");
        string q2 = Math.Abs(corr.CurvatureConvergenceR) switch
        {
            > 0.3 => "YES — Higher curvature significantly increases convergence",
            > 0.15 => "WEAKLY — Curvature has a modest effect on attraction",
            _ => "NO — Curvature does not increase attraction"
        };
        sb.AppendLine($"    {q2}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Do trajectory shapes change with curvature?");
        // Check if final positions differ significantly across β.
        var finalSeps = corr.Summary.Select(s => s.FinalSeparation).ToList();
        double sepRange = finalSeps.Max() - finalSeps.Min();
        sb.AppendLine($"    Final separation range across \u03B2: {sepRange:F4}");
        sb.AppendLine($"    {(sepRange > 0.02 ? "YES — Trajectories diverge with \u03B2" : "NO — Trajectory shapes are \u03B2-invariant")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Can motion be predicted from curvature alone?");
        double maxAbs = Math.Max(Math.Max(Math.Abs(corr.CurvatureVelocityR),
            Math.Abs(corr.CurvatureAccelerationR)), Math.Abs(corr.CurvatureConvergenceR));
        sb.AppendLine($"    Max |r| = {maxAbs:F4}");
        sb.AppendLine($"    {(maxAbs > 0.5 ? "YES — Curvature is a strong predictor of motion" : maxAbs > 0.3 ? "PARTIALLY — Curvature predicts some motion features" : "NO — Curvature alone cannot predict motion")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Are curvature and spatial dynamics independent?");
        sb.AppendLine($"    {(maxAbs < 0.15 ? "YES — Curvature and motion appear independent" : "NO — They are coupled")}");
        sb.AppendLine();

        // ── Section 6: Trajectory Analysis ───────────────────────────
        Sec(sb, "6. Trajectory Analysis");

        // Sample trajectories for β = 0, 0.5, 2.0.
        foreach (double beta in new[] { 0.0, 0.5, 2.0 })
        {
            var sample = allProfiles
                .Where(p => Math.Abs(p.Beta - beta) < 0.001)
                .OrderBy(p => p.Profiles[0].Iteration)
                .FirstOrDefault();
            if (sample.Profiles == null) continue;

            sb.AppendLine($"  Trajectory for \u03B2 = {beta}:");
            sb.AppendLine("  Iter │ X_A    Y_A    │ X_B    Y_B    │ Sep     │ Curv    │ Vel     │ R_A    R_B");
            sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

            foreach (var m in sample.Profiles)
            {
                double vel = (m.VelocityA + m.VelocityB) / 2;
                sb.AppendLine($"  {m.Iteration,4} │ {m.CenterX_A,6:F3} {m.CenterY_A,6:F3} │ {m.CenterX_B,6:F3} {m.CenterY_B,6:F3} │ {m.Separation,7:F4} │ {m.Curvature,7:F4} │ {vel,7:F4} │ {m.R_A,5:F3}  {m.R_B,5:F3}");
            }
            sb.AppendLine();
        }

        // ── Section 7: Interpretation ────────────────────────────────
        Sec(sb, "7. Interpretation");
        sb.AppendLine($"  Classification: {corr.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {corr.Interpretation}");
        sb.AppendLine();

        // Detailed breakdown.
        sb.AppendLine("  Evidence summary:");
        sb.AppendLine($"    \u03B2 \u2192 curvature:      r = {corr.BetaCurvatureR:F4} " +
            (Math.Abs(corr.BetaCurvatureR) > 0.3 ? "(strong)" : "(weak)"));
        sb.AppendLine($"    curvature \u2192 velocity:  r = {corr.CurvatureVelocityR:F4} " +
            (Math.Abs(corr.CurvatureVelocityR) > 0.15 ? "(significant)" : "(negligible)"));
        sb.AppendLine($"    curvature \u2192 accel:     r = {corr.CurvatureAccelerationR:F4} " +
            (Math.Abs(corr.CurvatureAccelerationR) > 0.15 ? "(significant)" : "(negligible)"));
        sb.AppendLine($"    curvature \u2192 converge:  r = {corr.CurvatureConvergenceR:F4} " +
            (Math.Abs(corr.CurvatureConvergenceR) > 0.15 ? "(significant)" : "(negligible)"));
        sb.AppendLine();

        // Physical interpretation.
        string physicalInterpretation = corr.Classification switch
        {
            "A: No Coupling" =>
                "Curvature is a geometric epiphenomenon — it exists but does not " +
                "exert forces. Spatial dynamics are governed entirely by coupling " +
                "gradients, which are determined by phase alignment, not by the " +
                "intrinsic curvature of the state space. Memory (β) affects phases " +
                "but the resulting curvature has no feedback on positions.",
            "B: Weak Coupling" =>
                "Curvature has a detectable but subdominant influence on spatial " +
                "motion. The primary driver of positions remains the direct coupling " +
                "gradient, but curvature provides a small correction. This may be " +
                "analogous to how spacetime curvature in GR provides corrections to " +
                "Newtonian trajectories in weak fields.",
            "C: Strong Coupling" or "D: Curvature Dominated Motion" =>
                "Curvature significantly shapes spatial dynamics. Memory-generated " +
                "geometry is not a passive side effect — it actively channels " +
                "condensate motion. This would mean the state-space manifold has " +
                "physical content, and curvature acts as an effective force field.",
            _ => "Curvature-motion relationship classification is inconclusive."
        };
        sb.AppendLine($"  Physical interpretation: {physicalInterpretation}");
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1. Classification: {corr.Classification}");
        sb.AppendLine($"  C2. r(curvature, velocity):     {corr.CurvatureVelocityR:F4}");
        sb.AppendLine($"  C3. r(curvature, acceleration): {corr.CurvatureAccelerationR:F4}");
        sb.AppendLine($"  C4. r(curvature, convergence):  {corr.CurvatureConvergenceR:F4}");
        sb.AppendLine($"  C5. r(β, curvature):           {corr.BetaCurvatureR:F4}");
        sb.AppendLine($"  C6. r(β, drift):               {corr.BetaDriftR:F4}");
        sb.AppendLine();
        sb.AppendLine($"  C7. Curvature-motion relationship: {corr.Classification}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-068 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
