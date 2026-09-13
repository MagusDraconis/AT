using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_004 — Vector Sector Audit (group E — Electromagnetism).
///
/// QUESTION. E_003 proved the obstruction is representation-theoretic: spin-1 needs l = 1, hence three
/// dimensions, which the single D96 ring cannot supply (irrep dimensions stop at 2) while the cubic substrate
/// D96^3 supplies T1(3). But a REPRESENTATION is not a FIELD. Can the T1(3) sector support a genuine spin-1
/// field?
///
/// ANSWER: **BOUNDARY — and the shortfall is precise.**
///
///  (1) T1(3) IS REAL, IRREDUCIBLE, AND ONLY ON D96^3. Computed: l = 1 subducts to T1 with multiplicity 1 and
///      dimension 3, so it is irreducible. The single ring has no dimension-3 irrep at all (max 2).
///      Requirement 1 is satisfied — by the cubic substrate only.
///  (2) A LORENTZ 4-VECTOR SPLITS ACROSS BOTH SUBSTRATES: A_mu reduces as 1 (timelike, A1) + 3 (spatial, T1) = 4.
///      The ring can carry only A_0, the cubic substrate only A_i. NEITHER ALONE CAN CARRY A LORENTZ VECTOR.
///  (3) THE TRANSVERSE PROJECTOR has rank 2 for every momentum direction, so 3 components reduce to 2 physical
///      states — but only if the longitudinal mode is removed, which needs a reason.
///  (4) THE DECISIVE COMPUTATION — THE REPRESENTATION DOES NOT SUPPLY THAT REASON. Two quadratic forms live on
///      the SAME three-dimensional space: the IDENTITY form (rank 3, no kernel — three states, a PROCA field,
///      and it is the representation's own natural invariant) and the CURL form (rank 2, one-dimensional kernel
///      — two states, MAXWELL-capable). The curl form annihilates the longitudinal mode EXACTLY. An irreducible
///      representation has no gauge orbit: the gauge direction lives in the FIELD SPACE.
///  (5) MASSLESSNESS IS A CONTINUUM-LIMIT STATEMENT. The vector sector is gapped — 0.386351 at N = 96 — but the
///      gap scales as c/n^2 with c ~ 3592, so it is a finite-size artefact and closes in the limit.
///  (6) THE MAXWELL LIMIT IS E_002's result: derivable, from imported premises.
///
/// So D96^3 is necessary AND sufficient at the level of REPRESENTATION, and insufficient at the level of
/// DYNAMICS. What is missing is the CHOICE OF KINETIC FORM that reduces three states to two.
/// </summary>
public class Y_E_004_Tests : ResearchTestBase
{
    public Y_E_004_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_004_TheVectorSectorExistsOnlyOnD96Cubed()
    {
        // l = 1 subducts to a single irreducible T1 multiplet of dimension 3.
        var content = VectorSectorAudit.VectorContent();
        Assert.Single(content);
        Assert.Equal("T1", content[0].Irrep);
        Assert.Equal(3, content[0].Dimension);
        Assert.Equal(1, content[0].Multiplicity);
        Assert.True(VectorSectorAudit.VectorSectorIsIrreducible());
        Assert.Equal(3, VectorSectorAudit.VectorSectorDimension());

        // The single ring cannot supply it — its irreps stop at dimension 2.
        Assert.Equal(2, VectorSectorAudit.SingleRingMaxIrrepDimension());
        Assert.False(VectorSectorAudit.SingleRingHasVectorSector());
        Assert.True(VectorSectorAudit.CubicHasVectorSector());
        Assert.True(VectorSectorAudit.ComparisonShowsCubicIsNecessary());
    }

    [Fact]
    public void Y_E_004_ALorentzVectorNeedsBothSubstrates()
    {
        // A_mu = (A_0, A_i) reduces as 1 (A1) + 3 (T1) = 4.
        var split = VectorSectorAudit.LorentzVectorSplit();
        Assert.Equal(2, split.Length);
        Assert.Equal(("timelike A_0", "A1", 1), split[0]);
        Assert.Equal(("spatial A_i", "T1", 3), split[1]);
        Assert.Equal(4, VectorSectorAudit.LorentzVectorDimension());

        // And the two parts come from DIFFERENT substrates.
        var assign = VectorSectorAudit.SubstrateAssignment();
        Assert.Equal(2, assign.Length);
        Assert.Contains("D96 ring", assign[0].SuppliedBy);
        Assert.Contains("cubic", assign[1].SuppliedBy);
    }

    [Fact]
    public void Y_E_004_TheTwoFormsDisagreeOnTheSameRepresentation()
    {
        // The transverse projector has rank 2 in every direction tested.
        var (transverse, total) = VectorSectorAudit.PolarisationRanks();
        Assert.Equal(2, transverse);
        Assert.Equal(3, total);
        Assert.Equal(2, VectorSectorAudit.TransverseCount());
        Assert.Equal(1, VectorSectorAudit.LongitudinalCount());

        // Two forms on the SAME three-dimensional space, with different ranks.
        Assert.Equal((3, 0), VectorSectorAudit.IdentityFormRankAndKernel());   // Proca
        Assert.Equal((2, 1), VectorSectorAudit.CurlFormRankAndKernel());       // Maxwell-capable

        // The curl form annihilates the longitudinal mode EXACTLY; the identity form does not.
        var f = VectorSectorAudit.FormsOnModes();
        Assert.Equal(1.0, f.IdentityLong, 12);
        Assert.Equal(0.0, f.CurlLong, 12);
        Assert.Equal(1.0, f.IdentityTrans, 12);
        Assert.Equal(2.0, f.CurlTrans, 12);

        // An irreducible representation supplies NO gauge direction; the field space supplies one.
        Assert.Equal(0, VectorSectorAudit.GaugeDirectionsFromRepresentation());
        Assert.Equal(1, VectorSectorAudit.GaugeDirectionsFromFieldSpace());

        Assert.Equal("BOUNDARY", VectorSectorAudit.Verdict());
    }

    [Fact]
    public void Y_E_004_TheGapClosesOnlyInTheContinuumLimit()
    {
        // The vector sector is gapped at finite N...
        Assert.Equal(0.386351, VectorSectorAudit.RingGap(96), 4);
        Assert.Equal(3.0 * VectorSectorAudit.RingGap(96), VectorSectorAudit.CubicGap(96), 10);

        // ...but the gap falls as 1/n^2, so it is a finite-size artefact, not a mass.
        var table = VectorSectorAudit.GapScaling();
        Assert.Equal(6, table.Length);
        Assert.True(table[0].Gap > table[^1].Gap * 100.0);          // strictly falling, and by a lot
        Assert.True(VectorSectorAudit.GapScalingSpreadLargeN() < 0.05);
        Assert.True(VectorSectorAudit.GapCoefficient() > 3000.0);
        Assert.True(VectorSectorAudit.GapCoefficient() < 4000.0);
        Assert.True(VectorSectorAudit.GapClosesInContinuumLimit());
    }

    [Fact]
    public void Y_E_004_TheComparisonAcrossTheFiveRequirements()
    {
        var rows = VectorSectorAudit.Comparison();
        Assert.Equal(5, rows.Length);
        Assert.Contains("NONE", rows[0].SingleRing);
        Assert.Contains("T1(3)", rows[0].Cubic);
        Assert.Contains("derivable", rows[4].Cubic);

        Assert.Contains("KINETIC FORM", VectorSectorAudit.KineticFormDecision());
        Assert.Contains("NEITHER SUBSTRATE ALONE", VectorSectorAudit.WhereItStands());

        Assert.Equal("BOUNDARY", VectorSectorAudit.Verdict());
    }

    [Fact]
    public void Y_E_004_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_004 — Vector Sector Audit: can the D96^3 T1(3) sector support a spin-1 field?");

        sb.AppendLine("QUESTION. E_003 showed the vector sector requires the cubic substrate because spin-1 needs");
        sb.AppendLine("l = 1, hence three dimensions, and the single ring's irreps stop at two. But a REPRESENTATION");
        sb.AppendLine("is not a FIELD. Can T1(3) support a genuine spin-1 field?");
        sb.AppendLine();
        sb.AppendLine("REQUIREMENTS");
        sb.AppendLine("  1. vector degrees of freedom   2. two physical polarisations   3. gauge redundancy");
        sb.AppendLine("  4. massless propagation        5. Maxwell limit");
        sb.AppendLine("COMPARE: single D96 against D96^3.");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Spin-1 is the l = 1 (vector / p-wave) representation; the octahedral content is computed");
        sb.AppendLine("     by character inner product, not quoted.");
        sb.AppendLine("  2. Physical-state counting is the standard component-minus-gauge reduction.");
        sb.AppendLine("  3. Plane waves with |k| = 1 are used for the two competing quadratic forms.");
        sb.AppendLine("  4. Deterministic: closed-form characters, permutations and matrix ranks; no RNG.");
        sb.AppendLine();

        PrintHeader(VectorSectorAudit.OutputVectorSector());
        PrintHeader(VectorSectorAudit.OutputPolarisations());
        PrintHeader(VectorSectorAudit.OutputMasslessness());
        PrintHeader(VectorSectorAudit.OutputComparison());

        PrintHeader("5. VERDICT");
        sb.AppendLine($"  {VectorSectorAudit.Verdict()}");
        sb.AppendLine();
        sb.AppendLine("  " + VectorSectorAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  THE FIVE REQUIREMENTS, ANSWERED");
        sb.AppendLine("   · 1 vector degrees of freedom : MET — T1(3) is a genuine irreducible 3-vector, on D96^3 only.");
        sb.AppendLine("   · 2 two polarisations         : MET GIVEN a reason to project — the projector has rank 2.");
        sb.AppendLine("   · 3 gauge redundancy          : NOT MET — an irrep has no orbit; the identity form gives 3");
        sb.AppendLine("                                    states (Proca), the curl form 2 (Maxwell). The choice between");
        sb.AppendLine("                                    them is the missing kinetic-form structure.");
        sb.AppendLine("   · 4 massless propagation      : MET IN THE LIMIT — gap ~ 3592/n^2 -> 0, a finite-size artefact.");
        sb.AppendLine("   · 5 Maxwell limit             : IMPORTED — E_002's derivation, from premises not AT's.");

        Output.WriteLine(sb.ToString());
    }
}
