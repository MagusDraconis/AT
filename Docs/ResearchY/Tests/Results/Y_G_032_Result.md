# Y_G_032 Result — Conformal Assumption Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_032_Tests.cs`
**Status:** 8/8 PASSED (~0.06 s)
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_032"`
**Group total:** G_001–G_032 = **260/260 PASSED**

## Verdict

**BOUNDARY** — `g = Ω²η` is **assumed**; no existing AT primitive entails it. Computed from the empty forcing set (ResearchY-G_027).

## Where it enters (the trace)

| # | step | ansatz? | provenance |
|---|---|---|---|
| 1 | Difference → Counting | no | DERIVED |
| 2 | Counting → ρ (scalar face) | no | DERIVED — QG288: the scalar face does not require η |
| 3 | **ρ → metric (TENSOR face)** | **YES — first** | **ASSUMED** — QG285/QG288: the tensor face requires η |
| 4 | metric → clock law / measure | inherits | DERIVED (within the ansatz) |

The chain is **metric-free for its first two steps**. The imported object is **η**, which QG289's anchor inventory (`AnchorInventoryAudit.cs`, QG77) classifies as a **TRUE THEORY INPUT**: *"part of the geometry, not a choice."*

## The criterion, proved

**`g = Ω²η ⟺ A = B`** (in the G-group's gauge).

- (⇐) A = B ⟹ `g = e^(2A)(−dt² + δ) = e^(2A)η`.
- (⇒) area gauge: `e^(B_area) = 1/(1+rB′)`, `R·A_R = rA′/(1+rB′)` ⟹ holds iff `B′ = A′`.
- 0 mismatches in an executed 401×401 sweep.
- Weyl confirmation: W = 0.0 (flat), 1.0e−30 and 1.6e−29 for A = B; 0.69, 0.75, 15.39 for A ≠ B; **0.2537 for the G_029 survivor**.

**The survivor is NOT conformally flat**: `deficit = −1.5x² + (13/6)x³ − (29/8)x⁴ + …` — conformal to first order, violated at second.

## No primitive forces it

Forcing set (η excluded as self-referential): **EMPTY**. Every other primitive — ρ, ψ, d, π — has **rank 0**; a scalar cannot determine a shape. Component accounting: 10 metric components → 1 conformal function, **discarding 9**; AT supplies **0 of 5** traceless components.

Corroboration: G_029's family `B = F(σ)` **is** that leftover freedom — two functions, not one.

## Counting is a determinant, not a shape

Two 3-metrics with **identical** `√det = ρ` (isotropic diag(ρ^(2/3),ρ^(2/3),ρ^(2/3)) vs anisotropic diag(ρ²,1,1)) but **different** conformal status. So counting cannot entail conformality.

With `A = σ` fixed: **conformality ⟺ counting measure ⟺ B = σ** (0 mismatches over 4001 densities).

## The decisive consequence

`A = B` + `A = σ` ⟹ `B = σ` ⟹ **`γ = −1` exactly at every body** ⟹ Cassini separation **86957.4348 σ**, zero deflection, zero Shapiro.

| body | band centre | \|A−B\|/\|A\| |
|---|---|---|
| Cassini (solar) | +4.000052e−6 | 2.000013 |
| Sun | +2.122536e−6 | 2.000017 |
| J0740+6620 | +0.164591 | 1.666354 |

Band width ~1.84e−10 at the solar point (exact inversion, no sampling). **γ = −1 is never inside the band.** Required violation: a **factor of two**.

**AT's own metric form `g = ρ^(2/d)η` IS the conformal member**, and so predicts γ = −1.

> **If the ansatz were entailed, AT would be refuted.**

## Amendments to prior audits

- **G_029** — the surviving rule `g_rr = 2 − ρ^(2/d)` is **not** conformally flat.
- **G_030** — the dilemma's root is the primitive **η**: restate "counting measure vs optics" as **"conformal flatness vs optics"**.
- **G_031** — the premise that makes the counting measure a theorem is **η: an input, not a derivation**.

## Files

- Core: `AT.Core/ResearchXH/ConformalAssumptionAudit.cs`
- Suite: `AT.Tests/ResearchY/G_GravitySource/Y_G_032_Tests.cs`
- Doc: `Docs/ResearchY/G_GravitySource/ResearchY-G_032.md`
