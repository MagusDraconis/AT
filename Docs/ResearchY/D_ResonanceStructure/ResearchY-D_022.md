# ResearchY-D_022 — Weak-Isospin Entry Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_022 (permanent)
**Title:** Weak-Isospin Entry Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_022.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_022_Tests.cs`

---

## Purpose

**Where does weak-isospin enter?** D_021 established that the Z2 pairing (doublet
structure) is oscillation-derived and the weak-isospin *reading* is EMERGENT. This audit
separates the three Z2 objects — oscillation, spectral, weak-isospin — and determines
whether weak-isospin is (A) the oscillation itself, (B) a spectral reading, or (C) an
independent input.

## Accepted (from D_020, D_021)

- The Z2 pairing is the two-quadrature structure of one real oscillation — DERIVED
  (D_021).
- Complete pairing (0 unpaired) is a BOUNDARY N-arithmetic selection (D_020/D_021).
- The two-anchor boson/fermion split is EMERGENT (D_014).

---

## 1. The three Z2 objects

| Object | Definition | Source | Status |
|---|---|---|---|
| **oscillation Z2** | +A ↔ −A, cos(ωt) ↔ −cos(ωt) — temporal phase inversion of a single mode | any real oscillator | **DERIVED** (universal phase gauge) |
| **spectral Z2** | λ_k = λ_{N−k} — the ring-reflection mirror pairing | ring's reflection automorphism | **DERIVED** (spectral) |
| **weak-isospin Z2** | SU(2) doublet rep, T₃ = ±1/2 (ν/e, u/d) | gauge sector S | **BOUNDARY** (gauge input) |

The three are **different objects**: the oscillation Z2 is the ± phase of one mode, the
spectral Z2 is the mirror pairing of two modes, the weak-isospin Z2 is an SU(2)
representation.

---

## 2. Tests

### 2.1 Can oscillation-derived Z2 exist WITHOUT weak-isospin?

**YES.** The spectral Z2 (λ_k = λ_{N−k}) and the oscillation quadrature pairs exist at
every ring size (verified N=32, 64, 96, 128, 192) with no gauge sector present. The
ring's graph Laplacian spectrum carries the full doublet structure independent of any
SU(2) gauge input.

### 2.2 Can weak-isospin exist WITHOUT spectral Z2?

**YES (formally).** SU(2) is a gauge group; a doublet rep (T₃ = ±1/2) is an SU(2)
representation that can be written on any space — spectral degeneracy is not required to
write an SU(2) doublet. The weak-isospin doublet (ν, e) is an SU(2) fundamental rep, not
a spectral degeneracy.

### 2.3 The doublet structures are different

| Doublet | Content | Group |
|---|---|---|
| spectral doublet | {cos, sin} at one ω_k — parity {even, odd} | Z₂ / reflection |
| weak-isospin doublet | {ν, e} — T₃ = ±1/2 | SU(2) |

The spectral pair {cos, sin} is a **parity doublet**: under reflection, cos → cos (+1)
and sin → −sin (−1). The weak-isospin doublet is an **SU(2) fundamental rep**. These are
different mathematical objects.

### 2.4 The {cos, sin} eigenspace is NOT an SU(2) rep

The ring rotation acts on {cos, sin} as a 2×2 real rotation matrix — an **SO(2)**
rotation with determinant 1 (verified: rotation by s=1 at k=1 gives a det-1 orthogonal
matrix). SO(2) is Abelian (one generator); SU(2) is non-Abelian (three Pauli
generators). A 2D Abelian rotation rep cannot carry SU(2) structure. The spectral
doublet is therefore NOT an SU(2) representation by itself.

---

## 3. Determination

| Option | Verdict |
|---|---|
| A) weak-isospin = oscillation | **NO** — oscillation gives the Z2 phase gauge + spectral mirror, not the SU(2) gauge algebra |
| B) weak-isospin = spectral reading | **PARTIAL** — the *doublet shape* (2 modes sharing ω) is the spectral reading; the SU(2) *gauge structure* is not spectral |
| C) weak-isospin = independent input | **YES for the SU(2) gauge structure** — SU(2) is an independent gauge input (sector S, BOUNDARY); the doublet *reading* of spectral pairs is EMERGENT |

**Verdict: C) weak-isospin is an independent input (SU(2) gauge structure), and the
weak-isospin *reading* of the oscillation-derived doublets is EMERGENT.** The
oscillation-derived Z2 provides the doublet *shape*; weak-isospin as a gauge structure
is not derivable from it.

---

## Theorem

> **Theorem (D_022).** Weak-isospin is not the oscillation-derived Z2. The oscillation
> Z2 (phase inversion) and the spectral Z2 (λ_k = λ_{N−k}) are DERIVED — they exist for
> every ring size with no gauge sector — but the weak-isospin Z2 is the SU(2) gauge
> structure, an independent input (BOUNDARY). The {cos, sin} spectral doublet is a 2D
> real SO(2) rotation rep (parity {even, odd}), NOT an SU(2) rep; the weak-isospin
> doublet (T₃ = ±1/2) is an SU(2) fundamental rep. Hence the doublet SHAPE is the
> EMERGENT reading of the spectral pairs, and the SU(2) gauge content is an independent
> input.
>
> *Proof sketch.* (1) The oscillation Z2 and spectral Z2 exist at N=32…192 with no gauge
> sector (Section 2.1) — DERIVED, independent of weak-isospin. (2) SU(2) is a gauge
> group that can be written without spectral degeneracy (Section 2.2) — independent of
> the spectral Z2. (3) The {cos, sin} eigenspace transforms as SO(2) (det-1 rotations),
> not SU(2) — the spectral doublet is a parity doublet, not an SU(2) rep (Sections
> 2.3–2.4). (4) Hence weak-isospin is C) an independent input for its gauge structure,
> with the doublet reading B) EMERGENT. ∎

---

## Dependency Graph

```
oscillation (ψ = A cos(ωt + δ))
 → oscillation Z2 (phase inversion)        [DERIVED — temporal gauge, universal]
 → spectral Z2 (λ_k = λ_{N−k})             [DERIVED — ring reflection]
 → quadrature doublets {cos, sin}          [DERIVED — 2D SO(2) parity doublet]
     → doublet SHAPE read as weak-isospin  [EMERGENT — the reading]
 → weak-isospin SU(2) gauge structure      [BOUNDARY — independent input, sector S]
 → observable sector (families, doublets)  [EMERGENT + BOUNDARY]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| oscillation-derived Z2 without weak-isospin? | **YES** — the spectral doublets exist at all N (32…192) with no gauge sector |
| weak-isospin without spectral Z2? | **YES** (formally) — SU(2) is an independent gauge group; in AT the doublet *reading* requires the spectral Z2, but the gauge structure does not |
| spectral doublet = SU(2) rep? | **NO** — it is an SO(2)/parity doublet |
| weak-isospin derived from oscillation? | **NO** — only the doublet shape is derived; the SU(2) gauge algebra is not |

---

## Counterexamples

1. **N=32** has full spectral Z2 and oscillation quadrature pairs with no SU(2) gauge
   sector — oscillation-derived Z2 without weak-isospin.
2. **An SU(2) doublet on a non-degenerate spectrum** — formally writable (a gauge rep
   does not require spectral degeneracy) — weak-isospin without spectral Z2.
3. **The spectral pair {cos, sin}** transforms under SO(2) rotations (det 1), not under
   SU(2) — a parity doublet that is not a weak-isospin doublet.

---

## Classification

| Component | Status |
|---|---|
| oscillation Z2 (phase inversion) | **DERIVED** (universal temporal gauge) |
| spectral Z2 (λ_k = λ_{N−k}) | **DERIVED** (ring reflection) |
| quadrature doublet {cos, sin} | **DERIVED** (SO(2) parity doublet) |
| doublet → weak-isospin reading | **EMERGENT** (the correspondence) |
| weak-isospin SU(2) gauge structure | **BOUNDARY** (independent input) |

**Weak-isospin is C) an independent input (SU(2) gauge), with the doublet reading B)
EMERGENT — it is NOT the oscillation-derived Z2.**

---

## Open Problems

1. **SU(2) origin (D_022 OP1).** Where does the SU(2) gauge structure itself come from?
   (Canonical: the link's S sector is a POSTULATED/REAL-UNDERIVED input, ATQG670/680.)
2. **Reading uniqueness (D_022 OP2).** Is the spectral-doublet → weak-isospin-doublet
   reading unique, or could the 2D eigenspaces read as other doublets (e.g. parity,
   color)? (Currently: the SU(2) reading is the supported one, D_014.)
3. **Gauge-spectrum link (D_022 OP3).** The doublet SHAPE is spectral but the SU(2)
   ALGEBRA is not; whether the gauge structure can attach to the spectral doublets
   canonically (rather than as an independent sector) is open.

---

## Next Steps

- **ResearchY-D_023 (or synthesis):** the weak-isospin-entry audit separates the
  DERIVED spectral doublets from the BOUNDARY SU(2) gauge input. A synthesis can map
  the full doublet chain: oscillation → spectral Z2 → doublets → weak-isospin reading.
- **D_021 follow-up:** the SO(2)-vs-SU(2) rep distinction sharpens the D_021
  oscillation-necessity claim (the pairing is oscillation-derived; the gauge algebra is
  not).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_022_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_022_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_022_Z2Separation` | oscillation/spectral/weak-isospin Z2 are three distinct objects | ✅ |
| `Y_D_022_NoWeakIsospin` | oscillation-derived Z2 exists at N=32..192 without gauge sector | ✅ |
| `Y_D_022_NoSpectralZ2` | SU(2) doublet writable without spectral degeneracy | ✅ |
| `Y_D_022_NotSU2Rep` | {cos, sin} transforms as SO(2) (det-1), not SU(2) | ✅ |
| `Y_D_022_ParityDoublet` | spectral pair is a parity doublet {even, odd} | ✅ |
| `Y_D_022_Verdict` | weak-isospin = independent input (C); doublet reading EMERGENT (B) | ✅ |
| `Y_D_022_Run` | Research report | ✅ |

**Conclusion:** Weak-isospin is NOT the oscillation-derived Z2. The oscillation Z2 and
spectral Z2 (λ_k = λ_{N−k}) are DERIVED (exist at all ring sizes); the weak-isospin Z2
is the SU(2) gauge structure — an independent input (BOUNDARY). The {cos, sin} spectral
doublet is a 2D SO(2)/parity doublet, not an SU(2) rep; only the doublet SHAPE is the
EMERGENT reading of the spectral pairs. Classification: weak-isospin C) independent
input; doublet reading B) EMERGENT. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_022"`

---

## References

- ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry), D_014
  (two-anchor structure — boson/fermion split EMERGENT).
- AT-QG: QG153 (doublet origin), QG155 (Z2 symmetry origin — dihedral D_n 2D irreps),
  QG670/680 (SU(2) spin sector — POSTULATED/REAL-UNDERIVED input).
- Monograph V2.0: Ch6 (D96 spectrum).
