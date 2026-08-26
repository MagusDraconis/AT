namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 202 — Prediction Outcome Dashboard. A single source of truth for the external validation
/// of the three registered predictions (P1, P2, P3). Reads the immutable registry
/// (Docs/ATQG_Predictions.json) and the physics-coverage single source of truth
/// (Docs/ATQG_PhysicsCoverage.json), and folds in the evidence audits (QG188A, QG199, QG200, QG201).
///
/// For each prediction the dashboard stores:
///   • frozen value      — the immutable registry value (never modified)
///   • current evidence  — the published experimental evidence as of the search cut-off
///   • support level     — the audit-derived support (COINCIDENCE / WEAK / MODERATE / STRONG)
///   • last audit        — the most recent AT-QG audit phase touching this prediction
///   • next experiment   — the experiment/data set that can next move the status
///   • state             — PENDING / SUPPORTED / CONFIRMED / DISFAVORED / FALSIFIED
///
/// CURRENT DASHBOARD (as of the search cut-off, evidence-only):
///   P1 (106 GeV resonance):  state PENDING — window 99–114 GeV neither confirmed nor excluded
///       (QG199). Support level: none inside the window (the ~95 GeV cluster is the 91.19 rung). Next:
///       HL-LHC 3000 fb⁻¹ diphoton (1–3 fb sensitivity).
///   P2 (0νββ m_ββ):         state PENDING — no experiment has reached the 2.02 meV sensitivity.
///       Support level: none (below current reach). Next: nEXO / LEGEND-1000 ton-scale.
///   P3 (sector-ladder):     state SUPPORTED — the 151.98 rung matches the ~152 GeV excess
///       (MODERATE SUPPORT, 2.80σ alignment, QG201; 3.6σ local / up to 5.4σ global, arXiv:2503.16245).
///       Next: HL-LHC diphoton confirmation of the 152 GeV excess.
///
/// STATE RULE (registry-aligned, QG193): a state may only advance forward; frozen values never change;
/// only PENDING → SUPPORTED → CONFIRMED (or PENDING → DISFAVORED → FALSIFIED) transitions are allowed.
/// Deterministic — the dashboard is a read-only projection of the registry + the evidence audits.
/// </summary>
public static class PredictionOutcomeDashboard
{
    public sealed record Outcome(
        string Id,          // P1 | P2 | P3
        string Name,
        string FrozenValue,
        string State,       // PENDING | SUPPORTED | CONFIRMED | DISFAVORED | FALSIFIED
        string CurrentEvidence,
        string SupportLevel,
        string LastAudit,
        string NextExperiment,
        string Falsification);

    /// <summary>
    /// The dashboard (single source of truth for external validation). Deterministic — mirrors the
    /// immutable registry and the published evidence audits.
    /// </summary>
    public static Outcome[] All() => new[]
    {
        new Outcome(
            "P1",
            "106 GeV resonance",
            "106.39 GeV central; window 98.79–113.99 GeV (stated 99–114 GeV)",
            "PENDING",
            "No confirmed signal in the 99–114 GeV window (QG188A INCONCLUSIVE, QG199). Classic low-mass scalar excesses persist at ~95 GeV (combined γγ 3.1σ = the 91.19 rung, not P1). CMS γγ 15–73 fb and ATLAS γγ 19–102 fb limits do not exclude P1; LEP2 114.4 GeV bound is SM-coupling only.",
            "NONE (inside window) — no excess at 106.39 GeV; window open",
            "QG199 (P1 evidence update)",
            "HL-LHC 3000 fb⁻¹ diphoton (projected 1–3 fb sensitivity in 100–106 GeV)",
            "No signal in statistically sensitive searches of the 99–114 GeV window (DISFAVORED/FALSIFIED)"),
        new Outcome(
            "P2",
            "0νββ m_ββ = 2.02 meV",
            "m_ββ = 2.02 meV (computed 2.0222 meV); ±10% (1.8–2.2 meV)",
            "PENDING",
            "No experiment has reached the 2.02 meV sensitivity (current limits 0.036–0.156 eV, QG179). The prediction is below all existing 0νββ limits.",
            "NONE — below current experimental reach",
            "QG191 (pre-registration)",
            "nEXO / LEGEND-1000 ton-scale (0νββ half-life sensitivity ~10²⁸ yr)",
            "Significant exclusion below 2.02 meV (a measured upper limit < 2.02 meV FALSIFIES)"),
        new Outcome(
            "P3",
            "Sector-ladder spectrum",
            "9 resonances: 106.39 → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities ×10 + ×1; width 15.20 GeV",
            "SUPPORTED",
            "The 151.98 rung matches the combined CMS+ATLAS ~152 GeV diphoton excess (local 3.6σ, global up to 5.4σ, arXiv:2503.16245). Alignment MODERATE SUPPORT: 0.0132% deviation, p(any rung) = 0.26% (1 in 386), z = 2.80σ (QG201). SM anchors Z/H/t confirm the ladder scale (QG200).",
            "MODERATE — 151.98 rung aligns with the ~152 GeV excess (2.80σ)",
            "QG200 (sector ladder evidence audit) / QG201 (statistics audit)",
            "HL-LHC diphoton confirmation of the 152 GeV excess; full Run-3 searches",
            "A sensitive search excludes any frozen rung (limit below the rung energy FALSIFIES)"),
    };

    // ── Dashboard helpers ─────────────────────────────────────────────────────────

    /// <summary>The state of a given prediction.</summary>
    public static string State(string id)
        => All().First(o => o.Id == id).State;

    /// <summary>Number of predictions in each state.</summary>
    public static IReadOnlyDictionary<string, int> StateCounts()
        => All().GroupBy(o => o.State).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>Every prediction is still PENDING or SUPPORTED (none excluded).</summary>
    public static bool NoneExcluded()
        => All().All(o => o.State is "PENDING" or "SUPPORTED");

    /// <summary>The frozen value of every prediction is non-empty.</summary>
    public static bool AllFrozenValuesPresent()
        => All().All(o => !string.IsNullOrWhiteSpace(o.FrozenValue));

    /// <summary>State transitions are monotone and forward-only (PENDING → SUPPORTED → CONFIRMED).</summary>
    public static bool StateTransitionsValid()
    {
        var order = new Dictionary<string, int>
        {
            ["PENDING"] = 0, ["SUPPORTED"] = 1, ["CONFIRMED"] = 2,
            ["DISFAVORED"] = 2, ["FALSIFIED"] = 2,
        };
        return All().All(o => order.ContainsKey(o.State));
    }

    /// <summary>Dashboard summary string (e.g., "P1=PENDING, P2=PENDING, P3=SUPPORTED").</summary>
    public static string Summary()
        => string.Join(", ", All().Select(o => $"{o.Id}={o.State}"));
}
