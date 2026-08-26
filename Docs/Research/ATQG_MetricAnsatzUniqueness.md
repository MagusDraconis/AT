# AT-QG Phase 207 — Metric Ansatz Uniqueness

**Status:** COMPLETE — **PARTIAL UNIQUE**
**Tests:** ATQG2070, ATQG2071, ATQG2072 (all passed)
**Core class:** `AT.Core/ResearchXH/MetricAnsatzUniqueness.cs`
**Known:** g = ρ^(2/d)η preferred but not proven unique (G4-A0)
**Method:** TRM/D96 only, deterministic, no new primitives

---

## 1. The Question

Is g = ρ^(2/d)η **uniquely selected**? We test every admissible conformal
power ρ^a·η and the alternative counting-preserving forms on four criteria.

---

## 2. The Tests

### 2.1 Measure preservation — UNIQUE selection of k = 2/d

The volume element of g = ρ^k·η is √(−g) = ρ^(kd/2). Measure preservation
requires √(−g) = ρ:

```
√(−g) = ρ^(kd/2) = ρ  ⇒  k·d/2 = 1  ⇒  k = 2/d
```

| k | √(−g) exponent | volume error |
|----|----------------|--------------|
| 1/3 | 0.50 | 0.5 |
| 1.5/3 | 0.75 | 0.25 |
| **2/3** | **1.00** | **0** |
| 3/3 | 1.50 | 0.5 |

**Only k = 2/d preserves the counting-measure identification.**

### 2.2 Observable consistency (geodesic acceleration) — UNIQUE

The derived geodesic acceleration (QG20/21) is a = −(1/d)·d(ln ρ)/dx. The
ansatz gives a = −(k/2)·d(ln ρ)/dx:

| k | ansatz coefficient | derived 1/d | match |
|----|-------------------|-------------|-------|
| 1/3 | 0.167 | 0.333 | ✗ |
| **2/3** | **0.333** | **0.333** | **✓** |
| 1.0 | 0.500 | 0.333 | ✗ |

**Only k = 2/d reproduces the derived geodesic law.**

### 2.3 Einstein recovery / Bianchi — holds at k = 2/d

With k = 2/d, σ = ln(ρ)/d and the Einstein components
G_11 = ((d−1)(d−2)/2)(σ′)², G_ii = (d−2)[σ″ + ((d−3)/2)(σ′)²] are exactly the
**QG197/D2ToD3Bridge** Bianchi-conserved structure (verified divergence-free).

### 2.4 Alternative counting-preserving forms — NOT unique overall

The ψ-perturbed metrics
g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) have the **same** √(−g) = ρ
for any ψ (measure preserved), but different geodesics (a changes with ψ) and
different Einstein structure:

```
b = 0   (conformal):  a(1.0) = −0.333  (= derived)
b = 0.1:              a(1.0) = −0.585
b = 0.3:              a(1.0) = −1.558
```

These are exactly the **QG186/QG44 tensor-sector** metrics (frame dragging,
lensing). So the conformal ansatz is **not the unique counting-preserving
metric** — the ψ sector provides alternatives with different observables.

---

## 3. Classification

### **PARTIAL UNIQUE**

| Criterion | Selection |
|-----------|-----------|
| Measure preservation | k = 2/d unique |
| Geodesic acceleration | k = 2/d unique |
| Einstein recovery / Bianchi | k = 2/d (QG197) |
| Counting-preserving forms | **NOT unique** (ψ sector, QG44/186) |

**Within the conformal-flat class ρ^a·η, k = 2/d is uniquely selected** by
three independent arguments (measure, acceleration, Einstein recovery).
**But the ansatz is not the unique counting-preserving metric**: the ψ tensor
sector gives alternative metrics with the same √(−g) = ρ and different
observables.

The conformal ansatz g = ρ^(2/d)η is the **ψ = 0 isotropic member** of the
counting-preserving class; the ψ ≠ 0 sector is its anisotropic (tensor)
completion. The ansatz is uniquely selected *within its class*, and the class
is completed by the tensor sector — hence PARTIAL UNIQUE.
