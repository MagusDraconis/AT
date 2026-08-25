# TQM-QG Phase 314 — Organization Predictor Audit

**Status:** COMPLETE — **PARTIAL PREDICTION**
**Tests:** TQMQG3140, TQMQG3141, TQMQG3142 (all passed)
**Core class:** `TQM.Core/ResearchXH/OrganizationPredictorAudit.cs`
**Question:** can the lock VALUES (QG313: lock law universal, lock values domain-specific) predict ORGANIZATION STRENGTH?
**Method:** deterministic, no observables, no target values — an organization score computed from the lock structure ONLY, across 8 domains.

---

## 1. The Organization Score (from lock structure only)

For each domain, the four normalized lock identities (moment/span, compression/count, higher-moment, √moment/span) are scored by their **lock coherence**: how exactly each identity locks onto a *small-fraction* rational p/q (q ≤ 5, p ≤ 120). Coherence 1.0 = exactly on the rational; 0 = more than 1% away. The organization score is the mean coherence, in [0,1].

The small-numerator bound is the principled discriminator: the D96 locks are small fractions (10, 20, 12/5, 25/3, 95/3). A large random ratio is trivially near *some* rational (rational density), but only an organized spectrum lands exactly on a ratio with a **small numerator**.

| Domain | Class | M/S | C/C | H-M | √M/S | Score | Locks |
|---|---|---|---|---|---|---|---|
| random | unorganized | 349.5 | 325.7 | 370.1 | 21.4 | 0.216 | 1 |
| uniform | unorganized | — | 1.00 | 1.00 | — | 0.000 | 0 |
| language | organized (Zipf) | 45.0 | 180.6 | 369.8 | 5.70 | 0.331 | 1 |
| music | organized (octave) | 19.7 | 22.1 | 26.3 | 4.67 | 0.873 | 4 |
| DNA | organized (codon) | 36.0 | 5.67 | 6.35 | 16.3 | 0.881 | 4 |
| software | organized (power law) | 9.19 | 523.2 | 877.7 | 0.84 | 0.217 | 1 |
| finance | organized (heavy tail) | 1.62 | 334.5 | 469.9 | 0.18 | 0.000 | 0 |
| networks | organized (modular) | 66.3 | 3.97 | 4.02 | 33.5 | 0.604 | 3 |

---

## 2. Class Separation Holds

- **Organized mean 0.484 vs unorganized mean 0.108** — the lock-coherence score separates the organized class from the unorganized class.
- **5 of 6 organized systems lock above BOTH unorganized systems** (music, DNA, networks, language, software — all above random's 0.216 and uniform's 0.000).
- Stable locks: organized mean 2.2 vs unorganized 0.5.

## 3. Strength Ranking Fails

- **finance (heavy-tailed, QG310's STRONGEST organization, score 0.796) locks at 0.000** — its lock identities [M/S=1.618, C/C≈334, H-M≈470] have LARGE numerators that never lock onto a small fraction.
- **software (0.779 in QG310) locks at 0.217** — C/C≈523, H-M≈878, no small-fraction locks.
- The QG310 operator-strength ranking (heavy-tailed ≥ Zipf ≥ unorganized) is **NOT reproduced** by lock coherence.

---

## 4. Conclusion

### **PARTIAL PREDICTION** (prediction score 4/5)

**The lock VALUES predict the organized/unorganized CLASS, not the organization STRENGTH within it.**

- The lock-coherence organization score separates organized systems (which lock onto small-fraction rationals coherently) from unorganized systems (which do not) — organized mean 0.484 vs unorganized 0.108, 5/6 organized above both unorganized;
- The score does **NOT** rank organization strength: heavy-tailed systems (finance, software) have large characteristic numerators (C/C ≈ 334, 523) that never lock onto a small fraction, so they score **below** the Zipf systems despite being QG310's stronger organizations;
- The lock values are domain fingerprints (QG313); they separate the class, but the discriminating content for *strength* remains the operator basis (CROWDING/COMPRESSION/BEAT/LOCKING, QG310).

**The reduction chain (QG260→314):**
```
Resonance Layer → … → Null Spectrum Audit → Organization Metric Prediction
→ Operator Specificity Audit → Adversarial Spectrum Audit → Lock Universality Audit
→ ORGANIZATION PREDICTOR AUDIT
(the lock values predict the organized/unorganized class, not the strength within it)
```

**Frontier status:** the locks separate class but not strength — the operator basis remains the strength metric (QG310). Remaining frontier unchanged: temporal evidence, SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
