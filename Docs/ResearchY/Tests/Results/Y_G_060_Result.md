# Y_G_060 - Result

**Audit:** ResearchY-G_060 - Phase Evolution Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

The promotion exists, and it is the **norm-preserving** form: the exact flow of the **centred** difference sustains all
**42** reachable phase directions with `|μ| = 1` **exactly**. The update the question names - `ρ + εDρ` - is the one
that fails: it is **stable because it is dissipative**, and its attractor is the **phase-free** uniform state.

## Measurements

| update | \|μ\| at ε = 1E-3 | reachable phase | by Krylov | amplitude | decay / sustain / grow at ε = 1E-3 | off-simplex |
|---|---|---|---|---|---|---|
| forward difference | 0.998000 … 0.999998 | 42 | 42 | 42 | 42 / 0 / 0 | > 4000 |
| backward difference | 1.000002 … 1.002000 | 42 | 42 | 42 | 0 / 0 / 42 | 796 |
| centred (skew) difference | 1.000000 … 1.000001 | 42 | 42 | 42 | 0 / 0 / 42 | > 4000 |
| exact flow exp(εD) | 0.998002 … 0.999998 | 42 | 42 | 42 | 42 / 0 / 0 | > 4000 |
| **unitary (Cayley)** | **1.000000 … 1.000000** | **42** | **42** | **42** | **0 / 42 / 0** | > 4000 |
| positivity-clipped | 0.998000 … 0.999998 | 42 | 42 | 42 | 42 / 0 / 0 | > 4000 |
| actualization (CONTROL) | 1.000000 | 0 | 0 | 42 | 0 / 0 / 0 | > 4000 |

| measurement | value |
|---|---|
| the eleven unreachable directions | channels **14, 19, 24, 32, 40** (both quadratures) + the **alternating mode** |
| forward step's deviation norm, 20 000 steps | **1.005E+000 → 2.424E-001** (attractor: the phase-free state) |
| unitary form's deviation norm | **exactly conserved** (1.005011E+000 at 100 / 1000 / 20 000 steps) |
| unitary form's minimum cell over 20 000 steps | **0.693** … **0.770** (every cell positive) |
| fixed-point dimensions | **1** (constant) / **2** (constant + alternating mode) / **96** (control) |
| the positivity guard fires? | **never** (agreement with the unclipped step to 5E-15) |
| the difference-of-orbits leak (measured) | **2.329E-008** relative, into the unreachable directions |

## Notes

**A claim is withdrawn.** The draft asserted the phase content decays monotonically under the forward difference.
Measured, it **oscillates** (0.065 at 100 steps, **0.303** at 1000) while the envelope falls - so the deviation norm is
the envelope and the phase content is the oscillation.

**Three defects are recorded rather than hidden:** the positivity guard divided by a near-zero mean; the rank scan
counted **63** and then **54** directions inside a **53**-dimensional space; and the difference-of-orbits construction
carries the measured 2.329E-008 leak. The audit now measures reachability from the **linear** response, in the phase
sector's own coordinates.
