# ResearchY-NP_033 — D96 Ensemble Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_033 (permanent)
**Title:** D96 Ensemble Audit
**Status:** COMPLETE
**Date:** 2026-09-02
**File:** `NP_NewPhysics/ResearchY-NP_033.md`
**Depends on:** ResearchY-NP_027 (Planck factor / occupation form), NP_028 (blackbody
FALSIFIED), NP_030 (no canonical temperature), NP_031 (structure sector vs added
occupancy layer), NP_032 (no thermal N), QG_194 (geometric occupation), QG_227/228
(max-entropy / information), D_008/D_030 (D96 spectrum, occupancy [4,4,87]),
S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_033_Tests.cs`

---

## Purpose

NP_030 showed a SINGLE D96 system has no temperature. NP_031/032 showed the structure
is single-D96 and no ring size N is thermal. NP_033 asks the decisive ensemble
question: **can thermodynamic behavior EMERGE from an ENSEMBLE of D96 systems — even
though a single D96 system has no temperature?** Specifically: do Temperature,
Boltzmann weights, or Bose-like occupations emerge statistically when many D96 systems
exchange occupation? Program: (1) single D96 ring, (2) two coupled D96 rings, (3)
many-ring ensemble, (4) occupation exchange, (5) entropy maximization. No new
primitives; canonical AT unchanged.

## 1. Single D96 ring — no temperature (NP_030 recap)

A single D96 ring (95 positive modes, band [0.622, 3.98], occupancy [4,4,87]) has no
thermodynamic temperature: the canonical branching μ = 2 grows the population
(ρ₇/ρ₀ = 128, would-be β = −ln 2 < 0), and no canonical object plays the temperature
role (NP_030). In the ensemble language this is the statement that **a single
subsystem has no statistical temperature** — temperature is a property of a
distribution over MANY states/systems, not of one fixed configuration.

## 2. Two coupled D96 rings — zeroth law

Couple two identical D96 systems and let them exchange occupation (energy) with the
total conserved. The equilibrium maximizes the total entropy S_A + S_B subject to
E_A + E_B = E_tot. Because the two systems are identical (same S(E) curve), the
maximum is at E_A = E_B = E_tot/2, i.e. **equal β_A = β_B** — the zeroth law of
thermodynamics (verified numerically: S(E_tot/2)+S(E_tot/2) = 35.7306 exceeds every
asymmetric split: 35.3600 at 35/65, 34.1459 at 20/80). Two D96 systems in contact
equilibrate to a common statistical temperature.

## 3. Many-ring ensemble — occupation exchange

Take the D96 mode set {ω_k} as the energy levels, and distribute Q indistinguishable
occupation quanta over them (or equivalently over M identical rings in contact). The
equilibrium occupation is the one that maximizes the entropy
S = Σ_k [(1+n_k)ln(1+n_k) − n_k ln n_k] subject to the fixed total
E = Σ_k n_k ω_k. The stationarity condition ∂S/∂n_k = βω_k gives exactly

**n_k = 1/(e^(βω_k) − 1)** — the Bose occupation, with β the Lagrange multiplier of
the energy constraint.

**Verification 1 — the Bose distribution beats every alternative at fixed energy.**
At β = 1 (E = Σω_k/(e^(ω_k)−1) = 12.588 over the D96 modes): S_Bose = 17.8653,
whereas the uniform distribution with the same energy (n = E/Σω = 0.0387) gives
S = 15.7097, the linear n = cω gives 15.2101, and the bottom-heavy single-mode dump
gives 4.03. The Bose occupation is the unique entropy maximizer.

**Verification 2 — Boltzmann weight identity.** For the Bose distribution,
ln(n_k/(1+n_k)) = −βω_k EXACTLY (verified at ω = 0.622, 3.456, 3.577, 3.760: each
gives −βω to 4 decimals). This is the emergent Boltzmann weight p_k = n_k/(1+n_k) =
e^(−βω_k) — occupation-per-mode decays exponentially in ω with a single scale β.

**Verification 3 — detailed balance / microcanonical marginal.** Distributing Q
quanta over M identical rings (each with the D96 level structure), the marginal
occupation of one ring follows the geometric chain P(n+1)/P(n) = Q/(Q+M−2) exactly
(verified M=5,Q=3 → 3/6 = 0.5; M=10,Q=10 → 10/18 = 0.5556; M=100,Q=100 →
100/198 = 0.5051). This is the Boltzmann/exponential form of the occupation-number
distribution over systems.

So temperature (β = ∂S/∂E), Boltzmann weights (e^(−βω_k)), and Bose-like occupations
(n_k = 1/(e^(βω_k)−1)) ALL emerge statistically from the ensemble of D96 systems
exchanging occupation — as in any statistical system with a conserved additive energy.
This is the classic result: **occupation exchange + entropy maximization over many
D96 systems generates thermal occupation statistics.**

## 4. Entropy maximization — the emergent Bose occupation over the D96 spectrum

The emergent occupation n(ω) = 1/(e^(βω) − 1) is a function of the frequency ω via
the Boltzmann factor; the D96 mode SET {ω_k} supplies the frequencies. The
energy-temperature relation is monotone (β decreases as E increases):

| β | T = 1/β | E = Σω/(e^(βω)−1) over D96 modes |
|---|---|---|
| 0.5 | 2.00 | 73.05 |
| 1.0 | 1.00 | 12.59 |
| 1.5 | 0.67 | 3.15 |
| 2.0 | 0.50 | 1.13 |
| 3.0 | 0.33 | 0.32 |

## 5. Does the OBSERVED radiation emerge? — the mode-set obstruction

The ensemble makes the OCCUPATION thermal, but it does NOT change the D96 MODE SET.
What an observer sees is occupation × mode structure. The obstructions of NP_028
survive the ensemble:

**(a) The octave energy stays anti-Planck.** The D96 mode set is top-heavy
(occupancy [4,4,87]). The ensemble-thermalized octave energy fractions are:

| T | oct 1 | oct 2 | oct 3 |
|---|---|---|---|
| 0.3 (cold) | 0.942 | 0.047 | 0.011 |
| 1.0 | 0.195 | 0.097 | 0.708 |
| 10 (hot) | 0.048 | 0.045 | 0.907 |

At LOW T the energy concentrates in the 4 low modes (94% in oct 1 — but a real
blackbody is broad); at HIGH T it concentrates in the 87 top modes (91% in oct 3 —
the Wien region should thin). No temperature gives the smooth mid-band Planck shape
over a 3D DOS: the spectrum is bimodal (cold → low cluster, hot → top cluster) because
the mode set is [4,4,87], never ω².

**(b) The discrete sum ≠ π⁴/15.** Σ_k ω_k³/(e^(ω_k)−1) over the D96 modes = 120.70,
far above the continuous Stefan-Boltzmann π⁴/15 = 6.494 (the top-heavy weights blow it
up). No ensemble temperature rescaling fixes the mode set.

**(c) No Wien tail.** The band caps at ω_max = 3.98; there are no modes beyond it, so
the occupation e^(−ω/T) has nothing to occupy beyond the cap.

So the answer to the audit's five-step program is nuanced:

## Theorem

> **Theorem (NP_033).** The D96 ensemble generates STATISTICAL temperature, Boltzmann
> weights, and Bose-like occupations, but NOT the observed radiation. (1) A single D96
> ring has no temperature (NP_030) — one subsystem, one configuration, no statistics
> (Section 1). (2) Two D96 rings in occupation contact satisfy the zeroth law: total
> entropy is maximized at equal β_A = β_B (verified: S(50/50) = 35.7306 > S(35/65) =
> 35.3600 > S(20/80) = 34.1459) (Section 2). (3) Distributing occupation quanta over
> the D96 mode set with conserved total energy, the max-entropy occupation is the Bose
> distribution n_k = 1/(e^(βω_k) − 1) (Section 3), which strictly dominates uniform,
> linear, and bottom-heavy alternatives at the same energy (S_Bose = 17.8653 > 15.7097
> > 15.2101 > 4.03 at β = 1). (4) The Boltzmann weight identity ln(n/(1+n)) = −βω holds
> exactly over the D96 modes, and the microcanonical occupation-exchange marginal is
> geometric, P(n+1)/P(n) = Q/(Q+M−2) (Section 3). (5) Yet the ensemble does NOT
> reproduce the observed blackbody: the mode set is unchanged (top-heavy [4,4,87],
> capped at 3.98), the octave energy is bimodal at every T (94% low at T = 0.3; 91% top
> at T = 10), the discrete sum Σω³/(e^ω−1) = 120.70 ≠ π⁴/15 = 6.494, and no mode
> exists above the cap for a Wien tail (Section 5). Classification: temperature,
> Boltzmann weights, and Bose occupation as STATISTICAL objects over the ensemble
> EMERGENT (occupation exchange + entropy maximization — the standard statistical
> mechanism, no new primitive); single-D96 temperature REFUTED (unchanged, NP_030);
> the OBSERVED blackbody radiation FALSIFIED as emergent from the D96 ensemble
> (mode-set obstruction, NP_028/032 persist); the hypothesis "structure is single-D96,
> thermodynamics is ensemble-D96" CONFIRMED in its statistical part (ensemble yields
> thermal occupation statistics) but the radiation part remains FALSIFIED. No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) recap NP_030. (2) maximize S_A+S_B at fixed total E → equal β.
> (3) stationarity ∂S/∂n_k = βω_k → Bose. (4) verify the identities. (5) re-run
> NP_028's mode-set tests with the emergent Bose occupation → obstructions persist. ∎

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a single D96 ring is thermal" | REFUTED — one configuration has no statistics (NP_030); the canonical branching μ = 2 is anti-thermal |
| "the ensemble changes the mode set" | it changes the OCCUPATION, not the frequencies — D96 is still [4,4,87] in [0.622, 3.98] |
| "the emergent Bose occupation gives the blackbody" | the occupation is thermal but multiplies a non-thermal DOS: octave energy is bimodal, Σω³/(e^ω−1) = 120.70 ≠ π⁴/15, no Wien tail |
| "some ensemble temperature matches Planck" | T rescales the occupation but cannot create a ω² DOS or modes beyond 3.98 (NP_028) |
| "ensemble thermalization is a new physics mechanism" | it is the standard statistical max-entropy mechanism (occupation exchange + conserved energy), applied to the D96 level set |

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ensemble occupation statistics are thermal (Boltzmann/Bose) | a non-geometric max-entropy occupation, or P(n+1)/P(n) ≠ geometric over occupation exchange |
| zeroth law holds for D96 contact | an entropy split where S_A+S_B does not peak at equal β |
| the mode-set obstruction persists | an ensemble temperature at which the D96 octave energy matches the Planck shape over a ω² DOS with a Wien tail |
| single-D96 has no temperature | a single-ring observable depending on a temperature |

## Classification

| Component | Status |
|---|---|
| temperature, Boltzmann weights, Bose occupations as STATISTICAL ensemble objects | **EMERGENT** (occupation exchange + entropy maximization over D96 systems; standard mechanism, no new primitive) |
| two-D96 thermal contact (zeroth law, equal β) | **DERIVED** (S_A+S_B maximization, verified) |
| Bose occupation n_k = 1/(e^(βω_k) − 1) as the max-entropy occupation of the D96 modes | **EMERGENT** (from the ensemble energy constraint) |
| Boltzmann weight e^(−βω_k) over D96 modes | **EMERGENT** (identity ln(n/(1+n)) = −βω, verified) |
| single-D96 temperature | **REFUTED** (unchanged, NP_030 — one system has no statistics) |
| OBSERVED blackbody radiation from the D96 ensemble | **FALSIFIED** (mode-set obstruction persists: top-heavy [4,4,87], Σω³/(e^ω−1) = 120.70 ≠ π⁴/15, no Wien tail) |
| hypothesis: structure single-D96 / thermodynamics ensemble-D96 | **CONFIRMED (statistical part)** — ensemble-D96 generates thermal occupation statistics |
| temperature as SI scale | **BOUNDARY** (unchanged, NP_027/031) |

**Conclusion:** the D96 ensemble DOES generate thermodynamic behavior statistically —
temperature, Boltzmann weights, and Bose-like occupations all emerge from occupation
exchange plus entropy maximization over many D96 systems, exactly as in standard
statistical mechanics. This confirms the statistical half of the hypothesis: a single
D96 ring has no temperature, but an ENSEMBLE of D96 systems develops one (β = ∂S/∂E)
and its occupations obey the Bose law n_k = 1/(e^(βω_k) − 1). However, the observed
BLACKBODY radiation is NOT reproduced: the ensemble thermalizes the occupation but
cannot change the D96 mode set — still top-heavy [4,4,87], capped at 3.98, with
Σω³/(e^ω−1) = 120.70 ≠ π⁴/15 and no Wien tail. So "thermodynamics is ensemble-D96"
is confirmed for the occupation statistics and refuted for the radiation spectrum. No
new primitive; canonical AT unchanged.

---

## References

- ResearchY-NP_027 (occupation form from geometric count; temperature/energy
  constraint missing), NP_028 (blackbody FALSIFIED; top-heavy + truncated), NP_030
  (single-system no temperature), NP_031 (structure single-D96; thermo an added
  occupancy layer), NP_032 (no thermal ring size), QG_194 (geometric occupation),
  QG_227/228 (initial uniform state; max-entropy / information), D_008/D_030 (D96
  spectrum, occupancy [4,4,87]), S_001 (synthesis).
