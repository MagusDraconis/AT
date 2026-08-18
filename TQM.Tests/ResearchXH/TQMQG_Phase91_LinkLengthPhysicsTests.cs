using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 91 — Physical meaning of link length. Determines whether link length/distance can encode physical
/// parameter values. Classify: IRRELEVANT / PARTIAL / VALUE SELECTION.
///
/// Tests: TQMQG910 (coupling + mass hierarchy), TQMQG911 (Yukawa + mixing + metric), TQMQG912 (classification).
/// </summary>
public class TQMQG_Phase91_LinkLengthPhysicsTests : ResearchTestBase
{
    public TQMQG_Phase91_LinkLengthPhysicsTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG910: coupling strength vs link length, mass hierarchy ─────────────────

    [Fact]
    public void TQMQG910_CouplingAndMass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG910: does link length relate to coupling and mass?");

        bool metric = LinkLengthPhysics.LinkLengthIsMetric();
        bool coupling = LinkLengthPhysics.LinkLengthRelatesToCoupling();
        bool mass = LinkLengthPhysics.LinkLengthEncodesMassViaYukawa();

        sb.AppendLine($"link length is the network metric (derived from ρ): {metric}");
        sb.AppendLine($"link length relates to gauge coupling (lattice analogy): {coupling}");
        sb.AppendLine($"link length encodes mass via Yukawa suppression: {mass}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: link length IS the metric (derived), and it can RELATE to coupling/mass via lattice/Yukawa");
        sb.AppendLine("analogies. These are natural encoding mechanisms, not derivations of specific values.");
        Output.WriteLine(sb.ToString());

        Assert.True(metric, "link length is metric");
        Assert.True(coupling, "coupling relation representable");
        Assert.True(mass, "mass encoding representable");
    }

    // ── TQMQG911: Yukawa suppression, mixing strength, metric interpretation ───────

    [Fact]
    public void TQMQG911_YukawaMixingMetric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG911: Yukawa suppression, CKM/PMNS strength, metric");

        bool yukawa = LinkLengthPhysics.YukawaSuppressionRepresentable();
        bool mixing = LinkLengthPhysics.MixingSuppressionRepresentable();
        bool determines = LinkLengthPhysics.LinkLengthDeterminesValues();

        sb.AppendLine($"Yukawa suppression e^(−m r) representable: {yukawa}");
        sb.AppendLine($"mixing strength suppressed by link length: {mixing}");
        sb.AppendLine($"link length DETERMINES specific values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Yukawa suppression and distance-suppressed mixing are COMPATIBLE mechanisms — they show HOW");
        sb.AppendLine("link length could encode values — but the exponents (m), couplings (g), and mixing angles stay free.");
        Output.WriteLine(sb.ToString());

        Assert.True(yukawa, "Yukawa suppression representable");
        Assert.True(mixing, "mixing suppression representable");
        Assert.False(determines, "link length does not determine values");
    }

    // ── TQMQG912: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG912_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG912: IRRELEVANT / PARTIAL / VALUE SELECTION?");

        sb.AppendLine($"CLASSIFICATION: {LinkLengthPhysics.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT IRRELEVANT: link length IS the metric and can host Yukawa/lattice encoding.");
        sb.AppendLine("  • NOT VALUE SELECTION: link length does not determine the specific values.");
        sb.AppendLine("  • PARTIAL: metric geometry is derived; Yukawa/lattice encoding of values is compatible but not derivational.");
        sb.AppendLine();
        sb.AppendLine("So link length PARTIALLY encodes parameter values (geometry derived; value encoding compatible).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL", LinkLengthPhysics.Classify());
        Assert.True(LinkLengthPhysics.LinkLengthIsMetric());
        Assert.False(LinkLengthPhysics.LinkLengthDeterminesValues());
    }
}
