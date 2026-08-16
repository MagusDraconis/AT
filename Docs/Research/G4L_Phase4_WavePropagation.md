# G4-L Phase 4 — Wave Propagation

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 4 — does H2 propagate information as a Lorentzian wave operator?
**Status:** COMPLETED — 3/3 xUnit tests pass (all 15 G4-L tests re-verified)

---

## 1. Setup

The Green response (propagator) φ = op⁻¹ · source is applied to a localized δ, a compact, and a
random source, and its support is measured on the 1+1D causal set. (This phase also corrected the
retarded/advanced indexing convention: `PastDirectedLayer`/`RetardedBdg` are now lower-triangular,
so their Green response propagates FORWARD — the prior phases were re-verified and remain valid.)

Measurements: **directionality** (future/(future+past)), **causal front velocity** (max |Δx|/Δt
within the light cone), and **spacelike leakage** (response outside the causal future).

---

## 2. Results

### 2.1 G4-L40 — δ-source

| operator | directionality | front-v | leak |
|---|---|---|---|
| BDG (retarded) | 1.000 | 0.750 | **0.021** |
| R1 | 1.000 | 0.667 | 0.569 |
| **H2** | 0.626 | 0.750 | 0.759 |
| L3 | 0.596 | 0.750 | 0.772 |

### 2.2 G4-L41 — three sources (mean)

| operator | mean leakage |
|---|---|
| BDG | **0.061** |
| **H2** | 0.725 |
| L3 | 0.755 |

### 2.3 G4-L42 — refinement (δ-source, H2)

forward-biased ✅ · front-v ≤ 1 ✅ at N = 72 and N = 110.

---

## 3. Success criteria

| Criterion | Verdict |
|---|---|
| SC1 causal | **PARTIAL** — front velocity ≤ 1 (no superluminal) ✅, but H2's propagator leaks ~73 % (Feynman tail) |
| SC2 finite-speed | ✅ front velocity 0.75 ≤ 1 |
| SC3 closer to BDG | ✅ H2 leak 0.725 < L3 leak 0.755 (H2 more causal) |
| SC4 refinement | ✅ stable at N=72, 110 |

---

## 4. Conclusion

H2 = R1 + L3 propagates as a **forward-biased, finite-speed** wave and is **more causal than L3**
(directionality 0.626 vs 0.596; leakage 0.725 vs 0.755). But it does **not** achieve full retarded
causality: its propagator carries a large **Feynman tail** (~73 % leakage) because the retarded
component R1 is **nilpotent** (no diagonal self-term) — the inverse is dominated by the symmetric
(indefinite) part L3. The retarded BDG, by contrast, is essentially causal (leak ~2–6 %).

The missing ingredient for a fully causal native operator is the **diagonal self-term** (BDG's
coefficient −2), which is the natural next step — but is explicitly a BDG coefficient, so it lies
outside this phase's constraint.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L40 `G4_L40_PropagationConeAndDirectionality` | PASS |
| G4-L41 `G4_L41_FiniteSpeedSpreadAndClosenessToBdg` | PASS (SC2, SC3) |
| G4-L42 `G4_L42_PropagationSurvivesRefinement` | PASS (SC4) |

Code: `TQM.Core/ResearchXH/LorentzianOperator.cs` (corrected `PastDirectedLayer`/`RetardedBdg`
convention; `GreenResponse` via solve-with-fallback); tests
`TQM.Tests/ResearchXH/G4L_Phase4_WavePropagationTests.cs`.
