# ResearchY-D_003 — Resonance Observables Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_003 (permanent)
**Title:** Resonance Observables Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_003.md`
**Depends on:** ResearchY-D_002 (canonical standing-wave model)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_003_Tests.cs`

---

## Purpose

Determine whether **resonance alone** — the standing-wave content of the D96 spectrum
(D_002) — can generate **physical observables**, and classify each resonance-driven
quantity as **DERIVED, EMERGENT, or BOUNDARY**.

## Accepted (from D_002 and the canonical claim classification)

- The canonical standing-wave model is the center-free hybrid decomposition (D_002).
- The D96 spectral moments are **theorem** (exact spectral values) — claim registry.
- The sector mappings (which moment reads which sector) are **correspondence** —
  claim registry.
- The dimensionful masses/couplings are **calibration** (anchors v, m_e) or **fit**
  (1/α_em) — claim registry.

---

## Test: can resonance alone generate physical observables?

### 1. Mode Occupation

The octave occupancies [4,4,87] and the occupancy moment occMom = 1900.25 are direct
reads of the spectral mode distribution.

- [4,4,87]: the counts of ω_k in the three octave bands — a pure spectral output.
- occMom = Σocc_j²/occ₀ = 1900.25 — an exact spectral identity.
- **Classification: DERIVED.** The mode occupation is theorem-class content: it follows
  from the spectrum alone with no mapping and no calibration.

### 2. Resonant Pair Access

The 47 Z2 pairs (94 paired modes + self-conjugate k=48) are a pure spectral structure.
"Access" to the pairs — which pair(s) map to which sector — is not spectral:

- The pair *structure* (λ_k = λ_{N−k}, degeneracy) is DERIVED (theorem).
- The *sector role* of the pairs (e.g., weak-isospin doublets as ring degeneracy,
  A_001 R4) is a **correspondence** — a supported mapping, not a unique derivation.
- **Classification: DERIVED (structure), EMERGENT (sector role).** Resonance gives the
  pairs; the assignment of the pairs to physical doublets is a correspondence.

### 3. Zero-Mode Role

The zero mode (ω₀ = 0, uniform) is the reference state of the standing-wave model
(D_002). Its role — the uniform background against which differences are measured — is
fully spectral.

- **Classification: DERIVED.** The zero mode's reference role follows from the spectrum
  (the constant eigenvector, λ₀ = 0).

### 4. Observable Projection

The spectral moments are derived; the projection onto *physical* observables is not
resonance alone:

- Spectral content (DERIVED): Σm = 95, Σ√m = 64.08, Σm² = 229, occMom = 1900.25,
  span = 6.40 — exact spectral reads (theorem).
- Sector projection (EMERGENT/correspondence): which moment reads the neutral sector,
  the full sector, the doublet sector, the octave sector — a supported mapping, not a
  unique derivation (claim registry: sector mappings = correspondence).
- Dimensional content (BOUNDARY/calibration): the masses and couplings need the anchors
  v and m_e (and SI conversion). 1/α_em = 137 is a documented fit.
- **Classification: DERIVED (spectral moments), EMERGENT (sector mapping),
  BOUNDARY (calibration anchors, fit).** Resonance alone produces the spectral numbers;
  the *physical* observables require the mapping and the anchors.

### 5. Spectral Invariants

The span (6.40), the moments, the Z2 pairing (47 pairs), the octave bands [4,4,87], and
the algebraic spectrum (B_002) are invariant under the ring's automorphisms (B_003).

- **Classification: DERIVED.** The invariants are theorem-class spectral content.

---

## Overall Verdict

> **Resonance alone generates the SPECTRAL observables (DERIVED): mode occupation,
> pair structure, zero-mode role, and spectral invariants. It does NOT generate the
> PHYSICAL observables: the sector mapping is EMERGENT (correspondence) and the
> dimensional values are BOUNDARY (calibration anchors and fits).**

Resonance is the *spectral source* of the observables' numbers, but the *physics* of the
observables (which sector, what units, what value) requires the additional canonical
structure: the sector correspondence and the calibration anchors.

---

## Classification Summary

| Quantity | Resonance alone? | Classification |
|---|---|---|
| mode occupation [4,4,87], occMom | YES | **DERIVED** |
| resonant pair structure (47 Z2) | YES | **DERIVED** |
| resonant pair sector role | NO (mapping) | **EMERGENT** |
| zero-mode role | YES | **DERIVED** |
| spectral moments (Σm, Σ√m, Σm², span) | YES | **DERIVED** |
| sector projection | NO (correspondence) | **EMERGENT** |
| dimensional masses/couplings | NO (anchors, fit) | **BOUNDARY** |
| spectral invariants | YES | **DERIVED** |

---

## Theorem

> **Theorem (D_003).** Resonance alone generates the spectral observables of C96 —
> the mode occupation, the resonant pair structure, the zero-mode role, and the spectral
> invariants — as derived (theorem-class) content. It does not generate the physical
> observables: the sector mapping is a correspondence (emergent) and the dimensional
> values are calibrated (boundary).
>
> *Proof sketch.* (1) The spectral quantities — [4,4,87], occMom, the moments, the span,
> the 47 Z2 pairs, the zero mode — are exact functions of the spectrum alone (Sections
> 1, 2, 3, 5). (2) The physical observables require (a) the sector assignment (which
> moment reads which sector — a correspondence, claim registry) and (b) the calibration
> anchors v, m_e and the fit 1/α_em (claim registry). Neither is spectral content.
> Hence resonance generates the spectral numbers but not the physical values. ∎

---

## Dependency Graph

```
D_002 (standing-wave model: 95 modes, 47 pairs, zero mode)
  → spectral observables (DERIVED): occupation, pairs, zero mode, invariants
  → sector mapping (EMERGENT): correspondence (claim registry)
  → dimensional values (BOUNDARY): calibration anchors v, m_e; fit 1/α_em
```

---

## Invariant Formulation

The spectral observables generated by resonance are **translation-invariant**: the mode
occupation, pair structure, zero-mode role, and invariants are unchanged under all
automorphisms of the ring (B_003: the spectrum and mode set are invariant). The physical
observables are *not* spectral invariants — they depend on the (invariant-breaking)
sector mapping and the (external) calibration anchors.

---

## Research Conclusions

1. **Resonance alone generates spectral observables — DERIVED.** The mode occupation
   [4,4,87], occMom = 1900.25, the moments, the span, the 47 Z2 pairs, and the zero-mode
   role are exact spectral reads (theorem-class).
2. **Resonance alone does NOT generate physical observables.** The sector mapping
   (EMERGENT correspondence) and the calibration anchors/fits (BOUNDARY) are required
   for masses, couplings, and mixings.
3. **The distinction is the claim classification:** spectral = theorem (derived);
   sector roles = correspondence (emergent); dimensional values = calibration/fit
   (boundary).
4. **Resonance is the spectral source, not the complete generator.** It supplies the
   numbers; the physics of the observables requires the additional canonical structure.

---

## Open Problems

1. **Sector mapping uniqueness (D_003 OP1).** Is there a canonical derivation of the
   sector assignments (which moment reads which sector), or is the correspondence
   permanent? (Currently: correspondence — claim registry.)
2. **Anchor origin (D_003 OP2).** The calibration anchors v and m_e are imported. Can
   resonance fix them? (Currently: BOUNDARY — B_002-style algebraic argument may apply.)
3. **Resonance-generated spectral observables as invariants (D_003 OP3).** Are the
   spectral observables the *only* invariant content, and do the physical observables
   break the ring's symmetries? (Candidate observation.)

---

## Next Steps

- **ResearchY-D_004 (or synthesis):** the resonance observables audit (this) closes the
   D-group's resonance chain; a synthesis with the claim registry could verify the
   observable-classification consistency.
- **ResearchY-B_002 follow-up:** the anchor origin (OP2) connects to the π-boundary
   argument (transcendental/boundary content).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_003_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_003_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_003_ModeOccupation` | [4,4,87], occMom = 1900.25 derived from spectrum | ✅ |
| `Y_D_003_ResonantPairAccess` | 47 pairs derived; sector role is a mapping | ✅ |
| `Y_D_003_ZeroModeRole` | λ₀=ω₀=0 uniform reference (derived) | ✅ |
| `Y_D_003_ObservableProjection` | moments derived; sector mapping emergent; anchors boundary | ✅ |
| `Y_D_003_SpectralInvariants` | span, moments, Z2 pairs, octaves — invariant, derived | ✅ |
| `Y_D_003_Run` | Research report | ✅ |

**Conclusion:** resonance alone generates the SPECTRAL observables (DERIVED); the physical
observables require the sector correspondence (EMERGENT) and calibration anchors (BOUNDARY).
No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_003"`

---

## References

- ResearchY-D_002 (standing-wave model), C_001/C_002 (centerless, non-radial),
  B_002 (algebraic spectrum), B_003 (closure invariants).
- ATQG_ClaimClassificationRegistry.md (theorem/correspondence/calibration/fit).
- Monograph V2.0: Ch6 (D96 spectrum), Ch11 (SM observables).
