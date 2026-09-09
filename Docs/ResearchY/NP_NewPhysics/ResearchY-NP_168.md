# ResearchY-NP_168 — Yang–Mills Existence and Mass Gap Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_168 (permanent)
**Title:** Yang–Mills Existence and Mass Gap Audit
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `NP_NewPhysics/ResearchY-NP_168.md`
**Depends on:** QG104 (finite causal-network spectra), QG161 (gauge-sector origin), QG243
(generator-action dynamics), QG244 (Lagrangian structure), ResearchY-D_009 (D96 minimum
excitation), D_040 (canonical classification registry)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_168_Tests.cs`

---

## Purpose and scientific target

Determine the strongest result Actualization Theory (AT) currently establishes about the
Yang–Mills existence and mass gap problem, without identifying a finite graph eigenvalue with a
physical particle mass.

The Clay target is a mathematically rigorous, non-trivial quantum Yang–Mills theory for a compact
simple gauge group on four-dimensional Euclidean space \(\mathbb R^4\), satisfying sufficiently
strong quantum-field-theory axioms, together with a positive spectral gap \(\Delta>0\) above the
vacuum in the physical gauge-invariant sector. For \(SU(3)\), the massive excitations are
gauge-invariant states such as glueballs—not eigenmodes of an unrelated graph Laplacian.

This audit separates that target from two real AT results:

1. **QG244:** the classical/minimal gauge Lagrangian *structure*
   \[
   \mathcal L=-\frac14F^a_{\mu\nu}F^{a\mu\nu}
   +i\bar\psi\gamma^\mu D_\mu\psi-m\bar\psi\psi
   \]
   is claimed from the D96 generator algebra and generator action.
2. **QG104 / D96:** finite network Laplacians have computable discrete spectra and positive
   finite-size gaps.

Neither statement is, by itself, a construction of interacting quantum Yang–Mills theory.

---

## Assumptions and conventions

1. The canonical D96 attractor is the unweighted circulant \(C_{96}(1,\ldots,6)\).
2. For the refinement family \(C_N(1,\ldots,6)\),
   \[
   \lambda_k(N)=2\sum_{d=1}^{6}
   \left[1-\cos\left(\frac{2\pi d k}{N}\right)\right],
   \qquad \omega_k(N)=\sqrt{\lambda_k(N)}.
   \]
3. “Lowest nonzero spacing” means the separation from the zero mode to the lowest positive
   eigenvalue/frequency. The \(k=1\) and \(k=N-1\) modes form a degenerate pair.
4. All results are dimensionless graph quantities. No physical lattice spacing, energy unit, or
   glueball scale is guessed.

---

## 1. Exact finite-\(N\) result

Scanning all \(k=1,\ldots,95\) gives the minimum at \(k=1,95\):

\[
\boxed{\lambda_{\rm gap}(96)=0.386350893377790}
\]

\[
\boxed{\omega_{\rm gap}(96)=\sqrt{\lambda_{\rm gap}}
=0.621571309969974}.
\]

The multiplicity is 2. This is a genuine, reproducible property of the finite D96 graph. It is the
same finite-network fact used by D_009, now retained at full precision.

**Classification:** the D96 finite-network kinematic gap is **DERIVED**.

---

## 2. Deterministic refinement and scaling

For fixed step set \(\{1,\ldots,6\}\), the small-angle expansion gives

\[
2(1-\cos x)=x^2+O(x^4),
\]

so

\[
\lambda_{\rm gap}(N)
=\frac{4\pi^2}{N^2}\sum_{d=1}^{6}d^2+O(N^{-4})
=\frac{4\pi^2\cdot91}{N^2}+O(N^{-4}).
\]

The analytic leading coefficient is

\[
4\pi^2\sum d^2=4\pi^2\cdot91\approx3592.536001996526.
\]

| \(N\) | \(\lambda_{\rm gap}\) | \(\omega_{\rm gap}\) | \(N^2\lambda_{\rm gap}\) | \(\lambda(N)/\lambda(N/2)\) |
|---:|---:|---:|---:|---:|
| 48 | 1.504528509127226 | 1.226592234251965 | 3466.433685029 | — |
| 96 | 0.386350893377790 | 0.621571309969974 | 3560.609833370 | 0.256792005624 |
| 192 | 0.097236577501828 | 0.311827801040620 | 3584.529193027 | 0.251679442622 |
| 384 | 0.024349858377839 | 0.156044411555937 | 3590.532716963 | 0.250418710771 |
| 768 | 0.006090011735292 | 0.078038527249635 | 3592.035081757 | 0.250104605981 |
| 1536 | 0.001522662169456 | 0.039021304046077 | 3592.410765750 | 0.250026147016 |

Thus the unscaled graph-Laplacian gap is \(O(N^{-2})\) and tends to zero; the frequency gap is
\(O(N^{-1})\) and also tends to zero. The observed factor approaches \(1/4\) when \(N\) doubles,
as required by \(N^{-2}\) scaling.

---

## 3. Scaling conventions do not rescue the Clay claim

Two limits must not be conflated.

### A. Fixed lattice spacing / growing circumference

If graph spacing is held fixed, the circumference grows as \(L=Na\). Then
\(\lambda_{\rm gap}/a^2\to0\). This is the relevant kinematic behavior for an expanding-volume
sequence: there is no uniform positive graph-Laplacian lower bound.

### B. Fixed compact circumference

If \(L\) is fixed and \(a=L/N\), scaling by \(1/a^2\) gives

\[
\frac{\lambda_{\rm gap}}{a^2}
\longrightarrow \frac{4\pi^2\cdot91}{L^2}.
\]

After normalizing the stencil by \(\sum d^2=91\), this is the ordinary compact-circle Laplacian gap
\((2\pi/L)^2\). It exists because a compact domain has a minimum nonzero momentum. It is:

- kinematic, not dynamically generated;
- finite-volume, not an infinite-volume result;
- a site-function Laplacian gap, not a gauge-invariant Hamiltonian gap;
- one-dimensional circulant behavior, not a four-dimensional Yang–Mills construction.

Therefore compact-domain rescaling is useful for a regulator interpretation but cannot prove the
Clay mass gap.

---

## 4. Clay-requirement audit

| Requirement | Current AT evidence | Status |
|---|---|---|
| rigorous quantum Hilbert space or Euclidean measure | Complex amplitudes and Born structure exist elsewhere in AT, but no interacting Yang–Mills Hilbert-space/measure construction satisfying Wightman or Osterwalder–Schrader-strength axioms is given | **MISSING** |
| gauge constraints and physical observable sector | D96 generator labels and a gauge-covariant form are claimed; no Gauss-law constraint, quotient by gauge redundancy, BRST-equivalent construction, or rigorous gauge-invariant observable algebra is built | **MISSING** |
| interacting Hamiltonian or transfer matrix | QG244 writes the minimal Lagrangian form; it does not construct a self-adjoint interacting Hamiltonian or positive transfer matrix | **MISSING** |
| regulator and refinement limit | \(C_N(1,\ldots,6)\) supplies a deterministic graph refinement family, but no four-dimensional gauge-link regulator, renormalization flow, or convergence proof connects it to interacting Yang–Mills | **MISSING** |
| infinite-volume \(\mathbb R^4\) limit | D96 is a fixed finite ring; no thermodynamic/infinite-volume four-dimensional limit is constructed | **MISSING** |
| uniform positive physical lower bound | The unscaled graph gap vanishes as \(N^{-2}\); no \(N\)- and volume-uniform lower bound for gauge-invariant Hamiltonian excitations is proved | **MISSING** |

Even a construction for one gauge group would still require these steps. The official Clay statement
asks for any compact simple gauge group; AT currently discusses specific \(SU(2)\) and \(SU(3)\)
structures rather than a general construction.

---

## 5. Why QG244 is not quantization

QG244 establishes its claimed endpoint at the level of a Lagrangian *form*: generator algebra,
field-strength expression, covariant derivative, and minimal action density. That can be useful
classical input to quantization. It does not itself define:

- the interacting state space;
- a gauge-fixed or gauge-invariant measure;
- physical-state constraints;
- operator domains and a self-adjoint Hamiltonian;
- reflection positivity / reconstruction;
- continuum and infinite-volume limits.

The audit therefore does **not** reclassify QG244’s narrower structural claim. It rejects the stronger
inference “a Yang–Mills-shaped Lagrangian has been written, therefore quantum Yang–Mills exists.”

---

## 6. Existing “mass-gap scale” nomenclature

QG161, QG169, QG171, QG246, QG260, QG262, and QG300 already call the same D96 value
\(\lambda_2=0.386351\) a “mass-gap scale” or propagate that label through scalar/Higgs, muon
\(g-2\), resonance, lepton/Yukawa, and operator-universality formulas. For example, QG169 uses it
as a dimensionless factor in \(M_H=v\sqrt{\lambda_2g_2}\), while QG171 uses
\(\lambda_2/\sum m\) as a correction to \(a_\mu\). NP_168 does not erase those formulas or
reclassify their local claims. It establishes the necessary sector-independent scope restriction:

- the value used there is numerically the same finite graph-Laplacian gap;
- “mass-gap scale” is AT graph/operator nomenclature, not an independently constructed pure
  Yang–Mills Hamiltonian spectrum;
- multiplying that dimensionless graph value by another AT scale in a Higgs-sector ansatz does not
  provide the Clay quantum construction or a glueball bound;
- any use of the phrase as evidence that the Clay mass-gap requirement is solved is **REFUTED**.

Correcting every historical wording instance would change the scope of prior QG phases and is not
done by this audit. Their scientific interpretation is restricted here.

---

## 7. Graph gap versus Yang–Mills mass gap

| D96 graph gap | Clay Yang–Mills mass gap |
|---|---|
| eigenvalue of a finite combinatorial Laplacian | energy difference in a quantum Hamiltonian spectrum |
| acts on functions/modes on graph vertices | acts on physical gauge-invariant quantum states |
| positive on every finite connected graph | positivity must survive regulator removal and infinite volume |
| depends on graph size and normalization | physical, dynamically generated scale |
| tends to zero under unscaled refinement | must obey a uniform \(\Delta>0\) above the vacuum |
| no glueball operator or correlator | can be probed through gauge-invariant operators/glueball states |

Equating the two is a category error.

---

## 8. Strongest honest result and adversarial attack

### Strongest result

AT has:

1. a claimed minimal classical Yang–Mills Lagrangian scaffold (QG244);
2. exact finite-network spectral machinery (QG104 and D96);
3. a DERIVED positive D96 graph gap at \(N=96\);
4. an explicit deterministic refinement family exposing the required limit question.

Together these form a **plausible regulator scaffold**, classified **EMERGENT** as a research
direction—not a completed regulator and not a proof.

### Hostile tests

1. **“Positive at \(N=96\) proves a mass gap.”** Refuted: every finite connected graph has a
   positive Laplacian gap, and this one vanishes as \(N^{-2}\).
2. **“Multiply by \(N^2\), so the gap survives.”** Refuted as a Clay inference: that selects a
   fixed compact-volume normalization. It does not supply an interacting quantum spectrum or an
   infinite-volume bound.
3. **“QG244 supplies Yang–Mills, so quantization is automatic.”** Refuted: writing a classical
   action is not a non-perturbative quantum construction.
4. **“The graph mode is a glueball.”** Refuted: no gauge-invariant composite operator, physical
   Hilbert sector, or Hamiltonian identifies it as one.
5. **“D96’s fixed finite size avoids the limit.”** Refuted as a Clay solution: avoiding the
   regulator-removal and \(\mathbb R^4\) limits avoids the problem’s central requirement.
6. **“Attach a physical energy scale to \(0.62157\).”** Rejected: AT supplies no justified scale
   map here; importing one would be a guess.
7. **“Several QG phases already say mass-gap scale.”** Refuted as Clay evidence:
   QG161/169/171/246/260/262/300 reuse the finite graph \(\lambda_2\) across several observable
   formulas; none constructs a gauge-invariant pure-Yang–Mills Hamiltonian spectrum.

All hostile attacks survive. No uniform positive physical mass bound is established.

---

## Theorem

> **Theorem (NP_168).** For the AT circulant family \(C_N(1,\ldots,6)\), the finite D96 graph has
> the exact numerical kinematic gaps
> \(\lambda_{\rm gap}(96)=0.386350893377790\) and
> \(\omega_{\rm gap}(96)=0.621571309969974\), while
> \[
> \lambda_{\rm gap}(N)
> =4\pi^2(91)N^{-2}+O(N^{-4})
> \]
> and hence the unscaled gap tends to zero under refinement. A fixed-compact-domain \(1/a^2\)
> scaling yields only the expected finite-volume continuum Laplacian gap. Current AT does not
> construct the quantum Hilbert/measure, physical gauge-invariant sector, interacting
> Hamiltonian/transfer matrix, controlled continuum limit, infinite-volume \(\mathbb R^4\) limit,
> or a uniform positive physical spectral bound required by the Clay problem. Therefore the
> finite-network gap is DERIVED, a regulator-scaffold reading is EMERGENT, and the claim that
> current AT solves the Yang–Mills existence and mass gap problem is REFUTED. QG244’s narrower
> classical/minimal Lagrangian-structure claim and all canonical D-series classifications remain
> unchanged. ∎

---

## 9. Falsification paths

| Current finding | What would overturn or strengthen it |
|---|---|
| quantum Yang–Mills construction is MISSING | construct a non-trivial interacting gauge theory from AT with a rigorous Hilbert space or Euclidean measure and the required QFT axioms |
| physical gauge-invariant sector is MISSING | implement gauge constraints and construct the gauge-invariant observable/state sector |
| interacting dynamics is MISSING | construct a self-adjoint interacting Hamiltonian or positive transfer matrix, not only a classical Lagrangian form |
| controlled limits are MISSING | prove regulator removal and a four-dimensional infinite-volume \(\mathbb R^4\) limit |
| uniform positive physical gap is MISSING | prove an \(N\)-, regulator-, and volume-uniform \(\Delta>0\) for gauge-invariant excitations/correlators |
| regulator scaffold is only EMERGENT | supply a four-dimensional gauge-link refinement whose measure, observables, and dynamics converge |
| D96 graph gap is not a glueball gap | derive a gauge-invariant composite operator and show its Hamiltonian/correlator spectrum is controlled by the D96 eigenvalue through the required limits |
| current AT Clay-solution claim is REFUTED | completing all construction and bound rows above would require a superseding audit |

---

## 10. Classification

| Component | Status |
|---|---|
| finite \(C_{96}(1,\ldots,6)\) graph-Laplacian gap | **DERIVED** |
| \(\lambda_{\rm gap}=O(N^{-2})\), \(\omega_{\rm gap}=O(N^{-1})\) | **DERIVED** |
| fixed-compact-domain rescaled continuum gap | **DERIVED kinematic result** |
| D96/QG244 as a possible regulator scaffold | **EMERGENT** |
| interacting 4D quantum Yang–Mills construction | **MISSING** |
| physical gauge-invariant/glueball mass gap | **MISSING** |
| “AT currently solves the Clay problem” | **REFUTED** |

**Canonical registry decision:** no update to
`Y_D_040_Tests.ClassificationRegistry` is warranted. NP_168 does not reclassify a canonical
D-series object; it restricts the physical inference that may be drawn from the already-DERIVED
finite graph spectrum.

---

## 11. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_168_Tests.cs`

| Test | Verifies |
|---|---|
| `Y_NP_168_D96Gap` | full-precision \(\lambda\), \(\omega\), and multiplicity |
| `Y_NP_168_RefinementScaling` | deterministic \(N^{-2}\) convergence |
| `Y_NP_168_ScalingConventions` | compact-volume versus growing-volume limits |
| `Y_NP_168_QG244Scope` | Lagrangian structure is not quantum construction |
| `Y_NP_168_ClayRequirements` | all six required construction steps remain missing |
| `Y_NP_168_GapDistinction` | graph gap is not a gauge-invariant Hamiltonian gap |
| `Y_NP_168_ExistingNomenclature` | QG161/169/171/246/260/262/300 reuse the graph gap across sectors |
| `Y_NP_168_Classification` | DERIVED / EMERGENT / REFUTED verdict |
| `Y_NP_168_Run` | reproducible assumptions-first scientific report |

**Reproduction:**

```text
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_168"
```

---

## References

- Clay Mathematics Institute, *Yang–Mills & the Mass Gap*:
  <https://www.claymath.org/millennium/yang-mills-the-maths-gap/>
- `Docs/Research/ATQG_LagrangianOrigin.md` (QG244).
- AT-QG104, finite causal-network spectrum.
- ResearchY-D_009, D96 minimum excitation.
- ResearchY-D_040, canonical D-series classification registry.
