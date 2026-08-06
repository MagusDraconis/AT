namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for norm conservation origin analysis.
///
/// TQM-152: Origin of Norm Conservation
/// </summary>
public static class NormOriginModel
{
    public sealed record NormOrigin(
        string Hypothesis, string Derivation,
        bool PredictsConservation, bool Reducible,
        string Assessment);

    public sealed record NormConservationReport(
        List<NormOrigin> Origins,
        bool NormConservationDerived,
        int IrreduciblePostulates,
        string Classification, string Verdict);
}
