using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 318 (reissue 2) — Final Theory Architecture. Review QG223-QG317 and produce the CANONICAL
/// MINIMAL ARCHITECTURE of AT: 4 layers [primitive, dynamic, spectrum, physics], every concept
/// classified [FOUNDATIONAL, DERIVED, EMERGENT, BOUNDARY], and an acyclic dependency graph. No
/// observables, no target values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase318_FinalTheoryArchitectureTests : ResearchTestBase
{
    public ATQG_Phase318_FinalTheoryArchitectureTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3180_TheFourLayers()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3180: the four-layer canonical architecture");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Layer 1 primitive: Difference, η — irreducible (plus π boundary); Actualization");
        sb.AppendLine("    is DERIVED from Difference (MONO006/A01 correction);");
        sb.AppendLine("  - Layer 2 dynamic: Resonance → attractor — derived from primitives;");
        sb.AppendLine("  - Layer 3 spectrum: D96 spectrum → operators → locks — emergent;");
        sb.AppendLine("  - Layer 4 physics: fermions, gauge, gravity, cosmology — emergent; SM dynamics and");
        sb.AppendLine("    the open questions — boundary.");
        sb.AppendLine();

        foreach (var layer in Enum.GetValues<FinalTheoryArchitecture.Layer>())
        {
            sb.AppendLine($"  {layer}:");
            foreach (var c in FinalTheoryArchitecture.Concepts().Where(c => c.Layer == layer))
            {
                sb.AppendLine($"    {c.Name.PadRight(24)} {c.Kind.ToString().PadRight(11)} — {c.Note}");
            }
        }

        Output.WriteLine(sb.ToString());

        Assert.True(FinalTheoryArchitecture.AllLayersPopulated(),
            "all four layers must contain at least one concept");
        Assert.True(FinalTheoryArchitecture.AllClassified(),
            "every concept must be classified in exactly one category");
        Assert.True(FinalTheoryArchitecture.PrimitivesIrreducible(),
            "the primitive layer must be Foundational or Boundary only");
        Assert.Equal(2, FinalTheoryArchitecture.Concepts().Count(
            c => c.Layer == FinalTheoryArchitecture.Layer.Primitive && c.Kind == FinalTheoryArchitecture.ConceptKind.Foundational));
    }

    [Fact]
    public void ATQG3181_TheDependencyGraphIsAcyclic()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3181: the dependency graph is acyclic and topological");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Kahn's topological sort orders every concept [no cycles];");
        sb.AppendLine("  - no concept depends on a concept in a higher layer [the layering is topological];");
        sb.AppendLine("  - the primitives are independent [no primitive depends on another].");
        sb.AppendLine();

        var order = FinalTheoryArchitecture.TopologicalSort();
        sb.AppendLine($"acyclic: {FinalTheoryArchitecture.IsAcyclic()}");
        if (order is not null) sb.AppendLine($"topological order ({order.Length} concepts): {string.Join(" → ", order)}");
        sb.AppendLine($"layers topological: {FinalTheoryArchitecture.LayersTopological()}");

        Output.WriteLine(sb.ToString());

        Assert.True(FinalTheoryArchitecture.IsAcyclic(), "the dependency graph must be acyclic");
        Assert.NotNull(FinalTheoryArchitecture.TopologicalSort());
        Assert.Equal(FinalTheoryArchitecture.Concepts().Length, FinalTheoryArchitecture.TopologicalSort()!.Length);
        Assert.True(FinalTheoryArchitecture.LayersTopological(),
            "no concept may depend on a concept in a higher layer");
    }

    [Fact]
    public void ATQG3182_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3182: the final architecture determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - FINAL AT ARCHITECTURE: the canonical minimal architecture is complete and");
        sb.AppendLine("    sound — 4 layers, acyclic graph, irreducible independent primitives.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FinalTheoryArchitecture.Summary()}");
        sb.AppendLine($"Architecture score: {FinalTheoryArchitecture.ArchitectureScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {FinalTheoryArchitecture.Classify()}");
        sb.AppendLine();
        sb.AppendLine("DEPENDENCY GRAPH (canonical):");
        sb.AppendLine("  Difference, η [primitives] → Actualization [derived from Difference]");
        sb.AppendLine("    → Resonance = Conservation + Boundary → Self-Consistency → Individuation");
        sb.AppendLine("    → Difference Principle → Actualization Attractor → Spectrum Necessity");
        sb.AppendLine("    → D96 Spectrum → Operator Basis → Lock Identities → Organization Maturity");
        sb.AppendLine("    → Spectrum → Physics → Fermion/Gauge/Gravity/Cosmology [emergent]");
        sb.AppendLine("    → SM Dynamics, Bekenstein 1/4, ψ, Experimental Frontier [boundary]");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the minimal base is Difference → Actualization → Spectrum → Physics;");
        sb.AppendLine("  - the primitives [Difference, η] are irreducible and independent; Actualization is");
        sb.AppendLine("    derived from Difference (MONO006/A01 correction);");
        sb.AppendLine("  - the dynamics are DERIVED, the spectrum and physics are EMERGENT, and the");
        sb.AppendLine("    remaining open questions are BOUNDARY.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FINAL AT ARCHITECTURE", FinalTheoryArchitecture.Classify());
        Assert.Equal(6, FinalTheoryArchitecture.ArchitectureScore());
        Assert.Contains("FINAL AT ARCHITECTURE", FinalTheoryArchitecture.Summary());
    }
}
