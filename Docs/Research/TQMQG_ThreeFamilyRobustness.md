# TQM-QG Phase 136 — Robustness of the 3-Family Sector

**Status:** COMPLETED — CLASSIFICATION: **PARTIAL ROBUSTNESS**
**Tests:** TQMQG1360, TQMQG1361, TQMQG1362 (3/3 pass; 414/414 TQMQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG135 found 3 families emerge from octave structure but the family count changes under
damping. This phase asks: **is there a dynamical regime where the 3-family structure
becomes stable and parameter-independent?**

## Starting Point

- QG135: PARTIAL ORIGIN — 3 families at default, 4 at high damping (d=0.4 at low f).

## Method

Five robustness probes on the observable sector (family count from its intra-sector
octave spectrum):

1. **Feedback sweep** — family count across the feedback axis.
2. **Damping sweep** — family count across the damping axis.
3. **Size scaling** — family count across network sizes (48–192).
4. **Family stability basin** — 3-family fraction of the refined (feedback × damping) grid.
5. **Universality** — is the 3-family state size-independent?

All probes are deterministic.

## Assumptions

1. The observable sector's family count = its intra-sector octave-family count (QG135).
2. A "coherent basin" means ≥ 90% of the refined parameter grid gives 3 families.
3. "Universal" means all tested network sizes give 3 families.

## Results

### 1. Feedback sweep (TQMQG1360)

```
f=0.2: 4   f=0.3: 4   f=0.4: 4   f=0.5: 4   f=0.6: 4
f=0.7: 3   f=0.8: 3   f=0.9: 3   f=1.0: 3
```

- **High feedback (f ≥ 0.7) gives exactly 3 families.**
- Low feedback gives 4 — the 3-family regime is feedback-gated.

### 2. Damping sweep (TQMQG1360)

```
d=0.1: 3   d=0.2: 3   d=0.3: 3   d=0.4: 3   d=0.5: 4
```

- **Low-to-moderate damping (d ≤ 0.4) gives 3 families** at default feedback.

### 3. Size scaling (TQMQG1361)

```
n= 48: 2 families
n= 64: 3 families
n= 96: 3 families
n=128: 4 families
n=192: 4 families
```

- The 3-family structure holds at **moderate sizes (64–96)** but **not at small (48) or
  large (128+) sizes** — the structure is NOT size-independent.

### 4. Family stability basin (TQMQG1361)

- **3-family fraction of the refined f×d grid (f 0.6–1.0, d 0.05–0.35) = 0.937** — a
  coherent, dominant 3-family basin exists.
- Coherent basin (≥ 0.9): **True**.

### 5. Universality (TQMQG1362)

- Default point (f=0.9, d=0.3) → 3 families: **True**.
- Coherent basin: **True**.
- Size-independent: **False** (2 at n=48, 4 at n≥128).

## Conclusions

1. A **coherent dynamical regime exists** where the 3-family structure is stable: high
   feedback (f ≥ 0.7), low-to-moderate damping (d ≤ 0.4), at moderate sizes.
2. The basin is **wide** (93.7% of the refined grid at n=96).
3. However, the family count **depends on network size** (2 at n=48, 4 at n≥128) — the
   3-family state is **not universal**.

## Classification: **PARTIAL ROBUSTNESS**

- **FRAGILE rejected**: a coherent 3-family basin exists (93.7%).
- **ROBUST ORIGIN rejected**: the family count depends on network size.
- **PARTIAL ROBUSTNESS accepted**: the 3-family state is stable in a coherent dynamical
  basin (high feedback, low damping) but is not universal across network sizes.

## Connection to the TQM research arc

- QG135 PARTIAL ORIGIN → QG136 identifies the robust regime: high feedback, low damping
  gives a wide 3-family basin.
- The size-dependence (2 at small n, 4 at large n) suggests a **finite-size selection** of
  the family count — the observed 3-generation structure corresponds to a specific
  network size range.
- QG119/120 horizon/finite-size effects → the same finite-size dependence that hides
  families from local observers also sets the generation count.
- The coherent basin (f≥0.7) matches the strong-feedback regime of QG116 (universal
  attractor), linking the 3-family robustness to the universal-attractor dynamics.
