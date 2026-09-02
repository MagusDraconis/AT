# ResearchY-NP_028 — Blackbody Reconstruction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_028 (permanent)
**Title:** Blackbody Reconstruction Audit
**Status:** COMPLETE
**Date:** 2026-09-02
**File:** `NP_NewPhysics/ResearchY-NP_028.md`
**Depends on:** ResearchY-NP_027 (Planck form/law), QG_194 (geometric occupation),
QG_228 (information), D_041 (spectrum), D_030 (octave structure), NP_024/025/026
(degeneracy structure), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_028_Tests.cs`

---

## Purpose

NP_027 established that the Planck FACTOR FORM n(x) = 1/(e^x − 1) emerges per-mode from
the D96 geometric occupation, but the FULL Planck law (T⁴, Wien displacement, continuous
DOS) does NOT emerge from the finite discrete 95-mode spectrum. NP_028 asks the coarse-
graining question: **after binning the 95 discrete D96 modes into a smooth spectral
density and weighting by occupancy, can the resulting coarse-grained spectrum reproduce
the OBSERVED Planck/blackbody curve?**

This hostile audit tests whether coarse-graining (the standard way a discrete resonator
spectrum becomes a continuum) heals the NP_027 gaps.

---

## 1. The mode inventory (D96, C_96(±1..±6))

The 95 positive modes ω_k = √λ_k, k = 1..95, span the band [0.622, 3.980]
(span ratio 6.40, the D96 span). The mode count is NOT a smooth power law:

| Quantity | Value | Blackbody (3D cavity) needs |
|---|---|---|
| positive mode slots | 95 | ∞ (continuum) |
| distinct frequencies | 44 | ∞ |
| min ω | 0.622 | 0 (Rayleigh-Jeans continuum) |
| max ω | 3.980 | ∞ (Wien exponential tail) |
| cumulative N(<2.5)/N(<1.0) | 8/2 = 4.0 | (2.5/1)³ = 15.6 |
| cumulative N(<3.0)/N(<1.5) | 10/4 = 2.5 | (3/1.5)³ = 8 |
| fraction above band mid (2.30) | **0.937** | smooth unimodal peak at x=2.82 |
| fraction in top 20% of band (≥3.31) | **0.874** | ~0.23 of in-band energy (θ=1) |

**The D96 spectrum is top-heavy and truncated:** 87% of the modes sit in the top 20% of
the band. This is the opposite of a blackbody, whose spectral density rises smoothly
(∝ω²) to a peak at x = 2.82 then decays exponentially.

## 2. Occupancy-weighted spectral density (coarse-grained)

Weight each mode by the geometric-occupation Planck factor n(ω) = 1/(e^(ω/θ) − 1)
(θ = temperature-like scale, per-mode NP_027 form; θ is a BOUNDARY input, not a
canonical primitive). Coarse-grain into bins. At θ = 1:

| Band region | D96 weighted energy share | Planck in-band share |
|---|---|---|
| above ω = 3.3 (top of band) | **0.657** | 0.232 |

**The occupancy-weighted D96 spectrum is still top-heavy (0.657 of energy in the top
cluster) while the Planck curve concentrates its energy below the peak.** The weight
does not flatten the top-heavy DOS because the mode density is so concentrated that the
occupancy suppression (which needs many octaves to bite) barely acts within the narrow
top cluster.

## 3. The high-frequency falloff

The observed Planck curve has a Wien exponential tail n → e^(−x) as x → ∞ — smooth,
supported to arbitrarily high ω. D96 has:

- **No modes above ω_max = 3.98** (hard spectral cutoff, zero density). There is NO
  exponential tail: the "falloff" is a sharp edge at the last mode, not e^(−ω/θ).
- Within the band the density does not decay: counts per 0.1 bin are 0 in [3.0,3.1)
  but 6 in [3.3,3.4) and 6 in [3.9,4.0) — the density RISES into the cutoff.

**Coarse-graining cannot create modes that do not exist.** Binning 95 modes in a finite
band produces a finite, top-heavy, band-truncated spectral density. The Wien tail of the
observed blackbody requires mode support to infinity, which D96 does not have.

## 4. Does coarse-graining heal the NP_027 gaps?

**No.** Three independent obstructions survive coarse-graining:

1. **DOS mismatch.** The blackbody needs a smooth density of states ∝ω² (3D cavity). The
   D96 cumulative count grows as ~ω^1.5 (N(2.5)/N(1)=4 vs 15.6 for ω³), and is lumpy
   (44 distinct frequencies, mirror pairs + 5-fold/6-fold blocks). Binning the 44
   distinct values over the band gives a coarse DOS that is far below ω² at low ω and
   far above it at the top.
2. **Top-heaviness.** 87% of modes (and 66% of occupancy-weighted energy at θ=1) sit in
   the top 20% of the band. A Planck curve at any θ puts its energy around the peak
   x = 2.82 and < 25% above ω = 3.3 (in-band). The D96 curve cannot match this.
3. **Truncation / no Wien tail.** The observed blackbody decays as e^(−ω/θ) to ∞; D96
   ends hard at ω_max = 3.98 with zero modes above. The exponential tail is absent.

**The only CORRESPONDENCE is the per-mode occupation factor itself** (the geometric
count gives n = 1/(e^x − 1), NP_027 DERIVED). That factor multiplies a DOS that is NOT
the blackbody DOS, so the product (the observable spectrum) is NOT Planck.

## Theorem

> **Theorem (NP_028).** Coarse-graining the 95 positive D96 modes into a spectral density
> and weighting by the per-mode occupation factor does NOT reproduce the observed
> Planck/blackbody spectrum. Proof: (1) The D96 cumulative mode count grows sub-power-law
> (~ω^1.5, N(2.5)/N(1.0) = 4.0 vs ω³ = 15.6) with 44 distinct frequencies — not the
> smooth ω² DOS of a 3D cavity (Section 1, verified). (2) The spectrum is top-heavy:
> 93.7% of modes above band mid, 87.4% in the top 20% of the band; occupancy weighting
> (θ = 1) leaves 65.7% of energy above ω = 3.3 vs 23.2% for Planck in-band (Section 2,
> verified). (3) There is a hard spectral cutoff at ω_max = 3.980 with zero modes above —
> no Wien exponential tail exists, and the density rises into the cutoff (Section 3,
> verified). (4) Therefore the coarse-grained product (occupation × DOS) is top-heavy and
> truncated, not the smooth unimodal Planck curve peaked at x = 2.82 with an exponential
> tail. (5) Classification: per-mode occupation factor CORRESPONDENCE (NP_027 form);
> blackbody DOS (ω²) FALSIFIED for D96; Wien high-frequency tail FALSIFIED (hard cutoff);
> full observed blackbody after coarse-graining FALSIFIED. No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Count modes. (2) Coarse-grain + weight. (3) Inspect the falloff.
> (4) Compare to Planck. ∎

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Coarse-graining 95 modes gives the blackbody continuum" | binning cannot change the mode COUNT: 44 distinct freqs in a finite band, DOS ~ω^1.5 top-heavy, not ω² |
| "Occupancy weighting flattens the spectrum" | at θ=1 D96 still has 0.657 of energy above ω=3.3 vs Planck 0.232 — the top cluster dominates |
| "The high-frequency tail is the Wien falloff" | there are no modes above ω_max=3.98; the falloff is a hard edge, not e^(−ω/θ) |
| "Choosing θ removes the mismatch" | θ rescales x but cannot create low-frequency modes or a ω² DOS, nor modes beyond ω_max |
| "The 5-fold/6-fold blocks supply the peak" | those are degenerate lines at ω=3.46/3.74, not a smooth continuum; they make the spectrum MORE line-like, not Planck-like |

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| the blackbody DOS ω² is not D96's | a D96-derived DOS growing as ω² (N(ω) ∝ ω³) over ≥ 1 octave |
| the Wien tail is absent | any mode above ω_max or an exponential decay of the top cluster |
| the coarse-grained spectrum is top-heavy | a D96 weighting whose energy peaks away from the top 20% of the band |
| the observed blackbody is reproduced | the coarse-grained weighted D96 spectrum matching Planck at any θ to <10% per-bin |

## Classification

| Component | Status |
|---|---|
| per-mode occupation factor n = 1/(e^x − 1) | **CORRESPONDENCE** (geometric count; NP_027 DERIVED form) |
| blackbody DOS (ω², 3D cavity) | **FALSIFIED for D96** (sub-power-law ~ω^1.5, lumpy, top-heavy) |
| Wien exponential high-ω tail | **FALSIFIED** (hard cutoff at ω_max = 3.98, zero modes above) |
| full observed blackbody after coarse-graining | **FALSIFIED** (top-heavy + truncated; energy concentrated at band top) |
| temperature scale θ | **BOUNDARY** (not a canonical primitive; cannot rescue the shape) |

**Conclusion:** coarse-graining the D96 mode structure does NOT reproduce the observed
Planck spectrum. The per-mode occupation factor corresponds (NP_027), but the blackbody
shape needs a smooth ω² density of states over an unbounded band — D96 provides neither.
The observed blackbody spectrum is therefore NOT an emergent read of the D96 spectrum:
it is hosted/correspondence content (as is the Wien law's exponential tail). No new
primitive; canonical AT unchanged.

---

## References

- ResearchY-NP_027 (Planck form DERIVED / full law NOT reproduced), QG_194 (geometric
  occupation), D_041 (spectrum), D_030 (octave structure), NP_024/025/026 (D96
  degeneracy structure), S_001 (synthesis).
