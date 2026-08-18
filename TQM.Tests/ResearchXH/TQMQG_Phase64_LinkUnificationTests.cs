using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 64 — unify link content. Determines whether trace/traceless/phase are one link object.
/// Classify: SEPARATE / PARTIAL UNIFICATION / UNIFIED.
///
/// Tests: TQMQG640 (three sectors), TQMQG641 (one complex link), TQMQG642 (classification).
/// </summary>
public class TQMQG_Phase64_LinkUnificationTests : ResearchTestBase
{
    public TQMQG_Phase64_LinkUnificationTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG640: the three sectors ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG640_ThreeSectors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG640: trace, traceless, and phase are three irreducible sectors");

        foreach (var s in LinkUnification.Sectors)
            sb.AppendLine($"{s,-10} -> {LinkUnification.Kind(s)}");

        bool independent = LinkUnification.SectorsIndependent();

        sb.AppendLine();
        sb.AppendLine($"three sectors are INDEPENDENT d.o.f.: {independent}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: ρ (spin-0 magnitude), ψ (spin-2 shape), and θ (U(1) phase) are three different representations");
        sb.AppendLine("— independent degrees of freedom that can each vary separately.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, LinkUnification.Sectors.Length);
        Assert.True(independent, "the three sectors should be independent");
    }

    // ── TQMQG641: one complex link object ────────────────────────────────────────────

    [Fact]
    public void TQMQG641_OneComplexLink()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG641: the complete link is a single complex rank-2 object");

        bool oneObject = LinkUnification.ExpressibleAsOneObject();
        bool singleStructure = LinkUnification.CompleteLinkSingleStructure();

        sb.AppendLine($"three sectors expressible as ONE link object: {oneObject}");
        sb.AppendLine($"complete link is a SINGLE structure:           {singleStructure}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: L_ij = a_ij · e^(iθ_ij) — magnitude a_ij (trace ρ + traceless ψ) times phase θ (U(1)). The");
        sb.AppendLine("three sectors are components of ONE complex rank-2 link object.");
        Output.WriteLine(sb.ToString());

        Assert.True(oneObject, "the three sectors should be expressible as one object");
        Assert.True(singleStructure, "the complete link should be a single structure");
    }

    // ── TQMQG642: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG642_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG642: SEPARATE / PARTIAL UNIFICATION / UNIFIED?");

        sb.AppendLine($"CLASSIFICATION: {LinkUnification.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT SEPARATE: the three sectors are components of one complex rank-2 link (L_ij = a_ij e^{iθ_ij}), not");
        sb.AppendLine("    three unrelated objects.");
        sb.AppendLine("  • UNIFIED: the complete link is a SINGLE object whose decomposition gives ρ (trace), ψ (traceless), and θ");
        sb.AppendLine("    (phase) — exactly as the network primitive unified nodes + links (QG55).");
        sb.AppendLine("  • WITH IRREDUCIBLE SECTORS: the three remain independent d.o.f. (spin-0 / spin-2 / U(1)), a 'unified with");
        sb.AppendLine("    irreducible interior' structure.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIFIED", LinkUnification.Classify());
        Assert.True(LinkUnification.ExpressibleAsOneObject());
        Assert.True(LinkUnification.SectorsIndependent());
    }
}
