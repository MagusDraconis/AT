namespace AT.Core.ResearchXH;

/// <summary>The three terms of the G4-P decomposition Lc = −Δ_g + V (d = 2).</summary>
public enum CurvatureTermKind
{
    /// <summary>−Δ_g only: the Laplace–Beltrami part (= Lc with the potential subtracted).</summary>
    DeltaGOnly,

    /// <summary>V = Δρ/ρ² only: the native zeroth-order curvature potential (diagonal).</summary>
    PotentialOnly,

    /// <summary>Full operator: Lc = ρ⁻¹ L ρ⁻¹ = −Δ_g + V.</summary>
    Full
}

/// <summary>
/// G4-P Phase 1 — isolate the native curvature potential. The Phase-0 analytic result is
/// Lc = ρ⁻¹ L ρ⁻¹ = −c Δ_g + c·V, with V = Δρ/ρ² (d = 2). For the conformal profile ρ = 1 + a·x²
/// the Laplacian is Δρ = 2a and a = −R(0)/4, so V_i = 2a/ρ_i². This class builds the three terms
/// (−Δ_g, V, and their sum Lc) so their individual contribution to curvature reconstruction can be
/// measured. No new primitives: only ρ and L.
/// </summary>
public static class CurvaturePotential
{
    /// <summary>Analytic profile coefficient a = −R(0)/4.</summary>
    public static double Coefficient(GeometricGraph g) => -g.ScalarCurvature / 4.0;

    /// <summary>Native curvature potential V_i = Δρ(x_i)/ρ(x_i)² = 2a/ρ_i² (d = 2).</summary>
    public static double[] Potential(GeometricGraph g)
    {
        double a = Coefficient(g);
        return g.VertexDensity().Select(r => 2.0 * a / (r * r)).ToArray();
    }

    /// <summary>Build the operator for a curvature term.</summary>
    public static double[,] Build(GeometricGraph g, CurvatureTermKind kind)
    {
        var lc = ConformalOperator.Build(g, ConformalOperatorKind.RhoInverseSquared);
        if (kind == CurvatureTermKind.Full) return lc;

        var v = Potential(g);
        int n = g.VertexCount;
        if (kind == CurvatureTermKind.PotentialOnly)
        {
            var m = new double[n, n];
            for (int i = 0; i < n; i++) m[i, i] = v[i];
            return m;
        }

        // DeltaGOnly: −Δ_g = Lc − V (subtract the potential off the diagonal).
        var r = (double[,])lc.Clone();
        for (int i = 0; i < n; i++) r[i, i] -= v[i];
        return r;
    }

    public static double[] Eigenvalues(GeometricGraph g, CurvatureTermKind kind)
        => SpectralCurvature.Eigenvalues(Build(g, kind));

    /// <summary>Curvature score of a term for a geometry, referenced to the flat graph (robust).</summary>
    public static double Score(GeometricGraph flat, GeometricGraph geometry, CurvatureTermKind kind)
        => CurvatureReconstruction.ScoreRobust(Eigenvalues(flat, kind), Eigenvalues(geometry, kind));
}
