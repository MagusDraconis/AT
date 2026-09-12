# Y_G_017_Result.md — ResearchY-G_017 Metric Coupling Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_017_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.03 s) — group G total 154/154 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_017"`

## Summary

**Question:** does any experimentally realizable |ψ|² profile produce a measurable clock shift? (systems:
optical cavity, resonator array, photon lattice, oscillator network; measure Δτ, ΔΦ, clock signal; compare AT,
GR, observed limits)
**Answer:** the **substrate** reading is **DERIVED** and consistent; the **laboratory identification** is a
**BOUNDARY** input and its naive form is **REFUTED** — 0.77 – 3.07 (77 – 307 %) predicted against a 1e-18
ceiling, with AT-naive and GR differing by **1.9e46 … 4.3e49** and observation agreeing with GR.

## Detail — the three readings

| reading | law | status |
|---|---|---|
| AT-substrate | ρ = the actualization density: `Δτ/τ = ΔΦ/c² = (1/d)Δlnρ` | **DERIVED** — 0.99600 (G_004), 0.99668 (G_009) |
| AT-naive (lab) | a lab intensity profile *is* ρ, so `ρ ∝ I` | **BOUNDARY** identification → **REFUTED** |
| GR | `m = U/c²`, `ΔΦ/c² = G U/(R c⁴)` | observation agrees |

## Detail — the four systems

| system | contrast | AT-naive `Δτ/τ` | GR `ΔΦ/c²` | AT/GR | exclusion @1e-18 |
|---|---|---|---|---|---|
| optical cavity (F = 1e6, 1 W, 0.3 m) | Gaussian 1e2 | **0.6666666667** | 3.509234e-47 | **1.900e46** | **6.667e17** |
| resonator array (Q = 1e7, 1 mW, 1550 nm) | 1e4 | **3.0701134573** | 7.071071e-50 | **4.342e49** | **3.070e18** |
| photon lattice (Sr, I = 2.439413e8 W/m²) | 1e3 | **2.3025850930** | — | large | **2.303e18** |
| oscillator network (Q = 1e6, 1 pW, 6 GHz) | 10 : 1 | **0.7675283643** | 3.533090e-58 | **2.17e57** | **7.675e17** |

Stored energies: cavity `U = 6.370605e-4 J`; resonator `U = 8.228698e-12 J` (`u = 2.209714e6 J/m³`);
network `U = 2.652582e-17 J` (`u = 2.652582e-8 J/m³`).

## Detail — the lattice-clock anchor (sharpest test)

Sr lattice clock: `λ = 813 nm`, depth `100 E_rec` (`E_rec = 2.272842e-30 J`), 300 a.u. polarizability →
`I = 2.439413e8 W/m²`; the clock reads **1e-18**.

| node/antinode contrast | `Δln I` | AT-naive | exclusion @1e-18 |
|---|---|---|---|
| 1e2 | 4.605170 | 1.53505673 | 1.5351e18 |
| 1e3 | 6.907755 | **2.30258509** | **2.3026e18** |
| 1e4 | 9.210340 | 3.07011346 | 3.0701e18 |
| 1e6 | 13.815511 | 4.60517019 | 4.6052e18 |

AT-naive predicts a **230.26 %** shift across the lattice of the very clock that tests it.

## Detail — exclusion against every ceiling

| ceiling | cavity | resonator | lattice | network |
|---|---|---|---|---|
| 1e-12 | 6.667e11 | 3.070e12 | 2.303e12 | 7.675e11 |
| 1e-15 | 6.667e14 | 3.070e15 | 2.303e15 | 7.675e14 |
| **1e-18** | **6.667e17** | **3.070e18** | **2.303e18** | **7.675e17** |
| 1e-19 | 6.667e18 | 3.070e19 | 2.303e19 | 7.675e18 |

No ceiling exists at which any realizable profile survives. The failure is **structural**: the law is
logarithmic, so `Δτ/τ = (1/3)ln(contrast)` is O(1) for every realizable contrast (> 10).

## The critical answer

> **NO.** No experimentally realizable |ψ|² produces a measurable AT metric clock shift. The four systems span
> **0.7675 – 3.0701 (77 – 307 %)** against a **1e-18** ceiling; the effect that exists is the **substrate's**
> and is consistent with GR.

The identification premise flagged as residual in G_014/G_015 is therefore not merely unproven — it is
**experimentally excluded at bench scale** (2.303e18 by the lattice-clock experiment). This is G_011b's "the
coupling is not borrowed" promoted to a measured exclusion.

## Classification and caveats

**No reclassification.** G_004/G_005/G_009/G_011b/G_014/G_015/G_016/G_016b unchanged inputs. D_040 untouched;
no canonical claim, value or equation changes; no new primitive. Deterministic, no randomness.

* G_014's PHYSICAL verdict is about **κ = 1 structure** (which observable to measure), not about the coupling —
  consistent with G_011b.
* The ceiling 1e-18 is state of the art; the verdict is insensitive to it (1e-12 → 1e-19 leaves 11–19 orders).
* The systems use ordinary powers (1 W, 1 mW, 1 pW) and standard cavity/resonator expressions.

## Open problems (OP1–OP5)

1. A **pre-registered** clock experiment phrasing a bound on `Δν/ν` versus optical intensity?
2. Can 2.303e18 be turned into a **quantitative bound on the identification coupling**?
3. Is there any system with a controlled **near-unity intensity contrast** (the only regime that could hide it)?
4. Can a **differential** test separate the substrate reading from GR at any contrast?
5. Does G_016b's **unbounded fixed-energy family** have a laboratory counterpart through a *non-intensity*
   ρ-carrying observable?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_017.md`
* `AT.Tests/Shared/PhysicalUnits.cs`
