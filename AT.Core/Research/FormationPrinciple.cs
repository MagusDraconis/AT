namespace AT.Core.Research;

/// <summary>
/// Data types for information carrier formation analysis.
/// AT-X009: Information Carrier Formation Principle
/// </summary>
public static class FormationPrinciple
{
    public sealed record FormationMechanism(
        string Name, string Description,
        bool WorksForLinear, bool WorksForNonlinear,
        bool WorksForTopological, bool IsUniversal);

    public sealed record CarrierFormationReport(
        List<FormationMechanism> Mechanisms,
        string UniversalPrinciple,
        int MechanismCount, int UniversalCount,
        bool PrincipleFound,
        string DeepestInvariant,
        string Classification, string Verdict);
}
