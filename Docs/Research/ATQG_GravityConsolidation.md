# AT-QG Gravity Consolidation Audit

**Scope:** all completed gravity phases — QG6, QG12, QG13, QG21, QG22, QG26, QG103, QG181, G4-G0,
G4-G2, G4-G3, G4-O0 (+ supporting phases QG17-19, QG23-25, QG43, QG48, GW reconciliation, G4-ME).
**Method:** consolidation of existing reports only. No new physics, no new derivations.
**Status:** COMPLETED (audit of existing results, 2026-08-21)

---

## 1. Unified Gravity Roadmap

The completed phases form a coherent chain with three distinct frameworks that are not yet
fully connected:

### Chain A — Kinematic gravity from counting (G4 program)

```
causal order (Q-events, primitive)
└─► counting measure ρ (actualization density, primitive)
    ├─► conformal structure from causal order (D, G4-M0)
    ├─► Lorentzian signature (D, G4-L0)
    ├─► metric ansatz g = ρ^(2/d)η  (PREFERRED, G4-A0 — exponent 2/d uniquely selected
    │    by counting-measure preservation, but not UNIQUE)
    ├─► curvature R from ρ  (D, G4-G0/G2)
    ├─► Einstein tensor G_μν(ρ,∂ρ,∂²ρ)  (D, G4-G0/G2 — d≥3; ≡0 in d=2)
    ├─► conservation (Bianchi/Gauss–Bonnet)  (D, G4-G0)
    ├─► Einstein equation G=κT  (P — identity only, T≡G/κ; no independent matter, G4-G3)
    ├─► weak-field observables  (G4-O0)
    │     ├─► acceleration a=−∇Φ  (KNOWN GR-LIKE)
    │     ├─► gravitational redshift Δν/ν=−ΔΦ  (KNOWN GR-LIKE)
    │     ├─► lensing ∝ ΔΦ  (KNOWN GR-LIKE — as weak-field form only, see §2)
    │     └─► expansion H=ρ̇/ρ  (KNOWN GR-LIKE)
    ├─► Newton-like gravity GM_eff = m₀r₀/(d·ρ̄)  (D, QG6 — G native, only GM product)
    ├─► flat rotation curves from log-deficit  (P — SEMI-NATURAL α=0, G4-ME)
    └─► BDG −2 discretization normalization  (RU — imported, G4-L12)
```

### Chain B — Conformal observable consequences (QG program, ψ=0)

```
g = ρ^(2/d)η with ψ=0 (conformal flatness)
├─► light: null geodesics straight → redshift WITHOUT lensing  (QG21, QG26)
│     ├─► redshift survives (g_00 varies)  (MATCH)
│     └─► deflection/convergence/delay all = 0  (NO MATCH — PPN γ=−1)
├─► no tensor GWs (ψ=0 freezes graviton)  (QG18, QG22)
├─► no Hawking T ∝ 1/R (E∝R^d gives T∝R)  (QG13)
└─► black-hole entropy S ∝ Area  (MATCH, QG12 — area law native)
```

### Chain C — Tensor completion (ψ ≠ 0, required but not yet present)

```
ψ-field (spin-2, 2 d.o.f.) — MINIMAL NEW PRIMITIVE (QG24)
├─► restores lensing (ψ≠0 → Weyl≠0)  (QG22: CONFORMAL-FLATNESS ARTIFACT)
├─► restores tensor GWs (2 polarizations)  (QG18/19: NEW PRIMITIVE)
├─► restores Mercury perihelion γ=β=+1  (QG103: MATCH via ψ)
├─► Hawking T partly restorable (mass-radius issue separate)  (QG22)
└─► QG43: only GW polarization intrinsically requires spin-2;
     lensing/Shapiro/γ need only a 1-d.o.f. scalar ψ
```

### Chain D — Newton constant from D96 (QG181)

```
D96 spectrum → Σm=95, #g=44, occ₂=87 → A=363,660
└─► v=(Σm+#doublets)·ln(span)=254.37 GeV (QG168)
    └─► M_Pl = v·A³ = 1.22335e19 GeV (dev 0.2006%)
        └─► G = 1/M_Pl² = 6.6476e-11 m³/kg/s² (dev 0.3999%)
```

**Key structural fact:** Chain A/B operate on the counting-measure ρ (deficit gravity, arcsec-scale
weak field); Chain D operates on the D96 particle spectrum (Planck-scale magnitude). The two "G's" —
QG6's GM_eff (native scale, magnitude free) and QG181's G (absolute magnitude from D96) — are **not yet
connected** (§2, §4).

---

## 2. Contradictions Matrix

| # | Contradiction | Phase A | Phase B | Nature | Status |
|---|---|---|---|---|---|
| C1 | **Lensing present vs absent** | G4-O0: lensing deflection ∝ ΔΦ, KNOWN GR-LIKE (weak-field form) | QG21/QG26: deflection = 0 exactly (null geodesics conformally invariant, PPN γ=−1) | Apparent | **RESOLVED** — QG21 explicitly corrects G4-O0: "lensing" there was a *potential difference*, not a *deflection angle*. The weak-field form and the null-geodesic result measure different objects. |
| C2 | **G native but normalization imported (QG6) vs G fully derived (QG181)** | QG6: G is native but GM_eff magnitude free; BDG −2 imported; G–M non-separable | QG181: G = 1/M_Pl² from D96 spectral content, dev 0.4% | Real gap | **UNRESOLVED** — different frameworks (deficit abundance vs D96 spectrum); no report connects QG6's GM_eff to QG181's M_Pl. Both claims can be true in their own framework, but the bridge is missing. |
| C3 | **Hawking T needs 0 d.o.f. (QG24) vs Hawking T NO MATCH (QG13)** | QG24: "Hawking T (T=κ/2π): 0 additional d.o.f. — derived from horizon profile, no independent mode" | QG13: NO MATCH — native deficit energy E∝R^d gives T∝R, opposite of Hawking | Apparent | **PARTIALLY RESOLVED** — QG24 refers to the cost *after* ψ is added (T is a derived scalar, not a new mode); QG13 documents the ψ=0 native failure. QG22 clarifies: "partly artifact, main failure from mass-radius relation, separate issue." |
| C4 | **Perihelion requires spin-2 (QG103) vs only GW polarization requires spin-2 (QG43)** | QG103: "confirms ψ as the graviton sector — a tensor observable (perihelion) that scalar-only cannot reproduce" | QG43: perihelion/γ is SCALAR; only GW polarization intrinsically requires spin-2 | Apparent | **PARTIALLY RESOLVED** — QG103's "tensor observable" means *the tensor sector restores γ=β=+1*, not that perihelion is intrinsically spin-2. QG43 notes a 1-d.o.f. scalar ψ would also restore γ=+1. The classifications are about different questions (which sector is *needed* vs which observable is *spin-2*). |
| C5 | **Conformal no-lensing is "fundamental" vs "artifact"** | QG21/QG26: no lensing is a definitive prediction (falsifiable) | QG22: no lensing is a CONFORMAL-FLATNESS ARTIFACT (ψ=0), not fundamental | Real tension | **RESOLVED** — QG22 supersedes: the no-lensing result is real *within ψ=0*, but ψ=0 is an assumption (G4-A1, PREFERRED not derived). The falsifiable prediction stands only in the conformal sector. |
| C6 | **Hawking T: partial artifact (QG22) vs undecided (QG25/QG43)** | QG22: no-Hawking-T partly artifact of conformal flatness | QG25: hawking-temperature UNDECIDED; QG43: horizon physics AMBIGUOUS | Minor | **RESOLVED** — consistent: QG22 gives the mechanism, QG25/43 record the epistemic status. |

**Contradiction count: 6 total; 3 fully resolved, 2 partially resolved, 1 unresolved (C2).**

---

## 3. Open Gravity Questions

Unresolved gaps (each from explicit "OPEN"/"not yet"/"caveat" language in the reports):

1. **Mass-radius discrepancy (QG12/QG13)** — AT deficit mass ∝ R^d (volume), Schwarzschild M ∝ R
   (surface/radius). Blocks: exact S ∝ M², the 1/4 Bekenstein coefficient, Hawking T ∝ 1/R.
   A "holographic mass definition" is needed but **not derived**.
2. **ψ/Weyl field origin** — QG17/19/23/24: ψ cannot emerge from scalar actualization; it is a
   MINIMAL NEW PRIMITIVE (2 d.o.f., spin-2). Its **physical origin is open** — it is required
   observationally, not derived.
3. **Bridge QG6 ↔ QG181** — the deficit-abundance G (native scale) and the D96-derived G (absolute
   magnitude) are not connected. What fixes m₀, r₀, ρ̄ to the D96 values? **Open.**
4. **Matter = deficit hypothesis (G4-ME)** — m = ρ̄−ρ is a *hypothesis* (reinterpretation), not
   derived; the "repulsion at peaks" bug is fixed by redefinition, not by dynamics. **Open.**
5. **Metric ansatz status** — g = ρ^(2/d)η is PREFERRED (exponent derived) but not UNIQUE; the flat
   background η is a defining axiom. **Open (as an axiom).**
6. **No independent matter sector (G4-G3/G4)** — G=κT is an identity (Lovelock), kinetic T not
   conserved; the actualization density is the *only* source. Whether real matter couples this way is
   **open**.
7. **Hawking temperature after ψ** — QG24 says ψ costs nothing extra for T, but no phase *derives*
   T ∝ 1/R with ψ≠0 explicitly. **Open.**
8. **Flat rotation-curve α=0** — the log-deficit hierarchy is SEMI-NATURAL; α=0 (the marginal member)
   is a symmetry assumption, not dynamically derived. **Open.**
9. **2D degeneracy (G4-G0)** — Einstein tensor ≡ 0 in d=2; native 2D program cannot reach the
   non-trivial d≥3 structure that QG22/103 use. The d=3 machinery exists (G4-G2/G3) but the 2D native
   path and the 3D usage are not connected in one report. **Open bridge.**

---

## 4. Already Solved Questions

Completed, classified results:

| Question | Phase(s) | Answer |
|---|---|---|
| Does counting give area-law entropy? | QG12 | **YES** — S ∝ Area (MATCH, conditional on holographic boundary identification; no 1/4, no S∝M²) |
| Does the Einstein structure emerge from ρ? | G4-G0, G4-G2 | **YES** — G_μν(ρ,∂ρ,∂²ρ) exact (<1e−12), dimension-generic, Bianchi automatic |
| Does the Einstein equation emerge? | G4-G3 | **FORM YES** — G=κT holds as identity (T≡G/κ); kinetic-only insufficient (∂²ρ essential) |
| Is light redshifted? | QG21, G4-O0 | **YES** — Δν/ν = −ΔΦ (KNOWN GR-LIKE); redshift survives conformal geometry |
| Is light bent in the conformal sector? | QG21, QG26 | **NO** — δ=0, μ=1, Δt=0 (PPN γ=−1); definitive within ψ=0 |
| Is Shapiro delay produced? | QG26 | **NO** in conformal sector (0); would need ψ≠0 (QG22) |
| Is Mercury perihelion reproduced? | QG103 | **YES** — +42.98″/century via ψ (γ=β=+1); ρ-only gives retrograde |
| Is G derived? | QG6, QG181 | **TWO ANSWERS** — QG6: native scale, magnitude free; QG181: absolute magnitude from D96 (0.4%) |
| Are tensor GWs available? | QG18/19, QG22 | **NO** in scalar sector (NEW PRIMITIVE required, ψ spin-2) |
| What is the minimal completion? | QG24 | **ψ-field** — 2 d.o.f. transverse-traceless spin-2; unique minimum restoring lensing+GWs |
| Does Hawking T emerge? | QG13 | **NO** (T∝R); needs holographic mass definition |
| Which observables require spin-2? | QG43 | **Only GW polarization**; lensing/Shapiro/γ are scalar, horizon ambiguous |

---

## 5. Missing Experimental Tests

From the reports, the observables that would discriminate AT's gravity claims:

| Observable | AT prediction | Status |
|---|---|---|
| **Gravitational lensing** | ZERO in conformal (ψ=0) sector; non-zero only with ψ | **CRITICAL** — observed lensing exists (classic tests); the conformal-sector prediction is falsified by existing data unless ψ is the active sector. QG21 flags it as the decisive difference from GR. |
| **GW polarization (h_+, h_×)** | Requires the ψ tensor; scalar-only gives 0 | **CRITICAL** — LIGO/Virgo observed 2 polarizations (QG48). Confirms ψ is needed. |
| **Shapiro delay** | 0 in conformal sector | **CRITICAL** — Cassini measured it; consistent only if ψ sector active. |
| **Mercury perihelion** | +42.98″/century (ψ) | **MATCHED** — QG103 reproduces observed value exactly. |
| **Black-hole entropy coefficient 1/4** | Not derived (only area law) | **MISSING** — no prediction for the exact coefficient. |
| **Hawking radiation** | T ∝ R (native, wrong); T ∝ 1/R requires holographic mass | **MISSING** — no native prediction; the negative result is documented. |
| **Curvature-sourced Poisson** | ΔΦ + ... = −ρ^(2/d)R/(2(d−1)) (source = ρ″, curvature) | **AT-SPECIFIC** — differs from GR's ΔΦ=4πGρ; "testable in principle" (G4-O0) but no concrete experiment proposed. |
| **Frame dragging / Lense–Thirring** | No completed phase | **MISSING** — listed as future priority in NewChat_Start.md only. |
| **GPS correction / time dilation** | Redshift mechanism exists (g_00 varies) | **NOT TESTED** — no report connects redshift to GPS/timedilation observables. |

---

## 6. Duplicate Research

Overlapping phases that address the same question:

| Duplicate cluster | Phases | Assessment |
|---|---|---|
| **Lensing absence** | QG21 (null geodesics), QG26 (PPN γ=−1, 5 mechanisms), QG22 (artifact), G4-O0 (weak-field form) | 4 reports on the same "no lensing" fact. QG26 is a formal restatement of QG21 (mechanism census); QG22 adds the artifact diagnosis; G4-O0 uses "lensing" loosely. **Consolidation possible**: QG21 + QG22 + G4-O0's correction suffice. |
| **ψ needed** | QG17 (unsourced), QG18 (GW fail), QG19 (GW reconcile → NEW PRIMITIVE), QG23 (can't emerge), QG24 (minimal extension), QG43 (uniqueness) | 6 phases establish the same conclusion (ψ = new primitive). QG19 vs QG24 overlap heavily; QG43 refines QG40. **Consolidation possible** into QG24 (minimal) + QG43 (uniqueness). |
| **Hawking temperature** | QG13 (native fail), QG22 (artifact partial), QG24 (0 d.o.f. note), QG25 (undecided), QG43 (ambiguous) | 5 reports on the same negative/unresolved result. **Consolidation possible.** |
| **Newton constant** | QG6 (deficit GM_eff), QG181 (D96 M_Pl) | 2 frameworks, same symbol G, different constructions, **no cross-reference**. |
| **Mercury perihelion** | QG103 (only phase) | No duplication. |

---

## 7. Conflicting Results

Summary of the conflicts requiring reconciliation (details in §2):

1. **C2 (G: native-magnitude-free vs D96-absolute)** — the only **unresolved** conflict. Two derivations
   of G in different frameworks with no bridge. Highest priority for a future consolidation phase.
2. **C3 (Hawking T: 0 d.o.f. vs NO MATCH)** — resolved by reading QG24's "0 d.o.f." as *cost after ψ*;
   but no phase derives T ∝ 1/R explicitly. Partially open.
3. **C4 (perihelion: tensor vs scalar)** — resolved by reading QG103's "tensor observable" as *the tensor
   sector restores γ=β=+1*; the scalar-ψ alternative (QG43) is not tested for perihelion.

---

## 8. Unresolved Gaps

The single most important structural facts:

- **Gap A (mass-radius):** AT's volume-scaling deficit mass (∝R^d) is incompatible with Schwarzschild's
  radius-scaling mass (∝R). This blocks S∝M², the 1/4 coefficient, and Hawking T. Identified in QG12,
  reiterated in QG13, acknowledged in QG22 — **never resolved**.
- **Gap B (ψ origin):** the tensor completion is required observationally (GW polarization, perihelion,
  lensing) but is a **new primitive with no origin**.
- **Gap C (G bridge):** QG6's deficit abundance and QG181's D96 spectrum are two disconnected sources
  for G.
- **Gap D (matter):** no independent matter sector; "matter = deficit" is a hypothesis.
- **Gap E (2D→3D):** the native G4 program is 2D (Einstein tensor ≡ 0); the non-trivial results used by
  QG22/QG103 are the d≥3 formulas, which are not reached natively by the 2D causal-set program in one
  connected chain.

---

## 9. Recommendation Summary

- **No new physics proposed** in this audit.
- Highest-value next steps (all *consolidation*, not derivation):
  1. A bridge report connecting QG6's GM_eff = m₀r₀/(d·ρ̄) to QG181's M_Pl = v·A³ (what fixes m₀, r₀, ρ̄
     from D96?).
  2. A single consolidated "tensor ψ requirement" report merging QG19/QG24/QG43.
  3. A consolidated "horizon thermodynamics" report merging QG12/QG13/QG22/QG25/QG43.
  4. An explicit d≥3 native gravity chain report linking G4-G2/G3 to QG22/QG103 (bridging the 2D native
     program to the 3D usage).

---

## 10. Source Reports

| Phase | Report |
|---|---|
| QG6 | `ATQG_OriginOfG.md` |
| QG12 | `ATQG_BlackHoleEntropy.md` |
| QG13 | `ATQG_HorizonThermodynamics.md` |
| QG21 | `ATQG_LightPropagation.md` |
| QG22 | `ATQG_ConformalFlatnessAudit.md` |
| QG25 | `ATQG_ObservableReconstructionAudit.md` |
| QG26 | `ATQG_NonTensorLensing.md` |
| QG43 | `ATQG_ObservationalUniqueness.md` |
| QG103 | `ATQG_MercuryRevalidation.md` |
| QG181 | `ATQG_NewtonConstantOrigin.md` |
| QG19 | `ATQG_GWReconciliation.md` |
| QG24 | `ATQG_MinimalTensorExtension.md` |
| G4-G0 | `G4G_EinsteinStructure.md` |
| G4-G2 | `G4G_RhoToEinstein.md` |
| G4-G3 | `G4G_NativeEinsteinEquation.md` |
| G4-O0 | `G4O_PhysicalObservables.md` |
| G4-A0 | `G4A_MetricAnsatzAudit.md` |
| G4-ME | `G4ME_RealityCheck.md` (+ G4ME series) |
| Synthesis | `AT_Gravity_Reassessment.md` |
