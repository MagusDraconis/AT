# AT-QG Phase 26 — Non-Tensor Explanation of Lensing

**Program:** AT-QG (Unification)
**Phase:** 26 — can apparent lensing emerge from the scalar machinery?
**Status:** COMPLETED — 3/3 xUnit tests pass (81/81 AT-QG)
**Constraint:** no new primitives; observable quantities only (image shift, magnification, time delay)

---

## 1. Goal

QG25 classified lensing as OBSERVABLE AMBIGUITY. Here we test directly whether apparent lensing can emerge from
the five scalar mechanisms — actualization-density gradients, time-delay statistics, path-selection effects,
conformal optical depth, horizon-counting geometry — using only observable quantities and ignoring GR's curvature
interpretation. Classify: MATCH / PARTIAL MATCH / NO MATCH.

---

## 2. Core fact: PPN γ = −1

The conformally-flat metric g = ρ^(2/d)η has PPN parameter **γ = −1**. Every lensing observable is proportional to
(1+γ)/2: deflection δ = (1+γ)/2 · 4GM/(bc²), convergence κ ∝ (1+γ)/2, shear ∝ (1+γ)/2, Shapiro delay
Δt = (1+γ)/2 · 2GM/c³·ln(...). With γ = −1 every one of these factors is **zero**.

| Observable | conformal (γ=−1) | GR (γ=+1) |
|---|---|---|
| deflection | 0 | 4GM/bc² |
| convergence κ | 0 | (1+γ)/2 Σ |
| magnification μ | 1 | >1 |
| Shapiro delay | 0 | 2GM/c³·ln |

Only the gravitational redshift z = (ρ2/ρ1)^(1/d) − 1 (governed by g_00 alone) survives.

---

## 3. Results

### (a) Image shift & magnification (ATQG260) — NO MATCH
The conformal factor multiplies g_00 and g_ii equally, so null geodesics are unchanged: δ = 0 and μ = 1. Density
gradients and path-selection cannot deflect or focus light.

### (b) Time delay (ATQG261) — NO MATCH
The conformal factor cancels in the null condition (dt = dx), so light arrives with no extra coordinate delay.
Time-delay statistics produce no Shapiro delay. (The redshift — a frequency shift, not an arrival-time shift — is a
separate, genuine MATCH.)

### (c) Mechanism census (ATQG262) — all NO MATCH
All five mechanisms operate through the same conformal geometry (γ = −1) and hence cancel deflection, focusing, and
delay identically.

---

## 4. Classification

| Observable | Classification |
|---|---|
| image shift | NO MATCH |
| magnification | NO MATCH |
| time delay | NO MATCH |
| (redshift — not in the lensing trio) | MATCH (survives) |

**OVERALL: NO MATCH.** No non-tensor mechanism reproduces apparent lensing. The conformal geometry (γ = −1) cancels
deflection, focusing, and Shapiro delay exactly; only the gravitational redshift survives.

---

## 5. Conclusion

This closes the loop with QG25: lensing is an *observable ambiguity* in the sense that it is not *logically* forced
to be tensor — but in AT's actual conformal geometry, **no scalar mechanism produces it at all** (γ = −1). The
"ambiguity" is resolved in the negative: apparent lensing does **not** emerge from the scalar sector; a non-conformal
extension (scalar ψ, 1 d.o.f., or spin-2, 2 d.o.f.) is required to move γ away from −1 and turn on deflection.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG260 `ATQG260_ImageShiftAndMagnification` | PASS (δ=0, μ=1) |
| ATQG261 `ATQG261_TimeDelayAndRedshiftSurvivor` | PASS (Δt=0, z>0) |
| ATQG262 `ATQG262_MechanismCensus` | PASS (5/5 NO MATCH) |

Code: `AT.Core/ResearchXH/NonTensorLensing.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase26_NonTensorLensingTests.cs`.
