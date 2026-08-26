# AT-QG Phase 259 — Observable Origin Audit

**Status:** COMPLETE — **MEDIUM observable-selection risk**
**Tests:** ATQG2590, ATQG2591, ATQG2592 (all passed)
**Core class:** `AT.Core/ResearchXH/ObservableOriginAudit.cs`
**Scope:** observable audit only — no formula complexity, no uniqueness (that is QG253)

---

## 1. The Question

For each major result — masses, couplings, mixings, cosmology, GR observables —
was the **observable** selected because a D96 formula matched it (post-hoc), or
because D96 naturally points to it?

This is distinct from QG253 (formula audit). Here we ask only: *where did the
target itself come from?*

**Classification rules:**
- **NATURAL TARGET** — D96 structure alone leads to the observable: a structural
  identity (family count, θ_QCD = 0), an octave-forced class (lepton ratios), or a
  value frozen/hidden before measurement (P1/P2/P3, QG176 blind Higgs).
- **SECONDARY TARGET** — a standard catalog value known at derivation time, but
  D96 produces a quantity of the right *class*.
- **POST-HOC TARGET** — entered the register because a formula matched it, with no
  independent D96 pointer (QG239 retro-selection flags, QG250 asserted dictionary).

---

## 2. The Register (29 observables)

| Category | Count | Natural | Secondary | Post-hoc |
|----------|-------|---------|-----------|----------|
| Masses | 10 | 7 (family, blind Higgs ×2, P1/P2/P3) | 3 | 0 |
| Couplings | 7 | 1 (θ_QCD = 0) | 5 | 1 (1/α_em) |
| Mixings | 3 | 0 | 3 | 0 |
| Cosmology | 4 | 0 | 2 (Λ, Ω) | 2 (n_s, peaks) |
| GR | 5 | 0 | 5 | 0 |
| **Total** | **29** | **7** | **19** | **3** |

**Risk score** = (0.5·19 + 1.0·3)/29 = **0.431 → MEDIUM**.

---

## 3. The Findings

**The register is predominantly CATALOG-DRIVEN.** 19/29 observables are SECONDARY:
D96 produces the right *class* of quantity (spectral ratio, gauge degree, octave
hierarchy), but the specific target was selected from the measured catalog with its
value known at derivation. This is the expected situation for any theory that
reproduces the measured SM/GR/cosmology register.

**A genuine NATURAL core exists and is temporally independent.** 7/29: the family
count (exact structural identity), θ_QCD = 0 (exact automorphism result), the
blind Higgs reconstruction (QG176, target hidden), and the three PRE-REGISTERED
predictions (P1, P2, P3 — frozen before measurement). These are D96's own outputs,
not catalog picks.

**A small POST-HOC minority is explicitly flagged.** 3/29: n_s and the acoustic
peaks (QG239 RETRO-SELECTION RISK) and the 1/α_em = 137 dictionary (QG250 MAJOR
attack). These are the observables where the selection is cleanest to criticize.

**The honest Bekenstein failure is anti-retro evidence.** QG185/196 proved D96
CANNOT produce S = A/4 without importing π — a catalog target that was *not*
matched. A pure retro-fitting program would match every catalog item; the fact that
one important GR observable provably fails shows the selection is not total fitting.

---

## 4. Conclusion

### **MEDIUM observable-selection risk**

Observable selection was substantially **target-informed** (the register mirrors
the measured catalog; 19/29 SECONDARY), but:
- the structural core (7/29) is genuinely D96-natural and temporally independent;
- the post-hoc minority is small and already flagged (n_s, peaks, 1/α_em);
- the honest Bekenstein impossibility demonstrates the selection is not pure
  retro-fitting.

Consistent with QG252 (MEDIUM independent evidence) and QG253 (formula-level
target-information): the *observables* are mostly catalog-driven but the D96
structural classes they belong to are genuine — the risk is concentrated in the
specific numerical choices of n_s, the acoustic peaks, and the 1/α_em dictionary.
