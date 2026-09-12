# ResearchY-G_031 — Spatial-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_031 (permanent)
**Title:** Spatial-Origin Audit — exactly where does `B = σ` enter the theory?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_031.md`
**Depends on:** G_030 (the no-go), G_029 (the identified survivor), G_028 (the clock closure), G_023 (the "two pins"), G_018 (the provenance asymmetry), G_025 (the measure premise)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_031_Tests.cs` (7/7 PASSED, ~0.11 s)
**Core:** `AT.Core/ResearchXH/SpatialOriginAudit.cs`

## The answer: it does not enter anywhere

`B = σ` is **not a separate assumption anywhere in the theory.** Write the conformal ansatz with an arbitrary exponent `n`:

```
g_μν = ρ^(2n)·η_μν   ⟹   A = B = n·ln ρ = n·d·σ
                          √(−g₀₀)   = ρ^n        (the clock)
                          √det g_ij = ρ^(n·d)   (the spatial measure)
```

Conformal flatness (`A = B`) is **built into the ansatz**, so only `n` is free. Then in `d = 3`:

```
n = 1/d   ⟺   √(−g₀₀) = ρ^(1/d)   ⟺   √det g_ij = ρ
```

**The clock law and the counting measure are THE SAME EQUATION** — two readings of one exponent choice, read off two slots that conformal flatness has already made equal. Verified exhaustively over a 401 × 401 grid of `(n, ρ)`: **0 mismatches** (the flat-space point `ρ = 1`, where every exponent agrees trivially, is excluded and separately tested).

**There is no "spatial measure" step in the chain to be found or removed.**

### Q1 — which step forces `√det(g_ij) = ρ`?

The step that fixes the **exponent `n = 1/d`**. That step is *simultaneously* the clock law. There is no separate spatial-measure step.

### Q2 — DERIVED, ASSUMED, or CORRESPONDENCE?

| step | provenance | basis |
|---|---|---|
| Difference → Density (ρ = scalar face) | **DERIVED** | QG285/QG286/QG292 — trace/traceless decomposition |
| Density → Metric (`g = ρ^(2n)η`) | **ASSUMED** | QG207 "the metric ansatz" — the source of `A = B` |
| the exponent `n = 1/d` | **ASSUMED** | **this IS the clock law**, anchored by G_004's 0.99600 calibration and G_009's GPS agreement |
| Metric → Spatial measure (`√det g_ij = ρ`) | **DERIVED** | algebraic: `ρ^(n·d) = ρ` identically at `n = 1/d`, `d = 3` |
| ρ read as a geometric volume | **CORRESPONDENCE** | G_018's provenance asymmetry — a boundary identification |

So: **DERIVED algebraically, from an ASSUMED exponent, under a CORRESPONDENCE reading.**

### Q3 — does an alternative measure exist without a new primitive?

**Yes — but not within the ansatz.** An alternative requires `B ≠ σ`, which under conformal flatness forces `A ≠ B`: the **traceless face ψ**. ψ is **not a new primitive type** (G_024/QG285 — it is the traceless face of the one Difference), but it is an **independent degree of freedom the theory does not constrain** — so the metric becomes underdetermined, which is exactly G_030's result. The two audits agree.

### Q4 — is the counting measure required anywhere else?

| place | usage | basis |
|---|---|---|
| the metric ansatz itself | **LoadBearing** | `g = ρ^(2/d)η` — not separable from the geometry it produces |
| count conservation `N = ∫ρ dV` | **LoadBearing** | QG194/222 — the volume integral is the count only if `√det g_ij = ρ` |
| deficit accounting `Σρ = 1` | **LoadBearing** | QG181/QG182 — the deficit dust is defined against count-per-volume |
| horizon area → entropy `S = A/4` | **LoadBearing** | QG185/QG259 — the area follows the volume |
| cosmological densities `Ω`, `n_s` | **LoadBearing** | the count→volume map sets the densities |
| quantum amplitude `|ψ|² = ρ` | Neutral | QG216 — a probability reading, not a volume reading |
| RAR scale `g† = cH₀/(2π)` | Neutral | QG080 — uses `c`, `H₀` only; no volume element |

**Five load-bearing uses, two neutral.**

## Critical — what breaks

`B = σ` **is load-bearing**, so removal breaks the following, quantified by the geometric-to-count ratio `√det g_ij / ρ` of the surviving alternative (G_029's γ = +1 member):

| body | `√det g_ij / ρ` | count error |
|---|---|---|
| Sun | 1.000012735 | 1.3e−5 |
| x = 1e−4 | 1.000600120 | 6.0e−4 |
| x = 0.1 | 1.733052388 | 7.3e−1 |
| **J0740+6620** | **3.437584871** | **2.44** |
| x = 1 | 51.142808724 | 50.1 |

**What breaks, exactly:**
1. **`N = ∫ρ dV`** — the volume integral is no longer the count; the conserved count stops being a geometric invariant.
2. **The deficit accounting (QG181/QG182)** — the count-per-volume basis shifts by the ratios above.
3. **The horizon-area/entropy chain (QG185/QG259)** — the area moves with the volume, i.e. **3.44×** at J0740+6620.
4. **The cosmological density parameters** — the count→volume map changes.

Each listed breakage carries magnitude **2.44** (243.8 %) at the densest measured star.

## Output

| label | content |
|---|---|
| **REQUIRED** | `B = σ` is **entailed**, not assumed — so it cannot be removed on its own — and it is consumed in five places. **The verdict is computed**, not typed (ResearchY-G_027). |

## This sharpens G_030

G_023 said AT "pins `k = 0` **twice** — the clock law forces `A = σ`; the counting measure forces `B = σ`." **There are not two pins.** Conformal flatness supplies `A = B`, and **one** exponent choice supplies both values. The counting measure was therefore never an independent postulate.

So G_030's dilemma is **sharper than stated**: not *"counting measure versus optics"*, but

> **conformal flatness plus the clock law versus optics.**

That is a smaller and more decisive statement: it reduces the theory's difficulty to the two things that *are* assumed — the conformal ansatz and its exponent — and shows that the "counting measure", the item the programme had been treating as a removable identification, is a **theorem**, not a premise.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_031_Tests.cs` — **7/7 PASSED**
**Group total:** G_001–G_031 = **252/252 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_031"`

| test | asserts |
|---|---|
| `Y_G_031_TheClockLawAndTheCountingMeasureAreOneEquation` | 0 mismatches over 401×401; any other exponent breaks **both** at once |
| `Y_G_031_BCannotBeRemovedWithoutLeavingConformalFlatness` | no exponent other than 1/d keeps the clock law |
| `Y_G_031_ProvenanceOfEveryStepIsRecorded` | DERIVED / ASSUMED / ASSUMED / DERIVED / CORRESPONDENCE, each citing a basis |
| `Y_G_031_TheCountingMeasureIsLoadBearing` | 5 load-bearing, 2 neutral |
| `Y_G_031_WhatBreaksIsQuantified` | ratios 1.000012735 → 3.437584871 → 51.142808724; four breakages at 2.44 |
| `Y_G_031_VerdictIsRequired` | the verdict is computed; the alternative exists but requires `A ≠ B` |
| `Y_G_031_Run` | the full report |

**Opens:** OP1 state the conformal ansatz's exponent as the theory's *single* metric premise (it currently appears twice — as "clock law" and as "counting measure"); OP2 re-derive the area/entropy and cosmological chains on the alternative measure to see whether any of them can absorb a 3.44× volume change; OP3 decide whether the count↔volume reading should be re-stated as a *reference-measure* correspondence.
