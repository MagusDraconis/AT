# AT-QG Phase 273 — Assignment Principle Audit

**Status:** COMPLETE — **PARTIAL ASSIGNMENT**
**Tests:** ATQG2730, ATQG2731, ATQG2732 (all passed)
**Core class:** `AT.Core/ResearchXH/AssignmentPrincipleAudit.cs`
**Question:** why does a projection become mass, coupling, mixing, gravity, or cosmology instead of another sector? Is there a D96-native assignment rule?
**Method:** no observables, no target values, D96 only, deterministic.

---

## 1. The Structural Assignment Features

| Feature | D96-native | Determines sector |
|---------|------------|-------------------|
| **dimension** (me-anchored) | no (me is free) | **mass** — the only dimensional read |
| **unitarity** (V†V = I) | yes | **mixing** — the only unitary matrix |
| **log/global** (ln(span), I_occ/ln K) | yes | **cosmology** — the only log-of-spectrum read |
| **power ≥ 2** (M_Pl cube) | yes | **gravity** — the only power≥2 combination |
| **ratio** (3/Σm, #d/(2Σm)) | yes | **ambiguous** — coupling OR mixing OR mass-ratio |

**4/5 features are D96-native and each determines a sector by form** — a partial
assignment rule exists.

---

## 2. The Decisive Evidence (assignment is not by form alone)

```
m_τ/m_μ (MASSES)   = √occMom·λ₂ = 16.842
y_τ/y_μ (COUPLINGS) = √occMom·λ₂ = 16.842   (identical form)
```

**The IDENTICAL formula √occMom·λ₂ is assigned to BOTH the mass sector (m_τ/m_μ,
QG209) and the coupling sector (y_τ/y_μ, QG247 — since y_f = m_f/v).**

The assignment of this read is NOT determined by its structure — it is determined
by the **theoretical role** (which equation it appears in: the mass hierarchy vs
the Yukawa Lagrangian). Similarly, Vus = #d/(2Σm) is structurally a coupling-like
ratio; only its placement in the unitary CKM matrix makes it a mixing angle.

---

## 3. Sector Determinability by Form

| Sector | Determinable | Note |
|--------|--------------|------|
| mass | ✓ | the only dimensional read |
| cosmology | ✓ | the only log-of-spectrum read |
| gravity | ✓ | the only power≥2 read |
| mixing | partial | unitarity is structural, but individual ratios look like couplings |
| coupling | ✗ | a ratio read is ambiguous — could be coupling, mixing, or mass-ratio |

**3/5 sectors determinable by form alone.**

---

## 4. Conclusion

### **PARTIAL ASSIGNMENT** (the decisive blocker)

A D96-native assignment rule exists:
```
R1 dimensional → mass
R2 unitary     → mixing
R3 log/global  → cosmology
R4 power ≥ 2   → gravity
ratio-class    → ROLE-BASED (ambiguous by structure)
```

But the **ratio-class is NOT separable by structure**: the identical form
√occMom·λ₂ is both a mass ratio and a Yukawa coupling. The duplication is the
**decisive blocker** — a complete assignment principle would require every read
to map uniquely, and this one maps to two sectors.

**This is the precise location of the QG271 frontier:** 4 structural rules + a
residual role-based step. The operator → physics assignment is **partially
derivable** (dimension/log/power/unitarity classes are structural) and
**partially role-based** (the ratio class depends on which equation the read
enters — the target-informed step).

**The reduction chain (QG260→273):**
```
Resonance Layer → Operator Layer → Same Operator Sectors
→ Single Resonance Dynamics → Single Resonance Invariant
→ Universal Conservation → Self-Consistency
→ Individuation → Difference Principle
→ Post-Resonance Integrity (frontier = assignment)
→ Sector Emergence (sectors = projection classes)
→ PARTIAL ASSIGNMENT (4 structural rules + role-based ratio-class)
```
