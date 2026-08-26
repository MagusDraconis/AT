namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for the global topology of the Theta information attractor landscape.
///
/// AT-139: Information Attractor Landscape Topology
/// </summary>
public static class AttractorBasin
{
    /// <summary>
    /// A single attractor basin in the information landscape.
    /// </summary>
    public sealed record AttractorBasinInfo(
        string Name,                        // species identifier
        double[] Prototype,                 // representative pattern
        double BasinVolume,                 // fraction of ICs converging here
        double Stability,                   // mean persistence time
        double Fitness,                     // w = r/c
        double Complexity,                  // mean zero crossings
        double Energy,                      // pattern energy
        double PotentialDepth,              // V at minimum (lower = deeper basin)
        int Connectivity,                   // number of neighboring attractors
        string SymmetryClass);              // "Uniform", "Odd", "Even", "Mixed", "Complex"

    /// <summary>
    /// A directed transition edge between two attractors.
    /// </summary>
    public sealed record AttractorTransition(
        string FromAttractor,
        string ToAttractor,
        double TransitionProbability,       // P(from → to) under small perturbations
        double EnergyBarrier,               // potential height between basins
        double PatternDistance,             // Euclidean distance in pattern space
        bool IsBidirectional);              // can return as easily?

    /// <summary>
    /// The complete attractor graph with topology metrics.
    /// </summary>
    public sealed record AttractorGraphInfo(
        List<AttractorBasinInfo> Basins,
        List<AttractorTransition> Transitions,
        int TotalAttractors,
        int TotalTransitions,
        double MeanConnectivity,            // average edges per node
        double GraphDensity,                // edges / (n*(n-1))
        int ConnectedComponents,            // number of disconnected subgraphs
        bool IsFullyConnected,              // every attractor reachable from every other?
        double Diameter,                    // longest shortest path
        double ClusteringCoefficient,       // tendency to form clusters
        string Topology,                    // "Hierarchical", "Hub-and-Spoke", "Lattice", "Random", "Small-World"
        int CentralHubAttractorCount,       // attractors with connectivity > 2× mean
        List<string> BottleneckAttractors); // whose removal disconnects the graph

    /// <summary>
    /// Complete landscape topology report.
    /// </summary>
    public sealed record LandscapeTopologyReport(
        List<AttractorBasinInfo> Basins,
        AttractorGraphInfo Graph,
        double[] PotentialLandscape1D,      // 1D slice of V(p) along principal axis
        int TotalICsGenerated,              // number of initial conditions tested
        int ConvergedICs,                   // number that converged
        double ConvergenceRate,             // fraction that converged
        double MeanBasinVolume,             // average basin volume
        double BasinVolumeEntropy,          // Shannon entropy of basin volumes
        bool FiniteLandscape,               // is the landscape demonstrably finite?
        bool StructuredTopology,            // does topology show structure?
        string LandscapeClass,              // "Flat", "Rugged", "Funnel", "Hierarchical"
        string Classification,              // "A: Random Attractors" ... "D: Fundamental Information Landscape"
        string Verdict);
}
