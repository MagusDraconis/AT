# ResearchY-G_032 — Conformal Assumption Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_032 (permanent)
**Title:** Conformal Assumption Audit — is `g = Ω²η` derived, or only assumed?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_032.md`
**Depends on:** G_031 (the provenance theorem), G_030 (the no-go), G_029 (the survivor), G_028 (the clock closure), QG285/QG288 (trace/traceless duality), QG289/QG77 (the anchor inventory)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_032_Tests.cs` (8/8 PASSED, ~0.06 s)
**Core:** `AT.Core/ResearchXH/ConformalAssumptionAudit.cs`

## The answer: ASSUMED — and it enters at the tensor face

`g = Ω²η` is **not derived.** It is **assumed**, and the theory's own records name the imported object: **η, the conformal reference metric — AT's second primitive.**

**Which step first requires it?** The **ρ → metric** step — the step at which the Difference acquires its **tensor (rank-2) face**.

| # | step | ansatz required? | provenance | basis |
|---|---|---|---|---|
| 1 | Difference → Counting | **no** | DERIVED | counts and indices — pure bookkeeping; no metric content |
| 2 | Counting → ρ (scalar face) | **no** | DERIVED | QG288: *"the scalar face does not [require η]"* |
| 3 | **ρ → metric (TENSOR face)** | **YES — first** | **ASSUMED** | QG285/QG288: *"the tensor face requires η"*; QG289: η is a *TRUE THEORY INPUT* |
| 4 | metric → clock law / spatial measure | inherits | DERIVED | derived **within** the ansatz — G_031: the two are one equation |

The chain is **metric-free for its first two steps.** The conformal ansatz first becomes *necessary* when a rank-2 field must be built out of scalar primitives — and that is exactly where η is imported.

**The repository already says this.** `AT.Core/ResearchXH/ActualizationOriginAudit.cs` (G_018) records E6/QG288: *"ρ and ψ are the trace/traceless faces of the ONE Difference; **the tensor face requires η** [conformal reference, QG285], the scalar face does not."* And `AnchorInventoryAudit.cs` (QG289) classifies:

> **η (conformal reference) — QG77 — STRUCTURAL, `IsTrueInput = true`, `IsCalibration = false`, `IsReplaceable = false`:** *"the conformal reference metric (g = ρ^(2/d)·η) — a TRUE THEORY INPUT: the framework's flat reference, not a measured value; part of the geometry, not a choice."*

This audit's finding is that G_031's phrase *"conformal flatness is built into the ansatz"* has a precise referent: **the ansatz IS the primitive η.** G_031 named the premise; G_032 names its owner.

## The criterion, proved: `g = Ω²η ⟺ A = B`

In the G-group's gauge the spatial part is `e^(2B)·(flat 3-space)`, so the metric is `ds² = −e^(2A)dt² + e^(2B)δ_ij`. Then

**`g = Ω²η  ⟺  A = B.`**

- **(⇐)** `A = B` gives `g = e^(2A)(−dt² + δ) = e^(2A)η` — manifestly conformal.
- **(⇒)** In area gauge the same statement is `e^(B_area) = 1 − R·A_R` with `R = e^B·r`. Two lines of chain rule: `e^(B_area) = 1/(1 + rB′)` and `R·A_R = rA′/(1 + rB′)`, so the criterion holds **iff `B′ = A′`**, i.e. `B = A` by asymptotic flatness. The converse construction is explicit: setting `x = R·e^(−A)` turns the area-gauge metric into `e^(2A)(−dt² + dx² + x²dΩ²)`.

Executed equivalence sweep: **0 mismatches**. **Independent Weyl confirmation** (W = C_abcd·C^abcd):

| profile | W |
|---|---|
| flat `A = B = 0` | **0.0** |
| `A = B = −c/r` | **1.0e−30** (machine zero) |
| `A = B = −c/r²` | **1.6e−29** (machine zero) |
| `A = −c/r, B = A/2` | 0.6937 |
| `A = −c/r, B = 0` | 0.75 |
| `A = 0, B = −c/r` | 15.39 |
| **G_029 survivor** | **0.2537** |

**The G_029 survivor is NOT conformally flat.** Its departure from `e^B = 1 − R·A_R` expands as

**`deficit = −1.5x² + (13/6)x³ − (29/8)x⁴ + …`**

so the survivor is **conformal to first order and violated at second** — the deficit is O(x²), never zero. Verified against the closed form to 2.4e−12 relative.

> **Numerical note.** The direct difference `√(2−e^(−2x)) − (1+x)` loses ~12 digits to cancellation as x → 0: at x = 1e−6 it returns −1.499911e−12 where the true value is −1.4999978e−12. The audit computes it through the conjugate form with a series numerator for small x. The Weyl check was also initially wrong — it reported W = 9.85 for `A = B`, which is *manifestly* `Ω²η`; the cause was an r-only derivative assumption invalidated by the metric's θ-dependence through `sin²θ`.

## Does any primitive force it? NO

| primitive | rank | entails the shape? | is the conformal reference? |
|---|---|---|---|
| ρ (scalar face of the Difference) | **0** | no | no |
| ψ (traceless face of the Difference) | **0** | no | no |
| **η (the conformal reference metric)** | **2** | yes | **YES** |
| d = 3 | 0 | no | no |
| π | 0 | no | no |

Set η aside, because it is **self-referential**: it *is* the conformal statement, so citing it to derive conformal flatness restates the assumption. The forcing set is then **empty**. Every remaining primitive is **rank 0** — and a scalar can only be read into a conformal factor; it cannot determine a *shape*.

**Component accounting.** A symmetric 4×4 metric has **10** components; a conformal metric has **1** function. The ansatz **discards 9**. AT's scalar content at a point is **2** (ρ, ψ) — and it supplies **0 of the 5** independent traceless (Weyl) components. Conformal flatness therefore *removes* freedom the primitives leave open; it does not confer freedom they lack.

**Corroboration from the group's own work:** G_029's candidate family `B = F(σ)` **is** that leftover freedom. Its existence is proof that the primitives' content supports *two* metric functions, not one — which is why conformality has to be imposed separately.

## Counting is a determinant condition, not a shape condition

`√det g_ij = ρ` fixes **one** combination — the product of the eigenvalues. It says nothing about the shape. Explicit counterexample in d = 3:

| ρ | √det (isotropic) | √det (anisotropic) | iso conformal? | aniso conformal? |
|---|---|---|---|---|
| 1e−3 | 1e−3 | 1e−3 | yes | **no** |
| 0.5 | 0.5 | 0.5 | yes | **no** |
| 2.0 | 2.0 | 2.0 | yes | **no** |
| 1000 | 1000 | 1000 | yes | **no** |
| 1.0 | 1.0 | 1.0 | yes | yes *(flat — the degenerate point)* |

Two metrics with **identical** `√det = ρ` but **different** conformal status. The counting measure therefore **cannot entail** conformal flatness. And with `A = σ` fixed, the three names coincide (0 mismatches over 4001 densities):

**conformality `A = B`  ⟺  the counting measure `e^(3B) = ρ`  ⟺  `B = σ`.**

G_031 called the counting measure a theorem; G_032 locates the single premise that makes it one — and finds that premise is an assumption.

## The decisive consequence: imposing the ansatz is refuted

With `A = σ` fixed by G_028, `A = B` forces **`B = σ`** — which gives **`γ = −1` exactly**, at *every* body:

| body | x | γ(conformal) | Cassini separation | band centre | \|A−B\|/\|A\| required |
|---|---|---|---|---|---|
| Cassini (solar) | 4e−6 | **−1.000000000000** | **86957.4348 σ** | +4.000052e−6 | 2.000013 |
| Earth | 6.957e−10 | **−1.000000000000** | **86957.4348 σ** | +6.9571e−10 | 2.000021 |
| Sun | 2.1225e−6 | **−1.000000000000** | **86957.4348 σ** | +2.122536e−6 | 2.000017 |
| J0740+6620 | 0.247002 | **−1.000000000000** | **86957.4348 σ** | +0.164591 | 1.666354 |

Excluded at **8.6957e4 σ**, with **zero** light deflection and **zero** Shapiro delay.

The admissible band is computed by **exact inversion** of γ (no sampling): `B(x,γ) = ½ln(1 + γ(1 − e^(−2x)))`. It is ~**1.84e−10** wide at the solar point and sits at **≈ +x = −σ**, while conformality demands **+σ** — **opposite sides of zero**. The required violation is a **factor of 2.000013**, not a small correction.

**`γ = −1` is never inside the band at any body.** So:

> **If the ansatz were entailed by AT's primitives, AT would be refuted.** Being unentailed is what keeps it viable as a *choice* — and that is precisely what makes it an assumption rather than a derivation.

And the sting: **AT's own metric form, `g = ρ^(2/d)η`, IS the conformal member.** Read at face value it predicts `γ = −1` — no light bending. This is exactly the concern raised earlier in the programme, now traced to its root: **the second primitive η.**

## Output

| label | content |
|---|---|
| **BOUNDARY** | `g = Ω²η` is **assumed**; no existing AT primitive entails it. The verdict is **computed** from the empty forcing set, never typed (ResearchY-G_027). |

## How this sits with G_029–G_031

| audit | finding | G_032's amendment |
|---|---|---|
| G_029 | exactly one space rule survives Cassini: `g_rr = 2 − ρ^(2/d)` | **that survivor is not conformally flat** (W = 0.2537; deficit −1.5x²) |
| G_030 | NO-GO: no local `B = F(σ)` satisfies everything; dilemma "counting measure vs optics" | dilemma's root is the primitive **η** — restate as **"conformal flatness vs optics"** |
| G_031 | the clock law and the counting measure are one equation, entailed by conformal flatness | the premise doing the entailing is **η — an input, not a derivation** |

**Honest note on the repo's wording.** QG289 describes η as *"part of the geometry, not a choice."* This audit agrees with the substance: η is **not** empirical and **not** a fitted constant. But *input* and *derived* are opposites, the anchor inventory itself calls it a "TRUE THEORY **INPUT**", and the entailing set is empty. BOUNDARY records exactly that.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_032_Tests.cs` — **8/8 PASSED**
**Group total:** G_001–G_032 = **260/260 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_032"`

| test | asserts |
|---|---|
| `Y_G_032_TheAnsatzFirstBecomesRequiredAtTheTensorFace` | step 3, the tensor face; 2-step metric-free prefix; ASSUMED |
| `Y_G_032_TheCriterionIsEqualityOfTheTwoExponents` | `g = Ω²η ⟺ A = B`, 0 mismatches; survivor deficit −1.5x²+(13/6)x³ |
| `Y_G_032_TheThreeNamesAreOneCondition` | conformality ⟺ counting measure ⟺ B = σ |
| `Y_G_032_CountingIsADeterminantConditionNotAShapeCondition` | same √det, different conformal status |
| `Y_G_032_NoPrimitiveForcesConformalFlatness` | forcing set empty; ranks 0 except η; 10→1 discarding 9; 0 of 5 traceless |
| `Y_G_032_ImposingTheAnsatzGivesGammaMinusOneAndCassiniExcludesIt` | γ = −1 exactly; 86957.4348 σ; band never contains it; factor-2 violation |
| `Y_G_032_VerdictIsBoundary` | verdict computed; entailing set empty; η self-referential |
| `Y_G_032_Run` | the full report |

**Opens:** OP1 re-derive the optics sector **without** η — i.e. treat the tensor face's reference as a dynamical field — and check whether the clock closure (G_028) survives; OP2 decide whether η should be re-classified from "part of the geometry" to a **fifth primitive** with the trace/traceless duality made explicit; OP3 test whether the survivor's O(x²) non-conformality is measurable anywhere (its relative deficit reaches 5.5e−2 at J0740+6620).
