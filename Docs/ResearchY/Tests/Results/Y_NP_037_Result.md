# Y_NP_037_Result.md — ResearchY-NP_037 The Role of Three Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_037_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 11/11 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_037"`

---

## Summary

**Question:** Is the recurring exponent 3 — A³ (M_Pl = v·A³, QG181/183), the blackbody
DOS ω³ (N(ω) ∝ ω³), and the spatial dimension d = 3 — a DERIVED consequence of AT (one
canonical generator), or only a CORRESPONDENCE to observed 3D physics?

**Verdict: success criterion C — MULTIPLE UNRELATED APPEARANCES.** The recurring 3
decomposes into distinct quantities with different origins: (i) a structural
octave/family VALUE 3 on the single D96 ring (DERIVED given span(96), window [4,8)
BOUNDARY — D_040 unchanged); (ii) a cube-exponent VALUE 3 that is a numeric read of the
OBSERVED Planck–weak ratio (value DERIVED, QG183; law-level CORRESPONDENCE — the NEW
rung ladder e(N) = 3.73/3.00/2.50/2.14 shows the cube does NOT track the octave count
except at N=96); (iii) the hosted geometric 3s d = 3 and ω³ DOS (CORRESPONDENCE,
unchanged NP_035/036). A unified canonical generator of the recurring exponent 3 is
FALSIFIED. No new primitive; canonical AT unchanged.

## Inventory & taxonomy (Sections 1–2)

STRUCTURAL (single-ring values): period-3 seed p=3 (S1, DERIVED D_040), N=96 = 3·2⁵
(S2, DERIVED), family count 3 = floor(log₂ span)+1 (S3, VALUE DERIVED / window
BOUNDARY), occupancy [4,4,87] three octave bands (S4, DERIVED), A = 95·44·87 triple
of spectral counts (S5, DERIVED frequency-content triple), 1+3+8 gauge generators
(S6, structure derived / color-count CORRESPONDENCE), ΩΛ = I_occ/ln K ≈ 3 (S7).
GEOMETRIC (hosted): ω³ DOS (G1), d=3 exact (G2), preferred-boundary d=3 (G3), ρ-support
dimension (G4), D96⊗3 (G5), ψ-prefactor 3/2 (G6) — all CORRESPONDENCE/EMERGENT except
the p=d identity (DERIVED). DIMENSIONAL: A³ (D1), p=d (D2). NUMERICAL/COINCIDENTAL:
QG183 p=2.99984 read (N1), generations/color (N2/N3), valence-3 (N4), near-3 numerics
(N5), triplet 2j+1 (N6), ensemble no-preference (N7).

## Removal analysis (Section 3)

- Remove cube: A¹/A²/A⁴ fail the Planck scale by 100%/99.9997%/3.6e7%; only A³ within
  0.2% (QG183 power test re-verified).
- Remove 3-family window: pairing-complete rungs N=48/192/384 (2/4/5 families) still
  exist but the QG209 lepton ratio m_μ/m_e = Σm²/√occMom matches physical 206.77 only
  at N=96 (N=48 → 102.3, N=192 → 416.3).
- Remove third axis (D96⊗3 → D96⊗2): mode count changes from (π/6)R³, N∝ω³ to
  (π/4)R², N∝ω² — the observed blackbody / Stefan–Boltzmann π⁴/15 is lost.
- Remove exact d=3: the Einstein prefactor (d−1)(d−2) vanishes at d=2 but is non-zero
  for every d ≥ 3 — nothing breaks at exactly 3 (only the inequality d ≥ 3 is
  load-bearing).

## Common-generator search & falsifier (Sections 4–5)

**NEW rung-ladder test** (e(N) = ln(M_Pl/v(N))/ln A(N)):

| N | octave bands | v (GeV) | A | e(N) |
|---|---|---|---|---|
| 48 | 2 | 76.40 | 40,420 | 3.735 |
| 96 | 3 | 254.37 | 363,660 | 2.9998 |
| 192 | 4 | 715.94 | 3,075,100 | 2.502 |
| 384 | 5 | 1,843.63 | 25,129,396 | 2.138 |

e(N) is monotone decreasing; e(N) ≈ bands(N) ONLY at N=96 — the "cube = three octave
bands" anatomy is a value coincidence, not a causal generator. Axis/family decoupling:
single ring family=3/4/5 always has DOS p=1.000; the 3-family ring's tensor ⊗1/⊗2/⊗3
gives p=1/2/3; the cube e(N) is independent of both. A³, ω³ and d=3 share NO unified
origin — different inputs (span window, observed M_Pl/v, hosted dimension) all read as
3 only at the canonical ring.

## Derivation attempts (Section 6)

Octave/family 3 from the N=96 ring alone: DERIVED. Cube exponent 3 without observed
M_Pl: FAILS (e(N) not a constant of the structure). d=3 from Einstein structure alone:
FAILS (only d ≥ 3). DOS p=3 from one ring: FALSIFIED (p=1). A common generator of all
three 3s: FAILS.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_037_CanonicalStructuralThree` | N=96: [4,4,87], family 3, N=3·2⁵ | ✅ |
| `Y_NP_037_ATripleProductAndCube` | A = 95·44·87 = 363,660; M_Pl = v·A³; exponent 3 | ✅ |
| `Y_NP_037_RemoveCubeBreaksPlanckScale` | A¹/A²/A⁴ fail 100%/99.9997%/3.6e7% | ✅ |
| `Y_NP_037_RemoveFamilyThreeBreaksMassContent` | only N=96 matches m_μ/m_e = 206.77 | ✅ |
| `Y_NP_037_RemoveThirdAxisBreaksBlackbodyDos` | ⊗3→ω³, ⊗2→ω²; SB = π⁴/15 | ✅ |
| `Y_NP_037_RemoveExactD3KeepsGravityButD2Breaks` | prefactor 0 at d=2, >0 for all d ≥ 3 | ✅ |
| `Y_NP_037_RungLadderCubeVsOctaves` | e(N) = 3.73/3.00/2.50/2.14; ≈ bands only at N=96 | ✅ |
| `Y_NP_037_FamilyDosCubeDecouple` | family-3 / DOS-p / cube-e independent | ✅ |
| `Y_NP_037_DerivationAttemptFromCanonicalObjects` | only octave/family 3 derivable from ring | ✅ |
| `Y_NP_037_Classification` | DERIVED/CORRESPONDENCE/FALSIFIED flags | ✅ |
| `Y_NP_037_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| family/octave VALUE 3 at N=96 (floor(log₂ span)+1) | **DERIVED** (window [4,8) BOUNDARY — D_040 unchanged) |
| period-3 seed, 6\|N, N=96 = 3·2⁵ | **DERIVED** (D_040 unchanged) |
| triple A = 95·44·87; cube content A³ | **DERIVED** (QG181/183; NP_036 frequency-content triple) |
| cube exponent 3 in M_Pl = v·A³ | value **DERIVED** (observed M_Pl/v read); law-level **CORRESPONDENCE** (no structural generator) |
| d = 3; blackbody DOS ω³ | **CORRESPONDENCE** (hosted 3D geometry; unchanged NP_035/036) |
| 3-family window (span ∈ [4,8)) | **BOUNDARY** (unchanged D_020/D_040) |
| unified origin of recurring exponent 3 | **FALSIFIED** (rung-ladder + axis/family decoupling) |

## Conclusion

The recurring exponent 3 is NOT a single derived consequence of AT. A³, ω³ and d=3 are
multiple unrelated appearances (success criterion C): each enters AT through a
different input — the span window [4,8) (BOUNDARY) for the octave/family value 3, the
observed Planck–weak ratio for the cube exponent, and the hosted 3D spatial dimension
for the DOS ω³ / d = 3 (CORRESPONDENCE, NP_035/036). The rung-ladder falsifier
(e(N) = 3.73/3.00/2.50/2.14 ≈ bands(N) only at N=96) is the new quantitative result.
No reclassification of prior registry entries; no new primitive; canonical AT
unchanged.
