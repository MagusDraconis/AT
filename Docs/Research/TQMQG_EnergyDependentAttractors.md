# TQM-QG Phase 122 — Energy Dependence of Attractor Classes

**Program:** TQM-QG (Unification)
**Phase:** 122 — can higher actualization-energy regimes generate new attractor classes not accessible in the
current parameter range?
**Status:** COMPLETED — 3/3 xUnit tests pass (372/372 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis using QG89 energy = actualization rate)

---

## 1. Goal

QG89 derived energy = actualization rate (Q-event activity); QG117 showed the (feedback, damping) plane maps
to a discrete ladder of attractor geometry classes (radius ≤ K = 6). This phase asks: can HIGHER
actualization-energy regimes generate NEW attractor classes not accessible in the current parameter range?
Classify: NO EFFECT / PARTIAL EFFECT / NEW CLASSES.

Method: the QG115/116 dynamics clamps activity to a ceiling (default 1.0), so the saturated activity fixed
point a* = min(ceiling, f/d) bounds the link radius k = round(a*·K) ≤ K. We raise the ceiling (the energy
regime), scale the seed energy, count spectral classes and octave families, and test whether classes exist
beyond the baseline range cap.

---

## 2. Energy scaling + actualization-rate regimes (TQMQG1220)

Radius vs seed energy scale (baseline ceiling 1.0):
- E=0.25 → 0.00; E=0.50 → 0.00; E=1.00 → 6.00; E=2.00 → 7.67; E=4.00 → 11.67; E=8.00 → **22.00**.

Radius ladder by energy ceiling (actualization-rate regime):
- ceiling 1.0 → [2.00, 6.00];
- ceiling 1.5 → [2.00, 2.67, 3.00, 9.00];
- ceiling 2.0 → [2.00, 2.67, 9.33, 11.33, 12.00];
- ceiling 4.0 → [2.00, 2.67, 9.33, 11.33, 13.67, 15.33, 17.33, **19.67**];
- ceiling 8.0 → same as 4.0 (saturates).

Raising the seed energy scale grows the attractor radius (0 → 22 as E goes 0.25 → 8), and raising the activity
ceiling extends the radius ladder from {2, 6} (baseline) to radii as large as 19.67. Energy controls which
attractor classes are accessible.

---

## 3. Phase transitions + family-count evolution (TQMQG1221)

Attractor phase transitions (distinct spectral classes vs energy ceiling):
- ceiling 1.0 → 2; 1.5 → 4; 2.0 → 5; 4.0 → **8**; 8.0 → 8 (saturates).

Family-count evolution (f=0.7, d=0.3):
- ceiling 1.0: radius 6.00, families **3**, span 6.40;
- ceiling 1.5: radius 9.00, families 3, span 4.39;
- ceiling 2.0: radius 12.00, families 2, span 3.34;
- ceiling 4.0: radius 13.67, families 2, span 2.98.

The number of accessible attractor phases GROWS sharply with the energy regime (2 classes at ceiling 1 → 8 at
ceiling 4). The octave-family count COMPRESSES at high energy (3 → 2 families; span 6.40 → 2.98) — higher
energy merges family structure while opening new geometry classes.

---

## 4. High-energy classes + classification (TQMQG1222)

- baseline max radius (ceiling 1.0): **6.00** (the K=6 cap);
- high-energy max radius (ceiling 4.0): **19.67**;
- classes exist beyond the baseline range: **True**.

**NEW CLASSES.**

- NOT NO EFFECT: energy strongly controls the attractor (radius 0→22 with seed energy; spectral class count
  2→8 across ceilings).
- NEW CLASSES: higher actualization-energy regimes OPEN attractor classes unreachable in the baseline regime —
  the radius ladder extends to 19.67 (vs the K=6 cap), so classes above the current range genuinely exist.
  (Family count compresses 3→2, so new geometry classes come with MERGED family structure.)

---

## 5. Conclusion

The attractor classes of QG117 are energy-dependent: raising the actualization-energy regime — modeled by the
activity ceiling, since energy = actualization rate (QG89) — opens NEW attractor geometry classes that are
unreachable at the baseline regime. The radius ladder extends from {2, 6} to radii as large as 19.67, and the
number of distinct spectral phases grows from 2 to 8 as the ceiling rises from 1.0 to 4.0 (saturating after
4.0). Higher energy also MERGES family structure (octave families 3 → 2, span 6.40 → 2.98): new high-energy
geometry classes carry fewer, wider families.

Physically: energy (actualization rate) acts as an order parameter over the attractor ladder — increasing
energy accesses increasingly rich network geometries. This is consistent with QG89 (energy = actualization
rate) and QG117 (discrete ladder), and gives the QG121 FUNDAMENTAL discreteness an energy-axis: the ladder is
discrete at every energy, but its RANGE grows with energy. The high-energy regime (radius > K) represents a
phase in which local connectivity exceeds the link-length parameter — a candidate analog for the SM hierarchy
probe at high energy (consistent with the QG118–120 family arc).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1220 `TQMQG1220_EnergyScalingAndRateRegimes` | PASS (radius 0→22 with seed energy; ladder to 19.67) |
| TQMQG1221 `TQMQG1221_PhaseTransitionsAndFamilyEvolution` | PASS (classes 2→8; families compress 3→2) |
| TQMQG1222 `TQMQG1222_HighEnergyClassesAndClassification` | PASS (NEW CLASSES; high-energy classes exist) |

Code: `TQM.Core/ResearchXH/EnergyDependentAttractors.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase122_EnergyDependentAttractorsTests.cs`.
Note: the prompt's test IDs (1210–1212) were already used by Phase 121; tests use TQMQG1220–1222 per convention.
