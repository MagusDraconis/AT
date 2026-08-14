namespace TQM.Core.ResearchQG;

/// <summary>QG-091 geometry emergence: metric/distance from causality alone. The causal
/// relation (the light-cone structure) determines the conformal metric; the volume of a
/// causal interval fixes the conformal factor. In causal set theory the LINK count and the
/// interval volume recover the spacetime dimension and the metric up to conformal factor.</summary>
public static class GeometryEmergence
{
    /// <summary>Volume of a causal diamond of depth D in d dimensions: V ∝ D^d.</summary>
    public static double DiamondVolume(double depth, double dimension)
        => CausalUniverse.CausalVolume(depth, dimension);

    /// <summary>The metric is recovered up to a conformal factor: the causal relations give
    /// the light cone; the interval volumes give the scale.</summary>
    public static string MetricRecovery =>
        "causal order → light cone (conformal metric); interval volume → conformal factor";

    /// <summary>Distance as the number of elements in the interval (a causal measure of length).</summary>
    public static double CausalDistance(double intervalVolume, double dimension)
        => Math.Pow(intervalVolume, 1.0 / dimension);
}
