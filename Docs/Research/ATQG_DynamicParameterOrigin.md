# AT-QG Phase 101 — Parameter Origin from Network Dynamics

**Program:** AT-QG (Unification)
**Phase:** 101 — can masses/couplings/mixing angles emerge from stable dynamic activity patterns?
**Status:** COMPLETED — 3/3 xUnit tests pass (306/306 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether masses/couplings/mixing angles can emerge from stable dynamic activity patterns rather than static geometry. Classify: NO RELATION / PARTIAL RELATION / DYNAMIC ORIGIN.

---

## 2. Actualization-rate patterns & dynamic attractors (ATQG1010)

The network has genuine dynamics — actualization activity (QG89) and RG attractors (QG88) — providing a dynamic
substrate for a parameter origin.

---

## 3. Oscillatory states, metastable, parameter families (ATQG1011)

Dynamics provides an organizing structure (frequencies, attractor families), but no native dynamics selects the
specific SM parameter values.

---

## 4. Classification (ATQG1012)

**PARTIAL RELATION.**

- NOT NO RELATION: actualization dynamics, attractors, and oscillations are real;
- NOT DYNAMIC ORIGIN: no native dynamics is identified whose activity pattern equals the SM parameters;
- PARTIAL RELATION: real dynamics + organizing structure, without value selection.

---

## 5. Conclusion

Network dynamics gives a **PARTIAL RELATION** to parameters (organizing structure, not dynamic origin).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1010 `ATQG1010_RatesAndAttractors` | PASS (dynamics exist) |
| ATQG1011 `ATQG1011_OscillationMetastableFamilies` | PASS (organizing, no selection) |
| ATQG1012 `ATQG1012_Classification` | PASS (PARTIAL RELATION) |

Code: `AT.Core/ResearchXH/DynamicParameterOrigin.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase101_DynamicParameterOriginTests.cs`.
