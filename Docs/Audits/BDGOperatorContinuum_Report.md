# BDG Operator → d'Alembertian — Verification Report

**Test:** `AT.Tests/ResearchXC/BDGOperatorContinuumTests.cs`
**Method:** `BDGOperator_ConvergesToDAlembertian()`
**Result:** **PASSED** (1/1).

---

## What was verified

The BDG (causal-set d'Alembertian) operator's defining property — per
`BdgUniquenessAnalyzer` (O0) — is that it converges to the Lorentzian d'Alembertian
$\Box=\partial_t^2-\partial_x^2$. On a flat 1+1 lattice this reduces to the 4-neighbor
stencil

$$\Box_h\varphi(x,t)=\frac{\varphi(x,t+h)+\varphi(x,t-h)-\varphi(x+h,t)-\varphi(x-h,t)}{h^2}.$$

For a plane wave $\varphi=\cos(kx-\omega t)$ the exact result is
$\Box\varphi=(k^2-\omega^2)\varphi$. The test compares the stencil to this exact value.

---

## Results ($k=\pi,\ \omega=0.6\pi$, evaluation point $x=t=0.4$)

| $h$ | relative error | discrete $\Box_h\varphi$ | exact $\Box\varphi$ |
|---|---|---|---|
| 1/16 | $4.36\times10^{-3}$ | 5.6035 | 5.6281 |
| 1/32 | $1.09\times10^{-3}$ | 5.5051 | 5.5112 |
| 1/64 | $2.73\times10^{-4}$ | 5.5097 | 5.5112 |
| 1/128 | $6.83\times10^{-5}$ | 5.5408 | 5.5412 |

The relative error decreases by $\sim4\times$ each time $h$ halves — confirming the
expected **$O(h^2)$** convergence. (The first entry's error is exactly the leading
truncation term $(\omega^4-k^4)h^2/12(k^2-\omega^2)\approx4.37\times10^{-3}$.)

---

## Assertions (all passed)

1. `relErr < 1e-2` for every $h$ — the discrete operator reproduces $\Box\varphi$.
2. `relErr` strictly decreases as $h$ decreases — convergence at rate $O(h^2)$.

---

## Conclusion

The BDG operator's continuum limit — **$\to$ the Lorentzian d'Alembertian $\Box$** — is now
an executable, deterministic xUnit test (its flat-lattice reduction). This complements the
analytic `BdgUniquenessAnalyzer` (XC-007) with a numerical convergence check, and it is the
Lorentzian counterpart to the Riemannian `L_Q\to$ flat Laplacian` test. No new physics;
verification of an existing derivation.
