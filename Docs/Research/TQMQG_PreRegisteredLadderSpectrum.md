# TQM-QG Phase 192 — Pre-Registered Sector-Ladder Spectrum

**Status:** COMPLETE — **PRE-REGISTERED**
**Tests:** TQMQG1920, TQMQG1921, TQMQG1922 (all passed)
**Core class:** `TQM.Core/ResearchXH/PreRegisteredLadderSpectrum.cs`

---

## 1. What a Pre-Registration Is

The full 12-rung ladder spectrum is **LOCKED** before any future collider
data is examined. It is frozen from the **ladder structure, the attractor
spectrum, and D96 geometry (QG121–QG132)** only. No collider bump, resonance
catalog, or fitted energy enters anywhere.

---

## 2. Allowed vs Forbidden Inputs

**ALLOWED (the only inputs used):**
- ladder structure — the 12-rung decay ladder (radii 6.0–17.333, QG121/QG128)
- attractor spectrum — 8 discrete energy thresholds, emitted-quantum spectrum
  (unit Δradius=1, top Δradius=1.333, QG127/QG128)
- D96 geometry — the Z-anchor electroweak calibration family (scale = MZ/6,
  QG130)

**FORBIDDEN (never used):**
- collider bumps
- resonance catalogs
- fitted energies

The **forbidden-input guard** asserts that the energies are computed (not
stored) and that no field named for a bump, catalog, fitted energy, or excess
exists in the class.

---

## 3. Frozen Ladder Spectrum

Calibration scale = MZ/6 = 91.19/6 = **15.198 GeV per radius unit**.

| Rung | Energy | Status | Channel |
|------|--------|--------|---------|
| 0 | 263.43 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 1 | 243.17 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 2 | 227.97 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 3 | 212.78 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 4 | 197.58 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 5 | 182.38 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 6 | 167.18 GeV | aligned with t (not predicted) | — |
| 7 | 151.98 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 8 | 136.78 GeV | ladder resonance | unit-quantum cascade (15.2 GeV) |
| 9 | 121.59 GeV | aligned with H (not predicted) | — |
| 10 | 106.39 GeV | **PRIMARY** ladder resonance | unit-quantum cascade (15.2 GeV) |
| 11 | 91.19 GeV | aligned with Z (not predicted) | — |

**9 predicted ladder resonances:** 106.39, 136.78, 151.98, 182.38, 197.58,
212.78, 227.97, 243.17, 263.43 GeV.

---

## 4. Required Output Table (Rung | Energy | Visibility | Channel)

| Rung | Energy | Expected visibility | Expected channel |
|------|--------|---------------------|------------------|
| 0 | 263.43 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 1 | 243.17 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 2 | 227.98 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 3 | 212.78 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 4 | 197.58 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 5 | 182.38 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 6 | 167.18 GeV | aligned with SM t | not a prediction |
| 7 | 151.98 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 8 | 136.78 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 9 | 121.59 GeV | aligned with SM H | not a prediction |
| 10 | 106.39 GeV | searchable at LHC13 (metastable decay) | unit-quantum cascade (15.2 GeV) |
| 11 | 91.19 GeV | aligned with SM Z | not a prediction |

---

## 5. Pre-Registered Outputs

**1. Ladder energies** — the 9 resonance masses frozen above.

**2. Multiplicities** — from the QG128 emitted-quantum spectrum:
```
unit quantum: Δradius 1.000 → 15.20 GeV × 10   (fraction 0.909)
top quantum:  Δradius 1.333 → 20.26 GeV ×  1
```

**3. Widths** — a predicted resonance is **metastable** (QG125) and decays in
**unit-quantum steps**: the width scale is the unit-quantum energy **15.20
GeV** (radius drop 1).

**4. Production ordering** — by rung mass ascending (the lightest predicted
state, 106.39 GeV, is most accessible); **all 9 rungs below LHC13 (13 TeV)
and FCC-hh (100 TeV)**.

---

## 6. Acceptance Criteria

| Outcome | Condition |
|---------|-----------|
| **CONFIRMED** | a new resonance matches a frozen rung energy (within 5%) |
| **FALSIFIED** | sensitive searches exclude a frozen rung (limit below rung energy) |

Frozen check examples (used only to test the criteria, not to set the
spectrum):
- new resonance at 106.4 GeV → **CONFIRMED** (matches rung 10)
- new resonance at 136.8 GeV → **CONFIRMED** (matches rung 8)
- new resonance at 95 GeV → **not confirmed** (not a frozen rung)
- exclusion limit 100 GeV at rung 106.39 → **FALSIFIED**

---

## 7. Why This Is Pre-Registered

Freezing the full spectrum prevents post-hoc selection: future collider data
must be compared against the 9 frozen rung energies and the 15.2 GeV
unit-quantum decay pattern — not against a spectrum adjusted to fit observed
bumps. The sector-ladder hypothesis is now falsifiable rung-by-rung.

---

## 8. Scientific Limitations

- The spectrum depends on the **Z-anchor calibration family** (QG130/133):
  the boson anchors (Z, W) agree within 0.74%, while fermion anchors (H, t)
  shift the rungs. The frozen values are the boson-family prediction.
- The widths are a scale (15.20 GeV unit quantum), not a precise Breit-Wigner
  prediction — the metastable cascade is the observable, not a narrow line.
- "CONFIRMED" requires a match within 5% of a frozen rung; a broad or
  shifted signal would be a partial match, not a confirmation.
