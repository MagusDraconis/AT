# TQM-QG Phase 43 — Observational Uniqueness of ψ

**Program:** TQM-QG (Unification)
**Phase:** 43 — which observations require the tensor ψ and cannot be reproduced by a scalar?
**Status:** COMPLETED — 3/3 xUnit tests pass (132/132 TQM-QG)
**Constraint:** no new primitives (audit only)

---

## 1. Goal

Determine which observations require the TENSOR ψ and which can be reproduced by a SCALAR (non-conformal) ψ.
Classify: SCALAR / PSI / AMBIGUOUS.

---

## 2. Classification table (TQMQG430)

| observable | classification | spin |
|---|---|---|
| lensing | **SCALAR** | 0 (a deflection angle) |
| GW polarization | **PSI** | 2 (h_+, h_×) |
| Shapiro delay | **SCALAR** | 0 (a time shift) |
| PPN γ | **SCALAR** | 0 (a scalar parameter) |
| horizon physics | **AMBIGUOUS** | 0 (shadow/entropy scalar; Hawking T undecided) |

**3 SCALAR / 1 PSI / 1 AMBIGUOUS.**

---

## 3. The PSI case (TQMQG431)

Lensing, Shapiro delay, and PPN γ are each a single SCALAR quantity: a 1-d.o.f. non-conformal scalar ψ (γ → ≠ −1)
reproduces all three. Only the GW polarization (h_+, h_×) is intrinsically spin-2 and requires the tensor ψ.

---

## 4. Uniqueness summary (TQMQG432)

- Exactly **ONE** observable — the **GW polarization** — genuinely requires the spin-2 tensor ψ.
- Lensing, delay, and γ need only a scalar ψ (1 d.o.f.).
- Horizon physics is AMBIGUOUS: shadow/entropy are scalar, Hawking T is UNDECIDED (QG25).

---

## 5. Conclusion

This **refines QG40**: the tensor ψ is observationally **unique only for gravitational-wave polarization**. A
cheaper 1-d.o.f. scalar ψ (breaking conformal flatness) would already restore lensing, Shapiro delay, and γ = +1.
So ψ's irreducible spin-2 content is demanded by a **single, specific observation**: the GW polarization. The
graviton is, observationally, the most economical possible addition — it is the one spin-2 requirement in a theory
whose every other observational gap is scalar.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG430 `TQMQG430_ClassificationTable` | PASS (3 SCALAR / 1 PSI / 1 AMBIGUOUS) |
| TQMQG431 `TQMQG431_PsiCase` | PASS (GW polarization spin-2) |
| TQMQG432 `TQMQG432_UniquenessSummary` | PASS (ψ unique for GW only) |

Code: `TQM.Core/ResearchXH/ObservationalUniqueness.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase43_ObservationalUniquenessTests.cs`.
