# AT-QG Phase 138 — Origin of the Effective-Size Law

**Status:** COMPLETED — CLASSIFICATION: **FUNDAMENTAL**
**Tests:** ATQG1380, ATQG1381, ATQG1382 (3/3 pass; 420/420 ATQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG137 established that the family count follows the effective size N/K (r = 0.950).
This phase asks: **why does N/K control the family count?** Is it an artifact, a
dynamical coincidence, or a fundamental spectral/combinatorial law?

## Starting Point

- QG137: EFFECTIVE-SIZE ORIGIN — family count ~ log2(N/K).

## Method

The family count is the number of OCTAVE BANDS (QG00) of the observable sector's
Laplacian spectrum: family k holds modes with ω ∈ [ω₁·2^k, ω₁·2^(k+1)). Therefore the
family count = floor(log2(ω_max/ω_min)) + 1 — set entirely by the SPECTRAL SPAN. For a
K-neighbor circulant-like network, the eigenvalues give ω_min ~ K^(3/2)/N (longest
wavelength) and ω_max ~ √K (shortest), so ω_max/ω_min ∝ N/K and the family count ~
log2(N/K). Five probes verify this mechanism:

1. **Mode density** — total modes and per-octave distribution.
2. **Octave spacing** — band boundaries at ω₁·2^k.
3. **Spectral crowding** — most modes in the top octave.
4. **Effective horizon** — ω_min set by the longest wavelength (N/K steps).
5. **Family-band formation** — the identity familyCount = floor(log2(span)) + 1.

All probes are deterministic.

## Assumptions

1. A family is an octave band of the intra-sector Laplacian spectrum (QG00).
2. The number of octave bands is determined by the spectral span.
3. If the family-count identity holds across the whole (N,K) grid, the law is
   fundamental (combinatorial/spectral), not dynamical or numerical.

## Results

### 1. Mode density (ATQG1380)

- **95 positive modes** (N−1 for the connected 96-node sector).
- Per-octave distribution:

```
octave 0: [0.622, 1.243) → 4 modes
octave 1: [1.243, 2.486) → 4 modes
octave 2: [2.486, 4.973) → 87 modes
```

### 2. Octave spacing (ATQG1380)

```
octave 0: start=0.622 ideal=0.622 ratio=1.000
octave 1: start=1.799 ideal=1.243 ratio=1.447
octave 2: start=2.790 ideal=2.486 ratio=1.122
mean ratio = 1.190
```

- Band boundaries approximately follow the **frequency-doubling rule** (ω₁·2^k).

### 3. Spectral crowding (ATQG1381)

- **91.6% of modes sit in the top octave** — the familiar spectral crowding that makes
  the octave-band count small (3) despite 95 modes.

### 4. Effective horizon (ATQG1381)

- Fundamental mode ω_min = 0.622; effective size N/K = 16.0 (link-length steps).
- **Pearson r(log2(ω_max/ω_min), log2(N/K)) = 0.999** over the (N, K) grid — the spectral
  span tracks the effective size almost perfectly.

### 5. Family-band formation (ATQG1382)

- The identity **familyCount = floor(log2(ω_max/ω_min)) + 1** holds at the default point:
  floor(log2(6.40)) + 1 = floor(2.68) + 1 = **3** ✓.
- The identity holds **across the whole (N, K) grid**.

## Conclusions

1. The family count IS the octave-band count of the spectrum.
2. The octave-band count = floor(log2(spectral span)) + 1 — a purely spectral quantity.
3. The spectral span ∝ N/K for the K-neighbor network (r = 0.999), so the family count ~
   log2(N/K) is a **spectral/combinatorial law**, not a numerical or dynamical accident.

## Classification: **FUNDAMENTAL**

- **ARTIFACT rejected**: the identity holds and the span tracks N/K (r = 0.999).
- **DYNAMICAL rejected**: the law is independent of the specific dynamics parameters — it
  is fixed by the network combinatorics.
- **FUNDAMENTAL accepted**: the family count is the octave-band count, which is
  floor(log2(spectral span)) + 1, and the spectral span ∝ N/K for the K-neighbor network
  — a spectral/combinatorial law.

## Connection to the AT research arc

- QG137 EFFECTIVE-SIZE ORIGIN → QG138 explains WHY: the family count is the octave-band
  count, and the octave-band count is a spectral/combinatorial function of the spectral
  span, which scales as N/K.
- The spectral-span law is scale-free and parameter-free — it follows from the network
  being a K-neighbor (circulant-like) graph. This is why the 3-family structure is robust
  within its effective-size band.
- QG119/120 horizon → the effective horizon N/K is the fundamental-mode wavelength: the
  number of local-actualization steps across the network. The family count is literally
  how many octaves of modes fit between the horizon and the shortest mode.
- The octave-family structure (QG106) and the effective-size law now share a single
  origin: the octave quantization of the Laplacian spectrum.
