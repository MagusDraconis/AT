# ResearchY-G_080 — Connectivity Origin Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_080_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ConnectivityOriginAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_080`) · **Verdict:** **ASSUMED** (registry: Boundary)

---

## Question

G_079 derived the continuity law `∂tρ + ∇·J = 0` for the surviving update class and proved that its equivalence with
count conservation rests on one premise the law cannot supply for itself: the **connectivity** of the substrate. On a
connected ring the zero-sum subspace **is** the image of the divergence; on a substrate of `c` components a transfer
across a cut conserves the total, is not any flux, and is a source/sink pair.

**Is that connectivity derived, emergent, or assumed?**

**Allowed as evidence:** Difference, Actualization, the generation relation, count conservation.
**Forbidden as evidence:** a graph-connectivity axiom, an imported topology.

1. Can disconnected universes exist?
2. Is connectivity generated automatically by the update relation?
3. Smallest disconnected survivor?
4. Observable signatures of disconnected sectors?
5. Classification: derived, emergent, assumed.

## Answer

**ASSUMED — CONNECTIVITY IS AN INPUT AT THE LEVEL OF THE PRIMITIVES, AND BOTH ALTERNATIVES ARE REFUTED BY
MEASUREMENT.**

- **NOT DERIVED: the four allowed primitives are blind to the cut.** Adjoin a **disjoint copy** of a substrate to
  itself — the universe simply gains a second component — and **not one** primitive changes the content of its
  certificate: the same **set** of realised differences (`{−5,−3,1,2,3,4}`), the same local ticks (`{−6,−3,0,5,6,7}`),
  the same realised in-degrees (`{0,1}`), the same component sums (`{47}` with leak 0). **0 of 4** detect the union,
  while **every** global descriptor does: components **1 → 2**, transport directions **7 → 14**, generation roots
  **1 → 2**. The reason is structural and it is the audit's sharpest sentence: **three of the four primitives are
  local — radius one link — and the fourth is a sum, and a sum cannot see which component its terms came from.**
- **NOT EMERGENT: the update class conserves the component count instead of producing it.** The class is written on
  the link set, so its Laplacian is **block diagonal** across a cut: the weight carried between different components
  is exactly **0.000E+000** against a **live control** of **4** entries joining the two halves of the connected ring —
  a partition no link set can influence — so a zero means something. The pair that is a **link** on the 8-cell ring and
  a **cut** on two 4-cell rings has influence **1 against 0**.
- **The generation relation does not repair it.** One root can generate **both** components: **1** generation
  component against **2** transport components. The two relations are different objects and the law needs the second.
- **The generative rule assumes the ring rather than building it.** The actualization link rule is `(i + d) mod n` —
  arithmetic that never tests a link because it computes one. Applied to a genuinely disconnected substrate it does
  not fail: it **silently reconnects it**, manufacturing **1 link across the cut** on the smallest split pair and
  returning **1 component** where the substrate has 2 (**9** on two 4-cell rings, **13** on the canonical split).
  **Connectivity is in the rule's arithmetic, not in its output.**
- **The smallest disconnected survivor is two cells, and it is frozen.** One cell per component: transport rank
  **0**, **no non-zero transport at all**, and the single non-trivial count-conserving change it does admit is the
  **forbidden** transfer (1 of 1 witness). The same two cells **connected** admit **1** direction, so the zero is
  measured. Transport capacity is `n − c` throughout — **every cut costs exactly one direction** — and no single defect
  can cut the canonical substrate: exhaustive search gives **2 links or 2 cells** as the smallest disconnection.
- **The signature of a disconnected sector is a frozen, unreadable offset.** Its per-component-uniform configuration
  is an **exact fixed point** (max update **0.000E+000**) with a potential gap of **0.270**; on the smallest survivor
  there is **no acceleration observable at all** (a gradient along links, and it has none); and the **redshift between
  components is defined and moves** with the free level (**−0.370 to +0.310**) while being **unreachable** —
  **0 links across the cut**, infinite path length — and **0 of 6** temporal observables read a flux.
- **No axiom was imported.** Every substrate is **built** from an explicit link set, its component count is
  **computed** from that set by union-find, and the blindness of the primitives is then **measured**.

---

## 1. What connectivity is worth: the transport capacity

The rank of the divergence operator is the number of independent transport directions a substrate offers, and it is
`n − c` exactly — **each cut costs exactly one direction**:

| substrate | cells | components | directions | lost to the cuts |
|---|---|---|---|---|
| the smallest ring | 2 | 1 | **1** | 0 |
| the smallest split pair | 2 | 2 | **0** | **1** |
| a 4-cell ring | 4 | 1 | 3 | 0 |
| two 2-cell rings | 4 | 2 | 2 | **1** |
| four single cells | 4 | 4 | **0** | **3** |
| the 8-cell ring | 8 | 1 | 7 | 0 |
| two 4-cell rings | 8 | 2 | 6 | **1** |
| four 2-cell rings | 8 | 4 | 4 | **3** |
| the canonical ring | 96 | 1 | 95 | 0 |
| the canonical ring split in two | 96 | 2 | 94 | **1** |

## 2. Q1 — Can disconnected universes exist?

**Yes, and the smallest one is frozen.** The per-component-uniform occupation (level 4 in the first component, 9 in
the second, …) is an **exact fixed point** of the transport class, conserves the count, and carries a non-zero
potential **gap** — so it is not a trivial state:

| substrate | cells | components | max update | witnesses | transports | forbidden | potential gap (4 vs 9) |
|---|---|---|---|---|---|---|---|
| the smallest split pair | 2 | 2 | 0.000E+000 | 1 | **0** | **1** | 0.270 |
| two 4-cell rings | 8 | 2 | 0.000E+000 | 9 | 8 | **1** | 0.270 |
| four 2-cell rings | 8 | 4 | 0.000E+000 | 11 | 8 | **3** | 0.270 |
| four single cells | 4 | 4 | 0.000E+000 | 3 | **0** | **3** | 0.270 |
| the canonical ring split in two | 96 | 2 | 0.000E+000 | 97 | 96 | **1** | 0.270 |

The exact count of forbidden transfers is `c − 1` — one transfer across each neighbouring pair of components — and no
flux of the class can carry any of them.

## 3. Q1 continued — the four allowed primitives are blind to the cut

The **disjoint-union test**: take a substrate and adjoin a disjoint copy of it. Nothing changes except that the
universe has two components.

| primitive | content on the substrate | content on the disjoint union | changed |
|---|---|---|---|
| Difference | `{−5,−3,1,2,3,4}` | `{−5,−3,1,2,3,4}` | **False** |
| Actualization | `{−6,−3,0,5,6,7}` | `{−6,−3,0,5,6,7}` | **False** |
| Generation relation | `{0,1}` | `{0,1}` | **False** |
| Count conservation | `{47}` with leak 0 | `{47}` with leak 0 | **False** |

| primitive | reach |
|---|---|
| Difference | **local**: one link |
| Actualization | **local**: one link |
| Generation relation | **local**: one generator |
| Count conservation | **global, but only a sum** |

…while every global descriptor moves — so the audit is not blind, the **allowed vocabulary** is:

| descriptor | substrate | union | note |
|---|---|---|---|
| connected components | **1** | **2** | the union **is** the cut |
| transport directions (rank of the divergence) | **7** | **14** | the audit's **own** detector |
| generation roots (cells of in-degree 0) | **1** | **2** | one root per component |
| links in the link set | 8 | 16 | the union removes no link |

The sweep is not one instance: over 4 states × 2 decompositions (**8** instances) **0** primitive content changes, and
the rank identity `n − c` is verified for every substrate in the family.

## 4. Q2 — Is connectivity generated by the update relation?

**No: it is conserved.** The influence of one cell on another's update is the Laplacian entry, and the pair that is a
**link** on one substrate and a **cut** on the other separates them exactly:

| substrate | cells | components | source | target | influence |
|---|---|---|---|---|---|
| the 8-cell ring (the pair 3–4 is a link) | 8 | 1 | 3 | 4 | **1** |
| two 4-cell rings (the pair 3–4 is a cut) | 8 | 2 | 3 | 4 | **0** |
| the canonical ring (the pair 47–48 is a link) | 96 | 1 | 47 | 48 | **1** |
| the canonical ring split in two (the pair 47–48 is a cut) | 96 | 2 | 47 | 48 | **0** |

| substrate | entries joining different components | weight out of component 0 | entries joining the two halves |
|---|---|---|---|
| two 4-cell rings | **0** | 0.000E+000 | **0** |
| four 2-cell rings | **0** | 0.000E+000 | **0** |
| the canonical ring | 0 (one component) | 0.000E+000 | **4** |
| the canonical ring split in two | **0** | 0.000E+000 | **0** |

The last column is the **live control**: the same index set split **in half** — a partition no link set can influence —
is joined by **4** Laplacian entries on the connected ring and by **0** on the cut. A conserved integer that no
dynamics sets is an **initial datum**, which is what the count itself is; the component count now joins it.

**Generation is not transport.** One root can generate both components:

| case | cells | generation components | transport components | note |
|---|---|---|---|---|
| one root generating both components | 8 | **1** | **2** | the generation graph is one tree while the substrate has two components |
| one root per component | 8 | 2 | 2 | generation and transport agree |
| generation along the links | 8 | **3** | 2 | a subgraph of the links can only split further, never rejoin |

**And the generative rule assumes the ring.** Applied to substrates that *are* disconnected:

| substrate | cells | components | active cells | links created | links crossing the cut | components of the output |
|---|---|---|---|---|---|---|
| the smallest ring | 2 | 1 | 1 | 1 | **0** | 1 |
| the smallest split pair | 2 | **2** | 1 | 1 | **1** | **1** |
| four single cells | 4 | **4** | 2 | 5 | **5** | **1** |
| two 4-cell rings | 8 | **2** | 3 | 15 | **9** | **1** |
| four 2-cell rings | 8 | **4** | 3 | 15 | **13** | **1** |

On the smallest split pair the rule manufactures the **very link** whose absence made the pair disconnected, and
returns **1** component. The rule cannot represent the substrate it is supposed to build on.

## 5. Q3 — The smallest disconnected survivor, and the smallest cut

Both ends of the question, by exhaustive search (size one verified not to suffice):

| cells | fewest links to disconnect | fewest cells to disconnect |
|---|---|---|
| 2 | **1** | 0 (no removal of ≤ 2 cells disconnects it) |
| 3 | **2** | 0 |
| 4 | **2** | **2** |
| 8 | **2** | **2** |
| 96 | **2** | **2** |

So the smallest survivor is **two cells** — rank **0**, **no** non-zero transport, one forbidden transfer, **frozen** —
and the canonical substrate needs **two** links or **two** cells removed before it disconnects: **a single defect can
never cut it.**

## 6. Q4 — The observable signature of a disconnected sector

| substrate | cells | components | max \|acceleration\| over the sweep | cells reading one |
|---|---|---|---|---|
| the smallest split pair | 2 | 2 | **0.000E+000** | **0** |
| the smallest ring | 2 | 1 | **> 0** | **2** |
| two 4-cell rings | 8 | 2 | > 0 | > 0 |
| the canonical ring | 96 | 1 | > 0 | > 0 |

The acceleration is a gradient along the links a cell has, so a cut **silences** it: on the smallest survivor the
observable does not exist at all, while the same two cells connected read one. And the free level is **written into**
an observable that no experiment can reach:

| level 1 | level 2 | redshift between the components |
|---|---|---|
| 4 | 4 | 0.000 |
| 4 | 9 | **−0.237** |
| 4 | 16 | **−0.370** |
| 9 | 4 | **+0.310** |

Links across the cut: **0**. Path length from cell 3 to cell 4: **1** on the 8-cell ring, **none** on two 4-cell rings.
Temporal observables that read a flux: **0 of 6**. The sector's signature is therefore a **frozen, unreadable
offset** — not invisibility, **unreachability**.

## 7. The verdict

Computed with live branches (G_027): the audit would read **DERIVED** if a primitive's certificate changed with the
cut, and **EMERGENT** if an operator moved count across one. Neither branch is taken.

> **ASSUMED — CONNECTIVITY IS AN INPUT AT THE LEVEL OF THE PRIMITIVES, AND BOTH ALTERNATIVES ARE REFUTED BY
> MEASUREMENT.** Connectivity is a **global** property of the substrate and the allowed vocabulary is local or
> relational, so no combination of the four primitives can state it. Like the count itself, the **component count is
> a conserved integer the theory conserves and never sets**, and it must be supplied as an **initial datum**.

---

## Defects found while building this audit

1. **A control that was zero by construction — the same defect class twice.** The block-diagonal test first compared
   the entries joining *different components* of the cut substrate with the same count on a connected ring; but with
   one component there is nothing to join, so the "control" was **0 by construction** and the measurement was vacuous.
   The first replacement was no better: it computed the control as `EntriesJoiningDifferentComponents(cells, 1)`, which
   is identically zero for exactly the same reason. Both were caught before the suite ran, by asking what the control
   would read if the audit were wrong. The control is now a partition **no link set can influence** — the same index
   set split **in half**, joined by **4** entries on the connected ring and **0** on the cut.
2. **Component counting on a subgraph.** The smallest-cut search first counted components over **all** cells, so every
   removed cell became its own component and the search reported a disconnection it had just created — a "minimum
   vertex cut" of 1 for every ring. Fixed by an induced-subgraph component count that does not count removed cells;
   found by re-reading the search before running it, not by the tests.
3. **A nonsense assertion reached the test file** (`LinksAcrossTheCut(...) − (... * 0)`), which would have passed
   vacuously; replaced by the real cross-check that the Laplacian's off-diagonal entries are exactly twice the link
   count for every substrate in the family.
4. **A helper left behind by an edit** (`LinkCountCells`) survived a round of edits once its only caller was
   rewritten; found by reading the file after the tests were already green, not by the compiler.
5. **A rule applied rather than a defect found, recorded for the next audit.** G_079's third defect was a `Single`
   lookup on a substrate name that was not unique. This audit's labels were made pairwise non-prefix **before** the
   lookups were written and are matched with exact equality — the guard is now a habit rather than a discovery.

## What this does and does not establish

- **Does:** show that the four allowed primitives cannot separate a connected substrate from a disconnected one
  (**0 of 4**), while the divergence's rank can (`n − c`); that the transport class **conserves** the component count
  rather than producing it; that the generative rule **assumes** the ring; that the smallest disconnected survivor is
  **two cells and frozen**; that a single defect can never cut the canonical substrate; and that a disconnected
  sector's signature is a **frozen offset that is defined but unreachable**.
- **Does not:** claim that no conceivable primitive of a larger theory could yield connectivity — only that none of
  the four **allowed here** does; claim **total** blindness (a state that varies at the cut is locally visible at the
  two cells beside it, and what is unreachable is the **global** component count); derive the generation relation from
  the transport relation (they are **different** relations, measured); or remove the premise from G_079's law — the
  law is still conditional on it, and it is now known to be an **input**.

## Where it stands

**Connectivity is an input, and the audit says which kind of input.** G_079 handed on the question; G_080 answers it
by measurement rather than by definition: the substrates are built, their component counts are computed, and only
then is each allowed primitive asked whether it can see the difference. It cannot — while the audit's own detector,
the rank of the divergence, sees it exactly. A disconnected universe is therefore **not forbidden**: it is a frozen,
unreadable offset, and the theory's boundary list gains one integer. **The open question handed on is what remains:
the theory conserves the component count and never sets it — is `c = 1` fixed by any principle, or only by the
canonical single-ring initial condition?**
