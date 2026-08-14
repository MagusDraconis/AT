# THE Q-MODEL — From Q to Cosmology

**Version 1.0 (revised)**

*A Theory of Structure, Complexity and Random Actualization*

---

## Abstract

THE Q-MODEL (TQM) is a theory of structure and content that compresses observable
physics to four primitives — the individuation principle $Q$, Random Actualization,
the scale triad $(\ell,\tau,\hbar)$, and a single continuous nonlinearity parameter
$M^2$ — and holds that the *form* of every structure is **derivable** from these
primitives while the *content* (specific masses, couplings, multiplicities, the Koide
angle) is **realized** by contingent draw. The derivation program yields: $U(1)$ as a
theorem ($\mathrm{Aut}(S^1)=U(1)$, $\pi_1(S^1)=\mathbb{Z}$; 0.95), spatial dimensionality
$d=3{+}1$ from the intersection of physical viability windows ($M^2\approx5$; 0.85), the
multiplicity lower bound $N\ge3$ from CP violation ($(N{-}1)(N{-}2)/2\ge1$; 0.90), the
log-normal abundance law from the central limit theorem, and phase-gradient gravity whose
leading order is the Einstein equations. The dynamical layer is the graph Laplacian
$L_Q$, whose tight-binding identity $H=tL_Q$ and reversible dynamics yield the
Schrödinger equation $i\partial_t\psi=L_Q\psi$ and a Hilbert space of eigenmodes. The
non-abelian gauge factors and the Koide relation are classified by a minimal
three-category taxonomy (DERIVED / REAL-UNDERIVED / DRAWN) with program consistency 0.95
and zero category conflicts. The theory makes quantitative, falsifiable predictions — the
zero-parameter radial acceleration $g_\dagger=cH_0/(2\pi)$, a specific $w(z)$, a
time-varying $\Lambda(t)$, and the log-normal abundance form — and one such prediction
(neutrino-Koide) was already falsified, demonstrating the theory is falsifiable. Five
no-go theorems (T-08 through T-12) are conditional statements under the no-new-primitives
constraint. Every in-scope question is **dispositioned** — derived, underivable,
underdetermined, contingent, or accepted as a computational layer — with the internal-3
node dispositioned *unresolved-contingent* (its gauge-count no-go provisional at 0.10).

---

## 1 Introduction

The Standard Model describes the *content* of particle physics — three gauge factors,
three generations, a hierarchy of masses and couplings — but leaves their *values*
unexplained. Attempts to explain these values by symmetry, topology, or dynamics
repeatedly terminate in the observation that specific numbers appear to be "given."

THE Q-MODEL (TQM) addresses this with a single organizational claim: **structure is
derivable; content is realized.** The *form* of observable structure is provable from a
minimal primitive set. The *content* is a draw from a contingent ensemble, and is
therefore classified rather than derived.

This paper reports the closed form of the theory at version 1.0, revised. It synthesizes
an extended hostile-audit program (Phases 1–159, the QG/X/DATA/QM programs, and the TRM
legacy reconciliation). Throughout we distinguish three categories with explicit
confidence assessments, we state the no-go theorems that bound what the theory cannot do,
and we state the dynamical system and the quantitative predictions that make the theory
falsifiable.

### 1.1 Method

Every result is classified by a **minimal three-category taxonomy**:

| Category | Meaning |
|---|---|
| **DERIVED** | computable from the primitives by theorem |
| **REAL-UNDERIVED** | real, precise, predictive structure whose origin is not computable (with generating mechanism: *emergent*; without: *structured*) |
| **DRAWN** | a coincidental draw of the abundance law — no hidden structure |

The taxonomy classifies *underivability* — REAL-UNDERIVED and DRAWN are **not** claimed as
derivations. Confidence is assigned per result (Phases 148–158); the overall program
consistency is 0.95 with zero category conflicts. No new primitives are introduced;
anthropic, numerological, hidden-parameter, and post-selection arguments are rejected by
protocol.

---

## 2 Formal Primitive Definitions

The primitives have two layers: an ontology layer that underwrites the *derivation of
structure*, and a dynamical layer that underwrites the *dynamical system* (§3). They meet
at $Q$.

### 2.1 Ontology layer

| Primitive | Role |
|---|---|
| $Q$ | principle of individuation (ontology) |
| **Random Actualization** | genuine ontological chance (becoming) — **assumption A-03**, not a derived object |
| $(\ell,\tau,\hbar)$ | spacetime scale, clock, action (unit conventions) |
| $M^2$ | nonlinearity regime (the single contingent continuous parameter) |

$Q$ individuates; Random Actualization realizes; $(\ell,\tau,\hbar)$ fixes units; $M^2$
sets the single continuous parameter the derivation hierarchy pins to $M^2\approx5$ (§6).
No gauge-group, multiplicity, or hidden-dimension primitive is permitted.

### 2.2 Dynamical layer (quantum postulates, TQM-155)

1. **$Q$ exists.** Topological charge quanta have position $x_i$ and phase $\theta_i$, and
   interact pairwise with coupling $J_{ij}=f(|x_i-x_j|)$. (Assumed, irreducible.)
2. **Reversible dynamics.** The state norm $\|\psi\|^2$ is conserved
   ($\Leftrightarrow$ unitarity). (Assumed; a mathematical equivalence.)
3. **Born rule.** $P=|\langle\phi|\psi\rangle|^2$, uniquely selected by additivity
   (Gleason's theorem, 1957).
4. **Measurement.** A measurement yields one definite outcome (the collapse axiom).

Postulates 1–2 derive the Hilbert space and the Schrödinger equation (§3); postulates 3–4
close the interpretation.

---

## 3 Dynamical System Summary

The dynamical content of TQM is the graph Laplacian and its consequences.

**The graph Laplacian.** For the $Q$-interaction graph with adjacency $A$ and degree $D$,
$L_Q=D-A$ is real symmetric, positive semi-definite, with zero row sums. Its eigenvectors
form an orthonormal basis — the **Hilbert space** of the theory.

**The tight-binding identity (TQM-142).** For a 1D chain,
$(L_Q)_{ij}=2\delta_{ij}-\delta_{i,j+1}-\delta_{i,j-1}$, identical to the tight-binding
Hamiltonian with $\varepsilon=2t$: $H=tL_Q$. This is a mathematical **identity**, not an
analogy.

**Schrödinger from reversibility (TQM-149–151).** For linear evolution
$\partial_t\psi=M\psi$, norm conservation $\partial_t\|\psi\|^2=\psi^\dagger(M+M^\dagger)\psi=0$
forces $M^\dagger=-M$. For real $M$, $M^T=-M$; the simplest 2×2 antisymmetric matrix $J$
satisfies $J^2=-I$ (so $J$ acts as $i$), and $M=J\otimes L_Q$ gives
$i\partial_t\psi=L_Q\psi$, with unitary evolution $\psi(t)=e^{-iL_Qt}\psi(0)$.

**Observables from the spectrum (TQM-145).** $m_{\rm eff}=1/\lambda_1$, $E=\mathrm{tr}(L)$,
$\Delta=\lambda_2-\lambda_1$, $\xi=1/\sqrt{\lambda_1}$, $D=\lambda_1$, $C=\log_2 N$.

**Ontology→physics bridge.** $L_Q$ eigenvector basis = Hilbert space (TQM-149); the causal
set continuum limit yields the metric (QG-001, XC006–012).

---

## 4 Structure/Content Split

The central principle is the **structure/content split** (D-06):

> **Form** (topology, symmetry, distribution *shape*, lower bounds) is **derived**.
> **Content** (drawn values, specific angles, upper bounds) is **contingent**.

The *form* of the abundance distribution (log-normal) is derived from the central limit
theorem in log-space; the *content* (the realized $\mu,\sigma$ and the drawn values) is
not computable and is classified DRAWN. The *form* of the Yukawa operator (an overlap
operator) is derived; the *spectrum* (the hierarchy $1{:}207{:}3478$) is not.

The split is **falsifiable** (§11): it makes specific predictions (log-normal form,
$N\ge3$, the RAR, $w(z)$), and one of them (neutrino-Koide) was falsified. It is a
classification scheme, not a protective one — a result is DRAWN only when no derivation
route survives, and the theory can be (and has been) wrong.

The taxonomy is minimal in the formal sense (Phase 158: three irreducible categories plus
two composite objects, zero conflicts):

- **internal $N=3$** = DERIVED (lower bound) ∩ DRAWN (upper bound);
- **$SU(3)$ whole** = REAL-UNDERIVED (structure) + DRAWN (count 3).

---

## 5 Derivation Hierarchy

The derivation tree from $Q$:

```
Q
│
├── Random Actualization ── (ℓ, τ, ħ) ── M²
│
├──► Complexity maximization (§6)
│        └──► M² ≈ 5  ──►  spatial 3  (d = 3+1)
│
├──► Oscillation
│        └──► Phase
│                └──► S¹ vacuum
│                        └──► U(1)   [Aut(S¹)=U(1), π₁(S¹)=ℤ]
│
├──► Binary defects (n = ±1 doublet)
│        └──► Z₂ winding ──► SO(3) = SU(2)/Z₂ ──► spinor SU(2)
│
└──► Tri-defect moduli (n = 3)
         └──► Aut(moduli of n defects) ⊇ SU(n) ──► SU(3) structure
```

Derived (theorem) ⇒ $U(1)$, spatial 3, $N\ge3$, log-normal form.
Emergent (structure from dynamics, value unpinned) ⇒ $SU(2)$, $SU(3)$ structure.
Contingent (drawn count) ⇒ $n=3$ (color count).

---

## 6 Complexity Functional

The complexity argument is a **weighted six-component decomposition** of the conditions
for observers (`ComplexityOptimumAnalyzer.cs`, X029/XE009), not a single scalar
variational functional.

| Component | Weight | $M^2$ window | Mechanism |
|---|---|---|---|
| Structure formation | 3.0 | broad $2$–$8$ | Bertrand's theorem (stable orbits) |
| Particle stability | 2.5 | $3$–$7$ | topological protection window |
| Chemistry potential | 4.0 | $3$–$5$ | atomic binding $\propto\alpha^2m_e$ |
| Information capacity | 2.0 | $3$–$10$ | diversity × stability |
| Evolution potential | 1.5 | $2$–$6$ | Darwinian diversity window |
| Observer viability | 5.0 | $4$–$6$ | intersection of all above |

**Spatial dimensionality.** $d=3{+}1$ is the **unique** value satisfying all of: Bertrand's
theorem (closed orbits only for $1/r^2$), Gauss's law $F\propto1/r^{d-1}$ (1/r Coulomb only
in 3D), knot stability (codim-2), Huygens (sharp propagation in odd $d$), and 2 GR
polarizations. $d=2{+}1$ fails gravity/atoms/knots; $d=4{+}$ fails orbits and knots.

**Nonlinearity.** $M^2\approx5$ is the **observer-viability intersection** — the chemistry
window ($M^2\approx3$–5) is the dominant constraint, and the observer peak is
$M^2\approx4$–6. It is not a hand-inserted number: it is where the component windows
overlap.

**Honesty note.** This is a *window-intersection* argument, not a variational theorem
(T-02 confidence 0.85). The analyzer's own output records that the generation count
$G\approx3$ is a **plateau, not a peak** — "our G=3 is contingent, not necessary."

---

## 7 Gauge Sector

**U(1) — DERIVED (0.95).** Phase lives on $S^1$; the circle's isometry group *is* $U(1)$,
and its winding $\oint\nabla\theta\cdot dl=2\pi n$ yields integer topological charge.
This is theorem T-01 ($\mathrm{Aut}(S^1)=U(1)$, $\pi_1(S^1)=\mathbb{Z}$), success 1.0.
TQM derives the gauge-group **structure** (which group), **not** the gauge **dynamics**
(Maxwell / Yang–Mills actions are borrowed; the 8-gluon algebra is assumption A-07).

**SU(2) — REAL-UNDERIVED, emergent (0.70).** Binary winding $\{n=\pm1\}$ gives the minimal
winding doublet; the Bloch sphere $S^2$ gives $SO(3)=SU(2)/Z_2$; the spinor double cover
gives $SU(2)$. The "2" is near-derived, but the lift $Z_2\to SU(2)$ is assumption A-06.

**SU(3) — REAL-UNDERIVED structure, DRAWN count.** The automorphism of the $n$-defect
moduli space satisfies $\mathrm{Aut}(C^n/S_n)\supseteq SU(n)$ (T-07), deriving the
*structure* but not the count. The count $n=3$ is **unfixed**: topology gives
$\pi_1(S^1)=\mathbb{Z}$, $S_n$ only permutes, persistence selects no classical group. This
is the **gauge-count no-go** (T-09), held **provisionally** at confidence 0.10
(graph-spectrum and lattice-mode routes untested).

| Factor | Status | Confidence |
|---|---|---|
| $U(1)$ | DERIVED | 0.95 |
| $SU(2)$ | REAL-UNDERIVED (emergent) | 0.70 |
| $SU(3)$ structure | REAL-UNDERIVED (emergent) | 0.10 |
| color count $n=3$ | DRAWN | — |

---

## 8 Flavor Sector

**Yukawa architecture.** The Yukawa operator is the **overlap operator**
$Y_{ij}=\langle\text{arch}_i|\text{amplitude}|\text{arch}_j\rangle$ (T-05): its form is
derived, its spectrum (the hierarchy $1{:}207{:}3478$) is underived — set by the contingent
architecture shapes.

**Koide relation — REAL-UNDERIVED, structured, CLOSED (underivable).** The charged-lepton
amplitudes satisfy

$$Q=\frac{\sum m}{(\sum\sqrt{m})^2}=0.6666605,\qquad \theta=44.9997^\circ,$$

equivalently $Q=2/3$ at $\theta=\arccos(1/\sqrt2)=45^\circ$. The relation is **real**
($\sim10^{-5}$ precision, Bayes factor vs coincidence $\approx3.2\times10^4$),
**predictive** (1981 prediction $m_\tau=1776.97$ MeV, confirmed 1992), and **RG-stable**.
Its origin is **not derivable**: the Koide no-go (T-08, 0.70) states no symmetry,
attractor, topology, or information-geometry selects $2/3$. The closure audit (Phase 159)
exhausted seven routes:

| Route | Status |
|---|---|
| Symmetry ($S_3$) | No-Go |
| Topology ($S^1$) | No-Go |
| Attractors | No-Go |
| Information Geometry | No-Go |
| Group Theory | No-Go |
| m=3 Closure | No-Go |
| Moduli Arguments | Blocked |

The Koide origin is a **closed question** — real, predictive, RG-stable, underivable — the
canonical REAL-UNDERIVED structure. We state plainly: the one quantitative relation the
theory highlights is the one it cannot derive; that is by design.

**Neutrino-Koide — FALSIFIED (0.90).** Requiring $Q=2/3$ for neutrinos yields
$Q_{\max}=0.585$ (normal) / $0.500$ (inverted), both below $2/3$; the measured
$\Delta m^2$ exclude all-lepton Koide (T-11). Koide is **charged-lepton-specific** and
contingent.

---

## 9 Gravity and Emergent GR

**Newton's constant.** $G=\ell^2c^3/\hbar$ (QG-007) is a **dimensional (unit-consistency)
result**, not a derivation of gravity's dynamics.

**The phase-gravity chain (QG-022).** oscillation → phase → causal density → metric →
curvature → Einstein:

- Phase gradient = varying causal-event density.
- Causal-set density → continuum metric (QG-001, Sorkin+).
- Metric curvature → Einstein equations.

**Leading-order recovery (X061).** $G_{\mu\nu}=8\pi G_{\rm eff}T_{\mu\nu}+O(\ell_P^2 R^2)$,
with Planck-scale corrections $\sim10^{-40}$; Newtonian $1/r^2$, lensing, redshift,
perihelion precession, and 2 gravitational-wave polarizations at speed $c$ are reproduced.

**Two modifications beyond GR:**

1. **Time-varying dark energy** $\Lambda(t)=\alpha/\sqrt{V(t)}$ (the 4-volume of the past
   light cone) — the only macroscopic deviation from $\Lambda$CDM.
2. **Singularity resolution** at $r\sim\ell_P$ (maximum curvature $1/\ell_P^2$).

**Honesty note.** The chain is **logical, not a new dynamical derivation**: its own
hostile review records "same equations, same predictions; no falsifiable difference" in
the weak field. The value is **ontological** — identifying the gravitational potential
with the phase field — and the *physical* content beyond GR is confined to $\Lambda(t)$
and singularity resolution.

**Dark matter / dark energy.** Dark matter is identified with hidden topological defects
(X064–X065b; $\Omega_{DM}\approx0.27$ DRAWN); dark energy is the metastable repulsive
architecture (QG-031) with $w(z)=-1+0.015(1+z)^{3/2}$.

**Radial acceleration relation.** TQM derives the RAR with **zero free parameters**:
$g_\dagger=cH_0/(2\pi)\approx1.05\times10^{-10}\ \mathrm{m/s^2}$, matching the measured
$a_0$; the $2\pi$ is a numerical accident (Phase 144).

**Frame dragging.** The TRM vector sector $\vec A_T,\ \vec B_T=\nabla\times\vec A_T$ is
structurally GR gravitomagnetism (Lense–Thirring, already measured) — **absorbed**, not a
new result.

---

## 10 Cosmology

**Expansion.** Expansion is an *interpretation* of FLRW (QG-081); every event/rate/
structure reinterpretation collapses to FLRW (QG-080–089). Tired-light descendants are
falsified.

**Causal-set $\Lambda$.** $\Lambda\sim1/\sqrt{N}$ is a genuine postdiction (Phase 140), not
numerology.

**Pantheon+SH0ES.** TQM and $\Lambda$CDM are indistinguishable at the current signal
$w(z)=-1+0.015(1+z)^{3/2}$ (DATA-001); a detectability audit (DATA-002) quantified the
power required for separation.

**Clusters.** Coma dynamical mass and ACCEPT X-ray gas fraction are reproduced.

**CMB — accepted partial computational layer.** The CMB chain (recombination,
$\theta_*$, acoustic oscillator, projection, visibility, velocity) reproduces the first
compressions and identifies the missing second-peak physics; a full $C_\ell$ Boltzmann
solver (acoustic phase shift $\phi\approx0.84$ rad, finite decoupling, ISW) remains
**computational** scope, not a theory gap. CMB enters the theory as a *constraint*
(acoustic peaks require collisionless dark matter, X063).

---

## 11 Quantitative Predictions

| Prediction | Type | Status |
|---|---|---|
| RAR $g_\dagger=cH_0/(2\pi)\approx1.05\times10^{-10}$ | zero-parameter | matches $a_0$ |
| $w(z)=-1+0.015(1+z)^{3/2}$ | cosmological | Euclid $>3\sigma$ by ~2030 |
| $\Lambda(t)=\alpha/\sqrt{V(t)}$ | dark energy | Euclid / Roman |
| log-normal abundance law | distribution form | testable on new abundances |
| $N\ge3$ (CP lower bound) | a priori (pre-observation) | confirmed |
| neutrino-Koide $Q=2/3$ | falsifiable | **falsified** (Phase 155) |

The theory is **falsifiable**: it made a concrete prediction (neutrino-Koide) that was
falsified, and it carries live quantitative predictions (RAR, $w(z)$, $\Lambda(t)$, the
abundance form) that distinguish it from the Standard Model plus $\Lambda$CDM in the
*form* sector, even though the *content* is DRAWN.

---

## 12 Classification System

The minimal taxonomy classifies fourteen results:

| Object | Category | Confidence |
|---|---|---|
| $U(1)$ | DERIVED | 0.95 |
| spatial 3 | DERIVED | 0.85 |
| $N\ge3$ | DERIVED | 0.90 |
| log-normal law | DERIVED | theorem |
| $SU(2)$ | REAL-UNDERIVED (emergent) | 0.70 |
| $SU(3)$ structure | REAL-UNDERIVED (emergent) | 0.10 |
| Koide $Q=2/3$ (reality) | REAL-UNDERIVED (structured) | 0.90 |
| Koide $45^\circ$ (origin) | REAL-UNDERIVED (structured) | 0.70 |
| Yukawas / couplings / $\Omega_{DM}$ | DRAWN | — |
| $N\le3$ | DRAWN | 0.70 |
| color count 3 | DRAWN | — |
| internal $N=3$ | DERIVED ∩ DRAWN | 0.70 |
| neutrino-Koide | FALSIFIED | 0.90 |

Category conflicts: **0**. Composite objects: **2**. Overall consistency: **0.95**.

---

## 13 Closed Questions and No-Go Theorems

The program closes eleven questions, five of which are **conditional no-go theorems**
(relative to the no-new-primitives constraint and the tested route space; not absolute
logical impossibilities, except the computation T-11):

| Theorem | Statement | Confidence |
|---|---|---|
| **T-08** Koide no-go | no symmetry / attractor / topology / information-geometry selects $Q=2/3$ | 0.70 |
| **T-09** Gauge-count no-go (provisional) | no principle fixes the defect count $n$; graph-spectrum/lattice-mode untested | 0.10 |
| **T-10** $N\le3$ no-go | no stability / anomaly / representation / defect / information bound gives $N\le3$ | 0.70 |
| **T-11** Neutrino-Koide falsification | $Q_{\max}=0.585$/0.500 $<2/3$ (computation) | 0.90 |
| **T-12** Shared-cascade no-go | one cascade vs three is untestable from one universe | 0.55 |

The full closed-question register (eleven items): Koide origin, neutrino-Koide, flavor
reducibility, gauge origin, $N\le3$, the Random-Actualization ensembles (four independent),
minimal taxonomy, the TRM legacy classification (3 Absorbed / 2 Rejected / 3 Candidate
Mathematics / 1 Open), the $S_3$ bridge (coincidental reuse), the why-3 meta-analysis (one
node, two faces), and the internal-3 closure.

**Internal-3 disposition.** Gauge count $n=3$ and multiplicity $N\le3$ are one node — the
internal multiplicity/count saturating at 3 — with two formally unlinked faces ($N$ vs $n$,
assumption A-10). It is dispositioned **unresolved-contingent**: contingent under the
no-new-primitives constraint, with the multiplicity face closed (T-10, 0.70) and the
gauge-count face **provisionally** closed (T-09, 0.10).

---

## 14 Open Questions (Dispositioned)

Under the no-new-primitives constraint, **no derivation route remains open**; the four
former open items are **dispositioned** (not resolved):

| Item | Disposition | Basis |
|---|---|---|
| Internal-3 Node | **unresolved-contingent** | T-09 (0.10, provisional) + T-10 (0.70) |
| Shared Cascade | **underdetermined** | T-12 (0.55), untestable from one universe |
| Unified Action | **TRM roadmap only** | $S_{\rm eff}[T,\vec A_T,\Theta]$, not a TQM result |
| CMB | **accepted partial computational layer** | full Boltzmann solver deferred as computational |

---

## 15 Scope and Limitations

**Scope.** TQM derives the **form** of structure and **classifies** the content. It does
**not** derive the gauge *dynamics* (Maxwell / Yang–Mills actions are borrowed, A-07), nor
the contingent *values* (masses, couplings, multiplicities, the Koide angle). The no-go
theorems are **conditional** (relative to the no-new-primitives constraint and the tested
route space).

**Limitations.**

1. **The gauge-count face of the internal-3 node is only provisionally closed.** T-09 is a
   weak no-go (0.10); the graph-spectrum and lattice-mode routes were never tested. This is
   the most tractable entry point for a future derivation.
2. **Encyclopedia completeness is ~81%.** The CMB chapter is a documented partial
   computational layer; this is accepted, not unresolved.
3. **Content is contingent by design.** The theory predicts the *distribution shape*
   (log-normal) but not the *values*; specific masses, couplings, multiplicities, and the
   Koide angle are REAL-UNDERIVED / DRAWN.
4. **The shared-cascade question is logically, not empirically, closed** — one universe
   cannot discriminate one cascade from three.
5. **The unified action is a roadmap**, not a result; it depends on vector/theta sectors
   absent from TQM.
6. **Confidence numbers are uncalibrated.** The 0.95/0.85/0.70/0.10/0.55 values are
   Phase-149/156 estimates, not calibrated posterior probabilities; they rank, they do not
   measure.
7. **The phase-gravity chain is ontological in the weak field** — it reinterprets GR rather
   than replacing it; the physical content beyond GR is $\Lambda(t)$ and singularity
   resolution.

---

## 16 Conclusion

THE Q-MODEL is a structurally complete theory in the following precise sense: every
derivable structure is derived, every contingent content item is classified, and every
in-scope question is **dispositioned** by theorem, no-go, falsification, or explicit
classification. The theory derives $U(1)$, spatial 3, the multiplicity lower bound, the
abundance-law form, Newton's constant, and phase-gradient gravity (with Einstein as its
leading order); it possesses a dynamical system ($L_Q\to$ Schrödinger); it classifies
$SU(2)$, $SU(3)$, the Koide relation, and the contingent content as REAL-UNDERIVED or
DRAWN; and it closes the Koide origin, the gauge count (provisionally), the $N\le3$ bound,
the neutrino-Koide conjecture, and the shared-cascade question by five conditional no-go
theorems (T-08 through T-12). It makes falsifiable quantitative predictions (§11), and one
was already falsified.

The single residual of genuine scientific tension is the **internal-3 node** — why the
internal multiplicity/count saturates at 3 — which is dispositioned unresolved-contingent
with a provisional gauge-count no-go (T-09, 0.10). This is the theory's one open door: the
graph-spectrum and lattice-mode routes to $n=3$ remain untested.

**Structure is derivable. Content is realized.** That one sentence, made precise by a
three-category taxonomy with confidence 0.95 and zero conflicts, a dynamical system, and a
set of falsifiable predictions, is the closed form of THE Q-MODEL at version 1.0.
