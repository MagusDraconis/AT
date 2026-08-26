namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 23 — origin of the ψ-field. Tests whether ψ (the tensor/Weyl mode) can EMERGE from the scalar
/// actualization, or requires a NEW PRIMITIVE. The key facts: a single scalar ρ (spin-0, 1 d.o.f.) cannot produce
/// a spin-2 (2 d.o.f.) tensor; the Weyl tensor is conformally invariant (any scalar ρ, even anisotropic, gives
/// Weyl = 0); a rank-2 tensor requires multiple scalars (multi-field actualization). No new primitives.
/// </summary>
public static class OriginOfPsi
{
    /// <summary>Degrees of freedom of the scalar counting measure ρ (spin-0 = 1).</summary>
    public static double ScalarDof() => 1.0;

    /// <summary>Degrees of freedom of the tensor (spin-2) sector = (d+1)(d−2)/2 (2 at d=3).</summary>
    public static double TensorDof(int d) => DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Weyl tensor of g = ρ^(2/d)η for ANY scalar ρ (even anisotropic): identically 0 (conformal invariance).</summary>
    public static double WeylOfAnisotropicScalar() => 0.0;

    /// <summary>Number of scalar fields required to build a rank-2 tensor (e.g. ∂ᵢρ₁ ∂ⱼρ₂): 2.</summary>
    public static double MultiFieldRequired() => 2.0;
}
