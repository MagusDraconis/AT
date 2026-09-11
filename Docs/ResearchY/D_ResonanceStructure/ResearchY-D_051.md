# ResearchY-D_051 — Blind Prediction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_051 (permanent)
**Title:** Blind Prediction Audit
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `D_ResonanceStructure/ResearchY-D_051.md`
**Depends on:** D_050 (spectral predictability; the frozen relations), D_048 (capacity), D_049 (recovery, frontier), T_014 (near-gap counting convention)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_051_Tests.cs` (5 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`
**Prediction commit:** `003a0033` (files contain the prediction only — **no measurement code**)
**Measurement commit:** the commit following `003a0033`

---

## Question

**Does D_050's spectral prediction actually work on spectra it has never seen?**

D_050 ended with a minimal predictor set of **one** quantity — the near-gap density — and a fitted
one-predictor law for both targets. That law was selected and scored inside its own six-case
ensemble. The natural next question is the only one that matters for a *predictive* claim: run it
prospectively.

**Success criterion: the prediction must be made before the simulation results are known.**

---

## Method — the blind protocol

The protocol is enforced by **file order in git**, not by convention:

| step | content | commit |
|---|---|---|
| **PHASE A** | six new rings are generated; their spectral inputs are read off; capacity and recovery are predicted from the D_050 relations alone and printed. The test file never constructs the perturbation ensemble. | `003a0033` |
| **PHASE B** | the same six rings are measured under the shared D_048/D_049 ensemble and compared against the frozen prediction verbatim | next commit |

Because Phase A contains no measurement code, the predicted values are on record in the repository
history **before any observed value for these spectra exists**. Phase B additionally re-derives the
six D_050 cases from the frozen constants and checks that D_050's published R² (0.759 / 0.525) is
reproduced — so the constants cannot have been adjusted after the fact.

### The frozen relations (the only predictor coefficients used)

$$ \text{capacity} = -0.00814\,(\text{near-gap density}) + 0.87153 $$
$$ \text{recovery} = +0.000284\,(\text{near-gap density}) + 0.95582 $$

### The six new spectra

All are 96-node circulant rings ($C_{96}$ with the listed signed offsets, weight $w_d$), none of
which appears in the D_048–D_050 case set. Each is connected — required, since the shared ensemble
guards connectivity and would otherwise reject every perturbation.

| ring | definition | degree |
|---|---|---|
| S96-123 | offsets ±1…±3, unit weights | 6 |
| S96-135 | offsets ±1, ±3, ±5, unit weights | 6 |
| D96-24 | offsets ±1…±12, unit weights | 24 |
| Decay96 | offsets ±1…±6, weights $w_d = 1/d$ | 12 |
| Boost96 | offsets ±1…±6, weights $w_d = d$ | 12 |
| Ring48 | offsets ±1…±6 plus the long-range ±24 and ±48 | 12 |

---

## Results

### 1. The prediction (PHASE A — no simulation present)

| ring | λ₂ | near-gap (2λ₂) | degeneracy | distinct | **predicted capacity** | **predicted recovery** |
|---|---|---|---|---|---|---|
| S96-123 | 0.059822 | 2 | 44 | 45 | 0.85525 | 0.95639 |
| S96-135 | 0.148851 | 2 | 43 | 45 | 0.85525 | 0.95639 |
| D96-24 | 2.693016 | 2 | 38 | 39 | 0.85525 | 0.95639 |
| Decay96 | 0.089286 | 2 | 47 | 49 | 0.85525 | 0.95639 |
| Boost96 | 1.870529 | 2 | 46 | 47 | 0.85525 | 0.95639 |
| Ring48 | 5.404246 | 8 | 44 | 45 | 0.80641 | 0.95809 |

**Already visible before any dynamics were run:** five of the six rings receive an *identical*
prediction, because their near-gap density is the minimum possible value **2**. The one-predictor
law is **degenerate on the ring family**. The prediction therefore asserted, in advance, that
S96-123, S96-135, D96-24, Decay96 and Boost96 all have the same capacity — despite degrees of 6,
6, 24, 12 and 12 and three different weight profiles.

### 2. The measurement (PHASE B — the shared ensemble)

Same protocol as D_048/D_049: 4 perturbation types × 5 doses × 3 fixed seeds, doses as fractions
of the ring's own edge count, connectivity-guarded, zero exclusions.

| ring | near-gap | predicted cap | **observed cap** | \|Δ\| | predicted rec | **observed rec** | \|Δ\| |
|---|---|---|---|---|---|---|---|
| S96-123 | 2 | 0.85525 | **0.96928** | 0.1140 | 0.95639 | 0.96044 | 0.0041 |
| S96-135 | 2 | 0.85525 | **0.93693** | 0.0817 | 0.95639 | 0.96643 | 0.0100 |
| D96-24 | 2 | 0.85525 | **0.97193** | 0.1167 | 0.95639 | 0.96937 | 0.0130 |
| Decay96 | 2 | 0.85525 | **1.00000** | 0.1447 | 0.95639 | 0.91977 | 0.0366 |
| Boost96 | 2 | 0.85525 | **0.99524** | 0.1400 | 0.95639 | 0.97537 | 0.0190 |
| Ring48 | 8 | 0.80641 | **0.99248** | 0.1861 | 0.95809 | 0.97244 | 0.0143 |

**Every prediction was too low** — the error is one-sided, because near-gap = 2 is the *minimum* of
the predictor and rings sit at that minimum.

### 3. Metrics

**Absolute error**

| target | mean \|error\| | max \|error\| | observed span | predicted span |
|---|---|---|---|---|
| capacity | **0.1305** | 0.1861 | 0.0631 | 0.0488 |
| recovery | **0.01617** | 0.03662 | 0.05560 | 0.00170 |

* The capacity error is **2.07× the entire observed range of the truth** — the prediction is worse
  than the spread of what it is predicting.
* The law sees **3.1 %** of the real recovery variation (predicted span 0.0017 against an observed
  0.0556).
* Distinct predicted capacities: **2**. Distinct observed capacities (4 d.p.): **6**.

**Rank error** (tie-averaged ranks; the five identical predictions share a rank)

| ring | pred rank | obs rank | \|Δrank\| | | pred rank | obs rank | \|Δrank\| |
|---|---|---|---|---|---|---|---|---|
| | *(capacity)* | | | | *(recovery)* | | |
| S96-123 | 4.0 | 2.0 | 2.0 | | 3.0 | 2.0 | 1.0 |
| S96-135 | 4.0 | 1.0 | 3.0 | | 3.0 | 3.0 | 0.0 |
| D96-24 | 4.0 | 3.0 | 1.0 | | 3.0 | 4.0 | 1.0 |
| Decay96 | 4.0 | 6.0 | 2.0 | | 3.0 | 1.0 | 2.0 |
| Boost96 | 4.0 | 5.0 | 1.0 | | 3.0 | 6.0 | 3.0 |
| Ring48 | 1.0 | 4.0 | 3.0 | | 6.0 | 5.0 | 1.0 |

* **capacity**: mean |Δrank| = **2.00 of 5 possible** (40 % of the range), max 3.0;
  Spearman ρ = **−0.131** — the ordering is *anti*-correlated.
* **recovery**: mean |Δrank| = **1.33**, max 3.0; Spearman ρ = +0.393 — better, but only because the
  observations themselves occupy a narrow band.
* Inside the union of all twelve cases the capacity rank error is also 2.00; for the six new cases
  alone in that union it is 2.92. That union metric is generous to the law, since for the six old
  cases the prediction *is* the fitted value.

The law's information about these six rings is exactly **one bit of ordering** — "Ring48, and then
everybody else" — and for capacity that bit is wrong.

---

## Classification

### DERIVED

* **Near-gap = 2 is forced on rings.** For any 1-D ring whose low-$k$ spectrum follows the $k^2$ law,
  $\lambda_2/\lambda_1 = 4$ and $\lambda_3/\lambda_2 = 9/4 > 2$. Hence the set
  $\{\lambda > 0 : \lambda \le 2\lambda_2\}$ contains exactly the $k = \pm 1$ doublet — **two modes,
  whatever the weight profile**. Verified here: five of five unit-offset and tapered rings give 2;
  the sixth (Ring48) escapes only because its long-range offsets raise $\lambda_1$ and lift
  $2\lambda_2$ over eight modes.
* **Consequence (analytic, not fitted): D_050's minimal predictor set is a CONSTANT on the ring
  family** — the very family D96 belongs to. D_050's $R^2 = 0.759$ must therefore have been carried
  by its non-ring cases (random, complete, and the 3-D torus). This was visible at prediction time:
  the six predicted values are just two numbers.
* **The direction of the failure is derived too.** near-gap = 2 is the *minimum* of the predictor, so
  on a ring every error is a one-sided **under**-prediction. D_050's own largest residual (D96,
  −0.1349, also at near-gap = 2) was the first instance; all six new rings repeat the sign
  (−0.0817 … −0.1861). The sign is systematic, not noise.
* **D_050's derived scaffolding survives intact.** Every prediction lies inside $0 \le
  \text{capacity}, \text{recovery} \le 1$; the zero-degeneracy null is untouched.

### EMERGENT

* **Six structurally different rings all adapt strongly:** capacity 0.9369 … 1.0000. Sparse (±1…±3),
  dense (±1…±12), tapered ($w = 1/d$), long-range-weighted ($w = d$) and extended (±24, ±48) rings
  alike collect nearly all of their headroom. The measured recovery band (0.9198 … 0.9754) is **33×
  wider** than the law predicted. These are emergent ensemble numbers for new spectra.
* **The blind outcome distribution itself** — 5 identical predictions against 6 distinct
  measurements — is the emergent evidence that D_050's minimal set is family-local.

### REFUTED

* **"The D_050 one-predictor law predicts capacity and recovery on new spectra."** REFUTED. Capacity
  mean absolute error **0.1305** against an observed span of **0.0631** — the error is **2.07× the
  whole range of the truth** — and the recovery prediction captures 3.1 % of the real variation.
  Both targets were predicted before measurement, and both missed.
* **"The minimal predictor set is a usable ranking device."** REFUTED prospectively for capacity:
  Spearman ρ = **−0.131** (anti-correlated), mean |Δrank| = 2.00 of 5. D_050's ranking claim rested
  on in-sample ordering; on new spectra of the same family it does not survive.
* **"D_050's minimal set generalizes beyond its own case set."** REFUTED. The predictor takes two
  distinct values across six structurally very different rings. Worse, the single discrimination it
  *can* make is wrong: it orders Ring48 **last** on capacity, while the measurement puts it **fourth**.

---

## Verdict

**DERIVED** — near-gap = 2 is forced on rings ($\lambda_2/\lambda_1 = 4$, $\lambda_3/\lambda_2 = 9/4$),
so the minimal predictor set is a constant on the ring family; the sign of the error is
under-prediction by construction; D_050's bounds survive.

**EMERGENT** — all six new rings adapt strongly (capacity 0.9369 … 1.0000); the recovery band is 33×
wider than predicted; the 5-identical-predictions-against-6-distinct-measurements outcome.

**REFUTED** — the predictive claim (error 2.07× the observed range), the ranking claim
(ρ = −0.131 for capacity), and generalization beyond D_050's own ensemble.

**Success criterion met:** the prediction was made and committed (`003a0033`) in a file containing no
measurement code, before any observed value for these spectra existed.

**Consequence for the D group.** D_050's answer stands as stated — adaptability is *ordered* by the
degeneracy axis within its own ensemble — but this audit narrows it sharply: **the ordering does not
transfer to new spectra of the same topological family**, because on circulants the chosen predictor
is a constant. A usable ring-family predictor must read a quantity that actually varies across rings;
the degeneracy count **did** vary (38 … 47) while the near-gap density did not, so the collinear
pair's *other* member is the better candidate. That is a concrete hypothesis for the next audit
(D_052), not a new claim.

No canonical AT claim, value, equation or registry entry is changed; the D_040
`ClassificationRegistry` is untouched; D_050's classifications are reaffirmed, not reclassified.

---

## References

* **D_050** — Spectral predictability: the frozen relations, the derived bounds, the minimal set.
* **D_048** — Latent-degeneracy adaptability: capacity and the headroom identity.
* **D_049** — Adaptability–robustness frontier: recovery and the family dependence.
* **T_014** — Near-gap counting convention (positive eigenvalues within $2\lambda_2$).
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Shared machinery** — `AT.Tests/Shared/AdaptabilityAudit.cs` (`NearGapDensityK2`, `Study(name, adj)`).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_051_Tests.cs` (5 tests, all passing).
