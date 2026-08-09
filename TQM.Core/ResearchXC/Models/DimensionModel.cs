namespace TQM.Core.ResearchXC.Models;

/// <summary>
/// Models for Dimensionality Unification (ResearchXC-012).
/// </summary>
public static class DimensionModel
{
    /// <summary>A dimensionality derivation path.</summary>
    public sealed record DimensionPath(
        string Name, string Chain,
        string KeyQuantity, double D3plus1Value,
        string Status);

    /// <summary>Connectivity as function of dimension.</summary>
    public sealed record ConnectivityDimension(
        int SpatialDim, int TotalDim,
        double LinkedDegree, double InteractionDegree,
        bool SupportsChemistry, bool SupportsObservers,
        string Verdict);

    /// <summary>A requirement for viable dimensionality.</summary>
    public sealed record DimensionRequirement(
        string Requirement, string Condition,
        bool Satisfied2, bool Satisfied3, bool Satisfied4, bool Satisfied5,
        string Origin);

    /// <summary>The unified dimension chain.</summary>
    public sealed record UnifiedChain(
        string Title,
        List<DimensionPath> Paths,
        List<ConnectivityDimension> Connectivities,
        List<DimensionRequirement> Requirements,
        string BridgeQuantity,
        string Principle,
        string Verdict);
}
