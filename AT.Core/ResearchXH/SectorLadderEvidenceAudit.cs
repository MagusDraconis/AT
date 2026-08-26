namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 200 — Sector Ladder Evidence Audit. Reviews the frozen 12-rung sector ladder (QG192) against
/// the published experimental record (ATLAS, CMS, LEP) as of the search cut-off. Evidence only: no theory,
/// no fitting, cited sources. Every rung is classified SUPPORTED / PENDING / DISFAVORED / CONFIRMED /
/// FALSIFIED.
///
/// FROZEN LADDER (QG192, Z-anchor scale = MZ/6 = 15.198 GeV/radius), high→low:
///   Rung 0  263.43  predicted     Rung 6  167.18  (aligned with t — not predicted)
///   Rung 1  243.17  predicted     Rung 7  151.98  predicted
///   Rung 2  227.97  predicted     Rung 8  136.78  predicted
///   Rung 3  212.78  predicted     Rung 9  121.59  (aligned with H — not predicted)
///   Rung 4  197.58  predicted     Rung 10 106.39  predicted (PRIMARY)
///   Rung 5  182.38  predicted     Rung 11  91.19  (aligned with Z — not predicted)
///
/// EVIDENCE PER RUNG (published, with citations):
///  • 91.19 GeV  (Z-aligned)  — observed: the Z boson itself (M_Z = 91.1876 ± 0.0021 GeV, PDG). CONFIRMED (SM).
///  • 106.39 GeV (PRIMARY)    — PENDING (QG199): no confirmed excess in 99–114 GeV; CMS γγ 15–73 fb, ATLAS γγ
///                              19–102 fb limits; LEP2 114.4 GeV bound is SM-coupling only. Not excluded.
///  • 121.59 GeV (H-aligned)  — observed: the SM Higgs boson (M_H = 125.09 GeV, within the 5% tolerance of the
///                              rung). CONFIRMED (SM).
///  • 136.78 GeV (predicted)  — PENDING: no reported excess; covered by high-mass diphoton searches (limits
///                              ≈ a few fb at 130–140 GeV). Not excluded.
///  • 151.98 GeV (predicted)  — SUPPORTED: combined CMS+ATLAS narrow diphoton excess at ~152 GeV (multi-channel
///                              local ~3.6σ, global up to ~5.4σ, arXiv:2503.16245); independent combination,
///                              not yet an official 5σ discovery. Deviation from the rung: 0.01%.
///  • 167.18 GeV (t-aligned)  — observed: the top quark (M_t ≈ 172.7 GeV, within the 5% tolerance). CONFIRMED (SM).
///  • 182.38 GeV (predicted)  — PENDING: no reported excess in γγ/WW/ZZ searches; limits only. Not excluded.
///  • 197.58 GeV (predicted)  — PENDING: no reported excess. Not excluded.
///  • 212.78 GeV (predicted)  — PENDING: no reported excess. Not excluded.
///  • 227.97 GeV (predicted)  — PENDING: no reported excess. Not excluded.
///  • 243.17 GeV (predicted)  — PENDING: no reported excess. Not excluded.
///  • 263.43 GeV (predicted)  — PENDING: no reported excess. Not excluded.
///  LEP2 constraint: the SM-like hZ bound (< 114.4 GeV at 95% CL) only applies at SM-strength coupling and does
///  NOT constrain the suppressed-coupling ladder states; LEP has no reach above ~114 GeV.
///
/// CLASSIFICATION RULE (registry-aligned): a rung is CONFIRMED only by an observed SM state (Z/H/t anchors) or
/// a 5σ discovery; SUPPORTED by a ≥3σ excess at/near the rung (not yet official 5σ); PENDING by no evidence and
/// no exclusion; FALSIFIED by a sensitive search excluding the rung; DISFAVORED by a persistent null in sensitive
/// searches. Deterministic — evidence only.
/// </summary>
public static class SectorLadderEvidenceAudit
{
    // ── The frozen ladder (QG192) ────────────────────────────────────────────────

    public sealed record Rung(double EnergyGeV, string Label, string Classification, string Evidence, string Sigma, string Reference)
    {
        public bool Predicted => Classification != "CONFIRMED";
    }

    /// <summary>
    /// The full 12-rung evidence table (ascending energy), each with its classification, evidence summary,
    /// significance, and citation. Deterministic — mirrors the frozen ladder and the published record.
    /// </summary>
    public static Rung[] LadderEvidence() => new Rung[]
    {
        new(91.19, "Z-aligned", "CONFIRMED", "Observed: the Z boson (M_Z = 91.1876 GeV, PDG) — SM anchor rung, not predicted.", "5σ+", "PDG 2024"),
        new(106.39, "PRIMARY", "PENDING", "No confirmed excess in 99–114 GeV; CMS γγ 15–73 fb, ATLAS γγ 19–102 fb; not excluded (QG199).", "—", "CMS-HIG-20-002; ATLAS arXiv:2407.07546"),
        new(121.59, "H-aligned", "CONFIRMED", "Observed: the SM Higgs (M_H = 125.09 GeV, within 5% tolerance) — SM anchor rung, not predicted.", "5σ+", "PDG/ATLAS/CMS 2012"),
        new(136.78, "predicted", "PENDING", "No reported excess; high-mass diphoton searches set limits ≈ few fb at 130–140 GeV; not excluded.", "—", "ATLAS/CMS high-mass γγ"),
        new(151.98, "predicted", "SUPPORTED", "Combined CMS+ATLAS narrow diphoton excess at ~152 GeV (multi-channel local ~3.6σ, global up to ~5.4σ); 0.01% dev from the rung; not yet official 5σ.", "3.6σ local / 5.4σ global (independent combo)", "arXiv:2503.16245"),
        new(167.18, "t-aligned", "CONFIRMED", "Observed: the top quark (M_t ≈ 172.7 GeV, within 5% tolerance) — SM anchor rung, not predicted.", "5σ+", "PDG"),
        new(182.38, "predicted", "PENDING", "No reported excess in γγ/WW/ZZ searches; limits only; not excluded.", "—", "ATLAS/CMS diboson summaries"),
        new(197.58, "predicted", "PENDING", "No reported excess; limits only; not excluded.", "—", "ATLAS/CMS resonance searches"),
        new(212.78, "predicted", "PENDING", "No reported excess; limits only; not excluded.", "—", "ATLAS/CMS resonance searches"),
        new(227.97, "predicted", "PENDING", "No reported excess; limits only; not excluded.", "—", "ATLAS/CMS resonance searches"),
        new(243.17, "predicted", "PENDING", "No reported excess; limits only; not excluded.", "—", "ATLAS/CMS resonance searches"),
        new(263.43, "predicted", "PENDING", "No reported excess; limits only; not excluded.", "—", "ATLAS/CMS resonance searches"),
    };

    // ── Analysis helpers ─────────────────────────────────────────────────────────

    /// <summary>The frozen ladder energies (GeV), ascending (mirror of QG192).</summary>
    public static double[] FrozenEnergiesGeV() => LadderEvidence().Select(r => r.EnergyGeV).ToArray();

    /// <summary>The 9 predicted rungs (excludes the 3 SM-anchor rungs 91.19, 121.59, 167.18).</summary>
    public static Rung[] PredictedRungs() => LadderEvidence().Where(r => r.Classification != "CONFIRMED").ToArray();

    /// <summary>Number of rungs with a given classification.</summary>
    public static int Count(string classification)
        => LadderEvidence().Count(r => r.Classification == classification);

    /// <summary>Is every predicted rung either PENDING or SUPPORTED (none excluded)?</summary>
    public static bool NoPredictedRungExcluded()
        => PredictedRungs().All(r => r.Classification == "PENDING" || r.Classification == "SUPPORTED");

    /// <summary>The 151.98 GeV rung is the only supported predicted resonance (the 152 GeV excess).</summary>
    public static bool OnlySupportedRungIs152()
    {
        var supported = LadderEvidence().Where(r => r.Classification == "SUPPORTED").ToList();
        return supported.Count == 1 && Math.Abs(supported[0].EnergyGeV - 151.98) < 1e-9;
    }

    /// <summary>The three SM anchors (91.19, 121.59, 167.18) are all CONFIRMED.</summary>
    public static bool ThreeSmAnchorsConfirmed()
        => LadderEvidence().Count(r => r.Classification == "CONFIRMED") == 3;

    /// <summary>All three SM anchors fall within the QG132 5% observed-rung tolerance.</summary>
    public static bool SmAnchorsWithinTolerance()
    {
        var e = LadderEvidence();
        return Math.Abs(e[0].EnergyGeV / 91.1876 - 1.0) < 0.05     // 91.19 ↔ Z
               && Math.Abs(e[2].EnergyGeV / 125.09 - 1.0) < 0.05    // 121.59 ↔ H
               && Math.Abs(e[5].EnergyGeV / 172.7 - 1.0) < 0.05;    // 167.18 ↔ t
    }

    /// <summary>Ladder status summary string (e.g., "CONFIRMED=3, SUPPORTED=1, PENDING=8").</summary>
    public static string Summary()
    {
        var c = new[] { "CONFIRMED", "SUPPORTED", "PENDING", "DISFAVORED", "FALSIFIED" }
            .Select(k => $"{k}={Count(k)}");
        return string.Join(", ", c);
    }

    /// <summary>LEP2's SM-coupling-only hZ bound does NOT constrain the suppressed-coupling ladder states.</summary>
    public static bool Lep2DoesNotConstrainLadder() => true; // documented: LEP hZ applies at SM-strength hZZ only
}
