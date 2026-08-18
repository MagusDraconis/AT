using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 41 — derive the TRM acceleration law. Tests whether the √(g_N·a0) term emerges from Q-event
/// saturation. Classify: DERIVED / PARTIAL MATCH / IMPORTED.
///
/// Tests: TQMQG410 (saturation has a core, no √ regime), TQMQG411 (opposite sign), TQMQG412 (classification).
/// </summary>
public class TQMQG_Phase41_TRMAccelerationOriginTests : ResearchTestBase
{
    public TQMQG_Phase41_TRMAccelerationOriginTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG410: saturation has a regular core, no √ regime ─────────────────────────

    [Fact]
    public void TQMQG410_SaturationHasCoreNoSqrt()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG410: saturation acceleration g_sat = g_N(1-e^(-r^3/rc^3))");

        double G = 1.0, M = 1.0, rc = 1.0;
        double[] rs = { 0.1, 0.5, 1.0, 5.0, 10.0 };
        foreach (var r in rs)
        {
            double gN = TRMAccelerationOrigin.Newtonian(G, M, r);
            double gSat = TRMAccelerationOrigin.SaturationAcceleration(G, M, r, rc);
            double ratio = gSat / gN;
            sb.AppendLine($"r = {r,5:F2}  g_N = {gN:F6}  g_sat = {gSat:F6}  g_sat/g_N = {ratio:F6}");
        }

        bool coreAtSmallR = TRMAccelerationOrigin.SaturationAcceleration(G, M, 0.1, rc)
                            < TRMAccelerationOrigin.Newtonian(G, M, 0.1);
        bool newtonAtLargeR = Math.Abs(
            TRMAccelerationOrigin.SaturationFactor(10.0, rc) - 1.0) < 1e-6;

        sb.AppendLine();
        sb.AppendLine($"core (suppression) at small r: {coreAtSmallR}");
        sb.AppendLine($"Newtonian recovery at large r: {newtonAtLargeR}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: saturation gives a regular CORE (g_sat → 0 as r→0) and Newtonian recovery at large r. The");
        sb.AppendLine("correction factor is always ≤ 1, so there is NO 1/r (flat-curve) regime and hence no √(g_N·a0) term.");
        Output.WriteLine(sb.ToString());

        Assert.True(coreAtSmallR, "saturation should suppress at small r");
        Assert.True(newtonAtLargeR, "saturation should recover Newtonian at large r");
    }

    // ── TQMQG411: opposite sign / regime ──────────────────────────────────────────────

    [Fact]
    public void TQMQG411_OppositeSign()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG411: saturation suppresses; MOND enhances");

        double G = 1.0, M = 1.0, rc = 1.0, a0 = 0.01, lambda = 1.0;
        double r = 10.0;
        double gN = TRMAccelerationOrigin.Newtonian(G, M, r);
        double gSat = TRMAccelerationOrigin.SaturationAcceleration(G, M, r, rc);
        double gTrm = TRMAccelerationOrigin.TrmAcceleration(gN, a0, lambda);

        sb.AppendLine($"at r = {r}:  g_N = {gN:F6}  g_sat = {gSat:F6}  g_TRM = {gTrm:F6}");
        sb.AppendLine($"saturation ratio g_sat/g_N = {gSat / gN:F6}  (≤ 1, suppression)");
        sb.AppendLine($"MOND ratio      g_TRM/g_N = {gTrm / gN:F6}  (≥ 1, enhancement)");

        bool satSuppresses = TRMAccelerationOrigin.SaturationIsSuppression(r, rc);
        bool mondEnhances = TRMAccelerationOrigin.MondIsEnhancement(gN, a0, lambda);

        sb.AppendLine();
        sb.AppendLine($"saturation is a SUPPRESSION (≤ g_N): {satSuppresses}");
        sb.AppendLine($"MOND is an ENHANCEMENT (≥ g_N):       {mondEnhances}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: saturation and MOND act in OPPOSITE directions — saturation weakens gravity at the core,");
        sb.AppendLine("MOND strengthens it at large radii. They cannot be the same object; the √(g_N·a0) term does not emerge.");
        Output.WriteLine(sb.ToString());

        Assert.True(satSuppresses, "saturation should suppress");
        Assert.True(mondEnhances, "MOND should enhance");
        double gSatSmall = TRMAccelerationOrigin.SaturationAcceleration(G, M, 0.5, rc);
        double gNSmall = TRMAccelerationOrigin.Newtonian(G, M, 0.5);
        Assert.True(gSatSmall < gNSmall, "saturation suppresses at the core (small r)");
        Assert.True(gTrm > gN, "MOND enhances at large r");
    }

    // ── TQMQG412: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG412_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG412: DERIVED / PARTIAL MATCH / IMPORTED?");

        bool reproduces = TRMAccelerationOrigin.SaturationReproducesMond();

        sb.AppendLine($"saturation reproduces √(g_N·a0): {reproduces}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: IMPORTED.");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: saturation yields g_sat = g_N(1−e^(−r³/r_c³)) — a core (suppression) with no 1/r regime;");
        sb.AppendLine("    the MOND term √(g_N·a0) ∝ 1/r (enhancement) is a different function with a new scale a0.");
        sb.AppendLine("  • The two act in opposite regimes (core vs large-r) and opposite sign (≤ vs ≥ g_N).");
        sb.AppendLine("  • TQM DOES derive flat rotation curves, but via the log-deficit (α=0 scale-free) profile (G4-ME Phases 3–4),");
        sb.AppendLine("    a DIFFERENT derived mechanism — not saturation and not the exact √ interpolating form.");
        sb.AppendLine("  • So the specific √(g_N·a0)/λ term is IMPORTED: a MOND ansatz with scale a0 that Q-event saturation");
        sb.AppendLine("    does not produce.");
        Output.WriteLine(sb.ToString());

        Assert.False(reproduces, "saturation should not reproduce the MOND term");
        Assert.True(TRMAccelerationOrigin.SaturationIsSuppression(10.0, 1.0));
        Assert.True(TRMAccelerationOrigin.MondIsEnhancement(0.01, 0.01, 1.0));
    }
}
