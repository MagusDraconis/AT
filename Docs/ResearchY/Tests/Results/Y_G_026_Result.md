# Y_G_026_Result.md — ResearchY-G_026 Authored-Verdict Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_026_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 8/8 PASSED (~5 s) — group G total **218/218 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_026"`

## Summary

**Why this audit exists:** G_025 verified the AT-QG optics arithmetic, found an off-by-one determinant and a hard-coded γ, and asked whether that defect class is isolated. **It is not — it is systemic.**

**The fingerprint:** a member or data field whose **name** claims a computation or a verification verdict, whose **value** is a literal, and which a **score, classification or test** then consumes.

## Five findings

| # | site | the defect | severity |
|---|------|-----------|----------|
| 1 | `QuantumGravityClosureAudit` / `ReclosureAudit` / `ReclosureAudit2` / `FinalQuantumGravityAudit` | four audits, identical method names, verdicts differing only by literals; QG221/QG223 sub-scores were typed `1.0`s that **never read the criteria**; four test suites asserted **mutually contradictory** verdicts and all passed; ψ asserted as a new primitive, contradicting G_024/QG285 | 🔴 |
| 2 | `BornRuleAnalyzer` / `BornRuleDerivation` | `Survives` and the pass arrays typed; `AllRequirementsUniquelySatisfied(reqs, tests)` **ignored `tests`**; and the headline "α = 2 UNIQUELY selected" was **dead code** — the uniqueness test returned false unconditionally, so the real output was "C: Strong Theorem" | 🟠 |
| 3 | `RarScatterAnalyzer.AuditCompletion` | 7 of 11 completion rows typed `Derived = true` with the literal label `"DERIVED ✓"`, then counted | 🟠 |
| 4 | `FrameDraggingOrigin.G_D96`, `PhysicalUnits.G_SI` | the derived constant G duplicated as independent literals with **no check** against `NewtonConstantOrigin`'s computation | 🟡 |
| 5 | `EffectiveSizeLaw.IdentityHoldsAcrossGrid(feedback, damping)` | declared the dynamics parameters, then called `FamilyBandIdentity(n, K)` — silently answering for the **defaults** | 🟡 |

## Detector yields

| detector | criterion | yield |
|---|---|---|
| 1 | literal-bodied members feeding a score | 248 of 1221 literal-bodied members |
| 2 | **declared parameters the body never uses** (decidable) | 16 |
| 3 | evidence booleans passed as literals at construction sites | 49 |

## The QG ladder, before and after

| phase | before (typed) | after (derived) |
|---|---|---|
| QG215 | `IsQuantumMechanicsDerived() => false` → PARTIAL QG 2.0 | criteria table → **2.0 / PARTIAL QG** |
| QG219 | `=> true` → EFFECTIVE QG 4.0 | criteria table → **4.0 / EFFECTIVE QG** |
| QG221 | `=> true`, sub-scores typed `1.0/0.5` → 5.0 | criteria table → **5.0 / NEAR-COMPLETE QG** |
| QG223 | `=> true`, sub-scores typed `1.0` → 6.0 | criteria table → **6.0 / COMPLETE QG** |

The **verdicts are unchanged** — the derived ladder reproduces the historical rungs exactly. What changed is that the score, total, classification and progression are now **functions of the criteria** (so flipping one moves the ladder), every criterion carries a **cited basis**, and the four test suites assert the **derived** values.

## The α screen, executed

`AlphaInvarianceScreen` — deterministic (normalized DFT + Givens at fixed angles, no RNG), dims 2–5:

| α | outcome | max relative violation |
|---:|---|---:|
| 0.5 | BREAKS | 2.343702 |
| 1.0 | BREAKS | 1.236068 |
| 1.5 | BREAKS | 4.953488e−1 |
| **2.0** | **INVARIANT** | **2.220446e−16** |
| 3.0 | BREAKS | 5.527864e−1 |
| 4.0 | BREAKS | 8.000000e−1 |

`Survives` is now computed from this screen. Requirements carry `RequirementEvidence` (Computed / Analytic): **4 computed, 3 analytic** (Partial trace consistency, Complexity additivity, Linearity of expectation — no executable test exists). The classification now discloses the split.

## Robustness defect found en route

`AllRequirementsUniquelySatisfied` matched the chosen exponent by comparing **formatted number strings** (`"2.0"`), which is **culture-dependent** — a comma-decimal culture would silently collapse the classification to a lower rung. Now parsed with `InvariantCulture` and compared numerically.

## Verified non-findings

`ConservationPrincipleAudit.Laws()` and `MajoranaOrigin.Checks()` pass **computed calls** as their evidence flags (the correct pattern). `TemporalWaveObservables.* => 0.0` are analytic null results documented as such. `MetricOrigin.SqrtMinusG_Const` is honestly named. `LightPropagation.LightSpeed(rho) => 1.0` is the c = 1 convention. `Anchor.IsTrueInput`, `MeasurementClass.StructurallyUnique` and `Rule.Derivable` are authored **input taxonomies** with their reasoning inline.

## Tests

| test | asserts | result |
|---|---|---|
| `Y_G_026_QgLadderIsDerivedFromCriteria` | all bases cited; ladder follows; sub-scores match criteria; int ≡ double score | ✅ |
| `Y_G_026_QgLadderReproducesTheHistoricalRungs` | 2/4/5/6 → PARTIAL/EFFECTIVE/NEAR-COMPLETE/COMPLETE; partial credit is a declared status; progression monotone | ✅ |
| `Y_G_026_PsiIsNotANewPrimitive` | canonical `false`, historical `true` preserved, functional content intact | ✅ |
| `Y_G_026_BornAlphaTwoUniquenessIsComputed` | screen selects α = 2 alone; every recorded verdict agrees; exclusions have real margin | ✅ |
| `Y_G_026_RequirementEvidenceIsDisclosed` | bases cited; Computed/Analytic partition; discriminating rows admit α = 2 only; classification discloses split | ✅ |
| `Y_G_026_CompletionAndCacheEvidenceAreDisclosed` | RAR limits computed; G cache agrees with the computation | ✅ |
| `Y_G_026_IgnoredParametersAreUsed` | grid ≡ per-point recomputation at the supplied dynamics point | ✅ |
| `Y_G_026_Run` | the full report | ✅ |

**Headline:** the G_025 defect class is **systemic** — the QG closure ladder "PARTIAL → EFFECTIVE → NEAR-COMPLETE → COMPLETE" was four audited method names differing only by typed literals, its sub-scores did not read its own criteria, its four test suites asserted mutually contradictory verdicts and all passed, and the Born-rule headline classification was **unreachable dead code**. All five instances are now derived, with evidence bases cited and computed/analytic status disclosed.
