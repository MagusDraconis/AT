# Q Configuration Dynamics Audit

**Goal:** derive or reconstruct equations of motion for $x_i$ and $\theta_i$ using only
existing TQM assumptions. No new primitives, no new parameters.
**Inputs:** `03_Q_Theory.md`, `04_Q_Networks_and_Laplacian.md`, `Q_Formalization_Program.md`,
`TemporalSimulation.cs`, `TemporalMatrix.cs`, `TopologyEvolutionAnalyzer.cs`,
`ThetaFieldEquation.cs`.

---

## 1. Inventory of existing interaction laws

| # | Law | Form | Source |
|---|---|---|---|
| 1 | Spatial coupling | $J_{ij}=\exp(-|x_i-x_j|/r_c)$ | `03_Q_Theory.md` |
| 2 | Thresholded adjacency | $A_{ij}=\mathbf1[J_{ij}>\text{threshold}]$ | `03_Q_Theory.md` |
| 3 | Phase coupling (Kuramoto) | $K_{ij}\sin(\theta_j-\theta_i)$ | `TemporalSimulation.cs`, `TopologyEvolutionAnalyzer.cs` |
| 4 | State-vector dynamics | $i\partial_t\psi=L_Q\psi$ | `02_Fundamental_Postulates.md` |
| 5 | Macroscopic phase-gradient force | $\vec a=c^2\nabla\theta$ | `PhaseGradientGravityAnalyzer.cs` |

---

## 2. Does $J_{ij}$ implicitly define dynamics?

**Partially.** $J_{ij}=\exp(-|x_i-x_j|/r_c)$ enters the Kuramoto coupling matrix $K_{ij}$
(`TemporalMatrix.FillSpatialCoupling`), so it **implicitly defines the phase dynamics** —
the strength of the $\theta_i$ coupling depends on the *static* positions. But $J_{ij}$
does **not** define any equation of motion for the positions themselves: it is a static
kernel, evaluated once and fixed. Hence $J_{ij}$ implies $\dot\theta_i$ but not
$\dot x_i$.

---

## 3. Search for dynamics

| Category | Result | Source |
|---|---|---|
| **Gradient flow** | Present **macroscopically** only — $\vec a=c^2\nabla\theta$ (phase-gradient gravity); no microscopic $\dot x_i=-\nabla\Phi$ | `PhaseGradientGravityAnalyzer.cs` |
| **Phase evolution** | **Present** — the Kuramoto equation $\dot\theta_i=\omega_i+\frac{K}{N}\sum_j K_{ij}\sin(\theta_j-\theta_i)$ is explicit in `TemporalSimulation.Step()` | `TemporalSimulation.cs` |
| **Network evolution** | **Absent** — `TopologyEvolutionAnalyzer` *measures* topology dependence of $dR/dt$ but never evolves the network; positions $X,Y$ are set at initialization and never updated | `TopologyEvolutionAnalyzer.cs` |
| **Causal update rules** | Absent as position dynamics — `MinimumActualizationIntervalAnalyzer`/`StructuralEvolutionAnalyzer` concern Q-event timing/structure, not $\dot x_i$ | `ResearchQG/*` |

**Key finding:** there is an explicit **phase** dynamics (Kuramoto), but **no position**
dynamics anywhere. Positions are a *static input*, not a dynamical degree of freedom.

---

## 4. Minimal dynamics candidate (reconstruction)

Using only existing ingredients, the minimal configuration dynamics is:

$$\boxed{\;\dot\theta_i = \omega_i + \frac{K}{N}\sum_{j}K_{ij}\sin(\theta_j-\theta_i),\qquad
K_{ij}=\exp\!\Big(\!-\frac{|x_i-x_j|}{r_c}\Big),\qquad \dot x_i = 0\;}$$

- **Phase:** the Kuramoto equation — already implemented (`TemporalSimulation.Step()`).
- **Position:** static ($\dot x_i=0$) — the *de facto* assumption of the entire Resonance
  program; no evolution law exists.

**Parameter check (no new primitives/parameters):** $\omega_i$ (natural frequencies), $K$
(global coupling), $K_{ij}$/$r_c$ (spatial coupling) are all **pre-existing** ingredients.
The per-node frequency $\omega_i$ is, however, a **free** parameter (not derived), and the
static-position assumption is exactly the "missing piece" identified in the
`Q_Formalization_Program` (§2.6).

---

## 5. Classification

| Degree of freedom | Equation | Classification |
|---|---|---|
| $\theta_i$ (phase) | Kuramoto $\dot\theta_i=\omega_i+\frac{K}{N}\sum_j K_{ij}\sin(\theta_j-\theta_i)$ | **Derived** (explicit in repository) |
| $x_i$ (position) | none — static ($\dot x_i=0$) | **Missing** |

| Overall minimal candidate | Classification |
|---|---|
| Kuramoto phase dynamics + static lattice | **Reconstructed** — phase side derived, position side assumed static |

---

## 6. Conclusion

The **phase** dynamics of the $Q$-configuration is already **derived**: it is the Kuramoto
model, implemented verbatim in `TemporalSimulation.Step()`. The **position** dynamics is
**missing**: positions are a static input, and $J_{ij}$ defines only the *strength* of the
phase coupling, not a motion law for $x_i$.

A minimal dynamics candidate can therefore be **reconstructed** (Kuramoto for $\theta_i$,
static for $x_i$) using zero new primitives and zero new parameters — but it is not fully
*derived*, because the static-position assumption is exactly the gap the formalization
program flagged. The single open question for full formalization is: **is there a
$\dot x_i$ law?** The repository's only position-related dynamics is the macroscopic
$\vec a=c^2\nabla\theta$, which is emergent gravity, not a microscopic equation of motion —
so at the microscopic level $\dot x_i$ remains **missing**, and any candidate (e.g. a
gradient flow $\dot x_i=-\mu\nabla_i\Phi$) would introduce new physics, which this audit
does not do.
