# ResearchY-G_098 — Directed Transport Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_098_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/DirectedTransportAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_098`) · **Verdict:** **DIRECTED TRANSPORT IS ASSUMED — THE SURVIVING STRUCTURE DOES NOT EVEN REPRESENT A DIRECTION**

---

## Question

G_096 left the kernel's coefficient assembled from an assumed principle; G_097 measured that the evolution **diffuses**
(the width grows as **√t**), that the **centroid shift is exactly zero**, and that **48!** histories are observationally
identical.

> **Does the surviving AT structure contain a derived carrier for directed propagation, or is transport independent
> information?**

**Constraints:** every count, rank, dimension and multiplicity on the **96-cell ring alone**; the **cubic substrate
forbidden**; no new primitive, field, source term, topology axiom, action, Lagrangian, memory channel, carrier or
vector structure introduced. **A negative result is acceptable.** **Prefer refutation.**

---

## Answer

**DIRECTED TRANSPORT IS ASSUMED — AND THE ANSWER IS STRONGER THAN A NEGATIVE RESULT ABOUT SELECTION.** The surviving
structure does not merely fail to **select** a direction: it does not **represent** one.

| Required output | Measured |
|---|---|
| **Carrier inventory** | **7** candidates · **5** derived · **3** directed — and every directed one is a coordinate, a derived output read by nothing, or a static field |
| **Transport/diffusion table** | T = **0.000** (surviving operator) · **0.000** (symmetric transfer) · **0.368** (orientation supplied) |
| **Max T** | **0.000E+000** over the surviving modes; **0.368** over all modes |
| **Directional bias** | **0** of **4** instruments selects a direction |
| **Observable loss count** | **0** with diffusion alone; **4** without the labelling (a coordinate loss) |
| **Classification** | EMERGENT (state, excess, conserved quantity) · **ASSUMED** (label) · BOUNDARY (flux, acceleration, phase) |

---

## 1. The carrier inventory (tasks 1 and 2)

| carrier | state-like | derived | reads | excess conserved | centroid shift | directed | width growth |
|---|---|---|---|---|---|---|---|
| the state (the occupation) | yes | yes | **8** | yes | **0.000E+000** | no | 2.000 |
| the excess over the flat background | yes | yes | **8** | yes | **0.000E+000** | no | 2.000 |
| a conserved quantity (the total count) | yes | yes | 8 | yes | 0.000E+000 | no | 2.000 |
| the phase (the theta sector) | **no** | no | **0** | no | 0.000E+000 | no | 0.000 |
| the label (the cell index) | yes | **no** | 4 | yes | **8.000E-001** | **yes** | 2.173 |
| the flux (the transport's own variable) | no | yes | **0** | no | 0.000E+000 | **yes** | 0.000 |
| the acceleration (the potential's gradient) | no | yes | 2 | no | 0.000E+000 | **yes** | 0.000 |

**The three directed entries are the audit's whole problem, and each fails differently:** the **label** is the only
carrier that can show a non-zero centroid shift — because the centroid is a function of the **labelling**, so what
moves is the label; the **flux** is directed (a flow has a sign) but written by an operation and **read by 0
observables**; the **acceleration** is directed and derived and **static** given the state, with no update relation
taking it as an argument. **None of them propagates.** The **phase** is not a carrier at all — excluded by **arity**.

---

## 2. The transport/diffusion table (task 4)

| mode | centroid shift | width growth | **T** | orientation supplied |
|---|---|---|---|---|
| the surviving operator alone | **0.000E+000** | 2.000 | **0.000** | no |
| a **symmetric** transfer (count moved both ways) | 0.000E+000 | 2.000 | **0.000** | no |
| a **drift supplied from outside** | 8.000E-001 | 2.173 | **0.368** | **yes** |

**MAX T over the surviving modes is 0.000E+000; over all modes it is 0.368** — and the difference between them is
exactly the **orientation**.

**A ratio of zero is a structural statement, not a numerical accident:** the numerator never leaves zero — measured at
every one of the 8 ticks — while the width grows as **√t**. The symmetric transfer is included deliberately, so that
the class's operations are **not misrepresented as biased**: an operation is individually directed, and collectively
they cancel in the first moment.

---

## 3. The directional bias (task 5)

**Four independent instruments**, so the conclusion does not rest on one:

| instrument | measured | selects a direction |
|---|---|---|
| the operator's matrix asymmetry | **0** | no |
| the symbol's evenness under k → −k | **2.665E-015** | no |
| the degree's uniformity | **2** (one distinct value) | no |
| **the orientation the link set STORES** | **0** | no |

**The last is the sharpest, because it is a property of the representation rather than of a simulation:** `LinksOf`
writes every link as an **ORDERED PAIR** with `a ≤ b`, so the orientation is **discarded when the link set is written
down**. The operator built from an unordered set is symmetric; the dispersion is even, so no mode rotates; the substrate
is isotropic about every site. **The direction is not merely unselected — it is not represented.**

**The missing information, quantified:** the ring has **96 links**; a **global chirality** costs **1 bit**, and an
independent orientation of every link costs **96 bits**. **The sector supplies 0.**

---

## 4. Without labels (task 6) and the reduction test (task 7)

| quantity | with labels | without labels | survives |
|---|---|---|---|
| the centroid shift of the surviving operator | 0.000000 | — | **no — UNDEFINED** |
| the peak | 1.250000 | 1.250000 | **yes** |
| the width of the excess | 2.000000 | 2.000000 | **yes** |
| the centroid shift of a chosen drift | **0.800000** | **0.000000** | **no** |

**The transport measure is itself a function of the labelling** — a centroid needs an index — so of **4** propagation
quantities **2** survive the removal of the labels and **0** of them is a **transport** quantity: *the measure of
transport does not survive its own coordinate.* The audit reports the undefined cases as **undefined** rather than as
zero.

**The reduction: 8 readouts, 0 of them reads a directed object** — the sector's one directed object, the flux, is read
by **0** observables — so the **observable loss with diffusion alone is 0**. The only loss is **4** when the labelling
is removed, which is a **coordinate** loss and not a transport loss. **Directed transport costs no observable.**

---

## Defects found while building this audit

1. **A degree row that reported a count of counts.** The uniformity instrument printed `degrees.Distinct().Single()`
   as its *measured* value and passed `distinctDegrees` — so the verdict read "every cell has **1** links" where the
   degree is **2** and the *number of distinct degree values* is **1**. The tuple now carries the **degree** and the
   uniformity separately, and the two are no longer confusable.
2. **A carrier row that attributed an external drift's shift to a derived carrier.** The label's centroid shift is the
   **supplied** drift's, not the label's; the row now says so in its own measurement, because a reader comparing columns
   would otherwise read the sector as producing transport.
3. **A "measured" value that was an instrument's own scale.** The symbol's evenness is reported as **2.665E-015** — a
   `cos` round-trip — and the audit states that it is the rounding scale rather than exact zero, in keeping with the
   project's rule that a mathematically zero quantity is compared against its rounding rather than against equality.
4. **Two structural declarations in a positional record.** `CarrierRow` carried `Adjacency`/`Symmetry` style flags; the
   record was reduced to the two facts actually measured and the derived quantities computed, so no column is a
   declaration the audit chose.

---

## What this does and does not establish

**Establishes:** that the surviving operator is **symmetric four independent ways**; that the direction is **not
represented**, since every link is stored as an ordered pair; that directed transport needs **one bit** of chirality and
the sector supplies **none**; that the transport/diffusion ratio is **zero** for every surviving mode and **0.368** once
an orientation is supplied; that without labels the transport measure **disappears**; and that **none of this costs an
observable**.

**Does not establish** that transport is **impossible**, or that the sector's operations are not directed — an
operation **is** directed (a transfer goes one way) and G_097 measured that it moves a pattern. What this audit
establishes is that **no surviving structure supplies the direction**: the operation's direction is a **choice**, and
the choice is exactly the one bit the representation does not carry. Nor does it establish that the ring is the wrong
substrate — a directed substrate would be a **different theory**, which this audit's rules forbid it to introduce.

**Why this is not a restatement of G_097:** G_097 measured that the **law** diffuses and that the **operations** move a
pattern, and left open whether anything **derives** the direction. This audit asks it **structurally** rather than
dynamically: it measures whether the link set represents an orientation at all, quantifies the missing information in
**bits**, and reports the **transport/diffusion ratio** — a quantity G_097 did not compute.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, densities = `MapRedundancyAudit.State`, links =
`LinksOf(96, 1)`. The **cubic substrate is forbidden and is not named in the suite**. **Equality rule:** `1e-12` on a
computed value; the symbol's evenness is reported as a **rounding scale** rather than as equality; the evolution step
`ε = 0.25` is the same stated convention G_097 used, so the two audits are **comparable** and their shared pattern and
step are cross-checked by identity. **No spectral quantity is quoted.**

**Guards:** `Y_G_033` — `without` **66 → 67**; `Y_G_035` — **98 → 99**, survives **84 → 85**. Registry entry:
**derived**.

**What would change the verdict:** an **orientation carried by the link set** (which would make the operator
asymmetric and give a non-zero transport ratio); a surviving **rule that fixes which transfer happens** (which would
derive the drift); or an **observable that reads the flux**, which would make the direction measurable and give the
missing information a cost.
