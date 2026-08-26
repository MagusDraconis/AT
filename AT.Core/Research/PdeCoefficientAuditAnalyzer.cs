using System.Globalization;

namespace AT.Core.Research;

/// <summary>
/// Nondimensionalizes AT PDE to find true parameter count.
/// AT-X060c: PDE Coefficient Dependency Audit
/// </summary>
public static class PdeCoefficientAuditAnalyzer
{
    public static List<PdeCoefficientMetrics.NondimensionalResult> Nondimensionalize()
    {
        return new List<PdeCoefficientMetrics.NondimensionalResult>
        {
            new("Order parameter R", "1 (dimensionless)",
                "—", "R ∈ [0,1], already dimensionless"),

            new("Time t", "T",
                "τ = 1/(c₀·M)", "t' = t/τ ∈ [0,∞)"),
            new("Space x", "L",
                "L = √(D_R/(c₀·M))", "x' = x/L"),
            new("Reaction term c₀·M", "T⁻¹",
                "τ = 1/(c₀·M)", "Π₁ = τ·c₀·M = 1"),
            new("Diffusion term D_R", "L²T⁻¹",
                "L² = D_R·τ", "Π₂ = τ·D_R/L² = 1"),

            new("c₀ individually", "T⁻¹·M⁻¹??",
                "—", "Cannot separate from M without additional observable"),
            new("M individually", "??",
                "—", "Appears in soliton mass formula m_eff = 4(1+M²)/(3w)"),
            new("D_R individually", "L²T⁻¹",
                "Absorbed into length scale", "Only through L = √(D_R·τ)"),
        };
    }

    public static List<PdeCoefficientMetrics.ReductionStep> ReductionSteps()
    {
        return new List<PdeCoefficientMetrics.ReductionStep>
        {
            new(1, "Define t' = t·(c₀·M)", "c₀·M (reaction rate)",
                2, "Reaction timescale τ = 1/(c₀·M) defines time unit.\n"
                + "PDE becomes: ∂R/∂t' = R·(1-R²) + (D_R/(c₀·M))·∇²R.\n"
                + "Still has D_R/(c₀·M) as a parameter."),

            new(2, "Define x' = x / √(D_R/(c₀·M))", "D_R/(c₀·M) (diffusion length²)",
                1, "Spatial scale L = √(D_R/(c₀·M)) defines length unit.\n"
                + "PDE becomes: ∂R/∂t' = R·(1-R²) + ∇'²R.\n"
                + "NO dimensionless parameters in the PDE! ZERO."),

            new(3, "But: M also appears in soliton mass",
                "M (separately from c₀·M)",
                1, "AT-111: m_eff = 4(1+M²)/(3w).\n"
                + "M² is the RATIO of nonlinear to linear terms.\n"
                + "If M ≫ 1: strongly nonlinear. If M ≪ 1: weakly nonlinear.\n"
                + "M² survives as a dimensionless PHYSICS parameter."),

            new(4, "c₀ sets absolute timescale → mass scale",
                "c₀ (absolute rate)",
                1, "c₀·M = 1/τ sets the time unit. Once τ is chosen,\n"
                + "the absolute mass scale is fixed.\n"
                + "c₀·M IS the mass scale → NOT a free parameter (unit choice)."),
        };
    }

    public static string TheDerivation()
    {
        return @"
NONDIMENSIONALIZATION OF THE AT PDE

THE PDE: ∂R/∂t = c₀·M·R·(1-R²) + D_R·∇²R

STEP 1: Define dimensionless time t' = t/τ with τ = 1/(c₀·M).
        ∂R/∂t' = R·(1-R²) + (D_R/(c₀·M))·∇²R

STEP 2: Define dimensionless space x' = x/L with L² = D_R/(c₀·M).
        ∂R/∂t' = R·(1-R²) + ∇'²R

RESULT: The dimensionless PDE is PARAMETER-FREE.
        ZERO dimensionless parameters in the dynamics.

BUT: M survives in the SOLITON MASS FORMULA:
        m_eff/ρ₀L^d = 4(1+M²)/(3w)  where ρ₀ = energy density scale.

        The energy density scale is ρ₀ = (c₀·M)² (from the potential V(R)).
        The volume scale is L^d with L² = D_R/(c₀·M).
        
        Therefore: m_eff = f(M²) · (c₀·M)² · L^d
                        = f(M²) · (c₀·M)² · (D_R/(c₀·M))^(d/2)
                        = f(M²) · (c₀·M)^(2-d/2) · D_R^(d/2)

        In 3+1D (d=3): m_eff = f(M²) · (c₀·M)^(1/2) · D_R^(3/2)

        Only M² survives as a PHYSICAL dimensionless parameter.
        The combination (c₀·M)^(1/2)·D_R^(3/2) is the mass scale
        → set by choosing units (one measurement).

SURVIVING PARAMETERS:
  1 dimensionless: M² (nonlinearity strength)
  1 mass scale: (c₀·M)^(1/2)·D_R^(3/2) → 1 measured mass

TOTAL: 3 PDE coefficients → 1 dimensionless + 1 scale = effectively 1+1.
       But the 1 scale is the ABSOLUTE MASS SCALE, which every theory needs.
       So: 1 dimensionless parameter (M²) survives.
";
    }

    public static string HostileReview()
    {
        return @"
HOSTILE REVIEW: Is this reduction valid?

CHALLENGE 1: 'c₀·M defines the time unit' is a tautology.
You can ALWAYS define units to eliminate parameters.
That doesn't mean the parameters don't exist — it means
you've hidden them in the unit definitions.

RESPONSE: The distinction between 'unit conventions' and
'physical parameters' is precisely what the Buckingham-Π
theorem formalizes. A physical theory has N parameters with
K independent dimensions → N-K dimensionless Π-groups survive.
For the AT PDE: {c₀·M [1/T], D_R [L²/T]} → K=2 (L, T)
→ N=2, K=2 → N-K=0 dimensionless groups. The PDE itself
has ZERO dimensionless parameters.

CHALLENGE 2: But M appears separately from c₀·M in the soliton
mass formula. You need c₀ AND M separately, not just c₀·M.

RESPONSE: Correct. In the SOLITON MASS observable, M appears
as M² specifically. The PDE dynamics use only c₀·M, but the
ENERGY of a configuration uses M separately. So the true
parameter count is:
  {c₀·M, D_R} → governing PDE dynamics
  {M²} → soliton mass (nonlinearity observable)

But c₀·M and D_R define the unit system (time and length).
M² is ONE dimensionless physical parameter.
The mass scale (c₀·M)^(1/2)·D_R^(3/2) in 3D is set by
measuring ONE mass → not a 'free parameter,' just unit choice.

CHALLENGE 3: So the claim is: the ENTIRE AT physics depends on
exactly ONE dimensionless number: M²?

RESPONSE: After nondimensionalization and measuring one mass
to set the mass scale: YES. M² controls:
  • How nonlinear the system is (soliton stability).
  • The ratio of soliton mass to the natural mass scale.
  • The anharmonicity (larger M² → larger a₀).
  • Through these, ALL mass ratios and mixing parameters.

Whether M² is uniquely determined by consistency requirements
(complexity maximization?) is an open question. If M² can be
derived, AT becomes a ZERO-parameter theory (after unit choices).

VERDICT: The PDE reduces {c₀, M, D_R} → 1 dimensionless parameter
         (M²) + 1 mass scale (unit convention).
         Classification C: One independent coefficient.
";
    }
}
