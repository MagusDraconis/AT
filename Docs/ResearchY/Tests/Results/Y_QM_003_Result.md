# Y_QM_003 - Result

**Audit:** ResearchY-QM_003 - Dispersion Closure Audit
**Verdict:** **BOUNDARY**
**Tests:** 8/8 PASSED

## Answer

**BOUNDARY - the fold can be replaced exactly, and the replacement is not free. LOCATED: the fold is the price of
LOCALITY, not of discreteness.** It is forced by the conjunction of three AT facts: the generator is
**antisymmetric**, it is **local**, and the lattice is **periodic**.

## The theorem

A real antisymmetric operator on a periodic ring has an **odd** symbol `f`, and oddness forces `f(π) = 0` identically:
**the zone edge is stationary for every antisymmetric lattice generator.**

| candidate | band | f(π) |
|---|---|---|
| nearest-neighbour (the AT difference) | 1 | **1.22E-016** |
| 5-point antisymmetric (4th order) | 2 | **2.04E-016** |
| 7-point antisymmetric (6th order) | 3 | **2.69E-016** |
| **spectral (exact) derivative**, same 96 sites | 48 | **3.14E+000** |
| D96³, per axis | 1 | **1.22E-016** |

## The fold is the sign change of the group velocity

| candidate | linear window | fold channel | fold / zone | reversed | **reversed occupied** |
|---|---|---|---|---|---|
| **nearest-neighbour** | **1** | **24.00** | 0.5000 | 24 | **21 of 42** |
| 5-point (4th order) | **6** | 27.46 | 0.5722 | 21 | 18 of 42 |
| 7-point (6th order) | **11** | 29.58 | 0.6163 | 19 | 16 of 42 |
| **spectral (exact)** | **48** | **none** | - | **0** | **0 of 42** |
| D96³ per axis | 1 | 24.00 | 0.5000 | 24 | 21 of 42 |

A wider stencil buys linearity (**1 → 6 → 11**) and only **pushes** the fold (**24.00 → 27.46 → 29.58**), at **one more
neighbour shell per order**.

## Locality is what the exact replacement costs

| candidate | band | non-zeros per row | local | has fold |
|---|---|---|---|---|
| nearest-neighbour | 1 | **2** | yes | yes |
| 4th order | 2 | 4 | yes | yes |
| 6th order | 3 | 6 | yes | yes |
| **spectral (exact)** | 48 | **96** | **no** | **no** |

**The fold and the locality are in exact opposition** - verified candidate by candidate.

## The two routes that do not help

| route | status | costs |
|---|---|---|
| **D96³** | **makes it worse** | folded fraction **1/2 (1D) → 7/8 (3D)** = `1 − (1/2)^d` |
| **the continuum limit** | **leaves it at a fixed zone fraction** | at a fixed physical wavenumber the deviation is frozen at **9.968E-002** at every N; it falls as **1/N^1.97** only at a fixed mode index (a different state) |

The zone-edge deviation is **1.000E+000** and the fold stays at channel **N/4** at every resolution - the fold is
**scale-invariant in the zone fraction**.

## Notes

**Three defects in the audit's own first version are recorded rather than quietly fixed.** (1) `HasAFold` bisected to
machine zero and then tested `velocity < 0`, which fails because `cos(π/2 + 1e-60)` evaluates to **−0.0** and
`−0.0 < 0.0` is **false** in IEEE — so every local candidate was reported as having **no** fold and the
locality claim came out **False**; the test is now the channel census. (2) The bisection's fold position needed its
own tolerance (`23.99999999` rather than `24.0`) because the symmetric finite difference of `sin` vanishes *exactly*
at π/2. (3) The refinement table's first version computed a "fixed physical wavenumber" as a formula in which the cell
count cancelled, so it reported order **0** — the measurement has been kept, because it is the **correct and important**
one, and the two questions are now separated explicitly: a fixed physical wavenumber does **not** improve, and a fixed
mode index improves as `1/N²` but describes a **different state**.

**No group-G change.** The group-G consistency counts and the `TemporalIndependenceAudit` registry are unchanged; the
G_027, G_033 and G_035 scanners were re-run after the new core was added.
