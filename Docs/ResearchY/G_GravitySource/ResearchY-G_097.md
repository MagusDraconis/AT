# ResearchY-G_097 — Pattern Kinematics Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_097_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/PatternKinematicsAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_097`) · **Verdict:** **MOTION IS EMERGENT; ITS KINEMATICS IS ASSUMED**

---

## Question

G_084 reduced the sector to one state variable and three equations, G_086 collapsed the observable kernel to one
function, and G_096 showed the requirement that would fix the remaining coefficient is assumed.

> **Can observed spatial motion be derived as pattern propagation on the 96-cell ring, or is kinematics independent
> information?**

**Definitions in force:** a **pattern** is any state configuration distinguishable by the surviving observables;
**motion** is the change of a pattern's apparent position; and **distance must be derived, not assumed**.
**Constraints:** the ring is the only substrate; the cubic substrate is **not** used; no new primitive, field, source
term, topology axiom, action, Lagrangian, memory mechanism or extra structure is introduced.
**Prefer refutation.**

**Leaned on, not re-derived:** **NP_092** established that propagation in AT is **not transport**, that everything
*appears* to move, and that the velocity is the **group velocity in units of one link per tick**. This audit therefore
asks what those units **are** in the surviving sector.

---

## Answer

**MOTION IS EMERGENT; ITS KINEMATICS IS ASSUMED.** The failure criterion is **MET**; the success criterion is **NOT MET**.

| Task | Required output | Measured |
|---|---|---|
| 1 | **structure inventory** | **5** candidates → **1** state variable and **2** evolution maps |
| 2 | **distance classification** | **ASSUMED** (the derived candidate is a *state* difference) |
| 3 | **worldline classification** | **BOUNDARY** (the tick is the deepest structural boundary) |
| 4 | **velocity-family dimension** | **1** (the amount) with **2** directions; the bound is derived, the value is not |
| 5 | — | without links **0** operations and transport **stops**; without the labelling the physics survives |
| 6 | **degeneracy count** | **48!** = **61.094** orders of magnitude, states bit-identical |
| 7 | **observable loss count** | **0** with the labelling kept, **4** without it |
| — | **final classification** | **EMERGENT** propagation · **ASSUMED** distance · **BOUNDARY** worldline · **ASSUMED** velocity |

---

## 1. The structure inventory (task 1)

| structure | state? | map? | survives as a carrier | readouts carried |
|---|---|---|---|---|
| the state (the occupation, one scalar per cell) | **yes** | no | **yes** | **8** |
| the update relation (the Laplacian action) | no | **yes** | **yes** | 0 |
| the transport layer (the class's operations) | no | **yes** | **yes** | 0 |
| the flux | no | no | **no** | 0 |
| the phase (the theta sector) | no | no | **no** | 0 |

**One scalar and two maps**, and everything else is derived (the flux, written by an operation), excluded by **arity**
(the phase), or absent. A carrier must be a state variable the evolution acts on or the evolution map itself.

---

## 2. What the law does to a pattern — the measurement motion depends on

The evolution step is `ρ → ρ + ε·Lρ`. **The step size is a CONVENTION the sector does not supply** — it supplies the
Laplacian, not the rate at which it is applied — and its admissible range is **derived** from the Laplacian's own
spectrum through the alternating mode `k = N/2`: **ε ≤ 0.5**.

| tick | peak | bump width | centroid shift | excess total |
|---|---|---|---|---|
| 0 | 1.250000 | 0.000000 | 0.000E+000 | 0.250000 |
| 1 | 1.125000 | 0.707107 | 0.000E+000 | 0.250000 |
| 2 | 1.093750 | 1.000000 | 0.000E+000 | 0.250000 |
| 4 | 1.068359 | 1.414214 | 0.000E+000 | 0.250000 |
| 8 | 1.049095 | 2.000000 | 0.000E+000 | 0.250000 |

- the **peak falls** and the **width grows** — the pattern **spreads**;
- the **excess is conserved exactly** (0.250000 at every tick) — the law neither creates nor destroys it;
- the **width grows as √t** with `width²/(2t)` = the step size to **0.1 per cent** (the deviation being the discrete
  correction), which is the **fingerprint of diffusion**;
- the **centroid moves by exactly 0.000E+000 at every tick** — the law **moves nothing**;
- the mode symbols are **0.000, −0.004, −0.017, −0.038, −0.068** — **non-positive**, so the map is **dissipative and
  not a wave**.

**So motion cannot come from the law.** Apparent motion can only come from the **operations**, which the operator
chooses — and that is a sequence of choices, not a kinematics.

---

## 3. Distance (task 2)

| candidate | kind | derived | needs links | needs labelling | status |
|---|---|---|---|---|---|
| the hop count along the link set | graph distance | yes | **yes** | no | **ASSUMED** |
| the potential difference between two sites | **state difference** | **yes** | no | no | **DERIVED** |
| the difference of cell indices | **coordinate** | yes | no | **yes** | ASSUMED |
| the clock-to-acceleration ratio along a path | path ratio | **no** | no | no | ASSUMED |

**Spatial distance is ASSUMED.** Exactly one candidate is derived, and it is a **difference of states, not of
positions** — it says how *different* two sites are, never how *far apart*: it **is** the acceleration integral, the
quantity the observables actually read (G_077's two-readings-of-one-difference). The graph distance is derived
**given** the link set, and the link set is **assumed** (G_080); the index difference is the **labelling**, which
G_084/G_085 classified as a coordinate; and the physical **length of a hop** is supplied by nothing — the link-length
programme's own record is `LinkLengthDeterminesValues = false`.

---

## 4. The worldline (task 3)

| ingredient | available? | measurement |
|---|---|---|
| **pattern identification** across ticks | **yes** | all **8** readouts are functions of the occupation, so two configurations are identical exactly when their readouts are |
| **a position** for the pattern | **no** | the sector has **no absolute-position observable**: **4** of 8 readouts read the labelling and **4** are frame-invariant |
| **a time parameter** | **no** | the tick is the sector's recorded **deepest structural boundary** (QG_008/009/011) |

**The worldline is BOUNDARY**: the pattern can be tracked, the position cannot be named, and the sequence is indexed by
something the theory does not supply.

---

## 5. Velocity (task 4)

Defined only from surviving quantities — the apparent displacement per tick, in **hops**:

| component | value | derived | measurement |
|---|---|---|---|
| the displacement **bound** | 1.0 | **yes** | the largest cyclic distance between the two sites any witness touches is **1** over all witnesses |
| the **amount** transferred | 1.600 | no | a free parameter: the largest count on the ring |
| the **direction** | 2 | no | two are available and the choice is the operator's |
| the **unit** (one link per tick) | 1.0 | **no** | both halves unsupplied — the link's length and the tick |

**Velocity-family dimension: 1** (the amount, a continuum) with **2** directions. **The bound is derived and the value
is not**, and the **unit** is exactly the one NP_092's group velocity is stated in.

---

## 6. Without spatial assumptions (task 5), the degeneracy (task 6) and the reduction (task 7)

**Removing the assumptions:**

| removed | operations remaining | readouts | transport survives |
|---|---|---|---|
| the link set (adjacency) | **0** | 8 | **no — it stops** |
| the labelling (the cell index) | 95 | 8 | **yes** |
| the order of the operations | 95 | 8 | **yes** |

**Degeneracy:** the ring admits **48 mutually independent transfers** (disjoint adjacent pairs); they **commute**, so
moving them in the reverse order gives a **bit-identical state** — and the observables are functions of the state
alone. **48! = 10^61.094** histories are observationally identical: the missing information is **not recoverable** from
the readouts. The count is reported as a **power** because it exceeds a 64-bit integer.

**Reduction:** **8** readouts, of which **4** involve a **position** and **4** read the labelling. Reconstructing them
from pattern propagation costs **0** with the labelling retained as a coordinate and loses **4** without it — and there
is **no readout of a pattern's absolute position**, so the reconstruction has nothing else to lose. **Motion is the
motion of a LABEL.**

---

## Defects found while building this audit

1. **The propagation step replaced the state by its Laplacian.** `UpdateAt` returns `Lρ`, not `ρ + εLρ`; using it as the
   step made the total collapse to **0** and the amplitude explode (**peak 3217**, spread `NaN`). The step is now
   `ρ + εLρ`, **ε is named as a convention**, and its admissible range is **derived** from the spectrum.
2. **The stability bound sampled one mode short.** `StableStepBound()` took the minimum over modes `k = 0 … 47`, missing
   the alternating mode `k = N/2` whose eigenvalue is the most negative, and reported **0.500536** where the bound is
   exactly **0.5** — a 0.1 per cent error that a test caught. The spectrum is now sampled **through** `k = N/2`.
3. **An adjacency test that was not an adjacency test.** The operations' one-hop character was first measured by the
   **first moment** of the change, `|Σ c·i| ≤ 1`. That is not a support measure: the witness transferring between cells
   **0 and 95** reads as **95** and failed. The support itself is now measured — the largest **cyclic** distance between
   the two sites a witness touches — and it is **1** for every witness.
4. **A profile-wide width that hid the bump.** The pattern's spread was first measured over the whole profile, whose
   second moment is the ring's (**27.78**) and not the bump's. The audit now tracks the **excess** over the flat
   background, which is what makes the diffusion measurable at all.
5. **The degeneracy count overflowed.** `48!` was assembled into a 64-bit integer and reported as **0**. It is now
   reported as a **power of ten** with the overflow stated, which is the only honest form for a count of that size.

---

## What this does and does not establish

**Establishes:** that the ring carries a pattern with **one scalar and two maps** and nothing else; that the **law moves
nothing** (the width grows as **√t**, the centroid shift is exactly **0**, the symbols are non-positive); that
**distance** is assumed while its derived surrogate is a **state** difference; that the **worldline**'s missing
ingredient is the sector's deepest boundary; that the velocity's **bound** is derived and its **value and unit** are not;
that **48!** histories agree observationally; and that the reduction itself costs **no** observable.

**Does not establish** that motion is impossible, or that a larger theory could not supply the hop's length or the tick.
The audit measures that the **surviving** sector does not — the failure criterion the question names as acceptable. Nor
does it contradict **NP_092**: that audit's group velocity is stated in exactly the units this one measures as
unsupplied, so the two are consistent and the difference is the question asked, not the answer given.

**Why this is not a restatement of G_021 or NP_092:** G_021 asked whether the sector has a spatial metric **for light**,
and NP_092 asked what propagates **on the network** and what its velocity analogue is. This audit asks whether
**kinematics** — distance, worldline, velocity — is derivable on the **96-cell ring** from the surviving structure, and
it answers by measuring the **two units** those audits took as given.

---

## Where it stands

**Substrate:** the canonical **96-cell ring**, cells = 96, densities = `MapRedundancyAudit.State`, links =
`LinksOf(96, 1)`. The **cubic substrate is not used** and no substrate token beyond the ring appears in the suite.
**Equality rule:** `1e-12` on a computed value; the diffusion relation is checked to **0.1 per cent** with the deviation
attributed to the discrete correction; the step `ε = 0.25` is a **stated convention** with a derived admissible range.
**No spectral quantity is quoted** — no A₀, no free room, no lock release, no survivor count — so the spectral-substrate
rule is not engaged.

**Guards:** `Y_G_033` — `without` **65 → 66**; `Y_G_035` — **97 → 98**, survives **83 → 84**. Registry entry:
**derived**.

**What would change the verdict:** a surviving structure that fixes the **physical length of a link**; a **derivation of
the tick** from the primitives; or a **positional readout** in the observable set — any of which would move one of the
three objects from ASSUMED or BOUNDARY to DERIVED.
