# ResearchY-G_102 — Freedom Closure Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_102_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/FreedomClosureAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_102`) · **Verdict:** **CLOSED — RANK 1**

---

## Question

G_101 counted the sector's state space: **96** components, **95** moving directions, **1** conserved direction and
**ONE INDEPENDENT STATE COORDINATE**, with every surviving quantity either an invertible reparametrisation of the
occupation or a many-to-one functional of it.

> **Can the surviving G-sector generate an independent degree of freedom internally, or is the one-degree-of-freedom
> reduction exact?**

**Constraints:** every count, dimension and rank names its object and is computed on the **96-cell ring alone**; the
**cubic substrate forbidden**; no new primitive, field, source term, topology axiom, action, Lagrangian or hidden
variable introduced. **A negative result is acceptable.** **Prefer refutation.**

---

## Answer

**CLOSED — the reduction is exact for everything the sector DERIVES.**

| Required output | Measured |
|---|---|
| **Candidate table** | **16** candidates: **6** functions of the state · **7** supplied structures · **3** absent |
| **Rank-contribution table** | **13** stacked measurements, **all rank 96**, **all increase 0** |
| **Maximum state-space rank** | **96** from all surviving structures together — against **96** for the state alone, an increase of **0** |
| **Independent-coordinate count** | **1** (G_101's measurement, re-used) |
| **Reconstruction residuals** | **4 of 11** close in closed form (**worst 2.776E-016**); **1** obstructed by a **scale**, **1** by a **kernel dimension**, **4** by a measured **movement**, **1** by **absence** |
| **Final classification** | **CLOSED (rank = 1)** |

---

## 0. The instrument has two arms, because two claims are in play

A quantity that **is** a function of the state **cannot add a direction**: its Jacobian against the state has 96
columns, so **stacking** it with the state leaves the rank where it was — and that is measured. A quantity that **is
not** a function of the state **can** be varied while the state is held fixed, and then the only question that makes it
a **dynamical** degree of freedom is whether the **evolution** reads it. The audit therefore separates three things
the first draft bundled: **generated internally**, **supplied to the description**, and **absent**.

---

## 1. The candidate table (task 1)

| candidate | origin | rank | kernel | readout effect | update effect | how the evolution sees it |
|---|---|---|---|---|---|---|
| the orientation field | function of the state | **95** | **1** | 0 | 0 | — |
| the acceleration field | function of the state | 95 | 1 | 0 | 0 | — |
| the excess over the flat background | function of the state | 95 | 1 | 0 | 0 | — |
| the total count | function of the state | **1** | **95** | 0 | 0 | — |
| the flux's divergence | function of the state | 95 | 1 | 0 | 0 | — |
| the surviving readouts | function of the state | **4** | **92** | 0 | 0 | — |
| the flux's own circulation | **supplied** | — | — | **0.000E+000** | 0 | — |
| the component count | **supplied** | — | — | **0.000E+000** | 0 | — |
| the labelling | **supplied** | — | — | **3.680E-001** | 0 | — |
| the clock law's coefficient | **supplied** | — | — | **2.183E-002** | 0 | — |
| the step size ε | **supplied** | — | — | 0 | 2.500E-001 | **coefficient** |
| the link set | **supplied** | — | — | 0 | **1.200E+000** | **argument** |
| the tick | **supplied** | — | — | 0 | 5.000E-001 | **index** |
| the memory channel | **absent** | — | — | 0 | 0 | — |
| the vector sector | **absent** | — | — | 0 | 0 | — |
| the unified action | **absent** | — | — | 0 | 0 | — |

**The three roles of "how the evolution sees it" are distinguished, not bundled.** The update reads
`(cells, links, ρ)`: the **link set** is the only non-state structure it takes as an **argument**; ε is its
**coefficient** (the rate at which the law is applied) and the tick its **index** (the independent variable) — neither
is a structure. The first draft folded all three into one flag and reported **three** structures as "read by the
evolution" where only one is an argument.

---

## 2. The rank-contribution table (task 2) and the maximum rank (task 3)

| candidate | group | rank stacked with the state | increase |
|---|---|---|---|
| the orientation field | orientation | 96 | **0** |
| the acceleration field | orientation | 96 | **0** |
| the excess over the flat background | state-like | 96 | **0** |
| the total count | conservation | 96 | **0** |
| the flux's divergence | evolution | 96 | **0** |
| the surviving readouts | observable | 96 | **0** |
| the link set | supplied | 96 | **0** |
| the step size ε | supplied | 96 | **0** |
| the tick | supplied | 96 | **0** |
| the labelling | supplied | 96 | **0** |
| the clock law's coefficient | supplied | 96 | **0** |
| the flux's circulation | supplied | 96 | **0** |
| the component count | supplied | 96 | **0** |

**MAXIMUM STATE-SPACE RANK = 96**, against **96** for the state alone — **an increase of 0**, with **5 supplied inputs**
reported beside it. **The stacking works for a supplied structure too, and that is the audit's sharpest step:** a
supplied structure has no Jacobian against the state, so what is stacked is its measured **effect** — and an effect is
a **vector in the 96-dimensional state space**, so stacking it cannot raise the space's rank either. It can add a
**parameter dimension to the description**, which is a different count and is reported separately.

**The rank helper had to be fixed for this:** stacking quantities with *different component counts* (a 96-wide
Jacobian with the 1-wide total count) made the first draft's elimination throw an `IndexOutOfRangeException`, because
it assumed a rectangular matrix. Short rows are now read as **zero-padded**, which is the correct extension — a
quantity with fewer components has zeros where the state has directions.

---

## 3. The three groups (tasks 4, 5 and 6)

| group | members | max rank increase | measurement |
|---|---|---|---|
| **the orientation quantities (task 4)** | 2 | **0** | the field 95/1 and the acceleration 95/1: one kernel each, the count's scale |
| **the observable quantities (task 5)** | 1 | **0** | the readouts at rank 4 with a **92**-dimensional kernel |
| **the evolution quantities (task 6)** | 1 | **0** | the update's **own rank is 95**, which is the state's rank and not an increase |
| the state-like functionals | 1 | 0 | the excess, 95/1 |
| the conservation quantity | 1 | 0 | the total count, 1/95 |
| the supplied structures | 7 | **0** | effects, not columns — a parameter dimension at most |

---

## 4. The reconstruction residual table (task 7)

| quantity | direction | residual | obstruction | kind | closes |
|---|---|---|---|---|---|
| the potential map `A` | the state from `A` | **1.388E-016** | — | — | **yes** |
| the rate kernel `K` | the state from `K` | **2.776E-016** | — | — | **yes** |
| `g00` | the state from `g00` | **1.388E-016** | — | — | **yes** |
| the clock rate | the state from the rate | **2.776E-016** | — | — | **yes** |
| the orientation field | the state from the field | — | **6.939E-017** | **scale** | no |
| the surviving readouts | the state from the readouts | — | **92** | **kernel** | no |
| the flux's own circulation | the flux from the state | — | 0.000E+000 | movement | no |
| the labelling | the labelling from the state | — | **3.680E-001** | movement | no |
| the clock law's coefficient | the coefficient from the state | — | **2.183E-002** | movement | no |
| the link set | the link set from the state | — | **1.200E+000** | movement | no |
| the closure requirements | from the state | — | — | **absent** | no |

**The obstruction's KIND is named, because a count is not a magnitude.** The first draft averaged them into one
number (**1.337E+001**) — a statistic of a kernel dimension and a displacement, which is nothing.

---

## Defects found while building this audit

1. **A relabelling test that was the identity.** The audit's relabelling candidate measured **0** moved readouts,
   because it reflected the *canonical* state — which G_099 measured to be **parity-even to the rounding**, so the
   reflection fixes it. The test now runs on the audit's own **skewed pattern** (measured **3.680E-001**), and the
   reading says why.
2. **A rank helper that assumed a rectangle.** Stacking a **1-wide** quantity with the state's **96-wide** identity
   threw an `IndexOutOfRangeException`; the rank is now taken with short rows **zero-padded**.
3. **Three roles bundled into one flag.** ε, the tick and the link set were all reported as "read by the evolution";
   the record now carries an **`EvolutionRole`** (not read / argument / coefficient / index) and the audit reports
   **one** structure the law takes as an argument.
4. **Counts printed where magnitudes were meant.** The free room's readout effect printed **3.000E+000** — the *number*
   of readouts that move, not the movement; every effect is now a magnitude and every count lives in the reading.
5. **A units mash in one column.** The group table put the evolution's **rank** (95) in the same column as the other
   groups' **increase** (0), and the reconstruction table averaged a kernel dimension with displacements; both are now
   split — the rank lives in its reading, and the obstruction carries its **kind**.

---

## What this does and does not establish

**Establishes:** that every surviving quantity G_101 did not classify as a reparametrisation is enumerated with the
rank of its Jacobian against the state and the **magnitude** of what a variation of it moves; that every derived
quantity's Jacobian stacked with the state's own leaves the rank unchanged; that a **supplied** structure's rank
contribution is measured the same way through its **effect**, which is a vector in the state space and therefore cannot
raise its rank either; that the **maximum rank obtainable from all surviving quantities together is the state's own**;
that the **orientation, observable and evolution** groups each contribute **zero** increase, with the evolution's own
rank reported as the state's rank; that **four** quantities rebuild the state in **closed form** at the rounding while
the others' obstructions are measured and **classified by kind**; and that exactly **one** candidate is a structure the
evolution takes as an **argument** — and it is supplied.

**Does not establish** that the theory is **complete**. Three structures G_093 found necessary for closure — the
**memory channel**, the **vector sector** and the **unified action** — are **absent** from the surviving sector, and
this audit reports them as absent rather than reconstructed; the law's coefficient remains **free** (G_095); and the
description still needs **six inputs**, five of which are not state coordinates. Nor does it establish that a closure,
if performed, would leave the rank at one: a memory channel or a vector sector is a structure the theory **never had**,
and adding one is a **different theory**, which these rules forbid this audit to construct. What it establishes is that
nothing **surviving** generates a second direction.

**Why this is not a restatement of G_101:** G_101 counted the state space and measured each quantity's rank **against**
the state — what a quantity **sees**. This audit measures the other direction: whether stacking a quantity with the
state **raises** the rank, and whether a quantity that is **not** a function of the state can be varied into a
dynamical direction. The first is a rank of a **stacked matrix** and the second is an **effect on the update**, and
neither was computed by G_101 — which is why "the reduction is exact" is a measurement here rather than a corollary.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, links = `LinksOf(96, 1)`, densities =
`MapRedundancyAudit.State`; the relabelling and free-room tests use the audit's **skewed** working pattern, because the
canonical state is reflection-symmetric. **Equality rules:** **1e-12** on a computed value, **exact** where an identity
holds (the uniform mode's symbol is exactly 0.0), and a **stated tolerance** for every rank — **1e-12** for the
dynamics and **1e-8** for the finite-difference Jacobians, whose floor is ~1e-12 and whose truncation is ~1e-8 at the
step of **1e-4**. **No spectral quantity is quoted** beyond this ring's own symbol spectrum. The **cubic substrate is
forbidden and is not named in the suite**.

**Guards:** `Y_G_033` — `without` **70 → 71**; `Y_G_035` — **102 → 103**, survives **88 → 89**. Registry entry:
**derived**.

**What would change the verdict:** a quantity that is **not** a function of the occupation *and* is **read by the
evolution** (which the removal and variation tests would show as a non-zero update effect for a candidate that is not
supplied — the link set is the only one, and it is an input); a **second field** the readouts depended on; or a
**closure** that introduced a structure the surviving sector lacks, which is a different theory rather than a
generation.
