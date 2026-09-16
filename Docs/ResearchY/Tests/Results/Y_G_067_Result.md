# Y_G_067 - Result

**Audit:** ResearchY-G_067 - Flow Selection Audit
**Verdict:** **REFUTED**
**Tests:** 7/7 PASSED

## Answer

**No existing AT quantity selects between the dissipative and the unitary flow.** Among the question's six candidates,
**two are invariant under both** flows (occupancy total, free room) and **four change under both** (clock, acceleration,
field, phase) - so **none selects** - and the **laws hold under both** because they are **identities in ρ**.

## Measurements

| id | quantity | start | dissipative | unitary | class |
|---|---|---|---|---|---|
| Q1 | clock contrast | 9.485E-002 | 3.754E-002 | 9.829E-002 | changes under both |
| Q2 | acceleration contrast | 1.399E-001 | 1.165E-002 | 1.450E-001 | changes under both |
| Q3 | field contrast | 2.745E-002 | 2.370E-003 | 2.891E-002 | changes under both |
| Q4 | occupancy total | 9.600E+001 | 9.600E+001 | 9.600E+001 | invariant under both |
| Q5 | phase norm | 1.048E+000 | 2.648E-001 | 7.143E-001 | changes under both |
| Q6 | free room | 5.100E+001 | 5.100E+001 | 5.100E+001 | invariant under both |
| Q7 | *control* deviation | 1.048E+000 | 3.992E-001 | 1.048E+000 | **SELECTS** |
| Q8 | *control* entropy | 9.987E-001 | 9.998E-001 | 9.987E-001 | changes under both |

| measurement | value |
|---|---|
| selectors among the question's candidates | **none** |
| free room, every state and every flow | **51** (= 96 − 45, a substrate fact) |
| worst law residual under each flow | **5.122E-014 / 5.773E-014** |
| the selector pattern | **only in the control** (Q7, norm conservation) |
| entropy: dissipative / unitary at 4000 steps | **0.99982 / 0.99874** |
| the dissipative flow entropy-monotone | **True**; the unitary one **not** |

## Notes

**A correction to the audit's own expectation, and a refinement of G_065.** The audit expected the dissipative flow to
keep the canonical state phase-free. **It does not**: the difference's multiplier is **complex**, so every step rotates
visible content into hidden, and the phase norm rises from 9.246E-015 to a **peak near 3.12E-001** before decaying to
**3.672E-002 at 50 000 steps** (with the deviation falling too). So the **attractor is still the uniform state**, but
**G_065's phrasing must not be read as phase-freeness being preserved**. What distinguishes the flows is **transience
versus permanence**: the unitary flow holds its phase content at about **0.75** indefinitely.

**The selection is available but unmade.** Norm conservation would select the unitary flow and entropy monotonicity
would select the dissipative one - and AT states neither. What could select, the audit notes, is a **monotonicity
requirement about the state**, not a conservation requirement derived from the laws.
