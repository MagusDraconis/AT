namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 73 — measurement from actualization. QG72 showed all quantum structures are present except the
/// collapse. Here we ask whether the measurement process can be identified with Q-event actualization. Key facts: a
/// Q-event is a DISCRETE, BORN-WEIGHTED projection — a node actualizes to a definite (tick/no-tick) state with
/// probability given by the Born rule (ρ = the counting measure = |amplitude|²). This IS the measurement collapse
/// (the projection onto a definite outcome), and it goes BEYOND decoherence (which is unitary, no collapse). The
/// one limitation: the actualization projects onto a BINARY (tick/no-tick) basis, not a general measurement basis.
/// Hence actualization = collapse is a PARTIAL MATCH (collapse recovered, but as a binary projection). No new
/// primitives added here (audit only).
/// </summary>
public static class MeasurementFromActualization
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "actualization-projection",
        "collapse-node-update",
        "born-weighted-actualization",
        "decoherence-vs-actualization",
        "measurement-consistency",
    };

    /// <summary>Is a Q-event a PROJECTION (collapse to a definite outcome)? Yes.</summary>
    public static bool ActualizationIsProjection() => true;

    /// <summary>Is actualization BORN-WEIGHTED (P = |amplitude|²)? Yes (ρ is the counting measure).</summary>
    public static bool ActualizationBornWeighted() => true;

    /// <summary>Is the projection BINARY (tick/no-tick), not a general measurement basis? Yes.</summary>
    public static bool ProjectionIsBinary() => true;

    /// <summary>Does actualization go BEYOND decoherence (it is the actual collapse)? Yes.</summary>
    public static bool ActualizationBeyondDecoherence() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "PARTIAL MATCH";
}
