# Y_G_041 — Substrate Dimension Audit — Result

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_041_Tests.cs` — **8/8 PASSED**
**Core:** `AT.Core/ResearchXH/SubstrateDimensionAudit.cs`
**Doc:** `Docs/ResearchY/G_GravitySource/ResearchY-G_041.md`

## Question

AT needs the ring (d = 1) and the cube (d = 3). What do D96² and D96⁴ do — and is d = 3 **selected** or
**assumed**?

The family is D96^d with symmetry the signed-permutation group B_d (order 2^d·d!; B₃ is the cubic group O_h of
order 48). d = 1 is the **periodic** ring, so its group is dihedral of order 192.

## Answer: BOUNDARY — one half strengthens the programme, the other half weakens it

## The ladder (computed; Burnside Σ d_i² = |G| verified for every d)

| d | group | order | #irreps | max irrep dim | 3-dim sector? |
|---|---|---|---|---|---|
| 1 | D96 (ring) | 192 | 51 | **2** | no |
| 2 | B₂ | 8 | 5 | **2** | **no** |
| 3 | B₃ = O_h | 48 | 10 | **3** | yes |
| 4 | B₄ | 384 | 20 | **8** | yes |
| 5 | B₅ | 3840 | 36 | **20** | yes |
| 6 | B₆ | 46080 | 65 | **80** | yes |

**D96² fails exactly as the ring fails** (max irrep dim 2), so the cube is the **minimal** working dimension —
G_033 **strengthened**.

## The negative result

⟨V,V⟩ = 1, ⟨W,W⟩ = 2, ⟨V,W⟩ = 0, ⟨V,A⟩ = 0 **at every d ≥ 2**. The vector is irreducible everywhere; the traceless
sector always splits into exactly two pieces (E_003's E + T2 at d = 3 was never a d = 3 coincidence); the sectors
never mix. **The signature is dimension-blind** → the claim that the irrep argument *selects* d = 3 is **REFUTED as
a selector**: it selects **d ≥ 3**. Only "the vector is the largest irrep" breaks, and only from d = 4 (max irrep
dim jumps to 8, then 20).

## Two independent accidents do pin d = 3

* **ε/Hodge:** dim Λ² = dim V **only at d = 3** (3 = 3); d = 2 gives 1 (a *scalar*), d = 4 gives 6, d = 5 gives 10.
* **Polarisation match:** photon d−1 vs graviton (d+1)(d−2)/2 equal **only at d = 3** (2 and 2); d = 2 gives
  **1 and 0** — no propagating graviton at all; d = 4 gives 3 and 5.

## The choice is observable

ρ^(1/d): the same 20:1 contrast gives **129 415.634 / 86 277.089 / 64 707.817** s/day at d = 2 / 3 / 4, with
**9 216 / 884 736 / 84 934 656** modes. The published 86 277.089 s/day is a **d = 3 number**.

## Registry

`ClockOnly` → **SURVIVES**, `ScanDetectsIt: false`. Counts become **28 / 11 / 3 of 42**; boundary index unchanged;
no prior classification changed. **One hypothesis weakened and recorded**: G_033 is strengthened in its positive
claim (the cube is minimal) but the irrep-supply argument does not *select* d = 3 — d = 4 is not excluded by it.
G_033's live classifier now reads **23** substrate suites (40 classified in total).

## Caveats

* d = 4 is ruled out by the two accidents, the polarisation mismatch, the clock exponent and cost (96×) — **not**
  by the representation theory.
* The d = 1 row must use the dihedral group (periodic ring), not B₁ — a category error otherwise.
* The irrep spectra here are **exact** (representations of a finite group), so this audit is immune to the A₀
  robustness problem G_033 documented for spectral level counting.
