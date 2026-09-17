# ResearchY-QM_011 - Fingerprint Justification Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_011 (permanent)
**Title:** What surviving result requires the exact native spectrum rather than only its existence?
**Status:** COMPLETE
**Date:** 2026-09-17
**File:** `QM_ManyBody/ResearchY-QM_011.md`
**Depends on:** QM_007 (the fingerprint is an input), QM_008 (it is operative), QM_009 (no prediction fails), QM_010 (it carries numerical realisation), G_019/G_020/G_068 (the redshift), G_017/G_018 (the clock), G_051 (flux quantisation), G_040/G_061 (the mode table)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_011_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/FingerprintJustificationAudit.cs`

## The question

**What surviving result requires the exact native spectrum rather than only its existence?** Compare the native
**{1..6}** against the nearest-neighbour **{1}**, search **all** surviving claims, classify each
**REQUIRES_NATIVE / REQUIRES_SPECTRUM_ONLY / INDEPENDENT**, and find the **first genuinely physical consequence** of
the native fingerprint - **and if none exists, state that explicitly**.

## The answer

> **NONE - AND THE AUDIT STATES IT EXPLICITLY: NO GENUINELY PHYSICAL CONSEQUENCE OF THE NATIVE FINGERPRINT EXISTS.**
> **7 claims require the native spectrum, 2 require a spectrum without requiring the native one, and 4 are independent
> of it - and the count of claims that BOTH require the native spectrum AND carry an observable is 0.**

## 1. The classification is a rule with three measured inputs

```
REQUIRES_NATIVE      the claim's predicate FAILS on {1}                    (measured)
REQUIRES_SPECTRUM_ONLY  it survives on {1} and its own files NAME spectral objects   (measured scan)
INDEPENDENT          it survives on {1} and its files name no spectral object  (measured scan)
```

The third input is **the repository's own observable inventory**, and it is what turns "requires native" into a
statement about physics: a claim is observable-typed **exactly when its symbol appears in that inventory**.

## 2. The search

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

**The two observable-typed claims are both INDEPENDENT, and all seven native-requiring claims are non-observable.**

## 3. Why - and the reason is structural and measured

**The repository's observable inventory contains eleven entries and not one of them names a spectral object.** They
are the six external observables (`lensing-deflection`, `time-delay`, `magnification`, `horizon-shadow`,
`hawking-temperature`, `gw-strain`) and the five pure temporal ones (`ClockRateAT`, `ClockRateGR`, `RedshiftAT`,
`RedshiftGR`, `Discriminator`).

**A claim can only require the exact spectrum by quantifying over spectral objects** - eigenvalues, levels,
multiplicities, level bases. **So the observability test cannot be passed by anything the fingerprint fixes**, and the
empty intersection is a consequence of the inventory's shape rather than a gap in the search.

## 4. The near miss, named rather than glossed

The fingerprint **does** have measured **physical** consequences, and they are dynamical:

| quantity | {1..6} | {1} |
|---|---|---|
| fold channel | **11** | **0** (no fold) |
| channels with reversed group velocity | **23** | **0** |
| maximum \|group velocity\| | **36.859293** | **2.000000** |

**Past the fold the group velocity changes sign, so the two substrata differ in a real dynamical property - where a
wave's group velocity reverses inside the band.** **None of it is observable**, because no inventory observable reads
the dispersion at high wavenumber.

**And one of my own expectations was wrong here.** I reasoned that every channel from the fold to the zone edge would
reverse, i.e. `48 − 11 = 37` of them. **The measurement gives 23**, because the symbol is a sum of **six** sinusoids and
the group velocity therefore **oscillates back above zero** rather than staying negative - **a single fold position
does not bound the reversed region**. The measured value is asserted, and the refuted expectation is recorded.

## 5. What the fingerprint is actually justified by

**The realisation, and the audit names the whole of it:** the mode table (**42 hidden and 53 visible** against
**46 and 49**), the free room (**51** against **47**), the trace (**1152** against **192**), the level count (**45**
against **49**), the maximum eigenvalue (**15.837372** against **4.000000**) and the fold position (**channel 11**
against **no fold at all**).

**Every one of those is a number the theory quotes, and not one is a number an observer measures.** So the native
fingerprint is **an input the theory is entitled to take, and it is NOT a physical necessity.**

## 6. The qualification the audit makes rather than buries

**This is a statement about the current observable inventory and not a theorem about all conceivable ones.** If a
future audit adds an observable whose value depends on the dispersion inside the band, **the fold's position becomes
observable and this verdict becomes conditional** - which is why the fold's dynamical signature is reported explicitly
(23 reversed channels, peak group velocity 36.859293) rather than left unmentioned.

**A note on the flux row:** the flux quantum is **INDEPENDENT** of the fingerprint but is **not** in the observable
inventory - it is a physical **constant** rather than an observable. The audit keeps the two categories separate rather
than counting the flux as an observable to strengthen the result.
