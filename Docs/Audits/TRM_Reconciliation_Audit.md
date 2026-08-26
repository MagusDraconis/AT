# TRM Reconciliation Audit

**Scope:** reconcile the historical TRM documents (Clockwork Cosmology V1, TRM V2.2,
TRM V3.0 baseline / memory-mode-locking / theta-vector-unified-action) against the
current AT framework (`AT_Master_Reference.md`, `AT_Encyclopedia.md`, Phase 1–158).

**Discipline:** no new physics; no assumed equivalence; explicit mapping required.
Categories: **Equivalent** (same math, different wording), **Integrated** (already
derived in AT), **Missing** (new mathematics absent from AT), **Rejected**
(contradicted by a later audit).

---

## 1. Master Classification

| TRM concept | Status | AT replacement | Migration? |
|---|---|---|---|
| Time Field T (scalar $T(x,t)$, gradient→acceleration) | Equivalent | Phase field $\theta$; phase-gradient gravity (QG-022) | N (rename $T\to\theta$) |
| Time Field T ("time-rate" interpretation) | Rejected | Phase, not time-rate (QG-080–089 closed time-first) | N |
| Temporal Drift ($\beta_T$ expansion damping) | Rejected | FLRW + $w(z)=-1+0.015(1+z)^{3/2}$ (DATA-001/002) | N |
| TRM Gravity ($\vec a = c^2\nabla T \to$ Newton) | Equivalent | $G=\ell^2 c^3/\hbar$ + phase gradient (QG-007, QG-022) | N |
| TRM RAR ($a_0$, BTFR, flat curves) | Integrated | $g_\dagger = cH_0/(2\pi)$; $a_0\sim cH$ (QG-084–086, DATA-003+) | N |
| Theta ($\Theta\to O_5\to\lambda_\Theta\to g_{\rm obs}$) | Missing | — (homonym only: AT-128–133 information layer) | Y (TODO) |
| Memory Channel ($\phi^2|\dot\mu|$ invariant) | Missing | — (≠ AT-130 information memory) | Y (TODO) |
| m=3 Closure (rational mode-locking $\Omega=\tfrac{q+3}{q}$) | Missing | $N=3$ multiplicity (Phase 150–151) — *different mechanism* | Y (TODO) |
| Frame Dragging ($\vec A_T,\ \vec B_T=\nabla\times\vec A_T$) | Missing | — | Y (TODO) |
| Unified Action ($S_{\rm eff}[T,\vec A_T,\Theta]$) | Missing | — | Y (TODO) |
| TRM Cosmology — CMB / cluster sub-models | Missing | — (AT CMB & clusters = TODO) | Y (TODO) |
| TRM Cosmology — expansion-as-drift mechanism | Rejected | FLRW (QG-080–089, DATA-001/002) | N |

---

## 2. Detailed Mapping

### 2.1 Time Field T

- **TRM:** scalar time-rate $T(x,t)$; field equation $T = 1 - GM/(c_0^2 r)$;
  $\vec a = c_0^2\,\vec\nabla T$; Newtonian convergence proven.
- **AT:** gravity = phase-gradient phenomenon (QG-022, classification C STRONG
  EMERGENCE); $G=\ell^2c^3/\hbar$ (QG-007). The *mathematics* — acceleration from the
  gradient of a scalar field, recovering Newton — is **equivalent**. The *interpretation*
  differs: AT's scalar is oscillation **phase** $\theta$, not a "time-rate".
- **Verdict:** **Equivalent** (math) / **Rejected** (time-rate wording). QG-080–089
  audited every time-first/clock/event/rate reinterpretation and found each collapses to
  FLRW or is falsified; only the phase interpretation survives.

### 2.2 Temporal Drift ($\beta_T$)

- **TRM:** $D_L(z)=\tfrac{c}{H_T}z(1+z)e^{\beta_T z}$, $\beta_T\approx-0.284$; expansion
  reinterpreted as cumulative temporal damping (a tired-light descendant).
- **AT:** expansion = interpretation of FLRW (QG-081); the rate/event/structure
  reinterpretation program was **closed** (QG-089 "rate-first is tautological").
  Pantheon+ fits use standard FLRW + $w(z)$ (DATA-001/002).
- **Verdict:** **Rejected.** The drift-damping mechanism is contradicted by QG-080–089;
  replaced by FLRW with evolving $w(z)$. (TRM's own fit $\sigma\approx0.656$ is far
  worse than $\Lambda$CDM's $\sim0.13$.)

### 2.3 TRM Gravity

- **TRM:** $\vec a=c^2\nabla T$; rotation curves via $g_{\rm eff}=g_{\rm bar}+\sqrt{g_{\rm bar}a_0}$;
  $V^4=GM a_0$ (BTFR); fit $a_0\approx1.02\times10^{-10}\ \mathrm{m/s^2}$.
- **AT:** the gradient→Newton step is already derived (QG-022, QG-007). The RAR is
  already derived with **zero free parameters**: $g_\dagger = cH_0/(2\pi) \approx
  1.05\times10^{-10}\ \mathrm{m/s^2}$. TRM's $a_0$ and AT's $g_\dagger$ are **the same
  number** (both $\approx cH$); AT explains it as an emergent cosmological scale
  (QG-086, Phase 143), and QG-084/085/097 audit the $2\pi$.
- **Verdict:** **Equivalent** (gradient→Newton) + **Integrated** (RAR/BTFR/a₀). No
  migration needed; TRM's "single scalar field" ambition is realized in AT as
  phase-gradient gravity.

### 2.4 Theta ($\Theta\to O_5\to\lambda_\Theta\to g_{\rm obs}$)

- **TRM:** nonlocal observable chain, guarded by TO01–28 / TQK01–04 / LC01–08 / TOL01–04.
- **AT:** the only "Theta" is the **information layer** in the proto-matter program
  (AT-128–133) — an autonomous information field, *not* an observable chain to $g_{\rm obs}$.
  No $\Theta\to O_5\to\lambda_\Theta$ structure exists in the gauge/physics program
  (QG-038, Phase 149 contain no theta sector).
- **Verdict:** **Missing** (new mathematics). The homonym "Theta" must not be confused
  with AT's information-layer Θ.

### 2.5 Memory Channel ($\phi^2|\dot\mu|$)

- **TRM:** effective invariant $I_{\rm micro}\propto\phi^2|\dot\mu|$, via
  $A_{\rm dyn}\propto\phi\to A^2_{\rm dyn}|\dot\mu|\to\phi^2|\dot\mu|$ (MC09–12).
- **AT:** no transport invariant of this form. AT-130 ("information memory: signals
  persist") is a *different* concept (persistence in the Θ layer, not a $\phi^2|\dot\mu|$
  action invariant).
- **Verdict:** **Missing.** No equivalent mathematics.

### 2.6 m=3 Closure

- **TRM:** rational mode-locking $\Omega=\tfrac{q+3}{q}$, $\gamma=1/\Omega$,
  $\Omega\approx1.16{-}1.19$, $\gamma\approx0.84{-}0.86$; closure order $m=3$ (RBF16–23).
- **AT:** the "recurring integer 3" is fully resolved but by a *different* route —
  multiplicity: $N\ge3$ DERIVED (CP lower bound), $N\le3$ DRAWN (empirical),
  spatial 3 DERIVED (complexity) (Phases 150–151). AT has **no** mode-locking /
  rational-band mechanism.
- **Verdict:** **Missing.** The $m=3$ *mode-locking* mathematics is absent; AT's
  $N=3$ multiplicity does **not** subsume it. Superficial thematic overlap only.

### 2.7 Frame Dragging

- **TRM:** vector sector $\vec A_T,\ \vec B_T=\nabla\times\vec A_T$; weak-field
  Lense–Thirring-like candidate (FD01–20), effective coupling $k_T$.
- **AT:** gravity is a scalar phase-gradient (QG-022); there is **no** vector /
  frame-dragging sector. (QG-035 "winding sign & gravity coupling" concerns defect
  winding signs, not Lense–Thirring.)
- **Verdict:** **Missing.** New mathematics.

### 2.8 Unified Action

- **TRM:** candidate roadmap
  $S_{\rm eff}[T,\vec A_T,\Theta]=S_T+S_A+S_\Theta+S_{\rm int}$ (UF01–09).
- **AT:** no unified effective action of this form. (`UnifiedATAnalyzer`/`Framework`
  in `Research/` concern the proto-matter AT correspondence, not $S_{\rm eff}[T,A,\Theta]$.)
- **Verdict:** **Missing.** New mathematics.

### 2.9 TRM Cosmology

- **TRM:** CMB peaks via "temporal phase coherence" ($\ell_1\approx220$, $\ell_2\approx540$);
  clusters via pressure-gradient threshold $\Xi_{\rm crit}\approx6.0\times10^{-34}\ \mathrm{dyn/cm^3}$
  (ACCEPT, accuracy ~62.7%); Pantheon+ via drift $\beta_T$ ($\sigma\approx0.656$).
- **AT:** **no** CMB analysis and **no** cluster audit exist (both marked TODO in
  `Coverage_Report.md`/`AT_Encyclopedia.md`). The drift-expansion mechanism itself is
  **Rejected** (QG-080–089, DATA-001/002).
- **Verdict:** sub-models **Missing**; mechanism **Rejected**. The cluster/CMB regimes
  are genuinely unpopulated in AT — but TRM's own fits there are weak or unvalidated,
  so nothing transfers.

---

## 3. Bottom Line

| Count | Category | Concepts |
|---|---|---|
| 2 | Equivalent | Time Field T (math), TRM Gravity (∇→Newton) |
| 1 | Integrated | TRM RAR ($a_0\equiv g_\dagger$) |
| 5 | Missing | Theta chain, Memory Channel, m=3 Closure, Frame Dragging, Unified Action |
| 3 | Rejected | time-rate interpretation, Temporal Drift, expansion-as-drift |

**The survival core of TRM is its gravity/RAR content**, which AT already re-derives
(phase-gradient gravity + $g_\dagger=cH_0/2\pi$). TRM's **scalar-transport / memory /
mode-locking / theta-chain / vector / unified-action** machinery is genuinely novel
mathematics with **no AT counterpart** — it is **Missing**, not equivalent.

The TRM V3.0 documents themselves correctly disclaim theorem-level closure ("not GR
replacement", "tested-effective weak-field"). This is consistent with AT's hostile-audit
discipline: those five Missing sectors remain **TODO**, not results.

**No physics is invented, and no equivalence is assumed:** every row above is traced to a
specific TRM equation and a specific AT phase/result.
