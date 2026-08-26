# TRM Legacy Final Classification Audit

**Goal:** assign every TRM legacy module **exactly one** final disposition class and
produce the migration summary for `AT_Encyclopedia.md`.
**Inputs:** all TRM reconciliation/viability/mapping audits in `Docs/Audits/`.
**Discipline:** no new physics, no speculation — synthesis of accepted audits only.

---

## 1. Classification scheme

| Class | Meaning |
|---|---|
| **Absorbed** | content already present in AT, or equivalent to established physics AT already accepts (GR). No migration action. |
| **Rejected** | falsified/contradicted by a later audit, or non-viable (strictly worse than AT's own machinery). Discard. |
| **Candidate Mathematics** | genuinely new mathematics with **no observable mapping** yet. Keep as a math-level TODO. |
| **Candidate Physics** | genuinely new mathematics **with concrete testable observable predictions**. High-value TODO. |
| **Open** | undetermined — depends on information/results not yet present in the repository. |

---

## 2. Master table

| Module | Classification | Reason | Future Value |
|---|---|---|---|
| Time Field | **Absorbed** | $T(x,t)$, $\vec a=c^2\nabla T\to$Newton ≡ phase-gradient gravity (QG-022); $G=\ell^2c^3/\hbar$ (QG-007). Only the "time-rate" *wording* is rejected (QG-080–089). | None — already derived |
| Temporal Drift | **Rejected** | $\beta_T$-expansion damping is a tired-light descendant; contradicted by QG-080–089; replaced by FLRW + $w(z)=-1+0.015(1+z)^{3/2}$ (DATA-001/002). Own fit $\sigma\approx0.656$ ≫ ΛCDM $\sim0.13$. | None |
| RAR | **Absorbed** | TRM $a_0$ ≡ AT $g_\dagger=cH_0/(2\pi)\approx1.05\times10^{-10}$; BTFR/flat curves derived with **zero** free parameters (QG-084–086, DATA-003+). | None — already derived |
| Quantum Engine | **Rejected** | $D(x)=1/(1+x+bx^2+x^4)$, $e^{-p^2/\Lambda^2}$, Padé kernel solve **no** AT problem: BDG layer operator already UV-finite/causal/unitary/stable; Gaussian cutoff is **non-unitary**, Padé causality-ambiguous, adds 2 fitted params. | None (only the "UV regularization" *job* overlaps the lattice) |
| m=3 Closure | **Candidate Mathematics** | $\Omega=(q+3)/q$, $\gamma=1/\Omega$ genuinely new math (rational mode-locking); independent predictions $\Omega\approx1.16$–$1.19$, $\gamma\approx0.84$–$0.86$ **unmapped** to observables; targets the Phase-151 $N\le3$ gap but is a *path*, not a theorem. | **High** — the central "why 3" mystery |
| Frame Dragging | **Absorbed** | $\vec A_T,\ \vec B_T=\nabla\times\vec A_T$ ≡ GR gravitomagnetism / Lense–Thirring (already measured: GP-B, LAGEOS). No new observable; sole novel claim ($k_T$ "derived non-fitted") unverified. | None — re-labeling of GR |
| Memory Channel | **Candidate Mathematics** | $\phi^2\lvert\dot\mu\rvert$ invariant genuinely new; **no observable, no fitted params, untestable**; "memory" = homonym of AT-130. | Low — untestable as stated |
| Theta Chain | **Candidate Mathematics** | $\Theta\to O_5\to\lambda_\Theta\to g_{\rm obs}$ genuinely new math; **homonym** with AT-128–133 information layer; $g_{\rm obs}$ unspecified, testability low. | Low |
| Unified Action | **Open** | $S_{\rm eff}[T,\vec A_T,\Theta]=S_T+S_A+S_\Theta+S_{\rm int}$ is a **roadmap** (UF01–09), not a result; depends on every other module being settled first. | Latent — capstone only after the above |

---

## 3. Count summary

| Class | Count | Modules |
|---|---|---|
| Absorbed | 3 | Time Field, RAR, Frame Dragging |
| Rejected | 2 | Temporal Drift, Quantum Engine |
| Candidate Mathematics | 3 | m=3 Closure, Memory Channel, Theta Chain |
| Candidate Physics | **0** | *(none)* |
| Open | 1 | Unified Action |

---

## 4. Verdict

**The survival core of TRM is already inside AT.** Time Field and RAR are absorbed
(phase-gradient gravity + $g_\dagger=cH_0/2\pi$); Frame Dragging is GR re-labeled, not new.
Temporal Drift and the Quantum Engine are rejected (falsified / non-viable).

**TRM contributes no Candidate Physics** — no module carries a genuinely new *testable*
observable. Its genuine novelty is **three mathematics-level candidates** (m=3 Closure,
Memory Channel, Theta Chain), all currently **unmapped to observables**, plus one
**Open** roadmap (Unified Action) that presupposes the others.

The single highest-value residual is **m=3 Closure**, which targets the exact $N\le3$
gap Phase 151 left open — but only as a *path*, not a theorem, and only after
$\Omega,\gamma$ are assigned a physical meaning. **Nothing here reverses any prior
AT result.** Consistent with the TRM documents' own claim boundaries
("tested-effective", "not theorem-level"), TRM's legacy reduces to: 3 absorbed,
2 rejected, 3 candidate-mathematics, 0 candidate-physics, 1 open.
