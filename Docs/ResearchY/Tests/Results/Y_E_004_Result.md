# Y_E_004 Result — Vector Sector Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_004_Tests.cs`
**Status:** 6/6 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_E_004"`
**Group total:** group E = **26/26 PASSED** (E_001 7 + E_002 6 + E_003 7 + E_004 6)

## Verdict

**BOUNDARY** — D96³ is necessary **and** sufficient at the level of **representation**, and insufficient at the
level of **dynamics**. What is missing is the **choice of kinetic form** that reduces three states to two.

## 1. The vector sector exists — only on D96³

| | |
|---|---|
| `l = 1` → | **T1**, dimension **3**, multiplicity **1** (irreducible) |
| single ring max irrep dim | **2** → vector sector **NO** |
| cubic D96³ | **YES** |

**A Lorentz 4-vector needs BOTH substrates:** `A_μ = (A_0, A_i)` = **A1(1) + T1(3) = 4**. The ring supplies only
`A_0`; the cubic supplies only `A_i`. **Neither alone can carry a Lorentz vector.**

## 2. The two forms disagree on the same representation

| form | matrix | rank | kernel | states | |
|---|---|---|---|---|---|
| identity `V·V` | `I` | **3** | **0** | **3** | ← **PROCA** |
| curl `F_ij F_ij` | `2(I − k̂k̂ᵀ)` | **2** | **1** | **2** | ← **MAXWELL-capable** |

| mode | identity | curl |
|---|---|---|
| longitudinal | 1.000000 | **0.000×10⁰** (exact) |
| transverse | 1.000000 | 2.000000 |

The transverse projector has rank **2** in all 12 directions tested. Gauge directions: **0 from the
representation**, **1 from the field space**. The identity form is the representation's own invariant, so
**T1(3) alone yields a massive vector.**

## 3. Masslessness is a limit statement

| n | `μ_min` | `μ_min·n²` |
|---|---|---|
| 12 | 12.000000 | 1728.0 |
| 48 | 1.504529 | 3466.4 |
| **96** | **0.386351** | **3560.6** |
| 384 | 0.024350 | **3590.5** |

`μ_min ~ 3591/n² → 0` (spread **0.84 %** for n ≥ 96). Cubic gap = 3× the ring's. The gap is a **finite-size
artefact, not a mass**.

## 4. The five requirements

| requirement | single D96 | D96³ |
|---|---|---|
| 1 vector d.o.f. | **NONE** (max dim 2) | **T1(3), irreducible** |
| 2 two polarisations | n/a | 2 of 3, *given a reason to project* |
| 3 gauge redundancy | n/a | **not supplied** — irrep has no orbit |
| 4 massless propagation | n/a | **in the limit only** |
| 5 Maxwell limit | n/a | **imported** (E_002) |

## The missing primitive, located exactly

Not the phase (E_003 found AT already has one), not the group, and not the vector representation — but the
**choice of kinetic form** that takes three states to two.
