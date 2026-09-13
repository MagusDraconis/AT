# ResearchY-G_035 — Temporal Independence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_035 (permanent)
**Title:** Temporal Independence Audit — which G-results survive if the conformal metric is removed?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_035.md`
**Depends on:** G_009 (the clock law), G_028 (clock closure — the source law IS the clock law), G_020→G_021 (the conformal boundary), G_029/G_030 (the spatial sector closes on a postulate), G_031/G_032 (conformal flatness is assumed and imposing it is refuted), M_013 (rotation self-duality)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_035_Tests.cs` (6/6 PASSED, ~4 s)
**Core:** `AT.Core/ResearchXH/TemporalIndependenceAudit.cs`

## The answer: SURVIVES — and the boundary is sharp

| verdict | component required | count |
|---|---|---|
| **SURVIVES** | g₀₀ only | **25 of 36** |
| **BOUNDARY** | g_ij (but not conformality) | **8** |
| **REFUTED** | conformal flatness (A = B) | **3** |

> **Registry extension (2026-09-13):** G_036 (Temporal Core Test) has since been added to the live registry as
> `Spatial` → **BOUNDARY**, triaged `ScanDetectsIt: false` with a written reason. The counts become **25 / 9 / 3
> of 37**; the numbers above are the state G_035 itself audited (G_001–G_034). No prior classification changed,
> and the boundary index is still **23**.

The classification axis maps 1:1 onto the output labels, so the verdict is not a judgement call: a result requiring only g₀₀ *cannot* be affected by removing conformality, because it never refers to the spatial block at all.

## The sharp boundary: G_020 → G_021

**Every audit from G_001 to G_020 is g₀₀-only.** The whole density era — the source law, the clock law, control and realizability, the SI magnitudes, the calibration, the suppression mechanism, the actuation chain, the mass-independence and watch-ontology results, the second-order signature and the neutron-star redshift — uses **g₀₀ = −ρ^(2/d) and nothing else**.

The spatial requirement begins **exactly at G_021 (Light-Propagation)**, the first audit to ask for light bending. The conformal boundary of the programme is therefore the step from G_020 to G_021, computed from the registry (boundary index **23** of 36, temporal era **22** entries).

| era | audits | what it needs |
|---|---|---|
| **temporal** | G_001–G_020 (+G_011b) | g₀₀ = −ρ^(2/d) |
| **spatial** | G_021, G_022, G_023, G_025, G_028, G_029, G_030, G_033 | g_ij (some B) |
| **conformal** | G_024, G_031, G_032 | A = B |
| **metric-free** | G_026, G_027, G_034 | nothing (verdict discipline, numerics) |

## The minimal time sector

> **1 metric function** `g₀₀ = −ρ^(2/d)` **+ 1 scalar** ρ **+ 1 exponent** `1/d` (d = 3, by rotation self-duality, M_013)
> — **no spatial metric, no conformal factor, no reference η.**

The source law and the clock law are **one statement**:

```
A = ½ln(−g₀₀) = σ = (1/d)ln ρ        (the clock potential, and Φ/c²)
dτ/dt = √(−g₀₀) = ρ^(1/d)            (the clock)
a = −(1/d)∇ln ρ = −∇A                 (the source law — the NEGATIVE gradient of A)
```

So gravity is the negative gradient of the time exponent, exactly as an attractive force requires. **The theory's time half is one equation on one scalar.**

## The arity proof (structural, not numerical)

The audit's strongest evidence is that the temporal observables are **functions whose signature contains no B**:

| temporal (no B) | spatial (takes B) |
|---|---|
| `G00(double rho)` | `GammaOf(double a, double b)` |
| `ClockOf(double rho)` | `AdmittedBand(double x)` |
| `ClockPotentialOf(double rho)` | `SurvivorB(double x)` |
| `RedshiftOf(double rho)` | |
| `SourceAccelerationOf(double[] rhoField)` | |
| `SecondOrderRatioOf(double x)` | |

**A result that cannot be *called* with B cannot *require* B.** `NoTemporalObservableTakesB()` is asserted.

Verified against the recorded results: g₀₀ = −ρ^(2/d), the clock √(−g₀₀) = ρ^(1/d), the redshift `1/√(−g₀₀) − 1` (= e^x − 1 at x = 0.247002), G_019's discriminator `e^x/√(1+2x)` = 1 + x² − (4/3)x³, and the source law as the exact central difference of −A. On a **peaked** profile the acceleration is verified to point **inward** — the attractive-force property — with the peak as an equilibrium.

## The mechanical check, and where it is honest about its own limits

The registry is *checked* against a live scan of the group-G sources (comments **and string literals** stripped, so a narrative mention cannot mask a dependency — G_019's only spatial "hit" was a report sentence and disappears under stripping).

**Two patterns were removed after measurement**, because they produced false positives on the temporal side and would have made the safety rule meaningless:

| removed pattern | what it actually matched |
|---|---|
| `double b` | a **least-squares slope** (G_006) and a **tridiagonal parameter** (G_007, G_016b) |
| `K(` | any method of that name |

And bare `psi` is **excluded on purpose**: it names the **wavefunction** in the density era (`|ψ|² = ρ`, G_014) and the metric's traceless face elsewhere — counting it would corrupt the temporal classification. The rule adopted: **a token qualifies only if it cannot plausibly mean anything else.**

**Three claims declare `ScanDetectsIt = false` with written reasons** — an explicit, auditable triage rather than a silent exception:

| audit | why the scan cannot see it |
|---|---|
| **G_025** | the dependence is an **exponent** in `ρ·exp(−dψ/(d−1))`, and `psi` is unusable as a marker |
| **G_033** | the suite contains **no metric vocabulary at all** (0/0/0) — its argument is group-theoretic (T2g) |

**The check is two-directional with different strength**: HARD (asserted) — a `ClockOnly` result must have **zero** executable spatial references; SOFT — a spatial/conformal result should show at least one, unless triaged. The HARD direction is the one that defends the audit's claim, and it holds for all 25.

## What this buys

G_031 showed conformal flatness is the *same* premise as the counting measure; G_032 showed it is an **assumed primitive η**, and that imposing it is **refuted** (γ = −1, Cassini **8.6957e4 σ**). G_035 shows the cost is **confined**:

> **The conformal problem is confined to the spatial sector. AT's time half is not hostage to its space half.**

That is a materially better position than the group appeared to be in after G_030. The dilemma is real but it is *about space*: nothing in the source, the clock, the density control, the actuation chain or the redshift results moves when the spatial metric is discarded.

## Output

| label | content |
|---|---|
| **SURVIVES** | 25 of 36 results need g₀₀ and nothing else; the minimal time sector is non-empty, self-consistent, and every temporal observable is B-free. The verdict is **computed** (G_027). |

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_035_Tests.cs` — **6/6 PASSED**
**Group total:** G_001–G_035 = **277/277 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_035"`

| test | asserts |
|---|---|
| `Y_G_035_TheTemporalSectorIsIndependentAndTheBoundaryIsSharp` | 25/8/3; boundary at index 23 = G_021; the whole prefix is ClockOnly |
| `Y_G_035_TheRegistryAgreesWithTheExecutableScan` | HARD/SOFT agreement; stripping works; the scan discriminates |
| `Y_G_035_NoTemporalObservableTakesB` | the arity proof, and that the spatial observables do take B |
| `Y_G_035_TheMinimalTimeSectorReproducesTheTemporalResults` | g₀₀, the clock, the redshift, the second-order ratio, the source law = −∇A, inward acceleration on a peak |
| `Y_G_035_VerdictIsSurvives` | computed verdict; the three ingredients |
| `Y_G_035_Run` | the full report |

**Opens:** OP1 build the **time-sector-only** theory explicitly — the 25 surviving results as a closed axiomatic set (ρ, g₀₀ = −ρ^(2/d), d = 3) with no spatial sector at all, and check whether anything in the surviving set secretly needs the spatial metric for its *interpretation* rather than its formula; OP2 apply the same audit to the **D-, T- and M-series** to find whether the conformal dependency is confined in the same way outside group G; OP3 formalise the vocabulary rule (a token qualifies as a marker only if it is unambiguous) so the next scanner does not repeat the `double b` / `psi` false positives.
