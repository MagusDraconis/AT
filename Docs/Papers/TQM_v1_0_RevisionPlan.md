# TQM Paper Revision Plan

**Goal:** turn `TQM_v1_0_Paper.md` into a publication-ready paper by adding six sections
and fixing three wording issues identified by the hostile-review response.
**Inputs:** `TQM_v1_0_Paper.md`, `HostileReview.txt`, `HostileReviewResponse.md`.
**Constraint:** no new physics, no new derivations — use only accepted repository results.

---

## 0. Priorities

| Priority | Meaning |
|---|---|
| **P0** | publication blockers — must be done before resubmission |
| **P1** | major improvements — recommended for v1.1 |
| **P2** | optional polish |

---

## 1. New sections (P0)

### 1.1 Formal Primitive Definitions (fixes M1)

**Content to add:** a two-layer formalization of the primitives, replacing the one-line
§2 table.

- **Ontology layer** (the Master Reference set): $Q$ (individuation), Random
  Actualization (ontological chance, assumption A-03), $(\ell,\tau,\hbar)$ (units), $M^2$
  (the single contingent continuous parameter). State explicitly that Random Actualization
  is an **assumption**, not a derived object.
- **Dynamical layer** (the quantum postulates, TQM-155): (1) $Q$ exists — topological
  charge quanta with position $x_i$, phase $\theta_i$, pairwise coupling $J_{ij}$;
  (2) reversible dynamics — norm conservation $\Leftrightarrow$ unitarity; (3) the Born
  rule (uniqueness via Gleason's theorem); (4) measurement (the collapse axiom).
- **Reconciliation sentence:** the ontology primitives underwrite the *derivation of
  structure*; the quantum postulates underwrite the *dynamical system* (see §1.2). The
  two layers meet at $Q$.

**Source:** `Docs/Theory/02_Fundamental_Postulates.md`, `Docs/Theory/03_Q_Theory.md`,
`TQM_Master_Reference.md` §2, §17 (A-03).

### 1.2 Dynamical System Summary (fixes F1, F3)

**Content to add:** the missing dynamics, in one compact section.

- The graph Laplacian $L_Q = D - A$ (real symmetric, positive semi-definite, zero row
  sums); the tight-binding identity $H = t\,L_Q$ (TQM-142, a mathematical identity, not
  an analogy).
- Reversible dynamics ⇒ $M^\dagger=-M$ (anti-Hermitian) ⇒ $J^2=-I$ acts as $i$ ⇒
  Schrödinger $i\,\partial_t\psi = L_Q\,\psi$; unitary evolution $\psi(t)=e^{-iL_Qt}\psi(0)$
  (TQM-149–151).
- Observables from the spectrum: $m_{\rm eff}=1/\lambda_1$, $E=\mathrm{tr}(L)$,
  $\Delta=\lambda_2-\lambda_1$, $\xi=1/\sqrt{\lambda_1}$, $D=\lambda_1$, $C=\log_2 N$
  (TQM-145).
- Ontology→physics bridge: $L_Q$ eigenvector basis = Hilbert space (TQM-149); causal set
  → continuum metric (QG-001, XC006–012).

**Source:** `Docs/Theory/04_Q_Networks_and_Laplacian.md`, `02_Fundamental_Postulates.md`.

### 1.3 Complexity Functional (fixes M3)

**Content to add:** the actual complexity argument, stated honestly.

- The "complexity" is a **weighted six-component decomposition**, not a single scalar
  variational functional: Structure (3.0), Particles (2.5), Chemistry (4.0), Information
  (2.0), Evolution (1.5), Observer (5.0) (`ComplexityOptimumAnalyzer.cs`).
- $d=3{+}1$ is the **unique** intersection of Bertrand's theorem (stable orbits), Gauss's
  law $1/r^{d-1}$ (1/r EM only in 3D), knot theory (codim-2), Huygens, and 2 GR
  polarizations.
- $M^2\approx5$ is the **observer-viability intersection** (chemistry window $M^2\approx3$–5
  dominates; observer peak $M^2\approx4$–6), **not** a hand-inserted 5.
- **Honesty note (must include):** this is a *window-intersection* argument, not a
  variational theorem; state T-02 at confidence 0.85, and quote the analyzer's own
  admission that $G\approx3$ is a **plateau, not a peak** ("our G=3 is contingent").

**Source:** `TQM.Core/ResearchXE/ComplexityOptimumAnalyzer.cs`, X029/XE009,
`TQM_Master_Reference.md` T-02.

### 1.4 Emergent-GR Derivation Summary (fixes M6)

**Content to add:** the gravity chain, with its true scope.

- The phase-gravity chain: oscillation → phase → causal density → metric → curvature →
  Einstein (QG-022). Each link is established; the composition is the claim.
- Leading-order recovery $G_{\mu\nu}=8\pi G_{\rm eff}T_{\mu\nu}+O(\ell_P^2 R^2)$; Newtonian
  $1/r^2$, lensing, redshift, precession, 2 polarizations at speed $c$ (X061).
- Two modifications: time-varying $\Lambda(t)=\alpha/\sqrt{V(t)}$ and singularity
  resolution at $r\sim\ell_P$.
- **Honesty note (must include):** $G=\ell^2c^3/\hbar$ is **dimensional analysis**
  (unit-consistency), not a derivation; and the phase-gradient chain is, by the program's
  own hostile review, **ontological** — "same equations, same predictions; no falsifiable
  difference" in the weak field. The two macroscopic deviations ($\Lambda(t)$, singularity
  resolution) are the only physical predictions beyond GR.

**Source:** `PhaseGradientGravityAnalyzer.cs` (incl. its built-in hostile review),
`EmergentGravityAnalyzer.cs`, QG-022, X060h/X061.

### 1.5 Quantitative Predictions (fixes F2, F5)

**Content to add:** a dedicated predictions section, foregrounding the falsifiable numbers.

| Prediction | Type | Status |
|---|---|---|
| RAR $g_\dagger = cH_0/(2\pi) \approx 1.05\times10^{-10}$ | zero-parameter | matches $a_0$ |
| $w(z)=-1+0.015(1+z)^{3/2}$ | cosmological | Euclid by ~2030 |
| $\Lambda(t)=\alpha/\sqrt{V(t)}$ | dark energy | Euclid/Roman |
| log-normal abundance law | distribution form | testable on new abundances |
| $N\ge3$ (CP lower bound) | a priori (pre-observation) | confirmed |
| neutrino-Koide $Q=2/3$ | falsifiable | **falsified** (Phase 155) |

- Emphasize: the theory **can be wrong** (neutrino-Koide was), so it is falsifiable.

**Source:** `RARModel.cs`, `TwoPiOriginAnalyzer.cs`, X062, XB002, T-03, T-11,
`EmergentGravityAnalyzer.cs`.

### 1.6 Scope and Limitations (fixes M2, M5, M7)

**Content to add:** a scope statement (up-front and repeated in the conclusion):

- TQM derives the **form** of structure and **classifies** the content; it does **not**
  derive the gauge *dynamics* (Maxwell / Yang–Mills actions are borrowed, A-07) nor the
  contingent *values* (masses, couplings, multiplicities, Koide).
- The no-go theorems (T-08…T-12) are **conditional** — relative to the no-new-primitives
  constraint and the tested route space — not absolute logical impossibilities. T-11 is a
  computation (falsification), distinct from the enumerative no-gos.
- The structure/content split is **falsifiable** (see §1.5), not pure immunization.

---

## 2. Wording fixes (P0)

| Fix | Old (wrong) | New (correct) |
|---|---|---|
| Internal-3 wording | "the internal-3 node is closed" | "the internal-3 node is **dispositioned unresolved-contingent**"; its gauge-count face is **provisionally** closed (T-09 = 0.10) |
| "closed" vs "dispositioned" | "every in-scope question is closed"; "no derivation route remains open" | "every in-scope question is **dispositioned** (derived / underivable / underdetermined / contingent / roadmap)"; do not imply *resolved* |
| classification vs derivation | "DERIVED" applied loosely; REAL-UNDERIVED/DRAWN framed as successes | "DERIVED" = theorem only; "REAL-UNDERIVED" / "DRAWN" = **classifications of underivability**, not derivations; add "derives structure, not dynamics" |

**Mechanical edits:** replace "closed question" with "dispositioned question" in §11–§13;
replace "no in-scope route remains open" with "no in-scope route remains open as a
*derivation*; the residuals are dispositioned (not derived)"; relabel T-09 "provisional
no-go" in §10 and the theorem registry.

---

## 3. Priorities recap

### P0 — publication blockers (must do before resubmission)
1. Add §1.1 Formal Primitive Definitions (M1).
2. Add §1.2 Dynamical System Summary (F1, F3).
3. Add §1.3 Complexity Functional (M3).
4. Add §1.4 Emergent-GR Derivation Summary (M6).
5. Add §1.5 Quantitative Predictions (F2, F5).
6. Add §1.6 Scope and Limitations (M2, M5, M7).
7. Apply the three wording fixes (F4 + framing).

All seven are **documentation fixes**; no new physics or derivations are required.

### P1 — major improvements (v1.1)
- Add a confidence-provenance note (the 0.95/0.85/0.70/0.10/0.55 are Phase-149/156
  estimates, not calibrated posteriors).
- Clarify the CMB as a *constraint* (X063) with the full solver deferred as computational.
- Relabel T-09 in the theorem registry as "provisional no-go" rather than "no-go".

### P2 — optional polish
- Address the minor review points in full (Koide "real yet underivable" as an explicit
  statement; neutrino-Koide as a computation vs a new measurement; DRAWN-content
  non-discrimination caveat).
- Add a formal "falsifiability" subsection (what would refute the structure/content split).

---

## 4. Expected outcome

After P0, the paper will (a) contain a dynamical system, (b) formalize its primitives,
(c) exhibit its complexity and gravity arguments with honest scope, (d) foreground its
falsifiable predictions, and (e) use "dispositioned" instead of "closed". This directly
answers F1–F5 and M1–M7 of the hostile review. The three genuine theory gaps (provisional
T-09, contingent content, immunization risk) remain, but are now **stated**, not hidden —
which is the standard required of a publication whose scope is honestly drawn.

**Resubmit after P0; the verdict should move from NOT READY to READY (with stated scope).**
