# Y_G_053 - Result

**Audit:** ResearchY-G_053 - Phase Sector Dynamics Audit
**Verdict:** **DERIVED**
**Tests:** 6/6 PASSED

## Answer

Phase modes have effects **no amplitude move reproduces** (non-reproducible fraction **1.0000 / 1.0000 / 0.9926** for
clock / acceleration / field). The **amplitude sector is exactly the no-rotation sector**; the **phase sector rotates**
the Fourier content. The first **uniquely phase-sensitive** observable is the **quadrature functional**.

## Measurements

| quantity | value |
|---|---|
| perturbation overlap | **2.168E-018** |
| responses: clock / acceleration / field | 6.678E-003 vs 6.772E-003 / 8.904E-004 vs 7.024E-004 / 1.128E-004 vs 9.971E-005 |
| amplitude-response span rank | **42 of 42** |
| non-reproducible fraction | **1.0000 / 1.0000 / 0.9926** |
| channel 1: amplitude magnitude / angle | **3.464E-001** / **2.220E-015** (machine zero) |
| channel 1: phase angle / magnitude | **3.463E-001** / 6.059E-002 |
| quadrature functional: phase / amplitude response | **2.000E-002** / **3.272E-015** |

## Note

The draft expected a clean 100× "resize versus rotate" separation; the measurement gives an **exact zero** for the
amplitude move's angle change (the sharp statement) and a **6×** magnitude ratio (weaker than expected). The criterion
was corrected to what is measured, and the reason the phase move changes the magnitude at all is reported: the state
carries content in both quadratures of a populated channel.
