# Conformal Structure Program — Report

**Test file:** `TQM.Tests/ResearchXC/ConformalStructureTests.cs`
**Result:** **PASSED (3/3).**

---

## Results

| # | Test | Result | Classification |
|---|---|---|---|
| 1 | CausalOrder_DefinesLightConeStructure | causal order is a valid partial order; its boundary is the light cone | **PRESENT** |
| 2 | LightConeStructure_DeterminesConformalClass | null structure invariant under $g\to f\cdot g$ ($f>0$); changed by non-conformal rescale | **standard (holds)** |
| 3 | ConformalClass_ReconstructibleOrImported | order native, order→conformal-class **IMPORTED** (Malament) | **verdict** |

---

## What TQM already contains

- **Native primitive:** the causal partial order. `OriginOfCausalityModel.Axioms()` gives
  transitivity, antisymmetry, acyclicity, local finiteness; `GrBridgeAnalyzer` marks
  "Causal ordering" as **TQM-derived** (`IsTqmNative = true`).
- **The claim:** `CausalOrderAnalyzer` and `GeometryEmergence.MetricRecovery` state
  "causal relation defines the light cone ⇒ the conformal metric."

## What the standard mathematics shows (verified)

- A reconstructed 1+1D causal order is a valid partial order whose boundary (null-separated
  pairs, $|\Delta x|=\Delta t$) is exactly the light cone.
- The null (light-cone) structure is invariant under conformal transformations $g\to f\cdot g$
  with $f>0$ — so the light cone picks out **exactly** the conformal class, no more and no
  less. A non-conformal rescale $g=\mathrm{diag}(-1,2)$ changes the null cone (the null vector
  $(1,1)$ maps to $g(v,v)=1\neq0$), confirming the conformal class is the *unique* structure
  fixed by the light cone.

## Conclusion

The causal order **does** contain enough information to reconstruct the conformal class — this
is precisely Malament's theorem (causal order ⇒ light cones ⇒ conformal metric). But:

- **PRESENT (native):** the causal order itself (the light-cone primitive).
- **IMPORTED (external):** the reconstruction *order → conformal class* — TQM cites the
  Malament / Hawking–King–McCarthy theorem (`GrBridgeAnalyzer`, "External theorem") and does
  not re-derive it.

So the conformal class is **reconstructible in principle** (the order is sufficient) but
**imported in practice** (TQM does not compute the light-cone → conformal-metric map). This
completes the picture from MetricEmergenceProgram.md: the conformal *factor* is native
(counting measure) and the conformal *class* is imported (causal order → Malament). Together,
the full metric $g_{\mu\nu}$ is **described and imported, but not generated**.
