# TQM-QG Phase 47 — Why Does Primitive 2 (ψ) Exist?

**Program:** TQM-QG (Unification)
**Phase:** 47 — what principle forces the existence of ψ?
**Status:** COMPLETED — 3/3 xUnit tests pass (144/144 TQM-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

Q-events derive the entire scalar sector; ψ is required only for spin-2 observables. Determine the principle behind
ψ's existence. Classify: FORCED / PREFERRED / CONTINGENT / NEW POSTULATE.

---

## 2. Q-event-only vs Q-event + ψ (TQMQG470)

The scalar (conformal, γ=−1) universe still has: redshift, matter attraction, flat rotation curves (log-deficit),
regular cores (saturation). It **cannot** produce:

- lensing (γ=−1, no deflection),
- Shapiro delay,
- PPN γ=+1,
- GW polarization (spin-2).

Only GW polarization is spin-2 and uniquely requires the tensor ψ; the others need only a scalar ψ (QG43).

---

## 3. No internal forcing (TQMQG471)

- The scalar universe is **internally self-consistent** — it has no contradiction, it simply fails to bend light
  and emit GWs.
- The "principle" motivating ψ is **observational completeness** (light bending / GWs), not internal necessity.
- The scalar responds only to the trace of the stress-energy, not the full tensor.

---

## 4. Classification (TQMQG472)

**NEW POSTULATE.**

- NOT FORCED: no internal principle demands ψ.
- CONTINGENT: ψ is added because of a specific observation (GW polarization, QG43).
- PREFERRED (form only): given GWs exist, spin-2 is the unique viable spin (QG46).
- NEW POSTULATE: ψ is a primitive axiom — not derivable or emergent (QG23/24/37).

---

## 5. Conclusion

**Why ψ exists:** not because TQM forces it, but because the universe demonstrably has spin-2 gravitational waves
and light bending, which the derived scalar sector cannot produce. ψ is the **minimal new postulate** that closes
this observational gap — the **second and final primitive** of the theory.

This completes the "why" of the QG arc: Q-events (Primitive 1) are the actualization substrate; ψ (Primitive 2) is
the minimal observational completion, unique in spin and maximally decoupled.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG470 `TQMQG470_WhatIsImpossibleWithoutPsi` | PASS (4 observations) |
| TQMQG471 `TQMQG471_NoInternalForcing` | PASS (self-consistent, not forced) |
| TQMQG472 `TQMQG472_Classification` | PASS (NEW POSTULATE) |

Code: `TQM.Core/ResearchXH/WhyPsiExists.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase47_WhyPsiExistsTests.cs`.
