# AT-QG Phase 87 — Role of Higher-Dimensional Network Structure

**Program:** AT-QG (Unification)
**Phase:** 87 — can unresolved SM structure live on faces/volumes rather than nodes/links?
**Status:** COMPLETED — 3/3 xUnit tests pass (264/264 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether unresolved Standard Model structure can live on faces (2-cells) or volumes (3-cells) rather than nodes/links. Classify: IRRELEVANT / COMPATIBLE / PREFERRED.

---

## 2. Faces & volumes (ATQG870)

Faces and volumes are DERIVED composites: a face is a closed cycle of links, a volume is a composite of faces.
Any structure on a face reduces to structure on its boundary links — higher cells add no independent degrees of
freedom.

---

## 3. Flux vs structure homes (ATQG871)

Curvature/magnetic flux legitimately lives on faces (derived from link holonomies). But the unresolved structure —
family index (QG81), color connection (QG78), Higgs scalar (QG84) — already has homes on nodes/links.

---

## 4. Classification (ATQG872)

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
| ATQG870 `ATQG870_FacesAndVolumesAreDerived` | PASS (no independent dof) |
| ATQG871 `ATQG871_FluxAndStructureHomes` | PASS (homes already on nodes/links) |
| ATQG872 `ATQG872_Classification` | PASS (IRRELEVANT) |

Code: `AT.Core/ResearchXH/FacesAndVolumes.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase87_FacesAndVolumesTests.cs`.
