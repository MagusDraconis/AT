# Y_G_038 Result — Measure-Decomposition Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_038_Tests.cs`
**Status:** 7/7 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_038"`
**Group total:** G_001–G_038 = **297/297 PASSED**

## Verdict

**REFUTED** — for the clock-compensation route, **with a DERIVED by-product**: the first-order spatial coefficient
`B = +x` is forced by two measurements, so only its `O(x²)` completion is a postulate. Computed.

## The decomposition

With `n = e^(B−A)`, light responds **only** to the difference: `n − 1 = (+B distance) + (−A clock)`.

**The clock's share is exactly `1/(1+γ)`:**

| γ | clock share |
|---|---|
| 0 | **100 %** |
| 1 | **50 %** |
| 2 | 33.3 % |
| −1 | **net zero** (halves equal and opposite — AT's case) |

**Time does bend light.** The intuition is correct.

## A conformal change cannot bend light at all

`B = A` for **any** conformal factor ⟹ `n = 1` exactly — asserted across `x = 1e−12, 1e−6, 1e−3, 0.1, 0.3`, all
returning `n − 1 = 0`. Structural reason: **a conformal factor maps null geodesics to null geodesics.**

Routes compared (all asserted):

| route | a | bending |
|---|---|---|
| AT conformal (B = A) | 0 | 0 % |
| time only (B = 0) | 1 | 50 % |
| required (B = +x) | 2 | 100 % |

## The compensation route, and what excludes it

| route | A | z = −A | a | redshift |
|---|---|---|---|---|
| A = −x, B = −x | −2.12e−6 | 2.12e−6 | 0 | 1× |
| A = −x, B = 0 | −2.12e−6 | 2.12e−6 | 1 | 1× |
| **A = −2x, B = 0** | **−4.24e−6** | **4.24e−6** | **2** | **2×** |
| A = −x, B = +x | −2.12e−6 | 2.12e−6 | 2 | 1× |

Doubling the clock gives the full deflection at **twice the redshift** — and every other route sits at 1×. The
suite asserts all four. The solar-limb redshift is measured as `2.12e−6` to ~1 %, so **A is pinned**.

## The refinement: first order is DERIVED

| level | status |
|---|---|
| B's first-order coefficient (`B = +x`) | **DERIVED** — forced by the measured redshift and deflection; no freedom at O(x) |
| B's `O(x²)` completion | **BOUNDARY** — G_030's no-go leaves this open |

The surviving freedom, measured: `g_rr` survivor vs GR = **−2.697×10⁻¹¹** relative at solar compactness,
**−29.68 %** at neutron-star compactness x = 0.247002. Both asserted, plus `B/x = 1.0000` for survivor and GR
(each to 4 dp at solar x).

## The medium escape is unavailable in AT

- AT has **no graviton** — the tensor sector is the massless spin-2 **ψ field** (`□ψ_μν = 0`).
- And the branch is not merely excluded but **unavailable**: any AT-natural index is built from **ρ**, which sources
  the metric *including* that sector, so a ρ-based index acts on light and on the metric sector identically — it
  **is** a metric.

## Registry

Added to the G_035 classification registry as `Spatial` → **BOUNDARY**, triaged `ScanDetectsIt: false`. Counts
become **25 / 11 / 3 of 39**; boundary index still **23**; no prior classification changed. G_033's D96-free era
count 15 → 16.

## Caveats

- First order in `x = GM/(Rc²)`; `O(x²)` statements are as measured in G_029/G_035.
- The redshift bound is quoted at ~1 %; a tighter bound would exclude proportionally tighter clock compensations.
