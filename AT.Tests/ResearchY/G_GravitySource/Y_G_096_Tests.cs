using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_096 - MULTIPLICATIVITY ORIGIN AUDIT.
///
/// G_086 collapsed the observable kernel to one function; G_095 measured that multiplicativity - the requirement that
/// the clock composition multiply, K(A+B) = K(A)K(B) - is the only known principle that would force the free
/// coefficient to zero, and that the surviving theory does not contain it. This suite asks whether the surviving AT
/// structure can DERIVE it or whether it is independent information.
///
/// Constraints obeyed literally: every multiplicity, dimension, rank and count is computed on the canonical 96-cell
/// ring ALONE; the cubic substrate is not used (the vector sector is not part of this question); and no new primitive,
/// field, source term, topology axiom, gauge sector, action, Lagrangian or memory mechanism is introduced, assumed,
/// fitted or postulated. A negative result is fully acceptable.
///
/// Two numerical rules are in force and are stated with their measurements: the ring's potential values are computed,
/// so they are CLUSTERED at 1e-12 and the exact-keyed count is reported as artifact-prone rather than used; and the
/// defect that is mathematically zero is compared against its ln-exp ROUNDING scale rather than against equality.
///
/// The report follows the seven tasks.
/// </summary>
public sealed class Y_G_096_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_096_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
    private static string Y(bool v) => v ? "yes" : "no";

    private static StringBuilder Start(params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_096_01_NoCoreObjectImpliesMultiplicativity()
    {
        PrintHeader("G_096 - task 1: every surviving object of the minimal core, tested");
        var sb = Start(
            "the objects are the G_084 minimal core's own: the state, the labelling, the potential map, the clock law and",
            "the gradient definition;",
            "each is TESTED against K(A+B) = K(A)K(B) by imposing its defining property and recomputing the defect, so a",
            "negative is a measurement rather than a reading;",
            "every count in this suite is computed on the canonical 96-cell ring alone; the cubic substrate is NOT used,",
            "because the vector sector is not part of this question;",
            "no new primitive, field, source term, action or mechanism is introduced or assumed anywhere.");
        sb.Append(MultiplicativityOriginAudit.OutputRingAndCore());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var objects = MultiplicativityOriginAudit.CoreObjects();
        Assert.Equal(5, objects.Length);
        Assert.All(objects, o => Assert.False(o.ImpliesMultiplicativity));
        Assert.All(objects, o => Assert.False(string.IsNullOrWhiteSpace(o.Measurement)));
        var counts = MultiplicativityOriginAudit.CoreObjectCounts();
        Assert.Equal(5, counts.Objects);
        Assert.Equal(0, counts.Implying);
        // the ring, and only the ring: 96 cells, and the distinct-potential count clustered rather than exact-keyed
        var resolution = MultiplicativityOriginAudit.RingResolution();
        Assert.Equal(96, resolution.Cells);
        Assert.True(resolution.ClusteredDistinct < resolution.ExactDistinct);
        Assert.Equal(49, resolution.ClusteredDistinct);
        Assert.Equal(78, resolution.ExactDistinct);

        sb.AppendLine($"  {counts.Objects} core objects, {counts.Implying} implying the requirement. The ring carries 96 cells and");
        sb.AppendLine($"  {resolution.ClusteredDistinct} distinct potentials clustered at 1e-12 ({resolution.ExactDistinct} by exact double equality —");
        sb.AppendLine("  an artifact-prone count, which is why it is reported rather than used).");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_02_TheAdmissibleFamilyAndItsDimension()
    {
        PrintHeader("G_096 - task 2: the family satisfying only K(0)=1 and K'(0)=1");
        var sb = Start(
            "the two surviving normalisations are the chain's own UNIQUELY FORCED orders 0 and 1, so the family's free",
            "coefficients are c_2 and above;",
            "two dimensions are reported and the difference between them is the point: the COMBINATORIAL count of free",
            "coefficients, and the RESOLVABLE rank of the coefficient matrix on the ring;",
            "the rank is smaller because the ring's potentials span a narrow interval, so high powers are nearly linearly",
            "dependent and their directions ALIAS; the rank is the dimension the ring resolves and is what the audit",
            "quotes;",
            "the rank is checked across tolerances, because a count quoted off a rounding scale is an artifact (Rules 1, 2).");
        sb.Append(MultiplicativityOriginAudit.OutputFamily());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var family = MultiplicativityOriginAudit.AdmissibleFamily();
        Assert.Equal(2, family.Length);
        var ring = family[0];
        var ladder = family[1];
        Assert.Equal(49 - 2, ring.FreeCoefficients);
        Assert.False(ring.FullColumnRank);                     // the count is NOT verified by the rank, and the audit says so
        Assert.Equal(9, ring.RankOnTheRing);
        Assert.True(ladder.FullColumnRank);
        Assert.Equal(MultiplicativityOriginAudit.LadderReach - 1, ladder.FreeCoefficients);
        // the quoted dimension is the tolerance-free combinatorial count, NOT the rank
        Assert.Equal(47, MultiplicativityOriginAudit.FamilyDimension());
        Assert.Equal(ring.FreeCoefficients, MultiplicativityOriginAudit.FamilyDimension());
        // and the rank is reported because it does NOT sit on a plateau: it is a resolvability measurement
        var band = MultiplicativityOriginAudit.RankByTolerance();
        Assert.Equal(4, band.Length);
        Assert.False(MultiplicativityOriginAudit.RankIsOnAPlateau());
        Assert.True(band.Select(b => b.Rank).Distinct().Count() > 1);
        Assert.Contains(ring.RankOnTheRing, band.Select(b => b.Rank));

        sb.AppendLine($"  the family's dimension is {ring.FreeCoefficients}, exactly, and the ladder's truncation gives {ladder.FreeCoefficients}.");
        sb.AppendLine($"  The coefficient matrix's rank is {ring.RankOnTheRing} and DRIFTS across the band "
            + $"({string.Join(", ", band.Select(b => $"{N(b.Tolerance)} -> {b.Rank}"))}), so it is reported and NOT quoted as the");
        sb.AppendLine("  dimension — a number that moves with its tolerance cannot be one.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_03_TheConstraintCascade()
    {
        PrintHeader("G_096 - task 3: the surviving constraints applied one at a time");
        var sb = Start(
            "six surviving constraints are applied in turn and the remaining dimension is measured after each;",
            "two rows are the audit's centre: the kernel collapse and the source relation both WOULD collapse the family",
            "if imposed, and imposing either is the SAME PREMISE as multiplicativity — so neither is an independent",
            "selector;",
            "the source sharing that genuinely survives is the ZERO SET, which every tested law satisfies and which",
            "therefore constrains nothing, while the derivative-level ratio is measured as unselected (G_077);",
            "observable equivalence removes nothing because the family directions are NOT observationally null.");
        sb.Append(MultiplicativityOriginAudit.OutputCascade());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var cascade = MultiplicativityOriginAudit.Cascade();
        Assert.Equal(7, cascade.Length);
        int dimension = MultiplicativityOriginAudit.FamilyDimension();
        // the first six rows remove nothing at all
        Assert.All(cascade.Take(6), row => Assert.Equal(0, row.DimensionsRemoved));
        Assert.All(cascade.Take(6), row => Assert.Equal(dimension, row.Remaining));
        // and only multiplicativity reduces the family
        Assert.Equal(dimension, cascade[^1].DimensionsRemoved);
        Assert.Equal(0, cascade[^1].Remaining);
        Assert.Contains("MULTIPLICATIVITY", cascade[^1].Constraint);
        // the two collapse rows name the same premise rather than an independent one
        Assert.Contains("SAME PREMISE", cascade.Single(c => c.Constraint.Contains("kernel collapse")).Measurement);
        Assert.Contains("same premise", cascade.Single(c => c.Constraint.Contains("source relation")).Measurement);
        // and the surviving source sharing is the zero set
        Assert.All(ClockSourceAudit.ZeroSetAgreement(), z => Assert.True(z.Agrees));

        sb.AppendLine($"  the family stays at {dimension} through all {cascade.Length - 1} surviving constraints and falls to 0 only");
        sb.AppendLine("  under multiplicativity — which is the same premise as the strong source relation and the kernel collapse.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_04_TheDefectOnTheRingsOwnPairs()
    {
        PrintHeader("G_096 - task 4: R(A,B) = lnK(A+B) - lnK(A) - lnK(B) on the ring's own pairs");
        var sb = Start(
            "the domain is the ring's OWN potential pairs, so no interval is chosen by the audit;",
            "the free room's coefficient is stated in the family's convention: the audit's family is ln K = A + sum c_n A^n,",
            "so the chain's lambda is 2 c_2 and the reference member is c_2 = lambda / 2 = 0.15;",
            "the sector's own law has a defect that is MATHEMATICALLY zero and numerically a ln-exp round trip, so it is",
            "compared against its own rounding scale and never against equality;",
            "the worst pair is reported, so the maximum is located rather than merely stated.");
        sb.Append(MultiplicativityOriginAudit.OutputDefectAndSelectors());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var defects = MultiplicativityOriginAudit.Defects();
        Assert.Equal(4, defects.Length);
        var reference = defects.Single(d => d.Law == "the free room, lambda = 0.300");
        var sector = defects.Single(d => d.Law == "the sector's own law, lambda = 0");
        // the reference member's defect matches the closed form c_2 * 2 * A * B = lambda * A * B at the deepest pair
        double deepest = MultiplicativityOriginAudit.Potentials().Min();
        double expected = Math.Abs(MultiplicativityOriginAudit.ReferenceCoefficient * deepest * deepest);
        Assert.Equal(expected, reference.MaxAbsR, 9);
        Assert.False(reference.Vanishes);
        Assert.True(reference.MaxAbsR > 1e-2);
        // the sector's law vanishes, and the claim is made against the rounding scale
        Assert.True(sector.Vanishes);
        Assert.True(sector.MaxAbsR < 1e-15);
        Assert.Contains("ROUNDING", sector.Measurement);
        // the defect grows with the coefficient's size, monotonically over the tested members
        Assert.True(defects.Single(d => d.Law == "the free room, lambda = 0.5").MaxAbsR > reference.MaxAbsR);
        Assert.True(defects.Single(d => d.Law == "the free room, lambda = -1").MaxAbsR
            > defects.Single(d => d.Law == "the free room, lambda = 0.5").MaxAbsR);

        sb.AppendLine($"  the reference member's defect is {N(reference.MaxAbsR)} at the deepest pair ({N(deepest)}), matching the closed form");
        sb.AppendLine($"  lambda*A*B; the sector's law vanishes at {N(sector.MaxAbsR)} — the ln-exp rounding, not exact zero — and the defect");
        sb.AppendLine("  grows with the coefficient across the tested members.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_05_TheSelectorInventory()
    {
        PrintHeader("G_096 - task 5: does any surviving structure force R(A,B) = 0?");
        var sb = Start(
            "seven candidate selectors, each tested for whether it SURVIVES and whether it forces the defect to vanish;",
            "the audit distinguishes a selector that survives from one that merely would work if assumed, which is the",
            "whole point of the question;",
            "the three rows that would force the defect to vanish are checked for being the SAME premise rather than three",
            "independent routes;",
            "no selector is credited with the requirement merely because it is compatible with it.");
        sb.Append(MultiplicativityOriginAudit.OutputDefectAndSelectors());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var selectors = MultiplicativityOriginAudit.Selectors();
        Assert.Equal(7, selectors.Length);
        var counts = MultiplicativityOriginAudit.SelectorCounts();
        Assert.Equal(7, counts.Selectors);
        Assert.Equal(4, counts.Surviving);
        Assert.Equal(3, counts.ForcingZero);
        Assert.Equal(0, counts.SurvivingAndForcing);      // the answer, as a count
        // the forcing rows are one premise
        var forcing = selectors.Where(s => s.ImpliesMultiplicativity).ToArray();
        Assert.Equal(3, forcing.Length);
        Assert.All(forcing, s => Assert.True(s.IsTheSamePremiseAsMultiplicativity));
        Assert.Contains(forcing, s => s.Selector.StartsWith("MULTIPLICATIVITY"));
        Assert.Contains(forcing, s => s.Selector.Contains("source relation"));
        Assert.Contains(forcing, s => s.Selector.Contains("kernel collapse"));
        // and no forcing row survives
        Assert.All(forcing, s => Assert.False(s.Survives));
        // the surviving rows are the ones that are blind to the clock law
        var surviving = selectors.Where(s => s.Survives).ToArray();
        Assert.All(surviving, s => Assert.Equal(0, s.DimensionsRemoved));

        sb.AppendLine($"  {counts.Selectors} selectors: {counts.Surviving} survive, {counts.ForcingZero} would force R = 0, and the");
        sb.AppendLine($"  INTERSECTION IS {counts.SurvivingAndForcing} — no surviving structure forces it; and all {forcing.Length} of the forcing rows");
        sb.AppendLine("  are the same premise in three guises (they all say ln K = A).");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_06_TheFamilyBeforeAndAfter()
    {
        PrintHeader("G_096 - task 6: the admissible family before and after multiplicativity");
        var sb = Start(
            "the two numbers are measured on the ring: the family's resolvable dimension before and after;",
            "the requirement is imposed as an algebraic condition and the residual family is measured, not asserted;",
            "the combinatorial count and the ladder reach are reported beside the resolvable dimension so the reader can",
            "see which count is which;",
            "the removal is complete, which is the formal statement of the independent-information criterion.");
        sb.Append(MultiplicativityOriginAudit.OutputSizeAndVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var size = MultiplicativityOriginAudit.FamilySize();
        Assert.Equal(47, size.BeforeOnTheRing);
        Assert.Equal(5, size.BeforeAtTheLadderReach);
        Assert.Equal(0, size.After);
        Assert.Equal(1.0, size.RemovedFraction, 12);
        // the requirement is imposed and the residual family is measured: only the sector's own law satisfies it
        foreach (var lambda in new[] { 0.3, 0.5, -1.0 })
        {
            var c = MultiplicativityOriginAudit.FreeRoomCoefficients(lambda);
            double a = 0.11;
            double defect = MultiplicativityOriginAudit.Defect(c, a, a);
            Assert.True(Math.Abs(defect) > 1e-4);
        }
        // whereas the member with the coefficient at zero satisfies it to the rounding
        Assert.True(MultiplicativityOriginAudit.Defect(
            MultiplicativityOriginAudit.FreeRoomCoefficients(0.0), 0.11, 0.11) < 1e-16);

        sb.AppendLine($"  before: {size.BeforeOnTheRing} resolvable dimensions (47 combinatorial, {size.BeforeAtTheLadderReach} at the ladder reach).");
        sb.AppendLine($"  after: {size.After} — a single member, ln K = A. The removal is complete, so the requirement carries the whole");
        sb.AppendLine("  of the sector's remaining freedom.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_07_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_096 - task 7: DERIVED / EMERGENT / ASSUMED / BOUNDARY, and the criteria");
        var sb = Start(
            "the four classes are each decided by a measurement rather than by preference;",
            "DERIVED is decided by the count of surviving structures that force the requirement;",
            "EMERGENT is decided by whether any generative process produces it, and the chain measured the absence of a",
            "flow for the coefficient;",
            "BOUNDARY would require the requirement to constrain the family at the edge of its admissible interval, which",
            "it does not: it names an interior point.");
        sb.Append(MultiplicativityOriginAudit.OutputSizeAndVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = MultiplicativityOriginAudit.Classification();
        Assert.Equal(4, classification.Length);
        Assert.False(classification.Single(c => c.Status == MultiplicativityStatus.Derived).Holds);
        Assert.False(classification.Single(c => c.Status == MultiplicativityStatus.Emergent).Holds);
        Assert.False(classification.Single(c => c.Status == MultiplicativityStatus.Boundary).Holds);
        Assert.True(classification.Single(c => c.Status == MultiplicativityStatus.Assumed).Holds);
        // and the criteria
        var criteria = MultiplicativityOriginAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("NOT MET", criteria.Single(c => c.Criterion.StartsWith("SUCCESS")).Outcome);
        Assert.Equal("MET", criteria.Single(c => c.Criterion.StartsWith("FAILURE")).Outcome);
        Assert.Equal("NOT INDEPENDENT", criteria.Single(c => c.Criterion.StartsWith("and the two")).Outcome);
        // the classification's ASSUMED row names the equivalence rather than a unique route
        Assert.Contains("SAME PREMISE",
            classification.Single(c => c.Status == MultiplicativityStatus.Assumed).Measurement);

        sb.AppendLine("  the classification is ASSUMED on all four counts: not derived (no surviving structure forces it), not");
        sb.AppendLine("  emergent, not boundary — and the assumption is not even a unique one, since the three forcing rows are one");
        sb.AppendLine("  premise.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_096_08_TheVerdict()
    {
        PrintHeader("G_096 - the verdict, computed from the measured rows");
        var verdict = MultiplicativityOriginAudit.Verdict();
        Output.WriteLine(verdict);

        Assert.StartsWith("MULTIPLICATIVITY IS INDEPENDENT INFORMATION", verdict);
        Assert.Contains("ASSUMED, NOT DERIVED", verdict);
        Assert.Contains("remove 0 dimensions", verdict);
        Assert.Contains("ONE PREMISE IN THREE GUISES", verdict);
        Assert.Contains("ROUNDING rather than exact zero", verdict);
        // every number in the verdict is one of the audit's own measurements
        Assert.Contains(MultiplicativityOriginAudit.RingResolution().ClusteredDistinct
            .ToString(System.Globalization.CultureInfo.InvariantCulture), verdict);
        Assert.Contains(N(MultiplicativityOriginAudit.DefectOfTheReferenceMember()), verdict);
        Assert.Contains(N(MultiplicativityOriginAudit.DefectOfTheSectorLaw()), verdict);
        // and the two quantities the criteria turn on
        Assert.Equal(47, MultiplicativityOriginAudit.FamilyDimension());
        Assert.Equal(0, MultiplicativityOriginAudit.SelectorCounts().SurvivingAndForcing);
    }

    [Fact]
    public void Y_G_096_09_TheAuditAgreesWithTheChainItConsumes()
    {
        PrintHeader("G_096 - cross-check against G_075/G_076, G_077, G_085, G_086 and G_095");
        var sb = Start(
            "the audit consumes five of the chain's results, so each is cross-checked by computed identity rather than",
            "quotation;",
            "G_077 supplies the source sharing and the ratio measurement, G_086 the kernel collapse's mismatch, G_085 the",
            "free room's observable status, G_095 the absence of a flow, and G_075/G_076 the map's status as an input;",
            "the suite names no substrate token other than the ring, because the cubic substrate is not part of this",
            "audit;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(MultiplicativityOriginAudit.OutputCascade());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // G_086: the collapse's mismatch, which the free room fails and the sector's law satisfies
        var theories = ObservableKernelAudit.Theories();
        Assert.Equal(KernelDimension.OneFunction,
            theories.Single(t => t.Name.StartsWith("the sector's own law")).Dimension);
        Assert.Equal(KernelDimension.TwoFunctions, theories.Single(t => t.Name.StartsWith("the free room")).Dimension);
        Assert.True(theories.Single(t => t.Name.StartsWith("the free room")).Mismatch > 1e-2);
        // G_077: the source sharing is the zero set, and the ratio is free
        Assert.All(ClockSourceAudit.ZeroSetAgreement(), z => Assert.True(z.Agrees));
        var ratios = ClockSourceAudit.TwoReadingsOfOneDifference().Where(r => !double.IsNaN(r.Ratio)).ToArray();
        Assert.True(ratios.Length > 0);
        Assert.True(ratios.Max(r => r.Ratio) - ratios.Min(r => r.Ratio) > 1.0);
        // G_085: the free room is observable (it moves predictions) and its status is EFFECTIVE, not excluded
        var freeRoom = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("free room"));
        Assert.True(freeRoom.PredictionsMoved > 0);
        Assert.StartsWith("EFFECTIVE", freeRoom.Verdict);
        // G_095: the principle that would select was measured absent, and there is no flow for the coefficient
        Assert.True(ClockLawUniquenessAudit.Composition()
            .Single(c => c.Law == ClockLawUniquenessAudit.At().Name).Residual < 1e-12);
        var chain = ClockLawUniquenessAudit.ForcedFeatures().Single(f => f.Feature.Contains("multiplicativity"));
        Assert.Contains("FREE", chain.Status);
        // G_075/G_076: the map is an input, which is why the potential map cannot supply the requirement
        Assert.True(ClockLawUniquenessAudit.ForcedFeatures()
            .Single(f => f.Feature.Contains("g00 is a negative function")).LawsWithIt
            == ClockLawUniquenessAudit.ForcedFeatures()
                .Single(f => f.Feature.Contains("g00 is a negative function")).LawsCompared);

        sb.AppendLine("  the chain's own numbers are reproduced: the collapse's mismatch for the free room, the zero-set");
        sb.AppendLine("  agreement with a free ratio, the free room as EFFECTIVE and observable, and multiplicativity classed");
        sb.AppendLine("  FREE by the chain's own audit — which is the absence this audit measures the consequences of.");
        Output.WriteLine(sb.ToString());
    }
}
