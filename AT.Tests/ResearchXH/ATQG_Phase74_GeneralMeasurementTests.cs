using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 74 — general measurement basis. Determines whether actualization reproduces arbitrary bases.
/// Classify: MATCH / PARTIAL / NO MATCH.
///
/// Tests: ATQG740 (multi-state), ATQG741 (basis rotation + POVM), ATQG742 (classification).
/// </summary>
public class ATQG_Phase74_GeneralMeasurementTests : ResearchTestBase
{
    public ATQG_Phase74_GeneralMeasurementTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG740: multi-state actualization ────────────────────────────────────────

    [Fact]
    public void ATQG740_MultiStateActualization()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG740: the node is multi-state, not merely binary");

        bool multiState = GeneralMeasurement.MultiStateActualization();

        sb.AppendLine($"node's state space is MULTI-STATE (θ continuous + S spin): {multiState}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: with θ (continuous phase) and S (spin), a node's state space is multi-dimensional, so");
        sb.AppendLine("actualization projects onto a general state, not just tick/no-tick.");
        Output.WriteLine(sb.ToString());

        Assert.True(multiState, "the node should be multi-state");
    }

    // ── ATQG741: basis rotation + POVM ────────────────────────────────────────────

    [Fact]
    public void ATQG741_BasisRotationAndPovm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG741: arbitrary bases via unitaries; POVMs via ancillas");

        bool rotation = GeneralMeasurement.BasisRotationAvailable();
        bool povm = GeneralMeasurement.PovmReproducible();
        bool born = GeneralMeasurement.BornWeightConsistent();

        sb.AppendLine($"arbitrary basis via UNITARY rotation (θ + S + J): {rotation}");
        sb.AppendLine($"POVMs via ancillas (Naimark dilation):            {povm}");
        sb.AppendLine($"Born rule consistent in any basis:                {born}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: an arbitrary basis {|φ_i⟩} is mapped to the actualization basis by a unitary; actualization then");
        sb.AppendLine("projects Born-weighted. The most general (POVM) measurements use extra nodes as ancillas. So arbitrary");
        sb.AppendLine("measurements are reproduced.");
        Output.WriteLine(sb.ToString());

        Assert.True(rotation, "basis rotation should be available");
        Assert.True(povm, "POVMs should be reproducible");
        Assert.True(born, "the Born rule should be consistent");
    }

    // ── ATQG742: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG742_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG742: MATCH / PARTIAL / NO MATCH?");

        sb.AppendLine($"CLASSIFICATION: {GeneralMeasurement.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • MATCH: multi-state actualization (θ + S) + unitary basis rotation (θ + S + J) + POVM via ancillas");
        sb.AppendLine("    reproduce ARBITRARY quantum measurement bases, with the Born rule in any basis.");
        sb.AppendLine("  • This resolves the residual gap of QG73 (binary limitation): arbitrary bases are now reproduced.");
        sb.AppendLine("  • CAVEAT: it requires the full quantum structure (θ, S, J) and extra nodes (ancillas) — all already present.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("MATCH", GeneralMeasurement.Classify());
        Assert.True(GeneralMeasurement.MultiStateActualization());
        Assert.True(GeneralMeasurement.BasisRotationAvailable());
    }
}
