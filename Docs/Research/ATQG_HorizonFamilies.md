# AT-QG Phase 120 — Horizon Suppression of Families

**Program:** AT-QG (Unification)
**Phase:** 120 — does a finite horizon naturally suppress higher-family modes?
**Status:** COMPLETED — 3/3 xUnit tests pass (366/366 AT-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational analysis of the QG119 horizon suppression)

---

## 1. Goal

QG119 showed local observers see FEWER octave families than exist globally (a fixed horizon-24 window
saturates at 2 families while the total grows to 4 at N=192). This phase asks: does a FINITE HORIZON NATURALLY
suppress higher-family modes? Classify: NO SUPPRESSION / PARTIAL SUPPRESSION / HORIZON ORIGIN.

Method: for a fixed global converged network (N=192, radius-2 class), extract local window patches (induced
subgraphs) at a grid of horizon sizes 8–192 and measure observable octave-family count, mean IPR (inverse
participation ratio) of the global eigenmodes per octave family, family visibility, the suppression profile
(total − visible), and whether the observable count follows a clean monotone spectral-resolution law.

---

## 2. Horizon size + family visibility (ATQG1200)

Observable families vs horizon (global N=192, total = 4):
- h=8 → 1; h=12 → 1; h=16 → 2; h=24 → 2; h=32 → 3; h=48 → 3; h=64 → 4; h=96 → 4; h=128 → 5; h=192 → 4.

A smaller horizon genuinely sees FEWER families — the observable family count is suppressed at small scales
(1 family at h=8 vs 4 at h=64). The finite horizon limits family visibility.

---

## 3. Mode localization + spectral suppression (ATQG1201)

Mode localization (mean IPR of global eigenmodes per octave family):
- family 1: 0.0072; family 2: 0.0078; family 3: 0.0078; family 4: 0.0080 (all ≈ 1/N = 1/192 ≈ 0.005).

Suppression profile (total = 4):
- h=8: visible 1, suppressed 3; h=16: 2/2; h=32: 3/1; h=64: 4/0; h=96: 4/0; h=128: 5/−1; h=192: 4/0.

All family modes are DELOCALIZED (IPR ≈ 1/N — plane-wave modes on the ring), so suppression is NOT a
localization effect — it is SPECTRAL: the window truncates the resolvable frequency range. But the suppression
profile is NOT perfectly monotone: the open-path window boundary can ADD spectral span (the h=128 patch shows
5 families, exceeding the closed total 4) — so suppression is partial.

---

## 4. Observable count + classification (ATQG1202)

- observable count grows (monotone) with horizon: False (5 at h=128 breaks monotonicity);
- observable count saturates to total at full horizon: True;
- suppression is strictly monotone: False.

**PARTIAL SUPPRESSION.**

- NOT NO SUPPRESSION: a smaller horizon genuinely sees fewer families (1 at h=8 vs 4 at h=64) — the finite
  horizon suppresses higher-family modes.
- NOT HORIZON ORIGIN: the suppression is NOT a clean monotone function of the horizon — the open-path window
  boundary adds its own spectral span (h=128 patch shows 5 families, exceeding the closed total 4), so the
  observable count does not follow a pure spectral-resolution law.
- PARTIAL SUPPRESSION: a finite horizon DOES suppress higher families at small scales, but the window-boundary
  structure perturbs the count — suppression exists but is not perfectly systematic in the horizon size.

---

## 5. Conclusion

A finite horizon DOES suppress higher-family modes at small scales: the observable octave-family count grows
from 1 (h=8) to the full 4 (h=64) as the window widens, and this suppression is SPECTRAL, not spatial — all
ring eigenmodes are delocalized plane waves (IPR ≈ 1/N), so the mechanism is simply that a small window
cannot resolve the full frequency range. However, the suppression is only PARTIAL in the strict sense: the
open-path window boundary of an induced subgraph adds its own spectral span, so an intermediate window
(h=128) can transiently resolve MORE families (5) than the closed global network (4). The horizon suppresses
higher families qualitatively, but the exact observable count is window-structure dependent, not a pure
function of horizon size.

This qualifies QG119 (LOCAL SUBSET): local observers genuinely see fewer families, and the mechanism is the
finite spectral window — but the boundary structure of the sampled patch perturbs the count, so the
suppression is not a clean HORIZON ORIGIN law. The observable family spectrum is a suppressed, partially
window-dependent subset of the global one.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1200 `ATQG1200_HorizonSizeAndFamilyVisibility` | PASS (1 at h=8 vs 4 at h=64; small horizon < total) |
| ATQG1201 `ATQG1201_ModeLocalizationAndSpectralSuppression` | PASS (IPR ≈ 1/N delocalized; suppression real, not monotone) |
| ATQG1202 `ATQG1202_ObservableCountAndClassification` | PASS (PARTIAL SUPPRESSION; suppression real but not monotone) |

Code: `AT.Core/ResearchXH/HorizonFamilies.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase120_HorizonFamiliesTests.cs`.
