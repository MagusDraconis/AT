using AT.Core.ResearchXB.Models;
using System.Globalization;

namespace AT.Core.ResearchXB;

/// <summary>
/// Derives the origin of μ and σ for abundance distributions.
/// ResearchXB-003: Origin of Distribution Parameters
/// </summary>
public static class DistributionParameterAnalyzer
{
    public static string AnalyzeAll()
    {
        var parameters = CascadeDepthModel.ComputeParameters();
        var sb = new System.Text.StringBuilder();

        sb.AppendLine(CascadeDepthModel.ParameterTable(parameters));
        sb.AppendLine();
        sb.AppendLine(CascadeDepthModel.TheDerivation());

        // Verify the σ²/N ≈ constant prediction
        sb.AppendLine();
        sb.AppendLine("  VERIFICATION: σ²/N should be CONSTANT across all variables.");
        sb.AppendLine("  Variable    N     σ²      σ²/N");
        sb.AppendLine("  " + new string('-', 40));
        foreach (var p in parameters)
        {
            double varN = p.PredictedSigma * p.PredictedSigma / p.CascadeSteps;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-10} {1,4}  {2,7:F3}  {3,7:F4}",
                p.Symbol, p.CascadeSteps, p.PredictedSigma * p.PredictedSigma, varN));
        }
        sb.AppendLine();
        sb.AppendLine("  PREDICTION: σ²/N ≈ 0.09 for ALL abundance variables.");
        sb.AppendLine("  This is the universal per-step volatility σ₀².");

        return sb.ToString();
    }

    public static string TheAbundanceHierarchy()
    {
        return @"
THE COMPLETE ABUNDANCE HIERARCHY

ResearchXB has built a three-layer theory of abundance:

LAYER 1 (XB001): CATEGORY
  Abundance ≠ Identity.
  Identity = topology (WHAT exists).
  Abundance = history (HOW MUCH exists).

LAYER 2 (XB002): DISTRIBUTION FAMILY
  All abundance quantities are LOG-NORMAL.
  Mechanism: Multiplicative actualization cascades.
  Law: log(X) ~ N(μ, σ²).

LAYER 3 (XB003): PARAMETER ORIGIN
  μ = N·log(r̄) — cascade depth × mean step.
  σ² = N·σ₀² — cascade depth × per-step volatility.
  N = log(T_init/T_freezeout) — cosmological freezeout epoch.
  σ₀² ≈ 0.09 — universal per-step volatility.

THREE LAYERS. THREE PARAMETERS (N, r̄, σ₀).
ALL abundance physics from ONE stochastic process.
";
    }
}
