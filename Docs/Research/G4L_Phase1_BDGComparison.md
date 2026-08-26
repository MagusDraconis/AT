# G4-L Phase 1 — BDG Comparison

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 1 — rank native Lorentzian operators against the BDG d'Alembertian
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Which native Lorentzian operator is closest to BDG behavior?
**Reference:** symmetric BDG d=2 (B = −2·I + 4·link − 2·next-layer) + retarded (past-only) BDG.

---

## 1. Results

### 1.1 G4-L10 — spectrum + eigenmodes (KS distance to symmetric BDG)

| operator | signature (n+, n−, n0) | KS to BDG |
|---|---|---|
| L1 causal-link | (36, 36, 0) | 0.3750 |
| L2 interval | (45, 27, 0) | 0.3194 |
| **L3 layer** | (31, 41, 0) | **0.2222** |
| L4 density-weighted | (36, 36, 0) | 0.5972 |

Symmetric BDG reference: (29+, 43−, 0), min λ = −29.1, max λ = +15.8. **L3 has the smallest KS
distance** — spectrally closest to BDG.

### 1.2 G4-L11 — layer/interval response + alternation

BDG layer profile: (k=0) +4, (k=1) −2, (k=2) 0, (k=3) 0 — **alternates** between links and the
next layer.

| operator | k=0 | k=1 | k=2 | k=3 | alternates |
|---|---|---|---|---|---|
| L1 causal-link | 1.00 | 0.00 | 0.00 | 0.00 | no |
| L2 interval | 0.00 | 1.00 | 2.00 | 3.00 | no |
| **L3 layer** | −1.00 | 1.00 | −1.00 | 1.00 | **yes** |
| L4 density-weighted | 0.04 | 0.00 | 0.00 | 0.00 | no |

Only L3 shares BDG's defining **alternating layer sign**.

### 1.3 G4-L12 — propagation + ranking

δ-source at (t=3, x=0):

| operator | past | future | direction |
|---|---|---|---|
| BDG (retarded) | 0.00 | 16.00 | forward-only |
| L1 | 3.00 | 3.00 | symmetric |
| L2 | 39.00 | 106.00 | symmetric |
| L3 | 15.00 | 24.00 | symmetric |
| L4 | 0.08 | 0.08 | symmetric |

All candidates are time-symmetric (Feynman-like); BDG is retarded.

---

## 2. Ranking and classification

| Rank | Operator | Classification | Basis |
|---|---|---|---|
| 1 | **L3 layer** | **BEST MATCH** | only alternating-layer operator + smallest KS distance (0.2222) |
| 2 | L1 causal-link | PROMISING | clean link operator (first layer only, no alternation) |
| 3 | L4 density-weighted | WEAK | links distorted by density (worst KS 0.5972) |
| 4 | L2 interval | REJECT | monotonic interval weighting, no link/alternation |

---

## 3. Conclusion

The **layer operator L3** (alternating sign over layers, uniform weights) is the closest native
operator to BDG — confirmed by both the spectral KS distance and the structural layer-alternation
test. It is BDG's structure with **uniform** weights.

**Remaining gaps (honest):** (i) L3 uses uniform alternating weights, not BDG's binomial
coefficients; (ii) L3 has no diagonal (self-term); (iii) all candidates are time-symmetric,
whereas BDG is retarded. These are the targets of the next phase (coefficient derivation / a
retarded symmetrization).

---

## Test program

| Test | Verdict |
|---|---|
| G4-L10 `G4_L10_SpectrumAndEigenmodesComparison` | PASS (L3 smallest KS) |
| G4-L11 `G4_L11_LayerResponseAndAlternation` | PASS (only L3 alternates) |
| G4-L12 `G4_L12_PropagationAndRanking` | PASS (L3 BEST MATCH, L2 REJECT) |

Code: `AT.Core/ResearchXH/LorentzianOperator.cs` (added `BdgReference`, `RetardedBdg`,
`LayerProfile`, `Alternates`, `BdgCoefficient`); tests
`AT.Tests/ResearchXH/G4L_Phase1_BDGComparisonTests.cs`.
