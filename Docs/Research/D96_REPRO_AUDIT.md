# D96_REPRO_AUDIT — Graph-Theory Provenance Audit of the D96 Spectrum

**Audit:** Can the D96 spectrum be reconstructed from the provided information?
**Auditor role:** graph-theory referee
**Date:** 2026-08-27
**Status:** **FAIL (monograph-alone) — fully recoverable from the research record, verified exact**

---

## 1. Scope

Items to reconstruct:

| Item | Required |
|---|---|
| Network | the converged N=96 network |
| Adjacency matrix | A ∈ {0,1}^(96×96) |
| Laplacian | L = D − A |
| Multiplicity | [42×2, 5, 6] |
| Modes | 95 positive + 1 zero |
| (moments, span) | Σm=95, Σ√m=64.08, Σm²=229, occMom=1900.25, span=6.40 |

---

## 2. What the monograph provides (Ch5–Ch6)

- "the Laplacian eigenspectrum of the converged N=96 network" (Ch6 Def/Thm `d96-derived`)
- "the graph Laplacian of the N=96 network is a 96×96 matrix with one zero eigenvalue and 95 positive" (Ch6 Thm `95-modes`)
- multiplicity `[42×2, 5, 6]` (Ch6 Thm `multiplicity`)
- moments and span (Ch6 Thm `moment-ladder`, `occmom`, `span`)

**The monograph does NOT specify the graph, the adjacency, or the generator set.**
A reader of the monograph alone cannot reconstruct the spectrum.

---

## 3. What the research record provides

| Source | Content |
|---|---|
| QG116 (ActualizationStructures) | N=96, 576 links, 3 families, span 6.40; dynamics converge to one geometry |
| QG153/155 (DoubletOrigin / Z2SymmetryOrigin) | adjacency is **12-regular**, reflection-invariant (i→n−1−i), half-shift-invariant (i→i+n/2); eigenvalue pairs exact to 4.5e-14 |
| QG159 (D96SelectionOrigin) | **"the observable attractor generates a circulant ring C_96(1..6)"** |

**The graph is defined by QG159: the 12-regular circulant graph on 96 vertices with
generator set**

$$S = \{\pm1,\pm2,\pm3,\pm4,\pm5,\pm6\},\qquad A_{ij}=1 \iff (i-j)\bmod 96 \in S.$$

Links: 96·12/2 = 576 ✓ (matches QG116).

---

## 4. Reconstruction recipe

For a circulant graph $C_{96}(S)$, $S=\{\pm d_1,\dots,\pm d_6\}$, the Laplacian
eigenvalues are closed-form:

$$\lambda_k \;=\; 2\sum_{d=1}^{6}\Bigl(1-\cos\!\tfrac{2\pi d k}{96}\Bigr),\qquad k=0,\dots,95,$$

with modes $\omega_k=\sqrt{\lambda_k}$ (the "95 modes ω = √λ" convention of the
Boundary-Condition audit). This is fully deterministic — no simulation needed.

---

## 5. Verification (computed, exact)

| Claim | Computed from C_96(±1..±6) | Match |
|---|---|---|
| multiplicity | 42× size-2, 1× size-5, 1× size-6 | **EXACT** |
| positive modes | 95 | **EXACT** |
| zero modes | 1 (λ₀=0) | **EXACT** |
| Σm | 95.00 | **EXACT** |
| Σ√m | 64.08 | **EXACT** |
| Σm² | 229.00 | **EXACT** |
| span = ω_max/ω_min | 3.9796/0.6216 = 6.40 | **EXACT** |
| links | 576 | **EXACT** |

The claimed spectrum [42×2,5,6], 95+1 modes, all moments, and span 6.40 are reproduced
**exactly** (to machine precision) by the Laplacian of the graph C_96(±1,…,±6).

Note: span = 6.40 requires the **frequency convention ω=√λ**; the eigenvalue ratio
λ_max/λ_min ≈ 40.99, so the ω=√λ convention must be stated (it is the monograph's
"ω_max/ω_min" definition).

---

## 6. Verdict

**FAIL** — as presented, the monograph does not provide enough information to
reconstruct the network/adjacency/Laplacian.

**However:** the provenance **exists and is verified exact**. The missing piece is a
single specification (the circulant generator set, QG159) plus the ω=√λ convention.
No physics, spectrum, or numbers need to change.

---

## 7. Minimum reproducibility requirements (monograph)

Add to Ch5 or Ch6 (documentation only — no physics change):

1. **Graph definition:** "the converged network is the 12-regular circulant ring
   $C_{96}(\pm1,\ldots,\pm6)$, i.e. $A_{ij}=1$ iff $(i-j)\bmod 96\in\{\pm1,\ldots,\pm6\}$"
   with citation to QG159 (and QG155 for the Z2 automorphisms).
2. **Eigenvalue formula:** $\lambda_k = 2\sum_{d=1}^{6}(1-\cos(2\pi dk/96))$, $k=0,\dots,95$.
3. **Convention:** state explicitly that the modes are $\omega_k=\sqrt{\lambda_k}$ and that
   span $= \omega_{\max}/\omega_{\min}$.

With these three lines, the D96 spectrum is fully, independently reproducible.
