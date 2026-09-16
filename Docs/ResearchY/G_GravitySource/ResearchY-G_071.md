# ResearchY-G_071 - Observational Roadmap Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_071 (permanent)
**Title:** What exact observations are required to decide AT redshift vs GR redshift first?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_071.md`
**Depends on:** G_068 (the one-line prediction), G_069 (the thresholds and the uncertainty models), G_070 (the target and the requirements)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_071_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/ObservationalRoadmapAudit.cs`

## The question

What exact observations are required to decide **AT redshift vs GR redshift** first? Measure the **target object**, the
required **timing** precision, the required **mass** precision, the required **radius** precision and the **resulting
significance**. Output **CURRENT / 3SIGMA / 5SIGMA**. Goal: a concrete, observer-facing test program.

## The answer

> **One object, three precisions, and a frontier rather than a pair of numbers.** The target is **J0740+6620
> (Riley 2021)**; the program is a **5 % surface-redshift determination**, and the mass and the radius must each reach
> **7.54 %** for 3σ or **3.99 %** for 5σ **if their errors are independent** - that is **±0.156 / ±0.083 M☉** and
> **±0.934 / ±0.494 km**. **The audit also refutes its own input:** the "generic NICER compactness (11.90 %)" that
> G_069 recorded is the **worst-case extreme** of the repository's own marginals, not a generic value.

## 1. The decomposition the earlier audits never made

For `x = −k M/R` the compactness error is **not** determined by the mass and radius errors separately:

```
(σ_x/x)² = u² + v² − 2ρuv,   u = σ_M/M,   v = σ_R/R
```

with `ρ` the correlation of the two estimators' relative errors. **A single compactness number therefore does not fix
the two axis requirements** - which is why G_070's requirement (stated as a compactness precision) and this question
(stated as a mass precision *and* a radius precision) are not the same request, and the answer has to be a frontier.

The repository carries **marginals for exactly one object** - G_068's deciding experiment, **M = 1.4 ± 0.05 M☉** and
**R = 12 ± 1 km** - so `u = 3.5714 %`, `v = 8.3333 %`, `v/u = 2.333`.

| model | form | σ_x/x | ratio to the recorded literal |
|---|---|---|---|
| worst case | `u + v` (ρ = −1) | **11.9048 %** | **1.000** |
| independent | `√(u² + v²)` (ρ = 0) | **9.0664 %** | 0.762 |
| cancellation | `|u − v|` (ρ = +1) | 4.7619 % | 0.400 |

## 2. The recorded literal is an extreme of these marginals, not a generic value

G_069's scenario table carries the literal **`0.1190`**, described in its own basis string as **"a generic NICER
compactness (11.90 %)"**. The worst-case extreme of these marginals is **11.9048 %** - agreement **4.76E-5** - and the
correlation the literal implies is **ρ = −0.998096**. Measured consequences:

- the quadrature value for the same object is **9.0664 %**, so the recorded compactness uncertainty is **1.313×** the
  independent form;
- at a 20 % timing the generic object's single-theory significance is **1.0534 σ** under the literal (reproducing
  G_069's own **1.05 σ**, which is the cross-check that the trace is right) and **1.1255 σ** under the quadrature -
  **the current significance is 6.9 % higher than the audits have been recording.**

**This is the same defect class as the A₀ = 20 812 artifact:** a number that *looks* computed, carried in a scenario
table and quoted downstream, whose value is an assumption about correlation rather than a measurement.

## 3. The roadmap: CURRENT / 3SIGMA / 5SIGMA

Target **J0740+6620 (Riley 2021)**, `x = −0.247001`, separation **1.256E-001** (shared-x difference model):

| row | timing | compactness | each axis (ρ = 0) | σ_M | σ_R | worst case (ρ = −1) each | resulting significance |
|---|---|---|---|---|---|---|---|
| **CURRENT** | 20 % (published) | 3.661 % (published) | 2.59 % | 0.054 M☉ | 0.321 km | 1.83 % | **2.1791 σ** |
| **3SIGMA** | **5 %** | 10.66 % | **7.54 %** | **0.156 M☉** | **0.934 km** | **5.33 %** | 3.0000 σ |
| **5SIGMA** | **5 %** | 5.64 % | **3.99 %** | **0.083 M☉** | **0.494 km** | **2.82 %** | 5.0000 σ |

The **worst case demands each axis 1.414× better** than the independent case, because the two errors add instead of
being added in quadrature - the single largest modelling lever in the roadmap.

**The frontier at the 3σ requirement (10.66 %):**

| model | constraint |
|---|---|
| ρ = −1 | `σ_M/M + σ_R/R = 10.66 %` |
| ρ = 0 | `√((σ_M/M)² + (σ_R/R)²) = 10.66 %` |
| **ρ = +1** | `|σ_M/M − σ_R/R| = 10.66 %` - **the split itself is free (degenerate)** |

## 4. The requirement is a surface, and the direction is counter-intuitive

| reachable timing | 3σ compactness | 3σ each | 5σ compactness | 5σ each |
|---|---|---|---|---|
| **published (20 %)** | **NaN** | **NaN** | **NaN** | **NaN** |
| improved (10 %) | 8.41 % | 5.95 % | **NaN** | **NaN** |
| projected (5 %) | 10.66 % | 7.54 % | 5.64 % | 3.99 % |
| perfect (0 %) | **11.32 %** | 8.00 % | 6.79 % | 4.80 % |

**A better timing RELAXES the mass-and-radius requirement** - the timing term takes a smaller share of the budget, so
looser mass and radius determinations suffice. The audit expected the opposite and reports the measured direction. At
the **published 20 % timing the requirement does not exist at all (NaN)**: the timing term alone already exceeds the
3σ budget, so **no mass-and-radius precision can reach it**. **The program's first step is therefore the timing, not
the mass-radius work** - which is the reverse of the order the earlier audits implied.

And the requirement has a **floor**: with perfect timing it is set by the separation alone (**11.32 %** at 3σ,
**6.79 %** at 5σ) and **cannot be relaxed further by any timing improvement**.

## 5. Which axis buys the decision

Measured on the only object whose split is known (`v/u = 2.333`):

| move | from | to | improvement |
|---|---|---|---|
| drive the **mass** to perfection | 9.0664 % | 8.3333 % | **1.088×** |
| drive the **radius** to perfection | 9.0664 % | 3.5714 % | **2.539×** |
| equalise **down** (radius → mass's precision) | 9.0664 % | 5.0508 % | **1.795×** |
| equalise **up** (mass relaxed → radius's precision) | 9.0664 % | 11.7851 % | **0.769×** |

**The radius carries the leverage**, and the last row is the trap the audit measures rather than warns about:
adopting the radius's poorer precision on both axes makes the compactness **worse**.

## 6. The program

| step | what | precision | why |
|---|---|---|---|
| **1** | **J0740+6620's surface redshift** | **5 %** (3σ at 14.14 % timing per G_070) | **without it the requirement is undefined** - a 20 % timing puts the 3σ target out of reach whatever the mass-radius work achieves |
| **2** | **J0740+6620's mass and radius** | **each to 7.54 %** for 3σ, **3.99 %** for 5σ (±0.156 / ±0.083 M☉ and ±0.934 / ±0.494 km) | with **independent** mass and radius errors; if they add, **5.33 % / 2.82 %** each |
| **3** | the **radius first, then the mass** | radius to the mass's current precision buys **1.795×**; the mass to perfection buys **1.088×** | the radius carries **2.333×** the relative error |

## Verdict

**DERIVED.** The roadmap is **one object and three precisions** - a **5 %** redshift determination of **J0740+6620**
with the mass and the radius each to **7.54 % (3σ)** or **3.99 % (5σ)** under independent errors, or **5.33 % / 2.82 %**
if they add - and the audit states it as a **frontier with the correlation named**, because a single compactness number
does not determine it. **The audit refutes its own input:** G_069's "generic NICER compactness (11.90 %)" is the
**worst-case extreme** of the repository's own marginals (implied **ρ = −0.998**), and the independent form for the
same object is **9.07 %**, raising the current significance from **1.05 σ to 1.13 σ**.
