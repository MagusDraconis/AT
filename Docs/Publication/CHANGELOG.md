# Changelog — THE Q-MODEL

All notable changes to THE Q-MODEL (TQM) are documented here. Versions follow the
repository's release cadence; this file covers the publication-relevant milestones.

## [1.1.0] — 2026-08-16

**Release status: Program G4 complete (native metric-to-operator coupling).**

### Added
- **Program G4** — Native Metric-to-Operator Coupling (13 phases, 39 deterministic research
  tests; `TQM.Core/ResearchXH/`, `TQM.Tests/ResearchXH/`, `Docs/Research/G4*.md`):
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
- `Docs/TQM_LabBook.md`: G4 program section added (Total 46 → 59).

---

## [1.0.0] — 2026-08-15 (revised)

**Release status: READY_FOR_WHITEPAPER — NOT_READY_FOR_JOURNAL.**

### Added
- Formal primitive definitions (§2) with an explicit ontology layer and dynamical layer
  (quantum postulates, TQM-155).
- Dynamical System Summary (§3): graph Laplacian $L_Q$, tight-binding identity $H=tL_Q$,
  Schrödinger from reversibility.
- Complexity functional (§6): weighted six-component observer-viability decomposition.
- Quantitative predictions section (§11): RAR, $w(z)$, $\Lambda(t)$, log-normal law, $N\ge3$.
- Scope and limitations section (§15): honest disclosure of gauge dynamics (A-07),
  uncalibrated confidence, ontological phase-gravity, deferred unified action.
- Executable verification of the continuum-limit chain (TQM.Tests/ResearchXC):
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
