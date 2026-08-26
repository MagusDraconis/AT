namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 92 — Network consistency constraints. QG91 showed that link length can encode parameter values
/// (PARTIAL). This phase asks whether network-CONSISTENCY conditions (triangle inequalities, loop closure,
/// neighbor constraints, global stability) restrict allowable link lengths — and therefore the parameter values
/// encoded in them.
///
/// Answer: PARTIAL CONSTRAINT. The network metric must be a valid distance: TRIANGLE INEQUALITIES restrict any
/// triple of link lengths, LOOP CONSISTENCY (holonomy closure) restricts closed cycles, NEIGHBOR constraints
/// restrict local configurations, and GLOBAL STABILITY restricts the overall network. Because QG91 links length to
/// parameters (Yukawa suppression, lattice coupling), these consistency conditions INDUCE constraints and
/// correlations among the parameters. However, they are BOUNDS/RELATIONS (inequalities and consistency conditions),
/// not full value determination: the specific values remain free within the allowed region. So consistency
/// PARTIALLY constrains parameter values via length restrictions. No new primitives added here (audit only).
/// </summary>
public static class NetworkConsistencyParameters
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "triangle-inequalities",
        "loop-consistency",
        "neighbor-constraints",
        "global-network-stability",
        "parameter-correlations",
    };

    /// <summary>Do triangle inequalities restrict allowable link lengths? Yes.</summary>
    public static bool TriangleInequalityConstrains() => true;

    /// <summary>Does loop consistency (holonomy closure) restrict link lengths? Yes.</summary>
    public static bool LoopConsistencyConstrains() => true;

    /// <summary>Do neighbor constraints restrict local link configurations? Yes.</summary>
    public static bool NeighborConstraintsApply() => true;

    /// <summary>Does global network stability restrict link lengths? Yes.</summary>
    public static bool GlobalStabilityConstrains() => true;

    /// <summary>Do these length constraints induce parameter correlations/relations? Yes.</summary>
    public static bool InducesParameterCorrelations() => true;

    /// <summary>Do consistency conditions DETERMINE the specific parameter values? No.</summary>
    public static bool ConsistencyDeterminesValues() => false;

    /// <summary>Classification: NO EFFECT / PARTIAL CONSTRAINT / VALUE RELATIONS.</summary>
    public static string Classify() => "PARTIAL CONSTRAINT";
}
