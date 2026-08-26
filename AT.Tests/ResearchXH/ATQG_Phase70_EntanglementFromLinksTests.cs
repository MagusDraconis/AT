using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 70 — quantum entanglement from link structure. Determines whether entanglement emerges from θ + S.
/// Classify: MATCH / PARTIAL / REQUIRES NEW SECTOR.
///
/// Tests: ATQG700 (classical vs Bell), ATQG701 (prerequisites), ATQG702 (classification).
/// </summary>
public class ATQG_Phase70_EntanglementFromLinksTests : ResearchTestBase
{
    public ATQG_Phase70_EntanglementFromLinksTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG700: shared phases give classical, not Bell, correlations ──────────────

    [Fact]
    public void ATQG700_ClassicalVsBell()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG700: shared phases are classical correlations, not entanglement");

        bool sharedEntangle = EntanglementFromLinks.SharedPhasesGiveEntanglement();

        sb.AppendLine($"shared link phases give QUANTUM entanglement (Bell): {sharedEntangle}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a fixed link phase gives a DETERMINISTIC (classical) phase correlation between the two nodes,");
        sb.AppendLine("like the classical correlations of QG30. Bell-type entanglement requires NON-SEPARABILITY — a quantum");
        sb.AppendLine("superposition ACROSS multiple degrees of freedom — which a fixed phase does not provide.");
        Output.WriteLine(sb.ToString());

        Assert.False(sharedEntangle, "shared phases should not give entanglement");
    }

    // ── ATQG701: prerequisites present, entangling interaction missing ─────────────

    [Fact]
    public void ATQG701_Prerequisites()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG701: θ and S give the prerequisites, but not the entangling interaction");

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

    // ── ATQG702: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG702_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG702: MATCH / PARTIAL / REQUIRES NEW SECTOR?");

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
