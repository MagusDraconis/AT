# AT-QG Phase 133 — Robustness of the 106 GeV Prediction

**Status:** COMPLETED — CLASSIFICATION: **MODERATE**
**Tests:** ATQG1330, ATQG1331, ATQG1332 (3/3 pass; 405/405 ATQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG132 predicts a primary resonance near 106 GeV (Z-anchor calibration). This phase
asks: **how sensitive is the prediction to calibration assumptions?**

## Starting Point

- QG129: PARTIAL MAPPING — ladder calibrates at the electroweak scale.
- QG132: FALSIFIABLE PREDICTION — primary predicted resonance at 106.39 GeV.

## Method

Recompute the primary predicted resonance (lowest ladder rung not within 5% of an
observed SM state) under each of the four electroweak calibration anchors (Z, H, W, t —
each anchoring the observable radius-6 sector on that mass). Then measure parameter
uncertainty: the shift produced by each anchor's documented experimental mass
uncertainty, and the sensitivity to the observed-tolerance parameter. All probes are
deterministic.

## Assumptions

1. The four plausible calibration anchors are the electroweak states Z, W, H, t.
2. The observed set includes all four electroweak states (a rung within 5% counts as
   seen).
3. Experimental mass uncertainties are the documented PDG values (Z ± 0.0021, W ± 0.009,
   H ± 0.17, t ± 0.40 GeV).

## Results

### 1–4. Anchor calibrations (ATQG1330)

Primary predicted resonance under each anchor:

| Anchor | Primary | vs Z |
|---|---|---|
| Z | 106.39 GeV | — |
| W | 107.17 GeV | **0.74%** |
| H | 145.95 GeV | 37.2% |
| t | 201.83 GeV | 89.7% |

- The two **electroweak BOSON anchors (Z, W) agree within 0.74%** (~106–107 GeV).
- The **fermion-sector anchors (H, t) shift** the prediction to ~146 GeV and ~202 GeV.

### 5. Parameter uncertainty (ATQG1331)

- Experimental mass-uncertainty widths: Z ± 0.000, W ± 0.02, H ± 0.40, t ± 0.93 GeV.
- **Max width = 0.93 GeV (0.9% of the Z prediction)** — experimental errors shift the
  prediction by less than 1 GeV.
- Observed-tolerance sweep (3% → 10%): the Z-anchor primary stays **106.39 GeV
  unchanged** — fully tolerance-insensitive.

## Conclusions

1. The ~106 GeV prediction is **stable within the electroweak-boson calibration family**
   (Z and W anchors agree within 0.74%).
2. It is **insensitive to experimental mass uncertainties** (< 1 GeV shift) and **to the
   observed-tolerance parameter**.
3. It is **not robust against re-anchoring on the fermion-sector states** (H → 146 GeV,
   t → 202 GeV).

## Classification: **MODERATE**

- **FRAGILE rejected**: boson anchors and parameters leave the prediction stable.
- **ROBUST rejected**: fermion anchors (H → 146 GeV, t → 202 GeV) shift the prediction.
- **MODERATE accepted**: the prediction is stable within the electroweak-boson calibration
  family (Z/W agree within 1%), insensitive to experimental/parameter uncertainty, but
  not robust against re-anchoring on the fermion-sector states.

## Connection to the AT research arc

- QG129 PARTIAL MAPPING → QG133 shows the calibration family matters: the boson sector
  (Z/W) pins ~106 GeV robustly, while the fermion anchors are less consistent.
- QG132 FALSIFIABLE PREDICTION → QG133 qualifies it: the ~106 GeV prediction is a robust
  statement of the electroweak-boson calibration family, not of the ladder in general.
- The fermion-sector spread (146–202 GeV) mirrors the QG129 generation-hierarchy gap —
  the same incompleteness.
- The primary ~106 GeV prediction (search window 99–114 GeV) survives as the best
  falsifiable target; a null result there tests the boson-calibrated sector ladder.
