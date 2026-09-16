# Y_QM_007 - Result

**Audit:** ResearchY-QM_007 - Generator Selection Audit
**Verdict:** **BOUNDARY**
**Tests:** 8/8 PASSED

## Answer

**The constraint is the substrate's own SPECTRAL FINGERPRINT, and it is an INPUT rather than a dynamical law.**
**The identification is computed:** the recorded trace **1152 = 2 × 6 × 96** fixes the shell **count** at six, and the
recorded **45 levels** with maximum **15.837372** fix **which** six - of all **63** subsets, **exactly 1** reproduces
the recorded spectrum.

## The identification, measure by measure

| requirement | recorded | {1} | {1,2} | {1..6} | discriminates |
|---|---|---|---|---|---|
| trace | 1152 | 192 | 384 | **1152** | yes |
| distinct levels | 45 | 49 | 47 | **45** | yes |
| free room | 51 | 47 | 49 | **51** | yes |
| maximum eigenvalue | 15.837372 | 4.000000 | 6.249689 | **15.837372** | yes |
| reproduces all 96 eigenvalues | yes | no | no | **yes** | yes |
| amplitude/phase split 42/53 | 42 / 53 | 42 / 53 | 42 / 53 | 42 / 53 | **NO - \|S\|-independent** |

**Census over the 63 subsets:** **1** matches the trace, **9** match the level count, **1** reproduces the whole
spectrum.

## Nothing dynamical selects the native set

| measure | {1} | {1,2} | {1..6} | prefers |
|---|---|---|---|---|
| locality (non-zeros/row) | 2 | 4 | **12** | the **native** set |
| dispersion error at the edge | **59.47 %** | 91.89 % | 98.66 % | the **singleton** |
| fold position | **none** | 28 | 11 | the **singleton** |
| packet window at 10 % | **514** | 153 | 23 | the **singleton** (22×) |
| existing AT requirements | fails all | fails all | **satisfies all** | the **native** set |

**Two rows prefer the native set, and the first follows from the second:** a 12-regular graph is local by
construction.

## The packet ordering

| t | {1} | {1,2} | {1..6} | reference |
|---|---|---|---|---|
| 10 | 4.7067 | 4.6829 | 4.5122 | 4.7170 |
| 20 | 6.3729 | 6.3024 | 5.7828 | 6.4031 |
| 40 | 10.6983 | 10.5299 | 9.2609 | 10.7702 |

Distances at t = 20: **0.004149 / 0.013906 / 0.088008**.

## Notes — two defects in the audit's own first version

1. **The trace comparison used exact `double` equality.** The record is a **rounded integer** (1152) while the
   computed symbol sum is **1151.9999999999998**, so a match was reported as a **mismatch** - the repository's rule-1
   trap in miniature. The comparison now carries a **1E-6 tolerance**, and the same helper serves the census and the
   table.
2. **The table's trace cell was fed the MASK rather than the trace** (1, 3, 63 instead of 192, 384, 1152), from a
   helper whose parameter I renamed but whose call sites I did not. The cells now go through
   `TraceMatchesTheRecord`, so table and census cannot disagree by construction.

**The question's premise is confirmed:** **{1} really is the Schrodinger optimum** - no fold, the smallest dispersion
error, and a packet that tracks the continuum law **22×** longer. **AT cannot use it**, because the substrate is a
12-regular graph and its spectrum is the record from which the **free room 51**, the **mode table** and the
**42/53 decomposition** were derived. **The fold is the signature of the substrate the theory actually has.**

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
