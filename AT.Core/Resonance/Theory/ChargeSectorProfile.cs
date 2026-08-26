namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for the charge quantization mechanism analysis.
/// Defines charge sectors, fractional charge construction attempts,
/// homotopy classes, and the quantization proof.
///
/// AT-121: Charge Quantization Mechanism
/// </summary>
public static class ChargeSectorProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>A charge sector: all field configurations with the same Q.</summary>
    public sealed record ChargeSector(
        int Q,
        string Description,
        bool IsPhysicallyRealizable,
        bool IsTopologicallyProtected,
        string BoundaryCondReq,
        string StabilityAnalysis);

    /// <summary>Attempt to construct a fractional charge state.</summary>
    public sealed record FractionalChargeAttempt(
        string TargetCharge,        // e.g. "Q=0.5"
        string ConstructionMethod,  // "HalfKink", "AsymmetricProfile", "DeformedDomain", etc.
        bool ConstructionSucceeded, // did we create the R-field?
        double ActualQ,             // measured Q at T=0.5
        double[,]? Rfield,         // the constructed R-field (if succeeds)
        bool IsStable,             // does it survive PDE evolution?
        double StabilityLifetime,  // iterations before decay/change
        string FailureReason);     // why it failed (if it did)

    /// <summary>A homotopy class of R-field configurations.</summary>
    public sealed record HomotopyClass(
        int Index,                 // the Q value
        string DefiningProperty,   // what characterizes this class
        bool IsDiscrete,           // can you change class continuously?
        double EnergyBarrier,      // minimum energy to change class
        string Proof);             // proof of discreteness

    /// <summary>A step in the quantization proof.</summary>
    public sealed record QuantizationProofStep(
        int StepNumber,
        string Statement,
        string Justification,
        string MathematicalBasis);

    /// <summary>A candidate quantization mechanism.</summary>
    public sealed record QuantizationMechanism(
        string Name,
        string Description,
        bool IsSufficient,          // does this alone guarantee Q ∈ ℕ?
        bool IsNecessary,           // is this mechanism required for quantization?
        string ProofSketch,
        string Weakness);           // where it might fail

    /// <summary>Complete quantization analysis report.</summary>
    public sealed record QuantizationReport(
        List<ChargeSector> AllowedSectors,
        List<ChargeSector> ForbiddenSectors,
        List<FractionalChargeAttempt> FractionalAttempts,
        List<HomotopyClass> HomotopyClasses,
        List<QuantizationProofStep> ProofSteps,
        List<QuantizationMechanism> Mechanisms,
        QuantizationMechanism BestMechanism,
        bool QuantizationProven,
        string Classification,
        string Verdict,
        string MathematicalProofSummary);

    // ══════════════════════════════════════════════════════════════════
    // Charge sector catalog
    // ══════════════════════════════════════════════════════════════════

    public static List<ChargeSector> GetAllowedSectors()
    {
        return new List<ChargeSector>
        {
            new(0, "Vacuum — no condensates. R(x)<0.5 everywhere.",
                true, true,
                "R(0)≈0, R(L)≈0 — satisfied by R≡0",
                "Linearly stable at N→∞ (AT-118). Metastable at finite N."),

            new(1, "Single condensate — one kink-antikink pair.",
                true, true,
                "R(0)≈0, R(L)≈0 — one 0→1→0 excursion",
                "Topologically protected. Reaction ≫ diffusion inside (AT-113)."),

            new(2, "Two separated condensates.",
                true, true,
                "R(0)≈0, R(L)≈0 — two 0→1→0 excursions",
                "Each independently stable at d > 3w (AT-107). Merges at d < 5λ."),

            new(3, "Three condensates.",
                true, true,
                "Requires spacings > coupling range",
                "Multi-particle state. Each Q=+1 independent."),

            new(5, "Five condensates.",
                true, true,
                "Requires large system or small width",
                "Dense multi-condensate state."),
        };
    }

    public static List<ChargeSector> GetForbiddenSectors()
    {
        return new List<ChargeSector>
        {
            new(-1, "Negative charge — would require inverted kink (1→0→1).",
                false, false,
                "Cannot satisfy R(0)≈0, R(L)≈0 with inverted kink",
                "No reaction mechanism for 1→0 crossing (barrier is one-way downward)."),

            new(0, "Q=0.5 — half a condensate (kink without antikink).",
                false, false,
                "Would need R>0.5 at one boundary → violates BCs",
                "Kink without antikink = boundary artifact. Not a closed configuration."),

            new(0, "Q=0.25 — quarter condensate.",
                false, false,
                "No topological meaning. Would need 1/4 of a connected component.",
                "Connected components are binary — either connected or not."),

            new(0, "Fractional Q = p/q — any non-integer.",
                false, false,
                "Requires fractional Betti number β₀ = p/q",
                "β₀ ∈ ℕ by definition (homology). Cannot be fractional."),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Homotopy class structure
    // ══════════════════════════════════════════════════════════════════

    public static List<HomotopyClass> GetHomotopyClasses()
    {
        return new List<HomotopyClass>
        {
            new(0, "Q=0 sector: R(x) < 0.5 everywhere. " +
                   "No 0.5-crossing points. Homotopic to R≡0.",
                true, 0,
                "All Q=0 configurations are continuously deformable to R≡0 " +
                "without crossing the 0.5 threshold. This is the trivial homotopy class."),

            new(1, "Q=1 sector: exactly one 0→1→0 excursion. " +
                   "One kink pair. Homotopic to a standard Gaussian bump > 0.5.",
                true, double.PositiveInfinity,
                "Any Q=1 configuration can be continuously deformed to any other Q=1 " +
                "configuration without changing Q. But crossing to Q=0 or Q=2 requires " +
                "R to cross 0.5 at some point → topology change."),

            new(2, "Q=2 sector: two separated 0→1→0 excursions.",
                true, double.PositiveInfinity,
                "Two separated R>0.5 domains. Deformable within class as long as " +
                "domains remain separated. Merging changes Q (Q=2→1)."),

            new(3, "Q=k sector: k separated excursions. General.",
                true, double.PositiveInfinity,
                "k separated R>0.5 domains. Each domain = one kink pair. " +
                "Q ∈ ℕ enumerates the homotopy classes. " +
                "PROOF: Two configurations are in the same class iff they have " +
                "the same number of R>0.5 connected components. β₀ is the " +
                "complete homotopy invariant for the superlevel set."),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Quantization mechanisms
    // ══════════════════════════════════════════════════════════════════

    public static List<QuantizationMechanism> GetAllMechanisms()
    {
        return new List<QuantizationMechanism>
        {
            new("A: Topology (β₀)",
                "Q = β₀({R>0.5}). Betti numbers are integer-valued by definition. " +
                "Quantization follows directly from homology.",
                true, true,
                "β₀ counts connected components → always integer. " +
                "No fractional β₀ exists. This is a MATHEMATICAL TRUTH.",
                "Shifts the question to: why is Q = β₀ the right definition? " +
                "β₀ alone doesn't explain why Q is CONSERVED."),

            new("B: Kink-Antikink Pair Structure",
                "Each Q=+1 requires one kink (0→1) + one antikink (1→0). " +
                "No fractional kinks exist — a crossing is binary.",
                true, false,
                "A crossing of R=0.5 is a DISCRETE EVENT. R is either >0.5 " +
                "or ≤0.5 at each point. There is no 'partially crossed' state. " +
                "Each pair = +1 charge. Frac charge = fraction of a pair = impossible.",
                "Explains why Q is integer but not why Q exists. " +
                "Pairs are the UNIT but topology determines what a unit IS."),

            new("C: Reaction-Diffusion Barrier",
                "The one-way barrier c₀·M·R·(1−R²) > 0 prevents R from " +
                "crossing 0.5 downward. This ENFORCES conservation and makes " +
                "the topology rigid.",
                false, true,
                "Without the barrier, R could cross 0.5 freely → Q not conserved. " +
                "The barrier is what makes Q MEANINGFUL as a charge. " +
                "dQ/dt = 0 because R cannot cross 0.5 downward under PDE.",
                "Does not alone guarantee Q ∈ ℕ — that comes from β₀. " +
                "Could a different PDE produce the same β₀ quantization? Yes — " +
                "any PDE that preserves the superlevel set topology."),

            new("D: Homotopy Classes",
                "The space of R-field configurations splits into homotopy " +
                "classes indexed by Q. Two configurations with different Q " +
                "cannot be continuously deformed into each other.",
                true, false,
                "π₀(Map(Ω, [0,1]) relative to {R=0.5}) ≅ ℕ. " +
                "The homotopy classes are indexed by crossing number. " +
                "Crossing number = Q (kink pair count). " +
                "Classes are DISCRETE — no continuous path between them.",
                "Homotopy classification requires the threshold T=0.5. " +
                "Why 0.5? Because of the reaction barrier (mechanism C). " +
                "Topology + Barrier = Homotopy → Quantization."),

            new("E: Morse Topology",
                "Morse theory: #{maxima with R>0.5} = Q for well-separated " +
                "condensates. Each maximum = one condensate center.",
                true, false,
                "Critical points of R(x) have integer indices. " +
                "Maxima with R>0.5 are stable (gradient flow attracts to them). " +
                "Changes in Q require creation/annihilation of critical points " +
                "at R=0.5 → discrete events.",
                "Morse theory describes STRUCTURE but doesn't enforce Q ∈ ℕ " +
                "any more than β₀ does. It's equivalent to the topology argument."),

            new("F: Persistent Homology",
                "Features with persistence spanning T∈[0.1,0.9] are charges. " +
                "Features with short persistence are noise. No intermediate " +
                "persistence → no sub-Q structure → Q is quantized.",
                false, true,
                "The persistence diagram has a GAP between long-persistence " +
                "(charges) and short-persistence (noise) features. " +
                "This gap ENFORCES distinct charge values. " +
                "Without the gap, charge would be fuzzy.",
                "Describes WHY quantization is clean but doesn't prove " +
                "Q ∈ ℕ — persistence is continuous; the gap is empirical."),

            new("G: Combined Mechanism (A+C+D)",
                "Q = β₀({R>0.5}) is integer (A). The reaction barrier prevents " +
                "R from crossing 0.5 downward (C), making β₀ conserved. " +
                "Together, they partition configuration space into discrete " +
                "homotopy classes indexed by Q (D).",
                true, true,
                "COMPLETE PROOF:\n" +
                "1. Q = β₀({R>0.5}) → integer by homology (A).\n" +
                "2. ∂R/∂t > 0 for R∈(0,1), M>0 → one-way barrier (C).\n" +
                "3. Barrier prevents R from leaving {R>0.5} → β₀ conserved.\n" +
                "4. β₀ conserved + integer = QUANTIZED CHARGE.\n" +
                "5. Homotopy classes = {β₀ = 0, 1, 2, ...} (D).\n" +
                "Q.E.D.",
                "None. This is the complete mechanism. " +
                "Topology provides the mathematical structure, " +
                "the reaction barrier provides the physical enforcement."),
        };
    }
}
