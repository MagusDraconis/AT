using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 56 — origin of Weyl-capable links. Determines why links carry a non-conformal (traceless) DOF.
/// Classify: FORCED / PREFERRED / CONTINGENT.
///
/// Tests: TQMQG560 (rank-2 decomposition), TQMQG561 (link completeness), TQMQG562 (classification).
/// </summary>
public class TQMQG_Phase56_OriginOfWeylLinksTests : ResearchTestBase
{
    public TQMQG_Phase56_OriginOfWeylLinksTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG560: rank-2 link tensor always has a traceless part ────────────────────

    [Fact]
    public void TQMQG560_Rank2Decomposition()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG560: a link tensor decomposes into trace + traceless (Weyl)");

        bool hasTraceless = OriginOfWeylLinks.Rank2HasTracelessPart();
        bool conformalRestriction = OriginOfWeylLinks.ConformalOnlyIsRestriction();

        sb.AppendLine($"a symmetric rank-2 link tensor has a traceless (Weyl) part: {hasTraceless}");
        sb.AppendLine($"conformal-only links (Weyl=0) are a RESTRICTION:         {conformalRestriction}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a link relation A_ij decomposes as trace (scalar/conformal factor) + traceless (spin-2/Weyl).");
        sb.AppendLine("The conformal-only link (trace only) drops the traceless part — it is the restricted, not the general, case.");
        Output.WriteLine(sb.ToString());

        Assert.True(hasTraceless, "a rank-2 link tensor should have a traceless part");
        Assert.True(conformalRestriction, "conformal-only should be a restriction");
    }

    // ── TQMQG561: link completeness forces the Weyl capacity ────────────────────────

    [Fact]
    public void TQMQG561_LinkCompleteness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG561: a complete link carries the full rank-2 relation");

        bool completeCarriesWeyl = OriginOfWeylLinks.CompleteLinkCarriesWeyl();
        bool capacityForced = OriginOfWeylLinks.WeylCapacityForced();

        sb.AppendLine($"a complete link carries the Weyl (traceless) content: {completeCarriesWeyl}");
        sb.AppendLine($"the Weyl CAPACITY is forced by link completeness:      {capacityForced}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a complete link encodes the full relation between two nodes — trace AND traceless. Dropping the");
        sb.AppendLine("traceless part (Weyl = 0) is an incomplete description; link completeness forces the Weyl capacity.");
        Output.WriteLine(sb.ToString());

        Assert.True(completeCarriesWeyl, "a complete link should carry the Weyl content");
        Assert.True(capacityForced, "link completeness should force the Weyl capacity");
    }

    // ── TQMQG562: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG562_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG562: FORCED / PREFERRED / CONTINGENT?");

        bool capacityForced = OriginOfWeylLinks.WeylCapacityForced();
        bool valueContingent = OriginOfWeylLinks.WeylValueContingent();

        sb.AppendLine($"Weyl CAPACITY is FORCED (link completeness): {capacityForced}");
        sb.AppendLine($"Weyl VALUE (ψ≠0) is CONTINGENT (observation): {valueContingent}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {OriginOfWeylLinks.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • FORCED (capacity): a complete link is a full rank-2 relation, which necessarily carries the traceless");
        sb.AppendLine("    (Weyl) degree of freedom; conformal-only (Weyl=0) links are an incomplete restriction, not the default.");
        sb.AppendLine("  • CONTINGENT (value): whether that Weyl degree of freedom is excited (ψ≠0) is set by observation (GWs).");
        sb.AppendLine("  • So the Weyl content is FORCED in its capacity and CONTINGENT in its value — the scalar sector was the");
        sb.AppendLine("    conformally-flat (Weyl=0) restriction, and ψ is the general (complete-link) case.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("FORCED", OriginOfWeylLinks.Classify());
        Assert.True(capacityForced);
        Assert.True(valueContingent);
    }
}
