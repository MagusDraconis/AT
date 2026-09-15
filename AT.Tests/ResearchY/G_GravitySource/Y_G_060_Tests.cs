using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseEvolutionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_060 - Phase Evolution Audit (group G - Gravity Source).
///
/// QUESTION. Can the difference-generated phase push (G_059) be promoted to an actual update rule? Test
/// rho(t+1) = rho(t) + eps*D*rho and other AT-native updates. Measure the phase evolution rank, the amplitude evolution
/// rank, stability and fixed points. Critical: does any AT-native update generate non-trivial phase dynamics?
///
/// ANSWER: **DERIVED - the promotion exists, and it is the norm-preserving form; the form the question names is the one
/// that fails, because its stability IS dissipation and dissipation destroys the phase.**
/// </summary>
public class Y_G_060_Tests : ResearchTestBase
{
    public Y_G_060_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_060_TheReachablePhaseRankIs42ByTwoIndependentRoutes()
    {
        foreach (var (update, _, isControl) in Updates())
        {
            int perChannel = ReachablePhaseRank(update);
            if (isControl) { Assert.Equal(0, perChannel); continue; }
            Assert.Equal(42, perChannel);
            Assert.Equal(42, ReachablePhaseRankByKrylov(update, 1e-3, 12));
            Assert.Equal(42, ReachableAmplitudeRank(update));
            Assert.Equal(PhaseDimension() - 42, PhaseDimension() - perChannel);
        }
        Assert.Equal(53, PhaseDimension());
        Assert.Equal(42, PhaseBearingChannels().Length);

        // the eleven unreachable directions, named: five doubly-hidden channels and the alternating mode
        var names = UnreachablePhaseDirections();
        Assert.Equal(5, names.Count(n => n.StartsWith("empty channel")));
        Assert.Single(names, n => n.Contains("alternating mode"));
        Assert.Contains("11 unreachable phase directions", names);
    }

    [Fact]
    public void Y_G_060_TheNamedUpdateIsDissipativeAndReachesThePhaseFreeState()
    {
        // the difference's symmetric part is negative semi-definite: |mu| < 1 on every channel
        Assert.All(Channels(), c => Assert.True(Multiplier("forward difference", c, 1e-3).Magnitude < 1.0));
        Assert.All(Channels(), c => Assert.True(Multiplier("exact flow exp(eps D)", c, 1e-3).Magnitude < 1.0));
        Assert.InRange(SpectralRadius("forward difference", 1e-3), 0.99, 1.0);

        // measured over 20000 steps: the deviation norm falls toward the phase-free state
        var (steps, start, end) = DeviationEnvelope("forward difference", 1e-3, 20000);
        Assert.Equal(20000, steps);
        Assert.InRange(start, 1.0, 1.02);
        Assert.InRange(end, 0.2, 0.3);
        Assert.True(TheDissipativeFlowContractsTowardThePhaseFreeState());

        // THE WITHDRAWN CLAIM: the phase content OSCILLATES rather than decaying monotonically
        Assert.True(ThePhaseContentOscillatesOnTheWayDown());
        Assert.True(PhaseNorm(Orbit("forward difference", Base(), 1e-3, 1000))
                  > PhaseNorm(Orbit("forward difference", Base(), 1e-3, 100)));
    }

    [Fact]
    public void Y_G_060_TheAmplifyingFormsLeaveTheSimplex()
    {
        foreach (var update in new[] { "backward difference", "centred (skew) difference" })
        {
            Assert.True(SpectralRadius(update, 1e-3) > 1.0);
            var (decaying, sustained, growing) = StabilitySplit(update, 1e-3);
            Assert.Equal(0, decaying);
            Assert.Equal(0, sustained);
            Assert.Equal(42, growing);
        }

        // the backward difference leaves the simplex in a measured, shrinking number of steps
        int slow = StepsUntilOffSimplex("backward difference", 0.0, 1e-3);
        int fast = StepsUntilOffSimplex("backward difference", 0.0, 1e-1);
        Assert.InRange(slow, 700, 900);
        Assert.InRange(fast, 5, 15);
        Assert.True(fast < slow);
    }

    [Fact]
    public void Y_G_060_TheUnitaryFormSustainsThePhase()
    {
        // |mu| = 1 EXACTLY on every channel, so 42 directions are sustained and none decays or grows
        Assert.All(Channels(), c => Assert.Equal(1.0, Multiplier(SustainingForm, c, 1e-3).Magnitude, 12));
        Assert.Equal(1.0, SpectralRadius(SustainingForm, 1e-3), 12);
        var (decaying, sustained, growing) = StabilitySplit(SustainingForm, 1e-3);
        Assert.Equal(0, decaying);
        Assert.Equal(42, sustained);
        Assert.Equal(0, growing);

        // the deviations are conserved, the sum is conserved, and every cell stays positive over 20000 steps
        Assert.True(TheUnitaryFormConservesTheDeviationNorm());
        Assert.True(TheUnitaryFormKeepsTheSum());
        Assert.True(TheUnitaryFormKeepsTheSimplex());
        Assert.True(PhaseNorm(Orbit(SustainingForm, Base(), 1e-3, 20000)) > 1e-1);
        Assert.Equal(SustainingForm, ThePromotionThatWorks());
    }

    [Fact]
    public void Y_G_060_TheAtNativePositivityGuardNeverFires()
    {
        // rho is a density, so the clipped variant is carried - and it is numerically identical to the unclipped step
        Assert.True(TheClippedUpdateNeverFires());
        var clipped = Orbit("positivity-clipped difference", Base(), 1e-3, 4000);
        var plain = Orbit("forward difference", Base(), 1e-3, 4000);
        Assert.True(clipped.Zip(plain, (a, b) => Math.Abs(a - b)).Max() < 1e-12);
        Assert.True(clipped.Min() > 0.5);
    }

    [Fact]
    public void Y_G_060_TheVerdictIsDerivedAndTheFixedPointsAreTheConstant()
    {
        Assert.True(AnyUpdateGeneratesPhaseDynamics());
        Assert.True(AnyStableUpdateSustainsThePhase());
        Assert.Equal("DERIVED", Verdict());

        // the constant is fixed by every form; the CENTRED family additionally fixes the alternating mode,
        // which the centred difference annihilates - so its fixed-point set is 2-dimensional, not 1
        Assert.True(TheConstantIsFixedByEveryForm());
        Assert.True(TheCentredFamilyAlsoFixesTheAlternatingMode());
        Assert.Equal(1, FixedPointDimension("forward difference"));
        Assert.Equal(2, FixedPointDimension("centred (skew) difference"));
        Assert.Equal(2, FixedPointDimension(SustainingForm));
        foreach (var (update, _, _) in Updates())
            Assert.True(TheUniformStateIsTheFixedPoint(update));

        // the named form is the one that fails: stability here IS dissipation
        Assert.Equal(0, StabilitySplit("forward difference", 1e-3).Sustained);
        Assert.Equal(42, StabilitySplit(SustainingForm, 1e-3).Sustained);
    }

    [Fact]
    public void Y_G_060_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_060 - Phase Evolution Audit: can the difference-generated push be promoted to an update rule?");

        sb.AppendLine("QUESTION. Can the difference-generated phase push (G_059) be promoted to an actual UPDATE RULE?");
        sb.AppendLine("GIVEN        G_057 (the running process is phase-static), G_059 (the difference IS the source; the");
        sb.AppendLine("             canonical state has no phase content; the difference's multiplier is exact)");
        sb.AppendLine("TEST         rho(t+1) = rho(t) + eps*D*rho, and five other AT-native forms");
        sb.AppendLine("MEASURE      phase evolution rank, amplitude evolution rank, stability, fixed points");
        sb.AppendLine("CRITICAL     does any AT-native update generate non-trivial phase dynamics?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Every candidate is CIRCULANT - a function of the shift - so one complex multiplier per channel is");
        sb.AppendLine("     the whole update, and the audit drives both its analysis and its iteration from that number.");
        sb.AppendLine("  2. The reachable rank is the dimension of the phase subspace reachable FROM A PHASE-FREE state, which");
        sb.AppendLine("     is the state AT actually has (G_059), not from an arbitrary one.");
        sb.AppendLine("  3. Stability is the spectral radius over the channels that carry a visible mode, and it is reported");
        sb.AppendLine("     WITH the simplex budget: rho is a density, so an update must keep its cells positive and its sum");
        sb.AppendLine("     equal to 96.");
        sb.AppendLine("  4. The actualization is carried as a CONTROL: G_059 measured that it supplies no rho-generator at all.");
        sb.AppendLine("  5. Deterministic throughout; no randomness anywhere.");
        sb.AppendLine();

        PrintHeader(OutputMultipliers());
        PrintHeader(OutputRanks());
        PrintHeader(OutputStability());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
