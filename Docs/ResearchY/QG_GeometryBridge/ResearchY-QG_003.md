# ResearchY-QG_003 — Information Reconstruction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_003 (permanent)
**Title:** Information Reconstruction Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `QG_GeometryBridge/ResearchY-QG_003.md`
**Depends on:** ResearchY-QG_001 (information–geometry bridge), QG_002
(distinguishability → geometry), NP_018 (distinguishability observable), NP_019
(information cosmology)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_003_Tests.cs`

---

## Purpose

**Can geometry be reconstructed from information alone?** QG_001/QG_002 established
that ρ generates both the metric (g = ρ^(2/d)η) and the information (I =
KL(ρ‖uniform)). This audit asks the inverse question: is INFORMATION sufficient to
recover the metric? Can ρ be reconstructed from I, ΩΛ, Ωm alone?

---

## 1. The reconstruction question

| Given (information only) | Want (geometry) | Unique? |
|---|---|---|
| I = I_occ = 0.7513 nats | ρ (the count density) | **NO — a scalar cannot determine a distribution** |
| ΩΛ = 0.6839 | ln K | **YES — ln K = I_occ/ΩΛ = 1.0986** |
| ρ | g = ρ^(2/d)η | YES (if ρ were known) |

---

## 2. Can ρ be reconstructed from I, ΩΛ, Ωm?

**Only partially.** The information content I = KL(ρ‖uniform) is ONE scalar; ρ is a
full distribution. MANY distributions have the same KL-divergence, so I alone does not
determine ρ uniquely. ΩΛ does determine ln K uniquely (ln K = I_occ/ΩΛ = 1.0986,
K ≈ 3), but the full distribution ρ requires the STATE STRUCTURE, not just the scalar
information.

| Quantity | Reconstructible from I alone? |
|---|---|
| ln K | YES — ln K = I_occ/ΩΛ = 1.0986 |
| K (state-space size) | YES — K ≈ 3 |
| ρ (full distribution) | **NO — a scalar KL does not fix a distribution** |
| g (metric) | **NO — requires ρ** |

---

## 3. Can g be reconstructed from I alone?

**NO.** The metric g = ρ^(2/d)η requires ρ. Information alone (the scalar I = I_occ)
does not determine ρ, so it cannot determine g. The reconstruction chain is:

```
Information (I) → ρ → g = ρ^(2/d)η
```

and the first step (I → ρ) is NOT invertible: I is a scalar, ρ is a distribution.

---

## 4. Compare: Information → Geometry vs Geometry → Information

| Direction | Works? | Why |
|---|---|---|
| **Information → Geometry** | **NO** — I is a scalar; ρ (needed for g) is not uniquely determined | information is DERIVED from ρ, not ρ from information |
| **Geometry → Information** | **NO** — g is also derived from ρ, not the source of ρ | geometry is derived from ρ, not the source of information |

**Neither direction inverts the chain.** ρ is the source; information and geometry are
both derived FROM ρ. The state structure (N=96 → spectrum → ρ) is what fixes ρ —
neither I alone nor g alone suffices.

---

## 5. Prove or refute: geometry is informationally complete

**REFUTED — geometry is NOT informationally complete.**

The information content I = KL(ρ‖uniform) is a single scalar. A scalar cannot
determine a distribution. Therefore:
1. ρ is not uniquely reconstructible from I.
2. g = ρ^(2/d)η is not reconstructible from I alone.
3. The reconstruction requires the STATE STRUCTURE (N=96 → spectrum → ρ), which is
   the actual primitive — not the information.

**Information alone is insufficient to recover the metric.**

---

## Theorem

> **Theorem (QG_003).** Geometry is NOT informationally complete: information alone
> cannot reconstruct the metric. Proof: (1) The information content is a single scalar,
> I = KL(ρ‖uniform) = I_occ = 0.7513 nats (QG228). (2) ρ is a full distribution; MANY
> distributions have the same KL-divergence, so I does not determine ρ uniquely
> (verified: ΩΛ determines ln K = I_occ/ΩΛ = 1.0986, K ≈ 3 — the state-space SIZE —
> but not the distribution itself). (3) The metric g = ρ^(2/d)η (QG197) requires ρ;
> since ρ is not uniquely determined by I, g is not reconstructible from I alone. (4)
> The reconstruction chain Information → ρ → g FAILS at the first step: I is a scalar,
> ρ is a distribution. (5) The chain is one-directional: STATE STRUCTURE → ρ → {I, g};
> information and geometry are both DERIVED from ρ, so neither inverts it. (6)
> Therefore geometry is NOT informationally complete — the information content does
> not suffice; the state structure (N=96) is the actual primitive that fixes ρ.
> Classification: ρ DERIVED (from the state structure); I = KL(ρ‖uniform) DERIVED
> (QG228); g = ρ^(2/d)η DERIVED (QG197); the I → ρ inversion REFUTED (a scalar cannot
> determine a distribution); information-only reconstruction BOUNDARY (requires the
> state structure). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) State the reconstruction question (Section 1). (2) Test ρ from I
> (Section 2, verified: ln K = 1.0986, but ρ not unique). (3) Test g from I
> (Section 3, verified: fails at I → ρ). (4) Compare directions (Section 4). (5) Refute
> informational completeness (Section 5). ∎

---

## Dependency Graph

```
Information (I = KL(ρ‖uniform), QG228)
 → ρ
 → Geometry (g = ρ^(2/d)η, QG197)

FAILS: I is a scalar, ρ is a distribution

State structure (N=96, D_039) — THE ACTUAL PRIMITIVE
 → Spectrum
 → ρ
 → Information (I)
 → Geometry (g)
```

---

## 6. Reconstruction chain (correct direction)

```
State structure (N=96) → spectrum → ρ → {I = KL(ρ‖uniform), g = ρ^(2/d)η}
```

The forward chain is well-defined and pure-functional (QG_002). The inverse
(information → ρ → g) is NOT invertible: I is one scalar, ρ is a distribution.

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| geometry is informationally complete | a metric successfully reconstructed from I alone (no state structure) |
| I uniquely determines ρ | two distinct ρ with the same KL-divergence producing different g (counterexample constructed) |

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "I determines ρ" | many distributions share a KL-divergence; I is a scalar, ρ is a distribution |
| "g from I alone" | g = ρ^(2/d)η needs ρ; ρ is not fixed by I |
| "Information → Geometry" | the chain Information → ρ fails at the first step |
| "ΩΛ determines the metric" | ΩΛ fixes ln K (the state-space size), not the full ρ |

---

## Classification

| Component | Status |
|---|---|
| ρ (count density) | **DERIVED** (from the state structure) |
| I = KL(ρ‖uniform) | **DERIVED** (QG228) |
| g = ρ^(2/d)η | **DERIVED** (QG197) |
| I → ρ inversion | **REFUTED** (a scalar cannot determine a distribution) |
| information-only reconstruction | **BOUNDARY** (requires the state structure) |

**Geometry is NOT informationally complete: information alone cannot reconstruct the
metric. The state structure (N=96) is the actual primitive that fixes ρ; information
and geometry are both its derived faces. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Information-plus-structure reconstruction (QG_003 OP1).** Whether the state
   structure plus the information scalar (N + I) uniquely fixes ρ and hence g (the
   forward chain already gives this; the question is about independent recovery).

---

## Next Steps

- **Registry note:** information alone does not reconstruct geometry; the state
  structure is required.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_003_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_QG_003_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_003_InformationToRho` | I is a scalar; ρ not unique | ✅ |
| `Y_QG_003_RhoToMetric` | g = ρ^(2/d)η needs ρ | ✅ |
| `Y_QG_003_MetricReconstruction` | g not reconstructible from I alone | ✅ |
| `Y_QG_003_InformationCompleteness` | geometry NOT informationally complete | ✅ |
| `Y_QG_003_ReconstructionChain` | the correct chain is state structure → ρ → {I, g} | ✅ |
| `Y_QG_003_Run` | research report | ✅ |

**Conclusion:** Geometry is NOT informationally complete — information alone cannot
reconstruct the metric. I = KL(ρ‖uniform) is a scalar; ρ is a distribution; the metric
g = ρ^(2/d)η requires ρ. The state structure (N=96) is the actual primitive. No new
primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_003"`

---

## References

- ResearchY-QG_001 (information–geometry bridge), QG_002 (distinguishability →
  geometry), NP_018 (distinguishability observable), NP_019 (information cosmology).
- AT-QG: QG197 (metric ansatz), QG228 (information I = KL(ρ‖uniform)), QG234
  (ΩΛ = I_occ/ln K).
