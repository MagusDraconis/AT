# Y_E_003 Result — Photon Ontology Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_003_Tests.cs`
**Status:** 7/7 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_E_003"`
**Group total:** group E = **20/20 PASSED** (E_001 7 + E_002 6 + E_003 7)

## Verdict

**BOUNDARY** — AT does not have to import U(1) as an axiom (the rotation subgroup of `Aut(C96(1..6))` **is** a
compact Z_96, and the weak sector has a genuine su(2)), but it cannot supply the photon without the **cubic
substrate** — which its own M_011/M_012 prove is required and its own G_032/G_033 prove is never instantiated.

**The photon and the graviton are blocked by the SAME missing substrate.**

## 1. The obstruction (representation-theoretic, decisive)

The photon is spin-1 → `l = 1` → **three dimensions**.

| | single ring | cubic 96³ |
|---|---|---|
| group | D_96 dihedral, order **192** | O_h |
| irreps | **4 one-dim + 47 two-dim** | includes **T1(3)**, T2(3) |
| Σd² | **4·1 + 47·4 = 192** ✓ | — |
| max irrep dimension | **2** | **3** |
| vector sector | **IMPOSSIBLE** | **PRESENT** |

| l | sector | octahedral | ring | cubic |
|---|---|---|---|---|
| 0 | scalar (ρ) | A1 | **YES** | YES |
| 1 | **VECTOR (photon)** | **T1** | **no** | **YES** |
| 2 | traceless metric (graviton) | E + T2 | **no** | **YES** |

The budget checks the group order (verified for every even n = 4…200). The ring spectrum pairs `k ↔ 96−k`
(the 47 doublets) and is **scalar**: **45** distinct eigenvalues. M_011: no vector sector on a ring. M_012: a
genuine dimension-3 irrep `T1(3)` with `⟨χ,χ⟩ = 1` on `C96 □ C96 □ C96`. The graviton's traceless part needs the
same (`l = 2 → E + T2`). G_033: the cubic substrate is **never instantiated** — 3-D space is imported as **η**,
which G_032 proved is assumed.

## 2. The U(1): derived and compact — but global

| check | result |
|---|---|
| r order / s order | **96** / **2** |
| `s r s⁻¹ = r⁻¹` | **true** |
| generated group order | **192** |
| Z_96 finite ⇒ compact | **true** |
| rigid rotation is an automorphism | **true** |
| site-dependent permutations that are | **0 of 6** |

**Falsified:** "the 12 link-directions are the 12 gauge generators". Offsets `1,95,…,6,90`; **not** closed under
addition (witness `1 + 95 = 0`); additive closure inside Z_96 is **96**; the algebra is **abelian**; the gauge
algebra's non-abelian part is **11** dimensions. A cardinality match with no algebraic map.
**But** the su(2)-from-doublets claim **passes**: Lie closure dimension **3**.

## 3. What AT already has (corrects a first reading)

`PhaseOrigin` gives AT a real link phase: quantum **6.544985×10⁻²** (= 2π/96), forward/backward **±6.544985×10⁻²**,
path phase over a full cycle **6.283185** = 2π, loop holonomy **compact**, interference law `2 + 2cos(δ)` → **4.000**
at δ = 0 and **0.000** at δ = π.

The substrate's Laplacian has **exactly ONE zero eigenvalue** — the constant mode, **spin 0** — with gap
**0.386351**.

| AT's massless wave objects | spin | status |
|---|---|---|
| Laplacian zero mode (the background) | 0 | **COMPUTED** |
| `TemporalField` scalar wave equation | 0 | **COMPUTED** |
| `□ψ_μν = 0` (Fierz–Pauli) | 2 | **POSTULATED** |
| a massless spin-1 field | 1 | **ABSENT** |

## 4. Polarisation count

| candidate | components | removed | physical | photon? |
|---|---|---|---|---|
| scalar phase (Goldstone) | 1 | 0 | **1** | no |
| ∇ρ on links | 3 | 2 | **1** | no |
| **A_μ, gauge-reduced** | 4 | 2 | **2** | **YES** |
| massive vector (Proca) | 4 | 1 | 3 | no |

## The missing primitive — exactly one, and smaller than it looked

**A phase on spacetime links *with its own dynamics*.** AT already has the phase, the holonomy and the
interference law; it lacks **(a) the spacetime index** (its links are 1-D ring links) and **(b) the fluctuation**
(the step is the fixed constant 2π/N, not a field).

## Caveats and self-corrections

- "D96" is overloaded (the ring **and** its dihedral automorphism group of order 192); the group statement is
  verified on the permutations.
- Spin-1 is identified with `l = 1`, hence a dimension-3 multiplet; a different helicity carrier would require the
  counting to be redone.
- The polarisation table is bookkeeping, not dynamics.
- **Three corrections, all recorded rather than absorbed:** (i) the site-dependent family degenerated to the rigid
  rotation at one parameter value; (ii) the spectral-degeneracy bound is one-sided — 49 is an **upper** bound and
  the measured count is **45**; (iii) **AT *does* already have a link phase**, so the missing primitive is the
  spacetime index and the dynamics, not the phase.
