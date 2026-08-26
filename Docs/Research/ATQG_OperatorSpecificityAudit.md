# AT-QG Phase 311 — Operator Specificity Audit

**Status:** COMPLETE — **MIXED**
**Tests:** ATQG3110, ATQG3111, ATQG3112 (all passed)
**Core class:** `AT.Core/ResearchXH/OperatorSpecificityAudit.cs`
**Question:** do the four operators measure organization (the arrangement) or merely inequality (the distribution)?
**Method:** deterministic, D96 only — pairs constructed that hold one quantity fixed while varying the other.

---

## 1. Pair (a) — Same inequality, different arrangement (frequency read)

| | power law | shuffled power law |
|---|---|---|
| frequency multiset | identical | identical |
| arrangement | 1/k rank order | reordered |
| operators | **indistinguishable** | **indistinguishable** |

**FINDING:** the frequency reading is **ORDER-BLIND** — a power law and its shuffled multiset have identical operators. As frequency statistics, the operators measure **INEQUALITY**, not arrangement.

---

## 2. Pair (b) — Same degree sequence, different arrangement (graph read)

| | modular graph | degree-preserving rewiring |
|---|---|---|
| degree sequence | identical | identical |
| arrangement | two clusters + bridge | clustering destroyed |
| spectral span | **≈ 15.5** | **≈ 6.3** |

**FINDING:** the graph-spectral reading **SEES the arrangement quantitatively** — the modular graph spans ≈15.5 while its degree-preserving rewiring spans ≈6.3 (a **2.4× difference with the SAME degrees**). As graph spectra, the operators measure **ORGANIZATION**.

---

## 3. Pair (c) — Same organization, different inequality

| | exponent 1 | exponent 2 |
|---|---|---|
| form | rank-ordered power law | rank-ordered power law |
| inequality | weak | strong |
| span | smaller | larger |

**FINDING:** within one organizational form, the operators track inequality **monotonically**.

---

## 4. Conclusion

### **MIXED** (specificity score 5/5)

**The operators measure BOTH, depending on the READ:**
- as **frequency statistics** — **INEQUALITY-specific** (a power law and its shuffle are indistinguishable);
- as **graph spectra** — **ORGANIZATION-specific** (a modular graph differs from its degree-preserving rewiring);
- within one form — track inequality **monotonically**.

**The operator basis is a spectral read of the underlying structure**: when the structure is a distribution it sees inequality; when it is an arrangement it sees organization.

**The reduction chain (QG260→311):**
```
Resonance Layer → … → Operator Necessity → ALIEN DOMAIN AUDIT → RED TEAM AUDIT
→ ANTI-ORGANIZATION PREDICTION → ANTI-HIERARCHY AUDIT → NULL SPECTRUM AUDIT
→ ORGANIZATION METRIC PREDICTION → OPERATOR SPECIFICITY AUDIT
(the operators measure both organization and inequality, depending on the read)
```

**Frontier status:** the operator basis is clarified as a spectral read of structure (organization when the structure is an arrangement, inequality when it is a distribution). Remaining frontier unchanged: temporal evidence, SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
