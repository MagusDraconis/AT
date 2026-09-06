# ResearchY-NP_055 — Dark Energy Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_055 (permanent)
**Title:** Dark Energy Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_055.md`
**Depends on:** AT-QG QG234 (cosmological fractions ΩΛ = I_occ/ln K), ResearchY-QG_018
(information-cosmology closure), ResearchY-NP_027–NP_037 (Planck/blackbody, ħ,
temperature, structure vs thermodynamics, thermal-N, ensemble, Bose-without-blackbody,
DOS origin, 3D emergence, role of three), QG230 (Lambda origin), QG228 (I_occ),
NP_018/NP_019/QG_012/QG_017 (the information-cosmology finite family), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_055_Tests.cs`

---

## Purpose

**What is Dark Energy physically inside Actualization Theory?** ΩΛ = I_occ/ln K = 0.6839
matches observation to 0.12% (QG234) — but what *ontology* does ΩΛ represent? This audit
inventories every object linked to ΩΛ, tests five candidate interpretations
(A vacuum energy, B information content, C occupancy deficit, D actualization pressure,
E cosmological bookkeeping only), determines whether any interpretation predicts
additional observables, compares with ΛCDM (w = −1), and tests future evolution / ΩΛ(z) /
q₀ / z_acc. **Success criterion:** determine whether AT contains a *physical* ontology of
Dark Energy or only a successful *numerical* relation. No new primitives; canonical AT
unchanged.

---

## 1. Inventory — every object linked to ΩΛ

| Object | Value | Source | Role in ΩΛ |
|---|---|---|---|
| octave occupancy | [4, 4, 87] (95 modes, top-heavy) | A_003 / D_030 | the count density ρ |
| count density ρ | ρ = [4,4,87]/95 = [0.0421, 0.0421, 0.9158] | QG194/QG216 | the distribution |
| information density I_occ | KL(ρ‖uniform) = 0.7513 nats | QG228 | numerator |
| state-space size ln K | ln 3 = 1.0986 nats | QG227/234 | denominator |
| realized entropy H | ln K − I_occ = 0.3473 nats | QG228 | the matter side |
| **ΩΛ** | I_occ/ln K = **0.6839** | QG234 | OBSERVED 0.12% |
| **Ωm** | H/ln K = 1 − ΩΛ = **0.3161** | QG234 | OBSERVED 0.26% |
| actualization (branching) | μ = 2 growth, S = 255 | A_003 | the generator of ρ |

**Key identity (verified):** the canonical ρ = [4,4,87]/95 reproduces
I_occ = KL(ρ‖uniform) = 0.7513 exactly, and the pair partitions the state-space size:

```
I_occ + H = ln K     (0.7513 + 0.3473 = 1.0986)
ΩΛ = I_occ/ln K      (the EXCESS-over-uniform fraction)
Ωm = H/ln K          (the realized-entropy fraction)
```

---

## 2. The five interpretations — tests

### A) Vacuum energy (w = −1)

| Test | Result |
|---|---|
| Is ΩΛ an energy density? | **NO** — it is a dimensionless fraction of two information quantities |
| Is w = −1 derived? | **NO** — w = −1 is a ΛCDM *hosted* reading, not a consequence of ΩΛ |
| QG230's own scaling | Λ ∝ 1/R² (NOT a constant) — ρ_Λ ∝ a^(−2) ⇒ **w = −1/3**, not w = −1 |
| q₀/z_acc forms | q₀ = Ωm/2 − ΩΛ and z_acc = (2ΩΛ/Ωm)^(1/3) − 1 **assume w = −1** (hosted) |

**A is NOT the derived ontology.** A constant vacuum energy (w = −1) is imported from
ΛCDM; AT's own Lambda scaling (Λ ∝ 1/R², QG230) translates to w = −1/3. The hosted
q₀/z_acc closures use w = −1, which is inconsistent with QG230's w = −1/3.

### B) Information content

| Test | Result |
|---|---|
| Literal definition | ΩΛ = I_occ/ln K **is** the normalized information density |
| I_occ > 0 meaning | top-heaviness of [4,4,87] is a *positive* departure from uniformity |

**B is CORRECT and literal** — ΩΛ *is* information content (the KL-excess as a fraction
of the state-space capacity). But "information" here is a counting measure, not an
energy density; no identification I → ρ_Λ (energy) is derived.

### C) Occupancy deficit

| Test | Result |
|---|---|
| Which side is the "deficit"? | QG195/196: **matter** is the deficit = Ωm = H/ln K |
| Is ΩΛ a deficit? | **NO** — ΩΛ is the *excess* (KL > 0), not a deficit |

**C is INVERTED.** The "deficit" maps to matter (Ωm), not dark energy. ΩΛ is the
top-heavy *excess* over uniformity.

### D) Actualization pressure

| Test | Result |
|---|---|
| QG230's phrase | "residual actualization pressure" |
| Is it a thermodynamic pressure? | **NO** — NP_030: canonical μ = 2 growth is a *population inversion* (anti-thermal); ρ₇/ρ₀ = 128 > 1 |

**D is METAPHORICAL.** "Pressure" is an analogy; the canonical branching is anti-thermal
(growth, not decay), so no physical (thermodynamic) pressure object exists.

### E) Cosmological bookkeeping only

| Test | Result |
|---|---|
| Is ΩΛ a bookkeeping identity? | **YES** — the pair (ΩΛ, Ωm) partitions ln K |
| Distinct from B? | **NO** — E *is* B (the information partition *is* the bookkeeping) |

**E is CORRECT and identical to B.** ΩΛ is the information-bookkeeping fraction.

### Determination

| Interpretation | Verdict |
|---|---|
| A) vacuum energy | **NOT DERIVED** (w = −1 hosted; QG230 implies w = −1/3) |
| **B) information content** | **YES — the literal ontology** |
| C) occupancy deficit | **INVERTED** (the deficit is matter, Ωm) |
| D) actualization pressure | **METAPHORICAL** (anti-thermal, NP_030) |
| **E) bookkeeping only** | **YES — identical to B** |

**B = E: the ontology of ΩΛ is the information-bookkeeping fraction — the realized
information density I_occ as a fraction of the state-space capacity ln K.** This is a
DERIVED, observed (0.12%) *number* — but it is NOT a physical energy density, pressure,
or equation-of-state object.

---

## 3. Do any interpretations predict additional observables?

| Observable | Formula | Status |
|---|---|---|
| ΩΛ | I_occ/ln K = 0.6839 | OBSERVED 0.12% |
| Ωm | 1 − ΩΛ = 0.3161 | OBSERVED 0.26% |
| ΩΛ/Ωm | I_occ/(ln K − I_occ) = 2.1633 | derived |
| q₀ | Ωm/2 − ΩΛ = −0.5258 | closure (hosted w = −1 form) |
| z_acc | (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295 | closure (hosted w = −1 form) |
| H₀, σ₈, BAO, growth, lensing | — | BOUNDARY (need non-information inputs) |

The information ontology predicts **exactly five observables** (the finite family of
QG_012/QG_017) and nothing else. q₀ and z_acc are DERIVED as *values* but their *form*
is hosted FRW/ΛCDM (w = −1). No amplitude/size observable is derived.

---

## 4. Compare with ΛCDM — w = −1

| Source | Implied equation of state | Derived from ΩΛ? |
|---|---|---|
| ΛCDM cosmological constant | w = −1 (ρ constant) | hosted |
| QG230 Λ ∝ 1/R² (ρ_Λ ∝ a^(−2)) | **w = −1/3** | **NO — and inconsistent with the hosted w = −1 closures** |
| legacy Λ(t) = α/√V(t) (X046/XD001) | w ≠ −1, w(z) ≈ −1 + 0.015·(1+z)^(3/2) | NO — coefficient 0.015 FITTED |

**The corpus carries THREE incompatible equations of state for dark energy** — w = −1
(hosted closures), w = −1/3 (QG230 scaling), and w ≠ −1 time-varying (legacy Λ(t)) — and
**none is derived from ΩΛ = I_occ/ln K itself**, which is a pure dimensionless number.
This is an unresolved internal tension: the constant-fraction result (ΩΛ = 0.6839,
time-independent) is inconsistent with the legacy time-varying Λ(t) prediction, and
QG230's Λ ∝ 1/R² is inconsistent with the w = −1 assumed by the hosted q₀/z_acc forms.

---

## 5. Future evolution / ΩΛ(z) / q₀ / z_acc

| Quantity | AT status |
|---|---|
| **future evolution** | NOT derived — ΩΛ is a *constant* bookkeeping fraction; the time evolution is hosted FRW |
| **ΩΛ(z)** | NOT derived — the information partition is time-independent; the ΩΛ(z) evolution is hosted ΛCDM |
| **q₀** | value DERIVED (−0.5258), form CORRESPONDENCE (hosted w = −1) |
| **z_acc** | value DERIVED (0.6295), form CORRESPONDENCE (hosted w = −1) |

AT predicts the **present-day** fraction ΩΛ = 0.6839 (a time-independent bookkeeping
number). It does NOT predict ΩΛ(z), future evolution, or a time-varying equation of
state — those are hosted content. The legacy time-varying Λ(t) prediction is therefore
**in tension with** the ResearchY constant-fraction derivation.

---

## Theorem

> **Theorem (NP_055).** AT contains a DERIVED ontology of Dark Energy ONLY as the
> information-bookkeeping fraction (interpretations B = E): ΩΛ = I_occ/ln K = 0.6839 is
> the realized information density (KL(ρ‖uniform) = 0.7513 nats over the top-heavy
> octave record [4,4,87]) as a fraction of the state-space capacity (ln K = ln 3 =
> 1.0986 nats). It does NOT contain a *physical* ontology: (A) vacuum energy with w = −1
> is hosted, not derived — QG230's own Λ ∝ 1/R² translates to w = −1/3; (C) the
> "occupancy deficit" is matter (Ωm = H/ln K), not dark energy; (D) "actualization
> pressure" is a metaphor (the canonical μ = 2 branching is anti-thermal, NP_030).
> Proof: (1) Verify the bookkeeping identity (Section 1, verified): ρ = [4,4,87]/95
> gives KL = 0.7513 = I_occ, H = 0.3473, I_occ + H = ln K = 1.0986, ΩΛ + Ωm = 1.
> (2) Test the interpretations (Section 2, verified): A fails (no energy density, no
> derived w = −1; Λ ∝ a^(−2) ⇒ w = −1/3); B and E hold (ΩΛ is literally the normalized
> information, and the partition IS the bookkeeping); C is inverted (the deficit is
> Ωm); D is anti-thermal (μ = 2 growth). (3) Additional observables (Section 3,
> verified): exactly the finite family {ΩΛ, Ωm, ratio, q₀, z_acc}; q₀/z_acc values
> DERIVED, forms hosted (w = −1). (4) Compare with ΛCDM (Section 4): three incompatible
> equations of state (w = −1 hosted, w = −1/3 QG230, w ≠ −1 legacy), none derived from
> ΩΛ. (5) Future/ΩΛ(z) (Section 5): the information fraction is time-independent, so
> future evolution and ΩΛ(z) are hosted, not derived. Classification: ΩΛ value and the
> information-bookkeeping ontology DERIVED (B = E); vacuum-energy / w = −1 ontology
> CORRESPONDENCE (hosted ΛCDM); occupancy-deficit reading REFUTED as the dark-energy
> ontology (it is matter); actualization-pressure reading EMERGENT-as-metaphor /
> REFUTED as physical (NP_030); legacy time-varying Λ(t) in TENSION with the constant
> fraction (coefficient FITTED). **Success criterion: PARTIAL — a DERIVED informational
> ontology exists, but no physical (energetic/equation-of-state) ontology is derived;
> ΩΛ is a successful numerical relation plus a bookkeeping reading, not a physical
> dark-energy mechanism.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Verify the partition. (2) Test A–E. (3) Enumerate observables.
> (4) Compare w = −1. (5) Test future/ΩΛ(z). ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ΩΛ is vacuum energy with w = −1" | no energy density is derived; QG230's Λ ∝ 1/R² gives w = −1/3, not −1; w = −1 enters only in the hosted q₀/z_acc forms |
| "ΩΛ is the occupancy deficit" | the deficit is Ωm = H/ln K (matter, QG195/196); ΩΛ is the *excess* (KL > 0) |
| "actualization pressure is a physical pressure" | μ = 2 branching is a population inversion (anti-thermal, NP_030); no thermodynamic pressure object exists |
| "information content is an energy density" | I_occ is a counting measure (KL), and no I → ρ_Λ (energy) identification is derived |
| "the ontology is independent of interpretation" | B and E are the same object (the partition); A/C/D are distinct failed/metaphorical readings |
| "AT derives ΩΛ(z) and future evolution" | the information fraction is time-independent; evolution is hosted FRW/ΛCDM |
| "the legacy Λ(t) and the constant ΩΛ are consistent" | Λ(t) = α/√V(t) predicts w ≠ −1 time-varying; ΩΛ = I_occ/ln K is a constant bookkeeping fraction — incompatible |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ΩΛ is the information-bookkeeping fraction | a measured ΩΛ deviating from I_occ/ln K beyond 0.12% |
| no physical (w = −1) ontology is derived | a derivation of w = −1 from {I_occ, ln K, ρ} alone (without hosting ΛCDM) |
| QG230's Λ ∝ 1/R² implies w = −1/3 | a derivation showing Λ ∝ 1/R² corresponds to w = −1 |
| the occupancy-deficit is matter | a reading where Ωm ≠ H/ln K (the realized-entropy fraction) |
| the branching is anti-thermal (not a pressure) | a canonical μ < 1 (decaying) branching from the canonical structure |
| ΩΛ(z) / future evolution are hosted | an ΩΛ(z) or future-evolution prediction derived from the info objects alone |
| the legacy Λ(t) is in tension | a reconciliation of time-varying Λ(t) with the constant fraction ΩΛ = 0.6839 |

---

## 8. Classification

| Component | Status |
|---|---|
| ΩΛ = I_occ/ln K = 0.6839 (the value) | **DERIVED** (QG234), OBSERVED 0.12% |
| the information-bookkeeping ontology (B = E) | **DERIVED** (the partition I_occ + H = ln K) |
| vacuum-energy / w = −1 ontology (A) | **CORRESPONDENCE** (hosted ΛCDM; QG230 implies w = −1/3) |
| occupancy-deficit reading (C) | **REFUTED** as dark-energy ontology (the deficit is matter, Ωm) |
| actualization-pressure reading (D) | **EMERGENT-as-metaphor / REFUTED** as physical (anti-thermal, NP_030) |
| legacy time-varying Λ(t) = α/√V(t) | **in TENSION** with the constant fraction (coefficient FITTED, BOUNDARY) |
| q₀ = −0.5258, z_acc = 0.6295 | value **DERIVED**, form **CORRESPONDENCE** (hosted w = −1) |
| ΩΛ(z), future evolution, H₀/σ₈/BAO/growth | **BOUNDARY** (hosted / non-information inputs) |

**Conclusion.** AT contains a DERIVED ontology of Dark Energy only as the
information-bookkeeping fraction (B = E): ΩΛ = I_occ/ln K = 0.6839 is the realized
information density as a fraction of the state-space capacity — a real, derived,
observed number. AT does NOT contain a *physical* ontology: no derived vacuum energy,
no derived equation of state (w = −1 is hosted; QG230's Λ ∝ 1/R² implies w = −1/3), no
derived dynamics (future evolution and ΩΛ(z) are hosted). The "pressure" (D) is a
metaphor and the "deficit" (C) is matter. The corpus additionally carries an unresolved
internal tension: the legacy time-varying Λ(t) = α/√V(t) prediction (w ≠ −1, coefficient
FITTED) is inconsistent with the ResearchY constant-fraction derivation. **Success
criterion: AT has a successful numerical relation plus a derived informational ontology,
but not a physical dark-energy ontology.** No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_055_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_055_InformationBookkeepingIdentity` | I_occ + H = ln K; ΩΛ + Ωm = 1 | ✅ |
| `Y_NP_055_InterpretationA_VacuumEnergy` | no w = −1 derived; Λ ∝ 1/R² ⇒ w = −1/3 | ✅ |
| `Y_NP_055_InterpretationB_InformationContent` | ΩΛ is literally I_occ/ln K | ✅ |
| `Y_NP_055_InterpretationC_OccupancyDeficit` | the deficit is Ωm, not ΩΛ | ✅ |
| `Y_NP_055_InterpretationD_ActualizationPressure` | μ = 2 anti-thermal (no pressure) | ✅ |
| `Y_NP_055_InterpretationE_Bookkeeping` | E = B (the partition is the bookkeeping) | ✅ |
| `Y_NP_055_AdditionalObservables` | finite family {ΩΛ, Ωm, ratio, q₀, z_acc} | ✅ |
| `Y_NP_055_CompareLCDM_w` | three incompatible w's, none derived | ✅ |
| `Y_NP_055_FutureEvolution` | ΩΛ(z)/future evolution hosted | ✅ |
| `Y_NP_055_Classification` | B=E DERIVED; A/C/D CORRESPONDENCE/REFUTED | ✅ |
| `Y_NP_055_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_055"`

---

## References

- AT-QG: QG234 (cosmological fractions ΩΛ = I_occ/ln K), QG230 (Lambda origin,
  Λ ∝ 1/R²), QG228 (I_occ = KL(ρ‖uniform) = 0.7513), QG227 (initial uniform state),
  QG195/196 (matter = deficit), QG194/QG216 (count density ρ).
- ResearchY: QG_018 (information-cosmology closure), QG_012/QG_017 (finite family,
  q₀/z_acc closures), NP_018/NP_019 (distinguishability/information cosmology),
  NP_027–NP_037 (Planck/blackbody/ħ/temperature/structure-thermodynamics/thermal-N/
  ensemble/Bose/DOS/3D/three), S_001 (synthesis).
- Legacy: X046 (Λ(t) = α/√V(t)), XD001/White Paper/Euclid/DESI (w(z) ≠ −1 prediction).
