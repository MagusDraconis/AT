namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for physical scaling law comparison.
///
/// AT-146: Physical Scaling Laws from Topological Charge
/// </summary>
public static class ScalingLawCandidate
{
    public sealed record ScalingCandidate(
        string ObservableName, string QScaling, double QExponent,
        string[] PhysicalMatches, string[] ExactMatches,
        bool HasExactCorrespondence, string UniversalityClass);

    public sealed record ScalingReport(
        List<ScalingCandidate> Candidates,
        int ExactCorrespondences, int ApproximateCorrespondences,
        bool PredictsKnownPhysics, bool PredictsNewPhysics,
        string Classification, string Verdict);
}
