# ResearchY-NP_066 — Dark Matter Evidence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_066 (permanent)
**Title:** Dark Matter Evidence Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_066.md`
**Depends on:** AT-QG QG194 (matter = deficit), QG195 (deficit dust T_μν), QG206 (flat
rotation α=0), QG184 (M ∝ R), QG26 (PPN γ=−1, no lensing), QG212 (conformal optics
resolved via ψ), QG231 (structure formation), QG237/238 (CMB spectrum/acoustic peaks),
QG234 (Ωm = H/ln K), ResearchY-NP_065 (dark matter ontology), legacy X063–X065 (defect DM),
`Audits/ClusterMassAudit.md` (Coma)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_066_Tests.cs`

---

## Purpose

NP_065 established Dark Matter = the matter deficit m = ρ̄ − ρ (an effect, not a particle).
NP_066 measures its **explanatory power**: which observed dark-matter phenomena does the
deficit actually reproduce? Program: test six phenomena — rotation curves, gravitational
lensing, cluster dynamics, the Bullet Cluster, large-scale structure, and the CMB matter
fraction — each classified as derived / correspondence / refuted / unresolved. No new
primitives; canonical AT unchanged.

---

## 1. The six phenomena

### 1. Rotation curves — DERIVED

The deficit m ∝ r^(−α) sources gravity with a = −∇Φ ∝ r^(−α−1), giving
v² = r·|a| ∝ r^(−α). **Flat rotation (v = const) requires exactly α = 0** — equal deficit
per octave, the unique scale-free point (QG206). This is the deficit's **strongest** result:
flat rotation curves follow from the counting-measure deficit with no free parameter, and it
is consistent with M ∝ R (QG184).

### 2. Gravitational lensing — REFUTED (for the deficit alone)

The ρ-only metric is **conformally flat**: g = ρ^(2/d)η gives PPN **γ = −1**, so null
geodesics are NOT bent — **no lensing, no Shapiro delay** (QG26). Lensing is restored only by
the non-conformal **ψ tensor sector** (spin-2), a second primitive (QG212: "optics resolved"
via ψ). **The deficit m alone does not lens.**

### 3. Cluster dynamics — CORRESPONDENCE

The Coma cluster is reproduced: the dynamical mass M_vir ≈ 8.8×10¹⁴ M☉ is 6.7× the baryon
mass, and the AT defect model matches ΛCDM (the deficit is collisionless). **But it is
observationally degenerate with ΛCDM at the mass-profile level** — both require ~85% dark mass;
they differ only in dark-matter identity, not in the reconstructed mass. Reproduced, but not
distinguished.

### 4. Bullet Cluster — REFUTED

The Bullet Cluster is the signature *particle* dark-matter observation: in the merging pair,
the collisionless dark matter separates from the shocked X-ray gas (the offset between the
mass centroid and the gas centroid). AT's deficit is **not a particle** — it cannot separate
from the gas the way collisionless CDM does. The corpus is explicit: **"Bullet Cluster, CMB,
structure formation require particle DM. Hybrid needed."** The deficit does not reproduce it.

### 5. Large-scale structure — CORRESPONDENCE (partial)

The *seed* is derived: Poisson counting variance δ_i = 1/√⟨N⟩, and the deficit dust
T_μν = ρ_m v_μv_ν is pressureless and self-gravitating, growing linearly (δ ∝ a) (QG231).
The scalar tilt n_s = 0.96497 is derived (0.007%, QG237). **But the full power spectrum and the
acoustic peaks require the hosted baryon-photon sound-horizon/recombination sector** (QG238:
PARTIAL). The seed and growth are derived; the observed structure shape is hosted.

### 6. CMB matter fraction — DERIVED

Ωm = H/ln K = 0.3161 matches the observed Ωm = 0.3153 to **0.26%** (QG234). The matter
fraction is derived jointly with ΩΛ from the single information partition.

---

## 2. The scorecard

| Phenomenon | What it probes | Deficit m reproduces? | Classification |
|---|---|---|---|
| Rotation curves | gravitational potential (disk scale) | **YES** (flat, α=0) | **DERIVED** |
| Gravitational lensing | null geodesics (PPN γ) | **NO** (γ=−1, needs ψ) | **REFUTED** (deficit alone) |
| Cluster dynamics | total mass (virial) | **YES**, degenerate with ΛCDM | **CORRESPONDENCE** |
| Bullet Cluster | collisionless particle separation | **NO** (not a particle) | **REFUTED** |
| Large-scale structure | power spectrum shape | seed + growth only | **CORRESPONDENCE** (partial) |
| CMB matter fraction | Ωm | **YES** (0.3161, 0.26%) | **DERIVED** |

**Explanatory power: 2 DERIVED, 2 CORRESPONDENCE, 2 REFUTED.**

---

## 3. The pattern — what the deficit can and cannot do

The deficit reproduces **exactly those phenomena that depend only on the total
gravitational potential** (flat rotation, cluster mass, the matter fraction). It fails
**exactly those phenomena that probe the dark matter's particle nature or its relativistic
light-bending**:

```
DEFICIT m = ρ̄ − ρ is a SCALAR field of the counting measure
   → reproduces  GRAVITATIONAL-POTENTIAL phenomena   (rotation, cluster mass, Ωm)
   → fails       PARTICLE phenomena                  (Bullet Cluster separation)
   → fails       NULL-GEODESIC phenomena             (lensing — needs non-conformal ψ)
```

The deficit is a **gravitational-potential surrogate for dark matter, not a full dark
matter.** It is collisionless enough to reproduce cluster masses, but it has no particle
degree of freedom to separate from gas (Bullet), and the conformal metric it sources does not
bend light (lensing).

---

## Theorem

> **Theorem (NP_066).** The deficit ontology's explanatory power is PARTIAL and sharply
> partitioned: it reproduces gravitational-potential phenomena (flat rotation α=0 — DERIVED;
> the CMB matter fraction Ωm = 0.3161 — DERIVED; cluster mass — CORRESPONDENCE, degenerate
> with ΛCDM) but fails particle and null-geodesic phenomena (the Bullet Cluster — REFUTED, no
> collisionless separation; gravitational lensing — REFUTED, the conformal metric gives
> γ=−1). Proof: (1) Rotation (Section 1.1, verified): v² ∝ r^(−α), flat requires α=0 (QG206).
> (2) Lensing (Section 1.2): g=ρ^(2/d)η ⇒ γ=−1 ⇒ no null-geodesic bending (QG26); ψ (a second
> primitive) restores it (QG212). (3) Clusters (Section 1.3): Coma M_vir=8.8e14 M☉, 6.7×
> baryon — matched but degenerate with ΛCDM. (4) Bullet (Section 1.4): the deficit is not a
> particle, so it cannot separate from the shocked gas. (5) LSS (Section 1.5): Poisson seed +
> deficit-dust growth derived (QG231), n_s=0.96497 derived (QG237), but the power-spectrum
> shape/acoustic peaks are hosted (QG238). (6) Ωm (Section 1.6, verified): 0.3161 vs 0.3153,
> 0.26% (QG234). Classification: rotation and Ωm DERIVED; cluster mass and LSS-seed
> CORRESPONDENCE; lensing REFUTED for the deficit alone (needs ψ); Bullet Cluster REFUTED
> (needs a particle). **Success criterion: the deficit reproduces the gravitational-potential
> half of dark-matter evidence and fails the particle and light-bending half — explanatory
> power 2 DERIVED / 2 CORRESPONDENCE / 2 REFUTED.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Enumerate the six phenomena. (2) Classify each. (3) State the
> potential-vs-particle partition. ∎

---

## 4. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the deficit reproduces all dark matter" | lensing (γ=−1) and the Bullet Cluster (no particle) are not reproduced |
| "the deficit fails everything" | flat rotation (α=0), cluster mass, and Ωm = 0.3161 are reproduced |
| "the deficit reproduces the Bullet Cluster" | the deficit is a scalar field, not a collisionless particle — no separation from gas |
| "the deficit alone reproduces lensing" | conformal flatness (γ=−1) bends no null geodesics; ψ is a second primitive |

---

## 5. Falsification paths

| Claim | Falsification |
|---|---|
| flat rotation is DERIVED | a measured rotation curve deviating from the α=0 deficit scaling |
| lensing is REFUTED for the deficit alone | a conformally-flat ρ-only metric that bends null geodesics (γ ≠ −1) |
| the Bullet Cluster is REFUTED | a deficit mechanism that separates collisionlessly from shocked gas |
| Ωm = 0.3161 is DERIVED | a measured Ωm deviating from H/ln K beyond 0.26% |

---

## 6. Classification

| Component | Status |
|---|---|
| flat rotation curves (α=0) | **DERIVED** (QG206) |
| CMB matter fraction Ωm = 0.3161 | **DERIVED** (QG234) |
| cluster dynamics (Coma mass) | **CORRESPONDENCE** (degenerate with ΛCDM) |
| large-scale structure (seed + growth) | **CORRESPONDENCE** (power-spectrum shape hosted) |
| gravitational lensing (deficit alone) | **REFUTED** (γ=−1; needs ψ) |
| Bullet Cluster | **REFUTED** (needs a particle) |

**Conclusion.** The deficit ontology explains the **gravitational-potential half** of dark
matter — flat rotation curves (DERIVED, α=0), cluster masses (CORRESPONDENCE, degenerate with
ΛCDM), and the matter fraction Ωm = 0.3161 (DERIVED, 0.26%) — but it fails the **particle
half** (the Bullet Cluster, which needs collisionless separation) and the **light-bending
half** (lensing, which needs the non-conformal ψ sector). The deficit is a
gravitational-potential surrogate for dark matter, not a full dark matter. **Explanatory
power: 2 DERIVED / 2 CORRESPONDENCE / 2 REFUTED.** No new primitive; canonical AT unchanged.

---

## 7. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_066_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_066_RotationCurveFlat` | v² ∝ r^(−α); flat ⇒ α=0 | ✅ |
| `Y_NP_066_LensingRefuted` | conformal γ=−1 ⇒ no lensing | ✅ |
| `Y_NP_066_ClusterCorrespondence` | Coma 6.7× baryon, degenerate with ΛCDM | ✅ |
| `Y_NP_066_BulletRefuted` | deficit is not a particle | ✅ |
| `Y_NP_066_LSSPartial` | seed derived; power spectrum hosted | ✅ |
| `Y_NP_066_OmegaMDerived` | Ωm = 0.3161 (0.26%) | ✅ |
| `Y_NP_066_Classification` | 2 DERIVED / 2 CORRESPONDENCE / 2 REFUTED | ✅ |
| `Y_NP_066_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_066"`

---

## References

- AT-QG: QG206 (flat rotation α=0), QG194 (matter = deficit), QG195 (deficit dust T_μν),
  QG184 (M ∝ R), QG26 (PPN γ=−1, no lensing), QG212 (conformal optics via ψ), QG231
  (structure formation), QG237/238 (CMB spectrum/acoustic peaks), QG234 (Ωm = H/ln K).
- ResearchY-NP_065 (dark matter ontology), legacy X063–X065 (defect DM), `Audits/ClusterMassAudit.md`.
