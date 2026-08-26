using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 49 — network-mode explanation of GW strain. Tests whether collective Q-event network modes can
/// reproduce the observed differential strain without ψ. Classify: IMPOSSIBLE / PARTIAL MATCH / FULL MATCH.
///
/// Tests: ATQG490 (breathing vs tensor), ATQG491 (collective modes are scalar), ATQG492 (classification).
/// </summary>
public class ATQG_Phase49_NetworkModeGWTests : ResearchTestBase
{
    public ATQG_Phase49_NetworkModeGWTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG490: breathing (common-mode) vs tensor (differential) ───────────────────

    [Fact]
    public void ATQG490_BreathingVsTensor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG490: scalar breathing is common-mode; tensor is differential");

        double breathing = NetworkModeGW.BreathingDifferentialStrain();
        double tensor = NetworkModeGW.TensorDifferentialStrain(1.0);
        bool breathingVisible = NetworkModeGW.BreathingVisibleToMichelson();

        sb.AppendLine($"breathing (scalar) mode: differential strain = {breathing:F1}  (both arms stretch equally)");
        sb.AppendLine($"tensor (+/×) mode:       differential strain = {tensor:F1}  (one arm stretches, other squeezes)");
        sb.AppendLine($"breathing mode visible to a Michelson: {breathingVisible}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the Michelson measures DIFFERENTIAL arm strain. A scalar breathing mode is common-mode");
        sb.AppendLine("(zero differential), so it is invisible; only the tensor mode gives a nonzero differential signal.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0.0, breathing);
        Assert.Equal(2.0, tensor);
        Assert.False(breathingVisible, "breathing mode should be invisible to a Michelson");
    }

    // ── ATQG491: collective network modes are scalar ────────────────────────────────

    [Fact]
    public void ATQG491_CollectiveModesAreScalar()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG491: collective modes of a scalar network stay scalar");

        bool scalar = NetworkModeGW.CollectiveModesAreScalar();
        bool reproduce = NetworkModeGW.ReproduceDifferentialStrain();

        sb.AppendLine($"collective network modes are SCALAR (ρ is spin-0): {scalar}");
        sb.AppendLine($"scalar collective modes reproduce the +/× differential strain: {reproduce}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: however many nodes, however synchronized, collective oscillation modes of a scalar density ρ");
        sb.AppendLine("are themselves scalar — they can only form a breathing (monopole) wave, never the quadrupole (+/×) pattern");
        sb.AppendLine("of a spin-2 wave (QG23/QG37: no scalar can source spin-2).");
        Output.WriteLine(sb.ToString());

        Assert.True(scalar, "collective modes should be scalar");
        Assert.False(reproduce, "scalar collective modes should not reproduce the differential strain");
    }

    // ── ATQG492: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG492_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG492: IMPOSSIBLE / PARTIAL MATCH / FULL MATCH?");

        sb.AppendLine($"CLASSIFICATION: {NetworkModeGW.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT FULL MATCH: a scalar network mode is a breathing (common-mode) wave — zero differential, so it");
        sb.AppendLine("    cannot be the observed +/× strain.");
        sb.AppendLine("  • NOT EVEN PARTIAL for the OBSERVABLE: the breathing mode is invisible to a Michelson (common-mode), so it");
        sb.AppendLine("    produces no differential detector output at all (QG20).");
        sb.AppendLine("  • IMPOSSIBLE: collective Q-event network modes are scalar; no scalar (collective or otherwise) can source");
        sb.AppendLine("    the spin-2 +/× pattern (QG23/QG37). The fundamental ψ remains required.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("IMPOSSIBLE", NetworkModeGW.Classify());
        Assert.False(NetworkModeGW.ReproduceDifferentialStrain());
        Assert.True(NetworkModeGW.CollectiveModesAreScalar());
    }
}
