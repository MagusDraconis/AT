# AT-QG Phase 218 — Hilbert Origin

**Status:** COMPLETE — **HILBERT ORIGIN**
**Tests:** ATQG2180, ATQG2181, ATQG2182 (all passed)
**Core class:** `AT.Core/ResearchXH/HilbertOrigin.cs`
**Known:** amplitude magnitude derived (QG216), phase located on U(1) links (QG63)
**Method:** no new primitives, deterministic

---

## 1. The Question

Quantum states must be **complex** — ψ = |ψ|·e^(iθ). Why? Given the derived
amplitude magnitude and the located phase, show that the complex structure
is forced.

---

## 2. The Derivation

### 2.1 Two real degrees of freedom

A quantum state carries exactly **two independent real numbers**:

| DOF | Source | Property |
|-----|--------|----------|
| **Magnitude** |ψ| = √ρ | branching counting measure (QG216) | node |
| **Phase** θ | U(1) link connection (QG63) | link |

Neither reduces to the other (a node property vs a link property).

### 2.2 Interference requires the phase

QG65: P = |e^(iθ₁) + e^(iθ₂)|² = **2 + 2cos(θ₁ − θ₂)**.

- Constructive (θ₁=θ₂=0): P = 4
- Intermediate (θ₂=π/2): P = 2

A **real-only** state (single real number, no phase) gives classical addition
P = P₁ + P₂ = 2 — **no interference**. Interference is phase-dependent, which
a real state space cannot reproduce.

### 2.3 A state with magnitude and phase is a complex number

```
ψ = |ψ|·e^(iθ) = |ψ|cosθ + i·|ψ|sinθ
```

The two real DOFs are exactly the polar form of a complex amplitude. The
complex structure is **not postulated** — it is the mathematical necessity of
carrying a magnitude AND a phase per state.

### 2.4 The Hilbert space is over ℂ

- Superposition: ψ = Σ a_k φ_k with **complex** a_k
- Inner product: ⟨ψ|φ⟩ = Σ a_k* b_k (ℂ-bilinear)
- Born rule: P = |⟨φ|ψ⟩|² — the ℂ-inner-product probability

A **real** Hilbert space cannot reproduce interference; a **quaternionic**
one would add structure with no source. The **ℂ structure is uniquely forced**
by the (magnitude, phase) pair.

### 2.5 Consistency

QG74's general measurement uses unitary rotations (U(1), SU(2), J) and the
Born rule in any basis — all ℂ-linear operations. The graph-Laplacian
eigenbasis (AT-149) is the Hilbert space; with complex amplitudes it is the
standard ℂ Hilbert space.

---

## 3. Origin Score (4/4)

| Channel | Value | Held? |
|---------|-------|-------|
| Interference phase-dependent (real-only impossible) | 4 vs 2 | ✓ |
| State with magnitude + phase is complex | Re≠|ψ|, Im≠0 | ✓ |
| Complexity forced by the two DOFs | both hold | ✓ |
| ℂ Hilbert + Born consistent with QG74 | unitary ℂ-linear | ✓ |

---

## 4. Conclusion

**HILBERT ORIGIN.** Quantum states MUST be complex:

- The network provides exactly **two real DOFs** per state — the magnitude
  |ψ| = √ρ (branching, QG216) and the phase θ (U(1) links, QG63).
- A state carrying both is a **complex number** ψ = |ψ|·e^(iθ).
- Only a **ℂ Hilbert space** reproduces interference and the Born rule.

No new primitive — the complexity is **forced by the (magnitude, phase)
pair**. The Hilbert space is over ℂ because the actualization structure
supplies a magnitude (from branching) and a phase (from the U(1) links).
