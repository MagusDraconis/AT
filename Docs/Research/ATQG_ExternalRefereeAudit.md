# AT-QG Phase 250 — External Referee Audit

**Status:** COMPLETE — the hostile referee's top-25 strongest attacks (attack only, no defense)
**Tests:** ATQG2500, ATQG2501, ATQG2502 (all passed)
**Core class:** `AT.Core/ResearchXH/ExternalRefereeAttack.cs`
**Review:** QG0–QG249
**Method:** hostile-referee attack; the referee does not defend AT

---

## 1. The Verdict

### **2 FATAL / 14 MAJOR / 8 MINOR / 1 EDITORIAL**

A hostile referee would **not accept the internal audit program as evidence**:
the coverage register is self-maintained, the closure/referee audits are
self-authored, the BOUNDARY labels are self-assigned to every hard gap, and
the passing test suite validates the formulas it encodes.

---

## 2. The Two FATAL Attacks

**F1 — Parameter leakage.** The D96 moment set (Σm, #d, #g, occMom, λ₂, span,
Σ√m, occ=[4,4,87]) is not fixed before the derivations, plus the me anchor and
multiplicative factors (5/4, √3, 1/2, 2). Reproducing ~25 fermion/cosmological
quantities with this many knobs is over-parameterized fitting, not derivation.
The referee demands: effective free-parameter count > number of derived targets.

**F2 — Self-confirmation.** Every derivation is validated by a test the same
phase writes and asserts. Passing only means the code matches the formula the
phase chose. There is no independent, pre-committed falsification of the
derivations themselves — only of P1-P3. If the formulas are effective
numerology, the test suite cannot detect it.

---

## 3. The Top-25 Attacks (Summary)

| # | Severity | Focus | Attack (condensed) | Hits |
|---|----------|-------|--------------------|------|
| 1 | FATAL | Parameter leakage | D96 moment set + factors can fit ~25 targets | all closed-form laws |
| 2 | FATAL | Effective vs fundamental | tests validate the formulas they encode | validation architecture |
| 3 | MAJOR | Unjustified selection | N=96 selected by criteria that ARE the physics | QG159/160 |
| 4 | MAJOR | Hidden assumption | flat η imported; conformal class assumed | QG207 |
| 5 | MAJOR | Hidden assumption | me = 0.511 MeV is a free input anchor | QG140/173/209 |
| 6 | MAJOR | Unjustified selection | n_s/acoustic retro-selection (5/4, √3) | QG237/238 |
| 7 | MAJOR | Effective vs fundamental | y_f = m_f/v is definitional, not a derivation | QG247/248 |
| 8 | MAJOR | Hidden assumption | uniform initial state = maximum-ignorance postulate | QG227/228 |
| 9 | MAJOR | Unjustified selection | octave grouping [4,4,87] chosen to give 3 families | QG155/210 |
| 10 | MAJOR | Boundary classification | Bekenstein 1/4 is a real gap, not a boundary | QG185/196 |
| 11 | MAJOR | Effective vs fundamental | per-particle mass fits, not one unified law | QG173/209/203 |
| 12 | MAJOR | Boundary classification | theory resolves its own objections | audit program |
| 13 | MAJOR | Unjustified selection | 3+1 via constraints chosen to yield 3+1 | QG2/3/161 |
| 14 | MAJOR | Parameter leakage | 1/α_em = 137 = Σm+#d is an asserted dictionary | QG162 |
| 15 | MAJOR | Hidden assumption | ψ is a hand-placed second primitive | QG23-57 |
| 16 | MAJOR | Effective vs fundamental | mass mechanism = same data read twice | QG168/169/246/247 |
| 17 | MINOR | Effective vs fundamental | Λ derives scaling, not the value | QG230 |
| 18 | MINOR | Hidden assumption | H is an epoch-scale input | QG77/233 |
| 19 | MINOR | Contradiction | Poisson white seed vs tilted CMB spectrum | QG231/237/238 |
| 20 | MINOR | Effective vs fundamental | no quantization of gravity; hybrid theory | QG14/216-224 |
| 21 | MINOR | Boundary classification | metric is only PARTIAL UNIQUE (ψ alternatives) | QG207 |
| 22 | MINOR | Falsification | P1/P2 can stay PENDING indefinitely | QG190-193 |
| 23 | MINOR | Effective vs fundamental | RG imported from MS̄, not derived | QG163/164/204 |
| 24 | MINOR | Hidden assumption | 1.08 bits cannot account for observed complexity | QG228 |
| 25 | EDITORIAL | Publication weakness | no peer review, no external replication | QG0-249 |

---

## 4. The Referee's Bottom Line

The strongest attacks cluster on **parameter leakage** (F1, #11, #14), **retro-
selection** (#3, #6, #9, #13), **self-confirming validation** (F2, #12), and
**definitional/effective derivations** (#7, #16). The BOUNDARY classifications —
the theory's own mechanism for closing gaps — are themselves the most attacked
feature: a hostile referee reads "BOUNDARY" as "failure relabeled."

**Attack only. No defense is offered.**
