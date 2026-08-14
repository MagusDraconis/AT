# THE Q-MODEL — Publication Package (v1.0, revised)

**Publication status: READY_FOR_WHITEPAPER — NOT_READY_FOR_JOURNAL.**

This directory contains the Zenodo publication package for THE Q-MODEL (TQM), version 1.0
(revised). It is released as a **white paper** (research-program description), not a
peer-reviewed journal article.

## Contents

| File | Purpose |
|---|---|
| `TQM_v1_0.tex` | LaTeX source of the paper (build with `pdflatex TQM_v1_0.tex`) |
| `TQM_v1_0.pdf` | Compiled PDF (see checklist in `TQM_v1_0_PublicationPackage.md`) |
| `Zenodo_Metadata.json` | Zenodo upload metadata (title, authors, keywords, license) |
| `CITATION.cff` | Citation File Format metadata |
| `CHANGELOG.md` | Version history |
| `TQM_v1_0_PublicationPackage.md` | Package overview, abstract, keywords, caveat, checklist |
| `README.md` | This file |

## Publication caveat

**READY_FOR_WHITEPAPER — NOT_READY_FOR_JOURNAL.**

The central derivation claim (Einstein recovery from Q-events) remains *logical, not
mathematical*: the metric and the BDG action are imported (proven but not TQM-derived),
$G=\ell^2c^3/\hbar$ is dimensional analysis, and no unique sharp prediction yet
discriminates TQM from SM + $\Lambda$CDM. See
`../Audits/PublicationReadiness_Final.md` for the full re-evaluation of every Round-2
fatal review issue.

## Citation

See `CITATION.cff`, or cite as:

> Fabrice Wieser, *THE Q-MODEL — From Q to Cosmology: A Theory of Structure, Complexity
> and Random Actualization*, Version 1.0 (revised), 2026. White paper (Zenodo).

## Source repository

https://github.com/MagusDraconis/TQM

Build & test: `dotnet build TQM.Core/TQM.Core.csproj` then
`dotnet test TQM.Tests/TQM.Tests.csproj` (.NET 10, MathNet.Numerics 5.0).
