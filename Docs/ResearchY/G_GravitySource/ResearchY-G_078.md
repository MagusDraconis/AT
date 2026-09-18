# ResearchY-G_078 — Source Manipulation Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_078_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/SourceManipulationAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_078`)

---

## Question

G_077 established that the clock and the acceleration share **one** source: the local occupancy `rho`, through the
potential `A = (1/d) ln rho` with `d = 3`. This audit asks whether that source can be **changed locally**.

Allowed: actualization, density transport, count conservation, causal structure.
Forbidden and not used: new fields, imported matter sectors, imported GR equations.

1. Which processes can alter local `rho`?
2. Are they redistribution, amplification, or suppression?
3. Under count conservation, what is the maximum achievable **local enhancement**?
4. Can a bounded finite region have `Δrho ≠ 0` with no compensating deficit elsewhere?
5. Does every **clock** manipulation necessarily create a gravitational signature?
6. Does every **gravitational** manipulation necessarily create a clock signature?

Preferred outcome: refutation. *Only redistribution exists* would kill practical gravity engineering; *a source term
increases local `rho`* would open a genuine manipulation channel.

## Answer

**REFUTED — REDISTRIBUTION IS THE ONLY ELEMENTARY CHANNEL, AND THE SYMMETRY OF ITS BOUNDS TURNS ON ONE ASSUMPTION
THIS AUDIT REFUSES TO MAKE SILENTLY.**

- **No source term is supplied.** Every natural operator of the update class is a **divergence** and leaks
  `0.000E+000` of the count on a closed ring, while the deliberately non-divergent **control** leaks `8.166E+000`.
  The detector can see a source; there is none in the class. A source term would therefore be an **import**, which
  the question forbids — so it is **absent, not excluded**.
- **The uplift cap is robust.** Concentrating everything in one cell gives `1.5214` in the potential, a rate ratio of
  `4.5789` and a metric ratio of `20.9659`; keeping a one-unit floor in every cell and concentrating the surplus gives
  `1.5249`, `4.5947`, `21.1113`. The two readings differ by `2.270E-003` relative — **the cap is a property of
  positivity, not of the reading**. Positivity is the whole reason: no cell can hold more than the total.
- **The contrast cap is conditional, and this refutes the obvious formulation of this audit's own headline.** Under a
  unit floor in every cell the maximum **contrast** between a well and a hill of the same count is `4.5947` — *the same
  number as the uplift cap*, so **the uplift/depression asymmetry vanishes**. The asymmetry exists **only if an empty
  cell is admitted**, where the contrast is unbounded (`1.260E+002` at well occupancy `1.000E-006` with the total fixed)
  and the reason is the sector's own `A = (1/d) ln rho` diverging — **not** anything count conservation does.
- **A bounded region cannot hold a change alone.** The change inside a region equals minus its net boundary flux, to
  the arithmetic's resolution (`4.857E-017`), measured as a ledger identity over 8 cells. The compensating deficit must
  exist **somewhere**; its distance is free, because the flux decides it.
- **The two implications are asymmetric, and that is the sharpest pair.** Every **gravitational** manipulation carries
  a clock signature (`0` counterexamples over 9 clock laws × 4 depths), because an acceleration change needs a gradient
  of the occupancy and the clock is a strictly monotone readout of it. Every **clock** manipulation carries a
  gravitational signature too, with **exactly one exception** — the **uniform** direction, which changes every clock
  (`6.623E-003`) and no acceleration (`0.000E+000`) while changing the total count by `1.920E+000`. **Count
  conservation forbids exactly that move**, so the only channel that would have decoupled time from gravity is the
  channel the theory closes.

Effective verdict: **no local net source exists.** What the theory permits is the balanced pair with a fixed total,
and what the pair buys depends on the empty-cell question; what it forbids is the one move that would have separated
time from gravity.

---

## 1. Which processes can alter local `rho`

The update class on one state (count change of `sum L(rho)`; `1.971E-001` is the boundary term the open chain drops):

| operator | closed ring | open chain | dropped boundary | leak = dropped |
|---|---|---|---|---|
| local difference (upwind) | 0.000E+000 | 1.971E-001 | 1.971E-001 | True |
| centred difference | 0.000E+000 | 1.971E-001 | 1.971E-001 | True |
| second difference (Laplacian) | 0.000E+000 | 0.000E+000 | 1.971E-001 | False |
| **CONTROL: growth (not a divergence)** | **8.166E+000** | 8.166E+000 | 1.971E-001 | False |

The two first-order forms conserve the count on a **closed** ring and leak on an **open** chain exactly the boundary
term the chain drops — **question 4's answer arriving already**: a bounded region has nowhere to send its boundary flux.

Channel census:

| channel | status | measurement |
|---|---|---|
| density transport | **AVAILABLE** — the elementary channel | changes a region's count by exactly its net boundary flux, residual `4.857E-017` |
| actualization (the update class) | **COUNT-PRESERVING** — not a source | every divergence leaks `0.000E+000` on a closed ring while the non-divergence control leaks `8.166E+000` |
| a source term | **NOT SUPPLIED** — it would be an import | no operator of the natural class changes the total |
| causal structure | **ORIENTS AND SUPPRESSES** — it does not create | a causal order restricts which links may carry flux in which direction |

## 2. Redistribution, amplification, or suppression

Transport is a **divergence**: it moves count, it does not make it. The pair `rho = (0.5, 1.5, …)` has the *same*
total as the uniform state (`16.000000` vs `16.000000`), a well potential of `-0.231049` and a hill potential of
`0.135155`. **The pair is one statement containing both an amplification and a suppression** — a local rise is the
visible half of a redistribution, and the other half must be somewhere:

| region | change inside | net boundary flux | residual |
|---|---|---|---|
| one cell | -6.000E-002 | -6.000E-002 | 4.857E-017 |
| three cells | -2.000E-002 | -2.000E-002 | 1.735E-017 |
| seven cells | 6.000E-002 | 6.000E-002 | 4.857E-017 |
| the whole ring | 0.000E+000 | -0.000E+000 | 0.000E+000 |

All 8 single cells change, and none changes without a boundary flux.

## 3. The maximum achievable local enhancement

| reading | max occupancy | uplift `ΔA` | rate ratio | metric ratio |
|---|---|---|---|---|
| all of the count in one cell (others empty) | 96.0 | 1.5214 | 4.5789 | 20.9659 |
| one unit in every cell + the surplus in one | 97.0 | 1.5249 | 4.5947 | 21.1113 |

The uplift is **logarithmic** in the available count — `ΔA = (1/3) ln m` — so each `e^3 = 20.09×` of count adds
exactly `1.0`:

| cells | uplift | rate ratio | metric ratio |
|---|---|---|---|
| 4 | 0.4621 | 1.5874 | 2.5198 |
| 16 | 0.9242 | 2.5198 | 6.3496 |
| 48 | 1.2904 | 3.6342 | 13.2077 |
| **96 (canonical)** | **1.5214** | **4.5789** | **20.9659** |
| 9 216 | 3.0429 | 20.9659 | 439.5703 |
| 1 000 000 | 4.6052 | 100.0000 | 10 000.0000 |

Enhancement is **expensive**: to double the potential uplift one needs `20 000×` the count. The uplifts of the two
readings differ by `2.270E-003` relative, so the cap is a property of **positivity**.

| contrast branch | well occupancy | max contrast | status |
|---|---|---|---|
| unit floor in every cell | 1.0 | **4.5947** | **BOUNDED — and equal to the uplift cap, so the asymmetry VANISHES** |
| empty cell admitted (occupancy 0) | 0.0 | **∞** | UNBOUNDED — but this is where `A = (1/d) ln rho` itself diverges, i.e. the sector's breakdown, and G_075 measured that no surviving datum probes it |
| integer counts, surplus over the floor | 1.0 | 4.5947 | a genuine integer count cannot fund more than the total |

The floorless branch, carried **only** to show where the divergence comes from (these are **not** integer counts and
are **not** configurations), total fixed at 2 throughout:

| well occupancy | hill occupancy | total | `ΔA` | rate ratio |
|---|---|---|---|---|
| 1.000E+000 | 1.000000 | 2.000000 | 0.000000 | 1.000E+000 |
| 5.000E-001 | 1.500000 | 2.000000 | 0.366204 | 1.442E+000 |
| 1.000E-001 | 1.900000 | 2.000000 | 0.981480 | 2.668E+000 |
| 1.000E-002 | 1.990000 | 2.000000 | 1.764435 | 5.838E+000 |
| 1.000E-004 | 1.999900 | 2.000000 | 3.301146 | 2.714E+001 |
| 1.000E-006 | 1.999999 | 2.000000 | 4.836219 | 1.260E+002 |

`100×` of well depth raises the contrast by only `4.642×` — **the cube root**, because `A = (1/d) ln rho` with
`d = 3`. A linear response would have been wrong.

**Self-refutation.** Under a floor the contrast potential difference is `1.525E+000` — the **same** as the uplift cap
(rate ratio `4.595E+000` vs `4.595E+000`). So *"redistribution leaves the depression unbounded"* is **not** a result
of count conservation: it is a consequence of admitting an **empty cell**.

## 4. Can a bounded region change alone

Measured over three flux patterns × four regions — the change inside **equals minus** its net boundary flux to
`4.857E-017` in every non-degenerate row, and **0** regions changed with no boundary flux. The whole ring is the
**one** region with no boundary and there the change is zero, because the transport is a divergence. **The change must
be paid for, and *where* it is paid for is free** — the flux decides the distance, which is an engineering choice and
not a theorem. **REFUTED** that a bounded region can change alone.

## 5. Every gravitational manipulation carries a clock signature

| law | configurations | acceleration without clock | implication holds |
|---|---|---|---|
| exp(2x) · 1 + 2x · (1+x)^2 · 1/(1−2x) · Padé [1/1] · Padé [2/2] · exp(2x−2x²) · exp(2x−100x²) · flat-then-rising (m = 3) | 4 each | **0** in every row | **True** in every row |

**9 laws × 4 depths = 36 configurations, 0 counterexamples.** A gravitational manipulation that left every clock
unchanged would need a step in the acceleration with no step in the occupancy — and the acceleration *is* the
occupancy's gradient in this sector. **YES.**

## 6. Does every clock manipulation carry a gravitational signature

| claim | answer | measurement |
|---|---|---|
| every GRAVITATIONAL manipulation carries a clock signature | **YES** | over 9 clock laws × 4 depths, acceleration changed with no clock change in **0** configurations |
| every CLOCK manipulation carries a gravitational signature | **NO — exactly one exception** | a uniform occupancy scaling changes the clock by `6.623E-003` and the acceleration by `0.000E+000`, while changing the total count by `1.920E+000`, which count conservation forbids |
| a bounded region can change its count ALONE | **NO** | the change inside equals minus the net boundary flux to `4.857E-017` |

Uniform escape, canonical lattice:

| cells | clock change | acceleration change | count change |
|---|---|---|---|
| 8 | 6.623E-003 | 0.000E+000 | 1.600E-001 |
| 96 | 6.623E-003 | 0.000E+000 | 1.920E+000 |

So **every clock manipulation that count conservation permits DOES carry a gravitational signature**; the one move
that would not (a uniform scaling) is exactly the move that changes the total, so it is not a manipulation of the
conserved source at all. **The theory closes the one channel that would have separated time from gravity.**

---

## Defects found while building this audit

1. **A ledger of zeros proves nothing.** The region table used fixed starts against a flux pattern that left those
   regions unchanged, so every row read `0.000E+000` and the identity was satisfied vacuously. Fixed by starting the
   regions at cell 0 and **asserting the rows are non-trivial**.
2. **An inverted verdict row.** `MirrorImplication`'s third row encoded `Answer = All(residual > 1e-12)` — i.e. it
   answered **true exactly when the identity failed**, inverting the refutation. `Answer` now means *the claim is
   true*, and the test asserts `false`.
3. **The verdict was culture-dependent.** Its figures were interpolated with the machine's culture (`1,5214`), so a
   test that re-formatted a computed number passed on `en-US` and failed on `de-DE`. This is the "never compare
   numbers as formatted strings" rule biting in a **new** place (locale, not format). Fixed twice over: invariant
   formatting inside the verdict, **and** `VerdictFigures()`, which returns the quantities as **numbers**.
4. **A wrong growth law in the audit's own first draft.** The contrast was asserted to grow `10×` per decade of depth;
   the truth is `10^(1/3) ≈ 2.15×`, because the contrast is a **cube root**. A "×10 per decade" claim would have been
   a wrong published figure. The closed form is now asserted.
5. **The open-chain identity is form-specific.** The "open-chain leak equals the dropped boundary term" identity holds
   for the two first-order divergence forms, **not** for the control or (under the end-duplicated convention) the
   Laplacian. Narrowed, and the convention-dependence is now stated in `WhereItStands()` rather than left implicit.
6. **The headline claim was conditional and the condition was invisible.** *"The depression is unbounded"* is true only
   if an **empty cell** is admissible. With a unit floor both sides are capped by the *same* logarithmic number. The
   audit now reports both branches and **refutes its own unconditional formulation**.
7. **A missing method signature** removed by a mechanical edit inside this session — caught by the compiler, recorded
   for honesty about the process rather than the physics.

## What this does and does not establish

- **Does:** no operator of the surviving update class changes the total, so no **source** exists; the **uplift** is
  capped by the count through positivity; the **contrast** is capped iff a cell may not be empty; a bounded region can
  never change alone; gravity implies clocks unconditionally, and clocks imply gravity except for the one move count
  conservation forbids.
- **Does not:** settle whether an empty cell is a legitimate configuration (the whole conditional structure of §3
  turns on it); exclude a source term — the audit measures that none is *present* in the natural class, and says that
  importing one would be a new primitive; and it does not touch causation — causal structure was tested as a **channel**
  and found to be a **constraint**.

## Where it stands

**No local net source.** A proof that only redistribution exists does **not** by itself kill gravity engineering: it
kills the **monopole**. What it leaves is the **balanced pair** — a total that is fixed and two ends that diverge —
whose reachable contrast is `4.59` under a unit floor and unbounded only where the sector's own logarithm breaks down.
So the practical statement is quantitative and modest: on the canonical lattice the attainable clock offset is a factor
of a few, bought with `20 000×` the count per unit of potential, and the one move that would have decoupled time from
gravity is the one the theory forbids. The open question this audit hands on: **may a cell be empty?** — the conditional
structure of every bound here rests on it, and no surviving datum probes it.
