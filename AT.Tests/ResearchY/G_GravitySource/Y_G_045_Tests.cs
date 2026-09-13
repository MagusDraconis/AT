using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ObservedHiddenDimensionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_045 — Observed vs Hidden Dimension Audit.
///
/// QUESTION. Can the apparent 3D world emerge as a PROJECTION of a higher-dimensional actualization space?
/// Compared: the 3D host space against the N-dimensional state space. Measured: observable dimension, spectral
/// dimension, information dimension, attractor dimension.
///
/// ANSWER: **REFUTED — the two "dimensions" are different indices, and the one that could hide is measured
/// exactly.**
///
///  (1) THE GROUND IS RECOMPUTED, not cited: the d-torus spectrum gives 45 distinct levels at d = 1 (room 51,
///      both G_039's numbers) and 16080 at d = 3 (G_033's robust count) — which fixes the reachable room at
///      d = 3 to 868656.
///  (2) THE INFORMATION DIMENSION IS EXACTLY THE FACTOR COUNT: box counting at 2, 4, 8 and 16 boxes per axis
///      gives slopes of exactly 1, 2 and 3. A hidden direction would be visible here.
///  (3) THE SYMMETRY GROUP IS EXACT TOO: its order is 2^d d! = 2, 8, 48, and 48 occurs at exactly one
///      dimension. Two exact probes name d without ambiguity.
///  (4) THE SPECTRAL PROBES FAIL, AND THE AUDIT RECORDS ITS OWN FAILURE: the density-of-states estimate is
///      biased (up to 80 % off the true 1, 2, 3 depending on the window) and the spectral GAP does not depend
///      on d at all (0.3863508934 everywhere, since one factor may be excited alone).
///  (5) THE OBSERVABLE DIMENSION IS NEITHER 3 NOR THE STATE SPACE: families C(48 + d, d) - 1 = 48, 1224,
///      20824 — so the higher-dimensional object is largely RESOLVABLE, not hidden.
///  (6) PROJECTION IS THEREFORE REFUTED AS AN EXPLANATION OF THE APPARENT 3: the apparent three is the tensor
///      factor count, while the state space is the space of CONFIGURATIONS OVER that substrate.
/// </summary>
public class Y_G_045_Tests : ResearchTestBase
{
    public Y_G_045_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_045_TheRecomputedGroundReproducesThreeOtherAudits()
    {
        // the family is built from the ring itself
        Assert.Equal(96, D96Cells);
        Assert.Equal(96, TorusSpectrum(1).Length);

        // G_039's two numbers, recomputed
        Assert.Equal(45, DistinctLevels(1));
        Assert.Equal(51, ReachableDimension(1));

        // G_033's robust level count, recomputed — and the room it implies at d = 3
        Assert.Equal(16080, DistinctLevels(3));
        Assert.Equal(884736L, StateSpaceDimension(3) + 1);
        Assert.Equal(868656L, ReachableDimension(3));

        // the rooms are strictly increasing, so the "higher-dimensional space" is real and growing
        Assert.True(ReachableDimension(1) < ReachableDimension(2));
        Assert.True(ReachableDimension(2) < ReachableDimension(3));
    }

    [Fact]
    public void Y_G_045_TheInformationDimensionIsExactlyTheFactorCount()
    {
        // every box is occupied at every scale — computed by walking the lattice, not asserted
        foreach (int d in new[] { 1, 2, 3 })
            foreach (int n in new[] { 2, 4, 8, 16 })
                Assert.Equal((long)Math.Pow(n, d), BoxCount(d, n).Occupied);

        // so the slope of ln N against ln n is d exactly
        Assert.Equal(1.0, InformationDimension(1), 9);
        Assert.Equal(2.0, InformationDimension(2), 9);
        Assert.Equal(3.0, InformationDimension(3), 9);
    }

    [Fact]
    public void Y_G_045_TheSymmetryGroupAlsoRevealsTheFactorCountExactly()
    {
        Assert.Equal(2L, SymmetryGroupOrder(1));
        Assert.Equal(8L, SymmetryGroupOrder(2));
        Assert.Equal(48L, SymmetryGroupOrder(3));

        // the order 48 occurs at exactly one dimension — an exact probe, not an estimate
        Assert.Equal(new[] { 3 }, DimensionsWithGroupOrder(48));
        Assert.Equal(new[] { 2 }, DimensionsWithGroupOrder(8));
    }

    [Fact]
    public void Y_G_045_TheSpectralProbesFailAndTheAuditSaysSo()
    {
        // the gap is the SAME at every d: one tensor factor may be excited alone
        Assert.True(TheGapIsDimensionIndependent());
        Assert.Equal(0.3863508934, SpectralGap(1), 9);
        Assert.Equal(SpectralGap(1), SpectralGap(2), 9);
        Assert.Equal(SpectralGap(1), SpectralGap(3), 9);

        // and the density-of-states estimator is biased at every window (true values 1, 2, 3)
        double worst = 0.0;
        foreach (int window in new[] { 10, 20, 40 })
            for (int d = 1; d <= 3; d++)
                worst = Math.Max(worst, Math.Abs(SpectralDimensionEstimate(d, window) - d));
        Assert.True(worst > 0.2, $"the estimator is closer to truth than the audit claims: {worst:F4}");

        // a probe that DIVIDES BY ZERO on a legitimate window is not a probe — recorded, not hidden
        Assert.True(EstimatorIsDegenerate(3, 6));
        Assert.Equal("   n/a", FormatEstimate(3, 6));
    }

    [Fact]
    public void Y_G_045_TheObservableDimensionIsNeitherThreeNorTheStateSpace()
    {
        Assert.Equal(48L, ObservableDimension(1));      // 49 orbitals less the state itself
        Assert.Equal(1224L, ObservableDimension(2));
        Assert.Equal(20824L, ObservableDimension(3));

        // it exceeds 3 — the extra dimensions are resolvable in principle, not hidden
        Assert.True(ObservableDimension(3) > 3);
        Assert.True(ObservableDimension(3) < StateSpaceDimension(3));
    }

    [Fact]
    public void Y_G_045_TwoProbesAreExactTwoFailAndTheVerdictIsRefuted()
    {
        Assert.Equal(2, ExactProbes().Length);
        Assert.Equal(2, FailedProbes().Length);
        Assert.Equal(6, Measurements().Length);
        Assert.Equal("REFUTED", Verdict());

        // the exact probes are the ones that name d; the failed ones are the spectral pair
        Assert.Contains(ExactProbes(), p => p.Contains("information dimension"));
        Assert.Contains(ExactProbes(), p => p.Contains("symmetry group"));
        Assert.Contains(FailedProbes(), p => p.Contains("spectral"));
    }

    [Fact]
    public void Y_G_045_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_045 — Observed vs Hidden Dimension Audit: is the apparent 3 a projection?");

        sb.AppendLine("QUESTION. Can the apparent 3D world emerge as a PROJECTION of a higher-dimensional");
        sb.AppendLine("actualization space?   COMPARED  3D host space vs N-dimensional state space");
        sb.AppendLine("MEASURED  observable dimension, spectral dimension, information dimension,");
        sb.AppendLine("          attractor (reachable) dimension");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The substrate is the d-torus over the 96-cell ring (radius-6 Laplacian), grown by");
        sb.AppendLine("     tensor powers; d = 1, 2, 3 are measured, and the ladder beyond 3 is G_041's.");
        sb.AppendLine("  2. The HIGHER-DIMENSIONAL OBJECT is the actualization space — the simplex over the 96^d");
        sb.AppendLine("     cells. Its usable room is 96^d - A0(d), where A0 is the distinct-level count.");
        sb.AppendLine("  3. OBSERVABLE dimension is the number of families a substrate-constructed measurement can");
        sb.AppendLine("     name, C(48 + d, d) - 1 (G_040 at d = 1, G_043 across the ladder).");
        sb.AppendLine("  4. SPECTRAL dimension is measured two ways — the density-of-states exponent N(mu) ~");
        sb.AppendLine("     mu^(d_s/2) fitted over the lowest levels, and the gap itself — and both are reported");
        sb.AppendLine("     even where they fail, because a validator certifies only in its own regime.");
        sb.AppendLine("  5. INFORMATION dimension is box counting of the uniform measure: at n boxes per axis the");
        sb.AppendLine("     occupied count is computed by walking every lattice site.");
        sb.AppendLine("  6. Deterministic throughout; no randomness, no fitted constant carried between runs.");
        sb.AppendLine();

        PrintHeader(OutputSpectrum());
        PrintHeader(OutputDimensions());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
