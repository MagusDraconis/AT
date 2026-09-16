# Y_QM_006 - Result

**Audit:** ResearchY-QM_006 - Schrodinger Propagator Audit
**Verdict:** **PARTIAL** (with **{1} ANALOGOUS**, which is what the question asks about)
**Tests:** 8/8 PASSED

## Answer

**The answer to the question as asked is YES: `{1}` produces `exp(-iHt)psi(0)` with the lattice Schrodinger
dispersion** - norm conservation **exact**, the packet **spreads by the continuum law**, and its distance from the
exact Schrodinger solution stays inside **10 %** for **t up to 514.0**. The **theory** is PARTIAL because it does
**not** use `{1}`.

## The evolution is exact, and the reference reproduces its own law

| time | reference width | analytic law | ratio |
|---|---|---|---|
| 0 | 4.0000 | 4.0000 | 1.000000 |
| 5 | 4.1908 | 4.1908 | 1.000000 |
| 20 | 6.4031 | 6.4031 | 1.000000 |
| 40 | 10.7702 | 10.7703 | 1.000000 |
| 80 | 19.8236 | **20.3961** | 0.9719 (finite ring) |

## Norm conservation

| candidate | norm deviation at t = 20 |
|---|---|
| native {1..6} | **6.66E-016** |
| nearest neighbour {1} | **6.66E-016** |
| spectral derivative | **0.00E+000** |
| **dissipative control** | **1.83E-001** (the control, which cannot pass vacuously) |

## The packet's width — the measure that decides

| time | **{1}** | native | **spectral** | reference |
|---|---|---|---|---|
| 10 | 4.7067 | 4.5122 | **4.0000** | 4.7170 |
| 20 | 6.3729 | 5.7828 | **4.0000** | 6.4031 |
| 40 | 10.6983 | 9.2609 | 4.9323 (seam) | 10.7702 |
| 80 | 19.7511 | 17.1761 | **4.0000** | 19.8236 |

**Schrodinger spreads; the advection operator drives.** The spectral candidate's centroid moves **20.000000 cells in
t = 20** with width growth **1.000000**.

## The window (10 % distance, bisected)

| candidate | window |
|---|---|
| **nearest neighbour {1}** | **514.0** |
| native {1..6} | **23.0** |
| spectral derivative | **0.8** |
| control | 2.9 |

Window ratio **22.39**, quartic contamination ratio `E/D` **25.00** — the same measurement from the dynamical side,
but **tracking rather than equal**, since the distance is not linear in the phase error.

**Distances at t = 20:** `{1}` **0.004149**, native **0.088008**, spectral **1.384233**.

## Notes — three defects in the audit's own first version

The whole audit rests on the width, and the first version got it wrong three times:

1. **Packet normalisation:** `exp(-x^2/(2w^2))` has RMS width `w/sqrt(2)`, so the packet was compared against a law
   for a different width. Now `exp(-x^2/(4w^2))` → density standard deviation exactly `w`.
2. **The analytic spread law's coefficient was wrong** (`(2t/w)^2` instead of `(t/w)^2`) and the **measured** reference
   refused it by **41 %**. The law is now checked against the exact evolution at every time (five digits).
3. **The second moment was not unwrapped about the centroid**, inflating the width (6.09 where truth was 2.83) when
   the packet straddled the ring edge. Now unwrapped, with the residual seam artifact at t = 40 **reported**.

**A fourth correction is textual:** the window ratio was first described as *being* the contamination ratio; it
**tracks** it without equalling it.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
