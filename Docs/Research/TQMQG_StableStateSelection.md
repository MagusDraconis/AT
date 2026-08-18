# TQM-QG Phase 96 — Stable State Selection

**Program:** TQM-QG (Unification)
**Phase:** 96 — does the network possess preferred stable states whose spectra could select physical parameters?
**Status:** COMPLETED — 3/3 xUnit tests pass (291/291 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether the network possesses preferred stable states whose spectra could select physical parameters. Classify: NO SELECTION / PARTIAL SELECTION / STATE SELECTION.

---

## 2. Energy minima & stable modes (TQMQG960)

Stable modes exist (QG95), but there is NO native energy functional whose minima select a preferred state — energy
is derived as a concept (QG89), not as a native selection functional.

---

## 3. Attractors, spectrum selection, metastable states (TQMQG961)

Stability and RG attractors PARTIALLY select/narrow the region, but nothing selects a unique preferred state whose
spectrum equals the SM parameters.

---

## 4. Classification (TQMQG962)

**PARTIAL SELECTION.**

- NOT NO SELECTION: stability + RG attractors do narrow the allowed region;
- NOT STATE SELECTION: no unique preferred state is selected;
- PARTIAL SELECTION: stability/attractors partially select; full state selection is absent.

---

## 5. Conclusion

The network gives a **PARTIAL SELECTION** of parameter values (not a unique state selection).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG960 `TQMQG960_EnergyMinimaAndModes` | PASS (no native functional, modes exist) |
| TQMQG961 `TQMQG961_AttractorsSpectrumMetastable` | PASS (partial, not full selection) |
| TQMQG962 `TQMQG962_Classification` | PASS (PARTIAL SELECTION) |

Code: `TQM.Core/ResearchXH/StableStateSelection.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase96_StableStateSelectionTests.cs`.
