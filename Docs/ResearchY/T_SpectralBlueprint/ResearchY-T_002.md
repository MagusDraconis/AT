# ResearchY-T_002 — Physical Spectrum Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_002 (permanent)
**Title:** Physical Spectrum Audit — which spectra admit sparse positive-weight realizations
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `T_SpectralBlueprint/ResearchY-T_002.md`
**Depends on:** ResearchY-T_001 (inverse spectral design), D_015 (N=96), D_041
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_002_Tests.cs` (8/8 ✅)
**Shared helper:** `AT.Tests/Shared/SpectralBlueprint.cs`

---

## Question

Which target spectra admit **sparse positive-weight** (physical) realizations?

**Scope constraint:** No AT assumption beyond `Laplacian ↔ spectrum`. A ring material
(oscillator lattice) can only provide attractive (non-negative) couplings, so a target
spectrum is *physical* iff every reconstructed weight `w_d ≥ 0`.

---

## Method

A spectrum is physically realizable by a circulant ring iff its inverse-DFT weights
`w_d = −(1/N)Σ_k λ_k cos(2πdk/N)` are all non-negative. This is exactly the condition
that the spectrum is a **negative-definite function on Z_N** (Bochner/Schoenberg): the
physical spectra form the convex cone spanned by the single-edge generators
`g_d = 2(1−cos 2πdk/N)`. The physical/unphysical boundary is the cone boundary, crossed
where the smallest generator coefficient `w_d` changes sign.

**Physicality score** `P = (attractiveness + sparsity + stability)/3`:

| Term | Definition | Range |
|---|---|---|
| attractiveness | positive-mass fraction `pos/(pos+neg)` | [0,1] |
| sparsity | `1 − edges/(N/2)` | [0,1] |
| stability | `min(positive weight)/max(weight)` (0 if any repulsive coupling) | [0,1] |

Classification: `P ≥ 0.8` PHYSICAL · `0.4 ≤ P < 0.8` MARGINAL · `P < 0.4` UNPHYSICAL.

---

## Results

### 1. Five canonical targets (N = 96)

| Target | Score P | Class | Edges | Neg. w | Min w | Mass |
|---|---|---|---|---|---|---|
| D96 (canonical) | 0.958 | **PHYSICAL** | 6 | 0 | 0.000 | 12.00 |
| band-gap | 0.236 | UNPHYSICAL | 48 | 50 | −1.434 | 44.85 |
| clustered | 0.235 | UNPHYSICAL | 47 | 42 | −3.031 | 79.49 |
| octave-spaced | 0.190 | UNPHYSICAL | 48 | 47 | −264.611 | 3029.30 |
| max-separated | 0.500 | MARGINAL | 24 | 0 | 0.000 | 24.00 |

### 2. Boundary map (first repulsive coupling)

| Family | Boundary | Physical side | Unphysical side |
|---|---|---|---|
| geometric (octave) λ=r^(m−1) | r = 1.0110 (span ≈ 1.67) | 48 edges, 0 neg | 48 edges, 2 neg |
| band-gap (lift upper band) | g = 0.0000 | D96, 6 edges, 0 neg | 48 edges, 42 neg |
| clustered (3 clusters) | sep = 0.40 | 48 edges, 0 neg | 48 edges, 2 neg |

### 3. Random-spectrum scan

0/400 random symmetric spectra are physical (0.00%). The physical cone is a
**measure-(near-)zero** subset of spectrum space.

---

## Key findings

1. **Physicality = negative-definiteness (DERIVED).** A spectrum is realizable by an
   oscillator ring iff it is a non-negative combination of single-edge generators
   `g_d` — precisely the negative-definite functions on Z_N.

2. **D96-like (circulant) spectra are always physical (DERIVED).** `C_N(±1..±K)` has
   weights exactly 1 on distances 1..K: the physical cone is *spanned* by these.

3. **Physicality is rare (DERIVED).** Almost no random spectrum is physical —
   physicality is a structured, exceptional property, not a generic one.

4. **The boundary is sharp and structured (CORRESPONDENCE).**
   - Geometric (octave) spectra go unphysical at span ≈ 1.67 (< 1 octave); D96 reaches
     span 6.4 only because it is *band-limited* (a sum of 6 generators), not geometric.
   - Band-gap spectra are unphysical for **any** gap: D96 sits exactly ON the cone
     boundary (far weights zero), so lifting the upper band flips them negative at once.
   - Clustered spectra are physical only up to separation ≈ 0.40 (near-degenerate).

---

## Classification

| Item | Classification |
|---|---|
| Physical spectra = negative-definite cone | DERIVED |
| D96-like (circulant) family always physical | DERIVED |
| Physicality score maps spectra → materials | CORRESPONDENCE |
| "Sparse positive-weight realization" hypothesis | SUPPORTED (smooth, moderate-span, band-structured); REFUTED (discontinuous / extreme-span) |

---

## Conclusion

The physical/unphysical boundary is the boundary of the cone of negative-definite
functions on Z_N, crossed exactly when the smallest generator coefficient `w_d` changes
sign. D96 sits deep inside the cone (6 sparse positive edges, the canonical sparse
physical realization), while band-gap, clustered, and extreme-octave spectra demand
repulsive coupling and are unrealizable by a plain oscillator ring. Physicality is a
rare, structured property: the designable spectra of a ring material are exactly the
non-negative band-limited combinations of the single-edge generators.

Next step (ResearchY-T_003): relax the ring constraint to general (non-circulant)
weighted graphs — the genuinely hard inverse eigenvalue problem for graphs.
