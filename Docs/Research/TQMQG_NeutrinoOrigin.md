# TQM-QG Phase 154 — Origin of the Neutrino Sector

**Status:** COMPLETED — CLASSIFICATION: **NEUTRINO ORIGIN**
**Tests:** TQMQG1540, TQMQG1541, TQMQG1542 (3/3 pass)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG138-QG153 derive families, hierarchies, mode access, and the Z2 doublet structure. QG148 showed the
linear exponent law overfits — the neutrino prediction (p = 9.113) deviates 103% from the observed
exponent (p = 4.48). This phase asks: **why does the neutrino sector deviate from the lepton and quark
scaling laws?**

## Starting Point

- QG148: OVERFIT — neutrino predicted p = 9.113 vs observed 4.483 (103% deviation).
- QG153: DOUBLET ORIGIN — the spectrum is fully Z2-paired; the pairs are weak-isospin doublets.

## Method

Five deterministic probes:

1. **Neutral-charge limit** — the neutrino is the unique Q = 0 fermion; its charge channel vanishes.
2. **T3-only access** — the neutrino reverts to T3-only Z2-channel spectral access.
3. **Doublet occupancy** — the T3=+1/2 vs T3=−1/2 member split within each weak doublet.
4. **Spectral accessibility** — the neutrino's effective dimension vs all sectors.
5. **Neutrino hierarchy** — p_eff = log(ν3/ν1)/log(4) with ν3/ν1 = 500.

All probes are deterministic.

## Assumptions

1. A sector's effective dimension δ_eff = p_eff/2 (QG149/150).
2. The neutrino is the only neutral fermion (Q = 0); charge-dependent mode access scales as Q^n.

## Results

### 1. Neutral-charge limit (TQMQG1540)

```
neutrino p_eff = log(500)/log(4) = 4.483
neutrino δ_eff = p/2 = 2.241
neutrino is the UNIQUE neutral (Q=0) sector: TRUE
charge amplification Q^n (n=6.47): 0.000E+000
```

- The neutrino is the **only** fermion with Q = 0. The charge-dependent mode access (QG143 charge power
  n ≈ 6.47) vanishes identically.

### 2. T3-only access (TQMQG1540)

```
T3=+1/2 Z2 channel Weyl = 2.3189
neutrino δ vs channel Weyl deviation = 3.3%
```

- With no charge channel, the neutrino reverts to T3-only Z2-channel spectral access — its dimension
  matches the Weyl of one member of each doublet (3.3% deviation).

### 3. Doublet occupancy (TQMQG1541)

```
quark (u,d):  r31 ratio = 88.4   log2 = 6.47   (up enhanced)
lepton (ν,e): r31 ratio = 6.95   log2 = 2.80   (electron enhanced)
```

- In the quark doublet the up (T3=+1/2) member is enhanced. In the lepton doublet the **electron** is
  enhanced and the neutrino is the suppressed neutral member — the doublet is inverted for the neutrino.

### 4. Spectral accessibility (TQMQG1541)

```
leptons: δ = 2.940
up:      δ = 4.066
down:    δ = 2.449
neutrino: δ = 2.241   ← MINIMUM
neutrino δ / full-spectrum Weyl = 0.906
```

- The neutrino has the **lowest** effective dimension of all fermion sectors — below even the full-spectrum
  Weyl.

### 5. Neutrino hierarchy (TQMQG1542)

```
ν3/ν1 = 500, p_eff = 4.483
QG147 linear law prediction = 9.113 (deviation 103.3%)
linear law fails for the neutrino: TRUE
```

- The QG147 law overfits precisely because it predicts a charge-enhanced neutrino that cannot exist.

### Classification (TQMQG1542)

```
neutrino-origin score: 5/5
CLASSIFICATION: NEUTRINO ORIGIN
```

## Conclusions

1. The neutrino is the unique neutral fermion — its charge channel vanishes identically (Q^n = 0).
2. Without the charge channel, the charge×isospin enhancement (QG145) that boosts other T3=+1/2 sectors
   cannot act.
3. The neutrino reverts to T3-only Z2-channel spectral access (δ ≈ T3=+1/2 channel Weyl, 3.3%).
4. It becomes the lowest (suppressed) sector — δ = 2.24, below even the full-spectrum Weyl.
5. The QG147 linear law overfits because it cannot represent this neutral-charge limit.

## Classification: **NEUTRINO ORIGIN**

- **NO ORIGIN rejected**: the neutral-charge limit, T3-only access, and doublet inversion provide a
  complete mechanism.
- **PARTIAL ORIGIN rejected**: all five conditions hold (score 5/5).
- **NEUTRINO ORIGIN accepted**: the neutrino deviates because it is the ONLY neutral fermion. The
  charge-dependent mode amplification vanishes identically (Q^n = 0), the charge×isospin enhancement
  cannot act, and the neutrino reverts to T3-only Z2-channel spectral access, making it the lowest
  (suppressed) sector. This explains why the QG147 linear law overfits.

## Connection to the TQM research arc

- QG148's neutrino prediction failure is now EXPLAINED: the linear law cannot represent the neutral-charge
  limit.
- The mechanism is the neutral analogue of the up-sector enhancement (QG145): charge×isospin coupling
  boosts charged T3=+1/2 sectors (up); its absence suppresses the neutral T3=+1/2 sector (neutrino).
- Consistent with the Z2 doublet structure (QG153): the neutrino occupies the T3=+1/2 member of the lepton
  doublet but without the charge channel it cannot access the enhanced dense band (QG150).
- The neutrino hierarchy (ν3/ν1 = 500) is the suppressed neutral-sector hierarchy — the open "neutrino
  hierarchy" problem is now given a structural origin.
