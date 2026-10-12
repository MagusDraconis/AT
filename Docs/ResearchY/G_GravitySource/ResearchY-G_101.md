# ResearchY-G_101 — State Degrees-of-Freedom Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_101_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/StateDegreesOfFreedomAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_101`) · **Verdict:** **ONE DEGREE OF FREEDOM**

---

## Question

G_097 measured that the law **diffuses** and that transport is absent; G_098 that the direction is not represented;
G_099 that the orientation field is derived and observable; G_100 that the field is dynamically **decoupled** and has
**0 independent degrees of freedom**.

> **Does the surviving G-sector contain more than one independent dynamical degree of freedom?**

**Constraints:** every count, dimension and rank names its object and is computed on the **96-cell ring alone**; the
**cubic substrate forbidden**; no new primitive, field, source term, topology axiom, action, Lagrangian, transport rule
or hidden variable introduced. **A negative result is acceptable.** **Prefer refutation.**

---

## Answer

**ONE DEGREE OF FREEDOM — and the decision is between three counts that are easy to conflate.**

| Count | Value | Measured how |
|---|---|---|
| **COMPONENTS** of the state | **96** | the occupation carries one number per cell |
| **MOVING directions** the dynamics has | **95** | the update's rank, from the symbol spectrum's multiplicities |
| **CONSERVED directions** | **1** | the uniform mode, whose symbol is **exactly 0.0** |
| **INDEPENDENT STATE COORDINATES** | **1** | of **11** state-like quantities, **4** are invertible reparametrisations and **6** are functionals with a kernel |
| **INDEPENDENT COORDINATES BEYOND THE STATE** | **0** | measured three ways: by rank, by kernel and by removal |
| **CONTROL loss** from reducing to the state | **0** | the operations act on the state, which is retained |

**The sector is a FIELD, not a collection of fields.**

---

## 1. The state-like quantities (task 1)

| quantity | components | role | rank | kernel | status |
|---|---|---|---|---|---|
| the count density (the occupation) | 96 | **the state** | 96 | 0 | ASSUMED (an initial datum) |
| the potential map `A = (1/d) ln ρ` | 96 | reparametrisation | **96** | **0** | DERIVED |
| the rate kernel `K = exp(A + λA²/2)` | 96 | reparametrisation | **96** | **0** | DERIVED |
| `g00 = −ρ^(2/d)` | 96 | reparametrisation | **96** | **0** | DERIVED |
| the clock rate `ρ^(1/d)` | 96 | reparametrisation | **96** | **0** | DERIVED |
| the acceleration field (the averaged gradient) | 96 | functional | **95** | **1** | DERIVED |
| the orientation field (the signed link differences) | 96 | functional | **95** | **1** | DERIVED |
| the excess over the flat background | 96 | functional | **95** | **1** | DERIVED |
| the total count | **1** | functional | **1** | **95** | DERIVED |
| the flux's divergence | 96 | functional | **95** | **1** | DERIVED |
| the surviving readouts (eight functionals) | 8 | functional | **4** | **92** | DERIVED |

**The rank is the measurement, so "independent" and "derived" are decided by a number.** The **four** full-rank rows are
**strictly monotone cell-by-cell maps** whose inverse is **closed form** (`ρ = exp(d·A)`, `ρ = (−g00)^(d/2)`, …) — a
**change of variable cannot add a degree of freedom**. The **six** functionals each have a kernel, and the kernels are
the audit's content: **1** for every operator that differences (the constant, i.e. the count's **SCALE**), **95** for the
total (which fixes the scale and nothing else), **92** for the readouts (**G_086's kernel collapse, as a rank**).

**Every rank is reported at the stated rule and at the loose end of its band, and every row's rank is flat** — no
classification in this audit turns on the rule it was taken at (G_096: a rank that moves with its rule is not a
dimension).

---

## 2. The dependency table (tasks 2 and 4)

The **forward** direction (the quantity from the state) is an **identity** for every row, because each quantity is
*defined* from the state. The informative direction is the **inverse**, and that is what the rank measures:

| quantity | rank | kernel | the state is recoverable? |
|---|---|---|---|
| the potential `A` | 96 | 0 | **yes, in closed form** |
| the kernel `K` | 96 | 0 | **yes, in closed form** |
| `g00` | 96 | 0 | **yes**, `ρ = (−g00)^(d/2)` |
| the clock rate | 96 | 0 | **yes** |
| the acceleration field | 95 | 1 | no — the **scale** is invisible |
| the orientation field | 95 | 1 | no — the **scale** |
| the excess | 95 | 1 | no — the **scale** |
| the total count | 1 | 95 | no — it fixes the **scale** and nothing else |
| the flux's divergence | 95 | 1 | no — the **scale** (and G_083's circulation is a further freedom) |
| the readouts | 4 | 92 | no — **4 directions of 96** |

**The partition is three-way, and the binary split the task asks for is the last line:** the state (**1**),
the invertible reparametrisations (**4**), the functionals with a kernel (**6**); **independent = the state alone**.

---

## 3. The dynamical state-space rank, with its tolerance band (task 3)

The update is the ring's Laplacian, so its modes are the ring's own Fourier modes and the symbol at mode *k* is the
eigenvalue, with multiplicities **1** at *k* = 0, **2** for the interior modes and **1** at the alternating mode — the
multiplicities sum to **96**, which the suite asserts rather than assumes.

| mode | symbol | multiplicity |
|---|---|---|
| 0 | **0.000000E+000** | 1 |
| 1 | −4.282154E-003 | 2 |
| 2 | −1.711028E-002 | 2 |
| 47 | −3.995718E+000 | 2 |
| 48 (alternating) | −4.000000E+000 | 1 |

| tolerance | rank | kernel |
|---|---|---|
| 1e-12 (the audit's stated rule) | **95** | **1** |
| 1e-10 / 1e-8 / 1e-6 / 1e-4 / 1e-3 | **95** | **1** |
| 1e-2 | 93 | 3 |

**The rank is 95 across nine decades of tolerance**, because the only **zero** symbol is the uniform mode (**exactly**
0.0) and the smallest non-zero magnitude is **4.282154E-003** — the band's edges are the **spectrum's own magnitudes**,
not the rule. At **1e-2** the rank is **93**, because that tolerance is *above* the smallest non-zero magnitude: the
band is **named** rather than assumed, and the first draft's claim of "tolerance-free across ten decades" was refuted by
its own loose end.

---

## 4. What the sector still needs (reported so that nothing is hidden)

| input | dimensions | a state coordinate? |
|---|---|---|
| the occupation itself (the initial datum) | 1 | **yes** |
| the actualization tick | 1 | no |
| the step size ε | 1 | no |
| the clock law's coefficient (G_075's free room) | **5** | no |
| the link set (connectivity) | 1 | no |
| the labelling | 1 | no |

**Six independent inputs, one of which is a state coordinate.** A **parameter** or a **structure** indexes the
*description*; only a **field** indexes the *state space*. The law's coefficient is **free** (G_095) and is reported as
an input rather than as a coordinate for exactly that reason.

---

## 5. The removal test (task 6)

| structure removed | readouts lost | readouts moved | update residual |
|---|---|---|---|
| the flux (and its gauge displacement) | 0 | 0 | **0.000E+000** |
| count conservation | 0 | 0 | **0.000E+000** |
| the transport operator | 0 | 0 | **0.000E+000** |
| the ledger | 0 | 0 | **0.000E+000** |
| the potential map | **6** | 0 | 0.000E+000 |
| the clock law | **4** | 0 | 0.000E+000 |
| the gradient definition | **2** | 0 | 0.000E+000 |
| the link set (a two-component cut) | 0 | 1 | **1.200E+000** |
| the link set (removed entirely) | 0 | 1 | **2.569E-003** |
| the component count | 0 | 0 | **0.000E+000** |

**One structure FAMILY enters the evolution — the link set — and it is an input (G_080), not a state coordinate.** The
three maps supply readouts (**12** readout-losses over three parts) and move **no** update; the layers G_083 demoted
move nothing at all; and the largest movement of any readout is **1 of 8**.

---

## 6. The reduction test (task 7)

The whole surviving sector rebuilt from the state alone (the state, the link set and the three maps — G_084's minimal
core):

| loss | measured |
|---|---|
| **observable loss** | **0** readouts lost, **0** moved of 8 |
| **evolution loss** | **0.000E+000** |
| **control loss** | **0** — the reach is unchanged at **95** elementary directions and the **1** actuatable structure is the state itself |

**The reduction is not vacuous**, and the suite asserts that: the operations act on the **state**, so removing every
derived description part costs no reach.

---

## Defects found while building this audit

1. **A rank above its own component count.** At the first draft's difference step `h = 1e-6` the instrument's rounding
   floor (~1e-16/h ≈ **1e-10**) sat **exactly on** the 1e-10 rank rule, so the centring projection — whose exact kernel
   is one dimensional — read a rank of **96** with kernel **1**, i.e. **rank + kernel = 97 > 96**, an impossible pair.
   Fixed by `h = 1e-4` (noise ~1e-12, truncation ~1e-8) with a **stated** Jacobian rule of 1e-8, and by an assertion
   that rank and kernel **partition** the components for every 96-component row.
2. **A rank quoted without its band.** The first reading reported a single rank per quantity. Every rank is now reported
   at the rule **and** at the loose end, and the suite asserts flatness for **every** row.
3. **A false "ten decades" claim.** The dynamics' rank is **93** at 1e-2, because that tolerance is **above** the
   smallest non-zero symbol magnitude: the claim now names the band's edges as the spectrum's own magnitudes and reports
   a measured **nine decades of tolerance**.
4. **One structure counted twice.** The link set appears in two forms (a cut and a complete removal), so a count of
   *rows* read "2 structures enter the evolution" and the **success criterion read MET**. Rows are now keyed on a
   structure **family** and distinct families are counted — the same discipline G_092's rule 12 imposed on
   interventions that move more than one quantity.
5. **A sum reported as a count.** "12 readout(s)" was printed against a sector that has **8** readouts; the number is
   **12 readout-losses over three description parts**, and the two are different quantities.

---

## What this does and does not establish

**Establishes:** that every surviving state-like quantity is enumerated with the **rank** of its Jacobian against the
state, so that "independent" and "derived" are decided by measurement; that **four** quantities are **invertible
reparametrisations** (full rank 96, kernel 0) and are therefore **changes of variable**; that **five** are functionals
whose lost direction is the count's **scale** and one is the total count with a **95**-dimensional kernel; that the
**readouts are a rank-4 contraction** of a 96-dimensional state (G_086's kernel collapse as a rank); that the dynamics'
rank is **95** with **1** conserved direction and is **flat across nine decades of tolerance**; that removing each of
**ten** structures individually moves the update only for the **link set**; and that reconstructing the whole sector
from the state alone costs **0** observables, **0** evolution and **0** control.

**Does not establish** that the sector is **simple** — one degree of freedom is not one number, and all three counts are
reported so the verdict cannot be read as a trivial state space; nor that the **description** is minimal, since the tick,
the step, the law's coefficient, the link set and a labelling are still needed (and the coefficient is **free**).
Nor does it re-derive the earlier audits' findings: it **re-measures** the ones it consumes (the scale the orientation
field loses, the conservativity of the total, the flux's divergence) at its own tolerances and reports them as its own
numbers.

**Why this is not a restatement of G_084 or G_100:** G_084 counted the **description's** variables and found the minimal
core to be one state variable and three equations; G_100 measured that **one derived object** (the orientation field)
has no independent degrees of freedom. Neither measured the **rank** of the state space, the **kernel** of each
quantity's map, or the partition between components, moving directions and coordinates — and "is there more than one
degree of freedom" cannot be answered by a count of named variables, which is exactly why it is answered here by a rank
with a **stated tolerance band**.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, links = `LinksOf(96, 1)`, densities =
`MapRedundancyAudit.State`. **Equality rules:** **1e-12** on a computed value, **exact** where an identity holds (the
uniform mode's symbol is exactly 0.0), and a **stated tolerance** for every rank — **1e-12** for the dynamics, where
the zeros are exact, and **1e-8** for the finite-difference Jacobians, where the instrument's floor is ~1e-12 and its
truncation ~1e-8. `ε = 0.25` is the same stated step the four preceding audits used. **No spectral quantity is
quoted** beyond this ring's own symbol spectrum. The **cubic substrate is forbidden and is not named in the suite**.

**Guards:** `Y_G_033` — `without` **69 → 70**; `Y_G_035` — **101 → 102**, survives **87 → 88**. Registry entry:
**derived**.

**What would change the verdict:** a quantity that is **not** a function of the occupation (which would make its
Jacobian against the state undefined rather than full-rank); a **second field** the readouts depended on (which would
give the sector a two-coordinate state space); or a **structure that enters the evolution and is not an input**, which
the ten-part removal test would have shown as an update residual outside the link set.
