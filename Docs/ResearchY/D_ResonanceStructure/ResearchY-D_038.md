# ResearchY-D_038 — State-Identity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_038 (permanent)
**Title:** State-Identity Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_038.md`
**Depends on:** ResearchY-D_034 (reciprocity), D_035 (multiplet-requirement),
D_036 (complex-state-origin), D_037 (reciprocity-observability)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_038_Tests.cs`

---

## Purpose

**Why should an observable state carry both magnitude and phase?** D_036 derived the
complex state from the two faces of the same tick; D_037 derived reciprocity from
reconstruction. This audit asks the information-theoretic question directly: does
**observability itself** — the requirement that a state be identifiable, distinguishable,
and fully specified — force the two-DOF (complex) structure?

## Accepted (from D_034, D_035, D_036, D_037)

- Magnitude |ψ| = √ρ is DERIVED from the count (QG216); phase θ = 2πk/N is DERIVED
  from the circulation (QG220) — two faces of the same tick k (D_036).
- The complex state is DERIVED; complete pairing is DERIVED from complex observability
  (D_035); the Z2-paired sector requirement is BOUNDARY (D_020).
- Observability = complete state reconstruction; the {cos, sin} pair is the two-channel
  measurement basis (D_037).
- Born rule Σ|ψ|² = 1 is EXACT by construction (QG216/QG220).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **observable state** | a state that is fully specified, identifiable, and distinguishable from all others |
| **magnitude** | |ψ| = √ρ — the count share (branching, QG216) |
| **phase** | θ = 2πk/N — the circulation position (QG220; link, QG63) |
| **information content** | the number of independent real DOFs a state carries (1 = real, 2 = complex) |
| **state identity** | the map from mode index k to its observable state is injective (distinct k → distinct states) |

---

## 2. The three candidate state spaces

| Space | State | # distinct states (N=96) | Identity? | Probability content? |
|---|---|---|---|---|
| magnitude-only | ψ = |ψ| | **3** (from [4,4,87] occupancy) | **NO** (mirror k/N−k collapse) | YES (shares) |
| phase-only | ψ = e^{iθ} | 95 | YES | **NO** (uniform |ψ|=1, no Born weights) |
| **both (complex)** | ψ = |ψ|·e^{iθ} | **95** | **YES** | **YES** (Σρ=1) |

---

## 3. State identity: the information requirement

**State identity** = the map k → state is injective: distinct modes must be
distinguishable.

### Magnitude-only collapses identity

The occupancy structure of the observable sector is [4, 4, 87] (D_020): the 95
positive modes group into branch depths with shares ρ₁ = 1/7, ρ₂ = 2/7, ρ₃ = 4/7
(μ=2). The magnitudes |ψ| = √ρ take only **3 distinct values** across all 95 modes:

```
|ψ| ∈ {√(1/7), √(2/7), √(4/7)} = {0.37796, 0.53452, 0.75593}
```

So a magnitude-only state space has only **3 distinct states** for 95 modes — most
modes are indistinguishable. Worse, the mirror pair k and N−k have IDENTICAL magnitude
(cos is even, D_021): even within a group, the pair collapses.

### Phase-only restores identity but loses probability

The phase θ_k = 2πk/N is injective in k (95 distinct values for k=1..95). A phase-only
space restores full distinguishability. BUT the magnitude is fixed at |ψ| = 1: the
state carries no probability content — no shares, no branching weights, the count
structure ρ = 1/7, 2/7, 4/7 is gone. The observable sector has no "how much"
information.

### Both: the complex state is the minimal complete identity

The complete amplitude ψ_k = √(μ^g/S)·e^(2πik/N) (g = group of k) gives a **95/95
injective** map with **Born rule Σρ = 1 EXACT over the generation shares** (the count
structure of the branching, QG216): state identity AND probability content
simultaneously. This is the **minimal information structure**: two real DOFs
(magnitude, phase) — a complex number.

---

## 4. Test: observability, distinguishability, reconstruction, interference

| Test | magnitude-only | phase-only | complex |
|---|---|---|---|
| observability (fully specified) | NO (3 states) | NO (no probability) | **YES** |
| distinguishability (k → state injective) | NO | YES | YES |
| reconstruction (z = a + ib) | NO (1 channel) | NO (1 channel) | **YES** |
| interference (P = 2+2cos Δθ) | NO (classical) | YES | YES |
| reciprocity (mirror pair distinct) | NO (collapse) | YES (conjugate) | YES |

---

## 5. Remove magnitude — what survives?

Remove the magnitude → phase-only ψ = e^{iθ}.
- **SURVIVES:** distinguishability (95 distinct phases), interference, reciprocity.
- **BREAKS FIRST: probability content** — no Born-rule weights, no shares, no branching
  structure; every state is equally likely. The observable sector has no count.

---

## 6. Remove phase — what survives?

Remove the phase → magnitude-only ψ = |ψ|.
- **SURVIVES:** the count shares (probability weights), normalization (Σρ=1).
- **BREAKS FIRST: state identity** — only 3 distinct states for 95 modes; the mirror
  pair k/N−k collapses; most modes indistinguishable.

---

## 7. Is observability possible with A/B/C?

| Option | Verdict |
|---|---|
| A) magnitude only | **NO** — state identity collapses (3 states), mirror pairs indistinguishable |
| B) phase only | **NO** — probability content lost (uniform, no Born rule) |
| C) both | **YES** — 95/95 identity + Born rule exact + interference |

**Observability requires BOTH: magnitude (probability/count content) AND phase (state
identity/distinguishability).**

---

## 8. Minimal information structure

The minimal information structure for an observable state is **two real DOFs** — the
complex number ψ = |ψ|·e^{iθ}. One DOF is insufficient: magnitude alone loses identity,
phase alone loses probability. The two DOFs are exactly the two faces of the same tick
k (D_036): count → magnitude, circulation → phase. The complex state is therefore the
minimal complete observable state.

---

## Theorem

> **Theorem (D_038).** Observability forces the two-DOF complex structure because state
> identity requires both magnitude and phase. An observable state must be (1) fully
> specified and (2) distinguishable from all others. Magnitude-only fails (2): the
> occupancy structure [4,4,87] gives only 3 distinct magnitudes for 95 modes, and the
> mirror pair k/N−k collapses (cos even). Phase-only fails (1): uniform |ψ|=1 loses all
> probability content (no branching/count structure). The complete amplitude
> ψ_k = √(μ^g/S)·e^(2πik/N) is 95/95 injective with the Born rule Σρ = 1 EXACT over
> the generation shares — state identity AND probability simultaneously. The minimal
> information structure is two real DOFs (magnitude, phase) = a complex number. Hence: magnitude DERIVED (count, QG216); phase DERIVED
> (circulation, QG220); state identity (information completeness) EMERGENT; the complex
> state as minimal complete identity DERIVED; interference DERIVED; the Z2-paired sector
> requirement BOUNDARY (D_020).
>
> *Proof sketch.* (1) State identity requires injectivity of k → state (Section 1). (2)
> Magnitude-only is non-injective: 3 distinct values over 95 modes; mirror collapse
> (Section 3, verified). (3) Phase-only is injective but loses probability (Section 3,
> verified). (4) The complex map is injective with Born rule exact (Section 3, verified).
> (5) Hence both DOFs are necessary (Sections 5–7). ∎

---

## Dependency Graph

```
Difference → count → magnitude |ψ| = √ρ          [DERIVED — QG216]
Actualization → circulation → phase θ = 2πk/N      [DERIVED — QG220]
magnitude + phase → complex state ψ = |ψ|·e^{iθ}   [DERIVED — QG218]
  → state identity (k → state injective)           [EMERGENT — information completeness]
  → probability content (Born rule Σρ=1)           [DERIVED — QG216]
  → observability                                  [EMERGENT — requires both]
  → reciprocity / interference                     [DERIVED — D_037]
  → complete pairing                               [DERIVED — D_035]
  → Z2-paired sector requirement                   [BOUNDARY — D_020]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is magnitude-only state identity possible? | **NO** (3 distinct states for 95 modes; mirror collapse) |
| Is phase-only probability possible? | **NO** (uniform |ψ|=1, no Born rule) |
| Is the complex map injective? | **YES** (95/95) |
| Is the Born rule exact for the complex state? | **YES** (Σρ=1 over the generation shares) |
| Does observability require both DOFs? | **YES** |
| Is the complex state the minimal complete identity? | **YES** (2 real DOFs) |
| Is state identity derived? | NO — it is the EMERGENT information-completeness requirement |

---

## Counterexamples

1. **Magnitude-only (N=96)**: only 3 distinct states — modes k=1 and k=2 (both
   magnitude 0.37796) are indistinguishable; mirror k and N−k collapse.
2. **Phase-only (N=96)**: 95 distinct states but uniform |ψ|=1 — no count/branching
   content, no probability structure.
3. **Complex (N=96)**: 95/95 distinct, Born rule exact — the full observable sector.
4. **N=64 singlet**: phase pinned to π (k=N/2) — the state has no free phase; it needs
   the degenerate multiplet (D_035) to restore a full two-DOF identity.

---

## Classification

| Component | Status |
|---|---|
| magnitude (count face) | **DERIVED** (QG216) |
| phase (circulation face) | **DERIVED** (QG220) |
| complex state (both DOFs) | **DERIVED** (QG218) |
| state identity (information completeness) | **EMERGENT** (the requirement) |
| probability content (Born rule) | **DERIVED** |
| interference / reciprocity | **DERIVED** (D_037) |
| Z2-paired sector requirement | **BOUNDARY** (D_020) |
| N=96 | **DERIVED** |

**Observability forces the two-DOF complex structure: state identity requires the phase,
probability content requires the magnitude — both are DERIVED; the information-
completeness requirement is EMERGENT; the Z2-paired sector requirement BOUNDARY.**

---

## Open Problems

1. **Information-necessity origin (D_038 OP1).** Why observability must be
   information-complete (fully specified + distinguishable) — whether this follows from
   the primitives or is the deepest observable input (the D_020 boundary) is open.

---

## Next Steps

- **ResearchY-D_039 (or synthesis):** the state-identity audit completes the
  information-content chain. A synthesis can map the full observable-sector boundary:
  Difference → count → magnitude; Actualization → circulation → phase;
  magnitude+phase → observability → complete pairing → N=96.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_038_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_038_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_038_MagnitudeOnly` | magnitude-only collapses state identity (3 states); mirror collapse | ✅ |
| `Y_D_038_PhaseOnly` | phase-only loses probability (uniform); identity survives | ✅ |
| `Y_D_038_StateIdentity` | complex map 95/95 injective; Born rule over shares exact | ✅ |
| `Y_D_038_Observability` | requires both DOFs; single DOF fails | ✅ |
| `Y_D_038_InformationContent` | minimal info structure = 2 real DOFs | ✅ |
| `Y_D_038_DependencyTrace` | Difference → magnitude; Actualization → phase; both → observability | ✅ |
| `Y_D_038_Run` | Research report | ✅ |

**Conclusion:** Observability forces the two-DOF complex structure because state
identity requires the phase (magnitude-only gives only 3 distinct states for 95 modes;
mirror pairs collapse) while probability content requires the magnitude (phase-only is
uniform). The complex state ψ = |ψ|·e^{iθ} is the minimal complete observable state:
95/95 injective with Born rule exact. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_038"`

---

## References

- ResearchY-D_034 (reciprocity), D_035 (multiplet-requirement), D_036
  (complex-state-origin), D_037 (reciprocity-observability).
- AT-QG: QG63 (link phase), QG216 (amplitude = branching count), QG218 (Hilbert origin),
  QG220 (phase origin — the complete amplitude).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics — Born rule).
