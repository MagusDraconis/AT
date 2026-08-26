# AT-QG Phase 30 — Q-Event Correlation Dynamics

**Program:** AT-QG (Unification)
**Phase:** 30 — can Q-event correlations generate the effective kernels without ψ?
**Status:** COMPLETED — 3/3 xUnit tests pass (93/93 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

Q-events are local temporal-network transitions (QG29). Here we test whether CORRELATIONS between Q-events (tick
correlations, synchronization defects, branching covariance, temporal-network propagation, emergent bilocal
kernels) can generate the systematic observation-level effects (lensing, delay, magnification) previously
attributed to the TRM kernel — WITHOUT introducing ψ.

---

## 2. Key distinction: 1-point vs 2-point

- The **background metric** g = ρ̄^(2/d)η is set by the **1-point function** ρ̄ (conformal → n = 1, no lensing).
- **Correlations** are **2-point** quantities: K(x,y) = ⟨δρ(x) δρ(y)⟩ is a **variance with ZERO mean**.

A systematic (background) lensing effect requires a non-zero **mean**; correlations have zero mean by construction.

---

## 3. Results

### (a) Zero mean vs nonzero variance (ATQG300)

| quantity | value | kind |
|---|---|---|
| mean deflection ⟨δα⟩ | 0 | systematic (vanishes) |
| mean Shapiro delay ⟨Δt⟩ | 0 | systematic (vanishes) |
| mean magnification ⟨μ⟩ | 1 | systematic (no focusing) |
| deflection variance ⟨α²⟩ | 8πσ²ξ² > 0 | stochastic (jitter) |

### (b) Systematic vs stochastic (ATQG301)
- Correlations produce **zero-mean jitter** (scintillation), NOT systematic lensing.
- A mean-field **scalar renormalization** of ρ̄ remains conformal (n = 1) — it cannot turn on deflection.

### (c) Determination (ATQG302)
All five correlation mechanisms (tick correlations, synchronization defects, branching covariance, temporal-network
propagation, emergent bilocal kernels) produce **jitter + scalar renormalization**, none of which breaks conformal
flatness.

---

## 4. Conclusion: correlations CANNOT replace ψ

- The background metric is fixed by the 1-point ρ̄ (conformal, n = 1); correlations are 2-point (variance) with
  zero mean, so they produce **no systematic** deflection/delay/magnification.
- A scalar renormalization of ρ̄ remains a conformal factor; it cannot break conformal flatness.
- Systematic lensing requires the **anisotropic (rank-2) ψ sector** — a scalar and its isotropic correlations
  cannot supply it.

Correlations add only a **stochastic (jitter) layer** on top of the conformal background. This is a *new*
observable (image jitter / scintillation from density fluctuations) but it is **not** gravitational lensing and it
does **not** substitute for ψ. The ψ requirement of QG23/QG24/QG28 stands.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG300 `ATQG300_ZeroMeanVsVariance` | PASS (zero mean, nonzero variance) |
| ATQG301 `ATQG301_SystematicVsStochastic` | PASS (jitter, no lensing) |
| ATQG302 `ATQG302_Determination` | PASS (correlations cannot replace ψ) |

Code: `AT.Core/ResearchXH/QEventCorrelations.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase30_QEventCorrelationsTests.cs`.
