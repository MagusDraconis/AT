# ResearchY-QM_010 - Fingerprint Load Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_010 (permanent)
**Title:** Which surviving AT claims actually depend on the native {1..6} spectrum?
**Status:** COMPLETE
**Date:** 2026-09-17
**File:** `QM_ManyBody/ResearchY-QM_010.md`
**Depends on:** QM_007 (the fingerprint is an input), QM_008 (it is operative), QM_009 (no prediction fails), G_017/G_018 (the clock law), G_068 (the redshift law), G_059 (the source law), G_052 (the split), G_040/G_061 (the kernel and observability theorems), G_051 (flux quantisation)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_010_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/FingerprintLoadAudit.cs`

## The question

**Which surviving AT claims actually depend on the native {1..6} spectrum?** For each of the **clock law**, the
**redshift law**, the **source law**, the **amplitude/phase split**, the **kernel theorem**, the **observability
theorem**, the **phase accessibility** and the **flux quantisation**, remove the fingerprint-specific constants,
compare **theorem content only**, and classify **UNCHANGED / NUMERICALLY_CHANGED / STRUCTURALLY_CHANGED / REFUTED**.
Goal: does the fingerprint carry **physical content** or only **numerical realisation**? Output **CORE / FINGERPRINT /
ARTEFACT**.

## The answer

> **FINGERPRINT - THE SPECTRUM CARRIES NUMERICAL REALISATION AND NOT PHYSICAL CONTENT: 3 CORE + 5 FINGERPRINT +
> 0 ARTEFACT, and by theorem classification 3 UNCHANGED + 3 NUMERICALLY_CHANGED + 2 STRUCTURALLY_CHANGED +
> 0 REFUTED.** **The zero REFUTED is the central positive result, not an absence of findings:** for every claim the
> fingerprint touches, the claim is still **true** on the replacement.

## The rules, stated before the claims are looked at

```
UNCHANGED             the theorem holds on both substrata and its constants are identical
NUMERICALLY_CHANGED   the theorem holds and its constants move
STRUCTURALLY_CHANGED  the theorem holds but the OBJECTS it names change identity
REFUTED               the theorem fails on the replacement

CORE         the theorem holds and nothing it quotes moves
FINGERPRINT  the theorem holds and its constants or objects move
ARTEFACT     its constant moves over the 63-subset family while its predicate is VACUOUS there
```

The classification and the load are **functions applied to measured booleans and measured constants** - they are not
hand-assigned - so no claim can be classified by reading its own text.

## The load table

| claim | holds {1..6} | holds {1} | constants | objects | classification | load |
|---|---|---|---|---|---|---|
| **clock law** | yes | yes | no | no | **UNCHANGED** | **CORE** |
| **redshift law** | yes | yes | no | no | **UNCHANGED** | **CORE** |
| **flux quantisation** | yes | yes | no | no | **UNCHANGED** | **CORE** |
| source law | yes | yes | **yes** | **yes** | **STRUCTURALLY_CHANGED** | FINGERPRINT |
| amplitude/phase split | yes | yes | **yes** | **yes** | **STRUCTURALLY_CHANGED** | FINGERPRINT |
| kernel theorem | yes | yes | **yes** | no | **NUMERICALLY_CHANGED** | FINGERPRINT |
| observability theorem | yes | yes | **yes** | no | **NUMERICALLY_CHANGED** | FINGERPRINT |
| phase accessibility | yes | yes | **yes** | no | **NUMERICALLY_CHANGED** | FINGERPRINT |

**The constants each claim quotes:**

| claim | constants |
|---|---|
| clock law | `d = 3`, `rate(8) = 2` |
| redshift law | `z_AT = 2.123049E-006`, `z_GR = 2.123054E-006` |
| flux quantisation | `quantum = 0.065449846950`, `holonomy(1) = 6.283185307180` |
| source law | fixed point `9.7979589711`, phase-null `4` |
| amplitude/phase split | `1 + 42 + 53` against `1 + 46 + 49` |
| kernel theorem | kernel `53` against `49` |
| observability theorem | kernel `53` against `49` |
| phase accessibility | accessible `53` against `49` |

## The CORE block - the physical content the fingerprint cannot reach

**The flux quantisation is the sharpest case.** Its quantum is **2π/96 = 0.065449846950** and its unit holonomy is
**2π = 6.283185307180**: both are functions of the ring's **length**, not of its shell set. **The same 96 cells would
quantise the flux identically under any admissible generator**, which is why the claim reports **UNCHANGED with no
constant moving** - the fingerprint is not merely unneeded here, it is **unreachable**.

The clock law (`ρ^(1/d)`, with `d = 3` the spatial dimension) and the redshift law (`z_AT < z_GR` at every x) are
substrate-free for the same reason: neither takes a generator argument.

## The FINGERPRINT block - true claims with substrate-fixed realisations

- **STRUCTURALLY_CHANGED:** the **source law** (the ranking and the phase-null membership move, as QM_009 measured)
  and the **amplitude/phase split** (`1 + 42 + 53` becomes `1 + 46 + 49`) - their **objects** change.
- **NUMERICALLY_CHANGED:** the **kernel theorem**, the **observability theorem** and the **phase accessibility** -
  their **predicates** hold on both substrata and only the **kernel size** they quantify over moves.

## The refuted prediction - and it is the audit's own

**My draft put the observability theorem in the CORE block**, on the reasoning that its content - *every invisible
direction is still seen by the clock* - quotes no substrate number. **The measurement refused it.**

| substrate | kernel | minimum clock response | silent directions | first-order directions |
|---|---|---|---|---|
| **{1..6}** | 53 | **6.531E-003** | **0** | **53** |
| **{1}** | 49 | **6.557E-003** | **0** | **49** |

**The predicate is substrate-free** (zero silent directions on both, every response first order) **and the kernel it
quantifies over is not**: the theorem's *subject* has a fingerprint-dependent size, so the claim as stated is
**NUMERICALLY_CHANGED** with load **FINGERPRINT**. **Stripping the number leaves the predicate intact - which is why
the claim is near-CORE - but the audit reports the measured label rather than the intended one.**

## 5. The ARTEFACT label, and the honest negative

**No named claim is an artefact, and the audit says so rather than filling the category.** The label is **defined
mechanically** - a predicate that is **vacuous over the whole 63-subset family** while the number it quotes **moves** -
and the eight claims are not shaped that way. Three **controls** are, and they are carried so the classifier is seen to
produce all three labels:

| control | vacuous over 63 | {1..6} | {1} | distinct values | load |
|---|---|---|---|---|---|
| the free room is 51 | **yes** | 51 | 47 | 14 | **ARTEFACT** |
| the trace is 1152 | **yes** | 1152 | 192 | 6 | **ARTEFACT** |
| the level count is 45 | **yes** | 45 | 49 | 14 | **ARTEFACT** |

Each is true for **every one of the 63 subsets by definition**, and each quotes a number that is a pure function of the
substrate choice: **a number dressed as content.** The label is not free - a predicate that **can** fail is not an
artefact whatever its number does - and that is asserted too.

## 6. What this is worth

**If the fingerprint carried physical content, replacing it would be a change of theory. Since it carries only the
realisation, replacing it is a change of numbers** - and the audit states exactly which numbers move and which
theorems do not.

**The one qualification the audit makes rather than buries:** *CORE* here means **substrate-free**, not
fingerprint-dependent. The CORE claims are the ones the fingerprint **cannot reach**, so the sharper form of the
result is that **the theory's physical content is fingerprint-free and its bookkeeping is fingerprint-bearing** -
which is the inversion a reader of QM_007 would not have predicted.

**A performance note.** QM_008's `SeenDirections` was rebuilt inside `ModeShare` **once per Fourier mode** (96 Gram-
Schmidt rebuilds per candidate) and is now memoised per substrate, together with the canonical state, the kernel basis
and the observability reading. The QM block's runtime fell from **~30 s to 10 s** (22 tests) with no result changing,
which is why the memoisation is reported here rather than left silent.
