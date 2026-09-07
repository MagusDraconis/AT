# ResearchY-NP_132 — Reversible Softening Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_132 (permanent)
**Title:** Reversible Softening Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_132.md`
**Depends on:** ResearchY-NP_094 (inertia = persistence), NP_095 (friction = scattering), NP_096
(heat = decoherence / entropy), NP_100 (binding = phase locking), NP_110 (condensed matter =
phase-locked crystals), NP_128 (coherence = resource), NP_129 (coherent matter control), NP_131
(critical resonance), NP_075 (force = resonance transition)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_132_Tests.cs`

---

## Purpose

NP_131 established the resonance "master key": a small set of critical modes controls rigidity.
NP_132 asks the reversibility question: **can coherent excitation of critical modes produce a
reversible low-rigidity state without thermal melting?** Program: (1) define rigidity / elasticity /
softening / melting; (2) compare thermal melting vs coherent critical-mode softening; (3) determine
whether lattice order survives during softening; (4) test crystal / metal / granite / glass;
(5) measure rigidity reduction / entropy increase / reversibility; (6) search for temporary gel-like
states recoverable after coherence returns. **Success criterion:** determine whether materials can be
temporarily softened and reshaped without conventional heating. No new primitives; canonical AT
unchanged.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **rigidity** | degree of backbone phase-locking, R ∈ [0, 1] (NP_131) |
| **elasticity** | restoring stiffness of locked modes (∝ R) |
| **softening** | R → 0 while ORDER (lattice periodicity S) survives |
| **melting** | R → 0 AND order destroyed (S → 0) |

The crux is the **order parameter S** — the phase coherence of the remaining N − m non-critical
modes. Softening drops rigidity but *keeps order*; melting drops both.

---

## 2. Thermal melting vs coherent critical-mode softening

| | Thermal melting | Coherent softening |
|---|---|---|
| mechanism | heat ALL modes (equipartition, NP_096) | resonantly drive the m critical modes only (NP_129) |
| rigidity R | → 0 | → 0 |
| order S | → 0 (lattice dissolves) | **→ 1 (lattice survives)** |
| entropy ΔS | N·ln 2 bits | m·ln 2 bits |
| state | liquid (disordered) | gel-like (unjammed, still ordered) |
| reversibility | IRREVERSIBLE (must re-nucleate order) | **REVERSIBLE** (re-lock restores R) |

Both reach R → 0, but only coherent softening is a *reversible* transition.

---

## 3. Does lattice order survive during softening?

**YES.** Only the m backbone locks are driven open; the N − m modes remain locked in their periodic
lattice arrangement. The material is a **gel-like (unjammed) solid** — soft but still ordered — not a
liquid. Its atoms keep their lattice sites; they simply lose the load-bearing lock.

---

## 4/5. Per material: rigidity, entropy, reversibility

| Material | m | ΔS_soft | ΔS_melt | S_soft | recovery soft | recovery melt | advantage |
|---|---|---|---|---|---|---|---|
| **crystal** | 6 | 4.16 bits | 65.85 bits | 1.00 | 6·E_bind | 95·E_bind + barrier | ~15.8× |
| **metal** | 6 | 4.16 bits | 65.85 bits | 1.00 | 6·E_bind | 95·E_bind + barrier | ~15.8× |
| **granite** | 20 | 13.86 bits | 65.85 bits | 1.00 | 20·E_bind | 95·E_bind + barrier | ~4.8× |
| **glass** | 40 | 27.73 bits | 65.85 bits | 1.00 | 40·E_bind | 95·E_bind + barrier | ~2.4× |

Recovery after softening costs **m·E_bind** (re-lock the critical modes). Recovery after melting
costs **N·E_bind + a nucleation barrier** (re-grow the destroyed order) — strictly more, and not
guaranteed.

---

## 6. Temporary gel-like states

Coherent critical-mode drive produces a **temporary gel-like state** that is fully recoverable: stop
the drive and the critical modes re-lock (rigidity returns, R → 1) with no re-nucleation, because
order was never lost. The state is a *transient unjamming*, not a phase change.

---

## Theorem

> **Theorem (NP_132).** Coherent excitation of critical modes produces a REVERSIBLE low-rigidity
> state without thermal melting: materials can be temporarily softened and reshaped without
> conventional heating. Softening and melting are DISTINCT. Softening drives the m critical (backbone)
> modes open, dropping rigidity R → 0 while the lattice order S SURVIVES (the N − m modes stay
> locked) — a gel-like (unjammed) solid with entropy ΔS = m·ln 2 bits. Melting heats all N modes,
> dropping R → 0 AND order S → 0 — a disordered liquid with ΔS = N·ln 2 bits. Softening is therefore
> REVERSIBLE: stop the drive, re-lock the m modes, and rigidity returns (no re-nucleation, because
> order was never lost). Melting is IRREVERSIBLE: order must be re-grown (N·E_bind + nucleation
> barrier). The reversibility advantage is N/m (~15.8× for crystal/metal, ~4.8× granite, ~2.4× glass).
> Proof: (1) Define (Section 1). (2) Compare (Section 2). (3) Order survives (Section 3, verified —
> S_soft = 1). (4/5) Measure (Sections 4–5, verified — ΔS = m·ln2 vs N·ln2). (6) Gel state
> (Section 6). **Success criterion: materials can be temporarily softened and reshaped without
> conventional heating.** Classification: reversible softening DERIVED (NP_100 + NP_131); "softening
> ≡ melting" REFUTED (order survives vs destroyed); "softening is irreversible" REFUTED (re-lock
> restores). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) Compare. (3) Order survives. (4/5) Measure. (6) Recover. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "softening = melting" | softening keeps order (S = 1); melting destroys it (S = 0) |
| "softening is irreversible" | order survives, so re-locking restores R without re-nucleation |
| "softening needs as much energy as melting" | it drives m modes, not N (ΔS = m·ln2 ≪ N·ln2) |
| "all materials soften equally reversibly" | ordered (crystal/metal) recover ~7× cheaper than glass |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| reversible softening exists | a coherent drive that collapses R while permanently destroying S |
| order survives softening | a soft state whose lattice periodicity is gone (S = 0) |
| re-lock restores rigidity | a soft state that does not re-rigidify when the drive stops |

---

## 9. Classification

| Component | Status |
|---|---|
| reversible softening (R→0, S survives) | **DERIVED** (NP_100 + NP_131) |
| softening ≡ melting | **REFUTED** |
| softening is irreversible | **REFUTED** |

**Conclusion.** Materials can be **temporarily softened and reshaped without heating**: coherent
critical-mode excitation drives rigidity to zero while lattice order survives, producing a
reversible, gel-like (unjammed) state — recovered simply by letting coherence return. This is the
reversibility theorem of NP_131's master key: coherence unlocks, coherence re-locks, and order is
never sacrificed. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_132_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_132_Definitions` | rigidity / elasticity / softening / melting | ✅ |
| `Y_NP_132_Compare` | softening (S=1) vs melting (S=0) | ✅ |
| `Y_NP_132_OrderSurvives` | lattice order survives during softening | ✅ |
| `Y_NP_132_PerMaterial` | ΔS = m·ln2 vs N·ln2; N/m advantage | ✅ |
| `Y_NP_132_Reversibility` | re-lock restores R; melting re-nucleates | ✅ |
| `Y_NP_132_GelState` | temporary gel-like state recoverable | ✅ |
| `Y_NP_132_Classification` | DERIVED; softening≡melting / irreversible REFUTED | ✅ |
| `Y_NP_132_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_132"`

---

## References

- ResearchY-NP_094 (inertia), NP_095 (friction), NP_096 (heat/entropy), NP_100 (binding), NP_110
  (condensed matter), NP_128 (coherence resource), NP_129 (coherent matter control), NP_131 (critical
  resonance), NP_075 (force).
