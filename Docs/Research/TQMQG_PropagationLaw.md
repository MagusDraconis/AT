# TQM-QG Phase 28 — Derive the Propagation Law

**Program:** TQM-QG (Unification)
**Phase:** 28 — which light-propagation rule follows from actualization dynamics?
**Status:** COMPLETED — 3/3 xUnit tests pass (87/87 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

QG27 showed null geodesics give NO lensing while the TRM kernel gives GR-like lensing. Here we determine which rule
is *native* to actualization dynamics. Classify: DERIVED / PREFERRED / IMPORTED.

---

## 2. The decisive fact: causal order fixes the conformal class, not the conformal factor

Actualization produces, in order:

Q-events → causal order → **conformal class** (the light cone) → conformal factor ρ → metric g = ρ^(2/d)η.

The causal order determines the **conformal class** — the null directions (the light cone). The counting measure ρ
then supplies only the **conformal factor** ρ^(2/d), a conformal rescaling that **leaves the light cone invariant**.
Consequently light propagates along the causal-order light cone: **null geodesics, n = 1, independent of ρ**.

---

## 3. Results

### (a) Effective index (TQMQG280)
- Null geodesics (full conformal metric): n = √(g_ii/(−g_00)) = √(ρ^(2/d)/ρ^(2/d)) = **1** → no refraction.
- TRM effective index (temporal-only): n = ρ^(1/d) = e^Φ → refracts light.

### (b) Mechanism census (TQMQG281)

| mechanism | native? | index |
|---|---|---|
| event-to-event | native | n = 1 |
| branching-path | native | n = 1 |
| correlation-kernel | native | n = 1 |
| null-geodesic-limit | native | n = 1 |
| effective-refractive-index | **imported** | n = e^Φ |

Branching and correlations give ρ (the conformal factor), which cannot refract light; only the temporal-only
refractive index n = e^Φ does, and it is not derivable from the actualization primitives.

### (c) Classification (TQMQG282)
- **NULL GEODESICS: DERIVED** (the native propagation law).
- **TRM EFFECTIVE MEDIUM: IMPORTED** (the temporal-only refractive index, not in TQM's primitives).

---

## 4. Conclusion

Null geodesics are the **native (DERIVED)** propagation law of TQM: the causal order fixes the light cone, and the
conformal factor ρ cannot refract light. TRM's lensing kernel n = e^Φ is **IMPORTED** — it is precisely the
non-conformal (ψ ≠ 0) sector in disguise (for the ψ-perturbed metric, n = e^(−ψd/(d−1))), i.e. the very new
primitive identified in QG23/QG24.

This closes the propagation-law question decisively: **no refractive medium emerges from actualization**; TQM's
native optics are conformally invariant null geodesics, and lensing requires the imported non-conformal extension.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG280 `TQMQG280_IndexAndConformalInvariance` | PASS (n=1 vs ρ^(1/d)) |
| TQMQG281 `TQMQG281_MechanismCensus` | PASS (4 native / 1 imported) |
| TQMQG282 `TQMQG282_Classification` | PASS (null geodesics DERIVED, TRM IMPORTED) |

Code: `TQM.Core/ResearchXH/PropagationLaw.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase28_PropagationLawTests.cs`.
