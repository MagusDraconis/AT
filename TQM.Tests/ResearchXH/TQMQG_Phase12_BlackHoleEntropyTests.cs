using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 12 — black-hole microstate test. Tests whether horizon entropy S ∝ Area emerges from the
/// counting measure (horizon boundary events vs bulk volume events). Classify: MATCH / PARTIAL MATCH / NO MATCH.
///
/// Tests: TQMQG120 (event counting: area vs volume), TQMQG121 (microstate entropy scaling), TQMQG122 (classification).
/// </summary>
public class TQMQG_Phase12_BlackHoleEntropyTests : ResearchTestBase
{
    public TQMQG_Phase12_BlackHoleEntropyTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG120: event counting — area vs volume scaling ────────────────────────────

    [Fact]
    public void TQMQG120_EventCountingAreaVsVolume()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG120: the counting measure gives both area and volume counts");

        int d = 3;   // 3+1 spacetime
        double R = 1.0;
        sb.AppendLine($"d={d} (3+1); horizon radius R:");
        sb.AppendLine($"{"R",6} {"area R²",10} {"volume R³",11} {"S_horizon ∝ R²",16} {"S_bulk ∝ R³",14}");
        for (double rr = 1.0; rr <= 8.0; rr *= 2.0)
        {
            double a = BlackHoleEntropy.HorizonAreaScale(d, rr);
            double v = BlackHoleEntropy.BulkVolumeScale(d, rr);
            double sh = BlackHoleEntropy.HorizonEntropy(d, rr);
            double sbEntropy = BlackHoleEntropy.BulkEntropy(d, rr);
            sb.AppendLine($"{rr,6:F0} {a,10:F0} {v,11:F0} {sh,16:F2} {sbEntropy,14:F2}");
        }

        double areaRatio = BlackHoleEntropy.HorizonAreaScale(d, 2.0 * R) / BlackHoleEntropy.HorizonAreaScale(d, R);
        double volumeRatio = BlackHoleEntropy.BulkVolumeScale(d, 2.0 * R) / BlackHoleEntropy.BulkVolumeScale(d, R);

        bool areaScalesAsSurface = Math.Abs(areaRatio - Math.Pow(2, d - 1)) < 1e-9;   // 2² = 4
        bool volumeScalesAsBulk = Math.Abs(volumeRatio - Math.Pow(2, d)) < 1e-9;      // 2³ = 8

        sb.AppendLine();
        sb.AppendLine($"horizon area ∝ R^(d−1) (ratio 2²=4): {areaScalesAsSurface}");
        sb.AppendLine($"bulk volume ∝ R^d (ratio 2³=8): {volumeScalesAsBulk}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the counting measure provides BOTH a boundary (area) count ∝ R^(d−1) and a bulk");
        sb.AppendLine("(volume) count ∝ R^d. The horizon is the BOUNDARY, so its event count scales as AREA.");
        Output.WriteLine(sb.ToString());

        Assert.True(areaScalesAsSurface, "horizon should scale as area R^(d-1)");
        Assert.True(volumeScalesAsBulk, "bulk should scale as volume R^d");
    }

    // ── TQMQG121: microstate multiplicity → S ∝ Area ─────────────────────────────────

    [Fact]
    public void TQMQG121_MicrostateEntropyScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG121: horizon microstates give S ∝ Area, not S ∝ Volume");

        int d = 3;
        double sR = BlackHoleEntropy.HorizonEntropy(d, 1.0);
        double s2R = BlackHoleEntropy.HorizonEntropy(d, 2.0);
        double ratio = s2R / sR;

        double wR = BlackHoleEntropy.Microstates(d, 1.0);
        double w2R = BlackHoleEntropy.Microstates(d, 2.0);
        double wRatio = w2R / wR;

        sb.AppendLine($"S(R=1) = {sR:F3}, S(R=2) = {s2R:F3}, ratio = {ratio:F2}");
        sb.AppendLine($"W(R=1) = {wR:F1}, W(R=2) = {w2R:E2}, ratio = {wRatio:E2}");
        sb.AppendLine($"S ∝ R^(d−1) = R² (area), so S(2R)/S(R) = 4 (area law), NOT 8 (volume law)");

        bool areaLaw = Math.Abs(ratio - Math.Pow(2, d - 1)) < 1e-9;   // S ∝ R² → ratio 4
        bool exponentialMicrostates = Math.Abs(Math.Log(wRatio) - (s2R - sR)) < 1e-9;   // W = e^S

        sb.AppendLine();
        sb.AppendLine($"S ∝ Area (ratio 4, not 8): {areaLaw}");
        sb.AppendLine($"microstates W = e^S (exponential in area): {exponentialMicrostates}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: counting horizon events (1 bit per boundary cell) gives S ∝ Area and an exponential");
        sb.AppendLine("microstate multiplicity W = e^(A ln 2) — the Bekenstein–Hawking area law, from counting statistics.");
        Output.WriteLine(sb.ToString());

        Assert.True(areaLaw, "horizon entropy should follow the area law (S ∝ R^(d-1))");
        Assert.True(exponentialMicrostates, "microstates should be exponential in the horizon area");
    }

    // ── TQMQG122: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG122_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG122: does S ∝ Area emerge naturally? MATCH / PARTIAL / NO MATCH?");

        sb.AppendLine("CLASSIFICATION: MATCH (S ∝ Area from horizon counting), with a holographic caveat.");
        sb.AppendLine();
        sb.AppendLine("  • The counting measure gives the horizon a BOUNDARY count ∝ R^(d−1) (area), distinct from the");
        sb.AppendLine("    bulk count ∝ R^d (volume) (TQMQG120).");
        sb.AppendLine("  • Counting 1 bit per horizon cell gives S ∝ Area and W = e^(A ln 2) — the Bekenstein–Hawking area");
        sb.AppendLine("    law (TQMQG121).");
        sb.AppendLine("  • CAVEAT (holographic): the area law requires identifying the entropy with the HORIZON (boundary)");
        sb.AppendLine("    degrees of freedom, not the bulk. The counting measure provides both; the area law follows only");
        sb.AppendLine("    from the boundary identification, which is a natural (minimal) choice, not a dynamical derivation.");
        sb.AppendLine("  • CAVEAT (mass scaling): TQM's deficit mass (enclosed deficit) ∝ R^d, whereas Schwarzschild M ∝ R,");
        sb.AppendLine("    so the S ∝ M² relation (and the exact 1/4 coefficient) is NOT reproduced — only the area law");
        sb.AppendLine("    S ∝ Area (the scaling with radius) is native.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
