# AT-QG Release Check — Monograph V2.0

**Date:** 2026-08-27
**Release state commit:** `ec057881`
**Generated PDF:** `Docs/Publication/V2.0/main.pdf`

---

## 1. Verification checklist

| # | Check | Result |
|---|---|---|
| 1 | All registry-consistency patches present | **PASS** — `claim_status_wording`, `final_registry_patch`, `residual_2line`, `k6_conditional_note` applied and committed in `ec057881` |
| 2 | No pending wording patches remain | **PASS** — status-aware wording present for all 16 classified claims (monograph Ch10/11/12/14, FrontMatter, Conclusion; AT.App Atlas, CoverageAudit, BlindValidation, ValidationStandardModel); no residual "derived"-for-fit/correspondence/hosted/calibration strings |
| 3 | No TODO markers | **PASS** — no `TODO` / `FIXME` / `XXX` / `placeholder` in `Docs/Publication/V2.0/*.tex` |
| 4 | No unresolved references | **PASS** — pdflatex log: zero undefined references |
| 5 | No unresolved citations | **PASS** — pdflatex log: zero undefined citations |

## 2. Build

Sequence: `pdflatex` × 3 (`main.tex` uses manual `thebibliography` — no bibtex/biber required).

| Metric | Value |
|---|---|
| Page count | **95** |
| Errors | **0** |
| Undefined references / citations | **0** |
| Warnings | 58 — all pre-existing cosmetic: 52× fancyhdr "headheight too small (12.0pt)", 6× hyperref "Token not allowed in a PDF string (Unicode)"; none related to content |
| AT.App build | 0 errors (14 pre-existing MudBlazor/nullable warnings) |
| PDF path | `Docs/Publication/V2.0/main.pdf` (885,063 bytes) |

## 3. Remaining open issues (documented, non-blocking)

| Item | Status |
|---|---|
| **Observable-selection non-uniqueness** | OPEN — some observables selected from candidate families; 1/α_em HIGH target-selection dependence (fit), CKM/PMNS medium (correspondence) |
| **Sector-label non-uniqueness** | OPEN — sector access-role labels are supported assignments, not globally unique |
| **Gauge correspondence vs hosted structure** | OPEN — 1+3+8 is a dimensional correspondence (degree 12 = 2K conditional on selected K=6); gauge groups/Lie algebras hosted |
| **ℓ₁ fitted normalization (5/4)** | OPEN — 5/4 is a fit (QG297), removable (QG289), no mechanism; ℓ₁ = 220.48; peak ratios are pure spectral correspondences |

## 4. Release readiness

**READY**

The monograph is registry-consistent, builds cleanly (95 pages, 0 errors, 0 undefined
refs/citations), and all wording patches are consolidated in the repository. The four
remaining open items are honest, documented disclosures of the theory's current scope —
they are classification/selection limitations already reflected in the registry and the
monograph's claim-status wording, not release blockers. No physics was modified in this
pass (wording-only).
