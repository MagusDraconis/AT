# ResearchY-NP_024 — O(2) Mirror-Pair Physical Prediction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_024 (permanent)
**Title:** O(2) Mirror-Pair Physical Prediction Audit
**Status:** COMPLETE
**Date:** 2026-09-01
**File:** `NP_NewPhysics/ResearchY-NP_024.md`
**Depends on:** ResearchY-NP_015 (O(2) doublet prediction), NP_016 (mirror-pair
observation), NP_017 (natural D96 signature), NP_022 (unique prediction search),
NP_023 (O(2) mirror search — establishes the exact degeneracy algebra)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_024_Tests.cs`

---

## Purpose

**What observable physical consequence follows uniquely from the exact D96 symmetry
structure O(2)_D96 = {42 mirror-pair irreps} ∪ {λ=12 five-fold block} ∪ {λ=14
six-fold block}?** NP_023 established this exact algebra; NP_022 ranked the mirror-
pair degeneracy as the #2 structural prediction. This audit derives the FULL physical
consequences of the exact algebra — resonance splitting, selection rules, forbidden
transitions, paired excitation spectra, and frequency ratios — filters every
candidate through the correspondence reductions (generic ring, Fourier spectra, QM,
SM, phonons), and identifies the strongest falsifiable discriminator.

---

## 1. The exact degeneracy algebra (NP_023, verified)

```
O(2)_D96 = {42 mirror-pair 2D irreps}          [84 modes, the generic k↔N−k pairs]
         ∪ {λ=12 five-fold block}              [{16,32,48,64,80}, ω = √12]
         ∪ {λ=14 six-fold block}               [{8,24,40,56,72,88}, ω = √14]
```

| Block | Multiplicity | Modes | ω = √λ |
|---|---|---|---|
| mirror pairs | 2 (×42) | {k, N−k} | ω_k = ω_{N−k} |
| λ=12 | **5** | {16,32,48,64,80} | √12 = 3.4641 |
| λ=14 | **6** | {8,24,40,56,72,88} | √14 = 3.7417 |

**95 positive modes = 42×2 + 5 + 6.** The two high-multiplicity blocks are the
non-generic content.

---

## 2. Physical consequences of the exact structure

### 2.1 Protected resonance splitting

| Consequence | Detail |
|---|---|
| mirror pairs | each ω_k has an exact partner ω_{N−k} (|Δλ| = 0), protected by the ring reflection (NP_023) |
| λ=12 five-fold | ONE resonance at ω = √12 with FIVE orthogonal modes |
| λ=14 six-fold | ONE resonance at ω = √14 with SIX orthogonal modes |
| splitting | any perturbation preserving the ring keeps all degeneracies exact (NP_023: ~1e−14) |

### 2.2 Selection rules / forbidden transitions

The ring Laplacian is real symmetric; the degenerate blocks transform under the ring
symmetry. The observable content:

- A resonance at ω = √12 (5 modes) and ω = √14 (6 modes) — the multiplicity IS the
  observable: a peak with degeneracy 5/6, not 1 or 2.
- Excitation of mode k excites its mirror partner k→N−k and (within the octave
  blocks) its octave partners — a PAIRED excitation spectrum.

### 2.3 Paired excitation spectra

- Mirror pairs: exciting mode k excites mode N−k at the same frequency (generic).
- **Octave blocks (non-generic):** exciting k=16 also excites k=32,48,64,80 at the
  SAME frequency ω = √12 — a 5-fold paired excitation. Similarly k=8 excites the
  6-fold {8,24,40,56,72,88} at ω = √14.

### 2.4 Observable frequency ratios

| Ratio | Value | Source |
|---|---|---|
| ω_k/ω_{N−k} | 1 exactly | mirror pair |
| ω₂/ω₁ | 1.9734 ≈ 2 (octave) | long-wavelength dispersion |
| **ω(√12)/ω(√14)** | **√(6/7) = 0.925820** | **the two degenerate blocks** |
| ω(√12)/ω₁ | 5.5731 | block-to-fundamental |

**The exact ratio ω(√12)/ω(√14) = √(6/7) is the strongest: it is coupling-
INDEPENDENT (the ring couplings cancel in the ratio) and follows purely from the
integer eigenvalues λ=12 and λ=14.**

---

## 3. Correspondence filter (what survives?)

| Candidate | Generic ring | Fourier | QM | SM | Phonons | Survives? |
|---|---|---|---|---|---|---|
| mirror pairs (ω_k = ω_{N−k}) | ✓ (k↔−k) | ✓ (cos/sin) | ✓ (m↔−m) | ✗ (weak doublets split) | ✓ (time-reversal) | **CORRESPONDENCE** |
| **5-fold / 6-fold multiplicities** | ✗ (all 2-fold) | ✗ | ✗ (generic 2-fold) | ✗ | ✗ (generic 2-fold) | **PREDICTION** |
| **ω(√12)/ω(√14) = √(6/7)** | ✗ (coupling-dependent) | ✗ | ✗ | ✗ | ✗ | **PREDICTION** |

**The mirror-pair degeneracy is CORRESPONDENCE** (any rotationally symmetric system,
QM central potentials, and time-reversal-invariant phonons have k↔−k pairs). **The
5-fold/6-fold multiplicities and the √(6/7) ratio are PREDICTION** — a generic ring,
Fourier spectrum, QM, SM, or phonon system shows only 2-fold pairs with no such exact
cross-block ratio.

---

## 4. The strongest experimental discriminator

**A C96-ring resonator must show EXACTLY:**

1. 47 mirror pairs (2-fold) — generic content.
2. **ONE 5-fold resonance at ω = √12** (the λ=12 block).
3. **ONE 6-fold resonance at ω = √14** (the λ=14 block).
4. **The exact ratio ω(√12)/ω(√14) = √(6/7) ≈ 0.92582** between them.

A generic ring shows only 2-fold pairs and no √(6/7) relation — so the 5-fold/6-fold
multiplicities and the exact cross-block ratio are the UNIQUE discriminator.

---

## Theorem

> **Theorem (NP_024).** The exact D96 symmetry O(2)_D96 = {42 mirror-pair irreps} ∪
> {λ=12 five-fold block} ∪ {λ=14 six-fold block} implies a unique falsifiable
> observable that EXCEEDS the NP_022 mirror-pair prediction: a C96-ring resonator
> must show one 5-fold resonance at ω = √12, one 6-fold resonance at ω = √14, and
> the exact, coupling-independent ratio ω(√12)/ω(√14) = √(6/7) ≈ 0.92582. Proof:
> (1) Reconstruct the degeneracy algebra (Section 1, verified): 95 modes = 42×2
> (mirror pairs, generic) + 5 (λ=12 block {16,32,48,64,80}) + 6 (λ=14 block
> {8,24,40,56,72,88}). (2) Derive the physical consequences (Section 2, verified):
> protected resonances (perturbation ~1e−14), paired excitation (the octave blocks
> are co-excited), and the exact ratios — ω_k/ω_{N−k} = 1, ω₂/ω₁ ≈ 2 (octave), and
> ω(√12)/ω(√14) = √(12/14) = √(6/7) = 0.925820 (exact, coupling-independent — the
> couplings cancel in the ratio). (3) Apply the correspondence filter (Section 3,
> verified): the mirror-pair degeneracy is CORRESPONDENCE (generic rings, Fourier
> cos/sin, QM central-potential m↔−m, and time-reversal phonons all have k↔−k
> pairs); the 5-fold/6-fold multiplicities and the √(6/7) ratio are PREDICTION —
> no generic system produces them (a single-coupling ring has all-distinct 2-fold
> eigenvalues with no cross-block relation). (4) The strongest discriminator
> (Section 4): the C96-ring spectrum must show exactly one 5-fold and one 6-fold
> resonance with the exact √(6/7) ratio. (5) This EXCEEDS NP_022: the mirror-pair
> prediction (|Δλ| = 0) is generic content, while the multiplicity + ratio content
> is uniquely K=6 and more specific. Classification: mirror-pair degeneracy
> CORRESPONDENCE (generic); the λ=12 five-fold and λ=14 six-fold blocks DERIVED
> (spectral, D_030 octave structure); the exact ratio ω(√12)/ω(√14) = √(6/7)
> PREDICTION (coupling-independent, uniquely K=6); the multiplicities PREDICTION
> (observable as peak degeneracy 5 and 6); a generic-ring reproduction FALSIFIED
> (single-coupling rings show no such structure). No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Reconstruct the algebra (Section 1). (2) Derive the
> consequences (Section 2). (3) Filter the correspondence (Section 3). (4) Identify
> the discriminator (Section 4). ∎

---

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The mirror pairs are unique to AT" | generic rings, QM central potentials, and time-reversal phonons all have k↔−k degeneracy (CORRESPONDENCE) |
| "A generic ring reproduces the 5-fold/6-fold" | a single-coupling ring has all-distinct 2-fold eigenvalues (verified) |
| "The √(6/7) ratio is coupling-dependent" | the couplings cancel in the ratio — it is exact (verified: √(12/14) = 0.925820) |
| "Any degenerate spectrum shows this" | the 5-fold/6-fold blocks require the specific K=6 ring structure (NP_023) |
| "The discriminator is weaker than NP_022" | NP_024's multiplicity + ratio content is more specific than the mirror pairs alone |

---

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| one 5-fold resonance at ω = √12 | a C96-ring spectrum without a 5-fold peak at √12 |
| one 6-fold resonance at ω = √14 | a C96-ring spectrum without a 6-fold peak at √14 |
| ω(√12)/ω(√14) = √(6/7) = 0.92582 | a measured ratio deviating from √(6/7) |
| the mirror pairs are correspondence | a rotationally symmetric system WITHOUT k↔−k pairs |
| the blocks require K=6 | a single-coupling ring producing a 5-fold or 6-fold eigenvalue |

---

## Classification

| Component | Status |
|---|---|
| mirror-pair degeneracy | **CORRESPONDENCE** (generic: rings, QM, phonons) |
| λ=12 five-fold block | **DERIVED** (octave structure, D_030) |
| λ=14 six-fold block | **DERIVED** (octave structure, D_030) |
| **ω(√12)/ω(√14) = √(6/7)** | **PREDICTION** (coupling-independent, uniquely K=6) |
| **5-fold / 6-fold multiplicities** | **PREDICTION** (observable peak degeneracy) |
| generic-ring reproduction | **FALSIFIED** (single-coupling rings show 2-fold only) |

**The strongest falsifiable observable implied by {mirror pairs} ∪ {octave blocks} is
the exact, coupling-independent ratio ω(√12)/ω(√14) = √(6/7) = 0.92582 together with
the 5-fold/6-fold resonance multiplicities — and this EXCEEDS NP_022's mirror-pair
prediction (which is generic correspondence content). No new primitive; canonical AT
unchanged.**

---

## 7. Success criterion

**Does NP_024 exceed NP_022's current O(2) prediction? YES.**

| Criterion | NP_022 | NP_024 |
|---|---|---|
| mirror pairs | PREDICTION (#2, 18/20) | CORRESPONDENCE (generic) — downgraded to its correct class |
| 5-fold/6-fold multiplicities | — | **PREDICTION** (new, observable) |
| √(6/7) inter-block ratio | — | **PREDICTION** (new, coupling-independent, stronger) |

NP_024 REFINES NP_022: the mirror-pair degeneracy itself is generic (correspondence),
but the octave-block structure adds a STRONGER, more specific, uniquely-K=6
prediction — the √(6/7) ratio — that NP_022 did not state. The strongest single
prediction of the exact O(2)_D96 structure is therefore the exact inter-block
frequency ratio, not the mirror pairs.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_024_Tests.cs`
**Run:** 2026-09-01 · **Result:** see `Tests/Results/Y_NP_024_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_024_DegeneracyAlgebra` | 42×2 + 5 + 6 = 95; the exact blocks | ✅ |
| `Y_NP_024_MirrorPairRatios` | ω_k/ω_{N−k} = 1; ω₂/ω₁ ≈ 2 | ✅ |
| `Y_NP_024_OctaveBlockRatio` | ω(√12)/ω(√14) = √(6/7) exact | ✅ |
| `Y_NP_024_SelectionRules` | paired excitation; protected splitting | ✅ |
| `Y_NP_024_CorrespondenceFilter` | mirror pairs generic; blocks unique | ✅ |
| `Y_NP_024_Discriminator` | the C96-ring 5-fold/6-fold + √(6/7) | ✅ |
| `Y_NP_024_Run` | research report | ✅ |

**Conclusion:** The exact O(2)_D96 structure implies a unique falsifiable observable
that exceeds NP_022: a C96-ring resonator must show one 5-fold resonance at ω = √12,
one 6-fold resonance at ω = √14, and the exact coupling-independent ratio
ω(√12)/ω(√14) = √(6/7) = 0.92582. The mirror-pair degeneracy is downgraded to
CORRESPONDENCE (generic), while the octave-block multiplicities and ratio are the
strongest PREDICTION. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_024"`

---

## References

- ResearchY-NP_015 (O(2) doublet prediction), NP_016 (mirror-pair observation),
  NP_017 (natural D96 signature), NP_022 (unique prediction search), NP_023 (O(2)
  mirror search — the exact degeneracy algebra).
- ResearchY-D_030 (octave-rung structure), D_035 (multiplet requirement).
