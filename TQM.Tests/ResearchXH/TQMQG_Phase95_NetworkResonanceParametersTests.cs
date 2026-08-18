using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 95 — Global resonance origin of parameters. Determines whether masses/couplings/mixing angles can be
/// interpreted as stable global resonance modes. Classify: NO RELATION / PARTIAL RELATION / RESONANCE ORIGIN.
///
/// Tests: TQMQG950 (normal modes + link resonances), TQMQG951 (actualization frequency + spectra + quantization),
/// TQMQG952 (classification).
/// </summary>
public class TQMQG_Phase95_NetworkResonanceParametersTests : ResearchTestBase
{
    public TQMQG_Phase95_NetworkResonanceParametersTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG950: network normal modes, link-state resonances ──────────────────────

    [Fact]
    public void TQMQG950_NormalModesAndResonances()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG950: do normal modes / link resonances exist?");

        bool modes = NetworkResonanceParameters.NetworkHasNormalModes();
        bool resonate = NetworkResonanceParameters.LinkStatesResonate();

        sb.AppendLine($"network HAS normal modes (Laplacian/dynamics eigenmodes): {modes}");
        sb.AppendLine($"link states resonate at eigenfrequencies: {resonate}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network genuinely hosts normal modes, and link states (ρ, ψ, θ, S, J) can oscillate at");
        sb.AppendLine("eigenfrequencies — the structural substrate for a resonance interpretation of parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(modes, "normal modes exist");
        Assert.True(resonate, "link states resonate");
    }

    // ── TQMQG951: actualization frequencies, discrete spectra, quantization ────────

    [Fact]
    public void TQMQG951_FrequencySpectraQuantization()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG951: actualization frequency, discrete spectra, quantization");

        bool frequency = NetworkResonanceParameters.ActualizationHasFrequency();
        bool discrete = NetworkResonanceParameters.DiscreteSpectraExist();
        bool quantize = NetworkResonanceParameters.ResonanceQuantizationPlausible();
        bool native = NetworkResonanceParameters.NativeDynamicsIdentified();

        sb.AppendLine($"actualization has a native frequency (energy = ħω): {frequency}");
        sb.AppendLine($"finite network gives a DISCRETE spectrum: {discrete}");
        sb.AppendLine($"parameter quantization PLAUSIBLE (resonance modes): {quantize}");
        sb.AppendLine($"NATIVE dynamics identified whose spectrum = SM params: {native}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: mass = resonance frequency (E = mc² = ħω) is a structural analogy, and quantization would be");
        sb.AppendLine("natural. But no native dynamics/Hamiltonian determines the specific frequencies — the mapping is speculative.");
        Output.WriteLine(sb.ToString());

        Assert.True(frequency, "actualization has frequency");
        Assert.True(discrete, "discrete spectra exist");
        Assert.True(quantize, "quantization plausible");
        Assert.False(native, "no native dynamics identified");
    }

    // ── TQMQG952: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG952_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG952: NO RELATION / PARTIAL RELATION / RESONANCE ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {NetworkResonanceParameters.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: normal modes and discrete spectra are real network structure.");
        sb.AppendLine("  • NOT RESONANCE ORIGIN: no native dynamics is identified whose resonance spectrum equals the SM parameters.");
        sb.AppendLine("  • PARTIAL RELATION: resonance modes exist and quantization is plausible, but the mapping is speculative.");
        sb.AppendLine();
        sb.AppendLine("So parameters-as-resonance-modes is a PARTIAL RELATION (analogy), not a full resonance origin.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", NetworkResonanceParameters.Classify());
        Assert.True(NetworkResonanceParameters.NetworkHasNormalModes());
        Assert.False(NetworkResonanceParameters.NativeDynamicsIdentified());
    }
}
