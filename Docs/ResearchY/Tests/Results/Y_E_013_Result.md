# Y_E_013 - Result

**Audit:** ResearchY-E_013 - Flux Population Audit
**Verdict:** **BOUNDARY** (form of the mechanism: **DERIVED**)
**Tests:** 7/7 PASSED

## Answer

The **form** of any populating mechanism is derived: locality and E_012's constraint force a **balanced pair**. The
**population** is a boundary: no AT process performs it, nothing sizes it, and nothing times it.

## Measurements

| quantity | value |
|---|---|
| minimum plaquettes changed by any local move | 4 |
| local moves populating a single plaquette | 0 |
| signed sum of a local move's changes | 0.000E+000 |
| P(single fluxon) / P(balanced pair) | 0.0 / 1.0 |
| pair amplitude at L = 8 / 16 / 32 / 64 | 3.141593 (every size) |
| pure gauge field changes the pair's content by | 6.939E-017 |
| single member decayed alone | residual 3.141593 -> forbidden |
| pair annihilated | residual 0.000E+000 -> allowed |
| clock law sensitivity: flux / organisation | 0.000E+000 / 1.061E-001 |
| update rule sectors: max \|F_ij\| / max \|F_0i\| | 0.000E+000 / 1.424E-002 |
| AT members coupling spectral index to link phase | 0 (controls: 25 link, non-zero spectral) |
| occupancy scaling exponent | a^1.97 |

## Candidates

- occupancy rearrangement - **REFUTED** (scaling + exact gradient limit)
- **defect pairs - DERIVED** (the only locally reachable shape)
- boundary conditions - **BOUNDARY** (amount, initial pattern, lifetime all unselected)
- actualization transitions - **REFUTED** (purely electric update rule)
- spectral transitions - **REFUTED** (disjoint sectors, controls non-zero)

## Note

E_012 said nothing creates a pair. E_013 sharpens it: the single fluxon is not merely forbidden but **unreachable by
any local move**, so anything that ever populates the sector must be a pair.
