# ResearchY-D_014 — Two-Anchor Structure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_014 (permanent)
**Title:** Two-Anchor Structure Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_014.md`
**Depends on:** ResearchY-D_012 (minimal anchors), D_013 (anchors irreducible)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_014_Tests.cs`

---

## Purpose

Determine **why physics requires exactly two irreducible anchors {v, m_e}** (D_012,
D_013), and whether the two-anchor structure is a **consequence of D96** or a supported
(boson/fermion) interpretation.

## Accepted (from D_012, D_013)

- The minimal anchor set is {v, m_e} — irreducible (D_012, D_013).
- v fixes the energy scale (M_W, M_Z, M_H, M_Pl); m_e fixes the fermion masses.

---

## 1. Treat v as the bosonic anchor, m_e as the fermionic anchor

| Anchor | Type | Calibrates |
|---|---|---|
| v (weak scale) | bosonic | M_W, M_Z, M_H (gauge/Higgs bosons), M_Pl (gravity) |
| m_e (electron) | fermionic | all quark/lepton masses (m_u..m_t, m_μ, m_τ) |

The reading "v = bosonic scale, m_e = fermionic scale" is a natural *label*: the two
anchors calibrate the bosonic (gauge/gravity) and fermionic (matter) observables
respectively.

---

## 2. Test the splits

| Split | v ↔ m_e mapping | Verdict |
|---|---|---|
| boson/fermion | v → bosons (W,Z,H,gravity); m_e → fermions (quarks, leptons) | consistent (supported) |
| even/odd sector | not a canonical AT sector split | no canonical mapping |
| gauge/matter | v → gauge (weak/Higgs); m_e → matter (fermion masses) | consistent (supported) |
| doublet structure | Z2 pairs → doublets (A_001 R4) — but anchors are not doublet-related | no direct link |
| family structure | octave bands → families (D_004) — but anchors are not family-related | no direct link |

The boson/fermion and gauge/matter splits are *consistent* with the two-anchor reading;
the even/odd, doublet, and family splits are not canonical mappings of the anchors.

---

## 3. Is the two-anchor structure a consequence of D96?

- **v's dimensionless structure IS D96-derived:** v = (Σm + #d)·ln(span) is a spectral
  construction (D_012/D_013). The *form* of v comes from D96.
- **The anchor COUNT (2) is NOT D96-derived:** the count is the calibration split —
  one anchor for the bosonic energy scale, one for the fermion masses. D96 fixes the
  dimensionless ratios, not the number of physical anchors.
- **m_e has no D96 construction** — it is a pure import.

**Verdict: PARTIAL.** D96 determines the form of v; it does not determine the two-anchor
count. The two-anchor structure is a consequence of the physics (bosonic + fermionic
scales), not of the D96 spectrum.

---

## 4. Prove or refute: two anchors correspond to two physical sectors

**PARTIALLY SUPPORTED (EMERGENT interpretation).**

- **Supporting:** the two anchors calibrate two distinct observable families — the
  bosonic (gauge/gravity) scale and the fermionic (matter) scale. This is a coherent
  reading.
- **Not a derivation:** the sector split is a *posteriori* labeling of the calibration
  structure. D96 does not force "one anchor per sector"; the count (2) follows from the
  dimensional structure of the observables (energy scale + mass scale), not from the
  spectrum.

**The two-anchor ↔ two-sector correspondence is an EMERGENT interpretation, not a
DERIVED consequence.**

---

## Theorem

> **Theorem (D_014).** The two-anchor structure {v, m_e} admits a boson/fermion
> interpretation, but is not a consequence of the D96 spectrum.
>
> *Proof sketch.* (1) The two anchors calibrate distinct observable families: v the
> bosonic scale (M_W, M_Z, M_H, M_Pl), m_e the fermionic masses (Section 1). (2) The
> boson/fermion and gauge/matter splits are consistent with this reading; the even/odd,
> doublet, and family splits are not canonical anchor mappings (Section 2). (3) v's
> dimensionless form is D96-derived ((Σm+#d)·ln(span)), but the anchor COUNT is not
> spectral — it is the calibration split, and m_e has no D96 construction (Section 3).
> (4) Hence the two anchors correspond to two physical sectors only as a supported
> (EMERGENT) interpretation, not as a derived (DERIVED) consequence. ∎

---

## Dependency Graph

```
D_012 (minimal anchors {v, m_e}) + D_013 (irreducible)
  → D_014: two-anchor structure
  ├── v = bosonic anchor (M_W, M_Z, M_H, M_Pl)
  ├── m_e = fermionic anchor (quark/lepton masses)
  ├── boson/fermion & gauge/matter splits: consistent (EMERGENT)
  ├── even/odd, doublet, family splits: no canonical anchor mapping
  ├── D96 consequence: PARTIAL (v's form derived; the count is not)
  └── two anchors ↔ two sectors: EMERGENT interpretation
```

---

## Anchor Interpretation

```
{v, m_e}  (D_012, D_013)
  ├── v → bosonic sector scale (gauge W/Z/H + gravity M_Pl)   [EMERGENT label]
  └── m_e → fermionic sector masses (quarks, leptons)          [EMERGENT label]
```

The interpretation is coherent but a posteriori: the calibration structure splits
naturally into bosonic and fermionic observables, and the two anchors map onto them.

---

## Research Conclusions

1. **v = bosonic anchor, m_e = fermionic anchor** — a natural, supported reading.
2. **The boson/fermion and gauge/matter splits are consistent** with the two-anchor
   structure; the even/odd, doublet, and family splits are not canonical anchor
   mappings.
3. **D96 determines v's form, not the anchor count.** The two-anchor structure is a
   consequence of the physical scales (bosonic + fermionic), not of the D96 spectrum.
4. **The two-anchor ↔ two-sector correspondence is EMERGENT** (a supported
   interpretation), not DERIVED.

---

## Open Problems

1. **Anchor-count origin (D_014 OP1).** Is there a deeper principle fixing the anchor
   count at 2 (bosonic + fermionic), or is it a brute calibration fact? (Currently:
   BOUNDARY.)
2. **Sector-anchor mapping (D_014 OP2).** Is the boson/fermion reading the unique
   natural split, or could other sector decompositions map to the anchors? (Currently:
   the boson/fermion reading is the supported one.)
3. **Anchor-to-sector derivation (D_014 OP3).** Could a derivation link the anchors to
   the Z2 doublet / octave family structure? (Currently: no direct link.)

---

## Next Steps

- **ResearchY-D_015 (or synthesis):** the two-anchor structure audit (this) completes
   the anchor interpretation; a synthesis can map the full boundary structure.
- **ResearchY-A_001 follow-up:** the doublet/family structure connects to the
   anchor-interpretation question.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_014_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_014_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_014_BosonFermionSplit` | v→bosons, m_e→fermions (supported) | ✅ |
| `Y_D_014_EvenOddSplit` | no canonical even/odd anchor mapping | ✅ |
| `Y_D_014_GaugeMatterSplit` | v→gauge, m_e→matter (supported) | ✅ |
| `Y_D_014_DoubletStructure` | anchors not doublet-linked (no direct mapping) | ✅ |
| `Y_D_014_FamilyStructure` | anchors not family-linked (no direct mapping) | ✅ |
| `Y_D_014_D96Consequence` | v's form D96-derived; the anchor count is not | ✅ |
| `Y_D_014_TwoSectors` | two anchors ↔ two sectors: EMERGENT interpretation | ✅ |
| `Y_D_014_Run` | Research report | ✅ |

**Conclusion:** the two anchors {v, m_e} admit a boson/fermion (gauge/matter)
interpretation, but the two-anchor structure is NOT a consequence of D96 — v's form is
D96-derived, while the anchor count is the calibration split. The two-anchor ↔ two-sector
correspondence is an EMERGENT interpretation, not DERIVED. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_014"`

---

## References

- ResearchY-D_012 (minimal anchors), D_013 (irreducible anchors), D_004 (family
  structure), A_001 (R4: doublet = ring degeneracy).
- Monograph V2.0: Ch10 (gravity), Ch11 (SM masses, weak scale).
- AT-QG: QG168 (weak boson masses, v), QG173 (quark masses, m_e).
