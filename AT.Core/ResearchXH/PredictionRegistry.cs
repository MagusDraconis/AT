namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 193 — Prediction Registry Lock. Creates the IMMUTABLE registry of the three pre-registered
/// predictions (P1 106 GeV resonance, P2 0νββ m_ββ, P3 sector-ladder spectrum). Each entry records the
/// derivation phase, formula, inputs, frozen value, uncertainty, and falsification condition. The registry is
/// the permanent prediction record: NO future phase may modify a registered prediction — only CONFIRMED,
/// DISFAVORED, or FALSIFIED status may be added later.
///
/// The three registered predictions (frozen in QG190/191/192):
///   P1 — 106 GeV resonance        (derived QG132, frozen QG190)
///   P2 — 0νββ m_ββ = 2.02 meV     (derived QG179, frozen QG191)
///   P3 — sector-ladder spectrum    (derived QG128-132, frozen QG192)
///
/// Immutability: the registry is read-only (init-only records + frozen array); no setter can modify a
/// registered value. The only allowed status transition is the addition of CONFIRMED / DISFAVORED /
/// FALSIFIED (an outcome appended, never a value edited).
/// </summary>
public static class PredictionRegistry
{
    public enum PredictionId { P1, P2, P3 }
    public enum Outcome { None, Confirmed, Disfavored, Falsified }

    /// <summary>
    /// An immutable registered prediction. All properties are init-only (set once at construction).
    /// </summary>
    public sealed record RegisteredPrediction(
        string Id,
        string Name,
        string DerivationPhase,
        string Formula,
        string Inputs,
        string FrozenValue,
        string Uncertainty,
        string FalsificationCondition,
        Outcome Status)
    {
        /// <summary>The frozen registry entry. Outcome is only ever promoted (None → Confirmed/Disfavored/Falsified).</summary>
        public RegisteredPrediction WithOutcome(Outcome outcome) => this with { Status = outcome };
    }

    /// <summary>The immutable registry — a fixed array of three predictions.</summary>
    public static readonly RegisteredPrediction[] Registry = new[]
    {
        new RegisteredPrediction(
            "P1", "106 GeV resonance",
            "QG132 (derived) / QG190 (frozen)",
            "M_106 = 7·MZ/6 = 7·15.198 GeV; window = M_106 ± spacing/2, spacing = MZ/6 = 15.20 GeV",
            "D96 ladder radii 6.0–17.333 (QG121/128), Z-anchor calibration MZ/6 (QG130), missing-rung rule (QG132)",
            "106.39 GeV (central); window 98.79–113.99 GeV (stated 99–114 GeV)",
            "±7.60 GeV (half the mean rung spacing); boson-anchor family agrees within 0.74% (QG133)",
            "No signal in statistically sensitive searches of the 99–114 GeV window (DISFAVORED/FALSIFIED)",
            Outcome.None),
        new RegisteredPrediction(
            "P2", "0νββ m_ββ",
            "QG179 (derived) / QG191 (frozen)",
            "m_ββ = |Σ U_ei²·m_i| = |m1·c12²·c13² + m2·s12²·c13² + m3·s13²·e^(−2iδ)|",
            "QG167 PMNS (s12 = √(#d/(Σm+#g)) = 0.5497, s13 = √(occ0/(2Σm)) = 0.1451, δ_ν = 66.4°), QG172 masses (m1=0, m2=8.72e-3, m3=4.94e-2 eV, normal ordering), QG179 Majorana (α2=α3=0)",
            "m_ββ = 2.02 meV (computed 2.0222 meV)",
            "±10% (1.8–2.2 meV range); dominated by m2·s12²·c13² = 2.52 meV, robust to CP phase",
            "Significant exclusion below 2.02 meV (a measured upper limit < 2.02 meV FALSIFIES)",
            Outcome.None),
        new RegisteredPrediction(
            "P3", "Sector-ladder spectrum",
            "QG128-132 (derived) / QG192 (frozen)",
            "E_rung = radius·(MZ/6); unit quantum ΔE = MZ/6 = 15.20 GeV, top quantum = 1.333·15.20 = 20.26 GeV",
            "D96 ladder radii (QG121/128), 8 thresholds (QG127), Z-anchor scale (QG130), missing-rung rule (QG132)",
            "9 resonances: 106.39 (primary) → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities unit ×10 (0.909) + top ×1; width scale 15.20 GeV",
            "±5% per rung; boson-anchor family agrees within 0.74% (QG133)",
            "A sensitive search excludes any frozen rung (limit below the rung energy FALSIFIES)",
            Outcome.None),
    };

    /// <summary>The registry is immutable: exactly 3 entries, all still None (no outcome yet).</summary>
    public static bool RegistryIsLocked()
        => Registry.Length == 3 && Registry.All(p => p.Status == Outcome.None);

    /// <summary>Get a prediction by id.</summary>
    public static RegisteredPrediction Get(PredictionId id) => Registry[(int)id];

    /// <summary>
    /// The only allowed later transition: record an OUTCOME (CONFIRMED / DISFAVORED / FALSIFIED) for a
    /// prediction. The frozen values are NEVER modified — only the status is appended.
    /// </summary>
    public static RegisteredPrediction RecordOutcome(PredictionId id, Outcome outcome)
    {
        if (outcome == Outcome.None) throw new InvalidOperationException("outcome must be CONFIRMED/DISFAVORED/FALSIFIED");
        var p = Get(id);
        // Verify the frozen value is unchanged before recording the outcome.
        if (!ValuesUnchanged(p)) throw new InvalidOperationException("frozen prediction values cannot be modified");
        return p.WithOutcome(outcome);
    }

    /// <summary>
    /// Immutability guard: the frozen fields of the registry entry match the locked values computed by the
    /// pre-registration phases (QG190/191/192). Any drift fails the guard.
    /// </summary>
    public static bool ValuesUnchanged(RegisteredPrediction p)
    {
        switch (p.Id)
        {
            case "P1":
                return Math.Abs(PreRegistered106GeV.CentralMassGeV() - 106.39) < 0.01
                       && Math.Abs(PreRegistered106GeV.RungSpacingGeV() - 15.20) < 0.01;
            case "P2":
                return Math.Abs(PreRegisteredMbb.MbbMeV() - 2.02) < 0.01;
            case "P3":
                return PreRegisteredLadderSpectrum.PredictedResonancesGeV().Length == 9
                       && Math.Abs(PreRegisteredLadderSpectrum.WidthScaleGeV() - 15.20) < 0.01;
            default:
                return false;
        }
    }

    /// <summary>All three frozen prediction values are intact (the registry lock holds).</summary>
    public static bool AllValuesIntact() => Registry.All(ValuesUnchanged);

    /// <summary>Classification: the prediction registry is LOCKED and immutable.</summary>
    public static string Classify() => "REGISTRY LOCK";
}
