namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for imaginary unit origin analysis.
///
/// TQM-150: Origin of the Imaginary Unit
/// </summary>
public static class ComplexEmergenceModel
{
    public sealed record RealCoupledSystem(
        string Name, string Equations,
        bool EquivalentToSchrodinger,
        bool NormConserved,
        string Mechanism);

    public sealed record ImaginaryUnitReport(
        List<RealCoupledSystem> Systems,
        bool ComplexStructureEmerges,
        bool DerivableFromRealDynamics,
        string Classification, string Verdict);
}
