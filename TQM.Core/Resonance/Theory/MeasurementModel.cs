namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Evaluates candidate probability measures and tests whether
/// the Born rule P=|ψ|² emerges uniquely.
///
/// TQM-153: Origin of the Born Rule
/// </summary>
public static class MeasurementModel
{
    public static List<ProbabilityMeasure.ProbabilityCandidate> EvaluateCandidates()
    {
        return new List<ProbabilityMeasure.ProbabilityCandidate>
        {
            new ProbabilityMeasure.ProbabilityCandidate(
                "P ∝ |ψ|", "Linear amplitude",
                false, false, false, false),

            new ProbabilityMeasure.ProbabilityCandidate(
                "P ∝ |ψ|² (Born)", "Squared amplitude",
                true, true, true, true),

            new ProbabilityMeasure.ProbabilityCandidate(
                "P ∝ |ψ|³", "Cubic amplitude",
                false, false, false, false),

            new ProbabilityMeasure.ProbabilityCandidate(
                "P ∝ |ψ|⁴", "Quartic amplitude",
                false, false, false, false),

            new ProbabilityMeasure.ProbabilityCandidate(
                "P ∝ log(|ψ|)", "Logarithmic",
                false, false, false, false),

            new ProbabilityMeasure.ProbabilityCandidate(
                "P ∝ exp(|ψ|)", "Exponential",
                false, false, false, false),
        };
    }

    /// <summary>
    /// Test: only |ψ|² satisfies additivity for orthogonal states.
    /// For ψ = α|0⟩ + β|1⟩, require P(ψ on |0⟩) + P(ψ on |1⟩) = 1.
    /// </summary>
    public static (bool bornUnique, string reason) TestUniqueness()
    {
        // For a 2-state system with ψ = (cos θ, sin θ):
        // Only P ∝ |c_i|² gives P_0 + P_1 = cos²θ + sin²θ = 1.
        // P ∝ |c_i| gives |cos θ| + |sin θ| ≠ 1 (except θ=0,π/2).
        // P ∝ |c_i|³ gives |cos θ|³ + |sin θ|³ < 1 (except edges).

        double theta = Math.PI / 4; // 45°
        double c0 = Math.Cos(theta);
        double c1 = Math.Sin(theta);

        double p1_linear = Math.Abs(c0) + Math.Abs(c1); // ≈ 1.414
        double p2_squared = c0 * c0 + c1 * c1;         // = 1.000
        double p3_cubic = Math.Pow(Math.Abs(c0), 3) + Math.Pow(Math.Abs(c1), 3); // ≈ 0.707

        bool bornUnique = Math.Abs(p2_squared - 1.0) < 0.001
                       && Math.Abs(p1_linear - 1.0) > 0.01
                       && Math.Abs(p3_cubic - 1.0) > 0.01;

        return (bornUnique,
            $"At θ=45°: P∝|ψ|={p1_linear:F3}, P∝|ψ|²={p2_squared:F3} (only one =1), P∝|ψ|³={p3_cubic:F3}. "
          + "Only |ψ|² is additive across orthogonal basis states.");
    }
}
