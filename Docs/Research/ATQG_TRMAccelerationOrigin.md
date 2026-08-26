# AT-QG Phase 41 — Derive the TRM Acceleration Law

**Program:** AT-QG (Unification)
**Phase:** 41 — can the √(g_N·a0) term emerge from Q-event saturation?
**Status:** COMPLETED — 3/3 xUnit tests pass (126/126 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

TRM predicts g_TRM = g_N + √(g_N·a0)/λ. We test whether the MOND-like √(g_N·a0) term emerges from Q-event
saturation. Classify: DERIVED / PARTIAL MATCH / IMPORTED.

---

## 2. Saturation has a core, not a √ regime (ATQG410)

Saturation gives g_sat = g_N·(1 − e^(−r³/r_c³)). The correction factor ∈ [0,1]:

| r | g_sat / g_N |
|---|---|
| 0.1 r_c | ≈ 0.001 (core) |
| 1.0 r_c | 0.632 |
| 10 r_c | ≈ 1 (Newtonian) |

Saturation **suppresses** gravity at the core (g_sat → 0 as r → 0) and recovers Newtonian at large r. There is no
1/r (flat-curve) regime, hence no √(g_N·a0) term.

---

## 3. Opposite sign (ATQG411)

| prescription | direction | regime |
|---|---|---|
| saturation g_sat/g_N ∈ [0,1] | **suppression** (≤ g_N) | core (small r) |
| MOND g_TRM/g_N ≥ 1 | **enhancement** (≥ g_N) | large r |

Saturation and MOND act in **opposite regimes with opposite sign** — they cannot be the same object.

---

## 4. Classification (ATQG412)

**IMPORTED.**

- NOT DERIVED: saturation gives a regular core (suppression), not the √(g_N·a0) ∝ 1/r enhancement, which carries a
  new scale a0.
- AT DOES derive flat rotation curves, but via the **log-deficit (α=0 scale-free)** profile (G4-ME Phases 3–4) —
  a DIFFERENT derived mechanism, neither saturation nor the exact √ interpolating form.
- The specific √(g_N·a0)/λ term is a MOND ansatz with scale a0 that Q-event saturation does not produce.

---

## 5. Conclusion

The √(g_N·a0) term does **not** emerge from saturation: saturation is a core **suppression**, MOND is a large-r
**enhancement**. The term is **IMPORTED** (a MOND ansatz). This is consistent with the final boundary audit (QG40):
AT's derived scalar sector gives the regular core (saturation) and flat curves (log-deficit), while the MOND
interpolating function is a separate, imported rotation-curve prescription.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG410 `ATQG410_SaturationHasCoreNoSqrt` | PASS (core, Newtonian recovery) |
| ATQG411 `ATQG411_OppositeSign` | PASS (suppression vs enhancement) |
| ATQG412 `ATQG412_Classification` | PASS (IMPORTED) |

Code: `AT.Core/ResearchXH/TRMAccelerationOrigin.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase41_TRMAccelerationOriginTests.cs`.
