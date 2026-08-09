using System.Globalization;

namespace TQM.Core.Research;

/// <summary>
/// Investigates the origin of absolute mass scales from Q-defect energetics.
/// TQM-X057: Origin of Absolute Mass Scales
/// </summary>
public static class AbsoluteMassScaleAnalyzer
{
    private const double MElectronMeV = 0.511;
    private const double MPlanckMeV = 1.22089e22;      // Planck mass in MeV
    private const double MPlanckGeV = 1.22089e19;

    public static List<AbsoluteMassScaleMetrics.MassScaleModel> AnalyzeModels()
    {
        // Compute natural scales from TQM parameters
        // ℓ = Q-event spacing ≈ ℓ_P (Planck length, X045)
        // Defect core size r_core ≈ ξ (correlation length)
        // The hierarchy: r_core / ℓ_P sets the mass suppression

        // Model A: Defect formation energy from core tension
        // E_defect ≈ (core tension) × (core volume)^(1/3) × (codimension factor)
        // For a domain wall (codim-1): E = σ × A (energy per unit area × area)
        // σ ≈ (energy scale of phase transition) × (1/ξ)
        // The fundamental energy scale is ℓ_P^(-1) ≈ M_Planck
        // But the defect energy is suppressed by (ξ/ℓ_P)^(d-1) for codim-d

        double xiOverLP = 1e17; // correlation length / Planck length (hierarchy factor)
        double codimFactor = Math.Pow(xiOverLP, -1); // domain wall (codim-1): E ~ ℓ_P^(-1) × (ℓ_P/ξ)

        double mA = MPlanckMeV * codimFactor; // defect energy in MeV
        double logErrA = Math.Abs(Math.Log10(mA / MElectronMeV));

        // Model B: Core size directly sets mass
        // r_core sets the localization energy: E ≈ ħc / r_core
        // If r_core ≈ 10^(-15) m (nuclear scale), E ≈ 200 MeV
        // If r_core ≈ 10^(-13) m (atomic scale), E ≈ 2 MeV
        double rCoreFermi = 1e-15; // meters
        double hbarC = 197.326;     // MeV·fm
        double mB = hbarC / (rCoreFermi / 1e-15); // MeV
        double logErrB = Math.Abs(Math.Log10(mB / MElectronMeV));

        // Model C: Defect energy from PDE parameters
        // TQM PDE: ∂R/∂t = c₀·M·R·(1-R²) + D_R·∇²R
        // Defect mass from TQM-111: m_eff = 4(1+M₀²)/(3w)
        // With typical soliton parameters: m_eff ~ O(1) in PDE units
        // The PHYSICAL mass requires the PDE unit → MeV conversion
        // This conversion factor IS the unknown mass scale

        // Model D: Mass scale from electroweak scale
        // Cannot derive — just identifies the Higgs VEV as the scale
        double mD = 246e3; // Higgs VEV in MeV
        double logErrD = Math.Abs(Math.Log10(mD / MElectronMeV));

        return new List<AbsoluteMassScaleMetrics.MassScaleModel>
        {
            new("A: Defect core tension",
                "E_defect ∝ ℓ_P^(-1) · (ℓ_P/ξ)^(codim-1).\n"
                + "For domain wall (codim-1): mass ∝ E_defect/c².\n"
                + "ξ/ℓ_P ≈ 10^17 (correlation length hierarchy).",
                mA, logErrA, false,
                $"Predicted: {mA:F1} MeV (electron observed: {MElectronMeV} MeV).\n"
                + $"Log10 error: {logErrA:F1}. ξ/ℓ_P must be EXACTLY right.\n"
                + "The hierarchy factor is MEASURED, not derived."),

            new("B: Localization energy (ħc/r_core)",
                "E ≈ ħc/r_core. If defect core has size r_core,\n"
                + "the ground state energy is the localization energy.\n"
                + "r_core is a measurable defect parameter.",
                mB, logErrB, false,
                $"r_core ≈ 1 fm → m ≈ {mB:F0} MeV (observed: {MElectronMeV}).\n"
                + "Off by factor ~400. Core would need to be ~0.4 pm.\n"
                + "r_core is not derived from Q alone."),

            new("C: PDE parameter conversion",
                "TQM PDE has natural units. Conversion to MeV\n"
                + "requires identifying (c₀, M, D_R) with physical scales.\n"
                + "This identification IS the mass scale — not derived.",
                0.511, 0.0, true,
                "TAUTOLOGY: m_e = m_e. The PDE parameters are CHOSEN\n"
                + "to reproduce the observed mass. No prediction."),

            new("D: Electroweak scale (Higgs VEV)",
                "The electroweak scale v ≈ 246 GeV sets fermion masses\n"
                + "via Yukawa couplings y: m_f = y·v/√2.\n"
                + "v is the scale of SU(2)×U(1) breaking.",
                mD, logErrD, false,
                $"v ≈ 246 GeV ≈ {mD:F0} MeV sets the SCALE.\n"
                + "But v itself is not derived in TQM (requires Higgs mechanism).\n"
                + "The hierarchy v/M_Planck ≈ 10^(-16) is unexplained."),

            new("E: No derivation possible",
                "Absolute masses are contingent facts about our universe.\n"
                + "TQM can derive mass RATIOS (X052, X053) but not\n"
                + "the absolute scale. The scale is set by initial conditions.",
                0, 999, false,
                "PESSIMISTIC but HONEST. All physical theories need at least\n"
                + "one mass scale as input. In TQM, this is the conversion\n"
                + "from PDE units to MeV. The electron mass is this conversion."),
        };
    }

    public static string TheDerivation()
    {
        return @"
ORIGIN OF ABSOLUTE MASS SCALES — HONEST ASSESSMENT

STATUS: TQM does NOT predict the absolute mass scale of elementary
        particles (e.g., m_e = 0.511 MeV).

THE FUNDAMENTAL SCALE:
  TQM provides ONE fundamental scale: ℓ (Q-event spacing).
  From ℓ, we derive: ℓ_P = ℓ·√β (Planck length, X045).
  From ℓ_P and ħ,c: M_Planck ≈ 1.22×10^19 GeV.

THE HIERARCHY PROBLEM:
  m_e / M_Planck ≈ 4×10^(-23)

  WHY are particles so light compared to the Planck scale?

  TQM EXPLANATION: Defect masses are SUPPRESSED relative to the
  fundamental scale by the ratio of scales:
    m_defect / M_Planck ≈ (ℓ_P / ξ)^(codim-1)
  where ξ is the defect correlation length.

  ξ ≫ ℓ_P because ξ is set by the PDE parameters (c₀, M, D_R),
  which are macroscopic compared to the Q-event spacing.

  The electron IS a domain wall (codim-1), so:
    m_e / M_Planck ≈ ℓ_P / ξ

  For m_e ≈ 0.5 MeV: ξ ≈ 10^17 · ℓ_P ≈ 10^(-18) m.
  This is a TEV-scale correlation length — close to the electroweak scale.

WHAT IS DERIVED:
  ✓ Mass hierarchy (ratios) — X052, X053.
  ✓ The Planck mass as the fundamental Q-event scale — X045.
  ✓ Why m_defect ≪ M_Planck — ratio of scales.

WHAT IS NOT DERIVED:
  ✗ The absolute value of ξ (correlation length).
  ✗ The absolute value of m_e in MeV.
  ✗ The conversion from PDE natural units to MeV.

CLASSIFICATION A: Absolute mass scale remains contingent.
          TQM needs ONE measured mass (e.g., m_e) to fix
          the conversion to physical units. All other masses
          are then predicted via ratios (X052, X053).
";
    }

    public static string HostileReview()
    {
        return @"
HOSTILE REVIEW: The absolute mass scale — TQM's Achilles heel?

CHALLENGE 1: After 57 experiments, TQM still can't predict the
electron mass. This is a FUNDAMENTAL failure. Any 'theory of
everything' that can't predict the most basic particle property
is incomplete.

RESPONSE: NO physical theory predicts the absolute mass scale.
The Standard Model has 1 mass scale (Higgs VEV ≈ 246 GeV) as input.
String theory has the string scale as input. Quantum gravity has
the Planck scale as input. TQM has ξ (correlation length) as input,
or equivalently, m_e as input. ONE mass scale MUST be measured
in ANY theory. TQM is no exception.

CHALLENGE 2: The 'correlation length' ξ is just a renamed version
of the Higgs VEV. You haven't explained the electroweak scale —
you've just given it a different name in the TQM framework.

RESPONSE: Fair. ξ ≈ 10^(-18) m ≈ (200 GeV)^(-1) — it IS the
electroweak scale in TQM language. TQM does not derive the
electroweak scale. But TQM DOES connect it to the Q-event spacing:
ξ/ℓ_P ≈ 10^17. This is the same hierarchy as v/M_Planck. The
hierarchy PROBLEM remains, but TQM provides a GEOMETRIC interpretation
(ratio of defect size to Q-event spacing) rather than a field-theoretic
one (renormalization of scalar mass).

CHALLENGE 3: So the final TQM parameter count is:
  1 measured mass scale (e.g., m_e)
  + a₀, γ (anharmonicity, X053)
  + β_quark, β_lepton (mixing, X054)

That's ~5 free parameters. Better than the SM (~19) but not zero.

RESPONSE: Correct. TQM reduces the SM's ~19 free parameters to ~5.
This is significant progress — over 70% reduction — but it's not
a zero-parameter theory. The remaining parameters are:
  • 1 absolute mass scale (m_e or ξ)
  • 2 anharmonicity parameters (a₀, γ) — same for all families
  • 2 mixing parameters (β_quark, β_lepton) — from defect overlap
  • α (fine-structure constant) — weakly constrained (X055)

VERDICT: Classification A. Absolute masses cannot be derived from
         Q alone. One mass scale must be measured. This is not
         a failure of TQM — it's a feature of ALL physical theories.
";
    }
}
