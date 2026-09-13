# Y_E_015 - Result

**Audit:** ResearchY-E_015 - Sector Weight Audit
**Verdict:** **BOUNDARY** (over a derived flatness)
**Tests:** 7/7 PASSED

## Answer

The measure on sectors is **exactly flat**: `P(n) = 1/k` for every `n`. The sector is **completely free** - no
AT-derived quantity prefers `n = 0` or `|n| > 0`.

## Measurements

| quantity | value |
|---|---|
| bin counts at (k,l) = (4,6) | 1024, 1024, 1024, 1024 |
| multiplicity: (4,6) / (6,5) / (8,4) / (5,7) | 1024/4096, 1296/7776, 512/4096, 15625/78125 |
| P(n) / P(n)/P(m) | 0.250000000 / 1.000000 |
| shift moves every class by one | True (bijection) |
| entropy spread / free-room ratio | 0.000E+000 / 1.000000 |
| occupancy weight spread (control) | 0.000E+000 (1.000000 quanta) |
| update rule spatial / time-like | 0.000E+000 / 1.424E-002 |
| cycle holonomy phase distance, every n | 4.899E-016 |
| sector-blindness residual (E_014) | 4.163E-017 |

## Candidates

- occupancy measure - **REFUTED** (sector-blind)
- actualization count - **REFUTED** (no transition exists)
- entropy - **DERIVED** (the flat weight)
- free room - **DERIVED** (the same quantity)
- multiplicity structure - **DERIVED** (exactly k^(L-1) per sector, by bijection)
- sector topology - **REFUTED** (identical charge for every sector)

## Note

Entropy, free room and multiplicity structure are **one quantity under three names** - the number of configurations per
sector - and they assign a **uniform** weight. The ratio of weights is derived; the absolute normalisation is an input.
