# G4-L Phase 0 — Native Lorentzian Operators

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 0 — investigate native Lorentzian operators
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can causal order alone produce a Lorentzian operator analogous to Lc's role in the Riemannian sector?
**Allowed:** causal order, interval structure, layers, counting measure, links.
**Forbidden:** BDG weights, metric tensor, Laplace–Beltrami, d'Alembertian formulas.

---

## 1. Goal

The Riemannian sector has Lc = ρ⁻¹ L ρ⁻¹ (elliptic, PSD). Here we seek a native operator whose
spectrum carries the **Lorentzian signature** — indefiniteness (both + and − eigenvalues) — built
only from the causal structure.

Deterministic setting: a 1+1D Minkowski grid causal set (t ∈ [0..7], x ∈ [−4..4], N = 72),
with order i ≺ j ⟺ t_j − t_i > |x_j − x_i|, 175 Hasse links.

---

## 2. Candidate operators

| ID | Operator | Construction |
|---|---|---|
| L1 | causal-link | symmetrized Hasse-link adjacency A + Aᵀ |
| L2 | interval | raw symmetric interval matrix (|[i,j]|) |
| L3 | layer | alternating-sign layer adjacency (−1)^(k+1) over layer k = |[i,j]| |
| L4 | density-weighted | ρ⁻¹ (A + Aᵀ) ρ⁻¹, ρ = past + future degree |

All four are symmetric (real spectrum) and built from the directed causal order only.

---

## 3. Results

### 3.1 G4-L00 — construction + directionality

- DAG (strict partial order, time a topological order): ✅
- Directed link relation (A ≠ Aᵀ, past/future asymmetry): ✅
- All four operators symmetric: ✅

### 3.2 G4-L01 — indefinite spectrum

| operator | n+ | n− | n0 | indefinite |
|---|---|---|---|---|
| L1 causal-link | 36 | 36 | 0 | ✅ |
| L2 interval | 45 | 27 | 0 | ✅ |
| L3 layer | 31 | 41 | 0 | ✅ |
| L4 density-weighted | 36 | 36 | 0 | ✅ |

**4/4 indefinite, 4/4 non-elliptic.** L1 (and L4) have a perfectly balanced spectrum
(min λ = −5.454, max λ = +5.454) — the link graph is bipartite; L4 preserves L1's inertia
(Sylvester's law under the ρ-congruence).

### 3.3 G4-L02 — distinguishability from Lc

- Riemannian Lc = ρ⁻¹Lρ⁻¹: signature (255+, 0−, 1 zero mode) → **PSD (elliptic)**.
- All causal operators: mixed-sign → **indefinite (Lorentzian)**.

The sign of the spectrum cleanly separates elliptic from Lorentzian — no metric or
d'Alembertian formula is needed.

---

## 4. Conclusion

**YES.** Causal order alone produces native operators with the **Lorentzian signature**
(indefinite, non-elliptic, causal-directional) — the analogue of Lc's role in the Riemannian
sector. The alternating layer operator L3 is the closest native analogue of the BDG
d'Alembertian (alternating signs over layers, uniform weights).

**Caveat (honest scope).** Indefiniteness is the *signature*, not yet the *wave operator*: these
operators are not yet shown to converge to □ (the continuum d'Alembertian) — that correspondence
(the Lorentzian analogue of G4-C Phase 1) is the next phase. The BDG binomial weights remain the
unresolved item; L3 shows the alternating-sign structure is native, but the *specific* weights
(and their continuum limit) are still open.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L00 `G4_L00_CausalOperatorsAreConstructibleAndDirectional` | PASS (DAG, directed, symmetric) |
| G4-L01 `G4_L01_OperatorsExhibitIndefiniteSpectrum` | PASS (4/4 indefinite) |
| G4-L02 `G4_L02_CausalOperatorsAreDistinguishableFromLc` | PASS (Lc PSD vs indefinite) |

Code: `TQM.Core/ResearchXH/CausalSet.cs` + `LorentzianOperator.cs`; tests
`TQM.Tests/ResearchXH/G4L_Phase0_NativeLorentzianOperatorsTests.cs`.
