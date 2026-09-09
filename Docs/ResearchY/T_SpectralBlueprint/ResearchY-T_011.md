# ResearchY-T_011 — Species Ceiling Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_011 (permanent)
**Title:** Species Ceiling — can the ~19-species value emerge naturally from D96?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_011.md`
**Depends on:** ResearchY-T_007–T_010; legacy `AT_138` (open-ended innovation), `AT_139`
(information landscape topology)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_011_Tests.cs` (6/6 ✅)
**Core analyzer:** `AT.Core/ResearchT/BoundedInnovationAnalyzer.cs` (extended with transient /
cumulative / discovery-mode measures)

---

## Question

The legacy Θ-field program found an attractor landscape with **~19 stable species**
(AT-138: "66 novel species, 15 unique, saturation index 0.82"; AT-139: "~19 stable species").
T_011 asks: **can this ~19-species value emerge naturally from D96?** Derive or refute
`N_species ≈ 19` from the T_001–T_010 laws.

## Method

Measure the four population quantities of the replicator–mutator (fitness `w = m/λ`, crowding
`f = w/(1+βx)`, ring mutation μ, extinction ε = 1e-6):

- **transient species count** — peak number of simultaneously-alive species during the run;
- **cumulative species count** — distinct modes ever above threshold;
- **survivor count** — S∞ at saturation;
- **turnover** — (extinctions + colonizations)/steps.

Two initialization regimes: **uniform** (the T_007 default) and **discovery** (start from the
single fittest species, matching AT-138's "few → many" semantics). Deterministic throughout.

---

## Results

### Population quantities (μ=0.01, β=1, uniform init)

| case | A | S∞ | transient | cumulative | turnover |
|---|---|---|---|---|---|
| D96 | 44 | **5** | 44 | 44 | 0.0019 |
| D96-3D | 12 | 9 | 12 | 12 | 0.0001 |
| random | 95 | **17** | 95 | 95 | 0.0039 |
| physical | 48 | 5 | 48 | 48 | 0.0022 |
| unphysical | 3 | 3 | 3 | 3 | 0.0000 |

Under uniform init the transient/cumulative counts are the trivial ceiling A (all modes start
alive); only S∞ is informative.

### Discovery trajectory (μ=0.01, β=1, start from fittest)

| case | A | cumulative | survivor | transient |
|---|---|---|---|---|
| D96 | 44 | **5** | **5** | 5 |
| D96-3D | 12 | 9 | 9 | 9 |
| random | 95 | **18** | **17** | 17 |
| physical | 48 | 5 | 5 | 5 |
| unphysical | 3 | 3 | 3 | 3 |

From a single fittest species, D96 discovers a total of **5** species; random discovers **18**
and keeps 17.

### Mutation sweep (D96, β=1)

| μ | 0.001 | 0.01 | 0.1 | 0.3 | 0.5 | 0.8 | 0.95 |
|---|---|---|---|---|---|---|---|
| S∞ | 3 | 5 | 6 | 8 | 9 | 11 | 13 |

Even at μ=0.95 (selection nearly erased), D96 reaches only 13 survivors. The value 19 is never
hit short of the degenerate μ→1 uniform limit (S∞→A=44).

---

## Findings

1. **D96's natural species ceiling is ~5, not ~19.** S∞(D96) = 5 at μ=0.01, β=1 (T_010), and
   its discovery trajectory (fittest-init) discovers a total of 5 species.

2. **~19 is nearest the RANDOM landscape, not D96.** Random's survivor count is 17 and its
   cumulative discovery is 18 — within ±2 of 19 — whereas D96's is 5.

3. **19 is not reachable for D96 short of erasing selection.** The mutation sweep saturates at
   13 (μ=0.95); only the degenerate uniform limit (μ→1, all A=44 modes) exceeds 19, and that
   is the trivial ceiling, not a physical prediction.

## Classification

| Item | Classification |
|---|---|
| D96 species ceiling = S∞ = min(A, N_fit) ≈ 5 (T_008/T_009/T_010) | DERIVED |
| "~19 species emerges naturally from D96" | REFUTED |
| The legacy ~19 value is nearest the random/near-degenerate landscape (17–18) | DERIVED (by measurement) |
| The legacy ~19 (AT-138/139) arises from a different model (Θ-field pattern novelty) | EMERGENT |

---

## Conclusion

**REFUTED.** The ~19-species value does **not** emerge naturally from D96. D96's spectral
blueprint produces a species ceiling of **~5** (both as equilibrium survivors and as total
discovery), by the exact T_008/T_009 laws `S∞ = min(A, N_fit(μ, β, {w}))` with D96's sparse,
peaked fitness field (T_010). The value ~19 is instead nearest the **random** landscape's
signature (17 survivors, 18 cumulative discoveries) — a dense, near-degenerate spectrum, the
opposite of D96. The legacy AT-138/139 "~19 stable species" therefore traces to a *different*
model (Θ-field pattern novelty with novelty threshold 0.4), not to the D96 spectral blueprint.
No new primitive; canonical AT unchanged.
