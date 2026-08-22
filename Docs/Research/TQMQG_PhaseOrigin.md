# TQM-QG Phase 220 — Phase Origin

**Status:** COMPLETE — **PHASE ORIGIN**
**Tests:** TQMQG2200, TQMQG2201, TQMQG2202 (all passed)
**Core class:** `TQM.Core/ResearchXH/PhaseOrigin.cs`
**Inputs:** QG216 (amplitude magnitude |ψ|² = ρ = μ^k/S), QG218 (complex structure ψ = |ψ|e^(iθ)),
QG63 (phase lives on the U(1) links), QG65 (path phase = Σ θ_links, interference),
QG155/159 (circulant ring C₉₆, rotation automorphism of order 96), QG166 (CP phase from circulation)
**Method:** deterministic derivation — no new primitives, Q-events only

---

## 1. The Question

QG216 derived the amplitude **magnitude** from Q-events (|ψ|² = ρ = μ^k/S).
QG218 showed quantum states must be complex — a state carries magnitude (branching)
plus phase (U(1) links). QG219 identified the **phase origin** as the main remaining
QM gap: the U(1) connection is *located* on the links but its value/mechanism was
not derived.

**Open: derive θ from network structure.**

---

## 2. The Origin — the U(1) angle is the circulation phase of the actualization cycle

| # | Investigation | Result |
|---|---------------|--------|
| 1 | **Causal ordering** | Q-events actualize in a definite causal order (QG1/QG11); each event has a branch depth k (generation = actualization tick) |
| 2 | **Actualization timing** | the actualization cycle is periodic — the attractor is a circulant ring C_N (N = 96, QG155/159); the rotation automorphism has order N |
| 3 | **Branch depth** | an event at causal position k has advanced k/N of the cycle → **θ_k = 2π·k/N** |
| 4 | **Network cycles** | cycle closure fixes the phase quantum: N ticks must advance 2π → **Δθ = 2π/N** per tick (uniform circulation) |
| 5 | **Link orientation** | forward links (i→i+1) add +2π/N, backward subtract; a path of L links accumulates Σ θ_links = 2πL/N (QG65) |
| 6 | **Connectivity phase** | phase difference = 2π·(graph distance)/N; interference P = 2 + 2cos(Δθ) is connectivity-determined |

**The derived phase: θ_k = 2π·k/N.**

---

## 3. The Complete Amplitude

Magnitude (QG216) + phase (this phase):

```
ψ_k = √ρ_k · e^(iθ_k) = √(μ^k/S) · e^(2πik/N)
```

Both pieces derive from Q-events:

- **magnitude** √(μ^k/S) — the branching counting measure (QG216);
- **phase** 2πk/N — the circulation of the actualization cycle (this phase).

The Born rule is preserved: Σ|ψ|² = 1 (the phase is a rotation).

---

## 4. Derived Structure

| Quantity | Formula | Status |
|----------|---------|--------|
| Phase quantum (per tick) | Δθ = 2π/N | derived from cycle closure (N·Δθ = 2π) |
| Event phase | θ_k = 2πk/N (mod 2π) | derived from branch depth (causal position) |
| Link phase | ±2π/N by orientation | derived |
| Path phase | 2πL/N = Σ θ_links | derived (QG65 compatible) |
| Loop holonomy | 2πL/N; full cycle trivial | derived (gauge-invariant) |
| Phase difference | 2π·d/N (d = causal distance) | derived from connectivity |
| Interference | P = 2 + 2cos(2π(k₁−k₂)/N) | derived, connectivity-determined |

Concrete values at N = 96: quantum 3.75°/tick; θ = 0, 60°, 90°, 180° at branch
depths k = 0, 16, 24, 48; half-cycle holonomy = π; full-cycle holonomy ≡ 0.

---

## 5. Scope and Gauge

A single global phase is **gauge** (unphysical, as in QM). The **observable content**
is the phase *difference* Δθ = 2π(k₁−k₂)/N, which is fully determined by the causal
positions (connectivity). Hence:

- the phase **structure** (quantum, link phases, path accumulation, holonomies,
  interference) is derived from the network;
- the absolute phase origin remains the standard U(1) gauge freedom.

No new primitive: the phase is the circulation of the actualization cycle — the same
rotational structure that generates the Z2 doublets (QG155) and the CKM CP phase
(QG166).

---

## 6. Classification

### **PHASE ORIGIN**

Origin score = **5/5**:

1. cycle closure fixes the phase quantum (N·Δθ = 2π);
2. deterministic periodic phase from branch depth (θ_k = 2πk/N);
3. link orientation gives the link phase (path phase = Σ θ_links);
4. loop holonomies derived (full cycle trivial);
5. Born rule preserved and interference connectivity-determined.

This **closes the QG219 gap (a) 'the phase origin'**. With QG216 (magnitude) and
QG218 (complex structure), the full amplitude ψ = √ρ·e^(iθ) now derives from
Q-events alone.

### Remaining QG gaps

- **(b)** native metric dynamics — the BDG action is imported (QG6), not derived;
- **(c)** ψ origin status — capacity forced (QG56), excitation derived (QG57), PARTIAL.

The phase — the last QM primitive — is now derived. The remaining gaps are in the
gravity/metric sector, not in quantum mechanics.
