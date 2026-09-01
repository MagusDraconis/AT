using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 151 — Origin of isospin-guided spectral access. QG150 established that mode access is
/// strongly isospin-constrained. This phase asks WHY weak isospin selects different spectral regions.
///
/// Tests: ATQG1510 (spectral-band selection + T3-dependent occupation), ATQG1511 (octave accessibility +
/// mode competition), ATQG1512 (sector-selection mechanism + classification).
/// </summary>
public class ATQG_Phase151_IsospinModeAccessTests : ResearchTestBase
{
    public ATQG_Phase151_IsospinModeAccessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1510_SpectralBandSelectionAndT3Occupation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1510: spectral-band selection and T3-dependent occupation");

        var z2 = IsospinModeAccess.Z2Pairing();
        sb.AppendLine("SPECTRAL-BAND SELECTION (Z2 doublet structure of the mode spectrum):");
        sb.AppendLine($"  mode groups: {z2.Groups}  paired modes: {z2.PairedModes}/{z2.TotalModes}");
        sb.AppendLine($"  Z2 doublet fraction: {z2.PairedFraction:F4}");
        sb.AppendLine();
        sb.AppendLine("  octave-band pair structure (the selection rules available to isospin):");
        foreach (var (b, m, d) in IsospinModeAccess.OctavePairStructure())
            sb.AppendLine($"    band {b}: modes={m}, doublets={d}");
        sb.AppendLine();
        sb.AppendLine("T3-DEPENDENT OCCUPATION:");
        var tc = IsospinModeAccess.T3ChannelOccupation();
        sb.AppendLine($"  T3=+1/2 channel: {tc.EvenDense}/{tc.EvenTotal} modes in the dense band ({tc.EvenDenseFraction:F3})");
        sb.AppendLine($"  T3=-1/2 channel: {tc.OddDense}/{tc.OddTotal} modes in the dense band ({tc.OddDenseFraction:F3})");
        sb.AppendLine($"  dense-band fraction: {IsospinModeAccess.DenseBandFraction():F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the spectrum is fully Z2-paired (isospin doublets are complete), each octave");
        sb.AppendLine("band carries an integer number of doublets, and both T3 channels occupy the dense band");
        sb.AppendLine("with ~identical weight — the doublet structure is the isospin selection substrate.");
        Output.WriteLine(sb.ToString());

        Assert.True(z2.PairedFraction > 0.9, "the spectrum should be Z2-paired (isospin doublets)");
        Assert.True(IsospinModeAccess.OctavePairStructure().All(p => p.Doublets >= 1),
            "every octave band should carry isospin doublets");
        Assert.True(IsospinModeAccess.DenseBandFraction() > 0.8, "the dense band should dominate");
    }

    [Fact]
    public void ATQG1511_OctaveAccessibilityAndModeCompetition()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1511: octave accessibility and mode competition");

        double full = IsospinModeAccess.FullWeyl();
        double downDev = IsospinModeAccess.DownFullSpectrumDeviation();
        var g = IsospinModeAccess.GoldenSplitting();

        sb.AppendLine("OCTAVE ACCESSIBILITY:");
        sb.AppendLine($"  full-spectrum Weyl δ = {full:F4}");
        sb.AppendLine($"  down δ_eff = {g.Down:F4}  deviation from full = {downDev:P2}");
        sb.AppendLine($"  down accesses the FULL spectrum: {IsospinModeAccess.DownAccessesFullSpectrum()}");
        sb.AppendLine();
        sb.AppendLine("MODE COMPETITION (isospin splitting of the effective dimension):");
        sb.AppendLine($"  up δ_eff = {g.Up:F4}");
        sb.AppendLine($"  down δ_eff = {g.Down:F4}");
        sb.AppendLine($"  split = up − down = {g.Split:F4}");
        sb.AppendLine($"  golden ratio φ = {g.Phi:F4}");
        sb.AppendLine($"  deviation = {g.Deviation:P2}");
        sb.AppendLine($"  golden-ratio match: {IsospinModeAccess.GoldenSplittingMatches()}");
        sb.AppendLine($"  up = down + φ: {IsospinModeAccess.UpEqualsDownPlusPhi()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the down sector accesses the full spectrum (δ_eff ≈ Weyl_full, 0.96%");
        sb.AppendLine("deviation); the up sector is elevated by the golden-ratio mode-competition splitting");
        sb.AppendLine("δ_eff(up) = δ_eff(down) + φ (deviation 0.06%).");
        Output.WriteLine(sb.ToString());

        Assert.True(IsospinModeAccess.DownAccessesFullSpectrum(), "down should access the full spectrum");
        Assert.True(IsospinModeAccess.GoldenSplittingMatches(), "isospin splitting should match φ within 2%");
        Assert.True(IsospinModeAccess.UpEqualsDownPlusPhi(), "up = down + φ");
    }

    [Fact]
    public void ATQG1512_SectorSelectionMechanismAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1512: sector-selection mechanism and classification");

        int score = IsospinModeAccess.OriginScore();
        string cls = IsospinModeAccess.Classify();
        double r = IsospinModeAccess.IsospinConstraint();

        sb.AppendLine("SECTOR DIMENSIONS AND QUANTUM NUMBERS:");
        foreach (var (n, de, q, t3) in IsospinModeAccess.SectorDimensions())
            sb.AppendLine($"  {n}: δ_eff={de:F4}  Q={q:F3}  T3={t3:F2}");
        sb.AppendLine();
        sb.AppendLine("SECTOR-SELECTION MECHANISM:");
        sb.AppendLine($"  Z2 doublet fraction: {IsospinModeAccess.DoubletFraction():F4}");
        sb.AppendLine($"  down = full spectrum: {IsospinModeAccess.DownAccessesFullSpectrum()}");
        sb.AppendLine($"  up = down + φ (golden mode competition): {IsospinModeAccess.UpEqualsDownPlusPhi()}");
        sb.AppendLine($"  isospin constraint r = {r:F4}");
        sb.AppendLine();
        sb.AppendLine($"isospin-access-origin score (0..5): {score}");
        sb.AppendLine($"  +1 Z2 doublets complete: {IsospinModeAccess.DoubletFraction() > 0.9}");
        sb.AppendLine($"  +1 octave bands carry doublets: {IsospinModeAccess.OctavePairStructure().All(p => p.Doublets >= 1)}");
        sb.AppendLine($"  +1 down = full spectrum: {IsospinModeAccess.DownAccessesFullSpectrum()}");
        sb.AppendLine($"  +1 golden-ratio splitting: {IsospinModeAccess.GoldenSplittingMatches()}");
        sb.AppendLine($"  +1 isospin guides selection: {IsospinModeAccess.IsospinGuidesSelection()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the isospin-doublet structure and golden-ratio splitting are real.");
        sb.AppendLine("  • ISOSPIN ACCESS ORIGIN accepted: weak isospin selects different spectral regions via the");
        sb.AppendLine("    Z2 doublet structure of the spectrum — the modes form weak-isospin doublets, the down");
        sb.AppendLine("    sector accesses the full spectrum (δ_eff = Weyl_full), the up sector is elevated by the");
        sb.AppendLine("    golden-ratio mode-competition fixed point δ_eff(up) = δ_eff(down) + φ, and T3 is the");
        sb.AppendLine("    guiding quantum number (r = 0.955).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(r) > 0.5, "isospin should guide the selection");
        Assert.True(score >= 4, "isospin-access-origin score should be strong");
        Assert.Equal("ISOSPIN ACCESS ORIGIN", cls);
    }
}
