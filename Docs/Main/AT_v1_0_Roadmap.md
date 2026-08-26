# AT v1.0 Roadmap Audit

**Goal:** determine the **shortest path** from AT v0.9 to v1.0.
**Inputs:** `AT_Master_Reference.md`, `AT_Encyclopedia.md`, `AT_Completeness_Report.md`,
`Coverage_Report.md`, `AT_VersionReadiness.md`.
**Discipline:** planning only — no new physics, no derivations.

---

## 1. Open items — assessment

| # | Item | Scientific value | Completion effort | Dependency count | v1.0 necessity |
|---|---|---|---|---|---|
| 1 | **Internal-3 Node** (gauge count $n=3$ + $N\le3$) | **High** — the central "why 3" residual | Low (formal closure as contingent) / High (derivation) | 0 (self-contained) | **Yes** — must be formally closed (contingent) or derived |
| 2 | **Shared Cascade** (3-class independence) | Medium — one vs three cascade mechanisms | Low (formal closure as untestable, T-12) | 0 (independent) | **Yes** — must be formally closed as untestable |
| 3 | **CMB Boltzmann Solver** | Medium — cosmology completeness (Chapter VIII 45%→100%) | Medium (engineering, CAMB-class) | 0 (independent) | **No** — computational, not a theory gap; may remain documented PARTIAL |
| 4 | **Unified Action** | Low — TRM capstone roadmap, not a AT result | Trivial (formal demotion) | 3 (needs $T$, $\vec A_T$, $\Theta$ first) | **Yes** — must be formally demoted to "roadmap" (removes the last OPEN chapter) |

---

## 2. Priority table

| Item | Required for v1.0? | Difficulty | Impact | Priority |
|---|---|---|---|---|
| Internal-3 Node (formal closure) | **Yes** | Low (doc) | High | **1** |
| Unified Action (demotion) | **Yes** | Trivial | Low (removes OPEN chapter) | **2** |
| Shared Cascade (formal closure) | **Yes** | Trivial | Medium | **3** |
| CMB Boltzmann Solver | No (theory) / Yes (100% encyclopedia) | Medium (engineering) | Medium | **4** |

Ranking logic: v1.0 necessity × (1 / effort). The three documentation closures are all
low-effort and required; the CMB solver is high-effort and *not* required for a theory-v1.0.

---

## 3. The shortest path (Minimal v1.0)

Three documentation-level dispositions (no new physics) unlock v1.0:

1. **Close the Internal-3 Node as contingent** — declare, under the accepted primitives
   $\{Q,\ \text{Random Actualization},\ (\ell,\tau,\hbar),\ M^2\}$ with **no new primitives**,
   the internal multiplicity/count saturating at 3 is **contingent**: $N\le3$ empirical
   (T-10, 0.70) and $n=3$ underived (T-09). *Caveat:* the gauge-count no-go is **weak**
   (0.10) with graph-spectrum/lattice-mode untested, so this closure is **provisional**,
   unlike the Koide closure (T-08, 0.70).
2. **Demote Unified Action** — mark $S_{\rm eff}[T,\vec A_T,\Theta]$ as a **TRM roadmap,
   not a AT result** (depends on the vector/theta sectors). This removes the last OPEN
   chapter (Part VII).
3. **Close Shared Cascade as untestable** — formalize T-12 (one universe cannot
   discriminate one cascade from three without a new primitive). This removes the last
   no-go residue.
4. **Accept CMB as a documented PARTIAL** — computational scope (acoustic phase shift +
   finite decoupling + ISW), not a theory gap.

**Result:** all 10 chapters dispositioned (0 OPEN), all open items closed/demoted,
encyclopedia ~81% (CMB partial). Theory completeness = "derivable structure 100% + content
100% classified."

---

## 4. Recommendations

### 4.1 Minimal v1.0 (shortest — documentation only, ~days)

- Close Internal-3 Node as contingent (provisional), demote Unified Action, close Shared
  Cascade, accept CMB as documented PARTIAL.
- **Delivers:** a self-consistent v1.0 with 0 open chapters and 0 unresolved theory items
  *under the no-new-primitives constraint*.
- **Weak spot:** the Internal-3 gauge-count closure rests on T-09 = 0.10 (weak no-go);
  two routes (graph-spectrum, lattice-mode) remain untested.

### 4.2 Conservative v1.0 (add computational completion, ~weeks–months)

- Minimal v1.0 **+ complete the CMB Boltzmann solver** (acoustic phase shift $\phi\approx0.84$
  rad + finite-decoupling velocity phase + ISW).
- **Delivers:** full encyclopedia (Chapter VIII 45%→100%; ~81%→~100% populated), removing
  the only remaining PARTIAL-as-computational item.
- **Still open:** the Internal-3 node (formally closed as contingent, but not *derived*).

### 4.3 Full v1.0 (add genuine new mathematics — open-ended)

- Conservative v1.0 **+ resolve the Internal-3 node**: either derive the defect count
  $n=3$ (promote TRM m=3 closure from path to theorem, or exhaust graph-spectrum /
  lattice-mode routes and strengthen T-09 to ≥0.70), or find a new closure-order mechanism.
- **Delivers:** a v1.0 in which the central "why 3" is either *derived* or *definitively*
  no-go (not merely provisionally contingent).
- **Effort:** high / open-ended (new mathematics, not documentation).

---

## 5. Verdict

The **shortest path to v1.0 is the Minimal path** — three documentation dispositions plus
accepting CMB as a documented computational PARTIAL. It requires **no new physics** and can
be completed in days. Its single caveat is that the Internal-3 node's gauge-count face is
closed *provisionally* (T-09 = 0.10), not definitively.

**Recommendation: adopt Minimal v1.0 now, then iterate toward Conservative (CMB) and Full
(Internal-3 resolution) as resources permit.** The theory's structure/content program is
already complete; v1.0 is a documentation-closure milestone, not a new-derivation milestone.

| Path | Effort | Encyclopedia | Internal-3 node | Verdict |
|---|---|---|---|---|
| **Minimal v1.0** | ~days (doc) | ~81% | closed provisionally (contingent) | **recommended now** |
| Conservative v1.0 | + weeks–months (code) | ~100% | closed provisionally | next |
| Full v1.0 | + open-ended (math) | ~100% | derived or definitively no-go | long-term |
