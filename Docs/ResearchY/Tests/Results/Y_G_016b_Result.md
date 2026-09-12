# Y_G_016b_Result.md — ResearchY-G_016b Mass Independence Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_016b_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.07 s) — group G total 147/147 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_016b"`

## Summary

**Question:** can two states have the same energy but different ρ, or the same ρ but different energy?
(measure `ΔE`, `Δρ`, `Δτ`; output INDEPENDENT / DEPENDENT / REFUTED)
**Answer:** one label per direction, and they are **not symmetric**:

| direction | label |
|---|---|
| same `E`, **different** `ρ` | **INDEPENDENT** — 94-dim kernel = **51** degeneracy + **43** λ-mixing |
| `E` given `ρ` | **DEPENDENT** — `E` is a **function** of ρ (one ρ, one E) |
| **same** `ρ`, different `E` | **REFUTED** — only the BOUNDARY capacity K moves it |

## Detail — INDEPENDENT (same E, different ρ)

The 51-dimensional **degeneracy** room (λ constant inside a multiplet ⟹ `ΔE = 0` **exactly**):

| measure | witness tilt | pairwise δ = 0.002 | pairwise δ = 0.005 |
|---|---|---|---|
| `ΔE` | **0** | −1.776e-15 | −1.776e-15 |
| `Δρ = L1` | **0.6666666666666667** | 0.004 | 0.010 |
| contrast | **20 : 1** | 1.4752 | 2.8462 |
| `Δτ/τ` | **0.9985774245179969** | 0.129609 | 0.348656 |
| s/day | **86 277.089** | — | — |

The 43-dimensional **λ-mixing** room (zero net over *distinct* λ), cells 94/92/90 with
λ = 15.837372467014836, 15.790176186632262, 15.414213562373096:

```
v = (1, -1.1255345008711273, 0.12553450087112727)    Σv = 0    ⟨λ,v⟩ = 0   EXACTLY
```

At scale 0.004: `ΔE = −1.776e-15`, `Δρ = 0.009004`, `ρ_min = 0.005915`, contrast **2.437501**,
`Δτ/τ = 0.29699105` = **25 660.0263 s/day**.

## Detail — DEPENDENT

`E = ⟨λ,ρ⟩` is single-valued in ρ: one ρ gives one E on every evaluation. The independence is strictly
**one-directional** — many ρ per E, never many E per ρ.

## Detail — REFUTED (same ρ, different E)

The **same** uniform ρ = 1/96 under six couplings:

| K | `A₀` | `Σλ` | `E(uniform)` |
|---|---|---|---|
| 1 | 49 | 192 | **2.0** |
| 2 | 47 | 384 | **4.0** |
| 3 | 45 | 576 | **6.0** |
| 4 | 47 | 768 | **8.0** |
| 5 | 45 | 960 | **10.0** |
| 6 | 45 | **1152** | **12.0** |

`Σλ = 192K` ⟹ **`E = 2K` exactly**, with `L1(ρ_K, ρ_6) = 0`: the only handle is K, which is **BOUNDARY**
(G_007).

## Detail — the phase sector

Four phase assignments: `L1(|ψ|², ρ) < 2.5e-16`, `ΔE = 0`, `Δτ = 0` — while
`|Σψ| : 0.03241962809423954 → 9.117182187865382`, ratio **281.22414487183346** (exactly G_011/G_014).

## Detail — the fixed-E family: Δτ is UNBOUNDED

```
ΔE = 0 ,   contrast = 5f/(1−f) ,   L1(f) = (2/96)[42|2f−1|+|5f−1|+|6f−1|] ,   f = 1/(1+5e^{−3T})
```

| f | `Δρ = L1` | contrast | `Δτ/τ` |
|---|---|---|---|
| 0.2 | 0.5291666667 | 4.000 | 0.462098 |
| 0.5 | 0.0729166667 | 5.000 | 0.536479 |
| **0.8** | **0.6666666667** | **20.000** | **0.998577** |
| 0.9 | 0.8645833333 | 45.000 | 1.268888 |
| 0.99 | 1.0427083333 | 495.000 | 2.068186 |
| 0.999 | 1.0605208333 | 4 995.000 | 2.838731 |
| `f → 1⁻` | **→ 1.0625** | **→ ∞** | **→ ∞** |

`f = 1/(1+5e^{−3T})` at `T = (1/d)ln20` returns **f = 0.8** — the canonical `TiltFractions` *is* the 20 : 1
solution. Since `L1 → 1.0625` and `ρ_min → 0` at `ΔE = 0`, **no target Δτ is forbidden by energy
conservation**; only positivity and the G_005 band bound it (`T = 4.8867e-6` needs `f = 0.1666687028016166`,
contrast 1.0000146602074598).

## Classification and caveats

**No reclassification.** G_016/G_002/G_005/G_009/G_011/G_014 unchanged inputs. D_040 untouched; no canonical
claim, value or equation changes; no new primitive. Deterministic, no randomness.

* `ΔE = 0` is exact in the algebra; tests assert it at 1e-12/1e-13 (examples show ≤ 1.776e-15 = rounding).
* The λ-mixing direction is scale-free; 0.004 is chosen only to keep ρ ≥ 0.
* `contrast = 5f/(1−f)` holds for `f ≥ 1/2`; the `L1(f)` law is exact across the range.
* The unboundedness is about **energy conservation only** — G_005/G_008 bound the realised effect.
* **Independence is not symmetry**: ρ is independent *of* E while E is fully determined *by* ρ.

## Open problems (OP1–OP5)

1. The **diameter** of the fixed-E fibre (family gives 1.0625; simplex bound 2)?
2. Can the 43-dim λ-mixing room be measured apart from the 51-dim degeneracy room?
3. Is the factor 5 a D96 artefact of m = 6 (does it generalise to random / D96³)?
4. Any AT-native quantity that is *not* a function of ρ (a second state argument)?
5. Does the unbounded fixed-E family change G_010's drive accounting (priced per Δlnρ, not per `L1`)?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_016b.md`
* `AT.Tests/Shared/DensityField.cs`; `AT.Book/Services/Calculations/SpectrumService.cs`
