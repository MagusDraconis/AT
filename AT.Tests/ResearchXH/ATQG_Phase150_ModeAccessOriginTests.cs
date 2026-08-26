using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 150 — Origin of mode access. QG149 established sector exponents emerge from
/// occupation-weighted mode access. This phase asks WHY different sectors access different parts of the same
/// spectrum.
///
/// Tests: ATQG1500 (mode-selection rules + charge/isospin constraints), ATQG1501 (spectral accessibility),
/// ATQG1502 (occupation mechanisms + classification).
/// </summary>
public class ATQG_Phase150_ModeAccessOriginTests : ResearchTestBase
{
    public ATQG_Phase150_ModeAccessOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1500_ModeSelectionRulesAndQuantumConstraints()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1500: mode-selection rules and quantum-number constraints");

        sb.AppendLine("OCTAVE BAND STRUCTURE (mode-selection rules):");
        foreach (var (b, o, d) in ModeAccessOrigin.OctaveBandStructure())
            sb.AppendLine($"  band {b}: occupancy={o} modes, local Weyl δ={d:F3}");
        sb.AppendLine();
        sb.AppendLine("QUANTUM-NUMBER CONSTRAINTS ON THE EFFECTIVE DIMENSION:");
        sb.AppendLine($"  charge Q: r = {ModeAccessOrigin.ChargeConstraint():F3}");
        sb.AppendLine($"  isospin T3: r = {ModeAccessOrigin.IsospinConstraint():F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the spectrum offers distinct bands with very different occupancies, and");
        sb.AppendLine("the sector's effective dimension is quantum-number constrained (isospin r≈0.96).");
        Output.WriteLine(sb.ToString());

        var occ = ModeAccessOrigin.BandOccupancies();
        Assert.True(occ.Distinct().Count() >= 2, "multiple distinct band occupancies should exist");
        Assert.True(Math.Abs(ModeAccessOrigin.IsospinConstraint()) > 0.5, "isospin should constrain mode access");
        Assert.True(ModeAccessOrigin.TopBandFraction() > 0.8, "top band should dominate the occupancy");
    }

    [Fact]
    public void ATQG1501_SpectralAccessibility()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1501: spectral accessibility");

        double fullWeyl = ModeAccessOrigin.FullWeyl();
        double downDev = ModeAccessOrigin.DownFullSpectrumDeviation();
        bool downFull = ModeAccessOrigin.DownAccessesFullSpectrum();

        sb.AppendLine($"FULL-SPECTRUM WEYL δ = {fullWeyl:F3}");
        sb.AppendLine();
        sb.AppendLine("SECTOR EFFECTIVE DIMENSIONS (δ_eff = p_eff/2):");
        foreach (var (n, d, q, t3) in ModeAccessOrigin.SectorDimensions())
            sb.AppendLine($"  {n}: δ_eff={d:F3}  Q={q:F3}  T3={t3:F2}");
        sb.AppendLine();
        sb.AppendLine("DOWN SECTOR SPECTRAL ACCESSIBILITY:");
        sb.AppendLine($"  δ_eff(down) = 2.449 vs full Weyl = {fullWeyl:F3}");
        sb.AppendLine($"  deviation = {downDev:P2}");
        sb.AppendLine($"  down accesses the FULL spectrum: {downFull}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the down sector's effective dimension matches the full-spectrum Weyl");
        sb.AppendLine("exponent — it accesses the entire spectrum.");
        Output.WriteLine(sb.ToString());

        Assert.True(fullWeyl > 1.0, "full-spectrum Weyl should be well-defined");
        Assert.True(downDev < 0.05, "down should match the full-spectrum dimension");
        Assert.True(downFull, "down should access the full spectrum");
    }

    [Fact]
    public void ATQG1502_OccupationMechanismsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1502: occupation mechanisms and classification");

        double ratio = ModeAccessOrigin.UpDimensionalRatio();
        bool upDense = ModeAccessOrigin.UpAccessesDenseBand();
        int score = ModeAccessOrigin.OriginScore();
        string cls = ModeAccessOrigin.Classify();

        sb.AppendLine("OCCUPATION MECHANISMS:");
        sb.AppendLine($"  up δ_eff = 4.066 vs full Weyl = {ModeAccessOrigin.FullWeyl():F3}");
        sb.AppendLine($"  up/full ratio = {ratio:F3}");
        sb.AppendLine($"  up accesses the DENSE top band: {upDense}");
        sb.AppendLine($"  top-band occupancy fraction = {ModeAccessOrigin.TopBandFraction():F3}");
        sb.AppendLine();
        sb.AppendLine($"mode-access-origin score (0..5): {score}");
        sb.AppendLine($"  +1 distinct band occupancies: {ModeAccessOrigin.BandOccupancies().Distinct().Count() >= 2}");
        sb.AppendLine($"  +1 top band dominates: {ModeAccessOrigin.TopBandFraction() > 0.8}");
        sb.AppendLine($"  +1 down = full spectrum: {ModeAccessOrigin.DownAccessesFullSpectrum()}");
        sb.AppendLine($"  +1 up = dense band: {upDense}");
        sb.AppendLine($"  +1 isospin constrained: {Math.Abs(ModeAccessOrigin.IsospinConstraint()) > 0.5}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: clear spectral-access mechanisms exist.");
        sb.AppendLine("  • MODE-ACCESS ORIGIN accepted: sectors access different parts of the same spectrum");
        sb.AppendLine("    because occupation-weighted mode access is quantum-number constrained — down");
        sb.AppendLine("    accesses the full spectrum, up the dense band, selected by isospin (r≈0.96).");
        Output.WriteLine(sb.ToString());

        Assert.True(ratio > 1.3, "up should access the dense band (elevated dimension)");
        Assert.True(upDense, "up should access the dense top band");
        Assert.True(score >= 4, "mode-access-origin score should be strong");
        Assert.Equal("MODE-ACCESS ORIGIN", cls);
    }
}
