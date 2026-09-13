# Y_G_044 — Minimal Working Substrate Audit — Result

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_044_Tests.cs` — **7/7 PASSED**
**Core:** `AT.Core/ResearchXH/MinimalWorkingSubstrateAudit.cs`
**Doc:** `Docs/ResearchY/G_GravitySource/ResearchY-G_044.md`

## Question

Is D96³ selected because it is the **first working** substrate, or because it **minimises complexity**?

## Answer: **EMERGENT** — the two explanations are one statement; **OPTIMAL is REFUTED**

## The working set is an up-set (computed)

| d | photon propagates | graviton propagates | 3-dim irrep | works |
|---|---|---|---|---|
| 1 | no | no | no | no |
| 2 | yes | **no** | no | **no** |
| 3 | yes | yes | yes | **YES** |
| 4–6 | yes | yes | yes | YES |

Working set = **{3, 4, 5, 6}** — one lower edge. d = 2 fails precisely because the graviton's sector *exists*
(2-dimensional) but carries **0** propagating states.

## All six measures are monotone in cost

| measure | D96² | D96³ | D96⁴ | D96⁵ |
|---|---|---|---|---|
| state space 96^d − 1 | 9 215 | 884 735 | 84 934 655 | 8 153 726 975 |
| irreps of B_d | 5 | 10 | 20 | 36 |
| photon support | 2 | 3 | 4 | 5 |
| graviton support | 2 | 5 | 9 | 14 |
| observability fraction | 0.1329 | 0.02354 | 0.00319 | 0.00035 |
| states per observable | 7.522 | 42.484 | 313.730 | 2 841.332 |

## The theorem: minimality ≡ firstness

With strictly increasing costs, **the cheapest member of any set is its smallest element** — so the
minimum-complexity working substrate **IS** the first working substrate, for all six measures at once. The two
proposed explanations **cannot differ**. Minimality is therefore a **corollary of first-ness**, which is why the
verdict is EMERGENT rather than DERIVED.

## The refutation: optimality

Every measure's **unconstrained** optimum lies at **d = 1 or 2**, outside the working set. D96³ is **96×** the
state space of D96², **5.65×** its states per observable and **5.65×** less observable — the best *working*
substrate, and a poor substrate outright.

## Computed aside

The observability cost factor per step: **3.88** (1→2), **5.65** (2→3), **7.38** (3→4), **9.11** (4→5) — the step
the theory is **forced** to take is the **least expensive available** step. A convenience, not a selection
argument.

## Options

| option | status |
|---|---|
| MINIMAL | **EMERGENT** — automatic given monotone costs |
| OPTIMAL | **REFUTED** — the optima are at d = 1 and 2 |
| FIRST | **DERIVED** — the computed fact; the working set is {3, 4, 5, 6} |

## Registry

`ClockOnly` → **SURVIVES**, `ScanDetectsIt: false`. Counts become **31 / 11 / 3 of 45**; boundary index
unchanged; no prior classification changed. G_033's live classifier now reads **26** substrate suites (43
classified).

## Caveats

* No weighting between the six measures is assumed — the theorem needs only monotonicity, which all six have.
* "Optimal" is defined over the whole ladder including non-working substrates; that is what makes it a different
  claim from "minimal", and it is the claim that fails.
* The observability measures rest on the orbital count `C(48 + d, d)`, verified against G_040's 49 at d = 1.
* This audit does **not** settle *why* the working set begins at 3 (G_043's question) — only that **everything
  above the lower edge is irrelevant to the choice**.
