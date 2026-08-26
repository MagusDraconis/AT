# AT-QG Prediction Outcome Dashboard

**Single source of truth for the external validation of the registered predictions.**

- Purpose: single source of truth for the external validation of the registered predictions
- Registry rule: No future phase may modify a registered prediction. Frozen values never change; only the state may advance forward.
- Locked by: AT-QG Phase 193 (registry) / Phase 202 (dashboard)
- Last updated: 2026-08-22
- States: PENDING | SUPPORTED | CONFIRMED | DISFAVORED | FALSIFIED

## Outcome Table

| ID | Prediction | State | Frozen value | Support level | Last audit | Next experiment |
|----|-----------|-------|--------------|---------------|------------|-----------------|
| P1 | 106 GeV resonance | **PENDING** | 106.39 GeV central; window 98.79–113.99 GeV (stated 99–114 GeV) | NONE (inside window) — no excess at 106.39 GeV; window open | QG199 (P1 evidence update) | HL-LHC 3000 fb⁻¹ diphoton (projected 1–3 fb sensitivity in 100–106 GeV) |
| P2 | 0νββ m_ββ = 2.02 meV | **PENDING** | m_ββ = 2.02 meV (computed 2.0222 meV); ±10% (1.8–2.2 meV) | NONE — below current experimental reach | QG191 (pre-registration) | nEXO / LEGEND-1000 ton-scale (0νββ half-life sensitivity ~10²⁸ yr) |
| P3 | Sector-ladder spectrum | **SUPPORTED** | 9 resonances: 106.39 → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities ×10 + ×1; width 15.20 GeV | MODERATE — 151.98 rung aligns with the ~152 GeV excess (2.80σ) | QG200 (sector ladder evidence audit) / QG201 (statistics audit) | HL-LHC diphoton confirmation of the 152 GeV excess; full Run-3 searches |

## P1 — 106 GeV resonance

**State:** `PENDING`

- **Frozen value:** 106.39 GeV central; window 98.79–113.99 GeV (stated 99–114 GeV)
- **Current evidence:** No confirmed signal in the 99–114 GeV window (QG188A INCONCLUSIVE, QG199). Classic low-mass scalar excesses persist at ~95 GeV (combined γγ 3.1σ = the 91.19 rung, not P1). CMS γγ 15–73 fb and ATLAS γγ 19–102 fb limits do not exclude P1; LEP2 114.4 GeV bound is SM-coupling only.
- **Support level:** NONE (inside window) — no excess at 106.39 GeV; window open
- **Last audit:** QG199 (P1 evidence update)
- **Next experiment:** HL-LHC 3000 fb⁻¹ diphoton (projected 1–3 fb sensitivity in 100–106 GeV)
- **Falsification:** No signal in statistically sensitive searches of the 99–114 GeV window (DISFAVORED/FALSIFIED)

## P2 — 0νββ m_ββ = 2.02 meV

**State:** `PENDING`

- **Frozen value:** m_ββ = 2.02 meV (computed 2.0222 meV); ±10% (1.8–2.2 meV)
- **Current evidence:** No experiment has reached the 2.02 meV sensitivity (current limits 0.036–0.156 eV, QG179). The prediction is below all existing 0νββ limits.
- **Support level:** NONE — below current experimental reach
- **Last audit:** QG191 (pre-registration)
- **Next experiment:** nEXO / LEGEND-1000 ton-scale (0νββ half-life sensitivity ~10²⁸ yr)
- **Falsification:** Significant exclusion below 2.02 meV (a measured upper limit < 2.02 meV FALSIFIES)

## P3 — Sector-ladder spectrum

**State:** `SUPPORTED`

- **Frozen value:** 9 resonances: 106.39 → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities ×10 + ×1; width 15.20 GeV
- **Current evidence:** The 151.98 rung matches the combined CMS+ATLAS ~152 GeV diphoton excess (local 3.6σ, global up to 5.4σ, arXiv:2503.16245). Alignment MODERATE SUPPORT: 0.0132% deviation, p(any rung) = 0.26% (1 in 386), z = 2.80σ (QG201). SM anchors Z/H/t confirm the ladder scale (QG200).
- **Support level:** MODERATE — 151.98 rung aligns with the ~152 GeV excess (2.80σ)
- **Last audit:** QG200 (sector ladder evidence audit) / QG201 (statistics audit)
- **Next experiment:** HL-LHC diphoton confirmation of the 152 GeV excess; full Run-3 searches
- **Falsification:** A sensitive search excludes any frozen rung (limit below the rung energy FALSIFIES)

## State Transitions

```
PENDING -> SUPPORTED -> CONFIRMED
PENDING -> DISFAVORED -> FALSIFIED
```

A state may only advance forward; frozen values never change (QG193).
