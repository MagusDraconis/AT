# AT-QG Phase 32 — TRM Compatibility Audit

**Program:** AT-QG (Unification)
**Phase:** 32 — which AT derivations break if the TRM (ψ) kernel is added?
**Status:** COMPLETED — 3/3 xUnit tests pass (99/99 AT-QG)
**Constraint:** no new primitives (audit of the effect of the already-identified ψ extension)

---

## 1. Goal

The TRM kernel is the ψ (non-conformal) sector (QG31). Before any AT/TRM unification, determine which existing
AT derivations survive if that kernel is added. Classify each: UNCHANGED / MODIFIED / BROKEN.

---

## 2. Compatibility matrix (ATQG320)

| derivation | classification | reason |
|---|---|---|
| counting measure (ρ counts Q-events) | UNCHANGED | ψ does not touch the 1-point ρ |
| metric origin √(−g)=ρ | UNCHANGED | det g = −ρ² is independent of ψ |
| matter = deficit (m = ρ̄−ρ) | UNCHANGED | scalar; ψ does not touch it |
| Einstein structure (G_μν from σ) | **MODIFIED** | gains ψ/Weyl (tensor) terms |
| α=0 attractor (scale-free ρ) | UNCHANGED | scale-space diffusion of ρ is independent of ψ |
| critical branching (ρ from branching) | UNCHANGED | branching generates ρ, not ψ |

**5 UNCHANGED, 1 MODIFIED, 0 BROKEN.**

---

## 3. Metric origin survives (ATQG321)

The ψ-perturbation g_00 = −ρ^(2/d)e^{2ψ}, g_ii = ρ^(2/d)e^{−2ψ/(d−1)} has
det g = −ρ^(2/d)e^{2ψ} · (ρ^(2/d)e^{−2ψ/(d−1)})^{d−1} = −ρ², so **√(−g) = ρ** is preserved. The metric-origin
derivation (√(−g)=ρ → k = 2/d) survives the ψ-extension **unchanged** — the ψ-perturbation is volume-preserving.

---

## 4. Unification readout (ATQG322)

The TRM (ψ) kernel is a **clean extension**:

- the scalar backbone — counting measure, metric origin √(−g)=ρ, matter=deficit, α=0 attractor, critical
  branching — is **UNCHANGED**;
- its **only** effect is to enrich the Einstein tensor with the ψ/Weyl (tensor) terms, i.e. exactly the
  non-conformal degree of freedom needed to restore lensing / GWs / horizon thermodynamics (QG22–24);
- **nothing is broken.**

---

## 5. Conclusion

A AT/TRM unification can proceed on this matrix: **add ψ, keep all scalar derivations, replace only the Einstein
sector.** This confirms that the minimal tensor extension of QG24 is structurally non-invasive — it augments
curvature (the tensor/Weyl sector) without disturbing the actualization backbone that produced the counting
measure, the metric, matter, and the α=0 attractor.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG320 `ATQG320_CompatibilityMatrix` | PASS (5 UNCHANGED / 1 MODIFIED / 0 BROKEN) |
| ATQG321 `ATQG321_MetricOriginPreserved` | PASS (√(−g)=ρ under ψ) |
| ATQG322 `ATQG322_UnificationReadout` | PASS (clean extension) |

Code: `AT.Core/ResearchXH/TRMCompatibilityAudit.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase32_TRMCompatibilityAuditTests.cs`.
