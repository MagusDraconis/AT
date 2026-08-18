# TQM-QG Phase 101 — Parameter Origin from Network Dynamics

**Program:** TQM-QG (Unification)
**Phase:** 101 — can masses/couplings/mixing angles emerge from stable dynamic activity patterns?
**Status:** COMPLETED — 3/3 xUnit tests pass (306/306 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether masses/couplings/mixing angles can emerge from stable dynamic activity patterns rather than static geometry. Classify: NO RELATION / PARTIAL RELATION / DYNAMIC ORIGIN.

---

## 2. Actualization-rate patterns & dynamic attractors (TQMQG1010)

The network has genuine dynamics — actualization activity (QG89) and RG attractors (QG88) — providing a dynamic
substrate for a parameter origin.

---

## 3. Oscillatory states, metastable, parameter families (TQMQG1011)

Dynamics provides an organizing structure (frequencies, attractor families), but no native dynamics selects the
specific SM parameter values.

---

## 4. Classification (TQMQG1012)

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
| TQMQG1010 `TQMQG1010_RatesAndAttractors` | PASS (dynamics exist) |
| TQMQG1011 `TQMQG1011_OscillationMetastableFamilies` | PASS (organizing, no selection) |
| TQMQG1012 `TQMQG1012_Classification` | PASS (PARTIAL RELATION) |

Code: `TQM.Core/ResearchXH/DynamicParameterOrigin.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase101_DynamicParameterOriginTests.cs`.
