# Fundamental Charge Theory

## TQM-120: Why Q = +1 Is the Minimal Charge Quantum

### Abstract

We prove that the topological charge Q = β₀({R>0.5}) is the minimal
conserved charge in the TQM reaction-diffusion field theory. Five
fragmentation methods fail to produce any valid sub-Q structure.
The charge quantum is Q=+1 — one kink-antikink pair — and this
follows from homological, topological, and dynamical arguments.

### 1. Homological Argument

Q is defined as the Betti number β₀ of the superlevel set:

```
Q = β₀(S_T)  where  S_T = {x ∈ Ω : R(x) > T}
```

with T = 0.5.

Betti numbers are topological invariants of the space S_T:
- β₀ counts connected components
- β₀ ∈ ℕ (non-negative integers)
- β₀ is invariant under homeomorphisms (continuous deformations
  that don't cross the threshold)

Since β₀ is inherently integer-valued, Q cannot be fractional.
This is a homology-level statement — no continuous deformation of
R(x) can produce β₀ = 0.5. Q changes only when R crosses T at some
point x, which is a discrete topological event.

### 2. Topological Protection

The one-way barrier:

```
c₀·M·R·(1−R²) > 0   for R ∈ (0,1), M > 0
```

means that R cannot spontaneously decrease inside a condensate.
Once R > 0.5, the reaction term pushes R → 1.

This creates an ASYMMETRIC BARRIER:
- Crossing 0.5 upward: possible (via nucleation, TQM-118)
- Crossing 0.5 downward: FORBIDDEN (requires extreme external forcing, TQM-011)

The barrier is one-way. Components can be BORN (R crosses 0.5 upward)
but cannot DIE (R cannot cross 0.5 downward) under PDE evolution.
This is exactly why Q is conserved.

Below 0.5, there is NO topological protection. R can evolve freely
below the threshold. Components detected at lower thresholds
(T=0.3, T=0.4) are dynamical fluctuations that can appear and
disappear continuously — they are NOT topological charges.

### 3. Kink-Antikink Pair Structure

Each condensate consists of:
- One KINK:    R crosses 0.5 upward   (0 → 1 transition)
- One ANTIKINK: R crosses 0.5 downward (1 → 0 transition)

Under R(0)≈R(L)≈0 boundary conditions:
- Kinks and antikinks MUST appear in pairs
- An individual kink without matching antikink would leave R>0.5
  at a boundary, which violates boundary conditions
- Net charge = (kinks − antikinks)/2 = 0 (always)
- But Q = kink-pair count = number of condensates

The PAIR is the minimal unit. You cannot have half a pair.

### 4. Morse-Theoretic Perspective

The Morse index counts critical points:
- Local maxima (R">0, usually peaks of condensates)
- Local minima (R">0, valleys between condensates)
- Saddles (one direction up, one down)

Morse inequality: #{maxima} ≥ β₀ + #{saddles between components}

For well-separated condensates: #{maxima with R>0.5} = Q.

Sub-threshold maxima (with R<0.5) correspond to noise structure.
They contribute to the total Morse count but NOT to Q.
The Morse-theoretic Q filters by the 0.5 threshold, which is
the physically motivated barrier height.

### 5. Persistent Homology

In persistent homology, we track how connected components
of S_T = {R>T} appear (birth) and disappear (death) as T varies.

A component born at T_birth and dying at T_death has:
- Persistence = T_birth − T_death (lifetime in threshold space)

Features with HIGH persistence (spanning most of [0,1]):
→ GENUINE CHARGES (condensates)

Features with LOW persistence (birth≈death):
→ NOISE (random fluctuations)

Features with INTERMEDIATE persistence:
→ WOULD indicate sub-Q structure (partial condensates)
→ NOT FOUND in any test

The absence of intermediate-persistence features is strong evidence
that no sub-Q structure exists.

### 6. Continuous Charge Candidates

The "coherence excess":

```
Q_c = ∫_{R>0.5} (R(x) − 0.5) dx
```

is CONTINUOUS and non-integer. However, it fails as a charge:

1. NOT conserved: dQ_c/dt = ∫ ∂R/∂t dx = ∫ c₀·M·R·(1−R²) dx > 0
   The reaction term continuously increases Q_c.

2. NOT quantized: Q_c varies continuously with R.

3. NOT topological: Q_c changes under continuous deformations
   of R that preserve the topology of {R>0.5}.

Q_c is a DIAGNOSTIC (measures condensate strength), not a charge.

### 7. Merger Transitions

During Q=2→Q=1 merger (TQM-012):
- Two condensates approach within coupling range
- They phase-lock rapidly (within ~250 iterations)
- The two kink-antikink pairs merge into one pair
- Q changes by -1 (discrete, integer)

Is there a Q=1.5 state during the transition?
NO — at any instant, the R-field either has:
- Two separate R>0.5 domains → Q=2
- One merged R>0.5 domain → Q=1

There is no continuous intermediate. The transition is discrete
at the resolution of the coupling range.

### 8. Conclusion

**Q IS THE MINIMAL CHARGE QUANTUM.**

The topological charge Q = β₀({R>0.5}) = +1 per condensate is the
smallest possible unit of conserved topological charge in the
TQM field theory. No amount of fragmentation — threshold lowering,
kink isolation, Morse decomposition, continuous charges, or
persistent homology — can produce a valid sub-Q structure.

Q=+1 is the charge quantum. The quantization is topological:
Betti numbers are integers by definition, and the one-way
reaction barrier enforces the conservation that makes Q
meaningful as a charge.
