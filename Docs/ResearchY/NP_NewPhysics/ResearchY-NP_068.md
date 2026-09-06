# ResearchY-NP_068 — Psi Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_068 (permanent)
**Title:** Psi Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_068.md`
**Depends on:** AT-QG QG43 (ψ unique for GW polarization), QG44 (ψ spin-2, Fierz-Pauli),
QG186 (frame dragging), QG212 (conformal optics via ψ), QG223 (ψ second primitive), QG19
(GW requires ψ primitive), QG46 (why spin-2), QG286/QG301 (Difference Duality), QG207
(metric ansatz), ResearchY-NP_067 (lensing sector)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_068_Tests.cs`

---

## Purpose

NP_067 established the ρ-only metric is conformally flat (γ=−1) and the ψ tensor sector
restores γ=+1. NP_068 asks the deepest question about that sector: **what is ψ physically?**
Program: (1) inventory every ψ appearance; (2) remove ψ and see what breaks; (3) test the
interpretations (geometry correction / information field / curvature field / physical degree of
freedom); (4) determine whether ψ is auxiliary / hosted / primitive / emergent. **Success
criterion:** identify the physical ontology of the ψ sector. No new primitives; canonical AT
unchanged.

---

## 1. Inventory — every ψ appearance

| Appearance | Role | Source |
|---|---|---|
| metric completion | g₀₀ = −ρ^(2/d)e^(2ψ), breaking conformal flatness | QG207/212 |
| **lensing** | PPN γ = +1 (deflection, convergence, shear, magnification, Shapiro) | QG212 |
| **frame dragging** | h_0i ≠ 0 (Lense–Thirring, gravitomagnetic) | QG186 |
| **gravitational waves** | the spin-2 polarization (2 d.o.f.) | QG43/44 |
| the Weyl curvature | the non-conformal (traceless) part of the metric | QG44/QG286 |

ψ is the **tensor face** of the theory — the complement of the scalar ρ face. ρ carries the
trace (conformal, Weyl=0) part; ψ carries the traceless (Weyl curvature) part.

---

## 2. Remove ψ — what breaks

| Observable | With ψ | Without ψ (ρ only) | Breaks? |
|---|---|---|---|
| rotation, cluster, redshift, Ωm | ✓ | ✓ (g₀₀ nontrivial) | **NO** |
| **lensing** (γ) | +1 | −1 (no bending) | **YES** |
| **frame dragging** (h_0i) | nonzero | 0 | **YES** |
| **gravitational waves** (spin-2) | present | absent | **YES** |

**ψ is essential for the tensor observables (lensing, frame dragging, GW) and dispensable for
the scalar observables (potential, rotation).**

---

## 3. The four interpretations

| Interpretation | Verdict |
|---|---|
| **A) geometry correction** | **PARTIAL.** ψ completes the metric (QG207), but it is not a *small* correction — it is a full sector (spin-2) carrying the curvature. |
| **B) information field** | **NO.** ρ is the information face; ψ is the geometric/tensor face. ψ carries no KL/entropy content. |
| **C) curvature field** | **YES.** ψ is the Weyl (traceless) curvature — exactly what conformal flatness (Weyl=0) lacks. |
| **D) physical degree of freedom** | **YES.** ψ is the massless spin-2 graviton (2 polarizations), a genuine physical d.o.f. |

**Determination: ψ = C (curvature field) = D (physical degree of freedom), realized as A
(metric completion); it is NOT B (information field).** ψ is the spin-2 graviton — the Weyl
curvature that bends light (γ=+1), drags frames (h_0i), and propagates as gravitational waves.

---

## 4. Auxiliary / hosted / primitive / emergent?

| Status | Verdict |
|---|---|
| **auxiliary** | **NO.** ψ carries genuine observables (lensing, GW, frame dragging); it is not a bookkeeping device. |
| **hosted** | **NO.** ψ is not imported from GR; its *necessity* is derived (spin-0 cannot source spin-2, QG19), its *form* is the Fierz-Pauli tensor (QG44). |
| **emergent** | **REFUTED.** QG19: "GW requires tensor/ψ primitive (spin-2); **emergent impossible**". No scalar (spin-0) can source spin-2; "no scalar saturation reaches spin 2" (QG37). |
| **primitive** | **YES.** ψ is the second primitive (QG223), adjudicated as an ontological boundary. Refined by the Difference Duality (QG286/301): ψ is the **tensor face of the one Difference** — the same founding primitive as ρ, in its traceless aspect. |

**ψ is a PRIMITIVE — the tensor face of Difference (the spin-2 graviton) — not auxiliary, not
hosted, not emergent.**

---

## 5. The Difference Duality — the refined ontology

The corpus's latest resolution (QG286/301) is that ρ and ψ are **two faces of one Difference**:

```
Difference
   ├── ρ  (scalar / trace face)   → conformal part:  potential, rotation, Ωm, information
   └── ψ  (tensor / traceless face) → Weyl curvature: lensing, frame dragging, gravitational waves
```

ψ is therefore not a *separate* new primitive bolted onto ρ; it is the **tensor half of the
same founding Difference** — the traceless (curvature) complement of the scalar (trace) face.
This is what "completes" the metric: ρ gives the conformal (trace) part, ψ gives the Weyl
(traceless) part, and together they reproduce full linearized GR.

---

## Theorem

> **Theorem (NP_068).** ψ is the spin-2 (Weyl-curvature) graviton — the tensor face of
> Difference, a PRIMITIVE physical degree of freedom (massless spin-2, 2 polarizations), not an
> auxiliary, hosted, or emergent object. Proof: (1) Inventory (Section 1): ψ appears as the
> metric completion (QG207), lensing γ=+1 (QG212), frame dragging h_0i (QG186), and GW
> polarization (QG43/44). (2) Removal (Section 2, verified): removing ψ breaks lensing (γ→−1),
> frame dragging (h_0i→0), and GW — but not the scalar potential effects (g₀₀). (3)
> Interpretations (Section 3): ψ = curvature field (C) = physical d.o.f. (D) = metric completion
> (A); it is not an information field (B). (4) Status (Section 4): auxiliary NO (real
> observables), hosted NO (necessity derived), emergent REFUTED (spin-0 cannot source spin-2,
> QG19), primitive YES (second primitive, QG223). (5) Duality (Section 5): QG286/301 refine ψ as
> the tensor face of the one Difference. Classification: ψ as the spin-2 graviton PRIMITIVE
> (QG223); the emergent route REFUTED (QG19); the Difference Duality (ρ=trace, ψ=traceless)
> DERIVED (QG286/301); the information-field reading REFUTED. **Success criterion: ψ is the
> spin-2 Weyl-curvature graviton — the tensor (traceless) face of Difference, a primitive
> physical degree of freedom that carries lensing, frame dragging, and gravitational waves.** No
> new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Remove ψ. (3) Test A–D. (4) Determine status. (5) State the
> duality. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ψ is auxiliary" | it carries genuine observables (lensing, GW, frame dragging), not bookkeeping |
| "ψ is hosted from GR" | its necessity is derived (spin-0 cannot source spin-2); only its form is Fierz-Pauli |
| "ψ is emergent from ρ" | QG19: emergent impossible — no scalar saturation reaches spin-2 |
| "ψ is an information field" | ψ is the geometric/tensor face; ρ is the information face |
| "ψ is a separate third primitive" | QG286: ψ is the tensor face of the ONE Difference, not a separate primitive |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ψ is a primitive (spin-2) | a spin-2 graviton emergent from the scalar ρ sector alone |
| ψ carries the Weyl curvature | a ρ-only metric that bends light (γ ≠ −1) without ψ |
| ψ = the tensor face of Difference | an observable requiring ψ that is not the traceless part of Difference |
| ψ is not an information field | an information/entropy content carried by ψ |

---

## 8. Classification

| Component | Status |
|---|---|
| ψ as the spin-2 graviton (second primitive) | **PRIMITIVE** (QG223) |
| the emergent route (spin-0 → spin-2) | **REFUTED** (QG19) |
| the Difference Duality (ρ = trace, ψ = traceless) | **DERIVED** (QG286/301) |
| the information-field reading | **REFUTED** |
| lensing / frame dragging / GW via ψ | **CORRESPONDENCE** (GR strength, QG212/186/43) |

**Conclusion.** ψ is the **spin-2 (Weyl-curvature) graviton** — the tensor (traceless) face of
the founding Difference, and a genuine **primitive** physical degree of freedom (massless
spin-2, 2 polarizations). It is not auxiliary (it carries real observables), not hosted (its
necessity is derived), and not emergent (spin-0 cannot source spin-2). Its physical content is
exactly the non-conformal curvature the scalar ρ sector lacks: lensing (γ=+1), frame dragging
(h_0i), and gravitational waves. **ψ is the graviton — the curvature-carrying tensor half of
Difference.** No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_068_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_068_Inventory` | ψ: lensing, frame dragging, GW | ✅ |
| `Y_NP_068_RemovePsi` | lensing/frame/GW break; potential survives | ✅ |
| `Y_NP_068_Interpretations` | C=D=A (curvature, d.o.f., completion); not B | ✅ |
| `Y_NP_068_Status` | primitive; not auxiliary/hosted/emergent | ✅ |
| `Y_NP_068_SpinMismatch` | spin-0 cannot source spin-2 | ✅ |
| `Y_NP_068_DifferenceDuality` | ρ = trace, ψ = traceless | ✅ |
| `Y_NP_068_Classification` | graviton PRIMITIVE; emergent REFUTED | ✅ |
| `Y_NP_068_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_068"`

---

## References

- AT-QG: QG43 (ψ unique for GW polarization), QG44 (ψ spin-2, Fierz-Pauli), QG186 (frame
  dragging), QG212 (conformal optics via ψ), QG223 (ψ second primitive), QG19 (GW requires ψ
  primitive), QG46 (why spin-2), QG207 (metric ansatz), QG286/QG301 (Difference Duality).
- ResearchY-NP_067 (lensing sector).
