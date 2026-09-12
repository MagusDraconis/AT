# ResearchY-G_001 — Gravity Source Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source (new)
**ID:** ResearchY-G_001 (permanent)
**Title:** Gravity Source Audit — what variable actually sources gravity in AT?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_001.md`
**Depends on:** AT-QG QG6 (native deficit scale), QG181/QG182 (Newton constant + bridge),
QG194/QG195/QG196 (matter = deficit, deficit dust), QG197 (metric `g = rho^(2/d)*eta`),
QG206 (alpha = 0 criticality), QG212/QG21/QG24/QG43 (conformal sector and psi), QG222 (native
metric dynamics), QG228/QG234 (information = KL, Omega_Lambda); G4-G0/G2/G3/G4 (Einstein
structure, Lovelock), G4-O3/O4/O5 (acceleration, rho interpretation), G4-ME0/ME2/ME3/ME4
(deficit matter, long range, rotation curves), G4-RHO00-02 (dynamical origin of rho), G4-L12
(BDG normalisation NO MATCH); AT-F1/NP_070 (scale-freeness, criticality); ResearchY-QG_001
(information-geometry bridge), NP_055-NP_064 (dark-energy ontology), NP_065 (dark matter =
deficit), NP_071 (matter ontology), NP_075 (force ontology), NP_076 (psi is not dark matter)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_001_Tests.cs` (9/9 PASSED)

---

## Purpose

AT routes gravity through a single object, the counting measure `rho` (QG197/QG222), and the QG/G4
literature repeatedly says "the actualization density is the *only* source" (G4-G3, G4-O4). That
claim has never been tested against its *rivals*. This audit takes the five candidate source
variables, gives each a unit, a dimension, a set of limits and a comparison against Newton, GR,
the RAR and the QG results, and adjudicates each as **SOURCE**, **CORRELATED** or **REFUTED**.

**Question.** What variable actually sources gravity in AT?

**Candidates.** (1) energy density, (2) actualization density, (3) spectral density,
(4) information density, (5) the curvature–density law `R = F(rho)`.

---

## 0. Definition of the labels

| Label | Meaning |
|---|---|
| **SOURCE** | the variable the field equation takes as **input**: it appears in `R = F(.)`, in `g = f(.)*eta`, and in the geodesic acceleration, and the equation is not invertible into it. |
| **CORRELATED** | not the source, not refuted: a variable that fixes the source's **parameters or magnitude** (spectral density) or is an **exact re-expression** of the source in other units (energy density), so mistaking it for the source needs one extra hosted identification. |
| **REFUTED** | a variable that **cannot** be the source, with a decisive reason (a permutation-invariant global functional; an output of the law). |

---

## 1. Units and dimensions

| # | Candidate | Units | `[M, L, T]` | Local? | Dimensionless? |
|---|---|---|---|---|---|
| 1 | energy density | J m^-3 | `[1, -1, -2]` | yes | no |
| 2 | **actualization density** | count per 4-volume (used as the ratio `rho/rhoBar`) | `[0, 0, 0]` | yes | **yes** |
| 3 | spectral density | modes per unit angular frequency | `[0, 0, 1]` | **no** (frequency space) | no |
| 4 | information density | nats per mode | `[0, 0, 0]` | **no** (one number per distribution) | yes |
| 5 | curvature `R = F(rho)` | 1/m^2 | `[0, -2, 0]` | yes | no |

**The dimensional argument.** The conformal factor `rho^(2/d)` of `g = rho^(2/d)*eta` must be
dimensionless, so the variable entering it must be dimensionless. Only the actualization density is
both dimensionless **and** local. Energy density is not dimensionless: it can reach curvature
dimensions only through the dimensionful coupling, `kappa = 8*pi*G/c^4` has `[M^-1 L^-1 T^2]` and
`kappa*T` has `[0, -2, 0] = [R]` — consistent, but only *with* `kappa`, which AT must import
(G4-L12: BDG scale −2 **NO MATCH**). The AT field equation needs no coupling at all: with `rho`
dimensionless, `[R] = L^-2` comes entirely from `rho`'s own derivatives. **This is the audit's
cleanest separation: GR's source is dimensionful and needs `kappa`; AT's source is dimensionless and
needs nothing.**

**Scale invariance (tested).** `a(lambda*rho) = a(rho)` exactly, while `R(lambda*rho) =
lambda^(-2/d)*R(rho)`. The native field depends only on *ratios* of the counting measure; the
absolute normalisation is a boundary (`rhoBar = 1`, QG182). This is why AT's gravity is scale-free
and why the *magnitude* of `G` must be supplied from elsewhere (§5).

---

## 2. Candidate 2 — actualization density: **SOURCE**

| Evidence | Result |
|---|---|
| metric | `g = rho^(2/d)*eta` (QG197) — `rho` alone; `rho = 0.5`, `d = 3` → conformal factor 0.6300 |
| curvature | `R = F(rho)` exact (G4-G2), `< 1e-12` against the metric-based route |
| acceleration | `a = -(1/d)*grad ln rho` (G4-O3), numerical vs analytic agreement `< 1e-8`; **no free coupling** |
| matter sector | attractive `a = +(1/d)*grad m/rho` for `m = rhoBar - rho` (G4-ME0) |
| dynamics | `rho_{k+1} = mu*rho_k` (QG222), count conserved, metric `g_{k+1} = mu^(2/d)*g_k` |
| **vacuum limit** | `rho = rhoBar` → `a = 0` **exactly**, `R = 0` **exactly** |
| **rho → 0 limit** | curvature diverges as `rho^(-2/3)`, `sqrt(-g) = rho` degenerates (PlanckRegime) |
| critical point | `mu = 1` (alpha = 0): `d rho/dt = 0` and `d g/dt = 0` exactly |

`rho` is the input of every AT gravitational statement. The *attractive Newtonian* sector is the
**standardised deficit** `m = rhoBar - rho` — a two-level structure the audit records explicitly
(§7).

---

## 3. Candidate 1 — energy density: **CORRELATED**

| Evidence | Result |
|---|---|
| deficit dust | `T_00 = (rhoBar - rho)*v^2` (QG195/196): positively rank-identical to the deficit `m`, negatively to `rho` |
| Lovelock | the only conserved symmetric second-order tensor built from the scalar geometry is `G/kappa` (G4-G4); Bianchi residual of the native `G` is `< 1e-9` |
| kinetic candidate | a gradient-built ("kinetic") energy tensor is **not** conserved: `nabla T_kin = 0.0209` at `x = 1, a = 0.5, d = 3`, i.e. `> 10^6 *` the Bianchi residual |
| independence | the dust is built from the deficit value and the flow, so it escapes the Lovelock obstruction (`MatterIndependentOfG()`), and `G = kappa*T` is a dynamical relation, **not** the vacuous identity `T := G/kappa` (G4-G3 superseded by QG222) |
| energy *reading* | hosted: "energy = actualization rate" is a **definition** (QG89/NP_058/NP_059) |

**Verdict.** Energy density is not a rival of `rho`; it is the same deficit counted in joules rather
than in events. The identification "deficit → energy" is *hosted*, so if one starts from energy
density one has already assumed the connection to `rho`. **CORRELATED.**

---

## 4. Candidate 3 — spectral density: **CORRELATED**

| Evidence | Result |
|---|---|
| source parameters | `m0 = occ0/SigmaM = 4/95 = 0.042105263`, `r0 = ln(span) = 1.856690882`, `rhoBar = 1`, `d = 3` — every QG6 deficit parameter is a D96 spectral quantity (QG182) |
| coupling magnitude | `A = SigmaM*#g*occ2 = 95*44*87 = 363 660`; `M_Pl/v = A^3 = 4.80935e16`; `M_Pl = v*A^3 = 1.223339e19` GeV (0.2%); `G = 6.64761e-11` m^3 kg^-1 s^-2 (**0.40%** from CODATA 6.67430e-11) |
| hierarchy exponents | the octave occupancy follows `modes_k ~ center_k^delta` (QG141) — the spectral-density exponent sets the mass hierarchy |
| **locality test** | permuting a density profile leaves every spectral moment (`Sigma x_i^2`, occupancy multiset) unchanged while the field flips sign: `a(1.5) = -0.222222` vs `+0.095238` |

**Verdict.** The spectral density **is not local** — it has no position index — so it can only enter a
local field equation through dimensionless *constants*. And that is exactly what it does: it supplies
the deficit parameters `m0`, `r0` and, through the occupation-weighted spectral content `A`, the
**magnitude** of `G`. It fixes the source's scale, never the source field. **CORRELATED.**

---

## 5. Candidate 4 — information density: **REFUTED**

| Evidence | Result |
|---|---|
| definition | `I = ln K - H(rho) = KL(rho || uniform) >= 0` (QG228); `I = 0` exactly for the uniform (vacuum) record |
| value | `I([0.1,0.2,0.3,0.4]) = 0.106440135` nats |
| **permutation test** | `I(record) = I(reversed record) = 0.106440135` — *identical* — while the same two configurations have fields of **opposite sign** (`-0.222222` vs `+0.095238`) |
| index count | a global scalar with **zero** position indices cannot be the source term of a local field equation |
| direction of the bridge | QG_001: information is a **function of** `rho`, not the reverse |
| physical role | the information density is the **dark-energy surplus** (`Omega_Lambda = I_occ/ln K = 0.6839`), and the dark-energy arc NP_055-NP_064 established that the surplus is a **descriptor with no derived gravitational role** (NP_060: it cannot do work); the *deficit* gravitates (NP_065) |

**Verdict.** The decisive refutation is the permutation test: the same information content is
compatible with **different** fields, so `I` does not determine the field. `I` tracks the source in
the trivial sense that both vanish in the uniform state — but it is the *non-gravitating* face. The
audit separates the two faces cleanly: **the deficit gravitates, the surplus only describes.**
**REFUTED.**

---

## 6. Candidate 5 — the curvature–density law `R = F(rho)` as a source: **REFUTED**
*(the law itself is DERIVED, G4-G2 — only its role as source is refuted)*

| Evidence | Result |
|---|---|
| exactness | `R(0) = -4*a*(d-1)/d` for `rho = 1 + a*x^2`: `a = 0.1, d = 3` → `-0.2666666667`; `a = 0.5, d = 3` → `-1.3333333333`; `a = 0.1, d = 2` → `-0.2000000000` |
| no independent d.o.f. | over a one-parameter family of densities the curvature is perfectly anti-correlated with the parameter (`Pearson = -1`, strictly monotone): `R` adds nothing to `rho` |
| vacuum | `rho = rhoBar` → `R = 0` — curvature vanishes with the source |
| **non-injectivity** | `R(0) = -2(d-1)*rho''(0)/(d*rho(0))`, so `rho = 1 + 0.5x^2` and `rho = 1 + 0.5x^2 + 5x^4` share `R(0) = -1.333333` while differing by `> 1` at `x = 1` |

**Verdict.** `R` is the **output** of the law. It is not injective in `rho`, so `R` cannot determine
`rho`; inverting the law to source `rho` is circular. **REFUTED as a source.**

---

## 7. Limits tested (summary)

| Limit | AT result | Reading |
|---|---|---|
| vacuum, `rho = rhoBar` | `a = 0`, `R = 0` exactly | the source is what makes `rho` non-uniform; AT has **no vacuum curvature** in the conformal sector |
| weak deficit (Newtonian) | `a = -m0*r0/(d*rhoBar*r^2)`; `M_eff(12) = 0.078367` = **94.04%** of `m0*r0/(d*rhoBar) = 0.083333`; `M_eff(200) > 99.5%` | the deficit **is** a Newtonian point mass in the far field |
| exponent of the field | `d ln|a| / d ln r = -1.9703` (target −2) | exact `1/r^2` range (QG6, G4-ME22) |
| log deficit | `d ln|a| / d ln r = -1.1524` (target −1) | flat rotation curve (G4-ME3) |
| `rho → 0` | `R ∝ rho^(-2/3)` → `10 000` at `rho = 1e-6`; `sqrt(-g) = rho → 0` | horizon-like degeneration of the metric |
| critical source | `mu = 1` (alpha = 0): `d rho/dt = d g/dt = 0` exactly | the source's own dynamics is stationary at criticality (QG206/QG222) |
| information | `I = 0` exactly for the uniform record | information and gravity vanish together — but do not determine each other (§5) |
| RAR | `g_obs = g_bar*sqrt(1 + g_dagger/g_bar)`, `g_dagger = c*H0/(2*pi) = 1.0422e-10 m/s^2`; deep regime `g_obs → sqrt(g_bar*g_dagger)`, break ratio `sqrt(2)` exactly at `g_bar = g_dagger`, Newtonian `→ g_bar` | the AT research model sits in the same family as the observed RAR (QG080) |

---

## 8. Comparison with Newton, GR, RAR and the QG results

| Item | AT (source = `rho`) | Newton / GR / RAR / QG |
|---|---|---|
| source variable | actualization density `rho` (counting measure) | mass density / `T_munu` |
| coupling | **none** (dimensionless source) | `G`, `kappa` (imported; G4-L12 NO MATCH) |
| long-range field | `a = -(1/d)*grad ln rho` → `m0*r0/(d*rhoBar*r^2)` | `a = -G*M/r^2` |
| coupling magnitude | `G = 6.6476e-11` (QG181/182, 0.40%) | CODATA `6.67430e-11` |
| acceleration scale | `g_dagger = c*H0/(2*pi) = 1.0422e-10 m/s^2` | MOND `a0 ~ 1.2e-10`; same functional form |
| flat-curve exponent | `alpha = 0` unique marginal member: ratios `v^2(3)/v^2(9)` = 1.1748, 1.4914, 1.9022, 3.1476, 9.0900 for `alpha` = 0, 0.25, 0.5, 1, 2 (QG206) | RAR deep regime `v^4 = G*M*a0` |
| vacuum curvature | `R = 0` identically in the conformal sector | GR vacuum admits non-zero Weyl curvature → **psi sector required** (QG21/24/43/212) |
| matter tensor | `T = (rhoBar - rho)*v*v` (QG195) | `T_munu` independent in GR |
| tensor conservation | deficit dust conserved; kinetic/gradient candidate **not** conserved | conserved by construction |
| Einstein equation | `G = kappa*T` as a relation with an **independent** dust (QG222), not `T := G/kappa` | field equation with an independent source |

The audit **confirms, and sharpens**, the canonical QG/G4 statement: the actualization density is the
source. It also makes explicit an asymmetry the literature states but does not test — **GR's source
must carry dimensions and therefore needs `kappa`; AT's source is dimensionless and needs nothing**,
which is why AT's gravity is scale-free and its coupling magnitude must come from the D96 spectrum
(QG181/QG182).

---

## 9. Verdicts

| # | Candidate | Verdict | Decisive reason |
|---|---|---|---|
| 1 | energy density | **CORRELATED** | exact re-expression of the deficit dust (rank-identical); the only conserved geometric tensor is `G/kappa` (Lovelock); the energy *reading* is hosted (QG89) |
| 2 | **actualization density** | **SOURCE** | the dimensionless, local input of `g = rho^(2/d)*eta`, `R = F(rho)`, `a = -(1/d)*grad ln rho`, `rho_{k+1} = mu*rho_k`; no free coupling |
| 3 | spectral density | **CORRELATED** | supplies the source's parameters (`m0`, `r0`) and the magnitude of `G` (`A`, `M_Pl = v*A^3`); non-local, so never the field |
| 4 | information density | **REFUTED** | permutation-invariant global functional: the same `I` with different fields; it is the non-gravitating surplus (`Omega_Lambda`) |
| 5 | curvature–density law | **REFUTED** (as a source) | it is the output of the law and is not injective in `rho`; sourcing `rho` from `R` is circular |

**Answer to the question.** Gravity in AT is sourced by the **actualization density** `rho` — the
counting measure — and, for the attractive Newtonian sector, by its **standardised deficit**
`m = rhoBar - rho`.

---

## 10. Classification (two-level, consistent with the D_040 registry)

| Component | Status |
|---|---|
| `rho` (counting measure) as the field-equation input | **DERIVED** (QG197, G4-G2, G4-O3, QG222) |
| the attractive sector being the deficit `m = rhoBar - rho` | **HYPOTHESIS / REAL-UNDERIVED** (G4-ME0: the sign is derived, the identification is a reinterpretation) |
| the energy *reading* of the deficit | **BOUNDARY / HOSTED** (QG89, NP_058/059) |
| `alpha = 0` (flat-curve exponent) | **DERIVED** given criticality (QG206); scale-freeness itself **BOUNDARY** (AT-F1/NP_070) |
| the deficit dispersion relation `M_eff = m0*r0/(d*rhoBar)` | **DERIVED** (QG6) |
| the magnitude of `G` | **DERIVED as a calibrated read** (QG181/182, 0.2%/0.4%); the SI conversion is a **BOUNDARY** |
| `rhoBar = 1` and the exponent `2/d` | **BOUNDARY** (metric ansatz, G4-A0) |
| the `psi` tensor completion | **BOUNDARY / separate sector** (QG24/43, unchanged) |
| the information density | **DERIVED** as a functional of `rho` (QG228); **REFUTED** as the source |
| `R = F(rho)` | **DERIVED** (G4-G2); **REFUTED** as a source |
| the spectral density | **DERIVED** as spectral content (QG141); **CORRELATED** with the source's parameters and scale |

**No reclassification.** Nothing here changes a canonical classification: the D_040
`ClassificationRegistry` is untouched, and no QG/G4 result is superseded. The audit *adds* the
candidate-comparison layer that the gravity literature presupposed.

---

## 11. Falsification paths

| Claim | Falsification |
|---|---|
| `rho` sources gravity | a metric whose source is not `rho` (e.g. a residual curvature at uniform `rho` in the conformal sector) |
| the source is dimensionless | a demonstration that the conformal factor requires a dimensionful input |
| energy density is only correlated | an independent conserved energy tensor built from `rho` that differs from the deficit dust |
| spectral density is only correlated | a local field that depends on the spectrum at fixed `rho` |
| information density is not the source | two configurations with the same field but different `I` (the permutation test's converse) |
| the curvature law is not the source | a density that `R` determines uniquely across the whole profile family |

---

## 12. Open problems

1. **G-001 OP1 — the dynamical origin of `rho`.** The audit locates the source; it does not explain
   why `rho` takes the profile it does. G4-RHO00-02 left this OPEN; QG206/QG222 supply criticality
   and the flow, but not the profile selection.
2. **G-001 OP2 — why the *deficit* branch.** The attractive sector needs `m = rhoBar - rho`, while
   raw flux conservation selects the *repulsive* `rho ∝ r^-2` (G4-RHO01). The sign selection remains
   a hypothesis.
3. **G-001 OP3 — localising the spectral input.** The spectral density is global; whether a *local*
   spectral functional (per-region occupancy) exists and whether it sharpens the `G` magnitude claim
   is open.
4. **G-001 OP4 — the `psi` boundary.** The conformal sector has no vacuum curvature, so lensing and
   tensor waves rest entirely on the separate `psi` sector (QG24/43/212) — outside this audit's scope.

---

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_001_Tests.cs`
**Run:** 2026-09-12 · **Result:** `Tests/Results/Y_G_001_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_G_001_UnitsAndDimensions` | units/dimensions of all five candidates; dimensionless source; scale invariance | PASS |
| `Y_G_001_ActualizationDensityIsSource` | `g = rho^(2/d)eta`, `a = -(1/d)grad ln rho`, vacuum, `rho -> 0`, QG222 dynamics | PASS |
| `Y_G_001_NewtonianLimit` | exact `1/r^2`, `M_eff -> m0*r0/(d*rhoBar)`, QG6/QG182 numbers | PASS |
| `Y_G_001_EnergyDensityCorrelated` | Lovelock/Bianchi vs kinetic divergence; `T00 ∝ m`; hosted energy reading | PASS |
| `Y_G_001_SpectralDensityCorrelated` | `m0`, `r0`, `A`, `M_Pl`, `G`; non-locality of the spectrum | PASS |
| `Y_G_001_InformationDensityRefuted` | permutation invariance vs field sign flip; surplus role | PASS |
| `Y_G_001_CurvatureLawRefuted` | `R = F(rho)` exact; non-injective; no independent d.o.f. | PASS |
| `Y_G_001_CompareNewtonGrRarQg` | Newton/GR/RAR/QG comparison (`g_dagger`, `alpha = 0`, dust, dynamics) | PASS |
| `Y_G_001_Run` | research report | PASS |

**Conclusion.** AT's gravity source is the **actualization density** `rho` (counting measure), and
for the attractive Newtonian sector its **standardised deficit** `m = rhoBar - rho`. Energy density
and spectral density are **CORRELATED** (an exact re-expression of the deficit; the source's
parameters and the magnitude of `G`). Information density is **REFUTED** (a permutation-invariant
global functional — it is the non-gravitating dark-energy surplus) and so is the curvature–density
law used as a source (**REFUTED** — it is the output of the law and is not injective). No canonical
value is reclassified; no new primitive is introduced.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_001"`

---

## References

- ResearchY-QG_001 (information–geometry bridge), NP_055-NP_064 (dark-energy ontology),
  NP_065 (dark matter = deficit and the deficit/surplus asymmetry), NP_070 (criticality),
  NP_071 (matter ontology), NP_075 (force ontology), NP_076 (psi is not dark matter).
- AT-QG: QG6, QG141, QG181, QG182, QG194, QG195, QG196, QG197, QG206, QG21, QG24, QG43, QG212,
  QG222, QG228, QG234.
- G4: G0, G2, G3, G4, A0, O3, O4, O5, ME0, ME2, ME3, ME4, RHO00-02, L12; `AT_Gravity_Reassessment.md`,
  `ATQG_GravityConsolidation.md`.
- `Docs/RAR_TimeInterpretation.md` (QG080), `Docs/ATQG_PhysicsCoverage.md` (Gravity / GR section).
