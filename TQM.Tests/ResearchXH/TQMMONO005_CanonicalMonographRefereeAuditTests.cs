using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-MONO005 — Hostile Referee Audit of the Final Canonical Monograph Structure. Assume submission of
/// the MONO004 17-chapter structure for publication. Search ONLY for theory-architecture defects:
/// logical circularity, dependency violations, hidden assumptions, boundary leakage, and unsupported
/// completeness claims. Ignore style, grammar, and missing citations. Audit only — no new physics.
/// </summary>
public class TQMMONO005_CanonicalMonographRefereeAuditTests : ResearchTestBase
{
    public TQMMONO005_CanonicalMonographRefereeAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMMONO0050_FindingsCatalog()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO0050: the hostile-referee architecture findings");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the audit searches ONLY circularity, dependency, assumption, leakage, and");
        sb.AppendLine("    completeness; style/grammar/citations are ignored;");
        sb.AppendLine("  - each finding has a target chapter and a required correction.");
        sb.AppendLine();

        foreach (var f in CanonicalMonographRefereeAudit.Catalog())
        {
            sb.AppendLine($"  {f.Id} [{f.Area}] {f.Severity} — target: {f.Target}");
            sb.AppendLine($"      {f.Challenge}");
            sb.AppendLine($"      FIX: {f.Correction}");
        }
        sb.AppendLine();
        sb.AppendLine($"CRITICAL: {CanonicalMonographRefereeAudit.CriticalCount()} " +
                      $"MAJOR: {CanonicalMonographRefereeAudit.MajorCount()} " +
                      $"MINOR: {CanonicalMonographRefereeAudit.MinorCount()}");

        Output.WriteLine(sb.ToString());

        Assert.True(CanonicalMonographRefereeAudit.Catalog().Length >= 8, "the audit must find real issues");
        Assert.True(CanonicalMonographRefereeAudit.CriticalCount() >= 1,
            "the primitive-count conflict is a critical dependency violation");
        Assert.True(CanonicalMonographRefereeAudit.MajorCount() >= 3,
            "there must be multiple major issues");
        Assert.True(CanonicalMonographRefereeAudit.AllAreasCovered(),
            "all five required focus areas must be searched");
        Assert.True(CanonicalMonographRefereeAudit.AllFindingsActionable(),
            "every finding must have a target and a correction");
    }

    [Fact]
    public void TQMMONO0051_BlockingFinding()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO0051: the blocking finding — the primitive-count conflict");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the monograph's canonical foundation is {Difference, η};");
        sb.AppendLine("  - the cited QG318(2) architecture classified {Difference, Actualization, η} as");
        sb.AppendLine("    three FOUNDATIONAL primitives;");
        sb.AppendLine("  - the monograph silently demotes Actualization — an undeclared dependency change.");
        sb.AppendLine();

        var a01 = CanonicalMonographRefereeAudit.Catalog().First(f => f.Id == "A01");
        sb.AppendLine($"A01 challenge: {a01.Challenge}");
        sb.AppendLine($"A01 severity: {a01.Severity}");
        sb.AppendLine($"A01 correction: {a01.Correction}");
        sb.AppendLine();
        sb.AppendLine("A primitive cannot be silently reclassified between the architecture phase and the");
        sb.AppendLine("monograph without an explicit canonical decision. This blocks publication.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(CanonicalMonographRefereeAudit.Severity.Critical, a01.Severity);
        Assert.Equal(CanonicalMonographRefereeAudit.Area.Dependency, a01.Area);
        Assert.Contains("Actualization", a01.Challenge);
    }

    [Fact]
    public void TQMMONO0052_Verdict()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO0052: the publication verdict");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - a CRITICAL finding blocks publication;");
        sb.AppendLine("  - the required corrections are documentation-level only — no physics changes.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {CanonicalMonographRefereeAudit.Summary()}");
        sb.AppendLine($"Audit score: {CanonicalMonographRefereeAudit.AuditScore()}/6");
        sb.AppendLine($"VERDICT = {CanonicalMonographRefereeAudit.Verdict()}");
        sb.AppendLine();
        sb.AppendLine("REQUIRED CORRECTIONS BEFORE ZENODO:");
        foreach (var f in CanonicalMonographRefereeAudit.Catalog())
        {
            sb.AppendLine($"  {f.Id}: {f.Correction}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal("FAIL", CanonicalMonographRefereeAudit.Verdict());
        Assert.True(CanonicalMonographRefereeAudit.AuditScore() >= 5);
        Assert.Equal(1, CanonicalMonographRefereeAudit.CriticalCount());
        Assert.True(CanonicalMonographRefereeAudit.CircularityContained(),
            "the circularity finding must be reclassification-level, not blocking");
    }
}
