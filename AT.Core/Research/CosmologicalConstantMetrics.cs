namespace AT.Core.Research;

/// <summary>
/// Data types for X046 Cosmological Constant.
/// </summary>
public static class CosmologicalConstantMetrics
{
    public enum LambdaStatus { Fundamental, WeaklyEmergent, PartiallyDerived, FullyDerived }

    public sealed record LambdaModel(
        string Name, string Origin, string Prediction,
        bool MatchesObservation, string FatalFlaw, bool Survives);

    public sealed record LambdaReport(
        List<LambdaModel> Models, int Surviving,
        LambdaStatus Status, string Derivation,
        string Verdict);
}
