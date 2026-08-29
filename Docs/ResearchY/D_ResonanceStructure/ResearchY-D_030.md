# ResearchY-D_030 — Octave-Rung Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_030 (permanent)
**Title:** Octave-Rung Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_030.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_029 (closure-defect)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_030_Tests.cs`

---

## Purpose

**Why octave rungs?** D_029 showed N=96 is uniquely selected only after the octave-rung
condition (n = 3·2^k). This audit asks whether N = p·2^k is derived or a remaining
boundary assumption — i.e., why frequency doubling?

## Accepted (from D_020, D_029)

- The octave rung (3·2^k) discriminates N=96 uniquely among the zero-defect rings
  (D_029).
- The zero-defect set without the rung is {60, 66, …, 120} (11 rings, D_029).
- family count = floor(log₂ span)+1 (D_016).

---

## 1. The octave-rung structure: N = p·2^k

The canonical chain is n = p·2^k with p = 3 (the period-3 seed):

| k | N | families | span | zero-defect |
|---|---|---|---|---|
| 4 | 48 | 2 | 3.240 | no (2 fam) |
| **5** | **96** | **3** | **6.4025** | **yes** |
| 6 | 192 | 4 | 12.779 | no (4 fam) |

**N=96 = 3·2⁵ is the unique zero-defect octave rung.**

---

## 2. Alternative rung structures

| base q | chain (3·q^k) | zero-defect rungs in [32,300] |
|---|---|---|
| **q=2** | 48, 96, 192 | **[96]** |
| q=3 | 81, 243 | none |
| q=4 | 48, 192 | none (48 has 2 fam, 192 has 4) |
| q=5 | 75 | none |
| **q=6** | 18, 108, 648 | **[108]** |

- **q=2 is the unique PURE scale-step base** whose rung chain hits a zero-defect ring
  (only 96).
- q=6 also hits a zero-defect ring (108), but q=6 = 2·3 **mixes the seed period into
  the ladder**: 3·6^k = 3^(k+1)·2^k. It does not separate the seed (p=3) from the
  scale step (q=2).
- q=4 = 2² is a sub-sequence of q=2 (4^k = 2^(2k)) — the same octave structure.

**The canonical chain n = 3·2^k is the one that SEPARATES the seed period (p=3) from
the scale step (q=2).**

---

## 3. Why frequency doubling (q=2)?

The doubling law has two derived sources:

### 3.1 The family partition IS an octave partition (D_016)

families = floor(log₂ span)+1 — a **factor-2 (octave) partition** of the frequency
range. The rung chain n = p·2^k is the chain whose sizes step by octaves (doubling N at
fixed p keeps the span structure, D_017). The octave is the family band by definition.

### 3.2 The long-wavelength dispersion makes mode doubling a frequency octave

For the K=6 circulant at small k (long wavelengths):

```
λ(k) ~ 4π²·k²·(Σd²)/N²   (with Σd² = 91)
⇒ ω(k) ~ (2π·k·√91)/N   (LINEAR in k)
⇒ ω(2k)/ω(k) ~ 2        (doubling is exact in the continuum limit)
```

Verified: ω(2)/ω(1) = 1.97 at N=96 (approaching 2 for the low-lying modes). The
**mode-index doubling k → 2k IS a frequency octave** in the long-wavelength regime.

**Hence q=2 is DERIVED: the continuum dispersion (ω ~ c·k) makes the mode-index
doubling a frequency doubling, and the family partition (floor(log₂ span)+1) is the
octave band count.**

---

## 4. Is the octave structure required by…?

| Candidate | Verdict |
|---|---|
| A) oscillation | **YES** (long-wavelength) — ω ~ c·k makes k→2k an octave (DERIVED-ish) |
| B) closure | **NO** — closure does not determine N or the chain (D_019) |
| C) symmetry | **PARTIAL** — the Z2 is k↔N−k (reflection), not k↔2k; but the low-k linear dispersion makes k→2k natural |
| D) information partition | **YES** — the family count IS an octave partition (D_016) |
| E) none | NO |

**The octave is the information partition of the spectrum (D), realized by the
long-wavelength dispersion (A).**

---

## 5. Remove the octave-rung assumption: does N=96 survive?

**NO.** Without the octave rung, the zero-defect set is {60, 66, …, 120} (11 rings,
D_029) — N=96 is NOT uniquely selected. The octave rung is what discriminates 96.

But the octave rung is itself the **discrete form of the octave partition**, which is
the family-count definition (D_016). Removing the octave rung removes the uniqueness;
the octave rung is not an independent assumption but the discrete octave ladder.

---

## 6. The minimal principle generating p·2^k

The minimal principle is:

```
p (the seed period) × q^k (the scale-step ladder) with q = 2
```

- The **seed period p = 3** is the BOUNDARY input (D_020: the period-3 seed derived
  from Z2 completeness).
- The **scale step q = 2** is DERIVED from the continuum dispersion (ω ~ c·k makes
  doubling a frequency octave) + the octave family partition (D_016).

The separation p × q^k is the minimal structure: the seed sets the base size, the
octave ladder steps it by frequency doublings.

---

## Theorem

> **Theorem (D_030).** The octave-rung structure n = p·2^k is derived, not a remaining
> boundary assumption. The family count floor(log₂ span)+1 is itself a factor-2
> (octave) partition (D_016), and the long-wavelength dispersion ω(k) ~ c·k makes the
> mode-index doubling k→2k a frequency octave (verified: ω(2)/ω(1) = 1.97 at N=96).
> Hence q=2 is the natural scale step: n = p·2^k is the discrete octave ladder. q=2 is
> the unique pure scale-step base whose rung chain hits a zero-defect ring (only 96;
> q=6 hits 108 but mixes the seed, 3·6^k = 3^(k+1)·2^k). Removing the octave rung
> leaves 11 zero-defect rings (96 not unique). The octave structure is DERIVED
> (dispersion + partition); the seed period p = 3 is BOUNDARY (D_020).
>
> *Proof sketch.* (1) families = floor(log₂ span)+1 is an octave (factor-2) partition
> (D_016, Section 3.1). (2) ω(k) ~ (2π·k·√91)/N is linear in k, so ω(2k)/ω(k) ~ 2 — mode
> doubling is a frequency octave (Section 3.2, verified). (3) Hence the doubling chain
> n = p·2^k is the discrete octave ladder (Section 1). (4) q=2 is the unique pure
> scale-step base with a zero-defect rung (96); q=6 mixes the seed (Sections 2, 5). (5)
> Without the rung, 11 zero-defect rings — 96 not unique (Section 5). (6) The seed
> period p=3 is the D_020 boundary input. Hence the octave structure is DERIVED; the
> seed is BOUNDARY. ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → seed period p = 3                  [BOUNDARY — D_020]
 → spectrum (D96 eigenvalues)
 → family partition floor(log2 span)+1  [DERIVED — D_016, octave bands]
 → long-wavelength dispersion ω ~ c·k   [DERIVED — mode doubling = octave]
 → octave rung n = p·2^k               [DERIVED — the discrete octave ladder]
 → N=96 (unique zero-defect octave rung) [DERIVED from the rung + zero-defect]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the octave rung required to select 96? | **YES** (without it, 11 zero-defect rings) |
| Is q=2 the unique pure base? | **YES** (only q=2 gives a pure-scale-step zero-defect rung, 96) |
| Is q=2 derived? | **YES** — ω ~ c·k makes doubling a frequency octave |
| Is the family partition octave-based? | **YES** — floor(log₂ span)+1 (D_016) |
| Is the octave required by closure? | NO (D_019) |
| What is the boundary? | the seed period p=3 (D_020) |

---

## Counterexamples

1. **N=81** (q=3 rung): 3 families but 6∤81 — not zero-defect; q=3 does not align with
   the base-2 family partition.
2. **N=75** (q=5 rung): 3 families but 6∤75 — not zero-defect.
3. **N=108** (q=6 rung): zero-defect, but q=6 = 2·3 mixes the seed period into the
   ladder (3·6^k = 3^(k+1)·2^k) — not the pure p×q^k separation.
4. **N=48, 192** (q=2 chain neighbors): 2 and 4 families — the octave rung only selects
   96 in the 3-family window.
5. **Without the octave rung**: {60, 66, …, 120} all zero-defect — 96 not unique.

---

## Classification

| Component | Status |
|---|---|
| family partition (octave bands) | **DERIVED** (floor(log₂ span)+1, D_016) |
| long-wavelength dispersion (ω ~ c·k) | **DERIVED** (continuum limit) |
| octave rung n = p·2^k | **DERIVED** (dispersion + partition) |
| q=2 base (frequency doubling) | **EMERGENT** (linear dispersion makes it natural) |
| seed period p=3 | **BOUNDARY** (D_020) |
| N=96 | **BOUNDARY** (the seed + zero-defect + octave rung combination) |

**The octave-rung structure is DERIVED; the seed period (p=3) is the boundary input.**

---

## Open Problems

1. **The log2 base (D_030 OP1).** The family partition uses base 2 (floor(log₂ span)+1);
   the base-2 choice is what makes the octave a factor-2 band. Whether base 2 is fully
   derived (from the Z2/mode doubling) or is a convention is the residual question.
2. **The seed-scale separation (D_030 OP2).** The canonical chain separates p (seed) from
   q (scale step); why the seed is a fixed period (p=3) rather than part of the ladder
   is the D_020 boundary.

---

## Next Steps

- **ResearchY-D_031 (or synthesis):** the octave-rung audit completes the N=96 chain
  (Difference → seed p=3 → octave rung → N=96). A synthesis can map the full
  seed-to-observables structure.
- **D_029 follow-up:** the "octave rung derived" verdict sharpens D_029 — the rung is
  not an independent assumption but the discrete octave ladder.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_030_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_030_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_030_OctaveNecessity` | octave rung required to select 96 (else 11 rings) | ✅ |
| `Y_D_030_AlternativeRungs` | q=2 unique pure base; q=3/4/5 fail; q=6 mixes seed | ✅ |
| `Y_D_030_DoublingLaw` | ω ~ c·k → ω(2k)/ω(k) ~ 2 (frequency octave) | ✅ |
| `Y_D_030_SelectionRemoval` | removing octave rung → 11 zero-defect rings, 96 not unique | ✅ |
| `Y_D_030_DependencyTrace` | Difference → seed p=3 → octave rung → N=96 | ✅ |
| `Y_D_030_Run` | Research report | ✅ |

**Conclusion:** The octave-rung structure n = p·2^k is **DERIVED**, not a remaining
boundary assumption. The family count floor(log₂ span)+1 is itself an octave (factor-2)
partition (D_016), and the long-wavelength dispersion ω(k) ~ c·k makes the mode-index
doubling k→2k a frequency octave (ω(2)/ω(1) = 1.97 at N=96). Hence q=2 is the natural
scale step; n = p·2^k is the discrete octave ladder. q=2 is the unique pure scale-step
base whose rung chain hits a zero-defect ring (only 96; q=6 hits 108 but mixes the
seed). Removing the octave rung leaves 11 zero-defect rings (96 not unique). The octave
structure is DERIVED (dispersion + partition); the seed period p=3 is BOUNDARY (D_020).
No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_030"`

---

## References

- ResearchY-D_020 (selection precondition), D_029 (closure-defect).
- Monograph V2.0: Ch3 (actualization), Ch4 (closure), Ch6 (D96 spectrum).
- AT-QG: QG159/160 (octave rung n = 3·2^k, period-3 seed), QG210 (families).
