namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 79 — Why SU(3)? The network already carries U(1) (θ) and SU(2) (S). QG78 established that
/// SU(3) color is a NEW SECTOR. This phase asks whether SU(3) is the MINIMAL non-Abelian extension of the link.
///
/// Answer: NO in the abstract — SU(2) (dim 3) is the smallest non-Abelian Lie group and it is already present
/// as the spin structure. The real question is not "smallest non-Abelian group" but "which group acts on a
/// 3-dimensional color space". The count of colors (N = 3) is an empirical input forced by fermion statistics
/// (the Δ++ baryon uuu antisymmetrization requires 3 colors); it is NOT derivable from the network. GIVEN N = 3
/// colors, the maximal unitary determinant-1 group is SU(3), with N²−1 = 8 generators (8 gluons). So SU(3) is
/// PREFERRED (unique) given the 3-color postulate, but the 3-color count itself is a NEW POSTULATE. No new
/// primitives added here (audit only).
/// </summary>
public static class WhySU3
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "su2-vs-su3",
        "color-triplets",
        "generator-counting",
        "confinement-requirements",
        "link-information-capacity",
    };

    /// <summary>SU(2) is already present as the spin structure S.</summary>
    public static bool Su2AlreadyPresentAsSpin() => true;

    /// <summary>SU(2) (dimension 3) is the smallest non-Abelian Lie group, NOT SU(3) (dimension 8).</summary>
    public static bool MinimalNonAbelianIsSu2() => true;

    /// <summary>Is SU(3) the MINIMAL non-Abelian extension? No — SU(2) is smaller.</summary>
    public static bool Su3IsMinimalNonAbelian() => false;

    /// <summary>The number of colors is 3 (color triplets). This is the empirical input.</summary>
    public static int ColorCount() => 3;

    /// <summary>Generator count of SU(N): dim = N²−1. SU(3) → 8 generators (gluons).</summary>
    public static int GeneratorCount(int n) => n * n - 1;

    /// <summary>Gluon count = 8.</summary>
    public static int GluonCount() => 8;

    /// <summary>Given N = 3 colors, the maximal unitary determinant-1 group is SU(3).</summary>
    public static bool GroupGivenColorsIsSu3() => true;

    /// <summary>Is the color count N = 3 DERIVABLE from the network? No — it is a postulate.</summary>
    public static bool ColorCountIsDerived() => false;

    /// <summary>Is confinement a non-perturbative (dynamical) property of SU(3)? Yes.</summary>
    public static bool ConfinementIsNonPerturbative() => true;

    /// <summary>Does the link's information capacity suffice for SU(3) (8 real parameters)? Yes — it already
    /// carries the full complex rank-2 object (ρ, ψ, θ, S, J).</summary>
    public static bool LinkCapacitySuffices() => true;

    /// <summary>Classification: DERIVED / PREFERRED / NEW POSTULATE.</summary>
    public static string Classify() => "NEW POSTULATE";
}
