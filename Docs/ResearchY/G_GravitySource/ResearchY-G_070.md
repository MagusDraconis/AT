# ResearchY-G_070 - Observational Decision Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_070 (permanent)
**Title:** What exact neutron-star measurements would decide AT vs GR first?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_070.md`
**Depends on:** G_068 (the one-line prediction), G_069 (the compactness thresholds and the three uncertainty models), G_020 (the NICER error budget), G_019 (the signature)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_070_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ObservationalDecisionAudit.cs`

## The question

What **exact neutron-star measurements** would decide AT vs GR **first**? Use the **current NICER limits** and the
**published mass-radius ranges**. Output the **1σ, 3σ and 5σ** requirements, the **target stars** and the **required
precision**. Goal: a **concrete observing program** capable of excluding either AT or GR.

## The answer: **DERIVED - the program is ONE TARGET AND ONE MEASUREMENT**

> **Measure the surface redshift of J0740+6620 to 7.55 % and the question is decided at 6.4σ.** The compactness is
> recomputed from the published mass and radius (`x = −1.4770 M/M☉ / (R/km)`), it reproduces the earlier audits' values,
> and the requirement is stated **in both directions** so an observer can trade timing against mass-radius work.

## 1. The catalogue (published NICER targets, compactness recomputed)

| target | M (M☉) | R (km) | x | σ_x/x | z_AT | z_GR | separation |
|---|---|---|---|---|---|---|---|
| **J0740+6620 (Riley 2021)** | 2.072 | 12.39 | **−0.247001** | 3.661 % | 0.2801806 | 0.4058070 | **1.256E-001** |
| J0740+6620 (Miller 2021) | 2.080 | 13.70 | −0.224245 | 3.477 % | 0.2513779 | 0.3465532 | 9.518E-002 |
| J0030+0451 (Riley 2019) | 1.340 | 13.02 | −0.152011 | **2.636 %** | 0.1641728 | 0.1986768 | 3.450E-002 |
| J0030+0451 (Miller 2019) | 1.440 | 13.02 | −0.163355 | 2.790 % | 0.1774544 | 0.2187056 | 4.125E-002 |

`σ_x/x` is the **published constraint on the compactness** rather than a quadrature sum of the mass and radius marginals,
because the joint posterior constrains the ratio more tightly than the marginals suggest.

## 2. What each target can decide

| target | at 20 % timing | at 5 % timing | with perfect timing |
|---|---|---|---|
| **J0740+6620 (Riley)** | **2.18σ** | **6.45σ** | 9.27σ |
| J0740+6620 (Miller) | 1.86σ | 6.09σ | 10.26σ |
| J0030+0451 (Riley) | 1.05σ | 4.06σ | **15.43σ** |
| J0030+0451 (Miller) | 1.16σ | 4.42σ | 14.31σ |

## 3. The required precision, in both directions

| target | significance | redshift precision at the published compactness | compactness at 5 % timing | which axis dominates |
|---|---|---|---|---|
| **J0740+6620** | **1σ** | **44.58 %** | 33.74 % | timing (1.2 % of budget vs 1.2 %) |
| **J0740+6620** | **3σ** | **14.14 %** | 10.66 % | timing (11.2 % vs 10.5 %) |
| **J0740+6620** | **5σ** | **7.55 %** | 5.64 % | timing (31.1 % vs 29.1 %) |
| J0030+0451 | 1σ | 20.97 % | 39.50 % | timing (5.7 % vs 0.4 %) |
| J0030+0451 | 3σ | 6.87 % | 9.50 % | timing (50.9 % vs 3.8 %) |
| J0030+0451 | **5σ** | **3.98 %** | **NaN** | **budget breached at 5 % timing (141.5 %)** |

**Which axis binds is a question about the budget, not about the requirement numbers** - the two requirement numbers are
equivalent conditions, so the audit reports each axis's **share of the significance budget**. At J0740+6620 the shares are
**comparable** (31 % timing against 29 % compactness at 5σ); at J0030+0451 the timing share is **141 %**, so 5σ is
unattainable there at a 5 % timing whatever the compactness does.

## 4. The program

| step | target | measurement | precision | effect |
|---|---|---|---|---|
| **1** | **J0740+6620** | a **simultaneous surface-redshift determination** (burst or line spectroscopy) with the mass and radius from the published NICER posterior | **1σ at 44.58 %, 3σ at 14.14 %, 5σ at 7.55 %** | **excludes whichever theory the measurement does not fit** |
| 2 | J0030+0451 | the same measurement, as the cross-check | 1σ at 20.97 %, 3σ at 6.87 %, 5σ at 3.98 %; **it becomes the better target once the timing reaches 2.051 %** | tests the method rather than the theory |
| 3 | either | a NICER-class mass-radius improvement | at 5 % timing the compactness must reach 10.66 % for 3σ and 5.64 % for 5σ | excludes nothing alone; it lowers the timing the other steps need |

## 5. The slack on current determinations

| significance | needed | better than 20 % | better than 50 % |
|---|---|---|---|
| 1σ | 44.58 % | 0.4× | 1.1× |
| 3σ | **14.14 %** | **1.4×** | **3.5×** |
| **5σ** | **7.55 %** | **2.6×** | **6.6×** |

**The program is not a new instrument but a stated factor on an existing measurement.**

## 6. A draft claim withdrawn, and a ranking that depends on the error

The draft assumed the most compact target wins on **both** counts. **The measurement refused it:** the most compact target
has the **largest separation** but the **best-constrained compactness belongs to J0030+0451**. The ranking therefore
depends on which error dominates, and the audit computes the crossing rather than asserting it:

- **20 % timing → J0740+6620**
- **5 % timing → J0740+6620** (6.45σ against 4.06σ)
- **perfect timing → J0030+0451** (15.43σ against 9.27σ)
- **the two swap at a timing precision of 2.051 %**, where both give 8.54σ.

## Verdict

**DERIVED.** The program is **one target and one measurement**: **J0740+6620's surface redshift to 7.55 %** (5σ) or
**14.14 %** (3σ) with its published NICER mass and radius - a **2.6× to 6.6×** improvement on current redshift
determinations. It **excludes whichever theory the measurement does not fit**, and if timing ever reaches **2.051 %** the
better target becomes **J0030+0451**.
