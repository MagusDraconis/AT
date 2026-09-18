# ResearchY-G_077 - Clock Source Audit

**Program:** ResearchY - Gravity Source Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_077 (permanent)
**Title:** What produces local clock-rate changes, and can the source be operated without gravity?
**Status:** COMPLETE
**Date:** 2026-09-18
**File:** `G_GravitySource/ResearchY-G_077.md`
**Depends on:** G_028 (the source law and the clock law are one statement), G_035 (the time sector is clock-only), G_049 (the clock pattern is lossless in the occupancy), G_075 (the forced data and the family), G_076 (no structural constraint selects the law)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_077_Tests.cs` (9/9 PASSED)
**Core:** `AT.Core/ResearchXH/ClockSourceAudit.cs`

## The question

**What produces local clock-rate changes?** The audit starts **only from the surviving AT primitives** - **Difference**,
**Actualization**, **the density ρ**, **causal structure**, **count conservation**, the surviving **G_035 temporal
sector** - and it does **not** use the GR field equations, a specific clock law, multiplicativity, neutron-star fitting
or any imported time-aether concept. It must define the **minimal object C** that changes the clock rate, decide which
quantity drives it, derive **clock change → redshift without assuming an F**, decide whether local clock changes are
**passive observables or manipulable states**, and identify what that costs - including the boundary question **can AT
permit time manipulation without gravity manipulation?**

## The answer

> **REFUTED - THE CLOCK HAS NO INDEPENDENT SOURCE, AND TIME CONTROL IS GRAVITY CONTROL.** The **minimal object C is
> ONE SCALAR** (the local occupancy ρ); the clock adds **ZERO** degrees of freedom to it; and **the clock offset and
> the acceleration integral are TWO READINGS OF THE SAME POTENTIAL DIFFERENCE** - so they vanish together and appear
> together **for every monotone clock law**, and the one direction they cannot read is also the one direction **count
> conservation forbids**.

## 1. The minimal object: one scalar, and the clock as a readout of it

| observation | count |
|---|---|
| temporal observables in the sector | **6** |
| taking the occupancy | **5** |
| taking the potential (the same field re-expressed) | **1** |
| **taking anything else** | **0** |

**A second driver would have no argument to enter through.** The sector's content is unchanged from G_035's arity
proof - **1 metric function, 1 scalar, 1 exponent** - and the audit re-runs it in the form this question needs.

**The readout is LOCAL**, measured: a **one-part-in-a-thousand** occupancy change at one site moves the rate **there**
and the largest change it makes anywhere else is **0.000E+000**.

**THE RESPONSE RANKS ARE THE AUDIT'S SHARPEST MEASUREMENT** (perturb one site at a time and take the rank of the
response matrix):

| observable | directions available | rank | **directions missed** |
|---|---|---|---|
| **clock rate at every site** | 9 | **9** | **0** |
| **acceleration at every site** | 9 | **8** | **1 - the uniform one** |
| the redshift between the ends | 9 | 1 | 8 |

**The clock reads the occupancy completely; the acceleration is blind to exactly one direction - the uniform
shift.**

## 2. The driver is the density, and every alternative is measured

| candidate driver | verdict | measurement |
|---|---|---|
| **the density ρ** | **DRIVES IT** | the clock is a function of ρ alone, and the rate→ρ inverse is recovered by bisection to **9.100E-016** |
| **the actualization rate** | **THE SAME QUANTITY** | reconstructing ρ from the rate has a residual of **9.100E-016**, so the "driver" is a renaming of the readout |
| **causal connectivity** | **NOT A DRIVER** | two graphs with the same occupancy give the **same** clock **and the same acceleration**; and the connectivity-to-gravity bridge is recorded as an **open problem**, not a result |
| **information density** | **NOT A DRIVER** | the same local occupancy with a **different global entropy** gives the same local clock |
| **the phase sector / another surviving quantity** | **NOT AVAILABLE** | G_049: the clock pattern is **lossless in ρ** with a **closed-form inverse**, so there is no second quantity to drive it |

## 3. Clock change → redshift, with no clock law assumed

> **1 + z = W(A_s)/W(A_o)** for an **arbitrary positive monotone W**, with **A = (1/d) ln ρ**.

The relation is measured over **33 law/pair combinations** carrying the pinned data - including **the exponential as
one member among others** and a **non-analytic** mesh member - and it holds **for all of them**, which is what makes it
**F-FREE** rather than a property of a chosen form.

**A local measurement fixes only the LOGARITHMIC DERIVATIVE of W**, measured at two steps:

| law | g'(A) | residual (h = 1E-4) | residual (h = 1E-5) | fall |
|---|---|---|---|---|
| **exp(2x)** | up to 1.0 | 9.564E-013 | 4.057E-013 | at the round-off floor |
| **mesh interpolant (δ = 0.01)** | 1.0 | 3.042E-002 | 3.0E-003 | **~10x per decade - a truncation, not a failure** |

**The stiff non-analytic member is why the audit reports two steps**: a fixed tolerance would have failed it for being
stiff rather than for being wrong (defect 5 below).

**SUCCESSIVE REDSHIFT FACTORS TELESCOPE** for every law - the product of the per-step factors **is** the total factor - which is the reason **G_076's composition constraint is about POTENTIALS ADDING** and not about redshifts
composing: the redshift side is automatic and therefore empty.

## 4. The decisive measurement: one potential difference, two readings

The surviving source law makes the acceleration the gradient of the **same** logarithmic potential,
**a = -grad A**, so the **acceleration integral** between two points **is** minus their potential difference **for any
clock law**; and the **clock offset** is `g(A_s) - g(A_o)` with `g = ln W`. Measured on **4 configurations × 11 laws**:

- **the two readings share their ZERO SET for every law** (`True`);
- the **vacuum** configuration gives **0.000E+000** for both;
- and **the ratio of the two readings is law-dependent** - it is the mean logarithmic derivative of W - so the
  **price** is not fixed: **AT says the accompanying gravity cannot be zero and cannot say how large it is**.

## 5. Passive or manipulable? Both, but only through the density

| test | result | measurement |
|---|---|---|
| the clock changes when the **local occupancy** changes | **YES** | a one-part-in-a-thousand occupancy change moves the rate by **3.045E-002** |
| the clock changes at sites whose occupancy did **not** change | **NO** | the largest rate change elsewhere is **0.000E+000** |
| the same perturbation **moves the acceleration profile** | **YES** | the largest acceleration change is **1.666E-004** |
| a clock change is available with **no occupancy change** | **NO** | nothing else is in the observable's signature |

**So the clock is manipulable - time control is not killed - and the required source term is the DENSITY's, not the
clock's.** The census: **4 quantities, ONE field, ONE transport, TWO readouts**, where the "clock potential" and "the
source of acceleration" are **the same object**.

**Count conservation makes the source a TRANSPORT**, measured: a redistribution keeps the total at **7.000000**
exactly while raising the clock at the probe and raising the acceleration at the source **by the same act**.

## 6. The boundary: confine or remove? The answer has three levels

| question | answer | measurement |
|---|---|---|
| a clock offset with **NO acceleration inside the region** that carries it | **YES** | interior acceleration **0.000E+000** with an offset of **2.000E-001**, the whole difference carried by **one wall link** |
| a clock change with **no observable redshift**, if the total count may change | **YES** | a uniform shift moves the clock by **3.045E-002** and **every pairwise redshift by 0.000E+000**, while changing the total occupancy by **4.709E-001** |
| the same, **under count conservation** | **NO** | the uniform direction is the **only** one with no observable redshift, and it is the one the conserved count forbids |
| a clock offset with **no acceleration on ANY path** to the comparison point | **NO** | the clock reads **9** directions, the acceleration **8** - the difference is the uniform one |

**And the gradient's support is free while its integral is not** (the width-versus-strength trade, measured on the
gradient each **lattice link** carries): a one-cell wall carries the whole **2.000E-001** of the transition, and
widening it lowers the peak in proportion - so a clock offset can be **confined** to a thin layer but not **removed**
from every path.

## 7. Defects in the audit's own first version

1. **THE UNIFORM DIRECTION WAS FIRST REPORTED THROUGH THE RANK OF THE REDSHIFT**, which is **1** because the redshift
   between two fixed points is **one number** - a rank cannot show *which* directions a single observable is blind to.
   The audit now measures the uniform shift directly, and **count conservation is what closes the argument**: the
   direction nobody can see is the direction the conserved count forbids.
2. **THE SUPPRESSION WITNESS WAS CARRIED AS F AND NOT AS A RATE**, so its logarithmic derivative came out **1.201**
   instead of 1 and it was reported as failing the pinned data. It is now the **square root** of that function.
3. **THE MESH WITNESS WAS REBUILT HERE AT AN AMPLITUDE THAT BROKE MONOTONICITY** (0.6 against G_076's bound
   `2δ/π` = 0.0064), so the audit's own non-analytic member was not a viable clock law. It now **uses G_075/G_076's
   interpolant as the rate**.
4. **THE WIDTH TRADE MEASURED THE PEAK WITH A CENTRAL DIFFERENCE**, which **smooths a one-cell step** and made a
   one-cell wall and a two-cell wall **indistinguishable** - so the trade looked flat where it should be steep. It now
   measures **the gradient each lattice link carries**, which is what a lattice actually holds.
5. **THE FIRST-ORDER LAW WAS ASSERTED WITH A FIXED TOLERANCE**, which the stiff non-analytic member failed **for being
   stiff rather than for being wrong**; the audit now measures the residual **at two steps** and asserts the fall.
6. **THE RATE→ρ INVERSION WAS BISECTED OVER ρ ∈ [0.01, 10]**, outside the physical band, and reported two laws as
   non-invertible where they invert perfectly where the theory is used; the bracket is now the **physical band**.

## Where it stands

**The clock has no source of its own.** It is a **readout** of one scalar, it adds no field, it cannot be moved except
by moving occupancy - and the **same transport** that moves it moves the acceleration, because the two are **the same
potential difference read twice**. The audit derives that **without any clock law**: the ratio form holds for every
positive monotone W, the source law that makes the acceleration a gradient of the same potential is a **surviving**
result, and the rank measurement shows the **one** direction the acceleration cannot see is the one **no observable
and no count-conserving move** can see.

**So the answer to the question the audit was asked to prefer a refutation of is: local clock changes ARE sourceable,
and the source is NOT the clock's.** Time control is not impossible; it is **not separate**. The boundary answer is
therefore two-level and the audit refuses to collapse it: **AT permits a clock offset with no LOCAL gravity, and
permits NO observable clock offset that is not bought by a gradient somewhere on every comparison path.**

**What is left open, and it is the same gap G_075 and G_076 left**: the **price**. The ratio of clock offset to
acceleration integral is the mean logarithmic derivative of the clock law, and the surviving sector does not select it
- the audit reports its spread over the corpus and says so instead of inventing a number. Closing that ratio needs
either the law (the sector's own open question) or a measurement of the pair at a compact object (G_072).
