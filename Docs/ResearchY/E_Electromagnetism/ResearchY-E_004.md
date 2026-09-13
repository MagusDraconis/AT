# ResearchY-E_004 — Vector Sector Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** E — Electromagnetism
**ID:** ResearchY-E_004 (permanent)
**Title:** Can the D96³ T1(3) sector support a genuine spin-1 field?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_004.md`
**Depends on:** E_001, E_002 (the field equation, derived but unearned), E_003 (the obstruction and the missing primitive), M_011, M_012 (the cubic dimension-3 sector), G_032/G_033 (the cubic requirement absorbed into η), `PhotonOntologyAudit` (the octahedral subduction machinery)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_004_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/VectorSectorAudit.cs`

## The question

E_003 proved the obstruction is representation-theoretic: the photon is spin-1, hence `l = 1`, hence three
dimensions — which the single D96 ring cannot supply, while the cubic substrate D96³ supplies **T1(3)**.

**But a representation is not a field.** Can the T1(3) sector support a genuine spin-1 **field**?

Requirements: (1) vector degrees of freedom, (2) two physical polarisations, (3) gauge redundancy,
(4) massless propagation, (5) Maxwell limit. Compare **single D96** against **D96³**.

## The answer: BOUNDARY — and the shortfall is precise

| requirement | single D96 | D96³ |
|---|---|---|
| 1. vector degrees of freedom | **NONE** (max irrep dim **2**) | **T1(3), irreducible — 3 components** |
| 2. two physical polarisations | n/a | **2 transverse of 3** — *given a reason to project* |
| 3. gauge redundancy | n/a | **0 in the representation** (identity form rank 3 → Proca); the curl kernel (1) lives in **field space** |
| 4. massless propagation | n/a | gapped **0.386351** at N = 96; `μ_min ~ 3591/n²` → **0** |
| 5. Maxwell limit | n/a | E_002: derivable, from **imported premises** |

## 1. The vector sector exists — and only on D96³

`l = 1` subducts onto the octahedral group as a **single** multiplet:

| | |
|---|---|
| irrep | **T1** |
| dimension | **3** |
| multiplicity | **1** ⇒ **irreducible** |
| single ring, max irrep dimension | **2** → vector sector **NO** |
| cubic D96³ | **YES** |

Requirement 1 is satisfied — **by the cubic substrate only**.

## 2. A Lorentz 4-vector needs *both* substrates

`A_μ = (A_0, A_i)` reduces as **1 + 3 = 4**:

| part | irrep | dim | supplied by |
|---|---|---|---|
| timelike `A_0` | **A1** | 1 | **the single D96 ring** |
| spatial `A_i` | **T1** | 3 | **the cubic D96³ substrate** |

This is sharper than E_003's statement: **neither substrate alone can carry a Lorentz vector.** The ring gives
only the timelike component; the cubic substrate gives only the spatial one.

## 3. The polarisation count, and the form that decides it

The transverse projector `P = I − k̂k̂ᵀ` has **rank 2 for every one of 12 momentum directions tested**, so the
three components reduce to **two** physical states with one removed.

**But two quadratic forms live on the same three-dimensional space:**

| form | matrix | rank | kernel | physical states | |
|---|---|---|---|---|---|
| identity `V·V` | `I` | **3** | **0** | **3** | ← **PROCA (massive)** |
| curl `F_ij F_ij` | `2(I − k̂k̂ᵀ)` | **2** | **1** | **2** | ← **MAXWELL-capable** |

Evaluated on the two modes (`|k| = 1`):

| mode | identity form | curl form |
|---|---|---|
| **longitudinal** | **1.000000** | **0.000×10⁰** ← annihilated *exactly* |
| transverse | 1.000000 | 2.000000 |

**This is the decisive computation.** The **identity form is the representation's own natural invariant** — so
**T1(3) alone delivers a massive vector.** And because an irreducible representation has **no gauge orbit**:

| gauge directions | count |
|---|---|
| from the **representation** | **0** — the irrep describes a single fibre, and a fibre point has no orbit |
| from the **field space** (the curl form's gradient kernel) | **1** |

**So the choice of kinetic form is new structure** — this is E_003's missing primitive, now identified precisely
as the **choice of kinetic form** rather than as the phase.

## 4. Masslessness is a continuum-limit statement

The vector sector is gapped on a finite lattice:

| n | ring gap `μ_min` | `μ_min · n²` |
|---|---|---|
| 12 | 12.000000 | 1728.0 |
| 24 | 5.404246 | 3112.8 |
| 48 | 1.504529 | 3466.4 |
| **96** | **0.386351** | **3560.6** |
| 192 | 0.097237 | 3584.5 |
| 384 | 0.024350 | **3590.5** |

The product settles to **≈3591** (spread **0.84 %** for n ≥ 96), so `μ_min ~ 3591/n² → 0`: the gap is a
**finite-size artefact, not a predicted mass**. The cubic substrate's gap is **3×** the ring's (the
Cartesian-product sum rule), so it closes too.

**Requirement 4 holds in the thermodynamic limit and on no finite lattice.**

## Output

**BOUNDARY.** D96³ is necessary **and** sufficient at the level of **representation** — a genuine irreducible
three-dimensional vector sector that the single ring cannot provide, and one that supplies the spatial part of a
Lorentz vector. It is insufficient at the level of **dynamics**: the sector's own invariant form gives **three**
states (Proca), and the **two**-state massless case requires the **curl** form, which the representation does not
choose. An irreducible representation carries **no gauge redundancy**; the gauge direction lives in the field
space of sections.

**What is missing is now located exactly:** not the phase (E_003 found AT already has it), not the group, and not
the vector representation — but the **choice of kinetic form** that reduces three states to two.

## Classification and caveats

**Registry:** group E has no G-style classification registry, so no registry entry is added and no prior
classification changes. `Verdict()` is **computed** from nine independent checks, never a literal (G_027).

**Caveats.**
(1) Spin-1 is identified with `l = 1`, so the requirement is a dimension-3 multiplet; a different helicity carrier
would require the counting to be redone.
(2) The physical-state count is the standard component-minus-gauge reduction — a bookkeeping argument, not a
dynamical derivation. The audit's contribution is to show that *which* form does the reducing is a genuine
choice, not a consequence of the representation.
(3) Plane waves with `|k| = 1` are used; the curl form's factor of 2 is a normalisation.
(4) Deterministic: closed-form characters, permutations and matrix ranks; no RNG, invariant-culture output.

**No reclassification.** E_001, E_002, E_003, M_011, M_012, G_032 and G_033 are unchanged inputs; the D_040
registry is untouched; no canonical claim, value or equation changes; no new primitive is added — the audit
locates the one the theory would need.

## Refinement after E_005 (no reclassification)

**E_005 - Propagation Origin Audit** (BOUNDARY) sharpens WHERE the shortfall is rather than WHICH forms compete.
This audit located it at *the choice of kinetic form* that takes three states to two. E_005 refines that to the
**second** missing step: a kinetic form is a functional of a **field strength**, and there is no field strength until
a first-order derivative carrying the **direction index** exists - at a direction rank of 1 the antisymmetric square
is exactly 0. E_005 also reproduces this audit's finding that the **representation supplies no gauge orbit**, and
its computed observation that AT's built-in link phase is **exactly pure gauge** (holonomy 2*pi = the identity,
conjugation residual 2.45E-016) explains why no kinetic form could act on it in the first place. **E_004 remains
BOUNDARY; nothing here is reclassified.**
