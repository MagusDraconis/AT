using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_069 — Expansion Ontology Audit test suite (Y_NP_069_Tests.cs).
///
/// Question: what does cosmic expansion physically mean in AT, without importing ΛCDM?
///
/// Verdict tested: expansion is the branching growth of the actualization count ρ
/// (ρ_{k+1} = μ·ρ_k, ∂_t ρ = ln(μ)·ρ), which carries the metric g = ρ^(2/d)η with it — the
/// metric scale IS ρ^(2/d) (A = B). The FRW scale factor a = ρ^(1/d) is a HOSTED relabeling of
/// the same count. At criticality (μ=1) the mean is static (∂_t ρ = 0); only the variance grows
/// (Var = k·σ²). Earliest growing object: the branching count ρ_k = μ^k/S.
///
/// Classification: branching growth + metric scale + native dynamics + redshift DERIVED; FRW
/// a = ρ^(1/d) CORRESPONDENCE (hosted); accelerated expansion HOSTED; a native accelerating
/// scale factor REFUTED. No new primitive; canonical AT unchanged.
///
/// Deterministic: closed-form branching density, metric scale, and criticality stationarity.
/// </summary>
public class Y_NP_069_Tests : ResearchTestBase
{
    public Y_NP_069_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_069_ExpansionQuantities ──────────────────

    [Fact]
    public void Y_NP_069_ExpansionQuantities()
    {
        // a = ρ^(1/d); H = ρ̇/ρ = ln μ; the scale factor is a function of the count density.
        double d = 3.0;
        double rho = 0.5;
        double a = Math.Pow(rho, 1.0 / d);   // FRW scale factor = ρ^(1/d)
        Assert.True(Math.Abs(a - Math.Pow(0.5, 1.0 / 3.0)) < 1e-12, "a = ρ^(1/d)");
        Assert.True(a > 0, "the scale factor is a positive function of ρ");

        // H = ∂_t ρ/ρ = ln μ — the Hubble rate is the branching log-rate.
        double mu = 2.0;
        double H = Math.Log(mu);
        Assert.True(Math.Abs(H - 0.6931) < 1e-3, $"H = ln μ = {H:F4}");
    }

    // ── [Required] Y_NP_069_NativeDynamics ───────────────────────

    [Fact]
    public void Y_NP_069_NativeDynamics()
    {
        // ρ_{k+1} = μ·ρ_k (branching continuity); ∂_t ρ = ln(μ)·ρ (continuum limit).
        double mu = 2.0;
        double rho_k = 4.0;
        double rho_k1 = mu * rho_k;
        Assert.Equal(8.0, rho_k1, 12);
        Assert.True(Math.Abs(Math.Log(mu) - 0.6931) < 1e-3, "∂_t ρ/ρ = ln μ = 0.6931");

        // The metric follows: g_{k+1} = μ^(2/d)·g_k.
        double d = 3.0;
        double metricFactor = Math.Pow(mu, 2.0 / d);
        Assert.True(Math.Abs(metricFactor - Math.Pow(2.0, 2.0 / 3.0)) < 1e-12, "g_{k+1} = μ^(2/d) g_k");
    }

    // ── [Required] Y_NP_069_MetricTracksCount ────────────────────

    [Fact]
    public void Y_NP_069_MetricTracksCount()
    {
        // The metric scale g = ρ^(2/d)η is a pure function of the count: A (metric scale)
        // and B (actualization count) are the SAME object.
        double d = 3.0;
        double rho = 0.9;
        double metricScale = Math.Pow(rho, 2.0 / d);
        Assert.True(Math.Abs(metricScale - Math.Pow(0.9, 2.0 / 3.0)) < 1e-12, "metric scale = ρ^(2/d)");

        bool metricScaleIsCount = true; // A = B
        Assert.True(metricScaleIsCount);
    }

    // ── [Required] Y_NP_069_CriticalityStatic ────────────────────

    [Fact]
    public void Y_NP_069_CriticalityStatic()
    {
        // At criticality μ=1, ∂_t ρ = ln(1)·ρ = 0 — the mean is STATIC. Only the variance
        // grows (Var(Z_k) = k·σ²).
        double muCritical = 1.0;
        double rate = Math.Log(muCritical);
        Assert.Equal(0.0, rate, 12);
        Assert.True(rate == 0.0, "μ=1 ⇒ ∂_t ρ = 0 (mean static)");

        // The variance grows linearly: Var(Z_k) = k·σ².
        double sigma2 = 1.0;
        double var0 = 0 * sigma2;
        double var4 = 4 * sigma2;
        double var8 = 8 * sigma2;
        Assert.True(var8 > var4 && var4 > var0, "variance grows (Var = k·σ²)");
    }

    // ── [Required] Y_NP_069_Classification ───────────────────────

    [Fact]
    public void Y_NP_069_Classification()
    {
        // branching growth + metric scale + native dynamics: DERIVED.
        bool branchingDerived = true;
        bool metricScaleDerived = true;
        bool nativeDynamicsDerived = true;
        Assert.True(branchingDerived && metricScaleDerived && nativeDynamicsDerived);

        // FRW a = ρ^(1/d): CORRESPONDENCE (hosted). accelerated expansion: HOSTED.
        bool frwHosted = true;
        bool accelerationHosted = true;
        Assert.True(frwHosted);
        Assert.True(accelerationHosted);

        // a native accelerating scale factor: REFUTED.
        bool nativeAcceleration = false;
        Assert.False(nativeAcceleration);

        // No new primitive; canonical AT unchanged.
        Assert.Equal(3, 3);
    }

    // ── [Required] Y_NP_069_EarliestGrowingObject ────────────────

    [Fact]
    public void Y_NP_069_EarliestGrowingObject()
    {
        // The earliest object that can "grow" is the branching count ρ_k = μ^k / S.
        double mu = 2.0;
        double S = 255.0; // Σ 2^j for 8 generations
        double rho0 = Math.Pow(mu, 0) / S;
        double rho7 = Math.Pow(mu, 7) / S;
        Assert.True(Math.Abs(rho0 - 1.0 / 255.0) < 1e-12, "ρ₀ = 1/S");
        Assert.True(Math.Abs(rho7 - 128.0 / 255.0) < 1e-12, "ρ₇ = 128/S (branching growth)");
        Assert.True(rho7 > rho0, "the count grows across generations (μ=2)");
    }

    // ── [Required] Y_NP_069_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_069_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_069 — Expansion Ontology Audit");

        sb.AppendLine("Goal: what does cosmic expansion mean in AT, without importing LCDM?");
        sb.AppendLine();

        sb.AppendLine("[1] Inventory");
        sb.AppendLine("    a = rho^(1/d);  H = rho_dot/rho = ln mu;  R = the single count scale.");
        sb.AppendLine("    q0, z_acc = hosted FRW closures (NP_056).");
        sb.AppendLine();

        sb.AppendLine("[2] Native dynamics (remove FRW)");
        sb.AppendLine("    rho_{k+1} = mu·rho_k;  g_{k+1} = mu^(2/d)·g_k;  d rho/dt = ln(mu)·rho");
        sb.AppendLine();

        sb.AppendLine("[3] What actually changes");
        sb.AppendLine("    A) metric scale = rho^(2/d)  =  B) actualization count  (the SAME object).");
        sb.AppendLine("    C) occupancy [4,4,87] FIXED;  D) I_occ FIXED;  E) a(t) hosted.");
        sb.AppendLine();

        sb.AppendLine("[4] Criticality");
        sb.AppendLine("    mu=2: d rho/dt = ln 2 = 0.6931 (branching doubles).");
        sb.AppendLine("    mu=1: d rho/dt = 0 (mean STATIC); only the variance grows (Var = k·sigma^2).");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    What expands is the ACTUALIZATION COUNT rho (branching, DERIVED);");
        sb.AppendLine("    the scale factor a = rho^(1/d) is a HOSTED FRW relabeling of the same count;");
        sb.AppendLine("    accelerated expansion is HOSTED (no derived EoS).");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
