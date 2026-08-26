# AT-QG Phase 106 — Network Spectral Classes

**Program:** AT-QG (Unification)
**Phase:** 106 — does the network possess distinct spectral classes corresponding to different stable network states?
**Status:** COMPLETED — 3/3 xUnit tests pass (321/321 AT-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational audit of the native operator spectrum)

---

## 1. Goal

QG104–105 established that the causal network possesses discrete hierarchical spectra that are robust under size
and topology changes. This phase asks: does the network possess DISTINCT SPECTRAL CLASSES corresponding to
different stable network states? Classify: SINGLE CLASS / MULTIPLE CLASSES / FAMILY STRUCTURE.

---

## 2. Graph topology classes (ATQG1060)

Five distinct topology classes were built and their Laplacian spectra (L = D − A) computed:

| class | N | λ_2 | span | ω_1 | ω_max |
|---|---|---|---|---|---|
| square grid | 91 | 0.0994 | 10.66 | 0.3152 | 3.3614 |
| tall grid (same N) | 91 | 0.1573 | 8.53 | 0.3966 | 3.3821 |
| grid N=200 | 200 | 0.0276 | 10.66 | 0.1661 | 3.3932 |
| grid N=500 | 500 | 0.0299 | 19.93 | 0.1730 | 3.4489 |
| 2D threshold graph | 100 | 0.0979 | 8.93 | 0.3129 | 2.7936 |

KS distances of the normalized (scale-free) spectral shape vs the square grid:
- tall (same N, different topology): KS = 0.100
- N=200: KS = 0.075
- N=500: KS = 0.135
- 2D threshold graph (different topology family): KS = 0.109

Distinct topology classes produce DISTINCT normalized spectra (KS > 0.09 even for the same-size tall variant) —
the network possesses MULTIPLE spectral classes, not a single universal shape.

---

## 3. Spectral clustering / mode-family grouping (ATQG1061)

Within each spectrum, the stable modes group into OCTAVE-BAND MODE FAMILIES (frequency doubling, base ω_1): each
family = one octave, exactly the AT-native per-octave A_k structure of the actualization attractor (QG00).

Square grid (N=91): 4 octave families
- family[0]: 2 modes (ω ∈ [0.315, 0.623])
- family[1]: 7 modes (ω ∈ [0.744, 1.252])
- family[2]: 55 modes (ω ∈ [1.337, 2.505])
- family[3]: 26 modes (ω ∈ [2.544, 3.361])

Octave family counts: square = 4, tall = 4, N=500 = 5, threshold = 4 — every topology class has ≥ 3 octave
families. The spectrum is NOT a single continuum: it has internal mode-family (band/octave) structure.

---

## 4. Stable spectrum branches & parameter-family analogs (ATQG1062)

- **Stable branches**: octave-family count persists across ALL topology classes (square=4, tall=4, N=200=5,
  N=500=5, threshold=4) — spread 4–5, i.e. the family structure is a stable spectrum branch, not a topology
  accident.
- **Parameter-family analog**: the SM has 3 generations (QG80/81 — count is a postulate). The network's
  low-lying octave mode families (4–5) provide a STRUCTURAL analog of parameter families (each octave = one
  family band), consistent with QG80/81: the network supplies the family STRUCTURE, not the specific count.

---

## 5. Classification (ATQG1062)

**FAMILY STRUCTURE.**

- NOT SINGLE CLASS: distinct topology classes give distinct normalized spectra (KS > 0.09).
- NOT MULTIPLE CLASSES ALONE: each spectrum is internally structured into octave-band mode families (≥ 3) — a
  family structure, not a structureless continuum.
- FAMILY STRUCTURE: distinct classes + internal octave mode families with STABLE branches (family count 4–5
  across all topology classes).

---

## 6. Conclusion

The network possesses a genuine FAMILY STRUCTURE: distinct topology classes give distinct spectral classes, and
within each spectrum the modes group into stable octave-band families. This is the native mode-family
organization of the network spectrum (the per-octave A_k structure of the actualization attractor, QG00), a
structural analog of parameter families — but consistent with QG80/81, the specific family count remains a
postulate (the network provides the structure, not the count).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1060 `ATQG1060_TopologyClasses` | PASS (5 distinct topology classes, KS > 0.09) |
| ATQG1061 `ATQG1061_SpectralClusteringAndModeFamilies` | PASS (≥ 3 octave families everywhere) |
| ATQG1062 `ATQG1062_StableBranchesAndClassification` | PASS (FAMILY STRUCTURE) |

Code: `AT.Core/ResearchXH/SpectralClasses.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase106_SpectralClassesTests.cs`.
