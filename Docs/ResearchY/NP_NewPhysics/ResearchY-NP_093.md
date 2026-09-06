# ResearchY-NP_093 — Actualization Selection Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_093 (permanent)
**Title:** Actualization Selection Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_093.md`
**Depends on:** ResearchY-NP_090 (network ontology), NP_092 (network propagation),
ResearchY-M_001 (measurement = state selection), M_002 (phase-pinning), M_003 (feedback:
deterministic phase), M_005 (information conservation), M_009 (phase lattice), AT-QG QG216
(Born rule = count conservation), QG220 (phase θ = 2πk/N), QG227 (initial conditions ρ_k = 1/K),
QG228 (information I_occ), ResearchY-D_041 (tick time-parameter / phase advance)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_093_Tests.cs`

---

## Purpose

NP_092 established that **nothing travels** — the network is static, and the only genuine
movement is the actualization tick. That raises the immediate follow-up: **if nothing travels,
what determines WHICH node actualizes at the next tick?** Program: (1) define the actualization
event; (2) analyze competing resonance maxima; (3) test selection rules; (4) test probabilistic
vs deterministic choice; (5) compare against Born statistics, the M_001 measurement, and the
double-slit buildup. **Determine:** A) deterministic selector, B) probabilistic selector,
C) information-weighted selector, or D) boundary import. **Success criterion:** the node-selection
law behind actualization. No new primitives; canonical AT unchanged.

---

## 1. Define the actualization event

| Term | Definition | Source |
|---|---|---|
| **actualization event** | one tick of Actualization — a COUNT REALIZATION: one count is realized on one node | QG216/QG222 |
| **tick** | the discrete advance (Δθ = 2πk/N), producing exactly ONE outcome | D_041/QG_011 |
| **count ρ_k** | the normalized share of mode k, ρ_k = \|ψ_k\|², Σρ = 1 EXACT | QG216 |
| **phase θ_k** | the circulation label θ = 2πk/N (the pairing DOF) | QG220 |
| **selection** | the realization of one outcome with Born weight ρ_k | M_001 |

An actualization event is **not** a signal travelling to a node (NP_092: nothing travels). It is
the realization of one count, on one node, drawn from the conserved occupancy distribution ρ.

---

## 2. Competing resonance maxima — how is the winner chosen?

Consider two competing modes A and B with occupancies ρ_A, ρ_B (ρ_A + ρ_B = 1). When an
actualization fires, which node realizes the count?

- **A deterministic "maximum wins" rule** (node with larger ρ always fires) would be a
  deterministic selector — it is **NOT** what AT says.
- **The Born rule** is what AT says: node k fires with probability ρ_k. The higher-occupancy node
  wins **more often**, proportionally to ρ — not always.

| ρ_A | ρ_B | P(A fires) | P(B fires) | (n = 10000) expected counts |
|---|---|---|---|---|
| 0.25 | 0.75 | 0.25 | 0.75 | 2500 / 7500 |

The competing maxima resolve **statistically**, by Born weight — the interference cross-term
I = ρ_A + ρ_B + 2√(ρ_Aρ_B)·cos(θ_A − θ_B) (QG216) is the ensemble pattern, not a per-event rule.

---

## 3. Test selection rules

| Rule | Test | Verdict |
|---|---|---|
| "the maximum ρ wins deterministically" | would give P = 1 to the larger mode always | **REFUTED** — Born gives P = ρ, not 0/1 |
| "uniform random among modes" | would give P = 1/K always | **REFUTED** — Born weights by ρ, not uniformly |
| "Born weight P(k) = ρ_k" | P(k) ∝ \|ψ_k\|², Σρ = 1 | **YES** — the canonical rule (QG216) |
| "the phase selects the node" | θ = 2πk/N is the pairing label, not a selector | **REFUTED** — phase evolves deterministically (D_041); it does not pick the outcome |

**The only rule that survives is the Born rule: P(k) = ρ_k = |ψ_k|².**

---

## 4. Probabilistic vs deterministic choice — the two DOFs

Actualization carries exactly TWO real degrees of freedom (D_036), and they select differently:

| DOF | Evolution | Does it select the node? | Source |
|---|---|---|---|
| **phase θ** | **DETERMINISTIC** — θ_t = θ₀ + t·Δθ, Δθ = 2πk/N per tick | **NO** — it fixes the coherence/pairing, not the outcome | D_041/M_003 |
| **count ρ** | **PROBABILISTIC** — P(k) = ρ_k = \|ψ_k\|² (Born) | **YES** — this IS the which-node selection | QG216/M_001 |

**The phase is the deterministic DOF; the count is the probabilistic DOF.** The node-selection
law is the Born rule on the count — the phase advance (D_041) is a *separate*, deterministic
evolution that does not determine which node actualizes.

---

## 5. Compare against Born statistics, M_001, double-slit buildup

| Reference | What it says | AT's node-selection law |
|---|---|---|
| **Born statistics (QG216)** | Σ\|ψ\|² = 1 EXACT; P(k) = \|ψ_k\|² | **EXACT MATCH** — the selection IS the Born rule |
| **M_001 measurement** | one outcome selected with weight ρ (state selection) | **EXACT MATCH** — measurement = a Born-weighted actualization |
| **double-slit buildup** | many events accumulate into the interference pattern | **EXACT MATCH** — the buildup is the ensemble of Born selections: each event picks one node with weight ρ; the pattern I = ρ_A+ρ_B+2√(ρ_Aρ_B)cos Δθ emerges statistically |

The node-selection law is the **same object** in all three readings: a count realization drawn
with probability ρ = |ψ|². The single event is Born-stochastic; the pattern (interference) is the
**statistical** accumulation of many such events — exactly the double-slit buildup.

---

## 6. A / B / C / D

| Determination | Verdict |
|---|---|
| **A) deterministic selector** | **REFUTED.** There is no deterministic rule for which node fires; the phase (D_041) is deterministic but does not select the outcome. |
| **B) probabilistic selector** | **YES — the answer.** Node k actualizes with probability ρ_k = \|ψ_k\|² (the Born rule), DERIVED from count conservation (QG216). |
| **C) information-weighted selector** | **PARTIAL re-description.** ρ is the occupancy (the count share); its information face is the self-information −ln ρ_k and the aggregate I = KL(ρ‖uniform) = I_occ = 0.7513 nats (QG228). "Information-weighted" is a valid but *secondary* description — the direct selector is the count share ρ. |
| **D) boundary import** | **REFUTED for the weight.** The weight ρ = \|ψ\|² is DERIVED (count conservation → normalization → Born rule). What IS a boundary is the **irreducible stochasticity of the realization itself** — that actualization realizes exactly one outcome per tick — which is the discrete tick, the deepest single boundary (QG_011). |

**Determination: B (probabilistic selector) — but with a DERIVED weight.** The selector is the
Born rule P(k) = ρ_k = |ψ_k|², which is not imported (D refuted for the weight) but derived from
count conservation (QG216). A (deterministic) is refuted; C (information-weighted) is a valid
secondary reading; the only boundary is the irreducible stochastic realization = the tick.

---

## 7. The node-selection law

```
Actualization (one tick)  →  realizes ONE count on ONE node
   node k selected with probability  ρ_k = |ψ_k|²   (Born rule, QG216)
   ρ = the conserved, normalized count share  (Σρ = 1 EXACT, count conservation)
   phase θ = 2πk/N  →  advances DETERMINISTICALLY  (Δθ = 2πk/N, D_041)  [NOT the selector]
```

**The node-selection law behind actualization is the Born rule.** Nothing travels (NP_092): the
selection is not a signal reaching the node, but the realization of the conserved count drawn with
weight ρ. The phase is the deterministic pairing DOF; the count is the probabilistic selection
DOF. The weight is DERIVED (count conservation); the irreducibly stochastic realization is the
discrete tick (the deepest boundary).

---

## Theorem

> **Theorem (NP_093).** The node-selection law behind actualization is the BORN RULE. An
> actualization event is one tick — a count realization (QG216/QG222) that realizes ONE count on
> ONE node, with node k selected with probability ρ_k = \|ψ_k\|², where ρ is the conserved,
> normalized count share (Σρ = 1 EXACT, QG216). This is the probabilistic selector (B), and its
> weight is DERIVED (count conservation → normalization → Born rule), not a boundary import (D
> refuted for the weight). The two real DOFs select differently: the PHASE θ = 2πk/N advances
> DETERMINISTICALLY per tick (θ_t = θ₀ + t·Δθ, Δθ = 2πk/N, D_041) and does NOT select the outcome
> (A refuted); the COUNT ρ realizes PROBABILISTICALLY with Born weight and IS the selector.
> Competing resonance maxima resolve statistically by Born weight (the larger-ρ node wins more
> often, proportionally, not always). The rule matches Born statistics exactly (QG216), matches the
> M_001 measurement (one outcome selected with weight ρ), and reproduces the double-slit buildup
> (the interference I = ρ_A + ρ_B + 2√(ρ_Aρ_B)·cos Δθ is the statistical accumulation of many
> Born selections). C (information-weighted) is a valid secondary reading (ρ is the occupancy; its
> information face is I = KL(ρ‖uniform) = 0.7513 nats, QG228). The only boundary is the IRREDUCIBLE
> STOCHASTIC REALIZATION itself — that one outcome per tick is realized — which is the discrete
> tick, the deepest single boundary (QG_011). **Success criterion: the node-selection law is the
> Born rule P(k) = ρ_k = |ψ_k|², with a DERIVED weight and a deterministic phase that is NOT the
> selector; the irreducibly stochastic realization is the tick.** Classification: the Born weight
> ρ = |ψ|² DERIVED (QG216, count conservation); the deterministic phase advance DERIVED (D_041);
> the probabilistic selection DERIVED (the Born rule, given the count); the irreducible stochastic
> realization FRAMEWORK/BOUNDARY (the discrete tick, QG_011); a deterministic selector REFUTED; a
> boundary-import selector REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define the event (Section 1). (2) Resolve competing maxima by Born weight
> (Section 2, verified). (3) Test selection rules (Section 3, verified — only Born survives).
> (4) Separate the two DOFs (Section 4, verified). (5) Compare to Born/M_001/double-slit
> (Section 5). (6) Decide A–D (Section 6). ∎

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a deterministic selector picks the node" | no deterministic rule exists; the phase (D_041) is deterministic but does not select the outcome |
| "the maximum-ρ node always wins" | Born gives P = ρ, not 0/1 — the larger node wins proportionally, not always |
| "uniform random selection" | Born weights by ρ = \|ψ\|², not uniformly over the K modes |
| "the phase selects the node" | θ = 2πk/N is the pairing label; it advances deterministically, it does not pick the outcome |
| "the selection weight is imported" | ρ = \|ψ\|² is DERIVED from count conservation (QG216) |
| "the selection is deterministic in some hidden way" | the irreducible stochastic realization (one outcome per tick) is the tick — the deepest boundary, not hidden determinism |

---

## 9. Falsification paths

| Claim | Falsification |
|---|---|
| the selector is the Born rule P(k) = ρ_k | an actualization whose outcome frequencies do NOT match ρ = \|ψ\|² (Born violation) |
| the weight is derived (not imported) | a derivation-failure: Σρ ≠ 1 (count not conserved) without an input |
| the phase is deterministic and not the selector | a phase that determines the outcome (phase-selects-node) in canonical AT |
| the selector is not deterministic | a deterministic rule for which node fires, derivable from the primitives |
| the only boundary is the tick | a derivation of the stochastic realization (one outcome per tick) from the primitives |

---

## 10. Classification

| Component | Status |
|---|---|
| the Born weight ρ = \|ψ\|² (the selection probability) | **DERIVED** (QG216 — count conservation → normalization) |
| the deterministic phase advance Δθ = 2πk/N | **DERIVED** (D_041) |
| the probabilistic node selection (Born rule) | **DERIVED** (given the conserved count) |
| the irreducible stochastic realization (one outcome per tick) | **FRAMEWORK/BOUNDARY** (the discrete tick, QG_011) |
| a deterministic selector | **REFUTED** |
| a boundary-import selector (the weight as input) | **REFUTED** |

**Conclusion.** If nothing travels (NP_092), what selects the node is the **Born rule**: at each
tick, one count is realized on one node with probability ρ_k = |ψ_k|² — the conserved, normalized
count share. The weight is DERIVED (count conservation, QG216); the phase is a deterministic DOF
(D_041) that does not select the outcome; competing maxima resolve statistically by Born weight;
and the pattern matches Born statistics, the M_001 measurement, and the double-slit buildup
exactly. The only boundary is the irreducible stochastic realization itself — one outcome per tick
— the discrete tick, the deepest single boundary of the theory. No new primitive; canonical AT
unchanged.

---

## 11. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_093_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_093_ActualizationEvent` | one tick = one count realization (QG216/QG222) | ✅ |
| `Y_NP_093_CompetingMaxima` | Born weight resolves competing nodes (not max-wins) | ✅ |
| `Y_NP_093_SelectionRules` | only Born P(k) = ρ_k survives | ✅ |
| `Y_NP_093_ProbabilisticVsDeterministic` | phase deterministic (not selector); count probabilistic (selector) | ✅ |
| `Y_NP_093_BornStatisticsMatch` | P(k) = \|ψ_k\|², Σρ = 1 EXACT | ✅ |
| `Y_NP_093_MeasurementMatch` | M_001 = Born-weighted actualization | ✅ |
| `Y_NP_093_DoubleSlitBuildup` | interference = statistical accumulation of Born selections | ✅ |
| `Y_NP_093_ABCD` | B (probabilistic, DERIVED weight); A refuted; C partial; D refuted | ✅ |
| `Y_NP_093_Classification` | weight DERIVED; stochastic realization FRAMEWORK (tick) | ✅ |
| `Y_NP_093_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_093"`

---

## References

- ResearchY-NP_090 (network ontology), NP_092 (network propagation).
- ResearchY-M_001 (measurement = state selection), M_002 (phase-pinning), M_003 (feedback /
  deterministic phase), M_005 (information conservation), M_009 (phase lattice).
- AT-QG: QG216 (Born rule = count conservation), QG220 (phase θ = 2πk/N), QG227 (initial
  conditions ρ_k = 1/K), QG228 (information I_occ = 0.7513 nats), QG_011 (tick = deepest boundary).
- ResearchY-D_041 (tick time-parameter / phase advance), D_036 (complex state).
