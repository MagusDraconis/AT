# Y_G_059 - Result

**Audit:** ResearchY-G_059 - Flow Source Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

The **first genuine source term is the difference itself** (occupancy imbalance) - AT's own primitive. The mechanism is
exact: the cyclic difference has Fourier multiplier `1 − e^(−iδ_c)`, so it carries a visible mode into the exact multiset
`{|sin δ|, 2 sin²(δ/2)}` (one amplitude part, one phase part), verified on all **42** visible modes. A **filter** cannot
create phase content at all, because its multiplier is real and symmetric under `c → −c`.

## Measurements

| candidate | \|push\| | phase | amplitude | rank |
|---|---|---|---|---|
| 1 occupancy imbalance | 1.368E+000 | 7.155E-001 | 1.166E+000 | 1 |
| 2 phase imbalance | 9.246E-015 | 9.246E-015 | 6.391E-029 | 0 |
| 3 amplitude-phase coupling | 8.295E-016 | 7.000E-016 | 4.450E-016 | 0 |
| 4a actualization pressure (uniform) | 9.798E+000 | 0.000E+000 | 0.000E+000 | 0 |
| 4b actualization pressure (local rate) | 3.407E-001 | 6.969E-003 | 3.407E-001 | 1 |
| 5 spectral mismatch | 2.572E-002 | 2.388E-016 | 2.572E-002 | 0 |
| 6 boundary assignment | 1.575E-002 | 9.282E-003 | 1.272E-002 | 1 |

| measurement | value |
|---|---|
| the audited state's own phase content | **9.246E-015** (phase-free) |
| the difference's multiplier identity | **exact** on 42 of 42 visible modes |
| amplitude residue range | **2.141E-003 … 1.998E+000** |
| creating sources / amplifying sources | **3 / 3** |
| creating sources' only fixed point | **the uniform state** |
| amplifiers move exactly the phase-bearing states | **True** |
| phase-heaviest candidate / ratio | **boundary assignment / 7.296E-001** |
| degenerate pairs | **0** |

## Notes

**A claim is withdrawn.** The draft called the difference a **pure rotation** (no amplitude residue); the measurement
refused it - the residue reaches **1.998E+000**, the near-maximum `2 sin²(δ/2)` at channel 47.

**An accounting is corrected.** G_057 recorded the actualization's phase velocity as **0.000E+000**, but returned it from
two **side conditions** without measuring the phase content of the time-like component it names. Measured directly the
**uniform** reading is **0.000E+000** (conclusion holds), while the **local clock-rate** reading is **6.969E-003** (not a
floor) - and **AT defines no update rule for the organisation**, so the identification carries the weight.
