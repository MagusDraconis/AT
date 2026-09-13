# Y_E_006 Result - Connection Origin Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_006_Tests.cs`
**Status:** 8/8 PASSED
**Group total:** group E = **41/41 PASSED** (E_001 7 + E_002 6 + E_003 7 + E_004 6 + E_005 7 + E_006 8)

## Verdict

**DERIVED** - the unique missing object between representation and field theory is the **D96^3 edge connection**, and
it needs **no new primitive**.

## The five requirements against the five candidates

| candidate | local | dir rank | first-order | acts on T1/T2 | no new prim | verdict |
|---|---|---|---|---|---|---|
| D96 ring derivative | yes | **1** | yes | **no** | yes | REFUTED |
| **D96^3 edge connection** | **yes** | **3** | **yes** | **yes** | **yes** | **DERIVED** |
| occupancy gradients | yes | **1** | yes | **no** | yes | REFUTED |
| actualization flow | **no** | 1 | **no** | no | yes | REFUTED |
| causal-order links | **no** | 1 | **no** | no | yes | REFUTED |

## The object

- **local**: support **2** per row
- **directional**: rank **3** (ring: **1**)
- **first-order**: identity to **8.88E-016**; `|sigma(k)|/k` = 0.999583 / 0.999996 / **1.000000**, `|sigma(k)|/k^2` =
  9.996 / 100.0 / **1000.0**
- **acts on T1 and T2**: **antisymmetric = T1** (dim 3), **symmetric = A1 + E + T2** (dims 1+2+3 = 6), total **9**
- **no new primitive**: the tensor product of the ring's own differences

## The other four, measured

`D96 ring derivative` rank 1, largest ring irrep 2. `occupancy gradients` domain = scalars; double gradient
antisymmetric part **1.11E-016** against a traceless part of **0.879**. `actualization flow` **0** members returning
a flow field. `causal-order links` closure reaches **95 of 96** cells against a direct support of **2**.

## Layers

representation **SATISFIED** (already) | **kinematics DERIVED HERE** | dynamics **STILL MISSING** | gauge **STILL
MISSING**.

## Boundary inside the verdict

Live scan of AT's sources: **0** occurrences of any product-lattice construction - the object is **derivable but not
instantiated**. And **0** members compute a field strength, so nothing couples it to a field. Derived, not built;
not coupled.

## Errors the audit caught on itself

The product-lattice scan counted its own token list (rule 11); a helper's name matched E_002's signature regex and
changed the number it was counting (rule 11 again); an over-claim that a gradient cannot reach the traceless sector
was **false** (a Hessian's traceless part is 0.879) and was corrected to the antisymmetric statement; and a linearity
threshold of 10 was just above the k = 0.1 sample's 9.996.
