namespace TQM.Core.ResearchQG;

/// <summary>QG-091 time emergence: time is a LABEL of the causal order, not an input.
/// A time coordinate t is any real-valued function t(e) that respects the order
/// (e_A ≺ e_B ⇒ t(e_A) < t(e_B)). Time is recovered as causal depth along maximal chains.</summary>
public static class TimeEmergence
{
    public static string Description =>
        "time = causal depth (longest chain length) — a labeling of the partial order";

    /// <summary>Causal depth t(D) along a maximal chain = chain length.</summary>
    public static double CausalDepth(double chainLength) => chainLength;

    /// <summary>A valid time coordinate must be order-preserving (monotone along chains).</summary>
    public static bool IsOrderPreserving(double tA, double tB, bool aPrecedesB)
        => !aPrecedesB || tA < tB;
}
