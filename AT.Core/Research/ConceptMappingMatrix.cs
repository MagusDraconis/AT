namespace AT.Core.Research;

/// <summary>
/// Data types for X033 Emergence Gap Audit.
/// </summary>
public static class ConceptMappingMatrix
{
    public enum GapCategory { Equivalent, Implicit, Emergent, GenuineGap }

    public sealed record ConceptMapping(
        string Concept, string MainATView, string ResearchXView,
        GapCategory Category, string DerivationPath, string Notes);

    public sealed record EmergenceGapReport(
        List<ConceptMapping> Mappings, int TotalConcepts,
        int Equivalent, int Implicit, int Emergent, int GenuineGaps,
        string Classification, string Verdict);

    public sealed record FrameworkProjection(
        string Framework, string StartingQuestion,
        string MathematicalCore, string PrimaryDiscovery,
        string BlindSpot, string[] NaturalConcepts, string[] HiddenConcepts);
}
