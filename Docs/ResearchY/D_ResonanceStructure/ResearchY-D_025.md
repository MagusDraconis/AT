# ResearchY-D_025 — Three-Generator Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_025 (permanent)
**Title:** Three-Generator Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_025.md`
**Depends on:** ResearchY-D_022 (weak-isospin entry), D_023 (SU(2) entry),
D_024 (doublet compatibility)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_025_Tests.cs`

---

## Purpose

**Why three generators?** Determine the minimal structure that upgrades a spectral
doublet from SO(2) (one generator, J) to SU(2) (three generators, σx, σy, σz). D_023
showed SU(2) is not derived from the spectral structure; this audit pinpoints exactly
what extra structure would have to be added.

## Accepted (from D_022, D_023, D_024)

- The spectral doublet {cos, sin} provides the SO(2) generator J (D_022).
- SU(2) needs three generators; the spectral structure has one (D_023).
- The doublet is the SU(2) fundamental carrier, but the attachment is EMERGENT
  (D_024).

---

## 1. The generator map

### 1.1 The spectral algebra is the full real 2×2 algebra

The {cos, sin} eigenspace provides the real 2×2 operators {I, J, P, JP} where:

| Operator | Matrix | Role |
|---|---|---|
| I | [[1,0],[0,1]] | identity |
| **J** | [[0,−1],[1,0]] | the SO(2) rotation generator = **iσy** (real skew) |
| **P** | [[1,0],[0,−1]] | the reflection/parity = **σz** (real symmetric) |
| **JP** | [[0,1],[1,0]] | = **σx** (real symmetric) |

The span {I, J, P, JP} is the **full real 2×2 matrix algebra** (dimension 4). The
spectral structure therefore contains the **Hermitian** σx = JP and σz = P, plus the
skew J = iσy.

### 1.2 SU(2) needs skew-Hermitian generators

The SU(2) algebra is spanned by the **skew-Hermitian** generators:

| Generator | Matrix | In the real spectral algebra? |
|---|---|---|
| **iσy** | [[0,−1],[1,0]] = J | **YES** (real skew) |
| iσx | [[0,i],[i,0]] | **NO** — complex (imaginary off-diagonal) |
| iσz | [[i,0],[0,−i]] | **NO** — complex (imaginary diagonal) |

The spectral algebra provides **iσy only**. The Hermitian σx = JP and σz = P are
present, but SU(2) needs the skew-Hermitian iσx, iσz — which require the **imaginary
unit i**.

---

## 2. What adds σx and σz to J = σy?

| Ingredient | Adds | Verdict |
|---|---|---|
| parity P | σz (Hermitian) | already in the spectral algebra — but NOT iσz |
| reflection (JP) | σx (Hermitian) | already in the spectral algebra — but NOT iσx |
| **complexification (i)** | **iσx, iσz** (skew-Hermitian) | **the unique missing ingredient** |
| phase structure (θ_k) | e^{iθ} (U(1)) | provides i, but is Abelian (1-dim), not SU(2) |
| spectral symmetry | D_n 2D irreps (O(2)-type) | no complex structure |

The **minimal extra ingredient** is **complexification**: the imaginary unit i converts
the Hermitian σx, σz into the skew-Hermitian iσx, iσz needed for SU(2).

---

## 3. Is the complexification canonical?

### 3.1 The Fourier phase provides i (EMERGENT)

The ring's Fourier modes are e^{i·2πkn/N} = cos + i·sin — the complex exponential is the
native object, with {cos, sin} as its real and imaginary parts (D_001/D_002). The phase
lattice closes (θ_{k+N} ≡ θ_k, z^N = 1, B_003). So the **imaginary unit is implicit in
the Fourier representation** — complexification is EMERGENT from the phase structure.

### 3.2 But complexification alone gives sl(2,C), not SU(2)

Complexifying the 2D space gives C²; the algebra becomes **sl(2,C)** (6 real
dimensions). SU(2) is the **compact real form** of sl(2,C). But sl(2,C) has THREE real
forms:

| Real form | Signature | Generators |
|---|---|---|
| **su(2)** | compact (negative-definite) | iσx, iσy, iσz (skew-Hermitian) |
| **sl(2,R)** | split | real traceless (IN the spectral algebra!) |
| su(1,1) | (1,1) | skew-Hermitian with mixed signature |

**The real spectral structure {I, J, P, JP} contains the sl(2,R) generators directly**
(they are real traceless matrices). If anything, the real spectral structure leans toward
**sl(2,R), NOT su(2)**. The choice of the compact form (su(2)) is NOT determined by the
spectrum.

---

## 4. Test: remove each ingredient

| Removed | Result | SU(2) survives? |
|---|---|---|
| complexification (i) | real {I,J,P,JP}, only J skew → SO(2)/O(2) | **NO** |
| parity (P) | only J → SO(2) | **NO** |
| phase structure (θ_k) | real modes only → SO(2) | **NO** |
| compact-form choice (su(2) signature) | complex sl(2,C) — no gauge group | **NO** |

**SO(2) → SU(2) is NOT possible without new input.** It requires BOTH:
1. **complexification** (the Fourier i — EMERGENT from the phase), and
2. **the compact-form choice** (su(2) not sl(2,R), not su(1,1) — BOUNDARY).

---

## Determination

| Option | Verdict |
|---|---|
| SO(2) → SU(2) without new input | **NO** — needs complexification (EMERGENT) + compact-form choice (BOUNDARY) |
| complexification derived | **PARTIAL/EMERGENT** — the Fourier i is implicit in the phase, but it is a representation choice |
| compact-form choice derived | **NO — BOUNDARY** — the spectrum leans sl(2,R), not su(2) |
| the three generators emerge | **NO** — only iσy is in the real spectrum; iσx, iσz need complexification + signature choice |

---

## Theorem

> **Theorem (D_025).** SO(2) → SU(2) is not possible without new input. The spectral
> algebra {I, J, P, JP} is the full real 2×2 algebra: it contains J = iσy (real skew),
> σz = P (parity), and σx = JP (Hermitian). SU(2) needs the skew-Hermitian generators
> iσx, iσz, which require the imaginary unit i (complexification). The Fourier phase
> provides i (EMERGENT — the complex exponential is native to the ring), but
> complexification alone gives sl(2,C), which has THREE real forms; the real spectral
> structure contains the sl(2,R) generators directly and leans toward sl(2,R), NOT
> su(2). The compact-form choice (su(2) signature) is therefore BOUNDARY. Hence the
> upgrade needs complexification (EMERGENT) + the compact-form choice (BOUNDARY); the
> three generators do not emerge from the spectral structure alone.
>
> *Proof sketch.* (1) The {cos, sin} algebra is the full real 2×2 span {I, J, P, JP},
> containing σx = JP and σz = P as Hermitian matrices (Section 1.1). (2) SU(2) needs the
> skew-Hermitian iσx, iσz; only iσy = J is in the real spectrum (Section 1.2). (3) The
> imaginary unit i is provided by the Fourier phase (complexification, EMERGENT) — but
> sl(2,C) has three real forms, and the spectrum contains the sl(2,R) generators
> directly (Section 3). (4) The compact-form choice (su(2) vs sl(2,R) vs su(1,1)) is
> not determined by the spectrum — BOUNDARY (Section 3.2). (5) Removing any ingredient
> breaks SU(2) (Section 4). Hence SO(2) → SU(2) requires complexification (EMERGENT) +
> compact-form choice (BOUNDARY); it is not possible without new input. ∎

---

## Dependency Graph

```
oscillation
 → spectral Z2 (λ_k = λ_{N−k})        [DERIVED]
 → quadrature doublets {cos, sin}     [DERIVED]
 → real algebra {I, J, P, JP}         [DERIVED — full real 2×2, σx=JP, σz=P, iσy=J]
 → complexification (Fourier i)       [EMERGENT — from the phase, a representation choice]
 → sl(2,C) (6 real dims)              [EMERGENT — complexification]
 → compact-form choice (su(2))        [BOUNDARY — not su(2)'s sl(2,R)/su(1,1)]
 → SU(2) (3 generators)               [BOUNDARY — gauge input]
```

---

## Generator Map

```
J = iσy        [DERIVED — the spectral SO(2) generator, real skew]
σz = P         [DERIVED — parity/reflection, Hermitian]
σx = JP        [DERIVED — reflection∘rotation, Hermitian]
─── (complexification: × i) ───
iσx, iσz       [EMERGENT — need the imaginary unit i from the Fourier phase]
─── (compact-form choice) ───
su(2) = {iσx, iσy, iσz}   [BOUNDARY — not sl(2,R), not su(1,1)]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is σx in the spectral algebra? | YES (σx = JP, Hermitian) — but not iσx |
| Is σz in the spectral algebra? | YES (σz = P, parity) — but not iσz |
| Is iσy in the spectral algebra? | YES (J, real skew) |
| What is missing? | the imaginary unit i (complexification) |
| Does complexification give SU(2)? | NO — it gives sl(2,C); the compact form su(2) is a further choice |
| Does the spectrum prefer su(2)? | **NO — it leans sl(2,R)** (real generators are in the algebra) |
| SO(2) → SU(2) without new input? | **NO** |

---

## Counterexamples

1. **The Hermitian σx, σz are present but insufficient**: σx = JP and σz = P are real
   symmetric; SU(2) needs the skew-Hermitian iσx, iσz (complex). The presence of σx, σz
   does not yield SU(2).
2. **The real spectral algebra IS sl(2,R)**, not su(2): the real traceless 2×2 matrices
   are in {I, J, P, JP} — the spectrum's natural real form is sl(2,R), a counterexample
   to "the spectrum selects su(2)".
3. **U(1) phase ≠ SU(2)**: the ring phase e^{iθ} is Abelian (1-dimensional); it provides
   i but not the three-generator non-Abelian algebra.
4. **Complexification alone → sl(2,C)**: 6 real dimensions, not a gauge group; the
   compact-form choice is required.

---

## Classification

| Component | Status |
|---|---|
| real algebra {I, J, P, JP} (σx, σz, iσy present) | **DERIVED** (oscillation + reflection) |
| complexification (the Fourier i) | **EMERGENT** (from the phase, a representation choice) |
| sl(2,C) | **EMERGENT** (complexification) |
| compact-form choice (su(2) signature) | **BOUNDARY** (spectrum leans sl(2,R)) |
| SU(2) three generators | **BOUNDARY** (gauge input) |
| SO(2) → SU(2) without new input | **REFUTED** |

**The upgrade from SO(2) to SU(2) requires complexification (EMERGENT) + the
compact-form choice (BOUNDARY); the three generators do not emerge from the spectral
structure alone.**

---

## Open Problems

1. **Phase-algebra role (D_025 OP1).** The Fourier phase provides the imaginary unit i
   (EMERGENT), but its role in selecting a real form is unclear — the phase is Abelian
   (U(1)), not the SU(2) algebra.
2. **Why su(2) not sl(2,R) (D_025 OP2).** The real spectral structure contains the
   sl(2,R) generators; why the physical gauge group is the compact su(2) (not the split
   sl(2,R)) is the boundary question (canonical: SU(2) spin sector POSTULATED,
   ATQG670/680).

---

## Next Steps

- **ResearchY-D_026 (or synthesis):** the three-generator audit completes the gauge
  chain (oscillation → doublets → complexification → compact form → SU(2)). A synthesis
  can map the full boundary structure of the gauge sector.
- **D_023 follow-up:** the sl(2,R)-vs-su(2) distinction sharpens D_023 — the spectral
  structure not only fails to provide SU(2), it actively leans toward sl(2,R).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_025_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_025_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_025_GeneratorMap` | J=iσy, σz=P, σx=JP all in the real spectral algebra | ✅ |
| `Y_D_025_SkewHermitian` | SU(2) needs iσx, iσz (complex); only iσy is real | ✅ |
| `Y_D_025_Complexification` | the Fourier i is the missing ingredient (EMERGENT) | ✅ |
| `Y_D_025_RealForms` | sl(2,C) has 3 real forms; spectrum leans sl(2,R), not su(2) | ✅ |
| `Y_D_025_RemovalTest` | removing any ingredient breaks SU(2) | ✅ |
| `Y_D_025_Verdict` | SO(2)→SU(2) not possible without complexification + compact-form | ✅ |
| `Y_D_025_Run` | Research report | ✅ |

**Conclusion:** The upgrade from SO(2) to SU(2) is not possible without new input. The
real spectral algebra {I, J, P, JP} is the full real 2×2 algebra — it contains σx = JP
and σz = P (Hermitian) and iσy = J (real skew), but SU(2) needs the skew-Hermitian
iσx, iσz, which require the imaginary unit i (complexification). The Fourier phase
provides i (EMERGENT), but complexification alone gives sl(2,C), whose three real forms
include sl(2,R) — which the real spectral structure contains directly and leans toward,
NOT su(2). The compact-form choice (su(2) signature) is BOUNDARY. Hence SO(2) → SU(2)
requires complexification (EMERGENT) + compact-form choice (BOUNDARY). No canonical value
was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_025"`

---

## References

- ResearchY-D_022 (weak-isospin entry), D_023 (SU(2) entry), D_024 (doublet
  compatibility).
- AT-QG: QG153 (doublet origin), QG670/680 (SU(2) spin sector — POSTULATED input).
- Monograph V2.0: Ch6 (D96 spectrum).
