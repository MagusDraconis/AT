using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 72 — complete quantum sector audit. Audits six quantum features with θ + S + J.
/// Classify: COMPLETE / PARTIAL / MISSING.
///
/// Tests: ATQG720 (feature census), ATQG721 (the missing collapse), ATQG722 (overall classification).
/// </summary>
public class ATQG_Phase72_QuantumSectorAuditTests : ResearchTestBase
{
    public ATQG_Phase72_QuantumSectorAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG720: feature census ────────────────────────────────────────────────────

    [Fact]
    public void ATQG720_FeatureCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG720: audit six quantum features with θ + S + J");

        int complete = 0, partial = 0, missing = 0;
        foreach (var f in QuantumSectorAudit.Features)
        {
            string c = QuantumSectorAudit.Classify(f);
            sb.AppendLine($"{f,-18} -> {c}");
            switch (c)
            {
                case "COMPLETE": complete++; break;
                case "PARTIAL": partial++; break;
                case "MISSING": missing++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"COMPLETE : {complete}");
        sb.AppendLine($"PARTIAL  : {partial}");
        sb.AppendLine($"MISSING  : {missing}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, complete);
        Assert.Equal(1, partial);
        Assert.Equal(0, missing);
    }

    // ── ATQG721: the missing collapse ──────────────────────────────────────────────

    [Fact]
    public void ATQG721_MissingCollapse()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG721: the one missing piece is the measurement collapse");

        bool bornRule = QuantumSectorAudit.BornRulePresent();
        bool collapse = QuantumSectorAudit.CollapseNative();

        sb.AppendLine($"Born rule (P = |amplitude|²) is present: {bornRule}  (QG65)");
        sb.AppendLine($"the COLLAPSE (state projection) is native: {collapse}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: measurement is HALF-present — the statistical (Born-rule) part is recovered, but the dynamical");
        sb.AppendLine("collapse (projection onto an eigenstate) has no native mechanism. That is the open measurement problem.");
        Output.WriteLine(sb.ToString());

        Assert.True(bornRule, "the Born rule should be present");
        Assert.False(collapse, "the collapse should not be native");
    }

    // ── ATQG722: overall classification ─────────────────────────────────────────────

    [Fact]
    public void ATQG722_Overall()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG722: is the full quantum structure present?");

        sb.AppendLine($"OVERALL: {QuantumSectorAudit.Overall()}");
        sb.AppendLine();
        sb.AppendLine("  • COMPLETE (5/6): superposition, interference, Born rule, entanglement, Bell correlations — all recovered");
        sb.AppendLine("    from θ (phase), S (spin), and J (joint state).");
        sb.AppendLine("  • PARTIAL (1/6): measurement — the Born rule is present, but the collapse is not.");
        sb.AppendLine("  • STILL MISSING: a native collapse (projection) mechanism — the quantum measurement problem.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: with θ + S + J, the quantum sector is ALMOST complete; the single remaining gap is the");
        sb.AppendLine("measurement collapse, the same open problem at the heart of quantum foundations.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL", QuantumSectorAudit.Overall());
        Assert.Equal("COMPLETE", QuantumSectorAudit.Classify("entanglement"));
        Assert.Equal("PARTIAL", QuantumSectorAudit.Classify("measurement"));
    }
}
