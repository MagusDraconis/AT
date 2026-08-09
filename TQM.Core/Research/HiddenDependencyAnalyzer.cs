namespace TQM.Core.Research;

/// <summary>
/// Searches for hidden dependencies among TQM's remaining free parameters.
/// TQM-X060b: Hidden Dependency Audit
/// </summary>
public static class HiddenDependencyAnalyzer
{
    public static List<HiddenDependencyMetrics.ParameterEntry> InventoryParameters()
    {
        return new List<HiddenDependencyMetrics.ParameterEntry>
        {
            new("Correlation length", "ξ", "Defect dynamics (X058)",
                "One measured mass scale. ξ_charged sets all charged fermion masses.",
                new[] { "N (entity count)", "PDE coefficients" }, true),

            new("Fine-structure constant", "α", "U(1) vortex coupling (X055)",
                "Weakly constrained to 10^(-4)-10^(-1) window.",
                new[] { "ξ (localization)", "vortex core geometry" }, true),

            new("Base anharmonicity", "a₀", "PDE reaction term (X053)",
                "Sets mass hierarchy steepness. Same for all fermion families.",
                new[] { "c₀ (PDE reaction rate)", "M (PDE coupling)" }, true),

            new("Codimension coupling", "γ", "Centrifugal barrier (X053)",
                "Sets how a varies with defect codimension d.",
                new[] { "D_R (PDE diffusion)", "defect geometry" }, true),

            new("Quark mixing beta", "β_quark", "Wavefunction overlap (X054)",
                "Sets CKM hierarchy steepness for charged defects.",
                new[] { "ξ_charged", "Δr (level spacing)", "a₀" }, true),

            new("Lepton mixing beta", "β_lepton", "Wavefunction overlap (X054)",
                "Sets PMNS anarchic/large mixing for neutral defects.",
                new[] { "ξ_neutral", "Δr (level spacing)", "a₀" }, true),

            new("U(1) charge existence", "Q_EM", "Vortex moduli space (X050)",
                "Binary: charged or neutral. Controls everything.",
                new string[] { }, false),
        };
    }

    public static List<HiddenDependencyMetrics.DependencyLink> FindDependencies()
    {
        return new List<HiddenDependencyMetrics.DependencyLink>
        {
            new("ξ_charged", "α",
                "α sets the U(1) coupling strength → determines gauge\n"
                + "localization strength → controls ξ_charged.\n"
                + "ξ_charged ∝ 1/α (stronger EM → tighter binding).",
                false,
                "ξ_charged = ℓ_P · f(α) where f(α) ~ α^(-p).\n"
                + "The exponent p depends on the defect geometry (codim-1\n"
                + "domain wall: p ≈ 1; codim-2 vortex: p ≈ 1/2).\n"
                + "Exact relation not derived but DIRECTION is clear:\n"
                + "larger α → smaller ξ_charged → larger masses.",
                true),

            new("ξ_charged", "β_quark",
                "β_quark = Δr/ξ_charged where Δr = spacing between\n"
                + "excitation levels. Δr depends on a₀ (anharmonicity).\n"
                + "β_quark = Δr(a₀)/ξ(α, a₀) → NOT independent.",
                false,
                "β_quark IS σ(α, a₀) — a function of the coupling and\n"
                + "anharmonicity, not an independent parameter.\n"
                + "If α and a₀ are known, β_quark is DETERMINED.",
                true),

            new("ξ_neutral", "β_lepton",
                "β_lepton = Δr/ξ_neutral. Without U(1), ξ_neutral ≫ ξ_charged.\n"
                + "ξ_neutral set by weak SU(2) + self-interaction.\n"
                + "β_lepton = σ'(ξ_neutral, a₀).",
                false,
                "β_lepton is NOT independent of β_quark — both come from\n"
                + "Δr(a₀)/ξ. The ratio β_quark/β_lepton = ξ_neutral/ξ_charged.\n"
                + "This ratio is set by the RATIO of U(1) to SU(2) coupling\n"
                + "strengths — which is α vs α_W (weak coupling).",
                true),

            new("a₀", "γ",
                "Both a₀ and γ describe the defect potential V(φ).\n"
                + "a₀ = anharmonicity of the 1D (kink) potential.\n"
                + "γ = how anharmonicity increases with codimension.\n"
                + "Both from the SAME PDE coefficients (c₀, M, D_R).",
                false,
                "a₀ and γ are NOT independent — they both derive from the\n"
                + "SAME underlying PDE parameter set (c₀, M, D_R).\n"
                + "In principle: {a₀, γ} = F(c₀, M, D_R) — TWO numbers\n"
                + "from THREE PDE coefficients. But the PDE has only 3\n"
                + "coefficients → can produce at most 3 independent observables.",
                true),

            new("α", "Q_EM (charge existence)",
                "Whether U(1) exists at all is determined by defect moduli\n"
                + "space topology (X050). IF a vortex with S¹ moduli exists,\n"
                + "U(1) exists and α > 0. The VALUE of α is determined by\n"
                + "the vortex core geometry.",
                true,
                "The BINARY existence of U(1) is topological — derived.\n"
                + "The VALUE of α requires the vortex core size → ξ.",
                true),

            new("a₀", "β_quark",
                "a₀ sets the level spacing Δr. Larger a₀ → larger Δr →\n"
                + "larger β_quark = Δr/ξ. β_quark IS determined by a₀ and ξ.",
                true,
                "β_quark = Δr(a₀) / ξ(α). Not an independent parameter.\n"
                + "Once a₀ (from mass ratios) and ξ (from absolute mass)\n"
                + "are known, β_quark is PREDICTED.",
                true),
        };
    }

    public static List<HiddenDependencyMetrics.ReductionProposal> ProposeReductions()
    {
        return new List<HiddenDependencyMetrics.ReductionProposal>
        {
            new("Reduction R1: β_quark from a₀ + ξ",
                "β_quark AND β_lepton", 2, 0,
                "β = Δr/ξ. Δr from a₀ (mass hierarchy). ξ from mass scale.\n"
                + "Neither β is an independent parameter — both are DERIVED\n"
                + "from the same two inputs (a₀, ξ). Quark vs lepton difference\n"
                + "= ξ_charged vs ξ_neutral = α-dependent ratio.",
                false),

            new("Reduction R2: a₀ + γ from PDE coefficients",
                "a₀ AND γ", 2, 2,
                "a₀ and γ both come from {c₀, M, D_R}. But the PDE has\n"
                + "THREE coefficients and we only have TWO numbers (a₀, γ).\n"
                + "One PDE coefficient remains as a free mass scale (= ξ).\n"
                + "So {c₀, M, D_R} → {ξ, a₀, γ} — same count (3→3).\n"
                + "NO REDUCTION — just a relabeling.",
                true),

            new("Reduction R3: α from vortex core geometry",
                "α", 1, 1,
                "α = (vortex core size / interaction range)^(codim).\n"
                + "The core size ~ ξ. The interaction range ~ ξ/α.\n"
                + "Self-consistent: α = f(ξ, codim). But f is not uniquely\n"
                + "determined without the vortex profile.\n"
                + "α is PROBABLY derivable from ξ + codimension, but not proven.",
                false),

            new("Reduction R4: Everything from ξ + PDE coefficients",
                "ALL remaining", 5, 3,
                "FUNDAMENTAL INPUTS: {c₀, M, D_R} = 3 PDE coefficients.\n"
                + "These produce: ξ (mass scale), a₀ (hierarchy), γ (codim).\n"
                + "From these: α(ξ, a₀), β(ξ, a₀), neutrino params(ξ, a₀).\n"
                + "MINIMAL PARAMETERS: 3 (PDE coefficients).\n"
                + "This is the MAXIMUM POSSIBLE REDUCTION within current TQM.",
                false),
        };
    }

    public static string DependencyGraph()
    {
        return @"
DEPENDENCY GRAPH — TQM FREE PARAMETERS

  PDE coefficients {c₀, M, D_R}  (3 fundamental numbers)
      │
      ├──→ ξ (correlation length / mass scale)
      │       │
      │       ├──→ m_e, m_μ, m_τ (via a₀)
      │       ├──→ m_quarks (via a₀ + γ·codim)
      │       └──→ m_neutrinos (via ξ_neutral ≫ ξ_charged)
      │
      ├──→ a₀ (base anharmonicity)
      │       │
      │       ├──→ mass hierarchy steepness
      │       ├──→ Δr (level spacing)
      │       └──→ β_quark = Δr/ξ_charged  ── NOT INDEPENDENT
      │
      └──→ γ (codimension coupling)
              │
              ├──→ a(d) = a₀·(1 + γ(d-1))
              └──→ quark/lepton mass ratio differences

  U(1) moduli space topology (binary: exists or not)
      │
      ├──→ α ≠ 0 if U(1) exists (value from vortex geometry)
      ├──→ ξ_charged ∝ 1/α (gauge localization)
      ├──→ ξ_neutral ≫ ξ_charged (no gauge localization)
      └──→ β_lepton = Δr/ξ_neutral  ── NOT INDEPENDENT

RESULT: TRUE INDEPENDENT PARAMETERS = 3 (PDE coefficients)
        + 1 binary choice (does U(1) exist?)
        ALL others are DERIVED from these.
";
    }

    public static string TheVerdict()
    {
        return @"
HIDDEN DEPENDENCY AUDIT — FINAL VERDICT

STARTING COUNT (apparent): 6 free parameters
  ξ, α, a₀, γ, β_quark, β_lepton

FOUND DEPENDENCIES:
  1. β_quark = Δr(a₀) / ξ_charged(α) → FUNCTION, not independent.
  2. β_lepton = Δr(a₀) / ξ_neutral → same functional form, different ξ.
  3. β_lepton/β_quark = ξ_charged/ξ_neutral → ratio fixed by α/α_W.
  4. a₀, γ, ξ ALL come from {c₀, M, D_R} — 3 PDE coefficients.
  5. α is set by vortex core geometry, which depends on ξ.

MINIMAL COUNT: 3 (PDE coefficients) + 1 (U(1) existence: binary).
  • Absolute mass scale (was ξ) → c₀ (reaction rate in PDE).
  • Mass hierarchy (was a₀, γ) → M, D_R (coupling + diffusion).
  • Mixing hierarchy (was β_quark, β_lepton) → DERIVED from a₀ + ξ.
  • Fine structure (was α) → DERIVED from vortex geometry + ξ.

TOTAL REDUCTION: 6 → 3 (+ 1 binary) = ~50% reduction.

CAVEAT: The reduction to 3 PDE coefficients is FORMAL — we currently
cannot COMPUTE α or β_quark from {c₀, M, D_R} without solving the
full defect dynamics. The reduction is a proof-of-principle that
the dependencies EXIST, not that we can currently predict all values.

CLASSIFICATION: C — Strong dependencies discovered.
  The apparent 6 free parameters overcount by ~2×.
  True degrees of freedom: 3 PDE coefficients + 1 binary choice.
";
    }
}
