# TQM-QG Phase 52 — Is ψ Fundamental or Effective?

**Program:** TQM-QG (Unification)
**Phase:** 52 — must ψ exist microscopically, or can it emerge in the continuum limit?
**Status:** COMPLETED — 3/3 xUnit tests pass (159/159 TQM-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG51 established the minimal two-primitive structure. Here we ask whether ψ must exist at the microscopic level or
can emerge only in the continuum limit. Classify: FUNDAMENTAL / EFFECTIVE / UNDECIDED.

---

## 2. Coarse-graining preserves spin (TQMQG520)

Averaging (coarse-graining) is a **spin-preserving** operation: scalar constituents average to a scalar field,
never a tensor. The microscopic theory (Q-events → ρ) is **scalar (spin-0)**, so its continuum limit is also
scalar — no spin-2 emerges.

---

## 3. No collective tensor mode (TQMQG521)

Collective modes inherit the symmetry of the microscopic theory. Scalar (isotropic) Q-events have scalar
(breathing) collective modes only; a transverse-traceless (spin-2) mode requires microscopic tensor (anisotropic)
degrees of freedom, which Q-events do not possess (QG23/QG37/QG49).

---

## 4. Classification (TQMQG522)

**FUNDAMENTAL.**

- NOT EFFECTIVE: spin-2 cannot emerge from scalar constituents under coarse-graining.
- FUNDAMENTAL: ψ must exist at the microscopic level as a genuine spin-2 degree of freedom — not a
  continuum-limit artifact of the scalar actualization.

---

## 5. Conclusion

ψ is **FUNDAMENTAL**, not effective. The spin-2 content cannot be produced by coarse-graining a scalar network, so
ψ is a genuine microscopic degree of freedom — confirming it as a true primitive (not an emergent field) in the
minimal two-primitive structure (QG50/51).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG520 `TQMQG520_CoarseGrainingPreservesSpin` | PASS (scalar → scalar) |
| TQMQG521 `TQMQG521_NoCollectiveTensorMode` | PASS (no spin-2 emergence) |
| TQMQG522 `TQMQG522_Classification` | PASS (FUNDAMENTAL) |

Code: `TQM.Core/ResearchXH/FundamentalVsEffectivePsi.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase52_FundamentalVsEffectivePsiTests.cs`.
