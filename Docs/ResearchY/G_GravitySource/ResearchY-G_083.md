# ResearchY-G_083 — Continuity Gauge Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_083_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ContinuityGaugeAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_083`) · **Verdict:** **COORDINATE CHOICE** (registry: Boundary)

---

## Question

G_079 derived the continuity law `∂tρ + ∇·J = 0` for the surviving update class and showed that its equivalence with
count conservation **needs the substrate's connectivity**. G_080 classified that connectivity as **ASSUMED**.
G_081 measured that connectivity acts on the surviving sector only through the component count — **6 of 29** claims
depend on it, all of them in the transport layer. G_082 classified the component count as **GAUGE-LIKE**.

> **Is the continuity law `∂tρ + ∇·J = 0` part of the physics, or part of the coordinate choice?**

**Allowed:** Difference, Actualization, the generation relation, count conservation.
**Forbidden:** assuming connectedness, fixing `c = 1`, importing topology, **adding source terms**.

---

## Answer

**COORDINATE CHOICE — NO OBSERVABLE REQUIRES THE LAW, AND ITS ONLY CONTENT IS A CONSTRAINT THAT IS ASSUMED.**

| Task | Result |
|---|---|
| 1 Re-express all observables without continuity | **Exact** — all 4 readouts agree in all three descriptions to the last bit |
| 2 Which claims need `J`, which need only `ρ`? | **0 of 29** need a **flux value**; **23** occupation only, **6** operator structure |
| 3 Remove the transport layer entirely | **0 of 4** quantities change; conservation becomes **VACUOUS**, not violated |
| 4 Equivalence classes | **3**, each with observable difference **0.000E+000** against non-zero bookkeeping difference |
| 5 Does continuity change a measurement or a bookkeeping? | **Only the bookkeeping** — physical difference **0.000E+000** in all 4 comparisons, bookkeeping up to **5.000** |
| 6 Classification | **COORDINATE CHOICE**; PHYSICAL LAW, EFFECTIVE LAW and GAUGE FIXING each refuted by a number |
| 7 `FailureCore(continuity)` | **0** observable + **3** structural |

---

## 0. The instrument, and why it is a three-way comparison

The natural way to test "does the law matter?" is to compare a run with the law against a run without it, and that
comparison is worthless on its own: the law's own bookkeeping appears in both, so **any** answer shows up. The
instrument here therefore carries the flux through **every readout in three forms**:

1. the flux **present** — the description G_079 derived;
2. the flux **displaced along its gauge orbit** — `J → J + 5` on every link, which is a **circulation**;
3. the flux **absent** — the transport layer deleted from the description.

All three are computed from the same state (the canonical uniform 24-cell occupation) through the same calls, and the
readouts are the four things the surviving sector actually measures: the **clock rate at a site**, the **potential at a
site**, the **gradient (acceleration) at a site**, and the **redshift between two sites**.

The result is that the three descriptions return **the same number to the last bit** — difference `0.000E+000` in all
four rows, in all three pairings. This is not an arithmetic coincidence of one state: the displacement chosen is a
circulation, and G_079 proved in **exact integer arithmetic** that a circulation leaves every divergence unchanged.
That identity is re-verified here on **every ring size** (4, 8, 16, 48, 96): the maximum absolute change of the
divergence is **0** in all of them, and the circulation's **nullity is 1** in all of them.

The consequence is the first substantive answer: **every readout of the sector is a function of the occupation
alone**. `ContinuityLawAudit.ObservablesReadingTheFlux()` — G_079's own measurement, re-run rather than cited —
returns **0**.

---

## 1. The flux requirement census (task 2)

The census is not assembled by hand: it runs the **29 surviving claims** of G_035 and G_068–G_080 through
G_081's own dependency map and asks, claim by claim, what value it consumes.

| What the claim needs | Count |
|---|---|
| the **occupation only** | **23** |
| the **operator's structure** (connectivity) | **6** |
| a **flux value** | **0** |

The identity `23 + 6 + 0 = 29` is asserted, and so are the two cross-checks against G_081: the
**operator-structure** group is exactly `ConnectivityDependencyAudit.FailureCore()` and the **occupation-only** group
is exactly `ConnectivityDependencyAudit.CoreWithoutConnectivity()`. The census therefore does not re-argue G_081 — it
**consumes** it.

The six that need more than the occupation need a **rank** (`n − c`), a **nullity**, a **free-mode count** or a
**reachability**. These are properties of the **substrate's structure**, and connectivity was classified **ASSUMED**
by G_080 and localised in exactly these six by G_081. So the transport layer's non-occupation content is **an input the
theory assumes**, not a value of any flux. **No claim in the surviving sector reads `J`.**

---

## 2. Removing the transport layer (tasks 3 and 5)

The layer is deleted outright: no flux, no divergence, no ledger — and the four quantities are recomputed.

| Quantity | With the layer | Without it | Difference |
|---|---|---|---|
| the clock rate at a site | `ClockOf(ρ₀)` | identical | **0.000E+000** |
| the redshift between two sites | ratio of clock rates | identical | **0.000E+000** |
| the acceleration (gradient) at a site | potential difference along a link | identical | **0.000E+000** |
| count conservation | reconstruction residual **0** | identical | **0.000E+000** — but **VACUOUS** |

The last row is where the audit is most careful about its own bookkeeping. Conservation does not become **violated**
when the layer is removed; it becomes **empty**, because there is no evolution left for it to govern. Reporting
`0.000E+000` for that row without the word would have been true and misleading, so the test asserts that the row
carries the word `VACUOUS` explicitly.

`PhysicalVersusBookkeeping()` then measures the separation directly, comparison by comparison: the **physical**
difference is `0.000E+000` in **every** row, the **bookkeeping** difference is non-zero in **3 of 4** rows (the flux
moves by **5.000**, the ledger residual by **0.030**), and the row that moves nothing is the transport layer **removed**
— which is exactly the row where there is no bookkeeping left to move.

---

## 3. The equivalence classes (task 4)

Three explicit classes of "same observables, different transport histories":

| Class | Representative A | Representative B | Observable difference | Bookkeeping difference |
|---|---|---|---|---|
| the flux's **gauge orbit** | `J` | `J + one circulation` | **0.000E+000** | **5.000** |
| the **source-annotated** bookkeeping | the change as a transport | the same change with a declared source | **0.000E+000** | **0.030** |
| **two histories to the same final state** | the prefix-sum flux | the same flux plus a constant | **0.000E+000** | the flux values |

Every class asserts observable difference `0.000E+000` **and** non-zero bookkeeping difference. This is the
quantitative form of the verdict: the transport description has **redundancy**, and the redundancy lives entirely on
the bookkeeping side.

---

## 4. The classification (task 6)

Four candidates, each decided by a **computed number** rather than by an argument:

| Candidate | Supported | The measurement that decides it |
|---|---|---|
| **PHYSICAL LAW** | **No** | **0** of the sector's observables read a flux, and **0 of 29** claims require a flux value |
| **EFFECTIVE LAW** | **No** | the law is **EXACT and not an approximation**: **3 of 3** operators of the class reconstruct with residual **0** in integer arithmetic — no regime boundary, no correction term |
| **GAUGE FIXING** | **No** | it does **not REMOVE a freedom**: the circulation survives it, with **nullity 1** on every ring size — so the law **CARRIES** a gauge rather than fixing one |
| **COORDINATE CHOICE** | **Yes** | what remains: a re-expression of the same evolution in which no observable changes |

The third refutation is the one that most cleanly separates this audit from a slogan. Calling the continuity equation
"a gauge choice" is common usage, and G_079 already showed the law carries a **circulation gauge**. A gauge **fixing**
would be a statement that **removes** such a freedom (`∇·J = 0` alone does not); the law **has** the freedom. The
`Classify()` method computes the verdict from these boolean predicates, so a change in any one measurement moves the
verdict — no literal reaches it.

---

## 5. `FailureCore(continuity)` (task 7)

**0 observable claims fail when the law is removed.** The three that do fail are **structural**, and the audit labels
them as such:

1. the **divergence identity** itself;
2. the **flux's gauge** (the circulation's orbit);
3. the **ledger**, which is a **detector** and not a measurement: its residual becomes **exactly the source**
   (`0.030` for a declared source of `0.03`), so the ledger **would** notice an imported source.

That third entry is the honest limit of the verdict, and it is why the audit does not say the law is *contentless*. The
law's one assertion beyond count conservation is the **absence of sources**; the audit that derived it (G_079) measured
that excluding them is a **choice** rather than a measurement, and that the exclusion is **falsifiable**. Falsifiable
and unrequired is a different thing from fundamental, and it is what the verdict says.

---

## Defects found while building this audit

Recorded rather than quietly fixed, per the project's rule that the first version's own errors are evidence.

1. **A comparison count printed as if it were a physical count** — the test originally asserted that **2** of the
   comparisons move the bookkeeping; the measured value is **3**. The defect was in the assertion, not the measurement,
   but it is the same class as G_081's `7 → 48` direction-count error: a number about the **instrument** quoted beside
   numbers about the **subject**.
2. **`0.000E+000` in the removal table was almost true and misleading.** The conservation row legitimately reads zero
   difference, because the quantity is the same zero in both descriptions — and it means something entirely different
   from the other three rows. The fix is the explicit `VACUOUS` note, asserted.
3. **The census could have been a restatement.** Reporting "23 occupation only / 6 connectivity" is vacuous unless it
   is G_081's map, so the test asserts the two groups are **exactly** `CoreWithoutConnectivity()` and `FailureCore()`.
4. **The circulation displacement was initially chosen as an arbitrary constant**, which would have made the orbit a
   property of the audit rather than of the law. It is the **circulation** — G_079's own object — and its
   divergence-invariance is asserted on **five** ring sizes rather than one.
5. **The classification initially carried the verdict as text.** `Classify()` and `StatusName()` now compute it, and
   the test asserts the verdict follows from the four predicates (G_027's rule).
6. **The "two histories" class initially compared two identical objects** — a vacuous self-comparison that would have
   passed forever. It now compares the prefix-sum flux against that flux plus a constant, with a non-zero bookkeeping
   difference asserted.

---

## What this does and does not establish

**Establishes.** That the continuity equation is **exact**, that it is **carried by no observable** of the surviving
sector, that its removal changes **0 of 4** recomputed quantities, that **0 of 29** surviving claims require a flux
value, that the transport description is redundant in **3** explicitly constructed equivalence classes where the
physical difference is zero and the bookkeeping difference is not, and that its failure core is **empty in
observables** (**0** observable, **3** structural).

**Does not establish.** That the continuity equation is **contentless** — it is exact, it is falsifiable, and its
ledger is a live detector of sources. That the law is **dispensable as notation** — it is the coordinate in which a
conserved evolution is written, and writing it another way is a choice, not a simplification. Or that the transport
layer's **structural** claims (rank, nullity, free modes, reachability) are bookkeeping: they are not, they depend on
the substrate's connectivity, and **connectivity is an assumed input** (G_080). The verdict is about the **law**, not
about those six.

**Out of scope (substrate).** This audit quotes **no spectral quantity**: every object it uses is a 24-cell ring with a
uniform occupation, and its statements are statements about operators, orbits and residuals. The D96/D96³ Spectral
Substrate Rule is therefore not engaged, and the audit says so rather than silently omitting the lattice.

---

## Where it stands

The chain G_079 → G_083 now reads:

- G_079: the continuity law holds, and it is equivalent to count conservation **given** connectivity;
- G_080: connectivity is **ASSUMED** — 0 of 4 primitives detect a cut;
- G_081: only **6 of 29** claims depend on it, all in the transport layer — a small failure core;
- G_082: the component count it acts through is **GAUGE-LIKE**, read by no local observable;
- G_083: the law itself is a **COORDINATE CHOICE**, exact, falsifiable, and required by no observable.

`ContinuityGaugeAudit.StatusName(Classify())` returns `"COORDINATE CHOICE"`, and the registry classifies the audit as
`TheoryClassification.Boundary` — consistent with G_081 and G_082, since what the audit establishes is a statement
about an **input** rather than a derived physical content.

What G_083 hands on is the residue: if the flux is bookkeeping, where does the **direction** in the theory come from?
Either something in the state or the update class **selects** the source-free, connected case — in which case the
transport layer is derived after all and this chain is superseded — or the sector's clock, redshift and acceleration
are carried entirely by the **occupation**, and the transport vocabulary is a description of a description. The
referee test is sharp and available: **any measurement not expressible in the occupation alone refutes G_083's
verdict**, and **any state whose evolution requires a source refutes the source-free premise**.
