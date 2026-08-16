namespace TQM.Core.ResearchXH;

/// <summary>
/// Native Lorentzian operator candidates (G4-L Phase 0). Each is a symmetric matrix built
/// ONLY from the causal order and counting measure (links, intervals, layers, density) — no
/// BDG weights, no metric tensor, no d'Alembertian formula. Symmetric ⇒ real spectrum.
/// </summary>
public static class LorentzianOperator
{
    /// <summary>L1 — causal-link operator: symmetrized Hasse-link adjacency A + Aᵀ.</summary>
    public static double[,] LinkOperator(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (cs.Link[i, j] || cs.Link[j, i])
                {
                    m[i, j] = 1.0;
                    m[j, i] = 1.0;
                }
        return m;
    }

    /// <summary>L2 — interval operator: raw symmetric interval matrix (zero diagonal).</summary>
    public static double[,] IntervalOperator(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                int v = Math.Max(cs.Interval[i, j], cs.Interval[j, i]);
                if (v >= 0)
                {
                    m[i, j] = v;
                    m[j, i] = v;
                }
            }
        return m;
    }

    /// <summary>
    /// L3 — layer operator: alternating-sign layer adjacency, (−1)^(k+1) over layer k = |[i,j]|
    /// (the native analogue of the BDG binomial alternation, with UNIFORM weights).
    /// </summary>
    public static double[,] LayerOperator(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                int k = Math.Max(cs.Interval[i, j], cs.Interval[j, i]);
                if (k >= 0)
                {
                    double v = (k % 2 == 0) ? -1.0 : 1.0; // (−1)^(k+1)
                    m[i, j] = v;
                    m[j, i] = v;
                }
            }
        return m;
    }

    /// <summary>
    /// L4 — density-weighted causal operator: ρ⁻¹ (A + Aᵀ) ρ⁻¹, the causal-link operator
    /// weighted by the native counting density ρ = past-degree + future-degree (the causal
    /// analogue of Lc = ρ⁻¹ L ρ⁻¹ in the Riemannian sector).
    /// </summary>
    public static double[,] DensityWeightedCausal(CausalSetData cs)
    {
        int n = cs.Count;
        var m1 = LinkOperator(cs);
        var rho = new double[n];
        for (int i = 0; i < n; i++)
            rho[i] = Math.Max(cs.PastDegree[i] + cs.FutureDegree[i], 1.0);

        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                m[i, j] = m1[i, j] / (rho[i] * rho[j]);
        return m;
    }

    /// <summary>Real ascending eigenvalues of a symmetric operator.</summary>
    public static double[] Eigenvalues(double[,] m) => SpectralCurvature.Eigenvalues(m);

    /// <summary>(n+, n−, n0) signature of the spectrum.</summary>
    public static (int pos, int neg, int zero) Signature(double[] evals)
    {
        int pos = 0, neg = 0, zero = 0;
        foreach (double l in evals)
        {
            if (l > 1e-9) pos++;
            else if (l < -1e-9) neg++;
            else zero++;
        }
        return (pos, neg, zero);
    }

    /// <summary>True if the operator is indefinite (has both positive and negative eigenvalues).</summary>
    public static bool IsIndefinite(double[,] m)
    {
        var s = Signature(Eigenvalues(m));
        return s.pos > 0 && s.neg > 0;
    }
}
