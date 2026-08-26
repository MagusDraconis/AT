namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 64 — unify link content. Links currently carry trace → ρ, traceless → ψ, and phase → U(1). We ask
/// whether these are three independent degrees of freedom or components of ONE link object. The complete link is a
/// COMPLEX symmetric rank-2 object L_ij = a_ij · e^{iθ_ij}: its MAGNITUDE a_ij decomposes into trace ρ (spin-0) +
/// traceless ψ (spin-2), and its PHASE θ is the U(1) gauge angle. So the three sectors (ρ, ψ, θ) are irreducible
/// components of a SINGLE complex link object — UNIFIED, exactly as the network primitive unified nodes + links
/// (QG55). The three sectors remain independent degrees of freedom (different representations), but they are the
/// components of one complete link. No new primitives added here (audit only).
/// </summary>
public static class LinkUnification
{
    /// <summary>The three sectors of a link.</summary>
    public static readonly string[] Sectors = { "trace", "traceless", "phase" };

    /// <summary>Spin/kind of each sector: trace = spin-0 magnitude, traceless = spin-2, phase = U(1).</summary>
    public static string Kind(string sector) => sector switch
    {
        "trace" => "spin-0 (magnitude)",
        "traceless" => "spin-2 (shape)",
        "phase" => "U(1) (phase)",
        _ => throw new ArgumentOutOfRangeException(nameof(sector))
    };

    /// <summary>Are the three sectors INDEPENDENT degrees of freedom? Yes (different representations).</summary>
    public static bool SectorsIndependent() => true;

    /// <summary>Can the three sectors be expressed as components of ONE link object? Yes (a complex rank-2 link).</summary>
    public static bool ExpressibleAsOneObject() => true;

    /// <summary>Is the complete link a SINGLE structure (magnitude + phase)? Yes.</summary>
    public static bool CompleteLinkSingleStructure() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "UNIFIED";
}
