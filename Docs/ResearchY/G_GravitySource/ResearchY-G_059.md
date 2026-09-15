# ResearchY-G_059 - Flow Source Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_059 (permanent)
**Title:** What AT object can produce a non-zero push rank?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_059.md`
**Depends on:** G_058 (all admissible dynamics have identical local rank), G_052 (the interface identity), G_050 (the phase sector is spanned by the substrate's sine modes), G_057 (the recorded actualization phase velocity)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_059_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/FlowSourceAudit.cs`

## The question

What AT **object** can produce a **non-zero push rank**? Given **G_058** that all admissible dynamics have identical
local rank. Candidates: **occupancy imbalance**, **phase imbalance**, **amplitude-phase coupling**, **actualization
pressure**, **spectral mismatch**, **boundary assignment**. Measure the **push vector**, the **flow source** and the
**fixed points**. Goal: locate the **first genuine source term**.

## The answer: **DERIVED - the source is the DIFFERENCE ITSELF, and it is derived rather than argued**

> **The cyclic difference's Fourier multiplier is `1 − e^(−iδ_c)`. Its action on a single visible mode returns exactly
> the multiset `{|sin δ_c|, 2 sin²(δ_c/2)}` - one amplitude part, one phase part, no third term. A filter's multiplier is
> real and symmetric under `c → −c`, which is exactly what preserves the visible subspace, so a filter can never create
> phase content. Tested against all 42 visible modes: exact.**

## 1. The canonical state has no phase content, so a source must create it

| measurement | value |
|---|---|
| the audited state's phase content | **9.246E-015** - the floating-point floor |
| phase-free? | **True** |

The canonical state lies in the **mean + visible** span, so the phase sector is **not where the state is; it is where a
source sends it**. This is also what explains a result G_054 recorded but refused to count: its **"addressed-state
determination" was a tautology because the phase coordinates are zero**, so the statement was `0 = 0`. And it names
**G_052's own reconstruction residual (2.442E-015) as the phase part itself** - that identity was satisfied *because*
the phase part is a floor.

## 2. The push vector per candidate

| candidate | \|push\| | phase part | amplitude part | push rank |
|---|---|---|---|---|
| 1 occupancy imbalance | 1.368E+000 | **7.155E-001** | 1.166E+000 | **1** |
| 2 phase imbalance | 9.246E-015 | 9.246E-015 | 6.391E-029 | 0 |
| 3 amplitude-phase coupling | 8.295E-016 | 7.000E-016 | 4.450E-016 | 0 |
| 4a actualization pressure (uniform) | 9.798E+000 | **0.000E+000** | 0.000E+000 | 0 |
| 4b actualization pressure (local rate) | 3.407E-001 | **6.969E-003** | 3.407E-001 | **1** |
| 5 spectral mismatch | 2.572E-002 | 2.388E-016 | 2.572E-002 | 0 |
| 6 boundary assignment | 1.575E-002 | **9.282E-003** | 1.272E-002 | **1** |

The phase floor is **1E-9**. The phase-null group is exactly the **filter-shaped** candidates (a projection, a level
filter, multiplication by a constant) plus the **uniform advance** - measured at the floor rather than asserted.

## 3. The difference's exact multiplier

| measurement | value |
|---|---|
| visible modes sent into the phase sector | **42 of 42** |
| the antitone identity holds | **True** (to 1E-12) |
| amplitude residue range | **2.141E-003 … 1.998E+000** |
| most phase-heavy channel | **1** (30.6 : 1) |
| most amplitude-heavy channel | **47** (0.033 : 1) |

`Δ` is circulant ⇒ diagonal in Fourier ⇒ **a rotation composed with a rescale**; the phase share is `cot(δ_c/2)`, so the
difference is a **phase source at the low channels** and an **amplitude source at the high ones**.

**A claim is withdrawn.** The first draft called the difference a **pure rotation** - visible carried into phase with no
residue. The measurement refused it: the residue reaches **1.998E+000**, the near-maximum `2 sin²(δ/2)` at channel 47.
The exact multiset replaced the claim.

## 4. Creating versus amplifying, separated by measurement

| group | candidates | fixed points in the family |
|---|---|---|
| **creating** | occupancy imbalance, actualization pressure (local rate), boundary assignment | **only the uniform state** (191 of 192 states moved) |
| **amplifying** | phase imbalance, amplitude-phase coupling, spectral mismatch | **exactly** the phase-bearing states (106 of 192) |

The family is 192 deterministic states: uniform, the audited state, and every non-constant mode displaced by ±1E-3.
**Creating sources push the phase from *any* non-uniform state; amplifiers move *exactly* the phase-displaced states and
no others.** At the canonical state only the **three creating sources** push; the amplifiers need phase content to exist.

## 5. The G_057 accounting, corrected

| measurement | value |
|---|---|
| G_057 recorded | **0.000E+000** |
| returned from side conditions | **True** (spatial part 0.000E+000, census 0) |
| **measured** phase content, uniform reading | **0.000E+000** |
| **measured** phase content, local clock-rate reading | **6.969E-003** |

G_057's implementation returns its number from **two side conditions** and **never measures the phase content of the
time-like component it names** (project rule 5). Measured directly, the **uniform** reading is phase-null at the floor -
because every phase mode sums to zero - so **G_057's conclusion holds**; but the **local clock-rate** reading of the same
pressure measures **6.969E-003**, which is **not** a floor, and **AT defines no update rule for the organisation at
all**. **It is the identification that carries the weight. The number survives; the reason for it does not.**

## 6. Where it stands

- **No candidate is a pure phase source**: every one that pushes the phase pushes amplitude too - the phase-heaviest is
  the **seam pair** at a ratio of **7.296E-001**.
- **No two candidates coincide** (0 degenerate pairs), so the six names describe distinct directions.
- **The located source is the difference itself** - AT's own primitive, and the **first term of the canonical hierarchy**
  Difference → Actualization → Inevitable Spectrum → Physics.

## Verdict

**DERIVED.** The first genuine source term is the **occupancy imbalance**, the difference itself; its push carries a
phase component of **7.155E-001** on a state whose own phase content is **9.246E-015**; and the mechanism is the exact
multiplier `1 − e^(−iδ_c)`, verified on all **42** visible modes. Three candidates create phase content and three only
amplify it; the creating ones are fixed only at the uniform state.
