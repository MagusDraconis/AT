# Y_QM_005 - Result

**Audit:** ResearchY-QM_005 - Laplacian Dispersion Audit
**Verdict:** **BOUNDARY**
**Tests:** 8/8 PASSED

## Answer

**The two halves separate cleanly: the power law is REPRESENTATION-INDEPENDENT and the fold is NOT.** Measured on
**all 63 non-empty subsets** of the six shells.

## Half one: the exponent is forced (DERIVED)

| subset | D | power law | coefficient verified as Σr² |
|---|---|---|---|
| NATIVE {1..6} | 91 | **2.0000** | yes |
| one shell {1} | 1 | **2.0000** | yes |
| one shell {2} | 4 | **2.0000** | yes |
| {1,2} | 5 | **2.0000** | yes |

Every non-empty shell set is a Laplacian and every term `2 − 2cos(rδ)` starts at `(rδ)²`: **the exponent is 2 for all
63 subsets** (`ThePowerLawIsRepresentationIndependent() = True`), verified as a limit to eight digits, and the
coefficient is the second moment (`TheCoefficientIsTheSecondMoment() = True`).

## Half two: the fold arrives with the second shell (BOUNDARY)

| shell | measured fold | closed form 48/r |
|---|---|---|
| **{1}** | **0 - never** | 48.0 |
| **{2}** | **25** | 24.0 |
| {3} | 17 | 16.0 |
| {4} | 13 | 12.0 |
| {5} | 10 | 9.6 |
| {6} | 9 | 8.0 |

Agreement with the closed form to **exactly one channel past 48/r**, in every case.

| census | value |
|---|---|
| subsets that fold | **62 of 63** |
| subsets that never fold | **1** (`{1}`) |
| earliest fold any subset reaches | **channel 9** |
| **latest fold** | **channel 28** (`{1,2}`) |
| native fold | **channel 11**, 23 reversed channels |

## The cure and its cost

| subset | D | fold | window | occupied in window |
|---|---|---|---|---|
| NATIVE {1..6} | 91 | 11 | 3 | **3 of 42** |
| {1,2} | 5 | 28 | 9 | 9 of 42 |
| {2,3,4} | 29 | 15 | 4 | 4 of 42 |
| **one shell {1}** | **1** | **none** | **17** | **16 of 42** |

**Best coverage 16 of 42 at `{1}`, 5.33× the native set's 3** - at a coefficient of **1 against 91**.

## The continuum expansion

`ω = D k² − E k⁴`, `E = Σr⁴/12`: native **D = 91, E = 189.58, k at 10 % = 0.2191** (window 3 channels); one-shell
**D = 1, E = 0.08, k at 10 % = 1.0954** (window 17 channels). The analytic k-regime predicts the measured window and
its **order**.

## Notes

**The audit's own first version had three defects, all recorded.**

1. **The power-law fit was taken at δ = 1e-3 and 2e-3**, where the six-shell quartic term already contributes ~9E-6 to
   the exponent and pushed it outside a 1E-6 tolerance (`ThePowerLawIsRepresentationIndependent()` came out **False**).
   It is now taken at 1E-4 and 2E-4.
2. **The single-shell fold was predicted at 24/r instead of 48/r** - a factor two from conflating δ = 2πc/96 - and the
   measurement refused it. The closed form is now checked, not quoted.
3. **The dispersion was computed as `2 − 2cos(rδ)` directly, which loses digits to cancellation** - exactly the regime
   the power-law fit consumes - so the δ → 0 limit test failed. The half-angle identity
   `2 − 2cos(rδ) = 4sin²(rδ/2)` is now used, and the error it removes is **measured**: the direct form is exact to the
   printed digits at δ = 1E-4…1E-6 and **0.01 % low at δ = 1E-7**. My first prediction of that error was **too
   pessimistic by an order of magnitude**, and the test asserts the measured value.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
