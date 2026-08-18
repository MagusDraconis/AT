# TQM-QG Phase 89 — Origin of Energy

**Program:** TQM-QG (Unification)
**Phase:** 89 — what is energy in the network?
**Status:** COMPLETED — 3/3 xUnit tests pass (270/270 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine what energy is in the network. Classify: DERIVED / COMPATIBLE / NEW SECTOR.

---

## 2. Actualization rate & link-update activity (TQMQG890)

Network time = causal order (from Q-events). Energy is the conserved generator of time translation — the conjugate
of causal-order evolution — measured as the actualization rate (Q-event activity). Link-update activity carries its
flux.

---

## 3. Excitation, mass-energy equivalence, conservation (TQMQG891)

Energy is stored in ψ/ρ excitation; E = mc² links the Higgs condensate (rest mass) to energy; conservation follows
from time-translation symmetry (Noether). The concept is derived; specific values remain empirical.

---

## 4. Classification (TQMQG892)

**DERIVED** (the concept).

- DERIVED: energy = the conserved generator of causal-order evolution (Noether), not an extra postulate;
- NOT NEW SECTOR: no new representation required;
- NUANCE: specific energy VALUES (Hamiltonian, masses) remain empirical (QG85).

---

## 5. Conclusion

The CONCEPT of energy is **DERIVED**; its values are postulatory.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG890 `TQMQG890_ActualizationAndActivity` | PASS (energy = actualization generator) |
| TQMQG891 `TQMQG891_ExcitationEquivalenceConservation` | PASS (E=mc², Noether) |
| TQMQG892 `TQMQG892_Classification` | PASS (DERIVED) |

Code: `TQM.Core/ResearchXH/OriginOfEnergy.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase89_OriginOfEnergyTests.cs`.
