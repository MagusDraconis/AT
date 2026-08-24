# TQM-QG Phase 281 — Boundary Condition Audit

**Status:** COMPLETE — **RESONANCE = CONSERVATION + BOUNDARY**
**Tests:** TQMQG2810, TQMQG2811, TQMQG2812 (all passed)
**Core class:** `TQM.Core/ResearchXH/BoundaryConditionAudit.cs`
**Hypothesis:** observable structure is determined primarily by boundary conditions, not by energy content.
**Method:** no observables, no target values, D96 only, deterministic.

---

## 1. The Classical Analogy

| System | Boundary sets | Energy sets |
|--------|---------------|-------------|
| vibrating string | the frequencies (length) | the amplitude |
| pot with a lid | the standing waves (walls) | the fill level (does not change them) |
| resonance chamber | the resonances (geometry) | the power (does not change them) |

**In every case: energy sets the amplitude; the boundary sets the frequency.**

---

## 2. The D96 Application (the spectrum is boundary-determined)

### 2.1 The spectrum is a Laplacian eigenspectrum
The 95 modes ω = √λ are eigenvalues of the graph Laplacian L = D − A of the
N=96 network. L is determined by the **ADJACENCY** (the network structure = the
boundary), **NOT** by the activity array (the energy content / actualization
amplitude).

### 2.2 Frequencies are energy-invariant
The eigenvalues of L do not change if the activity (energy) is rescaled. The
activity enters the **dynamics** (what oscillates), not the **spectrum** (the
frequencies). Like a string: energy sets the amplitude, boundary sets frequency.

### 2.3 Conservation × boundary = the total
```
Σλ = trace(L) = 2·edges = 1152 = N·d = 96·12   (QG266)
CONSERVATION (handshake lemma)  ×  BOUNDARY (N=96 degree-12 regularity)
```
The **total** spectral weight is set by conservation × boundary conditions.

### 2.4 The individual modes are boundary-set
- family count = floor(log2 span)+1 = 3 (from span = a boundary-set ratio);
- occupancies [4,4,87] = the boundary-set mode distribution;
- the ladder, the acoustic peaks — all determined by the boundary-set spectrum.

### 2.5 The N=96 attractor is the boundary
The N=96 network is the **converged attractor** of actualization (QG116: 0%
residual link growth). The attractor is the **boundary**: the network closure
(N=96) fixes the spectrum — the **'pot with a lid'** whose walls fix the
resonances.

---

## 3. Conclusion

### **RESONANCE = CONSERVATION + BOUNDARY** (boundary-role score 6/6)

The resonance does NOT come from the energy content. It **emerges from**:
- **CONSERVATION** — the trace identity Σλ = 2E = N·d fixes the TOTAL weight;
- **BOUNDARY CONDITIONS** — the N=96 closure (the attractor) fixes the INDIVIDUAL
  modes (the frequencies).

The energy content sets the amplitudes, NOT the structure. **Observable structure
is determined by the boundary conditions** — resonance = conservation (total) +
boundary (modes).

**The reduction chain (QG260→281):**
```
Resonance Layer → Operator Layer → Same Operator Sectors
→ Single Resonance Dynamics → Single Resonance Invariant
→ Universal Conservation → Self-Consistency → Individuation → Difference Principle
→ Post-Resonance Integrity → Sector Emergence → Partial Assignment
→ Measurement Class Layer → Partial Role Principle → Equation Class Layer
→ Question Layer → Fundamental Boundary → True Fundamental Boundary
→ Final Frontier Inventory
→ RESONANCE = CONSERVATION + BOUNDARY
```

**Boundary-condition role in the reduction chain:** the boundary (N=96 closure)
is what selects the individual resonances; conservation (the trace) fixes the
total. The observable structure of the theory is the boundary's mode structure —
the pot with a lid whose walls determine the music.
