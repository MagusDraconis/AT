# ResearchY-NP_034 — Bose Without Blackbody Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_034 (permanent)
**Title:** Bose Without Blackbody Audit
**Status:** COMPLETE
**Date:** 2026-09-03
**File:** `NP_NewPhysics/ResearchY-NP_034.md`
**Depends on:** ResearchY-NP_027 (Planck factor form), NP_028 (blackbody FALSIFIED),
NP_030 (no canonical temperature), NP_031 (structure vs added occupancy layer),
NP_032 (no thermal N), NP_033 (ensemble Bose occupations EMERGENT, blackbody still
FALSIFIED), QG_194 (geometric occupation), D_008/D_030 (D96 spectrum),
S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_034_Tests.cs`

---

## Purpose

NP_033 established the paradox precisely: a D96 ensemble exchanging occupation DOES
produce the Bose occupation statistics n(ω) = 1/(e^(βω) − 1) — the occupation factor
that appears in Planck's law — yet the observed blackbody spectrum does NOT emerge
from the same ensemble. NP_034 is the surgical isolation audit: **which factor of the
observed radiance u(ω) = g(ω)·n(ω)·ω is the minimal obstruction?** The audit
factorizes u into its mode-set (density of states) and occupation parts, performs two
control experiments (replace the occupations; replace the mode set), quantifies the
sensitivity of the blackbody to each mode-set property, and seeks the smallest
deformation of D96 that restores the blackbody. No new primitives; canonical AT
unchanged.

---

## 1. Factorization u(ω) = g(ω)·n(ω)·ω

The observed spectral radiance separates into exactly three factors: the mode-set
g(ω) (density of states), the mean occupation n(ω), and the mode energy ω.

For the D96 ensemble the two factors are cleanly identified:

| Factor | D96 ensemble supplies | Status |
|---|---|---|
| g(ω) — mode set | 95 positive modes, band [0.622, 3.98], 44 distinct freqs ([4,4,87] octave occupancy, mirror pairs + 5-fold λ=12 + 6-fold λ=14 blocks) | structural (D_030, NP_031) |
| n(ω) — occupation | n_k = 1/(e^(βω_k) − 1) — the max-entropy occupation of the D96 mode set (NP_033) | EMERGENT (NP_033) |
| ω — mode energy | the mode frequency itself | structural |

**Factorization verified exactly:** Σ over the raw 95 modes of ω·n(ω) equals Σ over
the 44 distinct frequencies of m_i·ω_i·n(ω_i) (m_i = multiplicity), both
U(β = 1) = 12.588 (test `Y_NP_034_Factorization`). The Boltzmann identity
ln(n/(1+n)) = −βω holds exactly on every sampled D96 mode (NP_033 re-verified).

**This is the central observation:** the observable radiance is a PRODUCT. A correct
occupation factor is necessary but not sufficient — the observed curve is the product
of the occupation with the mode-set g(ω) and ω. Bose statistics can be present while
the product is not the blackbody.

---

## 2. Test 2 — replace the D96 occupations with exact Planck occupations

The D96 ensemble occupation n_k = 1/(e^(βω_k) − 1) **already IS** the exact
Planck/Bose occupation (NP_033 identity — the ensemble occupation was verified to
satisfy ln(n/(1+n)) = −βω exactly, i.e. it is precisely the Bose occupation). Hence:

- "Replacing the D96 occupations with exact Planck occupations" is the **identity
  operation** (verified pointwise to 12 decimals, test `Y_NP_034_OccupationReplacementIsNoop`).
- After the replacement, the blackbody **still fails**: the discrete
  Σ_k ω_k³/(e^(ω_k) − 1) over the D96 modes = **120.70**, still ≫ π⁴/15 = 6.494, and
  the D96 energy remains top-heavy (65.7% above ω = 3.3 at β = 1).

**Conclusion (Test 2):** the occupation is NOT the obstruction. The D96 ensemble
already gives the exact Planck occupation, and giving it "more exact" occupations
changes nothing. The failure must live in the mode-set factor g(ω).

---

## 3. Test 3 — replace the D96 mode set with the ideal ω² DOS

Keep the Bose occupation n(ω) = 1/(e^(βω) − 1) exactly as the D96 ensemble produces
it, but replace the mode set g(ω) with the ideal 3D-cavity density of states
g(ω) = ω². Then:

```
u(ω) = g(ω)·ω·n(ω) = ω²·ω·1/(e^(βω)−1) = ω³/(e^(βω)−1)
```

This is **exactly the Planck blackbody spectral law**. The test
(`Y_NP_034_IdealW2DOSRestoresBlackbody`) verifies all four classical limits of this
control spectrum:

| Limit | Ideal ω² DOS + Bose occupation | Value |
|---|---|---|
| Stefan-Boltzmann | ∫₀^∞ x³/(e^x−1) dx | **π⁴/15 = 6.4939** ✅ |
| Wien displacement | peak of x³/(e^x−1) | **x = 2.821** ✅ |
| Rayleigh-Jeans | u → x² as x → 0 | ✅ |
| Wien tail | u(x) → x³e^(−x) as x → ∞ | ✅ |

**Conclusion (Test 3):** with the SAME Bose occupation that the D96 ensemble already
produces, the ideal ω² DOS restores the blackbody completely. The Bose occupation is
sufficient — the mode-set g(ω) is the entire obstruction.

---

## 4. Test 4 — sensitivity analysis

The mode-set g(ω) enters through four properties. Their individual contributions are
measured:

| Mode-set property | D96 value | Blackbody needs | Contribution to the failure |
|---|---|---|---|
| **UV cutoff** | band caps at ω_max = 3.98 | support to ∞ (Wien tail) | 40.7% of the blackbody energy lies above ω_max at β = 1; 0 modes exist above ⇒ no exponential tail |
| **DOS exponent** | cumulative p ≈ 1.51 mid-band, ≈ 1.0 low-band | p = 3 (ω² DOS) | a p = 1.51 power host gives total ∫x^p/(e^x−1)dx = 1.79 vs π⁴/15 = 6.49 (>3× suppression); low-band p = 1 is a 1D chain (NP_032) |
| **mode clustering** | 44 distinct freqs; 8-bin counts [2,2,2,0,2,2,33,52] | smooth ω² continuum | lumpiness: an empty interior bin and a dense top cluster — the opposite of ω² |
| **finite frequency count** | 95 modes | count is NOT the issue | an ideal ω²-distributed 95-mode set over the same band reproduces the in-band blackbody integral to ~0.05% |

**UV cutoff (test `Y_NP_034_UvCutoffSensitivity`).** At β = 1 the continuous
blackbody has 40.7% of its total energy above ω_max = 3.98 (verified) and only ~0.97%
below ω_min = 0.622. The D96 band therefore cannot even host the full Planck curve
with the correct shape — and it has **no modes at all** above the cap, so the Wien
exponential tail is structurally impossible (NP_027/028 already FALSIFIED the tail).

**DOS exponent (test `Y_NP_034_DosExponentSensitivity`).** N(2.5)/N(1.0) = 4 over
D96 ⇒ p ≈ 1.51 on [1, 2.5]; the low-frequency octaves grow with p ≈ 1.0 (the 1D
linear dispersion of NP_032). The blackbody needs cumulative N ∝ ω³ (p = 3). A
power-law host with the D96 exponent would integrate to Γ(2.51)ζ(2.51) = 1.79, less
than a third of π⁴/15 = 6.49 — the exponent deficit alone suppresses the Stefan-
Boltzmann total by >3×.

**Mode clustering (test `Y_NP_034_ClusteringSensitivity`).** The D96 spectrum is 44
distinct frequencies (42 mirror pairs + one 5-fold block + one 6-fold block). Binned
into 8 equal cells over the band the counts are [2,2,2,0,2,2,33,52]: an empty
interior cell and a dense top pair — the reverse of a smooth ω² DOS that rises
gently. Clustering is a lumpiness obstruction on top of the exponent error.

**Finite count (test `Y_NP_034_FiniteCountSensitivity`).** 95 modes is ample. An ideal
ω²-distributed 95-mode set over the same D96 band reproduces the in-band blackbody
integral ∫ω³/(e^ω−1)dω to a relative error of ~0.05% (verified). The finite frequency
count is therefore NOT an obstruction — only the DISTRIBUTION of the modes is.

---

## 5. Test 5 — minimal deformation

What is the smallest change to D96 that restores ω² DOS, the Wien tail, and the
π⁴/15 integral?

| Deformation | Size | Restores |
|---|---|---|
| change the DOS exponent p: 1.0–1.51 → 3 | the exponent gap is > 1 (large) | ω² DOS, and the in-band blackbody shape |
| redistribute the 95 modes ω³-uniformly over the band (same count) | ≤ the mode rearrangement to ω²; in-band error → < 1% | in-band blackbody integral |
| remove the hard UV cap (unbind the band) | adds the missing 40.7% above ω_max | Wien tail, full π⁴/15 |

Crucially, **the occupation is NOT part of the deformation**: it is already the exact
Bose occupation, so the blackbody is restored purely by changing the mode set g(ω)
(test `Y_NP_034_MinimalDeformation`). The ideal ω²-distributed 95-mode set over the
same D96 band reproduces the in-band blackbody integral to < 1% with the SAME
occupation, and unbinding the band recovers the 40.7% missing above the cap. The
minimal obstruction is thus: **the D96 mode-set g(ω)** — its sub-power DOS exponent,
its top-heavy clustering, and its hard UV cap.

---

## Theorem

> **Theorem (NP_034).** The D96 ensemble produces exact Bose occupation statistics
> (NP_033), and that occupation is SUFFICIENT for the blackbody: the observed
> blackbody is u(ω) = g(ω)·ω·n(ω) with n the Bose occupation and g the ω² 3D-cavity
> DOS; the D96 failure is entirely in the mode-set factor g(ω). Proof: (1)
> Factorization (Section 1, verified): u = g·n·ω, U(1) = 12.588, the Boltzmann
> identity holds exactly over the D96 modes. (2) Occupation replacement no-op
> (Section 2, verified): the D96 ensemble occupation already equals the exact Planck
> occupation pointwise; replacing it changes nothing, and Σω³/(e^ω−1) = 120.70 ≠ π⁴/15
> persists ⇒ occupation is NOT the obstruction. (3) Ideal-DOS control (Section 3,
> verified): with the SAME Bose occupation over the ideal ω² DOS, u = ω³/(e^(βω)−1),
> giving Stefan-Boltzmann π⁴/15 = 6.4939, Wien displacement x = 2.821,
> Rayleigh-Jeans x², and the Wien tail ⇒ the mode set is the obstruction and Bose is
> sufficient. (4) Sensitivity (Section 4, verified): the failure decomposes into the
> UV cap (40.7% of blackbody energy above ω_max at β = 1), the DOS exponent (p ≈ 1.0–
> 1.51 vs 3; a p = 1.51 host integrates to 1.79 < π⁴/15/3), mode clustering
> ([2,2,2,0,2,2,33,52] 8-bin counts, 44 distinct freqs), while finite count is NOT an
> obstruction (an ideal ω² 95-mode set reproduces in-band blackbody to ~0.05%).
> (5) Minimal deformation (Section 5): keeping the occupation fixed, only the mode set
> must change — exponent to 3, modes ω³-redistributed, band unbound — to restore the
> ω² DOS, the Wien tail, and π⁴/15. Classification: Bose occupation from the D96
> ensemble EMERGENT (unchanged, NP_033); the occupation as the blackbody's occupation
> factor SUFFICIENT (Test 3, answer A); D96 mode-set as blackbody host FALSIFIED
> (unchanged, NP_028/033); the UV cap, DOS exponent, and clustering of the D96 mode
> set are the minimal obstruction DERIVED; finite mode count NOT an obstruction
> DERIVED; temperature scale BOUNDARY (unchanged). No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Factorize u. (2) Show occupation replacement is identity.
> (3) Show Bose + ω² DOS = blackbody. (4) Measure the sensitivity. (5) Deform the mode
> set minimally. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The D96 occupation is not exactly Planck, so blackbody fails" | the D96 ensemble occupation IS exactly Bose/Planck (NP_033 identity, re-verified pointwise); replacement is a no-op and blackbody still fails |
| "Bose statistics is insufficient for the blackbody" | with the same Bose occupation over the ideal ω² DOS the blackbody is reproduced exactly (π⁴/15, x = 2.821, RJ, Wien) — Test 3 |
| "The finite 95-mode count is the obstruction" | an ideal ω²-distributed 95-mode set over the same band reproduces the in-band blackbody to ~0.05% — count is ample, only the distribution is wrong |
| "The D96 UV cap is the only obstruction" | removing the cap alone still leaves the DOS exponent (~1–1.5 vs 3) and clustering wrong; the exponent deficit alone suppresses the total by >3× |
| "The DOS exponent alone explains the failure" | the band also ends at ω_max = 3.98 (no Wien tail) and the modes are lumpy ([2,2,2,0,2,2,33,52]) — all three mode-set properties contribute |
| "A new occupation layer is needed for blackbody physics" | the occupation is already the exact Bose occupation (NP_033 EMERGENT); the blackbody needs only a hosted ω² DOS (3D-cavity geometry) over an unbounded band |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| the occupation is not the obstruction | a D96 occupation that is NOT already the exact Bose occupation, or a replacement that changes the radiance |
| Bose occupation is sufficient given ω² DOS | Bose occupation + ideal ω² DOS failing to reproduce π⁴/15, x = 2.821, RJ, or the Wien tail |
| the mode set is the minimal obstruction | a D96-derived mode set with p = 3 and no UV cap still failing to host the blackbody |
| finite count is not an obstruction | an ideal ω²-distributed 95-mode set failing to reproduce the in-band blackbody to < 1% |
| UV cap + exponent + clustering are the obstruction components | removing any single one restoring the full blackbody shape |

---

## Classification

| Component | Status |
|---|---|
| Bose occupation n(ω) = 1/(e^(βω) − 1) from the D96 ensemble | **EMERGENT** (unchanged, NP_033) |
| the occupation as the blackbody occupation factor | **SUFFICIENT** (Test 3: Bose + ω² DOS reproduces the blackbody exactly) — answer A |
| D96 mode-set as blackbody host | **FALSIFIED** (unchanged, NP_028/033) |
| UV cap, DOS exponent (p ≈ 1.0–1.51 vs 3), clustering of the D96 mode set | **DERIVED — the minimal obstruction** (the mode-set factor g, not the occupation) |
| finite mode count (95) | **NOT an obstruction** (ideal ω² 95-mode set → in-band error ~0.05%) |
| additional (occupation-level) obstruction | **NONE** (occupation replacement is a no-op) — answer B refuted |
| new primitive / layer for blackbody | **NOT REQUIRED** (hosted ω² DOS suffices) — answer C refuted |
| temperature scale β | **BOUNDARY** (unchanged, NP_027/028/030) |

**Conclusion: Bose statistics is sufficient — the D96 failure is entirely the mode
set.** The D96 ensemble already produces the exact Planck occupation (NP_033); giving
it exact Planck occupations changes nothing (Test 2), while replacing only the mode
set with the ideal ω² DOS restores the blackbody completely with the same occupation
(Test 3). The minimal obstruction is the D96 mode-set factor g(ω): a sub-power DOS
(exponent ≈ 1.0–1.51 vs the required 3), a hard UV cap (40.7% of blackbody energy
above ω_max at β = 1, no Wien tail), and top-heavy clustering ([2,2,2,0,2,2,33,52],
44 distinct frequencies). The finite 95-mode count is not the problem. Answer A of the
success criterion is CONFIRMED: Bose is sufficient, only the D96 DOS fails; there is
no additional obstruction (B), and no new primitive or physics layer is needed (C).
No new primitive; canonical AT unchanged.

---

## 8. Closure score and dependency DAG

**Closure score: 8/10.** The audit isolates the obstruction to a single factor (the
mode set), proves the occupation is sufficient by direct control, and quantifies each
mode-set contribution — but the ω² DOS itself remains hosted (3D-cavity geometry,
NP_031/032), and the Wien tail requires an unbounded band that no D_N supplies
(NP_032).

```
Dependency DAG:
Difference → Actualization → Spectrum (D96, 95 modes)
 → mode-set g(ω)                       [DERIVED structural]
 → ensemble occupation n = 1/(e^{βω}−1) [EMERGENT — NP_033]
   → u(ω) = g(ω)·ω·n(ω)                [DERIVED — factorization]
   → Test 2: occupation replacement no-op ⇒ occupation NOT the obstruction
   → Test 3: g = ω² ⇒ u = ω³/(e^{βω}−1) ⇒ blackbody (π⁴/15, 2.821, RJ, Wien)
   → minimal obstruction = D96 mode set (p ≈ 1–1.5, cap 3.98, clustering)
   → temperature scale β                 [BOUNDARY]
```

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_034_Tests.cs`
**Run:** 2026-09-03 · **Result:** see `Tests/Results/Y_NP_034_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_034_Factorization` | u = g·n·ω; U(1) = 12.588; Boltzmann identity over D96 | ✅ |
| `Y_NP_034_OccupationReplacementIsNoop` | exact-Planck replacement = identity; blackbody still fails | ✅ |
| `Y_NP_034_IdealW2DOSRestoresBlackbody` | Bose + ω² DOS → π⁴/15, x = 2.821, RJ, Wien | ✅ |
| `Y_NP_034_UvCutoffSensitivity` | 40.7% of blackbody above ω_max; no modes above cap | ✅ |
| `Y_NP_034_DosExponentSensitivity` | p ≈ 1.0–1.51 vs 3; exponent deficit >3× total suppression | ✅ |
| `Y_NP_034_ClusteringSensitivity` | 44 distinct; 8-bin [2,2,2,0,2,2,33,52] | ✅ |
| `Y_NP_034_FiniteCountSensitivity` | ideal 95-mode ω² set → in-band error ~0.05% | ✅ |
| `Y_NP_034_MinimalDeformation` | mode set only: p → 3, redistribute, unbind | ✅ |
| `Y_NP_034_Classification` | A CONFIRMED / B none / C no new primitive | ✅ |
| `Y_NP_034_Run` | research report | ✅ |

**Conclusion:** Bose statistics is sufficient and only the D96 DOS fails. The D96
ensemble produces the exact Planck occupation (NP_033, EMERGENT), and that occupation
multiplied by the ideal ω² DOS IS the blackbody. The minimal obstruction to the
observed blackbody is the D96 mode set — its sub-power DOS exponent, hard UV cap, and
clustering — not the occupation, not the mode count. No new primitive; canonical AT
unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_034"`

---

## References

- ResearchY-NP_027 (Planck factor form EMERGENT / temperature BOUNDARY / full law not
  reproduced), NP_028 (blackbody FALSIFIED for D96; occupation factor CORRESPONDENCE),
  NP_030 (no canonical temperature), NP_031 (structure sector DERIVED; thermo an added
  occupancy layer), NP_032 (no thermal N; 1D DOS at every ring size), NP_033 (ensemble
  Bose occupations EMERGENT; blackbody FALSIFIED), QG_194 (geometric occupation),
  D_008/D_030 (D96 spectrum; occupancy [4,4,87]), S_001 (synthesis).
