# Y_E_001 Result — Electromagnetism Inventory Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_001_Tests.cs`
**Status:** 7/7 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_E_001"`
**Group total:** Group E = **7/7 PASSED**

## Verdict

**BOUNDARY** — AT's electromagnetic **kinematics is derived; its dynamics is declared but never computed.**

## The draft that was wrong (and how it was caught)

The first version of this audit concluded the EM **dynamics was absent** — no current, no kinetic term, no
Lagrangian. **The live scan refuted it.** AT has all of them, in `ResearchXH/LagrangianOrigin.cs` (QG244), as
members returning **strings** — verified mechanically by `EmDynamicsIsStringReturning()`:

| member | returns | kind |
|---|---|---|
| `ConservedCurrents()` | J^μ_em / J^μ_W / J^μ_s | `string[]` |
| `FieldStrengthForm()` | `F^a_μν = ∂_μA^a_ν − ∂_νA^a_μ + g f^abc A^b_μ A^c_ν` | `string` |
| `GaugeKineticTerm()` | `"L_gauge = −(1/4) F^a_μν F^aμν"` | `string` |
| `CovariantDerivative()` | `"D_μ = ∂_μ − ig T^a A^a_μ"` | `string` |
| `MatterTerm()` | `"L_matter = iψ̄γ^μ D_μ ψ − m ψ̄ψ"` | `string` |
| `LagrangianDensity()` | `"L = −(1/4) F^a_μν F^aμν + iψ̄γ^μ D_μ ψ − m ψ̄ψ"` | `string` |

`DerivationsThatAreConjunctions()` confirms the declaring booleans are conjunctions of other audits' predicates:

- `NoetherCurrentsExist() => OriginOfEnergy.EnergyConservationViaNoether() && GaugeDynamicsOrigin.CurrentsConserved()`
- `QedLagrangianDerived() => GaugeDynamicsOrigin.QedEquationDerived() && GaugeCouplingOrigin.AlphaEmMatches137() && GeneratorAlgebraCloses()`

## The program had already recorded it as open

`DynamicsWasRecordedOpen()` reads QG242's own admission from the repository: the gauge **DYNAMICS (interaction
Lagrangian, vertices, propagators)** was recorded as **HOSTED/OPEN**. QG243/244 then close it by declaration.

## Inventory

| component | verdict |
|---|---|
| gauge symmetry (U(1)) | **DERIVED** (twice, incompatibly — see below) |
| gauge structure 1 + 3 + 8 = 12 | **DERIVED** |
| electric charge (topological) | **DERIVED** |
| charge quantization | **BOUNDARY** |
| gauge connection / vector potential A_μ | **BOUNDARY** |
| photon sector | **BOUNDARY** |
| Noether currents J^μ_em / J^μ_W / J^μ_s | **ASSUMED** |
| current conservation ∂_μJ^μ = 0 | **ASSUMED** |
| field strength F^a_μν | **ASSUMED** |
| gauge kinetic term −¼F² | **ASSUMED** |
| derived Lagrangian density L | **ASSUMED** |
| **Maxwell field equation ∂_μF^μν = J^ν** | **MISSING** |
| **massless spin-1 wave equation □A_μ = 0** | **MISSING** |
| substrate wave equation | **DERIVED** (computed — `TemporalField`) |
| speed of light c | **DERIVED** |
| fine-structure constant α_em | **REFUTED** |

## Conservation is not the field equation

QG243 offers, as its "derived QED current-conservation law", **∂_μJ^μ = 0** — kinematic, implied by the symmetry.
The **sourced Maxwell equation ∂_μF^μν = J^ν** is never written and never solved. AT's only massless wave equation
is the **spin-2** Fierz–Pauli `□ψ_μν = 0`; nothing makes the photon propagate.

## The Lagrangian is selected, not derived

`LagrangianOrigin.cs` states its form is "the unique minimal action consistent with the D96 symmetries" — a
**minimality** argument, and the form written is the conventional minimal one. What AT derives is the **symmetries
and couplings** (its real achievement); the functional form is **selected**.

## The two contradictions

1. **U(1) has two incompatible origins:** Z_96 ⊂ D96 (QG161, deterministic) vs Aut(S¹) of the vortex moduli space
   (AT-X050). Neither references the other — the G_026 pattern.
2. **α has two values:** QG162 gives `1/α = 95 + 42 = 137` (0.03 % of 137.036); `FineStructureAnalyzer` gives
   **α⁻¹ ≈ 100**, concedes "not a precise derivation", and names α "the LARGEST REMAINING FREE PARAMETER in AT".

## One claim is not evidence

`GaugeSymmetryAnalyzer.SimulateEmergence` is `new Random(42)` with hand-picked thresholds
(0.15 / 0.07 / 0.18 / 0.02) presented as a "COMPUTATIONAL EXPERIMENT". Its conclusion — U(1) dominates — is a
deterministic function of the literal 0.15. The mechanism claim stands; the simulation is not support (G_027).

## Instrument defect found — it affects G_027 / G_033 / G_035

Those scanners strip with a **per-line** regex, which cannot see a verbatim (`@"..."`) string spanning lines — and
this repository writes most report text in exactly such blocks, so **prose is counted as executable code**.
Live proof on `J_Q` (pure report prose in `ProtoMatterCollectiveAnalyzer.cs`):

| stripper | counts `J_Q` as executable |
|---|---|
| per-line (G_027 / G_033 / G_035) | ≥ 1 |
| whole-file state machine (`AtSourceScan`) | 0 |

`AtSourceScan` (`AT.Core/ResearchXH/AtSourceScan.cs`) handles ordinary, verbatim, interpolated and raw strings,
char literals and both comment forms, preserving newlines. **Recommended follow-up: re-run G_027, G_033 and G_035
with it** — their "patterns removed after measurement" triage may have been compensating for this defect.

*Related:* `J_Q` — AT's only current-like object — is a **proto-matter condensate** current (continuity equation
`∂ρ_Q/∂t + ∇·J_Q = S` plus a Fick/drift form `J_Q = −D_eff·∇ρ_Q + v_drift·ρ_Q`), stated **only in prose**, with no
gauge index and no coupling to A_μ. A candidate *precursor* of the electromagnetic current, not that current.

## Minimal route to Maxwell theory

Three steps, and **only the third is new physics**:

1. **Compute what is declared** — turn `ConservedCurrents()` into a Noether current evaluated from the D96
   generator action.
2. **Make the action real** — turn the string `L = −¼F² + iψ̄γ^μD_μψ − mψ̄ψ` into an actual functional, so that "the
   field equations are its Euler–Lagrange equations" is about something rather than about a string.
3. **Then the new physics** — elevate from conservation ∂_μJ^μ = 0 to the **sourced** ∂_μF^μν = J^ν and read off
   □A_μ = 0 in the free case. No current AT result takes this step.

Before all three: resolve the two contradictions, and show that D96 **forces** that functional form rather than
merely permitting it.

## Classification and caveats

**No reclassification of any prior result.** QG161 / QG162 / QG243 / QG244 are unchanged inputs; their status labels
are re-read, not overwritten. D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: the scan is exact, cached, and excludes the audit's own source (so it cannot count its own
vocabulary as evidence).
