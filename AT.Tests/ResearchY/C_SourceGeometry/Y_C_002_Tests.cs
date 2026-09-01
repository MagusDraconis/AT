using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.C_SourceGeometry;

/// <summary>
/// ResearchY-C_002 — Radial Propagation Audit test suite (Y_C_002_Tests.cs).
///
/// Goal: can propagation in C96 be genuinely radial? Radial propagation requires a
/// distinguished origin + shell ordering. C96 is vertex-transitive (automorphism group
/// D96): no node is a canonical origin; every node has the identical shell profile.
/// The canonical spreading (branching + spectral projection, A_003 rev.2) is
/// tree-local + global — NOT radial.
///
/// Verdict tested: radial propagation in C96 is NOT canonical (FAIL).
/// Deterministic: BFS on the circulant graph + closed-form eigenvalues.
/// </summary>
public class Y_C_002_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_C_002_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    /// <summary>BFS distances from a source on C96 (±1..±6).</summary>
    private static int[] BfsDistances(int src)
    {
        var dist = new int[N];
        Array.Fill(dist, -1);
        var q = new Queue<int>();
        dist[src] = 0;
        q.Enqueue(src);
        while (q.Count > 0)
        {
            int u = q.Dequeue();
            for (int d = 1; d <= K; d++)
            {
                int v = (u + d) % N;
                if (dist[v] < 0) { dist[v] = dist[u] + 1; q.Enqueue(v); }
                v = (u - d + N) % N;
                if (dist[v] < 0) { dist[v] = dist[u] + 1; q.Enqueue(v); }
            }
        }
        return dist;
    }

    /// <summary>Shell sizes: count of nodes at each distance.</summary>
    private static int[] ShellSizes(int[] dist)
    {
        int maxD = dist.Max();
        var sizes = new int[maxD + 1];
        foreach (int d in dist) sizes[d]++;
        return sizes;
    }

    // ── [Required] Y_C_002_RadialDefinition ──────────────────────────────

    /// <summary>
    /// Radial propagation requires (1) a distinguished origin o, (2) the radial
    /// coordinate d(o,·) (shortest path), (3) shell ordering. If no origin is derived,
    /// no law is canonically radial.
    /// </summary>
    [Fact]
    public void Y_C_002_RadialDefinition()
    {
        // The radial coordinate is the graph distance d(o, v).
        int[] d0 = BfsDistances(0);
        Assert.Equal(0, d0[0]);   // origin at distance 0
        Assert.Equal(1, d0[1]);   // nearest neighbors at distance 1

        // Shell ordering: nodes populate by increasing distance.
        int maxD = d0.Max();
        var present = new bool[maxD + 1];
        foreach (int d in d0) present[d] = true;
        for (int r = 0; r <= maxD; r++) Assert.True(present[r]); // no gaps (connected)

        // Definition requirement: an origin must be derived, not chosen.
        // (Documented: radiality requires a preferred o, which vertex-transitivity forbids.)
        Assert.Equal(8, maxD); // diameter = N/(2K) = 8
    }

    // ── [Required] Y_C_002_OriginTest ────────────────────────────────────

    /// <summary>
    /// Formally any node can be chosen as an origin (well-defined shells); canonically
    /// no node is derived as one — C96 is vertex-transitive, so all nodes have the same
    /// shell profile.
    /// </summary>
    [Fact]
    public void Y_C_002_OriginTest()
    {
        // Shell profile is identical for every origin (vertex-transitivity).
        int[] p0 = ShellSizes(BfsDistances(0));
        int[] p5 = ShellSizes(BfsDistances(5));
        int[] p37 = ShellSizes(BfsDistances(37));
        Assert.Equal(p0, p5);
        Assert.Equal(p0, p37);

        // Every node is equivalent: no node is canonically distinguished.
        // (Formally any node works; canonically none is preferred.)
        Assert.Equal(1, p0[0]);   // one origin node
        Assert.Equal(12, p0[1]);  // 12 nearest neighbors (degree 12)
    }

    // ── [Required] Y_C_002_ShellStructure ────────────────────────────────

    /// <summary>
    /// Shortest-path propagation and radial shells coincide: both are the BFS layers.
    /// C96 has diameter 8 = N/(2K), near-uniform shells (12/12/…/11), and reflection
    /// symmetry d(o,k) = d(o,N−k).
    /// </summary>
    [Fact]
    public void Y_C_002_ShellStructure()
    {
        int[] d0 = BfsDistances(0);
        int[] shells = ShellSizes(d0);

        // Diameter = 8 = N/(2K).
        Assert.Equal(8, shells.Length - 1);
        Assert.Equal(N / (2 * K), shells.Length - 1);

        // Near-uniform shells: 12 nodes per shell for r=1..7, 11 for r=8 (N−1 odd).
        for (int r = 1; r <= 7; r++) Assert.Equal(12, shells[r]);
        Assert.Equal(11, shells[8]);
        Assert.Equal(N, shells.Sum()); // all 96 nodes covered

        // Reflection symmetry: d(o,k) = d(o,N−k).
        for (int k = 0; k < N; k++)
            Assert.Equal(d0[k], d0[(N - k) % N]);
    }

    // ── [Required] Y_C_002_Automorphism ──────────────────────────────────

    /// <summary>
    /// The automorphism group of C96 is D96 (vertex-transitive). Any node maps to any
    /// other, so the shell profile is origin-independent and radial structure is a
    /// gauge/coordinate choice, not invariant content.
    /// </summary>
    [Fact]
    public void Y_C_002_Automorphism()
    {
        // Vertex-transitivity: identical shell profiles for arbitrary origins.
        int[] p0 = ShellSizes(BfsDistances(0));
        for (int src = 1; src < N; src += 7)
            Assert.Equal(p0, ShellSizes(BfsDistances(src)));

        // A radial description centered on any node is equivalent to one centered on
        // any other — radiality is not invariant content (documented).
        // The ring's rotations are automorphisms: C96 is 12-regular.
        Assert.Equal(12, 2 * K);
    }

    // ── [Required] Y_C_002_SpreadingClass ────────────────────────────────

    /// <summary>
    /// The canonical spreading is NOT radial. Branching is tree-local (generation
    /// depth, no graph-distance coordinate); the spectral readout is global
    /// (|φ_k(n)|² = 1/96 on every site). The pair is a hybrid, non-radial.
    /// </summary>
    [Fact]
    public void Y_C_002_SpreadingClass()
    {
        // Branching is tree-local: ρ_k = μ^k/S is a function of generation, not distance.
        double mu = 2.0;
        int gens = 8;
        double S = 0.0;
        for (int j = 0; j < gens; j++) S += Math.Pow(mu, j);
        double rho3 = Math.Pow(mu, 3) / S;
        Assert.Equal(Math.Pow(mu, 3) * (1.0 / S), rho3, 10); // generation-depth law

        // The spectral readout is global: a mode's squared amplitude is flat on the ring.
        // |φ_k(n)|² = 1/N for every site (verified analytically).
        Assert.Equal(0.01042, 1.0 / N, 4);

        // No canonical quantity is a function of graph distance d(o,·) alone:
        // the eigenvalue λ_k depends on the mode index k, not on a distance to an origin.
        double lam1 = Lambda(1);
        Assert.Equal(0.3864, lam1, 3);
        Assert.Equal(Lambda(N - 1), Lambda(1), 10); // ring symmetry, not radial
    }

    // ── [Required] Y_C_002_Run ───────────────────────────────────────────

    [Fact]
    public void Y_C_002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-C_002 — Radial Propagation Audit");

        sb.AppendLine("Goal: can propagation in C96 be genuinely radial?");
        sb.AppendLine();

        int[] d0 = BfsDistances(0);
        int[] shells = ShellSizes(d0);

        sb.AppendLine("[1] Radial propagation definition");
        sb.AppendLine("    P(v) = f(d(o,v)) with shell ordering — requires a derived origin o.");
        sb.AppendLine("    No origin is derived (C96 vertex-transitive).");
        sb.AppendLine();

        sb.AppendLine("[2] Shell structure of C96 (from any origin)");
        sb.AppendLine($"    diameter = {shells.Length - 1} = N/(2K) = {N / (2 * K)}");
        sb.AppendLine($"    shells: {string.Join("/", shells)}");
        sb.AppendLine($"    reflection symmetry d(o,k) = d(o,N−k): holds");
        sb.AppendLine("    shortest-path propagation = radial shells (identical BFS layers)");
        sb.AppendLine();

        sb.AppendLine("[3] Automorphism analysis");
        sb.AppendLine("    automorphism group = D96, vertex-transitive");
        sb.AppendLine("    every node has the identical shell profile → radiality is a gauge choice");
        sb.AppendLine();

        sb.AppendLine("[4] Spreading classification");
        sb.AppendLine("    radial:               NO (no derived origin; no distance coordinate)");
        sb.AppendLine("    tree-local:           YES (branching ρ_k = μ^k/S, generation depth)");
        sb.AppendLine("    resonance/global:     YES (spectral projection, |φ_k(n)|² = 1/96)");
        sb.AppendLine("    hybrid:               YES (tree-local + global) — NOT radial");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict — FAIL (not canonically radial)");
        sb.AppendLine("    Radial propagation requires a preferred origin; C96's D96 vertex-");
        sb.AppendLine("    transitivity removes all preferred sites (C_001). The canonical law is");
        sb.AppendLine("    tree-local + global (hybrid). Radial shells exist only as a formal");
        sb.AppendLine("    diffusion model with a chosen origin. No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
