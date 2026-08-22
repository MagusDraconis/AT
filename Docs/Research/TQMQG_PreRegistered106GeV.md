# TQM-QG Phase 190 — Pre-Registered 106 GeV Resonance

**Status:** COMPLETE — **PRE-REGISTERED**
**Tests:** TQMQG1900, TQMQG1901, TQMQG1902 (all passed)
**Core class:** `TQM.Core/ResearchXH/PreRegistered106GeV.cs`

---

## 1. What a Pre-Registration Is

This phase LOCKS the prediction BEFORE any future data is examined. The
prediction is frozen from **D96 geometry, the sector ladder, the octave
structure, and QG128–QG132 results only**. No ATLAS or CMS excess location,
no fitted resonance mass, and no new scaling constant is used anywhere in the
prediction.

---

## 2. Allowed vs Forbidden Inputs

**ALLOWED (the only inputs used):**
- D96 geometry (period-3 → D96 → 12-rung decay ladder, QG128)
- Sector ladder (8 discrete thresholds QG127; radii 6.0–17.333, QG128)
- Octave structure (unit quantum Δradius=1, top quantum Δradius=1.333, QG128)
- QG128–QG132 results (the Z-anchor electroweak calibration family, the
  missing-rung prediction)

**FORBIDDEN (never used):**
- ATLAS excess locations
- CMS excess locations
- fitted resonance masses
- new scaling constants

The **forbidden-input guard** asserts that the prediction is computed (not
stored) from the D96 ladder and the Z anchor, and that no field named for
ATLAS, CMS, or "Excess" exists in the class.

---

## 3. Pre-Registered Outputs (Frozen)

### 3.1 Central Mass

**106.39 GeV** — the LOWEST missing ladder rung under the Z-anchor
electroweak calibration family (QG130/133):

```
ladder radii (QG128): 17.333 … 6.0 (12 rungs)
Z-anchor scale = MZ/6 = 91.19/6 = 15.198 GeV per radius unit
missing rungs (not within 5% of Z, H, or t): 263.43, 243.17, 227.97,
  212.78, 197.58, 182.38, 151.98, 136.78, 106.39 GeV
lowest missing rung = 106.39 GeV   (rung 10, radius 7.0)  ← CENTRAL MASS
```

### 3.2 Uncertainty Window

**98.79 – 113.99 GeV**, stated as **99–114 GeV**:

```
mean rung spacing = 15.20 GeV (scale × unit quantum)
half-spacing = 7.60 GeV
window = 106.39 ± 7.60 = 98.79 – 113.99 GeV
```

### 3.3 Production Hierarchy

All 9 predicted resonances, ascending, **all below LHC13 and FCC-hh reach**:

```
106.39  →  136.78  →  151.98  →  182.38  →  197.58
→ 212.78  →  227.97  →  243.17  →  263.43 GeV
```

The primary (106.39 GeV) is the most kinematically accessible predicted
state — the expected production hierarchy is by rung mass, with the lightest
missing rung most copiously produced.

### 3.4 Decay Hierarchy

The QG128 emitted-quantum spectrum, calibrated:

```
unit quantum: radius drop 1.000 → 15.20 GeV × 10   (fraction 0.909)
top quantum:  radius drop 1.333 → 20.26 GeV ×  1
cascade endpoint: radius 6, 3 families (the observable sector)
```

A decaying high sector produces a **characteristic 15–20 GeV quantum
pattern** (dominant 15.20 GeV line, one 20.26 GeV line) terminating in the
observable 3-family sector.

---

## 4. Acceptance Criteria

| Outcome | Condition |
|---------|-----------|
| **CONFIRMED** | a signal appears within the frozen window 99–114 GeV with compatible production pattern (rung-mass hierarchy) and the 15–20 GeV decay-quantum pattern |
| **DISFAVORED** | no signal in statistically sensitive searches of the frozen window |

Frozen check examples (used only to test the criteria, not to set the
prediction):
- signal at 106.4 GeV with 15.2 GeV quanta → **CONFIRMED**
- signal at 95 GeV → outside the frozen window → **not confirmed** by this
  pre-registration
- null result (no excess) → **DISFAVORED**

---

## 5. Why This Is Pre-Registered

The point of freezing the prediction now is to prevent post-hoc selection:
future data must be compared against the D96-computed window 99–114 GeV and
the 15–20 GeV decay pattern, NOT against a prediction adjusted to fit any
observed excess. This makes the sector-ladder hypothesis genuinely
falsifiable.

---

## 6. Scientific Limitations

- The prediction depends on the **Z-anchor calibration family** (QG130/133):
  the boson anchors (Z, W) agree within 0.74% (~106–107 GeV), while fermion
  anchors (H, t) shift to ~146 GeV and ~202 GeV. The frozen 106.39 GeV is
  the boson-family prediction.
- The decay hierarchy (15–20 GeV quanta) is the QG128 spectrum, not an
  independently fitted pattern.
- "CONFIRMED" requires both the mass window AND the decay pattern; a signal
  in the window with an incompatible decay pattern would be a partial match,
  not a confirmation.
