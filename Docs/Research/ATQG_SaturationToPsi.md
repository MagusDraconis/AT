# AT-QG Phase 37 — Can Saturation Generate ψ?

**Program:** AT-QG (Unification)
**Phase:** 37 — does nonlinear saturation generate an effective tensor sector?
**Status:** COMPLETED — 3/3 xUnit tests pass (114/114 AT-QG)
**Constraint:** no new primitives (audit of the already-identified ψ extension)

---

## 1. Goal

QG23 ruled out ψ from the local scalar ρ; QG36 derived the regular-core profile from finite-density saturation.
Here we test whether **nonlinear saturation** of the Q-event network can generate an effective anisotropic/tensor
sector. Classify: NEW PRIMITIVE / EMERGENT / PARTIAL MATCH.

---

## 2. Spin census (ATQG370)

| mechanism | spin |
|---|---|
| nonlinear scalar function ρ→f(ρ) | 0 |
| saturation gradient ∇f(ρ) | 1 |
| anisotropic saturation front (a direction) | 1 |
| tensor (ψ/Weyl) sector | **2** |

No scalar saturation mechanism reaches spin 2: a nonlinear function of a scalar is still a scalar, and its
gradient / any anisotropic front reaches at most spin 1 (a single direction).

---

## 3. No independent degree of freedom (ATQG371)

Saturation is a scalar reparameterization ρ → f(ρ): it renormalizes the conformal factor and produces the regular
core (QG36), but **f(ρ) is determined by ρ** — it adds **no independent field**. A tensor (2 d.o.f.) cannot be
manufactured from a function of one scalar.

---

## 4. Classification (ATQG372)

**NEW PRIMITIVE.**

- NOT EMERGENT: no scalar nonlinearity reaches spin 2, so the tensor sector does not emerge from saturation.
- The PARTIAL content: saturation DOES generate the scalar regular-core profile (QG36) — a scalar
  renormalization, not a tensor; that is a partial contribution to the ψ-extension's **scalar** side only.
- ψ (the tensor sector) remains a **NEW PRIMITIVE**: the graviton still needs a genuinely independent rank-2 field
  (QG23/QG24/QG28/QG34).

---

## 5. Conclusion

Saturation closes the scalar side of the ψ-extension (it supplies the regular-core mass profile, QG36), but it
**cannot generate the tensor sector**. The two-layer structure is now fully resolved:

- **scalar layer** (regular core, non-conformal scalar profile) — DERIVED from finite-density saturation (QG36);
- **tensor layer** (the graviton, 2 helicities) — a **NEW PRIMITIVE**, unreachable by any scalar nonlinearity.

This is the definitive separation: saturation is scalar physics; ψ (spin-2) is irreducibly new.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG370 `ATQG370_SpinCensus` | PASS (scalar/gradient/front ≤ spin 1) |
| ATQG371 `ATQG371_NoIndependentDof` | PASS (no independent d.o.f.) |
| ATQG372 `ATQG372_Classification` | PASS (NEW PRIMITIVE) |

Code: `AT.Core/ResearchXH/SaturationToPsi.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase37_SaturationToPsiTests.cs`.
