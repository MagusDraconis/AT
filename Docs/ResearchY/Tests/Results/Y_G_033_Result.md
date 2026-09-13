# Y_G_033 Result — Cubic Substrate Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_033_Tests.cs`
**Status:** 5/5 PASSED (~0.6 s)
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_033"`
**Group total:** G_001–G_033 = **265/265 PASSED**

## Verdict

**PARTIAL** — gravity needs the cubic substrate (**SAME** requirement as the rest of physics) but the use is **era-local**, and the requirement is discharged by the primitive **η** rather than exercised. Computed from the live scan plus the subduction (ResearchY-G_027).

## 1. In code — era-local (live scan, 34 suites classified)

| class | count | members |
|---|---|---|
| D96 in **code** | **20** | G_002–G_011b, G_012, G_013, G_014, G_016, G_016b, G_018, G_023, G_024, G_026 |
| comment-only | 1 | G_001 |
| **no reference at all** | **13** | **G_015, G_017, G_019, G_020, G_021, G_022, G_025, G_027, G_028, G_029, G_030, G_031, G_032** |

The 13 are exactly the **metric / closure era** — pure continuum PPN work. `Y_G_033` excludes itself from classification (it computes the spectrum in order to audit it).

## 2. In structure — inherited, not eliminated

```
spatial metric g_ij: 6 components = 1 (trace, l=0) + 5 (traceless, l=2)
subduction onto O (order 24, dims 1·1·2·3·3, Σd²=24):
    l=0 (1)  ‖χ‖²=1  -> A1
    l=1 (3)  ‖χ‖²=1  -> T1(3)              <- M_012's vector sector
    l=2 (5)  ‖χ‖²=2  -> E(2) + T2(3)       <- the metric's traceless part
    l=3 (7)  ‖χ‖²=3  -> A2(1)+T1(3)+T2(3)
    l=4 (9)  ‖χ‖²=4  -> A1(1)+E(2)+T1(3)+T2(3)
```

Reproduces M_012 exactly. The traceless metric part **requires a dimension-3 irrep (T2g)**; a single D96 ring is a cycle graph C₉₆ whose symmetry D₉₆ has irreps of **dim 1–2 only** → it supplies neither T1u nor T2g. **Gravity requires D96³.** Dimension 3 is derived: rotation self-duality `d(d−1)/2 = d` has the **unique** solution **d = 3**.

## 3. The locus — η

The metric era imports 3D space through the **primitive η** (G_032: *"a TRUE THEORY INPUT … part of the geometry, not a choice"*, QG289). **G_032 and G_033 are one fact seen twice:** the shape of space is an input, and that input is what the cubic lattice would otherwise have supplied.

## 4. Defect — A₀ = 20 812 is a floating-point artifact

| method | value |
|---|---|
| exact-double keying, .NET — the cited figure | **20 812** |
| exact-double keying, Python (same algorithm) | **20 440** |
| **tolerance-clustered (robust)** | **16 080** |
| exact keying under one-ulp (1e−16) noise, 6 trials | **20 488 … 21 369** (range **881**) |
| tolerant count under 1e−16 / 1e−13 noise | spread **0** / **0** |

Decimal-place profile: `6→16072, 7–10→16080, 11→16085, 12→16105, 13→16319, 14→18282` — a genuine plateau at 7–10.

**16 080 is M_012's own figure and the value used elsewhere in the repo** (`NewChat_Start`: *"D96³ 136 005 of 868 656"*; 868 656 = 884 736 − 16 080).

| source | A₀ | free room | L |
|---|---|---|---|
| group G / `DensityField` / `SpectralCaseCatalog` (code) | 20 812 | 863 924 | 0.976477 |
| M_012 doc + `NewChat_Start` | **16 080** | **868 656** | **0.981825** |

**Survives:** the qualitative claim (D96³ ≈ 98 % energy-free; landscape ~150× larger; compression dominated by A).
**Does not survive:** 20 812, 863 924, L = 0.97648, and the lock-release 3.948614 derived from them.

**Not fixed deliberately** — the keying lives in shared infrastructure consumed by other research groups; changing it renumbers A₀ programme-wide and is a programme-level decision. Registered as an open item.

## Files

- Core: `AT.Core/ResearchXH/CubicSubstrateAudit.cs`
- Suite: `AT.Tests/ResearchY/G_GravitySource/Y_G_033_Tests.cs`
- Doc: `Docs/ResearchY/G_GravitySource/ResearchY-G_033.md`
