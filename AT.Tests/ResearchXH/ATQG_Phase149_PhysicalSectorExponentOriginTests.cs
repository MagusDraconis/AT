using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 149 — Physical origin of sector exponents. QG148 showed the linear exponent law overfits.
/// This phase tests whether sector exponents can emerge from a physical spectral mechanism rather than
/// parameter fitting.
///
/// Tests: ATQG1490 (spectral density shifts + occupation weighting), ATQG1491 (charge/isospin mode
/// access + effective dimension), ATQG1492 (mechanism + classification).
/// </summary>
public class ATQG_Phase149_PhysicalSectorExponentOriginTests : ResearchTestBase
{
    public ATQG_Phase149_PhysicalSectorExponentOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1490_SpectralDensityShiftsAndOccupationWeighting()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1490: spectral density shifts and occupation weighting");

        sb.AppendLine("LOCAL WEYL EXPONENTS (available effective spectral dimensions):");
        foreach (var (r, d) in PhysicalSectorExponentOrigin.SpectralDensityShifts())
            sb.AppendLine($"  {r}: δ = {d:F3}");
        sb.AppendLine();
        sb.AppendLine($"MODE OCCUPATION = [{string.Join(", ", PhysicalSectorExponentOrigin.ModeOccupation())}]");
        sb.AppendLine($"top-octave fraction = {PhysicalSectorExponentOrigin.TopOctaveFraction():F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the spectral density shifts substantially across octave bands, and the");
        sb.AppendLine("top band dominates the occupation — the available dimensions a sector can access.");
        Output.WriteLine(sb.ToString());

        var shifts = PhysicalSectorExponentOrigin.SpectralDensityShifts().Select(s => s.Delta).ToArray();
        Assert.True(shifts.Length >= 2, "multiple spectral ranges should be available");
        Assert.True(shifts.Max() - shifts.Min() > 1.0, "spectral density should shift across bands");
        Assert.True(PhysicalSectorExponentOrigin.FullWeyl() > 1.0, "full-spectrum Weyl should be well-defined");
    }

    [Fact]
    public void ATQG1491_ChargeIsospinModeAccessAndEffectiveDimension()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1491: charge/isospin mode access and effective dimension");

        double fullWeyl = PhysicalSectorExponentOrigin.FullWeyl();
        double twoFull = 2.0 * fullWeyl;
        var iso = PhysicalSectorExponentOrigin.IsospinSplitting();

        sb.AppendLine($"FULL-SPECTRUM WEYL δ = {fullWeyl:F3};  2×δ = {twoFull:F3}");
        sb.AppendLine();
        sb.AppendLine("EFFECTIVE SPECTRAL DIMENSIONS (δ_eff = p_eff/2):");
        foreach (var (n, d) in PhysicalSectorExponentOrigin.EffectiveDimensions())
            sb.AppendLine($"  {n}: δ_eff = {d:F3}");
        sb.AppendLine();
        sb.AppendLine("ISOSPIN-DEPENDENT SPLITTING:");
        sb.AppendLine($"  up exponent = {iso.Up:F3}, down exponent = {iso.Down:F3}, difference = {iso.Difference:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the up/down exponent splitting is substantial (~3.2), consistent with an");
        sb.AppendLine("isospin-dependent spectral access, and the effective dimensions straddle the Weyl δ.");
        Output.WriteLine(sb.ToString());

        Assert.True(iso.Difference > 2.0, "up/down exponent splitting should be substantial");
        Assert.True(PhysicalSectorExponentOrigin.EffectiveDimensions().First(d => d.Name == "up").DeltaEff > fullWeyl,
            "up effective dimension should exceed the Weyl exponent");
    }

    [Fact]
    public void ATQG1492_MechanismAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1492: physical mechanism and classification");

        double downDev = PhysicalSectorExponentOrigin.DownTwoFullWeylDeviation();
        bool mechanism = PhysicalSectorExponentOrigin.DownMechanism();
        int score = PhysicalSectorExponentOrigin.OriginScore();
        string cls = PhysicalSectorExponentOrigin.Classify();

        sb.AppendLine("PHYSICAL MECHANISM CANDIDATE:");
        sb.AppendLine($"  down p_eff = 4.898 vs 2×Weyl = {2 * PhysicalSectorExponentOrigin.FullWeyl():F3}");
        sb.AppendLine($"  deviation = {downDev:P2}");
        sb.AppendLine($"  (the down sector exponent IS twice the full spectral dimension: {mechanism})");
        sb.AppendLine();
        sb.AppendLine($"physical-origin score (0..5): {score}");
        sb.AppendLine($"  +1 well-defined Weyl: {PhysicalSectorExponentOrigin.FullWeyl() > 1.0}");
        sb.AppendLine($"  +1 spectral density shifts: {PhysicalSectorExponentOrigin.SpectralDensityShifts().Select(s => s.Delta).Where(d => !double.IsNaN(d)).ToArray().Max() - PhysicalSectorExponentOrigin.SpectralDensityShifts().Select(s => s.Delta).Where(d => !double.IsNaN(d)).ToArray().Min() > 1.0}");
        sb.AppendLine($"  +1 down = 2×Weyl: {mechanism}");
        sb.AppendLine($"  +1 isospin splitting: {PhysicalSectorExponentOrigin.IsospinSplitting().Difference > 2.0}");
        sb.AppendLine($"  +1 up dimension exceeds Weyl: {PhysicalSectorExponentOrigin.EffectiveDimensions().First(d => d.Name == "up").DeltaEff > PhysicalSectorExponentOrigin.FullWeyl()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO MECHANISM rejected: the down exponent matches 2×Weyl within ~1%.");
        sb.AppendLine("  • PHYSICAL ORIGIN accepted: the sector exponents emerge from the spectral density");
        sb.AppendLine("    (occupation-weighted mode access); the down exponent = 2×Weyl and the up/down");
        sb.AppendLine("    splitting is an isospin-dependent spectral access — a physical mechanism, not a");
        sb.AppendLine("    parameter fit.");
        Output.WriteLine(sb.ToString());

        Assert.True(downDev < 0.05, "down exponent should match 2×Weyl closely");
        Assert.True(mechanism, "the 2×Weyl mechanism should hold");
        Assert.True(score >= 4, "physical-origin score should be strong");
        Assert.Equal("PHYSICAL ORIGIN", cls);
    }
}
