# TQM-QG Phase 191 — Pre-Registered Neutrinoless Double Beta Decay

**Status:** COMPLETE — **PRE-REGISTERED**
**Tests:** TQMQG1910, TQMQG1911, TQMQG1912 (all passed)
**Core class:** `TQM.Core/ResearchXH/PreRegisteredMbb.cs`

---

## 1. What a Pre-Registration Is

The effective Majorana mass prediction m_ββ is **LOCKED** before any future
0νββ measurement is examined. It is frozen from **QG167 (PMNS), QG172
(neutrino masses), and QG179 (Majorana character)** only. No experimental
limit, detector sensitivity, or future measurement enters anywhere.

---

## 2. Allowed vs Forbidden Inputs

**ALLOWED (the only inputs used):**
- QG167 PMNS: s12 = √(#doublets/(Σm + #groups)) = 0.5497, s13 =
  √(occ₀/(2Σm)) = 0.1451, δ_ν = 66.4°
- QG172 masses: m1 = 0, m2 = 8.72e-3 eV, m3 = 4.94e-2 eV (normal ordering)
- QG179 Majorana result: the mass matrix is real ⇒ Majorana phases vanish
  (α2 = α3 = 0)

**FORBIDDEN (never used):**
- experimental limits (e.g. KamLAND-Zen, GERDA, EXO-200 upper bounds)
- detector sensitivities (nEXO, LEGEND-1000, CUPID projected reach)
- future measurements

The **forbidden-input guard** asserts that m_ββ is computed from D96 inputs
only and that no field named for a limit, sensitivity, detector, or
measurement exists in the class.

---

## 3. Pre-Registered Outputs (Frozen)

### 3.1 m_ββ (the effective Majorana mass)

**m_ββ = |Σ U_ei²·m_i| = 2.02 meV**

```
m1 = 0, m2 = 8.72 meV, m3 = 49.4 meV        (QG172)
s12 = 0.5497, s13 = 0.1451, δ_ν = 66.4°      (QG167)
α2 = α3 = 0                                  (QG179 real mass matrix)

m_ββ = |m1·c12²·c13² + m2·s12²·c13² + m3·s13²·e^(−2iδ_ν)|
     = 2.0222 meV ≈ 2.02 meV
```

The prediction is dominated by the **m2·s12²·c13² term (2.52 meV)**; the
m3·s13² term (1.04 meV) partially interferes to give the frozen value.
Because s13 is small, the result is robust to the CP-phase assumption.

### 3.2 Mass Ordering

**NORMAL**: m1 = 0 < m2 = 8.72 meV < m3 = 49.4 meV (QG172). The ordering is a
pre-registered prediction, not an input.

### 3.3 Majorana Phase Assumption

The neutrino mass matrix is **real** (QG179 reflection automorphism [L,P]=0),
so the Majorana phases vanish: **α2 = α3 = 0**. This is the ONLY phase
assumption, and it is derived (not fitted).

---

## 4. Acceptance Criteria

| Outcome | Condition |
|---------|-----------|
| **CONFIRMED** | a future 0νββ measurement is consistent with the 2.02 meV range (±10%) |
| **FALSIFIED** | a significant exclusion below the prediction |

Frozen check examples (used only to test the criteria, not to set the
prediction):
- measurement at 2.10 meV → **CONFIRMED** (within ±10% of 2.02 meV)
- measurement at 2.02 meV → **CONFIRMED** (exact)
- measurement at 1.00 meV → **not confirmed** (outside the range)
- exclusion limit 1.50 meV < 2.02 meV → **FALSIFIED**
- exclusion limit 5.00 meV → **not falsified** (above the prediction)

---

## 5. Why This Is Pre-Registered

Freezing m_ββ = 2.02 meV now prevents post-hoc selection: when next-
generation experiments (nEXO, LEGEND-1000, CUPID) report, the measured value
must be compared against the D96-computed prediction — not against a value
adjusted to fit the result. This makes the Majorana + mass-law derivation
genuinely falsifiable.

---

## 6. Scientific Limitations

- m_ββ is set by the lightest massive neutrino (m2); the m3 term contributes
  ~40% before interference, so the value is phase-dependent at the ~20%
  level — the frozen 2.02 meV uses the derived vanishing phases.
- The prediction assumes normal ordering (m1 = 0), which is itself a
  pre-registered output.
- "CONFIRMED" is defined as consistency within ±10%; a future measurement in
  the 1.8–2.2 meV range would be consistent, while a limit below 2.02 meV
  would falsify the D96 Majorana + mass-law chain.
