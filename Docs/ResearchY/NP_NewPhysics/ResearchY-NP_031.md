# ResearchY-NP_031 — Structure vs Thermodynamics Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_031 (permanent)
**Title:** Structure vs Thermodynamics Audit
**Status:** COMPLETE
**Date:** 2026-09-02
**File:** `NP_NewPhysics/ResearchY-NP_031.md`
**Depends on:** ResearchY-NP_027 (Planck factor form / full law), NP_028 (blackbody
FALSIFIED), NP_029 (ħ BOUNDARY), NP_030 (no canonical temperature), A_003 (branching
μ=2, gens=8), QG_194 (geometric occupation), QG_228 (information), QG_234 (ΩΛ =
I_occ/ln K), M_004 (entropy log₂95), QG_184/208 (Hawking T ∝ 1/R), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_031_Tests.cs`

---

## Purpose

The NP_027–NP_030 chain produced a consistent picture: the Planck occupation FORM is
emergent from the geometric count (NP_027), the blackbody radiation is FALSIFIED as an
emergent read of D96 (NP_028), ħ is a unit bridge (NP_029), and NO canonical object
plays the thermodynamic-temperature role (NP_030). NP_031 asks the architectural
question these results raise: **does D96 belong EXCLUSIVELY to the structural layer,
while thermodynamics belongs to a SEPARATE occupancy layer?** Concretely, does AT
naturally split into a Structure Sector (Difference → Actualization → Spectrum) and a
Thermodynamic Sector (Occupations → Temperature → Radiation)? No new primitives;
canonical AT unchanged.

## 1. Inventory — every D96-derived object (the structural layer)

The structural layer is the complete set of objects derived from
Difference → Actualization → Spectrum (with the two anchors v, m_e), each requiring
NO temperature input:

| Structural object | Value | Source |
|---|---|---|
| spectrum ω_k = √λ_k | 95 positive modes, band [0.622, 3.98], span 6.40, ω₁ = 0.6216 | D_008/D_030 |
| octave occupancy | [4, 4, 87] | A_003/D_030 |
| spectral moments | Σ√m = 64.0825, Σm² = 229, occMom = 1900.25 | D_chain |
| information density | I_occ = KL(ρ‖uniform) = 0.7513 nats | QG_228 |
| cosmological fractions | ΩΛ = I_occ/ln K = 0.6839, Ωm = 0.3161 | QG_234 |
| u-quark mass | m_u = m_e·Σ√m/√Σm² = 2.164 MeV | QG_173 |
| Planck scale | M_Pl = v·A³ = 1.2234e19 GeV | QG_181 |
| gauge couplings | α_weak = 3/Σm, α_strong = 8/Σ√m | D_012 |
| state-count entropy | H = log₂(95) = 6.57 bits | M_004 |
| branching (geometric occupation) | μ = 2, gens = 8, S = Σ2^k = 255 | A_003/QG_194 |

Every row is a pure number of the count structure — none references a temperature, a
Boltzmann constant, or a radiation law. (Verified: the ResearchY derivation chain
contains zero SI thermal constants.)

## 2. Inventory — every thermodynamic object

| Thermodynamic object | Status in AT (after NP_027–030) |
|---|---|
| temperature scale T | BOUNDARY — no canonical object plays this role (NP_030) |
| thermal occupation law n(x) = 1/(e^x − 1) | FORM EMERGENT from geometric occupation (NP_027); canonical μ=2 is anti-thermal |
| blackbody / radiation spectrum | FALSIFIED as emergent from D96 (NP_028) — hosted content |
| Stefan-Boltzmann U ∝ T⁴ (π⁴/15) | NOT REPRODUCED from finite spectrum (NP_027) |
| Wien exponential tail / displacement | FALSIFIED (hard cutoff, no tail) (NP_027/028) |
| energy↔frequency conversion ħ | BOUNDARY unit bridge (NP_029) |

## 3. Identify overlap

The overlap between the two inventories is exactly **one object: the occupation
statistics ρ_k = μ^k/S** — the geometric count distribution. It appears on both sides:

- **Structural reading:** ρ_k is the branching count over generations (μ=2, S=255,
  A_003), the source of the octave occupancy [4,4,87] and of I_occ = KL(ρ‖uniform)
  (QG_228/234). As a count it is DERIVED.
- **Thermodynamic reading:** the SAME geometric form, when read as a per-mode
  occupation with a DECAYING rate μ<1, produces the Planck factor
  ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1) (QG_194/NP_027) — the FORM of the Bose occupation law.

Nothing else overlaps: the structural masses/couplings/fractions/entropy have no
thermal twin, and the temperature/radiation objects have no structural twin. The
occupation statistics is the bridge — and NP_030 showed that the canonical direction
of that bridge (μ=2 growth) is ANTI-thermal, while the thermal direction (μ<1 decay)
is a free parameter, not fixed by the structure.

## 4. Test — does any thermal observable derive from structure alone?

**No.** The NP_027–030 results, re-tested here:

1. **State-count entropy DOES derive** (H = log₂ 95 = 6.57 bits, M_004) — but this is
   a structural count (Shannon entropy of the mode set), not a thermal function of T.
2. **The occupation FORM derives** (n = 1/(e^x−1) from geometric occupation, NP_027) —
   but only as a FORM with a free decay rate μ<1; the canonical branching μ=2 gives a
   negative occupation (population inversion, NP_030).
3. **No radiation observable derives.** Stefan-Boltzmann T⁴ needs the continuous
   integral π⁴/15 (discrete 95-mode sum ≠ π⁴/15, NP_027); the Wien tail needs modes
   beyond ω_max (none exist, NP_027/028); the blackbody DOS needs ω² (D96 is
   sub-power-law ~ω^1.5, NP_028).

So the honest answer: structure alone yields the occupation FORM and the state
entropy, but NEVER a temperature-carrying radiation observable. Every thermal
*observable* (a spectrum at temperature T) is REFUTED as a structure-only derivation.

## 5. Test — can thermodynamics be added only as a state-occupation law?

**Yes.** The only route from the D96 structure to thermal content is:

1. take the DERIVED mode set {ω_k} (structural),
2. assign each mode an occupation n(ω_k) = 1/(e^{ℏω_k/kT} − 1) (a state-occupation
   law over the derived modes),
3. supply the temperature scale T (BOUNDARY import, NP_027/028/030).

The structural layer provides the substrate (the modes and their geometric count
statistics, which fix the occupation FORM); it does NOT provide the temperature scale
that makes the occupation a function of T. Equivalently: AT's own count structure
provides the shape n(x) = 1/(e^x − 1); only the boundary parameter T converts the
shape into a thermodynamic state. There is no second derivation engine — thermal
content is added as an occupation of the structural modes.

## Theorem

> **Theorem (NP_031).** AT splits into exactly one DERIVED sector and one ADDED
> occupancy layer, and the split is that structure is closed while thermodynamics is a
> state-occupation over it. (1) The Structure Sector — Difference → Actualization →
> Spectrum and every structural observable (spectrum, occupancy [4,4,87], moments,
> I_occ = 0.7513, ΩΛ = 0.6839, masses m_u = 2.164 MeV, M_Pl = 1.2234e19 GeV,
> couplings, entropy H = log₂ 95 = 6.57 bits) — is DERIVED with no thermal input, and
> the derivation chain contains no SI thermal constant (Section 1). (2) The
> Thermodynamic objects (T, radiation, T⁴, Wien tail) are NOT reproduced from the
> structure: T is BOUNDARY (NP_030), radiation is FALSIFIED (NP_027/028), and ħ is a
> unit bridge (NP_029) (Section 2). (3) The overlap is exactly the occupation
> statistics ρ_k = μ^k/S — structural as a count, thermal as a FORM when read with a
> free decay rate μ<1 (Section 3). (4) No thermal OBSERVABLE derives from structure
> alone: state entropy and the occupation FORM derive, but no temperature-carrying
> spectrum does (Section 4). (5) Thermal content can be added ONLY as a state-occupation
> law over the derived modes, with the temperature scale supplied as a BOUNDARY
> parameter (Section 5). Classification: Structure Sector DERIVED (self-contained);
> Thermodynamic Sector as an autonomous primitive sector REFUTED (no separate
> primitive, no derived temperature); the two-sector split as architecture DERIVED
> (structure closes without temperature; thermodynamic content is the occupation layer
> over it, scale BOUNDARY); the overlap object (occupation statistics) DERIVED as a
> count, EMERGENT as the occupation-law FORM; temperature BOUNDARY; radiation FALSIFIED
> as emergent, hosted. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) enumerate the closed structural set. (2) enumerate the thermal
> objects and their NP_027–030 status. (3) identify ρ_k = μ^k/S as the sole overlap.
> (4) show only entropy + occupation FORM derive. (5) construct thermal content as
> occupation over modes + boundary T. ∎

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The two sectors are two autonomous DERIVED pillars" | thermodynamics has no derived temperature (NP_030), no derived radiation (NP_028), no derived T⁴ (NP_027) — it is an added layer, not a peer sector |
| "Structure alone yields a radiation spectrum" | blackbody FALSIFIED (NP_028), no Wien tail, DOS not ω² — only the occupation FORM derives |
| "D96's canonical count IS the thermal occupation" | canonical μ=2 is anti-thermal (growth, NP_030); the thermal occupation needs the free decay μ<1 |
| "Temperature is a structural object" | no canonical candidate plays the temperature role (NP_030); T is the BOUNDARY scale in x = ℏω/kT |
| "I_occ / entropy provide the temperature" | I_occ = 0.7513 is a fixed order parameter (→ ΩΛ = 0.6839); H = 6.57 bits is a state count — neither is a scale that sets an occupation shape (NP_030) |
| "Radiation is hosted by the same engine as the masses" | masses derive as anchor × dimensionless ratio (QG173); radiation needs the occupation law + T — two different mechanisms |

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| structure sector is closed without temperature | a structural derivation that needs a Boltzmann constant or T to complete |
| no thermal observable from structure alone | a canonical D96 object that produces a T-dependent spectrum without an added occupation law |
| thermodynamics is added as state-occupation + boundary T | a derivation of T from {Difference, η, spectrum} alone (NP_030 falsification, still open) |
| occupation statistics is the sole overlap | a second structural object that appears in a thermal law |
| radiation is hosted, not emergent | a D96-derived DOS ω² with a Wien tail over an unbounded band |

## Classification

| Component | Status |
|---|---|
| Structure Sector (Difference → Actualization → Spectrum + structural observables) | **DERIVED** (self-contained, no thermal input) |
| thermodynamic sector as an autonomous second sector | **REFUTED** (no derived temperature, radiation, or T⁴) |
| two-sector split as architecture (structure derived; thermo added as occupation) | **DERIVED** (confirmed by NP_027–030) |
| occupation statistics ρ_k = μ^k/S as count | **DERIVED** (A_003/QG_194) |
| Planck-factor occupation FORM n = 1/(e^x − 1) | **EMERGENT** from geometric occupation (NP_027) — needs free decay μ<1 |
| state-count entropy H = log₂ 95 = 6.57 bits | **DERIVED** (M_004) — a count, not a function of T |
| information density I_occ = 0.7513 → ΩΛ | **DERIVED** (order parameter, QG_228/234 — NOT temperature) |
| temperature scale T (x = ℏω/kT) | **BOUNDARY** (unchanged, NP_027/028/030) |
| radiation / blackbody / T⁴ / Wien tail | **FALSIFIED** as emergent from D96 — hosted content (NP_027/028) |

**Conclusion:** the NP_027–NP_030 results do indicate a genuine two-layer architecture,
but it is NOT two autonomous sectors. AT has ONE closed DERIVED structural layer
(Difference → Actualization → Spectrum → every structural observable, none needing a
temperature), and thermodynamics exists only as an ADDED state-occupation law over the
structural modes: the structure supplies the mode set and the occupation FORM (from its
own geometric count statistics ρ_k = μ^k/S), while the temperature scale that turns the
form into a thermal state is a BOUNDARY parameter. No thermal observable derives from
structure alone (REFUTED), and the only way thermal content enters is as an occupation
of the derived modes (CONFIRMED). The split itself is therefore a DERIVED architectural
fact — not a division into two primitive pillars. No new primitive; canonical AT
unchanged.

---

## References

- ResearchY-A_003 (branching μ=2, gens=8), D_008/D_030 (spectrum, octave occupancy),
  QG_194 (geometric occupation ρ_k = μ^k/S), QG_228 (I_occ = KL(ρ‖uniform) = 0.7513),
  QG_234 (ΩΛ = I_occ/ln K = 0.6839), M_004 (entropy log₂ 95 = 6.57 bits), QG_173/181
  (masses, Planck scale), D_012 (couplings), NP_027 (occupation FORM emergent,
  temperature BOUNDARY, full law not reproduced), NP_028 (blackbody FALSIFIED), NP_029
  (ħ BOUNDARY), NP_030 (no canonical temperature), QG_184/208 (Hawking T ∝ 1/R — a
  ρ-sector first-law conjugate, coefficient BOUNDARY, not a mode-occupation
  temperature), S_001 (synthesis).
