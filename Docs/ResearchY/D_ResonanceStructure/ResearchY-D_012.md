# ResearchY-D_012 — Minimal Anchor Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_012 (permanent)
**Title:** Minimal Anchor Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_012.md`
**Depends on:** ResearchY-D_007…D_011
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_012_Tests.cs`

---

## Purpose

Determine the **minimal physical anchor** — the smallest additional ingredient required
to turn the D96 structure into physical dimensions — and prove whether one anchor is
sufficient for all derived observables.

## Accepted (from D_007…D_011)

- The dimensionless Planck structure is DERIVED; the absolute scale requires the anchor v
  (D_007).
- ω₁ is the natural dimensionless reference (D_008), the minimum excitation (D_009),
  not a physical unit (D_010), and the universal dimensionless reference (D_011).

---

## 1. Definitions

| Term | Definition |
|---|---|
| dimensionless structure | the D96 spectral content: ratios, moments, occupancies, frequencies — pure numbers |
| physical dimension | a dimensionful quantity (time, length, mass, energy) requiring units |
| calibration anchor | an imported dimensionful constant that fixes the physical scale |

---

## 2. Candidate anchors

| Candidate | Dimensionful? | Can it become physical without external input? |
|---|---|---|
| ω₁ = 0.6216 | NO (dimensionless) | no (D_010/D_011) |
| λ₂ = 0.3864 | NO (dimensionless) | no |
| zero mode (ω₀ = 0) | NO | no (reference only) |
| N=96 closure | NO (integer) | no (a count) |
| actualization tick | NO | no (a count unit) |
| weak scale v | **YES** (GeV) | yes — the calibration anchor |

**Only the weak scale v is dimensionful.** All spectral candidates are dimensionless and
cannot produce physical units by themselves (D_010).

---

## 3. Can any candidate become physical without external input?

**NO.** All D96 structure (ω₁, λ₂, zero mode, N=96, tick) is dimensionless; a physical
dimension requires a dimensionful input (D_010, D_011). The weak scale v is the canonical
dimensionful anchor — it is *imported*, not derived from the spectrum.

---

## 4. Minimum anchor count

**Dimensionless observables require NO anchor** (DERIVED): couplings (α_weak, α_strong),
mixings (CKM, PMNS), the cosmological fractions (Ω_Λ, Ω_m), n_s, and all ratios.

**Dimensionful observables require anchors:**

| Observable | Anchor |
|---|---|
| weak/Higgs scale (M_W, M_Z, M_H) | v (weak scale) |
| Planck scale (M_Pl = v·A³) | v (D_007) |
| absolute quark/lepton masses (m_u = m_e·ratio, QG173) | **m_e** (electron anchor) |

**Minimum anchor count: 2 (v and m_e).** One anchor (v) fixes the electroweak/Planck
energy scale; the fermion masses require the second anchor m_e. (SI units additionally
import c, ħ, and the GeV↔kg conversion — unit-convention imports, not physics anchors.)

---

## 5. Is one physical anchor sufficient for all derived observables?

**NO — refuted.** One anchor (v) fixes the energy scale (M_Pl, M_W, M_Z, M_H), but the
absolute fermion masses require the second anchor m_e (QG173: m_u = m_e·Σ√m/√Σm², etc.).
Hence:

- **One anchor (v): sufficient for the electroweak/Planck energy scale** — but not for
  the fermion masses.
- **Two anchors (v, m_e): sufficient for all dimensionful observables** (with c, ħ for
  SI units).

---

## 6. Trace

```
D96 (dimensionless spectrum)
 → ratios (ω_k/ω₁, λ_k/λ₂, span — DERIVED)
 → ω₁ (universal dimensionless reference, D_011)
 → anchor (v: energy scale; m_e: fermion masses)
 → dimensions (GeV for energy/mass; c, ħ for SI length/time)
 → observables (masses, couplings, mixings, fractions, M_Pl)
```

The trace splits at "anchor": dimensionless observables need no anchor (DERIVED);
dimensionful observables need v and m_e (BOUNDARY).

---

## Theorem

> **Theorem (D_012).** The minimal physical anchor is the weak scale v; two anchors (v
> and m_e) are required for all derived dimensionful observables; one anchor is not
> sufficient.
>
> *Proof sketch.* (1) All D96 candidates (ω₁, λ₂, zero mode, N=96, tick) are
> dimensionless (D_010); only the weak scale v is dimensionful (Section 2). (2)
> Dimensionless observables (couplings, mixings, fractions) require no anchor (DERIVED).
> (3) The dimensionful energy observables (M_W, M_Z, M_H, M_Pl) require v (Section 4);
> the absolute fermion masses require m_e (QG173). (4) Hence one anchor (v) fixes the
> energy scale but not the fermion masses — one anchor is not sufficient for all
> observables; the minimum is two (v, m_e), with c and ħ for SI units (unit-convention,
> not physics anchors). ∎

---

## Dependency Graph

```
D_007 (Planck structure) + D_008 (ω₁) + D_009 (min excitation) + D_010 (not a unit)
  + D_011 (universal dimensionless reference)
  → D_012: minimal anchor
  ├── dimensionless observables → NO anchor (DERIVED)
  ├── energy scale (M_Pl, M_W, M_Z, M_H) → anchor v (BOUNDARY)
  ├── fermion masses → anchor m_e (BOUNDARY)
  └── SI units → c, ħ (unit-convention imports)
  minimal anchor count: 2 (v, m_e)
```

---

## Anchor Hierarchy

```
D96 dimensionless structure (DERIVED)
  └── + v (weak scale, GeV) → electroweak/Planck energy scale (BOUNDARY)
        └── + m_e (electron) → absolute fermion masses (BOUNDARY)
              └── + c, ħ (SI) → length/time units (unit-convention, BOUNDARY)
```

---

## Research Conclusions

1. **Only v is a dimensionful candidate** — all spectral candidates (ω₁, λ₂, zero mode,
   N=96, tick) are dimensionless.
2. **No candidate becomes physical without external input** (D_010).
3. **Dimensionless observables need NO anchor** (DERIVED): couplings, mixings, fractions.
4. **One anchor (v) is NOT sufficient** for all observables: it fixes the energy scale,
   but the fermion masses need m_e.
5. **The minimal anchor count is 2 (v, m_e)**, plus c and ħ for SI units (unit
   conventions, not physics anchors).

---

## Open Problems

1. **Anchor origin (D_007 OP1).** Can v and m_e be derived from the spectrum?
   (Currently: BOUNDARY.)
2. **Anchor reduction (D_012 OP2).** Is there a construction that fixes m_e from v (or
   vice versa), reducing the count to one? (Currently: no canonical reduction.)
3. **Unit-convention status (D_012 OP3).** Are c and ħ genuinely unit conventions, or do
   they carry physics content? (Currently: unit-convention imports.)

---

## Next Steps

- **ResearchY-D_013 (or synthesis):** the minimal-anchor audit (this) completes the
   anchor analysis; a synthesis can map the full anchor structure.
- **ResearchY-B_002 follow-up:** the anchor-origin question connects to the
   π/calibration boundary.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_012_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_012_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_012_Definitions` | dimensionless structure / physical dimension / calibration anchor | ✅ |
| `Y_D_012_Candidates` | only v is dimensionful; ω₁/λ₂/zero/N/tick are dimensionless | ✅ |
| `Y_D_012_NoExternal` | no candidate becomes physical without external input | ✅ |
| `Y_D_012_MinAnchorCount` | dimensionless: 0; dimensionful: v + m_e = 2 | ✅ |
| `Y_D_012_OneAnchorRefuted` | one anchor (v) not sufficient for fermion masses | ✅ |
| `Y_D_012_Trace` | D96 → ratios → ω₁ → anchor → dimensions → observables | ✅ |
| `Y_D_012_Run` | Research report | ✅ |

**Conclusion:** the minimal physical anchor is the weak scale v; two anchors (v, m_e)
are required for all derived dimensionful observables — one anchor is NOT sufficient
(refuted: v fixes the energy scale, m_e the fermion masses). Dimensionless observables
need no anchor (DERIVED); the anchors are BOUNDARY. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_012"`

---

## References

- ResearchY-D_007…D_011 (Planck structure, ω₁ reference/excitation/unit, universal
  reference).
- Monograph V2.0: Ch6 (D96 spectrum), Ch10 (gravity), Ch11 (SM masses).
- AT-QG: QG173 (quark masses, m_e anchor), QG181 (Newton constant, v anchor).
