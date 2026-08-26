namespace AT.Core.Research;

/// <summary>
/// Data types for X034 Unified AT Synthesis.
/// </summary>
public static class UnifiedATMetrics
{
    public enum ConceptStatus { Postulate, DerivedTheorem, EmergentStructure, NecessaryConsequence, Irreducible }

    public sealed record UnifiedConcept(
        string Name, int Level, string Origin,
        ConceptStatus Status, string Derivation,
        string[] DependsOn, string Notes);

    public sealed record ReductionResult(
        string Name, bool IsRedundant,
        string ReducedTo, string Justification);

    public sealed record UnifiedATReport(
        List<UnifiedConcept> Concepts, List<ReductionResult> Reductions,
        int PostulateCount, int DerivedCount, int EmergentCount,
        int NecessaryCount, int IrreducibleCount,
        string[] MinimalPostulates, string Classification,
        string Verdict);

    public sealed record ConsistencyCheck(
        string Experiment, string Concept, bool Present,
        string Location, string Notes);
}
