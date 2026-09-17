# Y_G_072 - Result

**Audit:** ResearchY-G_072 - Observational Program Audit
**Verdict:** **DERIVED** - the program is one measurement on one object
**Tests:** 7/7 PASSED

## Target / Precision / Instrument class / Decision significance

| output | value |
|---|---|
| **Target** | **J0740+6620 (Riley 2021)**, M = 2.072 M☉, R = 12.39 km, **x = -0.24704** |
| **Precision** | surface redshift / compactness to **14.1422 %** (3 sigma) and **7.5524 %** (5 sigma) |
| **Instrument class** | **large-area X-ray timing**: 10 % timing with 1 % and 3 % mass/radius marginals |
| **Decision significance** | **4.1374 sigma**, rising to **44.8377 sigma** for a direct surface-redshift measurement to 1 % |

## The program

| instrument class | timing | compactness | significance | reaches |
|---|---|---|---|---|
| A. current X-ray timing (NICER-class) | 20 % | 9.0664 % | **1.9235** | 1 sigma |
| B. A plus radio pulsar timing | 20 % | 8.3339 % | **1.9641** | 1 sigma |
| **C. next-generation X-ray timing** | **10 %** | **3.1623 %** | **4.1374** | **3 sigma** |
| D. next-generation timing and spectroscopy | 5 % | 1.5811 % | **8.2749** | **5 sigma** |
| **E. a direct surface-redshift measurement** | **1 %** | - | **44.8377** | **5 sigma** |

## The requirements

| significance | required timing | required compactness | which binds |
|---|---|---|---|
| 3 sigma | **14.1422 %** | **NaN** | the TIMING |
| 5 sigma | **7.5524 %** | **NaN** | the TIMING |

**The required compactness is NaN at the recorded 20 % timing** - no compactness precision decides the question until
the timing improves. These reproduce **G_070's** recorded 7.55 % / 14.14 %.

## The leverage

| capability | significance | gain |
|---|---|---|
| the baseline (A) | 1.9235 | 1.0000 |
| **timing alone to 1 %** | 3.7316 | **1.9400** |
| mass alone to 0.1 % | 1.9641 | 1.0211 |
| radius alone to 1.5 % | 2.1720 | 1.1292 |
| everything together (D) | 8.2749 | 4.3020 |

## Notes - three defects in the audit's own first version

1. **The draft headline was refuted**: I claimed the decision "is not bought with timing" and that the radius carries
   the leverage. **Timing leads by 1.7200 over the radius**, and the mass is nearly useless alone.
2. **My expectation for the current row was wrong**, and for a reason worth recording: I expected **1.05-1.2 sigma**
   from G_071; the measurement gives **1.9235 sigma**. G_071's **1.0534 sigma** is the **generic** 1.4 M☉, 12 km
   object at its **worst-case correlated** error of **11.9048 %**, while this row is the **target** J0740+6620 (more
   compact, larger second-order separation) at the **quadrature** error of **9.0664 %**.
3. **A bookkeeping error**: I compared the class census (counting classes) with the length of the FirstDecider list
   (returning only the first decider) and asserted 2 = 1.

## Honest limits

- The classes are **capability triples, not mission commitments** - the audit measures what a triple buys and does not
  audit whether any funded instrument delivers one.
- The correlation is **zero**, so the numbers are **floors**; G_071's frontier is where the favourable cases live.
- Class E assumes a resolved surface feature exists; it is the strongest route by two orders of magnitude and the one
  with no current instrument behind it.

**Group-G count guards bumped:** `Y_G_033` 41 -> 42 and `Y_G_035` 72 -> 73 with survives 58 -> 59 (both the registry
census and the `MinimalTimeSector` view, the second of which the guard itself caught).
