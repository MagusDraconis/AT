namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 236 — Inflation Necessity Audit. Determines whether inflation is actually required, by
/// checking the five problems inflation was invented to solve against the TQM derivations (QG227-231).
/// Audit only — no new physics, no new derivations. Deterministic.
///
/// THE FIVE PROBLEMS (what inflation was invented for), each checked:
///  1. HORIZON PROBLEM — why is the CMB isotropic across causally disconnected regions?
///       • solved by inflation: a superluminal expansion epoch puts the whole observed sky inside one
///         causal horizon.
///       • solved by TQM: the initial state is the UNIFORM CRITICAL STATE ρ_k = 1/K (QG227) — a single
///         globally-uniform fixed point of the actualization flow. Isotropy is inherited from the initial
///         state's exact uniformity, not built by an epoch. No horizon problem exists to solve.
///  2. FLATNESS PROBLEM — why is Ω ≈ 1 so precisely?
///       • solved by inflation: the expansion drives Ω → 1.
///       • solved by TQM: the single-scale R universe has Ω_Λ + Ω_m = 1 EXACTLY as a structural identity
///         (QG230 Λ ~ ρ̄; QG234 Ω_Λ + Ω_m = 1). Flatness is derived, not fine-tuned.
///  3. INITIAL PERTURBATIONS — where do the primordial density fluctuations come from?
///       • solved by inflation: quantum vacuum fluctuations stretched by the expansion.
///       • solved by TQM: the Poisson counting variance of Q-events (QG228 information; QG231 seeds
///         δ_i = 1/√⟨N⟩). The seeds are derived from the counting measure, no epoch needed.
///  4. CMB ISOTROPY — is the CMB isotropic (and on what angular scales)?
///       • solved by inflation: the horizon problem's resolution gives large-scale isotropy.
///       • solved by TQM: the uniform critical initial state (QG227) is isotropic by construction; QG77
///         confirms conformal-metric CMB isotropy compatibility.
///  5. STRUCTURE FORMATION — how do structures grow from the seeds?
///       • solved by inflation: gravitational growth of the inflationary spectrum.
///       • solved by TQM: the pressureless deficit dust grows the Poisson seeds linearly (QG231:
///         δ(a) = δ_i·a/a_i).
///
/// RESULT: all five problems are SOLVED BY TQM without an inflationary epoch.
///
/// CAVEAT — the CMB ANISOTROPY SPECTRUM (the acoustic-peak structure and the near-scale-invariant tilt
/// n_s ≈ 0.96 that inflation predicts) is NOT numerically derived by TQM: the Poisson seed is white/scale-
/// free (δ_i = 1/√⟨N⟩), not the slightly red-tilted spectrum inflation predicts, and the acoustic-peak
/// pattern is not computed (QG235 marks CMB spectrum PARTIAL). So the inflation EPOCH is replaced, but its
/// specific predictive CONTENT (the spectrum) is not fully reproduced.
///
/// CLASSIFICATION: PARTIAL INFLATION — inflation is NOT REQUIRED (all five motive problems are solved by
/// TQM: uniform critical initial state QG227, exact flatness identity QG230/234, Poisson seeds QG228/231,
/// built-in isotropy, linear structure growth), so its epoch is REPLACED; but the replacement is PARTIAL
/// because the inflationary perturbation SPECTRUM (tilt n_s, acoustic peaks) is not numerically matched —
/// the seeds are Poisson-white, not near-scale-invariant.
/// </summary>
public static class InflationNecessityAudit
{
    public enum Resolution { ByInflation, ByTqm, Unresolved }

    /// <summary>A checked problem.</summary>
    public sealed record Check(
        int Index,
        string Name,
        Resolution Resolved,
        string InflationSolution,
        string TqmSolution);

    /// <summary>The five problems checked.</summary>
    public static Check[] Checks() => new[]
    {
        new Check(1, "Horizon problem", Resolution.ByTqm,
            "a superluminal expansion epoch places the observed sky inside one causal horizon",
            "the initial state is the UNIFORM critical state ρ_k = 1/K (QG227) — a single globally-uniform fixed point; isotropy is inherited, no epoch needed"),
        new Check(2, "Flatness problem", Resolution.ByTqm,
            "the expansion drives Ω → 1",
            "the single-scale R universe has Ω_Λ + Ω_m = 1 EXACTLY as a structural identity (QG230 Λ ~ ρ̄; QG234) — flatness is derived, not fine-tuned"),
        new Check(3, "Initial perturbations", Resolution.ByTqm,
            "quantum vacuum fluctuations stretched by the expansion",
            "the Poisson counting variance of Q-events: δ_i = 1/√⟨N⟩ (QG228 information, QG231 seeds) — derived from the counting measure"),
        new Check(4, "CMB isotropy", Resolution.ByTqm,
            "the horizon resolution gives large-scale isotropy",
            "the uniform critical initial state (QG227) is isotropic by construction; QG77 confirms conformal CMB isotropy"),
        new Check(5, "Structure formation", Resolution.ByTqm,
            "gravitational growth of the inflationary spectrum",
            "the pressureless deficit dust grows the Poisson seeds linearly (QG231: δ(a) = δ_i·a/a_i)"),
    };

    /// <summary>Number of problems solved by TQM.</summary>
    public static int TqmSolvedCount()
        => Checks().Count(c => c.Resolved == Resolution.ByTqm);

    /// <summary>Number of problems that would be solved by inflation (0 in the TQM view).</summary>
    public static int InflationSolvedCount()
        => Checks().Count(c => c.Resolved == Resolution.ByInflation);

    /// <summary>Number of unresolved problems.</summary>
    public static int UnresolvedCount()
        => Checks().Count(c => c.Resolved == Resolution.Unresolved);

    // ── The CMB-spectrum caveat ───────────────────────────────────────────────

    /// <summary>The CMB anisotropy spectrum (acoustic peaks, tilt n_s) is not numerically derived.</summary>
    public static bool CmbSpectrumNotDerived()
        => true;

    /// <summary>The Poisson seed is white/scale-free (δ_i = 1/√⟨N⟩), not the near-scale-invariant inflationary tilt.</summary>
    public static bool SeedSpectrumIsWhite()
        => true;

    /// <summary>All five motive problems are solved by TQM.</summary>
    public static bool AllFiveSolvedByTqm()
        => TqmSolvedCount() == 5;

    // ── Classification ────────────────────────────────────────────────────────

    /// <summary>
    /// Inflation classification:
    ///   INFLATION REQUIRED — the motive problems are not solved by TQM (unresolved or inflation-only);
    ///   INFLATION REPLACED — all motive problems are solved by TQM AND the observable spectrum is matched;
    ///   PARTIAL INFLATION  — all motive problems are solved by TQM (the epoch is replaced) but the
    ///                        observable spectrum content (tilt, acoustic peaks) is not fully reproduced.
    /// </summary>
    public static string Classify()
    {
        if (!AllFiveSolvedByTqm()) return "INFLATION REQUIRED";
        if (CmbSpectrumNotDerived() || SeedSpectrumIsWhite()) return "PARTIAL INFLATION";
        return "INFLATION REPLACED";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
        => $"{Classify()} — {TqmSolvedCount()}/5 motive problems solved by TQM "
         + $"({InflationSolvedCount()} by inflation, {UnresolvedCount()} unresolved); "
         + $"epoch REPLACED, spectrum content {(CmbSpectrumNotDerived() ? "NOT matched (CMB anisotropy spectrum not numerically derived)" : "matched")}";
}
