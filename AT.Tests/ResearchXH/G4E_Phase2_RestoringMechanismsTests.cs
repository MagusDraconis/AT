using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-E Phase 2 — search for a native restoring mechanism. Starting from the anti-diffusive
/// feedback ρ̇ = −kR (Phase 1), add primitive-native restoring terms and test whether they
/// stabilize ρ around flat: (1) diffusion −d(ρ−1), (2) logistic −c(ρ−1)³,
/// (3) conservation mean(ρ)=1. Measures fixed points, stability, oscillation, boundedness.
/// Result: diffusion (d &gt; k|F′(1)|) stabilizes flat; logistic gives bistable finite
/// attractors; conservation pins flat degenerately.
///
/// Tests: G4-E20 (diffusion), G4-E21 (logistic), G4-E22 (conservation + comparison).
/// </summary>
public class G4E_Phase2_RestoringMechanismsTests : ResearchTestBase
{
    public G4E_Phase2_RestoringMechanismsTests(ITestOutputHelper o) : base(o) { }

    private const double K = 1.0;     // feedback gain
    private const double Dt = 0.05;   // discrete time step
    private const int T = 300;        // simulation steps

    private static readonly Lazy<(double[] rho, double[] score)> Map =
        new(() => CurvatureFeedback.BuildMap());

    private static double[] Sim(RestoringTerm term, double strength, double rho0)
        => CurvatureFeedback.SimulateRestoring(Map.Value.rho, Map.Value.score, K, Dt, T, rho0, term, strength);

    private static bool Converged(double[] rho) => Math.Abs(rho[^1] - rho[^2]) < 1e-6;

    // ── G4-E20: diffusion term stabilizes flat above a critical strength ───────────────

    [Fact]
    public void G4_E20_DiffusionTermStabilizesFlat()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-E20: diffusion term −d(ρ−1) stabilizes flat above a critical strength");

        var (rmap, smap) = Map.Value;
        double slope = CurvatureFeedback.SlopeAtFlat(rmap, smap);
        double dStar = K * Math.Abs(slope);

        sb.AppendLine($"F′(1) = {slope:F2}  ⇒  critical diffusion d* = k·|F′(1)| = {dStar:F2}.");
        sb.AppendLine("Linearization: λ = k|F′(1)| − d.  d < d* ⇒ flat unstable;  d > d* ⇒ flat stable.");
        sb.AppendLine();
        sb.AppendLine($"{"d",6} {"ρ₀",6} {"ρ_T",10} {"class",-12} {"flat-stable",10}  bounded");
        foreach (double d in new[] { 3.0, dStar, 15.0, 25.0 })
        {
            foreach (double r0 in new[] { 0.9, 1.1 })
            {
                var rho = Sim(RestoringTerm.Diffusion, d, r0);
                string cls = CurvatureFeedback.Classify(rho);
                bool flatStable = Math.Abs(rho[^1] - 1.0) < 0.05;
                bool bounded = Math.Abs(rho[^1]) < 5.0;
                sb.AppendLine($"{d,6:F2} {r0,6:F2} {rho[^1],10:F3} {cls,-12} {flatStable,10}  {bounded}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"ASSUMPTION: strong diffusion (d > d* = {dStar:F2}) makes flat a globally stable");
        sb.AppendLine("fixed point (the bounded F cannot outrun a linear restoring term).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the diffusion term stabilizes ρ around flat for d > d* — flat becomes");
        sb.AppendLine("a stable finite attractor with NO new primitives (a linear restoring force on ρ).");
        Output.WriteLine(sb.ToString());

        Assert.True(dStar > 0.0, "critical diffusion not positive");

        // Strong diffusion ⇒ flat is a stable attractor.
        foreach (double d in new[] { 15.0, 25.0 })
            foreach (double r0 in new[] { 0.9, 1.1 })
            {
                var rho = Sim(RestoringTerm.Diffusion, d, r0);
                Assert.True(Math.Abs(rho[^1] - 1.0) < 0.05,
                    $"d={d}, ρ₀={r0}: flat not stable (ρ_T={rho[^1]:F3})");
            }

        // Weak diffusion ⇒ flat unstable but the system is bounded (finite off-flat attractor).
        foreach (double r0 in new[] { 0.9, 1.1 })
        {
            var rho = Sim(RestoringTerm.Diffusion, 3.0, r0);
            Assert.True(Math.Abs(rho[1] - 1.0) > Math.Abs(r0 - 1.0),
                $"d=3, ρ₀={r0}: flat not repelling (ρ_1={rho[1]:F3})");
            Assert.True(Math.Abs(rho[^1]) < 5.0, $"d=3, ρ₀={r0}: unbounded (ρ_T={rho[^1]:F3})");
            Assert.True(Converged(rho), $"d=3, ρ₀={r0}: did not converge");
        }
    }

    // ── G4-E21: logistic term gives bistable finite attractors ─────────────────────────

    [Fact]
    public void G4_E21_LogisticTermGivesBistableAttractors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-E21: logistic term −c(ρ−1)³ gives bistable finite attractors");

        double[] r0s = { 0.9, 0.98, 1.02, 1.1 };

        sb.AppendLine("The cubic term is restoring (pushes ρ toward 1) but has NO linear part, so flat");
        sb.AppendLine("stays unstable; the balance with anti-diffusion yields two STABLE finite points");
        sb.AppendLine("(asymmetric — the reconstruction F is asymmetric: clamps at +4.335 / −4.764).");
        sb.AppendLine();
        sb.AppendLine($"{"c",6} {"ρ₀",6} {"ρ_T",10} {"class",-12}  bounded  no-oscillation");
        int oscCount = 0;
        foreach (double c in new[] { 0.5, 1.0, 2.0 })
        {
            foreach (double r0 in r0s)
            {
                var rho = Sim(RestoringTerm.Logistic, c, r0);
                string cls = CurvatureFeedback.Classify(rho);
                if (cls == "oscillatory") oscCount++;
                bool bounded = Math.Abs(rho[^1]) < 5.0;
                bool noOsc = cls != "oscillatory";
                sb.AppendLine($"{c,6:F1} {r0,6:F2} {rho[^1],10:F3} {cls,-12}  {bounded,-7}  {noOsc}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"Oscillatory trajectories: {oscCount}/{3 * r0s.Length} (expect 0).");
        sb.AppendLine("All trajectories converge to a FINITE attractor — a stable, bounded state.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the logistic term yields a bistable system: flat is unstable, but two");
        sb.AppendLine("finite stable attractors exist — a stable finite attractor with no new primitives.");
        Output.WriteLine(sb.ToString());

        foreach (double c in new[] { 0.5, 1.0, 2.0 })
            foreach (double r0 in r0s)
            {
                var rho = Sim(RestoringTerm.Logistic, c, r0);
                Assert.True(Math.Abs(rho[^1]) < 5.0, $"c={c}, ρ₀={r0}: unbounded (ρ_T={rho[^1]:F3})");
                Assert.NotEqual("oscillatory", CurvatureFeedback.Classify(rho));
                Assert.True(Converged(rho), $"c={c}, ρ₀={r0}: did not converge");
            }

        // Near-flat initial conditions diverge (flat is unstable).
        foreach (double r0 in new[] { 0.98, 1.02 })
        {
            var rho = Sim(RestoringTerm.Logistic, 1.0, r0);
            Assert.True(Math.Abs(rho[^1] - 1.0) > Math.Abs(r0 - 1.0),
                $"c=1, ρ₀={r0}: flat not repelling (ρ_T={rho[^1]:F3})");
        }
    }

    // ── G4-E22: conservation term + cross-mechanism comparison ─────────────────────────

    [Fact]
    public void G4_E22_ConservationPinsFlatAndComparison()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-E22: conservation mean(ρ)=1 pins flat; comparison of all mechanisms");

        sb.AppendLine("Conservation = hard constraint mean(ρ)=1 (pins the mean density to flat).");
        var pinned = Sim(RestoringTerm.Conservation, 0.0, 1.0);
        var snapped = Sim(RestoringTerm.Conservation, 0.0, 0.9);
        bool pinnedFlat = pinned.All(x => Math.Abs(x - 1.0) < 1e-12);
        bool snappedFlat = snapped.Skip(1).All(x => Math.Abs(x - 1.0) < 1e-12);

        sb.AppendLine($"  from ρ₀=1.0: all steps flat = {pinnedFlat}.");
        sb.AppendLine($"  from ρ₀=0.9: projected to flat at every step ≥ 1 = {snappedFlat}.");
        sb.AppendLine();

        sb.AppendLine("Cross-mechanism comparison:");
        sb.AppendLine($"{"mechanism",-22} {"fixed point",-16} {"stability",-14} {"oscillation",-12} {"bounded",-8} stable-attractor");
        sb.AppendLine($"{"none (anti-diffusive)",-22} {"flat (ρ=1)",-16} {"unstable",-14} {"no",-12} {"no",-8} no");
        sb.AppendLine($"{"diffusion d>d*",-22} {"flat (ρ=1)",-16} {"stable",-14} {"no",-12} {"yes",-8} YES (flat)");
        sb.AppendLine($"{"logistic c>0",-22} {"two finite pts",-16} {"bistable",-14} {"no",-12} {"yes",-8} YES (finite pair)");
        sb.AppendLine($"{"conservation",-22} {"flat (pinned)",-16} {"trivial",-14} {"no",-12} {"yes",-8} YES (degenerate)");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: primitive-native restoring terms DO stabilize the anti-diffusive feedback.");
        sb.AppendLine("Diffusion (d > d* = k|F′(1)|) stabilizes flat; logistic gives bistable finite attractors;");
        sb.AppendLine("conservation pins flat degenerately. A stable finite attractor is achievable with NO new");
        sb.AppendLine("primitives (only ρ, R, and arithmetic).");
        Output.WriteLine(sb.ToString());

        Assert.True(pinnedFlat, "conservation did not pin flat");
        Assert.True(snappedFlat, "conservation did not project to flat");
    }
}
