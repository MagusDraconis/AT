using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 159 — D96 selection origin. QG155 established the observable attractor generates a
/// circulant ring C_96(1..6) with dihedral automorphism group D_96, and QG158 derived the moment orders
/// as Z2 powers. This phase asks WHY the observable attractor selects n = 96 over D64, D128, D192.
///
/// Tests: TQMQG1590 (Z2 automorphism + family-count constraints), TQMQG1591 (octave-rung selection),
/// TQMQG1592 (candidate discrimination + classification).
/// </summary>
public class TQMQG_Phase159_D96SelectionOriginTests : ResearchTestBase
{
    public TQMQG_Phase159_D96SelectionOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1590_Z2AndFamilyCountConstraints()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1590: Z2 automorphism and family-count constraints");

        sb.AppendLine("ASSUMPTIONS: the Z2 doublet symmetry (QG153/155) requires the half-shift");
        sb.AppendLine("automorphism i → i+n/2; the seed is period-3 (every 3rd node active), so the");
        sb.AppendLine("half-shift is a seed symmetry only when n/2 ≡ 0 (mod 3), i.e. 6 | n. The observable");
        sb.AppendLine("sector must have exactly 3 octave families (span ∈ [4, 8)).");
        sb.AppendLine();
        sb.AppendLine("Z2 AUTOMORPHISM CONSTRAINT (period-3 seed half-shift):");
        foreach (int n in new[] { 48, 64, 96, 128, 192 })
        {
            bool seed = D96SelectionOrigin.SeedHalfShiftAt(n);
            bool adj = D96SelectionOrigin.AdjacencyHalfShiftAt(n);
            sb.AppendLine($"  n={n}: seed half-shift (6|n)={seed}, adjacency half-shift={adj}, Z2 OK={D96SelectionOrigin.Z2ConstraintSatisfied(n)}");
        }
        sb.AppendLine();
        sb.AppendLine("  D64/D128: 64 mod 6 = 4, 128 mod 6 = 2 → period-3 seed NOT half-shift invariant");
        sb.AppendLine("  → the Z2 doublet structure is BROKEN for pure-power sizes.");
        sb.AppendLine();
        sb.AppendLine("FAMILY-COUNT CONSTRAINT (span ∈ [4, 8) → 3 octave families):");
        foreach (int n in new[] { 48, 96, 192 })
        {
            double span = D96SelectionOrigin.SpanAt(n);
            int fam = D96SelectionOrigin.FamilyCountAt(n);
            sb.AppendLine($"  n={n}: span={span:F3} (log2={Math.Log2(span):F3}), families={fam}, 3-family window={D96SelectionOrigin.ThreeFamilyWindow(span)}");
        }
        sb.AppendLine();
        sb.AppendLine("  n=48: span 3.24 < 4 → 2 families (too few)");
        sb.AppendLine("  n=96: span 6.40 ∈ [4, 8) → 3 families ✓");
        sb.AppendLine("  n=192: span 12.8 ≥ 8 → 4 families (too many)");
        Output.WriteLine(sb.ToString());

        Assert.True(D96SelectionOrigin.Z2ConstraintSatisfied(96), "D96 should satisfy the Z2 constraint");
        Assert.False(D96SelectionOrigin.Z2ConstraintSatisfied(64), "D64 should fail Z2");
        Assert.False(D96SelectionOrigin.Z2ConstraintSatisfied(128), "D128 should fail Z2");
        Assert.True(D96SelectionOrigin.ThreeFamilyWindow(D96SelectionOrigin.SpanAt(96)), "D96 should be in the 3-family window");
        Assert.True(D96SelectionOrigin.FamilyCountAt(96) == 3, "D96 should have 3 families");
        Assert.False(D96SelectionOrigin.ThreeFamilyWindow(D96SelectionOrigin.SpanAt(192)), "D192 should be outside the 3-family window");
    }

    [Fact]
    public void TQMQG1591_OctaveRungSelection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1591: octave-rung selection — D96 is the unique 3-family rung");

        sb.AppendLine("ASSUMPTIONS: the natural doubling chain is n = 3·2^k (period-3 seed × frequency");
        sb.AppendLine("doubling). The span scales as span ≈ 0.0667·n, so the 3-family window [4, 8) fixes");
        sb.AppendLine("n ∈ [60, 120).");
        sb.AppendLine();
        sb.AppendLine("SPAN SCALING (spectral optimality):");
        foreach (int n in new[] { 48, 96, 192 })
            sb.AppendLine($"  n={n}: span/n = {D96SelectionOrigin.SpanScaling(n):F4}");
        sb.AppendLine();
        sb.AppendLine("OCTAVE RUNG CHAIN n = 3·2^k:");
        var (rungs, three, selected) = D96SelectionOrigin.OctaveRungSelection();
        foreach (int n in rungs)
        {
            double span = D96SelectionOrigin.SpanAt(n);
            int fam = D96SelectionOrigin.FamilyCountAt(n);
            sb.AppendLine($"  n={n}: span={span:F3}, families={fam}, in 3-family window={D96SelectionOrigin.ThreeFamilyWindow(span)}");
        }
        sb.AppendLine();
        sb.AppendLine($"  3-family rungs: [{string.Join(", ", three)}]");
        sb.AppendLine($"  selected: n={selected}");
        sb.AppendLine($"  D96 is the UNIQUE octave rung in the 3-family window: {D96SelectionOrigin.UniqueThreeFamilyRung()}");
        sb.AppendLine();
        sb.AppendLine("  k=4 → n=48: 2 families (span 3.24 < 4)");
        sb.AppendLine("  k=5 → n=96: 3 families (span 6.40 ∈ [4, 8)) ✓");
        sb.AppendLine("  k=6 → n=192: 4 families (span 12.8 ≥ 8)");
        Output.WriteLine(sb.ToString());

        Assert.True(D96SelectionOrigin.UniqueThreeFamilyRung(), "D96 should be the unique 3-family octave rung");
        Assert.Equal(new[] { 96 }, three);
        Assert.Equal(96, selected);
        Assert.True(D96SelectionOrigin.RadiusUniformAcrossRungs(), "all rungs should have the same radius (stability not size-selecting)");
    }

    [Fact]
    public void TQMQG1592_CandidateDiscriminationAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1592: candidate discrimination and classification");

        sb.AppendLine("DISCRIMINATION OF D64, D128, D192 vs D96:");
        foreach (var (n, z2, fam, span, sel) in D96SelectionOrigin.CandidateDiscrimination())
            sb.AppendLine($"  D{n}: Z2={z2}, families={fam}, span={span:F3}, selected={sel}");
        sb.AppendLine();
        sb.AppendLine("  D64  — fails Z2 (64 mod 6 = 4, no half-shift) despite 3 families");
        sb.AppendLine("  D128 — fails Z2 (128 mod 6 = 2), and has 4 families (span 8.5 ≥ 8)");
        sb.AppendLine("  D192 — passes Z2 (192 mod 6 = 0) but has 4 families (span 12.8 ≥ 8)");
        sb.AppendLine("  D96  — passes Z2 (96 mod 6 = 0) AND exactly 3 families (span 6.40 ∈ [4, 8)) ✓");
        sb.AppendLine();
        int score = D96SelectionOrigin.SelectionScore();
        string cls = D96SelectionOrigin.Classify();
        sb.AppendLine($"D96-selection score (0..5): {score}");
        sb.AppendLine($"  +1 Z2 constraint satisfied at 96: {D96SelectionOrigin.Z2ConstraintSatisfied(96)}");
        sb.AppendLine($"  +1 3-family window at 96: {D96SelectionOrigin.ThreeFamilyWindow(D96SelectionOrigin.SpanAt(96))}");
        sb.AppendLine($"  +1 span scaling 0.06–0.07: {D96SelectionOrigin.SpanScaling(96):F4}");
        sb.AppendLine($"  +1 unique octave rung: {D96SelectionOrigin.UniqueThreeFamilyRung()}");
        sb.AppendLine($"  +1 all alternatives discriminated: {D96SelectionOrigin.CandidateDiscrimination().Count(c => !c.Selected) == 3}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO SELECTION rejected: structural constraints uniquely single out n=96.");
        sb.AppendLine("  • PARTIAL SELECTION rejected: BOTH the Z2 automorphism constraint AND the family-");
        sb.AppendLine("    count constraint select 96, and the alternatives are all discriminated.");
        sb.AppendLine("  • INEVITABLE accepted: D96 is the inevitable attractor geometry — the Z2 doublet");
        sb.AppendLine("    symmetry requires 6|n (period-3 seed half-shift), the 3-family constraint requires");
        sb.AppendLine("    span ∈ [4, 8) which with span ≈ 0.0667·n fixes n ∈ [60, 120), and the natural");
        sb.AppendLine("    doubling chain n = 3·2^k contains exactly ONE rung in that window — n = 96.");
        sb.AppendLine("    D64/D128 (no Z2) and D192 (4 families) are excluded by the structural constraints;");
        sb.AppendLine("    selection is driven by automorphism + family-count structure, not stability");
        sb.AppendLine("    (all candidates are stable radius-6 attractors).");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "selection score should be strong");
        Assert.Equal("INEVITABLE", cls);
    }
}
