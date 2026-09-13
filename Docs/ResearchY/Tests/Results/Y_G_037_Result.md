# Y_G_037 Result — Refractive Lens Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_037_Tests.cs`
**Status:** 7/7 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_037"`
**Group total:** G_001–G_038 = **297/297 PASSED**

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

Shortfall is asserted to be monotone decreasing in φ, and the first-order coefficient is asserted to be the
formula's **leading constant (2)**, not `λ_time` — which by the identity **is** the spatial metric function.

## The source check — and a correction to this audit's own first reading

`TRM.Core/Shared/PhotonTransportModel.cs` was still on disk, so the formula was read *in situ* rather than
inferred. That **falsified the first estimate** and strengthened the verdict.

```csharp
double nEff = 2.0 + parameters.LambdaTime * phi + parameters.LambdaSpace * localMemory;
double ar   = -nEff * G * M / (r * r);           // the force multiplier is nEff, NOT lambda_time
double timeAccumDerivative = (nEff - 2.0) * v;   // ... but here the 2 is subtracted back
```

| finding | measured |
|---|---|
| `n_eff` at the solar limb (with the leading 2) | **2.000002120** → ratio **1.000001** → **FULL**, not half |
| the same quantity without the leading 2 (`(n_eff−2)·v`) | **2.12e−6** → **HALF** |
| γ implied by the leading constant (`a = 1 + γ`) | **+1** |
| `KBase = 2 + 2Aφ + 3Bφ²` contribution at solar φ | **−7.2e−7** (negligible) |

**`λ_time = 1` does not give half.** The code accelerates by `ar = −n_eff·G·M/(r·r)`, and `n_eff` *includes* the
leading 2, so the coefficient the deflection sees is `n_eff` itself. **And the file uses both conventions at
once** — `(n_eff − 2)·v` for travel time, `n_eff·G·M/r²` for the acceleration — so the result depends on which
line is read. The bending is full **only because a hardcoded 2 supplies γ = 1**, i.e. AT's G_029 postulate in other
variables: **"no space bending" is false.**

| symbol | value | source's own status |
|---|---|---|
| `LambdaTime` | 1.0 | hardcoded; **not** the index coefficient |
| `LambdaSpace` | 30.0 | summary says *"CALIBRATED (lambda terms)"* |
| `A` | −0.1701452243330672 | fitted to 16 digits |
| `B` | −8.484408441898648 | fitted to 16 digits |
| `Lambda` | 30.79445857638716 | fitted to 16 digits |
| `EulerBridgeScale` | 0.85 | *"17/20 … NOT a fundamental physical constant"* |

**TRM's own tests cannot support "matches GR":**

| test | ratio band | γ accepted |
|---|---|---|
| `EL03` | [0.70, 1.25] | [+0.40, +1.50] |
| `EL04` transport | [0.95, 1.08] | [+0.90, +1.16] |
| `EL04` Euler | [0.85, 1.25] | [+0.70, +1.50] |

Cassini pins γ to 1 ± 2.3×10⁻⁵; the widest accepted TRM window is **23,913× looser**, and every run is
`G = 1, c = 1, b = 1` at ε = 10⁻³…10⁻² — **472× to 4,717× outside the solar regime**. Solar-compactness
deflection was never tested.

**The rate channel is dead on its own terms:** first order needs `|μ̇| = 1.57×10⁴` at solar compactness; the
code's own `ComputeAbsDmuDtBase` yields `|μ̇| ~ O(1.83×10⁻⁶ … 0.431)` /s there → **shortfall 3.65×10⁴**. And
`|μ̇|` is a new primitive AT does not have (G_035's core is ρ → g₀₀ → clock).

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

## Caveats — both now CLOSED by the source check

- **`φ` is the dimensionless potential.** `PhotonTransportModel.Phi(G,M,c,r) = G·M/(c·c·r)` is the canonical TRM
  definition, so the `Φ/c²` reading is correct and the O(φ²) conclusion stands. *(Closed.)*
- **`f(κ,b) → 1` was fitted, not derived.** The deflection path multiplies by `EulerBridgeScale = 0.85`, which the
  code annotates as the reciprocal of the mid-band rational `20/17` and *"NOT a fundamental physical constant"*,
  and uses to make the Euler/Fermat branch *"comparable to the established transport RK4 branch"* — a
  self-consistency calibration between two code paths. The λ's are declared **CALIBRATED** by the file's own
  summary, and `A`, `B`, `Lambda` are fitted to 16 digits. *(Closed.)*
- Deterministic: exact algebra, one numerical check, and invariant-culture formatting.

## Registry

Added to the G_035 classification registry as `Spatial` → **BOUNDARY**, triaged `ScanDetectsIt: false` (the suite
names no `B`, `g_rr` or `GammaOf` — its subject is a coefficient). Counts become **25 / 10 / 3 of 38**; boundary
index still **23**; no prior classification changed. G_033's D96-free era count moved 14 → 15.
