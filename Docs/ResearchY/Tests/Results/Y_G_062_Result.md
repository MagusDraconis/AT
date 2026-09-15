# Y_G_062 - Result

**Audit:** ResearchY-G_062 - Mode Occupation Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

The occupancy is set by the **construction**: the canonical state takes **one seed per level** of **non-zero weight**, so
the count is `(levels with non-zero weight) − 1 = 43 − 1 = **42**`, and the empty channels are the ones the formula's
zeros and the degenerate levels leave untouched. **The eleven CAN be populated** - a state carrying every mode gives
**95 occupied, 0 empty, 11 of 11** - with **no change to any AT object**. What survives is an **algebraic floor of 47**.

## Measurements

| state | occupied | empty | empty channels | of the eleven | row rank | kernel |
|---|---|---|---|---|---|---|
| canonical | 42 | 53 | **14, 19, 24, 32, 40, 48** | 0 | **43** | **53** |
| alternative seed | 42 | 53 | 6 channels | 2 | 43 | 53 |
| full weight (all weights 1) | **44** | 51 | 4 channels | 2 | 45 | 51 |
| shifted weight formula w' | 42 | 53 | **6, 23, 24, 32, 40, 48** | 2 | 43 | 53 |
| full weight, alternative seed | 44 | 51 | 4 channels | 4 | 45 | 51 |
| **all modes occupied** | **95** | **0** | **none** | **11** | **49** | **47** |

| measurement | value |
|---|---|
| levels / non-zero weights / rule | **45 / 43 / 42** = measured 42 |
| canonical formula's zero-weight levels | **8, 31** → channels **14, 19** |
| shifted formula's zero-weight levels | **6, 29** → channels **6, 23** |
| distance classes (the row-space bound) | **49** |
| smallest kernel reached / canonical | **47 / 53** (excess **6**) |
| degenerate levels (state-independent) | index **13** (λ **12.0**, mult **5**, channels 16, 32, 48); index **35** (λ **14.0**, mult **6**, channels 8, 24, 40) |
| Σ(m−1) | **51** |

## Notes

**The prompt's phrasing is corrected.** The canonical state occupies 42 **visible** modes and leaves the whole
**53-direction phase sector** empty; of those 53 the **eleven** are the ones in channels it does not occupy at all.

**A probe defect is recorded.** A first version of the all-modes state used a weight formula with its own zeros
(`((i·7) mod 11 − 5)/11`) and therefore left **eight** modes empty, which made the audit report that one of the eleven
could not be occupied by any state - false, and a defect in the **probe** rather than a finding.
