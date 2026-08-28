# ResearchY-D_004 — Sector Mapping Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_004 (permanent)
**Title:** Sector Mapping Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_004.md`
**Depends on:** ResearchY-D_003 (resonance observables audit)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_004_Tests.cs`

---

## Purpose

Locate the **exact origin** of the spectral → physics correspondence by testing four
specific mappings:

1. occupancies → families
2. moments → masses
3. gaps → couplings
4. Z2 pairs → doublets

and classify each as **DERIVED, EMERGENT, or BOUNDARY**.

## Accepted (from D_003 and the claim registry)

- Resonance alone generates the spectral observables (DERIVED); the sector mapping is
  EMERGENT (correspondence); dimensional values are BOUNDARY (calibration) — D_003.
- The claim registry: moment values = theorem; sector mappings = correspondence;
  couplings = correspondence/fit; masses = calibration.

---

## Test: the four mappings

### 1. Occupancies → Families

The octave occupancies [4,4,87] and the family count:

- The three octave bands ARE the three families: family count = floor(log₂ span) + 1 = 3
  (QG210). This is an **exact spectral identity** — the number of octave bands spanned by
  the medium.
- The band occupancies (4, 4, 87) are the mode counts per family band — exact spectral
  content.
- **Classification: DERIVED.** The families are the octave bands; the mapping is an
  identity, not an assignment. (The *labels* "family 1/2/3" are names; the structure —
  three octave bands — is derived.)

### 2. Moments → Masses

The moment ladder (Σ√m = 64.08, Σm = 95, Σm² = 229, occMom = 1900.25) and the mass
sectors:

- **The moment ladder is DERIVED** (theorem): exact spectral values.
- **The sector assignment is EMERGENT** (correspondence): which moment reads the neutral
  sector, the full sector, the doublet sector, the octave sector is a *supported mapping*
  (QG149/150/157), not a unique derivation.
- **The dimensional masses are BOUNDARY** (calibration): the absolute quark/lepton masses
  require the anchor m_e (claim registry: calibration).
- **Classification: DERIVED (ladder) + EMERGENT (sector assignment) + BOUNDARY
  (calibrated values).**

### 3. Gaps → Couplings

The spectral gaps and the gauge couplings:

- **The gap structure is DERIVED**: the locking gap λ₂ = 0.3864 is an exact spectral read
  (LOCKING operator, Ch7/Ch8).
- **The coupling reads are EMERGENT** (correspondence): α_weak = 3/Σm and
  α_strong = 8/Σ√m are spectral ratios matched to observation (claim registry:
  correspondence) — no free constant, but the ratio forms are selected.
- **1/α_em = 137 is BOUNDARY (FIT)**: a post-hoc match (claim registry: fit).
- **Classification: DERIVED (gap structure) + EMERGENT (α_weak, α_strong) + BOUNDARY
  (1/α_em fit).**

### 4. Z2 Pairs → Doublets

The 47 Z2 pairs and the weak-isospin doublets:

- **The pair structure is DERIVED**: λ_k = λ_{N−k} (47 pairs) is the ring's ±k degeneracy
  (exact).
- **The doublet reading is EMERGENT** (correspondence): reading the Z2 pairs as
  weak-isospin doublets is a *supporting interpretation* (A_001 R4, claim registry:
  sector mappings = correspondence) — the structure supports it, but the assignment is
  not uniquely derived.
- **Classification: DERIVED (pairs) + EMERGENT (doublet reading).**

---

## The Exact Origin of the Correspondence

The four mappings reveal a **three-layer origin**:

1. **The spectral structure is DERIVED (exact).** The occupancies, moment ladder, gap
   structure, and Z2 pairs are theorem-class spectral outputs. This layer is the *fixed
   numbers*.
2. **The sector assignment is EMERGENT (correspondence).** The mapping of a spectral
   quantity onto a physical sector (family, mass, coupling, doublet) is a *supported
   assignment* — the structure supports it (no fitting of the structure), but the
   labeling (which sector reads which moment/gap/pair) is not uniquely derived. This
   layer is the *interpretation*.
3. **The dimensional values are BOUNDARY (calibration/fit).** The physical units and the
   absolute values enter through anchors (v, m_e) and fits (1/α_em). This layer is the
   *measurement*.

**The exact origin of the spectral → physics correspondence is the coexistence of the
exact spectral structure (DERIVED) with the sector labels (EMERGENT) and the calibration
anchors (BOUNDARY).** The correspondence is "supported, not unique": the numbers are
fixed by the spectrum, the assignment is a supported mapping, and the units are imported.

---

## Overall Verdict

| Mapping | Structure | Assignment | Values |
|---|---|---|---|
| occupancies → families | DERIVED | DERIVED (identity) | — |
| moments → masses | DERIVED | EMERGENT | BOUNDARY (calibration) |
| gaps → couplings | DERIVED | EMERGENT | BOUNDARY (1/α_em fit) |
| Z2 pairs → doublets | DERIVED | EMERGENT | — |

**The sector mapping is EMERGENT as an assignment, DERIVED as a structure, and BOUNDARY
as dimensional values.** The families are the one case where the mapping is an identity
(derived): the octave bands ARE the families.

---

## Theorem

> **Theorem (D_004).** The spectral → physics correspondence has a three-layer origin:
> the spectral structures (occupancies, moments, gaps, Z2 pairs) are DERIVED (exact);
> their assignment to physical sectors (families, masses, couplings, doublets) is
> EMERGENT (a supported correspondence, not a unique derivation); and the dimensional
> values are BOUNDARY (calibration anchors and fits). The families are the exception:
> the occupancies → families mapping is DERIVED (the octave bands are the families).
>
> *Proof sketch.* (1) The spectral structures are exact functions of the spectrum
> (Sections 1–4: band counts, moment ladder, λ₂, Z2 pairs — all theorem-class). (2) The
> sector assignments are supported mappings per the claim registry (sector mappings =
> correspondence); no derivation fixes which moment/gap/pair reads which sector. (3) The
> dimensional values require the anchors v, m_e and the fit 1/α_em (claim registry:
> calibration/fit). (4) The family count is floor(log₂ span)+1 = 3, an exact spectral
> identity (QG210). Hence the three-layer origin. ∎

---

## Dependency Graph

```
D_003 (resonance generates spectral, not physical observables)
  → D_004: why the mapping exists
  ├── occupancies → families: DERIVED (octave identity, QG210)
  ├── moments → masses: DERIVED ladder + EMERGENT assignment + BOUNDARY calibration
  ├── gaps → couplings: DERIVED gap + EMERGENT reads + BOUNDARY fit (1/α_em)
  └── Z2 pairs → doublets: DERIVED pairs + EMERGENT doublet reading
```

---

## Invariant Formulation

The spectral structures are invariant under the ring's automorphisms (B_003). The sector
assignment is **not** an invariant statement: it is a labeling of invariant structure with
physical names. The correspondence is invariant-in-numbers (the spectral values are
fixed) and convention-in-assignment (the sector labels are supported, not forced).

---

## Research Conclusions

1. **The sector mapping is EMERGENT as an assignment.** No derivation forces which
   spectral quantity reads which physical sector; the mappings are supported
   correspondences (claim registry).
2. **The spectral structure is DERIVED.** The occupancies, moment ladder, gaps, and Z2
   pairs are exact — the correspondence's numbers are fixed, not fitted.
3. **The dimensional values are BOUNDARY.** Masses and couplings require calibration
   anchors (v, m_e) and fits (1/α_em).
4. **The families are the exception.** The occupancies → families mapping is DERIVED:
   the three octave bands ARE the three families (an identity, QG210).
5. **The exact origin** of the spectral → physics correspondence is the coexistence of
   exact spectral structure (DERIVED) + supported sector labels (EMERGENT) + imported
   dimensional anchors (BOUNDARY). The correspondence is "supported, not unique."

---

## Open Problems

1. **Family-labels uniqueness (D_004 OP1).** The three octave bands are derived; is the
   labeling "family 1/2/3" (or e/μ/τ) unique or conventional? (The bands are derived;
   the names are conventional.)
2. **Sector-assignment derivation (D_003 OP1).** Is there any canonical derivation of the
   moment → sector assignment, or is the correspondence permanent? (Currently:
   correspondence.)
3. **Anchor origin (D_003 OP2).** Can the calibration anchors v, m_e be fixed by the
   spectrum? (Currently: BOUNDARY.)

---

## Next Steps

- **ResearchY-D_005 (or synthesis):** the sector-mapping origin (this) and the resonance
   observables (D_003) complete the D-group's correspondence analysis; a synthesis with
   the claim registry can verify the observable-classification consistency.
- **ResearchY-B_002 follow-up:** the anchor origin (OP3) connects to the π-boundary
   (transcendental/boundary) argument.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_004_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_004_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_004_OccupanciesFamilies` | family count = floor(log₂ span)+1 = 3; octave bands ARE families (DERIVED) | ✅ |
| `Y_D_004_MomentsMasses` | ladder DERIVED; sector assignment EMERGENT; values BOUNDARY | ✅ |
| `Y_D_004_GapsCouplings` | gap λ₂ DERIVED; α_weak/α_strong EMERGENT; 1/α_em FIT | ✅ |
| `Y_D_004_Z2PairsDoublets` | 47 pairs DERIVED; doublet reading EMERGENT | ✅ |
| `Y_D_004_Classification` | three-layer origin: DERIVED structure + EMERGENT assignment + BOUNDARY values | ✅ |
| `Y_D_004_Run` | Research report | ✅ |

**Conclusion:** the sector mapping is EMERGENT as an assignment, DERIVED as a structure,
BOUNDARY as dimensional values — the families being the derived exception. The
correspondence's exact origin is the coexistence of exact spectral structure, supported
sector labels, and imported anchors. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_004"`

---

## References

- ResearchY-D_003 (resonance observables), A_001 (R4: doublet = ring degeneracy),
  B_003 (invariants).
- ATQG_ClaimClassificationRegistry.md (theorem/correspondence/calibration/fit).
- Monograph V2.0: Ch6 (D96 spectrum), Ch7/Ch8 (locking gap), Ch11 (SM observables).
- AT-QG: QG149/150 (sector exponents, mode access), QG157 (effective access),
  QG210 (family index).
