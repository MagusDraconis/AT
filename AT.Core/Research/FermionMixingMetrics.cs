namespace AT.Core.Research;

/// <summary>
/// Data types for X054 Fermion Mixing.
/// </summary>
public static class FermionMixingMetrics
{
    public enum MixingStatus { MixingArbitrary, WeakEmergence, StructureEmerges, PatternDerived }

    public sealed record MixingMechanism(
        string Name, string OverlapSource,
        string ScalingLaw, bool ProducesHierarchy,
        bool ExplainsCKM, bool ExplainsPMNS,
        string FatalFlaw, bool Survives);

    public sealed record MixingMatrix(
        string Name, double[,] Matrix,
        double DeviationFromObserved, string Notes);

    public sealed record MixingReport(
        List<MixingMechanism> Mechanisms,
        List<MixingMatrix> PredictedMatrices,
        int SurvivingModels, MixingStatus Status,
        string Derivation, string Verdict);
}
