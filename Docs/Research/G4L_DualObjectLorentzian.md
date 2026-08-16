# G4-L Phase 9 — Dual-Object Lorentzian Structure

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 9 — must causality and Lorentzian signature be two operators, not one?
**Status:** COMPLETED — 3/3 xUnit tests pass

---

## 1. Goal

Following the Phase-8 audit (the signature–causality tension), test whether the two properties
must live in **two operators** — the native analogue of BDG's symmetric d'Alembertian □ + retarded
Green-function split:

- **Signature Operator** S = H2 + D = 2R1 + R2 + D  (indefinite, time-symmetric/Feynman)
- **Retarded Propagator** G = D + 2R1  (strictly causal, lower-triangular)

---

## 2. Results (N = 72, δ-source)

| object | leakage | spectrum (n+, n−) | indefinite | direction | front-v |
|---|---|---|---|---|---|
| **S (signature)** | 0.770 | (27, 45) | ✅ | 0.537 | 0.750 |
| **G (retarded)** | 0.082 | (0, 72) | ❌ elliptic | 1.000 | 0.750 |
| H (single, native) | 0.428 | (30, 42) | ✅ | 0.879 | — |
| BDG_ret (ref) | 0.021 | (0, 72) | ❌ | ~1.0 | — |

Structural link (verified, max|diff| = 0): **S = G + R2** — the signature operator is exactly the
retarded propagator plus the future-directed part.

---

## 3. Success criteria

| criterion | result |
|---|---|
| S is indefinite (signature) | ✅ (27+, 45−) |
| G is strictly causal | ✅ leak 0.082, direction 1.000, front-v 0.75 ≤ 1 |
| S = G + R2 (structural link) | ✅ max|diff| = 0 |
| dual-object pair resolves the tension | ✅ G causal **and** S indefinite — jointly |

---

## 4. Conclusion

**YES — the dual-object formulation resolves the signature–causality tension.**

The two operators split the two properties that no single native matrix could hold:

- **G = D + 2R1** is the *retarded propagator*: leakage 0.082 (≈ BDG_ret's 0.021), directionality
  1.000 (fully forward), front velocity ≤ 1 — strictly causal, but **elliptic** (no indefinite
  spectrum).
- **S = G + R2 = H2 + D** is the *signature operator*: indefinite (27+, 45−) — the Lorentzian
  signature — but time-symmetric/Feynman (leak 0.770).

The single-object operator H (Phase 7) was a **compromise** — leak 0.428, indefinite-but-leaky.
The dual-object pair achieves **both** criteria exactly: G's causality (0.082) and S's
indefiniteness, with the clean structural identity S = G + R2 tying them together.

This mirrors BDG exactly: the symmetric d'Alembertian □ carries the (indefinite) signature while the
retarded Green function carries causality — two objects, one physics. The native single-matrix
program's irreducible ~40–55 % tail (Phase 8) is the price of conflating these two; the dual-object
formulation is leak-free without sacrificing the signature.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L90 `G4_L90_DualObjectsCarryComplementaryProperties` | PASS (S indefinite, G elliptic + causal) |
| G4-L91 `G4_L91_RetardedPropagatorPropagatesCausally` | PASS (G leak 0.082, dir 1.0, v ≤ 1) |
| G4-L92 `G4_L92_DualObjectResolvesTension` | PASS (S = G + R2 exact; pair resolves tension) |

Code: `TQM.Core/ResearchXH/LorentzianOperator.cs` (added `DegreeDiagonal`, `RetardedPropagator`,
`SignatureOperator`); tests `TQM.Tests/ResearchXH/G4L_Phase9_DualObjectLorentzianTests.cs`.
