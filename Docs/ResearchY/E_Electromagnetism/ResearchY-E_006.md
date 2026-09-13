# ResearchY-E_006 - Connection Origin Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_006 (permanent)
**Title:** What AT structure could supply the missing direction index that E_005 located?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_006.md`
**Depends on:** E_005 (the bottleneck is a first-order derivative carrying a direction index; kinematics is the first failing layer), E_004 (the representation is necessary and sufficient), E_003 (AT has a phase, but no spacetime index and no fluctuation), E_002 (no member computes a field strength), G_033 (the cubic substrate is never instantiated), G_042 (polarisation equality is the Hodge equality), G_041 (the D96^d ladder)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_006_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/ConnectionOriginAudit.cs`

## The question

What AT structure could supply the missing **direction index**? The candidate must be (1) **local**, (2)
**directional**, (3) **first-order**, (4) **acting on T1 and T2**, and (5) require **no new primitive**.
Candidates: the **D96 ring derivative**, the **D96^3 edge connection**, **occupancy gradients**, **actualization
flow**, **causal-order links**. Determine representation / kinematics / dynamics / gauge.

## The answer: **DERIVED - the unique missing object is the D96^3 EDGE CONNECTION, and it needs no new primitive**

## 1. The five requirements against the five candidates

Every mark is computed; the evidence for each row is printed by the audit.

| candidate | local | direction rank | first-order | acts on T1/T2 | no new primitive | verdict |
|---|---|---|---|---|---|---|
| D96 ring derivative | yes | **1** (needs 3) | yes | **no** | yes | **REFUTED** |
| **D96^3 edge connection** | **yes** | **3** | **yes** | **yes** | **yes** | **DERIVED** |
| occupancy gradients | yes | **1** | yes | **no** | yes | **REFUTED** |
| actualization flow | **no** | 1 | **no** | no | yes | **REFUTED** |
| causal-order links | **no** | 1 | **no** | no | yes | **REFUTED** |

## 2. The object, requirement by requirement

| requirement | measurement |
|---|---|
| **local** | support **2** per row: each factor's difference is nearest-neighbour |
| **directional** | rank **3** - the three factor differences are independent, against the ring's **1** |
| **first-order** | symbol identity to **8.88E-016**; `\|sigma(k)\|/k` = 0.999583 / 0.999996 / **1.000000** and `\|sigma(k)\|/k^2` = 9.996 / 100.0 / **1000.0** at k = 1e-1 / 1e-2 / 1e-3 - linear, not quadratic |
| **acts on T1 and T2** | the tensor square of the vector irrep: **antisymmetric = T1**, **symmetric = A1 + E + T2** |
| **no new primitive** | the tensor product of the ring's own difference operators, three times over |

## 3. Requirement 4 is the decisive line - and it is computed from characters

The tensor square of the vector irrep, class by class of the proper rotation group (order **24**), with the
antisymmetric part separated by `(chi(g)^2 - chi(g^2))/2` and the symmetric part by `(chi(g)^2 + chi(g^2))/2`:

| class | size | chi(g) | chi(g)^2 | chi(g^2) | antisym. | sym. |
|---|---|---|---|---|---|---|
| E | 1 | 3 | 9 | 3 | 3 | 6 |
| 8C3 | 8 | 0 | 0 | 0 | 0 | 0 |
| 3C2 | 3 | -1 | 1 | 3 | -1 | 2 |
| 6C4 | 6 | 1 | 1 | -1 | 1 | 0 |
| 6C2' | 6 | -1 | 1 | 3 | -1 | 2 |

**Antisymmetric part = T1** (multiplicity 1, dimension **3**) - the photon's field strength, in the vector irrep
itself, which is **G_042's Hodge identity** at d = 3. **Symmetric part = A1 + E + T2** (multiplicities 1, 1, 1;
dimensions 1 + 2 + 3 = **6**) - the trace plus the **graviton's sector**. And 3 + 6 = **9** = 3 x 3.

> **One derivative acting on the vector sector reproduces BOTH sectors.** That is the uniqueness E_005 asked for,
now derived rather than asserted.

## 4. Why the other four fail - measured, not asserted

- **The ring derivative** is genuinely local and genuinely first-order (its square *is* the radius-6 Laplacian) but
  its direction rank is **1** and the ring's largest irrep is **2**-dimensional, so it has no vector sector at all.
- **Occupancy gradients fail for a sharper reason than rank.** Their domain is **scalars**, so they cannot act on
  the vector sector, and on a uniform lattice a double gradient has an **identically vanishing antisymmetric part**
  - measured **1.11E-016** - against a traceless part of **0.879**. A scalar-derived connection can never carry a
  field strength, which is *the same mechanism* that made E_005's built-in phase exactly pure gauge.
- **Actualization flow** fails twice: AT's flow objects are report models and metrics rather than fields (a live
  scan finds **0** members returning a flow field), and a flow is one vector per site, not an index.
- **Causal-order links** fail three requirements: an order is not local - the transitive closure of the ring's own
  links reaches **95 of its 96** cells against a direct-link support of **2** - it carries no index, and it has no
  dispersion relation.

## 5. The layer verdicts

| layer | status | basis |
|---|---|---|
| representation | SATISFIED (already) | T1 is 3 and the traceless rank-2 is 5; the cube's irreps are already computed |
| **kinematics** | **DERIVED HERE** | the D96^3 edge connection: local, rank 3, first-order, reaches both sectors |
| dynamics | STILL MISSING | **0** AT members compute a field strength or its divergence (E_002) |
| gauge | STILL MISSING | the orbit is the image of the derivative, so it needs the field |

## 6. The boundary stated inside the DERIVED verdict

A live scan of AT's sources (code only, comments and string literals stripped per line) finds **no
product-lattice construction at all** - not `Kronecker`, not `TensorProduct`, not `ProductLattice`, not
`CubeLattice`. So the object is **derivable but NOT INSTANTIATED**, which is exactly G_033's finding about the
substrate, reached here from the code side. And **0** AT members compute a field strength or its divergence, so
nothing couples such an operator to a field either.

The object is therefore **derived, not built, and not coupled** - which is why the verdict is DERIVED for the
direction index the question asks about, while **dynamics and gauge remain missing**.

## 7. Refinement of E_005 (no reclassification)

E_005 concluded that "the ingredient itself must be supplied". **E_006 refines that:** the ingredient does not have to
be *invented* - it is the tensor product of AT's own difference operators, so **no new primitive is required** - but
it does have to be *built and coupled*, which AT has never done. **E_005 remains BOUNDARY**; its bottleneck is
resolved at the kinematic level rather than reclassified.

## 8. Errors this audit caught while running

1. **The scan counted itself.** The first run of the product-lattice scan reported one match, and the match was this
   audit's own token list. Fixed by excluding the audit's own file - **rule 11**, and recorded because the rule
   exists precisely for this.
2. **A member name changed the number it was counting.** The helper first called
   `MembersThatComputeAFieldStrength` matched **E_002's own signature regex** (it matches any `int`-returning member
   whose name contains `FieldStrength`), so the coupling count read **1** instead of **0**. Renamed; the incident is
   in the code comment. Rule 11 again, in a place nobody would look.
3. **An over-claim, corrected.** A first draft said a gradient "can never reach the symmetric traceless sector". That
   is **false** - a Hessian's traceless part is nonzero (**0.879** measured here). The honest statement is the one
   the measurement supports: the *antisymmetric* part of a double gradient vanishes identically, so a scalar-derived
   connection has no field strength.
4. **A threshold set too tight.** The linearity check first required `\|sigma(k)\|/k^2 > 10`; at k = 0.1 the value is
   **9.996**, so the test failed on its own sample. The criterion now also requires the ratio to grow with shrinking
   k, which is what "diverges" actually means.

## Success criterion

> Locate the unique missing object between representation and field theory.

**Located and derived: the D96^3 edge connection** - local, directional (rank 3), first-order, and acting on both
T1 and T2 (antisymmetric part = T1; symmetric part = A1 + E + T2), built from AT's own structures with **no new
primitive**. It is **derived but not instantiated**, so what remains is the construction and the coupling.
