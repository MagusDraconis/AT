# Y_G_043 — Minimality Audit — Result

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_043_Tests.cs` — **8/8 PASSED**
**Core:** `AT.Core/ResearchXH/MinimalityAudit.cs`
**Doc:** `Docs/ResearchY/G_GravitySource/ResearchY-G_043.md`

## Question

Why does nature stop at the first working dimension (d = 3) instead of continuing to d = 4, 5, …? Is there a
quantity **optimal only at d = 3**?

## Answer: **BOUNDARY — the outcome is over-determined, the mechanism is undetermined**

## The measured families

| family | d=2 | d=3 | d=4 | d=5 | d=6 | profile |
|---|---|---|---|---|---|---|
| photon polarisations | 1 | 2 | 3 | 4 | 5 | MONOTONE ↑ |
| graviton polarisations | 0 | **2** | 5 | 9 | 14 | MONOTONE ↑ |
| **Hodge mismatch** | 1 | **0** | 2 | 5 | 9 | **UNIQUELY ZERO AT d = 3** |
| representation growth | 2 | 3 | 8 | 20 | 80 | MONOTONE ↑ |
| state space 96^d − 1 | 9 215 | 884 735 | 84 934 655 | 8.15e9 | 7.83e11 | MONOTONE ↑ |
| clock scaling ρ^(1/d) | 129 416 | 86 277 | 64 708 | 51 766 | 43 139 | MONOTONE ↓ |
| observability fraction | 0.133 | 0.0235 | 0.00319 | 3.5e−4 | 3.3e−5 | MONOTONE ↓ |
| states per observable | 7.52 | 42.5 | 313.7 | 2 841 | 30 308 | MONOTONE ↑ |
| graviton fits in dimensions | yes | **yes** | no | no | no | THRESHOLD through d = 3 |

**Seven of nine families are monotone** (no optimum). **Exactly one is uniquely zero at d = 3** — the Hodge
mismatch, which is G_042's root.

## The two derived findings

**(1) The stop is not an economy optimum.** Every growth family favours *smaller* d — observability *falls*
(0.516 → 0.0235 → 0.00319) and states-per-observable *rises* (1.94 → 42.5 → 314). An economy principle would
stop at d = 1 or 2, which do not work.

**(2) The one other extremal quantity is the same family.** The graviton count bound `(d+1)(d−2)/2 ≤ d` holds
through d = 3, i.e. `d² − 3d − 2 ≤ 0` — the same quadratic as the Hodge equality `d² − 3d = 0` at a different
offset. One family, two members.

## The obstruction

| criterion | holds at | direction |
|---|---|---|
| a dimension-3 irrep is supplied | {3 … 8} | turns **ON** at 3 |
| the graviton count is positive | {3 … 8} | turns **ON** at 3 |
| the Hodge mismatch vanishes | {3} | turns **ON** at 3 |
| the graviton fits within the dimensions | {1, 2, 3} | turns **OFF** after 3 |
| the vector is the largest irrep | {1, 2, 3} | turns **OFF** after 3 |

Intersection **{3}** → *"first dimension that works"* and *"only dimension where the mismatch vanishes"* are the
same dimension, so no measured quantity can separate the mechanisms. **The outcome is over-determined; the
mechanism is not.**

## Robustness

The whole family `d² − 3d − c` separates 3 from 4 for **c = 0, 1, 2, 3**, containing both selected members
(equality c = 0, bound c = 2). **d = 4 is excluded by a family of conditions, not one tuned coincidence.**

## Cross-audit check

Orbitals of D96^d = `C(48 + d, d)` → **49 at d = 1**, exactly G_040's centralizer dimension for the ring,
computed there from the dihedral group by different means.

## Registry

`ClockOnly` → **SURVIVES**, `ScanDetectsIt: false`. Counts become **30 / 11 / 3 of 44**; boundary index
unchanged; no prior classification changed. G_033's live classifier now reads **25** substrate suites (42
classified in total).

## Caveats

* "Optimal only at d = 3" is true exactly once and is **not new** — it is G_042's root. The suggestion that the
  growth families carry an independent optimum is **refuted**.
* Monotone ⟹ no *interior* optimum; a monotone family may still be extremal at a ladder boundary.
* The orbital formula is the pair-invariant count, verified against G_040 at d = 1 and otherwise assumed.
* The mechanism question may be unanswerable **in principle**: minimality and optimality coincide *because* the
  Hodge defect vanishes exactly where the sector first becomes available — a feature of the same algebra.
