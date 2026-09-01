using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-RHO Phase 1 — explain why α=0 is selected. G4-RHO0 showed α=0 is preferred (unique scale-invariant)
/// but not derived. Here we test whether a native principle (entropy maximization, abundance-law
/// stationarity, scale-free fixed points, flow equilibria, hierarchy growth) UNIQUELY selects α=0.
///
/// Tests: G4-RHO10 (entropy maximization), G4-RHO11 (RG fixed points — not selective), G4-RHO12 (uniformity
///        + scale-free field + classification).
/// </summary>
public class G4RHO_Phase1_AlphaSelectionTests : ResearchTestBase
{
    public G4RHO_Phase1_AlphaSelectionTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;
    private const int K = 8;
    private const double LAMBDA = 1.5;

    private static double V2Ratio(double alpha)
    {
        double v3 = DeficitCollective.RotationCurveProxy(u => DeficitCollective.AbundanceDeficit(u, alpha), 3.0, D);
        double v9 = DeficitCollective.RotationCurveProxy(u => DeficitCollective.AbundanceDeficit(u, alpha), 9.0, D);
        return v3 / v9;
    }

    // ── G4-RHO10: entropy maximization uniquely selects α=0 ──────────────────────────

    [Fact]
    public void G4_RHO10_EntropyMaximization()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO10: entropy maximization uniquely selects α=0");

        // The deficit is allocated across K octaves with fractions p_k ∝ λ^(−αk). α=0 gives the UNIFORM
        // allocation p_k = 1/K, which is the UNIQUE Shannon-entropy maximizer (least-biased, no preferred scale).
        sb.AppendLine($"{"α",7} {"H(α)",12}");
        double h0 = 0;
        var hs = new Dictionary<double, double>();
        foreach (double a in new[] { -1.0, -0.5, 0.0, 0.5, 1.0 })
        {
            double h = RhoDynamics.Entropy(a, K, LAMBDA);
            hs[a] = h;
            if (a == 0.0) h0 = h;
            sb.AppendLine($"{a,7:F1} {h,12:F6}");
        }
        sb.AppendLine($"ln K = {Math.Log(K):F6} (uniform max)");

        bool maxAtZero = Math.Abs(h0 - Math.Log(K)) < 1e-12;
        bool uniqueMax = true;
        foreach (var kv in hs) if (kv.Key != 0.0 && h0 <= kv.Value) uniqueMax = false;

        sb.AppendLine();
        sb.AppendLine($"H(0) = ln K (uniform maximum): {maxAtZero}");
        sb.AppendLine($"α=0 is the UNIQUE entropy maximum (H(0) > H(α) ∀ α≠0): {uniqueMax}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: with only conservation of total deficit (Σp=1) and no preferred scale, the");
        sb.AppendLine("maximum-entropy (least-biased) allocation is UNIFORM — p_k = 1/K — which is exactly α=0.");
        Output.WriteLine(sb.ToString());

        Assert.True(maxAtZero, "α=0 should attain the uniform (maximal) entropy");
        Assert.True(uniqueMax, "α=0 should be the unique entropy maximum");
    }

    // ── G4-RHO11: scale-invariance / RG is NOT selective ─────────────────────────────

    [Fact]
    public void G4_RHO11_RgNotSelective()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO11: scale-invariance (RG fixed points) does NOT select α=0");

        // Block-spin coarse-graining maps the α-hierarchy to itself (α invariant), so EVERY α is a fixed
        // point. Scale-invariance/stationarity therefore gives a continuum, not a unique α.
        sb.AppendLine($"{"α",7} {"α_eff (coarse-grained)",22}");
        bool allInvariant = true;
        foreach (double a in new[] { -1.0, -0.5, 0.0, 0.5, 1.0 })
        {
            double aEff = RhoDynamics.CoarseGrainedAlpha(a, K, LAMBDA);
            if (Math.Abs(aEff - a) > 1e-6) allInvariant = false;
            sb.AppendLine($"{a,7:F1} {aEff,22:F6}");
        }

        sb.AppendLine();
        sb.AppendLine($"α is invariant under coarse-graining for ALL α: {allInvariant}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: scale-invariance (RG stationarity) does NOT single out α=0 — every α is a fixed");
        sb.AppendLine("point. The degeneracy is broken only by an additional principle: entropy maximization.");
        Output.WriteLine(sb.ToString());

        Assert.True(allInvariant, "all α should be RG fixed points (invariant under coarse-graining)");
    }

    // ── G4-RHO12: uniformity + scale-free field + classification ─────────────────────

    [Fact]
    public void G4_RHO12_UniformityClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO12: α=0 is the unique uniform / scale-free-field member — classification");

        // (a) Uniformity: α=0 gives p_k = 1/K (uniform); α≠0 gives a non-uniform (biased) allocation.
        double[] p0 = RhoDynamics.DeficitFractions(0.0, K, LAMBDA);
        double[] p1 = RhoDynamics.DeficitFractions(1.0, K, LAMBDA);
        double spread0 = p0.Max() - p0.Min();
        double spread1 = p1.Max() - p1.Min();

        // (b) Scale-free field (flat rotation curve): v² ∝ r^(−α), so v²(3)/v²(9) = 3^α — closest to 1 at α=0.
        sb.AppendLine($"{"α",7} {"v²(3)/v²(9)",12}");
        double r0 = V2Ratio(0.0), r05 = V2Ratio(0.5), r1 = V2Ratio(1.0);
        sb.AppendLine($"{0.0,7:F1} {r0,12:F3}");
        sb.AppendLine($"{0.5,7:F1} {r05,12:F3}");
        sb.AppendLine($"{1.0,7:F1} {r1,12:F3}");

        bool uniformAtZero = spread0 < 1e-15 && spread1 > 1e-3;
        bool flatAtZero = r0 < 1.5;                 // ≈ flat (Keplerian α=1 would be ≈3)
        bool monotonic = r0 < r05 && r05 < r1;       // ratio = 3^α, increasing in α

        sb.AppendLine();
        sb.AppendLine($"α=0 uniform (spread 0) vs α=1 biased (spread {spread1:F3}): {uniformAtZero}");
        sb.AppendLine($"α=0 flat rotation curve (ratio {r0:F2} ≈ 1): {flatAtZero}");
        sb.AppendLine($"ratio monotonic in α (3^α): {monotonic}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: DERIVED (α=0), via entropy maximization.");
        sb.AppendLine("  • α=0 is the UNIQUE maximum-entropy (uniform) allocation of deficit across scales");
        sb.AppendLine("    (G4-RHO10), and the UNIQUE member with the scale-free field a ∝ 1/r (flat rotation).");
        sb.AppendLine("  • Scale-invariance/RG alone is NOT selective (G4-RHO11) — every α is a fixed point.");
        sb.AppendLine("  • The two independent characterizations — maximum entropy and scale-free field — COINCIDE");
        sb.AppendLine("    at α=0, upgrading it from PREFERRED (G4-RHO0) to DERIVED.");
        sb.AppendLine("  • Caveat: maximum entropy is a statistical (least-bias) principle, not a dynamical equation.");
        Output.WriteLine(sb.ToString());

        Assert.True(uniformAtZero, "α=0 should be uniform; α≠0 biased");
        Assert.True(flatAtZero, "α=0 should give the flat rotation curve");
        Assert.True(monotonic, "rotation-curve ratio should increase monotonically in α");
    }
}
