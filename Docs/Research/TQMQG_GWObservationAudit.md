# TQM-QG Phase 48 — GW Observation Audit

**Program:** TQM-QG (Unification)
**Phase:** 48 — what is directly observed, and what is inferred?
**Status:** COMPLETED — 3/3 xUnit tests pass (147/147 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

ψ exists only because of GW polarization data (QG47). Here we separate what is DIRECTLY observed from what is
INFERRED, and determine whether spin-2 is directly measured or reconstructed. Classify: DIRECT / MODEL-DEPENDENT /
UNDECIDED.

---

## 2. The four layers (TQMQG480)

| layer | classification |
|---|---|
| detector signal (strain h(t)) | **DIRECT** |
| polarization reconstruction (h_+/h_×/b/x) | MODEL-DEPENDENT |
| model assumptions (GR templates) | MODEL-DEPENDENT |
| spin assignment (spin-2) | MODEL-DEPENDENT |

**1 DIRECT / 3 MODEL-DEPENDENT / 0 UNDECIDED.**

---

## 3. Spin-2 is reconstructed (TQMQG481)

The only DIRECT observable is the **strain signal h(t)** (differential arm-length change δL/L). The spin-2 (tensor)
reading is **RECONSTRUCTED**: it is the output of fitting the strain to a polarization basis under GR model
assumptions (templates, massless light-speed propagation).

---

## 4. Consequence for ψ (TQMQG482)

QG47 said ψ exists because of GW polarization data. This audit refines that: the data are a **direct strain
signal**, but the "spin-2 polarization" reading is a **model-dependent reconstruction**. So:

- ψ is justified by an **inference**, not a raw measurement;
- ψ remains the minimal postulate consistent with that model, but its necessity is **one model-deep** — forced by
  the GR reconstruction of the strain, not by the strain itself.

---

## 5. Conclusion

This is the final epistemological honesty of the QG arc: the one observation that motivates Primitive 2 is itself
a **model-dependent reconstruction**. ψ is therefore a **model-consistent postulate**, not a directly-forced one.
The tensor interpretation is the best (and unique viable, QG46) reading of the strain, but it is an interpretation.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG480 `TQMQG480_FourLayers` | PASS (1 DIRECT / 3 MODEL-DEPENDENT) |
| TQMQG481 `TQMQG481_Spin2Reconstructed` | PASS (spin-2 reconstructed) |
| TQMQG482 `TQMQG482_ConsequenceForPsi` | PASS (ψ model-consistent) |

Code: `TQM.Core/ResearchXH/GWObservationAudit.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase48_GWObservationAuditTests.cs`.
