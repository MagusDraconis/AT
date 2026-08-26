namespace AT.Core.ResearchXH;

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
        => Score(
            ConformalOperator.Eigenvalues(flat, ConformalOperatorKind.RhoInverseSquared),
            ConformalOperator.Eigenvalues(geometry, ConformalOperatorKind.RhoInverseSquared));

    /// <summary>Curvature score from two (ascending) eigenvalue spectra.</summary>
    public static double Score(double[] eFlat, double[] eGeo)
    {
        double gapF = SpectralCurvature.SpectralGap(eFlat);
        double gapG = SpectralCurvature.SpectralGap(eGeo);
        double zF = SpectralCurvature.HeatTrace(eFlat, 1.0);
        double zG = SpectralCurvature.HeatTrace(eGeo, 1.0);
        double zetaF = SpectralCurvature.SpectralZeta(eFlat, 2.0);
        double zetaG = SpectralCurvature.SpectralZeta(eGeo, 2.0);
        double entF = SpectralCurvature.SpectralEntropy(eFlat, 1.0);
        double entG = SpectralCurvature.SpectralEntropy(eGeo, 1.0);

        // Each term has sign = sign(R):  + ⇒ positive curvature, − ⇒ negative curvature.
        double dGap = (gapG - gapF) / gapF;
        double dZ = (zF - zG) / zF;
        double dZeta = (zetaF - zetaG) / zetaF;
        double dEnt = (entF - entG) / entF;
        return dGap + dZ + dZeta + dEnt;
    }

    /// <summary>
    /// Robust curvature score: identical to Score but guards against ill-defined observables
    /// (the spectral gap and ζ(2) of a sign-definite/zero spectrum, e.g. the pure potential
    /// term V = Δρ/ρ²). Such terms are dropped rather than poisoning the score with NaN.
    /// </summary>
    public static double ScoreRobust(double[] eFlat, double[] eGeo)
    {
        double gapF = SpectralCurvature.SpectralGap(eFlat);
        double gapG = SpectralCurvature.SpectralGap(eGeo);
        double zF = SpectralCurvature.HeatTrace(eFlat, 1.0);
        double zG = SpectralCurvature.HeatTrace(eGeo, 1.0);
        double zetaF = SpectralCurvature.SpectralZeta(eFlat, 2.0);
        double zetaG = SpectralCurvature.SpectralZeta(eGeo, 2.0);
        double entF = SpectralCurvature.SpectralEntropy(eFlat, 1.0);
        double entG = SpectralCurvature.SpectralEntropy(eGeo, 1.0);

        double dGap = (double.IsFinite(gapF) && double.IsFinite(gapG) && gapF > 1e-12) ? (gapG - gapF) / gapF : 0.0;
        double dZ = (zF - zG) / zF;
        double dZeta = zetaF > 1e-12 ? (zetaF - zetaG) / zetaF : 0.0;
        double dEnt = (entF - entG) / entF;
        return dGap + dZ + dZeta + dEnt;
    }

    /// <summary>Reconstructed curvature sign (−1, 0, +1).</summary>
    public static int Sign(GeometricGraph flat, GeometricGraph geometry)
        => Math.Sign(Score(flat, geometry));
}
