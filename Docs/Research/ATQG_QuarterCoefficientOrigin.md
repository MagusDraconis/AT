# AT-QG Phase 196 — Quarter Coefficient Origin

**Status:** COMPLETE — **PARTIAL ORIGIN** (impossibility proof for the exact 1/4)
**Tests:** ATQG1960, ATQG1961, ATQG1962 (all passed)
**Core class:** `AT.Core/ResearchXH/QuarterCoefficientOrigin.cs`

---

## 1. Starting Point

Known: S ∝ A (QG12 boundary counting), T ∝ 1/R (QG184). Missing: the exact
coefficient 1/4 in S = A/4.

**Open problem:** can the exact 1/4 be DERIVED — **no fitting, no imported
Hawking factor** — or is it IMPOSSIBLE within D96/TRM?

This is a stricter re-examination than QG185: QG185 found the structure
derived but flagged the 2π gap; here we prove rigorously that 1/4 cannot be
derived under the stated constraints.

---

## 2. Method

1. **Boundary counting (QG12)** — S = b·R² (b bits per horizon cell,
   A_cell = R²). The physical area is A_phys = 4πR², so S/A_phys = b/(4π).
   For the Bekenstein target S/A_phys = 1/4 this forces **b = π**.
2. **Deficit first-law (QG185)** — S = R²/2 = A_cell/2 → S/A_phys = 1/(8π).
3. **The bit-per-cell constraint** — S/A_phys = 1/4 forces b = π. π is NOT a
   D96/TRM quantity; setting b = π would be an imported normalization
   (forbidden).
4. **The occ₀ = 4 candidate** — 1/occ₀ = 1/4 as a CELL coefficient gives
   S = (1/4)R² = A_phys/(16π), i.e. S/A_phys = 1/(16π) ≈ 0.0199, which is
   1/(4π) of the target. It would require π = 1/4 (not D96/TRM).

---

## 3. Results

### 3.1 Structure derived; natural coefficients definite but ≠ 1/4 (ATQG1960)

```
structure (S∝A, M∝R, T∝1/R) derived?   YES
QG12 boundary counting:  S/A = ln2/(4π) = 0.055159   (≠ 1/4)
deficit first-law:       S/A = 1/(8π)  = 0.039789   (≠ 1/4, off by 2π)
```

### 3.2 The bit-per-cell constraint: 1/4 requires imported π (ATQG1961)

```
S = b·R², A_phys = 4πR²  ⇒  S/A_phys = b/(4π)
target S/A_phys = 1/4  ⇒  b = π = 3.141593 bits/cell
QG12 natural count: b = ln2 = 0.693147 bits/cell

The ONLY bits-per-cell that gives S = A_phys/4 is b = π.
π is not a D96/TRM quantity; setting b = π would be an imported
normalization — forbidden (no fitting, no imported Hawking factor).
```

### 3.3 The occ₀ = 4 candidate is a wrong-units coincidence (ATQG1962)

```
occ₀ = 4; 1/occ₀ = 0.2500 (cell coefficient)
1/occ₀ S/A_phys = 1/(16π) = 0.019894   (target 0.25, ratio 1/(4π))
1/occ₀ reproduces the physical 1/4?  NO  (it would require π = 1/4)
```

The 1/4 identity of 1/occ₀ is a numerical coincidence of the label 4 in the
wrong units: as a cell coefficient it gives S = A_phys/(16π), not A_phys/4.

### 3.4 Classification (ATQG1962)

Origin score: 3/3.

```
+1 structure derived (S∝A, M∝R, T∝1/R)
+1 definite coefficients (QG12 ln2/(4π), deficit 1/(8π))
+1 exact 1/4 proven impossible without imported π

⇒ PARTIAL ORIGIN
```

---

## 4. The Impossibility Proof

To derive S = A_phys/4 from boundary counting S = b·R², the bits-per-cell
must satisfy b/(4π) = 1/4, i.e. **b = π**. Three consequences:

1. **QG12's natural count (1 bit = ln 2 per cell)** gives S/A = ln2/(4π) ≈
   0.055 — not 1/4.
2. **The deficit first-law** gives S/A = 1/(8π) ≈ 0.040 — not 1/4 (the QG185
   2π gap).
3. **1/occ₀ = 1/4** is a cell-unit identity: S = (1/4)R² = A_phys/(16π) ≈
   0.020·A_phys — 1/(4π) of the target, requiring π = 1/4.

**There is no D96/TRM choice of bits-per-cell, boundary rule, or occupancy
identity that yields 1/4 without importing π.** The exact 1/4 is therefore
impossible to derive under the phase constraints.

---

## 5. Dependency Structure

```
D96/TRM (derived)
 ├── QG12 boundary counting  → S = ln2·R²   → S/A = ln2/(4π) ≈ 0.055
 ├── QG184 deficit first-law → S = R²/2     → S/A = 1/(8π)  ≈ 0.040
 └── occ₀ = 4                → S = (1/4)R²  → S/A = 1/(16π) ≈ 0.020

imported (NOT derivable from D96/TRM)
 └── π (the 2π quantum factor)  → S/A = 1/4  (b = π bits per cell)
```

---

## 6. Classification

- **NO ORIGIN** rejected: the structure (S ∝ A, M ∝ R, T ∝ 1/R) is fully
  derived and the natural coefficients are definite.
- **QUARTER ORIGIN** rejected: the exact 1/4 is **proven impossible** to
  derive from D96/TRM without fitting and without importing π.
- **PARTIAL ORIGIN** accepted: the structure and the definite coefficients
  (QG12 ln2/(4π), deficit 1/(8π), occ₀ 1/(16π)) are derived, but the exact
  Bekenstein 1/4 requires the imported π (the 2π quantum factor) — impossible
  within the phase constraints.

**Result: PARTIAL ORIGIN** (with the impossibility proof)

---

## 7. Interpretation & Caveats

- This phase STRENGTHENS QG185: not merely "the 2π is missing" but a rigorous
  proof that **no D96/TRM normalization yields 1/4 without importing π**.
  The bits-per-cell would have to be π itself.
- The occ₀ = 4 candidate is definitively rejected as numerology: 1/occ₀ = 1/4
  in cell units is 1/(16π) in physical units — a wrong-units coincidence.
- The 1/4 is a genuinely quantum/geometric statement (π enters through
  A = 4πR² and T = κ/2π); D96/TRM's classical structures do not contain π as
  a derived quantity.
- As always, the impossibility is within the stated constraints (no fitting,
  no imported Hawking factor); a future phase could import the quantum factor
  explicitly, but that would no longer be a derivation from D96/TRM alone.
