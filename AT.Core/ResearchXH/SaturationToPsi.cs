namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 37 — can saturation generate ψ? QG23 ruled out ψ from the local scalar ρ; QG36 derived the regular
/// core from finite-density saturation. Here we test whether NONLINEAR saturation can generate an effective
/// anisotropic/tensor sector. Key fact: saturation is a scalar map ρ → f(ρ); a nonlinear function of a scalar is
/// still a scalar (spin 0). Its gradient is a vector (spin 1), and an anisotropic front defines a direction
/// (spin 1) — none of these is spin 2. The tensor (ψ/Weyl) sector needs 2 helicities (spin 2), which no scalar
/// nonlinearity can supply; saturation also adds no INDEPENDENT degree of freedom (f(ρ) is determined by ρ).
/// Conclusion: saturation generates only the SCALAR regular-core profile (QG36); the tensor ψ remains a NEW
/// PRIMITIVE. No new primitives.
/// </summary>
public static class SaturationToPsi
{
    /// <summary>Example saturation map ρ → f(ρ) = 1 − e^(−ρ) (scalar in, scalar out).</summary>
    public static double Saturate(double rho) => 1.0 - Math.Exp(-rho);

    /// <summary>Spin of a nonlinear function of a scalar: still 0.</summary>
    public static double SpinOfScalarFunction() => 0.0;

    /// <summary>Spin of a saturation GRADIENT ∇f(ρ): a vector, spin 1.</summary>
    public static double SpinOfGradient() => 1.0;

    /// <summary>Spin of an anisotropic saturation front (a preferred direction n̂): spin 1.</summary>
    public static double SpinOfAnisotropicFront() => 1.0;

    /// <summary>Spin of the tensor (ψ/Weyl) sector: 2.</summary>
    public static double TensorSpin() => 2.0;

    /// <summary>Can any scalar saturation mechanism reach spin 2? No — max spin from scalar saturation is 1.</summary>
    public static bool SaturationGeneratesTensor()
        => Math.Max(Math.Max(SpinOfScalarFunction(), SpinOfGradient()), SpinOfAnisotropicFront()) >= TensorSpin();

    /// <summary>Does saturation add an INDEPENDENT degree of freedom beyond ρ? No — f(ρ) is determined by ρ.</summary>
    public static bool SaturationAddsIndependentDof() => false;

    /// <summary>Does saturation generate the SCALAR regular-core profile? Yes (QG36).</summary>
    public static bool SaturationGeneratesScalarProfile() => true;

    /// <summary>Classification of ψ under saturation.</summary>
    public static string Classify() => "NEW PRIMITIVE";
}
