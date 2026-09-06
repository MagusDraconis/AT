# ResearchY-NP_077 — Structure Formation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_077 (permanent)
**Title:** Structure Formation Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_077.md`
**Depends on:** AT-QG QG194/195 (matter = deficit, dust T_μν), QG206 (flat rotation α=0),
QG227/228 (uniform critical state, Poisson fluctuations), QG231 (structure formation origin),
QG237/238 (CMB spectrum: n_s derived, acoustic peaks partial), QG104/105 (hierarchical network
spectrum), QG116b (universal attractor), G4-ME Phase 1/3 (deficit gravity, astrophysical
profiles), G4-RHO Phase 0 (dynamical origin of ρ), `Audits/ClusterMassAudit.md` (Coma),
ResearchY-NP_065/066 (dark matter ontology/evidence), NP_070 (criticality μ=1)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_077_Tests.cs`

---

## Purpose

NP_065/066 established Dark Matter = the deficit m = ρ̄ − ρ, and NP_070 that critical branching
μ = 1 is derived (conditional on scale-freeness). NP_077 asks whether that deficit field can
**naturally** produce large-scale structure — halos, galaxy profiles, cluster structure —
*without* particle dark matter. Program: (1) start from m = ρ̄ − ρ; (2) evolve under critical
branching μ = 1; (3) test clustering; (4) measure density profiles; (5) compare with NFW, cored
halos, and galaxy rotation data. **Success criterion:** determine whether the deficit A) naturally
forms halos, B) forms structure only with extra assumptions, or C) fails structure formation. No
new primitives; canonical AT unchanged.

---

## 1. The derived part — seed and linear growth

Starting from m = ρ̄ − ρ under critical branching μ = 1, the following are **DERIVED** (QG231):

| Step | Result | Status |
|---|---|---|
| Poisson seed | δ_i = 1/√⟨N⟩ (Q-event counting noise) | **DERIVED** (QG228) |
| scale-free variance | Var(2k)/Var(k) = 2 at criticality | **DERIVED** (QG227/228) |
| pressureless dust | T_μν = ρ_m v_μ v_ν self-gravitating | **DERIVED** (QG195/196) |
| linear growth | δ(a) = δ_i · a/a_i (δ ∝ a) | **DERIVED** (QG231) |
| scalar tilt | n_s = 0.96497 (0.007% of Planck) | **DERIVED** (QG237) |

The **seed and growth** are derived: the initial field is uniform-critical + Poisson noise, and
over-densities amplify linearly as the count density grows. This is the honest derived core of
structure formation.

---

## 2. The non-derived part — the profile shape

The growth law tells us over-densities amplify; it does **not** tell us what profile a halo
settles into. That requires the **abundance law** — how the deficit is distributed in radius —
and here the dynamical origin is open (G4-RHO Phase 0):

| Radial ansatz | Result | Source |
|---|---|---|
| power-law deficit m ∝ 1/r | Keplerian v² ∝ 1/r (point mass) | G4-ME30 |
| **log deficit m ∝ ln(Rmax/r)** (α=0) | **flat rotation v² ≈ const, M_eff ∝ r** | G4-ME31 |
| arbitrary scale-free ρ ∝ r^s | continuum of flat profiles, sign by s | G4-RHO00 |

The flat rotation curve (the dark-matter halo signature) requires the **α = 0 log-deficit
abundance law** — the unique scale-invariant form. But G4-RHO Phase 0 is explicit: this is a
**symmetry selection (PREFERRED), NOT a dynamical attractor**. Conservation gives the wrong
(repulsive) sector ρ ∝ r⁻²; scale-freeness alone gives a continuum. **No dynamical principle
investigated produces the attractive α = 0 sector over the repulsive raw flux.** This is the
single non-derived step.

---

## 3. What profile does the deficit actually give?

Flat rotation v² = GM(r)/r = const ⇒ **M(r) ∝ r** ⇒ density **ρ(r) ∝ r⁻²** (the singular
isothermal sphere, SIS):

| Profile | Inner slope | Outer slope | M(r) | Source |
|---|---|---|---|---|
| **deficit (α=0, log)** | ρ ∝ r⁻² | ρ ∝ r⁻² | M ∝ r | G4-ME31 (derived from α=0) |
| NFW | ρ ∝ r⁻¹ (cusp) | ρ ∝ r⁻³ | M ∝ r²ln r | fitted (ClusterMassAudit) |
| cored (Burkert/ISO) | ρ ≈ const | ρ ∝ r⁻³ | M ∝ r³ | fitted |

The deficit's natural profile is the **SIS** (isothermal, ρ ∝ r⁻²): it reproduces the flat
rotation curve and the halo-like M ∝ r, but its inner slope (r⁻²) is **steeper than the NFW
cusp (r⁻¹)** and it is **singular at the center** — it predicts a cuspy, non-cored halo. The
NFW concentration parameter is **FITTED**, not predicted (ClusterMassAudit lists "NFW
concentration" under FITTED). The deficit does not, by itself, distinguish a cusp from a core;
it *predicts* a steeper cusp than NFW and no core.

---

## 4. Cluster structure

| Observable | Value | Status |
|---|---|---|
| Coma M_vir | 8.75×10¹⁴ M☉ | DERIVED (virial) |
| dynamical/baryon | 6.7× | DERIVED (virial) |
| AT defect model mass | ~8.8×10¹⁴ M☉ | **CORRESPONDENCE** (degenerate with ΛCDM) |
| R_vir, NFW concentration | 1.4 Mpc, fitted | **FITTED** |

The deficit reproduces the cluster **mass** (6.7× the baryons, matching ΛCDM), but the profile
shape (R_vir, NFW concentration) is fitted, and the AT defect model is **observationally
degenerate with ΛCDM** at the mass-profile level — they differ only in dark-matter identity, not
in the reconstructed mass.

---

## 5. A) naturally forms halos / B) extra assumptions / C) fails

| Determination | Verdict | Reason |
|---|---|---|
| **A) deficit naturally forms halos** | **REFUTED** | no dynamical principle selects the attractive α=0 sector over the repulsive raw flux (G4-RHO) |
| **B) deficit forms structure with extra assumptions** | **YES — the answer** | seed + growth DERIVED, but the profile needs the α=0 symmetry selection + fitted NFW concentration + hosted baryon-photon sector for acoustic peaks |
| **C) deficit fails structure formation** | **REFUTED** | given α=0 it reproduces flat rotation, M ∝ r halos, and cluster mass |

**Determination: B.** The deficit field produces the *seed* and *linear growth* of structure
derivedly, but it forms actual **halos, galaxy profiles, and cluster structure only with extra
assumptions** — chiefly the α = 0 log-deficit abundance law, which is a symmetry selection
(PREFERRED/BOUNDARY), not a dynamical attractor. This is the same boundary as NP_070 (μ=1 is
derived *conditional on scale-freeness*) and NP_066 (LSS seed + growth derived, power-spectrum
shape hosted).

---

## Theorem

> **Theorem (NP_077).** The deficit field produces the SEED and LINEAR GROWTH of structure
> derivatively (Poisson δ_i = 1/√⟨N⟩, scale-free variance, pressureless dust, δ ∝ a — QG231), but
> it forms HALOS, GALAXY PROFILES, and CLUSTER STRUCTURE only with EXTRA ASSUMPTIONS: the
> flat-rotation / halo profile (v² ≈ const, M ∝ r ⇒ ρ ∝ r⁻², the SIS) requires the α = 0
> log-deficit abundance law, which is a symmetry selection (PREFERRED/BOUNDARY), not a dynamical
> attractor (G4-RHO Phase 0: conservation gives the repulsive ρ ∝ r⁻², scale-freeness gives a
> continuum). The natural deficit profile is the SIS ρ ∝ r⁻² — steeper than the NFW inner
> cusp (r⁻¹) and singular at the center (no core) — and matching NFW/cored data requires a FITTED
> concentration (ClusterMassAudit); clusters match ΛCDM only degenerately (R_vir, NFW fitted).
> Determination: **B — structure forms only with extra assumptions.** Classification: seed + linear
> growth DERIVED (QG231); scale-free critical clustering DERIVED (QG227/228); the α=0 abundance
> law BOUNDARY (symmetry selection, G4-RHO); flat-rotation / M∝r halo profile CORRESPONDENCE
> (G4-ME); NFW/cored concentration FITTED (ClusterMassAudit); "halos form with no assumptions"
> REFUTED; "structure formation fails" REFUTED. **Success criterion: B.** No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Derive seed + growth. (2) Expose the non-derived profile step. (3) Compute
> the natural SIS profile. (4) Compare clusters. (5) Decide A/B/C. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the deficit naturally forms halos" | no dynamics selects the attractive α=0 sector; conservation gives the repulsive ρ∝r⁻² (G4-RHO) |
| "the deficit fails structure formation" | given α=0 it reproduces flat rotation, M∝r halos, and the Coma mass (6.7×) |
| "the profile is derived" | the flat rotation requires the α=0 abundance law = symmetry selection, not an attractor |
| "NFW concentration is predicted" | ClusterMassAudit lists NFW concentration as FITTED; the deficit gives the SIS ρ∝r⁻² |
| "clusters are distinguished from ΛCDM" | the AT defect model is observationally degenerate with ΛCDM at the mass-profile level |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| seed + growth are derived | a Poisson seed or δ ∝ a growth law requiring a fitted parameter |
| the α=0 profile is not dynamical | a dynamical principle that selects the attractive deficit sector over the repulsive flux |
| the natural profile is the SIS ρ ∝ r⁻² | a deficit hierarchy giving a flat rotation curve with ρ ∝ r⁻¹ or r⁰ (not r⁻²) |
| halos need extra assumptions | a halo profile emerging from scale-free actualization alone (no α=0 assumption) |
| clusters are degenerate with ΛCDM | a cluster observable that distinguishes the deficit from collisionless CDM |

---

## 8. Classification

| Component | Status |
|---|---|
| Poisson seed δ_i = 1/√⟨N⟩ | **DERIVED** (QG228/231) |
| scale-free critical clustering (Var(2k)/Var(k)=2) | **DERIVED** (QG227/228) |
| linear dust growth δ ∝ a | **DERIVED** (QG231) |
| scalar tilt n_s = 0.96497 | **DERIVED** (QG237) |
| α = 0 log-deficit abundance law | **BOUNDARY** (symmetry selection, G4-RHO) |
| flat rotation / M ∝ r halo profile (SIS ρ∝r⁻²) | **CORRESPONDENCE** (G4-ME) |
| NFW/cored concentration | **FITTED** (ClusterMassAudit) |
| cluster mass (degenerate with ΛCDM) | **CORRESPONDENCE** |
| "halos form with no assumptions" | **REFUTED** |
| "structure formation fails" | **REFUTED** |

**Conclusion.** The deficit field produces the **seed and linear growth** of structure derivatively
— Poisson noise δ_i = 1/√⟨N⟩, scale-free critical variance, pressureless dust, and linear growth
δ ∝ a (QG231), with the scalar tilt n_s = 0.96497 derived (QG237). But it forms actual **halos,
galaxy profiles, and cluster structure only with extra assumptions**: the flat-rotation halo
profile (v² ≈ const, M ∝ r ⇒ the SIS ρ ∝ r⁻²) requires the α = 0 log-deficit abundance law, a
symmetry selection (PREFERRED/BOUNDARY) rather than a dynamical attractor (G4-RHO). The natural
profile is the isothermal SIS — steeper than the NFW cusp and singular at the center — and
matching NFW/cored data needs a fitted concentration; clusters match ΛCDM only degenerately.
**Determination: B — the deficit forms structure only with extra assumptions.** No new primitive;
canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_077_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_077_PoissonSeed` | δ_i = 1/√⟨N⟩ for 1e6/1e8/1e10 | ✅ |
| `Y_NP_077_LinearGrowth` | δ ∝ a (×10 at a/a_i=10, ×100 at 100) | ✅ |
| `Y_NP_077_ScaleFreeCriticality` | Var(2k)/Var(k) = 2 | ✅ |
| `Y_NP_077_FlatRotationProfile` | v²≈const ⇒ M∝r ⇒ ρ∝r⁻² (SIS) | ✅ |
| `Y_NP_077_ProfileComparison` | deficit r⁻² vs NFW r⁻¹/r⁻³ vs cored r⁰ | ✅ |
| `Y_NP_077_AbundanceLawNotDynamical` | α=0 is symmetry selection, not attractor | ✅ |
| `Y_NP_077_ClusterCorrespondence` | Coma 6.7×, degenerate with ΛCDM | ✅ |
| `Y_NP_077_Classification` | B (extra assumptions); A and C REFUTED | ✅ |
| `Y_NP_077_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_077"`

---

## References

- AT-QG: QG194/195 (matter deficit, dust T_μν), QG206 (flat rotation α=0), QG227/228 (critical
  state, Poisson fluctuations), QG231 (structure formation origin), QG237/238 (CMB spectrum),
  QG104/105 (hierarchical network spectrum), QG116b (universal attractor).
- G4-ME Phase 1 (`G4ME_DeficitMatterGravity.md`), G4-ME Phase 3 (`G4ME_AstrophysicalProfiles.md`),
  G4-RHO Phase 0 (`G4RHO_DynamicalOrigin.md`), `Audits/ClusterMassAudit.md`.
- ResearchY-NP_065 (dark matter ontology), NP_066 (dark matter evidence), NP_070 (criticality).
