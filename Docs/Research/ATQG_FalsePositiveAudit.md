# AT-QG Phase 319 — False Positive Audit

**Status:** COMPLETE — **WEAK**
**Tests:** ATQG3190, ATQG3191, ATQG3192 (all passed)
**Core class:** `AT.Core/ResearchXH/FalsePositiveAudit.cs`
**Question:** can the QG317 lock rule [coherence ≥ 0.10 → organized] be FAKED [locks present, organization absent] or MISSED [organization present, locks absent]?
**Method:** deterministic, no observables, no target values — 1000 synthetic systems attempt BOTH failure modes; the honest false-positive and false-negative rates are measured.

---

## 1. The Generation (1000 adversarial systems)

- **Group A — 500 lock-fake attempts**: tiny few-bin spectra (2–5 bins) engineered so a moment ratio lands EXACTLY on a small fraction (lock coherence 1.0) while the span stays small (low maturity);
- **Group B — 500 org-miss attempts**: power-law spectra with large span and degeneracy (high maturity) whose moment ratios are NOT small fractions (finance-like: large numerators, C/C ≈ 334).

## 2. The Contingency Table

| | Org present | Org absent |
|---|---|---|
| **Lock present** | TP = 120 | FP = 626 |
| **Lock absent** | FN = 234 | TN = 20 |

- **False positive rate = 96.9%** — among truly unorganized systems, 96.9% are falsely flagged as organized;
- **False negative rate = 66.1%** — among truly organized systems, 66.1% are missed;
- Precision 16.1%, recall 33.9%.

## 3. Both Failure Modes Succeed

- **Group A (lock-fake):** 500/500 lock present, 1/500 org present — the lock identity is TRIVIALLY FAKED by a tiny engineered rational-ratio spectrum;
- **Group B (org-miss):** 353/500 org present, but 66% of the organized systems carry no locks — the finance-like large-numerator organization is MISSED.

---

## 4. Conclusion

### **WEAK** (robustness score 4/5)

**The QG317 lock rule is WEAK as a standalone detector: locks can be faked and real organizations can be missed.**

- The lock identity is **fake-able**: tiny rational-ratio spectra (2–5 bins engineered onto small fractions) produce locks WITHOUT any organization — 96.9% false positive rate;
- The lock identity is **miss-able**: finance-like large-numerator power laws are genuinely organized (span, degeneracy) but carry no small-fraction locks — 66.1% false negative rate;
- **QG317's 8/8 held for the SPECIFIC evolving power-law cohort, not as a universal detector.** The lock rule is a cohort-scoped predictor, not a robust standalone organization detector.

This is an honest red-team result: it scopes the QG317/QG318 predictions to the evolving-law cohort and shows that as a general classifier the lock coherence rule is weak. The robust content remains the OPERATOR basis on organized systems (QG300-312); the lock RULE is cohort-specific.

**The reduction chain (QG260→319):**
```
Resonance Layer → … → Blind Organization Prediction → Reorganization Prediction
→ FALSE POSITIVE AUDIT
(the QG317 lock rule is WEAK as a standalone detector: locks can be faked [96.9% FP] and missed [66.1% FN])
```

**Frontier status:** the lock rule is cohort-scoped, not universal. Remaining frontier unchanged: temporal evidence (SM), SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
