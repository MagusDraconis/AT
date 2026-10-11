# ResearchY-G_086 — Observable Kernel Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_086_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ObservableKernelAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_086`) · **Verdict:** **OBSERVABLE KERNEL COLLAPSE** (registry: Derived)

---

## Question

G_084 derived the minimal gravity core; G_085 showed that the maps are observable only through their **composition**
and their **derivative**, leaving two observable objects — the **rate** `K(ρ) = W(f(ρ))` and the **source**
`S(ρ) = f′(ρ)`.

> **Can the observable kernel be reduced further?**

**Prefer reduction.** A successful reduction removes the distinction between `f` and `W`; a failure proves the kernel
minimal.

---

## Answer

**OBSERVABLE KERNEL COLLAPSE — 8 OF 8 READOUTS ARE REPRODUCED FROM THE RATE KERNEL ALONE, AND THE KERNEL'S DIMENSION IS
1 FOR THE SECTOR'S OWN CLOCK LAW AGAINST 2 IN GENERAL.**

| Task | Result |
|---|---|
| 1 Every observable as a function of `ρ` | **8 of 8** recomputed from the rate kernel, difference **0.000E+000** |
| 2 Eliminate `f` and `W` separately | **Both individually eliminable** — readouts need neither map |
| 3 Is `K` the only clock object? | **Yes** — the λ family shares it exactly at every member |
| 4 Is `S` the only gravity object? | **It is not independent** for the sector's law: `S = (ln K)′` to **9.021E-013** |
| 5 Further redundancy | `g00 = −K²` exactly, both redshifts are functions of `K`; the mismatch field explains the anchors' blindness |
| 6 `ObservableKernelDimension` | **1** for the sector's law, **2** in general (the deciding parameter is the free room) |
| 7 Minimal representation | **one state, one observable map, seven predictions** plus the anchor |

---

## 0. The instrument

The readouts are recomputed from the kernel with **no potential map and no clock map anywhere**:

- the potential is **defined** as `ln K` — which is exactly what `A = ½ ln(−g00)` says once `g00 = −K²`;
- the source is the **gradient** of that.

So if the values match the ones computed through the maps, the maps are not inputs at all. Derivatives use central
differences with `h = 1e-4` (truncation ~`1e-9`), compared at `1e-6` — stated, not implied.

---

## 1. Every readout as a function of the density (tasks 1 and 2)

| observable | through the maps | from the kernel | difference |
|---|---|---|---|
| `g00`, the metric's time component | −1.265883E+000 | −1.265883E+000 | **0.000E+000** |
| the clock rate at a site | 1.125115E+000 | 1.125115E+000 | **0.000E+000** |
| the potential at a site | 1.178851E-001 | 1.178851E-001 | 2.776E-017 |
| the redshift of a site from its own rate | −1.112018E-001 | −1.112018E-001 | **0.000E+000** |
| the source acceleration of the field (a gradient) | 1.068823E-003 | 1.068823E-003 | **0.000E+000** |
| the second-order AT/GR ratio at the target | 1.799687E+000 | 1.799687E+000 | **0.000E+000** |
| the gradient (acceleration) along a link | 2.760186E-004 | 2.760186E-004 | 5.551E-017 |
| the redshift between two sites | 3.954467E-002 | 3.954467E-002 | **0.000E+000** |

**Eight of eight reproduced**, six of them bit-identical and **two at the 1-ulp rounding of the `ln ∘ exp` round trip**
(`2.776E-017` and `5.551E-017`) — which the audit reports rather than calling zero, because the kernel route writes the
potential as a logarithm of an exponential while the maps route carries it directly. And the identity behind it:
`A = ln K` holds to **1e-12** for the sector's law. **Both maps are individually eliminable.**

---

## 2. The collapse, and its exact condition (tasks 3 and 4)

With the potential tied to the rate by the theory's own definition, the source is the rate's logarithmic derivative
**iff** the clock law's logarithmic derivative is `1` — that is, **iff the clock law is exponential**, which the
recorded law is.

**`K(ρ) = W(f(ρ))` is the only observable clock object**, and for the sector's law **`S` is not an independent gravity
object**: it *is* `(ln K)′`.

The failure is **measured rather than argued**:

| description | mismatch field `S − (ln K)′` | dimension |
|---|---|---|
| the sector's own law (rate `exp(A)`) | **9.021E-013** (the numerical floor) | **1** |
| the free room (rate `exp(A + cA²/2)`) | **7.636E-002** | **2** |
| a GR-shaped law (rate `sqrt(1 + 2A)`) | **1.308E+000** | **2** |

What fails is **exactly the source-carrying readouts** — the potential's level and the two gradients — and the **clock
readouts never fail** in any description. For the free room the mismatch is the exact field **`−c ln ρ / (d²ρ)`**,
checked cell by cell on **96** cells.

---

## 3. Task 5: further redundancy, and why the anchors are blind

**`g00 = −K²` exactly**; the site redshift is `1/K − 1`; the pair redshift is a ratio of `K`. The readout set's content
is the rate, plus the source only when the clock is not exponential.

And the sharpest observation of the audit: the mismatch field is proportional to `ln ρ`, so it **vanishes at the mean
density** — which is exactly where the pinned data live.

| description | `K(1)` | `d(K²)/dA(0)` | mismatch at the mean | elsewhere |
|---|---|---|---|---|
| the sector's own law | **1.0000** | **2.0000** | 5.551E-013 | 9.021E-013 |
| the free room | **1.0000** | **2.0000** | **1.671E-010** | **7.636E-002** |
| a GR-shaped law | **1.0000** | **2.0000** | 1.605E-009 | 1.308E+000 |

**Every** member satisfies the pinned data, and the free room's signature vanishes precisely there. That is the
**structural reason** the anchors cannot select the free room — G_085 measured the blindness; this audit explains it.
(The slope is `d(K²)/dA` at `A = 0`, i.e. `d·d(K²)/dρ` at `ρ = 1`, the chain rule applied explicitly since
`dρ/dA = d` at the mean density.)

---

## 4. The collapse is not a reparametrization artefact

The λ family (`f → λf`, `W → W(·/λ)`) shares the **rate exactly** at every member while the source scales with `λ`:

| λ | the rate's residual | the source's ratio | the potential's residual |
|---|---|---|---|
| 0.50 | 0.000E+000 | 0.500 | 5.894E-002 |
| 0.75 | 0.000E+000 | 0.750 | 2.947E-002 |
| **1.00** | 0.000E+000 | **1.000** | **0.000E+000** |
| 1.50 | 0.000E+000 | 1.500 | 5.894E-002 |
| 2.00 | 0.000E+000 | 2.000 | 1.179E-001 |

So a rate alone does **not** determine the potential — and the **acceleration excludes every member but `λ = 1`**, which
is the sector's own split. The collapse therefore follows from the **sector's content** and not from how it was written
down.

---

## 5. The final minimal representation (task 7)

| component | content |
|---|---|
| **State** | the count density `ρ` on the 96-cell ring — one object, and the only free datum |
| **Observable map (always)** | the **rate kernel** `K(ρ) = W(f(ρ))`: it carries `g00`, the clock rate, both redshifts and the target comparison |
| **Observable map (only when the clock is not exponential)** | the **source kernel** `S(ρ) = f′(ρ)`, which equals `(ln K)′` exactly for the sector's law |
| **Prediction: the clock** | `1 + z = K(ρ_o)/K(ρ_s)` between two sites, and `1/K − 1` at a site |
| **Prediction: the metric** | `g00 = −K²` — the same statement as the clock, squared |
| **Prediction: gravity** | `a = ∇ln K` along the links, with the potential's zero point the one gauge |
| **Prediction: the discrimination** | the AT/GR ratio at the target's compactness, whose density lies **outside** the state |
| **Anchor** | `K(1) = 1` and `d(K²)/dA = 2` at `A = 0` — the pinned data, which live at the mean density |

`ObservableKernelDimension = 1` for the sector's law, `2` in general — and the parameter that decides which is the
**free room**, the one thing the surviving data leave open.

---

## Defects found while building this audit

1. **A `Single` lookup for "the cell at the mean density" throws.** The profile has **two** cells at `ρ = 1` (it is
   symmetric), so `Single(...)` failed. The anchors are now evaluated **from the kernel** at `ρ = 1` rather than from a
   cell of the state — which is also the more honest statement, since the anchor is a property of the kernel.
2. **The pinned slope was differentiated with respect to the wrong variable.** The first version computed
   `d(K²)/dρ` and called it `d(K²)/dA`; the two differ by the chain factor `d = 3` at the mean density. Corrected, and
   the chain rule is now written out rather than left implicit — without it the audit would have reported a slope of
   `0.6667` against the sector's `2`.
3. **A cross-check compared a pointwise derivative with a ring gradient.** The kernel's `S(ρ)` is a derivative *with
   respect to the density*, while G_085's source field is a **ring gradient**; the two are related by the density
   gradient, not equal. The cross-check now asserts the two **exact** identities — the rate field is `K`, and the
   source field is `∇ln K` — with the pointwise collapse checked separately.
4. **An assertion about a count that was not the count in question.** The test asserted three map parts where G_085's
   inventory has six. Recorded because the same slip appeared in G_084's suite: the *instrument's* counts are easy to
   confuse with the *subject's*.
5. **A garbled symbol in the Theory Book text** (`½ ln(−g₀₀)` written as `½l n(−g₀₀)`), caught by reading the rendered
   output rather than the diff.

---

## What this does and does not establish

**Establishes.** That every surviving readout is a function of the density through the **rate kernel** alone
(**8 of 8** reproduced, six bit-identical and two at the **1e-17** rounding of the `ln ∘ exp` round trip); that the
potential map and the clock map are **individually eliminable**; that the
source is **redundant with the rate** exactly when the clock law is exponential — the sector's own law, at
**9.021E-013** — with the failure measured for the free room (**7.636E-002**) and a GR-shaped law (**1.308E+000**); and
that the mismatch **vanishes at the mean density**, which explains the anchors' blindness.

**Does not establish.** That a second function is never needed: for any non-exponential clock the mismatch is a field,
the acceleration reads it, and the dimension is 2. **The collapse is CONDITIONAL**, and its condition is the free room.

**Out of scope (substrate).** No spectral quantity is quoted — no eigenspace count, no multiplicity — so the
D96/D96³ Spectral Substrate Rule is **not engaged**, and the audit says so rather than omitting the lattice.

---

## Where it stands

- G_084: the core is **one state variable and three equations**, with 0 observable loss;
- G_085: the two maps are observable only through the **composition** and the **derivative** — one gauge, two units,
  one undetermined physical parameter;
- G_086: **those two objects are ONE** — the rate — for the sector's own law, with the source as its logarithmic
  derivative; the second function returns **only** if the clock law departs from the exponential, which is the free
  room.

What remains is one state, one observable map and seven predictions. And the open question is now the measurement
itself, not another reduction: **a second-order redshift at the target either confirms the one-function kernel (free
room zero) or restores the source as an independent gravity object.** The chain has reached the point where the next
step is data.
