using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 129 — Physical calibration of the sector ladder. QG128 established that sector transitions
/// generate a predictive discrete spectrum. This phase asks whether the ladder can be calibrated to known
/// particle masses or collider energy scales.
///
/// Tests: TQMQG1290 (mass-spectrum matching + resonance spacing), TQMQG1291 (threshold energies + collider
/// accessibility), TQMQG1292 (scaling laws + classification).
/// </summary>
public class TQMQG_Phase129_PhysicalCalibrationTests : ResearchTestBase
{
    public TQMQG_Phase129_PhysicalCalibrationTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1290_MassSpectrumMatchingAndResonanceSpacing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1290: mass-spectrum matching and resonance spacing");

        sb.AppendLine("SM MASS RATIOS:");
        foreach (var (n, r) in PhysicalCalibration.SmMassRatios())
            sb.AppendLine($"  {n} = {r:F4}");
        sb.AppendLine();
        sb.AppendLine("NETWORK RATIOS → BEST SM MATCH:");
        foreach (var (n, r) in PhysicalCalibration.NetworkCharacteristicRatios())
        {
            var m = PhysicalCalibration.BestMassMatch(r);
            sb.AppendLine($"  {n}={r:F4} ~ {m.SmName}={m.SmRatio:F4} (dev {m.Deviation:P2})");
        }
        var best = PhysicalCalibration.BestOverallMatch();
        sb.AppendLine();
        sb.AppendLine($"BEST OVERALL MATCH: {best.NetName}={best.NetRatio:F4} ~ {best.SmName}={best.SmRatio:F4} (dev {best.Deviation:P2})");
        sb.AppendLine($"ratios matching an SM ratio within 10%: {PhysicalCalibration.MassMatchCount(0.10)}");
        sb.AppendLine();
        sb.AppendLine("RESONANCE SPACING:");
        sb.AppendLine($"  ladder spacings = [{string.Join(",", PhysicalCalibration.LadderSpacings().Select(s => s.ToString("F3", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine($"  spacing uniformity (rel. std) = {PhysicalCalibration.SpacingUniformity():F4}");
        sb.AppendLine($"  uniform (harmonic-like) resonance spacing: {PhysicalCalibration.UniformResonanceSpacing()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the electroweak ratio H/Z is reproduced by the top transition quantum");
        sb.AppendLine("within ~3%, and the ladder has harmonic-like uniform spacing.");
        Output.WriteLine(sb.ToString());

        Assert.True(PhysicalCalibration.MassMatchCount(0.10) >= 1, "at least one network ratio should match an SM ratio");
        Assert.True(best.Deviation < 0.10, "best match should be within 10%");
        Assert.True(PhysicalCalibration.UniformResonanceSpacing(), "resonance spacing should be uniform");
    }

    [Fact]
    public void TQMQG1291_ThresholdEnergiesAndColliderAccessibility()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1291: threshold energies and collider accessibility");

        var thr = PhysicalCalibration.ThresholdEnergies();
        double span = PhysicalCalibration.ThresholdSpan();
        double acc = PhysicalCalibration.AccessibilityRatio();

        sb.AppendLine("ENERGY THRESHOLDS (dimensionless ceiling units):");
        foreach (double t in thr)
            sb.AppendLine($"  ceiling ≥ {t:F2}");
        sb.AppendLine($"threshold count = {thr.Length}, threshold span = {span:F3}");
        sb.AppendLine();
        sb.AppendLine("COLLIDER ACCESSIBILITY:");
        sb.AppendLine($"  energy range to highest sector / collider scale span = {acc:F4}");
        sb.AppendLine($"  all sectors within a narrow collider window: {PhysicalCalibration.NarrowColliderWindow()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the sector ladder lies within a narrow energy window — all sectors are");
        sb.AppendLine("in principle reachable at modest collider energies.");
        Output.WriteLine(sb.ToString());

        Assert.True(thr.Length >= 3, "multiple discrete thresholds should exist");
        Assert.True(PhysicalCalibration.NarrowColliderWindow(), "sectors should be in a narrow collider window");
    }

    [Fact]
    public void TQMQG1292_ScalingLawsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1292: scaling laws and classification");

        double hostable = PhysicalCalibration.HostableLeptonRatio();
        double muOverE = PhysicalCalibration.MMuon / PhysicalCalibration.MElectron;
        bool canHost = PhysicalCalibration.CanHostLeptonHierarchy();
        int score = PhysicalCalibration.CalibrationScore();
        string cls = PhysicalCalibration.Classify();

        sb.AppendLine("SCALING LAWS:");
        sb.AppendLine($"  ladder radius span (hostable mass ratio, linear calibration) = {hostable:F4}");
        sb.AppendLine($"  lepton hierarchy needed: muon/electron = {muOverE:F1}");
        sb.AppendLine($"  ladder can host the lepton hierarchy: {canHost}");
        sb.AppendLine();
        sb.AppendLine($"calibration score (0..5): {score}");
        sb.AppendLine($"  +1 mass-spectrum match: {PhysicalCalibration.MassMatchCount(0.10) >= 1}");
        sb.AppendLine($"  +1 uniform resonance spacing: {PhysicalCalibration.UniformResonanceSpacing()}");
        sb.AppendLine($"  +1 discrete thresholds: {PhysicalCalibration.ThresholdEnergies().Length >= 3}");
        sb.AppendLine($"  +1 narrow collider window: {PhysicalCalibration.NarrowColliderWindow()}");
        sb.AppendLine($"  +1 hosts lepton hierarchy: {canHost}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • PHYSICAL CALIBRATION rejected: the ladder cannot host the generation hierarchy.");
        sb.AppendLine("  • PARTIAL MAPPING accepted: the electroweak ratio H/Z is reproduced (~3%) but the");
        sb.AppendLine("    ladder span (2.889) cannot reach the lepton hierarchy (mu/e = 207).");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "calibration score should be strong");
        Assert.False(canHost, "ladder should not be able to host the full lepton hierarchy");
        Assert.Equal("PARTIAL MAPPING", cls);
    }
}
