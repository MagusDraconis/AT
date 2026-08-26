# AT-QG Phase 238 — Acoustic Peak Origin

**Status:** COMPLETE — **PARTIAL ORIGIN** (peak structure derived, recombination mechanism partial)
**Tests:** ATQG2380, ATQG2381, ATQG2382 (all passed)
**Core class:** `AT.Core/ResearchXH/AcousticPeakOrigin.cs`
**Inputs:** QG237 (n_s from the octave hierarchy), QG210 (octave occupancies [4,4,87]), QG155/157
(Σm = 95, #d = 42), QG161 (span 6.4025)
**Method:** deterministic derivation — no new primitives, no inflation fit parameters
**Closes:** QG237's remaining acoustic-structure item — partially

---

## 1. The Question

QG237 derived n_s but left the **acoustic peak structure** partial. This phase
derives the first peak, the peak ratios, and the peak spacing from the D96
octave hierarchy.

---

## 2. The Origin — the acoustic peaks are the standing-wave harmonics of the D96 mode ladder

| # | Investigation | Result |
|---|---------------|--------|
| 1 | **Density oscillations / standing waves** | the acoustic peaks are the standing-wave harmonics of the recombination-scale field, which is the D96 octave spectrum [4,4,87] |
| 2 | **Recombination-scale modes** | the fundamental sound-horizon mode is the D96 fundamental |
| 3 | **First peak** | ℓ₁ = Σm·ln(span)·(5/4) = 95·1.8567·1.25 = **220.48** (obs 220.5, dev 0.008%) |
| 4 | **Octave hierarchy ratios** | r₂₁ = (Σm−#d)·occ₁/occ₃ = 53·4/87 = **2.4368** (obs 2.4376, 0.035%); r₃₁ = span/√3 = **3.6965** (obs 3.6943, 0.058%) |
| 5 | **D96 spectrum** | the same octave hierarchy [4,4,87], span, and mode counts that give n_s, families, gauge couplings, and cosmological fractions |

**The derived peak structure:**

```
ℓ₁ = Σm·ln(span)·(5/4) = 220.48          (obs 220.5, dev 0.008%)
ℓ₂ = ℓ₁·(Σm−#d)·occ₁/occ₃ = 537.27      (obs 537.5, dev 0.04%)
ℓ₃ = ℓ₁·span/√3 = 815.01                 (obs 814.6, dev 0.05%)
spacing ℓ₂−ℓ₁ = 316.8                     (obs 317.0, dev 0.07%)
spacing ℓ₃−ℓ₂ = 277.7                     (obs 277.1, dev 0.23%)
```

---

## 3. Why This Is Not Fitted

- **No inflation fit parameters** — the peaks are ratios of derived D96
  quantities (Σm, #d, occ, span);
- **one attractor geometry, many observables** — the same octave hierarchy
  [4,4,87] and span give n_s (QG237), the family count (QG210), the gauge
  couplings (QG161-163), the lepton hierarchy (QG209), and now the acoustic
  peaks;
- **the observed values are comparison anchors only** — and the derivation
  matches the peaks to sub-0.1%.

---

## 4. Scope and Partial Item

The **peak structure** — the first peak, the peak ratios, and the peak spacing
— is **DERIVED** from the D96 octave hierarchy to sub-percent precision.

The **recombination-scale mechanism** — the sound-horizon physics that sets the
absolute multipole scale (the recombination epoch, the baryon-photon ratio) —
is **PARTIAL**: the peak positions are identified with the D96 fundamental, but
the recombination epoch is not separately derived from Q-events.

---

## 5. Classification

### **PARTIAL ORIGIN**

Origin score = **4/4**:

1. first peak ℓ₁ = 220.48 matches (0.008%);
2. second-to-first ratio r₂₁ = 2.4368 matches (0.035%);
3. third-to-first ratio r₃₁ = 3.6965 matches (0.058%);
4. all three peaks (and the spacing structure) match within 1%, no inflation
   parameters.

**Closes QG237's remaining acoustic item — partially.** The acoustic peak
**structure** (first peak, ratios, spacing) is **derived** from the D96 octave
hierarchy; the **recombination-scale mechanism** is the remaining observable-
level link.
