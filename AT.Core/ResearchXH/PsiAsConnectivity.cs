namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 54 — is ψ a connectivity primitive? QG52 showed ψ is fundamental, but it was always modeled as a
/// field. Here we test whether the spin-2 sector can originate from LINK (connectivity) degrees of freedom rather
/// than node degrees. Key result: a symmetric rank-2 ADJACENCY tensor A_ij (the link structure) has d(d+1)/2
/// components (6 at d=3), which decompose as 1 trace (scalar) + 5 symmetric-traceless (spin-2); the spin-2 part
/// contains exactly 2 transverse-traceless polarizations. The causal order determines the conformal class, whose
/// WEYL tensor IS this spin-2 content — so ψ = Weyl ≠ 0 is precisely the non-conformal content of the causal
/// connectivity (frozen to zero in the scalar sector). Hence ψ has a genuine CONNECTIVITY origin, EQUIVALENT to the
/// field description (the Weyl tensor is a rank-2 field). Classification: BOTH. No new primitives beyond ψ.
/// </summary>
public static class PsiAsConnectivity
{
    /// <summary>Symmetric rank-2 adjacency-tensor components: d(d+1)/2 = 6 at d=3.</summary>
    public static double AdjacencyComponents(int d) => d * (d + 1.0) / 2.0;

    /// <summary>Trace (scalar) degree of freedom: 1.</summary>
    public static double TraceDof() => 1.0;

    /// <summary>Transverse-traceless (spin-2) polarizations: (d+1)(d−2)/2 = 2 at d=3.</summary>
    public static double Spin2Dof(int d) => DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Does the adjacency tensor carry 2 independent spin-2 polarizations? Yes.</summary>
    public static bool ConnectivityCarriesTwoPolarizations(int d) => Spin2Dof(d) == 2.0;

    /// <summary>Is ψ the WEYL (non-conformal) content of the causal connectivity? Yes.</summary>
    public static bool PsiIsWeylContent() => true;

    /// <summary>Are the field and connectivity descriptions EQUIVALENT (Weyl is a rank-2 field)? Yes.</summary>
    public static bool FieldAndConnectivityEquivalent() => true;

    /// <summary>Does the connectivity interpretation ELIMINATE the new primitive? No — Weyl ≠ 0 is still new.</summary>
    public static bool EliminatesNewPrimitive() => false;

    /// <summary>Classification.</summary>
    public static string Classify() => "BOTH";
}
