# AT-QG Phase 201 — Ladder Statistics Audit

**Status:** COMPLETE — **MODERATE SUPPORT**
**Tests:** ATQG2010, ATQG2011, ATQG2012 (all passed)
**Core class:** `AT.Core/ResearchXH/LadderStatisticsAudit.cs`
**Inputs (frozen QG192 only):** 9 predicted rungs 106.39, 136.78, 151.98, 182.38, 197.58, 212.78, 227.97, 243.17, 263.43 GeV; observed excess 152.0 GeV (arXiv:2503.16245)
**Method:** deterministic statistics — no new theory, no new ladder values, no fitting.

---

## 1. The Question

Is the alignment of the ~152 GeV excess with the frozen 151.98 GeV ladder rung
statistically significant, or a chance coincidence?

**Null hypothesis:** the observed excess mass is drawn *uniformly* over the
search range [95, 270] GeV (span 175 GeV — the full low/intermediate-mass
window covered by ATLAS+CMS γγ searches). Under the null, what is the
probability that the mass lands within the observed tolerance of ANY of the
9 frozen rungs?

---

## 2. Computed Quantities

### 2.1 Observed deviation and nearest-rung distance

| Quantity | Value |
|----------|-------|
| Observed excess | 152.0 GeV |
| Nearest frozen rung | 151.98 GeV |
| **Nearest-rung distance** | **0.020 GeV** |
| Deviation τ = \|152.0/151.98 − 1\| | **0.0132%** (the stated "0.01%" is the rounded figure) |
| Mean rung spacing | ~15.2 GeV (unit quantum MZ/6) |
| Excess vs spacing | ~760× closer to its rung than the typical spacing |

### 2.2 Nearest-rung distances within the ladder

| Rung [GeV] | Nearest-neighbour distance [GeV] |
|------------|----------------------------------|
| 106.39 | 30.39 (gap to 136.78) |
| 136.78 | 15.20 |
| 151.98 | 15.20 |
| 182.38 | 15.20 |
| 197.58 | 15.20 |
| 212.78 | 15.19 |
| 227.97 | 15.19 |
| 243.17 | 15.20 |
| 263.43 | 20.26 (top quantum) |

The ladder is quasi-uniform (15.2 GeV unit quantum); the observed 0.020 GeV
distance is ~760× smaller than the nearest-neighbour spacing.

### 2.3 Random coincidence rate and look-elsewhere

| Quantity | Formula | Value |
|----------|---------|-------|
| Per-rung window | 2·τ·E_rung | Σ = 0.4533 GeV |
| Search span | 95–270 GeV | 175 GeV |
| **p(any rung)** | window/span | **0.2591% → 1 in 386** |
| p(151.98 alone) | 2·τ·151.98/span | 0.02286% → 1 in 4375 |
| LEE trial factor | any-rung / single-rung | ~11.3 |
| **z(any rung)** | Φ⁻¹(1−p_any) | **2.80σ** |
| **z(151.98 alone)** | Φ⁻¹(1−p_one) | **3.50σ** |

**Key point:** the any-rung probability (0.26%) is *already* look-elsewhere
corrected — the covered window sums all 9 rungs, so no extra trial factor
applies. The 2.80σ is the honest significance of a ladder-wide alignment.

### 2.4 Probability of a 0.0132% match by chance

- **Against any of the 9 rungs:** 0.26% (1 in 386) → 2.80σ.
- **Against the 151.98 rung alone:** 0.023% (1 in 4375) → 3.50σ.

If the 151.98 rung had been the *only* pre-registered prediction, the
coincidence would be strong (3.5σ); the existence of the full 9-rung ladder
widens the target and reduces the significance to moderate (2.8σ).

---

## 3. Classification

### **MODERATE SUPPORT**

| Band | p_any | z | Verdict |
|------|-------|---|---------|
| COINCIDENCE | > 5% | < 1.6σ | — |
| WEAK SUPPORT | 1–5% | 1.6–2.3σ | — |
| **MODERATE SUPPORT** | **0.1–1%** | **2.3–3.1σ** | **← 0.26%, 2.80σ** |
| STRONG SUPPORT | < 0.1% | ≥ 3.1σ | — |

Evidence score = 4/4: τ < 0.05% ✓, p_any < 1% ✓, z_any ≥ 2σ ✓, single-rung
p < 0.1% ✓.

---

## 4. Findings

1. **The alignment is unlikely by chance:** 1-in-386 after look-elsewhere over
   all 9 frozen rungs (2.80σ). It is not a fluke at the ~3σ level.
2. **It does not reach 5σ on its own.** The ladder-wide 2.80σ is MODERATE;
   the isolated 151.98 rung would be 3.50σ.
3. **Consistent with the excess's own significance:** the ~152 GeV excess has
   local ~3.6σ / global up to ~5.4σ (arXiv:2503.16245). The ladder alignment
   (2.8σ) and the excess significance are independent evidence that reinforce
   each other.
4. **The stated "0.01%" understates the exact deviation** (0.0132%), but the
   conclusion is unchanged in either band.
5. **No ladder-value changes:** all inputs are the frozen QG192 spectrum.

---

## 5. Conclusion

The 152 GeV ↔ 151.98 GeV alignment is **MODERATE SUPPORT**: a 0.0132%
(0.020 GeV) coincidence that would occur by chance only 1-in-386 times across
the 9-rung ladder (2.80σ), or 1-in-4375 against the single rung (3.50σ). The
alignment is statistically meaningful but not yet conclusive; combined with
the excess's own global significance it strengthens the sector-ladder
prediction (QG192/QG200) while the ladder as a whole awaits decisive HL-LHC
data.
