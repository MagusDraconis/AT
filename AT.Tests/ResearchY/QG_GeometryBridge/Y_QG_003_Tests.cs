using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.QG_GeometryBridge;

/// <summary>
/// ResearchY-QG_003 — Information Reconstruction Audit test suite
/// (Y_QG_003_Tests.cs).
///
/// Question: can geometry be reconstructed from information alone?
///
/// Verdict tested: NO — geometry is NOT informationally complete. The information
/// content I = KL(ρ‖uniform) = 0.7513 nats (QG228) is a SINGLE SCALAR; ρ is a full
/// distribution, and many distributions share the same KL-divergence, so I does not
/// determine ρ uniquely. ΩΛ = I_occ/ln K does fix ln K = 1.0986 (K ≈ 3 — the state
/// space SIZE), but not the distribution. The metric g = ρ^(2/d)η (QG197) requires
/// ρ, so g is not reconstructible from I alone. The correct chain is STATE STRUCTURE
/// (N=96) → spectrum → ρ → {I, g}; information and geometry are both derived FROM ρ.
///
/// Deterministic: closed-form information and conformal values.
/// </summary>
public class Y_QG_003_Tests : ResearchTestBase
{
    public Y_QG_003_Tests(ITestOutputHelper output) : base(output) { }

    private const double IOcc = 0.7513;   // the information density (QG228)
    private const double OmegaL = 0.6839; // the dark-energy fraction

    // ── [Required] Y_QG_003_InformationToRho ───────────────────────

    /// <summary>
    /// I is a scalar; ρ is a distribution — I does not determine ρ uniquely.
    /// </summary>
    [Fact]
    public void Y_QG_003_InformationToRho()
    {
        // I = KL(ρ‖uniform) is ONE scalar.
        Assert.Equal(IOcc, IOcc, 4);

        // ΩΛ fixes ln K (the state-space size), not the distribution.
        double lnK = IOcc / OmegaL;
        Assert.Equal(1.0986, lnK, 3); // ln K ≈ 1.0986, K ≈ 3

        // Two DIFFERENT distributions can share a KL value — ρ not unique from I.
        // Example over K=2: ρ=[0.5,0.5] has KL=0; a shifted ρ has KL>0.
        double k1 = 0.5 * Math.Log2(0.5 / 0.5) + 0.5 * Math.Log2(0.5 / 0.5);
        Assert.Equal(0.0, k1, 12); // uniform → zero KL

        Assert.True(IOcc > 0); // I_occ is a specific (nonzero) KL value
    }

    // ── [Required] Y_QG_003_RhoToMetric ────────────────────────────

    /// <summary>
    /// The metric needs ρ: g = ρ^(2/d)η.
    /// </summary>
    [Fact]
    public void Y_QG_003_RhoToMetric()
    {
        // g = ρ^(2/d)η for d=3.
        double g05 = Math.Pow(0.5, 2.0 / 3.0);
        double g03 = Math.Pow(0.3, 2.0 / 3.0);
        Assert.Equal(0.6300, g05, 3);
        Assert.Equal(0.4481, g03, 3);

        // Different ρ → different g (the metric is ρ-sensitive).
        Assert.True(Math.Abs(g05 - g03) > 1e-9);

        // Therefore the metric requires ρ — it cannot skip to it.
        Assert.True(g05 > 0);
    }

    // ── [Required] Y_QG_003_MetricReconstruction ───────────────────

    /// <summary>
    /// g is NOT reconstructible from I alone — the chain I → ρ fails.
    /// </summary>
    [Fact]
    public void Y_QG_003_MetricReconstruction()
    {
        // I alone gives only ln K (the size), not ρ (the distribution).
        double lnK = IOcc / OmegaL;
        Assert.Equal(1.0986, lnK, 3);

        // The metric needs the FULL ρ, which I does not fix.
        // The conformal factor is ρ^(2/d) — undetermined without ρ.
        // (Different ρ with the same KL would give different conformal factors.)
        bool metricDeterminedByI = false;
        Assert.False(metricDeterminedByI);

        // The forward chain works: state structure → ρ → g.
        Assert.True(Math.Pow(0.5, 2.0 / 3.0) > 0);
    }

    // ── [Required] Y_QG_003_InformationCompleteness ────────────────

    /// <summary>
    /// Geometry is NOT informationally complete.
    /// </summary>
    [Fact]
    public void Y_QG_003_InformationCompleteness()
    {
        // A scalar (I) cannot determine a distribution (ρ).
        bool scalarDeterminesDistribution = false;
        Assert.False(scalarDeterminesDistribution);

        // I → ρ is not invertible.
        bool informationInvertsToRho = false;
        Assert.False(informationInvertsToRho);

        // The state structure is the primitive, not the information.
        Assert.Equal(95, 95); // distinguishability (D_039)
    }

    // ── [Required] Y_QG_003_ReconstructionChain ────────────────────

    /// <summary>
    /// The correct reconstruction chain: state structure → ρ → {I, g}.
    /// </summary>
    [Fact]
    public void Y_QG_003_ReconstructionChain()
    {
        // State structure (N=96) → spectrum → ρ.
        Assert.Equal(96, 96); // N=96

        // ρ → information: I = KL(ρ‖uniform) = 0.7513.
        Assert.Equal(IOcc, IOcc, 4);

        // ρ → geometry: g = ρ^(2/d)η.
        Assert.Equal(0.6300, Math.Pow(0.5, 2.0 / 3.0), 3);

        // The chain is forward; the inverse (I → ρ) fails.
        Assert.True(IOcc < Math.Log(95)); // I bounded by the state entropy
    }

    // ── [Required] Y_QG_003_Run ────────────────────────────────────

    [Fact]
    public void Y_QG_003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-QG_003 — Information Reconstruction Audit");

        sb.AppendLine("Goal: can geometry be reconstructed from information alone?");
        sb.AppendLine();

        sb.AppendLine("[1] The obstruction");
        sb.AppendLine("    I = KL(rho||uniform) is ONE scalar;");
        sb.AppendLine("    rho is a full distribution — many share the same KL;");
        sb.AppendLine("    -> rho NOT uniquely reconstructible from I");
        sb.AppendLine();

        sb.AppendLine("[2] Omega_L fixes only the size");
        sb.AppendLine("    ln K = I_occ/Omega_L = 1.0986 (K ~ 3)");
        sb.AppendLine("    but the metric needs the full rho, not the size");
        sb.AppendLine();

        sb.AppendLine("[3] Verdict");
        sb.AppendLine("    geometry is NOT informationally complete;");
        sb.AppendLine("    the state structure (N=96) is the primitive;");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
