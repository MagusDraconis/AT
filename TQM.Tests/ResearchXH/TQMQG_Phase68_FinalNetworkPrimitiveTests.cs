using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 68 — unified primitive audit. Determines whether the four sectors are four primitives or one object.
/// Classify: FOUR PRIMITIVES / TWO PRIMITIVES / ONE NETWORK PRIMITIVE.
///
/// Tests: TQMQG680 (four sectors), TQMQG681 (one complete link), TQMQG682 (classification).
/// </summary>
public class TQMQG_Phase68_FinalNetworkPrimitiveTests : ResearchTestBase
{
    public TQMQG_Phase68_FinalNetworkPrimitiveTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG680: the four sectors ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG680_FourSectors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG680: ρ, ψ, θ, S are four irreducible sectors");

        foreach (var s in FinalNetworkPrimitive.Sectors)
            sb.AppendLine($"{s,-16} -> {FinalNetworkPrimitive.Kind(s)}");

        bool irreducible = FinalNetworkPrimitive.SectorsIrreducible();

        sb.AppendLine();
        sb.AppendLine($"four sectors are IRREDUCIBLE (independent d.o.f.): {irreducible}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: spin-0 (magnitude), spin-2 (shape), U(1) (gauge), and SU(2) (spinor) are four different");
        sb.AppendLine("representations — independent degrees of freedom that cannot be reduced to each other.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, FinalNetworkPrimitive.Sectors.Length);
        Assert.True(irreducible, "the four sectors should be irreducible");
    }

    // ── TQMQG681: one complete link ─────────────────────────────────────────────────

    [Fact]
    public void TQMQG681_OneCompleteLink()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG681: the four sectors are components of one complete link");

        bool oneLink = FinalNetworkPrimitive.OneCompleteLink();

        sb.AppendLine($"four sectors expressible as ONE complete link: {oneLink}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a complete link carries magnitude (ρ + ψ), phase (θ), and spin (S) together — a single");
        sb.AppendLine("mathematical object whose decomposition gives the four sectors.");
        Output.WriteLine(sb.ToString());

        Assert.True(oneLink, "the four sectors should be one complete link");
    }

    // ── TQMQG682: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG682_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG682: FOUR PRIMITIVES / TWO PRIMITIVES / ONE NETWORK PRIMITIVE?");

        sb.AppendLine($"CLASSIFICATION: {FinalNetworkPrimitive.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT FOUR PRIMITIVES: the four sectors are components of ONE complete link, not four unrelated objects.");
        sb.AppendLine("  • ONE NETWORK PRIMITIVE: the causal network (V, E) is one primitive; its link carries the four sectors.");
        sb.AppendLine("  • WITH IRREDUCIBLE SECTORS: ρ, ψ, θ, S remain independent d.o.f. — a 'one primitive, four sectors' structure,");
        sb.AppendLine("    the terminal unification of the QG arc (QG55 → QG64 → QG68).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("ONE NETWORK PRIMITIVE", FinalNetworkPrimitive.Classify());
        Assert.True(FinalNetworkPrimitive.OneNetworkPrimitive());
        Assert.True(FinalNetworkPrimitive.OneCompleteLink());
    }
}
