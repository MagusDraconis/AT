using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 143 — Origin of quark amplification. QG141/142 derived the lepton hierarchy from the
/// spectral law but quarks and neutrinos deviate. This phase asks what sector-dependent factor amplifies
/// quark and neutrino masses beyond the octave hierarchy.
///
/// Tests: ATQG1430 (deviation factors + color-sector effects), ATQG1431 (charge + isospin effects),
/// ATQG1432 (sector occupation + multi-sector coupling + classification).
/// </summary>
public class ATQG_Phase143_QuarkAmplificationTests : ResearchTestBase
{
    public ATQG_Phase143_QuarkAmplificationTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1430_DeviationFactorsAndColorSectorEffects()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1430: deviation factors and color-sector effects");

        sb.AppendLine($"OCTAVE-PREDICTED r31 = {QuarkAmplification.R31Octave:F1}");
        sb.AppendLine();
        sb.AppendLine("DEVIATION FACTORS (r31_observed / r31_octave):");
        foreach (var (n, f) in QuarkAmplification.DeviationFactors())
            sb.AppendLine($"  {n}: factor={f:F3}");
        sb.AppendLine();
        sb.AppendLine($"COLOR-SECTOR EFFECTS (quarks both color N=3):");
        sb.AppendLine($"  up/down factor ratio = {QuarkAmplification.ColorFactorRatio():F1}");
        sb.AppendLine($"  a single color factor explains both quarks: {QuarkAmplification.SingleColorFactor()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deviations are strongly sector-dependent, and color alone does NOT");
        sb.AppendLine("explain them (up and down, both color 3, differ by ~88×).");
        Output.WriteLine(sb.ToString());

        Assert.True(QuarkAmplification.ColorFactorRatio() > 3.0, "up/down deviation should differ strongly");
        Assert.False(QuarkAmplification.SingleColorFactor(), "color should not be the single factor");
        var factors = QuarkAmplification.DeviationFactors();
        Assert.True(factors.First(d => d.Name == "leptons").Factor < 1.5, "leptons should track the octave law");
        Assert.True(factors.First(d => d.Name == "up").Factor > 5.0, "up quarks should be strongly amplified");
    }

    [Fact]
    public void ATQG1431_ChargeAndIsospinEffects()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1431: charge-sector and isospin effects");

        double chargeCorr = QuarkAmplification.ChargeCorrelation();
        var iso = QuarkAmplification.IsospinAsymmetry();

        sb.AppendLine("CHARGE-SECTOR EFFECTS:");
        sb.AppendLine($"  Pearson r(deviation, |Q|) across sectors = {chargeCorr:F3}");
        sb.AppendLine();
        sb.AppendLine("ISOSPIN EFFECTS:");
        sb.AppendLine($"  up factor (T3=+1/2) = {iso.Up:F2}");
        sb.AppendLine($"  down factor (T3=-1/2) = {iso.Down:F2}");
        sb.AppendLine($"  up/down = {iso.UpOverDown:F1}");
        sb.AppendLine($"  isospin-signed amplification (up ↑, down ↓): {QuarkAmplification.IsospinSignedAmplification()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the amplification is isospin-signed — up-type is strongly amplified and");
        sb.AppendLine("down-type suppressed — while the charge correlation is only weak/moderate.");
        Output.WriteLine(sb.ToString());

        Assert.True(QuarkAmplification.IsospinSignedAmplification(),
            "amplification should be isospin-signed (up amplified, down suppressed)");
        Assert.True(iso.Up > 5.0, "up should be strongly amplified");
        Assert.True(iso.Down < 1.0, "down should be suppressed");
    }

    [Fact]
    public void ATQG1432_OccupationDensityAndMultiSectorCoupling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1432: sector occupation density, multi-sector coupling, classification");

        double density = QuarkAmplification.SectorOccupationDensity();
        double chargePower = QuarkAmplification.ImpliedChargePower();
        int score = QuarkAmplification.FactorScore();
        string cls = QuarkAmplification.Classify();

        sb.AppendLine($"SECTOR OCCUPATION DENSITY (top-octave fraction) = {density:F3}");
        sb.AppendLine();
        sb.AppendLine("MULTI-SECTOR COUPLING:");
        sb.AppendLine($"  implied charge-power exponent n (|Q_up|/|Q_down|)^n = up/down: {chargePower:F2}");
        sb.AppendLine($"  (a steep charge-power coupling; not a simple single factor)");
        sb.AppendLine();
        sb.AppendLine($"amplification-origin score (0..5): {score}");
        sb.AppendLine($"  +1 sector-dependent deviations: {QuarkAmplification.ColorFactorRatio() > 3.0}");
        sb.AppendLine($"  +1 color NOT the single factor: {!QuarkAmplification.SingleColorFactor()}");
        sb.AppendLine($"  +1 charge correlation: {QuarkAmplification.ChargeCorrelation() > 0.5}");
        sb.AppendLine($"  +1 isospin-signed: {QuarkAmplification.IsospinSignedAmplification()}");
        sb.AppendLine($"  +1 charge-power coupling: {chargePower > 3.0 && chargePower < 12.0}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • AMPLIFICATION ORIGIN rejected: the charge correlation is weak and no single");
        sb.AppendLine("    sector factor (color, charge, or isospin alone) reproduces all deviations.");
        sb.AppendLine("  • PARTIAL FACTOR accepted: the amplification is isospin-signed (up ↑, down ↓) with a");
        sb.AppendLine("    steep charge-power coupling (n≈6.5), but the factor is not fully determined.");
        Output.WriteLine(sb.ToString());

        Assert.True(QuarkAmplification.IsospinSignedAmplification(), "isospin-signed amplification should hold");
        Assert.True(chargePower > 3.0 && chargePower < 12.0, "charge-power coupling should be steep but bounded");
        Assert.True(score >= 4, "factor score should be strong");
        Assert.Equal("PARTIAL FACTOR", cls);
    }
}
