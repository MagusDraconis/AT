# ResearchY-G_028 — Clock Sector Closure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_028 (permanent)
**Title:** Clock Sector Closure Audit — is the physical clock law `ρ^(1/d)` or `ρ^(1/d)·e^ψ`?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_028.md`
**Depends on:** G_009 (the clock law), G_021 (the metric), G_022, G_023 (the invariant `k = B − A` and the spatial route), G_024 (**corrected here**), G_025, G_026, G_027
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_028_Tests.cs` (7/7 PASSED, ~0.04 s)
**Core:** `AT.Core/ResearchXH/ClockSectorClosure.cs`

## The question

Is the physical clock law

```
A:  dτ/dt = ρ^(1/d)                 or      B:  dτ/dt = ρ^(1/d)·e^ψ  ?
```

## The one fact that decides it

The Newtonian potential **is** the time exponent:

```
g₀₀ = −e^(2A) ,   A = σ + ψ ,   σ = (1/d)·ln ρ ,   Φ/c² = A
⟹  a = −∇Φ = −(1/d)∇ln ρ − ∇ψ
```

**The stated source law `a = −(1/d)∇ln ρ` holds iff `∇ψ = 0`** — a *constant* ψ, which is nothing but a global choice of time unit. Any ψ varying in space changes the source law.

### Critical question: can ψ be nonzero without changing the source law?

> **NO.** Every nonzero ψ adds `−∇ψ` to the acceleration, and within the trace-preserving QG207 direction `A − σ = ψ` *identically*, so ψ ≠ 0 forces the time exponent away from σ. A constant ψ is physically empty — it cancels from the redshift ratio. This is a **proof**, not a judgement.

## Candidates at every body

`ρ = e^(−dx)`, `σ = −x`, `x = GM/(Rc²)`. QG207: `A = σ + ψ`, `B = σ − ψ/2` (d = 3).

| candidate | body | x | A | B | **z** | γ_exact | z > 0? |
|---|---|---|---|---|---|---|---|
| **A: ρ^(1/d)** | Earth | 6.957e−10 | −6.957e−10 | −6.957e−10 | **+6.957e−10** | −1 | ✅ |
| | Sun | 2.1225e−6 | −2.1225e−6 | −2.1225e−6 | +2.1225e−6 | −1 | ✅ |
| | 1e−4 | 1e−4 | −1.0000e−4 | −1.0000e−4 | +1.0000e−4 | −1 | ✅ |
| | J0740+6620 | 0.247002 | −0.247002 | −0.247002 | **+0.2801817** | −1 | ✅ |
| **B: ρ^(1/d)·e^ψ**, ψ=−4σ | Earth | 6.957e−10 | **+2.0871e−9** | −2.0871e−9 | **−2.0871e−9** | +1 | ❌ |
| | Sun | 2.1225e−6 | +6.3675e−6 | −6.3675e−6 | −6.3675e−6 | +1 | ❌ |
| | 1e−4 | 1e−4 | +3.0000e−4 | −3.0000e−4 | −3.0000e−4 | +1 | ❌ |
| | J0740+6620 | 0.247002 | +0.741006 | −0.741006 | **−0.5233658** | +0.2272 | ❌ |
| **C: spatial route** (`A = σ` fixed) | 1e−4 | 1e−4 | −1.0000e−4 | +1.0000e−4 | +1.0000e−4 | **+1** | ✅ |
| | J0740+6620 | 0.247002 | −0.247002 | +0.1645877 | **+0.2801817** | **+1** | ✅ |

**γ = +1 within the trace-preserving family fixes ψ = −4σ uniquely**, and that forces **A = −3σ = +3x > 0**. The potential becomes **positive**: the surface clock runs **fast**, every gravitating body is **blueshifted**, and the clock's **sign is inverted** — it is not merely shifted.

## The five requirements

| requirement | **A: ρ^(1/d)** | **B: QG207 (ψ=−4σ)** | **C: spatial route** |
|---|---|---|---|
| 1. source law `a = −(1/d)∇ln ρ` | **YES** | **NO** (off by 4×) | **YES** |
| 2. Earth/GPS agreement | **YES** (0.000 σ) | **NO — 2000 σ** | **YES** |
| 3. neutron-star redshift audit (G_020) | **YES** (z_AT = 0.2801817) | **NO** (predicts −0.5234) | **YES** |
| 4. positivity for compact objects | **YES** (all bodies) | **NO** (every body) | **YES** |
| 5. no new primitive | **YES** | YES | **NO** — volume cost 3.437585 |

The Earth separation for candidate B: `|−2.087e−9 − 6.957e−10| / (0.2 % × 6.957e−10)` = **2000.4 σ**.

## Correction to G_024 (and hence to the restored QG212)

G_024 concluded that the QG207 completion produces **"NO solar-system conflict"**, quoting a GPS bound `|ψ| ≲ 2e−3` and calling it **7.2e5× looser** than required.

**That comparison was wrong in kind.** It treated the clock shift `e^(4x) − 1 = 2.78e−9` as an *additive* perturbation on a clock of 1. But the observable is the redshift **relative to infinity**, and the conformal law *already* predicts `z ≈ +x = 6.957e−10`. A shift of 2.78e−9 is not small beside that — it is **4×**, and it **reverses the sign**.

Corrected bound:

```
|ψ| ≲ 0.2 % × z_AT = 1.391e−12     — not 2e−3
```

i.e. G_024's quoted bound is **~1.44e9× too loose**, and ψ = −4σ (2.783e−9) is excluded at **2000 σ** by the Earth redshift alone.

**Consequence:** the QG212 "physical sector" — the ψ ≠ 0 completion at ψ = −4σ — is **not** the physical sector. It is a *repulsive* geometry. G_024 restored QG212's two-sector table; G_028 shows that the sector it called physical is observationally refuted, and that the optics fix cannot come from the trace-free direction at all.

## Output

| label | content |
|---|---|
| **DERIVED** | **`dτ/dt = ρ^(1/d)`** — candidate A satisfies **all five** requirements, exactly. Positivity holds at every body including compact objects. Nothing beyond ρ is used. |
| **REFUTED** | **`dτ/dt = ρ^(1/d)·e^ψ`** at the ψ that γ = +1 requires (ψ = −4σ, unique in the trace-preserving family): A = +3x > 0, blueshift at every body, z negative at compactness, source law broken by 4×, **2000 σ** at Earth. Also REFUTED: G_024's "no solar-system conflict" and its 2e−3 bound. |
| **BOUNDARY** | the **spatial route** (A held at σ, `B = ½ln(2 − e^(2σ))`) — the *only* way to reach γ = +1 without moving the clock. It satisfies requirements 1–4 and fails 5: it leaves the trace-free direction, so the volume measure becomes **3.437585×** the count at J0740+6620. |

## **CLOCK CLOSED** — on `dτ/dt = ρ^(1/d)`

The clock sector closes, and the closure **relocates** the failure: the clock is sound, and the entire open problem is the **spatial** sector. γ = −1 (the conformal value) is excluded at Cassini 8.6957e4 σ — but *optics is not a clock-sector requirement*, and the fix cannot be bought with the clock.

**The decisive structural result:** ψ and the clock are **not independent**. Bringing the tensor face in to fix optics necessarily moves the time exponent — which is why every attempt to repair γ from the trace-free direction repays the debt in the clock, and the repayment is a sign inversion.

## Opened

1. **Fix optics in the spatial sector** — G_023's route is the only survivor; its 3.437585 volume cost at J0740+6620 now needs a verdict of its own (primitive, or within-ψ?).
2. **Re-audit QG212's "physical sector"** — its γ = +1 member is repulsive and excluded at 2000 σ.
3. **Re-derive the GPS bound** for every ψ proposal in the corpus (the 2e−3 error is a *method* error, not a typo).

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_028_Tests.cs` — **7/7 PASSED**
**Group total:** G_001–G_028 = **230/230 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_028"`

| test | asserts |
|---|---|
| `Y_G_028_CandidateARhoOnlySatisfiesAllFiveRequirements` | source law, 0.000 σ at Earth, z_AT = 0.2801817, positivity at all bodies, no new primitive |
| `Y_G_028_CandidateBQg207IsRefuted` | A > 0 and z < 0 at every body; 2000 σ; J0740 z = −0.5233658; ψ contributes 4× the ρ term |
| `Y_G_028_PsiCannotBeNonzeroWithoutChangingTheSourceLaw` | the **critical answer**: NO — `A − σ = ψ` identically, so ψ ≠ 0 must move the clock |
| `Y_G_028_GammaPlusOneForcesTheClockToInvert` | ψ = −4σ unique; A + B = 0; A = +3x; γ > 0 ⟺ A > 0 across the whole family |
| `Y_G_028_SpatialRoutePreservesTheClockAtAVolumeCost` | A = σ preserved, z positive, γ = +1, volume cost 3.437585 |
| `Y_G_028_GpsBoundCorrectedAgainstG024` | bound ≈ 1.391e−12, **1.4e9× tighter** than G_024's 2e−3 |
| `Y_G_028_Run` | the full report |
