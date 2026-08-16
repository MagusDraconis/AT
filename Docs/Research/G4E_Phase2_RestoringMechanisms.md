# G4-E Phase 2 — Restoring Mechanisms

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-E)
**Phase:** 2 — search for a native restoring mechanism
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can any primitive-native term stabilize ρ around flat?
**Primitives used:** ρ · R · arithmetic (linear/cubic terms, counting-measure constraint). No metric tensor, no Einstein equations, no Laplace–Beltrami import.

---

## 1. Goal

Phase 1 established that the naive feedback ρ̇ = −kR is **anti-diffusive** (flat unstable,
runaway). Search for a term built only from ρ and R that stabilizes the system:

1. **Diffusion:** ρ̇ = −kR − d(ρ−1)
2. **Logistic:** ρ̇ = −kR − c(ρ−1)³
3. **Conservation:** mean(ρ) = 1 constraint

Measure fixed points, stability, oscillation, boundedness. Success = a stable finite attractor
with no new primitives.

---

## 2. Results

### 2.1 G4-E20 — diffusion

F′(1) = −10.68 ⇒ critical diffusion **d\* = k·|F′(1)| = 10.68** (linearization λ = k|F′(1)| − d).

| d | ρ₀ | ρ_T | class | flat-stable | bounded |
|---|---|---|---|---|---|
| 3.00 | 0.90 | −0.445 | fixed | no | yes |
| 3.00 | 1.10 | +2.588 | fixed | no | yes |
| 10.68 | 0.90 | 1.000 | fixed | yes | yes |
| 10.68 | 1.10 | 1.446 | fixed | no | yes |
| 15.00 | 0.90 | 1.000 | fixed | yes | yes |
| 25.00 | 1.10 | 1.000 | fixed | yes | yes |

- **d < d\*:** flat unstable, but the bounded F cannot outrun the linear restoring term, so the
  system settles onto two **finite off-flat attractors** (bistable).
- **d = d\*:** marginal (asymmetric: converges to flat from below, to 1.446 from above).
- **d > d\*:** flat is a **globally stable attractor** — the system stabilizes *around flat*.

### 2.2 G4-E21 — logistic

The cubic term has no linear part, so flat stays unstable; balance with anti-diffusion gives
two stable finite points (asymmetric, because F clamps at +4.335 / −4.764):

| c | below-flat attractor | above-flat attractor |
|---|---|---|
| 0.5 | −1.054 | +3.120 |
| 1.0 | −0.631 | +2.683 |
| 2.0 | −0.294 | +2.336 |

All 12 trajectories converge (0 oscillatory), all bounded. The attractors approach flat as c
increases.

### 2.3 G4-E22 — conservation + comparison

Conservation mean(ρ)=1 pins the mean density to flat (ρ_t = 1 for every step) — a trivially
stable, degenerate stabilizer.

| mechanism | fixed point | stability | oscillation | bounded | stable attractor |
|---|---|---|---|---|---|
| none (anti-diffusive) | flat | unstable | no | no | no |
| diffusion d > d\* | flat | stable | no | yes | **YES (flat)** |
| logistic c > 0 | two finite pts | bistable | no | yes | **YES (finite pair)** |
| conservation | flat (pinned) | trivial | no | yes | YES (degenerate) |

---

## 3. Conclusion

**YES.** Primitive-native restoring terms stabilize the anti-diffusive feedback without any new
primitive:

- **Diffusion** (d > d\* = k|F′(1)|) makes flat a **stable finite attractor** — the cleanest
  "stabilize ρ around flat" mechanism.
- **Logistic** (−c(ρ−1)³) yields a **bistable** system: flat unstable but two stable finite
  attractors — a bounded, non-runaway cosmology.
- **Conservation** pins flat but is degenerate (it removes the degree of freedom, forbidding all
  curvature dynamics).

This closes the G4-E feedback program: the anti-diffusive instability is not fundamental — it is
the absence of a restoring term, and a stable bounded cosmology is reachable natively.

---

## Test program

| Test | Verdict |
|---|---|
| G4-E20 `G4_E20_DiffusionTermStabilizesFlat` | PASS (d\* = 10.68; flat stable for d > d\*) |
| G4-E21 `G4_E21_LogisticTermGivesBistableAttractors` | PASS (12/12 bounded, converged, 0 oscillatory) |
| G4-E22 `G4_E22_ConservationPinsFlatAndComparison` | PASS (conservation pins flat; comparison table) |

Code: `TQM.Core/ResearchXH/CurvatureFeedback.cs` (added `RestoringTerm`, `SimulateRestoring`);
tests `TQM.Tests/ResearchXH/G4E_Phase2_RestoringMechanismsTests.cs`.
