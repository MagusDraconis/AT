# TQM-QG Phase 6 — Origin of G

**Program:** TQM-QG (Unification)
**Phase:** 6 — can G emerge from counting statistics or actualization dynamics?
**Status:** COMPLETED — 3/3 xUnit tests pass (21/21 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

The gravity structure is derived; only the overall scale (Newton's G / BDG −2) remains imported. Here we test
whether G can emerge from counting statistics or actualization dynamics. Classify: DERIVED / PREFERRED /
IMPORTED.

---

## 2. Results

### (a) The conformal gravity has no free coupling (TQMQG60)

The geodesic acceleration a = −(1/d)∇lnρ contains **no coupling constant** — the 1/d is fixed and the profile
is all that matters. The power-law deficit's asymptotic effective mass equals the native deficit mass

  GM_eff = m₀·r₀/(d·ρ̄) = 0.5·0.5/(3·1) = 0.0833

(M_eff(−a·r²) at r=12 = 0.0784, within 6% of the asymptote). The gravitational scale is fully determined by
the deficit abundance (m₀, r₀, ρ̄).

### (b) G and M are not separable (TQMQG61)

GM_eff is invariant under m₀ → c·m₀, r₀ → r₀/c — a large deficit over a small scale is indistinguishable from
a small deficit over a large scale. G and M are NOT separately determined; only the product is physical.

### (c) Classification (TQMQG62)

**IMPORTED as a discrete normalization (BDG −2); DERIVED as the physical scale.**

---

## 3. Classification: IMPORTED (discrete normalization), DERIVED (physical scale)

- **The physical gravitational scale is DERIVED**: GM_eff = m₀·r₀/(d·ρ̄) is the deficit abundance of the
  actualization, with no independent coupling G. The conformal gravity has no free coupling constant.
- **G and M are not separable**: only GM_eff is physical, so "what is G" is really "what is the deficit
  abundance" — which the actualization dynamics supplies (m₀, r₀, ρ̄).
- **The BDG scale −2 (G4-L12) is a discretization normalization**: it fixes the discrete operator's second
  moment to match the continuum Laplacian — IMPORTED, and distinct from the physical G.

---

## 4. Conclusion

The gravitational coupling is **native**: the conformal gravity a = −(1/d)∇lnρ has no free coupling constant,
and the physical scale GM_eff = m₀·r₀/(d·ρ̄) is the deficit abundance, derived from the actualization. G and M
are not individually separable — only their product is physical. The one "imported scale" that remains is the
**BDG −2**, which is a *bookkeeping discretization normalization* (continuum second-moment matching, G4-L12),
not a physical coupling. This resolves the foundation audit's "G imported" item: the physical gravitational
strength is native; only the discrete operator normalization is imported.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG60 `TQMQG60_NoFreeCoupling` | PASS (GM_eff native, no free coupling) |
| TQMQG61 `TQMQG61_GMDegeneracy` | PASS (G–M non-separable) |
| TQMQG62 `TQMQG62_Classification` | PASS (IMPORTED BDG −2; DERIVED physical scale) |

Code: `TQM.Core/ResearchXH/CouplingOrigin.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase6_OriginOfGTests.cs`.
