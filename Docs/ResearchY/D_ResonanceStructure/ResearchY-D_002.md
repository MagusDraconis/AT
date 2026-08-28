# ResearchY-D_002 — Standing Wave Model

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_002 (permanent)
**Title:** Standing Wave Model
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_002.md`
**Depends on:** ResearchY-C_001 (no center), C_002 (non-radial), D_001 (standing waves
exist)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_002_Tests.cs`

---

## Purpose

**Construct the canonical standing wave model** of C96: the mode decomposition, the
resonant pair structure, the zero mode role, the 47 Z2-pair analysis, the spatial vs
spectral content, and the closure consistency — and classify the model as **GEOMETRIC,
SPECTRAL, or HYBRID**.

## Accepted (from C_001, C_002, D_001)

- C96 has no spatial center (C_001).
- Propagation is non-radial; the ring is vertex-transitive (C_002).
- Standing waves exist on C96: the Fourier modes are time-harmonic eigenfunctions,
  center-free, hybrid (D_001).

---

## 1. Mode Decomposition

The canonical standing wave model is the modal decomposition of the graph Laplacian of
C96. The eigenmodes are the discrete Fourier harmonics

```
φ_k(n) = cos(2πkn/N),   ψ_k(n) = sin(2πkn/N),   k = 1..95,
```

with eigenvalues λ_k = 2Σ_{d=1..6}(1 − cos 2πdk/96) and frequencies ω_k = √λ_k. The
general standing wave is

```
Ψ(n,t) = Σ_k [a_k cos(2πkn/N) + b_k sin(2πkn/N)] cos(ω_k t).
```

The decomposition is **complete** (95 positive modes + 1 zero mode), **orthogonal**
(Fourier basis), and **centerless** (translation-invariant mode functions).

## 2. Resonant Pair Structure

The Z2 symmetry of the ring pairs modes k and N−k with the same eigenvalue (degenerate
pair). Each pair provides two real harmonics (cos and sin) at the same frequency:

```
pair k:  {cos(2πkn/N), sin(2πkn/N)}  both at ω_k = ω_{N−k}.
```

- **47 Z2 pairs** (k = 1..47, each paired with N−k = 95..49) provide 94 real modes.
- **1 self-conjugate mode** (k = 48, the anti-podal harmonic) is not paired.
- Total: 94 + 1 = **95 positive modes**.

The resonant pair is the fundamental building block: each pair is a two-dimensional
degenerate oscillator (two phases of the same frequency).

## 3. Zero Mode Role

The zero mode λ₀ = 0, ω₀ = 0 has the constant eigenvector φ₀(n) = 1/√N. In the standing
wave model it is:

- **The rest state** (zero frequency, no oscillation) — the reference (C_001).
- **The uniform background** against which all positive modes oscillate (A_002 RQ7).
- **The closure constant** (ω₀ = 0 is the only zero-frequency mode; it is the
  translation-invariant member of the decomposition).

The zero mode carries no standing-wave content (no oscillation); it is the medium's
reference level.

## 4. 47 Z2 Pair Analysis

The 47 pairs decompose the 95 positive modes:

| Structure | Count |
|---|---|
| Z2 pairs (k = 1..47, paired with N−k) | 47 |
| real modes from pairs (2 per pair) | 94 |
| self-conjugate mode (k = 48) | 1 |
| **total positive modes** | **95** |

Each pair is a **resonant doublet** — two degenerate harmonics. This is the spectral
source of the doublet structure (weak-isospin doublets read as ring-mode degeneracy,
A_001 R4). The octave bands organize the pairs by frequency: the band occupancies
[4, 4, 87] are counts of modes (94 paired + 1 self-conjugate) by octave of ω.

## 5. Spatial vs Spectral Content

| Content | Object | Center? |
|---|---|---|
| Spatial (geometric) | the harmonics cos(2πkn/N), sin(2πkn/N) | center-free (translation-invariant) |
| Spectral | the eigenvalues λ_k, frequencies ω_k = √λ_k | center-free (rotation-invariant) |
| Hybrid | the standing wave Ψ = harmonic × cos(ωt) | center-free |

The model is **hybrid**: each standing wave is a spatial pattern (harmonic) oscillating
at a spectral frequency (eigenvalue). Neither component is center-based (C_001, C_002).

## 6. Closure Consistency

The model is consistent with closure:

- **R^N = identity** (B_003): the ring closes; the modes are N-periodic
  (φ_k(n+N) = φ_k(n)).
- **θ_{k+N} ≡ θ_k** (B_003): the phase lattice closes; the full cycle exists.
- **z_k^N = 1** (B_003): the eigenmode rotations close algebraically.
- **The spectrum is algebraic** (B_002): all eigenvalues are algebraic; the model uses no
  transcendental value (π enters only as the parametrization of the roots of unity, a
  role, B_003).

The standing wave model is therefore a *center-free hybrid* built entirely from the
closed ring's algebraic content.

---

## Classification

> **HYBRID.** The canonical standing wave model of C96 is a hybrid: spatial harmonics
> (geometric content) oscillating at spectral eigenvalues (spectral content), with the
> zero mode as the uniform reference and the 47 Z2 pairs as the resonant doublet
> structure — all center-free and closure-consistent.

---

## Theorem

> **Theorem (D_002).** The canonical standing wave model of C96 is a center-free hybrid
> standing-wave decomposition: Ψ(n,t) = Σ_k [a_k cos(2πkn/N) + b_k sin(2πkn/N)] cos(ω_k t),
> with 95 positive modes organized into 47 Z2 resonant pairs plus one self-conjugate
> mode, the zero mode as the uniform rest state, and the spectrum algebraic and
> closure-consistent.
>
> *Proof sketch.* (1) Completeness: the Fourier modes are the 95 positive eigenmodes of
> the circulant Laplacian plus the zero mode (Section 1). (2) Pairing: the Z2 symmetry
> λ_k = λ_{N−k} gives 47 degenerate pairs (94 real modes) and one self-conjugate mode
> (k = 48), total 95 (Section 4). (3) Zero mode: λ₀ = 0, constant, the reference
> (Section 3). (4) Hybrid: each standing wave is a spatial harmonic × spectral frequency
> (Section 5). (5) Closure: R^N = identity, θ_{k+N} ≡ θ_k, z_k^N = 1, algebraic spectrum
> (Section 6). Hence the model is a center-free, closure-consistent hybrid. ∎

---

## Invariant Formulation

The standing wave model is **translation-invariant** (rotation-invariant):

- The spectrum {λ_k} is invariant under all automorphisms (rotation-invariant content).
- The mode set {cos(2πkn/N), sin(2πkn/N)} is invariant as a set under rotations (a
  rotation maps one harmonic to another of the same k).
- The pair structure (47 pairs) is invariant (Z2 is an automorphism symmetry).
- The model content — spectrum, pairs, octave bands, zero mode — is invariant; any
  particular pattern (choice of phases a_k, b_k, δ) is a gauge choice (C_002), not
  distinguished structure.

---

## Dependency Graph

```
C_001 (no center)
  + C_002 (non-radial, vertex-transitive)
  + D_001 (standing waves exist: Fourier modes are time-harmonic eigenfunctions)
  + B_002 (algebraic spectrum)
  + B_003 (closure: R^N=id, z^N=1, θ_{k+N}≡θ_k)
        ↓
D_002 Standing Wave Model
   ├── mode decomposition (Fourier harmonics, 95+1 modes)
   ├── resonant pair structure (47 Z2 pairs + self-conjugate)
   ├── zero mode role (uniform reference)
   ├── spatial vs spectral content (hybrid)
   └── closure consistency (algebraic, center-free)
```

---

## Research Conclusions

1. **Mode decomposition:** the model is the complete orthogonal Fourier decomposition of
   the ring's Laplacian (95 positive + 1 zero mode), centerless.
2. **Resonant pair structure:** 47 Z2 pairs provide 94 paired real modes; one
   self-conjugate mode (k = 48) completes the 95 — each pair a two-dimensional
   degenerate oscillator.
3. **Zero mode role:** the uniform rest state (ω₀ = 0), the reference against which all
   standing waves oscillate.
4. **47 Z2 pair analysis:** the doublet structure is the ring-mode degeneracy (the
   spectral source of the doublet reading, A_001 R4); octave bands organize the pairs.
5. **Spatial vs spectral:** the model is hybrid — spatial harmonics (geometric) ×
   spectral eigenvalues (spectral).
6. **Closure consistency:** R^N = identity, θ_{k+N} ≡ θ_k, z_k^N = 1; the spectrum is
   algebraic (B_002); π enters only in role (B_003).

**Classification: HYBRID (center-free).** The canonical standing wave model is the
center-free hybrid decomposition of the closed ring's algebraic spectrum.

---

## Open Problems

1. **Phase gauge (D_002 OP1, from D_001).** The phases (a_k, b_k) are gauge choices. Does
   any canonical observable fix them? (Currently nothing does.)
2. **Pair → observable mapping (D_002 OP2).** The 47 Z2 pairs are the doublet structure;
   the mapping to weak-isospin doublets is a supporting interpretation (A_001 R4), not a
   derivation. A canonical mapping is open.
3. **Octave-band pair content (D_002 OP3).** The octave bands [4,4,87] count modes; do
   the bands have pair-level structure (which pairs fall in each band)? (Candidate
   observation.)

---

## Next Steps

- **ResearchY-D_003 (or B_003 follow-up):** the standing wave model is the resonance
  content; test the octave-band pair content (OP3) or the phase-gauge question (OP1).
- **ResearchY-A_003 follow-up:** the spectral readout (A_003 rev.2) is the frequency
  content of the standing wave model.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_002_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_002_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_002_ModeDecomposition` | 95+1 Fourier modes; completeness; orthogonality | ✅ |
| `Y_D_002_ResonantPairs` | 47 Z2 pairs; 94 paired modes; self-conjugate k=48 | ✅ |
| `Y_D_002_ZeroMode` | λ₀=ω₀=0, constant — uniform reference | ✅ |
| `Y_D_002_Z2Pairs` | 47 pairs = 94 modes + 1 = 95; doublet structure | ✅ |
| `Y_D_002_SpatialSpectral` | hybrid: spatial harmonics × spectral frequencies | ✅ |
| `Y_D_002_ClosureConsistency` | R^N=id; θ_{k+N}≡θ_k; z^N=1; algebraic spectrum | ✅ |
| `Y_D_002_Run` | Research report | ✅ |

**Conclusion:** the canonical standing wave model is a center-free HYBRID (spatial
harmonics + spectral eigenvalues), 47 Z2 pairs + self-conjugate mode, zero mode as
reference, closure-consistent. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_002"`

---

## References

- ResearchY-C_001 (no center), C_002 (non-radial), D_001 (standing waves exist).
- ResearchY-B_002 (algebraic spectrum), B_003 (closure invariants).
- ResearchY-A_001 (R4: doublet = ring degeneracy), A_003 rev.2 (spectral readout).
- Monograph V2.0: Ch5/Ch6 (C96, Laplacian spectrum, Z2 pairing), Ch9 (phase lattice).
