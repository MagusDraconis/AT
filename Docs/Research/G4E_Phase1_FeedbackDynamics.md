# G4-E Phase 1 — Curvature–Density Feedback

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-E)
**Phase:** 1 — test curvature–density feedback
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can reconstructed curvature modify future density evolution?
**Primitives used:** ρ · L · Lc · spectral observables. No metric tensor, no Einstein equations, no Laplace–Beltrami import.

---

## 1. Goal

Close the loop from Phase 0 (R = F(ρ), Ṙ = F′(ρ)·ρ̇) by feeding reconstructed curvature back
into the density, and characterize the resulting self-consistent dynamics.

$$\rho_{t+1}=\rho_t+\Delta t\,\dot\rho(\rho_t),\qquad \dot\rho\in\{-kR,\;-k\,\mathrm{sign}\,R,\;-kR\rho\}$$

The native F map (ρ̄ → R̂) was rebuilt from the reconstruction: 17 points, ρ̄ ∈ [0.8137, 1.3108],
R̂ ∈ [−4.764, +4.335], with F(1) = 0 exactly and **F′(1) = −10.68**.

---

## 2. Results

### 2.1 G4-E10 — fixed point and stability

| model | fixed point(s) | eigenvalue | stability |
|---|---|---|---|
| ρ̇ = −kR | ρ* = 1 | λ = −kF′(1) = **+10.68** | **unstable** |
| ρ̇ = −k·sign(R) | ρ* = 1 | — (constant-speed repulsion) | **unstable** |
| ρ̇ = −kRρ | ρ* = 1, ρ* = 0 | +10.68 / attracting from below | **unstable / stable** |

Trajectories started at ρ₀ = 1 ± 0.02 diverge from flat under every model.

### 2.2 G4-E11 — runaway vs oscillation

Simulated 3 models × 4 initial conditions (T = 200):

| model | ρ₀ = 0.85 | 0.95 | 1.05 | 1.15 |
|---|---|---|---|---|
| −kR | −42.4 | −41.8 | +48.0 | +48.6 |
| −k·sign(R) | −9.15 | −9.05 | +11.05 | +11.15 |
| −kRρ | → 0 (fixed) | → 0 (fixed) | → +∞ | → +∞ |

**0/12 oscillatory**; all trajectories move away from flat. The product model has a
second, unphysical fixed point ρ = 0 (attracting from below flat).

### 2.3 G4-E12 — self-consistent, anti-diffusive dynamics

**2217/2217 (100 %)** steps satisfy sign(ρ̇) = sign(ρ − 1): the feedback *amplifies* the
density deviation that created it (positive feedback). Since F′(1) < 0, every model of the
form ρ̇ = −kR(·ρ) is anti-diffusive.

---

## 3. Conclusion

The closed system ρ → R = F(ρ) → ρ̇ is **self-consistent but anti-diffusive**: flat (ρ̄ = 1) is
an **unstable fixed point** (λ = +10.68) and every trajectory runs away monotonically (no
oscillation). The naive native curvature feedback is *curvature-amplifying* — it cannot, by
itself, produce a bounded cosmology. A stable, non-runaway dynamics would require an
**additional restoring term** (e.g. a sign flip, a higher-order −ρ² or diffusion term, or a
potential). This is the quantitative statement of the "feedback instability" and points to the
next phase (restoring terms / bounded attractors).

---

## Test program

| Test | Verdict |
|---|---|
| G4-E10 `G4_E10_FixedPointAndStability` | PASS (F(1)=0, F′(1)=−10.68, flat unstable) |
| G4-E11 `G4_E11_RunawayVersusOscillation` | PASS (0/12 oscillatory, all runaway) |
| G4-E12 `G4_E12_SelfConsistentAntiDiffusiveDynamics` | PASS (2217/2217 anti-diffusive) |

Code: `TQM.Core/ResearchXH/CurvatureFeedback.cs` (`FeedbackModel`, `BuildMap`, `Simulate`,
`Interpolate`, `SlopeAtFlat`, `Classify`); tests
`TQM.Tests/ResearchXH/G4E_Phase1_FeedbackDynamicsTests.cs`.
