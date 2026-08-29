# Release Notes — The Actualization Theory, ResearchY V2.1 Boundary Program

**Version:** 2.1.0 · **Date:** 2026-08-30 · **Branch:** `feature/v2.1-boundary-program`
**Suite:** 372 xUnit tests passing · **AT.App build:** 0 errors

## Summary

ResearchY V2.1 is the **origin-program milestone** of *The Actualization Theory*. Over
26 audits (D_020–D_045) plus the closure audit (R_001), the program traced the complete
origin chain — **Difference → Actualization → Spectrum → N=96 → Physics** — and reduced
every origin question to a definitive classification (DERIVED / EMERGENT / BOUNDARY).
The **V2.1 Boundary Program Closure Audit (R_001) declares the program COMPLETE**: the
final irreducible boundary set has exactly **five items**, everything else in the chain
is derived or emergent, and no origin question remains open.

## What's in this release

- **The origin chain, fully traced.** From the primitives {Difference, η} through the
  tick/count/magnitude/phase to the complex state, reciprocity, pairing, p=3, N=96,
  closure, and the spectrum — every step classified.
- **The boundary program, closed.** R_001: 13 original boundary items reclassified
  (7 → DERIVED, 1 → EMERGENT, 6 confirmed BOUNDARY); 20 objects DERIVED, 10 EMERGENT,
  0 OPEN.
- **26 audit documents** (D_020–D_045) + **closure audit** (R_001), each with a
  matching xUnit suite.
- **The final irreducible boundary set (5 items):**
  1. **{Difference, η}** — the primitives (D_027/D_039).
  2. **{Z2-paired (complex) sector}** — the observable-sector input (D_020/D_036).
  3. **{3 octave families}** — the span ∈ [4,8) window (D_020).
  4. **{SU(2) gauge + j=1/2}** — the weak-isospin input (D_022/D_024).
  5. **{v, m_e}** — the dimensionful anchors (D_012/D_044).
- **AT.App** — Research News entries for every finding (D_021–D_045, R_001) and
  Theory Book results (Resonances, Cosmology chapters).

## Boundary Reductions (the headline result)

| Object | Original | Final | Via |
|---|---|---|---|
| complete pairing | BOUNDARY | **DERIVED** | D_035 (every eigenvalue mult ≥ 2 from complex observability) |
| singleton prohibition | BOUNDARY | **DERIVED** | D_035/D_037 |
| p=3 seed, 6\|N, N=96 | BOUNDARY | **DERIVED** | D_031 |
| su(2) compact-form | BOUNDARY | **EMERGENT** | D_026 (selected by observability) |
| state identity | EMERGENT | **DERIVED** | D_039 (Difference IS distinguishability) |
| span value / family count value | — | **DERIVED** | D_028 |
| v structure (137·ln span) | — | **DERIVED** | D_044/QG168 |
| ΩΛ / Ωm | — | **DERIVED** | QG234/D_045 |
| Z2-paired sector, 3-family window, SU(2) gauge, {Difference,η}, {v,m_e}, π | BOUNDARY | **BOUNDARY** | confirmed |

## Derived Chain (the complete origin path)

```
Difference
 → Actualization
 → tick (ordering)                 [DERIVED — QG220]
 → count ρ                         [DERIVED — QG216]
 → magnitude |ψ| = √ρ              [DERIVED — QG216]
 → phase θ = 2πk/N                 [DERIVED — QG220]
 → complex state ψ = |ψ|·e^{iθ}    [DERIVED — QG218]
 → state identity / observability  [EMERGENT/DERIVED — D_039]
 → reciprocity                     [EMERGENT — D_037]
 → pairing structure               [DERIVED — D_021]
 → complete pairing (mult ≥ 2)     [DERIVED — D_035]
 → p=3                             [DERIVED — D_031]
 → 6|N                             [DERIVED — D_031]
 → N=96                            [DERIVED — D_031/D_040]
 → Closure (fixed point)           [DERIVED — Ch4]
 → Spectrum (D96 eigenvalues)      [DERIVED]
 → span, ratios, hierarchies       [DERIVED — D_028/D_042]
 → {v, m_e} anchors                [BOUNDARY — D_044]
 → Dimensionful Physics            [EMERGENT]
```

## Remaining Boundaries (documented inputs, not gaps)

1. **{Difference, η}** — the two primitives.
2. **{Z2-paired (complex) sector}** — the observable sector is a paired complex sector.
3. **{3 octave families}** — the span ∈ [4,8) window (3 generations).
4. **{SU(2) gauge + j=1/2}** — the weak-isospin gauge structure.
5. **{v, m_e}** — the dimensionful anchors (v's structure derived, m_e pure boundary).

These are the theory's irreducible inputs: every "why" beyond them has been answered.

## Verification

- `dotnet test AT.Tests` — **372/372 passing** (350 D-group + A/B/C + 22 via the
  R-group closure; every audit has a matching suite).
- `dotnet build AT.App` — **0 errors**.
- All canonical D96 values independently recomputed and verified across the audits
  (span = 6.4025, λ(48) = 12, min mult 2 at N=96, v = 254.37 GeV, ΩΛ = 0.6839).
- No new primitives; **canonical AT V2.0 unchanged**.

## Tag Recommendation

- **Tag name:** `v2.1-boundary-program` (matches the branch).
- **Changelog section:** `## [2.1.0] — 2026-08-30` with the boundary-reduction table
  and the five-item boundary set as the headline.
- **Migration notes:** none required — V2.1 is research/audit only; no canonical V2.0
  values, equations, predictions, or claim statuses were altered. Projects consuming the
  canonical monograph are unaffected. The AT.App adds Research News + Theory Book
  entries only.

## Deliverables

- `Docs/ResearchY/R_BoundaryProgram/ResearchY-R_001.md` — the closure audit.
- `Docs/ResearchY/R_BoundaryProgram/ResearchY-R_002.md` — this release document.
- `Docs/ResearchY/D_ResonanceStructure/ResearchY-D_020…D_045.md` — the 26 audit docs.
- `AT.Tests/ResearchY/...` — 372 passing tests (incl. `Y_R_001_Tests.cs`).

**Status:** READY FOR TAGGING.
