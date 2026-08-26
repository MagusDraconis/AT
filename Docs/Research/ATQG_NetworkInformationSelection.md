# AT-QG Phase 110 — Network Information Selection

**Program:** AT-QG (Unification)
**Phase:** 110 — can information-processing capacity select a unique network class?
**Status:** COMPLETED — 3/3 xUnit tests pass (333/333 AT-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational audit of the native operator spectrum)

---

## 1. Goal

QG109 showed no unique physical network is selected by stability alone (PARTIAL SELECTION). This phase asks
whether INFORMATION-PROCESSING capacity — information flow, communication efficiency, causal depth, memory
capacity, and stable computation — can select a unique network class. Classify: NO EFFECT / PARTIAL SELECTION /
PHYSICAL SELECTION.

---

## 2. Information flow + communication efficiency (ATQG1100)

Measured over the 77-network deterministic ensemble:
- **Information flow** (log spanning-tree count / N): causal grids 1.38, ER random 2.24, threshold 1.37 —
  ER random carries more redundant flow routes.
- **Communication efficiency** (mean 1/shortest-path): causal grids 0.24, ER random 0.50, threshold 0.23 —
  dense random graphs have shorter paths and faster transport.

These metrics DISTINGUISH the classes but favour ER random (the opposite of stability, QG109).

---

## 3. Causal depth + memory capacity + stable computation (ATQG1101)

- **Causal depth** (graph diameter): causal grids 15.8, ER random 4.6, threshold 18.0 — causal grids are
  causally deep.
- **Memory capacity** (effective active modes e^H): causal grids 152.3, ER random 107.8, threshold 124.6 —
  causal grids host more distinct modes (hierarchical spectrum).
- **Stable computation** (family survival under 10% removal): causal grids 100% (exactly preserved), ER random
  107% (fluctuating), threshold 106%.

The causal class is the information-rich, deep, exactly-stable class — these metrics PREFER the causal grid, in
the opposite direction to communication efficiency. This is the information trade-off.

---

## 4. Information selection + classification (ATQG1102)

Composite capacity functional (depth × memory × stable): causal family (grid + threshold + perturbed) scores
~2.5× ER random (e.g. grid 451 vs ER 179; perturbed 509). The information-capacity functional STRONGLY prefers
the causal family, but the causal class contains 8+ distinct grids (plus threshold and perturbed variants) — no
unique network is singled out, and flow/efficiency trade off against depth/memory/stability.

**PARTIAL SELECTION.**

- NOT NO EFFECT: information metrics distinguish and narrow — causal depth, memory capacity, and stable
  computation prefer the causal grid via the native capacity functional.
- NOT PHYSICAL SELECTION: the causal class contains many distinct members, and the metrics trade off
  (communication efficiency prefers dense random) — no unique network is selected.
- PARTIAL SELECTION: information capacity contributes to selection but does not uniquely determine the physical
  network — consistent with QG109 (stability) and QG102 (non-unique solution space).

---

## 5. Conclusion

Information-processing capacity narrows the selection toward the causal class even more strongly than stability
alone (QG109), but it does not select a UNIQUE network: the causal family contains many distinct members, and the
information metrics trade off against each other (flow/efficiency vs depth/memory/stability). The physical
network is PARTIALLY selected by information capacity — a genuine contribution, not a full determination.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1100 `ATQG1100_InformationFlowAndEfficiency` | PASS (flow/efficiency distinguish classes, prefer ER) |
| ATQG1101 `ATQG1101_CausalDepthMemoryStable` | PASS (depth/memory/stability prefer causal grids) |
| ATQG1102 `ATQG1102_InformationSelectionAndClassification` | PASS (PARTIAL SELECTION) |

Code: `AT.Core/ResearchXH/NetworkInformationSelection.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase110_NetworkInformationSelectionTests.cs`.
