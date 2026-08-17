# TQM-QG Phase 13 — Horizon Thermodynamics

**Program:** TQM-QG (Unification)
**Phase:** 13 — can a Hawking-like temperature emerge?
**Status:** COMPLETED — 3/3 xUnit tests pass (42/42 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

S ∝ Area is derived (QG12). Here we test whether a Hawking-like temperature T ∝ 1/R emerges from the first law
T = dE/dS, via horizon microstate counting, entropy gradients, information flow, deficit horizons, and the
temperature–entropy relation. Classify: MATCH / PARTIAL MATCH / NO MATCH.

---

## 2. Results

### (a) Entropy and its gradient (TQMQG130)

S ∝ R^(d−1) (area, ratio 4 at d=3) and dS/dR ∝ R^(d−2) (ratio 2 at d=3) — both correct from counting.

### (b) First-law temperature — the contrast (TQMQG131)

T = dE/dS = (dE/dR)/(dS/dR):

| R | T_deficit (E ∝ R³) | T_hawking (E ∝ R) |
|---|---|---|
| 1 | 1.50 | 0.500 |
| 2 | 3.00 | 0.250 |
| 4 | 6.00 | 0.125 |
| 8 | 12.00 | 0.063 |

- TQM's deficit energy E ∝ R^d (volume) gives **T ∝ R** (ratio 2, *grows*).
- Schwarzschild energy E ∝ R gives **T ∝ 1/R** (ratio 0.5, *falls*) — Hawking.

### (c) Classification (TQMQG132)

**NO MATCH for T ∝ 1/R.**

---

## 3. Classification: NO MATCH (temperature)

- The entropy S ∝ R^(d−1) (area) is the MATCH (QG12), with correct gradient dS/dR ∝ R^(d−2).
- The first law T = dE/dS requires E(R). TQM's native deficit energy E ∝ R^d (volume) gives T ∝ R — the
  temperature GROWS with radius, the opposite of Hawking.
- Hawking's T ∝ 1/R requires the Schwarzschild mass relation E ∝ R (mass linear in radius), which TQM's
  volume-scaled counting does not provide.
- **Root cause:** TQM's counting measure makes "mass" a VOLUME quantity (enclosed deficit ∝ R^d), whereas
  black-hole mass is a SURFACE/radius quantity (M ∝ R). A native T ∝ 1/R would require a holographic mass
  definition (mass from horizon area, not enclosed volume).

---

## 4. Conclusion

The **entropy law S ∝ Area is native** (QG12), but the **temperature T ∝ 1/R does NOT emerge** — TQM's
volume-scaled deficit energy gives T ∝ R. This is a clean negative result pointing to a specific gap: TQM needs
a *holographic* mass definition (mass from horizon area, M ∝ R) to reproduce Hawking's T ∝ 1/R and the full
black-hole thermodynamics. The failure is the same mass-radius discrepancy already flagged in QG12.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG130 `TQMQG130_EntropyAndGradient` | PASS (S ∝ R², dS/dR ∝ R) |
| TQMQG131 `TQMQG131_FirstLawTemperature` | PASS (deficit → T ∝ R; Schwarzschild → T ∝ 1/R) |
| TQMQG132 `TQMQG132_Classification` | PASS (NO MATCH) |

Code: `TQM.Core/ResearchXH/HorizonThermodynamics.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase13_HorizonThermodynamicsTests.cs`.
