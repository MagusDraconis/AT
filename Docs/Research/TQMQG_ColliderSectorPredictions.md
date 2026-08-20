# TQM-QG Phase 130 — Collider-Accessible Sector Prediction

**Status:** COMPLETED — CLASSIFICATION: **ACCESSIBLE**
**Tests:** TQMQG1300, TQMQG1301, TQMQG1302 (3/3 pass; 396/396 TQMQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG124–129 established that higher-energy sectors exist, are metastable, and generate a
predictive spectrum. This phase asks: **which sector transitions are accessible within
current and next-generation collider energies?**

## Starting Point

- QG125: METASTABLE — high sectors decay stepwise when energy is removed.
- QG128: PREDICTIVE SPECTRUM — 12-rung ladder, unit quantum, 8 thresholds.
- QG129: PARTIAL MAPPING — top transition quantum reproduces H/Z within ~3% (electroweak
  calibration family).

## Method

Calibrate the 12-rung decay ladder under the QG129-supported **electroweak calibration
family** — anchoring the observable radius-6 sector on each heavy SM state (W, Z, H, t).
Then compare the calibrated sector masses against documented collider c.o.m. energies:

| Collider | Energy (TeV) |
|---|---|
| LEP | 0.209 |
| LHC13 | 13 |
| HL-LHC | 14 |
| FCC-ee | 0.365 |
| FCC-hh | 100 |

All probes are deterministic (documented masses and collider energies; fixed network
parameters).

## Assumptions

1. Linear radius→mass calibration (mass ∝ radius), per QG129's partial mapping.
2. The plausible calibration family is the electroweak anchors (W, Z, H, t) — the heavy
   SM states whose ratios the ladder reproduces.
3. A rung is kinematically accessible if its mass is below the collider c.o.m. energy.
4. Accessible high sectors are METASTABLE (QG125) — they appear as decay signatures, not
   stable particles.

## Results

### 1. Sector thresholds (TQMQG1300)

- **8 discrete thresholds** at ceiling 1.25, 1.50, 1.75, 2.00, 2.25, 2.50, 2.75, 3.00.

### 2. Ladder accessibility (TQMQG1300)

Z-anchor calibrated rung masses:

```
rung  0: 263.4 GeV  (highest sector)
rung  1: 243.2 GeV
rung  2: 228.0 GeV
rung  3: 212.8 GeV
rung  4: 197.6 GeV
rung  5: 182.4 GeV
rung  6: 167.2 GeV
rung  7: 152.0 GeV
rung  8: 136.8 GeV
rung  9: 121.6 GeV
rung 10: 106.4 GeV
rung 11:  91.2 GeV  (observable)
```

Collider accessibility (Z anchor):
- LEP (0.209 TeV): **8/12** rungs, top NOT accessible.
- **LHC13 (13 TeV): 12/12, top accessible.**
- HL-LHC (14 TeV): 12/12, top accessible.
- FCC-ee (0.365 TeV): 12/12, top accessible.
- **FCC-hh (100 TeV): 12/12, top accessible.**

### 3. Decay spectra (TQMQG1301)

Z-anchor emitted-quantum energies:
- unit quantum (Δradius=1.0) → **15.20 GeV**
- top quantum (Δradius=1.333) → **20.26 GeV**

The decay quanta are in the tens-of-GeV range — inside the collider's accessible range.

### 4. Observable signatures (TQMQG1301)

- Top-sector decay signature observable at **LHC13: True**.
- Top-sector decay signature observable at **FCC-hh: True**.

Accessible high sectors decay (QG125 metastability) with quanta in the collider's energy
range — the decay itself is an observable signature.

### 5. LHC/FCC reach (TQMQG1302)

Reach summary (top-rung mass per anchor):

| Anchor | Top mass | LHC13 | FCC-hh | fraction at LHC |
|---|---|---|---|---|
| W | 232.2 GeV | ✓ | ✓ | 1.000 |
| Z | 263.4 GeV | ✓ | ✓ | 1.000 |
| H | 361.4 GeV | ✓ | ✓ | 1.000 |
| t | 499.8 GeV | ✓ | ✓ | 1.000 |

For the **entire electroweak calibration family**, the highest-energy sector is
**LHC13- and FCC-hh-accessible** with 100% of rungs below LHC energy.

## Conclusions

1. Under the plausible electroweak calibration, the sector ladder spans **~90–500 GeV** —
   the "electroweak-to-top" window.
2. All ladder rungs are within **LHC13, HL-LHC, and FCC-hh** reach (12/12).
3. The decay quanta (15–20 GeV) are observable — accessible sectors appear as metastable
   decay signatures rather than stable particles.

## Classification: **ACCESSIBLE**

- **NOT ACCESSIBLE rejected**: the sectors lie far below LHC/FCC energies.
- **PARTIALLY ACCESSIBLE rejected**: the whole electroweak calibration family is reachable
  at LHC13 (score 5/5).
- **ACCESSIBLE accepted**: the highest-energy sectors fall within LHC13 and FCC-hh reach
  for the entire plausible electroweak calibration family, appearing as metastable decay
  signatures.

## Connection to the TQM research arc

- QG129 PARTIAL MAPPING → QG130 shows that within the plausible (electroweak) calibration,
  the predicted sectors are collider-reachable — a **testable prediction**.
- QG125 METASTABLE → the sectors would appear as decay cascades/signatures, not new stable
  particles — consistent with the absence of new stable resonances in LHC data.
- QG128 PREDICTIVE SPECTRUM → the decay quanta (15–20 GeV) are in a discoverable range.
- QG127 OBSERVABLE SIGNATURES → the transition spectrum gives the search signature.
- QG119/120 horizon suppression → higher families suppressed from steady state, but their
  collider-accessible decay signatures (this phase) provide a possible observation channel.
