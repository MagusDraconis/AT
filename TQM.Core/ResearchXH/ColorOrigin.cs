namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 78 — origin of SU(3) color. The network hosts ρ, ψ, θ, S, J; its existing gauge content is U(1)
/// (the phase θ) and SU(2) (the spin structure S). SU(3) color is a DIFFERENT Lie algebra — 3 colors, 8 generators,
/// non-Abelian — which does NOT emerge from U(1) or SU(2). However, the link CAN carry an SU(3) connection exactly
/// as it carries the U(1) phase (lattice QCD: a link variable is a group element of the gauge group G). Wilson loops
/// and gluons are the SU(3) analogues of the U(1) holonomy/photon. Confinement is a DYNAMICAL (non-perturbative)
/// property of SU(3), not a structural link feature. Hence SU(3) color is COMPATIBLE but a NEW SECTOR. No new
/// primitives added here (audit only).
/// </summary>
public static class ColorOrigin
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "triplet-representations",
        "color-dof",
        "confinement",
        "wilson-loops",
        "gluon-analogs",
    };

    /// <summary>The existing gauge content is U(1) (θ) and SU(2) (S).</summary>
    public static bool ExistingGaugeIsU1AndSu2() => true;

    /// <summary>Is SU(3) a DIFFERENT Lie algebra (not derivable from U(1)/SU(2))? Yes — 3 colors, 8 generators.</summary>
    public static bool Su3DifferentLieAlgebra() => true;

    /// <summary>Can the link carry an SU(3) connection (like it carries U(1))? Yes (lattice QCD).</summary>
    public static bool LinkCanCarrySu3() => true;

    /// <summary>Is SU(3) color a NEW SECTOR? Yes.</summary>
    public static bool NewSector() => true;

    /// <summary>Is confinement a DYNAMICAL (not structural) property of SU(3)? Yes.</summary>
    public static bool ConfinementIsDynamical() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "NEW SECTOR";
}
