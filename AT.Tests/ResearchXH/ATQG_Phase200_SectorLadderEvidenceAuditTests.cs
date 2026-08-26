using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 200 — Sector Ladder Evidence Audit. Reviews the frozen 12-rung sector ladder (QG192) against
/// the published ATLAS/CMS/LEP record. Evidence only, cited, deterministic. Each rung classified
/// SUPPORTED / PENDING / DISFAVORED / CONFIRMED / FALSIFIED.
/// </summary>
public class ATQG_Phase200_SectorLadderEvidenceAuditTests : ResearchTestBase
{
    public ATQG_Phase200_SectorLadderEvidenceAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2000_FullLadderEvidenceTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2000: the frozen ladder vs published evidence (all 12 rungs)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Frozen ladder = QG192 (Z-anchor scale MZ/6 = 15.198 GeV/radius).");
        sb.AppendLine("  - Evidence only, cited; no theory, no fitting.");
        sb.AppendLine();

        var rungs = SectorLadderEvidenceAudit.LadderEvidence();
        sb.AppendLine("LADDER EVIDENCE TABLE (ascending energy):");
        sb.AppendLine("  Rung     E [GeV]  Classification  Sigma               Evidence");
        foreach (var r in rungs)
        {
            sb.AppendLine($"  {r.EnergyGeV,7:F2}  {r.Label,-11} {r.Classification,-13} {r.Sigma,-19}  {r.Evidence}");
        }
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Predicted rungs (9): {SectorLadderEvidenceAudit.PredictedRungs().Length}");
        sb.AppendLine($"  SM anchors confirmed (3): {SectorLadderEvidenceAudit.ThreeSmAnchorsConfirmed()}");
        sb.AppendLine($"  Ladder summary: {SectorLadderEvidenceAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The three SM anchors (91.19 Z, 121.59 H, 167.18 t) are CONFIRMED as observed SM states.");
        sb.AppendLine("  - The 151.98 rung is SUPPORTED by the combined ~152 GeV excess (arXiv:2503.16245).");
        sb.AppendLine("  - All other predicted rungs are PENDING (no evidence, no exclusion).");

        Output.WriteLine(sb.ToString());

        Assert.Equal(12, rungs.Length);
        Assert.True(SectorLadderEvidenceAudit.ThreeSmAnchorsConfirmed(), "Z/H/t anchors must be CONFIRMED");
        Assert.Equal(9, SectorLadderEvidenceAudit.PredictedRungs().Length);
    }

    [Fact]
    public void ATQG2001_ClassificationDistribution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2001: classification distribution across the ladder");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - CONFIRMED: observed SM anchor or 5σ discovery;  SUPPORTED: ≥3σ excess at/near rung;");
        sb.AppendLine("    PENDING: no evidence and no exclusion;  FALSIFIED: sensitive search excludes rung;");
        sb.AppendLine("    DISFAVORED: persistent null in sensitive searches.");
        sb.AppendLine();

        string summary = SectorLadderEvidenceAudit.Summary();
        bool noneExcluded = SectorLadderEvidenceAudit.NoPredictedRungExcluded();
        bool only152 = SectorLadderEvidenceAudit.OnlySupportedRungIs152();
        bool anchorsOk = SectorLadderEvidenceAudit.SmAnchorsWithinTolerance();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Distribution: {summary}");
        sb.AppendLine($"  No predicted rung excluded (PENDING or SUPPORTED only)? {noneExcluded}");
        sb.AppendLine($"  Only supported predicted rung is 151.98 (the 152 GeV excess)? {only152}");
        sb.AppendLine($"  SM anchors within QG132 5% tolerance? {anchorsOk}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - CONFIRMED 3 (SM anchors), SUPPORTED 1 (151.98), PENDING 8 (all other predicted).");
        sb.AppendLine("  - No predicted rung is FALSIFIED or DISFAVORED: the ladder survives current data.");
        sb.AppendLine("  - The single supported resonance is the 152 GeV excess at the 151.98 rung (0.01% dev).");

        Output.WriteLine(sb.ToString());

        Assert.True(noneExcluded, "no predicted rung may be excluded by current data");
        Assert.True(only152, "only the 151.98 rung is supported");
        Assert.True(anchorsOk, "SM anchors must lie within the 5% tolerance");
        Assert.Equal(3, SectorLadderEvidenceAudit.Count("CONFIRMED"));
        Assert.Equal(1, SectorLadderEvidenceAudit.Count("SUPPORTED"));
        Assert.Equal(8, SectorLadderEvidenceAudit.Count("PENDING"));
    }

    [Fact]
    public void ATQG2002_ConsistencyWithQg192AndLep2()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2002: consistency with the frozen ladder and the LEP2 caveat");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The frozen ladder energies match QG192 to the quoted precision.");
        sb.AppendLine("  - LEP2's hZ bound (< 114.4 GeV, 95% CL) applies only at SM-strength hZZ coupling.");
        sb.AppendLine();

        var frozen = SectorLadderEvidenceAudit.FrozenEnergiesGeV();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Frozen energies: {string.Join(", ", frozen.Select(e => e.ToString("F2", CultureInfo.InvariantCulture)))}");
        sb.AppendLine($"  LEP2 SM-coupling-only bound constrains the suppressed-coupling ladder? {SectorLadderEvidenceAudit.Lep2DoesNotConstrainLadder()}");
        sb.AppendLine($"  Ladder summary: {SectorLadderEvidenceAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The audited ladder is exactly the QG192 frozen spectrum.");
        sb.AppendLine("  - LEP2 cannot exclude the suppressed-coupling ladder states (SM-coupling assumption only).");
        sb.AppendLine("  - Current status: CONFIRMED 3 + SUPPORTED 1 + PENDING 8 — no rung falsified.");

        Output.WriteLine(sb.ToString());

        Assert.True(SectorLadderEvidenceAudit.Lep2DoesNotConstrainLadder(), "LEP2 SM-coupling bound must not constrain the ladder");
        Assert.True(SectorLadderEvidenceAudit.Count("FALSIFIED") == 0 && SectorLadderEvidenceAudit.Count("DISFAVORED") == 0);
    }
}
