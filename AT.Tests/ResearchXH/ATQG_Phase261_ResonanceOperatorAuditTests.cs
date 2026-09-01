using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 261 — Resonance Operator Audit. Determine whether the five named D96 quantities
/// (Σm, span, λ₂, occMom, Σ√m) are projections of deeper resonance operators.
/// </summary>
public class ATQG_Phase261_ResonanceOperatorAuditTests : ResearchTestBase
{
    public ATQG_Phase261_ResonanceOperatorAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2610_OperatorSet()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2610: the candidate operator set");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - six candidate resonance operators (crowding, compression, beat, locking,");
        sb.AppendLine("    moment, synchronization) applied to the single D96 spectrum;");
        sb.AppendLine("  - hypothesis: the five named quantities are their projections, not primitives.");
        sb.AppendLine();

        foreach (var op in ResonanceOperatorAudit.Operators())
            sb.AppendLine($"  [{op.Kind,-15}] {op.Name}: {op.Definition}");
        sb.AppendLine();
        sb.AppendLine("PROJECTIONS (each named quantity as an operator output):");
        foreach (var p in ResonanceOperatorAudit.Projections())
            sb.AppendLine($"  {p.Quantity,-7} = {p.Formula} = {p.Value:F4}  via {p.Operator}  [verified: {p.Verified}]");

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, ResonanceOperatorAudit.Operators().Length);
        Assert.Equal(6, ResonanceOperatorAudit.Projections().Length);
        Assert.True(ResonanceOperatorAudit.VerifiedProjectionCount() == 6, "all six derived quantities are operator projections");
    }

    [Fact]
    public void ATQG2611_DerivationClustering()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2611: clustering the successful derivations by operator");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - if the operator layer is real, every successful derivation should consume");
        sb.AppendLine("    operator outputs (the moments/ratios/gap), never the raw spectrum modes.");
        sb.AppendLine();

        foreach (var c in ResonanceOperatorAudit.Clusters())
            sb.AppendLine($"  {c.Phase,-12} {c.Result,-42} [{c.OperatorsUsed,-28}] {c.Quantities}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(7, ResonanceOperatorAudit.Clusters().Length);
        Assert.True(ResonanceOperatorAudit.AllDerivationsThroughOperators(),
            "the QG140-258 formulas use the moment projections, never raw Laplacian modes/eigenvalues");
    }

    [Fact]
    public void ATQG2612_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2612: the minimum operator basis and the layer determination");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - NO OPERATOR LAYER (score ≤ 3), PARTIAL (4-5), OPERATOR LAYER (6-7);");
        sb.AppendLine("  - the minimum basis = the spectral operators (crowding, compression, beat,");
        sb.AppendLine("    locking) + the universal moment read-out.");
        sb.AppendLine();

        sb.AppendLine($"Minimum operator basis ({ResonanceOperatorAudit.MinimumBasisSize()} operators): "
            + string.Join(", ", ResonanceOperatorAudit.MinimumBasis()));
        sb.AppendLine($"Basis outputs: {ResonanceOperatorAudit.BasisOutputCount()} quantities");
        sb.AppendLine();
        sb.AppendLine($"SUMMARY: {ResonanceOperatorAudit.Summary()}");
        sb.AppendLine($"Operator-basis score: {ResonanceOperatorAudit.OperatorBasisScore()}/7");
        sb.AppendLine($"CLASSIFICATION = {ResonanceOperatorAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The five named quantities are NOT primitives: each is a verified projection");
        sb.AppendLine("    (Σm=Σmᵢ, Σ√m=Σ√mᵢ over the crowding multiset; occMom=Σocc²/occ₀ over the");
        sb.AppendLine("    compressed octave bands; span=ω_max/ω_min; λ₂=spectral gap).");
        sb.AppendLine("  - The minimum basis is small (4 spectral operators + 1 read-out) and generates");
        sb.AppendLine("    all six quantities from the one D96 spectrum.");
        sb.AppendLine("  - Every successful derivation passes through the layer — no formula reads a raw");
        sb.AppendLine("    mode or eigenvalue. The operator layer is the interface between D96 and physics.");
        sb.AppendLine("  - Honest caveat: the operators are structural projections, but WHICH output was");
        sb.AppendLine("    assigned to WHICH sector (ν→Σ√m, u→occMom, ...) retains target-information from");
        sb.AppendLine("    the QG149-157 era (QG259 MEDIUM; QG257 NO UNIVERSAL PRINCIPLE).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("OPERATOR LAYER", ResonanceOperatorAudit.Classify());
        Assert.True(ResonanceOperatorAudit.OperatorBasisScore() == 7, "6 verified projections + 1 small-basis");
        Assert.True(ResonanceOperatorAudit.MinimumBasisSize() <= 6);
        Assert.Contains("OPERATOR LAYER", ResonanceOperatorAudit.Summary());
    }
}
