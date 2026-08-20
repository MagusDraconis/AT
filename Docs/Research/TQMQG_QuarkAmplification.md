# TQM-QG Phase 143 — Origin of Quark Amplification

**Status:** COMPLETED — CLASSIFICATION: **PARTIAL FACTOR**
**Tests:** TQMQG1430, TQMQG1431, TQMQG1432 (3/3 pass)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG141 derived the lepton hierarchy exponents from the spectral density and QG142 showed
leptons follow the octave law (τ/e within 0.26%) while quarks and neutrinos deviate.
This phase asks: **what extra sector-dependent factor amplifies quark and neutrino
masses beyond the octave hierarchy?**

## Starting Point

- QG141: DERIVED EXPONENTS — mass ~ center^5.88 from spectral density.
- QG142: PARTIAL LAW — leptons match, up/down/neutrino deviate.

## Method

The deviation factor of each sector is f = r31_observed / r31_octave, with
r31_octave = 4^5.88 = 3468 (the QG140/141 spectral law). Five candidate factors are
tested against the documented sector quantum numbers:

1. **Color-sector effects** — a color multiplicity (N=3 quarks vs N=1 leptons).
2. **Charge-sector effects** — correlation with electric charge |Q|.
3. **Isospin effects** — up (T3=+1/2) vs down (T3=−1/2) asymmetry.
4. **Sector occupation density** — spectral mode-density proxy.
5. **Multi-sector coupling** — a product of sector factors (charge-power coupling).

All probes are deterministic (documented SM quantum numbers; fixed network parameters).

## Assumptions

1. The octave law (r31 = 4^5.88) is the baseline; any deviation is a sector-dependent
   amplification.
2. Candidate factors: color, electric charge, weak isospin, occupation density.
3. A single factor must explain the deviations to qualify as "the" origin.

## Results

### 1. Deviation factors (TQMQG1430)

```
r31_octave = 3468.3
leptons   factor = 1.003   (tracks the octave law)
up        factor = 22.673  (strongly amplified)
down      factor = 0.256   (suppressed)
neutrino  factor = 0.144   (strongly suppressed)
```

- The deviations are **strongly sector-dependent**.

### 2. Color-sector effects (TQMQG1430)

- Up/down factor ratio = **88.4**.
- A single color factor explains both quarks: **False**.
- **Color alone does NOT explain the deviations** (both quarks are color 3 but differ
  by ~88×).

### 3. Charge-sector effects (TQMQG1431)

- Pearson r(deviation, |Q|) = **0.290** — only a weak/moderate correlation.

### 4. Isospin effects (TQMQG1431)

- Up factor (T3=+1/2) = **22.67**; down factor (T3=−1/2) = **0.26**.
- Up/down = **88.4**.
- **Isospin-signed amplification: True** (up-type amplified, down-type suppressed).

### 5. Sector occupation density + multi-sector coupling (TQMQG1432)

- Occupation density (top-octave fraction) = **0.916**.
- Implied charge-power exponent n (|Q_up|/|Q_down|)^n = up/down: **6.47** — a steep
  charge-power coupling.

## Conclusions

1. The deviations are sector-dependent and **isospin-signed**: up-type amplified (~23×),
   down-type and neutrino suppressed.
2. **Color alone fails** (both quarks color 3, differ 88×).
3. The charge correlation is weak (0.29); the up/down split implies a **steep
   charge-power coupling** (n ≈ 6.5).
4. No single factor (color, charge, or isospin alone) reproduces all deviations.

## Classification: **PARTIAL FACTOR**

- **NO FACTOR rejected**: the deviations are strongly sector-dependent.
- **AMPLIFICATION ORIGIN rejected**: no single sector factor reproduces all deviations
  (charge correlation weak).
- **PARTIAL FACTOR accepted**: the amplification is isospin-signed (up ↑, down ↓) with a
  steep charge-power coupling (n≈6.5), but the factor is not fully determined.

## Connection to the TQM research arc

- QG141/142 spectral law → QG143 identifies the deviation structure: isospin-signed
  amplification beyond the octave hierarchy.
- QG134 FUNDAMENTAL SPLIT → the quark/lepton difference is not a single color factor but
  an isospin/charge-signed sector effect.
- The up/down split (88×) is the key target for a future mechanism — it correlates with
  T3 (charge asymmetry), not color.
- Open: what sets the charge-power exponent n ≈ 6.5? (a next-phase target).
