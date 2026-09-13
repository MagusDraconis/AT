using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.MinimalWorkingSubstrateAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_044 — Minimal Working Substrate Audit.
///
/// QUESTION. Is D96^3 selected because it is the FIRST working substrate, or because it MINIMISES COMPLEXITY?
/// Compared: D96^2, D96^3, D96^4, D96^5. Measured: state-space size, irreps, photon support, graviton support,
/// observability fraction, states per observable.
///
/// ANSWER: **EMERGENT — the two proposed explanations are the SAME statement, and the third is refuted.**
///
///  (1) THE WORKING SET IS AN UP-SET: {3, 4, 5, ...} — one lower edge, no gaps.
///  (2) ALL SIX MEASURES ARE MONOTONE IN COST (state space, irreps, photon support, graviton support and states
///      per observable rise; the observability fraction falls).
///  (3) THEREFORE MINIMALITY ≡ FIRSTNESS, as a theorem rather than a finding: with strictly increasing costs the
///      cheapest member of any set is its smallest element. The two explanations cannot differ — which is why
///      the minimality is EMERGENT rather than independent.
///  (4) OPTIMALITY IS REFUTED: the unconstrained optimum of every measure lies at d = 1 or 2, outside the
///      working set. D96^3 is 96× the state space of D96^2 and 5.6× its states per observable.
///  (5) A COMPUTED ASIDE: the mandatory step (2 → 3) is the CHEAPEST step available — the compulsory move is
///      the cheapest move (penalties 3.88, 5.65, 7.38, 9.11).
/// </summary>
public class Y_G_044_Tests : ResearchTestBase
{
    public Y_G_044_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_044_TheWorkingSetIsAnUpSetWithOneLowerEdge()
    {
        // the family is built from the ring itself
        Assert.Equal(96, D96Cells);

        // the criteria, computed rather than assumed
        Assert.False(Works(1));
        Assert.False(Works(2));
        Assert.True(Works(3));
        Assert.True(Works(4));
        Assert.True(Works(5));
        Assert.True(Works(6));

        Assert.Equal(new[] { 3, 4, 5, 6 }, WorkingSet());
        Assert.Equal(3, FirstWorkingDimension());
        Assert.True(TheWorkingSetIsAnUpSet());

        // and the precise reason d = 2 fails: the sector exists but carries nothing
        Assert.Equal(2, GravitonSupport(2));
        Assert.Equal(0, Gravitons(2));
        Assert.False(GravitonPropagates(2));
    }

    [Fact]
    public void Y_G_044_AllSixMeasuresAreMonotoneInCost()
    {
        Assert.True(EveryMeasureIsMonotoneInCost());

        // state space: 9 215 -> 884 735 -> 84 934 655 -> 8 153 726 975
        Assert.Equal(9215L, StateSpace(2));
        Assert.Equal(884735L, StateSpace(3));
        Assert.Equal(84934655L, StateSpace(4));
        Assert.Equal(8153726975L, StateSpace(5));

        // irreps 5, 10, 20, 36; photon support 2..5; graviton support 2, 5, 9, 14
        Assert.Equal(new[] { 5, 10, 20, 36 }, Enumerable.Range(2, 4).Select(IrrepCount).ToArray());
        Assert.Equal(new[] { 2, 3, 4, 5 }, Enumerable.Range(2, 4).Select(PhotonSupport).ToArray());
        Assert.Equal(new[] { 2, 5, 9, 14 }, Enumerable.Range(2, 4).Select(GravitonSupport).ToArray());

        // observability falls, states per observable rises
        Assert.True(ObservabilityFraction(2) > ObservabilityFraction(3));
        Assert.True(ObservabilityFraction(3) > ObservabilityFraction(4));
        Assert.True(StatesPerObservable(3) > StatesPerObservable(2));
        Assert.Equal(5.65, StatesPerObservable(3) / StatesPerObservable(2), 1);
    }

    [Fact]
    public void Y_G_044_MinimalityIsTheSameStatementAsFirstness()
    {
        // for EVERY measure, the cheapest WORKING substrate is the first working substrate
        Assert.True(MinimalityIsTheSameAsFirstness());
        foreach (var (name, _, cost, _) in Measures())
            Assert.Equal(3, ArgMinOverWorkingSet(cost));

        // so the two proposed explanations cannot differ — the minimality is a corollary of the firstness
        Assert.Equal(3, FirstWorkingDimension());
    }

    [Fact]
    public void Y_G_044_OptimalityIsRefutedBecauseTheOptimaAreOutsideTheWorkingSet()
    {
        Assert.True(OptimalityIsRefuted());
        foreach (var (_, _, cost, _) in Measures())
        {
            int best = ArgMinOverAll(cost);
            Assert.True(best < 3, $"a measure's unconstrained optimum was at d = {best}");
            Assert.False(Works(best));
        }

        // the cost of the mandatory step, in numbers
        Assert.Equal(96.0, (double)StateSpace(3) / StateSpace(2), 0);
        Assert.Equal(5.65, StatesPerObservable(3) / StatesPerObservable(2), 1);
    }

    [Fact]
    public void Y_G_044_TheMandatoryStepIsTheCheapestStep()
    {
        Assert.True(TheMandatoryStepIsTheCheapest());
        Assert.Equal(3.88, ObservabilityPenalty(1, 2), 1);
        Assert.Equal(5.65, ObservabilityPenalty(2, 3), 1);
        Assert.Equal(7.38, ObservabilityPenalty(3, 4), 1);
        Assert.Equal(9.11, ObservabilityPenalty(4, 5), 1);
    }

    [Fact]
    public void Y_G_044_TheVerdictIsEmergentBecauseMinimalityCollapsesIntoFirstness()
    {
        Assert.Equal("EMERGENT", Verdict());

        Assert.Contains("MINIMAL", EmergentOptions());
        Assert.Contains("OPTIMAL", RefutedOptions());
        Assert.Contains("FIRST", DerivedOptions());

        // the classification is settled even though G_043's mechanism question is not
        Assert.Single(EmergentOptions());
        Assert.Single(RefutedOptions());
        Assert.Single(DerivedOptions());
    }

    [Fact]
    public void Y_G_044_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_044 — Minimal Working Substrate Audit: minimal, optimal, or merely first?");

        sb.AppendLine("QUESTION. Is D96^3 selected because it is the FIRST working substrate, or because it");
        sb.AppendLine("MINIMISES complexity?  COMPARED  D96^2, D96^3, D96^4, D96^5");
        sb.AppendLine("MEASURED  state-space size, irreps, photon support, graviton support, observability");
        sb.AppendLine("          fraction, states per observable");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A substrate WORKS when both sectors carry propagating states (photons d - 1 > 0 and");
        sb.AppendLine("     gravitons (d+1)(d-2)/2 > 0) AND the symmetry supplies a dimension-3 irrep (G_033).");
        sb.AppendLine("  2. 'Minimises complexity' means minimising one of the six measured costs; each is reported");
        sb.AppendLine("     with its direction of travel, and no weighting between them is assumed.");
        sb.AppendLine("  3. The orbitals of D96^d are C(48 + d, d) — what a substrate-constructed measurement can");
        sb.AppendLine("     name (G_040 at d = 1, G_043 across the ladder).");
        sb.AppendLine("  4. 'Optimal' means optimal over the whole ladder, INCLUDING the substrates that do not work;");
        sb.AppendLine("     'minimal' means minimal within the working set. That difference is the test.");
        sb.AppendLine("  5. Deterministic throughout; every argmin is computed by ordering, never quoted.");
        sb.AppendLine();

        PrintHeader(OutputWorkingSet());
        PrintHeader(OutputMeasures());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
