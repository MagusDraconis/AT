# TQM-QG Phase 251 — Parameter Independence Audit

**Status:** COMPLETE — **LOW** parameter-leakage risk (effective independent parameters = 2)
**Tests:** TQMQG2510, TQMQG2511, TQMQG2512 (all passed)
**Core class:** `TQM.Core/ResearchXH/ParameterIndependenceAudit.cs`
**Motivation:** QG250's FATAL attack F1 claimed the D96 moment set is "eight independent knobs"
**Method:** audit the dependency structure of the nine D96 parameters; deterministic

---

## 1. The Question

How many of the nine D96 parameters are genuinely independent? Could a
critic reasonably claim over-parameterization?

---

## 2. The Dependency Structure (the key finding)

**All eight spectral quantities descend from ONE object** — the D96 network
spectrum: the degeneracy multiset of the observable sector

```
m = [2,2,2,…,2 (42×), 5, 6]     #g = 44 groups, Σm = 95 modes
```

plus the octave occupancies of that same spectrum, occ = [4,4,87].

| Parameter | Value | Status | Source |
|-----------|-------|--------|--------|
| Σm (total modes) | 95 | DEPENDENT | Σ of the multiset |
| #d (doublet pairs) | 42 | DEPENDENT | count of m_i = 2 in the same multiset |
| #g (groups) | 44 | DEPENDENT | group count of the same multiset |
| span | 6.4025 | DEPENDENT | eigenvalue ratio of the same spectrum |
| λ₂ (spectral gap) | 0.38635 | DEPENDENT | gap of the same network's Laplacian |
| Σ√m (half-moment) | 64.08 | DEPENDENT | Σ √m_i of the same multiset |
| occ (octave occupancies) | [4,4,87] | DEPENDENT | band occupancies of the same spectrum |
| occMom (occupation moment) | 1900.25 | DERIVED | Σ occ²/occ₀ — a function of occ |
| **me (electron anchor)** | 0.511 | **INDEPENDENT** | the single free empirical input |

**None of the eight spectral quantities is independently adjustable** — each
is fixed the moment the D96 network (the universal attractor, QG116b/159/160)
is given. They are moments, counts, ratios, and gaps **of the same spectrum**,
not eight free knobs.

---

## 3. The Effective Parameter Count

```
Independent listed parameters (me)            = 1
+ D96 structural selection (the network)      = 1
EFFECTIVE INDEPENDENT PARAMETER COUNT         = 2
```

The observable register catalogs ~40 physical quantities (35 tested). The
derived-target-to-free-input ratio is ≈ **20:1** — an order of magnitude
above the 1:1 that would signal fitting.

---

## 4. The Determination

### **LOW** parameter-leakage risk

The QG250 F1 attack's premise — **eight independent knobs** — is factually
wrong: the eight quantities collapse to one spectrum. With 2 effective free
inputs against ~40 targets, the count basis does not support an
over-parameterization claim.

**Residual (separate) risk:** formula *selection* — which specific combination
of the locked quantities was picked post-hoc (n_s and acoustic peaks, QG239;
QG250 #6). That is a distinct claim, already flagged as RETRO-SELECTION RISK,
and is not adjudicated by the parameter-count audit.

**Answer to the critic:** the critic cannot reasonably claim
over-parameterization on parameter *count*; the honest remaining attack is
formula *selection*, which TQM has already disclosed (QG239) and blind-tested
(QG240 BLIND SUCCESS).
