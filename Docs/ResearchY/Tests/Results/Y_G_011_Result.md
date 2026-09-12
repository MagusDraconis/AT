# Y_G_011_Result.md — ResearchY-G_011 Rho Actuator Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_011_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED (~0.1 s) — group G total 98/98 PASSED (with G_011b)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_011"`

## Summary

**Question:** Can any physical quantity change ρ? (candidates: energy density, phase coherence, spectral
organization, degeneracy engineering, attractor compression, synchronization, information density)
**Answer:** **No candidate is an actuator.** Five are *functions of ρ* and two are *independent but exactly
ρ-inert*. ρ is nevertheless **fully actuable** — `s = (I − W)ρ*` is invertible with finite gains everywhere —
so the vacancy is a **source**, not a handle.

## Detail — the four measured deltas

| candidate | Δρ (L1) | Δa_AT | ΔΦ/c² | Δτ/τ [s/day] | function of ρ? | survives W? | drive/step | **label** |
|-----------|---------|-------|-------|--------------|----------------|-------------|------------|-----------|
| energy density (fixed E) | 0.6666667 | 0.603175 | 0.201058 | 17 371.4 | **yes** (E = ⟨λ,ρ⟩) | 33.78× erased | 0.48675 | **CORRELATED** |
| spectral organization | 0.6666667 | 0.603175 | 0.201058 | 17 371.4 | **yes** (DCT bijection) | 33.78× erased | 0.48675 | **CORRELATED** |
| degeneracy engineering | 0.6666667 | 0.603175 | 0.201058 | 17 371.4 | **yes** (free part of ρ) | 33.78× erased | 0.48675 | **CORRELATED** |
| attractor compression (k=48) | 2.8669638 | 0.032121 | 0.010707 | 925.1 | **yes** (a map) | 1.12× (smooth) | 0.0336912 | **REFUTED** |
| information density | 0.6583333 | 1.0031746 | — | — | **yes** (global) | invariant | 0 | **REFUTED** |
| phase coherence | **0** (2.5e-16) | **0** | **0** | **0** | **no** | n/a | 0 | **REFUTED** |
| synchronization | **0** | **0** | **0** | **0** | **no** | n/a | 0 | **REFUTED** |
| imported source s | drives any ρ* | — | — | — | external | held exactly | — | **ACTUATOR (imported)** |

| measure | result |
|---------|--------|
| arena | D96: 45 eigenspaces, 96 cells, free room `Σ(m−1) = 51 = D_048's L`; simplex dim 95, fixed-E fibre 94 |
| energy identity | `E = 12.000000000000` for both canonical and tilt (Δ to **1e-12**); every eigenspace total equal to 12 digits; ρ still moves `L1 = 0.6666667` and the field goes `0 → 0.603175` |
| energy is not injective | fibre ⊇ the whole 51-dim degeneracy space; re-ordering the same multiset moves `E` by **1.5401766 (12.8 %)** |
| density-space contrast | tilt vs uniform: `ln 4.8 = 1.5686159` → `ΔΦ/c² = 0.5228720` → **45 176.1 s/day** |
| phase sector | `L1(ρ,\|ψ\|²) ≤ 2.5e-16` and `max\|Δa\| < 1e-9` for canonical / global-shift / mirror / locked, while `\|Σψ\|` runs **0.0324196 → 9.1171822 (281.22×)** |
| synchronization | canonical grid is maximally incoherent: Kuramoto `r = 8.0788382e-17 → 1.0` on locking, ρ untouched; interference still 4 → 2 → 0 |
| spectral coordinates | `\|CCᵀ − I\| = 1.37e-14`, round trip **4.2e-16**, DC = Σρ = 1 (fixed) → **95 free coordinates**; dominant mode **k = 94**, `k ≥ 48` share **0.7965733** |
| operator spectrum | fixed by `(d,N)`: `μ_48 = 0.6` exactly, `1 − μ_1 = 2.141650094e-4`, `1 − μ_95 = 0.799785835` |
| compression | `Δa = 0.032121` survives 200 steps essentially unchanged (base std contracts only **1.1207×**), unlike the witness's **33.78×** |
| information | `KL(uniform) = 0`, `KL(tilt) = 0.2725652026 = ln 96 − H`; exactly permutation- and reversal-invariant (`ΔKL = 0`) while `L1 = 0.6583333` and `max\|Δa\| = 1.0031746`; monotone flow `0.27257 → 1.0873e-3 → 2.6946e-4` |
| actuator map | `s = (I − W)ρ*`, `Σs = 2.1e-17`, `‖s‖₁ = 0.48675` (witness), `max\|s\| = 0.01866667`; gains **4669.296831218 / 2.500000000 / 1.250334722**; inverse reconstruction **1.9e-15**; driven recursion converges **< 1e-12**; uniform state `‖s‖₁ = 0` (unique undriven fixed point) |

## Verdicts

* **ACTUATOR** — none of the seven candidates. The one handle that changes ρ is the imported, count-neutral,
  unique occupancy source `s = (I − W)ρ*` (the drive G_008/G_010 priced and nothing supplies).
* **CORRELATED** — energy density (an exact linear re-expression, non-injective), spectral organization
  (orthonormal coordinates of ρ), degeneracy engineering (the free part of ρ itself).
* **REFUTED** — phase coherence and synchronization (the only candidates independent of ρ, and exactly
  ρ-inert), attractor compression (a map, not a quantity), information density (global,
  permutation-invariant).

## Classification and caveats

**No reclassification.** D_040 untouched. G_002's `CONTROLLABLE` verdicts concern the **operations** on ρ and
stand unchanged; the G_011 labels concern whether the **quantity** is an actuator. G_001's `SOURCE = ρ` is
confirmed from the control side. No canonical claim, value or equation changes; no new primitive.
Deterministic, no randomness.

* The four-delta table uses the series convention `ΔΦ/c² = Δa_AT/d` (G_003/G_009), so the rows match G_009's
  clock table exactly (0.201058 → 17 371.4 s/day).
* Re-measured detail: the witness's high-k share at `k ≥ 48` is **0.7965733** (G_008's report line quoted
  78.2 % for the same object; no verdict depends on it).

## Open problems (OP1–OP4)

1. Can any external agent be mode-matched to the AT occupancy index?
2. Is a non-Hermitian (antisymmetric) coupling allowed in an open system (NP_174 closes it canonically)?
3. Does any other AT observable couple to the ψ-sector's 281× coherent sum?
4. Is there an entropy argument forcing the 51 free directions into equiprobability?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_011.md`
* `AT.Tests/Shared/RhoActuators.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
