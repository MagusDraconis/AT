# ResearchY-QM_001 - ManyBody Correspondence Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence (new group; first audit)
**ID:** ResearchY-QM_001 (permanent)
**Title:** Does the AT decomposition rho = mean + amplitude + phase have an analogue in many-body quantum states?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_001.md`
**Depends on:** G_052 (the decomposition itself), G_050/G_061 (the phase sector as the kernel), G_062 (the mode count), HilbertOrigin and QuantumMechanicsCompatibility (the legacy two-degree-of-freedom result)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_001_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/ManyBodyCorrespondenceAudit.cs`

## The question

Does the AT decomposition **rho = mean + amplitude + phase** have an analogue in many-body quantum states? Compare
**wavefunction amplitudes**, **wavefunction phases**, the **density matrix**, the **reduced density matrix** and
**occupation numbers**, measuring **dimension**, **kernel** and **observable rank**. Output
**ANALOGOUS / PARTIAL / REFUTED**. Goal: determine whether rho behaves more like **|Psi|^2** or a **many-body density state**.

## The answer

> **PARTIAL - two structures analogous, three refuted.** And the goal has a definite answer: **rho behaves more like
> |Psi|^2.** rho is a non-negative real vector of 96 cells with total 96, reconstructed as **|Psi|^2 with error
> 2.22E-016**, and its rank as an operator is **1** - the rank of |Psi><Psi|. The one density-matrix-like feature it has
> is the **rank/kernel pair (43, 53)**, and that belongs to its **observable algebra**, not to its state.

## 1. The AT side: 1 + 42 + 53 = 96

| sector | dimension | role |
|---|---|---|
| mean | **1** | the constant mode |
| amplitude | **42** | G_052's amplitude sector = the visible Fourier modes |
| phase | **53** | G_052's phase sector = the kernel of the contractions |

The decomposition is a **complete orthogonal partition** of one real 96-dimensional space, and the observable
(contraction) rank is **43 = 1 + 42** with the phase sector as its kernel. The canonical state's phase content is
**9.246E-015** - phase-free - so the canonical rho is a **real modulus**.

## 2. The deciding measurement: rho is a modulus, not an operator

```
rho = |Psi|^2   with   Psi = sqrt(rho)   on the same 96 sites,   reconstruction error 2.22E-016
```

measured, constructively. Two further measurements fix the interpretation:

- **the state's rank is 1** - the rank of |Psi><Psi|;
- **the total is 96 = the cell count**, a *density* normalisation rather than a trace of 1 (mean occupancy 1.000).

And a contrast the census alone would hide: **AT has no empty cells** (96 strictly positive, 0 zero) **but it does have
empty modes** - the construction's 44 mode weights contain exactly **2 zeros** (levels 8 and 31, i.e. channels 14 and
19). That is the *modulus* signature (a wavefunction is non-zero everywhere while its Fourier content is limited), not
the *natural-orbital* signature (an unoccupied orbital).

## 3. Dimension: the modulus against the operator

| object | state dimension | matrix parameters | ratio |
|---|---|---|---|
| **AT rho (96 cells)** | **96** | **96** | 1 |
| **one particle on 96 modes** | **96** | **9 216** | **96** |
| two bosons on 12 modes | 78 | 6 084 | 78 |
| three bosons on 6 modes | 56 | 3 136 | 56 |
| three fermions on 12 modes | 220 | 48 400 | 220 |

**One particle on 96 modes has Fock dimension exactly 96 - AT's state dimension - while its density matrix needs
9 216 real parameters. AT supplies 96, a factor of 96 fewer, and that factor IS the difference between a modulus and an
operator.** (For every system the ratio is the dimension itself, which is the structural statement rather than a
numerical coincidence.)

## 4. The three ranks, which decide the goal

| rank | value | what it is | quantum counterpart |
|---|---|---|---|
| **state rank** | **1** | rho is one vector, so the operator built from it is rank 1 | a **pure** state's density matrix is rank 1 |
| **observable rank** | **43** | the contractions span 1 + 42 | an observable algebra's rank, which a density matrix leaves implicit |
| **kernel** | **53** | 96 - 43, and it **is** the phase sector | a rank-43 operator on 96 dims has kernel 53 |

**Reading AT's kernel as a density-matrix kernel demands rank 43 - 43x the rank of the state itself.** A density matrix
**conflates** these two ranks; AT **separates** them. That is why the kernel relation *hidden <-> zero occupancy* (G_061)
is the analogue of the **natural-orbital** relation (an unoccupied natural orbital is orthogonal to the state) rather
than the analogue of a rank deficiency.

## 5. The five structures, side by side

| structure | AT counterpart | dimension | kernel | observable rank | verdict |
|---|---|---|---|---|---|
| wavefunction amplitudes | 42-dim amplitude sector | D complex = 2D real; AT 96 real | n/a | rank 1 (one vector) | **ANALOGOUS** |
| wavefunction phases | 53-dim phase sector | D polar angles; AT 53 = 96 - 42 - 1 | polar split is per component, AT's per subspace | AT observable rank 43 | **ANALOGOUS** |
| density matrix | rho: 96 real, total 96 | D^2 = 9 216; AT 96 | D - rank; AT 53 | state rank 1 vs observable rank 43 | **REFUTED** |
| reduced density matrix | none on D96; D96^3 only | dim_A^2 = 400 for the 6-mode system | dim_A - rank; AT n/a | entropy log 2, rank 2 | **REFUTED** |
| occupation numbers | 45 spectral levels, multiplicities summing to 96 | 1-RDM eigenvalues | rank 2 of 6 modes | trace = the particle number | **REFUTED** |

**The amplitude/phase correspondence is structural, not numerical.** A complex wavefunction carries two real degrees of
freedom *per configuration* (D magnitudes and D polar angles of the same vectors), while AT's split is one real
96-dimensional space divided into orthogonal **subspaces** (42 + 53 + 1). Both are "two halves", and the counts
demonstrate which kind: `1 + 42 + 53 = 96` is a **dimension** identity; `D amplitudes + D phases` is a **component**
identity.

## 6. The quantum side, computed exactly

Fock space, the one-body reduced density matrix, its occupations, the partial trace and the Pauli bound are all
computed from a deterministic two-configuration superposition - no randomness, and no imported library.

| system | Fock dim | 1-RDM occupations | trace | 1-RDM rank |
|---|---|---|---|---|
| 2 bosons on 3 modes | 6 | 1.866025, 0.133975, 0 | **2.000000000000** | 2 |
| 2 bosons on 4 modes | 10 | 1.866025, 0.133975, 0, 0 | **2.000000000000** | 2 |
| 3 bosons on 6 modes | 56 | 2.822876, 0.177124, 0, ... | **3.000000000000** | 2 |
| 4 bosons on 6 modes | 126 | 3.802776, 0.197224, 0, ... | **4.000000000000** | 2 |
| 3 fermions on 6 modes | 20 | 1, 1, 1, 0, 0, 0 | **3.000000000000** | 3 |
| 4 fermions on 8 modes | 70 | 1, 1, 1, 1, 0, ... | **4.000000000000** | 4 |

**The conservation law holds to 12 digits: the 1-RDM's trace IS the particle number**, and every fermionic occupation
lies in **[0, 1]**. AT's occupancies sum to **96 = the state dimension**, and its "occupation numbers" are the
**45 spectral levels** whose integer multiplicities sum to 96 with values **above 1** - operator **degeneracies**, not
occupations.

**The partial trace:** a superposition confined to one side of the cut is a **product** state (rDM dim 20, rank **1**,
entropy **0**), while one straddling the cut is **entangled** (rank **2**, entropy **log 2 = 0.693147**). Both measured.
**D96 offers nothing for a partial trace to act on** - the nearest AT object is the cubic substrate D96^3, whose whole is
96^3 = 884 736 and whose one-factor reduced object is 96^2 = 9 216.

## 7. A probe defect, recorded

A first version of the audit built its correlated pair from the Fock enumeration's **first two configurations**, which
in every system left the kept block **empty** - so the reduced density matrix came out rank 1 with zero entropy for
every system, i.e. the entanglement question was never being asked. The pair is now chosen explicitly and the entangled
case is a separate, named pair, with **both** outcomes asserted. This is the G_063 defect class: a helper whose
*domain* silently excluded the case the audit existed to test.

## Verdict

**PARTIAL.** **Two** of the five structures correspond to the decomposition - the **amplitudes** and the **phases** -
and **three** do not: the density matrix, the reduced density matrix and the occupation numbers all fail by
measurement, not by argument (the total is the cell count rather than 1; D96 carries no bipartition; the spectral
levels are degeneracies summing to the state dimension).

**And the goal is answered: rho behaves more like |Psi|^2.** It is a non-negative real modulus reconstructed as
Psi = sqrt(rho) to **2.22E-016**, of operator rank **1** - the rank of |Psi><Psi| - with **no empty cells while having
two empty modes**. The one density-matrix-like feature it possesses is the pair **(rank 43, kernel 53)**, and that is a
property of the **observable algebra**, not of the state.
