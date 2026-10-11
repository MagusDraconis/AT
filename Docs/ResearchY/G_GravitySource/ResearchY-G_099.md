# ResearchY-G_099 — Orientation Origin Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_099_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/OrientationOriginAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_099`) · **Verdict:** **ORIENTATION IS DERIVED AS A FIELD, ASSUMED AS A TRANSPORT RULE**

**Full verdict:** ORIENTATION SURVIVES ONLY AS A PARITY OF DIFFERENCES — THE CONFIGURATION SUPPLIES ZERO CHIRALITY BITS, AND THE TRANSPORT DIRECTION REMAINS ASSUMED.

---

## Question

G_097 measured that motion is **emergent** while its kinematics is **assumed** (the centroid shift is exactly zero, the
width grows as **√t**); G_098 measured that the transport/diffusion ratio is **zero** for every surviving mode and —
sharpest of all — that the surviving **link set does not represent a direction**.

> **Can orientation emerge from the surviving AT structure, or is orientation independent boundary information?**

**Constraints:** every count, dimension, rank and multiplicity names its object and is computed on the **96-cell ring
alone**; the **cubic substrate forbidden**; no new primitive, field, source term, topology axiom, action, Lagrangian,
carrier, orientation tag or external coordinate introduced. **A negative result is acceptable.** **Prefer refutation.**

---

## Answer

**THE STRUCTURE DOES NOT SUPPLY A HANDEDNESS. It supplies a *direction at a place* — a parity, not a chirality.**

| Required output | Measured |
|---|---|
| **Orientation inventory** | **8** objects · **3** can tell A→B from B→A · **5** derived — every distinguishing one is a potential difference or the unobserved flux |
| **Symmetry-breaking table** | the state preserves **2** of **192** automorphisms (1 rotation, 1 reflection) and breaks **190**; the law preserves **192 of 192** (worst residual **2.220E-016**) |
| **Chirality-bit table** | **0** bits in total — from the configuration, the spectrum, the law, conservation and the observable set, measured source by source |
| **Observable change count** | reversal sweep over **96** maps: **1** changes nothing, the change count ranges **0 … 6**, largest single change **8.251E-001** |
| **Remaining orientation information** | **3** of **5** quantities survive the removal of every label, and **3** of them are parity-odd |
| **ΔI from one added orientation bit** | **0** new observables · **1** transport quantity (ratio **0.368**) · **0** degeneracy reduction · **ΔI = 0** |
| **Classification** | FIELD — **DERIVED** · TRANSPORT RULE — **ASSUMED** · CHIRALITY of the configuration — **ASSUMED** (measured absent) · ADDED global bit — **BOUNDARY** |

---

## 1. The orientation inventory (task 1)

| object | tells A→B from B→A | derived | content | status |
|---|---|---|---|---|
| the potential's **gradient** (the acceleration) | **yes** | yes | 1.069E-003 | DERIVED |
| the potential **difference** between two sites | **yes** | yes | 1.567E-001 | DERIVED |
| the surviving **state's configuration** (its chirality) | **no** | no | **6.661E-016** | **ASSUMED** |
| the **flux** (the transport's own variable) | **yes** | yes | 1.000E+000 | BOUNDARY |
| the **link set** (adjacency) | no | no | 0 | ASSUMED |
| the **update law** (the Laplacian action) | no | yes | 0 | EMERGENT |
| the **conservation** statement | no | yes | 0 | EMERGENT |
| the **phase** (the theta sector) | no | no | 0 | BOUNDARY |

**The distinction the audit keeps throughout, because collapsing it would be the easy error:** an object can carry
orientation as a **field** (a gradient distinguishes the two directions at a place) while carrying none as a
**transport rule** (nothing says which way count should move). G_098 measured the second; this audit measures both and
reports them **separately**. **"Derived" is computed, never typed:** a carrier is derived exactly when its content is
computed from the surviving primitives, which is an adjudication (`OrientationOrigin`) with its own measurement, so no
boolean column is a declaration the audit chose.

---

## 2. The symmetry-breaking measurement (task 2) — and the audit's decisive refutation

The cycle's automorphism group is the dihedral group: **96 rotations** and **96 reflections**, **192** in total. A
reflection is exactly the map that swaps A→B for B→A, so whether orientation is represented is not a matter of
opinion.

| structure | preserves | breaks | reading |
|---|---|---|---|
| the surviving **state** | **1** rotation, **1** reflection | **95**, **95** | the state is not translation-invariant — but see below |
| the **law** | **192 of 192** | **0** | worst residual **2.220E-016**: the operator breaks nothing |

**The draft's story died here.** The first reading was *"the state breaks 190 automorphisms, so the configuration
distinguishes A→B from B→A"* — and its own measurement refutes it: **`max |ρ_i − ρ_(N−i)| = 6.661E-016`**, which is
**below the equality rule 1e-12**, so the surviving state is **PARITY-EVEN**. The one reflection it preserves is the one
through **its own axis**, and the **190 broken elements measure the field's LOCALISATION, not a handedness** — they are
the reason the gradient has a direction at all. **The configuration supplies no chirality bit.**

A second measurement of the same kind: the audit's first **reversal census chose the reflection the state happens to
preserve** and therefore read **"0 of 8 readouts change"** — a **convention masquerading as a measurement**. A ring has
**96** orientation-reversing maps, so the audit now **sweeps them all** and reports a **range**.

---

## 3. Reveral applied to every observable (task 3)

| readout (at the strongest reversal) | original | reversed | change | frame-invariant |
|---|---|---|---|---|
| the metric's time component | −1.368E+000 | −5.429E-001 | **8.251E-001** | no |
| the clock rate at a site | 1.170E+000 | 7.368E-001 | 4.328E-001 | no |
| the clock potential at a site | 1.567E-001 | −3.054E-001 | 4.621E-001 | no |
| the redshift of a site from its own rate | −1.450E-001 | 3.572E-001 | 5.022E-001 | no |
| the **source acceleration** of the field | 1.634E-002 | 1.634E-002 | **0** | **yes** |
| the **second-order AT/GR ratio** at the target | 1.098E+000 | 1.098E+000 | **0** | **yes** |
| the **gradient along a link** | 2.677E-004 | −1.069E-003 | **1.337E-003** | **yes** |
| the **redshift between two sites** | −1.450E-001 | 3.572E-001 | **5.022E-001** | **yes** |

Over all **96** reversals: **1** leaves every readout unchanged, the change count ranges **0 … 6**, and the largest
single change is **8.251E-001**. **The effect of a reversal depends on WHICH reversal is meant** — which is precisely
why a single-reflection census would smuggle in a convention.

---

## 4. The chirality-bit table (task 4)

| source | bits | how it is decided |
|---|---|---|
| the state (its configuration) | **0** | `max |ρ_i − ρ_(N−i)| = 6.661E-016` is **below** the equality rule: the state is **PARITY-EVEN** and preserves the one reflection through its own axis |
| the spectrum | 0 | G_098 measured `max |symbol(k) − symbol(N−k)| = 2.665E-015`: the dispersion is **even** |
| the update law | 0 | measured here: it commutes with **192 of 192** automorphisms |
| the conservation statement | 0 | a sum is invariant under every relabelling |
| the observable set (as functionals) | 0 | the readouts change only because the **state** is relabelled, and the sweep shows the range |

**Total: 0 bit.** The sector's chirality content is **zero**, measured source by source — not "unknown".

---

## 5. Parity-odd quantities and their survivors (task 5)

| quantity | parity-odd | frame-invariant | survives the reductions |
|---|---|---|---|
| the field's acceleration (a gradient) | **yes** | yes | **yes** |
| the gradient along a link | **yes** | yes | **yes** |
| the redshift between two sites | **yes** | yes | **yes** |
| the site redshift | no | no | no |
| the clock rate, the potential and G₀₀ at a site | no | no | no |
| the **AT/GR ratio at the target** | no | **yes** | **yes** |

**3 of 6 are parity-odd and all 3 survive.** But the audit refuses the easy reading: **an odd functional is not a
handedness** — the three survivors are **two-site differences**, and a difference is odd under reversal **by
construction**. What the readouts show is that a **signed direction exists wherever the potential is not constant**;
they are not evidence that the source is chiral. The sector's **unique prediction is even**, which is why the
second-order AT/GR ratio carries no parity at all.

---

## 6. Without labels (task 6)

Of **5** quantities recomputed with every label, index and ordering convention removed, **3** survive and **3** of them
are parity-odd:

| quantity | survives without labels |
|---|---|
| the field's acceleration (odd, frame-invariant) | **yes** |
| the gradient along a link (odd, frame-invariant) | **yes** |
| the redshift between two sites (odd, frame-invariant) | **yes** |
| the centroid of a pattern (the transport measure) | **no** — it needs an **index** |
| the flux (a direction no measurement sees) | no |

**A relative position is not a label:** comparing two sites survives, indexing one does not.

---

## 7. The reduction test (task 7): what one added orientation bit buys

| gain | count | measure |
|---|---|---|
| new observables enabled | **0** | 0.0000 |
| new transport quantities enabled | **1** | **0.3682** |
| reduction of the 48! degeneracy (G_097) | **0** | 0.0000 |
| information gain in observable terms | **0** | 0.0000 |

The bit is added to the **link set**, which is where G_098 found the orientation missing. It buys **no observable**
(an added orientation does not change the **state**), reduces **no** degeneracy (the transfers that commute with the
law still commute), and moves the transport/diffusion ratio from **0.000** to **0.368** — a quantity **no observable
reads**. **Gain and cost are both zero, measured.**

---

## Defects found while building this audit

1. **A convention masquerading as a measurement.** The first reversal census chose the single reflection the state
   happens to preserve and read **"0 of 8 readouts change"**, which would have concluded that reversal is a no-op. A
   ring has **96** orientation-reversing maps; the audit now **sweeps all of them** and reports the range, and the
   suite asserts that the sweep is not a single reflection.
2. **The draft's own narrative refuted by the draft's own measurement.** A carrier row asserted *"the state is not
   parity-even … a chiral configuration is a direction-bearing object"* while the measurement beside it read
   **6.661E-016**. The audit now reports the state as **parity-even to the rounding**, supplying **0** bits, and the
   verdict, the classification and the criteria were rewritten to match the number rather than the story.
3. **A literal verdict beside a computation.** `ChiralityBits`' state row carried the fixed narrative *"carries ONE
   bit"* next to a computed `chirality > Tol ? 1 : 0` that evaluated to **0** — a comment standing in for the result.
   Both branches are now computed, and the row says **MEASURED ZERO**.
4. **A record boolean supplied as a literal** (`OrientationCarrier.Derived`), caught by **G_027**'s source scan. Fixed
   by adjudicating **where the content comes from** (`OrientationOrigin`: computed, supplied, absent) and **computing**
   the boolean from it — not by registering an exception.
5. **A `Single()` keyed on a shared prefix** matched two sources — *"the state (its configuration)"* and *"the
   conservation **state**ment"* — the recurring trap this project has hit before. The assertion keys on the prefix.
6. **A summary that overclaimed.** `ParityOddCounts` read *"so the sector does contain derived, observable chirality"*
   — refuted by this audit's own finding that the three survivors are differences. It now states that the readouts
   carry **parity**, not handedness.

---

## What this does and does not establish

**Establishes:** that the ring's automorphism group is enumerated and the surviving state tested against **all 192** of
its elements; that **neither the configuration nor the operator supplies a chirality bit** (the state is parity-even
to the rounding; the law commutes with every automorphism); that the readout change under reversal is a **range** over
**96** maps; that the sector's chirality content is **0 bits**, measured source by source; that **three parity-odd
readouts survive** the gauge and coordinate reductions and that all three are **differences**; and that **one added
orientation bit buys no observable and reduces no degeneracy**.

**Does not establish** that the sector's operations are undirected — an operation **is** directed, and G_097 measured
that it moves a pattern. Nor that the two orientations are physically indistinguishable: what is established is that
the theory's **vocabulary carries no transport rule**, so the choice is the operator's. **Nor does it claim a new
result for the gradient**: the gradient has been an observable since G_075; what is new is that its **parity is
measured**, that it **survives every reduction**, and that the two questions — *orientation as a field* and
*orientation as a transport rule* — are **separated rather than conflated**, which is what stops either the earlier
"chirality is derived" or the later "orientation is absent" from being overclaimed.

**Why this is not a restatement of G_098:** G_098 measured that the direction is **not represented** and that the
transport/diffusion ratio is **zero**. This audit asks the **parity** question over the ring's **whole automorphism
group**, measures how many bits each source supplies, and **refutes its own initial answer** in the process: the
configuration is **not** chiral, so what remains observable is a **signed field**, and the one bit G_098 found missing
is confirmed **absent rather than hidden**.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, densities = `MapRedundancyAudit.State`, links =
`LinksOf(96, 1)`; the automorphism group is the **dihedral group of the cycle** (order **192**). The **cubic substrate
is forbidden and is not named in the suite**. **Equality rule:** **1e-12** on a computed value, **exact** equality
where two built configurations are compared; `ε = 0.25` is the same stated evolution step G_097 and G_098 used, so the
three audits are **comparable**. **No spectral quantity is quoted.**

**Guards:** `Y_G_033` — `without` **67 → 68**; `Y_G_035` — **99 → 100**, survives **85 → 86**. Registry entry:
**derived**.

**What would change the verdict:** an **orientation carried by the link set** (which would make the operator
asymmetric); a surviving **rule that fixes which transfer happens** (which would derive the transport direction); an
**observable that reads the flux** (which would give the missing bit a cost); or a **chirality of the configuration
above the equality rule**, which would make the state itself carry a handedness and turn the field's direction into a
property of the source.
