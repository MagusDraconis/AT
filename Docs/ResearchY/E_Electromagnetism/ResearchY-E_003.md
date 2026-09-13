# ResearchY-E_003 — Photon Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** E — Electromagnetism
**ID:** ResearchY-E_003 (permanent)
**Title:** What would the photon BE in AT? Trace Difference → Actualization → Spectrum → ?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_003.md`
**Depends on:** E_001 (the inventory), E_002 (the field equation, derived but unearned), M_011 (no O(3) on a single ring), M_012 (the cubic network supplies the dimension-3 sector), G_032 (the conformal sector is assumed), G_033 (D96³ — required but absorbed into η), QG161 (`GaugeSectorOrigin`), AT-X050 (`GaugeSymmetryAnalyzer`), `PhaseOrigin`, `MinimalPsiEquation`
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_003_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/PhotonOntologyAudit.cs`

## The question

Trace **Difference → Actualization → Spectrum → ?** and test the five requirements every photon must meet:
massless, spin-1, gauge invariance, Maxwell limit, light propagation.

**Critical question (posed by the programme):** can AT supply a photon **without importing U(1)** — and is
there an AT-native route to the electromagnetic sector, of the kind D96³ provides elsewhere?

## The answer: BOUNDARY — and the constructive question has a real answer

| requirement | status |
|---|---|
| massless | **DERIVED GIVEN GAUGE INVARIANCE** |
| spin-1 | **ONLY ON THE CUBIC SUBSTRATE** (which AT does not build) |
| gauge invariance | **GROUP DERIVED, LOCALITY NOT** |
| Maxwell limit | **NOT DERIVED — IMPORTED PREMISES** (E_002) |
| light propagation | **ABSENT** — no computed spin-1 mode |

## 1. The obstruction is representation-theoretic, and it is decisive

The photon is **spin-1** → the **l = 1 (vector / p-wave)** sector → **three dimensions**.

| | single ring | cubic substrate 96³ |
|---|---|---|
| group | D_96 = dihedral, **order 192** | O_h (M_012) |
| irreps | **4 one-dim + 47 two-dim** | includes **T1(3)**, **T2(3)** |
| Σd² | **4·1 + 47·4 = 192** ✓ group order | — |
| **max irrep dimension** | **2** | **3** |
| vector sector (l = 1) | **IMPOSSIBLE** | **PRESENT** |

The budget `4·1 + 47·4 = 192` **checks the group order**, so it is verified rather than quoted (and it holds for
every even n from 4 to 200). The ring's adjacency spectrum pairs `k ↔ 96−k` — the degeneracy that produces the
**47 doublets** — and it is **scalar** throughout: 45 distinct eigenvalues, all site modes.

| l | sector | octahedral | ring | cubic 96³ |
|---|---|---|---|---|
| 0 | scalar (ρ) | A1 | **YES** | YES |
| 1 | **VECTOR (photon)** | **T1** | **no** | **YES** |
| 2 | traceless metric (graviton) | E + T2 | **no** | **YES** |

**AT's own audits already contain both halves of this.** M_011 found no O(3) and no vector sector on a single
ring; M_012 found a genuine dimension-3 irreducible sector on the **cubic network** `C96 □ C96 □ C96` — `l = 1 →
T1(3)` with `⟨χ,χ⟩ = 1` — and identified its group as O_h (order 48).

**And the graviton is blocked by the same thing, one multipole higher:** the traceless metric is `l = 2 → E(2) +
T2(3)`. So **the photon and the graviton need the same missing substrate.**

G_033 established what became of that requirement: the metric era **never instantiates 96³**, because it imports
three-dimensional space through the primitive **η** — which G_032 proved is an **assumed input**. **The photon
therefore inherits exactly the gap gravity has.**

## 2. The U(1): derived, compact — but global, not local

`D96 = Aut(C96(1..6))`, verified **on the permutations** rather than quoted:

| check | result |
|---|---|
| rotation r order | **96** |
| reflection s order | **2** |
| `s r s⁻¹ = r⁻¹` | **true** |
| order of the generated group | **192** (so "D96" means the dihedral group of order 192) |
| **Z_96 = the rotation subgroup = the U(1) charge** | finite, hence **COMPACT** |

**But the symmetry is rigid.** The rotation **is** an adjacency automorphism; **0 of 6** site-local transpositions
are. A photon is not a rigid symmetry — it is a connection on **spacetime** links, which requires independent
phases per site.

*(A first version of this test generated its "site-dependent" family as `p[i] = i + 1 + (a·i) mod 5`; for a = 5
that degenerates to the rigid rotation and reported **one** phase that worked. The test caught it.)*

### A programme claim falsified: "the 12 link-directions ARE the 12 gauge generators"

QG161 records that the twelve link-directions of the 12-regular `C96(1..6)` **are** the twelve gauge generators,
matching `1 + 3 + 8 = 12`. Tested:

| check | result |
|---|---|
| offsets | `1,95,2,94,3,93,4,92,5,91,6,90` |
| closed under addition mod 96 | **FALSE** — witness `1 + 95 = 0`, outside the set |
| additive closure inside Z_96 | **96** (not 12) |
| the algebra generated is | **abelian** |
| the gauge algebra's non-abelian part | **11** dimensions (8 + 3) |

**The two twelves are a cardinality match with no algebraic map between them.** The offsets are not closed under
addition, so they cannot be a Lie algebra; and an abelian set cannot generate `su(3) ⊕ su(2)`.

**By contrast, the su(2)-from-doublets claim PASSES a structural test:** the Lie closure of the doublet
generators (`iσ_z` for the rotation, `σ_x` for the reflection) has dimension **3** — genuinely su(2). So the weak
sector has a real basis while the "12 = 12" identification does not.

## 3. What AT *already* has — and the gap is narrower than it looks

**This corrects a first reading of the audit.** AT **already puts a U(1) phase on its links** (`PhaseOrigin`):

| quantity | value |
|---|---|
| phase quantum `2π/N`, N = 96 | **6.544985×10⁻²** |
| a link advances forward / backward | **+6.544985×10⁻² / −6.544985×10⁻²** |
| path phase over a full cycle | **6.283185** = 2π exactly |
| loop holonomy `2π(L mod N)/N` | **compact** |
| interference law `2 + 2cos(δ)` | **4.000** at δ = 0, **0.000** at δ = π |

So **AT natively has a link phase, a path phase, a loop holonomy, a two-slit interference law and a phased Born
rule.**

And the substrate's **Laplacian** (`μ_k = 2k − λ_k`) has **exactly ONE zero eigenvalue** — the constant mode —
with **spin 0**, and a spectral gap of **0.386351** above it.

| AT's massless wave objects | spin | status |
|---|---|---|
| the substrate's Laplacian zero mode (the background) | **0** | **COMPUTED** |
| `TemporalField`: a discrete scalar wave equation on the phase field | **0** | **COMPUTED** |
| `□ψ_μν = 0` (Fierz–Pauli) | **2** | **POSTULATED** (`Derived() = false`, `Postulated() = true`) |
| a massless spin-1 field | **1** | **ABSENT** |

*(A second correction: E_001/E_002 described the spin-2 `□ψ_μν = 0` as AT's "computable massless wave equation".
It is **postulated** — the class returns booleans, not a solution. AT's genuinely computed massless waves are
both **scalars**.)*

## 4. The decisive count — polarisations

A massless spin-1 particle has exactly **two** transverse polarisations. Every cheap AT-native route gives the
wrong number:

| candidate | components | removed | physical | photon? |
|---|---|---|---|---|
| scalar phase (Goldstone / ring modulus) | 1 | 0 | **1** | no |
| density gradient ∇ρ on links | 3 | 2 | **1** | no |
| **spacetime connection A_μ, gauge-reduced** | 4 | 2 | **2** | **YES** |
| massive vector (Proca) | 4 | 1 | 3 | no |

A single scalar phase carries one **longitudinal** mode; so does ∇ρ. Neither is a photon. **This is what pins the
missing primitive to a spacetime-indexed connection.**

## Output

**BOUNDARY.** AT does **not** have to import U(1) as an axiom — the rotation subgroup of `Aut(C96(1..6))` **is**
a Z_96, it is **compact**, and the weak sector has a genuine su(2) on the doublets. It **cannot** supply the
photon without the **cubic substrate**, which its own M_011/M_012 prove is required and its own G_032/G_033 prove
is **never instantiated** (3-D space is the assumed primitive η).

**The most useful result here is that the photon and the graviton are blocked by the SAME missing substrate** —
so one construction would unblock both.

**The missing primitive is exactly one, and it is smaller than it first appeared:** a phase on **spacetime**
links **with its own dynamics**. AT already has the phase, the holonomy and the interference law; it lacks
**(a) the spacetime index** (its links are 1-D ring links) and **(b) the fluctuation** (the step is the fixed
constant 2π/N, not a field).

## Classification and caveats

**Registry:** group E has no G-style classification registry, so no registry entry is added and no prior
classification changes. The audit's `Verdict()` is **computed** from ten independent checks, never a literal
(G_027).

**Caveats.**
(1) "D96" is overloaded — it denotes the 96-site circulant ring **and** its dihedral automorphism group of order
192; this audit verifies the group statement on the permutations.
(2) Spin-1 is identified with the `l = 1` representation, so the requirement is a **dimension-3 multiplet under
whatever spatial group is available**. That is the standard identification and it is what makes the obstruction
sharp; if a different carrier for helicity were proposed the counting would have to be redone.
(3) The polarisation table uses the standard component-minus-gauge reduction; it is a bookkeeping argument, not a
dynamical derivation.
(4) Deterministic: closed-form characters and permutations, no RNG, invariant-culture output.

**Two self-corrections, both caught by this audit's own tests:** the degenerate site-dependent family in §2, and
the wrong spectral-degeneracy bound (`k ↔ 96−k` makes 49 an **upper** bound; the measured count is 45).

**A third correction came from the reconnaissance and is recorded rather than absorbed:** AT *does* have a link
phase, so the missing primitive is the spacetime index and the dynamics, not the phase.

**No reclassification.** E_001, E_002, M_011, M_012, G_032 and G_033 are unchanged inputs; the D_040 registry is
untouched; no canonical claim, value or equation changes; no new primitive is added by this audit — it identifies
the one AT would need.

## Located by E_005 (no reclassification)

**E_005 - Propagation Origin Audit** (BOUNDARY) takes this audit's missing primitive - *a phase on spacetime links
with its own dynamics*, i.e. the **spacetime index** and the **fluctuation** - and locates it in the four-layer
scheme as the **KINEMATICS** layer, the **first missing step**. The spacetime index **is** the direction index, and
E_005 adds the computed reason the existing phase cannot stand in for it: the built-in connection is **exactly pure
gauge** (step 2*pi/96 = 0.065449847, holonomy 6.283185 = 2*pi = the identity, conjugation residual 2.45E-016), so its
field strength is identically zero and the only gauge-invariant content a phase on a closed line could have is
exactly the residue that vanishes. **E_003 remains BOUNDARY; nothing here is reclassified.**
