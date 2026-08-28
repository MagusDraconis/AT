# THE ACTUALIZATION THEORY — Publication Package

**Publication status: V2.0 RELEASED (2026-08-27) · Prediction Registry v1.0 RELEASED (companion).**

This directory contains the Zenodo publication package for The Actualization Theory
(formerly THE Q-MODEL, TQM). It is organized into two versioned subdirectories plus the
shared metadata at this root.

## Contents

| Path | Purpose |
|---|---|
| `V1.0/` | Superseded v1.0 publication package (white paper + reference monograph, "four primitives" era) |
| `V2.0/` | Canonical v2.0 monograph chapters + compiled PDF (The Actualization Theory, primitives {Difference, η}) |
| `V2.0/ActualizationTheory_PredictionRegistry.tex/.pdf` | Companion Prediction Registry v1.0 (standalone Zenodo publication) |
| `Zenodo_Metadata.json` | Zenodo upload metadata (title, authors, keywords, license) |
| `Zenodo_Abstract_V2_0.md` | Zenodo abstract for V2.0 |
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

## Source repository

https://github.com/MagusDraconis/AT

Build & test: `dotnet build AT.Core/AT.Core.csproj` then
`dotnet test AT.Tests/AT.Tests.csproj` (.NET 10, MathNet.Numerics 5.0).

