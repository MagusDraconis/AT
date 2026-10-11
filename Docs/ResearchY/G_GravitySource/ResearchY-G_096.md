# ResearchY-G_096 — Multiplicativity Origin Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_096_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/MultiplicativityOriginAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_096`) · **Verdict:** **MULTIPLICATIVITY IS INDEPENDENT INFORMATION — IT IS ASSUMED, NOT DERIVED**

---

## Question

G_086 collapsed the observable kernel to one function; G_095 measured that **multiplicativity** — the requirement that
the clock composition multiply, `K(A+B) = K(A)K(B)` — is the only known principle that would force the free coefficient
`λ = 0`, and that the surviving theory does not contain it.

> **Can multiplicativity be derived from the surviving AT structure, or is it independent information?**

**Constraints, obeyed literally:** every multiplicity, dimension, rank and count is computed on the **96-cell ring
alone**; the **cubic substrate is not used** (the vector sector is not part of this question); and **no new primitive,
field, source term, topology axiom, gauge sector, action, Lagrangian or memory mechanism** is introduced, assumed,
fitted or postulated. **A negative result is fully acceptable.**
**Prefer refutation.**

---

## Answer

**MULTIPLICATIVITY IS INDEPENDENT INFORMATION — IT IS ASSUMED, NOT DERIVED.**

| Task | Result |
|---|---|
| 1 Core objects tested | **5** objects, **0** implying the requirement |
| 2 Family dimension | **47**, exactly (49 clustered distinct potentials − 2); the rank **9** **drifts** and is not a dimension |
| 3 Constraint cascade | **47 → 47 → 47 → 47 → 47 → 47 → 0** — six surviving constraints remove **nothing** |
| 4 The defect | **2.798629E-002** for the reference member against **5.551115E-017** (the rounding) for the sector's law |
| 5 Selector inventory | **7** candidates · **4** surviving · **3** forcing · **intersection 0** |
| 6 Family size | **47** before, **0** after — the removal is complete |
| 7 Classification | **ASSUMED** — not derived, not emergent, not boundary |

---

## 1. Every surviving object tested (task 1)

| object | implies `K(A+B) = K(A)K(B)`? | measurement |
|---|---|---|
| the state (the occupation) | **no** | it is the **argument** of the law; the defect there is **4.68E-003** for the free room |
| the labelling (the units) | **no** | the requirement is invariant under a unit change while the defect is not |
| the potential map (ρ → A) | **no** | it **fixes the coordinate** in which the requirement is stated, and it is an **input** (G_075, G_076) |
| the clock law (A → K) | **no** | it is the object **constrained**; the family shares every map and state property and still has a defect |
| the gradient definition | **no** | it reads the potential difference **for any clock law** (G_077), so it is blind to the clock's form |

**5 objects, 0 implying the requirement.**

---

## 2. The admissible family and its dimension (task 2)

Satisfying **only** `K(0) = 1` and `K′(0) = 1` — the chain's own **UNIQUELY FORCED** orders 0 and 1:

- the ring carries **96 cells** and **49 distinct potentials**, clustered at **1e-12** (**78** by exact `double`
  equality, an artifact-prone count that is **reported rather than used** — Rule 1);
- a function of the potential is determined by its values there, so the family `ln K = A + Σ c_n A^n` has
  **49 − 2 = 47** free coefficients — **exactly, without a tolerance**;
- the coefficient matrix's **rank is 9** and **drifts across its tolerance band**, because the ring's potentials span
  the narrow interval **[−3.054E-001, 1.567E-001]** where high powers alias.

**A number that moves with its tolerance is not a dimension.** The rank is therefore a **resolvability**
measurement: it is reported with its band and **not quoted** as the family's dimension. The chain's own ladder
(orders 0–1 forced, 2–4 weakly constrained by a projection, 5–6 free) is a **truncation** of the same family and
gives **5** as a lower bound.

---

## 3. The family-dimension evolution table (task 3)

| constraint | dimensions removed | remaining |
|---|---|---|
| the two surviving normalisations (baseline) | 0 | **47** |
| count conservation | 0 | **47** |
| the kernel collapse (G_086) | 0 | **47** |
| the source relation (G_077) | 0 | **47** |
| observable equivalence | 0 | **47** |
| the gauge and coordinate reductions | 0 | **47** |
| **MULTIPLICATIVITY (task 6)** | **47** | **0** |

**Six surviving constraints remove zero dimensions between them**, and the audit's central collapse is that the two
rows which *would* collapse the family are **the same premise as multiplicativity**:

- the **kernel collapse** is satisfied by the sector's law to **9.021E-013** and fails for the free room by
  **7.636E-002** — imposing it would remove every dimension, and it says `ln K = A`;
- the **source relation** in its strong form `(ln K)′ = A′` says the same thing, with the normalisation.

**And the source sharing that genuinely survives constrains nothing**: the **zero set** agrees for **4 of 4**
configurations across all **11** tested laws, while the derivative-level **ratio** it would need spreads across the
corpus and is measured as **unselected** (G_077's own finding). **Observable equivalence** likewise removes nothing,
because the free room **moves predictions** — a family direction that moves a readout is not quotiented away.

---

## 4. The defect on the ring's own pairs (task 4)

`R(A, B) = ln K(A+B) − ln K(A) − ln K(B)`, maximised over the **ring's own** potential pairs:

| law | max \|R\| | vanishes | worst pair |
|---|---|---|---|
| the free room, λ = 0.300 | **2.798629E-002** | no | (−3.054E-001, −3.054E-001) |
| **the sector's own law, λ = 0** | **5.551115E-017** | **yes** | (−3.054E-001, −2.960E-001) |
| the free room, λ = 0.5 | 4.664E-002 | no | (−3.054E-001, −3.054E-001) |
| the free room, λ = −1 | 9.329E-002 | no | (−3.054E-001, −3.054E-001) |

The reference member matches the **closed form** `λ·A·B` at the deepest pair. **The sector's law vanishes at
5.551115E-017 — which is the ln∘exp ROUNDING and not exact zero**, so the vanishing is claimed against the rounding
scale and never against equality. The defect grows with the coefficient across the tested members.

**Convention, stated once because it is a factor of two:** the family is `ln K = A + Σ c_n A^n`, so the chain's λ is
**2c₂**, and the reference member is c₂ = λ/2 = **0.15**.

---

## 5. The selector inventory (task 5)

| selector | survives | forces R = 0 | removes | same premise |
|---|---|---|---|---|
| **MULTIPLICATIVITY** | **no** | yes | 47 | **yes** |
| the strong source relation (`(ln K)′ = A′`) | **no** | yes | 47 | **yes** |
| the kernel collapse (G_086) | **no** | yes | 47 | **yes** |
| the source sharing (`a = −grad A`) | yes | no | 0 | no |
| count conservation | yes | no | 0 | no |
| observable equivalence (the readouts) | yes | no | 0 | no |
| the gauge and coordinate reductions | yes | no | 0 | no |

**7 candidates · 4 surviving · 3 forcing · intersection 0 — no surviving structure forces `R = 0`.** And the three
forcing rows are **one premise in three guises**: they all say `ln K = A`.

---

## 6. The family before and after (task 6) and the classification (task 7)

**Before:** 47 dimensions on the ring (5 at the ladder's reach, a truncation of the same family).
**After:** **0** — a single member, `ln K = A`, because the requirement forces **every** higher coefficient to vanish.
**The removed fraction is complete.**

**Classification: ASSUMED.**

- **NOT DERIVED** — 0 of 5 core objects, 0 surviving selectors, 0 dimensions removed by six surviving constraints;
- **NOT EMERGENT** — nothing generates it; there is no flow for the coefficient (G_095);
- **NOT BOUNDARY** — it names an **interior** point of the admissible set rather than constraining its edge;
- **ASSUMED** — and **not even a unique assumption**, since the forcing rows form an equivalence class of size 3.

---

## Defects found while building this audit

1. **A rank that drifts was about to be published as a dimension.** `FamilyDimension()` first returned the
   coefficient matrix's **rank** on the ring (**9**). The rank does **not** sit on a plateau — it moves with its
   tolerance because the ring's potentials span a narrow interval and high powers alias — so it is a **resolvability**
   measurement, not a dimension. The audit now quotes the **tolerance-free** combinatorial count (**47**), reports the
   rank with its band, and states the rule it is applying: *a number that moves with its tolerance cannot be a
   dimension* (Rules 1 and 2).
2. **A factor of two in the family's convention.** The family was first built as `ln K = A + c A²`, which makes the
   audit's `c` equal to **λ/2** and doubled every defect (**5.597E-002** where the chain's own value is
   **2.798629E-002**). The convention is now stated once, with `λ = 2c₂` and the reference member at `c₂ = 0.15`.
3. **A `Single` keyed on a shared prefix.** Four defect rows begin with "the free room", so lookups keyed on
   `StartsWith("the free room")` threw `Sequence contains more than one matching element` — the same class of defect
   G_092 recorded. The lookups now use the rows' **full** names through two named accessors.
4. **A count keyed on exact `double` equality.** The ring's distinct potential values are **computed** (`ln ρ / d`), so
   counting them exactly gives **78** — pairs that are mathematically equal but differ in the last bit. The audit
   clusters at **1e-12**, quotes **49**, and reports the exact-keyed count as artifact-prone.
5. **A typed-in verdict the mechanical guard caught.** `SelectorRow.Survives` was supplied as a **literal `false`/`true`
   per row**, which is precisely a literal-to-verdict path — and `LiteralVerdictAudit` (G_027) re-read `AT.Core` at test
   time and **failed the build** with the offending lines named. The fix was **not** to register an exemption: every
   `Survives` value is now **computed** from the owning audit's own measurement (the chain's `ForcedFeatures` status for
   multiplicativity, the ratio's spread for the strong source relation, the free room's kernel dimension for the
   collapse, the zero-set agreement, the transport-layer identity, the reduction's reproduction test, and the presence
   of a gauge and a coordinate row). **The guard is why this audit's central claim is a measurement rather than an
   assertion** — and it needed no registry entry, because no literal path remains.

---

## What this does and does not establish

**Establishes:** that **no** object of the minimal core implies the requirement; that the admissible family's
dimension on the ring is **47** and that **six surviving constraints remove zero** of them; that multiplicativity
removes **all 47**, so it carries **independent information**; that the defect it removes is measured on the ring's own
pairs and compared against its `ln∘exp` **rounding** scale; and that the candidate selectors which *would* force it are
**one premise in three guises** rather than three routes.

**Does not establish** that multiplicativity is **false**, or that no principle could derive it. The audit measures
that the **surviving structure does not contain** the derivation — the failure criterion the question itself names as
acceptable. Nor does it establish that the family's dimension is the right **measure** of information: it is the count
of coefficients the ring can carry, and a finer substrate would give a larger family and therefore a larger
independence.

**Why this is not a restatement of G_095:** G_095 asked which **principle** selects the coefficient and found the
singleton absent. This audit asks whether the surviving structure can **derive** that principle, and answers by
**counting** on the ring: zero core objects, zero surviving constraints, and a family dimension that only the
requirement itself reduces. It also adds what G_095 could not see — that the candidate selectors are **not independent
of one another**.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, densities = `MapRedundancyAudit.State`. The **cubic
substrate is not used** — the vector sector is not part of this question — and no substrate token beyond the ring
appears in the suite. **Equality rules:** the ring's computed potentials are clustered at **1e-12**; the rank is quoted
with its tolerance band; the defect is compared against its own **rounding scale**, never against equality. **No
spectral quantity is quoted** — no A₀, no free room, no lock release, no survivor count — so the spectral-substrate
rule is not engaged.

**Guards:** `Y_G_033` — `without` **64 → 65**; `Y_G_035` — **96 → 97**, survives **82 → 83**. Registry entry:
**derived**.

**What would change the verdict:** a **derivation of the composition law from the primitives** (Difference,
Actualization, the generation relation, count conservation), which would turn this failure into a success; or a
surviving constraint that removes family dimensions, which the cascade measures as **empty**.
