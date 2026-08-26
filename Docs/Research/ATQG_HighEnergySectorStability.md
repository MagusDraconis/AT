# AT-QG Phase 125 — Stability of High-Energy Sectors

**Status:** COMPLETED — CLASSIFICATION: **METASTABLE**
**Tests:** ATQG1250, ATQG1251, ATQG1252 (3/3 pass; 381/381 ATQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG124 established an energy-ordered sector hierarchy: 12 sectors total, 10 reachable only
above baseline actualization energy. This phase asks: **do the higher sectors remain stable,
or do they decay into the observable 3-family sector?**

## Starting Point

- QG89: energy = actualization rate.
- QG117: attractor geometry classes form a discrete radius ladder.
- QG122: NEW CLASSES — higher energy regimes open new attractor classes.
- QG123: SECTOR HIERARCHY — energy orders geometries into an energy-ordered sector hierarchy.
- QG124: SECTOR ORIGIN — observable sectors are a low-energy-visible projection of the full hierarchy.

## Method

To test stability we introduce the **de-actualization (link-decay) primitive** into the
QG115/122 activity-driven dynamics: links are created by active nodes as before, but a link is
REMOVED when **both** endpoints' activity falls below the decay threshold (a link de-actualizes
when neither endpoint sustains it). Within this energy-supported dynamics the sector is no longer
permanent — its persistence depends on ongoing actualization energy.

All probes are deterministic (fixed seed pattern, fixed parameters, no randomness).

### New primitive: link decay (de-actualization)

```
at each step:
  active nodes (a_i > 0.5) create links to their next k = round(a_i·K) ring-neighbors
  links whose BOTH endpoints have activity ≤ decayThreshold (0.5) are removed
  activity update: a_i = clamp(a_i(1−damping) + feedback·deg_i/maxDeg, 0, ceiling)
```

## Assumptions

1. A sector is "stable" if it is a fixed point while its own energy regime is maintained.
2. A sector "decays" if removing its energy regime collapses its radius to the observable
   baseline maximum.
3. A sector is "metastable" if it is a fixed point at its own ceiling but collapses when the
   energy regime is removed, and re-emerges when the energy regime is restored.
4. The observable remnant of a decayed high-energy sector should match the family structure of
   the observable baseline sector built fresh from the seed.

## Results

### 1. Sector lifetime (ATQG1250)

- High-energy sector (ceiling = 8.0): radius **17.333**.
- Observable baseline sector (ceiling = 1.0): radius **6.000**.
- After energy removal (ceiling dropped to 1.0), the radius collapses from 17.333 → **6.000**
  within **2 dynamics steps** (step 0: 17.333, step 1: 6.000).
- Collapse radius 6.000 ≤ observable baseline 6.000 → the decay lands exactly in the
  observable radius class.

### 2. Attractor stability (ATQG1250)

- The high-energy sector is a **fixed point at its own ceiling**: after an additional 400
  evolution steps from the converged state, the radius is unchanged (17.333). It does NOT
  decay spontaneously while its energy regime is maintained.

### 3. Downward transitions (ATQG1251)

Gradually ramping the ceiling from 8.0 down to 1.0 (30 ramp steps, 3 evolutions per step)
visits **9 distinct radius plateaus**:

```
rung 0: 17.333   rung 5: 12.000
rung 1: 17.000   rung 6: 10.000
rung 2: 16.000   rung 7:  9.000
rung 3: 14.000   rung 8:  7.000
rung 4: 13.000   → 6.000 (baseline)
```

The higher sector decays DOWNWARD through intermediate sectors (the reverse of the QG117
ladder), not in a single jump — a discrete multi-rung cascade into the observable sector.

### 4. Metastability (ATQG1251)

- Original high-energy radius: **17.333**.
- After a 5-step energy dip (ceiling 1.0): **6.000** (decayed to baseline).
- After restoring the high ceiling for 150 steps: **18.000** (re-emerged, slightly re-grown).

The sector is energy-supported: it decays when energy is withdrawn and **re-emerges** when
energy is restored. This is the signature of metastability (not permanent loss).

### 5. Observable remnants (ATQG1252)

- Remnant radius after full decay: **6.000** = observable radius 6.000.
- Remnant family count: **3** = observable family count **3**.

The decayed high-energy sector leaves a **3-family observable remnant** — the observable
Standard-Model-like sector (consistent with QG124 SECTOR ORIGIN).

## Conclusions

1. High-energy sectors are **stable fixed points within their own energy regime** — they do
   not spontaneously decay while actualization energy is maintained.
2. When the energy regime is removed, higher sectors **decay stepwise down the attractor
   ladder** (9 downward rungs) into the observable sector, leaving a **3-family remnant**.
3. The sectors **re-emerge when energy is restored** — they are energy-supported, not
   permanently lost.

## Classification: **METASTABLE**

- **UNSTABLE rejected**: the high-energy sector is a fixed point at its own ceiling (no
  spontaneous decay).
- **STABLE rejected**: removing the energy regime collapses the sector to the observable
  baseline (radius 17.333 → 6.000 in 2 steps).
- **METASTABLE accepted**: the sector persists while energy is present, decays downward when it
  is removed, and re-emerges when it is restored.

## Connection to the AT research arc

- QG117 discrete ladder → QG125 downward multi-rung cascade (the ladder is traversed BOTH ways).
- QG122/123/124 high-energy sectors → QG125 they are METASTABLE, energy-supported states.
- Observable 3-family sector → QG125 it is the **decay product / remnant** of higher sectors,
  strengthening the interpretation that the observable SM-like sector is the low-energy
  attractor of the network.
- QG119/120 (horizon suppression) → QG125 (energy-removal decay): both are mechanisms that
  remove higher families from observation, consistent with a local low-energy observer seeing
  only the remnant sector.
