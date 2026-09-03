# ResearchY-NP_035 — Density-of-States Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_035 (permanent)
**Title:** Density-of-States Origin Audit
**Status:** COMPLETE
**Date:** 2026-09-03
**File:** `NP_NewPhysics/ResearchY-NP_035.md`
**Depends on:** ResearchY-NP_028 (blackbody FALSIFIED, DOS ~ω^1.5 mid-band),
NP_032 (no thermal N; 1D linear dispersion of every C_N(±1..±K) ring), NP_034
(Bose sufficient; mode set is the minimal obstruction; hosted ω² DOS), D_008/D_030
(D96 spectrum, occupancy [4,4,87]), D_021 (pairing), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_035_Tests.cs`

---

## Purpose

NP_034 isolated the blackbody failure to the D96 mode set: with the Bose occupation
(already exact, EMERGENT) over an ideal ω² DOS the blackbody is reproduced, but over
the D96 mode set it is not. NP_035 asks the structural question that raises: **why
does the D96 mode set produce g_D96(ω) (a 1D-chain DOS, N(ω) ∝ ω, p = 1) instead of
the observed g_BB(ω) ∝ ω² (N(ω) ∝ ω³, p = 3)?** The audit derives the analytic DOS of
the D96 ring, compares the DOS exponent p against 1D/2D/3D cavities and the circulant
family C_N(±1..±K), tests the candidate structural origins (dimensionality, topology,
finite count, circulant structure, hosted higher-layer geometry), tests the natural
extensions (larger N, larger K, coupled rings, tensor products), and searches the
minimal construction producing N(ω) ∝ ω³ while preserving the D96 local rule.
No new primitives; canonical AT unchanged.

---

## 1. Analytic DOS of the canonical D96 ring

The D96 spectrum is the circulant ring C_96(±1..±6): λ_k = Σ_s 2(1 − cos(2πks/N)),
ω_k = √λ_k, k = 1..95. For small k (low frequency) the cosine expands:

```
λ_k = Σ_s 2(1 − cos(2πks/N)) ≈ Σ_s (2πks/N)² = (2πk/N)² · Σ_s s² = (2πk/N)² · 91
ω_k ≈ (2π√91/N) · k  ≡  c·k,   c = 2π√91/N
```

**The D96 low-frequency dispersion is exactly linear in k** (verified:
ω_k/(c·k) → 1 as N grows; at N = 6144, k = 1..4 the ratio is 1.000 to 1e-3). A linear
dispersion ω_k = c·k means the modes are EQUALLY SPACED in frequency: the number of
modes below ω is N(ω) ≈ ω/c — **N(ω) ∝ ω, DOS exponent p = 1**.

This is the signature of a **1D chain**: the D96 spectrum is indexed by a SINGLE
integer k ∈ [1, N−1]. One integer quantum number gives the 1D density of states
g(ω) = dN/dω = const (the 1D cavity DOS). The D96 DOS is NOT a ω² (3D) DOS because
the mode set has only ONE index direction.

**Verified octave structure:** the low octaves of D96 hold 4 modes each
([ω₁, 2ω₁): 4, [2ω₁, 4ω₁): 4) — exactly the constant-density-per-unit-ω of a linear
1D chain — while the third octave [4ω₁, 8ω₁) holds 87 modes because the band ends at
ω_max = 3.98 (test `Y_NP_035_D96TopHeavinessIsFiniteBandEffect`). The NP_028 mid-band
"exponent ~1.5" is a finite-band artifact of the top-heavy clustering near the hard
cap, NOT an asymptotic DOS exponent.

---

## 2. Comparison against cavities and lattices

| System | Mode indices | Low-ω dispersion | N(ω) | exponent p | g(ω) |
|---|---|---|---|---|---|
| **D96 ring C_96(±1..±6)** | 1 (k = 1..95) | ω ≈ c·k | ∝ ω | **1** | const |
| 1D cavity / 1D lattice | 1 | ω ∝ k | ∝ ω | 1 | const |
| 2D cavity / 2D lattice | 2 | ω ∝ √(k₁²+k₂²) | ∝ ω² | 2 | ∝ ω |
| 3D cavity / 3D lattice | 3 | ω ∝ √(k₁²+k₂²+k₃²) | ∝ ω³ | **3** | ∝ ω² |
| circulant C_N(±1..±K) | 1 (any K) | ω ≈ c_K·k | ∝ ω | 1 | const |

The DOS exponent p **equals the number of independent integer mode indices** — i.e.
the dimension d of the lattice whose spectrum is being counted (verified in test
`Y_NP_035_ExponentEqualsDimension`: 1D lattice count exponent → 1, 2D → 2, 3D → 3 as
the counting radius grows).

**Circulant family C_N(±1..±K).** The exponent is p = 1 for EVERY K (test
`Y_NP_035_CirculantKFamilyIsOneD`: K = 1..12 all give octave exponent p = 1 at large
N). Increasing K only widens the band (Σs² = K(K+1)(2K+1)/6 raises c and the UV cap
√(2K(K+1))-like scale) — it never adds a second index. The ring remains a 1D chain
for every coupling range.

---

## 3. Which structural property forces p ≈ 1 instead of p = 3?

| Candidate | Verdict |
|---|---|
| **A) dimensionality** | **YES — the origin.** D96 is a 1D ring: its spectrum is indexed by ONE integer k. A d-dimensional mode index space gives p = d; d = 1 forces p = 1. |
| B) topology | NO — the ring is a circle (compact 1D). Topology of a ring is 1D, but the exponent is set by the number of independent indices (a 1D open chain also has p = 1). Topology does not add dimension. |
| C) finite mode count | NO — p = 1 persists as N → ∞ (N = 96..6144 all octave-double to p = 1, test `Y_NP_035_SingleIndexDosExponent`). Adding modes along the SAME ring does not add an index. |
| D) circulant structure | NO — a circulant matrix is a 1D periodic lattice; the structure realizes one index. Non-circulant 1D chains also give p = 1. The circulant form sets the band profile (cos chain), not the exponent. |
| E) hosted higher-layer geometry | **YES for ω².** The blackbody ω² DOS is the DOS of a genuinely 3D host. It is NOT derivable from the 1D ring (NP_032: no ring size is a 3D cavity); it is hosted higher-layer content (NP_028/034). |

**The exact structural origin of the mismatch is DIMENSIONALITY (A):** the D96 mode
set lives on a 1D ring, carries a single integer mode index k, and therefore has the
1D density of states N(ω) ∝ ω (p = 1). The observed blackbody DOS ω² (p = 3) is the
density of a 3D mode-index space — a hosted higher-layer geometry that the 1D ring
cannot produce at any N or K.

---

## 4. Test extensions

| Extension | Result |
|---|---|
| **larger N** (96 → 384 → 1536 → 6144) | p stays 1 (test `Y_NP_035_SingleIndexDosExponent`) — finite count refuted |
| **larger K** (C_N(±1..±K), K = 1..12) | p stays 1 (test `Y_NP_035_CirculantKFamilyIsOneD`) — K refuted |
| **coupled D96 rings** (two rings, longer rings) | p stays 1 (test `Y_NP_035_CoupledRingsRemainOneD`) — coupling along 1D keeps 1D |
| **tensor-product rings** C_N^⊗d | p → d: C_N^⊗2 → p ≈ 2, C_N^⊗3 → p ≈ 3 (test `Y_NP_035_TensorProductRaisesDimension`) — adding an INDEPENDENT direction raises the exponent |

The tensor product is the key extension: **only adding an independent spatial
direction raises p.** Two coupled rings sharing the same circle (or a longer ring)
remain 1D; the tensor product C_N ⊗ C_N ⊗ C_N places three independent indices and
produces the 3D DOS.

---

## 5. Minimal construction producing N(ω) ∝ ω³

The minimal construction that produces N(ω) ∝ ω³ **while preserving the D96 local
rule** is the **3D tensor product of three D96 rings**:

```
C_96(±1..±6) ⊗ C_96(±1..±6) ⊗ C_96(±1..±6)
```

Each axis carries the same ±1..±6 nearest-neighbour coupling rule (the D96 local
rule), and the eigenvalues are separable: Λ = λ_k1 + λ_k2 + λ_k3, ω = √Λ. In the
low-frequency limit ω ≈ c·|k| with k ∈ Z³, so the number of positive integer triples
with |k| ≤ R is ~ (4π/3)R³/8 ∝ ω³ (test `Y_NP_035_MinimalW3Construction`). The
construction preserves the D96 local coupling on each axis and reproduces the
blackbody DOS g(ω) ∝ ω² (verified: the integer count matches (π/6)R³, and the DOS
exponent → 3).

This confirms answer E from the other side: the ω² DOS is genuinely higher-layer
content — it requires THREE independent copies of the D96 1D rule, not any
deformation of one ring.

---

## Theorem

> **Theorem (NP_035).** The D96 mode set produces a 1D density of states
> (N(ω) ∝ ω, p = 1) because its spectrum is indexed by a single integer k on a 1D
> ring; the observed blackbody DOS ω² (N ∝ ω³, p = 3) requires a 3D mode-index space,
> which is a hosted higher-layer geometry. Proof: (1) Analytic DOS (Section 1,
> verified): λ_k ≈ (2πk/N)²·91 gives ω_k ≈ c·k (ratio 1.000 at N = 6144, k = 1..4),
> hence N(ω) ∝ ω, p = 1; the D96 octave occupancy [4,4,87] shows 4 modes per low
> octave (constant 1D density) with the top octave holding 87 only because the band
> ends at ω_max = 3.98. (2) p = dimension (Section 2, verified): 1D/2D/3D lattice
> counts give exponents → 1, 2, 3; the circulant family C_N(±1..±K) gives p = 1 for
> every K (K = 1..12 verified) because K changes only the band width, not the index
> count. (3) Origin (Section 3): dimensionality (A) is the cause — one integer index
> forces p = 1; topology stays 1D; finite count refuted (p = 1 at N = 6144); circulant
> structure refuted (K-independent); the ω² DOS is hosted higher-layer content (E).
> (4) Extensions (Section 4, verified): larger N, larger K, and coupled rings keep
> p = 1; tensor products C_N^⊗2 → p ≈ 2 and C_N^⊗3 → p ≈ 3 raise the exponent only by
> adding independent directions. (5) Minimal construction (Section 5, verified): the
> 3D tensor product of three D96 rings preserves the ±1..±6 local rule on each axis
> and produces N(ω) ∝ ω³, DOS ∝ ω². Classification: 1D DOS exponent p = 1 of the D96
> ring DERIVED (analytic: single index, linear dispersion); the exponent p = number of
> independent mode indices DERIVED; blackbody ω² DOS as the DOS of a 3D host
> CORRESPONDENCE (hosted higher-layer geometry, NP_028/034); D96/any ring as a 3D
> blackbody host FALSIFIED (NP_032 unchanged); finite count / topology / circulant
> structure as the cause FALSIFIED; the 3D tensor-product construction preserving the
> D96 local rule DERIVED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) derive the linear dispersion and count. (2) compare exponents.
> (3) identify dimensionality. (4) test extensions. (5) construct the 3D product. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "A larger D96 ring becomes a 3D cavity" | p = 1 at N = 6144 — adding modes on the SAME 1D ring adds no index direction |
| "A larger coupling K produces ω²" | C_N(±1..±K) gives p = 1 for K = 1..12 — K changes the band width, not the dimension |
| "The circulant structure causes p = 1" | any 1D chain (circulant or not) has p = 1; the exponent is set by the number of indices, not the matrix symmetry |
| "The finite 95-mode count is the cause" | p = 1 persists to N → ∞; the count changes the number of modes, not their index dimensionality |
| "Topology (the ring being closed) forces p = 1" | an open 1D chain also has p = 1; the ring's topology is 1D but the exponent is the index count |
| "D96's mid-band exponent ~1.5 is its DOS exponent" | it is a finite-band artifact of top-heavy clustering near the hard cap; the low-frequency (thermodynamic) exponent is p = 1 |
| "Tensor products change the local rule" | C_N(±1..±6)^⊗3 applies the SAME ±1..±6 rule on each axis — the local rule is preserved, only the number of directions grows |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| the D96 DOS exponent is p = 1 | a D96-derived N(ω) growing faster than linearly over an octave that is not at the band edge |
| the exponent equals the number of mode indices | a 2D or 3D lattice count whose N(ω) does not scale as ω^d |
| K does not raise p | a ring C_N(±1..±K) with low-frequency exponent > 1 |
| the ω² DOS is hosted 3D content | a single D96 ring (or any 1D chain) reproducing the 3D DOS |
| the 3D tensor product preserves the local rule and gives ω³ | a C_N^⊗3 whose mode count does not scale as ω³ |

---

## Classification

| Component | Status |
|---|---|
| 1D DOS of the D96 ring (N(ω) ∝ ω, p = 1) | **DERIVED** (analytic: single integer index, linear dispersion ω_k = c·k) |
| DOS exponent p = number of independent mode indices (dimension) | **DERIVED** (lattice-count Weyl law, verified 1D→1, 2D→2, 3D→3) |
| dimensionality as the origin of the blackbody DOS mismatch | **DERIVED / answer A CONFIRMED** (one index → p = 1) |
| hosted ω² DOS as the content the blackbody needs | **CORRESPONDENCE** (hosted higher-layer 3D geometry, unchanged NP_028/034) |
| D96 / any C_N(±1..±K) ring as a 3D blackbody DOS host | **FALSIFIED** (unchanged, NP_032: p = 1 for all K, all N) |
| finite mode count / topology / circulant structure as the cause | **FALSIFIED** (all keep p = 1) |
| 3D tensor product C_N^⊗3 (same local rule per axis) → N ∝ ω³ | **DERIVED** (minimal construction preserving the D96 local rule) |
| temperature / occupation layer | **BOUNDARY / EMERGENT** (unchanged, NP_027/028/033) |

**Conclusion:** the exact structural origin of the blackbody DOS mismatch is
DIMENSIONALITY. The D96 ring is a 1D structure — its spectrum is indexed by a single
integer k with a linear low-frequency dispersion ω_k = c·k — and therefore has the 1D
density of states N(ω) ∝ ω (p = 1). Larger N, larger K, coupling into longer rings,
and the circulant structure all preserve p = 1 because none adds an independent mode
index. The observed ω² DOS (p = 3) is the density of a 3D mode-index space: it is
hosted higher-layer geometry (CORRESPONDENCE), produced minimally by the 3D tensor
product of three D96 rings that preserves the ±1..±6 local rule on each axis. Bose
occupation (EMERGENT, NP_033) and dimensionality jointly explain NP_028's original
verdict: the D96 mode set is the wrong DOS because it is 1D, not because of its
occupation, count, coupling range, or matrix symmetry. No new primitive; canonical AT
unchanged.

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_035_Tests.cs`
**Run:** 2026-09-03 · **Result:** see `Tests/Results/Y_NP_035_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_035_LowFrequencyDispersion` | ω_k ≈ c·k (linear, converges to 1D chain as N → ∞) | ✅ |
| `Y_NP_035_SingleIndexDosExponent` | octave doubling p = 1 at N = 96..6144 | ✅ |
| `Y_NP_035_ExponentEqualsDimension` | 1D→1, 2D→2, 3D→3 lattice exponents | ✅ |
| `Y_NP_035_CirculantKFamilyIsOneD` | p = 1 for C_N(±1..±K), K = 1..12 | ✅ |
| `Y_NP_035_CoupledRingsRemainOneD` | coupled/longer rings keep p = 1 | ✅ |
| `Y_NP_035_TensorProductRaisesDimension` | C_N^⊗2 → p ≈ 2; C_N^⊗3 → p ≈ 3 | ✅ |
| `Y_NP_035_MinimalW3Construction` | 3D tensor of D96 rings → N ∝ ω³, DOS ∝ ω² | ✅ |
| `Y_NP_035_D96TopHeavinessIsFiniteBandEffect` | [4,4,87]: low octaves 4/4 (p=1), top = cap | ✅ |
| `Y_NP_035_Classification` | A confirmed; B/C/D refuted; E hosted | ✅ |
| `Y_NP_035_Run` | research report | ✅ |

**Conclusion:** the D96 mode set is a 1D structure — one integer mode index, linear
dispersion, N(ω) ∝ ω (p = 1). The blackbody ω² DOS requires three independent mode
indices and is hosted higher-layer content, minimally realized by the 3D tensor
product of three D96 rings with the same local rule per axis. Dimensionality, not
topology, count, coupling range, or circulant symmetry, is the origin of the DOS
mismatch. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_035"`

---

## References

- ResearchY-NP_028 (blackbody FALSIFIED; DOS sub-power ~ω^1.5 mid-band / 1.0 low-band),
  NP_032 (every C_N(±1..±K) ring is 1D: linear dispersion, N-independent UV cap), NP_034
  (Bose occupation sufficient; mode set is the minimal obstruction; hosted ω² DOS),
  D_008/D_030 (D96 spectrum; occupancy [4,4,87]), D_021 (pairing), S_001 (synthesis).
