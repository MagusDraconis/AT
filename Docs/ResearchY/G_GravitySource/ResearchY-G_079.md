# ResearchY-G_079 — Continuity Law Audit

**Group:** G (Gravity Source) · **Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_079_Tests.cs` ·
**Core:** `AT.Core/ResearchXH/ContinuityLawAudit.cs` ·
**Registry:** `TemporalIndependenceAudit` (`Y_G_079`)

---

## Question

G_077 established ρ as the sole surviving clock source; G_078 found only transport, with no source and no sink in the
surviving class. This audit asks whether

$$\partial_t\rho + \nabla\cdot J = 0$$

is **forced** — and if it is, what exactly the forcing rests on. A proof of continuity elevates transport from an
**observation** to a **law**; any surviving source term refutes the transport-only conclusion.

1. Can every surviving update rule be rewritten as a continuity equation?
2. Is count conservation equivalent to continuity?
3. Does any surviving update create local source terms?
4. Can source terms be added without breaking previous audits?
5. Are the clock and gravity observables uniquely determined once `J` is specified?
6. Does a source-free theory imply that gravity engineering is limited to redistribution?

## Answer

**BOUNDARY — THE CONTINUITY LAW IS DERIVED FOR THE SURVIVING CLASS, AND ITS THREE PREMISES ARE MEASURED RATHER THAN
ASSUMED.**

- **The law is a theorem and not a tolerance.** For every conserving operator of the class the audit **writes the flux
  down** (`J_i = ρ[i]`, `2J_i = ρ[i+1] + ρ[i]`, `J_i = ρ[i+1] − ρ[i]`), reconstructs it by prefix sums and verifies
  `Δρ + div J = 0` as an **integer equality, residual 0**. The criterion is **live**: the non-divergent control
  **fails** the membership test, which is what makes the successes mean anything.
- **The flux is a gauge.** The divergence on an *n*-cell ring has `rank = n − 1` and **`nullity = 1` for every *n***,
  so the flux carrying a given evolution is fixed only up to **one circulation** — and that circulation is not merely
  undetermined but **unobservable**: **0 of 6** temporal observables read a flux at all.
- **The missing premise is connectivity, and the counterexample is a cut.** On a connected ring the zero-sum subspace
  **is** the image of the divergence (**8 of 8** conserving witnesses are transports). On a two-component substrate the
  image requires **each component's sum** to vanish, so a **transfer across the cut** conserves the total and is **not
  any flux** (**2 of 10** witnesses fail, and 4 of 12 on four components). A transfer **is** a source/sink pair — so
  **conservation does *not* imply continuity; conservation plus connectivity does.** Note the check must be a **rank**
  check and not a sum: the failing witness has total `0`.
- **A source could be added without breaking a single surviving measurement.** The pinned rows constrain the **map**
  from the state to the observables and not the dynamics — measured: **9 of 9** laws carry the pinned slope and all
  **9** target rows are state functions — so any state a source produces still satisfies them. What a source breaks is
  the **closure**, and the detector is G_078's own ledger, which this audit shows is **live**: with a source of
  `0.03` the residual becomes **exactly `0.030`**, and **declaring** the source restores the identity to `0.0E+000`.
- **The observables need one number the flux cannot supply — which the conserved count supplies.** The kernel of the
  divergence is the **uniform mode**: `dim = 1` on a connected ring, so ratios and differences (the **redshift** and
  the **acceleration**) are determined and the **absolute clock rate** is not; the conserved total fixes exactly that
  mode, leaving **0** free modes. On a **disconnected** substrate the kernel has **one dimension per component**, so
  the total fixes the *sum* of the levels and leaves **c − 1 relative levels** free that no measurement in the sector
  can reach (**1** free with two rings, **3** with four).
- **So Q6 is YES, conditionally** — and the audit refuses to answer it unconditionally.

---

## 1. Every surviving update rewritten as a continuity equation

The flux of each operator, written down exactly (the centred form at twice scale so its halves stay integral):

| operator | flux |
|---|---|
| local difference (upwind) | `J_i = ρ[i]` |
| centred difference | `2 J_i = ρ[i+1] + ρ[i]` |
| second difference (Laplacian) | `J_i = ρ[i+1] − ρ[i]` |

The decomposition, with integer reconstruction:

| operator | conserves | is a transport | residual | flux ambiguity | exact |
|---|---|---|---|---|---|
| local difference (upwind) | True | True | 0 | 1 | True |
| centred difference | True | True | 0 | 1 | True |
| second difference (Laplacian) | True | True | 0 | 1 | True |
| transport (one link) | True | True | 0 | 1 | True |
| **CONTROL: growth (not a divergence)** | False | **False** | **n/a** | 0 | False |

The rank and nullity of the divergence operator — `rank = n − c`, `nullity = c`, exactly:

| substrate | cells | components | rank | nullity | image |
|---|---|---|---|---|---|
| one connected ring | 8 | 1 | 7 | 1 | the zero-sum subspace |
| two rings (disconnected) | 8 | 2 | 6 | 2 | changes whose sum vanishes **on each component** |
| four rings (disconnected) | 8 | 4 | 4 | 4 | changes whose sum vanishes **on each component** |
| one ring, canonical size | 96 | 1 | 95 | 1 | the zero-sum subspace |

And the circulation is a gauge, on integers:

| cells | circulation | max change of the divergence |
|---|---|---|
| 4 · 8 · 16 · 96 | 5 | **0** |

## 2. Conservation is not continuity — connectivity is the missing premise

The cut test:

| substrate | cells | components | conserves | is a transport | witness |
|---|---|---|---|---|---|
| one connected ring | 8 | 1 | True | True | a change inside one component |
| two rings | 8 | 2 | True | **False** | a change that **leaves** one component and **enters** another |
| four rings | 8 | 4 | True | **False** | a change that **leaves** one component and **enters** another |

Swept over witnesses rather than asserted on one:

| substrate | witnesses | conserving and transport | conserving and **not** transport | status |
|---|---|---|---|---|
| one connected ring | 8 | **8** | **0** | conservation **is** continuity on this substrate |
| two rings | 10 | 8 | **2** | conservation is **strictly weaker** |
| four rings | 12 | 8 | **4** | conservation is **strictly weaker** |

The check must be a **rank** check and not a sum: the failing witness has total `0`, so a sum test would have accepted
it. **A transfer across a cut is a source/sink pair — count-conserving and not a transport.**

## 3. Does any surviving update create local source terms

An **interior** source is a change outside the image of the divergence; the class supplies **none** (§1). A
**boundary flux** is the one genuinely source-like term the framework can express, and it is exactly the leak G_078
measured on an open chain:

| quantity | value |
|---|---|
| largest interior leak over the class on a **closed** ring | 0.000E+000 |
| boundary term an **open** chain is forced to carry | 1.971E-001 |

So the interior source is zero, and the boundary source is available to **any** substrate with an edge.

## 4. Can source terms be added without breaking previous audits

The ledger as a **live** detector:

| case | source | residual before | residual after | live |
|---|---|---|---|---|
| no source | 0.00 | 0.000 | 0.0E+000 | True |
| source at one cell | 0.03 | **0.030** | 0.0E+000 | True |
| sink at one cell | −0.07 | **0.070** | 0.0E+000 | True |

| question | answer | measurement |
|---|---|---|
| does a source contradict a **pinned** measurement? | **NO** | the pinned rows are functions of the **state** — 9 of 9 laws carry the pinned slope, all 9 target rows are state functions — so a source changes **which** states occur, not the relation they obey |
| does a source contradict the **count-conserving** premise? | **NO** | a balanced pair keeps the total exactly, and the sector has no measurement that excludes a changing total either |
| does a source contradict **G_078's class** result? | **NO** | G_078 measured what the **class** does; a source is an **added primitive**, so the class result stands and only its scope changes |
| is a source **detectable**? | **YES** | exactly: the ledger residual equals the source to the last bit |

**No surviving measurement excludes a source.** What a source breaks is the **closure**, which is a structural premise
rather than a measured fact — so the transport-only conclusion is forced **given the class**, not by the data.

## 5. Are the observables uniquely determined once `J` is given

| substrate | cells | components | kernel | free modes after the total | determined |
|---|---|---|---|---|---|
| one connected ring | 8 | 1 | 1 | **0** | True |
| two rings | 8 | 2 | 2 | **1** | False |
| four rings | 8 | 4 | 4 | **3** | False |
| one connected ring, canonical size | 96 | 1 | 1 | **0** | True |

**Temporal observables that read a flux rather than a state: 0 of 6.** The circulating flux is therefore not merely
undetermined — it is **unobservable**. The circulation invariant is swept over the ring size (4, 8, 16, 48, 96):
**nullity 1 for every *n***, so it is a property of the ring and not of one chosen size.

## 6. Does a source-free theory limit manipulation to redistribution

**Yes, conditionally.** The chain, each link measured:

| link | measured |
|---|---|
| (a) transport **is** forced for the class | **True** |
| (b) no surviving measurement excludes a source | **True** |
| (c) connectivity is load-bearing | **True** (2 of 10 conserving witnesses are not fluxes) |

So **if** the update class is the surviving class **and** the substrate stays connected and closed **and** no source is
added, every change of the source is a redistribution and G_078's cap applies — uplift `1.5249`, rate ratio `4.5947`
on the canonical lattice. **Any one** of those premises failing opens a channel: a source changes the total, a
boundary supplies a boundary flux, a cut makes a transfer a source/sink pair.

---

## Defects found while building this audit

1. **The control's residual read as a success.** A non-divergent change has **no flux at all**, yet the table printed
   `residual = 0` for it — a reader would see the same number that means "reconstructed exactly". Fixed by
   initialising the residual to `−1` and rendering it as **`n/a`**.
2. **Every cross-transfer was counted twice.** The witness sweep generated the transfer `A→B` from both of its sides,
   doubling the failure count (`8 of 16` instead of `2 of 10`). Fixed by taking each cross-transfer **once per pair**.
3. **`Single` was used where the substrate name is not unique** — `"one connected ring"` matches both the 8-cell and
   the canonical 96-cell rows, so three report functions threw `Sequence contains more than one matching element`
   instead of returning a figure. Caught by the tests, not by reading.
4. **A garbage assertion reached the test file** (`verdict.ToUpperInvariant().Replace(...) is string s ? … : ""`), which
   would have asserted an empty string and passed vacuously; replaced by a check of the actual sentence the verdict
   carries.
5. **A scale factor nearly became a physics claim.** The centred operator's flux carries halves; the audit keeps it at
   **twice scale** rather than dividing, because dividing would have put floating point into the one place where the
   audit claims **exactness**.

## What this does and does not establish

- **Does:** every conserving operator of the surviving class **is** a divergence, exactly; the flux is unique up to one
  **unobservable** circulation; conservation ⟺ continuity **requires connectivity**; the source term is **detectable
  and not excluded**; the observables are fixed once the divergence and the conserved total are given; and a
  source-free conserved theory limits manipulation to redistribution.
- **Does not:** supply a **derivation of connectivity** (it is a property of the canonical substrate, not of the
  theory); exclude a source term by measurement; extend the exact-integer result to a continuum derivation; or remove
  the **boundary** route to a source, which exists for any substrate with an edge.

## Where it stands

**Transport is elevated from an observation to a law — on a connected, closed, source-free substrate — and the audit
measures each of those three words.** What is proved is a theorem in exact integer arithmetic with a live failing
branch; what is *not* proved is that any of the three premises had to hold. **A surviving source term would refute the
transport-only conclusion, and the honest position is that one could be added without contradicting a single
measurement in the surviving list.** The sector supplies a **detector** rather than a prohibition — the ledger residual
equals the source exactly — and that is the strongest form the claim can take: **falsifiable, not forbidden.** The open
question handed on is the premise the law cannot supply for itself: **is connectivity a consequence of the theory or
an input to it?**
