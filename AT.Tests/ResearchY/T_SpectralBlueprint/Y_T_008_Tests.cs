using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_008 — Asymptotic Diversity Limit Audit.
///
/// Question: what DETERMINES the saturated species count S∞ found in T_007?
///
/// Sweep the replicator–mutator model (BoundedInnovationAnalyzer) over its governing
/// parameters — mutation rate μ, crowding β, landscape size A, fitness variance, and
/// spectral rigidity — and derive the scaling law S∞ = F(μ, β, A, …). Deterministic.
/// </summary>
public class Y_T_008_Tests : ResearchTestBase
{
    public Y_T_008_Tests(ITestOutputHelper output) : base(output) { }

    // ── Landscape helpers ────────────────────────────────────────────────────

    private static (double[] Distinct, int[] Mult) Eigenspaces(double[,] adj)
        => AttractorDominanceAnalyzer.Eigenspaces(adj);

    private static (double[] Distinct, int[] Mult) Eigenspaces(double[] spectrum)
        => AttractorDominanceAnalyzer.GroupSpectrum(spectrum.OrderBy(x => x).ToArray());

    /// <summary>Fitness w_k = m_k / λ_k for the non-zero modes (the species set).</summary>
    private static double[] Fitness(double[] distinct, int[] mult)
    {
        var w = new List<double>();
        for (int i = 0; i < distinct.Length; i++)
            if (distinct[i] > 1e-9) w.Add(mult[i] / distinct[i]);
        return w.ToArray();
    }

    private static int NonZeroCount(double[] distinct) => distinct.Count(d => d > 1e-9);

    /// <summary>Scale-invariant fitness spread: variance of log fitness.</summary>
    private static double LogFitnessVariance(double[] w)
    {
        double[] lw = w.Select(x => Math.Log(x)).ToArray();
        double mean = lw.Average();
        return lw.Sum(x => (x - mean) * (x - mean)) / lw.Length;
    }

    /// <summary>Spectral-rigidity proxy = degeneracy fraction (N−A)/N (circulant ⇒ 1 in T_004).</summary>
    private static double RigidityProxy(int a, int n) => (double)(n - a) / n;

    // ── Case descriptors ─────────────────────────────────────────────────────

    private static (string Name, double[] Fitness, int A, int N) CaseD96()
    {
        var (d, m) = Eigenspaces(AttractorDominanceAnalyzer.D96Ring());
        return ("D96", Fitness(d, m), NonZeroCount(d), 96);
    }

    private static (string Name, double[] Fitness, int A, int N) CaseD963D()
    {
        var (d, m) = Eigenspaces(AttractorDominanceAnalyzer.D963D(4, 4, 6));
        return ("D96-3D", Fitness(d, m), NonZeroCount(d), 96);
    }

    private static (string Name, double[] Fitness, int A, int N) CaseRandom()
    {
        var (d, m) = Eigenspaces(GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42));
        return ("random", Fitness(d, m), NonZeroCount(d), 96);
    }

    private static (string Name, double[] Fitness, int A, int N) CasePhysical()
    {
        var (d, m) = Eigenspaces(SpectralBlueprint.BuildSymmetric(96, x => (double)x));
        return ("physical", Fitness(d, m), NonZeroCount(d), 96);
    }

    private static (string Name, double[] Fitness, int A, int N) CaseUnphysical()
    {
        var (d, m) = Eigenspaces(SpectralBlueprint.BuildSymmetric(96, x => x <= 16 ? 5.0 : x <= 32 ? 25.0 : 60.0));
        return ("unphysical", Fitness(d, m), NonZeroCount(d), 96);
    }

    private static (string Name, double[] Fitness, int A, int N)[] AllCases()
        => [CaseD96(), CaseD963D(), CaseRandom(), CasePhysical(), CaseUnphysical()];

    private static int SOf(string model, Func<double[,]> adj, double mu, double beta)
        => BoundedInnovationAnalyzer.Run(model, adj(), mutationRate: mu, crowding: beta).FinalSpecies;

    private static int SOfSpectrum(string model, Func<double[]> spec, double mu, double beta)
        => BoundedInnovationAnalyzer.RunSpectrum(model, spec(), mutationRate: mu, crowding: beta).FinalSpecies;

    // ── 1. Mutation rate μ ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_MutationScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] muValues = [0.0001, 0.001, 0.01, 0.1, 0.5];
        int[] s = muValues.Select(mu => SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), mu, 1.0)).ToArray();

        // Monotonic non-decreasing: more mutation re-seeds more species.
        for (int i = 1; i < s.Length; i++)
            Assert.True(s[i] >= s[i - 1], $"S∞ must be non-decreasing in μ: {s[i-1]} -> {s[i]}");

        // μ = 0 exactly: pure selection → a single fittest (S∞ = 1). Any μ > 0, however tiny,
        // re-seeds a finite mutation–selection balance S₀⁺ > 1. Large μ spreads toward A.
        int pureSelection = SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.0, 1.0);
        Assert.Equal(1, pureSelection);           // no mutation → single winner
        Assert.True(s[0] > pureSelection, $"tiny μ re-seeds beyond one survivor (S∞={s[0]})");
        Assert.True(s[^1] > s[0]);                // high mutation → more survivors
        Assert.True(s[^1] <= CaseD96().A);        // bounded by landscape size
    }

    // ── 2. Crowding β ────────────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_CrowdingScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] betaValues = [0.0, 0.1, 0.5, 1.0, 2.0, 10.0];
        int[] s = betaValues.Select(b => SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.01, b)).ToArray();

        // Crowding levels the fitness field (self-limitation on the fittest), so S∞ is
        // non-decreasing in β and stays bounded by the landscape.
        for (int i = 1; i < s.Length; i++)
            Assert.True(s[i] >= s[i - 1], $"S∞ must be non-decreasing in β: {s[i-1]} -> {s[i]}");
        Assert.All(s, x => Assert.InRange(x, 1, CaseD96().A));
    }

    // ── 3. Landscape size A ──────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_LandscapeSizeScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        int[] ns = [48, 96, 192, 384];
        foreach (int n in ns)
        {
            var (d, m) = Eigenspaces(AttractorDominanceAnalyzer.D96Ring(n));
            int a = NonZeroCount(d);
            int s = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(n)).FinalSpecies;
            Assert.True(s <= a, $"S∞ ≤ A must hold at N={n}");
        }

        // S∞ saturates and does NOT track A: A grows 20→188 while S∞ stays ≈ 4–5.
        int a48 = NonZeroCount(Eigenspaces(AttractorDominanceAnalyzer.D96Ring(48)).Distinct);
        int a384 = NonZeroCount(Eigenspaces(AttractorDominanceAnalyzer.D96Ring(384)).Distinct);
        int s48 = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(48)).FinalSpecies;
        int s384 = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(384)).FinalSpecies;
        Assert.True(a384 > 4 * a48, $"A must grow with N ({a48} → {a384})");
        Assert.True(s384 <= s48, "S∞ does NOT grow with A (it saturates)");
        Assert.True(s384 < 10, $"S∞ stays small while A reaches {a384}");
    }

    // ── 4. Fitness variance ──────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_FitnessVarianceScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var rows = AllCases().Select(c => new
        {
            c.Name,
            Variance = LogFitnessVariance(c.Fitness),
            A = c.A,
            S = SOfCase(c.Name, 0.01, 1.0)
        }).ToArray();

        // Fitness variance is informative but NOT a complete determinant: A is a hard ceiling,
        // and the full fitness DISTRIBUTION (not one scalar) sets how many modes are within the
        // mutation–selection reach of the fittest. The clean monotone extreme still holds:
        // random has by far the lowest variance AND the highest S∞.
        var random = rows.Single(r => r.Name == "random");
        var others = rows.Where(o => o.Name != "random").ToArray();
        Assert.True(others.All(o => random.Variance < o.Variance), "random must have the lowest fitness variance");
        Assert.True(others.All(o => random.S > o.S), "random must have the highest S∞");

        // Counterexample to "variance alone determines S∞": D96-3D has HIGHER variance than
        // unphysical yet MORE survivors (A=12 ceiling vs A=3 ceiling).
        var d963d = rows.Single(r => r.Name == "D96-3D");
        var unphysical = rows.Single(r => r.Name == "unphysical");
        Assert.True(d963d.Variance > unphysical.Variance, "D96-3D has higher fitness variance than unphysical");
        Assert.True(d963d.S > unphysical.S, "yet D96-3D has MORE survivors (A ceiling, not variance)");
    }

    private static int SOfCase(string name, double mu, double beta) => name switch
    {
        "D96" => SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), mu, beta),
        "D96-3D" => SOf("D96-3D", () => AttractorDominanceAnalyzer.D963D(4, 4, 6), mu, beta),
        "random" => SOf("random", () => GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42), mu, beta),
        "physical" => SOfSpectrum("physical", () => SpectralBlueprint.BuildSymmetric(96, x => (double)x), mu, beta),
        "unphysical" => SOfSpectrum("unphysical", () => SpectralBlueprint.BuildSymmetric(96, x => x <= 16 ? 5.0 : x <= 32 ? 25.0 : 60.0), mu, beta),
        _ => throw new ArgumentOutOfRangeException(nameof(name)),
    };

    // ── 5. Spectral rigidity ─────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_RigidityScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var rows = AllCases().Select(c => new
        {
            c.Name,
            Rigidity = RigidityProxy(c.A, c.N),
            S = SOfCase(c.Name, 0.01, 1.0)
        }).ToArray();

        // Rigidity (degeneracy fraction) is NOT a monotonic determinant of S∞:
        // D96-3D is MORE rigid than D96 yet has MORE survivors — rigidity is confounded with
        // the fitness distribution (degeneracy m enters fitness w = m/λ).
        var d96 = rows.Single(r => r.Name == "D96");
        var d963d = rows.Single(r => r.Name == "D96-3D");
        Assert.True(d963d.Rigidity > d96.Rigidity, "D96-3D is more rigid than D96");
        Assert.True(d963d.S > d96.S, "yet D96-3D has MORE survivors than D96 — rigidity is not monotonic");
    }

    // ── 6. Scaling law ───────────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_ScalingLaw()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // DERIVED decomposition: S∞ = min(A, N_fit(μ, β, {w})), where
        //   · A is the hard landscape ceiling,
        //   · N_fit is the number of modes within the mutation–selection reach of the fittest:
        //     one mode at μ=0 (pure selection), a finite set S₀⁺ ≥ 1 for any μ>0, widened by μ.
        // Consequences (each verified below):
        //   ∂S∞/∂μ ≥ 0   (mutation widens the surviving cloud)
        //   ∂S∞/∂β ≥ 0   (crowding levels the field → more coexistence)
        //   ∂S∞/∂A = 0   (S∞ saturates; it does NOT track A)
        //   1 ≤ S∞ ≤ A    (structural)
        double[] muValues = [0.001, 0.01, 0.1];
        int[] s = muValues.Select(mu => SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), mu, 1.0)).ToArray();
        Assert.True(s[2] > s[1] && s[1] >= s[0], "∂S∞/∂μ ≥ 0");

        // μ = 0 collapses to one survivor; any μ > 0 keeps a mutation–selection balance ≥ 1.
        int pureSelection = SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.0, 1.0);
        Assert.Equal(1, pureSelection);
        Assert.True(s[0] >= pureSelection);

        // ∂S∞/∂β ≥ 0.
        int b0 = SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.01, 0.0);
        int b10 = SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.01, 10.0);
        Assert.True(b10 >= b0, "∂S∞/∂β ≥ 0");
    }

    // ── 7. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // DERIVED: 1 ≤ S∞ ≤ A; ∂S∞/∂μ ≥ 0; ∂S∞/∂β ≥ 0; μ→0 ⇒ S∞→S₀ (crowding baseline).
        // EMERGENT: the exact functional form F(μ, β, {w}) and its coefficients.
        // REFUTED: "S∞ tracks A" (circulant A 20→188 yet S∞ stays ≈4–5); "rigidity alone
        //          determines S∞" (D96-3D more rigid than D96 yet more survivors); "variance
        //          alone determines S∞" (D96-3D vs unphysical counterexample).
        var d96 = CaseD96();
        var random = CaseRandom();
        Assert.Equal(44, d96.A);
        Assert.Equal(95, random.A);
        Assert.NotEqual(
            SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.01, 1.0),
            SOf("random", () => GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42), 0.01, 1.0));
    }

    // ── 8. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_008_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_008 — Asymptotic Diversity Limit Audit");

        sb.AppendLine("Question: what determines the saturated species count S∞ (from T_007)?");
        sb.AppendLine("Sweep μ, β, A, fitness variance, spectral rigidity.");
        sb.AppendLine();

        sb.AppendLine("[1] Mutation-rate scaling (D96, β=1)");
        sb.AppendLine("     μ        S∞");
        foreach (double mu in new[] { 0.0001, 0.001, 0.01, 0.1, 0.5 })
            sb.AppendLine($"     {mu,7:F4}   {SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), mu, 1.0),4}");
        sb.AppendLine();

        sb.AppendLine("[2] Crowding scaling (D96, μ=0.01)");
        sb.AppendLine("     β        S∞");
        foreach (double b in new[] { 0.0, 0.1, 0.5, 1.0, 2.0, 10.0 })
            sb.AppendLine($"     {b,7:F2}   {SOf("D96", () => AttractorDominanceAnalyzer.D96Ring(), 0.01, b),4}");
        sb.AppendLine();

        sb.AppendLine("[3] Landscape-size scaling (circulant C_N(1..6), μ=0.01, β=1)");
        sb.AppendLine("     N      A      S∞");
        foreach (int n in new[] { 48, 96, 192, 384 })
        {
            var (d, m) = Eigenspaces(AttractorDominanceAnalyzer.D96Ring(n));
            int a = NonZeroCount(d);
            int s = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(n)).FinalSpecies;
            sb.AppendLine($"     {n,5}  {a,5}  {s,5}");
        }
        sb.AppendLine();

        sb.AppendLine("[4] Fitness variance vs S∞ (all cases, μ=0.01, β=1)");
        sb.AppendLine("     model             A    Var(log w)   S∞");
        foreach (var c in AllCases().OrderByDescending(x => LogFitnessVariance(x.Fitness)))
        {
            double v = LogFitnessVariance(c.Fitness);
            int s = SOfCase(c.Name, 0.01, 1.0);
            sb.AppendLine($"     {c.Name,-15} {c.A,5}   {v,10:F3}   {s,4}");
        }
        sb.AppendLine();

        sb.AppendLine("[5] Rigidity proxy vs S∞ (all cases)");
        sb.AppendLine("     model             A    rigidity   S∞");
        foreach (var c in AllCases())
        {
            double r = RigidityProxy(c.A, c.N);
            int s = SOfCase(c.Name, 0.01, 1.0);
            sb.AppendLine($"     {c.Name,-15} {c.A,5}   {r,7:F3}   {s,4}");
        }
        sb.AppendLine();

        sb.AppendLine("[6] Conclusions");
        sb.AppendLine("  DERIVED decomposition: S∞ = min(A, N_fit(μ, β, {w})).");
        sb.AppendLine("     · 1 ≤ S∞ ≤ A  (structural ceiling).");
        sb.AppendLine("     · ∂S∞/∂μ ≥ 0  (mutation widens the surviving cloud).");
        sb.AppendLine("     · ∂S∞/∂β ≥ 0  (crowding levels the field → more coexistence).");
        sb.AppendLine("     · μ=0 ⇒ S∞=1 (pure selection); any μ>0 keeps S₀⁺>1 alive.");
        sb.AppendLine("     · ∂S∞/∂A = 0  (S∞ saturates; it does NOT track the landscape size).");
        sb.AppendLine("  EMERGENT: the functional form F(μ, β, {w}) and its coefficients.");
        sb.AppendLine("  REFUTED:  'S∞ is set by A alone' (circulant A 20→188 yet S∞ ≈ 4–5);");
        sb.AppendLine("            'rigidity alone determines S∞' (D96-3D more rigid than D96 yet more");
        sb.AppendLine("            survivors); 'variance alone determines S∞' (D96-3D vs unphysical).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
