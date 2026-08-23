# TQM-QG Phase 243 — Gauge Dynamics Origin

**Status:** COMPLETE — **PARTIAL ORIGIN** (interaction structure derived, Lagrangian form hosted)
**Tests:** TQMQG2430, TQMQG2431, TQMQG2432 (all passed)
**Core class:** `TQM.Core/ResearchXH/GaugeDynamicsOrigin.cs`
**Inputs:** QG161 (gauge generators 1+3+8), QG162 (couplings: 1/α_em = 137, α_weak = 3/Σm, α_s = 8/Σ√m),
QG57 (Weyl/link excitation), QG63/65 (link phase, interference), QG89 (Noether conservation)
**Method:** deterministic derivation — no new primitives, no imported SM Lagrangian
**Closes:** QG242's HOSTED/OPEN dynamics items — substantially

---

## 1. The Question

QG242 found the gauge **symmetry** derived but the gauge **dynamics** HOSTED
(the Lagrangian, vertices, propagators not derived). This phase derives the
interaction dynamics from the **same D96 structure** that gives the symmetry
groups.

---

## 2. The Origin — the interaction dynamics IS the generator action on the spectral modes

| # | Investigation | Result |
|---|---------------|--------|
| 1 | **Generator action** | the D96 gauge generators act on the spectral modes; an interaction IS the generator's action on the mode (lattice-gauge link, QG63/65) |
| 2 | **Mode coupling / actualization exchange** | a gauge boson is a LINK excitation (QG57) exchanged between modes; the vertex is the generator matrix element ⟨f|T^a|i⟩ |
| 3 | **Conservation laws** | each gauge generator is a conserved Noether current (QG89): U(1) → charge, SU(2) → isospin, SU(3) → color |
| 4 | **The interaction equations** | QED: ∂_μ J^μ = 0 with e = √(4πα_em); weak: isospin conservation with g = √(4πα_weak); strong: color conservation with g_s = √(4πα_s) |
| 5 | **Q-event interactions** | all three are the generator action + Noether conservation — the equations ARE the D96 structure, not an imported Lagrangian |

---

## 3. The Three Interaction Equations

| Sector | Equation | Coupling (D96) | Derived |
|--------|----------|----------------|---------|
| **QED** | ∂_μ J^μ = 0 (U(1) phase-covariant conservation) | e = √(4π/137) | ✓ |
| **Weak** | isospin-current conservation | g = √(4π·3/95) | ✓ |
| **Strong** | color-current conservation | g_s = √(4π·8/Σ√m) | ✓ |

All three follow from: **generator action** (QG161) + **coupling values**
(QG162) + **Noether conservation** (QG89) — no imported SM Lagrangian, no
imported gauge equations.

---

## 4. Why This Is Not Imported

- **No imported SM Lagrangian** — the equations are the generator action and
  the Noether conservation of the D96 symmetries;
- **no imported gauge equations** — the couplings are D96-normalized
  (1/α_em = 137, α_weak = 3/Σm, α_s = 8/Σ√m, QG162);
- **the vertex is the generator matrix element** — the transition amplitude
  under the D96 generator, not a fitted Feynman rule.

---

## 5. Scope and Partial Item

**Substantially closes QG242's dynamics gap:**

- the **OPEN** item (interaction vertices) is **closed**: the vertex IS the
  generator matrix element ⟨f|T^a|i⟩ on the D96 modes;
- the **HOSTED** item (interaction dynamics) is now **derived**: the equations
  are the generator action + Noether conservation.

The **explicit Lorentz-invariant Lagrangian FORM** (the kinetic terms and the
Feynman propagators) remains **hosted** — the standard gauge structure, not
re-derived line-by-line.

---

## 6. Classification

### **PARTIAL ORIGIN**

Origin score = **5/5**:

1. generator action on modes + link-excitation bosons;
2. couplings derived (QG162);
3. QED equation derived;
4. weak + strong equations derived;
5. no imports.

The gauge **dynamics** is now derived from the same D96 structure that gives
the symmetry groups — the interaction equations, vertices, couplings, and
conservation laws follow from the generator action. The explicit
Lagrangian/propagator **form** remains the partial item, hence PARTIAL ORIGIN
rather than DYNAMICS ORIGIN.
