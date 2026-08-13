namespace TQM.Core.ResearchQG;

/// <summary>QG-090 emergent time: time is not fundamental in quantum gravity — the
/// Wheeler-DeWitt equation H|Ψ⟩=0 gives a static universe state, and time/change emerge from
/// internal correlations (a 'clock' degree of freedom).</summary>
public static class EmergentTimeModel
{
    public static string Description =>
        "Wheeler-DeWitt: H|Ψ⟩=0 → the quantum state is timeless; time emerges from correlations";

    /// <summary>Is time fundamental? (No — it is emergent in quantum gravity.)</summary>
    public static bool TimeIsFundamental => false;

    /// <summary>The deeper primitive: the causal partial order (relational/causal-set view).</summary>
    public static string DeeperPrimitive => "causal partial order (causality)";
}
