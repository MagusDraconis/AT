# ResearchY-E_002 — Field Equation Derivation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** E — Electromagnetism
**ID:** ResearchY-E_002 (permanent)
**Title:** Can AT derive an actual field equation? (`dF = 0` and `partial F = J`)
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_002.md`
**Depends on:** E_001 (the inventory: dynamics declared, never computed), QG162 (the coupling), `FineStructureAnalyzer` (the competing coupling), G_027 (the verdict discipline), `AtSourceScan` (the whole-file strip)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_002_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/FieldEquationDerivationAudit.cs`

## The question

E_001 established that AT's electromagnetic **dynamics is declared, never computed**: the Lagrangian, field
strength, covariant derivative and conserved currents all exist in `LagrangianOrigin.cs` as members returning
**strings**, and the sourced Maxwell equation `∂_μF^μν = J^ν` is recorded **MISSING**.

So: can AT derive an actual field equation — specifically `dF = 0` and `∂F = J` — or is that impossible?

**Requirement:** *computed* dynamics, not declared strings.

## The answer: BOUNDARY — both ARE derivable, and this audit derives them by computation, but not from AT's premises

| equation | status | why |
|---|---|---|
| `dF = 0` | **DERIVED — and VACUOUS** | an identity: true for *any* A, free to anyone who writes F = dA |
| `∂F = J` | **DERIVED HERE, in full** | the variation is actually performed below — but from a *selected* action |
| continuity `∂J = 0` | **DERIVED, twice, independently** | antisymmetry; gauge invariance |
| photon masslessness | **DERIVED given gauge invariance** | the mass term is not gauge invariant |
| the photon field itself | **ABSENT** | nothing in AT produces the spin-1 field |

**The finding that matters:** E_001's defect is one of **METHOD, not impossibility**. The Lagrangian AT writes
down is the standard one, its field equation is the standard one, and both are **correct** — nobody had done the
variation. AT never *earned* what it wrote down.

## 1. `dF = 0` — an identity, and it is free

With `F = dA` the cyclic sum vanishes because partials commute. On a uniform grid it vanishes **exactly** — the
cross second-differences cancel term by term — so the computed residual is **roundoff, flat in h**:

| h | F = dA (identity) | directly-built F | B = r̂ (div B = 2/r) |
|---|---|---|---|
| 0.400 | 2.776×10⁻¹⁶ | 2.000 | 4.748 |
| 0.100 | 8.327×10⁻¹⁶ | 2.000 | 4.681 |
| 0.010 | 5.551×10⁻¹⁵ | 2.000 | 4.642 |

**Worst residual for F = dA: 5.551×10⁻¹⁵** — roundoff, and *not* a converging discretisation error. It is an
algebraic cancellation, not an asymptotic approximation.

The identity has **content** only as *"no magnetic charge"*, which the audit demonstrates by exhibiting fields
that **fail** it:

- a generic antisymmetric `F` built directly (residual **2.0**, constant in h);
- the radial field `B = r̂`, whose divergence is the non-zero **2/r** (computed 4.64–4.78) — so `B = r̂` is not
  any curl.

**Verdict for `dF = 0`: DERIVED and VACUOUS.** It costs nothing, so it neither demonstrates nor requires any
dynamics.

## 2. `∂F = J` — the variation, actually performed

The audit builds

```
S = Σ_x h⁴ [ −¼ F_μν F_μν − J_μ A_μ ]
```

on a periodic 4-D lattice (N = 6, h = 0.4), then computes `∂S/∂A_ν(y)` by **brute force**: perturb one link,
recompute the whole action twice, difference. Nothing here is a declared string.

| quantity | measured |
|---|---|
| brute force vs closed form, **relative** disagreement | **3.945×10⁻¹²** |
| brute force vs closed form, **absolute** disagreement | 1.364×10⁻¹² |
| stationarity residual when the EOM **is** satisfied | **4.036×10⁻¹²** |
| stationarity residual when it is **not** | 3.376×10¹ |

The closed form is

```
dS/dA_ν(y) = h³ Σ_μ [F_μν(y) − F_μν(y − μ̂)] − h⁴ J_ν(y)
```

so, dividing by h⁴, **stationarity of the action IS the discrete Maxwell equation** `∂_μF^μν = J^ν`. Computed,
not declared — this is precisely the element E_001 found missing.

**Method note (a defect the audit found in itself).** The brute-force step is **1e-2**, not 1e-6. `S` is exactly
quadratic in A, so the central difference is *exact for any step* and the only error is roundoff: at eps = 1e-6
the agreement is 3.6×10⁻⁸ (roundoff-limited, `S ~ 3×10³` so the noise is `S·ε_mach/(2ε)`), and at eps = 1e-2 it
is 4×10⁻¹². A first draft used 1e-6 and reported the roundoff floor as a disagreement.

## 3. Continuity — derived twice, independently

**(a) By antisymmetry.** `∂_ν∂_μF^μν = 0` because `F^μν = −F^νμ`: a symmetric double derivative meeting an
antisymmetric tensor has nothing left. Measured residual **1.554×10⁻¹⁴**; the current *sourced* by the field,
`J := ∂_μF^μν`, is then automatically conserved (relative divergence **3.309×10⁻¹⁶**).

**(b) By gauge invariance.** Under `A → A + ∂χ` the coupling term changes by exactly minus the divergence term,
`ΔS_c = −Σ_x h⁴ χ ∂_μJ^μ`:

| quantity | value |
|---|---|
| direct change of the coupling term | 8.917034×10⁻¹ |
| divergence form `−Σ h⁴χ ∂J` | 8.917034×10⁻¹ |
| agreement | **8.771×10⁻¹⁵** |

Invariance for **every** χ therefore forces `∂_μJ^μ = 0`. The control confirms it is not empty: a
lattice-periodic non-conserved current `J_0 = cos(2πx₀/L)` has `|∂J| = 2.5` against the analytic
maximum `2π/L = 2.618`.

*(A first draft used `J_0 = x₀`, which is not lattice-periodic; the forward difference then reports the **wrap
jump** — 5 instead of 1 — not the derivative. Caught by the test.)*

## 4. The photon sector — derived from given, absent as derived

**Gauge invariance forbids the mass.** Under `A → A + ∂χ`:

| density | change |
|---|---|
| Maxwell `−¼F²` | **2.235×10⁻¹³** (gauge invariant) |
| Proca `−½m²A²` | **5.017×10⁻¹** (not gauge invariant) |

**The dispersion comes from the field equation.** For `A_ν = ε_ν cos(k·x)` with `k = (ω, κ, 0, 0)` and
`ε·k = 0`, in Minkowski signature the divergence is `ε_ν(ω² − κ²)cos(k·x)`, so the equation holds iff
`k² = 0`:

| wave vector | residual | analytic |
|---|---|---|
| null (`ω = |k|`) | **8.327×10⁻⁹** | 0 |
| non-null (`ω = 0.9|k|`) | 1.539×10⁻¹ | 0.1539 |

So `ω = c|k|` and the photon is **exactly massless** — *if* U(1) is a gauge symmetry. Both the masslessness and
the transversality `ε·k = 0` come from that; the dispersion comes from the field equation. **Neither comes from
AT.**

**And AT's only computable massless wave equation is the spin-2** `□ψ_μν = 0` (Fierz–Pauli). Numeric massless
spin-1 wave equations in `AT.Core`: **0**.

*(A first draft ran the dispersion check in Euclidean signature, where `k² = ω² + κ²` has no real null vector,
and reported a residual of 1.62 where zero was expected. Caught by the test — the Lorentzian signs are required
for the null condition to exist at all.)*

## 5. What AT supplies — and what the derivation imports

| quantity | measured |
|---|---|
| AT members that **compute** a field strength or its divergence | **0** |
| AT EM-dynamics members that return **strings** | **6** (`ConservedCurrents`, `FieldStrengthForm`, `GaugeKineticTerm`, `CovariantDerivative`, `MatterTerm`, `LagrangianDensity`) |

**The coupling contradicts itself.** `ResearchXH/GaugeCouplingOrigin.cs` (QG162) gives
`1/α = Σm + #doublets = 95 + 42 = 137`, matching 137.036 to 0.03 %; `Research/FineStructureAnalyzer.cs` gives
`α⁻¹ ≈ 100`, concedes "not a precise derivation", and is called by its own hostile review *"the LARGEST REMAINING
FREE PARAMETER in AT"*. Both cannot stand.

**Imported (not AT's):** the action principle, locality, 4-D dimensional counting, Lorentz invariance, the
normalisation `−¼` and the minimal coupling.
**AT's own:** the gauge symmetry's existence, and the dimension in which Maxwell has the right form.

## Output

**BOUNDARY.** The success criterion is met in the only way available to AT: `dF = 0` is derived (as an
identity, hence vacuously), and `∂F = J` is derived here **in full** — but from an action whose form AT selects
by minimality and whose coupling AT cannot supply. The derivation runs on premises AT does not provide. The
field equation is **earned by standard means, not by AT**.

## Classification and caveats

**Registry:** group E has no G-style classification registry, so no registry entry is added and no prior
classification changes. The audit's own `Verdict()` is **computed** from six independent numerical checks, not a
literal (`G_027`).

**Caveats.**
(1) The lattice work is **Euclidean**; the variational identity `g/h⁴ = Σ∂F − J` is signature-independent, and
the **Minkowski** signs are carried explicitly in the dispersion check (they are required there).
(2) The action is the **minimal** one, `−¼F² − J·A` — the form AT itself writes down; the audit tests the
consequence of that form, not its uniqueness. Uniqueness of `−¼F²` among local gauge-invariant dimension-4
operators is argued in the audit's discussion but is **not** what the numerics establish.
(3) Deterministic: explicit wave numbers, no RNG, invariant-culture formatting.

**Methodological finding: an audit's own derivation must be excluded from other audits' scans.** This audit's core
file reproduces a sourced Maxwell divergence and a massless vacuum wave equation *by hand*, in order to test
whether AT has them. E_001's whole-tree scan counted them, and **two E_001 tests failed** — its two `Missing`
components picked up `DocumentedCount 3/7` and `ExecutableCount 0/2`, i.e. E_001 briefly concluded AT *has* a
computable massless spin-1 wave equation, which is the exact opposite of its finding and of this audit's. The fix
is the established self-reference exclusion (G_033/G_035's `SelfId` pattern): `FieldEquationDerivationAudit.cs`
is now in E_001's `SelfFiles`, with the reason recorded in the code. This is a general rule, not a one-off — an
audit that *derives* a standard result will otherwise be read as evidence that the theory *contains* it.

**No reclassification.** E_001's inventory is unchanged and is used as an input; the D_040 registry is untouched;
no canonical claim, value or equation changes; no new primitive.
