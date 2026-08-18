using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 98 — Physical meaning of network angles. Determines whether network angles correspond to physical
/// mixing angles and internal symmetry rotations. Classify: NO RELATION / PARTIAL RELATION / ANGLE ORIGIN.
///
/// Tests: TQMQG980 (triangle + orientation angles), TQMQG981 (CKM/PMNS + gauge rotations), TQMQG982 (classification).
/// </summary>
public class TQMQG_Phase98_NetworkAnglesTests : ResearchTestBase
{
    public TQMQG_Phase98_NetworkAnglesTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG980: triangle angles, link orientation ────────────────────────────────

    [Fact]
    public void TQMQG980_TriangleAndOrientation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG980: do real geometric network angles exist?");

        bool triangle = NetworkAngles.TriangleAnglesExist();
        bool orientation = NetworkAngles.LinkOrientationAnglesExist();

        sb.AppendLine($"triangle angles exist (from length ratios): {triangle}");
        sb.AppendLine($"link-orientation angles exist (geometric): {orientation}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network genuinely has GEOMETRIC angles (triangle and orientation) in spacetime geometry.");
        Output.WriteLine(sb.ToString());

        Assert.True(triangle, "triangle angles exist");
        Assert.True(orientation, "orientation angles exist");
    }

    // ── TQMQG981: CKM/PMNS analogs, gauge rotations ────────────────────────────────

    [Fact]
    public void TQMQG981_InternalVsGeometric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG981: are mixing/gauge angles geometric or internal?");

        bool ckm = NetworkAngles.CkmAnglesAreInternal();
        bool pmns = NetworkAngles.PmnsAnglesAreInternal();
        bool gauge = NetworkAngles.GaugeRotationsAreInternal();
        bool differ = NetworkAngles.GeometricAnglesDifferFromInternalRotations();
        bool determines = NetworkAngles.AnglesDetermineMixingValues();

        sb.AppendLine($"CKM mixing angles are INTERNAL (flavor) rotations: {ckm}");
        sb.AppendLine($"PMNS mixing angles are INTERNAL (flavor) rotations: {pmns}");
        sb.AppendLine($"gauge rotations are INTERNAL (gauge-space): {gauge}");
        sb.AppendLine($"geometric angles differ from internal rotations: {differ}");
        sb.AppendLine($"network angles DETERMINE mixing-angle values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: mixing and gauge angles are INTERNAL-space rotations, distinct from geometric triangle angles.");
        sb.AppendLine("The correspondence is an ANALOGY (both are angles), not an identification or derivation.");
        Output.WriteLine(sb.ToString());

        Assert.True(ckm, "CKM angles internal");
        Assert.True(pmns, "PMNS angles internal");
        Assert.True(gauge, "gauge rotations internal");
        Assert.True(differ, "geometric differs from internal");
        Assert.False(determines, "network angles do not determine values");
    }

    // ── TQMQG982: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG982_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG982: NO RELATION / PARTIAL RELATION / ANGLE ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {NetworkAngles.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: real geometric angles exist, and the angle analogy is structurally meaningful.");
        sb.AppendLine("  • NOT ANGLE ORIGIN: geometric angles and internal rotations live in different spaces; no native mapping");
        sb.AppendLine("    identifies them.");
        sb.AppendLine("  • PARTIAL RELATION: the correspondence is analogical (angles ↔ angles), not derivational.");
        sb.AppendLine();
        sb.AppendLine("So network angles give a PARTIAL RELATION to mixing angles (analogy across spaces, not angle origin).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", NetworkAngles.Classify());
        Assert.True(NetworkAngles.TriangleAnglesExist());
        Assert.True(NetworkAngles.GeometricAnglesDifferFromInternalRotations());
    }
}
