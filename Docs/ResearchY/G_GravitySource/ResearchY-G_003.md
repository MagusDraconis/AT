# ResearchY-G_003 — Gravity Magnitude Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_003 (permanent)
**Title:** Gravity Magnitude Audit — what physical gravitational change corresponds to a measured Δρ?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_003.md`
**Depends on:** ResearchY-G_001 (ρ sources gravity), ResearchY-G_002 (ρ is controllable at fixed total
energy; the witnesses and their lattice-level Δa/ΔR), D_047/D_048 (degeneracy structure, L = 0.53125);
AT-QG QG181/QG182 (G, M_Pl), QG187 (GPS/redshift anchor), QG21 (redshift law), QG197 (g₀₀ = −ρ^(2/d)),
QG194 (count conservation, Σm = 0), QG080/DATA (`g† = cH₀/(2π)`); G4-O3 (native acceleration),
G4-G2 (R = F(ρ))
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_003_Tests.cs` (7/7 PASSED, 252 ms)
**Shared machinery:** `AT.Tests/Shared/DensityField.cs`, `AT.Tests/Shared/PhysicalUnits.cs`

---

## Purpose

G_001 identified the source (the counting measure ρ) and G_002 showed that ρ can be reorganised **at
fixed total energy** (Σm = 0 exactly, QG194). The remaining question is purely quantitative:

> **What physical gravitational change corresponds to a measured Δρ — and can a G_002 configuration
> produce a gravity change above 10⁻¹² g, 10⁻⁹ g, 10⁻⁶ g without changing the total energy?**

---

## 0. The conversion chain (all canonical; no fitted quantity)

| Step | AT statement | Source |
|---|---|---|
| metric | `g₀₀ = −ρ^(2/d)` | QG197 |
| potential | `ΔΦ/c² = (1/d)·Δln ρ` | redshift law, QG21/QG187 |
| acceleration | `Δa = −∇ΔΦ = −(c²/d)·∇ln ρ` | G4-O3 |
| curvature | `ΔR_phys = ΔR_AT / L²` | G4-G2 |

Inverting `a = −(1/d)∇ln ρ` over **one cell** gives `Δln ρ = d·Δa_AT`, so the audit's measures are

```
ΔΦ/c² = Δa_AT                    [dimensionless — NO length scale needed]
Δa_phys = c²·Δa_AT / L           [m/s²  — needs the physical scale L]
ΔR_phys = ΔR_AT / L²             [1/m²]
ΔM_eq = Δa_phys·L²/G             [kg — the equivalent Newtonian mass]
```

**Calibration assumption (BOUNDARY).** A lattice-level `Δa_AT` is realised as a gradient across one
**physical coherence length L**. ρ is a large-scale field, so L is astrophysical or cosmological; the
potential channel is the only one free of L.

**Anchors.** `c`, `g = 9.80665 m/s²`, `G = 6.6476e-11` (QG181, 0.40% from CODATA),
`g† = cH₀/(2π) = 1.04220e-10 m/s²` (QG080).

**Canonical cross-check (GPS).** The Earth-surface vs GPS-orbit potential gives
`ΔΦ/c² = 5.2935e-10` ⟹ **45.74 μs/day**, matching QG187's 45.7 μs/day. In AT the same number is a
counting-measure contrast `Δln ρ = 1.588e-9`. The conversion chain is therefore anchored on an
existing canonical result, not asserted.

---

## 1. The AT-native deltas (from the G_002 witnesses)

| case | Δa_AT | ΔΦ/c² | Δa @ 15 kpc [m/s²] | [g] | ΔM_eq @ 15 kpc [M_☉] |
|---|---|---|---|---|---|
| arrangement | 0.685714 | 0.685714 | 1.3315e-4 | 1.358e-5 | 2.157e17 |
| degeneracy redistribution | 0.603175 | 0.603175 | 1.1712e-4 | 1.194e-5 | 1.898e17 |
| D96 vs random | 0.333333 | 0.333333 | 6.4726e-5 | 6.600e-6 | 1.049e17 |
| D96³ vs D96 | 0.276596 | 0.276596 | 5.3709e-5 | 5.477e-6 | 8.703e16 |
| survivor compression (k = 48) | 0.032121 | 0.032121 | 6.2372e-6 | 6.360e-7 | 1.011e16 |
| **phase coherence** | **0** | **0** | **0** | **0** | **0** |
| rescaling (control) | 0 (fp residue 5.1e-15) | 0 | ~0 | ~0 | ~0 |

The ordering is arrangement > degeneracy > D96-vs-random > D96³-vs-D96 > compression, and the two zero
cases are exact: phase is inert, and rescaling leaves `a` invariant (its curvature changes only by the
overall factor `λ^(−2/d)`, verified to < 1e-6, so the observable geometry is untouched).

---

## 2. Scale ladder (strongest witness: arrangement, Δa_AT = 0.685714, ΔΦ/c² = 0.6857)

| L | Δa [m/s²] | Δa / g | ΔR [1/m²] | ΔM_eq [M_☉] |
|---|---|---|---|---|
| 1 pc | 1.9973 | 2.037e-1 | 5.652e-32 | 1.438e13 |
| 1 kpc | 1.9973e-3 | 2.037e-4 | 5.652e-38 | 1.438e16 |
| 15 kpc | 1.3315e-4 | 1.358e-5 | 2.512e-40 | 2.157e17 |
| 1 Mpc | 1.9973e-6 | 2.037e-7 | 5.652e-44 | 1.438e19 |
| 1 Gpc | 1.9973e-9 | 2.037e-10 | 5.652e-50 | 1.438e22 |
| c/H₀ = 4.45 Gpc | 4.4903e-10 | 4.579e-11 | 2.857e-51 | 6.398e22 |

Two structural facts are asserted exactly: `ΔΦ` is **independent of L** while `Δa ∝ 1/L`
(`a(1 kpc)/a(1 Mpc) = Mpc/Kpc = 1000` exactly) and `ΔR ∝ 1/L²` (halving L quadruples R).

**The AT-native magnitude is not small.** ΔΦ/c² = 0.686 is a **68.6 % potential change**, and the
equivalent mass at the galactic scale is 2.2e17 M_☉ — 3.6e6 × the Milky Way's baryonic mass.

---

## 3. The critical question: threshold lengths at fixed total energy

| case | > 10⁻¹² g for L < | > 10⁻⁹ g for L < | > 10⁻⁶ g for L < |
|---|---|---|---|
| arrangement | 203.66 Gpc | 203.66 Mpc | 203.66 kpc |
| degeneracy redistribution | 179.15 Gpc | 179.15 Mpc | 179.15 kpc |
| D96 vs random | 99.00 Gpc | 99.00 Mpc | 99.00 kpc |
| D96³ vs D96 | 82.15 Gpc | 82.15 Mpc | 82.15 kpc |
| survivor compression (k = 48) | 9.54 Gpc | 9.54 Mpc | 9.54 kpc |
| phase coherence, rescaling | 0 | 0 | 0 (force channel zero) |

**ANSWER: YES to all three thresholds — at fixed total energy.** The windows nest
(`L₁₂ > L₉ > L₆`), at the threshold length the change is exactly at threshold (asserted), just inside it
is above, and the **guaranteed window** (weakest witness, survivor compression) is

```
10⁻¹² g  for  L < 9.54 Gpc
10⁻⁹  g  for  L < 9.54 Mpc
10⁻⁶  g  for  L < 9.54 kpc
```

The binding constraint is the **physical scale L, not the energy budget**. The observable-universe
radius (~14.3 Gpc) exceeds the 10⁻¹² g window of every case, so the "practically zero" regime of the
acceleration channel lies beyond the horizon: **within the observable universe no non-zero G_002
configuration is practically zero.**

---

## 4. Detectability thresholds

| threshold | in g† units | reading |
|---|---|---|
| 10⁻¹² g | **0.0941 g†** | about a tenth of the galactic AT field — the RAR scale itself is 1.06e-11 g |
| 10⁻⁹ g | **94.1 g†** | ~100 × the entire AT galactic field — unmissable |
| 10⁻⁶ g | **9.41e4 g†** | ~10⁵ × the AT galactic field — strong-field regime |

**Clock/redshift channel.** `ΔΦ/c² = Δa_AT` is a pure number, so the witnesses give 3–69 % clock and
redshift changes — **6.0e17 ×** above an optical clock's 10⁻¹⁸ fractional floor, and 10⁴ × above the
strongest present astrophysical bound (~10⁻¹¹).

**Ambient calibration (the audit's falsifiable number).** The entire observed galactic AT field
`g†` over 15 kpc corresponds to a counting-measure contrast of only

```
Δln ρ_ambient = d·g†·L/c² = 1.6102e-6
```

so the G_002 witnesses (Δa_AT ≈ 0.03–0.69) are **3.746e5 ×** the observed ambient contrast. Any
**realised** fixed-energy reconfiguration must therefore be **suppressed by at least 3.7e5** to leave
the rotation curves AT reproduces intact. No physical process is known to realise one (G_002 OP2) — this
is the audit's sharp falsifiable requirement.

---

## 5. Verdicts

| verdict | channel | why |
|---|---|---|
| **MEASURABLE** | potential / clock / redshift | `ΔΦ/c² = Δa_AT` is a pure number: 3–69 % changes for the witnesses, ~1e17× above an optical clock. No length scale enters. |
| **ASTROPHYSICAL ONLY** | acceleration / curvature | requires a region of size L; thresholds met only at kpc (10⁻⁶ g), Mpc (10⁻⁹ g) and Gpc (10⁻¹² g) scales. No laboratory realisation exists — ρ is a large-scale field and the G_002 operations are lattice reorganisations. |
| **PRACTICALLY ZERO** | phase; rescaling | phase: every channel exactly zero. rescaling: `Δa = 0` exactly and only the overall factor `λ^(−2/d)` changes, so the observable geometry is untouched. |

**Answer to the question.** A measured Δρ corresponds to `ΔΦ/c² = d⁻¹Δln ρ` (a pure number — clock and
redshift), to `Δa = (c²/d)Δln ρ / L` (needing the physical scale L), to `ΔR = ΔR_AT/L²` and to an
equivalent Newtonian mass `ΔM = Δa·L²/G`. A G_002-class reconfiguration is **not** a small effect: it is
astronomically large, in fact large enough that its realisation would be excluded unless suppressed by
≥ 3.7e5 — which is exactly the open question G_002 left (no known process realises it).

---

## 6. Classification and caveats

| Component | Status |
|---|---|
| the conversion laws and their canonical anchors (QG197/QG21/QG187/G4-G2/G4-O3) | **DERIVED** |
| the threshold lengths, their nesting, and the exact-threshold identities | **DERIVED** |
| the exact zeros of the phase and rescaling channels | **DERIVED** |
| the ambient calibration (1.6102e-6) and the suppression requirement (3.746e5) | **DERIVED** |
| the realisation of a lattice-level `Δa_AT` as a gradient across one physical length L | **BOUNDARY** |
| the values of `G` (QG181, 0.40%) and `g† = cH₀/(2π)` | **BOUNDARY** (anchors) |
| `ρ̄ = 1/N` and the exponent `2/d` | **BOUNDARY** (unchanged from G_001/G_002) |
| the specific per-case magnitudes | **EMERGENT** (they follow from the G_002 witnesses) |

**No reclassification.** The D_040 `ClassificationRegistry` is untouched; G_001's and G_002's verdicts
are used, not modified.

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| `ΔΦ/c² = Δa_AT` | a measured redshift/time-dilation that does not track the counting-measure contrast |
| `Δa ∝ 1/L` (scale-free field) | an acceleration with a length-independent amplitude |
| the threshold windows | a fixed-energy reconfiguration that fails to exceed 10⁻¹² g below 9.54 Gpc |
| the suppression requirement | a realised reconfiguration above 1.61e-6 that leaves the observed rotation curves intact |
| phase is inert | a phase-dependent term in `g₀₀ = −ρ^(2/d)` or in `R = F(ρ)` |

---

## 8. Open problems

1. **G-003 OP1 — the physical coherence length.** The whole acceleration/curvature translation hinges on
   L. Whether L is the cell size of the realised lattice, an astrophysical coherence length, or a
   spectral-functional scale is open (G_001 OP3 / G_002 OP3).
2. **G-003 OP2 — the suppression mechanism.** *Why* should a realisable reconfiguration be suppressed by
   ≥ 3.7e5 (or absent)? This is the quantitative form of G_002 OP2.
3. **G-003 OP3 — observational discrimination.** A fixed-energy reconfiguration at Δln ρ ≲ 1.6e-6 is
   exactly at the level of the ambient galactic field: whether such a superposition is distinguishable
   from a mass redistribution is open (G_002 OP4).
4. **G-003 OP4 — strong-field limit.** The witnesses give ΔΦ/c² ≫ 1 at small L, where the weak-field
   conversion used here is invalid; the strong-field (non-linear) translation is not derived.

---

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_003_Tests.cs` — **7/7 PASSED** (252 ms)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_003"`

| Test | Verifies | Result |
|---|---|---|
| `Y_G_003_Calibration` | conversion chain; GPS anchor 45.74 μs/day; ΔΦ L-free, Δa ∝ 1/L, ΔR ∝ 1/L² | PASS |
| `Y_G_003_ATDelta` | the five G_002 witnesses and the two exact zeros | PASS |
| `Y_G_003_PhysicalField` | Δa, ΔΦ, ΔR, ΔM_eq on the scale ladder | PASS |
| `Y_G_003_Thresholds` | the three threshold windows at fixed total energy (the critical question) | PASS |
| `Y_G_003_Detectability` | thresholds in g†; ambient calibration; suppression requirement | PASS |
| `Y_G_003_Verdicts` | MEASURABLE / ASTROPHYSICAL ONLY / PRACTICALLY ZERO | PASS |
| `Y_G_003_Run` | research report | PASS |

---

## References

- ResearchY-G_001 (Gravity Source Audit — the source is the counting measure),
  ResearchY-G_002 (Density Control Audit — the witnesses, at fixed total energy),
  D_047 (degeneracy = free room), D_048 (latent fraction).
- AT-QG: QG21, QG080, QG181, QG182, QG187, QG194, QG197; G4-O3, G4-G2.
- `Docs/RAR_TimeInterpretation.md` (QG080), `Docs/ATQG_PhysicsCoverage.md` (Gravity / GR).
