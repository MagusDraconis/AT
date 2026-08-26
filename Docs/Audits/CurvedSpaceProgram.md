# Curved-Space Program

**Goal:** define the exact mathematics required for the missing curved-space bridge, and
determine whether it can arise from $L_Q$, BDG, or neither.
**Inputs:** `CurvedSpaceBridge_Report.md`, `QuantumGravityBridge_Report.md`,
`Q_Formalization_Program.md`, `CurvedSpaceSchrodinger.md`, `QuantumGravityBridge.md`.
**Discipline:** no new physics, no proposed derivations — formal gap analysis only.

---

## 1. Mathematical Requirements

### 1.1 The minimal operator

The missing object is the **metric-dependent second-order operator** — the Laplace–Beltrami
operator $\Delta_g$ (Riemannian) or, for the physical Lorentzian signature, the curved
d'Alembertian $\Box_g$:

$$\Delta_g f=\frac{1}{\sqrt{|g|}}\,\partial_\mu\!\big(\sqrt{|g|}\,g^{\mu\nu}\partial_\nu f\big),
\qquad
\Box_g f=\frac{1}{\sqrt{|g|}}\,\partial_\mu\!\big(\sqrt{|g|}\,g^{\mu\nu}\partial_\nu f\big)$$

(identical form; the two differ only in the signature of $g_{\mu\nu}$). This single object is
what is absent: it would (a) promote the flat Schrödinger operator to a **curved-space
Schrödinger** $i\partial_t\psi=\Box_g\psi$, and (b) provide the kinetic operator of the
Einstein–Hilbert action, closing the loop to Einstein.

### 1.2 The two required couplings

| Coupling | What it must do |
|---|---|
| **Flat → curved** | introduce the metric $g_{\mu\nu}$ as a *coefficient field*, so the operator reduces to $-\nabla^2$ / $\Box$ when $g=\eta$ (flat) |
| **Operator → Einstein** | show the operator's continuum action gives the Einstein tensor $G_{\mu\nu}$ (or the Ricci scalar in a conformal restriction) |

---

## 2. Existing Support

| Step | Status | Source |
|---|---|---|
| $L_Q\to-\nabla^2$ (flat, Riemannian) | **Proven + tested** | `GraphLaplacianContinuumTests` |
| BDG $\to\Box$ (flat, Lorentzian) | **Established (analytic) + tested** (flat-lattice reduction) | `BdgUniquenessAnalyzer` (O0), `BDGOperatorContinuumTests` |
| $L_Q$ and BDG are distinct operators (signature) | **Proven + tested** | `QuantumGravityBridgeTests` |
| Causal set $\to$ metric $g_{\mu\nu}$ | **Imported (external)** — Malament / Hawking–King–McCarthy | `GrBridgeAnalyzer`, `PoissonSprinklingAnalyzer` |
| $\Delta_g$ / $\Box_g$ (metric-dependent operator) | **Absent** | 0 "Beltrami" matches (`CurvedSpaceBridgeTests`) |
| Curved-space Schrödinger $i\partial_t\psi=\Box_g\psi$ | **Absent** | no metric-dependent operator |
| Einstein recovery from the operator | **Absent** (only the external causal-set route) | `EmergentGravityAnalyzer` (leading order, "logical not mathematical") |

---

## 3. Can $\Delta_g$ arise from $L_Q$, BDG, or neither?

**Neither, as implemented — but each has a natural curved generalization that is missing.**

| Source | As implemented | Curved generalization | Status in AT |
|---|---|---|---|
| $L_Q=D-A$ (unweighted) | flat Laplacian $-\nabla^2$ | **weighted** graph Laplacian $L_W=\mathrm{diag}(\sum w)-W$, with edge weights $w_{ij}$ encoding the metric → converges to $\Delta_g$ | **Absent** (only unweighted $L_Q$ exists) |
| BDG (flat causal set) | flat d'Alembertian $\Box$ | BDG on a **curved** causal set → $\Box_g$ (Benincasa–Dowker) | **Absent** (only the flat limit is asserted) |

The formal gap is therefore **not** "which operator" but **"how the metric enters the
discrete operator."** AT has the flat operator (twice over: $L_Q$ and BDG), but no rule that
inserts $g_{\mu\nu}$ as a coefficient.

---

## 4. Research Gaps

| # | Gap | What is missing | Blocked by |
|---|---|---|---|
| G1 | Metric-dependent operator $\Delta_g$/$\Box_g$ | a coefficient rule $g_{\mu\nu}\to$ operator | no metric field on the graph/causal set |
| G2 | Weighted graph Laplacian $L_W$ | edge weights $w_{ij}$ from the metric | only unweighted $L_Q$ implemented |
| G3 | Curved-space Schrödinger | $i\partial_t\psi=\Box_g\psi$ | G1 |
| G4 | Operator → Einstein | continuum action → $G_{\mu\nu}$ | G1; external causal-set route only |

---

## 5. Implementation Roadmap

| Step | Action | Status |
|---|---|---|
| 1 | State $g_{\mu\nu}$ as a field on the Q-event configuration (via the *external* Malament correspondence) | **Imported** |
| 2 | Generalize $L_Q\to L_W$ (weighted) **or** BDG on a curved causal set, so the operator carries $g_{\mu\nu}$ | **Missing** (G1/G2) |
| 3 | Define the flat reduction check: $\Delta_g\to\nabla^2$, $\Box_g\to\Box$ as $g\to\eta$ | **Missing** (the identity itself is standard, but no implementation) |
| 4 | Couple to the state vector: curved-space Schrödinger | **Missing** (G3) |
| 5 | Recover Einstein from the operator's action | **Missing** (G4); currently external |

---

## 6. Conclusion

The missing bridge requires **one object** — the metric-dependent Laplace–Beltrami /
d'Alembertian $\Delta_g$/$\Box_g$ — and **one rule** — how the metric enters the discrete
operator. AT already possesses both *flat* operators ($L_Q$ Riemannian, BDG Lorentzian) and
the *external* metric ($g_{\mu\nu}$ via Malament), but **no** step that combines them. The gap
is therefore well-localized: it is neither the flat Laplacian nor the metric in isolation,
but the **coupling** between them. This is a genuine research gap (G1–G4), not a documentation
gap, and closing it is the defining open mathematical problem of the emergent-gravity side of
AT.
