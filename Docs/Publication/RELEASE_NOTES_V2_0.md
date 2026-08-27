# Release Notes — The Actualization Theory, Monograph V2.0

**Version:** 2.0.0 · **Date:** 2026-08-27 · **Build:** 95 pages, 0 errors, 0 undefined references, 0 multiply-defined labels

## Summary

Monograph V2.0 is the publication-hardened build of *The Actualization Theory — A
Reconstruction of Physics from Difference, Actualization and Spectrum*. This release
consolidates the full referee-hardening pass: every major claim is classified by
derivational status, the D96 spectrum is reproducible from a stated graph, all
previously undefined symbols are defined, and the monograph and the AT.App present
registry-consistent, status-aware wording.

## What's in this release

- **Monograph PDF** — `Docs/Publication/V2.0/main.pdf` (95 pages), 14 chapters +
  FrontMatter + Conclusion + Appendices.
- **Claim-classification registry** — `Docs/Research/ATQG_ClaimClassificationRegistry.md`:
  16 major claims classified as theorem / necessity / correspondence / calibration /
  hosted / fit (source of truth for all wording).
- **D96 reproducibility** — the canonical attractor graph `C96(±1..±6)`, its Laplacian
  eigenvalues `λ_k = 2Σ(1−cos 2πdk/96)`, and the `ω = √λ` convention are stated in the
  monograph; all moments, multiplicities, span, and occMom reproduce exactly from the
  published graph alone.
- **Closure patches (this commit)** — `MONO_PHASE001`, `MONO_FREEZE001`,
  `MONO_PHASE002`, `MONO_FREEZE002`:
  - State-phase lattice `θ_k = 2πk/96` distinguished from continuous mixing/CP phases
    `δ = asin(r)` (not lattice-restricted).
  - Undefined symbols defined at first use: `σ_occ` (occupation-density scalar),
    `δ_d` (down-sector effective dimension), `Ω_0/Ω_2` (octave-family centers, distinct
    from Laplacian modes `ω_k`), `K_gen` (generations), `K_oct` (occupied octave
    classes), `α_2` (Majorana phase, = 0).
  - `μ^k` clarified as combinatorial path multiplicity, distinct from the conserved
    Q-event count.
  - Peak-ratio constructions stated: `r21 = (Σm−#d)·occ0/occ2`, `r31 = span/√3`.
- **AT.App** — claim-status badges (THEOREM / NECESSITY / CORRESPONDENCE / CALIBRATION /
  HOSTED / FIT) on Theory and Validation pages.

## Verification

- `pdflatex` ×3: clean — 95 pages, 0 errors, 0 undefined references, 0 multiply-defined labels.
- D96 values recomputed independently from the published graph: `[42×2,5,6]`, `Σm=95`,
  `Σ√m=64.08`, `Σm²=229`, `span=6.40`, `occMom=1900.25` — all exact.
- `dotnet build AT.App`: 0 errors.
- All changes are wording/definition only — no physics, equations, numbers, predictions,
  or claim statuses were altered.

## Open items (documented, non-blocking)

- Observable-selection non-uniqueness (selection principle not globally unique).
- Sector-label non-uniqueness (supported assignments, not a unique mapping).
- Gauge correspondence vs hosted structure (1+3+8 dimensional correspondence; groups hosted).
- ℓ₁ fitted normalization — 5/4 is a documented FIT (QG297), removable (QG289); peak
  ratios remain pure spectral correspondences.

**Status:** READY FOR RELEASE.
