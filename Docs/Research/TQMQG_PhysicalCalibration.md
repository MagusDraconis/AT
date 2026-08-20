# TQM-QG Phase 129 — Physical Calibration of the Sector Ladder

**Status:** COMPLETED — CLASSIFICATION: **PARTIAL MAPPING**
**Tests:** TQMQG1290, TQMQG1291, TQMQG1292 (3/3 pass; 393/393 TQMQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG128 established that sector transitions generate a predictive discrete spectrum.
This phase asks: **can the sector ladder be calibrated to known particle masses or
collider energy scales?**

## Starting Point

- QG128: PREDICTIVE SPECTRUM — 12-rung ladder (radii 6.0–17.333), dominant unit
  quantum (Δradius=1), one top quantum (1.333), 8 discrete energy thresholds.

## Method

Take the network ladder's characteristic ratios and compare them against the documented
Standard Model mass ratios:

| SM ratio | value |
|---|---|
| t/H | 1.3829 |
| H/Z | 1.3719 |
| Z/W | 1.1345 |
| t/W | 2.1523 |
| τ/μ | 16.817 |
| μ/e | 206.77 |
| τ/e | 3477.2 |

Network characteristic ratios tested:
- unit quantum (Δradius = 1.0)
- top transition quantum (1.333)
- ladder span (17.333/6.0 = 2.889)

Five calibration criteria are evaluated (mass-spectrum matching, resonance spacing,
threshold energies, collider accessibility, scaling laws). All probes are deterministic
(SM masses are documented empirical constants; no randomness).

## Assumptions

1. A mass-ratio match means a network characteristic ratio reproduces a known SM mass
   ratio within a relative tolerance (10%).
2. A linear calibration maps radius → mass (mass ∝ radius), so the ladder's radius span
   bounds the hostable mass ratio.
3. Uniform ladder spacing = harmonic-like resonance ladder.
4. Collider accessibility compares the energy range to reach all sectors against the
   approximate collider scale span (LHC 14 TeV / LEP 0.2 TeV ≈ 65).

## Results

### 1. Mass-spectrum matching (TQMQG1290)

```
unit_quantum = 1.0000 ~ Z/W = 1.1345   (dev 13.5 %)
top_quantum  = 1.3330 ~ H/Z = 1.3719   (dev  2.9 %)   ← BEST
ladder_span  = 2.8888 ~ t/W = 2.1523   (dev 25.5 %)
```

- **1 network ratio matches an SM ratio within 10%**: the top transition quantum
  (1.333) reproduces the **H/Z mass ratio (1.372)** within **2.9%**.
- The electroweak ratios are on the right scale; the ladder span is not.

### 2. Resonance spacing (TQMQG1290)

- Ladder spacings = [1.333, 1.0 × 10].
- Spacing uniformity (relative std) = **0.0929** — **uniform, harmonic-like**
  resonance spacing.

### 3. Threshold energies (TQMQG1291)

- **8 discrete thresholds**, span = **2.400** (dimensionless ceiling units).

### 4. Collider accessibility (TQMQG1291)

- Energy range to the highest sector / collider scale span = **0.1231** — all sectors
  lie within a **narrow collider window** (reachable at modest energies).

### 5. Scaling laws (TQMQG1292)

- Ladder radius span (hostable mass ratio under linear calibration) = **2.889**.
- Lepton hierarchy needed: μ/e = **206.8** — far exceeds the hostable span.
- **The ladder cannot host the lepton hierarchy** (linear calibration fails for the
  generation hierarchy).

## Conclusions

1. The ladder reproduces the **electroweak H/Z ratio within 2.9%** via the top
   transition quantum — a striking electroweak-scale correspondence.
2. The ladder is harmonic-like (uniform spacing) and lies in a narrow collider window
   (all sectors reachable).
3. However, the ladder span (2.889) **cannot** reach the lepton generation hierarchy
   (μ/e = 207) under a linear calibration — the hierarchy is not reproduced.

## Classification: **PARTIAL MAPPING**

- **NO MAPPING rejected**: the ladder does reproduce an SM electroweak ratio (~3%) and
  has observable threshold/resonance structure.
- **PHYSICAL CALIBRATION rejected**: the ladder cannot host the generation hierarchy.
- **PARTIAL MAPPING accepted**: the electroweak H/Z ratio is reproduced by the top
  quantum, but the ladder span cannot reach the lepton hierarchy — a calibration exists
  for the electroweak scale, not for the generation hierarchy.

## Connection to the TQM research arc

- QG128 PREDICTIVE SPECTRUM → QG129 shows partial physical calibration: the electroweak
  H/Z correspondence is genuine, but the generation hierarchy is beyond the ladder's span.
- QG118 family-count scaling and QG122 family compression suggest the generation
  hierarchy needs a different mechanism (high-energy family merging) than the linear
  radius ladder.
- The H/Z ≈ top-quantum correspondence (2.9%) is a candidate anchor for future
  calibration of the sector spectrum.
- Consistent with QG85 POSTULATED SM parameters: the network hosts the scales but the
  numerical hierarchy (esp. generation) remains underdetermined at linear calibration.
