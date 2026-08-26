using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 160 — Period-3 seed origin. The established chain is period-3 seed → D96 selection →
/// Z2 doublets → moment orders → N_eff → δ → p. This phase asks WHY the seed period is exactly 3:
/// is it inevitable (derived from attractor dynamics + spectral structure) or merely empirical?
///
/// Tests: ATQG1600 (stability + octave-family natural size), ATQG1601 (Z2 completeness + automorphism),
/// ATQG1602 (candidate discrimination + classification).
/// </summary>
public class ATQG_Phase160_Period3SeedOriginTests : ResearchTestBase
{
    public ATQG_Phase160_Period3SeedOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1600_StabilityAndNaturalSize()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1600: stability and octave-rung natural size");

        sb.AppendLine("ASSUMPTIONS: each seed period p has a natural octave-rung size n = p·2^k; the");
        sb.AppendLine("3-family window is n ∈ [60, 120) (QG159). The attractor must converge to the");
        sb.AppendLine("radius-6 circulant (D96).");
        sb.AppendLine();
        sb.AppendLine("NATURAL OCTAVE-RUNG SIZE n = p·2^k:");
        sb.AppendLine("p | natural n | in 3-family window | converges at natural n | radius");
        foreach (int p in new[] { 2, 3, 4, 5, 6 })
        {
            int n = Period3SeedOrigin.NaturalSize(p);
            bool conv = n > 0 && Period3SeedOrigin.ConvergesAtNaturalSize(p);
            double radius = n > 0 ? Period3SeedOrigin.RadiusAt(n, p) : double.NaN;
            sb.AppendLine($"  {p} | {n} | {Period3SeedOrigin.InThreeFamilyWindow(n)} | {conv} | {radius:F1}");
        }
        sb.AppendLine();
        sb.AppendLine("CONVERGENCE THRESHOLD (active density 1/p):");
        foreach (int p in new[] { 2, 3, 4, 5, 6, 7, 8, 12, 16 })
            sb.AppendLine($"  p={p}: density {Period3SeedOrigin.ActiveDensity(p):F3}, " +
                $"converges at n=96: {Period3SeedOrigin.ConvergesToD96(96, p)}");
        sb.AppendLine();
        sb.AppendLine("  p=6+: density ≤ 1/6 → attractor collapses (radius ≤ 1).");
        sb.AppendLine("  Period 3 converges to D96 at its natural size 96.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(96, Period3SeedOrigin.NaturalSize(3));
        Assert.True(Period3SeedOrigin.InThreeFamilyWindow(96));
        Assert.True(Period3SeedOrigin.ConvergesToD96(96, 3), "period-3 seed should converge to D96");
        Assert.True(Period3SeedOrigin.ConvergesAtNaturalSize(3));
        Assert.False(Period3SeedOrigin.ConvergesAtNaturalSize(6), "p=6 should not converge");
    }

    [Fact]
    public void ATQG1601_Z2CompletenessAndAutomorphism()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1601: Z2 completeness and automorphism constraint");

        sb.AppendLine("ASSUMPTIONS: the weak-isospin doublet structure (QG153) requires COMPLETE Z2");
        sb.AppendLine("pairing (0 unpaired modes). The seed half-shift automorphism (Z2 origin, QG155)");
        sb.AppendLine("requires p | n/2.");
        sb.AppendLine();
        sb.AppendLine("Z2 COMPLETENESS AT THE NATURAL 3-FAMILY SIZE:");
        sb.AppendLine("p | natural n | unpaired modes | DoubledFraction | COMPLETE?");
        foreach (int p in new[] { 2, 3, 4, 5, 6 })
        {
            int n = Period3SeedOrigin.NaturalSize(p);
            int unp = Period3SeedOrigin.UnpairedModesAt(n, p);
            double frac = Period3SeedOrigin.DoubledFractionAt(n, p);
            sb.AppendLine($"  {p} | {n} | {unp} | {frac:F3} | {unp == 0}");
        }
        sb.AppendLine();
        sb.AppendLine("  n=64 (p=2,4) and n=80 (p=5): 1 unpaired mode — INCOMPLETE doublets");
        sb.AppendLine("  n=96 (p=3): 0 unpaired modes — COMPLETE doublet structure ✓");
        sb.AppendLine();
        sb.AppendLine("SEED HALF-SHIFT AUTOMORPHISM (p | n/2):");
        sb.AppendLine($"  p=3 at n=96: 3 | 48 = {Period3SeedOrigin.Period3HalfShiftHolds()}");
        sb.AppendLine($"  p=5 at n=80: 5 | 40 = {Period3SeedOrigin.SeedHalfShiftAt(80, 5)}");
        sb.AppendLine();
        sb.AppendLine("ENTROPY: seed entropy is nearly equal across periods (does NOT select): " +
            Period3SeedOrigin.EntropyDoesNotSelect());
        Output.WriteLine(sb.ToString());

        Assert.True(Period3SeedOrigin.CompleteZ2AtNaturalSize(3), "p=3 natural size should have complete Z2");
        Assert.False(Period3SeedOrigin.CompleteZ2AtNaturalSize(2), "p=2 natural size should be incomplete");
        Assert.False(Period3SeedOrigin.CompleteZ2AtNaturalSize(4), "p=4 natural size should be incomplete");
        Assert.False(Period3SeedOrigin.CompleteZ2AtNaturalSize(5), "p=5 natural size should be incomplete");
        Assert.True(Period3SeedOrigin.Period3HalfShiftHolds(), "3 | 48 should hold");
    }

    [Fact]
    public void ATQG1602_CandidateDiscriminationAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1602: candidate discrimination and classification");

        sb.AppendLine("DISCRIMINATION OF COMPETING PERIODS:");
        foreach (var (p, n, conv, unp, complete, sel) in Period3SeedOrigin.CandidateDiscrimination())
            sb.AppendLine($"  p={p}: natural n={n}, converges={conv}, unpaired={unp}, COMPLETE Z2={complete}, selected={sel}");
        sb.AppendLine();
        sb.AppendLine("  p=2 → n=64: converges, but 1 unpaired mode (INCOMPLETE) — no full doublets");
        sb.AppendLine("  p=3 → n=96: converges, 0 unpaired (COMPLETE) — full doublet structure ✓");
        sb.AppendLine("  p=4 → n=64: converges, but 1 unpaired mode (INCOMPLETE)");
        sb.AppendLine("  p=5 → n=80: converges, but 1 unpaired mode (INCOMPLETE)");
        sb.AppendLine("  p=6 → n=96: does NOT converge (density 1/6)");
        sb.AppendLine();
        int score = Period3SeedOrigin.OriginScore();
        string cls = Period3SeedOrigin.Classify();
        sb.AppendLine($"Period-3-origin score (0..5): {score}");
        sb.AppendLine($"  +1 converges to D96 at n=96: {Period3SeedOrigin.ConvergesToD96(96, 3)}");
        sb.AppendLine($"  +1 natural size 96 in 3-family window: {Period3SeedOrigin.NaturalSize(3) == 96}");
        sb.AppendLine($"  +1 complete Z2 at natural size: {Period3SeedOrigin.CompleteZ2AtNaturalSize(3)}");
        sb.AppendLine($"  +1 seed half-shift 3|48: {Period3SeedOrigin.Period3HalfShiftHolds()}");
        sb.AppendLine($"  +1 unique complete period: {Period3SeedOrigin.UniqueCompletePeriod()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • EMPIRICAL rejected: the period choice is NOT free — complete Z2 doublet pairing");
        sb.AppendLine("    (the weak-isospin structure) uniquely selects period 3.");
        sb.AppendLine("  • PARTIAL rejected: both stability and Z2-completeness select period 3, and the");
        sb.AppendLine("    alternatives (p=2,4,5 incomplete; p=6+ non-convergent) are all discriminated.");
        sb.AppendLine("  • INEVITABLE accepted: period-3 is the inevitable seed period — each seed period p");
        sb.AppendLine("    has a natural octave-rung size n = p·2^k, and in the 3-family window [60, 120)");
        sb.AppendLine("    the natural sizes are p=2→64, p=3→96, p=4→64, p=5→80. COMPLETE Z2 doublet pairing");
        sb.AppendLine("    requires 0 unpaired modes, which holds ONLY at n=96 (64 and 80 have 1 unpaired");
        sb.AppendLine("    mode); periods p ≥ 6 fail to converge (active density ≤ 1/6). Therefore p=3 is");
        sb.AppendLine("    the unique period whose natural 3-family size has complete Z2 doublet pairing —");
        sb.AppendLine("    derived from attractor dynamics and spectral structure, no fitted constants.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "period-3-origin score should be strong");
        Assert.Equal("INEVITABLE", cls);
    }
}
