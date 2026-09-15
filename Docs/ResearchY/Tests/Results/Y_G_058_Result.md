# Y_G_058 - Result

**Audit:** ResearchY-G_058 - Phase Dynamics Closure Audit
**Verdict:** **BOUNDARY**
**Tests:** 6/6 PASSED

## Answer

The question asked whether an AT update rule can reach **rank 53** on the phase sector. Measured, it is the **wrong
question**: the **linearisation** and **operator** ranks are **53 for every rule tested, the do-nothing identity
included**. The ranks that separate rules are the **push rank** (**1** for any rule that moves, **0** for the process AT
runs) and the presence of a generator at all.

## Measurements

| rule | push rank | linearisation | operator |
|---|---|---|---|
| single scalar flow | 1 | 53 | 53 |
| multiple coupled scalar flows | 1 | 53 | 53 |
| vector-valued flow | 1 | 53 | 53 |
| connection-driven flow | 1 | 53 | 53 |
| T1/T2-coupled flow | 1 | 53 | 53 |
| identity update (CONTROL) | **0** | **0** | **53** |

| measurement | value |
|---|---|
| phase dimensions | **53** |
| every generator linearises full | **True** |
| every operator rank full (control included) | **True** |
| largest push rank | **1** |
| coupling scalars raises the push rank? | **no** |
| actualization spatial part / census / push rank | **0.000E+000 / 0 / 0** |
| vector-valued vs scalar distinguishable by any rank? | **no** |

## Note

**An expectation is withdrawn.** The premise of a hierarchy (vector generators at 53, scalar rules at 1) is refuted: no
rank measure separates them, because a scalar flow's linearisation is its potential's **Hessian** - positive definite,
hence full rank on every subspace - and every state update contains the **identity**, invertible on the phase sector.
