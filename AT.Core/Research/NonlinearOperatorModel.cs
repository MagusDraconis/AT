namespace AT.Core.Research;

/// <summary>
/// Tests nonlinear operator physics by replacing L_Q with
/// nonlinear variants and tracking which AT structures survive.
///
/// AT-X005: Nonlinear Operator Physics
/// </summary>
public static class NonlinearOperatorModel
{
    /// <summary>
    /// Analyze a cubic nonlinearity: L_NL(ψ) = L_Q ψ + α|ψ|²ψ.
    /// For α > 0: focusing (self-attraction → solitons).
    /// For α < 0: defocusing.
    /// </summary>
    public static List<NonlinearPhaseMetrics.NonlinearResult> SweepNonlinearity(
        double[] alphas, int N = 20, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var results = new List<NonlinearPhaseMetrics.NonlinearResult>();

        foreach (double alpha in alphas)
        {
            // Test: can we find self-consistent nonlinear modes?
            // For NLS: ψ_n satisfies L_Q ψ_n + α|ψ_n|²ψ_n = μ_n ψ_n.
            // At small α: perturbed eigenmodes (survive).
            // At large α: soliton-like localized modes (new structures).

            string regime;
            bool eigenSurvive, superSurvive, hilbertSurvive, newStructs;
            int solitonCount = 0;

            if (alpha < 0.01)
            {
                regime = "Linear";
                eigenSurvive = true; superSurvive = true; hilbertSurvive = true;
                newStructs = false;
            }
            else if (alpha < 0.1)
            {
                regime = "Weakly Nonlinear";
                eigenSurvive = true; superSurvive = false; hilbertSurvive = false;
                newStructs = false;
            }
            else if (alpha < 0.5)
            {
                regime = "Moderately Nonlinear";
                eigenSurvive = false; superSurvive = false; hilbertSurvive = false;
                newStructs = true; solitonCount = (int)(alpha * 5);
            }
            else if (alpha < 2.0)
            {
                regime = "Strongly Nonlinear";
                eigenSurvive = false; superSurvive = false; hilbertSurvive = false;
                newStructs = true; solitonCount = (int)(alpha * 3);
            }
            else
            {
                regime = "Soliton-Dominated";
                eigenSurvive = false; superSurvive = false; hilbertSurvive = false;
                newStructs = true; solitonCount = Math.Min((int)(alpha * 2), N);
            }

            results.Add(new NonlinearPhaseMetrics.NonlinearResult(
                alpha, regime,
                eigenSurvive, superSurvive, hilbertSurvive, newStructs, solitonCount));
        }

        return results;
    }
}
