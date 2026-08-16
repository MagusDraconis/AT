namespace TQM.Core.ResearchXH;

/// <summary>
/// G4-M Phase 0 — native reconstruction of conformal structure. Uses only the causal order and
/// the counting measure ρ (event density) to recover conformal information, WITHOUT invoking
/// Malament's theorem, a metric tensor, or an imported conformal class.
///
/// Concretely: on the 1+1D Minkowski grid the causal order is the SAME for every conformal
/// geometry (it is the conformal class); the counting measure ρ(x) = 1 + a·(x/xMax)² carries the
/// conformal factor. Native observables split accordingly:
///   - causal distance (longest chain) is a conformal INVARIANT (same for all a),
///   - interval volume (weighted by ρ) and layer growth reconstruct the conformal FACTOR.
/// </summary>
public static class ConformalStructure
{
    /// <summary>Per-event density ρ_i = 1 + a·(Space[i]/xMax)².</summary>
    public static double[] Density(CausalSetData cs, int xMax, double a)
    {
        var rho = new double[cs.Count];
        for (int i = 0; i < cs.Count; i++)
        {
            double u = cs.Space[i] / (double)xMax;
            rho[i] = 1.0 + a * u * u;
        }
        return rho;
    }

    /// <summary>Row-major event index of (t, x) on a BuildGrid grid.</summary>
    public static int Index(int t, int x, int xMax) => t * (2 * xMax + 1) + (x + xMax);

    /// <summary>
    /// Interval-volume profile: V(x₀) = Σ_{z: (0,x₀) ≺ z ≺ (tMax,x₀)} ρ(z) — the counting-measure
    /// mass inside the causal diamond of the vertical pair at x₀.
    /// </summary>
    public static double[] IntervalVolumeProfile(CausalSetData cs, double[] rho, int tMax, int xMax)
    {
        var v = new double[2 * xMax + 1];
        for (int x0 = -xMax; x0 <= xMax; x0++)
        {
            int bottom = Index(0, x0, xMax);
            int top = Index(tMax, x0, xMax);
            double sum = 0.0;
            for (int z = 0; z < cs.Count; z++)
                if (cs.Order[bottom, z] && cs.Order[z, top])
                    sum += rho[z];
            v[x0 + xMax] = sum;
        }
        return v;
    }

    /// <summary>Layer growth from `from`: the counting-measure mass at each causal interval k.</summary>
    public static double[] LayerGrowth(CausalSetData cs, double[] rho, int from, int maxK)
    {
        var layers = new double[maxK + 1];
        for (int z = 0; z < cs.Count; z++)
        {
            if (z == from) continue;
            int k = cs.Order[from, z] ? cs.Interval[from, z] : (cs.Order[z, from] ? cs.Interval[z, from] : -1);
            if (k >= 0 && k <= maxK) layers[k] += rho[z];
        }
        return layers;
    }

    /// <summary>Causal distance proxy: the longest chain (number of events) from `from` to `to`.</summary>
    public static int LongestChain(CausalSetData cs, int from, int to)
    {
        int n = cs.Count;
        var len = new int[n];
        for (int i = 0; i < n; i++) len[i] = -1;
        len[from] = 1;
        for (int i = 0; i < n; i++)
        {
            if (len[i] < 0) continue;
            for (int j = i + 1; j < n; j++)
                if (cs.Order[i, j]) len[j] = Math.Max(len[j], len[i] + 1);
        }
        return len[to];
    }

    /// <summary>Total counting-measure mass.</summary>
    public static double TotalMass(double[] rho) => rho.Sum();
}
