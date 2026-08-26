# AT-QG Phase 51 — Origin of the Two-Primitive Structure

**Program:** AT-QG (Unification)
**Phase:** 51 — why are two primitives needed instead of one?
**Status:** COMPLETED — 3/3 xUnit tests pass (156/156 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG50 showed the minimal complete universe = Q-events + ψ. Here we ask why **two primitives** are needed instead of
one. Classify: FORCED / PREFERRED / CONTINGENT.

---

## 2. Spin and kind mismatch (ATQG510)

| primitive | spin | kind |
|---|---|---|
| Q-events | 0 | discrete **process** (counting events → ρ) |
| ψ | 2 | continuous **field** (propagating waves) |

The two primitives differ irreducibly in **both** spin (0 vs 2) and kind (process vs field).

---

## 3. Single primitive insufficient (ATQG511)

A single primitive would have to be **both** a spin-0 source (counting discrete events) **and** a spin-2 propagator
(continuous tensor waves). A field has a definite spin, and a process is not a field — so no single primitive can
serve both roles. **Two primitives is the minimum.**

---

## 4. Classification (ATQG512)

**FORCED** (minimal), tiered:

- the **Q-events half** is FORCED — actualization is intrinsically a discrete scalar process;
- the **ψ half** is CONTINGENT on the spin-2 GW observation (QG48);
- the structure is not one choice among many — it is the minimal closure.

---

## 5. Conclusion

Nature needs two primitives because **actualization** (a discrete scalar process of events being counted) and
**propagation** (a continuous spin-2 field) are irreducibly different in spin and kind. The two-primitive structure
is **FORCED** and **minimal** — with the tensor half contingent on the single model-dependent observation that
motivates it.

This completes the structural chain of the QG arc: **Q-events (scalar source) + ψ (tensor propagator) = the
minimal two-primitive universe.**

---

## Test program

| Test | Verdict |
|---|---|
| ATQG510 `ATQG510_SpinAndKindMismatch` | PASS (spin 0 vs 2, process vs field) |
| ATQG511 `ATQG511_SinglePrimitiveInsufficient` | PASS (two is minimum) |
| ATQG512 `ATQG512_Classification` | PASS (FORCED) |

Code: `AT.Core/ResearchXH/OriginOfTwoPrimitives.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase51_OriginOfTwoPrimitivesTests.cs`.
