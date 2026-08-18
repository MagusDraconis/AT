using System.Globalization;
using System.Numerics;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 65 — can quantum interference emerge from link phases?
/// Classify: MATCH / PARTIAL MATCH / NO MATCH.
///
/// Tests: TQMQG650 (path amplitude + accumulation), TQMQG651 (double-slit), TQMQG652 (Born rule + classification).
/// </summary>
public class TQMQG_Phase65_InterferenceFromLinksTests : ResearchTestBase
{
    public TQMQG_Phase65_InterferenceFromLinksTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG650: path amplitudes and phase accumulation ────────────────────────────

    [Fact]
    public void TQMQG650_PathAmplitudeAndAccumulation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG650: a path accumulates phase and carries the amplitude e^(iθ)");

        double[] linkPhases = { 0.3, 0.5, 0.2 };
        double total = InterferenceFromLinks.PhaseAccumulation(linkPhases);
        double holonomy = InterferenceFromLinks.LoopHolonomy(linkPhases);
        Complex amp = InterferenceFromLinks.PathAmplitude(total);

        sb.AppendLine($"link phases: {string.Join(", ", linkPhases)}");
        sb.AppendLine($"path phase accumulation = {total:F4}   (holonomy = {holonomy:F4})");
        sb.AppendLine($"path amplitude e^(iθ) = {amp.Real:F4} + {amp.Imaginary:F4} i   (|amp| = {InterferenceFromLinks.BornRule(amp):F4})");

        bool unitModulus = Math.Abs(InterferenceFromLinks.BornRule(amp) - 1.0) < 1e-12;

        sb.AppendLine();
        sb.AppendLine($"a single path amplitude has unit modulus (|e^(iθ)|=1): {unitModulus}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: phases accumulate along a path, giving the amplitude e^(iΣθ); the loop holonomy is the");
        sb.AppendLine("gauge-invariant sum around a closed loop.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1.0, total, 3);
        Assert.Equal(1.0, holonomy, 3);
        Assert.True(unitModulus, "path amplitude should have unit modulus");
    }

    // ── TQMQG651: double-slit interference ──────────────────────────────────────────

    [Fact]
    public void TQMQG651_DoubleSlit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG651: the double-slit pattern 2 + 2cos(Δθ) emerges");

        double constructive = InterferenceFromLinks.DoubleSlitProbability(0.0, 0.0);
        double destructive = InterferenceFromLinks.DoubleSlitProbability(0.0, Math.PI);
        double partial = InterferenceFromLinks.DoubleSlitProbability(0.0, Math.PI / 2.0);

        sb.AppendLine($"Δθ = 0     : P = {constructive:F4}  (constructive)");
        sb.AppendLine($"Δθ = π     : P = {destructive:F4}  (destructive)");
        sb.AppendLine($"Δθ = π/2   : P = {partial:F4}  (partial)");

        bool correct = Math.Abs(constructive - 4.0) < 1e-12
                       && Math.Abs(destructive) < 1e-12
                       && Math.Abs(partial - 2.0) < 1e-12;

        sb.AppendLine();
        sb.AppendLine($"double-slit pattern matches 2 + 2cos(Δθ): {correct}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: two paths with different link phases interfere — |e^(iθ1)+e^(iθ2)|² = 2 + 2cos(θ1−θ2) — the");
        sb.AppendLine("classic double-slit interference pattern.");
        Output.WriteLine(sb.ToString());

        Assert.True(correct, "the double-slit pattern should be correct");
    }

    // ── TQMQG652: Born rule + classification ─────────────────────────────────────────

    [Fact]
    public void TQMQG652_BornRuleAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG652: Born rule consistency and classification");

        double p1 = InterferenceFromLinks.BornRule(InterferenceFromLinks.PathAmplitude(0.5));
        Complex two = InterferenceFromLinks.PathAmplitude(0.0) + InterferenceFromLinks.PathAmplitude(Math.PI);
        double p2 = InterferenceFromLinks.BornRule(two);

        sb.AppendLine($"Born rule |e^(iθ)|² = {p1:F4}  (single path)");
        sb.AppendLine($"Born rule |e^(iθ1)+e^(iθ2)|² = {p2:F4}  (two-path interference)");

        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: MATCH");
        sb.AppendLine("  • path amplitudes, phase accumulation, loop holonomies, the double-slit pattern, and the Born rule are all");
        sb.AppendLine("    NATURALLY recovered from link phases.");
        sb.AppendLine("  • CAVEAT: the U(1) phase itself is the new primitive (QG62) — interference emerges GIVEN the phase, not");
        sb.AppendLine("    from the bare network.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1.0, p1, 3);
        Assert.Equal(0.0, p2, 3);
        Assert.True(Math.Abs(InterferenceFromLinks.DoubleSlitProbability(0.0, Math.PI / 2.0) - 2.0) < 1e-12);
    }
}
