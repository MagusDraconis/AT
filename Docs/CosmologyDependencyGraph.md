# Cosmology Dependency Graph (QG-081)

Every inferred cosmological quantity, with each assumption step made explicit.

```
OBSERVED (model-free)                       INFERRED (FLRW-dependent)
──────────────────────                      ──────────────────────────
redshift z ──[assume 1+z = a0/a]──────────► scale factor a(t)
time dilation b=1 ──[consistent w/ γ=a]────► clock rate γ(t)  (= a(t))
angular size θ ──[assume D_A = χ/(1+z)]────► angular diameter distance D_A
flux ──[assume F = L/4πD_L²]───────────────► luminosity distance D_L
                                               │  [assume D_L = (1+z)χ, χ=∫dz/H]
                                               ▼
                                          expansion history H(z)
                                               │  [assume Friedmann eqn]
                                               ▼
                                          Ωm, ΩΛ
                                               │  [ΩΛ ≠ 0]
                                               ▼
                                          "dark energy"
```

## Depth of inference (nested assumptions)
1. redshift → a(t): 1 assumption (FLRW metric).
2. a(t) → H(z): 1 more (H = ȧ/a).
3. H(z) → distances: 1 more (D_L/D_A from χ = ∫dz/H).
4. distances → Ωm, ΩΛ: 1 more (Friedmann equation + functional form).
5. ΩΛ ≠ 0 → "dark energy": interpretation.

The "dark energy" conclusion sits at the deepest level (4 nested FLRW assumptions) and
is therefore the MOST model-dependent accepted conclusion.

## Model-light islands
- Cosmic chronometers: H(z) = −1/(1+z)·dz/dt uses only Δt(galaxy ages) + Δz — no metric.
- Etherington reciprocity D_L = (1+z)²·D_A is geometric and holds in ANY metric theory.
- CMB blackbody T0 and its preservation by redshift are model-light.
