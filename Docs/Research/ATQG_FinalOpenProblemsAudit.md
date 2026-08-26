# AT-QG Phase 198 — Final Open Problems Audit

**Status:** COMPLETE — **OPEN PROBLEMS AUDIT**
**Tests:** ATQG1980, ATQG1981, ATQG1982 (all passed)
**Core class:** `AT.Core/ResearchXH/OpenProblemsFinalAudit.cs`
**Sources:** `Docs/ATQG_PhysicsCoverage.json` (open questions, observables, gr topics) + `Docs/ATQG_Predictions.json` (registry)

---

## 1. Goal

List **ALL unresolved physics questions** of the AT-QG program. Exclude
resolved, partial-resolved, and audit-only entries. Classify each problem
(FOUNDATIONAL / GRAVITY / STANDARD MODEL / PREDICTION) with why it is still
open, its blocking impact, and an estimated priority. Output: the Top-20
open problems ranked by importance.

---

## 2. Exclusion & Inclusion Rules

**Excluded** (from the coverage register statuses):

- **RESOLVED** — matter = deficit (QG194), independent matter sector (QG195),
  2D native program (QG197)
- **PARTIALLY-SOLVED** — the ψ/Weyl field register entry
- **AUDIT / COVERAGE** — methodology phases (QG170, QG188, QG189, …)

**Included**:

- OPEN, OPEN-AXIOM, PARTIALLY-OPEN, FALSIFIABLE-PENDING, PREDICTED-NO-DATA
- PARTIAL origin / law / mapping entries (genuinely unresolved derivations)
- Every registered prediction whose outcome is still `null` (PENDING)

---

## 3. The Catalog (20 problems)

| Category | Count |
|----------|-------|
| PREDICTION | 5 |
| STANDARD MODEL | 8 |
| GRAVITY | 5 |
| FOUNDATIONAL | 2 |

---

## 4. Top-20 Ranking

Ranking: **score = impact·3 + feasibility·2 + falsifiability·2** (deterministic
weights, same as QG188 for cross-phase consistency). Priority bands:
**HIGH ≥ 30, MEDIUM 18–29, LOW < 18.**

| # | ID | Category | Priority | Score | Title |
|---|----|----------|----------|-------|-------|
| 1 | P1 | PREDICTION | HIGH | 35 | 106 GeV resonance |
| 2 | SM1 | STANDARD MODEL | HIGH | 33 | Exact neutrino masses m1,m2,m3 |
| 3 | SM3 | STANDARD MODEL | HIGH | 32 | Neutrino mass ordering (experimental) |
| 4 | P2 | PREDICTION | HIGH | 31 | 0νββ m_ββ = 2.02 meV |
| 5 | P3 | PREDICTION | HIGH | 30 | Sector-ladder collider spectrum |
| 6 | G2 | GRAVITY | MEDIUM | 26 | Flat rotation-curve α = 0 origin |
| 7 | G3 | GRAVITY | MEDIUM | 22 | Conformal optics: redshift without lensing / δ=0 bending |
| 8 | G5 | GRAVITY | MEDIUM | 21 | Gravitational-wave polarization sector |
| 9 | F1 | FOUNDATIONAL | MEDIUM | 21 | Metric ansatz uniqueness (g = ρ^(2/d)η) |
| 10 | SM5 | STANDARD MODEL | MEDIUM | 21 | Quark hierarchy — unified law |
| 11 | SM2 | STANDARD MODEL | MEDIUM | 21 | Quark running-scale / MS̄ conversion |
| 12 | P10 | PREDICTION | MEDIUM | 21 | Regular-core black-hole profile |
| 13 | SM4 | STANDARD MODEL | MEDIUM | 21 | Lepton hierarchy — exact law |
| 14 | G1 | GRAVITY | MEDIUM | 20 | Hawking temperature with ψ ≠ 0 |
| 15 | F2 | FOUNDATIONAL | MEDIUM | 20 | Exact Bekenstein 1/4 coefficient |
| 16 | SM6 | STANDARD MODEL | LOW | 17 | Family index origin |
| 17 | G4 | GRAVITY | LOW | 17 | Curvature-sourced Poisson equation |
| 18 | P9 | PREDICTION | LOW | 15 | Common sector granularity scale |
| 19 | SM7 | STANDARD MODEL | LOW | 14 | Golden-ratio hierarchy robustness |
| 20 | SM8 | STANDARD MODEL | LOW | 14 | Physical calibration ladder |

Priority distribution: HIGH 5, MEDIUM 10, LOW 5.

---

## 5. Why Each Problem Is Still Open

### PREDICTION

- **P1 — 106 GeV resonance** (QG132/QG188A/QG190): pre-registered window
  99–114 GeV (central 106.39 GeV). The QG188A evidence audit is INCONCLUSIVE:
  the 95 GeV excess aligns with the 91.19 rung, and the 106 GeV window is
  neither confirmed nor excluded. **Blocks:** the primary falsifiable target;
  a null result would rule out the Z-anchor electroweak calibration.
- **P2 — 0νββ m_ββ = 2.02 meV** (QG179/QG191): pre-registered; below current
  experimental reach (limits 0.036–0.156 eV). **Blocks:** the only
  lepton-number-violation target; FALSIFIED if a limit lands below 2.02 meV.
- **P3 — Sector-ladder spectrum** (QG130/QG192): 9 resonances 106.39→263.43
  GeV pre-registered (rungs 6/9/11 align with t/H/Z); no dedicated search run.
  **Blocks:** the whole sector-ladder program lacks experimental validation.
- **P9 — Common sector granularity** (QG69): qualitative, free scale,
  Planck-reach — no plausible probe.
- **P10 — Regular-core BH profile** (QG75): M(1−e^(−r³/r_c³)) differs from
  GR/Hayward/Bardeen; only horizon-scale (EHT-class) imaging could test.

### STANDARD MODEL

- **SM1 — Exact neutrino masses m1,m2,m3** (QG172): splittings derived and
  m1=0 normal ordering derived (QG179), but the absolute mass scale is open.
  **Blocks:** cosmology (Σm_ν, structure formation) and the 0νββ rate.
- **SM2 — Quark running-scale/MS̄ conversion** (QG173): all six masses derived
  within 0.2%, but the matching to scale-dependent PDG values is open.
- **SM3 — Neutrino mass ordering** (QG179): normal ordering derived; JUNO/DUNE
  can measure the sign of Δm²31 but have not yet.
- **SM4 — Lepton hierarchy exact law** (QG142): PARTIAL LAW — leptons match
  within 0.26%, quarks deviate; exact unified law open.
- **SM5 — Quark hierarchy unified law** (QG146): QG146 fit superseded by
  QG149 occupation-weighted exponents; one closed unified law open.
- **SM6 — Family index origin** (QG135): PARTIAL ORIGIN — index emerges from
  intra-sector octaves but full origin open (robustness partial, QG136).
- **SM7 — Golden-ratio hierarchy robustness** (QG152): PARTIAL ROBUSTNESS —
  sensitive to parameter choices.
- **SM8 — Physical calibration ladder** (QG129): PARTIAL MAPPING — ladder
  ratios vs SM mass ratios not closed.

### GRAVITY

- **G1 — Hawking temperature with ψ ≠ 0** (QG24): no phase derives T ∝ 1/R
  explicitly with ψ ≠ 0 (QG13/QG22 native T∝R partly conformal-flatness
  artifact; QG184 restores 1/R).
- **G2 — Flat rotation-curve α = 0** (G4-ME4): SEMI-NATURAL — imposed by
  symmetry, not derived.
- **G3 — Conformal optics** (QG21/QG26): redshift survives but lensing and
  Shapiro delay vanish in the conformal (ψ=0) sector (PPN γ=−1); no clean
  probe isolates the scalar sector.
- **G4 — Curvature-sourced Poisson** (G4-O0): source = (ln ρ)″, not density;
  no Newtonian field in uniform-density / shell-exterior regions; no feasible
  probe.
- **G5 — GW polarization sector** (QG18/QG43): scalar GW energy/speed OK but
  polarization NO MATCH; only GW strain requires the tensor (ψ) sector.

### FOUNDATIONAL

- **F1 — Metric ansatz uniqueness** (G4-A0): g = ρ^(2/d)η is PREFERRED but not
  UNIQUE — flat η is a defining axiom, not derived.
- **F2 — Exact Bekenstein 1/4** (QG12/13/184/185/196): structure fully
  derived; QG196 PROVES the exact 1/4 is impossible within D96/TRM without
  importing π (bits-per-cell = π; 1/occ₀=1/4 is wrong-units → 1/(16π)).

---

## 6. Findings

1. **The 106 GeV resonance (P1) remains the single most important open
   problem** — highest score (35) across impact, feasibility and
   falsifiability, consistent with QG188's recommendation.
2. **The neutrino sector is the most crowded open cluster** (SM1, SM3, P2 at
   ranks 2–4): absolute masses, ordering, and the 0νββ rate are three linked
   gates of the same origin.
3. **The gravitational tensor sector is the main gravity gap** (G1, G3, G5):
   the conformal (ψ=0) sector reproduces redshift but not lensing, the Hawking
   law is not derived with ψ ≠ 0, and GW polarization is undecided.
4. **Two foundational axioms remain un-derived** (F1 metric uniqueness, F2
   Bekenstein 1/4). F2 is proven-impossible within the framework; F1 is a
   preference, not a derivation.
5. **Every open problem carries a blocking impact** — nothing on the list is
   decorative; each gates a further derivation, a falsifiable target, or an
   experimental closure.

---

## 7. Recommended Next Target

**P1 — 106 GeV resonance** (score 35, HIGH): the highest-ranked open problem.
It is pre-registered (QG190), its evidence audit is INCONCLUSIVE (QG188A),
and it is testable now at LHC Run 3. The runner-up cluster is the neutrino
sector (SM1/SM3/P2, scores 31–33), where JUNO/DUNE and ton-scale 0νββ
experiments are within reach.
