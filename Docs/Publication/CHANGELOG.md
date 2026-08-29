# Changelog — THE Q-MODEL

All notable changes to THE Q-MODEL (AT) are documented here. Versions follow the
repository's release cadence; this file covers the publication-relevant milestones.

## [2.1.0] — 2026-08-30

**Release status: READY FOR TAGGING — ResearchY V2.1 Boundary Program (origin chain complete).**

### Added
- ResearchY V2.1 Boundary Program — 26 origin audits (D_020–D_045) + closure audit
  (R_001): the complete origin chain **Difference → Actualization → Spectrum → N=96 →
  Physics** traced and every origin question classified.
- **V2.1 Boundary Program Closure Audit (R_001)** — the program is **COMPLETE**: the
  final irreducible boundary set has exactly **five items** —
  {Difference, η}, {Z2-paired (complex) sector}, {3 octave families},
  {SU(2) gauge + j=1/2}, {v, m_e} — with 20 objects DERIVED, 10 EMERGENT, 0 OPEN.
- **Boundary reductions** — 13 original boundary items reclassified: complete pairing,
  singleton prohibition, p=3, 6|N, N=96, state identity → **DERIVED**; su(2) compact-form
  → **EMERGENT**; Z2-paired sector, 3-family window, SU(2) gauge, {Difference, η},
  {v, m_e}, π → **BOUNDARY** (confirmed).
- **Derived chain** — tick/count/magnitude/phase → complex state → identity →
  reciprocity → pairing → p=3 → N=96 → Closure → Spectrum → span/ΩΛ/Ωm/v structure.
- Release artifact: `Docs/Publication/RELEASE_NOTES_V2_1.md`, `ResearchY-R_002`.
- AT.App Research News (D_021–D_045, R_001) + Theory Book results (Resonances,
  Cosmology).

### Changed
- Research/audit only — **no canonical AT V2.0 values, equations, predictions, or claim
  statuses altered**.
- AT.App shows a **dynamic version** (from `AtlasDataService.Version` = "2.1.0") in the
  top app bar, nav menu, and home hero (previously hardcoded "v2.0").

### Fixed
- Classification drift between D_028 and D_040 (3-family window / N=96) reconciled via
  the two-level rule (value DERIVED, window/requirement BOUNDARY) + a
  `ClassificationRegistry` guard test.

### Verification
- 372 xUnit tests passing; AT.App builds with 0 errors; canonical D96 values
  independently re-verified (span = 6.4025, λ(48) = 12, min mult 2, v = 254.37 GeV,
  ΩΛ = 0.6839).

### Migration
- None required — V2.1 is additive research; canonical consumers unaffected.

## [2.0.0] — 2026-08-27

**Release status: READY FOR RELEASE — Monograph V2.0 (registry-consistent publication build).**

### Added
- Monograph V2.0 — `Docs/Publication/V2.0/main.pdf` (95 pages): publication-hardened
  build of *The Actualization Theory* (Difference → Actualization → Spectrum → Physics).
- Claim-classification registry — `Docs/Research/ATQG_ClaimClassificationRegistry.md`:
  16 major claims classified theorem / necessity / correspondence / calibration / hosted / fit.
- D96 reproducibility — canonical attractor graph `C96(±1..±6)`, Laplacian eigenvalues
  `λ_k = 2Σ(1−cos 2πdk/96)`, and `ω = √λ` convention stated in the monograph; all D96
  values reproduce exactly from the published graph alone.
- Closure patches — `MONO_PHASE001/002`, `MONO_FREEZE001/002`: phase-type distinction,
  symbol definitions (σ_occ, δ_d, Ω_0/Ω_2, K_gen, K_oct, α_2), μ^k clarification,
  peak-ratio constructions.
- AT.App claim-status badges (THEOREM…FIT) on Theory and Validation pages.
- Release artifacts: `Docs/Publication/RELEASE_NOTES_V2_0.md`,
  `Docs/Publication/Zenodo_Abstract_V2_0.md`.

### Changed
- Status-aware wording across monograph and AT.App aligned to the claim-classification
  registry (wording only — equations, numbers, citations unchanged).
- 5/4 first-peak factor documented as **FIT** (QG297, removable QG289), superseding the
  earlier "boundary projection" reading.
- K=6 link-length parameter disclosed as a selected input; degree-12 connectivity is
  conditional on it (radius-6 and degree-12 are dynamical necessities given K=6).

### Fixed
- 0 undefined references, 0 multiply-defined labels, 0 errors (pdflatex ×3).
- D96 spectrum reconstructible from the monograph alone (previously research-record only).

### Open (documented, non-blocking)
- Observable-selection non-uniqueness; sector-label non-uniqueness; gauge correspondence
  vs hosted structure; ℓ₁ fitted 5/4 normalization.

---

## [1.1.0] — 2026-08-16

**Release status: Program G4 complete (native metric-to-operator coupling).**

### Added
- **Program G4** — Native Metric-to-Operator Coupling (13 phases, 39 deterministic research
  tests; `AT.Core/ResearchXH/`, `AT.Tests/ResearchXH/`, `Docs/Research/G4*.md`):
  - Spectral curvature (Phase 0/1/2A): curvature encoded in graph spectra; SCI calibrated.
  - Time-rate hypothesis (G4-T): rate gradients define conformal geometry.
  - Conformal operator selection (G4-C): Lc = ρ⁻¹ L ρ⁻¹ ≈ Δ_g identified and benchmarked.
  - Curvature reconstruction (G4-C2/3): sign and magnitude recovered from Lc spectra.
  - Uniqueness (G4-U): (1,1) is one member of a large empirical family, the unique conformal
    Laplace–Beltrami representative.
  - Curvature dynamics (G4-D): Lc evolves consistently with the density field.
  - Curvature evolution law (G4-E): R = F(ρ), Ṙ = F′(ρ)·ρ̇ with F′(ρ) < 0.
  - Feedback dynamics (G4-E1): self-consistent but anti-diffusive — bounded cosmology needs a
    restoring term.

### Changed
- `README.md`: version bumped to 1.1; Research Programs table + key results updated.
- `Docs/AT_LabBook.md`: G4 program section added (Total 46 → 59).

---

## [1.0.0] — 2026-08-15 (revised)

**Release status: READY_FOR_WHITEPAPER — NOT_READY_FOR_JOURNAL.**

### Added
- Formal primitive definitions (§2) with an explicit ontology layer and dynamical layer
  (quantum postulates, AT-155).
- Dynamical System Summary (§3): graph Laplacian $L_Q$, tight-binding identity $H=tL_Q$,
  Schrödinger from reversibility.
- Complexity functional (§6): weighted six-component observer-viability decomposition.
- Quantitative predictions section (§11): RAR, $w(z)$, $\Lambda(t)$, log-normal law, $N\ge3$.
- Scope and limitations section (§15): honest disclosure of gauge dynamics (A-07),
  uncalibrated confidence, ontological phase-gravity, deferred unified action.
- Executable verification of the continuum-limit chain (AT.Tests/ResearchXC):
  - $L_Q\to-\nabla^2$ (exact spectrum), BDG $\to\square$ ($O(h^2)$)
  - weighted Laplacian $\to$ Laplace–Beltrami (S¹ example)
  - curved-space Schrödinger (unitary)
  - Einstein tensor chain in standard geometry (2D/3D)
  - metric-origin closure (conformal class = imported Malament theorem)

### Changed
- Replaced "closed" with **"dispositioned"** throughout; internal-3 node flagged as the
  "one open door".
- Relabeled no-gos as **conditional** (T-09 provisional at 0.10).
- Reframed phase-gradient gravity as **ontological** in the weak field (honesty note).
- Scope note: gauge *structure* derived, gauge *dynamics* borrowed (A-07).

### Publication
- Added Zenodo publication package (LaTeX, metadata, citation, changelog).

---

## [0.9.0] — 2026 (pre-release)

- Completed DATA (10), QM (5), and QG (31) research programs.
- Three-category taxonomy (DERIVED / REAL-UNDERIVED / DRAWN) established.
- TRM legacy reconciliation (3 Absorbed / 2 Rejected / 3 Candidate / 1 Open).
- Hostile-review response and v1.0 paper revision.
