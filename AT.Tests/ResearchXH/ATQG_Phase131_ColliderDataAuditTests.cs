using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 131 — Existing collider anomaly audit. QG127-130 predict metastable sector cascades and a
/// discrete spectrum. This phase audits whether ALREADY OBSERVED collider data contain structures
/// consistent with the sector ladder.
///
/// Tests: ATQG1310 (excess-event searches + cascade-like signatures), ATQG1311 (resonance clustering +
/// threshold structures), ATQG1312 (null-result consistency + classification).
/// </summary>
public class ATQG_Phase131_ColliderDataAuditTests : ResearchTestBase
{
    public ATQG_Phase131_ColliderDataAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1310_ExcessEventSearchesAndCascadeSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1310: excess-event searches and cascade-like signatures");

        sb.AppendLine("ANOMALY CANDIDATES vs nearest ladder rung:");
        foreach (var (n, e) in ColliderDataAudit.AnomalyCandidates)
        {
            var (r, d) = ColliderDataAudit.NearestRung(e);
            sb.AppendLine($"  {n} = {e:F1} GeV → rung {r:F2} GeV (dev {d:P2})");
        }
        var match = ColliderDataAudit.MatchingExcess();
        sb.AppendLine();
        sb.AppendLine($"matching excess within 10%: {(match.HasValue ? $"{match.Value.Name} at {match.Value.EnergyGeV:F1} GeV (dev {match.Value.Deviation:P2})" : "none")}");
        sb.AppendLine($"excess match count = {ColliderDataAudit.ExcessMatchCount()}");
        sb.AppendLine();
        sb.AppendLine("CASCADE-LIKE SIGNATURE (SM masses on distinct rungs):");
        foreach (var (n, m) in ColliderDataAudit.SmMasses)
        {
            var (r, d) = ColliderDataAudit.NearestRung(m);
            sb.AppendLine($"  {n} = {m:F2} GeV → rung {r:F2} GeV (dev {d:P2})");
        }
        sb.AppendLine($"cascade-like signature (≥3 masses on distinct rungs within 5%): {ColliderDataAudit.CascadeLikeSignature()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the documented ~95 GeV excess sits near the lowest ladder rung, and the");
        sb.AppendLine("electroweak masses form a ladder-like sequence on distinct rungs.");
        Output.WriteLine(sb.ToString());

        Assert.True(match.HasValue, "a documented excess candidate should match a ladder rung");
        Assert.True(match.Value.Deviation < 0.10, "matching excess should be within 10%");
        Assert.True(ColliderDataAudit.CascadeLikeSignature(), "electroweak masses should form a cascade-like ladder");
    }

    [Fact]
    public void ATQG1311_ResonanceClusteringAndThresholdStructures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1311: resonance clustering and threshold structures");

        sb.AppendLine("RESONANCE CLUSTERING (SM masses within 5% of a rung):");
        foreach (var (n, m, r, d) in ColliderDataAudit.ClusteredResonances())
            sb.AppendLine($"  {n} = {m:F2} GeV → rung {r:F2} GeV (dev {d:P2})");
        sb.AppendLine($"clustered resonance count = {ColliderDataAudit.ResonanceClusterCount()}");
        sb.AppendLine();
        sb.AppendLine("THRESHOLD STRUCTURES (pair-production thresholds within 5% of a rung):");
        foreach (var (n, t, r, d) in ColliderDataAudit.ClusteredThresholds())
            sb.AppendLine($"  {n} = {t:F1} GeV → rung {r:F2} GeV (dev {d:P2})");
        sb.AppendLine($"clustered threshold count = {ColliderDataAudit.ThresholdMatchCount()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: observed electroweak masses and pair-production thresholds cluster on");
        sb.AppendLine("sector-ladder rungs within a few percent.");
        Output.WriteLine(sb.ToString());

        Assert.True(ColliderDataAudit.ResonanceClusterCount() >= 2, "multiple SM masses should cluster on rungs");
        Assert.True(ColliderDataAudit.ThresholdMatchCount() >= 2, "multiple pair thresholds should cluster on rungs");
    }

    [Fact]
    public void ATQG1312_NullResultConsistencyAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1312: null-result consistency and classification");

        bool nullConsistent = ColliderDataAudit.NullResultsConsistent();
        int score = ColliderDataAudit.AuditScore();
        string cls = ColliderDataAudit.Classify();

        sb.AppendLine($"null results consistent with metastable sectors: {nullConsistent}");
        sb.AppendLine("  (QG125 METASTABLE ⇒ no stable new resonances predicted; LHC null results are");
        sb.AppendLine("  consistent — accessible sectors appear only as decay signatures, QG127/128)");
        sb.AppendLine();
        sb.AppendLine($"audit score (0..5): {score}");
        sb.AppendLine($"  +1 excess candidate on a rung: {ColliderDataAudit.ExcessMatchCount() >= 1}");
        sb.AppendLine($"  +1 cascade-like masses: {ColliderDataAudit.CascadeLikeSignature()}");
        sb.AppendLine($"  +1 resonance clustering: {ColliderDataAudit.ResonanceClusterCount() >= 2}");
        sb.AppendLine($"  +1 threshold structures: {ColliderDataAudit.ThresholdMatchCount() >= 2}");
        sb.AppendLine($"  +1 null results consistent: {nullConsistent}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO MATCH rejected: multiple observed structures align with the ladder.");
        sb.AppendLine("  • CONSISTENT SIGNATURE accepted: the 95 GeV excess, electroweak masses, and pair");
        sb.AppendLine("    thresholds all sit on sector-ladder rungs, and null results are consistent with");
        sb.AppendLine("    metastable sectors.");
        Output.WriteLine(sb.ToString());

        Assert.True(nullConsistent, "null results should be consistent with metastable sectors");
        Assert.True(score >= 4, "audit score should be strong");
        Assert.Equal("CONSISTENT SIGNATURE", cls);
    }
}
