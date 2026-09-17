# Y_G_073 - Result

**Audit:** ResearchY-G_073 - Exponential Uniqueness Audit
**Verdict:** **BOUNDARY** (unique conditionally, not mathematically)
**Tests:** 6/6 PASSED

## Answer

**The surviving constraints select the exponential among the NAMED alternatives and cannot select it against the
PADE family.** Order 1: 5 of 5 survive. Order 2: 3 of 5. Order 3: 2 of 5. **The ladder never reaches one.**

## The constraint ladder

| candidate | x^1 | x^2 | x^3 | beta |
|---|---|---|---|---|
| **exp(2x)** | 2 | **2** | 1.333333 | **1.0** |
| **(1+x)^2** | 2 | **1** | 0 | **0.5** |
| **1/(1-2x)** | 2 | **4** | 7.999998 | **2.0** |
| **Pade [1/1]** | 2 | **2** | **2** | **1.0** |
| **Pade [2/2]** | 2 | **2** | 1.333333 | **1.0** |

| constraints to order | survivors | which |
|---|---|---|
| 1 (Newtonian limit) | **5** | all - buys nothing |
| 2 (PPN beta = +1) | **3** | exp(2x), Pade [1/1], Pade [2/2] |
| 3 | **2** | exp(2x), Pade [2/2] |

## The structural constraint

| candidate | composition residual |
|---|---|
| **exp(2x)** | **4.441E-016** |
| (1+x)^2 | 5.439E-001 |
| 1/(1-2x) | **pole at x = 1/2** |
| Pade [1/1] | 2.000E+000 |
| Pade [2/2] | 4.633E-002 |

**The multiplicativity row's source is NOT in the surviving list**, and the constraint table says so.

## The exponential is the clock law

With the conformal metric g00 = -rho^(2/d) and **x = (1/d) ln rho**, g00 = exp(2x) is the **same statement** as the
clock rate **rho^(1/d)** - measured residual **0.0E+000** (below 1E-16). **The exponential is a restatement of the
clock law, not a consequence of the metric ansatz.**

## Where the discrimination lives

| regime | x | spread | widest |
|---|---|---|---|
| the solar weak field | -2.123047E-006 | **6.761E-012** | (1+x)^2 |
| the compact object | -2.470011E-001 | **1.057E-001** | (1+x)^2 |

**Eight orders of magnitude apart.** And the G_068 AT-below-GR ordering **excludes nothing** (exp 1.280180559309,
(1+x)^2 1.328023241157, 1/(1-2x) 1.222293851693, [1/1] 1.286874695654, [2/2] 1.280154024836, GR 1.405807032087).

## Notes - five defects in the audit's own first version

1. **The stencil used the backward-difference sign convention**: every odd coefficient came back **negative** and the
   exponential appeared to fail its own first order.
2. **A single step size left an O(h^2) truncation of 1E-4** in the third coefficient, above the 1E-6 comparisons use.
   Richardson extrapolation now applied.
3. **The same defect, unfixed on the second route, was the audit's worst symptom**: the redshift quadratic's ~4E-6
   truncation **excluded the exponential from the very row that pins it**. That row now uses the extrapolated value.
4. **The redshift-quadratic row used the g00 coefficient as a proxy** (that is the PPN beta, not the redshift
   quadratic). It now differences **1 + z** itself.
5. **A bound of mine was wrong**: I asserted the solar spread is below **1E-12**; it is **6.761E-012**.

**Group-G count guards bumped:** Y_G_033 42 -> 43 and Y_G_035 73 -> 74 with survives 59 -> 60 (registry census and the
MinimalTimeSector view).
