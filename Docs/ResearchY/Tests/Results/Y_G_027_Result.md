# Y_G_027_Result.md — ResearchY-G_027 Literal Verdict Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_027_Tests.cs`
**Mechanism:** `AT.Core/ResearchXH/LiteralVerdictAudit.cs`
**Run:** 2026-09-13
**Result:** ✅ 5/5 PASSED (~1 s) — group G total **223/223 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_027"`

## The rule

> **No result path may let a classification depend on a literal. Every verdict must be traceable to computed evidence.**

```
literal boolean (verdict-vocabulary name)  →  score / classification / verdict function
```

REFUTED unless COMPUTED or adjudicated SAFE / BOUNDARY with a cited reason.
**FAILS IF:** `literal → verdict` without a computation path.

## Why this is an enforcement, not another sweep

G_025 and G_026 each found instances and fixed them by hand — and the class recurred **four times in the QG ladder alone**. `LiteralVerdictAudit` therefore **re-reads the AT.Core source at test time**: a **new** literal-to-verdict path fails the build until it is computed or explicitly adjudicated.

A path is located by (1) parsing every `record` for verdict-vocabulary **boolean** parameters, **by name and by position**; (2) finding those booleans supplied as **literals** at construction sites; (3) requiring the file to **consume** the flag from a `Score` / `Classify` / `Verdict` / `Rank` function. Condition (3) keeps the inventory honest — a literal nobody scores is inert.

## Inventory: 7 paths, all BOUNDARY, zero REFUTED

| file | verdict field | disposition |
|---|---|---|
| `ResearchXH/SelectionPrincipleAudit.cs` | `Rule.Derivable` | **BOUNDARY** |
| `ResearchXH/AnchorInventoryAudit.cs` | `Anchor.IsTrueInput` | **BOUNDARY** |
| `ResearchXH/PrincipleCompetitionAudit.cs` | `Principle.Consistent` | **BOUNDARY** |
| `ResearchXH/MeasurementClassAudit.cs` | `MeasurementClass.StructurallyUnique` | **BOUNDARY** |
| `ResearchXH/ActualizationOriginAudit.cs` | `DependencyFact.SupportsDerivedFromDifference` | **BOUNDARY** |
| `ResearchXH/ReorganizationPrediction.cs` | `ReorgMember.Correct` | **BOUNDARY** |
| `Resonance/Theory/MicroscopicChargeProfile.cs` | `FragmentationAttempt.IsValidCharge` | **BOUNDARY** |

Every one is an **authored judgement or input taxonomy with its reason cited in place**. None is a physics result. **No path is REFUTED** — and the registry structurally forbids a REFUTED entry, because a refuted path is *fixed*, not tolerated.

## The one REFUTED path found — and fixed

`AT.Core/ResearchQG/GdaggerOriginAnalyzer.cs`: every evidence flag, the ratio **and** the score were literals at all six construction sites, and `Score` feeds `OrderByDescending` — **the ranking**. The Coincidence row was **internally inconsistent**:

```csharp
new CouplingMechanism("Coincidence", …, double.NaN, false, 1.0, true, false, …, 1.5)
//                                                ^^^^^^^^^^^ NaN numerator, ratio claims 1.0
```

Now derived:

```text
HasExactTwoPi = |predicted − cH₀/(2π)| / (cH₀/(2π)) < 1e−6
RatioToA0     = predicted / a0                        (NaN propagates honestly)
Matches       = |RatioToA0 − 1| ≤ 0.15                (the criterion the Verdict text states)
Score         = 3.0 if exact 2π and matching; else 1.5 if not falsifiable; else 0.5
```

| mechanism | Score (was typed → derived) | HasExactTwoPi | Matches | RatioToA0 |
|---|---:|---|---|---|
| Coincidence | 1.5 → **1.5** | false | false | **NaN** (was typed `1.0`) |
| Mach-like | 0.5 → **0.5** | false | false | computed |
| Cosmic-boundary | 0.5 → **0.5** | false | false | computed |
| Causal-horizon | 0.5 → **0.5** | false | false | computed |
| Information/holographic | 3.0 → **3.0** | true | true | computed |
| Time-scale (QG-080) | 3.0 → **3.0** | true | true | computed |

The derived values **reproduce the previously typed values exactly**, so the fix changes *provenance*, not the physics — and the NaN-ratio contradiction is gone.

## Regression record

`FixedByComputation` lists the eleven sites fixed by G_025/G_026/G_027. They no longer scan, so a **regression** (a literal reappearing) is caught by the scan and **cannot be excused by the registry**.

## Tests

| test | asserts | result |
|---|---|---|
| `Y_G_027_NoUnTriagedLiteralToVerdictPath` | **zero** un-triaged paths — the FAIL condition | ✅ |
| `Y_G_027_RegistryIsCompleteAndBacked` | no stale triage; **zero REFUTED**; every reason cited; scan non-vacuous and exhaustive | ✅ |
| `Y_G_027_GdaggerEvidenceIsComputed` | derived flags/ratio/score reproduce the typed values; NaN contradiction gone | ✅ |
| `Y_G_027_ScannerDetectsTheKnownDefectShape` | fires on the G_025/G_026 fingerprint; **silent** when the flag is computed | ✅ |
| `Y_G_027_Run` | the full report | ✅ |

**Headline:** the rule is now **mechanically enforced**, not merely observed. Seven literal-to-verdict paths remain and all seven are **BOUNDARY** authored judgements/input taxonomies with cited reasons; exactly one path was **REFUTED** (`GdaggerOriginAnalyzer`) and it is **fixed**, with the registry forbidding refuted entries so the fix cannot be undone quietly.
