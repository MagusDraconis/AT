# THE ACTUALIZATION THEORY — Publication Package

**Publication status: V2.0 RELEASED (2026-08-27) · Prediction Registry v1.0 RELEASED (companion).**
**Repository line: V2.2 (2026-09-13) — audit lines complete, NOT yet tagged, no V2.2 Zenodo deposit.**
The archived, citable artifact remains the V2.0 monograph under DOI https://doi.org/10.5281/zenodo.20681734.

This directory contains the Zenodo publication package for The Actualization Theory
(formerly THE Q-MODEL, TQM). It is organized into two versioned subdirectories plus the
shared metadata at this root.

## Contents

| Path | Purpose |
|---|---|
| `V1.0/` | Superseded v1.0 publication package (white paper + reference monograph, "four primitives" era) |
| `V2.0/` | Canonical v2.0 monograph chapters + compiled PDF (The Actualization Theory, primitives {Difference, η}) |
| `V2.0/ActualizationTheory_PredictionRegistry.tex/.pdf` | Companion Prediction Registry v1.0 (standalone Zenodo publication) |
| `Zenodo_Metadata.json` | Zenodo upload metadata (title, authors, keywords, license) — current release line (V2.2) |
| `Zenodo_Metadata_V1_0.json` | Zenodo upload metadata as deposited for V1.0 (archived) |
| `Zenodo_Abstract.md` | Zenodo abstract for the current release line (V2.2) |
| `Zenodo_Abstract_V2_0.md` | Zenodo abstract for V2.0 (archived) |
| `Zenodo_Abstract_V1_0.md` | Zenodo abstract for V1.0 (archived, "four primitives" era) |
| `RELEASE_NOTES_V2_2.md` | V2.2 release notes (current audit line) |
| `RELEASE_NOTES_V2_1.md` | V2.1 release notes |
| `RELEASE_NOTES_V2_0.md` | V2.0 release notes |
| `CITATION.cff` | Citation File Format metadata |
| `CHANGELOG.md` | Version history |
| `README.md` | This file |

## Zenodo

DOI: https://doi.org/10.5281/zenodo.20681734

The canonical monograph **The Actualization Theory: A Reconstruction of Physics from
Difference, Actualization and Spectrum** (V2.0, 95 pages) and its companion **Prediction
Registry** (v1.0, 24 pages) are archived together under this DOI.

## V1.0 (archived, superseded)

The v1.0 release (THE Q-MODEL) is a white paper + reference monograph based on the
pre-canonical "four primitives" framing (individuation Q, Random Actualization, the scale
triad (ℓ,τ,ℏ), and a nonlinearity parameter M²). It is **marked superseded** by the canonical
v2.0 monograph per MONO004. Its files remain archived under `V1.0/` for the historical
record.

## V2.0 (released)

The canonical v2.0 monograph — **The Actualization Theory: A Reconstruction of Physics from
Difference, Actualization and Spectrum** — derives all physics from the primitives
{Difference, η} through the hierarchy:

```
Difference → Actualization → Inevitable Spectrum → Physics
```

The build is publication-hardened (95 pages, 0 undefined references, 0 multiply-defined
labels, pdflatex ×3 clean): every major claim carries a documented derivational status
(theorem / necessity / correspondence / calibration / hosted / fit) from the
claim-classification registry, the D96 spectrum is reproducible from the stated graph
`C96(±1..±6)`, and all symbols are defined at first use. Chapters follow the MONO004
structure and are assembled from the canonical end-state (QG278–QG318, MONO004–MONO007).

### Companion: Prediction Registry v1.0

The companion **Prediction Registry** (`ActualizationTheory_PredictionRegistry.pdf`)
records all quantitative and qualitative predictions derived from the canonical theory —
41 entries (AT-P001…AT-P041) across the gauge, CKM/PMNS, neutrino, Majorana, g-2,
oblique, gravity, black-hole, cosmology, universality, and experimental-frontier sectors —
each with derivation source, dependency chain, numerical value, uncertainty, validation
status, and falsification criterion. Statuses: CONSISTENT 32, PENDING 8, BOUNDARY 1,
FALSIFIED 0. The three pre-registered frontier predictions (P1 106 GeV, P2 0νββ,
P3 sector ladder) are recorded with frozen values and explicit falsification criteria.

## V2.1 and V2.2 (research releases — no new Zenodo deposit)

| Version | Date | Content | Deposit |
|---|---|---|---|
| **V2.1** | 2026-08-30 | Boundary Program — the origin chain traced end to end and closed with exactly **five irreducible boundaries**; 13 boundary items reclassified | none |
| **V2.2** | 2026-09-13 | New Physics (NP_001–NP_175, **formally closed**) + the gravity/time audits G_021–G_036 and the new electromagnetism group E_001 | none |

Neither V2.1 nor V2.2 adds a publication artifact, so **the archived, citable monograph remains
V2.0** and the DOI is unchanged. V2.2's headline results are deliberately negative and are recorded
in full in `RELEASE_NOTES_V2_2.md`:

- AT's **derived** spatial structure (the counting measure) is **excluded by measurement** —
  γ = −1 gives zero light bending, refuted at 8.6957×10⁴ σ by Cassini.
- The spatial sector **cannot be derived** (G_030 no-go) and closes instead on the postulate
  `g_rr = 2 − ρ^(2/d)`, which reproduces γ = +1.
- The electromagnetic **dynamics is declared but never computed** (E_001): the currents, `F^a_μν`,
  `−¼F²` and the full Lagrangian exist only as **string-returning members**, and the sourced Maxwell
  equation is absent.
- Light bending is **resolved**: time dilation alone gives exactly **half** the deflection, excluded
  at 43,479 σ, so the spatial half is required.

## Source repository

https://github.com/MagusDraconis/AT

Build & test: `dotnet build -c Debug` then `dotnet test AT.Tests/AT.Tests.csproj -c Debug`
(.NET 10; FITS.Lib 5.0.381, MathNet.Numerics 5.0.0, SkiaSharp 3.119.0). Release builds are
supported. Note that the **full test suite currently hangs** in `D_ResonanceStructure` — run
targeted filters; see the Known Issues section of the repository `README.md`.

