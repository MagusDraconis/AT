# AT-QG Phase 144 — Weak-Isospin Amplification Origin

**Status:** COMPLETED — CLASSIFICATION: **PARTIAL EFFECT**
**Tests:** ATQG1440, ATQG1441, ATQG1442 (3/3 pass)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG143 established that quark/neutrino mass deviations are strongly isospin-signed. This
phase asks: **can weak-isospin coupling explain the quark hierarchy amplification?**

## Starting Point

- QG143: PARTIAL FACTOR — deviations are isospin-signed (up 22.7×, down 0.26×).

## Method

Use the documented sector deviation factors (QG143) and the documented SM quantum
numbers (T3, Q, Y, with Q = T3 + Y/2). Five probes:

1. **T3 dependence** — correlation of the deviation with weak isospin.
2. **Up/down amplification** — the up (T3=+1/2, Q=+2/3) vs down (T3=−1/2, Q=−1/3) split.
3. **Charge-isospin combinations** — test candidate combinations (Q, |Q|, Q·T3, |Q|·T3,
   Q², (1+T3), …) and the charge-SIGN gate.
4. **Sector splitting** — separation of the amplified up sector from the others.
5. **Hierarchy reconstruction** — reproduction of the observed ordering
   (neutrino < down < lepton < up).

All probes are deterministic.

## Assumptions

1. The documented SM quantum numbers (T3, Q, Y) are the candidate couplings.
2. A clean weak-isospin law would correlate strongly AND satisfy the charge-sign gate
   (only Q>0 amplified).

## Results

### 1. T3 dependence (ATQG1440)

```
leptons:  factor=1.003  T3=-0.5  Q=-1.000  log2(f)= 0.00
up:       factor=22.673 T3=+0.5  Q=+0.667  log2(f)= 4.50
down:     factor= 0.256  T3=-0.5  Q=-0.333  log2(f)=-1.97
neutrino: factor= 0.144  T3=+0.5  Q= 0.000  log2(f)=-2.80
```

- T3 correlation with log2(factor): **0.325** (weak).
- |T3| correlation: **0.000**.

### 2. Up/down amplification (ATQG1440)

- Up factor = **22.67**; down factor = **0.26**.
- Up/down = **88.6** — a **strongly isospin-signed split** (True).

### 3. Charge-isospin combinations (ATQG1441)

```
|Q|  r=0.588   (best)
Q·T3 r=0.588
Q    r=0.532
|Q|·T3 r=0.532
Y    r=0.471
Q²   r=0.429
```

- Best combination: **|Q|** (r = 0.588) — a moderate correlation.
- Charge-SIGN gate: **False** — leptons (Q=−1) still track the octave law (factor ≈ 1),
  not suppressed like the negatively-charged down.

### 4. Sector splitting (ATQG1441)

- Separation (up / max other factor) = **22.6×** — the up sector is cleanly separated.

### 5. Hierarchy reconstruction (ATQG1442)

- Observed ordering: neutrino (0.144) < down (0.256) < leptons (1.003) < up (22.673).
- **Ordering reconstructed: True.**

## Conclusions

1. The up/down split is **strongly isospin-signed** (88.6×).
2. The deviation ordering (neutrino < down < lepton < up) is **reconstructed**.
3. However, the best charge/isospin combination correlates only **moderately** (|Q|,
   r = 0.588) and the **charge-sign gate fails** — a clean weak-isospin law is absent.

## Classification: **PARTIAL EFFECT**

- **NO EFFECT rejected**: the up/down split is strongly signed and the ordering is
  reconstructed.
- **ISOSPIN ORIGIN rejected**: no single isospin/charge combination reproduces the full
  hierarchy (correlations only moderate; charge-sign gate fails).
- **PARTIAL EFFECT accepted**: the up/down split is strongly isospin-signed, the ordering
  is reconstructed, and |Q| correlates moderately — but the amplification is not a clean
  weak-isospin law.

## Connection to the AT research arc

- QG143 PARTIAL FACTOR → QG144 tests the isospin hypothesis quantitatively: the signed
  structure is real, but no linear isospin/charge law captures it.
- The charge-SIGN gate failure is informative: leptons (Q=−1) track the octave law, so
  the amplification is NOT simply "charge magnitude" — it is specific to the up sector
  (Q=+2/3, T3=+1/2).
- The reconstructed ordering (neutrino < down < lepton < up) is a constraint for any
  future mechanism: it must order sectors by something that peaks at up and bottoms at
  neutrino.
- Open: what single quantity orders neutrino < down < lepton < up while leaving leptons
  at the octave baseline?
