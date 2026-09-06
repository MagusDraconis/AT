# ResearchY-NP_056 — Equation-of-State Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_056 (permanent)
**Title:** Equation-of-State Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_056.md`
**Depends on:** ResearchY-NP_055 (dark energy ontology: ΩΛ is the information-bookkeeping
fraction, w unresolved), AT-QG QG234 (ΩΛ = I_occ/ln K = 0.6839), QG230 (Λ ∝ 1/R²),
QG184 (M ∝ R), ResearchY-QG_018 (info-cosmology closure), QG_012/QG_017 (q₀/z_acc
hosted closures), legacy X046/XD001 (Λ(t) = α/√V(t), w(z) ≠ −1)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_056_Tests.cs`

---

## Purpose

NP_055 established that ΩΛ = I_occ/ln K = 0.6839 is DERIVED, and that its ontology is
the information-bookkeeping fraction — but that the equation of state w is UNRESOLVED.
NP_056 asks the sharper question: **can the AT informational ontology generate a unique
equation of state?** Program: (1) fix ΩΛ and attempt to derive the expansion dynamics;
(2) compare w = −1, w = −1/3, and an evolving w(z); (3) determine whether any follows
uniquely from ΩΛ = I_occ/ln K. **Goal:** establish whether AT predicts a unique
dark-energy dynamics or only a present-day density fraction. No new primitives;
canonical AT unchanged.

---

## 1. ΩΛ = I_occ/ln K is a snapshot number, not a dynamical law

ΩΛ = I_occ/ln K = 0.7513/1.0986 = 0.6839 is a **single dimensionless number** — the
realized information density as a fraction of the state-space capacity. It has:

- **no time argument** (I_occ and ln K are fixed by the D96 octave record);
- **no pressure** (it is a fraction of two information quantities, not an energy density);
- **no equation of state** (a number cannot specify w = p/ρ).

A single present-day fraction cannot determine a functional relation w(z). To derive
expansion dynamics one needs the **full energy-momentum content** — specifically the
scaling of ρ_Λ with the scale factor a — which ΩΛ = I_occ/ln K does not supply.

---

## 2. The expansion-dynamics degeneracy (ΩΛ fixed, dynamics free)

In flat FLRW, the same present-day ΩΛ = 0.6839 is compatible with **infinitely many**
equations of state, each giving a different expansion history:

```
H²(z) = H₀² [ Ωm(1+z)³ + ΩΛ (1+z)^{3(1+w)} ]
```

| w | H²(z=1)/H₀² | q₀ = [Ωm + ΩΛ(1+3w)]/2 |
|---|---|---|
| w = −1 (cosmological constant) | 3.2127 | **−0.5258** (accelerating) |
| w = −1/2 | 4.4632 | −0.2620 (accelerating) |
| w = −1/3 (Λ ∝ 1/R², QG230) | 5.2644 | **+0.1581** (decelerating) |

H²(z=1)/H₀² spans 3.21 → 5.26 (a **64% spread**) for the *same* ΩΛ. The density
fraction does **not** determine the expansion dynamics.

---

## 3. The deceleration parameter depends on w — acceleration is NOT derived

```
q₀ = (1/2) Σ Ωᵢ (1 + 3wᵢ) = [Ωm + ΩΛ(1+3w)]/2
```

| Quantity | Value |
|---|---|
| acceleration threshold (q₀ = 0) | **w = −0.4874** |
| q₀ at w = −1 (hosted) | −0.5258 (accelerating) |
| q₀ at w = −1/3 (QG230 Λ ∝ 1/R²) | +0.1581 (**decelerating**) |
| q₀ at w = 0 (scaling solution) | +0.5000 (decelerating) |

**The observed acceleration (q₀ < 0) requires w < −0.4874** — an *additional* input
beyond ΩΛ. The "q₀ = Ωm/2 − ΩΛ = −0.5258" of QG_012/QG_017 is the *specific w = −1
case*, not a consequence of ΩΛ = I_occ/ln K. AT's own Λ ∝ 1/R² (QG230) would give
w = −1/3 and q₀ = +0.158 — **no acceleration**.

---

## 4. Do any of the candidate equations of state follow uniquely?

### A) w = −1 (cosmological constant)

Requires ρ_Λ = constant (ρ ∝ a⁰). Nothing in ΩΛ = I_occ/ln K — a *fraction* — fixes
ρ_Λ to be constant. **NOT derived.** w = −1 is the ΛCDM *hosted* reading.

### B) w = −1/3 (QG230 Λ ∝ 1/R²)

Requires ρ_Λ ∝ a^(−2). QG230 obtains Λ ∝ 1/R² from the *separate* counting-measure
scaling M ∝ R (QG184) and ρ̄ ~ M/R³ ~ 1/R² — **not** from ΩΛ = I_occ/ln K. Moreover
w = −1/3 does **not** accelerate (q₀ = +0.158). **NOT derived from ΩΛ; and it fails to
reproduce acceleration.**

### C) Evolving w(z) (legacy Λ(t) = α/√V(t))

Requires Λ ∝ V^(−1/2) ∝ a^(−3/2), giving w(z) ≈ −1 + 0.015·(1+z)^(3/2) with the
dimensionless coefficient 0.015 **fitted**, not derived. **NOT derived from ΩΛ.** Also
in tension with the time-independent bookkeeping fraction (NP_055).

**Determination:** none of the three follows uniquely from ΩΛ = I_occ/ln K.

---

## 5. The bookkeeping reading is dynamically inconsistent with acceleration (NEW)

If ΩΛ = 0.6839 is read literally as a **time-independent** bookkeeping fraction
(NP_055's B = E ontology), then in a flat matter + dark-energy universe:

```
ΩΛ = ρ_Λ/ρ_crit = const  and  Ωm = 1 − ΩΛ = const
⇒ ρ_Λ = (ΩΛ/Ωm) ρ_m  ∝  ρ_m  ∝  a^(−3)
⇒ w_de = 0   (dark energy tracks matter — a "scaling solution")
⇒ q₀ = [Ωm + ΩΛ]/2 = 0.5 > 0   (decelerating)
```

A **truly constant** ΩΛ fraction forces dark energy to behave like matter (w = 0) and
**cannot accelerate**. The observed acceleration requires ΩΛ(z) to *evolve* (grow
toward 1), which the bookkeeping fraction — being a fixed partition of ln K — does not
do. **The informational ontology alone does not reproduce the accelerating universe;
acceleration is a hosted ΛCDM (w < −1/3) input.**

---

## Theorem

> **Theorem (NP_056).** ΩΛ = I_occ/ln K = 0.6839 does NOT generate a unique equation of
> state; AT predicts only a present-day density fraction, not a dark-energy dynamics.
> Proof: (1) ΩΛ is a snapshot number (Section 1) — no time argument, no pressure, no
> equation of state. (2) Degeneracy (Section 2, verified): for the SAME ΩΛ = 0.6839,
> H²(z=1)/H₀² = 3.2127 at w = −1, 4.4632 at w = −1/2, 5.2644 at w = −1/3 — a 64%
> spread; the density fraction does not fix the expansion history. (3) Deceleration
> (Section 3, verified): q₀ = [Ωm + ΩΛ(1+3w)]/2, and acceleration requires w < −0.4874;
> w = −1 gives q₀ = −0.5258 (accelerating), w = −1/3 gives q₀ = +0.1581 (decelerating)
> — so acceleration is an extra input, not a consequence of ΩΛ. (4) Candidate w's
> (Section 4): w = −1 needs ρ_Λ = const (not derived from a fraction); w = −1/3 needs
> the separate M ∝ R scaling (QG184, not from I_occ/ln K) and does not accelerate;
> evolving w(z) needs a fitted coefficient 0.015 (legacy). (5) Bookkeeping
> inconsistency (Section 5, verified): a time-independent ΩΛ forces w_de = 0 and
> q₀ = +0.5 — the informational ontology alone cannot accelerate. Classification:
> ΩΛ = 0.6839 (density fraction) DERIVED (unchanged, QG234); the equation of state w
> BOUNDARY (undetermined by the informational ontology); w = −1, w = −1/3, evolving
> w(z) all CORRESPONDENCE (hosted / separately-scaled / fitted); a unique dark-energy
> dynamics REFUTED (no w follows uniquely). **AT predicts only a present-day density
> fraction, not a unique dark-energy dynamics.** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Show ΩΛ is a number. (2) Demonstrate the H(z) degeneracy.
> (3) Compute q₀(w) and the acceleration threshold. (4) Test each candidate w.
> (5) Derive the bookkeeping→w=0 inconsistency. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ΩΛ fixes the expansion dynamics" | the same ΩΛ = 0.6839 gives H²(z=1)/H₀² from 3.21 to 5.26 for different w (64% spread) |
| "ΩΛ implies acceleration" | acceleration requires w < −0.4874; w = −1/3 (QG230) gives q₀ = +0.158, no acceleration |
| "w = −1 follows from ΩΛ" | w = −1 needs ρ_Λ = constant; ΩΛ is a dimensionless fraction, not an energy density |
| "w = −1/3 follows from ΩΛ" | Λ ∝ 1/R² comes from M ∝ R (QG184), a separate scaling, not from I_occ/ln K |
| "evolving w(z) is derived" | the coefficient 0.015 in w(z) ≈ −1 + 0.015(1+z)^(3/2) is fitted (legacy X046) |
| "the bookkeeping fraction accelerates" | a constant ΩΛ forces w_de = 0 and q₀ = +0.5 (decelerating) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ΩΛ does not fix the dynamics | a unique H(z) (or w) derived from ΩΛ = I_occ/ln K alone |
| acceleration is not derived | a derivation of w < −1/3 from {I_occ, ln K, ρ} without hosting ΛCDM |
| w = −1/3 does not accelerate | a Λ ∝ 1/R² model with q₀ < 0 (acceleration) |
| the bookkeeping fraction cannot accelerate | a time-independent ΩΛ model that accelerates in flat FLRW |
| no unique w follows | any single w selected uniquely by the informational structure |

---

## 8. Classification

| Component | Status |
|---|---|
| ΩΛ = I_occ/ln K = 0.6839 (present-day fraction) | **DERIVED** (QG234, unchanged) |
| the equation of state w | **BOUNDARY** (undetermined by the informational ontology) |
| w = −1 (cosmological constant) | **CORRESPONDENCE** (hosted ΛCDM) |
| w = −1/3 (QG230 Λ ∝ 1/R²) | **CORRESPONDENCE** (separate M ∝ R scaling; does not accelerate) |
| evolving w(z) (legacy Λ(t) = α/√V(t)) | **CORRESPONDENCE** (coefficient 0.015 fitted) |
| a unique dark-energy dynamics | **REFUTED** (no w follows uniquely from ΩΛ) |
| cosmic acceleration | **NOT DERIVED** (requires hosted w < −1/3) |
| ΩΛ(z), future evolution, H₀/σ₈/BAO/growth | **BOUNDARY** (hosted / non-information inputs) |

**Conclusion.** ΩΛ = I_occ/ln K = 0.6839 is a DERIVED present-day density fraction, but
it does NOT generate a unique equation of state. The expansion dynamics are degenerate
(the same ΩΛ admits a 64% range of H²(z=1)/H₀² across w = −1 … −1/3), acceleration
requires an extra hosted input (w < −1/3), and the candidate equations of state — w = −1
(hosted), w = −1/3 (separate QG184 scaling, non-accelerating), evolving w(z) (fitted
coefficient) — are all CORRESPONDENCE, none DERIVED. A truly time-independent
bookkeeping fraction is moreover dynamically inconsistent with acceleration (it forces
w = 0). **AT predicts only a present-day density fraction, not a unique dark-energy
dynamics; the equation of state remains BOUNDARY.** No new primitive; canonical AT
unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_056_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_056_OmegaLIsSnapshot` | ΩΛ is a single dimensionless number, no w/z | ✅ |
| `Y_NP_056_ExpansionDynamicsDegeneracy` | same ΩΛ → different H²(z=1) for w=−1,−1/2,−1/3 | ✅ |
| `Y_NP_056_DecelerationDependsOnW` | q₀(w); acceleration needs w < −0.4874 | ✅ |
| `Y_NP_056_WMinusOne` | w=−1 = ρ∝a⁰, not derived from the fraction | ✅ |
| `Y_NP_056_WMinusOneThird` | w=−1/3 = ρ∝a^(−2), separate scaling, non-accelerating | ✅ |
| `Y_NP_056_EvolvingW` | w(z) coefficient 0.015 fitted | ✅ |
| `Y_NP_056_BookkeepingVsAcceleration` | constant ΩΛ ⇒ w=0 ⇒ q₀=+0.5 | ✅ |
| `Y_NP_056_Classification` | w BOUNDARY; candidates CORRESPONDENCE; unique dynamics REFUTED | ✅ |
| `Y_NP_056_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_056"`

---

## References

- ResearchY-NP_055 (dark energy ontology: ΩΛ information-bookkeeping fraction, w
  unresolved), AT-QG QG234 (ΩΛ = I_occ/ln K), QG230 (Λ ∝ 1/R²), QG184 (M ∝ R),
  ResearchY-QG_018 (info-cosmology closure), QG_012/QG_017 (q₀/z_acc hosted closures),
  legacy X046/XD001 (Λ(t) = α/√V(t), w(z) ≠ −1).
