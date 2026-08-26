# AT-QG Phase 146 — Quark Mass Hierarchy Law

**Status:** COMPLETED — CLASSIFICATION: **PARTIAL LAW**
**Tests:** ATQG1460, ATQG1461, ATQG1462 (3/3 pass)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG145 established that quark amplification arises from spectral structure × charge-isospin
interaction. This phase asks: **can the full up/down quark mass hierarchy be reproduced
from one spectral-interaction law?**

## Starting Point

- QG141: spectral-density exponents (p_net = 5.88).
- QG145: UP-SECTOR ORIGIN — amplification = spectral × charge×isospin cross term.

## Method

The spectral law fixes the within-sector octave ratios {1, 2^5.88, 4^5.88} = {1, 58.9,
3468}. If ONE law applies to both quark sectors, the within-sector ratios must be the
same for up and down. Five probes:

1. **Up-quark sector** — u/c/t within-sector ratios.
2. **Down-quark sector** — d/s/b within-sector ratios.
3. **Spectral density** — the octave spectral input (Weyl exponent, occupancy).
4. **Charge×isospin amplification** — the deviation factor vs the Q·(1+T3) cross term.
5. **Hierarchy reconstruction** — does a single law (shared exponents) reproduce both?

All probes are deterministic.

## Assumptions

1. A universal law ⇒ identical within-sector ratios for up and down.
2. The effective exponent p_eff = log(r31)/log(4) characterizes each sector's hierarchy.

## Results

### 1. Up-quark sector (ATQG1460)

```
octave ratios = [1, 58.9, 3468]
up within-sector ratios: r21=577.3, r31=78636
deviation: r21×9.8, r31×22.7  (amplified)
```

### 2. Down-quark sector (ATQG1460)

```
down within-sector ratios: r21=20.2, r31=889
deviation: r21×0.34, r31×0.26  (suppressed)
```

- Up is **amplified** (~23× at r31); down is **suppressed** (~0.26×).

### 3. Spectral density (ATQG1460)

- Weyl exponent = **2.473**; octave occupancy = **0.916** — well-defined spectral input.

### 4. Charge×isospin amplification (ATQG1461)

- **Pearson r(log2(factor), Q·(1+T3)) = 0.767** — the cross term correlates strongly.
- Effective exponents: **up p_eff = 8.131**, **down p_eff = 4.898** (octave baseline
  5.88). Up is steeper, down shallower.

### 5. Hierarchy reconstruction (ATQG1462)

- Exponent split |p_up − p_down|/|p_up| = **0.398**.
- Universal law (split < 15%): **False**.
- **A single law reproduces BOTH hierarchies: False.**
- Quark-hierarchy-law score **4 / 5**.

## Conclusions

1. The charge×isospin amplification is **real and strong** (r = 0.767).
2. Up and down have **different effective exponents** (8.13 vs 4.90) — the sectors do not
   share one within-sector structure.
3. **ONE universal spectral-interaction law does NOT reproduce both quark hierarchies.**

## Classification: **PARTIAL LAW**

- **NO LAW rejected**: the charge×isospin amplification is real (r≈0.77) and each sector
  deviates strongly.
- **QUARK HIERARCHY ORIGIN rejected**: a single universal law cannot reproduce both
  sectors (exponent split 39.8%).
- **PARTIAL LAW accepted**: the charge×isospin amplification is real and each sector
  deviates from the octave law, but the full up AND down hierarchies require
  sector-dependent exponents — not a single law.

## Connection to the AT research arc

- QG145 UP-SECTOR ORIGIN → QG146 confirms the cross-term amplification (r=0.767) but
  shows it is NOT a single universal law — up and down have different effective exponents.
- The result is consistent with QG142 (PARTIAL LAW): no single spectral-interaction law
  covers all fermion sectors.
- The up/down exponent difference (8.13 vs 4.90) is the remaining structure: a
  sector-dependent steepening beyond the octave baseline.
- Open: what sets the sector-dependent exponent (up 8.13, down 4.90, lepton 5.88)?
