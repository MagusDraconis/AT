# TRM Quantum Engine Reconciliation Audit

**Goal:** reconcile the legacy TRM "Quantum Engine" formulas against AT's QG /
causal-set / graph-Laplacian / lattice programs and mathematical foundation.
**Note:** the Quantum Engine formulas are **not** present in the five legacy PDFs
(`LegacyDocs/`); they are supplied here as input. They are therefore treated as a
proposed external module and compared only against AT repository content.
**Discipline:** no new physics, no new derivations — reconciliation only.

---

## 1. Master table

| Concept | Status | AT replacement | Evidence |
|---|---|---|---|
| $D(x)=\dfrac{1}{1+x+bx^2+x^4}$ | **New Mathematics** | none (Laplacian inverse is a different kernel) | no such propagator anywhere in repo |
| UV damping $e^{-p^2/\Lambda^2}$ | **Integrated (concept) / New (form)** | graph-Laplacian **lattice** = natural UV cutoff (discrete spectrum) | `Docs/Theory/04_Q_Networks_and_Laplacian.md` (finite $N$, $\Delta x$); `BdgUniquenessAnalyzer.cs` "1/τ² kernel diverges at $y\to x$ (UV divergence). Requires regularization" |
| Padé kernel | **New Mathematics** | none | "Padé"/"Pade" absent from repo |
| Loop finiteness | **New Mathematics** | none (no loop/renormalization program) | QG derives gravity as phase-gradient (QG-022); no loop calculation exists |
| *(none)* | **Contradicted** | — | nothing conflicts; AT simply has no Quantum-Engine sector |

---

## 2. The four questions

### 2.1 Does AT already contain UV regularization?

**Partially.** AT's **lattice / graph-Laplacian** program provides UV regularization
by *discreteness*: the graph Laplacian has a finite spectrum ($N$ modes, eigenvalues
$\lambda_k=-(1/\Delta x^2)[2-2\cos(\pi k/(N{+}1))]-\gamma$, bounded above by
$\sim-4/\Delta x^2$), so high momenta are cut off by the lattice spacing $\Delta x$ —
**not** by a continuum momentum cutoff. The specific Gaussian form $e^{-p^2/\Lambda^2}$
is **absent**, and AT's own nonlocal gravity kernel explicitly still has a UV
divergence "requiring regularization". So: *concept present (lattice), form absent.*

### 2.2 Does AT already imply loop finiteness?

**No.** AT has no loop expansion, no renormalization, and no finiteness claim. The
discrete lattice would *make* loops finite, but AT never computes loops.

### 2.3 Is the Padé kernel derivable?

**Not from AT as it stands.** AT's propagators come from the graph Laplacian /
continuum Laplacian (a $1/k^2$-type or tight-binding kernel), not a rational
$D(x)=1/(1+x+bx^2+x^4)$. No Padé structure appears anywhere in the repository.

### 2.4 Are $\Lambda$ and $b$ fundamental or fitted?

**Fitted (phenomenological), on present evidence.** The TRM documents' own claim
boundary is "tested-effective, not theorem-level first-principles", and every TRM
parameter so far extracted ($a_0$, $\beta_T\approx-0.284$) is fitted to data, not
derived. AT's only comparable fundamental scale is the lattice spacing / fundamental
clock $\omega_0=2\pi/\tau=1.17\times10^{44}$ Hz; AT contains no $\Lambda$ or $b$.

---

## 3. Where the overlap genuinely is

| TRM Quantum Engine | AT locus | Relation |
|---|---|---|
| UV regularization $e^{-p^2/\Lambda^2}$ | graph Laplacian lattice ($\Delta x$ cutoff) | same *job* (tame UV), different *mechanism* (discrete vs Gaussian) |
| Padé kernel $D(x)$ | graph-Laplacian propagator | different kernel; no correspondence |
| loop finiteness | — (no loop program) | AT silent |

---

## 4. Conclusion

The TRM Quantum Engine is **new mathematics relative to AT**: none of its four
ingredients ($D(x)$, the Gaussian UV damping, the Padé kernel, loop finiteness) is
present, and nothing is contradicted. The single point of genuine contact is
**UV regularization**, where AT's lattice program already does the same job by a
*different mechanism* (discrete spectrum rather than a momentum cutoff). The Padé
kernel and loop-finiteness claims are unmapped and underived in AT, and $\Lambda,b$
are, on the available evidence, fitted rather than fundamental. The Quantum Engine
therefore remains a **Missing (TODO)** external candidate — not equivalent, not
integrated, not contradicted.
