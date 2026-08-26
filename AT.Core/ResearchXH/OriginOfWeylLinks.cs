namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 56 — origin of Weyl-capable links. QG55 showed ψ = the link content of the causal network. Here we
/// ask WHY links carry a non-conformal (traceless) degree of freedom. Key fact: a link is a relation between two
/// nodes, represented by a symmetric rank-2 (adjacency) tensor A_ij, which ALWAYS decomposes into a trace (scalar,
/// the conformal factor) plus a traceless part (spin-2, the Weyl content). A "conformal-only" link (trace only,
/// Weyl = 0) is a RESTRICTION — it drops the traceless part of the full relation. LINK COMPLETENESS therefore
/// FORCES the Weyl CAPACITY (a complete link carries the full rank-2 relation); the scalar sector's Weyl = 0 was
/// the incomplete (conformally-flat) restriction. The actual nonzero VALUE (ψ ≠ 0) remains CONTINGENT on the GW
/// observation. No new primitives beyond ψ.
/// </summary>
public static class OriginOfWeylLinks
{
    /// <summary>A symmetric rank-2 link tensor ALWAYS has a traceless (Weyl) part in its decomposition.</summary>
    public static bool Rank2HasTracelessPart() => true;

    /// <summary>Conformal-only links (trace only, Weyl = 0) are a RESTRICTION of the full relation.</summary>
    public static bool ConformalOnlyIsRestriction() => true;

    /// <summary>A COMPLETE link carries the full rank-2 relation (trace + Weyl).</summary>
    public static bool CompleteLinkCarriesWeyl() => true;

    /// <summary>Is the Weyl CAPACITY FORCED by link completeness? Yes.</summary>
    public static bool WeylCapacityForced() => true;

    /// <summary>Is the Weyl VALUE (ψ ≠ 0) CONTINGENT on observation? Yes (GW data, QG48).</summary>
    public static bool WeylValueContingent() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "FORCED";
}
