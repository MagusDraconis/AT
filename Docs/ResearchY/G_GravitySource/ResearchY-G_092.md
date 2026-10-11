# ResearchY-G_092 — Actuator versus Observable Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_092_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ActuatorVersusObservableAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_092`) · **Verdict:** **NO ACTUATABLE STRUCTURE IS UNOBSERVABLE** (registry: Derived)

---

## Question

G_084–G_091 reduced the observable sector, each time on the ground that **the surviving observables did not move**:
connectivity, the component count, the transport layer, the continuity law, the flux, the second kernel component and
every candidate pump were stripped or demoted.

> **Did any eliminated structure remain necessary for dynamics or for control?**

**Allowed:** all structures eliminated during G_080–G_091 — connectivity, transport, continuity, the component count,
memory channels, mode locking, the theta sector.
**Forbidden:** observable-reduction arguments (the reduction kept the readouts is **not** evidence about dynamics);
"not observed therefore irrelevant" (relevance is **not** read off observability).
**Prefer refutation.** Success would be a structure removed by the reduction and still required for control while
invisible; failure would be that the observable and actuator cores coincide.

---

## Answer

**FAILURE OF THE SUCCESS CRITERION — AND THE CORES DO *NOT* COINCIDE, IN EITHER DIRECTION.**

| Task | Result |
|---|---|
| 1 Roles separated | three **procedures** on three objects — **7** structures are read and not evolved, **3** evolved and not read |
| 2 Four-way classification | of **17** structures: observable **7** · dynamical **5** · actuatable **1** · **invisible actuators 0** |
| 3 Chain on the full theory | **1** actuatable channel against **1** — the pre-reduction structures add **0** |
| 4 The two cores | observables **7**, actuation **3**, intersection **2**, actuation-only **1**, observables-only **5** |
| 5 Dynamics without observables | **3** structures (**c**, topology, the rule) — and **none** is actuatable; the reach can only **fall** |
| 6 Table | three columns, computed: necessity is never inferred from irrelevance |
| 7 Criteria | success **NOT MET**; the coincidence **NOT MET IN THE LETTER**; the chain's gain **0** |

**CONTROL NEEDS THREE AND CAN SET ONE.** The structures whose removal kills the operations number **3** — the state,
the link set and the operation's **own rule** — and the **1** an operator can set is the **state**: the object the
readouts already read.

---

## 1. Observable role versus dynamical role (task 1)

The two roles are measured on **different objects**, which is what makes them separable:

| role | procedure | what it measures |
|---|---|---|
| **OBSERVABLE** | remove or move the structure in the description, re-read the 8 readouts | readouts **lost** and readouts **moved** — the readouts, and nothing else |
| **DYNAMICAL** | remove or change it in the update relation, in the class's reach, and in the operation set | the update residual, the transport rank `n − c`, and the operations that write it |
| **ACTUATABLE** | count the class's own operations that change it | whether an operator can **SET** it |
| **REQUIRED FOR CONTROL** | remove it and ask whether the operations survive | a **precondition**, not a knob — kept apart on purpose |

The roles separate in the inventory, measured: the **potential map** is read by **6** readouts of 8 and leaves the
update at exactly **0.0**; the **component count** leaves every readout at exactly **0.0** and moves the reach. So the
two roles are not two names for one thing.

---

## 2. The necessity table (tasks 2 and 6)

| structure | observable | dynamical | actuator | role | evidence |
|---|---|---|---|---|---|
| the state (the occupation) | **yes** | **yes** | **yes** | observable and actuatable | **95** operations write it; **3** of 8 readouts move with it |
| the link set (connectivity) | **yes** | **yes** | no | **a boundary condition** | **0** of 2 cells read an acceleration on the split pair against **2** on the ring; update moves by **2.6E-003** |
| the component count | no | **yes** | no | **dynamical only** | reach **95 → 94**; own instrument invariant: shape **0**, values **0**, ratio **0**, readers **0** |
| density topology | no | **yes** | no | **dynamical only** | the link set's global descriptor: acts through `n − c` alone |
| the transport operator | no | **yes** | no | **dynamical only** | readout movement **0.0** (gauge residual exactly **0**), yet it **is** the evolution |
| the flux | no | no | no | inert | **0** observables read a flux value; no operation takes one as an argument |
| the continuity law | no | no | no | inert | an identity satisfied by the operation, not an operation |
| the ledger | no | no | no | inert | it **detects** a source; not an input of anything |
| the potential map | **yes** | no | no | observable only | read through **6** readouts, update residual **0.0** |
| the clock law | **yes** | no | no | observable only | read through **4** readouts, a map applied at read time |
| the gradient definition | **yes** | no | no | observable only | read through **2** readouts; a difference of the state |
| the kernel's second component | **yes** | no | no | observable only | moves **7** readouts (largest **3.109E-001**) — and **G_087** measured it undecidable |
| the phase structure (the theta sector) | **yes** | no | no | observable only | the interference term moves by **1.283E-002** — in the interference sector |
| mode locking | no | no | no | inert | G_090 measured it a **function of the distribution** |
| the distribution's functionals | no | no | no | inert | correlation, variance, amplitude, frequency, information: all functions of ρ |
| the actualization rate | no | no | no | inert | outside the class, no actuator (G_090) |
| memory channels | no | no | no | **absent from the vocabulary** | no function takes one as an argument: G_091 **named** it rather than using it |

**Census:** observable **7** · dynamical **5** · actuatable **1** · observable+actuatable **1** · boundary condition
**1** · dynamical-only **3** · observable-only **5** · **invisible actuator 0** · inert **6** · absent **1** — of **17**.

---

## 3. The chain on the full pre-reduction theory (task 3)

G_089's eight channels are re-run (unchanged) and joined by the structures the reduction removed: the component count,
the flux, the continuity law, the ledger, the transport's own **rule** and memory channels — **14** rows, each judged by
the only question that decides actuatability: *does an operation of the class write it?*

**Actuatable channels: 1 on the full theory, 1 on the reduced one — a gain of 0.** The pre-reduction structures are
**controls without actuators**, not channels: the reduction changed no **operation** (it removed bookkeeping), so the
full theory's extra vocabulary adds nothing an operator could turn.

---

## 4. The two failure cores (task 4)

| core | members |
|---|---|
| **FailureCore(observables)** | the state · the link set · the potential map · the clock law · the gradient definition · the kernel's second component · the phase structure (**7**) |
| **FailureCore(actuation)** | the state · the link set · the transport operator (**3**) |

Intersection **2**; **actuation-only 1** (the operation's **own rule**); **observables-only 5**. So the cores differ in
**both** directions: **observable necessity ≠ dynamic necessity ≠ actuator necessity**. What matters is *which way* the
**actuatable** members lie, and the answer is one-way: **the only actuatable structure is in the observable core.**

**Why the cores differ is itself the finding:** an operation writes the **state** and nothing else, and the state is
read. So an actuation that left the observables unchanged does not exist in this class — not because it was looked for
and missed, but because the operation's **output** is the observable's **input**.

---

## 5. Dynamics without observables (task 5) — and why it is not a channel

**3** structures move the evolution and no readout: the **component count**, **density topology** and the operation's
**rule**. **None is actuatable.** And the direction is what settles it:

| components | reachable | conserving | unreachable |
|---|---|---|---|
| 1 | **95** | 95 | **0** |
| 2 | **94** | 95 | **1** |
| 4 | **92** | 95 | **3** |

The reach is `n − c` of the `n − 1` conserving directions: a dynamical structure without observables can only **take
reach away**, never grant it. The direction is what refutes it as a channel.

---

## 6. The substance of the failure — an invisible rule, not an invisible actuator

The reduction's strip list contained the **transport operator**, and stripping it was measurably harmless to the
readouts: the layer's gauge residual is **exactly 0** and **0** readouts move. The same structure **is** the whole
operation set, so no operation exists without it — which is the success criterion's **letter**, and the audit reports
it rather than choosing the convenient reading.

Its **content** is about a settable structure, and the rule is not settable by any operation. So the audit's verdict is
the substance: **there is no invisible actuator, only an invisible RULE whose effect is always visible** — every
operation there is writes the state, and the state is read.

---

## Defects found while building this audit

1. **The cut is not a count-only intervention.** The component-count row was first given the readout movement produced
   by the counterfactual cut — **2.304E-001**, and an update residual of **1.200E+000**. Both belong to the **links**:
   a cut changes the link set as well as `c`. The row now uses **G_082's union construction**, which holds the
   component-internal link set fixed and varies only `c` (shape **0**, values **0**, ratio **0**, readers **0**), and the
   cut's numbers are reported in the **LINKS** row where they are measured.
2. **A literal count where a measurement was available.** The state row first asserted "**8** of 8 readouts move with
   it", derived from `8 − lost`. The measured number of readouts that **move** is **3**. The count is now computed.
3. **"Written by an operation" conflated with "is the operation".** The transport operator's row first carried
   `OperationsChangingIt = 95`, which made the class's operations appear to write the **rule** they obey; the truth is
   that no operation writes the rule. The column is **0** for every row but the state, and the rule's dynamical role is
   stated as what it is — **by identity**.
4. **An undefined column read as a false one.** The "decidable in situ" flag was set `false` for every row that is not
   observable, where the question is vacuous. It is now the **one** row that is read and **not** decidable — the
   curvature — and the audit says so once, with G_087's own ratio (`signature / separation = 0.093`) as the independent
   confirmation.
5. **A verdict clause that could have rested on one reading.** "The cores differ" was first going to be argued from
   the actuation-only member alone; it is now computed in both directions (**1** one way, **5** the other), so the
   coincidence is refuted by a measurement rather than asserted.

**Defect 1 is a new defect CLASS, and the guard was NOT built.** The rule "a counterfactual that varies two structures
at once must not be attributed to one of them" was added to the instruction set's *Numerical Reproducibility Rules*
(rule 12) with this audit as the canonical exemplar. A mechanical guard was **not** built, and the reason is stated
rather than glossed: a guard would have to know which structures each intervention touches, and no surviving scanner
carries that map — the honest alternatives are the union construction and the kernel re-cut, which vary one structure
and are what the rule points at.

---

## What this does and does not establish

**Establishes:** that the three roles are separable and are separated by **measurement**; that they come apart in
**both** directions in the surviving inventory; that the set an operator can **set** is a subset of the observable
core; that the set **required** for control is not, and contains the operation's own rule; that the full pre-reduction
theory adds **no** actuatable channel; and that the reach is bounded by `n − c`, so the dynamics-without-observables
structures can only take control away.

**Does not establish:** that a hidden actuator is impossible in **physics**. The audit measures the **surviving
vocabulary**, and its one absent member — a memory channel carrying a coefficient — is **named rather than used**,
because introducing it is what the rules forbid. Nor does it claim the roles are fundamental: it claims that on the
canonical 96-cell ring, with the counterfactual cuts `c = 1, 2, 4` and the tolerance **1e-12**, the roles fall as the
table says.

**Why this is not a restatement of G_089:** G_089 asked control **versus** observable and found 3 unobservable
controls, none actuatable. What is added here is the **dynamical axis** — measured on the update relation, the class's
reach and the operation set, three objects G_089 never touched — the per-structure **removal** measurement (readouts
lost and moved, the update residual), the re-run of the chain on the **full pre-reduction** theory, and the finding
that the reduction was **observation-safe exactly where it was control-fatal**.

---

## Where it stands

**Substrate:** the canonical **96-cell ring** (`SourceManipulationAudit.CanonicalCells`), with the counterfactual cuts
`c = 1, 2, 4` of G_090's inverse requirement. **Equality rule:** `1e-12` on a computed readout value. **No spectral
quantity is quoted** — no A₀, no free room, no lock release, no survivor count — so the spectral-substrate rule is not
engaged.

**Guards:** `Y_G_033` (the substrate scan) — `without` **60 → 61**; `Y_G_035` (the registry) — **92 → 93**, survives
**78 → 79**. Registry entry: **derived**.

**What would change the verdict:** an operation whose argument list contains a link, a count, a flux or a phase (which
would make the corresponding structure actuatable); a readout that survives the removal of the maps (which would shrink
the observable core); or a measurement that decides the kernel's second component, which would move the one row where
**read** and **decidable** come apart.
