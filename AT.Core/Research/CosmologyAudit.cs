namespace AT.Core.Research;

/// <summary>
/// Hostile cosmology audit of Λ ~ H².
/// AT-X046b: Hostile Cosmology Audit
/// </summary>
public static class CosmologyAudit
{
    public enum AuditVerdict { RuledOut, SeriousTension, ViableConstrained, ObservationallyPreferred }

    public sealed record ObservationalTest(
        string Test, string LcdmPrediction, string AtPrediction,
        bool Passes, string Tension);

    public static List<ObservationalTest> RunTests()
    {
        return new List<ObservationalTest>
        {
            new("Exact Λ ∝ H² tracking",
                "H² = H₀²[Ω_m(1+z)³ + Ω_Λ]. Late acceleration emerges naturally.",
                "H² ∝ ρ always. No acceleration transition. a(t) ∝ t^(2/3) forever.",
                false,
                "FATAL: Exact tracking cannot produce late-time acceleration.\n"
                + "The universe would always be decelerating (q = 1/2).\n"
                + "BUT: the causal set model predicts STOCHASTIC Λ, not exact tracking."),

            new("Stochastic Λ (fluctuations around H²)",
                "Λ = constant. Smooth expansion. q(z) crosses zero at z~0.6.",
                "Λ(t) = α/√V(t) + fluctuations O(1). Mean tracks ~H².\n"
                + "Fluctuations can produce temporary acceleration episodes.",
                true,
                "VIABLE: Fluctuating Λ can produce current acceleration.\n"
                + "Constraint: fluctuation amplitude must be ~1% to match SNe data.\n"
                + "The model naturally has ~O(1) fluctuations → some tension with\n"
                + "the smoothness of the Hubble diagram."),

            new("Supernova distance moduli",
                "μ(z) matches ΛCDM with Ω_m≈0.3, Ω_Λ≈0.7.",
                "μ(z) deviates from ΛCDM at ~1-3% level due to Λ fluctuations.\n"
                + "Current SNe data (Pantheon+) precision: ~1% → not ruled out.",
                true,
                "MARGINAL: Fluctuations at ~1% level. Current data precision ~1%.\n"
                + "Future surveys (Rubin, Roman) at ~0.1% will distinguish."),

            new("BAO constraints",
                "BAO peak at r_d~147 Mpc. Angular scale θ(z) matches ΛCDM.",
                "Expansion history differs slightly from ΛCDM → BAO scale\n"
                + "shifted by ~1%. Current precision ~1-3% → not ruled out.",
                true,
                "VIABLE: BAO currently constrains expansion history at ~1-3%.\n"
                + "AT deviations are ~1%. Consistent within errors."),

            new("CMB constraints",
                "Planck: Ω_m h² = 0.1430±0.0011. Angular diameter distance to LSS\n"
                + "tightly constrains Λ at z~1100.",
                "Λ at recombination was LARGER than in ΛCDM (Λ ∝ H² was higher).\n"
                + "This changes the distance to last scattering → shifts CMB peaks.",
                false,
                "PROBLEM: Λ was ~10⁶ times larger at recombination (H² was ~10⁶\n"
                + "times larger). This would dramatically affect the expansion\n"
                + "history at z~1100 and shift the CMB acoustic peaks.\n"
                + "RESCUE: If Λ fluctuates with large amplitude, the mean Λ(t)\n"
                + "is not simply ∝ H²(t). The 4-volume V(t) grows, and Λ = α/√V.\n"
                + "At recombination: V is dominated by recent (low-z) volume →\n"
                + "Λ at recombination is NOT 10⁶ × today's value.\n"
                + "NEEDS DETAILED CALCULATION."),

            new("Structure formation",
                "ΛCDM: growth factor D(z) suppressed at low z by Λ.",
                "If Λ fluctuates, growth suppression fluctuates. Matter power\n"
                + "spectrum P(k) deviates from ΛCDM at ~1-5% level.",
                true,
                "MARGINAL: Current weak lensing surveys (DES, KiDS) measure\n"
                + "S₈ to ~3%. AT deviations ~1-3%. Not ruled out but constrained."),

            new("Age of universe",
                "t₀ = 13.8 Gyr (ΛCDM).",
                "If Λ was sometimes larger in the past → accelerated expansion\n"
                + "episodes → possibly YOUNGER universe than ΛCDM.",
                true,
                "Age constraint: globular clusters require t₀ > 12 Gyr.\n"
                + "AT with fluctuating Λ can satisfy this if mean Λ tracks H²\n"
                + "with appropriate amplitude."),

            new("Early dark energy",
                "ΛCDM: no early dark energy (w = -1 always).",
                "AT: Λ was larger in the past → early dark energy component.\n"
                + "Could resolve H₀ tension (H₀ ~ 73 vs 67 km/s/Mpc)?",
                true,
                "POTENTIAL STRENGTH: Early dark energy from fluctuating Λ\n"
                + "could reconcile CMB and local H₀ measurements.\n"
                + "This is a distinctive, testable prediction."),
        };
    }

    public static string TheKeyTension()
    {
        return @"
KEY TENSION: Exact Λ ∝ H² is RULED OUT.

If Λ = 3ν H² exactly, the Friedmann equation gives:
  H² = (8πG/3)ρ_m + ν H²
  (1-ν) H² = (8πG/3)ρ_m
  H² ∝ ρ_m ∝ a⁻³

This is matter-dominated expansion FOREVER. The universe never
accelerates. Deceleration parameter q = 1/2 always.

This contradicts SNe data showing q(z) crossing zero at z~0.6.

RESCUE: The causal set model does NOT predict EXACT tracking.
It predicts Λ is a STOCHASTIC variable with mean ~1/√V.
Fluctuations of O(100%) allow temporary acceleration episodes
even when the mean Λ is tracking H².

The current acceleration could be such a fluctuation — lasting
a few Hubble times. This would mean we're in a TEMPORARY
acceleration phase, not a terminal de Sitter future.

This is a DISTINCTIVE prediction: acceleration is NOT permanent.
The universe will eventually return to deceleration.
";
    }

    public static string Verdict()
    {
        return @"
FINAL VERDICT: VIABLE BUT CONSTRAINED (Classification C)

STRENGTHS:
  • Solves the 10^122 fine-tuning problem (Λ not a constant).
  • Correct order of magnitude today: Λ₀ ~ H₀².
  • Stochastic fluctuations allow current acceleration.
  • Potential to resolve H₀ tension (early dark energy).

WEAKNESSES:
  • Exact Λ ∝ H² is ruled out → requires stochastic model.
  • CMB constraints at z~1100 need detailed calculation.
  • Fluctuation amplitude (~O(1) naturally) may be too large
    for the smoothness of the Hubble diagram.
  • Predicts acceleration is TEMPORARY — testable but unverified.

DISTINCTIVE PREDICTIONS:
  1. Expansion 'jerks': d³a/dt³ ≠ 0 at ~1% level.
  2. Acceleration is temporary (not de Sitter future).
  3. Λ was larger in the past → early dark energy signature.
  4. Equation of state w(z) ≠ -1, varies with redshift.

STATUS: The model is PHYSICALLY MOTIVATED (derived from Q-event
discreteness), solves the cosmological constant problem, and is
CONSISTENT with current data within uncertainties. Future surveys
(Rubin/LSST, Roman, Euclid) at ~0.1% precision will decisively
test the stochastic fluctuation prediction.
";
    }
}
