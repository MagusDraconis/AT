# TQM-QG Phase 179 — Majorana Origin

**Status:** COMPLETE — **MAJORANA ORIGIN**
**Tests:** TQMQG1790, TQMQG1791, TQMQG1792 (all passed)
**Core class:** `TQM.Core/ResearchXH/MajoranaOrigin.cs`

---

## 1. Starting Point

Known: QG154 (neutrino origin: the UNIQUE Q = 0 sector with T3-ONLY access),
QG172 (neutrino masses: m1 = 0, m2 = 8.72e-3, m3 = 4.94e-2 eV), QG167 (PMNS
angles).

**Open problem:** Is the neutrino DIRAC or MAJORANA — can its character be
derived from D96 spectral geometry — no fitted assumptions, deterministic?

---

## 2. Method

The neutrino character is derived from four structural facts and one
numerical prediction:

1. **Degrees of freedom** — Dirac needs a particle/antiparticle pair over the
   full spectrum; Majorana is self-conjugate. The neutrino has T3-ONLY access
   (QG154): exactly the T3 = +1/2 (even) channel, 48 of 95 modes. There is no
   separate antiparticle channel.
2. **Charge** — Majorana requires no conserved charge; the neutrino is the
   unique Q = 0 sector. Dirac requires a charge distinguishing ν from ν̄.
3. **Z2 doublets** — one member per doublet accessed; each is self-conjugate.
4. **Reflection symmetry** — [L,P] = 0 (QG174) makes the spectrum and masses
   real; a real Majorana mass term is allowed.
5. **0νββ** — m_ββ = |Σ U_ei²·m_i| from the D96 masses and PMNS angles.

---

## 3. Results

### 3.1 Degrees of Freedom

```
full intra-sector modes = 95
neutrino access (T3=+1/2 channel) = 48   (QG154)
T3=−1/2 channel (not accessed)    = 47
access fraction = 0.505

self-conjugate by access: TRUE
```

The neutrino reaches only the T3=+1/2 channel. There is no separate
antiparticle channel to host a Dirac partner — the neutrino is
self-conjugate.

### 3.2 Charge

```
unique Q=0 fermion sector: TRUE
no conserved charge separates ν from ν̄: TRUE
```

The neutrino is the unique neutral sector (QG154). No charge distinguishes
particle from antiparticle — the Majorana prerequisite holds.

### 3.3 Z2 Doublets

```
T3+ channel octave occupancies = [2, 2, 44]
doublet member self-conjugate: TRUE
```

The neutrino occupies one member of each Z2 doublet; with no distinct
antiparticle member accessed, each accessed member is its own conjugate.

### 3.4 Reflection Symmetry

```
reflection is an exact graph automorphism (QG174): TRUE
arg det M = 0 (real masses): TRUE
real mass matrix: TRUE
```

The real spectrum allows a real Majorana mass term M·ν·ν; no complex Dirac
phase exists.

### 3.5 0νββ Expectation

```
m1 = 0 eV, m2 = 8.72e-3 eV, m3 = 4.94e-2 eV   (QG172)
s12 = 0.5497, s13 = 0.1451, δ_ν = 66.4°        (QG167)

m_ββ = |Σ U_ei²·m_i| = 2.02e-3 eV
experimental limit m_ββ < 0.036–0.156 eV: TRUE (within)
non-zero (decay allowed): TRUE
```

With Majorana neutrinos the neutrinoless double-beta decay rate is set by
m_ββ. The D96 prediction 2.02e-3 eV is non-zero, within current limits, and
in reach of next-generation experiments.

---

## 4. Classification

**Majorana-origin score: 5 / 5**

- +1 self-conjugate by access (T3-only channel)
- +1 unique neutral sector (no conserved charge)
- +1 Z2 doublet member self-conjugate
- +1 real mass matrix (reflection automorphism)
- +1 0νββ non-zero and within experimental limit

```
CLASSIFICATION: MAJORANA ORIGIN
```

- **NO ORIGIN rejected:** the D96 structure fully determines the character.
- **DIRAC ORIGIN rejected:** the neutrino has T3-only access — no antiparticle
  channel — and is the unique Q=0 sector (no conserved charge).
- **MAJORANA ORIGIN accepted.**

---

## 5. Conclusion

The **neutrino is Majorana by D96 spectral geometry**:

1. **Self-conjugate access** — the neutrino has T3-ONLY access (QG154),
   reaching only the T3 = +1/2 channel (48 of 95 modes). There is no separate
   antiparticle channel, so the neutrino is its own antiparticle.

2. **Unique neutral sector** — the neutrino is the unique Q = 0 sector; no
   conserved charge separates ν from ν̄ (the Majorana prerequisite).

3. **Z2 doublet self-conjugation** — one member per Z2 doublet is accessed;
   each is self-conjugate.

4. **Real mass matrix** — the reflection automorphism (QG174) makes the
   spectrum and masses real; a real Majorana mass term is allowed and no
   complex Dirac phase exists.

5. **0νββ prediction** — m_ββ = |Σ U_ei²·m_i| = **2.02e-3 eV** (from the D96
   masses and PMNS angles), non-zero and within the current experimental
   limit — a testable Majorana signature.

All from D96 spectral geometry with **no fitted assumptions**.

---

## 6. Chain

```
period-3 seed (QG160)
  → D96 selection (QG159)
  → neutrino origin (QG154: unique Q=0, T3-only access)
  → neutrino masses (QG172: m1=0, m2, m3)
  → PMNS origin (QG167: angles, δ_ν)
  → strong CP reflection (QG174: real spectrum/masses)
  → MAJORANA ORIGIN (QG179)                                                        ← THIS PHASE
      self-conjugate T3-only channel (48/95 modes)     (no antiparticle channel)
      unique Q=0 sector                                (no conserved charge)
      real mass matrix                                 (reflection automorphism)
      m_ββ = |Σ U_ei²·m_i| = 2.02e-3 eV                (0νββ, within limits)
      → neutrino is MAJORANA
```
