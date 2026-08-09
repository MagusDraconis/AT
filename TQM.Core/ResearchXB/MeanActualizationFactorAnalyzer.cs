using TQM.Core.ResearchXB.Models;

namespace TQM.Core.ResearchXB;

/// <summary>
/// Derives the mean actualization factor r̄ from cosmic expansion.
/// ResearchXB-005: Origin of the Mean Actualization Factor
/// </summary>
public static class MeanActualizationFactorAnalyzer
{
    public static string AnalyzeAll()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("ORIGIN OF r̄ — COSMOLOGICAL EXPANSION");
        sb.AppendLine();
        sb.AppendLine("  r̄ > 1 because the universe EXPANDS.");
        sb.AppendLine("  Each actualization: N → N+1 → factor = (N+1)/N.");
        sb.AppendLine("  The mean drift μ = log(N_final/N_initial).");
        sb.AppendLine();
        sb.AppendLine(MeanGrowthModel.ParameterTable());
        sb.AppendLine();
        sb.AppendLine(MeanGrowthModel.TheDerivation());

        return sb.ToString();
    }

    public static string CompleteAbundanceHierarchy()
    {
        return @"
COMPLETE ABUNDANCE PHYSICS — ALL FIVE LAYERS

═══════════════════════════════════════════════════════════════
  RESEARCHXB: ABUNDANCE PHYSICS — COMPLETE HIERARCHY
═══════════════════════════════════════════════════════════════

LAYER 1 (XB001): CATEGORY
  Abundance ≠ Identity.
  'What exists?' = Topology. 'How much?' = History.

LAYER 2 (XB002): DISTRIBUTION FAMILY
  All abundance = LOG-NORMAL.
  log(X) ~ N(μ, σ²).
  Mechanism: Multiplicative actualization cascades.

LAYER 3 (XB003): VARIANCE ORIGIN
  σ² = N·σ₀².
  N = log(T_init/T_freeze) — cascade depth.
  σ₀² ≈ 0.09 — universal per-step volatility.

LAYER 4 (XB004): VOLATILITY ORIGIN
  σ₀² = Var[-log(p)] from Born rule.
  M² → σ₀² (Identity-Abundance bridge).

LAYER 5 (XB005): MEAN ORIGIN
  μ = log(N_final/N_initial) — cosmic expansion.
  r̄ > 1 because the universe grows.
  Same freezeout → same μ.

═══════════════════════════════════════════════════════════════

FIVE LAYERS. TWO PARAMETERS (M², N).
ABUNDANCE PHYSICS IS COMPLETE.

M² → σ₀² (volatility per step).
N → μ, σ² (cosmic expansion + accumulated randomness).

All abundance quantities = log-normal draws from:
  log(X) ~ N(log(N_final/N_initial), N·σ₀²(M²))

ONE STOCHASTIC PROCESS. ONE PARAMETER (M²). COMPLETE THEORY.
";
    }
}
