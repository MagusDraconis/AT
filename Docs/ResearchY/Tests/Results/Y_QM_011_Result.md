# Y_QM_011 - Result

**Audit:** ResearchY-QM_011 - Fingerprint Justification Audit
**Verdict:** **NONE** - no genuinely physical consequence of the native fingerprint exists
**Tests:** 6/6 PASSED

## The answer

**NONE, stated explicitly as the question's own fallback branch.** **7 claims require the native spectrum, 2 require a
spectrum without requiring the native one, and 4 are independent of it - and the count of claims that BOTH require the
native spectrum AND carry an observable is 0.**

## The search

| claim | holds {1..6} | holds {1} | native value | {1} value | need | observable |
|---|---|---|---|---|---|---|
| **the clock law ρ^(1/d)** | yes | yes | `d = 3`, `rate(8) = 2` | identical | **INDEPENDENT** | **yes** |
| **the redshift law 1 + z = exp(−x)** | yes | yes | 2.123049E-006 | 2.123049E-006 | **INDEPENDENT** | **yes** |
| the flux quantum 2π/96 | yes | yes | 0.065449846950 | 0.065449846950 | **INDEPENDENT** | no |
| the flow conserves the norm | yes | yes | 8.9E-016 | 1.3E-015 | **INDEPENDENT** | no |
| the long-wavelength power law is 2 | yes | yes | 2.0000 | 2.0000 | SPECTRUM_ONLY | no |
| the kernel is a union of Fourier modes | yes | yes | no split modes | no split modes | SPECTRUM_ONLY | no |
| the amplitude/phase split is 1 + 42 + 53 | yes | **no** | 1 + 42 + 53 | 1 + 46 + 49 | **REQUIRES_NATIVE** | no |
| the free room is 51 | yes | **no** | 51 | 47 | **REQUIRES_NATIVE** | no |
| the trace is 1152 | yes | **no** | 1152 | 192 | **REQUIRES_NATIVE** | no |
| the level count is 45 | yes | **no** | 45 | 49 | **REQUIRES_NATIVE** | no |
| the maximum eigenvalue is 15.837372 | yes | **no** | 15.837372 | 4.000000 | **REQUIRES_NATIVE** | no |
| the Schrödinger coefficient is 91 | yes | **no** | 91 | 1 | **REQUIRES_NATIVE** | no |
| the dispersion folds at channel 11 | yes | **no** | 11 | 0 (no fold) | **REQUIRES_NATIVE** | no |

## Why - measured, not argued

**The repository's observable inventory has 11 entries and 0 of them names a spectral object.** They are the six
external observables plus the five pure temporal ones (`ClockRateAT`, `ClockRateGR`, `RedshiftAT`, `RedshiftGR`,
`Discriminator`). **A claim can only require the exact spectrum by quantifying over spectral objects**, so the
observability test **cannot be passed by anything the fingerprint fixes**.

## The near miss

| quantity | {1..6} | {1} |
|---|---|---|
| fold channel | **11** | **0** (no fold) |
| channels with reversed group velocity | **23** | **0** |
| maximum \|group velocity\| | **36.859293** | **2.000000** |

The group velocity **changes sign inside the band** natively and never for `{1}`: a real dynamical difference between
the substrata, and **not observable**, because no inventory observable reads the dispersion at high wavenumber.

## Notes

1. **The audit's own expectation was refuted and is recorded.** I reasoned that every channel from the fold to the zone
   edge would reverse (`48 − 11 = 37`); the measurement gives **23**, because the symbol is a sum of **six** sinusoids
   and the group velocity **oscillates back above zero** - a single fold position does not bound the reversed region.
2. **The qualification is stated rather than buried.** This is a statement about the **current** observable inventory,
   not a theorem about all conceivable ones: an observable reading the dispersion inside the band would make the fold's
   position observable and the verdict conditional. The fold's dynamical signature is therefore reported explicitly.
3. **The flux row is kept honest.** The flux quantum is INDEPENDENT but is a physical **constant**, not an inventory
   observable - the audit does not count it as an observable to strengthen the result.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
