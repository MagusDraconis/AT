# TQM-QG Phase 57 — Excitation of the Traceless Link Sector

**Program:** TQM-QG (Unification)
**Phase:** 57 — what excites the traceless content of network links?
**Status:** COMPLETED — 3/3 xUnit tests pass (174/174 TQM-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG56 showed the Weyl capacity is forced but its excitation is contingent. Here we ask what excites the traceless
content of network links. Classify: DERIVED / PREFERRED / OBSERVATION-TRIGGERED.

---

## 2. Quadrupole sourcing (TQMQG570)

| candidate | quadrupole (source)? |
|---|---|
| anisotropic sources | yes |
| moving deficits | yes |
| binary systems | yes |
| network stress | yes |
| propagation stability | no (a necessary property, not a source) |

A spin-2 field couples to the full stress-energy T_μν, so the traceless (quadrupole) part of matter excites the
traceless (Weyl) part of ψ.

---

## 3. Mechanism vs instances (TQMQG571)

- **DERIVED mechanism:** quadrupole → Weyl sourcing is a consequence of spin-2 coupling (Weinberg: a massless
  spin-2 field must couple to T_μν).
- **OBSERVATION-TRIGGERED instances:** which sources actually excite ψ (binary mergers, supernovae) is set by the
  astrophysical events we observe.

---

## 4. Classification (TQMQG572)

**DERIVED** (mechanism) + OBSERVATION-TRIGGERED (instances).

---

## 5. Conclusion

ψ is excited by **anisotropic (quadrupole) sources** — moving deficits and binary systems being the canonical
gravitational-wave sources. The excitation mechanism is **DERIVED** from spin-2 coupling to T_μν, while the specific
instances are **observation-triggered**. This completes the excitation story: the Weyl capacity is forced (QG56),
and its excitation is the quadrupole sourcing of a spin-2 field.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG570 `TQMQG570_QuadrupoleSourcing` | PASS (4 quadrupole sources) |
| TQMQG571 `TQMQG571_MechanismVsInstances` | PASS (derived + triggered) |
| TQMQG572 `TQMQG572_Classification` | PASS (DERIVED) |

Code: `TQM.Core/ResearchXH/WeylExcitation.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase57_WeylExcitationTests.cs`.
