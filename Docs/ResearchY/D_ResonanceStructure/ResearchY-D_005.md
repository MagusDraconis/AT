# ResearchY-D_005 — Moment Ordering Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_005 (permanent)
**Title:** Moment Ordering Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_005.md`
**Depends on:** ResearchY-D_003 (resonance observables), D_004 (sector mapping origin)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_005_Tests.cs`

---

## Purpose

Determine whether the **moment ordering** (the strictly increasing spectral moment
ladder) **uniquely determines the sector assignment** in the moment → mass mapping.

## Accepted (from D_003, D_004)

- The moment ladder is DERIVED (theorem-class spectral values).
- The sector assignment is EMERGENT (a supported correspondence, not a unique
  derivation).
- The dimensional masses are BOUNDARY (calibration anchors v, m_e).

---

## The Moment Ladder

The four spectral moments (DERIVED, strictly ordered):

| Moment | Value | Canonical sector role |
|---|---|---|
| half-moment Σ√m | 64.08 | neutral |
| first moment Σm | 95.00 | full |
| second moment Σm² | 229.00 | doublet |
| octave moment occMom | 1900.25 | up/octave |

The ordering 64.08 < 95.00 < 229.00 < 1900.25 is **strict** (DERIVED — a spectral fact).

---

## Test: moment → mass mapping

### 1. Is the assignment unique?

**NO.** The moment ladder is strictly ordered (a derived fact), but the *assignment* of
moments to sectors is a **correspondence** (D_004). The ordering does not force which
moment reads which sector. The spectral content fixes the four numbers; it does not fix
their pairing with the sector labels.

- The canonical pairing (half→neutral, first→full, second→doublet, octave→up) is the
  *supported* assignment.
- Nothing in the spectrum excludes other pairings.
- **Classification: EMERGENT (assignment not unique).**

### 2. Are alternative assignments possible?

**YES — 24 permutations exist.** The four moments can be assigned to the four sector
roles (neutral, full, doublet, octave) in 4! = 24 ways. The spectrum alone does not
select among them:

- Any permutation gives a *different* set of sector-access numbers.
- The canonical assignment is selected by *matching observation* — which is precisely the
  definition of a correspondence (a supported mapping, not a derivation).
- A permutation (e.g., first→neutral, half→full) is not excluded by the spectrum; it is
  excluded only because it does not match the observed sectors.
- **Classification: EMERGENT (alternatives possible; selection by matching observation).**

### 3. Is electron selection derived?

**NO.** The electron mass m_e = 0.511 MeV is a **calibration anchor** — an imported
dimensionful input (claim registry: masses = calibration). No moment selects the electron;
the electron sets the unit scale for the other lepton/quark masses.

- **Classification: BOUNDARY (electron is a calibration anchor).**

### 4. Is family ordering invariant?

The **octave band ordering** is DERIVED: the three bands are ordered by frequency
(band 1 = lowest frequencies [4,4,87], band 3 = highest). This is a spectral fact
(frequency ordering).

The **family labeling** (which band is e, μ, τ) is EMERGENT/conventional: the bands are
ordered, but their names are a labeling choice.

- **Classification: DERIVED (band frequency ordering), EMERGENT (family labeling).**

---

## Overall Verdict

| Question | Answer | Classification |
|---|---|---|
| Is the moment ladder ordering derived? | YES (strict: 64.08 < 95 < 229 < 1900.25) | **DERIVED** |
| Is the sector assignment unique? | NO (a correspondence) | **EMERGENT** |
| Are alternative assignments possible? | YES (24 permutations; selected by matching observation) | **EMERGENT** |
| Is electron selection derived? | NO (calibration anchor m_e) | **BOUNDARY** |
| Is family ordering invariant? | band order DERIVED; family labels EMERGENT | **DERIVED + EMERGENT** |

**Moment ordering does NOT uniquely determine sector assignment.** The ordering is a
derived spectral fact; the assignment is an emergent correspondence (24 possibilities,
canonical one selected by matching observation); the electron is a boundary (calibration
anchor).

---

## Theorem

> **Theorem (D_005).** The spectral moment ordering does not uniquely determine the sector
> assignment.
>
> *Proof sketch.* (1) The moments are strictly ordered (64.08 < 95.00 < 229.00 < 1900.25
> — DERIVED, a spectral fact). (2) The assignment of moments to sector roles is a
> correspondence: 4! = 24 permutations of the four moments onto the four sector labels
> are possible, and the spectrum alone excludes none (Sections 1, 2). (3) The canonical
> assignment is selected by matching observation — the defining property of a
> correspondence, not a derivation. (4) The electron mass m_e is an imported calibration
> anchor (no moment selects it). (5) The octave band frequency order is derived; the
> family labels are conventional. Hence the ordering constrains but does not uniquely
> determine the assignment. ∎

---

## Counterexamples

1. **Permutation counterexample.** The assignment (first→neutral, half→full,
   second→doublet, octave→up) is a valid alternative: the four moments are assigned to
   four sector roles, but the pairing differs. The spectrum does not exclude it; only
   observation does.
2. **Electron counterexample.** If a moment selected the electron, changing the moment
   would change m_e. But m_e = 0.511 MeV is fixed by measurement (calibration); no
   spectral moment determines it. The electron is not moment-selected.
3. **Labeling counterexample.** The octave bands are ordered by frequency, but naming
   band 1 "e" vs "τ" is a labeling choice. The order is derived; the names are not.

---

## Uniqueness Proof (negative)

**Claim:** the sector assignment is NOT uniquely determined by the moment ordering.

*Proof.* Suppose the assignment were uniquely determined. Then the spectrum alone would
single out one of the 24 permutations of {64.08, 95.00, 229.00, 1900.25} onto
{neutral, full, doublet, octave}. But the spectrum is invariant under relabeling the
sector names (the sector labels are not spectral objects — they are physical
assignments). Hence the spectrum cannot distinguish the permutations; uniqueness would
require a non-spectral selection principle. No such principle is part of the canonical
structure (D_004: the assignment is a correspondence). Therefore the assignment is not
uniquely determined. ∎

---

## Dependency Graph

```
D_003 (resonance observables) + D_004 (sector mapping three-layer origin)
  → D_005: moment ordering does not fix the assignment
  ├── moment ladder ordering: DERIVED (strict)
  ├── sector assignment: EMERGENT (24 permutations; correspondence)
  ├── electron selection: BOUNDARY (calibration anchor m_e)
  └── family ordering: band order DERIVED, labels EMERGENT
```

---

## Invariant Formulation

The moment ladder is invariant under the ring's automorphisms (B_003). The sector
assignment is **not** an invariant statement — it is a labeling of invariant numbers
with physical sector names. The assignment is invariant-in-numbers (the moment values
are fixed) and conventional-in-labeling (which moment reads which sector is a supported
choice, not forced by the spectrum).

---

## Research Conclusions

1. **Moment ordering is DERIVED** — the ladder is strictly increasing (a spectral fact).
2. **The sector assignment is NOT uniquely determined** — 24 permutations are possible;
   the canonical one is selected by matching observation (a correspondence, EMERGENT).
3. **Electron selection is NOT derived** — m_e is a calibration anchor (BOUNDARY).
4. **Family ordering is derived for the bands** (frequency order) and **emergent for the
   labels** (e/μ/τ naming).
5. **The negative uniqueness proof** shows the spectrum cannot distinguish the
   permutations (sector labels are not spectral objects) — confirming D_004's
   "supported, not unique."

---

## Open Problems

1. **Selection principle (D_005 OP1).** Is there any canonical principle that selects the
   canonical assignment among the 24 permutations? (Currently: matching observation —
   the correspondence's defining property.)
2. **Electron origin (D_004 OP2).** Can any spectral structure fix m_e, or is the
   electron permanently a calibration anchor? (Currently: BOUNDARY.)
3. **Family-label convention (D_005 OP3).** Is there an observable that fixes which band
   is e vs μ vs τ, or is the labeling purely conventional? (The band order is derived;
   the names are conventional.)

---

## Next Steps

- **ResearchY-D_006 (or synthesis):** the moment-ordering audit (this) completes the
  moment → mass chain; a synthesis with the claim registry can verify the assignment
  classification.
- **ResearchY-B_002 follow-up:** the electron-origin question (OP2) connects to the
  π/calibration boundary analysis.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_005_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_005_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_005_MomentOrdering` | ladder strictly ordered 64.08 < 95 < 229 < 1900.25 (DERIVED) | ✅ |
| `Y_D_005_AssignmentUniqueness` | assignment not unique (24 permutations; correspondence) | ✅ |
| `Y_D_005_AlternativeAssignments` | 4! = 24 assignments possible; canonical selected by matching | ✅ |
| `Y_D_005_ElectronSelection` | m_e = 0.511 MeV is a calibration anchor (BOUNDARY) | ✅ |
| `Y_D_005_FamilyOrdering` | band frequency order DERIVED; family labels EMERGENT | ✅ |
| `Y_D_005_Run` | Research report | ✅ |

**Conclusion:** moment ordering does NOT uniquely determine sector assignment — the
ordering is DERIVED, the assignment is EMERGENT (correspondence, 24 alternatives), the
electron is BOUNDARY (calibration anchor), and family band order is derived while labels
are emergent. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_005"`

---

## References

- ResearchY-D_003 (resonance observables), D_004 (sector mapping origin), B_003
  (invariants).
- ATQG_ClaimClassificationRegistry.md (theorem/correspondence/calibration/fit).
- Monograph V2.0: Ch6 (D96 moments), Ch11 (SM masses).
- AT-QG: QG149/150 (sector exponents), QG157 (effective access), QG210 (family index).
