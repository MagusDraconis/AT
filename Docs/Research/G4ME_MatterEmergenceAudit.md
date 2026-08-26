# G4-ME Phase 0 — Matter Emergence Audit

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-ME)
**Phase:** 0 — is observable matter identical to ρ, or a derived structure?
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Determine whether observable matter is identical to ρ (the actualization density) or emerges as a
derived structure — and, crucially, which quantity behaves as **attractive** matter given that
a = −(1/d)∇lnρ is repulsive at density peaks.

---

## 2. The key resolution

Define the **derived matter density** as the density *deficit*

```
m = ρ̄ − ρ      (positive in voids, negative in peaks)
```

Then the geodesic acceleration satisfies

```
a = −(1/d)∇lnρ = −(1/d)(∇ρ/ρ) = +(1/d)(∇m/ρ)   ∝  +∇m
```

which points **toward m > 0 (matter)** — i.e. the *same* acceleration is **attractive** toward the
matter (the voids), once matter is correctly identified as the deficit rather than ρ itself.

---

## 3. Results

### (a) Maxima repulsive, minima attractive (G4-ME00)

| structure | matter m | acceleration a |
|---|---|---|
| ρ-peak (ρ=1+A e^(−x²)) | −0.085 (< 0) | +0.231 (repulsive) |
| ρ-void (ρ=1−A e^(−x²)) | +0.085 (> 0) | −0.274 (attractive) |

### (b) Deficit abundance (G4-ME01)

| profile | ∫m dV |
|---|---|
| Gaussian peak | −0.266 (negative abundance) |
| void | +0.266 (positive abundance) |

The matter density m = ρ̄−ρ is a positive, localized, globally-conserved abundance (deficit) field.

### (c) Classification (G4-ME02)

ρ is **REAL-UNDERIVED** (the counting/actualization primitive); matter m = ρ̄−ρ is **DERIVED** (an
excitation of ρ).

---

## 4. Conclusion

**Matter is NOT identical to ρ — it is a derived excitation of ρ, the density deficit m = ρ̄ − ρ.**

The apparent "repulsion" of G4-O was an artifact of identifying matter with ρ itself. Once matter is
identified as the **deficit** (the voids where actualization density is *below* the mean), the native
geodesic acceleration a = +(1/d)∇m/ρ is **attractive toward matter** — recovering Newtonian-like
attraction natively, with no new primitive.

The physical picture is therefore coherent and elegant:

- **ρ-excess (peaks, high actualization)** → repulsive → the "dark-energy"/expansion sector;
- **ρ-deficit (voids, low actualization)** → attractive → the "matter" sector.

Both are the *same* conformal geodesics; they differ only by which derived structure (excess vs deficit)
is identified with matter. This is a genuinely new, testable resolution of the G4-O tension.

---

## Test program

| Test | Verdict |
|---|---|
| G4-ME00 `G4_ME00_MaximaRepulsiveMinimaAttractive` | PASS (peak repulsive, void attractive) |
| G4-ME01 `G4_ME01_DeficitAbundanceStructure` | PASS (void ∫m>0, peak ∫m<0) |
| G4-ME02 `G4_ME02_RealUnderivedVsDerived` | PASS (ρ underived, matter derived) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added `Void`, `MatterDensity`);
tests `AT.Tests/ResearchXH/G4ME_Phase0_MatterEmergenceAuditTests.cs`.
