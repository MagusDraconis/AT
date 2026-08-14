# Curved-Space Schrödinger Audit

**Goal:** determine whether $L_Q$ already implies a Laplace–Beltrami operator $\Delta_g$.
**Inputs:** `Q_ContinuumLimit.md`, `03_Q_Theory.md`, `04_Q_Networks_and_Laplacian.md`,
`BdgUniquenessAnalyzer.cs` (the operator-uniqueness audit), `GrBridgeAnalyzer.cs`.
**Discipline:** no new physics — reconstruction only.

---

## 1. All continuum-limit derivations (extracted)

| # | Derivation | Result | Signature | Source |
|---|---|---|---|---|
| 1 | 1D chain $N\to\infty,\ \Delta x\to0$ | $L_Q\to-d^2/dx^2$; exact $\lambda_k=-(1/\Delta x^2)[2-2\cos(\pi k/(N{+}1))]-\gamma$ | **Riemannian (Euclidean)** | `04_Q_Networks_and_Laplacian.md` |
| 2 | Theta operator $L=-(1/\Delta x^2)L_Q-\gamma I$ | $L\to c^2d^2/dx^2-\gamma$ | Riemannian | `04_Q_Networks_and_Laplacian.md` |
| 3 | Operator-uniqueness audit (O3) | graph Laplacian $\to\Box$ **only in Riemannian signature**; for Lorentzian causal sets the graph is **directed** → Laplacian **not symmetric** → continuum limit **is NOT** the d'Alembertian | — | `BdgUniquenessAnalyzer.cs` (O3, "REJECTED") |
| 4 | BDG layer operator (O0) | $B\varphi=\sum_{k=1}^{d+1}(-1)^{k+1}C(d{+}1,k)\sum_{y\in L_k}\varphi(y)$ → $\Box$ with binomial weights $(+1,-4,+6,-4,+1)$ | **Lorentzian** | `BdgUniquenessAnalyzer.cs` (O0, "GOLD STANDARD") |

**Key fact.** The repository already audited the exact question "does $L_Q$ converge to the
wave operator?" — and **rejected** the graph Laplacian for the Lorentzian (physical)
signature, accepting instead the **BDG layer operator**. The graph Laplacian survives only as
the *Riemannian/Euclidean* operator.

---

## 2. Comparison of the three operators

| Operator | Definition | Curved? | Signature | Status in TQM |
|---|---|---|---|---|
| Graph Laplacian $L_Q=D-A$ | combinatorial Laplacian | no (graph) | Riemannian (symmetric) | **Present** (the $Q$ operator) |
| Discrete Laplace operator | finite-difference $\nabla^2$ | no | Riemannian | **Present** ($L_Q\to-d^2/dx^2$ on a lattice) |
| Laplace–Beltrami $\Delta_g$ | $\frac{1}{\sqrt{|g|}}\partial_\mu(\sqrt{|g|}\,g^{\mu\nu}\partial_\nu)$ | **yes** (curved) | Riemannian | **Missing** (nowhere in repo) |
| d'Alembertian $\Box=\partial_t^2-\nabla^2$ | wave operator | on a background metric | Lorentzian | **Partial** (BDG $\to\Box$, *not* $L_Q\to\Box$) |

---

## 3. Does $L_Q\to\Delta_g$ exist implicitly?

**No.** Three independent reasons:

1. **The only continuum limit is the flat Laplacian.** $L_Q\to-d^2/dx^2$ (1D chain) is the
   *Euclidean, flat* limit; no curved/metric-dependent operator is derived anywhere.

2. **$L_Q\to\Delta_g$ is the wrong signature for physics.** The Laplace–Beltrami operator is
   *Riemannian*; general relativity is *Lorentzian*. `BdgUniquenessAnalyzer` O3 states this
   explicitly: the graph Laplacian "converges to $\Box$ ONLY in Riemannian (Euclidean)
   signature"; for a Lorentzian causal set the graph is directed and the Laplacian is not
   symmetric, so "the continuum limit is NOT the d'Alembertian" — it was **REJECTED**.

3. **The Lorentzian operator is BDG, not $L_Q$.** The accepted operator that converges to
   $\Box$ is the BDG layer operator (binomial weights), a *different* object from $L_Q$. So
   the curved-space (Lorentzian) continuum operator is **not** the Laplace–Beltrami operator
   and is **not** obtained from $L_Q$.

---

## 4. Classification

| Step | Status | Evidence |
|---|---|---|
| $L_Q\to$ discrete Laplace (flat) | **Present** | exact 1D limit $L_Q\to-d^2/dx^2$ |
| discrete Laplace $\to$ Laplace–Beltrami $\Delta_g$ (curved) | **Missing** | no $\Delta_g$ or metric-dependent Laplacian anywhere |
| $L_Q\to\Delta_g$ (implicit) | **Missing** | not implied; and $L_Q$ is REJECTED for the Lorentzian signature (BDG accepted instead) |

**Overall: Missing.**

---

## 5. Conclusion

$L_Q$ does **not** imply a Laplace–Beltrami operator. The graph Laplacian yields only the
*flat Euclidean* Laplacian ($-d^2/dx^2$), and the repository's own operator-uniqueness audit
(`BdgUniquenessAnalyzer` O3) **rejected** the graph Laplacian for the Lorentzian (physical)
signature, accepting the BDG layer operator as the object that converges to the d'Alembertian
$\Box$. Consequently:

- A **curved-space Schrödinger equation** (coupling $L_Q$ to a metric to obtain $\Delta_g$)
  is **not** present, not implied, and would require the *Riemannian* Laplace–Beltrami —
  which is the wrong signature for the Lorentzian theory.
- The correct curved/Lorentzian route is the **BDG operator → $\Box$**, which exists (O0)
  but is a *different* chain from $L_Q\to$ Schrödinger.

The two chains ($L_Q\to$ flat Schrödinger, and BDG → $\Box$ → GR) remain **disjoint**,
consistent with `Q_ContinuumLimit.md`. No Laplace–Beltrami step exists to bridge them, and
none is reconstructed here.
