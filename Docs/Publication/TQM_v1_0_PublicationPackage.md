# TQM v1.0 — Publication Package

**Release:** Version 1.0 (revised) — 15 August 2026
**Author:** Fabrice Wieser
**Source:** https://github.com/MagusDraconis/TQM

---

## Publication Caveat

> **READY_FOR_WHITEPAPER — NOT_READY_FOR_JOURNAL.**

This package is released as a **white paper** (research-program description) for archival
on **Zenodo**. It is **not** submitted for peer-reviewed journal publication.

Rationale (from `../Audits/PublicationReadiness_Final.md`):
- The central derivation claim (Einstein recovery from Q-events) remains **logical, not
  mathematical**: the metric and the BDG action are *imported* (proven but not
  TQM-derived), $G=\ell^2c^3/\hbar$ is *dimensional analysis*, and no unique sharp
  prediction yet discriminates TQM from SM + $\Lambda$CDM.
- The Schrödinger continuum limit ($L_Q\to-\nabla^2\to$ Schrödinger) is now **tested**;
  the Einstein side is only **partially** verified (standard chain works, but the native
  metric is external).

---

## Abstract

THE Q-MODEL (TQM) is a theory of structure and content that compresses observable physics
to four primitives — the individuation principle $Q$, Random Actualization, the scale
triad $(\ell,\tau,\hbar)$, and a single continuous nonlinearity parameter $M^2$ — and
holds that the *form* of every structure is **derivable** from these primitives while the
*content* (specific masses, couplings, multiplicities, the Koide angle) is **realized** by
contingent draw. The derivation program yields: $U(1)$ as a theorem
($\mathrm{Aut}(S^1)=U(1)$, $\pi_1(S^1)=\mathbb{Z}$; 0.95), spatial dimensionality
$d=3{+}1$ from the intersection of physical viability windows ($M^2\approx5$; 0.85), the
multiplicity lower bound $N\ge3$ from CP violation, the log-normal abundance law from the
central limit theorem, and phase-gradient gravity whose leading order is the Einstein
equations. The dynamical layer is the graph Laplacian $L_Q$, whose tight-binding identity
$H=tL_Q$ and reversible dynamics yield the Schrödinger equation
$i\partial_t\psi=L_Q\psi$ and a Hilbert space of eigenmodes. The non-abelian gauge factors
and the Koide relation are classified by a minimal three-category taxonomy
(DERIVED / REAL-UNDERIVED / DRAWN) with program consistency 0.95 and zero category
conflicts. The theory makes quantitative, falsifiable predictions — the zero-parameter
radial acceleration $g_\dagger=cH_0/(2\pi)$, a specific $w(z)$, a time-varying
$\Lambda(t)$, and the log-normal abundance form — and one such prediction (neutrino-Koide)
was already falsified, demonstrating the theory is falsifiable.

---

## Keywords

THE Q-MODEL; TQM; emergence; quantum foundations; causal set theory; graph Laplacian;
Schrödinger equation; gauge structure; radial acceleration relation; dark energy;
structure/content split; falsifiability.

---

## Zenodo metadata summary

| Field | Value |
|---|---|
| Title | THE Q-MODEL — From Q to Cosmology (Version 1.0, revised) |
| Type | publication / whitepaper |
| Author | Fabrice Wieser (Independent) |
| License | MIT |
| Version | 1.0.0 |
| Date | 2026-08-15 |
| Access | open |
| Keywords | see above |
| Related ID | https://github.com/MagusDraconis/TQM (isSupplementTo) |

Full metadata: `Zenodo_Metadata.json`.

---

## File manifest

| File | Purpose | Status |
|---|---|---|
| `TQM_v1_0.tex` | LaTeX source of the paper | **created** |
| `TQM_v1_0.pdf` | compiled PDF | **compiled** (5 pages) |
| `README.md` | package README | **created** |
| `CITATION.cff` | Citation File Format | **created** |
| `CHANGELOG.md` | version history | **created** |
| `Zenodo_Metadata.json` | Zenodo upload metadata | **created** |
| `TQM_v1_0_PublicationPackage.md` | this overview | **created** |

---

## PDF checklist

- [x] Compile `TQM_v1_0.tex`: `pdflatex TQM_v1_0.tex` (run twice for the table of contents).
- [x] Confirm the **caveat box** (READY_FOR_WHITEPAPER / NOT_READY_FOR_JOURNAL) appears on
      the title page.
- [x] Confirm the abstract, keywords, and all 14 numbered sections render.
- [x] Confirm the equations ($i\partial_t\psi=L_Q\psi$; $G_{\mu\nu}=8\pi G_{\rm eff}T_{\mu\nu}
      +O(\ell_P^2R^2)$) compile without errors.
- [x] Name the PDF `TQM_v1_0.pdf` and place it in this directory.
- [ ] Upload the bundle (`.tex`, `.pdf`, `Zenodo_Metadata.json`, `README.md`,
      `CITATION.cff`, `CHANGELOG.md`) to Zenodo with the metadata above.
- [ ] Verify the caveat and the `related_identifiers` link to the GitHub repository survive
      the Zenodo upload.

---

## Release bundle (final)

1. `TQM_v1_0.tex` — LaTeX paper (with title-page caveat).
2. `TQM_v1_0.pdf` — compiled white paper.
3. `Zenodo_Metadata.json` — metadata for the deposit.
4. `README.md` — package description.
5. `CITATION.cff` — machine-readable citation.
6. `CHANGELOG.md` — version history.

---

## Verdict

**READY_FOR_WHITEPAPER (deposit on Zenodo). NOT_READY_FOR_JOURNAL.**

The package accurately describes a research program with a tested dynamical core
($L_Q\to$ Schrödinger, verified by executable tests), a precise taxonomy, falsifiable
predictions, and explicitly-flagged open items. It stops short of the standard required
for a peer-reviewed derivation article: the Einstein recovery is imported (metric + BDG
action), $G$ is dimensional analysis, and no unique discriminating prediction is yet
supplied.
