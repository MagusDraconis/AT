# ResearchY-G_072 - Observational Program Audit

**Program:** ResearchY - Gravity Source Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_072 (permanent; G_071 is the earlier Observational Roadmap Audit and is preserved)
**Title:** What exact future measurement can first decide AT vs GR using a neutron-star redshift?
**Status:** COMPLETE
**Date:** 2026-09-17
**File:** `G_GravitySource/ResearchY-G_072.md`
**Depends on:** G_068 (the redshift split), G_069 (the compactness decision map), G_070 (the target and the programme), G_071 (the mass/radius frontier)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_072_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ObservationalProgramAudit.cs`

## The question

**What exact future measurement can first decide AT vs GR using a neutron-star redshift?** Output **target**,
**precision**, **instrument class** and **decision significance**, as a realistic observer-facing test plan.

## The answer

> **THE PROGRAM IS ONE MEASUREMENT ON ONE OBJECT, AND THE FIRST REALISTIC DECIDER IS A NEXT-GENERATION TIMING
> MEASUREMENT. TARGET: J0740+6620. MEASUREMENT: its surface gravitational redshift, equivalently its compactness
> −GM/(Rc²), to 14.1422 % for 3σ and 7.5524 % for 5σ. INSTRUMENT CLASS: a large-area X-ray timing facility at 10 %
> timing with 1 % and 3 % mass and radius marginals, which reaches 4.1374σ. DECISION SIGNIFICANCE: 4.1374σ there, with
> a direct surface-redshift measurement to 1 % reaching 44.8377σ.**

## 1. The target, taken from the earlier audits rather than re-derived

| quantity | value |
|---|---|
| target | **J0740+6620 (Riley 2021)** |
| mass | 2.072 M☉ |
| radius | 12.39 km |
| compactness | **x = −0.24704** (recomputed from the mass and radius) |
| recorded relative σx | 3.661 % |

It is the **most compact published object**, so the second-order AT-vs-GR split is largest on it - which is why
G_070 selected it and why this audit does not re-open the choice.

## 2. The program - the four requested outputs, class by class

| instrument class | timing | compactness | **significance** | reaches |
|---|---|---|---|---|
| A. current X-ray timing (NICER-class) | 20 % | 9.0664 % | **1.9235σ** | 1σ |
| B. A plus radio pulsar timing | 20 % | 8.3339 % | **1.9641σ** | 1σ |
| **C. next-generation X-ray timing** | **10 %** | **3.1623 %** | **4.1374σ** | **3σ** |
| D. next-generation timing and spectroscopy | 5 % | 1.5811 % | **8.2749σ** | **5σ** |
| **E. a direct surface-redshift measurement** | **1 %** | — | **44.8377σ** | **5σ** |

**The correlation is taken at ZERO, the conservative choice**, so every significance in that table is a **floor**: a
favourable negative mass-radius correlation only shrinks the compactness error and can only help.

**Class E is the decisive one**: a resolved redshifted surface feature needs **neither a mass nor a radius**, because
the redshift *is* the measurement.

## 3. The precision the decision needs

| significance | required timing | required compactness | which binds |
|---|---|---|---|
| **3σ** | **14.1422 %** | **NaN** | **the TIMING** |
| **5σ** | **7.5524 %** | **NaN** | **the TIMING** |

**The required compactness is NaN at the recorded 20 % timing, and that is the finding rather than a failure: no
compactness precision decides the question until the timing improves.** These are the numbers **G_070** recorded for
this target (7.55 % / 14.14 %), recovered here from the decision side.

## 4. The leverage - and it refuted the audit's own draft

**Each capability improved ALONE, the other two held at the recorded current values:**

| capability | significance | **gain** |
|---|---|---|
| the baseline (A) | 1.9235σ | 1.0000 |
| **timing alone to 1 %** | **3.7316σ** | **1.9400** |
| mass alone to 0.1 % | 1.9641σ | 1.0211 |
| radius alone to 1.5 % | 2.1720σ | **1.1292** |
| everything together (D) | 8.2749σ | 4.3020 |

**The draft expected the radius to lead. The measurement gives timing 1.9400, radius 1.1292, mass 1.0211 - timing
leads by a factor of 1.7200 over the radius.**

**And that is G_071's finding recovered rather than contradicted**, because the two tables are about **different
operations**: improving **one capability alone at the current budget** is a **timing** question, while equalising the
two marginals **at a fixed significance** is the **radius** question. The reason timing wins here is visible in the
requirements table - **the required compactness is NaN at 20 % timing**.

## 5. The plan in phases

| phase | capability | instrument class | significance | decides |
|---|---|---|---|---|
| 1 (in hand) | published mass and radius | A | 1.9235σ | **no** - the status quo is undecided |
| 2 (near term) | the same targets with radio-timing masses | B | 1.9641σ | **no** - the mass is not what binds |
| 3 (next generation) | large-area X-ray timing, 10 % redshift | C | **4.1374σ** | **3σ** |
| 4 (next generation, spectroscopic) | 5 % redshift with a 1.5 % radius | D | **8.2749σ** | **5σ** |
| 5 (decisive) | the redshift itself, to 1 % | E | **44.8377σ** | **5σ** |

**The first two phases fail, and they fail for different reasons:** the published row fails because the timing is at
20 %, and the radio-timing row fails for the **same** reason even after the mass is fixed to a tenth of a per cent -
**better masses do not buy the decision.**

## 6. The honest limits

1. **The classes are capability triples, not mission commitments.** The audit measures what a given
   (timing, mass, radius) triple buys; it does **not** audit whether any funded instrument will deliver a triple.
2. **The correlation is zero.** A negative mass-radius correlation only helps, so the numbers are floors - and the
   frontier in G_071 is where the favourable cases live.
3. **Class E assumes a resolved surface feature exists.** It is the strongest route by two orders of magnitude
   (44.8377σ against 4.1374σ) and it is also the one with no current instrument behind it; the audit states that
   rather than folding it into the plan's near term.

## 7. Defects in the audit's own first version

1. **My draft headline was refuted by the measurement** (section 4): I claimed the decision "is not bought with
   timing" and that the radius carries the leverage. Timing leads, the radius is second, and the mass is nearly
   useless alone.
2. **My expectation for the current row was wrong**, and for a reason worth recording: I expected **1.05-1.2σ** from
   G_071 and the measurement gives **1.9235σ**. The two are about **different objects and different error models** -
   G_071's 1.0534σ is the **generic** 1.4 M☉, 12 km object at its **worst-case correlated** compactness error of
   11.9048 %, while this row is the **target** J0740+6620 (more compact, so a larger second-order separation) at the
   **quadrature** error of 9.0664 %. The test now asserts the measured value and records the difference.
3. **A bookkeeping error**: I compared the **class census** (which counts classes) with the **length of the
   FirstDecider list** (which returns only the first decider) and asserted 2 = 1. The test now counts classes on both
   sides.
