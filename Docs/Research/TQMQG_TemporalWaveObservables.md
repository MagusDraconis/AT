# TQM-QG Phase 20 — Temporal-Wave Observables

**Program:** TQM-QG (Unification)
**Phase:** 20 — can temporal (time-rate) waves generate the LIGO/Virgo observables?
**Status:** COMPLETED — 3/3 xUnit tests pass (63/63 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

TRM predicts propagating time-rate oscillations. QG18 ruled out scalar breathing modes as a GR tensor
replacement. Here we test whether temporal waves can generate the same detector observables (clock-rate
oscillations, interferometer arm phase shifts, round-trip light travel times, waveform propagation, binary-merger
signals). Classify: MATCH / PARTIAL MATCH / NO MATCH.

---

## 2. Results

### (a) Round-trip light travel time is conformally invariant (TQMQG200)

In g = ρ^(2/d)η, g_00 = −ρ^(2/d) and g_ii = ρ^(2/d) multiply **equally**, so the light-cone ds²=0 is conformally
invariant — the conformal factor ρ cancels out of null geodesics. The round-trip time τ = 2L is **independent of
ρ**, so a temporal wave δρ produces **zero** change in light travel time.

### (b) Breathing vs tensor differential strain (TQMQG201)

LIGO measures the **differential** arm strain. A breathing (scalar) mode stretches both arms equally
(common-mode) → zero differential signal; a tensor (+) mode stretches one arm and squeezes the other →
differential 2h₀. The temporal wave is a breathing mode: **invisible** to a Michelson interferometer.

### (c) Classification (TQMQG202)

**NO MATCH.**

---

## 3. Classification: NO MATCH

- A temporal wave is a conformal (scalar) disturbance of ρ; null geodesics are conformally invariant, so it
  produces zero change in light round-trip times — an interferometer sees nothing.
- Even ignoring that, the breathing mode is common-mode (both arms stretch equally), so the differential phase
  (the LIGO observable) is zero.
- GR tensor waves (+/×) are differential (one arm stretches, the other squeezes), producing the observed signal.

---

## 4. Conclusion

Temporal waves do **not** generate the LIGO/Virgo observables: they are doubly invisible (conformally invariant
light travel + common-mode breathing). The observed gravitational waves are tensor (spin-2), consistent with
QG18/QG19: the scalar/temporal sector fails the polarization test, and reproducing GWs requires a new tensor
primitive. This closes the GW arc (QG18–QG20) with the decisive conclusion that **no scalar/temporal
interpretation can mimic the measured interferometer signal**.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG200 `TQMQG200_RoundTripConformalInvariance` | PASS (τ = 2L independent of ρ) |
| TQMQG201 `TQMQG201_DifferentialStrain` | PASS (breathing invisible, tensor visible) |
| TQMQG202 `TQMQG202_Classification` | PASS (NO MATCH) |

Code: `TQM.Core/ResearchXH/TemporalWaveObservables.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase20_TemporalWaveObservablesTests.cs`.
