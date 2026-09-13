# ResearchY-G_043 — Minimality Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_043 (permanent)
**Title:** Why does nature stop at the first working dimension (d = 3) instead of continuing to d = 4, 5, …?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_043.md`
**Depends on:** G_041 (d = 2 fails like the ring; the cube is the minimal working dimension), G_042 (the selectors of d = 3 reduce to ONE root equation), G_040 (the centralizer dimension of the ring is 49 — reproduced here as a cross-check), G_033 (the substrate requirement), G_016b (the clock law ρ^(1/d))
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_043_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/MinimalityAudit.cs`

## The question

Why does nature stop at the first working dimension (d = 3) instead of continuing to d = 4, 5, …? Is there a
quantity that is **optimal only at d = 3**?

**Compared:** D96³, D96⁴, D96⁵ (and the rest of the ladder).
**Measured:** photon polarisations, graviton polarisations, Hodge mismatch, representation growth, state-space
growth, clock-law scaling — plus two derived observability measures and one count bound.

## The answer: BOUNDARY — the outcome is **over-determined**, the mechanism is **undetermined**

## 1. The measured families across the ladder

| family | d=2 | d=3 | d=4 | d=5 | d=6 | profile |
|---|---|---|---|---|---|---|
| photon polarisations d−1 | 1 | 2 | 3 | 4 | 5 | MONOTONE UP |
| graviton polarisations (d+1)(d−2)/2 | 0 | **2** | 5 | 9 | 14 | MONOTONE UP |
| **Hodge mismatch \|dim Λ² − dim V\|** | 1 | **0** | 2 | 5 | 9 | **UNIQUELY ZERO AT d = 3** |
| representation growth max irrep dim | 2 | 3 | 8 | 20 | 80 | MONOTONE UP |
| state-space growth 96^d − 1 | 9 215 | 884 735 | 84 934 655 | 8.15e9 | 7.83e11 | MONOTONE UP |
| clock-law scaling ρ^(1/d) (s/day) | 129 416 | 86 277 | 64 708 | 51 766 | 43 139 | MONOTONE DOWN |
| observability fraction orbitals/state | 0.133 | 0.0235 | 0.00319 | 3.5e−4 | 3.3e−5 | MONOTONE DOWN |
| states per observable | 7.52 | 42.5 | 313.7 | 2 841 | 30 308 | MONOTONE UP |
| graviton fits within dimensions | yes | **yes** | no | no | no | THRESHOLD through d = 3 |

**Exactly one family is uniquely zero at d = 3**, and it is the quantity G_042 already identified as the root.

## 2. Every growth family is monotone — so the stop is *not* an economy optimum

Photon counts, graviton counts, representation growth (2, 3, 8, 20, 80), the state space (96^d − 1), the fraction
of it a measurement can name (0.516, 0.133, 0.0235, 0.00319) and the number of states hiding behind one
observable (1.94, 7.52, 42.5, 314, 2841) **all get worse as d grows**.

> **An economy optimum would stop at d = 1 or 2 — and those do not work.** So "nature stops at 3" cannot be an
> economy argument, and the growth families contain **no optimum at all**.

## 3. The one other extremal quantity is the *same family*

The graviton's polarisations fit within the number of directions for d ≤ 3 (`(d+1)(d−2)/2 ≤ d`), so **3 is the
largest admissible dimension** — a count *bound*, not an equality. But it is the same quadratic family at a
different offset:

| member | condition | root behaviour |
|---|---|---|
| the Hodge equality | `d² − 3d = 0` | zero at d = 3 |
| the count bound | `d² − 3d − 2 ≤ 0` | holds through d = 3 |

**One family, two members, no second mechanism.**

## 4. The obstruction — five criteria turn over at the same dimension

| criterion | holds at | direction |
|---|---|---|
| A dimension-3 irrep is supplied | {3 … 8} | **turns ON at d = 3** |
| The graviton count is positive | {3 … 8} | **turns ON at d = 3** |
| The Hodge mismatch vanishes | {3} | **turns ON at d = 3** |
| The graviton fits within the dimensions | {1, 2, 3} | **turns OFF after d = 3** |
| The vector is the largest irrep | {1, 2, 3} | **turns OFF after d = 3** |

**Their intersection is exactly {3}.** So *"the first dimension that works"* and *"the only dimension where the
mismatch vanishes"* are the **same dimension**, and **no measured quantity can separate the two explanations**.
The audit can say with confidence *why* each candidate fails at d = 4 — but not *which candidate is doing the
work*, because they all fail at the same place for the same reason.

**That is the finding: the outcome is over-determined, the mechanism is not.**

## 5. But the outcome is *robust*, not knife-edge

The whole one-parameter family `d² − 3d − c ≤ 0` separates d = 3 from d = 4 for **c = 0, 1, 2, 3** — a window
containing both selected members (the equality at c = 0 and the bound at c = 2). So d = 4 is excluded by a
**family** of conditions, not by a single tuned coincidence.

## 6. Cross-audit check

The number of **orbitals** of the D96^d lattice's symmetry — what a substrate-constructed measurement can name
(G_040) — is `C(48 + d, d)`, the pair invariant being the multiset of per-axis distances. At **d = 1 this gives
49**, exactly the centralizer dimension G_040 computed for the ring from the dihedral group. Two different
routes to the same integer, from two different audits.

| d | orbitals | state space | observability | states per observable |
|---|---|---|---|---|
| 1 | **49** | 95 | 0.516 | 1.94 |
| 3 | 20 825 | 884 735 | 0.0235 | 42.5 |
| 4 | 270 725 | 84 934 655 | 0.00319 | 313.7 |
| 5 | 2 869 685 | 8.15e9 | 3.5e−4 | 2 841 |

## 7. Caveats

**(a) "Optimal only at d = 3" is true exactly once, and it is not new.** The Hodge mismatch is the only measured
family with a unique optimum at d = 3, and it is G_042's root. The audit therefore **refutes** the suggestion
that the growth families contain an independent optimum.

**(b) Monotone ⟹ no optimum *on the ladder*.** A monotone family can still have an optimum at a *boundary* of
the ladder (d = 1 or the large-d end). The audit's claim is that no growth family has an interior optimum at
d = 3, which is what "optimal only at d = 3" requires.

**(c) The orbital formula is the pair-invariant count**, verified against G_040's independent computation at
d = 1 rather than derived twice. For d ≥ 2 it uses the same multiset argument; the audit states that as its
assumption 2.

**(d) The clock-law family is monotone by construction** (1/d), so it cannot select; its role — established in
G_041/G_042 — is to make the dimension *observable*, not to pick it.

**(e) The mechanism question may be unanswerable in principle** rather than merely unanswered here: minimality
and optimality coincide *because* the Hodge defect vanishes precisely where the sector first becomes available.
That coincidence is a feature of the same algebra, so no measurement of these families can break it.

## 8. Classification

**Registry:** added to the G_035 classification registry as **`ClockOnly`** → **SURVIVES** (counts, growth laws
and the g₀₀ clock law), triaged `ScanDetectsIt: false`. Counts become **30 / 11 / 3 of 44**; the boundary index
is unchanged; no prior classification changed.

**No verdict changed.** G_041 (BOUNDARY), G_042 (REDUNDANT: one root) and G_033 are unchanged inputs; the
G_042 root is *reproduced* here rather than extended. The D_040 registry is untouched; no canonical claim, value
or equation changes; no new primitive is added.

**Scanner side-effect, recorded:** G_033's live classifier now reads **26** substrate suites (42 classified in
total).
