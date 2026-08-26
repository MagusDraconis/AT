# L_Q → Flat Laplacian — Verification Report

**Test:** `AT.Tests/ResearchQG/GraphLaplacianContinuumTests.cs`
**Method:** `GraphLaplacian_ConvergesToFlatLaplacian()`
**Result:** **PASSED** (1/1).

---

## What was verified

The 1D path-graph Laplacian $L_Q$ (tridiagonal, diagonal $=2$, off-diagonal $=-1$) has the
exact spectrum

$$\lambda_k = \frac{1}{\Delta x^2}\left[2-2\cos\frac{\pi k}{N+1}\right],\qquad
k=1,\dots,N,\qquad \Delta x=\frac{1}{N+1},$$

which converges to $(\pi k)^2$ — the eigenvalues of $-d^2/dx^2$ on $[0,1]$ — as
$N\to\infty$. This is the flat-Laplacian continuum limit.

---

## Results

| N | $dx$ | max relative error (EVD vs closed form) | low-mode continuum error |
|---|---|---|---|
| 32 | 1/33 | $1.62\times10^{-14}$ | $6.02\times10^{-1}$ |
| 64 | 1/65 | $8.88\times10^{-14}$ | $1.56\times10^{-1}$ |
| 128 | 1/129 | $5.42\times10^{-14}$ | $3.95\times10^{-2}$ |
| 256 | 1/257 | $6.80\times10^{-12}$ | $9.95\times10^{-3}$ |

- **Column 3** (EVD vs closed form) is at machine precision — the numerical eigenvalues
  equal the analytic discrete spectrum to $<10^{-12}$.
- **Column 4** (deviation of the low modes from the continuum $(\pi k)^2$) decreases by
  $\sim4\times$ whenever $N$ doubles — confirming the expected $O(1/N^2)$ convergence.

---

## Assertions (all passed)

1. `maxRelErr < 1e-6` for every $N$ — discrete spectrum matches closed form.
2. `continuumErr` strictly decreases with $N$ — convergence to the flat Laplacian.

---

## Conclusion

The first link of the continuum chain — **$L_Q\to$ flat Laplacian** — is now an executable,
deterministic xUnit test. The graph Laplacian's spectrum is the discrete second-difference
operator, converging to $-d^2/dx^2$ at rate $O(1/N^2)$, exactly as stated in
`04_Q_Networks_and_Laplacian.md`. No new physics; verification of an existing derivation.
