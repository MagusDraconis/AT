# AT-QG Phase 193 — Prediction Registry Lock

**Status:** COMPLETE — **REGISTRY LOCK**
**Tests:** ATQG1930, ATQG1931, ATQG1932 (all passed)
**Core class:** `AT.Core/ResearchXH/PredictionRegistry.cs`
**Registry files:** `Docs/ATQG_Predictions.md` + `Docs/ATQG_Predictions.json`
**Generator:** `Tools/build_predictions_registry.py`

---

## 1. Goal

Create the **immutable registry** of the three pre-registered predictions and
generate the permanent prediction record (`Docs/ATQG_Predictions.md` +
`.json`). Rule: **no future phase may modify a registered prediction** — only
CONFIRMED, DISFAVORED, or FALSIFIED may be added later as an outcome.

---

## 2. The Immutable Registry

| ID | Prediction | Derivation | Frozen value | Falsification |
|----|-----------|-----------|--------------|---------------|
| P1 | 106 GeV resonance | QG132 / QG190 | 106.39 GeV; window 99–114 GeV | no signal in the window |
| P2 | 0νββ m_ββ | QG179 / QG191 | 2.02 meV | exclusion below 2.02 meV |
| P3 | Sector-ladder spectrum | QG128-132 / QG192 | 9 rungs 106.4–263.4 GeV | search excludes a rung |

### P1 — 106 GeV resonance

- **Formula:** M_106 = 7·MZ/6 = 7·15.198 GeV; window = M_106 ± spacing/2,
  spacing = MZ/6 = 15.20 GeV
- **Inputs:** D96 ladder radii 6.0–17.333 (QG121/128), Z-anchor calibration
  MZ/6 (QG130), missing-rung rule (QG132)
- **Frozen value:** 106.39 GeV (central); window 98.79–113.99 GeV (stated
  99–114 GeV)
- **Uncertainty:** ±7.60 GeV (half the mean rung spacing); boson-anchor
  family agrees within 0.74% (QG133)
- **Falsification:** no signal in statistically sensitive searches of the
  99–114 GeV window

### P2 — 0νββ m_ββ

- **Formula:** m_ββ = |Σ U_ei²·m_i| = |m1·c12²·c13² + m2·s12²·c13² +
  m3·s13²·e^(−2iδ)|
- **Inputs:** QG167 PMNS (s12=0.5497, s13=0.1451, δ_ν=66.4°), QG172 masses
  (m1=0, m2=8.72e-3, m3=4.94e-2 eV, normal ordering), QG179 Majorana
  (α2=α3=0)
- **Frozen value:** m_ββ = 2.02 meV (computed 2.0222 meV)
- **Uncertainty:** ±10% (1.8–2.2 meV); dominated by m2·s12²·c13² = 2.52 meV,
  robust to CP phase
- **Falsification:** significant exclusion below 2.02 meV

### P3 — Sector-ladder spectrum

- **Formula:** E_rung = radius·(MZ/6); unit quantum ΔE = 15.20 GeV, top
  quantum = 20.26 GeV
- **Inputs:** D96 ladder radii (QG121/128), 8 thresholds (QG127), Z-anchor
  scale (QG130), missing-rung rule (QG132)
- **Frozen value:** 9 resonances 106.39 (primary) → 136.78 → 151.98 → 182.38
  → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities unit ×10
  (0.909) + top ×1; width scale 15.20 GeV
- **Uncertainty:** ±5% per rung; boson-anchor family agrees within 0.74%
  (QG133)
- **Falsification:** a sensitive search excludes any frozen rung

---

## 3. Immutability Mechanism

The registry lock is enforced three ways:

1. **`readonly` static field** — the `Registry` array cannot be reassigned.
2. **Init-only records** — `RegisteredPrediction` is a positional record with
   init-only properties: the frozen fields have no setter after construction.
3. **Values-unchanged guard** — `ValuesUnchanged()` re-derives each frozen
   value from the pre-registration phases (QG190/191/192) and fails if any
   value drifts.

The only allowed transition is `RecordOutcome(id, Confirmed/Disfavored/
Falsified)`, which returns a *new* record with the outcome set — the original
registry is never mutated (verified: `Get(P1).Status` remains `None` after
recording).

---

## 4. The Registry Files

Generated atomically from the single source of truth:

- **`Docs/ATQG_Predictions.md`** — human-readable registry with the rule
  header and a per-prediction field table (derivation phase, formula, inputs,
  frozen value, uncertainty, falsification, outcome).
- **`Docs/ATQG_Predictions.json`** — machine-readable twin
  (`immutable: true`, the rule string, and the predictions array).
- **`Tools/build_predictions_registry.py`** — the generator; run with
  `python Tools/build_predictions_registry.py`.

---

## 5. The Lock Rule

> **No future phase may modify a registered prediction. Only CONFIRMED,
> DISFAVORED, FALSIFIED may be added later.**

This is the permanent prediction record: P1, P2, P3 are frozen. Future
experimental outcomes are appended as status, never as edits.

---

## 6. Scientific Limitations

- The registry freezes the *values*, not the underlying derivation. If a
  future phase legitimately re-derives a quantity with new inputs, it must
  register a NEW prediction — it cannot edit P1/P2/P3.
- The outcome status is an audit record: CONFIRMED/DISFAVORED/FALSIFIED will
  be assigned by future evidence audits (e.g. HL-LHC results, nEXO/LEGEND
  limits), not by this phase.
- The registry documents the prediction and its falsification condition; it
  does not by itself make the predictions true.
