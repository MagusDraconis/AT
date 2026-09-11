# ResearchY-D_047 — Degeneracy-Splitting Amplification Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_047 (permanent)
**Title:** Degeneracy-Splitting Amplification Audit
**Status:** COMPLETE
**Date:** 2026-09-10
**File:** `D_ResonanceStructure/ResearchY-D_047.md`
**Depends on:** D_028 (span), D_040 (classification registry), T_015 (spectral robustness)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_047_Tests.cs`

---

## Purpose

**Can D96 degeneracy splitting amplify weak perturbations?** Equivalently: is the
amplification factor

$$\mathcal{G} \;=\; \frac{\Delta(\text{observable})}{\varepsilon}$$

unbounded as the perturbation strength $\varepsilon \to 0$? Three observables are measured —
**eigenvalue splitting** (the attractor/eigenspace count shift $\Delta A$), the **attractor
count shift** itself, and the **spectral entropy shift** $\Delta E$ — for four cases: **D96**,
**Random**, **Complete** ($K_{96}$), and **D96³**.

**No canonical value, equation, or claim status is changed. Research only.**

---

## Method

1. Build the Laplacian $L$ of each case graph (D96 = circulant $C_{96}(\pm1..\pm6)$;
   random = seeded sparse $p=0.3$; complete = $K_{96}$; D96³ = the tensor product
   D96⊗D96⊗D96 with $96^3 = 884\,736$ modes, spectrum the Minkowski sum
   $\Lambda = \lambda_i+\lambda_j+\lambda_k$).
2. Perturb: $L(\varepsilon) = L + \varepsilon P$, where $P$ is a **deterministic
   symmetry-breaking ±1 edge-sign Laplacian** on the graph's edge set (an LCG bit stream,
   seed 12345; row sums vanish so $L(\varepsilon)$ is the Laplacian of
   $w_{ij} \to w_{ij}(1+\varepsilon s_{ij})$). For D96³ the same pattern is applied to **each
   of the three factor rings** (so the 3-factor first-order response is up to 3× the
   single-factor cases — this factor does not affect any qualitative conclusion).
3. Sweep $\varepsilon$ over 9 decades ($10^{-9} \ldots 10^{-1}$; $10^{-8} \ldots 10^{-2}$ for
   D96³) and measure at each point:
   - $A(\varepsilon)$ — number of **distinct eigenvalues** = number of attractor eigenspaces
     (tolerance-resolved, $10^{-9}$ for one factor, $10^{-8}$ for the 3-factor sums);
   - $E(\varepsilon)$ — **Shannon entropy** (nats) of the basin distribution $p_i = m_i/N$;
   - $\Delta\lambda_{\text{rms}}$ — the relative RMS spectral shift, the **continuous**
     observable.
4. Report the gains $\mathcal{G}_A = \Delta A/\varepsilon$, $\mathcal{G}_E = \Delta E/\varepsilon$,
   $\mathcal{G}_\lambda = \Delta\lambda_{\text{rms}}/\varepsilon$.
5. Deterministic and reproducible: no unseeded randomness, no external data.

---

## 1. The amplifier reservoirs

| case | $N$ | $A_0$ | degenerate groups | max multiplicity | $\Delta E_{\text{lock}}$ (nats) | headroom $N-A_0$ |
|---|---|---|---|---|---|---|
| **D96** | 96 | 45 | 44 ($[42\times2,5,6]$) | 6 | **0.80231** | **51** |
| **random** | 96 | 96 | 0 | 1 | 0 | 0 |
| **complete** | 96 | 2 | 1 ($[95]$) | 95 | **4.50644** | **94** |
| **D96³** | 884 736 | 16 080 | 16 079 | 738 | **4.16569** | **868 656** |

"Degeneracy" is the *reservoir*: each degenerate group of multiplicity $m$ is a single
attractor that can be split into up to $m$ attractors. Random's all-singleton spectrum
($A_0 = 96 = N$) has **no reservoir at all**.

---

## 2. The amplitude sweep

### 2.1 Attractor-count gain $\mathcal{G}_A = \Delta A/\varepsilon$

| $\varepsilon$ | D96 | random | complete | D96³ |
|---|---|---|---|---|
| $10^{-9}$ | 2.600e+10 | **0** | 3.700e+10 | — |
| $10^{-8}$ | 4.800e+09 | **0** | 9.300e+09 | 3.477e+12 |
| $10^{-7}$ | 5.100e+08 | **0** | 9.400e+08 | 1.077e+12 |
| $10^{-6}$ | 5.100e+07 | **0** | 9.400e+07 | 1.339e+11 |
| $10^{-5}$ | 5.100e+06 | **0** | 9.400e+06 | 1.357e+10 |
| $10^{-4}$ | 5.100e+05 | **0** | 9.400e+05 | 1.360e+09 |
| $10^{-3}$ | 5.100e+04 | **0** | 9.400e+04 | 1.360e+08 |
| $10^{-2}$ | 5.100e+03 | **0** | 9.400e+03 | 1.360e+07 |
| $10^{-1}$ | 5.100e+02 | **0** | 9.400e+02 | — |

For every degenerate case the gain is $\propto 1/\varepsilon$ over the whole resolved range —
the **amplification is divergent**, and the response is a **step function** at
$\varepsilon = 0$, not a smooth one.

### 2.2 D96 in detail

| $\varepsilon$ | $\Delta A$ | $\mathcal{G}_A$ | $\Delta E$ | $\mathcal{G}_E$ | $\mathcal{G}_\lambda$ |
|---|---|---|---|---|---|
| $10^{-9}$ | 26 | 2.600e+10 | 0.435849 | 4.358e+08 | 0.0562 |
| $10^{-8}$ | 48 | 4.800e+09 | 0.758992 | 7.590e+07 | 0.0562 |
| $10^{-7}$ | **51** | 5.100e+08 | **0.802314** | 8.023e+06 | 0.0562 |
| $10^{-6}$ | 51 | 5.100e+07 | 0.802314 | 8.023e+05 | 0.0562 |
| $10^{-3}$ | 51 | 5.100e+04 | 0.802314 | 8.023e+02 | 0.0556 |
| $10^{-1}$ | 51 | 5.100e+02 | 0.802314 | 8.023e+00 | 0.0968 |

$\Delta A$ saturates at the **full headroom 51 = N − A₀ = 42 + 4 + 5** and $\Delta E$ at
**0.802314 nats**, both independently of $\varepsilon$ below $10^{-7}$. Meanwhile
$\mathcal{G}_\lambda$ stays at $\approx 0.056$ — **finite and $\varepsilon$-independent**.

### 2.3 D96³ — the hardest amplifier, with a ceiling

| $\varepsilon$ | $\Delta A$ | $\mathcal{G}_A$ | $\Delta E$ | unlocked |
|---|---|---|---|---|
| $10^{-8}$ | 34 767 | 3.477e+12 | 1.132312 | 4.0 % of headroom |
| $10^{-7}$ | 107 670 | 1.077e+12 | 2.123009 | 12.4 % |
| $10^{-6}$ | 133 935 | 1.339e+11 | 2.376600 | 15.4 % |
| $10^{-4}$ | 135 994 | 1.360e+09 | 2.395366 | **15.7 %** |
| $10^{-2}$ | 136 005 | 1.360e+07 | 2.395464 | **15.7 %** |

$\mathcal{G}_A$ peaks at $\gtrsim 10^{12}$ — **≳ 2000× D96** at the same $\varepsilon$ — but the
unlock saturates at **15.7 %** of the headroom (136 005 of 868 656). The residual degeneracy is
**protected**: the perturbation is applied factor-wise and is invariant under permutations of
the three factors, so every permuted triple sum $\lambda_i+\lambda_j+\lambda_k$ stays *exactly*
degenerate. Entropy recovers more of its ceiling (57.5 %) than the count does, because the
unsplit permutation orbits carry large multiplicities.

---

## 3. The Degeneracy-Lock Theorem

The $\varepsilon$-independence of the released entropy is exact, not a numerical accident:

> **Theorem (D_047 — Degeneracy Lock).** Let a Laplacian $L$ on $N$ nodes have
> $\mathcal{A}_0$ distinct eigenvalues with multiplicities $m_i$ ($\sum_i m_i = N$). Under any
> symmetry-breaking perturbation that resolves *every* degenerate group into singletons, the
> spectral entropy of the basin distribution $p_i = m_i/N$ increases by exactly
> $$\Delta E_{\text{lock}} \;=\; \frac{1}{N}\sum_{m_i>1} m_i \ln m_i,$$
> **independently of the perturbation amplitude** $\varepsilon$. Consequently
> $\mathcal{G}_E = \Delta E_{\text{lock}}/\varepsilon$ and, with
> $\Delta A_{\max} = \sum_{m_i>1}(m_i-1) = N - \mathcal{A}_0$, also
> $\mathcal{G}_A \to \infty$ as $\varepsilon \to 0^+$.

**Proof.** After full resolution every multiplicity is 1 ($m'_i = 1$ for all $i$), so
$E_1 = -\sum N^{-1}\ln N^{-1} = \ln N$. Before,
$E_0 = -\sum_i (m_i/N)\ln(m_i/N)$. Hence
$\Delta E = \ln N + \sum_i (m_i/N)\ln(m_i/N)
= \ln N - \frac{1}{N}\sum_i m_i\ln N + \frac{1}{N}\sum_i m_i\ln m_i
= \ln N - \ln N + \frac{1}{N}\sum_{m_i>1} m_i\ln m_i$. ∎

**Verified numerically:** 42 doublets + $[5]$ + $[6]$ over $N=96$ gives
$\frac{1}{96}(42\cdot2\ln2 + 5\ln5 + 6\ln6) = 0.802314$ — matched by the measured $\Delta E$ to
6 decimals; $K_{96}$ gives $\frac{95}{96}\ln 95 = 4.506441$ — matched likewise.

The theorem is the quantitative form of a structural statement: a **degeneracy is a lock**, and
the locked entropy is a closed-form functional of the multiplicity structure alone.

---

## 4. Random: the exact null

The degenerate-free control shows **no amplification whatsoever**:

- $A \equiv 96$ and $E \equiv \ln 96$ for every $\varepsilon$ in $[10^{-9}, 10^{-1}]$;
- $\Delta A \equiv \Delta E \equiv 0$, $\mathcal{G}_A = \mathcal{G}_E = 0$;
- still $A = 96$ at $\varepsilon = 1.0$, where the operator is strongly perturbed.

Random's smallest eigenvalue spacing is $0.0123$ — a finite, $\varepsilon$-independent barrier
that no first-order perturbation can remove. **A spectrum with no degeneracy has no lock, so
there is nothing to unlock and nothing to amplify.**

---

## 5. What is NOT amplified

$\mathcal{G}_\lambda = \Delta\lambda_{\text{rms}}/\varepsilon$ is **finite and
$\varepsilon$-independent** in all cases:

| case | $\mathcal{G}_\lambda$ at $10^{-9}$ | at $10^{-6}$ | at $10^{-3}$ |
|---|---|---|---|
| D96 | 0.0562 | 0.0562 | 0.0556 |
| random | 0.0523 | 0.0523 | 0.0524 |

The two agree to ~7 %: **D96's continuous spectral response is the same as the
degeneracy-free random control's.** Over the same 6-decade span $\mathcal{G}_A$ and
$\mathcal{G}_E$ grow by $\ge 10^5$ while $\mathcal{G}_\lambda$ does not move. The amplification
is therefore **purely a degeneracy effect**, not a spectral one — the continuous spectrum obeys
ordinary linear-response behaviour.

---

## 6. Bounds and caveats

1. **Resolution floor.** The weakest $\varepsilon$ at which the *full* split is observed
   ($\approx 10^{-7}$ for D96) is set by the numerical tolerance, not by any intrinsic physical
   scale. Below it the partial $\Delta A$ ($26$ at $10^{-9}$) is a measurement-resolution
   artifact, not a physical threshold.
2. **Case normalisation.** The ±1 pattern is applied on each case's own edge set; amplitudes are
   comparable in edge-weight units, not in Frobenius norm. The divergence/zero dichotomy is
   invariant under any such rescaling.
3. **D96³ factor convention.** The pattern is applied with the same $\varepsilon$ to each of the
   three factor rings; the 3-fold first-order response does not change the ceiling result.
4. **Second-order region.** No regime was probed in which a symmetry-breaking perturbation
   *fails* to split a degeneracy — first-order degenerate perturbation theory forbids it
   generically.

---

## 7. Classification

| Component | Status |
|---|---|
| $\Delta E_{\text{lock}} = \frac{1}{N}\sum_{m_i>1} m_i\ln m_i$ | **DERIVED** (closed form in the multiplicity structure) |
| $\Delta A_{\max} = N - \mathcal{A}_0 = \sum(m_i-1)$ | **DERIVED** (degeneracy headroom) |
| Degeneracy splitting is first-order in $\varepsilon$ | **DERIVED** (degenerate perturbation theory) |
| The $\varepsilon$-independence of the release (step response at $\varepsilon=0$) | **EMERGENT** |
| D96³ partial unlock protected by factor-permutation symmetry | **DERIVED** |
| "D96 amplifies weak perturbations in its continuous spectrum" | **REFUTED** ($\mathcal{G}_\lambda$ finite) |
| Random / complete / D96³ reference roles | **BOUNDARY** (case selection) |
| Resolution floor $\varepsilon^\ast$ at which full splitting is seen | **BOUNDARY** (numerical, not physical) |

**Classification-guard:** this audit introduces **no reclassification** of any prior result and
does **not** touch `Y_D_040_Tests.ClassificationRegistry`. D_028's span/3-family-window
classification and D_040's registry are unaffected.

---

## Theorem (D_047)

> **Theorem (D_047).** Degeneracy splitting in D96 amplifies weak perturbations **without
> bound**, but only on degeneracy-indexed (structural) observables. Any symmetry-breaking
> perturbation of amplitude $\varepsilon > 0$ releases **exactly**
> $\Delta E_{\text{lock}} = \frac{1}{N}\sum_{m_i>1} m_i\ln m_i$ nats of spectral entropy
> (D96: $0.802314$; $K_{96}$: $4.506441$; D96³: partial, $2.3955$) and up to
> $\Delta A_{\max} = N-\mathcal{A}_0$ attractors (D96: $51$; $K_{96}$: $94$; D96³: $136\,005$ of
> $868\,656$, permutation-protected), **independently of how weak $\varepsilon$ is** — so
> $\mathcal{G}_A, \mathcal{G}_E \to \infty$ as $\varepsilon \to 0^+$. The continuous observable
> is **not** amplified: $\mathcal{G}_\lambda$ is finite and $\varepsilon$-independent
> ($0.0562$ for D96 vs $0.0523$ for the degeneracy-free random control). Random is an exact null
> ($\Delta A \equiv \Delta E \equiv 0$, $\mathcal{G} \equiv 0$). D96³ is the hardest amplifier
> ($\mathcal{G}_A$ peak $\gtrsim 10^{12}$) but capped at $15.7\%$ of its headroom.
>
> *Proof sketch.* (1) Full resolution gives $E_1 = \ln N$; subtracting $E_0$ yields the closed
> form (Section 3, verified to 6 decimals). (2) $\Delta A$ saturates at $N-\mathcal{A}_0$ because
> every degenerate group of size $m$ yields exactly $m-1$ new attractors (Section 2.1,
> verified). (3) Both are $\varepsilon$-independent step functions → $\mathcal{G} \propto 1/\varepsilon$
> (Section 2). (4) A non-degenerate spectrum has $\mathcal{A}_0 = N$, so the closed form is
> identically zero (Section 4). (5) $\mathcal{G}_\lambda$ is the ordinary linear-response
> coefficient of a symmetric eigenvalue problem (Section 5). (6) Factor-wise perturbations
> preserve factor-permutation symmetry exactly (Section 2.3). ∎

---

## Dependency Graph

```
Difference → Actualization → Spectrum (N=96)
 → multiplicity structure [42×2, 5, 6]           (D96 = 44 degenerate groups)
 → degeneracy lock  ΔE_lock = (1/N)·Σ m ln m      (D_047, DERIVED 0.80231 nats)
 → headroom         ΔA_max  = N − A₀ = Σ(m−1)     (D_047, DERIVED 51)
 → symmetry-breaking ε → STEP release             (D_047, EMERGENT)
 → G_A, G_E ∝ 1/ε → divergent amplification
 → G_λ finite 0.0562 (continuous spectrum NOT amplified)
```

---

## xUnit candidates

The suite `Y_D_047_Tests.cs` verifies each element numerically:

| Test | Verifies |
|---|---|
| `Y_D_047_DegeneracyStructure` | reservoir table: D96 45/44/42/6, K96 2/95, random 96/0, D96³ 16 080 |
| `Y_D_047_AttractorCountGain` | step saturation at $N-A_0$, $\mathcal{G}_A \propto 1/\varepsilon$ |
| `Y_D_047_DegeneracyLockEntropy` | closed form 0.80231 / 4.50644 / 0, $\varepsilon$-independence |
| `Y_D_047_RandomNullResponse` | $\Delta A \equiv \Delta E \equiv 0$ up to $\varepsilon = 1.0$ |
| `Y_D_047_SpectralGainFinite` | $\mathcal{G}_\lambda$ finite, $\varepsilon$-independent, matches random |
| `Y_D_047_CubeAmplificationAndCeiling` | D96³ peak $\gtrsim 10^{11}$, 15.7 % permutation ceiling |
| `Y_D_047_Run` | research report |

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Does the amplification require a *specific* perturbation? | **NO** — any symmetry-breaking perturbation, to first order |
| Does it require tuning of $\varepsilon$? | **NO** — weaker is *stronger* ($\mathcal{G} \propto 1/\varepsilon$) |
| Is it a property of D96's spectrum rather than its graph? | **YES** — it is a functional of the multiplicity structure alone |
| Is the continuous spectrum amplified? | **NO** — $\mathcal{G}_\lambda$ finite and equal to random's |
| Are new primitives or canonical values introduced? | **NO** — research only |

---

## Counterexamples

1. **Random** ($\mathcal{A}_0 = N$, no degenerate group): $\Delta A = \Delta E = 0$ for every
   $\varepsilon$ — the closed form returns zero. *Refutes any claim that the amplification is a
   generic property of all spectra.*
2. **D96³ at large $\varepsilon$**: unlocked stays at 15.7 % of headroom — *refutes the claim
   that $\Delta A$ always reaches $N - \mathcal{A}_0$.*
3. **$\mathcal{G}_\lambda$**: identical order for D96 ($0.0562$) and random ($0.0523$) —
   *refutes the claim that the spectral response itself is amplified.*

---

## Open Problems

1. **D_047 OP1 — exact permutation-orbit counting.** The D96³ ceiling (136 005 unlocked) is not
   yet derived in closed form from the octahedral orbit structure; only the *existence* of the
   protection is established.
2. **D_047 OP2 — lock read-out.** Whether the degeneracy lock has an observable physical
   reading (a stability/rigidity statement about D96's attractors) beyond the spectral-entropy
   accounting.

---

## Next Steps

- **D_047 → T_015 link.** T_015 found robustness controlled by the degeneracy *count* under
  single-edge perturbation; D_047 quantifies the same mechanism as a *divergent gain* in the
  weak-perturbation limit. A follow-up could unify the two into one lock-strength metric.
- **D_047 → NP series.** The lock is a candidate reading of the theory's own stability claims
  (locking from the spectral gap, Ch6); worth an audit against the organization/lock sector.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_047_Tests.cs`
**Run:** 2026-09-10 · **Result:** 7/7 PASSED

| Test | Verifies | Result |
|---|---|---|
| `Y_D_047_DegeneracyStructure` | reservoir table | ✅ |
| `Y_D_047_AttractorCountGain` | step saturation / $1/\varepsilon$ | ✅ |
| `Y_D_047_DegeneracyLockEntropy` | closed form | ✅ |
| `Y_D_047_RandomNullResponse` | exact null | ✅ |
| `Y_D_047_SpectralGainFinite` | finite $\mathcal{G}_\lambda$ | ✅ |
| `Y_D_047_CubeAmplificationAndCeiling` | D96³ peak + ceiling | ✅ |
| `Y_D_047_Run` | research report | ✅ |

**Conclusion:** YES — D96 degeneracy splitting amplifies weak perturbations **divergently**
($\mathcal{G} \propto 1/\varepsilon$) on structural observables, releasing exactly the
closed-form locked entropy $\frac{1}{N}\sum_{m_i>1}m_i\ln m_i = 0.802314$ nats for D96
($4.506441$ for $K_{96}$) independently of the perturbation amplitude. Random is an exact null.
The continuous spectrum is **not** amplified. D96³ is the hardest amplifier, capped at 15.7 %
of its headroom by factor-permutation symmetry. No canonical changes; research only.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_047"`

---

## References

- ResearchY-D_028 (span), D_040 (classification registry), D_042 (π-analogue).
- ResearchY-T_015 (spectral robustness — the degeneracy mechanism under finite perturbation).
- ResearchY-NP_036 (D96⊗3 tensor product), NP_037 (role of three).
- V2.0 Monograph Ch6 (D96 spectrum, multiplicity structure $[42\times2,5,6]$).
- AT-QG: QG155 (Z2 doublet), QG157 (effective access counts).
