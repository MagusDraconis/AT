namespace TQM.Core.Research;

/// <summary>
/// Data types for X059 Neutrino Mass Origin.
/// </summary>
public static class NeutrinoMassMetrics
{
    public enum NeutrinoStatus { NoMechanism, WeakExplanation, PartiallyDerived, FullyDerived }

    public sealed record NeutrinoModel(
        string Name, string Mechanism,
        double PredictedMassEV, bool ExplainsTinyMass,
        bool ExplainsLargeMixing, bool PredictsMajorana,
        string FatalFlaw, bool Survives);

    public sealed record LocalizationComparison(
        string DefectType, bool HasU1Charge,
        double XiOverLP, double MassSuppression,
        double MixingBeta, string Notes);

    public sealed record NeutrinoReport(
        List<NeutrinoModel> Models,
        List<LocalizationComparison> Comparisons,
        int SurvivingModels, NeutrinoStatus Status,
        string Derivation, string Verdict);
}
