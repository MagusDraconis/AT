# ResearchY-G_038 — Measure-Decomposition Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_038 (permanent)
**Title:** Measure-Decomposition Audit — can the clock bend light on its own?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_038.md`
**Depends on:** G_009/G_028 (the clock law and its measurement), G_029/G_030 (the spatial sector closes on a postulate; no-go), G_031/G_032 (conformal flatness is the counting measure; imposing it is refuted), G_037 (the refractive index IS γ)
**Raised by:** the observation that **curvature is a change in *measure*** — "space is distance bending a little" — and that **time is a measure too**, so time "should be able to do the same".
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_038_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/MeasureDecompositionAudit.cs`

## The answer: the intuition is CORRECT — and the redshift is what forbids it

> **Time *does* bend light. It supplies exactly 1/(1+γ) of the deflection — 50 % in GR. But it cannot supply the
> other half, because the clock function is already measured.**

And the audit produces a **derived by-product** that refines G_029/G_030: the **first-order spatial coefficient is
not postulated at all**.

## 1. Light responds only to the difference

With `n = e^(B−A)`:

```
n − 1  =  B − A  =  ( +B  from the distance measure )  +  ( −A  from the clock measure )
```

So the measure intuition is right: the clock contributes. Its share is exactly **1/(1+γ)**:

| γ | clock share | meaning |
|---|---|---|
| 0 | **100 %** | time-only: the clock supplies everything it can |
| 1 | **50 %** | GR: the conventional split |
| −1 | **net zero** | the halves are equal and opposite — **AT's case** |

## 2. But a conformal change cannot bend light at all

A conformal metric has `B = A` for **any** conformal factor, so `n = e^(B−A) = 1` **exactly** — verified across
`x = 1e−12 … 0.3`, where `n − 1` is identically zero:

| x | A = B | n − 1 |
|---|---|---|
| 1e−12 | −1e−12 | 0 |
| 1e−6 | −1e−6 | 0 |
| 1e−3 | −1e−3 | 0 |
| 0.1 | −0.1 | 0 |
| 0.3 | −0.3 | 0 |

This is a **theorem**, not a numerical accident: **a conformal factor maps null geodesics to null geodesics.** So
"time and space bending equally" is *invisible to light* — and that is precisely AT's position (`A = B = σ`).
**Making the clock bend harder makes space bend equally harder, and the two cancel.**

## 3. The compensation route works — and is excluded by the redshift

Keep space flat (`B = 0`) and double the clock (`A = −2x`):

| route | A | z = −A | a | bending | redshift |
|---|---|---|---|---|---|
| A = −x, B = −x (AT conformal) | −2.12e−6 | 2.12e−6 | 0 | 0 % | 1× |
| A = −x, B = 0 (time only) | −2.12e−6 | 2.12e−6 | 1 | 50 % | 1× |
| **A = −2x, B = 0 (clock-compensated)** | **−4.24e−6** | **4.24e−6** | **2** | **100 %** | **2×** |
| A = −x, B = +x (required) | −2.12e−6 | 2.12e−6 | 2 | 100 % | 1× |

Doubling the clock **does** deliver the full deflection — at **twice the redshift**. But the solar-limb redshift
is measured to be `GM/(Rc²) = 2.12e−6`, pinned to about **1 %** by Pound–Rebka (first order), GPS and solar line
measurements, and AT already reproduces it with `A = −x` (which is why the G_009 clock law passes).

**The redshift is what forbids compensating via time.** A is not a free knob — it is a measured quantity.

## 4. Therefore B = +x is forced — the refinement of G_029/G_030

With A measured as −x, the deflection `B − A = 2x` leaves exactly one choice: **B = +x**. This is the project's
established **two-level rule** (already used for the 3-family window in D_028/D_040 — a derived *value* with a
boundary *window*):

| level | status | why |
|---|---|---|
| **B's first-order coefficient (B = +x)** | **DERIVED** | forced jointly by the measured redshift (fixing A = −x) and the measured deflection (fixing B − A = 2x) |
| **B's O(x²) completion** | **BOUNDARY** | G_030's no-go leaves this open; G_029's survivor and GR both give +x at first order and differ afterwards |

The remaining freedom, measured:

| comparison | solar compactness | neutron star (x = 0.247002) |
|---|---|---|
| survivor vs GR in `g_rr` | **−2.697×10⁻¹¹** relative | **−29.7 %** |

So the spatial postulate is **far less arbitrary than G_029 alone suggested**: its leading behaviour is fixed by
two measurements, and only its higher-order completion is a free choice.

## 5. Why the medium escape is unavailable in AT

G_037 closed the "make the index a medium" branch with GW170817, which bounds the propagation of the **tensor
sector** against light. AT has **no graviton** — that sector is the massless **spin-2 ψ field** of
`MinimalPsiEquation` (`□ψ_μν = 0`), a field rather than a particle, and GW170817 bounds *its* propagation speed
against light's.

But in AT that branch is not merely excluded — it is **unavailable**. The natural carrier of any index is **ρ**, and
ρ is what sources the metric *including* the ψ sector. An index built from ρ therefore acts on light and on the
metric sector **identically**: it **is** a metric, and `n − 1 = −(1+γ)Φ/c²` applies directly (G_037's identity).
There is no AT-specific medium for light to travel in.

## Output

**REFUTED** — for the clock-compensation route, with a **DERIVED** by-product. The measure intuition does not
remove the need for a spatial sector; it explains **in one line** why AT's spatial sector is wrong by a **sign** and
by a **factor of two** at first order (`B = −x` where `+x` is required), and why the clock cannot repair it.

## Classification and caveats

**Registry:** added to the G_035 classification registry as `Spatial` → **BOUNDARY**, triaged `ScanDetectsIt: false`
(the suite's metric vocabulary is a coefficient plus the negative conformal test, not `B`/`g_rr`/`GammaOf`
symbols). Counts become **25 / 11 / 3 of 39**; boundary index still **23**; no prior classification changed.
G_033's D96-free era count moved 13 → 16 across G_036–G_038.

**Caveats.** (1) First order in `x = GM/(Rc²)` throughout (the PPN regime); the `O(x²)` statements are as measured
in G_029/G_035. (2) The redshift constraint is quoted at the ~1 % level (Pound–Rebka, GPS, solar lines), which is
what excludes the factor-2 compensation; a 100× tighter bound would exclude only proportionally tighter
compensations. (3) Deterministic: exact algebra on closed forms.

**No reclassification.** G_009, G_028, G_029, G_030, G_031, G_032 and G_037 are unchanged inputs; the D_040
registry is untouched; no canonical claim, value or equation changes; no new primitive.
