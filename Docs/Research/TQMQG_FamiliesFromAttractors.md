# TQM-QG Phase 118 — Families from Attractor Geometries

**Program:** TQM-QG (Unification)
**Phase:** 118 — can particle-family structure emerge from the different attractor geometry classes?
**Status:** COMPLETED — 3/3 xUnit tests pass (360/360 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis of the QG117 attractor classes)

---

## 1. Goal

QG117 showed the (feedback, damping) parameter plane maps to a discrete ladder of stable attractor geometries
(radius 2 and 6 links/node for K=6). This phase asks: can particle-FAMILY structure emerge from the different
attractor geometry classes? Classify: NO RELATION / PARTIAL RELATION / FAMILY ORIGIN.

Method: for each geometry class realized in the parameter plane, extract the class count, the internal
octave-family count (QG106 family structure — the discrete family-like content of each class), the sharpness
of class transitions, the internal hierarchy (low-mode successive ratios), and the stability of the family
counts under perturbation and network size.

---

## 2. Geometry-class count + family analogs (TQMQG1180)

Geometry classes (K=6, parameter plane):
- radius 2.0: families = **4**, span = **11.90**;
- radius 6.0: families = **3**, span = **6.40**.

Class counts across K:
- K=3: 2 classes, family counts [5,4]; K=4: 2 classes [5,4]; K=5: 2 classes [5,3]; K=6: 2 classes [4,3].

- distinct family counts (K=6): **[3, 4]**;
- a three-family geometry class exists (K=6): **True** (also at K=5).

The parameter plane yields a DISCRETE set of geometry classes (2 for every K), and the classes carry DISTINCT
internal family content (4 vs 3 octave families at K=6). A three-family class — the SM count — is realized for
K=5 and K=6.

---

## 3. Class transitions + hierarchy generation (TQMQG1181)

- max adjacent-point spectral sensitivity: **0.6211** (classes are sharply separated);
- distinct hierarchy spans per class: 11.90 (radius 2) vs 6.40 (radius 6) — distinct hierarchy depths;
- low-mode ratio ladders (first 4): radius-2 class [1.00, 1.92, 1.00, 1.40]; radius-6 class [1.00, 1.97,
  1.00, 1.47];
- low-mode ratio size-stability (N=48→192): radius-2 **0.0724**, radius-6 **0.0326** (nearly size-invariant
  mass-like ladders).

Classes are sharply separated and EACH class generates its own hierarchy (distinct spans, size-stable
low-mode ladders) — discrete classes with internal mass-like structure.

---

## 4. Stability of classes + classification (TQMQG1182)

- family counts stable under 10% link-removal perturbation: **True**;
- family counts stable across N=48/96/192: **False** (radius-2 class: 3→4→5 families; radius-6 class:
  2→3→4 as N grows);
- inter-class low-mode ratio deviation: **0.0178**.

**PARTIAL RELATION.**

- NOT NO RELATION: geometry classes DO carry distinct, perturbation-robust family content (4 vs 3 octave
  families at K=6; three-family class at K=5,6).
- NOT FAMILY ORIGIN: the octave family count is NOT a size-invariant property — it grows with the network
  (3→4→5 as N=48→96→192), so the discrete family number is not a fixed emergent constant of the class.
- PARTIAL RELATION: distinct stable geometry classes with class-dependent family structure partially emerge,
  but a size-independent discrete family spectrum is not achieved.

---

## 5. Conclusion

Attractor geometry classes do carry family-like content: distinct classes have distinct octave-family counts
and hierarchy depths, a three-family class exists at K=5,6, transitions between classes are sharp, and the
family counts are robust under perturbation. But the family structure is only PARTIALLY emergent: the octave
family count scales with the network size rather than being a fixed invariant of the class. The internal
low-mode hierarchy ladders, by contrast, ARE nearly size-invariant (deviations 0.03–0.07), suggesting that
the mass-like RELATIVE structure of a class is robust even though the total family COUNT is not.

This qualifies the QG117 ATTRACTOR ORIGIN result: parameters generate discrete stable geometry classes, and
those classes carry distinct family-analog content, but a clean size-independent discrete family spectrum
(the SM's 3 families) is not yet achieved — consistent with the ongoing program result that family-like
discreteness exists structurally (QG79/80, QG106–108) while the specific family numbers remain
parameter/size-dependent (QG109–117).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1180 `TQMQG1180_GeometryClassCountAndFamilyAnalogs` | PASS (2 classes; distinct family counts; three-family class) |
| TQMQG1181 `TQMQG1181_ClassTransitionsAndHierarchyGeneration` | PASS (sensitivity 0.62; distinct spans; size-stable ladders) |
| TQMQG1182 `TQMQG1182_ClassStabilityAndClassification` | PASS (PARTIAL RELATION; perturbation-robust, not size-invariant) |

Code: `TQM.Core/ResearchXH/FamiliesFromAttractors.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase118_FamiliesFromAttractorsTests.cs`.
