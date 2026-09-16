# Y_G_068 - Result

**Audit:** ResearchY-G_068 - Temporal Prediction Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

**The unique prediction is one line:** AT gives `1 + z = e^(−x)` where GR gives `1 + z = (1 + 2x)^(−1/2)`, with
`x = −GM/(Rc²)` the surface potential. **The observable is a compact object's surface redshift**; **the split is second
order** (`−x²`); and **the status is ALLOWED but UNDECIDED**.

## The three outputs the question asked for

**1. Observable.** The surface redshift of a compact object (equivalently the rate of a clock at its surface against a
distant one). Both theories predict it exactly; neither is fitted.

**2. Magnitude.**

| regime | x | split | precision available | short by |
|---|---|---|---|---|
| Earth's surface | −6.961E-10 | **−4.846E-19** | 1E-18 | **2.1×** |
| the Sun's surface | −2.123E-06 | −4.507E-12 | 1E-5 | **2.2E+6×** |
| Sirius B | −2.573E-04 | −6.620E-08 | 0.02 | **3.0E+5×** |
| **J0030+0451 (NICER)** | −0.152011 | **−17.367 %** relative | 3–10 % (rate) | **live** |
| **J0740+6620 (Riley)** | −0.247002 | **−30.957 %** relative | 17–31 % (redshift) | **live** |

**3. Measurement status.**

| quantity | value |
|---|---|
| separation (M = 1.4, R = 12 km) | **0.047205** |
| combined uncertainty | 0.046394 |
| **significance now** | **1.017 σ** |
| **3σ needs** | σ_z ≤ **0.015735** = **8.37 %** of z_AT |
| **5σ needs** | σ_z ≤ **0.009441** = **5.02 %** of z_AT |
| current determinations | 20–50 % → short by **2.4×–6.0×** |
| allowed by published data | **yes** (inside 2.5σ everywhere, 0.33σ for the most compact) |
| preferred by published data | **no** |

## Notes

**A qualitative difference needing no precision:** AT's `g₀₀` **never vanishes** (no clock-stopping surface) while GR's
`1 + 2x` vanishes at `y = 0.5` where its redshift diverges.

**Two numerical facts are measured rather than cautioned.** At `x = −1E-9` the naive subtraction of two redshifts is
**inflated by a factor of 82.2** (returning −8.224E-17 where the truth is −1.000E-18), and the split is **below one ulp of
unity** - so the audit takes the split from the **series** and the redshift through `AtNumerics.ExpM1`. The series route
itself carries **6E-8 relative** error at that depth, which is **six digits better** than the naive route and is reported
as a measurement.

**The constants are recomputed, not imported:** the Sun's potential agrees with G_019's recorded value to **2.6E-4**
relative, the residual being the choice of solar mass.
