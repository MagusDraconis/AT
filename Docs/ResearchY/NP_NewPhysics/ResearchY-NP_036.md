# ResearchY-NP_036 — 3D Emergence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_036 (permanent)
**Title:** 3D Emergence Audit
**Status:** COMPLETE
**Date:** 2026-09-03
**File:** `NP_NewPhysics/ResearchY-NP_036.md`
**Depends on:** ResearchY-NP_035 (DOS exponent = number of mode indices; dimensionality
origin), NP_034 (Bose sufficient; mode set is the obstruction; hosted ω² DOS), NP_032
(no thermal ring; 1D DOS at every N/K), NP_028 (blackbody FALSIFIED for D96),
QG_181/183 (M_Pl = v·A³, A = Σm·#g·occ₂), QG_290 (3+1 spatial dimension derived as
d ≥ 3), QG_197 (metric ansatz dimension-generic), QG_210 (family = three octave bands
[4,4,87]), R_001 (five-item boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_036_Tests.cs`

---

## Purpose

NP_035 established that the D96 ring is a 1D structure: its DOS exponent p equals the
number of independent integer mode indices, and the blackbody ω² DOS (p=3) is the DOS
of a genuinely 3D host. NP_036 asks the constructive question: **can observed 3D
physics emerge from multiple D96 structure sectors?** Single D96 gives N(ω) ∝ ω
(p=1); the tensor product D96⊗D96 gives ω²; D96⊗D96⊗D96 gives ω³. Is the observed 3D
blackbody DOS — N(ω) ∝ ω³, g(ω) ∝ ω² — naturally explained as **D96⊗D96⊗D96**? The
audit derives the DOS exponents of the three constructions, compares them against the
blackbody / free-field / 3D-cavity mode counts, tests whether three independent D96
coordinates suffice for N(ω) ∝ ω³, tests whether observed 3D corresponds to "one D96
axis × three", and searches existing AT derivations for hidden triple-factor
structures. No new primitives; canonical AT unchanged.

---

## 1. DOS exponents of D96, D96⊗D96, D96⊗D96⊗D96

The DOS exponent of a d-fold tensor product of rings is the 3D Weyl count exponent:

| Construction | Independent integer indices | DOS exponent p | Cumulative law |
|---|---|---|---|
| **D96** (single ring) | 1 (k ∈ [1, N−1]) | **1** | N(ω) ∝ ω |
| **D96⊗D96** | 2 (k₁, k₂) | **2** | N(ω) ∝ ω² |
| **D96⊗D96⊗D96** | 3 (k₁, k₂, k₃) | **3** | N(ω) ∝ ω³ |

**Verified** (test `Y_NP_036_TensorThreeExponent`): the integer-lattice count in a 3D
positive-octant ball grows with exponent p → 3 (measured p = 3.09 over [20,80], 3.04
over [40,160]); the 2D count gives p → 2.09; the single ring octave-doubles to p = 1
(test `Y_NP_036_SingleRingExponent`). The eigenvalues of the tensor product are
separable: Λ = λ_k1 + λ_k2 + λ_k3, so ω ≈ c·|k| with k ∈ Z³ in the low-frequency
limit — the 3D lattice count.

**This is the central fact:** three independent D96 coordinates (each a D96 ring axis)
reproduce the ω³ cumulative DOS that NP_034/035 identified as the blackbody mode-set
requirement.

---

## 2. Comparison with blackbody, free field, 3D cavity

| Quantity | Blackbody / 3D cavity | Free-field mode count | D96⊗D96⊗D96 |
|---|---|---|---|
| DOS g(ω) | ∝ ω² | ∝ ω² | ∝ ω² |
| cumulative N(ω) | ∝ ω³ | ∝ ω³ | ∝ ω³ |
| Weyl coefficient (positive octant) | (π/6)R³ | (π/6)R³ | (π/6)R³ (verified to <2%) |

**Verified** (test `Y_NP_036_BlackbodyDosMatch`): the 3D positive-octant mode count
approaches (π/6)R³ — the same Weyl term as the 3D-cavity spectrum; the 2D count
approaches (π/4)R² (the wrong, non-blackbody law); the blackbody Stefan-Boltzmann
integral ∫x³/(e^x−1)dx = π⁴/15 = 6.4939 is reproduced exactly by the ω³-weighted
occupation. The DOS exponent of D96⊗3 is **identical** to the blackbody's DOS
exponent.

---

## 3. Are three independent D96 coordinates sufficient for N(ω) ∝ ω³?

**Yes — as a construction.** The D96⊗3 spectrum has separable eigenvalues
Λ = λ_k1 + λ_k2 + λ_k3, and each axis contributes a linear low-frequency branch
ω_k ≈ c·k (verified: ω₁/(ck) → 1 for N = 96..1536, test
`Y_NP_036_ThreeCoordinatesSufficient`). The number of modes below ω is then the
number of positive integer triples with c·|k| ≤ ω — the 3D ball count ∝ ω³. Fewer
axes give R¹ (1D) or R² (2D), not R³. Three independent coordinates are **minimally
sufficient** for the ω³ law.

---

## 4. Do observed 3 spatial dimensions correspond to "one D96 axis × three"?

The tensor construction D96⊗D96⊗D96 literally is "one D96 axis × three": three
independent coordinate axes each carrying the D96 local rule (±1..±6 nearest-neighbour
ring). Its DOS is the 3D Weyl DOS, matching the observed blackbody (test
`Y_NP_036_ThreeAxesCorrespondToSpace`: the Weyl coefficient converges to (π/6)R³).

**BUT the correspondence is hosted, not emergent:**

1. **Canonical AT has ONE D96 ring.** The derived chain (Difference → Actualization →
   Spectrum) produces a single structure sector. No derivation stacks three copies —
   the tensor product is a construction applied to the canonical object, not a
   consequence of it.
2. **A single ring is 1D at every N and K** (NP_032/035: p = 1 for N = 96..6144 and
   K = 1..12). No deformation of one D96 ring produces ω³; three INDEPENDENT copies
   are required.
3. **The spatial dimension is not a D96 output.** AT's metric ansatz g = ρ^(2/d)η is
   dimension-generic (QG197); the Einstein prefactor (d−1)(d−2) forces only d ≥ 3
   (QG290) — the value d = 3 is an observed/hosted input, not derived from the count
   structure. D96⊗3 supplies the ω³ DOS exponent, but nothing in the single-ring
   content selects "exactly three independent copies".

So the observed 3D DOS is **CORRESPONDENCE**: the tensor product of three D96 rings
reproduces it exactly, but as a hosted 3D geometry (consistent with NP_028/034/035,
which classify the ω² DOS as hosted higher-layer content).

---

## 5. Hidden triple-factor structures in existing AT derivations

The audit searched the established derivation chain for triple-factor structures:

| Structure | Value | Status |
|---|---|---|
| **A = Σm·#g·occ₂** (three spectral counts of the SINGLE ring) | 95·44·87 = 363,660 | DERIVED (spectral moments, # distinct freqs, top-octave occupancy) |
| **M_Pl = v·A³** (the cube of that triple product) | v·(95·44·87)³ = 1.2234e19 GeV | DERIVED (QG181; cube exponent 3.0000 to 1e-4, QG183) |
| **three octave families** [4,4,87] (family index = octave band, QG210) | 3 | DERIVED value / BOUNDARY window (D_020/D_040) |
| **dimension d** in g = ρ^(2/d)η | d ≥ 3 | DERIVED (only d ≥ 3; QG290) |

**Verified** (test `Y_NP_036_HiddenTripleFactorStructure`): A = 95·44·87 is a triple
product of three spectral counts of the ONE D96 ring; M_Pl = v·A³ with cube exponent
p = ln(M_Pl/v)/ln(A) = 3.0000.

**Critical distinction:** the hidden A = 95·44·87 triple is a triple of FREQUENCY-
CONTENT counts (mode count × distinct frequencies × top-octave modes) within a single
ring — **not three spatial axes**. It explains why "three" and "cubes" appear
throughout AT (the Planck scale is v × a triple product cubed; the D96 occupancy is
three octave bands) — but it is a different kind of "three" than the three independent
tensor coordinates needed for ω³.

---

## Theorem

> **Theorem (NP_036).** The DOS exponent p = d of the d-fold tensor product of D96
> rings reproduces the blackbody DOS exponent only for d = 3, and this reproduction is
> a hosted CORRESPONDENCE, not an EMERGENT consequence of a single D96 sector.
> Proof: (1) DOS exponents (Section 1, verified): D96 has p=1 (octave doubling),
> D96⊗2 has p→2, D96⊗3 has p→3 (integer-lattice Weyl counts). (2) Comparison
> (Section 2, verified): the blackbody DOS g ∝ ω², the free-field 3D count, and the
> 3D-cavity spectrum all give N(ω) ∝ ω³ with the (π/6)R³ Weyl coefficient — identical
> to D96⊗3; the 2D count gives the wrong (π/4)R² law; the Stefan-Boltzmann integral is
> π⁴/15. (3) Sufficiency (Section 3, verified): the separable tensor eigenvalues
> Λ = λ_k1+λ_k2+λ_k3 give ω ≈ c|k| with k ∈ Z³, so three independent coordinates
> suffice for N ∝ ω³; fewer axes give R¹ or R². (4) Observed 3D = "one D96 axis ×
> three" (Section 4): the three-axis tensor reproduces the 3D Weyl DOS, but canonical
> AT is a single ring (p=1 at every N, K — NP_032/035) and the metric ansatz is
> dimension-generic (only d ≥ 3 derived, QG290): three independent copies must be
> hosted, so the correspondence is CORRESPONDENCE, not EMERGENT. (5) Hidden triples
> (Section 5, verified): AT already contains A = Σm·#g·occ₂ = 95·44·87, a triple of
> spectral counts cubed to M_Pl = v·A³ (QG181, cube exponent 3.0000) — a frequency-
> content triple, not three spatial axes. Classification: DOS exponent p = d of the
> d-fold tensor DERIVED (Weyl law); three independent D96 coordinates SUFFICIENT for
> N(ω)∝ω³ DERIVED (as a construction); observed 3D blackbody DOS explained as
> D96⊗D96⊗D96 CORRESPONDENCE (hosted 3D geometry, NP_028/034/035 unchanged); 3D
> EMERGING from a single D96 sector FALSIFIED (single ring p=1); spatial dimension
> d=3 as a canonical D96 output FALSIFIED (metric dimension-generic, d ≥ 3 only);
> hidden triple A = 95·44·87 DERIVED (QG181). No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) compute exponents. (2) match the blackbody Weyl count. (3) show
> three coordinates suffice. (4) test the "one axis × three" reading. (5) search
> hidden triples. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "A single D96 ring produces 3D physics" | p = 1 at every N, K (NP_032/035): one ring is 1D, one coordinate |
| "D96⊗D96 (two axes) gives the blackbody DOS" | two axes give N ∝ ω² (g ∝ ω), the 2D law — the blackbody needs ω³ |
| "D96⊗3 emerges from canonical AT" | canonical AT is ONE ring; no derivation stacks three independent copies — the tensor is a hosted construction |
| "d = 3 is derived from the D96 count structure" | the metric g = ρ^(2/d)η is dimension-generic; the Einstein factor forces only d ≥ 3 (QG290) |
| "The three octave families [4,4,87] are the three axes" | [4,4,87] are three octave BANDS within one frequency coordinate — not three independent spatial coordinates |
| "A = 95·44·87 is a spatial triple" | A is a triple of spectral counts of the single ring (mode count × distinct freqs × top-octave modes), cubed for M_Pl — a frequency-content triple, not three axes |
| "The tensor product is the only ω³ construction" | any three independent integer-coordinate lattices give the 3D Weyl count; D96⊗3 is one realization (NP_035 already: the ω² DOS is generic 3D-host content) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| D96⊗3 has DOS exponent 3 | a 3-fold D96 tensor whose N(ω) does not scale as ω³ |
| three independent D96 coordinates suffice for ω³ | a 3-axis D96 construction whose mode count is not the (π/6)R³ ball count |
| a single D96 sector cannot host 3D | a single ring (any N, K) with DOS exponent 3 |
| the ω² DOS is hosted content | a canonical derivation from ONE D96 that produces three independent spatial axes and d=3 |
| d = 3 is a canonical output | a derivation of exactly three independent D96 copies from the D96 boundary set alone |
| M_Pl = v·A³ is a triple-product cube | a spectral A = Σm·#g·occ₂ whose cube does not give M_Pl (QG183 re-check) |

---

## Classification

| Component | Status |
|---|---|
| DOS exponent p = d of the d-fold D96 tensor (d = 1, 2, 3) | **DERIVED** (Weyl lattice count, verified) |
| three independent D96 coordinates sufficient for N(ω) ∝ ω³ | **DERIVED** (as a construction: separable eigenvalues → ω ≈ c|k|, k ∈ Z³) |
| observed 3D blackbody DOS as D96⊗D96⊗D96 | **CORRESPONDENCE** (hosted 3D geometry; NP_028/034/035 unchanged — the ω² DOS is hosted content, not D96-emergent) |
| 3D physics EMERGING from a single D96 sector | **FALSIFIED** (single ring p=1 at every N, K) |
| spatial dimension d = 3 as a canonical D96 output | **FALSIFIED** (metric ansatz dimension-generic; only d ≥ 3 derived, QG290) |
| hidden triple-factor A = Σm·#g·occ₂ = 95·44·87, M_Pl = v·A³ | **DERIVED** (QG181/183 — a frequency-content triple, NOT spatial axes) |
| temperature / occupation / Bose statistics | **BOUNDARY / EMERGENT** (unchanged, NP_027/028/033) |

**Conclusion:** three independent D96 coordinates DO reproduce the observed 3D DOS —
the tensor product D96⊗D96⊗D96 has exactly the blackbody's N(ω) ∝ ω³ (g ∝ ω²) Weyl
count. But this is a CORRESPONDENCE, not an EMERGENCE: canonical AT contains ONE D96
ring (p=1 at every N and K), the metric g = ρ^(2/d)η is dimension-generic (only d ≥ 3
derived), and nothing in the D96 chain derives that three independent copies must be
stacked. The existing hidden triple-factor structure — A = Σm·#g·occ₂ = 95·44·87
cubed to M_Pl = v·A³ (QG181, cube exponent 3.0000) — explains the prevalence of
"threes" and cubes in AT content, but it is a frequency-content triple within one
ring, not three spatial axes. So the success-criterion answer is: the observed 3D DOS
is naturally explained as D96⊗D96⊗D96 **as a hosted 3D construction (CORRESPONDENCE),
not as an emergent product of a single D96 sector**. No new primitive; canonical AT
unchanged.

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_036_Tests.cs`
**Run:** 2026-09-03 · **Result:** see `Tests/Results/Y_NP_036_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_036_SingleRingExponent` | single D96: p=1 (octave doubling) | ✅ |
| `Y_NP_036_TensorTwoExponent` | D96⊗D96: p→2 (2D Weyl count) | ✅ |
| `Y_NP_036_TensorThreeExponent` | D96⊗D96⊗D96: p→3 (3D Weyl count) | ✅ |
| `Y_NP_036_BlackbodyDosMatch` | 3D count (π/6)R³ matches blackbody/cavity; SB = π⁴/15 | ✅ |
| `Y_NP_036_ThreeCoordinatesSufficient` | three axes → ω³; separable eigenvalues | ✅ |
| `Y_NP_036_ThreeAxesCorrespondToSpace` | "one D96 axis × three" = 3D Weyl DOS | ✅ |
| `Y_NP_036_HiddenTripleFactorStructure` | A = 95·44·87; M_Pl = v·A³ (cube exponent 3) | ✅ |
| `Y_NP_036_Classification` | DERIVED / CORRESPONDENCE / FALSIFIED flags | ✅ |
| `Y_NP_036_Run` | research report | ✅ |

**Conclusion:** the 3D blackbody DOS (N ∝ ω³, g ∝ ω²) is exactly reproduced by
D96⊗D96⊗D96 — three independent D96 coordinates are sufficient for the ω³ Weyl count.
The reproduction is CORRESPONDENCE (hosted 3D geometry), not EMERGENT: canonical AT is
a single 1D ring, and the metric is dimension-generic (d ≥ 3 only). The hidden triple
A = Σm·#g·occ₂ = 95·44·87, cubed to M_Pl = v·A³, is a frequency-content triple, not a
spatial one. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_036"`

---

## References

- ResearchY-NP_035 (DOS exponent = number of mode indices; dimensionality origin),
  NP_034 (Bose sufficient; mode set the obstruction; hosted ω² DOS), NP_032 (no
  thermal ring; 1D at every N/K), NP_028 (blackbody FALSIFIED for D96), QG_181/183
  (M_Pl = v·A³, A = 95·44·87, cube exponent 3.0000), QG_290 (3+1 derived as d ≥ 3),
  QG_197 (metric ansatz g = ρ^(2/d)η dimension-generic), QG_210 (family = three octave
  bands [4,4,87]), R_001 (five-item boundary set), S_001 (synthesis).
