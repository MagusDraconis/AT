# TQM-QG Phase 36 — Derive the TRM Regular-Core Profile

**Program:** TQM-QG (Unification)
**Phase:** 36 — can M_eff(r)=M(1−e^(−r³/r_c³)) be derived from a ψ-dynamics?
**Status:** COMPLETED — 3/3 xUnit tests pass (111/111 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

QG35 showed ψ explains regularity but not the specific profile. Here we determine whether the profile
M_eff(r) = M(1 − e^(−r³/r_c³)) is DERIVED, PREFERRED, or an ANSATZ.

---

## 2. The Poisson-saturation derivation (TQMQG360)

With Q-events at critical density ρ_c, the expected count in a 3-ball of radius r is

N(r) = ρ_c · (4π/3) r³ = (r/r_c)³,   r_c³ = 3/(4πρ_c).

The saturated mass is M_eff(r) = M·(1 − e^(−N(r))) = **M(1 − e^(−r³/r_c³))** — reproduced **exactly** at every
sample point. The exponent **3 = the spatial dimension** (volume ∝ r³), not a free ansatz parameter.

---

## 3. Mechanism census (TQMQG361)

| mechanism | yields the profile? |
|---|---|
| max-entropy (scale-free, α=0) | no (no length scale) |
| scale-space diffusion (α=0 attractor) | no |
| network tick propagation (n=1) | no |
| **finite-density saturation (Poisson Q-event counting)** | **yes** |
| Q-event update rules set r_c via ρ_c | yes |

Entropy maximization and diffusion give **scale-free** profiles and cannot reproduce a regular core; only
**finite-density saturation** yields 1−e^(−r³/r_c³).

---

## 4. Classification (TQMQG362)

**DERIVED.**

- NOT an ANSATZ: the form is the Poisson saturation function; the exponent 3 is the spatial dimension.
- DERIVED from finite-density saturation: M_eff(r) = M·(fraction of Q-events activated within r) = M(1−e^(−N)).
- CAVEAT: r_c is not itself derivable — it is set by the critical density ρ_c, which is **supplied** (TQM has
  bounds but no native cutoff value, QG14). The Poisson-independence assumption is the max-entropy counting model
  (TQM-F Phase 1). So the profile is DERIVED, with r_c as the one free scale.

---

## 5. Conclusion

The TRM regular-core profile is **DERIVED** from finite-density saturation: it is the Poisson "mass-activation"
function with exponent = spatial dimension. This resolves QG35's gap — ψ supplies the physics (non-conformal
curvature) *and* the counting measure ρ supplies the profile *shape*, once finite-density saturation is admitted.
The only residual input is the critical density ρ_c (equivalently the core scale r_c), consistent with TQM's
established result that it has bounds but no native cutoff value.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG360 `TQMQG360_PoissonSaturationReproducesProfile` | PASS (exact, exponent 3) |
| TQMQG361 `TQMQG361_MechanismCensus` | PASS (only saturation works) |
| TQMQG362 `TQMQG362_Classification` | PASS (DERIVED) |

Code: `TQM.Core/ResearchXH/TRMProfileOrigin.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase36_TRMProfileOriginTests.cs`.
