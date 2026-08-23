# TQM-QG Phase 264 — Projection Family Audit

**Status:** COMPLETE — **SINGLE RESONANCE INVARIANT**
**Tests:** TQMQG2640, TQMQG2641, TQMQG2642 (all passed)
**Core class:** `TQM.Core/ResearchXH/ProjectionFamilyAudit.cs`
**Question:** are the Density and Frequency projections fundamental, or manifestations of a single resonance invariant?
**Method:** structure only — no observables, no formulas, D96 only, deterministic.

---

## 1. The Two Projections (from QG263)

| Projection | Operators | Outputs |
|------------|-----------|---------|
| **Density** | CROWDING ≡ COMPRESSION | multiset [42×2,5,6], occupancies [4,4,87], moments Σm/Σ√m/Σm²/occMom |
| **Frequency** | BEAT ≡ LOCKING | span 6.4025, λ₂ 0.3864 |

---

## 2. The Duality Evidence

### 2.1 Shared origin — one object

Both projections are deterministic functions of the **same** 95-mode frequency
list ω = √λ. The density structure is NOT an independent primitive:
- the multiset [42×2,5,6] is computed from the frequencies by degeneracy counting;
- the occupancies [4,4,87] are computed from the frequencies by octave banding;
- the span and λ₂ are computed from the same list.

**No density quantity exists independently of the frequency list.**

### 2.2 Frequency → Density duality

The number of density bands is **determined by the span**:
```
family count = floor(log2(span))+1 = 3 = octave band count
log2(span) = 2.6786  →  3 bands
```
The frequency projection fixes how many density bands exist; the occupancy
[4,4,87] is the mode count per such band.

### 2.3 Density ↔ Frequency duality (unified exponent law)

The unified spectral access law (QG156/157) pairs each **density** moment with
the **frequency** span into **one** exponent:

| Sector | δ = log(N_eff)/log(span) | Target | Dev |
|--------|--------------------------|--------|-----|
| ν | 2.22 | 2.241 | <1% |
| d | 2.45 | 2.449 | <1% |
| ℓ | 2.93 | 2.940 | <1% |
| u | 4.07 | 4.066 | <1% |

The density moments and the frequency span are **not independent inputs** — they
combine into a single exponent per sector.

### 2.4 Common invariant — the beat identity

The beat identity (QG260) directly couples a density moment to the frequency
span:
```
Σ√m / span = 10.009 ≈ 10  (dev 0.09%)
```

### 2.5 Actualization interpretation

The resonance dynamics (N=96) actualizes the spectrum. The density projection
reads **how many** modes actualize at each frequency / in each octave; the
frequency projection reads **where** the actualized frequencies sit (span, gap).
Both are views of the **single actualized list**.

---

## 3. The Minimum Structure

```
Resonance Dynamics (N=96)
    → the D96 spectrum (ONE 95-mode list)
        → { density read, frequency read }
            → the moments
```

The two projections are **not fundamental** — they are **dual reads of the single
spectrum**, coupled by:
1. the octave-count duality (frequency → density);
2. the unified exponent law δ = log(N_eff)/log(span) (density ↔ frequency);
3. the beat identity Σ√m/span ≈ 10.

---

## 4. Conclusion

### **SINGLE RESONANCE INVARIANT** (projection score 6/6)

The Density and Frequency projections are **manifestations of one object**: the
D96 spectrum (the resonance invariant). There is **no independent density
primitive** — the density structure is entirely determined by the frequency list.

**The reduction chain (complete):**
```
QG260 RESONANCE LAYER
 → QG261 OPERATOR LAYER
 → QG262 SAME OPERATOR SECTORS
 → QG263 SINGLE RESONANCE DYNAMICS
 → QG264 SINGLE RESONANCE INVARIANT
Resonance Dynamics → spectrum → {density, frequency} reads → moments → all sectors
```

**Honest caveat** (consistent with QG261/262/263): the operator-to-sector
assignment retains QG149-157-era target information; this *structural* duality
is D96-only and independent of any observable.
