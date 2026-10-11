# ResearchY-G_089 — Manipulation Channel Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_089_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ManipulationChannelAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_089`) · **Verdict:** **ONE ACTUATABLE CHANNEL** (registry: Derived)

---

## Question

**Working hypothesis, ASSUMED TRUE:** local manipulation of time and/or gravity **is** physically possible. The audit
does **not** ask whether it exists; it asks **what physical channel would permit it**, inside the surviving sector's own
vocabulary (Difference, Actualization, ρ, `K(ρ)`, all surviving audits). Arguing from previous negative verdicts is
forbidden, as is adding primitives immediately or assuming impossibility.

**Success criterion:** a surviving channel not already eliminated by G_077–G_088.
**Failure criterion:** every logically possible channel collapses back to the existing kernel without adding a
controllable variable.

---

## Answer

**ONE ACTUATABLE CHANNEL — AND IT IS THE THEORY'S OWN ELEMENTARY OPERATION. THE FAILURE CRITERION IS MET FOR PATHWAYS,
AND THE SUCCESS CRITERION ONLY IN THE SENSE THAT THE CHANNEL THAT WOULD BE NEEDED IS IDENTIFIED.**

| Task | Result |
|---|---|
| 1 Inverse problem | a **1 %** clock change needs a **3.030 %** local count change — the channel is **DAMPED** by `d/(1+cA)` |
| 2 Channel search | of **8** channels **1** is actuatable, **3** control without an actuator, **4** are not independent controls |
| 3 Hidden variables | **3** eliminated structures still **control** the kernel — and all **3** are unobservable **and** unactuatable |
| 4 Control vs observable | **YES in principle** (unobservable controls exist) — but among the **controls** nothing is actuatable |
| 5 Energy scaling | the ledger prices a redistribution at **0** and a **source** exactly (residual `0.0300` = the source) |
| 6 Technology | **impossible** (conserved inputs) · **principle-only** (the curvature) · **astrophysical-only** · **laboratory-feasible** (ordinary gravity) |
| 7 Minimal pathway | human action → count transfer → density → kernel → clock/gravity, bounded by conservation, damping and the ledger |

---

## 1. The inverse problem, solved (task 1)

A fractional clock change `Δ` at a site requires a fractional change in the **local count** of
`(1 + Δ)^(d/(1+cA)) − 1`, computed in closed form (the hazard the numerical rules warn about is a difference of nearly
equal terms, which the closed form avoids entirely):

| target clock change | required local count change | density per unit clock |
|---|---|---|
| **1 %** | **3.030 %** | **3.0301** |
| 10 % | 33.100 % | 3.3100 |
| 100 % | 700.000 % | 7.0000 |

**The density is the lever arm and the clock is DAMPED**: the density must move *more* than the clock by about `d`. (The
first version reported this ratio as a "gain below one" and the measurement refuted it — recorded as a defect.)

**The gravity side:**

| direction | acceleration change | clock change | the class contains it |
|---|---|---|---|
| adding a constant potential everywhere (**the uniform direction**) | **5.551E-017** (the rounding of a zero) | 1.000E-001 | **NO** |
| moving count between neighbouring cells (**a differential change**) | **6.734E-003** | 0.000E+000 | **yes** |

So a gravity change requires a **differential** change, and the **uniform** direction — which would shift every clock at
once — is exactly the one the class does not contain.

---

## 2. The channel search (task 2)

| channel | controls the kernel | directly observable | actuatable | status |
|---|---|---|---|---|
| local density | yes | yes | **yes** | **ACTUATABLE** |
| density correlation | no | yes | no | not a control |
| density variance | no | yes | no | not a control |
| density topology | **yes** | no | no | **control without an actuator** |
| kernel curvature | **yes** | no | no | **control without an actuator** |
| kernel drift | no | yes | no | not a control |
| phase structure | no | no | no | **excluded by arity** |
| boundary conditions | **yes** | no | no | **control without an actuator** |

---

## 3. The hidden variables still act (task 3)

A quantity removed as **observationally redundant** may still be able to **act** — that distinction is the task:

| quantity | controls | observable | actuatable |
|---|---|---|---|
| the link structure (connectivity) | **yes** | no | no |
| the component count | **yes** | no | no |
| the transport layer (the class's operations) | no | **yes** | **yes** |
| the flux itself (the transport's variable) | no | no | no |
| the kernel's curvature | **yes** | no | no |

**Three** eliminated structures still control the kernel — the link structure (0 of 4 primitives detect a cut), the
component count (changed by the class in **0** operations) and the kernel's curvature (which **is** `c`, the second
derivative, and which nothing measures in situ) — and **all three are unobservable *and* unactuatable**.

---

## 4. Control versus observable (task 4)

**Measured counts:** controlling the kernel **3**, of which unobservable **3**; actuatable **1**, of which unobservable
**0**; actuatable and observable **1**.

**So the answer is YES in principle and NO within the class:** quantities *do* exist that are unobservable and control
the kernel, but **among the controls nothing is actuatable** — every actuatable quantity is one the observables read,
because the actuator is a **transfer of count**, and a transfer *is* a change in ρ, which the observables read. The
unobservable controls are exactly the **conserved inputs**, and **conservation is what makes them uncontrollable**.

---

## 5. Energy scaling (task 5)

In the sector's own terms: a **redistribution costs 0** in the ledger (it conserves the count), while a **source** would
be **detected exactly** — the residual becomes the source (`0.0300` for a declared `0.0300`) — rather than produced. The
class's own operators reconstruct their changes with residual `0.000E+000`. The **practical** cost is therefore the
calibration's: the ordinary cost of moving mass-energy, which is the ordinary gravitational shift.

| regime | gain (damping) | 1 % clock | 10 % clock | 100 % clock |
|---|---|---|---|---|
| a compact object's surface | 1/3 | 3.030 % | 33.10 % | 700.0 % |
| a shallow (laboratory-like) potential | 1/3 | 3.030 % | 33.10 % | 700.0 % |

At leading order the requirement is the same fraction in both regimes — **the channel is scale-free in the theory and
priced only by the calibration**.

---

## 6. Technology classification (task 6)

| classification | channel | why |
|---|---|---|
| **impossible** | the conserved inputs — the link structure, the component count, the total | an operation that changes them is not in the class |
| **principle-only** | a change in the kernel's **curvature** | effective and (until measured) unobservable — but it is a parameter of the **law** |
| **astrophysical-only** | large redistributions of count | the clock and the source are one object: ordinary gravitational effects at stellar scale |
| **laboratory-feasible** | the ordinary gravitational shift of a local redistribution | measured routinely — and it **is** the kernel, so it confirms the channel rather than manipulating the clock independently of the density |

---

## 7. The minimal manipulation pathway (task 7)

| step | what happens | limit |
|---|---|---|
| a human action | moves count along the substrate's links | it must be a **transfer**: no operation creates count |
| the physical variable | the local density changes by the inverse-problem amount | the **damping** `d/(1+cA)`: the density must move ~3× the clock |
| the kernel | `K(ρ)` reads the new density — clock and source are one object | the **ledger is exact**: count that appeared untransferred would show as a residual equal to the source |
| the observable change | the clock and any gravity difference follow | bounded by the three above: it is the sector's own dynamics, not a new channel |

---

## Defects found while building this audit

1. **The inverse-problem ratio was reported as a "gain below one" and the measurement refuted it.** The quantity
   `Δρ/Δclock` is **≈3**, not <1: the density must move **more** than the clock. The audit now reports the **damping**
   `d/(1+cA)` and states that this is what makes a manipulation *expensive* rather than cheap. A sign-and-direction
   error of exactly the class this chain keeps finding.
2. **The control-versus-observable count was taken over ALL rows instead of the CONTROLS**, so the transport layer and
   the flux — which are actuatable and not directly observable — landed in the intersection and the audit's own
   conclusion (`nothing is both unobservable and actuatable`) was contradicted by its own table. The resolution was not
   to redefine the intersection but to fix a **mislabeled row**: the transport layer is the **operation set**, and what
   the actuation changes is the **density**, which the observables read. The ledger now says so, and the counts agree.
3. **"EXACTLY" beside a rounding.** The uniform direction showed an acceleration change of `5.551E-017` — a rounding of a
   zero — and the text said the acceleration was "unchanged"; the verdict now prints the residual *and* calls it a
   rounding. (The same class as G_083's `VACUOUS` and G_088's telescoping.)
4. **A scripted edit mangled two string literals** while inserting assumptions, leaving the argument list unclosed and
   breaking the build; and an earlier scripted rewrite of the same file inserted a duplicate line. Both were caught by
   the compiler, and both are recorded because the tooling is part of the audit's provenance.

---

## What this does and does not establish

**Establishes.** That the inverse problem has a closed solution — **the density is the lever arm, the requirement is
`d/(1+cA)` ≈ 3, and a gravity change needs a differential rather than a uniform change**; that exactly **one** channel is
actuatable and it is the class's own transfer; that every unobservable channel is a **conserved input**; that **among the
controls nothing is both unobservable and actuatable**; and that the sector prices a **source** exactly while pricing a
**redistribution** at nothing.

**Does not establish.** That manipulation is impossible — the audit was not allowed to argue that, and it does not. What
it establishes is **which channel would be needed** and **what the class would have to contain**: an operation that
changes a **conserved input** (the link structure, the component count, the total), or one that changes a **parameter of
the law** (the kernel's curvature, the only channel that would be both effective and unobservable in situ). **Those are
requirements on the theory, stated as such rather than smuggled in as primitives.**

**Why the actuatable channel is not a new manipulation.** The clock and the source are **one object** (the sector's own
finding that the rate and the gravity share a source), so moving count moves the clock, and the effect is the ordinary
gravitational one. A manipulation **the observables do not read** would have to act through a quantity the observables do
not read — and **every such quantity in this sector is conserved**.

**Out of scope (substrate).** No spectral quantity is quoted, so the D96/D96³ rule is **not engaged**.

---

## Where it stands

- G_077: the clock and gravity share a common source; G_078: the elementary channel is redistribution, and the ledger
  prices a source exactly;
- G_080–G_083: connectivity is **assumed**, the component count is **gauge-like**, the continuity law is a **coordinate
  choice**;
- G_084–G_086: the core is **one state variable and three equations**, and the observables are the **rate kernel alone**;
- G_087–G_088: the kernel's restored second function is **empirically undecidable** at every recorded and planned
  capability, and no alternative observable escapes the degeneracy;
- **G_089: the manipulation question is answered for the CHANNEL rather than for the possibility** — one actuatable
  channel, three unobservable and unactuatable controls, and a named requirement for the channel that would be needed.

`ManipulationChannelAudit.Verdict()` starts with `ONE ACTUATABLE CHANNEL`.
