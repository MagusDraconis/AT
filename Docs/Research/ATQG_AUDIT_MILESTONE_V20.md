# AT-QG Audit Milestone — Monograph V2.0

**Purpose:** consolidated record of the V2.0 publication-hardening audit pass: what is closed,
what remains open, and the authoritative claim-status state that the monograph and AT.App now
present.

**Source of truth:** Docs/Research/ATQG_ClaimClassificationRegistry.md for claim statuses;
Docs/Research/D96_REPRO_AUDIT.md for D96 provenance.

---

## Closed Audits

| Audit | Scope | Result | Artifacts |
|---|---|---|---|
| **QM_AUDIT001** | Born rule, interference normalization, measurement basis | Basis separation (generation G / interference I / measurement M); unnormalized interference `2+2cos` replaced by `½(1±cos Δθ)`; measurement rule `P(e)=|⟨e|ψ⟩|²` in basis M; Born rule restricted to generation basis. Applied to Ch9 and AT.App (TheoryBookDataService, DerivationMap). | Ch9 edits; AT.App QM wording |
| **D96_REPRO_AUDIT** | Reproducibility of the D96 spectrum from a stated graph | C96(±1..±6), 12-regular, 576 links; Laplacian eigenvalues `λ_k = 2Σ(1−cos 2πdk/96)`, `ω=√λ`; reproduces `[42×2,5,6]`, 95+1 modes, Σm=95, Σ√m=64.08, Σm²=229, span 6.40 — all exact. Graph definition + formula disclosed in Ch5/Ch6. | Docs/Research/D96_REPRO_AUDIT.md |
| **P2 reach** | 0νββ reach statement | "2.02 meV inside the 36–156 meV window" wording corrected in Ch14 and Appendices (wording only). | Ch14/Appendices edits |
| **Claim classification** | Classify every major claim by derivational status | DERIVATION_AUDITOR + hostile/validation referee passes → 16 claims in 6 categories (theorem / necessity / correspondence / calibration / hosted / fit). Validation-referee corrections applied: CKM→correspondence, PMNS→correspondence, neutrino splittings→correspondence+calibration, Higgs→calibration, couplings split (1/α_em→fit, α_weak/α_strong→correspondence), peak ratios→correspondence. | Docs/Research/ATQG_ClaimClassificationRegistry.md |
| **Registry consistency** | Align all "derived / reconstructed / predicted" wording in monograph + AT.App to registry status | Status-aware wording across Ch10/11/12/14, FrontMatter, Conclusion, AT.App (AtlasDataService, CoverageAudit, BlindValidation). Wording only — equations, numbers, citations, layout untouched; all theorem-status wording preserved. Full combined state verified: pdflatex 2-pass clean (95 pages, no undefined refs), dotnet build 0 errors. | Wording patches (claim status / final registry / residual 2-line), verified stackable |
| **AT.App claim badges** | Display claim status in the UI | ClaimStatus enum, ClaimStatusInfo, ClaimStatusBadge component; registry data in `TheoryBookDataService.ClaimStatuses`; badges on Theory section pages, Validation Overview, and Standard-Model observables table. | AT.App commit `ef09eafd` |

---

## Remaining Open Items

| Item | Status | Evidence / classification |
|---|---|---|
| **Observable-selection non-uniqueness** | OPEN — some observables are selected from candidate families; the selection principle is not globally unique | ATQG_ObservableSelectionAudit.md: 1/α_em HIGH target-selection dependence → **fit**; CKM/PMNS medium dependence → **correspondence** |
| **Sector-label non-uniqueness** | OPEN — sector access-role labels (neutral/full/doublet/octave) are supported assignments over the forced moment ladder, not a globally unique mapping | Registry "sector mappings" row (**correspondence**); ATQG_MomentSectorAssignmentAudit.md |
| **Gauge correspondence vs hosted structure** | OPEN — the 1+3+8 sector counts are a real dimensional correspondence; the gauge groups U(1)×SU(2)×SU(3) and their Lie algebras are **hosted**; the structural origin of the hosted dynamics is not derived | Gauge referee audits (finite structure cannot generate continuous Lie groups); registry gauge rows |
| **ℓ₁ fitted normalization (5/4)** | OPEN — 5/4 is a **fit** (QG297), removable (QG289), no wave/octave/beat mechanism; ℓ₁ = Σm·ln(span)·(5/4) = 220.48; the peak ratios are pure spectral correspondences | ATQG_ExceptionAudit.md; registry "CMB peak location" row (**fit**); PEAK001 (existence = structural theorem, location = fit) |

**Correction of record:** the earlier QG298 "first peak origin" reading that 5/4 is a *structural
boundary projection* `(occ₀ + zero_mode)/occ₀ = 5/4` is **REJECTED** by the V2.0 audit pass: that
identity is a label identity without a mechanism (the same standard used to reject Bekenstein
1/occ₀ = 1/4). The registry and all patched wording classify 5/4 as **fit**. This supersedes the
QG298/QG299 "R4 CLOSED" entries in Docs/NewChat_Start.md.

---

## Status Summary (16 classified claims)

| Category | Claims |
|---|---|
| theorem | D96 spectrum, moment values, CMB peak existence, conformal factor |
| necessity | N=96 (scoped) |
| correspondence | sector mappings, 1+3+8 dimensions, CKM, PMNS, couplings (α_weak, α_strong), neutrino splittings (ratios), peak ratios |
| calibration | gravity (natural-unit part), Higgs (value), neutrino splittings (eV² units), couplings (scale) |
| hosted | gauge groups, spacetime metric/signature, black-hole inputs |
| fit | 1/α_em (couplings), ℓ₁ location |

---

## V2.0 Wording-Patch Inventory

All wording patches are text-only deltas (no equation, numerical, citation, or layout changes;
theorem-status wording preserved). Applied in order on top of the repository baseline:

1. `claim_status_wording` — the 16 consistency-audit fixes (Ch10/11/12/14, FrontMatter, Conclusion, AT.App).
2. `final_registry_patch` — remaining mismatches: Ch10 theorem title "derived→calibrated", hosted M∝R profile input, Ch11 blanket "every observable derived" statements, AT.App "boundary projection" wording.
3. `residual_2line` — Ch11 intro "as reads … per documented claim status"; Ch12 cosmo-emergent "readout whose claim status is documented".

Each patch applies cleanly on top of the previous; the combined state builds cleanly (monograph 95 pages, AT.App 0 errors).
