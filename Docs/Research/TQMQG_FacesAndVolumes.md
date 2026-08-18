# TQM-QG Phase 87 — Role of Higher-Dimensional Network Structure

**Program:** TQM-QG (Unification)
**Phase:** 87 — can unresolved SM structure live on faces/volumes rather than nodes/links?
**Status:** COMPLETED — 3/3 xUnit tests pass (264/264 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether unresolved Standard Model structure can live on faces (2-cells) or volumes (3-cells) rather than nodes/links. Classify: IRRELEVANT / COMPATIBLE / PREFERRED.

---

## 2. Faces & volumes (TQMQG870)

Faces and volumes are DERIVED composites: a face is a closed cycle of links, a volume is a composite of faces.
Any structure on a face reduces to structure on its boundary links — higher cells add no independent degrees of
freedom.

---

## 3. Flux vs structure homes (TQMQG871)

Curvature/magnetic flux legitimately lives on faces (derived from link holonomies). But the unresolved structure —
family index (QG81), color connection (QG78), Higgs scalar (QG84) — already has homes on nodes/links.

---

## 4. Classification (TQMQG872)

**IRRELEVANT.**

- IRRELEVANT: higher cells are derived (no independent dof), so they cannot resolve structure already on nodes/links;
- NOT PREFERRED: no reason to move family/color/mass onto higher cells;
- COMPATIBLE (subordinate): faces host derived curvature/flux, but not new SM structure.

---

## 5. Conclusion

Higher-dimensional cells are **IRRELEVANT** for the unresolved SM structure.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG870 `TQMQG870_FacesAndVolumesAreDerived` | PASS (no independent dof) |
| TQMQG871 `TQMQG871_FluxAndStructureHomes` | PASS (homes already on nodes/links) |
| TQMQG872 `TQMQG872_Classification` | PASS (IRRELEVANT) |

Code: `TQM.Core/ResearchXH/FacesAndVolumes.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase87_FacesAndVolumesTests.cs`.
