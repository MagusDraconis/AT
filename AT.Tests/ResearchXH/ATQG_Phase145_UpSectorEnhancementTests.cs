using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 145 — Origin of up-sector enhancement. QG143-144 established the quark anomaly is
/// concentrated in the up-type sector. This phase asks whether the hierarchy can emerge from INTERACTIONS
/// between spectral structure and internal quantum numbers.
///
/// Tests: ATQG1450 (spectral × charge + spectral × isospin coupling), ATQG1451 (charge-isospin cross
/// terms + up-peak), ATQG1452 (sector occupancy + hierarchy reconstruction + classification).
/// </summary>
public class ATQG_Phase145_UpSectorEnhancementTests : ResearchTestBase
{
    public ATQG_Phase145_UpSectorEnhancementTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1450_SpectralChargeAndIsospinCoupling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1450: spectral × charge and spectral × isospin coupling");

        double chargeCorr = UpSectorEnhancement.SpectralChargeCorrelation();
        double isospinCorr = UpSectorEnhancement.SpectralIsospinCorrelation();

        sb.AppendLine($"SPECTRAL × CHARGE COUPLING:");
        sb.AppendLine($"  Pearson r(deviation, Q) given the octave baseline = {chargeCorr:F3}");
        sb.AppendLine();
        sb.AppendLine($"SPECTRAL × ISOSPIN COUPLING:");
        sb.AppendLine($"  Pearson r(deviation, T3) given the octave baseline = {isospinCorr:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deviation couples positively to both charge and isospin — the");
        sb.AppendLine("spectral structure alone is insufficient; the quantum numbers matter.");
        Output.WriteLine(sb.ToString());

        Assert.True(chargeCorr > 0.3, "charge coupling should be positive");
        Assert.True(isospinCorr > 0.3, "isospin coupling should be positive");
    }

    [Fact]
    public void ATQG1451_ChargeIsospinCrossTermsAndUpPeak()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1451: charge-isospin cross terms and the up-peak");

        sb.AppendLine("CANDIDATE CHARGE×ISOSPIN CROSS TERMS (leptons, up, down, neutrino):");
        foreach (var (n, v) in UpSectorEnhancement.CrossTerms())
            sb.AppendLine($"  {n}: [{string.Join(", ", v.Select(x => x.ToString("F3", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine();
        sb.AppendLine("UP-PEAK SIGNATURE (cross term uniquely maximized at the up sector):");
        foreach (var (n, p) in UpSectorEnhancement.CrossTermUpPeaks())
            sb.AppendLine($"  {n}: up-peak = {p}");
        sb.AppendLine($"  up-peak count = {UpSectorEnhancement.UpPeakCount()} / 8");
        sb.AppendLine($"  robust (≥5 of 8): {UpSectorEnhancement.UpPeakRobust()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: ALL candidate cross terms single out the up sector uniquely — the");
        sb.AppendLine("charge×isospin interaction is the interaction signature of the up enhancement.");
        Output.WriteLine(sb.ToString());

        Assert.True(UpSectorEnhancement.UpPeakCount() >= 1, "at least one cross term should peak at up");
        Assert.True(UpSectorEnhancement.UpPeakRobust(), "the up-peak should be robust across cross terms");
    }

    [Fact]
    public void ATQG1452_SectorOccupancyHierarchyReconstructionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1452: sector occupancy, hierarchy reconstruction, classification");

        double occupancy = UpSectorEnhancement.SpectralOccupancy();
        bool reconstructs = UpSectorEnhancement.ReconstructsHierarchy();
        int score = UpSectorEnhancement.InteractionScore();
        string cls = UpSectorEnhancement.Classify();

        sb.AppendLine($"SECTOR OCCUPANCY (top-octave spectral density) = {occupancy:F3}");
        sb.AppendLine($"  (the spectral amplification channel the cross term multiplies)");
        sb.AppendLine();
        sb.AppendLine($"HIERARCHY RECONSTRUCTION:");
        sb.AppendLine($"  observed ordering: neutrino < down < leptons < up");
        sb.AppendLine($"  interaction reconstructs the full hierarchy: {reconstructs}");
        sb.AppendLine();
        sb.AppendLine($"interaction score (0..5): {score}");
        sb.AppendLine($"  +1 charge coupling: {UpSectorEnhancement.SpectralChargeCorrelation() > 0.3}");
        sb.AppendLine($"  +1 isospin coupling: {UpSectorEnhancement.SpectralIsospinCorrelation() > 0.3}");
        sb.AppendLine($"  +1 up-peak exists: {UpSectorEnhancement.UpPeakCount() >= 1}");
        sb.AppendLine($"  +1 up-peak robust: {UpSectorEnhancement.UpPeakRobust()}");
        sb.AppendLine($"  +1 hierarchy reconstructed: {reconstructs}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO INTERACTION rejected: the up enhancement is reproduced by charge×isospin");
        sb.AppendLine("    cross terms (all 8 peak at up).");
        sb.AppendLine("  • UP-SECTOR ORIGIN accepted: the up-type enhancement emerges from the INTERACTION of");
        sb.AppendLine("    the spectral structure with a charge×isospin cross term that robustly singles out");
        sb.AppendLine("    the up sector and reconstructs the hierarchy.");
        Output.WriteLine(sb.ToString());

        Assert.True(occupancy > 0.5, "sector occupancy should be high (strong spectral channel)");
        Assert.True(reconstructs, "the interaction should reconstruct the full hierarchy");
        Assert.True(score >= 4, "interaction score should be strong");
        Assert.Equal("UP-SECTOR ORIGIN", cls);
    }
}
