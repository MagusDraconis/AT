# Y_M_016_Result.md — ResearchY-M_016 Criterion-Independence Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_016_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 10/10 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_016"`

---

## Summary

**Question:** Are the four M_014 score criteria independent over N = 16..512?
A = 0 unpaired modes; B = 3 families ∧ span < 8; C = 6|N; D = N = 3·2^k.

**Verdict:** **PARTIALLY REDUNDANT.** The criteria are NOT independent — A, C, D form a
deterministic inclusion chain D ⊆ C ⊆ A over [16,512] (every rung is 6-divisible and
zero-unpaired; every 6-divisible ring is zero-unpaired), giving pairwise phi up to 0.285
(A–C), I(A;C) = 0.0912 bits, and total correlation 0.1179 bits (5.5% of ΣH). Yet NO
criterion is redundant given the other three: unique-information fractions are 0.895 (A),
0.999 (B), 0.827 (C), 0.672 (D); PCA of the standardized criteria has eigenvalues
[1.397, 1.001, 0.935, 0.667] (participation ratio 3.744 ≈ 4 effective dimensions); B is
statistically near-independent (I(B;·) ≤ 0.0004 bit). N = 96 is selected by the **pair
{B, D}**: B holds exactly on the 3-family window [60,120] (61 rings), D on the seed-3 rung
ladder {24, 48, 96, 192, 384}, and their conjunction is the singleton {96}. A and C add
no selection power for 96 once {B,D} is imposed (they are implied by D at rungs). This
confirms from information-theoretic and minimal-selector angles the M_014/M_015 finding
that B and D are the load-bearing discriminators.

## Key measurements (N = 16..512, 497 rings)

| Quantity | Value |
|---|---|
| rings where A / B / C / D | 354 / 61 / 83 / 5 |
| marginal entropy H(A/B/C/D) | 0.866 / 0.537 / 0.651 / 0.081 bits |
| joint entropy H(A,B,C,D) | 2.017 bits |
| ΣH − H_joint (total correlation) | 0.1179 bits = 5.5% |
| max pairwise I | A–C = 0.0912 bits |
| max pairwise phi | A–C = 0.285 |
| nesting | D ⊆ C ⊆ A (deterministic) |
| H(X_i\|others) unique fraction | A 0.895, B 0.999, C 0.827, D 0.672 |
| only criterion predictable from others | C (+1.0% over majority) |
| PCA correlation eigenvalues | [1.397, 1.001, 0.935, 0.667]; PR 3.744 |
| minimal selector of N = 96 | {B, D} (B∧D = {96}) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_016_CriterionBits` | A/B/C/D on 354/61/83/5 of 497 rings; 96 satisfies all four | ✅ |
| `Y_M_016_Entropies` | ΣH = 2.135, H_joint = 2.017, TC = 0.118 bit | ✅ |
| `Y_M_016_MutualInformation` | pairwise MI matrix | ✅ |
| `Y_M_016_CorrelationMatrix` | phi matrix | ✅ |
| `Y_M_016_NestingChain` | D ⊆ C ⊆ A; B∧D = {96} | ✅ |
| `Y_M_016_UniqueInformation` | unique fractions ≥ 0.672 | ✅ |
| `Y_M_016_Predictability` | only C gains from others | ✅ |
| `Y_M_016_Pca` | eigenvalues + participation ratio 3.744 | ✅ |
| `Y_M_016_SelectorOf96` | B on [60,120], D on rung ladder, {B,D} = {96} | ✅ |
| `Y_M_016_Run` | research report | ✅ |

## Conclusion

The four M_014 criteria are partially redundant: the A–C–D inclusion chain carries 5.5%
total-correlation redundancy, but no criterion is a function of the others, B is
independent, and the set spans ≈3.74 effective dimensions. N = 96 is uniquely selected by
the pair {B, D}; A and C are implied. No reclassification of M_014/M_015; no new
primitive; canonical AT unchanged.
