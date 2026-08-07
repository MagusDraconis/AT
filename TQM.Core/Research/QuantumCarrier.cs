namespace TQM.Core.Research;

/// <summary>
/// Data types for quantum information carrier analysis.
/// TQM-X012: Quantum Information Carrier Principle
/// </summary>
public static class QuantumCarrier
{
    public sealed record QuantumCarrierClass(
        string Name, string Prototype,
        bool IsReversible, bool IsSelfConsistent,
        bool IsQuantumCarrier,
        double InfoRetention, double CoherenceTime,
        string Regime);

    public sealed record QuantumCarrierReport(
        List<QuantumCarrierClass> Classes,
        int QuantumCarrierCount,
        string UniversalPrinciple,
        string IntersectionEquation,
        bool NewUniversalClassExists,
        string Classification, string Verdict);
}
