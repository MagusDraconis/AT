# AT-QG Phase 79 — Why SU(3)?

**Program:** AT-QG (Unification)
**Phase:** 79 — is SU(3) the minimal non-Abelian extension of the link?
**Status:** COMPLETED — 3/3 xUnit tests pass (240/240 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether SU(3) is the minimal non-Abelian extension of the network link. Classify: DERIVED / PREFERRED / NEW POSTULATE.

---

## 2. SU(2) vs SU(3) (ATQG790)

SU(2) (dimension 3) is the smallest non-Abelian Lie group and is already present as the spin structure S. SU(3)
(dimension 8) is NOT minimal in the abstract. Minimality alone does not select SU(3).

---

## 3. Color triplets, generator counting, confinement (ATQG791)

The color count N = 3 is an empirical input (baryon statistics force 3 colors), not a network output. GIVEN N = 3,
the maximal unitary determinant-1 group is SU(3), with N²−1 = 8 generators (8 gluons). Confinement is non-perturbative
(dynamical). The link's information capacity is ample (it already carries the full complex rank-2 object).

---

## 4. Classification (ATQG792)

**NEW POSTULATE.**

- NOT DERIVED: N = 3 is not a network output;
- PREFERRED (conditional): GIVEN N = 3 colors, SU(3) is unique/forced;
- NEW POSTULATE: the existence of exactly 3 colors is itself a new postulate; SU(3) follows once it is accepted.

---

## 5. Conclusion

Why SU(3)? Because **3 colors are postulated** (from baryon statistics), and **given 3 colors SU(3) is forced**. The
3-color count — not the group — is the new postulate.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG790 `ATQG790_Su2VsSu3` | PASS (SU(2) is minimal, not SU(3)) |
| ATQG791 `ATQG791_TripletsAndGenerators` | PASS (N=3 → SU(3), 8 gluons) |
| ATQG792 `ATQG792_Classification` | PASS (NEW POSTULATE) |

Code: `AT.Core/ResearchXH/WhySU3.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase79_WhySU3Tests.cs`.
