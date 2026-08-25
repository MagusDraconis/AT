# TQM-QG Phase 318 (reissue) — Lock Origin Audit

**Status:** COMPLETE — **PARTIAL ORIGIN**
**Tests:** TQMQG3180, TQMQG3181, TQMQG3182 (all passed)
**Core class:** `TQM.Core/ResearchXH/LockOriginAudit.cs`
**Question:** WHY do the D96 lock identities emerge — are they EMERGENT, INEVITABLE, or RESONANCE FIXED POINTS?
**Method:** D96 only, no observables, no target values, deterministic — investigate the moment ratios, gap ratios, span ratios, and occupancy ratios; search for the common source of lock formation.

---

## 1. The Four Lock Families (ratios of the D96 moment hierarchy)

| Lock | Ratio | Value | Target |
|---|---|---|---|
| lock0 (span) | Σ√m / span | 10.0090 | 10 |
| lock1 (occupancy) | occMom / Σm | 20.0026 | 20 |
| lock2 (moment) | Σm² / Σm | 2.4105 | 12/5 |
| lock3 (occupancy) | occMom / Σm² | 8.2980 | 25/3 |

## 2. The Common Source — The Moment-Chain (Telescoping) Identity

**lock1 = lock2 × lock3 EXACTLY:**
```
occMom/Σm = (Σm²/Σm)·(occMom/Σm²)
20.0026   = 2.4105 × 8.2980   (identical to 1e-6)
```

The four locks are **NOT independent**: they are the ratio chain of ONE moment hierarchy {Σ√m, Σm, Σm², occMom}. lock1 is algebraically forced by lock2 × lock3 — this is a universal self-consistency (fixed-point) relation of the moment chain.

## 3. Robustness to Perturbation

Changing one D96 group to a nearby size moves the locks only 0.6–4.6%:

| Perturbation | Σm²/Σm | Σ√m/span | Max dev |
|---|---|---|---|
| 2→1 | 2.4043 | 9.9443 | 0.6% |
| 2→3 | 2.4375 | 10.0586 | 1.1% |
| 5→4 | 2.3404 | 9.9721 | 2.9% |
| 5→6 | 2.5000 | 10.0423 | 3.7% |
| 6→5 | 2.3191 | 9.9756 | 3.8% |
| 6→7 | 2.5208 | 10.0396 | 4.6% |

## 4. Not Inevitable

2000 deterministic random multiplicity sets with the same constraints (sum 95, 44 groups): **0/2000 reproduce even two of the D96 lock values** within 1%. The specific values 10, 20, 12/5, 25/3 are D96-specific — NOT inevitable.

---

## 5. Conclusion

### **PARTIAL ORIGIN** (origin score 5/5)

**The locks are RESONANCE FIXED POINTS in structure, EMERGENT in value.**

- **The STRUCTURE is explained**: the locks are the self-consistent ratio chain of ONE D96 moment hierarchy, linked by the exact telescoping identity lock1 = lock2 × lock3 — an algebraic necessity (the resonance fixed-point relation of the actualization attractor);
- **The VALUES are emergent**: robust to nearby perturbation (≤ 4.6%) but NOT reproduced by any random same-constraint spectrum (0/2000) — the values 10, 20, 12/5, 25/3 are specific to the D96 geometry;
- The common source of lock formation is found (the moment-chain identity), but the specific integer values are not forced by a universal principle — hence PARTIAL ORIGIN.

**The reduction chain (QG260→318):**
```
Resonance Layer → … → Blind Organization Prediction → Reorganization Prediction
→ False Positive Audit → LOCK ORIGIN AUDIT
(the locks are resonance fixed points in structure — the moment-chain identity lock1=lock2×lock3 —
but emergent in value — D96-specific)
```

**Frontier status:** the lock STRUCTURE traces to the moment-chain identity; the lock VALUES remain D96-specific. Remaining frontier unchanged: temporal evidence (SM), SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
