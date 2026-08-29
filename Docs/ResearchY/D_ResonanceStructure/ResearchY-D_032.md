# ResearchY-D_032 — Pairing-Requirement Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_032 (permanent)
**Title:** Pairing-Requirement Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_032.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry),
D_022 (weak-isospin entry), D_031 (seed-origin)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_032_Tests.cs`

---

## Purpose

**Why must the observable sector be completely paired (0 unpaired modes)?** D_031 showed
p=3 is derived from complete pairing. This audit asks whether the complete-pairing
requirement itself is derived or the final boundary.

## Accepted (from D_020, D_021, D_022, D_031)

- The pairing STRUCTURE (cos/sin quadrature pairs) is DERIVED from oscillation (D_021).
- The complete pairing (0 unpaired) is the observable-sector INPUT selecting p=3 and
  N=96 (D_020, D_031).
- The weak-isospin doublet is the SU(2) fundamental (D_022); the doublet shape is the
  EMERGENT attachment.

---

## 1. The self-conjugate mode

The self-conjugate mode is k = N/2 (the antipodal harmonic). At k = N/2:

```
sin(2π·(N/2)·n/N) = sin(πn) = 0   — the sin quadrature VANISHES
cos(2π·(N/2)·n/N) = (−1)ⁿ         — only the cos harmonic survives
```

So the self-conjugate mode has **only ONE real harmonic** (cos), no sin partner
(verified: max|sin| ≈ 10⁻¹⁴ at k=N/2 for all tested N).

---

## 2. Complete vs incomplete pairing

The antipodal eigenvalue is **fixed at λ(N/2) = 12** for all even N ≥ 12, but its
**multiplicity varies**:

| N | λ(N/2) | multiplicity | unpaired | pairing |
|---|---|---|---|---|
| 64 | 12 | 1 | 1 | **INCOMPLETE** |
| 80 | 12 | 1 | 1 | INCOMPLETE |
| **96** | **12** | **5** | **0** | **COMPLETE** |
| 128 | 12 | 1 | 1 | INCOMPLETE |
| 192 | 12 | 5 | 0 | COMPLETE |

- **Complete** (0 unpaired): the self-conjugate mode sits in a degenerate group (λ=12 is
  5-fold at N=96/192; k = N/6, N/3, N/2, 2N/3, 5N/6 all share it).
- **Incomplete** (1 unpaired): the self-conjugate mode is isolated (λ=12 is 1-fold at
  N=64/80/128).

The 5-fold group provides the complete quadrature structure for λ=12: even though k=N/2
alone lacks the sin quadrature, the other members (k = 16, 32, 64, 80 at N=96) have full
cos/sin pairs.

---

## 3. What fails with unpaired modes?

The unpaired mode (a lone, non-degenerate self-conjugate mode) lacks:

| Failed structure | Why |
|---|---|
| **phase freedom** | only cos(ωt), no sin(ωt) component at that frequency |
| **representation closure** | the mode has no 2D (doublet) partner |
| **symmetry closure** | reflection maps cos → cos (self), no partner |
| **weak-isospin attachment** | no doublet for the SU(2) fundamental reading (D_022) |

A single real mode cos(ωt) IS a valid oscillator — what is lost is the **phase freedom**
and the **doublet structure**.

---

## 4. The degenerate group supplies the completeness

Complete pairing is about the **eigenvalue's eigenspace**, not the individual mode:
every eigenvalue must have multiplicity ≥ 2 at N=96, so every frequency carries a
(at least) doublet structure.

| N | modes in doublets / total | unpaired |
|---|---|---|
| 64 | 62/63 = 0.984 | 1 |
| **96** | 84/95 = 0.884 (all others in 5/6-fold) | **0** |
| 128 | 126/127 = 0.992 | 1 |
| 192 | 180/191 = 0.942 (all others in 5/6-fold) | 0 |

At N=96 every eigenvalue has multiplicity ≥ 2 — the weak-isospin reading (D_022) can
attach to EVERY mode. At N=64, λ=12 is a lone singlet — no weak-isospin doublet.

---

## 5. Is complete pairing required by…?

| Candidate | Verdict |
|---|---|
| A) observability | **PARTIAL** — a single mode is observable, but its phase/doublet structure is incomplete |
| B) count conservation | **NO** — the count is conserved regardless of pairing (pairing is a spectral symmetry, not a count property) |
| C) oscillation symmetry | **YES** — the Z2 quadrature structure (D_021) requires two quadratures per mode; an unpaired mode has one |
| D) closure consistency | **NO** — closure converges regardless; pairing is a spectral fact |
| E) none | NO |

**Complete pairing is required by the doublet-structure observability (A/C): the
requirement that every observable frequency carry a full quadrature/doublet structure.**

---

## 6. Remove complete pairing: what survives, what breaks?

| Removed | Survives | Breaks |
|---|---|---|
| complete pairing (allow 1 unpaired) | the spectral content (families, moments, span) | the doublet structure: one frequency is a lone singlet (no weak-isospin partner) |

Removing complete pairing leaves the spectrum intact but breaks the
**every-frequency-is-a-doublet** structure — the weak-isospin reading can no longer
attach to all modes.

---

## 7. The minimal principle forcing 0 unpaired modes

The minimal principle is:

```
Every observable frequency must carry full phase/doublet structure.
```

If the observable sector is the weak-isospin doublet structure (D_014/D_022), then every
observable frequency must sit in a 2D (or higher) eigenspace — forcing 0 unpaired modes.
This is the observable-sector construction (D_020).

---

## Theorem

> **Theorem (D_032).** The pairing STRUCTURE is DERIVED (cos/sin quadrature pairs from
> oscillation, D_021), but the COMPLETENESS (0 unpaired modes) is BOUNDARY: it is the
> observable-sector requirement that every frequency carry a full doublet/phase
> structure. The self-conjugate mode k=N/2 has sin(πn) = 0 (vanishing quadrature);
> complete pairing requires it to sit in a degenerate group (λ=12 5-fold at N=96/192,
> 1-fold at N=64/80/128). The unpaired mode has no weak-isospin doublet partner. Complete
> pairing is NOT required by count conservation (B) or closure (D); it is required by
> the doublet-structure observability (A/C), the observable-sector construction (D_020).
> Everything downstream (p=3, N=96) is DERIVED from this input.
>
> *Proof sketch.* (1) The self-conjugate mode k=N/2 has sin(πn) = 0 — one quadrature
> (Section 1, verified). (2) λ(N/2) = 12 always; its multiplicity is 5 at N=96/192 and
> 1 at N=64/80/128 (Section 2, verified). (3) An unpaired mode has no doublet partner —
> phase/representation/symmetry closure fail (Sections 3–4). (4) The 5-fold group
> supplies the completeness for λ=12 (Section 4). (5) Complete pairing is not forced by
> count conservation or closure (Section 5). (6) It is the observable-sector requirement
> (doublet structure, D_020/D_022) — BOUNDARY. Hence pairing structure DERIVED,
> completeness BOUNDARY. ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → observable-sector construction (doublet structure)  [BOUNDARY — D_020]
 → complete pairing (0 unpaired modes)                  [BOUNDARY — the requirement]
 → self-conjugate degeneracy (λ=12, 6|N)                [DERIVED — N-arithmetic]
 → p=3 (minimal complete-pairing period)                [DERIVED — D_031]
 → N=96 (unique zero-defect octave rung)                [DERIVED]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the pairing structure derived? | **YES** (D_021: oscillation quadrature pairs) |
| Is the completeness (0 unpaired) derived? | **NO** — it is the observable-sector requirement (BOUNDARY) |
| Is the self-conjugate degeneracy N-arithmetic? | **YES** (λ=12 5-fold at N=96, 1-fold at N=64) |
| Does an unpaired mode have a doublet partner? | NO (it is a lone singlet) |
| Is complete pairing required by count conservation? | NO |
| Is complete pairing required by closure? | NO |
| Is complete pairing required by the doublet structure? | **YES** (observability, D_020/D_022) |

---

## Counterexamples

1. **N=64**: λ=12 is 1-fold — the self-conjugate mode k=32 is a lone singlet (no
   doublet partner), despite 3 families.
2. **N=80, 128**: same — 1 unpaired mode, incomplete pairing.
3. **A single mode cos(ωt)**: a valid oscillator but with only one quadrature — complete
   pairing is about the doublet structure, not bare oscillation.
4. **N=192**: complete pairing (0 unpaired) like N=96, but 4 families — completeness
   alone does not select 96.

---

## Classification

| Component | Status |
|---|---|
| pairing structure (cos/sin quadrature) | **DERIVED** (oscillation, D_021) |
| complete pairing (0 unpaired) | **BOUNDARY** (observable-sector input, D_020) |
| self-conjugate degeneracy (λ=12, 6|N) | **DERIVED** (N-arithmetic) |
| p=3 / N=96 | **DERIVED** (from the completeness input) |

**The pairing structure is DERIVED; the completeness (0 unpaired) is the BOUNDARY
observable-sector requirement.**

---

## Open Problems

1. **Doublet-sector origin (D_032 OP1).** Why the observable sector must be a doublet
   structure (weak-isospin) — whether this is derivable from a deeper symmetry is the
   D_014/D_022 boundary question.
2. **λ=12 degeneracy (D_032 OP2).** The self-conjugate eigenvalue λ=12 is fixed; its
   multiplicity tracks 6|N (N-arithmetic). A closed-form characterization of when λ=12
   is degenerate is open.

---

## Next Steps

- **ResearchY-D_033 (or synthesis):** the pairing-requirement audit completes the N=96
  chain (Difference → observable sector → complete pairing → p=3 → N=96). A synthesis
  can map the full observable-sector boundary structure.
- **D_031 follow-up:** the "completeness BOUNDARY" verdict sharpens D_031 — the p=3
  derivation rests on the observable-sector input.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_032_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_032_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_032_UnpairedModeTest` | self-conjugate k=N/2 has vanishing sin; unpaired at 64/80/128, not 96/192 | ✅ |
| `Y_D_032_ObservableCompleteness` | every eigenvalue has mult ≥ 2 at N=96 (all doublets/higher) | ✅ |
| `Y_D_032_RepresentationClosure` | λ=12 5-fold at 96/192, 1-fold at 64/80/128 | ✅ |
| `Y_D_032_SymmetryClosure` | reflection maps cos→cos (self); the group supplies partners | ✅ |
| `Y_D_032_DependencyTrace` | Difference → observable sector → complete pairing → p=3 → N=96 | ✅ |
| `Y_D_032_Run` | Research report | ✅ |

**Conclusion:** The pairing STRUCTURE is DERIVED (oscillation quadrature pairs, D_021);
the COMPLETENESS (0 unpaired modes) is **BOUNDARY** — the observable-sector requirement
that every frequency carry a full doublet/phase structure. The self-conjugate mode
k=N/2 has sin(πn) = 0 (vanishing quadrature); complete pairing requires it to sit in a
degenerate group (λ=12 5-fold at N=96/192, 1-fold at N=64/80/128). The unpaired mode has
no weak-isospin doublet partner. Complete pairing is NOT required by count conservation
or closure; it is required by the doublet-structure observability (the observable-sector
construction, D_020). Everything downstream (p=3, N=96) is DERIVED. No canonical value
was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_032"`

---

## References

- ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry), D_022
  (weak-isospin entry), D_031 (seed-origin).
- Monograph V2.0: Ch3 (actualization), Ch4 (closure), Ch6 (D96 spectrum).
- AT-QG: QG153 (doublet origin), QG159/160 (D96 selection, period-3 seed).
