# Q Position Dynamics Audit

**Goal:** determine whether $x_i$ dynamics is already implicitly present in AT.
**Inputs:** `Q_Formalization_Program.md`, `Q_ConfigurationDynamics.md`, `03_Q_Theory.md`,
`04_Q_Networks_and_Laplacian.md`, plus the Resonance/Kuramoto and Theory analyzers.
**Discipline:** no new primitives, no new parameters, no proposed physics — reconstruction only.

> **Correction.** The preceding `Q_ConfigurationDynamics` audit concluded $x_i$ dynamics was
> **Missing** after inspecting only `TemporalSimulation.Step()` (phase-only). A full search
> shows this was **incomplete**: an explicit position dynamics exists in ~15 analyzers. This
> audit corrects that conclusion.

---

## 1. Every equation containing $x_i$ (extracted)

| # | Equation | Nature | Source |
|---|---|---|---|
| 1 | $J_{ij}=\exp(-|x_i-x_j|/r_c)$ | static coupling kernel | `03_Q_Theory.md` |
| 2 | $K_{ij}$ from $J_{ij}$ (spatial coupling) | coupling matrix | `TemporalMatrix.FillSpatialCoupling` |
| 3 | $\dot\theta_i=\omega_i+\frac{K}{N}\sum_j K_{ij}\sin(\theta_j-\theta_i)$ | phase dynamics | `TemporalSimulation.Step()` |
| 4 | $x_i\leftarrow x_i+\eta\sum_j K_{ij}\cos(\theta_j-\theta_i)\frac{x_j-x_i}{\|x_j-x_i\|}$ | **position dynamics** | `MeanCouplingDerivationAnalyzer.PositionStep`, `CurvatureMotionAnalyzer`, `CriticalCouplingAnalyzer`, and ~12 others |
| 5 | $\vec a=c^2\nabla\theta$ | macroscopic phase-gradient force | `PhaseGradientGravityAnalyzer` |

---

## 2. Does any equation imply $\dot x_i$?

**Yes.** Equation (4) is an explicit Euler update for $x_i$:

$$\dot x_i = \eta\sum_{j\neq i} K_{ij}\cos(\theta_j-\theta_i)\,\frac{x_j-x_i}{|x_j-x_i|}.$$

This is a **phase-dependent interaction force**: each node is pulled toward phase-aligned
neighbors ($\cos\Delta\theta>0$) and pushed from anti-aligned neighbors
($\cos\Delta\theta<0$), weighted by the coupling. It is, equivalently, **gradient descent on
the coupling energy**

$$E=-\sum_{i<j} K_{ij}\cos(\theta_j-\theta_i)\,|x_j-x_i|,\qquad \dot x_i=-\eta\,\nabla_{x_i}E,$$

which `CurvatureMotionAnalyzer` states verbatim ("position update: gradient descent on
coupling energy").

---

## 3. Search classification

| Category | Status | Evidence |
|---|---|---|
| Energy minimization | **Existing** | gradient descent on $E$ (`CurvatureMotionAnalyzer`); 10 scalar-potential candidates tested (`MinimizationAnalyzer`) |
| Graph rewiring | **Implicit** | positions move ⇒ spatial coupling changes; re-evaluation of $K_{ij}$ varies by analyzer |
| Causal updates | **Missing** | no causal update rule for $x_i$ |
| Phase-gradient motion | **Existing** | force ∝ $\cos(\theta_j-\theta_i)$ — a phase-gradient law |
| Interaction-potential motion | **Existing** | force = $\nabla$ of the coupling-weighted distance potential |

**Overall $x_i$ dynamics classification: Existing.**

---

## 4. The minimal mathematical structure (already present)

The minimal structure required to define $x_i$ evolution is a **coupled Kuramoto +
interaction-potential system**:

$$\dot\theta_i=\omega_i+\frac{K}{N}\sum_j K_{ij}\sin(\theta_j-\theta_i),$$

$$\dot x_i=-\eta\,\nabla_{x_i}\!\sum_{i<j}K_{ij}\cos(\theta_j-\theta_i)|x_j-x_i|,$$

with $K_{ij}=\exp(-|x_i-x_j|/r_c)$ (or its thresholded adjacency form). **Both equations are
already implemented** in the repository — the phase side in `TemporalSimulation.Step()`, the
position side in `PositionStep`/`CurvatureMotion`/`CriticalCoupling` and ~12 further
analyzers. No new primitives and no new parameters are introduced: $\omega_i$, $K$, $K_{ij}$,
$r_c$ are pre-existing; $\eta$ is a numerical step size.

---

## 5. Conclusion

Contrary to the earlier audit, **$x_i$ dynamics is already present in AT**, in the form of a
phase-dependent interaction-potential motion — gradient descent on the coupling energy
$E=-\sum K_{ij}\cos(\theta_j-\theta_i)|x_j-x_i|$. It is **Existing**, not Missing, and is
implemented in ~15 analyzers (e.g. `MeanCouplingDerivationAnalyzer`, `CurvatureMotionAnalyzer`,
`CriticalCouplingAnalyzer`, `AttractionOnsetAnalyzer`, `SpatialCurvatureAnalyzer`,
`DynamicalClosureAnalyzer`). The only caveat is that it is **not** in the canonical
`TemporalSimulation.Step()` (which is phase-only) — the position dynamics lives in the
parallel analyzers that add a `PositionStep`. The configuration dynamics of $Q$ is therefore
**fully specified** (phase + position), and the formalization gap identified in
`Q_Formalization_Program` §2.6 is smaller than reported: both $\dot\theta_i$ and
$\dot x_i$ exist; what remains is only to *promote* the position dynamics into the canonical
integrator and to re-derive it from a single energy functional (which the repository already
states is the coupling energy).
