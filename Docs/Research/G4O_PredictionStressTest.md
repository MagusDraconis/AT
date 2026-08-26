# G4-O Phase 2 — Stress-Test the Discriminating Prediction

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-O)
**Phase:** 2 — does the GR/AT difference survive realistic density profiles?
**Status:** COMPLETED — 3/3 xUnit tests pass (9/9 G4-O)
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Verify that the qualitative GR/AT difference — AT source ∝ (lnρ)″ (field ∝ −∇lnρ), GR source ∝ ρ
(field ∝ −∫ρ) — persists under realistic profiles (Gaussian halos, NFW-like halos, exponential disks,
uniform spheres, shells), and classify it ROBUST / WEAK / ARTIFACT.

---

## 2. Results

| profile | a_GR (density-source) | a_AT (curvature-source) | difference |
|---|---|---|---|
| Gaussian halo (x=0.4) | −0.525 (attractive) | **+0.231 (repulsive)** | sign flip |
| uniform sphere inside | −0.400 (linear) | 0 | localization |
| uniform sphere outside | −1.300 (long-range) | 0 | localization |
| NFW-like (x=0.8) | −1.021 (attractive) | +0.061 (repulsive) | sign flip |
| exponential disk (x=0.8) | −0.973 (attractive) | +0.053 (repulsive) | sign flip |
| pure exponential | saturates | **1/(d·r_d) = constant** | MOND-like constant |
| shell outside | −0.853 (long-range) | 4×10⁻¹⁰ (≈0) | localization |

---

## 3. Findings

1. **Sign flip** (Gaussian, NFW-like, exponential): AT's field is **repulsive** around density peaks
   (a = −∇lnρ > 0), GR's is **attractive** (a = −∫ρ < 0).
2. **Localization** (uniform sphere, shell): AT's field vanishes wherever ρ is uniform (inside and
   outside a compact mass), while GR's is linear inside and long-range (1/r²) outside.
3. **MOND-like constant** (pure exponential): ρ = A·e^(−r/r_d) gives a_AT = 1/(d·r_d), a **constant**
   repulsive acceleration — a striking AT-specific signature vs GR's attractive saturation.

---

## 4. Classification: **ROBUST**

The qualitative difference is **not an artifact of a single profile**. Across Gaussian halos, NFW-like
halos, exponential disks, uniform spheres, and shells, the same two structural signatures persist:

- **AT repulsive around density peaks** (field points toward density minima),
- **AT zero-field in uniform/exterior regions** (field is localized at gradients).

Both follow directly from the source being the log-density curvature (lnρ)″ rather than the density value
ρ, and are therefore robust to the exact density profile.

---

## Test program

| Test | Verdict |
|---|---|
| G4-O20 `G4_O20_GaussianAndUniformSphere` | PASS (sign flip + localization) |
| G4-O21 `G4_O21_NfwAndExponential` | PASS (sign flip + MOND-like constant) |
| G4-O22 `G4_O22_ShellAndClassification` | PASS (shell localization + ROBUST) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added `Nfw`, `Exponential`, `UniformSphere`);
tests `AT.Tests/ResearchXH/G4O_Phase2_PredictionStressTestTests.cs`.
