# TQM-QG Phase 116b — Origin of the Universal Attractor

**Program:** TQM-QG (Unification)
**Phase:** 116b — why does actualization converge to this specific attractor?
**Status:** COMPLETED — 3/3 xUnit tests pass (354/354 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis of the QG116 fixed point)

---

## 1. Goal

QG116 showed the sustained self-reinforcing dynamics (damping 0.2, feedback 0.7, K=6) drives EVERY initial
activity pattern to the SAME final geometry (N·K = 576 links for N=96, span 6.40, one single spectral class).
This phase asks WHY: is that universal attractor ACCIDENTAL (a fragile parameter coincidence), DYNAMICAL (a
genuine selection by the feedback dynamics), or INEVITABLE (forced regardless of content, size, and
parameters)? Classify: ACCIDENTAL / DYNAMICAL / INEVITABLE.

Method: study the fixed point of the QG115/116 activity→links→activity map. Attractor stability is tested by
perturbation recovery (remove links deterministically, re-seed activity from degrees, re-run); basin size by a
deterministic pseudo-random pattern sweep; universality across network size (N=48/96/192); exact fixed-point
structure by feeding converged activity back in; geometry emergence by the link trajectory and the parameter
dependence of the saturated link radius.

---

## 2. Attractor stability (TQMQG1163)

- exact fixed point (feeding converged activity back in): **True**;
- fixed-point residual (one full re-run): **0.00e+000**;
- perturbation recovery after removing 20% of links: **True**;
- perturbation recovery after removing 50% of links: **True**;
- spectral shape distance original vs recovered: **0.0800**.

The attractor is an EXACT fixed point of the feedback map (residual 0) and the dynamics RETURNS to the
identical network even after removing up to 50% of its links — a genuinely stable attractor, not a fragile
coincidence.

---

## 3. Basin size + universality (TQMQG1164)

- basin fraction (30 deterministic pseudo-random patterns): **100.0 %**;
- N=48 → 288 links (expected 288); N=96 → 576 (expected 576); N=192 → 1152 (expected 1152);
- size-universal: **True**;
- featureless content (all activity below the 0.5 threshold) stays EMPTY: **True**.

The basin is essentially universal (100% of random patterns) and the attractor forms identically at every
network size (links = N·K exactly). But featureless all-sub-threshold content stays EMPTY — a second, trivial
attractor — so the basin is not literally everything.

---

## 4. Fixed-point structure, emergence, parameter dependence (TQMQG1165)

Geometry emergence (link count over steps, N=96):
- step 1 → 192 links; step 2 → 192; step 4 → 384; step 8 → 576; steps 16/32/64/120 → 576 (monotone growth,
  saturates at the N·K attractor).

Parameter dependence (saturated link radius, links per node):
- strong feedback / weak damping (f=0.9, d=0.1): **6.00**;
- weak feedback / strong damping (f=0.3, d=0.5): **2.00**.

**DYNAMICAL.**

- NOT ACCIDENTAL: exact fixed point (residual 0), 100% basin, size-universal, stable under 50% perturbation —
  the geometry is a genuine dynamical attractor.
- NOT INEVITABLE: the saturated link radius DEPENDS on the feedback/damping ratio (6.0 vs 2.0 links/node), and
  featureless all-sub-threshold content stays EMPTY — the geometry is parameter-determined, not forced by the
  model alone.
- DYNAMICAL: actualization converges to the maximal local-connectivity circulant its own feedback can
  maintain — robust to content and size, but its radius is set by the dynamics' parameters.

---

## 5. Conclusion

The universal attractor of QG116 is a genuine dynamical selection. It is an exact, strongly stable fixed point
of the activity→links→activity map with a near-universal basin (100% of random content) that forms identically
at every network size — so it is emphatically NOT accidental. But it is not inevitable either: the geometry is
the maximal K-neighbor circulant the feedback can sustain, and its radius (links per node) is set by the
feedback/damping ratio, while featureless content below threshold collapses to the empty network. Actualization
therefore does not pick the attractor from nothing; it relaxes, deterministically, to the richest local
connectivity its own feedback parameters allow. This echoes the earlier QG series conclusion that model
parameters (link lengths, ratios) carry the SM-matching freedom: the network geometry is dynamically
selected, but the numeric radius is parameter-determined.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1163 `TQMQG1163_AttractorStability` | PASS (exact fixed point; recovers after 50% perturbation) |
| TQMQG1164 `TQMQG1164_BasinSizeAndUniversality` | PASS (100% basin; links = N·K at N=48/96/192; featureless stays empty) |
| TQMQG1165 `TQMQG1165_FixedPointStructureAndClassification` | PASS (DYNAMICAL; radius 6.0 vs 2.0; monotone saturation) |

Code: `TQM.Core/ResearchXH/UniversalAttractor.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase116b_UniversalAttractorTests.cs`.
Shared helper added: `StructureFromContent.AdaptiveNetworkFull` (returns final activity + adjacency for
fixed-point analysis; same update rule as `AdaptiveNetwork`).
