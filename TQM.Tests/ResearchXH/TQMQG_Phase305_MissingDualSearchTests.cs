using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 305 — Missing Dual Search. Find physics quantities lacking explicit tensor/scalar
/// duals; predict the missing dual observables. No observables, no target values, D96 only,
/// deterministic.
/// </summary>
public class TQMQG_Phase305_MissingDualSearchTests : ResearchTestBase
{
    public TQMQG_Phase305_MissingDualSearchTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3050_MatrixTensorDuals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3050: matrix/tensor quantities → scalar angle/mass sets");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the mixing matrices (CKM, PMNS) are tensors whose scalar face is the angle set;");
        sb.AppendLine("  - the Majorana mass matrix's scalar face is the effective mass m_ββ.");
        sb.AppendLine();

        sb.AppendLine("MATRIX/TENSOR → SCALAR DUAL:");
        foreach (var e in MissingDualSearch.Entries().Where(e => e.HasMatrixTensorStructure))
        {
            sb.AppendLine($"  {e.Quantity} ({e.Type})");
            sb.AppendLine($"      missing dual: {e.MissingDualObservable} ({e.DualType})");
            sb.AppendLine($"      {e.Reading}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(MissingDualSearch.MatrixTensorCount() >= 2,
            "the mixing matrices and mass matrix must have scalar duals");
        Assert.True(MissingDualSearch.MixingMatricesDualized(),
            "the CKM and PMNS matrices must be dualized to their angle sets");
    }

    [Fact]
    public void TQMQG3051_ScalarToTensorDuals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3051: scalar quantities → tensor rotation/stress/polarization");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Λ's tensor face is the cosmological term Λg_μν;");
        sb.AppendLine("  - the CMB temperature's tensor face is the B-mode polarization;");
        sb.AppendLine("  - the Jarlskog invariant's tensor face is the CKM matrix.");
        sb.AppendLine();

        sb.AppendLine("SCALAR → TENSOR DUAL:");
        foreach (var e in MissingDualSearch.Entries().Where(e => !e.HasMatrixTensorStructure))
        {
            sb.AppendLine($"  {e.Quantity} ({e.Type})");
            sb.AppendLine($"      missing dual: {e.MissingDualObservable} ({e.DualType})");
            sb.AppendLine($"      {e.Reading}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(MissingDualSearch.ScalarToTensorCount() >= 3,
            "Λ, the CMB, J, and the Weinberg angle must have tensor duals");
        Assert.True(MissingDualSearch.RemainingObservablesDualized(),
            "the remaining observables must be dualized");
    }

    [Fact]
    public void TQMQG3052_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3052: the missing-dual determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - NEW DUALS: physics quantities lacking explicit duals are found and their");
        sb.AppendLine("    missing dual observables are predicted;");
        sb.AppendLine("  - the scalar/tensor duality extends to the full published observable record.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {MissingDualSearch.Summary()}");
        sb.AppendLine($"Search score: {MissingDualSearch.SearchScore()}/5");
        sb.AppendLine($"missing duals: {MissingDualSearch.MissingDualCount()}  matrix→scalar: {MissingDualSearch.MatrixTensorCount()}  scalar→tensor: {MissingDualSearch.ScalarToTensorCount()}");
        sb.AppendLine($"full record dualized: {MissingDualSearch.FullRecordDualized()}");
        sb.AppendLine($"CLASSIFICATION = {MissingDualSearch.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the published record lacks explicit duals for:");
        sb.AppendLine("    · the mixing matrices — CKM V → {Vus, Vcb, Vub, δ_CP}, PMNS U → {θ12, θ23, θ13, δ_ν};");
        sb.AppendLine("    · the Majorana mass matrix — M_ν → m_ββ;");
        sb.AppendLine("    · the cosmological constant — Λ → Λg_μν;");
        sb.AppendLine("    · the CMB temperature — C_ℓ^TT → C_ℓ^BB (B-mode polarization);");
        sb.AppendLine("    · the Jarlskog invariant — J → V (the CKM rotation tensor);");
        sb.AppendLine("    · the Weinberg angle — sin²θ_W → the SU(2) isospin rotation;");
        sb.AppendLine("  - each predicted dual observable completes the scalar/tensor duality for the");
        sb.AppendLine("    full observable record.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW DUALS", MissingDualSearch.Classify());
        Assert.True(MissingDualSearch.SearchScore() >= 3);
        Assert.True(MissingDualSearch.FullRecordDualized());
        Assert.Contains("NEW DUALS", MissingDualSearch.Summary());
    }
}
