# TQM-F Phase 3 — Metric Origin

**Program:** TQM-F (Foundation)
**Phase:** 3 — can √(−g)=ρ emerge from counting-measure consistency alone?
**Status:** COMPLETED — 3/3 xUnit tests pass (9/9 TQM-F)
**Constraint:** no new primitives

---

## 1. Goal

The gravity chain depends on the metric origin √(−g)=ρ. Here we test whether it emerges uniquely from
counting-measure consistency (invariant counting, volume preservation, causal-set measure theory), and whether
alternatives exist. Classify: DERIVED / PREFERRED / ASSUMED.

---

## 2. Results

### (a) The count and the metric volume are both measures (TQMF30)

The counting measure N[a,b] = ∫ρ dx and the metric volume V[a,b] = ∫√(−g) dx are both additive (measures), and
√(−g)=ρ makes them **identical** for every region — the causal-set "number = volume" principle.

### (b) √(−g)=ρ is the unique consistent volume element (TQMF31)

| √(−g) | max mismatch vs count |
|---|---|
| **ρ** | **0 (exact)** |
| ρ² | ≈ 0.6 |
| √ρ | ≈ 0.3 |
| const | ≈ 0.5 |

Requiring the metric volume measure to equal the counting measure **uniquely** selects √(−g)=ρ; every
alternative over- or under-counts the events.

### (c) Classification (TQMF32)

**DERIVED (unique form), with a PREFERRED identification.**

---

## 3. Classification: DERIVED (unique form), PREFERRED (identification)

- The **form** √(−g)=ρ is **DERIVED**: it is the unique volume element making the metric volume measure equal
  the counting measure for every region — no alternative works.
- The **identification** "counting measure = volume element" is the causal-set "number = volume" principle: in
  an event-based theory the only measure over spacetime is the count of events, so it must be the volume
  element. This is **PREFERRED** (minimal/definitional, no new structure).

This **upgrades metric origin from PREFERRED (TQM-F0) to DERIVED-in-form (unique)**, leaving only the
"number = volume" principle as the one remaining structural identification.

---

## 4. Conclusion

The metric origin √(−g)=ρ is **derived in form** — it is the unique way to make the geometry's volume element
coincide with the native counting measure — and **preferred in identification** (the causal-set "number =
volume" principle, which in an event-based theory is the only available measure). Combined with G4-A0 (the
exponent 2/d derived from √(−g)=ρ) and G4-A1 (conformal flatness = minimum-information), the metric structure
of the gravity program now rests on a single remaining identification: "number = volume".

---

## Test program

| Test | Verdict |
|---|---|
| TQMF30 `TQMF30_VolumePreservation` | PASS (count & volume are measures; equal under √(−g)=ρ) |
| TQMF31 `TQMF31_Uniqueness` | PASS (√(−g)=ρ unique; alternatives fail) |
| TQMF32 `TQMF32_Classification` | PASS (DERIVED form; PREFERRED identification) |

Code: `TQM.Core/ResearchXH/MetricOrigin.cs`;
tests `TQM.Tests/ResearchXH/TQMF_Phase3_MetricOriginTests.cs`.
