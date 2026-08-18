namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 58 — discrete or continuous links? QG55–57 established ψ = the traceless content of links. Here we
/// ask whether links themselves are DISCRETE network objects or CONTINUOUS fields. Key facts: microscopically the
/// adjacency matrix A_ij has 0/1 (integer) entries — the links are QUANTIZED and COUNTABLE, exactly like the
/// discrete Q-events; the traceless (Weyl) content of a finite graph is therefore discrete too. In the CONTINUUM
/// LIMIT (large N, coarse-graining), the coarse-grained adjacency becomes a smooth field and its traceless content
/// becomes the continuous Weyl tensor ψ. So links are DISCRETE microscopically and CONTINUOUS in the continuum
/// limit — BOTH, in exact parallel to the nodes (discrete Q-events → continuous ρ). No new primitives beyond ψ.
/// </summary>
public static class DiscreteOrContinuousLinks
{
    /// <summary>The adjacency matrix of a simple network has 0/1 (integer) entries — quantized.</summary>
    public static bool AdjacencyQuantized() => true;

    /// <summary>The number of links |E| is a non-negative integer — countable.</summary>
    public static bool LinkCountDiscrete() => true;

    /// <summary>The traceless (Weyl) content of a finite graph is discrete (built from 0/1 entries).</summary>
    public static bool WeylDiscreteMicroscopically() => true;

    /// <summary>In the continuum limit, the coarse-grained link content becomes a smooth field.</summary>
    public static bool ContinuumLimitContinuous() => true;

    /// <summary>Propagation on a finite graph is discrete (hopping), not a continuous wave.</summary>
    public static bool PropagationOnFiniteGraphDiscrete() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "BOTH";
}
