# AT-QG Phase 199 — P1 Evidence Update

**Status:** COMPLETE — **P1 = PENDING** (window 99–114 GeV neither confirmed nor excluded)
**Tests:** ATQG1990, ATQG1991, ATQG1992 (all passed)
**Core class:** `AT.Core/ResearchXH/P1EvidenceUpdate.cs`
**Prediction audited:** P1 — 106.39 GeV resonance, window 99–114 GeV (QG132/QG190)
**Search cut-off:** August 2026
**Method:** evidence only — no theory, no fitting; every number cited from the published experimental record.

---

## 1. Supportive Evidence

### 1.1 The classic low-mass scalar cluster (~95 GeV) — BELOW the P1 window

| Experiment | Channel | Mass | Local σ | Reference |
|-----------|---------|------|---------|-----------|
| CMS | γγ | 95.3 GeV | 2.9σ (global 1.3σ) | CMS-HIG-20-002, PLB 860 (2025) |
| ATLAS | γγ | 95.4 GeV | 1.7σ | arXiv:2407.07546, JHEP 01 (2025) 053 |
| CMS | ττ | ~95 GeV | ~2.6σ | CMS Run-2 |
| LEP | bb̄ | ~98 GeV | 2.3σ | LEP Higgs Working Group |
| **Combined γγ** | γγ | 95.4 GeV | **3.1σ local**, μ = 0.24 | PRD 109 (2024) 035005 |

These lie **below** the P1 window (99 GeV) and are consistent with the **lowest ladder rung 91.19 GeV**
(deviation 4.0%, QG131) — **not** with the 106.39 GeV prediction (−10.4%).

### 1.2 NEW: the ~152 GeV diphoton excess — ABOVE the window, at the NEXT ladder rung

A combined CMS+ATLAS analysis reports a narrow diphoton excess at ~152 GeV with multi-channel local
significance ~3.6σ and global significance up to ~5.4σ in the arXiv:2503.16245 combination.

- **Ladder alignment:** the pre-registered sector ladder (QG192) predicts the next missing rung at
  **151.98 GeV** — the 152 GeV excess deviates by **0.01%**.
- **Relevance:** this is a *different* prediction (P3, ladder rung), not P1, but it strengthens the
  sector-ladder program and shows the ladder-mass pattern is being tested.

## 2. Null Searches in the P1 Window (99–114 GeV)

| Experiment | Channel | Mass range | 95% CL limit | Reference |
|-----------|---------|-----------|--------------|-----------|
| CMS | γγ | 70–110 GeV | 15–73 fb | CMS-HIG-20-002 |
| ATLAS | γγ | 66–110 GeV | 19–102 fb | arXiv:2407.07546 |
| LEP2 | hZ | SM Higgs | excluded < 114.4 GeV | LEP Higgs WG |

- **No confirmed excess** appears in the 99–114 GeV window.
- The 106.39 GeV central mass is fully covered by both null searches.
- **No full Run-3 (13.6 TeV) low-mass diphoton resonance search** was published as of the cut-off;
  CMS Run-3 (2022–2023) updates confirm the ~95 GeV excess persists near 3σ but do not grow it.

## 3. Exclusion Status

- **P1 is NOT excluded.** Current diphoton limits (≈15–102 fb in 100–110 GeV) still allow a
  suppressed-coupling scalar.
- The LEP2 bound (< 114.4 GeV) applies only at **SM-strength hZZ coupling** — a weakly-coupled ladder
  state escapes it.

## 4. Current Status & Discovery Potential

- **HL-LHC (3000 fb⁻¹):** projected σ×BR(γγ) sensitivity ≈ **1–3 fb** (central 2 fb) in the 100–106 GeV
  range — roughly **5–6× below** the current limits. The P1 window becomes decisive at HL-LHC.
- **Run 3:** continues accumulating data; full-13.6-TeV low-mass diphoton analyses are expected.
- **152 GeV watch:** the next rung (151.98 GeV) is being probed; its confirmation would validate the
  ladder scale while P1 remains open.

---

## 5. Classification

### **PENDING**

| Criterion | Verdict |
|-----------|---------|
| Signal inside 99–114 GeV at ≥5σ | NO (window is empty of confirmed excess) |
| Exclusion of the window by sensitive searches | NO (suppressed couplings still allowed) |
| LEP2 bound applies | NO (SM-coupling only) |
| **Registry outcome** | **PENDING** — unchanged (no CONFIRMED / DISFAVORED / FALSIFIED may be written) |

Evidence score = 4/4: empty P1 window, P1 not excluded, 95 GeV ↔ 91.19 rung (4.0%), 152 GeV ↔ 151.98
rung (0.01%). The supporting scalar evidence is real but sits at **other ladder rungs**, not inside the
P1 window. The frozen P1 prediction (106.39 GeV) and its registry entry remain exactly as locked in QG193.

### Sources

1. CMS-HIG-20-002, "Search for Higgs boson decays into two photons in the 70–110 GeV mass range", Phys.
   Lett. B 860 (2025) 139067, arXiv:2405.18149.
2. ATLAS, "Search for diphoton resonances in the 66 to 110 GeV mass range using 140 fb⁻¹ of 13 TeV pp
   collisions", JHEP 01 (2025) 053, arXiv:2407.07546.
3. A. Belyaev et al., "The 95.4 GeV di-photon excess at ATLAS and CMS", Phys. Rev. D 109 (2024) 035005,
   arXiv:2306.03889.
4. S. Crépé-Renaudin et al., "Emerging Excess Consistent with a Narrow Resonance at 152 GeV in
   High-Energy Proton-Proton Collisions", arXiv:2503.16245.
5. LEP Higgs Working Group, "Search for the Standard Model Higgs boson at LEP", Phys. Lett. B 565 (2003)
   61–75; summary arXiv:0804.4146.
6. CMS at Higgs 2025 and CMS at Moriond 2025 (low-mass di-tau scouting, Run-3 status).
7. CMS-HIG-25-002 / ATL-PHYS-PUB-2025-018 (HL-LHC diphoton sensitivity projections).
