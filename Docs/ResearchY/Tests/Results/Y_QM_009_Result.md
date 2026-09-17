# Y_QM_009 - Result

**Audit:** ResearchY-QM_009 - Fingerprint Necessity Audit
**Verdict:** **BOUNDARY**
**Tests:** 8/8 PASSED

## Answer

**No AT prediction fails.** Of the five sectors, **1 is UNCHANGED, 4 are BOUNDARY and 0 is REFUTED**. The sector that
is unchanged is the only one whose output is a **measurement**, and the four that move move for one reason: the
canonical **state** is spectrum-derived, so every quantity evaluated **on** the state moves, while every **law** stated
in terms of the state's scalars does not.

## The live source scan

| sector | in code | in comments | files | scan verdict |
|---|---|---|---|---|
| **redshift sector** | **0** | 0 | 2 | **LATTICE-FREE** |
| **clock law** | **0** | 0 | 2 | **LATTICE-FREE** |
| source law | 5 | 0 | 1 | READS THE SUBSTRATE |
| phase sector | 8 | 0 | 2 | READS THE SUBSTRATE |
| observable algebra | 59 | 0 | 2 | READS THE SUBSTRATE |

## The redshift sector - UNCHANGED

| quantity | {1..6} | {1} |
|---|---|---|
| solar compactness x | −2.123047E-006 | −2.123047E-006 |
| AT redshift `expm1(−x)` | 2.123049E-006 | 2.123049E-006 |
| GR redshift | 2.123054E-006 | 2.123054E-006 |
| ratio law `(ρ₁/ρ₂)^(1/3) − 1` | 0.2599210499 | 0.2599210499 |
| required compactness precision | 0.03661 | 0.03661 |

**The scalar bridge:** the sector consumes `d` = 3 and occupancies; the one scalar the state supplies - the density
normalisation, **96** - is itself `|S|`-independent.

## The clock law - the law is unchanged and the pattern is not

| measurement | value |
|---|---|
| the law against its own definition | **0.000E+000** |
| the pattern's maximum relative shift | 1.013E-001 |
| the pattern's correlation | **0.222310** |
| the pattern's mean rate | 0.99879755714637009 |

The clock's own file is `LATTICE-FREE` and the law is the same function of the same two numbers, **yet the pattern
moves** - and it is nearly **decorrelated**, matching QM_008's state correlations of 0.19 to 0.24.

## The source law - the audit's own hypothesis refuted

| candidate | {1..6} | {1} |
|---|---|---|
| 1 occupancy imbalance | 1.3676915272 | 1.5191704749 |
| **2 phase imbalance** | **0.0000000000** | **0.4671052634** |
| **3 amplitude-phase coupling** | **0.0000000000** | **0.0813500268** |
| **4a actualization pressure (uniform)** | **9.7979589711** | **9.7979589711** |
| 4b actualization pressure (local rate) | 0.3407493037 | 0.3349461334 |
| 5 spectral mismatch | 0.0257218163 | 0.0325700679 |
| 6 boundary assignment | 0.0157488441 | 0.0034945520 |

**The draft assumed the RANKING and the phase-null classification were the structural part of a source law. Both
move:** two candidates go from **exactly zero** to real pushes, and the phase-null membership falls from **four
candidates to one**. **What survives is the fixed point and only the fixed point:** the uniform actualization pressure
has norm **√96 = 9.7979589711 on both substrata**.

## The phase sector and the observable algebra

| quantity | {1..6} | {1} | classification |
|---|---|---|---|
| phase dimension | 53 | 49 | BOUNDARY |
| amplitude dimension | 42 | 46 | BOUNDARY |
| partially hidden modes | 0 | 0 | UNCHANGED |
| **the algebra's dimension** | **49** | **49** | **UNCHANGED** |
| sum of multiplicity squares | 230 | 190 | BOUNDARY |
| protected dimensions | 181 | 141 | BOUNDARY |
| the level-population observable | 45 | 49 | BOUNDARY |

**The algebra's dimension is `|S|`-independent because every `|k|` belongs to exactly one level**, so restricting the
orbital algebra to the levels counts each `|k|` once whatever the partition - **49 from 45 levels natively and 49 from
49 levels for `{1}`**, and the two partitions are genuinely different.

## Notes - six defects in the audit's own first version

1. **The redshift comparison was BACKWARDS**: I asserted `z_AT > z_GR`; AT is the **smaller** (2.123049E-006 against
   2.123054E-006), which G_068 recorded.
2. **A wrong literal in the ratio law**: `(ρ₁/ρ₂)^(1/d) − 1` at ρ = 2 against ρ = 1 is **2^(1/3) − 1 = 0.259921049895**,
   not 1.0.
3. **A wrong literal in the mean clock rate**: 0.99879755714637009, not 1 - the gap is **Jensen's inequality**, the
   mean of the cube roots being strictly below the cube root of the mean.
4. **A mistyped computed constant**: the Jensen gap is 0.99639700730968433, first written as 0.9963974978.
5. **The structural hypothesis about the source law was refuted by the measurement** and is reported as refuted.
6. **Two clock numbers were written into the doc BEFORE being measured** - "1.484E-001" and "0.981666" against the
   measured **1.013E-001** and **0.222310**. The second is not a rounding difference but the opposite conclusion: 0.222310
   says the patterns are nearly decorrelated where 0.981666 would have said the shape survives. Rule 5 in miniature.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
