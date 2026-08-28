# ResearchY-D_009 — Minimum Excitation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_009 (permanent)
**Title:** Minimum Excitation Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_009.md`
**Depends on:** ResearchY-D_008 (reference unit)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_009_Tests.cs`

---

## Purpose

Determine whether **ω₁ is the minimum non-zero actualization** — the first possible
difference above the zero mode — and classify the answer.

## Input (from D_008)

- ω₁ = 0.6216 (the fundamental doublet frequency).
- ω₁² = λ₂ = 0.3864 (the spectral gap).

---

## 1. Definitions

| Term | Definition |
|---|---|
| zero mode | the eigenvalue λ₀ = 0, ω₀ = 0, constant eigenvector — the uniform rest state |
| minimum excitation | the smallest non-zero frequency: ω₁ = min{ω_k : k = 1..95} |
| minimum difference | the smallest non-zero spectral separation from the zero mode: ω₁ − ω₀ = ω₁ |
| actualization event | a unit of Difference (a Q-event, Ch1) — the count of the process |

---

## 2. Tests

- **ω₀ = 0:** the zero mode has zero frequency (the reference state, D_008).
- **ω₁ > 0:** the first positive frequency is ω₁ = 0.6216 > 0.

Verified numerically: the minimum positive frequency is ω₁ = 0.6216; there are **zero**
positive states below it.

---

## 3. Proof: no spectral state between ω₀ and ω₁

> **Claim.** No spectral state exists between ω₀ = 0 and ω₁ = 0.6216.

*Proof.* The positive frequencies are ω_k = √λ_k for k = 1..95, with the graph Laplacian
eigenvalues λ_k = 2Σ(1−cos 2πdk/96). The spectrum is discrete and sorted: ω₁ is the
smallest positive frequency (verified: 0 positive states below ω₁). The interval (0, ω₁)
contains no eigenvalue: the spectral gap λ₂ = ω₁² = 0.3864 is the smallest positive
eigenvalue (LOCKING read, Ch7/Ch8), and ω₁ = √λ₂ is its frequency. Hence no spectral
state lies strictly between the zero mode and the first excitation. ∎

The gap is **derived** (a spectral fact): the first positive eigenvalue λ₂ is the
smallest, so the first frequency ω₁ is the minimum excitation.

---

## 4. Comparison: zero mode, first excitation, higher modes

| State | Frequency | Role |
|---|---|---|
| zero mode | ω₀ = 0 | the uniform reference (no oscillation) |
| first excitation | ω₁ = 0.6216 (multiplicity 2, the fundamental doublet) | the minimum non-zero difference |
| higher modes | ω₂..ω₉₅ | the full spectrum (octave bands, Z2 pairs) |

The first excitation is distinguished: it is the smallest non-zero frequency, isolated by
the spectral gap from the zero mode, and carries multiplicity 2 (the fundamental doublet
k = 1 and k = N−1).

---

## 5. Is ω₁ the first frequency, first difference, first actualization, or natural clock only?

| Option | Answer | Classification |
|---|---|---|
| A) first frequency | YES — ω₁ is the smallest positive frequency | **DERIVED** (spectral fact) |
| B) first difference | YES — ω₁ is the minimum non-zero separation from the zero mode | **DERIVED** (spectral fact) |
| C) first actualization | PARTIAL — the minimum excitation as the "first count event" is an interpretation | **EMERGENT** (the structure is derived; the actualization reading is interpretive) |
| D) natural clock only | NO — it is more than a clock (the first difference/excitation); as a *physical* clock it is | **BOUNDARY** (D_008: dimensionless only) |

---

## Overall Verdict

**ω₁ IS the minimum non-zero actualization** in the precise sense: it is the first
(smallest) positive frequency — the minimum excitation and the first difference above the
zero mode. This is **DERIVED** (the spectral gap λ₂ = ω₁² isolates it). Whether it is
"the first actualization" (the first count event) is an **EMERGENT** interpretation, and
as a physical clock it is **BOUNDARY** (dimensionless only, D_008).

---

## Theorem

> **Theorem (D_009).** ω₁ is the minimum non-zero excitation of D96: no spectral state
> exists between the zero mode and the first frequency.
>
> *Proof sketch.* (1) ω₀ = 0 and ω₁ = 0.6216 (Section 2). (2) The positive spectrum is
> discrete; ω₁ = √λ₂ is the smallest positive frequency (λ₂ = ω₁² is the spectral gap,
> the smallest positive eigenvalue, Ch7/Ch8). (3) Hence no eigenvalue lies in (0, ω₁)
> (Section 3, verified: zero states below ω₁). (4) ω₁ is therefore the minimum
> excitation — the first difference above the zero mode. Its role as "first
> actualization" is the interpretive reading (EMERGENT); its physical-clock role is
> BOUNDARY (D_008). ∎

---

## Dependency Graph

```
D_008 (ω₁ = 0.6216; ω₁² = λ₂; dimensionless reference)
  → D_009: ω₁ is the minimum excitation
  ├── zero mode: ω₀ = 0 (reference) — DERIVED
  ├── first excitation: ω₁ = 0.6216 (multiplicity 2) — DERIVED
  ├── spectral gap: no state in (0, ω₁) — DERIVED
  ├── first actualization: interpretive reading — EMERGENT
  └── physical clock: dimensionless only — BOUNDARY
```

---

## Invariant Formulation

The minimum-excitation property is **translation-invariant**: the spectral gap λ₂ = ω₁²
and the minimality of ω₁ are unchanged under all automorphisms of the ring (the spectrum
is invariant, B_003). ω₁ is the smallest element of the invariant positive spectrum — an
invariant fact.

---

## Counterexamples

1. **Intermediate-state counterexample.** A naive continuum picture would place states
   arbitrarily close to zero. The D96 spectrum is discrete: verified zero positive states
   below ω₁. There is no intermediate state.
2. **"ω₁ is not minimal" counterexample.** If a state existed with 0 < ω < ω₁, it would
   be a positive eigenvalue below the smallest positive eigenvalue λ₂ — a contradiction
   of the definition of λ₂ as the spectral gap. No such state exists.
3. **"First actualization" overreach.** Claiming ω₁ is "the first actualization" in the
   count sense goes beyond the spectral fact: the structure (first excitation) is
   derived, but the identification with the first count event is interpretive.

---

## Classification

| Item | Classification |
|---|---|
| ω₀ = 0, ω₁ > 0 | **DERIVED** |
| no spectral state in (0, ω₁) | **DERIVED** (spectral gap) |
| A) first frequency | **DERIVED** |
| B) first difference | **DERIVED** |
| C) first actualization | **EMERGENT** (interpretive) |
| D) natural clock only | NO (more than a clock); physical clock **BOUNDARY** |

---

## Research Conclusions

1. **ω₁ IS the minimum non-zero excitation** — the first (smallest) positive frequency,
   DERIVED.
2. **No spectral state exists between ω₀ and ω₁** — the spectral gap λ₂ = ω₁² isolates
   the first excitation, DERIVED.
3. **The first difference above the zero mode is ω₁** — DERIVED.
4. **"First actualization" is an EMERGENT interpretation** — the structure is derived,
   the count-event identification is interpretive.
5. **As a physical clock, ω₁ is BOUNDARY** (dimensionless only, D_008).

---

## Open Problems

1. **Count-event identification (D_009 OP1).** Is the identification of the minimum
   excitation with the "first actualization event" derivable, or permanently
   interpretive? (Currently: EMERGENT.)
2. **Gap minimality origin (D_009 OP2).** The spectral gap λ₂ = ω₁² is the LOCKING
   constant; is its minimality (no smaller positive eigenvalue) fully derived? (Yes —
   it is the smallest positive eigenvalue, a spectral fact.)
3. **Physical clock (D_008 OP1).** Can ω₁ be promoted to a physical frequency without
   external calibration? (Currently: BOUNDARY.)

---

## Next Steps

- **ResearchY-D_010 (or synthesis):** the minimum-excitation audit (this) completes the
   first-excitation analysis; a synthesis with D_008 maps the reference-unit boundary.
- **ResearchY-A_001 follow-up:** the fundamental doublet's role as the first-peak
   candidate connects to the minimum-excitation result.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_009_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_009_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_009_ZeroMode` | ω₀ = 0 (reference state) | ✅ |
| `Y_D_009_MinimumExcitation` | ω₁ = 0.6216 > 0, the smallest positive frequency | ✅ |
| `Y_D_009_MinimumDifference` | ω₁ is the first difference above the zero mode | ✅ |
| `Y_D_009_ActualizationEvent` | minimum excitation as count event is interpretive (EMERGENT) | ✅ |
| `Y_D_009_NoStateBetween` | zero positive states in (0, ω₁) — spectral gap λ₂=ω₁² | ✅ |
| `Y_D_009_Classification` | first frequency/difference DERIVED; actualization EMERGENT; clock BOUNDARY | ✅ |
| `Y_D_009_Run` | Research report | ✅ |

**Conclusion:** ω₁ IS the minimum non-zero excitation — the first frequency and first
difference above the zero mode (DERIVED, isolated by the spectral gap λ₂=ω₁²); "first
actualization" is an EMERGENT interpretation; as a physical clock it is BOUNDARY. No
canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_009"`

---

## References

- ResearchY-D_008 (ω₁, λ₂, reference unit), D_003 (resonance observables), A_001
  (fundamental doublet).
- Monograph V2.0: Ch6 (D96 spectrum), Ch7/Ch8 (spectral gap, LOCKING).
- AT-QG: QG155 (D96 symmetry), QG157 (effective access).
