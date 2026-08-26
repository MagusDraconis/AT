namespace AT.Core.Research;

/// <summary>
/// Data types for X038 Origin of Measurement from Individuation.
/// </summary>
public static class MeasurementMetrics
{
    public enum CollapseStatus { Fundamental, WeakReduction, PartiallyDerived, FullyDerived }

    public sealed record MeasurementModel(
        string Name, string Mechanism,
        bool PreservesQAxiom, bool PreservesIdentity,
        bool PreservesFiniteComplexity, bool PredictsSingleOutcome,
        string FatalFlaw, bool Survives);

    public sealed record IndividuationAnalysis(
        string Scenario, int QBefore, int? QAfter,
        bool QConserved, string Implication);

    public sealed record MeasurementReport(
        List<MeasurementModel> Models,
        List<IndividuationAnalysis> IndividuationTests,
        int ModelsTested, int SurvivingModels,
        CollapseStatus Status, string Derivation,
        string Verdict);
}
