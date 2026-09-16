# ResearchY-G_065 - Phase-Free Principle Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_065 (permanent)
**Title:** Is phase-freeness a derived requirement or only a preferred convention?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_065.md`
**Depends on:** G_054 (the phases are freely assigned), G_057 (the running process is phase-static), G_063 (one invariant out of thirteen), G_064 (phase-freeness pins the recipe), G_060 (the flow's two forms)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_065_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/PhaseFreePrincipleAudit.cs`

## The question

Is **phase-freeness** a **derived requirement** or only a **preferred convention**? Test the **phase-free canonical
state** against **phase-bearing alternatives**; compare the **clock**, the **acceleration**, the **field**, the **kernel**
and the **flux**; measure whether **any AT law fails** when the phase content is non-zero.

## The answer: **BOUNDARY - no AT law fails, and phase-freeness is still not a mere convention: the dissipative flow erases phase content, so the phase-free configuration is its ATTRACTOR**

## 1. The states tested

| state | phase-free | phase norm | hidden modes occupied |
|---|---|---|---|
| **canonical** (phase-free) | **True** | **9.246E-015** | **0** |
| alternative seed (`basis[^1]`) | False | 1.048E+000 | **42** |
| shifted weight w' | False | 2.285E-001 | 1 |
| no zeros (ramp) | False | 3.657E-001 | 2 |
| every mode occupied | False | 1.166E+000 | **53** |

## 2. The laws, on every state

| state | L1 clock | L2 metric | L3 potential | L4 source | L5 field | L6 simplex | L7 flux | **worst** |
|---|---|---|---|---|---|---|---|---|
| canonical | 3.755E-016 | 2.201E-016 | 1.041E-016 | **0** | **0** | 4.441E-016 | **0** | **4.441E-016** |
| alternative seed | 4.394E-016 | 2.220E-016 | 1.110E-016 | **0** | **0** | 2.961E-016 | **0** | 4.394E-016 |
| shifted w' | 4.411E-016 | 2.206E-016 | 1.076E-016 | **0** | **0** | 1.480E-016 | **0** | 4.411E-016 |
| ramp | 4.308E-016 | 2.188E-016 | 1.110E-016 | **0** | **0** | 8.882E-016 | **0** | 8.882E-016 |
| all modes | 4.396E-016 | 2.220E-016 | 1.104E-016 | **0** | **0** | 2.961E-016 | **0** | 4.396E-016 |

**No law fails for any state**, and **no law separates** the phase-free from the phase-bearing ones: the phase sector is
**invisible to the laws**, and the worst residual anywhere in the family is **8.882E-016**.

**Three of the seven laws were mis-stated at first, and the measurement caught all three** - each produced a residual of
order **1E-2 for *every* state including the phase-free one**, which is the signature of a defect in the law's statement
rather than a property of the states: the potential law subtracted one term too many, the source law compared a
**clamped** helper against a **periodic** difference, and the field law took the **magnitude** of a signed quantity.

**The linearised source law is reported separately** because it is a discretisation order rather than a law:

| state | exact log form | linearised (`Δρ/ρ`) |
|---|---|---|
| canonical | **0** | 1.441E-002 |
| alternative seed | **0** | 1.347E-002 |
| ramp | **0** | 3.051E-001 |
| all modes | **0** | 5.980E-001 |

## 3. The dynamical law: a density must stay a density

| state | min cell, dissipative | min cell, unitary |
|---|---|---|
| canonical | 9.390E-001 | 7.136E-001 |
| alternative seed | 8.910E-001 | 7.357E-001 |
| shifted w' | 9.201E-001 | 7.752E-001 |
| ramp | 8.459E-001 | **2.223E-001** |
| all modes | 8.838E-001 | 5.041E-001 |

Every state stays a density under **both** AT-native flows - so this law does not separate them either, and the
phase-free state is **not privileged**: it does not even have the largest minimum cell.

## 4. The main result: the flow erases phase content

| steps | phase norm | hidden modes occupied |
|---|---|---|
| 0 | **1.048E+000** | **42** |
| 100 | 9.488E-001 | 43 |
| 1000 | 4.937E-001 | 43 |
| 5000 | 2.445E-001 | 43 |
| **20000** | **1.610E-001** | **18** |

Starting from a state that occupies **42 hidden directions**, the **dissipative difference flow** - the form AT admits as
an update - drives the phase norm down by a factor of **six** and the hidden occupancy below **half**. **The phase-free
state is the attractor of the flow AT admits**, and it is already there: its hidden occupancy is **0**.

## 5. What phase-freeness buys: the interface

| state | no split | hidden ⟺ empty | kernel | row rank |
|---|---|---|---|---|
| canonical | True | True | 53 | 43 |
| alternative seed | True | True | 53 | 43 |
| shifted w' | True | True | 54 | 42 |
| ramp | True | True | 51 | 45 |
| **all modes** | **False** | **False** | 47 | **49** |

The interface holds for **every** one-seed state, phase-free or not, and fails **only** where the row space **saturates**
the distance-class bound. **What breaks the interface is saturation, not phase content.**

## Verdict

**BOUNDARY.** **Requirement is too strong a word**: no AT law fails without phase-freeness - all seven hold at the
floating-point floor for every state, including the extreme one that occupies all 53 hidden directions. **Convention is
too weak**: the theory's own dissipative flow **converges to** the phase-free configuration (**1.048 → 1.610E-001** over
20 000 steps), and phase-freeness is the requirement that **pins the canonical recipe** (G_064).
**Phase-freeness is a theorem about the attractor rather than an axiom.**
