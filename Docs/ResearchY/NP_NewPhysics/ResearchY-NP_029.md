# ResearchY-NP_029 — ħ Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_029 (permanent)
**Title:** ħ Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-02
**File:** `NP_NewPhysics/ResearchY-NP_029.md`
**Depends on:** ResearchY-D_007 (Planck scale), D_010 (unit anchoring), D_012
(minimal anchors), D_013 (anchor reduction), D_041 (time origin), NP_027/028
(Planck law / blackbody), QG_173/209 (mass derivations), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_029_Tests.cs`

---

## Purpose

**Does AT require a fundamental ħ at all?** AT derives spectral frequencies ω_k as
dimensionless ratios (D_008/D_010) and derives physical masses directly in MeV/GeV from
the two calibration anchors (v, m_e) — never passing through ħ (QG173/209/181).
Canonical prior audits already classify c and ħ as "SI unit-convention imports, not
physics anchors" (D_012) and "E = ħω requires ħ (BOUNDARY, D_010/D_012)" (D_041).
NP_029 asks the sharper question: is ħ a required fundamental constant, or merely the
dimensional bridge between derived frequencies and measured energies?

## 1. Remove ħ — what breaks?

AT's derived physical content is produced entirely without ħ:

| Derived observable | Formula | ħ used? |
|---|---|---|
| dimensionless spectrum ω_k = √λ_k | pure ratios (span 6.40, ω₁ = 0.6216) | **NO** |
| u-quark mass | m_u = m_e·Σ√m/√Σm² = 0.511·64.0825/√229 = **2.164 MeV** (QG173) | **NO** |
| Planck scale | M_Pl = v·A³ = 254.37·(95·44·87)³ = **1.2234e19 GeV** (QG181) | **NO** |
| lepton hierarchy | m_μ = m_e·(dimensionless D96 law) (QG209) | **NO** |
| ΩΛ = I_occ/ln K = 0.6839 | dimensionless (QG234) | **NO** |
| α_weak = 3/Σm, α_strong = 8/Σ√m | dimensionless (D_012) | **NO** |

**Removing ħ breaks NOTHING in the derived chain.** Every derived mass is an anchor
(m_e or v, in MeV/GeV) times a dimensionless D96 ratio. The MeV/GeV unit is defined by
the anchors, not by ħ. A scan confirms: the canonical ResearchY derivation chain
(D_ResonanceStructure + NP_NewPhysics) contains no ħ constant. ħ appears only in legacy
ResearchQG/ResearchDATA/ResearchXH analyzers that compare AT results to SI units
(e.g. G in SI, H0 in Hz) — exactly the unit-convention role.

## 2. Keep all dimensionless D96 structure

All dimensionless structure survives unchanged: ω_k = √λ_k (span 6.40, octave 1.97,
occupancy [4,4,87], 95 modes, moments Σ√m = 64.0825, Σm² = 229, occMom = 1900.25).
These are pure numbers; they carry no reference to ħ.

## 3. Can energy be replaced by pure frequency?

The question splits by unit convention:

- **In natural units (ħ = c = 1):** E[GeV] = ω — energy IS frequency, and AT's anchors
  (v = 254.37 GeV, m_e = 0.511 MeV) are already energy anchors stated in GeV/MeV. The
  frequency ratios ARE the mass ratios: m_u/m_e = Σ√m/√Σm² = 4.2347 is a pure D96
  number. **YES, energy-content is frequency-content in the derived ratios.**
- **In SI units:** E[J] = ħω requires ħ to convert the frequency of a given physical
  state to Joules. But AT never derives observables in Joules — it uses GeV/MeV
  directly. The GeV→J conversion (1 GeV = 1.602e-10 J) and the frequency→energy step
  are SI conventions, exactly like the c conversion for lengths.

So energy can be replaced by pure frequency for **all derived content** (the ratios and
the anchor-fixed GeV/MeV values). ħ is required only if one insists on Joules and Hz
simultaneously.

## 4. Compare with the v and m_e anchor logic

The two anchors v and m_e are **irreducible, dimensionful inputs** (D_012/D_013): they
fix the absolute scale of the energy and the fermion masses, and no D96 number reduces
them (v/m_e ~ 2e-6 is not a spectral number). They are genuine **physics anchors**.

ħ is different in kind:

| Property | v (254.37 GeV) | m_e (0.511 MeV) | ħ (1.055e-34 J·s) |
|---|---|---|---|
| dimensionful | yes (energy) | yes (mass) | yes (action) |
| needed for any derived AT observable | **yes** (energy scale) | **yes** (fermion masses) | **no** |
| carries D96 information | no (import) | no (import) | no (import) |
| appears in the canonical ResearchY derivation chain | no | yes (as anchor input) | **never** |
| physics anchor vs unit convention | physics anchor | physics anchor | **unit convention** (D_012) |

v and m_e are required because they set the absolute scale of observables that are
measured in MeV/GeV. ħ sets no AT scale: the GeV scale is fixed by v, the MeV scale by
m_e. ħ converts between unit systems (J↔GeV, Hz↔MeV), which is the role of a
unit-convention constant (like c), not a physics anchor.

## 5. What ħ actually is in AT

ħ appears in AT's narrative only in two places, both as unit conventions inherited when
talking to standard QM/GR:

1. **E = ħω** (frequency → energy in SI): D_041 already classifies "E = ħω requires ħ
   (BOUNDARY, D_010/D_012)". In AT, energies are anchored by v/m_e directly; the
   frequency content is dimensionless.
2. **G = ħc/M_Pl²** (SI gravitational constant): D_007 classifies SI G as importing
   c, ħ, GeV↔kg — BOUNDARY. AT's own gravity derivation (QG181) never uses ħ.

**ħ is the dimensional bridge between derived (dimensionless) frequencies and SI
energies — nothing more.** It is not derived from D96 (its value is not a spectral
number), does not emerge from the actualization structure, and is not required to
derive any observable AT already derives.

## Theorem

> **Theorem (NP_029).** AT does not require a fundamental ħ. All derived observables —
> the dimensionless spectrum, the quark/lepton masses (m_u = m_e·Σ√m/√Σm² = 2.164 MeV,
> QG173), the Planck scale (M_Pl = v·A³ = 1.2234e19 GeV, QG181), the lepton hierarchy
> (QG209), the cosmological fractions (ΩΛ = 0.6839, QG234), and the gauge couplings —
> are obtained as (dimensionless D96 ratios) × (the two calibration anchors v, m_e)
> stated in MeV/GeV, never invoking ħ. Removing ħ leaves every derived value unchanged
> (Section 1, verified: the ResearchY derivation chain contains no ħ constant); the dimensionless
> structure is ħ-free (Section 2); energy-content ratios equal frequency ratios
> (m_u/m_e = Σ√m/√Σm² = 4.2347, a pure D96 number) so energy can be pure frequency for
> all derived content (Section 3); and unlike the irreducible physics anchors v and m_e,
> ħ fixes no AT scale — its only roles are the SI conventions E = ħω and G = ħc/M_Pl²
> (Section 4). Classification: ħ as a fundamental constant REFUTED (removing it changes
> no derived observable); ħ as the frequency↔energy dimensional bridge BOUNDARY (a
> unit-convention import, D_012 — like c); the derived ħ-free mass/energy chain DERIVED.
> No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Remove ħ — nothing breaks. (2) Keep the dimensionless structure.
> (3) Show energy = frequency in natural units and ratios. (4) Contrast with the v/m_e
> anchor logic. ∎

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ħ is needed to derive the masses" | every mass is m_e/v × a dimensionless D96 ratio (QG173/209/181); ħ appears nowhere |
| "E = ħω makes ħ fundamental" | AT anchors energies in GeV/MeV directly; ħ is only the J↔GeV/Hz↔MeV SI conversion |
| "ħ carries D96 information" | ħ's value (1.055e-34 J·s) matches no spectral number (95, 64.08, 229, 6.40) |
| "ħ emerges from actualization" | no actualization/counting derivation produces a J·s constant; the tick is dimensionless (D_012) |
| "ħ is a physics anchor like v, m_e" | v and m_e set absolute scales AT measures; ħ sets no scale and appears only in legacy SI-comparison analyzers, never in the derived chain |

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ħ is not required | a derived AT observable whose value changes when ħ is removed |
| energy-content = frequency-content | a mass ratio ≠ a dimensionless D96 ratio |
| ħ is a unit convention (not a physics anchor) | a derivation of an absolute scale needing ħ (not just J↔GeV) |

## Classification

| Component | Status |
|---|---|
| ħ as a fundamental constant of AT | **REFUTED** (removing it changes no derived observable) |
| ħ as the frequency↔energy dimensional bridge | **BOUNDARY** (SI unit-convention import, D_012 — like c) |
| derived ħ-free mass/energy chain (m_u, M_Pl, ladder) | **DERIVED** (anchors × dimensionless D96 ratios) |
| dimensionless spectrum and frequency ratios | **DERIVED** (no ħ reference) |
| v, m_e as irreducible physics anchors | **BOUNDARY** (D_012/D_013, unchanged) |

**Conclusion:** AT does not require a fundamental ħ. ħ is the dimensional bridge between
derived frequencies and SI energies — a unit-convention import (like c), classified
BOUNDARY. As a fundamental constant it is REFUTED: the two physics anchors v and m_e,
multiplied by dimensionless D96 ratios, produce every derived mass and energy without ħ.
No new primitive; canonical AT unchanged.

---

## References

- ResearchY-D_007 (SI G imports c, ħ — BOUNDARY), D_010 (energy unit needs ħ or v),
  D_012 (c, ħ are SI unit-convention imports, not physics anchors; anchors = 2: v, m_e),
  D_013 (v, m_e irreducible), D_041 (E = ħω requires ħ — BOUNDARY), NP_027/028 (Planck
  law/blackbody: temperature/ℏ-cutoff not canonical), QG_173/209/181 (mass derivations
  without ħ), S_001 (synthesis).
