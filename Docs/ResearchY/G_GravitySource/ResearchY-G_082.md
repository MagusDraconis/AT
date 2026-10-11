# ResearchY-G_082 — Component Count Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_082_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ComponentCountAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_082`) · **Verdict:** **GAUGE-LIKE** (registry: Boundary)

---

## Question

G_080 established that the substrate's connectivity is **ASSUMED**, that disconnected universes survive and that
**0 of 4** allowed primitives detect a cut. G_081 then measured that connectivity acts on the surviving gravity/time
sector **only through one integer**, the component count `c`: **6 of 29** claims depend on it, **23** are independent.

> **What sets `c`? And is `c` physical or a coordinate choice?**

**Allowed:** Difference, Actualization, the generation relation, count conservation.
**Forbidden:** assuming connectedness, importing topology, **fixing `c = 1`**.

---

## Answer

**GAUGE-LIKE — `c` HAS NO LOCAL PHYSICAL CONTENT, WITH ONE GLOBAL EXCEPTION THE AUDIT MEASURES AND DISCLOSES.**

| Task | Result |
|---|---|
| 1 Is `c` observable? | **Only globally** — 0 local or ratio readouts read it; 3 global readings do |
| 2 Two universes, different `c`, identical local physics? | **Yes** — shape changes **0**, ratio changes **0** at every `c`, in both readings |
| 3 Does any observable read absolute `c`? | **No** — 0 do; the total is normalized away |
| 4 Classification | **GAUGE-LIKE**; DERIVED, EMERGENT and PHYSICAL INPUT refuted by a number each |
| 5 Redundancy | **Confirmed** — `c` can be changed with no local measurement changing |
| 6 `c = 1`: physical or coordinate? | **Coordinate for 23 of 29 claims (0.793), physical for 6** |

---

## 0. The instrument, and why it is the first substantive decision

A counterfactual that **fixes the total cell count** and varies the number of components would **shrink each
component** — and then local physics changes for a trivial reason (a smaller ring), which is not a statement about `c`
at all.

So the audit **copies one component**: the same **24 cells**, `c = 1, 2, 4, 8` times — **24, 48, 96 and 192 cells** —
with the component held fixed and **only `c`** varying. That is the union construction of G_080.

## 1. The redundancy map (tasks 2 and 5)

| reading | copies | total cells | shape changes | value changes | ratio changes | global descriptors | absolute scale |
|---|---|---|---|---|---|---|---|
| unnormalized | 1 | 24 | 0 | 0 | 0 | 0 | 1.000 |
| unnormalized | 2 | 48 | **0** | **0** | **0** | **3** | 1.000 |
| unnormalized | 4 | 96 | **0** | **0** | **0** | **3** | 1.000 |
| unnormalized | 8 | 192 | **0** | **0** | **0** | **3** | 1.000 |
| normalized | 2 | 48 | **0** | **0** | **0** | **3** | **7.937E-001** |
| normalized | 4 | 96 | **0** | **0** | **0** | **3** | **6.300E-001** |
| normalized | 8 | 192 | **0** | **0** | **0** | **3** | **5.000E-001** |

The local reads are the **relative** ones the sector actually makes — potential **differences**, clock **ratios** and
the **gradient** — because the potential shifts *additively* and the clock *multiplicatively* when the state is
rescaled, so only the relative pattern is observable (G_079: ratios and differences are determined, the absolute level
is not). The **shape** of a read set is every value divided by the largest, taken as a distribution: it is **one and the
same shape at every `c`**, in both readings (72 readouts at `c = 1`, 144 at `c = 2`, 288 at `c = 4`, 576 at `c = 8`).

What moves: the **three global descriptors** (the component count, the transport rank `n − c`, the free levels) and —
under the theory's own normalization — a single **uniform** shift of the absolute level, measured as the factor
`c^(−1/d)` = **7.937E-001 / 6.300E-001 / 5.000E-001**. **A uniform scale is exactly what the sector cannot read: 0 of
6 temporal observables read an absolute scale (G_079).**

## 2. The census of readouts (tasks 1 and 3)

| readout | kind | reads `c` | measurement |
|---|---|---|---|
| the clock rate at a cell | local | **no** | the read shape is invariant at every `c` |
| the potential at a cell | local | **no** | the read shape is invariant at every `c` |
| the gradient inside a component | local | **no** | invariant, and it is the acceleration G_077 measures |
| the redshift between two cells of one component | ratio | **no** | the ratio changes by **0** at every `c` |
| the redshift between cells of different components | cross-component | **no** | **no value at all**: 0 links across a cut |
| the transport rank of the divergence | global | **yes** | a property of the **state space**, not a readout: `n − c` |
| a transfer attempted across a cut | global procedure | **yes** | needs **two components at once**; the transfer has no flux |
| the total count | global | **yes** | the one quantity the union changes — **normalized away** |

**3 of the 8 readouts read `c`, and all 3 are global; 5 cannot see it at all; 0 of the local or ratio kind do.**

**The total count is a convention, not a measurement:** the theory's own Born normalization (`Σρ = 1` is exact by
construction) divides the union by `c`, so the normalized union has total **1** and each component **1/c** — measured in
the suite — and no local read can tell the difference.

## 3. The classification (task 4)

| candidate | supported | measurement |
|---|---|---|
| DERIVED | **no** | **0 of 4** allowed primitives detect a cut, and the union of a solution is a solution |
| EMERGENT | **no** | the operator that would join two components is **absent**: **0** Laplacian entries between components over **6** disconnected substrates, against a **live control of 4** entries on the connected one |
| **GAUGE-LIKE** | **yes** | **0** local readouts read `c`; the union changes the read shape by **0** and every ratio by **0** |
| PHYSICAL INPUT | **no** | **0** observables read `c`; the **3** readings that do are **global procedures** |

**COMPUTED CLASSIFICATION: GAUGE-LIKE.**

## 4. Physical or coordinate (task 6)

The split is **G_081's own dependency map**, not a fresh judgement:

- **coordinate choice** (c-independent): **23 of 29** claims, **0.793**;
- **physical** (c-dependent): **6** — the transport layer: the rank `n − c`, the nullity `c`, the failure count `c − 1`,
  the free levels `c − 1`, the ledger's region conclusion, cross-substrate reachability.

**By the rule the assignment states — if every local observable is `c`-invariant, `c` is unphysical — `c` is
UNPHYSICAL**, and the audit adds the one exception rather than hiding it: **a global reachability test really can read
`c`**, so GAUGE-LIKE is **not** "pure gauge". `c` is unobservable **locally**.

## 5. What sets `c`?

**Nothing the theory contains.** **0 of 4** allowed primitives detect a cut (G_080); the class **conserves** the
component count and never produces it (G_081, and the EMERGENT predicate here); and the union of a solution is a
solution. So `c` is not set by the state, by the dynamics, or by any primitive: it is an **initial datum of the
substrate**, and the canonical `c = 1` is the **coordinate** in which the transport layer is defined.

---

## Defects found while building this audit

1. **A flag that confused "the vector grew" with "the values changed".** The redundancy test first compared the local
   read vector *positionally*, so at `c = 2` it had twice as many readouts and reported a change — measuring the
   audit's own indexing rather than the physics. Fixed by comparing **distributions** (the sorted distinct values).
2. **The invariance was measured on the wrong quantities.** The first version compared **absolute** potentials and
   clocks, whose shape is *not* invariant under normalization — the potential shifts additively, the clock
   multiplicatively — so the audit would have reported a change where the theory has none. Fixed by reading the
   **relative** quantities the sector actually reads (differences and ratios), which is what makes the gauge argument
   a statement about observables rather than about arithmetic.
3. **An EMERGENT predicate that was not a measurement.** The first version keyed the EMERGENT branch on global
   descriptors moving with `c` — which is not the same as the dynamics *generating* `c`, and it made GAUGE-LIKE
   unreachable. Replaced by the operator test (0 Laplacian entries between components) **with a live control**, since a
   zero with no control proves nothing.
4. **A vacuous measurement caught before it shipped.** The first replacement for the EMERGENT predicate compared a
   quantity with itself (`ComponentsOf(x) != ComponentsOf(x)`), which is always false and would have "measured" that
   the dynamics never changes `c` by construction. Caught by reading it as a reader would, and replaced.
5. **A normalization that left the total at 71.** The first normalized union divided by the number of copies instead of
   by the copies times the component's own sum, so the "normalized" state had total 71 rather than 1 — the test caught
   it, and the assertion now pins the total to 1 and each component to `1/c`.
6. **A census total that disagreed with its parts.** `ObservableCounts` counted only kinds equal to `"global"` while
   the census has a `"global procedure"` row, so the parts summed to 7 of 8; fixed by matching the prefix.

## What this does and does not establish

- **Does:** show that **no local or ratio readout** reads `c`; that two universes with different `c` produce identical
  local physics up to a **uniform shift** that no observable reads; that the redundancy is **confirmed**; that
  **nothing in the theory sets `c`**; and that `c = 1` is a **coordinate choice for 23 of 29 claims** and a physical
  statement for the 6 that make up the transport layer.
- **Does not:** claim `c` is **unobservable** (a global reachability test does read it); claim the result for a
  counterfactual that fixes the total cell count (that changes the component size, and local physics then changes for a
  different reason); claim the gauge argument without its premise — **the uniform shift is unobservable only given
  G_079's measurement** that no temporal observable reads an absolute scale; or claim the normalization is anything
  other than the theory's own convention.

## Where it stands

**`c` is gauge-like for the local sector and physical for the transport layer, and the audit measures both sides.**
The question the last three audits converged on — what sets the component count — has an answer that is itself a
measurement: **nothing the theory contains**, which is why the theory can be read as a local physics with an integer
initial datum attached, and why fixing `c = 1` costs the local sector nothing at all and the transport layer
everything it has. **The open question this leaves is not about `c` but about the transport layer**: the six claims
that need `c` to be one are exactly the claims G_079's continuity law added, so **whether the continuity law is part of
the theory or of the coordinate choice** is the next thing to decide.
