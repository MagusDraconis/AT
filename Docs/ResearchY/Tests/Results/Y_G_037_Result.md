# Y_G_037 Result — Refractive Lens Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_037_Tests.cs`
**Status:** 6/6 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_037"`
**Group total:** G_001–G_037 = **289/289 PASSED**

## Verdict

**REFUTED** — a static refractive index cannot give light deflection without space curvature, because
**the index _is_ the PPN γ**. The rate-dependent term is second order and ≈10⁶ too small in the solar system,
and the EM-only reading is excluded by GW170817 at ≈10⁹. Computed.

## The identity

For a static metric `g₀₀ = −e^(2A)`, `g_ij = e^(2B)δ_ij`, light sees `n = e^(B−A)`. With `A = Φ/c²`,
`B = −γΦ/c²`:

```
n − 1 = −(1+γ)·Φ/c²        ⇒   a = 1 + γ   in   n = 1 + a·GM/(c²r)
Δθ    = 2a·GM/(c²b)        =   ((1+γ)/2)·4GM/(c²b)
```

For a **bound** object Φ < 0, so `n − 1 > 0`: the vacuum slows light. The suite asserts the sign explicitly
(`IndexMinusOneFromGamma(−2e−6, 1) = +4e−6`, `γ = 0 → +2e−6`, `γ = −1 → 0`).

| spatial rule | γ | n − 1 | deflection |
|---|---|---|---|
| **AT's derived conformal sector** (A = B) | **−1** | **0** (n = 1) | **0** |
| time-only | 0 | +1·x | 2GM/(c²b) — exactly half |
| GR | +1 | +2·x | 4GM/(c²b) |

**Closed form checked numerically:** `∫∂_⊥ln n dl` returns **4.000000** for a = 2 and **2.000000** for a = 1.

**A conformal metric cannot bend light at all** — `A = B ⟹ n = 1`. This is G_032's Cassini refutation
(8.6957×10⁴ σ) restated optically.

## The rate term is second order

`n − 1 = 2φ` is needed (**first order**); `λ_space·φ²|μ̇|` is **second order**:

| arena | φ | needed n − 1 | φ² (max) | shortfall |
|---|---|---|---|---|
| solar surface | 2.12e−6 | 4.24e−6 | 4.49e−12 | **9.43×10⁵** |
| white dwarf | 1e−4 | 2e−4 | 1e−8 | 2.0×10⁴ |
| neutron star J0740+6620 | 0.247002 | 0.494 | 0.061 | 8.1× |

Shortfall is asserted to be monotone decreasing in φ, and the first-order carrier is asserted to be the linear
term — which by the identity **is** the spatial metric function.

## The EM-only reading is excluded by GW170817

| strength (n − 1) | differential delay | vs 1.7 s |
|---|---|---|
| 1e−6 (galactic) | 4.1171×10⁹ s ≈ **130.5 yr** | **2.4×10⁹** |
| 2.12e−6 (solar) | 8.73×10⁹ s | 5.1×10⁹ |
| 3e−5 (cluster) | 1.24×10¹¹ s | 7.3×10¹⁰ |
| 1e−12 | 4.12×10³ s | 2.4×10³ |

Surviving strength: **n − 1 ≲ 4.13×10⁻¹⁶** — asserted to be more than **10⁹ times** smaller than the `2φ` a
deflection needs.

## Bending and delay are one function

`∫(n−1)dl/c` with `n − 1 = 2GM/(c²r)` returns the GR Shapiro delay `2GM/c³·ln(4r₁r₂/b²)`. A static index cannot
be tuned to fit one observable and not the other.

## The three escapes

| escape | constraint |
|---|---|
| dispersion `n(ω)` | lensing is achromatic to ~1 % from radio to optical |
| birefringence | vacuum-birefringence limits; polarizations must bend identically |
| **time dependence** | the TRM `φ²\|μ̇\|` signature — genuinely non-metric, but O(φ²), hence compact-object only |

## What survives from the TRM idea

1. **The optical language** — `n − 1 = −(1+γ)Φ/c²` is the cleanest statement of AT's actual problem, and it shows
   the problem is not notation.
2. **The rate-dependent term** as a genuinely non-metric, compact-object effect (within an order of magnitude at
   neutron-star compactness).
3. **A new falsification handle** — any EM-only lens must beat GW170817's 1.7 s bound, which no astrophysically
   natural strength does.

## Caveats

- φ is read as Φ/c² (the natural weak-field reading). If TRM's φ is an order-1 order parameter, the O(φ²)
  conclusion changes and the specific `f(κ,b)` needs the TRM definitions.
- TRM's `{β,γ} → {1,1}` at κ→0.3, b→1.248 is a **tuned→target** statement; whether `f(κ,b) → 1` is derived or
  fitted cannot be settled from the published formula alone.

## Registry

Added to the G_035 classification registry as `Spatial` → **BOUNDARY**, triaged `ScanDetectsIt: false` (the suite
names no `B`, `g_rr` or `GammaOf` — its subject is a coefficient). Counts become **25 / 10 / 3 of 38**; boundary
index still **23**; no prior classification changed. G_033's D96-free era count moved 14 → 15.
