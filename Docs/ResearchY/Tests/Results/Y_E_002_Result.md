# Y_E_002 Result — Field Equation Derivation Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_002_Tests.cs`
**Status:** 6/6 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_E_002"`
**Group total:** group E = **13/13 PASSED** (E_001 7 + E_002 6)

## Verdict

**BOUNDARY** — both target equations **are** derivable, and this audit derives them by computation rather than
asserting them, but the premises the derivation needs are not AT's. `dF = 0` is an **identity** (hence vacuous);
`∂F = J` is derived **in full** from a *selected* action with an **undetermined** coupling. E_001's defect is one
of **method**, not impossibility.

## The success criterion, answered

| target | status |
|---|---|
| `dF = 0` | **DERIVED** — an identity; true for any A, costs nothing, so it is not a dynamical achievement |
| `∂F = J` | **DERIVED HERE, in full** — by actually performing the variation — but the premises are not AT's |
| continuity `∂J = 0` | **DERIVED twice, independently** — antisymmetry; gauge invariance |
| photon masslessness | **DERIVED** given gauge invariance |
| the photon field itself | **ABSENT** — AT's only computable massless wave equation is spin-2 |

## 1. `dF = 0` is an identity — and exactly so

The cyclic sum of `F = dA` vanishes for **any** A, and on a uniform grid it vanishes **exactly**, because the
cross second-differences cancel term by term. The residual is **roundoff, flat in h**:

| h | F = dA | directly-built F | B = r̂ (div B = 2/r) |
|---|---|---|---|
| 0.400 | 2.776×10⁻¹⁶ | 2.000 | 4.748 |
| 0.100 | 8.327×10⁻¹⁶ | 2.000 | 4.681 |
| 0.010 | 5.551×10⁻¹⁵ | 2.000 | 4.642 |

Worst residual **5.551×10⁻¹⁵**. It is an *algebraic cancellation*, not a converging approximation — a draft that
asserted a decreasing ratio here was wrong twice over.

**Content:** *no magnetic charge*. Exhibited by fields that fail it — a directly-built antisymmetric `F`
(constant 2.0) and `B = r̂` (**2/r**, computed 4.64–4.78), which is not any curl.

## 2. `∂F = J` — the variation actually performed

`S = Σ h⁴[−¼F_μνF_μν − J_μA_μ]` on a periodic lattice (N = 6, h = 0.4); `∂S/∂A_ν(y)` computed by perturbing
one link and recomputing `S` twice.

| quantity | measured |
|---|---|
| brute force vs closed form, relative | **3.945×10⁻¹²** |
| brute force vs closed form, absolute | 1.364×10⁻¹² |
| stationarity residual, EOM **satisfied** | **4.036×10⁻¹²** |
| stationarity residual, EOM **not** satisfied | 3.376×10¹ |

`dS/dA_ν(y) = h³Σ_μ[F_μν(y) − F_μν(y−μ̂)] − h⁴J_ν(y)`, so **stationarity of the action IS `∂_μF^μν = J^ν`**.

**Self-correction:** the brute-force step is **1e-2**, not 1e-6. `S` is exactly quadratic in A, so the central
difference is exact for any step and the only error is roundoff; at 1e-6 the report read 3.6×10⁻⁸ (the roundoff
floor masquerading as a disagreement), at 1e-2 it reads 4×10⁻¹².

## 3. Continuity — twice, independently

- **Antisymmetry:** `∂_ν∂_μF^μν = 0`. Residual **1.554×10⁻¹⁴**; the field-sourced current `J := ∂F` has relative
  divergence **3.309×10⁻¹⁶**.
- **Gauge invariance:** `ΔS_c = −Σh⁴χ ∂_μJ^μ`. Direct **8.917034×10⁻¹** vs divergence form **8.917034×10⁻¹**,
  agreement **8.771×10⁻¹⁵** — so invariance for every χ forces `∂_μJ^μ = 0`.
- **Control:** a lattice-periodic non-conserved `J_0 = cos(2πx₀/L)` has `|∂J| = 2.5` against the analytic
  `2π/L = 2.618`. *(A draft used `J_0 = x₀`, which is not periodic — the forward difference then reports the
  wrap jump, 5, not the derivative. Caught by the test.)*

## 4. The photon sector

| density | change under `A → A + ∂χ` |
|---|---|
| Maxwell `−¼F²` | **2.235×10⁻¹³** (invariant) |
| Proca `−½m²A²` | **5.017×10⁻¹** (not invariant) |

`A_ν = ε_ν cos(k·x)`, `k = (ω, κ, 0, 0)`, `ε·k = 0`, **Minkowski** signature:

| wave vector | residual | analytic |
|---|---|---|
| null (`ω = |k|`) | **8.327×10⁻⁹** | 0 |
| non-null (`ω = 0.9|k|`) | 1.539×10⁻¹ | 0.1539 |

So `ω = c|k|` and the photon is **exactly massless** — if U(1) is a gauge symmetry. Neither that nor the
transversality comes from AT; and AT's only computable massless wave equation is the **spin-2** `□ψ_μν = 0`.
*(A draft ran this in Euclidean signature, where no real null vector exists, and reported 1.62. Caught by the
test.)*

## 5. What AT supplies

| quantity | measured |
|---|---|
| AT members that **compute** a field strength or its divergence | **0** |
| AT EM-dynamics members returning **strings** | **6** |

Coupling contradiction: QG162 gives `1/α = 95 + 42 = 137` (0.03 % of 137.036); `FineStructureAnalyzer` gives
`α⁻¹ ≈ 100` and names α "the LARGEST REMAINING FREE PARAMETER in AT".

**Imported:** action principle, locality, 4-D counting, Lorentz invariance, the `−¼` normalisation, minimal
coupling. **AT's own:** the gauge symmetry's existence, and the dimension.

## Caveats

- Lattice work is **Euclidean**; the variational identity is signature-independent and the **Minkowski** signs are
  carried explicitly in the dispersion check, where they are required.
- The action is the **minimal** one AT itself writes down. The audit tests the *consequence* of that form, not its
  uniqueness.
- Three of the audit's own errors were caught by its own tests while running it (the eps step, the non-periodic
  control, the Euclidean dispersion). They are recorded rather than silently fixed.
