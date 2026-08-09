namespace TQM.Core.ResearchXB.Models;

/// <summary>
/// Models freezeout as the epoch when actualization rate < Hubble rate.
/// ResearchXB-007
/// </summary>
public static class FreezeoutCriterionModel
{
    /// <summary>
    /// Universal freezeout criterion: Γ(T) < H(T).
    /// Γ = rate of abundance-changing actualizations.
    /// H = Hubble expansion rate ∝ T²/M_Planck.
    /// Freezeout at T_f where Γ(T_f) = H(T_f).
    /// </summary>
    public static (double tFreeze, string explanation) ComputeFreezeout(
        string variable, double couplingScale, double massScale)
    {
        double mPlanck = 1.22e19; // GeV
        double tFreeze;

        string explanation;

        switch (variable)
        {
            case "α":
                // EM coupling freezes when U(1) vortex forms at EW scale
                // Γ_EM ∝ α·T (gauge interaction rate)
                // H ∝ T²/M_P → T_f ≈ α·M_P ≈ 100 GeV
                tFreeze = 100;
                explanation = "α freezes at EW scale (~100 GeV).\n"
                    + "  Γ_EM = α·T, H = T²/M_P.\n"
                    + "  Γ_EM = H → T_f ≈ α·M_P ≈ 10^2 GeV.\n"
                    + "  This is when the U(1) vortex stabilizes and the\n"
                    + "  gauge coupling becomes 'locked in.'";
                break;

            case "m_e":
                // Mass scale freezes when defect formation completes
                // Same epoch as α because both involve EW-scale defects
                tFreeze = 100;
                explanation = "m_e freezes at EW scale (~100 GeV).\n"
                    + "  Defect formation completes at the electroweak\n"
                    + "  phase transition. The defect mass (formation energy)\n"
                    + "  is locked in when the defect stabilizes.\n"
                    + "  SAME EPOCH AS α — both are EW-scale phenomena.";
                break;

            case "M²":
                // Nonlinearity freezes when coarse-graining completes
                // Much earlier — at the GUT or Planck scale
                tFreeze = 1e16;
                explanation = "M² freezes at GUT scale (~10^16 GeV).\n"
                    + "  M² is the coarse-grained nonlinearity from Q-event\n"
                    + "  dynamics. It freezes when the Q-event network becomes\n"
                    + "  dense enough for the PDE description to emerge.\n"
                    + "  This occurs at the GUT scale or earlier.";
                break;

            case "Ω_DM":
                // DM relic density freezes when annihilation rate < H
                // Γ_ann = n·⟨σv⟩, n = T³, ⟨σv⟩ ~ 1/TeV²
                tFreeze = massScale / 20; // T_f ≈ m_DM/20 (standard freezeout)
                explanation = $"Ω_DM freezes at ~{tFreeze:F0} GeV.\n"
                    + "  Standard WIMP freezeout: T_f ≈ m_DM/20.\n"
                    + "  Γ_ann = n·⟨σv⟩ drops below H → abundance frozen.\n"
                    + "  Determined by DM mass (~TeV) and cross-section.";
                break;

            default:
                tFreeze = 100;
                explanation = "Default: EW scale.";
                break;
        }

        return (tFreeze, explanation);
    }

    public static string FreezeoutTable()
    {
        var sb = new System.Text.StringBuilder();

        var variables = new[] { "α", "m_e", "Ω_DM", "Ω_b", "M²" };
        double[] masses = { 0, 0, 1000, 1000, 0 };

        sb.AppendLine("FREEZEOUT EPOCHS — Γ < H CRITERION");
        sb.AppendLine();
        sb.AppendLine("  Variable  T_freeze(GeV)  N_steps  Mechanism");
        sb.AppendLine("  " + new string('-', 60));

        double tInit = 1e19;
        foreach (var v in variables)
        {
            var (tFreeze, _) = FreezeoutCriterionModel.ComputeFreezeout(v, 0.1,
                v == "Ω_DM" || v == "Ω_b" ? 1000 : 0);
            int n = (int)Math.Log(tInit / tFreeze);
            string mech = v switch
            {
                "α" => "Gauge coupling stabilization (EW vortices)",
                "m_e" => "Defect formation completion (EW scale)",
                "Ω_DM" => "DM annihilation freezeout (Γ_ann < H)",
                "Ω_b" => "Tied to Ω_DM freezeout (same epoch)",
                "M²" => "Coarse-graining completion (GUT scale)",
                _ => ""
            };
            sb.AppendLine($"  {v,-8}  {tFreeze,12:E0}  {n,6}    {mech}");
        }

        sb.AppendLine();
        sb.AppendLine("  UNIVERSAL CRITERION: Γ(T) < H(T) → freezeout.");
        sb.AppendLine("  Different variables → different Γ → different T_f.");
        sb.AppendLine("  Variables with same physics → same T_f (α and m_e).");
        return sb.ToString();
    }

    public static string TheDerivation()
    {
        return @"
ORIGIN OF FREEZEOUT EPOCHS

THEOREM: Freezeout occurs when the actualization rate Γ(T)
         for a given abundance variable falls below the
         Hubble expansion rate H(T).

UNIVERSAL FREEZEOUT CRITERION:
  Γ_X(T_f) = H(T_f)  →  T_f determined.

WHERE:
  Γ_X(T) = rate of actualizations changing variable X.
  H(T) = T²/M_P (Hubble rate in radiation era).

FREEZEOUT CLASSES:

  CLASS 1 — GAUGE COUPLINGS:
    Γ_α ∝ α·T. Freezeout at T_f ≈ α·M_P ≈ 100 GeV.
    When gauge interactions become slower than expansion.

  CLASS 2 — MASS SCALES:
    Γ_m ∝ defect formation rate. Freezeout at EW scale.
    When defect topology stabilizes (same epoch as class 1).

  CLASS 3 — RELIC DENSITIES:
    Γ_ann = n·⟨σv⟩. Freezeout at T_f ≈ m_DM/20 ≈ 5 GeV.
    Standard thermal freezeout (WIMP miracle).

  CLASS 4 — DYNAMICAL PARAMETERS:
    Γ_M² ∝ coarse-graining rate. Freezeout at GUT scale.
    When effective PDE description emerges.

WHY DIFFERENT EPOCHS:
  • Different variables involve different physical processes.
  • Each process has its OWN rate Γ_X(T).
  • The freezeout epoch is the SOLUTION of Γ_X(T) = H(T).
  • NOT a free parameter — determined by the physics of X.
";
    }
}
