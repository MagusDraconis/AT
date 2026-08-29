# ResearchY-D_044 — Anchor-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_044 (permanent)
**Title:** Anchor-Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_044.md`
**Depends on:** ResearchY-D_007 (Planck ratio), D_012 (minimal anchor), D_013
(anchor reduction), D_014 (two-anchor structure), D_043 (dual-anchor necessity)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_044_Tests.cs`

---

## Purpose

**What is the physical origin of v and m_e?** D_043 established that the anchor count
= 2 is EMERGENT (from sector splitting). This audit asks the deeper question: are the
anchor VALUES themselves derivable, or are they irreducible physical constants?

## Accepted (from D_007–D_043)

- Anchor count = 2 is irreducible (D_012/D_013); v = bosonic anchor, m_e = fermionic
  anchor (D_014); the dual-anchor necessity is EMERGENT from sector splitting (D_043).
- No spectral invariant links v and m_e (D_013 H1/H2/H3 REFUTED).
- v = (Σm+#d)·ln(span) = 254.37 GeV is the canonical weak-scale construction (QG168);
  M_Pl = v·A³ = 1.2234e19 GeV (D_007); m_u = m_e·Σ√m/√Σm² (QG173).
- The D96 structure is dimensionless (D_041/D_042); ω₁ is the universal dimensionless
  reference (D_011).

---

## 1. Trace the origin of v

**v = 137 · ln(span) = 254.37 GeV** (QG168/D_007):

```
v_dimensionless = (Σm + #d) · ln(span)
                = 137 · ln(6.4025)     [Σm + #d = 137]
                = 137 · 1.8567
                = 254.37
```

The **137** is the fine-structure denominator (1/α_em ≈ 137.036) — a canonical D96
quantity. **ln(span) = ln 6.4025** is the derived spectral span (D_028). So **v's
dimensionless VALUE is D96-derived**. The **GeV UNIT** — converting 254.37 to a
physical energy — is the calibration anchor (BOUNDARY, D_010/D_012).

| v component | Origin | Classification |
|---|---|---|
| 137 (Σm+#d) | D96 spectrum | **DERIVED** |
| ln(span) = ln 6.4025 | D96 spectrum (D_028) | **DERIVED** |
| product = 254.37 | D96 structure | **DERIVED** |
| GeV unit | calibration anchor | **BOUNDARY** |

---

## 2. Trace the origin of m_e

**m_e = 0.511 MeV** has **NO D96 construction** (D_013/D_014):

- No spectral expression (moment, resonance, ratio) equals 0.511 MeV.
- H1 (m_e = v·f), H2 (v = m_e·g), H3 (common A0) all REFUTED (D_013).
- m_e is the electron mass — a pure observable-sector boundary value (the fermionic
  anchor, D_014).

| m_e component | Origin | Classification |
|---|---|---|
| 0.511 MeV value | no D96 construction | **BOUNDARY** |
| role as fermionic anchor | sector split (D_014) | EMERGENT |
| independence from v | D_013 H1/H2/H3 | **BOUNDARY** |

---

## 3. Are v and m_e arbitrary calibrations, sector-boundary values, or hidden outputs?

| Option | v | m_e |
|---|---|---|
| A) arbitrary calibrations | NO — v's structure is D96-derived (137·ln span) | PARTIAL — value is a calibration, role is structural |
| B) observable-sector boundary values | **YES** — the GeV unit is the boundary anchor | **YES** — the 0.511 MeV is the boundary anchor |
| C) hidden outputs of a deeper process | NO — no deeper mechanism produces the GeV unit | NO — no construction at all |

**Both anchors are OBSERVABLE-SECTOR BOUNDARY VALUES (B): the dimensionless structure is
derived (v fully, m_e none), but the physical unit is the boundary input.** Neither is
a hidden output of a deeper process.

---

## 4. Ratio analysis

| Ratio | Value | Status |
|---|---|---|
| v/m_e | ~4.98e5 | NOT a canonical spectral number (D_013) |
| ln(v/m_e) | ~13.12 | NOT a canonical spectral number |
| M_Pl/v = A³ | 4.8094e16 | **DERIVED** (D_007 — the Planck content) |
| m_e/ω₁ | ~8.2e-4 | anchor-over-reference, not spectral |
| v/ω₁ | ~409.2 | anchor-over-reference, not spectral |

**M_Pl/v is DERIVED (a pure D96 ratio); the v/m_e and anchor/ω₁ ratios are NOT canonical
— they mix the boundary anchors with the derived structure.**

---

## 5. Does either anchor define the other?

**NO** (D_013): v/m_e ≈ 5e5 is not a canonical spectral number; no construction of m_e
from v (H1) or v from m_e (H2) or both from A0 (H3) exists. The anchors are
independent irreducible boundary values.

---

## 6. Replace v / replace m_e — what physics survives?

| Replaced | Survives | Breaks |
|---|---|---|
| v → v' | all dimensionless ratios, couplings, mixings, M_W/M_Z/M_H/M_Pl scale with v' | the absolute energy scale (W/Z/H/Planck values) |
| m_e → m_e' | all dimensionless ratios, couplings; m_u..m_t scale with m_e' | the absolute fermion masses |

Replacing an anchor re-scales its sector; the dimensionless structure (couplings,
mixings, Ω ratios) survives — consistent with D_043 (dimensionless physics DERIVED,
dimensionful physics EMERGENT).

---

## Theorem

> **Theorem (D_044).** v and m_e are observable-sector BOUNDARY values, not hidden
> outputs. v has a PARTIALLY-DERIVED structure: v = 137·ln(span) = 254.37 GeV (QG168)
> where 137 = Σm+#d (the fine-structure denominator) and ln(span) are D96-derived; only
> the GeV UNIT is the boundary anchor. m_e = 0.511 MeV has NO D96 construction — it is a
> pure boundary value (the fermionic anchor). Neither defines the other (v/m_e ≈ 5e5 not
> canonical, D_013 H1/H2/H3 REFUTED). M_Pl/v = A³ = 4.81e16 is DERIVED (D_007). Hence:
> v's dimensionless structure DERIVED (137·ln span); v's GeV unit BOUNDARY; m_e's value
> BOUNDARY (no construction); anchor independence BOUNDARY; M_Pl/v DERIVED. No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) v = 137·ln(span) = 254.37 — 137 and ln(span) are D96-derived
> (Section 1, verified). (2) m_e has no D96 construction (Section 2, D_013). (3) The
> anchor ratios v/m_e, ln(v/m_e) are not canonical (Section 4, D_013). (4) M_Pl/v = A³ is
> derived (Section 4, D_007). (5) Hence both are sector-boundary values, not hidden
> outputs (Sections 3, 5–6). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Spectrum (D96 eigenvalues)
 → dimensionless structure (137 = Σm+#d, ln(span))    [DERIVED]
 → v = 137·ln(span) = 254.37 (dimensionless value)    [DERIVED — QG168]
    → GeV unit                                       [BOUNDARY — anchor]
 → m_e = 0.511 MeV (no D96 construction)              [BOUNDARY — anchor]
 → anchor independence (v/m_e not canonical)          [BOUNDARY — D_013]
 → M_Pl/v = A³ = 4.81e16                              [DERIVED — D_007]
 → Dimensionful Physics                               [EMERGENT]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is v's dimensionless structure derived? | **YES** (137·ln span, QG168) |
| Is v's GeV unit derived? | **NO** — BOUNDARY (anchor) |
| Is m_e derived? | **NO** — BOUNDARY (no construction) |
| Does v define m_e (or vice versa)? | **NO** (D_013 H1/H2/H3 REFUTED) |
| Is M_Pl/v derived? | **YES** (A³, D_007) |
| Are the anchors hidden outputs? | **NO** — sector-boundary values |
| What survives anchor replacement? | the dimensionless structure |

---

## Counterexamples

1. **v = 137·ln(span)**: the dimensionless value is derived, but the GeV unit is NOT —
   replacing the unit convention changes nothing physical, confirming the unit is the
   boundary.
2. **m_e = 0.511 MeV**: no spectral expression reproduces it — pure boundary.
3. **v/m_e ≈ 5e5**: not a canonical spectral number (D_013) — the anchors are
   independent.
4. **M_Pl/v = A³ = 4.81e16**: a pure D96 ratio — DERIVED (the Planck content needs only
   v, D_007).

---

## Classification

| Component | Status |
|---|---|
| v dimensionless structure (137·ln span) | **DERIVED** (QG168) |
| v GeV unit | **BOUNDARY** (anchor) |
| m_e value (0.511 MeV) | **BOUNDARY** (no construction) |
| anchor independence | **BOUNDARY** (D_013) |
| M_Pl/v = A³ | **DERIVED** (D_007) |
| anchor roles (bosonic/fermionic) | **EMERGENT** (D_014) |
| dimensionful physics | **EMERGENT** |

**v is a boundary value with DERIVED dimensionless structure (137·ln span); m_e is a
pure boundary value. Neither is a hidden output of a deeper process. No new primitive;
canonical AT unchanged.**

---

## Open Problems

1. **137 origin (D_044 OP1).** Whether the fine-structure denominator 137 = Σm+#d has
   independent meaning (beyond the D96 sum) is open — the known 1/α_em correspondence
   is the deepest connection.

---

## Next Steps

- **ResearchY-D_045 (or synthesis):** the anchor-origin audit completes the
  anchor-chain (dimensionless → sectors → anchors → boundary). A synthesis can map the
  full boundary inventory: dimensionless structure DERIVED, anchors BOUNDARY,
  dimensionful physics EMERGENT.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_044_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_044_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_044_VOrigin` | v = 137·ln(span) = 254.37 — structure DERIVED | ✅ |
| `Y_D_044_ElectronOrigin` | m_e has no D96 construction (BOUNDARY) | ✅ |
| `Y_D_044_AnchorReplacement` | replacing v/m_e preserves dimensionless physics | ✅ |
| `Y_D_044_RatioAnalysis` | M_Pl/v = A³ DERIVED; v/m_e not canonical | ✅ |
| `Y_D_044_DependencyTrace` | Difference → Spectrum → v/m_e → boundary | ✅ |
| `Y_D_044_Run` | Research report | ✅ |

**Conclusion:** v and m_e are observable-sector BOUNDARY values, not hidden outputs.
v's dimensionless structure (137·ln span = 254.37, QG168) is DERIVED — only the GeV
unit is boundary; m_e (0.511 MeV) has no D96 construction (pure boundary). Neither
defines the other (D_013). M_Pl/v = A³ is DERIVED. No new primitive; canonical AT
unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_044"`

---

## References

- ResearchY-D_007 (Planck ratio), D_012 (minimal anchor), D_013 (anchor reduction),
  D_014 (two-anchor structure), D_043 (dual-anchor necessity).
- AT-QG: QG168 (weak scale v = 137·ln span), QG173 (fermion masses), QG172.
- Monograph V2.0: Ch9 (standard model).
