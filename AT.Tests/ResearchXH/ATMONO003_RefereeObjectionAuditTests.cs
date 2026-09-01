using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-MONO003 — Referee Objection Audit. Assume a hostile referee reviewing QG0-QG225; catalog the
/// strongest 50 objections, classify FATAL/MAJOR/MINOR/EDITORIAL, and record resolution. Audit only.
/// </summary>
public class ATMONO003_RefereeObjectionAuditTests : ResearchTestBase
{
    public ATMONO003_RefereeObjectionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATMONO0030_FiftyObjectionsCatalogued()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0030: the Top-50 referee objections are catalogued and classified");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A hostile referee reviews QG0-QG225; objections span five focus areas.");
        sb.AppendLine("  - Each objection is classified FATAL/MAJOR/MINOR/EDITORIAL with a resolution status.");
        sb.AppendLine();

        var cat = RefereeObjectionAudit.Catalog();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Total objections: {cat.Length}");
        sb.AppendLine($"  By area: {string.Join(", ", RefereeObjectionAudit.AreaCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine($"  By severity: {string.Join(", ", RefereeObjectionAudit.SeverityCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine($"  By resolution: {string.Join(", ", RefereeObjectionAudit.ResolutionCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine();
        sb.AppendLine("  Objection list (first 10):");
        foreach (var o in cat.Take(10))
            sb.AppendLine($"    {o.Id} [{o.Area}/{o.Severity}/{o.Resolved}] {o.Challenge}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(50, cat.Length);
        Assert.All(cat, o => Assert.False(string.IsNullOrWhiteSpace(o.Challenge)));
        Assert.Equal(5, RefereeObjectionAudit.AreaCounts().Count);
        Assert.Equal(4, RefereeObjectionAudit.SeverityCounts().Count);
    }

    [Fact]
    public void ATMONO0031_StrongestObjections()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0031: the strongest objections (FATAL and MAJOR) and their resolutions");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The strongest objections a hostile referee raises are the FATAL and MAJOR items.");
        sb.AppendLine("  - Whether they are resolved or boundaries determines the audit verdict.");
        sb.AppendLine();

        var strong = RefereeObjectionAudit.Catalog()
            .Where(o => o.Severity == RefereeObjectionAudit.Severity.Fatal
                     || o.Severity == RefereeObjectionAudit.Severity.Major)
            .ToArray();
        sb.AppendLine($"FATAL + MAJOR objections: {strong.Length}");
        foreach (var o in strong)
            sb.AppendLine($"  {o.Id} [{o.Area}] {o.Severity} → {o.Resolved}: {o.Challenge}");
        sb.AppendLine();

        sb.AppendLine($"Open FATAL objections: {RefereeObjectionAudit.OpenFatalCount()}");
        sb.AppendLine($"Open (any severity): {RefereeObjectionAudit.OpenCount()}");
        sb.AppendLine($"Partial: {RefereeObjectionAudit.PartialCount()}");
        sb.AppendLine($"Closed (resolved + boundary): {RefereeObjectionAudit.ClosedCount()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The strongest objections (ψ imported, BDG dynamics, Bekenstein 1/4, cosmology) are");
        sb.AppendLine("    either RESOLVED (BDG → QG222) or stated BOUNDARIES (ψ, Bekenstein, cosmology).");
        sb.AppendLine("  - No FATAL objection remains open.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(0, RefereeObjectionAudit.OpenFatalCount()); // no fatal objection may remain open
        Assert.True(strong.Length >= 10, "a hostile referee must find at least 10 strong objections");
    }

    [Fact]
    public void ATMONO0032_Verdict()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0032: the hostile-referee verdict");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The verdict reflects the distribution of resolutions across all 50 objections.");
        sb.AppendLine();

        var counts = RefereeObjectionAudit.ResolutionCounts();
        string verdict = RefereeObjectionAudit.Verdict();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  RESOLVED: {counts.GetValueOrDefault(RefereeObjectionAudit.Resolution.Resolved)}");
        sb.AppendLine($"  BOUNDARY: {counts.GetValueOrDefault(RefereeObjectionAudit.Resolution.Boundary)}");
        sb.AppendLine($"  PARTIAL:  {counts.GetValueOrDefault(RefereeObjectionAudit.Resolution.Partial)}");
        sb.AppendLine($"  OPEN:     {counts.GetValueOrDefault(RefereeObjectionAudit.Resolution.Open)}");
        sb.AppendLine($"  Closed (resolved+boundary): {RefereeObjectionAudit.ClosedCount()}");
        sb.AppendLine($"  Partial: {RefereeObjectionAudit.PartialCount()}");
        sb.AppendLine($"  Open: {RefereeObjectionAudit.OpenCount()}");
        sb.AppendLine($"  VERDICT = {verdict}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The majority of the 50 objections are RESOLVED or BOUNDARY (closed); the remainder are");
        sb.AppendLine("    PARTIAL (documented gaps and experiment-ahead-of-data predictions) — none OPEN/fatal.");
        sb.AppendLine("  - The genuine partial items: the ψ primitive's existence (a stated boundary), cosmology");
        sb.AppendLine("    (out of scope), P1/P2 falsification reach (awaiting HL-LHC / nEXO-LEGEND), and a few");
        sb.AppendLine("    documentation-transparency items (O35 tolerance derivation, O22 branching distribution).");

        Output.WriteLine(sb.ToString());

        Assert.StartsWith("STRONG", verdict);
        Assert.True(RefereeObjectionAudit.ClosedCount() >= 36, "the majority of objections must be closed");
        Assert.Equal(0, RefereeObjectionAudit.OpenCount()); // essentially no objection is left open
    }
}
