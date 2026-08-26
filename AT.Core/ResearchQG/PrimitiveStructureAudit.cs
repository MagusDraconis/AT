namespace AT.Core.ResearchQG;

/// <summary>QG-092 primitive-structure audit: what remains in a universe with NO causal order?</summary>
public static class PrimitiveStructureAudit
{
    public static string NoCausalOrderConsequences =>
        "no dynamics (no before/after), no information (no distinctions), no observers, " +
        "no paradoxes — an acausal structure is observationally empty";

    /// <summary>Can observations/dynamics/information/observers remain meaningful without causality?</summary>
    public static bool MeaningfulWithoutCausality => false;

    /// <summary>Does consistency FORBID the intransitive/cyclic orders (leaving a partial order)?</summary>
    public static bool ConsistencyForcesPartialOrder =>
        true; // cycles/intransitivity produce paradoxes; a consistent order is a partial order
}
