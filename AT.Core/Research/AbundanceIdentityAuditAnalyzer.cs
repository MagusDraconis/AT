namespace AT.Core.Research;

/// <summary>
/// Audits the identity/abundance split across all AT results.
/// AT-X065b: Abundance vs Identity Audit
/// </summary>
public static class AbundanceIdentityAuditAnalyzer
{
    public static List<AbundanceIdentityMetrics.ATResult> ClassifyResults()
    {
        return new List<AbundanceIdentityMetrics.ATResult>
        {
            // ===== IDENTITY RESULTS =====
            new("X035", "Q = principle of individuation",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Q IS this principle",
                "The bedrock. Identity itself is what Q IS."),

            new("X036", "Quantum mechanics emerges",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Theorem",
                "QM STRUCTURE (Hilbert, unitary, Schrödinger) is identity, not abundance."),

            new("X040", "Time = partial order of events",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED",
                "WHAT time IS, not its rate."),

            new("X042", "3+1 spacetime dimensions",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED",
                "HOW MANY dimensions — an identity question (integer)."),

            new("X047", "Particles = topological defects",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED",
                "WHAT particles ARE. Existence, not population."),

            new("X048-X050", "Gauge symmetry = Aut(moduli space)",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Theorem",
                "WHAT gauge symmetry IS. Existence, not coupling strength."),

            new("X051", "Three generations exist",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED",
                "HOW MANY generations — an identity question (integer count)."),

            new("X052", "Mass hierarchy exists (geometric)",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Pattern",
                "THAT masses are hierarchical. The pattern is identity."),

            new("X054", "Mixing exists (exponential overlap)",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Pattern",
                "THAT mixing is hierarchical. The structure is identity."),

            new("X056", "SM gauge group preferred",
                AbundanceIdentityMetrics.Category.Identity,
                "STRONG PREFERENCE",
                "WHICH gauge group — an identity question."),

            new("X059", "Neutrino = delocalized defect",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Mechanism",
                "WHAT neutrinos ARE and WHY they're different."),

            new("X060", "Normal ordering (m1<m2<m3)",
                AbundanceIdentityMetrics.Category.Identity,
                "STRONG PREFERENCE",
                "WHICH ordering — an identity question (binary choice)."),

            new("X060e", "U(1) exists (theorem)",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED — Theorem",
                "THAT U(1) exists. Existence, not coupling value."),

            new("X064", "DM = neutral topological defects",
                AbundanceIdentityMetrics.Category.Identity,
                "DERIVED",
                "WHAT dark matter IS. Identity, not abundance."),

            // ===== ABUNDANCE RESULTS =====
            new("X053", "Anharmonicity a(d) = a0*(1+γ(d-1))",
                AbundanceIdentityMetrics.Category.Mixed,
                "PATTERN DERIVED, VALUES CONTINGENT",
                "Functional form = identity. Specific a0,γ = abundance."),

            new("X055", "Fine-structure constant α ≈ 1/137",
                AbundanceIdentityMetrics.Category.Abundance,
                "WEAKLY CONSTRAINED",
                "HOW STRONG is EM. A continuous 'how much' question."),

            new("X057", "Absolute mass scale (m_e = 0.511 MeV)",
                AbundanceIdentityMetrics.Category.Abundance,
                "CONTINGENT",
                "HOW HEAVY. One measurement required. Pure abundance."),

            new("X058", "Correlation length ξ",
                AbundanceIdentityMetrics.Category.Abundance,
                "WEAKLY CONSTRAINED",
                "HOW LARGE is the defect. A continuous scale question."),

            new("X060d", "Nonlinearity parameter M²",
                AbundanceIdentityMetrics.Category.Abundance,
                "CONTINGENT",
                "HOW NONLINEAR. The one continuous parameter."),

            new("X065", "Ω_DM ≈ 0.27",
                AbundanceIdentityMetrics.Category.Abundance,
                "CONTINGENT",
                "HOW MUCH dark matter. Initial conditions."),

            new("X046", "Λ ≈ H² (cosmological constant)",
                AbundanceIdentityMetrics.Category.Abundance,
                "DERIVED — Order of magnitude",
                "The functional form Λ~H² is derived. Exact value fluctuates.\n"
                + "THIS IS THE EXCEPTION — an abundance question partially derived."),
        };
    }

    public static AbundanceIdentityMetrics.SplitAnalysis AnalyzeSplit(
        List<AbundanceIdentityMetrics.ATResult> results)
    {
        var identity = results.Where(r => r.Category == AbundanceIdentityMetrics.Category.Identity).ToList();
        var abundance = results.Where(r => r.Category == AbundanceIdentityMetrics.Category.Abundance).ToList();
        var mixed = results.Where(r => r.Category == AbundanceIdentityMetrics.Category.Mixed).ToList();

        int idDerived = identity.Count(r => r.DerivationStatus.Contains("DERIVED"));
        int abDerived = abundance.Count(r => r.DerivationStatus.Contains("DERIVED"));

        double idRate = (double)idDerived / identity.Count;
        double abRate = (double)abDerived / abundance.Count;

        string pattern = $"Identity success rate: {idRate:P0}. Abundance success rate: {abRate:P0}.";
        var status = idRate > 0.8 && abRate < 0.3
            ? AbundanceIdentityMetrics.SplitStatus.FundamentalSplit
            : idRate > abRate + 0.2
            ? AbundanceIdentityMetrics.SplitStatus.StrongDistinction
            : AbundanceIdentityMetrics.SplitStatus.WeakDistinction;

        string verdict = status switch
        {
            AbundanceIdentityMetrics.SplitStatus.FundamentalSplit =>
                "FUNDAMENTAL SPLIT DISCOVERED. AT is a theory of IDENTITY.\n"
                + "It tells you WHAT exists and WHY, but not HOW MUCH.\n"
                + "Topology determines identities (existence, structure, patterns).\n"
                + "Initial conditions determine abundances (masses, couplings, densities).\n"
                + "This is not a failure — it's a CLASSIFICATION of physical questions.\n"
                + "All of AT's greatest successes are identity questions.\n"
                + "Almost all 'failures' are abundance questions.\n"
                + "THIS IS THE DEEPEST META-RESULT OF THE AT PROGRAM.",
            _ => "Split detected but not fundamental."
        };

        return new AbundanceIdentityMetrics.SplitAnalysis(
            identity.Count, abundance.Count,
            idDerived, abDerived, idRate, abRate,
            pattern, status, verdict);
    }

    public static string TheTwoLayers()
    {
        return @"
THE TWO-LAYER ONTOLOGY OF AT

═══════════════════════════════════════════════════════════════
  LAYER 1: IDENTITY — TOPOLOGY DETERMINES WHAT EXISTS
═══════════════════════════════════════════════════════════════

  Questions answered:     What? Why? How many (discrete)?
  Method:                 Topological invariants, moduli spaces.
  Status:                 LARGELY DERIVED.

  Examples:
    ✓ Particles exist (topological defects).
    ✓ Gauge symmetries exist (Aut of moduli spaces).
    ✓ U(1) exists (S¹ moduli → Aut = U(1)).
    ✓ 3 generations (excitation spectrum stability cutoff).
    ✓ Mass hierarchy pattern (anharmonic WKB).
    ✓ Mixing structure (wavefunction overlap → exponential).
    ✓ Dark matter identity (neutral defects).
    ✓ 3+1 dimensions (complexity maximization).

  Identity results are DERIVED because they depend on
  TOPOLOGICAL STRUCTURE, which is determined by the
  fundamental principles (Q, Randomness, M²).

═══════════════════════════════════════════════════════════════
  LAYER 2: ABUNDANCE — HISTORY DETERMINES HOW MUCH
═══════════════════════════════════════════════════════════════

  Questions answered:     How much? How strong? How heavy?
  Method:                 Measured from one scale; rest computed.
  Status:                 CONTINGENT (except ratios).

  Examples:
    ~ Absolute mass scale (m_e) — one measurement.
    ~ Coupling strengths (α) — weakly constrained.
    ~ Dark matter abundance (Ω_DM) — initial conditions.
    ~ Nonlinearity M² — one continuous parameter.
    ~ Correlation length ξ — one scale sets all.

  Abundance results are CONTINGENT because they depend on
  INITIAL CONDITIONS and COSMOLOGICAL HISTORY, which are
  not determined by the fundamental principles alone.

═══════════════════════════════════════════════════════════════
  THE ASYMMETRY IS NOT A BUG — IT'S A FEATURE
═══════════════════════════════════════════════════════════════

  Any theory of physics must contain BOTH:
    • Universal necessary structure (topology → identity).
    • Contingent specific values (history → abundance).

  AT EXPLAINS why identity questions are answerable and
  abundance questions are not: because topology is universal
  but initial conditions are contingent.

  The Standard Model confounds these — treating Yukawa couplings
  (abundance) as fundamental parameters. AT SEPARATES them:
  topology determines the PATTERN of masses; history determines
  the SCALE.
";
    }
}
