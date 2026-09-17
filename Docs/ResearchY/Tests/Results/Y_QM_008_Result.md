# Y_QM_008 - Result

**Audit:** ResearchY-QM_008 - Spectral Necessity Audit
**Verdict:** **BOUNDARY**
**Tests:** 8/8 PASSED

## Answer

**The fingerprint is indispensable for the NUMBERS and dispensable for the STRUCTURE, and the partition is computed
rather than assigned:** of the **12** conclusions recomputed on all three generators, **5** are **UNCHANGED**, **5** are
**BOUNDARY** and **2** are **REFUTED**. **5 of 12 do not mention the spectrum anywhere; 7 require it - 5 for their value
and 2 for their existence.**

## The recomputation is real

The canonical state walks the **spectrum's levels**, so replacing the generator replaces the **state**. The audit
rebuilds the levels, the level basis, the canonical state and the observable split from each candidate spectrum.

| check | measurement |
|---|---|
| rebuilt native state vs the recorded state | **0.000E+000** (bitwise) |
| rebuilt native split vs five recorded quantities (rank 43, amplitude 42, phase 53, kernel 53, contraction rank 42) | **all five agree** |

| pair | max shift | L2 shift | correlation |
|---|---|---|---|
| {1} vs {1,2} | 0.370396 | 1.419953 | **0.221376** |
| {1} vs {1..6} | 0.346756 | 1.274251 | **0.236824** |
| {1,2} vs {1..6} | 0.420200 | 1.419054 | **0.189997** |

**The three canonical states are nearly orthogonal - correlations 0.19 to 0.24.**

## The full table

| conclusion | source | {1} | {1,2} | {1..6} | classification |
|---|---|---|---|---|---|
| the split is mean + amplitude + phase | QM_001 | 1+46+49 | 1+44+51 | 1+42+53 | **BOUNDARY** |
| the observable rank is 43 | QM_001 | 47 | 45 | 43 | **BOUNDARY** |
| ρ = \|Ψ\|² exactly | QM_001 | 2.2E-016 | 2.2E-016 | 2.2E-016 | UNCHANGED |
| the state totals the cell count | QM_001 | 96.0000 | 96.0000 | 96.0000 | UNCHANGED |
| the flow conserves the norm exactly | QM_002 / QM_006 | 1.3E-015 | 8.9E-016 | 8.9E-016 | UNCHANGED |
| the mode occupation is conserved | QM_002 | 8.9E-016 | 1.6E-015 | 1.3E-015 | UNCHANGED |
| the long-wavelength power law is 2 | QM_004 / QM_005 | 2.0000 | 2.0000 | 2.0000 | UNCHANGED |
| the Schrödinger coefficient is the second moment | QM_004 | 1 | 5 | 91 | **BOUNDARY** |
| the quartic coefficient is Σr⁴/12 | QM_005 | 0.08 | 1.42 | 189.58 | **BOUNDARY** |
| the dispersion folds inside the band | QM_003 / QM_005 | none | ch 28 | ch 11 | **REFUTED** |
| the packet tracks Schrödinger for a window | QM_006 / QM_007 | 514 | 153 | 23 | **BOUNDARY** |
| the recorded fingerprint | QM_007 | 192/49/47/4.000 | 384/47/49/6.250 | 1152/45/51/15.837 | **REFUTED** |

The power-law fits are **1.999999996 / 1.999999988 / 1.999999910** - they differ in the **eighth digit**, which is the
fit's own noise, so that row is compared at the fit's 1E-6 and the raw values are printed beside the table.

## The split MOVES - QM_007's claim is refuted

| candidate | observable rank | mean | amplitude | phase | hidden | visible | split |
|---|---|---|---|---|---|---|---|
| {1} | 47 | 1 | **46** | **49** | 49 | 46 | 0 |
| {1,2} | 45 | 1 | **44** | **51** | 51 | 44 | 0 |
| {1..6} | 43 | 1 | **42** | **53** | 53 | 42 | 0 |

QM_007 claimed `|S|`-independence by calling a **mask-free helper**, which returns the native answer by construction.
The split is a property of the **state's kernel**, and the state is spectrum-derived. **What survives is the structural
half:** every candidate's kernel is still a union of Fourier modes (zero split modes, exact partition, in all three).
A **refinement note** has been added to the QM_007 doc and registry entry.

## Notes - six defects in the audit's own first version

1. **The mode-leakage helper evolved the PACKET, not the mode.** `EvolveUnder` ignores its argument, so feeding it a
   single Fourier mode measured the packet's overlap with channel 5 (**1.9E+000**) and would have reported occupation
   conservation **REFUTED** on all three candidates. Now evolved explicitly: **8.9E-016 to 1.6E-015**.
2. **The power law was compared at 1E-9** - tighter than the fit's own precision - so the row classified **BOUNDARY**
   on noise. Rows now carry their own tolerance.
3. **My own assumption was refuted.** I expected the state replacement to be a small perturbation and asserted a
   correlation **above 0.9**; the measurement is **0.19 to 0.24**, nearly orthogonal. Recorded, not quietly fixed.
4. **A literal reached a verdict (rule 5).** The quartic coefficient of {1} was asserted as QM_005's printed **0.08**;
   the true value is **1/12 = 0.083333** and the test failed. It is now asserted against the closed form.
5. **An off-by-one in the rebuilt split** gave the native **42 / 41 / 54** against the recorded **43 / 42 / 53**;
   the correct reading is observable = seen, amplitude = seen − 1, phase = 96 − seen.
6. **My probe-removal script deleted five of the audit's own tests** - it cut everything between the temporary probe's
   `[Fact]` and the diagnostic's `[Fact]`, and the five new tests sat **between** them, so the suite silently dropped
   from **8 to 3**. It was caught by the **ResearchY total: 3296 against the expected 3293 + 8 = 3301**, not by the
   suite, which was passing. Restored; the suite is **8/8**. A test count is a measurement and is checked against
   arithmetic rather than read as a pass.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
