# TQM-QG Phase 1 — Microscopic Origin of ρ

**Program:** TQM-QG (Unification)
**Phase:** 1 — derive ρ directly from microscopic Q-event actualization dynamics.
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

The chain is Q-events → counting measure → ρ → geometry → gravity. Here we ask whether the observed ρ profile
emerges *uniquely from microscopic event dynamics* — event generation rules, branching statistics,
abundance-law growth, causal-set accumulation, actualization probabilities — and compare the generated ρ to
the gravity-required ρ. Classify: FULL / PARTIAL / NO MATCH.

---

## 2. The microscopic model: Q-event branching

Actualization is modeled as a **Galton–Watson branching process** over logarithmic (octave) layers. Each
deficit quantum branches to produce, on average, μ descendants in the next octave, so the per-octave deficit
counts are A_k = A₀·μ^k. The branching ratio maps to the abundance exponent via

  μ = λ^(−α)   ⟺   α = −ln μ / ln λ,

so **criticality (μ=1) ⟺ α=0** (the log-deficit attractor).

---

## 3. Results

### (a) Branching → α; critical μ=1 → α=0 → log deficit (TQMQG10)

- μ ↔ α round-trips exactly (α=0→μ=1, α=0.5→μ=0.8165, α=1→μ=0.6667).
- Critical μ=1 gives uniform per-octave counts, whose cumulative deficit equals the log deficit
  m_k = m₀·ln(Rmax/R_k)/ln(Rmax/r₀) **exactly** (0.400, 0.300, 0.200, 0.100 at k=0,4,8,12).

### (b) Branching-generated ρ equals the gravity density (TQMQG11)

The branching density reproduces the gravity-required `AbundanceDeficit` **exactly** (match to 1e-12 for all
α), and at α=0 reproduces all four gravity requirements: ρ>0 (metric origin), m>0 (deficit matter),
G_11/G_ii non-trivial (Einstein), v²(3)/v²(9)=1.18 (flat rotation).

### (c) Criticality is the unique scale-free branching point (TQMQG12)

The intrinsic scale of a branching process is L = 1/|ln μ| octaves. Only μ=1 has L → ∞ (no preferred scale);
sub/supercritical processes (μ=0.9, μ=1.1) have finite L. Scale-freeness (renormalization invariance, TQM-F1)
therefore selects μ=1, i.e. α=0, **uniquely**.

---

## 4. Classification: FULL MATCH (conditional on scale-freeness = criticality)

- The microscopic actualization rule — **critical Q-event branching** (μ=1) — generates **exactly** the ρ the
  gravity program requires (the log-deficit density), via μ=λ^(−α) with α=0.
- This single ρ reproduces the metric origin, deficit matter, Einstein structure, and flat rotation curves.
- The chain is closed at the microscopic level: **Q-events → critical branching → α=0 → log-deficit ρ → gravity**.
- The one remaining input is scale-freeness itself — the criticality of the branching — which is the
  renormalization-invariance requirement already reduced in TQM-F1.

---

## 5. Conclusion

ρ now emerges from microscopic dynamics: critical (scale-free) Q-event branching is the unique scale-free
actualization process, and it produces exactly the log-deficit density that drives the entire gravity program.
Combined with TQM-QG0 (the actualization attractor = the gravity density), the unification is complete from
the microscopic event-generation rule up to flat rotation curves — with scale-freeness (criticality) as the
single, already-justified input.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG10 `TQMQG10_BranchingToAlpha` | PASS (μ↔α exact; critical → log deficit) |
| TQMQG11 `TQMQG11_ReproducesGravityDensity` | PASS (branching ρ = gravity ρ, all requirements) |
| TQMQG12 `TQMQG12_CriticalityClassification` | PASS (FULL MATCH, criticality = scale-freeness) |

Code: `TQM.Core/ResearchXH/QEventBranching.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase1_MicroscopicOriginOfRhoTests.cs`.
