# AT-QG Phase 185 — Bekenstein Quarter Origin

**Status:** COMPLETE — **PARTIAL ORIGIN**
**Tests:** ATQG1850, ATQG1851, ATQG1852 (all passed)
**Core class:** `AT.Core/ResearchXH/BekensteinQuarterOrigin.cs`

---

## 1. Starting Point

Known: QG12 derives the horizon entropy S ∝ R^(d−1) (area) from boundary
counting. QG184 derives the mass-radius relation M ∝ R and the temperature
scaling T ∝ 1/R from the per-octave deficit.

**Open problem:** Can the EXACT Bekenstein-Hawking coefficient 1/4 in
S = A/4 be derived from TRM/D96 — no imported normalization, deterministic?

---

## 2. Method

1. **Structure audit** — confirm that the three structural results are
   derived: the area law S ∝ R^(d−1) (QG12), the radius-proportional mass
   M ∝ R (QG184), and the inverse-radius temperature T ∝ 1/R (QG184).
2. **The deficit first-law coefficient** — with the Schwarzschild deficit
   normalization GM = R/2 (m₀/(d·L·ρ̄) = 1/2) and the QG184 temperature
   T = 1/((d−1)·R^(d−2)) = 1/(2R) at d = 3, the first law
   S = ∫d(GM)/T gives S = R²/2 = A_cell/2 — coefficient **1/2** in cell
   units, or **1/(8π)** in physical area units (A = 4πR²).
3. **The 2π gap** — the deficit temperature is the SURFACE GRAVITY
   κ = 1/(2R). The Bekenstein-Hawking temperature is T = κ/(2π) =
   1/(8πM). The missing factor between the deficit entropy (1/2) and the
   Bekenstein entropy (1/4) is exactly **2π** — the QUANTUM factor
   (Unruh/Hawking), which is not present in the D96/TRM classical
   structures.
4. **Is the 2π derivable from D96?** — check the natural candidates:
   span/(2π) = 1.019 ≠ 1. The D96 span is NOT the quantum 2π, so the 2π
   cannot be imported from the spectrum.
5. **Candidate 1/occ₀ = 1/4** — occ₀ = 4 (lightest-octave occupancy) gives
   1/occ₀ = 1/4 exactly, but this is a numerical identity of the label 4,
   not a derived counting rule.

---

## 3. Results

### 3.1 The Structure Is Fully Derived

```
S(2R)/S(R) = 4 = 2^(d−1)          → area law (QG12)          ✓
GM(R)/GM(R/2) = 2                  → mass ∝ radius (QG184)    ✓
T(R)/T(R/2) = 1/2                  → temperature ∝ 1/R (QG184)✓
```

### 3.2 The Deficit First-Law Coefficient Is 1/2, Not 1/4

```
R = 2:  S_deficit = R²/2 = 2.0000   (= A_cell/2)
        A_phys = 4πR² = 50.2655
        S_target = A/4  = 12.5664
        → deficit coefficient = 1/2 (cell units) = 1/(8π) (physical units)
```

The deficit first law gives a DEFINITE coefficient, but it is the surface
gravity answer, not the quantum answer.

### 3.3 The 2π Quantum Gap

```
Deficit T = κ = 1/(2R)            (surface gravity)
Hawking T = κ/(2π) = 1/(8πM)      (quantum, Unruh/Hawking)
Ratio     = 2π
→ S_deficit = A_cell/2  vs  S_BH = A/4 = πR²   (off by 2π)

span/(2π) = 1.0194  ≠ 1          → D96 span is NOT the quantum 2π
```

The exact Bekenstein-Hawking S = A/4 requires the 2π quantum factor
T = κ/(2π), which is not derivable from the D96/TRM structures as they
stand.

### 3.4 The occ₀ = 4 Candidate Is Numerological

```
occ₀ (lightest octave) = 4
1/occ₀                  = 1/4    ← numerically EXACT
```

But there is no mechanism connecting the lightest-octave occupancy to the
first-law entropy per cell. It is a label identity, not a derivation. It is
reported here as a numerical correspondence, not as an origin.

---

## 4. Dependency Structure

```
D96 / TRM (derived)
 ├── boundary counting (QG12)            → S ∝ R^(d−1)      (area)
 ├── per-octave deficit (QG184)          → M ∝ R
 ├── first law (QG184)                   → T ∝ 1/R          (surface gravity)
 └── deficit first law (QG185)           → S = R²/2 = A_cell/2

imported (NOT derived from D96/TRM)
 └── 2π quantum factor T = κ/(2π)        → S = A/4  (Unruh/Hawking)
```

---

## 5. Classification

- **NO ORIGIN** rejected: the structure (area law, M ∝ R, T ∝ 1/R) is
  fully derived and the deficit first law gives a definite coefficient.
- **QUARTER ORIGIN** rejected: the exact 1/4 is NOT reproduced without the
  2π quantum factor (T = κ/(2π)), which is not in D96/TRM; the candidate
  1/occ₀ = 1/4 (occ₀ = 4) is a numerical identity without a mechanism.
- **PARTIAL ORIGIN** accepted: the area-law STRUCTURE is derived (S ∝ A,
  M ∝ R, T ∝ 1/R) and the deficit first law produces a definite coefficient
  (1/2 in cell units = 1/(8π) physical), but the exact Bekenstein-Hawking
  quarter requires the 2π quantum factor, which the framework does not
  provide.

**Result: PARTIAL ORIGIN**

---

## 6. Interpretation & Caveats

- This phase is an honest negative result at the coefficient level: the
  entire structural chain (S ∝ A, M ∝ R, T ∝ 1/R) is derived with no new
  primitives, but the exact 1/4 coefficient is a quantum (2π) statement
  that D96/TRM — as currently formulated — does not contain.
- The deficit first law yields a *definite* entropy coefficient (1/2 in cell
  units, 1/(8π) in physical units). The discrepancy with 1/4 is exactly
  the 2π quantum factor in T = κ/(2π).
- The D96 span is 1.9% away from 2π — close enough to be suggestive, not
  exact enough to be a derivation. It is recorded as an open numerical
  correspondence.
- 1/occ₀ = 1/4 (occ₀ = 4) is numerically exact but mechanistically
  unjustified; it is reported only as a numerical identity.
- As always, internal consistency of the counting-measure framework does not
  by itself prove physical correctness.

---

## 7. Open Question

Derive (or reject) the 2π quantum factor from D96/TRM — e.g. from the
octave-ladder structure, the deficit counting rule, or a phase-space
normalization — to convert PARTIAL ORIGIN into QUARTER ORIGIN. Until then,
the exact Bekenstein coefficient remains the one imported element of the
black-hole entropy program.
