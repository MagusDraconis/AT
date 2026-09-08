# ResearchY-T_001 — Spectral Blueprint (Inverse Spectral Design)

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_001 (permanent)
**Title:** Spectral Blueprint — Inverse Spectral Design
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `T_SpectralBlueprint/ResearchY-T_001.md`
**Depends on:** D_015 (N=96), D_041 (spectrum λ_k = 2−2cos(2πk/N)), D_028 (span),
D_040 (3 families)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_001_Tests.cs` (7/7 ✅)

---

## Question

Can material properties be designed by specifying a **target spectrum** and
reconstructing the coupling graph (Laplacian), instead of the forward path
`topology → simulation → observed spectrum`?

**Hypothesis (spectral blueprint):**

```
desired behavior → target eigenvalue spectrum → inverse Laplacian
reconstruction → material topology

instead of

topology → simulation → observed spectrum
```

**Scope constraint:** No AT assumption beyond `Laplacian ↔ spectrum`. The
experiment does not use Q, Difference, or any cosmological input — only the
mathematical statement that a graph's eigenvalues are its Laplacian spectrum.

---

## Method

For a circulant (ring) graph C_N the Laplacian is a circulant matrix whose
eigenvalues are the discrete Fourier transform (DFT) of the first row, and whose
first row is the inverse DFT (IDFT) of the eigenvalues:

```
forward   λ_k = Σ_{d=1..N−1} w_d · (1 − cos 2πdk/N)      (weights → spectrum)
inverse   w_d = −(1/N) Σ_{k=0..N−1} λ_k · cos 2πdk/N     (spectrum → weights)
```

Edge weights `w_d = −c_d ≥ 0` are physical (attractive) coupling. The inverse
problem is therefore **closed form** and deterministic: target spectrum → IDFT →
coupling weights → material topology. N = 96 (the canonical D96 ring).

### Target spectra (all symmetric, λ_0 = 0)

| Target | Definition | Character |
|---|---|---|
| D96 (canonical) | λ_k = 2Σ_{d=1..6}(1−cos 2πdk/96) | 3 octave bands, span 6.4 |
| band-gap | two parabolic bands + empty interval | discontinuous (gap) |
| clustered | 3 degenerate clusters (5 / 25 / 60) | hard steps |
| octave-spaced | λ = 2^((m−1)/4), geometric | extreme span ~3444 |
| max-separated | λ_m = m (uniform integer) | all distinct, minimal degeneracy |

---

## Results

| Target | RMS err | Edges | Neg. weights | Min weight | Coupling mass |
|---|---|---|---|---|---|
| D96 (canonical) | 1.5e-13 | **6** | **0** | 0.000 | 12.00 |
| band-gap | 2.4e-13 | 48 | 50 | −1.434 | 44.85 |
| clustered | 3.2e-13 | 47 | 42 | −3.031 | 79.49 |
| octave-spaced | 1.2e-11 | 48 | 47 | −264.611 | 3029.30 |
| max-separated | 2.7e-13 | 24 | **0** | 0.000 | 24.00 |

### Key findings

1. **Exactness (DERIVED).** The round trip `spectrum → weights → spectrum` closes
   to floating-point precision for **every** target. Inverse spectral design is a
   closed-form identity for circulant graphs.

2. **Isometry (DERIVED).** The inverse map is an isometry:
   `||Δw||₂ = (1/√N) ||Δλ||₂` (condition number 1, Parseval/unitarty of the DFT).
   Reconstruction is maximally robust — a spectral perturbation induces a
   graph perturbation of equal relative magnitude, with no amplification.

3. **Physicality selects spectra (CORRESPONDENCE).** D96 reconstructs to a
   **sparse, all-attractive** coupling (6 edges, all weight 1, degree 12); the
   max-separated (uniform) spectrum is also all-attractive but needs 24 edges.
   Band-gap, clustered, and extreme-octave spectra require **dense coupling with
   negative (repulsive) weights** — couplings a simple oscillator material cannot
   provide. Physicality (non-negative coupling) is the binding constraint on
   which spectra a real material can realize.

4. **D96 is canonical.** Its spectrum reconstructs the connection set
   {±1..±6} exactly — the same sparse attractor geometry found independently in
   the D-group. The D96 octave-band structure (span 6.4, 3 families) is the
   sparse, all-physical solution.

5. **Sparsity–accuracy tradeoff.** 6 edges reproduce D96 exactly; a band-gap
   spectrum needs many more edges for any accuracy.

---

## Classification

| Item | Classification |
|---|---|
| Inverse-map exactness (spectrum ↔ coupling via DFT) | DERIVED |
| Isometry robustness (condition number 1) | DERIVED |
| Physicality constraint selects sparse band spectra | CORRESPONDENCE |
| Hypothesis "target spectrum → low-complexity graph" | SUPPORTED (D96, max-separated); REFUTED (band-gap/cluster/extreme-octave) |

---

## Conclusion

Spectral-blueprint design is exact and optimally robust for circulant (ring)
topologies: the spectrum **is** the Fourier transform of the coupling, so
`target spectrum → material topology` is an inverse DFT. AT's canonical D96
structure is the **sparse, all-physical** solution, while discontinuous or
extreme-span spectra demand repulsive (negative) couplings. Inverse spectral
design is viable; physicality (non-negative coupling) is the binding constraint
on which spectra a real material can realize.

This closes the first step of the "spectral blueprint" program: the inverse map
exists, is closed-form, and is optimally conditioned. The open next step is the
general (non-circulant) weighted-graph inverse spectral problem, which is the
hard case (inverse eigenvalue problem for graphs) and is left to ResearchY-T_002.
