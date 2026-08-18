# TQM-QG Phase 74 — General Measurement Basis

**Program:** TQM-QG (Unification)
**Phase:** 74 — can actualization reproduce arbitrary quantum measurement bases?
**Status:** COMPLETED — 3/3 xUnit tests pass (225/225 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

QG73 showed actualization reproduces binary collapse. Here we ask whether it reproduces arbitrary bases. Classify:
MATCH / PARTIAL / NO MATCH.

---

## 2. Multi-state actualization (TQMQG740)

With θ (continuous phase) and S (spin), a node's state space is **multi-dimensional** — actualization projects onto a
general state, not just tick/no-tick.

---

## 3. Basis rotation + POVM (TQMQG741)

- an arbitrary basis {|φ_i⟩} is mapped to the actualization basis by a **unitary rotation** (θ + S + J);
- the most general (POVM) measurements use extra nodes as **ancillas** (Naimark dilation);
- the Born rule holds in any basis.

---

## 4. Classification (TQMQG742)

**MATCH.**

Multi-state actualization + unitary basis rotation + POVM via ancillas reproduce arbitrary quantum measurement
bases. This resolves the residual binary limitation of QG73. (Requires the full quantum structure θ + S + J, all
already present.)

---

## 5. Conclusion

Actualization now reproduces **arbitrary quantum measurement bases** — the full measurement process is recovered.
This closes the quantum measurement arc (QG72–74): the binary collapse (QG73) generalizes to arbitrary bases (QG74)
via unitary rotation and POVM, completing the quantum sector.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG740 `TQMQG740_MultiStateActualization` | PASS (multi-state) |
| TQMQG741 `TQMQG741_BasisRotationAndPovm` | PASS (unitaries + POVM) |
| TQMQG742 `TQMQG742_Classification` | PASS (MATCH) |

Code: `TQM.Core/ResearchXH/GeneralMeasurement.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase74_GeneralMeasurementTests.cs`.
