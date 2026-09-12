# Y_G_006_Result.md — ResearchY-G_006 Suppression Mechanism Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_006_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED (~1 s) — group G total 49/49 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_006"`

## Summary

**Question:** What term suppresses large-density rearrangements?
**Answer:** The **relaxation (coarse-graining) operator** `RhoDynamics.DiffuseStep` — a **linear low-pass
filter** on the eigenspace-occupancy index. The decay is **exponential per mode** (exact eigenbasis); the
apparent power law is a **window illusion**; entropy is **downstream**; the branching flow is
**arrangement-neutral**.

**The 34× derived:** `1/r(200) = 33.78 = exp(200 × 0.0175991) = exp(3.5198 nats)` for the canonical D96
witness tilt at `d = 0.2`, `N = 96` — reproduced by the closed form to < 1e-9. It is DERIVED as a
closed form but EMERGENT as a number (2.27× at m = 1, 33.78× at m = 200, 65.13× at m = 1000).

## Detail

| measure | result |
|---------|--------|
| Spectrum | `μ_k = 1 − 2d(1 − cos(πk/N))`; every Neumann mode an **exact** eigenvector (one step ⇒ constant factor, verified < 1e-12). μ₁ = 0.999785834991 (1/e after 4669 steps, survives 200 steps at 0.958067); μ₄₈ = 0.6 exactly; μ₉₅ = 0.200214165009. Rate spread over 200 steps: **2.08e-140** |
| Closed form | `r(m) = sqrt(Σ_{k≥1} w_k²μ_k^{2m} / Σ_{k≥1} w_k²)`, `w = DCT-II(ρ)` — matches direct iteration to < 1e-9 at m = 1, 10, 50, 100, 200, 1000, 5000, 50 000 |
| Suppression factors | 2.27 (m=1) · 6.15 (10) · 16.79 (50) · 25.16 (100) · **33.78 (200)** · 48.78 (500) · 65.13 (1000) · 162.10 (5000) · 2.49e6 (50 000) |
| Exp vs power law | over 1…200: exponential R² **0.7579** vs power law R² **0.9945** (α = 0.5612) — the illusion is real; window rate 8.0787e-3 is **37.7×** the true rate |
| Asymptotic tail | fit over m = 5000…50 000: slope **−2.1418813131e-4** vs `ln μ₁` = **−2.1418794605e-4** (diff 1.85e-10, R² = 1.0000000000) — a single exponential, not a power law |
| Rate floor | 1.7599e-2 (200) → 4.1505e-4 (20 000) → 2.941e-4 (50 000) → **2.1419e-4 = |ln μ₁|** (converges, does not vanish) |
| Entropy | `|D(ax+by) − aDx − bDy| = 3.47e-18` (exactly linear ⇒ cannot read entropy); `H + (N/2)E → ln 96` with residual **9.7e-7** at m = 200 (H is slaved to the Dirichlet energy) |
| Branching | continuity ρ_{k+1} = μρ_k True; `a(λρ) = a(ρ)` to 5.56e-11 at λ = 10⁶; static at criticality; basin 1 for **every** size (universal across size) ⇒ contracts nothing |
| Deficit & field | count conserved (`Σρ = 1`) at every step; deficit L1 and max|deficit| decay monotonically; field `max|a|` 0.6032 → 1.8746e-3 after 200 relaxation steps (322× removal) |
| Cases at m = 200 | **D96 33.78** · **D96³ 7.33** (884 736 modes, A₀ = 20 812, free room 863 924) · **Random: witness class EMPTY** (A₀ = 96, every multiplicity 1, free room 0) · Random extremal alternation **55.13** |
| Structural | `μ_k` depends on **N only**: D96 and Random share the identical operator; the cube's `μ₁ = 0.99999999999748` (|ln μ₁| < 1e-11) never decays ⇒ **arrangement-selective, not lattice-selective** |

## Verdicts

* **DERIVED** — the relaxation operator as the suppressing term; geometric per-mode decay; the exact
  closed form; the 34× as `exp(200 · rate(200))`; the asymptotic single-exponential tail; linearity; the
  downstream entropy identity; arrangement-vs-lattice selectivity.
* **EMERGENT** — the numeric value 33.78 (horizon- and arrangement-specific) and the apparent power-law
  exponent (α = 0.5612 over 1…200; tail is exponential).
* **REFUTED** — power-law decay as the *law*; entropy-driven suppression; actualization/branching-driven
  suppression.

## Test results

| # | Test | Verdict |
|---|------|---------|
| 1 | `Y_G_006_ExponentialModeLaw` | ✅ PASS |
| 2 | `Y_G_006_ClosedFormContraction` | ✅ PASS |
| 3 | `Y_G_006_PowerLawIllusionRefuted` | ✅ PASS |
| 4 | `Y_G_006_ThreeLattices` | ✅ PASS |
| 5 | `Y_G_006_EntropyIsDownstream` | ✅ PASS |
| 6 | `Y_G_006_BranchingIsNeutral` | ✅ PASS |
| 7 | `Y_G_006_ContractionRateAndDeficit` | ✅ PASS |
| 8 | `Y_G_006_Verdicts` | ✅ PASS |
| 9 | `Y_G_006_Run` | ✅ PASS |

## Classification

**No reclassification.** The D_040 `ClassificationRegistry` is untouched; G_005's ACCESSIBLE /
SUPPRESSED / FORBIDDEN verdicts are unchanged and are *explained* here rather than revised; no canonical
claim, value or equation changes; no new primitive.

## Consequence

G_005's open mechanism is closed: the G_002 free directions are *within-multiplet* (high-k)
rearrangements — precisely what the coarse-graining filter removes fastest — while the smooth, scale-free
deficit that produces the observed galactic field sits on the slowest mode and survives
(`μ₁²⁰⁰ = 0.958`). **Suppression and observability are the two ends of the same filter.**
