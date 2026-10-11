# ResearchY-G_084 — Minimal Gravity Core Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_084_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/MinimalGravityCoreAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_084`) · **Verdict:** **MINIMAL CORE** (registry: Derived)

---

## Question

G_080 classified the substrate's connectivity as **ASSUMED**, G_082 the component count as **GAUGE-LIKE** and G_083 the
continuity law as **COORDINATE CHOICE**. The transport vocabulary of the surviving gravity sector has therefore been
measured, piece by piece, to be something the observables do not consume.

> **Strip everything no surviving observable requires — connectivity, the component count, the transport layer, the
> continuity law, the flux variables — and find the smallest theory that reproduces every surviving observable.**

**Prefer reduction.** The licence is to **remove**, never to add.

---

## Answer

**MINIMAL CORE — ONE STATE VARIABLE AND THREE EQUATIONS REPRODUCE EVERY SURVIVING OBSERVABLE EXACTLY.**

| Task | Result |
|---|---|
| 1 Minimal variable set | **3 quantities** — the count density, the potential map, the clock law (1 state + 2 maps) |
| 2 Minimal equation set | **3 equations** — the potential map, the clock law, the gradient definition (of 6) |
| 3 Smallest surviving theory | `(ρ, a labelling)` with the three maps; **8 of 8** readouts reproduced, **0** lost |
| 4 Which claims hold | **23 of 29** (0.793) hold; **6 dissolved**; **0 refuted** |
| 5 Symbols never in an observable | **5** — the flux, the divergence, the component count, the transport capacity, the ledger residual |
| 6 `CoreCompressionRatio` | **2.667** (16 named items → 6), with observable fidelity **1.000** |
| 7 Classification | **essential 3 · coordinate 2 · gauge 2 · redundant 3** |

---

## 0. The instrument, and why the state is not uniform

Three choices decide whether this audit can find anything, and all three are stated before the result:

1. **The state is one cosine mode on the 96-cell ring**, mean 1 and total 96, strictly positive. A **uniform**
   occupancy would make the gradient, every redshift and the uplift identically zero, so "no readout moves" would hold
   for a trivial reason and the elimination test would prove nothing. This is the uniformity trap, and it is the
   analogue of G_081's `7 → 48` defect: a comparison that is vacuous because the object has no structure.
2. **The readout set is the sector's own**: the six temporal observables G_035 declares **by signature**, plus the two
   pair readouts the sector actually predicts (the gradient along a link, the redshift between two sites). A set chosen
   by this audit could be tuned to make the strip look good.
3. **The equations are switches, not prose.** Each of the six is a flag on a description record, so an equation can be
   *removed* and its supply **measured** by re-evaluating the readouts instead of being declared necessary.

The **minimal description** is the state, the labelling, the potential map, the clock law and the gradient
definition — no flux, no divergence, no component count, no transport operator, no ledger, no conservation statement.

---

## 1. The reproduction test (tasks 3 and 4)

Every readout is computed from the full description and from the minimal one, and the transport layer **enters the
computation** rather than being bypassed (its contribution is the divergence's gauge residual, which the circulation
identity makes exactly zero) — so this is a measurement, not a tautology.

| Observable | Full | Minimal | Difference | Frame |
|---|---|---|---|---|
| G00, the metric's time component | −1.367981E+000 | −1.367981E+000 | **0.000E+000** | absolute |
| the clock rate at a site | 1.169607E+000 | 1.169607E+000 | **0.000E+000** | absolute |
| the clock potential at a site | 1.566679E-001 | 1.566679E-001 | **0.000E+000** | absolute |
| the redshift of a site from its own rate | −1.450120E-001 | −1.450120E-001 | **0.000E+000** | absolute |
| the source acceleration of the field (a gradient) | 1.634219E-002 | 1.634219E-002 | **0.000E+000** | invariant |
| the second-order AT/GR ratio at the target | 1.098132E+000 | 1.098132E+000 | **0.000E+000** | invariant |
| the gradient (acceleration) along a link | 2.677421E-004 | 2.677421E-004 | **0.000E+000** | invariant |
| the redshift between two sites | −1.450120E-001 | −1.450120E-001 | **0.000E+000** | invariant |

**8 of 8 reproduced, 0 lost.** The conservation row is kept **out** of the observable set and reported separately:
its value is `0.000E+000` in **both** descriptions, and in the minimal one it is **VACUOUS** — there is no evolution
left for it to govern. Its ledger, a declared source of `0.0300`, is detected exactly (residual `0.000E+000` from the
source): that is G_083's **falsifiability**, not a measurement.

---

## 2. The inventory and the elimination test (tasks 1 and 5)

Each quantity is varied over an **admissible family** built from the surviving audits' own objects, with the state held
fixed, and the readouts are counted. `supplies` is the number of readouts that become **unproducible** when the
equation stating the quantity is removed.

| Quantity | Kind | supplies | invariant reads | absolute reads | config differs | role |
|---|---|---|---|---|---|---|
| the count density | State | 8 | 3 | 0 | no | **essential** |
| the potential map | MapOfTheState | 6 | 0 | 4 | no | **essential** |
| the clock law | MapOfTheState | 4 | 1 | 2 | no | **essential** |
| the adjacency | Labelling | 2 | 0 | 0 | no | **coordinate** |
| the scale reference | Labelling | 0 | 0 | 4 | no | **coordinate** |
| the component count | Freedom | 0 | 0 | 0 | **yes** | **gauge** |
| the flux | Freedom | 0 | 0 | 0 | **yes** | **gauge** |
| the divergence | Descriptor | 0 | 0 | 0 | no | **redundant** |
| the transport capacity (`n − c`, the nullity) | Descriptor | 0 | 0 | 0 | no | **redundant** |
| the ledger's source residual | Descriptor | 0 | 0 | 0 | no | **redundant** |

The classification rule is computed, not chosen:

```
ESSENTIAL   supplies readouts and one of them moves when the quantity is varied
COORDINATE  its variation is a relabelling or a change of units (the state transforms with it)
GAUGE       the configuration really differs and no readout moves
REDUNDANT   a function of the removed layer: no readout, no independent variation
```

---

## 3. The equation set and the minimum cover (task 2)

| Equation | Readouts lost when it is removed |
|---|---|
| the potential map `a = (1/d) ln ρ` | **6** |
| the clock law `W = F(a)`, `F(0) = 1`, `F'(0) = 2` | **4** |
| the gradient definition: a difference of potentials along a link | **2** |
| count conservation `∂tρ + ∇·J = 0` | **0** |
| the transport operator (the divergence, the rank `n − c`, the nullity) | **0** |
| the ledger (the source detector) | **0** |

**Three of six equations supply no readout at all.** The cover is found by brute force over all **64** subsets with ties
broken by equation order: **MINIMUM COVER (3 equations)** — the potential map, the clock law, the gradient definition.

And because one choice here is a judgement rather than a measurement — whether the conservation row counts as an
observable — the audit computes the alternative too: counting it, the cover is **5 equations**, since the row needs both
the conservation statement and the operator. Both are reported, neither preferred.

---

## 4. `CoreCompressionRatio` (task 6)

**Defined, not chosen:** the description's size is its named quantities plus its stated equations, the minimal
description's size is the essential quantities plus the minimum cover.

| | Full | Minimal |
|---|---|---|
| quantities | 10 | **3** |
| equations | 6 | **3** |
| **total** | **16** | **6** |

**`CoreCompressionRatio = 16/6 = 2.667`**, with **observable fidelity 1.000** and **observable loss 0** — because a
compression that loses observables is not a compression of the theory. Claims held: **0.793**.

---

## 5. What survives, and how it is lost (tasks 4 and 5)

Of the 29 surviving claims of G_081's map, **23 hold unchanged** and **6 are dissolved**. The dissolved six are exactly
G_081's connectivity failure core:

- the divergence's rank is `n − c`: the transport capacity of the substrate;
- the circulation is one-dimensional per component: the nullity is `c`;
- conserving changes that are **not** transports: the continuity law's failure count is `c − 1`;
- the conserved total leaves `c − 1` free relative levels;
- a bounded region cannot change alone: the ledger conclusion for a whole component;
- a comparison across the substrate needs a channel: the path length is finite.

**DISSOLVED, NOT REFUTED** is the audit's most important distinction. A refuted claim costs the theory a false
statement; a dissolved claim costs it a **subject**. Every one of the six is a true statement *about the transport
layer*, and when the layer is stripped the statement has nowhere to be true or false. **0 claims are refuted**, measured
at the readout level where the strip contradicts no value.

**Task 5, the symbols that never appear in any observable:** the flux, the divergence, the component count, the
transport capacity and the ledger residual — **5 symbols, 0 appearances**. The evidence is the **arity proof, executed**:
every parameter of every temporal observable's signature is the occupation or the compactness, and none names a `J`, a
`c` or an `n`. **No token scan is attempted here on purpose** — G_081 measured that pattern scans over this tree inflate
counts on ordinary arithmetic — so the signatures and the read counts are the evidence instead.

---

## 6. What resists elimination, and why it is a coordinate (task 6)

The minimal description is **not** free of structure, and the audit says which structure rather than quietly keeping it:
**two readouts must be written along some adjacency** (the sector's field gradient and the link gradient), so the
**labelling** cannot be struck from the description.

What makes it a coordinate rather than a primitive is a **measurement**: reverse the labels **and** the state together
and every readout is unchanged to the last bit — **0 of 8 move**. The pair (state, labelling) is one coordinate system.
A second measurement fixes the other coordinate: a **change of units** moves **4 absolute** readouts and **0
frame-invariant** ones, so the scale is the coordinate of the absolute level — disclosed, not called invisible.

The minimal core is therefore **`(ρ, a labelling)`**: one state variable and one coordinate.

---

## Defects found while building this audit

Recorded rather than quietly fixed, per the project's rule that the first version's own errors are evidence.

1. **A vacuous "no readout moves" for the component count.** The c-family evaluated the pair readout at a *placeholder*
   distance derived from the cell count (`n/4`), so at `c = 2` it compared a different pair of cells and the readout
   moved for an indexing reason, not a physical one. The family now reads every member at the **same sites inside the
   component**. This is the same class as G_082's union construction: the counterfactual must hold the local object
   fixed, or its answer is about the instrument.
2. **"No flux at all" that kept the displaced flux.** Zeroing `J` while leaving `J + circulation` in place left the
   layer's gauge residual non-zero, so the gradient readout moved in the member that removed the layer — a bookkeeping
   artefact reported as a physical read. Both fields are now zeroed together.
3. **A hardcoded supply column.** The first draft declared which readouts each equation supplies. Replaced by a
   **measured** supply: the equation is removed from the description and the unproducible readouts are counted.
4. **A uniform state would have made the audit empty.** With `ρ` constant the gradient, every redshift and the uplift
   are 0, so every "nothing moves" answer would have been true and meaningless. The canonical profile is non-uniform by
   construction.
5. **A cover that could be tuned by a judgement.** Whether the conservation row is an observable is not a measurement,
   so the audit computes **both** covers (3 and 5) instead of choosing the flattering one.
6. **A claim-loss row that counted dissolution as refutation.** The first draft's `refuted` was the count of claims
   whose value moves — which would have scored the strip as 6 refutations when the honest reading is 6 **dissolutions**
   and 0 refutations. Refutation is now counted where it can be measured: at the readouts.
7. **An assertion about the maps' number.** The test asserted one derived map in the minimal set; the measurement says
   **two** (the potential map and the clock law). Recorded because it is a reminder that the *test* was wrong, not the
   audit.
8. **A pre-existing guard counted prose as code — and the guard was rebuilt rather than the prose reworded.**
   `CubicSubstrateAudit.ScanAudits` (G_033) split code from comments by testing only whether a **line** begins with `//`
   or `*`, so a substrate mention inside a **report string** or a `/* */` block was counted as an executable reference.
   This audit's own suite was the trigger — its report states the substrate rule, and the scanner read that sentence as
   *using* the lattice — but the same rule had already misclassified three earlier suites: **Y_G_004** (its report string
   names the derived coupling), **Y_G_024** (a quoted method line) and **Y_G_043** (the compared-family lines are
   literals). The fix strips literals and comments with the project's own `AtSourceScan` helper before counting, and the
   guard now reads **24** suites whose code names the substrate, **6** that name it in prose only and **53** not at all.
   This is the **G_081 defect class** (a pattern scan counting ordinary text), and the correction is a mechanical guard,
   not a rule in prose.

   **The caveat travels with the correction, because the scanner's honest limit is now visible:** it detects the *token*,
   not the *dependency*. A suite that reaches the substrate through a helper — which is exactly what Y_G_043's family
   comparison does — names nothing in its executable lines and is classified as a non-user. The G_033 bucket is therefore
   a statement about **vocabulary**, and this audit says so rather than reading it as a statement about computation.

---

## What this does and does not establish

**Establishes.** That a description with **no connectivity, no component count, no transport layer, no continuity law
and no flux variable** reproduces all **8** readouts with difference `0.000E+000`; that **7 of 10** named quantities are
removable, each with a measured reason and a four-way role; that **3 of 6** equations supply no readout and the minimum
cover is **3**; that the compression is **2.667** with **fidelity 1.000**; and that the cost is **6 dissolved claims and
0 refutations**.

**Does not establish.** That the removed layers are **false**. The transport layer is exact, its ledger is a live source
detector, and its six claims are true statements about a structure the theory may still choose to carry. What is
measured is that **nothing observable requires them** — the distinction G_035 drew between the metric and the numbers.

**The remaining dependence, stated rather than hidden.** The minimal description is `(ρ, a labelling)`: two readouts
must be written along an adjacency. The audit measures that no adjacency is privileged rather than asserting it, and it
reports the 2 absolute readouts a change of units moves.

**Out of scope (substrate).** The audit quotes **no spectral quantity** — no eigenspace count, no multiplicity, no
lattice spectrum — so the D96/D96³ Spectral Substrate Rule is **not engaged**, and the audit says so instead of omitting
the lattice. Its objects are the 96-cell canonical occupancy, the sector's own observables and the transport operators
of the class.

---

## Where it stands

The chain G_079 → G_084 now reads:

- G_079: the continuity law holds and is equivalent to count conservation **given** connectivity;
- G_080: connectivity is **ASSUMED** — 0 of 4 primitives detect a cut;
- G_081: **6 of 29** claims depend on it, all in the transport layer;
- G_082: the component count is **GAUGE-LIKE**, read by no local observable;
- G_083: the continuity law is a **COORDINATE CHOICE**, exact, falsifiable, required by no observable;
- G_084: **strip it all and nothing observable is lost** — the minimal core is one state variable, one coordinate and
  three equations, at a compression of **2.667** with **0** observable loss.

`MinimalGravityCoreAudit.Verdict()` starts with `MINIMAL CORE`, and the registry classifies the audit as
**Derived** — the difference from G_081–G_083, which are Boundary entries: those measured what an **input** costs, while
this audit **derives** what the surviving observables actually require.

The reduction is not a proposal to delete the transport layer from the project. It is the measurement that the layer is
**not what carries the sector's numbers**, and it hands on the question G_083 raised in sharper form: if the flux is
bookkeeping and the strip costs nothing observable, then the direction in the theory comes from the **state and its
labelling** alone — and any statement that needs more than `(ρ, A)` is a statement about structure the observables do
not read.
