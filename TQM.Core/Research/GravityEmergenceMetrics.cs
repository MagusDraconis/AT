namespace TQM.Core.Research;

/// <summary>
/// Data types for X041 Emergence of Gravity.
/// </summary>
public static class GravityEmergenceMetrics
{
    public enum GravityStatus { NoGravity, WeakCandidate, PartialEmergence, FullyDerived }

    public sealed record GravityModel(
        string Name, string Mechanism,
        bool PredictsAttraction, bool PredictsRedshift,
        bool HasNewtonianLimit, string FatalFlaw,
        bool Survives);

    public sealed record GravityTest(
        string Test, string PredictedA, string PredictedB,
        string PredictedC, string GRPrediction,
        string BestMatch);

    public sealed record GravityReport(
        List<GravityModel> Models,
        List<GravityTest> Tests,
        int ModelsTested, int Surviving,
        GravityStatus Status, string Derivation,
        string Verdict);
}
