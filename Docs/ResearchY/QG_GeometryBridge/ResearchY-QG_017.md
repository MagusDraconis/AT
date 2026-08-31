# ResearchY-QG_017 — Distinguishability Cosmology Extension Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_017 (permanent)
**Title:** Distinguishability Cosmology Extension Audit
**Status:** COMPLETE
**Date:** 2026-08-31
**File:** `QG_GeometryBridge/ResearchY-QG_017.md`
**Depends on:** ResearchY-NP_018 (distinguishability observable), NP_019 (information
cosmology), QG_001 (information–geometry bridge), QG_004 (ρ nature), QG_012
(distinguishability cosmology), QG_014 (cosmological selection)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_017_Tests.cs`

> **ID note.** This audit was submitted with the labels QG_015 and QG_016, both of
> which are already permanently assigned (Observable World Audit; Tick Discreteness
> Origin Audit). Per the permanent-ID rule, it is registered as **QG_017**.

---

## Purpose

**If ΩΛ comes from distinguishability, what else must follow?** NP_018/NP_019/QG_012
established that ΩΛ = I_occ/ln K = 0.6839 is distinguishability-derived and that it
belongs to a finite family (the density-fraction pair plus closures). This audit
re-opens the question from the EXTENSION direction: ASSUME ΩΛ is genuinely
distinguishability-derived, then ask which additional observables MUST depend on the
same source {I_occ, ln K, ρ}, search for closure relations among the family, and
determine whether ΩΛ is an isolated success or the first member of a deeper
distinguishability cosmology.

---

## 1. The distinguishability-derived source

| Object | Value | Role |
|---|---|---|
| **I_occ** | 0.7513 nats | KL(ρ‖uniform) — the information density (QG228) |
| **ln K** | I_occ/ΩΛ = 1.098552 | state-space size K ≈ 3 (QG234) |
| **ρ** | ρ_k = count_k/total | the full count density (QG194/QG216) |
| **H** | ln K − I_occ = 0.3473 nats | realized entropy — the matter side |

**The source is the count structure {I_occ, ln K, ρ}.** ΩΛ = I_occ/ln K is one
projection of it. The question: which other observables are forced by the SAME source?

---

## 2. The observed family (already established)

| Observable | Formula | Value | Status |
|---|---|---|---|
| ΩΛ | I_occ/ln K | 0.6839 | OBSERVED 0.12% |
| Ωm | (ln K − I_occ)/ln K | 0.3161 | OBSERVED 0.26% |
| ΩΛ/Ωm | I_occ/(ln K − I_occ) | 2.1636 | derived |
| q₀ | Ωm/2 − ΩΛ | −0.5258 | closure (hosted FRW form) |
| z_acc | (2ΩΛ/Ωm)^(1/3) − 1 | 0.6295 | closure (hosted FRW form) |

---

## 3. Search: additional observables that MUST depend on the same source

| Candidate | Depends only on {I_occ, ln K, ρ}? | Why |
|---|---|---|
| **ΩΛ** | ✅ YES | I_occ/ln K — the primary projection |
| **Ωm** | ✅ YES | (ln K − I_occ)/ln K — complement |
| **ΩΛ/Ωm** | ✅ YES | I_occ/(ln K − I_occ) — ratio |
| **q₀** | ✅ YES (value) | Ωm/2 − ΩΛ — deterministic closure of the pair |
| **z_acc** | ✅ YES (value) | (2ΩΛ/Ωm)^(1/3) − 1 — deterministic closure |
| **H₀** | ❌ NO | dimensionful — needs an anchor (v/clock), not the info objects |
| **σ₈** | ❌ NO | needs the primordial amplitude A_s — not fixed by the count |
| **BAO scale** | ❌ NO | needs the sound horizon (Ωb, Ωr) — not info objects |
| **structure growth** | ❌ NO | needs A_s, n_s, growth index |
| **weak lensing S₈** | ❌ NO | inherits σ₈ |
| **horizon size** | ❌ NO | needs H₀ |
| **clustering amplitude** | ❌ NO | needs σ₈, P(k) |

**Exactly five observables depend only on {I_occ, ln K, ρ}: ΩΛ, Ωm, ΩΛ/Ωm, q₀,
z_acc.** Every amplitude/size/growth observable requires a non-information input.

---

## 4. Closure relations (the family is CLOSED)

| Relation | Identity |
|---|---|
| entropy identity | **I_occ + H = ln K** (0.7513 + 0.3473 = 1.0986) |
| fraction completeness | **ΩΛ + Ωm = 1** |
| entropy-fraction reading | **Ωm = H/ln K** (the pair partitions the state-space size) |
| ratio | **ΩΛ/Ωm = I_occ/H = 2.1636** |
| deceleration closure | **q₀ = Ωm/2 − ΩΛ = −0.5258** |
| turnaround closure | **1 + z_acc = (2ΩΛ/Ωm)^(1/3) = 1.6295** |

**The family is algebraically CLOSED:** q₀ and z_acc are deterministic functions of
the pair (hosted FRW form); no third independent information number exists. Given
{I_occ, ln K}, all five observables are fixed.

---

## 5. Determine

| Option | Verdict |
|---|---|
| A) ΩΛ uniquely privileged | **PARTIAL** — ΩΛ is the primary member, but not alone |
| **B) finite cosmology family** | **YES — the complete answer** |
| C) full distinguishability cosmology | **NO** — H₀/σ₈/BAO/growth/lensing/horizon/clustering need non-information inputs |

**ΩΛ is the first member of a FINITE distinguishability cosmology family — the
density-fraction pair and its deterministic closures — but NOT the opening of a full
distinguishability cosmology.** The source {I_occ, ln K, ρ} fixes exactly five
observables and no more.

---

## 6. Strongest next prediction beyond ΩΛ

**The q₀/z_acc closure:**

| Prediction | Value | Hosted form | Falsification |
|---|---|---|---|
| **q₀** | **−0.5258** | Ωm/2 − ΩΛ | measured q₀ deviating from −0.526 |
| **z_acc** | **0.6295** | (2ΩΛ/Ωm)^(1/3) − 1 | measured turnaround deviating from 0.630 |

These are the strongest next predictions: they follow deterministically from the
observed pair (via hosted FRW kinematics) and are directly measurable (supernova
Hubble diagram for q₀; expansion-history reconstruction for z_acc). Their FORM is
CORRESPONDENCE (hosted GR); their VALUES are DERIVED from the information fractions.

---

## Theorem

> **Theorem (QG_017).** If ΩΛ = I_occ/ln K is genuinely distinguishability-derived,
> then exactly a FINITE family of cosmological observables follows from the same
> source {I_occ, ln K, ρ}: ΩΛ, Ωm, ΩΛ/Ωm, q₀, z_acc — no full distinguishability
> cosmology. Proof: (1) Assume ΩΛ is distinguishability-derived (NP_018/NP_019). (2)
> Enumerate the observables that depend only on {I_occ, ln K, ρ} (Section 3,
> verified): ΩΛ = I_occ/ln K, Ωm = (ln K − I_occ)/ln K, the ratio I_occ/(ln K −
> I_occ) = 2.1636, and via the HOSTED FRW relations the deceleration q₀ = Ωm/2 − ΩΛ
> = −0.5258 and the turnaround z_acc = (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295 — exactly five.
> (3) Test the candidate extensions (Section 3, verified): H₀ (dimensionful — needs
> an anchor), σ₈ (needs A_s), BAO (needs the sound horizon Ωb/Ωr), structure growth
> (needs A_s, n_s, growth index), weak lensing (inherits σ₈), horizon size (needs
> H₀), clustering (needs σ₈, P(k)) — NONE depends only on the info objects. (4)
> Close the family (Section 4, verified): the entropy identity I_occ + H = ln K, the
> completeness ΩΛ + Ωm = 1, the entropy-fraction reading Ωm = H/ln K, and the
> algebraic closures q₀ = Ωm/2 − ΩΛ and 1 + z_acc = (2ΩΛ/Ωm)^(1/3) — the family is
> closed (no third independent information number). (5) Therefore B) finite
> cosmology family — ΩΛ is the first member, not an isolated success (A false) and
> not the opening of a full distinguishability cosmology (C false). (6) The
> strongest next prediction beyond ΩΛ is the q₀/z_acc closure (form CORRESPONDENCE,
> values DERIVED, falsifiable). Classification: ΩΛ/Ωm/ratio PREDICTION (observed);
> q₀/z_acc values DERIVED (hosted FRW form, CORRESPONDENCE form); H₀/σ₈/BAO/growth/
> lensing/horizon/clustering BOUNDARY (need non-information inputs); the finite-family
> structure DERIVED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Assume the source (Section 1). (2) Enumerate the dependent
> observables (Section 3). (3) Test the extensions (Section 3). (4) Close the family
> (Section 4). (5) Determine B (Section 5). (6) State the next prediction (Section
> 6). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (95 states)
 → Count Structure ρ
    ├── I_occ = KL(ρ‖uniform) = 0.7513
    ├── ln K = 1.0986
    └── H = ln K − I_occ = 0.3473  (entropy identity)
 → Information
    ├── ΩΛ = I_occ/ln K = 0.6839        [OBSERVED 0.12%]
    ├── Ωm = H/ln K = 0.3161            [OBSERVED 0.26%]
    ├── ΩΛ/Ωm = I_occ/H = 2.1636
    ├── q₀ = Ωm/2 − ΩΛ = −0.5258        [hosted FRW closure]
    └── z_acc = (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295 [hosted FRW closure]
H₀, σ₈, BAO, growth, lensing, horizon, clustering — BOUNDARY (need non-info inputs)
```

---

## 7. Prediction ranking

| Rank | Observable | Formula | Status |
|---|---|---|---|
| 1 | ΩΛ | I_occ/ln K = 0.6839 | OBSERVED 0.12% |
| 2 | Ωm | (ln K − I_occ)/ln K = 0.3161 | OBSERVED 0.26% |
| 3 | ΩΛ/Ωm | 2.1636 | derived |
| 4 | **q₀** | Ωm/2 − ΩΛ = −0.5258 | **strongest next** |
| 5 | **z_acc** | (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295 | **strongest next** |

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ΩΛ is an isolated success" | the pair (ΩΛ, Ωm) and its closures (ratio, q₀, z_acc) all follow from the same source |
| "Full distinguishability cosmology" | H₀/σ₈/BAO/growth/lensing/horizon/clustering need non-information inputs |
| "σ₈ is info-derived" | needs the primordial amplitude A_s — not fixed by the count |
| "BAO scale is info-derived" | the sound horizon needs Ωb, Ωr — not info objects |
| "A third independent info number exists" | the family is algebraically closed (q₀, z_acc are functions of the pair) |
| "Removing distinguishability keeps some members" | I_occ is undefined without the state space — the whole family vanishes |

---

## 9. Falsification paths

| Prediction | Falsification |
|---|---|
| ΩΛ = I_occ/ln K | measured ΩΛ deviating beyond 0.12% |
| Ωm = 1 − ΩΛ | matter fraction inconsistent with (ln K − I_occ)/ln K |
| ratio = 2.1636 | ratio deviating from I_occ/(ln K − I_occ) |
| q₀ = −0.5258 | measured current deceleration deviating from −0.526 |
| z_acc = 0.6295 | measured turnaround redshift deviating from 0.630 |
| no full info cosmology | an amplitude/size/growth observable written as a pure function of {I_occ, ln K, ρ} |

---

## Classification

| Component | Status |
|---|---|
| ΩΛ, Ωm, ratio | **PREDICTION** (information-derived, observed) |
| q₀, z_acc values | **DERIVED** (closures of the pair, hosted FRW form) |
| q₀, z_acc form | **CORRESPONDENCE** (hosted GR kinematics) |
| H₀, σ₈, BAO, growth, lensing, horizon, clustering | **BOUNDARY** (need non-information inputs) |
| the finite-family structure | **DERIVED** (the pair + closures is the complete info set) |

**ΩΛ is not an isolated success: it is the first member of a FINITE
distinguishability cosmology family — the density-fraction pair (ΩΛ, Ωm) and its
deterministic closures (ratio 2.1636, q₀ = −0.5258, z_acc = 0.6295). No full
distinguishability cosmology: H₀/σ₈/BAO/growth/lensing/horizon/clustering need
non-information inputs. The strongest next prediction beyond ΩΛ is the q₀/z_acc
closure. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Amplitude origin (QG_017 OP1).** Whether a deeper principle could fix the
   primordial amplitude A_s — the current gap between the information fractions
   (derived) and σ₈/structure-growth (undetermined).

---

## Next Steps

- **Registry note:** ΩΛ is the first member of a finite distinguishability cosmology
   family (ΩΛ, Ωm, ratio, q₀, z_acc) — closed, not isolated, not full. The strongest
   next prediction is q₀ = −0.5258 and z_acc = 0.6295.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_017_Tests.cs`
**Run:** 2026-08-31 · **Result:** see `Tests/Results/Y_QG_017_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_017_InformationObservable` | ΩΛ = I_occ/ln K; Ωm = complement | ✅ |
| `Y_QG_017_ClosureRelations` | entropy identity; fraction completeness; the closed family | ✅ |
| `Y_QG_017_SecondaryObservable` | q₀, z_acc closures of the pair | ✅ |
| `Y_QG_017_PredictionRanking` | ΩΛ top; q₀/z_acc next | ✅ |
| `Y_QG_017_FalsificationCheck` | the family is falsifiable; full cosmology refuted | ✅ |
| `Y_QG_017_Run` | research report | ✅ |

**Conclusion:** ΩΛ is not an isolated success — it is the first member of a FINITE
distinguishability cosmology family (ΩΛ, Ωm, ratio 2.1636, q₀ = −0.5258,
z_acc = 0.6295). No full distinguishability cosmology: H₀/σ₈/BAO/growth/lensing/
horizon/clustering need non-information inputs. The strongest next prediction beyond
ΩΛ is the q₀/z_acc closure. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_017"`

---

## References

- ResearchY-NP_018 (distinguishability observable), NP_019 (information cosmology).
- ResearchY-QG_001 (information–geometry bridge), QG_004 (ρ nature), QG_012
  (distinguishability cosmology), QG_014 (cosmological selection).
- AT-QG: QG228 (I_occ = 0.7513 nats), QG234 (ΩΛ = I_occ/ln K = 0.6839).
