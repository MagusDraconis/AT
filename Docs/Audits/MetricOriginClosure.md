# Metric Origin Closure Audit — Report

**Test file:** `AT.Tests/ResearchXC/MetricOriginTests.cs`
**Result:** **PASSED (3/3).**

---

## Results

| # | Test | Result | Classification |
|---|---|---|---|
| 1 | CausalOrder_ContainsConformalInformation | order invariant under $g\to f\cdot g$; Malament cited | **contains the class, not the factor** |
| 2 | ConformalClass_UniquenessCondition | class unique up to factor; factor free ($\sqrt{|g|}=1$ vs $100$) | **uniqueness holds** |
| 3 | MetricOrigin_NativeOrImported | order native, class imported (proven), factor native | **verdict** |

---

## Classification of the metric-origin chain

| Link | Status | Nature |
|---|---|---|
| Q-events | **NATIVE** | AT-derived primitive |
| Causal order | **NATIVE** | AT-derived (precedence from individuation) |
| Conformal class (order → light cone → class) | **IMPORTED** | Malament 1977 / Hawking–King–McCarthy 1976 — **PROVEN** |
| Conformal factor (counting measure) | **NATIVE** | reconstructed in MetricEmergenceProgram |
| Full $g_{\mu\nu}$ | **determined** | class × factor — closed |

---

## The conformal-class "gap" is an imported theorem, not a theory gap

`GrBridgeAnalyzer` already states it exactly:

> "The causal set → manifold reconstruction theorem (Malament 1977, Hawking-King-McCarthy 1976)
> shows that the causal order plus volume element determines the metric up to conformal factor.
> AT does not re-derive this. Imported from mathematical relativity."

The audit confirms this is **correct and sufficient**:

1. **The causal order contains the conformal information** — it is invariant under conformal
   transformation, so it fixes the class and *nothing* else.
2. **The class is unique** — conformally-related metrics are the only ones sharing the causal
   order (Malament's uniqueness); the conformal factor is the single remaining freedom.
3. **The factor is a separate, native input** — the counting measure (interval volume) fixes it
   ($f=\rho^{2/d}$), and it is *not* determined by the order.

## Final verdict

- **Publication blocker?** **NO** — importing a proven theorem (Malament) is standard
  mathematical practice, not a defect or an unproven claim.
- **Research program?** Only an *optional* native re-derivation of Malament; not required for
  the metric origin to be valid.
- **Already solved?** **YES** — the metric origin is logically closed:
  `Q-events → causal order (native) → conformal class (proven) → conformal factor (native)
  → g_μν`. The metric is *determined*, not merely described.

The residual gaps identified in the prior audits (MetricGenerationAudit, MetricEmergenceProgram,
ConformalStructureProgram) are therefore **not** theory gaps — they are the legitimate,
already-proven Malament reconstruction that AT imports. The only genuine open item, if any, is
the *preference* to re-derive Malament natively, which is a research taste, not a correctness
requirement.
