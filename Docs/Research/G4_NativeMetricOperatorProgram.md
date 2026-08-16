# G4 — Native Metric-to-Operator Coupling

**Program ID:** G4
**Status:** PROPOSED (program definition — no experiment executed yet)
**Predecessors:** `MetricOperatorProgram.md`, `WeightedLaplacian_Report.md`,
`LaplaceBeltramiApproximation.md`, `BDGOperatorContinuum_Report.md`,
`MetricEmergenceProgram.md`, `ConformalStructureProgram.md`, `MetricOriginClosure.md`.
**Constraint:** No new primitives. Use existing TQM v1.0 framework only.

---

## 1. Goal

Investigate whether a **geometric operator** (a Laplace/d'Alembertian-type operator acting
on fields over Q-events) can be constructed **directly from causal density and event
structure**, without importing Laplace–Beltrami (LB) or Benincasa–Dowker–Glaser (BDG)
machinery.

The starting chain is already established and logically closed **for the metric**:

```
Q-events  →  causal order  →  conformal class  →  conformal factor  →  metric
 (native)     (native)          (IMPORTED:          (native: ρ^(2/d),   (determined)
                                 Malament/HKM)       counting measure)
```

The **operator** is currently imported, not native:

- Riemannian: $L_W = D_K - K$, the weighted graph Laplacian over the *spatial* coupling
  $K_{ij}=K\exp(-d/\lambda)$. The Laplacian form is imported; $K_{ij}$ is a spatial
  coupling, not a metric coefficient (`MetricOperatorProgram.md`).
- Lorentzian: the BDG operator with fixed binomial coefficients over causal layers $L_k$
  (`BdgUniquenessAnalyzer`). The layer structure is native; the binomial weights are
  imported.

G4 asks whether the operator can be made **native** in the same sense the conformal factor
is native.

---

## 2. Constraint boundary (what "native" means here)

**Native data — allowed, no import:**

1. The set of Q-events $V$ (finite).
2. The causal order $\prec$ (a partial order; precedence).
3. The counting measure: for any causal interval/region, $\rho$ = number of events
   (the **causal density**).
4. Everything definable from (1)–(3) by counting:
   - **links** (Hasse edges): $i \ltimes j \iff i\prec j$ and there is no $k$ with
     $i\prec k\prec j$;
   - **degree** $d(i)$ = number of links incident to $i$ (a native density proxy);
   - **interval volume** $|[i,j]|$ = number of events $k$ with $i\prec k\prec j$;
   - **layers** $L_k(x)=\{y\prec x : |[y,x]|=k\}$;
   - **common past/future** $|J^-(i)\cap J^-(j)|$.

**Imported machinery — forbidden in the construction step:**

- the Laplace–Beltrami formula $\Delta_g\varphi=\frac1{\sqrt{|g|}}\partial_\mu(\sqrt{|g|}\,g^{\mu\nu}\partial_\nu\varphi)$;
- the BDG binomial coefficients $(-1)^{k+1}\binom{d+1}{k}$;
- any explicit metric tensor $g_{\mu\nu}$ supplied as input;
- the Malament/HKM reconstruction (already accepted as imported **for the metric**; G4 does
  not re-derive it, but also must not silently reuse it to build the operator).

**Boundary case (allowed as *benchmark*, not as *source*):** a conformally-flat metric
$g=f\cdot\eta$ with $f=\rho^{2/d}$ may be used **only** to compute the *reference* operator
that native candidates must reproduce. It may not feed the native operator itself.

---

## 3. Research questions (the four investigation areas)

| # | Area | Question |
|---|---|---|
| 1 | Density-derived weighted Laplacians | Can the *weight rule* be replaced by the causal density itself (degree, interval counts)? |
| 2 | Metric–operator correspondence | Does the native operator converge to $\Delta_g$ where $g$ is the native metric $f\cdot\eta$? |
| 3 | Spectral curvature indicators | Can scalar curvature be read off the native operator's spectrum alone (heat trace / Weyl)? |
| 4 | Operator emergence from event geometry | Does the operator arise as the generator of a native stochastic process (diffusion) on the event set, with no imposed operator form? |

---

## 4. Candidate constructions

### C1 — Causal-link Laplacian (symmetrized Hasse diagram)

**Definition.** Build the undirected link adjacency from the causal order:

$$A_{ij} =
\begin{cases}
1, & i \ltimes j \ \text{or}\ j \ltimes i,\\
0, & \text{otherwise},
\end{cases}
\qquad
D_{ii}=\sum_j A_{ij},
\qquad
L_{\text{link}} = D - A.$$

**Native?** Yes — only $\prec$ (to detect links) is used. No kernel, no metric, no LB.

**Continuum expectation.** For a Poisson sprinkling into a $d$-dimensional Riemannian slice,
$L_{\text{link}}$ is the unnormalized graph Laplacian of the link graph; it converges to the
**density-weighted** operator $p(x)\,\Delta_g$ (up to scale), *not* to $\Delta_g$ directly.

**Failure mode.** Density bias (F2 below). The link graph also has near-null clustering
(F3) in the Lorentzian setting.

### C2 — Density-normalized Laplacian (native "density-derived weighted Laplacian")

**Definition.** Same $A$, but normalize by the native degree (the causal density proxy):

$$L_{\text{rw}} = I - D^{-1}A
\qquad\text{(random-walk normalized), or}\qquad
L_{\text{sym}} = I - D^{-1/2}A\,D^{-1/2}.$$

**Native?** Yes — the degree $D_{ii}$ *is* the causal density proxy; the "weight" assigned to
each link is $1/d(i)$, i.e. the inverse causal density. This is the genuinely density-derived
weight rule that area 1 asks for.

**Continuum expectation (key claim to test).** By the Belkin–Niyogi / Coifman–Lafon theorem
for kernel graph Laplacians, the random-walk normalized Laplacian converges to $\Delta_g$
**independently of the sampling density $p$**:

$$\frac{1}{\varepsilon}\left(I - D^{-1}W\right)\ \xrightarrow[N\to\infty,\ \varepsilon\to0]{}\ -\Delta_g,$$

with $\varepsilon$ the kernel bandwidth (for the link graph, $\varepsilon$ = the mean link
separation). C2 is the candidate that should remove the density bias of C1 and yield a
*genuinely metric* operator.

**Failure modes.** Near-null dominance (F3), wrong normalization exponent (F4), finite-$N$
noise (F5).

### C3 — Interval / overlap kernel Laplacian (event-geometry operator)

**Definition.** Replace the link adjacency with a **native volume kernel** built from
counting alone:

1. *Interval kernel:* $K_{ij}=|[i,j]|$ for comparable $i,j$; $0$ otherwise.
   (In $d$ dimensions $|[i,j]|\propto(\text{proper time})^{d}$.)
2. *Common-past overlap:* $O_{ij}=|J^-(i)\cap J^-(j)|$ (symmetric, native).

Then $D_{ii}=\sum_j K_{ij}$ and $L_K = D - K$ (with optional normalization as in C2).

**Native?** Yes — interval volumes and overlaps are pure counting measure.

**Continuum expectation.** Unknown a priori; this is a genuinely *new* operator that is not
the link Laplacian and not the BDG operator. The program must characterize whether
$L_K$ (normalized) converges to $\Delta_g$, to a density-corrected variant, or to something
else.

**Failure modes.** Kernel non-uniqueness (F7), noise (F5), possibly no clean continuum limit.

### C4 — Metric–operator correspondence (benchmark, not source)

**Definition.** The *reference* operator produced by the native conformal factor
$f=\rho^{2/d}$ and the conformally-flat ansatz $g=f\cdot\eta$:

$$\Delta_g\varphi = f^{-d/2}\,\partial_\mu\!\left(f^{d/2-1}\,\eta^{\mu\nu}\,\partial_\nu\varphi\right).$$

**Role.** C4 is the **target**. It uses the LB formula (imported), so it is *not* a native
candidate. G4's central claim to be established is:

> **Correspondence conjecture.** For a Poisson sprinkling, the native operator C2 (and, if
> shown, C3) converges to C4 with the native $f=\rho^{2/d}$, i.e.
> $L_{\text{rw}}\to\Delta_g$ **where the metric is the one already determined natively**
> (conformal class × conformal factor).

Proving (numerically, then analytically) this correspondence *closes* the metric-to-operator
gap natively: the operator is then determined by the same two native data (order + counting
measure) that determine the metric.

### C5 — Native Lorentzian operator from layers (requirement-determined weights)

**Definition.** The layer sets $L_k(x)$ are native (counting + order). Define a candidate
d'Alembertian

$$B\varphi(x)=\sum_{k=1}^{K} w_k \sum_{y\in L_k(x)} \varphi(y),$$

and **derive** the weights $w_k$ from native requirements instead of importing them:

1. **Zero mode:** $B\,\mathbf{1}=0$ ($\sum_k w_k=0$).
2. **Locality:** finite $K$.
3. **Continuum limit:** $B\to\Box$ on a flat sprinkling (finite-difference identity).

**Claim to test.** Requirements (1)–(3) force $K=d+1$ and $w_k = (-1)^{k+1}\binom{d+1}{k}$
(up to normalization) — the binomial coefficients **emerge** from the requirements rather than
being imported. If true, the BDG operator is *derivable* natively and G4 removes the last
import in the Lorentzian sector.

**Failure mode.** The finite-difference uniqueness may fail for a *random* (Poisson)
sprinkling, where layers are not exactly aligned (F5, F8).

---

## 5. Spectral curvature indicators (area 3)

Given the spectrum $\{\lambda_k\}$ of any native operator $L$ (built from event structure +
density only), define curvature/volume observables **without ever forming $g_{\mu\nu}$**:

### S1 — Heat trace (scalar curvature)

The heat trace $Z(t)=\sum_k e^{-t\lambda_k}$ has the small-$t$ expansion for a $d$-dimensional
manifold:

$$Z(t)=(4\pi t)^{-d/2}\left[\mathrm{vol}(M)+\frac{t}{6}\int_M R\,dV+O(t^2)\right].$$

With $\mathrm{vol}(M)=N$ (the counting measure is the volume), the **native curvature
indicator** is

$$\boxed{\;C_R(t)=\frac{6}{t}\left[(4\pi t)^{d/2}\sum_k e^{-t\lambda_k}-N\right]\;\xrightarrow[t\to0]{}\int_M R\,dV.\;}$$

### S2 — Weyl law (dimension and volume)

$N(\lambda)=\#\{k:\lambda_k\le\lambda\}\approx \frac{\omega_d}{(2\pi)^d}\,\mathrm{vol}(M)\,\lambda^{d/2}$,
with $\omega_d$ the unit-ball volume. The slope of $\log N(\lambda)$ vs $\log\lambda$
recovers $d$; the intercept recovers $\mathrm{vol}$. Native and metric-free.

### S3 — Spectral zeta / eigenvalue ratios (dimensionless curvature proxies)

$\zeta(s)=\sum_{k>0}\lambda_k^{-s}$; $\mathrm{Res}_{s=d/2}\zeta$ gives $\mathrm{vol}$;
dimensionless combinations (e.g. ratios of heat-trace coefficients) are pure shape/curvature
invariants.

**Test target:** on a known curved manifold (2-sphere $R=2$; conformally-flat 2D disk with
known $R$), S1 must converge to $\int R\,dV$ as $N\to\infty$ then $t\to0$ (the limit order
$N\to\infty$ *before* $t\to0$ is essential and must be asserted).

---

## 6. Operator emergence from event geometry (area 4)

**E1 — native diffusion generator.** The link graph defines a native Markov chain with
transition matrix $P=D^{-1}A$. Its generator $G=I-P$ *is* $L_{\text{rw}}$ (C2), and the
diffusion semigroup $e^{-tG}$ is the native heat flow. The operator therefore **emerges** as
the generator of the only stochastic process definable from event geometry (the causal random
walk), not as an imposed operator. The heat trace S1 is then literally
$\mathrm{Tr}\,e^{-tG}$ — the native diffusion trace.

**E2 — emergence test.** Verify (a) $P$ is a valid stochastic matrix (rows sum to 1,
non-negative), (b) $e^{-tG}$ preserves the counting measure iff $G\mathbf1=0$, (c) the
spectrum of $G$ is real and non-negative, (d) the invariant distribution of $P$ is the
causal density (degree), closing the loop: *the density determines the operator; the operator
returns the density as its invariant measure.*

---

## 7. Mathematical requirements (acceptance criteria)

Any accepted native operator must satisfy:

| ID | Requirement | Formal statement |
|---|---|---|
| R1 | Linearity | $L(\alpha\varphi+\beta\psi)=\alpha L\varphi+\beta L\psi$. |
| R2 | Locality | $(L\varphi)(i)$ depends only on $\varphi$ within a bounded causal neighborhood of $i$. |
| R3 | Causal compatibility | $L$ is a function of $(\prec,\ \text{counting measure})$ only — invariant under order-isomorphism. |
| R4 | Symmetry (Riemannian sector) | $L=L^\top$ (for the spatial operator). |
| R5 | Positive semi-definiteness | $\lambda_k\ge0$ for all $k$. |
| R6 | Zero mode | $L\mathbf1=0$ (constant function annihilated). |
| R7 | Continuum limit | $L\to\Delta_g$ (or $\Box$) with controlled $O(h^2)$ rate on flat references. |
| R8 | Metric-dependence | spectrum changes under non-uniform density/weights. |
| R9 | Density-invariance | continuum limit independent of sprinkling density $p$ (C2's defining property). |
| R10 | Spectral curvature | S1 $\to\int R\,dV$ on known curved manifolds. |
| R11 | Dimension recovery | S2 slope $\to d$. |
| R12 | No new primitives | only $\prec$ and counting measure enter the construction. |

---

## 8. Failure modes (and their symptoms)

| ID | Failure mode | Symptom | Detection |
|---|---|---|---|
| F1 | Lorentzian directedness | the raw causal link graph is directed; symmetrized Laplacian is Riemannian, not $\Box$ | C5/E2; R4 vs $\Box$ signature mismatch |
| F2 | Density bias | $L_{\text{link}}$ (C1) $\to p\cdot\Delta_g$, spectrum biased by density | R9 fails; S2 intercept wrong |
| F3 | Near-null clustering | links concentrate near the light cone → fat-tailed degree, spectrum blow-up/non-locality | degree distribution; R2/R7 fail |
| F4 | Wrong normalization exponent | $I-D^{-\alpha}A D^{\alpha-1}$ with $\alpha\neq1$ gives $\Delta_g+2(1-\alpha)\nabla\ln p\cdot\nabla$ (Fokker–Planck drift) | R9 fails; heat trace has drift term |
| F5 | Sprinkling noise | Poisson fluctuations make interval/overlap kernels (C3) and layers (C5) noisy | variance vs $N$; convergence rate |
| F6 | Boundary effects | finite causal set has unspecified boundary conditions | edge/boundary spectrum deviation |
| F7 | Kernel non-uniqueness | many functions of $|[i,j]|$ give the same continuum limit; finite-$N$ differs | C3 not uniquely pinned |
| F8 | Circularity | constructing the operator to match $f=\rho^{2/d}$ may presuppose the conformal factor | audit: does the operator use $f$ explicitly? |

---

## 9. xUnit test program (specification)

**Placement.** Research tests inherit `ResearchTestBase` (`TQM.Tests/Shared`), use
`ITestOutputHelper` (`Output`), `StringBuilder`, and `PrintHeader(...)`; they are
deterministic (fixed seeds, no randomness in the assertions). Recommended locations:
`TQM.Tests/Research/` (project research-test convention, `TQM_###_Tests.cs`) or a dedicated
`TQM.Tests/ResearchXH/` folder to parallel the `ResearchXC` metric program. Candidate-construction
core code goes in `TQM.Core/ResearchXH/` (namespace `TQM.Core.ResearchXH`).

| Test ID | Test | Deterministic input | Assertion / expected output |
|---|---|---|---|
| G4-01 | `CausalLinkLaplacian_IsConstructible` | fixed small causet (e.g. a 6-event chain/diamond) | $L_{\text{link}}$ symmetric, row-sum $0$, min eigenvalue $=0$ (R4,R5,R6) |
| G4-02 | `CausalLinkLaplacian_ReducesToPathLaplacian` | causet = linear chain (total order) | $L_{\text{link}}=L_Q$ of the path graph exactly |
| G4-03 | `DensityNormalizedLaplacian_IsStochastic` | fixed causet | $P=D^{-1}A$ rows sum to $1$, non-negative; $G=I-P$ has zero mode (R6) |
| G4-04 | `DensityNormalizedLaplacian_ConvergesToFlatLaplacian` | uniform chain $N=32,64,128$ | scaled eigenvalues $\to(\pi k)^2$ at $O(1/N^2)$ (R7) |
| G4-05 | `DensityNormalizedLaplacian_IsDensityInvariant` | 1D chain with **non-uniform** density (e.g. Chebyshev clustering), $N$ growing | low-mode spectrum $\to(\pi k)^2$ independent of density (R9); unnormalized C1 fails this (F2) |
| G4-06 | `DensityNormalizedLaplacian_MatchesKnownManifold` | cycle $S^1$, $N=32,64,128$ | scaled spectrum $\to\{k^2\}$ at $O(1/N^2)$ (R7) |
| G4-07 | `MetricOperatorCorrespondence_ConformalFactor` | conformally-flat 2D $f=1+\tfrac12 x^2$ | native $L_{\text{rw}}$ spectrum $\to$ spectrum of C4 $\Delta_g$ (R7, correspondence conjecture) |
| G4-08 | `IntervalKernelLaplacian_IsConstructible` | fixed causet | $L_K$ symmetric, zero row-sum, PSD (R4–R6) |
| G4-09 | `IntervalKernelLaplacian_ConvergenceProbe` | chain causet $N$ growing | characterize: converges to $\Delta_g$, density-corrected, or neither (C3) |
| G4-10 | `HeatTraceCurvature_FlatManifold` | uniform chain/cycle | $C_R(t)\to0$ (flat $\int R=0$) |
| G4-11 | `HeatTraceCurvature_CurvedManifold` | $S^2$ ($R=2$, $\int R\,dV=8\pi$) sprinkling, $N$ growing | $C_R(t)\to8\pi$ as $N\to\infty$ then $t\to0$ (R10) |
| G4-12 | `WeylLaw_DimensionRecovery` | $d$-dim flat sprinkling | $\log N(\lambda)$ slope $\to d/2$ (R11) |
| G4-13 | `NativeLayerOperator_WeightsEmergeFromRequirements` | flat 1+1 sprinkling; requirements R1–R3 | weights forced to $(1,-2,1)$ (2D binomial) up to scale; otherwise assert fail (C5) |
| G4-14 | `NativeDiffusion_PreservesCountingMeasure` | fixed causet | invariant distribution of $P$ = degree/counting measure (E2) |

Each test writes a multi-section report (assumptions → intermediate calculations → final
conclusions) to `Output`, per the research-test contract.

---

## 10. Acceptance criteria (program success)

1. **C2 passes R7–R9** on flat, non-uniform-density, and $S^1$ references — establishing the
   native density-normalized Laplacian as a genuine metric operator.
2. **G4-07 passes** — the native operator reproduces $\Delta_g$ for the native
   $f=\rho^{2/d}$, closing the metric–operator correspondence natively.
3. **G4-11 passes** — scalar curvature is recovered from the spectrum alone (S1), with no
   metric tensor formed.
4. **G4-13 resolves C5** — either the BDG weights are derived from native requirements (import
   removed) or the program records *why* they cannot be (a genuine negative result, still
   valuable).
5. **No new primitives** — every construction is a function of $(\prec,\text{counting measure})$.

## 11. Risks / open questions

- The link-graph $\to\Delta_g$ correspondence (C2) is conjectural for causal sets in the
  **Lorentzian** signature; the Riemannian (symmetrized) version is on firmer ground.
- The limit order $N\to\infty$ *before* $t\to0$ (S1) is delicate and must be asserted, not
  assumed.
- C3 (interval kernel) may not have a clean $\Delta_g$ limit — if so, the program records it
  as a rejected candidate (per the failure-mode discipline) rather than forcing a fit.
