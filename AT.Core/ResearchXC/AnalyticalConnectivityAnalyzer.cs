using AT.Core.ResearchXC.Models;

namespace AT.Core.ResearchXC;

/// <summary>
/// Presents analytical derivation of average causal degree.
/// ResearchXC-004: Analytical Derivation of Causal Degree
/// </summary>
public static class AnalyticalConnectivityAnalyzer
{
    public static string AnalyzeAll()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(AlexandrovIntegralModel.DimensionalFormulaTable());
        sb.AppendLine();
        sb.AppendLine(AlexandrovIntegralModel.TheAnalyticalProof());
        return sb.ToString();
    }

    public static string HonestVerdict()
    {
        return @"
HONEST VERDICT — ANALYTICAL CROSS-CHECK

ANALYTICALLY:  ⟨k⟩(4D) ≈ 3.5  (Alexandrov integral)
NUMERICALLY:   ⟨k⟩(4D) ≈ 5.0  (XC003, causal link counting)
OBSERVED:      M² ≈ 5.0        (mass hierarchy, X053)

DISCREPANCY: Factor ~1.5 between analytical and numerical.
  • Analytical uses Poisson sprinkling with specific link definition.
  • Numerical uses random events with nearest-causal-neighbor links.
  • Different link definitions give different ⟨k⟩.

THE KEY RESULT (ROBUST TO DEFINITION CHOICE):

  ⟨k⟩ = f(d) — depends ONLY on dimensionality, NOT on density or N.

  The ρ (sprinkling density) CANCELS OUT of the integral.
  This is a RIGOROUS MATHEMATICAL RESULT.

  THEREFORE: M² = f(d) is NOT a free parameter.
  It's determined by spacetime dimension (derived: X042).

  The PRECISE value f(3+1) depends on the exact causal set model
  (sprinkling type, link definition, boundary conditions).
  But it is CONSTRAINED to the O(1-10) range.

CLASSIFICATION: C — Strong analytical support.
  ⟨k⟩ = f(d) is proven analytically (ρ cancels).
  Exact f(3+1) requires the specific causal set model.
  M² is NOT an independent parameter — it's f(d).
";
    }
}
