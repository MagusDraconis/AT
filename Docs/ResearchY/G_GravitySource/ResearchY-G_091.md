# ResearchY-G_091 — Kernel Curvature Actuation Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_091_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/KernelCurvatureActuationAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_091`) · **Verdict:** **A PARAMETER, NOT A CONTROLLABLE VARIABLE** (registry: Derived)

---

## Question

G_090 left exactly **one** exception standing: the kernel's **curvature** — the only amplifying regime, a parameter of
the law, and the one quantity G_088 measured as effective yet unobservable in situ.

> **Can the kernel's curvature be actuated by known physical processes?**

**Allowed:** the surviving kernel `K(ρ)`, all surviving audits, known physical processes.
**Forbidden:** new primitives, source terms, component-count manipulation.
**Prefer refutation.** Success would be a physical actuator; **failure** would be that the curvature is a **parameter**
rather than a controllable variable.

---

## Answer

**FAILURE — THE KERNEL'S CURVATURE IS A PARAMETER, NOT A CONTROLLABLE VARIABLE.**

| Task | Result |
|---|---|
| 1 Operational definition | a **procedure**, executed twice — **2.618E-009** by second difference, **2.498E-015** by the acceleration's slope |
| 2 Observable / controllable | **four separate answers**: in principle **yes**, in practice **no**, controllable **no**, effective **yes (both)** |
| 3 Coupling search | **all zeros**, and the zero is **structural**: the curvature's state-dependence is **identically zero** |
| 4 `Δ`curvature per unit input | **5** candidates at **0.0** against **7** readouts moving — the largest at **3.109E-001** |
| 5 Clock, gravity or both | **both** — five clock readouts **and** the kernel re-cut acceleration |
| 6 Classification | **impossible · indirect · astrophysical · laboratory-feasible: NONE** |

---

## 1. The operational definition (task 1)

The curvature **is** the second derivative of the log-rate with respect to the potential — equivalently the **slope of the
acceleration** against it. Both procedures are executed:

| potential | by second difference | exact | residual |
|---|---|---|---|
| 1.0E-009 | 3.000000E-001 | 0.300 | 2.618E-009 |
| 2.470E-001 | 3.000000E-001 | 0.300 | 2.618E-009 |
| 5.360E-001 | 3.000000E-001 | 0.300 | 2.618E-009 |

and by the acceleration's slope over three potential pairs, to **2.498E-015**. **The definition is a procedure, not a
symbol** — and the identical values across the potential range are the first sign of what the rest of the audit finds.

---

## 2. Observable or controllable (task 2) — four answers, not one

| question | answer | basis |
|---|---|---|
| observable **in principle**? | **yes** | the two procedures reproduce it |
| observable **in practice** by the programme? | **no** | signature **1.166E-002** against an AT/GR separation of **1.256E-001**; below **\|c\| = 0.72** the compactness error vetoes any decision |
| **controllable** by a surviving operation? | **no** | the class's operations are transfers; nothing changes a coefficient of the law |
| **effective** if it did change? | **yes**, on **both** | it multiplies the potential squared in the rate and the potential in the gradient |

---

## 3. The coupling search (task 3) — all zeros, and the zero is a computation

| process | couples through | a state? | coupling | status |
|---|---|---|---|---|
| energy | the local energy density | yes | **0.0** | zero by state-independence |
| coherence | a coherence of the motion | yes | **0.0** | zero by state-independence |
| phase | the phase structure | yes | **0.0** | zero by state-independence |
| information | the information content | yes | **0.0** | zero by state-independence |
| resonance | a resonant drive | yes | **0.0** | zero by state-independence |
| memory-channel structure | a structure carrying a coefficient | **no** | — | **forbidden: a new primitive** |

**The maximum over the five surviving candidates is 0.0** — and the reason is the object, not the search: the curvature is
the **same at every density, every potential and every site**, so its **state-dependence is identically zero** and a
state-based process has **nothing to couple to**. The one candidate that **could** couple — a **memory-channel
structure** — is a **new primitive**, which the rules forbid this audit to introduce: it is **named rather than used**.

---

## 4. The asymmetry (task 4) — an infinite lever arm with zero actuation

| side | count | value |
|---|---|---|
| the **actuation** side | **5** candidates | **0.0** each |
| the **effect** side | **7** readouts | largest **3.109E-001** per unit curvature |

**And the effect is on BOTH** (task 5): the clock readouts — `g00`, the clock rate, the site redshift, the AT/GR ratio,
the pair redshift — **and** the gravity readout (the kernel re-cut acceleration). That is why the quantity is worth
naming even though it cannot be actuated.

---

## 5. Classification (task 6)

| classification | candidate | why |
|---|---|---|
| **impossible** | every state-based process | the coupling is zero **by state-independence** — arithmetic, not a failed search |
| **indirect** | any process that changes the **state** and thereby the potential at which the curvature is read | it moves the **argument**, not the coefficient |
| **astrophysical** | the regime of the largest **effect** (a compact object's surface; **×4.7** over the interior) | the effect grows with the potential squared |
| **laboratory-feasible** | **none** | a laboratory can only move the density, which is G_090's collapse |

**The requirement an actuator would have to meet:** a process that changes a **COEFFICIENT OF THE LAW** — which is a
statement about what the theory would need, **not** a mechanism inside it.

---

## Defects found while building this audit

1. **The gravity side of the "both" claim was taken from the wrong convention.** With the **map held** (G_085's
   convention) a free-room change leaves the **gradients exactly untouched** — the source readouts are *insensitive* to
   it — so the first version's gravity list came back empty. In the **collapsed** representation the potential **is**
   `ln K`, so the consistent reading is the **kernel re-cut** acceleration, which G_087 already measured at
   **3.049E-001** per unit curvature. The audit now reads the gravity side from that ranking and **says which
   convention** it is using, because the two differ and the difference is exactly G_085's finding.
2. **Two assertions confused the log-rate with the rate.** The test asserted `ln K = 1` where the pinned datum is
   `K = 1 ⇔ ln K = 0`, and asserted a curvature equal to a *value* rather than to the *coefficient times A²/2*. Both
   were corrected, and the corrected forms now *test the pinned data* rather than restating a symbol.
3. **A "both sectors" claim nearly rested on a single readout.** The gravity side has **one** measuring readout in the
   collapsed representation; the audit reports the count (5 clock, 1 gravity) rather than saying "both" without
   quantities — the G_084 lesson that a category count is not a measurement count.

---

## What this does and does not establish

**Establishes.** That the curvature has an exact **operational definition** with two procedures that reproduce it; that
it is **observable in principle and not in practice**; that it is **controllable by nothing** in the surviving
vocabulary; that its coupling to every state-based process is **exactly zero by state-independence** rather than by a
failed search; that its effect is on **both** clock and gravity; and that the **actuation gain is zero while the effect
is large**.

**Does not establish.** That **no** actuator could ever exist. The audit names the one candidate that **could** couple —
a structure carrying a coefficient, i.e. a **memory channel** — and states plainly that it is a **new primitive**, which
the rules forbid introducing here. **The conclusion is about the surviving vocabulary, not about physics at large.**

**Why this is not a restatement of G_088–G_090.** G_088 measured that the curvature is unobservable in situ; G_089 and
G_090 that it has no actuator. What is added here is the **reason and its arithmetic**: the curvature is
**state-independent**, so the coupling is zero **by construction**, and the audit **computes** the coupling strength
instead of reporting the absence of a mechanism.

**Out of scope (substrate).** No spectral quantity is quoted — so the D96/D96³ rule is **not engaged**.

---

## Where it stands

- G_086: the observables are the **rate kernel alone**, with the free room the only parameter that restores a second
  function;
- G_087–G_088: the restored function is **undecidable** at every recorded capability, and no alternative observable
  escapes the degeneracy — while the curvature remains the one **effective and unmeasured** probe;
- G_089–G_090: **one** actuatable channel (the class's own transfer), and every candidate pump collapses;
- **G_091: the surviving exception is closed** — the curvature is a **parameter**, with a **zero coupling computed from
  its state-independence**, an effect on **both** sectors, and an actuator that would have to change a **coefficient of
  the law**.

`KernelCurvatureActuationAudit.Verdict()` starts with `KERNEL CURVATURE IS A PARAMETER`.
