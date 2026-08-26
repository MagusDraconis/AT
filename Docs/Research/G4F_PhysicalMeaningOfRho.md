# G4-F Phase 0 — Physical Meaning of ρ

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-F)
**Phase:** 0 — interpret the physical meaning of ρ
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Which interpretation of ρ is most self-consistent inside AT?
**Primitives used:** Q-events · counting measure · τ. No metric tensor, no Einstein equations, no Laplace–Beltrami import.

---

## 1. Goal

ρ already generates the conformal factor f = ρ^(2/d), the native operator Lc = ρ⁻¹ L ρ⁻¹,
curvature, and curvature dynamics. Determine its minimal physical interpretation among four
candidates:

- **C1** Event Density
- **C2** Actualization Rate
- **C3** Information Density
- **C4** Hybrid Density

---

## 2. Grounding

**Metric-origin chain** (`MetricOriginClosure`): Q-events → causal order (native) → conformal
class (imported Malament, PROVEN) → conformal factor **f = ρ^(2/d)** (NATIVE counting measure)
→ g_μν. The conformal factor is the counting measure — so ρ *is* the counting measure.

Programmatic grounding (`ConformalRateGraph`, d=2 ⇒ f = ρ):
- ρ is a **positive per-vertex scalar** (density) ✅
- flat (a=0): ρ ≡ 1 everywhere, mean ρ̄ = 1.000000 (normalized counting measure) ✅
- curved (a=0.5): ρ varies spatially, min 1.003 < max 1.452 ✅

---

## 3. Results

### 3.1 G4-F00 — metric-origin compatibility

| candidate | metric-origin-compatible |
|---|---|
| C1 Event Density | ✅ |
| C2 Actualization Rate | ✅ |
| C3 Information Density | ❌ |
| C4 Hybrid Density | ❌ |

### 3.2 G4-F01 — structure/content split & primitive cost

| candidate | new layers | structure/content | minimal |
|---|---|---|---|
| C1 Event Density | 0 | ✅ | ✅ |
| C2 Actualization Rate | 0 | ✅ | ✅ |
| C3 Information Density | 1 | ❌ | ❌ |
| C4 Hybrid Density | 1 | ❌ | ❌ |

- C1 = Q-events + counting measure (both native).
- C2 = Q-events + τ (native); rate = density × ω₀.
- C3 requires the emergent Θ/information layer (non-primitive).
- C4 is composite (≥ 1 layer).

### 3.3 G4-F02 — minimal-interpretation selection

| candidate | score (/4) | primitive cost | minimal? |
|---|---|---|---|
| C1 Event Density | 4 | 0 | ✅ |
| C2 Actualization Rate | 4 | 0 | ✅ |
| C3 Information Density | 0 | 1 | ❌ |
| C4 Hybrid Density | 0 | 1 | ❌ |

Minimal set (max score ∧ zero cost) = **{C1, C2}**.

**Tiebreak:** rate = density × ω₀ (ω₀ = 2π/τ is a universal constant), and the conformal
factor is defined only up to constant rescaling — so C1 and C2 are the **same primitive**.

---

## 4. Conclusion

ρ is the **counting measure**, canonically read as **EVENT DENSITY (C1)** and equivalently as
**ACTUALIZATION RATE (C2)** — two readings of one primitive, requiring **no new primitive**.
Information density (C3) and hybrid density (C4) are rejected as non-minimal / non-native.

This closes the G4 interpretation question: the entire native operator program (ρ → Lc → R →
dynamics → feedback) is built on the **counting measure**, the same object that fixes the
conformal factor in the metric-origin chain — so G4 is self-consistent with Metric Origin and
the structure/content split.

---

## Test program

| Test | Verdict |
|---|---|
| G4-F00 `G4_F00_RhoIsTheCountingMeasure` | PASS (ρ = positive per-vertex counting measure; C1/C2 compatible) |
| G4-F01 `G4_F01_NoNewPrimitiveRequired` | PASS (C1/C2 = 0 new layers; C3/C4 ≥ 1) |
| G4-F02 `G4_F02_MinimalInterpretationSelection` | PASS (minimal set = {C1, C2}; canonical = C1) |

Code: `AT.Tests/ResearchXH/G4F_PhysicalMeaningOfRhoTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
