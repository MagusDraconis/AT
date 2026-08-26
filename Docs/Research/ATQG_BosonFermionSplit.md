# AT-QG Phase 134 — Boson-Fermion Calibration Split

**Status:** COMPLETED — CLASSIFICATION: **FUNDAMENTAL SPLIT**
**Tests:** ATQG1340, ATQG1341, ATQG1342 (3/3 pass; 408/408 ATQG verified)
**Type:** COMPUTATIONAL (fully deterministic, reproducible)

---

## Question

QG133 showed the boson anchors (Z, W) calibrate the ladder consistently while the fermion
anchors (H, t) shift it. This phase asks: **why does the attractor ladder calibrate
consistently to bosons but not to fermions?**

## Starting Point

- QG126: SECTOR-PARTICLE MAPPING — the observable sector is the 3-family sector.
- QG129: PARTIAL MAPPING — electroweak ratios reproduced, generation hierarchy not.
- QG133: MODERATE — Z/W anchors agree (0.74%), H/t anchors shift (37–90%).

## Method

Five structural comparisons between bosons and fermions:

1. **Boson sector mapping** — W/Z, H/Z, t/Z ratios vs the ladder radius span.
2. **Fermion sector mapping** — μ/e, τ/e, τ/μ ratios vs the ladder radius span.
3. **Family-index effects** — the observable sector's family count (QG126).
4. **Generation hierarchy gap** — largest lepton ratio / ladder radius span.
5. **Calibration universality** — boson-anchor agreement vs fermion-anchor spread.

All probes are deterministic (documented masses; fixed network parameters).

## Assumptions

1. The ladder radius span (17.333/6 = 2.889) bounds what a single linear radius→mass
   calibration can place on rungs.
2. Bosons are single family-index states per rung; fermions carry a family index
   (3 generations).
3. The observable sector (radius 6) hosts the 3 families (QG126).

## Results

### 1. Boson sector mapping (ATQG1340)

```
ladder radius span = 2.889
W/Z = 0.881  within span: True
H/Z = 1.372  within span: True
t/Z = 1.897  within span: True
```

- All boson ratios lie **within the ladder span** (O(1)-few × Z).
- Bosons are single family-index states → they map cleanly onto ladder rungs.

### 2. Fermion sector mapping (ATQG1340)

```
μ/e  =  206.8   beyond span: True
τ/e  = 3477.2   beyond span: True
τ/μ  =   16.8   beyond span: True
```

- All lepton (generation) ratios lie **far beyond the ladder span**.

### 3. Family-index effects (ATQG1341)

- **Observable sector family count = 3** (a 3-family sector).
- Family-index classes resolved: **3**.
- **Fermion generations are carried by a family index WITHIN the observable sector** —
  not placed on separate ladder rungs. Bosons have no such index.

### 4. Generation hierarchy gap (ATQG1341)

- Largest lepton ratio / ladder radius span = **1203.7** — a genuine hierarchy beyond the
  ladder span by three orders of magnitude.

### 5. Calibration universality (ATQG1342)

- Boson-anchor agreement (Z vs W): **0.74%**.
- Fermion-anchor spread (H vs t): **38.3%**.
- Bosons calibrate universally (agreement ≪ spread): **True**.

## Conclusions

1. **Bosons calibrate because they are single family-index states on ladder rungs** — their
   mass ratios lie within the ladder span.
2. **Fermions do not calibrate because their generations are a family-index structure
   WITHIN the observable sector** — the generation hierarchy (gap factor ~1200) vastly
   exceeds the ladder span.
3. The split is structural, not a parameter choice.

## Classification: **FUNDAMENTAL SPLIT**

- **NO SPLIT rejected**: bosons and fermions map differently by structure.
- **PARTIAL SPLIT rejected**: all five split conditions hold (score 5/5).
- **FUNDAMENTAL SPLIT accepted**: bosons are single family-index states on ladder rungs
  (ratios within span, anchors agree), while fermions are 3-family states whose
  generations are resolved by a family index WITHIN the observable sector (ratios far
  beyond the span, anchors spread).

## Connection to the AT research arc

- QG118/119/120 family arc → QG134: the family index (not the ladder) carries the
  generations — consistent with horizon suppression hiding higher families.
- QG129 PARTIAL MAPPING → QG134 explains WHY it is partial: the ladder resolves bosons
  (single-index) but the generation hierarchy is a within-sector family-index effect.
- QG133 MODERATE → QG134: the ~106 GeV prediction is robust because it anchors on bosons,
  the sector type the ladder genuinely resolves.
- The boson/fermion structural difference is itself a candidate origin of the
  spin-statistics-like distinction: bosons = rung states, fermions = family-index states.
