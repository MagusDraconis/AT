# ResearchY-NP_032 — Thermal-N Search Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_032 (permanent)
**Title:** Thermal-N Search Audit
**Status:** COMPLETE
**Date:** 2026-09-02
**File:** `NP_NewPhysics/ResearchY-NP_032.md`
**Depends on:** ResearchY-NP_027/028 (Planck factor / blackbody FALSIFIED), NP_030
(no canonical temperature), NP_031 (structure sector vs added occupancy layer),
A_003/D_008/D_030 (D96 spectrum), NP_025/026 (circulant C_N(±1..±K) family),
D_021 (pairing), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_032_Tests.cs`

---

## Purpose

NP_031 established that D96 belongs to a DERIVED structural sector while thermodynamics
is an ADDED occupancy layer (temperature BOUNDARY, radiation FALSIFIED). NP_032 asks the
question the NP_027–031 chain naturally raises about the SIZE dimension: **is D96
SPECIFICALLY a structure attractor, while some OTHER ring size D_N acts as a thermal
attractor?** If the structure/thermodynamics split were an N-selection — D96 optimal for
structure, another N optimal for hosting thermal occupation — the scan should reveal a
"thermal N" with ω² DOS (3D-cavity blackbody density of states), exponential (Wien)
tail tendency, and thermal-occupancy compatibility. Program: scan N = 8..512 of the
canonical circulant family C_N(±1..±6), compute occupancy hierarchy, DOS scaling,
spectral crowding and UV behavior at every N, and compare against D96. No new
primitives; canonical AT unchanged.

## 1. The scan

The canonical spectrum is the circulant ring C_N(±1..±6): for N sites with couplings
s = 1..6, the eigenvalues are λ_k = Σ_s 2(1 − cos(2πks/N)) and the frequencies are
ω_k = √λ_k for the N−1 positive modes (D96: N = 96, ω₁ = 0.6216, ω_max = 3.9796,
span 6.40, occupancy [4,4,87]). Scan N = 8..512 of the same K = 6 family.

**UV behavior (decisive).** The band edge is N-independent: ω_max → the continuum
maximum 3.9851 as N grows (verified: N = 96 → 3.9796, N = 512 → 3.9849, N = 1024 →
3.9851, N = 4096 → 3.9851). Adding sites does NOT extend the spectrum upward — every
ring C_N(±1..±6) has the same hard UV cap 3.9851. Since a Wien tail needs modes above
the observed e^(−ω/θ) range, NO N in the family can supply an exponential tail. The
UV cap is a structural property of the coupling set (±1..±6), not of N.

**DOS scaling (decisive).** For small k the eigenvalues expand as
λ_k = Σ_s 2(1 − cos(2πks/N)) ≈ (2πk/N)²·Σ_s s² = (2πk/N)²·91, so
ω_k ≈ (2π√91/N)·k — an exactly LINEAR dispersion at low frequency (verified: at
N = 4096 the ratio ω_k/(c·k) = 1.0000 for k = 1..4). Equal-k modes are equally spaced
in ω, so the cumulative mode count N(ω) ∝ ω — a 1D-chain DOS with exponent ≈ 1
(measured: 1.06–1.09 over the low band at N = 512..4096). The 3D-cavity blackbody DOS
requires cumulative N(ω) ∝ ω³ (DOS ω²); the 2D cavity requires ∝ ω². NO N in 8..512
reaches even exponent 2 (scan: no N with low-frequency exponent in [2.5, 3.5]; best
large-N values all ≈ 1.0–1.2). The ring family is structurally 1D at every N — it can
never be a 3D blackbody cavity.

**Occupancy hierarchy.** Across the whole scan the occupancy is top-heavy at every N:
the first octave holds 4 modes for 478 of 505 N values (4,4,... patterns repeat), and
the top octave holds the overwhelming majority (D96: 87/95 = 91.6% in the top octave).
D96's [4,4,87] is the N = 96 member of a FAMILY of identical-structure rings in the
3-family span window [4,8): N = 60..120 all give 3-octave occupancy with the (4,4,X)
hierarchy (61 rings), and D96 is not the edge — it sits at span 6.40 in the middle of
that window. D96 is structurally special only in the sense that NP_031's structure
sector uses N = 96 (span 6.40 → 3 octaves, occupancy [4,4,87]) as its canonical
counting base — not because N = 96 has any thermal property the others lack.

**Spectral crowding.** Crowding rises into the top of the band at every N (the
distribution is bottom-sparse, top-dense), the anti-thermal direction established for
D96 in NP_028. Larger N adds more low-frequency modes below the fixed cap but does not
change the top-heaviness.

## 2. Search results

| Search target | Needed for a thermal attractor | Found in C_N(±1..±6), N = 8..512 |
|---|---|---|
| ω² DOS | cumulative N(ω) ∝ ω³ (3D cavity) | **NO** — linear dispersion ω ∝ k, exponent ≈ 1 at every N |
| exponential (Wien) tail tendency | modes above the band, decaying density | **NO** — hard UV cap 3.9851, identical at every N |
| thermal occupancy compatibility | a decaying geometric rate μ<1 | **NO** — canonical branching is μ = 2 growth (NP_030); N changes nothing |

## 3. Compare against D96

D96 (N = 96) is quantitatively identical in its spectral character to its neighbours
in the same K = 6 family: N = 92..100 all have occupancy (4,4,X) and the same UV cap
and the same linear low-frequency DOS. D96's role is STRUCTURAL — it is the canonical
base of the structure sector (NP_031), selected by the octave/family window (span 6.40
→ 3 families). It is not a thermal attractor (NP_028 FALSIFIED the blackbody), and no
other N in 8..512 is a thermal attractor either: every ring of the canonical class is
a 1D chain with a fixed UV cap and top-heavy occupancy. Thermodynamics is not an
N-property of this family at all — it is the added occupancy layer of NP_031.

## Theorem

> **Theorem (NP_032).** There is NO thermal-attractor ring size in the canonical
> circulant family: Structure N = 96, Thermal N = ∅. (1) UV behavior: the band edge
> ω_max → 3.9851 for ALL N (verified N = 96..4096) — the UV cap is set by the coupling
> set ±1..±6, not by N, so no ring can host a Wien tail above the band (Section 1).
> (2) DOS scaling: λ_k ≈ (2πk/N)²·91 gives ω_k ∝ k exactly at low frequency (ratio
> 1.0000 at N = 4096), hence cumulative N(ω) ∝ ω with exponent ≈ 1.06–1.09 at every
> large N — never the ω² (2D) or ω³ (3D) cumulative growth a thermal cavity needs; the
> scan over N = 8..512 finds NO ring with low-frequency DOS exponent in [2.5, 3.5]
> (Section 1). (3) Occupancy hierarchy and crowding: every ring is top-heavy (first
> octave holds 4 modes for 478/505 N; top octave holds 87–93% for the 3-octave rings),
> the anti-thermal direction (NP_028); D96's [4,4,87] is one member of a 61-ring family
> in the 3-family span window (Section 1). (4) Thermal-occupancy compatibility: the
> Bose occupation n = 1/(e^x − 1) requires a decaying geometric rate μ < 1, but the
> canonical branching is μ = 2 growth at every N (NP_030) — N does not change the
> branching. (5) Conclusion: structure N (96) ≠ thermal N because NO thermal N exists —
> the structure/thermodynamics split is NOT a size selection (NP_032) but a LAYER split
> (NP_031: DERIVED structure sector + added occupancy layer). Classification: the
> hypothesis "another D_N is a thermal attractor" FALSIFIED (no ω² DOS, no exponential
> tail, no thermal occupancy at any N = 8..512 of the canonical class); D96 as the
> structure-sector base DERIVED (octave/family window, NP_031); the 1D linear DOS of
> every C_N(±1..±6) ring DERIVED (analytic: ω_k ∝ k); the N-independent UV cap DERIVED
> (coupling-set property); temperature BOUNDARY (unchanged, NP_027/028/030);
> thermodynamics as an added occupancy layer DERIVED (NP_031). No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) compute ω_max across N → constant cap 3.9851. (2) expand λ_k for
> small k → ω ∝ k → DOS exponent ≈ 1. (3) count octave occupancies across the scan →
> top-heavy at every N. (4) note the branching μ = 2 is N-independent (NP_030). (5)
> conclude no thermal N exists; the split is a layer split. ∎

## 4. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a larger N becomes a blackbody cavity" | adding sites adds modes BELOW the fixed cap (ω_max = 3.9851 for all N); it cannot create ω² DOS or a Wien tail |
| "some N gives ω² DOS" | λ_k ≈ (2πk/N)²·91 → ω_k ∝ k, cumulative N(ω) ∝ ω (exponent ≈ 1.06); scan finds no exponent in [2.5,3.5] over N = 8..512 |
| "the occupancy flattens at some N" | first octave holds 4 modes for 478/505 N and the top octave dominates at every N — the ring is bottom-sparse at every size |
| "D96 is a thermal attractor" | already FALSIFIED for the blackbody (NP_028); D96 is the structure-sector base, not a thermal one |
| "a different coupling/ring class is needed for a thermal N" | that is a NEW primitive/geometry, not a D_N of the canonical class — out of scope (NP_031: thermo is an added layer, not an N-selection) |

## 5. Falsification paths

| Claim | Falsification |
|---|---|
| no thermal N in C_N(±1..±6) | a ring size N = 8..512 with low-frequency cumulative DOS exponent ≥ 2.5 (ω² DOS) |
| UV cap is N-independent | a ring size with ω_max above the continuum 3.9851 |
| occupancy is top-heavy at every N | a ring size whose high-octave occupancy does not dominate (thermal spread) |
| thermal occupation needs μ < 1 (unchanged) | an N-dependence of the canonical branching rate |

## Classification

| Component | Status |
|---|---|
| "another D_N is a thermal attractor" (N = 8..512, C_N(±1..±6)) | **FALSIFIED** (no ω² DOS, no exponential tail, no thermal occupancy at any N) |
| Structure N = 96 as the structure-sector base | **DERIVED** (octave/family window, occupancy [4,4,87], NP_031) |
| 1D linear low-frequency DOS of every ring (ω_k ∝ k) | **DERIVED** (analytic: λ_k ≈ (2πk/N)²·91) |
| N-independent UV cap (ω_max → 3.9851) | **DERIVED** (property of the coupling set ±1..±6) |
| top-heavy occupancy at every N | **DERIVED** (structural; anti-thermal direction, NP_028) |
| temperature scale T | **BOUNDARY** (unchanged, NP_027/028/030) |
| thermodynamics as an added occupancy layer | **DERIVED** (NP_031) — NOT an N-selection |
| structure N ≠ thermal N (as a size dichotomy) | **FALSIFIED** as a size split — there is no thermal N; the split is a LAYER split |

**Conclusion:** D96 is specifically a STRUCTURE attractor (the canonical N = 96 of the
octave/family window, occupancy [4,4,87]), but there is NO thermal attractor ring size.
Every ring C_N(±1..±6), N = 8..512, is a 1D chain: linear low-frequency dispersion
(ω ∝ k, DOS exponent ≈ 1), the same hard UV cap 3.9851 at every N, and top-heavy
occupancy. The ω² DOS, Wien tail, and thermal occupancy a blackbody needs are absent at
every N. Thermodynamics is therefore not an N-property of the canonical family — it is
the ADDED occupancy layer of NP_031 (occupation law + BOUNDARY temperature). The
hypothesis "structure N ≠ thermal N" is FALSIFIED as a size dichotomy because no
thermal N exists; the true split is structural-layer vs added-occupancy-layer. No new
primitive; canonical AT unchanged.

---

## References

- ResearchY-A_003 (branching μ = 2), D_008/D_030 (D96 spectrum, occupancy [4,4,87]),
  D_021 (pairing), NP_025/026 (circulant C_N(±1..±K) family — the K = 6 ratio
  √(K/(K+1))), NP_027 (occupation FORM emergent; temperature BOUNDARY), NP_028
  (blackbody FALSIFIED; top-heavy + truncated), NP_030 (no canonical temperature;
  μ = 2 anti-thermal), NP_031 (structure sector DERIVED; thermodynamics an added
  occupancy layer), QG_194 (geometric occupation), S_001 (synthesis).
