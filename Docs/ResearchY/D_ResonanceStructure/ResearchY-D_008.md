# ResearchY-D_008 — Reference Unit Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_008 (permanent)
**Title:** Reference Unit Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_008.md`
**Depends on:** ResearchY-D_007 (Planck-scale result)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_008_Tests.cs`

---

## Purpose

Search for the **first natural reference unit** in D96: what object plays the role of
light or an atomic clock? Determine whether any candidate provides a **natural clock,
ruler, or energy unit** without external calibration.

## Accepted (from D_007)

- The dimensionless Planck structure is DERIVED; the absolute scale (M_Pl = v·A³) and the
  SI value (G = ħc/M_Pl²) require external anchors (v, c, ħ) — BOUNDARY.

---

## The Six Candidates

| # | Candidate | Value | Dimensionless? |
|---|---|---|---|
| 1 | actualization tick (Q-event) | the count unit | yes |
| 2 | closure cycle N=96 | the full cycle | yes |
| 3 | zero mode | λ₀ = 0, ω₀ = 0 | yes |
| 4 | fundamental doublet | ω₁ = 0.6216 | yes |
| 5 | spectral gap | λ₂ = 0.3864 | yes |
| 6 | resonant pair structure | 47 Z2 pairs | yes |

**Key relation:** ω₁² = λ₂ (0.6216² = 0.3864) — the fundamental doublet frequency
squared is the spectral gap. The doublet and the gap are the same content in two
readings (frequency vs eigenvalue).

---

## Test: natural clock, ruler, or energy unit?

### A) Natural clock

An atomic clock is a physical transition frequency (e.g., Cs-133 hyperfine =
9.192631770 GHz) — a *physical* frequency with units (Hz).

- D96 provides a **dimensionless frequency**: ω₁ = 0.6216 (the fundamental doublet).
  This is a *pure number* — it defines a *relative* frequency reference (ratios,
  ordering), not a physical clock.
- The closure cycle N=96 provides a **dimensionless periodicity** (the full cycle) —
  an ordering reference, not a physical time unit.
- A *physical* clock (seconds) requires an external unit anchor (the energy/unit scale)
  — BOUNDARY (D_007).
- **Classification: EMERGENT (dimensionless frequency reference); the physical clock is
  BOUNDARY.**

### B) Natural ruler

The light-based meter is c = 299792458 m/s — a physical constant defining length via
light travel time.

- D96 provides **dimensionless ratios** (span = 6.40, moments, occMom) — a *relative*
  ruler (ordering of the spectrum), not a physical length.
- A *physical* meter requires c (imported) — BOUNDARY.
- **Classification: EMERGENT (dimensionless ratio ruler); the physical ruler is
  BOUNDARY.**

### C) Natural energy unit

D96 provides dimensionless spectral content; a physical energy unit (J, GeV) requires
the anchor v (D_007: M_Pl = v·A³) and the SI constants (c, ħ).

- **Classification: BOUNDARY (physical energy unit requires external calibration).**

---

## Separation: ordering vs dimensionless frequency vs physical unit

| Level | What it provides | Classification |
|---|---|---|
| ordering only | the spectral ordering (which frequency is higher) | **DERIVED** |
| dimensionless frequency | ω₁ = 0.6216, ω₁² = λ₂ = 0.3864 (pure numbers) | **DERIVED** |
| physical unit | Hz, m, J — requires external calibration (v, c, ħ) | **BOUNDARY** |

The D96 candidates provide the first two levels (derived); the third (physical units) is
external (boundary).

---

## Comparison with atomic clock and light-based meter

| Reference | Physical basis | Unit | In D96? |
|---|---|---|---|
| atomic clock | Cs-133 hyperfine transition | Hz (9.19 GHz) | dimensionless analogue only (ω₁ = 0.6216) |
| light-based meter | c = 299792458 m/s | m | dimensionless ratios only (span = 6.40) |
| D96 "natural reference" | the spectrum | — | dimensionless frequency/ordering/ratio (DERIVED); no physical unit (BOUNDARY) |

An atomic clock and the meter are **physical references with units**; the D96 candidates
are **dimensionless references** (frequency ratios, ordering, spectral ratios). They can
define *relative* references (which frequency, which ratio) but not *absolute* physical
units.

---

## Test: reference without external calibration?

**Can any candidate define a reference without external calibration?**

- **As a dimensionless reference (ordering, ratios): YES — DERIVED.** The spectral
  frequencies, ratios, and ordering are exact and calibration-free.
- **As a physical reference (seconds, meters, Joules): NO — BOUNDARY.** No candidate
  provides a physical unit without external calibration (the anchor v, the constants c
  and ħ, D_007).

The first natural reference unit of D96 is therefore the **dimensionless spectral
frequency** (ω₁ = 0.6216) — a derived, calibration-free *relative* reference — while any
*physical* unit requires the external calibration of D_007.

---

## Candidate Ranking

| Rank | Candidate | Role | Classification |
|---|---|---|---|
| 1 | fundamental doublet ω₁ = 0.6216 | the natural dimensionless frequency reference (best clock analogue) | DERIVED (frequency) / EMERGENT (as clock) / BOUNDARY (physical) |
| 2 | spectral gap λ₂ = 0.3864 | the natural dimensionless gap; ω₁² = λ₂ links it to the doublet | DERIVED (dimensionless) |
| 3 | closure cycle N=96 | the natural periodicity (full cycle) | DERIVED (ordering) |
| 4 | actualization tick | the count unit (ordering reference) | DERIVED (ordering) |
| 5 | resonant pair structure | 47 pairs (dimensionless structure) | DERIVED |
| 6 | zero mode | the reference state (no oscillation) | DERIVED (as reference) |

---

## Theorem

> **Theorem (D_008).** The first natural reference unit of D96 is the dimensionless
> spectral frequency, not a physical unit.
>
> *Proof sketch.* (1) The D96 candidates (actualization tick, closure cycle, zero mode,
> fundamental doublet ω₁, spectral gap λ₂, resonant pairs) are all dimensionless spectral
> quantities (Sections: candidates). (2) The fundamental doublet ω₁ = 0.6216 provides a
> dimensionless frequency reference; ω₁² = λ₂ = 0.3864 links it to the spectral gap. (3)
> Dimensionless references (ordering, ratios, frequencies) are exact and calibration-free
> (DERIVED). (4) Physical units (Hz, m, J) require external calibration — the anchor v and
> the constants c, ħ (D_007) — BOUNDARY. Hence the first natural reference is the
> dimensionless spectral frequency; no physical unit is internal. ∎

---

## Dependency Graph

```
D_007 (Planck scale: dimensionless DERIVED, absolute scale BOUNDARY)
  → D_008: reference unit
  ├── actualization tick / closure cycle / zero mode / doublet / gap / pairs
  ├── dimensionless frequency reference ω₁ = 0.6216 → DERIVED (relative reference)
  ├── physical clock/ruler/energy → BOUNDARY (external calibration v, c, ħ)
  └── comparison with atomic clock & meter → dimensionless analogue only
```

---

## Research Conclusions

1. **The candidates are all dimensionless.** No D96 object carries physical units (Hz, m,
   J).
2. **The first natural reference is the dimensionless spectral frequency** (ω₁ = 0.6216)
   — a derived, calibration-free *relative* reference.
3. **Natural clock:** dimensionless frequency (EMERGENT); physical clock (BOUNDARY).
4. **Natural ruler:** dimensionless ratios (EMERGENT); physical ruler (BOUNDARY).
5. **Natural energy unit:** BOUNDARY (requires external calibration, D_007).
6. **Comparison with atomic clock/meter:** the D96 reference is a dimensionless analogue,
   not a physical reference. It can order and ratio the spectrum, but absolute physical
   units remain external.

---

## Open Problems

1. **Dimensionless-to-physical bridge (D_008 OP1).** Is there any derivation from the
   dimensionless frequency ω₁ to a physical frequency, or is the physical clock
   permanently external? (Currently: BOUNDARY.)
2. **Unit anchor origin (D_007 OP1).** Can v (and hence a physical frequency/energy
   unit) be derived? (Currently: BOUNDARY/calibration.)
3. **ω₁ as the fundamental reference (D_008 OP3).** The fundamental doublet is the
   natural frequency reference (A_001: first-peak candidate). Is its reference role
   canonical or conventional? (The frequency is derived; the choice to reference it is
   conventional.)

---

## Next Steps

- **ResearchY-D_009 (or synthesis):** the reference-unit audit (this) completes the
   natural-reference search; a synthesis with D_007 can map the dimensionless-to-physical
   boundary.
- **ResearchY-A_001 follow-up:** the fundamental doublet's role as the first-peak
   candidate connects to the frequency-reference question.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_008_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_008_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_008_Candidates` | six candidates are dimensionless spectral quantities | ✅ |
| `Y_D_008_ClockRulerEnergy` | natural clock/ruler/energy classification | ✅ |
| `Y_D_008_OrderingVsUnit` | ordering/dimensionless DERIVED; physical unit BOUNDARY | ✅ |
| `Y_D_008_AtomicClockComparison` | dimensionless analogue only (no physical Hz/m) | ✅ |
| `Y_D_008_ExternalCalibration` | reference without calibration: dimensionless YES, physical NO | ✅ |
| `Y_D_008_Run` | Research report | ✅ |

**Conclusion:** the first natural reference unit of D96 is the dimensionless spectral
frequency (ω₁ = 0.6216) — DERIVED as a relative reference; physical clock/ruler/energy
units require external calibration (v, c, ħ) — BOUNDARY. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_008"`

---

## References

- ResearchY-D_007 (Planck scale: dimensionless vs absolute), D_003 (resonance
  observables), A_001 (fundamental doublet).
- Monograph V2.0: Ch6 (D96 spectrum), Ch10 (gravity).
- AT-QG: QG181 (Newton constant).
