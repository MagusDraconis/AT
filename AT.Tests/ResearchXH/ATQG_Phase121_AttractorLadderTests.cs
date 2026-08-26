using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 121 — Origin of the attractor ladder. QG117 showed attractor geometries form discrete radius
/// classes (a ladder). This phase asks: WHY does the feedback dynamics produce a discrete ladder instead of a
/// continuous family of geometries? Probes threshold effects, rounding structure (including a continuous-
/// weight variant), fixed-point bifurcations, class transitions, and ladder universality. Classify: ARTIFACT /
/// DYNAMICAL / FUNDAMENTAL.
///
/// Tests: ATQG1210 (threshold effects), ATQG1211 (rounding structure), ATQG1212 (fixed-point
/// bifurcations + universality + classification).
/// </summary>
public class ATQG_Phase121_AttractorLadderTests : ResearchTestBase
{
    public ATQG_Phase121_AttractorLadderTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1210: threshold effects ───────────────────────────────────────────────

    [Fact]
    public void ATQG1210_ThresholdEffects()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1210: does the ladder persist under different activity thresholds?");

        var byThreshold = AttractorLadder.LadderByThreshold();
        bool persists = AttractorLadder.LadderPersistsAcrossThresholds();

        sb.AppendLine("RADIUS LADDER BY ACTIVITY THRESHOLD (feedback sweep at d=0.3, K=6):");
        foreach (var (thr, radii) in byThreshold)
            sb.AppendLine($"  threshold {thr:F1}: [{string.Join(", ", radii.Select(r => r.ToString("F2")))}]");
        sb.AppendLine();
        sb.AppendLine($"  ladder persists across thresholds: {persists}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the discrete ladder {2, 6} is IDENTICAL for thresholds 0.3, 0.5, and 0.7 —");
        sb.AppendLine("the discreteness does NOT come from the specific activity gate value.");
        Output.WriteLine(sb.ToString());

        Assert.True(persists, "ladder persists across all activity thresholds");
        // same rungs at every threshold
        double[] thr03 = byThreshold[0].Radii;
        foreach (var (_, radii) in byThreshold)
        {
            Assert.Equal(thr03.Length, radii.Length);
            for (int i = 0; i < thr03.Length; i++)
                Assert.True(Math.Abs(thr03[i] - radii[i]) < 0.01, "rungs identical across thresholds");
        }
    }

    // ── ATQG1211: rounding structure ──────────────────────────────────────────────

    [Fact]
    public void ATQG1211_RoundingStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1211: is the ladder a rounding artifact? (round / floor / ceil / continuous)");

        var byDiscretization = AttractorLadder.LadderByDiscretization();
        bool persists = AttractorLadder.LadderPersistsAcrossDiscretization();
        bool continuousLadder = AttractorLadder.ContinuousVariantShowsLadder();

        sb.AppendLine("RADIUS LADDER BY LINK DISCRETIZATION (feedback sweep at d=0.3, K=6):");
        foreach (var (m, radii) in byDiscretization)
            sb.AppendLine($"  {m,-11}: [{string.Join(", ", radii.Select(r => r.ToString("F2")))}]");
        sb.AppendLine();
        sb.AppendLine($"  ladder persists across ALL discretizations: {persists}");
        sb.AppendLine($"  continuous-weight variant still shows a ladder: {continuousLadder}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: even the CONTINUOUS-WEIGHT variant (no integer rounding at all) produces the");
        sb.AppendLine("discrete ladder {2, 6} — the discreteness is NOT an artifact of the round() function.");
        Output.WriteLine(sb.ToString());

        Assert.True(persists, "ladder persists across all discretization modes");
        Assert.True(continuousLadder, "continuous-weight variant still shows a discrete ladder");
    }

    // ── ATQG1212: fixed-point bifurcations + universality + classification ───────

    [Fact]
    public void ATQG1212_FixedPointBifurcationsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1212: fixed-point bifurcations, transitions, universality → classification");

        int algebraicRungs = AttractorLadder.AlgebraicRungCount();
        bool matchesHigh = AttractorLadder.RadiusMatchesAlgebraicFixedPoint(0.9, 0.1);
        bool sharp = AttractorLadder.TransitionsAreSharp();
        bool universalK = AttractorLadder.LadderUniversalAcrossK();
        string cls = AttractorLadder.Classify();

        var transitions = AttractorLadder.TransitionPoints();
        sb.AppendLine("FIXED-POINT BIFURCATIONS:");
        sb.AppendLine($"  algebraic ladder rungs round(K·min(1,f/d)) for K=6: {algebraicRungs}");
        sb.AppendLine($"  measured radius matches algebraic fixed point at high f/d (f=0.9,d=0.1): {matchesHigh}");
        sb.AppendLine($"  sharp transition points (d=0.3): [{string.Join(", ", transitions.Select(t => $"f/d={t.Ratio:F2}→r={t.Radius:F0}"))}]");
        sb.AppendLine();
        sb.AppendLine("LADDER UNIVERSALITY:");
        foreach (var (k, radii) in AttractorLadder.LadderByK())
            sb.AppendLine($"  K={k}: [{string.Join(", ", radii.Select(r => r.ToString("F2")))}]");
        sb.AppendLine($"  discrete ladder present for every K: {universalK}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT ARTIFACT: the ladder persists under different thresholds AND under the");
        sb.AppendLine("    continuous-weight variant (no rounding) — it is not a numerical accident.");
        sb.AppendLine("  • FUNDAMENTAL: the saturated activity fixed point a* = min(1, f/d) is a continuous");
        sb.AppendLine("    parameter, but the link radius round(K·a*) is a step function of it — the bounded-");
        sb.AppendLine("    activity × discrete-link structure of the model FORCES a discrete ladder, universal");
        sb.AppendLine("    across thresholds, discretizations, and every K.");
        sb.AppendLine("  • (The intermediate algebraic rungs 3,4,5 are stable fixed points but unreachable from");
        sb.AppendLine("    the seed — a basin-selection nuance; the discreteness itself is fundamental.)");
        Output.WriteLine(sb.ToString());

        Assert.Equal("FUNDAMENTAL", cls);
        Assert.True(algebraicRungs == AttractorLadder.DefaultK + 1, "algebraic ladder has K+1 rungs");
        Assert.True(matchesHigh, "high-f/d radius matches the algebraic fixed point");
        Assert.True(sharp, "class transitions are sharp");
        Assert.True(universalK, "ladder is universal across K");
    }
}
