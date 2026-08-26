namespace AT.Core.Research;

/// <summary>
/// Data types for X051 Particle Generations.
/// </summary>
public static class ParticleGenerationMetrics
{
    public enum GenerationStatus { NoStructure, WeakFamilies, GenerationsEmerge, ThreeGenerationsDerived }

    public sealed record GenerationModel(
        string Name, string Mechanism,
        int PredictedGenerations, bool HasMassHierarchy,
        bool HasMixing, bool MatchesObservation,
        string FatalFlaw, bool Survives);

    public sealed record ExcitationLevel(
        int Level, double Mass, double Stability,
        double Lifetime, bool IsObservable,
        string PhysicalAnalog);

    public sealed record GenerationReport(
        List<GenerationModel> Models,
        List<ExcitationLevel> Spectrum,
        int SurvivingModels, GenerationStatus Status,
        string Derivation, string Verdict);
}
