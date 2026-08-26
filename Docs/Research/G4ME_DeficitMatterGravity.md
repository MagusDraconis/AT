# G4-ME Phase 1 — Deficit Matter Gravity

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-ME)
**Phase:** 1 — does the derived deficit matter reproduce Newton-like attraction?
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 G4-ME)
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Test whether the derived matter m = ρ̄ − ρ (the density deficit) produces the correct large-scale
attractive behavior, comparing acceleration sign, radial falloff, effective-mass profile, and
superposition against Newtonian expectation.

---

## 2. Results

### (a) Gaussian deficit (G4-ME10)

| x | a_AT | a_Newton |
|---|---|---|
| 0.2 | −0.17 | −0.23 |
| 0.6 | −0.34 | −0.72 |
| 1.0 | −0.05 | −0.93 |

- **Attractive** (a < 0) everywhere ✅
- **Falloff**: a_AT ∝ ∇m (exponential, localized); a_Newton ∝ −∫m (→ constant, long-range). The
  ratio |a_AT/a_Newton| shrinks outward — AT is localized.

### (b) Spherical deficit + superposition (G4-ME11)

- Compact spherical deficit: a ≈ 0 **inside and outside** (field ∝ ∇m = 0 in uniform regions) — no 1/r² exterior.
- Two deficits: each test particle is attracted toward its nearest void (localized superposition).

### (c) Extended halo (G4-ME12)

- a_AT(0.3) = −0.19, a_AT(1.0) = −0.03 (attractive, decaying);
- a_Newton(0.3) = −0.08, a_Newton(1.0) = −0.15 (attractive, growing toward the enclosed mass).
- Effective mass profile: AT ∝ ∇m (local), Newton ∝ ∫m (enclosed).

---

## 3. Classification: **PARTIAL MATCH**

| Newtonian feature | AT deficit matter |
|---|---|
| attractive sign | ✅ (a < 0 toward deficits) |
| long-range 1/r² falloff | ❌ (localized ∝ ∇m, zero outside) |
| enclosed-mass profile | ❌ (gradient profile, not integral) |
| superposition | ✅ (local, toward nearest deficit) |

The derived deficit matter is **attractive** (resolving the G4-O repulsion), but it is a **localized**
(short-range) gravity — it does **not** reproduce Newton's long-range 1/r² falloff. Full Newtonian
gravity would require an additional non-conformal (long-range) sector.

---

## 4. Conclusion

The deficit matter correctly fixes the **sign** of gravity (attractive) but not the **range** (localized
vs 1/r²). It is a genuine, native resolution of the repulsion, but it is a **short-range** gravity: the
field is proportional to the density gradient ∇m, vanishing wherever the deficit is uniform. Reproducing
Newtonian long-range attraction would require either a different observable mapping or an additional
non-conformal primitive — which remains an open question of the G4-ME program.

---

## Test program

| Test | Verdict |
|---|---|
| G4-ME10 `G4_ME10_GaussianDeficitAttractiveFalloff` | PASS (attractive, localized falloff) |
| G4-ME11 `G4_ME11_SphericalDeficitAndSuperposition` | PASS (no exterior field; local superposition) |
| G4-ME12 `G4_ME12_ExtendedHaloAndClassification` | PASS (PARTIAL MATCH) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added `SphericalDeficit`, `NewtonianDeficitAcceleration`);
tests `AT.Tests/ResearchXH/G4ME_Phase1_DeficitMatterGravityTests.cs`.
