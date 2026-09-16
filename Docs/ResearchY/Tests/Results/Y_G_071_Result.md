# Y_G_071 - Result

**Audit:** ResearchY-G_071 - Observational Roadmap Audit
**Verdict:** **DERIVED**
**Tests:** 8/8 PASSED

## Answer

**One object, three precisions, and a frontier rather than a pair of numbers.** Target **J0740+6620 (Riley 2021)**: a
**5 % surface-redshift determination**, with the mass and the radius each to **7.54 %** for 3σ or **3.99 %** for 5σ
**if their errors are independent** - **±0.156 / ±0.083 M☉** and **±0.934 / ±0.494 km** - or **5.33 % / 2.82 %** each
if the errors add.

## The decomposition

`(σ_x/x)² = u² + v² − 2ρuv`, with `u = σ_M/M`, `v = σ_R/R`. For the only object with recorded marginals
(M = 1.4 ± 0.05 M☉, R = 12 ± 1 km): `u = 3.5714 %`, `v = 8.3333 %`, `v/u = 2.333`.

| model | σ_x/x | ratio to the recorded literal |
|---|---|---|
| worst case (ρ = −1), `u + v` | **11.9048 %** | **1.000** |
| independent (ρ = 0), `√(u²+v²)` | **9.0664 %** | 0.762 |
| cancellation (ρ = +1), `|u − v|` | 4.7619 % | 0.400 |

## The recorded literal

G_069's `0.1190`, called "a generic NICER compactness (11.90 %)", equals the **worst-case extreme** of these marginals
(agreement **4.76E-5**) and implies **ρ = −0.998096**. The independent form is **9.0664 %** for the same object, so the
recorded uncertainty is **1.313×** the quadrature value. At a 20 % timing the generic object's single-theory
significance is **1.0534 σ** under the literal (reproducing G_069's **1.05 σ**) and **1.1255 σ** under the quadrature -
**6.9 % higher**.

## The roadmap

| row | timing | compactness | each axis (ρ = 0) | σ_M | σ_R | worst case each | significance |
|---|---|---|---|---|---|---|---|
| **CURRENT** | 20 % | 3.661 % (published) | 2.59 % | 0.054 M☉ | 0.321 km | 1.83 % | **2.1791 σ** |
| **3SIGMA** | **5 %** | 10.66 % | **7.54 %** | **0.156 M☉** | **0.934 km** | **5.33 %** | 3.0000 σ |
| **5SIGMA** | **5 %** | 5.64 % | **3.99 %** | **0.083 M☉** | **0.494 km** | **2.82 %** | 5.0000 σ |

| finding | value |
|---|---|
| the worst case demands each axis | **1.414×** better than the independent case |
| the ρ = +1 frontier | degenerate - only `|u − v|` is constrained, the split is free |
| 3σ at the **published 20 % timing** | **NaN** - the timing term alone exceeds the budget; **no** mass-radius precision reaches it |
| the direction | **better timing RELAXES** the mass-and-radius requirement (8.41 % at 10 %, 10.66 % at 5 %, 11.32 % at 0 %) |
| the floor | with perfect timing the requirement is **11.32 % (3σ) / 6.79 % (5σ)** and cannot be relaxed further |
| the leverage | radius to perfection **2.539×**, radius equalised down **1.795×**, mass to perfection **1.088×**, equalising up **0.769× (worse)** |

## Notes

**Two of the audit's own assertions were wrong and are recorded rather than quietly fixed.** The perfect-timing limit
is **not** model-independent: the requirement scales as `1/|slope|`, so the single-theory model's requirement is the
shared-x one scaled by the slope ratio. At this target the single-theory slope is the **shallower** of the two
(−1.2802 against −1.4981), so the single-theory model demands a **looser** compactness (13.24 % against 11.32 %) - the
opposite of the ordering the audit first asserted.

**The requirement is a frontier, not a pair.** A compactness requirement cannot be converted into a mass requirement
and a radius requirement without naming the **correlation** of the two estimators' errors, which no earlier audit has
carried. The roadmap therefore states three constraint forms rather than two numbers, and the observer-facing program
names the correlation it assumes.
