# AT-QG Phase 22 — Conformal-Flatness Audit

**Program:** AT-QG (Unification)
**Phase:** 22 — are the failures consequences of conformal flatness itself?
**Status:** COMPLETED — 3/3 xUnit tests pass (69/69 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

Multiple failures point to the same source: no lensing (QG21), no tensor GWs (QG18), no Hawking T (QG13). Here
we test whether these are consequences of the conformal-flatness assumption (ψ=0) or fundamental AT results.
Classify: CONFORMAL-FLATNESS ARTIFACT / FUNDAMENTAL AT RESULT.

---

## 2. Results

### (a) "No lensing" is a conformal-flatness artifact (ATQG220)

Light bending (the reference-metric curvature that deflects null geodesics) is **zero at ψ=0** (conformal
flatness) and **non-zero at ψ≠0** (weakly non-conformal). Relaxing conformal flatness restores lensing.

### (b) "No tensor GWs" is a conformal-flatness artifact (ATQG221)

The tensor (Weyl+graviton) sector has 12 d.o.f. at d=3, **frozen to zero by ψ=0** and **activated by ψ≠0** —
the same knob as lensing.

### (c) Classification (ATQG222)

**CONFORMAL-FLATNESS ARTIFACT** (lensing + tensor GWs directly; Hawking T partly).

---

## 3. Classification: CONFORMAL-FLATNESS ARTIFACT

- **No lensing**: DIRECT artifact of ψ=0 (Weyl=0 → null geodesics straight).
- **No tensor GWs**: DIRECT artifact of ψ=0 (graviton frozen).
- **No Hawking T**: PARTLY an artifact — but the main failure (T ∝ R vs 1/R) stems from the mass-radius relation
  (deficit mass ∝ R^d vs Schwarzschild M ∝ R), a separate issue.

The three failures are **not** fundamental AT results: they are consequences of the conformal-flatness
ASSUMPTION (minimum-information, G4-A1), which is PREFERRED but not derived. They share a single cure: a weakly
non-conformal reference (ψ/Weyl field) — the new primitive already identified in QG19.

---

## 4. Conclusion

This is the key insight of the whole GW/observables arc: **the negative results (no lensing, no tensor GWs, no
Hawking T) are artifacts of the conformal-flatness ASSUMPTION, not of the fundamental primitives.** AT's two
primitives (causal order + counting measure) determine the conformal factor ρ and its scalar gravity; the
conformal-flatness choice (ψ=0) then freezes the tensor sector, producing the failures. Relaxing that single
assumption (admitting ψ ≠ 0) would restore lensing, tensor GWs, and (partly) horizon thermodynamics — at the cost
of the same one new primitive (ψ/Weyl field) identified throughout QG16–QG19.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG220 `ATQG220_LensingIsConformalFlatnessArtifact` | PASS (bending turns on with ψ) |
| ATQG221 `ATQG221_TensorModesConformalFlatnessArtifact` | PASS (tensor sector frozen by ψ=0) |
| ATQG222 `ATQG222_Classification` | PASS (CONFORMAL-FLATNESS ARTIFACT) |

Code: `AT.Core/ResearchXH/ConformalFlatnessAudit.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase22_ConformalFlatnessAuditTests.cs`.
