namespace TQM.Core.ResearchXH;

using MathNet.Numerics.LinearAlgebra;

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
    /// Retarded (past-only) BDG d'Alembertian: B[j,i] ≠ 0 only for i ≺ j (i past, j future),
    /// so (Bφ)_j sums over past events. Lower-triangular in time order → forward (causal)
    /// Green response.
    /// </summary>
    public static double[,] RetardedBdg(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++) m[i, i] = -2.0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (cs.Order[i, j])  // i ≺ j (i past, j future)
                    m[j, i] = BdgCoefficient(cs.Interval[i, j]);
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
    /// (j ≺ i). Lower-triangular in time order → nilpotent (zero spectrum); its Green
    /// response propagates FORWARD (causal).
    /// </summary>
    public static double[,] PastDirectedLayer(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (cs.Order[j, i])  // j ≺ i (j in past of i)
                    m[i, j] = (cs.Interval[j, i] % 2 == 0) ? -1.0 : 1.0;
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

    /// <summary>Native causal density ρ_i = past-degree + future-degree (counting measure proxy).</summary>
    public static double[] CausalDensity(CausalSetData cs)
    {
        var rho = new double[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            rho[i] = Math.Max(cs.PastDegree[i] + cs.FutureDegree[i], 1.0);
        return rho;
    }

    /// <summary>Number of events comparable to i (causal density over the full order).</summary>
    public static int[] ComparableCount(CausalSetData cs)
    {
        var c = new int[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
                if (cs.Order[i, j] || cs.Order[j, i]) c[i]++;
        return c;
    }

    /// <summary>Number of events in the causal past of i (layer-count self-term source).</summary>
    public static int[] PastCount(CausalSetData cs)
    {
        var c = new int[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
                if (cs.Order[j, i]) c[i]++;
        return c;
    }

    /// <summary>Local link degree of i (past + future Hasse links).</summary>
    public static int[] LocalDegree(CausalSetData cs)
    {
        var d = new int[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            d[i] = cs.PastDegree[i] + cs.FutureDegree[i];
        return d;
    }

    /// <summary>
    /// D2 — interval count: sum over comparable j of 1/(k+1) (near-layer weighted cardinality).
    /// Emphasises the immediate causal layers (links weight 1, next layer 1/2, …).
    /// </summary>
    public static double[] IntervalCount(CausalSetData cs)
    {
        var c = new double[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
            {
                int k;
                if (cs.Order[i, j]) k = cs.Interval[i, j];
                else if (cs.Order[j, i]) k = cs.Interval[j, i];
                else continue;
                c[i] += 1.0 / (k + 1.0);
            }
        return c;
    }

    /// <summary>
    /// D4 — layer occupancy: number of events on the same time slice (simultaneous layer) as i.
    /// Degenerate (constant) on a uniform grid — reported as such.
    /// </summary>
    public static double[] LayerOccupancy(CausalSetData cs)
    {
        var c = new double[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
                if (cs.Time[j] == cs.Time[i]) c[i]++;
        return c;
    }

    /// <summary>
    /// D5 — causal volume: sum over comparable j of (k+1) (interval size + 1). Emphasises the
    /// full causal-interval volume through i (far layers weighted more).
    /// </summary>
    public static double[] CausalVolume(CausalSetData cs)
    {
        var c = new double[cs.Count];
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
            {
                int k;
                if (cs.Order[i, j]) k = cs.Interval[i, j];
                else if (cs.Order[j, i]) k = cs.Interval[j, i];
                else continue;
                c[i] += k + 1.0;
            }
        return c;
    }

    /// <summary>
    /// H0 — retarded interval operator: R1 + A3. Doubles the past (retarded) alternation and
    /// keeps only the interval-decayed future (symmetric remnant) alternation. The Phase-7
    /// base onto which a native diagonal self-term is added.
    /// </summary>
    public static double[,] RetardedInterval(CausalSetData cs)
        => Add(PastDirectedLayer(cs), IntervalWeightedAlternation(cs));

    /// <summary>
    /// NativeLorentzian — the G4-L Phase 7 best operator: H = R1 + A3 + D, where D = −s·(degree/
    /// max-degree) is the negated local-degree self-term at the calibrated strength s (default
    /// 0.75). Size-independent (grid-interior max link degree is 4), so D = −s·degree/4.
    /// Retarded-biased, indefinite, alternating, Feynman tail suppressed to ~0.43.
    /// </summary>
    public static double[,] NativeLorentzian(CausalSetData cs, double strength = 0.75)
        => AddDiagonal(RetardedInterval(cs), DegreeDiagonal(cs, strength));

    /// <summary>Negated local-degree self-term: D_i = −s·degree_i/max(degree) (s = 0.75 default).</summary>
    public static double[] DegreeDiagonal(CausalSetData cs, double strength = 0.75)
    {
        var deg = LocalDegree(cs).Select(x => (double)x).ToArray();
        double norm = deg.Length == 0 ? 1.0 : deg.Max();
        return norm == 0.0 ? new double[deg.Length] : deg.Select(x => -strength * x / norm).ToArray();
    }

    /// <summary>
    /// RetardedPropagator — the strictly causal dual object: G = D + 2R1 (lower-triangular, no
    /// future coupling). Causal (leak ≈ 0) but elliptic (not indefinite) — the native analogue
    /// of the retarded Green function's kernel.
    /// </summary>
    public static double[,] RetardedPropagator(CausalSetData cs, double strength = 0.75)
        => AddDiagonal(Scale(PastDirectedLayer(cs), 2.0), DegreeDiagonal(cs, strength));

    /// <summary>
    /// SignatureOperator — the indefinite dual object: S = H2 + D = 2R1 + R2 + D (full future
    /// coupling). Lorentzian signature (indefinite) but Feynman (time-symmetric, leaks) — the
    /// native analogue of the symmetric d'Alembertian □.
    /// </summary>
    public static double[,] SignatureOperator(CausalSetData cs, double strength = 0.75)
        => AddDiagonal(HybridRetardedAlternating(cs), DegreeDiagonal(cs, strength));

    /// <summary>Add a per-vertex diagonal d to a square matrix (returns a copy).</summary>
    public static double[,] AddDiagonal(double[,] m, double[] d)
    {
        int n = m.GetLength(0);
        var r = (double[,])m.Clone();
        for (int i = 0; i < n; i++) r[i, i] += d[i];
        return r;
    }

    /// <summary>Scale a square matrix by a scalar (returns a copy).</summary>
    public static double[,] Scale(double[,] m, double s)
    {
        int n = m.GetLength(0);
        var r = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                r[i, j] = s * m[i, j];
        return r;
    }

    /// <summary>
    /// A3 — retarded interval alternation: past layers carry full weight (−1)^(k+1), future
    /// layers carry a decayed weight (−1)^(k+1)/(k+1). Retarded-biased by interval distance.
    /// </summary>
    public static double[,] IntervalWeightedAlternation(CausalSetData cs)
    {
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (cs.Order[j, i])  // j ≺ i (past): full weight
                {
                    int k = cs.Interval[j, i];
                    m[i, j] = (k % 2 == 0) ? -1.0 : 1.0;
                }
                else if (cs.Order[i, j])  // i ≺ j (future): interval-decayed
                {
                    int k = cs.Interval[i, j];
                    m[i, j] = ((k % 2 == 0) ? -1.0 : 1.0) / (k + 1.0);
                }
            }
        return m;
    }

    /// <summary>Matrix addition.</summary>
    public static double[,] Add(double[,] a, double[,] b)
    {
        int n = a.GetLength(0);
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                m[i, j] = a[i, j] + b[i, j];
        return m;
    }

    /// <summary>
    /// H2 — retarded alternating-layer operator: R1 + L3. Combines the retarded (past-only)
    /// layer operator with the symmetric alternating operator to be forward-BIASED (retarded)
    /// while keeping an indefinite (both-sign) spectral structure.
    /// </summary>
    public static double[,] HybridRetardedAlternating(CausalSetData cs)
        => Add(PastDirectedLayer(cs), BidirectionalLayer(cs));

    /// <summary>
    /// H3 — retarded density-weighted layer operator: ρ⁻¹ (R1 + L3) ρ⁻¹, the hybrid weighted
    /// by the native causal density (the Lorentzian analogue of Lc's density weighting).
    /// </summary>
    public static double[,] HybridRetardedDensityWeighted(CausalSetData cs)
    {
        var h = HybridRetardedAlternating(cs);
        var rho = CausalDensity(cs);
        int n = cs.Count;
        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                m[i, j] = h[i, j] / (rho[i] * rho[j]);
        return m;
    }

    /// <summary>
    /// Green response: the propagated field φ solving op · φ = δ_source. Uses a direct solve
    /// (exact inverse) when op is invertible, and the pseudoinverse (minimum-norm) otherwise.
    /// Its support reveals the propagation cone, directionality, and wave speed.
    /// </summary>
    public static double[] GreenResponse(double[,] op, int source)
    {
        int n = op.GetLength(0);
        var m = Matrix<double>.Build.DenseOfArray(op);
        var e = Vector<double>.Build.Dense(n, 0.0);
        e[source] = 1.0;
        try
        {
            var r = m.Solve(e).ToArray();
            if (r.All(x => !double.IsNaN(x) && !double.IsInfinity(x))) return r;
        }
        catch
        {
            // singular — fall through to pseudoinverse
        }
        return (m.PseudoInverse() * e).ToArray();
    }

    /// <summary>
    /// Measures a Green response relative to a source at (tc, xc): past/future support, causal
    /// front velocity (within the light cone |Δx| &lt; Δt), and spacelike leakage (response
    /// outside the causal future, as a fraction of the total).
    /// </summary>
    public static (double past, double future, double causalFront, double leak) GreenResponseMetrics(
        CausalSetData cs, double[] resp, int tc, int xc)
    {
        double maxAbs = resp.Max(x => Math.Abs(x));
        double thresh = 1e-9 * Math.Max(maxAbs, 1.0);
        double past = 0.0, future = 0.0, causalFront = 0.0, leak = 0.0, total = 0.0;
        for (int j = 0; j < cs.Count; j++)
        {
            double a = Math.Abs(resp[j]);
            if (a < thresh) continue;
            total += a;
            int dt = cs.Time[j] - tc;
            int dx = Math.Abs(cs.Space[j] - xc);
            if (dt < 0) { past += a; leak += a; }
            else if (dt == 0) { if (dx > 0) leak += a; }
            else
            {
                future += a;
                if (dx < dt) causalFront = Math.Max(causalFront, dx / (double)dt);
                else leak += a;
            }
        }
        return (past, future, causalFront, total > 0.0 ? leak / total : 0.0);
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
