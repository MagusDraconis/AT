# TQM-QG Phase 45 — Minimal Coupling of ψ

**Program:** TQM-QG (Unification)
**Phase:** 45 — the weakest coupling between ψ and the scalar backbone
**Status:** COMPLETED — 3/3 xUnit tests pass (138/138 TQM-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG44 gave ψ the massless spin-2 wave equation. Here we find the weakest coupling between ψ and the derived scalar
backbone (ρ, the deficit, saturation, Q-event density) required to recover the GW polarization observable.
Classify: INDEPENDENT / WEAKLY COUPLED / STRONGLY COUPLED.

---

## 2. Zero coupling for the polarization (TQMQG450)

| coupling | required for polarization? |
|---|---|
| ψ ↔ ρ | no |
| ψ ↔ deficit | no |
| ψ ↔ saturation | no |
| ψ ↔ Q-event density | no |

The two helicities (h_+, h_×) are **intrinsic** to the FREE massless spin-2 field — recovering the polarization
structure requires **zero** coupling to the scalar sector.

---

## 3. Polarization vs sourcing (TQMQG451)

- **Polarization (2 helicities):** intrinsic — needs no coupling.
- **Sourcing (nonzero amplitude h ~ κ·source):** needs a coupling to the matter deficit, and that coupling is the
  **weak** gravitational constant κ = 8πG.

---

## 4. Classification (TQMQG452)

**INDEPENDENT** (for the polarization observable), **WEAKLY COUPLED** (only when sourced).

- NOT strongly coupled: nothing requires a large coupling; the observed GWs are linear/weak-field.

---

## 5. Conclusion

ψ is the **most decoupled possible new primitive**: it rides free (zero coupling) for its polarization content, and
touches the scalar sector only through the weak source coupling κ = 8πG. The minimal ψ coupling is **zero for
polarization** and **weak for sourcing** — reinforcing the QG arc's conclusion that ψ is the smallest possible
addition: one free massless spin-2 field, weakly sourced by the deficit.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG450 `TQMQG450_ZeroCouplingForPolarization` | PASS (zero coupling) |
| TQMQG451 `TQMQG451_PolarizationVsSourcing` | PASS (weak sourcing) |
| TQMQG452 `TQMQG452_Classification` | PASS (INDEPENDENT) |

Code: `TQM.Core/ResearchXH/MinimalPsiCoupling.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase45_MinimalPsiCouplingTests.cs`.
