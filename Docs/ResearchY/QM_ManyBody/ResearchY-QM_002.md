# ResearchY-QM_002 - Unitary Correspondence Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_002 (permanent)
**Title:** Can any AT flow reproduce unitary Schrodinger evolution on the occupied mode sector?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_002.md`
**Depends on:** QM_001 (rho is a modulus), G_066 (the five flow forms), G_067 (the flow selection), G_052/G_061 (the amplitude/phase split and the kernel)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_002_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/UnitaryCorrespondenceAudit.cs`

## The question

Can any AT flow reproduce **unitary Schrodinger evolution** on the **occupied mode sector**? Compare the **unitary
Cayley flow**, the **centred difference flow** and the **dissipative flow**. Measure **norm conservation**, **phase
evolution** and **mode occupation**. Output **ANALOGOUS / PARTIAL / REFUTED**.

## The answer

> **PARTIAL - and the reason is a TRADE-OFF rather than a defect. Schrodinger evolution needs two things: a norm that
> is conserved (|m| = 1) and a phase that advances LINEARLY in the mode momentum. Each AT flow has exactly ONE of them,
> and NO AT FLOW HAS BOTH.** Measured, not argued: **1** of the four flows carried is unitary, **1** has the clean
> dispersion, and they are **different flows**.

## 1. Norm conservation: ANALOGOUS, exactly

| flow | worst &#124;&#124;m&#124; − 1&#124; | at ε | channel |
|---|---|---|---|
| **unitary (Cayley of the skew part)** | **2.220E-016** | 1E-002 | 15 |
| centred (skew) difference | 4.988E-003 | 1E-001 | 24 |
| forward difference (dissipative) | 2.000E-001 | 1E-001 | 48 |
| exact flow exp(εD) | 1.813E-001 | 1E-001 | 48 |

The Cayley multiplier is `(1 + iεs)/(1 − iεs)`, so `|m| = 1` **identically** - verified on all 48 channels at every ε
in the ladder, as a test that fails if the unitary property is ever lost. The centred form **amplifies**; the forward
difference and the exact flow **dissipate**.

## 2. Mode occupation: ANALOGOUS - and equivalent to the first measure

Every AT update is **circulant**, hence diagonal in the Fourier basis, so each `|c_k|` changes only through `|m|`. The
audit measures the equivalence rather than listing a third independent measure:

- Cayley: drift **< 1E-12** per step (constant);
- the others: **4.98E-007** (centred), **4.91E-004** (forward), **4.91E-004** (exact).

**Conserving the mode occupations IS having unit modulus** here, and the audit verifies that flow by flow.

But the mode count alone would miss the important part: **the amplitude/phase split is NOT a constant of the motion,
even under the unitary flow** (2000 steps, ε = 1E-3):

| flow | phase before | phase after | amplitude before | amplitude after |
|---|---|---|---|---|
| **unitary (Cayley)** | **9.246E-015** | **0.6392** | 1.005011 | 0.775567 |
| centred | 9.246E-015 | 0.8510 | 1.005011 | 0.535627 |
| forward difference | 9.246E-015 | 0.3414 | 1.005011 | 0.351792 |
| exact flow | 9.246E-015 | 0.3414 | 1.005011 | 0.351771 |

The flows rotate amplitude content into the phase sector from a state whose phase content is zero to the floor. **A
real Hamiltonian could not do that** - so the generator is Hermitian and **not real** in the amplitude/phase basis.
Under the unitary flow the conserved quantity is the total deviation norm, not the split.

## 3. Phase evolution: REFUTED - and it breaks in three independent places

A Hermitian generator **exists**: the audit computes `H_eff = −arg m / ε` mode by mode and finds a real diagonal
spectrum, so the AT unitary flow **is** the exponential of a Hermitian operator. But that operator is not the AT
operator, and the gap has three sources:

| source | magnitude | ε-order | removable by smaller steps |
|---|---|---|---|
| **the Cayley generator convention** | **1.999999** (→ 2) | none | **NO** |
| **the time discretisation** | **3.333331E-007** at ε = 1E-3 | **2** (coefficient 1/3) | yes |
| **the lattice symbol sin δ against δ** | **0.363380** at channel 24 | none | **NO** |

**The factor two is a measurement, not an error.** `(1 + iεs)/(1 − iεs) = exp(2i arctan(εs))`, so the repository's
Cayley update unitarises **twice** the skew generator the other flows advance: its phase advance is `2ε sin δ`
against their `ε sin δ`. It is a **convention in the flow's definition with a measurable consequence**, and it is
reported as such.

**The step ladder separates the removable from the irreducible:**

| ε | discretisation gap | lattice gap (channel 24) | zone edge stationary |
|---|---|---|---|
| 1E-003 | 3.333331E-007 | **0.363380** | True |
| 1E-002 | 3.333133E-005 | **0.363380** | True |
| 1E-001 | 3.313475E-003 | **0.363380** | True |

The discretisation gap falls by **100 per decade** - order **ε²**, with the arctan coefficient **1/3** - while the
lattice gap sits at **1 − 2/π = 0.363380** and **does not move at all**.

## 4. The fold: sin δ is bounded, and half the occupied sector is behind it

**The lattice symbol is `sin δ`, not `δ`.** It peaks at the **folding channel 24** (δ = π/2) and returns to
**2E-019** at the **zone edge** (channel 48):

- **the highest mode does not advance at all** under the unitary flow, where Schrodinger's ω ∝ k would give it the
  *largest* frequency;
- of the 47 adjacent channel pairs, **23 advance in the continuum's order and 24 in REVERSE**;
- **on the occupied sector the audit counts rather than generalises: of the 42 occupied modes, `21` lie at or above the
  folding channel** (25–31, 33–39, 41–47).

**That is the answer to the question as asked:** the AT unitary flow cannot reproduce Schrodinger evolution on the
**occupied** mode sector, because half of that sector lies where the AT dispersion runs backwards against the
continuum - and no choice of step size changes it.

## 5. The trade-off, which is the audit's answer

| flow | &#124;m&#124; = 1 (unitary) | arg m = ε sin δ (clean dispersion) |
|---|---|---|
| **unitary (Cayley of the skew part)** | **True** | **False** |
| centred (skew) difference | False | False |
| forward difference | False | False |
| **exact flow exp(εD)** | **False** | **True** |

**The unitary flow buys unitarity at the price of a distorted dispersion; the exact flow has the dispersion exactly
right and dissipates.** Schrodinger needs both.

## Verdict

**PARTIAL.** Of the three measures the question names:

| measure | verdict | basis |
|---|---|---|
| **norm conservation** | **ANALOGOUS** | the Cayley modulus is 1 on all 48 channels to 2.220E-016 |
| **mode occupation** | **ANALOGOUS** | circulant, hence diagonal: constant under the unitary flow; equivalent to the first measure |
| **phase evolution** | **REFUTED** | the symbol is sin δ, not δ; the zone edge is stationary; 21 of 42 occupied modes are past the fold; the Hermitian generator is 2 arctan(ε sin δ)/ε, not the symbol |

**2 analogous, 0 partial, 1 refuted - and the refuted measure is the one that defines a Schrodinger evolution.** The
correspondence is **constructive but confined**: the AT unitary flow can be *written* as `exp(−iHt)` with
`H = (2/ε) arctan(ε sin δ)`, and the term that cannot be removed by taking smaller steps is **the lattice**.
