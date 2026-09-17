# ResearchY-QM_008 - Spectral Necessity Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_008 (permanent)
**Title:** Which AT conclusions actually require the native {1..6} spectrum?
**Status:** COMPLETE
**Date:** 2026-09-17
**File:** `QM_ManyBody/ResearchY-QM_008.md`
**Depends on:** QM_001 (the split and the modulus relation), QM_002/QM_006 (unitarity and the packet), QM_003/QM_005 (the fold), QM_004 (the coefficient), QM_007 (the fingerprint is an input), G_016/G_039 (the ring and its levels), G_062 (the canonical state recipe)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_008_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/SpectralNecessityAudit.cs`

## The question

**Which AT conclusions actually require the native {1..6} spectrum?** Recompute each using **{1}**, **{1,2}** and
**{1..6}**, classify every QM/G result **UNCHANGED / BOUNDARY / REFUTED**, and determine whether the spectral
fingerprint is **physically indispensable** or only **historically inherited**.

## The answer

> **BOUNDARY - THE FINGERPRINT IS INDISPENSABLE FOR THE NUMBERS AND DISPENSABLE FOR THE STRUCTURE, AND THE PARTITION
> IS COMPUTED RATHER THAN ASSIGNED.** Of the **12** conclusions recomputed on all three generators, **5** are
> **UNCHANGED** (the same statement, the same value), **5** are **BOUNDARY** (the same statement, a new value) and
> **2** are **REFUTED** (they do not survive the replacement at all). **So 5 of 12 do not mention the spectrum
> anywhere, and 7 require it - 5 for their value and 2 for their existence.**

## 0. The recomputation is real, and that is the whole difficulty

The canonical state is **not** a fixed object: `RhoAccessibilityAudit.BuildBaseState` walks the **spectrum's levels**
and builds one generic perturbation per level. **Replacing the generator therefore replaces the STATE.** A comparison
that re-read the native values at a different mask would have measured nothing, so this audit **rebuilds** the levels,
the level basis and the canonical state on each candidate spectrum and **rebuilds the observable split from that
state**.

Two fidelity checks keep the rebuild honest:

| check | measurement |
|---|---|
| the rebuilt native state against the recorded state | **0.000E+000** (bitwise) |
| the rebuilt native split against four quantities recorded by other audits (observable rank 43, amplitude 42, phase 53, kernel 53, contraction rank 42) | **all five agree** |

And the state genuinely moves, by far more than a perturbation:

| pair | max shift | L2 shift | correlation |
|---|---|---|---|
| {1} vs {1,2} | **0.370396** | 1.419953 | **0.221376** |
| {1} vs {1..6} | **0.346756** | 1.274251 | **0.236824** |
| {1,2} vs {1..6} | **0.420200** | 1.419054 | **0.189997** |

**The three canonical states are nearly ORTHOGONAL** (correlations 0.19 to 0.24), which is why nothing derived from
the state can be carried across a generator replacement unexamined.

## 1. The structural block - UNCHANGED

| conclusion | source | **{1}** | **{1,2}** | **{1..6}** | classification |
|---|---|---|---|---|---|
| ρ = \|Ψ\|² exactly | QM_001 | 2.2E-016 | 2.2E-016 | 2.2E-016 | **UNCHANGED** |
| the state totals the cell count | QM_001 | 96.0000 | 96.0000 | 96.0000 | **UNCHANGED** |
| the flow conserves the norm exactly | QM_002 / QM_006 | 1.3E-015 | 8.9E-016 | 8.9E-016 | **UNCHANGED** |
| the mode occupation is conserved | QM_002 | 8.9E-016 | 1.6E-015 | 1.3E-015 | **UNCHANGED** |
| the long-wavelength power law is 2 | QM_004 / QM_005 | 2.0000 | 2.0000 | 2.0000 | **UNCHANGED** |

**These are properties of the RING, not of the shell set:** 96 cells, a reflection pairing, a circulant structure and a
**real** symbol - and every candidate has all four. The modulus relation is pointwise algebra; the density
normalisation is the cell count; norm conservation and occupation conservation follow from a **real symbol**, hence a
Hermitian generator, hence a unitary flow.

**The one row whose tolerance is not the comparison floor is the power law**, and the audit says so rather than
rounding it away: the three fits give **1.999999996 / 1.999999988 / 1.999999910**, differing in the **eighth digit**
because the quartic term contaminates each fit differently. That is the **fit's own noise**, not a different exponent,
so the row is compared at the fit's precision (**1E-6**) - and the raw values are printed beside the table.

## 2. The numerical block - BOUNDARY

| conclusion | source | **{1}** | **{1,2}** | **{1..6}** | classification |
|---|---|---|---|---|---|
| the Schrödinger coefficient is the second moment | QM_004 | **1** | **5** | **91** | **BOUNDARY** |
| the quartic coefficient is Σr⁴/12 | QM_005 | **0.08** | **1.42** | **189.58** | **BOUNDARY** |
| the packet tracks Schrödinger for a window | QM_006 / QM_007 | **514** | **153** | **23** | **BOUNDARY** |
| the split is 1 + amplitude + phase | QM_001 / QM_007 | **1 + 46 + 49** | **1 + 44 + 51** | **1 + 42 + 53** | **BOUNDARY** |
| the observable rank is 43 | QM_001 | **47** | **45** | **43** | **BOUNDARY** |

**A BOUNDARY row is the same statement with a different number** - which is what a dimensional constant looks like when
its substrate moves. The effective mass, the quartic correction and the Schrödinger window are all still defined, all
still finite and all changed, so the theory keeps its **form** and loses its **values**.

### 2.1 The split MOVES, and QM_007 asserted otherwise

| candidate | observable rank | mean | amplitude | phase | hidden modes | visible modes | split modes |
|---|---|---|---|---|---|---|---|
| **{1}** | 47 | 1 | **46** | **49** | 49 | 46 | **0** |
| **{1,2}** | 45 | 1 | **44** | **51** | 51 | 44 | **0** |
| **{1..6}** | 43 | 1 | **42** | **53** | 53 | 42 | **0** |

**QM_007 claimed the 42/53 split is `|S|`-independent, and the recomputation REFUTES that.** The claim was made by
calling a mask-free helper - which of course returns the native answer - rather than by rebuilding the split on another
generator, and the split is a property of the **state's** kernel, not of the ring alone.

**What survives of the claim is its structural half:** every candidate's kernel is still a **union of Fourier modes**
(zero split modes in all three), and the partition is exact in all three (1 + amplitude + phase = 96). So the
*classification mechanism* is `|S|`-independent while the *membership* is not.

## 3. The block that does not survive - REFUTED

| conclusion | source | **{1}** | **{1,2}** | **{1..6}** | classification |
|---|---|---|---|---|---|
| the dispersion folds inside the band | QM_003 / QM_005 | **none** | **ch 28** | **ch 11** | **REFUTED** |
| the recorded fingerprint (trace 1152, 45 levels, free room 51, max 15.837372) | QM_007 | 192 / 49 / 47 / 4.000 | 384 / 47 / 49 / 6.250 | 1152 / 45 / 51 / 15.837 | **REFUTED** |

**REFUTED here means "does not survive the replacement", NOT "false".** Each of these is true on the substrate it was
measured on; the point is that it is a statement about **that** substrate.

- **The fold does not exist on {1}.** QM_003 and QM_005 established the fold as the price of locality *on the native
  set*; the replacement shows that the fold is not a property of AT's generator at all, only of the six-shell one.
- **The recorded fingerprint is the input.** QM_007 showed the fingerprint *identifies* the native set; this audit
  shows the same fingerprint **is not re-derivable** from any other generator - which is the same statement from the
  other side.

## 4. The verdict

```
12 conclusions = 5 UNCHANGED + 5 BOUNDARY + 2 REFUTED
  do not mention the spectrum at all : 5 of 12
  require it (for the value)         : 5 of 12
  require it (for the existence)     : 2 of 12
```

**The fingerprint is neither physically indispensable nor merely historically inherited: it is OPERATIVE.**
It is **indispensable wherever AT quotes a number** and **dispensable wherever AT states a relation**.

## 5. Defects in the audit's own first version

1. **The mode-leakage helper evolved the PACKET, not the mode.** `GeneratorSelectionAudit.EvolveUnder` ignores its
   argument and always evolves the packet, so feeding it a single Fourier mode measured the packet's overlap with
   channel 5 (**1.9E+000**) and would have reported occupation conservation **REFUTED** on all three candidates. The
   audit now evolves the mode explicitly and measures **8.9E-016 to 1.6E-015**.
2. **The power law was compared at a tolerance tighter than the method's own precision.** At the 1E-9 comparison floor
   the three exponents (**1.999999996 / 1.999999988 / 1.999999910**) differ, so the row classified **BOUNDARY** on
   noise. Rows now carry their own tolerance and this one uses the fit's **1E-6**, with the raw values printed.
3. **A wrong assumption of my own is recorded rather than quietly fixed.** I expected the state replacement to be a
   small perturbation and asserted a correlation **above 0.9**; the measurement gives **0.19 to 0.24** - the states are
   nearly orthogonal. The assertion now records the refutation.
4. **A literal reached a verdict, which the repository's rule 5 forbids.** The quartic coefficient of {1} was asserted
   as QM_005's **printed** `0.08` at six decimals; the true value is **1/12 = 0.083333**, and the test failed. It is
   now asserted against the **closed form** recomputed in the test.
5. **An off-by-one in the rebuilt split.** The first version wrote observable rank = seen − 1 and amplitude =
   seen − 2, giving the native **42 / 41 / 54** against the recorded **43 / 42 / 53**. The correct reading is
   observable = seen, amplitude = seen − 1, phase = 96 − seen; the rebuild now agrees with all five recorded
   quantities.
6. **My probe-removal script deleted five of the audit's own tests.** The script cut everything between the temporary
   probe's `[Fact]` and the diagnostic's `[Fact]`, and the five new tests had been inserted **between** them, so the
   suite silently dropped from **8 to 3**. It was caught by the **ResearchY total: 3296 against the expected
   3293 + 8 = 3301** - not by the suite itself, which was passing. The tests are restored and the suite is **8/8**;
   the lesson is that a test count is a measurement, so it is checked against an arithmetic expectation rather than
   read as a pass.

## Where it stands

**The recomputation found what QM_007 had assumed rather than measured: replacing the spectrum replaces the state.**
The state is built from the spectrum's levels, its cross-candidate correlations are **0.19 to 0.24**, and every
state-derived quantity has to be rebuilt rather than re-read - which is why the amplitude/phase split moves and why
QM_007's `|S|`-independence claim is refuted here.

**And the question's two options were both wrong.** The spectral fingerprint is not physically indispensable: five
structural conclusions hold as the same statement with the same value on a generator that never folds and has an
effective mass 91 times smaller. It is not merely historically inherited either: seven conclusions require it, two of
them for their **existence**, and the fingerprint is exactly the **input** QM_007 identified. What the fingerprint
actually is, is **operative** - it fixes every number AT quotes and none of the relations AT states.
