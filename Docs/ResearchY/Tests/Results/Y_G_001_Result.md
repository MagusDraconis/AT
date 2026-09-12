# Y_G_001_Result.md — ResearchY-G_001 Gravity Source Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_001_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_001"`

---

## Summary

**Question:** What variable actually sources gravity in AT?

**Verdict:** The **actualization density** `rho` (the counting measure) — and, for the attractive
Newtonian sector, its **standardised deficit** `m = rhoBar - rho`.

| # | Candidate | Verdict | Decisive reason |
|---|---|---|---|
| 1 | energy density | **CORRELATED** | `T00 = (rhoBar-rho)v^2` is rank-identical to the deficit; the only conserved geometric tensor is `G/kappa` (Lovelock; kinetic candidate `nabla T = 0.0209` vs Bianchi `1e-12`); the energy *reading* is hosted (QG89) |
| 2 | **actualization density** | **SOURCE** | dimensionless local input of `g = rho^(2/d)eta`, `R = F(rho)`, `a = -(1/d)grad ln rho`, `rho_{k+1} = mu rho_k`; no free coupling |
| 3 | spectral density | **CORRELATED** | supplies `m0 = 4/95`, `r0 = ln span = 1.856691`, `A = 363 660`, `M_Pl = v A^3` (0.2%) and `G = 6.6476e-11` (0.40%); global, so never the field |
| 4 | information density | **REFUTED** | permutation-invariant global functional: same `I = 0.106440` nats with fields `-0.222222` vs `+0.095238`; it is the non-gravitating surplus (`Omega_Lambda = 0.6839`) |
| 5 | curvature law `R = F(rho)` | **REFUTED** (as a source) | it is the output of the law and not injective in `rho` (`1+0.5x^2` and `1+0.5x^2+5x^4` share `R(0) = -1.333333`) |

## Units and dimensions

| Candidate | `[M, L, T]` | Local? | Dimensionless? |
|---|---|---|---|
| energy density | `[1, -1, -2]` | yes | no |
| **actualization density** | `[0, 0, 0]` | yes | **yes** |
| spectral density | `[0, 0, 1]` | no | no |
| information density | `[0, 0, 0]` | no | yes |
| curvature `R` | `[0, -2, 0]` | yes | no |

GR's source needs the dimensionful `kappa = 8 pi G/c^4` (`kappa T` has `[0,-2,0] = [R]`); AT's source
is dimensionless and needs nothing. `a(lambda rho) = a(rho)` exactly while
`R(lambda rho) = lambda^(-2/d) R(rho)`.

## Limits

| Limit | Result |
|---|---|
| vacuum `rho = rhoBar` | `a = 0`, `R = 0` exactly |
| Newtonian | `M_eff(12) = 0.078367` = 94.04% of `m0 r0/(d rhoBar) = 0.083333`; `M_eff(200) > 99.5%` |
| field exponent | `d ln|a|/d ln r = -1.9703` (target −2) |
| log deficit | `d ln|a|/d ln r = -1.1524` (target −1) |
| `rho -> 0` | `R ∝ rho^(-2/3)`; `sqrt(-g) = rho -> 0` (degenerate metric) |
| critical point | `mu = 1`: `d rho/dt = d g/dt = 0` exactly |
| RAR | `g_dagger = c H0/(2 pi) = 1.0422e-10 m/s^2`; deep `-> sqrt(g_bar g_dagger)`; break ratio `sqrt(2)`; Newtonian `-> g_bar` |

## Comparison

| Item | AT (`rho`) | Newton / GR / RAR / QG |
|---|---|---|
| coupling | none (dimensionless source) | `G`, `kappa` (imported; G4-L12 NO MATCH) |
| magnitude | `G = 6.6476e-11` (0.40%) | CODATA `6.67430e-11` |
| scale | `g_dagger = c H0/(2 pi) = 1.0422e-10` | MOND `a0 ~ 1.2e-10` (same form) |
| flat curve | `alpha = 0` marginal member: ratios 1.1748 / 1.4914 / 1.9022 / 3.1476 / 9.0900 | RAR deep regime `v^4 = G M a0` |
| vacuum | `R = 0` (conformal) | GR vacuum may carry Weyl → `psi` (QG21/24/43/212) |
| matter | `T = (rhoBar-rho) v v` (QG195) | `T_munu` (independent) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_G_001_UnitsAndDimensions` | units/dimensions; dimensionless source; scale invariance | ✅ |
| `Y_G_001_ActualizationDensityIsSource` | metric, curvature, acceleration, vacuum, `rho -> 0`, QG222 dynamics | ✅ |
| `Y_G_001_NewtonianLimit` | exact `1/r^2`, `M_eff -> m0 r0/(d rhoBar)`, QG6/QG182 | ✅ |
| `Y_G_001_EnergyDensityCorrelated` | Lovelock/Bianchi vs kinetic divergence; `T00 ∝ m` | ✅ |
| `Y_G_001_SpectralDensityCorrelated` | `m0`, `r0`, `A`, `M_Pl`, `G`; non-locality | ✅ |
| `Y_G_001_InformationDensityRefuted` | permutation invariance vs field sign flip | ✅ |
| `Y_G_001_CurvatureLawRefuted` | exact law; non-injective; no independent d.o.f. | ✅ |
| `Y_G_001_CompareNewtonGrRarQg` | the four-way comparison | ✅ |
| `Y_G_001_Run` | research report | ✅ |

## Classification

Two-level, consistent with the D_040 registry: the SOURCE **variable** (`rho` as the field-equation
input) is **DERIVED** (QG197/G4-G2/G4-O3/QG222); the attractive-deficit **reading** is a
**HYPOTHESIS** (G4-ME0); its energy reading is **HOSTED** (QG89/NP_058); `alpha = 0` is **DERIVED**
given criticality (QG206) with scale-freeness **BOUNDARY** (AT-F1/NP_070); the SI conversion of `G`
is **BOUNDARY**. **No reclassification** — the D_040 `ClassificationRegistry` is untouched and no
QG/G4 result is superseded.

## Conclusion

AT's gravity source is the **actualization density** `rho`, and for the attractive Newtonian sector
its **standardised deficit** `m = rhoBar - rho`. Energy density and spectral density are
CORRELATED; information density and the curvature–density law used as a source are REFUTED. The
audit adds the candidate-comparison layer the gravity literature presupposed: **GR's source is
dimensionful and needs `kappa`; AT's is dimensionless and needs nothing** — which is why AT's
gravity is scale-free and its coupling magnitude must come from the D96 spectrum (QG181/QG182).
