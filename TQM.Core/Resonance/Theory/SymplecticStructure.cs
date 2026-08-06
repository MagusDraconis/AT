namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for antisymmetric coupling origin analysis.
///
/// TQM-151: Origin of the Antisymmetric Coupling
/// </summary>
public static class SymplecticStructure
{
    public sealed record CouplingOrigin(
        string Hypothesis, string Derivation,
        bool ProducesJ, bool ConservesNorm,
        string Assessment);

    public sealed record AntisymmetricCouplingReport(
        List<CouplingOrigin> Origins,
        bool JDerived, string BestDerivation,
        string Classification, string Verdict);
}
