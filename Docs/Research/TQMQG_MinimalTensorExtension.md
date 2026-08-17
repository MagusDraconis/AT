# TQM-QG Phase 24 — Minimal Tensor Extension Audit

**Program:** TQM-QG (Unification)
**Phase:** 24 — the smallest extra primitive that restores lensing, tensor GWs, and Hawking thermodynamics
**Status:** COMPLETED — 3/3 xUnit tests pass (75/75 TQM-QG)
**Constraint:** no new primitives (the *conclusion* is the smallest primitive that would have to be added)

---

## 1. Goal

QG23 showed ψ cannot emerge from the scalar actualization. Here we find the **smallest** extra primitive that
restores the three observable gaps: lensing, tensor gravitational waves, and Hawking thermodynamics. Four
candidates are ranked by the additional degrees of freedom they introduce and whether they can source a
helicity-2 (spin-2) mode. Classify: DERIVED / EMERGENT / NEW PRIMITIVE / **MINIMAL NEW PRIMITIVE**.

---

## 2. Candidate census (TQMQG240)

Conventions: d = spatial dimension (3), spacetime D = d+1 (4); graviton polarizations = (d+1)(d−2)/2 = **2**.

| Candidate | d.o.f. | Spin | Verdict |
|---|---|---|---|
| tensor counting measure | 6 (symmetric rank-2) | 2 | over-complete |
| directional actualization | 3 (vector) | 1 | **insufficient** (cannot make helicity-2) |
| anisotropic causal structure | 6 (rank-2 on the cone) | 2 | over-complete |
| **ψ-field (spin-2)** | **2 (transverse-traceless graviton)** | 2 | **minimal & capable** |

A symmetric rank-2 tensor's 6 components decompose into 1 scalar + 3 vector + 2 transverse-traceless graviton;
the graviton subset is the only part actually needed.

---

## 3. Observable requirements (TQMQG241)

| Observable | minimum additional d.o.f. | note |
|---|---|---|
| lensing (Weyl ≠ 0) | 1 | a scalar ψ already breaks conformal flatness (MetricAnsatzAudit) |
| tensor GWs (helicity-2) | 2 | the two polarizations |
| Hawking T (T = κ/2π) | 0 | derived from the horizon profile, no independent mode |

Minimal additional d.o.f. = max(1, 2, 0) = **2**. The two graviton helicities are the unique minimum: they cover
lensing (1), satisfy tensor GWs (exactly 2), and cost nothing extra for Hawking T.

---

## 4. Classification (TQMQG242)

**MINIMAL NEW PRIMITIVE.**

- NOT DERIVED / NOT EMERGENT: QG23 rules these out from the single scalar actualization.
- The required new primitive is the **ψ-field**: a transverse-traceless, symmetric rank-2 (spin-2) field carrying
  exactly the 2 graviton polarizations. It is the smallest extension that restores all three observables; every
  other candidate is either over-complete (rank-2: 6 d.o.f.) or insufficient (vector: spin-1).

---

## 5. Conclusion

The minimal tensor extension of TQM is a **single spin-2 field ψ** with **2 degrees of freedom** (the graviton).
This closes the QG arc with a precise cost: TQM's conformal core needs exactly one new primitive — the graviton —
and adding it restores lensing, gravitational waves, and horizon thermodynamics simultaneously.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG240 `TQMQG240_CandidateCensus` | PASS (ψ minimal & spin-2-capable) |
| TQMQG241 `TQMQG241_ObservableRequirements` | PASS (minimal = 2 graviton d.o.f.) |
| TQMQG242 `TQMQG242_Classification` | PASS (MINIMAL NEW PRIMITIVE) |

Code: `TQM.Core/ResearchXH/MinimalTensorExtension.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase24_MinimalTensorExtensionTests.cs`.
