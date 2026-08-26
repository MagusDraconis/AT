namespace AT.Core.Research;

/// <summary>
/// Data types for X048 Internal Symmetry Emergence.
/// </summary>
public static class InternalSymmetryMetrics
{
    public enum SymmetryStatus { NoSymmetry, WeakEmergence, GaugeLikeStructures, FullyDerived }

    public sealed record SymmetryCandidate(
        string Name, string Group, string TopologicalOrigin,
        int Dimension, bool IsLocal, string Evidence,
        bool Survives);

    public sealed record SymmetryReport(
        List<SymmetryCandidate> Candidates,
        int Surviving, SymmetryStatus Status,
        string Derivation, string Verdict);
}
