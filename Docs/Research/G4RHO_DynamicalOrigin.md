# G4-RHO Phase 0 — Dynamical Origin of ρ

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-RHO)
**Phase:** 0 — what determines ρ itself?
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no new primitives

---

## 1. Goal

Nearly the entire gravity program follows from ρ (the counting measure). Here we ask what determines ρ
itself, testing abundance laws, actualization dynamics, conservation principles, attractor behavior, and
scale-free solutions — and whether the α=0 (log-deficit) hierarchy arises naturally.

---

## 2. Results

### (a) Scale-free actualization gives a continuum, not a unique profile (G4-RHO00)

Self-similar densities ρ ∝ r^s all give a **flat** rotation curve v² = |s|/d = const:

| s | a(3) | v²(3)=v²(9) | sign |
|---|---|---|---|
| −1.0 | +0.111 | 0.333 | repulsive |
| −0.5 | +0.056 | 0.167 | repulsive |
| +0.5 | −0.056 | 0.167 | attractive |
| +1.0 | −0.111 | 0.333 | attractive |

Scale-free-ness yields a **continuum**; the sign (attractive ⟺ s>0) flips at s=0. No unique profile.

### (b) Flux conservation selects a repulsive power law and rejects the log (G4-RHO01)

Steady-state actualization with a conserved flux F = ρ·v·r^(d−1) (v=const) is satisfied by ρ ∝ r^(−(d−1)) =
r⁻² (F = const exactly), but NOT by the log density ρ = ρ̄ + c·ln r (F grows 1 → 24.9 over r=1→4):

| density | F(r) conserved? | a(3) |
|---|---|---|
| ρ ∝ r⁻² | ✅ const | +0.222 (repulsive) |
| ρ = ρ̄ + c·ln r | ❌ grows | −0.031 (attractive) |

The **raw** actualization flux, if conserved, gives the **wrong** (repulsive) sector; the attractive
log-deficit density is not a steady state of flux conservation.

### (c) The scale-free field a ∝ 1/r is the symmetry, not a dynamics (G4-RHO02)

The flat rotation curve v² = r|a| = const ⟺ a ∝ 1/r ⟺ a(λr) = a(r)/λ (the unique scale-free field). It is
satisfied by EVERY power-law ρ ∝ r^s (a(9)/a(3) = 1/3 for all s), so scale-invariance gives flatness but not
uniqueness.

---

## 3. Classification: PREFERRED (α=0), not DERIVED from dynamics

- **Self-similarity** → a continuum of power laws (all flat, sign set by s) — no unique profile.
- **Conservation** → ρ ∝ r⁻² (repulsive) — the wrong sector; it rejects the log.
- **Scale-invariance** → flatness (a ∝ 1/r), but again no unique ρ.

The flat, *attractive* rotation curve (the matter sector) requires the **deficit** m = ρ̄ − ρ, whose unique
scale-invariant form is **α = 0** (the log). This is a **symmetry selection** — the log deficit is the unique
scale-invariant abundance law — not a dynamical attractor.

The **dynamical origin of ρ remains OPEN**: no conservation or attractor principle investigated here produces
the deficit (attractive) sector over the raw conserved flux (repulsive). Why actualization settles into the
deficit form is the program's #1 unresolved problem, matching the G4 reassessment.

---

## 4. Conclusion

ρ is not yet dynamically determined. Scale-free actualization yields a continuum of flat-rotation-curve power
laws; conservation favors the repulsive ρ ∝ r⁻²; only the scale-invariance *symmetry* of the field singles out
α = 0 (the log deficit) in the matter sector. α = 0 is therefore **PREFERRED** (unique scale-invariant), not
**DERIVED** — the dynamics that would select the attractive deficit sector over the repulsive raw flux have
not been found.

---

## Test program

| Test | Verdict |
|---|---|
| G4-RHO00 `G4_RHO00_ScaleFreeContinuum` | PASS (continuum of flat scale-free densities) |
| G4-RHO01 `G4_RHO01_ConservationRejectsLog` | PASS (conservation → repulsive ρ∝r⁻², rejects log) |
| G4-RHO02 `G4_RHO02_ScaleFreeFieldClassification` | PASS (PREFERRED, α=0 not dynamically derived) |

Code: `TQM.Core/ResearchXH/RhoDynamics.cs`;
tests `TQM.Tests/ResearchXH/G4RHO_Phase0_DynamicalOriginTests.cs`.
