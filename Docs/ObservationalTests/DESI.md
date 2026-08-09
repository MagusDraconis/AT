# DESI — TQM Observational Status Report

**Experiment:** Dark Energy Spectroscopic Instrument
**Agency / Collaboration:** DOE / Lawrence Berkeley National Laboratory + international partners
**Location:** Kitt Peak National Observatory, Arizona, USA
**Document Version:** V1 — August 2026

---

## 1. Experiment Overview

| Field | Detail |
|-------|--------|
| **Full Name** | Dark Energy Spectroscopic Instrument |
| **Agency / Collaboration** | LBNL (DOE) + ~500 collaborators, ~70 institutions |
| **Scientific Goal** | Measure the expansion history of the universe via BAO (Baryon Acoustic Oscillations) using 40 million galaxy and quasar spectra. Constrain dark energy equation of state w(z) and growth of structure fσ₈(z). |
| **Operational Status** | Survey operations began May 2021. Completed 5-year survey. First data release (DR1): June 2023. Early cosmology results: 2024. |
| **Major Data Releases** | DR1: 2023 (validation + first year). DR2: 2025. Final cosmology: 2025–2026. |
| **Relevance to TQM** | DESI provides the first high-precision BAO constraints on w(z) and H(z) at z ~ 0.5–3.5. TQM predicts w(z) ≠ −1 and H(z) systematically higher than ΛCDM at moderate redshifts. DESI is the earliest test of the time-varying Λ prediction, preceding Euclid's full cosmological results. |
| **TQM Priority** | **HIGH — First BAO test of Λ(t).** |

---

## 2. TQM Predictions Tested

| Prediction | Program | Confidence | Observable | Expected Signal |
|-----------|:--:|:--:|------------|-----------------|
| **P1: w(z) ≠ −1** | XD001 | Medium-High | w(z) from BAO + external data (Planck + SNe) | w(z) ≈ −1 + 0.015·(1+z)^(3/2) |
| **P2: H(z) higher at z > 0** | XD001 | Medium | H(z) from BAO peak position at multiple redshifts | H(z) systematically 1–3% higher than ΛCDM prediction at z ~ 0.5–3 |
| Growth of structure fσ₈(z) | XD002 | Medium | Redshift-space distortions | fσ₈(z) 2–3% lower than ΛCDM at z ~ 0.5–1 |
| Log-normal scatter (statistical) | XD001 | Low-Medium | Consistency of w(z) across independent redshift bins | Underlying Λ values should show intrinsic scatter consistent with log-normal distribution |

---

## 3. Current Observational Status

| Prediction | Current Measurement | Uncertainty | TQM Expectation | Agreement |
|-----------|---------------------|-------------|-----------------|:--:|
| w(z) — DESI DR1 + Planck + SNe | w₀ = −0.94 ± 0.08, wₐ = −0.4 ± 0.4 (CPL parameterization) | σ(w₀) ≈ 0.08 | w(z) ≈ −0.985 (z=0) | **Consistent** (large uncertainty) |
| w(z) — DESI + Planck + SNe (constant w) | w = −0.997 ± 0.019 | σ ≈ 0.02 | w ≈ −0.985 | **Consistent** (within 1σ) |
| H(z) at z ~ 0.5–2 | Consistent with ΛCDM | ~2–4% | 1–3% higher | **Consistent** (uncertainty too large) |
| fσ₈(z) | Consistent with ΛCDM | ~5–10% | 2–3% lower | **Consistent** |

**Status Summary:** DESI's early results are consistent with both ΛCDM and TQM. Current uncertainties on w(z) are approximately 2–4× larger than the predicted deviation. DESI alone cannot reach the decisive threshold — Euclid + Roman are needed for σ(w) ≈ 0.01. However, DESI BAO provides the first systematic high-precision H(z) measurements and complements Euclid's weak lensing with an independent probe.

---

## 4. Falsification Criteria

| Outcome | Threshold | TQM Status |
|---------|:--:|------------|
| **A: Supports TQM** | H(z) systematically higher than ΛCDM at z > 0.5; w(z) best-fit > −1 | First indications consistent. |
| **B: Creates tension** | H(z) matches ΛCDM precisely at all z; no hint of deviation | Tension noted. Wait for Euclid + Roman. |
| **C: Requires revision** | Combined DESI + Euclid + Roman show w(z) = −1.000 at >3σ | See Euclid protocol. |
| **D: Falsifies sector** | (Cannot — DESI alone lacks precision for decisive falsification) | — |

---

## 5. Impact Analysis

### If Predictions Succeed

| Tier | Impact |
|:--:|--------|
| **Tier 4** | H(z) deviation provides first hint of non-ΛCDM expansion history. Consistent with Λ(t). |
| **Tier 3** | Early support for abundance framework. |

### If Predictions Fail (H(z) matches ΛCDM)

| Tier | Impact |
|:--:|--------|
| **Tier 4** | No early support. Wait for Euclid + Roman for definitive test. |
| **Tier 0-3** | Unaffected. |

**Note:** DESI is an EARLY TEST. It cannot falsify the Λ(t) prediction alone due to limited precision. Its primary value is providing the first high-precision BAO data to combine with Euclid and Roman.

---

## 6. Evidence Scorecard

| Prediction | Status | Confidence | Evidence Class | Impact | Action |
|-----------|--------|:--:|:--:|--------|--------|
| w(z) from DESI BAO | Consistent (w ≈ −0.997 ± 0.019) | Medium | A | HIGH | Wait for Euclid |
| H(z) higher than ΛCDM | Consistent within errors | Medium | A | HIGH | Monitor |
| fσ₈(z) lower | Consistent (large errors) | Medium | A | MEDIUM | Monitor |

---

## 7. Next Milestones

| Milestone | Date | Significance |
|-----------|------|-------------|
| DESI DR1 cosmology (full) | 2024–2025 | Best BAO-only constraints on w(z) and H(z) |
| DESI final cosmology | ~2026 | 5-year survey complete; final BAO + RSD results |
| DESI + Euclid combined | ~2027 | BAO + weak lensing → improved w(z) constraints |
| DESI + Euclid + Roman | ~2030 | Definitive combined constraint |
| **Decision point** | **~2027 (DESI+Euclid), ~2030 (full)** | **DESI alone insufficient. Must combine with Euclid/Roman.** |

---

## 8. Hostile Audit

*Assuming TQM is wrong: what would DESI most likely reveal?*

DESI's BAO measurements are most likely to show H(z) perfectly consistent with ΛCDM at all redshifts — no hint of the 1–3% deviation predicted by TQM. This would not falsify TQM (the deviation is within DESI's errors) but would reduce the circumstantial support.

The more dangerous outcome for TQM is if DESI's best-fit w(z) strongly favors w < −1 (phantom dark energy) — this would be inconsistent with TQM's prediction of w > −1 from Poisson fluctuations. However, current data slightly favor w > −1 in the CPL parameterization (w₀ = −0.94), which is consistent with TQM's sign.

DESI's real value is in combination with other surveys. Alone, it cannot reach the precision needed to distinguish TQM from ΛCDM. The framework's fate will be decided by Euclid and Roman, not DESI.

---

## 9. Current Verdict

**Status: GREEN — Consistent, but low discriminating power.**

DESI's results are consistent with both TQM and ΛCDM. The experiment provides important BAO constraints and complements Euclid, but cannot independently test the Λ(t) prediction at decisive significance.

| Criterion | Assessment |
|-----------|------------|
| Prediction clarity | **Good** — H(z) and w(z) are well-defined |
| Experimental capability | **Good** for BAO, but **insufficient precision** for decisive test alone |
| Current tension | None |
| Timeline to decision | Combined with Euclid: ~2027. Decisive: ~2030. |
| Overall | **Valuable complement.** Provides independent BAO probe. Cannot falsify TQM alone. |

---

*Observational Status Report V1 — August 2026. Next update: August 2027 or upon DESI final cosmology release.*
