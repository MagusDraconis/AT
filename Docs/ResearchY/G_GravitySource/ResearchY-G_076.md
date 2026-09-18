# ResearchY-G_076 - Clock-Law Selection Audit

**Program:** ResearchY - Gravity Source Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_076 (permanent)
**Title:** Is there any independent structural constraint that selects a unique clock law?
**Status:** COMPLETE
**Date:** 2026-09-18
**File:** `G_GravitySource/ResearchY-G_076.md`
**Depends on:** G_074 (the clock law's necessity up to its map), G_075 (the forced data and the maximal family), G_035 (the time sector is clock-only), G_072 (the only surviving row that reaches the second order)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_076_Tests.cs` (9/9 PASSED)
**Core:** `AT.Core/ResearchXH/ClockLawSelectionAudit.cs`

## The question

G_075 established **F(0) = 1**, **F'(0) = 2** and the **maximal family F(x) = 1 + 2x + x²G(x)**, with **GR a member**.
So: is there any **independent structural constraint**, drawn only from the allowed sources, that **selects a unique
clock law**?

| allowed sources | forbidden |
|---|---|
| composition of successive redshifts; clock synchronization consistency; path independence; group properties; conservation laws; locality; the actualization-density interpretation; multiplicativity; operational clock transport | fitting astrophysical data; assuming the exponential form; importing GR |

Determine: (1) **which constraints force a unique F**, (2) **whether multiplicativity uniquely yields exp(2x)**,
(3) **whether any weaker law also survives**, (4) the **cost of every added constraint**. Prefer refutation; if no
unique selector exists, conclude that **the clock sector is structurally underdetermined**.

## The answer

> **REFUTED - NO INDEPENDENT SELECTOR EXISTS, AND THE CLOCK SECTOR IS STRUCTURALLY UNDERDETERMINED.** Of the **10
> allowed sources as functional equations**, **3 are NON-SELECTIVE**, **1 is REFUTED**, **5 are PARTIAL** and **1
> SELECTS UNIQUELY - and the one that selects is not an independent source: it is the CONJUNCTION of the composition
> law with the pinned measurement, so it selects BY ASSUMING the composition law.**

## 1. The allowed sources are not independent, and that is the first result

Made precise as functional equations on the clock rate R = √F, **four rows from four different sources are ONE
equation**. Their **zero sets on the corpus are identical** (13 of 13 laws agree), which is the measured form of the
claim:

| law | composition | (1+z) factor | metric magnitude | log-additivity | path independence | same zero set |
|---|---|---|---|---|---|---|
| **exp(2x)** | **2.155E-016** | 2.288E-016 | 2.091E-016 | 2.949E-016 | 4.250E-016 | **yes** |
| (1+x)^2 | 1.459E-001 | 1.459E-001 | 2.705E-001 | 1.577E-001 | 1.830E-002 | yes |
| 1/(1-2x) | 1.489E-001 | 1.489E-001 | 1.289E+000 | 1.612E-001 | 7.156E-002 | yes |
| Pade [1/1] | 1.416E-001 | 1.416E-001 | 2.632E-001 | 1.527E-001 | 4.691E-003 | yes |
| Pade [2/2] | 4.627E-003 | 4.627E-003 | 9.233E-003 | 4.638E-003 | 1.930E-005 | yes |
| mesh interpolant (δ = 0.01) | 4.145E-003 | 4.145E-003 | 8.273E-003 | 4.154E-003 | 1.630E-003 | yes |
| exp(x), exp(3x) | ~2E-016 | ~2E-016 | ~2E-016 | ~2E-016 | ~2E-016 | yes |

**So the source list is one equation in four languages** - composition of successive redshifts, the group property on
the metric magnitude, log-additivity, and **path independence of the ACCUMULATED ratio** - rather than four
independent chances at a selector.

## 2. The non-selective sources, and why satisfied-by-everything is the measurement

| constraint | survivors | spread | kind |
|---|---|---|---|
| **locality** | **13 of 13** | 0.000E+000 | **NON-SELECTIVE (law-independent)** |
| **synchronization transitivity** | **13 of 13** | 0.000E+000 | **NON-SELECTIVE (law-independent)** |
| **count conservation** | **13 of 13** | 0.000E+000 | **NON-SELECTIVE (law-independent)** |

**The residual is identical for every law, not merely small.** That is not luck: the family is **by construction a
function of the potential alone** (G_035's arity proof), so **the gradient enters nothing** - locality's two probe
configurations agree at the probe and differ in their gradient, and every law returns the same rate - **equality of
rates is an equivalence relation** for any function of the potential, and **the count is conserved for any F**, so
the conservation residual contains **no F at all**. A constraint that every member satisfies **selects nothing**,
however physically natural it sounds.

## 3. The actualization composition is refuted rather than selecting

If a combined system advances **only when all its subsystems advance**, its rate is the **MINIMUM** of theirs while
its occupancy **MULTIPLIES**. The rule census, at the level of the rule:

| rule | rule value | recorded rate | contradiction |
|---|---|---|---|
| **product** | 0.782974 | 0.782974 | **0.000E+000** |
| **minimum** ("all subsystems must advance") | 0.843433 | 0.782974 | **6.046E-002** |
| **arithmetic mean** | 0.885875 | 0.782974 | **1.029E-001** |
| **geometric mean** | 0.884858 | 0.782974 | **1.019E-001** |

**The rule is constrained - but to the PRODUCT, which is multiplicativity again.** The row is carried in the
selection table as **REFUTED (0 of 13)**, measured against each law's **own** rate at the combined density, so it is
law-level rather than a constant: for a law that does not carry the pinned data the row is **∞**, i.e. it is not an
admissible candidate for it, which the audit states rather than scoring as a pass.

## 4. The one row that selects, and how far it gets

| constraint | survivors | kind |
|---|---|---|
| **composition of successive redshifts** | **4 of 13** | **PARTIAL** |
| multiplicativity of the metric magnitude | 4 of 13 | PARTIAL |
| log-additivity of the rate | 4 of 13 | PARTIAL |
| path independence of the accumulated ratio | 4 of 13 | PARTIAL |
| operational clock transport consistency | 4 of 13 | PARTIAL |
| **composition AND the pinned data (the conjunction)** | **1 of 13** | **SELECTS UNIQUELY** |

**The composition law ALONE does not select.** The slope family satisfies it to machine precision and spreads over
the target:

| law | composition residual | 1 + z at the target | carries the pinned slope |
|---|---|---|---|
| exp(0.5x) | 2.204E-016 | 1.063696687867 | no |
| exp(x) | 2.015E-016 | 1.131450643780 | no |
| exp(1.5x) | 2.171E-016 | 1.203520302273 | no |
| **exp(2x)** | **2.155E-016** | **1.280180559309** | **yes** |
| exp(3x) | 2.123E-016 | 1.448461117984 | no |

**So the constant is fixed by the MEASUREMENT and the law by the ASSUMPTION.** And that assumption is a **new
structural primitive about how clocks combine**, implied by no surviving result - which is the audit's answer to
question (2): **yes, multiplicativity with the pinned data yields exp(2x) uniquely, and it yields it by assuming the
composition law rather than by deriving it.**

## 5. Every weaker form survives, measured

**(a) The mesh witness - the selector is only as strong as the resolution it is tested at.** The interpolating law
**e^(2x)·(1 + β sin²(πx/δ))** with **β at 99.9 % of the monotonicity bound 2δ/π** is **positive, monotone, carries
F(0) = 1 and F'(0) = 2**, and **agrees with exp(2x) at every point of a mesh of spacing δ = 0.01**:

| quantity | value |
|---|---|
| **composition residual ON the mesh** | **1.110E-016** (an observer testing there cannot see the difference) |
| **composition residual BETWEEN the mesh points** | **4.145E-003** |
| worst deviation from exp(2x) | **6.227E-003** |
| **1 + z shift at J0740+6620** | **2.655E-003** |

**Monotonicity bounds the modulation** (the derivative carries βπ/δ, so β < 2δ/π), i.e. **the deviation a
mesh-multiplicative law can hide is set by the operational resolution**, and the audit reports the bound rather than
choosing a small number.

**(b) Approximate multiplicativity leaves a CONTINUUM.** The surviving set is an **interval**, not a point, and the
edge is **bisected on the residual the audit actually uses**:

| tolerance | surviving interval \|c\| ≤ | span in 1 + z at the target |
|---|---|---|
| 1E-002 | 4.893E-002 | 3.822E-003 |
| **1E-003** | **4.871E-003** | **3.805E-004** |
| 1E-004 | 4.869E-004 | 3.803E-005 |
| 1E-006 | 4.869E-006 | 3.803E-007 |

**A tighter tolerance buys a narrower interval, never a point** - which is the useful form of the answer for an
observer: the constraint is only ever as strong as the precision it is verified to.

**(c) The subgroup witness - what actually closes the selector is CONTINUITY.** With the exponent **additive on the
subgroup generated by 1 and √2**, the law is multiplicative there and carries F(0) = 1 exactly, while its values are
**unbounded in every neighbourhood of the vacuum**:

| n | subgroup element x | exponent A | F at that x |
|---|---|---|---|
| 2 | 1.716E-001 | 0.200000 | 1.221403E+000 |
| 12 | 2.944E-002 | -0.800000 | 4.493290E-001 |
| 169 | -2.092E-003 | -12.100000 | 5.559513E-006 |
| 985 | **-3.589E-004** | **-70.500000** | **2.411232E-031** |

**F runs to 2.411E-031 at x = -3.6E-004 while F(0) = 1.** The only exponent that keeps the law bounded, monotone and
continuous is the **linear** one, which is **λ = 2√2 = 2.828427** - i.e. **regularity, a separate primitive, is doing
the closing and not the composition law.** The audit does **not** claim this witness is a physical law: it claims
that the constraint, imposed on the potentials an observer can prepare and compare, does not determine the law.

## 6. The cost of every added constraint, with a running total

| constraint set | survivors of the corpus | spread at the target | primitives | what it costs |
|---|---|---|---|---|
| **the surviving empirical sector alone (G_075)** | **10** | 2.590E+001 | **0** | an infinite family, as G_075 measured - the corpus is a sample of it |
| **+ composition / multiplicativity ALONE** | **4** | 4.485E-001 | **1** | a composition law for clocks: a **new structural primitive** - and it does **not** finish the job (exp(x), exp(2x), exp(3x) and the trivial law all satisfy it) |
| **+ composition AND the pinned data** | **1** | **0.000E+000** | **1** | exactly one law **in the potential x**: the pinned slope fixes the constant, so the selection is the composition law **plus the measurement** |
| **+ composition on the CONTINUUM (regularity)** | **1** | 0.000E+000 | **2** | continuity at one point closes the mesh and subgroup freedoms; without it the composition law is only as strong as the resolution it is tested at |
| **+ a FIXED density-to-potential map** | **1** | 0.000E+000 | **3** | the map x = (1/d) ln ρ with its scale d fixed: **rate = ρ^a with a·d = 1** leaves the pair **(a, d)** free (G_074), so the **named** form is fixed only up to this map |

**From the infinite family to one law costs THREE primitives - multiplicativity, regularity, and a fixed
map - and NOT ONE of the three is derived by any surviving result, and NO combination of the other allowed sources
substitutes for any of them.**

## 7. Defects in the audit's own first version

1. **THE COMPOSITION RESIDUAL WAS ABSOLUTE**, and an absolute residual cannot see a multiplicative constraint in the
   deep field where **both rates are tinier than any tolerance**: the free-room law `exp(2x - 100x²)` was reported as
   satisfying the constraint at **1.786E-001** while its **log-additivity residual was 12.5**. Every multiplicative
   row is now measured **relative to the size of its own factors**.
2. **THE MEASUREMENT'S OWN PAIR SET WAS MADE OF MESH POINTS.** The first version used (0.01, 0.02), (0.05, −0.02), …,
   which are **all multiples of the mesh spacing**, so the audit's own witness satisfied **every** composition test
   exactly and was invisible to the measurement that existed to catch it. The pair set is now deliberately
   **off-mesh**.
3. **THE PATH-INDEPENDENCE ROUTES HAD THE SAME DEFECT**: 0.24, 0.12 and 0.06 are all mesh points, so the
   interpolant passed that row too (residual 1.6E-003 before, 1.6E-003 after the pairs fix, and only 1.110E-016 on
   routes that are genuinely off-mesh). The routes are now an irregular split.
4. **THE PINNED DATA WAS MEASURED WITH THE STANDARD STENCIL**, whose step (h = 5E-3) is **half the mesh spacing**
   (δ = 1E-2) and therefore reads the modulator as a slope: the measured value is **2(1+β)** rather than 2, so the
   interpolant was excluded from the pinned set by the **step of the probe** rather than by its own behaviour. The
   pinned residual now uses **h = 1E-5**, where the modulation contributes (πh/δ)² and is measured in the audit.
5. **THE CONJUNCTION HAD TO BE ADDED AS ITS OWN ROW, and that is a result rather than a convenience**: the first
   version labelled the composition row itself as SELECTS UNIQUELY, which was **false** - exp(x), exp(2x), exp(3x)
   and the trivial law all satisfy it - so the audit would have claimed a selection that the constant of exp(kx)
   refutes.
6. **THE ACTUALIZATION ROW WAS A CONSTANT**, so the law-independent branch of the table classified it as
   NON-SELECTIVE even though **no admissible law passes it**; it is now measured against each law's own rate, and a
   law that does not carry the pinned data is marked **∞ (not an admissible candidate)** rather than scored.

## Where it stands

**The allowed sources do not contain a selector. Three of them are satisfied by everything, one is refuted by the
recorded rate, and the four that share a single functional equation are that equation - which, applied where it can
actually be tested, leaves a mesh family, a continuum of approximately-compatible laws, and an unbounded subgroup
freedom. What closes all of them is a REGULARITY assumption, and what makes the closed result the theory's clock law
is the MAP.**

**So the clock sector is structurally underdetermined**, in a form sharper than "the map is an input": the sector has
**no structural route to its own law**, and the one constraint that does select **presupposes the composition law it
would have to derive**. Its cost table prices the gap exactly - **three primitives to go from an infinite family to
one law, with none derived** - and it says which measure would close it instead: the **compact-object redshift**,
the only surviving row that reaches the second order (G_072), or a **derivation of the composition law** rather than
of the law.

**What this leaves open is the sector's own next question**: *why does the clock composition multiply?* The audit
shows that this single question carries the whole of the theory's time-law uniqueness, that no other allowed source
substitutes for it, and that answering it in the continuum requires a regularity primitive the theory also has not
derived.
