using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 48 — GW observation audit. Separates what is directly observed from what is inferred.
/// Classify: DIRECT / MODEL-DEPENDENT / UNDECIDED.
///
/// Tests: TQMQG480 (the four layers), TQMQG481 (spin-2 reconstructed), TQMQG482 (consequence for ψ).
/// </summary>
public class TQMQG_Phase48_GWObservationAuditTests : ResearchTestBase
{
    public TQMQG_Phase48_GWObservationAuditTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG480: the four layers ─────────────────────────────────────────────────────

    [Fact]
    public void TQMQG480_FourLayers()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG480: directly observed vs inferred");

        int direct = 0, modelDependent = 0, undecided = 0;
        foreach (var layer in GWObservationAudit.Layers)
        {
            string c = GWObservationAudit.Classify(layer);
            sb.AppendLine($"{layer,-28} -> {c}");
            switch (c)
            {
                case "DIRECT": direct++; break;
                case "MODEL-DEPENDENT": modelDependent++; break;
                case "UNDECIDED": undecided++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"DIRECT          : {direct}");
        sb.AppendLine($"MODEL-DEPENDENT : {modelDependent}");
        sb.AppendLine($"UNDECIDED       : {undecided}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, direct);
        Assert.Equal(3, modelDependent);
        Assert.Equal(0, undecided);
    }

    // ── TQMQG481: spin-2 is reconstructed ─────────────────────────────────────────────

    [Fact]
    public void TQMQG481_Spin2Reconstructed()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG481: is spin-2 directly measured or reconstructed?");

        bool direct = GWObservationAudit.Spin2DirectlyMeasured();
        bool reconstructed = GWObservationAudit.Spin2Reconstructed();

        sb.AppendLine($"spin-2 DIRECTLY measured:   {direct}");
        sb.AppendLine($"spin-2 RECONSTRUCTED:       {reconstructed}");
        sb.AppendLine($"directly observed layer:    {GWObservationAudit.DirectLayer()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the only DIRECT observable is the strain signal h(t) (differential arm-length change). The");
        sb.AppendLine("spin-2 (tensor) reading is RECONSTRUCTED — it is the output of fitting the strain to a polarization basis");
        sb.AppendLine("under GR model assumptions (templates, massless light-speed propagation).");
        Output.WriteLine(sb.ToString());

        Assert.False(direct, "spin-2 should not be directly measured");
        Assert.True(reconstructed, "spin-2 should be reconstructed");
    }

    // ── TQMQG482: consequence for ψ ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG482_ConsequenceForPsi()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG482: what this means for the ψ postulate");

        sb.AppendLine("IMPLICATION FOR ψ:");
        sb.AppendLine("  • QG47 said ψ exists because of GW polarization data. This audit refines that: the data are a DIRECT");
        sb.AppendLine("    strain signal, but the 'spin-2 polarization' reading is a MODEL-DEPENDENT reconstruction.");
        sb.AppendLine("  • So ψ is justified by an INFERENCE, not a raw measurement — the tensor interpretation is one model");
        sb.AppendLine("    (GR) among possible readings of the same strain.");
        sb.AppendLine("  • ψ remains the minimal postulate consistent with that model; but its necessity is one model-deep:");
        sb.AppendLine("    it is forced by the GR reconstruction of the strain, not by the strain itself.");
        sb.AppendLine();
        sb.AppendLine("This is the final epistemological honesty of the QG arc: the one observation that motivates Primitive 2 is");
        sb.AppendLine("itself a model-dependent reconstruction, so ψ is a model-consistent postulate, not a directly-forced one.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("MODEL-DEPENDENT", GWObservationAudit.Classify("spin-assignment"));
        Assert.Equal("DIRECT", GWObservationAudit.Classify("detector-signal"));
        Assert.True(GWObservationAudit.Spin2Reconstructed());
    }
}
