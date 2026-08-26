using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 94 — Parameters as network eigenvalues. Determines whether masses/couplings/mixing angles can emerge
/// as eigenvalues of global network consistency. Classify: NO RELATION / PARTIAL RELATION / EIGENVALUE ORIGIN.
///
/// Tests: ATQG940 (loop constraints + consistency equations), ATQG941 (spectra + modes + quantization),
/// ATQG942 (classification).
/// </summary>
public class ATQG_Phase94_ParameterEigenvaluesTests : ResearchTestBase
{
    public ATQG_Phase94_ParameterEigenvaluesTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG940: loop constraints, global consistency equations ───────────────────

    [Fact]
    public void ATQG940_LoopsAndEquations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG940: do loop/consistency conditions form equations?");

        bool loops = ParameterEigenvalues.LoopConstraintsFormSystem();
        bool equations = ParameterEigenvalues.GlobalConsistencyEquationsExist();

        sb.AppendLine($"loop constraints form a system of consistency equations: {loops}");
        sb.AppendLine($"global consistency equations exist: {equations}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: loop closure and global metric consistency DO form a system of equations — the natural arena");
        sb.AppendLine("where eigenvalues (as consistency solutions) could in principle arise.");
        Output.WriteLine(sb.ToString());

        Assert.True(loops, "loop constraints form a system");
        Assert.True(equations, "global consistency equations exist");
    }

    // ── ATQG941: network spectra, stable modes, quantization ──────────────────────

    [Fact]
    public void ATQG941_SpectraModesQuantization()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG941: spectra, stable modes, parameter quantization");

        bool spectra = ParameterEigenvalues.NetworkHasSpectra();
        bool modes = ParameterEigenvalues.StableModesHaveEigenvalues();
        bool quantize = ParameterEigenvalues.ParameterQuantizationPlausible();
        bool native = ParameterEigenvalues.NativeOperatorIdentified();

        sb.AppendLine($"network POSSESSES spectra (graph Laplacian): {spectra}");
        sb.AppendLine($"stable normal modes have eigenfrequencies: {modes}");
        sb.AppendLine($"parameter quantization PLAUSIBLE (structural analogy): {quantize}");
        sb.AppendLine($"NATIVE operator identified whose spectrum = SM params: {native}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network has spectra and stable modes, so parameters-as-eigenvalues is a PLAUSIBLE analogy");
        sb.AppendLine("(spectral gap → mass, eigenvectors → mixing). But no native operator is identified — it is speculative.");
        Output.WriteLine(sb.ToString());

        Assert.True(spectra, "network has spectra");
        Assert.True(modes, "stable modes have eigenvalues");
        Assert.True(quantize, "quantization plausible");
        Assert.False(native, "no native operator identified");
    }

    // ── ATQG942: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG942_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG942: NO RELATION / PARTIAL RELATION / EIGENVALUE ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {ParameterEigenvalues.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: the network HAS spectra, and the eigenvalue analogy is structurally real.");
        sb.AppendLine("  • NOT EIGENVALUE ORIGIN: no native operator is identified whose spectrum equals the SM parameters.");
        sb.AppendLine("  • PARTIAL RELATION: spectra exist and quantization is plausible, but the mapping is speculative, not derived.");
        sb.AppendLine();
        sb.AppendLine("So parameters-as-eigenvalues is a PARTIAL RELATION (analogy), not a full eigenvalue origin.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", ParameterEigenvalues.Classify());
        Assert.True(ParameterEigenvalues.NetworkHasSpectra());
        Assert.False(ParameterEigenvalues.NativeOperatorIdentified());
    }
}
