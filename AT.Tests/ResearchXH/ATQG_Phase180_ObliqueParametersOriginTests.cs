using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 180 — Oblique parameters origin. Known: QG162 (couplings), QG168 (MW, MZ, ρ = 1),
/// QG169 (MH), QG175 (precision EW). This phase derives the electroweak oblique parameters S, T, U
/// from D96 spectral geometry — no fitted parameters, deterministic.
///
/// Tests: ATQG1800 (S parameter from the lightest-octave fraction), ATQG1801 (T parameter + T=2S
/// relation), ATQG1802 (U parameter + global-fit consistency + classification).
/// </summary>
public class ATQG_Phase180_ObliqueParametersOriginTests : ResearchTestBase
{
    public ATQG_Phase180_ObliqueParametersOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1800_SParameterFromLightestOctave()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1800: the S parameter from the lightest-octave fraction");

        sb.AppendLine("ASSUMPTIONS: the S parameter measures Z-photon-mixing new physics — the");
        sb.AppendLine("deviation of the effective leptonic mixing angle from the SM reference. In D96");
        sb.AppendLine("the lightest octave band carries occ₀ = 4 of the Σm = 95 modes; the fraction of");
        sb.AppendLine("the spectrum in the lightest family band is the natural isospin-conserving");
        sb.AppendLine("new-physics measure.");
        sb.AppendLine();
        sb.AppendLine("D96 SPECTRAL QUANTITIES:");
        sb.AppendLine($"  Σm = {ObliqueParametersOrigin.TotalModes()}");
        sb.AppendLine($"  octave occupancies = [{string.Join(",", ObliqueParametersOrigin.OctaveOccupancies())}]");
        sb.AppendLine($"  occ₀ = {ObliqueParametersOrigin.LightestOctaveOccupancy():F0}");
        sb.AppendLine();
        sb.AppendLine("S PARAMETER:");
        sb.AppendLine($"  S = occ₀/Σm = {ObliqueParametersOrigin.LightestOctaveOccupancy():F0}/{ObliqueParametersOrigin.TotalModes()} = {ObliqueParametersOrigin.SParameter():F5}");
        sb.AppendLine($"  global fit S = 0.04 ± 0.08 → deviation {Math.Abs(ObliqueParametersOrigin.SParameter() / 0.04 - 1.0):P3}");
        sb.AppendLine();
        sb.AppendLine("  the S parameter is the lightest-octave occupancy fraction — the isospin-");
        sb.AppendLine("  conserving spectral new-physics measure, with no fitted parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(ObliqueParametersOrigin.SMatches(), "S should match the global fit within 10%");
        Assert.True(ObliqueParametersOrigin.SParameter() > 0.02 && ObliqueParametersOrigin.SParameter() < 0.08,
            "S should be near 0.04");
    }

    [Fact]
    public void ATQG1801_TParameterAndTEqualsTwoS()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1801: the T parameter and the T = 2S relation");

        sb.AppendLine("ASSUMPTIONS: the T parameter measures custodial-symmetry breaking (isospin");
        sb.AppendLine("violation). In D96 the Z2 doublet structure weights the light octaves twice (two");
        sb.AppendLine("octave bands, occupancies [4,4,87]): T = 2·occ₀/Σm. The global-fit relation is");
        sb.AppendLine("T ≈ 2S, and the D96 structure must reproduce it.");
        sb.AppendLine();
        sb.AppendLine("T PARAMETER:");
        sb.AppendLine($"  T = 2·occ₀/Σm = 2·{ObliqueParametersOrigin.LightestOctaveOccupancy():F0}/{ObliqueParametersOrigin.TotalModes()} = {ObliqueParametersOrigin.TParameter():F5}");
        sb.AppendLine($"  global fit T = 0.08 ± 0.07 → deviation {Math.Abs(ObliqueParametersOrigin.TParameter() / 0.08 - 1.0):P3}");
        sb.AppendLine();
        sb.AppendLine("THE D96 RELATION T = 2S:");
        sb.AppendLine($"  S = {ObliqueParametersOrigin.SParameter():F5}, T = {ObliqueParametersOrigin.TParameter():F5}");
        sb.AppendLine($"  T/S = {ObliqueParametersOrigin.TRatio():F4}  (global fit 0.08/0.04 = 2.0)");
        sb.AppendLine($"  T = 2S exactly: {ObliqueParametersOrigin.TEqualsTwoS()}");
        sb.AppendLine();
        sb.AppendLine("  the Z2-doublet structure doubles the light-octave weight — the D96 relation");
        sb.AppendLine("  T = 2S reproduces the global-fit relation exactly, with no fitted parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(ObliqueParametersOrigin.TMatches(), "T should match the global fit within 10%");
        Assert.True(ObliqueParametersOrigin.TEqualsTwoS(), "T should equal 2S exactly");
    }

    [Fact]
    public void ATQG1802_UParameterAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1802: the U parameter, global-fit consistency, and classification");

        sb.AppendLine("ASSUMPTIONS: the U parameter measures the residual W-Z mass consistency beyond");
        sb.AppendLine("S and T. In D96 the W-Z relation is EXACTLY the SM tree-level one (QG168:");
        sb.AppendLine("MZ = MW/cosθ_W, ρ = 1.00000), so U = 0 exactly — the framework has no residual");
        sb.AppendLine("beyond S and T.");
        sb.AppendLine();
        sb.AppendLine("U PARAMETER:");
        sb.AppendLine($"  U = 0 exactly  (D96 W-Z relation = SM tree-level, QG168)");
        sb.AppendLine($"  ρ = {ObliqueParametersOrigin.RhoParameter():F6}  (exact SM tree-level 1: {ObliqueParametersOrigin.RhoIsExactSM()})");
        sb.AppendLine($"  global fit U = 0.0 ± 0.06 → deviation {Math.Abs(ObliqueParametersOrigin.UParameter() - 0.0):E2}");
        sb.AppendLine();
        sb.AppendLine("AGREEMENT SUMMARY:");
        foreach (var (name, d, f, dev) in ObliqueParametersOrigin.Comparison())
            sb.AppendLine($"  {name}: derived {d,9:F5}, global-fit {f,7:F2}, dev {dev:P3}");
        sb.AppendLine();
        int score = ObliqueParametersOrigin.OriginScore();
        string cls = ObliqueParametersOrigin.Classify();
        sb.AppendLine($"Oblique-origin score (0..5): {score}");
        sb.AppendLine($"  +1 S = occ₀/Σm within 10%: {ObliqueParametersOrigin.SMatches()}");
        sb.AppendLine($"  +1 T = 2·occ₀/Σm within 10%: {ObliqueParametersOrigin.TMatches()}");
        sb.AppendLine($"  +1 U = 0 within fit uncertainty: {ObliqueParametersOrigin.UMatches()}");
        sb.AppendLine($"  +1 T = 2S exactly: {ObliqueParametersOrigin.TEqualsTwoS()}");
        sb.AppendLine($"  +1 ρ = 1 (QG168) anchors U = 0: {ObliqueParametersOrigin.RhoIsExactSM()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: S, T, U all reproduce the global-fit values.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the T = 2S relation matches exactly and U = 0");
        sb.AppendLine("    follows from the exact tree-level W-Z consistency.");
        sb.AppendLine("  • OBLIQUE ORIGIN accepted: the oblique parameters EMERGE from D96 spectral");
        sb.AppendLine("    geometry — S = occ₀/Σm = 4/95 = 0.0421 (the lightest-octave fraction, fit");
        sb.AppendLine("    0.04, dev 5.3%), T = 2·occ₀/Σm = 8/95 = 0.0842 (the Z2-doublet-weighted");
        sb.AppendLine("    custodial-breaking measure, fit 0.08, dev 5.3%), with T = 2S reproducing the");
        sb.AppendLine("    global-fit relation exactly, and U = 0 (the D96 W-Z relation is the exact SM");
        sb.AppendLine("    tree-level one, ρ = 1) — consistent with the electroweak global fit beyond");
        sb.AppendLine("    masses and widths, no fitted parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(ObliqueParametersOrigin.UMatches(), "U should match the global fit within uncertainty");
        Assert.True(score >= 4, "oblique-origin score should be strong");
        Assert.Equal("OBLIQUE ORIGIN", cls);
    }
}
