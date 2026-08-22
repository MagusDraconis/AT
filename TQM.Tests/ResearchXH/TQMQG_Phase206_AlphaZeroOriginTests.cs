using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 206 — Alpha Zero Origin. Derive α = 0 (the flat-rotation-curve deficit exponent) from
/// TRM/D96 instead of assuming it. No new primitives, deterministic.
/// </summary>
public class TQMQG_Phase206_AlphaZeroOriginTests : ResearchTestBase
{
    public TQMQG_Phase206_AlphaZeroOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2060_FlatRotationRequiresAlphaZero()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2060: flat rotation requires α = 0 exactly");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The general abundance deficit is m(r) ∝ r^(−α); α = 0 → LogDeficit.");
        sb.AppendLine("  - For such a deficit the field a ∝ r^(−α−1), so v² = r·|a| ∝ r^(−α).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (double a in new[] { -0.6, -0.3, 0.0, 0.3, 0.6 })
            sb.AppendLine($"  α = {a,5:F1}: rotation-curve log-slope = {AlphaZeroOrigin.RotationCurveSlope(a),6:F2}  (0 = flat)");
        sb.AppendLine($"  Flat requires α = 0? {AlphaZeroOrigin.FlatRequiresAlphaZero()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - v² ∝ r^(−α): the rotation curve is flat (v = const) ONLY when α = 0.");
        sb.AppendLine("  - α ≠ 0 gives either a rising (α &lt; 0) or falling (α &gt; 0) curve — not flat.");

        Output.WriteLine(sb.ToString());

        Assert.True(AlphaZeroOrigin.FlatRequiresAlphaZero(), "flat rotation must require α = 0");
        Assert.Equal(0.0, AlphaZeroOrigin.RotationCurveSlope(0.0), 9);
    }

    [Fact]
    public void TQMQG2061_LogDeficitSelfSimilarAndStable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2061: the log deficit is self-similar and α = 0 is the unique stable point");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The D96 counting measure is octave-organized (occupancies [4,4,87], QG155).");
        sb.AppendLine("  - A scale-free deficit contributes EQUALLY in every octave (self-similar).");
        sb.AppendLine();

        var per = AlphaZeroOrigin.DeficitPerOctave();
        double s0 = AlphaZeroOrigin.OctaveUniformity(0.0);
        double sNeg = AlphaZeroOrigin.OctaveUniformity(-0.3);
        double sPos = AlphaZeroOrigin.OctaveUniformity(0.3);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Deficit per octave (α=0): {per[0]:F4}, {per[1]:F4}, {per[2]:F4}  (equal = self-similar)");
        sb.AppendLine($"  Self-similar (log deficit)? {AlphaZeroOrigin.LogDeficitIsSelfSimilar()}");
        sb.AppendLine($"  Octave-deficit spread: α=0 → {s0:E2}, α=−0.3 → {sNeg:E2}, α=+0.3 → {sPos:E2}");
        sb.AppendLine($"  α = 0 unique scale-free point? {AlphaZeroOrigin.AlphaZeroIsUniqueScaleFree()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The log deficit (α=0) contributes EQUALLY per octave — the self-similar choice.");
        sb.AppendLine("  - α ≠ 0 breaks the self-similarity (outer-dominant or core-dominant): α=0 is the");
        sb.AppendLine("    unique stable, scale-free point.");

        Output.WriteLine(sb.ToString());

        Assert.True(AlphaZeroOrigin.LogDeficitIsSelfSimilar(), "the log deficit must be self-similar");
        Assert.True(AlphaZeroOrigin.AlphaZeroIsUniqueScaleFree(), "α=0 must be the unique scale-free point");
    }

    [Fact]
    public void TQMQG2062_ClassificationAlphaZeroOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2062: classification — ALPHA-ZERO ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Matter = ρ̄−ρ is the actualization deficit (QG194), octave-organized (QG155).");
        sb.AppendLine("  - Uniform per-mode deficit over the self-similar octave ladder → equal per octave → α=0.");
        sb.AppendLine("  - α = 0 ⇔ M ∝ R (QG184 mass-radius, Hawking T ∝ 1/R).");
        sb.AppendLine();

        int score = AlphaZeroOrigin.OriginScore();
        string classification = AlphaZeroOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 flat requires α=0 ({AlphaZeroOrigin.FlatRequiresAlphaZero()})");
        sb.AppendLine($"    +1 log deficit self-similar ({AlphaZeroOrigin.LogDeficitIsSelfSimilar()})");
        sb.AppendLine($"    +1 α=0 unique scale-free ({AlphaZeroOrigin.AlphaZeroIsUniqueScaleFree()})");
        sb.AppendLine($"    +1 α=0 ⇔ M ∝ R ({AlphaZeroOrigin.AlphaZeroGivesLinearMassRadius()})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - α = 0 is DERIVED, not assumed: the flat rotation curve is the unique scale-free");
        sb.AppendLine("    deficit profile of the octave-organized counting measure.");
        sb.AppendLine("  - It is stable (equal per octave), self-similar, actualization-scaled (QG194/155),");
        sb.AppendLine("    and consistent with the derived mass-radius relation M ∝ R (QG184).");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("ALPHA-ZERO ORIGIN", classification);
        Assert.Equal(4, score);
    }
}
