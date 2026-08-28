# ResearchY-D_001 — Standing Wave Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_001 (permanent)
**Title:** Standing Wave Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_001.md`
**Depends on:** ResearchY-C_001 (no spatial center), C_002 (propagation not radial)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_001_Tests.cs`

---

## Purpose

Determine whether **standing waves can exist on C96 without center-based geometry**, and
classify the standing structure as **spatial, spectral, or hybrid**.

## Accepted (from C_001, C_002)

- C96 has no spatial center (C_001).
- Propagation is not radial; the ring is vertex-transitive (C_002).
- The canonical readout is global (spectral projection).

---

## 1. Formal Definition of a Standing Wave

**Definition (standing wave on a graph).** A *standing wave* on C96 is a time-harmonic
field

```
ψ(n, t) = φ(n) · cos(ωt + δ)
```

whose spatial part φ is a stationary eigenfunction of the graph Laplacian and whose
frequency is the corresponding normal-mode frequency:

```
L φ = λ φ,    ω = √λ,    φ(n) = cos(2πkn/N + δ)   (Fourier mode on the ring).
```

A standing wave is the sum of two counter-propagating traveling waves of equal amplitude:
ψ = cos(ωt + δ) cos(2πkn/N) = ½[cos(2πkn/N + ωt + δ) + cos(2πkn/N − ωt − δ)]. The spatial
pattern (nodes at fixed positions) is stationary; only the phase oscillates.

---

## 2. Expression with Laplacian Eigenmodes

The eigenmodes of C96 (a circulant graph) are the discrete Fourier modes
φ_k(n) = cos(2πkn/N) and ψ_k(n) = sin(2πkn/N), with eigenvalues
λ_k = 2Σ_{d=1..6}(1 − cos 2πdk/96) and frequencies ω_k = √λ_k.

The general standing wave is a superposition

```
ψ(n,t) = Σ_k [a_k cos(2πkn/N) + b_k sin(2πkn/N)] cos(ω_k t).
```

Key property: **the mode functions are translation-invariant in structure** — shifting
the pattern by one site (n → n+1) is a rotation of the same harmonic; no origin enters.
The node positions of mode k are the solutions of cos(2πkn/N + δ) = 0, which depend only
on k (and the phase δ), never on a distinguished site.

---

## 3. Geometric vs Spectral Standing Wave

| | Geometric standing wave | Spectral standing wave |
|---|---|---|
| What it is | a stationary spatial pattern (fixed nodes) | a time-harmonic eigenfunction of L |
| Source | the ring's spatial structure (harmonics) | the graph spectrum (ω_k = √λ_k) |
| Center required? | NO (modes are translation-invariant) | NO (spectrum is centerless) |
| Content | spatial harmonic cos(2πkn/N) | eigenfrequency ω_k = √λ_k |
| Role | the pattern | the frequency |

The two are *faces of the same object*: the spectral standing wave (eigenmode) has a
geometric realization (the spatial harmonic), and the geometric standing wave (harmonic)
carries a spectral frequency. Neither requires a center.

---

## 4. Zero Mode Test

The zero mode λ₀ = 0, ω₀ = √0 = 0 has the constant eigenvector φ₀(n) = 1/√N. It is a
"standing wave" with zero frequency: ψ(n,t) = φ₀(n)·cos(0·t) = φ₀(n) — a static,
perfectly uniform pattern. It is the **rest state** (the reference, C_001/A_002), not an
oscillating standing wave. It exists on the centerless ring (constant everywhere), and it
is the only mode with zero frequency.

---

## 5. Resonant Mode Pairs

The ring's Z2 symmetry gives degenerate pairs λ_k = λ_{N−k} (same frequency). The two
members of a pair are the cos and sin harmonics:

- φ_k(n) = cos(2πkn/N) and ψ_k(n) = sin(2πkn/N), both at ω_k = √λ_k.

A *resonant pair* is a degenerate pair of standing modes (same ω). These exist for the 42
doublet groups (multiplicity 2) plus the multiplicity-5 and multiplicity-6 groups. The
fundamental doublet (k = 1, N−1) at ω₁ = 0.6216 is the pair that carries the "first peak"
structure (A_001). Degeneracy does not require a center — it follows from the ring's ±k
symmetry.

---

## 6. Classification: spatial, spectral, or hybrid?

The standing structure on C96 is **HYBRID**, but **center-free**:

- **Spatial content:** the modes are spatial harmonics on the ring (the geometric
  standing wave) — a spatial substrate exists (the ring), but it is centerless
  (translation-invariant, C_002).
- **Spectral content:** the frequencies are the Laplacian eigenvalues ω_k = √λ_k (the
  spectral standing wave) — the spectral readout of the ring (A_003 rev.2/A_005).
- **Hybrid:** a standing wave is both a spatial pattern (harmonics) and a spectral
  object (eigenfrequency). Neither component requires a center.

**Answer to the question: standing waves exist on C96 without center-based geometry —
YES.** The ring's harmonics are centerless; the spectrum is centerless; the standing
structure is the hybrid (spatial pattern + spectral frequency) with no origin involved.

---

## Theorem-Style Verdict

> **Theorem (D_001).** Standing waves exist on C96 without any center-based geometry.
>
> *Proof sketch.* The eigenmodes of C96 are the Fourier modes
> φ_k(n) = cos(2πkn/N) (Section 2). Their node positions depend only on the mode index k
> and an arbitrary phase, never on a distinguished site: shifting the pattern is a
> rotation (an automorphism of the ring), so no origin is selected (C_002). The
> frequencies ω_k = √λ_k are the eigenvalues of the graph Laplacian, which is
> translation-invariant. Hence every mode is a standing wave (time-harmonic eigenfunction)
> whose spatial and spectral structure is centerless. The zero mode (ω₀ = 0) is the
> uniform rest state, also centerless. ∎

---

## Invariant Formulation

The standing-wave content of C96 is **translation-invariant (rotation-invariant)**:

- The *spectrum* {λ_k} is invariant under all automorphisms (the eigenvalues of L do not
  depend on any labeling).
- The *mode set* {cos(2πkn/N), sin(2πkn/N)} is invariant as a *set* under rotations
  (a rotation maps one harmonic to another harmonic of the same k).
- The *standing structure* (spectrum + mode set) is therefore invariant content; any
  particular pattern (choice of phase δ) is a gauge choice (C_002), not a distinguished
  structure.

---

## Counterexamples

1. **Center-requiring counterexample.** A standing wave on a string with fixed endpoints
   requires boundaries/center structure. On C96 there are no boundaries (closed ring) and
   no center — the modes exist without either. The "fixed-end" standing wave is NOT the
   C96 case.
2. **Radial-standing-wave counterexample.** A radial standing wave (concentric shells
   oscillating about a center) would require a center. C96 has no center (C_001), and its
   modes are not radial (C_002) — they are ring harmonics. A radial standing wave does not
   exist on C96.
3. **Spatial-only counterexample.** A standing wave is not merely spatial: the pattern
   without a frequency (a static harmonic, no ω) is not a standing wave (it is a fixed
   pattern). The spectral frequency is essential — the standing structure is hybrid.
4. **Origin-dependent-pattern counterexample.** A naive "nodal line through the center"
   picture fails: the modes of the ring have nodes at positions depending only on k, with
   no reference to any origin. There is no nodal line through a "center" (there is no
   center).

---

## Pass/Fail Classification

| Claim | Classification |
|---|---|
| Standing waves exist on C96 | PASS (Fourier modes are time-harmonic eigenfunctions) |
| Standing waves require a center | **FAIL** (modes are translation-invariant; no origin enters) |
| The zero mode is a standing wave | PASS (ω₀ = 0, uniform rest state) |
| Resonant mode pairs exist | PASS (Z2 degenerate pairs, 42 doublets + 5 + 6 groups) |
| Standing structure is spatial-only | **FAIL** (the frequency is essential — spectral) |
| Standing structure is spectral-only | **FAIL** (the pattern is a spatial harmonic) |
| Standing structure is hybrid | PASS (spatial pattern + spectral frequency) |
| Standing structure is center-based | **FAIL** (center-free) |

---

## Research Conclusions

1. **Standing waves on C96 exist** — the Fourier modes are time-harmonic eigenfunctions of
   the graph Laplacian (Definition, Section 1).
2. **They require no center** — the modes are translation-invariant (node positions depend
   only on k and an arbitrary phase; rotations are automorphisms), consistent with C_001/
   C_002.
3. **Geometric vs spectral:** the standing wave is both a spatial harmonic (geometric) and
   an eigenfrequency (spectral) — two faces of one object.
4. **The zero mode** (ω₀ = 0) is the uniform rest state — a degenerate (zero-frequency)
   standing wave, the reference (C_001).
5. **Resonant pairs** are the Z2-degenerate mode pairs (λ_k = λ_{N−k}), existing on the
   centerless ring.
6. **Classification: HYBRID and center-free** — spatial pattern + spectral frequency, with
   no origin.

**Final verdict: standing waves exist on C96 without center-based geometry — YES.** The
standing structure is a center-free hybrid (spatial harmonics + spectral frequencies).
No new primitives; canonical AT unchanged.

---

## Open Problems

1. **Phase gauge (D_001 OP1).** The pattern's phase δ is a gauge choice (C_002). Does any
   canonical observable fix δ (breaking the rotation invariance)? (Currently nothing does.)
2. **Resonant-pair content (D_001 OP2).** The degenerate pairs (42 doublets) carry the
   spectral content; does the pair structure map to any observable (e.g., the weak-isospin
   doublets, A_001 R4)? (Supporting interpretation only.)
3. **Zero mode as degenerate standing wave (D_001 OP3).** ω₀ = 0 is a degenerate
   standing-wave limit. Is its role (reference vs wave) fully captured by the reference
   reading (A_002 RQ7)?

---

## Next Steps

- **ResearchY-D_002 (Standing Wave Model):** the hybrid center-free standing structure is
   the substrate for a standing-wave model; D_002 can test the phase dynamics (OP1).
- **ResearchY-A_003 follow-up:** the spectral readout (A_003 rev.2) is the frequency
   content of the standing waves.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_001_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_001_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_001_FormalDefinition` | standing wave = time-harmonic eigenfunction (Fourier mode) | ✅ |
| `Y_D_001_EigenmodeExpression` | L φ_k = λ_k φ_k; ω_k = √λ_k; modes are cos/sin harmonics | ✅ |
| `Y_D_001_GeometricVsSpectral` | geometric (spatial harmonic) vs spectral (frequency) — faces of one object | ✅ |
| `Y_D_001_ZeroMode` | λ₀=ω₀=0, constant — uniform rest state (degenerate standing wave) | ✅ |
| `Y_D_001_ResonantPairs` | Z2 degenerate pairs λ_k=λ_{N−k} (42 doublets + 5 + 6 groups) | ✅ |
| `Y_D_001_Classification` | hybrid (spatial + spectral), center-free | ✅ |
| `Y_D_001_Run` | Research report | ✅ |

**Conclusion:** standing waves exist on C96 without center-based geometry — the standing
structure is a center-free hybrid (spatial harmonics + spectral frequencies). No canonical
value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_001"`

---

## References

- ResearchY-C_001 (no center), C_002 (no radial propagation; vertex-transitivity).
- ResearchY-A_001 (R5: D96 as standing-wave content of the ring; fundamental doublet),
  A_003 rev.2 (spectral readout), A_005 (eigenbasis derived from the attractor).
- Monograph V2.0: Ch5/Ch6 (C96, Laplacian spectrum, Z2 pairing).
