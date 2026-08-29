# ResearchY-D_037 — Reciprocity-Observability Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_037 (permanent)
**Title:** Reciprocity-Observability Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_037.md`
**Depends on:** ResearchY-D_033 (singlet-prohibition), D_034 (reciprocity),
D_035 (multiplet-requirement), D_036 (complex-state-origin)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_037_Tests.cs`

---

## Purpose

**Why does observability require complete reciprocity?** D_036 showed complex states are
DERIVED. This audit asks whether reciprocity (every mode has a reciprocal partner)
follows from the NATURE of observable states — i.e. from observability itself —
rather than being a separate input.

## Accepted (from D_033, D_034, D_035, D_036)

- The singlet is mathematically valid but physically excluded (D_033).
- Reciprocity = the [magnitude, phase] complex structure; the two DOFs are DERIVED
  (D_034/QG218).
- Complete pairing is DERIVED from complex observability (D_035).
- Complex states are DERIVED — magnitude (count face, QG216) + phase (circulation
  face, QG220); the phase is the pairing discriminator (D_036).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **observability** | a state can be COMPLETELY reconstructed from measurements of its DOFs (information completeness) |
| **reciprocity** | every mode possesses a distinct reciprocal partner — the mirror k ↔ N−k, the {cos, sin} / [Re, Im] quadrature pair |
| **isolated oscillator** | a lone mode with no partner (the self-conjugate singlet k=N/2) |
| **reciprocal partner** | the mirror mode N−k; together the pair {cos, sin} spans the 2D eigenspace at λ_k = λ_{N−k} |

---

## 2. Reciprocity is the complete measurement basis

A complex state ψ = |ψ|·e^{iθ} carries TWO independent real DOFs (D_036). Complete
observation therefore requires measuring BOTH quadratures:

```
ψ_k(n) = |ψ|·e^{i·2πkn/N}
       = a·cos(2πkn/N) + b·sin(2πkn/N)        a = Re, b = Im
reconstruction:  z = a + i·b                  (exact — verified)
single channel:  a alone ⇒ θ ambiguous        (phase unobservable)
```

The {cos, sin} pair at frequency ω_k is exactly the reciprocal-pair structure (D_021):
both are eigenfunctions of L at λ_k = λ_{N−k}, they are orthogonal
(Σ cos·sin = 0), have equal norm (Σ cos² = Σ sin² = N/2), and together span the 2D
eigenspace. **Reciprocity IS the minimal complete measurement basis of the complex
state.**

---

## 3. Reciprocal mode vs isolated mode

| | Reciprocal mode (paired) | Isolated mode (singlet) |
|---|---|---|
| eigenspace | 2D {cos, sin} | 1D {cos} (sin(πn) = 0) |
| quadrature channels | 2 (Re + Im) | 1 (Re only) |
| phase information | full (θ measurable relatively) | none (phase pinned to π) |
| state reconstruction | z = a + ib (exact) | θ ambiguous — underdetermined |
| interference | P = 2 + 2cos(θ₁−θ₂) | classical addition P = P₁ + P₂ |
| reversibility | phase advance Δθ = 2πk/N per tick tracks the cycle | phase fixed — no cycle position |
| observability (complex) | **YES — complete** | **NO — phase channel is zero** |

---

## 4. What is lost for singlets

1. **PHASE INFORMATION** — the sin quadrature is identically zero; the mode carries no
   free phase.
2. **INTERFERENCE** — real-only addition P = P₁ + P₂ (no cross term).
3. **STATE RECONSTRUCTION** — with one quadrature channel the state is underdetermined:
   infinitely many (|ψ|, θ) give the same a = Re; θ cannot be recovered.
4. **REVERSIBILITY** — the phase θ = 2πk/N advances by Δθ = 2πk/N per site; the phase
   IS the cycle position (QG220). A phase-less mode has no cycle position to reverse.
   The singlet's phase is pinned to θ = π (k = N/2).
5. **OBSERVABILITY** — the second measurement channel is identically zero, so the full
   complex state is NOT observable.

---

## 5. Is reciprocity required by…?

| Candidate | Verdict |
|---|---|
| A) observability | **YES** — complete state reconstruction needs BOTH quadratures = the reciprocal pair |
| B) interference | PARTIAL — interference manifests phase differences, but the requirement is the two-channel measurement |
| C) complex structure | **YES** — the complex state's two DOFs demand two measurement channels (D_036) |
| D) information completeness | **YES** — one channel leaves the phase (half the information) unobservable |
| E) none | NO |

**Reciprocity (the two-quadrature measurement basis) is required by observability as
information completeness of the complex state (A/C/D).**

---

## 6. Remove reciprocity: what survives, what becomes unobservable?

| Removed | Survives | Becomes unobservable |
|---|---|---|
| reciprocity (allow isolated modes) | the spectral content (families, moments, span); normalization; the real cos modes | **the PHASE of every isolated mode** — the complex state structure itself; interference; reversibility |

Removing reciprocity does not remove the spectrum — it removes the SECOND quadrature
channel, so only the real part of each state remains observable. The complex state
collapses to a real amplitude.

---

## 7. Prove or refute: observable structure requires reciprocal partners

**YES for a COMPLEX (interference) observable structure.** A complex state carries two
real DOFs (D_036); complete observation requires measuring both; the two-quadrature
{cos, sin} basis IS the reciprocal pair. Observable structure (as complete information
of a complex state) requires reciprocal partners.

---

## Theorem

> **Theorem (D_037).** Observability requires complete reciprocity because observing a
> complex state completely requires its reciprocal-pair measurement basis. A complex
> state ψ = |ψ|·e^{iθ} carries two real DOFs (D_036); complete observation (state
> reconstruction) requires measuring BOTH quadratures. The {cos, sin} pair at ω_k —
> both eigenfunctions of L at λ_k = λ_{N−k}, orthogonal, equal norm, spanning the 2D
> eigenspace — is exactly the reciprocal pair (D_021): from the two projections the
> state is reconstructed exactly (z = a + ib); from one alone the phase θ is ambiguous.
> An isolated singlet (1D real, sin(πn) = 0) has only one quadrature channel — its
> phase is unobservable, its state underdetermined, and its cycle position (reversibility)
> lost. Hence reciprocity is the EMERGENT observable requirement (information
> completeness); complete pairing is DERIVED from it; the Z2-paired sector requirement (D_020) is
> BOUNDARY. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) The complex state has two DOFs (D_036). (2) Complete observation =
> measuring both quadratures; the pair {cos, sin} is orthogonal, equal-norm, and spans
> the 2D eigenspace (Section 2, verified). (3) z = a + ib reconstructs exactly; a alone
> leaves θ ambiguous (Section 2, verified). (4) The singlet has one zero channel —
> phase unobservable (Sections 3–4, verified). (5) Reciprocity is the two-channel
> requirement — EMERGENT; complete pairing DERIVED; D_020 BOUNDARY (Sections 5–6). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → complex state ψ = |ψ|·e^{iθ}          [DERIVED — QG218/QG220, D_036]
 → two real DOFs (magnitude, phase)       [DERIVED]
 → observability = complete reconstruction [EMERGENT — information completeness]
 → reciprocity (two-quadrature basis)     [EMERGENT — the measurement requirement]
 → complete pairing (0 unpaired)          [DERIVED]
 → p=3 / N=96                             [DERIVED]
 → Z2-paired sector requirement           [BOUNDARY — D_020]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Does a complex state have two real DOFs? | **YES** (D_036) |
| Is complete observation = both quadratures? | **YES** (z = a + ib exact; a alone ambiguous) |
| Is the {cos, sin} pair the reciprocal structure? | **YES** (orthogonal, equal norm, 2D eigenspace) |
| Does a singlet have a zero quadrature channel? | **YES** (sin(πn) = 0) |
| Is the singlet's phase unobservable? | **YES** (underdetermined reconstruction) |
| Does observability require reciprocity? | **YES** (as information completeness of the complex state) |
| Is reciprocity derived? | NO — it is EMERGENT from complex-state observability |

---

## Counterexamples

1. **N=64 singlet k=32**: one quadrature channel (sin ≡ 0) — the state is real-only,
   θ unobservable, reconstruction underdetermined.
2. **Single-quadrature measurement a = 0.5**: infinitely many (|ψ|, θ) are consistent —
   the phase is NOT observable without the partner channel.
3. **N=96 reciprocal mode k=16**: {cos₁₆, sin₁₆} span the 2D eigenspace at λ=12;
   z = a + ib reconstructs the complex state exactly.
4. **Reversibility**: at k=16 the phase advances Δθ = 2π·16/96 = π/3 per site (verified);
   at the singlet k=48 Δθ = π — the phase is pinned, no cycle position.

---

## Classification

| Component | Status |
|---|---|
| complex state structure | **DERIVED** (D_036/QG218) |
| two-DOF structure | **DERIVED** |
| observability = information completeness | **EMERGENT** (the requirement) |
| reciprocity (two-quadrature basis) | **EMERGENT** (from complex-state observability) |
| complete pairing (0 unpaired) | **DERIVED** (from reciprocity) |
| Z2-paired sector requirement | **BOUNDARY** (D_020) |
| N=96 | **DERIVED** |

**Observability requires complete reciprocity because complete observation of a complex
state requires its two-quadrature (reciprocal-pair) measurement basis. Reciprocity is
EMERGENT from complex-state observability; complete pairing DERIVED; the Z2-paired
sector requirement BOUNDARY.**

---

## Open Problems

1. **Measurement-basis origin (D_037 OP1).** Whether the {cos, sin} pair as the
   canonical measurement basis (beyond its role as the complex structure's two DOFs)
   has a deeper origin, or is simply the Fourier quadrature basis, is open.
2. **Reversibility role (D_037 OP2).** The phase advance Δθ = 2πk/N tracks the cycle
   position; whether reversibility (the ability to invert the actualization trajectory)
   is a further constraint on the observable sector beyond reconstruction is open.

---

## Next Steps

- **ResearchY-D_038 (or synthesis):** the reciprocity-observability audit completes the
  complex-state chain (Difference → Actualization → complex state → reciprocity →
  complete pairing → N=96). A synthesis can map the full observable-sector boundary.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_037_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_037_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_037_ReciprocalMode` | {cos, sin} eigenfunctions at λ_k=λ_{N−k}, orthogonal, equal norm | ✅ |
| `Y_D_037_IsolatedMode` | singlet real-only (sin≡0), 1D eigenspace, no partner | ✅ |
| `Y_D_037_InterferenceLoss` | real-only → classical addition; complex → interference | ✅ |
| `Y_D_037_StateReconstruction` | z = a + ib exact; a alone ambiguous (θ unobservable) | ✅ |
| `Y_D_037_Observability` | complete observation = two channels; singlet channel zero; reversibility | ✅ |
| `Y_D_037_DependencyTrace` | Difference → complex state → reciprocity → complete pairing → N=96 | ✅ |
| `Y_D_037_Run` | Research report | ✅ |

**Conclusion:** Observability requires complete reciprocity because observing a complex
state completely requires its reciprocal-pair measurement basis — the {cos, sin} (Re,
Im) quadrature pair. A singlet's second channel is identically zero, so its phase is
unobservable and its state underdetermined. Reciprocity is **EMERGENT** from
complex-state observability; complete pairing **DERIVED**; the Z2-paired sector requirement **BOUNDARY**
(D_020). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_037"`

---

## References

- ResearchY-D_033 (singlet-prohibition), D_034 (reciprocity = complex structure), D_035
  (multiplet-requirement), D_036 (complex-state-origin).
- AT-QG: QG63 (link phase), QG216 (amplitude = branching count), QG218 (Hilbert origin),
  QG220 (phase origin — the complete amplitude ψ_k = √(μ^k/S)·e^(2πik/N)).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics — Born rule).
