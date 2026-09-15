using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_033 — Cubic Substrate Audit (the D96³ requirement check).
///
/// QUESTION. The programme established that D96 ⊗ D96 ⊗ D96 ("D96³") is required to calculate and explain
/// physics — M_012 proved a genuine 3D (vector / p-wave, l = 1) sector exists on the cubic lattice and is
/// **absent from a single D96 ring, whose maximum irrep dimension is 2**. Does the search for gravitation and
/// time (group G) show the same behaviour?
///
/// ANSWER: **PARTIAL.** The requirement is **present in structure** but **era-local in practice**, and it was
/// absorbed into the primitive η rather than exercised.
///
///  (1) ERA-LOCAL IN CODE — the scan re-reads the group-G sources at test time. The density era
///      (G_001–G_018, G_023/024/026) computes with D96 / D96³; the whole metric / closure era
///      (G_015, G_017, G_019–G_022, G_025, G_027–G_032) contains no D96 reference at all.
///  (2) STRUCTURALLY INHERITED — the spatial metric is a symmetric rank-2 tensor in d = 3: 6 = 1 (trace,
///      l = 0) + 5 (traceless, l = 2), and l = 2 subducts as Eg(2) + T2g(3). So gravity **requires a
///      dimension-3 irrep** — exactly the structure a single D96 ring cannot supply. Gravity therefore needs
///      the cubic substrate just as much as M_012's vector sector does.
///  (3) WHERE IT WENT — into η (G_032). The metric era imports 3D space instead of generating it.
///  (4) A DEFECT — the cited A₀ = 20 812 is not an invariant but a binary64-keying artifact; the robust count
///      is 16 080 (M_012's own figure, and the repository uses 868 656 = 884 736 − 16 080 elsewhere).
/// </summary>
public class Y_G_033_Tests : ResearchTestBase
{
    public Y_G_033_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_033_TheSubstrateRequirementIsEraLocal()
    {
        var all = CubicSubstrateAudit.ScanAudits();
        // The scanner must actually find the suites — otherwise the audit is vacuously green (G_027's rule).
        Assert.True(all.Length >= 34, $"scanner found only {all.Length} group-G suites; the audit would be vacuous");

        // The substrate META-audits are excluded from the classification: they compute the D96³ spectrum in
        // order to audit it, so counting them would inflate the substrate era (a self-reference the live
        // scanner exposed as soon as the first such audit existed — and again when G_034 was added).
        Assert.Contains(all, u => CubicSubstrateAudit.IsMetaAudit(u.Audit));
        var classified = CubicSubstrateAudit.AuditsSubjectToClassification();
        Assert.DoesNotContain(classified, u => CubicSubstrateAudit.IsMetaAudit(u.Audit));
        Assert.Equal(all.Length - CubicSubstrateAudit.MetaAudits.Length, classified.Length);

        var withSubstrate = CubicSubstrateAudit.AuditsUsingSubstrate();
        var commentOnly = CubicSubstrateAudit.AuditsCommentOnly();
        var without = CubicSubstrateAudit.AuditsWithoutReference();

        // BOTH classes are populated: the requirement is era-local, not programme-wide.
        Assert.NotEmpty(withSubstrate);
        Assert.NotEmpty(without);
        Assert.True(CubicSubstrateAudit.SubstrateUseIsEraLocal());
        Assert.Equal(classified.Length, withSubstrate.Length + commentOnly.Length + without.Length);

        // The metric / closure era is exactly the D96-FREE one — sixteen audits (thirteen through G_035;
        // G_036, G_037 and G_038 added the rest, and none uses a substrate). G_039 and G_040 are DENSITY-era
        // audits: both recompute the D96 ring spectrum, so they belong on the other side of the line. G_041 joined
        // them: it asks whether d = 3 is selected, which means computing the D96^d family.
        Assert.Equal(28, without.Length);
        Assert.Equal(27, withSubstrate.Length);
        Assert.Equal(2, commentOnly.Length);                 // G_001 names D96 only in comments; G_046 uses it indirectly, through G_040

        // The metric / closure era is the D96-FREE one. Spot-check its members by name.
        var bare = without.Select(u => u.Audit).ToHashSet(StringComparer.Ordinal);
        Assert.Contains("Y_G_028", bare);   // clock sector closure
        Assert.Contains("Y_G_029", bare);   // spatial sector closure
        Assert.Contains("Y_G_030", bare);   // no-go
        Assert.Contains("Y_G_031", bare);   // spatial origin
        Assert.Contains("Y_G_032", bare);   // conformal assumption
        Assert.Contains("Y_G_020", bare);   // neutron-star redshift
        Assert.Contains("Y_G_027", bare);   // literal verdict

        // ...while the density era is not.
        var withIt = withSubstrate.Select(u => u.Audit).ToHashSet(StringComparer.Ordinal);
        Assert.Contains("Y_G_002", withIt);
        Assert.Contains("Y_G_006", withIt);

        // Every scanned audit is attributed.
        Assert.All(all, u => Assert.False(string.IsNullOrWhiteSpace(u.Audit)));
    }

    [Fact]
    public void Y_G_033_GravityRequiresADimension3Irrep()
    {
        // The spatial metric: a symmetric 2-tensor in d = 3.
        var (total, trace, traceless) = CubicSubstrateAudit.SymmetricTensorContent();
        Assert.Equal(6, total);
        Assert.Equal(1, trace);
        Assert.Equal(5, traceless);
        Assert.Equal(total, trace + traceless);

        // The octahedral group O: order 24, irreps of dimension 1,1,2,3,3 — Σd² = 24.
        int sumSquares = 0;
        foreach (var (_, dimension, _) in new[] { ("A1", 1, "g"), ("A2", 1, "g"), ("E", 2, "g"), ("T1", 3, "u"), ("T2", 3, "u") })
            sumSquares += dimension * dimension;
        Assert.Equal(CubicSubstrateAudit.OctahedralOrder, sumSquares);

        // The subduction reproduces M_012 exactly: norms² = 1,1,2,3,4 and l = 2 → E + T2.
        Assert.Equal(1.0, CubicSubstrateAudit.CharacterNormSquared(0), 12);
        Assert.Equal(1.0, CubicSubstrateAudit.CharacterNormSquared(1), 12);
        Assert.Equal(2.0, CubicSubstrateAudit.CharacterNormSquared(2), 12);
        Assert.Equal(3.0, CubicSubstrateAudit.CharacterNormSquared(3), 12);
        Assert.Equal(4.0, CubicSubstrateAudit.CharacterNormSquared(4), 12);

        Assert.Equal(1, CubicSubstrateAudit.MultiplicityOf(0, "A1"));
        Assert.Equal(1, CubicSubstrateAudit.MultiplicityOf(1, "T1"));
        Assert.Equal(1, CubicSubstrateAudit.MultiplicityOf(2, "E"));
        Assert.Equal(1, CubicSubstrateAudit.MultiplicityOf(2, "T2"));

        // THE REQUIREMENT: the metric's traceless part lands on a dimension-3 irrep (T2g).
        var tracelessSector = CubicSubstrateAudit.TracelessSector();
        Assert.Equal(5, tracelessSector.Sum(p => p.Dimension * p.Multiplicity));
        Assert.Equal(3, CubicSubstrateAudit.MaxIrrepDimension(tracelessSector));
        Assert.Contains(tracelessSector, p => p.Irrep == "T2g" && p.Dimension == 3);
        Assert.Contains(tracelessSector, p => p.Irrep == "Eg" && p.Dimension == 2);
        Assert.True(CubicSubstrateAudit.MetricNeedsDim3Irrep());

        // ...and so does M_012's vector sector (T1u).
        Assert.True(CubicSubstrateAudit.VectorNeedsDim3Irrep());
        Assert.Contains(CubicSubstrateAudit.VectorSector(), p => p.Irrep == "T1u" && p.Dimension == 3);

        // A single D96 ring cannot supply it: dihedral D_96 tops out at dimension 2.
        Assert.Equal(2, CubicSubstrateAudit.SingleRingMaxIrrepDim());
        Assert.True(CubicSubstrateAudit.SingleRingMaxIrrepDim() < CubicSubstrateAudit.CubicMaxIrrepDim());
        Assert.True(CubicSubstrateAudit.RequiresCubicSubstrate());

        // The dimension is 3 for the same derived reason M_013 found: rotation self-duality.
        Assert.Equal(3, CubicSubstrateAudit.RotationSelfDualityDimension());
        Assert.True(CubicSubstrateAudit.RotationSelfDualityIsUnique());
    }

    [Fact]
    public void Y_G_033_TheCitedA0IsAFloatingPointArtifact()
    {
        // The count group G cites is reproduced exactly — it IS the number in the code and the docs.
        Assert.Equal(20812, CubicSubstrateAudit.GroupGRecordedA0());
        Assert.Equal(20812, CubicSubstrateAudit.RecordedA0()[0].Value);
        Assert.Equal(884736L, CubicSubstrateAudit.CubeModes);

        // ...but it is NOT stable: a one-ulp (1e-16) perturbation of the 1D spectrum moves it by hundreds.
        var (min, max) = CubicSubstrateAudit.ExactKeyingNoiseSweep(1.0e-16, 6);
        Assert.True(max - min > 500, $"exact-keying count range under 1e-16 noise was only {max - min}");
        Assert.True(CubicSubstrateAudit.ExactKeyingIsArtifact());

        // And it degrades further with a slightly larger (still numerically negligible) perturbation.
        var (min15, max15) = CubicSubstrateAudit.ExactKeyingNoiseSweep(1.0e-15, 6);
        Assert.True(max15 - min15 >= max - min, "the instability must not shrink as the perturbation grows");

        // The tolerant count is the robust one: it is invariant under the same perturbations at every scale.
        Assert.Equal(16080, CubicSubstrateAudit.RobustA0());
        Assert.Equal(0, CubicSubstrateAudit.TolerantCountNoiseSpread(1.0e-16, 6));
        Assert.Equal(0, CubicSubstrateAudit.TolerantCountNoiseSpread(1.0e-15, 6));
        Assert.Equal(0, CubicSubstrateAudit.TolerantCountNoiseSpread(1.0e-13, 6));
        Assert.Equal(0, CubicSubstrateAudit.TolerantCountNoiseSpread(1.0e-13, 6));

        // The robust value sits on a plateau across several decimal places — the signature of a real invariant.
        // (The upper end is set by the round-off floor of the sums, not by the clustering itself.)
        var (from, to, value) = CubicSubstrateAudit.ToleranceClusterPlateau();
        Assert.Equal(16080, value);
        Assert.True(to - from >= 3, $"plateau was only {from}..{to} decimal places");
        Assert.All(CubicSubstrateAudit.TolerantCountsByDecimalPlace()
                     .Where(r => r.Decimals >= from && r.Decimals <= to),
                   r => Assert.Equal(16080, r.Count));

        // The two A0 values the repository carries do NOT agree, and one of them is the artifact.
        var recorded = CubicSubstrateAudit.RecordedA0();
        Assert.Equal(2, recorded.Length);
        Assert.True(CubicSubstrateAudit.RecordedA0Disagrees());

        // The consequence, quantified: free room and latent fraction differ.
        Assert.Equal(863924L, CubicSubstrateAudit.FreeRoom(20812));
        Assert.Equal(868656L, CubicSubstrateAudit.FreeRoom(16080));
        Assert.Equal(0.976477, CubicSubstrateAudit.LatentFraction(20812), 6);
        Assert.Equal(0.981825, CubicSubstrateAudit.LatentFraction(16080), 6);

        // The qualitative conclusion survives — D96^3 is ~98 % energy-free either way.
        Assert.True(CubicSubstrateAudit.LatentFraction(20812) > 0.97);
        Assert.True(CubicSubstrateAudit.LatentFraction(16080) > 0.97);
    }

    [Fact]
    public void Y_G_033_VerdictIsPartialAndTheRequirementLivesInEta()
    {
        // COMPUTED verdict, not a literal (ResearchY-G_027).
        Assert.Equal("PARTIAL", CubicSubstrateAudit.Verdict());

        // PARTIAL because gravity needs the dimension-3 sector AND the use is era-local.
        Assert.True(CubicSubstrateAudit.RequiresCubicSubstrate());
        Assert.True(CubicSubstrateAudit.SubstrateUseIsEraLocal());

        // The requirement did not vanish — it is discharged by η (G_032's assumed primitive).
        string locus = CubicSubstrateAudit.LocusOfTheRequirement();
        Assert.Contains("η", locus);
        Assert.Contains("G_032", locus);
    }

    [Fact]
    public void Y_G_033_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_033 — Cubic Substrate Audit: does gravity/time need D96³ too?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. D96³ means D96 ⊗ D96 ⊗ D96 (NP_037/NP_088): 96³ = 884 736 modes.");
        sb.AppendLine("  2. M_012 established the criterion: a GENUINE 3D sector needs an irreducible rep of dim >= 3,");
        sb.AppendLine("     which a single D96 ring (dihedral D_96, irreps of dim 1-2) cannot host.");
        sb.AppendLine("  3. Group G is the gravity/time programme, G_001..G_032.");
        sb.AppendLine();

        PrintHeader("1. IN CODE: THE SUBSTRATE IS ERA-LOCAL");
        var all = CubicSubstrateAudit.ScanAudits();
        sb.AppendLine($"  suites scanned: {all.Length}   (classified: {CubicSubstrateAudit.AuditsSubjectToClassification().Length};"
                      + $" the {CubicSubstrateAudit.MetaAudits.Length} substrate meta-audits excluded as self-referential)");
        sb.AppendLine($"  reference D96 in CODE    : {CubicSubstrateAudit.AuditsUsingSubstrate().Length}   <- the density era");
        sb.AppendLine($"  comment-only (named only): {CubicSubstrateAudit.AuditsCommentOnly().Length}");
        sb.AppendLine($"  NO D96 reference at all  : {CubicSubstrateAudit.AuditsWithoutReference().Length}   <- the metric / closure era");
        sb.AppendLine();
        sb.AppendLine("  audit       code-refs   comment-refs");
        foreach (var u in all)
        {
            bool self = CubicSubstrateAudit.IsMetaAudit(u.Audit);
            sb.AppendLine($"  {u.Audit,-12}{u.CodeRefs,8}{u.CommentRefs,14}"
                        + (u.UsesInCode ? (self ? "   (this audit)" : "") : "   (substrate-free)"));
        }
        sb.AppendLine();

        PrintHeader("2. IN STRUCTURE: GRAVITY NEEDS A DIMENSION-3 IRREP");
        sb.AppendLine("  octahedral subduction of the O(3) irreps (|chi|^2 = number of O-irreps):");
        sb.AppendLine("    l   dim   |chi|^2   decomposition");
        for (int l = 0; l <= 4; l++)
        {
            var parts = CubicSubstrateAudit.Subduction(l)
                .Select(p => $"{p.Irrep}({p.Dimension})" + (p.Multiplicity > 1 ? $"x{p.Multiplicity}" : ""));
            sb.AppendLine($"    {l}   {2 * l + 1,4}   {CubicSubstrateAudit.CharacterNormSquared(l),7:F4}   {string.Join(" + ", parts)}");
        }
        var (tt, tr, tl) = CubicSubstrateAudit.SymmetricTensorContent();
        sb.AppendLine();
        sb.AppendLine($"  the spatial metric g_ij: {tt} components = {tr} (trace, l=0) + {tl} (traceless, l=2)");
        sb.AppendLine($"  traceless part -> {string.Join(" + ", CubicSubstrateAudit.TracelessSector().Select(p => $"{p.Irrep}({p.Dimension})"))}"
                      + $"   max irrep dim = {CubicSubstrateAudit.MaxIrrepDimension(CubicSubstrateAudit.TracelessSector())}");
        sb.AppendLine($"  vector / p-wave  -> {string.Join(" + ", CubicSubstrateAudit.VectorSector().Select(p => $"{p.Irrep}({p.Dimension})"))}"
                      + $"   max irrep dim = {CubicSubstrateAudit.MaxIrrepDimension(CubicSubstrateAudit.VectorSector())}");
        sb.AppendLine($"  single D96 ring max irrep dim = {CubicSubstrateAudit.SingleRingMaxIrrepDim()}"
                      + $"   vs cubic D96^3 = {CubicSubstrateAudit.CubicMaxIrrepDim()}");
        sb.AppendLine($"  => gravity REQUIRES the cubic substrate: {CubicSubstrateAudit.RequiresCubicSubstrate()}");
        sb.AppendLine($"  dimension is derived: rotation self-duality d(d-1)/2 = d has the UNIQUE solution d = "
                      + $"{CubicSubstrateAudit.RotationSelfDualityDimension()} (M_013), unique = {CubicSubstrateAudit.RotationSelfDualityIsUnique()}");
        sb.AppendLine();

        PrintHeader("3. THE A₀ DEFECT (found while checking)");
        sb.AppendLine($"  D96³ modes                                  : {CubicSubstrateAudit.CubeModes}");
        sb.AppendLine($"  exact binary64 keying (the cited figure)    : {CubicSubstrateAudit.GroupGRecordedA0()}");
        sb.AppendLine($"  tolerance-clustered (ROBUST)                : {CubicSubstrateAudit.RobustA0()}");
        var (nmin, nmax) = CubicSubstrateAudit.ExactKeyingNoiseSweep(1.0e-16, 6);
        sb.AppendLine($"  exact keying under one-ulp (1e-16) noise    : {nmin} … {nmax}   (range {nmax - nmin})");
        sb.AppendLine($"  tolerant count under the same noise         : spread {CubicSubstrateAudit.TolerantCountNoiseSpread(1.0e-16, 6)}");
        sb.AppendLine($"  tolerant count under 1e-13 noise            : spread {CubicSubstrateAudit.TolerantCountNoiseSpread(1.0e-13, 6)}");
        var (pf, pt, pv) = CubicSubstrateAudit.ToleranceClusterPlateau();
        sb.AppendLine($"  plateau of the robust value                 : {pv} across {pf}..{pt} decimal places");
        sb.AppendLine("  tolerant count by decimal place:");
        sb.AppendLine("    dp  :  " + string.Join("  ", CubicSubstrateAudit.TolerantCountsByDecimalPlace().Select(r => $"{r.Decimals,6}")));
        sb.AppendLine("    n   :  " + string.Join("  ", CubicSubstrateAudit.TolerantCountsByDecimalPlace().Select(r => $"{r.Count,6}")));
        sb.AppendLine();
        sb.AppendLine("  the repository carries BOTH values for the same quantity:");
        foreach (var v in CubicSubstrateAudit.RecordedA0())
            sb.AppendLine($"    {v.Value,6}  <- {v.Source}");
        sb.AppendLine();
        sb.AppendLine("  consequence:");
        foreach (int a0 in new[] { 20812, 16080 })
            sb.AppendLine($"    A0 = {a0,6}  free room = {CubicSubstrateAudit.FreeRoom(a0),7}  L = {CubicSubstrateAudit.LatentFraction(a0):F6}");
        sb.AppendLine("  the qualitative claim (D96³ is ~98 % energy-free) SURVIVES; the specific numbers do not.");
        sb.AppendLine();

        PrintHeader("4. VERDICT");
        sb.AppendLine($"  {CubicSubstrateAudit.Verdict()} — same requirement, different locus.");
        sb.AppendLine("  • IN CODE the substrate is era-local: the density era computes with D96/D96³, the metric /");
        sb.AppendLine("    closure era (G_017, G_019-G_022, G_025, G_027-G_032) computes with none of it.");
        sb.AppendLine("  • IN STRUCTURE the requirement is inherited, not eliminated: the spatial metric's traceless");
        sb.AppendLine("    part needs T2g(3), which a single D96 ring cannot supply — exactly M_012's argument.");
        sb.AppendLine($"  • THE LOCUS: {CubicSubstrateAudit.LocusOfTheRequirement()}");
        sb.AppendLine();
        sb.AppendLine("  So the answer to 'does gravity/time behave the same?' is: YES in requirement, NO in practice —");
        sb.AppendLine("  and the difference is itself the finding, because it means 3D space enters the metric era as an");
        sb.AppendLine("  input (η) rather than as a generated structure. G_032 and G_033 are one fact seen twice.");
        Output.WriteLine(sb.ToString());
    }
}
