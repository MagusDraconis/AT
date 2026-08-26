namespace AT.Core.Research;

/// <summary>
/// Data types for X053 Anharmonicity Parameter.
/// </summary>
public static class AnharmonicityMetrics
{
    public enum AnharmonicityStatus { FreeParameter, WeaklyConstrained, PartiallyDerived, FullyDerived }

    public sealed record PotentialAnalysis(
        string DefectType, int Codimension,
        double BarrierHeight, double WellWidth,
        double CubicCoeff, double QuarticCoeff,
        double ComputedA, string Constraints);

    public sealed record HierarchyPrediction(
        string ParticleFamily, double PredictedA,
        double PredictedR21, double PredictedR31,
        double ObservedR21, double ObservedR31,
        double Agreement, string Notes);

    public sealed record AnharmonicityReport(
        List<PotentialAnalysis> Potentials,
        List<HierarchyPrediction> Predictions,
        int DerivedCount, AnharmonicityStatus Status,
        string Derivation, string Verdict);
}
