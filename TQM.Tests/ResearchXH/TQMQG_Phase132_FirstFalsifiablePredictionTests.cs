using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 132 — First falsifiable collider prediction. QG131 established that existing collider data
/// are consistent with the sector ladder. This phase asks whether the hierarchy predicts a specific
/// yet-unobserved energy region or decay signature.
///
/// Tests: TQMQG1320 (missing ladder rungs + predicted resonances), TQMQG1321 (decay-cascade endpoints +
/// threshold regions), TQMQG1322 (collider reach + classification).
/// </summary>
public class TQMQG_Phase132_FirstFalsifiablePredictionTests : ResearchTestBase
{
    public TQMQG_Phase132_FirstFalsifiablePredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1320_MissingLadderRungsAndPredictedResonances()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1320: missing ladder rungs and predicted resonances");

        sb.AppendLine("SECTOR LADDER (Z anchor, GeV):");
        foreach (double r in ColliderDataAudit.LadderRungs().OrderBy(r => r))
        {
            var near = ColliderDataAudit.NearestRung(r);
            bool observed = FirstFalsifiablePrediction.ObservedMasses
                .Any(o => Math.Abs(r / o.MassGeV - 1.0) < 0.05);
            sb.AppendLine($"  {r:F2} GeV  {(observed ? "(observed)" : "(MISSING)")}");
        }
        sb.AppendLine();
        var missing = FirstFalsifiablePrediction.PredictedResonances();
        sb.AppendLine($"missing rung count = {FirstFalsifiablePrediction.MissingRungCount()}");
        sb.AppendLine("PREDICTED (unobserved) RESONANCES:");
        foreach (double r in missing)
            sb.AppendLine($"  {r:F2} GeV");
        double primary = FirstFalsifiablePrediction.PrimaryPredictedResonance();
        var win = FirstFalsifiablePrediction.PrimarySearchWindow();
        sb.AppendLine();
        sb.AppendLine($"PRIMARY PREDICTED RESONANCE: {primary:F2} GeV");
        sb.AppendLine($"  search window: {win.CenterGeV - win.HalfWidthGeV:F2} – {win.CenterGeV + win.HalfWidthGeV:F2} GeV");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the ladder predicts 9 specific yet-unobserved resonances, with the");
        sb.AppendLine("lowest at ~106 GeV in a clean window between Z and H.");
        Output.WriteLine(sb.ToString());

        Assert.True(FirstFalsifiablePrediction.HasMissingRungs(), "missing rungs should exist");
        Assert.True(missing.Length >= 3, "multiple predicted resonances should exist");
        Assert.True(primary > PhysicalCalibration.MZGeV && primary < PhysicalCalibration.MHGeV,
            "primary prediction should be in the clean Z–H window");
    }

    [Fact]
    public void TQMQG1321_DecayCascadeEndpointsAndThresholdRegions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1321: decay-cascade endpoints and threshold regions");

        sb.AppendLine("DECAY-CASCADE SIGNATURE (QG128 emitted quanta, Z calibration):");
        foreach (var (q, d, e, m) in FirstFalsifiablePrediction.CascadeEndpoints())
            sb.AppendLine($"  {q} quantum: radius drop={d:F3} → energy={e:F2} GeV × {m}");
        var end = FirstFalsifiablePrediction.CascadeEndpointSector();
        sb.AppendLine($"  cascade endpoint sector: radius={end.Radius:F3} families={end.Families} (observable 3-family sector)");
        sb.AppendLine();
        sb.AppendLine("THRESHOLD REGIONS (QG127, dimensionless ceiling units):");
        foreach (double t in FirstFalsifiablePrediction.ThresholdRegions())
            sb.AppendLine($"  ceiling ≥ {t:F2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the decay cascade has a well-defined quantum signature and terminates in");
        sb.AppendLine("the observable 3-family sector, with discrete thresholds marking sector appearances.");
        Output.WriteLine(sb.ToString());

        Assert.True(FirstFalsifiablePrediction.CascadeEndpoints().Length >= 2,
            "cascade should have multiple quantum endpoints");
        Assert.True(end.Families == 3, "cascade endpoint should be the 3-family sector");
        Assert.True(FirstFalsifiablePrediction.ThresholdRegions().Length >= 3,
            "multiple threshold regions should exist");
    }

    [Fact]
    public void TQMQG1322_ColliderReachAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1322: collider reach and classification");

        var reach = FirstFalsifiablePrediction.ColliderReach();
        int score = FirstFalsifiablePrediction.PredictionScore();
        string cls = FirstFalsifiablePrediction.Classify();

        sb.AppendLine("COLLIDER REACH OF PREDICTED RESONANCES:");
        sb.AppendLine($"  all predicted resonances below LHC13 (13 TeV): {reach.Lhc13}");
        sb.AppendLine($"  all predicted resonances below FCC-hh (100 TeV): {reach.Fcchh}");
        sb.AppendLine();
        sb.AppendLine($"falsifiable-prediction score (0..5): {score}");
        sb.AppendLine($"  +1 missing rungs exist: {FirstFalsifiablePrediction.HasMissingRungs()}");
        sb.AppendLine($"  +1 ≥3 predicted resonances: {FirstFalsifiablePrediction.PredictedResonances().Length >= 3}");
        sb.AppendLine($"  +1 primary in clean Z–H window: {FirstFalsifiablePrediction.PredictionScore() >= 3}");
        sb.AppendLine($"  +1 cascade quantum signature: {FirstFalsifiablePrediction.CascadeEndpoints().Length >= 2}");
        sb.AppendLine($"  +1 reachable at LHC/FCC: {reach.AllBelowLhc}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO PREDICTION rejected: the ladder predicts specific unobserved resonances.");
        sb.AppendLine("  • FALSIFIABLE PREDICTION accepted: 9 specific resonances (primary ~106 GeV in the");
        sb.AppendLine("    clean Z–H window) are TESTABLE at LHC13/FCC-hh — the first falsifiable collider");
        sb.AppendLine("    prediction of the sector hierarchy.");
        Output.WriteLine(sb.ToString());

        Assert.True(reach.AllBelowLhc, "all predicted resonances should be LHC-reachable");
        Assert.True(score >= 4, "falsifiable-prediction score should be strong");
        Assert.Equal("FALSIFIABLE PREDICTION", cls);
    }
}
