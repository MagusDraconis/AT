# ResearchY-NP_076 — Psi Dark Matter Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_076 (permanent)
**Title:** Psi Dark Matter Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_076.md`
**Depends on:** ResearchY-NP_065 (dark matter = deficit), NP_066 (dark matter evidence),
NP_067 (lensing sector: ψ restores γ=+1), NP_068 (ψ = spin-2 graviton, tensor face of
Difference), AT-QG QG43/44 (ψ spin-2 Fierz-Pauli, GW), QG19 (GW requires ψ), QG26 (PPN
γ=−1), QG186 (frame dragging), QG194/195 (deficit matter, T_μν), QG206 (flat rotation),
QG212 (conformal optics via ψ), QG223 (ψ second primitive), QG234 (Ωm = H/ln K)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_076_Tests.cs`

---

## Purpose

NP_065–068 established Dark Matter = the deficit m = ρ̄ − ρ (a scalar effect that gravitates)
and ψ = the massless spin-2 graviton (the tensor face of Difference, restoring lensing). NP_076
asks a pointed question: **can the ψ graviton sector also account for part of the observed Dark
Matter signal?** Program: (1) inventory the observations (rotation, lensing, Bullet, cluster
mass); (2) separate the deficit contribution from the ψ contribution; (3) test whether ψ
produces A) propagating waves only, B) stationary bound configurations, or C) an effective mass
density; (4) determine whether ψ can mimic Dark Matter; (5) compare deficit-only / ψ-only /
deficit+ψ. **Success criterion:** determine whether Dark Matter is deficit-only, ψ-only, hybrid,
or neither. No new primitives; canonical AT unchanged.

---

## 1. Inventory and the deficit/ψ partition

| Observation | What it probes | Deficit (ρ, scalar) | ψ (tensor, spin-2) |
|---|---|---|---|
| rotation curves | gravitational potential (g₀₀) | **YES** (flat, α=0) | NO (ψ carries no trace/potential source) |
| cluster mass | total gravitating mass | **YES** (Coma, degenerate) | NO (massless, no rest mass) |
| Ωm = 0.3161 | matter density fraction | **YES** (H/ln K) | NO (Ω_ψ ≈ 0) |
| gravitational lensing | null geodesics (PPN γ) | NO (γ=−1) | **YES** (γ=+1) |
| Bullet Cluster | collisionless separation | NO | NO (neither is a particle) |

The partition is clean: the **deficit carries the dark-matter mass** (potential, rotation,
cluster mass, Ωm), and **ψ carries the optics** (lensing, frame dragging, GW). They are
complementary, not competitive.

---

## 2. What ψ produces — A / B / C

| Test | Result | Reason |
|---|---|---|
| **A) propagating waves only** | **YES** | ψ is the *massless* spin-2 field (Fierz–Pauli, QG44); it propagates transverse-traceless waves (2 polarizations) at c (QG43) |
| **B) stationary bound configurations** | **REFUTED** | a massless field has no Yukawa mass term (m_ψ = 0); the linear spin-2 field is non-self-interacting at first order, so it cannot self-bind into a stationary halo |
| **C) effective mass density** | **REFUTED** | ψ has **no rest mass**; its energy density is **radiation-like** (w = 1/3, ρ_ψ ∝ a⁻⁴) and observationally Ω_gw ≈ 10⁻⁹, ~9 orders below Ωm = 0.3161 |

**ψ produces A (propagating waves) ONLY.** It does not bind (B) and does not supply matter
density (C).

---

## 3. Why ψ cannot mimic Dark Matter

Dark Matter is, observationally, **cold, pressureless, non-relativistic, collisionless matter**
(w = 0, ρ ∝ a⁻³) that clusters and separates from baryons. ψ fails every one of these:

| Requirement | ψ's actual behavior | Match? |
|---|---|---|
| non-relativistic (cold) | massless ⇒ relativistic (v = c) | **NO** |
| pressureless (w = 0) | radiation-like (w = 1/3) | **NO** |
| ρ ∝ a⁻³ (matter scaling) | ρ_ψ ∝ a⁻⁴ (radiation scaling) | **NO** |
| Ω ≈ 0.316 | Ω_gw ≈ 10⁻⁹ | **NO** |
| clusters into halos | linear massless field, no self-binding | **NO** |
| separates collisionlessly (Bullet) | not a particle | **NO** |

**ψ cannot mimic Dark Matter.** It is the graviton — the *geometry* that carries gravity and
bends light — not a dark-matter substance.

---

## 4. ψ's actual role in the dark-matter signal

ψ does not contribute dark-matter *mass*, but it is not irrelevant: it is what makes the
deficit's mass **visible through lensing**. The deficit sources a conformally flat metric
(γ=−1) that pulls mass but does not bend light (NP_067); ψ breaks conformal flatness (γ=+1) so
the deficit's mass distribution can be reconstructed from null geodesics. In one sentence: **the
deficit is the dark matter; ψ is the gravity (the geometry) through which we see it.**

This is the same conclusion as NP_075: gravity is the metric geometry (ρ + ψ), not a gauge
force. ψ is the graviton, a force/geometry object, not a matter object.

---

## 5. Deficit-only / ψ-only / deficit+ψ

| Model | Rotation | Cluster | Ωm | Lensing | Bullet | Verdict |
|---|---|---|---|---|---|---|
| **deficit-only** | ✓ (α=0) | ✓ (degenerate) | ✓ (0.3161) | ✗ (γ=−1) | ✗ | reproduces mass, misses optics |
| **ψ-only** | ✗ (no trace) | ✗ (massless) | ✗ (Ω_gw≈0) | ✓ (γ=+1) | ✗ | reproduces optics, has no mass to lens |
| **deficit + ψ** | ✓ | ✓ | ✓ | ✓ | ✗ | reproduces mass + optics; still no collisionless particle |

**Neither model is complete alone; deficit + ψ reproduces everything except the Bullet
Cluster.** But the "hybrid" is not a hybrid *dark-matter* — it is a **division of labor**: the
deficit is the mass, ψ is the optics. ψ is not dark matter; it is the graviton.

---

## Theorem

> **Theorem (NP_076).** ψ (the massless spin-2 graviton) CANNOT account for any of the observed
> Dark Matter mass: it produces propagating waves only (A) — no stationary bound configurations
> (B, REFUTED: m_ψ = 0, no Yukawa binding) and no effective mass density (C, REFUTED: w = 1/3,
> ρ_ψ ∝ a⁻⁴, Ω_gw ≈ 10⁻⁹ ≪ Ωm). ψ's role in the dark-matter signal is to restore LENSING
> (γ = +1), making the deficit's mass visible through null geodesics — but it contributes zero
> dark-matter mass. Dark Matter (the mass) is therefore **DEFICIT-ONLY**; the full observational
> signal is a deficit (mass) + ψ (optics) partition, where ψ is the graviton (gravity's geometry,
> NP_075), not a dark-matter candidate. Proof: (1) Partition (Section 1, verified): deficit
> carries rotation/cluster/Ωm; ψ carries lensing/frame-dragging/GW. (2) A/B/C (Section 2):
> massless spin-2 ⇒ waves only; m_ψ=0 forbids bound states; w=1/3, ρ∝a⁻⁴, Ω_gw≈10⁻⁹ forbids
> matter density. (3) Mimicry (Section 3): ψ fails cold (v=c), pressureless (w=1/3), a⁻³
> scaling, Ω=0.316, halo clustering, and collisionless separation. (4) Role (Section 4): ψ makes
> the deficit lens (γ=+1) but adds no mass. (5) Comparison (Section 5, verified): deficit-only =
> mass without optics; ψ-only = optics without mass; deficit+ψ = full signal except Bullet, but
> ψ is the graviton, not dark matter. Classification: ψ propagating waves DERIVED (QG43/44);
> stationary ψ-bound configurations REFUTED; ψ effective mass density REFUTED; ψ mimics dark
> matter REFUTED; the deficit+ψ signal partition DERIVED (NP_066/067). **Success criterion: Dark
> Matter is DEFICIT-ONLY (the mass); ψ is the graviton that makes the deficit visible, not a
> dark-matter component.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Partition the observations. (2) Test A/B/C. (3) Show ψ fails every dark
> matter requirement. (4) State ψ's true role. (5) Compare the three models. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ψ is part of the dark matter" | ψ is massless (m_ψ=0), radiation-like (w=1/3, ρ∝a⁻⁴), Ω_gw≈10⁻⁹ — it cannot be the Ωm=0.3161 matter |
| "ψ forms a dark-matter halo" | a massless, linear spin-2 field has no Yukawa binding and no self-interaction to cluster |
| "ψ supplies the missing mass" | ψ has no rest mass and no trace source; the mass is the deficit (ρ̄−ρ), not the graviton |
| "ψ-only reproduces dark matter" | ψ restores lensing but has no mass to lens — rotation/cluster/Ωm all vanish without the deficit |
| "deficit+ψ is a two-component dark matter" | ψ is the graviton (gravity's geometry, NP_075), not a matter component; the mass is deficit-only |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ψ is massless | a ψ with m_ψ > 0 (massive gravity) in the AT setting |
| ψ produces waves only | a stationary self-bound ψ configuration (a ψ "halo") |
| ψ has no matter density | an Ω_gw today approaching Ωm = 0.316 |
| ψ cannot mimic dark matter | a ψ that reproduces cold, pressureless, a⁻³, collisionless behavior |
| dark matter is deficit-only | a dark-matter observable that requires ψ mass rather than deficit mass |

---

## 8. Classification

| Component | Status |
|---|---|
| ψ = propagating waves only (massless spin-2) | **DERIVED** (QG43/44) |
| stationary bound ψ-configurations | **REFUTED** (m_ψ = 0, no binding) |
| ψ effective mass density | **REFUTED** (w = 1/3, ρ ∝ a⁻⁴, Ω_gw ≈ 10⁻⁹) |
| ψ mimics dark matter | **REFUTED** (not cold/pressureless/matter/clustering) |
| the deficit (mass) + ψ (optics) partition | **DERIVED** (NP_066/067) |
| dark matter = deficit (the mass) | **DERIVED** (NP_065) |

**Conclusion.** The ψ graviton sector **cannot account for any part of the observed Dark Matter
mass**: it is the massless spin-2 field that propagates waves only, forms no bound
configurations, and supplies no matter density (w = 1/3, ρ ∝ a⁻⁴, Ω_gw ≈ 10⁻⁹). Its actual role
in the dark-matter signal is to restore **lensing** (γ = +1) so the deficit's mass becomes
visible — but it contributes zero dark-matter mass. **Dark Matter is therefore DEFICIT-ONLY;
ψ is the graviton (the geometry) through which the deficit is seen, not a dark-matter
component.** No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_076_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_076_DeficitPsiPartition` | deficit = mass, ψ = optics | ✅ |
| `Y_NP_076_PsiPropagatesOnly` | ψ massless ⇒ waves only (A) | ✅ |
| `Y_NP_076_PsiNoBoundStates` | m_ψ = 0 ⇒ no stationary configurations (B REFUTED) | ✅ |
| `Y_NP_076_PsiNoMassDensity` | w = 1/3, ρ ∝ a⁻⁴, Ω_gw ≈ 10⁻⁹ (C REFUTED) | ✅ |
| `Y_NP_076_PsiCannotMimicDM` | ψ fails cold/pressureless/a⁻³/Ω/cluster | ✅ |
| `Y_NP_076_ModelComparison` | deficit-only / ψ-only / deficit+ψ | ✅ |
| `Y_NP_076_Classification` | waves DERIVED; bound/mass/mimic REFUTED | ✅ |
| `Y_NP_076_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_076"`

---

## References

- ResearchY-NP_065 (dark matter = deficit), NP_066 (dark matter evidence), NP_067 (lensing
  sector, ψ restores γ=+1), NP_068 (ψ = spin-2 graviton).
- AT-QG: QG43/44 (ψ spin-2, GW), QG19 (GW requires ψ), QG26 (PPN γ=−1), QG186 (frame
  dragging), QG194/195 (deficit matter), QG206 (flat rotation), QG212 (conformal optics),
  QG223 (ψ second primitive), QG234 (Ωm = H/ln K).
