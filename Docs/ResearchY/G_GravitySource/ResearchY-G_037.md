# ResearchY-G_037 — Refractive Lens Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_037 (permanent)
**Title:** Refractive Lens Audit — can light bend without space bending?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_037.md`
**Depends on:** G_021 (light propagation), G_029/G_030 (the spatial sector closes on a postulate; no-go), G_031 (conformal flatness IS the counting measure), G_032 (imposing it is refuted — γ = −1, Cassini)
**Provenance:** the **TRM-era** programme, which reported a deflection matching GR from an effective index
`n_eff = 2 + λ_time·φ + λ_space·φ²·|μ̇|` with `c_eff = c₀/n_eff` and `{β,γ} → {1,1}` at κ→0.3, b→1.248
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_037_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/RefractiveLensAudit.cs`

## The question

An earlier version of the programme (TRM) obtained **light deflection without space bending**, by giving the
vacuum an effective refractive index and using `c_eff = c₀/n_eff`. If that worked, it would release AT from its
worst problem: the spatial sector **cannot be derived** (G_030) and closes only on the postulate
`g_rr = 2 − ρ^(2/d)` (G_029), while the sector AT *does* derive is conformal and therefore **bends nothing**
(G_032, refuted at 8.6957×10⁴ σ by Cassini).

So: can the refractive route replace the spatial postulate?

## The answer: REFUTED — by an identity, not an argument

> **The refractive index of a static vacuum *is* the PPN γ.**

For a static metric with `g₀₀ = −e^(2A)` and `g_ij = e^(2B)δ_ij`, light in the optical description sees
`n = e^(B−A)`. With `A = Φ/c²` and `B = −γΦ/c²`:

```
n − 1  =  −(1+γ)·Φ/c²          ⇒   a = 1 + γ   in   n = 1 + a·GM/(c²r)
Δθ     =  2a·GM/(c²b)          =   ((1+γ)/2)·4GM/(c²b)
```

**A static, non-dispersive index is not an alternative to space curvature — it is the same object in different
variables.** The coefficient that deflects the light *is* `1 + γ`.

| spatial rule | γ | n − 1 | deflection |
|---|---|---|---|
| **AT's DERIVED conformal sector** (A = B; G_031) | **−1** | **0** (n = 1) | **0** |
| time-only (no space term) | 0 | +1·x | 2GM/(c²b) — exactly half |
| GR | +1 | +2·x | 4GM/(c²b) |

The closed form is checked **numerically** in the suite: `∫∂_⊥ln n dl` returns 4.000000 for a = 2 and 2.000000 for
a = 1.

And AT's derived sector sits in the first row. This is not a new result — it is **G_032's Cassini refutation
restated optically**, and it has a one-line reason: a conformal factor maps null geodesics to null geodesics, so
`A = B ⟹ n = 1` and the ray is undeflected.

## The rate-dependent term cannot supply an independent spatial term

Reproducing the observed deflection needs `n − 1 = (1+γ)φ = 2φ` — **first order** in φ. The TRM formula's second
term, `λ_space·φ²·|μ̇|`, is **second order**. With `|μ̇| ~ 1`:

| arena | φ | needed n − 1 | φ² (max) | shortfall |
|---|---|---|---|---|
| **solar surface** | 2.12e−6 | 4.24e−6 | 4.49e−12 | **9.43×10⁵** |
| white dwarf | 1e−4 | 2e−4 | 1e−8 | 2.0×10⁴ |
| neutron star J0740+6620 | 0.247002 | 0.494 | 0.061 | 8.1× |

The first-order coefficient is the formula's **leading constant** (2), not `λ_time` — and by the identity that
constant *is* the spatial metric function (see the source check below). The φ² term is nevertheless genuinely
interesting, because it depends on a **rate**, which no static metric can: it is a real non-metric ingredient, but
it can matter only where φ is O(1), i.e. at **compact objects**.

## The source check — the formula already contains the spatial factor

`TRM.Core/Shared/PhotonTransportModel.cs` was still on disk, so the formula was read *in situ* rather than
inferred. That **falsified this audit's own first reading** and strengthened the verdict.

```csharp
double nEff = 2.0 + parameters.LambdaTime * phi + parameters.LambdaSpace * localMemory;
double ar   = -nEff * G * M / (r * r);           // the force multiplier is nEff, NOT lambda_time
double timeAccumDerivative = (nEff - 2.0) * v;   // ... but here the 2 is subtracted back
```

**`λ_time = 1` does not give half.** `n_eff` *includes* the leading `2.0`, so the coefficient the deflection sees
is `n_eff` itself: at the solar limb `n_eff = 2.000002120` → ratio 1.000001 → **full deflection**. An earlier
reading of this audit treated `λ_time` as the index coefficient; the code uses the whole `n_eff` as the force
multiplier. **Corrected.**

**And the file uses both conventions at once** — `(n_eff − 2)·v` for travel time (physical index is `n_eff − 2`,
⟹ half) and `n_eff·G·M/r²` for the acceleration (⟹ full). One function, two conventions, so the reported result
depends on which line is read.

**So the full deflection comes from a literal `2.0`.** `ar = −n_eff·GM/r²` with `n_eff ≈ 2` is the flat-space
"2× Newton" photon law. By this audit's identity (`a = 1 + γ`), a constant of 2 **is γ = 1** — the spatial factor is
already in the formula, hardcoded. **"No space bending" is therefore false: the space bending is typed in as a
constant**, and it is exactly AT's G_029 postulate in other variables, so it removes nothing.

At solar compactness every other term is ≲10⁻⁵ relative — `KBase = 2 + 2Aφ + 3Bφ²` with
`A = −0.1701452243330672`, `B = −8.484408441898648` moves the factor by −7.2×10⁻⁷ — so the leading 2 is the entire
first-order physics. The remaining coefficients are declared or fitted, not derived:

| symbol | value | source's own status |
|---|---|---|
| `LambdaTime` | 1.0 | hardcoded; **not** the index coefficient |
| `LambdaSpace` | 30.0 | summary says *"CALIBRATED (lambda terms)"* |
| `A` | −0.1701452243330672 | fitted to 16 digits |
| `B` | −8.484408441898648 | fitted to 16 digits |
| `Lambda` | 30.79445857638716 | fitted to 16 digits |
| `EulerBridgeScale` | 0.85 | *"17/20 … NOT a fundamental physical constant"* |

**TRM's own tests cannot support "matches GR".** They assert order-of-magnitude bands:

| test | ratio band | γ accepted |
|---|---|---|
| `EL03` | [0.70, 1.25] | [+0.40, +1.50] |
| `EL04` transport | [0.95, 1.08] | [+0.90, +1.16] |
| `EL04` Euler | [0.85, 1.25] | [+0.70, +1.50] |

Cassini pins γ to 1 ± 2.3×10⁻⁵; the widest accepted TRM window is 1.10 wide — **23,913× looser**. And every run is
`G = 1, c = 1, b = 1` at ε = 10⁻³…10⁻², i.e. **472× to 4,717× outside the solar regime**: solar-compactness
deflection was never tested.

**The rate channel is dead on its own terms.** Reaching first order needs
`|μ̇| = λ_time/(λ_space·φ) = 1.57×10⁴` at solar compactness; the code's own `ComputeAbsDmuDtBase` yields
`|μ̇| ~ O(1.8×10⁻⁶ … 0.43)` /s there → **shortfall ≥ 3.65×10⁴**. And `|μ̇|` is a new field: AT's temporal core is
`ρ → g₀₀ → clock` (G_035) with no such variable, and G_029/G_030's no-new-primitive rule forbids adding one.

**TRM's own documents already said so.** `docs/Archive/TRM_Geodesic_Derivation.md` separates the channels into
`λ_t∇φ` ("the local **time-rate gradient**") and `λ_s|μ̇|∇(φ²)`, calls the latter *"a natural **candidate** for the
missing spatial / **curvature-like** contribution"*, states that the Euler–Lagrange track does *"not close the full
formal derivation chain yet"*, and lists *"λ_s can be derived from a dimensionless coupling structure"* as the
**next open theoretical step** — with `20/17` itself an "Open Derivation Task" (§12). The published
`V3_4/main.tex` nonclaims read *"No GR replacement is claimed"* and *"Any mapping to gravitational or cosmological
observables requires a physical frequency scale not currently provided."* The slide reflects the **transport
subsystem** (TRM34–41, TRM78), which the paper chose not to claim.

## The dichotomy — and its observational kill

If the index is really a **medium** rather than a metric, it affects **light but not gravitational waves**. That
is testable. Over GW170817/GRB170817A's 40 Mpc (≈130 million years of light-travel), an EM-only index delays
light against gravitons by:

| strength (n − 1) | differential delay | vs the 1.7 s bound |
|---|---|---|
| 1e−6 (galactic scale) | 4.12×10⁹ s ≈ **131 yr** | **2.4×10⁹** |
| 2.12e−6 (solar scale) | 8.73×10⁹ s ≈ 277 yr | 5.1×10⁹ |
| 3e−5 (cluster scale) | 1.24×10¹¹ s | 7.3×10¹⁰ |
| 1e−12 | 4.12×10³ s | 2.4×10³ |

The surviving strength is **n − 1 ≲ 1.7·c/D = 4.13×10⁻¹⁶** — nothing like the `2φ` a deflection requires. **The
EM-only reading is excluded by about nine orders of magnitude.**

## Bending and delay are not independently tunable

Any static index that reproduces the bending also reproduces the **Shapiro delay**:
`∫(n−1)dl/c` with `n − 1 = 2GM/(c²r)` returns exactly `2GM/c³·ln(4r₁r₂/b²)`, the GR value. Bending and delay come
from **one function**, so a static index cannot be tuned to fit one and not the other.

## The three genuine escapes — and only these

A medium is *not* a metric in exactly three ways, and each is an observational commitment rather than a free
choice:

| escape | why it is not a metric | constraint |
|---|---|---|
| **dispersion** (`n(ω)`) | a frequency-dependent index has no static metric form | lensing is observed achromatic to ~1 % from radio to optical |
| **birefringence** (`n` depends on polarization) | polarization-dependent propagation is not a metric | vacuum-birefringence limits; the two polarizations must bend identically to high precision |
| **time dependence** (`n` varies during the crossing) | a rate-dependent index is exactly the φ²\|μ̇\| signature | this is the ONE genuinely non-metric ingredient in the TRM formula — but O(φ²), hence a compact-object effect |

## What survives from the TRM idea

The audit is a refutation, but it is not a dismissal. Three things carry forward:

1. **The optical language is the cleanest statement of AT's actual problem.** `n − 1 = −(1+γ)Φ/c²` shows at a
   glance that the problem is *not* notation: any static mechanism that bends light has already chosen γ, and
   AT's derived sector chose −1.
2. **The rate-dependent term is a genuine non-metric ingredient.** `φ²|μ̇|` cannot be written as a static metric,
   so it is new physics — bounded to compact objects, where it is within an order of magnitude.
3. **A new falsification handle.** Any "medium" lens must beat GW170817's 1.7 s light-vs-graviton bound, and no
   astrophysically natural strength does.

## Output

**REFUTED** — a static refractive index cannot release AT from γ, because it *is* γ; the rate-dependent term is
≈10⁶ too small in the solar system; and the EM-only reading is excluded at ≈10⁹. Verdict computed.

**Reading the TRM source strengthens this rather than weakening it.** The only ingredient that ever produced the
observed deflection is the literal constant `2` — i.e. **γ = 1**, the very quantity G_030 proved cannot be
derived. TRM's "no space bending" was space bending entered as a constant.

## Classification and caveats

**Registry:** added to the G_035 classification registry as `Spatial` → **BOUNDARY** (it is a statement about γ,
the spatial block), triaged `ScanDetectsIt: false` — the suite names no `B`, `g_rr` or `GammaOf`, because its
subject is a *coefficient*, and its only metric vocabulary is the negative test that a conformal metric cannot
bend light. Registry counts become **25 / 10 / 3 of 38**; the boundary index is still **23**, and no prior
classification changed.

**Caveats — BOTH NOW CLOSED by the source check.**
(1) **`φ` is the dimensionless potential.** `PhotonTransportModel.Phi(G,M,c,r) = G·M/(c·c·r)` is the canonical TRM
definition, so the `Φ/c²` reading is correct and the O(φ²) conclusion stands. *(Closed.)*
(2) **`f(κ,b) → 1` was fitted, not derived.** The deflection path multiplies by `EulerBridgeScale = 0.85`, which
the code annotates as the reciprocal of the mid-band rational `20/17` and *"NOT a fundamental physical constant"*,
and uses to make the Euler/Fermat branch *"comparable to the established transport RK4 branch"* — a
self-consistency calibration between two code paths. The λ's are declared **CALIBRATED** by the file's own summary,
and `A`, `B`, `Lambda` are fitted to 16 digits. *(Closed.)*
(3) Deterministic: exact algebra, one numerical check, and invariant-culture formatting.

**No reclassification.** G_021, G_029, G_030, G_031 and G_032 are unchanged inputs; the D_040 registry is
untouched; no canonical claim, value or equation changes; no new primitive.
