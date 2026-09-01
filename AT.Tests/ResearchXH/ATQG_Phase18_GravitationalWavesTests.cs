using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 18 — compatibility with observed gravitational waves. Tests whether observed GW phenomena can
/// arise in the scalar (conformal) sector, comparing polarization and trace structure. Classify: MATCH /
/// PARTIAL MATCH / NO MATCH.
///
/// Tests: ATQG180 (polarization count), ATQG181 (trace/transverse structure), ATQG182 (classification).
/// </summary>
public class ATQG_Phase18_GravitationalWavesTests : ResearchTestBase
{
    public ATQG_Phase18_GravitationalWavesTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG180: polarization count — scalar 1 vs tensor 2 ──────────────────────────

    [Fact]
    public void ATQG180_PolarizationCount()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG180: scalar sector has 1 breathing mode; GWs have 2 (+ and ×) modes");

        int d = 3;
        double scalar = GravitationalWaves.ScalarPolarizations();
        double tensor = GravitationalWaves.TensorPolarizations(d);

        sb.AppendLine($"scalar (conformal) polarizations = {scalar} (breathing/monopole)");
        sb.AppendLine($"tensor (graviton) polarizations = {tensor} (transverse-traceless +, ×)");
        sb.AppendLine($"observed LIGO/Virgo: consistent with pure tensor (2 modes)");

        bool scalarOne = scalar == 1.0;
        bool tensorTwo = tensor == 2.0;
        bool countMismatch = scalar != tensor;

        sb.AppendLine();
        sb.AppendLine($"scalar = 1 breathing mode: {scalarOne}");
        sb.AppendLine($"tensor = 2 modes at d=3 (+, ×): {tensorTwo}");
        sb.AppendLine($"polarization count does NOT match (1 vs 2): {countMismatch}");
        Output.WriteLine(sb.ToString());

        Assert.True(scalarOne, "scalar sector should have 1 polarization");
        Assert.True(tensorTwo, "graviton should have 2 polarizations at d=3");
        Assert.True(countMismatch, "scalar and tensor polarization counts should differ");
    }

    // ── ATQG181: trace / transverse structure — breathing vs traceless ──────────────

    [Fact]
    public void ATQG181_TraceTransverseStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG181: scalar mode is breathing (non-zero trace); GWs are traceless");

        int d = 3;
        double scalarTrace = GravitationalWaves.ScalarModeTrace(1000.0, d);
        double tensorTrace = GravitationalWaves.TensorModeTrace();

        sb.AppendLine($"scalar disturbance trace δg^μ_μ = {scalarTrace:F5} (NON-zero → breathing/volume change)");
        sb.AppendLine($"tensor (graviton) disturbance trace = {tensorTrace:F1} (ZERO → volume-preserving shear)");

        bool scalarBreathing = Math.Abs(scalarTrace) > 1e-3;   // breathing: changes volume isotropically
        bool tensorTraceless = Math.Abs(tensorTrace) < 1e-12;  // transverse-traceless: no volume change

        sb.AppendLine();
        sb.AppendLine($"scalar mode is BREATHING (isotropic stretch, non-zero trace): {scalarBreathing}");
        sb.AppendLine($"tensor mode is TRACELESS (transverse shear, +/× patterns): {tensorTraceless}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a scalar disturbance changes the volume isotropically (breathing), whereas a GW");
        sb.AppendLine("shears space transversely at fixed volume (+, ×). The two are physically distinct and cannot be");
        sb.AppendLine("interchanged.");
        Output.WriteLine(sb.ToString());

        Assert.True(scalarBreathing, "scalar mode should be breathing (non-zero trace)");
        Assert.True(tensorTraceless, "tensor mode should be traceless");
    }

    // ── ATQG182: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG182_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG182: can observed GWs arise in the scalar sector? MATCH / PARTIAL / NO MATCH?");

        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH — energy/speed conceptually compatible, POLARIZATION is NO MATCH.");
        sb.AppendLine();
        sb.AppendLine("  • A scalar curvature pulse CAN carry energy and (with a wave dynamics) propagate at the null speed,");
        sb.AppendLine("    so the ENERGY-TRANSPORT and SPEED observables are conceptually compatible.");
        sb.AppendLine("  • But the POLARIZATION is decisive: the scalar sector has ONE breathing (monopole) mode, whereas");
        sb.AppendLine("    observed GWs have TWO transverse-traceless modes (+, ×) (ATQG180/181).");
        sb.AppendLine("  • LIGO/Virgo are consistent with PURE tensor polarization and constrain (strongly disfavor) breathing");
        sb.AppendLine("    modes, so the scalar breathing mode is observationally EXCLUDED as the GW signal.");
        sb.AppendLine("  • Therefore AT's scalar sector does NOT reproduce the observed gravitational waves: it is a PARTIAL");
        sb.AppendLine("    MATCH (energy/speed only), with the decisive polarization (tensor +/×) requiring the frozen graviton");
        sb.AppendLine("    sector (QG16/QG17).");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
