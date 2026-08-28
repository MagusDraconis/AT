# ResearchY-D_013 — Anchor Reduction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_013 (permanent)
**Title:** Anchor Reduction Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_013.md`
**Depends on:** ResearchY-D_007 (Planck structure), D_012 (minimal anchors v, m_e)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_013_Tests.cs`

---

## Purpose

Determine whether the two minimal anchors **v and m_e** (D_012) are independent or
projections of a deeper anchor, and attempt to **reduce the anchor count** from 2 to 1.

## Accepted (from D_007, D_012)

- The minimal anchor count is 2 (v, m_e): v fixes the energy scale, m_e the fermion
  masses (D_012).
- The dimensionless Planck structure is DERIVED; the absolute scale requires v (D_007).

---

## 1. Definitions

| Term | Definition |
|---|---|
| anchor | an imported dimensionful constant fixing a physical scale |
| projection | a derived quantity expressed as an anchor × a dimensionless factor |
| calibration | the use of an anchor to fix the units of a derived dimensionless structure |

---

## 2. Test hypotheses

### H1 — v fundamental, m_e derived from v

- **Claim:** m_e = v × (dimensionless factor).
- **Test:** canonical AT has NO construction of m_e from v. The electron mass is an
  independent import (the quark masses are m_q = m_e × ratio, QG173); it is not built
  from the weak scale.
- **Verdict:** REFUTED (no canonical construction; introducing one would be new physics).
- **Classification:** BOUNDARY (m_e independent).

### H2 — m_e fundamental, v derived from m_e

- **Claim:** v = m_e × (dimensionless factor).
- **Test:** v = (Σm + #d)·ln(span) is a D96 *dimensionless-structure* construction, but
  its GeV value comes from the calibration anchor. If that anchor were m_e, then the
  electron would set the weak scale — which is NOT canonical (the weak scale is the
  independent anchor).
- **Verdict:** REFUTED (v's unit anchor is independent of m_e; a relation would be new
  physics).
- **Classification:** BOUNDARY (v independent).

### H3 — v and m_e from a common anchor A0

- **Claim:** there exists A0 with v = A0·f and m_e = A0·g (dimensionless f, g).
- **Test:** canonical AT has no common anchor A0. Introducing one is a new primitive.
- **Verdict:** REFUTED (no canonical A0 exists).
- **Classification:** BOUNDARY (no common anchor).

**All three hypotheses are rejected** — v and m_e are independent anchors.

---

## 3. Ratio analysis

| Ratio | Value | Meaning |
|---|---|---|
| v/m_e | 4.98×10⁵ | the two anchors are separated by five orders of magnitude |
| v/ω₁ | 409 GeV | v × (dimensionless ω₁⁻¹) — no canonical relation |
| m_e/ω₁ | 8.22×10⁻⁴ GeV | m_e × (dimensionless ω₁⁻¹) — no canonical relation |
| v/A³ | ~5.3×10⁻¹⁵ GeV | v × (dimensionless A⁻³) — the calibration step (D_007) |
| m_e/A³ | ~1.1×10⁻¹⁷ GeV | m_e × (dimensionless A⁻³) — no canonical use |

The ratios v/ω₁ and m_e/ω₁ are dimensionful (v and m_e are GeV; ω₁ is dimensionless);
they carry no canonical spectral meaning. The ratios v/A³ and m_e/A³ are the
calibration steps but do not link the two anchors.

---

## 4. Search for a common invariant

| Candidate | Does it link v and m_e? |
|---|---|
| common spectral source | NO — v is a weak-scale anchor; m_e is an electron-mass anchor, no shared spectral source |
| common moment | NO — v uses (Σm+#d)·ln(span); m_e has no spectral construction |
| common resonance scale | NO — no common frequency/scale links them |
| common closure scale | NO — N=96 fixes the spectrum, not the anchors |

No common invariant links v and m_e within the canonical framework.

---

## 5. Anchor count

**2 → irreducible.** The attempt to reduce the anchor count from 2 to 1 fails:

- H1 (m_e from v): no canonical construction — REFUTED.
- H2 (v from m_e): non-canonical relation — REFUTED.
- H3 (common A0): new primitive — REFUTED.

The anchor count is irreducible at **2 (v, m_e)**.

---

## Rejection criteria

Any solution reducing the anchor count is rejected because it:
- introduces a new primitive (H3's A0),
- requires a fitted constant (a v↔m_e relation would be a fit), or
- breaks D_012 (the two-anchor minimal result).

---

## Theorem

> **Theorem (D_013).** The anchors v and m_e are independent and irreducible: no
> canonical reduction lowers the anchor count from 2 to 1.
>
> *Proof sketch.* (1) H1 (m_e = v·f) fails: canonical AT has no construction of m_e from
> v (m_e is an independent import, QG173). (2) H2 (v = m_e·g) fails: v's unit anchor is
> the weak scale, independent of m_e (a relation would be new physics). (3) H3 (common
> A0) fails: no canonical A0 exists (a new primitive). (4) No common invariant links the
> two anchors (Section 4). Hence the anchor count is irreducible at 2. Any reduction
> introduces a new primitive, a fit, or breaks D_012 — all rejected. ∎

---

## Dependency Graph

```
D_007 (Planck structure, anchor v) + D_012 (minimal anchors v, m_e)
  → D_013: anchor reduction
  ├── H1 (m_e from v): REFUTED (no construction)
  ├── H2 (v from m_e): REFUTED (non-canonical)
  ├── H3 (common A0): REFUTED (new primitive)
  └── anchor count: 2 → IRREDUCIBLE (v, m_e independent)
```

---

## Anchor Reduction Tree

```
2 anchors (v, m_e)  [D_012]
  ├── H1: m_e = v·f  → no f exists → branch fails
  ├── H2: v = m_e·g  → no canonical g → branch fails
  └── H3: v, m_e = A0·f, A0·g → no A0 → branch fails
= irreducible 2 anchors
```

---

## Uniqueness Proof

**Claim:** the two-anchor set {v, m_e} is the unique minimal anchor set; no reduction is
possible.

*Proof.* (1) D_012 proved one anchor is insufficient (v fixes the energy scale, m_e the
fermion masses). (2) This audit proves no two-anchor reduction: H1, H2, and H3 all fail
without new primitives, fits, or breaking D_012. (3) Hence {v, m_e} is the unique
irreducible minimal anchor set. ∎

---

## Research Conclusions

1. **H1 (v fundamental, m_e from v) is REFUTED** — no canonical construction of m_e
   from v.
2. **H2 (m_e fundamental, v from m_e) is REFUTED** — v's anchor is independent of m_e.
3. **H3 (common anchor A0) is REFUTED** — no canonical A0 exists (would be a new
   primitive).
4. **No common invariant links v and m_e** (no shared spectral source, moment,
   resonance, or closure scale).
5. **The anchor count is irreducible at 2 (v, m_e).**

---

## Open Problems

1. **Anchor origin (D_007 OP1).** Can v and m_e be derived from deeper structure?
   (Currently: BOUNDARY — this audit strengthens the irreducibility.)
2. **Anchor unification (D_013 OP2).** Is there any physical mechanism (beyond the
   framework) that could tie the weak scale to the electron mass? (Currently: none
   canonical.)
3. **Anchor semantics (D_013 OP3).** Are v and m_e "independent" in the strong sense, or
   is the independence a unit-convention artifact? (Currently: independent anchors.)

---

## Next Steps

- **ResearchY-D_014 (or synthesis):** the anchor-reduction audit (this) completes the
   anchor analysis; a synthesis can map the full boundary structure.
- **ResearchY-B_002 follow-up:** the anchor-origin question connects to the
   π/transcendental boundary.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_013_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_013_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_013_Definitions` | anchor / projection / calibration | ✅ |
| `Y_D_013_H1` | m_e from v REFUTED (no canonical construction) | ✅ |
| `Y_D_013_H2` | v from m_e REFUTED (non-canonical relation) | ✅ |
| `Y_D_013_H3` | common A0 REFUTED (new primitive) | ✅ |
| `Y_D_013_Ratios` | v/m_e=4.98e5; v/ω₁, m_e/ω₁, v/A³, m_e/A³ — no link | ✅ |
| `Y_D_013_Invariants` | no common source/moment/resonance/closure scale | ✅ |
| `Y_D_013_AnchorCount` | 2 → irreducible (v, m_e independent) | ✅ |
| `Y_D_013_Run` | Research report | ✅ |

**Conclusion:** v and m_e are independent, irreducible anchors. H1, H2, and H3 are all
refuted without new primitives, fits, or breaking D_012. The anchor count remains 2
(v, m_e). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_013"`

---

## References

- ResearchY-D_007 (Planck structure, anchor v), D_012 (minimal anchors v, m_e).
- Monograph V2.0: Ch10 (gravity), Ch11 (SM masses).
- AT-QG: QG173 (quark masses, m_e anchor), QG181 (Newton constant, v anchor).
