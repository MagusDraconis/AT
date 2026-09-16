# Y_QM_001 - Result

**Audit:** ResearchY-QM_001 - ManyBody Correspondence Audit (new group QM)
**Verdict:** **PARTIAL**
**Tests:** 8/8 PASSED

## Answer

**Two structures analogous, three refuted - and the goal is answered: rho behaves more like |Psi|^2.** rho is a
non-negative real vector of 96 cells with total 96, reconstructed as **|Psi|^2 with error 2.22E-016**, of operator rank
**1** - the rank of |Psi><Psi|. The one density-matrix-like feature it has is the **rank/kernel pair (43, 53)**, which
belongs to its **observable algebra**, not its state.

## The AT side

| sector | dimension |
|---|---|
| mean | **1** |
| amplitude | **42** |
| phase | **53** |
| **total** | **96** |

Observable (contraction) rank **43 = 1 + 42**; kernel **53 = 96 - 43 = the phase sector**; state rank **1**;
canonical phase content **9.246E-015**.

## Dimension: the modulus against the operator

| object | state dimension | matrix parameters | ratio |
|---|---|---|---|
| **AT rho (96 cells)** | **96** | **96** | 1 |
| **one particle on 96 modes** | **96** | **9 216** | **96** |
| two bosons on 12 modes | 78 | 6 084 | 78 |
| three bosons on 6 modes | 56 | 3 136 | 56 |
| three fermions on 12 modes | 220 | 48 400 | 220 |

**The dimension-matched many-body object needs 96x the parameters AT supplies.**

## The five structures

| structure | verdict |
|---|---|
| wavefunction amplitudes | **ANALOGOUS** |
| wavefunction phases | **ANALOGOUS** |
| density matrix | **REFUTED** |
| reduced density matrix | **REFUTED** |
| occupation numbers | **REFUTED** |

| finding | value |
|---|---|
| rho's occupancy total | **96** = the cell count (a density normalisation, not a trace of 1) |
| the cell census | **96 non-negative, 96 strictly positive, 0 zero** |
| the mode-weight census | **2 zeros of 44** - empty MODES with no empty CELLS |
| the state's operator rank | **1** |
| the rank AT's kernel implies | **43** = 43x the state's own rank |
| the 1-RDM trace (bosons, 3 particles) | **3.000000000000** = the particle number |
| the fermionic occupations | in **[0, 1]** (the Pauli bound), trace = the particle number |
| AT's spectral levels | **45 distinct**, multiplicities summing to **96**, maximum **above 1** |
| the reduced density matrix | product pair rank **1** / entropy **0**; straddling pair rank **2** / entropy **log 2 = 0.693147** |

## Notes

**The amplitude/phase correspondence is structural rather than numerical.** A wavefunction carries two real degrees of
freedom *per configuration* (D magnitudes and D polar angles), while AT's split divides one real 96-dimensional space
into orthogonal *subspaces* (42 + 53 + 1). `1 + 42 + 53 = 96` is a **dimension** identity; `D amplitudes + D phases` is
a **component** identity.

**A probe defect is recorded.** The first version built the correlated pair from the Fock enumeration's first two
configurations, which left the kept block empty in every system - so the reduced density matrix was rank 1 with zero
entropy for every system and the entanglement question was never being asked. The entangled case is now a separate,
named pair and **both** outcomes are asserted. Same class as G_063: a helper whose domain silently excluded the case
the audit existed to test.

**No group-G registry change.** The new suite is not a group-G suite, and the group-G consistency counts and the
`TemporalIndependenceAudit` registry are unchanged - verified by running the G_027 (literal-verdict), G_033 (substrate)
and G_035 (registry) scanners after the new core was added, all 16 tests passing.
