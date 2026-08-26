# AT-QG Phase 148 — Independent Validation of the Exponent Law

**Status:** COMPLETED — CLASSIFICATION: **OVERFIT**
**Tests:** ATQG1480, ATQG1481, ATQG1482 (3/3 pass)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG147 constructed the linear law p = 6.760 − 1.473·Q + 4.706·T3 by fitting the lepton,
up, and down sectors. This phase asks: **does the law correctly predict fermion sectors
that were NOT used to construct it?**

## Starting Point

- QG147: EXPONENT ORIGIN — p = 6.760 − 1.473·Q + 4.706·T3 reproduces the training
  sectors exactly.

## Method

The law is a 3-parameter model fitted to 3 points (lepton, up, down). Independent
validation:

1. **Neutrino sector** — the only fully unseen fermion sector (Q=0, T3=+1/2).
2. **Unseen sector predictions** — the neutrino is the out-of-sample test.
3. **Leave-one-out validation** — refit 2-parameter reduced models (p = p0 + k·T3,
   p = p0 + k·Q) on two sectors and predict the held-out third.
4. **Overfitting check** — 3 params for 3 points is saturated interpolation.
5. **Predictive accuracy** — the out-of-sample deviations.

All probes are deterministic.

## Assumptions

1. The neutrino is the valid out-of-sample test (not used in the QG147 fit).
2. A predictive law must generalize: small neutrino deviation AND small LOO deviations.
3. A saturated fit (3 params, 3 points) with poor generalization indicates overfitting.

## Results

### 1. Neutrino sector (ATQG1480)

```
law: p = 6.760 − 1.473·Q + 4.706·T3
neutrino (Q=0, T3=+1/2):
  predicted exponent = 9.113
  observed exponent (ν3/ν1 = 500) = 4.483
  relative deviation = 103.3%
```

- The **unseen neutrino prediction fails** (103% deviation).

### 2. Leave-one-out validation (ATQG1481)

T3-only reduced model (p = p0 + k·T3):

```
held-out leptons: 16.7%
held-out up:      27.7%
held-out down:    20.1%
mean = 21.5%
```

Q-only reduced model (p = p0 + k·Q):

```
held-out leptons: 53.4%
held-out up:      57.9%
held-out down:    38.4%
mean = 49.9%
```

- The T3 model generalizes partially (~21%); the Q-only model is worse (~50%).

### 3. Overfitting check (ATQG1482)

- The 3-parameter law is a **saturated fit** (3 params, 3 points — exact interpolation).
- Overall deviation (neutrino + best LOO) = **0.624**.

## Conclusions

1. The law reproduces its training sectors exactly (as expected for a saturated fit).
2. The T3-only reduced model generalizes partially (~21% LOO mean).
3. **The unseen neutrino sector is NOT predicted** (103% deviation).

## Classification: **OVERFIT**

- **PARTIAL VALIDATION rejected**: the unseen neutrino prediction fails badly (103%).
- **OVERFIT accepted**: the 3-parameter law reproduces its training sectors exactly
  (saturated interpolation) but does NOT predict the unseen neutrino sector.

## Connection to the AT research arc

- QG147 EXPONENT ORIGIN → QG148 tempers it: the law interpolates the training sectors
  but does not generalize to the neutrino — it is an overfit, not yet a predictive law.
- The neutrino failure is the key constraint: a valid exponent law must also predict the
  neutrino exponent (≈4.48), which the QG147 law does not.
- The T3-only reduced model's partial success (~21% LOO) suggests isospin carries the
  main signal, but the full 3-parameter law is over-parameterized for 3 points.
- Open: can a law with fewer parameters (or a spectral origin) predict the neutrino
  exponent?
