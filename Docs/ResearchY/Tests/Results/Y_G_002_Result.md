# Y_G_002_Result.md — ResearchY-G_002 Density Control Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_002_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED (155 ms)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_002"`

---

## Summary

**Question:** Can the actualization density ρ change independently of mass-energy?

**Verdict:** **YES — CONTROLLABLE.** ρ can change at fixed total mass-energy.

**Reason (structural, not dynamical):** count conservation makes the **total deficit vanish
identically** (`Σm = 0`, QG194), so the *arrangement* of ρ is never tied to the energy. What the
arrangement is tied to is the **degeneracy structure**, whose free directions number `N − A₀`.

| # | Candidate | ΔE/E | L1(Δρ) | max\|Δa\| | Verdict |
|---|---|---|---|---|---|
| 1 | spectral organization | **0.0000** | 0.7583 | 0.685714 | **CONTROLLABLE** |
| 2 | phase coherence | 0.0000 | **0.0000** | **0.000000** | **REFUTED** |
| 3 | degeneracy structure | **0.0000** | 0.6667 | **0.603175** | **CONTROLLABLE** |
| 4a | survivor compression, total fixed | **0.0000** | 2.87 / 5.16 | 0.032121 | **CONTROLLABLE** |
| 4b | survivor deletion, total not fixed | **−46.8% / −73.0% / −86.4%** | — | — | **CORRELATED** |
| 5 | D96 vs random | **0.0000** | 0.5313 | 0.333333 | **CONTROLLABLE** |
| 6 | D96³ vs D96 | **0.0000** | 0.9990 | 0.276596 | **CONTROLLABLE** |
| c | rescaling ρ → 3.7ρ (control) | **+270%** | 2.7000 | **0.000000** | **CORRELATED** |

## Baseline

- Canonical measure (uniform within multiplets): `Σρ = 1.000000000000`, `Σm = 0.000000000000`
  **exactly** — the total deficit vanishes (QG194).
- `max|a| = 0` and `max|R| = 0` to 12 dp: the canonical measure is the **exact zero-field
  configuration**.

## Real lattices (shared catalog)

| Lattice | modes | A₀ | L = (N−A₀)/N | pattern | lock release |
|---|---|---|---|---|---|
| D96 | 96 | 45 | 0.53125 | 42×2, 5, 6, 1 | 0.80231 |
| random | 96 | 96 | 0 | all singletons | 0 |
| D96³ | 884 736 | 20 812 | 0.97648 | max multiplicity 562 | — |

## The free room IS the degeneracy structure

```
independent energy-free directions = Σ (m_i − 1) = N − A₀
D96:     51 / 96        = 0.53125  = the D_048 latent fraction L exactly
random:   0 / 96        = 0        (no free room)
D96³:  863 924 / 884 736 = 0.97648
```

**New cross-link:** the D_048 *latent fraction* `L = (N − A₀)/N` is exactly the fraction of
energy-free reconfiguration directions of the counting measure — the adaptability ceiling of D_048
and the free room of ρ are the same number.

## Key witness (degeneracy)

A redistribution **inside** degenerate multiplets (80% of each multiplet's share on its first cell)
leaves every per-multiplet total identical to 12 dp — so the spectrum and all mode energies are
untouched — at `ΔE = 0` exactly, and takes the field from **exactly zero** to `max|a| = 0.603175`.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_G_002_Baseline` | Σm = 0, zero-field baseline, real lattice structure | ✅ |
| `Y_G_002_SpectralOrganizationControllable` | permutation at ΔE = 0; a sign flip at every probe | ✅ |
| `Y_G_002_PhaseCoherenceRefuted` | ρ, a, R phase-blind; interference 4 → 2 → 0 | ✅ |
| `Y_G_002_DegeneracyStructureControllable` | block sums fixed; 0 → 0.603; free room = N − A₀ | ✅ |
| `Y_G_002_SurvivorCompression` | ΔE = 0 with the total fixed; ΔE ≠ 0 without | ✅ |
| `Y_G_002_LatticeChoiceD96VsRandom` | random is the exact zero-field null | ✅ |
| `Y_G_002_LatticeChoiceD96Cubed` | 97.6% vs 53.1% free; different field | ✅ |
| `Y_G_002_RescalingCorrelated` | E ∝ λ, a exactly invariant, R ∝ λ^(−2/d) | ✅ |
| `Y_G_002_Run` | research report | ✅ |

## Classification

Derived: the CONTROLLABLE verdict, the `N − A₀` free-direction count, the exact-ΔE rearrangements, the
free-room = `L` identification, the zero-field baseline, phase-blindness. Boundary: `ρ̄ = 1/N`, the
exponent `2/d`, the cell-lattice definition. Emergent: the particular witness tilt (one member of a
free family). **No reclassification** — the D_040 `ClassificationRegistry` is untouched and G_001's
verdicts are used, not modified.

## Conclusion

ρ is **CONTROLLABLE**: spectral organization, degeneracy structure, survivor compression (at fixed
total) and the lattice choice all change ρ with `ΔE = 0` exactly, while phase coherence is **REFUTED**
(it changes nothing) and mere rescaling is **CORRELATED** (it changes the energy and nothing else). The
free room is exactly the degeneracy structure, `N − A₀` directions (51 of 96 for D96 = the D_048
latent fraction; 0 for a degeneracy-free lattice). This sharpens G_001's open problem OP1: the profile
of ρ cannot be fixed by conservation — a dynamical principle is genuinely required.
