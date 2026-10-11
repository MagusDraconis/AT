# AT Project Memory — Active Programs

What is running now, what is waiting on data, and what has been closed. Split out of `Docs/NewChat_Start.md` on
2026-10-11.

- Current state and status table: `Docs/NewChat_Start.md`
- Open problems and the four next questions: `Docs/NewChat_OpenProblems.md`
- Audit catalogue: `Docs/AuditIndex.md`
- Detailed experiment log: `Docs/AT_LabBook.md`

---

## 1. The ResearchY audit programme — running

Each audit is one investigation: a research doc, an xUnit suite, an entry in the temporal-independence registry and a
Theory Book chapter. A verdict is one of **DERIVED / EMERGENT / BOUNDARY / REFUTED** — the two-level pattern of
D_028/D_040 is standard: a **value** may be DERIVED while the **window or input** it needs is BOUNDARY.

| Front | State |
|---|---|
| Group G — gravity source | **at G_080** (connectivity origin). The chain G_068-G_080 closed the time sector. |
| Group QM — many-body correspondence | **closed** at QM_011 (the spectral fingerprint is an input, not a law). |
| Group E — electromagnetism | **complete** at E_016. |
| Groups A, B, C, D, M, NP, QG, R, S, T | complete or closed; catalogue in `Docs/AuditIndex.md`. |

Standing guards that must stay green: `Y_G_027` (no literal reaches a verdict), `Y_G_033` (which suites use the
spectral substrate), `Y_G_034` (A₀ robustness), `Y_G_035` (the temporal-independence registry covers every suite).

## 2. The prediction programme — frozen, awaiting data

Three predictions are **pre-registered and locked** (QG193: values immutable, only outcomes may be added):

| | Prediction | Frozen value | Decided by |
|---|---|---|---|
| P1 | Z-anchor resonance | **106.39 GeV**, window 99-114 GeV | HL-LHC |
| P2 | 0νββ effective mass (Majorana, normal ordering) | **m_ββ = 2.02 meV** | nEXO / LEGEND-1000 |
| P3 | Sector ladder, 9 rungs | **106.39 → 263.43 GeV**, quanta 15.20 GeV (×10) + 20.26 GeV (×1) | LHC / FCC-hh |

Registry: `Docs/ATQG_Predictions.md` (+ `.json`, immutable) and outcomes in `Docs/ATQG_PredictionOutcomes.md`. The
106 GeV window is **not** excluded by the Run-2 diphoton null searches; the CMB observables (n_s, ℓ₁, ratios) are
already published as derived.

## 3. The time-sector measurement programme — one measurement on one object

G_072: the theory's unique time prediction `1 + z = exp(−x)` differs from GR's `(1 + 2x)^(−1/2)` at **second order**,
and one object decides it:

- **J0740+6620** surface redshift: AT **14.1422 %** against GR **7.5524 %** — reached by a next-generation **timing**
  capability at **4.1374 σ**, and by a direct **1 % surface-redshift** measurement at **44.8377 σ**.
- **The leverage is the timing, not the mass**: the published mass row and the radio-timing row both fail.

What G_073-G_080 establish about the same sector: the clock law's **form** is an input (only `F(0) = 1`, `F'(0) = 2`
are forced, and the surviving family contains GR), the clock has **no independent source**, redistribution is the only
elementary channel, and the substrate's **connectivity is an input** as well.

## 4. The publication programme — V2.0 monograph and the coverage record

- **Monograph V2.0** is complete; the editorial density passes (Ch1-Ch14) removed theorem-style block density without
  touching a claim, value or citation (93-106 pages, clean `pdflatex`, no undefined references). Referee-safe wording
  (MONO007) is retained once per chapter.
- **Coverage record**: `Docs/ATQG_PhysicsCoverage.md` and `.json` are the machine-readable single source of truth for
  validation status; the claim-classification registry mirrors it in AT.Book.
- **PRED001**: the prediction registry is a standalone publication record (Zenodo).

## 5. Closed programmes (not active)

- **V2.2 lock-lattice hardware programme** — formally closed (NP_175): no AT-specific observable remains, so a
  hardware search is not warranted.
- **V2.1 wave-geometry track** — archived (see `Docs/Archive/NewChat_Part5_TheoryEvolution.md`).
- **Build dependency** — `SixLabors.ImageSharp` replaced by `SkiaSharp` (2026-09-13): ImageSharp's build targets
  hard-failed every Release build without a commercial licence.

## 6. Rules in force while working

- **Validate in the configuration you ship**: build and test **Release** (`dotnet build AT.slnx -c Release`,
  `dotnet test AT.Tests/AT.Tests.csproj -c Release`), never Debug alone.
- **Prefer a failing test to a prose rule**: a new numerical hazard gets a scanner that re-reads the source.
- New findings are surfaced in AT.App (Research News + Theory Book) and recorded in `Docs/AT_LabBook.md` and, when
  they change the knowledge, in this memory set.
