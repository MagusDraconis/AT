# ResearchY-D_036 — Complex-State-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_036 (permanent)
**Title:** Complex-State-Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_036.md`
**Depends on:** ResearchY-D_021 (oscillation symmetry), D_022 (weak-isospin entry),
D_034 (reciprocity), D_035 (multiplet-requirement)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_036_Tests.cs`

---

## Purpose

**Why must observable states be complex?** D_035 identified "the observable sector is
complex" as the boundary (D_035 OP1). This audit pushes one step deeper: is the complex
state structure itself DERIVED from Difference → Actualization, or does it remain the
final boundary input?

## Accepted (from D_021, D_022, D_034, D_035)

- The Z2 pairing {cos, sin} at each k is the two-quadrature structure of ONE oscillation
  (D_021); the mirror map k ↔ N−k is the pairing generator.
- The weak-isospin doublet is the EMERGENT attachment surface; the SU(2) gauge algebra
  is BOUNDARY (D_022).
- Reciprocity = the [magnitude, phase] complex structure; magnitude DERIVED (count,
  QG216), phase DERIVED (link, QG63), complex structure DERIVED (QG218) (D_034).
- Complete pairing is DERIVED from complex observability; the boundary was "the
  observable sector is complex" (D_035).
- QG220: the phase θ_k = 2πk/N is the circulation phase of the actualization cycle;
  the complete amplitude ψ_k = √(μ^k/S)·e^(2πik/N) — magnitude AND phase both from
  Q-events.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **real state** | a state carrying ONE real DOF — the magnitude only, ψ = |ψ| (no phase) |
| **complex state** | a state carrying TWO real DOFs — magnitude and phase, ψ = |ψ|·e^{iθ} |
| **magnitude** | |ψ| = √ρ — the branching counting measure (QG216, Difference's count face) |
| **phase** | θ = 2πk/N — the circulation of the actualization cycle (QG220; the U(1) link connection, QG63, Actualization's link face) |
| **observability** | a state carries the two-DOF [magnitude, phase] structure (QG218) — i.e. is a complex number |

---

## 2. Two state spaces

| | real-only space ψ = |ψ| | complex space ψ = |ψ|·e^{iθ} |
|---|---|---|---|
| DOFs per state | 1 (magnitude) | 2 (magnitude, phase) |
| mirror k ↔ N−k | cos(2π(N−k)n/N) = cos(2πkn/N) — **identical** | e^{iθ_{N−k}} = conj(e^{iθ_k}) — **distinct** (conjugate) |
| Z2 pairing | collapses (no 2D eigenspace) | preserved (the 2D {cos, sin} eigenspace) |
| interference | P = P₁ + P₂ (classical addition) | P = 2 + 2cos(θ₁−θ₂) (phase-dependent) |
| doublet / weak-isospin (D_022) | no | yes |

---

## 3. Test: remove the phase — what survives, what breaks?

**Remove phase → magnitude-only ψ = |ψ|.**

- **SURVIVES:** the magnitude/count structure (ρ_k = μ^k/S, normalized), the spectral
  content (families, moments, span), normalization (Σρ = 1).
- **BREAKS FIRST: the Z2 pairing.** cos(2π(N−k)n/N) = cos(2πkn/N) — the mirror pair
  k and N−k collapse to the SAME real state. A magnitude-only space cannot distinguish
  a mode from its mirror: the 2D eigenspace {cos, sin} degenerates to a 1D space, the
  doublet structure (D_021) is lost, and with it the weak-isospin attachment (D_022).
- Then interference (P = P₁ + P₂, no cross term), then reciprocity and complete
  pairing, then N=96.

The **phase is the pairing discriminator**: it is what distinguishes k from N−k. The
Z2 pairing structure (D_020/D_021) therefore REQUIRES the phase — it cannot exist in a
real-only space.

---

## 4. Test: remove the magnitude — what survives, what breaks?

**Remove magnitude → phase-only ψ = e^{iθ} (uniform |ψ| = 1).**

- **SURVIVES:** interference (P = 2 + 2cos(θ₁−θ₂) is still phase-dependent).
- **BREAKS FIRST: the count/probability structure.** The branching shares
  ρ_k = μ^k/S collapse to uniform 1/K: no probability weights, no Born-rule content,
  no observable sector (the D96 occupancy structure is empty). The magnitude is the
  count face of Difference (QG216) — without it the theory has no content to observe.

The **magnitude is the count face**: it encodes how much actualization has occurred at
each branch depth. Without it the observable sector is uniform and empty.

---

## 5. Is complex structure required by…?

| Candidate | Verdict |
|---|---|
| A) Difference | **PARTIAL** — provides the count face → magnitude (DERIVED, QG216) |
| B) Actualization | **PARTIAL** — provides the circulation face → phase (DERIVED, QG220; link, QG63) |
| C) observability | **YES** — the Z2-paired observable sector requires the phase to distinguish k from N−k |
| D) interference | NO — interference is a DERIVED CONSEQUENCE of complexity, not its cause |
| E) none | NO |

**The complex structure is DERIVED: its two DOFs are the two faces of the SAME
actualization tick k** — the branch count (magnitude face, QG216) and the circulation
position (phase face, QG220). A complete state description must carry both, and the
Z2 pairing (D_020/D_021) forces the phase to be observable.

---

## 6. Minimal principle generating ψ = |ψ|·e^{iθ}

```
each actualization tick k produces TWO faces:
  branch depth k  →  count μ^k   →  magnitude |ψ| = √(μ^k/S)     (QG216)
  cycle position k → circulation 2πk/N → phase θ = 2πk/N          (QG220)
  → the state ψ_k = √(μ^k/S)·e^(2πik/N)   [DERIVED — QG218 + QG220]
```

The complete amplitude is **already the canonical object** (QG220): magnitude from the
count, phase from the circulation of the same cycle. No new primitive is introduced.
The state MUST be complex because (1) a complete description of the actualization state
carries both faces, and (2) the Z2 pairing (the observable-sector input, D_020) requires
the phase to distinguish k from N−k.

---

## 7. The refinement: the "complex" requirement is the pairing requirement

D_035 left the boundary at "the observable sector is complex." This audit shows that
statement is not a SEPARATE input — it is the SAME input as the Z2 pairing (D_020):
a paired sector IS a two-DOF sector, and a two-DOF sector IS complex. The pairing
discriminator is the phase.

```
Z2 pairing (0 unpaired, D_020)   [BOUNDARY — the observable-sector input]
  → phase required to distinguish k from N−k      [DERIVED consequence]
  → two real DOFs per mode ({cos, sin} / [Re, Im]) [DERIVED]
  → complex state ψ = |ψ|·e^{iθ}                   [DERIVED — QG218]
  → interference P = 2 + 2cos(θ₁−θ₂)               [DERIVED consequence]
  → complex observability (mult ≥ 2)               [EMERGENT]
  → complete pairing                               [DERIVED — D_035]
  → p=3 / N=96                                     [DERIVED]
```

The boundary count does not increase: "the observable sector is complex" (D_035) is
REDUCED to the D_020 pairing input. Both the magnitude and the phase are DERIVED
(QG216/QG220); the complex structure is DERIVED (QG218); only the pairing requirement
(D_020, within the primitives {Difference, η}, D_027) is BOUNDARY.

---

## Theorem

> **Theorem (D_036).** Observable states must be complex because the complex structure is
> DERIVED from Difference → Actualization, and the "complex" requirement is the same
> input as the Z2 pairing. The two real DOFs are the two faces of the same actualization
> tick k: magnitude |ψ| = √ρ (the count face, QG216) and phase θ = 2πk/N (the
> circulation face, QG220; link connection, QG63). The phase is REQUIRED to distinguish
> k from N−k — the Z2 pairing generator (D_021): in a magnitude-only (1-DOF real) space
> cos(2π(N−k)n/N) = cos(2πkn/N), so the pair collapses and no doublet/weak-isospin
> sector exists. Interference P = 2 + 2cos(θ₁−θ₂) is a DERIVED consequence, not the
> cause. Hence: magnitude DERIVED (QG216); phase DERIVED (QG220); complex state DERIVED
> (QG218); complex observability EMERGENT (= the Z2 pairing, D_020); interference
> DERIVED; N=96 DERIVED. No new primitive; "the observable sector is complex" (D_035) is
> reduced to the D_020 pairing input.
>
> *Proof sketch.* (1) The two DOFs are the magnitude (count, QG216) and the phase
> (circulation, QG220) of the SAME tick k — both DERIVED (Sections 2, 5). (2) The phase
> is the pairing discriminator: cos is even, sin is odd under k → N−k, so the complex
> mode e^{iθ_k} vs conj(e^{iθ_{N−k}}) distinguishes the pair while cos alone cannot
> (Sections 2–3, verified). (3) Removing the phase collapses the pairing (Section 3);
> removing the magnitude removes the count content (Section 4) — both DOFs are necessary
> and each is derived. (4) Interference follows from complexity (Section 5D). (5) Hence
> the complex structure is DERIVED and the "complex" requirement coincides with the Z2
> pairing input (D_020, BOUNDARY). ∎

---

## Dependency Graph

```
Difference (count face)
 → magnitude |ψ| = √ρ            [DERIVED — QG216]
Actualization (circulation face)
 → phase θ = 2πk/N               [DERIVED — QG220, QG63]
 → complex state ψ = |ψ|·e^{iθ}  [DERIVED — QG218]
 → phase distinguishes k from N−k  [DERIVED — the pairing discriminator, D_021]
 → Z2 pairing (0 unpaired)        [BOUNDARY — D_020, the observable-sector input]
 → complex observability (mult ≥ 2) [EMERGENT]
 → complete pairing               [DERIVED — D_035]
 → p=3 / N=96                     [DERIVED]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the magnitude derived? | **YES** (count face, QG216) |
| Is the phase derived? | **YES** (circulation face, QG220; link, QG63) |
| Is the complex state structure derived? | **YES** (two DERIVED DOFs → complex, QG218) |
| Is the phase required to distinguish k from N−k? | **YES** (cos alone is even under the mirror) |
| Does removing the phase break the pairing first? | **YES** (the Z2 doublet collapses) |
| Does removing the magnitude break the count first? | **YES** (the sector becomes uniform/empty) |
| Is interference the cause of complexity? | NO — it is a DERIVED consequence |
| Is "the observable sector is complex" a separate boundary? | NO — it reduces to the D_020 pairing input |

---

## Counterexamples

1. **Magnitude-only state space** (ψ = |ψ|): cos(2π(N−k)n/N) = cos(2πkn/N) — the mirror
   pair collapses; the Z2 pairing, doublets, and weak-isospin cannot exist. Interference
   is classical addition P = P₁ + P₂.
2. **Phase-only state space** (|ψ| = 1): interference survives (P = 2 + 2cos(θ₁−θ₂))
   but the count/probability content ρ_k = μ^k/S is gone — the observable sector is
   uniform and empty (no Born-rule weights).
3. **N=96, k=16**: e^{iθ₁₆} and e^{iθ₈₀} are complex conjugates (distinct) — the complex
   mode carries the pairing; the real part cos alone cannot distinguish them.
4. **k=N/2 (self-conjugate)**: e^{iθ_{N/2}} = ±1 is real — the lone mode is real-only,
   which is exactly why it needs a degenerate multiplet (D_035).

---

## Classification

| Component | Status |
|---|---|
| magnitude (count face) | **DERIVED** (QG216) |
| phase (circulation face) | **DERIVED** (QG220, QG63) |
| complex state structure | **DERIVED** (QG218) |
| phase as pairing discriminator | **DERIVED** (D_021) |
| interference | **DERIVED** (consequence) |
| complex observability | **EMERGENT** (= the Z2 pairing requirement) |
| Z2 pairing (0 unpaired) | **BOUNDARY** (D_020) |
| N=96 | **DERIVED** |

**The complex state structure is fully DERIVED from Difference → Actualization; "the
observable sector is complex" reduces to the Z2 pairing input (D_020). No new
primitive; canonical AT unchanged.**

---

## Open Problems

1. **Pairing-sector origin (D_036 OP1).** Why the observable sector must be Z2-paired
   (the D_020 input itself) — whether complete pairing can be traced deeper than the
   observable-sector construction remains open. (D_035/D_036 push all intermediate
   claims to DERIVED; only the pairing requirement remains BOUNDARY.)
2. **Interference role (D_036 OP2).** Interference is a DERIVED consequence of
   complexity; its role as the operational definition of "observable" (vs. a mere
   corollary) is open.

---

## Next Steps

- **ResearchY-D_037 (or synthesis):** the complex-state audit completes the origin
  chain (Difference → count/circulation → magnitude/phase → complex state → Z2 pairing →
  complete pairing → N=96). A synthesis can map the full observable-sector boundary and
  the exact role of the D_020 input.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_036_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_036_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_036_MagnitudeOnly` | removing phase → mirror pairs collapse (cos even), classical addition | ✅ |
| `Y_D_036_PhaseOnly` | removing magnitude → uniform empty sector; interference survives | ✅ |
| `Y_D_036_Interference` | P = 2 + 2cos(θ₁−θ₂) phase-dependent; real-only P = P₁+P₂ | ✅ |
| `Y_D_036_Observability` | Born rule Σρ=1 exact; complex states preserve it | ✅ |
| `Y_D_036_ComplexNecessity` | phase distinguishes k from N−k (conjugates); complete amplitude | ✅ |
| `Y_D_036_DependencyTrace` | Difference → magnitude → phase → complex → pairing → N=96 | ✅ |
| `Y_D_036_Run` | Research report | ✅ |

**Conclusion:** Observable states must be complex because the complex structure is
DERIVED from Difference → Actualization — magnitude (count face, QG216) and phase
(circulation face, QG220) are the two faces of the same actualization tick k, and the
phase is the discriminator that distinguishes k from N−k (the Z2 pairing). "The
observable sector is complex" (D_035) reduces to the Z2 pairing input (D_020); no new
primitive. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_036"`

---

## References

- ResearchY-D_021 (oscillation symmetry — the mirror-map identities), D_022 (weak-isospin
  entry), D_034 (reciprocity = the [magnitude, phase] pair), D_035 (multiplet-requirement).
- AT-QG: QG63 (link phase), QG216 (amplitude = branching count), QG218 (Hilbert origin),
  QG220 (phase origin — the complete amplitude ψ_k = √(μ^k/S)·e^(2πik/N)).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics — Born rule).
