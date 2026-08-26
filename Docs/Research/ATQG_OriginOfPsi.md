# AT-QG Phase 23 — Origin of the ψ-Field

**Program:** AT-QG (Unification)
**Phase:** 23 — can ψ emerge from actualization, or is it a new primitive?
**Status:** COMPLETED — 3/3 xUnit tests pass (72/72 AT-QG)
**Constraint:** no new primitives (the *conclusion* is that ψ requires a new primitive)

---

## 1. Goal

QG22 traced all remaining observable gaps to ψ=0. Here we test whether ψ can emerge from the actualization
(anisotropic branching, directional actualization, higher-order counting statistics, multi-field actualization,
support-rank fluctuations) rather than being a new primitive. Classify: DERIVED / EMERGENT / NEW PRIMITIVE.

---

## 2. Results

### (a) Anisotropic branching → still scalar, still conformally flat (ATQG230)

The scalar ρ is spin-0 (1 d.o.f.); the tensor ψ is spin-2 (2 d.o.f.). The Weyl tensor is conformally invariant:
Weyl(g = ρ^(2/d)η) = 0 for ANY scalar ρ, even anisotropic. So anisotropic/directional actualization still yields a
conformally-flat metric.

### (b) Multi-field actualization → a tensor, but a new primitive (ATQG231)

A rank-2 tensor (∂ᵢρ₁∂ⱼρ₂) requires ≥2 scalars; AT has exactly ONE counting measure ρ. Adding a second scalar
(or a vector/tensor) is a new primitive.

### (c) Classification (ATQG232)

**NEW PRIMITIVE.**

---

## 3. Classification: NEW PRIMITIVE

- NOT DERIVED: spin-0 cannot produce spin-2; conformal invariance keeps Weyl = 0 for any scalar actualization.
- NOT EMERGENT (from existing primitives): an effective tensor requires multiple scalars or a reference metric
  h ≠ η — a new primitive.
- ψ is therefore a NEW PRIMITIVE: the ψ/Weyl field is the minimal extension that relaxes conformal flatness and
  restores lensing, tensor GWs, and (partly) horizon thermodynamics (QG22).

---

## 4. Conclusion

This is the **definitive answer to the GW arc**: AT's conformal gravity is closed under its two primitives
(causal order + counting measure), and the tensor (ψ) sector is the one degree of freedom that requires a
genuinely third primitive. There is no emergent channel from the scalar actualization — the graviton is
irreducibly new physics relative to AT's conformal core.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG230 `ATQG230_AnisotropicStillScalar` | PASS (anisotropic scalar → Weyl=0) |
| ATQG231 `ATQG231_MultiFieldNeedsNewPrimitive` | PASS (tensor needs 2 scalars) |
| ATQG232 `ATQG232_Classification` | PASS (NEW PRIMITIVE) |

Code: `AT.Core/ResearchXH/OriginOfPsi.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase23_OriginOfPsiTests.cs`.
