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
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_037_Tests.cs` (6/6 PASSED)
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

## The rate-dependent term cannot supply the missing half

Reproducing the observed deflection needs `n − 1 = (1+γ)φ = 2φ` — **first order** in φ. The TRM formula's second
term, `λ_space·φ²·|μ̇|`, is **second order**. With `|μ̇| ~ 1`:

| arena | φ | needed n − 1 | φ² (max) | shortfall |
|---|---|---|---|---|
| **solar surface** | 2.12e−6 | 4.24e−6 | 4.49e−12 | **9.43×10⁵** |
| white dwarf | 1e−4 | 2e−4 | 1e−8 | 2.0×10⁴ |
| neutron star J0740+6620 | 0.247002 | 0.494 | 0.061 | 8.1× |

Only the **linear** φ term can carry the first-order half — and by the identity that term *is* the spatial metric
function. The φ² term is nevertheless genuinely interesting, because it depends on a **rate**, which no static
metric can: it is a real non-metric ingredient, but it can matter only where φ is O(1), i.e. at **compact
objects**.

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

## Classification and caveats

**Registry:** added to the G_035 classification registry as `Spatial` → **BOUNDARY** (it is a statement about γ,
the spatial block), triaged `ScanDetectsIt: false` — the suite names no `B`, `g_rr` or `GammaOf`, because its
subject is a *coefficient*, and its only metric vocabulary is the negative test that a conformal metric cannot
bend light. Registry counts become **25 / 10 / 3 of 38**; the boundary index is still **23**, and no prior
classification changed.

**Caveats.** (1) φ is read as the dimensionless potential Φ/c² — the natural reading for a weak-field PPN
statement. If TRM's φ is an order-1 order parameter instead, the O(φ²) conclusion changes and the specific
`f(κ,b)` would need the TRM definitions. (2) The TRM report that `{β,γ} → {1,1}` at κ→0.3, b→1.248 is a
**tuned→target** statement; whether `f(κ,b) → 1` is derived or fitted cannot be settled from the published
formula alone. (3) Deterministic: exact algebra and one numerical check.

**No reclassification.** G_021, G_029, G_030, G_031 and G_032 are unchanged inputs; the D_040 registry is
untouched; no canonical claim, value or equation changes; no new primitive.
