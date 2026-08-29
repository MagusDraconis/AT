# ResearchY-D_034 — Reciprocity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_034 (permanent)
**Title:** Reciprocity Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_034.md`
**Depends on:** ResearchY-D_021 (oscillation symmetry), D_032 (pairing-requirement),
D_033 (singlet-prohibition)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_034_Tests.cs`

---

## Purpose

**Why must every observable oscillator possess a reciprocal partner?** D_033 showed the
singlet (a lone oscillator) is physically excluded by the observable sector. This audit
asks whether reciprocity itself is derived or the final boundary principle.

## Accepted (from D_021, D_032, D_033)

- The pairing STRUCTURE (cos/sin quadrature) is DERIVED (D_021).
- The singlet is mathematically valid but physically excluded (D_033).
- Complete pairing (0 unpaired) is the observable-sector input (D_032).
- QG218: a state carries TWO independent real DOFs — magnitude |ψ| and phase θ — and
  MUST be complex to give interference.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **reciprocity** | every mode possesses a reciprocal partner (the Z2 mirror / the [Re, Im] quadrature pair) |
| **paired oscillator** | a mode with two quadratures (cos, sin) = the complex e^{iθ} |
| **isolated oscillator** | a mode with no partner (the singlet, cos only) |
| **observable mode** | a mode in the observable sector (carries the two-DOF complex structure) |

---

## 2. Reciprocity = the [magnitude, phase] complex structure

The canonical QG218 result: a quantum state carries **TWO independent real DOFs** —

```
magnitude |ψ| = √ρ     (the branching counting measure, QG216)
phase θ                (the U(1) link connection, QG63)
```

The state MUST be complex (magnitude + phase) to give interference. The quadrature pair
(cos, sin) is exactly the [Re, Im] of the complex mode e^{i·2πkn/N}. **Reciprocity is
the requirement that every mode be complex (two DOFs).**

---

## 3. Paired mode vs singlet mode

| Structure | Paired mode (complex) | Singlet (real-only) |
|---|---|---|
| quadratures | cos + sin (Re + Im) | cos only (sin(πn) = 0) |
| phase freedom | full (spatial phase θ) | none (real-only) |
| interference | P = 2 + 2cos(θ₁−θ₂) (varies) | P = P₁ + P₂ (classical addition) |
| representation | 2D+ (doublet) | 1D (no doublet) |
| weak-isospin | doublet reading (D_022) | no attachment |
| observability (complex) | YES | NO (real-only) |

---

## 4. What is fundamentally lost when reciprocity is removed?

1. **INTERFERENCE** — a real-only mode gives classical addition (no interference) for
   that frequency (QG218). This is the first, deepest loss.
2. **PHASE FREEDOM** — the mode has no spatial phase partner.
3. **DOUBLET STRUCTURE** — no 2D representation.
4. **WEAK-ISOSPIN ATTACHMENT** — no doublet for the SU(2) reading (D_022).

What survives: the mode as a real oscillator, the count, normalization.

---

## 5. Does reciprocity follow from the primitives?

| Candidate | Verdict |
|---|---|
| A) Difference | **PARTIAL** — the magnitude |ψ| = √ρ is the count face (DERIVED, QG216) |
| B) Actualization | **PARTIAL** — the phase θ is the link connection (DERIVED, QG63) |
| C) Count conservation | NO — gives the magnitude (the normalized share), not the phase partner |
| D) Closure | NO — gives the stable fixed point, not the pairing |
| E) none | NO |

**The [magnitude, phase] pair is DERIVED (magnitude from count, phase from link); the
complex structure is DERIVED (QG218). Reciprocity (every mode complex) is the
observable requirement.**

---

## 6. Remove reciprocity: what survives, what breaks first?

| Removed | Survives | Breaks first |
|---|---|---|
| reciprocity (allow a singlet) | the spectral content (families, moments, span); normalization | **INTERFERENCE** — the singlet frequency becomes real-only (classical addition), then doublet structure and weak-isospin |

---

## 7. Prove or refute: observable structure requires reciprocal partners

**YES for a COMPLEX (interference) observable structure.** Every observable state must
carry the [magnitude, phase] pair (QG218); a real-only singlet cannot give interference
for its frequency. Observable structure (as an interference/complex structure) requires
reciprocity.

---

## Theorem

> **Theorem (D_034).** Reciprocity is the [magnitude, phase] complex structure (QG218):
> every observable mode must carry two independent real DOFs — magnitude |ψ| = √ρ (the
> branching count, QG216, Difference's count face) and phase θ (the U(1) link
> connection, QG63, Actualization's link face). The complex structure (two DOFs) is
> DERIVED: real-only states give classical addition, complex states give interference.
> Reciprocity (every mode complex) is the EMERGENT observable requirement; complete
> pairing (0 unpaired) is BOUNDARY (D_020). Removing reciprocity breaks INTERFERENCE
> first (the singlet contribution becomes real-only), then the doublet structure and
> weak-isospin. Observable structure (as a complex/interference structure) requires
> reciprocal partners.
>
> *Proof sketch.* (1) The two DOFs are the magnitude (count, QG216) and the phase (link,
> QG63) — both DERIVED (Sections 5). (2) The complex structure (two DOFs) is DERIVED
> (QG218): real-only → classical addition, complex → interference (Section 4, verified).
> (3) The singlet is real-only (no sin partner) — no interference for its frequency
> (Sections 3–4). (4) Reciprocity (every mode complex) is the requirement that every
> observable be complex — EMERGENT from the observable sector (Section 5). (5) Complete
> pairing is BOUNDARY (D_032). Hence the DOFs and complex structure are DERIVED;
> reciprocity is EMERGENT; complete pairing is BOUNDARY. ∎

---

## Dependency Graph

```
Difference (count face)
 → magnitude |ψ| = √ρ          [DERIVED — QG216]
Actualization (link face)
 → phase θ                     [DERIVED — QG63]
 → complex structure (2 DOFs)   [DERIVED — QG218]
 → reciprocity (every mode complex)   [EMERGENT — observable requirement]
 → complete pairing (0 unpaired)      [BOUNDARY — D_020]
 → p=3 / N=96                        [DERIVED]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the magnitude derived? | **YES** (branching count, QG216) |
| Is the phase derived? | **YES** (link connection, QG63) |
| Is the complex structure derived? | **YES** (QG218) |
| Is reciprocity (every mode complex) derived? | NO — it is the EMERGENT observable requirement |
| Is complete pairing derived? | NO — BOUNDARY (D_020) |
| Does removing reciprocity break interference? | **YES** (the first break) |
| Does observable structure require reciprocity? | **YES** (as a complex/interference structure) |

---

## Counterexamples

1. **A real-only state** (no phase): gives classical addition P = P₁ + P₂ — no
   interference (QG218). Demonstrates the phase partner is needed for interference.
2. **N=64 singlet k=32**: a real-only mode — its contribution to interference is
   classical for that frequency.
3. **N=96 (complete)**: every mode is complex (paired) — full interference structure.
4. **Normalization survives**: the Fourier basis is complete with or without
   reciprocity — reciprocity is about the complex structure, not normalization.

---

## Classification

| Component | Status |
|---|---|
| magnitude (count/branching) | **DERIVED** (QG216) |
| phase (link connection) | **DERIVED** (QG63) |
| complex structure (2 DOFs) | **DERIVED** (QG218) |
| reciprocity (every mode complex) | **EMERGENT** (observable requirement) |
| complete pairing (0 unpaired) | **BOUNDARY** (D_020) |
| N=96 | **DERIVED** |

**The DOFs and complex structure are DERIVED; reciprocity is EMERGENT; complete pairing
is BOUNDARY.**

---

## Open Problems

1. **Interference necessity (D_034 OP1).** Why the observable sector must be a complex
   (interference) structure — whether interference itself is derivable from Difference
   or is the deeper observable requirement is the QG218 boundary.
2. **Link-phase origin (D_034 OP2).** The phase θ (link connection) is derived (QG63);
   its full role in the reciprocity structure (beyond the doublet) is open.

---

## Next Steps

- **ResearchY-D_035 (or synthesis):** the reciprocity audit completes the pairing chain
  (Difference → count/phase → complex structure → reciprocity → complete pairing →
  N=96). A synthesis can map the full observable-sector structure.
- **D_033 follow-up:** the "reciprocity = complex structure" verdict sharpens D_033 —
  the singlet prohibition is the observable requirement that every mode be complex.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_034_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_034_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_034_ReciprocityOrigin` | magnitude (count) + phase (link) = the [magnitude, phase] pair | ✅ |
| `Y_D_034_SingletFailure` | singlet is real-only (no sin partner) — no interference | ✅ |
| `Y_D_034_PhaseFreedom` | paired mode has full phase; singlet real-only | ✅ |
| `Y_D_034_Observability` | real-only → classical addition (no interference, QG218) | ✅ |
| `Y_D_034_DependencyTrace` | Difference → count/phase → complex → reciprocity → N=96 | ✅ |
| `Y_D_034_Run` | Research report | ✅ |

**Conclusion:** Reciprocity is the **[magnitude, phase] complex structure** (QG218):
every observable mode must carry two independent real DOFs — magnitude |ψ| = √ρ (the
branching count, QG216, Difference's count face) and phase θ (the U(1) link connection,
QG63, Actualization's link face). The complex structure (two DOFs) is DERIVED: real-only
states give classical addition, complex states give interference. Reciprocity (every
mode complex) is the **EMERGENT** observable requirement; complete pairing (0 unpaired)
is **BOUNDARY** (D_020). Removing reciprocity breaks interference first, then the
doublet structure and weak-isospin. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_034"`

---

## References

- ResearchY-D_021 (oscillation symmetry), D_032 (pairing-requirement), D_033
  (singlet-prohibition).
- AT-QG: QG63 (link phase), QG216 (amplitude = branching count), QG218 (Hilbert origin:
  complex states from the [magnitude, phase] pair).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics — Born rule).
