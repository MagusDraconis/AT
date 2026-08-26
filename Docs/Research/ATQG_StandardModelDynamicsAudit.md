# AT-QG Phase 242 — Standard Model Dynamics Audit

**Status:** COMPLETE — 3 DERIVED / 1 HOSTED / 1 PARTIAL / 1 OPEN
**Tests:** ATQG2420, ATQG2421, ATQG2422 (all passed)
**Core class:** `AT.Core/ResearchXH/StandardModelDynamicsAudit.cs`
**Scope:** QG60/76/78-85/149-180
**Method:** audit only — determine whether SM dynamics are derived or only hosted

---

## 1. The Question

QG241 marked **Standard Model dynamics** as a PARTIAL TOE criterion. This audit
determines exactly what is DERIVED vs HOSTED: the gauge interactions, the gauge
symmetry origin, SU(3)/SU(2)/U(1) origin, and the interaction vertices.

---

## 2. The Six Checks

| Check | Status | Evidence |
|-------|--------|----------|
| **Gauge symmetry origin** | **DERIVED** | QG161 (GAUGE ORIGIN): the D96 automorphism group gives 1+3+8 = 12 generators; the 12 link-directions of C_96(1..6) ARE the gauge generators |
| **U(1) origin** | **DERIVED** | QG161: the rotation subgroup Z_96 ⊂ D96 is the U(1) charge (photon) |
| **SU(2) origin** | **DERIVED** | QG161: restricted to a Z2 doublet the D96 generators span su(2) — reflection = σ_z (T3), rotation = σ_y, commutator = σ_x; exactly 3, algebra closes |
| **SU(3) origin** | PARTIAL | QG161 derives su(3) (3²−1 = 8) from the 3 octave families; but QG79 notes the 3-color count was a NEW POSTULATE pre-D96 — structure derived, color-count identification retains a postulate trace |
| **Gauge interactions (dynamics)** | **HOSTED** | QG60/76: gauge theory is COMPATIBLE/HOSTED — the 12-generator structure is hosted, but the interaction LAGRANGIAN, vertices, and propagators are not derived from Q-events (coupling VALUES are derived QG162/163, the dynamics is not) |
| **Interaction vertices** | **OPEN** | no QG phase derives the specific vertices (γ-e-e, W-u-d, gluon-quark, Higgs Yukawa) as dynamical consequences |

---

## 3. Exact Missing Dynamics

1. **The gauge interaction Lagrangian / equations of motion** — QG60/76 host
   the structure, not the dynamics;
2. **The interaction vertices** (γ-e-e, W-u-d, gluon-quark, Higgs Yukawa) — no
   QG phase derives them;
3. **The propagators / momentum dependence** of the interactions;
4. **The SU(3)-color-count identification** with the 3-family space (QG79
   postulate trace).

---

## 4. Conclusion

### The gauge SYMMETRY is DERIVED; the gauge DYNAMICS is HOSTED.

- **3 DERIVED** (gauge symmetry origin, U(1), SU(2)) — the 1+3+8 generator
  structure is derived from D96 (QG161);
- **1 PARTIAL** (SU(3)) — the generator structure derived, the color-count
  identification retains a postulate trace;
- **1 HOSTED** (gauge interactions) — the interaction dynamics is compatible
  with the network, not derived from Q-events;
- **1 OPEN** (interaction vertices) — the specific vertices are not derived.

This is the **exact content of the QG241 "SM dynamics" partial criterion**:
the gauge structure (generators, groups, coupling values) is derived, but the
dynamical content — the interaction Lagrangian, the vertices, and the
propagators — remains hosted/open. The next step toward COMPLETE TOE is
deriving the interaction vertices and propagators from the network.
