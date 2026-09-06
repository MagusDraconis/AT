# ResearchY-NP_062 — High-Order Universe Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_062 (permanent)
**Title:** High-Order Universe Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_062.md`
**Depends on:** ResearchY-NP_055–NP_061 (the dark-energy arc), AT-QG QG234 (ΩΛ = I_occ/ln K),
QG228 (I_occ = KL(ρ‖uniform)), QG227 (initial uniform state), QG230 (uniform-state
instability), D_041 (spectrum λ_k = 2−2cos(2πk/N)), A_003/D_030 (occupancy [4,4,87]),
NP_032/NP_035 (1D linear dispersion, top-heavy DOS), NP_037 (occupancy rarity carries no
selection info)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_062_Tests.cs`

---

## Purpose

ΩΛ = I_occ/ln K = 0.6839 means the realized D96 occupancy sits **far from the uniform
state**. NP_062 asks why: **why is reality highly ordered instead of near-uniform?**
Program: (1) compare uniform / random / canonical [4,4,87]; (2) measure entropy, I_occ,
ΩΛ; (3) search whether high order is typical / selected / required; (4) determine whether
[4,4,87] is an attractor, fixed point, boundary, or selection outcome; (5) locate the
earliest source of the large information surplus. **Success criterion:** explain why reality
is highly ordered. No new primitives; canonical AT unchanged.

---

## 1. Uniform / random / canonical — the order measure

| Occupancy (over 3 octaves) | H (nats) | I_occ (nats) | ΩΛ |
|---|---|---|---|
| **uniform** [95, 95, 95] | 1.0986 (= ln 3, max) | 0.0000 | **0.0000** |
| **random** [32, 32, 31] | 1.0985 | 0.0001 | 0.0001 |
| width-proportional [13, 27, 55] | 0.9461 | 0.1525 | 0.1388 |
| **canonical** [4, 4, 87] | 0.3473 | **0.7513** | **0.6839** |

The canonical occupancy realizes only H = 0.3473 nats of the available ln 3 = 1.0986 nats —
**68% of the state-space's information capacity is left unrealized as entropy**, and shows up
as the information surplus I_occ = 0.7513 (ΩΛ = 0.6839). A random/near-uniform occupancy
would sit at ΩΛ ≈ 0.

---

## 2. Why is the canonical occupancy top-heavy? — the spectral origin

ρ = [4,4,87]/95 is NOT a free choice. It is the **normalized octave occupancy of the D96
spectrum** λ_k = 2−2cos(2πk/N), and that spectrum is structurally top-heavy:

| Ring size N | span | octave occupancy | top-octave share |
|---|---|---|---|
| 48 | 3.24 | [4, 43] | 0.915 |
| 96 | 6.40 | **[4, 4, 87]** | **0.916** |
| 120 | 8.00 | [4, 4, 111] | 0.933 |
| 192 | 12.78 | [4, 4, 8, 175] | 0.916 |

**The top-heaviness is universal** across the circulant family C_N(±1..±6): every ring puts
~92% of its modes in the highest octave. The reason is the 1D chain structure (NP_035): the
low-frequency dispersion is approximately **linear**, ω_k ≈ c·k (verified: ω_k/k =
0.6216, 0.6133, 0.5997, 0.5812 for k = 1..4), so the density of states is bottom-sparse and
top-dense, and the modes crowd toward the finite UV cap ω_max = 3.98.

**The high order is a spectral fact, not a dynamical outcome.**

---

## 3. Is high order typical, selected, or required?

| Question | Verdict |
|---|---|
| **typical?** | **YES.** Top-heaviness is the NORM of the circulant family — ~92% top-octave share at every N (Section 2). Far-from-uniform is typical; the *uniform* state is the exceptional (idealized) case. |
| **selected?** | **NO dynamical selection.** The map N → occupancy is a bijection (NP_037): every occupancy is one-of-a-kind, and its rarity carries no selection information. No fitness/relaxation chooses [4,4,87]. |
| **required?** | **YES, in structure.** The top-heaviness is REQUIRED by the 1D ring's linear dispersion + finite UV cap (NP_035). The specific [4,4,87] is NOT required (it is N-specific); the top-heaviness is. |

**High order is a structural requirement (typical of the class), not a selection and not an
accident.**

---

## 4. What is [4,4,87] — attractor, fixed point, boundary, or selection outcome?

| Reading | Verdict |
|---|---|
| **attractor** | **NO.** There is no dynamics that relaxes to [4,4,87]; the occupancy is a static spectral count. |
| **fixed point** | **NO.** There is no iteration; nothing converges to it. |
| **boundary** | **PARTIAL.** The top-heaviness (I_occ ≫ 0) is DERIVED from the spectrum; but the exact N = 96 (which fixes [4,4,87]) rests on the BOUNDARY 3-family window [4,8). |
| **selection outcome** | **YES, weakly.** [4,4,87] is the occupancy of the canonical N = 96 (selected as the octave/family base), not a dynamically preferred state. |

**Determination:** [4,4,87] is a **selection outcome** (the occupancy of the canonical ring)
whose *top-heaviness* is DERIVED (spectral structure) and whose *exact value* rests on the
BOUNDARY N = 96 window.

---

## 5. The earliest source of the large information surplus

The chain and where the surplus enters:

```
Difference → Actualization (discrete tick)                [BOUNDARY]
   → spectrum λ_k = 2 − 2cos(2πk/N),  N=96               [DERIVED — D_041]
       · circulant ring C_96(±1..±6): 1D chain            [DERIVED — NP_035]
       · linear dispersion ω_k ≈ c·k                       [DERIVED]
       · finite UV cap ω_max ≈ 3.98                        [DERIVED]
   → top-heavy occupancy [4, 4, 87]                        [DERIVED — A_003/D_030]
   → I_occ = KL(ρ‖uniform) = 0.7513 nats  ≫ 0             [DERIVED — QG228]
   → ΩΛ = I_occ/ln K = 0.6839                              [DERIVED — QG234]
```

**The earliest source of the large information surplus is the discrete circulant spectrum —
its 1D linear dispersion and finite UV cap.** The uniform state (ρ = 1/K) is the
maximum-entropy *reference* (QG227's "initial uniform critical state"), but it is unattainable
by a discrete process (QG228/QG230): a specific discrete spectrum is intrinsically
non-uniform, and the D96 ring's is top-heavy. **The "high order" is the mandatory shape of a
discrete 1D spectrum, not a deviation that needs explaining away.**

---

## Theorem

> **Theorem (NP_062).** Reality is highly ordered because the count density ρ is the
> normalized occupancy of a discrete 1D circulant spectrum (λ_k = 2−2cos(2πk/N), N=96),
> which is structurally top-heavy; the uniform state is a maximum-entropy reference, not a
> realized baseline. Proof: (1) Order measure (Section 1, verified): uniform/random
> occupancies have ΩΛ ≈ 0, the canonical [4,4,87] has ΩΛ = 0.6839 (H = 0.3473 vs ln 3 =
> 1.0986; I_occ = 0.7513 = 68% of capacity unrealized). (2) Spectral origin (Section 2,
> verified): top-heaviness is universal across C_N(±1..±6) — ~92% top-octave share at
> N = 48/96/120/192 — from the 1D linear dispersion ω_k ≈ c·k and finite UV cap. (3)
> Typical/selected/required (Section 3): top-heaviness is typical (universal) and required
> (structural), not dynamically selected (N→occupancy bijection, NP_037). (4) Classification
> (Section 4): [4,4,87] is a selection outcome (occupancy of canonical N=96), not an
> attractor/fixed point; its top-heaviness DERIVED, its exact value BOUNDARY (N=96 window).
> (5) Earliest source (Section 5): the discrete circulant spectrum — linear dispersion + UV
> cap. Classification: the top-heavy occupancy and I_occ DERIVED (spectral structure); the
> high order (ΩΛ far from uniform) DERIVED; the uniform state as max-entropy reference
> BOUNDARY (QG227 reference); the exact N = 96 (hence exact [4,4,87]) BOUNDARY (3-family
> window); dynamical attractor / selection REFUTED. **Success criterion: reality is highly
> ordered because its count density is the occupancy of a discrete 1D spectrum that is
> top-heavy by construction — high order is the mandatory, typical structure of the canonical
> ring, not a near-uniform state that drifted away.** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Contrast uniform/random/canonical. (2) Show the spectral top-heaviness.
> (3) Rule out dynamical selection; establish structural requirement. (4) Classify [4,4,87].
> (5) Locate the surplus at the discrete spectrum. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "high order is a dynamical attractor" | no dynamics relaxes to [4,4,87]; it is a static spectral count |
| "[4,4,87] is a selection (fitness)" | the N→occupancy map is a bijection; rarity carries no selection information (NP_037) |
| "near-uniform is the natural baseline" | the uniform state is a max-entropy reference, not a realized state (QG227/228) |
| "the top-heaviness is specific to N=96" | it is universal (~92% top octave at every N, NP_032/035) |
| "the high order needs a mechanism" | it is the mandatory shape of a discrete 1D spectrum (linear dispersion + UV cap) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| the top-heaviness is structural (DERIVED) | a circulant ring C_N(±1..±6) with a non-top-heavy occupancy |
| the uniform state is a reference, not a baseline | a discrete process that realizes the exact uniform ρ = 1/K |
| [4,4,87] is not a dynamical attractor | a dynamics that converges to the [4,4,87] occupancy |
| the surplus originates at the spectrum | an occupancy whose I_occ ≫ 0 does not trace to the spectral dispersion |

---

## 8. Classification

| Component | Status |
|---|---|
| top-heavy occupancy [4,4,87] | **DERIVED** (spectral binning, A_003/D_030) |
| the large information surplus I_occ = 0.7513 | **DERIVED** (KL over the spectral occupancy) |
| ΩΛ = 0.6839 (far from uniform) | **DERIVED** (QG234) |
| the high order being "typical / required" | **DERIVED** (1D linear dispersion, universal top-heaviness) |
| the uniform state as max-entropy reference | **BOUNDARY** (QG227 reference) |
| exact N = 96 (hence exact [4,4,87]) | **BOUNDARY** (3-family window [4,8)) |
| dynamical attractor / selection mechanism | **REFUTED** |

**Conclusion.** Reality is highly ordered because its count density is the occupancy of a
**discrete 1D circulant spectrum** that is top-heavy by construction: the ring's approximately
linear dispersion (ω_k ≈ c·k) and finite UV cap crowd ~92% of modes into the top octave, so
the realized state realizes only 32% of its entropy capacity (I_occ = 0.7513, ΩΛ = 0.6839).
High order is the **typical and required** structure of the canonical ring — not a dynamical
attractor, not a selection, and not a deviation from a near-uniform baseline (the uniform
state is only a maximum-entropy *reference*). The earliest source of the large information
surplus is the **discrete spectrum itself**. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_062_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_062_OrderMeasure` | uniform/random ΩΛ≈0; canonical ΩΛ=0.6839 | ✅ |
| `Y_NP_062_SpectralOrigin` | top-heavy occupancy universal across N | ✅ |
| `Y_NP_062_LinearDispersion` | ω_k ≈ c·k (1D chain) | ✅ |
| `Y_NP_062_TypicalSelectedRequired` | typical + required, not selected | ✅ |
| `Y_NP_062_Classification` | [4,4,87] selection outcome, not attractor | ✅ |
| `Y_NP_062_EarliestSource` | surplus originates at the discrete spectrum | ✅ |
| `Y_NP_062_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_062"`

---

## References

- ResearchY-NP_055–NP_061 (dark-energy arc), NP_032 (top-heavy at every N), NP_035 (1D linear
  dispersion, DOS origin), NP_037 (occupancy rarity carries no selection info).
- AT-QG: QG234 (ΩΛ = I_occ/ln K), QG228 (I_occ = KL(ρ‖uniform)), QG227 (initial uniform
  state), QG230 (uniform-state instability), D_041 (spectrum λ_k = 2−2cos(2πk/N)),
  A_003/D_030 (occupancy [4,4,87]).
