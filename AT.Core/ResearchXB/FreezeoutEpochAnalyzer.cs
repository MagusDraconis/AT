using AT.Core.ResearchXB.Models;

namespace AT.Core.ResearchXB;

/// <summary>
/// Derives freezeout epochs from actualization rate vs expansion.
/// ResearchXB-007: Freezeout Epoch Physics
/// </summary>
public static class FreezeoutEpochAnalyzer
{
    public static string AnalyzeAll()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(FreezeoutCriterionModel.FreezeoutTable());
        sb.AppendLine();
        sb.AppendLine(FreezeoutCriterionModel.TheDerivation());

        sb.AppendLine();
        sb.AppendLine("FREEZEOUT CLASSES → ABUNDANCE UNIVERSALITY CLASSES");
        sb.AppendLine();
        sb.AppendLine("  Class          T_freeze    Examples    Why This Epoch");
        sb.AppendLine("  " + new string('-', 65));
        sb.AppendLine("  GAUGE          ~100 GeV    α           Γ_EM < H at EW scale");
        sb.AppendLine("  MASS           ~100 GeV    m_e         Defect formation completes");
        sb.AppendLine("  RELIC          ~5 GeV      Ω_DM, Ω_b   Γ_ann < H (thermal freezeout)");
        sb.AppendLine("  DYNAMICS       ~10^16 GeV  M²          Coarse-graining → PDE emerges");
        sb.AppendLine();
        sb.AppendLine("  FOUR FREEZEOUT CLASSES = THREE ABUNDANCE UNIVERSALITY CLASSES.");
        sb.AppendLine("  (MASS and GAUGE freeze at same epoch → share μ and σ².)");

        return sb.ToString();
    }

    public static string TheFreezeoutHierarchy()
    {
        return @"
COMPLETE ABUNDANCE HIERARCHY — ALL SIX LAYERS

LAYER 1 (XB001): Category — Abundance ≠ Identity
LAYER 2 (XB002): Distribution — All LOG-NORMAL
LAYER 3 (XB003): Variance — σ² = N·σ₀²
LAYER 4 (XB004): Volatility — σ₀² from Born rule, M² → σ₀²
LAYER 5 (XB005): Mean — μ = log(N_f/N_i), cosmic expansion
LAYER 6 (XB007): Freezeout — Γ_X(T) < H(T) criterion

UNIVERSAL FREEZEOUT CRITERION:
  Γ_X(T_f) = H(T_f) = T_f²/M_P

  Different variables → different Γ_X → different T_f.
  Same physical process → same Γ_X → same T_f.
  Freezeout epoch is DERIVED, not postulated.

COMPLETE ABUNDANCE PHYSICS:
  log(X) ~ N(log(N_f/N_i), log(T_i/T_f)·σ₀²(M²))

  WHERE T_f is the SOLUTION of Γ_X(T_f) = H(T_f).

  ALL parameters now reduce to:
    • M² (one continuous AT parameter)
    • The physics of each abundance variable (sets Γ_X)

  ZERO free abundance parameters remaining.
";
    }
}
