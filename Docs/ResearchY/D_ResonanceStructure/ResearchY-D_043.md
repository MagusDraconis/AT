# ResearchY-D_043 — Dual-Anchor-Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_043 (permanent)
**Title:** Dual-Anchor-Necessity Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_043.md`
**Depends on:** ResearchY-D_007 (Planck ratio), D_012 (minimal anchor), D_013
(anchor reduction), D_014 (two-anchor structure), D_041 (time origin), D_042
(fundamental ratio)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_043_Tests.cs`

---

## Purpose

**Why does a dimensionless structure require multiple physical anchors?** The D96
spectrum is entirely dimensionless (D_041/D_042), yet converting it to physical
dimensions requires the minimal anchor set {v, m_e} — two irreducible anchors (D_012/
D_013). This audit asks whether the dual-anchor need is FUNDAMENTAL (each physical
dimension inherently needs its own scale) or EMERGENT (it follows from the sector
structure of the observables).

## Accepted (from D_007–D_042)

- All spectral quantities are dimensionless (D_008/D_041); ω₁ is the universal
  dimensionless reference (D_011); no π-like universal ratio exists (D_042).
- Minimal anchor set = {v, m_e}; anchor count 2 is IRREDUCIBLE (D_012/D_013).
- v is the bosonic anchor (M_W/M_Z/M_H/M_Pl); m_e the fermionic anchor (quark/lepton
  masses) — the two-anchor ↔ two-sector reading is EMERGENT (D_014).
- v's dimensionless form (Σm+#d)·ln(span) is D96-derived; m_e has no D96 construction
  (D_013/D_014).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **dimensionless structure** | the D96 spectral content — pure ratios, no units (D_041/D_042) |
| **anchor** | a dimensionful input that sets an absolute scale (v, m_e) |
| **physical dimension** | a dimensionful quantity (mass, energy) requiring a unit |
| **sector** | a set of observables sharing one anchor (bosonic vs fermionic) |

---

## 2. Single anchor vs two anchors

| | One anchor (v) | Two anchors (v, m_e) |
|---|---|---|
| bosonic scale | M_W/M_Z/M_H/M_Pl = v·(dimensionless) ✓ | same ✓ |
| fermionic masses | m_u = v·2.3e-6 ✗ (needs m_e) | m_u = m_e·Σ√m/√Σm² ✓ |
| energy scale | ✓ | ✓ |
| matter masses | **NO** | ✓ |
| observables covered | bosonic sector only | bosonic + fermionic |

**A single anchor fails:** it sets the bosonic energy scale but cannot set the
fermionic absolute masses — m_u/v = (m_e/v)·(Σ√m/√Σm²) ~ 2.3e-6 requires m_e as a
second, independent input (D_012/D_013).

---

## 3. Why does a single anchor fail?

The dimensionless structure contains **physically distinct sectors**:

- **bosonic sector**: M_W/M_Z/M_H/M_Pl = v × (dimensionless ratios) — one anchor (v)
  sets the whole scale;
- **fermionic sector**: m_u/m_d/... = m_e × (dimensionless ratios) — a DIFFERENT
  anchor (m_e) sets the absolute scale.

There is **no canonical dimensionless factor linking the two sectors**: m_e/v ~ 2e-6 is
not a spectral number (D_013 H1 REFUTED). The two sector scales are independent —
neither is v×(derived ratio). Hence one anchor cannot calibrate both.

---

## 4. Bosonic sector (v) vs fermionic sector (m_e)

| Sector | Anchor | Observables | Anchor relation |
|---|---|---|---|
| bosonic (gauge/gravity) | v | M_W, M_Z, M_H, M_Pl | v·(dimensionless) |
| fermionic (matter) | m_e | m_u..m_t, m_μ, m_τ | m_e·(dimensionless) |

The two anchors map onto the two sectors (D_014). The sectors are **physically
distinct observables**: boson masses are scale-set by the weak/gravity coupling;
fermion masses by the Yukawa/matter couplings — different physical mechanisms, no
shared dimensionful relation.

---

## 5. Common dimension principle or intrinsic dual-anchor necessity?

| Candidate | Verdict |
|---|---|
| one common dimension principle | NO — no single principle sets both boson and fermion absolute scales |
| intrinsic dual-anchor necessity | **YES (EMERGENT)** — the D96 dimensionless structure hosts two distinct sectors; each sector's absolute scale requires its own anchor |

The dual-anchor necessity is **intrinsic to the sector structure**: whenever the
dimensionless theory splits into physically distinct sectors, each needs its own scale
anchor. This is not a "dimension principle" (a common rule) — it is the consequence of
**sector independence**.

---

## 6. Prove or refute: multiple anchors required whenever observables split into distinct sectors

**YES.** Each physically distinct sector has its own absolute scale (a dimensionful
input). A single anchor can set only ONE sector's scale. Therefore multiple anchors are
required whenever the dimensionless observables split into multiple physically distinct
sectors. In AT: the bosonic sector (gauge/gravity, scale v) and the fermionic sector
(matter, scale m_e) are distinct — hence two anchors.

---

## Theorem

> **Theorem (D_043).** The dual-anchor necessity {v, m_e} is EMERGENT from sector
> splitting, not a fundamental dimension principle. The D96 dimensionless structure
> hosts two physically distinct sectors: the bosonic (gauge/gravity, M_W/M_Z/M_H/M_Pl =
> v·(dimensionless)) and the fermionic (matter, m_u..m_t = m_e·(dimensionless)). Each
> sector's absolute scale requires its own anchor; no canonical dimensionless factor
> links them (m_e/v ~ 2e-6 is not a spectral number, D_013 H1 REFUTED). Hence a single
> anchor fails (m_u = v·2.3e-6 is not derivable from v alone), and the minimal anchor
> set is {v, m_e} (D_012/D_013 irreducible). Prove/refute: multiple anchors are
> required whenever observables split into physically distinct sectors — YES. Hence:
> sector split DERIVED (observable structure); anchor count EMERGENT (from sector
> splitting); each sector's anchor BOUNDARY (dimensionful input). No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) The D96 structure is dimensionless (D_041/D_042). (2) The
> observables split into bosonic and fermionic sectors with distinct scale relations
> (Section 4, D_014). (3) A single anchor sets only one sector's scale; m_u/v needs m_e
> (Section 2–3, verified). (4) No common dimensionless factor links the sectors
> (D_013). (5) Hence dual-anchor necessity is EMERGENT from sector splitting
> (Sections 5–6). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Spectrum (D96 eigenvalues)
 → Dimensionless Physics (ratios, ω₁, span)      [DERIVED — D_041/D_042]
 → observable sector split (bosonic/fermionic)   [DERIVED — D_014]
 → {v, m_e} (each sector's scale)                [BOUNDARY — dimensionful inputs]
 → anchor count 2                                [EMERGENT — from sector splitting]
 → Dimensionful Physics (M_W, m_u, ...)          [EMERGENT — calibrated observables]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the D96 structure dimensionless? | **YES** (D_041/D_042) |
| Do the observables split into distinct sectors? | **YES** (bosonic/fermionic, D_014) |
| Does one anchor fail? | **YES** (m_u needs m_e; v alone insufficient) |
| Is m_e/v a spectral number? | **NO** (~2e-6, not canonical; D_013) |
| Is the dual-anchor necessity emergent? | **YES** (from sector splitting) |
| Are the anchors boundary? | **YES** (dimensionful inputs, D_012/D_013) |
| Does sector splitting imply multiple anchors? | **YES** (each sector needs its own scale) |

---

## Counterexamples

1. **One anchor (v) for fermions**: m_u = v·2.3e-6 — not derivable; the 2e-6 factor is
   not a spectral number (D_013 H1 REFUTED).
2. **One anchor (m_e) for bosons**: M_W = m_e·(g) — no canonical g (D_013 H2 REFUTED);
   the weak scale is independent.
3. **A common anchor A0**: no canonical A0 exists (D_013 H3 REFUTED — would be a new
   primitive).
4. **Sector-free theory (hypothetical)**: if all observables shared one scale relation,
   ONE anchor would suffice — confirming that the TWO anchors track the TWO sectors.

---

## Classification

| Component | Status |
|---|---|
| dimensionless structure | **DERIVED** (D_041/D_042) |
| observable sector split (bosonic/fermionic) | **DERIVED** (D_014) |
| anchor count 2 | **EMERGENT** (from sector splitting) |
| each sector's anchor (v, m_e) | **BOUNDARY** (dimensionful inputs) |
| single-anchor failure | **DERIVED** (m_u/v not spectral) |
| dimensionful physics | **EMERGENT** (calibrated observables) |

**The dual-anchor necessity is EMERGENT from sector splitting: two physically distinct
sectors require two anchors. The anchors themselves are BOUNDARY; the sector split is
DERIVED; the anchor count is EMERGENT. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Sector-origin (D_043 OP1).** Whether the bosonic/fermionic sector split itself is
   DERIVED from a deeper D96 structure or is the physical boundary (D_014 left it
   EMERGENT) remains open.

---

## Next Steps

- **ResearchY-D_044 (or synthesis):** the dual-anchor audit completes the
  dimension-chain (dimensionless → sectors → anchors → dimensionful physics). A
  synthesis can map the full anchor-to-observable correspondence.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_043_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_043_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_043_SingleAnchor` | one anchor (v) fails to set fermion masses | ✅ |
| `Y_D_043_DualAnchor` | {v, m_e} covers bosonic + fermionic sectors | ✅ |
| `Y_D_043_BosonicScale` | M_W/M_Z/M_H/M_Pl = v·(dimensionless) | ✅ |
| `Y_D_043_FermionicScale` | m_u = m_e·Σ√m/√Σm² | ✅ |
| `Y_D_043_DimensionOrigin` | dual-anchor necessity EMERGENT from sector split | ✅ |
| `Y_D_043_Run` | Research report | ✅ |

**Conclusion:** The dual-anchor necessity {v, m_e} is EMERGENT from sector splitting.
The D96 dimensionless structure hosts two physically distinct sectors — bosonic (v) and
fermionic (m_e) — and each requires its own absolute-scale anchor; no canonical
dimensionless factor links them (m_e/v ~ 2e-6 not spectral). One anchor fails; two are
irreducible. Multiple anchors are required whenever observables split into physically
distinct sectors — YES. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_043"`

---

## References

- ResearchY-D_007 (Planck ratio), D_012 (minimal anchor), D_013 (anchor reduction),
  D_014 (two-anchor structure), D_041 (time origin), D_042 (fundamental ratio).
- AT-QG: QG173 (m_u = m_e·Σ√m/√Σm²), QG168 (weak scale), QG172 (fermion masses).
- Monograph V2.0: Ch8 (matter), Ch9 (standard model).
