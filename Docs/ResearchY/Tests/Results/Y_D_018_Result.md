# Y_D_018_Result.md — ResearchY-D_018 Occupancy Selection Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_018_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ 11/11 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_018"`

---

## Summary

**Question:** Why does N=96 generate occupancy structure [4,4,87]? Is [4,4,87] the true
selection mechanism behind D96?

**Verdict:** **[4,4,87] is unique to N=96 only trivially.** The occupancy map
N → occupancy is a **bijection** over [32,300] — all 269 N values produce 269 distinct
patterns, and adjacent N always differ. Every occupancy value is one-of-a-kind, so the
rarity of [4,4,87] carries no selection information.

## Key measured values

| Quantity | Value |
|---|---|
| Exact [4,4,87] occurrences in [32,300] | 1 (N=96 only) |
| [4,4,87] as a prefix | 2 (N=96, N=128) |
| Distinct occupancy patterns (N ∈ [32,300]) | 269 / 269 (bijection) |
| Adjacent N always differing | 268 / 268 |
| band₁ = 4 | 266 / 269 |
| [4,4,...] prefix | 230 / 269 |
| [4,4,...] in 3-family window [71,120] | 50 / 50 |
| Identity occ(N) = [4,4,N−9] | holds for all 50 rings in [71,120] |
| occMom(96) | 1900.25 = (87²+32)/4 (monotone, no extremum) |
| occMom max in scan | N=300 (17416) |
| occMom(71) → occMom(96) → occMom(120) | 969 → 1900.25 → 3088.25 (strictly increasing) |
| Top-octave share | 0.910 (N=90), 0.916 (N=96), 0.933 (N=120) — monotone |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_018_Uniqueness` | [4,4,87] exact only at N=96 in [32,300] | ✅ |
| `Y_D_018_Bijection` | N → occupancy injective; 269 distinct; adjacent N differ | ✅ |
| `Y_D_018_PrefixGeneric` | [4,4,...] generic (230/269); band₁=4 266/269 | ✅ |
| `Y_D_018_Identity` | occ(N) = [4,4,N−9] for all 50 rings in [71,120] | ✅ |
| `Y_D_018_PrefixNotUnique` | [4,4,87] also a prefix at N=128 | ✅ |
| `Y_D_018_OccMomMonotone` | occMom strictly increasing; no extremum at 96 | ✅ |
| `Y_D_018_OccMomFormula` | occMom = (x²+32)/4 for [4,4,x] | ✅ |
| `Y_D_018_NoPlateau` | occupancy changes at every ΔN (least stable) | ✅ |
| `Y_D_018_InfoConcentration` | top-octave share monotone, not N=96-specific | ✅ |
| `Y_D_018_SelectionRefuted` | occupancy-selected REFUTED; closure-selected (D_017) | ✅ |
| `Y_D_018_Run` | Research report | ✅ |

## Conclusion

**D96 is NOT occupancy-selected.** The occupancy pattern [4,4,N−9] is a *derived
bijection* of N within the three-family window [71,120]; [4,4,87] is unique to N=96 only
in the trivial sense that every occupancy is unique (a bijection carries zero selection
power). occMom is monotone increasing (no extremum at 96); occupancy is the least stable
structure under ΔN (adjacent N always differ). N=96 remains **closure-selected** (D_017,
Ch5 attractor). [4,4,87] is a **DERIVED** projection of the closure selection.

No canonical value was changed.
