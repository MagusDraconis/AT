# ResearchY-G_067 - Flow Selection Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_067 (permanent)
**Title:** Can any existing AT quantity select between the dissipative and the unitary flows?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_067.md`
**Depends on:** G_060 (the multipliers, stability and ranks), G_065 (no law requires phase-freeness), G_066 (four of five forms are admissible and split two-and-two), G_063 (the free room is a substrate fact)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_067_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/FlowSelectionAudit.cs`

## The question

Can any **existing AT quantity** select between the **dissipative** and the **unitary** flows? Candidates: the **clock
law**, the **acceleration law**, the **field law**, **occupancy conservation**, the **phase sector** and the **free
room**. Measure whether any quantity **changes under admissible flow replacement**. Goal: the **first principle that
selects a physical flow**.

## The answer: **REFUTED - no existing AT quantity selects**

> **The flows are distinguishable by almost every quantity the theory names, and not one of those quantities is required
> by anything AT states. The one pattern that WOULD select - norm conservation - appears only in the audit's own control,
> and the entropy's monotonicity is a principle AT does not state.**

## 1. The candidates, measured over 4000 steps from the phase-bearing state

| id | quantity | start | dissipative | unitary | class |
|---|---|---|---|---|---|
| Q1 | **clock** contrast | 9.485E-002 | 3.754E-002 | 9.829E-002 | changes under both |
| Q2 | **acceleration** contrast | 1.399E-001 | 1.165E-002 | 1.450E-001 | changes under both |
| Q3 | **field** contrast | 2.745E-002 | 2.370E-003 | 2.891E-002 | changes under both |
| Q4 | **occupancy** total | 9.600E+001 | 9.600E+001 | 9.600E+001 | **invariant under both** |
| Q5 | **phase** norm | 1.048E+000 | 2.648E-001 | 7.143E-001 | changes under both |
| Q6 | **free room** | 5.100E+001 | 5.100E+001 | 5.100E+001 | **invariant under both** |
| Q7 | *control*: deviation from uniform | 1.048E+000 | 3.992E-001 | 1.048E+000 | **SELECTS** |
| Q8 | *control*: occupancy entropy | 9.987E-001 | 9.998E-001 | 9.987E-001 | changes under both |

**A quantity can only select in three ways:** invariant under both (it cannot select), changing under both (it
distinguishes them but states no preference), or **invariant under exactly one** - the only pattern a requirement could
turn into a selection. **Among the question's six candidates, none selects.** Two are invariant under both, four change
under both.

**The free room's invariance is not an accident of the flows** but a property of the **substrate** (G_063): it is
`96 − 45 = 51` for every state and every flow, so no state and no flow can ever move it.

## 2. The requirement test: the laws are identities in ρ

| flow | worst law residual | min cell | total residual |
|---|---|---|---|
| forward difference | 5.122E-014 | 8.333E-001 | 5.122E-014 |
| unitary (Cayley) | 5.773E-014 | 7.790E-001 | 5.773E-014 |

**Every law holds under both flows** - measured on the **evolved** states rather than cited from G_065. A law that holds
at every instant under **every** flow cannot distinguish the flows, let alone select between them: **the theory's own
content is blind to the replacement.**

## 3. Where the flows disagree most, and a correction to the audit's own expectation

**The difference's multiplier is complex**, so every step rotates visible content into hidden content - and **both**
flows create phase content from the phase-free state:

| steps | dissipative phase norm | unitary phase norm |
|---|---|---|
| 0 | 9.246E-015 | 9.246E-015 |
| 1000 | 3.033E-001 | 8.505E-001 |
| 4000 | **3.118E-001** | 7.560E-001 |
| 20000 | 1.863E-001 | 7.492E-001 |
| **50000** | **3.672E-002** | ~0.75 |

**So the dissipative flow does NOT keep the canonical state phase-free.** The audit set out expecting that it would, and
the measurement refused the expectation. What distinguishes the flows is **transience versus permanence**: the
dissipative flow's phase content **peaks near 3.12E-001 and then decays** (with the deviation falling too, so the
**attractor remains the uniform state**), while the unitary flow **holds its phase content at about 0.75 indefinitely**.
**G_065's attractor statement stands; its phrasing must not be read as phase-freeness being preserved.**

## 4. The near-misses, and why neither answers the question

1. **The selector pattern appears only in the control.** The deviation norm is **fixed by the unitary flow** and decays
   under the dissipative one - i.e. **norm conservation would select** - but the deviation is not one of the question's
   candidates, and AT states no conservation requirement of that kind.
2. **The entropy is an unmade principle.** The dissipative flow is **entropy-monotone** (0.99874 → 0.99982) and the
   unitary flow is **not** (0.99874 → 0.99874), so a **thermodynamic arrow would select dissipation** - and **AT states
   no such arrow**, while G_055 and E_016 measured the entropy-like quantities as sector-blind.

## Verdict

**REFUTED.** **No existing AT quantity selects between the dissipative and the unitary flow.** The flows *are*
distinguishable - by the clock, the acceleration, the field and the phase quantities, which all change under both - but
**nothing in AT prefers either**: the six candidates either change under both or are invariant under both, and the
**laws hold under both** because they are identities in ρ. What would select is a **monotonicity requirement** rather
than a conservation one, **stated about the state rather than derived from the laws** - and AT states none. **The
selection is available but unmade**, and that is recorded as the open question it is.
