using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-MONO001 — Quantum Gravity Monograph Assembly. Assemble the complete monograph structure from
/// QG0-QG225: 18 chapters, each mapped to its source QG phases. Assembly only — no new physics.
/// </summary>
public class ATMONO001_MonographAssemblyTests : ResearchTestBase
{
    public ATMONO001_MonographAssemblyTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATMONO0010_FullChapterOutline()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0010: the complete 18-chapter monograph outline with source QG phases");

        var (title, subtitle) = MonographAssembly.MonographTitle();
        sb.AppendLine($"TITLE: {title}");
        sb.AppendLine($"SUBTITLE: {subtitle}");
        sb.AppendLine();
        sb.AppendLine("CHAPTER OUTLINE (with source QG phases):");
        foreach (var c in MonographAssembly.Chapters())
        {
            sb.AppendLine($"  {c.Index:00}. {c.Title} — {c.Scope}");
            sb.AppendLine($"      sources: {string.Join(", ", c.SourcePhases)}");
        }
        sb.AppendLine();

        sb.AppendLine("STRUCTURE CHECKS:");
        sb.AppendLine($"  Chapters: {MonographAssembly.ChapterCount()}");
        sb.AppendLine($"  Sequential 1..18? {MonographAssembly.ChaptersSequential()}");
        sb.AppendLine($"  All chapters have sources? {MonographAssembly.AllChaptersHaveSources()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(18, MonographAssembly.ChapterCount());
        Assert.True(MonographAssembly.ChaptersSequential(), "chapters must be numbered 1..18");
        Assert.True(MonographAssembly.AllChaptersHaveSources(), "every chapter must map to source phases");
    }

    [Fact]
    public void ATMONO0011_SourceCoverage()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0011: source-phase coverage across the 18 chapters");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The register spans QG0-QG225 (226 phases); the monograph references them by chapter.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Distinct phases referenced: {MonographAssembly.DistinctPhaseCount()}");
        sb.AppendLine($"  Total phase references (with repeats): {MonographAssembly.TotalPhaseReferences()}");
        sb.AppendLine($"  Register coverage fraction: {MonographAssembly.RegisterCoverageFraction():P1}");

        sb.AppendLine();
        sb.AppendLine("PER-CHAPTER SOURCE COUNTS:");
        foreach (var c in MonographAssembly.Chapters())
            sb.AppendLine($"  Ch {c.Index:00} {c.Title}: {c.SourcePhases.Length} source phases");

        sb.AppendLine();
        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The monograph references most of the register; support/audit phases are folded into");
        sb.AppendLine("    the validation chapters (11) and the discussion/limitations chapters (16/17).");

        Output.WriteLine(sb.ToString());

        Assert.True(MonographAssembly.DistinctPhaseCount() >= 150, "the monograph must reference a large fraction of the register");
        Assert.True(MonographAssembly.RegisterCoverageFraction() >= 0.66, "at least 2/3 of the register covered by chapters");
    }

    [Fact]
    public void ATMONO0012_ClosureChapters()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0012: the closure/validation chapters carry the QG completion phases");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The monograph must place the QM origin phases (216/218/220), the gravity phases");
        sb.AppendLine("    (222), the QG closure (215/219/221/223), and the readiness verdicts (224/225).");
        sb.AppendLine();

        var all = MonographAssembly.Chapters().SelectMany(c => c.SourcePhases).ToHashSet();
        sb.AppendLine("REQUIRED PHASES PRESENT IN THE OUTLINE:");
        foreach (string p in new[] { "QG216", "QG218", "QG220", "QG222", "QG215", "QG219", "QG221", "QG223", "QG224", "QG225" })
            sb.AppendLine($"  {p}: {(all.Contains(p) ? "YES" : "NO")}");

        sb.AppendLine();
        sb.AppendLine("READING ORDER:");
        foreach (var r in MonographAssembly.ReadingOrder())
            sb.AppendLine($"  {r}");

        Output.WriteLine(sb.ToString());

        foreach (string p in new[] { "QG216", "QG218", "QG220", "QG222", "QG215", "QG219", "QG221", "QG223", "QG224", "QG225" })
            Assert.True(all.Contains(p), $"the monograph must include {p}");
    }
}
