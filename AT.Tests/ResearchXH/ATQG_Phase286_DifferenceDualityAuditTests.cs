using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 286 — Difference Duality Audit. Are ρ and ψ independent primitives, or dual projections
/// of Difference? D96 only, no observables.
/// </summary>
public class ATQG_Phase286_DifferenceDualityAuditTests : ResearchTestBase
{
    public ATQG_Phase286_DifferenceDualityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2860_TwoSectors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2860: the two sectors — scalar and tensor difference");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - ρ is the SCALAR count share (|ψ|² = ρ, QG216);");
        sb.AppendLine("  - ψ is the TENSOR (Weyl) field (QG285).");
        sb.AppendLine();

        sb.AppendLine($"ρ is the scalar count share (|ψ|²=ρ): {DifferenceDualityAudit.RhoIsScalarCount()}");
        sb.AppendLine($"ψ is the tensor (Weyl) field: {DifferenceDualityAudit.PsiIsTensor()}");
        sb.AppendLine();
        sb.AppendLine("ρ = the ISOTROPIC difference from the uniform background (the count);");
        sb.AppendLine("ψ = the ANISOTROPIC difference from conformal flatness (the orientation).");

        Output.WriteLine(sb.ToString());

        Assert.True(DifferenceDualityAudit.RhoIsScalarCount(), "ρ = |ψ|² is the normalized count share");
        Assert.True(DifferenceDualityAudit.PsiIsTensor(), "ψ is the Weyl/anisotropic content");
    }

    [Fact]
    public void ATQG2861_TraceTracelessDecomposition()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2861: the trace/traceless decomposition — ρ and ψ are not independent");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the rank-2 object A_ij decomposes exhaustively as trace + traceless;");
        sb.AppendLine("  - ρ (trace) and ψ (traceless) are projections of the same object.");
        sb.AppendLine();

        sb.AppendLine($"adjacency components (d=3): {DifferenceDualityAudit.AdjacencyComponents()}");
        sb.AppendLine($"trace (scalar, ρ) DOF: {DifferenceDualityAudit.TraceDof()}");
        sb.AppendLine($"traceless (tensor, ψ) DOF: {DifferenceDualityAudit.TracelessDof()}");
        sb.AppendLine($"spin-2 TT polarizations: {DifferenceDualityAudit.Spin2Polarizations()}");
        sb.AppendLine($"decomposition exhaustive (1+5=6): {DifferenceDualityAudit.DecompositionExhaustive()}");
        sb.AppendLine($"ρ and ψ not independent: {DifferenceDualityAudit.RhoAndPsiNotIndependent()}");

        Output.WriteLine(sb.ToString());

        Assert.True(DifferenceDualityAudit.DecompositionExhaustive(),
            "the trace + traceless decomposition is exhaustive");
        Assert.True(DifferenceDualityAudit.RhoAndPsiNotIndependent(),
            "ρ and ψ are both projections of the same rank-2 object");
        Assert.Equal(2, DifferenceDualityAudit.Spin2Polarizations());
    }

    [Fact]
    public void ATQG2862_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2862: the final duality determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - INDEPENDENT (score ≤ 2), PARTIAL DUALITY (3-4), DIFFERENCE DUALITY (5-6);");
        sb.AppendLine("  - the question: is Difference → {ρ, ψ} the final duality?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {DifferenceDualityAudit.Summary()}");
        sb.AppendLine($"Duality score: {DifferenceDualityAudit.DualityScore()}/6");
        sb.AppendLine($"count/orientation duality: {DifferenceDualityAudit.CountOrientationDuality()}");
        sb.AppendLine($"CLASSIFICATION = {DifferenceDualityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - ρ and ψ are the TRACE and TRACELESS decomposition of the SAME rank-2 object");
        sb.AppendLine("    (the connectivity/stress/difference structure): 6 = 1 (trace, ρ) + 5");
        sb.AppendLine("    (traceless, ψ), with 2 TT polarizations.");
        sb.AppendLine("  - They are NOT independent primitives: both are projections of the one object.");
        sb.AppendLine("  - The count/orientation duality: ρ = how many units of difference (|ψ|² = ρ),");
        sb.AppendLine("    ψ = which direction (the + and × modes).");
        sb.AppendLine("  - The decomposition is EXHAUSTIVE — {ρ, ψ} is the COMPLETE duality. The two");
        sb.AppendLine("    'primitives' of AT are the two faces of the single Difference: the scalar");
        sb.AppendLine("    (count) face and the tensor (orientation) face.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("DIFFERENCE DUALITY", DifferenceDualityAudit.Classify());
        Assert.True(DifferenceDualityAudit.DualityScore() >= 5);
        Assert.Contains("DIFFERENCE DUALITY", DifferenceDualityAudit.Summary());
        Assert.Contains("trace", DifferenceDualityAudit.Summary());
    }
}
