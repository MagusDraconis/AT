# Y_G_066 - Result

**Audit:** ResearchY-G_066 - Physical Flow Audit
**Verdict:** **REFUTED**
**Tests:** 7/7 PASSED

## Answer

**Phase-freeness is a property of the DISSIPATIVE flows, not of AT.** Four of the five forms are admissible, the
admissible set contains flows with **opposite** long-run behaviour, and the **unitary** form - which keeps the laws,
conserves the total and fixes the uniform state - **preserves the phase content indefinitely**.

## Measurements

| flow | admissible | deviation ratio | phase ratio | long run |
|---|---|---|---|---|
| forward difference | True | **0.381** | **0.253** | DECAYS TO UNIFORM |
| exact flow `exp(εD)` | True | 0.381 | 0.253 | DECAYS TO UNIFORM |
| centred (skew) difference | True | 1.001 | **0.800** | PRESERVES THE STATE |
| **unitary (Cayley)** | **True** | **1.000** | **0.682** | **PRESERVES THE STATE** |
| backward difference | **False** | 1.058E+003 | 8.547E+002 | **LEAVES THE SIMPLEX** |

| measurement | value |
|---|---|
| admissible forms | **4** (all but the backward difference) |
| forms that erase the state | **2** (forward, exact flow) |
| forms that preserve the state | **2** (centred, unitary) |
| forms that preserve the **phase** | **2** (centred, unitary) |
| worst law residual, amplifying form | **1.000E+000** (cells negative) |
| worst law residual, admissible forms | **< 1E-13** |
| the uniform state stationary for every form | **True** |
| every form conserves the total | **True** (the amplifying one loses it to floating point at \|ρ\| ≈ 259) |

## Notes

**The dissipative forms erase everything, not just the phase:** the forward difference's deviation ratio is **0.381** while
its phase ratio is **0.253**, so the attractor is the **uniform state**. The two dissipative forms agree to three digits
(**0.3810 vs 0.3809**), confirming G_060's identification of the exact flow as the forward step's flow.

**A performance defect recorded.** The first version of this suite took **4m38s**: the exact flow and the unitary form
each evaluated **48 complex exponentials per step**, and the table was rebuilt on every call. Memoising the multiplier
(pure in `(update, channel, eps)`) and the table brought it to **19s**.
