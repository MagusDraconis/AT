# THE ACTUALIZATION THEORY — Publication Package

**Publication status: V1.0 READY_FOR_WHITEPAPER (archived, superseded) · V2.0 in preparation.**

This directory contains the Zenodo publication package for The Actualization Theory
(formerly THE Q-MODEL, TQM). It is organized into two versioned subdirectories plus the
shared metadata at this root.

## Contents

| Path | Purpose |
|---|---|
| `V1.0/` | Superseded v1.0 publication package (white paper + reference monograph, "four primitives" era) |
| `V2.0/` | Canonical v2.0 monograph chapters (The Actualization Theory, primitives {Difference, η}) |
| `Zenodo_Metadata.json` | Zenodo upload metadata (title, authors, keywords, license) |
| `CITATION.cff` | Citation File Format metadata |
| `CHANGELOG.md` | Version history |
| `README.md` | This file |

## V1.0 (archived, superseded)

The v1.0 release (THE Q-MODEL) is a white paper + reference monograph based on the
pre-canonical "four primitives" framing (individuation Q, Random Actualization, the scale
triad (ℓ,τ,ℏ), and a nonlinearity parameter M²). It is **marked superseded** by the canonical
v2.0 monograph per MONO004. Its files remain archived under `V1.0/` for the historical
record.

## V2.0 (in preparation)

The canonical v2.0 monograph — **The Actualization Theory: A Reconstruction of Physics from
Difference, Actualization and Spectrum** — derives all physics from the primitives
{Difference, η} through the hierarchy

```
Difference → Actualization → Inevitable Spectrum → Physics
```

Chapters are assembled from the canonical end-state (QG278-QG318, MONO004-MONO007) and
follow the MONO004 17-chapter structure. Each chapter is publication-grade LaTeX with formal
theorem structure, verified to compile cleanly. See `V2.0/` for the current chapter list.

## Source repository

https://github.com/MagusDraconis/AT

Build & test: `dotnet build TQM.Core/TQM.Core.csproj` then
`dotnet test TQM.Tests/TQM.Tests.csproj` (.NET 10, MathNet.Numerics 5.0).

