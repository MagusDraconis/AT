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

    /// <summary>
    /// BDG d'Alembertian coefficient for interval size k (d = 2): diagonal −2, links (k=0) +4,
    /// next layer (k=1) −2, and 0 beyond. The standard 2D light-cone finite difference.
    /// </summary>
    public static double BdgCoefficient(int k)
        => k switch
        {
            0 => 4.0,
            1 => -2.0,
            _ => 0.0
        };

    /// <summary>
    /// Symmetric d=2 BDG reference operator: B = −2·I + 4·(link adjacency)ᵗ − 2·(next-layer)ᵗ.
    /// A benchmark (not a construction source): used to rank native candidates by similarity.
    /// </summary>
    public static double[,] BdgReference(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++) m[i, i] = -2.0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                int k = Math.Max(cs.Interval[i, j], cs.Interval[j, i]);
                if (k < 0) continue;
                double v = BdgCoefficient(k);
                if (v != 0.0) { m[i, j] = v; m[j, i] = v; }
            }
        return m;
    }

    /// <summary>
    /// Retarded (past-only) BDG d'Alembertian: B[i,j] ≠ 0 only for i ≺ j (forward propagation).
    /// Lower-triangular in time order. Used to probe directionality vs the symmetric candidates.
    /// </summary>
    public static double[,] RetardedBdg(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++) m[i, i] = -2.0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (cs.Order[i, j])
                    m[i, j] = BdgCoefficient(cs.Interval[i, j]);
        return m;
    }

    /// <summary>
    /// Layer profile: mean off-diagonal matrix entry per interval size k (k = 0..maxK).
    /// Captures how an operator weights causal layers (its "interval/layer response").
    /// </summary>
    public static double[] LayerProfile(CausalSetData cs, double[,] m, int maxK = 3)
    {
        var sums = new double[maxK + 1];
        var counts = new int[maxK + 1];
        for (int i = 0; i < cs.Count; i++)
            for (int j = i + 1; j < cs.Count; j++)
            {
                int k = Math.Max(cs.Interval[i, j], cs.Interval[j, i]);
                if (k < 0 || k > maxK) continue;
                sums[k] += m[i, j];
                counts[k]++;
            }
        var p = new double[maxK + 1];
        for (int k = 0; k <= maxK; k++)
            p[k] = counts[k] > 0 ? sums[k] / counts[k] : 0.0;
        return p;
    }

    /// <summary>True if the layer profile alternates sign between layers 0 and 1 (BDG's signature).</summary>
    public static bool Alternates(double[] profile)
        => Math.Sign(profile[0]) != 0 && Math.Sign(profile[0]) * Math.Sign(profile[1]) < 0.0;

    /// <summary>
    /// R1 — past-directed (retarded) layer operator: (−1)^(k+1) over past layers only
    /// (i ≺ j). Strictly lower-triangular in time order → nilpotent (zero spectrum).
    /// </summary>
    public static double[,] PastDirectedLayer(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (cs.Order[i, j])
                    m[i, j] = (cs.Interval[i, j] % 2 == 0) ? -1.0 : 1.0;
        return m;
    }

    /// <summary>R2 — future-directed (advanced) layer operator = transpose of R1.</summary>
    public static double[,] FutureDirectedLayer(CausalSetData cs) => Transpose(PastDirectedLayer(cs));

    /// <summary>R3 — bidirectional layer operator (baseline) = R1 + R2 = symmetric L3.</summary>
    public static double[,] BidirectionalLayer(CausalSetData cs) => LayerOperator(cs);

    /// <summary>Matrix transpose.</summary>
    public static double[,] Transpose(double[,] m)
    {
        int n = m.GetLength(0);
        var t = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                t[i, j] = m[j, i];
        return t;
    }

    /// <summary>
    /// Directed layer profile: (past, future) mean entries per interval size k, where
    /// past[k] = mean over i ≺ j and future[k] = mean over i ≻ j.
    /// </summary>
    public static (double[] past, double[] future) DirectedLayerProfile(CausalSetData cs, double[,] m, int maxK = 3)
    {
        var pastSum = new double[maxK + 1];
        var pastCnt = new int[maxK + 1];
        var futSum = new double[maxK + 1];
        var futCnt = new int[maxK + 1];
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
            {
                if (cs.Order[i, j])
                {
                    int k = cs.Interval[i, j];
                    if (k <= maxK) { pastSum[k] += m[i, j]; pastCnt[k]++; }
                }
                else if (cs.Order[j, i])
                {
                    int k = cs.Interval[j, i];
                    if (k <= maxK) { futSum[k] += m[i, j]; futCnt[k]++; }
                }
            }
        var past = new double[maxK + 1];
        var fut = new double[maxK + 1];
        for (int k = 0; k <= maxK; k++)
        {
            past[k] = pastCnt[k] > 0 ? pastSum[k] / pastCnt[k] : 0.0;
            fut[k] = futCnt[k] > 0 ? futSum[k] / futCnt[k] : 0.0;
        }
        return (past, fut);
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
