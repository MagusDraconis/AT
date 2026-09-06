# ResearchY-NP_098 — Localization Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_098 (permanent)
**Title:** Localization Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_098.md`
**Depends on:** ResearchY-NP_072 (particle = resonance class), NP_091 (network → spacetime),
NP_093 (actualization selection = Born rule), NP_094 (inertia / motion), NP_092 (nothing
travels), ResearchY-M_001 (measurement = Born selection), AT-QG QG216 (Born rule), QG220 (phase
θ = 2πk/N), ResearchY-D_041 (tick / phase advance)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_098_Tests.cs`

---

## Purpose

NP_072 made particles resonance classes (global modes); NP_094 made motion resonance propagation
(the wave-packet envelope at v_g); NP_093 made actualization Born-weighted node selection. NP_098
asks the question those three leave open: **why does a resonance class appear at a specific place
and time — what is localization?** Program: (1) define locality on the network; (2) determine
whether a particle is a localized node / propagating resonance / probability distribution /
actualization history; (3) trace the electron, photon, graviton over multiple ticks; (4) determine
what produces position, trajectory, localization; (5) compare the wave packet, the actualization
chain, and the classical worldline; (6) test whether localization is derived / emergent /
framework. **Success criterion:** explain why a resonance class appears localized. No new
primitives; canonical AT unchanged.

---

## 1. Define locality vs localization on the network

| Term | Meaning |
|---|---|
| **locality** | the adjacency — a node couples only to its neighbours (NP_092) |
| **localization** | the *concentration* of a particle's amplitude |ψ|² at one node |

**A resonance class is NOT localized.** A mode is a standing wave over the whole ring: |ψ_k|² =
1/N at every node (uniform, delocalized). Localization is a *separate* phenomenon — it is what
happens when many modes are superposed into a **wave packet**.

---

## 2. A / B / C / D — what is a particle?

| Interpretation | Verdict |
|---|---|
| **A) a localized node** | **REFUTED.** A particle is a resonance class (a global mode), not a single node (NP_072). |
| **B) a propagating resonance** | **YES — the ontology.** A particle IS a resonance mode; its localized form is a propagating wave packet (NP_094). |
| **C) a probability distribution** | **YES — its localization.** \|ψ\|² = ρ is the Born distribution; the particle's *position* is the envelope peak of this distribution. |
| **D) an actualization history** | **PARTIAL — its trajectory.** The actualization chain (Born-selected nodes) traces the path, but the particle is the mode, not the history. |

**Determination: B (propagating resonance) is the ontology; C (probability distribution) is its
localization; D (actualization history) is its trajectory; A (localized node) is refuted.**

---

## 3. Trace electron, photon, graviton over multiple ticks

| Object | Mode | Localized form | Position (per tick) |
|---|---|---|---|
| **electron** (matter, ω₀ > 0) | a superposition of electron modes | a wave packet | envelope peak advances at v_g = k/ω |
| **photon** (massless generator) | a superposition of photon modes | a wave packet | envelope advances at n = 1 (c) |
| **graviton** (massless ψ ripple) | a superposition of ψ modes | a wave packet | ripple advances at n = 1 |

Each tick: the envelope peak advances one step (at most, one link = c); the Born selection
(NP_093) then realizes one node with probability |ψ|² = ρ — so the particle's *actualized* position
is a node drawn from the envelope, following it statistically.

---

## 4. What produces position, trajectory, localization

| Object | Produced by |
|---|---|
| **localization** | the **constructive interference** of a wave packet (a superposition of modes): the modes add coherently at one node and cancel elsewhere. |
| **position** | the **envelope peak** — the node where \|ψ\|² = ρ is maximal. |
| **trajectory** | the **actualization chain** — the sequence of Born-selected nodes, which follows the envelope at the group velocity v_g. |

The Fourier uncertainty ties them together:

```
Δx · Δk = 1   (1/e² widths; ≥ 1/2 for standard deviations)
```

- a **single mode** (Δk → 0): Δx → ∞ — delocalized (|ψ|² uniform);
- a **wave packet** (Δk large): Δx small — localized (|ψ|² peaked).

**Localization is the constructive interference of a mode superposition**; the narrower the
position, the broader the mode spread.

---

## 5. Wave packet vs actualization chain vs classical worldline

| Description | What it is | Status |
|---|---|---|
| **wave packet** | the superposition of modes; its \|ψ\|² envelope is the *potential* position | DERIVED (from the spectrum) |
| **actualization chain** | the Born-selected node sequence — the *actualized* position, stochastic | EMERGENT (NP_093) |
| **classical worldline** | the smooth envelope trajectory at v_g — the emergent classical limit | EMERGENT |

The classical worldline is the **smooth envelope** of the wave packet; the actualization chain is
its **stochastic realization** (the Born selections); the wave packet is the **underlying
superposition**. One particle, three readings.

---

## 6. Derived / emergent / framework?

| Component | Status |
|---|---|
| the wave packet (superposition of modes) | **DERIVED** (from the spectrum) |
| the Born rule \|ψ\|² = ρ (the localization weight) | **DERIVED** (QG216 — count conservation) |
| the localized appearance (the peaked envelope; position; trajectory) | **EMERGENT** (constructive interference; the actualization chain) |
| the position basis (the node labeling / network geometry) | **FRAMEWORK** (η + D96, NP_084/091) |

**Localization is EMERGENT** — the peaked envelope arises from the constructive interference of
many derived modes, realized by the derived Born rule, within the framework's node basis.

---

## Theorem

> **Theorem (NP_098).** Localization in AT is the CONSTRUCTIVE INTERFERENCE of a wave packet — a
> superposition of resonance modes whose coherent addition peaks |ψ|² = ρ at one node and cancels
> elsewhere. A particle is a propagating resonance (B); its localization is the Born distribution's
> envelope peak (C); its trajectory is the actualization chain of Born-selected nodes (D); a single
> localized node (A) is refuted. The Fourier uncertainty Δx·Δk = 1 governs the trade-off: a single
> mode (Δk → 0) is delocalized (|ψ|² = 1/N uniform, Δx → ∞), while a wave packet (Δk large) is
> localized (Δx small). Position = the envelope peak; time = the tick (the envelope advances at the
> group velocity v_g, one link per tick at most); the Born selection (NP_093) realizes the node with
> probability |ψ|² = ρ, so the observed trajectory is a stochastic chain following the envelope. The
> classical worldline is the smooth envelope (the emergent limit). Proof: (1) Define
> locality/localization (Section 1). (2) Test A–D (Section 2, verified — B the ontology, C/D its
> localization/trajectory, A refuted). (3) Trace e/γ/graviton (Section 3). (4) Position/trajectory/
> localization (Section 4, verified — Fourier uncertainty). (5) Three readings (Section 5).
> (6) Derived/emergent/framework (Section 6). **Success criterion: a resonance class appears at a
> specific place because its wave packet's |ψ|² envelope peaks there (constructive interference),
> and at a specific time because the envelope advances at v_g, one step per tick — localization is
> EMERGENT.** Classification: the wave packet DERIVED (superposition of modes); the Born weight
> |ψ|² = ρ DERIVED (QG216); the localized appearance (position, trajectory) EMERGENT; the position
> basis FRAMEWORK (η + D96); "particle = a localized node" REFUTED. No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Define. (2) Test A–D. (3) Trace the three objects. (4) Fourier uncertainty.
> (5) Three readings. (6) Classify. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a particle is a localized node" | a particle is a resonance class (a global mode), not a single node (NP_072) |
| "a single mode is localized" | a single mode has |ψ|² = 1/N uniform — it is delocalized |
| "localization is a framework input" | localization is constructive interference (emergent); only the node basis is framework |
| "the trajectory is deterministic" | the actualization chain is Born-stochastic (NP_093); only the envelope (worldline) is smooth |
| "position is primitive" | position = the envelope peak of |ψ|², derived from the mode superposition |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| localization = constructive interference | a localized particle with no mode superposition (a single-mode packet) |
| a single mode is delocalized | a single resonance mode with a peaked (non-uniform) |ψ|² |
| the trajectory follows the envelope | an actualization chain that does not track the |ψ|² envelope |
| position = the envelope peak | a position not given by the maximal node of |ψ|² |

---

## 9. Classification

| Component | Status |
|---|---|
| the wave packet (superposition of modes) | **DERIVED** (from the spectrum) |
| the Born weight \|ψ\|² = ρ | **DERIVED** (QG216) |
| localization / position / trajectory (the envelope + actualization chain) | **EMERGENT** |
| the position basis (node labeling / geometry) | **FRAMEWORK** (η + D96) |
| particle = a localized node | **REFUTED** |

**Conclusion.** Localization is **constructive interference** — a resonance class (a delocalized
mode) appears at a specific place because a *wave packet* (a superposition of modes) adds
coherently there, peaking |ψ|² = ρ; and at a specific time because the envelope advances at the
group velocity, one step per tick. The Born selection then realizes one node with probability
|ψ|². A single mode is delocalized (|ψ|² uniform); localization requires a superposition (the
Fourier uncertainty Δx·Δk = 1). The wave packet and Born weight are DERIVED; the localized
appearance (position, trajectory) is EMERGENT; the node basis is FRAMEWORK. No new primitive;
canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_098_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_098_LocalityVsLocalization` | locality = adjacency; localization = concentration | ✅ |
| `Y_NP_098_ParticleABCD` | B ontology; C localization; D trajectory; A refuted | ✅ |
| `Y_NP_098_SingleModeDelocalized` | |ψ|² = 1/N uniform | ✅ |
| `Y_NP_098_WavePacketLocalized` | superposition → peaked |ψ|² | ✅ |
| `Y_NP_098_FourierUncertainty` | Δx·Δk = 1 | ✅ |
| `Y_NP_098_TraceObjects` | e/γ/graviton: envelope at v_g (n=1 for massless) | ✅ |
| `Y_NP_098_ThreeReadings` | wave packet / actualization chain / worldline | ✅ |
| `Y_NP_098_Classification` | packet DERIVED; localization EMERGENT; basis FRAMEWORK | ✅ |
| `Y_NP_098_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_098"`

---

## References

- ResearchY-NP_072 (particle = resonance class), NP_091 (network → spacetime), NP_092 (nothing
  travels), NP_093 (actualization selection = Born rule), NP_094 (inertia / motion).
- ResearchY-M_001 (measurement = Born selection).
- AT-QG: QG216 (Born rule), QG220 (phase θ = 2πk/N).
- ResearchY-D_041 (tick / phase advance).
