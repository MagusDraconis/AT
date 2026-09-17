# ResearchY-QM_009 - Fingerprint Necessity Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_009 (permanent)
**Title:** Which AT predictions fail if the spectral fingerprint is replaced by the Schrodinger-optimal generator {1}?
**Status:** COMPLETE
**Date:** 2026-09-17
**File:** `QM_ManyBody/ResearchY-QM_009.md`
**Depends on:** QM_007 (the fingerprint is an input), QM_008 (the fingerprint is operative), G_019/G_020/G_068 (the redshift sector), G_017/G_018 (the clock law), G_001/G_059 (the source law), G_052 (the phase sector), G_040/G_061 (the observable algebra)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_009_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/FingerprintNecessityAudit.cs`

## The question

**Which AT predictions fail if the spectral fingerprint is replaced by the Schrodinger-optimal generator {1}?**
Compare the native **{1..6}** against **{1}** across five sectors - the **redshift sector**, the **clock law**, the
**source law**, the **phase sector** and the **observable algebra** - and classify each **UNCHANGED / BOUNDARY /
REFUTED**.

## The answer

> **BOUNDARY - AND THE SHARP FORM OF THE ANSWER IS THAT NO AT PREDICTION FAILS.** Of the five sectors, **1 is
> UNCHANGED, 4 are BOUNDARY and 0 is REFUTED**. **The sector that is unchanged is the only one whose output is a
> measurement**, and the four that move move for one reason: the canonical **state** is spectrum-derived, so every
> quantity evaluated **on** the state moves, while every **law** stated in terms of the state's scalars does not.

## 1. The live source scan - the mechanical half of the answer

A sector whose **code** cannot name the spectrum cannot fail when the spectrum is replaced. The scan reads each
sector's own files at test time and counts **substrate tokens in code**, comments excluded (the G_027 discipline):

| sector | in code | in comments | files | scan verdict |
|---|---|---|---|---|
| **redshift sector** | **0** | 0 | 2 | **LATTICE-FREE** |
| **clock law** | **0** | 0 | 2 | **LATTICE-FREE** |
| source law | 5 | 0 | 1 | READS THE SUBSTRATE |
| phase sector | 8 | 0 | 2 | READS THE SUBSTRATE |
| observable algebra | 59 | 0 | 2 | READS THE SUBSTRATE |

Tokens: `ModeEigenvalues`, `LevelBasis`, `LevelIndexOfMode`, `LevelIndex`, `DistinctLevels`, `Levels(`,
`Multiplicity`, `LaplacianDispersionAudit`, `LaplacianTrace`, `Spectrum(`, `GeneratorSelectionAudit`,
`RhoObservableAudit`.

**A zero is negative evidence and the audit treats it as such.** The scan is a statement about the sector's **files**;
a lattice-free file can still produce a moving quantity when the thing that moves is its **input**. That is exactly
what happens to the clock law below.

## 2. The redshift sector - UNCHANGED

| quantity | {1..6} | {1} | classification |
|---|---|---|---|
| solar compactness x | −2.123047E-006 | −2.123047E-006 | **UNCHANGED** |
| AT redshift `z = expm1(−x)` | 2.123049E-006 | 2.123049E-006 | **UNCHANGED** |
| GR redshift `z = (1+2x)^(−1/2) − 1` | 2.123054E-006 | 2.123054E-006 | **UNCHANGED** |
| clock rate `ρ^(1/3)` at ρ = 2 | 1.2599210499 | 1.2599210499 | **UNCHANGED** |
| ratio law between ρ = 2 and ρ = 1 | 0.2599210499 | 0.2599210499 | **UNCHANGED** |
| second-order coefficients | 0.5 / 1.5 | 0.5 / 1.5 | **UNCHANGED** |
| required compactness precision | 0.03661 | 0.03661 | **UNCHANGED** |

**Why nothing in that chain can see a substrate** - the **scalar bridge**, measured:

| input | kind | {1..6} | {1} | same |
|---|---|---|---|---|
| `d` | dimension of space | 3 | 3 | yes |
| state total | the density normalisation | 96 | 96 | **yes** |
| mean occupancy | state total / cells | 1 | 1 | yes |
| `x = −GM/(Rc²)` | a ratio of physical constants | −2.12E-006 | −2.12E-006 | yes |

**The sector consumes a dimension and occupancies and never a spectrum, a level or a Laplacian**, and the one scalar
the state supplies - the density normalisation - is itself `|S|`-independent (QM_008).

## 3. The clock law - the law is unchanged and the pattern is not

| measurement | value |
|---|---|
| the law sampled against its own definition | **0.000E+000** |
| the pattern's maximum relative shift | **1.013E-001** |
| the pattern's correlation | **0.222310** |
| the pattern's mean absolute shift | 3.352E-002 |
| the pattern's mean rate | **0.99879755714637009** |

**The clock pattern is nearly DECORRELATED between the two substrata** (0.222310), which is the same phenomenon QM_008
measured for the states themselves (correlations 0.19 to 0.24): the state is most of the pattern's shape.

**The clock's own file is `LATTICE-FREE`, and the law `ρ^(1/d)` is the same function of the same two numbers on both
substrata - yet the pattern the law induces moves**, because the pattern is evaluated on a state that is
spectrum-derived. **So the clock sector is a `BOUNDARY` in one direction and a `LATTICE-FREE` file in the other, and
both are measurements.**

## 4. The source law - and the audit's own hypothesis is refuted

| candidate | {1..6} | {1} |
|---|---|---|
| 1 occupancy imbalance | 1.3676915272 | 1.5191704749 |
| **2 phase imbalance** | **0.0000000000** | **0.4671052634** |
| **3 amplitude-phase coupling** | **0.0000000000** | **0.0813500268** |
| **4a actualization pressure (uniform)** | **9.7979589711** | **9.7979589711** |
| 4b actualization pressure (local rate) | 0.3407493037 | 0.3349461334 |
| 5 spectral mismatch | 0.0257218163 | 0.0325700679 |
| 6 boundary assignment | 0.0157488441 | 0.0034945520 |

**The draft of this audit assumed the RANKING of the candidate sources and the phase-null classification were the
structural part that a "source law" is made of. The measurement refutes both:**

- **The ranking moves.** Two candidates are **EXACTLY NULL** natively and carry real pushes on `{1}` - the
  phase-imbalance candidate jumps from **0.0** to **0.4671052634**, and the amplitude-phase-coupling candidate from
  **0.0** to **0.0813500268**.
- **The phase-null membership falls from four candidates to one**: `{2, 3, 4a, 5}` natively, `{4a}` on `{1}`.

**What actually survives is the FIXED POINT, and only the fixed point.** The uniform actualization pressure
(`4a`) has norm **9.7979589711 = √96 on both substrata**, because a constant push has no phase part and no occupancy
gradient - so **the only source-law statement that is `|S|`-independent is the statement that the uniform pressure is
a fixed point.**

## 5. The phase sector - membership moves, structure holds

| quantity | {1..6} | {1} | classification |
|---|---|---|---|
| phase dimension | 53 | 49 | **BOUNDARY** |
| amplitude dimension | 42 | 46 | **BOUNDARY** |
| partially hidden modes | 0 | 0 | **UNCHANGED** |
| partition residual | 0 | 0 | **UNCHANGED** |

## 6. The observable algebra - one invariant and three moves

| quantity | {1..6} | {1} | classification |
|---|---|---|---|
| **the algebra's dimension** | **49** | **49** | **UNCHANGED** |
| sum of multiplicity squares | 230 | 190 | **BOUNDARY** |
| protected dimensions | 181 | 141 | **BOUNDARY** |
| the level-population observable | 45 | 49 | **BOUNDARY** |

**The invariance of the dimension is a theorem worth stating: every `|k|` belongs to exactly one level, so restricting
the orbital algebra to the levels counts each `|k|` once whatever the partition - 49 from 45 levels natively and 49
from 49 levels for `{1}`.** The two partitions are genuinely different, so the equality is structural rather than a
coincidence - and the multiplicity-weighted **content** moves, because it weighs each level by its size.

## 7. The verdict

```
1 UNCHANGED + 4 BOUNDARY + 0 REFUTED
redshift sector    LATTICE-FREE        UNCHANGED
clock law          LATTICE-FREE        BOUNDARY
source law         READS THE SUBSTRATE BOUNDARY
phase sector       READS THE SUBSTRATE BOUNDARY
observable algebra READS THE SUBSTRATE BOUNDARY
```

**The goal was to determine whether the native spectrum is physically required or merely historically inherited, and
the prediction frame gives a sharper answer than the conclusion frame did (QM_008).** The theory's **observable
content is fingerprint-free**: the redshift sector is completely immune, and it is the only sector whose output is a
measurement. **The fingerprint is required only by the theory's internal bookkeeping** - the amplitude/phase
membership, the multiplicity-weighted algebra content and the level count.

## 8. Defects in the audit's own first version

1. **The redshift comparison was BACKWARDS.** I asserted `z_AT > z_GR`; the measurement gives **2.123049E-006
   against 2.123054E-006**, so AT is the **smaller** - which G_068 recorded and `AtRedshiftIsAlwaysSmaller` already
   asserts. Fixed to the measured direction.
2. **A wrong literal in the ratio law.** I asserted `(ρ₁/ρ₂)^(1/d) − 1 = 1.0` at ρ = 2 against ρ = 1. The value is
   **2^(1/3) − 1 = 0.259921049895**. Fixed to the closed form.
3. **A wrong literal in the mean clock rate.** I asserted the mean rate is exactly **1** from the mean occupancy being
   1. It is **0.99879755714637009**, and the reason is **Jensen's inequality** - the mean of the cube roots is strictly
   below the cube root of the mean whenever the state is not uniform. The assertion now records the measured value and
   the direction.
4. **A mistyped computed constant.** The Jensen gap was first written as `0.9963974978`; the value is
   **0.99639700730968433** - a wrong digit in a constant I had just computed, the same class of slip as QM_008's
   printed-literal defect.
5. **The structural hypothesis about the source law was refuted by the measurement** (section 4), and the audit
   records the refutation rather than keeping the claim.
6. **Two clock numbers were written into this document BEFORE they were measured.** I drafted "maximum relative shift
   1.484E-001" and "correlation 0.981666" from an expectation about how far a pattern would move. The measurements are
   **1.013E-001** and **0.222310** - the second is not a rounding difference but the opposite conclusion, since
   0.222310 says the two clock patterns are nearly **decorrelated** while 0.981666 would have said the shape survives.
   The numbers are corrected to the measurements, and the correction is recorded because the repository's rule 5
   exists for exactly this slip: a number in a document is not evidence.

## Where it stands

**No AT prediction fails.** The redshift sector - the theory's one observable-time prediction, and the sector G_068,
G_070 and G_071 built an observing programme on - is **lattice-free in its code and identical in its numbers**, and
its only state-supplied input (the density normalisation, 96) is itself `|S|`-independent.

**And the four `BOUNDARY` sectors fail in the same way, which is the result the goal was after:** they move because
they are evaluated **on the canonical state**, not because they state a law about the substrate. The cleanest single
illustration is the clock: **the law `ρ^(1/d)` is the same function on both substrata, and the pattern it induces is
a different pattern** - so "does the clock law need the six-shell spectrum?" has the answer "the law does not and the
pattern does".

**The sharpest new fact is the source law's invariant.** The draft expected the ranking and the phase-null set to be
the structural content; the measurement says they both move, and leaves **one** invariant statement: the uniform
actualization pressure is a fixed point, with norm **√96** on both substrata. That is a weaker structural core than
G_059 assumed, and it is reported as such rather than as a preserved conclusion.
