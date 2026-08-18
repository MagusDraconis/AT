using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 70 — quantum entanglement from link structure. Determines whether entanglement emerges from θ + S.
/// Classify: MATCH / PARTIAL / REQUIRES NEW SECTOR.
///
/// Tests: TQMQG700 (classical vs Bell), TQMQG701 (prerequisites), TQMQG702 (classification).
/// </summary>
public class TQMQG_Phase70_EntanglementFromLinksTests : ResearchTestBase
{
    public TQMQG_Phase70_EntanglementFromLinksTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG700: shared phases give classical, not Bell, correlations ──────────────

    [Fact]
    public void TQMQG700_ClassicalVsBell()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG700: shared phases are classical correlations, not entanglement");

        bool sharedEntangle = EntanglementFromLinks.SharedPhasesGiveEntanglement();

        sb.AppendLine($"shared link phases give QUANTUM entanglement (Bell): {sharedEntangle}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a fixed link phase gives a DETERMINISTIC (classical) phase correlation between the two nodes,");
        sb.AppendLine("like the classical correlations of QG30. Bell-type entanglement requires NON-SEPARABILITY — a quantum");
        sb.AppendLine("superposition ACROSS multiple degrees of freedom — which a fixed phase does not provide.");
        Output.WriteLine(sb.ToString());

        Assert.False(sharedEntangle, "shared phases should not give entanglement");
    }

    // ── TQMQG701: prerequisites present, entangling interaction missing ─────────────

    [Fact]
    public void TQMQG701_Prerequisites()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG701: θ and S give the prerequisites, but not the entangling interaction");

        bool superposition = EntanglementFromLinks.ThetaProvidesSuperposition();
        bool spinor = EntanglementFromLinks.SpinProvidesSpinorDof();
        bool entangling = EntanglementFromLinks.RequiresEntanglingInteractions();
        bool recovered = EntanglementFromLinks.EntanglementRecovered();

        sb.AppendLine($"θ provides single-DOF superposition: {superposition}  (QG65)");
        sb.AppendLine($"S provides spinor DOF:              {spinor}  (QG66)");
        sb.AppendLine($"entangling interaction is REQUIRED: {entangling}");
        sb.AppendLine($"entanglement recovered from θ + S:  {recovered}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: θ and S supply the ingredients (superposition + spinor DOF), but the entangling interaction —");
        sb.AppendLine("which creates non-separability across multiple DOF — is missing. It is a new sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(superposition, "theta should provide superposition");
        Assert.True(spinor, "spin structure should provide spinor DOF");
        Assert.True(entangling, "entangling interactions should be required");
        Assert.False(recovered, "entanglement should not be fully recovered");
    }

    // ── TQMQG702: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG702_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG702: MATCH / PARTIAL / REQUIRES NEW SECTOR?");

        sb.AppendLine($"CLASSIFICATION: {EntanglementFromLinks.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT MATCH: shared link phases give classical correlations, not Bell-type non-separability.");
        sb.AppendLine("  • PARTIAL (prerequisites): θ gives superposition, S gives spinor DOF.");
        sb.AppendLine("  • REQUIRES NEW SECTOR: the entangling interaction (a quantum link / entangling gate) is a new sector");
        sb.AppendLine("    beyond θ and S — entanglement is not recovered from θ + S alone.");
        sb.AppendLine();
        sb.AppendLine("So interference (QG65) MATCHes from θ, but entanglement needs a further new sector: entangling interactions.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("REQUIRES NEW SECTOR", EntanglementFromLinks.Classify());
        Assert.False(EntanglementFromLinks.EntanglementRecovered());
        Assert.True(EntanglementFromLinks.RequiresEntanglingInteractions());
    }
}
