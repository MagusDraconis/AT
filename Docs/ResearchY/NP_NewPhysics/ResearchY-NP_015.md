# ResearchY-NP_015 — O(2) Doublet Prediction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_015 (permanent)
**Title:** O(2) Doublet Prediction Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_015.md`
**Depends on:** ResearchY-NP_013 (unique spectral prediction), D_021 (oscillation
symmetry), D_022 (weak-isospin entry), D_023 (SU(2) entry), D_024 (doublet
compatibility)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_015_Tests.cs`

---

## Purpose

**What observable consequences follow from the exact O(2) doublet degeneracy?** NP_013
ranked the O(2) exact doublet degeneracy (λ_k = λ_{N−k} for every mode k) as the
strongest D96-specific prediction. This audit turns it into testable physics: it
analyzes exact/broken/approximate doublets, finds observable signatures, determines
what would falsify the degeneracy, and compares against QM/SM/GR.

---

## 1. Doublet analysis

| Type | Definition | λ_k = λ_{N−k}? |
|---|---|---|
| **exact doublet** | the D96 ring forces identical eigenvalues | **YES — |Δλ| = 0 exactly** (verified: k=1,2,16,47 all give 0.00e+00) |
| **broken doublet** | a perturbation lifts the degeneracy | NO — the members split |
| **approximate doublet** | a near-degeneracy from unrelated origins | only approximately |

**The canonical claim is EXACT: λ_k = λ_{N−k} with |Δλ| = 0 to machine precision
(for every k = 1..95, k ≠ 48).**

---

## 2. Observable consequences

| Consequence | Signature |
|---|---|
| **identical frequencies** | ω_k = ω_{N−k} exactly — mode k and its mirror twin oscillate at the same rate |
| **frequency ratio 1** | ω_k/ω_{N−k} = 1 exactly |
| **doublet structure** | every non-central mode has a degenerate partner (47 mirror pairs + k=48 central) |
| **spectral fingerprint** | a measured spectrum must exhibit the exact mirror-pairing pattern |

**The observable signature: a spectrum whose every mode k has an exact mirror partner
N−k at identical frequency.**

---

## 3. What would falsify the degeneracy

| Observation | Verdict |
|---|---|
| a mode k with NO mirror partner at the same frequency | **FALSIFIES** the O(2) degeneracy |
| any |Δλ| > 0 between a claimed pair | **FALSIFIES** exactness (would leave only approximate) |
| a triplet structure with no pairing | FALSIFIES (contradicts O(2), would be SU(3)-type) |

**The falsification is precise and structural: any measured spectrum lacking the exact
mirror-pair degeneracy falsifies the prediction.**

---

## 4. Comparison with QM / SM / GR

| Framework | Doublet structure |
|---|---|
| **QM** | NO fixed spectrum — degeneracies are Hamiltonian-dependent, often accidental |
| **SM** | weak-isospin doublets (u,d), (c,s), (t,b) are NOT degenerate in mass — only approximate (the SM doublets are gauge pairs, not degeneracies) |
| **GR** | NO frequencies at all |
| **AT** | **EXACT spectral degeneracy** λ_k = λ_{N−k} — structural, forced by the C96 ring |

**AT's claim is DISTINCT from SM's:** the SM weak doublets are non-degenerate mass
pairs; the AT O(2) doublets are EXACT spectral degeneracies. QM does not force any
degeneracy; AT forces this one.

---

## 5. Top observable signatures

1. **Exact mirror-pair frequencies** — the sharpest signature: mode k and N−k must
   oscillate at identical frequency (ω_k/ω_{N−k} = 1 exactly).
2. **47+1 doublet count** — the spectrum has 47 mirror pairs plus one central mode
   (k=48): a specific, countable structure.
3. **The frequency ratio fingerprint** — the full ratio set
   {ω_k/ω_{N−k} = 1} is an O(2)-specific signature.
4. **The O(2) reflection symmetry** — the spectrum is symmetric under k → N−k.

---

## Theorem

> **Theorem (NP_015).** The O(2) exact doublet degeneracy (λ_k = λ_{N−k} for every
> mode k, D_021) predicts an OBSERVABLE spectral signature: every non-central mode has
> an exact mirror partner at identical frequency (ω_k = ω_{N−k}, ω_k/ω_{N−k} = 1
> exactly), giving 47 mirror pairs plus the central mode k=48 (verified: |Δλ| = 0 to
> machine precision for k=1,2,16,47). The degeneracy is EXACT, not approximate:
> any |Δλ| > 0 between a claimed pair falsifies the prediction, as does a mode with no
> mirror partner or a triplet structure (which would indicate SU(3)-type, not O(2)).
> The prediction is DISTINCT from QM (which fixes no spectrum — degeneracies are
> Hamiltonian-dependent), SM (whose weak-isospin doublets (u,d),(c,s),(t,b) are
> NON-degenerate gauge pairs, not degeneracies), and GR (which has no frequencies).
> TOP SIGNATURES: (1) exact mirror-pair frequencies (ω_k/ω_{N−k}=1); (2) the 47+1
> doublet count; (3) the O(2) reflection symmetry k → N−k. Classification: the
> degeneracy is a PREDICTION (uniquely D96, structural); its SM analogue (weak
> doublets) is CORRESPONDENCE only approximately (they are gauge pairs, not
> degeneracies); the reflection symmetry is DERIVED (algebra of cos). No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Verify exactness (Section 1, verified: |Δλ|=0 to 1e-16). (2)
> Derive the observable signatures (Section 2). (3) Specify the falsification
> (Section 3). (4) Compare with QM/SM/GR (Section 4). (5) List the top signatures
> (Section 5). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Spectrum (N=96, C96 ring)
 → O(2) exact doublet degeneracy (λ_k = λ_{N−k}) — D_021
 → Observable signature
    → exact mirror-pair frequencies (ω_k/ω_{N−k} = 1)
    → 47+1 doublet count
    → O(2) reflection symmetry
 → Experiment
    → any broken/missing pair falsifies
```

---

## 6. Falsification paths

| Prediction | Falsification |
|---|---|
| exact mirror-pair frequencies | any measured ω_k ≠ ω_{N−k} (|Δλ| > 0) |
| 47+1 doublet count | a count different from 47 pairs + central mode |
| O(2) reflection symmetry | a spectrum not symmetric under k → N−k |

---

## 7. Prediction registry entries

| ID | Prediction | Classification |
|---|---|---|
| (extends D_046 P1) | O(2) exact spectral degeneracy λ_k = λ_{N−k} | **PREDICTION** |
| | ω_k/ω_{N−k} = 1 exactly | **PREDICTION** |
| | 47 mirror pairs + central mode k=48 | **PREDICTION** (structural count) |

---

## Classification

| Component | Status |
|---|---|
| O(2) exact doublet degeneracy | **PREDICTION** (uniquely D96, structural) |
| SM weak doublets (u,d),(c,s),(t,b) | **CORRESPONDENCE** only approximately — gauge pairs, NOT degeneracies |
| O(2) reflection symmetry | **DERIVED** (algebra of cos, D_021) |
| central mode k=48 | **DERIVED** (self-pairing at the ring midpoint) |

**The O(2) exact doublet degeneracy is the strongest testable D96 prediction: an
exact mirror-pair spectrum, distinct from QM's accidental degeneracies and SM's
non-degenerate gauge doublets. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Mapping to physical sectors (NP_015 OP1).** Whether the spectral doublets
   correspond to physical degenerate doublets in an observable sector (the SM weak
   doublets are gauge pairs, not spectral degeneracies — the mapping is open).

---

## Next Steps

- **Registry note:** add the O(2) doublet signatures (mirror-pair frequencies, 47+1
  count, reflection symmetry) to the prediction registry.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_015_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_015_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_015_ExactDoublets` | λ_k = λ_{N−k} exactly (|Δλ| = 0) | ✅ |
| `Y_NP_015_BrokenDoublets` | a perturbation would falsify exactness | ✅ |
| `Y_NP_015_ObservableSignature` | mirror-pair frequencies (ω_k/ω_{N−k} = 1) | ✅ |
| `Y_NP_015_PredictionRanking` | top observable signatures | ✅ |
| `Y_NP_015_Run` | research report | ✅ |

**Conclusion:** The O(2) exact doublet degeneracy predicts observable mirror-pair
frequencies (ω_k/ω_{N−k} = 1 exactly), a 47+1 doublet count, and O(2) reflection
symmetry — distinct from QM/SM/GR. Any broken or missing pair falsifies it. No new
primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_015"`

---

## References

- ResearchY-NP_013 (strongest D96 prediction), D_021 (oscillation symmetry λ_k =
  λ_{N−k}), D_022 (weak-isospin entry), D_023 (SU(2) entry), D_024 (doublet
  compatibility), D_046 (predictions P1–P8).
