# TQM-QG Phase 49 — Network-Mode Explanation of GW Strain

**Program:** TQM-QG (Unification)
**Phase:** 49 — can collective Q-event network modes reproduce the strain without ψ?
**Status:** COMPLETED — 3/3 xUnit tests pass (150/150 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

QG48 established only the strain h(t) is directly observed. Here we test whether COLLECTIVE modes of a Q-event
network can reproduce that strain without a fundamental ψ. Classify: IMPOSSIBLE / PARTIAL MATCH / FULL MATCH.

---

## 2. Breathing vs tensor (TQMQG490)

A Michelson interferometer measures the **differential** arm strain.

| mode | arm response | differential strain |
|---|---|---|
| scalar breathing | both arms stretch equally (common-mode) | **0** |
| tensor (+/×) | one arm stretches, the other squeezes | **2h₀** |

A scalar breathing mode is common-mode → invisible to a Michelson.

---

## 3. Collective modes are scalar (TQMQG491)

However many nodes and however synchronized, collective oscillation modes of the scalar density ρ are themselves
**scalar** — they can only form a breathing (monopole) wave, never the quadrupole (+/×) pattern of a spin-2 wave
(QG23/QG37: no scalar can source spin-2).

---

## 4. Classification (TQMQG492)

**IMPOSSIBLE.**

- NOT FULL MATCH: a scalar network mode is common-mode (zero differential).
- NOT PARTIAL for the observable: the breathing mode produces no differential detector output (QG20).
- IMPOSSIBLE: no scalar (collective or otherwise) can source the spin-2 +/× pattern; the fundamental ψ remains
  required.

---

## 5. Conclusion

Collective Q-event network modes **cannot** replace ψ: they are scalar, and the observed strain is a differential
quadrupole. This closes the last "emergent tensor" loophole — the graviton cannot be faked by network dynamics; it
is irreducibly the spin-2 primitive ψ.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG490 `TQMQG490_BreathingVsTensor` | PASS (common-mode vs differential) |
| TQMQG491 `TQMQG491_CollectiveModesAreScalar` | PASS (scalar, no +/×) |
| TQMQG492 `TQMQG492_Classification` | PASS (IMPOSSIBLE) |

Code: `TQM.Core/ResearchXH/NetworkModeGW.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase49_NetworkModeGWTests.cs`.
