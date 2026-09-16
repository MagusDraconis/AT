# ResearchY-G_069 - Neutron Star Decision Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_069 (permanent)
**Title:** For which compactness does AT differ from GR by 1σ, 3σ and 5σ?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_069.md`
**Depends on:** G_068 (the temporal prediction and its magnitude), G_019 (the second-order signature), G_020 (the neutron-star error budget and the NICER compactness), G_035 (the surviving time sector)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_069_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/NeutronStarDecisionAudit.cs`

## The question

For which **compactness x** does AT differ from GR by **1σ, 3σ, 5σ**, given **current and projected NICER-class
uncertainties**? Measure **Δz** and **σ_obs**. Output **CURRENTLY UNDECIDED / REACHABLE / EXCLUDED**. Goal: the **first
realistic observation** that can decide AT against GR.

## The answer: **REACHABLE - the projected test crosses 3σ at x = −0.121, and the verdict rests on the COMPACTNESS assumption as much as on the timing**

## 1. The map is of the significance, not of the separation

Δz grows monotonically with compactness (**2.730E-003** at `x = −0.0492` to **1.617E-001** at `x = −0.2685`), so the
question is whether the **uncertainty** grows faster - and that depends on **which uncertainty model** is used:

| model | σ at the earlier deciding object | significance |
|---|---|---|
| **SINGLE theory** (what *excluding* AT means) | **0.044812** | **1.05 σ** |
| **SHARED-x difference** (slopes cancel) | **0.040233** | **1.17 σ** |
| **QUADRATURE** (the earlier audits' form) | **0.070118** | **0.67 σ** |

The **single-theory form reproduces the earlier audit's 1.03σ**, so the difference between the models is a **modelling
choice rather than an arithmetic disagreement**. The shared-x form is the smallest because the two theories' slopes agree
to first order (`dz_AT/dx = −1.1881`, `dz_GR/dx = −1.8848`, difference **−0.6968**), so the compactness uncertainty
**largely cancels in the difference**; the quadrature form is the largest because it adds two errors that share a cause.

## 2. The separation map (radius fixed, compactness from the mass)

| M (M☉) | R (km) | x | z_AT | z_GR | separation |
|---|---|---|---|---|---|
| 0.4 | 12.0 | −0.0492 | 0.050466 | 0.053196 | **2.730E-003** |
| 1.0 | 12.0 | −0.1231 | 0.130979 | 0.151761 | 2.078E-002 |
| 1.4 | 12.0 | −0.1723 | 0.188055 | 0.235259 | 4.720E-002 |
| 2.2 | 12.0 | −0.2708 | 0.310992 | 0.476939 | **1.659E-001** |
| 1.4 | 10.0 | −0.2068 | 0.229713 | 0.305836 | 7.612E-002 |

## 3. The thresholds, and the decision map

**The projected difference test crosses:** **1σ at x = −0.046**, **3σ at x = −0.121**, **5σ at x = −0.188**.
**The current quadrature form would need x = −0.490 for 3σ** - beyond any published object.

| object | x | Δz | now (single) | now (quadrature) | projected difference | classification |
|---|---|---|---|---|---|---|
| 0.4 M☉ / 12 km | −0.0492 | 2.730E-003 | 0.23 | 0.16 | 1.08 | CURRENTLY UNDECIDED |
| 0.6 M☉ / 12 km | −0.0739 | 6.543E-003 | 0.36 | 0.25 | 1.69 | CURRENTLY UNDECIDED |
| 0.8 M☉ / 12 km | −0.0985 | 1.242E-002 | 0.51 | 0.35 | 2.35 | CURRENTLY UNDECIDED |
| **1.0 M☉ / 12 km** | **−0.1231** | 2.078E-002 | 0.67 | 0.45 | **3.06** | **REACHABLE** |
| 1.4 M☉ / 12 km | −0.1723 | 4.720E-002 | 1.05 | 0.67 | 4.55 | REACHABLE |
| 1.8 M☉ / 12 km | −0.2216 | 9.201E-002 | 1.55 | 0.92 | 5.91 | REACHABLE |
| 2.2 M☉ / 12 km | −0.2708 | 1.659E-001 | 1.96 | 1.16 | 7.61 | REACHABLE |
| 1.4 M☉ / 10 km | −0.2068 | 7.612E-002 | 1.37 | 0.82 | 5.23 | REACHABLE |

**CURRENTLY UNDECIDED: 0.4, 0.6 and 0.8 M☉ at 12 km (3 objects). REACHABLE: ten objects, from 1.0 M☉ upward.
EXCLUDED: none.**

**The first realistic observation is an object of about 1.0 M☉ at 12 km radius (`x = −0.123`)** - inside the published
NICER mass range, so **no new class of object is required**.

## 4. The requirement read backwards - and how much the projection assumes

| significance | at 1.0 M☉ (x = −0.123) | at 1.8 M☉ (x = −0.2216) |
|---|---|---|
| **1σ** | **15.81 %** | 36.91 % |
| **3σ** | **5.11 %** | 11.77 % |
| **5σ** | **2.86 %** | 6.38 % |

These are **timing** requirements: the compactness uncertainty cancels in the difference.

**And the audit measures how much the projection leans on its own assumption:** the 3σ threshold is

| compactness assumption | 3σ threshold |
|---|---|
| **best current NICER (3.661 %)** | **x = −0.121** |
| **perfect compactness knowledge** | x = −0.118 |
| **generic NICER (11.90 %)** | **x = −0.490** |

**With a generic compactness the decision is not reachable inside the physical range at all** (`x = −0.5` is GR's
horizon). So the **REACHABLE** verdict rests on a compactness assumption **as well as** a timing one - the audit says so
rather than presenting the best case as the case.

## Verdict

**REACHABLE.** The decision becomes possible at **x = −0.121** (3σ) with a **5 % redshift determination** and a
**NICER-class compactness at the current best (3.7 %)**; the first object is about **1.0 M☉ at 12 km**, and the
requirement is **purely timing** at that object (**5.11 % for 3σ**). Nothing in the published range is **EXCLUDED** -
current significances reach only **1.96σ** even at 2.2 M☉ - and **nothing is decidable today**.
