namespace TQM.Core.ResearchQG;

/// <summary>QG-091 pure causal framework: a universe of events {e} and a partial order ≺
/// (e_A ≺ e_B = "A precedes B"), with NO time coordinate. Measures: causal depth (longest
/// chain), chain length, branching, causal volume (number of events in the order interval).</summary>
public static class CausalUniverse
{
    /// <summary>Number of events in a causal interval of 'depth' D for a causet of
    /// spacetime dimension d: N(D) ∝ D^d. (Used to reconstruct dimension.)</summary>
    public static double CausalVolume(double depth, double dimension)
        => Math.Pow(depth, dimension);

    /// <summary>Recover dimension from the growth of causal volume with depth:
    /// d = d ln N / d ln D.</summary>
    public static double RecoverDimension(double nAtDepth1, double nAtDepth2, double d1, double d2)
        => Math.Log(nAtDepth2 / nAtDepth1) / Math.Log(d2 / d1);

    /// <summary>Number of links/relations per event for a sprinkling into d-dim Minkowski.</summary>
    public static double LinksPerEvent(double dimension) => dimension; // ~order of magnitude

    /// <summary>The dimension of the emergent spacetime (causal sets: d = 4).</summary>
    public const double EmergentDimension = 4.0;
}
