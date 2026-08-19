# TQM-QG Phase 119 — Local vs Global Attractor Classes

**Program:** TQM-QG (Unification)
**Phase:** 119 — do local observers sample only a subset of the network's attractor classes?
**Status:** COMPLETED — 3/3 xUnit tests pass (363/363 TQM-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis of the QG118 family-count scaling)

---

## 1. Goal

QG118 showed the octave-family count of each attractor geometry class SCALES with the network (radius-2
class: 3→4→5 families as N=48→96→192). This phase asks: do LOCAL observers — who can only sample a finite
subregion (horizon) of the network — see a subset of the network's attractor classes? Classify: EXACT MATCH /
LOCAL SUBSET / HIDDEN CLASSES.

Method: compare the geometry-class ladder reachable from the full parameter plane at global sizes
N=48/96/192 vs at local horizons n_local=16/24/32 (rings of that size run through the same dynamics), then
compare the octave-family count of the whole network vs a FIXED local window embedded in growing networks.

---

## 2. Local accessibility + global spectrum (TQMQG1190)

Global attractor spectrum (saturated radii over the parameter plane):
- N=48 → [2.0, 6.0]; N=96 → [2.0, 6.0]; N=192 → [2.0, 6.0] (size-invariant).

Local attractor accessibility (radius ladder reachable at each horizon):
- horizon 16 → [2.25, 6.00]; horizon 24 → [2.00, 6.00]; horizon 32 → [2.06, 6.00].

The geometry-class ladder ({2, 6} for K=6) is IDENTICAL at every global size and FULLY ACCESSIBLE to every
local horizon (16/24/32) — local observers can reach every global geometry class. (The small 2.25/2.06 vs
2.00 deviations are finite-size distortions of the same radius rung, within tolerance.)

---

## 3. Hidden classes + suppression (TQMQG1191)

Hidden stable classes (global rungs unreachable at each horizon): all horizons → hidden = [] (no hidden
classes).

Suppression of higher families (does the local family count saturate below total?):
- horizon 16: suppressed=True; horizon 24: suppressed=True; horizon 32: suppressed=True.

NO geometry class is hidden (all rungs accessible at every horizon), but the locally observable FAMILY COUNT
is suppressed — higher octave families grow beyond the local horizon, so local observers see only a subset of
the total family content.

---

## 4. Observable vs total families + classification (TQMQG1192)

Observable vs total families (fixed horizon 24 embedded in growing networks):
- N=48: total 2, local 2; N=96: total 3, local 2; N=192: total 4, local 2.

**LOCAL SUBSET.**

- NOT HIDDEN CLASSES: the geometry-class ladder is fully locally accessible (no hidden stable classes — all
  radius rungs reachable at every horizon).
- NOT EXACT MATCH: the total family count GROWS with the network (QG118 scaling: 2→3→4) while the local
  window's family count saturates — local observers see fewer families.
- LOCAL SUBSET: local observers sample a strict subset of the network's family spectrum — the higher octave
  families are suppressed beyond the local horizon.

---

## 5. Conclusion

Local observers do NOT lose any geometry class — the full attractor ladder is locally accessible. What they
lose is FAMILY CONTENT: the octave-family count of a fixed local window saturates (2 families at horizon 24)
while the total network family count keeps growing (2→3→4 as N=48→96→192). The higher octave families that
QG118 showed appearing at large N are suppressed beyond the local horizon — they are real global structure
that no local observer can see.

This has a sharp physical reading consistent with the program's family arc: if the observable universe were a
local horizon inside a larger network, the locally-observed family count would be a fixed small number (2–3)
regardless of the global network size — the higher "families" of QG118 are global (hidden from local view),
while the low octave families (including the SM's 3-family structure at K=5/6, QG118) are the locally
observable ones. The observable family spectrum is a LOCAL SUBSET of the total, with the higher classes
suppressed beyond the horizon.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG1190 `TQMQG1190_LocalAccessibilityAndGlobalSpectrum` | PASS (global {2,6} size-invariant; all horizons reach all rungs) |
| TQMQG1191 `TQMQG1191_HiddenClassesAndSuppression` | PASS (no hidden classes; higher families suppressed at all horizons) |
| TQMQG1192 `TQMQG1192_ObservableVsTotalAndClassification` | PASS (LOCAL SUBSET; total grows, local saturates) |

Code: `TQM.Core/ResearchXH/LocalVsGlobalAttractors.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase119_LocalVsGlobalAttractorsTests.cs`.
