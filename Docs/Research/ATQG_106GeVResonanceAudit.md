# AT-QG Phase 188A — 106 GeV Resonance Evidence Audit

**Status:** COMPLETE — **INCONCLUSIVE**
**Tests:** ATQG188A0, ATQG188A1, ATQG188A2 (all passed)
**Core class:** `AT.Core/ResearchXH/ResonanceEvidenceAudit.cs`

---

## 1. Goal

QG132 predicts a primary resonance at **106.39 GeV** in the search window
**99–114 GeV** (Z-anchor electroweak calibration, robustness MODERATE per
QG133). This phase audits ALL existing published experimental evidence —
ATLAS, CMS, LEP — against that prediction, using only completed AT results,
no new theory, no fitting.

---

## 2. The Prediction

| Quantity | Value | Source |
|----------|-------|--------|
| Predicted mass | 106.39 GeV | QG132 (Z-anchor) |
| Search window | 99–114 GeV | QG132 |
| Robustness | MODERATE (Z/W anchors agree 0.74%) | QG133 |
| Lowest ladder rung | 91.19 GeV | QG130 |

---

## 3. Supporting Evidence (published excesses)

A persistent low-mass scalar excess cluster is observed near **~95 GeV**:

| Experiment | Channel | Mass | Local σ | Reference |
|-----------|---------|------|---------|-----------|
| CMS | γγ | 95.3 GeV | 2.9σ | CMS-HIG-20-002, PLB 860 (2025) |
| ATLAS | γγ | 95.4 GeV | 1.7σ | arXiv:2407.07546 |
| CMS | ττ | ~95 GeV | 2.6σ | CMS Run-2 |
| LEP | bb̄ | ~98 GeV | 2.3σ | LEP bb̄ excess |

**Combined γγ (ATLAS+CMS, neglecting correlations): 3.1σ local, μ = 0.24.**

**CRITICAL finding:** all four excesses are at ~95–98 GeV — **BELOW the
predicted 99–114 GeV window**. The 95.3 GeV excess is **−10.4%** from the
predicted 106.39 GeV but only **+4.5%** from the lowest ladder rung
(91.19 GeV, QG131's documented match). **The observed excess aligns with the
91.19 GeV rung, not with the 106 GeV prediction.**

---

## 4. Null Searches in the Predicted Window

| Experiment | Range | Result | Limits (95% CL) |
|-----------|-------|--------|-----------------|
| CMS | 70–110 GeV (γγ, full Run-2) | No excess beyond 95.4 GeV | 15–73 fb |
| ATLAS | 66–110 GeV (γγ, full Run-2) | "No significant deviation" | 19–102 fb |
| LEP2 | hZ, SM-like Higgs | Excluded below 114.4 GeV | SM-strength hZZ only |

The predicted mass 106.39 GeV is covered by both full-Run-2 diphoton null
searches, with no excess observed. In the 100–110 GeV range the limits are
≈20–50 fb.

---

## 5. Current Exclusion Status

- **NOT excluded.** The 106.39 GeV prediction is not ruled out:
  - Diphoton limits (≈20–50 fb in 100–110 GeV) still allow a
    suppressed-coupling sector-ladder scalar.
  - LEP2's 114.4 GeV bound applies to SM-strength hZZ coupling; a sector
    state with reduced coupling evades it.
- The prediction is a specific, searchable target that has NOT yet been
  excluded — but has also not been confirmed.

---

## 6. Discovery Potential (Run 3 → HL-LHC)

- **Run 3 (2022–2025):** no confirmed increase in the 95 GeV significance
  with early Run-3 data to date.
- The strongest hint (95 GeV γγ, 3.1σ combined) remains below the **5σ
  discovery threshold**.
- **HL-LHC (~late 2020s, ~5× luminosity):** the decisive experiment for the
  99–114 GeV window — with the sensitivity to confirm or exclude a
  suppressed-coupling scalar at 106 GeV.

---

## 7. Classification

Evidence score: **3/3**.

```
+1 supporting low-mass scalar excess cluster (3.1σ combined γγ, near 95 GeV)
+1 predicted window has no confirmed excess AND is not excluded
+1 excess aligns with the 91.19 GeV ladder rung, NOT the 106 GeV prediction

⇒ INCONCLUSIVE
```

- **SUPPORTED rejected:** no confirmed excess (≥5σ) appears at the predicted
  106 GeV itself.
- **DISFAVORED rejected:** the prediction is not excluded — the null
  searches set limits but leave suppressed-coupling states allowed.
- **INCONCLUSIVE accepted:** the experimental record shows a persistent
  low-mass scalar excess cluster at ~95 GeV (3.1σ combined), consistent with
  the **91.19 GeV ladder rung** (dev 4.0%, QG131) — but the specific
  **106 GeV prediction is neither confirmed nor excluded**. HL-LHC is
  decisive.

**Result: INCONCLUSIVE**

---

## 8. Interpretation & Caveats

- The 106 GeV prediction is a **different rung** from the ~95 GeV excess.
  The observed cluster supports a sector-ladder scalar at the lowest rung,
  not the primary predicted resonance. This is the most important caveat.
- The combined 3.1σ is below discovery threshold and could still be a
  statistical fluctuation or background mismodeling.
- Null searches in 66–110 GeV are powerful but coupling-model-dependent; the
  prediction survives because a suppressed hZZ/γγ coupling is allowed.
- This audit collects only the published experimental record; it does not
  change the QG132 prediction or add new theory.
- Recommended action: search the 99–114 GeV window (especially ~106 GeV)
  with HL-LHC data; monitor the 95 GeV excess as a separate sector-ladder
  signal at the 91.19 GeV rung.
