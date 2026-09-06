# ResearchY-NP_057 — Dark Energy Meaning Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_057 (permanent)
**Title:** Dark Energy Meaning Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_057.md`
**Depends on:** ResearchY-NP_055 (dark energy ontology: information-bookkeeping fraction,
w unresolved), NP_056 (no unique equation of state), ResearchY-QG_018 (info-cosmology
closure), AT-QG QG234 (ΩΛ = I_occ/ln K), QG228 (I_occ = KL(ρ‖uniform)), QG230 (Λ ∝ 1/R²),
QG195/196 (matter = deficit), NP_030 (I_occ as order parameter), D_039 (95 states),
D_041 (spectrum), A_003 (occupancy [4,4,87]), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_057_Tests.cs`

---

## Purpose

NP_055 fixed the *ontology* (ΩΛ = the information-bookkeeping fraction) and NP_056 fixed
the *dynamics* (no unique equation of state). NP_057 asks the deepest question: **what
does ΩΛ physically represent — and why should information content appear as a
cosmological density fraction?** Program: (1) inventory every quantity; (2) determine
what ΩΛ measures (surplus / deficit / unused state-space / pressure / bookkeeping);
(3) search equivalent formulations; (4) determine whether ΩΛ is a cause, an effect, or a
state descriptor; (5) compare with vacuum energy, a cosmological constant, and
entropy-based interpretations. **Success criterion:** the strongest ontological
interpretation of ΩΛ currently supported. No new primitives; canonical AT unchanged.

---

## 1. Inventory — every quantity used

| Object | Role | Value |
|---|---|---|
| Difference | the primitive | BOUNDARY |
| Actualization | discrete tick → events | BOUNDARY (QG_011) |
| D96 spectrum | λ_k = 2−2cos(2πk/N), N=96 | DERIVED (D_041) |
| Occupancy | [4, 4, 87] (95 modes, top-heavy) | DERIVED (A_003/D_030) |
| count density ρ | [4,4,87]/95 = [0.0421, 0.0421, 0.9158] | DERIVED (QG194/216) |
| Shannon entropy H | −Σρ ln ρ = 0.3473 nats | DERIVED |
| max entropy ln K | ln 3 = 1.0986 nats (uniform reference) | DERIVED (QG227) |
| information density I_occ | KL(ρ‖uniform) = ln K − H = 0.7513 nats | DERIVED (QG228) |
| **ΩΛ** | I_occ/ln K = **0.6839** | OBSERVED 0.12% |
| **Ωm** | H/ln K = **0.3161** | OBSERVED 0.26% |

**The key identity (verified):** I_occ = ln K − H (KL = max-entropy − realized-entropy).

---

## 2. What does ΩΛ measure?

The five candidate readings resolve cleanly because **A, C, and E are the SAME
quantity**, and **B is its complement**:

```
I_occ = KL(ρ‖uniform) = ln K − H = 0.7513 nats
ΩΛ  = I_occ/ln K = (ln K − H)/ln K = 1 − H/ln K = 0.6839
Ωm  = H/ln K     = realized-entropy fraction   = 0.3161
```

| Reading | Formula | Measures | Describes |
|---|---|---|---|
| **A) information surplus** | I_occ/ln K | the KL excess over the uniform prior | **ΩΛ** ✅ |
| B) information deficit | H/ln K | the realized entropy (gap from max) | **Ωm** ❌ |
| **C) unused state-space** | (ln K − H)/ln K | the capacity left unrealized | **ΩΛ** ✅ |
| D) actualization pressure | — | (metaphor; μ=2 anti-thermal, NP_030) | — ❌ |
| **E) pure bookkeeping** | I_occ/ln K | the partition of ln K | **ΩΛ** ✅ |

**Determination: A = C = E.** ΩΛ measures the **normalized entropy deficit** — the
fraction of the state-space's information capacity that the top-heavy record fails to
realize as entropy. Equivalently, it is the **information surplus** over the uniform
(max-entropy) prior. B (the deficit/realized-entropy) is **Ωm = H/ln K — matter**, not
dark energy. D is a metaphor (the canonical branching is anti-thermal, NP_030).

**Verified as an order parameter:** ΩΛ = 0 for the uniform record ([1/3,1/3,1/3]), and it
*grows* with top-heaviness — [4,4,87] → 0.6839, the more extreme [1,1,93] → 0.8938.
ΩΛ is a **monotone measure of how non-uniform (structured) the D96 occupancy is**.

---

## 3. Equivalent formulations

| Formulation | Value |
|---|---|
| ΩΛ = I_occ/ln K | 0.6839 |
| ΩΛ = KL(ρ‖uniform)/ln K | 0.6839 |
| ΩΛ = (ln K − H)/ln K | 0.6839 |
| ΩΛ = 1 − H/ln K = 1 − Ωm | 0.6839 |

All four are the **same object**: the normalized entropy deficit (= information surplus)
of the realized occupancy relative to the uniform reference. The pair (ΩΛ, Ωm) is the
**information budget** I_occ + H = ln K, which AT *identifies* with the cosmological
**energy budget** (flatness ΩΛ + Ωm = 1).

---

## 4. Is ΩΛ a cause, an effect, or a state descriptor?

| Reading | Verdict |
|---|---|
| **cause** | **NO** — ΩΛ is a dimensionless fraction; it drives nothing (NP_056: no dynamics) |
| **effect** | **partial** — it is produced by the top-heavy occupancy [4,4,87], but it does not itself act |
| **state descriptor** | **YES — the strongest reading.** ΩΛ is an order parameter of the realized count density ρ, exactly as I_occ was classified (NP_030): a snapshot measure of departure from uniformity |

**ΩΛ is a STATE DESCRIPTOR (order parameter), not a cause and not an energy-density
effect.** It tells us how structured (non-uniform) the realized D96 occupancy is; it does
not *produce* acceleration or any dynamics (NP_056).

---

## 5. Compare with vacuum energy / cosmological constant / entropy

| Interpretation | Status in AT |
|---|---|
| **vacuum energy** (ρ_Λ = Λ/8πG, w = −1) | **CORRESPONDENCE** — no derived energy density; w = −1 hosted, QG230's Λ ∝ 1/R² gives w = −1/3 |
| **cosmological constant** (Λ = const) | **CORRESPONDENCE** — QG230's Λ ∝ 1/R² is NOT constant; AT has no constant Λ |
| **entropy/information (holographic family)** | **YES — this is the natural family.** ΩΛ = (ln K − H)/ln K is an entropy-deficit fraction, kin to entropic/holographic dark-energy ideas, but with an AT-specific form |

**AT's dark energy belongs to the entropy/information family, not the vacuum-energy
family.** It is a deficit of realized entropy, not a constant energy density.

---

## 6. Why should information appear as a density fraction? — the open bridge

AT derives the *number* ΩΛ = I_occ/ln K = 0.6839 from the counting measure, and the
*number* matches Planck ΩΛ = 0.6847 to 0.12%. But the **bridge** — why a normalized
information deficit should equal an energy-density fraction — is **not derived**. AT
offers no mechanism converting nats into erg/cm³. The identification is therefore:

```
I_occ + H = ln K   (information budget — DERIVED)
      ⇕   identified with   ⇕
ΩΛ + Ωm = 1        (energy budget — flatness, OBSERVED)
```

The identification is **empirical CORRESPONDENCE**: a numeric match plus a structural
analogy (information partition ↔ energy partition), with the information→energy link
**OPEN (BOUNDARY)**.

---

## Theorem

> **Theorem (NP_057).** ΩΛ physically represents the normalized ENTROPY DEFICIT — the
> information surplus of the realized D96 occupancy over the uniform prior — and is a
> STATE DESCRIPTOR (order parameter), not a cause and not an energy density. Proof:
> (1) Inventory (Section 1, verified): I_occ = KL(ρ‖uniform) = ln K − H = 0.7513 nats.
> (2) Measurement (Section 2, verified): A = C = E (ΩΛ = I_occ/ln K = (ln K − H)/ln K =
> unused-state-space fraction = bookkeeping); B is the complement Ωm = H/ln K (matter);
> D is metaphorical (μ = 2 anti-thermal, NP_030); ΩΛ is monotone in top-heaviness
> (0 for uniform, 0.6839 for [4,4,87], 0.8938 for [1,1,93]). (3) Equivalent
> formulations (Section 3, verified): I_occ/ln K = KL/ln K = (ln K − H)/ln K =
> 1 − H/ln K = 0.6839. (4) Cause/effect/descriptor (Section 4): ΩΛ drives nothing
> (NP_056), it is a snapshot order parameter of ρ (NP_030). (5) Comparison (Section 5):
> vacuum energy and cosmological constant are CORRESPONDENCE (hosted); the
> entropy/information family is the natural home. (6) The information→energy bridge
> (Section 6) is an empirical CORRESPONDENCE with an OPEN (BOUNDARY) mechanism.
> Classification: ΩΛ = I_occ/ln K DERIVED (QG234); the meaning "normalized entropy
> deficit / information surplus" DERIVED (KL structure); state-descriptor (order
> parameter) status DERIVED (NP_030); the identification with the cosmological energy
> fraction CORRESPONDENCE (empirical, no mechanism); vacuum-energy / cosmological
> constant ontology CORRESPONDENCE (hosted); actualization-pressure reading REFUTED as
> physical (NP_030). **Strongest interpretation: ΩΛ is the normalized entropy deficit
> (information surplus) of the realized D96 occupancy — a DERIVED state descriptor in
> the entropic/informational family, whose identification with the energy-density
> fraction is an empirical CORRESPONDENCE with an open information→energy bridge.** No
> new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Measure A–E. (3) Equivalent forms. (4) Descriptor
> status. (5) Compare families. (6) Locate the open bridge. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ΩΛ is the information deficit" | the deficit (realized entropy) is Ωm = H/ln K; ΩΛ is the surplus/excess |
| "ΩΛ is actualization pressure" | μ = 2 branching is a population inversion (anti-thermal, NP_030) — a metaphor, not a measure |
| "ΩΛ is vacuum energy" | no energy density, no derived w = −1 (NP_056); QG230's Λ ∝ 1/R² ≠ constant |
| "ΩΛ is a cosmological constant" | QG230's Λ ∝ 1/R² is NOT constant |
| "ΩΛ is a cause" | it is a dimensionless order parameter that drives nothing (NP_056) |
| "information = energy is derived" | no nats → erg/cm³ mechanism exists; the bridge is empirical |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| ΩΛ = the normalized entropy deficit | a measured ΩΛ inconsistent with (ln K − H)/ln K for the canonical ρ |
| ΩΛ is a state descriptor, not a cause | an observable that ΩΛ *drives* (a dynamics) rather than describes |
| the information→energy bridge is open | a derivation converting I_occ (nats) to ρ_Λ (energy density) from canonical structure |
| A = C = E (surplus = unused = bookkeeping) | a reading where these three are distinct for the canonical ρ |
| the entropic family is natural | a formulation of ΩΛ as a constant energy density (vacuum) without hosting ΛCDM |

---

## 9. Classification

| Component | Status |
|---|---|
| ΩΛ = I_occ/ln K = 0.6839 (the value) | **DERIVED** (QG234), OBSERVED 0.12% |
| meaning: normalized entropy deficit / information surplus (A = C = E) | **DERIVED** (KL structure) |
| state-descriptor (order-parameter) status | **DERIVED** (NP_030) |
| identification with the cosmological energy fraction | **CORRESPONDENCE** (empirical; information→energy bridge BOUNDARY/open) |
| vacuum-energy / cosmological-constant ontology | **CORRESPONDENCE** (hosted ΛCDM) |
| actualization-pressure reading | **REFUTED** as physical (anti-thermal, NP_030) |
| ΩΛ as a cause / dynamics | **REFUTED** (no dynamics, NP_056) |

**Conclusion.** ΩΛ physically represents the **normalized entropy deficit** — the
information surplus of the realized top-heavy D96 occupancy over the uniform prior. It
is a **DERIVED state descriptor (order parameter)**, not a cause and not an energy
density. It belongs to the **entropy/information family** of dark-energy interpretations,
not to vacuum energy or a cosmological constant. The identification of this information
fraction with the observed cosmological energy fraction is an **empirical CORRESPONDENCE**
whose "why" (information → energy) remains **OPEN (BOUNDARY)**. **Strongest supported
interpretation: ΩΛ = the realized occupancy's information surplus, normalized by the
state-space capacity — a state descriptor of the count structure.** No new primitive;
canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_057_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_057_EntropyDeficitFormulation` | ΩΛ = (ln K − H)/ln K = 1 − H/ln K | ✅ |
| `Y_NP_057_SurplusVsDeficit` | surplus (I_occ/ln K) = ΩΛ; deficit (H/ln K) = Ωm | ✅ |
| `Y_NP_057_UnusedStateSpace` | unused = ln K − H = I_occ ⇒ ΩΛ (= A) | ✅ |
| `Y_NP_057_OrderParameter` | ΩΛ monotone in top-heaviness (0 → 0.6839 → 0.8938) | ✅ |
| `Y_NP_057_EquivalentFormulations` | I_occ/ln K = KL/ln K = (ln K−H)/ln K = 1−Ωm | ✅ |
| `Y_NP_057_StateDescriptorNotCause` | ΩΛ drives nothing; a function of ρ alone | ✅ |
| `Y_NP_057_VacuumVsConstantVsEntropy` | vacuum/const CORRESPONDENCE; entropy family natural | ✅ |
| `Y_NP_057_Classification` | surplus/descriptor DERIVED; identification CORRESPONDENCE | ✅ |
| `Y_NP_057_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_057"`

---

## References

- ResearchY-NP_055 (dark energy ontology), NP_056 (no unique equation of state), NP_030
  (I_occ as order parameter; anti-thermal branching), QG_018 (info-cosmology closure).
- AT-QG: QG234 (ΩΛ = I_occ/ln K), QG228 (I_occ = KL(ρ‖uniform)), QG230 (Λ ∝ 1/R²),
  QG227 (initial uniform state), QG195/196 (matter = deficit), QG194/QG216 (count density).
- ResearchY: D_039 (95 states), D_041 (spectrum), A_003 (occupancy [4,4,87]), S_001.
