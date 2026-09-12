# ResearchY-G_027 — Literal Verdict Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_027 (permanent)
**Title:** Literal Verdict Audit — rejecting every result path whose classification depends on a literal
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_027.md`
**Depends on:** G_026 (the defect class), G_025 (its first instance); AT-QG **QG080/QG084** (the g† coupling analysis)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_027_Tests.cs` (5/5 PASSED, ~1 s)
**Mechanism:** `AT.Core/ResearchXH/LiteralVerdictAudit.cs`

## The rule

> **No result path may let a classification depend on a literal. Every verdict must be traceable to computed evidence.**

A path of the form

```
literal boolean (verdict-vocabulary name)  →  score / classification / verdict function
```

is **REFUTED** unless it is **COMPUTED** or adjudicated **SAFE** / **BOUNDARY** with a cited reason.

**FAILS IF:** `literal → verdict` without a computation path.

## Why an enforcement mechanism, not another sweep

G_025 and G_026 each found instances and fixed them by hand. **Fixing instances does not prevent a class from recurring** — and this class recurred *four times in the QG ladder alone*. G_027 therefore makes the rule **enforceable**:

`LiteralVerdictAudit` **re-reads the AT.Core source at test time**, locates every literal-to-verdict path, and requires it to be either computed or adjudicated. A **new** path fails the build until it is computed or explicitly recorded.

## How a path is located

1. Every `record` in the file is parsed for **verdict-vocabulary boolean** parameters — by **name** and by **position** (the codebase writes both `Holds: true` and positional `…, true, …`).
2. Every construction site is scanned for those booleans supplied as **literals**.
3. The path counts only if the file also **consumes** the flag from a score / classification / verdict function (`Score`, `Classify`, `Verdict`, `Rank`, …).

That last condition is what keeps the inventory honest: a literal nobody scores is inert.

## Output: 7 paths, all BOUNDARY. Zero REFUTED.

| file | verdict field | disposition | why |
|---|---|---|---|
| `ResearchXH/SelectionPrincipleAudit.cs` | `Rule.Derivable` | **BOUNDARY** | meta-level judgement about D96 grounding; the audit's own summary states neither rule is FORCED and both were selected *after* QG253 revealed the non-uniqueness |
| `ResearchXH/AnchorInventoryAudit.cs` | `Anchor.IsTrueInput` | **BOUNDARY** | input taxonomy; each row states its reason in place ("a TRUE THEORY INPUT: the conformal reference metric") |
| `ResearchXH/PrincipleCompetitionAudit.cs` | `Principle.Consistent` | **BOUNDARY** | authored consistency flag; the audit records the Noether row as `false` and explains why |
| `ResearchXH/MeasurementClassAudit.cs` | `MeasurementClass.StructurallyUnique` | **BOUNDARY** | authored uniqueness claim; making it computed needs an exhaustive enumeration of reads, which does not exist |
| `ResearchXH/ActualizationOriginAudit.cs` | `DependencyFact.SupportsDerivedFromDifference` | **BOUNDARY** | authored per-evidence dependency finding, each citing its phase (QG292/293/284/294/295/288) |
| `ResearchXH/ReorganizationPrediction.cs` | `ReorgMember.Correct` | **BOUNDARY** | authored correctness flag; the comparison target (pre-registered values) is recorded in the same audit |
| `Resonance/Theory/MicroscopicChargeProfile.cs` | `FragmentationAttempt.IsValidCharge` | **BOUNDARY** | authored validity flag with its reason in place ("Isolated kink detected") |

**No path is REFUTED.** A REFUTED disposition is *not a tolerated state* — the registry forbids it, because a refuted path is fixed rather than registered.

## The one REFUTED path found — and fixed

**`AT.Core/ResearchQG/GdaggerOriginAnalyzer.cs`** — every evidence flag, the ratio *and* the score were literals at each of the six construction sites, and `Score` feeds `OrderByDescending` (**the ranking**). The Coincidence row was even **internally inconsistent**:

```csharp
new CouplingMechanism("Coincidence", …, double.NaN, false, 1.0, true, false, …, 1.5)
//                                                ^^^^^^^^^^^ NaN numerator, but the ratio claims 1.0
```

All four quantities are now derived from the predicted g†:

```text
HasExactTwoPi = |predicted − cH₀/(2π)| / (cH₀/(2π)) < 1e−6
RatioToA0     = predicted / a0                        (NaN propagates honestly)
Matches       = |RatioToA0 − 1| ≤ 0.15                (the criterion the Verdict text states)
Score         = 3.0 if exact 2π and matching; else 1.5 if not falsifiable; else 0.5
```

**Verified to reproduce the previously typed values for all six mechanisms** — 3.0 / 3.0 / 0.5 / 0.5 / 0.5 / 1.5 — so the fix changes *provenance*, not the physics. The typed `1.0` ratio with a `NaN` numerator is gone.

## Previously-literal paths now computed (the regression record)

`FixedByComputation` records the eleven sites fixed by G_025/G_026/G_027 — the optics determinant and γ (G_025); the four QG criteria tables, the Born-rule `Survives`/pass arrays, and the RAR `Derived` rows (G_026); the g† flags, ratio and score (G_027). Because these now fail to *scan*, a **regression** — a literal reappearing — is caught by the scan and **cannot be excused by the registry**.

## Verdicts

| label | content |
|-------|---------|
| **SAFE** | the eleven formerly-literal paths, now computed; a regression is caught by the scan. |
| **BOUNDARY** | the seven adjudicated authored judgements and input taxonomies — each with a cited reason, none presented as a computation. **These are the honest residue: AT's remaining literal verdicts are all input classifications and meta-level judgements, not physics results.** |
| **REFUTED** | exactly one path was found — `GdaggerOriginAnalyzer`'s evidence flags, ratio and score — and it is **fixed**. **Zero remain**, and the registry structurally forbids a refuted entry. |

## What this audit does *not* claim

It does not decide whether a research verdict is **correct**. It decides whether a verdict is **computed** — and, when it is not, it forces that fact into the open instead of leaving it implied by a method name. Restructuring an authored judgement does not make it a computation; it makes it *impossible to mistake* for one.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_027_Tests.cs` — **5/5 PASSED**
**Group total:** G_001–G_027 = **223/223 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_027"`

| test | asserts |
|---|---|
| `Y_G_027_NoUnTriagedLiteralToVerdictPath` | **zero** un-triaged paths (the FAIL condition) |
| `Y_G_027_RegistryIsCompleteAndBacked` | no stale triage, **zero REFUTED**, every reason cited, scan non-vacuous and exhaustive |
| `Y_G_027_GdaggerEvidenceIsComputed` | the derived flags/ratio/score reproduce the typed values; the NaN-ratio contradiction is gone |
| `Y_G_027_ScannerDetectsTheKnownDefectShape` | the scanner **fires** on the G_025/G_026 fingerprint and is **silent** when the flag is computed |
| `Y_G_027_Run` | the full report |

**Opens:** OP1 give `MeasurementClass.StructurallyUnique` an enumeration of reads; OP2 decide whether `Rule.Derivable` should be computed from D96 moments or formally reclassified; OP3 extend the scan to AT.Tests/AT.App/AT.Book (currently downstream of the adjudicated AT.Core objects).
