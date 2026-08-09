# TQM Failure Analysis Framework

## Failure Modes, Survival Structure, and Experimental Kill-Shots

*TQM Collaboration — Hostile Audit Document, August 2026*

---

## 1. Introduction

### 1.1 Why Failure Analysis

A scientific theory that cannot define the conditions of its own falsification is not a scientific theory. The TQM research program has produced a framework of interlocking claims. Many of these claims are testable. Some will be wrong.

This document exists for one purpose: to define, in advance of the relevant experiments, exactly how the framework can fail, which parts would survive, and how the surviving pieces should be revised. It is written from the perspective of a skeptical auditor who assumes the framework contains errors and seeks to identify them.

### 1.2 Scope

This document does not defend TQM. Every claim is treated as possibly false. Every prediction is assumed to be at risk. The goal is not to preserve the framework but to prepare for its failure.

The analysis covers:

- **Dependency structure**: which claims depend on which others.
- **Failure modes**: for each major prediction, what observation would falsify it, and what the consequences would be.
- **Survival analysis**: if a prediction fails, what pieces of the framework survive.
- **Kill-shot scenarios**: specific experimental outcomes and their cascading consequences.
- **Most likely failure points**: where the framework is most vulnerable.
- **Hostile referee reports**: the strongest scientific criticisms of the framework.

---

## 2. TQM Dependency Hierarchy

### 2.1 Tiered Structure

The framework's claims form a dependency hierarchy. Higher tiers are logically prior; lower tiers depend on them. A failure at a higher tier propagates to all lower tiers that depend on it.

```
═══════════════════════════════════════════════════════════════
  TIER 0 — AXIOMS (cannot be falsified by experiment)
═══════════════════════════════════════════════════════════════
  Q          — Individuation (distinguishable entities exist)
  Randomness — Actualization (outcome selection is genuinely random)

  These are definitional. They can only be rejected on grounds
  of internal inconsistency, not experimental conflict.
═══════════════════════════════════════════════════════════════
  TIER 1 — DERIVED STRUCTURES (falsifiable by internal contradiction)
═══════════════════════════════════════════════════════════════
  Quantum mechanics (Hilbert, Schrödinger, Born, measurement)
  Time (partial order of actualization events)
  Spacetime structure (3+1 dimensions)
  Causal set gravity → General Relativity

  These are proposed to follow logically from Tier 0.
  They could be falsified by demonstrating a logical error
  in the derivations. Experimental falsification of GR would
  falsify the causal set gravity derivation, not necessarily
  the entire Tier 0–1 structure.
═══════════════════════════════════════════════════════════════
  TIER 2 — IDENTITY PHYSICS (falsifiable by experiment)
═══════════════════════════════════════════════════════════════
  Particles = topological defects
  Gauge symmetry = Aut(moduli space)
  U(1) from vortex S¹ moduli
  Three generations from stability cutoff
  Mass hierarchy pattern (geometric)
  Mixing structure (exponential overlap)

  These are proposed to follow from Tier 0–1 plus internal
  consistency arguments. They make specific claims about
  what particles and forces exist. They can be falsified
  by discovering particles or forces incompatible with
  the defect taxonomy.
═══════════════════════════════════════════════════════════════
  TIER 3 — ABUNDANCE PHYSICS (falsifiable by experiment)
═══════════════════════════════════════════════════════════════
  All abundance quantities are log-normal draws
  σ² = N·σ₀² (cascade depth × per-step volatility)
  σ₀² = Var[-log(p)] (Born rule volatility)
  μ = log(N_f/N_i) (cosmic expansion drift)
  Γ_X(T_f) = H(T_f) (freezeout criterion)
  Γ_X = n·σ·v (actualization rate)
  σ_X from defect geometry

  These are proposed to describe the statistical properties
  of continuous physical quantities. They can be falsified
  by demonstrating that abundance quantities are not log-normal
  or that the parameters μ, σ² do not follow the predicted scaling.
═══════════════════════════════════════════════════════════════
  TIER 4 — COSMOLOGY AND DARK SECTOR (falsifiable by experiment)
═══════════════════════════════════════════════════════════════
  Λ(t) = α/√V(t) → w(z) ≠ −1
  a₀ ≈ cH₀ from Λ
  DM = neutral topological defects
  Neutrino normal ordering
  M² = ⟨k⟩_interact ≈ 5

  These are the most exposed predictions. They make specific
  quantitative claims about observable cosmology and particle
  properties. They are the primary targets for experimental
  falsification.
═══════════════════════════════════════════════════════════════
```

### 2.2 Dependency Graph

```
Tier 0 (Axioms)
    │
    ├──→ Tier 1 (QM, Time, Spacetime, GR)
    │        │
    │        ├──→ Tier 2 (Particles, Gauge, Generations, Masses)
    │        │        │
    │        │        ├──→ Tier 3 (Abundance distributions)
    │        │        │        │
    │        │        │        └──→ Tier 4 (Cosmology: Λ, a₀)
    │        │        │
    │        │        └──→ Tier 4 (DM identity, ν ordering, M²)
    │        │
    │        └──→ Tier 3 (Freezeout, rates, cross-sections)
    │
    └──→ (no direct path to Tier 4 — all Tier 4 depends on Tier 1-3)

KEY OBSERVATION: Tier 4 fails → Tier 0-3 survive.
                 Tier 2 fails → Tier 0-1 survive, Tier 3-4 fail.
                 Tier 1 fails → entire framework collapses.
```

---

## 3. Failure Modes

### 3.1 Prediction-by-Prediction Analysis

For each major prediction, we identify: the claim, the observation that would falsify it, the consequence for the framework, and what survives.

#### Mode 1: w(z) ≠ −1

| Aspect | Detail |
|--------|--------|
| **Claim** | w(z) ≈ −1 + 0.015·(1+z)^(3/2); deviation ~1–4% |
| **Falsifying observation** | w(z) = −1.000 ± 0.01 (Euclid + Roman combined) |
| **Consequence** | Tier 4 Λ(t) hypothesis falsified. Tier 3 abundance framework in current quantitative form invalidated (depends on Λ for freezeout scaling). Tier 2 particle physics unaffected. Tier 1 QM/GR unaffected. |
| **Required revision** | Abandon Λ(t) = α/√V. The cosmological constant may be a true constant or have a different origin. The abundance framework must be reformulated without Λ-dependent freezeout. |
| **Survival score** | 6/8 predictions survive. Cosmology-specific predictions killed. |

#### Mode 2: a₀ ≠ f(H₀)

| Aspect | Detail |
|--------|--------|
| **Claim** | a₀ ≈ cH₀/(2π); the MOND acceleration scale is derived from Λ and H₀ |
| **Falsifying observation** | a₀ shown to be constant while H(z) evolves; no correlation between a₀ and Hubble rate across cosmic epochs |
| **Consequence** | The Λ → a₀ link (X063) is broken. Correlation gravity may still produce MOND-like effects, but not through the claimed Λ connection. |
| **Required revision** | Decouple a₀ from Λ. Correlation gravity may survive as a separate mechanism or be abandoned. |
| **Survival score** | 7/8 predictions survive. Only the a₀ derivation is killed. |

#### Mode 3: Dark Matter ≠ Neutral Defects

| Aspect | Detail |
|--------|--------|
| **Claim** | DM consists of stable neutral topological defects (TeV-scale neutral vortices, moduli excitations) |
| **Falsifying observation** | Detection of a DM particle (WIMP, axion, or other) with properties inconsistent with topological defects |
| **Consequence** | Tier 4 defect DM identity falsified. Tier 2 broad defect taxonomy survives — neutral defects simply are not the dominant DM component. |
| **Required revision** | Separate the defect ontology from the DM problem. DM may be a separate sector or a different defect type not currently identified. |
| **Survival score** | 7/8 predictions survive. Only the specific DM identity is killed. |

#### Mode 4: Neutrino Inverted Ordering

| Aspect | Detail |
|--------|--------|
| **Claim** | Normal ordering (m₁ < m₂ < m₃) from attractive self-interaction in φ⁴ |
| **Falsifying observation** | Inverted ordering confirmed at >5σ (JUNO, DUNE, Hyper-K) |
| **Consequence** | Model A (X060) falsified. The broader delocalized-defect explanation survives — more complex defect potentials could produce inverted ordering. |
| **Required revision** | Modify the neutrino defect model. The core insight (neutrinos = delocalized neutral defects) is not falsified by ordering alone. |
| **Survival score** | 7/8 predictions survive. Only the simplest neutrino model is killed. |

#### Mode 5: Log-Normal Abundance Law Falsified

| Aspect | Detail |
|--------|--------|
| **Claim** | All abundance quantities are log-normal draws from multiplicative actualization cascades |
| **Falsifying observation** | Demonstrated that α (or other abundance quantity) is constant to precision exceeding the predicted log-normal width; or that different abundance quantities follow different distribution families |
| **Consequence** | The quantitative abundance framework (XB002-XB005) is invalidated. The qualitative distinction between identity and abundance survives. |
| **Required revision** | Abandon or significantly modify the universal log-normal claim. Retain the identity/abundance distinction as a conceptual framework. |
| **Survival score** | 5/8 predictions survive (cosmology predictions survive if independent of abundance framework). Tier 0-2 survive. |

#### Mode 6: M² ≠ ⟨k⟩

| Aspect | Detail |
|--------|--------|
| **Claim** | M² = ⟨k⟩_interact ≈ 5 is the average causal degree; depends only on dimensionality |
| **Falsifying observation** | Demonstrated that no consistent definition of causal connectivity produces the observed M² ≈ 5; or that M² varies independently of dimensionality |
| **Consequence** | The parameter elimination claim (XC002-XC005) is invalidated. M² remains as a free parameter of the framework. |
| **Required revision** | Accept M² as a measured input. The reduction from ~19 SM parameters to 1 (M²) still represents significant compression. |
| **Survival score** | All 8 predictions survive. Only the "zero-parameter" claim is weakened to "one-parameter." |

---

## 4. Survival Analysis

### 4.1 Complete Failure Matrix

The following table shows, for each possible falsification, exactly which predictions survive and which are killed.

| If this is falsified... | P1 (w) | P2 (a₀) | P3 (DM) | P4 (singular) | P5 (log-n) | P6 (ν) | P7 (M²) | TQM core |
|------------------------|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
| w(z) = −1.000 | ✗ | ✗ | ✓ | ✓ | ✓* | ✓ | ✓ | ✓ |
| a₀ ≠ f(H₀) | ✓ | ✗ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| WIMP detected | ✓ | ✓ | ✗ | ✓ | ✓ | ✓ | ✓ | ✓ |
| Inverted ν | ✓ | ✓ | ✓ | ✓ | ✓ | ✗ | ✓ | ✓ |
| α constant (no log-normal) | ✓ | ✓ | ✓ | ✓ | ✗ | ✓ | ✓ | ✓ |
| M² ≠ ⟨k⟩ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✗ | ✓ |
| All of the above | ✗ | ✗ | ✗ | ✗ | ✗ | ✗ | ✗ | ✓ |

*Key: ✓ = survives. ✗ = killed. \* = survives in qualitative form but quantitative predictions invalidated.*

**Critical observation:** Tier 0-1 (QM, time, spacetime, GR) survives ALL experimental falsifications of Tiers 2-4. The only way to kill Tier 0-1 is to demonstrate an internal logical contradiction in the derivations themselves.

### 4.2 What Would Kill the Entire Framework?

The framework can only be killed entirely by:

1. **Internal inconsistency.** A demonstration that the Tier 1 derivations (QM from complexity maximization, time from actualization order) contain logical errors. This is a theoretical kill, not an experimental one.

2. **Counter-evidence to all predictions simultaneously.** If all eight predictions fail, the framework has no empirical support and should be abandoned. This is unlikely — the predictions span independent domains (cosmology, particle physics, neutrino physics).

3. **Discovery of a simpler competing framework.** If a framework with fewer primitives or simpler axioms makes the same or better predictions, TQM is superseded by Occam's razor.

---

## 5. The Euclid Scenario

### 5.1 Scenario A: w(z) = −1.00 ± 0.01 (Clean Falsification)

**What happens.** Euclid and Roman together measure w(z) = −1.000 with uncertainty σ ≈ 0.01. The deviation predicted by TQM (~1–4%) is not observed at >3σ.

**Immediate consequences:**
- The time-varying Λ hypothesis (P1) is falsified.
- The specific Λ(t) = α/√V(t) model (P2) is falsified.
- The abundance framework (XB series) depends on Λ for freezeout scaling and must be revised.
- The a₀ ≈ cH₀ connection loses its primary motivation.

**Surviving framework:**
- Tier 0: Q, Randomness (intact).
- Tier 1: QM, time, spacetime, GR (intact).
- Tier 2: Particles, gauge structure, generations, mass patterns (intact).
- Tier 4: DM identity, neutrino ordering (unaffected).

**Required revisions:**
- Abandon or substantially revise the cosmological constant derivation (X046).
- Reformulate the abundance framework without Λ-dependent freezeout.
- Investigate whether correlation gravity (X063) can survive without the Λ connection.
- The framework would remain a theory of quantum mechanics, particles, and gravity, with approximately one free parameter (M²).

### 5.2 Scenario B: Weak Deviation Detected (w ≠ −1 at ~2σ)

**What happens.** Euclid measures w ≈ −0.98 ± 0.02. The deviation is in the right direction but at marginal significance (~2σ).

**Consequences:**
- The framework is consistent with data but not confirmed.
- Additional data from Roman is needed to reach >3σ.
- The framework remains viable but unproven.

**Required action:**
- Wait for Roman to reduce uncertainty to σ ≈ 0.01.
- If Roman confirms w ≠ −1 at >3σ, the framework survives its most critical test.
- If Roman finds w = −1.00 ± 0.01, revert to Scenario A.

### 5.3 Scenario C: Deviation Confirmed (w ≠ −1 at >3σ)

**What happens.** Euclid + Roman measure w ≠ −1 with the predicted sign and approximate magnitude at >3σ significance.

**Consequences:**
- The time-varying Λ hypothesis survives its most critical test.
- The framework's cosmology receives strong empirical support.
- The framework is not "proven" — other models also predict w ≠ −1 — but it is consistent with the data.

**Remaining challenges:**
- Distinguishing TQM's specific functional form from other dark energy models.
- Testing the remaining predictions (neutrino ordering, DM identity, abundance distributions).
- The framework remains falsifiable through its other predictions.

---

## 6. The Neutrino Scenario

### 6.1 Normal Ordering Confirmed

**Consequence.** The framework's simplest neutrino model (Model A, X060) is consistent with data. This is a necessary but not sufficient condition — many models predict normal ordering.

**Impact on framework.** Minimal. The prediction was not unique to TQM. It adds modest support but does not distinguish the framework from competitors.

### 6.2 Inverted Ordering Confirmed at >5σ

**Consequence.** Model A is falsified. The attractive self-interaction argument for normal ordering is wrong.

**What survives:**
- The delocalized-defect explanation for neutrino identity (X059) is unaffected. Neutrinos can still be neutral, delocalized topological defects.
- The ordering could emerge from a more complex defect potential where self-interaction is repulsive or where different defect types (beyond simple φ⁴ kinks) govern the neutrino sector.
- The broader framework loses one specific model but retains its explanatory structure.

**Required revision.** Develop a defect model that accommodates inverted ordering. This may require abandoning the simple φ⁴ kink model for neutrinos in favor of a different defect type.

### 6.3 Quasi-Degenerate Spectrum

**Consequence.** If all three neutrino masses are nearly equal (Δm² ≪ observed), the excitation-level interpretation of generations (X051) is challenged. The stability cutoff argument predicts hierarchical masses.

**Impact.** More severe than ordering reversal. Would require revisiting the generation-count derivation.

---

## 7. The Dark Matter Scenario

### 7.1 Case A: Continued Null Results (Most Likely Under TQM)

**Consequence.** Consistent with the defect DM hypothesis. No positive evidence. The framework neither gains nor loses support.

**Risk.** Prolonged null results at increasingly sensitive experiments make the framework increasingly plausible by elimination but never confirmed. This is an uncomfortable position — the defect DM model may be unfalsifiable in practice.

### 7.2 Case B: WIMP Detected (~50 GeV–1 TeV, σ ~ 3×10⁻²⁶ cm³/s)

**Consequence.** The defect DM hypothesis (X064) is falsified. The Standard Model's WIMP miracle would be confirmed.

**What survives.** Tier 2 defect taxonomy survives — neutral defects exist but are not the dominant DM component. DM would require a separate explanation, possibly outside the current TQM framework entirely.

**Required revision.** The defect ontology must be decoupled from the DM problem. A new DM candidate must be identified within TQM, or the framework must accept that DM is beyond its current scope.

### 7.3 Case C: Axion Detected

**Consequence.** Similar to Case B. The specific defect DM candidate is wrong. However, TQM's "hidden moduli excitation" candidate is axion-like, so an axion detection could be interpreted as support for the moduli DM model — depending on the axion's properties.

**Required analysis.** Compare the detected axion's mass and coupling to the predicted moduli excitation parameters (~500 GeV mass, specific coupling to gauge fields). If consistent, this supports the moduli DM candidate. If inconsistent, the candidate is falsified.

### 7.4 Case D: Unknown DM Sector (Entirely New Physics)

**Consequence.** The framework's DM prediction is wrong, and the true DM sector is beyond any current framework. This is the hardest scenario — no framework anticipated it. All DM predictions across all theories are simultaneously falsified. TQM would be in the same position as all competitors.

---

## 8. The Parameter Scenario — M² ≠ ⟨k⟩

### 8.1 The Claim

The framework claims that M² = ⟨k⟩_interact ≈ 5, eliminating the last free continuous parameter.

### 8.2 Falsification

This claim is falsified if:
- No consistent definition of causal degree produces M² ≈ 5.
- M² is shown to vary independently of dimensionality.
- The observed M² ≈ 5 is demonstrated to be a coincidence rather than a necessary consequence of 3+1D causal structure.

### 8.3 Consequences

**What breaks:**
- The "zero-parameter" claim becomes "one-parameter."
- ResearchXC (XC002-XC005) is invalidated in its specific quantitative form.
- The identification of M² with causal connectivity is rejected.

**What survives:**
- The analytical result that ⟨k⟩ = f(d) (ρ cancels) remains a valid theorem of causal set theory — it just doesn't correspond to M².
- All Tier 2-4 predictions are unaffected. M² was treated as a parameter in those derivations anyway.
- The framework remains a one-parameter theory — still a ~95% reduction from the Standard Model's ~19 parameters.

### 8.4 Is One Parameter Acceptable?

A one-parameter framework that derives all of quantum mechanics, general relativity, the Standard Model gauge structure, three generations, mass hierarchies, and mixing patterns is still an extraordinary achievement. The difference between zero and one parameter is scientifically significant but philosophically subtle — both represent massive compression from the status quo.

**Recommendation.** If M² ≠ ⟨k⟩ is established, the framework should accept M² as a measured input and continue. The compression ratio (~19 → 1) is sufficient to justify continued investigation.

---

## 9. Most Likely Failure Points

The following ranking reflects an assessment of where the framework is most vulnerable to experimental falsification, based on the precision of the predictions, the quality of the internal derivations, and the proximity of decisive experiments.

| Rank | Failure Point | Likelihood | Reasoning |
|:--:|--------------|:--:|-----------|
| **1** | **w(z) = −1.000** | **Medium-High** | The prediction is highly specific. The derivation depends on the Poisson fluctuation model, the radiation-era expansion assumption, and an uncomputed dimensionless coefficient. Multiple independent failure modes exist. The experiment is imminent. |
| 2 | Inverted neutrino ordering | Low-Medium | The prediction is model-dependent (Model A only). Even if falsified, the broader framework survives. The experimental timescale is 5–10 years. |
| 3 | M² ≠ ⟨k⟩ | Low-Medium | The identity depends on the definition of "interaction degree." Different definitions give different values. The framework can tolerate this failure. |
| 4 | WIMP detected | Low | The defect DM model specifically predicts null results. A WIMP detection would falsify it but leave the broader framework intact. Current direct detection limits already constrain the relevant parameter space. |
| 5 | α constant (no log-normal) | Low | The log-normal prediction is the least precisely quantified. The distribution width is estimated, not derived. The prediction is hard to falsify in practice. |

**Summary.** The framework is most likely to fail at its most exposed prediction — time-varying dark energy. This is also its most scientifically valuable prediction, because it is the most falsifiable.

---

## 10. Robustness Scorecard

| Prediction | Internal Confidence | Dependency Depth | Falsification Risk | Survival Probability |
|-----------|:--:|:--:|:--:|:--:|
| w(z) ≠ −1 | Medium-High | Deep (depends on X046, X062, XB) | **High** | 40% |
| Λ(t) = α/√V | Medium | Deep | **High** | 35% |
| a₀ ≈ cH₀ | Medium | Medium | Medium | 55% |
| DM = neutral defects | Medium | Shallow (depends on X047, X064) | Low | 70% |
| No singularities | High | Shallow | None (untestable) | 95% |
| Log-normal abundance | Low-Medium | Medium | Low-Medium | 50% |
| Neutrino normal ordering | Medium | Shallow | Medium | 65% |
| M² = ⟨k⟩ | Medium | Deep | Low | 60% |

**Key.** *Internal Confidence*: how well the derivation holds within the framework. *Dependency Depth*: how many internal claims the prediction depends on (deeper = more failure points). *Falsification Risk*: likelihood of experimental falsification within one decade. *Survival Probability*: estimated probability that the prediction survives (or that the framework survives its falsification).

---

## 11. Hostile Referee Report

The following sections represent the strongest possible scientific criticisms of the framework, written as if by skeptical referees evaluating the program for funding or publication.

### 11.1 Cosmology Referee

"The time-varying dark energy prediction is the centerpiece of the experimental program, and it is the most likely to fail. The derivation of Λ(t) = α/√V(t) depends on at least three unverified assumptions: (1) that Q-event discreteness produces Poisson fluctuations in causal diamonds with exactly the right statistics, (2) that the continuum limit of these fluctuations maps correctly to the Friedmann equation, and (3) that the dimensionless coefficient α is O(1). The prediction w(z) ≈ −1 + 0.015·(1+z)^(3/2) is semi-quantitative — the coefficient 0.015 is not computed but estimated. If Euclid finds w = −1.00, the proponents will argue that 'the coefficient was wrong but the idea survives.' This is not falsifiability — it is a moving target.

Furthermore, the entire Λ(t) derivation piggybacks on standard ΛCDM thermal history. If the early universe departs from radiation-era scaling, the prediction changes. The framework does not predict the thermal history — it imports it. This is a hidden assumption that undermines the 'zero-parameter' claim."

### 11.2 Particle Physics Referee

"The framework's particle physics is simultaneously its strongest and weakest component. The identification of particles with topological defects is elegant and provides genuine explanatory power — why particles exist, why gauge symmetries exist, why there are three generations. However, the framework does not predict a single particle mass. The geometric hierarchy m_n ∝ exp(n·π·a) is a pattern, not a prediction — the anharmonicity parameter a must be measured, not derived. The framework has replaced the Standard Model's Yukawa couplings with a smaller set of parameters (a₀, γ), but it has not eliminated them.

The dark matter prediction is unfalsifiable in practice. A TeV-scale neutral defect with no electromagnetic coupling and unknown weak coupling could hide from any conceivable experiment. The framework's proponents claim this is a prediction; a skeptic would call it an excuse for null results.

The prediction most likely to be wrong is the neutrino ordering. The attractive self-interaction argument is model-dependent and would need to be revisited if the defect type differs from a simple φ⁴ kink."

### 11.3 Quantum Gravity Referee

"The causal set-to-GR bridge is not TQM's achievement — it is causal set theory's achievement. TQM provides a physical interpretation for causal set elements (they are Q-events), but the hard mathematical work — the BDG action, the continuum limit, the emergence of the Einstein equations — was done by Sorkin, Benincasa, Dowker, Glaser, and others. The framework claims to 'derive GR' but what it actually does is assert that Q-events form a causal set and then appeal to well-known results in causal set theory. This is not a derivation — it is an appropriation.

The M² = ⟨k⟩ claim is similarly dependent on a specific definition of degree in causal set theory. There are multiple plausible definitions — linked degree, interaction degree, graph degree — and they give different numerical values. The proponents have selected the one that matches their desired result. This is post-hoc model selection, not prediction.

The most vulnerable part of the quantum gravity sector is the claim that ⟨k⟩ depends only on dimensionality. This is true analytically (the sprinkling density ρ cancels beautifully in the Alexandrov integral), but the numerical value depends on the Poisson sprinkling assumption. Real Q-event networks may not be Poisson-sprinkled — the framework has not demonstrated that the actualization process produces Poisson statistics."

---

## 12. Final Assessment

### 12.1 If the Framework Fails

The most likely failure scenario is:

> **Euclid + Roman measure w(z) = −1.000 ± 0.01.** The time-varying Λ hypothesis is falsified. The abundance framework requires substantial revision. The framework retreats to a one-parameter theory of quantum mechanics, particles, and gravity, with an unexplained cosmological constant.

This is not a catastrophic failure. A one-parameter framework that derives the Standard Model gauge structure, three generations, mass hierarchy patterns, and mixing from two primitives would still be a significant scientific achievement. The framework would abandon its cosmological predictions and focus on its particle physics and quantum gravity core.

#### Known Recovery Paths (w(z) = −1 scenario)

The following recovery paths define how the framework could be revised if the Λ(t) prediction fails, without abandoning the research program entirely.

**Path A: Constant Λ, Modified Abundance.** Accept Λ as a fundamental constant (as in ΛCDM). Revise the abundance framework to use a freezeout criterion that does not depend on Λ(t). The identity/abundance distinction survives. The log-normal distribution law may survive if the cascade mechanism is reformulated with a different timescale. *Loss: cosmological constant derivation, time-varying dark energy prediction. Gain: clean separation of abundance physics from cosmology.*

**Path B: Freezeout Independent of Λ.** Retain the abundance framework but decouple freezeout from cosmic expansion history. The cascade depth N would be set by a different physical timescale — perhaps the actualization rate itself, or the defect formation epoch. *Loss: the elegant Λ → freezeout connection. Gain: abundance framework survives in modified form.*

**Path C: Alternative Dark Energy Emergence.** Abandon the Poisson fluctuation model but retain the broader idea that dark energy emerges from Q-event structure. Investigate alternative emergence mechanisms: entropic gravity, correlation-induced curvature, defect-density effects. *Loss: the specific X046 derivation. Gain: the research program's motivation survives.*

**Path D: Full Cosmology Sector Replacement.** Accept that TQM's current cosmological predictions are wrong. Replace Tier 4 (cosmology, dark sector) with a new approach while retaining Tier 0-3 (QM, particles, gravity). This preserves the framework's strongest results — quantum mechanics emergence, particle physics from defects, gauge structure from topology. *Loss: all cosmological predictions. Gain: core framework intact.*

### 12.2 If the Framework Survives

The strongest possible confirmation scenario is:

> **Euclid + Roman measure w(z) ≠ −1 at >3σ, with the predicted sign (w > −1) and approximate magnitude.** JUNO and DUNE confirm normal ordering. Direct DM detection continues to return null results. Galaxy surveys show a₀ tracking H(z).

Even in this optimistic scenario, the framework is not "proven." It is consistent with data and survives its most critical tests. It would join ΛCDM as a viable cosmological model, with the advantage of having fewer free parameters and a physical derivation for dark energy. Distinguishing it from other models that also predict w ≠ −1 (quintessence, modified gravity) would require additional tests beyond 2035.

### 12.3 The Value of the Framework

Regardless of whether the framework is correct or incorrect, the exercise of deriving specific, falsifiable predictions from minimal primitives is scientifically valuable. It provides:

- Clear experimental targets for the next decade.
- A benchmark for parameter compression.
- A demonstration that "theory of everything" programs can make contact with observational cosmology.

The framework will be tested. Some of it will survive. Some of it will fail. The experiments that determine which is which are already underway.

---

## 13. Framework Revision Protocol

### 13.1 Motivation

A scientific framework should define in advance what types of evidence would trigger what types of revision. This prevents post-hoc rationalization and ensures that the decision to modify or abandon components of the framework is based on pre-established criteria.

### 13.2 Action Levels

Three levels of response are defined, corresponding to increasing severity of the required revision:

| Action Level | Trigger | Description |
|:--:|----------|-------------|
| **Model Update** | A specific model within the framework is falsified; the broader sector survives. | Revise or replace the specific model. Example: replace Model A (X060) with a different neutrino defect model. |
| **Sector Replacement** | An entire sector (cosmology, DM, abundance) is falsified; the dependency tier above survives. | Replace the sector with a new approach while preserving the higher tiers. Example: replace Λ(t) cosmology with an alternative dark energy mechanism. |
| **Framework Abandonment** | Internal logical contradiction demonstrated in Tier 0-1 derivations, or all predictions falsified. | Abandon the framework. Preserve salvageable mathematical results (causal set gravity bridge, defect topology insights) as standalone contributions. |

### 13.3 Observation-to-Action Matrix

The following matrix defines, for each possible experimental outcome, what action should be taken. This is a pre-commitment — the framework's proponents should not deviate from these responses when data arrives.

| Observation | Certainty Required | Action Level | Specific Response |
|------------|:--:|:--:|------------------|
| w(z) = −1.000 ± 0.01 | >3σ (Euclid+Roman) | **Sector Replacement** | Abandon Λ(t) model. Replace Tier 4 cosmology with one of the recovery paths (Section 12.1). Tier 0-3 survives. |
| Weak w(z) deviation (w ≈ −0.98) | ~2σ (Euclid alone) | **No action** | Wait for Roman. Do not revise. |
| w(z) ≠ −1 at >3σ with correct sign | >3σ | **Model Update** | Refine Λ(t) model with measured parameters. Framework survives its most critical test. |
| Inverted neutrino ordering | >5σ | **Model Update** | Replace Model A (φ⁴ kink). Develop alternative neutrino defect model (different defect type, different potential). Delocalized-defect ontology survives. |
| WIMP detected at LHC/direct | >5σ | **Model Update** | Abandon neutral-vortex DM candidate. Investigate whether the detected particle can be identified with a different TQM defect type. If not, accept external DM. |
| Axion detected | >5σ | **Model Update** | Compare to moduli-excitation prediction. If consistent: confirm TQM-axion. If inconsistent: abandon moduli DM candidate. |
| α demonstrated constant beyond log-normal width | >3σ | **Sector Replacement** | Abandon quantitative abundance framework (XB series). Retain identity/abundance distinction as conceptual framework. |
| M² ≠ ⟨k⟩ demonstrated | Proof | **Model Update** | Accept M² as measured input. Framework becomes one-parameter. Compression ratio still ~95%. |
| Internal contradiction in Tier 1 (QM derivation) | Proof | **Framework Abandonment** | Publish the contradiction. Preserve salvageable mathematical results. |
| All Tier 4 predictions fail | >3σ each | **Sector Replacement** | Replace entire cosmology + dark sector. Preserve Tier 0-3. |
| All Tier 2-4 predictions fail | >3σ each | **Framework Abandonment** | Only Tier 0-1 survives — insufficient for a research program. Abandon framework. |

### 13.4 Decision Rules

The following rules govern when to escalate between action levels:

1. **A single failed prediction at Model Update level** does not trigger Sector Replacement. Fix the model and continue.
2. **Two or more failed predictions within the same sector** triggers Sector Replacement. The sector is likely fundamentally wrong.
3. **Sector Replacement that fails to produce new testable predictions within 5 years** triggers a review of whether to abandon that sector entirely.
4. **Framework Abandonment** requires either (a) an internal logical contradiction demonstrated by independent researchers, or (b) failure of all predictions within a tier that leaves no salvageable explanatory power.
5. **No observation triggers Framework Abandonment from a single experiment.** The framework is modular by design. Only cumulative failure across multiple independent sectors justifies abandonment.

### 13.5 The Pre-Commitment Principle

This protocol is a pre-commitment. It is published before the relevant experimental results are available. The framework's proponents commit to following these decision rules regardless of the outcome. This is the strongest possible demonstration of scientific integrity: defining in advance what evidence would constitute falsification, and what action would follow.

---

*Hostile Audit Document, August 2026. This document will be updated as experimental results become available. The Revision Protocol (Section 13) is binding — future revisions may only add new observations and responses, not modify existing commitments.*
