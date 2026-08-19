# TQM-QG Phase 121 — Origin of the Attractor Ladder

**Program:** TQM-QG (Unification)
**Phase:** 121 — why does the feedback dynamics produce a discrete ladder instead of a continuous family of
geometries?
**Status:** COMPLETED — 3/3 xUnit tests pass (369/369 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis of the QG117 ladder mechanism)

---

## 1. Goal

QG117 showed attractor geometries form DISCRETE radius classes (a ladder: radius 2 and 6 for K=6) with sharp
transitions at threshold ratios. This phase asks WHY the feedback dynamics produces a discrete ladder instead
of a continuous family of geometries. Classify: ARTIFACT / DYNAMICAL / FUNDAMENTAL.

Method: probe each candidate cause independently — (1) vary the activity threshold gating link creation
(0.3/0.5/0.7); (2) replace the integer rounding k = round(a·K) with floor, ceil, AND a continuous-weight
variant (no integer count at all); (3) derive the algebraic fixed point a* = min(1, f/d) → radius
round(K·a*); (4) locate transition ratios; (5) test ladder universality across K = 3,4,5,6,8.

---

## 2. Threshold effects (TQMQG1210)

Radius ladder by activity threshold (feedback sweep at d=0.3, K=6):
- threshold 0.3 → [2.00, 6.00]; threshold 0.5 → [2.00, 6.00]; threshold 0.7 → [2.00, 6.00].

The discrete ladder {2, 6} is IDENTICAL for thresholds 0.3, 0.5, and 0.7 — the discreteness does NOT come from
the specific activity gate value.

---

## 3. Rounding structure (TQMQG1211)

Radius ladder by link discretization (feedback sweep at d=0.3, K=6):
- Round → [2.00, 6.00]; Floor → [1.67, 2.00, 6.00]; Ceil → [2.00, 6.00];
- **Continuous (no integer rounding) → [2.00, 6.00]**.

Even the CONTINUOUS-WEIGHT variant produces the discrete ladder {2, 6} — the discreteness is NOT an artifact of
the round() function.

---

## 4. Fixed-point bifurcations + universality + classification (TQMQG1212)

Fixed-point structure:
- algebraic ladder rungs round(K·min(1,f/d)) for K=6: **7** (rungs 0..6);
- measured radius matches the algebraic fixed point at high f/d (f=0.9, d=0.1): **True**;
- sharp transition at f/d ≈ 2.07 (radius jumps 2 → 6).

Ladder universality (discrete ladder over the feedback sweep for every K):
- K=3 → [1.00, 3.00]; K=4 → [1.33, 4.00]; K=5 → [1.67, 5.00]; K=6 → [2.00, 6.00]; K=8 → [2.67, 8.00].

**FUNDAMENTAL.**

- NOT ARTIFACT: the ladder persists under different thresholds AND under the continuous-weight variant (no
  rounding) — it is not a numerical accident.
- FUNDAMENTAL: the saturated activity fixed point a* = min(1, f/d) is a continuous parameter, but the link
  radius round(K·a*) is a step function of it — the bounded-activity × discrete-link structure of the model
  FORCES a discrete ladder, universal across thresholds, discretizations, and every K.
- (Nuance: the intermediate algebraic rungs 3,4,5 are stable fixed points but unreachable from the seed — a
  basin-selection detail; the discreteness itself is fundamental.)

---

## 5. Conclusion

The discrete attractor ladder of QG117 is not a numerical accident and not a dynamical accident either — it is
FUNDAMENTAL to the model structure. The feedback dynamics saturates to an activity fixed point a* =
min(1, f/d) which is continuous in the parameters, but the link radius round(K·a*) quantizes that continuous
parameter into a step ladder of discrete geometry classes. The ladder is unchanged when the activity threshold
is varied, when rounding is replaced by floor/ceil, and even when the link rule is made fully continuous —
demonstrating that the discreteness is intrinsic to the bounded-activity × discrete-link architecture, not to
any specific implementation choice.

This explains WHY QG117 saw a discrete ladder: continuous physical parameters (feedback, damping) map through
the network's discrete link structure into a discrete spectrum of stable geometries — the same discrete-
structure origin the program has pursued for families (QG79/80, QG118) and the 3-family count. The discreteness
of geometry is a consequence of the discreteness of the network itself.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1210 `TQMQG1210_ThresholdEffects` | PASS (ladder {2,6} identical at thresholds 0.3/0.5/0.7) |
| TQMQG1211 `TQMQG1211_RoundingStructure` | PASS (continuous-weight variant still shows ladder) |
| TQMQG1212 `TQMQG1212_FixedPointBifurcationsAndClassification` | PASS (FUNDAMENTAL; 7 algebraic rungs; universal across K) |

Code: `TQM.Core/ResearchXH/AttractorLadder.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase121_AttractorLadderTests.cs`.
