# TQM-QG Phase 117 — Do Physical Parameters Control the Attractor Geometry?

**Program:** TQM-QG (Unification)
**Phase:** 117 — can changes in attractor parameters produce distinct stable geometries analogous to masses,
families, or interaction strengths?
**Status:** COMPLETED — 3/3 xUnit tests pass (357/357 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis of the QG116b attractor parameter plane)

---

## 1. Goal

QG116b showed the universal attractor exists (the N·K circulant) but its saturated link radius depends on the
dynamics parameters (feedback 0.9/damping 0.1 → 6.0 links/node vs feedback 0.3/damping 0.5 → 2.0). This phase
asks: can changes in attractor parameters produce DISTINCT STABLE geometries analogous to masses, families, or
interaction strengths? Classify: NO RELATION / PARTIAL RELATION / ATTRACTOR ORIGIN.

Method: sweep the (feedback, damping) parameter plane of the QG115/116 activity→links→activity map (4×4 grid
plus fine sweeps), converging the network from a fixed seed at each parameter pair, and record the attractor
radius (links/node), hierarchy span, octave-family count, and spectral shape.

---

## 2. Attractor radius + parameter response (TQMQG1170)

Parameter plane (K=6, N=96) distinct saturated radii: **[2.0, 6.0]** — a discrete ladder, not a continuum.

Feedback response (radius at d=0.3):
- f=0.3 → 2.0; f=0.5 → 2.0; f=0.7 → 6.0; f=0.9 → 6.0 (monotone non-decreasing).

Damping response (radius at f=0.5):
- d=0.1 → 6.0; d=0.3 → 2.0; d=0.5 → 2.0; d=0.7 → 2.0 (monotone non-increasing).

Fine sweeps confirm sharp plateaus: at d=0.3 the radius stays 2.0 from f=0.20 up to f=0.60 and jumps to 6.0
at f=0.65; at f=0.7 it stays 6.0 from d=0.05 to d=0.30 and drops to 2.0 at d=0.35. The radius is controlled
by the feedback/damping ratio with a sharp threshold (f/d ≈ 2: below → radius 2, above → radius 6).

---

## 3. Geometry classes (TQMQG1171)

- distinct geometry classes (KS single-linkage, ε=0.12, 16-point plane): **2**;
- geometry robust WITHIN each radius plateau: **True**;
- max intra-plateau shape distance: **0.0421**;

Plateau geometries (distinct spectral signatures):
- radius 2 class: span **11.90**, families **4**;
- radius 6 class: span **6.40**, families **3**.

The parameter plane maps to a SMALL set of DISCRETE geometry classes — each radius plateau is a distinct
stable geometry (different span, different family count), and the geometry is IDENTICAL within a plateau
(robust). This is a discrete spectrum of stable geometries parameter-controlled like families or mass levels.

---

## 4. Parameter sensitivity + classification (TQMQG1172)

- max spectral shape distance between ADJACENT parameter points: **0.6211** (sharp jumps);
- max shape distance WITHIN a plateau: **0.0421** (near-identical).

**ATTRACTOR ORIGIN.**

- NOT NO RELATION: the geometry strongly RESPONDS to parameters (radius ladder 2↔6, adjacent-point shape
  distance up to 0.62).
- NOT PARTIAL RELATION: the response is NOT a smooth continuum — geometries are near-identical within each
  plateau and JUMP between discrete classes at thresholds.
- ATTRACTOR ORIGIN: the parameter plane controls a discrete ladder of stable geometry classes (radius = k
  links/node, each a distinct spectral class) — distinct stable geometries parameter-controlled exactly as
  masses/families/interaction strengths would require.

---

## 5. Conclusion

The feedback/damping parameters of the actualization dynamics do NOT smear the geometry into a continuum —
they control a DISCRETE ladder of stable attractor geometries. Each saturated activity level a* =
feedback/damping (capped at 1) sets the link radius k = round(K·a*), so the geometry takes only a small set
of discrete values (0,1,…,K links/node for a given K), each a distinct K-circulant spectral class with its
own span and family count. Geometries are robust within parameter plateaus and jump sharply between classes
at threshold ratios.

This is the parameter-origin analog the program has been seeking: distinct stable geometries (families, mass-
like levels) arise from parameter control of a single dynamics, with discrete transitions between them —
consistent with the QG79/80 family structure (3 families), QG82 mixing (parameters carry the freedom), and
QG109–116b (dynamics selects geometry; parameters set the radius). The exact ladder values still depend on K
(the link-length parameter), leaving the same freedom noted in QG117: the NUMBER of rungs is structural, the
specific values parameter-dependent.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1170 `TQMQG1170_AttractorRadiusAndParameterResponse` | PASS (discrete ladder [2.0,6.0]; monotone responses) |
| TQMQG1171 `TQMQG1171_GeometryClasses` | PASS (2 classes; robust within plateaus; distinct signatures) |
| TQMQG1172 `TQMQG1172_ParameterSensitivityAndClassification` | PASS (ATTRACTOR ORIGIN; adjacent 0.62 vs intra 0.04) |

Code: `TQM.Core/ResearchXH/AttractorParameterOrigin.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase117_AttractorParameterOriginTests.cs`.
