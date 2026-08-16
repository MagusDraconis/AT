namespace TQM.Core.ResearchXH;

/// <summary>
/// Curvature reconstruction from the native conformal operator Lc = ρ⁻¹ L ρ⁻¹.
/// Each spectral observable of Lc deviates from the flat reference (ρ = 1) in a direction
/// fixed by the scalar-curvature sign, so their normalized deviations are summed into a
/// signed score: Score &gt; 0 ⇒ R &gt; 0, Score ≈ 0 ⇒ flat, Score &lt; 0 ⇒ R &lt; 0.
/// Observables used: spectral gap, heat trace Z(1), spectral zeta ζ(2), spectral entropy S(1).
/// </summary>
public static class CurvatureReconstruction
{
    public static double Score(GeometricGraph flat, GeometricGraph geometry)
    {
        double[] ef = ConformalOperator.Eigenvalues(flat, ConformalOperatorKind.RhoInverseSquared);
        double[] eg = ConformalOperator.Eigenvalues(geometry, ConformalOperatorKind.RhoInverseSquared);

        double gapF = SpectralCurvature.SpectralGap(ef);
        double gapG = SpectralCurvature.SpectralGap(eg);
        double zF = SpectralCurvature.HeatTrace(ef, 1.0);
        double zG = SpectralCurvature.HeatTrace(eg, 1.0);
        double zetaF = SpectralCurvature.SpectralZeta(ef, 2.0);
        double zetaG = SpectralCurvature.SpectralZeta(eg, 2.0);
        double entF = SpectralCurvature.SpectralEntropy(ef, 1.0);
        double entG = SpectralCurvature.SpectralEntropy(eg, 1.0);

        // Each term has sign = sign(R):  + ⇒ positive curvature, − ⇒ negative curvature.
        double dGap = (gapG - gapF) / gapF;
        double dZ = (zF - zG) / zF;
        double dZeta = (zetaF - zetaG) / zetaF;
        double dEnt = (entF - entG) / entF;
        return dGap + dZ + dZeta + dEnt;
    }

    /// <summary>Reconstructed curvature sign (−1, 0, +1).</summary>
    public static int Sign(GeometricGraph flat, GeometricGraph geometry)
        => Math.Sign(Score(flat, geometry));
}
