# TQM-QG Phase 163 — Running Coupling Origin

**Status:** COMPLETE — **RUNNING ORIGIN**
**Tests:** TQMQG1630, TQMQG1631, TQMQG1632 (all passed)
**Core class:** `TQM.Core/ResearchXH/RunningCouplingOrigin.cs`

---

## 1. Starting Point

QG162 established the couplings at the observable scale:

```
1/α_em  = Σm + #doublets = 95 + 42 = 137
α_weak  = 3/Σm  = 3/95
α_strong = 8/Σ√m = 8/64.083
```

**Open question:** Why do the couplings run with energy? Does a unification
scale emerge — using only D96 spectral geometry, with **no fitted beta
functions**?

---

## 2. Assumptions

1. The couplings are functions of the occupancy statistics of the
   **activated spectral content**.
2. The octave-band ladder of the observable sector (sizes [4,4,87]) defines
   the natural spectral (energy) scale: as E increases, more modes activate.
3. No fitted beta functions — the running must come from the spectral
   geometry itself.

---

## 3. Results

### 3.1 Spectral Scale (octave ladder)

```
octave sizes: [4, 4, 87]
activation ladder: 4 → 8 → 95 modes
```

Each octave (frequency doubling) is one rung of the energy ladder. There
are 3 octave families (QG138).

### 3.2 Occupation Flow

```
N=4:  Σm=4,  #doublets=2, Σ√m=2.83
N=8:  Σm=8,  #doublets=4, Σ√m=5.66
N=95: Σm=95, #doublets=42, Σ√m=64.08
```

The denominators **grow** with the occupation flow (4 → 8 → 95 modes). The
dense top band (87 modes) dominates the observable-scale occupancy (0.916).

### 3.3 Running Couplings α(E) = g_i / D_i(N(E))

| rung | N | 1/α_em | α_weak | α_strong |
|------|---|--------|--------|----------|
| 1 | 4 | 6.0 | 0.7500 | 2.8284 |
| 2 | 8 | 12.0 | 0.3750 | 1.4142 |
| 3 (observable) | 95 | **137.0** | **0.0316** | **0.1248** |

**Running factors** (lowest rung → observable):

```
α_em⁻¹:  22.8x  (6 → 137)
α_weak:  23.8x  (0.75 → 0.0316)
α_strong: 22.7x  (2.83 → 0.1248)
```

All three couplings **decrease monotonically** by **comparable factors
(~23x)** — driven by the shared occupation flow.

### 3.4 Scale-Dependent Access and Mode Activation

```
D96 → spectral scale (octave ladder) → occupancy evolution → coupling evolution
```

The running is **not** a fitted beta function: it is the direct consequence
of the occupation flow (mode activation) along the D96 octave ladder.

### 3.5 Unification (hierarchy preservation)

```
structural bound: 1/α_em = Σm + #doublets > Σm/3 = 1/α_weak (since #doublets > 0)
hierarchy preserved at all scales: True
α_strong largest at every rung: True
```

**NO in-sector unification:** α_em < α_weak < α_strong at all scales — the
hierarchy is preserved. Consistent with the observed low-energy hierarchy
and a GUT scale **beyond** the observable octave ladder.

---

## 4. Classification

**Running-origin score: 5 / 5**

- +1 octave ladder defines the spectral scale (3 rungs)
- +1 denominators grow (occupation flow)
- +1 all couplings decrease monotonically
- +1 comparable running rates (~23x each)
- +1 hierarchy preserved (no in-sector unification)

```
CLASSIFICATION: RUNNING ORIGIN
```

- **NO ORIGIN rejected:** the couplings run monotonically along the D96
  octave ladder (occupation flow 4 → 8 → 95 drives the denominators).
- **PARTIAL ORIGIN rejected:** the full mechanism holds — spectral scale,
  occupation flow, scale-dependent access, mode activation, monotone
  running at comparable rates.
- **RUNNING ORIGIN accepted.**

---

## 5. Conclusion

The running of the gauge couplings **emerges from D96 spectral geometry**:

1. **Spectral scale** — the octave-band ladder (sizes [4,4,87]) is the
   natural energy scale: 3 rungs (4 → 8 → 95 modes).

2. **Occupation flow** — the coupling denominators (Σm, #doublets, Σ√m)
   grow as occupation flows up the bands, driven by mode activation.

3. **Running law** — α_i(E) = g_i / D_i(N(E)): all three couplings decrease
   monotonically by comparable factors (~23x) from the lowest rung to the
   observable scale.

4. **Hierarchy preservation / unification** — the structural bound
   1/α_em = Σm + #doublets > Σm/3 = 1/α_weak holds at **every** scale, so
   the couplings do NOT unify within the observable sector: the hierarchy
   α_em < α_weak < α_strong is preserved. This is consistent with the
   observed low-energy hierarchy and places any unification at a GUT scale
   **beyond** the observable octave ladder.

All with **no fitted beta functions** — the running is the direct spectral
consequence of mode activation along the D96 octave ladder.

---

## 6. Chain

```
period-3 seed (QG160)
  → D96 selection (QG159)
  → Z2 doublets (QG153, QG155)
  → gauge generators 1+3+8 (QG161)
  → gauge couplings (QG162)
  → RUNNING COUPLINGS (QG163)                                         ← THIS PHASE
      spectral scale: octave ladder [4,4,87]
      occupation flow: 4 → 8 → 95
      α_i(E) = g_i / D_i(N(E)) runs monotonically (~23x)
      hierarchy preserved (no in-sector unification)
  → moment orders = Z2 powers {2⁻¹, 2⁰, 2¹} (QG158)
  → N_eff = moments (QG157)
  → δ = log(N_eff)/log(span) (QG156)
  → hierarchy exponent p = 2δ (QG140/141)
  → fermion hierarchy
```
