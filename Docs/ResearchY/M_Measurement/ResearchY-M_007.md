# ResearchY-M_007 — Measurement-Program Synthesis

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_007 (permanent)
**Title:** Measurement-Program Synthesis
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `M_Measurement/ResearchY-M_007.md`
**Depends on:** ResearchY-D_020, D_036, D_037, D_038, M_001–M_006
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_007_Tests.cs`

---

## Purpose

**Synthesize all measurement results.** This audit unifies the D-chain (D96 → complex
state → observability) and the M-series (event → disturbance → feedback → information →
conservation → observer) into a single classified chain.

---

## 1. The unified chain with classifications

```
D96
 → pairing                                     [DERIVED — D_021, D_035]
     λ_k = λ_{N−k} (spectral degeneracy); complete pairing (mult ≥ 2) from complex
     observability
 → complex state                                [DERIVED — D_036]
     the {cos, sin} quadrature pair IS the [Re, Im] of ψ = |ψ|·e^{iθ}
 → reciprocity                                 [EMERGENT — D_037]
     the two-quadrature measurement basis {cos, sin} (orthogonal, equal norm)
 → observability                               [DERIVED — D_037/D_038]
     z = a + ib reconstruction exact; a alone ambiguous
 → measurement                                 [EMERGENT — M_001]
     an actualization event reads both quadratures of a distinguishable state
 → information                                 [DERIVED — M_004]
     the outcome resolves the state space (log₂ 95 ≈ 6.57 bits)
 → conservation                                [DERIVED — M_005]
     reveal + redistribute, not create (count conservation, QG216)
 → observer                                    [EMERGENT — M_006]
     the epistemic recipient; changes access, not the ontic state
```

---

## 2. Link-by-link classification

| Link | Status | Source |
|---|---|---|
| D96 → pairing | **DERIVED** | D_021 (λ_k=λ_{N−k}), D_035 (complete pairing) |
| pairing → complex state | **DERIVED** | D_036 ({cos,sin} = [Re,Im]) |
| complex state → reciprocity | **EMERGENT** | D_037 (the measurement basis) |
| reciprocity → observability | **DERIVED** | D_037 (z = a + ib exact) |
| observability → measurement | **EMERGENT** | M_001 (the actualization read) |
| measurement → disturbance | **DERIVED** | M_002 (phase-pinning) |
| disturbance → feedback | **DERIVED** | M_003 (outcome = initial condition) |
| feedback → information | **DERIVED** | M_004 (log₂ 95) |
| information → conservation | **DERIVED** | M_005 (reveal + redistribute) |
| information → observer | **EMERGENT** | M_006 (epistemic recipient) |

---

## Unified Theorem

> **Theorem (M_007).** The measurement chain is fully classified. D96 → pairing is
> DERIVED (λ_k = λ_{N−k}, D_021; complete pairing from complex observability, D_035);
> pairing → complex state is DERIVED (the {cos, sin} pair is [Re, Im], D_036);
> complex state → reciprocity is EMERGENT (the two-quadrature measurement basis,
> D_037); reciprocity → observability is DERIVED (z = a + ib exact, D_037);
> observability → measurement is EMERGENT (an actualization event reads both
> quadratures, M_001). The measurement program continues: disturbance = phase-pinning
> DERIVED (M_002); feedback DERIVED (M_003); information = log₂ 95 DERIVED (M_004);
> conservation = reveal + redistribute DERIVED (M_005); observer = epistemic recipient
> EMERGENT (M_006). The DERIVED links are consequences of the structure; the EMERGENT
> links are requirements/readings (the basis, the event, the recipient). The only
> BOUNDARY items remain the five irreducible inputs (R_001): {Difference, η},
> {Z2-paired sector}, {3 octave families}, {SU(2) gauge + j=1/2}, {v, m_e}. No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Each link is verified by its source audit (Section 2). (2) The
> DERIVED links trace to the structure; the EMERGENT links are requirements/readings
> (Sections 1–2). (3) No new boundary is introduced — the five R_001 inputs remain
> (Section 4). ∎

---

## Dependency Graph

```
D96
 └→ pairing (λ_k=λ_{N−k})                    [DERIVED — D_021/D_035]
    └→ complex state ({cos,sin}=[Re,Im])     [DERIVED — D_036]
       └→ reciprocity (two-quadrature basis) [EMERGENT — D_037]
          └→ observability (z=a+ib)          [DERIVED — D_037/D_038]
             └→ measurement (actualization)  [EMERGENT — M_001]
                └→ disturbance (phase-pin)   [DERIVED — M_002]
                └→ feedback (initial cond.)  [DERIVED — M_003]
                └→ information (log₂ 95)     [DERIVED — M_004]
                └→ conservation (reveal)     [DERIVED — M_005]
                └→ observer (epistemic)      [EMERGENT — M_006]
```

---

## Remaining Open Questions

1. **Born-weighted information (M_007 OP1).** The uniform gain is log₂ 95; the
   Born-weighted average (Shannon entropy of the realized record, I_occ = 0.7513 nats,
   QG228) refines this for non-uniform outcomes.
2. **Observer-network coupling (M_007 OP2).** Whether the observer's own reads feed
   back into its state (the observer as a full measurement-system network, M_003) — the
   final measurement-program extension.
3. **Measurement predictions (M_007 OP3).** The D_046 predictions (measurement-
   disturbance relations, no-cloning bounds, information-theoretic uncertainty) become
   the testable output of this program.

---

## xUnit Validation

The suite `Y_M_007_Tests.cs` validates the full chain numerically:

| Test | Verifies |
|---|---|
| `Y_M_007_PairingDerived` | λ_k = λ_{N−k} (the pairing, D_021) |
| `Y_M_007_ComplexState` | {cos, sin} = [Re, Im] (D_036) |
| `Y_M_007_ReciprocityBasis` | the two-quadrature basis (orthogonal, D_037) |
| `Y_M_007_Observability` | z = a + ib exact (D_037) |
| `Y_M_007_Measurement` | both quadratures read → outcome (M_001) |
| `Y_M_007_InformationConserved` | log₂ 95 = outcome + observer (M_004/M_005) |
| `Y_M_007_Run` | research report |

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the chain acyclic and complete? | **YES** (D96 → … → observer) |
| Are the DERIVED links structural consequences? | **YES** (verified) |
| Are the EMERGENT links requirements/readings? | **YES** (basis, event, recipient) |
| Does the program introduce a new boundary? | **NO** (the five R_001 inputs remain) |
| Is the observer required for the ontic structure? | **NO** (M_006) |
| Is information conserved? | **YES** (M_005) |

---

## Counterexamples

1. **Pairing removal**: the doublet structure collapses (D_021) — the chain breaks at
   the first DERIVED link.
2. **Single-quadrature read**: z ambiguous (D_037) — not a complete measurement.
3. **Observer removal**: the ontic structure remains (M_006) — only the recipient is
   gone.
4. **Measurement without disturbance**: impossible (M_002) — the phase is pinned.

---

## Classification

| Component | Status |
|---|---|
| pairing | **DERIVED** (D_021/D_035) |
| complex state | **DERIVED** (D_036) |
| reciprocity (basis) | **EMERGENT** (D_037) |
| observability | **DERIVED** (D_037/D_038) |
| measurement (event) | **EMERGENT** (M_001) |
| disturbance / feedback / information / conservation | **DERIVED** (M_002–M_005) |
| observer / epistemic access | **EMERGENT** (M_006) |
| five irreducible boundaries | **BOUNDARY** (R_001) |

**The measurement chain is fully classified: the structure is DERIVED, the
requirements/readings are EMERGENT, and the only boundaries are the five irreducible
inputs. No new primitive; canonical AT unchanged.**

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_007_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_M_007_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_007_PairingDerived` | λ_k = λ_{N−k} (D_021) | ✅ |
| `Y_M_007_ComplexState` | {cos, sin} = [Re, Im] (D_036) | ✅ |
| `Y_M_007_ReciprocityBasis` | the two-quadrature basis (D_037) | ✅ |
| `Y_M_007_Observability` | z = a + ib exact (D_037) | ✅ |
| `Y_M_007_Measurement` | both quadratures read → outcome (M_001) | ✅ |
| `Y_M_007_InformationConserved` | log₂ 95 = outcome + observer (M_004/M_005) | ✅ |
| `Y_M_007_Run` | research report | ✅ |

**Conclusion:** The measurement chain D96 → pairing → complex state → reciprocity →
observability → measurement is fully classified: pairing, complex state, observability
DERIVED; reciprocity, measurement, observer EMERGENT; the five R_001 boundaries remain
the only BOUNDARY inputs. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_007"`

---

## References

- ResearchY-D_020 (observable-sector construction), D_036 (complex state), D_037
  (observability/reciprocity), D_038 (state identity), M_001–M_006 (measurement
  program).
- AT-QG: QG216 (Born rule), QG228 (information), QG74 (measurement basis), R_001
  (five boundaries).
- Monograph V2.0: Ch9 (quantum mechanics).
