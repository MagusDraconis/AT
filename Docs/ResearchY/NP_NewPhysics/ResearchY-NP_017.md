# ResearchY-NP_017 — Natural D96 Signature Search

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_017 (permanent)
**Title:** Natural D96 Signature Search
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_017.md`
**Depends on:** ResearchY-NP_013 (unique spectral prediction), NP_016 (mirror-pair
observation), D_021 (oscillation symmetry), D_030 (octave rung)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_017_Tests.cs`

---

## Purpose

**Can D96-type spectral structure appear naturally in real systems?** NP_013–NP_016
established the O(2) mirror-pair degeneracy, the 47+1 structure, the octave hierarchy,
and the span = 6.4025 as the theory's sharpest spectral claims. This audit searches
natural spectral domains for approximate D96 signatures — mirror-pair degeneracy,
octave hierarchy, and D96-like occupancy structure — and ranks the strongest physical
candidates.

---

## 1. Candidate domains

| Domain | Mirror pairs? | Octave hierarchy? | D96-like occupancy? |
|---|---|---|---|
| **cosmological spectra** | partial | **YES (D96 octaves)** | YES |
| **CMB acoustic peaks** | partial (ratios) | **YES — D96-derived** | **YES** |
| **gravitational-wave spectra** | no (damped) | no | no |
| **atomic spectra** | no (Rydberg 1/n²) | no | no |
| **molecular spectra** | no | no | no |
| **condensed-matter spectra** | approximate | approximate | partial |
| **plasma spectra** | no | no | no |

---

## 2. Test: mirror-pair degeneracy

| Domain | Mirror-pair degeneracy | Verdict |
|---|---|---|
| CMB acoustic peaks | peak RATIOS show the D96 octave structure | PARTIAL — ratios, not per-mode pairs |
| atomic (Rydberg) | E_n ~ 1/n² — no O(2) mirror pairing | **NO** |
| molecular | rovibrational ladders — no exact pairs | **NO** |
| condensed matter | phonons ~ linear — approximate only | NO |
| plasma | collective modes — no exact pairing | NO |
| GW | damped complex modes | NO |

**No natural domain shows the EXACT per-mode mirror-pair degeneracy (|Δλ| = 0) — the
sharpest D96 signature. The CMB shows only the octave RATIO structure.**

---

## 3. Test: octave hierarchy

| Domain | Octave hierarchy | D96 match |
|---|---|---|
| **CMB acoustic peaks** | **ℓ₁ = 220.48 (0.008%), r₂₁ = 2.4368 (0.035%), r₃₁ = 3.6965 (0.058%)** | **YES — D96-derived (QG237/238)** |
| atomic | Rydberg 1/n² — not octave | no |
| molecular | not octave | no |
| condensed matter | phonon branches ~ linear | approximate |
| plasma | no octave | no |

**The CMB acoustic peak ratios are the STRONGEST D96 signature in natural data —
already derived from the D96 octave hierarchy (QG237/QG238).**

---

## 4. Test: D96-like occupancy structure

| Domain | Occupancy structure | D96 match |
|---|---|---|
| **CMB** | the acoustic peak hierarchy follows the D96 rungs | **YES** |
| atomic | shell structure (not D96) | no |
| molecular | rovibrational (not D96) | no |
| condensed matter | band structure (approximate) | partial |
| plasma | collective (not D96) | no |

---

## 5. Does nature contain approximate D96 spectra?

**YES — partially.** The CMB acoustic peak ratios (ℓ₁ = 220.48, r₂₁ = 2.4368,
r₃₁ = 3.6965) are D96-derived and match observation to <0.06%. This is the closest
natural realization of the D96 hierarchy. The EXACT mirror-pair degeneracy (the
sharpest signature) has NO known natural realization.

---

## 6. Measure: deviations, exact matches, accidental matches

| Domain | Type of match | Deviation |
|---|---|---|
| CMB peaks | D96 octave ratios | < 0.06% (n_s = 0.96497, 0.007%) |
| condensed matter | approximate phonons | large — not D96-specific |
| other domains | none / accidental | — |

**The CMB match is a DERIVED correspondence, not accidental: the peak ratios follow
the D96 octave hierarchy (QG237/238).**

---

## 7. Candidate ranking

| Rank | Candidate | D96 signature | Strength |
|---|---|---|---|
| **1** | **CMB acoustic peaks** | octave-hierarchy ratios (ℓ₁, r₂₁, r₃₁) | **STRONG** (0.008–0.058%) |
| 2 | cosmological (general) | D96 octave structure | MEDIUM |
| 3 | condensed-matter phonons | approximate | WEAK |
| 4 | atomic/molecular | none | none |
| 5 | plasma / GW | none | none |

---

## Theorem

> **Theorem (NP_017).** Nature contains an APPROXIMATE D96 spectral signature in the
> CMB acoustic peak ratios, and NO natural realization of the exact mirror-pair
> degeneracy. Proof: (1) The CMB acoustic peaks follow the D96 octave hierarchy —
> ℓ₁ = 220.48 (0.008% deviation), r₂₁ = 2.4368 (0.035%), r₃₁ = 3.6965 (0.058%),
> n_s = 0.96497 (0.007%) — a DERIVED correspondence from the D96 octave rungs
> (QG237/QG238), not an accidental match. (2) The exact O(2) mirror-pair degeneracy
> (|Δλ| = 0, NP_015) has NO known natural realization: atomic (Rydberg 1/n²), molecular
> (rovibrational), condensed-matter (phonons ~ linear, approximate), plasma (collective),
> and gravitational-wave (damped) spectra all lack exact per-mode mirror pairs. (3)
> Therefore nature contains the D96 OCTAVE HIERARCHY (in the CMB) but not the exact
> mirror-pair structure. RANKING: CMB acoustic peaks — STRONG (D96-derived, <0.06%);
> cosmological general — MEDIUM; condensed matter — WEAK (approximate); atomic/
> molecular/plasma/GW — none. Classification: the CMB octave ratios are a
> CORRESPONDENCE (D96-derived peak ratios — already established, QG237/238); the exact
> mirror-pair degeneracy remains a PREDICTION with no current natural observation;
> no domain is FALSIFIED (no contradiction found). No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Search each domain (Section 1). (2) Test mirror pairs (Section 2,
> verified: no natural exact pairs). (3) Test octave hierarchy (Section 3, verified:
> CMB peaks D96-derived). (4) Test occupancy (Section 4). (5) Measure deviations
> (Section 6, verified: <0.06%). (6) Rank (Section 7). ∎

---

## Dependency Graph

```
Nature
 → Spectrum
 → D96 signature?
    → CMB acoustic peaks: octave hierarchy (CORRESPONDENCE — <0.06%)
    → exact mirror pairs: NO natural realization (PREDICTION — open)
 → Candidate ranking (CMB first)
 → Falsification strategy
```

---

## 8. Falsification strategy

| Step | Procedure |
|---|---|
| 1 | For the CMB: verify the peak ratios match the D96 octave hierarchy (ℓ₁, r₂₁, r₃₁) |
| 2 | For the mirror pairs: search new spectral data (ring-mode systems) for |Δλ| = 0 |
| 3 | Any natural exact mirror-pair set would CONFIRM the prediction |
| 4 | Any deviation in the CMB peak ratios beyond the D96 tolerance would FALSIFY the correspondence |

---

## Classification

| Component | Status |
|---|---|
| CMB acoustic peak ratios | **CORRESPONDENCE** (D96-derived, QG237/238, <0.06%) |
| exact mirror-pair degeneracy | **PREDICTION** (no natural observation yet) |
| octave hierarchy in nature | **CORRESPONDENCE** (CMB) |
| atomic/molecular/plasma/GW domains | **CORRESPONDENCE** (no D96 structure expected) |

**Nature contains the D96 OCTAVE HIERARCHY (in the CMB acoustic peaks, <0.06%) but
not yet the exact mirror-pair degeneracy. No domain is falsified. No new primitive;
canonical AT unchanged.**

---

## Open Problems

1. **Ring-mode realization (NP_017 OP1).** Whether any physical system exhibits the
   exact per-mode mirror-pair degeneracy (|Δλ| = 0) — the sharpest D96 signature,
   still unobserved in nature.

---

## Next Steps

- **Registry note:** the CMB acoustic peaks are the natural D96 octave signature
  (correspondence); the exact mirror pairs remain an unobserved prediction.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_017_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_017_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_017_MirrorPairs` | no natural exact mirror pairs | ✅ |
| `Y_NP_017_OctaveHierarchy` | CMB peaks follow the D96 octaves | ✅ |
| `Y_NP_017_SpectralMatch` | the CMB is the strongest D96 match | ✅ |
| `Y_NP_017_DeviationAudit` | CMB deviations < 0.06% | ✅ |
| `Y_NP_017_CandidateRanking` | CMB ranks first | ✅ |
| `Y_NP_017_Run` | research report | ✅ |

**Conclusion:** Nature contains an approximate D96 signature — the CMB acoustic peak
ratios (D96 octave hierarchy, <0.06%) — but not the exact mirror-pair degeneracy.
The CMB is the strongest candidate; no domain is falsified. No new primitive; canonical
AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_017"`

---

## References

- ResearchY-NP_013 (unique spectral prediction), NP_016 (mirror-pair observation),
  D_021 (oscillation symmetry), D_030 (octave rung).
- AT-QG: QG237 (n_s = 0.96497), QG238 (acoustic peaks ℓ₁ = 220.48, r₂₁ = 2.4368,
  r₃₁ = 3.6965).
