# AT-QG Observable Dependency Matrix

**Status:** PERMANENT RESEARCH MATRIX — CALIBRATION ANCHORED, TARGET SELECTION MOSTLY CATALOG-DRIVEN

---

## Summary

This matrix records the referee-safe dependency structure extracted from the units audit, the 29-observable audit, the hosted-dynamics review, the moment-sector audit, and the standard model / cosmology chapters.

### Counts

| Class | Count |
|---|---:|
| Total core observables | 29 |
| Natural | 7 |
| Secondary | 19 |
| Post-hoc | 3 |

### Calibration anchors

| Anchor | Role |
|---|---|
| `m_e` | quark-mass bridge / absolute fermion anchor |
| `v` | electroweak weak/Higgs calibration anchor |
| SI conversion (`ħ`, `c`, GeV↔kg) | unit-convention boundary for SI gravity quantities |

---

## Dependency Matrix

| Observable | Derived structure | Calibration anchor | Free parameters | Class |
|---|---|---|---:|---|
| `1/α_em` | D96 spectral ratio / total mode + doublet count | none | 0 | fully dimensionless |
| `α_weak` | ratio of spectral counts | none | 0 | fully dimensionless |
| `α_strong` | ratio of spectral counts | none | 0 | fully dimensionless |
| `sin²θ_W` | spectral ratio | none | 0 | fully dimensionless |
| `θ_QCD = 0` | exact structural identity | none | 0 | fully dimensionless |
| CKM angles / phase | octave-transition read | none | 0 | fully dimensionless |
| PMNS angles / phase | T3-only read | none | 0 | fully dimensionless |
| `S, T, U` | spectral-observable readout | none | 0 | dimensionless |
| electron `g-2` | spectral correction to Schwinger term | none | 0 | dimensionless |
| Majorana character / `m_ββ` | D96 PMNS + mass readout | none | 0 | dimensionless / ratio-defined |
| `Σ m` | first-moment / full access | none | 0 | structural count |
| `Σ√m` | half-moment / neutral access | none | 0 | structural count |
| `Σ m²` | second-moment / doublet occupancy | none | 0 | structural count |
| `occMom` | octave-occupation moment | none | 0 | structural count |
| neutrino masses | D96 closed-form laws | none | 0 | scale-dependent |
| charged lepton hierarchy | spectral access reads | none | 0 | scale-dependent |
| quark masses | D96 ratios | `m_e` | 0 | scale-dependent |
| weak scale `v` | fine-structure denominator × spectral span | `v` | 0 | scale-dependent |
| `M_W` | weak coupling × `v` | `v` | 0 | scale-dependent |
| `M_Z` | `M_W` / `cosθ_W` | `v` | 0 | scale-dependent |
| Higgs mass `M_H` | occupancy fluctuation × octave radius | `v` | 0 | scale-dependent |
| `M_Pl` | `v`-anchored spectral amplification | `v` | 0 | scale-dependent |
| `G` (natural units) | `1/M_Pl²` | `v` | 0 | scale-dependent |
| `G` (SI) | natural `G` plus unit conversion | `v` + SI conversion | 0 | unit-convention dependent |
| `Λ` | branching-vacuum residual pressure | none | 0 | scale-dependent |
| `Ω_Λ`, `Ω_m` | octave-record information fractions | none | 0 | dimensionless / scale-dependent readout |
| `n_s` | octave-hierarchy tilt | none | 0 | scale-dependent |
| CMB peaks / `ℓ_1`, `r_21`, `r_31` | recombination-scale octave harmonics | none | 0 | scale-dependent |
| GPS / gravitational redshift / time dilation | metric readouts | SI / `v` boundary where applicable | 0 | unit-convention dependent |

---

## Referee-Safe Conclusion

AT produces **dimensionless structure first**. Dimensional observables appear only after calibration to anchors or unit conventions. The observable register is therefore mostly **catalog-driven but structurally constrained**, not a global proof of unique derivation from first principles.

