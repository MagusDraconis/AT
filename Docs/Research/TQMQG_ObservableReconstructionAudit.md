# TQM-QG Phase 25 — Observable Reconstruction Audit

**Program:** TQM-QG (Unification)
**Phase:** 25 — separate OBSERVED EFFECT from GR EXPLANATION across the failure set
**Status:** COMPLETED — 3/3 xUnit tests pass (78/78 TQM-QG)
**Constraint:** no new primitives (this is an epistemological audit, not new physics)

---

## 1. Goal

Every prior "failure" (lensing, horizon thermodynamics, GW detector outputs) was identified using GR's observable
mappings. Here we ask: do those effects require tensor gravity *directly*, or only specific observables? We
separate the OBSERVED EFFECT (what is literally measured — its spin) from the GR EXPLANATION (a spin-2 metric
perturbation). Classify: TENSOR REQUIRED / OBSERVABLE AMBIGUITY / UNDECIDED.

---

## 2. Separation (TQMQG250)

| Observable | measured effect | spin | GR explanation |
|---|---|---|---|
| lensing-deflection | a deflection angle | 0 | geodesic bending, Weyl ≠ 0 |
| time-delay | a time shift | 0 | gravitational potential + path geometry |
| magnification | a magnification factor | 0 | lensing Jacobian (convergence + shear) |
| horizon-shadow | an angular size | 0 | photon sphere of a non-conformal horizon |
| hawking-temperature | a temperature | 0 | surface gravity of a Schwarzschild horizon |
| **gw-strain** | **h_+ and h_× (two helicities)** | **2** | transverse-traceless spin-2 |

Five of six observables measure a **single scalar quantity**; only the GW strain is intrinsically **spin-2** at
the level of the measurement itself.

---

## 3. Classification (TQMQG251)

| Classification | count | observables |
|---|---|---|
| TENSOR REQUIRED | 1 | gw-strain |
| OBSERVABLE AMBIGUITY | 4 | lensing-deflection, time-delay, magnification, horizon-shadow |
| UNDECIDED | 1 | hawking-temperature |

Lensing (and its descendants — time-delay, magnification) and the horizon shadow only need a **non-conformal
metric**, which a **scalar ψ** supplies. They are "tensor required" only under GR's particular observable mapping,
not in fact. The GW strain is different: its quadrupole, two-helicity polarization content *is* a spin-2 quantity
and cannot be produced by any scalar. Hawking temperature is left UNDECIDED: scalar-tensor theories DO recover
T ∝ 1/M, but TQM's ψ-extension horizon thermodynamics has not yet been re-derived.

---

## 4. Minimal-d.o.f. refinement (TQMQG252)

- Scalar-capable set (lensing + shadow, 4 observables): **1 d.o.f.** (a scalar ψ).
- Full set (including GW strain): **2 d.o.f.** (the spin-2 graviton).

This **refines QG24**: the 2-d.o.f. graviton is required *specifically* by the GW **polarization** observable.
Lensing, time-delay, magnification, and the shadow would be restored by a 1-d.o.f. scalar ψ alone. The tensor
requirement is narrower than "all three observables" — it is exactly the GW detector output.

---

## 5. Conclusion

The observational pressure for a **tensor** primitive comes from a **single** observable: the quadrupole,
two-polarization GW strain. Every other reported failure is an **observable-mapping assumption** — they need
non-conformal geometry (cheap: 1 scalar ψ), not spin-2. TQM's minimal extension therefore has a two-tier cost:
**1 scalar d.o.f.** to restore lensing and horizon phenomenology, **2 d.o.f. (spin-2)** only if the GW polarization
observable is taken at face value.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG250 `TQMQG250_SeparateEffectFromExplanation` | PASS (5 scalar, 1 spin-2) |
| TQMQG251 `TQMQG251_ClassificationCounts` | PASS (1 / 4 / 1) |
| TQMQG252 `TQMQG252_MinimalDofRefinement` | PASS (scalar 1 vs graviton 2) |

Code: `TQM.Core/ResearchXH/ObservableReconstructionAudit.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase25_ObservableReconstructionAuditTests.cs`.
