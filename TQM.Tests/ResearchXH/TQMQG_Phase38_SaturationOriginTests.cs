using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 38 — origin of finite-density saturation. Determines why Q-events saturate at a critical density.
/// Classify: DERIVED / PREFERRED / IMPORTED.
///
/// Tests: TQMQG380 (mechanism census), TQMQG381 (existence vs value), TQMQG382 (classification).
/// </summary>
public class TQMQG_Phase38_SaturationOriginTests : ResearchTestBase
{
    public TQMQG_Phase38_SaturationOriginTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG380: mechanism census ─────────────────────────────────────────────────────

    [Fact]
    public void TQMQG380_MechanismCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG380: the five mechanisms all reduce to discreteness");

        int discreteRoot = 0;
        foreach (var m in SaturationOrigin.Mechanisms)
        {
            bool root = SaturationOrigin.IsDiscreteRoot(m);
            sb.AppendLine($"{m,-22} -> discrete-tick root: {root}");
            if (root) discreteRoot++;
        }

        bool noNewPrimitive = !SaturationOrigin.RequiresNewPrimitive();

        sb.AppendLine();
        sb.AppendLine($"mechanisms rooted in discreteness: {discreteRoot}/5");
        sb.AppendLine($"requires a new primitive: {!noNewPrimitive}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: occupancy limits, update conflicts, exclusion principles, branching congestion, and tick");
        sb.AppendLine("capacity are all the SAME fact — a Q-event is a discrete tick, so a discrete counting measure has a");
        sb.AppendLine("maximal density. No new primitive is needed to reach saturation.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, discreteRoot);
        Assert.True(noNewPrimitive, "saturation should need no new primitive");
    }

    // ── TQMQG381: existence vs value ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG381_ExistenceVsValue()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG381: existence is derived; the value is imported");

        bool existence = SaturationOrigin.ExistenceDerived();
        bool value = SaturationOrigin.ValueImported();

        sb.AppendLine($"EXISTENCE of a critical density is DERIVED: {existence}  (discreteness ⇒ max density)");
        sb.AppendLine($"VALUE of ρ_c is IMPORTED/supplied:        {value}  (QG14: bounds, no native cutoff)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the discreteness of Q-events FORCES a maximal density — saturation exists by construction.");
        sb.AppendLine("But the actual number ρ_c (equivalently r_c) is not derivable from the primitives; it is a supplied");
        sb.AppendLine("parameter, exactly as QG14 concluded for the Planck cutoff.");
        Output.WriteLine(sb.ToString());

        Assert.True(existence, "saturation existence should be derived");
        Assert.True(value, "the critical-density value should be imported");
    }

    // ── TQMQG382: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG382_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG382: DERIVED / PREFERRED / IMPORTED?");

        sb.AppendLine($"CLASSIFICATION: {SaturationOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • DERIVED (existence): a Q-event is a discrete tick (QG29); a discrete counting measure ρ cannot be");
        sb.AppendLine("    subdivided, so there is necessarily a maximal density — saturation exists by construction.");
        sb.AppendLine("  • IMPORTED (value): the numerical ρ_c (equivalently r_c) is supplied, not derivable — consistent with");
        sb.AppendLine("    QG14 (TQM has bounds but no native cutoff value).");
        sb.AppendLine("  • So the MECHANISM is DERIVED; only the SCALE is imported. Saturation is not a hand-inserted assumption.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", SaturationOrigin.Classify());
        Assert.True(SaturationOrigin.ExistenceDerived());
        Assert.True(SaturationOrigin.ValueImported());
    }
}
