# Y_G_003_Result.md — ResearchY-G_003 Gravity Magnitude Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_003_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (252 ms)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_003"`

---

## Summary

**Question:** What physical gravitational change corresponds to a measured Δρ — and can a G_002
configuration produce a gravity change above 10⁻¹² g, 10⁻⁹ g, 10⁻⁶ g without changing total energy?

**Verdicts:** the potential/clock channel is **MEASURABLE**; the acceleration/curvature channel is
**ASTROPHYSICAL ONLY**; phase coherence and rescaling are **PRACTICALLY ZERO**.

**Critical answer:** **YES to all three thresholds** at fixed total energy — the binding constraint is
the physical scale L, not the energy budget.

## Conversion chain

```
g_00 = -rho^(2/d)                        (QG197)
ΔPhi/c^2 = (1/d)·Δln rho                 (QG21/QG187)
Δa = -(c^2/d)·grad ln rho                (G4-O3)
ΔR_phys = ΔR_AT / L^2                    (G4-G2)
Δln rho = d·Δa_AT  (per cell)   =>
  ΔPhi/c^2 = Δa_AT            (pure number, no length scale)
  Δa_phys  = c^2·Δa_AT / L    (m/s^2)
  ΔR_phys  = ΔR_AT / L^2      (1/m^2)
  ΔM_eq    = Δa_phys·L^2/G    (kg)
```

**Canonical anchor:** Earth-surface vs GPS-orbit `ΔΦ/c² = 5.2935e-10` ⟹ **45.74 μs/day** (QG187: 45.7);
in AT the same potential is `Δln ρ = 1.588e-9`. Anchors: `G = 6.6476e-11` (QG181),
`g† = cH₀/(2π) = 1.04220e-10 m/s²` (QG080).

## Per-case magnitudes (Δa at 15 kpc)

| case | Δa_AT | ΔΦ/c² | Δa [m/s²] | [g] | ΔM_eq [M_☉] |
|---|---|---|---|---|---|
| arrangement | 0.685714 | 0.685714 | 1.3315e-4 | 1.358e-5 | 2.157e17 |
| degeneracy redistribution | 0.603175 | 0.603175 | 1.1712e-4 | 1.194e-5 | 1.898e17 |
| D96 vs random | 0.333333 | 0.333333 | 6.4726e-5 | 6.600e-6 | 1.049e17 |
| D96³ vs D96 | 0.276596 | 0.276596 | 5.3709e-5 | 5.477e-6 | 8.703e16 |
| survivor compression (k=48) | 0.032121 | 0.032121 | 6.2372e-6 | 6.360e-7 | 1.011e16 |
| phase coherence | **0** | **0** | **0** | **0** | **0** |
| rescaling (control) | 0 (fp 5e-15) | 0 | ~0 | ~0 | ~0 |

## Threshold lengths at fixed total energy

| case | > 10⁻¹² g for L < | > 10⁻⁹ g for L < | > 10⁻⁶ g for L < |
|---|---|---|---|
| arrangement | 203.66 Gpc | 203.66 Mpc | 203.66 kpc |
| degeneracy redistribution | 179.15 Gpc | 179.15 Mpc | 179.15 kpc |
| D96 vs random | 99.00 Gpc | 99.00 Mpc | 99.00 kpc |
| D96³ vs D96 | 82.15 Gpc | 82.15 Mpc | 82.15 kpc |
| survivor compression (k=48) | 9.54 Gpc | 9.54 Mpc | 9.54 kpc |
| phase, rescaling | 0 | 0 | 0 |

**Guaranteed window** (weakest witness): 10⁻¹² g for L < 9.54 Gpc; 10⁻⁹ g for L < 9.54 Mpc;
10⁻⁶ g for L < 9.54 kpc. The observable-universe radius (~14.3 Gpc) exceeds every case's 10⁻¹² g window,
so the practically-zero regime lies beyond the horizon.

## Detectability

- 10⁻¹² g = **0.0941 g†**; 10⁻⁹ g = **94.1 g†**; 10⁻⁶ g = **9.41e4 g†** (the RAR scale g† is itself 1.06e-11 g).
- Ambient calibration: g† over 15 kpc ⟺ **Δln ρ = 1.6102e-6**.
- **Suppression requirement:** the G_002 witnesses are **3.746e5 ×** the observed ambient contrast, so
  any realised fixed-energy reconfiguration must be suppressed by ≥ 3.7e5.
- Clock floor: the witnesses are **6.03e17 ×** above an optical clock's 10⁻¹⁸ floor.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_G_003_Calibration` | chain, GPS anchor, L-scaling of ΔΦ/Δa/ΔR | ✅ |
| `Y_G_003_ATDelta` | the five witnesses and the two exact zeros | ✅ |
| `Y_G_003_PhysicalField` | Δa, ΔΦ, ΔR, ΔM_eq on the ladder | ✅ |
| `Y_G_003_Thresholds` | the three threshold windows at fixed energy | ✅ |
| `Y_G_003_Detectability` | g† units, ambient calibration, suppression | ✅ |
| `Y_G_003_Verdicts` | MEASURABLE / ASTROPHYSICAL ONLY / PRACTICALLY ZERO | ✅ |
| `Y_G_003_Run` | research report | ✅ |

## Classification

DERIVED: the conversion laws and anchors; the threshold lengths, nesting and exact-threshold identities;
the exact zeros of phase and rescaling; the ambient calibration and the suppression requirement.
BOUNDARY: the realisation of a lattice-level Δa_AT across one physical length L; the values of G and g†;
`ρ̄ = 1/N` and the exponent `2/d`. EMERGENT: the per-case magnitudes.

**No reclassification** — the D_040 registry is untouched; G_001/G_002 verdicts used, not modified.

## Conclusion

A measured Δρ corresponds to `ΔΦ/c² = d⁻¹Δln ρ` (a pure number: clock and redshift), `Δa = (c²/d)Δln ρ/L`,
`ΔR = ΔR_AT/L²` and an equivalent Newtonian mass `ΔM = Δa·L²/G`. The AT-native magnitudes are **large, not
small**: 3–69 % potential changes, and accelerations above 10⁻⁶ g whenever the reorganisation spans ≳ 10 kpc.
All three thresholds are passable at fixed total energy; the accelerator is the physical scale L, and the
sharp falsifiable consequence is that any realised configuration must be suppressed by ≥ 3.7e5 relative to
the G_002 witnesses — precisely the open question G_002 left (no known process realises one).
