# Y_QM_010 - Result

**Audit:** ResearchY-QM_010 - Fingerprint Load Audit
**Verdict:** **FINGERPRINT** (the spectrum carries numerical realisation, not physical content)
**Tests:** 6/6 PASSED

## Answer

**3 CORE + 5 FINGERPRINT + 0 ARTEFACT**, and by theorem classification **3 UNCHANGED + 3 NUMERICALLY_CHANGED +
2 STRUCTURALLY_CHANGED + 0 REFUTED**. **The zero REFUTED is the central positive result, not an absence of findings:**
for every claim the fingerprint touches, the claim is still **true** on the replacement.

## The load table

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

## The constants each claim quotes

| claim | constants |
|---|---|
| clock law | `d = 3`, `rate(8) = 2` |
| redshift law | `z_AT = 2.123049E-006`, `z_GR = 2.123054E-006` |
| flux quantisation | `quantum = 0.065449846950` (= 2π/96), `holonomy(1) = 6.283185307180` (= 2π) |
| source law | fixed point `9.7979589711`, phase-null `4` |
| amplitude/phase split | `1 + 42 + 53` against `1 + 46 + 49` |
| kernel theorem | kernel `53` against `49` |
| observability theorem | kernel `53` against `49` |
| phase accessibility | accessible `53` against `49` |

## The CORE block

**The flux quantisation is the sharpest case.** Its quantum **2π/96** and its unit holonomy **2π** are functions of the
ring's **length**, not of its shell set: **the same 96 cells would quantise the flux identically under any admissible
generator.** The fingerprint is not merely unneeded here, it is **unreachable**.

## The refuted prediction - the audit's own

**I drafted the observability theorem into the CORE block**, reasoning that its content quotes no substrate number.
**The measurement refused it:**

| substrate | kernel | minimum clock response | silent directions | first-order directions |
|---|---|---|---|---|
| {1..6} | 53 | **6.531E-003** | **0** | **53** |
| {1} | 49 | **6.557E-003** | **0** | **49** |

The **predicate** is substrate-free and the **kernel it quantifies over** is not, so the claim as stated is
**NUMERICALLY_CHANGED** / FINGERPRINT. The measured label is reported rather than the intended one.

## The ARTEFACT label and the honest negative

**No named claim is an artefact.** The label is mechanical - a predicate **vacuous over the whole 63-subset family**
carrying a number that **moves** - and three **controls** demonstrate it:

| control | vacuous over 63 | {1..6} | {1} | distinct values | load |
|---|---|---|---|---|---|
| the free room is 51 | yes | 51 | 47 | 14 | **ARTEFACT** |
| the trace is 1152 | yes | 1152 | 192 | 6 | **ARTEFACT** |
| the level count is 45 | yes | 45 | 49 | 14 | **ARTEFACT** |

Each is true for every one of the 63 subsets **by definition** while its number is a pure function of the substrate
choice: **a number dressed as content.** The label is not free - a predicate that **can** fail is not an artefact
whatever its number does - and that branch is asserted too.

## Notes

1. **The audit's own draft prediction was refuted** (the observability theorem, above) and is recorded rather than
   adjusted quietly.
2. **The classification and the load are functions of measured booleans and measured constants**, so every branch of
   both rules is exercised in the suite - including `REFUTED` and `ARTEFACT`, which the eight claims never trigger.
3. **A performance defect in QM_008 was found and fixed while building this audit.** `SeenDirections` was rebuilt
   inside `ModeShare` **once per Fourier mode** (96 Gram-Schmidt rebuilds per candidate). It is now memoised per
   substrate, as are the canonical state, the kernel basis and the observability reading. The QM block fell from
   **~30 s to 10 s** for 22 tests (QM_008 + QM_009 + QM_010), with **no result changing**.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
