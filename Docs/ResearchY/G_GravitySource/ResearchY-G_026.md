# ResearchY-G_026 — Authored-Verdict Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_026 (permanent)
**Title:** Authored-Verdict Audit — generalising the G_025 defect class across the codebase
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_026.md`
**Depends on:** G_025 (which found the class), G_024, G_021–G_023; AT-QG **QG215/QG219/QG221/QG223**, **QG24**, **QG216**, **QG285/QG286/QG292**; AT-X037
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_026_Tests.cs` (8/8 PASSED, ~5 s)

## Purpose

G_025 verified the AT-QG optics arithmetic, found an off-by-one determinant and a hard-coded γ, and closed with a question: **is that defect class isolated?** This audit generalises the search and answers: **no — it is systemic.** Five further instances were found and fixed.

## The fingerprint

> A member or data field whose **NAME** claims a computation or a verification verdict, whose **VALUE** is a literal, and which a **SCORE, CLASSIFICATION or TEST** then consumes.

Three mechanical detectors were built (kept as session artifacts):

| detector | criterion | yield |
|---|---|---|
| 1 | literal-bodied members feeding a score/classification | 248 candidates of 1221 literal-bodied members |
| 2 | **declared parameters the body never uses** — *decidable, not a heuristic* | 16 occurrences |
| 3 | **evidence booleans** (`Holds`/`Ok`/`Survives`/`Derived`/…) passed as literals at construction sites | 49 occurrences |

The decisive test is detector 2: a member that declares parameters and never mentions them **cannot** compute anything from its inputs. That is a proof, not a heuristic.

## Finding 1 — the QG verdict ladder (🔴 severe)

Four successive audits share **identical method names** and differ only in literals:

| file | `IsQuantumMechanicsDerived` | `SamePrimitiveForBoth` | `IsSpacetimeEmergent` | verdict |
|---|---|---|---|---|
| `QuantumGravityClosureAudit` (QG215) | `=> false` | `=> false` | `=> false` | PARTIAL QG |
| `QuantumGravityReclosureAudit` (QG219) | `=> true` | `=> true` | `=> false` | EFFECTIVE QG |
| `QuantumGravityReclosureAudit2` (QG221) | `=> true` | `=> true` | `=> false` | NEAR-COMPLETE QG |
| `FinalQuantumGravityAudit` (QG223) | `=> true` | `=> true` | `=> true` | **COMPLETE QG** |

The escalation PARTIAL → EFFECTIVE → NEAR-COMPLETE → COMPLETE was produced by **editing `false` to `true`**. Two aggravating defects:

* In QG221/QG223 the six **sub-scores were a separate set of typed `1.0`s that never read the criteria**. `FinalQuantumGravityAudit.TotalScore()` therefore returned 6.0 and `Classify()` returned `"COMPLETE QG"` *whatever the criteria said*. In `QuantumGravityReclosureAudit2`, `IsSpacetimeEmergent() => false` coexisted with `SpacetimeSubScore() => 0.5` — decoupled literals that could never disagree.
* The tests **asserted the literals**: `ATQG_Phase215` asserts QM **not** derived; `ATQG_Phase219/221/223` assert QM **is** derived. **All four pass.** The suite contained mutually contradictory claims about whether QM is derived.

Additionally `FinalQuantumGravityAudit.PsiIsNewPrimitive() => true` **contradicted ResearchY-G_024 / QG285/QG286/QG292**, which established that ψ is the traceless face of the one Difference (minimal set {Difference, η}) — i.e. *not* a new primitive. G_024 had already retracted that claim; the live code and its test still asserted it.

### Fix
A shared `QgCriterion` type (`AT.Core/ResearchXH/QgCriterion.cs`) makes a criterion a **whole value**: `Name`, `Status` (None/Partial/Full), and a **mandatory `Basis`**. `Score` is **derived** from the status. Each of the four audits now exposes a `Criteria()` table; every boolean accessor, the sub-scores, the total, the classification and the progression are **derived** from it. `AllBasesCited()` and `ClassificationFollowsFromCriteria()` are checkable.

The verdicts are **unchanged** — the derived ladder reproduces the historical rungs exactly (2.0 / 4.0 / 5.0 / 6.0 → PARTIAL / EFFECTIVE / NEAR-COMPLETE / COMPLETE). The ψ verdict was corrected to the canonical position, with `PsiWasCalledNewPrimitive()` preserving the historical record. The four test suites now assert the **derived** values, so flipping a criterion breaks them.

## Finding 2 — the Born-rule classification (🟠)

* `TestAllAlphas()` typed `Survives` per exponent.
* `BuildRequirements()` typed the pass arrays (`new[] { false, false, false, true, false, false }`).
* `AllRequirementsUniquelySatisfied(reqs, tests)` **accepted the executed tests and never read them**.

**And the headline verdict was dead code.** The old test was `Count(p => p) != 1` on *every* row — but the factorization / orthogonality / additivity rows legitimately pass for **all six** exponents (they do not discriminate), so their count was 6 and the method returned `false` **unconditionally**. `"D: Mathematical Derivation — α = 2 is UNIQUELY selected"` was therefore **unreachable**; X037's real output was `"C: Strong Theorem"`.

### Fix
`AlphaInvarianceScreen` **executes** the actual physics: `N(ψ) = Σ|ψ_i|^α` must be invariant under every unitary iff α = 2. Deterministic (normalized DFT + Givens rotations at fixed angles; no RNG), over dims 2–5:

| α | outcome | max relative violation |
|---:|---|---:|
| 0.5 | BREAKS | 2.343702 |
| 1.0 | BREAKS | 1.236068 |
| 1.5 | BREAKS | 4.953488e−1 |
| **2.0** | **INVARIANT** | **2.220446e−16** (floating-point zero) |
| 3.0 | BREAKS | 5.527864e−1 |
| 4.0 | BREAKS | 8.000000e−1 |

`Survives` is now computed from that screen. `RequirementEvidence` distinguishes **Computed** (executed test) from **Analytic** (a recorded claim with no executable test — Partial trace consistency, Complexity additivity, Linearity of expectation); the classification now discloses the split. The uniqueness test was rewritten to correct semantics (α = 2 passes every requirement; the *discriminating* rows admit α = 2 alone) and **actually uses its `tests` argument**, so it is now reachable and can fail.

**Robustness defect found en route:** the method matched the chosen exponent by comparing **formatted number strings** (`"2.0"`), which is culture-dependent — a comma-decimal culture would silently collapse the classification. Now parsed with `InvariantCulture` and compared numerically.

## Finding 3 — the RAR completion table (🟠)

`RarScatterAnalyzer.AuditCompletion` built eleven `CompletionScore` rows of which **seven were typed `Derived = true`** and labelled with the literal string `"DERIVED ✓"`, then reported `scores.Count(s => s.Derived)`. Rows such as `gs.ScatterVariesWithType` already used a computed expression, showing the intended pattern.

### Fix
`CompletionEvidence` marks each row **Computed** or **Authored**. The three limit rows are now **computed** from the AT functional form `g_obs = g_bar·√(1 + g†/g_bar)`:
* Newtonian limit ratio = 1.000000000 at `g_bar = 1e12 g†`
* deep-MOND ratio = 1.000000000 at `g_bar = 1e−12 g†`

Authored rows are relabelled `"DERIVED (authored)"` and listed explicitly in the report, and the audit prints the computed/authored split.

## Finding 4 — the derived constant G duplicated as a literal (🟡)

`G` **is** genuinely computed in `NewtonConstantOrigin` (`v·A³ → M_Pl → G = ħc/m_Pl²`). But the value was duplicated as an **independent literal** in `FrameDraggingOrigin.G_D96` and `PhysicalUnits.G_SI`, and **nothing asserted the copies agreed with the computation** — so the derivation and its caches could silently diverge.

### Fix
`NewtonConstantOrigin.CacheAgreesWithComputation(...)` / `CacheDiscrepancy(...)`, asserted in `ATQG_Phase186` and `Y_G_026`. Computed G = 6.64670e−11 (0.014 % from the cached 6.6476e−11; 0.414 % from CODATA).

## Finding 5 — a parameter that silently answered for the defaults (🟡)

`EffectiveSizeLaw.IdentityHoldsAcrossGrid(feedback, damping)` **declared** the dynamics parameters and then called `FamilyBandIdentity(n, K)`, which falls back to the defaults — so a caller passing non-default dynamics received the default answer. `FamilyBandIdentity` now takes the parameters and the grid threads them through; the test asserts the contract (grid result ≡ per-point recomputation **at the supplied point**).

*Observation recorded, not asserted:* at the point tested, `(feedback, damping)` does **not** move the intra-sector spectrum — the parameters reach `HighEnergySectorStability.ObservableSector` but do not change this observable. Flagged for follow-up rather than claimed.

## Verified **non**-findings (so the fix does not over-reach)

| site | why it is *not* a defect |
|---|---|
| `ConservationPrincipleAudit.Laws()` | every `Holds` argument is a computed **call** (`UnitarityHolds()`, `D2ToD3Bridge.BianchiHoldsAtD3()`, …) — the correct pattern |
| `MajoranaOrigin.Checks()` | every `Ok` is a computed call (`SelfConjugateByAccess()`, `RealMassMatrix()`, …) |
| `TemporalWaveObservables.RoundTripTimeChange(L) => 0.0`, `BreathingDifferentialStrain(h0) => 0.0` | analytic **null results**, documented as such (conformal invariance of light) — the constant *is* the result |
| `MetricOrigin.SqrtMinusG_Const(x) => 1.0` | honestly named `_Const` |
| `LightPropagation.LightSpeed(rho) => 1.0` | the c = 1 convention |
| `AnchorInventoryAudit.Anchor.IsTrueInput`, `MeasurementClass.StructurallyUnique`, `SelectionPrincipleAudit.Rule.Derivable` | authored **input taxonomies** with the reasoning inline, not claimed computations |

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the QG closure ladder, total and classification (from the criteria tables); α = 2 uniqueness (from the executed unitary-invariance screen); the RAR Newtonian and deep-MOND limits; the G cache discrepancy; `√(det g_ij) = ρ ⟺ ψ = 0` (G_025). |
| **REFUTED** | that the QG sub-scores read the criteria (they were independent literals); that `AllRequirementsUniquelySatisfied` used its `tests` argument; that the α = 2 classification was reachable; that ψ is a new primitive (G_024); that the RAR `"DERIVED ✓"` rows were computed; that the cached G was checked against its derivation. |
| **BOUNDARY** | the criteria **statuses** remain the authored QG223 adjudication — restructured, not re-adjudicated. Three of seven Born requirements remain **Analytic** (no executable test exists). The dynamics parameters do not move the tested spectrum. |

## What the fix does *not* claim

An authored research verdict does **not** become "computed" by being restructured. The fix makes authorship **visible** and impossible to mistake for a computation, makes every score a derived function of its criteria, makes every criterion cite its basis, and makes the tests **able to fail**. The verdicts themselves are unchanged except where the project had already superseded them (ψ).

**Lesson recorded:** *a test that asserts a literal against itself is not evidence, and four passing suites can assert mutually contradictory verdicts.*

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_026_Tests.cs` — **8/8 PASSED**
**Group total:** G_001–G_026 = **218/218 PASSED**
**Affected pre-existing suites:** 390/390 PASSED (resonance structure, RAR/DATA005, frame dragging, effective size)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_026"`

**Opens:** OP1 specify the exact ψ completion (G_024/G_025); OP2 give the three Analytic Born requirements executable tests; OP3 determine whether `(feedback, damping)` *should* move the intra-sector spectrum; OP4 triage the remaining 49 evidence-boolean construction sites, most of which are input taxonomies rather than claims.
