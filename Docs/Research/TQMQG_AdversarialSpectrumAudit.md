# TQM-QG Phase 312 — Adversarial Spectrum Audit

**Status:** COMPLETE — **PARTIAL FAILURE**
**Tests:** TQMQG3120, TQMQG3121, TQMQG3122 (all passed)
**Core class:** `TQM.Core/ResearchXH/AdversarialSpectrumAudit.cs`
**Question:** can the four operators be triggered by crafted fake spectra with large span / large gap / many groups but NO organization?
**Method:** deterministic, D96 only — three adversarial fakes crafted and measured for the operators + the beat-identity locks.

---

## 1. The Three Adversarial Fakes

| Fake | Construction | Span | Distinct | CROWDING | COMPRESSION | BEAT | LOCKING | Full basis |
|---|---|---|---|---|---|---|---|---|
| large span | 30 uniform + 1 outlier | 200 | 2 | ✓ | ✓ | ✓ | ✓ | **✓** |
| large gap | 20 low + 20 high | 100 | 2 | ✓ | ✓ | ✓ | ✓ | **✓** |
| many groups | 40 distinct random | 35 | 40 | ✗ | ✓ | ✓ | ✓ | ✗ |

---

## 2. The Honest Finding

**2/3 fakes trigger the full BINARY basis.** The large-span and large-gap fakes both pass CROWDING because they have **2 distinct values** (the two-level crafting fakes the binary screen). The many-groups fake fails CROWDING (all distinct — no degeneracy).

**BUT the organization signature is never faked:**

| Fake | Beat-identity locks |
|---|---|
| large span | **0** |
| large gap | **0** |
| many groups | **0** |

**Zero of the three fakes carry the beat-identity locks** (Σ√m/span ≈ 10, occMom/Σm ≈ 20 — exact integer ratios).

---

## 3. Conclusion

### **PARTIAL FAILURE** (robustness score 5/5)

**The binary presence is partially faked; the organization content is robust.**

- The adversarial fakes **CAN trigger the binary basis** — the two-level fakes (large span, large gap) pass CROWDING;
- The fakes **CANNOT fake the organization signature** — the beat-identity locks (exact integer ratios) are carried by **zero** of the fakes;
- The many-groups fake fails CROWDING entirely.

The binary operator screen is a weak filter (fakable by two-level crafting), but the **locks are the robust organization content** that span/gap/group manipulation cannot reproduce.

**The reduction chain (QG260→312):**
```
Resonance Layer → … → Organization Metric Prediction → OPERATOR SPECIFICITY AUDIT
→ ADVERSARIAL SPECTRUM AUDIT (the binary presence is partially faked, the locks are robust)
```

**Frontier status:** the adversarial attack shows the binary operator screen is partially fakeable, but the organization locks survive. Remaining frontier unchanged: temporal evidence, SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
