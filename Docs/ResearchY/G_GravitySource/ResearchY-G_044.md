# ResearchY-G_044 — Minimal Working Substrate Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_044 (permanent)
**Title:** Is D96³ selected because it is the first working substrate, or because it minimises complexity?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_044.md`
**Depends on:** G_041 (the cube is the minimal working dimension), G_042 (the selectors reduce to one root), G_043 (the growth families are monotone; the mechanism is undetermined), G_040 (orbitals = 49 at d = 1), G_033 (the dimension-3 irrep requirement)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_044_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/MinimalWorkingSubstrateAudit.cs`

## The question

Is D96³ selected because it is the **first working** substrate, or because it **minimises complexity**?
Compared: **D96², D96³, D96⁴, D96⁵**. Measured: state-space size, irreps, photon support, graviton support,
observability fraction, states per observable.

## The answer: **EMERGENT — the two proposed explanations are the SAME statement, and optimality is refuted**

## 1. The working set is an up-set — computed, not assumed

A substrate **works** when both sectors carry **propagating** states (photons `d−1 > 0`, gravitons
`(d+1)(d−2)/2 > 0`) **and** the symmetry supplies a dimension-3 irrep (G_033).

| d | photon propagates | graviton propagates | 3-dim irrep | **works** |
|---|---|---|---|---|
| 1 | no | no | no | no |
| 2 | yes | **no** | no | **no** |
| 3 | yes | yes | yes | **YES** |
| 4–6 | yes | yes | yes | **YES** |

**The working set is {3, 4, 5, …}** — one lower edge, no gaps.

**Why d = 2 fails, precisely** (sharper than "too small"): the graviton's **sector exists** there — the
traceless symmetric rank-2 is 2-dimensional in two dimensions — but it carries **zero propagating states**.
*Availability is not physics.*

## 2. All six measures are monotone in the same direction

| measure | D96² | D96³ | D96⁴ | D96⁵ | direction |
|---|---|---|---|---|---|
| state space 96^d − 1 | 9 215 | 884 735 | 84 934 655 | 8 153 726 975 | worse as d grows |
| irreps (count of irreps of B_d) | 5 | 10 | 20 | 36 | worse as d grows |
| photon support (vector sector) | 2 | 3 | 4 | 5 | worse as d grows |
| graviton support (traceless sector) | 2 | 5 | 9 | 14 | worse as d grows |
| observability fraction (orbitals/state) | 0.1329 | 0.02354 | 0.00319 | 0.00035 | **falls** |
| states per observable | 7.522 | 42.484 | 313.730 | 2 841.332 | worse as d grows |

The observability measure falls with d, so its **cost** is its reciprocal — the states per observable — which
restores the common direction. **Every cost is strictly increasing in d.**

## 3. Therefore minimality ≡ firstness — as a theorem, not a finding

With strictly increasing costs, **the cheapest member of ANY set is that set's smallest element**. Over the
working set, the minimum-complexity substrate **IS** the first working substrate — for all six measures at once,
automatically, not because they agree here but because **they cannot disagree**.

> *"Selected for being first"* and *"selected for minimising complexity"* are **one statement**.

That is why the verdict is **EMERGENT** rather than derived: the minimality is a **corollary that emerges from
first-ness**, not an independent optimum.

## 4. Optimality, by contrast, is REFUTED

The unconstrained optimum of **every** measure lies at **d = 1 or 2 — outside the working set**:

* D96³ is **96×** the state space of D96²,
* **5.65×** its states per observable,
* **5.65×** less observable.

**D96³ is the best *working* substrate and a poor substrate outright.** "D96³ is optimal" is false for all six
measures simultaneously.

## 5. A computed aside: the compulsory move is the cheapest move

The observability **cost factor** per dimension: **3.88** (1→2), **5.65** (2→3), **7.38** (3→4), **9.11** (4→5).
It grows with every step, so among the steps the theory is **allowed** to take — from d = 2 onward — the
**mandatory** step (2→3) is the **least expensive**. The step 1→2 is cheaper still but is not available, since
d = 1 does not work. This is a **convenience**, not a selection argument, and the audit records it as such.

## 6. The three options, as computed

| option | status | basis |
|---|---|---|
| **MINIMAL** | **EMERGENT** | the minimum-complexity working substrate IS the first working substrate for all six measures, because every cost is monotone — minimality **emerges** from first-ness |
| **OPTIMAL** | **REFUTED** | every measure's unconstrained optimum sits at d = 1 or 2, outside the working set |
| **FIRST** | **DERIVED** | the working criteria first hold together at d = 3; the working set is {3,4,5,6}, an up-set |

## 7. Caveats

**(a) "Minimises complexity" is ambiguous across measures, and the audit does not weight them.** Each is
reported with its own direction; the theorem needs only monotonicity, which all six have, so no weighting is
required for the collapse of MINIMAL into FIRST.

**(b) Optimality is defined over the whole ladder**, including substrates that do not work — that is what makes
"optimal" a *different* claim from "minimal", and it is the claim that fails.

**(c) The observability measures rest on the orbital count** `C(48 + d, d)`, verified against G_040's 49 at
d = 1 (G_043's cross-check). For d ≥ 2 it uses the same multiset argument.

**(d) What this audit does NOT settle** is G_043's question — *why* the working set begins at three. This audit
shows only that **everything above the lower edge is irrelevant to the choice**: since all costs increase with d,
no substrate beyond the first working one can ever be preferred, whatever the weighting.

## 8. Classification

**Registry:** added to the G_035 classification registry as **`ClockOnly`** → **SURVIVES** (counts, growth laws
and symmetry facts), triaged `ScanDetectsIt: false`. Counts become **31 / 11 / 3 of 45**; the boundary index is
unchanged; no prior classification changed.

**No verdict changed.** G_041 (BOUNDARY), G_042 (REDUNDANT: one root), G_043 (BOUNDARY) and G_033 are unchanged
inputs. This audit **settles the classification** (D96³ is MINIMAL, MINIMAL ≡ FIRST, not OPTIMAL) while leaving
G_043's mechanism question open. The D_040 registry is untouched; no canonical claim, value or equation changes;
no new primitive is added.

**Scanner side-effect, recorded:** G_033's live classifier now reads **26** substrate suites (43 classified in
total).
