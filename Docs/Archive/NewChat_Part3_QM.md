# AT Project Memory — Archive Part 3: Quantum-Mechanical Correspondence (group QM)

Historical detail split out of `Docs/NewChat_Start.md` (2026-10-11), moved **verbatim**: the many-body / quantum
correspondence audits ResearchY-QM_001 - QM_011, which closed the spectral-fingerprint frontier.

Current state: `Docs/NewChat_Start.md`. Audit catalogue: `Docs/AuditIndex.md`.

---
## ResearchY-QM_011 - Fingerprint Justification Audit (COMPLETE, NONE - no physical consequence is native-specific)

**Question.** **What surviving result requires the exact native spectrum rather than only its existence?** Compare the
native **{1..6}** against the nearest-neighbour **{1}**, search **all** surviving claims, classify each
**REQUIRES_NATIVE / REQUIRES_SPECTRUM_ONLY / INDEPENDENT**, and find the **first genuinely physical consequence** of
the native fingerprint - **and if none exists, state that explicitly**.

**Answer.** **NONE - AND THE AUDIT STATES IT EXPLICITLY: NO GENUINELY PHYSICAL CONSEQUENCE OF THE NATIVE FINGERPRINT
EXISTS.** **7 REQUIRES_NATIVE + 2 REQUIRES_SPECTRUM_ONLY + 4 INDEPENDENT, and the count of claims that BOTH require
the native spectrum AND carry an observable is 0.**

**The classification is a rule with three measured inputs** - whether the predicate **fails on {1}**, whether the
claim's own files **name eigen-spectral objects** (a code-only scan), and whether its symbol appears in the
**repository's own observable inventory** - so nothing is classified by reading a claim's prose.

| claim | holds {1..6} | holds {1} | native value | {1} value | need | observable |
|---|---|---|---|---|---|---|
| **the clock law ρ^(1/d)** | yes | yes | `d = 3`, `rate(8) = 2` | identical | **INDEPENDENT** | **yes** |
| **the redshift law 1 + z = exp(−x)** | yes | yes | 2.123049E-006 | 2.123049E-006 | **INDEPENDENT** | **yes** |
| the flux quantum 2π/96 | yes | yes | 0.065449846950 | 0.065449846950 | **INDEPENDENT** | no |
| the flow conserves the norm | yes | yes | 8.9E-016 | 1.3E-015 | **INDEPENDENT** | no |
| the long-wavelength power law is 2 | yes | yes | 2.0000 | 2.0000 | SPECTRUM_ONLY | no |
| the kernel is a union of Fourier modes | yes | yes | no split modes | no split modes | SPECTRUM_ONLY | no |
| the amplitude/phase split is 1 + 42 + 53 | yes | **no** | 1 + 42 + 53 | 1 + 46 + 49 | **REQUIRES_NATIVE** | no |
| the free room is 51 | yes | **no** | 51 | 47 | **REQUIRES_NATIVE** | no |
| the trace is 1152 | yes | **no** | 1152 | 192 | **REQUIRES_NATIVE** | no |
| the level count is 45 | yes | **no** | 45 | 49 | **REQUIRES_NATIVE** | no |
| the maximum eigenvalue is 15.837372 | yes | **no** | 15.837372 | 4.000000 | **REQUIRES_NATIVE** | no |
| the Schrödinger coefficient is 91 | yes | **no** | 91 | 1 | **REQUIRES_NATIVE** | no |
| the dispersion folds at channel 11 | yes | **no** | 11 | 0 (no fold) | **REQUIRES_NATIVE** | no |

**And the reason is structural and measured rather than interpretive: the repository's 11-entry observable inventory
contains NO SPECTRAL OBJECT** - six external observables (`lensing-deflection`, `time-delay`, `magnification`,
`horizon-shadow`, `hawking-temperature`, `gw-strain`) and five pure temporal ones (`ClockRateAT`, `ClockRateGR`,
`RedshiftAT`, `RedshiftGR`, `Discriminator`), every one a clock reading, a redshift, a deflection, a delay, a
magnification, a shadow, a temperature or a strain. **A claim can only require the exact spectrum by quantifying over
spectral objects, so the observability test cannot be passed by anything the fingerprint fixes.**

**The near miss is named rather than glossed:** the fold at **11 against 0**, **23 channels with a reversed group
velocity against 0**, peak **|group velocity| 36.859293 against 2.000000** - a **real dynamical difference**, and
**none of it observable**, because no inventory observable reads the dispersion at high wavenumber. **And my own
expectation there was refuted:** I expected every channel from the fold to the zone edge (**37**) to reverse; the
measurement gives **23**, because the symbol is a sum of **six** sinusoids and the group velocity **oscillates back
above zero** - a single fold position does not bound the reversed region.

**What the fingerprint is actually justified by is the realisation**, named in full: the mode table (**42 + 53** against
**46 + 49**), the free room (**51** against **47**), the trace (**1152** against **192**), the level count (**45** against
**49**), the maximum eigenvalue (**15.837372** against **4.000000**) and the fold position (**channel 11** against **no
fold**). **Every one is a number the theory quotes and not one is a number an observer measures** - so the native
spectrum is **an input the theory is entitled to take and not a physical necessity.**

**The qualification is stated rather than buried:** this is a statement about the **current** observable inventory, not
a theorem about all conceivable ones. An observable whose value depended on the dispersion inside the band would make
the fold's position **observable** and this verdict **conditional**, which is why the fold's dynamical signature is
reported explicitly.

**Status.** COMPLETE. Tests `Y_QM_011_Tests` **6/6**. Core `AT.Core/ResearchXH/FingerprintJustificationAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_011.md`. Registry: added as **Boundary** (`fingerprint-justification-audit`),
reclassifying nothing. **No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after the
new core was added.

## ResearchY-QM_010 - Fingerprint Load Audit (COMPLETE, FINGERPRINT - numerical realisation, not physical content)

**Question.** Which surviving AT claims **actually depend** on the native **{1..6}** spectrum? For the **clock law**,
the **redshift law**, the **source law**, the **amplitude/phase split**, the **kernel theorem**, the **observability
theorem**, the **phase accessibility** and the **flux quantisation**: remove the fingerprint-specific constants, compare
**theorem content only**, and classify **UNCHANGED / NUMERICALLY_CHANGED / STRUCTURALLY_CHANGED / REFUTED** with a load
label **CORE / FINGERPRINT / ARTEFACT**.

**Answer.** **FINGERPRINT - THE SPECTRUM CARRIES NUMERICAL REALISATION AND NOT PHYSICAL CONTENT: 3 CORE +
5 FINGERPRINT + 0 ARTEFACT**, and by theorem classification **3 UNCHANGED + 3 NUMERICALLY_CHANGED +
2 STRUCTURALLY_CHANGED + 0 REFUTED**. **The zero REFUTED is the central positive result, not an absence of findings:**
for every claim the fingerprint touches, the claim is still **true** on the replacement - it supplies the
**realisation** of every claim it touches and the **truth** of none.

**The rules come first and are functions of measured booleans and measured constants**, so no claim is classified by
reading its own text:

```
UNCHANGED  the theorem holds on both and its constants are identical
NUMERICALLY_CHANGED  it holds and its constants move
STRUCTURALLY_CHANGED  it holds but the OBJECTS it names change identity
REFUTED  it fails on the replacement
CORE  nothing it quotes moves | FINGERPRINT  its constants or objects move
ARTEFACT  its predicate is vacuous over the 63-subset family while its number moves
```

| claim | holds {1..6} | holds {1} | constants | objects | classification | load |
|---|---|---|---|---|---|---|
| **clock law** | yes | yes | no | no | **UNCHANGED** | **CORE** |
| **redshift law** | yes | yes | no | no | **UNCHANGED** | **CORE** |
| **flux quantisation** | yes | yes | no | no | **UNCHANGED** | **CORE** |
| source law | yes | yes | yes | yes | **STRUCTURALLY_CHANGED** | FINGERPRINT |
| amplitude/phase split | yes | yes | yes | yes | **STRUCTURALLY_CHANGED** | FINGERPRINT |
| kernel theorem | yes | yes | yes | no | **NUMERICALLY_CHANGED** | FINGERPRINT |
| observability theorem | yes | yes | yes | no | **NUMERICALLY_CHANGED** | FINGERPRINT |
| phase accessibility | yes | yes | yes | no | **NUMERICALLY_CHANGED** | FINGERPRINT |

**The CORE block is the physical content and it is substrate-free:** the clock law (`d = 3`, `rate(8) = 2`, with the
redshift identity following from it), the redshift law (`z_AT = 2.123049E-006` always below `z_GR = 2.123054E-006`)
and the **flux quantisation**. **The flux is the sharpest case:** its quantum **2π/96 = 0.065449846950** and its unit
holonomy **2π = 6.283185307180** are functions of the ring's **length**, so **the same 96 cells would quantise the
flux identically under any admissible generator** - the fingerprint is not merely unneeded there, it is
**unreachable**, and the statement is **topological** rather than numerical.

**The FINGERPRINT block is true claims with substrate-fixed realisations:** the source law and the amplitude/phase
split are **STRUCTURALLY_CHANGED** (`1 + 42 + 53` against `1 + 46 + 49`), while the kernel theorem, the observability
theorem and the phase accessibility are **NUMERICALLY_CHANGED** (`53` against `49`).

**And the audit's own draft prediction was refuted.** I put the **observability theorem** in the CORE block, reasoning
that its content quotes no substrate number. Its **predicate is indeed substrate-free** (zero silent directions on
both, minimum clock response **6.531E-003** against **6.557E-003**, every response first order) - **but the kernel it
quantifies over is not**, so the claim as stated is **NUMERICALLY_CHANGED** with load **FINGERPRINT**. Stripping the
number leaves the predicate intact, which is why it is near-CORE; the measured label is the one reported.

**No named claim is an artefact, and the audit says so rather than forcing the category.** The label is mechanical -
a predicate **vacuous over the whole 63-subset family** carrying a number that **moves** - and three **controls**
demonstrate it: **the free room** (51 against 47, **14** distinct values), **the trace** (1152 against 192, **6**),
**the level count** (45 against 49, **14**). Each is true for every one of the 63 subsets **by definition** while its
number is a pure function of the substrate choice: **a number dressed as content.** The label is not free - a
predicate that **can** fail is not an artefact whatever its number does.

**A performance defect in QM_008 was found and fixed.** `SeenDirections` was rebuilt inside `ModeShare` **once per
Fourier mode** (**96 Gram-Schmidt rebuilds per candidate**). It is now memoised per substrate, together with the
canonical state, the kernel basis and the observability reading. **The QM block fell from ~30 s to 10 s for 22 tests
with no result changing.**

**Status.** COMPLETE. Tests `Y_QM_010_Tests` **6/6**. Core `AT.Core/ResearchXH/FingerprintLoadAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_010.md`. Registry: added as **Boundary** (`fingerprint-load-audit`),
reclassifying nothing. **No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after
the new core was added.

## ResearchY-QM_009 - Fingerprint Necessity Audit (COMPLETE, BOUNDARY - no prediction fails)

**Question.** Which AT predictions **fail** if the spectral fingerprint is replaced by the Schrodinger-optimal
generator **{1}**? Compare the native **{1..6}** against **{1}** across five sectors - the **redshift sector**, the
**clock law**, the **source law**, the **phase sector** and the **observable algebra** - and classify each
**UNCHANGED / BOUNDARY / REFUTED**.

**Answer.** **BOUNDARY - AND THE SHARP FORM OF THE ANSWER IS THAT NO AT PREDICTION FAILS: 1 UNCHANGED + 4 BOUNDARY +
0 REFUTED.** The sector that is unchanged is the only one whose output is a **measurement**, and the four that move
move for one reason: the canonical **state** is spectrum-derived, so every quantity evaluated **on** the state moves,
while every **law** stated in terms of the state's scalars does not.

**The first instrument is a live source scan** (the G_027/G_033 pattern), reading each sector's own files at test time
and counting substrate tokens in **code** and in **comments** separately:

| sector | in code | in comments | files | scan verdict |
|---|---|---|---|---|
| **redshift sector** | **0** | 0 | 2 | **LATTICE-FREE** |
| **clock law** | **0** | 0 | 2 | **LATTICE-FREE** |
| source law | 5 | 0 | 1 | READS THE SUBSTRATE |
| phase sector | 8 | 0 | 2 | READS THE SUBSTRATE |
| observable algebra | 59 | 0 | 2 | READS THE SUBSTRATE |

**A zero is negative evidence and the audit treats it as such** - a sector whose code cannot name the spectrum cannot
fail when the spectrum is replaced - while noting that **a lattice-free file can still produce a moving quantity when
the thing that moves is its input**.

**The redshift sector is completely immune.** Solar compactness **−2.123047E-006**, AT redshift **2.123049E-006**, GR
redshift **2.123054E-006**, the ratio law **0.2599210499**, the second-order coefficients **0.5 / 1.5** and the
required compactness precision **0.03661** are the **same numbers on both substrata**, because the chain consumes a
**dimension** and **occupancies** and the one scalar the state supplies - the density normalisation, **96** - is itself
`|S|`-independent (QM_008).

**The clock law is the cleanest illustration of the whole finding.** Its own file is **LATTICE-FREE** and the law
`ρ^(1/d)` is the same function of the same two numbers on both substrata, **yet the pattern it induces moves** -
maximum relative shift **1.013E-001** and correlation **0.222310**, i.e. nearly **decorrelated**, matching QM_008's
state correlations of 0.19 to 0.24.

**The source law is where the audit's own draft hypothesis was refuted, and it is reported as refuted.** The draft
assumed the **ranking** of the candidate sources and the **phase-null classification** were the structural content.
**Both move:** the phase-imbalance candidate goes from **exactly 0.0000000000** to **0.4671052634** and the
amplitude-phase-coupling candidate from **exactly 0.0000000000** to **0.0813500268**, so the ranking is not preserved,
and the phase-null membership falls from **4 candidates to 1**. **What survives is the fixed point and only the fixed
point:** the uniform actualization pressure has norm **9.7979589711 = √96 on both substrata**, because a constant push
has no phase part and no occupancy gradient. **That is a weaker structural core than G_059 assumed, and it is reported
as weaker.**

**The observable algebra shows both directions at once.** Its **dimension is 49 on both**, and that is a **theorem
rather than a coincidence**: every `|k|` belongs to exactly one level, so restricting the orbital algebra to the levels
counts each `|k|` once whatever the partition - **49 from 45 levels** natively and **49 from 49 levels** for `{1}`,
from two genuinely different partitions. The multiplicity-weighted **content** moves: sum of multiplicity squares
**230 → 190**, protected dimensions **181 → 141**, level-population observable **45 → 49**. The phase sector moves
**53 → 49** and **42 → 46**, with **0** partially hidden modes on both.

**Six defects in the audit's own first version are recorded.** (1) The **redshift comparison was backwards** - I
asserted `z_AT > z_GR` and AT is the **smaller**, which G_068 recorded. (2) The **ratio-law literal was wrong**:
**2^(1/3) − 1 = 0.259921049895**, not 1.0. (3) The **mean clock rate was asserted as exactly 1** from the mean
occupancy being 1; it is **0.99879755714637009**, and the gap is **Jensen's inequality**. (4) A **mistyped computed
constant**: the Jensen gap is **0.99639700730968433**, first written 0.9963974978. (5) The **structural hypothesis
about the source law is refuted by the measurement**. (6) **Two clock numbers were written into the document before
being measured** - **1.484E-001** and **0.981666** against the measured **1.013E-001** and **0.222310** - and the
second is not a rounding difference but the **opposite conclusion**, since 0.222310 says the patterns are nearly
decorrelated where 0.981666 would have said the shape survives. **Rule 5 in miniature.**

**Status.** COMPLETE. Tests `Y_QM_009_Tests` **8/8**. Core `AT.Core/ResearchXH/FingerprintNecessityAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_009.md`. Registry: added as **Boundary** (`fingerprint-necessity-audit`),
reclassifying nothing. **No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after the
new core was added.

## ResearchY-QM_008 - Spectral Necessity Audit (COMPLETE, BOUNDARY - indispensable for the numbers, dispensable for the structure)

**Question.** Which AT conclusions **actually require** the native **{1..6}** spectrum? Recompute each using **{1}**,
**{1,2}** and **{1..6}**, classify every QM/G result **UNCHANGED / BOUNDARY / REFUTED**, and determine whether the
spectral fingerprint is **physically indispensable** or only **historically inherited**.

**Answer.** **BOUNDARY - THE FINGERPRINT IS INDISPENSABLE FOR THE NUMBERS AND DISPENSABLE FOR THE STRUCTURE.** Of the
**12** conclusions recomputed on all three generators, **5 are UNCHANGED**, **5 are BOUNDARY** and **2 are REFUTED** -
so **5 of 12 do not mention the spectrum anywhere, and 7 require it** (5 for their value, 2 for their existence).

**The recomputation had to rebuild the state, and that is the whole difficulty.** The canonical state is built by
walking the **spectrum's levels**, so **replacing the generator replaces the state**. The audit rebuilds the levels,
the level basis, the canonical state and the observable split from each candidate spectrum, and checks the rebuild
twice: the rebuilt native state reproduces the **recorded state bitwise (0.000E+000)**, and the rebuilt native split
lands on **all five** quantities other audits recorded independently (observable rank **43**, amplitude **42**, phase
**53**, kernel **53**, contraction rank **42**).

**The state is not perturbed, it is replaced:**

| pair | max shift | L2 shift | correlation |
|---|---|---|---|
| {1} vs {1,2} | 0.370396 | 1.419953 | **0.221376** |
| {1} vs {1..6} | 0.346756 | 1.274251 | **0.236824** |
| {1,2} vs {1..6} | 0.420200 | 1.419054 | **0.189997** |

The three canonical states are **nearly orthogonal**. **An assumption of my own draft - that the replacement would be
a small perturbation, asserted at a correlation above 0.9 - is refuted by that measurement and recorded.**

**The partition:**

| conclusion | source | **{1}** | **{1,2}** | **{1..6}** | classification |
|---|---|---|---|---|---|
| the split is mean + amplitude + phase | QM_001 | 1+46+49 | 1+44+51 | 1+42+53 | **BOUNDARY** |
| the observable rank is 43 | QM_001 | 47 | 45 | 43 | **BOUNDARY** |
| ρ = \|Ψ\|² exactly | QM_001 | 2.2E-016 | 2.2E-016 | 2.2E-016 | UNCHANGED |
| the state totals the cell count | QM_001 | 96.0000 | 96.0000 | 96.0000 | UNCHANGED |
| the flow conserves the norm exactly | QM_002/QM_006 | 1.3E-015 | 8.9E-016 | 8.9E-016 | UNCHANGED |
| the mode occupation is conserved | QM_002 | 8.9E-016 | 1.6E-015 | 1.3E-015 | UNCHANGED |
| the power law is 2 | QM_004/QM_005 | 2.0000 | 2.0000 | 2.0000 | UNCHANGED |
| the Schrödinger coefficient is Σr² | QM_004 | 1 | 5 | 91 | **BOUNDARY** |
| the quartic coefficient is Σr⁴/12 | QM_005 | 0.08 | 1.42 | 189.58 | **BOUNDARY** |
| the dispersion folds inside the band | QM_003/QM_005 | none | ch 28 | ch 11 | **REFUTED** |
| the packet tracks Schrödinger for a window | QM_006/QM_007 | 514 | 153 | 23 | **BOUNDARY** |
| the recorded fingerprint | QM_007 | 192/49/47/4.000 | 384/47/49/6.250 | 1152/45/51/15.837 | **REFUTED** |

The **five structural rows are facts about the ring** (96 cells, a reflection pairing, a circulant structure, a **real**
symbol) and every candidate has all four. The **numerical rows keep their form and lose their values** - what a
dimensional constant does when its substrate moves.

**One row's tolerance is not the comparison floor, and the audit says so rather than rounding it away:** the power-law
fits give **1.999999996 / 1.999999988 / 1.999999910**, differing in the **eighth digit** because the quartic term
contaminates each fit differently. That is the fit's own noise, so the row is compared at the fit's **1E-6** and the
raw values are printed beside the table.

**The split moves, which refutes a claim of QM_007.** QM_007 asserted the **42/53 split is `|S|`-independent** - read
from a **mask-free helper**, which returns the native answer by construction - whereas the split is a property of the
**state's kernel** and the state is spectrum-derived. The measurement: **1 + 46 + 49** for `{1}`, **1 + 44 + 51** for
`{1,2}`, **1 + 42 + 53** natively. **What survives is the structural half:** every candidate's kernel is still a
**union of Fourier modes** (**zero split modes**, exact partition, in all three) - so the classification *mechanism* is
`|S|`-independent while the *membership* is not. A **refinement note** is attached to the QM_007 doc and registry entry.

**REFUTED means "does not survive the replacement", NOT "false".** The fold **does not exist on `{1}` at all**
(folds **none / ch 28 / ch 11**), so the fold is a property of the six-shell **choice** rather than of AT's flow; and
the **recorded fingerprint** is not re-derivable from any other generator - QM_007's identification stated from the
other side.

**Six defects in the audit's own first version are recorded.** (1) **The leakage helper evolved the packet, not the
mode** - `EvolveUnder` ignores its argument, so feeding it a single Fourier mode measured the packet's overlap with
channel 5 (**1.9E+000**) and **would have reported occupation conservation REFUTED on all three candidates**; the mode
is now evolved explicitly (**8.9E-016 to 1.6E-015**). (2) The power law was compared at **1E-9**, tighter than the
fit's own precision, so the row classified BOUNDARY **on noise**. (3) My own draft assertion of a state correlation
**above 0.9** is refuted by the measured **0.19 to 0.24**. (4) **A literal reached a verdict**: the quartic coefficient
of `{1}` was asserted as QM_005's **printed** `0.08` where the value is **1/12 = 0.083333**, and the test failed - it
is now asserted against the **closed form** recomputed in the test. (5) An **off-by-one** in the rebuilt split gave
the native **42 / 41 / 54** against the recorded **43 / 42 / 53**; the correct reading is observable = seen,
amplitude = seen − 1, phase = 96 − seen.

**Status.** COMPLETE. Tests `Y_QM_008_Tests` **8/8**. Core `AT.Core/ResearchXH/SpectralNecessityAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_008.md`. Registry: added as **Boundary** (`spectral-necessity-audit`), with a
refinement note on QM_007. **No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners
after the new core was added.

## ResearchY-QM_007 - Generator Selection Audit (COMPLETE, BOUNDARY - the constraint is an input, not a law)

**Question.** Why does AT use the six-shell generator **{1..6}** instead of the Schrodinger-compatible **{1}**?
Compare {1}, {1,2} and {1..6}, measuring **locality**, the **dispersion error**, the **fold position**, **packet
evolution** and **the existing AT requirements**, and identify which AT constraint forces the native generator away
from the Schrodinger optimum.

**Answer.** **BOUNDARY - the constraint is the SUBSTRATE'S OWN SPECTRAL FINGERPRINT, and it is an INPUT rather than a
dynamical law.** **The identification is computed:** the recorded **trace 1152 = 2 x 6 x 96** fixes the shell
**count** at six, and the recorded **45 levels** with maximum **15.837372** fix **which** six. **No propagation
requirement is involved anywhere in that selection.**

**The trace alone counts the shells.** A circulant Laplacian's trace is **2 |S| x 96**: {1} gives **192**, {1,2} gives
**384**, {1..6} gives **1152** against the recorded **1152** - out by factors of six and three. Of the **63** subsets,
**1** matches.

**And the rest of the fingerprint fixes which six:**

| requirement | recorded | {1} | {1,2} | {1..6} | discriminates |
|---|---|---|---|---|---|
| trace | 1152 | 192 | 384 | **1152** | yes |
| distinct levels | 45 | 49 | 47 | **45** | yes |
| free room | 51 | 47 | 49 | **51** | yes |
| maximum eigenvalue | 15.837372 | 4.000000 | 6.249689 | **15.837372** | yes |
| reproduces all 96 eigenvalues | yes | no | no | **yes** | yes |
| amplitude/phase split 42/53 | 42 / 53 | 42 / 53 | 42 / 53 | 42 / 53 | **NO** |

Census: **1** matches the trace, **9** match the level count, **exactly 1** reproduces the whole spectrum.

**And one required structure does not discriminate at all, which is reported rather than omitted.** The **42/53
amplitude/phase split is |S|-INDEPENDENT** - it follows from the ring's reflection pairing and the canonical state's

> **REFINEMENT (QM_008, 2026-09-17).** The `|S|`-independence claim above was read from a **mask-free helper**, which
> returns the native answer by construction. QM_008 rebuilt the levels, the level basis, the canonical state and the
> observable split from each candidate spectrum, and the split **MOVES**: **1 + 46 + 49** for `{1}`, **1 + 44 + 51**
> for `{1,2}` and **1 + 42 + 53** natively. **QM_007's `|S|`-independence claim is REFUTED.** What survives is the
> structural half - every candidate's kernel is still a union of Fourier modes, with zero split modes and an exact
> partition. The rest of this section is unaffected. **SEE QM_008.**
construction rather than from which neighbours are summed - so **a requirement that looks load-bearing here selects
nothing**.

**Nothing dynamical selects the native set.** Only **two** of the five measures prefer it - **locality** (**2 / 4 /
12** non-zeros per row) and **the requirements themselves** - **and the first follows from the second**, because a
12-regular graph is local by construction. Everything else prefers the singleton: dispersal error at the zone edge
**59.47 % / 91.89 % / 98.66 %**, folds **none / 28 / 11**, packet windows **514 / 153 / 23** (a factor of **22**),
distances at t = 20 of **0.004149 / 0.013906 / 0.088008**.

| | t = 10 | 20 | 40 |
|---|---|---|---|
| **reference (ω = k²)** | 4.7170 | 6.4031 | 10.7702 |
| **{1}** | 4.7067 | 6.3729 | 10.6983 |
| {1,2} | 4.6829 | 6.3024 | 10.5299 |
| native {1..6} | 4.5122 | 5.7828 | 9.2609 |

The ordering is **monotone in the shell count** at every time. **The question's premise is confirmed: {1} really is the
Schrodinger optimum, and what AT cannot do is use it** - the substrate's spectrum is the record from which the free
room **51**, the mode table and the 42/53 decomposition were derived. **The fold is the signature of the substrate the
theory actually has.**

**Two defects in the audit's own first version are recorded.** (1) The trace comparison used **exact `double`
equality**; the record stores the rounded integer **1152** while the computed symbol sum lands on
**1151.9999999999998**, so a **match was reported as a mismatch** - the repository's rule-1 trap in miniature - and
the comparison now carries a **1e-6** tolerance, with the same helper serving the census and the table. (2) The
table's trace cell was fed the **mask** rather than the trace (**1, 3, 63** instead of **192, 384, 1152**), from a
helper whose parameter was renamed but whose call sites were not; the cells now go through `TraceMatchesTheRecord`,
so **the table and the census cannot disagree by construction**.

**Status.** COMPLETE. Tests `Y_QM_007_Tests` **8/8**. Core `AT.Core/ResearchXH/GeneratorSelectionAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_007.md`. Registry: added as **Boundary** (`generator-selection-audit`),
reclassifying nothing. **No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after
the new core was added.

## ResearchY-QM_006 - Schrodinger Propagator Audit (COMPLETE, PARTIAL - {1} is analogous, the theory is not)

**Question.** Can the **nearest-neighbour generator {1}** produce **`psi(t) = exp(-iHt)psi(0)`** with **Schrodinger-like
dispersion**? Compare the native {1..6}, {1} and the spectral derivative; measure ω(k), the group velocity, **packet
spreading** and **norm conservation** against the reference of **Schrodinger packet evolution**.

**Answer.** **PARTIAL at the level of the theory, and ANALOGOUS for the generator the question asks about.** The
answer to the question as asked is **YES**: **{1} produces `exp(-iHt)psi(0)` with the lattice Schrodinger dispersion**,
norm conservation **exact**, the packet **tracks the continuum spreading law to half a per cent**, and its distance
from the exact Schrodinger solution stays inside **10 %** for **t up to 514.0**. **The theory is PARTIAL because it
does not use {1}.**

**The audit evolves a packet, not a table.** Every generator is **circulant ⇒ diagonal in the Fourier basis**, so the
evolution is **exact** (no time-stepping error), and the reference is the **same packet** under ω = k² — every
difference measured is a lattice effect.

| | t = 5 | 10 | 20 | 40 | 80 |
|---|---|---|---|---|---|
| **reference (ω = k²)** | 4.1908 | 4.7170 | 6.4031 | 10.7702 | 19.8236 |
| **analytic law** | 4.1908 | 4.7170 | 6.4031 | 10.7703 | 20.3961 |
| **{1}** | 4.1879 | 4.7067 | 6.3729 | 10.6983 | 19.7511 |
| native {1..6} | 4.1340 | 4.5122 | 5.7828 | 9.2609 | 17.1761 |
| **spectral** | **4.0000** | **4.0000** | **4.0000** | 4.9323 (seam) | **4.0000** |

The law reproduces the exact reference to **five digits** (and the ring's finite size makes it lag at t = 80).

**Norm conservation:** **6.66E-016** (native), **6.66E-016** ({1}), **0.00E+000** (spectral) — because their symbols are
**real**, hence Hermitian. The **dissipative control** is the only failure at **1.83E-001**, carried **so the test
cannot pass vacuously**.

**The measure that decides — the packet's width:** a Schrödinger propagator **spreads** (ω ∝ k²); an advection
operator **drives** rigidly (ω ∝ k). The spectral candidate's centroid moves **20.000000 cells in t = 20** with width
growth **1.000000** — pure translation.

**The window (10 % distance, bisected):** **{1} 514.0**, native **23.0**, spectral **0.8**, control 2.9. Window ratio
**22.39** vs quartic contamination ratio **25.00** — the same measurement from the dynamical side, **not identically**.

**Distances at t = 20:** {1} **0.004149**, native **0.088008**, spectral **1.384233**.

**Power laws recovered from the propagator side:** 2.0000 ({1}, native, reference) vs **1.0000** (spectral).

**Three defects in the audit's own first version, all in the measure the audit rests on.** (1) The packet's RMS width
was the parameter **over √2**, so it was compared against a law for a different width. (2) The analytic spreading law
had a coefficient **four times too large** — and **the measured reference refused it by 41 %**; the law is now checked
*against* the exact evolution. (3) The second moment was **not unwrapped** about the centroid, inflating the spread
(**6.09 where the truth was 2.83**); the residual seam artefact at t = 40 is **reported, not hidden**. A fourth
correction is textual: the window ratio **tracks** the contamination ratio rather than **being** it.

**Status.** COMPLETE. Tests `Y_QM_006_Tests` **8/8**. Core `AT.Core/ResearchXH/SchrodingerPropagatorAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_006.md`. Registry: added as **Partial** (`schrodinger-propagator-audit`).
**No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after the new core was added.

## ResearchY-QM_005 - Laplacian Dispersion Audit (COMPLETE, BOUNDARY - the power law is forced, the fold is chosen)

**Question.** Can the native **D96(1..6) Laplacian** produce a **Schrodinger-compatible dispersion in any physical
regime**? Is the fold a consequence of the **six-shell geometry** or an **avoidable representation choice**?

**Answer.** **BOUNDARY - the two halves separate cleanly.** The audit enumerates **all 63 non-empty subsets** of the six
shells, and **every one of them is a Laplacian**, so every one is a candidate generator of the same pedigree.

**Half one, DERIVED: the exponent is forced.** Every Laplacian's symbol is a sum of terms `2 − 2cos(rδ)`, each starting
at `(rδ)²`, so the power law is **2.0000 for all 63 subsets** and the coefficient is **D = Σr²** for all 63 — verified
as the small-k limit and as a limit identity (halving δ divides ω by four, to eight digits). **Schrodinger
compatibility at long wavelength is a property of the operator's ORDER, not of the shell choice.**

**Half two, BOUNDARY: the fold is not forced.** A single shell's group velocity is `2r sin(rδ)`, non-negative until
**rδ passes π**, i.e. until **channel 48/r**:

| shell | measured fold | closed form 48/r |
|---|---|---|
| **{1}** | **0 - never** | 48.0 |
| **{2}** | **25** | 24.0 |
| {3} | 17 | 16.0 |
| {4} | 13 | 12.0 |
| {5} | 10 | 9.6 |
| {6} | 9 | 8.0 |

Each is **exactly one channel past** the closed form. **The fold arrives with the second shell** and moves inward.

**The census:** **62 of 63 fold, exactly one never does ({1})**; the **latest fold any subset achieves is channel 28
({1,2})**; the native set folds at **channel 11** with 23 reversed channels. **No shell choice removes the fold from
the band — the choice moves it.**

**The cure and its price:**

| subset | D | fold | window | occupied in window |
|---|---|---|---|---|
| NATIVE {1..6} | 91 | 11 | 3 | **3 of 42** |
| {1,2} | 5 | 28 | 9 | 9 of 42 |
| {2,3,4} | 29 | 15 | 4 | 4 of 42 |
| **one shell {1}** | **1** | **none** | **17** | **16 of 42** |

**Best coverage 16 of 42 at `{1}` — 5.33× the native 3 — at a coefficient of 1 against 91.** Since the cure changes
the effective mass, it is a **different generator**, which is why avoiding the fold is **not free**.

**The continuum expansion predicts the window:** `ω = Dk² − Ek⁴`, `E = Σr⁴/12` → native **D = 91, E = 189.58,
k at 10 % = 0.2191** (window 3) against one-shell **D = 1, E = 0.08, k at 10 % = 1.0954** (window 17): the native
quartic coefficient is **2400× larger**, and the analytic k-regime tracks the measured window **and its order**.

**Three defects in the audit's own first version, recorded.**

1. **The power-law fit was taken at δ = 1e-3 and 2e-3**, where the six-shell quartic term already contributes ~**9E-6**
   — pushing the exponent outside a 1E-6 tolerance so the representation-independence test came out **False**. It is
   now taken at 1E-4 and 2E-4.
2. **The single-shell fold was predicted at 24/r instead of 48/r** — a factor two from conflating the two zone
   normalisations — and **the measurement refused it**.
3. **The dispersion was computed as `2 − 2cos(rδ)` directly, which loses digits to cancellation** in exactly the
   small-δ regime the fit consumes, so the δ → 0 limit test failed. The **half-angle identity**
   `2 − 2cos(rδ) = 4sin²(rδ/2)` is now used **and the error it removes is measured**: the direct form is exact to the
   printed digits at δ = 1E-4…1E-6 and **0.01 % low at δ = 1E-7**. My first prediction of that error was **too
   pessimistic by an order of magnitude**, and the test asserts the measured value.

**Status.** COMPLETE. Tests `Y_QM_005_Tests` **8/8**. Core `AT.Core/ResearchXH/LaplacianDispersionAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_005.md`. Registry: added as **Boundary** (`laplacian-dispersion-audit`).
**No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after the new core was added.

## ResearchY-QM_004 - Schrodinger Correspondence Audit (COMPLETE, PARTIAL - the candidate list has the wrong order)

**Question.** Does AT's unitary flow reproduce the dispersion of the **Schrodinger equation** (reference **ω = k²**)?
Compare the local difference, the centred difference, the Cayley flow and the spectral derivative, measuring the phase
velocity, the group velocity and the dispersion error.

**Answer.** **PARTIAL - the candidate list contains the WRONG DIFFERENTIAL ORDER.** All four named candidates are
**first-order** generators (**ω ∝ k**); the reference is **second-order**. **The generator that does correspond is one
the question does not list: AT's own Laplacian.**

**The decisive measure — the power law `d log ω / d log k`:**

| candidate | power law | Schrodinger-like? |
|---|---|---|
| local difference, centred difference, Cayley flow | **1.0000** | no |
| **spectral derivative** | **1.0000** | **no - exact about the WRONG operator** |
| **AT-native Laplacian (C96(1..6))** | **2.0000** | **yes** |
| nearest-neighbour Laplacian | **2.0000** | yes |

**The velocities** (channel 24, k = π/2; reference **1.570796 / 3.141593**): local & centred **0.636620 / 0.000000**,
Cayley **1.273239 / 0.000000**, spectral **1.000000 / 1.000000**, **native Laplacian 8.912677 / 6.000000**, one-shell
**1.273240 / 2.000000**. The first-order group velocity is **exactly zero** at a quarter of the zone. **The raw error
diverges:** **1426 %** at the first channel (local difference), **2954 %** (Cayley, carrying the factor-two generator
QM_002 measured).

**The native generator, verified rather than assumed.** AT's mode operator is the graph Laplacian of **C96(1..6)**,
symbol `μ(δ) = Σ_{r=1..6}(2 − 2cos rδ)`, which **reproduces the recorded spectrum to 7.11E-15** over 49 channels —
zero mode at channel 0, recorded gap **0.386350893**, **μ(π) = 12**, maximum **15.837372 at channel 11**. Its
long-wavelength expansion is **`ω = D k²` with D = Σr² = 91**: **a Schrodinger propagator with a coefficient the shell
set supplies.**

**The window, taken contiguously from the longest wavelength (10 %):** the four named candidates **0 channels**; the
**native Laplacian 3 channels and 3 of the 42 occupied modes** (0.89 %, 3.51 %, 7.73 %, then 13.35 %); the **one-shell
Laplacian 17 channels and 16 of 42**. Because **six shells interfere**: the native group velocity **peaks at channel 5
and folds at channel 11**, then oscillates with **5 sign changes** and **23** reversed channels.

**Two fold mechanisms, named:** **oddness** (antisymmetry → the symbol vanishes at the zone edge) for the first-order
family; **even-symbol shell interference** for the native Laplacian, whose **μ(π) = 12**.

**Three defects in the audit's own first version, recorded.** (1) The sign-change counter **missed a reversal through
an exact zero** — the first-order family turns over exactly at the fold — so those candidates were counted as having
**no** reversal. (2) **"Does not reverse" was conflated with "is monotone"**: the one-shell Laplacian never reverses
and still peaks at π/2. (3) **The window count was not contiguous**, so the local difference "passed" at **2 interior
channels** (and the spectral derivative at 3) where `sin δ/δ²` crosses unity — **a coincidence of the ratio, not a
correspondence**; the window is now a contiguous run and the excluded passes are reported.

**Status.** COMPLETE. Tests `Y_QM_004_Tests` **8/8**. Core `AT.Core/ResearchXH/SchrodingerCorrespondenceAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_004.md`. Registry: added as **Partial**
(`schrodinger-correspondence-audit`). **No group-G count moves**, verified by re-running the G_027, G_033 and G_035
scanners after the new core was added.

## ResearchY-QM_003 - Dispersion Closure Audit (COMPLETE, BOUNDARY - the fold is the price of locality)

**Question.** Can the lattice dispersion sin(δ) be replaced or derived into δ without introducing a **new primitive**?
Compare the ring, D96³, the continuum limit and modified generators; measure arg(m), the group velocity and the
zone-edge behaviour.

**Answer.** **BOUNDARY** - the replacement **exists** and is **exact**, and it costs the one thing the difference
primitive supplies. **LOCATED: the fold is the price of LOCALITY, not of discreteness**, and it is forced by the
conjunction of **antisymmetry + locality + periodicity**.

**The theorem, checked rather than cited.** A real **antisymmetric** operator on a periodic ring has an **odd** symbol,
and oddness gives `f(π) = f(−π) = −f(π)`, so **`f(π) = 0` identically** — **the zone edge is stationary for every
antisymmetric lattice generator**.

| candidate | band | f(π) | linear window | fold channel | **reversed occupied** |
|---|---|---|---|---|---|
| nearest-neighbour (the AT difference) | 1 | **1.22E-016** | **1** | **24.00** | **21 of 42** |
| 5-point antisymmetric (4th order) | 2 | **2.04E-016** | **6** | 27.46 | 18 of 42 |
| 7-point antisymmetric (6th order) | 3 | **2.69E-016** | **11** | 29.58 | 16 of 42 |
| **spectral (exact) derivative**, same 96 sites | 48 | **3.14E+000** | **48** | **none** | **0 of 42** |
| D96³, per axis | 1 | **1.22E-016** | 1 | 24.00 | 21 of 42 |

**A wider stencil buys linearity (1 → 6 → 11 channels) and only PUSHES the fold (24.00 → 27.46 → 29.58)**, with the
zone edge still exactly stationary at every order — at the cost of **one more neighbour shell per order**, a new
primitive.

**The replacement that needs no new primitive:** the **spectral derivative on the same 96 cells** — symbol **exactly δ**,
no fold, unit group velocity everywhere — available because the **Fourier basis is already AT's amplitude/phase
decomposition** (G_052). It costs **locality: 96 non-zeros per row against 2**. **The fold and the locality are in exact
opposition**, verified candidate by candidate.

**Neither of the other two routes helps.**
- **D96³** keeps the same per-axis symbol while the folded fraction of the zone grows as **`1 − (1/2)^d`**: **0.5000 in
  1D against 0.8750 in 3D**. The cube does not fix the fold; it **multiplies** it.
- **The continuum limit** leaves the fold at a **fixed zone fraction**: at a **fixed physical wavenumber** the mode
  number rises with the cell count, so δ is constant and the deviation **does not move at all** (**9.968E-002** at 96,
  192, 384 and 768 cells). It falls as **1/N^1.97** only at a fixed **mode index** — a **different state**, not the same
  one measured better. Zone-edge deviation **1.000E+000**, fold at channel **N/4** at every resolution.

**Three defects in the audit's own first version, recorded.** (1) The fold test bisected to machine zero and then
asked whether the final velocity was negative — which **fails because `cos(π/2 + 1e-60)` evaluates to −0.0 and
`−0.0 < 0.0` is false in IEEE**, so every local candidate was reported as having **no** fold and the locality claim
came out **False**. (2) The fold position needed its own tolerance (`23.99999999` not `24.0`) because the symmetric
finite difference of `sin` vanishes **exactly** at π/2. (3) The refinement table's first version cancelled the cell
count and reported order **0** — that measurement is **kept**, because it is the correct and important one.

**Status.** COMPLETE. Tests `Y_QM_003_Tests` **8/8**. Core `AT.Core/ResearchXH/DispersionClosureAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_003.md`. Registry: added as **Boundary** (`dispersion-closure-audit`).
**No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after the new core was added.

## ResearchY-QM_002 - Unitary Correspondence Audit (COMPLETE, PARTIAL - unitary but not Schrodinger)

**Question.** Can any AT flow reproduce unitary Schrodinger evolution on the occupied mode sector? Compare the unitary
Cayley flow, the centred difference flow and the dissipative flow, measuring norm conservation, phase evolution and
mode occupation.

**Answer.** **PARTIAL - and the reason is a TRADE-OFF rather than a defect.** Schrodinger evolution needs **two** things:
a norm conserved at every step, and a phase **linear** in the mode momentum. **Of the four flows carried, 1 is unitary
and 1 has the clean dispersion - and they are different flows, so no flow has both.**

| measure | verdict | basis |
|---|---|---|
| **norm conservation** | **ANALOGOUS** | Cayley `|m| = 1` on all 48 channels at every ε, worst deviation **2.220E-016** |
| **mode occupation** | **ANALOGOUS** | circulant, hence diagonal; drift **< 1E-12** under the unitary flow - **equivalent to unitarity** |
| **phase evolution** | **REFUTED** | the symbol is **sin δ**, not δ: the zone edge is stationary and **21 of 42** occupied modes are past the fold |

**The other flows:** centred amplifies (**4.988E-003**) and drifts (**4.98E-007**/step); the forward difference
dissipates (**2.000E-001**); the **exact flow** dissipates (**1.813E-001**) but has `arg m = ε sin δ` **exactly** - it is
the flow with the clean dispersion.

**The three independent deviations of the unitary flow** (channel 24, ε = 1E-3):

| source | magnitude | ε-order | removable by smaller steps |
|---|---|---|---|
| **Cayley generator convention** | **1.999999** (→ 2) | none | **NO** |
| **time discretisation** | **3.333331E-007** | **2** (coefficient 1/3) | yes |
| **lattice symbol sin δ vs δ** | **0.363380** = 1 − 2/π | none | **NO** |

**The factor two is a finding about the flow's own definition:** `(1 + iεs)/(1 − iεs) = exp(2i arctan(εs))`, so the
Cayley update **unitarises twice the skew generator** the other flows advance — its phase advance is `2ε sin δ` against
their `ε sin δ`. Since **G_066 and G_067 compared flows at equal ε**, that factor is part of what those comparisons
measured; it is now on the record as a **convention with a measurable consequence**.

**The step ladder separates the removable from the irreducible:** the discretisation gap falls by **100 per decade**
(order **ε²**) while the lattice gap sits at **1 − 2/π = 0.363380** and **does not move**.

**The fold answers the question as asked:** `sin δ` peaks at **channel 24** and returns to **2E-019** at the **zone
edge**, so the highest mode never advances; **23** adjacent pairs advance in the continuum's order against **24 in
reverse**; and **21 of the 42 occupied modes lie at or above the fold** — **half the populated sector**, untouched by
any choice of step size.

**The mode count hides the rest:** the mode occupations are constants while the **amplitude/phase split is not
conserved even by the unitary flow** (phase **9.246E-015 → 0.6392**, amplitude **1.005011 → 0.775567** over 2000
steps). A **real** Hamiltonian could not do that, so the generator is Hermitian and **not real** in that basis.

**Two of the audit's own first assertions were wrong and are recorded:** the dispersion table's first version compared
the Cayley advance against the symbol **without dividing out the flow's own generator convention**, reporting a 100 %
deviation at every channel and hiding the ε² behaviour underneath; and the discretisation order's first version had its
log-ratio **inverted** and returned **−2.0**.

**Status.** COMPLETE. Tests `Y_QM_002_Tests` **8/8**. Core `AT.Core/ResearchXH/UnitaryCorrespondenceAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_002.md`. Registry: added as **Partial** (`unitary-correspondence-audit`).
**No group-G count moves**, verified by re-running the G_027, G_033 and G_035 scanners after the new core was added.

## ResearchY-QM_001 - ManyBody Correspondence Audit (COMPLETE, PARTIAL - new group QM; rho is |Psi|^2, not a density state)

**Question.** Does the AT decomposition rho = mean + amplitude + phase have an analogue in many-body quantum states?
Compare amplitudes, phases, the density matrix, the reduced density matrix and occupation numbers, measuring
dimension, kernel and observable rank. Output ANALOGOUS / PARTIAL / REFUTED.

**Answer.** **PARTIAL** - **two** structures analogous, **three** refuted - and the **goal is answered: rho behaves more
like |Psi|^2.** rho is a non-negative real vector of 96 cells with total 96, reconstructed as **|Psi|^2 with error
2.22E-016**, of operator rank **1** - the rank of |Psi><Psi|. The one density-matrix-like feature it has is the pair
**(rank 43, kernel 53)**, and that belongs to its **observable algebra**, not its state.

**The AT side:** **1 (mean) + 42 (amplitude) + 53 (phase) = 96**, observable rank **43 = 1 + 42**, kernel
**53 = 96 - 43 = the phase sector**, state rank **1**, canonical phase content **9.246E-015**.

**The deciding measurements:**

| finding | value |
|---|---|
| modulus reconstruction rho = \|Psi\|^2, Psi = sqrt(rho) | **2.22E-016** |
| AT's occupancy total | **96** = the cell count (a density normalisation, not a trace of 1) |
| cell census | **96 strictly positive, 0 zero** |
| mode-weight census | **2 zeros of 44** - empty MODES with no empty CELLS |
| dimension: AT vs one particle on 96 modes | **96 / 96** parameters against **96 / 9 216** for the density matrix (**96x**) |
| the rank AT's kernel implies | **43**, i.e. **43x** the state's own rank |
| 1-RDM trace vs the particle number | **equal to 12 digits** (2, 3 and 4 bosons; 3 and 4 fermions) |
| fermionic occupations | inside **[0, 1]** (the Pauli bound) |
| AT's spectral levels | **45 distinct**, multiplicities summing to **96**, maximum **above 1** |
| the reduced density matrix | product pair rank **1**, entropy **0**; straddling pair rank **2**, entropy **log 2 = 0.693147** |

**Two structures are analogous** - the **amplitudes** (42-dim sector) and the **phases** (53-dim sector) - and the
correspondence is **structural rather than numerical**: a wavefunction carries two real degrees of freedom *per
configuration* (D magnitudes and D polar angles of the same vectors), while AT splits one real 96-dimensional space
into orthogonal **subspaces**. `1 + 42 + 53 = 96` is a **dimension** identity; `D amplitudes + D phases` is a
**component** identity.

**Three are refuted, by measurement rather than argument:** the **density matrix** (the total is the cell count, and
AT carries 1/D of the parameters), the **reduced density matrix** (the 96 cells carry **no tensor factor**, so no
partial trace exists on D96 - the nearest AT object is D96³, whole 96³ = 884 736 and one-factor reduced 96² = 9 216),
and the **occupation numbers** (integer multiplicities summing to the state dimension, not reals summing to a particle
number).

**A probe defect is recorded.** The first version built its superposition from the Fock enumeration's first two
configurations, which left the kept block **empty in every system** - so the reduced density matrix was rank 1 with
zero entropy every time and the entanglement question was never being asked. Same class as G_063: a helper whose
**domain** silently excluded the case under test. Both regimes are now asserted on explicitly chosen pairs.

**New group.** **QM** is added to the group registry. **No group-G count moves** - the group-G consistency counts and
the `TemporalIndependenceAudit` registry are unchanged, verified by re-running the G_027, G_033 and G_035 scanners
after the new core was added (16/16 passing).

**Status.** COMPLETE. Tests `Y_QM_001_Tests` **8/8**. Core `AT.Core/ResearchXH/ManyBodyCorrespondenceAudit.cs`; doc
`Docs/ResearchY/QM_ManyBody/ResearchY-QM_001.md`. Registry: added as **Partial** (`many-body-correspondence-audit`).

