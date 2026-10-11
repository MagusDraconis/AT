# ResearchY-G_088 — Alternative Observable Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_088_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/AlternativeObservableAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_088`) · **Verdict:** **EMPIRICALLY UNDECIDABLE** (registry: Derived)

---

## Question

G_087 measured that the free room's signature at J0740+6620 is a factor **10.8** below the AT/GR separation, that the
current class would need an extension of order **4.6**, and that below `|c| = 0.72` the recorded **compactness** error
vetoes the decision at any redshift precision. Its degeneracy is specific: the signature is **second order in the
potential**, and the compactness error mimics it.

> **Is there an observable NOT proportional to that same signature?** Seven domains are searched.

**Prefer refutation.** If every observable remains degenerate, `KernelDimension` is **empirically undecidable** within
the surviving sector.

---

## Answer

**THE DEGENERACY IS STRUCTURAL, NOT SPECIFIC TO J0740 — AND THE SECTOR HAS EXACTLY ONE MATHEMATICALLY INDEPENDENT
PROBE, WHICH NO MEASUREMENT REACHES.**

| Question | Answer |
|---|---|
| 1 Which observable is first not degenerate with the redshift? | a **derivative** — its signature is **first order** in the potential where the redshift's is second |
| 2 Can any surviving prediction separate 1 from 2? | **No** — the gradient route **is** the compactness, whose error already vetoes the redshift route |
| 3 Is a second compactness target sufficient? | **No** — `N` targets give `N` equations in `N + 1` unknowns; the gain is only `√N` of the same error |
| 4 Is the degeneracy structural or specific to J0740? | **Structural** — the extension is a function of the potential alone, and the powers are target-independent |

**Of the 8 catalogued observables, 4 are mathematically independent and 0 are independently measurable.**

---

## 0. The measure of independence

An observable's `c`-signature scales as `A^p` in the potential. **Two observables are collinear — hence degenerate at one
target — exactly when their powers agree**, because the signature is a function of the potential alone. The power is
**measured** by evaluating the relative signature at the surface potential and at a core potential and fitting
`p = ln(ρ₂/ρ₁)/ln(A₂/A₁)`.

---

## 1. The catalogue across seven domains (task 1)

| domain | observable | power | collinear | measurable | rate fixed |
|---|---|---|---|---|---|
| value | the surface redshift | **2.00** | yes | yes | yes |
| higher derivatives | the acceleration (first gradient) | **1.00** | **NO** | yes | yes |
| higher derivatives | the kernel's **second derivative** (curvature) | **0.00** | **NO** | **no** | no |
| clock transport | the integrated gradient along a path | **2.00** | yes | yes | yes |
| compact-object profiles | the core-to-surface ratio | **2.00** | yes | **no** | no |
| multi-compactness | the ratio of two targets' signatures | **2.00** | yes | yes | yes |
| dynamical evolution | the redshift's time derivative | **1.00** | **NO** | yes | **no** |
| mixed gravity/clock | the value-signature over the gradient's | **0.00** | **NO** | yes | yes |

**The answer to question 1: the first non-degenerate observable is a DERIVATIVE** — the acceleration, whose signature is
`cA` (first order) where the redshift's is `cA²/2` (second order).

---

## 2. The sharpest mathematically independent probe (task 2)

**`d²(ln K)/dA² = c` EXACTLY**, checked to twelve decimals at every potential and extension size tried. A curvature
measurement would read the extension **directly**, with no baseline ratio to be swamped by. The audit reports both
halves: the object is **non-collinear** *and* **unmeasurable**, because **no surviving measurement reads a curvature of
the clock law** — and its relative signature is *undefined* rather than small, since it has no `c = 0` baseline.

---

## 3. The structural tests (tasks 2, 3 and 4)

- **Clock transport telescopes** into the value difference — residual **2.776E-017** (the rounding of the logarithmic
  route) — so a clock carried between two sites carries **nothing** the redshift ratio does not.
- **The interior's signature is a factor 4.7 larger** (the core potential **5.365E-001** against the surface's
  **2.470E-001**, and the signature scales as the potential squared) — and **hidden**, because no clock escapes from the
  core. The profile domain is closed by **reachability**, not by form.
- **A second target is not sufficient, by a count:** one target → 1 equation in 2 unknowns (c-error **0.296**); two →
  2 in 3 (**0.210**); three → 3 in 4 (**0.171**); five → 5 in 6 (**0.133**); ten → 10 in 11 (**0.094**). Every system is
  **structurally rank-deficient**, and the gain is exactly `√N` — **2.24 at five targets** — not a new axis.
- **The mixed route moves the number by precision only:** the ratio of the value signature to the gradient's is exactly
  **`A/2`**, so (rate, gradient) at one target gives two equations in `(c, A)` — but the gradient **is** the compactness,
  whose error already vetoes the redshift route. Its reach: a **c-error of 0.296**, i.e. a three-sigma threshold near
  **|c| = 0.89** — **G_087's veto (0.72), recovered from the compactness side** (within 24 %).

---

## 4. The measurability ledger

| route | reachable |
|---|---|
| the surface redshift | yes (1–20 % depending on the class) |
| the compactness | yes (3.661 % at the recorded target) |
| the kernel's curvature | **no** — nothing reads a curvature of the clock law |
| the core's clock | **no** — the interior does not escape |
| the tidal field | **no** — a structural model, not a reading of the kernel |
| the redshift's drift | **no** — the rate that scales it is fixed by no surviving datum |
| a second target's compactness | yes — but it arrives with its own **free offset** |

---

## Defects found while building this audit

1. **The independence criterion was incomplete, and the audit's own count caught it.** The first version called the
   acceleration route "measurable and non-collinear" and therefore counted **1** independent axis — which would have
   contradicted the audit's own conclusion. The missing question is **through what** the route is measured: the gradient
   is the compactness, and the compactness is where the degeneracy lives. The criterion now requires a route that is
   *not* through the compactness, and the count falls to **0** — which is the finding, not a fudge: the flag is
   reported in the catalogue so a reader can see why each row passes or fails.
2. **"EXACTLY" beside a non-zero residual.** The verdict first said the transport telescopes "EXACTLY" into the value
   difference while printing residual `2.776E-017` — the rounding of the logarithmic route. The wording now says so.
   This is the same class as G_083's `0.000E+000` that needed the word `VACUOUS`.
3. **A power fit asserted at four decimals.** The fitted power is `2.0001` rather than exactly `2`, because the fit uses
   a finite `c = 1e-3`; the assertion was tightened past the truncation. Corrected to two decimals **and the reason
   recorded**, so the tolerance is not mistaken for sloppiness.
4. **A tautological assertion in a cross-check** (`x/x == 0.3`) — removed and replaced by a real comparison of this
   audit's kernel against G_087's at four densities.
5. **A PowerShell rewrite inserted literal backtick-n sequences** into the C# source while adding a field to a record,
   breaking the build. Repaired; recorded because the tooling is part of the audit's provenance, and because the same
   class of slip (a scripted edit corrupting a source file) had already appeared once in this chain.

---

## What this does and does not establish

**Establishes.** That the measure of independence is the **power of the potential** in an observable's signature; that
the rate carries power **2**, a first derivative **1**, and the kernel's second derivative **exactly `c`**; that the
interior's bigger signature (**×4.7**) is hidden, the transport telescopes into the value, the dynamical rate is
unfixed, and a second target adds an equation with its own free offset; and that the one route which moves the number —
the mixed (redshift, compactness) pair — moves it **by precision only**, to a three-sigma threshold near **|c| = 0.89**.

**Does not establish.** That **no** observable can ever separate the two. The second derivative of the kernel **is** `c`
exactly, so the separation exists mathematically; what is absent is a **measurement route**. A future measurement of the
clock law's curvature — or of a drift whose rate the theory fixes — would **reverse this verdict**, and both are named.

**Why this is not a restatement of G_087.** G_087 measured **one route's precision**; this audit searches **seven
domains for an independent axis** and reports the **structural reason** none is reachable.

**Out of scope (substrate).** No spectral quantity is quoted — so the D96/D96³ rule is **not engaged**.

---

## Where it stands

- G_086: the kernel is **one function**, with the free room the only parameter that restores a second;
- G_087: that restoration is **undecidable at every recorded and planned capability**, with a compactness-error veto;
- G_088: **no alternative observable escapes the same degeneracy** — the degeneracy is **structural** (the extension is a
  function of the potential alone, and every measurement route in the surviving sector reduces to the surface redshift
  and the compactness), and the one mathematically independent probe is a curvature nobody measures.

`AlternativeObservableAudit.Verdict()` starts with `KERNEL DIMENSION EMPIRICALLY UNDECIDABLE WITHIN THE SURVIVING
SECTOR`. The classification the question asked for therefore **applies** — with its two halves stated: the mathematical
separation is real and named; the empirical one is absent.
