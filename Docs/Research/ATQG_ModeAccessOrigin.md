# AT-QG Phase 150 — Origin of Mode Access

**Status:** COMPLETED — CLASSIFICATION: **MODE-ACCESS ORIGIN**
**Tests:** ATQG1500, ATQG1501, ATQG1502 (3/3 pass)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG149 established that sector exponents emerge from occupation-weighted mode access. This
phase asks: **why do different particle sectors access different parts of the SAME
spectrum?**

## Starting Point

- QG149: PHYSICAL ORIGIN — down p_eff = 2×Weyl; up/down splitting = spectral access.

## Method

The observable sector's spectrum splits into octave bands with distinct occupancies and
local Weyl exponents. Each sector's effective dimension δ_eff = p_eff/2 implies a spectral
accessibility. Five probes:

1. **Mode-selection rules** — the octave band structure a sector can select.
2. **Charge constraints** — correlation of δ_eff with electric charge.
3. **Isospin constraints** — correlation with weak isospin.
4. **Spectral accessibility** — the fraction of the spectrum each sector accesses.
5. **Occupation mechanisms** — down = full spectrum, up = dense band.

All probes are deterministic.

## Assumptions

1. A sector's effective dimension δ_eff = p_eff/2 determines which spectral region it
   couples to.
2. δ_eff ≈ full Weyl ⇒ full-spectrum access; δ_eff ≫ full Weyl ⇒ dense-band access.

## Results

### 1. Mode-selection rules (ATQG1500)

```
band 0: occupancy=4,  local Weyl δ=1.318
band 1: occupancy=4,  local Weyl δ=3.496
band 2: occupancy=87, local Weyl δ=14.171
```

- The spectrum offers distinct bands with very different occupancies — the available
  mode-selection rules.
- Top-band occupancy fraction = **0.916**.

### 2. Charge constraints (ATQG1500)

- Pearson r(δ_eff, Q) = **0.759**.

### 3. Isospin constraints (ATQG1500)

- Pearson r(δ_eff, T3) = **0.955** — the mode access is strongly isospin-constrained.

### 4. Spectral accessibility (ATQG1501)

- Full-spectrum Weyl δ = **2.473**.
- **Down δ_eff = 2.449 ≈ full Weyl (deviation 0.96%)** — the down sector accesses the
  FULL spectrum.

### 5. Occupation mechanisms (ATQG1502)

- Up δ_eff = 4.066; up/full ratio = **1.644**.
- **The up sector accesses the DENSE top band** (True).

## Conclusions

1. The spectrum offers distinct bands with very different occupancies.
2. The down sector's effective dimension matches the full-spectrum Weyl — full-spectrum
   access.
3. The up sector's dimension exceeds the full Weyl — dense-band access.
4. The mode access is **strongly isospin-constrained** (r = 0.955).

## Classification: **MODE-ACCESS ORIGIN**

- **NO ORIGIN rejected**: clear spectral-access mechanisms exist.
- **PARTIAL ORIGIN rejected**: all five conditions hold (score 5/5).
- **MODE-ACCESS ORIGIN accepted**: sectors access different parts of the same spectrum
  because occupation-weighted mode access is quantum-number constrained — down accesses
  the full spectrum, up the dense band, selected by isospin (r≈0.96).

## Connection to the AT research arc

- QG149 PHYSICAL ORIGIN → QG150 explains WHY the mode access differs: it is
  isospin-constrained occupation-weighted access.
- The spectrum's band structure (sparse low bands vs dense top band) is the physical
  substrate; the sector quantum numbers select which part is accessed.
- Down = full-spectrum access (δ_eff ≈ Weyl) and up = dense-band access (δ_eff ≈ 1.64×
  Weyl) are the two mechanisms.
- The isospin constraint (r = 0.955) ties mode access to the weak-interaction structure,
  consistent with QG145's up-sector enhancement.
