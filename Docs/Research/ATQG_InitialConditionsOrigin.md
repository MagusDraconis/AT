# AT-QG Phase 227 — Initial Conditions Origin

**Status:** COMPLETE — **INITIAL-CONDITION ORIGIN**
**Tests:** ATQG2270, ATQG2271, ATQG2272 (all passed)
**Core class:** `AT.Core/ResearchXH/InitialConditionsOrigin.cs`
**Inputs:** QG1/QG7 (branching actualization), QG222 (∂_t ρ = (ln μ)·ρ, native dynamics), QG206 (α=0
unique scale-free), G4-RHO (entropy H(α) maximized at α=0), QG216 (ρ_k = μ^k/S at criticality),
QG116b/117/121 (universal attractor, exact fixed point, near-universal basin)
**Method:** deterministic derivation — no new primitives
**Closes:** QG226 TOE criterion 6 (initial conditions): OPEN → DERIVED

---

## 1. The Question

QG226 (TOE audit) found **initial conditions** to be the single fully-open TOE
criterion: no phase derived why the universe starts in its specific initial
state. This phase derives it — no new primitives, deterministic.

---

## 2. The Origin — the initial state is the UNIFORM CRITICAL STATE

### ρ_k = 1/K (μ = 1, α = 0)

| # | Investigation | Result |
|---|---------------|--------|
| 1 | **Fixed points / stationarity** | an initial state must be stationary (∂_t ρ = 0); from the native dynamics (QG222) ∂_t ρ = (ln μ)·ρ ⇒ **μ = 1 (critical)**; any μ≠1 is a transient |
| 2 | **Scale-freeness** | α=0 (equal deficit per octave) is the **unique scale-free** state (QG206): spread 0 vs >0 for α=±0.3; α≠0 introduces a preferred scale = information with no source |
| 3 | **Minimum-information states** | among critical states the least-committal allocation is uniform: **ρ_k = 1/K**; it maximizes the native entropy H(α) (H(0) = ln K ≥ H(α)) — zero initial-condition input needed |
| 4 | **Critical branching** | the uniform state IS the critical branching state (QG216 at μ=1: ρ_k = μ^k/S → 1/K) |
| 5 | **Attractors** | the universal attractor (QG116b) is a stable exact fixed point with basin ≥ 0.9 — residual content is **erased**, so no fine-tuning is required |

**The derived initial state: μ = 1, α = 0, ρ_k = 1/K.**

---

## 3. Concrete Values (K = 8)

| Quantity | Value |
|----------|-------|
| ∂_t ρ at μ=1 | 0 (stationary) |
| ∂_t ρ at μ=0.5 / μ=2 | ≠ 0 (transients) |
| Deficit-fraction spread α=0 | 0.0000 (self-similar) |
| Deficit-fraction spread α=±0.3 | 0.03+ (preferred scale) |
| H(0) | ln 8 = 2.0794 (max entropy) |
| H(0.5) | 1.9600 (< H(0)) |
| Uniform state ρ_k | 1/8 each |
| Uniform = critical branching state | YES |
| Attractor exact fixed point | YES |
| Attractor basin | ≥ 0.9 |

---

## 4. Why This Is Not Assumed

- **Not a postulate:** the state follows from the theory's own dynamics —
  stationarity (fixed point) forces criticality, scale-freeness forces α=0,
  and maximum entropy selects uniformity;
- **No new primitive:** only the existing branching process, the entropy
  functional, and the attractor are used;
- **No fine-tuning:** the universal attractor erases residual initial content,
  so the specific choice does not need to be set by hand.

The initial state is **the unique minimum-information fixed point** of the
actualization flow — the state that requires zero initial-condition input.

---

## 5. Classification

### **INITIAL-CONDITION ORIGIN**

Origin score = **5/5**:

1. stationarity ⇒ μ=1 (fixed point of the actualization flow);
2. α=0 is the unique scale-free state (QG206);
3. uniform allocation maximizes the native entropy (H(0) = ln K);
4. the uniform state is the critical branching state (QG216 at μ=1);
5. the universal attractor erases initial data (fine-tuning unnecessary).

**Closes QG226 TOE criterion 6**: initial conditions **OPEN → DERIVED**. The
TOE score rises from 6.5/10 toward 7.0/10 (initial conditions now DERIVED;
only 4 partial + 1 open item remains: the information-content origin).
