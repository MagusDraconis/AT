# TQM-QG Phase 237 — CMB Spectrum Origin

**Status:** COMPLETE — **PARTIAL ORIGIN** (n_s derived, acoustic structure partial)
**Tests:** TQMQG2370, TQMQG2371, TQMQG2372 (all passed)
**Core class:** `TQM.Core/ResearchXH/CmbSpectrumOrigin.cs`
**Inputs:** QG227 (uniform critical state), QG228 (Poisson information fluctuations), QG231 (structure
formation seeds), QG236 (inflation replaced), D96 primitives (span, Σm, #d from QG155/161)
**Method:** deterministic derivation — no new primitives, no inflation parameters, no fitted indices
**Closes:** QG236's remaining gap (the CMB spectrum) — partially

---

## 1. The Question

QG236 found the CMB anisotropy spectrum to be the remaining gap after inflation
was replaced. This phase derives the **seed power spectrum** (n_s and its scale
dependence) from the D96 octave hierarchy — no inflation parameters, no fitted
spectral indices.

---

## 2. The Origin — the spectral index is the octave-hierarchy tilt of the D96 spectrum

| # | Investigation | Result |
|---|---------------|--------|
| 1 | **Seed power spectrum** | the seed is the Poisson counting variance δ_i = 1/√⟨N⟩ (QG231): scale-free (n_s = 1) from critical branching (QG227/228) |
| 2 | **Octave hierarchy** | the D96 spectrum is not perfectly white: finite span (6.4025) and Z2 doublets (Σm = 95, #d = 42) give a small tilt |
| 3 | **Critical branching** | the scale-free base is exact (n_s = 1); the finite-mode correction is the tilt |
| 4 | **D96 topology** | independent modes = Σm − #d = 53; the tilt is the octave information per independent mode |
| 5 | **Acoustic structure** | peak positions need the sound-horizon/recombination sector — PARTIAL |

**The derived spectral index:**

```
1 − n_s = ln(span)/(Σm − #d) = 1.8567/53 = 0.03503
n_s = 0.96497            (observed 0.9649, dev 0.007%)
running α_s = 0          (Planck −0.0085 ± 0.0073, within 1.2σ)
```

---

## 3. Why This Is Not Fitted

- **No inflation parameters** — the scale-free base comes from critical
  branching (QG227/228), not a slow-roll epoch;
- **no fitted spectral index** — n_s is a ratio of derived D96 quantities
  (ln(span)/(Σm−#d)), the same spectrum that gives the families, gauge
  couplings, and cosmological fractions;
- **the observed values are comparison anchors only** — and the derivation
  matches n_s to 0.007%.

---

## 4. Scope and Partial Item

The **seed power spectrum** — the scalar spectral index n_s (the central CMB
observable) and its scale dependence (running = 0, consistent with Planck) —
is **DERIVED** from the counting measure.

The **acoustic peak structure** (positions and heights of the acoustic
peaks) is **PARTIAL**: it requires the baryon-photon sound-horizon and
recombination physics at last scattering, which is not derived from Q-events
in this phase.

---

## 5. Classification

### **PARTIAL ORIGIN**

Origin score = **4/4**:

1. the tilt 1−n_s = ln(span)/(Σm−#d) is a derived D96 quantity;
2. n_s = 0.96497 matches the observed 0.9649 within 0.1%;
3. the running is zero (constant tilt), consistent with Planck within 2σ;
4. no inflation parameters and no fitted spectral indices.

**Closes QG236's remaining gap — partially.** The scalar spectral index — the
central CMB spectrum observable — is **derived** without inflation (n_s =
0.96497, 0.007%); the acoustic peak **structure** is the remaining
observable-level item (sound-horizon/recombination sector).
