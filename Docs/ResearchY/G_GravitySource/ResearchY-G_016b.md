# ResearchY-G_016b — Mass Independence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_016b (permanent)
**Title:** Mass Independence Audit — can two states share an energy, or share a density, without sharing the other?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_016b.md`
**Depends on:** ResearchY-G_016 (ρ is more primitive than mass-energy; `E = ⟨λ,ρ⟩` is a rank-1 pairing with a
94-dimensional kernel), G_002 (ρ is free at fixed total energy), G_005 (the Poisson band), G_007 (the D96 form
and its BOUNDARY values), G_009 (the clock law and the galactic cross-check), G_011 (phase-inertness),
G_014 (the κ = 1 carrier); AT-QG QG180/QG181 (`E = Σmλ`), QG194 (`Σρ = 1`), QG197 (`g₀₀ = −ρ^(2/d)`), QG220
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_016b_Tests.cs` (7/7 PASSED, ~0.07 s)

## Purpose

G_016 established that ρ is more primitive than mass-energy: `E = ⟨λ,ρ⟩` is a rank-1 pairing with a
94-dimensional kernel. G_016b asks the operational version of that claim, in both directions, with explicit
examples:

> **Can two states have the same energy but different ρ, or the same ρ but different energy?**
> Construct explicit examples. Measure `ΔE`, `Δρ`, `Δτ`.     Output: **INDEPENDENT / DEPENDENT / REFUTED**.

**Answer.** One label per direction, and they are not symmetric:

| direction | label | why |
|---|---|---|
| same `E`, different `ρ` | **INDEPENDENT** | the kernel is **94-dimensional** on `{Σρ = 1}`: **51** degeneracy (λ constant within a multiplet) + **43** λ-mixing (zero net over *distinct* λ) |
| `E` given `ρ` | **DEPENDENT** | `E` is a **function** of ρ: one ρ, one E. The independence is strictly one-directional |
| same `ρ`, different `E` | **REFUTED** | impossible as a *state* change on a fixed lattice; only the **capacity** K moves it — and K is **BOUNDARY** (G_007) |

## 1. INDEPENDENT — the 51-dimensional degeneracy room

The canonical witness tilt is a **pure within-multiplet move** (λ is constant inside a multiplet, so `E` is
invariant *exactly*):

| measure | value |
|---|---|
| `ΔE` | **0** (exactly in the algebra; `Assert.Equal(0.0, …, 12)`) |
| `Δρ = L1(Tilt, uniform)` | **0.6666666666666667** |
| contrast | **20 : 1** |
| `Δτ/τ = (1/d)ln(contrast)` | **0.9985774245179969** = **86 277.089 s/day** |

A **pairwise** move inside an m = 2 multiplet (Δρ = 2δ exactly):

| δ | `Σρ − 1` | `ΔE` | `Δρ` | contrast | `Δτ/τ` |
|---|---|---|---|---|---|
| 0.002 | 0 | −1.776e-15 | 0.004 | 1.4752 | 0.129609 |
| 0.005 | 0 | −1.776e-15 | 0.010 | 2.8462 | 0.348656 |

Both moves live in the **51-dimensional** room `Σ(m−1)` over the 45 eigenspaces.

## 2. INDEPENDENT — the 43-dimensional λ-mixing room

The other half of the kernel is *not* degeneracy: it mixes **distinct** λ with zero net. Explicitly, cells
94 / 92 / 90 with

```
λ = 15.837372467014836, 15.790176186632262, 15.414213562373096   (three different multiplets)
v = (1, -1.1255345008711273, 0.12553450087112727)   with   Σv = 0   and   ⟨λ,v⟩ = 0   EXACTLY
```

At scale 0.004 (so ρ ≥ 0 holds):

| measure | value |
|---|---|
| `ΔE` | **−1.776e-15** (rounding; 0 in the algebra) |
| `Δρ = L1` | **0.009004** |
| `ρ_min` | 0.005915 |
| contrast | **2.437501** |
| `Δτ/τ` | **0.29699105** = **25 660.0263 s/day** |

So **both** halves of the kernel are physically populated: 51 directions that are energy-free *by symmetry* and
43 that are energy-neutral *by cancellation*.

## 3. DEPENDENT — E is a function of ρ

`E = ⟨λ,ρ⟩` is single-valued in ρ: one ρ gives one E, on every evaluation. Therefore the independence in §1–§2
is **strictly one-directional** — many ρ per E, never many E per ρ. ρ is the argument; E is the value.

## 4. REFUTED — same ρ, different E

Holding ρ fixed, `E` cannot move on a fixed lattice. The **only** handle is the capacity λ, and the cleanest
demonstration uses the *same* ρ vector for every coupling range K:

| K | `A₀` | `Σλ` | `E(uniform) = ⟨λ,ρ⟩` |
|---|---|---|---|
| 1 | 49 | 192 | **2.0** |
| 2 | 47 | 384 | **4.0** |
| 3 | 45 | 576 | **6.0** |
| 4 | 47 | 768 | **8.0** |
| 5 | 45 | 960 | **10.0** |
| 6 | 45 | **1152** | **12.0** |

`Σλ = 192K` and therefore **`E = 2K` exactly**, while `L1(ρ_K, ρ_6) = 0` for every K: the *same*
uniform counting measure takes six different energies depending only on how far the coupling reaches. So
"same ρ, different E" is **REFUTED as a state change** — it requires K, which G_007 established is a
**BOUNDARY** input, not something a state can vary.

## 5. The phase sector: same ρ, same E, same Δτ

Four phase assignments on the witness (canonical grid `θ_j = 2πj/N`, a global shift, the mirror, fully locked):
`L1(|ψ|², ρ) < 2.5e-16`, `ΔE = 0` exactly and `Δτ = 0` exactly — while the coherent sum moves by

```
|Σψ| : 0.03241962809423954  →  9.117182187865382      ratio = 281.22414487183346
```

exactly reproducing G_011/G_014. So the one degree of freedom *outside* ρ changes neither the energy nor any
clock: the phase sector is **energy-inert and clock-inert**.

## 6. The fixed-E family — and why Δτ is UNBOUNDED

Tilt every multiplet with fraction `f` on its first cell (`fr = 1` for m = 1). Then **exactly**

```
ΔE = 0 ,     contrast = 5f/(1−f)   (f ≥ 1/2) ,     L1(f) = (2/96)[42|2f−1| + |5f−1| + |6f−1|] ,
Δτ/τ = (1/d) ln(5f/(1−f)) = T      ⇔      f = 1/(1 + 5 e^{−3T}) .
```

| f | `ΔE` | `Δρ = L1` | contrast | `Δτ/τ` |
|---|---|---|---|---|
| 0.2 | −1.8e-15 | 0.5291666667 | 4.000 | 0.462098 |
| 0.5 | −1.8e-15 | 0.0729166667 | 5.000 | 0.536479 |
| **0.8** | **0** | **0.6666666667** | **20.000** | **0.998577** |
| 0.9 | −3.6e-15 | 0.8645833333 | 45.000 | 1.268888 |
| 0.99 | −3.6e-15 | 1.0427083333 | 495.000 | 2.068186 |
| 0.999 | −3.6e-15 | 1.0605208333 | 4 995.000 | 2.838731 |
| `f → 1⁻` | **0** | **→ 1.0625** | **→ ∞** | **→ ∞** |

**Two facts fall out.** (1) At the canonical `T = (1/d)ln20` the inversion returns **f = 0.8** — the canonical
`TiltFractions` *is* the 20 : 1 solution of this family. (2) Since `L1 → 1.0625` and `ρ_min → 0` while
`ΔE = 0` throughout, **the clock separation at fixed energy is unbounded**: `f(1.5) = 0.947377910367`,
`f(2) = 0.987757963984`, `f(3) = 0.999383331494`, `f(5) = 0.999998470491`. **No target `Δτ` is forbidden by
energy conservation.** What bounds it is positivity together with the G_005 Poisson band:
`T = 4.8867e-6` needs only `f = 0.1666687028016166`, a **1.0000146602074598 : 1** contrast
(0.14073696 s/day realised, against 0.04637376 s/day observed).

## 7. Verdicts

| label | content |
|-------|---------|
| **INDEPENDENT** | **same energy, different ρ** — the kernel of `ρ ↦ E` on `{Σρ = 1}` is **94-dimensional**, explicitly **51 (degeneracy) + 43 (λ-mixing)**. The canonical witness moves ρ by `L1 = 0.6666666666666667` at `ΔE = 0` exactly while the clocks separate by **0.9985774245179969 = 86 277.089 s/day**. |
| **DEPENDENT** | **E is a function of ρ** (given the capacity λ): one ρ, one E. The independence is **one-directional** — many ρ per E, never many E per ρ. |
| **REFUTED** | **same ρ, different energy** as a *state* change. The same uniform ρ gives `E = 2K` exactly for `K = 1…6` (`Σλ = 192K`); the only handle is the capacity K, which is **BOUNDARY** (G_007). |

## 8. Classification and caveats

**No reclassification.** G_016's pairing, G_002's fixed-energy freedom, G_009's clock law, G_011's
phase-inertness and G_014's κ = 1 are unchanged inputs. D_040 untouched; no canonical claim, value or equation
changes; no new primitive. Deterministic: exact algebra, no randomness.

* `ΔE = 0` is **exact in the algebra** for every within-multiplet move (λ constant there). The tests assert it
  at 1e-12/1e-13; the pairwise and λ-mixing examples show |ΔE| ≤ 1.776e-15, i.e. rounding.
* The λ-mixing example is scale-free: `v` has `Σv = ⟨λ,v⟩ = 0` exactly for any scale, and 0.004 is chosen only
  to keep ρ ≥ 0.
* The family law `contrast = 5f/(1−f)` holds for `f ≥ 1/2` (below that the extreme cells move to the m = 2 and
  m = 1 multiplets); the `L1(f)` law holds across the whole range and is exact.
* The unboundedness statement is about **energy conservation only**. G_005/G_008 bound the *realised* effect, so
  the unbounded family is an ontology result, not a laboratory proposal.
* **Independence is not symmetry.** `ρ ↦ E` loses 94 of 95 dimensions (G_016), so ρ is *independent of* E while
  E is *fully determined by* ρ. That one-way character is the whole content of the verdict split.

## 9. Open problems (OP1–OP5)

1. What is the **diameter** of the fixed-E fibre — the maximum `L1` between two `ΔE = 0` states (the family
   gives 1.0625; the simplex bound is 2)?
2. Can the 43-dimensional λ-mixing room be **measured** as a distinct signature from the 51-dimensional
   degeneracy room?
3. Does the family `contrast = 5f/(1−f)` have an analogue for other lattices (random, D96³), i.e. is the 5 a
   D96 artefact of m = 6?
4. Is there any AT-native quantity that is *not* a function of ρ — i.e. a second argument of the state — now
   that the phase sector is confirmed inert?
5. Does the unbounded fixed-E family change the G_010 time-control accounting (the drive cost was priced per
   unit Δlnρ, not per unit `L1`)?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_016b_Tests.cs` — **7/7 PASSED** (~0.07 s)
**Group total:** G_001–G_016b = **147/147 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_016b"`

| label | content |
|-------|---------|
| **INDEPENDENT** | same energy, different ρ — 94-dim kernel (51 degeneracy + 43 λ-mixing); witness `Δρ = 0.6666666666666667`, `Δτ = 86 277.089 s/day` at `ΔE = 0` |
| **DEPENDENT** | E is a function of ρ — one ρ, one E; the independence is one-directional |
| **REFUTED** | same ρ, different energy — only the BOUNDARY capacity K does it (`E = 2K` exactly) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_016.md`, `ResearchY-G_002.md`, `ResearchY-G_005.md`,
  `ResearchY-G_007.md`, `ResearchY-G_009.md`, `ResearchY-G_011.md`, `ResearchY-G_014.md`
* `Docs/ResearchY/Tests/Results/Y_G_016b_Result.md`
* `AT.Tests/Shared/DensityField.cs`; `AT.Book/Services/Calculations/SpectrumService.cs`
