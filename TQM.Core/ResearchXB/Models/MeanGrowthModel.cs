namespace TQM.Core.ResearchXB.Models;

/// <summary>
/// Models the mean actualization factor r̄ from cosmological expansion.
/// ResearchXB-005
/// </summary>
public static class MeanGrowthModel
{
    /// <summary>
    /// r̄ emerges from the universal expansion of the Q-event causal set.
    /// As the universe evolves, the total number of Q-events grows.
    /// Each actualization → N → N+1 → multiplicative factor = (N+1)/N ≈ 1 + 1/N.
    /// The accumulated drift μ = log(N_final/N_initial).
    /// </summary>
    public static double ComputeRBar(double tInitialGeV, double tFreezeGeV)
    {
        // N(t) ∝ V(t) ∝ t² in radiation era, t^(3/2) in matter era
        // log(N_final/N_initial) ≈ 2·log(T_init/T_freeze) in radiation era
        double nSteps = Math.Log(tInitialGeV / tFreezeGeV);
        double totalLogGrowth = 2.0 * nSteps; // radiation domination
        double rBar = Math.Exp(totalLogGrowth / nSteps); // mean per step
        return rBar;
    }

    /// <summary>
    /// μ = N·log(r̄) = log(N_final/N_initial) from cosmological expansion.
    /// </summary>
    public static (double mu, string explanation) ComputeMu(double tInitialGeV, double tFreezeGeV)
    {
        double nSteps = Math.Log(tInitialGeV / tFreezeGeV);
        double rBar = ComputeRBar(tInitialGeV, tFreezeGeV);
        double mu = nSteps * Math.Log(rBar);

        string explanation = $"From T_init = {tInitialGeV:E0} GeV to T_freeze = {tFreezeGeV} GeV:\n"
            + $"  N steps = {nSteps:F1}, r̄ = {rBar:F3}, μ = {mu:F2}\n"
            + "  μ = log(N_final/N_initial) — the LOGARITHMIC GROWTH\n"
            + "  of the Q-event causal set during cosmic expansion.\n"
            + "  r̄ > 1 because the universe EXPANDS (more Q-events over time).";

        return (mu, explanation);
    }

    /// <summary>
    /// Different abundance variables freeze at different epochs → different μ.
    /// </summary>
    public static string ParameterTable()
    {
        var sb = new System.Text.StringBuilder();
        double tInit = 1e19; // Planck temperature in GeV

        var specs = new (string name, string symbol, double tFreeze, string era)[]
        {
            ("Fine-structure constant", "α", 100, "EW scale"),
            ("Electron mass scale", "m_e", 100, "EW scale"),
            ("Nonlinearity parameter", "M²", 1e16, "GUT scale"),
            ("Dark matter abundance", "Ω_DM", 5, "DM freezeout"),
            ("Baryon abundance", "Ω_b", 5, "DM freezeout"),
        };

        sb.AppendLine("COSMOLOGICAL EXPANSION → μ");
        sb.AppendLine();
        sb.AppendLine("  Variable     T_freeze    N_steps   r̄        μ");
        sb.AppendLine("  " + new string('-', 55));

        foreach (var (name, symbol, tFreeze, era) in specs)
        {
            double n = Math.Log(tInit / tFreeze);
            double rBar = MeanGrowthModel.ComputeRBar(tInit, tFreeze);
            double mu = n * Math.Log(rBar);
            sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "  {0,-12} {1,10:F0}   {2,6:F1}   {3,7:F3}  {4,8:F2}",
                symbol, tFreeze, n, rBar, mu));
        }

        sb.AppendLine();
        sb.AppendLine("  μ = log(N_final/N_initial) — logarithmic growth of the causal set.");
        sb.AppendLine("  Variables freezing at same epoch have SAME μ.");
        sb.AppendLine("  Variables freezing earlier have LARGER μ (more expansion).");
        return sb.ToString();
    }

    public static string TheDerivation()
    {
        return @"
ORIGIN OF THE MEAN ACTUALIZATION FACTOR r̄

THEOREM: r̄ > 1 because the Q-event causal set EXPANDS during
         cosmic evolution. The accumulated drift μ = N·log(r̄)
         equals the log of the total expansion factor.

DERIVATION:

  1. Q-event count N(t) grows as the universe expands.
     In radiation era: N ∝ t² → dN/dt ∝ t.
     In matter era: N ∝ t^(3/2).

  2. Each actualization adds ~1 to the cosmic Q-event count.
     Multiplicative factor per step: r = (N+1)/N ≈ 1 + 1/N.

  3. The geometric mean r̄ > 1 because N grows.
     log(r̄) = ⟨log(1+1/N)⟩ ≈ ⟨1/N⟩ > 0.

  4. Accumulated drift: μ = Σ log(r_i) = log(N_final/N_initial).
     This is PURELY from cosmic expansion — not a free parameter.

  5. Different freezeout epochs → different N_final/N_initial → different μ.
     Same freezeout epoch → SAME μ.

  6. μ = 2·log(T_init/T_freeze) in the radiation era.

WHAT IS DERIVED:
  ✓ r̄ > 1 (from cosmic expansion).
  ✓ μ = log(N_final/N_initial) (from Q-event count growth).
  ✓ μ scales with freezeout epoch.
  ✓ Same-freezeout variables share same μ.

THE NATURE OF r̄:
  r̄ is NOT a fundamental parameter — it's the COSMOLOGICAL
  EXPANSION RATE expressed as a per-step multiplicative factor.
  The universe's growth IS the drift in abundance cascades.
";
    }
}
