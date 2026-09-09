# ResearchY-NP_169 — Natural Scaling vs Mass Gap Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_169 (permanent)
**Title:** Natural Scaling vs Mass Gap — can any AT scaling preserve a positive gap as N→∞?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `NP_NewPhysics/ResearchY-NP_169.md`
**Depends on:** ResearchY-NP_168 (Yang–Mills mass-gap audit), QG161 (gauge sector), QG228
(record information), QG210 (octave occupancies)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_169_Tests.cs`

---

## Question

Given NP_168's exact refinement result

\[
\lambda_{\rm gap}(N)=\frac{4\pi^2\cdot91}{N^2}+O(N^{-4}),
\]

can **any natural Actualization scaling** \(f(N)\) preserve a **positive mass gap**

\[
m(N)=f(N)\cdot\lambda_{\rm gap}(N)
\]

as \(N\to\infty\)? The scaling \(f\) must be derived from **existing AT primitives only**;
ad-hoc scalings (chosen merely to force a positive limit) are rejected.

## The scaling boundary

Because \(\lambda_{\rm gap}=O(N^{-2})\), the limit of \(m(N)\) is set entirely by the growth
of \(f(N)=O(N^e)\):

| Growth of \(f(N)\) | Limit of \(m(N)\) | Verdict |
|---|---|---|
| \(f=o(N^2)\) (\(e<2\)) | \(m\to0\) | **ZERO GAP** |
| \(f=\Theta(N^2)\) (\(e=2\)) | \(m\to c>0\) | **POSITIVE GAP** |
| \(f=\omega(N^2)\) (\(e>2\)) | \(m\to\infty\) | **DIVERGENT** |

So the whole question reduces to: **does AT supply a natural \(N^2\) scaling?**

## Scaling audit

### 1. Physical scalings

- **Fixed lattice spacing \(a_0\)** (growing circumference \(L=Na_0\)): \(f=1/a_0^2=O(1)\),
  so \(m(N)=\lambda_{\rm gap}(N)/a_0^2\to0\). **ZERO GAP.**
- **Fixed circumference \(L_0\)** (\(a=L_0/N\)): \(f=1/a^2=N^2/L_0^2\), so
  \(m(N)\to 4\pi^2\cdot91/L_0^2>0\). **POSITIVE** — but this **imports the external length
  \(L_0\)** (not an AT primitive) and yields only the kinematic **compact-domain** Laplacian
  gap, not an infinite-volume mass gap.

### 2. Spectral-density scalings

The Weyl counting law for \(C_N(1..6)\) is \(N(\lambda)\approx 2N\sqrt{\lambda/c}\)
(\(c=4\pi^2\cdot91\)), verified exactly: \(N(m^2\lambda_{\rm gap})=2m\). The density of states
at the gap edge is

\[
\rho(\lambda_{\rm gap})=\frac{dN}{d\lambda}\Big|_{\lambda_{\rm gap}}
=\frac{N}{\sqrt{c\,\lambda_{\rm gap}}}=\frac{N^2}{c},
\]

so

\[
m(N)=\rho(\lambda_{\rm gap})\cdot\lambda_{\rm gap}\to 1.
\]

**POSITIVE** — but **tautological**: the gap measured in units of its own level spacing is one
mode by definition. It produces no physical mass scale.

### 3. Occupancy scalings

AT occupancy primitives (QG210/QG228) are fixed dimensionless numbers — none scales with \(N\):

| Primitive | Value | Growth |
|---|---|---|
| \(I_{\rm occ}=\mathrm{KL}(p\|\text{uniform})\) | 0.7513 nats | \(O(1)\) |
| \(\ln K=\ln 3\) | 1.0986 | \(O(1)\) |
| \(\Omega_\Lambda=I_{\rm occ}/\ln K\) | 0.6839 | \(O(1)\) |
| octave count \(K\) | 3 | \(O(1)\) |
| occupied modes \(\Sigma\,{\rm occ}\) | 95 | \(O(1)\) |
| total modes \(N\) | \(N\) | \(O(N)\) |

Every one gives \(m=f\cdot\lambda_{\rm gap}\to0\). **ZERO GAP.**

### 4. D96 family scalings

The \(C_N(1..6)\) family has a **fixed** step set \(\{1,\ldots,6\}\):

| Primitive | Value | Growth |
|---|---|---|
| degree \(2k\) | 12 | \(O(1)\) |
| \(\Sigma_{d=1}^{6}d^2\) | 91 | \(O(1)\), \(N\)-independent |
| size \(N\) | \(N\) | \(O(N)\) |

None is \(O(N^2)\). **ZERO GAP.**

### Ad-hoc scalings (rejected)

- \(f=N^2\): POSITIVE (\(\to c\)), but \(N^2\) is not an AT primitive — no occupancy,
  D96-family, or spectral quantity is \(\Theta(N^2)\) except the tautological DOS or the
  imported length. **Rejected as underivable.**
- \(f=N^3\): DIVERGENT. **Rejected outright** — no AT primitive grows faster than \(O(N)\).

---

## Theorem

> **Theorem (NP_169).** For the circulant family \(C_N(1..6)\) with
> \(\lambda_{\rm gap}(N)=4\pi^2(91)N^{-2}+O(N^{-4})\), a scaling \(f(N)=O(N^e)\) preserves a
> positive mass gap iff \(e=2\). Every existing AT primitive is \(O(1)\) or \(O(N)\), so no
> natural Actualization scaling preserves a positive gap: occupancy and D96-family scalings
> give ZERO GAP. The only positive limits are (a) the imported external length \(L_0\) of a
> fixed-compact-volume physical scaling — a kinematic finite-volume gap, not an infinite-volume
> mass gap — and (b) the tautological spectral-density renormalization
> \(\rho(\lambda_{\rm gap})\lambda_{\rm gap}\to1\). Any \(f=\omega(N^2)\) diverges and is ad-hoc.
> Hence the claim "AT provides a natural scaling that preserves a positive mass gap" is REFUTED. ∎

---

## Classification

| Scaling | Limit | Verdict | Legitimate mass gap? |
|---|---|---|---|
| Physical, fixed spacing \(a_0\) | \(m\to0\) | ZERO GAP | — |
| Physical, fixed volume \(L_0\) | \(m\to 4\pi^2\cdot91/L_0^2\) | POSITIVE | No — imported length, kinematic compact-domain |
| Spectral density \(\rho(\lambda_{\rm gap})\) | \(m\to1\) | POSITIVE | No — tautological level-spacing |
| Occupancy (all) | \(m\to0\) | ZERO GAP | — |
| D96 family (all) | \(m\to0\) | ZERO GAP | — |
| Ad-hoc \(f=N^2\) | \(m\to c\) | POSITIVE | No — underivable |
| Ad-hoc \(f=N^3\) | \(m\to\infty\) | DIVERGENT | No — underivable |

**Canonical registry decision:** no update to `Y_D_040_Tests.ClassificationRegistry`. NP_169 does
not reclassify a canonical D-series object; it restricts the inference that a positive mass gap
can be obtained from the already-DERIVED finite graph spectrum by a natural AT scaling.

---

## Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_169_Tests.cs`

| Test | Verifies |
|---|---|
| `Y_NP_169_PhysicalScalings` | fixed-spacing → ZERO; fixed-volume → POSITIVE (imported \(L_0\)) |
| `Y_NP_169_SpectralDensityScalings` | Weyl law \(N(\lambda)\approx2N\sqrt{\lambda/c}\); \(\rho\lambda_{\rm gap}\to1\) |
| `Y_NP_169_OccupancyScalings` | all occupancy primitives \(O(1)\)/\(O(N)\) → ZERO |
| `Y_NP_169_D96FamilyScalings` | degree/\(\Sigma d^2/N\) primitives → ZERO |
| `Y_NP_169_AdHocRejection` | \(N^2\) POSITIVE (underivable); \(N^3\) DIVERGENT (rejected) |
| `Y_NP_169_Classification` | ZERO for AT-native; POSITIVE only imported/tautological; DIVERGENT ad-hoc |
| `Y_NP_169_Run` | assumptions-first scientific report |

**Reproduction:**

```text
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_169"
```

---

## References

- ResearchY-NP_168 (Yang–Mills existence and mass gap audit).
- `CosmologicalFractionsOrigin` (QG210/QG228, octave occupancies and \(I_{\rm occ}\)).
- `GaugeSectorOrigin.SpectralGap` (QG161, the \(\lambda_2\) mass-gap scale).
- Clay Mathematics Institute, *Yang–Mills & the Mass Gap*.
