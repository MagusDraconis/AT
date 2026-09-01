using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 30 — Q-event correlation dynamics. Tests whether correlations between Q-events can generate the
/// systematic observation-level effects (lensing, delay, magnification) without introducing ψ.
///
/// Tests: ATQG300 (zero-mean vs nonzero variance), ATQG301 (systematic vs stochastic), ATQG302 (determination).
/// </summary>
public class ATQG_Phase30_QEventCorrelationsTests : ResearchTestBase
{
    public ATQG_Phase30_QEventCorrelationsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG300: zero-mean fluctuations vs nonzero variance ──────────────────────────

    [Fact]
    public void ATQG300_ZeroMeanVsVariance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG300: correlations are zero-mean, but have nonzero variance");

        double sigma = 0.1, xi = 1.0;
        double mean = QEventCorrelations.FluctuationMean();
        double kernel = QEventCorrelations.CorrelationKernel(sigma);
        double meanDefl = QEventCorrelations.MeanDeflection();
        double meanDelay = QEventCorrelations.MeanDelay();
        double meanMag = QEventCorrelations.MeanMagnification();
        double var = QEventCorrelations.DeflectionVariance(sigma, xi);

        sb.AppendLine($"fluctuation mean ⟨δρ⟩          = {mean:F6}");
        sb.AppendLine($"correlation kernel K = σ²      = {kernel:F6}  (2nd order)");
        sb.AppendLine($"mean deflection ⟨δα⟩           = {meanDefl:F6}  (systematic)");
        sb.AppendLine($"mean Shapiro delay ⟨Δt⟩        = {meanDelay:F6}  (systematic)");
        sb.AppendLine($"mean magnification ⟨μ⟩         = {meanMag:F6}  (systematic)");
        sb.AppendLine($"deflection variance ⟨α²⟩       = {var:F6}  (jitter)");

        bool zeroMean = mean == 0.0 && meanDefl == 0.0 && meanDelay == 0.0 && meanMag == 1.0;
        bool nonzeroVariance = var > 0.0;

        sb.AppendLine();
        sb.AppendLine($"systematic (mean) effects vanish: {zeroMean}");
        sb.AppendLine($"stochastic (variance) effects exist: {nonzeroVariance}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: correlations are second-order — their MEAN is zero (no systematic deflection/delay/focusing),");
        sb.AppendLine("but their VARIANCE is nonzero (stochastic jitter / scintillation).");
        Output.WriteLine(sb.ToString());

        Assert.True(zeroMean, "all systematic (mean) effects should vanish");
        Assert.True(nonzeroVariance, "variance should be nonzero");
    }

    // ── ATQG301: systematic vs stochastic ────────────────────────────────────────────

    [Fact]
    public void ATQG301_SystematicVsStochastic()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG301: do correlations produce lensing, or only jitter?");

        double sigma = 0.1, xi = 1.0;
        bool systematic = QEventCorrelations.ProducesSystematicLensing();
        bool jitter = QEventCorrelations.ProducesJitter(sigma, xi);
        bool scalarBreaks = QEventCorrelations.ScalarRenormalizationBreaksConformal();

        sb.AppendLine($"correlations produce SYSTEMATIC lensing: {systematic}");
        sb.AppendLine($"correlations produce STOCHASTIC jitter:  {jitter}");
        sb.AppendLine($"scalar renormalization breaks conformal flatness: {scalarBreaks}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: correlations generate only ZERO-MEAN jitter, not the systematic (background) lensing that GR");
        sb.AppendLine("predicts. Even a mean-field scalar renormalization of ρ̄ keeps the metric conformal (n = 1), so it cannot");
        sb.AppendLine("turn on deflection. A systematic lensing background needs the anisotropic (rank-2) ψ — not a scalar.");
        Output.WriteLine(sb.ToString());

        Assert.False(systematic, "correlations should not produce systematic lensing");
        Assert.True(jitter, "correlations should produce jitter");
        Assert.False(scalarBreaks, "scalar renormalization should not break conformal flatness");
    }

    // ── ATQG302: determination ────────────────────────────────────────────────────────

    [Fact]
    public void ATQG302_Determination()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG302: can correlations replace ψ?");

        sb.AppendLine("The five correlation mechanisms and what each produces:");
        sb.AppendLine("  tick correlations        → 2-point variance (zero mean)  → jitter, no lensing");
        sb.AppendLine("  synchronization defects  → local phase mismatch          → jitter, no lensing");
        sb.AppendLine("  branching covariance     → zero-mean fluctuation field   → jitter, no lensing");
        sb.AppendLine("  temporal-network propagation → propagation of fluctuations → jitter, no lensing");
        sb.AppendLine("  emergent bilocal kernels → K(x,y)=⟨δρδρ⟩ variance        → jitter + scalar renormalization");
        sb.AppendLine();
        sb.AppendLine("DETERMINATION: correlations CANNOT replace ψ.");
        sb.AppendLine("  • The background metric g = ρ̄^(2/d)η is fixed by the 1-point ρ̄ (conformal, n = 1).");
        sb.AppendLine("  • Correlations are 2-point (variance) — zero mean, so no systematic deflection/delay/magnification.");
        sb.AppendLine("  • A scalar renormalization of ρ̄ remains conformal; it cannot break conformal flatness.");
        sb.AppendLine("  • Systematic lensing requires the anisotropic (rank-2) ψ sector — a scalar and its isotropic");
        sb.AppendLine("    correlations cannot supply it. Correlations add only a stochastic (jitter) layer on top.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0.0, QEventCorrelations.MeanDeflection());
        Assert.False(QEventCorrelations.ProducesSystematicLensing());
        Assert.True(QEventCorrelations.ProducesJitter(0.1, 1.0));
    }
}
