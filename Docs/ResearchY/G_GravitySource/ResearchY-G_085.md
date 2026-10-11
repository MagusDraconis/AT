# ResearchY-G_085 — Map Redundancy Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_085_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/MapRedundancyAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_085`) · **Verdict:** **REPARAMETERIZATION GAUGE** (registry: Derived)

---

## Question

G_084 reduced the gravity core to the count density `ρ`, the potential map `A = f(ρ)` and the clock map
`clock = W(A)`, and measured that the core reproduces every surviving observable. The **maps themselves** were left
unexamined.

> **Which parts of `f` and `W` are observable, and which are pure reparameterization?**

**Prefer reduction.** Any redundancy the audit finds reduces the core further.

---

## Answer

**THE MAPS ARE OBSERVABLE ONLY THROUGH THE COMPOSITION `W ∘ f` AND THE SOURCE `f'` — AND OF THE THREE PARAMETERS THE
PAIR CARRIES, TWO ARE CONVENTIONS AND ONE IS UNFIXED.**

| Task | Result |
|---|---|
| 1 Transformations that leave all observables invariant | **one** — the compensated shift `f → f + b, W → W(· − b)` |
| 2 Is `A = f(ρ)` unique? | **Up to its zero point** (the count's unit); its **derivative is observable** |
| 3 Is `clock = W(A)` unique? | **Up to its unit** (fixed by the pinned normalization) **and its shape** (unfixed) |
| 4 Gauge freedom in the maps | **1 gauge + 2 coordinates**; the scale and any non-affine distortion are **excluded** |
| 5 Smallest observable core | **2 invariant objects** — the rate field `W(f(ρ))` and the source field `∇f` |
| 6 Classification | **physical 2 · effective 1 · coordinate 2 · gauge 1** |

**`MapRedundancyRatio = 3.000`** (three parameters carried, one required), **two conventions removed**.

---

## 0. The instrument

Three decisions decide whether the audit can find anything:

1. **The readouts are computed *through* the maps**, so a change of the maps can move them. A readout computed from
   `ρ` directly (or from a fixed law) is blind to the very thing being tested.
2. **A readout is a PREDICTION when it is a comparison the theory makes** (a ratio or a difference) and a **LEVEL**
   when it is an absolute magnitude. That distinction is what separates a unit change from a physical one.
3. **Each variation is applied exactly** — the non-affine distortion is composed with its **exact inverse** — because
   a first-order expansion would not distinguish a free parameter from a truncated one.

**The site choices matter, and the first version got them wrong twice** (see the defects): at the profile's peak the
two neighbours carry the same potential, so a link gradient read there is identically zero; at the steepest slope the
potential is exactly zero, so the four level readouts were blind to the clock law's shape. The readouts are taken at
site **12**, which carries **both** a non-zero potential and a non-zero gradient.

---

## 1. The transformations, and what each one moves

| variation | predictions | levels | invariant pair | pinned data | sector values | verdict |
|---|---|---|---|---|---|---|
| the compensated shift of the potential | **0** | 1 | **identical** | fail | 0.000E+000 | **GAUGE** |
| the unit of the count | **0** | 4 | changed | hold | 0.000E+000 | **COORDINATE** |
| the unit of time | 1 | 3 | changed | **fail** | 0.000E+000 | OBSERVABLE (excluded) |
| the potential's scale | **2** | 1 | changed | fail | **5.000E-001** | OBSERVABLE (excluded) |
| a non-affine distortion of the potential | **2** | 1 | changed | hold | **5.000E-002** | OBSERVABLE (excluded) |
| the clock law's free room | **2** | 3 | changed | **hold** | 0.000E+000 | **EFFECTIVE (undetermined)** |

The readouts that move, by name:

- **the compensated shift** — levels: the potential at a site. **Nothing else.**
- **the unit of the count** — levels: `g00`, the rate, the potential, the site redshift. No prediction.
- **the unit of time** — predictions: the AT/GR ratio at the target. Levels: `g00`, the rate, the site redshift.
- **the potential's scale** and **the non-affine distortion** — predictions: **both gradient readouts** (and only those).
- **the free room** — predictions: the AT/GR ratio at the target **and the pair redshift**. Levels: `g00`, the rate,
  the site redshift.

**The verdict rule, stated once and computed:** invariants preserved → **GAUGE**; no prediction moves → **COORDINATE**;
predictions move **and** the pinned data hold **and** no quoted sector value moves → **EFFECTIVE**; otherwise
**OBSERVABLE (excluded)**.

---

## 2. The invariant pair

The pair **(the rate field `W(f(ρ))`, the source field `∇f`)** is the descriptions' content: two descriptions that
agree on it are the same description whatever their maps are. Measured:

- the **compensated shift** leaves both fields **bit-identical** while the maps differ — so the split of the
  composition into a potential map and a clock map **is not observable**, and the pair has a **one-parameter gauge**;
- the **potential's scale** preserves the rate field exactly and multiplies the source by exactly **1.5**;
- the **non-affine distortion** preserves the rate field exactly and multiplies the source by the exact **field**
  `1 + e(A_i + A_j)` — verified cell by cell over **96** cells against the weighted neighbour average, so it is a
  **field** and not the constant a first-order reading would give.

---

## 3. Is `f` unique? Is `W` unique? (tasks 2 and 3)

**`f` is unique up to its ZERO POINT.** The compensated shift changes `f` by a constant, moves **no prediction**, and
moves exactly **one** readout: the potential's own level — a **convention**. Its **derivative is observable**: scaling
the potential, or distorting it non-affinely, moves both gradient readouts **and** moves a value the sector already
quotes (the uplift cap, by **5.000E-001** and **5.000E-002**), so both are **excluded**, not free.

**`W` is unique up to its UNIT and its SHAPE.** Its unit is **not** a free gauge: the variation makes the pinned data
fail (`F(0) = 1`, `F'(0) = 2`), so the scale is fixed by the sector's own normalization `1 + z = 1/rate`. Its shape is
the opposite case: across the free-room sweep `c ∈ {−0.4, −0.2, 0, 0.2, 0.4}` **every** member satisfies the pinned
data to below `1E-09` while the target comparison moves by more than `1E-03`, and **no value the sector quotes moves**.
So the shape is physical and **no surviving datum selects a member** — which is exactly the prediction G_072 lists as
awaiting the compact-object measurement.

---

## 4. The smallest observable core (task 5)

| object | what it carries |
|---|---|
| the rate field `W(f(ρ))` | every rate and every ratio the sector reads |
| the source field `∇f` | every acceleration the sector reads |

The **reconstruction test** rebuilds the predictions from those two objects alone, for the canonical pair **and** for
its gauge orbit: differences `0.000E+000`. And it names the one readout **outside** the core: the AT/GR comparison at
the target evaluates the clock law at a compactness the **state does not contain**, which is precisely why the clock
law's shape is visible through that readout and nowhere else.

**One redundancy is in the readout set itself:** `g00 = −rate²` **exactly** (residual `0.000E+000`), so the metric
readout and the clock readout are **one statement, not two**. The set carries **4 predictions and 4 levels**.

---

## 5. The classification (task 6)

| part | role | the measurement |
|---|---|---|
| the composition `W ∘ f` (the rate field) | **PHYSICAL** | flattening the clock removes predictions; the rate field moves with the law's shape |
| the source `f'` | **PHYSICAL** | flattening the potential removes predictions; the source is what **excludes** the potential's scale and every non-affine distortion |
| the zero point of `f` | **GAUGE** | the compensated shift leaves the invariant pair bit-identical and moves no prediction |
| the unit of the count | **COORDINATE** | no prediction moves; the gradients are blind to it (differences of logarithms) |
| the unit of time | **COORDINATE** | fixed by the sector's own normalization rather than measured — a **definitional** unit |
| the shape of `W` (the free room) | **EFFECTIVE** | moves the target prediction while every member satisfies the pinned data: physical and **undetermined** |

**All four roles are exercised.** The maps are not wrong; they are **over-parametrized by two conventions and
under-determined in one parameter**.

---

## Defects found while building this audit

1. **The target was treated as a bare potential value.** The first version evaluated the clock map at the canonical
   compactness while the state's potentials had been shifted, so the compensated shift moved the AT/GR prediction —
   a readout that is *physically invariant*. The target is a **state**: its compactness is now carried **through** the
   description's own map. Without the fix the audit would have reported the gauge as a physical freedom.
2. **A readout sited at the profile's peak is blind.** At site 0 the two neighbours carry the same potential, so the
   link-gradient readout is **identically zero** and cannot see any variation. Recorded because it is the same class
   as G_084's placeholder-distance defect: a readout can be vacuous by **where it is evaluated**.
3. **A readout sited at the steepest slope loses the levels.** At site 24 the potential is exactly `0`, so the free
   room moved no level readout — the audit would have under-reported the shape's visibility. The site now carries both
   a non-zero potential and a non-zero gradient.
4. **The invariant-preservation test could be fooled by a state variation.** The first version compared the invariant
   pair computed from one `(maps, state)` pair with itself, so the **count unit** — a state change — registered as
   *preserved* and was misclassified as a gauge. The comparison now takes **two** `(maps, state)` pairs.
5. **The non-affine law was asserted, not measured.** The first test asserted the ratio `1 + 2eA_i`, which is the
   *unweighted* reading; the exact law is the **weighted** neighbour average of `1 + e(A_i + A_j)` (weights
   `A_j − A_i`). Corrected to the exact expression, checked on 96 cells — the audit was right and the *test* was wrong.
6. **A verdict assertion that carried a literal count** (`"1 are PHYSICAL"`) instead of the computed one, caught by
   the deterministic-verdict test.

---

## What this does and does not establish

**Establishes.** That the map pair has exactly **one gauge direction**, that its **two units are coordinates**, that
the potential's **scale** and any **non-affine distortion** are **excluded by the acceleration**, and that the clock
law's **shape parameter** is **effective** — physical, moved by the target prediction, fixed by no surviving datum.
`MapRedundancyRatio = 3.000`: **the reduction is in the freedom, not in the number of functions**.

**Does not establish.** That the potential map or the clock law are dispensable: the composition and the source **are
the maps re-cut**, and dropping either loses predictions. Nor that the free room is unphysical — it awaits the
compact-object measurement.

**The one convention in the readout set is disclosed:** the AT/GR readout is referenced to the GR prediction, so the
clock's **own unit** is visible in it. That is why the time unit reads as a definitional coordinate rather than a free
gauge, and the audit states the reference rather than reporting the freedom as larger than it is.

**Out of scope (substrate).** No spectral quantity is quoted — no eigenspace count, no multiplicity — so the
D96/D96³ Spectral Substrate Rule is **not engaged**, and the audit says so rather than omitting the lattice.

---

## Where it stands

The chain G_079 → G_085 now reads:

- G_080: connectivity is **ASSUMED**; G_081: **6 of 29** claims depend on it; G_082: the component count is
  **GAUGE-LIKE**; G_083: the continuity law is a **COORDINATE CHOICE**;
- G_084: strip it all and nothing observable is lost — the core is **one state variable and three equations**;
- G_085: **the core's own two maps are observable only through the composition and the source** — one gauge, two units,
  and one physical parameter no surviving datum fixes.

`MapRedundancyAudit.Verdict()` starts with `REPARAMETERIZATION GAUGE`, and the registry classifies the audit as
**Derived** (like G_084, and unlike the Boundary entries G_081–G_083).

What G_085 hands on is sharper than G_084's: the sector's content is **two fields of `ρ`** — the rate and the source —
and everything else in the core is either a **unit** or a **gauge**. Any future statement that reads more than those
two fields, or that depends on where the potential's zero is put, is a statement about the description rather than
about the observables.
