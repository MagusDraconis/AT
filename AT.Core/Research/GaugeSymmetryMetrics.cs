namespace AT.Core.Research;

/// <summary>
/// Data types for X050 Gauge Symmetry from Defect Topology.
/// </summary>
public static class GaugeSymmetryMetrics
{
    public enum DerivationStatus { Fundamental, WeakEmergence, PartialEmergence, FullyDerived }

    public sealed record DefectClass(
        string Name, string TopologicalInvariant,
        int Codimension, string ModuliSpace,
        string AutomorphismGroup, bool IsStable,
        string PhysicalInterpretation);

    public sealed record SymmetryDerivation(
        string Step, string From, string To,
        bool IsRigorous, string Gap);

    public sealed record GaugeSymmetryReport(
        List<DefectClass> Defects,
        List<SymmetryDerivation> DerivationChain,
        int DefectCount, int StableCount,
        DerivationStatus Status, string Verdict);
}
