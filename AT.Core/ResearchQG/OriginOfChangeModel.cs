namespace AT.Core.ResearchQG;

/// <summary>QG-090 candidate primitive realities (without assuming change), what mechanism
/// introduces apparent evolution, and the associated framework.</summary>
public sealed record PrimitiveReality(
    string Name,
    string MechanismOfApparentChange,
    string Framework,
    bool HasQuantitativePrediction,
    string Prediction);

public static class OriginOfChangeModel
{
    public static PrimitiveReality[] Realities() => new[]
    {
        new PrimitiveReality("Static block universe", "time slice moving through a 4-manifold",
            "eternalism / B-theory", false, "none (equivalent to GR)"),
        new PrimitiveReality("Information state space", "information update (processing)",
            "Wheeler 'It from Bit'", false, "none yet"),
        new PrimitiveReality("Causal network", "growth of a partially-ordered set",
            "Causal Set Theory", true, "Λ ~ 1/√N ~ 1e-122 (Planck units)"),
        new PrimitiveReality("Quantum state", "correlations/decoherence (problem of time)",
            "Wheeler-DeWitt / relational", false, "none (equivalent to QM+GR)"),
        new PrimitiveReality("Mathematical structure", "internal structure of the object",
            "MUH (Tegmark)", false, "none"),
    };
}
