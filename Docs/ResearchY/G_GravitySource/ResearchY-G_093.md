# ResearchY-G_093 — Dynamic Core Recovery Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_093_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/DynamicCoreRecoveryAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_093`) · **Verdict:** **NOTHING NECESSARY WAS REMOVED, AND NOTHING ABSENT WAS CLOSED**

---

## Question

G_084–G_091 reduced the observable sector to `ρ → K(ρ)`; G_092 asked whether the reduction left a hidden **actuator**
and found none.

> **Did observable minimization eliminate structures necessary for evolution, control, or first-principles
> completion?**

**Allowed:** all pre-reduction structures — connectivity, continuity, transport, memory channels, mode locking, the
theta sector, the vector sector, the unified action.
**Forbidden:** observable-reduction arguments; "not measured therefore irrelevant"; **assuming the minimal core is
complete**.
**Prefer refutation.** Success would be a structure that is **dynamically essential and observably redundant**;
failure would be that the observable, dynamic and closure cores **coincide**.

---

## Answer

**NOTHING NECESSARY WAS REMOVED — AND NOTHING ABSENT WAS CLOSED.**

| Task | Result |
|---|---|
| 1 Two theories | **A** = the observable-minimal core, **B** = the full pre-reduction theory, both from the chain's own factories |
| 2 Four axes | **no axis differs**: readouts (worst difference **0.000E+000**), evolution (**exactly 0.000E+000**), control (**95** operations), closure (**7** inputs) |
| 3 Removal, one at a time | the four losses measured per structure; removal costs closure in exactly **2** places, both **definitional** |
| 4 The matrix | over **19** structures: observable **7**, dynamic **5**, actuator **1**, **closure 10** |
| 5 The search | **3** structures are dynamically essential *and* observably redundant — the **success criterion is MET** |
| 6 Closure gaps | of the four named paths, **3** are required for closure and **1** is closed |
| 7 The three cores | ObservableCore **7** · DynamicCore **5** · ClosureCore **10** |
| 8 Dimensions | three different numbers, surpluses **1 · 1 · 3** — the **failure criterion is REFUTED** |

---

## 1. The two theories and the four axes (tasks 1 and 2)

| | A, the observable-minimal core | B, the full pre-reduction theory |
|---|---|---|
| contents | state, labelling, potential map, clock law, gradient definition | the same, plus the flux, its displacement, the conservation statement, the transport operator, the ledger, the component count |
| readouts produced | **8** of 8 | **8** of 8 |
| evolution residual | **0.000E+000** against B | the reference |
| reachable operations | **95** | **95** |
| inputs the theory must be given | **7** | **7** |

Each axis is decided by a **measured difference**, not by a reading: the observables by the reduction's own
reproduction test, the evolution by the update relation's residual (the update takes the state and the links, and the
reduction leaves both untouched), control by the operation count (the class belongs to the substrate, not to the
description), and closure by the input ledger.

**What the reduction removed** — the flux, the displaced flux, the conservation statement, the transport operator and
the ledger — **is implied by the operations themselves**: the layer's identity holds **exactly** (residual **0**), so
dropping the statement does not drop the constraint.

---

## 2. The matrix (tasks 3 and 4)

Four measured columns over **19** structures. The first three are **read from G_092's measured rows** rather than
restated, so the two audits cannot drift apart; the fourth is this audit's closure measurement, and every true row
carries its **witness**.

| structure | observable | dynamic | actuator | **closure** | loss | present | owner |
|---|---|---|---|---|---|---|---|
| the state (the occupation) | **yes** | **yes** | **yes** | no | 0 | yes | G_084 |
| the link set (connectivity) | **yes** | **yes** | no | **yes** | 0 | yes | G_080 |
| the component count | no | **yes** | no | **yes** | 0 | yes | G_082 |
| density topology | no | **yes** | no | **yes** | 0 | yes | G_089 |
| the transport operator | no | **yes** | no | no | **1** | yes | G_084 |
| the flux | no | no | no | no | 0 | yes | G_083 |
| the continuity law | no | no | no | no | **1** | yes | G_083 |
| the ledger | no | no | no | no | 0 | yes | G_084 |
| the potential map | **yes** | no | no | **yes** | 0 | yes | G_075 |
| the clock law | **yes** | no | no | **yes** | 0 | yes | G_085 |
| the gradient definition | **yes** | no | no | no | 0 | yes | G_084 |
| the kernel's second component (the curvature) | **yes** | no | no | **yes** | 0 | yes | G_087 |
| the phase structure (the theta sector) | **yes** | no | no | **yes** | 0 | yes | G_002 |
| mode locking | no | no | no | no | 0 | yes | G_090 |
| the distribution's functionals | no | no | no | no | 0 | yes | G_090 |
| the actualization rate | no | no | no | no | 0 | yes | G_090 |
| memory channels | no | no | no | **yes** | 0 | **no** | G_091 |
| the vector sector | no | no | no | **yes** | 0 | **no** | E_004 |
| the unified action | no | no | no | **yes** | 0 | **no** | E_004 with M_012 |

**Totals: observable 7 · dynamic 5 · actuator 1 · closure 10.**

**The closure column is measured, not declared.** A structure is closure-necessary when the surviving vocabulary does
**not** determine it — no readout pins it, no equation produces it, no operation sets it:

- **connectivity** — *not determined*: **4 of 4** primitive certificates are unchanged by a cut (G_080), and a cut
  **silences** the acceleration read (**0** of 2 cells read one on the split pair);
- **the component count** — *not determined*: a conserved integer the theory never sets, read by **0** local
  observables and changed by **0** operations;
- **the potential map** — *not determined*: the exponential metric and the rate's power are the same statement, and the
  map is taken as an **input** (G_075, G_076);
- **the clock law** — *not determined*: its shape is a **free parameter** (**5** free-room members satisfy the pinned
  data below **1E-09** while the target prediction moves);
- **the curvature** — *not determined*: signature **1.166E-002** against a separation of **1.256E-001**, a factor
  **10.8**; a parameter of the law with **no actuator**;
- **the theta sector** — *not determined by the density*: the interference term moves by **1.283E-002** between two
  phase assignments while the density is untouched;
- **memory channels** — **absent** *and* not determined: the coefficient they would carry **is** the kernel's free
  parameter, and G_091 named them as the only candidate that could carry it;
- **the vector sector** — **absent**: the time sector's substrate cannot supply it (largest irrep dimension **2**
  against a needed **3**; only the cubic substrate carries T1(3));
- **the unified action** — **absent**: the Lorentz vector's **4** parts are supplied by **2 different substrates**, and
  the two-state form needs the curl instead of the identity.

And the rows that are **not** closure-necessary are of two kinds: the theory's own **definitions** (the state, the
operation's rule, the gradient definition) and what it **derives** (the flux, the continuity law, the ledger, mode
locking and the distribution's functionals).

**Two definitional removals cost closure 1 each** — the operation's rule and the conservation statement — because
removing what *defines* an object turns that object into an input. The reduction did **not** perform either in that
sense: it removed their **bookkeeping**, and the class's operations survive it (**95** of them).

---

## 3. The search (task 5): dynamically essential, observably redundant

**3** structures qualify: the **component count**, **density topology** and the operation's **own rule**. The set is
**the same one G_092 found**, so the two searches agree.

**The success criterion is therefore MET** — and what it does **not** supply is a channel: **none** of the three is
actuatable, and G_092 measured that their reach effect is **restrictive** (`n − c` can only fall: **95/95**, **94/95**,
**92/95**).

---

## 4. The first-principles gap audit (task 6)

One criterion, applied to all four named paths — *required for closure?*

| path | required? | witness |
|---|---|---|
| the memory-channel path | **REQUIRED** | it is the only candidate that could carry the kernel's free coefficient, whose coupling to every state-based process is exactly **0.0** — and it is **absent** |
| the mode-locking path | **NOT required** | G_090 measured it a **function of the distribution**: a locked mode **is** the occupancy, renamed |
| the theta chain | **REQUIRED** | the phase grid is **not determined by the density** — the interference term moves (**1.283E-002**) while the density is fixed |
| the unified action | **REQUIRED** | **absent**: the Lorentz vector's parts come from **2 different substrates** and the two-state form needs the curl |

**3 of 4 paths are open, and 1 is closed** — and the closed one is closed by a *measurement* rather than by a choice.

---

## 5. The three cores (tasks 7 and 8)

| core | dimension | with dynamic | with closure | surplus |
|---|---|---|---|---|
| **ObservableCore** | **7** | 2 | 5 | **1** |
| **DynamicCore** | **5** | 5 | 3 | **1** |
| **ClosureCore** | **10** | 3 | 10 | **3** |

Three **different** dimensions, and each core holds members the other two do not — so **no two coincide** and the
**failure criterion is refuted**, by containment as well as by size.

**And the closure surplus is absent.** Exactly **3** structures are required for completion while being neither
observable, nor dynamical, nor actuatable — **none of which the theory has**:

1. **memory channels** — the carrier the kernel's free coefficient would need;
2. **the vector sector** — which lives on the cubic substrate M_012, not on the time sector's ring;
3. **the unified action** — the join that would generate both sectors from one object.

---

## Defects found while building this audit

1. **The core-surplus formula reported the whole core.** `Dimensions()` first computed a core's surplus as
   `core \ (union \ core)`, which returns the entire core (**7**) instead of the members it holds **alone**. The
   comparison would have reported surpluses **7 · 5 · 10** — three numbers that say nothing, and that would have made
   the "no two coincide" claim rest on the dimensions alone. It is now derived from the three sets directly
   (**1 · 1 · 3**), so containment is tested as well as size.
2. **A ratio read in the wrong direction.** The closure witness for the curvature asserted the audit's own
   `AgainstTheSeparation().Ratio < 1`, but that ratio is **separation ÷ signature**, so the undecidable case is
   **> 1** (measured: **10.8**). The assertion was inverted, and the witness now says which way the factor runs.
3. **Three structures whose absence the question could have hidden as inertness.** The prompt's own rule forbids
   treating "not measured" as "irrelevant", so `PresentInTheTheory` is a **third** column kept apart from the three
   necessities: a structure can be required for closure, unread and unevolved, and **not exist** at all — which is the
   case for the entire closure surplus.
4. **A first draft that made removal the definition of closure.** Defining closure necessity as "removing it adds an
   input" would have made the closure column empty (the reduction adds none) and lost the distinction between a
   structure that **is** an input and one that **defines** an object. Both are now measured: `ClosureNecessary` and
   `ClosureLossOnRemoval`, and they disagree in exactly the two definitional rows.

---

## What this does and does not establish

**Establishes:** that the reduction moved **none** of the four measured axes; that the three cores are three
**different** sets (**7 · 5 · 10**, surpluses **1 · 1 · 3**), so no two coincide; that the success criterion **is** met,
by the three structures that are observably redundant and dynamically essential; and that the **closure surplus** —
the structures required for completion that are neither observable nor dynamical — is exactly **three**, **none** of
which the theory has.

**Does not establish:** that the three absent structures would **close** the theory if introduced. The audit measures
that they are **required and absent**, not that they are **sufficient**; and two of them (the vector sector and the
unified action) live on the cubic substrate, so introducing them **changes the substrate**, which is another audit's
question. Nor is the closure column claimed **complete**: it is measured against the inputs the surviving audits name,
and a new input would have to be found by finding a new **measurement**, not by adding a row.

**Why this is not a restatement of G_092:** G_092 measured observable, dynamical and actuator necessity and stopped at
the control boundary. This audit adds the **closure** axis, constructs A and B explicitly and compares them on **four**
axes rather than one, searches the **closure** surplus rather than the **control** surplus — a different set, 3 to 3,
sharing only one name (the operation's rule) — and finds its members **absent** rather than inert.

---

## Where it stands

**Substrate:** the canonical **96-cell ring** (`CanonicalCells`) for the time sector; the **cubic** substrate only
where the vector sector is named, by E_004's own measurement. **Equality rule:** `1e-12` on a readout value. **No
spectral quantity is quoted** — no A₀, no free room, no lock release, no survivor count — so the spectral-substrate
rule is not engaged.

**Guards:** `Y_G_033` (the substrate scan) — `without` **61 → 62**; `Y_G_035` (the registry) — **93 → 94**, survives
**79 → 80**. Registry entry: **derived**.

**What would change the verdict:** a derivation of the substrate's connectivity, of the map, of the clock law's shape
or of the phase grid from the surviving vocabulary (which would empty closure rows and shrink the closure core); a
measurement that decides the kernel's second component, which would remove the one input whose only candidate carrier
is absent; or a demonstration that the vector sector follows from the ring, which E_004 refutes by the irrep
dimension.
