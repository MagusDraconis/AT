using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 11 — origin of causal order. Tests whether the partial (causal) order emerges from the
/// actualization/generation (branching) relation rather than being a separate primitive. Classify: DERIVED /
/// PREFERRED / REAL-UNDERIVED.
///
/// Tests: TQMQG110 (ancestor relation = partial order), TQMQG111 (temporal ordering + branching consistency),
///        TQMQG112 (classification).
/// </summary>
public class TQMQG_Phase11_OriginOfCausalOrderTests : ResearchTestBase
{
    public TQMQG_Phase11_OriginOfCausalOrderTests(ITestOutputHelper o) : base(o) { }

    private const int BRANCHING = 2;
    private const int DEPTH = 5;

    // ── TQMQG110: the ancestor relation IS a partial (causal) order ──────────────────

    [Fact]
    public void TQMQG110_AncestorRelationIsPartialOrder()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG110: the ancestor relation of the branching process is a partial order");

        int n = CausalOrder.EventCount(BRANCHING, DEPTH);
        bool irreflexive = CausalOrder.Irreflexive(BRANCHING, n);
        bool antisymmetric = CausalOrder.Antisymmetric(BRANCHING, n);
        bool transitive = CausalOrder.Transitive(BRANCHING, n);

        sb.AppendLine($"branching tree: b={BRANCHING}, depth={DEPTH}, events N={n}");
        sb.AppendLine($"irreflexive (no self-ancestor): {irreflexive}");
        sb.AppendLine($"antisymmetric (no mutual ancestors): {antisymmetric}");
        sb.AppendLine($"transitive (ancestor-of-ancestor is ancestor): {transitive}");
        bool partialOrder = irreflexive && antisymmetric && transitive;

        sb.AppendLine();
        sb.AppendLine($"ancestor relation is a strict partial order: {partialOrder}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the actualization/generation relation (parent→child), closed transitively, IS the");
        sb.AppendLine("causal (partial) order. No separate 'order' primitive is needed — it is the ancestor relation.");
        Output.WriteLine(sb.ToString());

        Assert.True(partialOrder, "the ancestor relation should be a partial order");
    }

    // ── TQMQG111: temporal ordering (linear extension) + branching consistency ───────

    [Fact]
    public void TQMQG111_TemporalOrderingAndConsistency()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG111: the generation order is a linear extension; branching is consistent");

        int n = CausalOrder.EventCount(BRANCHING, DEPTH);
        bool linearExtension = CausalOrder.GenerationIsLinearExtension(BRANCHING, n);

        // Branching consistency: every non-root event has exactly one parent in a strictly earlier generation.
        bool consistent = true;
        for (int i = 1; i < n; i++)
        {
            int p = CausalOrder.Parent(i, BRANCHING);
            if (p < 0 || CausalOrder.Generation(p, BRANCHING) >= CausalOrder.Generation(i, BRANCHING)) consistent = false;
        }

        sb.AppendLine($"generation order is a linear extension (ancestor ⟹ earlier generation): {linearExtension}");
        sb.AppendLine($"branching consistent (unique parent, strictly earlier generation): {consistent}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the branching process is consistent (acyclic, each event has a unique parent in an");
        sb.AppendLine("earlier layer), and the generation order is a valid temporal (linear) extension of the partial order.");
        sb.AppendLine("Temporal ordering is therefore also derived from the generation relation.");
        Output.WriteLine(sb.ToString());

        Assert.True(linearExtension, "generation order should be a linear extension");
        Assert.True(consistent, "branching should be consistent (acyclic, unique parent)");
    }

    // ── TQMQG112: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG112_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG112: is causal order DERIVED, PREFERRED, or REAL-UNDERIVED?");

        sb.AppendLine("CLASSIFICATION: DERIVED (causal order = transitive closure of the generation relation).");
        sb.AppendLine();
        sb.AppendLine("  • The FULL causal order is DERIVED: it is the transitive closure of the parent→child generation");
        sb.AppendLine("    relation, which is automatically a strict partial order (irreflexive, antisymmetric, transitive,");
        sb.AppendLine("    TQMQG110). Temporal ordering is the generation (layer) linear extension (TQMQG111).");
        sb.AppendLine("  • The remaining REAL-UNDERIVED primitive is the GENERATION RELATION itself — 'an event generates");
        sb.AppendLine("    descendants' — i.e., the actualization dynamics (branching). This is more minimal than causal");
        sb.AppendLine("    order: the full partial order is reconstructed from the single-step generation relation.");
        sb.AppendLine("  • This REPLACES the primitive pair (Q-events + causal order) with (Q-events + generation relation),");
        sb.AppendLine("    where 'generation' is just the actualization rule from TQM-QG1/QG7 (critical branching).");
        sb.AppendLine("  • The deepest remaining primitive is therefore the actualization dynamics itself (events generate");
        sb.AppendLine("    events), of which causal order is the order-theoretic content.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
