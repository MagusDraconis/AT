# ResearchY-G_095 — Memory Coefficient Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_095_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/MemoryCoefficientAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_095`) · **Verdict:** **THE COEFFICIENT REMAINS FREE — AND THE ONE PRINCIPLE THAT WOULD FIX IT IS ABSENT**

---

## Question

G_093 found that a **memory channel carrying the kernel's coefficient** is required for first-principles completion
and **absent**; G_094 found it the **only** gap whose closure changes a physical prediction, and that the change is
**conditional on the value delivered**.

> **What fixes the memory coefficient?**

**Allowed:** the full surviving theory, the memory-channel track, the mode-locking track.
**Forbidden:** **setting the coefficient to zero**; **fitting observations**.
**Prefer refutation.** **Failure** is that the coefficient remains free; **success** is that a unique value is forced.

Both prohibitions are obeyed literally: neither route is used as an **answer**. The audit measures every candidate
**selecting principle** instead of arguing from plausibility.

---

## Answer

**THE COEFFICIENT REMAINS FREE — AND THE ONE PRINCIPLE THAT WOULD FIX IT IS ABSENT.**

| Task | Result |
|---|---|
| 1 Classification | **empirical** on all four counts — not derived, not emergent, not fixed-point selected |
| 2 Boundaries | **one-sided**: **λ < 2.002** by monotonicity (2.000 analytically); **no bound below** |
| 3 Stability | **undefined rather than negative**: there is **no flow** for the coefficient |
| 4 Selecting principles | **7** measured · **4** contained · **1** would select · intersection **0** |
| 5 New observables | **0** — it moves **7** that already exist |
| 6 Manipulation | **no** channel from the value alone: the coupling is exactly **0.0** |

---

## 1. Which principles select (task 4)

| principle | in the theory | would select | composition residual |
|---|---|---|---|
| **MULTIPLICATIVITY** (the clock composition multiplies) | **no** | **yes, uniquely** | **4.441E-016** vs a best competitor at **3.960E-002** |
| viability (positivity + monotonicity) | yes | no | — |
| the second-order coefficient (shared, `F''(0) = 4`) | yes | no | — |
| the pinned data | **excluded by the task as a fit** | no | — |
| conservation | yes | no | — |
| stability (a flow with the coefficient as a variable) | no | no | — |
| the AT/GR distinction | yes | no (it **excludes** one point) | — |

**The principle that would select does so decisively** — and **the surviving theory does not contain it**: the chain's
own uniqueness audit classes the property **`COMPLETELY FREE`** at **1 of 9** laws and states that it is **absent
rather than imported**.

**Nothing else selects:** viability holds on both sides; the second-order coefficient is a **class** shared by **3 of 9**
laws; conservation **cannot see** the coefficient (it is applied at **read** time, while the conservation statement
takes the operations on the state); there is **no flow** for it; and the data route is excluded by the task as a **fit**.

---

## 2. The boundary analysis (task 2)

| regime | λ | viable | second-order coefficient | redshift quadratic | free part | 1+z at target | GR second order |
|---|---|---|---|---|---|---|---|
| λ = 0 | 0.0 | **yes** | 2.000 | 0.500 | 1.708 | 1.280 | no |
| λ > 0 | +0.5 | **yes** | 2.500 | 0.250 | 2.017 | 1.261 | no |
| λ > 0 | +2.0 | **yes** (the edge) | 4.000 | −0.500 | 2.566 | 1.222 | no |
| **λ strongly positive** | +10 | **NO — excluded** | 12.000 | −4.500 | 4.903 | 1.143 | no |
| λ < 0 | −0.5 | **yes** | 1.500 | 0.750 | 1.407 | 1.300 | no |
| **λ = −2 (the GR point)** | −2.0 | **yes** | **0.000** | **1.500** | 0.559 | 1.361 | **yes** |
| λ strongly negative | −100 | **yes** | −98.000 | 50.500 | −8.271 | 27.044 | no |

**One side is bounded and the other is not.** Monotonicity of the clock requires `1 + λx > 0` on the physical range, so
**λ < 2**; the edge found by **bisection** is **2.002** against an analytic **2.000** — a **0.1 per cent** gap that is the
**sampler's** rather than the theory's, and it is reported rather than smoothed away. The **negative side has no bound
at all**: every value tested down to **−100** is viable, and the extreme where the numerical test fails is an
**underflow of the clock**, recorded as a numerical limit rather than a physical one.

**The only exclusion in the whole analysis** is the strong positive region, where the clock stops being monotone —
and at **λ = −2** the clock law's second order **coincides with GR's**, destroying the sector's distinctive
second-order signature. So what the surviving theory fixes is a **point to avoid** rather than a value to take.

---

## 3. The stability analysis (task 3)

| question | answer | measurement |
|---|---|---|
| is there a flow with the coefficient as a variable? | **NO** — so "fixed-point selected" is **undefined** | no update relation and no operation takes it as an argument: the kernel is applied at **read** time |
| is the readout response stable? | **bounded and smooth** | the largest slope drift over the family is **1.443E-006** |
| does the family contain a runaway member? | yes, at extreme suppression — and it is **viable** | the target prediction at λ = −100 is **27.044** |

**The stability question is answered by being refused on its own terms**: a fixed point needs a flow, and the sector
has none for the coefficient. What can be measured — the readout response — is bounded.

---

## 4. New observables and channels (tasks 5 and 6)

**Zero new observables.** The coefficient moves **7** readouts that **already exist** (the largest at **3.109E-001**
relative per unit coefficient) while the description still has **8**, so the coefficient is a **constant of the
theory** rather than the source of a new field.

**No channel from the value alone.** The curvature's coupling to every state-based process is exactly **0.0** and the
actuatable channel count stays at **1**; no operation has the coefficient in its argument list. So **G_094's reopening
needs the STRUCTURE, not the value** — which is exactly why the two questions are separate.

**And the coefficient is empirically consequential all the same:** the target prediction moves from **1.280** at the
recorded coefficient to **27.044** at the extreme — a spread **207 times** the AT/GR separation of **0.126**.

---

## Defects found while building this audit

1. **The bound was reported as absent on BOTH sides, and it is not.** The first `Bounds()` computed a "bounded" flag as
   `viable == tested`, which is **inverted**: a side with *no* excluded value is **unbounded**. Worse, the sweep had
   been read as "every value viable" when the positive side in fact contains **two exclusions** (λ = 10 and λ = 100),
   because **monotonicity** fails there. The bound is now found by **bisection** and compared against the analytic value
   (`1/|x_end| = 2`), so the edge is verified rather than asserted, and the **0.1 per cent** gap is attributed to the
   **sampler** and stated.
2. **A tolerance tighter than the instrument.** The GR-point row's coincidence check used `1e-9`, but the
   second-order coefficients are **differenced numerically** and carry about **1e-9** of noise, so the row reported
   `MatchesGr = false` for a law whose second-order coefficient is `−3.6E-9` against GR's `0`. The comparison is now
   made at **1e-6**, with the tolerance stated in the row rather than left to the reader.
3. **A record built as a tuple.** `SelectingPrinciples()` and `Bounds()` returned **tuple literals** where the declared
   types are records and named arrays, which compiled the array's element type into an anonymous tuple: the audit had to
   be rebuilt with explicit constructors and an array return type. The measurement was right and the *declaration* was
   wrong, which is worth recording because the reverse (a right declaration over a wrong measurement) is the usual
   failure here.

---

## What this does and does not establish

**Establishes:** that the coefficient is **empirical** on all four classes tested, because **no principle the surviving
theory contains selects a value**; that the one principle which **would** select does so **uniquely** and is measured
**absent**; that the coefficient is **bounded on one side only** (λ < 2, unbounded below); that there is **no flow** for
it, so "fixed-point selected" is **undefined** rather than false; that the surviving theory fixes a **point it must
avoid**; and that the coefficient introduces **no observable and no channel** of its own.

**Does not establish:** that **no value is correct**. The audit measures that the surviving theory does not **select**
one — a statement about the premises, not about the world. Nor does it establish that multiplicativity is the **only**
route: it is the only route among the seven principles tested here, and a principle this audit did not think to test
would have to be found by a **new measurement** rather than by adding a row.

**Why this is not a restatement of G_091 or G_094:** G_091 measured that the curvature cannot be **actuated** and G_094
that closing the memory channel would move readouts **conditionally** on the value delivered. This audit asks which
**premise delivers** the value, measures seven candidate principles against the chain's own instruments, and finds the
one that would select it **outside the surviving theory** — which is the statement that makes the sector's open question
precise.

---

## Where it stands

**Substrate:** the canonical **96-cell ring** for the time sector, and the **clock law's own variable** for the
coefficient. **Equality rule:** `1e-12` on a residual, with the **numerically-differenced coefficients compared at
1e-6** because they carry about **1e-9** of noise — the tolerance is a property of the instrument and is stated with the
measurement. **No spectral quantity is quoted** — no A₀, no free room, no lock release, no survivor count — so the
spectral-substrate rule is not engaged.

**Guards:** `Y_G_033` — `without` **63 → 64**; `Y_G_035` — **95 → 96**, survives **81 → 82**. Registry entry:
**derived**.

**What would change the verdict:** a **derivation of the composition law** (which would force the coefficient to zero
and turn this audit's failure into a success); a measurement fine enough to see the second order at the target (which
would make the coefficient empirical **in fact** rather than in principle); or a **flow** that takes the coefficient as
a variable (which would make "fixed-point selected" a meaningful class here).
