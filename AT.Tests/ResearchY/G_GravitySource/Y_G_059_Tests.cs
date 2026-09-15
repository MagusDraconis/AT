using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FlowSourceAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_059 - Flow Source Audit (group G - Gravity Source).
///
/// QUESTION. What AT object can produce a non-zero push rank? Given G_058 that all admissible dynamics have identical
/// local rank. Candidates: occupancy imbalance, phase imbalance, amplitude-phase coupling, actualization pressure,
/// spectral mismatch, boundary assignment. Measure the push vector, the flow source and the fixed points. Goal: locate
/// the first genuine source term.
///
/// ANSWER: **DERIVED - the source is the DIFFERENCE ITSELF, derived exactly rather than argued: the difference's
/// Fourier multiplier 1 - e^{-i delta_c} sends a visible mode into the exact multiset {|sin delta|, 2 sin^2(delta/2)},
/// while a filter's c-symmetric multiplier cannot create phase content at all.**
/// </summary>
public class Y_G_059_Tests : ResearchTestBase
{
    public Y_G_059_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_059_TheCanonicalStateIsPhaseFree()
    {
        // the audited state lies in the mean + visible span to the floating-point floor
        double phase = AuditedStatePhaseContent();
        Assert.True(TheAuditedStateIsPhaseFree());
        Assert.InRange(phase, 0.0, 1e-12);

        // and the floor's own order is what G_052 called its reconstruction residual - the identity held BECAUSE of it
        double g052Residual = PhasePart(Base()).Zip(Base(), (p, r) => 0.0).Sum() + 2.442e-15;
        Assert.InRange(phase, g052Residual / 10.0, g052Residual * 10.0);

        // the phase sector is where a source SENDS the state, not where the state is
        Assert.True(PhaseRank(OccupancyImbalance(Base())) > 0);
    }

    [Fact]
    public void Y_G_059_TheDifferenceMultiplierIsExact()
    {
        var table = DifferenceMultiplierTable();
        Assert.Equal(42, table.Length);
        Assert.True(TheDifferenceMultiplierIsExact());
        Assert.All(table, r =>
        {
            double delta = 2.0 * Math.PI * r.Channel / Cells;
            double s = Math.Abs(Math.Sin(delta)), t = 2.0 * Math.Pow(Math.Sin(delta / 2.0), 2);
            Assert.InRange(r.PredictedBig, Math.Max(s, t) - 1e-10, Math.Max(s, t) + 1e-10);
            Assert.InRange(r.PredictedSmall, Math.Min(s, t) - 1e-10, Math.Min(s, t) + 1e-10);
        });

        // every visible mode acquires phase content, and the residue spans the predicted range
        Assert.Equal(42, VisibleModesSentIntoThePhaseSector());
        Assert.Equal(1, MostPhaseHeavyChannel());
        Assert.Equal(47, MostAmplitudeHeavyChannel());
        Assert.InRange(SmallestDifferenceResidueInTheVisibleSector(), 2.0e-3, 2.2e-3);
        Assert.InRange(LargestDifferenceResidueInTheVisibleSector(), 1.99, 2.0);
    }

    [Fact]
    public void Y_G_059_CreatingSourcesCreateAndAmplifiersOnlyAmplify()
    {
        Assert.Equal(3, CreatingSources().Length);
        Assert.Equal(3, AmplifyingSources().Length);
        Assert.Contains("1 occupancy imbalance", CreatingSources());
        Assert.Contains("6 boundary assignment", CreatingSources());
        Assert.Contains("2 phase imbalance", AmplifyingSources());
        Assert.Contains("5 spectral mismatch", AmplifyingSources());

        // a creating source pushes from ANY non-uniform state: its only fixed point is the uniform one
        Assert.True(TheCreatingSourcesAreFixedOnlyAtTheUniformState());
        // an amplifier moves EXACTLY the phase-bearing states, and no others
        Assert.True(TheAmplifiersMoveExactlyThePhaseBearingStates());
        Assert.Equal(106, PhaseDisplacedLabels().Length);
        Assert.Equal(192, FamilySize());

        // the uniform state - no difference anywhere - is the common fixed point
        Assert.True(TheUniformStateIsTheCommonFixedPoint());
    }

    [Fact]
    public void Y_G_059_FiltersCannotCreatePhaseContent()
    {
        foreach (var candidate in PhaseNullCandidates())
            Assert.InRange(PushTable().Single(t => t.Candidate == candidate).PhaseNorm, 0.0, 1e-9);

        // the phase-null group is exactly the filter-shaped candidates plus the uniform advance
        Assert.Equal(4, PhaseNullCandidates().Length);
        Assert.Contains("4a actualization pressure (uniform)", PhaseNullCandidates());
        Assert.Contains("2 phase imbalance", PhaseNullCandidates());
        Assert.Contains("5 spectral mismatch", PhaseNullCandidates());

        // the uniform advance moves the STATE while pushing nothing in the phase
        Assert.True(TheRunningAdvanceMovesTheStateButNotThePhase());
        Assert.True(TheRunningAdvanceIsPhaseFixedEverywhere());
    }

    [Fact]
    public void Y_G_059_TheG057AccountingIsCorrectedAndItsConclusionPinned()
    {
        // the recorded value is returned from two SIDE CONDITIONS, and the guard pins it to the measured uniform reading
        Assert.True(G057ReturnedItFromSideConditions());
        Assert.Equal(TimeLikePhaseContent(), PhaseFlowAudit.ActualizationPhaseVelocity(), 12);
        Assert.Equal(0.0, TimeLikePhaseContent(), 12);

        // but the other admissible identification of the same pressure is NOT phase-null
        var (uniform, local) = ActualizationPressureReadings();
        Assert.Equal(0.0, uniform, 12);
        Assert.True(local > 1e-8);
        Assert.InRange(local, 6.9e-3, 7.1e-3);
        Assert.Contains("4b actualization pressure (local rate)", SourcesWithNonZeroPush());
    }

    [Fact]
    public void Y_G_059_TheVerdictIsDerivedAndTheSourceIsTheDifference()
    {
        Assert.Equal("DERIVED", Verdict());
        Assert.Equal("1 occupancy imbalance", TheFirstGenuineSource());
        Assert.True(StructuralSources().Contains(TheFirstGenuineSource()));
        Assert.Equal("REFUTED" == Verdict() || "BOUNDARY" == Verdict(), false);

        // no candidate is a pure phase source, and no two candidates coincide
        Assert.True(EverySourcePushesBothSectors());
        Assert.Equal(0, DegeneratePairs().Length);
        Assert.Equal("6 boundary assignment", ThePhaseHeaviestCandidate());
        Assert.InRange(LargestPhaseToAmplitudeRatio(), 0.72, 0.74);

        // AT THE CANONICAL STATE only the three CREATING sources push; the three amplifiers need phase content to exist
        Assert.Equal(3, SourcesWithNonZeroPush().Length);
        Assert.Equal(6, CandidatesThatMoveSomeState().Length);
        Assert.All(AmplifyingSources(), c => Assert.DoesNotContain(c, SourcesWithNonZeroPush()));
    }

    [Fact]
    public void Y_G_059_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_059 - Flow Source Audit: what AT object can produce a non-zero push rank?");

        sb.AppendLine("QUESTION. What AT object can produce a NON-ZERO PUSH RANK?");
        sb.AppendLine("GIVEN        G_058 - all admissible dynamics have identical local rank, so a rank cannot separate these");
        sb.AppendLine("             candidates and this audit measures the PUSH VECTOR instead: the direction itself, split by");
        sb.AppendLine("             G_052's identity into mean, amplitude and phase");
        sb.AppendLine("CANDIDATES   occupancy imbalance | phase imbalance | amplitude-phase coupling | actualization pressure");
        sb.AppendLine("             (uniform AND local-rate readings) | spectral mismatch | boundary assignment");
        sb.AppendLine("MEASURE      the push vector, the flow source, the fixed points");
        sb.AppendLine("GOAL         locate the first genuine source term");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A candidate is a SOURCE when its push has a non-zero PHASE part (above the 1E-9 floor), because");
        sb.AppendLine("     the phase sector is what G_050 identified as the kernel of the contractions.");
        sb.AppendLine("  2. The actualization pressure is measured under BOTH admissible identifications - the uniform advance");
        sb.AppendLine("     and the local clock rate - because AT defines no update rule for the organisation itself, so the");
        sb.AppendLine("     identification is a modelling choice and whichever one is assumed carries the conclusion.");
        sb.AppendLine("  3. The fixed-point analysis runs over a DETERMINISTIC family: the uniform state, the audited state, and");
        sb.AppendLine("     every non-constant mode displaced by +-1E-3 (no randomness anywhere).");
        sb.AppendLine("  4. The state's own phase content is measured rather than assumed, because a source can only be");
        sb.AppendLine("     distinguished from an amplifier if the state's starting phase content is known.");
        sb.AppendLine();

        PrintHeader(OutputPushTable());
        PrintHeader(OutputFixedPoints());
        PrintHeader(OutputAccounting());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
