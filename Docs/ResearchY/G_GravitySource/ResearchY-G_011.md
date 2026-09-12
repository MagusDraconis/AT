# ResearchY-G_011 — Rho Actuator Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_011 (permanent)
**Title:** Rho Actuator Audit — can any physical quantity change ρ?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_011.md`
**Depends on:** ResearchY-G_001 (ρ is the source; energy/spectral are correlates), G_002 (the ρ operations and
the 51 free directions), G_003 (the Δa_AT convention), G_005 (the Poisson ceiling), G_006 (the relaxation
operator and its spectrum), G_007 (the derived form/range of DiffuseStep), G_008 (the drive law for an
arbitrary target), G_009 (the clock reading ΔΦ/c² = Δlnρ/d); AT-QG QG180/QG181 (E = Σ m λ), QG194 (count
conservation), QG220 (ψ_j = √ρ_j e^{iθ_j}), QG008 (I_occ = KL(ρ‖uniform)); `AT.Core/ResearchXH/RhoDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_011_Tests.cs` (9/9 PASSED, ~0.1 s)

## Purpose

Ask the control question directly:

> **Can any physical quantity change ρ?**
> Candidates: energy density, phase coherence, spectral organization, degeneracy engineering, attractor
> compression, synchronization, information density.

**Actuator criterion.** `q` is an actuator iff (i) `q` is **independent** of ρ (not a function of it),
(ii) `q`'s value **determines** ρ locally (∂ρ/∂q ≠ 0), and (iii) changing it leaves `Σρ = 1`, costs no
energy, breaks no symmetry and introduces no new primitive (the audit's constraints).

**Answer.** **No candidate is an actuator.** Five of the seven are *functions of ρ* (energy, spectral
organization, degeneracy engineering, compression, information); the remaining two are *independent of ρ but
exactly ρ-inert* (phase coherence, synchronization). ρ is nevertheless **fully actuable**: the map
`s = (I − W)ρ*` is invertible on the 95 zero-sum directions with finite gains everywhere (4669.2968 … 1.2503).
**The vacancy is a source, not a handle** — the same missing driver G_008/G_010 identified.

## 1. The four measured deltas per candidate

All quantities on the D96 lattice (45 eigenspaces, 96 cells, free room 51), `d = 0.2`. The G_003/G_009
convention reads a lattice-level change as `ΔΦ/c² = Δa_AT/d`.

| candidate | operation | Δρ (L1) | Δa_AT | ΔΦ/c² | Δτ/τ [s/day] | **label** |
|-----------|-----------|---------|-------|-------|--------------|-----------|
| energy density | degeneracy redistribution at fixed E | 0.6666667 | **0.603175** | 0.201058 | 17 371.4 | **CORRELATED** |
| spectral organization | reassign the mode amplitudes | 0.6666667 | 0.603175 | 0.201058 | 17 371.4 | **CORRELATED** |
| degeneracy engineering | within-eigenspace redistribution | 0.6666667 (ΔE = 0) | 0.603175 | 0.201058 | 17 371.4 | **CORRELATED** |
| attractor compression | survivor compaction (k = 48) | 2.8669638 | 0.032121 | 0.010707 | 925.1 | **REFUTED** |
| information density | any permutation / reversal | 0.6583333 | 1.0031746 | — | — | **REFUTED** |
| phase coherence | global shift, mirror, locking | **0** (2.5e-16) | **0** | **0** | **0** | **REFUTED** |
| synchronization | lock the phase grid | **0** | **0** | **0** | **0** | **REFUTED** |
| *imported source s* | `s = (I − W)ρ*` per step | drives any ρ* | — | — | — | **ACTUATOR (imported)** |

## 2. Energy density is an exact re-expression (CORRELATED)

* `E = ⟨λ, ρ⟩` (occupation-weighted spectral content, QG180/181) is a **fixed linear functional** of ρ:
  `E = 12.000000000000` for both the canonical measure and the tilt, identical to **1e-12**, because the
  behind it every eigenspace total is conserved (`BlockSums` equal to 12 digits).
* Yet ρ moves by `L1 = 0.6666667` and the field is created out of an **exact zero** (`0 → 0.603175`).
* `E` is **not injective**: the fixed-E fibre contains the whole **51-dimensional** degeneracy space (94
  dimensions in total — one linear constraint on the 95-dimensional simplex), so `E` cannot *select* a
  configuration.
* Re-ordering the **same multiset** (tilt → ascending) moves `E` by **1.5401766 (12.8 %)**: the energy depends
  on the pairing of ρ with the spectrum, i.e. the arrangement is prior to the energy.
* Density-space reading: the tilt is a **4.8 : 1** occupancy contrast, `Δlnρ = ln 4.8 = 1.5686159` →
  `ΔΦ/c² = 0.5228720` → **45 176.1 s/day** (the acceleration-channel reading above is the series convention;
  both are far above the band, so no verdict depends on the choice).

## 3. Phase coherence and synchronization are independent — and inert (REFUTED)

Phase is the **one** candidate that is not a function of ρ, so it is the only one that *could* have been an
actuator. It is not:

| phase assignment | `L1(ρ, |ψ|²)` | `max|Δa|` | `|Σψ|` |
|------------------|--------------|-----------|--------|
| canonical `θ_j = 2πj/96` | 2.5e-16 | < 1e-9 | 0.0324196281 |
| global shift `+1.234` | 2.3e-16 | < 1e-9 | 0.0324196281 |
| mirror `−θ` | 2.5e-16 | < 1e-9 | 0.0324196281 |
| **locked** `θ_j = 0` (synchronized) | 2.0e-16 | < 1e-9 | **9.1171821879** |

* `ρ = |ψ|²` is **phase-blind**: the four assignments give the same density, acceleration and curvature to
  the floating-point floor of the derivative estimate.
* The ψ-sector **does** move: the coherent sum runs `0.0324196 → 9.1171822`, a factor **281.22**.
* Synchronization is a **phase relation**, and the canonical grid `θ_j = 2πj/N` is the *maximally incoherent*
  configuration (the 96th roots of unity sum to zero): Kuramoto `r = 8.0788382e-17 → 1.0` on locking, with
  ρ, a and R untouched. The two-mode interference term still runs 4 → 2 → 0, so the phase is observable —
  it simply does not touch ρ.

## 4. Spectral organization and degeneracy engineering are coordinates of ρ (CORRELATED)

* The orthonormal DCT-II satisfies `C Cᵀ = I` to **1.37e-14**, `Idct(Dct(ρ)) = ρ` to **4.2e-16`, and its DC
  coefficient **is the count** (`Σρ = 1`, fixed): the 95 AC coefficients are independent coordinates of ρ —
  a bijection, so "changing the spectral organization" *is* changing ρ.
* The witness is a **high-k** object: dominant mode `k = 94`, with `k ≥ 48` carrying **79.66 %** of the AC
  energy — i.e. the mode amplitudes are the G_005/G_006 "free directions" in another basis.
* The **operator's** spectrum carries no ρ-dependence at all: `μ_k` is fixed by `(d, N)` (`μ_48 = 0.6`
  exactly; `1 − μ_1 = 2.141650094e-4`, `1 − μ_95 = 0.799785835`). Only the amplitudes are free.
* Degeneracy engineering is the same story made explicit: the **51** within-eigenspace directions conserve
  the count, every eigenspace total, the energy *and* the whole spectrum (`A₀ = 45` unchanged) and still
  create the field `0 → 0.603175`. It is the largest genuinely free structure in ρ — and nothing selects it.

## 5. Compression is a map, information is global (REFUTED)

* **Compression** (G_002's operation, `CONTROLLABLE` status unchanged) moves ρ by `L1 = 2.8669638` with
  `Δa = 0.032121`, but it is a **ρ → ρ map**, not a quantity of the theory. Unlike the witness, its
  difference is **smooth**: it survives 200 relaxation steps essentially unchanged (`max|Δa|` still
  0.032121; the base profile's std contracts only **1.1207×**), so it is not a "state" that the dynamics
  prices — it is a relabelling whose magnitude is a function of the ρ it acts on.
* **Information density** `I_occ = KL(ρ‖uniform)` is **exactly** permutation- and reversal-invariant
  (`ΔKL = 0` to 1e-15) while ρ moves (`L1 = 0.6583333`) and the field can even **grow**
  (`max|Δa| = 1.0031746`). `KL(uniform) = 0`, `KL(tilt) = 0.2725652026 = ln 96 − H(tilt)`
  (the G_005 witness cost). Along the flow it only decreases: `0.27257 → 1.0873e-3 (50) → 2.6946e-4 (200)`.
  A global functional cannot drive a local field (fiber dimension 94).

## 6. The actuator result

* The uniform state is the **unique undriven fixed point**: `‖(I − W)ρ̄‖₁ = 0` (G_008).
* For any target `ρ*`, the required source is `s = (I − W)ρ*`, **count-neutral** (`Σs = 2.1e-17`) and
  **unique**: `(I − W)` is diagonal in the DCT basis with gains `1/(1 − μ_k)`, all finite
  (`4669.296831218` at `k = 1`, `2.500000000` at `k = 48`, `1.250334722` at `k = 95`) — **no unreachable
  direction and no zero-gain mode**.
* The inverse reconstructs the target from its drive to **1.9e-15**, and the driven recursion
  (`ρ ← Wρ + s`, 20 000 steps) converges onto it to **< 1e-12**.
* Price of the witness: `‖s‖₁ = 0.48675` per step (49 % of the count), `max|s| = 0.01866667` — G_008's
  number, re-derived from the general law.

**ρ is completely controllable — the missing ingredient is a source.** The only quantity that changes ρ is
an imported occupancy flux, which is not an AT primitive.

## 7. Verdicts

| label | content |
|-------|---------|
| **ACTUATOR** | none of the seven candidates. The only handle is the imported source `s = (I − W)ρ*` (Σs = 0, unique, count-neutral), i.e. the drive G_008/G_010 priced and nothing supplies. |
| **CORRELATED** | energy density (exact linear re-expression, non-injective, fibre ⊇ 51 dims), spectral organization (orthonormal coordinates on ρ), degeneracy engineering (the free part of ρ itself). |
| **REFUTED** | phase coherence and synchronization (independent of ρ and exactly ρ-inert: Δρ = 0 to 1e-15, \|Δa\| < 1e-9, while the ψ-sector moves 281×); attractor compression (a map, not a quantity); information density (a global, permutation-invariant functional). |

## 8. Classification and caveats

**No reclassification.** D_040 is untouched. G_002's `CONTROLLABLE` verdicts concern the **operations** on ρ
(they do move it at fixed energy) and stand unchanged; the G_011 labels concern whether the **quantity** is
an actuator. G_001's `SOURCE = ρ` and its `CORRELATED` labels are confirmed from the control side. No
canonical claim, value or equation changes; no new primitive.

* The four-delta table uses the series convention `ΔΦ/c² = Δa_AT/d` (G_003/G_009) so the rows match the
  G_009 clock table exactly (0.201058 → 17 371.4 s/day = G_009's degeneracy row); the density-space reading
  is given alongside in §2 and changes no verdict.
* The 33.78× contraction quoted throughout is the G_006 witness factor (`std(ρ_t)/std(W²⁰⁰ρ_t) = 33.7781`);
  the L1 form of the same decay is 0.6666667 → 0.0201006 (33.17×).
* Re-measured detail: the witness's high-k share at the cut `k ≥ 48` is **0.7965733** (G_008's report line
  quoted 78.2 % for the same object; the difference is a reporting detail, no verdict depends on it).

## 9. Open problems (OP1–OP4)

1. Can any **external agent** be mode-matched to the AT occupancy index (the same gap as G_008 OP1/G_010 OP1,
   with NP_171's imported gate and NP_174's prohibition on antisymmetric self-coupling)?
2. Is there a **non-Hermitian** (antisymmetric) coupling that is forbidden here but allowed in an open
   system (NP_174 closes it in the canonical chain)?
3. The phase sector is ρ-inert but not inert in the ψ-sector: does any AT observable other than interference
   (QG44's spin-2 ψ) couple to the 281× coherent sum?
4. The 51 energy-free directions are never selected: is there an **entropy** argument that they should be
   equiprobable, and does that leave the observed field untouched (G_005's Poisson band)?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_011_Tests.cs` — **9/9 PASSED** (~0.1 s)
**Group total:** G_001–G_011 = **91/91 PASSED** (with G_011b: **98/98**)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_011"`

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_001.md`, `ResearchY-G_002.md`, `ResearchY-G_003.md`,
  `ResearchY-G_005.md`, `ResearchY-G_006.md`, `ResearchY-G_007.md`, `ResearchY-G_008.md`, `ResearchY-G_010.md`
* `Docs/ResearchY/Tests/Results/Y_G_011_Result.md`
* `AT.Tests/Shared/RhoActuators.cs` (shared spectral/actuator machinery), `DensityField.cs`
* `AT.Core/ResearchXH/RhoDynamics.cs`
