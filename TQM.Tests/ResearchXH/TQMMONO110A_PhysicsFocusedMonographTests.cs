using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-MONO110A — Physics-Focused Monograph Re-audit. Create a physics-focused publication: keep
/// Chapters 1-12 unchanged, remove the Universality Program from the core, retain only boundary and
/// experimental validation chapters, and treat Universality as future work. Assembly only — no new
/// physics.
/// </summary>
public class TQMMONO110A_PhysicsFocusedMonographTests : ResearchTestBase
{
    public TQMMONO110A_PhysicsFocusedMonographTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMMONO110A0_RevisedStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO110A0: the revised physics-focused chapter structure");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Chapters 1-12 [physics derivation] are kept unchanged;");
        sb.AppendLine("  - the Universality Program is removed from the core;");
        sb.AppendLine("  - only boundary and validation chapters are retained;");
        sb.AppendLine("  - Universality is treated as future work.");
        sb.AppendLine();

        foreach (var c in PhysicsFocusedMonographAudit.Chapters())
        {
            sb.AppendLine($"  {c.Index:00}. [{c.Part}] {c.Title} — ~{c.EstimatedPages} pp [{c.Status}]");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(15, PhysicsFocusedMonographAudit.Chapters().Length);
        Assert.True(PhysicsFocusedMonographAudit.PhysicsChaptersUnchanged(),
            "the physics chapters [Ch1-12] must be kept unchanged");
        Assert.True(PhysicsFocusedMonographAudit.UniversalityRemovedFromCore(),
            "the Universality Program must be removed from the core");
    }

    [Fact]
    public void TQMMONO110A1_UniversalityAsFutureWork()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO110A1: Universality as future work, boundary/validation retained");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the Universality Program [cross-domain, organization/prediction, validation]");
        sb.AppendLine("    is moved to an appendix as future work;");
        sb.AppendLine("  - the boundary and experimental-validation chapters are retained.");
        sb.AppendLine();

        sb.AppendLine($"universality removed from core: {PhysicsFocusedMonographAudit.UniversalityRemovedFromCore()}");
        sb.AppendLine($"boundary/validation retained: {PhysicsFocusedMonographAudit.BoundaryValidationRetained()}");
        sb.AppendLine($"universality is future work: {PhysicsFocusedMonographAudit.UniversalityIsFutureWork()}");
        sb.AppendLine();
        var appendix = PhysicsFocusedMonographAudit.Chapters().First(c => c.Part == PhysicsFocusedMonographAudit.Part.Appendix);
        sb.AppendLine($"appendix: {appendix.Title} [{appendix.Status}]");
        sb.AppendLine($"  sources: {string.Join(", ", appendix.Sources)}");

        Output.WriteLine(sb.ToString());

        Assert.True(PhysicsFocusedMonographAudit.BoundaryValidationRetained(),
            "the boundary and validation chapters must be retained");
        Assert.True(PhysicsFocusedMonographAudit.UniversalityIsFutureWork(),
            "Universality must be treated as future work in an appendix");
        Assert.Equal("future work", PhysicsFocusedMonographAudit.Chapters()
            .First(c => c.Part == PhysicsFocusedMonographAudit.Part.Appendix).Status);
    }

    [Fact]
    public void TQMMONO110A2_PageEstimateAndDetermination()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO110A2: the page estimate and the determination");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the physics chapters [Ch1-12] contribute the physics page count;");
        sb.AppendLine("  - the boundary/validation chapters and the appendix add their pages;");
        sb.AppendLine("  - ~10 pages of front matter [title, TOC, preface] are additional.");
        sb.AppendLine();

        sb.AppendLine($"physics pages [Ch1-12]: {PhysicsFocusedMonographAudit.PhysicsPages()}");
        sb.AppendLine($"boundary/validation pages [Ch13-14]: {PhysicsFocusedMonographAudit.BoundaryPages()}");
        sb.AppendLine($"appendix pages [Ch15, universality future work]: {PhysicsFocusedMonographAudit.AppendixPages()}");
        sb.AppendLine($"total estimated: ~{PhysicsFocusedMonographAudit.TotalPages()} pages + ~10 front matter");
        sb.AppendLine();
        sb.AppendLine($"structure score: {PhysicsFocusedMonographAudit.StructureScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {PhysicsFocusedMonographAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine($"SUMMARY: {PhysicsFocusedMonographAudit.Summary()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PHYSICS-FOCUSED PUBLICATION-READY", PhysicsFocusedMonographAudit.Classify());
        Assert.True(PhysicsFocusedMonographAudit.StructureScore() >= 6);
        Assert.InRange(PhysicsFocusedMonographAudit.TotalPages(), 100, 200);
        Assert.True(PhysicsFocusedMonographAudit.PhysicsPages() > PhysicsFocusedMonographAudit.AppendixPages(),
            "the physics content must dominate the appendix");
    }
}
