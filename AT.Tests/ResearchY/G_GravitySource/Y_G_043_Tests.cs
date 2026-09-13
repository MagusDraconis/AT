using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.MinimalityAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_043 — Minimality Audit.
///
/// QUESTION. Why does nature stop at the first working dimension (d = 3) instead of continuing to d = 4, 5, ...?
/// Is there a quantity that is optimal ONLY at d = 3?
///
/// Compared: D96^3, D96^4, D96^5. Measured: photon polarisations, graviton polarisations, the Hodge mismatch,
/// representation growth, state-space growth, clock-law scaling — plus two derived observability measures and
/// one count bound.
///
/// ANSWER: **BOUNDARY — the OUTCOME is over-determined and the MECHANISM is undetermined.**
///
///  (1) EXACTLY ONE measured family is uniquely zero at d = 3: the Hodge mismatch |dim L2 − dim V| (1, 0, 2, 5,
///      9 for d = 2..6) — which is G_042's root, so it is not a second mechanism.
///  (2) EVERY GROWTH FAMILY IS MONOTONE, and all of them favour SMALLER d (representation growth 2, 3, 8, 20,
///      80; state space 96^d − 1; observability fraction 0.516, 0.133, 0.0235, 0.00319; states per observable
///      1.94, 7.52, 42.5, 314, 2841). An economy optimum would stop at d = 1 or 2, which do not work — so the
///      stop at 3 is NOT an economy optimum.
///  (3) The one other extremal quantity (the graviton's polarisations fit within the number of directions,
///      holding for d ≤ 3) is the SAME quadratic family at a different offset: d² − 3d = 0 vs d² − 3d − 2 ≤ 0.
///  (4) FIVE criteria turn over at d = 3 — three ON at 3, two OFF after it — and their intersection is {3}, so
///      "the first dimension that works" and "the only dimension where the mismatch vanishes" are the same
///      dimension, and no measured quantity can separate them.
///  (5) But the exclusion of d = 4 is ROBUST: the whole family d² − 3d − c separates 3 from 4 for c ∈ [0, 3],
///      which contains both members (the equality at c = 0 and the bound at c = 2).
/// </summary>
public class Y_G_043_Tests : ResearchTestBase
{
    public Y_G_043_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_043_TheMeasuredFamiliesAreWhatTheLadderSays()
    {
        // the ladder, recomputed
        Assert.Equal(1, Photons(2));
        Assert.Equal(3, Photons(4));
        Assert.Equal(0, Gravitons(2));
        Assert.Equal(2, Gravitons(3));
        Assert.Equal(5, Gravitons(4));

        // the Hodge mismatch: zero only at d = 3
        int[] mismatch = Enumerable.Range(2, 5).Select(HodgeMismatch).ToArray();
        Assert.Equal(new[] { 1, 0, 2, 5, 9 }, mismatch);
        Assert.Equal(new[] { 3 }, Enumerable.Range(2, 5).Where(d => HodgeMismatch(d) == 0).ToArray());

        // and the cross-audit check: the orbital count reproduces G_040's 49 at d = 1
        Assert.Equal(49, Orbitals(1));
    }

    [Fact]
    public void Y_G_043_EveryGrowthFamilyIsMonotoneAndFavoursSmallerDimensions()
    {
        Assert.True(GrowthFavoursSmallerDimensions());
        Assert.True(MonotoneFamilies().Length >= 6);

        // the state space and the observability measures, in the direction that makes larger d worse
        Assert.True(StrictlyIncreasing(new[] { (double)StateSpaceDimension(3), StateSpaceDimension(4), StateSpaceDimension(5) }));
        Assert.True(StrictlyDecreasing(new[] { ObservabilityFraction(3), ObservabilityFraction(4), ObservabilityFraction(5) }));
        Assert.True(StrictlyIncreasing(new[] { StatesPerObservable(3), StatesPerObservable(4), StatesPerObservable(5) }));
        Assert.True(StrictlyDecreasing(new[] { ClockRatePerDay(3), ClockRatePerDay(4), ClockRatePerDay(5) }));

        // so an economy optimum would stop at d = 1 or 2 — therefore the stop at 3 is not an economy optimum
        Assert.True(ObservabilityFraction(2) > ObservabilityFraction(3));
        Assert.True(StatesPerObservable(2) < StatesPerObservable(3));
    }

    [Fact]
    public void Y_G_043_ExactlyOneFamilyIsUniquelyZeroAtThree()
    {
        var zero = UniquelyZeroAtThree();
        Assert.Single(zero);
        Assert.Contains("Hodge", zero[0]);

        // and that family is G_042's root, so it is not a second mechanism
        Assert.True(ThreeDimensionalityDependencyAudit.TheGeometricConditionsAreOneEquation());
    }

    [Fact]
    public void Y_G_043_TheCountBoundIsTheSameQuadraticFamily()
    {
        // the count bound: the graviton's polarisations fit within the dimensions
        Assert.True(GravitonsFitWithinDimensions(2));
        Assert.True(GravitonsFitWithinDimensions(3));
        Assert.False(GravitonsFitWithinDimensions(4));
        Assert.Equal(new[] { 1, 2, 3 }, Enumerable.Range(1, 3).Where(GravitonsFitWithinDimensions).ToArray());

        // ...and it is the same family at a different offset
        Assert.True(FamilyAdmits(3, 0) && !FamilyAdmits(4, 0));   // the Hodge equality
        Assert.True(FamilyAdmits(3, 2) && !FamilyAdmits(4, 2));   // the count bound
    }

    [Fact]
    public void Y_G_043_FiveCriteriaTurnOverAtTheSameDimension()
    {
        var criteria = CriteriaWithDirection();
        Assert.Equal(5, criteria.Length);
        Assert.Equal(3, criteria.Count(c => c.Direction.StartsWith("turns ON")));
        Assert.Equal(2, criteria.Count(c => c.Direction.StartsWith("turns OFF")));
        Assert.True(EveryCriterionTurnsAtThree());

        // the intersection is exactly {3}
        Assert.Equal(new[] { 3 }, DimensionsWhereAllCriteriaHold());

        // so the two candidate mechanisms coincide and cannot be separated by these measurements
        Assert.True(TheTwoCandidateMechanismsCoincide());
    }

    [Fact]
    public void Y_G_043_TheExclusionOfFourIsRobust()
    {
        Assert.Equal(new[] { 0, 1, 2, 3 }, RobustOffsets());
        Assert.True(BothSelectedMembersAreRobust());
        Assert.True(TheOutcomeIsRobust());

        // both members lie inside the window, and the window is more than a single tuned point
        Assert.Contains(0, RobustOffsets());
        Assert.Contains(2, RobustOffsets());
        Assert.True(RobustOffsets().Length > 1);
    }

    [Fact]
    public void Y_G_043_TheVerdictIsBoundaryBecauseTheMechanismIsUndetermined()
    {
        Assert.Equal("BOUNDARY", Verdict());

        // the outcome is over-determined: every criterion turns at the same place ...
        Assert.True(EveryCriterionTurnsAtThree());
        // ... and the mechanism is undetermined: the two candidates are the same dimension
        Assert.True(TheTwoCandidateMechanismsCoincide());
    }

    [Fact]
    public void Y_G_043_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_043 — Minimality Audit: why stop at the first working dimension?");

        sb.AppendLine("QUESTION. Why does nature stop at the first working dimension (d = 3) instead of continuing");
        sb.AppendLine("to d = 4, 5, ...? Is there a quantity that is optimal ONLY at d = 3?");
        sb.AppendLine("COMPARED  D96^3, D96^4, D96^5 (and the rest of the ladder)");
        sb.AppendLine("MEASURED  photon polarisations, graviton polarisations, the Hodge mismatch, representation");
        sb.AppendLine("          growth, state-space growth, clock-law scaling, plus two observability measures and");
        sb.AppendLine("          one count bound");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The family is D96^d, so the state space is the simplex over 96^d cells and the substrate's");
        sb.AppendLine("     symmetry is the semidirect product of the translations with the signed permutations.");
        sb.AppendLine("  2. The number of ORBITALS — what a substrate-constructed measurement can name (G_040) — is");
        sb.AppendLine("     C(48 + d, d), the pair invariant being the multiset of per-axis distances. It reproduces");
        sb.AppendLine("     G_040's 49 at d = 1, which is the audit's cross-check.");
        sb.AppendLine("  3. A family is a SELECTOR only if it has an optimum on the ladder; a monotone family has");
        sb.AppendLine("     none, by construction.");
        sb.AppendLine("  4. Polarisation counts are the standard massless counts (G_041/G_042): photons d - 1,");
        sb.AppendLine("     gravitons (d+1)(d-2)/2.");
        sb.AppendLine("  5. Deterministic throughout; every threshold is computed by scanning, never quoted.");
        sb.AppendLine();

        PrintHeader(OutputFamilies());
        PrintHeader(OutputThresholds());
        PrintHeader(OutputRobustness());

        Output.WriteLine(sb.ToString());
    }
}
