# TQM-QG Phase 39 — Separate TRM into Derived and Non-Derived Sectors

**Program:** TQM-QG (Unification)
**Phase:** 39 — which TRM results are saturation physics vs ψ/tensor physics?
**Status:** COMPLETED — 3/3 xUnit tests pass (120/120 TQM-QG)
**Constraint:** no new primitives (audit of the already-identified ψ extension)

---

## 1. Goal

QG36/QG38 derived the regular-core mechanism from Q-event counting (SATURATION physics); QG23/QG24/QG37
established ψ as the new tensor primitive (PSI physics). Here we separate six TRM observables into three buckets:
SATURATION / PSI / BOTH.

---

## 2. Sector census (TQMQG390)

| observable | sector |
|---|---|
| redshift | **SATURATION** (g_00 = −ρ^(2/d), no ψ) |
| lensing | **PSI** (non-conformal deflection) |
| PPN parameters | **PSI** (γ −1 → +1) |
| regular black hole | **BOTH** |
| horizon thermodynamics | **PSI** (horizon surface gravity) |
| GW phenomenology | **PSI** (spin-2) |

**1 SATURATION / 4 PSI / 1 BOTH.**

---

## 3. The BOTH case (TQMQG391)

A regular black hole is composite: the finite-curvature **core** is derived from Q-event saturation (QG36), while
the **horizon** requires the non-conformal ψ (QG33/35). Neither sector alone suffices.

---

## 4. Summary (TQMQG392)

- **DERIVED SCALAR SECTOR (saturation physics)**: redshift; the regular core.
- **NEW TENSOR PRIMITIVE (ψ physics)**: lensing, PPN recovery, horizon thermodynamics, GW phenomenology; the
  horizon of a regular black hole.

---

## 5. Conclusion

TRM's payload is **overwhelmingly ψ/tensor** (4 of 6 observables plus the horizon). Only **redshift** and the
**regular core** are derived scalar physics. This is the final sector separation:

- TQM derives the **scalar backbone** and the **regular core** (counting → saturation → profile).
- The **tensor sector ψ** is the single non-derived ingredient carrying **lensing, GWs, and horizons**.

This completes the QG unification arc's decomposition: one derived scalar sector, one irreducible tensor primitive.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG390 `TQMQG390_SectorCensus` | PASS (1 SATURATION / 4 PSI / 1 BOTH) |
| TQMQG391 `TQMQG391_BothCase` | PASS (core + horizon) |
| TQMQG392 `TQMQG392_Summary` | PASS (payload is ψ) |

Code: `TQM.Core/ResearchXH/TRMSectorAudit.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase39_TRMSectorAuditTests.cs`.
