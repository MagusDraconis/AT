# Quantum Engine Viability Audit

**Goal:** determine whether the TRM Quantum Engine solves an existing AT UV problem.
**Scope:** no new physics, no integration — viability comparison only.
**Inputs:** $D(x)=1/(1+x+bx^2+x^4)$, $e^{-p^2/\Lambda^2}$, Padé kernel, vs AT's
graph-Laplacian / layer (BDG) regulator.

---

## 1. AT UV divergences located

| # | Issue | Location | Status in AT |
|---|---|---|---|
| 1 | $1/\tau^2$ kernel diverges at $y\to x$ (UV) | `BdgUniquenessAnalyzer.cs` (O4) | **REJECTED** ("non-local + UV divergent") |
| 2 | non-local smearing kernel $K(\tau)$ over the past | `BdgUniquenessAnalyzer.cs` (O2) | **REJECTED** (violates discrete locality) |
| 3 | continuum limit $N\to\infty,\ \Delta x\to0$: eigenvalues $\to-\infty$ | `04_Q_Networks_and_Laplacian.md` | **not physical** — lattice stays finite ($\Delta x>0$) |
| 4 | $V(\varphi)=-|\lambda|\varphi^4$ unbounded below | `AT_X060_NeutrinoOrdering` | noted (vacuum instability, not a loop UV divergence) |

**Key fact:** AT's *accepted* operator is the **BDG layer operator** (finite-difference
binomial coefficients $(+1,-4,+6,-4,+1)$ over $d{+}1$ layers), which is **UV-finite by
construction**. The UV divergences (1, 2) live only in the *rejected* non-local
alternatives. AT has **no loop expansion**, so there are no loop divergences to remove.

---

## 2. Regulator comparison

| Property | AT lattice / BDG (discrete) | Quantum Engine ($e^{-p^2/\Lambda^2}$, Padé $D(x)$) |
|---|---|---|
| **finite?** | ✅ finite spectrum ($\lambda\gtrsim-4/\Delta x^2$) | ✅ Gaussian suppresses high-$p$; $D(x)\sim1/x^4$ |
| **causal?** | ✅ sign-alternating layers → d'Alembertian (Lorentzian) | ⚠️ $D(x)$ has complex poles (acausal risk); Gaussian is entire (causal) |
| **unitary?** | ✅ wave operator | ❌ Gaussian cutoff is non-unitary; Padé unitarity unestablished |
| **stable?** | ✅ wave operator | ✅ $D(x)>0$ ($b>0$), Gaussian $>0$ |

The lattice regulator is **finite + causal + unitary + stable** in one mechanism
(discreteness). The Quantum Engine is finite and stable but **fails unitarity** (Gaussian
cutoff) and is **ambiguous on causality** (Padé poles).

---

## 3. Fitted vs derivable parameters

| Parameter | AT | Quantum Engine |
|---|---|---|
| $\Delta x$ (lattice spacing) | **structural** (graph scale; tied to $\omega_0=2\pi/\tau=1.17\times10^{44}$ Hz) | — |
| $\gamma$ (damping) | **fitted** ("phenomenological, not derived from Q") | — |
| $\Lambda$ (UV scale) | — | **fitted** (no derivation given) |
| $b$ (Padé coefficient) | — | **fitted** (no derivation given) |

AT has one fitted damping parameter ($\gamma$); the Quantum Engine has two fitted
parameters ($\Lambda, b$).

---

## 4. Issue-by-issue improvement table

| Issue | Current AT | Quantum Engine | Improvement? |
|---|---|---|---|
| $1/\tau^2$ UV divergence | **rejected** (non-local) | would replace it, but non-unitary | **No** (AT already rejects the divergent form) |
| non-local kernel | **rejected** (locality) | $D(x)$ is still a continuum kernel | **No** (does not restore locality) |
| continuum-limit divergence | **finite lattice** (no divergence) | Gaussian cutoff (unnecessary) | **No** (already finite) |
| loop divergences | **none** (no loop program) | claims finiteness | **n/a** (no AT loops to fix) |
| unitarity | wave operator (unitary) | Gaussian cutoff (non-unitary) | **Worse** |

---

## 5. Conclusion

The Quantum Engine **does not solve an existing AT UV problem**, because AT has no
live UV problem to solve: its accepted layer/BDG operator is already UV-finite, causal,
unitary and stable, and the only UV divergences occur in operators AT has already
**rejected**. Relative to AT's lattice regulator, the Quantum Engine is **strictly worse
on unitarity** (non-unitary Gaussian cutoff) and **ambiguous on causality** (Padé poles),
while introducing two extra *fitted* parameters ($\Lambda, b$). It therefore offers no
viability advantage and is **not recommended** for integration.
