# ResearchY-G_081 — Connectivity Dependency Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_081_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ConnectivityDependencyAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_081`) · **Verdict:** **GLOBAL INPUT** (registry: Boundary)

---

## Question

G_079 proved the continuity law for the surviving update class and found that its equivalence with count conservation
needs the substrate's connectivity. G_080 then classified that connectivity as **ASSUMED**, exhibited two frozen
disconnected universes and showed that **0 of 4** allowed primitives detect a cut.

The quantitative question follows, and it is asked **adversarially**:

> **How much of the surviving gravity/time sector actually depends on connectivity?**

The goal is **not to save connectivity**. It is to measure exactly how much of the surviving theory collapses without
it. **A small failure core strengthens AT; a large one promotes connectivity to a fundamental assumption.**

**Allowed:** Difference, Actualization, the generation relation, count conservation, connectivity as an **explicit
input parameter**.
**Forbidden:** deriving connectivity, adding primitives, importing topology, repairing disconnected sectors.

---

## Answer

**GLOBAL INPUT — SMALL FAILURE CORE, LOCALISED ENTIRELY IN THE TRANSPORT LAYER.**

| Task | Result |
|---|---|
| 1 Dependency map | **6 DIRECT · 2 INDIRECT · 21 INDEPENDENT** of 29 surviving claims |
| 2 Counterfactual universe | transport capacity `n − c`; every local read invariant; the cut silences exactly the cross-channel |
| 3 Minimal failure set | **6 claims, all transport-layer**, triggered by **2 link removals** at `c = 2` |
| 4 Connectivity load | **4 of 29** rank-loaded (**0.138**) against 6 direct (**0.207**) — rank-**decorated**, not rank-loaded |
| 5 Hidden-use scan | **105** assumption sites in **4 of the 14** surviving sources, 19 undeclared |
| 6 Structural classification | **GLOBAL INPUT**, three alternatives refuted by a number each |
| 7 Referee answer | **23 of 29** claims (**0.793**) survive unchanged |

---

## 1. The counterfactual universe (task 2)

The same cells, split into `c = 1, 2, 4, 8` closed rings. On eight cells:

| components | cells | per component | transport directions | nullity | free modes | witnesses | transports | **not transports** |
|---|---|---|---|---|---|---|---|---|
| 1 | 8 | 8 | **7** | 1 | 0 | 8 | 8 | **0** |
| 2 | 8 | 4 | **6** | 2 | 1 | 9 | 8 | **1** |
| 4 | 8 | 2 | **4** | 4 | 3 | 11 | 8 | **3** |
| 8 | 8 | 1 | **0** | 8 | 7 | 7 | **0** | **7** |

The rank identity, the circulation and the free-mode count are three readings of one fact, and the continuity law's
failure count is exactly `c − 1`. The limit case is measured too: when every component is a **single cell** no
transport exists at all — **G_080's frozen pair, generalised**.

The read side, on the canonical 96 cells:

| components | per component | max gradient **inside** | cells reading one | gradient **across** the cut | links across the cut | path across the cut | total change of a cross-transfer | per-component residual |
|---|---|---|---|---|---|---|---|---|
| 1 | 96 | 4.993E-001 | 96 | 0 | 0 | 48 | 0 | 0 |
| 2 | 48 | 4.993E-001 | 96 | **0.000E+000** | **0** | **−1** | **0.000E+000** | **1** |
| 4 | 24 | 4.993E-001 | 96 | 0.000E+000 | 0 | −1 | 0.000E+000 | 1 |
| 8 | 12 | 4.993E-001 | 96 | 0.000E+000 | 0 | −1 | 0.000E+000 | 1 |

So a cut **does not touch the gradient inside a component** and **destroys the gradient across it**; it leaves the
total conserved and makes each component's ledger move alone.

## 2. The dependency map (task 1)

Every surviving claim of **G_035** and **G_068–G_080** is re-evaluated at `c = 1, 2, 4, 8` **through the owning audit's
own API**, and classified by a mechanical rule: a quantity that **moves** with `c` is DIRECT; a path-dependent quantity
that is unchanged while its path disappears is INDIRECT; a local quantity is INDEPENDENT; a global quantity that does not
move is INDEPENDENT.

| claim | audit | domain | c=1 | c=2 | c=4 | c=8 | dependence |
|---|---|---|---|---|---|---|---|
| the redshift is a ratio of clock rates, `1 + z = W(A_s)/W(A_o)` | G_077 | Local | 1.280E+000 | = | = | = | **Independent** |
| the clock rate reads 9 of 9 directions | G_077 | Local | 9 | = | = | = | **Independent** |
| the acceleration reads 8 of 9, blind to the uniform one | G_077 | Local | 1 | = | = | = | **Independent** |
| the acceleration is a gradient along a link | G_078 | Local | 3.719E-002 | = | = | = | **Independent** |
| the uplift cap is 1.5249 | G_078 | Local | 1.525E+000 | = | = | = | **Independent** |
| the rate-ratio cap is 4.5947 | G_078 | Local | 4.595E+000 | = | = | = | **Independent** |
| the uplift is logarithmic: +1.0 per e³ | G_078 | Local | 1.000E+000 | = | = | = | **Independent** |
| the uniform direction moves every clock by 6.623E-003 | G_078 | Local | 6.623E-003 | = | = | = | **Independent** |
| the AT-vs-GR separation at the target is 1.256E-001 | G_075 | Local | 1.256E-001 | = | = | = | **Independent** |
| 9 of 9 laws carry the pinned slope | G_075 | Local | 9 | = | = | = | **Independent** |
| the maximal family's target lower bound is 1.142288 | G_075 | Local | 1.142E+000 | = | = | = | **Independent** |
| the composition law leaves `exp(k x)` with a free constant | G_076 | Local | 2.828E+000 | = | = | = | **Independent** |
| the second-order ratio AT/GR | G_068 | Local | 1.098E+000 | = | = | = | **Independent** |
| the decision significance of the recorded capability | G_072 | Local | 3.840E+000 | = | = | = | **Independent** |
| no temporal observable takes `B` | G_035 | Local | 1 | = | = | = | **Independent** |
| **the divergence's rank is `n − c`** | G_079/G_080 | Global | **95** | **94** | **92** | **88** | **Direct** |
| **the circulation's nullity is `c`** | G_079 | Global | **1** | **2** | **4** | **8** | **Direct** |
| **conserving changes that are NOT transports (`c − 1`)** | G_079 | Global | **0** | **1** | **3** | **7** | **Direct** |
| **the conserved total leaves `c − 1` free levels** | G_079 | Global | **0** | **1** | **3** | **7** | **Direct** |
| **a bounded region cannot change alone (the ledger)** | G_078 | Global | **0** | **1** | **1** | **1** | **Direct** |
| the component count is conserved: 0 operations change it | G_080 | Global | 0 | = | = | = | **Independent** |
| the total count is conserved | G_078 | Global | 0 | = | = | = | **Independent** |
| the update class is a divergence, residual 0 | G_079 | Global | 0 | = | = | = | **Independent** |
| 0 of 4 primitives detect the cut | G_080 | Global | 0 | = | = | = | **Independent** |
| the smallest cut is two links | G_080 | Global | 2 | = | = | = | **Independent** |
| a redshift between two cells is defined by their rate ratio | G_079/G_080 | PathDependent | 7.631E-001 | = | = | = | **Indirect** |
| **a comparison needs a channel: the path length is finite** | G_079 | PathDependent | **1** | **−1** | **−1** | **−1** | **Direct** |
| the compensating deficit exists somewhere | G_078 | PathDependent | 1 | = | = | = | **Indirect** |

**The ground of the INDEPENDENT rows is measured, not argued:** the set of distinct local reads — potential, clock
rate, gradient — is **one and the same set** on every component count (`LocalSectorInvariance`, 1 distinct set).

## 3. The minimal failure set (task 3)

**FailureCore(connectivity)** — 6 claims, in map order:

1. the divergence's rank is `n − c` (the transport capacity);
2. the circulation's nullity is `c`;
3. conserving changes that are not transports (`c − 1`);
4. the conserved total leaves `c − 1` free relative levels;
5. a bounded region cannot change alone (the ledger's conclusion for a component);
6. a comparison across the substrate needs a channel (the path length is finite).

**Trigger cost: `c = 2` suffices, and on the canonical ring the smallest disconnection is 2 links or 2 cells** — two
defects buy the whole core. Every member is owned by G_078–G_080: the core is **entirely** in the transport and
continuity layer.

**CoreWithoutConnectivity** — the other **23 of 29** claims, each of them a clock-law, potential, redshift,
acceleration, cap or measurement claim, plus count conservation, the divergence identity and the union-invariance.

## 4. The connectivity load (task 4)

The test is arithmetic rather than textual: a quantity is **rank-loaded** if it is **linear in `c`** on the component
counts actually evaluated, because `n − c` is exactly that.

| | count | fraction |
|---|---|---|
| rank-loaded (linear in `c`) | **4** | **0.138** |
| directly dependent (any movement) | **6** | **0.207** |
| independent | 21 | 0.724 |
| survive without connectivity | **23** | **0.793** |

The four rank-loaded claims are the rank, the nullity, the failure count and the free-mode count. **So the surviving
sector is not rank-*loaded*, it is rank-*decorated*: count conservation alone carries everything except the transport
layer.**

## 5. The hidden-connectivity scan (task 5)

A **live scan** of the sources: it re-reads the 14 surviving audits at test time, with **literals and comments
stripped** (`AtSourceScan`, so a multi-line report string cannot be mistaken for code), and it **excludes its own
source** (the G_027/G_033 rule).

| category | hits | files | undeclared | declared |
|---|---|---|---|---|
| RING | 18 | 3 | **9** | 9 |
| PATH | 41 | 3 | **2** | 39 |
| TRANSPORT | 33 | 3 | **8** | 25 |
| NEIGHBOUR | 7 | 1 | **0** | 7 |
| LAPLACIAN | 6 | 1 | **0** | 6 |

**105 assumption sites in 4 of the 14 sources, 19 of them undeclared.** The undeclared uses, reported in full by the
scan, are:

- **RING, all nine in `SourceManipulationAudit.cs`** (G_078): lines 105–107 — the three operators of the surviving
  class are written as **ring differences** (`r[i] - r[(i - 1 + n) % n]`, `0.5 * (r[(i + 1) % n] - r[(i - 1 + n) % n])`,
  the second difference); lines 140–165 — the region ledger and the cell ledger wrap with `% n`; line 279 — the
  acceleration read uses `n - 1 : 0`, i.e. `ring: true`;
- **TRANSPORT, all eight in the same file**, in the ledger and the divergence reconstruction;
- **PATH, two in `NeutronStarDecisionAudit.cs`** (G_069) — the decision map's own `Reachable()` vocabulary, not a
  substrate path.

**The finding is the concentration:** the only surviving audit with an undeclared substrate assumption is the one that
owns the **failure core**, and its assumptions are exactly the transport-layer claims that fail. **Neighbour and
Laplacian vocabulary appears only in the two audits that name connectivity as their subject.** 12 of the 14 surviving
audits name no ring, path or neighbour assumption at all.

## 6. The structural classification (task 6)

| candidate | supported | measurement |
|---|---|---|
| NEW PRIMITIVE | **no** | **0** observables read the component count; **0** degrees of freedom are added (the count is a property of the link set, not a field) |
| LOCAL INPUT | **no** | **0 of 96** cells change a local read when the substrate is cut, on the canonical initial condition |
| BOUNDARY CONDITION | **no** | **0 of 96** cells beside the cut read anything new; the cut cells keep their degree |
| **GLOBAL INPUT** | **yes** | **4 of 4** global descriptors move with the component count |

**COMPUTED CLASSIFICATION: GLOBAL INPUT.** The caveat travels with it and is measured rather than
hidden: a state that **varies** at the cut **is** locally visible — **4** of the 96 cells move their acceleration read —
so the LOCAL INPUT refutation is stated **for the canonical state** and not generalised (G_080's own caveat).

## 7. The referee question (task 7)

> **If connectivity is removed from AT, what survives unchanged?**

**23 of 29 claims (0.793)** — the entire local sector and everything that does not consume a path:

- every clock-law claim (the ratio form, the 9-of-9 response, the 8-of-9 acceleration, the second-order separation),
- every potential, cap and measurement claim (the uplift and rate caps, the logarithmic law, the uniform channel),
- the counting claims (total conservation, the component count's conservation, the divergence identity, the
  union-invariance of the primitives, the two-link cut),
- and the ledger **per component**.

**What fails is the transport and continuity layer** — the layer a cut makes ill-defined.

---

## Defects found while building this audit

1. **A per-component count printed as a direction count.** The first version of the verdict reported that the transport
   capacity "falls from 7 to 48 directions" — it had interpolated the **per-component cell count** (`96/2`) where the
   direction count belongs (**6**). The same class as G_079's control-residual defect: a plausible number from the
   wrong quantity, caught by reading the printed verdict rather than by an assertion, since the assertion checked only
   that the string contained the count words.
2. **A rank-load test that could never pass.** The load was first tested by asking whether successive differences were
   equal, which is true only when the component counts are equally spaced (`1, 2, 4, 8` are not), so the *rank itself*
   failed its own test. Replaced by a genuine linearity test (a slope fitted at `c = 2` and verified at `c = 4, 8`).
3. **A non-linearity in the nullity formula.** The first version computed the nullity as `c − rank + (rank − rank)`,
   which is `2c − n` — a plausible-looking expression that is wrong for every `c` — instead of `cells − rank`. Caught
   by writing the value into a table and reading it.
4. **An assertion that the audit's own exclusion had failed.** The test asserted that the self-file appeared in the
   scan **as a check that something had been excluded** — which is backwards: the scan correctly contained no such row.
   The assertion now checks that the file exists in the source root *and* that no row names it.
5. **Two scan patterns fired on ordinary arithmetic.** `(i - 1) * 0.005` matched the ring pattern and
   `Path.GetFileName` matched the path pattern, inflating the undeclared count from 19 to 22 and the file count from 4
   to 7. Both patterns were tightened — a ring closure must wrap modulo a cell count, and `Path` must not be followed by
   a member accessor — and the report's figures were corrected everywhere they had been quoted.
6. **An over-strong assertion about the categories.** The test required every category to have undeclared members;
   the scan instead found that NEIGHBOUR and LAPLACIAN appear **only** in the declared users. The assertion was
   replaced by the finding, which is stronger information than the assertion was.

## What this does and does not establish

- **Does:** measure, claim by claim, how much of the surviving gravity/time sector depends on connectivity
  (**6 of 29 directly**), localise the whole failure core in the transport layer, measure the trigger cost
  (**2 links**), measure the rank load (**4 of 29, 0.138**), scan the sources for undeclared assumptions (**105 sites
  in 4 of 14 audits, 19 undeclared**) and classify connectivity as a **GLOBAL INPUT** with the three alternatives
  refuted by numbers.
- **Does not:** claim the map covers the whole theory (it covers G_035 and G_068–G_080, the gravity/time sector; the
  spectral, gauge and cosmological audits are outside it and are represented only by the hidden-use scan); claim that
  independence from connectivity means the theory is unchanged (a disconnected universe changes what is *observable*,
  which is recorded as INDIRECT); generalise the LOCAL INPUT refutation beyond the canonical initial condition; or
  claim the rank-load test proves dependence (it is an arithmetic signature).

## Where it stands

**Connectivity is a global input with a small, localised failure core.** The question was asked adversarially — how
much collapses without it — and the answer is: the transport and continuity layer, and nothing else. The clocks, the
potentials, the redshifts, the accelerations, the caps and the whole measurement programme are independent of the
component count, count conservation carries every claim except four that are written on the rank, and the only
surviving audit with an undeclared substrate assumption is the one that owns the core. **A small failure core
strengthens the theory: connectivity is an input the local sector does not consume.** The open question it leaves is
the one G_080 named: **what sets the component count** — now with a price attached, six claims and two defects.
