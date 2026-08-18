using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 51 — origin of the two-primitive structure. Determines why two primitives are needed instead of one.
/// Classify: FORCED / PREFERRED / CONTINGENT.
///
/// Tests: TQMQG510 (spin + kind mismatch), TQMQG511 (single primitive insufficient), TQMQG512 (classification).
/// </summary>
public class TQMQG_Phase51_OriginOfTwoPrimitivesTests : ResearchTestBase
{
    public TQMQG_Phase51_OriginOfTwoPrimitivesTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG510: spin + kind mismatch ───────────────────────────────────────────────

    [Fact]
    public void TQMQG510_SpinAndKindMismatch()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG510: Q-events and ψ differ in spin AND kind");

        foreach (var p in OriginOfTwoPrimitives.Primitives)
        {
            double spin = OriginOfTwoPrimitives.Spin(p);
            string kind = OriginOfTwoPrimitives.Kind(p);
            sb.AppendLine($"{p,-10} -> spin {spin}   kind = {kind}");
        }

        bool spinMismatch = OriginOfTwoPrimitives.Spin("q-events") != OriginOfTwoPrimitives.Spin("psi");
        bool kindMismatch = OriginOfTwoPrimitives.Kind("q-events") != OriginOfTwoPrimitives.Kind("psi");

        sb.AppendLine();
        sb.AppendLine($"spin mismatch (0 vs 2): {spinMismatch}");
        sb.AppendLine($"kind mismatch (process vs field): {kindMismatch}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Q-events are a DISCRETE scalar PROCESS (counting events → ρ, spin-0); ψ is a CONTINUOUS tensor");
        sb.AppendLine("FIELD (propagating spin-2 waves). They differ irreducibly in both spin and kind.");
        Output.WriteLine(sb.ToString());

        Assert.True(spinMismatch, "the two primitives should differ in spin");
        Assert.True(kindMismatch, "the two primitives should differ in kind");
    }

    // ── TQMQG511: single primitive insufficient ──────────────────────────────────────

    [Fact]
    public void TQMQG511_SinglePrimitiveInsufficient()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG511: can one primitive do both jobs?");

        bool single = OriginOfTwoPrimitives.SinglePrimitiveSufficient();
        bool forced = OriginOfTwoPrimitives.Forced();

        sb.AppendLine($"a single primitive suffices for both roles: {single}");
        sb.AppendLine($"two-primitive structure is FORCED:           {forced}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a single primitive would have to be BOTH a spin-0 source (counting discrete events) AND a");
        sb.AppendLine("spin-2 propagator (continuous tensor waves). A field has a definite spin, and a process is not a field —");
        sb.AppendLine("so no single primitive can serve both roles. Two primitives is the minimum.");
        Output.WriteLine(sb.ToString());

        Assert.False(single, "a single primitive should not suffice");
        Assert.True(forced, "the two-primitive structure should be forced");
    }

    // ── TQMQG512: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG512_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG512: FORCED / PREFERRED / CONTINGENT?");

        bool forced = OriginOfTwoPrimitives.Forced();
        bool tensorContingent = OriginOfTwoPrimitives.TensorHalfContingent();

        sb.AppendLine($"CLASSIFICATION: {OriginOfTwoPrimitives.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • FORCED (minimal): the two roles — spin-0 actualization/source and spin-2 propagation — are irreducibly");
        sb.AppendLine("    different (spin 0 vs 2, process vs field); no single primitive can be both, so two is the minimum.");
        sb.AppendLine("  • TIERED: the Q-events half is FORCED (actualization is intrinsically a discrete scalar process); the ψ");
        sb.AppendLine($"    half is CONTINGENT on the spin-2 GW observation: {tensorContingent} (QG48).");
        sb.AppendLine("  • NOT PREFERRED/NOT ARBITRARY: the structure is not one choice among many — it is the minimal closure.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("FORCED", OriginOfTwoPrimitives.Classify());
        Assert.True(forced);
        Assert.True(tensorContingent);
    }
}
