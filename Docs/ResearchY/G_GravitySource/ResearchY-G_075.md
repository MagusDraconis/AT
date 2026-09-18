# ResearchY-G_075 - Clock-Law Uniqueness Audit

**Program:** ResearchY - Gravity Source Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_075 (permanent)
**Title:** Which parts of g00 = -exp(2x) are forced by the surviving empirical constraints, and what is the maximal family g00 = -F(x)?
**Status:** COMPLETE
**Date:** 2026-09-18
**File:** `G_GravitySource/ResearchY-G_075.md`
**Depends on:** G_019/G_020 (the clock law and the redshift quadratic), G_035 (the time sector is clock-only), G_068-G_072 (the prediction and the programme that would decide it), G_073 (the exponential's uniqueness among named alternatives), G_074 (the clock law's necessity up to its map)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_075_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/ClockLawUniquenessAudit.cs`

## The question

Assume **only the surviving empirical constraints** - the **weak-field solar redshift**, **GPS time dilation**, the
**first-order agreement with GR**, the **surviving G_035 temporal sector**, **no conformal-optics argument** and **no
discarded spatial-sector derivation**. Then:

1. which parts of **g00 = -exp(2x)** are actually forced;
2. which alternative clock laws remain mathematically viable;
3. the **maximal family g00 = -F(x)** satisfying all of them;
4. classify every part as **uniquely forced / weakly constrained / completely free**;
5. the predicted **surface redshift of J0740+6620** for every surviving F(x);
6. the **spread** of those predictions;
7. whether **AT presently makes a unique observable prediction**.

Preference stated in the question: **refutation**.

## The answer

> **REFUTED - THE SURVIVING CONSTRAINT SET DOES NOT FORCE A UNIQUE CLOCK LAW, AND THE PART OF g00 = -exp(2x) THAT IT
> DOES FORCE IS EXACTLY THE PART AT SHARES WITH GR.** The forced part is a **form and two numbers**; the maximal family
> is **infinite-dimensional**; its J0740+6620 prediction is **unbounded** (measured from **1.142288** to **2.266E+013**
> between two viable witnesses, bounded below and not above); and the family **contains GR's own clock law**, so the
> surviving sector cannot even separate the two theories the G_068-G_072 programme was built to decide between.

## 1. What is forced: a form and two numbers

| feature | laws with it | status | why |
|---|---|---|---|
| **g00 = -F(x)**, F positive and monotone in the clock potential alone - the **form** | 9 of 9 | **UNIQUELY FORCED** | G_035's arity proof: no temporal observable can be *called* with a spatial quantity. **Not a measurement** - it is the surviving temporal sector's structure, and the audit labels it so |
| **F(0) = 1** - the vacuum normalisation | 9 of 9 | **UNIQUELY FORCED** | the clock at unit occupancy is the reference clock |
| **F'(0) = 2** - the rate's 1 + x | 9 of 9 | **UNIQUELY FORCED** | the weak-field solar redshift, the GPS correction and the first-order agreement with GR are **all this one number** |
| **F''(0) = 4** (the redshift quadratic **0.5**) | 3 of 9 | **WEAKLY CONSTRAINED, by a projection and not by data** | shared by the exponential and **both Pade forms** while GR does not have it - so the count alone pins nothing. No realised measurement reaches it |
| **F'''(0) = 4/3** | 2 of 9 | **WEAKLY CONSTRAINED, by a projection** | the free-room ladder exhibits viable laws that differ exactly here |
| **multiplicativity F(a+b) = F(a)F(b)** | **1 of 9** | **COMPLETELY FREE** | **not in the surviving list**: it is the structural property that singles the exponential out, and the audit states that it is absent rather than importing it |

**The classification of the orders is computed from the reach tables, not typed:** of the seven orders the audit
classifies, **2 are UNIQUELY FORCED (orders 0 and 1), 3 are WEAKLY CONSTRAINED (orders 2, 3, 4 - and only by the
programme's projections) and 2 are COMPLETELY FREE (orders 5 and 6)**.

## 2. The reach of each surviving constraint - the measurement that decides the verdict

A measurement of **1 + z** to relative precision eps at a potential x resolves the **k**-th term of the series exactly
when **|x|^(k-1) > eps**. So a constraint whose precision exceeds the term it would have to detect cannot pin that
order, however strong the constraint sounds:

| realised constraint | x | precision | **orders beyond the first** | bound on \|delta b2\| |
|---|---|---|---|---|
| the **weak-field solar redshift** | **-2.123E-006** | **1.000E-002** | **0** | **9.420E+003** |
| **GPS time dilation** | **-6.961E-010** | **2.000E-003** | **0** | **5.746E+006** |
| first-order agreement with GR | 0 | - | 0 | unbounded (a constraint at order one by definition) |

**Neither realised constraint sees past the first order.** The AT-vs-GR difference at the second order is
**|F''(0)_AT - F''(0)_GR| = |4 - 0| = 4**, so **the solar redshift would have to be measured about 9 420 times more
precisely in the coefficient - 4 711 times in F''(0) - to see the difference at all**, and the GPS row is weaker still
by a factor of 610.

| programme row (G_072) | x | precision | orders beyond the first | status |
|---|---|---|---|---|
| J0740+6620, 20 per cent timing (class A) | -0.247001 | 0.20 | **1** | **a PROJECTION, not a measurement** |
| J0740+6620, direct surface redshift (class E) | -0.247001 | 0.01 | **3** | **a PROJECTION too** |

**The compact object is the only row that reaches the second order at all, and it is the target of the measuring
programme rather than an input to it.** That is the whole of the surviving sector's second-order content: nothing.

## 3. The maximal family, and the free room that never closes

> **g00 = -F(x) with F(x) = 1 + 2x + x^2 G(x) for an ARBITRARY G with F > 0 and F' > 0 on the physical range.**

Two numbers are pinned and a whole function is free. The audit exhibits the free room **order by order**: for every
order k the two laws **exp(2x)** and **exp(2x + 0.5 x^k)** are **both viable** and **identical below order k** while
**differing at order k** - measured with the repo's central-difference stencil plus Richardson extrapolation, so
"identical" means the measurement cannot separate them, not that a table says so.

**The physical range is OPEN at its left end, and the audit says why rather than losing a law to arithmetic:** GR's own
clock law **F = 1 + 2x reaches zero at x = -1/2** - that is its horizon - so a grid that touched the endpoint would
call the comparison theory non-positive and the comparison would be vacuous.

## 4. The prediction at J0740+6620, for every law the audit carries

The target is **J0740+6620 (Riley 2021)** at **x = -0.247001**, whose compactness the decision audits recompute from
its published mass and radius.

| law | 1 + z | shift from AT |
|---|---|---|
| **exp(2x) - the recorded AT law** | **1.280180559309** | - |
| **1 + 2x - GR's own clock law** | **1.405807032086** | **+1.256E-001** |
| (1+x)^2 (G_074's logarithmic law) | 1.328023241157 | +4.784E-002 |
| 1/(1-2x) | 1.222293851693 | -5.789E-002 |
| Pade [1/1] | 1.286874695654 | +6.694E-003 |
| Pade [2/2] | 1.280154024836 | -2.653E-005 |
| exp(2x - 2x^2) | 1.360715532373 | +8.053E-002 |
| exp(2x - 100x^2) | **2.704E+001** | +2.576E+001 |
| flat-then-rising (m = 3) | **1.142287863594** | -1.379E-001 |

**All nine are viable under one criterion, measured identically for each:** positive on the interval's interior,
**non-decreasing to within double resolution**, F(0) = 1, and the pinned slope held to **the stencil's own measured
refinement drift** - printed beside every law, because a single typed 1e-6 could never be met by a law with a large
fifth derivative. The stiffest witness carries a measured slope of **1.999998** with a drift of **1.44E-006**, and the
audit reports that rather than quietly widening a tolerance.

**The classical values reproduce the earlier audits exactly:** (1+x)^2 **1.328023241157**, 1/(1-2x)
**1.222293851693**, Pade [1/1] **1.286874695654** and Pade [2/2] **1.280154024836** against G_073's recorded values, and
the exponential's **1.280180559309** and GR's **1.405807032086** against the decision audits'.

## 5. The spread: unbounded, measured with two witness families

| witness | parameter | 1 + z | log(1+z) | viable |
|---|---|---|---|---|
| flat-then-rising | m = 1 | 1.261869E+000 | 0.232594 | yes |
| flat-then-rising | m = 2 | 1.186906E+000 | 0.171350 | yes |
| **flat-then-rising** | **m = 3** | **1.142288E+000** | **0.133033** | **yes** |
| exp(2x + c x^2) | c = 0 | 1.280181E+000 | 0.247001 | yes |
| exp(2x + c x^2) | c = -10 | 1.736808E+000 | 0.552049 | yes |
| exp(2x + c x^2) | c = -100 | 2.704438E+001 | 3.297479 | yes |
| **exp(2x + c x^2)** | **c = -1000** | **2.266358E+013** | **30.751780** | **yes** |

**bounded below: True. bounded above: False. bounded: False.** The family **brackets GR's prediction from both
sides**, and the two families are the two directions of the same freedom: the suppression witness flattens the rate so
that the deep field never leaves the reference clock, and the free-room witness bends it so that the deep field
departs arbitrarily far.

**The finite comparison is already wider than the effect it was built to measure:** the **six** laws the earlier audits
name span **1.835E-001** at the target - **1.46x** the recorded AT-vs-GR separation of **1.256E-001**. (G_073 recorded
**1.057E-001** over its own five, which is the same measurement without GR's law in the set; the audit reports both
rather than reusing their number.)

## 6. The decisive row: the family contains GR

**GR's clock law F = 1 + 2x is a member of the family** - it is positive, monotone, carries F(0) = 1 and F'(0) = 2, and
**predicts 1.405807032086 where AT predicts 1.280180559309**. So:

- **the forced set is satisfied by a law that is not AT's** - measured, not argued;
- **the surviving sector cannot separate AT from GR**, which is what the decision programme G_068-G_072 was built to
  do: the separation it measures is a separation between **two members of one family**, admitted equally by every
  surviving constraint;
- and **the AT-below-GR ordering** is not a consequence of the surviving sector either, since the family also contains
  members **above** GR's value.

## 7. What would restore uniqueness, priced

**One structural constraint.** Multiplicativity **F(a+b) = F(a)F(b)** admits **exactly 1** of the 9 laws with the
pinned data - the exponential - measured as a composition residual of **4.441E-016** for exp(2x) against **8.400E-001**
(1 + 2x), **5.439E-001** ((1+x)^2), an infinity (1/(1-2x), through its pole), **2.000E+000** (Pade [1/1]),
**4.633E-002** (Pade [2/2]) and **3.960E-002** (exp(2x - 100x^2)).

**And that is exactly the constraint G_073 found ABSENT from the surviving list.** So the uniqueness of AT's time
prediction is **conditional on the density-to-potential map** - the input G_073 and G_074 each identified - and under
the surviving constraint list the condition is **not met**.

## 8. What this does not do

- It **does not touch the density era**, and it **does not claim the exponential is arbitrary**. With the recorded
  second-order constraint restored - **G_019/G_020's redshift quadratic**, which is a **theory-internal derivation
  rather than a measurement** - G_073's ladder applies again and returns its **BOUNDARY**.
- It **compares laws, not derivations**: it does not re-derive any member's map from a first principle, because the
  surviving audits supply none, which is itself the result.
- It **does not reclassify G_073 or G_074**, whose own verdicts are conditional on their own constraint lists. What
  G_075 measures is **the difference between those lists**: G_073's uniqueness rests on a second-order input that no
  measurement in the surviving list supplies.

## 9. Defects in the audit's own first version

1. **The physical range was CLOSED at x = -1/2, and that made GR's clock law non-viable** - F reaches zero exactly
   there, because that IS its horizon. The comparison theory was excluded by an endpoint convention rather than by a
   measurement, and the whole comparison would have been vacuous. The range is now open, and the audit states why.
2. **The pinned slope was held to a typed 1e-6 while viability was held to a measured drift**, so a witness passed
   viability and was then uncounted in the feature table (**8 of 9**). One criterion now feeds both, and the
   inconsistency is recorded rather than smoothed over.
3. **The stencil's Richardson step divided by h a second time**, giving a refinement drift of **466.7** for a linear
   law whose true drift is **2.2E-014**.
4. **The second-order comparison mixed two languages**: it differenced the coefficients **b2** (2 against 0) where the
   forced-set difference is **F''(0) = 2 b2** (4 against 0), a factor of two.
5. **The suppression witness was first run at m = 50 and m = 1000**, where the rate's slope over the left of the range
   is exponentially small and **successive samples of F round to the same double** - so the monotonicity test called a
   monotone law non-monotone. The witnesses are now reported at m = 1, 2 and 3, and the **m -> infinity limit is
   stated as a limit** rather than quoted as a number the grid cannot see.

## Where it stands

**The surviving constraint set buys one thing: the first order.** It fixes the form of the metric's time part, the
value of the clock at the vacuum and the rate's slope there - and every one of those is shared with GR, because they
**are** the weak field. Everything the AT time sector is *distinctive* for - the second order and above, the compact
redshift, the AT-below-GR ordering, the very decalability of the AT-vs-GR split - **lies in the free room**.

**The honest reading is not that the exponential is wrong.** It is that **under the surviving constraint list the
theory's time prediction is an INPUT rather than a prediction**: its uniqueness is purchased by the density-to-potential
map, and the audit measures both the price of that purchase (one structural assumption, multiplicativity) and the
consequence of declining it (an infinite family whose J0740+6620 prediction is unbounded and which contains GR).

**The next question this leaves is G_075's own**: **what fixes the map?** The audit shows that the answer cannot come
from the weak field - no solar-system measurement reaches the second order - so it must come either from a structural
principle (multiplicativity, if the clock composition can be derived rather than assumed) or from the
**compact-object measurement G_072 describes**, which is the only surviving row that reaches the second order at all.
