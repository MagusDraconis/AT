namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for Schrödinger correspondence analysis.
///
/// TQM-149: Emergence of Schrödinger Dynamics from Q Networks
/// </summary>
public static class SchrodingerMapping
{
    public sealed record DynamicsComparison(
        string Model, string Equation, bool NormConserved,
        bool PhaseEvolves, bool InterferencePossible,
        bool StationaryStatesExist, string DynamicsClass);

    public sealed record SchrodingerReport(
        List<DynamicsComparison> Models,
        int ModelCount, bool UnitaryEvolutionPossible,
        bool ContinuumLimitIsSchrodinger,
        string Classification, string Verdict);
}
