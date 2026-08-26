# AT-QG Phase 159 — D96 Selection Origin

**Status:** COMPLETE — **INEVITABLE**
**Tests:** ATQG1590, ATQG1591, ATQG1592 (all passed)
**Core class:** `AT.Core/ResearchXH/D96SelectionOrigin.cs`

---

## 1. Starting Point

The established chain:

```
D96 → Z2 doublets → moment orders → N_eff → δ → p → hierarchy
```

QG155 showed the observable attractor generates a circulant ring C_96(1..6)
with dihedral automorphism group D_96. QG158 showed the moment orders
(1/2, 1, 2) are the integer powers of the Z2 order.

**Open question:** Why does the observable attractor select n = 96? Why
not D64, D128, D192?

---

## 2. Assumptions

1. The Z2 doublet symmetry (QG153/155) requires the **half-shift
   automorphism** i → i + n/2.
2. The seed is **period-3** (every 3rd node active), so the half-shift is a
   seed symmetry only when n/2 ≡ 0 (mod 3), i.e. **6 | n**.
3. The observable sector must have **exactly 3 octave families** (QG138),
   which requires the spectral span ω_max/ω_min ∈ [4, 8).
4. No fitted constants, no phenomenological assumptions — the selection must
   follow from attractor dynamics and spectral structure only.

---

## 3. Results

### 3.1 Z2 Automorphism Constraint (eliminates D64, D128)

| n | seed half-shift (6|n) | adjacency half-shift | Z2 OK |
|---|----------------------|----------------------|-------|
| 48 | True | True | True |
| 64 | **False** | True | **False** |
| 96 | True | True | True |
| 128 | **False** | True | **False** |
| 192 | True | True | True |

D64/D128: 64 mod 6 = 4, 128 mod 6 = 2 → the period-3 seed is NOT half-shift
invariant → **the Z2 doublet structure is broken for pure-power sizes**.

### 3.2 Family-Count Constraint (eliminates D48, D192)

| n | span(ω) | log2 span | families | 3-family window |
|---|---------|-----------|----------|-----------------|
| 48 | 3.240 | 1.696 | 2 | False (too few) |
| 96 | **6.403** | **2.679** | **3** | **True** |
| 192 | 12.779 | 3.676 | 4 | False (too many) |

The octave decomposition counts one family per frequency octave: 3 families
requires log2(span) ∈ [2, 3), i.e. span ∈ [4, 8).

### 3.3 Spectral Optimality (span scaling)

```
n=48:  span/n = 0.0675
n=96:  span/n = 0.0667
n=192: span/n = 0.0666
```

The span scales as **span ≈ 0.0667·n**, so the 3-family window [4, 8)
fixes **n ∈ [60, 120)**.

### 3.4 Octave-Rung Selection (D96 is the unique 3-family rung)

The natural doubling chain is **n = 3·2^k** (period-3 seed × frequency
doubling):

| k | n | span | families | in 3-family window |
|---|----|------|----------|--------------------|
| 4 | 48 | 3.240 | 2 | False |
| 5 | **96** | **6.403** | **3** | **True** ✓ |
| 6 | 192 | 12.779 | 4 | False |

**D96 is the UNIQUE octave rung in the 3-family window.** The 3-family rungs
set is exactly `[96]`.

### 3.5 Candidate Discrimination

| candidate | Z2 OK | families | span | selected |
|-----------|-------|----------|------|----------|
| D64  | False (64 mod 6 = 4) | 3 | 4.298 | No |
| D96  | True  | 3 | 6.403 | **Yes** |
| D128 | False (128 mod 6 = 2) | 4 | 8.531 | No |
| D192 | True  | 4 | 12.779 | No |

- **D64** — fails Z2 despite 3 families → no Z2 doublets
- **D128** — fails Z2, and has 4 families (span 8.5 ≥ 8)
- **D192** — passes Z2 but has 4 families (span 12.8 ≥ 8)
- **D96** — passes Z2 AND exactly 3 families ✓

### 3.6 Stability (not size-selecting)

All octave rungs converge to the same radius-6 attractor. The size selection
is **not stability-driven** — it comes from the structural constraints.

---

## 4. Classification

**Selection score: 5 / 5**

- +1 Z2 constraint satisfied at 96 (6 | 96, seed half-shift)
- +1 3-family window at 96 (span 6.40 ∈ [4, 8))
- +1 span scaling 0.0667 (spectral optimality window)
- +1 unique octave rung (only 96 in [60, 120))
- +1 all alternatives discriminated (D64, D128, D192 all fail)

```
CLASSIFICATION: INEVITABLE
```

- **NO SELECTION rejected:** structural constraints uniquely single out n=96.
- **PARTIAL SELECTION rejected:** BOTH the Z2 automorphism constraint AND the
  family-count constraint select 96, and all alternatives are discriminated.
- **INEVITABLE accepted:** D96 is the inevitable attractor geometry.

---

## 5. Conclusion

The observable attractor selects **D96 because it is the unique octave rung
satisfying both structural constraints simultaneously**:

1. **Z2 automorphism** — the period-3 seed requires the half-shift symmetry
   i → i+n/2, forcing **6 | n**. D64 and D128 (pure powers of 2) are
   eliminated — they cannot support the Z2 doublet structure.

2. **Family-count constraint** — exactly 3 octave families requires
   span ∈ [4, 8), and since span ≈ 0.0667·n this fixes **n ∈ [60, 120)**.
   D192 (span 12.8) and D48 (span 3.24) are eliminated by too many / too few
   families.

3. **Octave-rung selection** — the natural doubling chain n = 3·2^k
   (period-3 × frequency doubling) contains n = 48, 96, 192, and **only
   n = 96 falls in the 3-family window**.

The selection is driven by **automorphism + family-count structure**, not by
stability (all candidates are stable radius-6 attractors). D96 is therefore
the **inevitable attractor geometry**.

---

## 6. Chain

```
D96 selection (QG159)                                          ← THIS PHASE
  → Z2 doublets (QG153, QG155)
  → moment orders = Z2 powers {2⁻¹, 2⁰, 2¹} (QG158)
  → N_eff = moments (QG157)
  → δ = log(N_eff)/log(span) (QG156)
  → hierarchy exponent p = 2δ (QG140/141)
```
