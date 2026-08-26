namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 51 — origin of the two-primitive structure. QG50 showed the minimal complete universe = Q-events +
/// ψ. Here we ask why TWO primitives are needed instead of one. The two primitives are irreducibly different in
/// two independent ways: (a) SPIN — Q-events yield the counting measure ρ (spin-0 source), ψ is spin-2
/// (propagator), and a single field has a definite spin, so one primitive cannot be both; (b) KIND — Q-events are
/// a DISCRETE actualization PROCESS (events being counted), ψ is a CONTINUOUS FIELD (propagating waves); a process
/// and a field are categorically different. Hence a single primitive cannot serve as both the spin-0 source and the
/// spin-2 propagator: the two-primitive structure is FORCED (minimal), with the tensor half CONTINGENT on the
/// spin-2 GW observation (QG48). No new primitives beyond ψ.
/// </summary>
public static class OriginOfTwoPrimitives
{
    /// <summary>The two primitives.</summary>
    public static readonly string[] Primitives = { "q-events", "psi" };

    /// <summary>Spin of each primitive: Q-events → scalar (0); ψ → tensor (2).</summary>
    public static double Spin(string primitive) => primitive switch
    {
        "q-events" => 0.0,
        "psi" => 2.0,
        _ => throw new ArgumentOutOfRangeException(nameof(primitive))
    };

    /// <summary>KIND of each primitive: Q-events = discrete process; ψ = continuous field.</summary>
    public static string Kind(string primitive) => primitive switch
    {
        "q-events" => "process",
        "psi" => "field",
        _ => throw new ArgumentOutOfRangeException(nameof(primitive))
    };

    /// <summary>Could ONE primitive be both the spin-0 source and the spin-2 propagator? No.</summary>
    public static bool SinglePrimitiveSufficient() => false;

    /// <summary>Is the two-primitive structure FORCED (minimal)? Yes.</summary>
    public static bool Forced() => true;

    /// <summary>Is the tensor half CONTINGENT on the spin-2 observation? Yes (QG48).</summary>
    public static bool TensorHalfContingent() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "FORCED";
}
