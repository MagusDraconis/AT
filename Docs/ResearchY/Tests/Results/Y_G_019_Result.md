# Y_G_019_Result.md — ResearchY-G_019 Second-Order Signature Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_019_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.03 s) — group G total 168/168 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_019"`

## Summary

**G_018 OP1 asked for the audit the group had never produced: one that imports measured constants and still
survives observation.** This is it, and the answer is **yes — AT survives**, with the deciding measurement
specified exactly.

**The signature.** AT's `dτ/dt = ρ^(1/d)` gives `e^σ` where GR gives `√(1 + 2σ)`, `σ = Φ/c²`:
first order **identical**, second order **opposite sign** (+½ vs −½), so

```
AT/GR = e^x / √(1 + 2x) = 1 + x² − (4/3)x³ + …          x = Φ/c² < 0 for a bound object
```

**The discriminator is x²** — which decides the whole audit.

## Detail — the weak-field no-go (REFUTED route)

| case | `x` | signature `x²` | precision | short by |
|---|---|---|---|---|
| Earth's surface | −6.961275e-10 | **4.845934e-19** | 1e-18 clock floor | **2.1×** |
| ground vs GPS (differential) | — | **4.567944e-19** | 1e-18 | 2.2× |
| Sun's surface | −2.122503e-6 | 4.505017e-12 | 1e-5 | **2.2e6×** |
| Sirius B | −2.572878e-4 | 6.619702e-8 | 0.02 (HST 2 %) | **3.3e6×** |

Series verified: `(ratio − 1)/x²` = 1.0000889006 (−1e-6), 1.0001333539 (−1e-4), 1.0135879522 (−1e-2),
1.2774576758 (−0.15). Sign: AT clocks **faster** (`e^{−0.15} = 0.8607079764 > √0.7 = 0.8366600265`), so AT's
redshift is **always smaller** (`z_AT = 0.1618342427` vs `z_GR = 0.1952286093` at x = −0.15). At `x = 1e-9` the
true difference (1e-18) is **below one ulp of 1.0** (1.11e-16): at solar-system depths the signature is
*unrepresentable*.

## Detail — the live arena (neutron stars)

| object | `x` | rate AT/GR − 1 | `z_AT` | `z_GR` | `Δz/z` |
|---|---|---|---|---|---|
| J0030+0451 (NICER) | −0.152011 | **+0.029638** | 0.1641732 | 0.1986775 | **−17.367 %** |
| J0740+6620 (Riley) | −0.247002 | **+0.098133** | 0.2801814 | 0.4058088 | **−30.957 %** |
| J0740+6620 (Miller) | −0.224246 | **+0.076057** | 0.2513786 | 0.3465546 | **−27.463 %** |

Absolute gap in `z` for J0740+6620: **0.1256**.

## Detail — the inverse map (M = 1.4 M☉)

`x_GR = (1 − (1+z)^(−2))/2`, `x_AT = ln(1+z)`, `R = GM/(x c²)`:

| z | `R_GR` | `R_AT` | `ΔR/R` |
|---|---|---|---|
| 0.15 | 16.959 km | 14.795 km | −12.76 % |
| 0.20 | 13.535 km | 11.342 km | −16.20 % |
| 0.25 | 11.488 km | 9.267 km | −19.33 % |
| 0.30 | 10.129 km | 7.881 km | −22.19 % |
| **0.35** | 9.164 km | **6.890 km** | **−24.81 %** |
| 0.40 | 8.444 km | 6.146 km | −27.22 % |

A NICER-compatible object (R ≈ 11–14 km) **cannot** have z = 0.35 under AT.

## Detail — the deciding precision (AT SURVIVES)

M = 1.4 ± 0.05 M☉, R = 12 ± 1 km → `x = −0.172317 ± 0.020514` (11.90 %); `z_AT = 0.188055 ± 0.024372`;
`z_GR = 0.235259 ± 0.038665`; separation **0.047205**; combined σ **0.045706** → **1.033 σ**.

3σ needs `σ_z ≤ 0.015735` = **8.37 % of `z_AT`** (6.69 % of `z_GR`). Current NS redshift determinations are
20–50 % relative → **short by 2.4× to 6.0×**.

## Detail — the horizon corollary and the g₀₀-only scope

| y | AT `g₀₀ = −e^(−2y)` | `z_AT` | GR `1 − 2y` | `z_GR` |
|---|---|---|---|---|
| 0.25 | −0.60653066 | 0.2840254 | +0.500000 | 0.4142136 |
| 0.4999 | −0.36795302 | 0.6485564 | +0.000200 | **69.71068** |
| **0.50** | **−0.36787944** | **0.6487213** | **0.000000** | **DIVERGES** |
| 0.60 / 1.0 / 5.0 | finite | 0.8221188 / 1.718282 / 147.4132 | negative | **no real surface** |

AT's `g₀₀` **never vanishes**: no clock-stopping surface. Every number above is **`g₀₀`-only**, so this audit
is silent on optics.

> **Correction (`G_021`).** This previously read "AT supplies no spatial metric, so no light bending, Shapiro
> delay or shadow size follows. The most distinctive consequence is the least derivable one." **That was
> wrong.** AT's metric is conformally flat, `g = ρ^(2/d)η`, hence `g_rr = +ρ^(2/d)`; the conformal class is
> imported (Malament 1977) and the factor native, and the project classes the chain as **closed**. Lensing
> *is* derivable — and `γ = −1` exactly, so deflection, `κ`, shear and Shapiro delay vanish while the redshift
> survives. That is **excluded by Cassini at 8.6957e4 σ** (VLBA 6.6660e3 σ, Gaia 124.8 σ). See
> **ResearchY-G_021**; the fix is the `ψ` sector, which requires `ψ = −4σ` and thereby changes the source law.

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the signature law · the weak-field no-go · the horizon corollary |
| **BOUNDARY** | the absolute-depth anchor `x = GM/(Rc²)` (imported M, R, z) · the `g₀₀`-only scope of these numbers |
| **REFUTED** | the weak-field route as a discriminator (2.1× to 3.3e6× short) |

**OP1 outcome:** an audit that imports measured constants **and survives** — 17–31 % divergence, 1.033 σ now,
3 σ at `σ_z = 8.37 %` of `z_AT`, i.e. 2.4×–6.0× beyond current measurements. The survival is not luck: the
signature is `x²`, and no solar-system or white-dwarf measurement reaches `x²`.

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: exact algebra on imported published values.

* Imported values are central values; NICER posteriors are asymmetric, so the σ are a symmetric approximation —
  the verdict turns on the ~2.4–6× gap, not on the σ detail.
* GR is compared exactly (`√(1+2Φ/c²)`), so no approximation does work on the GR side.
* The 20–50 % range for NS redshifts is a literature range across methods; it is the dominant uncertainty.
* The reachable arena is one object class, and the requirement is a **joint** measurement on the same object:
  `x` to ≈3 % **and** `z` to ≈8 %.

## Open problems (OP1–OP5)

1. Which single neutron star can deliver ≈3 % `M`–`R` **and** ≈8 % `z`? (NICER bursters; STROBE-X, eXTP, Athena.)
2. Can the radius inversion (z = 0.35 → R = 6.890 km under AT vs 9.164 km under GR) be a **pre-registered**
   kill criterion for a named object?
3. Is NICER's joint posterior tight enough in some source for `σ_x/x ≤ 3 %` without a new redshift measurement?
4. Any **strong-field electrodynamic** observable (magnetar QPOs, burst oscillations) that avoids
   atmosphere-model systematics?
5. **RESOLVED by `G_021`** — the premise was false. AT *does* derive the spatial metric
   (`g = ρ^(2/d)η`); the derived optics are `γ = −1`, **excluded** by Cassini at 8.6957e4 σ. The live question
   is now whether `ψ` is physical and whether the `ψ` sector preserves AT's source law.

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_019.md`
* `AT.Tests/Shared/PhysicalUnits.cs`
