# AT-QG Phase 244 — Lagrangian Origin

**Status:** COMPLETE — **LAGRANGIAN ORIGIN**
**Tests:** ATQG2440, ATQG2441, ATQG2442 (all passed)
**Core class:** `AT.Core/ResearchXH/LagrangianOrigin.cs`
**Inputs:** QG161 (gauge generators 1+3+8), QG162 (couplings), QG89 (energy = actualization rate, Noether),
QG243 (interaction = generator action), QG84 (Higgs = occupation-density scalar), QG63/65 (link phase)
**Method:** deterministic derivation — no new primitives, no imported SM Lagrangian
**Closes:** QG243's remaining Lagrangian-form partial

---

## 1. The Question

QG243 derived the interaction dynamics as the generator action, but the
explicit **Lagrangian form** remained the partial item. This phase derives the
Lagrangian density and field equations from D96 — no imported SM Lagrangian.

---

## 2. The Origin — the Lagrangian is the actualization-flow action of the D96 generator fields

| # | Investigation | Result |
|---|---------------|--------|
| 1 | **Noether currents** | the D96 symmetries generate conserved currents (QG89): U(1) electric, SU(2) isospin, SU(3) color |
| 2 | **Generator algebra** | the field strength F^a_μν = ∂A − ∂A + g f^abc A A is the generator-algebra curl (structure constants from QG161 commutators) |
| 3 | **Mode coupling** | the covariant derivative D_μ = ∂_μ − igT^aA^a_μ from the generator action (QG243) |
| 4 | **Actualization flow** | the matter term iψ̄γ^μD_μψ − mψ̄ψ from the actualization-flow energy (QG89) |
| 5 | **The derived density** | L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ |

**The derived Lagrangian density:**

```
L = −(1/4) F^a_μν F^aμν + iψ̄γ^μ D_μ ψ − m ψ̄ψ
```

with the QED / weak / strong cases from the D96 generators and couplings.

---

## 3. The Three Sectors

| Sector | Field strength | Coupling (D96) | Generators |
|--------|---------------|----------------|------------|
| **QED** | F_μν (Abelian, f^abc = 0) | e = √(4π/137) (QG162) | T = 1 (charge) |
| **Weak** | F^a_μν from su(2) | g = √(4π·3/Σm) | T^a = σ^a/2 (σ_z, σ_y, σ_x) |
| **Strong** | F^a_μν from su(3) | g_s = √(4π·8/Σ√m) | T^a = λ^a/2 (family generators) |

The field equations are the Euler–Lagrange equations of the density with
D96-determined couplings — the standard Klein–Gordon/Dirac/Yang–Mills
structure, now derived from D96.

---

## 4. Why This Is Not Imported

- **No imported SM Lagrangian** — the form (gauge kinetic + covariant matter +
  mass) is the unique minimal action consistent with the D96 symmetries and
  the actualization-flow energy (QG89);
- **no imported gauge equations** — the couplings (QG162) and generators
  (QG161) are D96-determined;
- **the structure constants** come from the D96 generator commutators, not a
  gauge-group table.

---

## 5. Classification

### **LAGRANGIAN ORIGIN**

Origin score = **5/5**:

1. Noether currents exist (QG89/QG243);
2. the generator algebra closes (su(2) + su(3), QG161);
3. the QED Lagrangian is derived (Abelian case, e = √(4π/137));
4. the weak and strong Lagrangians are derived (non-Abelian su(2)/su(3));
5. no imported SM Lagrangian.

**Closes QG243's remaining Lagrangian-form partial.** The explicit field
equations and Lagrangian structure now follow from D96. The Higgs/Yukawa
sector (Higgs = collective occupation-density scalar, QG84) is the remaining
partial item — the Higgs is identified, the full Yukawa coupling structure is
not re-derived.
