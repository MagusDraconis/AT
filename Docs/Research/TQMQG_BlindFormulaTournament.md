# TQM-QG Phase 258 — Blind Formula Tournament

**Status:** COMPLETE — **WEAK** (0/7 blind success)
**Tests:** TQMQG2580, TQMQG2581, TQMQG2582 (all passed)
**Core class:** `TQM.Core/ResearchXH/BlindFormulaTournament.cs`
**Method:** fully blind — select top formula from D96 alone, lock, then reveal target
**Forbidden:** observable values, target values (during selection)

---

## 1. The Tournament

For each observable:
1. **Input** — D96 quantities only (Σm, #d, #g, span, λ₂, Σ√m, occ, occMom);
2. **Generate** — all expressions up to complexity 6 (76,750 candidates),
   restricted to ratio-form (all seven observables are dimensionless ratios);
3. **Apply** — QG254 octave preservation → QG255 moment-closure MDL
   (min complexity → Noether → moment closure);
4. **Select** — the top formula only;
5. **Lock** — the selection is frozen;
6. **Reveal** — only now is the target consulted;
7. **Score** — success iff the locked formula matches within 1%.

---

## 2. The Result

**Blind top formula (locked): λ₂/occMom = 0.000203**

| Observable | Target | Locked selection | Dev | Result |
|-----------|--------|------------------|-----|--------|
| 1−n_s | 0.03503 | λ₂/occMom | 99.4% | MISS |
| r₂₁ | 2.4368 | λ₂/occMom | 99.99% | MISS |
| r₃₁ | 3.6965 | λ₂/occMom | 99.99% | MISS |
| m₂/m₃ | 0.1766 | λ₂/occMom | 99.9% | MISS |
| y_t/y_b | 41.26 | λ₂/occMom | 100% | MISS |
| m_μ/me | 207.03 | λ₂/occMom | 100% | MISS |
| m_τ/m_μ | 16.842 | λ₂/occMom | 100% | MISS |

**Success rate: 0/7.**

---

## 3. The Finding

The target-free rule chain is **degenerate**: it selects the **same** formula
(the globally minimal-complexity octave-preserving ratio) for *every*
observable, because it has no reference to which observable it is selecting
for. That locked formula matches **none** of the seven revealed targets.

The QG254/QG255 rules therefore have **no blind predictive power**.

---

## 4. Conclusion

### **WEAK** — 0/7 blind success

The selection rules only "work" when the candidate pool is **pre-restricted by
the target** (as in QG253, which found matches within 0.5% of each target) —
that is, when the target was already used to build the pool. In a genuine
blind setting, no target is available to restrict the pool, and the rules
collapse to a single global formula that predicts nothing.

**Honest implication:** the QG253–255 "selection principle" program does not
have predictive power. It is a *post-hoc descriptive* narrowing of pools that
were built using the targets they claim to predict. This is the decisive
confirmation of QG256 (HIGH selection-principle risk) and QG257 (NO UNIVERSAL
PRINCIPLE): a formula cannot be selected for a specific observable without any
reference to what that observable is.
