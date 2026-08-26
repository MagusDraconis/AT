# G4-G Phase 4 — Search for an Independent Native Stress-Energy

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-G)
**Phase:** 4 — can T_μν emerge independently from actualization-density dynamics (without T = G/κ)?
**Status:** COMPLETED — 3/3 xUnit tests pass (15/15 G4-G)
**Constraint:** no imported Einstein equations; native geometry program only

---

## 1. Goal

Test whether an **independent** stress-energy T_μν emerges from ρ, ∂ρ, ∂²ρ, ρ̇ — i.e. whether there is
a "matter" sector distinct from the geometry, or whether any conserved stress-energy is forced to be
G/κ.

---

## 2. Findings

### (a) The kinetic stress-energy is NOT conserved (G4-G40)

T^kin_μν = ∂_μσ∂_νσ − (1/2)g_μν(∂σ)² (built from ∇ρ only) is **symmetric but not divergence-free**:

∇^μ T^kin_μ1 = ρ^(−2/d)·σ′[σ″ + (d−1)(σ′)²] ≠ 0

for the curved profile. It is not a valid independent stress-energy.

### (b) The conserved tensor is unique (= G) — Lovelock uniqueness (G4-G41)

The general symmetric 2nd-order tensor T_11 = A(σ′)² + Bσ″, T_ii = C(σ′)² + Dσ″ is divergence-free
only if the three coefficient equations

```
B = 0,   (d−3)A − (d−1)C = 0,   2A − (d−1)D = 0
```

hold. These fix B, C, D in terms of A — a **1-dimensional** solution space. The unique solution
(parameter A = (d−1)(d−2)/2) is exactly **G_μν**. So any symmetric, conserved stress-energy built from
ρ is **forced to be G/κ**.

### (c) The density flux is curvature-sourced (G4-G42)

The actualization flux J = ∇ρ has divergence Δρ = ρ″ = 2a ≠ 0 — the density is **curvature-sourced**
(Δρ ∝ −R), not independently conserved.

---

## 3. Classification

| candidate | symmetric | conserved | valid stress-energy? |
|---|---|---|---|
| kinetic T^kin (∇ρ) | ✅ | ❌ | ❌ (not conserved) |
| **T = G/κ** | ✅ | ✅ | ✅ (unique) |
| independent scalar-field T | ❌ (or = G) | — | ❌ none exists |

---

## 4. Conclusion

**No independent matter sector exists.** Two independent facts force T = G/κ:

1. **The kinetic (∇ρ) stress-energy is not conserved**, so the density's gradient energy alone cannot
   be a stress-energy tensor.
2. **Lovelock uniqueness**: the divergence-free condition on the general symmetric 2nd-order tensor
   built from σ leaves a single overall scale, and that unique tensor is the Einstein tensor G_μν.

Combined with the curvature-sourced density flux (Δρ ∝ −R), the actualization density is **both** the
geometric source (conformal factor) **and** the matter source (T = G/κ) — there is no independent
"matter" that is not already the geometry. The native Einstein relation G = κT is therefore not an
imported field equation but an **unavoidable identity**: the only conserved stress-energy the
actualization density can carry is its own Einstein tensor.

---

## Test program

| Test | Verdict |
|---|---|
| G4-G40 `G4_G40_KineticStressEnergyIsNotConserved` | PASS (∇^μ T^kin ≠ 0) |
| G4-G41 `G4_G41_UniquenessOfConservedTensor` | PASS (divergence-free ⟹ T = G, up to scale) |
| G4-G42 `G4_G42_DensityFluxIsCurvatureSourced` | PASS (Δρ = 2a ≠ 0, ∝ −R) |

Code: `AT.Core/ResearchXH/HigherDimEinstein.cs` (added `KineticDivergence`);
tests `AT.Tests/ResearchXH/G4G_Phase4_IndependentMatterSectorTests.cs`.
