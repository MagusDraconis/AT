# ResearchY-G_019 — Second-Order Signature Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_019 (permanent)
**Title:** Second-Order Signature Audit — can AT's clock law be discriminated from GR by observation?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_019.md`
**Depends on:** ResearchY-G_018 (OP1: the group had never produced an audit that imports measured constants and
survives), G_009 (the clock law `dτ/dt = ρ^(1/d)` and the ±½ second-order split), G_004 (AT ≡ GR at the Earth's
surface, 0.99600), G_007 (the lattice form and its BOUNDARY values), G_016 (ρ is more primitive than
mass-energy), G_017 (the laboratory |ψ|² identification is excluded); AT-QG QG197 (`g₀₀ = −ρ^(2/d)`)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_019_Tests.cs` (7/7 PASSED, ~0.03 s)

## Purpose

G_018's provenance ledger ended with an explicit challenge: every audit that **imports measured constants** is
the only kind that can be *falsified* — and where testable, all of them had been excluded or shown to be a
rank-1 shadow. G_018 OP1 asked for the audit that had never been produced:

> **An audit that imports measured constants and still survives observation.**

This is it. AT's clock law `dτ/dt = ρ^(1/d)` from `g₀₀ = −ρ^(2/d)` (QG197), with `σ = Φ/c²`, gives

```
AT:  dτ/dt = e^σ              GR:  dτ/dt = √(1 + 2σ)
```

**First order they are identical; second order they have opposite signs (+½ against −½)** — G_009's finding,
here turned into a measurement audit:

```
AT/GR = e^x / √(1 + 2x) = 1 + x² − (4/3)x³ + …          (x = Φ/c² < 0 for a bound object)
```

The discriminator is therefore **x squared**. That single fact decides the entire audit.

## 1. DERIVED — the signature, and the weak-field no-go

Series verified numerically where representable: `(ratio − 1)/x² = 1.0000889006` at x = −1e-6,
`1.0001333539` at −1e-4, `1.0135879522` at −1e-2, and `1.2774576758` at −0.15 (the −4x/3 term).
Sign: AT clocks run **faster** (`e^{−0.15} = 0.8607079764 > √0.7 = 0.8366600265`), so **AT's redshift is always
smaller** (`z_AT = 0.1618342427` against `z_GR = 0.1952286093` at x = −0.15).

**The signature is x² — so no weak-field measurement can ever see it.** That is a *derived* statement about the
theory, and the imported data make it quantitative:

| case | `x` | signature `x²` | precision available | short by |
|---|---|---|---|---|
| Earth's surface | −6.961275e-10 | **4.845934e-19** | 1e-18 (clock floor) | **2.1×** |
| ground vs GPS (differential) | — | **4.567944e-19** | 1e-18 | 2.2× |
| Sun's surface | −2.122503e-6 | 4.505017e-12 | 1e-5 | **2.2e6×** |
| Sirius B (white dwarf) | −2.572878e-4 | 6.619702e-8 | 0.02 (HST, 2 %) | **3.3e6×** |

Two further facts from the same data: Sirius B's *own* AT-vs-GR redshift difference is only
**3.0e-4 relative** — a 2 % measurement cannot see it; and at `x = 1e-9` the true difference (1e-18) is **below
one ulp of 1.0 (1.11e-16)**, i.e. at solar-system depths the signature is not merely small, it is
*unrepresentable*.

## 2. The live arena — neutron-star compactness

The only place `x²` is comparable to the precision is where `x = GM/(Rc²)` is large. Imported NICER posteriors
give **3–10 % in the rate and 17–31 % in the redshift**:

| object | `x` | rate AT/GR − 1 | `z_AT` | `z_GR` | `Δz/z` |
|---|---|---|---|---|---|
| J0030+0451 (NICER) | −0.152011 | **+0.029638** | 0.1641732 | 0.1986775 | **−17.367 %** |
| J0740+6620 (Riley) | −0.247002 | **+0.098133** | 0.2801814 | 0.4058088 | **−30.957 %** |
| J0740+6620 (Miller) | −0.224246 | **+0.076057** | 0.2513786 | 0.3465546 | **−27.463 %** |

The absolute gap in `z` for J0740+6620 is **0.1256** — an order of magnitude above any plausible `z` precision.

## 3. The inverse map — same redshift, two radii

For a given observed `z` and known mass, AT and GR imply **different radii**
(`x_GR = (1 − (1+z)^(−2))/2`, `x_AT = ln(1+z)`, `R = GM/(x c²)`); at M = 1.4 M☉:

| z | `x_GR` | `x_AT` | `R_GR` | `R_AT` | `ΔR/R` |
|---|---|---|---|---|---|
| 0.15 | 0.121928 | 0.139762 | 16.959 km | 14.795 km | −12.76 % |
| 0.20 | 0.152778 | 0.182322 | 13.535 km | 11.342 km | −16.20 % |
| 0.25 | 0.180000 | 0.223144 | 11.488 km | 9.267 km | −19.33 % |
| 0.30 | 0.204142 | 0.262364 | 10.129 km | 7.881 km | −22.19 % |
| **0.35** | 0.225652 | 0.300105 | 9.164 km | **6.890 km** | **−24.81 %** |
| 0.40 | 0.244898 | 0.336472 | 8.444 km | 6.146 km | −27.22 % |

**A NICER-compatible object (R ≈ 11–14 km at M ≈ 1.4 M☉) cannot have z = 0.35 under AT**, which would demand
R = 6.890 km — far below every NICER radius. That is the sharpest form of the test.

## 4. The deciding precision — **AT survives**

For a NICER-quality object (M = 1.4 ± 0.05 M☉, R = 12 ± 1 km):

| quantity | value |
|---|---|
| `x` | **−0.172317 ± 0.020514** (11.90 %) |
| `z_AT` | **0.188055 ± 0.024372** |
| `z_GR` | **0.235259 ± 0.038665** |
| separation | **0.047205** |
| combined σ | **0.045706** |
| **significance now** | **1.033 σ** |
| needed for 3σ | `σ_z ≤ 0.015735` = **8.37 % of `z_AT`** (6.69 % of `z_GR`) |
| current NS `z` determinations | 20–50 % relative → **short by 2.4× to 6.0×** |

> **AT is not excluded by any compactness or redshift measurement, and the measurement that would decide it is
> now specified exactly.**

## 5. The horizon corollary — and the g₀₀-only boundary

AT's `g₀₀ = −e^(2x)` **never vanishes** for finite depth, so AT's clock law has **no clock-stopping surface**;
GR's `1 + 2x` vanishes at `y = GM/(Rc²) = 1/2`, where its redshift **diverges**:

| y | AT `g₀₀ = −e^(−2y)` | `z_AT` | GR `1 − 2y` | `z_GR` |
|---|---|---|---|---|
| 0.10 | −0.81873075 | 0.1051709 | +0.800000 | 0.1180340 |
| 0.25 | −0.60653066 | 0.2840254 | +0.500000 | 0.4142136 |
| 0.40 | −0.44932896 | 0.4918247 | +0.200000 | 1.2360680 |
| 0.4999 | −0.36795302 | 0.6485564 | +0.000200 | **69.71068** |
| **0.50** | **−0.36787944** | **0.6487213** | **0.000000** | **DIVERGES** |
| 0.60 / 1.0 / 5.0 | finite | 0.8221188 / 1.718282 / 147.4132 | **negative** | **no real surface** |

Beyond `y = 1/2` GR has **no real surface at all**, while AT still does.

**Correction (ResearchY-G_021).** This section originally claimed that "AT supplies **no spatial metric**, so
no light bending, no Shapiro delay and no shadow size follows from it" — **that is false**, and it is false in a
way that inverts the conclusion. AT **does** supply the full metric: it is **conformally flat**,
`g = ρ^(2/d) η`, so `g₀₀ = −ρ^(2/d)` **and** `g_rr = +ρ^(2/d)`. The project already classifies the chain as
**closed** (`Docs/Audits/MetricOriginClosure.md`: "class × factor — closed"), with the **conformal class
imported** (Malament 1977) and the **conformal factor native** (the counting measure).

So light bending **is** derivable — and the derived answer is **zero**. With `σ = (1/d) ln ρ` and `Φ = σ`, the
spatial part is `g_rr = +(1 + 2σ)` while `g₀₀ = −(1 + 2σ)`; PPN is `g_rr = 1 − 2γΦ`, so

```text
γ = −1  exactly,  hence  (1 + γ)/2 = 0
```

and **deflection, convergence κ, shear and the Shapiro delay all vanish identically** (`μ = 1`), while the
**redshift survives** because it is governed by `g₀₀` alone. AT predicts *redshift without lensing*. That is a
**derived falsification, not a missing derivation**, and it is excluded by **Cassini at 8.6957e4 σ**, VLBA at
6.6660e3 σ and Gaia at 124.8 σ — see **ResearchY-G_021**.

The escape (project result, QG207/QG212) is the `ψ ≠ 0` tensor completion
`g₀₀ = −ρ^(2/d)e^(2ψ)`, `g_ii = ρ^(2/d)e^(−2ψ/(d−1))`, where `γ = +1`; but `G_021` derives that this requires
`ψ = −4σ` in `d = 3`, which also moves `Φ` from `σ` to `−3σ` and therefore **changes the sourcing relation** —
so `ψ` is a new primitive (QG24), not a free patch.

What *remains* true about this audit is narrower and untouched: **every number in it is `g₀₀`-only**, so the
audit is silent on optics either way.

## 6. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the **signature law** `AT/GR = 1 + x² − (4/3)x³` (first order identical, second order opposite sign, AT clocks faster, AT redshift always smaller) · the **weak-field no-go** (the discriminator is `x²`, unreachable at every solar-system and white-dwarf depth) · the **horizon corollary** (`g₀₀` never vanishes). |
| **BOUNDARY** | the **absolute-depth anchor** `x = GM/(Rc²)` — imported `M`, `R` and a measured redshift; the test is live only at neutron-star compactness. And the **`g₀₀`-only scope of this audit's own numbers**. |
| **REFUTED** | the **weak-field route as a discriminator** — Earth 4.845934e-19 (0.4846× the clock floor, 2.1× short), ground-vs-GPS 4.567944e-19, the Sun 4.505017e-12 against 1e-5 (2.2e6× short), Sirius B 6.619702e-8 against a 2 % measured redshift (3.3e6× short). |

## 7. The OP1 answer

> **Yes — an audit that imports measured constants and survives.** The divergence is **17–31 % in `z`**; the
> current separation is **1.033 σ**; **3 σ needs `σ_z = 8.37 %` of `z_AT`**, which is **2.4× to 6.0× beyond**
> current determinations.

And the survival is **not luck**: the signature is `x²`, and no two-object measurement in the solar system or on
a white dwarf reaches `x²`, so only the compact-object test is live. This is the first audit in the group whose
imported content is falsifiable **and** not falsified.

It comes with two honest riders:

1. **The reachable arena is one object class** (neutron stars), and the requirement is a *joint* measurement on
   the *same* object: an independent `M`–`R` giving `x` to ≈3 %, **and** a surface redshift to ≈8 %.
2. **The largest discrepancy is not a lensing problem** (corrected by `G_021`): at `y = 1/2` GR's `z` diverges
   while AT's stays finite (0.6487213). This audit originally called that consequence "outside AT" because it
   believed AT had no spatial metric. It does: `g = ρ^(2/d)η` gives `γ = −1`, so lensing and Shapiro delay
   vanish — excluded by Cassini at 8.6957e4 σ. The missing piece is the `ψ` sector, not the spatial metric.

## 8. Classification and caveats

**No reclassification.** G_004's calibration, G_009's clock law, G_007's boundary values, G_016's pairing and
G_017's exclusion are unchanged inputs. D_040 untouched; no canonical claim, value or equation changes; no new
primitive. Deterministic: exact algebra on imported **published** values (NICER masses and radii, the IAU Sun,
Sirius B's mass and radius, the 1e-18 clock floor, a 2 % HST redshift).

* The imported values are used as *central values*; the NICER posteriors are asymmetric, so the quoted σ are a
  representative (symmetric) approximation. This does not affect the verdict, which turns on the ~2.4–6× gap.
* The GR comparison uses the exact Schwarzschild `√(1+2Φ/c²)`, not its weak-field expansion — so no
  approximation is doing work on the GR side.
* The 20–50 % range for neutron-star redshift determinations is a literature range across methods (burst
  spectra, atmosphere fits, absorption lines); it is the *dominant* uncertainty and is stated as a range for
  exactly that reason.
* The `x²` note at 1e-9 depth is a numerical *and* physical statement: at such depths the signature is below one
  ulp of 1.0.

## 9. Open problems (OP1–OP5)

1. Which single neutron star can deliver both a ≈3 % `M`–`R` **and** a ≈8 % surface redshift? Candidates:
   the NICER bursters with distance priors, and the next-generation missions (STROBE-X, eXTP, Athena).
2. Can the AT-vs-GR **radius inversion** (z = 0.35 requires R = 6.890 km under AT against 9.164 km under GR) be
   phrased as a *pre-registered* kill criterion for a named object?
3. Is `M`–`R` from NICER's *joint* two-parameter posterior tight enough in some source to give `σ_x/x ≤ 3 %`
   even without a new redshift measurement?
4. Does any **strong-field electrodynamic** observable (magnetar QPOs, burst oscillations) depend on the
   surface rate in a way that avoids the atmosphere-model systematics?
5. **RESOLVED by `G_021`** (this stated premise was false): AT *does* derive the spatial metric —
   `g = ρ^(2/d)η`, `g_rr = +ρ^(2/d)`. The derived optics are `γ = −1`, which is **excluded** by Cassini at
   8.6957e4 σ, so the open question is no longer "is there a spatial metric?" but **"is `ψ` physical, and does
   the `ψ` sector preserve AT's own source law?"** (see ResearchY-G_021).

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_019_Tests.cs` — **7/7 PASSED** (~0.03 s)
**Group total:** G_001–G_019 = **168/168 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_019"`

| label | content |
|-------|---------|
| **DERIVED** | the signature `AT/GR = 1 + x² − (4/3)x³` · the weak-field no-go · the horizon corollary |
| **BOUNDARY** | the absolute-depth anchor `x = GM/(Rc²)` (imported M, R, z) · the g₀₀-only reach |
| **REFUTED** | the weak-field route as a discriminator (2.1× to 3.3e6× short at every such depth) |

**OP1 outcome: AT SURVIVES** — 17–31 % divergence in `z`, 1.033 σ now, 3 σ at `σ_z = 8.37 %` of `z_AT`
(2.4×–6.0× beyond current measurements).

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_018.md`, `ResearchY-G_009.md`, `ResearchY-G_004.md`,
  `ResearchY-G_007.md`, `ResearchY-G_017.md`
* `Docs/ResearchY/Tests/Results/Y_G_019_Result.md`
* Imported published values: NICER masses and radii for PSR J0030+0451 and PSR J0740+6620; IAU nominal solar
  radius and `GM☉`; Sirius B mass and radius; the 1e-18 optical-clock floor; a 2 % HST white-dwarf redshift.
