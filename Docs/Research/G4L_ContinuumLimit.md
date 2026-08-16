# G4-L Phase 10 — Lorentzian Continuum Limit

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 10 — what continuum equation do the native Lorentzian operators generate?
**Status:** COMPLETED — 3/3 xUnit tests pass (33/33 G4-L)
**Constraint:** no BDG coefficients, no metric tensor, only native operators

---

## 1. Goal

Determine the continuum limit of the dual-object operators S (Signature) and G (Retarded
Propagator), and classify each against the d'Alembertian □ and the retarded Green function.

---

## 2. Results

### S → d'Alembertian? (G4-L100)

| observable | value |
|---|---|
| S spectrum | (27+, 45−) **indefinite** ✅ |
| S layer profile alternates | ✅ |
| H2 applied to harmonic t²+x² | **464.1** (true □ gives ≈ 0) ❌ |

S carries the **Lorentzian signature** (indefinite, alternating) but is a **UNIFORM-weight
alternating-layer operator** — it does *not* annihilate the harmonic, so it is **not the exact
d'Alembertian** (which requires the BDG binomial coefficients).

### G → retarded Green function? (G4-L101)

| observable | value |
|---|---|
| G future (anti-causal) entries | **0** |
| G past (causal) entries | 1344 |

G is **strictly retarded** (lower-triangular, causal), but its weights are UNIFORM (±1 alternation),
not the exact retarded d'Alembertian kernel.

### Wavefront (G4-L102)

| object | leakage | directionality |
|---|---|---|
| G (retarded) | 0.082 | 1.000 (causal) |
| S (signature) | 0.770 | 0.537 (symmetric/Feynman) |

---

## 3. Classification

| operator | continuum limit | classification |
|---|---|---|
| S | Lorentzian-signature operator (uniform-weight alternating layer) | **PARTIAL MATCH** (signature ✅, exact □ ❌) |
| G | retarded (causal) operator | **PARTIAL MATCH** (causality ✅, exact kernel ❌) |

---

## 4. Conclusion

The native dual-object formulation reproduces the **structure** of the continuum theory — S carries
the Lorentzian signature, G carries causality — but **not the exact operators**: both are
**uniform-weight** (±1) alternating-layer operators, whereas the d'Alembertian / retarded Green
function require the **BDG binomial coefficients**.

This confirms and sharpens the G4-L audit: the native operators converge to a Lorentzian-signature
operator and a retarded propagator, but the *final* step to the exact □ / retarded Green function is
blocked by the missing BDG binomial weights (outside the "no BDG coefficients" constraint). The
classification is **PARTIAL MATCH** for both S and G.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L100 `G4_L100_SignatureOperatorIsDAlembertian` | PASS (indefinite + alternating, but not exact □) |
| G4-L101 `G4_L101_RetardedPropagatorIsRetarded` | PASS (strictly retarded, uniform weights) |
| G4-L102 `G4_L102_WavefrontAndPropagationKernel` | PASS (G causal, S symmetric) |

Code: `TQM.Tests/ResearchXH/G4L_Phase10_ContinuumLimitTests.cs`.
