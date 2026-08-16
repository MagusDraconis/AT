# TQM-F Phase 1 — The Indifference Principle

**Program:** TQM-F (Foundation)
**Phase:** 1 — why is actualization unbiased across scales?
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no new primitives

---

## 1. Goal

G4-RHO3 traced indifference → maximum likelihood → entropy → α=0. Here we test *why* actualization is
unbiased across scales, via abundance-law statistics, scale transformations, self-similar temporal fields,
renormalization invariance, and counting-measure consistency. Classify: DERIVED / PREFERRED / POSTULATED.

---

## 2. Results

### (a) Primitives are scale-covariant; the power law is the unique scale-covariant form (TQMF10)

The counting measure is a density of weight d (N = ∫ρ dV is invariant under x → λx with ρ → λ⁻ᵈρ), and the
causal order is a scale-invariant partial order. A scale-covariant abundance must satisfy n(λR)/n(R) = f(λ)
independent of R — the **power law** n ∝ R⁻ᵖ:

| R | n∝R⁻¹ | n(2R)/n(R) | Gaussian n(2R)/n(R) |
|---|---|---|---|
| 1.0 | 1.000 | 0.500 | 0.472 |
| 2.0 | 0.500 | 0.500 | 0.018 |

The power-law ratio is constant (scale-covariant); the Gaussian ratio depends on R (scale-setting, breaks
covariance). The primitives carry **no intrinsic scale**.

### (b) Renormalization invariance: power laws are the fixed points (TQMF11)

Coarse-graining a power law preserves its form (successive ratios stay constant), so power laws are the
renormalization (RG) fixed points. A Gaussian-bump (scale-setting) profile is NOT self-similar — its
successive ratios vary, and coarse-graining washes out its characteristic scale.

### (c) Classification (TQMF12)

Scale-freeness is the **unique renormalization-invariant** abundance form.

---

## 3. Classification: PREFERRED (unique renormalization-invariant), DERIVED-conditional

- **Scale-freeness is the unique renormalization-invariant (self-similar) abundance** — power laws are the RG
  fixed points; scale-setting profiles flow away under coarse-graining.
- **The primitives carry no intrinsic scale** — the causal order is a scale-invariant partial order, the
  counting measure a scale-covariant density.
- **Therefore indifference is DERIVED conditional on renormalization invariance**, and renormalization
  invariance is the natural requirement for a theory with no external scale.

Indifference is **PREFERRED** — not a bare postulate (it is uniquely selected by renormalization invariance)
and not a theorem from the raw primitives alone (a scale-setting mechanism is not *excluded*, just not
*invariant*). This downgrades the indifference postulate (G4-RHO3) to a renormalization-invariance
requirement, exactly parallel to conformal flatness being a minimum-information requirement (G4-A1).

---

## 4. Conclusion

The indifference principle is justified: actualization is unbiased across scales because **scale-freeness is
the unique renormalization-invariant abundance law**, and the TQM primitives (partial order + counting
measure) have no intrinsic scale to prefer one scale over another. The two foundation postulates are now both
reduced — indifference = renormalization invariance (here), conformal flatness = minimum information (G4-A1)
— leaving the *primitives themselves* (causal order, counting measure) and the *free parameters* (d, G) as
the true irreducible base.

---

## Test program

| Test | Verdict |
|---|---|
| TQMF10 `TQMF10_ScaleCovariance` | PASS (power law = unique scale-covariant form) |
| TQMF11 `TQMF11_RenormalizationInvariance` | PASS (power law = RG fixed point) |
| TQMF12 `TQMF12_Classification` | PASS (PREFERRED, DERIVED-conditional) |

Code: `TQM.Core/ResearchXH/RhoDynamics.cs` (added `CoarseGrain`, `GaussianAbundance`, `SuccessiveRatios`);
tests `TQM.Tests/ResearchXH/TQMF_Phase1_IndifferencePrincipleTests.cs`.
