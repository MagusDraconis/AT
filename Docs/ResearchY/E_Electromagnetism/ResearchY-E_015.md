# ResearchY-E_015 - Sector Weight Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_015 (permanent)
**Title:** Can AT assign a probability or weight to flux sectors n?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_015.md`
**Depends on:** E_014 (n exists, is quantised and is not selected), E_013 (the flux has no potential), E_012 (the whole-torus constraint), E_011 (the holonomy and the quantum), E_009 (the update rule is purely electric), E_003 (the phase)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_015_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/SectorWeightAudit.cs`

## The question

Can AT assign a **probability or weight** to flux sectors `n`? Candidates: **occupancy measure**, **actualization
count**, **entropy**, **free room**, **multiplicity structure**, **sector topology**. Compute `P(n)`. Test: does any
AT-derived quantity **prefer** `n = 0` or `|n| > 0`? Goal: is the sector **weighted** or **completely free**?

## The answer: **the measure is EXACTLY FLAT - the sector is COMPLETELY FREE**

> `P(n) = 1/k` for every `n`. The **ratio** `P(n)/P(m) = 1` is **derived exactly**; the **absolute normalisation** is a
> **boundary**, because a flat measure over an unbounded label needs a regulator and AT supplies none.

## 1. The measure, counted rather than argued

Every configuration of a periodic chain - link phases on `k` equally spaced values - is **enumerated** and binned by
**holonomy class**:

| (k, l) | configurations per sector | total | uniform |
|---|---|---|---|
| (4, 6) | **1024** | 4096 | True |
| (6, 5) | **1296** | 7776 | True |
| (8, 4) | **512** | 4096 | True |
| (5, 7) | **15625** | 78125 | True |

Bin counts at (4, 6): **1024, 1024, 1024, 1024**. `P(n) = 0.250000000`, `P(n)/P(m) = 1.000000`.

**The equality is a theorem, not a coincidence of the sizes tested.** The shift that adds one quantum to one link is a
**bijection** of the configuration set and moves the class by **exactly one** - verified on every model - so the bins
*must* be equal.

## 2. Three of the six candidates are ONE quantity under three names

| quantity | value |
|---|---|
| largest entropy spread across sectors | **0.000E+000** |
| smallest free-room ratio (min / max) | **1.000000** |
| multiplicity | constant (see the table above) |

**Entropy, free room and multiplicity structure are all the number of configurations per sector.** They do assign a
weight - and the weight they assign is the **uniform** one: a weight with **no preference in it**.

| (k, l) | entropy per sector | multiplicity |
|---|---|---|
| (4, 6) | 6.931472 | 1024 |
| (6, 5) | 7.167038 | 1296 |
| (8, 4) | 6.238325 | 512 |
| (5, 7) | 9.656628 | 15625 |

## 3. The other three are refuted, each on its own measurement

| candidate | measurement | value |
|---|---|---|
| occupancy measure | weight read in both sectors | spread **0.000E+000** (control: sectors differ by **1.000000** quanta) |
| actualization count | update rule's spatial part | **0.000E+000** (time-like **1.424E-002**) - no transition to count |
| sector topology | cycle holonomy phase distance | **4.899E-016** for **every** sector |

The **topological charge is zero for n = 0 and n != 0 alike** (E_011's trivial holonomy), while the **strength
differs** - so topology cannot weight the sector.

## 4. The free-energy difference between sectors

A Boltzmann weight needs an action. AT's **sector-blindness residual** (E_014, reused) *is* the free-energy
difference, and it is **4.163E-017** - zero at machine precision.

## 5. P(n) and what is derived about it

- `P(n) = 1/k`, **flat in n, for every n**.
- The **ratio** `P(n)/P(m) = 1.000000` is **derived exactly** (by bijection and by enumeration).
- The **absolute normalisation** is **not** derived: the weight of a single sector is undefined without an input.

## Verdict

**BOUNDARY** - over a **derived flatness**. The sector is **completely free**: no AT-derived quantity prefers `n = 0`
or `|n| > 0`. The verdict is **computed** and has a **live branch in every direction**: a non-flat multiplicity, a
sector-dependent occupancy weight, a label-changing update or a sector-dependent invariant would each make the weight
**DERIVED**.
