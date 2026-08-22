# TQM-QG Phase 208 — Hawking Temperature With Psi

**Status:** COMPLETE — **HAWKING ORIGIN**
**Tests:** TQMQG2080, TQMQG2081, TQMQG2082 (all passed)
**Core class:** `TQM.Core/ResearchXH/HawkingTemperatureWithPsi.cs`
**Known:** QG184 (T ∝ 1/R), QG186 (frame dragging from ψ), QG207 (ψ-completed metric)
**Method:** TRM/D96 only, deterministic, no new primitives

---

## 1. The Question

Does the ψ sector change the Hawking temperature, or leave T ∝ 1/R
unchanged? We derive the surface gravity κ, the temperature scaling, and the
ψ corrections in the ψ-completed metric.

---

## 2. The Derivation

### 2.1 The ψ-completed metric (QG207)

```
g_00 = −ρ^(2/d)·e^(2ψ)
g_ii =  ρ^(2/d)·e^(−2ψ/(d−1))
√(−g) = ρ        (measure preserved)
```

### 2.2 Surface gravity

κ = (1/2)·√(−g^00·g^11)·|g_00′| at the horizon. Near the horizon the density
profile ρ ∝ (R − r) dominates, giving

```
κ = (1/d)·|ρ′|/ρ · e^(ψ·(1+1/(d−1)))
  ~ (1/R) · e^(ψ·3/2)          (d = 3; |ρ′|/ρ ~ 1/R_h)
```

### 2.3 Temperature scaling

```
T_ψ = κ/2π = T_0 · e^(ψ·(1+1/(d−1)))     T_0 = 1/((d−1)·R^(d−2))
```

At ψ = 0 this recovers **QG184 exactly** (T ∝ 1/R). The ψ contribution is a
**multiplicative, radius-independent factor**.

### 2.4 The ψ correction is prefactorial

The temperature ratio at two radii is **ψ-invariant**:

| Quantity | value |
|----------|-------|
| T(1)/T(2) without ψ | 2.0000 |
| T(1)/T(2) with ψ=0.2 | 2.0000 |

The **law T ∝ 1/R is preserved**; ψ rescales only the overall prefactor.

### 2.5 Horizon regularity

If ψ → 0 at the horizon (asymptotic flatness / the standard black-hole
boundary condition), then **T_ψ = T_0 exactly** — no correction at all.

---

## 3. The Contrast

| Observable | ψ = 0 | ψ ≠ 0 |
|------------|-------|-------|
| **Hawking temperature** (QG184) | T ∝ 1/R ✓ | T ∝ 1/R ✓ (prefactor only) |
| **Frame dragging** (QG186) | Ω = 0 ✗ | restored ✓ |

Frame dragging is a **ψ-sector observable** (it requires ψ). The Hawking
temperature is a **ρ-sector (first-law) observable** — it survives at ψ = 0.

---

## 4. Origin Score (4/4)

| Channel | Value | Held? |
|---------|-------|-------|
| κ ∝ (1/R)·e^(ψ·3/2) | surface gravity | ✓ |
| T ∝ 1/R law ψ-invariant | ratio unchanged | ✓ |
| ψ = 0 ⇒ QG184 | exact | ✓ |
| ψ(R_h) → 0 ⇒ T_ψ = T_0 | regularity | ✓ |

---

## 5. Conclusion

**HAWKING ORIGIN.** The ψ sector **does not change** the Hawking temperature
law. T ∝ 1/R (QG184) is preserved; ψ contributes only the prefactor
e^(ψ·(1+1/(d−1))), which is removed by the horizon-regularity condition
ψ(R_h) → 0. The Hawking temperature is a ρ-sector observable — unlike frame
dragging, which requires ψ (QG186). This closes the "Hawking temperature
after ψ" open question (QG24).
