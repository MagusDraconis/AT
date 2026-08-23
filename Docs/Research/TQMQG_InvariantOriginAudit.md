# TQM-QG Phase 266 — Invariant Origin Audit

**Status:** COMPLETE — **UNIVERSAL CONSERVATION LAW**
**Tests:** TQMQG2660, TQMQG2661, TQMQG2662 (all passed)
**Core class:** `TQM.Core/ResearchXH/InvariantOriginAudit.cs`
**Question:** is the invariant Σλ = 12×96 fundamental, or the projection of a deeper conservation law?
**Method:** D96 only, no observables, no formulas, structure only.

---

## 1. The Trace Identity (universal)

The invariant Σλ = Σω² is the **trace of the graph Laplacian** L = D − A of the
D96 observable sector. For ANY graph, by construction:

```
trace(L) = Σ_i L_ii = Σ_i deg(i) = 2·(number of edges)
```

This is the **HANDSHAKE LEMMA** — a universal identity that holds for every
graph, not a fitted constant. **Verified:**

| Quantity | Value |
|----------|-------|
| N (nodes) | 96 |
| edges | 576 |
| trace(L) = Σ degrees | **1152** |
| 2·edges | **1152** |
| Σλ (eigenvalues) | **1152** |

---

## 2. Why the Value Is 12×96 (the network is regular)

The observable sector is a **REGULAR graph**: every one of the 96 nodes has
degree **12** (the gauge sector 1+3+8, QG161). For a regular graph of degree d
on N nodes:

```
trace(L) = N·d = 96·12 = 1152
```

**Verified:** degree distribution is `{12}` — all 96 nodes have degree 12.

The factorization Σλ = 12×96 is **not** an independent relation — it is the
trace identity `trace(L) = N·d` of a degree-12-regular 96-node graph.

---

## 3. Why It Is Conserved (the deeper law)

### 3.1 Universal trace conservation
`trace(L) = Σ degrees = 2E` holds for every Laplacian — a mathematical identity
of the L = D − A construction. It cannot change: the trace is the sum of the
diagonal, and the diagonal is the degree sequence of a **fixed** network.

### 3.2 Kernel / total-mass conservation
Every Laplacian has the **constant vector in its kernel**: row sums are exactly
zero (**verified: max |row sum| = 0**). The Laplacian dynamics ẋ = −Lx therefore
**conserves the total sum** Σx — the constant vector is a zero mode. This is the
**ACTUALIZATION CONSERVATION**: total actualization amplitude is conserved, and
the trace identity is its scalar projection.

### 3.3 Network / cycle conservation
The N=96 network is the **converged attractor** of the actualization dynamics
(QG115/125/159/160 — the D96 selection is INEVITABLE). The dynamics conserves
its attractor → the network (N, E, degree sequence) is fixed → trace = 2E is
fixed.

---

## 4. Conclusion

### **UNIVERSAL CONSERVATION LAW** (origin score 6/6)

Σλ is **NOT fundamental**. It is the projection of a **universal conservation
law** — the Laplacian trace identity (handshake lemma: trace = Σ degrees = 2E)
and the kernel conservation (constant vector in ker L → total actualization
conserved) — instantiated on the **conserved N=96 actualization attractor**.

The specific value **1152 = 96×12 = N·d** follows from the network being
**degree-12 regular** (degree = the gauge sector 1+3+8), i.e. from the network
structure that the actualization dynamics conserves.

**The complete reduction chain (QG260→266):**
```
Resonance Layer → Operator Layer → Same Operator Sectors
→ Single Resonance Dynamics → Single Resonance Invariant
→ Universal Resonance Invariant (Σλ = 12·96)
→ UNIVERSAL CONSERVATION LAW (Σλ = trace = 2E = N·d, handshake lemma)
```

**Why Σλ is conserved:** because it is the trace of the Laplacian — a quantity
that every graph conserves by the handshake lemma, on a network that the
actualization dynamics itself conserves. The invariant is derived from a
universal law, not a primitive constant.
