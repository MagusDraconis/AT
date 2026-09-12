# Y_G_016_Result.md — ResearchY-G_016 Watch Ontology Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_016_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.07 s) — group G total 140/140 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_016"`

## Summary

**Question:** is mass-energy required to generate ρ? (trace Difference → Actualization → ρ → metric → clocks;
tests: ρ from primitives, mass-energy from ρ, the converse, the minimal carrier; candidates occupation density,
probability density, degeneracy distribution, survivor compression, actualization density)
**Answer:** **No.** ρ is **more primitive than mass-energy**: ρ is the counting measure over distinguishable
cells, and mass-energy is the **pairing** `E = ⟨λ,ρ⟩` — a **rank-1 functional on a 95-dimensional object**, i.e.
*bookkeeping*. **SOURCE = ρ · CARRIER = the cellwise counting density (κ = 1) · BOOKKEEPING = E = ⟨λ,ρ⟩.**

## Detail — the AT-native chain

```
Difference → distinguishability → the coupling lattice
   ↓                                    ↓
CAPACITY: spectrum λ, multiplicities m     OCCUPANCY: ρ (counting measure, Σρ = 1)
   ↓                                    ↓
        E = ⟨λ, ρ⟩  →  g₀₀ = −ρ^(2/d)  →  clocks dτ/dt = ρ^(1/d)
```

ρ and λ are **siblings**; `E` is their **pairing**. Nothing in ρ's definition mentions energy.

## Detail — TEST 1: ρ from AT primitives only — DERIVED

| quantity | value |
|---|---|
| cells | 96 |
| eigenspaces `A₀` | **45** |
| multiplicity histogram | **{1:1, 2:42, 5:1, 6:1}** |
| free room `Σ(m−1)` | **51** = N − A₀ |
| total spectral weight `Σλ` | **1152** (capacity invariant; `λ₀ = 0`) |
| `Σρ` | 1.0000000000000000 (canonical measure and every configuration) |

## Detail — TEST 2: mass-energy from ρ — DERIVED (a pairing)

`E = ⟨λ,ρ⟩` (QG180/QG181). `E(uniform) = Σλ/N` = 1152/96 = **12.0** exactly. On the 95-dimensional affine
set `{Σρ = 1}` the functional has **rank 1** and a **94-dimensional kernel** = **51** (energy-free by
degeneracy) + **43** (zero net mixing of distinct λ). It sees **1.0526 %** of ρ.

Non-degeneracy rearrangements do move `E`: comonotone **13.540176608029563** (+1.5401766080295634 =
**12.835 %**), anticomonotone **10.015359929915876** (spread 3.5248166781136874), reverse-witness
**12.095189171364584** (+9.519e-2).

## Detail — TEST 3: does ρ require mass-energy? REFUTED

Within-multiplet moves (λ constant there), m = 6 multiplet:

| move | `Σρ − 1` | `\|ΔE\|` | ρ_A/ρ_B |
|---|---|---|---|
| δ = 0.005 | 0.0 | 1.776e-15 | 2.8462 |
| δ = 0.010 | 0.0 | 1.776e-15 | 49.0000 |

The canonical witness tilt is itself a pure within-multiplet move: `L1 = 0.6666666666666667`, **`ΔE = 0`
exactly**, `max|a| = 0.6031746`. The **51-dimensional free room is the proof**.

## Detail — TEST 4: the minimal carrier — DERIVED

| candidate | verdict | dims | loses | why |
|---|---|---|---|---|
| actualization density | **SOURCE** | 95 | — | the primitive |
| probability density `\|ψ\|²` | **CARRIER** | 95 | — | the identity map (QG220, G_014), κ = 1 |
| occupation density | **CARRIER** | 95 | — | the same read by counting, κ = 1 |
| degeneracy distribution | **CORRELATED** | 44 | **51** | capacity side; invariant within the room |
| survivor compression | **CORRELATED** | (ρ) | — | a functional of ρ; not energy-free (+4.248925e-3 / −2.959751e-1) |
| `E = ⟨λ,ρ⟩` | **BOOKKEEPING** | 1 | **94** | rank-1 pairing; sees 1.0526 % |

The minimal carrier **must be cellwise**: 95 → 44 (loses exactly the 51-dim free room) → 1 (loses 94).

## The critical answer

> **YES** — the canonical witness changes the clock ratio by **2.7144176165949063** while `ΔE = 0` exactly.

| quantity | value |
|---|---|
| `ΔE` (fixed Σm) | **0** |
| contrast `ρ_max/ρ_min` | **20.0** |
| clock ratio `(ρ_max/ρ_min)^(1/d)` | **2.7144176165949063** |
| clock separation | **0.9985774245179969** = **86 277.089 s/day** |
| field `max\|a\|` / observed contrast | **3.7459607502174e5** (= G_005's 3.746e5 requirement) |
| realised band | 4.8867e-6 = 0.14073696 s/day (G_005); observed 0.04637376 s/day |

The **carrier exists**; the **dynamics**, not the ontology, forbids realisation.

## Classification and caveats

**No reclassification.** G_001/G_002/G_005/G_009/G_014 unchanged inputs. D_040 untouched; no canonical claim,
value or equation changes; no new primitive. Deterministic, no randomness.

* DERIVED = (1) ρ from primitives, (2) `E = ⟨λ,ρ⟩` as a pairing and its 94-dim kernel, (4) the cellwise
  minimal carrier · BOUNDARY = the numerical capacity `Σλ = 1152` · CORRELATED = degeneracy distribution,
  survivor compression · REFUTED = "ρ requires mass-energy".
* **What this does to G_015:** the import there was not "ρ needs energy" (false) but "the only laboratory
  handle on the metric is energy" (true) — now identified as the **rank-1 projection** of a 95-dimensional
  object, i.e. a handle that sees 1.0526 % of the occupancy.

## Open problems (OP1–OP5)

1. A laboratory quantity that reads the **energy-free room** directly?
2. Can the 51 within-multiplet directions be a **calibration-free** reference for the clock law?
3. Does the 43-dimensional energy-neutral mixing have a distinguishable signature?
4. What is the next spectral functional (⟨λ²,ρ⟩?) and is it measurable?
5. Is the identification premise now: "the lab density occupies the same 51-dim free room as ρ"?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_016.md`
* `AT.Tests/Shared/DensityField.cs`; `AT.Book/Services/Calculations/SpectrumService.cs`
