# Y_QG_014_Result.md — ResearchY-QG_014 Cosmological Selection Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_014_Tests.cs`
**Run:** 2026-08-31
**Result:** ✅ 5/5 PASSED
**Full suite:** 648/648 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_014"`

---

## Summary

**Question:** Is the observed cosmology itself a selector of the observable sector?

**Verdict:** The observed cosmology is a **CONSTRAINT** — not a full selection
mechanism and not a coincidence — that selects the 3-family sector among
pairing-complete candidates.

## The measurement (family counts 2–5)

| Sector | I_occ | ΩΛ | Ωm | q₀ | z_acc | vs observed |
|---|---|---|---|---|---|---|
| 2 families (N=48) | 0.5244 | 0.4773 | 0.5227 | −0.2160 | 0.2224 | −20.7% ❌ |
| **3 families (N=96)** | **0.7513** | **0.6839** | **0.3161** | **−0.5258** | **0.6295** | **0.0% ✅** |
| 4 families (N=192) | 0.8957 | 0.8153 | 0.1847 | −0.7230 | 1.0668 | +13.1% ❌ |
| 5 families (N=384) | 0.9827 | 0.8945 | 0.1055 | −0.8417 | 1.5688 | +21.1% ❌ |

**Only 3 families matches all four observables within the 0.12% precision.**

## Classification

| Reading | Verdict |
|---|---|
| SELECTION (cosmology determines sector) | CONDITIONAL YES — given the observed ΩΛ the sector is fixed, but ΩΛ is an input, not derived |
| **CONSTRAINT (cosmology rules out alternatives)** | **YES — unconditionally (13–21% deviations falsified)** |
| COINCIDENCE (accidental match) | NO — I_occ(96) = 0.7513 is exactly the KL of [4,4,87] (QG228); deterministic |

## Direction of explanation

- **Forward (theory → cosmology): DERIVED, exact** — N=96 → [4,4,87] → I_occ = 0.7513 → ΩΛ = 0.6839 → observed.
- **Backward (cosmology → sector): CONDITIONAL selector = constraint** — observed ΩΛ → only N=96 matches → 3 families.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_014_FamilyMatch` | 3 families uniquely matches all four observables | ✅ |
| `Y_QG_014_OmegaMeasure` | ΩΛ/Ωm/I_occ/q₀/z_acc for family counts 2–5 | ✅ |
| `Y_QG_014_SelectorClassification` | constraint (not selection, not coincidence) | ✅ |
| `Y_QG_014_CoincidenceCheck` | the match is deterministic, not accidental | ✅ |
| `Y_QG_014_Run` | research report | ✅ |

## Conclusion

The observed cosmology is a CONSTRAINT that selects the 3-family sector among
pairing-complete candidates — the only sector reproducing ΩΛ = 0.6839, Ωm = 0.3161,
q₀ = −0.526, and z_acc = 0.630 within precision. It is not a full selection (the
observed ΩΛ is an input, not derived) and not a coincidence (the prediction is
deterministic). No new primitive; canonical AT unchanged.
