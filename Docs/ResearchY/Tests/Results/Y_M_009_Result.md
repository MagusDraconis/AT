# Y_M_009_Result.md — ResearchY-M_009 Measurement Prediction Discriminator Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_009_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_009"`

---

## Summary

**Question:** Do AT-P042 and AT-P043 predict anything beyond standard QM?

**Verdict:** **EXACTLY ONE survives the discriminator.**

## AT-P042 — discrete tick phase advance (Δθ = 2πk/N)

**C — GENUINELY NEW (PREDICTION).** Standard QM has continuous time and a continuum
of reachable phases. AT derives a discrete tick COUNT with a FINITE phase lattice
{θ₀ + m·2πk/N}, of cardinality N/gcd(N,k):

| k | lattice cardinality |
|---|---|
| 16 | 6 |
| 1 | 96 |
| 48 | 2 |

Mathematically testable (deterministic lattice); experimental discrimination requires
sub-tick phase resolution (in-principle only, tick scale uncalibrated).

## AT-P043 — log₂(95) per-event information bound

**A — ALREADY IMPLIED by QM (CORRESPONDENCE, downgraded).** The per-event bound
log₂(d) is the standard d-outcome Shannon entropy bound. For d = 95 it is
log₂(95) ≈ 6.57 bits — QM imposes exactly the same limit (max entropy of a 95-outcome
distribution). AT-P043's only AT-specific content is the derived VALUE d = 95 (D_039),
not a new bound structure.

## Prediction table

| Prediction | QM equivalent? | Unique? | Experimentally testable? |
|---|---|---|---|
| AT-P042 discrete tick | NO (continuous in QM) | **YES** — finite lattice vs continuum | in-principle (sub-tick resolution) |
| AT-P043 log₂(95) bound | YES (standard d-outcome bound) | NO — only d=95 value AT-derived | not a discriminator |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_009_PhaseDiscriminator` | lattice vs continuum (N/gcd(N,k) reachable phases) | ✅ |
| `Y_M_009_InformationLimit` | log₂(d) d-outcome bound (standard, d=95) | ✅ |
| `Y_M_009_QMComparison` | QM imposes the same info bound | ✅ |
| `Y_M_009_PredictionUniqueness` | AT-P042 unique; AT-P043 not | ✅ |
| `Y_M_009_FalsificationPath` | falsification paths for both | ✅ |
| `Y_M_009_Run` | research report | ✅ |

## Conclusion

The discriminator keeps AT-P042 and downgrades AT-P043. **The V2.2 measurement program
yields exactly ONE uniquely-AT prediction: AT-P042, the discrete tick time-parameter.**
AT-P043 is a QM-standard bound whose only AT content is the derived value d = 95.
No new primitive; canonical AT unchanged.
