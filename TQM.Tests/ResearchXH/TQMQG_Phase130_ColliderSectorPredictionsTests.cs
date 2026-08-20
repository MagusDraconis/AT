using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 130 — Collider-accessible sector prediction. QG124-129 established that higher-energy
/// sectors exist, are metastable, and generate a predictive spectrum; QG129 showed partial electroweak
/// calibration. This phase asks which sector transitions are accessible within current and next-generation
/// collider energies.
///
/// Tests: TQMQG1300 (sector thresholds + ladder accessibility), TQMQG1301 (decay spectra + observable
/// signatures), TQMQG1302 (LHC/FCC reach + classification).
/// </summary>
public class TQMQG_Phase130_ColliderSectorPredictionsTests : ResearchTestBase
{
    public TQMQG_Phase130_ColliderSectorPredictionsTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1300_SectorThresholdsAndLadderAccessibility()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1300: sector thresholds and ladder accessibility");

        var thr = ColliderSectorPredictions.SectorThresholds();
        sb.AppendLine("SECTOR THRESHOLDS (dimensionless ceiling units):");
        foreach (double t in thr)
            sb.AppendLine($"  ceiling ≥ {t:F2}");
        sb.AppendLine($"threshold count = {thr.Length}");
        sb.AppendLine();
        sb.AppendLine("RUNG MASSES (Z-anchor linear calibration, GeV):");
        foreach (var (i, r, m) in ColliderSectorPredictions.RungMasses("Z"))
            sb.AppendLine($"  rung {i}: radius={r:F3} mass={m:F2} GeV");
        sb.AppendLine();
        sb.AppendLine("LADDER ACCESSIBILITY (Z anchor):");
        foreach (var (n, e) in ColliderSectorPredictions.Colliders)
            sb.AppendLine($"  {n} ({e} TeV): {ColliderSectorPredictions.AccessibleRungCount("Z", e)}/12 rungs, top accessible = {ColliderSectorPredictions.TopSectorAccessible("Z", e)}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: discrete sector thresholds define the ladder, and the calibrated sector");
        sb.AppendLine("masses fall within reach of modern hadron colliders.");
        Output.WriteLine(sb.ToString());

        Assert.True(thr.Length >= 3, "multiple discrete sector thresholds should exist");
        Assert.True(ColliderSectorPredictions.TopSectorAccessible("Z", 13.0), "top sector should be LHC13-accessible (Z anchor)");
        Assert.True(ColliderSectorPredictions.TopSectorAccessible("Z", 100.0), "top sector should be FCC-hh-accessible");
    }

    [Fact]
    public void TQMQG1301_DecaySpectraAndObservableSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1301: decay spectra and observable signatures");

        sb.AppendLine("DECAY SPECTRUM (Z anchor, emitted-quantum energies):");
        foreach (var (q, d, e) in ColliderSectorPredictions.DecaySpectrum("Z"))
            sb.AppendLine($"  {q} quantum: radius drop={d:F3} → energy={e:F2} GeV");
        sb.AppendLine();
        sb.AppendLine("OBSERVABLE SIGNATURES (metastable decay of an accessible sector):");
        sb.AppendLine($"  top-sector decay signature observable at LHC13: {ColliderSectorPredictions.DecaySignatureObservable("Z", 13.0)}");
        sb.AppendLine($"  top-sector decay signature observable at FCC-hh: {ColliderSectorPredictions.DecaySignatureObservable("Z", 100.0)}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: accessible high sectors decay (QG125 metastable) with quanta in the");
        sb.AppendLine("collider's energy range — the decay itself is an observable signature.");
        Output.WriteLine(sb.ToString());

        Assert.True(ColliderSectorPredictions.DecaySignatureObservable("Z", 13.0),
            "decay signature should be observable at LHC13");
        Assert.True(ColliderSectorPredictions.DecaySpectrum("Z").All(s => s.EnergyGeV > 0),
            "decay quanta should have positive energy");
    }

    [Fact]
    public void TQMQG1302_LhcFccReachAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1302: LHC/FCC reach and classification");

        sb.AppendLine("REACH SUMMARY (top rung mass per electroweak anchor):");
        foreach (var (a, top, lhc, fcc, frac) in ColliderSectorPredictions.ReachSummary())
            sb.AppendLine($"  anchor {a}: top={top:F2} GeV | LHC13={lhc} | FCC-hh={fcc} | fraction at LHC={frac:F3}");
        sb.AppendLine();
        sb.AppendLine("COLLIDER ENERGIES:");
        foreach (var (n, e) in ColliderSectorPredictions.Colliders)
            sb.AppendLine($"  {n}: {e} TeV");
        sb.AppendLine();
        int score = ColliderSectorPredictions.AccessibilityScore();
        sb.AppendLine($"accessibility score (0..5): {score}");
        sb.AppendLine($"CLASSIFICATION: {ColliderSectorPredictions.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT ACCESSIBLE rejected: sectors lie below LHC/FCC energies.");
        sb.AppendLine("  • ACCESSIBLE accepted: the highest-energy sectors fall within LHC13 and FCC-hh reach");
        sb.AppendLine("    for the whole electroweak calibration family, appearing as metastable decay");
        sb.AppendLine("    signatures rather than stable particles (QG125).");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "accessibility score should be strong");
        Assert.True(ColliderSectorPredictions.ReachSummary().All(r => r.Lhc13),
            "top sector should be LHC13-accessible for all electroweak anchors");
        Assert.Equal("ACCESSIBLE", ColliderSectorPredictions.Classify());
    }
}
