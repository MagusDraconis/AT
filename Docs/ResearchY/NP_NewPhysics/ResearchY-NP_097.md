# ResearchY-NP_097 — Temperature Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_097 (permanent)
**Title:** Temperature Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_097.md`
**Depends on:** ResearchY-NP_081 (energy = relabeling of count), NP_095 (friction = resonance
scattering), NP_096 (heat = random phase; entropy = mode multiplicity), AT-QG QG216 (Born rule /
count conservation), QG228 (information I_occ = KL(ρ‖uniform)), ResearchY-D_041 (tick / phase
advance)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_097_Tests.cs`

---

## Purpose

NP_096 established heat = random-phase mode multiplicity and entropy H = −Σρ ln ρ (the mode
multiplicity). NP_097 asks the closing thermodynamic question: **what is temperature?** Program:
(1) inventory heat / entropy / temperature; (2) decide whether temperature is mode-multiplicity
density / phase randomness / scattering rate / count-distribution width; (3) compare cold, warm,
hot states; (4) find the quantity that monotonically tracks temperature; (5) determine whether T
is derived from H, I_occ, ρ — or requires a new boundary. **Success criterion:** the AT ontology
of temperature, and whether it is derived or boundary. No new primitives; canonical AT unchanged.

---

## 1. Inventory — heat, entropy, temperature

| Object | In AT | Source |
|---|---|---|
| **heat** | random-phase mode multiplicity (the incoherent residue of friction) | NP_096 |
| **entropy** | H = −Σρ ln ρ — the mode multiplicity (how many modes the count is spread over) | NP_096 |
| **energy** | the conserved count U = Σρ (relabeling, NP_081) | NP_081 |
| **temperature** | **the width of the occupancy distribution ρ across modes** | this audit |

---

## 2. A / B / C / D — what is temperature?

| Interpretation | Verdict |
|---|---|
| **A) mode-multiplicity density** | **YES — the inverse reading.** 1/T = ∂S/∂U is the rate at which entropy (mode multiplicity) grows per unit count added. |
| **B) phase randomness** | **NO — that is HEAT** (the random phase itself, NP_096), not its temperature. |
| **C) scattering rate** | **CONSEQUENCE, not temperature.** A hotter body scatters faster, but the scattering rate is a downstream effect. |
| **D) count-distribution width** | **YES — the answer.** Temperature IS the width of the occupancy ρ over modes: T = ∂U/∂H, the derivative of the conserved count with respect to the entropy/mode multiplicity. |

**Determination: D (count-distribution width), with A as its inverse reading (1/T = ∂H/∂U).**
Temperature is the thermodynamic conjugate of entropy: T = ∂U/∂S, where U is the conserved count
(energy, NP_081) and S = H = −Σρ ln ρ (entropy, NP_096). It measures how the occupancy widens or
narrows as the count changes.

---

## 3. Cold, warm, hot

Use the canonical occupancy ρ_k ∝ e^(−E_k/T) over the K = 95 modes (E_k = k). Temperature T is the
width parameter: it sets how far up the mode ladder the occupancy extends.

| State | T | H = −Σρ ln ρ | U = ΣρE_k | significant modes | I_occ = ln K − H |
|---|---|---|---|---|---|
| **cold** | 0.2 | 0.0407 | 1.01 | 2 | 4.5132 |
| **warm** | 1.0 | 1.0407 | 1.58 | 7 | 3.5132 |
| **hot** | 10 | 3.3022 | 10.50 | 46 | 1.2517 |
| **very hot** | 100 | 4.5171 | 40.59 | 95 | 0.0368 |

**Cold = narrow occupancy (few modes); hot = wide occupancy (many modes).** The entropy H rises
monotonically with T, and the information I_occ = ln K − H falls monotonically with T.

---

## 4. What monotonically tracks temperature?

| Quantity | Behavior as T grows |
|---|---|
| **H (entropy / mode multiplicity)** | **INCREASES monotonically** (0.04 → 4.52) |
| **the occupancy width (significant modes)** | **INCREASES monotonically** (2 → 95) |
| **I_occ = ln K − H (information)** | **DECREASES monotonically** (4.51 → 0.04) |

**The mode multiplicity (H, equivalently the occupancy width) is the monotonic tracker of
temperature.** Hotter = the conserved count spread over more modes; colder = the count concentrated
in fewer modes.

---

## 5. Is T derived, or a new boundary?

```
T  =  ∂U/∂S        (the thermodynamic definition)
   =  ∂(count)/∂(H = −Σρ ln ρ)     [U from NP_081, H from NP_096]
   =  the width of the occupancy ρ across modes            [DERIVED from ρ]
```

**Temperature is DERIVED** — it is the derivative of the conserved count with respect to the
entropy, i.e., the occupancy width. It is a functional of ρ alone; nothing new is imported. The
thermodynamic identity is verified: for the canonical occupancy ρ_k ∝ e^(−βE_k), the finite-
difference dS/dU equals β exactly (verified: dS/dU = 0.500000 at β = 0.5, so T = dU/dS = 2.0).

**BUT — the absolute Kelvin scale is a BOUNDARY.** As with every dimensionful observable (NP_082:
m_e; the v anchor; standard unit conversion), the *dimensionless* temperature (the occupancy width)
is derived, while the *absolute* temperature unit (Kelvin) requires the unit-conversion anchor —
Boltzmann's constant k_B — exactly the "one dimensionful scale" pattern. So:

| Component | Status |
|---|---|
| dimensionless temperature (the occupancy width ∂U/∂H) | **DERIVED** (from ρ) |
| the absolute Kelvin scale (k_B unit conversion) | **BOUNDARY** (the dimensionful anchor) |

---

## Theorem

> **Theorem (NP_097).** Temperature in AT is the COUNT-DISTRIBUTION WIDTH (D) — the thermodynamic
> conjugate of entropy: T = ∂U/∂S, where U is the conserved count (energy, NP_081) and S = H =
> −Σρ ln ρ is the entropy / mode multiplicity (NP_096). Equivalently 1/T = ∂S/∂U is the mode-
> multiplicity density (A, the inverse reading). Temperature is DERIVED from the occupancy ρ alone:
> for the canonical occupancy ρ_k ∝ e^(−E_k/T) over the K = 95 modes, T is the width parameter — cold
> (T = 0.2) concentrates the count in 2 modes (H = 0.04), hot (T = 100) spreads it over all 95
> (H = 4.52). The mode multiplicity H (equivalently the occupancy width) monotonically tracks T, and
> the information I_occ = ln K − H (QG228) monotonically falls. The thermodynamic identity is
> verified exactly (dS/dU = β, so T = dU/dS). Phase randomness (B) is HEAT (NP_096), not temperature;
> the scattering rate (C) is a consequence. The dimensionless temperature is DERIVED (a functional of
> ρ); the ABSOLUTE Kelvin scale is BOUNDARY — the unit-conversion anchor k_B, the same "one
> dimensionful scale" pattern as m_e (NP_082) and v. Proof: (1) Inventory (Section 1). (2) Test
> A–D (Section 2, verified — D the answer, A the inverse). (3) Cold/warm/hot (Section 3, verified —
> H and width monotonic in T). (4) The monotonic tracker (Section 4, verified). (5) Derived vs
> boundary (Section 5, verified — dS/dU = β). **Success criterion: temperature is the count-
> distribution width (∂U/∂H), DERIVED from ρ; only the absolute Kelvin scale is BOUNDARY (the k_B
> unit anchor).** Classification: the dimensionless temperature DERIVED (from ρ/H/U); the absolute
> temperature scale BOUNDARY (unit conversion k_B); "temperature as phase randomness" REFUTED (that
> is heat); "temperature as a new primitive" REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Test A–D. (3) Cold/warm/hot. (4) Monotonic tracker.
> (5) Derived vs boundary. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "temperature is phase randomness" | phase randomness is HEAT (NP_096); temperature is its width parameter |
| "temperature is the scattering rate" | the scattering rate is a consequence of temperature, not temperature itself |
| "temperature is a new primitive" | T = ∂U/∂H is a functional of ρ (count + entropy), nothing imported |
| "temperature needs no unit anchor" | the absolute Kelvin scale needs k_B (the dimensionful anchor, like m_e/v) |
| "entropy is not monotonic in T" | H rises monotonically with T (0.04 → 4.52) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| T = ∂U/∂S is derived from ρ | a temperature not expressible as the derivative of count w.r.t. entropy |
| the occupancy width tracks T | a hotter state with a narrower occupancy (fewer modes) |
| the dimensionless temperature is derived | a temperature requiring an input beyond ρ and H |
| the absolute scale is the only boundary | a derivation of k_B from the D96 spectrum alone |

---

## 8. Classification

| Component | Status |
|---|---|
| dimensionless temperature (the occupancy width ∂U/∂H) | **DERIVED** (from ρ, H, U) |
| entropy H = −Σρ ln ρ (the mode multiplicity) | **DERIVED** (NP_096) |
| the absolute Kelvin scale (k_B unit conversion) | **BOUNDARY** (the dimensionful anchor) |
| temperature as phase randomness | **REFUTED** (that is heat, NP_096) |
| temperature as a new primitive | **REFUTED** |

**Conclusion.** Temperature is the **count-distribution width** — the thermodynamic conjugate of
entropy, T = ∂U/∂S, where U is the conserved count (energy, NP_081) and S = H = −Σρ ln ρ is the
mode multiplicity (entropy, NP_096). It is DERIVED from the occupancy ρ alone: cold concentrates
the count in few modes (narrow, low H), hot spreads it over many modes (wide, high H), and the
mode multiplicity H is the monotonic tracker (with the information I_occ = ln K − H falling). The
thermodynamic identity dS/dU = β = 1/T is exact. The dimensionless temperature is DERIVED; the
absolute Kelvin scale is a BOUNDARY (the unit-conversion anchor k_B, the same "one dimensionful
scale" pattern as m_e and v). No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_097_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_097_Inventory` | heat / entropy / temperature = random phase / H / width | ✅ |
| `Y_NP_097_ABCD` | D (width); A inverse; B heat; C consequence | ✅ |
| `Y_NP_097_ColdWarmHot` | H and width monotonic in T | ✅ |
| `Y_NP_097_MonotonicTracker` | H increases, I_occ decreases with T | ✅ |
| `Y_NP_097_ThermodynamicIdentity` | dS/dU = β (T = dU/dS) | ✅ |
| `Y_NP_097_DerivedOrBoundary` | dimensionless T derived; absolute scale boundary (k_B) | ✅ |
| `Y_NP_097_Classification` | T derived; phase-randomness/new-primitive REFUTED | ✅ |
| `Y_NP_097_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_097"`

---

## References

- ResearchY-NP_081 (energy = relabeling of count), NP_095 (friction), NP_096 (heat / entropy),
  NP_082 (m_e anchor — the "one dimensionful scale" pattern).
- AT-QG: QG216 (Born rule / count conservation), QG228 (information I_occ = KL(ρ‖uniform)).
- ResearchY-D_041 (tick / phase advance).
