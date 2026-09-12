# ResearchY-G_017 — Metric Coupling Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_017 (permanent)
**Title:** Metric Coupling Audit — does any experimentally realizable |ψ|² profile produce a measurable clock shift?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_017.md`
**Depends on:** ResearchY-G_014 (the κ = 1 carrier and the identification premise), G_015 (the metric price list),
G_016/G_016b (ρ is more primitive than mass-energy; ρ is free of E, not the reverse), G_004 (the calibration
AT ≡ GR at 0.99600), G_005 (the Poisson band), G_009 (the clock law and the galactic cross-check), G_011b (the
metric coupling is not borrowed); AT-QG QG197 (`g₀₀ = −ρ^(2/d)`), QG220 (ψ = √ρ e^{iθ})
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_017_Tests.cs` (7/7 PASSED, ~0.03 s)

## Purpose

G_015 established the metric *price list* for laboratory configurations. G_017 asks the question in the form a
laboratory actually poses it — with a **real optical |ψ|²**, where the intensity *is* the measured field and its
contrast is enormous:

> **Does any experimentally realizable |ψ|² profile produce a measurable clock shift?**
> Systems: optical cavity, resonator array, photon lattice, oscillator network.
> Measure: `Δτ`, `ΔΦ`, the clock signal.     Compare: **AT prediction, GR prediction, observed limits**.
> Output: **DERIVED / BOUNDARY / REFUTED**.

**Answer.** For the *substrate* reading the law is **DERIVED** and observationally consistent. For a
**laboratory** field the identification is a **BOUNDARY** input — and the naive reading is **REFUTED**, not
merely unproven: it predicts **0.77 – 3.07 (77 – 307 %)** clock shifts where GR predicts **1e-47 … 1e-58**, a
mismatch of **1.9e46 to 4.3e49**, while the observation agrees with GR.

## 1. The three readings

| reading | law | status |
|---|---|---|
| **AT-substrate** | ρ is the actualization density of the substrate (G_014): `Δτ/τ = ΔΦ/c² = (1/d)Δlnρ` | **DERIVED** and consistent (G_004's 0.99600; G_009's 0.99668) |
| **AT-naive (lab)** | a laboratory intensity profile *is* ρ, i.e. `ρ ∝ I`, so `Δτ/τ = (1/d)Δln I` | **BOUNDARY** identification → **REFUTED** by observation |
| **GR** | the field's stored energy `U` with `m = U/c²`: `ΔΦ/c² = G U/(R c⁴)` | the standard reading; observation agrees with it |

The observable is a **fractional frequency ratio**, so the ceiling is a clock's *fractional resolution*:
**1e-18** for the best optical clocks (1e-19 in the newest), 1e-12 for a crude laboratory systematic.

## 2. DERIVED — the substrate reading is not refuted

| case | `Δlnρ/d` | × the 1e-18 floor |
|---|---|---|
| Earth's surface (`6.96133e-10`) | **2.320443e-10** | **2.320e8** (G_004: AT/GR = 0.99600) |
| galactic field (`1.6102e-6`) | **5.367333e-7** = **0.046374 s/day** | **5.367e11** (G_009 cross-check 0.99668) |

So the *law* and its application to the substrate are derived and observationally supported. Nothing below
touches this.

## 3. The four systems — AT-naive vs GR vs the observed ceiling

| system | realizable contrast | **AT-naive `Δτ/τ`** | **GR `ΔΦ/c²`** | **AT/GR** | **exclusion @ 1e-18** |
|---|---|---|---|---|---|
| optical cavity (F = 1e6, 1 W, 0.3 m) | Gaussian 1e2 | **0.6666666667** | 3.509234e-47 | **1.900e46** | **6.667e17** |
| resonator array (Q = 1e7, 1 mW, 1550 nm) | on/off 1e4 | **3.0701134573** | 7.071071e-50 | **4.342e49** | **3.070e18** |
| photon lattice (Sr clock, I = 2.439413e8 W/m²) | node/antinode 1e3 | **2.3025850930** | — | large | **2.303e18** |
| oscillator network (Q = 1e6, 1 pW, 6 GHz) | 10 : 1 | **0.7675283643** | 3.533090e-58 | **2.17e57** | **7.675e17** |

Stored energies recomputed from the system parameters: cavity `U = 6.370605e-4 J` (F·P_in/π × 2L/c); resonator
`U = 8.228698e-12 J` (`QP/ω`, `u = 2.209714e6 J/m³`, `R = 9.615433e-7 m`); network
`U = 2.652582e-17 J` (`u = 2.652582e-8 J/m³`, `R = 6.203505e-4 m`).

## 4. The sharpest test — a lattice clock inside its own standing wave

A Sr optical lattice clock (`λ = 813 nm`, depth `100 E_rec`, 300 a.u. polarizability) operates at

```
I = 2.439413e8 W/m² at the antinode     (E_rec = 2.272842e-30 J, verified)
```

and reads a reproducible frequency to **1e-18** — that is, the very experiment that best tests the
identification does so every day, to 18 digits:

| node/antinode contrast | `Δln I` | AT-naive `Δτ/τ` | exclusion @ 1e-18 |
|---|---|---|---|
| 1e2 | 4.605170 | **1.53505673** | 1.5351e18 |
| 1e3 | 6.907755 | **2.30258509** | 2.3026e18 |
| 1e4 | 9.210340 | **3.07011346** | 3.0701e18 |
| 1e6 | 13.815511 | **4.60517019** | 4.6052e18 |

AT-naive predicts a **230.26 %** shift across the lattice; the clock measures **1e-18**. The naive
identification is excluded by **2.303e18** by the best available experiment.

## 5. Exclusion against every ceiling

| ceiling | cavity | resonator | lattice | network |
|---|---|---|---|---|
| 1e-12 | 6.667e11 | 3.070e12 | 2.303e12 | 7.675e11 |
| 1e-15 | 6.667e14 | 3.070e15 | 2.303e15 | 7.675e14 |
| **1e-18** | **6.667e17** | **3.070e18** | **2.303e18** | **7.675e17** |
| 1e-19 | 6.667e18 | 3.070e19 | 2.303e19 | 7.675e18 |

**There is no ceiling at which any realizable |ψ|² profile survives.** And the failure is *structural*, not a
matter of engineering: the law is **logarithmic**, so `Δτ/τ = (1/3)ln(contrast)` is **O(1) for every
realizable contrast** (> 10) and no tuning makes it small. A laboratory field cannot have a contrast below
about 1, and at contrast 1 there is no effect at all by definition.

## 6. Why the mismatch is structural

For the *same physical system* on the bench, the two readings differ by **46 to 49 orders of magnitude**:

```
optical cavity:   AT-naive 6.667e-1     vs GR 3.509e-47     -> 1.900e46
resonator array:  AT-naive 3.070e0      vs GR 7.071e-50     -> 4.342e49
oscillator net:   AT-naive 7.675e-1     vs GR 3.533e-58     -> 2.171e57
```

The observation agrees with GR. So the identification premise flagged as residual in G_014 and G_015 is not
merely *unproven*: **it is experimentally excluded at bench scale**, which is exactly why only the substrate
reading survives. This is G_011b's "the coupling is not borrowed" promoted from a structural statement to a
measured exclusion.

## 7. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the clock law `Δτ/τ = (1/d)Δlnρ` and its **logarithmic** form (depths add, ratios multiply), together with the **substrate** reading: Earth **2.320443e-10** (G_004's 0.99600) and galactic **5.367333e-7 = 0.046374 s/day** (G_009's 0.99668), at **2.320e8** and **5.367e11** × the 1e-18 floor. |
| **BOUNDARY** | the **identification** of a laboratory `\|ψ\|²` with ρ, and the **scale invariance** (only ratios of ρ are physical, G_009). The coupling is a boundary input — a readout does not borrow it (G_011b). |
| **REFUTED** | **any experimentally realizable \|ψ\|² profile producing a measurable AT metric clock shift**: excluded by **6.667e17 … 3.070e18** at the 1e-18 ceiling (and by **6.667e11 … 3.070e12** even at a crude 1e-12), with AT-naive differing from GR by **1.900e46 … 4.342e49** and the observation agreeing with GR. |

## 8. Critical answer

> **NO.** No experimentally realizable |ψ|² profile produces a measurable AT metric clock shift. The four
> systems span **0.7675 – 3.0701 (77 – 307 %)** against a **1e-18** ceiling; the effect that exists is the
> **substrate's**, and it is consistent with GR to the precision AT predicts.

The physical reason is that the AT clock law is a **log-ratio**: the prediction scales with `ln(contrast)`, and
every realizable optical contrast is large, so the prediction is always O(1) — it cannot be small. That makes
this the *most sensitive* test of the identification premise in the whole group, and the one the laboratory has
already performed.

## 9. Classification and caveats

**No reclassification.** G_014's κ = 1 identification, G_015's metric price list, G_016/G_016b's pairing and
independence results, G_004's calibration, G_005's band, G_009's clock law and G_011b's no-borrowed-coupling are
unchanged inputs. D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: exact algebra, no randomness.

* The consequence for the identification premise is stated plainly: for a **laboratory** field the premise is
  **false**, so "the lab's |ψ|² IS ρ" must be read as "the lab's |ψ|² is a *reading* of a substrate profile",
  not as an identity of source. G_014's PHYSICAL verdict is about **κ = 1 structure** (which observable to
  measure), not about the coupling — consistent with G_011b.
* The ceiling 1e-18 is a state-of-the-art optical clock; the verdict is insensitive to it (any ceiling from
  1e-12 to 1e-19 leaves 11–19 orders of exclusion).
* The four systems are worst-case-*free* constructions: total powers of 1 W, 1 mW and 1 pW are ordinary
  laboratory levels, and the intensities are computed from the standard cavity/resonator expressions.
* The audit does **not** claim that AT has no laboratory prediction: it claims that the only laboratory
  prediction of a *metric* clock shift (the naive reading) is excluded, while the substrate prediction is
  consistent.

## 10. Open problems (OP1–OP5)

1. Is there a **pre-registered** clock experiment whose systematics could be phrased as a bound on
   `Δν/ν` versus optical intensity, so the exclusion becomes a citable number rather than an inference?
2. Does the exclusion give a **quantitative bound on the identification coupling** — i.e. can the 2.303e18 be
   turned into a limit on the dimensionless coefficient in front of `(1/d)ln(contrast)`?
3. The **contrast floor**: since the law is logarithmic, only a contrast *below* about 1 could hide an effect.
   Is there any physical system with a near-unity, controlled intensity contrast?
4. Can a **differential** test separate the substrate reading from GR at any contrast, i.e. is G_015's OP3
   realizable at 1e-18?
5. Does G_016b's **unbounded fixed-energy family** have a laboratory counterpart through some *non-intensity*
   ρ-carrying observable, now that intensity is excluded?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_017_Tests.cs` — **7/7 PASSED** (~0.03 s)
**Group total:** G_001–G_017 = **154/154 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_017"`

| label | content |
|-------|---------|
| **DERIVED** | the logarithmic clock law and the substrate reading (2.320443e-10 and 5.367333e-7 = 0.046374 s/day, matching 0.99600 / 0.99668) |
| **BOUNDARY** | the identification `ρ = \|ψ\|²` for a laboratory field, and the scale invariance |
| **REFUTED** | any realizable \|ψ\|² metric clock shift — 6.667e17 … 3.070e18 exclusion; AT-naive vs GR = 1.900e46 … 4.342e49 |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_004.md`, `ResearchY-G_005.md`, `ResearchY-G_009.md`,
  `ResearchY-G_011b.md`, `ResearchY-G_014.md`, `ResearchY-G_015.md`, `ResearchY-G_016.md`, `ResearchY-G_016b.md`
* `Docs/ResearchY/Tests/Results/Y_G_017_Result.md`
* `AT.Tests/Shared/PhysicalUnits.cs`
