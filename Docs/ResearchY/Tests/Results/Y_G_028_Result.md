# Y_G_028_Result.md — ResearchY-G_028 Clock Sector Closure Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_028_Tests.cs`
**Core:** `AT.Core/ResearchXH/ClockSectorClosure.cs`
**Run:** 2026-09-13
**Result:** ✅ 7/7 PASSED (~0.04 s) — group G total **230/230 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_028"`

## The question

`dτ/dt = ρ^(1/d)` **or** `dτ/dt = ρ^(1/d)·e^ψ` ?

## The one fact that decides it

```
g₀₀ = −e^(2A),  A = σ + ψ,  σ = (1/d)ln ρ,  Φ/c² = A
⟹  a = −∇Φ = −(1/d)∇ln ρ − ∇ψ
```

**The source law holds iff ∇ψ = 0** — a constant ψ, i.e. a global time-unit choice. Any spatially varying ψ changes the source law.

**CAN ψ BE NONZERO WITHOUT CHANGING THE SOURCE LAW? → NO.** `A − σ = ψ` identically, so ψ ≠ 0 must move the time exponent. This is a proof.

## The three candidates

| | **A: ρ^(1/d)** | **B: ρ^(1/d)e^ψ** (ψ=−4σ) | **C: spatial route** |
|---|---|---|---|
| clock | e^(−x) | e^(3x) | e^(−x) |
| z at J0740+6620 | **+0.2801817** | **−0.5233658** | **+0.2801817** |
| z at Earth | **+6.957e−10** | **−2.087e−9** | +6.957e−10 |
| A (potential) | −x (**attractive**) | **+3x (repulsive)** | −x (**attractive**) |
| γ_exact | −1 | +1 (1st order), 0.2272 exact | **+1** |
| volume measure | ρ | ρ | **3.437585× ρ** |

## Five requirements

| requirement | A | B | C |
|---|---|---|---|
| 1. source law | **YES** | **NO** (4× off) | **YES** |
| 2. Earth/GPS | **YES** (0.000 σ) | **NO — 2000 σ** | **YES** |
| 3. neutron-star audit (G_020) | **YES** | **NO** | **YES** |
| 4. positivity | **YES** | **NO** (every body) | **YES** |
| 5. no new primitive | **YES** | YES | **NO** |

**2000 σ at Earth**, computed: `|−2.087e−9 − 6.957e−10| / (0.2 % × 6.957e−10)`.

## Correction to G_024 / QG212

G_024 said the QG207 completion creates **"NO solar-system conflict"**, quoting `|ψ| ≲ 2e−3` as 7.2e5× loose. **The comparison was wrong in kind** — it treated the shift as *additive* on a clock of 1, but the observable is the redshift **relative to infinity**, where the conformal law already gives `z ≈ +x = 6.957e−10`. The shift 2.78e−9 is **4×** the whole effect and **reverses its sign**.

```
correct bound:  |ψ| ≲ 0.2 % × z_AT = 1.391e−12      (not 2e−3)
G_024's bound is 1.44e9× too loose;  ψ = −4σ is excluded at 2000 σ
```

**Consequence:** QG212's "physical sector" (ψ ≠ 0 at ψ = −4σ) is a **repulsive** geometry and is **observationally refuted**. The optics fix cannot come from the trace-free direction.

## Output

| label | content |
|---|---|
| **DERIVED** | **`dτ/dt = ρ^(1/d)`** — satisfies **all five** requirements exactly; positivity everywhere; nothing beyond ρ. |
| **REFUTED** | **`dτ/dt = ρ^(1/d)·e^ψ`** at the ψ that γ = +1 forces (ψ = −4σ, unique): A = +3x > 0, blueshift everywhere, z < 0 at compactness, source law broken 4×, **2000 σ** at Earth. Also REFUTED: G_024's "no solar-system conflict" and the 2e−3 bound. |
| **BOUNDARY** | the **spatial route** (`A = σ`, `B = ½ln(2 − e^(2σ))`) — the only γ = +1 that keeps the clock; satisfies 1–4, fails 5 (volume cost 3.437585). |

## **CLOCK CLOSED**

on `dτ/dt = ρ^(1/d)`. The closure **relocates** the failure: the clock is sound; the entire open problem is the **spatial** sector (γ = −1 excluded at Cassini 8.6957e4 σ). ψ and the clock are **not independent** — repairing γ from the trace-free direction repays the debt in the clock, and the repayment is a sign inversion.

## Tests

| test | result |
|---|---|
| `CandidateARhoOnlySatisfiesAllFiveRequirements` | ✅ |
| `CandidateBQg207IsRefuted` | ✅ |
| `PsiCannotBeNonzeroWithoutChangingTheSourceLaw` | ✅ |
| `GammaPlusOneForcesTheClockToInvert` | ✅ |
| `SpatialRoutePreservesTheClockAtAVolumeCost` | ✅ |
| `GpsBoundCorrectedAgainstG024` | ✅ |
| `Run` | ✅ |

**Headline:** the clock sector **closes on `dτ/dt = ρ^(1/d)`**. The alternative `ρ^(1/d)·e^ψ` is **refuted at 2000 σ by the Earth redshift alone** — the only ψ giving γ = +1 (ψ = −4σ, unique within the trace-preserving family) makes the potential positive, i.e. repulsive, inverting the clock at every gravitating body. G_024's "no solar-system conflict" rested on comparing a shift against a clock of 1 instead of against the redshift relative to infinity; the correct GPS bound on |ψ| is **1.391e−12**, **1.4e9× tighter** than quoted. And ψ **cannot** be nonzero without changing the source law: `A − σ = ψ` identically.
