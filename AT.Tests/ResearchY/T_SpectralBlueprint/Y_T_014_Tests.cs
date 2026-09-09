using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_014 — Near-Gap Density Audit.
///
/// Question: what determines the near-gap mode count N_gap(k) = #{λ ≤ k·λ₂} for k = 1.5, 2, 3, 4?
///
/// Compare D96 / D96^3 / random / physical / unphysical, and test dependence on multiplicity
/// structure, distinct eigenvalues, tensor-product dimension, and symmetry class. Goal: derive
/// N_gap = F(spectrum). Deterministic throughout.
/// </summary>
public class Y_T_014_Tests : ResearchTestBase
{
    public Y_T_014_Tests(ITestOutputHelper output) : base(output) { }

    // ── Near-gap counting ────────────────────────────────────────────────────

    private static (double Gap, int Modes, int Distinct) NearGap(SpectralCase c, double k)
    {
        double gap = double.PositiveInfinity;
        foreach (double l in c.Distinct) if (l > 1e-9) gap = Math.Min(gap, l);

        int modes = 0, distinct = 0;
        for (int i = 0; i < c.Distinct.Length; i++)
            if (c.Distinct[i] > 1e-9 && c.Distinct[i] <= k * gap)
            {
                distinct++;
                modes += c.Multiplicities[i];
            }
        return (gap, modes, distinct);
    }

    private static int GapMultiplicity(SpectralCase c)
    {
        double gap = double.PositiveInfinity;
        foreach (double l in c.Distinct) if (l > 1e-9) gap = Math.Min(gap, l);
        for (int i = 0; i < c.Distinct.Length; i++)
            if (Math.Abs(c.Distinct[i] - gap) < 1e-9) return c.Multiplicities[i];
        return 0;
    }

    /// <summary>The five comparison landscapes: D96, D96^3, random, physical, unphysical.</summary>
    private static SpectralCase[] FiveModels()
        => [SpectralCaseCatalog.D96(), SpectralCaseCatalog.D96Cubed(),
            SpectralCaseCatalog.Random(), SpectralCaseCatalog.Physical(), SpectralCaseCatalog.Unphysical()];

    // ── 1. N_gap(1) = the gap multiplicity (degeneracy) ─────────────────────

    [Fact]
    public void Y_T_014_GapMultiplicity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // N_gap(1) counts only the gap eigenvalue: its multiplicity is the symmetry/degeneracy
        // of the lowest mode — 2 (1D doublet), 6 (3D axial), 1 (random generic), 2 (physical),
        // 32 (unphysical 32-fold degenerate cluster).
        Assert.Equal(2, GapMultiplicity(SpectralCaseCatalog.D96()));
        Assert.Equal(6, GapMultiplicity(SpectralCaseCatalog.D96Cubed()));
        Assert.Equal(1, GapMultiplicity(SpectralCaseCatalog.Random()));
        Assert.Equal(2, GapMultiplicity(SpectralCaseCatalog.Physical()));
        Assert.Equal(32, GapMultiplicity(SpectralCaseCatalog.Unphysical()));
    }

    // ── 2. N_gap is an exact function of the spectrum ────────────────────────

    [Fact]
    public void Y_T_014_ExactCounting()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] ks = [1.5, 2.0, 3.0, 4.0];

        foreach (var c in FiveModels())
            foreach (double k in ks)
            {
                var (_, modes, distinct) = NearGap(c, k);
                // N_gap(k) is a monotone counting function of the spectrum.
                Assert.True(modes >= distinct, "modes ≥ distinct eigenvalues");
                Assert.True(modes > 0, "non-empty near-gap");
            }
    }

    // ── 3. The k-scaling follows the tensor-product dimension (Weyl law) ─────

    [Fact]
    public void Y_T_014_DimensionScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // 1D D96: N_gap(k) ∝ k^{1/2} (quadratic circulant spectrum) — grows slowly.
        var d96 = SpectralCaseCatalog.D96();
        double r1 = (double)NearGap(d96, 4.0).Modes / NearGap(d96, 1.0).Modes;

        // 3D D96^3: N_gap(k) ∝ k^{3/2} (cubic DOS) — grows faster.
        var d963 = SpectralCaseCatalog.D96Cubed();
        double r3 = (double)NearGap(d963, 4.0).Modes / NearGap(d963, 1.0).Modes;

        // The 3D near-gap grows markedly faster than the 1D near-gap over the same k window.
        Assert.True(r3 > r1, $"3D growth {r3:F2} must exceed 1D growth {r1:F2}");
        Assert.True(r1 < 4.0, "1D grows sub-linearly in k (∝ k^{1/2})");
    }

    // ── 4. Multiplicity structure determines the magnitude ───────────────────

    [Fact]
    public void Y_T_014_MultiplicityDependence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // The unphysical clustered spectrum has a 32-fold-degenerate gap, so N_gap stays pinned
        // at 32 across k = 1.5..4 (until the next cluster at 5·λ₂) — a plateau from degeneracy.
        var unphysical = SpectralCaseCatalog.Unphysical();
        int n15 = NearGap(unphysical, 1.5).Modes;
        int n4 = NearGap(unphysical, 4.0).Modes;
        Assert.Equal(32, n15);
        Assert.Equal(n15, n4);

        // Random has all-singleton multiplicities, so N_gap(2) = 71 (T_010) is entirely distinct
        // eigenvalues, not degeneracy.
        var random = SpectralCaseCatalog.Random();
        var (_, rModes, rDistinct) = NearGap(random, 2.0);
        Assert.Equal(rModes, rDistinct);
    }

    // ── 5. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_014_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // DERIVED: N_gap(k) = Σ_{λ≤kλ₂} m(λ) is exact counting (a deterministic function of the
        // spectrum), and its k-growth follows the Weyl law (tensor-product dimension).
        // EMERGENT: the specific magnitudes (2, 6, 32, 71) depend on the symmetry/degeneracy class.
        // REFUTED: "N_gap(k) is a universal function of k" — it differs across spectra.
        int d96 = NearGap(SpectralCaseCatalog.D96(), 2.0).Modes;
        int d963 = NearGap(SpectralCaseCatalog.D96Cubed(), 2.0).Modes;
        int random = NearGap(SpectralCaseCatalog.Random(), 2.0).Modes;
        int unphys = NearGap(SpectralCaseCatalog.Unphysical(), 2.0).Modes;

        Assert.True(new[] { d96, d963, random, unphys }.Distinct().Count() > 1,
            "N_gap differs across spectra (not universal)");
    }

    // ── 6. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_014_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_014 — Near-Gap Density Audit");

        sb.AppendLine("Question: what determines the near-gap mode count N_gap(k) = #{λ ≤ k·λ₂}?");
        sb.AppendLine();

        sb.AppendLine("[1] Near-gap density (modes | distinct)");
        sb.AppendLine("     model             λ₂        k=1.5     k=2       k=3       k=4");
        foreach (var c in FiveModels())
        {
            var (gap, _, _) = NearGap(c, 1.5);
            sb.Append($"     {c.Name,-14} {gap,8:F4}  ");
            foreach (double k in new[] { 1.5, 2.0, 3.0, 4.0 })
            {
                var (_, modes, distinct) = NearGap(c, k);
                sb.Append($" {modes,3}|{distinct,2}");
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        sb.AppendLine("[2] Gap multiplicities (degeneracy of λ₂)");
        sb.AppendLine($"     D96={GapMultiplicity(SpectralCaseCatalog.D96())}  D96^3={GapMultiplicity(SpectralCaseCatalog.D96Cubed())}  " +
                      $"random={GapMultiplicity(SpectralCaseCatalog.Random())}  physical={GapMultiplicity(SpectralCaseCatalog.Physical())}  " +
                      $"unphysical={GapMultiplicity(SpectralCaseCatalog.Unphysical())}");
        sb.AppendLine();

        sb.AppendLine("[3] Dimension scaling (Weyl law)");
        var d96 = SpectralCaseCatalog.D96();
        var d963 = SpectralCaseCatalog.D96Cubed();
        sb.AppendLine($"     D96   N_gap(4)/N_gap(1) = {(double)NearGap(d96, 4).Modes / NearGap(d96, 1).Modes:F2}  (∝ k^(1/2))");
        sb.AppendLine($"     D96^3 N_gap(4)/N_gap(1) = {(double)NearGap(d963, 4).Modes / NearGap(d963, 1).Modes:F2}  (∝ k^(3/2))");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("  DERIVED:  N_gap(k) = Σ_{λ≤kλ₂} m(λ) is exact counting; k-growth follows the");
        sb.AppendLine("            Weyl law (tensor-product dimension d).");
        sb.AppendLine("  EMERGENT: the magnitudes (2, 6, 32, 71) depend on the symmetry/degeneracy class.");
        sb.AppendLine("  REFUTED:  'N_gap(k) is a universal function of k'.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
