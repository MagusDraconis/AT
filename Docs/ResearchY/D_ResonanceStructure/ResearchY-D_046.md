# ResearchY-D_046 — ResearchY-Predictions Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_046 (permanent)
**Title:** ResearchY-Predictions Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `D_ResonanceStructure/ResearchY-D_046.md`
**Depends on:** ResearchY-R_001 (closure), NP_001 (V2.2 roadmap), the full D-series
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_046_Tests.cs`

---

## Purpose

**What new predictions follow from ResearchY results that V2.0 could not state?** The
ResearchY origin chain (D_020–D_045, R_001) produced derivation paths V2.0 lacked. This
audit identifies the structurally new predictions that follow, classifies each, and
ranks testability. **No canonical changes; research only.**

## Method

1. Compare V2.0 claims vs ResearchY results.
2. Identify consequences ABSENT from V2.0 wording.
3. Search the gauge, closure, resonance, anchor, and family sectors.
4. Classify (theorem / necessity / correspondence / calibration / boundary) and rank.
5. Output: prediction, dependency chain, falsification path.

---

## 1. V2.0 vs ResearchY — the novelty gap

| Topic | V2.0 claim | ResearchY result | New? |
|---|---|---|---|
| N=96 origin | closure-produced (implicit) | selected by observable-sector construction (D_020/D_031/D_040) | **YES** |
| Z2 pairing | input | DERIVED from oscillation (D_021) | **YES** |
| complete pairing | boundary | DERIVED from complex observability (D_035) | **YES** |
| SU(2) | gauge input | BOUNDARY; the doublet is O(2)-type, not SU(2) (D_022/D_023) | **YES** |
| state identity | — | DERIVED from Difference (D_039) | **YES** |
| frequency origin | spectral | EMERGES from tick phase rate (D_041) | **YES** |
| v = 137·ln span | v value | the dimensionless structure is D96-derived (D_044) | **YES** |
| family count | derived | 3 = floor(log₂ span)+1 with span(96)=6.4025 (D_028) | **YES** |
| span as π-analogue | — | algebraic, N-specific structural ratio (D_042) | **YES** |

---

## 2. The Prediction Catalog (8 predictions absent from V2.0)

### P1 — Spectral doublets are O(2)-type, not SU(2)  [THEOREM]

- **Prediction:** the {cos, sin} eigenspace at λ_k = λ_{N−k} transforms as SO(2)
  (Abelian, one generator J) with one reflection (P) — an O(2)-type parity doublet,
  NOT an SU(2) doublet (which needs 3 non-Abelian Pauli generators).
- **Dependency chain:** oscillation (D_021) → spectral Z2 (D_022) → real algebra
  {I, J, P, JP} (D_023) → O(2)-type doublet.
- **Falsification:** if any spectral quantity produced a second, non-Abelian continuous
  generator of the {cos, sin} eigenspace, P1 fails.
- **Testability:** mathematical (spectrum algebra).

### P2 — su(2) compact-form is EMERGENT from observability  [NECESSITY]

- **Prediction:** the weak sector must use the compact form su(2) because finite-
  dimensional unitary (probability-preserving) representations exist only there;
  sl(2,R) and su(1,1) are non-compact (unbounded boosts) and cannot host the weak
  sector.
- **Dependency chain:** spectral doublet (D_021) → complexification (D_025) →
  compact-form selection by unitarity (D_026).
- **Falsification:** a probability-preserving finite-dim representation of sl(2,R) or
  su(1,1) hosting the weak sector would refute P2.
- **Testability:** mathematical (representation theory).

### P3 — N=96 is selected, not closure-produced  [THEOREM]

- **Prediction:** among the zero-defect rings (6|N, complete pairing) in [60,120), only
  96 = 3·2⁵ is an octave rung. The selection is the observable-sector construction
  (Z2-paired + 3 families + octave rung), not the closure dynamics (D_019/D_020/D_040).
- **Dependency chain:** complete pairing (D_035) → p=3 (D_031) → 6|N → octave rung →
  N=96 (D_031/D_040).
- **Falsification:** a closure dynamic that stabilizes a DIFFERENT zero-defect size as
  its unique fixed point would require re-examination; a zero-defect non-rung ring
  selected over 96 would refute.
- **Testability:** mathematical (ring classification; verified: {60,66,…,120}, rung
  {96}).

### P4 — Frequency emerges from the tick phase rate  [NECESSITY]

- **Prediction:** ω₁ ≈ √91·(2π/N) — the fundamental frequency is √91 times the
  phase-quantum-per-tick (Δθ = 2π/N). Verified: ω₁·N/(2π) = 9.50 vs √91 = 9.54.
- **Dependency chain:** tick ordering (D_041) → phase θ = 2πk/N → frequency ω₁ =
  √91·(2π/N) (D_041).
- **Falsification:** a spectrum whose fundamental mode is not proportional to 1/N with
  the √91 K=6 geometric factor.
- **Testability:** mathematical (spectral asymptotics).

### P5 — span is the algebraic π-analogue, N-specific  [THEOREM]

- **Prediction:** span(96) = 6.4025 is the structural ratio of the C96 ring — π's role
  but algebraic (integer-matrix spectrum) and N-specific (span ~ 0.0578·N). There is NO
  universal N-invariant ratio analogous to π's scale-independence.
- **Dependency chain:** N=96 → Spectrum → span = ωmax/ω₁ (D_028/D_042).
- **Falsification:** a ratio of D96 spectral invariants invariant across all N.
- **Testability:** mathematical (verified spans 4.02/6.40/12.78 at N=60/96/192).

### P6 — v = 137·ln(span) = 254.37 GeV (structure derived)  [CORRESPONDENCE]

- **Prediction:** the weak scale's dimensionless structure is v = 137·ln(span) =
  254.37 GeV, with 137 = Σm+#d (the fine-structure denominator) and ln(span) from the
  derived span. Only the GeV unit is boundary.
- **Dependency chain:** N=96 → span (D_028) → v = 137·ln(span) (D_044/QG168).
- **Falsification:** a measured weak scale inconsistent with 254.37 GeV (beyond
  convention) or a D96 construction of the GeV unit.
- **Testability:** experimental (weak-scale precision).

### P7 — v/m_e ≈ 4.98e5 is irreducible  [BOUNDARY]

- **Prediction:** v/m_e is NOT a canonical spectral number; no construction of m_e from
  v (or vice versa) exists. Falsifiable: finding such a construction would reduce the
  anchor count from 2 to 1.
- **Dependency chain:** {v, m_e} anchors (D_012/D_013) → irreducibility (D_044).
- **Falsification:** a canonical D96 expression producing m_e (or v) from the other.
- **Testability:** experimental (precision mass measurements).

### P8 — 3 families = floor(log₂ span)+1  [THEOREM]

- **Prediction:** family count = floor(log₂ span)+1; with span(96) = 6.4025 this gives
  exactly 3 families. The family-count VALUE is derived from the spectrum; the 3-family
  WINDOW is the boundary input (D_020/D_040).
- **Dependency chain:** N=96 → span (D_028) → floor(log₂ span)+1 = 3 (D_016/D_028).
- **Falsification:** a family count inconsistent with floor(log₂ span)+1 for the N=96
  spectrum.
- **Testability:** mathematical (verified floor(log₂ 6.4025)+1 = 3).

---

## 3. Classification and Ranking

| # | Sector | Classification | Testability |
|---|---|---|---|
| P1 | gauge | **THEOREM** | mathematical |
| P2 | gauge | **NECESSITY** | mathematical |
| P3 | closure | **THEOREM** | mathematical |
| P4 | resonance | **NECESSITY** | mathematical |
| P5 | resonance | **THEOREM** | mathematical |
| P6 | anchor | **CORRESPONDENCE** | experimental |
| P7 | anchor | **BOUNDARY** | experimental |
| P8 | family | **THEOREM** | mathematical |

**Ranking (experimentally testable):** P6 (weak scale) > P7 (v/m_e) > P4 (frequency
law, indirect).
**Ranking (mathematically testable):** P1–P3, P5, P8 (all verified by the test suite).

---

## 4. Novelty audit

Every prediction above is **absent from V2.0 wording** because it depends on a
ResearchY derivation path that V2.0 did not have:

- P1/P2 need D_021–D_026 (pairing/algebra/compact-form chain).
- P3 needs D_031/D_035/D_040 (p=3, complete pairing, reclassification).
- P4 needs D_041 (tick/phase/frequency origin).
- P5 needs D_042 (π-analogue).
- P6 needs D_028/D_044 (span, v structure).
- P7 needs D_012/D_013/D_044 (anchor irreducibility).
- P8 needs D_028 (span → families).

**No canonical AT value, equation, or claim status was changed.** These are
predictions-stated-from-the-origin-chain, not re-statements of V2.0 content.

---

## 5. xUnit candidates

The test suite `Y_D_046_Tests.cs` verifies the mathematical predictions (P1–P5, P8)
numerically:

| Test | Verifies |
|---|---|
| `Y_D_046_GaugeSector` | P1 (O(2)-type doublet, 1 generator) + P2 (su(2) from unitarity) |
| `Y_D_046_ClosureSector` | P3 (only 96 is a zero-defect octave rung) |
| `Y_D_046_ResonanceSector` | P4 (ω₁ ≈ √91·(2π/N)) + P5 (span N-specific) |
| `Y_D_046_AnchorSector` | P6 (v = 137·ln span) + P7 (v/m_e irreducible) |
| `Y_D_046_FamilySector` | P8 (floor(log₂ span)+1 = 3) |
| `Y_D_046_Run` | research report |

---

## Theorem

> **Theorem (D_046).** ResearchY produces 8 structurally new predictions absent from
> V2.0, all consequences of the origin chain: P1 O(2)-type spectral doublets (not
> SU(2)); P2 su(2) compact-form emergent from unitarity; P3 N=96 selected (not
> closure-produced); P4 frequency from the tick phase rate (ω₁ ≈ √91·(2π/N)); P5 span
> as algebraic N-specific π-analogue; P6 v = 137·ln(span) = 254.37 GeV (structure
> derived, GeV boundary); P7 v/m_e irreducible; P8 family count = floor(log₂ span)+1 =
> 3. Each has a dependency chain into the ResearchY origin path and an explicit
> falsification path. Classifications: P1/P3/P5/P8 THEOREM, P2/P4 NECESSITY,
> P6 CORRESPONDENCE, P7 BOUNDARY. No canonical change.
>
> *Proof sketch.* (1) The origin chain (D_020–D_045, R_001) provides derivation paths
> V2.0 lacked (Section 1). (2) Each prediction is a direct consequence (Section 2,
> verified). (3) Each is absent from V2.0 wording (Section 4). (4) Each has a
> falsification path (Section 2). ∎

---

## Dependency Graph

```
Difference → Actualization → Spectrum (N=96)
 → span (D_028) → families (P8, D_016/D_028)
 → tick → phase → ω₁ ≈ √91·(2π/N) (P4, D_041)
 → span as π-analogue (P5, D_042)
 → v = 137·ln span (P6, D_044)
 → {v, m_e} irreducible (P7, D_012/D_044)
 → pairing (D_021) → O(2)-type doublet (P1, D_022/D_023)
 → su(2) from unitarity (P2, D_026)
 → complete pairing (D_035) → p=3 (D_031) → N=96 selected (P3, D_040)
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Are the predictions absent from V2.0? | **YES** (each needs a ResearchY derivation path) |
| Are they consequences of the origin chain? | **YES** (Section 2 dependency chains) |
| Do any require canonical changes? | **NO** (research only) |
| Are the mathematical ones testable? | **YES** (P1–P5, P8 verified by the suite) |
| Are the experimental ones testable? | **YES** (P6, P7 — precision mass/scale measurements) |

---

## Counterexamples

1. **P1**: if the {cos, sin} eigenspace carried a 3-generator non-Abelian algebra, the
   weak-isospin reading would be derived — it is NOT (D_023).
2. **P3**: a closure dynamic stabilizing N=64 (1-unpaired) as its unique fixed point
   would contradict the selection claim — the observable-sector construction excludes
   it (D_020).
3. **P5**: a universal N-invariant ratio would break P5 — none exists (D_042).
4. **P7**: a canonical D96 construction of m_e from v would reduce the anchor count —
   none exists (D_013).

---

## Classification

| Component | Status |
|---|---|
| P1 O(2)-type doublet | **THEOREM** |
| P2 su(2) from unitarity | **NECESSITY** |
| P3 N=96 selected | **THEOREM** |
| P4 frequency from ticks | **NECESSITY** |
| P5 span π-analogue | **THEOREM** |
| P6 v = 137·ln span | **CORRESPONDENCE** |
| P7 v/m_e irreducible | **BOUNDARY** |
| P8 families = floor(log₂ span)+1 | **THEOREM** |

**ResearchY yields 8 new predictions (P1–P8), each absent from V2.0 and each with a
dependency chain and falsification path. No canonical change; research only.**

---

## Open Problems

1. **Experimental reach (D_046 OP1).** P6/P7 are the only directly experimental
   predictions; their precision (weak scale, v/m_e) is the experimental frontier.

---

## Next Steps

- **ResearchY-NP_001 follow-up:** these predictions are the V2.2 targets' testable
  cores (T7 = P7, T1 = measurement). Each can seed a dedicated V2.2 audit.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_046_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_D_046_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_046_GaugeSector` | P1 + P2 | ✅ |
| `Y_D_046_ClosureSector` | P3 | ✅ |
| `Y_D_046_ResonanceSector` | P4 + P5 | ✅ |
| `Y_D_046_AnchorSector` | P6 + P7 | ✅ |
| `Y_D_046_FamilySector` | P8 | ✅ |
| `Y_D_046_Run` | research report | ✅ |

**Conclusion:** ResearchY produces 8 structurally new predictions (P1–P8) absent from
V2.0, each a consequence of the origin chain with a dependency chain and falsification
path. P1/P3/P5/P8 are theorems, P2/P4 necessities, P6 a correspondence, P7 a boundary.
No canonical changes; research only.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_046"`

---

## References

- ResearchY-D_020–D_045, R_001 (the origin chain), NP_001 (V2.2 roadmap).
- AT-QG: QG168 (weak scale), QG173 (fermion masses), QG159/160 (D96/period-3).
- V2.0 Prediction Registry (AT-P### — the existing 41 predictions; P1–P8 are new).
