# TQM-QG Phase 109 — Selection of the Physical Network

**Program:** TQM-QG (Unification)
**Phase:** 109 — why does nature realize one specific network class?
**Status:** COMPLETED — 3/3 xUnit tests pass (330/330 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational audit of the native operator spectrum)

---

## 1. Goal

QG102 found many globally-consistent network classes (non-unique solution space); QG108 found a broad
family-count distribution. This phase asks: why does nature realize ONE specific network class? Five
mechanisms are measured — stability selection, attractor basins, actualization statistics, network growth
history, and anthropic-free selection. Classify: NO SELECTION / PARTIAL SELECTION / PHYSICAL SELECTION.

---

## 2. Stability selection + attractor basins (TQMQG1090)

- **Spectral gap** (mean λ_2): causal grids 0.103, ER random 9.025, threshold 0.173 — the spectral gap FAVOURS
  ER random.
- **Family-structure persistence** (fraction keeping all families under 10% link removal): causal grids 100%,
  threshold 100%, ER random 98.3% — family persistence FAVOURS the causal grid.
- **Attractor basins** (KS single-linkage): **17 basins**, largest 28.6%, no basin dominates (> 80%: false).

The stability criteria CONFLICT: the spectral gap prefers ER random, family-structure persistence prefers the
causal grid. No single stability criterion selects a unique class — the signature of PARTIAL (conflicted)
selection.

---

## 3. Actualization statistics + growth history (TQMQG1091)

- **Counting-measure variance** (QG89 actualization-rate observable): causal grids 1.73, threshold 2.03, ER
  random 13.27 — the counting measure statistically PREFERS the causal grid (more concentrated density).
- **Growth history** (octave-family count vs size): 3 → 4 → 4 → 5 → 4 → 5 → 5 — the family count drifts with
  size; it does NOT converge to a unique class.

---

## 4. Anthropic-free selection + classification (TQMQG1092)

- Anthropic-free stability functional: best class ER (60 members), **no unique network selected**.
- **Criterion conflict**:
  - counting-measure variance: grid 1.73 < ER 13.27 → statistics prefer grid;
  - family persistence: grid 100% > ER 98.3% → stability prefers grid;
  - spectral gap: grid 0.103 < ER 9.025 → gap prefers ER.

**PARTIAL SELECTION.**

- NOT NO SELECTION: the counting measure (QG89) and family-structure persistence BOTH narrow toward the causal
  grid without any observer input.
- NOT PHYSICAL SELECTION: no native functional selects a UNIQUE network — the preferred class contains many
  members, the spectral-gap criterion conflicts (prefers ER random), and growth history drifts the family count.
- PARTIAL SELECTION: a native, anthropic-free mechanism narrows the region, but conflicting criteria prevent a
  unique choice — consistent with QG96 (partial) and QG102 (non-unique).

---

## 5. Conclusion

Nature's preference for one network class is **PARTIALLY** selected: native, anthropic-free mechanisms (the
counting measure, family-structure persistence) narrow the region toward the causal grid, but the criteria
conflict (spectral gap prefers random) and no unique network is singled out. The selection is genuine but
partial — consistent with the established QG96/QG102 picture.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1090 `TQMQG1090_StabilityAndAttractorBasins` | PASS (criteria conflict; 17 basins, none dominant) |
| TQMQG1091 `TQMQG1091_ActualizationStatisticsAndGrowth` | PASS (counting measure prefers grid; growth drifts) |
| TQMQG1092 `TQMQG1092_AnthropicFreeSelectionAndClassification` | PASS (PARTIAL SELECTION) |

Code: `TQM.Core/ResearchXH/PhysicalNetworkSelection.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase109_PhysicalNetworkSelectionTests.cs`.
