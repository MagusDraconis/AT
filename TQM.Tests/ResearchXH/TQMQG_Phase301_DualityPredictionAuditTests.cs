using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 301 — Duality Prediction Audit. For every scalar result search the tensor dual; for
/// every tensor result search the scalar dual. No observables, no target values, D96 only,
/// deterministic. Output: DUALITY COMPLETE / PARTIAL / BROKEN.
/// </summary>
public class TQMQG_Phase301_DualityPredictionAuditTests : ResearchTestBase
{
    public TQMQG_Phase301_DualityPredictionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3010_ScalarToTensorDuals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3010: every scalar result has a tensor dual");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - ρ ↔ ψ (the trace/traceless of the same difference object, QG286);");
        sb.AppendLine("  - the Born rule, the conformal metric, and the masses/couplings have tensor faces.");
        sb.AppendLine();

        sb.AppendLine($"difference duality complete: {DualityPredictionAudit.DifferenceDualityComplete()}");
        sb.AppendLine($"decomposition exhaustive (6=1+5, 2 TT): {DualityPredictionAudit.DecompositionExhaustive()}");
        sb.AppendLine();
        sb.AppendLine("SCALAR → TENSOR:");
        foreach (var d in DualityPredictionAudit.ScalarDuals())
        {
            sb.AppendLine($"  {d.ScalarResult} → {d.TensorDual}  [explicit={d.IsExplicit}]");
            sb.AppendLine($"      {d.Reading}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(DualityPredictionAudit.DifferenceDualityComplete(),
            "the difference duality must be complete");
        Assert.True(DualityPredictionAudit.DecompositionExhaustive(),
            "the rank-2 decomposition must be exhaustive");
        Assert.True(DualityPredictionAudit.ExplicitScalarDuals() >= 3,
            "at least 3 scalar results must have explicit tensor duals");
    }

    [Fact]
    public void TQMQG3011_TensorToScalarDuals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3011: every tensor result has a scalar dual");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Weyl ψ → ρ, GW polarizations → |ψ|², frame dragging h_0i → h_00,");
        sb.AppendLine("    Einstein G_μν → scalar curvature R, S∝A → deficit count.");
        sb.AppendLine();

        sb.AppendLine("TENSOR → SCALAR:");
        foreach (var d in DualityPredictionAudit.TensorDuals())
        {
            sb.AppendLine($"  {d.TensorResult} → {d.ScalarDual}  [explicit={d.IsExplicit}]");
            sb.AppendLine($"      {d.Reading}");
        }
        sb.AppendLine();
        sb.AppendLine($"explicit tensor→scalar duals: {DualityPredictionAudit.ExplicitTensorDuals()}/5");

        Output.WriteLine(sb.ToString());

        Assert.True(DualityPredictionAudit.ExplicitTensorDuals() >= 3,
            "at least 3 tensor results must have explicit scalar duals");
    }

    [Fact]
    public void TQMQG3012_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3012: the duality determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - DUALITY COMPLETE: every tensor result has a scalar dual and vice versa;");
        sb.AppendLine("  - the scalar VALUES (masses/couplings) have weaker tensor duals — an asymmetry");
        sb.AppendLine("    of explicitness, not a break.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {DualityPredictionAudit.Summary()}");
        sb.AppendLine($"Duality score: {DualityPredictionAudit.DualityScore()}/5");
        sb.AppendLine($"explicit scalar duals: {DualityPredictionAudit.ExplicitScalarDuals()}  explicit tensor duals: {DualityPredictionAudit.ExplicitTensorDuals()}");
        sb.AppendLine($"duality structurally complete: {DualityPredictionAudit.DualityStructurallyComplete()}");
        sb.AppendLine($"CLASSIFICATION = {DualityPredictionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the Difference duality {ρ, ψ} (QG286) is structurally complete: 6 = 1 trace");
        sb.AppendLine("    + 5 traceless, 2 TT polarizations;");
        sb.AppendLine("  - SCALAR → TENSOR: ρ→ψ (count vs Weyl), Born |ψ|²=ρ→ψ (amplitude), conformal");
        sb.AppendLine("    g→h_ij^TT (metric perturbations), masses/couplings→M_Pl/κ (weaker, from the");
        sb.AppendLine("    same spectral constants);");
        sb.AppendLine("  - TENSOR → SCALAR: Weyl→ρ, GW(+×)→|ψ|², frame dragging h_0i→h_00, Einstein");
        sb.AppendLine("    G_μν→R, S∝A→deficit count;");
        sb.AppendLine("  - the residual asymmetry (the scalar VALUES have weaker tensor duals) is");
        sb.AppendLine("    structural — an asymmetry of EXPLICITNESS, not a duality break.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("DUALITY COMPLETE", DualityPredictionAudit.Classify());
        Assert.True(DualityPredictionAudit.DualityScore() >= 5);
        Assert.True(DualityPredictionAudit.DualityStructurallyComplete());
        Assert.Contains("DUALITY COMPLETE", DualityPredictionAudit.Summary());
    }
}
