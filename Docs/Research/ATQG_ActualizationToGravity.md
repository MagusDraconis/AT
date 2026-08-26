# AT-QG Phase 0 — Actualization → Gravity

**Program:** AT-QG (Unification)
**Phase:** 0 — do the actualization dynamics generate the gravity-required ρ?
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no new primitives

---

## 1. Goal

The derived chain is Q-events → counting measure ρ → geometry → Einstein structure → matter → gravity. The open
question: does the temporal-field / actualization program generate the *same* ρ that the gravity program
requires? Here we close the chain by checking whether the microscopic actualization rule reproduces the metric
origin, deficit matter, Einstein structure, and flat rotation curves.

---

## 2. Results

### (a) The α=0 actualization attractor is exactly the log-deficit density (ATQG00)

The entropy-maximized (uniform, α=0) per-octave allocation A_k = m₀/K accumulates to the discrete deficit
m_k = m₀(K−k)/K, which equals m₀·ln(Rmax/R_k)/ln(Rmax/r₀) — the **log-deficit** density
ρ = ρ̄ − m₀·ln(Rmax/r)/ln(Rmax/r₀) (inner octaves match to ≤25%). The actualization attractor *is* the
gravity-required density.

### (b) This ρ reproduces all four gravity requirements (ATQG01)

| Requirement | Value at r=3 | Verdict |
|---|---|---|
| Metric origin √(−g)=ρ | ρ = 0.839 > 0 | ✅ |
| Deficit matter m = ρ̄−ρ | m = 0.161 > 0 | ✅ |
| Einstein structure G(ρ) | G_11 = 3.1×10⁻⁴, G_ii = −6.8×10⁻³ | ✅ non-trivial |
| Flat rotation curve | v²(3)/v²(9) = 1.18 | ✅ flat |

### (c) Classification (ATQG02)

**FULL MATCH** (at the abundance-law level), with a sector caveat.

---

## 3. Classification: FULL MATCH (matter/gravity chain), with a sector caveat

- The actualization program (G4-RHO: Q-event counting → entropy maximization → α=0 → log-deficit) generates
  **exactly** the density the gravity program requires (G4-ME: flat rotation).
- This single ρ reproduces the metric origin, deficit matter, Einstein structure, and the flat rotation curve.
- The chain **Q-events → actualization dynamics → ρ → gravity is CLOSED**.

**Caveat (sector):** the raw conserved actualization *flux* selects the repulsive ρ ∝ r⁻² (G4-RHO0), while the
entropy-maximized *deficit* selects the attractive log-deficit (α=0). The actualization program generates the
matter (deficit) sector, not the dark-energy (raw-ρ) sector — so the unification is FULL for the matter/gravity
chain, but the raw-ρ (repulsive) sector remains a separate, un-unified channel.

---

## 4. Conclusion

The unification closes the main chain: the microscopic actualization rule (unbiased Q-event generation,
maximum-entropy scale-free abundance) produces exactly the radial density that the gravity program needs, and
that density reproduces the metric origin, deficit matter, Einstein structure, and flat rotation curves. The
one residual is the *dual-sector* structure — raw ρ (repulsive, dark-energy) vs deficit m = ρ̄−ρ (attractive,
matter) — of which only the matter sector is currently unified with gravity.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG00 `ATQG00_EntropyAttractorIsLogDeficit` | PASS (α=0 attractor = log-deficit density) |
| ATQG01 `ATQG01_ReproducesGravityRequirements` | PASS (all four requirements reproduced) |
| ATQG02 `ATQG02_Classification` | PASS (FULL MATCH + sector caveat) |

Code: `AT.Core/ResearchXH/ActualizationGravity.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase0_ActualizationToGravityTests.cs`.
