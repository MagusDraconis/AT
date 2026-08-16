using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-E Phase 1 — test curvature–density feedback. Starting from R = F(ρ), Ṙ = F′(ρ)·ρ̇,
/// close the loop with three feedback models (ρ̇ = −kR, ρ̇ = −k·sign(R), ρ̇ = −k·R·ρ) and
/// characterize the self-consistent dynamics: fixed points, stability, oscillation, runaway.
///
/// Tests: G4-E10 (fixed point + stability), G4-E11 (runaway vs oscillation),
///        G4-E12 (self-consistent anti-diffusive dynamics).
/// </summary>
public class G4E_Phase1_FeedbackDynamicsTests : ResearchTestBase
{
    public G4E_Phase1_FeedbackDynamicsTests(ITestOutputHelper o) : base(o) { }

    private const double K = 1.0;     // feedback gain
    private const double Dt = 0.05;   // discrete time step
    private const int T = 200;        // simulation steps

    private static readonly (string Name, FeedbackModel Model)[] Models =
    {
        ("linear  (ρ̇=−kR)",        FeedbackModel.Linear),
        ("sign    (ρ̇=−k·sgn R)",   FeedbackModel.Sign),
        ("product (ρ̇=−kRρ)",       FeedbackModel.Product),
    };

    // Build the native F map once (deterministic) and reuse across all three tests.
    private static readonly Lazy<(double[] rho, double[] score)> Map =
        new(() => CurvatureFeedback.BuildMap());

    private static double[] Sim(FeedbackModel m, double rho0)
        => CurvatureFeedback.Simulate(Map.Value.rho, Map.Value.score, m, K, Dt, T, rho0);

    // ── G4-E10: fixed point and stability ──────────────────────────────────────────────

    [Fact]
    public void G4_E10_FixedPointAndStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-E10: fixed point and stability of curvature–density feedback");

        var (rmap, smap) = Map.Value;
        double fFlat = CurvatureFeedback.Interpolate(rmap, smap, 1.0);
        double slope = CurvatureFeedback.SlopeAtFlat(rmap, smap);

        sb.AppendLine($"Native F map: {rmap.Length} points, ρ̄ ∈ [{rmap[0]:F4}, {rmap[^1]:F4}], " +
                      $"R̂ ∈ [{smap[^1]:F3}, {smap[0]:F3}].");
        sb.AppendLine($"F(1) = {fFlat:F6}   (flat fixed point: R = 0 at ρ̄ = 1).");
        sb.AppendLine($"F′(1) = {slope:F2}  (< 0: curvature decreases with density).");
        sb.AppendLine();
        sb.AppendLine($"Linearization eigenvalue λ = −k·F′(1) = {-K * slope:F2} > 0  ⇒  flat is UNSTABLE.");
        sb.AppendLine("Model ρ̇=−k·sign(R): piecewise-constant repulsion (no linearization).");
        sb.AppendLine();

        sb.AppendLine("Divergence from flat (ρ₀ = 1 ± 0.02, T=200):");
        sb.AppendLine($"{"model",-20} {"ρ₀",7} {"ρ_T",10}  |ρ_T−1|>|ρ₀−1|");
        bool allUnstable = true;
        foreach (var (name, model) in Models)
        {
            foreach (double r0 in new[] { 0.98, 1.02 })
            {
                var rho = Sim(model, r0);
                double dev0 = Math.Abs(r0 - 1.0), devT = Math.Abs(rho[^1] - 1.0);
                bool div = devT > dev0;
                if (!div) allUnstable = false;
                sb.AppendLine($"{name,-20} {r0,7:F2} {rho[^1],10:F3}  {div}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"All feedback models repel trajectories from the flat fixed point: {allUnstable}.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: flat (ρ̄=1) is the unique curvature-neutral fixed point and is UNSTABLE");
        sb.AppendLine("for all three feedback models, because F′(1) < 0.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(fFlat) < 1e-6, $"F(1) = {fFlat} not zero");
        Assert.True(slope < 0.0, $"F′(1) = {slope} not negative");
        Assert.True(allUnstable, "flat fixed point not repelling for some model");
    }

    // ── G4-E11: runaway vs oscillation ─────────────────────────────────────────────────

    [Fact]
    public void G4_E11_RunawayVersusOscillation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-E11: runaway vs oscillation across feedback models");

        double[] r0s = { 0.85, 0.95, 1.05, 1.15 };

        sb.AppendLine($"Simulate {Models.Length} models × {r0s.Length} initial conditions (T={T} steps).");
        sb.AppendLine();
        sb.AppendLine($"{"model",-20} {"ρ₀",7} {"ρ_T",10} {"class",-12}  away-from-flat");
        int oscCount = 0;
        bool allAway = true;
        foreach (var (name, model) in Models)
        {
            foreach (double r0 in r0s)
            {
                var rho = Sim(model, r0);
                string cls = CurvatureFeedback.Classify(rho);
                if (cls == "oscillatory") oscCount++;
                bool away = Math.Abs(rho[^1] - 1.0) >= Math.Abs(r0 - 1.0);
                if (!away) allAway = false;
                sb.AppendLine($"{name,-20} {r0,7:F2} {rho[^1],10:F3} {cls,-12}  {away}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"Oscillatory trajectories: {oscCount}/{Models.Length * r0s.Length}  (expect 0).");
        sb.AppendLine($"All trajectories move away from flat: {allAway}.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all three models produce monotonic RUNAWAY (no oscillation); the");
        sb.AppendLine("product model ρ̇=−kRρ additionally converges to the unphysical ρ=0 fixed point");
        sb.AppendLine("from below flat (curvature-neutral density is driven toward zero density).");
        Output.WriteLine(sb.ToString());

        Assert.True(oscCount == 0, $"{oscCount} oscillatory trajectories detected");
        Assert.True(allAway, "some trajectory was attracted back toward flat");
    }

    // ── G4-E12: self-consistent anti-diffusive dynamics ────────────────────────────────

    [Fact]
    public void G4_E12_SelfConsistentAntiDiffusiveDynamics()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-E12: self-consistent, anti-diffusive curvature–density dynamics");

        var (rmap, smap) = Map.Value;
        double slope = CurvatureFeedback.SlopeAtFlat(rmap, smap);
        double[] r0s = { 0.85, 0.95, 1.05, 1.15 };

        int total = 0, antidiff = 0;
        foreach (var (_, model) in Models)
        {
            foreach (double r0 in r0s)
            {
                var rho = Sim(model, r0);
                for (int t = 0; t < rho.Length - 1; t++)
                {
                    double d = rho[t + 1] - rho[t];
                    if (Math.Abs(d) < 1e-12) continue; // converged step
                    total++;
                    if (Math.Sign(d) == Math.Sign(rho[t] - 1.0)) antidiff++;
                }
            }
        }

        sb.AppendLine($"F′(1) = {slope:F2} < 0. For every model ρ̇ = −kR (or −k·sgn R, −kRρ):");
        sb.AppendLine($"  sign(ρ̇) = sign(ρ − 1) — the feedback AMPLIFIES the density deviation that created it.");
        sb.AppendLine();
        sb.AppendLine($"Anti-diffusive steps: {antidiff}/{total}  ({100.0 * antidiff / total:F1} %).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the closed system ρ → R = F(ρ) → ρ̇ is self-consistent but anti-diffusive");
        sb.AppendLine("(positive feedback): flat is unstable and trajectories run away. A bounded, non-runaway");
        sb.AppendLine("cosmology therefore requires an ADDITIONAL restoring term — the naive native feedback");
        sb.AppendLine("alone is curvature-amplifying.");
        Output.WriteLine(sb.ToString());

        Assert.True(antidiff == total, $"anti-diffusive only {antidiff}/{total} steps");
    }
}
