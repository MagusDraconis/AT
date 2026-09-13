# ResearchY-E_001 — Electromagnetism Inventory Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** E — Electromagnetism (new group)
**ID:** ResearchY-E_001 (permanent)
**Title:** What electromagnetic structure is already derived inside AT?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_001.md`
**Depends on:** QG161 (gauge generators), QG162 (couplings), QG243 (`GaugeDynamicsOrigin`), QG244 (`LagrangianOrigin`), AT-X050 (`GaugeSymmetryAnalyzer`), `FineStructureAnalyzer`, G_026/G_027 (the defect class and the verdict discipline), G_035 (the arity/strip discipline)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_001_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ElectromagnetismInventoryAudit.cs`, `AT.Core/ResearchXH/AtSourceScan.cs`

## The answer: BOUNDARY — the dynamics is declared, never computed

| question | answer |
|---|---|
| Is AT's EM kinematics derived? | **YES** — U(1), 1 + 3 + 8 = 12, topological charge, link connections, emergent c |
| Is AT's EM dynamics derived? | **DECLARED ONLY** — currents, F^a_μν, −¼F², D_μ and L all exist **as strings** |
| Is the Maxwell field equation present? | **NO** — only *current conservation* ∂_μJ^μ = 0, which is kinematic |
| Does the photon propagate? | **NO** — no massless spin-1 wave equation anywhere |
| Is the coupling settled? | **NO** — α has two contradictory values |

## A first draft got this wrong — and the live scan caught it

The first version of this audit concluded that the EM **dynamics was simply absent**: no current, no kinetic
term, no Lagrangian. **That was false.** AT has all of them — in `ResearchXH/LagrangianOrigin.cs` (QG244) — as
members returning **strings**:

| member | returns |
|---|---|
| `ConservedCurrents()` | the Noether currents J^μ_em / J^μ_W / J^μ_s, as an array of strings |
| `FieldStrengthForm()` | `F^a_μν = ∂_μA^a_ν − ∂_νA^a_μ + g f^abc A^b_μ A^c_ν` |
| `GaugeKineticTerm()` | `"L_gauge = −(1/4) F^a_μν F^aμν"` |
| `CovariantDerivative()` | `"D_μ = ∂_μ − ig T^a A^a_μ"` |
| `MatterTerm()` | `"L_matter = iψ̄γ^μ D_μ ψ − m ψ̄ψ"` |
| `LagrangianDensity()` | `"L = −(1/4) F^a_μν F^aμν + iψ̄γ^μ D_μ ψ − m ψ̄ψ"` |

And the booleans that declare this dynamics *derived* are **conjunctions of other audits' predicates**:
`NoetherCurrentsExist() => OriginOfEnergy.EnergyConservationViaNoether() && GaugeDynamicsOrigin.CurrentsConserved()`,
`QedLagrangianDerived() => GaugeDynamicsOrigin.QedEquationDerived() && GaugeCouplingOrigin.AlphaEmMatches137() && GeneratorAlgebraCloses()`.
Both are verified live by `EmDynamicsIsStringReturning()` and `DerivationsThatAreConjunctions()`.

## The program had already recorded this as open

QG242 recorded that it "found the gauge SYMMETRY derived but the gauge DYNAMICS (interaction Lagrangian, vertices,
propagators) **HOSTED/OPEN**". QG243 and QG244 then close it by declaration. The honest reading is that the
dynamics is **OPEN, restated**, which is why the inventory classifies it ASSUMED rather than DERIVED.
(`DynamicsWasRecordedOpen()` reads that admission from the repository at test time.)

## The distinction that matters: conservation ≠ field equation

QG243 offers, as "the derived QED current-conservation law", **∂_μJ^μ = 0**. That is a *conservation* statement,
kinematically implied by the symmetry. The **sourced Maxwell equation ∂_μF^μν = J^ν** is never written and never
solved. So AT has the conservation of a current it cannot compute, and lacks the field equation that would make
the photon propagate.

The one genuine, computed wave equation in the theory belongs to the **substrate** (`TemporalField`), not to the
photon. The only massless wave equation is the **spin-2** Fierz–Pauli `□ψ_μν = 0` of the gravity sector.

## Inventory

| component | verdict | why |
|---|---|---|
| gauge symmetry (U(1)) | **DERIVED** | QG161: rotation subgroup Z_96 ⊂ D96 — but see the double origin below |
| gauge structure 1 + 3 + 8 = 12 | **DERIVED** | QG161, = degree(C_96(1..6)) |
| electric charge (topological) | **DERIVED** | winding / monopole / instanton number |
| charge quantization | **BOUNDARY** | the integers are topological; the elementary unit is not derived |
| gauge connection / vector potential A_μ | **BOUNDARY** | a real link-connection picture (QG63/65); the *propagating* potential only stated |
| photon sector | **BOUNDARY** | exists as the U(1) generator, massless; no propagator |
| Noether currents J^μ_em / J^μ_W / J^μ_s | **ASSUMED** | named in strings; existence asserted by conjunction |
| current conservation ∂_μJ^μ = 0 | **ASSUMED** | kinematic; stated and asserted, never evaluated |
| field strength F^a_μν | **ASSUMED** | stated exactly, returned as a string |
| gauge kinetic term −¼F² | **ASSUMED** | declared, not constructed |
| derived Lagrangian density L | **ASSUMED** | a string, justified by a **minimality** argument |
| **Maxwell field equation ∂_μF^μν = J^ν** | **MISSING** | never written or solved |
| **massless spin-1 wave equation □A_μ = 0** | **MISSING** | nothing makes the photon propagate |
| substrate wave equation | **DERIVED** | `TemporalField`, computed |
| speed of light c | **DERIVED** | c = ℓ/τ |
| fine-structure constant α_em | **REFUTED** | two contradictory values |

### On the Lagrangian: what is derived, and what is selected

`LagrangianOrigin.cs` states that "the Lagrangian is NOT imported: its form … is the unique minimal action
consistent with the D96 symmetries". But the form it writes **is** the conventional minimal
`−¼F² + iψ̄γ^μD_μψ − mψ̄ψ`. So what AT genuinely derives is the **symmetries and the couplings** (which is AT's
real achievement — α from D96 spectral moments); what it **selects** is the standard functional form, by a
minimality argument. Selection is not derivation, and the audit says so.

## The two contradictions

1. **U(1) has two incompatible origins.** `GaugeSectorOrigin.cs` (QG161) derives it as the rotation subgroup
   Z_96 ⊂ D96 of the circulant automorphism group — deterministic, no fitted parameters. `GaugeSymmetryAnalyzer.cs`
   (AT-X050) derives it as Aut(S¹) of the **vortex moduli space** in a defect network. Neither references the
   other. Two origins for one group, both passing — the G_026 pattern.
2. **α disagrees with itself.** QG162 derives `1/α_em = Σm + #doublets = 95 + 42 = 137` (0.03 % of 137.036).
   `FineStructureAnalyzer` reports its vortex-stability route gives **α⁻¹ ≈ 100**, concedes the 1.4× gap is
   "not a precise derivation", and names α **"the LARGEST REMAINING FREE PARAMETER in AT"**. Both cannot stand.

## One claim is not evidence

`GaugeSymmetryAnalyzer.SimulateEmergence` presents a "COMPUTATIONAL EXPERIMENT" that is `new Random(42)` with
hand-picked defect probabilities (0.15 / 0.07 / 0.18 / 0.02). Its conclusion — U(1) dominates because
`vortexCount` is largest — is a **deterministic function of the literal 0.15**. The audit's own hostile review
concedes "the simulation is illustrative, not ab initio". The mechanism claim is unaffected; the simulation must
not be counted as support for it (G_027, applied to group E).

## Instrument defect found — it affects existing audits

The G_027 / G_033 / G_035 scanners strip with a **per-line** regex, which cannot see a verbatim (`@"..."`) string
spanning lines. This repository writes most of its report text in exactly such blocks, so under a per-line
stripper **prose is counted as executable code**.

Live proof (`VerbatimStringDefect()`), on `J_Q` in `ProtoMatterCollectiveAnalyzer.cs`, which is pure report prose:

| stripper | counts `J_Q` as executable |
|---|---|
| per-line (G_027/G_033/G_035) | **≥ 1** |
| whole-file state machine (`AtSourceScan`) | **0** |

`AtSourceScan` handles ordinary, verbatim, interpolated and raw strings, char literals and both comment forms,
preserving newlines so line alignment survives. **The three named audits should be re-run with it** — their
"patterns removed after measurement" triage may have been papering over this defect.

*Related:* `J_Q` — AT's only current-like object — is a **proto-matter condensate** current (a continuity
equation and a Fick/drift form `J_Q = −D_eff·∇ρ_Q + v_drift·ρ_Q`), stated **only in prose**, carrying no gauge
index and never coupling to A_μ. It is a candidate *precursor* of the electromagnetic current, not that current.

## Minimal route to Maxwell theory

Already present: the gauge group and its 1 + 3 + 8 = 12 structure (computed); the link connection; F^a_μν, −¼F²,
D_μ, the matter term and L (stated); current conservation (asserted); an emergent c; a computed substrate wave
equation.

The gap is **three steps, and only the third is new physics**:

1. **Compute what is declared.** J^μ_em is *named*; nothing evaluates it. Turn `ConservedCurrents()` into a Noether
   current computed from the D96 generator action.
2. **Make the action real.** Turn the *string* `L = −¼F² + iψ̄γ^μD_μψ − mψ̄ψ` into an actual functional, so that
   "the field equations are its Euler–Lagrange equations" is a statement about something rather than a sentence
   about a string.
3. **Then the new physics:** elevate from conservation ∂_μJ^μ = 0 to the **sourced** field equation
   ∂_μF^μν = J^ν, and read off □A_μ = 0 in the free case. No current AT result takes this step.

Before all three: resolve the two contradictions, and re-examine the minimality claim — D96 must be shown to
**force** that functional form, not merely permit it.

## Classification and caveats

**No reclassification of any prior result.** QG161/QG162/QG243/QG244 are unchanged inputs; their status labels
are re-read, not overwritten. D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: the scan is exact, cached, and excludes the audit's own source.
