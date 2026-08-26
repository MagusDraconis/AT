# AT-QG Phase 27 — TRM/AT Observable Bridge

**Program:** AT-QG (Unification)
**Phase:** 27 — compare TRM effective propagation with AT geometric optics
**Status:** COMPLETED — 3/3 xUnit tests pass (84/84 AT-QG)
**Constraint:** no new primitives (the "temporal fraction" t is a diagnostic, not physics)

---

## 1. Goal

QG21/26 used GR null-geodesic optics and found NO lensing (γ = −1). TRM, by contrast, reproduced lensing-like
effects through **effective propagation** (the time-rate field acting as a refractive medium). Here we ask: can
AT's actualization density ρ generate lensing, time delay, or magnification through effective propagation —
without tensor curvature — and how does that compare to GR and to AT's own geometry?

---

## 2. The "temporal fraction" diagnostic

For the same weak-field ρ with potential Φ = (1/d)ln ρ, light propagation is controlled by a single parameter
t ∈ [0,1] — how much of the conformal factor enters via g_00 alone (temporal) versus canceled by g_ii:

| prescription | t | effective index n | result |
|---|---|---|---|
| AT geometry (full conformal metric) | 0 | n = 1 (factor cancels) | no lensing |
| TRM effective propagation (temporal-only) | 1 | n = e^Φ | full GR lensing |

Every lensing observable scales **linearly** in t:
deflection α = 4GM/b · t, Shapiro Δt = 2GM/c³·ln · t, convergence κ = Σ · t, magnification μ = 1/[(1−κ)²−γ_s²].

---

## 3. Results

### (a) Deflection (ATQG270)
- AT geometry (t=0): n = 1, α = 0 → **NO EFFECT**.
- TRM effective (t=1): n = e^Φ, α = 4GM/b = Einstein deflection → **SAME EFFECT**.

### (b) Time delay & magnification (ATQG271)
- AT geometry: Δt = 0, μ = 1 → **NO EFFECT**.
- TRM effective: Δt = 2GM/c³·ln, μ = GR value → **SAME EFFECT**.

### (c) Three-way classification (ATQG272)

| framework (same ρ) | vs GR |
|---|---|
| GR null geodesics | reference |
| TRM effective propagation | **SAME EFFECT** |
| AT geometry | **NO EFFECT** |

---

## 4. Conclusion — the bridge is the optics, not the tensor sector

AT's ρ **can** generate full GR lensing (SAME EFFECT) — but only under TRM's *temporal-only* optics (t=1), which
ignores the spatial g_ii. AT's **own** metric (t=0) cancels the conformal factor and gives **NO EFFECT**. The
lensing discrepancy is therefore a choice of **light-propagation prescription** (null geodesic vs effective medium),
**not** the tensor sector:

- AT keeps NO EFFECT (conformal null geodesics, γ = −1).
- TRM's lensing is real and exactly GR-like, but it lives in a **different** theory of light propagation
  (an effective refractive medium) that AT's conformal metric does not derive.
- This reconciles QG20 (temporal waves: NO GWs — a tensor observable) with the TRM lensing claim (a scalar
  observable that *does* reproduce GR under effective propagation).

The effective-medium assumption (n = e^Φ) is exactly the "non-conformal coupling" that QG21 already flagged as the
single missing ingredient — it is a distinct, imported propagation rule, not a new tensor field.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG270 `ATQG270_IndexAndDeflection` | PASS (n=1 vs e^Φ; α=0 vs GR) |
| ATQG271 `ATQG271_DelayAndMagnification` | PASS (Δt=0,μ=1 vs GR) |
| ATQG272 `ATQG272_ThreeWayClassification` | PASS (TRM SAME EFFECT, AT NO EFFECT) |

Code: `AT.Core/ResearchXH/TRMObservableBridge.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase27_TRMObservableBridgeTests.cs`.
