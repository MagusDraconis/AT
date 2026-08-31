# Y_QG_016_Result.md — ResearchY-QG_016 Tick Discreteness Origin Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_016_Tests.cs`
**Run:** 2026-08-31
**Result:** ✅ 6/6 PASSED
**Full suite:** 660/660 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_016"`

---

## Summary

**Question:** Why must the actualization tick be discrete? Is tick discreteness a
primitive boundary or a consequence of Difference?

**Verdict:** Tick discreteness is a **BOUNDARY**. Difference implies discrete STATES
(a discrete set, DERIVED) but NOT discrete EVENTS (the stepwise dynamics).

## The two discretenesses

| Discreteness | Object | Status |
|---|---|---|
| state space | discrete SET of 95 states (D_039) | **DERIVED** (Difference → set) |
| dynamics tick | stepwise advance Δθ = 2πk/N (D_041) | **BOUNDARY** (canonical input) |

## Continuous actualization is observationally equivalent (M_010)

Continuous actualization with rate ω = 2πk/(N·τ) reproduces AT-P042 EXACTLY at every
tick-sampled time — phase, recurrence (N/gcd(N,k)), interference, and orbits are
identical. **Observability does NOT force discrete dynamics** — finite events need
discrete READS, which can sample a continuous evolution.

## First inconsistency of continuous actualization: STRUCTURAL

- The phase advance Δθ = 2πk/N **loses its spectral derivation** (D_041) — becomes a
  free continuous parameter.
- AT-P042 (the discrete lattice) is demoted from the fundamental clock to a sampling
  artifact.
- Observability, information, normalization, count conservation all survive.

## Prove/refute: Difference implies discrete events — REFUTED

Difference → discrete STATES (membership) but NOT discrete EVENTS (the advance).
The step VALUE Δθ = 2πk/N is DERIVED from the spectrum; the stepwise DYNAMICS is the
input.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_016_DiscreteTick` | the lattice structure; Δθ = 2πk/N derived | ✅ |
| `Y_QG_016_ContinuousActualization` | continuous sampling is observationally equivalent | ✅ |
| `Y_QG_016_PhaseLattice` | N/gcd(N,k) cardinalities; AT-P042 | ✅ |
| `Y_QG_016_InformationGain` | finite info per event; continuous breaks derivation | ✅ |
| `Y_QG_016_BoundaryReduction` | discreteness not reducible to Difference/observability | ✅ |
| `Y_QG_016_Run` | research report | ✅ |

## Conclusion

Tick discreteness is a BOUNDARY — Difference implies discrete STATES (DERIVED) but
not discrete EVENTS. Continuous actualization is observationally equivalent at
sampled times (M_010), so observability does not force discreteness; its
inconsistency is structural (the phase advance loses its spectral derivation). The
step VALUE Δθ = 2πk/N is DERIVED from the spectrum (D_041); the stepwise DYNAMICS is
the canonical input. No new primitive; canonical AT unchanged.
