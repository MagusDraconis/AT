namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 77 — cosmology compatibility audit. Can the unified network reproduce the basic cosmological
/// observations? The scalar sector gives EXPANSION (the gravitational redshift, QG26, and the scale-free ρ
/// evolution, G4-RHO) — DERIVED. The conformal metric g = ρ^(2/d)η is conformally flat, so FRW geometry (a = ρ^(1/d))
/// and CMB isotropy are COMPATIBLE. The deficit m = ρ̄ − ρ and the log-deficit give flat rotation curves (G4-ME) —
/// COMPATIBLE for the dark-matter effect (not the particle). STRUCTURE FORMATION and DARK ENERGY (Λ) are UNKNOWN.
/// Nothing is MISSING. No new primitives added here (audit only).
/// </summary>
public static class CosmologyAudit
{
    /// <summary>The six cosmological features audited.</summary>
    public static readonly string[] Features =
    {
        "expansion",
        "frw-geometry",
        "cmb-isotropy",
        "structure-formation",
        "dark-matter",
        "dark-energy",
    };

    /// <summary>Classification of each feature.</summary>
    public static string Classify(string feature) => feature switch
    {
        "expansion" => "DERIVED",       // redshift (QG26) + scale-free ρ evolution (G4-RHO)
        "frw-geometry" => "COMPATIBLE", // conformal metric hosts FRW (a = ρ^(1/d))
        "cmb-isotropy" => "COMPATIBLE", // conformal isotropy
        "structure-formation" => "UNKNOWN", // perturbation growth / clustering not derived
        "dark-matter" => "COMPATIBLE",  // deficit/log-deficit → flat rotation curves (G4-ME)
        "dark-energy" => "UNKNOWN",     // Λ is empirical
        _ => throw new ArgumentOutOfRangeException(nameof(feature))
    };

    /// <summary>The remaining cosmology gaps.</summary>
    public static readonly string[] Gaps =
    {
        "structure-formation",   // density-perturbation growth, galaxy clustering
        "dark-energy",           // Lambda, accelerating expansion
    };
}
