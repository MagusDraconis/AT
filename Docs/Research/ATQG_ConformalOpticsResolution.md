# AT-QG Phase 212 — Conformal Optics Resolution

> ⚠ **CORRECTION — ResearchY-G_025 / AT-QG phase 320
> (`Docs/Research/ATQG_ConformalOpticsDeterminantCorrection.md`).**
> Two defects in this phase's own basis were found by independent verification and are now fixed:
> **(1)** the ψ-perturbed determinant was off by one spatial factor — the correct identity is
> `det g = -ρ^(2(d+1)/d)e^(-2ψ/(d-1))`, `√(-det g) = ρ^((d+1)/d)e^(-ψ/(d-1))`,
> `√(det g_ij) = ρ·e^(-dψ/(d-1))`; the shipped line `√(-g) = ρ unchanged for ANY ψ` is
> **FALSE** (error unbounded in ψ: 36.24 % at b = 0.3, 8901.71 % at b = -3). ψ = 0 is the *only*
> measure-preserving member. **(2)** `GammaPsiNonZero()` was a **hard-coded constant**, never computed from the
> metric — γ is now derived (`GammaFromPsiMetric`), giving −1 at ψ = 0 and +1 at the **derived**
> ψ = -4σ (first order; exact value `e^(6σ) = ρ²` at d = 3, so +1 only in the weak field).
> The optics *conclusion* (full GR at ψ ≠ 0, no lensing at ψ = 0) **STANDS** and is now derived rather
> than asserted; what is withdrawn is the measure-preservation premise and the hard-coded-γ origin-score basis.
> See also G_024.

**Status:** COMPLETE — **OPTICS RESOLVED**
**Tests:** ATQG2120, ATQG2121, ATQG2122 (all passed)
**Core class:** `AT.Core/ResearchXH/ConformalOpticsResolution.cs`
**Known:** QG21 (no lensing), QG22 (artifact), QG26 (γ=−1), QG186 (ψ restores frame dragging), QG207 (ψ-completed metric)
**Method:** TRM/D96 only, deterministic, no new primitives

---

## 1. The Question

The conformal (ψ = 0) sector predicts **redshift without lensing** — the last
gravity frontier. Is conformal no-lensing **physical**, an **artifact**, or a
**restricted sector**?

---

## 2. The Two Sectors

### 2.1 ψ = 0 sector: PPN γ = −1 → no lensing

The conformally-flat metric g = ρ^(2/d)η has PPN γ = −1 (QG26). Every lensing
observable (deflection, convergence, shear, magnification) and the Shapiro
delay are ∝ (1+γ)/2:

```
(1+γ)/2 = 0  ⇒  deflection = 0, Shapiro = 0, κ = 0, γ_s = 0
```

Only the **gravitational redshift** survives — it is governed by g_00 = −ρ^(2/d)
alone.

### 2.2 ψ ≠ 0 sector: PPN γ = +1 → full GR optics

The ψ-completed metric g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) is the
Fierz-Pauli tensor sector (QG44), whose linearized limit is GR: PPN γ = +1.
Then:

```
(1+γ)/2 = 1  ⇒  deflection, Shapiro, convergence, shear all at full GR strength
```

Frame dragging (h_0i, QG186) and the tensor GW polarizations are restored
with it.

---

## 3. The Resolution

### 3.1 QG207 classification

The conformal ansatz g = ρ^(2/d)η is the **ψ = 0 isotropic member** of the
counting-preserving metric class; the ψ tensor sector completes it.

### 3.2 Verdict

Conformal no-lensing is a **RESTRICTED SECTOR**:

| Sector | PPN γ | Lensing | Shapiro | Frame dragging | Redshift |
|--------|-------|---------|---------|----------------|----------|
| ψ = 0 (conformal) | −1 | 0 | 0 | 0 | yes |
| ψ ≠ 0 (tensor) | +1 | GR | GR | restored | yes |

- **Not a numerical artifact** — γ = −1 is exact within the ψ = 0 slice.
- **Not physical GR** — the ψ = 0 slice is an *isotropic assumption*.
- **The physical sector is ψ ≠ 0** — the tensor completion restores full GR
  optics.

---

## 4. Origin Score (4/4)

| Channel | Value | Held? |
|---------|-------|-------|
| ψ = 0: γ = −1, all lensing observables vanish | (1+γ)/2 = 0 | ✓ |
| ψ ≠ 0: γ = +1, lensing restored at GR strength | (1+γ)/2 = 1 | ✓ |
| Shapiro delay follows γ | 0 vs GR | ✓ |
| QG207: conformal = restricted ψ=0 member | class-completed | ✓ |

---

## 5. Conclusion

**OPTICS RESOLVED.** Conformal no-lensing is a **restricted sector** — the
ψ = 0 isotropic slice of the counting-preserving metric class. It is real
*within* that slice (γ = −1 is exact), but the slice is an assumption, and
the physical sector is the ψ ≠ 0 tensor completion, which restores full GR
lensing, Shapiro delay, and frame dragging (γ = +1).

This closes **C1** (lensing present vs absent: the "lensing" was a potential
difference, corrected by QG21) and **C5** (no-lensing fundamental vs artifact:
it is the ψ = 0 sector prediction, superseded by the ψ tensor sector). The
conformal-optics frontier (G3 in the QG211 audit) is resolved.
