# ResearchY-NP_030 — Temperature Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_030 (permanent)
**Title:** Temperature Origin Audit
**Status:** COMPLETE
**Date:** 2026-09-02
**File:** `NP_NewPhysics/ResearchY-NP_030.md`
**Depends on:** ResearchY-NP_027/028 (Planck factor form / blackbody), A_003
(branching: Mu = 2, GenerationCount = 8), QG_194 (geometric occupation), QG_227
(initial uniform state), QG_228 (information content), QG_234 (ΩΛ = I_occ/ln K),
QG_184/185/196/208 (Hawking T ∝ 1/R and the 2π Bekenstein boundary), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_030_Tests.cs`

---

## Purpose

**What object in AT plays the role of thermodynamic temperature?** The canonical
theory derives masses, couplings, and cosmological fractions from the D96 counting
measure, but a *thermal* occupation law n(ω) = 1/(e^(ℏω/kT) − 1) requires a scale —
the temperature — that fixes the shape of an exponentially decaying occupation.
NP_027 already showed the Planck *factor form* n = 1/(e^x − 1) is emergent from the
geometric occupation (⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1), QG194) while the temperature in
x = ℏω/kT is a BOUNDARY import (not a canonical primitive). NP_028 then showed that no
choice of θ rescues a blackbody from the D96 spectrum. NP_030 asks the sharper
question from the other side: **is there ANY canonical AT object that plays the
thermodynamic-temperature role — that is, an object that (a) sets an occupation scale
and (b) generates a mode-occupation law?**

Program: test the four natural candidates (actualization density, occupancy disorder,
information density, spectral crowding) and ask whether any of them generates a
thermal mode-occupation law. No new primitives; canonical AT unchanged.

## 1. Candidate 1 — Actualization density ρ_k = μ^k/S

The canonical branching (A_003) is **growth**: Mu = 2.0, GenerationCount = 8, with
ρ_{k+1} = Mu·ρ_k. Normalized over 8 generations S = Σ2^k = 255:

| k | ρ_k | ρ_k/ρ_0 |
|---|---|---|
| 0 | 1/255 = 0.003922 | 1 |
| 1 | 2/255 = 0.007843 | 2 |
| 2 | 4/255 = 0.015686 | 4 |
| 3 | 8/255 = 0.031373 | 8 |
| 4 | 16/255 = 0.062745 | 16 |
| 5 | 32/255 = 0.125490 | 32 |
| 6 | 64/255 = 0.250980 | 64 |
| 7 | 128/255 = 0.501961 | **128** |

The occupation **grows** with the generation index: ρ_7/ρ_0 = 128. A thermal
(Boltzmann/Planck) occupation must **decay** with energy: ρ ∝ e^(−βE). Reading the
canonical sequence as a would-be thermal occupation of an equally-spaced ladder gives a
log-ratio ln(ρ_{k+1}/ρ_k) = ln μ = **+0.6931 > 0** per step — i.e. a **negative
inverse temperature** β = ln(1/μ) = −0.6931 < 0. Canonical actualization density is
**anti-thermal**: it is a *population inversion*, the opposite of a thermal
occupation. It cannot be the temperature source (it would need μ < 1 — decay — which
is the *free* parameter of NP_027, not the canonical branching).

## 2. Candidate 2 — Occupancy disorder

The D96 spectral record occupies modes in octaves as [4, 4, 87] (A_003 / D_030):
4 modes in the first octave, 4 in the second, **87 in the third**. The occupancy
*rises* toward high ω: 87/4 = **21.75×** from the middle octave to the top octave.
Occupancy disorder is therefore not a "disordering heat" either — the realized D96
occupancy is **top-heavy** (already NP_028: 93.7% of modes above band mid, 87.4% in
the top 20% of the band). If occupancy were thermal, the *higher* octave (higher ω)
would be *less* occupied, not 21.75× more. Candidate 2 fails as a temperature object:
it points the wrong way.

## 3. Candidate 3 — Information density I_occ

The realized information density is I_occ = KL(ρ‖uniform) over the octave record
(QG_228) = 0.7513 nats, and it *is* a genuine derived order parameter: ΩΛ =
I_occ/ln K = 0.6839 (QG_234). But an order parameter is a *scalar measure of
departure from uniformity*, not a temperature scale:

- **No scale.** I_occ is a pure number fixed by the D96 record (0.7513); a
  temperature must be a *continuously variable* scale parameter that sets the balance
  between occupation levels. Nothing in AT varies I_occ to produce a family of
  occupations.
- **No occupation law.** Information density measures *how non-uniform the realized
  occupancy is*; it does not *generate* an occupation law n(ω) over modes. The
  QG_228/234 chain derives ΩΛ from I_occ, not a Bose/Einstein occupation from a
  temperature.

Candidate 3 is DERIVED but as *order/information content*, not as temperature. It is
an anti-thermal quantity in the same direction as the occupancy: I_occ > 0 precisely
*because* the occupancy is top-heavy, not uniform.

## 4. Candidate 4 — Spectral crowding

Spectral crowding — the mode density per unit ω — also **rises into the top of the
band** (NP_028, verified): 0 modes in [3.0, 3.1), 6 in [3.3, 3.4), 6 in [3.9, 4.0);
83 of 95 modes lie above ω = 3.3 (the top 20% of the band [0.622, 3.98]). Crowding is
the same top-heavy fact as Candidate 2 phrased as a density. A thermal spectrum
*thins out* at high ω (Wien e^(−ω/θ)); D96 *crowds* there. Candidate 4 fails for the
same reason as Candidates 1 and 2: it is anti-thermal.

## 5. Does any candidate generate a mode-occupation law?

The thermal occupation law is n(x) = 1/(e^x − 1). NP_027 showed this form IS the mean
of a geometric occupation with a *decaying* rate: ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1) with
**μ < 1**. The test is decisive:

- Canonical μ = 2 (A_003, growth): the same formula gives n_k = 1/(2^(−k) − 1) < 0
  for every k ≥ 1 — a *negative* occupation, i.e. population inversion. The canonical
  branching does NOT produce a Bose occupation.
- A Bose occupation requires μ < 1. But μ < 1 is exactly the free parameter NP_027
  needed to obtain the Planck form, and it is NOT fixed by canonical AT (canonical μ
  is 2, the branching count).
- None of Candidates 1–4 carries a decaying occupation: actualization density grows
  (μ = 2), occupancy [4,4,87] grows, information density is a fixed scalar, crowding
  grows.

**No canonical AT object generates a mode-occupation law.** The Bose law requires an
imposed decay rate μ < 1 (free) *and* a temperature scale x = ℏω/kT (import) — both
are NP_027 BOUNDARY. (The only derived "temperature-like" object in AT is the
gravitational first-law temperature T ∝ 1/R from QG_184/208 — surface gravity of the
ρ-sector metric — whose *form* is derived but whose coefficient needs the imported 2π
of the Bekenstein quarter, QG_185/196. That is a horizon first-law relation, not a
mode-occupation generator, and it does not supply a statistical temperature for the
mode spectrum.)

## Theorem

> **Theorem (NP_030).** No object in canonical AT plays the role of thermodynamic
> temperature. (1) Actualization density ρ_k = μ^k/S with the canonical branching
> μ = 2 grows across generations (ρ_7/ρ_0 = 128, S = 255) — a population inversion,
> with would-be log-ratio ln μ = +0.6931 > 0 per step; a thermal occupation must decay
> (Section 1). (2) Occupancy disorder is the top-heavy octave record [4, 4, 87]
> (occupancy rises 21.75× into the top octave), the anti-thermal direction
> (Section 2). (3) Information density I_occ = KL(ρ‖uniform) = 0.7513 nats is a
> derived *order parameter* (ΩΛ = I_occ/ln K = 0.6839, QG_234) but is a fixed scalar,
> not a variable scale, and it measures non-uniformity rather than generating an
> occupation law (Section 3). (4) Spectral crowding rises into the band top (83/95
> modes above ω = 3.3; density grows into the cutoff) — again anti-thermal
> (Section 4). (5) The Bose occupation n = 1/(e^x − 1) requires a *decaying* geometric
> rate μ < 1; the canonical rate μ = 2 gives n_k < 0 (inversion), and no candidate
> supplies the decaying occupation or the scale (Section 5). Classification:
> temperature as a derived object of AT REFUTED (no canonical candidate generates a
> thermal mode-occupation law — every candidate is anti-thermal or an order-only
> scalar); temperature as the import scale in x = ℏω/kT BOUNDARY (unchanged,
> NP_027/028); actualization density and information density remain DERIVED in their
> own right (branching count, order parameter) but are NOT temperatures. No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1)–(4) evaluate each candidate and show growth/top-heaviness or
> scale-freeness. (5) substitutes μ = 2 into the NP_027 occupation formula and shows
> the canonical branching cannot yield a Bose occupation. ∎

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Actualization density is the temperature" | canonical μ = 2 grows (ρ_7/ρ_0 = 128); a thermal occupation decays; the would-be β = ln(1/μ) = −0.693 < 0 is negative (inversion) |
| "Occupancy disorder is the temperature" | the disorder is top-heavy ([4,4,87]; 21.75× rise into the top octave) — anti-thermal, not a disordering heat |
| "I_occ is the temperature" | I_occ = 0.7513 is a fixed order parameter (ΩΛ = I_occ/ln K = 0.6839, QG_234), not a variable scale, and it generates no occupation law |
| "Spectral crowding is the temperature" | crowding *rises into* the band top (83/95 modes above 3.3); a thermal spectrum thins out there |
| "The canonical branching gives the Planck law" | with μ = 2 the occupation n_k = 1/(2^(−k) − 1) is negative — population inversion, not Bose statistics; the Planck form needs the free μ < 1 of NP_027 |
| "Hawking T ∝ 1/R supplies thermodynamic temperature" | it is a horizon first-law (surface-gravity) temperature whose coefficient needs the imported 2π (QG_185/196); it generates no mode-occupation law |

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| no canonical object is the temperature | a canonical AT quantity that (a) varies continuously, (b) is fixed by the counting structure, and (c) generates a decaying occupation n ∝ e^(−E/T) with a fixed scale |
| canonical branching is anti-thermal | a derivation fixing μ < 1 (decay) from canonical structure rather than importing it |
| I_occ is order, not temperature | an occupation law n(ω) written as a function of I_occ alone that reproduces a thermal family |
| the mode spectrum has no thermal occupation | a canonical per-mode occupation decaying with ω (Wien tail) derived without the free μ < 1 |

## Classification

| Component | Status |
|---|---|
| temperature as a derived object of AT | **REFUTED** (no canonical candidate generates a thermal mode-occupation law) |
| actualization density ρ_k = μ^k/S (μ = 2) as temperature source | **REFUTED** (growth = population inversion; would-be β = −ln 2 < 0) |
| occupancy disorder ([4,4,87] octave record) as temperature | **REFUTED** (top-heavy: occupancy grows 21.75× into the top octave) |
| information density I_occ = 0.7513 as temperature | **REFUTED** as temperature — DERIVED as an order parameter (ΩΛ = 0.6839, QG_234) |
| spectral crowding as temperature | **REFUTED** (crowding rises into the cutoff — anti-thermal) |
| thermal (Bose) occupation law from canonical structure | **NOT GENERATED** — canonical μ = 2 gives negative occupation; needs free μ < 1 (NP_027) |
| temperature as the import scale x = ℏω/kT | **BOUNDARY** (unchanged, NP_027/028) |
| Hawking T ∝ 1/R (QG_184/208) | **DERIVED in form** (ρ-sector first law) — coefficient BOUNDARY (2π, QG_185/196); not a mode-occupation temperature |

**Conclusion:** temperature is not DERIVED and not EMERGENT in AT. Every canonical
candidate that could plausibly play the thermodynamic role — actualization density,
occupancy disorder, information density, spectral crowding — is either anti-thermal
(growth/top-heavy: μ = 2 branching, [4,4,87] occupancy, crowding into the cutoff) or a
fixed order-only scalar (I_occ = 0.7513, which derives ΩΛ = 0.6839 but no occupation
law). The Bose occupation n = 1/(e^x − 1) needs a decaying rate μ < 1 that canonical
AT does not fix (NP_027's free parameter) plus the temperature scale x = ℏω/kT
(BOUNDARY import). Temperature therefore remains exactly what NP_027/028 classified
it: a BOUNDARY import used to compare AT's derived frequency ratios to measured
thermal spectra — never a canonical object that generates a mode-occupation law. No
new primitive; canonical AT unchanged.

---

## References

- ResearchY-A_003 (canonical branching: Mu = 2, GenerationCount = 8), D_030 (octave
  structure; occupancy [4,4,87]), QG_194 (geometric occupation ρ_k = μ^k/S),
  QG_227/228 (uniform initial state; information I = ln K − H = KL(ρ‖uniform) =
  0.7513), QG_234 (ΩΛ = I_occ/ln K = 0.6839), NP_027 (Planck factor form emergent;
  temperature in x = ℏω/kT BOUNDARY; free μ), NP_028 (blackbody; temperature scale θ
  BOUNDARY; top-heavy + truncated spectrum), QG_184/208 (Hawking T ∝ 1/R from the
  ρ-sector first law), QG_185/196 (Bekenstein 1/4 coefficient requires imported 2π),
  S_001 (synthesis).
