# Y_G_021_Result.md — ResearchY-G_021 Light-Propagation Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_021_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.03 s) — group G total 182/182 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_021"`

## Summary

**Question:** G_019 claimed AT *"supplies no spatial metric, so no light bending, Shapiro delay or shadow size
follows from it"*. Is that true?

**Answer: NO — and the correction inverts the conclusion.** AT **does** supply the full metric; it is
**conformally flat**, `g = ρ^(2/d)η`, so `g₀₀ = −ρ^(2/d)` **and** `g_rr = +ρ^(2/d)`. Lensing *is* derivable, and
the derived answer is **zero**: `γ = −1` exactly, so deflection, `κ`, shear and the Shapiro delay all vanish
while the redshift survives. That is a **derived falsification, not a missing derivation** — excluded by
**Cassini at 8.6957e4 σ**.

## The framework

```
AT metric      g_uv = ρ^(2/d) η_uv          g_rr = +ρ^(2/d) = −g₀₀
AT optics      σ = (1/d) ln ρ ,  Φ = σ     g_rr = 1 − 2γΦ    ->    γ = −1   EXACTLY
lensing factor (1 + γ)/2 = 0               ->    deflection = κ = shear = μ−1 = Shapiro = 0
ψ completion   g₀₀ = −ρ^(2/d) e^(2ψ) ,  g_ii = ρ^(2/d) e^(−2ψ/(d−1))
               γ(ψ) = −[σ − ψ/(d−1)]/(σ + ψ)          γ = +1  <=>  ψ = −4σ  (d = 3)
clock with ψ   dτ/dt = ρ^(1/d) e^(ψ)      e^ψ = 1 + ψ + …  ->  FIRST order (vs G_019's x²)
```

## Detail — the exclusion

| measurement | published `γ` | separation from `γ = −1` |
|---|---:|---:|
| Cassini (Bertotti/Iess/Tortora 2003) | `1.0000210 ± 2.3e−5` | **8.6957e4 σ** |
| VLBA (Fomalont 2009) | `0.9998000 ± 3.0e−4` | **6.6660e3 σ** |
| Gaia (2022) | `0.9970000 ± 1.6e−2` | **1.2481e2 σ** |

## Detail — the escape and its price

`γ = +1` requires `ψ = −2σ(d−1)/(d−2)`: `−4σ` in `d = 3`, `−3σ` in `d = 4`, **no solution** in `d = 2`
(consistent with `G_uv ≡ 0`, QG180). Along that direction the exact `γ` is `e^(6σ)` — `+1` to first order.

**The price:** the acceleration is fixed by `g₀₀` alone, so `Φ = σ + ψ = −3σ` — which **contradicts**
`a = −(1/d)∇ln ρ` (the identification behind G_004's 0.99600). Restoring `γ = +1` therefore **changes the
sourcing relation**; `ψ` is a minimal new primitive (QG24), not a free optics patch (QG43: a 1-d.o.f. scalar
suffices for `γ`).

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the conformal spatial metric `g_ij = ρ^(2/d)δ_ij` (class × factor closed) · `γ = −1` exactly · all lensing observables and Shapiro delay zero with the redshift surviving · `γ = +1 ⇔ ψ = −4σ` in `d = 3` |
| **BOUNDARY** | the conformal class (imported Malament 1977) · the `ψ` sector (new primitive, QG24) · the `Φ = −3σ` conflict |
| **REFUTED** | the `ψ = 0` conformal optics — 8.6957e4 σ (Cassini), 6.6660e3 σ (VLBA), 1.2481e2 σ (Gaia) |

## Why the G-chain is unaffected

Every audit G_001–G_020 used `g₀₀` only (source law, clock law, 0.99600, 0.99668, `x²` signature, `z_NICER`).
Those results stand; the chain is **silent** on optics, not wrong about them.

**One recorded consequence:** with `ψ ≠ 0` the clock law is `dτ/dt = ρ^(1/d)e^(ψ)`, and `e^ψ` is **first
order** — at `ψ = 1e−3` that is **1000.5×** G_019's `x²` signature. So any nonzero `ψ` swamps the second-order
test the G-chain proposes; the G-chain's clock results are the `ψ = 0` limit.

## Classification and caveats

**No reclassification.** G_019's numbers stand (they are `g₀₀`-only); D_040 untouched; no canonical claim,
value or equation changes; no new primitive introduced by this audit. Deterministic: exact algebra on imported
published values.

* `γ = −1` rests on the conformal structure plus the `Φ = σ` identification — and that identification is what
  makes G_004 work.
* `ψ = −4σ` is exact to first order, verified exactly along the required direction; the residual is second
  order, below the order at which PPN `γ` is defined.

## Open problems (OP1–OP5)

1. **Is `ψ` physical?** The required value is derived exactly: `ψ = −(4/3)ln ρ` in `d = 3`.
2. **Can `ψ` be sourced without breaking `a = −(1/d)∇ln ρ`?** `Φ = −3σ` says no — a new source law is needed.
3. **What bound does the clock work set on `ψ`?** Never computed; G_019/G_020 assumed `ψ = 0`.
4. Is the conformal-class import irreducible, or can the class be derived natively?
5. Does the horizon corollary survive the `ψ` completion (`g₀₀ = −ρ^(2/d)e^(2ψ)` — does it still never vanish)?

## References

* Bertotti, Iess & Tortora, *Nature* **425**, 374 (2003) — Cassini Shapiro delay, `γ − 1 = (2.1 ± 2.3)e−5`.
* Fomalont et al. (2009) — VLBA solar deflection, `γ = 0.9998 ± 0.0003`.
* Gaia Collaboration (2022) — solar deflection, `γ = 0.997 ± 0.016`.
* Malament (1977); Hawking, King & McCarthy (1976) — causal order → conformal class.
* `Docs/Audits/MetricOriginClosure.md` — the metric-origin ledger (conformal class imported; factor native).
* `Docs/Research/ATQG_ConformalOpticsResolution.md` (QG212), `ATQG_NonTensorLensing.md` (QG26),
  `ATQG_GravityConsolidation.md` (QG24, QG43, QG44, QG180, QG186, QG207).
