# Publication Readiness — Final Audit

**Inputs:** `AT_v1_0_Paper_Revision.md`, `HostileReview.txt`, `PeerReview_Round2.md`,
`HostileReviewResponse.md`, and all formalization programs (Q, Random Actualization,
Continuum Limit, Quantum-Gravity Bridge, Metric Operator, Laplace-Beltrami, Curved
Schrödinger, Einstein Tensor, Metric Emergence, Metric Origin Closure).

**Method:** re-evaluate every Round-2 **FATAL** issue against the *completed tests* and their
*actual results*. No new physics.

---

## 1. Re-evaluation of Round-2 FATAL issues

| # | Round-2 FATAL issue | Evidence (completed tests / paper revision) | Classification |
|---|---|---|---|
| 1 | Primitives undefined as mathematical objects | Paper §2 formal definitions + §2.2 dynamical postulates; Q/Random-Actualization audits: object/state/operations formalized, measure/action still missing (now honestly listed) | **PARTIALLY RESOLVED** |
| 2 | Schrödinger derivation circular/incomplete | `GraphLaplacianContinuumTests` (exact $\lambda_k=(1/\Delta x^2)[2-2\cos(\pi k/(N{+}1))]$, error ~4× per $N$-doubling); `CurvedSchrodingerTests` (unitary); postulates explicit (§2.2) | **RESOLVED** |
| 3 | Gauge derivations = elementary topology | §15 scope: "structure, not dynamics (A-07)" | **PARTIALLY RESOLVED** |
| 4 | Complexity argument not a derivation | §6 exhibits weighted six-component decomposition; still a window-intersection, not a variational extremum | **PARTIALLY RESOLVED** |
| 5 | Structure/content split = immunization | §4 falsifiability + one falsified prediction (neutrino-Koide) | **PARTIALLY RESOLVED** |
| 6 | Composite objects admitted | §12 explicitly counts "composite objects: 2" and "zero conflicts" | **PARTIALLY RESOLVED** |
| 7 | Internal-3 unresolved while "complete" | §14/§16 "dispositioned"; internal-3 = "the one open door" | **RESOLVED** |
| 8 | T-09 at 0.10 cannot close | §13 "conditional no-go (provisional)" | **RESOLVED** |
| 9 | "Structurally complete" overstatement | §16 precise definition; internal-3 flagged | **RESOLVED** |
| 10 | Gravity→Einstein is ontological reinterpretation | §9 honesty note + §15 limitation 7 | **RESOLVED** |
| 11 | $\mathrm{Aut}(S^1)=U(1)$ as EM derivation | §15 scope note | **RESOLVED** |
| 12 | No controlled continuum limit (Schrödinger curved + Einstein + G) | see §2 below | **PARTIALLY RESOLVED** |
| — | *(MAJOR)* no unique testable prediction | §11 predictions table; uniqueness vs SM+$\Lambda$CDM still weak | **PARTIALLY RESOLVED** |

**Tally: RESOLVED = 6 · PARTIALLY RESOLVED = 7 · OPEN = 0** (one genuinely open
sub-component remains inside #12 — see §3).

---

## 2. The decisive item — FATAL #12 (continuum limit)

This was the reviewer's "single largest" objection. The formalization program tested the
chain piece by piece:

| Chain link | Test | Actual result |
|---|---|---|
| $L_Q \to -\nabla^2$ (flat) | `GraphLaplacianContinuumTests` | exact closed form, error ~4×/doubling |
| flat $\to$ Schrödinger | `HilbertSpaceAnalyzer` + `CurvedSchrodingerTests` | unitary, reduces to flat |
| BDG $\to \square$ (d'Alembertian) | `BDGOperatorContinuumTests` | $O(h^2)$, error ~4×/halving |
| curved Schrödinger ($L_W \to \Delta_g$) | `WeightedLaplacianTests`, `LaplaceBeltramiTests`, `CurvedSchrodingerTests` | symmetric/PSD, S¹ example, unitary |
| metric $\to \Gamma \to$ Riemann $\to$ Ricci $\to G_{\mu\nu}$ | `EinsteinTensorTests`, `EinsteinTensorIntegrationTests` | **works in standard math** (2D $K{=}1$, $R{=}2$; 3-sphere $G{=}-g$) |
| Q-events $\to$ metric | `MetricGenerationTests`, `MetricEmergenceTests`, `ConformalStructureTests`, `MetricOriginTests` | distance PRESENT, conformal factor native, conformal class **imported** (Malament) |

**Result:** the *Schrödinger* side of the continuum limit is now **controlled and tested**.
The *Einstein* side is **partially** verified: the standard $g\to G_{\mu\nu}$ chain works
(`EinsteinTensorBuilder`), but AT has **no native metric** — $g_{\mu\nu}$ arrives via the
external Malament/HKM theorem, and the BDG action (which produces the Einstein–Hilbert
action) is likewise imported. Newton's constant $G=\ell^2 c^3/\hbar$ is dimensional analysis,
now honestly labeled as such (§15).

---

## 3. The residual open sub-component

The single genuinely-open piece is the **native metric → operator coupling** (the "G4" gap
from `CurvedSpaceProgram.md`): AT does not produce $g_{\mu\nu}$ from Q-events; it imports
it. `MetricOriginTests` closed the *origin* question (the Malament import is a **proven
theorem**, not a gap), but the *dynamics* still flow through imported causal-set gravity
(BDG), not a AT-native derivation.

This is a **theory limit**, not a documentation gap, and the paper now discloses it
(§9 honesty note; §15 limitations 1, 5, 7).

---

## 4. Final recommendation

**READY_FOR_WHITEPAPER** (deposit on Zenodo as the archival step). **NOT READY_FOR_JOURNAL.**

Reasoning:

- **All framing/overstatement FATALs are RESOLVED** — the paper is now honest: "dispositioned"
  not "closed", conditional no-gos, $G$ as unit-consistency, gauge *structure* not *dynamics*,
  gravity as *ontological* in the weak field, internal-3 flagged as the one open door.
- **The dynamical core is now executable and tested** — $L_Q \to$ flat Laplacian $\to$
  Schrödinger (and curved $L_W$, Laplace–Beltrami, and the standard Einstein chain) are
  verified by passing xUnit tests. This converts the "tree of English phrases" objection
  into a concrete, reproducible dynamical system.
- **But the central derivation claim remains "logical, not mathematical"** at the Einstein
  boundary: the metric and the BDG action are imported (proven, but not AT-derived), $G$ is
  dimensional analysis, and no unique, sharp prediction discriminates AT from
  SM + $\Lambda$CDM (RAR $2\pi$ is admitted accidental; $w(z)$ is a small, not-yet-detected
  deviation). These are **theory limits**, not presentational defects.

A peer-reviewed journal article claiming to *derive observable structure from a minimal
primitive set* requires the Einstein/SM recovery to be a controlled derivation — which the
audits show it is not (yet). The honest, defensible artifact is a **white paper**: a research
program with a tested dynamical core, a precise taxonomy, falsifiable predictions, and
explicitly-flagged open items — exactly what the revised manuscript now is.

---

## 5. What would move it to READY_FOR_JOURNAL

1. A **native** $g_{\mu\nu}$ (metric → operator coupling, G4) — replacing the Malament import
   with a AT computation, or
2. A **unique, sharp, currently-testable** prediction that discriminates AT from
   SM + $\Lambda$CDM (e.g., a quantitative Einstein-correction signal, not an ontological
   reinterpretation), or
3. A **native re-derivation of the BDG action** from Q-event primitives (currently the single
   largest external dependency).

Until one of these lands, the theory is a **research program**, not a **derivation** — and the
white paper is its correct venue.
