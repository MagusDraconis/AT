# Y_G_061 - Result

**Audit:** ResearchY-G_061 - Residual Phase Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

The 11 unreachable phase directions are **not special directions of the substrate**: they are the modes the canonical
state does **not occupy**, and the split is a property of **shift-invariance**, not of the directions. The kernel is the
state's **orthogonal complement** - **hidden ⟺ zero occupancy**, measured over all 95 non-constant modes.

## Measurements

| measurement | value |
|---|---|
| the eleven | channels **14, 19, 24, 32, 40** (both quadratures) + the **alternating mode** |
| occupied modes / occupied span / kernel | **42 / 43 / 53** (43 + 53 = 96) |
| hidden ⟺ zero occupancy | **True** |
| levels / Σ(m−1) (free room) | **45 / 51** |
| degenerate levels | index **13** (λ = **12.0**, mult **5**, channels 16, 32, 48); index **35** (λ = **14.0**, mult **6**, channels 8, 24, 40) |
| vanishing construction weights | levels **8, 31** → channels **14, 19** |
| from degeneracy / from vanishing weights | **7 / 4** = **11**, disjoint |
| circulant operators (difference, centred difference) | projection ≤ **9.512E-015**, rank **0** |
| state-dependent operators (connection, T1/T2) | projection **2.968E-003 / 1.455E-001**, rank **11** |
| alternative state (`basis[^1]`) | empty channels **8, 14, 16, 19, 24, 32** → **12** directions |
| symmetry group order / invariant set | **192 / True** |
| orbit sizes | alternating mode **2**; channel 32 cosine **3**; others up to 192 |

## Notes

**The set moves.** Rebuilding from the last basis vector of each level changes the unreachable set from **11** to **12**
directions, so the eleven are **not substrate-invariant**. What is invariant is the count degeneracy forces: **Σ(m−1) =
51** modes are empty for **any** state that takes one vector per level. The other two come from a construction weight
that **vanishes** - a property of a chosen formula, not of the substrate.

**Both halves of the reachability answer are measured:** no **circulant** AT operator reaches the eleven (they preserve
the channel decomposition), while AT's **state-dependent** operators reach **all eleven at full rank**, because
multiplication by a non-constant `h(ρ)` mixes channels.
