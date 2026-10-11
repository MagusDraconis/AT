# ResearchY-G_100 — Orientation Coupling Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_100_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/OrientationCouplingAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_100`) · **Verdict:** **ORIENTATION-FIELD COUPLING IS DECOUPLED — THE ORIENTATION FIELD IS CARRIED, NOT ACTUATED**

**Full verdict:** the field is COUPLED as an observable-only effect and DECOUPLED as a source of transport and as a dynamical input.

---

## Question

G_097 measured that the sector's law **diffuses** (the width grows as **√t**, the centroid shift is zero, the mode
symbols are non-positive); G_098 measured that directed transport is **absent** and that the surviving transport
structure does not even **represent** a direction; G_099 measured that the orientation **field** is **DERIVED** and
observable while the **chirality** is **ASSUMED** and the **transport rule** is **ASSUMED**.

> **Does the derived orientation field couple to surviving dynamics strongly enough to generate directed
> propagation?**

**Constraints:** every count, dimension, rank and multiplicity names its object and is computed on the **96-cell ring
alone**; the **cubic substrate forbidden**; no new primitive, field, source term, topology axiom, action, Lagrangian,
carrier, vector structure, orientation tag or transport rule introduced. **A negative result is acceptable.** **Prefer
refutation.**

---

## Answer

**THE FIELD IS CARRIED, NOT ACTUATED.** It is an observable — and it is connected to nothing: the transport measure it
would have to move is zero at the equality rule on **every** member of the family the field parametrizes.

| Required output | Measured |
|---|---|
| **Orientation-field inventory** | **6** quantities · **3** read by an observable · **0** dynamical · **0** with independent degrees of freedom |
| **Coupling table** | descriptive: **Pearson r = 0.909**, signed overlap **0.912**, peak normalized product **1.000** — dynamical: **0.000E+000** on all three law channels |
| **Sensitivity table** | **7** members (both signs of the field), **7** widths defined, **max \|S\| = 5.334E-013** against the **1e-12** rule, largest numerator **1.066E-014** |
| **Orientation-reversal table** | **2** LAW objects, **0** changed; **5** state outputs, **1** changed — and that one is the density inversion's own second-order term |
| **Observable-change count** | at the fixed site **1** of **8** (amplification **0.249 < 1**); at the **mirrored** sites **0** of **8** — bit-identical |
| **Observable / evolution / transport loss** | **2** observables · **0** law objects · **0** transport quantities |
| **Final classification** | **DECOUPLED** as a transport source and as a dynamical input; **COUPLED** as an observable-only effect |

---

## 1. The orientation-field inventory (task 1)

| quantity | indep. dim | observable | dynamical | magnitude | status |
|---|---|---|---|---|---|
| the signed potential difference along a link (the orientation field) | **0** | **yes** | no | 7.438E-002 | DERIVED |
| the field's acceleration (the averaged gradient) | **0** | **yes** | no | 7.438E-002 | DERIVED |
| the field's content `O = Σ|d_i|` | **0** | no | no | 2.001E-001 | DERIVED |
| the odd part of the configuration | **0** | no | no | 1.250E-001 | ASSUMED |
| the flux, measured by its **reader count** | **0** | no | no | **0** | BOUNDARY |
| the parity-odd surviving readouts | **0** | **yes** | no | 1.450E-001 | DERIVED |

**The dimension column is the audit.** A quantity that is a **functional of the state** has **no degrees of freedom of
its own**: it cannot be varied while the state is held fixed, so it cannot be a control variable and cannot drive
anything. **All six are derived and none is independent** — and that is measured, not asserted: §3 measures the
partition the field defines and finds its one free direction to be the count's own **scale**.

---

## 2. The coupling table (task 2)

| channel | max | min | mean | equality rule |
|---|---|---|---|---|
| the field's magnitude against the update's magnitude, cell by cell | 1.000 | 0.000 | **0.0191** | 1e-12 on the measured values |
| the signed overlap between the field and the update | 0.9123 | 0.9123 | **0.9123** | cosine of two measured profiles |
| the update **operator** against the field | **0.000E+000** | 0.000E+000 | 0.000E+000 | exact: a state-independent map |
| the **mode symbol spectrum** against the field | **0.000E+000** | 0.000E+000 | 0.000E+000 | exact: a state-independent spectrum |
| the **conservation** statement against the field | **0.000E+000** | 0.000E+000 | 0.000E+000 | exact: the update's total is invariant |

**A strong descriptive coupling beside a zero dynamical one, measured on the same state, is the whole shape of this
audit.** The field and the update track each other closely (**r = 0.909**, cosine **0.912**) because a first difference
and a second difference of a localized bump live in the same few cells — and the three channels that would make the
coupling **dynamical** are recomputed and read **exactly zero**. The operator is a function of
`(cells, links)` with **no state argument**: its **9 216** matrix entries, its **symbol spectrum** and its conservation
statement are the same whatever the field does.

---

## 3. The partition by equal orientation field (task 3)

The field fixes the potential up to one additive constant by **telescoping**, hence the state up to a positive
**scale**. The measurement is taken on **different states**, so it is not an identity read back:

| member | scale | field residual | evolution residual |
|---|---|---|---|
| the state rebuilt from the field alone (**an identity, reported as one**) | 1.000 | 0.000E+000 | — |
| a **different** state with the same field (scale halved) | 0.500 | **2.776E-017** | **0.000E+000** |
| a **different** state with the same field (scale doubled) | 2.000 | **2.776E-017** | **0.000E+000** |
| a state that is **not** in the partition (the even part alone) | — | 3.926E-002 | 2.500E-001 |

**The partition has dimension 1, and that dimension is the count's own scale — the coordinate G_085 named.** Inside it
the evolution differs by **exactly** that scale (the Laplacian is linear), so the field adds **no second dynamical
mode**. The last row is the control: removing the odd component leaves the partition, and §7 measures what that costs.

---

## 4. The orientation reversal (task 4)

The field cannot be reversed while the state is held **literally** fixed — it is a functional of the state — so the
audit uses **two faithful operations** and says which is which:

* **the ring reflection** — a relabelling the law commutes with (G_099);
* **the density inversion** `ρ → c/ρ` with `c = mean(ρ)/mean(1/ρ)` — flips the potential's sign **exactly**, preserves
  the count's scale, and is **non-linear**.

| quantity | scope | operation | as is | reversed | change |
|---|---|---|---|---|---|
| the orientation field's sign (same index) | Field | inversion | 7.438E-002 | 7.438E-002 | **1.388E-017** |
| the orientation field's sign (reflected index) | Field | reflection | 7.438E-002 | 7.438E-002 | **0.000E+000** |
| the update **operator** (9 216 entries) | **LAW** | either | 0 | 0 | **0.000E+000** |
| the **parity channels** (even→odd, odd→even) | **LAW** | either | 0 | 0 | **0.000E+000** |
| the excess transport (centroid shift over 8 ticks) | state output | reflection | 7.105E-015 | 0.000E+000 | 7.105E-015 |
| the excess transport (centroid shift over 8 ticks) | state output | inversion | 7.105E-015 | 7.105E-015 | **0.000E+000** |
| the diffusion width growth (8 ticks) | state output | reflection | 1.319 | 1.319 | **0.000E+000** |
| the diffusion width growth (8 ticks) | state output | inversion | 1.319 | 1.300 | 1.866E-002 |
| the transport measure T | state output | either | 5.388E-015 | 0.000E+000 | 5.388E-015 |

**0 of 2 LAW objects change.** The **reflection** — the exact relabelling — moves **every** state output by
**0.000E+000**, including the width growth. The **inversion** leaves one visible residue, **1.866E-002**, and it is the
**transformation's own second-order term** (1/ρ is not −ρ), not a dynamical change: it is reported as a **state
output** for exactly that reason. The field's sign flip is **exact to the rounding** under the inversion
(**1.388E-017**, a ln/exp round trip) and **exact** under the reflection (**0.000E+000**, because the same two values
are subtracted in the opposite order). And the law leaks **no parity into its opposite** in either direction.

---

## 5. The transport sensitivity (task 5)

| member | orientation content O | peak | centroid shift | width growth | T |
|---|---|---|---|---|---|
| skew 0.00 | 0.1488 | 7.438E-002 | 0.000E+000 | 2.000 | 0.000E+000 |
| skew 0.25 | 0.1620 | 7.438E-002 | 1.066E-014 | 1.544 | 6.904E-015 |
| skew 0.50 | 0.1749 | 7.438E-002 | 0.000E+000 | 1.426 | 0.000E+000 |
| skew 0.75 | 0.1876 | 7.438E-002 | 0.000E+000 | 1.360 | 0.000E+000 |
| skew 1.00 | 0.2001 | 7.438E-002 | 7.105E-015 | 1.319 | 5.388E-015 |
| the mirror image (−field) | 0.2001 | 7.438E-002 | 0.000E+000 | 1.319 | 0.000E+000 |
| the density inversion (−field) | 0.2001 | 7.438E-002 | 7.105E-015 | 1.300 | 5.465E-015 |

**max |dT/dO| = 5.334E-013** over **7** members — **under the 1e-12 rule** — with **7 of 7** widths defined and the
largest numerator **1.066E-014**. The derivative is zero **at the equality rule and not merely small**: the first
moment of the update vanishes **identically** by summation by parts (`Σ i (Lρ)_i = 0` for every ρ), so what remains is
that vanishing's **rounding**, ≈ 1e-16 **relative to the pattern's own extent** of ~48 cells.

**Two measurement choices are stated because each could have hidden the answer:** the sweep is keyed on the field's
**content**, because the **peak is constant** over this family (7.438E-002 at **every** member — the second bump never
displaces the maximum); and every member's pattern is **sign-definite**, because a second moment exists only for a
sign-definite pattern (§ Defects).

---

## 6. The surviving observables under an orientation-dependent evolution (task 6)

**Two readings, because one of them was the first draft's mistake.**

**(a) read at the sector's FIXED canonical site:** **1** of **8** readouts changes under the reversal — the two-site
redshift, by **7.722E-002** — and its difference **CLOSES**: after 8 ticks it is **1.925E-002**, an amplification
factor of **0.249**. **0** readouts amplify. The reason only one readout notices is the coordinate: the pattern moved
to the opposite side of the ring, and the sector's readouts are read at site 0.

**(b) read at each orientation's own MIRRORED site:** **0** of **8** readouts differ, at t = 0 **and** after 8 ticks:
the two orientations are **bit-identical**. **This is the stronger statement and the correct one** — the reversal is a
**relabelling**, and a relabelling-covariant measurement cannot see it at all. What reading (a) measures is the
**coordinate** the reversal moves.

**And nothing grows.** A dynamics that generated orientation would amplify it; here the one visible difference closes
by a factor of **4**.

---

## 7. The reduction test (task 7)

The orientation information is removed by projecting the state onto its **even part** — the scale and every parity-even
feature kept, the odd component set to zero:

| object | lost | residual |
|---|---|---|
| the surviving observables | **2** of 8 | — |
| the **LAW** (the update operator) | **0** | 0.000E+000 |
| the **LAW** (the mode symbol spectrum) | **0** | 0.000E+000 |
| the update's **STATE** term (not a law object) | 1 | 2.500E-001 |
| the **transport quantities** | **0** | ≤ 1e-12 |

**The two observables lost are the orientation readouts themselves** (the along-a-link gradient and the two-site
redshift — the parity-odd functionals G_099 catalogued), and the sector's **unique prediction, the second-order AT/GR
ratio, is among the survivors**. The **law loss is zero**: no rate, no damping and no frequency depends on the field.
The **transport loss is zero** because there was no transport to lose. The state-term residual is the removed
component, carried by the same operator at the same rate as everything else — which is why it is reported separately
rather than counted as an evolution loss.

---

## Defects found while building this audit

1. **A pattern measured against the state's own mean has total zero by construction.** The audit's first draft defined
   the pattern as `ρ − mean(ρ)`, which sums to zero for **every** state, so the centroid was **undefined**: the draft
   read a "centroid shift" of **1.0** and a **NaN** width. Fixed by measuring the flat background as the state's own
   value **diametrically opposite its own maximum** — a two-pass measurement, fixed once and held over the ticks.
2. **A sweep keyed on the field's peak did not sweep.** `max |d_i|` is **7.438E-002 at every member** of the family,
   because the second bump never displaces the maximum, so `dT/dO` would have divided by a constant. Fixed by keying
   the derivative on the field's **content**, which moves from **0.1488** to **0.2001**.
3. **A second moment exists only for a sign-definite pattern.** A negative skew put a deficit two cells from the bump,
   the second moment went negative, and the width came back **NaN** — reported as a number until it was checked. Fixed
   by sweeping a sign-definite family (five skews plus the two reversals) and by carrying a **`WidthIsDefined`** flag.
4. **A state-output change was counted as a dynamical change.** The density inversion's own second-order residue
   (**1.866E-002**) made the success criterion read **MET**. Fixed by separating **LAW objects** from **STATE OUTPUTS**
   in the record, so the criterion is decided by the law and the transport — and the classification now reports both
   counts.
5. **A record boolean supplied as a literal** (`IsADynamicalObject`) was replaced by a **scope enum**
   (`ReversalScope`), with the derived booleans computed from it, so **G_027**'s source scan stays green without a
   registry exception.
6. **The first task-6 reading drew a conclusion from a coordinate.** Holding both orientations at the same site and
   finding 1 of 8 readouts different suggested that the readouts are nearly orientation-blind; reading each
   orientation at the **mirrored** site shows all 8 **bit-identical**, which is the stronger and correct statement.
   Both readings are now reported, and the phrasing that depended on the first was removed.

---

## What this does and does not establish

**Establishes:** that every derived orientation-field quantity surviving G_099 has **zero independent degrees of
freedom**; that the field correlates **strongly** with the update (r = 0.909, cosine 0.912) so the descriptive coupling
is not marginal and therefore cannot be the evidence; that the operator's **9 216** entries, the symbol spectrum and
the conservation statement are **state-independent**; that the law leaks **no parity** into its opposite; that the
partition by equal field has dimension **1**, measured on two **different** states, and that its free direction is the
count's **scale**; that **max |dT/dO| = 5.334E-013** over a seven-member sweep of the field's content, i.e. zero at
the **1e-12** rule, the numerator being the rounding of a first moment that vanishes **identically** by summation by
parts; that the two orientations are **bit-identical in all eight readouts** when each is read at its own mirrored
site; that the one visible difference **closes** (×0.249); and that removing the orientation costs **2** observables,
**0** law objects and **0** transport quantities.

**Does not establish** that the orientation field is unphysical or unobservable — it **is** an observable, and G_099
established that; nor that the odd component is inert **as a state component**, since the Laplacian carries it exactly
as it carries every other component, at the same rate and with no parity leak. What it establishes is that the odd
component is **not a variable**: it cannot be set independently of the state, and the transport measure it would have
to move is zero at the rule for **every** member of the family rather than for the one member the chain happened to
test. **A negative result is acceptable, and this one is measured rather than argued.**

**Why this is not a restatement of G_098 or G_099:** G_098 measured that the direction is **not represented** and that
T is zero for the surviving modes; G_099 measured that the orientation **field** is derived and the chirality is not.
Neither asked whether the field, once derived and observable, **acts** — and both left open the reading on which a
derived, observable, parity-odd field might yet generate directed propagation. This audit closes that reading by
sweeping the field through an admissible family and **differencing** the transport, which is the only form in which
"strongly enough to generate" is a measurement rather than a hope.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, links = `LinksOf(96, 1)`, densities =
`MapRedundancyAudit.State`. The orientation field is the **signed potential difference along each of the ring's 96
cyclic links**. The **cubic substrate is forbidden and is not named in the suite**. **Equality rules:** **1e-12** on a
computed value; **exact** equality where a telescoping or a summation-by-parts identity holds; `ε = 0.25` is the same
stated step G_097, G_098 and G_099 used, so the four audits are **comparable**. **No spectral quantity is quoted.**

**Guards:** `Y_G_033` — `without` **68 → 69**; `Y_G_035` — **100 → 101**, survives **86 → 87**. Registry entry:
**derived**.

**What would change the verdict:** a **state-dependent law** (an update that took the field as an argument, which
would make the operator's matrix move under reversal); a **transport quantity with a non-zero numerator** on some
member of the family (which would give `dT/dO` something to carry); a **parity leak** in the update (which would let
the odd component feed the even one); or an **observable that reads the flux**, which would give the direction a cost
and turn a coordinate difference into a measurement.
