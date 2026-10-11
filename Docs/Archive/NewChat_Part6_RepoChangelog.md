# AT Project Memory — Archive Part 6: Repository Changelog

Historical detail split out of `Docs/NewChat_Start.md` (2026-10-11), moved **verbatim**: the "Latest Repo Sync" entries
from the other machine, the V2.0 monograph editorial density-reduction passes, and the architectural build-dependency
decision (ImageSharp -> SkiaSharp).

Current state: `Docs/NewChat_Start.md`.

---
## Build Dependency — ImageSharp replaced by SkiaSharp (2026-09-13, architectural)

**RELEASE BUILDS WERE IMPOSSIBLE AND NOTHING SAID SO.** `SixLabors.ImageSharp` 4.1.0's build targets run a licence
check before `CoreCompile` with `ContinueOnError="$(Configuration.StartsWith('Debug'))"` - so it **hard-failed
every Release build** while Debug built fine, and because `dotnet test` was habitually run in Debug the failure was
invisible for the whole session.

- **Decision:** ImageSharp was removed and replaced by **SkiaSharp 3.119.0** (MIT, no licence check). Release now
  builds clean - verified.
- **Coupling removed, not just swapped.** SkiaSharp is confined to one internal helper (`AT.Core/AtBitmap.cs`);
  the plotter APIs (`RARPlotter.Series`, `ImageMapExporter`) now take a library-neutral `AtColor`
  (`AT.Core/AtColor.cs`). ~20 audit classes reference only that type, so the backend can change again without
  touching a single caller.
- **Guard added.** Every other image-producing test is FITS-data-gated and SKIPS on a clean checkout, which is why
  a broken backend went unnoticed. `AT.Tests/Unit/RenderingBackendTests.cs` (4 tests) runs unconditionally:
  valid PNG signature, IHDR dimensions, non-empty IDAT, and drawn colours deterministically reaching the raster.
- **Rule:** validate in the configuration you ship. A Debug-only test habit hid a Release-only hard failure.

## Latest Repo Sync

Recent commits from the other machine:

- `f8a4ded` VALID001 resolved the remaining SM observable gaps in the coverage record.- `8c9d707` updated the Coverage Audit page to match the newer validation state.
- `e19c066` completed MONO007 referee-readiness cleanup and refreshed the coverage assets.
- V2.0 monograph: Ch13 (Boundaries of The Actualization Theory) and Ch14 (Frontier and
  Falsification) theorem-block density reduced (Ch13: 21 → 10 named blocks; Ch14: 22 → 10):
  definitions/remarks/corollaries folded into the surviving theorems/lemma as prose, corollary
  proofs merged into theorem proofs, summaries compressed. Protected labels
  c13:chap:boundaries, c13:cor:no-open-validation, c13:thm:bekenstein-2pi, c13:thm:sm-hosted
  retained (c14 has no cross-chapter refs; all c13:sec:*/c14:sec:* labels kept). All claims,
  values (S=A/(8π), T=κ/(2π); S=0.0421/T=0.0842/U=0, a_e=1.159655e-3, mββ=2.02e-3 eV /
  2.02 meV, P1 106.39 GeV window 99–114, P3 nine resonances 106.39→263.43 GeV 15.20 GeV
  width, 151.98 ≈ 152 GeV excess), citations, and thebibliography untouched; no broken refs
  (verified, clean pdflatex build).
- V2.0 monograph: Ch13/Ch14 second editorial pass — text compressed a further ~13-15%
  (Ch13 ~15% raw / Ch14 ~13% raw): intro boilerplate ("no new physics and no speculation")
  reduced to a single statement per chapter; "We have proved" summary lists replaced by
  2-3 sentence syntheses; c13:thm:difference-ontological and c14:thm:temporal-evidence
  demoted to prose paragraphs (labels retained, \cite{QG278} / \cite{QG190,QG202,QG299}
  kept); claims/validation/boundary partition stated once in the classification section;
  one MONO007 referee-safe hedge per chapter. All equations (S=A/(8π), T=κ/(2π)), values
  (S=0.0421/T=0.0842/U=0, a_e=1.159655e-3, mββ=2.02e-3 eV / 2.02 meV, P1 106.39 GeV
  window 99–114, P3 106.39→263.43 GeV 15.20 GeV, 151.98≈152 GeV, 0.036–0.156 eV window,
  nEXO/LEGEND-1000, HL-LHC), failure conditions (P1 no signal in 99–114 = fail; P2 upper
  limit <2.02 meV = fail; P3 sensitive search excluding any frozen rung = fail), the four
  non-claims, all \cite keys, section labels, and thebibliography untouched; LOST/ADDED
  cite keys empty, no broken refs (verified, clean pdflatex build, 93 pages).
- V2.0 monograph: Ch11/Ch12 theorem-block density reduced ~35-40% (all definitions/corollaries
  folded into surviving theorems as prose; every claim, equation, value, and citation preserved).
- V2.0 monograph: Ch7 (Operator Basis) and Ch8 (Lock Law) theorem-block density reduced
  (~58% / ~55% of named blocks removed): Ch7 defs/cors/lemmas/remark folded into surviving
  theorems and prose (4-operator summary table added; the four operator theorems, universality,
  completeness, single-layer theorems retained); Ch8 defs/cors/lemmas/remarks folded into the
  kept theorems. Protected labels c07:thm:operators-derived, c07:cor:operators-not-primitive,
  c07:thm:crowding-moments, c07:thm:compression-occupancy, c07:thm:beat-span, c07:thm:locking-gap,
  c07:thm:operators-universal, c07:thm:four-operators, c07:thm:no-fifth, c07:thm:single-operator-layer,
  c08:thm:locks-locking, c08:thm:lock-law-universal, c08:thm:lock-values-domain, c08:thm:locks-precede,
  c08:thm:phase-transition, c08:thm:blind-prediction, c08:thm:class-not-strength, c08:thm:lock-origin,
  c08:thm:lock-scoped retained. All claims, values (95/64.08/229/1900.25/6.40, g*=0.31, lock
  20.0026/2.4105/8.2980, all domain lock-value sets), citations, and thebibliography untouched;
  no broken refs (verified).
- V2.0 monograph: Ch1 (Difference) and Ch2 (Tensor Reference η) theorem-block density reduced
  (~39% / ~52% of named blocks removed): lemmas/corollaries/remarks folded into surviving
  theorems and prose; theorems retained (minimal-primitives, fundamental-boundary, duality,
  closure, spectrum-inevitable in Ch1; eta-necessary, conformal, tensor-read,
  minimal-foundation, eta-primitive-pi-boundary, reference-safety in Ch2). Protected labels
  c01:chap:difference, c01:cor:closure-characterizes, c01:def:difference, c01:thm:duality,
  c01:thm:fundamental-boundary, c01:thm:minimal-primitives, c01:eq:canonical-core,
  c01:eq:decomposition, c02:chap:eta, c02:thm:conformal, c02:thm:eta-primitive-pi-boundary,
  c02:thm:minimal-foundation, c02:thm:tensor-read, c02:eq:metric-ansatz, c02:eq:decomposition,
  c02:eq:core retained. All claims, equations, numerical values (N=96, d=3 six components,
  95 positive modes, S=A/4, ΩΛ/Ωm, ns), citations, and thebibliography untouched; no broken
  refs (verified, clean pdflatex build).

## Monograph V2.0 — Editorial Density Reduction (Ch5/Ch6)

Decision (publication editorial, no physics change): the V2.0 monograph chapters
Chapter05_InevitableSpectrum.tex and Chapter06_D96Spectrum.tex were compressed a second
time (Ch5 2044 → 1679 words, Ch6 1892 → 1587 words; −16 to −18%). Every equation,
numerical value (Σm=95, Σ√m=64.08, Σm²=229, occMom=1900.25, span=6.40, [42×2,5,6],
42·2²+5²+6²=168+25+36=229), \cite key, and conclusion was preserved; all equation
environments are byte-identical and the per-file \cite-key set is unchanged. The
"canonical end-state / no new physics and no speculation" boilerplate was deleted from
both intros; the enumerated "We have proved" summaries were replaced by synthesis
paragraphs; the un-referenced Theorem c05:thm:spectrum-unique was merged into prose
(uniqueness follows from attractor uniqueness) and its internal \refs updated; the
"necessary" claim in Ch5 was folded into one prose sentence; "emergent" in Ch6 was
reduced 8× → 2×. Protected labels (c05:chap:spectrum, c05:thm:core-completed,
c05:thm:spectrum-eigenspectrum, c05:thm:spectrum-not-primitive, c05:eq:core; c06:chap:d96,
c06:cor:constants-derived, c06:cor:moment-ladder, c06:thm:d96-derived, c06:thm:multiplicity,
c06:thm:occmom, c06:thm:organization-derived, c06:thm:span, c06:eq:moment, c06:eq:occmom,
c06:eq:span) and all section labels survive. main.pdf compiles cleanly (94 pages, no
undefined refs).

## Monograph V2.0 — Editorial Density Reduction (Ch9/Ch10)

Decision (publication editorial, no physics change): the V2.0 monograph chapters
Chapter09_QuantumMechanics.tex and Chapter10_GravityAndSpacetime.tex were restructured to
reduce theorem-style block density. Definitions, corollaries, lemmas, and remarks were
folded into their parent theorems; every scientific claim, equation, numerical value,
citation, and conclusion was preserved, and the referee-safe MONO007 wording was retained
as prose inside the merged theorems. Protected labels c09:lem:qm-dynamics (contains the
Lagrangian) and c10:thm:bekenstein-boundary (plus c10:thm:mass-radius, c10:thm:frame-dragging,
c10:lem:gps-redshift) survive unchanged. Block counts: Ch9 20 → 8, Ch10 21 → 9. All
equation labels (c09:eq:amplitude, c09:eq:phase, c09:eq:interference, c10:eq:mpl,
c10:eq:metric, c10:eq:frame-dragging) and section labels preserved. main.pdf compiles
cleanly (106 pages, no undefined refs).

## Monograph V2.0 — Editorial Density Reduction (Ch7/Ch8, second pass)

Decision (publication editorial, no physics change): Chapter07_OperatorBasis.tex and
Chapter08_LockLaw.tex were compressed a second time (Ch7 2032 → 1675 words, Ch8 1936 →
1640 words; −17.6% / −15.3%). Every equation, numerical value (95, 64.08, 229, 1900.25,
6.40, λ2; g*=0.31; lock1=20.0026, lock2=2.4105, lock3=8.2980; all 28 domain lock values),
\cite key, and conclusion was preserved; the per-file \cite-key set is unchanged. The
intro boilerplate was deleted and its cites merged (c07:QG260 folded into the first
intro cite); each summary enumerate was replaced by a synthesis paragraph; each of the
four Ch7 operator sections is now one compact theorem + one-sentence proof (echo
sentences deleted); Ch8 predictive-consequences and validation-status sections were
merged to one theorem + prose each (labels c08:thm:class-not-strength, c08:thm:lock-scoped
removed — self-contained, un-referenced); repeated "universal structure, domain-specific
values" restatements reduced to one; referee-safe scope disclosures reduced to one per
chapter (MONO007 retained once per file); the Ch8 domain values are now presented as a
table. Protected labels (c07:chap:operators, c07:cor:operators-not-primitive,
c07:thm:operators-derived, c07:thm:crowding-moments, c07:thm:compression-occupancy,
c07:thm:beat-span, c07:thm:locking-gap) and all section labels and thebibliography
blocks survive. main.pdf compiles cleanly (94 pages, no undefined refs).

## Monograph V2.0 — Editorial Density Reduction (Ch11/Ch12, second pass)

Decision (publication editorial, no physics change): Chapter11_StandardModel.tex and
Chapter12_Cosmology.tex were compressed a second time (Ch11 1801 → 1546 prose words,
Ch12 1700 → 1464 prose words; −14.2% / −13.9%, within the ~12–14% band). Every equation
(c11:eq:couplings, c11:eq:higgs, c11:eq:oblique, c11:eq:electron-g2, c11:eq:mbb —
byte-identical), numerical value (S=0.0421, T=0.0842, U=0, T=2S exactly, 5.3%;
a_e=1.159655e-3, Δa_e=1.86e-13, 0.0003%; mββ=2.02e-3 eV, 48/95 modes; V_us=#d/(2Σm),
V_cb=(ω0/ω2)^δd, V_ub=2V_cb·(occ0/occ2), 0.58%; θ12=33.35°, θ23=49.72°, θ13=8.34°,
δν=66.4°, 1.5%; Δm²21=7.607e-5, Δm²31=2.44e-3, m2=8.72 meV, m3=49.4 meV, Σmν=0.058 eV;
v=254 GeV, M_H=125.25 GeV, 0.003%; ΩΛ=0.6839, Ωm=0.3161, I_occ=0.7513 nats, [4,4,87],
95 modes, ℓ1=220.48, r21=2.4368, r31=3.6965, n_s=1, Var(Z_k)=kσ²), \cite key, and
conclusion was preserved; the per-file \cite-key set is unchanged (verified LOST/ADDED
empty). The "canonical end-state / no new physics and no speculation" boilerplate was
deleted from both intros; the enumerated "We have proved" summaries were replaced by
2–3-sentence synthesis paragraphs; the Ch11 precision-prediction proofs were tightened
(every number kept; "derived, not fitted" retained once) and the electron-g-2 proof's
"anomaly-free" wording aligned with the theorem's "anomaly below 10⁻¹²"; in Ch12,
"emergent" was reduced 16× → 2× (prose occurrences; the c12:thm:cosmo-emergent label/ref
identifier is untouched), the Consequences remark that repeated the intro scope statement
was merged into the intro (c12:MONO007 kept; the "initial-condition and information
origins replace the inflation epoch" claim preserved), and the acoustic theorem's
self-repeating final sentence was removed. Protected labels (c11:chap:sm,
c11:remark:hosted-sm; c12:chap:cosmology, c12:cor:canonical-cosmology with its
Difference → Actualization → Spectrum → Cosmology display) and all section labels and
thebibliography blocks survive. main.pdf compiles cleanly (93 pages, no undefined refs).
