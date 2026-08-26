namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 53 — dependency audit. Map which conclusions depend on which assumptions across the full chain:
/// Q-events → ρ → geometry → matter → gravity → saturation, plus the ψ / GW-interpretation link. Classification:
/// Q-events is the ASSUMPTION-FREE root (the primitive); ρ, geometry, matter, gravity, and saturation are all
/// DERIVED from Q-events + principles; ψ and the GW interpretation are MODEL-DEPENDENT — ψ's necessity rests on
/// the model-dependent spin-2 reading of the strain (QG48). The weakest remaining links are therefore ψ and the
/// GW interpretation. No new primitives beyond ψ.
/// </summary>
public static class DependencyAudit
{
    /// <summary>The eight nodes of the dependency graph.</summary>
    public static readonly string[] Nodes =
    {
        "q-events",
        "rho",
        "geometry",
        "matter",
        "gravity",
        "saturation",
        "psi",
        "gw-interpretation",
    };

    /// <summary>Classification of each node.</summary>
    public static string Classify(string node) => node switch
    {
        "q-events" => "ASSUMPTION-FREE",   // the root primitive
        "rho" => "DERIVED",                // density of Q-events
        "geometry" => "DERIVED",           // g = ρ^(2/d)η (η preferred)
        "matter" => "DERIVED",             // m = ρ̄ − ρ deficit
        "gravity" => "DERIVED",            // a = −(1/d)∇ln ρ (kinematic)
        "saturation" => "DERIVED",         // discreteness ⇒ max density (value imported)
        "psi" => "MODEL-DEPENDENT",        // justified by the spin-2 reconstruction (QG48)
        "gw-interpretation" => "MODEL-DEPENDENT", // spin-2 is reconstructed, not measured (QG48)
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <summary>The dependency source of each node (what it rests on).</summary>
    public static string DependsOn(string node) => node switch
    {
        "q-events" => "(root primitive)",
        "rho" => "q-events",
        "geometry" => "rho (+ causal order)",
        "matter" => "rho",
        "gravity" => "geometry",
        "saturation" => "q-events (discreteness)",
        "psi" => "gw-interpretation",
        "gw-interpretation" => "observation + model",
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <summary>The weakest remaining links (MODEL-DEPENDENT nodes).</summary>
    public static readonly string[] WeakestLinks = { "psi", "gw-interpretation" };
}
