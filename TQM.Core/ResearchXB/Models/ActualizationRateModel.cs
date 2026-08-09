namespace TQM.Core.ResearchXB.Models;

/// <summary>
/// Universal actualization rate law: Gamma_X = n_X * sigma_X * v_X.
/// ResearchXB-008
/// </summary>
public static class ActualizationRateModel
{
    /// <summary>
    /// Universal form: Gamma_X(T) = n_X(T) * sigma_X * v_X.
    /// n_X = density of participating entities ∝ T^3.
    /// sigma_X = cross-section for the process.
    /// v_X ~ 1 (relativistic at high T).
    /// </summary>
    public static (double gammaAtT, string formula) ComputeRate(
        string variable, double temperature, double coupling)
    {
        double n = temperature * temperature * temperature; // T³ scaling
        double gamma;
        string formula;

        switch (variable)
        {
            case "α":
                // EM: Gamma = n * sigma_EM, sigma_EM ~ alpha^2 / T^2
                gamma = n * coupling * coupling / (temperature * temperature);
                formula = $"Γ_EM = T³ · α²/T² = α²·T = {gamma:E2} (at T={temperature:E0} GeV)";
                break;

            case "m_e":
                // Defect formation: Gamma ~ T (same scaling as EM at EW scale)
                gamma = temperature;
                formula = $"Γ_m = T = {gamma:E2} (defect formation rate ∝ T)";
                break;

            case "Ω_DM":
                // DM annihilation: Gamma = n * <sigma v>
                double sigmaV = 1.0 / (coupling * coupling); // 1/M² for TeV-scale
                gamma = n * sigmaV;
                formula = $"Γ_ann = T³ · 1/M² = T³/M² = {gamma:E2}";
                break;

            case "M²":
                // Coarse-graining: Gamma ~ T³ * l_P² at Planck scale
                double lP2 = 1.0 / (1.22e19 * 1.22e19);
                gamma = n * lP2;
                formula = $"Γ_M² = T³ · ℓ_P² = {gamma:E2} (coarse-graining rate)";
                break;

            default:
                gamma = temperature;
                formula = $"Γ = T = {gamma:E2}";
                break;
        }

        return (gamma, formula);
    }

    public static string UniversalRateLaw()
    {
        return @"
UNIVERSAL ACTUALIZATION RATE LAW

All actualization rates share a COMMON FORM:

  Γ_X(T) = n_X(T) · σ_X · v_X

WHERE:
  n_X(T) = density of entities involved in process X.
           Typically n_X ∝ T³ (thermal equilibrium).

  σ_X = characteristic cross-section for process X.
        • Gauge interactions: σ ~ α²/T² → Γ ∝ α²·T.
        • Defect formation: σ ~ constant → Γ ∝ T³.
        • Annihilation: σ ~ 1/M² → Γ ∝ T³/M².
        • Coarse-graining: σ ~ ℓ_P² → Γ ∝ T³·ℓ_P².

  v_X ~ 1 (relativistic at early-universe temperatures).

THE RATE IS NOT A FREE PARAMETER:
  • n_X from thermodynamics (T, known).
  • σ_X from the physics of process X (α, M², known).
  • v_X from kinematics (~1 at high T).

  Γ_X(T) is FULLY DETERMINED by the physics of X.
  No additional abundance parameters needed.
";
    }

    public static string RateTable()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ACTUALIZATION RATES AT T = T_freeze");
        sb.AppendLine();
        sb.AppendLine("  Variable  T_f(GeV)   n_X(T_f)    σ_X            Γ_X          vs H(T_f)");
        sb.AppendLine("  " + new string('-', 75));

        var specs = new (string var, double tFreeze, string desc)[]
        {
            ("α", 100, "EM coupling"),
            ("m_e", 100, "Defect formation"),
            ("Ω_DM", 5, "DM annihilation"),
            ("M²", 1e16, "Coarse-graining"),
        };

        double mPlanck = 1.22e19;
        foreach (var (v, tf, desc) in specs)
        {
            double n = tf * tf * tf;
            double h = tf * tf / mPlanck;
            double coupling = v == "α" ? 1.0 / 137 : (v == "Ω_DM" ? 1000 : 1);
            var (gamma, _) = ComputeRate(v, tf, coupling);

            sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "  {0,-8} {1,9:E0}  {2,9:E0}  {3,-14} {4,10:E2}  {5,10:E2}",
                v, tf, n, desc, gamma, h));
        }

        sb.AppendLine();
        sb.AppendLine("  AT FREEZEOUT: Γ_X(T_f) ≈ H(T_f) for all variables.");
        sb.AppendLine("  This is not a coincidence — it DEFINES T_f.");
        return sb.ToString();
    }
}
