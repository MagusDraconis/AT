# ResearchY-NP_084 — Eta Framework Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_084 (permanent)
**Title:** Eta Framework Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_084.md`
**Depends on:** ResearchY-NP_058 (information-to-energy bridge), NP_068 (ψ ontology), NP_080
(Difference duality), NP_081 (energy ontology), AT-QG QG77 (metric g = ρ^(2/d)η), QG285 (Weyl
content of ψ), QG286 (Difference Duality), QG290 (framework inventory), QG291 (framework
necessity), QG197/207 (metric ansatz), QG26 (PPN γ)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_084_Tests.cs`

---

## Purpose

NP_080 established that Difference splits into ρ (trace) and ψ (traceless); NP_068 identified ψ as
the Weyl/tensor face. NP_084 asks the question those two presuppose: **what is η — the reference
against which trace, traceless, conformal flatness, and Weyl are all defined?** Program: (1)
inventory every use of η; (2) remove η and see what breaks; (3) test A/B/C/D; (4) determine
whether η is derivable from Difference or remains a framework boundary. **Success criterion:**
identify the ontological status of η and whether it is reducible. No new primitives; canonical AT
unchanged.

---

## 1. Inventory — every use of η

| Use | Role | Source |
|---|---|---|
| the metric ansatz g = ρ^(2/d)·η | the conformal metric built from the reference | QG77/197/207 |
| the trace Tr(A) = η^ij A_ij = δ^ij A_ij | the isotropic projection (ρ) | QG286 |
| the traceless part A_ij − (1/d)Tr(A)η_ij | the anisotropic projection (ψ) | QG286 |
| conformal flatness (Weyl = 0) | g is η up to a conformal factor | QG197/212 |
| the Weyl content ψ | the deviation from conformal flatness | QG285 |
| PPN γ (lensing) | γ = +1 / −1 is read against the reference | QG26/212 |

**η is the reference structure against which every geometric reading is taken.** It is not the
content of the theory; it is the *reader*.

---

## 2. Remove η — what breaks

| Quantity | Depends on η? | Breaks? |
|---|---|---|
| **ρ (the trace)** | Tr(A) = η^ij A_ij — no contraction without η | **YES** |
| **ψ (the traceless part)** | "traceless" is defined relative to η | **YES** |
| **the decomposition** | trace + traceless needs the reference split | **YES** |
| **gravity (the metric g = ρ^(2/d)η)** | g is built FROM η | **YES** |
| **conformal flatness (Weyl = 0)** | "flat against what?" — no reference | **YES** |
| **lensing (PPN γ)** | γ measures deviation from the reference | **YES** |

**Removing η removes the entire geometric reading — ρ, ψ, gravity, lensing, and the decomposition
all collapse.** η is load-bearing in exactly the way a reading device is: it carries no content
itself, but without it there is nothing to read.

---

## 3. A / B / C / D

| Interpretation | Verdict |
|---|---|
| **A) pure coordinate convention** | **NO.** A coordinate convention can be transformed away; η cannot — it defines what "trace", "traceless", and "conformal" *mean*. |
| **B) reference structure** | **YES.** η is the conformal reference metric — the framework's reading structure (QG290). |
| **C) geometry primitive** | **PARTIAL.** η is a metric (geometric), but a *non-dynamical reference*: it carries no scale, no energy, no propagation — it is not a dynamical degree of freedom like ρ or ψ. |
| **D) hidden background metric** | **NO.** η is not a hidden dynamical background (a preferred frame); it is an explicit, non-dynamical reference — there is no Lorentz-violating content smuggled through it. |

**Determination: B (reference structure) = the FRAMEWORK reading device, realized as C in the
weak sense (a metric, but non-dynamical) and explicitly NOT A and NOT D.**

---

## 4. Can η be derived from Difference?

**NO.** This is the decisive point, and it completes NP_080:

- **Difference gives the rank-2 object** (the connectivity/stress tensor A_ij) — the *being* of the
  structure.
- **But the trace/traceless decomposition presupposes η** — the contraction Tr(A) = η^ij A_ij is
  *defined by* η, and "traceless" is "orthogonal to η". Without η there is no notion of isotropy,
  no conformal flatness, no Weyl content.

Difference answers "what is there"; η answers "how to read it". The two are complementary and
**neither reduces to the other**:

```
Difference  (the rank-2 object, the being)     [BOUNDARY primitive]
η           (the reference contraction, the reading)  [FRAMEWORK primitive]
   →  ρ = trace of A against η   →  ψ = traceless part against η
```

The founding pair {Difference, η} (Part I of the book) is therefore **genuinely two**: Difference
is the content, η is the reading, and the trace/traceless duality of NP_080 is the *joint*
consequence of both.

---

## 5. The ontological status — FRAMEWORK

| Status | Verdict |
|---|---|
| **DERIVED** | **NO.** No count produces a metric; η carries no scale and no dynamics to be derived. |
| **FRAMEWORK (boundary)** | **YES.** η is an irreducible structural reference — a framework item, not an empirical input (unlike the dimensionful anchors v, m_e) and not a physics primitive (no scale, no energy). |
| **empirical BOUNDARY** | **NO.** η is not an observed value; it is the reading structure itself. |
| **REFUTED / redundant** | **NO.** Removing η removes the reading — it is NECESSARY, not redundant (QG291). |

**η is a FRAMEWORK boundary — the conformal reference structure, NECESSARY (defines the reading)
and IRREDUCIBLE (not derivable from Difference), but not a physics primitive (no scale, no
dynamics).**

---

## Theorem

> **Theorem (NP_084).** η is the conformal reference metric — a FRAMEWORK boundary, irreducible
> and necessary, but not a physics primitive. It is the reference against which the trace
> (Tr(A) = η^ij A_ij, the scalar face ρ), the traceless part (the tensor face ψ), conformal
> flatness (Weyl = 0), the Weyl content (ψ = deviation from flatness), the metric (g = ρ^(2/d)η),
> and PPN γ (lensing) are all DEFINED. Proof: (1) Inventory (Section 1): every geometric read goes
> through η. (2) Removal (Section 2, verified): removing η breaks ρ, ψ, gravity, lensing, and the
> decomposition — no trace, no traceless, no conformal flatness (QG291). (3) Interpretations
> (Section 3): B (reference structure) YES, realized as C weakly (a metric, but non-dynamical);
> A (coordinate convention) and D (hidden background) REFUTED. (4) Reducibility (Section 4): η
> cannot be derived from Difference — Difference supplies the rank-2 object, but the trace/traceless
> split presupposes η as the contraction reference; the two are complementary halves of the founding
> pair {Difference, η}. (5) Status (Section 5): FRAMEWORK — not DERIVED (no count produces a
> metric), not an empirical BOUNDARY (no observed value), not REFUTED/redundant (necessary).
> **Success criterion: η is a FRAMEWORK boundary — the irreducible, necessary conformal reference
> structure that defines the reading, complementary to and not reducible to Difference.** No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Remove η. (3) Test A–D. (4) Show η is not reducible to
> Difference. (5) Classify. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "η is a coordinate convention" | a convention transforms away; η defines trace/traceless/conformal and cannot be removed |
| "η is a hidden background metric" | η is explicit and non-dynamical — no preferred frame, no Lorentz violation |
| "η is derivable from Difference" | no count produces a metric; the contraction Tr(A) = η^ij A_ij is presupposed, not produced |
| "η is redundant" | removing η removes ρ, ψ, gravity, lensing, and the decomposition (QG291) |
| "η is a dynamical geometry primitive" | η carries no scale, no energy, no propagation — it is a reference, not a degree of freedom |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| η is a framework boundary | a derivation of η (a metric with a trace/traceless contraction) from Difference alone |
| η is necessary | a trace/traceless decomposition (or conformal flatness) defined without any reference metric |
| η is not a physics primitive | a dynamical observable (energy, propagation) carried by η |
| η is not a hidden background | a Lorentz-violating or preferred-frame consequence traced to η |

---

## 8. Classification

| Component | Status |
|---|---|
| η as the conformal reference metric (reading structure) | **FRAMEWORK** (QG290/291) |
| the trace/traceless duality {ρ, ψ} against η | **DERIVED** (QG286 — given η) |
| the metric g = ρ^(2/d)η and PPN γ | **DERIVED** (QG77/197/26 — given η) |
| η as a coordinate convention | **REFUTED** |
| η as a hidden background metric | **REFUTED** |
| η as derivable from Difference | **REFUTED** |

**Conclusion.** η is the **conformal reference metric** — a **FRAMEWORK boundary**, irreducible and
necessary, but not a physics primitive. It is the reference against which the trace (ρ), the
traceless part (ψ), conformal flatness, the Weyl content, the metric, and PPN γ are all defined;
removing it removes the entire geometric reading. It cannot be derived from Difference — Difference
supplies the rank-2 object, but the trace/traceless decomposition presupposes η as the contraction
reference — so the founding pair {Difference, η} is genuinely two: Difference is the content, η is
the reading. η is not a coordinate convention (it cannot be transformed away), not a hidden
background (it is explicit and non-dynamical), and not a physics primitive (it carries no scale and
no energy). **η is the theory's reading structure — the second half of the founding pair, framework
rather than derived, necessary rather than redundant.** No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_084_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_084_Inventory` | η defines trace/traceless/conformal/Weyl/metric | ✅ |
| `Y_NP_084_RemoveEta` | ρ, ψ, gravity, lensing, decomposition all break | ✅ |
| `Y_NP_084_Interpretations` | B YES; A, D NO; C PARTIAL | ✅ |
| `Y_NP_084_NotDerivableFromDifference` | contraction presupposes η | ✅ |
| `Y_NP_084_NoScaleNoDynamics` | η carries no scale, no energy, no propagation | ✅ |
| `Y_NP_084_FrameworkStatus` | FRAMEWORK, necessary, irreducible | ✅ |
| `Y_NP_084_Classification` | FRAMEWORK; convention/background/derivable REFUTED | ✅ |
| `Y_NP_084_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_084"`

---

## References

- ResearchY-NP_058 (info-energy bridge), NP_068 (ψ ontology), NP_080 (Difference duality),
  NP_081 (energy ontology).
- AT-QG: QG77 (metric g = ρ^(2/d)η), QG285 (Weyl content), QG286 (Difference Duality), QG290
  (framework inventory), QG291 (framework necessity), QG197/207 (metric ansatz), QG26 (PPN γ).
