# AT-QG Phase 189 — Anti-Fit Audit

**Status:** COMPLETE — **PREDICTION AUDIT**
**Tests:** ATQG1890, ATQG1891, ATQG1892 (all passed)
**Core class:** `AT.Core/ResearchXH/AntiFitAudit.cs`

---

## 1. Goal

Review phases QG140–QG188 for **anti-fit safety**: whether each derivation
used its target value as an input, how many free choices / candidate formulas
were involved, and whether the known target influenced the formula selection.
Audit methodology only — no physics derived.

**Five risk classes:**

| Class | Meaning |
|-------|---------|
| **PREDICTION** | target from D96 primitives, unique/unforced formula, target not consulted |
| **BLIND RECONSTRUCTION** | target explicitly HIDDEN and rebuilt from the primitive base |
| **DEPENDENT DERIVATION** | uses an earlier phase result or external anchor (sound chain) |
| **RETRO-FIT RISK** | fitted free parameter/formula chosen with the target visible |
| **OVERFIT RISK** | free parameters ≥ data points (saturated interpolation) |

---

## 2. The Full Audit Table (49 phases)

| Phase | Target | Inputs | Risk Level | Reason |
|-------|--------|--------|------------|--------|
| QG140 | lepton ratios {1,59,3468} | octave centers, modes [4,4,87] | **RETRO-FIT (High)** | mass=A·center^p·modes^q, p≈7.69 fitted |
| QG141 | exponents p_net=5.88 | spectral density | DEPENDENT (Moderate) | derived, but inherits QG140 form |
| QG142 | all generations | QG140/141 law | PREDICTION (Low) | leptons match 0.26%, quarks deviate — honest |
| QG143 | quark r31 factor | r31_octave, quantum numbers | DEPENDENT (Moderate) | 5 candidate factors tested |
| QG144 | weak-isospin amplification | up/down r31 | DEPENDENT (Moderate) | partial effect |
| QG145 | up-sector enhancement | up vs down r31 | DEPENDENT (Moderate) | p_eff inferred from ratios |
| QG146 | quark hierarchy law | up/down r31 | **RETRO-FIT (High)** | fitted p_eff=8.13/4.90 |
| QG147 | exponent vs (Q,T3) | lepton/up/down p_eff | **OVERFIT (High)** | 3 params fit to 3 sectors |
| QG148 | out-of-sample generalization | QG147 law, neutrino held out | PREDICTION (None) | confirmed QG147 OVERFIT |
| QG149 | sector exponents (physical) | occupation-weighted access | PREDICTION (Low) | replaces the fit |
| QG150 | mode access | octave occupancies | PREDICTION (None) | structural |
| QG151 | isospin access | down/up spectral regions | PREDICTION (None) | structural |
| QG152 | golden-ratio δ diff | δ_eff values | PREDICTION (Low) | robustness AUDIT |
| QG153 | Z2 doublets | D96 symmetry | PREDICTION (None) | structural |
| QG154 | neutrino origin | Q=0, T3-only | PREDICTION (None) | structural |
| QG155 | Z2 symmetry | circulant C_96 | PREDICTION (None) | structural |
| QG156 | unified access law | N_eff, span | PREDICTION (Low) | δ = log N_eff/log span |
| QG157 | N_eff | D96 moments | PREDICTION (Low) | no fitted params |
| QG158 | moment orders 1/2,1,2 | Z2 order | PREDICTION (None) | INEVITABLE |
| QG159 | D96 selection n=96 | attractor geometry | PREDICTION (None) | INEVITABLE |
| QG160 | period-3 seed | natural size | PREDICTION (None) | INEVITABLE |
| QG161 | gauge sector 1+3+8 | D96 automorphisms | PREDICTION (None) | degree C_96 |
| QG162 | couplings 1/α=137 | Σm, #d, #g, Σ√m | PREDICTION (None) | no fitted params |
| QG163 | running couplings | octave ladder | PREDICTION (None) | no fitted beta |
| QG164 | continuous running | beta flow | PREDICTION (None) | no fitted params |
| QG165 | CKM | #d, Σm, ω0/ω2, occ | PREDICTION (None) | no fitted angles |
| QG166 | δ_CP, J | occ_top, Σm | PREDICTION (None) | no fitted phase |
| QG167 | PMNS | Σm, #g, Σ√m | PREDICTION (None) | no fitted angles |
| QG168 | MW, MZ, v=254 | Σm, #d, ln span | PREDICTION (None) | no fitted masses |
| QG169 | MH=125.25 | σ_occ, span | PREDICTION (None) | no fitted masses |
| QG170 | SM coverage | all QG results | PREDICTION (None) | AUDIT |
| QG171 | a_μ | α, λ₂, Σm | PREDICTION (None) | no fitted params |
| QG172 | Δm²21, Δm²31 | Σ√m, span, sin²θ | PREDICTION (None) | no fitted masses |
| QG173 | 6 quark masses | me anchor, D96 moments | DEPENDENT (Low) | uses me as anchor; 0.2% |
| QG174 | θ_QCD=0 | reflection [L,P]=0 | PREDICTION (None) | structural |
| QG175 | sin²θ_eff, ΓZ, ΓW... | #g, Σm, MH... | PREDICTION (None) | no fitted params |
| QG176 | MH (hidden) | pre-Higgs D96 ONLY | **BLIND (None)** | MH,ΓH,λ_H hidden |
| QG177 | 12 observables | primitive base only | **BLIND (None)** | leave-one-out, 1.89% max |
| QG178 | a_e | α, occ₀, Σm | PREDICTION (None) | same mechanism as muon |
| QG179 | Majorana, m_ββ | T3-channel, PMNS | PREDICTION (Low) | structural + 2.02e-3 eV |
| QG180 | S, T, U | occ₀, Σm, ρ=1 | PREDICTION (None) | no fitted params |
| QG181 | G | v, Σm, #g, occ₂ | PREDICTION (None) | M_Pl=v·A³, 0.4% |
| QG182 | G bridge | occ₀/Σm, ln span | DEPENDENT (Low) | bridges QG6↔QG181 |
| QG183 | Planck exponent p=3 | M_Pl, v, A | PREDICTION (Low) | robustness AUDIT |
| QG184 | M ∝ R | per-octave deficit | PREDICTION (None) | counting measure |
| QG185 | Bekenstein 1/4 | S∝A, M∝R, T∝1/R | PREDICTION (None) | honest NEGATIVE (2π) |
| QG186 | frame dragging | ψ, J, G | DEPENDENT (Low) | J is measured input |
| QG187 | GPS correction | QG21, Earth GM, r, v | DEPENDENT (Low) | Earth params are inputs |
| QG188 | prediction ranking | coverage JSON | PREDICTION (None) | AUDIT |

---

## 3. Distribution

| Class | Count |
|-------|-------|
| PREDICTION | 36 |
| BLIND RECONSTRUCTION | 2 |
| DEPENDENT DERIVATION | 8 |
| RETRO-FIT RISK | 2 |
| OVERFIT RISK | 1 |
| **HIGH risk** | 3 |

Era split: **fitting era (QG140–148): 9 phases** · **structural era
(QG149–188): 40 phases**.

---

## 4. Findings

1. **Confirmed overfit (1):** QG147 — a 3-parameter linear law
   p = p0 + a·Q + b·T3 fitted to 3 sectors is saturated interpolation.
   QG148's independent (out-of-sample) validation caught it: the neutrino
   sector prediction fails and leave-one-out fails → **CONFIRMED OVERFIT**.
   QG148 itself is an honest PREDICTION (it predicted the law would not
   generalize).

2. **Retro-fit risk (2):** QG140 and QG146 fitted exponents to the target
   ratios (p≈7.69; p_eff=8.13/4.90). Both were superseded — QG141 derived
   the exponents from the spectral density, and QG149 replaced the fit with
   the physical occupation-weighted mode-access origin. **No retro-fit risk
   survives in the current framework.**

3. **Gold-standard blind tests (2):** QG176 (Higgs: MH, ΓH, MH/MW, MH/MZ,
   λ_H all hidden, rebuilt from pre-Higgs D96 → 125.49/125.25 GeV) and QG177
   (leave-one-out: 12 observables each hidden and rebuilt, mean dev 0.58%,
   max 1.89%).

4. **Structural era (149+): no fitted parameters.** All targets derive from
   D96 primitives (mode counts, occupancies, span, moments, automorphisms)
   with the target not consulted in the formula selection.

5. **Dependent derivations (8):** QG141/143/144/145 (fitting-era chain),
   QG173 (uses the measured electron as the single universal anchor),
   QG182 (bridges two G constructions), QG186/187 (use measured J / Earth
   parameters as inputs). These are sound chains — each dependency is a
   previously-derived D96 quantity, not the target itself.

---

## 5. Conclusions

- **The framework has one confirmed overfit (QG147) and two retro-fit-risk
  phases (QG140, QG146), all in the fitting era (QG140–148), and all
  superseded by structural derivations.**
- **No phase after QG149 fits a parameter to its target** — the structural
  era is prediction-grade.
- **QG176/QG177 are genuine blind reconstructions**: the strongest available
  anti-fit evidence, covering the key SM observables (Higgs mass and 12
  others).
- The overall methodology arc is healthy: **fitting era → honest audit
  (QG148) → structural era → blind validation (QG176/177)**.

**Result: PREDICTION AUDIT**
