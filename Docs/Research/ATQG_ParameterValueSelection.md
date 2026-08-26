# AT-QG Phase 88 — Origin of Parameter Values

**Program:** AT-QG (Unification)
**Phase:** 88 — can dynamical selection principles determine preferred parameter values?
**Status:** COMPLETED — 3/3 xUnit tests pass (267/267 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether dynamical selection principles within the network can determine preferred parameter values. Classify: NO CONSTRAINT / PARTIAL CONSTRAINT / VALUE SELECTION.

---

## 2. Entropy extremization & stability criteria (ATQG880)

Entropy extremization is NOT native (an additional postulate). Stability IS native and bounds parameter ranges
(vacuum stability λ > 0, positive mass-squared) — a partial constraint.

---

## 3. Information minimization, criticality, attractors (ATQG881)

Information minimization and criticality are NOT native. RG attractors ARE native and relate/constrain couplings
(e.g. SU(3) asymptotic freedom), but no principle fully selects the specific values.

---

## 4. Classification (ATQG882)

**PARTIAL CONSTRAINT.**

- NOT NO CONSTRAINT: stability and RG flow do bound/relate values;
- NOT VALUE SELECTION: no native principle determines the specific 19 numbers;
- PARTIAL CONSTRAINT: stability bounds ranges; RG attractors relate couplings; the specific values stay free.

---

## 5. Conclusion

The network **PARTIALLY** constrains parameter values (bounds + relations), but does not select them.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG880 `ATQG880_EntropyAndStability` | PASS (stability bounds, entropy not native) |
| ATQG881 `ATQG881_MinimizationCriticalityAttractors` | PASS (RG constrains; no full selection) |
| ATQG882 `ATQG882_Classification` | PASS (PARTIAL CONSTRAINT) |

Code: `AT.Core/ResearchXH/ParameterValueSelection.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase88_ParameterValueSelectionTests.cs`.
