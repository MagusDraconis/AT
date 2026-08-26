# Minimal Proto-Matter Theory

## AT-122: The Origin of Q=+1 as the Minimal Stable Structure

### Abstract

We derive why Q=+1 is the minimal stable topological charge quantum
from the reaction-diffusion field theory. Three independent mechanisms
converge: (1) β₀=1 is the smallest non-zero Betti number, (2) the
kink-antikink pair is the minimal closed topological unit under
R(0)≈R(L)≈0, and (3) the minimum stable width w_c enforces a
lower bound on condensate size.

### 1. The Three Pillars

#### Pillar A: Discrete Spectrum (Topology)

Q = β₀({R>0.5}) counts connected components. β₀ ∈ ℕ by definition.
The smallest non-zero value of any count is 1.

```
Q ∈ {0, 1, 2, 3, ...}
```

There is no β₀ = 0.5 — a component either exists or doesn't.

#### Pillar B: Closed Topology (Kink-Antikink)

With boundary conditions R(0)≈0, R(L)≈0:

```
Any R>0.5 region requires:
  KINK:    R crosses 0.5 upward  (enters region)
  ANTIKINK: R crosses 0.5 downward (exits region)
```

One kink without antikink → R>0.5 at a boundary → violates BCs.
The PAIR is the minimal topologically closed unit.

#### Pillar C: Minimum Stable Width (Reaction-Diffusion)

At the condensate boundary R=0.5:

```
Reaction:  c₀·M·0.5·(1−0.5²) = 0.375·c₀·M
Diffusion: D_R·0.5/w²

Stability requires: reaction ≥ diffusion
  → 0.375·c₀·M ≥ D_R·0.5/w²
  → w² ≥ 4D_R/(3c₀·M)
  → w_c = √(4D_R/(3c₀·M))
```

Structures with w < w_c are UNSTABLE — diffusion dominates,
R drops below 0.5, component vanishes.

### 2. Numerical Values

| M | w_c | w_c in grid cells (30×30) |
|---|-----|---------------------------|
| 0.1 | 0.163 | ~5 cells |
| 0.5 | 0.073 | ~2 cells |
| 1.0 | 0.052 | ~1.5 cells |
| 2.0 | 0.037 | ~1 cell |
| 5.0 | 0.023 | <1 cell |

For typical coupling M≈1: w_c ≈ 0.05, corresponding to a
condensate spanning ~2-3 grid cells at 30×30 resolution.

### 3. The Convergence

The three pillars converge on the same conclusion:

1. **β₀=1** (A): the smallest non-zero charge VALUE
2. **Kink-pair** (B): the smallest CLOSED configuration
3. **w_c** (C): the smallest STABLE configuration

Q=+1 is simultaneously:
- The smallest non-zero Betti number
- The result of exactly one kink-antikink pair
- A condensate of minimum stable width w_c

There is no configuration that satisfies only 1.5 of these —
all three point to exactly Q=+1 as the minimal unit.

### 4. The Critical Droplet Analogy

In classical nucleation theory, droplets have a critical radius r_c:
- r < r_c: surface tension dominates → evaporates
- r > r_c: bulk energy dominates → grows

AT's Q=+1 is exactly the critical droplet:
- w < w_c: diffusion (surface tension) dominates → evaporates
- w > w_c: reaction (bulk energy) dominates → stable condensate

The critical droplet has ONE copy of the stable phase (R>0.5)
surrounded by the metastable phase (R<0.5). That's exactly Q=+1.

### 5. Stability of the Minimal Structure

The Q=+1 condensate is stable because:
1. Reaction pushes R→1 inside (grows the ordered phase)
2. The boundary at R=0.5 is stationary (reaction = diffusion)
3. R cannot cross 0.5 downward (one-way barrier)
4. The condensate is the global minimum of the local free energy

Destruction requires:
- External perturbation (AT-011: density -50%)
- Merger with another condensate (Q=2→Q=1)
- Catastrophic parameter change (M→0, c₀→0)

### 6. Universality

The minimum charge Q=+1 is universal because:
- β₀=1 is universal (mathematical tautology)
- The kink-pair structure is universal (boundary conditions)
- w_c > 0 is universal (any finite D_R with c₀>0)

No parameter values can produce Q<1 because:
- Q is a Betti number (always integer)
- The only integers below 1 are 0 and negative
- Q<0 is forbidden by boundary conditions
- Q=0 is the vacuum

Therefore: Q=+1 is the UNIVERSAL charge quantum across all
K, λ, N, and across all physically realizable parameter regimes.

### 7. Conclusion

The charge quantum Q=+1 is a NECESSITY, not an accident. It follows
from three independent requirements — topology, closure, and
stability — that all point to the same minimal unit. The
convergence of these three lines of reasoning elevates Q=+1 from
an empirical observation to a derived mathematical-physical
principle. Proto-matter exists in integer units because Betti
numbers are integers, and the minimal stable unit is Q=+1 because
that is the smallest non-zero integer that satisfies all three
requirements simultaneously.
