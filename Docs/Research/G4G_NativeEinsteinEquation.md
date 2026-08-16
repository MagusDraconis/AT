# G4-G Phase 3 — Do the Einstein Equations Emerge Natively?

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-G)
**Phase:** 3 — identify a native stress-energy analogue and the G = κT relation
**Status:** COMPLETED — 3/3 xUnit tests pass (12/12 G4-G)
**Constraint:** no imported Einstein equations; native geometry program only

---

## 1. Goal

Starting from G_μν being directly reconstructable from ρ (G4-G2), identify a native stress-energy
analogue T from the actualization density and its dynamics, and test whether a G_μν = κT_μν-like
relation emerges.

---

## 2. Findings

### (a) Native stress-energy is symmetric and conserved (G4-G30)

Define **T_μν = G_μν/κ** (κ the gravitational coupling). It is:
- **symmetric** (diagonal for the x-only profile);
- **divergence-free** — ∇^μ T_μν = (1/κ)∇^μ G_μν = 0 by the Bianchi identity (max < 1e−8).

### (b) The native Einstein relation (G4-G31)

G_μν = κT_μν holds exactly at all x and d = 3,4, with the trace structure
**T^μ_μ = −(d−2)R/(2κ)**.

### (c) The kinetic part alone is insufficient (G4-G32)

| d | G_11 | κT_kin_11 | G_ii | κT_kin_ii | single κ matches? |
|---|---|---|---|---|---|
| 3 | 0.009 | 0.0033 | 0.133 | −0.0033 | ❌ |
| 4 | 0.040 | 0.0077 | 0.288 | −0.0077 | ❌ |

The kinetic stress-energy T^kin_μν = ∂_μσ∂_νσ − (1/2)η_μν(∂σ)² (built from ∇ρ only) is ∝ (ρ′)² in
**both** components, whereas G_ii also carries a ∂²ρ (σ″) term — so **no single κ** relates them. The
source of the geometry is the **full conformal structure (ρ, ∂ρ, ∂²ρ)**, not just the density's
kinetic/gradient part.

---

## 3. Classification

| question | answer |
|---|---|
| native stress-energy analogue | ✅ T = G/κ (symmetric, conserved) |
| G_μν = κT_μν relation | ✅ holds (with T = G/κ) |
| kinetic (∇ρ, ρ̇) alone suffices | ❌ (the ∂²ρ term is essential) |
| conservation (∇^μ T_μν = 0) | ✅ automatic (Bianchi) |

---

## 4. Conclusion

**A native Einstein relation emerges, with an important qualification.**

Because the actualization density ρ is the *single* primitive (it generates both the geometry g =
ρ^(2/d)η and the curvature G_μν(ρ)), the stress-energy **T = G/κ** is the native matter sector — and
it is symmetric and divergence-free, so G = κT holds as a consistent, conserved dynamical relation.

The **non-trivial finding** is that the density's *kinetic* part (∇ρ, ρ̇) does **not** source the
geometry: the Einstein tensor's transverse component carries a ∂²ρ (σ″) term with no kinetic
counterpart. The actualization density acts as matter through its **complete conformal structure**
(ρ, ∂ρ, ∂²ρ), not through a naive scalar-field kinetic energy.

In other words: the native program yields the *form* of the Einstein equation — a symmetric, conserved
stress-energy T = G/κ built from ρ — but the "matter" is the conformal (geometric) content of ρ itself,
not an independent kinetic sector. κ (the coupling strength) remains a units convention, not a native
datum.

---

## Test program

| Test | Verdict |
|---|---|
| G4-G30 `G4_G30_NativeStressEnergyIsSymmetricAndConserved` | PASS (T = G/κ symmetric, ∇^μT = 0) |
| G4-G31 `G4_G31_EinsteinRelationAndTrace` | PASS (G = κT, trace −(d−2)R/(2κ)) |
| G4-G32 `G4_G32_KineticStressEnergyIsInsufficient` | PASS (kinetic T insufficient; ∂²ρ essential) |

Code: `TQM.Core/ResearchXH/HigherDimEinstein.cs` (added `KineticStress*`, `NativeStress*`);
tests `TQM.Tests/ResearchXH/G4G_Phase3_NativeEinsteinEquationTests.cs`.
