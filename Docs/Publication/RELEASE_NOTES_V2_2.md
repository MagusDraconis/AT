# Release Notes — The Actualization Theory, V2.2 New Physics Program

**Version:** 2.2.0 · **Audit lines complete:** 2026-09-13
**Status:** AUDIT LINES COMPLETE — **not yet tagged**, and **no Zenodo deposit exists for V2.2**.
The archived, citable monograph remains **V2.0** (DOI 10.5281/zenodo.20681734).

---

## Summary

V2.2 is the **New Physics** program, built on the V2.1 origin chain. It asked where testable new
physics could come from now that the origin chain was closed, and it answered — but not in the way
the program hoped.

**Its outcome is a closure, not a discovery.** The program proposed a lock-lattice hardware test,
demonstrated the mechanism inside a deterministic model, and then established that **no AT-specific
observable remains**: the predicted signature is generic across the controls. The
physical-realizability program was therefore **formally closed (NP_175, 2026-09-11)**.

In parallel, V2.2 extended the gravity/time group (G_021–G_036) and opened a new electromagnetism
group (E_001). Those extensions produced the sharpest results in the release, and three of them are
negative:

| finding | verdict |
|---|---|
| AT's **derived** spatial structure (the counting measure) is contradicted by the measured deflection of light | **REFUTED** (Cassini, 8.6957×10⁴ σ) |
| The spatial sector **cannot be derived** — it closes on a postulate | **NO-GO** (G_030) |
| The electromagnetic **dynamics is declared but never computed** | **BOUNDARY** (E_001) |

Plus one resolution and one closure:

| finding | verdict |
|---|---|
| **Light bending, resolved** — the spatial half is required; time dilation alone gives exactly half | **SETTLED** |
| The **clock law** and the **source law** are one statement | **CLOCK CLOSED** (G_028) |
| The temporal core is **independent** of the spatial sector and testable in principle | **SURVIVES / BOUNDARY** |

## What's in this release

### New Physics (NP) — 172 audits, NP_001–NP_175

- **Lock-lattice proposal** (NP_170) and **H1 in the deterministic model** (NP_171)
- **Retention null audit** (NP_172): the H2 signature is generic, not AT-specific
- **Discriminator audit** (NP_173): after a systematic search, **no AT-specific observable remains**
- **Program synthesis** (NP_174) and **formal closure** (NP_175, 2026-09-11) — status/archival labels
  only; **no canonical claim, value, equation or registry entry modified; no file deleted**

### Gravity and time (G) — G_021–G_036

- **G_028 — CLOCK CLOSED.** The physical clock law is `dτ/dt = ρ^(1/d)`, and the **source law is the
  clock law**: `a = −(1/d)∇ln ρ = −∇A`. The clock law is g₀₀-only.
- **G_029 — the spatial sector closes on a POSTULATE:** `g_rr = 2 − ρ^(2/d)` (equivalently
  `B = ½ln(2 − e^(−2x))`), which reproduces **γ = +1** and is **non-conformal** (A ≠ B).
- **G_030 — NO-GO.** No local `B(r) = F(σ)` survives all eight constraints; the spatial sector is
  **not derivable** from AT's primitives.
- **G_031 — conformal flatness IS the counting measure** (`√det g_ij = ρ`).
- **G_032 — the conformal ansatz is an ASSUMED primitive, and imposing it is REFUTED:** the derived
  conformal form gives **γ = −1 exactly** — zero light bending, zero Shapiro delay — excluded by
  **Cassini at 8.6957×10⁴ σ**.
- **G_033** — the D96³ requirement for gravity/time is **era-local** (PARTIAL): the density era
  computes with D96/D96³; the metric era imports 3D space instead of generating it.
- **G_034** — A0 robustness: exact-`double` keying replaced by **tolerance clustering**; every
  conclusion is UNCHANGED (the A0 dependence was a floating-point degeneracy artefact).
- **G_035 — TEMPORAL INDEPENDENCE:** classifying every group-G suite by the metric component it
  needs gives **25 need g₀₀ only, 9 need g_ij, 3 are the conformal assumption** (37 suites). The
  boundary is exactly **G_020 → G_021**, and the minimal time sector is one function + one scalar +
  one exponent.
- **G_036 — TEMPORAL CORE TEST (BOUNDARY):** the observable `z = 1/√(−g₀₀) − 1` is **pure**, and
  `Δz = 0.125628` at J0740+6620 (44.8 % of z_AT) — but the arena is **neutron stars only** and the
  compactness that reads the signal is itself obtained from light bending. The bottleneck is
  compactness precision (`σ_x/x` = 13.73 % against the 3.661 % needed), **not signal**.

### Electromagnetism (E) — new group

- **E_001 — Electromagnetism Inventory (BOUNDARY).** The **kinematics is derived** (U(1) as
  Z₉₆ ⊂ D96; the 1 + 3 + 8 = 12 gauge structure; charge as a topological invariant; emergent c =
  ℓ/τ; the substrate wave equation). The **dynamics is declared but never computed**: the Noether
  currents, `F^a_μν`, `−¼F²` and the full Lagrangian density all exist as members **returning
  strings**, and "derived" is asserted by ANDing other audits' booleans — while the program had
  already recorded the gauge dynamics as **HOSTED/OPEN**.
- **Missing:** the sourced Maxwell equation `∂_μF^μν = J^ν` (AT reaches only current *conservation*,
  which is kinematic) and any massless **spin-1** wave equation.
- **Unresolved:** the fine-structure constant has **two contradictory values** (137 from QG162 vs
  α⁻¹ ≈ 100, with α named AT's largest remaining free parameter), and **U(1) has two incompatible
  origins** (Z₉₆ automorphism vs vortex moduli space).

### Light bending — resolved

The measured deflection is `((1+γ)/2)·4GM/(c²b)`. The **spatial half is required**:

| spatial rule | γ | solar grazing deflection | Cassini |
|---|---|---|---|
| time-dilation only | 0 | 0.8750″ (exactly half) | **43,479 σ — excluded** |
| AT's derived conformal metric | −1 | 0.0000″ | **86,957 σ — excluded** |
| AT's surviving postulate (G_029) | **+1** | 1.7500″ | 1 σ — passes |

AT's survivor agrees with GR at O(x) and differs at O(x²): −2.7×10⁻¹¹ relative at solar compactness
(invisible, far below Cassini), but **−29.7 %** at neutron-star compactness x = 0.247002.

### Verdict discipline

- **G_027** — **no literal may reach a verdict.** Every classification must trace to computed
  evidence, enforced mechanically by a scanner that re-reads `AT.Core` at test time, because a
  literal-driven "computational experiment" and a hard-coded γ had each already produced a wrong
  result.
- **G_024–G_026** — three G-chain optics errors retracted, and the defect class found to be
  **systemic**: four QG audits carrying four contradictory test suites, all passing.

### Infrastructure

- **Release builds were impossible and nothing said so.** `SixLabors.ImageSharp`'s build targets ran
  a licence check with `ContinueOnError` true only for Debug, so every **Release build hard-failed**
  while Debug passed. Replaced with **SkiaSharp 3.119.0** (MIT, no licence check); Release now builds
  clean. The coupling was removed rather than just the package: the imaging backend is confined to one
  internal helper (`AtBitmap`) behind the library-neutral `AtColor`.
- **`AT.Tests/Unit/RenderingBackendTests.cs`** added — it runs unconditionally and asserts PNG
  signature, IHDR dimensions, a non-empty IDAT, and that drawn colours deterministically reach the
  raster. Every other image-producing test is FITS-data-gated and **skips** on a clean checkout, which
  is precisely how a broken rendering backend went unnoticed.
- **`AtSourceScan`** — a whole-file literal/comment stripper. The G_027/G_033/G_035 scanners stripped
  **per line**, so they could not see a verbatim (`@"..."`) string spanning lines; and since most
  report text in this repository is written in exactly such blocks, **prose could be counted as
  executable code**. Those three audits should be re-run with the fix.
- Root `README.md` rewritten for V2.2; `AtlasDataService.Version` = `"2.2.0"`.

## Verification

- **4,590 tests discovered** across 1,167 files in `AT.Tests` (group G 283/283; group E 7/7;
  `Unit` + `ResearchQG` 161 passed, 13 skipped as FITS-data-gated).
- **Release build clean** (0 errors), AT.App and AT.Book clean.
- Canonical D96 values unchanged; the D_040 classification registry untouched.

## Known issues

- **The full test suite hangs** in `D_ResonanceStructure` (`Y_D_022`, plus one legacy `Research`
  suite). Hang dumps in `AT.Tests/TestResults` date from **2026-09-01** — this predates V2.2 and is
  not caused by it. Run targeted filters until fixed.
- The 16 `T_SpectralBlueprint` suites are **not registered** in `Docs/ResearchY/ResearchY_Index.md`.

## Tag Recommendation

**Do not tag yet.** V2.1 carries a `READY FOR TAGGING` recommendation because its deliverable is a
*positive* closure (the origin chain complete, boundaries enumerated). V2.2's headline results are
**negative** — a refuted spatial structure, a no-go theorem, and a declared-but-uncomputed
electromagnetic dynamics. That is scientifically valuable and worth releasing, but two of the
negative findings are actionable first:

1. **Re-run G_027, G_033 and G_035** with `AtSourceScan`, since their prose/code separation was
   unreliable.
2. **Fix the `D_ResonanceStructure` hang**, so a full-suite verification can be quoted on the tag.

Recommended tag once both are done: `v2.2.0`, with **no new Zenodo deposit** (V2.2 adds no
publication artifact; the monograph remains V2.0).

## Deliverables

- `Docs/Publication/RELEASE_NOTES_V2_2.md` — this file
- `Docs/Publication/CHANGELOG.md` — `[2.2.0]` entry
- `README.md` — repository front page updated for V2.2
- `Docs/NewChat_Start.md` — primary project memory, current through G_036 and E_001
- `Docs/AT_LabBook.md`, `Docs/ResearchY/ResearchY_Index.md` — lab book and audit registry
- `Docs/ResearchY/G_GravitySource/ResearchY-G_021…G_036.md` — the gravity/time audits
- `Docs/ResearchY/E_Electromagnetism/ResearchY-E_001.md` — the electromagnetism inventory
