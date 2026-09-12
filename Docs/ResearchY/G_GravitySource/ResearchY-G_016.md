# ResearchY-G_016 — Watch Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_016 (permanent)
**Title:** Watch Ontology Audit — is mass-energy required to generate ρ?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_016.md`
**Depends on:** ResearchY-G_001 (ρ is the source), G_002 (ρ is controllable at fixed total energy), G_009 (the
clock law dτ/dt = ρ^(1/d) and the scale invariance), G_014 (the κ = 1 measurable carrier and the counting
floor); G_005/G_008 (the suppression that separates possibility from realisation); AT-QG QG194 (the counting
measure, Σρ = 1), QG180/QG181 (the energy/mass law), QG197 (g₀₀ = −ρ^(2/d)), QG220 (ψ = √ρ e^{iθ}), D_048 (the
free room)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_016_Tests.cs` (7/7 PASSED, ~0.07 s)

## Purpose

The chain of audits has established the source (G_001), its freedom at fixed energy (G_002), the clock law
(G_009), the measurable carrier (G_014) and the price of any metric effect (G_015). G_015 exposed a seam: its
metric numbers were priced through **imported Newtonian mass-energy** (`m = E/c²`), so the question of whether
that import was *necessary* was left open. G_016 closes it:

> **Is mass-energy required to generate ρ?**
> Trace: Difference → Actualization → ρ → metric → clocks.
> Tests: (1) derive ρ from AT primitives only, (2) derive mass-energy from ρ, (3) the converse — does ρ require
> mass-energy?, (4) identify the minimal ρ-carrying observable.
> Candidates: occupation density, probability density, degeneracy distribution, survivor compression,
> actualization density.     Output: **SOURCE / CARRIER / BOOKKEEPING**.

**Answer.** **No.** In AT, ρ is **more primitive than mass-energy**: ρ is the counting measure over
distinguishable cells, and mass-energy is the *pairing* `E = ⟨λ,ρ⟩` of the spectral weight with that
occupancy — a **rank-1 functional on a 95-dimensional object**, which is what "bookkeeping" means technically.
`ρ` does not require mass-energy; mass-energy requires `λ` and `ρ`.

## 1. The AT-native chain (no energy appears in it)

```
Difference → distinguishability → the coupling lattice
   ↓                                    ↓
CAPACITY: spectrum λ, multiplicities m     OCCUPANCY: ρ, counting measure (Σρ = 1, QG194)
   ↓                                    ↓
        E = ⟨λ, ρ⟩  (the pairing)  →  g₀₀ = −ρ^(2/d)  →  clocks dτ/dt = ρ^(1/d)
```

ρ and λ are **siblings** off the lattice; `E` is their **pairing**. Nothing in ρ's definition mentions energy.

## 2. TEST 1 — ρ from AT primitives only: **DERIVED**

ρ is the counting measure over distinguishable cells: positivity and `Σρ = 1` need **only**
distinguishability (QG194). Verified on the D96 capacity structure:

| quantity | value |
|---|---|
| cells | 96 |
| eigenspaces `A₀` | **45** |
| multiplicity histogram | **{1:1, 2:42, 5:1, 6:1}** |
| `Σm` | 96 |
| free room `Σ(m−1)` | **51** = N − A₀ |
| total spectral weight `Σλ` | **1152** (a capacity invariant — ρ-blind; `λ₀ = 0`) |
| `Σρ` | 1.0000000000000000 for the canonical measure and every configuration below |

No mass, no energy, no scale enters the definition.

## 3. TEST 2 — mass-energy from ρ: **DERIVED**, and it is a *pairing*

AT's energy is `E = ⟨λ,ρ⟩` (QG180/QG181): the spectral weight **evaluated on** the occupancy — literally a
covector paired with a probability vector.

| quantity | value |
|---|---|
| `E(uniform)` | `Σλ/N` = 1152/96 = **12.0** exactly |
| affine set `{Σρ = 1}` | 95 dimensions |
| rank of `E` on it | **1** |
| kernel | **94** = **51** (energy-free *by degeneracy*) + **43** (zero net mixing of distinct λ) |
| what `E` sees | **1.0526 %** of ρ |

**The pairing is non-injective**, and the non-injectivity has exact structure: the 51 within-multiplet
directions are *energy-free by degeneracy* (λ is constant there), and the remaining 43 mix distinct λ with zero
net. A rearrangement that is **not** within-multiplet *does* move `E` — the same multiset assigned
comonotonically with λ gives **13.540176608029563** (+1.5401766080295634 = **12.835 %**), anticomonotonically
**10.015359929915876** (a spread of 3.5248166781136874), and reversing the witness gives **12.095189171364584**
(+9.519e-2).

## 4. TEST 3 — does ρ require mass-energy? **REFUTED**

**Constructive proof.** Move density between two cells of the **same multiplet**. λ is constant there, so
`Σρ = 1` stays exact and `E` is exactly invariant, while ρ moves. Verified in the m = 6 multiplet:

| move | `Σρ − 1` | `\|ΔE\|` | ρ_A/ρ_B |
|---|---|---|---|
| δ = 0.005 | 0.0 | 1.776e-15 (rounding) | 2.8462 |
| δ = 0.010 | 0.0 | 1.776e-15 (rounding) | 49.0000 |

And the canonical witness tilt is *itself* a pure within-multiplet redistribution: `L1 = 0.6666666666666667`,
`ΔE = 0` exactly, while the field switches on (`max|a| = 0.6031746`).

**The 51-dimensional free room *is* the proof**: 51 independent directions in which ρ changes and the total
mass-energy does not. So ρ exists, moves, and carries a field with **no energy change at all**.

## 5. TEST 4 — the minimal ρ-carrying observable: **DERIVED**

| candidate | verdict | dimensions | loses | why |
|---|---|---|---|---|
| **actualization density** | **SOURCE** | 95 | — | the primitive itself; needs no carrier |
| **probability density** `q = \|ψ\|²` | **CARRIER** | 95 | — | the **identity** map (QG220, G_014): κ = 1 |
| **occupation density** | **CARRIER** | 95 | — | the same observable read by counting, κ = 1 |
| degeneracy distribution | **CORRELATED** | 44 | **51** | the *capacity* side; invariant under any within-room move |
| survivor compression | **CORRELATED** | (ρ) | — | a *functional* of ρ, and not even energy-free |
| `E = ⟨λ,ρ⟩` | **BOOKKEEPING** | 1 | **94** | rank-1 pairing; sees 1.0526 % of ρ |

**The minimal carrier must be cellwise.** Every coarser carrier destroys **exactly** the energy-free room:
- the cellwise density carries 95 dimensions;
- the per-multiplet totals (the degeneracy distribution) carry **44** — losing exactly the **51**;
- `E` carries **1** — losing **94**.

So the degeneracy distribution *sizes* the free room (51 = Σ(m−1)) but **carries no ρ** — it is invariant under
any move inside the room (block-sum L1 = 0 to 1e-12). Survivor compression is a *consequent* of ρ: it follows ρ
and it is not energy-free (48 kept: **+4.248925e-3**; 24 kept: **−2.959751e-1**, with the canonical index
order). The two genuinely κ = 1 carriers are the **same observable** — `|ψ|²` *is* ρ, and its counting noise is
the theory's own Poisson band (G_014).

## 6. The critical answer

> **Does any ρ-carrying observable change the clock rate while Σm remains fixed?**

**Yes — and exactly.** The canonical witness tilt is a pure within-multiplet redistribution, so it is an
*allowed* configuration with `Σρ = 1` and **ΔE = 0** (exactly in the algebra; at rounding level numerically),
yet its cells carry a **20 : 1** density contrast:

| quantity | value |
|---|---|
| `ΔE` (fixed total mass-energy) | **0** |
| ρ contrast `ρ_max/ρ_min` | **20.0** |
| clock ratio between extreme cells, `(ρ_max/ρ_min)^(1/d)` | **2.7144176165949063** |
| clock separation `(1/d)ln(ρ_max/ρ_min)` | **0.9985774245179969** = **86 277.089 s/day** |
| field `max\|a\|` | 0.6031746 |
| `max\|a\|` / observed galactic contrast | **3.7459607502174e5** (= G_005's suppression requirement 3.746e5) |

So the **carrier exists**; what forbids its realisation is the **dynamics, not the ontology**: the realised band
caps at 4.8867e-6 = **0.14073696 s/day** (G_005) and the observed level is 0.04637376 s/day — while the witness
sits **1.86e6×** above the observed level.

## 7. Verdicts

| label | content |
|-------|---------|
| **SOURCE** | the **actualization density ρ** — **DERIVED** from counting alone (positivity, `Σρ = 1`, QG194). The source law `a = −(1/d)∇ln ρ` and the clock law `dτ/dt = ρ^(1/d)` are functions of ρ **and of nothing else**: no coupling constant, no mass, no energy in either. |
| **CARRIER** | the **occupation (probability) density** — the **cellwise identity**, κ = 1 (**DERIVED**). The degeneracy distribution is **CORRELATED** (capacity; it sizes the 51-dim room but carries no ρ) and survivor compression is **CORRELATED** (a functional of ρ). |
| **BOOKKEEPING** | **mass-energy** `E = ⟨λ,ρ⟩` — a **rank-1 pairing** of a covector with the occupancy, discarding **94 of ρ's 95 dimensions** (it sees **1.0526 %**). **DERIVED** as a functional; the *value* `Σλ = 1152` (hence `E = 12`) is **BOUNDARY** — inherited from the D96 lattice and K = 6. |

**Classification.** DERIVED = (1) ρ from primitives, (2) `E = ⟨λ,ρ⟩` as a pairing and its 94-dimensional
kernel, (4) the cellwise minimal carrier · BOUNDARY = the numerical capacity `Σλ = 1152` · CORRELATED = the
degeneracy distribution and survivor compression · REFUTED = "ρ requires mass-energy".

## 8. What this does to G_015

G_015's metric ladder was priced through `m = E/c²`. G_016 shows that this import was **not necessary for
ρ** but **is necessary for a metric claim**: `E` is a genuine functional of ρ, so any laboratory handle on the
metric that goes through mass-energy is a *special* handle — one that sees only 1.0526 % of the occupancy.
G_016 therefore **localises G_015's seam exactly**: the imported step was not "ρ needs energy" (false) but
"the only laboratory handle on the metric is energy" (true, and now identified as the *rank-1 projection* of a
95-dimensional object). That is the honest boundary of the whole group:

> **ρ is the source; the counting density is its carrier; mass-energy is one rank-1 shadow of it.**

## 9. Caveats

**No reclassification.** G_001's source verdict, G_002's fixed-energy freedom, G_009's clock law and its scale
invariance, G_005's band and G_014's κ = 1 identification are unchanged and used as inputs. D_040 untouched;
no canonical claim, value or equation changes; no new primitive. Deterministic: exact algebra, no randomness.

* The BOUNDARY label on `Σλ = 1152` is deliberate: the *existence* of the pairing and the *structure* of its
  kernel (94 = 51 + 43) are derived from the counting constraint alone, but the *magnitude* 1152 is inherited
  from the D96 lattice and the coupling range K = 6.
* `ΔE = 0` for within-multiplet moves is **exact in the algebra**; the test asserts it at 1e-12 (rounding
  level). The pairwise moves show |ΔE| ≤ 1.776e-15.
* The survivor-compression drift depends on the tie order among equal cells; the quoted values use the
  canonical index order, and the test asserts only its magnitude and its monotonicity in compression depth.
* G_016 is an **ontology** audit: it does not reopen G_005/G_008's suppression (which is why the 20 : 1 witness
  is not realised) and it does not claim a laboratory route to ρ without mass-energy — it shows such a route is
  not *forbidden by the definition of ρ*.

## 10. Open problems (OP1–OP5)

1. Is there a laboratory quantity that **reads the energy-free room directly** — i.e. a measurement whose
   expectation is unchanged by any mass-energy change but sensitive to a within-multiplet redistribution?
2. Can the 51 within-multiplet directions be used as a **calibration-free** reference for the clock law, given
   that they cost no energy at all?
3. Does the 43-dimensional energy-neutral mixing of distinct λ have a physical signature distinguishable from
   the 51-dimensional degeneracy room?
4. If `E` is a rank-1 shadow, what is the **next** spectral functional (⟨λ²,ρ⟩ or the full spectrum of ρ) and
   does AT anywhere predict a measurable quantity that depends on it?
5. Is the identification premise of G_014/G_015 now **sharpened** to: "the laboratory's counting density
   occupies the same 51-dimensional free room as the actualization density"?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_016_Tests.cs` — **7/7 PASSED** (~0.07 s)
**Group total:** G_001–G_016 = **140/140 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_016"`

| label | content |
|-------|---------|
| **SOURCE** | the actualization density ρ — DERIVED from counting alone; the source and clock laws depend on ρ and nothing else |
| **CARRIER** | the occupation (probability) density — the cellwise identity, κ = 1 (degeneracy distribution and survivor compression are CORRELATED) |
| **BOOKKEEPING** | mass-energy `E = ⟨λ,ρ⟩` — a rank-1 pairing that keeps 1.0526 % of ρ (94 of 95 dimensions discarded) |

**Critical answer:** **YES** — the canonical witness changes the clock ratio by 2.7144176165949063
(0.9985774245179969 = 86 277.089 s/day separation) while `ΔE = 0` exactly.

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_001.md`, `ResearchY-G_002.md`, `ResearchY-G_005.md`,
  `ResearchY-G_008.md`, `ResearchY-G_009.md`, `ResearchY-G_014.md`, `ResearchY-G_015.md`
* `Docs/ResearchY/Tests/Results/Y_G_016_Result.md`
* `AT.Tests/Shared/DensityField.cs` (`Spread`, `TiltFractions`, `BlockSums`, `Compaction`, `D96Spaces`);
  `AT.Book/Services/Calculations/SpectrumService.cs` (the D96 λ_k law)
