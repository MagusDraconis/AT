# ResearchY-G_087 — Kernel Discriminability Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_087_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/KernelDiscriminabilityAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_087`) · **Verdict:** **KERNEL DIMENSION UNDECIDABLE** (registry: Derived)

---

## Question

G_086 collapsed the observable kernel to the rate `K(ρ)` alone — conditional on the clock law's logarithmic derivative
being `1` — and showed that the smallest extension restoring a second function is the free room,
`ln K = A + c A²/2`.

> **Can any surviving measurement distinguish the one-function kernel from the two-function one?**

**Prefer refutation.** The question is whether the extension's signature is measurable at all.

---

## Answer

**NO AT ANY RECORDED OR PLANNED CAPABILITY FOR A NATURAL SIZE OF THE EXTENSION — AND THERE IS A VETO INDEPENDENT OF
THE INSTRUMENT.**

| Task | Result |
|---|---|
| 1 Every observable from `K` alone | **9 readouts**, 5 rate-based, 4 source-carrying in **two readings** (kernel re-cut / map held) |
| 2 The smallest extension that breaks the collapse | **one parameter**, second order in the potential |
| 3 The first observable sensitive to it | the **AT/GR comparison** (**3.109E-001** per unit `c`), second the kernel re-cut acceleration; the **map-held source readouts are exactly ZERO** |
| 4 The minimum precision | **4.5759 %** (shared compactness) / **3.4398 %** (one prediction) on the redshift for `|c| = 1` at 3σ |
| 5 J0740 re-evaluated | smallest detectable `|c|`: **4.6** (current) … **0.7** (direct 1 % redshift) |
| 6 Can any experiment decide it? | **REFUTED** for the current and near-term classes; below `|c| = 0.72` the **compactness error vetoes** it at any precision |

---

## 1. Every readout from `K` alone (task 1)

The extension changes the **kernel** — and the kernel is what **defines** the potential in G_086's representation. So
the source-carrying readouts are reported in **both readings**, and the difference between them is itself a finding:

| observable | one-function | extended | signal | relative | reading |
|---|---|---|---|---|---|
| the rate at the deepest well | 7.368063E-001 | 7.471890E-001 | 1.038E-002 | 1.409E-002 | rate-based |
| `1 + z` at the deepest well | 1.357209E+000 | 1.338349E+000 | −1.886E-002 | −1.390E-002 | rate-based |
| the metric `g00` at the well | −5.428835E-001 | −5.582914E-001 | −1.541E-002 | −2.838E-002 | rate-based |
| the redshift between the hill and the well | 5.932562E-001 | 5.711168E-001 | −2.214E-002 | −3.732E-002 | rate-based |
| the AT/GR comparison at the target | −1.256265E-001 | −1.372885E-001 | −1.166E-002 | −9.283E-002 | rate-based |
| the potential at the well (**kernel re-cut**) | −3.054302E-001 | −2.914371E-001 | 1.399E-002 | 4.581E-002 | source-carrying |
| the acceleration at the well (**kernel re-cut**) | −1.068823E-003 | −9.710591E-004 | 9.776E-005 | 9.147E-002 | source-carrying |
| the potential at the well (**map held**) | −3.054302E-001 | −3.054302E-001 | **0.000E+000** | **0.000E+000** | source-carrying |
| the acceleration at the well (**map held**) | −1.068823E-003 | −1.068823E-003 | **0.000E+000** | **0.000E+000** | source-carrying |

**The redshift moves in both readings**, which is why it is the robust discriminator; the map-held source readouts are
**exactly insensitive**, which is why nothing already measured excludes the extension.

---

## 2. The smallest extension (task 2)

`ln K = A + c A²/2`, one parameter, second order in the potential — and the **first surviving** coefficient: the
constant and the linear terms are pinned by the sector's normalization and by G_086's collapse. `K(1) = 1` holds at
every member (`c = ±1, ±0.3`), and the effect is **exactly proportional to `c`** (doubling `c` doubles the increment to
**9** digits).

---

## 3. The first sensitive observable (task 3)

Relative sensitivity per unit `c`, measured by a central difference with `h = 1e-6`:

| rank | observable | relative sensitivity |
|---|---|---|
| 1 | the **AT/GR comparison** at the target | **3.109E-001** |
| 2 | the acceleration at the well (**kernel re-cut**) | 3.049E-001 |
| 3 | the potential at the well (**kernel re-cut**) | 1.527E-001 |
| 4 | the metric `g00` at the well | 9.329E-002 |
| 5 | the redshift between the hill and the well | 9.197E-002 |
| 6 | the rate at the deepest well | 4.664E-002 |
| 7 | `1 + z` at the deepest well | 4.664E-002 |
| 8–9 | **both map-held source readouts** | **0.000E+000** |

The **most sensitive comparison is the AT/GR difference itself** — because its own value is *already* second order, so a
second-order shift moves it the most in relative terms. That is why the kernel's dimension is not a separate experiment
but a **finer version of the one the programme is already chasing**.

---

## 4. The minimum precision (task 4)

At the target (`|x| = 2.470011E-001`), under the project's own two error models:

| model | required relative redshift precision for 3σ |
|---|---|
| the compactness shared (it cancels to first order) | **4.5759 %** at `|c| = 1` |
| one prediction against data | **3.4398 %** at `|c| = 1` |

and the sweep: `|c| = 0.1 → 0.4639 %`, `0.3 → 1.3874 %`, `1.0 → 4.5759 %`, `3.0 → 13.32 %`, `10.0 → 40.04 %` — with the
conservative model **unreachable (NaN)** for `|c| ≤ 0.3`: **the compactness error alone vetoes those sizes at 3σ, at
any redshift precision whatsoever**. The veto threshold is **`|c| = 0.72`**.

**The numerics are the rules' own:** the signature is a difference of nearly equal exponentials, so it is computed with
`AtNumerics.ExpM1`; the naive subtraction is *carried and measured* and is exact down to `c ~ 1e-6`, failing below it
(and the audit reports that failure scale rather than assuming it cannot matter).

---

## 5. J0740 re-evaluated (task 5)

Target **J0740+6620 (Riley 2021)**, `|x| = 2.470011E-001`, relative compactness error **3.661 %**:

| instrument class | timing | compactness | smallest detectable `\|c\|` | what binds |
|---|---|---|---|---|
| A. current X-ray timing (NICER-class) | 20.0 % | 9.066 % | **4.6** | the TIMING |
| B. A plus radio pulsar timing | 20.0 % | 8.334 % | **4.6** | the TIMING |
| C. next-generation X-ray timing | 10.0 % | 3.162 % | **2.3** | the TIMING |
| D. next-generation timing and spectroscopy | 5.0 % | 1.581 % | **1.1** | the TIMING |
| E. a direct surface-redshift measurement | 1.0 % | 3.661 % | **0.7** | the TIMING |

**The current class offers NO CONSTRAINT** — it would need an extension of order **4.6**, and the most advanced recorded
class would need order **0.7**. Its bound on the kernel's dimension is therefore *empty*.

---

## 6. Can any current or planned experiment decide it? (task 6)

**REFUTED at the recorded capability**, and for a quantitative reason rather than a slogan:

- the extension's signature at the target is **1.166E-002** against the AT/GR separation **1.256E-001** — the kernel's
  dimension sits a factor **10.8** *below* the effect the same programme is already chasing, so the measurement that
  decides the theory does **not** decide the kernel;
- `|c| = 1` mimics a compactness shift of **4.114E-002**, i.e. **16.65 %** of the compactness, against the recorded
  compactness error of **3.661 %** — the extension is **degenerate with the compactness error**;
- and the **veto**: below `|c| = 0.72` no redshift precision whatever can decide the kernel's dimension at 3σ, because
  the compactness-induced error already exceeds the signature's budget.

**The decisive measurement, in the programme's own form:** a surface redshift measured to **better than ~0.5 %** at the
target, *with the compactness known far better than it is* — that is what a natural extension (`|c| ~ 0.1–0.3`) would
require, and it is an order of magnitude beyond every recorded class.

---

## Defects found while building this audit

1. **The "controls" were not controls.** The first version asserted the source-carrying readouts were insensitive to the
   extension — true when the potential **map** is held (G_085's convention), false when the kernel is **re-cut** (G_086's
   representation, where the potential **is** `ln K`). The axis was wrong, not the arithmetic: the audit now reports
   **both readings** and states which observables move in each. Without the fix the largest signature in the whole
   instrument would have been reported as exactly zero.
2. **The audit's own report contradicted the measurement.** It claimed the **between-sites redshift** was the first
   sensitive observable; the measurement puts the **AT/GR comparison** first (3.109E-001) because that comparison's own
   value is already second order. The prose was rewritten from the table.
3. **The compactness is NEGATIVE in the project's convention** (`X = −GM/(Rc²)`), so a signed ratio against it flips
   sign: an assertion comparing `shift/x` with a positive error **failed while the magnitudes said the opposite**. Every
   figure in the audit is now a magnitude, and the convention is stated.
4. **A monotonicity assertion written backwards.** The first version required a ten-times smaller extension to need a
   *looser* measurement; the requirement **falls** with the extension's size (0.4639 % at `|c| = 0.1` against 4.5759 %
   at `|c| = 1`). Corrected, and the direction is now stated in the output.
5. **A silent file corruption in the toolchain, not in the science.** A PowerShell rewrite collapsed the test file onto
   one line, so every `///` comment swallowed the code after it and the suite vanished from discovery ("no test
   matches"). Caught because the *filter* found nothing rather than because a test failed — a reminder that a green
   discovery count is itself an assertion.

---

## What this does and does not establish

**Establishes.** That the smallest extension is **one parameter**; that the **AT/GR comparison** is the most sensitive
observable to it (**3.109E-001** per unit `c`) with the map-held source readouts **exactly** insensitive; that a shared
compactness needs **4.5759 %** and a single prediction **3.4398 %** on the redshift for `|c| = 1`; that the current
class reaches only `|c| ≈ 4.6` and the most advanced recorded class `|c| ≈ 0.7`; and that below `|c| = 0.72` the
compactness error **vetoes** the decision at any precision.

**Does not establish.** That the extension is **absent**. The audit shows the surviving measurements cannot **see** it
at their precisions — not that it is zero. A *large* extension (a clock law far from the exponential) would be visible
to the current class, and that case is not excluded either.

**Out of scope (substrate).** No spectral quantity is quoted — no eigenspace count, no multiplicity — so the
D96/D96³ Spectral Substrate Rule is **not engaged**.

---

## Where it stands

- G_084: the core is **one state variable and three equations**;
- G_085: the maps are observable only through the **composition** and the **derivative**;
- G_086: those two objects are **one** — the rate — for the sector's law, with the free room the only parameter that
  would restore a second;
- G_087: **the restoration is not measurable at any recorded or planned capability**, and below `|c| ≈ 0.7` the
  compactness error vetoes it outright.

`KernelDiscriminabilityAudit.Verdict()` starts with `KERNEL DIMENSION UNDECIDABLE`, and the registry classifies the
audit as **Derived** — the thresholds are computed, not estimated.

**The chain has reached data, and the data cannot answer it.** What would change the verdict is named: a signature
larger than the separation (an extension of order ten or more would do it — the current class would see *that*), a
precision an order of magnitude beyond the recorded classes, or **a second target at a different compactness** that
breaks the degeneracy with the compactness error.
