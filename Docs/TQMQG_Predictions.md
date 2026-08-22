# TQM-QG Prediction Registry

**Immutable.** Locked by TQM-QG Phase 193 (Prediction Registry Lock).

> **Rule:** No future phase may modify a registered prediction. Only **CONFIRMED**, **DISFAVORED**, **FALSIFIED** may be added later (as the outcome).

Machine-readable twin: `Docs/TQMQG_Predictions.json`.

---

## P1 — 106 GeV resonance

| Field | Value |
|-------|-------|
| Derivation phase | QG132 (derived) / QG190 (frozen) |
| Formula | `M_106 = 7·MZ/6 = 7·15.198 GeV; window = M_106 ± spacing/2, spacing = MZ/6 = 15.20 GeV` |
| Inputs | D96 ladder radii 6.0–17.333 (QG121/128), Z-anchor calibration MZ/6 (QG130), missing-rung rule (QG132) |
| Frozen value | **106.39 GeV (central); window 98.79–113.99 GeV (stated 99–114 GeV)** |
| Uncertainty | ±7.60 GeV (half the mean rung spacing); boson-anchor family agrees within 0.74% (QG133) |
| Falsification condition | No signal in statistically sensitive searches of the 99–114 GeV window (DISFAVORED/FALSIFIED) |
| Outcome | PENDING (no outcome yet) |

---

## P2 — 0νββ m_ββ

| Field | Value |
|-------|-------|
| Derivation phase | QG179 (derived) / QG191 (frozen) |
| Formula | `m_ββ = |Σ U_ei²·m_i| = |m1·c12²·c13² + m2·s12²·c13² + m3·s13²·e^(−2iδ)|` |
| Inputs | QG167 PMNS (s12 = √(#d/(Σm+#g)) = 0.5497, s13 = √(occ0/(2Σm)) = 0.1451, δ_ν = 66.4°), QG172 masses (m1=0, m2=8.72e-3, m3=4.94e-2 eV, normal ordering), QG179 Majorana (α2=α3=0) |
| Frozen value | **m_ββ = 2.02 meV (computed 2.0222 meV)** |
| Uncertainty | ±10% (1.8–2.2 meV range); dominated by m2·s12²·c13² = 2.52 meV, robust to CP phase |
| Falsification condition | Significant exclusion below 2.02 meV (a measured upper limit < 2.02 meV FALSIFIES) |
| Outcome | PENDING (no outcome yet) |

---

## P3 — Sector-ladder spectrum

| Field | Value |
|-------|-------|
| Derivation phase | QG128-132 (derived) / QG192 (frozen) |
| Formula | `E_rung = radius·(MZ/6); unit quantum ΔE = MZ/6 = 15.20 GeV, top quantum = 1.333·15.20 = 20.26 GeV` |
| Inputs | D96 ladder radii (QG121/128), 8 thresholds (QG127), Z-anchor scale (QG130), missing-rung rule (QG132) |
| Frozen value | **9 resonances: 106.39 (primary) → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities unit ×10 (0.909) + top ×1; width scale 15.20 GeV** |
| Uncertainty | ±5% per rung; boson-anchor family agrees within 0.74% (QG133) |
| Falsification condition | A sensitive search excludes any frozen rung (limit below the rung energy FALSIFIES) |
| Outcome | PENDING (no outcome yet) |

---

