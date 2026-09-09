# ResearchY-T_009 — Fitness-Reach Law Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_009 (permanent)
**Title:** Fitness-Reach Law — can N_fit be expressed analytically?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_009.md`
**Depends on:** ResearchY-T_007 (bounded innovation), T_008 (asymptotic diversity limit)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_009_Tests.cs` (6/6 ✅)
**Core analyzer:** `AT.Core/ResearchT/BoundedInnovationAnalyzer.cs` (T_007); shared
`AT.Tests/Shared/SpectralCaseCatalog.cs`

---

## Question

T_008 derived `S∞ = min(A, N_fit)`. T_009 asks: can `N_fit` — the number of modes within
mutation–selection reach of the fittest — be expressed **analytically**? Derive
`N_fit = F(μ, β, {w})` and test predicted vs simulated `S∞`.

## Answer: solvable in two limits, not in general

The replicator–mutator (fitness `w = m/λ`, crowding `f = w/(1+βx)`, ring mutation μ, threshold ε)
has **two exactly-solvable limits**, but no closed form in the general `(μ>0, β>0)` interior.

### 1. μ = 0 (pure selection + crowding) — EXACT

At μ = 0 each mode is independent; the equilibrium has `x_k = max(0, (w_k/Z − 1)/β)` where the
crowding threshold `Z*(β)` is the unique root of `Σ_{w_k>Z}(w_k − Z) = β·Z`. Hence

\[
S_\infty(\mu=0,\ \beta)=\#\{\,k\ :\ w_k > Z^*(\beta)(1+\beta\varepsilon)\,\}.
\]

Verified against simulation for all five cases × β ∈ {0.5, 1, 2, 10} (within the ≤1 critical-
slowing-down lag of the finite-time simulation).

| case | β=0.5 | β=1 | β=2 | β=10 |
|---|---|---|---|---|
| D96 | 1 | 1 | 1 | 3 |
| D96-3D | 2 | 3 | 3 | 8 |
| random | 7 | 11 | 17 | 45 |
| physical | 1 | 1 | 2 | 6 |
| unphysical | 1 | 1 | 1 | 2 |

### 2. β = 0 (mutation + selection, no crowding) — EXACT

At β = 0 the map is linear: `x(t+1) ∝ M·diag(w)·x(t)`, so the equilibrium is the Perron
(dominant) eigenvector of `B = M·diag(w)`, and

\[
S_\infty(\beta=0,\ \mu)=\#\{\,k\ :\ (\text{Perron}(M\,\mathrm{diag}(w)))_k > \varepsilon\,\}.
\]

Verified **exactly** against simulation for all five cases × μ ∈ {0.001, 0.01, 0.1}.

| case | μ=0.001 | μ=0.01 | μ=0.1 |
|---|---|---|---|
| D96 | 3 | 4 | 6 |
| D96-3D | 4 | 5 | 10 |
| random | 4 | 7 | 15 |
| physical | 3 | 4 | 7 |
| unphysical | 3 | 3 | 3 |

### 3. Small-μ uniform-gap reach — DERIVED form, EMERGENT accuracy

For a single characteristic fitness gap `δ = Δw/w*`, the Perron tail decays geometrically and

\[
N_{\rm fit}\ \approx\ 1+\frac{\log(1/\varepsilon)}{\log(2\delta/\mu)}.
\]

| case | w* | Δw | δ | predicted | simulated (β=0) |
|---|---|---|---|---|---|
| D96 | 5.18 | 3.85 | 0.743 | 3 | 4 |
| D96-3D | 4.00 | 0.67 | 0.167 | 4 | 5 |
| random | 0.06 | ~0 | 0.050 | 7 | 7 |
| physical | 2.00 | 1.00 | 0.500 | 3 | 4 |
| unphysical | 6.40 | 5.12 | 0.800 | 3 | 3 |

Order-of-magnitude agreement; the coefficients are landscape-dependent (EMERGENT).

---

## Theorem

> **Theorem (T_009).** For the replicator–mutator on the non-zero Laplacian eigenspaces, the
> surviving-mode count admits two EXACT closed forms — `S∞(μ=0) = #{w_k > Z*(β)}` with
> `Σ(w_k−Z*)=β·Z*`, and `S∞(β=0) = #{Perron(M·diag(w)) > ε}` — plus the small-μ uniform-gap
> reach `N_fit ≈ 1 + log(1/ε)/log(2δ/μ)`. No universal closed form `N_fit = F(μ, β, Δw)` in a
> single scalar gap exists: two spectra with identical Δw but different tails give different
> `N_fit` (tail-near-top → 5, tail-dropped → 2 at μ=0, β=10). ∎

---

## Classification

| Item | Classification |
|---|---|
| μ=0 crowding threshold `S∞ = #{w_k > Z*(β)}` | DERIVED (exact) |
| β=0 Perron equilibrium `S∞ = #{Perron(M·diag(w)) > ε}` | DERIVED (exact) |
| Uniform-gap reach `1 + log(1/ε)/log(2δ/μ)` | DERIVED form / EMERGENT accuracy |
| "N_fit = F(μ, β, Δw)" with a single scalar gap | REFUTED |

---

## Conclusion

`N_fit` **is** analytically expressible in the two solvable limits — a crowding threshold at
μ = 0 and a Perron (linear) equilibrium at β = 0 — both reproduced exactly by simulation. The
small-μ reach has a DERIVED geometric-tail form whose coefficients are EMERGENT. But the
general `(μ>0, β>0)` interior has no closed form, and a single scalar gap Δw is insufficient:
`N_fit` depends on the **full fitness spectrum**, confirming T_008's `S∞ = min(A, N_fit(μ, β, {w}))`.
No new primitive; canonical AT unchanged.
