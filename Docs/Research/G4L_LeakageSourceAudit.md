# G4-L Analytical Audit — The Origin of the Residual Feynman Tail

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Status:** COMPLETED — analytical audit (verified by matrix decomposition; no new physics)
**Inputs:** L3, R1, H2, D4, Phase 5–8 results
**Question:** Why does the native operator plateau at ~40–55 % leakage?

---

## 1. Matrix decomposition (verified: max|diff| = 0)

With R1 = past-directed (retarded) layer operator, R2 = R1ᵀ = future-directed (advanced),
L3 = symmetric layer operator:

```
L3   = R1 + R2                 (symmetric bidirectional alternation)
H2   = R1 + L3 = 2·R1 + R2     (full future weight → leak 0.70+)
A3   = R1 + R2_decayed         (future entries ÷ (k+1))
H    = R1 + A3 + D = 2·R1 + R2_decayed + D      ← the Phase-7 best operator
```

R2_decayed carries entries (−1)^(k+1)/(k+1) in the FUTURE (upper-triangular) direction only.

---

## 2. Tail source analysis (verified)

| operator | leakage | spectrum (n+, n−) | indefinite |
|---|---|---|---|
| D + 2R1 (future removed) | **0.082** | (0, 72) | ❌ elliptic |
| D + 2R1 + R2 (full future) | 0.770 | — | ✅ |
| D + 2R1 + R2_decayed (native H) | **0.428** | (30, 42) | ✅ |
| BDG retarded (lower-tri only) | 0.021 | (0, 72) | ❌ |
| BDG symmetric (BdgReference) | — | (29, 43) | ✅ |

**The future-directed (upper-triangular) component is the tail source.** Removing it entirely
gives a *causal* operator (leak 0.082) — but one that is **not indefinite** (all eigenvalues ≈
diagonal ≈ −0.75, an elliptic, sign-definite operator). Restoring it at full weight gives the full
Feynman tail (0.770); decaying it 1/(k+1) gives the plateau (0.428).

---

## 3. Term-by-term comparison (H vs BDG, N = 72)

| matrix term | native H | BDG (retarded) | max|H−BDG| |
|---|---|---|---|---|
| diagonal | ≈ −0.75 (−0.1875·degree) | −2 | 72/72 differ |
| past (lower) off-diagonal | 2·(−1)^(k+1), all k (2,−2,2,−2,…) | +4 (k=0), −2 (k=1), 0 (k≥2) | 6.0 |
| **future (upper) off-diagonal** | (−1)^(k+1)/(k+1), **1344 nonzero** | **0 (strictly retarded)** | 1.0 |

**Minimal structural difference:** the future off-diagonal. BDG's retarded operator has **zero**
future coupling; the native operator has 1344 (decayed) future entries. This single term — not the
diagonal — is what separates a causal propagator from the Feynman-tailed one.

---

## 4. Classification

| component | classification | reason |
|---|---|---|
| Symmetric (future) contribution of L3 | **ESSENTIAL** (dual) | it is BOTH the Lorentzian-signature source (indefiniteness) AND the tail source |
| BDG diagonal −2 | **ESSENTIAL role / OPTIONAL value** | some negative diagonal is required for a well-posed (non-nilpotent) operator; the exact −2 is not (native −0.75 works); it only *partially* suppresses the tail via diagonal dominance |
| Future off-diagonal (which generates spacelike propagation) | **ESSENTIAL** (the tail itself) | upper-triangular entries cause advanced (anti-causal) propagation = Feynman tail |
| Layer-alternation depth (non-truncation at k ≥ 2) | **OPTIONAL** | affects KS distance and the exact plateau value, not the tail's existence |
| R1's nilpotence (no diagonal) | **ARTIFACT** | Phase-6 A1 "leak 0.569" was a pseudoinverse artifact of the singular nilpotent R1; any diagonal removes it |

---

## 5. Conclusion — the signature–causality tension

The plateau is **not a missing coefficient and not a refinement artifact**. It is the irreducible
trade-off of demanding that a **single matrix** be simultaneously

1. **retarded** (causal: lower-triangular ⇒ eigenvalues equal the diagonal ⇒ *never* indefinite), and
2. **indefinite** (Lorentzian signature: requires a symmetric/bidirectional component, which
   necessarily reintroduces future/advanced coupling).

A strictly-retarded matrix has a sign-definite spectrum (all −2, or all −0.75); indefiniteness
*requires* the future component; and the future component *is* the Feynman tail. The two properties
are mathematically coupled — one term, two effects.

**BDG itself resolves this by using two different objects:** the symmetric d'Alembertian □
(BdgReference, indefinite 29+/43−) carries the signature, while the *retarded Green function*
(lower-triangular, non-indefinite) carries causality. The native G4-L program tried to fold both
into one matrix, and the ~40–55 % leakage plateau is where that forced compromise lands.

**Bottom line:** the residual Feynman tail is the irreducible price of native Lorentzian
*signature* inside a single retarded-biased operator. It cannot be removed by a diagonal (Phase 7)
or by refinement (Phase 8); only the BDG split — signature operator vs retarded propagator — is
leak-free, and that split is outside the native single-matrix constraint.
