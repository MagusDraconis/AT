namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for the microscopic origin of the Theta field operator:
/// Q charge interactions, graph Laplacians, and operator reconstruction.
///
/// TQM-142: Origin of the Theta Operator
/// </summary>
public static class ChargeInteractionOperator
{
    /// <summary>
    /// A topological charge quantum with spatial position and phase.
    /// </summary>
    public sealed record QCharge(
        int Index,
        double Position,                    // spatial coordinate x ∈ [0,1]
        double Phase,                       // θ_Q
        double Charge);                     // topological charge value

    /// <summary>
    /// The Q interaction network: a graph where nodes are charges
    /// and edges represent pairwise interactions.
    /// </summary>
    public sealed record QInteractionNetwork(
        int QCount,
        double[] Positions,                 // x_i
        double[] Phases,                    // θ_i
        double[,] InteractionMatrix,        // J_ij = coupling strength
        double[,] AdjacencyMatrix,          // A_ij = 1 if interacting, 0 otherwise
        double[,] GraphLaplacian,           // L_ij = D - A (graph Laplacian)
        double RhoQ,                        // charge density
        string Topology);                   // "1D Chain", "Ring", "Random", "Small-World"

    /// <summary>
    /// Comparison between reconstructed and original Theta operator.
    /// </summary>
    public sealed record OperatorReconstruction(
        int QEnsembleSize,
        int ReconstructedDimension,         // N of reconstructed L
        double[] OriginalEigenvalues,       // from TQM-140 L
        double[] ReconstructedEigenvalues,  // from Q graph Laplacian
        double SpectralOverlap,             // cosine similarity of eigenvalue vectors
        double MeanEigenvalueError,         // mean absolute difference
        bool Converged,                     // does reconstructed spectrum approach original?
        string ConvergenceQuality);         // "Excellent", "Good", "Moderate", "Poor"

    /// <summary>
    /// Complete operator origin report.
    /// </summary>
    public sealed record OperatorOriginReport(
        List<QInteractionNetwork> Networks,
        List<OperatorReconstruction> Reconstructions,
        int MaxQEnsembleSize,
        double BestSpectralOverlap,
        double ConvergenceThreshold,        // Q size needed for convergence
        bool OperatorDerived,               // can L be derived from Q?
        bool SpectrumMatches,               // do eigenvalues match?
        bool TopologyMatches,               // does graph structure match landscape?
        string Classification,              // "A: Phenomenological Operator" ... "D: Fundamental Microscopic Origin"
        string Verdict);
}
