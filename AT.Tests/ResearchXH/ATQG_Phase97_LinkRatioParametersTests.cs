using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 97 — Parameter ratios from network geometry. Determines whether dimensionless ratios of link lengths
/// can determine physical parameters. Classify: NO RELATION / PARTIAL RELATION / RATIO ORIGIN.
///
/// Tests: ATQG970 (ratios + triangle geometry), ATQG971 (loop + mixing + mass analogs), ATQG972 (classification).
/// </summary>
public class ATQG_Phase97_LinkRatioParametersTests : ResearchTestBase
{
    public ATQG_Phase97_LinkRatioParametersTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG970: link-length ratios, triangle geometry ────────────────────────────

    [Fact]
    public void ATQG970_RatiosAndTriangles()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG970: do length ratios define angles?");

        bool dimensionless = LinkRatioParameters.RatiosAreDimensionless();
        bool triangle = LinkRatioParameters.TriangleAnglesFromRatios();

        sb.AppendLine($"dimensionless length ratios are scale-invariant: {dimensionless}");
        sb.AppendLine($"triangle geometry turns length ratios into angles: {triangle}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: physical parameters are dimensionless, and length RATIOS are exactly scale-invariant. Triangle");
        sb.AppendLine("geometry converts ratios into ANGLES — the natural dimensionless network observable.");
        Output.WriteLine(sb.ToString());

        Assert.True(dimensionless, "ratios are dimensionless");
        Assert.True(triangle, "triangle ratios give angles");
    }

    // ── ATQG971: loop geometry, mixing-angle / mass-hierarchy analogs ─────────────

    [Fact]
    public void ATQG971_LoopMixingMass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG971: loop phases, mixing angles, mass ratios");

        bool loop = LinkRatioParameters.LoopHolonomyGivesAngles();
        bool mixing = LinkRatioParameters.MixingAnglesAreNetworkAngles();
        bool mass = LinkRatioParameters.MassRatiosFromLengthRatios();
        bool determines = LinkRatioParameters.RatiosDetermineValues();

        sb.AppendLine($"loop holonomy turns ratios into dimensionless phases: {loop}");
        sb.AppendLine($"mixing angles literally ARE angles (network analog): {mixing}");
        sb.AppendLine($"mass hierarchies have a length-ratio analog: {mass}");
        sb.AppendLine($"ratios DETERMINE specific parameter values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: CKM/PMNS angles and mass ratios have DIRECT geometric analogs (triangle/loop angles, length");
        sb.AppendLine("ratios). But the network does not specify WHICH ratio corresponds to WHICH parameter — the values stay free.");
        Output.WriteLine(sb.ToString());

        Assert.True(loop, "loop phases exist");
        Assert.True(mixing, "mixing angles are network angles");
        Assert.True(mass, "mass ratios have an analog");
        Assert.False(determines, "ratios do not determine values");
    }

    // ── ATQG972: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG972_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG972: NO RELATION / PARTIAL RELATION / RATIO ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {LinkRatioParameters.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: length ratios, triangle/loop angles, and mass-ratio analogs are real geometric");
        sb.AppendLine("    correspondences.");
        sb.AppendLine("  • NOT RATIO ORIGIN: the network does not determine WHICH ratio gives WHICH parameter value.");
        sb.AppendLine("  • PARTIAL RELATION: the structural correspondence (angles → angles, ratios → ratios) is direct; the");
        sb.AppendLine("    specific mapping is not derived.");
        sb.AppendLine();
        sb.AppendLine("So dimensionless length ratios give a PARTIAL RELATION to parameters (direct analog, not ratio origin).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", LinkRatioParameters.Classify());
        Assert.True(LinkRatioParameters.MixingAnglesAreNetworkAngles());
        Assert.False(LinkRatioParameters.RatiosDetermineValues());
    }
}
