using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_003 — Photon Ontology Audit (group E — Electromagnetism).
///
/// QUESTION. What would the photon BE in AT? Trace Difference -> Actualization -> Spectrum -> ? and test the
/// five requirements: massless, spin-1, gauge invariance, Maxwell limit, propagation.
///
/// CRITICAL QUESTION. Can AT supply a photon WITHOUT importing U(1) — is there an AT-native route to the
/// electromagnetic sector of the kind D96^3 provides elsewhere?
///
/// ANSWER: **BOUNDARY — and the constructive question has a real answer.**
///
///  (1) THE OBSTRUCTION IS REPRESENTATION-THEORETIC. The photon is spin-1, hence the l = 1 (vector/p-wave)
///      sector, hence THREE-dimensional. The single D96 ring's group has irreps of dimension 1 and 2 only
///      (4 one-dim + 47 two-dim, 4*1 + 47*4 = 192). AT's own M_011 found no vector sector on the ring; M_012
///      found a genuine one on the CUBIC network C96 box C96 box C96 (l = 1 -> T1(3), (chi,chi) = 1).
///  (2) THE GRAVITON IS BLOCKED BY THE SAME THING. The traceless metric is l = 2 -> E(2) + T2(3), also needing
///      a dimension-3 multiplet. G_033 showed that requirement was ABSORBED into the primitive eta rather than
///      filled — and G_032 proved eta is assumed. So the photon inherits gravity's gap precisely.
///  (3) WHAT AT SUPPLIES NATIVELY IS REAL. U(1) = Z_96 = the rotation subgroup of Aut(C96(1..6)), verified on
///      the permutations (r order 96, s order 2, s r s^-1 = r^-1, group order 192). Z_96 is FINITE, so the
///      emergent U(1) is COMPACT — the property that lets a compact U(1) with a conserved current carry a
///      massless vector without a Goldstone boson.
///  (4) BUT IT IS GLOBAL, NOT LOCAL. The rotation is an adjacency automorphism; a site-dependent phase is not.
///      A photon is a connection on spacetime links, not a rigid symmetry.
///  (5) THE POLARISATION COUNT DECIDES IT. A massless spin-1 has TWO transverse polarisations. A scalar phase
///      gives one longitudinal mode; grad(rho) gives one. Only a gauge-reduced A_mu gives 2.
///  (6) ONE PROGRAMME CLAIM IS FALSIFIED: the twelve link-offsets of C96(1..6) are NOT the twelve gauge
///      generators — they are not closed under addition (6 + 6 = 12) and their algebra is abelian, while
///      u(1) + su(2) + su(3) is 11-dimensional non-abelian. The su(2)-from-doublets construction DOES pass.
/// </summary>
public class Y_E_003_Tests : ResearchTestBase
{
    public Y_E_003_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_003_TheSingleRingCannotHostAVectorSector()
    {
        // The obstruction: spin-1 needs a dimension-3 irrep; the ring's irreps stop at 2.
        var budget = PhotonOntologyAudit.DihedralIrrepBudget(PhotonOntologyAudit.N96);
        Assert.Equal(4, budget.OneDim);
        Assert.Equal(47, budget.TwoDim);          // (n-2)/2 for n = 96 — the 47 doublets QG161 cites
        Assert.Equal(192, budget.SumOfSquares);   // the group order, so the budget is verified
        Assert.Equal(2, budget.MaxDim);

        Assert.Equal(3, PhotonOntologyAudit.VectorSectorDimension);
        Assert.Equal(2, PhotonOntologyAudit.MaxIrrepDimensionSingleRing());
        Assert.False(PhotonOntologyAudit.SingleRingCanHostVector());
        Assert.True(PhotonOntologyAudit.DihedralBudgetIsExact());

        // The ring's spectrum is SCALAR: eigenvalues pair k with n-k, giving the doublets. The pairing makes 49
        // (n/2 + 1) an UPPER bound on the distinct count — the actual number can be smaller through accidental
        // degeneracies, and is. (An earlier draft asserted >= 49 and failed; the bound is one-sided.)
        Assert.True(PhotonOntologyAudit.SpectrumPairsUnderReflection());
        int distinct = PhotonOntologyAudit.DistinctRingEigenvalues();
        Assert.True(distinct <= 49, $"distinct {distinct}");
        Assert.True(distinct >= 2, $"distinct {distinct}");
    }

    [Fact]
    public void Y_E_003_TheCubicSubstrateSuppliesTheVectorSector()
    {
        // l = 0 -> A1 (the density), l = 1 -> T1(3) (THE PHOTON), l = 2 -> E + T2 (the graviton).
        var scalar = PhotonOntologyAudit.SubductOntoOctahedral(0);
        Assert.Single(scalar);
        Assert.Equal("A1", scalar[0].Irrep);

        var vector = PhotonOntologyAudit.SubductOntoOctahedral(1);
        Assert.Contains(vector, p => p.Irrep == "T1" && p.Dimension == 3);

        var tensor = PhotonOntologyAudit.SubductOntoOctahedral(2);
        Assert.Contains(tensor, p => p.Irrep == "E" && p.Dimension == 2);
        Assert.Contains(tensor, p => p.Irrep == "T2" && p.Dimension == 3);

        Assert.False(PhotonOntologyAudit.HasDimensionThreeMultiplet(0));
        Assert.True(PhotonOntologyAudit.HasDimensionThreeMultiplet(1));
        Assert.True(PhotonOntologyAudit.HasDimensionThreeMultiplet(2));

        // The photon and the graviton's traceless part are blocked by the SAME missing thing.
        var ladder = PhotonOntologyAudit.SpinLadder();
        Assert.Equal(3, ladder.Length);
        Assert.True(ladder[0].Ring && ladder[0].Cubic);          // l = 0 lives on the ring
        Assert.False(ladder[1].Ring);                            // the photon does NOT
        Assert.True(ladder[1].Cubic);
        Assert.False(ladder[2].Ring);                            // nor the graviton
        Assert.True(ladder[2].Cubic);
    }

    [Fact]
    public void Y_E_003_TheU1IsDerivedCompactButGlobal()
    {
        // D96 = Aut(C96(1..6)), verified on the permutations rather than quoted.
        var (rotOrder, refOrder, relation, order) = PhotonOntologyAudit.DihedralRelations();
        Assert.Equal(96, rotOrder);
        Assert.Equal(2, refOrder);
        Assert.True(relation);          // s r s^-1 = r^-1
        Assert.Equal(192, order);       // so "D96" means the dihedral group of order 192

        Assert.True(PhotonOntologyAudit.GaugeGroupIsCompact());   // Z_96 finite => compact

        // But the symmetry is RIGID: the rotation is an automorphism, site-dependent phases are not.
        var (rotIsAuto, siteWorks, siteTried) = PhotonOntologyAudit.GlobalOrLocal();
        Assert.True(rotIsAuto);
        Assert.Equal(6, siteTried);
        Assert.Equal(0, siteWorks);
    }

    [Fact]
    public void Y_E_003_TheTwelveGeneratorClaimFailsButSu2Holds()
    {
        // QG161: "the 12 link-directions ARE the 12 gauge generators" (matching 1 + 3 + 8 = 12).
        Assert.Equal(12, PhotonOntologyAudit.LinkOffsets().Length);
        Assert.False(PhotonOntologyAudit.LinkOffsetsClosedUnderAddition());

        var witness = PhotonOntologyAudit.FirstClosureFailure();
        // The witness is whatever the scan finds first; assert it is a GENUINE failure rather than a specific
        // pair, so the claim is tested structurally. (An earlier version hard-coded 6 + 6 and failed: the scan
        // reaches 1 + 95 = 0 first, which is also outside the set.)
        Assert.False(PhotonOntologyAudit.LinkOffsets().Contains(witness.Sum));
        Assert.Equal((witness.A + witness.B) % PhotonOntologyAudit.N96, witness.Sum);

        var (closureSize, abelian, nonAbelian) = PhotonOntologyAudit.LinkOffsetAlgebra();
        Assert.Equal(96, closureSize);   // the offsets generate all of Z_96, not a 12-dimensional algebra
        Assert.True(abelian);
        Assert.Equal(11, nonAbelian);    // dim su(3) + su(2) = 8 + 3 — the part the offsets cannot supply

        // But the su(2)-from-doublets claim DOES pass a structural test.
        Assert.Equal(3, PhotonOntologyAudit.DoubletLieAlgebraDimension());
    }

    [Fact]
    public void Y_E_003_PolarisationsPinTheMissingPrimitive()
    {
        Assert.Equal(2, PhotonOntologyAudit.PhotonPolarisations());
        Assert.Equal(1, PhotonOntologyAudit.PhysicalPolarisations("scalar phase"));
        Assert.Equal(1, PhotonOntologyAudit.PhysicalPolarisations("gradient"));
        Assert.Equal(2, PhotonOntologyAudit.PhysicalPolarisations("A_mu"));
        Assert.Equal(3, PhotonOntologyAudit.PhysicalPolarisations("Proca"));

        // The missing primitive is stated precisely: not "a phase" but a SPACETIME-indexed, dynamical one.
        Assert.Contains("SPACETIME index", PhotonOntologyAudit.MissingPrimitive());
        Assert.Contains("DYNAMICS", PhotonOntologyAudit.MissingPrimitive());

        var reqs = PhotonOntologyAudit.Requirements();
        Assert.Equal(5, reqs.Length);
        Assert.Equal("ABSENT", reqs.Single(r => r.Requirement == "light propagation").Status);

        Assert.Equal("BOUNDARY", PhotonOntologyAudit.Verdict());
    }

    [Fact]
    public void Y_E_003_AtAlreadyHasThePhaseAndExactlyOneMasslessScalarMode()
    {
        // CORRECTION TO A FIRST READING: AT already carries a U(1) link phase natively (PhaseOrigin).
        Assert.Equal(2.0 * Math.PI / 96.0, PhotonOntologyAudit.PhaseQuantum(PhotonOntologyAudit.N96), 15);
        var (fwd, bwd) = PhotonOntologyAudit.LinkPhase(PhotonOntologyAudit.N96);
        Assert.Equal(PhotonOntologyAudit.PhaseQuantum(PhotonOntologyAudit.N96), fwd, 15);
        Assert.Equal(-fwd, bwd, 15);

        Assert.Equal(2.0 * Math.PI, PhotonOntologyAudit.PathPhase(PhotonOntologyAudit.N96, PhotonOntologyAudit.N96), 12);
        Assert.Equal(0.0, PhotonOntologyAudit.LoopHolonomy(PhotonOntologyAudit.N96, PhotonOntologyAudit.N96), 12);
        Assert.True(PhotonOntologyAudit.HolonomyIsCompact());
        Assert.Equal(4.0, PhotonOntologyAudit.InterferenceLaw(0.0), 12);
        Assert.Equal(0.0, PhotonOntologyAudit.InterferenceLaw(Math.PI), 12);

        // The substrate's Laplacian has EXACTLY ONE zero eigenvalue, and it is the scalar constant mode.
        Assert.Equal(1, PhotonOntologyAudit.ZeroModeCount());
        Assert.Equal(0, PhotonOntologyAudit.ZeroModeSpin());
        Assert.True(PhotonOntologyAudit.SpectralGap() > 1e-9);

        // And AT has no COMPUTED massless spin-1 mode; its spin-2 is postulated, not computed.
        Assert.True(PhotonOntologyAudit.NoComputedSpin1MasslessMode());
        var modes = PhotonOntologyAudit.MasslessModeInventory();
        Assert.Contains(modes, m => m.Spin == 2 && m.Status.StartsWith("POSTULATED", StringComparison.Ordinal));
        Assert.Contains(modes, m => m.Spin == 1 && m.Status == "ABSENT");
        Assert.Equal(2, modes.Count(m => m.Spin == 0 && m.Status == "COMPUTED"));

        // The missing primitive is therefore NOT "a phase" — which the wording must reflect.
        Assert.Contains("SPACETIME index", PhotonOntologyAudit.MissingPrimitive());
        Assert.Contains("already has the phase", PhotonOntologyAudit.MissingPrimitive());
    }

    [Fact]
    public void Y_E_003_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_003 — Photon Ontology Audit: what would the photon BE in AT?");

        sb.AppendLine("QUESTION. Trace Difference -> Actualization -> Spectrum -> ? and test the five requirements:");
        sb.AppendLine("   1. massless   2. spin-1   3. gauge invariance   4. Maxwell limit   5. light propagation");
        sb.AppendLine();
        sb.AppendLine("CRITICAL QUESTION. Can AT supply a photon WITHOUT importing U(1) — is there an AT-native");
        sb.AppendLine("route to the electromagnetic sector, of the kind D96^3 provides elsewhere?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. \"D96\" means Aut(C96(1..6)), the dihedral group of order 192 — verified below on the");
        sb.AppendLine("     permutations, not quoted.");
        sb.AppendLine("  2. Spin-1 for a massless particle means the l = 1 (vector / p-wave) representation, hence a");
        sb.AppendLine("     three-dimensional multiplet under whatever spatial group is available.");
        sb.AppendLine("  3. Polarisation counting uses the standard component-minus-gauge reduction.");
        sb.AppendLine("  4. Deterministic: closed-form characters and permutations, no RNG, invariant-culture output.");
        sb.AppendLine();

        PrintHeader(PhotonOntologyAudit.OutputObstruction());
        PrintHeader(PhotonOntologyAudit.OutputGauge());
        PrintHeader(PhotonOntologyAudit.OutputNativeAssets());
        PrintHeader(PhotonOntologyAudit.OutputPolarisation());
        PrintHeader(PhotonOntologyAudit.OutputRequirements());

        PrintHeader("5. VERDICT");
        sb.AppendLine($"  {PhotonOntologyAudit.Verdict()}");
        sb.AppendLine();
        sb.AppendLine("  " + PhotonOntologyAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  THE CRITICAL QUESTION, ANSWERED");
        sb.AppendLine("   · AT does NOT have to import U(1) as an axiom: the rotation subgroup of Aut(C96(1..6)) IS a");
        sb.AppendLine("     Z_96, it is COMPACT, and the weak sector has a genuine su(2) on the doublets.");
        sb.AppendLine("   · AT CANNOT supply the photon without the cubic substrate, and it does not build it: its own");
        sb.AppendLine("     M_011/M_012 prove the vector sector needs 96^3, and G_032/G_033 prove 3D space is imported");
        sb.AppendLine("     as the assumed primitive eta.");
        sb.AppendLine("   · The photon and the graviton are blocked by the SAME missing substrate — which is the most");
        sb.AppendLine("     useful result here, because it means one construction would unblock both.");
        sb.AppendLine("   · The missing primitive is exactly ONE: a phase on spacetime links (locality).");

        Output.WriteLine(sb.ToString());
    }
}
