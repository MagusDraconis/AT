# AT-QG Phase 31 — Derive the TRM Propagator from Q-Event Network Dynamics

**Program:** AT-QG (Unification)
**Phase:** 31 — what rule governs propagation of a tick, and is TRM's kernel a propagation law or a correlation?
**Status:** COMPLETED — 3/3 xUnit tests pass (96/96 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

Q-events are local ticks (QG29). Determine the rule governing how a tick propagates through the network, and
whether TRM's kernel M_eff(r) can be derived as a **propagation law** (a response function) rather than a
**correlation function**. Classify: NO RELATION / PARTIAL MATCH / SAME OBJECT.

---

## 2. Results

### (a) Native tick propagation vs TRM kernel (ATQG310)

- A tick propagates along the **generation relation**, whose boundary is the **light cone** (the conformal class):
  native index **n = 1**, effective profile **M_eff = n−1 = 0** (massless null propagation).
- TRM's kernel is a **nonzero** refractive/mass profile **M_eff = e^Φ−1 ≠ 0**.
- Both share the **causal** (retarded, light-cone) structure.

### (b) Derivability (ATQG311)
- The native tick dynamics give **only M_eff = 0**. TRM's nonzero kernel is a propagation law of the **ψ-extension**,
  not of the conformal tick network.
- As a correlation function it is zero-mean jitter (QG30). As a propagation law it is the ψ sector.
- The two **coincide only at M_eff = 0** (ψ = 0).

### (c) Classification (ATQG312)

**PARTIAL MATCH.**

| aspect | shared? |
|---|---|
| causal (retarded, light-cone) structure | yes |
| refractive content M_eff | no (0 vs e^Φ−1) |
| coincide | only at ψ = 0 |

---

## 3. Conclusion

TRM's kernel is **NOT** derivable as a propagation law from the conformal Q-event network: the native tick dynamics
are massless null propagation (M_eff = 0), while TRM's kernel is the **non-conformal (ψ) sector** (M_eff = e^Φ−1).
The two share the causal structure but differ by exactly the ψ correction — a **PARTIAL MATCH** that becomes a
SAME OBJECT only in the conformal limit. In neither the propagation nor the correlation reading is TRM's kernel
native to AT; it remains the imported ψ (QG23/QG24/QG28/QG30).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG310 `ATQG310_NativeVsTrm` | PASS (M_eff=0 vs e^Φ−1) |
| ATQG311 `ATQG311_Derivability` | PASS (not derivable natively) |
| ATQG312 `ATQG312_Classification` | PASS (PARTIAL MATCH) |

Code: `AT.Core/ResearchXH/TRMPropagatorOrigin.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase31_TRMPropagatorOriginTests.cs`.
