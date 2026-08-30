# ResearchY-NP_006 — Phase-Locking Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_006 (permanent)
**Title:** Phase-Locking Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_006.md`
**Depends on:** ResearchY-NP_003 (the phase lever), NP_004 (phase coupling), NP_005
(missing synchronization mechanism)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_006_Tests.cs`

---

## Purpose

**Does a phase-locking term emerge from Actualization?** NP_005 identified the missing
synchronization mechanism: a cross-phase feedback term κ·sin(θ_B−θ_A). This audit asks
whether that term can be DERIVED from the actualization process — specifically from
count redistribution and the interference structure — rather than introduced
externally.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **phase locking** | the relative phase converging to a fixed value (a fixed point of the evolution) |
| **synchronization** | the relative phase becoming time-invariant |
| **coupling** | an influence of one system's phase on another's behavior |
| **actualization interaction** | a mechanism by which actualization events couple two systems' evolution |

---

## 2. Independent vs shared actualization

| | Independent actualization | Shared actualization |
|---|---|---|
| events | each system actualizes alone | one event reads BOTH systems |
| phase | self-rate only (Δθ) | both pinned at the read (M_002) |
| relative phase | drifts linearly | definite AT the event, drifts after |

**Shared actualization pins both phases once, but does not persist — the fixed
self-rates drift the relative phase apart afterwards (NP_004).**

---

## 3. Can count redistribution generate a phase-coupling term?

The only phase-dependent observable in the derived chain is the two-mode interference
intensity (Born rule, QG216; complex state, D_036):

```
I = ρ_A + ρ_B + 2√(ρ_Aρ_B)·cos(θ_A − θ_B)
```

**The gradient of the intensity w.r.t. θ_A is exactly the Kuramoto form:**

```
∂I/∂θ_A = −2√(ρ_Aρ_B)·sin(θ_A − θ_B) = +2√(ρ_Aρ_B)·sin(θ_B − θ_A)
```

So the coupling coefficient is:

```
κ = 2√(ρ_Aρ_B)
```

This is the interference CROSS-AMPLITUDE — a DERIVED quantity (Born rule: the
cross-term amplitude 2√(ρ_Aρ_B) follows from the complex-state norm structure,
D_036/QG216).

**The FORM sin(θ_B−θ_A) and the STRENGTH κ = 2√(ρ_Aρ_B) are both derivable from the
interference structure.** What is NOT in the canonical chain is a phase update that
follows this gradient.

---

## 4. Analysis: shared events, reciprocity, feedback, information

| Candidate source | Provides the locking term? |
|---|---|
| **shared events** | joint pinning (one-time correlation) — but drift resumes after (NP_004) |
| **reciprocity** | the read is symmetric (D_037) — no phase feedback into evolution |
| **phase feedback** | NOT in the canonical update — the evolution has only the self-rate (NP_005) |
| **information exchange** | measurement redistributes information (M_005) — no cross-phase evolution term |

**None of the canonical mechanisms supplies a persistent cross-phase term.** The
canonical phase update θ(t+1) = θ(t) + Δθ (D_041) never reads the partner's phase.

---

## 5. Determination

| Option | Verdict |
|---|---|
| **A) derivable** | **PARTIAL — the FORM and STRENGTH are derivable** (κ = 2√(ρ_Aρ_B) from the interference gradient), but the EVOLUTION-TERM requires a phase update that follows the gradient |
| **B) emergent** | **CONDITIONAL — emergent IF actualization is variational** (phase advances along the intensity gradient) |
| **C) external boundary** | **YES for the MECHANISM in canonical AT** — no gradient-following phase update exists in the derived chain |

**κ·sin(θ_B−θ_A) is derivable as a FORM (the interference gradient) with a DERIVED
coefficient κ = 2√(ρ_Aρ_B), but it does NOT emerge as an evolution term in canonical
AT — the locking mechanism would require a variational actualization principle the
chain does not contain.**

---

## 6. Smallest modification of the actualization chain

**Allow the phase advance to follow the interference gradient:**

```
θ_A(t+1) = θ_A(t) + Δθ_A + η·(∂I/∂θ_A)
         = θ_A(t) + Δθ_A + 2η√(ρ_Aρ_B)·sin(θ_B − θ_A)
```

This is the Kuramoto coupling with κ = 2η√(ρ_Aρ_B). The locking threshold (NP_005)
becomes:

```
2η√(ρ_Aρ_B) ≥ |Δθ_A − Δθ_B|/2
```

The smallest modification is ONE variational requirement: the phase advance follows
the count-intensity gradient. With it, the locking coefficient is fixed by the Born
amplitudes (κ = 2√(ρ_Aρ_B) for η = 1), not a free parameter.

---

## Theorem

> **Theorem (NP_006).** The phase-locking term κ·sin(θ_B−θ_A) is derivable as a FORM
> from the actualization structure, but does NOT emerge as an evolution term in
> canonical AT. Proof: (1) The only phase-dependent observable is the interference
> intensity I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B) (Born rule, QG216). (2) Its gradient
> w.r.t. θ_A is ∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B−θ_A) — exactly the Kuramoto form, with
> coupling coefficient κ = 2√(ρ_Aρ_B) (the DERIVED interference cross-amplitude).
> (3) However, the canonical phase update θ(t+1) = θ(t) + Δθ (D_041) contains only the
> self-rate — it never follows the gradient, so the term is NOT present in the
> evolution. (4) The smallest modification that produces it is a variational
> requirement — the phase advances along the intensity gradient —
> θ_A(t+1) = θ_A(t) + Δθ_A + 2η√(ρ_Aρ_B)·sin(θ_B−θ_A), which locks the relative phase
> when 2η√(ρ_Aρ_B) ≥ |Δθ_A−Δθ_B|/2 (NP_005 threshold). Therefore: the FORM is
> DERIVABLE (A, partial) and the COEFFICIENT is DERIVED (κ = 2√(ρ_Aρ_B)); the
> MECHANISM (gradient-following phase update) is EMERGENT only under a variational
> principle and is otherwise EXTERNAL (C) to the canonical chain. No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Compute the interference gradient (Section 3, verified: κ =
> 2√(0.25·0.75) = 0.866 for ρ_A=0.25, ρ_B=0.75). (2) Confirm no canonical mechanism
> feeds it back (Section 4). (3) Exhibit the minimal variational modification and its
> locking condition (Section 6, verified: κ=0.866 ≥ 0.5236 locks; weak amplitudes
> κ=0.02 < 0.5236 do not). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Phase (NP_003 lever)
 → Coupling (NP_004: interference + shared event)
 → Interference gradient (∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B−θ_A)) — DERIVED form
 → Locking?
    → canonical: NO (no gradient-following update)
    → variational actualization: YES (κ = 2η√(ρ_Aρ_B))
```

---

## 7. Necessity Proof

The cross-phase term is NECESSARY for unequal-mode synchronization (NP_005: a stable
fixed point of the relative phase requires κ ≥ |Δθ_A−Δθ_B|/2). This audit shows its
natural form IS the interference gradient, and its coefficient IS the Born
cross-amplitude — so no free parameter is needed; synchronization, where it occurs,
is fixed by the amplitudes.

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Shared events synchronize persistently" | joint pinning is one-time; drift resumes (NP_004) |
| "Reciprocity provides the feedback" | reciprocity is the symmetric read (D_037), not an evolution term |
| "Information exchange locks phases" | measurement redistributes information (M_005), not the phase advance |
| "κ is a free external parameter" | κ = 2√(ρ_Aρ_B) is fixed by the Born amplitudes — not free (if variational) |

---

## 9. Falsification Path

1. **Canonical emergence** — falsified if two unequal modes synchronize with NO added
   mechanism and NO gradient-following principle: canonical AT contains no locking term.
2. **Variational emergence** — falsified if a system's phase advance is measured NOT to
   follow the interference gradient (∂I/∂θ_A), i.e., if the coupling coefficient is not
   2√(ρ_Aρ_B).

---

## 10. Observable consequences (if the mechanism were active)

| Consequence | Signature |
|---|---|
| **coherence** | sustained interference fringes (definite relative phase) |
| **resonance amplification** | locked phase → constructive accumulation, 2√(ρ_Aρ_B) maximal |
| **collective modes** | in-phase / anti-phase configurations stable |
| **synchronized measurements** | correlated readouts (joint phase fixed) |

---

## Classification

| Component | Status |
|---|---|
| interference intensity I | **DERIVED** (complex state D_036 + Born QG216) |
| gradient form sin(θ_B−θ_A) | **DERIVED** (algebra of I) |
| coupling coefficient κ = 2√(ρ_Aρ_B) | **DERIVED** (Born cross-amplitude) |
| gradient-following phase update | **EMERGENT** (only under a variational requirement) / **BOUNDARY** in canonical AT |
| canonical locking term | **absent** (no mechanism in the derived chain) |

**The locking term's form and strength are DERIVED from the interference structure;
the locking mechanism requires a variational actualization principle the canonical
chain does not contain. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Variational actualization (NP_006 OP1).** Whether the phase advance should follow
   the count-intensity gradient (a variational/least-action principle) — the single
   requirement that would make phase locking self-generated.

---

## Next Steps

- **Registry note:** κ·sin(θ_B−θ_A) is derivable as the interference gradient with
  κ = 2√(ρ_Aρ_B); synchronization would be self-generated under a variational
  actualization principle (open question).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_006_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_006_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_006_SharedActualization` | shared event pins once; drift resumes | ✅ |
| `Y_NP_006_CountRedistribution` | Born redistribution affects magnitude, not phase advance | ✅ |
| `Y_NP_006_PhaseCoupling` | ∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B−θ_A) — the Kuramoto form | ✅ |
| `Y_NP_006_SynchronizationThreshold` | κ = 2√(ρ_Aρ_B) ≥ \|Δθ_A−Δθ_B\|/2 locks | ✅ |
| `Y_NP_006_DependencyTrace` | chain to the interference-gradient origin | ✅ |
| `Y_NP_006_Run` | research report | ✅ |

**Conclusion:** The locking term's form (sin(θ_B−θ_A)) and coefficient (κ = 2√(ρ_Aρ_B))
are DERIVED from the interference structure — but the locking MECHANISM requires a
variational (gradient-following) phase update that canonical AT does not contain. No
new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_006"`

---

## References

- ResearchY-NP_003 (the phase lever), NP_004 (phase coupling), NP_005 (missing
  synchronization mechanism, threshold κ ≥ |Δθ_A−Δθ_B|/2), M_002 (phase-pinning),
  M_005 (information conservation), D_036 (complex state), D_041 (tick rate).
- AT-QG: QG216 (Born rule).
