using System.Globalization;

namespace AT.Core.ResearchT;

/// <summary>Per-family spectral-rigidity summary.</summary>
public sealed record FamilyRigidity(
    string Family,
    int Count,                    // labeled member graphs (or sample size for random-sparse)
    int Degenerate,               // members with an isospectral partner
    double IsospectralFrequency,  // Degenerate / Count
    double Rigidity,              // 1 − IsospectralFrequency
    double PerturbationStability);

/// <summary>
/// Spectral rigidity audit. Measures, for each graph family, the frequency of isospectral
/// (spectrum-sharing, non-isomorphic) partners and the resulting rigidity R = 1 − P(partner),
/// plus the stability of the rigidity classification under single-edge perturbation.
/// "Rigid" means the graph is uniquely determined by its Laplacian spectrum (no non-isomorphic
/// isospectral partner). Deterministic throughout.
/// </summary>
public static class SpectralRigidityAudit
{
    // ── Family classifiers ──────────────────────────────────────────────────

    public static bool IsCirculant(double[,] adj)
    {
        int n = adj.GetLength(0);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (adj[i, j] != adj[(i + 1) % n, (j + 1) % n])
                    return false;
        return true;
    }

    public static bool IsBipartite(double[,] adj)
    {
        int n = adj.GetLength(0);
        var color = new int[n];
        Array.Fill(color, -1);
        for (int s = 0; s < n; s++)
        {
            if (color[s] != -1) continue;
            color[s] = 0;
            var q = new Queue<int>();
            q.Enqueue(s);
            while (q.Count > 0)
            {
                int u = q.Dequeue();
                for (int v = 0; v < n; v++)
                {
                    if (adj[u, v] == 0.0) continue;
                    if (color[v] == -1) { color[v] = 1 - color[u]; q.Enqueue(v); }
                    else if (color[v] == color[u]) return false;
                }
            }
        }
        return true;
    }

    public static bool IsPath(double[,] adj)
    {
        int n = adj.GetLength(0);
        int[] deg = GeneralInverseSpectrumAnalyzer.DegreeSequence(adj);
        if (n == 1) return true;
        if (n == 2) return deg[0] == 1 && deg[1] == 1;
        int ones = deg.Count(d => d == 1);
        int twos = deg.Count(d => d == 2);
        return ones == 2 && twos == n - 2 && IsConnected(adj);
    }

    public static bool IsCycle(double[,] adj)
    {
        int[] deg = GeneralInverseSpectrumAnalyzer.DegreeSequence(adj);
        return deg.All(d => d == 2) && IsConnected(adj);
    }

    public static bool IsComplete(double[,] adj)
    {
        int n = adj.GetLength(0);
        return GeneralInverseSpectrumAnalyzer.EdgeCount(adj) == n * (n - 1) / 2;
    }

    /// <summary>2×3 grid proxy: connected, bipartite, degree multiset {2,2,3,3,3,3} (n=6).</summary>
    public static bool IsGrid(double[,] adj)
    {
        int n = adj.GetLength(0);
        if (n != 6) return false;
        int[] deg = GeneralInverseSpectrumAnalyzer.DegreeSequence(adj);
        int[] expected = { 2, 2, 3, 3, 3, 3 };
        return deg.SequenceEqual(expected) && IsBipartite(adj) && IsConnected(adj);
    }

    private static bool IsConnected(double[,] adj)
    {
        int n = adj.GetLength(0);
        var visited = new bool[n];
        var q = new Queue<int>();
        visited[0] = true;
        q.Enqueue(0);
        while (q.Count > 0)
        {
            int u = q.Dequeue();
            for (int v = 0; v < n; v++)
                if (adj[u, v] != 0.0 && !visited[v]) { visited[v] = true; q.Enqueue(v); }
        }
        return visited.All(x => x);
    }

    // ── Degeneracy ─────────────────────────────────────────────────────────

    private static string SpecKey(double[] spec)
        => string.Join(",", spec.Select(x => x.ToString("F6", CultureInfo.InvariantCulture)));

    private static string SpecOf(double[,] adj)
        => SpecKey(GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(adj)));

    /// <summary>
    /// The set of spectra shared by ≥2 distinct degree sequences — i.e. the spectra of
    /// graphs that have a non-isomorphic isospectral partner (a lower bound: same-degree
    /// non-isomorphic pairs are not detected).
    /// </summary>
    public static HashSet<string> DegenerateSpectra(int n)
    {
        int maxMask = 1 << (n * (n - 1) / 2);
        var degSets = new Dictionary<string, HashSet<string>>();
        for (int mask = 0; mask < maxMask; mask++)
        {
            var adj = GeneralInverseSpectrumAnalyzer.GraphFromMask(n, mask);
            string key = SpecOf(adj);
            if (!degSets.TryGetValue(key, out var set)) { set = new HashSet<string>(); degSets[key] = set; }
            set.Add(string.Join(",", GeneralInverseSpectrumAnalyzer.DegreeSequence(adj)));
        }
        var degenerate = new HashSet<string>();
        foreach (var kv in degSets)
            if (kv.Value.Count >= 2) degenerate.Add(kv.Key);
        return degenerate;
    }

    // ── Audit ──────────────────────────────────────────────────────────────

    public static List<FamilyRigidity> Audit(
        int n,
        int randomSamples = 200,
        double randomDensity = 0.3,
        int seed = 42,
        int stabilitySampleCap = 60)
    {
        int maxEdges = n * (n - 1) / 2;
        int maxMask = 1 << maxEdges;
        var degenerate = DegenerateSpectra(n);

        var families = new[] { "all", "circulant", "path", "cycle", "grid", "complete", "bipartite" };
        var small = new HashSet<string> { "circulant", "path", "cycle", "grid", "complete" };
        var counts = families.ToDictionary(f => f, _ => 0);
        var degen = families.ToDictionary(f => f, _ => 0);
        var stabSamples = families.ToDictionary(f => f, _ => new List<double[,]>());
        int largeStride = Math.Max(1, maxMask / stabilitySampleCap);

        // Single exhaustive pass over all labeled graphs.
        for (int mask = 0; mask < maxMask; mask++)
        {
            var adj = GeneralInverseSpectrumAnalyzer.GraphFromMask(n, mask);
            bool isDeg = degenerate.Contains(SpecOf(adj));

            counts["all"]++; if (isDeg) degen["all"]++;
            if (mask % largeStride == 0) stabSamples["all"].Add(adj);

            if (IsCirculant(adj)) { counts["circulant"]++; if (isDeg) degen["circulant"]++; if (small.Contains("circulant")) stabSamples["circulant"].Add(adj); }
            if (IsPath(adj)) { counts["path"]++; if (isDeg) degen["path"]++; stabSamples["path"].Add(adj); }
            if (IsCycle(adj)) { counts["cycle"]++; if (isDeg) degen["cycle"]++; stabSamples["cycle"].Add(adj); }
            if (IsGrid(adj)) { counts["grid"]++; if (isDeg) degen["grid"]++; stabSamples["grid"].Add(adj); }
            if (IsComplete(adj)) { counts["complete"]++; if (isDeg) degen["complete"]++; stabSamples["complete"].Add(adj); }
            if (IsBipartite(adj)) { counts["bipartite"]++; if (isDeg) degen["bipartite"]++; if (mask % largeStride == 0) stabSamples["bipartite"].Add(adj); }
        }

        var result = new List<FamilyRigidity>();
        foreach (var f in families)
        {
            int c = counts[f], d = degen[f];
            double freq = c == 0 ? 0.0 : (double)d / c;
            double rig = c == 0 ? 1.0 : 1.0 - freq;
            result.Add(new FamilyRigidity(f, c, d, freq, rig, AverageStability(stabSamples[f], degenerate, n, maxEdges)));
        }

        // random-sparse family (seeded sampling).
        var randomGraphs = new List<double[,]>();
        for (int s = 0; s < randomSamples; s++)
            randomGraphs.Add(GeneralInverseSpectrumAnalyzer.RandomSparseGraph(n, randomDensity, seed + s));
        int rc = randomGraphs.Count, rd = 0;
        foreach (var g in randomGraphs)
            if (degenerate.Contains(SpecOf(g))) rd++;
        double rfreq = (double)rd / rc;
        result.Add(new FamilyRigidity("random-sparse", rc, rd, rfreq, 1.0 - rfreq, AverageStability(randomGraphs, degenerate, n, maxEdges)));

        return result.OrderByDescending(r => r.Rigidity).ToList();
    }

    private static double AverageStability(List<double[,]> graphs, HashSet<string> degenerate, int n, int maxEdges)
    {
        if (graphs.Count == 0) return double.NaN;
        var edges = new List<(int, int)>();
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++) edges.Add((i, j));

        double total = 0.0;
        foreach (var adj in graphs)
        {
            bool baseRigid = !degenerate.Contains(SpecOf(adj));
            int same = 0;
            foreach (var (i, j) in edges)
            {
                var flipped = (double[,])adj.Clone();
                flipped[i, j] = 1.0 - flipped[i, j];
                flipped[j, i] = flipped[i, j];
                bool flipRigid = !degenerate.Contains(SpecOf(flipped));
                if (flipRigid == baseRigid) same++;
            }
            total += (double)same / edges.Count;
        }
        return total / graphs.Count;
    }
}
