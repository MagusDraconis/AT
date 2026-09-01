# Y_NP_025_Result.md — ResearchY-NP_025 K=6 Uniqueness Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_025_Tests.cs`
**Run:** 2026-09-01
**Result:** ✅ 6/6 PASSED
**Full suite:** 698/698 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_025"`

---

## Summary

**Question:** Is ω(√12)/ω(√14) = √(6/7) unique to K=6, or does the same protected
inter-block structure appear in other circulant rings C_N(±1..±K)?

**Verdict:** NOT unique to K=6 — √(6/7) is the K=6 member of the universal K-family
**√(K/(K+1))**, an N-independent protected inter-block ratio of every circulant ring
C_N(±1..±K) with K ≥ 2 (whenever the non-doublet blocks appear).

## The scan (K=1..12 at N=96)

| K | protected ratio | = √(K/(K+1))? |
|---|---|---|
| 2 | 0.81650 | ✅ |
| 3 | 0.86603 | ✅ |
| 4 | 0.89443 | ✅ |
| 5 | 0.91287 | ✅ |
| **6** | **0.92582** | ✅ |
| 7 | 0.93541 | ✅ |
| 8 | 0.94281 | ✅ |
| 9 | 0.94868 | ✅ |
| 11 | 0.95743 | ✅ |
| 12 | 0.96077 | ✅ |

(K=1 and K=10 at N=96 are size-suppressed — all 2-fold.)

## N-independence

The ratio is a pure K-property: K=6 gives √(6/7) at N=48, 96, and 192 whenever the
blocks appear. The multiplicities are N/K-dependent (K=6 blocks absent at N=64/128).

## Determination: B) family of K-values

- **NOT unique to K=6** (every K ≥ 2 has the protected structure).
- **Family of K-values** — the universal √(K/(K+1)) family.
- **Stronger discriminator:** the ratio is strictly increasing (injective) in K, so
  the measured ratio UNIQUELY identifies K. The multiplicities pin N (K=5 and K=6
  share (6,5) at N=96, so multiplicities alone cannot distinguish K).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_025_RingScan` | K=1..12 degeneracy structure | ✅ |
| `Y_NP_025_NonDoubletBlocks` | every K ≥ 2 has non-doublet blocks | ✅ |
| `Y_NP_025_ProtectedRatios` | each ring's ratio = √(K/(K+1)) exactly | ✅ |
| `Y_NP_025_MultiplicityProtection` | multiplicities N/K-dependent; ratio N-independent | ✅ |
| `Y_NP_025_UniquenessDetermination` | B — family of K-values; stronger discriminator | ✅ |
| `Y_NP_025_Run` | research report | ✅ |

## Conclusion

√(6/7) is NOT unique to K=6 — it is the K=6 member of the universal K-family
√(K/(K+1)), an N-independent protected inter-block ratio of every circulant ring
C_N(±1..±K) with K ≥ 2. The stronger discriminator is the ratio itself (injective in
K, pinning the coupling order) plus the multiplicities (pinning N). Refines NP_024;
elevates the prediction to a general K-family law. No new primitive; canonical AT
unchanged.
