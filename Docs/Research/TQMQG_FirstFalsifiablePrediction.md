# TQM-QG Phase 132 — First Falsifiable Collider Prediction

**Status:** COMPLETED — CLASSIFICATION: **FALSIFIABLE PREDICTION**
**Tests:** TQMQG1320, TQMQG1321, TQMQG1322 (3/3 pass; 402/402 TQMQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG131 established that existing collider data are consistent with the sector ladder.
This phase asks: **does the sector hierarchy predict a specific yet-unobserved energy
region or decay signature?**

## Starting Point

- QG130: ACCESSIBLE — ladder spans ~90–500 GeV, all within LHC/FCC reach.
- QG131: CONSISTENT SIGNATURE — 95 GeV excess, Z/H/t masses, pair thresholds on rungs.

## Method

Within the Z-anchor ladder (12 rungs, 91.19–263.43 GeV), identify the rungs NOT near an
observed SM state (Z, H, t within 5%) — these are the predicted-but-unobserved
resonances. Evaluate five criteria: missing rungs, predicted resonance masses, decay
cascade endpoints, threshold regions, and collider reach. All probes are deterministic.

## Assumptions

1. A rung within 5% of an observed SM mass (Z, H, t) is "already observed".
2. A missing rung is a predicted resonance — a specific, searchable mass.
3. The lowest missing rung in the clean Z–H window is the primary discovery target.
4. All predicted resonances being within LHC/FCC reach makes the prediction TESTABLE
   (falsifiable).

## Results

### 1. Missing ladder rungs (TQMQG1320)

The 12-rung ladder with observed states marked:

```
 91.19 GeV  (observed: Z)
106.39 GeV  (MISSING)
121.59 GeV  (observed: H)
136.78 GeV  (MISSING)
151.98 GeV  (MISSING)
167.18 GeV  (observed: t)
182.38 GeV  (MISSING)
197.58 GeV  (MISSING)
212.78 GeV  (MISSING)
227.97 GeV  (MISSING)
243.17 GeV  (MISSING)
263.43 GeV  (MISSING)
```

- **9 missing ladder rungs** — the predicted yet-unobserved resonances.

### 2. Predicted resonances (TQMQG1320)

Predicted resonance masses (ascending):

```
106.39 GeV   ← primary (clean Z–H window)
136.78 GeV
151.98 GeV
182.38 GeV
197.58 GeV
212.78 GeV
227.97 GeV
243.17 GeV
263.43 GeV
```

- **Primary predicted resonance: 106.39 GeV**.
- Search window: **98.6 – 114.2 GeV** (center ± half average rung spacing).

### 3. Decay-cascade endpoints (TQMQG1321)

- unit quantum: Δradius 1.000 → **15.20 GeV** × 10
- top quantum: Δradius 1.333 → **20.26 GeV** × 1
- Cascade endpoint sector: radius 6.000, families **3** — the observable 3-family sector.

A decaying predicted resonance emits a well-defined quantum signature (15.2 GeV × 10,
20.3 GeV × 1) and settles in the observable sector.

### 4. Threshold regions (TQMQG1321)

- **8 discrete threshold regions** (ceiling 1.25, 1.50, 1.75, 2.00, 2.25, 2.50, 2.75,
  3.00) marking where new sectors appear.

### 5. Collider reach (TQMQG1322)

- All predicted resonances below **LHC13 (13 TeV): True**.
- All predicted resonances below **FCC-hh (100 TeV): True**.
- Prediction score **5 / 5**.

## Conclusions

1. The sector hierarchy predicts **9 specific yet-unobserved resonances**.
2. The **primary prediction is ~106 GeV** — in a clean discovery window between Z
   (91.2) and H (125.1).
3. Decaying predicted resonances would produce a **characteristic 15.2/20.3 GeV quantum
   signature** terminating in the observable 3-family sector.
4. All predicted resonances are **testable at LHC13 and FCC-hh**.

## Classification: **FALSIFIABLE PREDICTION**

- **NO PREDICTION rejected**: the ladder predicts specific unobserved resonances.
- **PARTIAL PREDICTION rejected**: the prediction is specific and fully testable (score
  5/5).
- **FALSIFIABLE PREDICTION accepted**: 9 specific resonances (primary ~106 GeV in the
  clean Z–H window) with a defined decay signature, all within LHC/FCC reach — the
  FIRST falsifiable collider prediction of the sector hierarchy.

## The prediction in one line

> **A new resonance should appear at ~106 GeV (search window ~99–114 GeV), decaying with
> 15–20 GeV quanta into the observable sector; additional predicted resonances at ~137,
> 152, 182, 198, 213, 228, 243, 263 GeV.**

## Connection to the TQM research arc

- QG131 CONSISTENT SIGNATURE → QG132 converts the observed consistency into a specific,
  testable prediction.
- The 95 GeV excess (4% from the Z rung, QG131) is a hint in the same low-mass region as
  the primary 106 GeV prediction.
- QG125 METASTABLE → predicted resonances decay (not stable), explaining why they are
  not yet seen while the SM masses align.
- QG128 PREDICTIVE SPECTRUM → the 15.2/20.3 GeV decay quanta are the search signature.
- If LHC/FCC find nothing at ~106 GeV, the Z-anchor electroweak calibration is ruled out —
  a genuine falsifiable test of the sector-ladder picture.
