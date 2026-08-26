# AT-QG Phase 257 — Principle Competition Audit

**Status:** COMPLETE — **NO UNIVERSAL PRINCIPLE**
**Tests:** ATQG2570, ATQG2571, ATQG2572 (all passed)
**Core class:** `AT.Core/ResearchXH/PrincipleCompetitionAudit.cs`
**Use:** QG253 (search), QG254 (octave preservation), QG255 (moment-closure MDL)
**Method:** compare the seven principles on selection quality only (no targets, no accuracy)

---

## 1. The Seven Principles Measured

| Principle | Power (unique/7) | Survivors | Consistent | Exceptions |
|-----------|------------------|-----------|------------|------------|
| Octave preservation (QG254) | 1/7 | 2.0 | ✓ | 0 |
| Moment closure (QG255) | 2/7 | 1.5 | ✓ | 0 |
| MDL / min complexity (QG253) | 1/7 | 3.0 | ✓ | 0 |
| Maximum symmetry (D96) | 1/7 | 2.5 | ✓ | 0 |
| Maximum invariance (occ₀↔occ₁) | 0/7 | 7.0 | ✓ | 0 |
| **Noether consistency (QG255)** | **3/7** | **1.3** | **✗** | **1** |
| Full spectrum usage | 1/7 | 2.5 | ✓ | 0 |

---

## 2. Key Findings

**Maximum invariance (occ₀↔occ₁) is the weakest** — occ₀ = occ₁ = 4, so
every formula is trivially permutation-invariant: zero discriminating power.

**Noether consistency is the strongest by power (3/7) but inconsistent** —
it resolves the m_μ/me tie by rejecting 5/4·Σ√m/λ₂, yet the **published QG238
ℓ₁ = Σm·ln(span)·(5/4) uses 5/4**, requiring 1 ad-hoc exception.

**Octave preservation is the strongest consistent filter** — removes all 5
non-native alternatives with 0 exceptions, but leaves 3 octave-preserving ties.

**No single principle uniquely selects all 7 observables.**

---

## 3. The Determination

### **NO UNIVERSAL PRINCIPLE**

- No candidate principle is both **universal** (all 7 observables) and
  **exception-free**;
- the QG255 "UNIQUE SELECTION PRINCIPLE" came only from a **sequence**
  (octave preservation → MDL → Noether → moment closure), and the Noether
  step carries the 5/4 inconsistency;
- the best single filter (octave preservation) achieves ~1–3/7 uniqueness.

**Ranking:** Noether consistency > moment closure > octave preservation
(by raw power), but the only exception-free consistent option is the
sequence — and even that contains one ad-hoc carve-out. A single universal
rule does not exist among the seven candidates.

**Honest status of the selection-principle program:** the QG253–255
principles are a useful *heuristic narrowing* (octave preservation +
moment closure + MDL genuinely prune the candidate space), but they are
not a *universal derivation-choice rule*: no one principle is both
complete and exception-free.
