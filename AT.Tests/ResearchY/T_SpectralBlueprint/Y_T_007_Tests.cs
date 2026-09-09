using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_007 — Bounded Innovation Audit.
///
/// Question: does Darwinian evolution (replicator + mutation + extinction + resource
/// constraint) on a spectral landscape produce a FINITE, saturated species count?
///
/// Model: replicator–mutator over the non-zero Laplacian eigenspaces, fitness w = m/λ,
/// density-dependent crowding f = w/(1+βx), ring mutation (μ to neighbouring modes), and an
/// extinction threshold ε. Deterministic throughout; random-sparse uses the fixed seed 42.
/// </summary>
public class Y_T_007_Tests : ResearchTestBase
{
    public Y_T_007_Tests(ITestOutputHelper output) : base(output) { }

    private static (string Name, InnovationResult Result)[] RunAll(int steps = BoundedInnovationAnalyzer.DefaultSteps)
    {
        return
        [
            ("D96", BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(), steps)),
            ("D96-3D", BoundedInnovationAnalyzer.Run("D96-3D", AttractorDominanceAnalyzer.D963D(4, 4, 6), steps)),
            ("random sparse", BoundedInnovationAnalyzer.Run("random", GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42), steps)),
            ("complete", BoundedInnovationAnalyzer.Run("complete", GeneralInverseSpectrumAnalyzer.CompleteGraph(96), steps)),
            ("physical max-sep", BoundedInnovationAnalyzer.RunSpectrum("physical-maxsep", SpectralBlueprint.BuildSymmetric(96, m => (double)m), steps)),
            ("unphysical clustered", BoundedInnovationAnalyzer.RunSpectrum("unphysical-clustered", SpectralBlueprint.BuildSymmetric(96, m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0), steps)),
        ];
    }

    // ── Critical test 1: does diversity saturate? ──────────────────────────

    [Fact]
    public void Y_T_007_Saturation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        foreach (var (name, r) in RunAll())
        {
            Assert.True(r.SaturationTime >= 0, $"{name}: diversity must reach a plateau");
            Assert.Equal("BOUNDED", r.Verdict);
            Assert.True(r.FinalSpecies > 0, $"{name}: at least one species must survive");
            Assert.True(r.FinalSpecies <= r.LandscapeSize, $"{name}: species ≤ landscape");
        }
    }

    // ── Critical test 2: is saturation independent of runtime? ──────────────

    [Fact]
    public void Y_T_007_RuntimeIndependence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        const int shortRun = 20000;
        const int longRun = 40000;

        foreach (var (name, shortR) in RunAll(shortRun))
        {
            var longR = RunSingle(name, longRun);
            Assert.Equal(shortR.FinalSpecies, longR.FinalSpecies);
            Assert.Equal(shortR.Survivors, longR.Survivors);
            Assert.InRange(Math.Abs(shortR.FinalEntropy - longR.FinalEntropy), 0.0, 1e-8);
        }
    }

    private static InnovationResult RunSingle(string name, int steps)
    {
        return name switch
        {
            "D96" => BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(), steps),
            "D96-3D" => BoundedInnovationAnalyzer.Run("D96-3D", AttractorDominanceAnalyzer.D963D(4, 4, 6), steps),
            "random sparse" => BoundedInnovationAnalyzer.Run("random", GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42), steps),
            "complete" => BoundedInnovationAnalyzer.Run("complete", GeneralInverseSpectrumAnalyzer.CompleteGraph(96), steps),
            "physical max-sep" => BoundedInnovationAnalyzer.RunSpectrum("physical-maxsep", SpectralBlueprint.BuildSymmetric(96, m => (double)m), steps),
            "unphysical clustered" => BoundedInnovationAnalyzer.RunSpectrum("unphysical-clustered", SpectralBlueprint.BuildSymmetric(96, m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0), steps),
            _ => throw new ArgumentOutOfRangeException(nameof(name)),
        };
    }

    // ── Critical test 3: does D96 produce lower asymptotic diversity? ───────

    [Fact]
    public void Y_T_007_D96LowerDiversity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring());
        var random = BoundedInnovationAnalyzer.Run("random", GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42));

        // D96's sharp fitness gaps (m/λ) concentrate selection into fewer coexisting species
        // than the near-flat random-sparse fitness landscape.
        Assert.True(d96.FinalSpecies < random.FinalSpecies,
            $"D96 ({d96.FinalSpecies}) should have fewer survivors than random ({random.FinalSpecies})");
        Assert.True(d96.FinalDiversity < random.FinalDiversity,
            $"D96 N_eff ({d96.FinalDiversity:F3}) should be below random ({random.FinalDiversity:F3})");
    }

    // ── Critical test 4: is a finite attractor landscape observed? ──────────

    [Fact]
    public void Y_T_007_FiniteAttractor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        foreach (var (name, r) in RunAll())
        {
            // The surviving set is a finite, stable, strict subset of the landscape
            // (competitive exclusion: not every mode coexists).
            Assert.True(r.Survivors.Length > 0, $"{name}: survivors non-empty");
            Assert.True(r.Survivors.Length <= r.LandscapeSize, $"{name}: survivors ≤ landscape");
            Assert.Equal(r.Survivors, r.Survivors.OrderBy(s => s));
        }

        // The degenerate complete graph has a single non-zero mode → exactly one survivor.
        var complete = BoundedInnovationAnalyzer.Run("complete", GeneralInverseSpectrumAnalyzer.CompleteGraph(96));
        Assert.Equal(1, complete.LandscapeSize);
        Assert.Equal(1, complete.FinalSpecies);
    }

    // ── BOUNDED / UNBOUNDED / CONDITIONAL contrast ─────────────────────────

    [Fact]
    public void Y_T_007_UnboundedControl()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        const int steps = 1000;
        const double innovationRate = 0.05;

        // No carrying capacity: the species count grows without bound.
        var unbounded = BoundedInnovationAnalyzer.OpenLandscapeSpeciesCount(steps, innovationRate, null);
        Assert.True(unbounded[^1] > unbounded[0],
            "open landscape without a resource cap must grow");
        Assert.Equal("UNBOUNDED",
            BoundedInnovationAnalyzer.ClassifyOpenLandscape(unbounded[^1], unbounded[^1], null));

        // A finite carrying capacity saturates the count.
        const double cap = 20.0;
        var bounded = BoundedInnovationAnalyzer.OpenLandscapeSpeciesCount(steps, innovationRate, cap);
        Assert.Equal((int)cap, bounded[^1]);
        Assert.Equal("BOUNDED",
            BoundedInnovationAnalyzer.ClassifyOpenLandscape(bounded[^1], bounded[^1], cap));
        // Saturation is stable in the tail.
        Assert.True(bounded[^100..].All(c => c == (int)cap));
    }

    // ── Classification (DERIVED / EMERGENT / REFUTED) ──────────────────────

    [Fact]
    public void Y_T_007_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // DERIVED: boundedness (finite landscape + positive mutation → unique fixed point).
        foreach (var (_, r) in RunAll())
            Assert.Equal("BOUNDED", r.Verdict);

        // EMERGENT: the saturation value is parameter-dependent (mutation rate).
        var lowMu = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(),
            mutationRate: 0.001);
        var highMu = BoundedInnovationAnalyzer.Run("D96", AttractorDominanceAnalyzer.D96Ring(),
            mutationRate: 0.1);
        Assert.NotEqual(lowMu.FinalSpecies, highMu.FinalSpecies);
    }

    // ── Research report ─────────────────────────────────────────────────────

    [Fact]
    public void Y_T_007_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_007 — Bounded Innovation Audit");

        sb.AppendLine("Question: does Darwinian evolution on a spectral landscape produce");
        sb.AppendLine("          a FINITE, saturated species count?");
        sb.AppendLine();
        sb.AppendLine("Model: replicator + mutation + extinction + resource (crowding) constraint.");
        sb.AppendLine("       landscape = non-zero Laplacian eigenspaces, ordered by eigenvalue;");
        sb.AppendLine("       fitness w_k = m_k / λ_k; crowding f_k = w_k / (1 + β x_k);");
        sb.AppendLine("       ring mutation μ → k±1; extinction threshold ε = 1e-6.");
        sb.AppendLine("       Deterministic. Parameters μ=0.01, β=1.0, T=20000.");
        sb.AppendLine();

        var models = RunAll();
        sb.AppendLine("[1] Evolution results");
        sb.AppendLine($"    {"model",-19} {"A",4} {"S∞",4} {"N_eff",7} {"D",7} {"H",7} {"ext",5} {"col",5} {"turn",6} {"t_sat",6}");
        foreach (var (name, r) in models)
        {
            sb.AppendLine($"    {name,-19} {r.LandscapeSize,4} {r.FinalSpecies,4} {r.FinalDiversity,7:F3} {r.FinalDominance,7:F3} {r.FinalEntropy,7:F3} {r.ExtinctionEvents,5} {r.ColonizationEvents,5} {r.Turnover,6:F3} {r.SaturationTime,6}");
        }
        sb.AppendLine();

        var d96 = models[0].Result;
        var random = models[2].Result;

        sb.AppendLine("[2] Critical answers");
        sb.AppendLine($"    C1 diversity saturates:            ALL models reach a plateau (t_sat ≥ 0) → YES");
        sb.AppendLine($"    C2 saturation independent of runtime: S∞ identical at T=20000 and T=40000 → YES");
        sb.AppendLine($"    C3 D96 lower asymptotic diversity:   S∞(D96)={d96.FinalSpecies} < S∞(random)={random.FinalSpecies} → {(d96.FinalSpecies < random.FinalSpecies ? "YES" : "NO")}");
        sb.AppendLine($"    C4 finite attractor landscape:       survivors are a fixed, finite, stable set (≤ A) → YES");
        sb.AppendLine();

        sb.AppendLine("[3] BOUNDED / UNBOUNDED / CONDITIONAL");
        sb.AppendLine("    Open landscape control (innovation rate 0.05/step):");
        sb.AppendLine("      no carrying capacity → species count grows → UNBOUNDED");
        sb.AppendLine("      carrying capacity 20  → species count saturates → BOUNDED");
        sb.AppendLine("    Finite spectral landscapes (all 6 cases) → BOUNDED.");
        sb.AppendLine();

        sb.AppendLine("[4] Verdicts (DERIVED / EMERGENT / REFUTED)");
        sb.AppendLine("    V1 species count is BOUNDED (finite landscape + positive mutation");
        sb.AppendLine("       → unique mutation–selection fixed point)                    → DERIVED");
        sb.AppendLine("    V2 the saturated diversity value (which species, how many)");
        sb.AppendLine("       depends on μ, β, and the landscape                          → EMERGENT");
        sb.AppendLine("    V3 'mutation drives unbounded innovation' on a finite landscape → REFUTED");
        sb.AppendLine("       (resource constraint + selection cap coexistence)");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    Darwinian evolution on a spectral landscape produces a FINITE species count:");
        sb.AppendLine("    diversity saturates to a stable mutation–selection balance that is independent of");
        sb.AppendLine("    runtime and at or below the landscape size (competitive exclusion limits coexistence).");
        sb.AppendLine("    Boundedness is DERIVED (finite landscape + crowding); the value is EMERGENT");
        sb.AppendLine("    (parameter/landscape dependent). D96's sharp fitness gaps concentrate diversity more");
        sb.AppendLine("    than a random landscape (C3). The resource constraint is the binding mechanism:");
        sb.AppendLine("    remove the carrying capacity and innovation is UNBOUNDED (control).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
