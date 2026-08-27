# AT-QG Observable Selection Audit

**Status:** PERMANENT SELECTION AUDIT — 29 OBSERVABLES, 7 NATURAL, 19 SECONDARY, 3 POST-HOC

---

## Summary

This audit separates the observable targets by origin class using the documented selection rules from QG259.

### Counts

| Class | Count | Fraction |
|---|---:|---:|
| Natural | 7 | 0.241 |
| Secondary | 19 | 0.655 |
| PostHoc | 3 | 0.103 |

### Formulas

- Natural fraction = `7 / 29 = 0.241`
- Secondary fraction = `19 / 29 = 0.655`
- PostHoc fraction = `3 / 29 = 0.103`

---

## 29-Observable Classification

| Observable | Class | Origin path | First appearance | Prediction status | Calibration dependence | Target-selection dependence |
|---|---|---|---|---|---|---|
| Family count | Natural | D96 structural identity | early structural chapters / QG259 natural core | tested | none | none |
| θ_QCD = 0 | Natural | automorphism / structural identity | QG259 natural core | tested | none | none |
| Blind Higgs reconstruction #1 | Natural | hidden-target Higgs audit | QG176 | tested | `v`-anchored result | low |
| Blind Higgs reconstruction #2 | Natural | hidden-target Higgs audit | QG176 / QG169 | tested | `v`-anchored result | low |
| P1 (106 GeV resonance) | Natural | pre-registered prediction | QG132 / frozen QG190 | pending | weak-scale calibration | low |
| P2 (0νββ m_ββ) | Natural | pre-registered prediction | QG179 / frozen QG191 | pending | PMNS + mass readout only | low |
| P3 (sector-ladder spectrum) | Natural | pre-registered prediction | QG128-132 / frozen QG192 | supported | ladder scale calibration | low |
| `1/α_em` | PostHoc | asserted dictionary / coupling audit | QG162 / QG250 | tested | none | high |
| `α_weak` | Secondary | spectral ratio | QG162 | tested | none | medium |
| `α_strong` | Secondary | spectral ratio | QG162 | tested | none | medium |
| `sin²θ_W` | Secondary | spectral ratio | QG162 / QG168 | tested | none | medium |
| CKM matrix | Secondary | octave-transition read | QG165 | tested | none | medium |
| PMNS matrix | Secondary | T3-only read | QG167 | tested | none | medium |
| `S, T, U` | Secondary | precision-observable readout | QG180 | tested | none | medium |
| electron `g-2` | Secondary | spectral correction | QG178 | tested | none | medium |
| Majorana character / `m_ββ` | Natural | hidden-target / pre-registered mass readout | QG179 / QG191 | pending | none | low |
| `Σ√m` | Secondary | half-moment / neutral access | QG157 / QG158 | tested | none | medium |
| `Σm` | Secondary | first-moment / full access | QG157 / QG158 | tested | none | medium |
| `Σm²` | Secondary | second-moment / doublet occupancy | QG157 / QG158 | tested | none | medium |
| `occMom` | Secondary | octave-occupation moment | QG157 / QG155 | tested | none | medium |
| neutrino masses | Secondary | D96 closed-form laws | QG172 / QG203 | tested | none | medium |
| charged lepton hierarchy | Secondary | spectral access reads | QG140 | tested | `m_e` only for absolute scale | medium |
| quark masses | Secondary | D96 ratios | QG173 / QG204 | tested | `m_e` | medium |
| weak scale `v` | Secondary | fine-structure denominator × span | QG168 | tested | `v` | medium |
| `M_W` | Secondary | weak coupling × `v` | QG168 | tested | `v` | medium |
| `M_Z` | Secondary | `M_W` / `cosθ_W` | QG168 | tested | `v` | medium |
| Higgs mass `M_H` | Natural | hidden-target / Higgs audit | QG169 | tested | `v` | low |
| `M_Pl` | Secondary | `v`-anchored amplification | QG181 | tested | `v` | medium |
| `G` (natural units) | Secondary | `1/M_Pl²` | QG181 | tested | `v` | medium |
| `G` (SI) | Secondary | natural `G` + conversion | QG181 | tested | `v` + SI conversion | medium |
| `Λ` | Secondary | residual vacuum pressure | QG230 | tested | none | medium |
| `Ω_Λ`, `Ω_m` | Secondary | octave-record fractions | QG234 | tested | none | medium |
| `n_s` | PostHoc | retro-selection sensitive | QG237 / QG239 | tested | none | high |
| CMB peaks (`ℓ_1`, `r_21`, `r_31`) | PostHoc | retro-selection sensitive | QG238 / QG239 | tested | none | high |
| gravitational redshift / time dilation / GPS | Secondary | metric readouts | QG21 / QG187 | tested | SI / `v` boundary where applicable | medium |

---

## Referee-Safe Conclusion

The audited observables are mostly secondary catalog matches, with a smaller natural core and a small post-hoc minority. The natural fraction is `7/29`, the secondary fraction is `19/29`, and the post-hoc fraction is `3/29`. This cleanly separates pre-registered predictions from retrospective validations.

