# MONO116A — Global Naming Audit

**Date:** 2026-08-26
**Scope:** Docs/Publication/V2.0/ (the complete v2.0 monograph)
**Goal:** Make "The Actualization Theory (AT)" the sole active theory name.

---

## Summary

- **Total occurrences:** 185
- **Keep:** 180 (document identifiers, repository names, historical reference)
- **Replace:** 5 (active uses of "The Quantum Model" / "AT" as current theory name)
- **Result:** All 5 replaced; single active identity "The Actualization Theory (AT)".

---

## 1. Occurrence List

### REPLACE (5)

| # | File | Line | Original text |
|---|------|------|---------------|
| 1 | Chapter01_Difference.tex | 31 | `The Quantum Model (AT) is a deterministic theory of structure, complexity, and random actualization.` |
| 2 | Chapter01_Difference.tex | 71 | `The minimal set of irreducible primitives of AT is $\{\text{Difference},\eta\}$: exactly two items.` |
| 3 | Chapter01_Difference.tex | 121 | `Difference is the fundamental boundary of AT.` |
| 4 | Appendices.tex | 13 | `The universality research program of The Quantum Model is treated as future work, outside the core physics derivation of this monograph.` |
| 5 | FrontMatter.tex | 18 | `{\large The Quantum Model --- v2.0 (canonical)}` (title page edition line) |

### KEEP (180)

| Category | Examples | Count | Reason |
|----------|----------|-------|--------|
| Document identifiers | `AT-QG Phase 116`, `AT-MONO007`, `AT-VALID001` | ~165 | Document identifiers — MUST keep per task |
| Repository name / URL | `https://github.com/MagusDraconis/AT` | 2 | Repository name — MUST keep |
| Comment headers | `% AT-MONO100 — Chapter 1` in all chapter files | ~13 | Document identifiers in source comments |
| Historical transition | `FrontMatter.tex:113` "canonical v2.0 record of The Q-Model (AT), now presented under the name The Actualization Theory" | 1 | Explicit historical reference — MUST keep for traceability |
| File references in comments | `ATQG_CanonicalMonograph.md`, `ATQG_Mono007RefereeReadiness.md`, `ATVALID_UntestedItemsAudit.md` | ~4 | Document identifiers |

---

## 2. Classification

### Keep
- All `AT-QG Phase NNN` bibitem entries across chapters 1–14 (~120)
- All `AT-MONO###` bibitem and header-comment references (~25)
- `AT-VALID001` bibitems (Chapters 13–14)
- Repository URL `github.com/MagusDraconis/AT` (main.tex:60, FrontMatter.tex:17)
- FrontMatter.tex:113 — the one historical transition sentence
- File identifiers in comments (`ATQG_CanonicalMonograph.md`, etc.)

### Replace
- The 5 active uses listed in Section 1 — all were "The Quantum Model" as the present-day theory name, or "AT" as its acronym in active prose.

---

## 3. Exact Replacement Text

| # | Original | Replacement |
|---|----------|-------------|
| 1 | `The Quantum Model (AT) is a deterministic theory of structure, complexity, and random actualization.` | `The Actualization Theory (AT) is a deterministic theory of structure, complexity, and random actualization.` |
| 2 | `The minimal set of irreducible primitives of AT is $\{\text{Difference},\eta\}$: exactly two items.` | `The minimal set of irreducible primitives of AT is $\{\text{Difference},\eta\}$: exactly two items.` |
| 3 | `Difference is the fundamental boundary of AT.` | `Difference is the fundamental boundary of AT.` |
| 4 | `The universality research program of The Quantum Model is treated as future work, outside` | `The universality research program of The Actualization Theory is treated as future work, outside` |
| 5 | `{\large The Quantum Model --- v2.0 (canonical)}` | `{\large The Actualization Theory (AT) --- v2.0 (canonical)}` |

*Note:* after replacement 1, the acronym `AT` is introduced at first mention (Chapter 1, §Introduction) and used consistently in replacements 2–3, matching standard abbreviation conventions. Replacement 5 keeps the edition qualifier "(AT) --- v2.0 (canonical)" for title-page clarity.

---

## 4. Verification

- `grep "Quantum Model"` after changes → 0 results (correct historical name is **The Q-Model**).
- `grep "of AT"` → 0 results.
- Compilation verified after changes (see build log).
- All `AT-QG`, `AT-MONO`, `AT-VALID` identifiers untouched.

## 5. Result

The monograph now presents a **single active identity: "The Actualization Theory (AT)"**, while preserving full historical traceability through document identifiers, the repository URL, and the one explicit transition sentence in the preface.
