using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ConnectionOriginAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_006 - Connection Origin Audit (group E - Electromagnetism).
///
/// QUESTION. What AT structure could supply the missing direction index that E_005 located? The candidate must be
/// (1) local, (2) directional, (3) first-order, (4) act on T1 and T2, and (5) require NO NEW PRIMITIVE.
/// Candidates: the D96 ring derivative, the D96^3 edge connection, occupancy gradients, actualization flow and
/// causal-order links. For each, determine representation / kinematics / dynamics / gauge.
///
/// ANSWER: **DERIVED - the unique missing object between representation and field theory is the D96^3 EDGE
/// CONNECTION, and it needs no new primitive.**
///
///  (1) ALL FIVE REQUIREMENTS ARE COMPUTED FOR IT: local (support 2 per row); directional (rank 3, against the
///      ring's 1); first-order (the summed squared symbols equal the cube Laplacian's symbol, residual ~0); acts on
///      T1 and T2 - the tensor square of the vector irrep separates, via the octahedral characters, into an
///      ANTISYMMETRIC part that IS the vector irrep (the photon's field strength) and a SYMMETRIC part that IS
///      A1 + E + T2 (the trace plus the graviton's sector); and no new primitive (the tensor product of the ring's
///      own differences, three times over).
///  (2) THE FOUR OTHER CANDIDATES ARE REFUTED ON COMPUTED WITNESSES: the ring derivative is rank 1 and has no
///      vector sector; occupancy gradients can only ever produce a vector, so they cannot reach the symmetric
///      traceless sector and cannot act ON T1; actualization flow objects are report models, not fields, and a flow
///      is one vector per site; causal-order links are not local (closure reaches 95 of 96 cells against a support
///      of 2), carry no index and have no dispersion.
///  (3) THE BOUNDARY INSIDE THE VERDICT: a live scan of AT's sources finds no product-lattice construction in code,
///      so the object is DERIVABLE BUT NOT INSTANTIATED; and no member computes a field strength, so nothing
///      couples it to a field. Hence representation SATISFIED already, KINEMATICS DERIVED HERE, dynamics and gauge
///      STILL MISSING.
/// </summary>
public class Y_E_006_Tests : ResearchTestBase
{
    public Y_E_006_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_006_TheFiveRequirementsAreStatedAndTested()
    {
        Assert.Equal(5, Requirements.Length);
        Assert.Equal(new[] { "local", "directional", "first-order", "acts on T1 and T2", "no new primitive" }, Requirements);
        Assert.Equal(5, CandidateMeasurements().Length);
        Assert.All(CandidateMeasurements(), c => Assert.Equal(5, Requirements.Length));
    }

    [Fact]
    public void Y_E_006_TheEdgeConnectionIsLocalDirectionalAndFirstOrder()
    {
        // local: two entries per row, nearest neighbour
        Assert.Equal(2, EdgeSupportPerRow());
        Assert.True(TheEdgeConnectionIsLocal());

        // directional: the three factor differences are independent
        Assert.Equal(3, EdgeDirectionRank());
        Assert.Equal(3, SubstrateDimensionAudit.VectorSpaceDimension(3));
        Assert.True(TheEdgeConnectionIsDirectional());

        // first-order: its square IS the Laplacian, and the symbol is linear rather than quadratic
        Assert.True(TheEdgeConnectionIsFirstOrder());
        Assert.True(FirstOrderResidual() < 1e-12);
        Assert.True(TheSymbolIsLinear());
        foreach (var (k, ratio, quadrature) in SymbolLinearity())
        {
            Assert.True(Math.Abs(ratio - 1.0) < 1e-3);   // |sigma(k)|/k -> 1, all three k
            Assert.True(quadrature > 5.0);               // |sigma(k)|/k^2 diverges
            Assert.True(k > 0);
        }

        // while the ring's own derivative is rank 1
        Assert.Equal(1, PropagationOriginAudit.RingDirectionRank());
    }

    [Fact]
    public void Y_E_006_OneDerivativeReachesBothThePhotonAndTheGravitonSector()
    {
        // the character table of the tensor square, class by class
        var characters = TensorSquareCharacters();
        Assert.Equal(5, characters.Length);
        Assert.Equal(24, GroupOrder());

        // the antisymmetric square IS the vector irrep - the photon's field strength
        var antisymmetric = AntisymmetricSquare();
        Assert.Single(antisymmetric);
        Assert.Equal(("T1", 1), antisymmetric[0]);
        Assert.Equal(3, AntisymmetricSquareDimension());

        // the symmetric square IS the trace plus the graviton's sector
        var symmetric = SymmetricSquare();
        Assert.Equal(3, symmetric.Length);
        Assert.Contains(symmetric, r => r.Irrep == "A1" && r.Multiplicity == 1);
        Assert.Contains(symmetric, r => r.Irrep == "E" && r.Multiplicity == 1);
        Assert.Contains(symmetric, r => r.Irrep == "T2" && r.Multiplicity == 1);
        Assert.Equal(6, SymmetricSquareDimension());

        // and the dimensions add up to the tensor square
        Assert.Equal(9, AntisymmetricSquareDimension() + SymmetricSquareDimension());
        Assert.True(OneDerivativeReachesBothSectors());
        Assert.Contains("antisymmetric", DerivativeActionOnTheVectorSector(), StringComparison.Ordinal);
    }

    [Fact]
    public void Y_E_006_ExactlyOneCandidateDerivesAndFourAreRefuted()
    {
        Assert.True(ExactlyOneCandidateDerives());
        Assert.Equal(4, RefutedCandidates().Length);
        Assert.Equal("D96^3 edge connection", DerivingCandidate());

        // each refuted candidate fails for its own computed reason
        Assert.Contains("D96 ring derivative", RefutedCandidates());
        Assert.Contains("occupancy gradients", RefutedCandidates());
        Assert.Contains("actualization flow", RefutedCandidates());
        Assert.Contains("causal-order links", RefutedCandidates());

        var verdicts = CandidateVerdicts().ToDictionary(v => v.Candidate, v => v.Verdict);
        Assert.Contains("direction rank 1", verdicts["D96 ring derivative"], StringComparison.Ordinal);
        Assert.Contains("cannot act on T1 and T2", verdicts["occupancy gradients"], StringComparison.Ordinal);
        Assert.Contains("not local", verdicts["causal-order links"], StringComparison.Ordinal);
        Assert.Contains("not first-order", verdicts["actualization flow"], StringComparison.Ordinal);

        // and every row carries a measured witness
        Assert.Equal(5, CandidateEvidence().Length);
        Assert.All(CandidateEvidence(), e => Assert.False(string.IsNullOrWhiteSpace(e.Evidence)));
    }

    [Fact]
    public void Y_E_006_TheRefutationsAreMeasuredRatherThanAsserted()
    {
        // a causal order is not local: one step touches 2 cells, the closure touches every other one
        Assert.Equal(2, DirectLinkSupport());
        Assert.Equal(95, CausalClosureSupport());
        Assert.False(CausalOrderLinksAreLocal());

        // a scalar-derived connection has no field strength: partial derivatives commute
        var (antisymmetric, traceless) = DoubleGradientOfAScalar();
        Assert.True(antisymmetric < 1e-12, $"antisymmetric part {antisymmetric:E3}");
        Assert.True(traceless > 1e-3, $"traceless part {traceless:E3}");
        Assert.True(AScalarDerivedConnectionHasNoFieldStrength());

        // and AT computes no flow field at all
        Assert.Equal(0, FlowMembersReturningAField());
        Assert.True(TheFlowCandidateIsNotAField());
    }

    [Fact]
    public void Y_E_006_TheObjectIsDerivableButNotInstantiated()
    {
        // the live scan finds no product-lattice construction in AT's code
        var scan = InstantiationScan();
        Assert.Equal(4, scan.Length);
        var witnesses = InstantiationWitnesses();
        Assert.True(witnesses.Length == 0,
            "scan witnesses: " + string.Join(" | ", witnesses.Select(w => $"{w.File}:{w.Line} [{w.Token}] {w.Text}")));
        Assert.Equal(0, scan.Sum(t => t.CodeLines));
        Assert.True(TheObjectIsDerivableButNotInstantiated());

        // and nothing couples a derivative to a field
        Assert.Equal(0, CouplingMemberCount());
        Assert.True(NoMemberCouplesTheDerivativeToAField());
    }

    [Fact]
    public void Y_E_006_OnlyKinematicsIsDerivedAndTheVerdictIsDerived()
    {
        var layers = LayerVerdicts();
        Assert.Equal(4, layers.Length);
        Assert.Equal(new[] { "representation", "kinematics", "dynamics", "gauge" }, layers.Select(l => l.Layer).ToArray());
        Assert.StartsWith("SATISFIED", layers[0].Status, StringComparison.Ordinal);
        Assert.StartsWith("DERIVED", layers[1].Status, StringComparison.Ordinal);
        Assert.StartsWith("STILL MISSING", layers[2].Status, StringComparison.Ordinal);
        Assert.StartsWith("STILL MISSING", layers[3].Status, StringComparison.Ordinal);
        Assert.Equal(new[] { "kinematics" }, DerivedLayers());
        Assert.True(OnlyKinematicsIsDerived());

        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_E_006_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_006 - Connection Origin Audit: what AT structure supplies the direction index?");

        sb.AppendLine("QUESTION. What AT structure could supply the missing direction index that E_005 located?");
        sb.AppendLine("REQUIREMENTS  1 local | 2 directional | 3 first-order | 4 acts on T1 and T2 | 5 no new primitive");
        sb.AppendLine("CANDIDATES    D96 ring derivative | D96^3 edge connection | occupancy gradients |");
        sb.AppendLine("              actualization flow | causal-order links");
        sb.AppendLine("GOAL          locate the unique missing object between representation and field theory");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. E_005's finding is the starting point: the bottleneck is a first-order derivative carrying");
        sb.AppendLine("     a direction index, and its first failing layer is kinematics.");
        sb.AppendLine("  2. 'Directional' means the operator's own index space has rank >= 3, the size at which a");
        sb.AppendLine("     vector index and a traceless symmetric rank-2 can both exist.");
        sb.AppendLine("  3. 'Acts on T1 and T2' means the operator's DOMAIN includes the vector sector and its image");
        sb.AppendLine("     contains the traceless symmetric sector - tested through the octahedral characters of the");
        sb.AppendLine("     tensor square, with the antisymmetric part separated by (chi(g)^2 - chi(g^2))/2.");
        sb.AppendLine("  4. 'No new primitive' is tested by construction: the object must be built from structures AT");
        sb.AppendLine("     already has, and a live code scan checks whether it is instantiated.");
        sb.AppendLine("  5. Deterministic throughout; the wave-vector sample is a fixed grid and the scan strips");
        sb.AppendLine("     comments and string literals per line before matching.");
        sb.AppendLine();

        PrintHeader(OutputRequirements());
        PrintHeader(OutputTheObject());
        PrintHeader(OutputBoundary());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
