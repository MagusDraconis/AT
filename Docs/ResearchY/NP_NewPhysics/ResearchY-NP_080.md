# ResearchY-NP_080 — Difference Duality Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_080 (permanent)
**Title:** Difference Duality Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_080.md`
**Depends on:** AT-QG QG223 (ψ second primitive), QG286 (Difference Duality), QG301 (duality
prediction), QG19 (spin-0 cannot source spin-2), QG44 (Fierz-Pauli spin-2), QG285 (Weyl content
of ψ), QG216 (Born rule |ψ|² = ρ), QG291 (η reference necessity), ResearchY-NP_068 (ψ ontology),
NP_071 (matter ontology), NP_075 (force ontology), NP_079 (scale-freeness origin)
**Test suite:** `AT.Tests/ResearchY/NP_00080_Tests.cs`

---

## Purpose

NP_079 grounded scale-freeness in Difference's binary nature; NP_068 identified ψ as the tensor
face of Difference. NP_080 asks the structural question those two leave open: **why does Difference
split into exactly ONE scalar face (ρ) and ONE tensor face (ψ), not one or many fields?** Program:
(1) remove ψ; (2) remove ρ; (3) determine which observables survive; (4) test scalar-only /
tensor-only / scalar+tensor; (5) find the group-theoretic origin (trace / traceless / conformal /
Weyl); (6) decide A/B/C; (7) count the primitive cost. **Success criterion:** explain why reality
requires exactly one scalar and one tensor face of Difference. No new primitives; canonical AT
unchanged.

---

## 1. The two faces (recap)

| Face | ρ (scalar) | ψ (tensor) |
|---|---|---|
| type | scalar counting measure | spin-2 tensor field |
| difference kind | isotropic | anisotropic |
| what it carries | count / magnitude / isotropy | orientation / polarization / Weyl |
| observables | information, matter deficit, Ωm, ΩΛ, masses | lensing, frame dragging, GW |

ρ = "how much difference"; ψ = "which way the difference points".

---

## 2. The group-theoretic origin — the rank-2 decomposition

Difference actualizes into a **symmetric rank-2 object** — the connectivity/stress tensor
A_ij (a link connects a *pair* of nodes, so A_ij = A_ji). A symmetric rank-2 tensor in d = 3
dimensions decomposes **exhaustively and uniquely**:

```
A_ij = (1/d)·Tr(A)·δ_ij  +  (traceless part)
          └── TRACE ──┘       └── TRACELESS ──┘
           ρ (scalar)           ψ (tensor)
```

| Component count (d = 3) | Value | Irreducible rep |
|---|---|---|
| symmetric rank-2 total | d(d+1)/2 = **6** | — |
| trace | **1** | **spin-0** (scalar) |
| traceless (symmetric) | **5** = 2J+1 (J=2) | **spin-2** (tensor) |
| physical TT polarizations | **2** (+ and ×) | massless spin-2 |
| antisymmetric part | 3 | spin-1 — **absent** (A_ij is symmetric) |

**6 = 1 + 5: the decomposition is exhaustive.** A symmetric rank-2 object has exactly two
irreducible faces — the spin-0 trace and the spin-2 traceless part. There is **no third component**
and **no vector (spin-1) face** (the antisymmetric part is zero because connectivity is symmetric).

---

## 3. The removal tests

| Removal | What survives | What breaks |
|---|---|---|
| **remove ψ** | scalar face: information, matter deficit, Ωm, ΩΛ, masses, couplings | **lensing (γ→−1), frame dragging, GW** — the orientation face is gone (NP_068) |
| **remove ρ** | the traceless content (Weyl curvature) | **count/magnitude: information, Ωm, ΩΛ, masses** — nothing to count |
| **only scalar (trace only)** | potential, rotation, information | conformally flat (Weyl = 0): no lensing, no GW |
| **only tensor (traceless only)** | orientation, Weyl | no trace: no count, no magnitude, no information |
| **scalar + tensor** | **everything** | **nothing — complete** (6 = 1 + 5) |

**Neither face alone suffices; the two together are complete.** The trace carries the magnitude,
the traceless carries the orientation — both are needed to specify a rank-2 difference fully.

---

## 4. Why exactly two — and not one or many

1. **Not one.** A single scalar (spin-0) cannot carry orientation — it has no direction, only a
   magnitude. The tensor observables (lensing, frame dragging, GW) require an anisotropic face
   (QG19: spin-0 cannot source spin-2). A single tensor cannot stand alone either: its magnitude
   |ψ|² = ρ is the count face — the trace is a *distinct* projection that the traceless part
   alone does not fix.
2. **Not many.** The rank-2 decomposition is **exhaustive**: 6 = 1 + 5. There is no spin-1
   (vector) piece because the difference object is *symmetric* (a link connects a pair,
   A_ij = A_ji), and no spin-≥3 because a rank-2 object caps at spin-2. The 5 traceless
   components are a *single* irreducible spin-2 multiplet, not five independent faces.
3. **Exactly two.** Spin-0 ⊕ spin-2 is the complete, unique decomposition of a symmetric rank-2
   tensor: one scalar face (the trace) and one tensor face (the traceless part).

---

## 5. trace / traceless / conformal / Weyl

| Term | Meaning | Face |
|---|---|---|
| **trace** | Tr(A)/d — the isotropic, direction-averaged part | ρ |
| **traceless** | A_ij − (1/d)Tr(A)δ_ij — the anisotropic part | ψ |
| **conformal (flat)** | trace-only, Weyl = 0 | ρ-only metric g = ρ^(2/d)η |
| **Weyl** | the traceless curvature — deviation from conformal flatness | ψ (QG285) |

ψ is exactly the **Weyl content** of Difference: the non-conformal, anisotropic residue that the
conformal (trace) face lacks. The duality ρ (trace/conformal) ⊕ ψ (traceless/Weyl) is the full
SO(d) reading of the difference structure.

---

## 6. A / B / C — and the primitive cost

| Determination | Verdict |
|---|---|
| **A) duality is derived** | **YES.** It is the rank-2 decomposition theorem A = trace + traceless, exhaustive and unique — a pure group-theoretic fact. |
| **B) duality is boundary** | **NO.** Nothing is hand-placed: the {ρ, ψ} split is forced by the symmetry of the rank-2 difference object. |
| **C) duality is accidental** | **NO.** Exactly two faces follows from spin-0 ⊕ spin-2, not from a coincidence. |

**Primitive cost: 1.** The two "primitives" (ρ, ψ) collapse to ONE primitive (Difference) with
two faces (QG286). ψ is not a separate primitive; it is the traceless face of the one Difference.

---

## Theorem

> **Theorem (NP_080).** Difference splits into exactly one scalar face ρ and one tensor face ψ
> because Difference actualizes into a SYMMETRIC rank-2 object (the connectivity/stress tensor
> A_ij = A_ji), and a symmetric rank-2 tensor has an EXHAUSTIVE, UNIQUE decomposition into
> exactly two irreducible representations: the spin-0 TRACE (1 component = ρ, the scalar/count/
> isotropic face) and the spin-2 TRACELESS part (5 components = ψ, the tensor/orientation/Weyl
> face; 2 physical TT polarizations). 6 = 1 + 5 — no third component, no vector (spin-1) face
> (the antisymmetric part is zero for a symmetric object), no spin-≥3 (rank-2 caps at spin-2).
> Removing ψ leaves only the trace (conformally flat: no lensing, no frame dragging, no GW);
> removing ρ leaves only the traceless part (no count, no magnitude, no information); both faces
> together are complete. Determination: A — the duality is DERIVED (the rank-2 decomposition
> theorem), not boundary and not accidental. Primitive cost = 1 (Difference). **Success criterion:
> reality requires exactly one scalar and one tensor face because a symmetric rank-2 difference
> decomposes as exactly spin-0 ⊕ spin-2.** Classification: the {ρ, ψ} duality DERIVED (QG286/301);
> the rank-2 symmetry (A_ij = A_ji) DERIVED (connectivity links pairs); the η reference (against
> which trace/traceless/conformal/Weyl are defined) FRAMEWORK/BOUNDARY (QG291); a third face
> REFUTED (6 = 1+5 exhaustive); ψ as an independent primitive REFUTED (it is the traceless face
> of the one Difference). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Recap the faces. (2) Give the rank-2 decomposition. (3) Run the removal
> tests. (4) Show exactly-two (not one, not many). (5) Map trace/traceless/conformal/Weyl.
> (6) Decide A/B/C and cost. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Difference needs only a scalar" | a scalar (spin-0) cannot carry orientation — lensing/GW need the tensor (QG19) |
| "Difference needs many fields" | 6 = 1 + 5 is exhaustive; no third component in a symmetric rank-2 object |
| "there is a vector (spin-1) face" | the antisymmetric part is zero (A_ij = A_ji); no spin-1 |
| "ψ is an independent primitive" | ψ = the traceless face of the one Difference (QG286); not a separate input |
| "the duality is a boundary" | the {ρ, ψ} split is forced by the rank-2 decomposition, not hand-placed |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| the decomposition is exactly spin-0 ⊕ spin-2 | a symmetric rank-2 tensor with a third (spin-1 or spin-≥3) irreducible piece |
| 6 = 1 + 5 at d = 3 | a dimension d where the symmetric rank-2 count is not d(d+1)/2 |
| the duality is complete | an observable requiring a face that is neither trace nor traceless |
| ψ is not an independent primitive | a ψ observable not expressible as the traceless projection of the one Difference |

---

## 9. Classification

| Component | Status |
|---|---|
| the {ρ, ψ} duality (trace/traceless) | **DERIVED** (QG286/301) |
| the rank-2 symmetry (A_ij = A_ji) | **DERIVED** (connectivity links pairs) |
| d = 3 dimensionality | **DERIVED** (QG2/197) |
| the η reference (defines trace/traceless/conformal/Weyl) | **FRAMEWORK/BOUNDARY** (QG291) |
| a third (vector or higher-spin) face | **REFUTED** (6 = 1 + 5 exhaustive) |
| ψ as an independent primitive | **REFUTED** (the traceless face of the one Difference) |

**Conclusion.** Reality has exactly one scalar face and one tensor face of Difference because
Difference actualizes into a **symmetric rank-2 object** whose decomposition is **exhaustively
spin-0 ⊕ spin-2**: the trace (1 component) is the scalar face ρ (count/magnitude/isotropy →
information, matter deficit, Ωm, ΩΛ), and the traceless part (5 components, 2 TT polarizations)
is the tensor face ψ (orientation/anisotropy/Weyl → lensing, frame dragging, GW). There is no
third component and no vector face; one face alone is incomplete, two are complete. The duality
is **DERIVED** (the rank-2 decomposition theorem), and the primitive cost is **1 — Difference
itself**, with ρ and ψ as its two faces. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_080_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_080_Rank2Components` | d(d+1)/2 = 6 at d = 3 | ✅ |
| `Y_NP_080_TraceTracelessSplit` | 6 = 1 (trace) + 5 (traceless) | ✅ |
| `Y_NP_080_SpinDecomposition` | spin-0 ⊕ spin-2; no spin-1 (symmetric) | ✅ |
| `Y_NP_080_TTPolarizations` | 2 physical + and × | ✅ |
| `Y_NP_080_RemovePsi` | scalar survives; lensing/frame/GW break | ✅ |
| `Y_NP_080_RemoveRho` | count/information/Ωm break | ✅ |
| `Y_NP_080_ExactlyTwoFaces` | not one, not many; 6 = 1 + 5 exhaustive | ✅ |
| `Y_NP_080_ABCDuality` | A (derived); not boundary, not accidental | ✅ |
| `Y_NP_080_PrimitiveCost` | 1 (Difference), not 2 | ✅ |
| `Y_NP_080_Classification` | duality DERIVED; third face REFUTED | ✅ |
| `Y_NP_080_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_080"`

---

## References

- AT-QG: QG223 (ψ second primitive), QG286 (Difference Duality), QG301 (duality prediction),
  QG19 (spin-0 cannot source spin-2), QG44 (Fierz-Pauli), QG285 (Weyl content), QG216
  (|ψ|² = ρ), QG291 (η reference necessity).
- ResearchY-NP_068 (ψ ontology), NP_071 (matter ontology), NP_075 (force ontology), NP_079
  (scale-freeness origin).
