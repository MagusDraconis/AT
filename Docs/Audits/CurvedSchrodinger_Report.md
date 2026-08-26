# Curved Schrödinger — Report

**Test file:** `AT.Tests/ResearchXC/CurvedSchrodingerTests.cs`
**Result:** **PASSED (3/3).**

---

## Results

| # | Test | Output | Verdict |
|---|---|---|---|
| 1 | `WeightedLaplacian_DefinesCurvedOperator()` | asymmetry $=0$, min eig $=0$, $\max\|\lambda_{\rm flat}-\lambda_{\rm curved}\|=3.9952$ | **curved, valid** |
| 2 | `CurvedOperator_ReducesToFlatSchrodinger()` | $\max\|L_W-L_Q\|=0$ | **reduces** |
| 3 | `CurvedSchrodinger_ConservesNorm()` | $\|\psi(t)\|^2=1.0000000000$ (err $\sim10^{-16}$) at $t=0.1$–$2.0$ | **unitary** |

---

## The construction

The curved-space Schrödinger equation is

$$i\,\frac{\partial\psi}{\partial t}=L_W\,\psi,\qquad L_W=D_K-K,$$

built from the **already-verified weighted Laplacian** $L_W$ (whose edge weights $K_{ij}$ are
the existing spatial coupling). **No new primitives, no new parameters** — the only new
object is the *interpretation*: $L_W$ as the Hamiltonian of a curved Schrödinger equation.

---

## What each test establishes

1. **$L_W$ defines a curved operator** — it is symmetric and positive semi-definite (a valid
   self-adjoint Hamiltonian), and non-uniform weights shift its spectrum (metric-dependence).
2. **Reduction to flat** — uniform weights give $L_W=L_Q$, so the curved equation
   $i\partial_t\psi=L_W\psi$ reduces to the flat $i\partial_t\psi=L_Q\psi$ (the AT Postulate-2
   Schrödinger).
3. **Norm conservation** — because $L_W$ is self-adjoint with real eigenvalues, the propagator
   $e^{-iL_W t}$ is unitary and $\|\psi(t)\|^2$ is conserved to machine precision.

---

## Conclusion

A curved-space Schrödinger equation **can** be constructed from the verified weighted
Laplacian $L_W$. The bridge is now real at the **operator level**: $L_W$ is a metric-capable,
self-adjoint Hamiltonian that reduces to the flat Laplacian and preserves the norm. This
answers the first half of the curved-space program (`CurvedSpaceProgram.md` G1–G3): the
metric-dependent operator exists, and the curved Schrödinger is well-defined.

The **remaining** gap is unchanged and is one level deeper: $L_W$ carries the metric only
*discretely* (through the edge weights $K_{ij}$); connecting it to a continuum metric field
$g_{\mu\nu}$ — and from there to the Einstein equations — is still missing (G4). No new
physics is claimed here; this is a verification that the operator side of the bridge is
now in place.
