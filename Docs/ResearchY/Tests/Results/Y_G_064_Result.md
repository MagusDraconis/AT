# Y_G_064 - Result

**Audit:** ResearchY-G_064 - Canonical Recipe Origin Audit
**Verdict:** **DERIVED**
**Tests:** 6/6 PASSED

## Answer

The construction is **pinned**, and by a requirement its own comment does not name: **phase-freeness**. Every headline
count follows **arithmetically** from the recipe's zero count - **occupied = 44 − (zero-weight non-constant levels)** - and
the **seed choice alone** decides which sector the state occupies.

## Measurements

| formula | zeros (total / non-constant) | predicted | measured |
|---|---|---|---|
| ramp (no modulus) | 0 / 0 | 44 | 44 |
| mod-17 | 3 / 3 | 41 | 41 |
| **canonical mod-23** | **2 / 2** | **42** | **42** |
| mod-15 | 7 / 6 | 38 | 38 |
| mod-11 | 9 / 8 | 36 | 36 |

| seed | visible occupied | hidden occupied | occupied total | kernel |
|---|---|---|---|---|
| **basis[0]** | **42** | **0** | 42 | 53 |
| **basis[^1]** | **0** | **42** | 42 | 53 |

| requirement | holds (of 8 substances) | pins |
|---|---|---|
| **R3 phase-free** | **1** | **the canonical construction** |
| R4 hidden count = 47 | 1 | the **all-modes** construction, not the canonical one |
| R2 full visible sector | 3 | - |
| R1 generic (orbit ≥ 84 of 95) | 4 | - |
| R5 union of whole modes | 7 | - |

| measurement | value |
|---|---|
| canonical orbit / maximum orbit | **84 / 95** (the maximum is not phase-free) |
| the pinning requirement | **R3, phase-freeness** |
| G_040's doublet count vs the canonical kernel | **47 vs 53** - different quantities |

## Notes

**The named justification does not pin the recipe.** G_046's comment names *genericity*; that requirement is shared by 4
of the 8 substances and the canonical recipe's orbit is not the largest. The recipe **trades orbit size for
phase-freeness**, and that pair - fully occupying **and** phase-free - is exactly what the phase-sector programme needed.

**A defect recorded.** A first version of the count law read `44 − zeros` and was off by **one** on exactly the formulas
that zero the **constant** level: that level carries the simplex direction, which is not a mode, so zeroing it removes no
occupied mode.
