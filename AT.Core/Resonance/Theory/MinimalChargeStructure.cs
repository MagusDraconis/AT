namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for analyzing why Q=1 is the minimal stable charge quantum.
/// Defines sub-quantum construction attempts, stability profiles, minimal
/// structure candidates, and the minimality theorem.
///
/// AT-122: Origin of the Charge Quantum
/// </summary>
public static class MinimalChargeStructure
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>Attempt to construct a stable structure with 0 < Q < 1.</summary>
    public sealed record SubQuantumAttempt(
        string Name,
        string ConstructionMethod,
        bool StructureCreated,      // did we make the R-field?
        double PeakR,
        double EffectiveWidth,      // estimated half-width
        double MeasuredQ,           // Q at T=0.5 (should be 0 for true sub-Q)
        bool IsStable,              // survives PDE evolution?
        double StabilityLifetime,   // iterations before collapse/change
        double CriticalWidth,       // theoretical minimum width for this M
        bool BelowCriticalWidth,    // is width < w_c?
        string FailureReason);

    /// <summary>Stability analysis of a condensate profile.</summary>
    public sealed record StabilityProfile(
        double Width,
        double PeakR,
        double ReactionForce,       // c₀·M·R·(1−R²)
        double DiffusionForce,      // D_R·R/w²
        double NetForce,            // reaction − diffusion
        bool IsStable,              // reaction > diffusion at boundary?
        double EstimatedLifetime,
        string Regime);             // "Stable", "Marginal", "Unstable"

    /// <summary>Candidate explanation for why Q=1 is minimal.</summary>
    public sealed record MinimalityMechanism(
        string Name,
        string Description,
        bool IsSufficient,
        string MathematicalBasis,
        string Limitations);

    /// <summary>A step in the minimality proof.</summary>
    public sealed record MinimalityProofStep(
        int StepNumber,
        string Statement,
        string Justification,
        string Conclusion);

    /// <summary>Complete quantum origin report.</summary>
    public sealed record QuantumOriginReport(
        List<SubQuantumAttempt> SubQuantumAttempts,
        List<StabilityProfile> StabilityProfiles,
        List<MinimalityMechanism> Mechanisms,
        MinimalityMechanism BestMechanism,
        List<MinimalityProofStep> Proof,
        double MinimumStableWidth,
        double CriticalReactionDiffusionRatio,
        bool MinimalChargeDerived,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Minimality mechanisms
    // ══════════════════════════════════════════════════════════════════

    public static List<MinimalityMechanism> GetMechanisms()
    {
        return new List<MinimalityMechanism>
        {
            new("A: Minimal Connected Component",
                "Q = 1 is the smallest non-zero Betti number. β₀ counts " +
                "connected components. β₀=0 is vacuum; β₀=1 is one component. " +
                "There is no β₀=0.5 — components are discrete.",
                true,
                "Point-set topology: a set either has a connected component " +
                "or it doesn't. The smallest non-zero count of anything is 1.",
                "Explains why Q starts at 1 but not WHY one component is stable. " +
                "A component could exist momentarily and then decay → Q=1 transient."),

            new("B: Minimal Kink-Antikink Pair",
                "Each Q=+1 requires one kink + one antikink. A kink without " +
                "antikink is NOT a closed configuration. The pair is the " +
                "SMALLEST topologically closed unit.",
                true,
                "With R(0)≈0, R(L)≈0: any R>0.5 excursion requires one upward " +
                "crossing (kink) + one downward crossing (antikink). 1 kink " +
                "without antikink = R>0.5 at a boundary → not a closed config.",
                "Explains why Q=1 is the unit but not why the pair is STABLE. " +
                "A kink-antikink pair could in principle annihilate."),

            new("C: Minimal Homotopy Sector",
                "The configuration space has homotopy classes indexed by Q. " +
                "Q=0 is the trivial class. Q=1 is the FIRST non-trivial class. " +
                "There is no class between 0 and 1.",
                true,
                "Homotopy classes are discrete: π₀(Map(Ω,[0,1]) rel {R=0.5}) ≅ ℕ. " +
                "Classes are indexed by integer crossing number. No class for " +
                "crossing number = 0.5.",
                "Explains discrete spectrum but not minimality — just restates Q∈ℕ."),

            new("D: Minimal Stable Reaction-Diffusion Structure",
                "A condensate requires reaction > diffusion at the boundary. " +
                "This imposes a MINIMUM WIDTH: w > w_c = √(2D_R/(c₀·M)). " +
                "Structures narrower than w_c are UNSTABLE — diffusion dominates.",
                true,
                "Balance equation: c₀·M·R·(1−R²) = D_R·R/w² at R=0.5. " +
                "→ w_min² = 4D_R/(3c₀·M). For typical M≈1: w_min≈0.06. " +
                "A condensate is one connected component of width ~w. " +
                "One component = Q=1. The MINIMUM STABLE STRUCTURE = Q=1.",
                "Assumes Gaussian profile. For other profiles, w_min may differ. " +
                "But the existence of a minimum width is robust."),

            new("E: Minimal Energy Configuration",
                "Among all Q=1 configurations, there is a minimum energy state. " +
                "Q=0 (vacuum) has lower energy. Q=1 has finite energy cost " +
                "(nucleation barrier). No configuration with 0<Q<1 exists " +
                "because there is no topological sector between 0 and 1.",
                true,
                "Energy E[Q] = E₀ + Q·ΔE where ΔE is the kink-pair energy. " +
                "E[Q] is linear in Q. No fractional Q → no fractional energy. " +
                "The barrier ΔE > 0 explains why Q=0→Q=1 requires nucleation.",
                "Energy argument depends on Q being integer (mechanism A). " +
                "Does not independently prove minimality."),

            new("F: Combined Mechanism (A+B+D)",
                "Q=1 is minimal because: (A) β₀=1 is the smallest non-zero " +
                "Betti number → Q starts at 1. (B) The kink-antikink pair is " +
                "the minimal closed topological unit. (D) Reaction-diffusion " +
                "balance imposes minimum width w_c → structures narrower than " +
                "w_c are unstable → no sub-Q structures survive.",
                true,
                "COMPLETE: (A) explains discrete spectrum {0,1,2,...}. " +
                "(B) explains why the unit is the kink-pair. " +
                "(D) explains why the unit is STABLE (minimum width). " +
                "Together: Q=1 is the MINIMAL STABLE QUANTUM. " +
                "Q=0 is vacuum. Nothing exists between 0 and 1.",
                "None — this is the complete mechanism."),
        };
    }
}
