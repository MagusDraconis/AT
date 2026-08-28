# ResearchY-A — Wave Origin Audit

**Track:** ResearchY-A
**Title:** Wave Origin Audit — can Difference be read as a localized disturbance that propagates through an initially uniform background?
**Date:** 2026-08-28
**Status:** EXPLANATORY AND DERIVATIONAL AUDIT — open track

**Rules:**
- Does NOT modify canonical AT V2.0.
- Introduces NO new physics.
- Is an explanatory and derivational audit only: every "wave" notion must map onto an existing
  canonical object, or it is flagged as a candidate or a contradiction.
- Referee-safe: no claim in this document may exceed what the canonical monograph establishes.

**Scope:** This audit examines whether the canonical hierarchy

```
Difference → Actualization → Inevitable Spectrum → Physics
```

admits a consistent re-reading in the language of localized disturbances, propagation, and
standing waves. It answers eight research questions, gives mathematical candidates,
enumerates the contradictions with canonical AT, proposes an explanatory derivation chain,
and lists open problems.

---

## 1. Goal

Investigate whether Difference can be reinterpreted as a localized disturbance that
propagates through an initially uniform background, and whether the Inevitable Spectrum can
be read as the resonance (standing-wave) structure of that propagation. The audit is
explanatory: it tests whether the canonical mathematics *already contains* the wave
vocabulary, not whether new physics follows from it.

The audit is constrained by the canonical end-state:
- primitives $\{\text{Difference},\eta\}$ (Ch1, Ch2);
- Actualization is the derived process face of Difference (Ch3, MONO006);
- the spectrum is the Laplacian eigenspectrum of the converged attractor $C_{96}(\pm1..\pm6)$
  (Ch5, Ch6, QG295);
- $\rho,\psi$ are the trace/traceless faces of the one Difference (Ch1, QG286);
- $\pi$ is a boundary constant, not a primitive (Ch2, QG291; Bekenstein $1/4$ requires
  imported $2\pi$, QG185/QG196);
- light propagation is null-geodesic, conformally invariant, $n=1$, redshift-without-lensing
  (QG21, QG28, QG212);
- scalar (breathing) "waves" produce zero Michelson differential signal and do not match
  observed gravitational-wave tensor modes (QG18, QG20).

---

## 2. Conceptual Model

The wave re-reading of canonical AT is a *vocabulary mapping* onto structures that already
exist. The canonical anchor map is:

| Wave concept | Canonical AT object | Status |
|---|---|---|
| initially uniform background | the zero mode ($\lambda_0=0$, the uniform configuration) | exact |
| localized disturbance | a Q-event, or a compact deviation of the counting measure $\rho$ from uniform | structural |
| disturbance magnitude | the scalar face $\rho = |\psi|^2$, the count share | exact |
| disturbance anisotropy | the tensor face $\psi$ (Weyl content, read against $\eta$) | exact |
| propagation (generation) | Actualization: Galton–Watson branching, path multiplicity $\mu^k$ | structural |
| the "medium" | the converged attractor $C_{96}(\pm1..\pm6)$ (closed ring) | exact |
| standing normal modes | the 95 positive eigenmodes of the graph Laplacian | exact (mathematical identity) |
| mode frequency | $\omega_k = \sqrt{\lambda_k}$ (the canonical $\omega=\sqrt\lambda$ convention) | exact |
| wave number / wavelength | Fourier index $k$; wavelength $96/k$ in ring units | exact |
| circular topology | the circulant ring $C_{96}$ | exact |
| resonance structure | degeneracy groups $[42\times2,5,6]$, spectral gaps, octave bands $[4,4,87]$ | exact |
| cyclic phase | the discrete state-phase lattice $\theta_k = 2\pi k/96$ (Ch9) | exact |
| radius (radial coordinate) | the sector-ladder radii $6.0$–$17.333$ (QG121/128), $r_0=\ln(\mathrm{span})$ | derived quantity |

The model is therefore: **Difference is the deviation; Actualization is the spreading;
the attractor is the closed medium; the spectrum is its normal-mode (standing-wave) content.
No step in this mapping adds a primitive or an observable.**

---

## 3. Answers to the Eight Research Questions

### Q1. Is Difference equivalent to a localized disturbance?

**Verdict: COMPATIBLE (as a configuration of Difference, not its definition).**

Canonically, Difference is the counting difference from a uniform background (Ch1,
Definition c01:def:difference); a Q-event is its unit. The uniform background is exactly the
zero mode ($\lambda_0=0$); the positive modes are the deviations. Any compact deviation of
$\rho$ decomposes into a superposition of positive modes. Hence:

- a **localized disturbance** is a legitimate *particular configuration* of Difference (a
  Q-event, or a compact deviation of $\rho$);
- Difference is **not defined as** a disturbance — the definition is the counting difference;
  the disturbance reading is a re-description of a configuration, not the primitive's
  definition;
- the "wave" character is carried by the dynamics (Actualization), not by the primitive
  itself.

The distinction matters: re-reading is allowed, redefinition is not. The audit adopts the
re-reading.

### Q2. Does Actualization correspond to propagation of disturbance?

**Verdict: PARTIAL — propagation in *generation space* only; not spatial medium propagation.**

Actualization is the count-producing dynamics of Difference (Ch3). Its mathematical core is
the Galton–Watson branching: generation $k$ has $\mu^k$ root-to-generation-$k$ paths, the
path multiplicity (MONO_PHASE002), entering only through the normalized weight
$\rho_k=\mu^k/S$, $S=\sum_{j<K}\mu^j$ (QG216). The generation index $k$ is the "time" of the
branching.

- **Compatible:** the count *spreads* through the generation tree — a disturbance at the
  root (generation 0) reaches $\mu^k$ paths at generation $k$. This is propagation of count
  in generation space, a native structure.
- **Not compatible without qualification:** propagation through a *pre-existing spatial
  medium*. Canonical light propagation is null-geodesic, conformally invariant, $n=1$,
  redshift-without-lensing (QG21, QG28, QG212): the conformal factor $\rho$ cannot refract.
  Any reading of "propagation of disturbance" that implies a refractive medium contradicts
  the canonical propagation law and is excluded.
- Spatial structure itself is *emergent* (the metric $g=\rho^{2/d}\eta$ and spacetime are
  constructed, Ch2/Ch10); it is not a pre-existing arena. Treating the uniform background as
  a spatial medium would import an arena, contradicting emergent spacetime.

Hence: Actualization is the propagation of count in generation space; spatial propagation is
an emergent, conformally-invariant statement and must not be read as medium transport.

### Q3. Can the Inevitable Spectrum arise as a resonance structure of propagation?

**Verdict: COMPATIBLE AS AN EXPLANATION — but the canonical derivation does not route through propagation.**

The spectrum is the Laplacian eigenspectrum of the converged attractor (Ch5, Ch6, QG295):
$\lambda_k = 2\sum_{d=1}^6(1-\cos 2\pi dk/96)$, $\omega_k=\sqrt{\lambda_k}$. The graph
Laplacian is the discrete Laplacian of the ring; its eigenmodes are, by definition, the
standing normal modes of the discrete medium. This is an exact mathematical identity, not
new physics.

- The resonance condition of the theory is **Resonance = Conservation + Boundary** (Ch3,
  Theorem c03:thm:resonance-readout): the count-conservation identity plus the closure fixed
  point $N=96$.
- In wave language this reads: the standing-wave content of the closed ring is fixed by the
  conservation of the count and the closure (periodicity) of the ring — the same two
  conditions.
- However, the canonical *derivation* of the spectrum is: attractor → graph Laplacian →
  eigenspectrum. It does not require a propagation stage. The propagation re-reading is an
  explanatory layer over an already-derived structure, not the derivation path.

### Q4. Does a natural center/source exist in the Difference framework?

**Verdict: PARTIAL — no spatial center; a generation root and derived radial coordinates exist.**

- **No spatial center:** the attractor is a circulant ring $C_{96}$ — translation-invariant,
  every site equivalent. Standing waves on a closed ring need no center; there is no
  distinguished source site in the converged medium.
- **Generation root:** the Galton–Watson tree has a natural root (generation 0) — a source
  in generation space. This is the natural "origin" of the branching.
- **Derived radial coordinate:** the sector-ladder construction uses radii $6.0$–$17.333$
  (QG121/128) with $E_{\mathrm{rung}}=\mathrm{radius}\cdot(M_Z/6)$, and the gravity bridge
  uses $r_0=\ln(\mathrm{span})$ (QG182). These are center-relative quantities — the closest
  canonical analogue of a "radius."
- The discrete phase lattice $\theta_k=2\pi k/96$ (Ch9) provides a cyclic order (a circle of
  phase positions), but it is a phase, not a spatial coordinate.

A wave reading that *assumes* an outgoing-wave source in space would contradict the
centerless attractor. The defensible "source" is the branching root in generation space.

### Q5. Is D96 interpretable as a standing-wave solution?

**Verdict: YES — formally exact (static normal modes of the closed ring).**

The eigenmodes of the circulant graph Laplacian are the discrete Fourier modes
$\phi_k(n)=e^{2\pi i k n/96}$. Therefore:

- the 95 positive modes are the 95 non-trivial standing normal modes of the ring (the zero
  mode is the uniform rest state);
- $\omega_k=\sqrt{\lambda_k}$ are the mode frequencies (the canonical convention);
- the multiplicity structure $[42\times2,5,6]$ is the degeneracy pattern of the ring modes;
- the Z2 doublet pairing (95/95, QG153) is exactly the $\pm k$ degeneracy of a ring:
  $\lambda_k=\lambda_{96-k}$. This is a natural wave-mechanical signature (the same $\pm n$
  degeneracy of a drum/ring) and *supports* the weak-isospin doublet structure (QG153–QG155)
  as a ring-mode degeneracy — a supporting interpretation, not new physics;
- the octave bands $[4,4,87]$ are frequency bands of the medium; the family count
  $\lfloor\log_2(\mathrm{span})\rfloor+1=3$ (QG210) reads as the number of octaves spanned by
  the medium.

Caveat: "standing wave on a ring" means periodic normal modes with no fixed boundary. The
spectrum is a static normal-mode statement; a *dynamical* standing-wave evolution (a wave
equation on the ring) is not currently derived and would be new physics.

### Q6. Does circular propagation emerge naturally?

**Verdict: PARTIAL — circular *topology* emerges; circular *dynamics* is a candidate.**

- **Circular topology: YES.** The attractor converges to the circulant ring $C_{96}$ — a
  closed circle of 96 sites. Circularity is an output of the actualization attractor, not an
  input (Ch5, QG116).
- **Circular phase order: YES.** The state-phase lattice $\theta_k=2\pi k/96$ (Ch9) is a
  discrete circle of phase positions, and the mixing/CP phases $\delta=\arcsin(r)$ are
  continuous functions of spectral ratios, not lattice-restricted (MONO_PHASE001).
- **Circular propagation dynamics: CANDIDATE.** No canonical result currently derives a
  *traveling* disturbance around the ring. The CKM CP phase arises from "chiral circulation"
  / spectral circulation (QG166), which is a circulation *notion* in the spectral read, but
  not a derived dynamical traveling wave on the attractor. A genuine traveling-wave
  statement around the ring would require dynamics beyond the static attractor — new
  physics, out of scope.

### Q7. Can π or 2π arise from closure conditions rather than being imported?

**Verdict: OBSERVATION — the spectral layer already uses $2\pi$ from ring closure; this does NOT resolve the Bekenstein boundary.**

- The canonical eigenvalue formula is
  $\lambda_k=2\sum_{d=1}^6(1-\cos 2\pi dk/96)$, whose $2\pi$ enters through the roots of
  unity $e^{2\pi i k/96}$ of the circulant ring. For a *closed* ring of $N$ sites, the
  natural Fourier modes are $e^{2\pi i k n/N}$; the $2\pi$ is the **periodicity constant of
  the ring closure**, not an external physics import. So within the spectral layer, $2\pi$
  arises from closure (the ring is closed; closedness implies periodicity; periodicity is
  measured by the circle constant).
- This is a genuine observation: the theory *already uses* $2\pi$ from closure inside its own
  spectral formula, while declaring $\pi$ a boundary constant (Ch2, Theorem
  c02:thm:eta-primitive-pi-boundary; QG291).
- **Careful scoping:** the $2\pi$ in the spectral formula is distinct from the $2\pi$ in the
  Hawking/Bekenstein factor $T=\kappa/(2\pi)$ (QG185, QG196). The audit does **not** claim to
  derive the Bekenstein $1/4$; QG196 establishes that the exact $1/4$ cannot be produced
  internally without importing $\pi$. The observation here is limited to the *spectral
  layer's* use of the circle constant as the closure periodicity constant.
- The numerical value of $\pi$ is not "derived" by closure: the ring closure forces the use
  of the circle constant (the ratio of circumference to diameter of the emergent circle), it
  does not compute its value. This partially answers Q7 (closure *selects* $2\pi$ as the
  periodicity constant) without overturning the canonical $\pi$-boundary declaration.

### Q8. Is radius, circumference, wavelength, frequency, and resonance already implicit in Difference?

**Verdict: YES — all five are already present as derived/structural quantities.**

| Concept | Canonical location |
|---|---|
| radius | sector-ladder radii $6.0$–$17.333$ (QG121/128), $E=r\cdot(M_Z/6)$; $r_0=\ln(\mathrm{span})$ (QG182) |
| circumference | $N=96$: the discrete circumference of the closed ring $C_{96}$ |
| wavelength | mode index $k$ ↔ wavelength $96/k$ in ring units (discrete Fourier modes) |
| frequency | $\omega_k=\sqrt{\lambda_k}$, the canonical $\omega=\sqrt\lambda$ mode convention |
| resonance | degeneracy groups $[42\times2,5,6]$, spectral gaps, octave bands $[4,4,87]$, the operator/lock layer (Ch6/Ch7/Ch8) |

None of these needs to be introduced: the wave reinterpretation is a *vocabulary mapping*
onto quantities the theory already derives.

---

## 4. Mathematical Candidates

| # | Candidate | Canonical status | Note |
|---|---|---|---|
| C1 | Graph-Laplacian eigenmodes = standing normal modes of the ring | EXACT (canonical) | $\lambda_k=2\sum(1-\cos 2\pi dk/96)$; $\omega_k=\sqrt{\lambda_k}$ |
| C2 | Discrete Fourier decomposition $\phi_k(n)=e^{2\pi i k n/96}$ | EXACT (canonical) | eigenmodes of a circulant; wave number $k$, wavelength $96/k$ |
| C3 | Z2 doublet = $\pm k$ ring-mode degeneracy | EXACT (canonical) | $\lambda_k=\lambda_{96-k}$; supports weak-isospin doublets (QG153–155) as ring degeneracy |
| C4 | Zero mode = uniform rest state; Green's/heat kernel $\sum_k e^{-\lambda_k t}\phi_k\phi_k^\dagger$ | CANDIDATE (not canonical) | a "disturbance kernel" would require dynamics; out of canonical scope unless derived |
| C5 | Ladder radii as radial coordinate, $E=r\cdot(M_Z/6)$ | EXACT (canonical) | radii $6.0$–$17.333$ (QG121/128) |
| C6 | State-phase lattice $\theta_k=2\pi k/96$ = cyclic phase on the ring | EXACT (canonical) | Ch9; distinct from continuous mixing/CP phases (MONO_PHASE001) |
| C7 | Closure-periodicity: $2\pi$ enters via $e^{2\pi i k/96}$ | OBSERVATION | the ring's closure selects the circle constant; does not resolve Bekenstein $2\pi$ |
| C8 | Dispersion relation $\omega_k=\sqrt{\lambda_k}$ (graph dispersion) | EXACT (canonical) | the frequency is fixed by the graph; no continuum limit required |
| C9 | Family count = octave span: $\lfloor\log_2(\mathrm{span})\rfloor+1=3$ | EXACT (canonical) | the medium spans $2.68$ octaves (QG210); supports the "band structure" reading |

Only C1–C3, C5, C6, C8, C9 are canonical. C4 and C7 are flagged as candidate/observation and
must not be promoted to derivation status within this audit.

---

## 5. Contradictions against Canonical AT

A wave re-reading is safe only if it respects the following scoping constraints. Each is a
canonical result that the wave language must not contradict.

| # | Contradiction risk | Canonical constraint | Consequence |
|---|---|---|---|
| 1 | "propagation through a medium" implies refraction | Light propagates on null geodesics, $n=1$, conformally invariant; $\rho$ cannot refract (QG21, QG28, QG212) | The wave reading must not add a refractive index. Propagation is along the causal-order light cone. |
| 2 | scalar "wave" observables | Scalar (breathing) disturbances produce zero Michelson differential signal and do not match LIGO tensor modes (QG18, QG20) | The disturbance re-reading changes NO observable; no new wave observables may be claimed. |
| 3 | dynamical standing wave | The attractor is static; no derived wave equation on the ring exists | "Standing wave" is a static normal-mode statement; a dynamical evolution would be new physics. |
| 4 | "2π derived from closure" | $\pi$ is a boundary constant (Ch2, QG291); Bekenstein $1/4$ requires imported $2\pi$ (QG185, QG196) | The closure-periodicity observation is limited to the spectral layer and does NOT resolve the Bekenstein boundary. |
| 5 | assumed spatial center/source | The attractor is a centerless circulant ring | The only defensible "source" is the branching root in generation space; ladder radii are derived radial coordinates. |
| 6 | pre-existing spatial arena | Spacetime is emergent ($g=\rho^{2/d}\eta$); no background arena (Ch2, Ch10) | The uniform background is a counting reference, not a pre-existing spatial medium with time. |

None of these is a contradiction of the re-reading *per se*; each is a constraint the
re-reading must satisfy. The audit adopts all six.

---

## 6. Possible Derivation Chain (explanatory re-reading)

The proposed chain re-labels the canonical hierarchy; every node maps to an existing
canonical object, and no node adds content.

```
Difference  =  localized count deviation from the uniform background
                 (Q-event = unit; zero mode = the uniform rest state;
                  positive modes = the deviations)
    ↓
Actualization  =  spreading of the count through the generation tree
                 (Galton–Watson branching, μ^k path multiplicity,
                  ρ_k = μ^k/S; generation index = branching time)
    ↓
converged attractor  =  the closed medium
                 (C96(±1..±6), content-independent fixed point, ring)
    ↓
spectral decomposition  =  normal-mode (standing-wave) content of the ring
                 (λ_k = 2Σ(1−cos 2πdk/96); ω_k = √λ_k;
                  Z2 doublets = ±k ring degeneracy;
                  octave bands [4,4,87]; span = 6.40 octave span)
    ↓
resonance structure  =  Conservation + Boundary
                 (degeneracy groups [42×2,5,6], spectral gaps,
                  the operator/lock layer reading the mode groups)
    ↓
physics readouts  =  unchanged
                 (radii/ladder, phase lattice θ_k = 2πk/96,
                  all masses, couplings, mixings, cosmological fractions)
```

The chain is explanatory: it demonstrates that the canonical hierarchy *admits* the wave
vocabulary at every level, but the derivation order remains canonical
(Difference → Actualization → Spectrum → Physics), not
(disturbance → propagation → standing waves → observables).

---

## 7. Open Problems

1. **Traveling wave vs. static modes.** Is there any canonical statement of *dynamical*
   propagation on the attractor ring, or are only static normal modes available? A derived
   traveling-wave statement would likely require new dynamics — out of scope, but worth
   establishing explicitly as absent.
2. **Spectral $2\pi$ vs. Bekenstein $2\pi$.** What precisely is the relationship between the
   circle constant appearing in $\lambda_k=2\sum(1-\cos 2\pi dk/96)$ (ring-closure
   periodicity) and the $2\pi$ in the Bekenstein/Hawking factor $T=\kappa/(2\pi)$? Can the
   Bekenstein quarter be revisited on the basis of the spectral-layer $2\pi$ without
   contradicting QG196? (QG196 remains the canonical impossibility result; the audit does not
   overturn it.)
3. **Nature of the ladder radius.** Are the ladder radii $6.0$–$17.333$ a *derived radial
   coordinate* in an emergent space, or a combinatorial index of the rung structure? If a
   natural radius exists, can circumference/area relations ($2\pi r$, $\pi r^2$) be derived
   consistently without re-importing $\pi$ as a new input?
4. **Node structure of the ring modes.** Standing waves on a ring have no fixed boundary;
   the zero crossings of each mode depend on $k$. Does the node structure of the D96 modes
   correspond to any canonical partition (octave bands, sector labels)? (Candidate
   observation, not a claim.)
5. **Green's-function kernel.** Would the graph heat/wave kernel
   $\sum_k e^{-\lambda_k t}\phi_k\phi_k^\dagger$ reproduce the canonical null-geodesic
   propagation law ($n=1$) if a disturbance were expanded in the eigenbasis? The kernel is
   mathematically natural but not canonical; deriving propagation from it would be new work
   and must not be assumed.
6. **Ring degeneracy and weak isospin.** The Z2 doublet pairing = $\pm k$ ring-mode
   degeneracy supports the weak-isospin doublet structure (QG153–155) as a ring
   degeneracy. Is this a strengthening of the existing doublet-origin result, or merely a
   parallel description? (Supporting interpretation only; no claim-status change.)
7. **Wave vocabulary vs. count vocabulary.** Does adopting the wave vocabulary risk
   re-introducing a spatial-medium picture (an arena) that canonical AT excludes? The audit
   recommends the wave vocabulary be used only as a formal re-reading with the six scoping
   constraints of Section 5 always stated.

---

## 8. Summary and Recommendation

**Summary.**

1. Difference admits a localized-disturbance reading as a *configuration* (Q-event, compact
   deviation of $\rho$), not as the primitive's definition (Q1: COMPATIBLE).
2. Actualization is propagation of count in *generation space* (branching, $\mu^k$ paths),
   not spatial medium propagation; spatial propagation is null-geodesic, $n=1$, conformally
   invariant (Q2: PARTIAL).
3. The Inevitable Spectrum is exactly the standing normal-mode (resonance) content of the
   closed ring; this is an explanation, while the canonical derivation remains
   attractor → Laplacian → spectrum (Q3: COMPATIBLE AS EXPLANATION).
4. There is no spatial center; the defensible source is the branching root, and the ladder
   radii are derived radial coordinates (Q4: PARTIAL).
5. D96 is a standing-wave (normal-mode) solution of the ring, with the Z2 doublets the
   $\pm k$ ring degeneracy (Q5: YES, static).
6. Circular topology and circular phase order emerge; circular propagation dynamics does
   not yet (Q6: PARTIAL).
7. The spectral layer already uses $2\pi$ as the ring-closure periodicity constant; this
   does not resolve the Bekenstein boundary (Q7: OBSERVATION).
8. Radius, circumference, wavelength, frequency, and resonance are already implicit in the
   canonical structures (Q8: YES).

**Recommendation.**

The wave re-reading is *coherent*: every wave concept maps onto an existing canonical object,
and the standing-wave identity for the D96 spectrum is exact. The audit recommends:

- treat ResearchY-A as an open explanatory track, not a derivation program;
- adopt the wave vocabulary only with the six Section-5 scoping constraints stated;
- treat the spectral-layer $2\pi$ observation and the ring-degeneracy reading of weak isospin
  as candidate observations for future work, not as new claims;
- record open problems 1–7 for future tracks (e.g., ResearchY-B could examine the
  Green's-function kernel and the node structure).

**Boundary statement.** Nothing in this audit modifies canonical AT V2.0, adds a primitive,
changes an observable, or alters any claim status.

---

## References

- Monograph V2.0: Ch1 (Difference), Ch2 (Tensor Reference η), Ch3 (Actualization), Ch5
  (Inevitable Spectrum), Ch6 (D96 Spectrum), Ch7 (Operator Basis), Ch8 (Lock Law), Ch9
  (Quantum Mechanics), Ch10 (Gravity and Spacetime), Ch11 (Standard Model), Ch12 (Cosmology).
- AT-QG Phase 116 (Actualization Structures); 121/128 (Ladder Radii, Sector Ladder);
  153–155 (Z2 doublets, D96 symmetry); 159/160 (D96 selection, period-3 seed); 210–212
  (family index, light propagation); 216 (Quantum Amplitude Origin, $\rho_k=\mu^k/S$);
  218 (Hilbert Origin, magnitude + phase); 185/196 (Bekenstein quarter, impossibility);
  222 (Native Metric Dynamics); 291 (Framework Necessity, $\pi$ boundary);
  295 (Spectrum Necessity).
- MONO_PHASE001 (state-phase lattice vs. mixing/CP phases).
- MONO_PHASE002 ($\mu^k$ path multiplicity clarification).
- ATQG_ClaimClassificationRegistry.md (claim-status source of truth).
