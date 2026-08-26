using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-MONO004 — Final Canonical Monograph Structure. Assembly of the publication-grade Zenodo monograph
/// from the CANONICAL END-STATE of AT [post-QG319]: six parts, 17 chapters, every chapter classified
/// [Derived / Emergent / Boundary] and mapped to mandatory QG phases. Consolidation over exploration;
/// no new primitives, no new physics, no speculation. Deterministic.
/// </summary>
public class ATMONO004_CanonicalMonographTests : ResearchTestBase
{
    public ATMONO004_CanonicalMonographTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATMONO0040_ChapterStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0040: the six-part, 17-chapter canonical structure");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the canonical structure separates Foundation / Derived Dynamics / Spectrum /");
        sb.AppendLine("    Physics / Universality Program / Boundary Layer;");
        sb.AppendLine("  - every chapter is classified Derived / Emergent / Boundary and maps to mandatory");
        sb.AppendLine("    source QG phases;");
        sb.AppendLine("  - consolidation only — no new primitives, no new physics.");
        sb.AppendLine();

        var (title, subtitle) = CanonicalMonograph.MonographTitle();
        sb.AppendLine($"TITLE: {title}");
        sb.AppendLine($"SUBTITLE: {subtitle}");
        sb.AppendLine();
        sb.AppendLine("CHAPTERS:");
        foreach (var c in CanonicalMonograph.Chapters())
        {
            sb.AppendLine($"  {c.Index:00}. [{c.Part}] ({c.Kind}) {c.Title}");
            sb.AppendLine($"      sources: {string.Join(", ", c.Sources)}");
        }
        sb.AppendLine();
        sb.AppendLine($"chapters: {CanonicalMonograph.ChapterCount()} sequential: {CanonicalMonograph.ChaptersSequential()}");
        sb.AppendLine($"all parts populated: {CanonicalMonograph.AllPartsPopulated()} parts in order: {CanonicalMonograph.PartsInOrder()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(17, CanonicalMonograph.ChapterCount());
        Assert.True(CanonicalMonograph.ChaptersSequential(), "chapters must be numbered 1..17");
        Assert.True(CanonicalMonograph.AllChaptersHaveSources(), "every chapter must list mandatory source phases");
        Assert.True(CanonicalMonograph.AllPartsPopulated(), "all six parts must be populated");
        Assert.True(CanonicalMonograph.PartsInOrder(), "the six parts must appear in canonical order");
        Assert.True(CanonicalMonograph.AllClassified(), "every chapter must be classified");
    }

    [Fact]
    public void ATMONO0041_CanonicalCoreAndOperators()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0041: the canonical core, the primitives, and the universal operators");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the canonical foundation is {Difference, η};");
        sb.AppendLine("  - the canonical core is Difference → Actualization → Inevitable Spectrum → Physics;");
        sb.AppendLine("  - the universal operators are exactly {Crowding, Compression, Beat, Locking} — no fifth.");
        sb.AppendLine();

        sb.AppendLine($"primitives: {string.Join(", ", CanonicalMonograph.Primitives)}");
        sb.AppendLine($"canonical core: {string.Join(" → ", CanonicalMonograph.CanonicalCore)}");
        sb.AppendLine($"operators ({CanonicalMonograph.Operators.Length}): {string.Join(", ", CanonicalMonograph.Operators)}");
        sb.AppendLine($"canonical core present: {CanonicalMonograph.CanonicalCorePresent()}");
        sb.AppendLine($"operators present: {CanonicalMonograph.OperatorsPresent()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, CanonicalMonograph.Primitives.Length);
        Assert.Equal(4, CanonicalMonograph.Operators.Length);
        Assert.Equal("Difference", CanonicalMonograph.Primitives[0]);
        Assert.Equal("η", CanonicalMonograph.Primitives[1]);
        Assert.True(CanonicalMonograph.CanonicalCorePresent(),
            "the canonical core chain must be present");
        Assert.True(CanonicalMonograph.OperatorsPresent(),
            "all four universal operators must be named");
        Assert.DoesNotContain("Fifth", string.Join(" ", CanonicalMonograph.Operators));
    }

    [Fact]
    public void ATMONO0042_BoundaryLayerAndDetermination()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0042: the boundary layer, the frontier, and the monograph determination");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the boundary topics are {Difference itself, ψ ontological status};");
        sb.AppendLine("  - the current frontier is independent temporal evidence, the Bekenstein 2π boundary,");
        sb.AppendLine("    and experimental validation — NOT an open derivation frontier;");
        sb.AppendLine("  - four internal inconsistencies [v1.0 vs canonical] are flagged, not hidden.");
        sb.AppendLine();

        sb.AppendLine($"boundary topics: {string.Join(", ", CanonicalMonograph.BoundaryTopics)}");
        sb.AppendLine($"frontier: {string.Join("; ", CanonicalMonograph.Frontier)}");
        sb.AppendLine($"dependency graph acyclic: {CanonicalMonograph.DependencyGraphAcyclic()}");
        sb.AppendLine($"monograph score: {CanonicalMonograph.MonographScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {CanonicalMonograph.Classify()}");
        sb.AppendLine();
        sb.AppendLine($"SUMMARY: {CanonicalMonograph.Summary()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, CanonicalMonograph.BoundaryTopics.Length);
        Assert.Equal(3, CanonicalMonograph.Frontier.Length);
        Assert.True(CanonicalMonograph.DependencyGraphAcyclic(),
            "the dependency graph must be acyclic and topological");
        Assert.Equal("FINAL CANONICAL MONOGRAPH", CanonicalMonograph.Classify());
        Assert.True(CanonicalMonograph.MonographScore() >= 6);
    }
}
