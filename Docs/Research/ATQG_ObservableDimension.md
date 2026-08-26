# AT-QG Phase 5 — Observable Dimension

**Program:** AT-QG (Unification)
**Phase:** 5 — why does ρ vary along exactly d observable directions?
**Status:** COMPLETED — 3/3 xUnit tests pass (18/18 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

The observable dimension d is the support rank of ρ (the number of directions it varies along). Here we test
whether the actualization dynamics, entropy, branching efficiency, density dilution, or information capacity
selects d. Classify d=4: DERIVED / PREFERRED / NOT SELECTED.

---

## 2. Results

### (a) Entropy per active dimension is monotonic (ATQG50)

The maximum configurational entropy H_max = ln d + ln K is **strictly increasing** in d:

| d | H_max = ln d + ln 8 |
|---|---|
| 3 | 3.178 |
| 4 | 3.466 |
| 5 | 3.689 |
| 6 | 3.871 |

More active directions = more entropy (less bias). No interior maximum — entropy does NOT select a preferred d.

### (b) Dilution and branching efficiency are monotonic (ATQG51)

| d | dilution R^−d | μ_crit = λ^d | efficiency λ^−d |
|---|---|---|---|
| 3 | −3 | 3.375 | 0.296 |
| 4 | −4 | 5.063 | 0.198 |
| 5 | −5 | 7.594 | 0.132 |
| 6 | −6 | 11.391 | 0.088 |

Actualization dilutes faster and requires more branching in higher dimensions — all monotonic. No criterion
has an extremum at d=4.

### (c) Classification (ATQG52)

**NOT SELECTED** — the support rank is a conserved initial condition.

---

## 3. Classification: NOT SELECTED

- Entropy, dilution, branching efficiency, and information capacity are all **monotonic** in d — none selects
  a preferred observable dimension.
- The α=0 attractor dynamics (scale-space diffusion / entropy gradient flow) acts only on the **radial**
  (octave) structure and is **dimension-blind** (`DiffuseStep` operates on the octave index only, no
  dimension parameter).
- Therefore the support rank d (how many directions ρ varies along) is a **conserved initial condition**:
  any d is a stable fixed point of the dynamics; the dynamics neither selects nor destabilizes it.

d=4 is **NOT SELECTED** — it is supplied as the configuration of the actualization (which directions ρ happens
to vary along), not derived or preferred by any native criterion.

---

## 4. Conclusion

The observable dimension (the support rank of ρ) is **not dynamically selected**: entropy, dilution, branching
efficiency, and information capacity are all monotonic in d, and the actualization dynamics is dimension-blind
(radial-only). This completes the dimension arc with a coherent final picture:

- **AT-QG2** — d≥3 required for gravity (derived bound).
- **AT-QG3** — d=4 not native-special; preferred only as minimal propagating gravity (imported).
- **AT-QG4** — d is emergent as the support rank of ρ; fundamental D unconstrained.
- **AT-QG5** — the support rank itself is NOT selected; it is a conserved input.

The observable dimension is a **conserved configuration parameter** of the actualization field — supplied,
emergent-in-interpretation, but not derivable from the dynamics.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG50 `ATQG50_EntropyPerDimensionMonotonic` | PASS (entropy monotonic) |
| ATQG51 `ATQG51_DilutionBranchingMonotonic` | PASS (dilution/branching monotonic) |
| ATQG52 `ATQG52_Classification` | PASS (NOT SELECTED, conserved input) |

Code: `AT.Core/ResearchXH/ObservableDimension.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase5_ObservableDimensionTests.cs`.
