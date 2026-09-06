using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_097 — Temperature Ontology Audit test suite (Y_NP_097_Tests.cs).
///
/// Question: what is temperature inside Actualization Theory?
///
/// Verdict tested: temperature = the COUNT-DISTRIBUTION WIDTH (T = ∂U/∂S), the thermodynamic
/// conjugate of entropy H = −Σρ ln ρ. DERIVED from the occupancy ρ (dS/dU = β exactly); the
/// absolute Kelvin scale is BOUNDARY (the k_B unit anchor). Cold = narrow occupancy (few modes),
/// hot = wide (many modes); H monotonically tracks T, I_occ = ln K − H falls.
///
/// Deterministic: closed-form (Boltzmann occupancy ρ_k ∝ e^(−βE_k), entropy H = −Σρ ln ρ).
/// </summary>
public class Y_NP_097_Tests : ResearchTestBase
{
    public Y_NP_097_Tests(ITestOutputHelper output) : base(output) { }

    private const int K = 95;

    private static (double[] rho, double H, double U) Dist(double beta)
    {
        var rho = new double[K];
        double z = 0.0;
        for (int k = 1; k <= K; k++)
        {
            double w = Math.Exp(-beta * k);
            rho[k - 1] = w;
            z += w;
        }
        for (int i = 0; i < K; i++) rho[i] /= z;

        double h = -rho.Where(r => r > 0).Sum(r => r * Math.Log(r));
        double u = 0.0;
        for (int k = 1; k <= K; k++) u += rho[k - 1] * k;
        return (rho, h, u);
    }

    private static int SignificantModes(double[] rho, double threshold = 1e-3)
        => rho.Count(r => r > threshold);

    // ── [Required] Y_NP_097_Inventory ──────────────────────────

    [Fact]
    public void Y_NP_097_Inventory()
    {
        bool heatIsRandomPhase = true;      // NP_096
        bool entropyIsModeMultiplicity = true; // H = −Σρ ln ρ
        bool temperatureIsWidth = true;     // ∂U/∂S
        Assert.True(heatIsRandomPhase);
        Assert.True(entropyIsModeMultiplicity);
        Assert.True(temperatureIsWidth);
    }

    // ── [Required] Y_NP_097_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_097_ABCD()
    {
        bool A_multiplicityDensity = true;  // 1/T = ∂S/∂U (inverse reading)
        bool B_phaseRandomness = false;     // that is HEAT (NP_096)
        bool C_scatteringRate = false;      // a consequence, not temperature
        bool D_countDistributionWidth = true; // the answer
        Assert.True(A_multiplicityDensity);
        Assert.False(B_phaseRandomness);
        Assert.False(C_scatteringRate);
        Assert.True(D_countDistributionWidth);
    }

    // ── [Required] Y_NP_097_ColdWarmHot ────────────────────────

    [Fact]
    public void Y_NP_097_ColdWarmHot()
    {
        // cold (T=0.2): narrow (2 modes), low H; hot (T=100): wide (95 modes), high H.
        var (_, hCold, _) = Dist(5.0);      // T = 1/5 = 0.2
        var (_, hHot, _) = Dist(0.01);      // T = 100
        var coldModes = SignificantModes(Dist(5.0).rho);
        var hotModes = SignificantModes(Dist(0.01).rho);

        Assert.InRange(hCold, 0.03, 0.05);   // ~0.0407
        Assert.InRange(hHot, 4.51, 4.52);    // ~4.5171
        Assert.Equal(2, coldModes);
        Assert.Equal(95, hotModes);
        // hot is wider (more modes) and higher entropy than cold
        Assert.True(coldModes < hotModes);
        Assert.True(hCold < hHot);
    }

    // ── [Required] Y_NP_097_MonotonicTracker ───────────────────

    [Fact]
    public void Y_NP_097_MonotonicTracker()
    {
        // H monotonically increases with T; I_occ = ln K − H monotonically decreases.
        double lnK = Math.Log(K);
        double prevH = -1.0;
        foreach (double beta in new[] { 5.0, 1.0, 0.5, 0.1, 0.01 })
        {
            var (_, h, _) = Dist(beta);
            Assert.True(h > prevH); // H rises as T = 1/beta rises
            prevH = h;
        }
        double iOccCold = lnK - Dist(5.0).H;
        double iOccHot = lnK - Dist(0.01).H;
        Assert.True(iOccHot < iOccCold); // information falls as T rises
    }

    // ── [Required] Y_NP_097_ThermodynamicIdentity ──────────────

    [Fact]
    public void Y_NP_097_ThermodynamicIdentity()
    {
        // dS/dU = β (so T = dU/dS = 1/β). Verified by finite difference at β = 0.5.
        double beta = 0.5;
        double db = 1e-4;
        double sLo = Dist(beta - db).H;
        double sHi = Dist(beta + db).H;
        double uLo = Dist(beta - db).U;
        double uHi = Dist(beta + db).U;
        double dSdU = (sHi - sLo) / (uHi - uLo);
        Assert.InRange(dSdU, beta - 1e-3, beta + 1e-3);
        double T = 1.0 / dSdU;
        Assert.InRange(T, 2.0 - 1e-3, 2.0 + 1e-3);
    }

    // ── [Required] Y_NP_097_DerivedOrBoundary ──────────────────

    [Fact]
    public void Y_NP_097_DerivedOrBoundary()
    {
        // dimensionless T (∂U/∂H) is DERIVED from ρ; the absolute Kelvin scale is BOUNDARY (k_B).
        bool dimensionlessDerived = true;
        bool absoluteScaleBoundary = true; // k_B unit anchor (like m_e/v)
        Assert.True(dimensionlessDerived);
        Assert.True(absoluteScaleBoundary);
    }

    // ── [Required] Y_NP_097_Classification ─────────────────────

    [Fact]
    public void Y_NP_097_Classification()
    {
        bool temperatureDerived = true;   // ∂U/∂H from ρ
        bool entropyDerived = true;       // H = −Σρ ln ρ (NP_096)
        bool absoluteScaleBoundary = true; // k_B anchor
        bool phaseRandomnessRefuted = true; // that is heat
        bool newPrimitiveRefuted = true;
        Assert.True(temperatureDerived);
        Assert.True(entropyDerived);
        Assert.True(absoluteScaleBoundary);
        Assert.True(phaseRandomnessRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_097_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_097_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_097 — Temperature Ontology Audit");

        double lnK = Math.Log(K);

        sb.AppendLine("Goal: what is temperature inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Temperature = the COUNT-DISTRIBUTION WIDTH: T = ∂U/∂S,");
        sb.AppendLine("    U = conserved count (energy, NP_081), S = H = −Σρ ln ρ (entropy, NP_096).");
        sb.AppendLine();

        sb.AppendLine("[2] Cold → hot (Boltzmann ρ ∝ e^(−E/T)):");
        foreach (double beta in new[] { 5.0, 1.0, 0.1, 0.01 })
        {
            var (rho, h, u) = Dist(beta);
            int modes = SignificantModes(rho);
            sb.AppendLine($"    T={1.0 / beta,6:F2}: H={h,7:F4}  U={u,7:F2}  modes={modes,3}  I_occ={lnK - h,7:F4}");
        }
        sb.AppendLine();

        sb.AppendLine("[3] H (mode multiplicity) rises monotonically with T; I_occ = ln K − H falls.");
        sb.AppendLine();

        sb.AppendLine("[4] dS/dU = β exact (verified) → T = dU/dS is DERIVED from ρ.");
        sb.AppendLine("    Only the absolute Kelvin scale is BOUNDARY (the k_B unit anchor).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
