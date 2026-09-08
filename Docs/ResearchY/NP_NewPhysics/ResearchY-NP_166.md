# ResearchY-NP_166 — Periodic Table Emergence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_166 (permanent)
**Title:** Periodic Table Emergence Audit
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `NP_NewPhysics/ResearchY-NP_166.md`
**Depends on:** ResearchY-NP_072 (particle = resonance class), NP_100 (binding = resonance locking),
NP_101 (hierarchy = repeated organization), NP_105 (Difference conservation), NP_017 (atomic/molecular
domain CORRESPONDENCE), NP_085 (completeness frontier), NP_087 (nuclear shell structure MISSING),
NP_089 (O(3) approximate only)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_166_Tests.cs`

---

## Purpose

NP_072 made particles resonance classes; NP_100 made binding resonance locking; NP_101 made hierarchy
repeated organization; NP_105 made Difference the parent conservation law. NP_166 asks the chemistry
question those results make unavoidable: **can the periodic table emerge from AT resonance
organization alone — without solving the Schrödinger equation?** The periodic table is the empirical
ordering of the elements by their electron-shell structure. Program: (1) inventory shell structure,
valence structure, and periodicity; (2) determine whether electron shells correspond to resonance
layers / organization levels / locking hierarchies; (3) test H, He, Li, Ne, Ar; (4) compare AT shell
counts vs observed shell counts; (5) determine whether AT predicts 2, 8, 18, 32 naturally.
**Success criterion:** determine whether the periodic table can be derived from AT organization rather
than imported from quantum mechanics. No new primitives; canonical AT unchanged.

---

## 1. Inventory — shell structure, valence structure, periodicity

| Observable | Empirical content |
|---|---|
| **shell structure** | electrons fill principal shells n = 1, 2, 3, 4 with maximum occupancies 2n² = **2, 8, 18, 32** (K, L, M, N) |
| **shell capacity law** | capacity(n) = 2n² = 2 × [Σ_{l=0}^{n−1}(2l+1)] = 2·(1, 4, 9, 16) — the hydrogenic degeneracy n² (spherical-harmonic subshells 2l+1) × spin-2 |
| **subshell capacities** | s = 2, p = 6, d = 10, f = 14 electrons = 2(2l+1) for l = 0..3 |
| **valence structure** | chemical properties recur with the OUTER-shell (valence) electron count; noble gases close at the s²p⁶ octet |
| **periodicity** | row (period) lengths = 2, 8, 8, 18, 18, 32, 32 — set by the Madelung (n+l) filling order, NOT by sequential n = 1, 2, 3, 4 shell completion |
| **shell closures** | noble-gas atomic numbers Z = 2, 10, 18, 36, 54, 86, 118 |

The crucial observation: the *maximum shell capacity* 2n² and the *observed period lengths* are
different sequences. Capacity-2n² sequential filling would close shells at Z = 2, 10, 28, 60 (wrong);
the actual noble gases close at 2, 10, 18, 36, 54, 86, 118 because the d/f blocks fill out of
principal-shell order (Madelung). Both facts — the n² degeneracy and the (n+l) ordering — are content
of the central-field Schrödinger solution of real atoms.

---

## 2. Do electron shells correspond to AT objects?

| Candidate | Correspondence |
|---|---|
| **resonance layers** | REFUTED as a derivation — the D96 spectrum's layers are octave bands with occupancy [4, 4, 87]; its BFS shell profile is {1, 12, 12, …}; neither equals the electron shell ladder 2, 8, 18, 32 |
| **organization levels** | REFUTED as a derivation — the NP_101 hierarchy (particle → atom → molecule → crystal) organizes *bound structures at different scales*, not the *internal electron layers of one atom* |
| **locking hierarchies** | CORRESPONDENCE only — binding = resonance locking (NP_100) explains that an atom EXISTS as a locked deficit cluster; it supplies no law for how many electron resonances can lock at each internal level (2, then 8, then 18…) |
| **hydrogenic shell structure** | CORRESPONDENCE — the empirical shell capacities 2n² are the central-field Coulomb spectrum of real atoms, i.e. content imported from quantum mechanics, matching the atomic/molecular-domain verdict of NP_017 (no D96 structure expected) |

**Electron shells are not any AT-native object.** They are the eigenvalue ladder of the 3D
central-field (Coulomb) problem: n² = Σ(2l+1) orbital states per principal shell × 2 spin states.
The same structural obstruction that leaves nuclear shells MISSING (NP_087/089: exact (2l+1)
degeneracies need exact O(3), which the D96-derived cubic structure does not provide) blocks an
AT derivation of electron shells. AT explains *that atoms exist* (binding, NP_100) and *that they
organize into molecules, crystals, and larger structures* (hierarchy, NP_101) — but not the internal
shell ladder that orders the elements.

---

## 3. Test — H, He, Li, Ne, Ar

| Element | Z | Observed configuration | Observed shell counts |
|---|---|---|---|
| **H** | 1 | 1s¹ | [1] |
| **He** | 2 | 1s² | [2] |
| **Li** | 3 | 1s²2s¹ | [2, 1] |
| **Ne** | 10 | 1s²2s²2p⁶ | [2, 8] |
| **Ar** | 18 | 1s²2s²2p⁶3s²3p⁶ | [2, 8, 8] |

These observed configurations are hydrogenic central-field facts (orbital occupation, s/p filling,
Pauli exclusion). AT organization alone supplies only "a bound deficit cluster holds Z electron
resonances"; it does not assign them to shells of capacity 2, 8, 18, 32.

---

## 4. AT shell counts vs observed shell counts

| Candidate "AT shell count" | Value | vs observed |
|---|---|---|
| octave-band occupancy of D96 | [4, 4, 87] | NO (first layer 4 ≠ 2; no 2n² ladder) |
| C96 BFS shell profile | {1, 12, 12, …} | NO (not 2, 8, 18, 32) |
| D96 mode count / mirror pairs | 95 / 47 pairs | NO (not a layered capacity law) |
| principal-shell capacity 2n² | 2, 8, 18, 32 | observed — but this IS the hydrogenic n² degeneracy × spin, i.e. the Schrödinger spectrum of the Coulomb atom, imported, not derived from Difference → D96 |

**No AT-native counting yields the observed shell counts.** The only candidate that reproduces them is
the hydrogenic capacity law 2n² = 2·Σ(2l+1), which is the answer to the Schrödinger equation for a
central Coulomb potential — precisely the content the audit asks AT to avoid importing.

---

## 5. Does AT predict 2, 8, 18, 32 naturally?

**No.** The sequence 2, 8, 18, 32 = 2n² decomposes as

```
capacity(n) = 2n²  =  2 spin states × n² orbitals
   n² orbitals  =  Σ_{l=0}^{n-1} (2l+1)   (1, 4, 9, 16 for n = 1..4)
```

The n² (equivalently 2l+1) degeneracy is the rotational-shell structure of the 3D Coulomb central
field. AT has no native source of it:
- the D96 ring is 1D with O(2) mirror-pair degeneracies (47 pairs), not (2l+1) spherical shells;
- the D96⊗3 cubic lattice is O_h with irreps {1, 2, 3}, not the {1, 3, 5, 7} of O(3) — exact O(3)
  is only approximate (NP_089), the same reason the nuclear magic numbers are not reproduced
  (NP_087);
- spin (the ×2) and Pauli exclusion enter AT as the Z2-paired sector / hosted statistics, not as a
  derived shell-filling rule.

A resonance ladder would repeat octave-wise (2, 4, 8, 16…) or family-wise (3, 3, …), never as the
quadratic 2n² that observation demands. Therefore **2, 8, 18, 32 are imported hydrogenic content, not
an AT prediction.**

---

## Theorem

> **Theorem (NP_166).** The periodic table does NOT emerge from AT resonance organization alone; its
> defining structure — the electron shell capacities 2, 8, 18, 32 = 2n² — is the hydrogenic
> (central-field Coulomb) spectrum, i.e. content imported from the solution of the Schrödinger
> equation. Electron shells are not resonance layers (D96 octave bands give [4, 4, 87], not
> 2, 8, 18, 32), not organization levels (the NP_101 hierarchy organizes structures at different
> scales, not the internal layers of one atom), and not locking hierarchies (binding explains that an
> atom exists, not how many electrons occupy each internal level). What AT does derive — the atom as a
> resonance-locked deficit clustering (NP_100) and its organization into molecules, crystals, and
> larger structures (NP_101) — leaves the shell ladder, the period lengths, and the valence chemistry
> as hosted QM content. The structural reason is the same as for nuclear shells (NP_087/089): exact
> (2l+1) spherical degeneracies require exact O(3), which the D96-derived structure provides only
> approximately. Classification: atom binding DERIVED (NP_100/101, unchanged); electron shells as
> resonance layers / organization levels REFUTED as derivations; shell capacities 2, 8, 18, 32 =
> 2n² BOUNDARY-class (imported hosted values, the 13.6 eV/m_e-anchor pattern); the periodic table as a
> whole CORRESPONDENCE (hosted QM structure, confirming NP_017's atomic/molecular verdict and
> extending NP_085's frontier: chemistry does not join the derived core); "AT predicts 2, 8, 18, 32
> naturally" REFUTED. Proof: (1) Inventory (Section 1). (2) Shells vs AT objects (Section 2,
> verified — no identity). (3) Test elements (Section 3, observed = hydrogenic). (4) AT vs observed
> shell counts (Section 4, verified — no AT-native count matches). (5) The 2n² law (Section 5,
> verified — hydrogenic degeneracy). ∎
>
> *Proof sketch.* (1) Inventory. (2) Test candidate correspondences. (3) Test H/He/Li/Ne/Ar.
> (4) Compare counts. (5) Trace 2, 8, 18, 32 to the Coulomb spectrum. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "electron shells are AT resonance layers" | D96 octave layers carry [4, 4, 87]; no octave/family sequence gives 2, 8, 18, 32 |
| "shells are organization levels of the hierarchy" | the NP_101 hierarchy stacks structures across scales; one atom is ONE level, not a layered electron ladder inside a level |
| "binding tells us how full each shell can be" | resonance locking (NP_100) explains boundness/persistence; it has no 2n² capacity rule |
| "AT predicts 2, 8, 18, 32 naturally" | 2n² = 2·Σ(2l+1): the spherical-harmonic (2l+1) degeneracy and the spin ×2 are hydrogenic/Schrödinger content, structurally unavailable from the 1D/cubic D96 (NP_087/089) |
| "the periodic table is a derived AT structure" | observed period lengths 2, 8, 8, 18, 18, 32, 32 require the Madelung (n+l) ordering of the many-electron central field — imported QM, not Difference → D96 |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| electron shells are not AT-native | a D96-derived layered capacity law reproducing 2, 8, 18, 32 (and the noble-gas Z sequence) from Difference → D96 alone |
| the shell capacities are imported | a derivation of n² (2l+1) spherical degeneracy or of the spin ×2 shell-filling from the canonical ring/network structure |
| the periodic table is correspondence | a chemistry observable (ionization pattern, valence, period length) following from AT organization without importing the Coulomb spectrum |
| chemistry is outside the derived core | an electron-shell or chemical property entering the derived chain of NP_085 |

---

## 8. Classification

| Component | Status |
|---|---|
| the atom as a resonance-locked deficit clustering (binding) | **DERIVED** (NP_100, unchanged) |
| the hierarchy (particle → atom → molecule → crystal) | **DERIVED** (NP_101, unchanged) |
| electron shells as resonance layers / organization levels | **REFUTED** (no AT-native shell ladder) |
| electron shells as locking hierarchies | **CORRESPONDENCE** (binding explains existence, not shell capacity) |
| shell capacities 2, 8, 18, 32 = 2n² | **BOUNDARY-class** (imported hydrogenic hosted values; the 13.6 eV / m_e-anchor pattern) |
| the periodic table (periods, valence, noble gases) | **CORRESPONDENCE** (hosted QM structure; NP_017 confirmed, NP_085 frontier extended) |
| "AT predicts 2, 8, 18, 32 naturally" | **REFUTED** |

**Conclusion.** The periodic table cannot be derived from AT resonance organization alone. AT explains
that atoms exist (particles = resonance classes locking into deficit clusters, NP_072/100) and that
matter organizes hierarchically (NP_101), but the element-ordering structure — electron shells with
capacities 2, 8, 18, 32 = 2n², Madelung periodicity, valence chemistry — is the hydrogenic spectrum
of the Coulomb central field, imported from quantum mechanics. Electron shells are neither resonance
layers nor organization levels nor locking capacities of the D96 ontology. The observed shell counts
are reproduced only by the Schrödinger-equation content the audit was asked to avoid importing. This
confirms NP_017's classification of the atomic/molecular domain as CORRESPONDENCE and extends NP_085's
frontier: chemistry joins nuclear structure and condensed matter as structure not generated by the
Difference → D96 chain. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_166_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_166_Inventory` | shell capacities 2n² = 2, 8, 18, 32; periods 2, 8, 8, 18, 18, 32, 32; noble gases 2, 10, 18, 36, 54, 86, 118 | ✅ |
| `Y_NP_166_NotResonanceLayers` | D96 octave layers [4, 4, 87] ≠ electron shells | ✅ |
| `Y_NP_166_NotOrganizationLevels` | hierarchy stacks structures, not intra-atom shells | ✅ |
| `Y_NP_166_TestElements` | H [1], He [2], Li [2, 1], Ne [2, 8], Ar [2, 8, 8] | ✅ |
| `Y_NP_166_AtVsObserved` | no AT-native count matches the observed shells | ✅ |
| `Y_NP_166_NumberLaw` | 2, 8, 18, 32 = 2n² = 2·Σ(2l+1) (hydrogenic) | ✅ |
| `Y_NP_166_ABCD` | binding/hierarchy DERIVED; shell derivation REFUTED | ✅ |
| `Y_NP_166_Classification` | shells REFUTED; capacities imported; table CORRESPONDENCE | ✅ |
| `Y_NP_166_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_166"`

---

## References

- ResearchY-NP_072 (particle = resonance class), NP_100 (binding = resonance locking), NP_101
  (hierarchy = repeated organization), NP_105 (Difference conservation), NP_017 (atomic/molecular
  CORRESPONDENCE), NP_085 (completeness frontier), NP_087 (nuclear shell structure MISSING), NP_089
  (O(3) approximate only).
- Observed shell capacities and configurations are the empirical hydrogenic content of real atoms
  (principal shells, Madelung ordering, noble-gas closures).
