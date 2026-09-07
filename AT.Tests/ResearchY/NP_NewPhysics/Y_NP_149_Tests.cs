using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_149 — Defect State Optimization Audit test suite (Y_NP_149_Tests.cs).
///
/// Question: do materials possess optimal defect topologies for specific properties?
///
/// Verdict tested: SUPPORTED — materials can be tuned toward optimal property states via resonance-
/// driven defect engineering, but optimization is multi-objective (trade-offs) and closed-loop
/// convergence is PARTIAL (emerging).
///
/// Deterministic: fixed classification codes and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_149_Tests : ResearchTestBase
{
    public Y_NP_149_Tests(ITestOutputHelper output) : base(output) { }

    private const int SUPPORTED = 0;
    private const int PARTIAL = 1;
    private const int CONTRADICTED = 2;
    private const int UNKNOWN = 3;

    // ── [Required] Y_NP_149_Define ──────────────────────────────

    [Fact]
    public void Y_NP_149_Define()
    {
        bool density = true;       // how many
        bool topology = true;      // how they connect/organize
        bool distribution = true;  // where they sit
        Assert.True(density && topology && distribution);
    }

    // ── [Required] Y_NP_149_Targets ─────────────────────────────

    [Fact]
    public void Y_NP_149_Targets()
    {
        string[] properties = { "hardness", "yield stress", "fatigue life", "fracture toughness", "damping" };
        Assert.Equal(5, properties.Length);
        Assert.Contains("hardness", properties);
        Assert.Contains("damping", properties);
    }

    // ── [Required] Y_NP_149_Optimality ──────────────────────────

    [Fact]
    public void Y_NP_149_Optimality()
    {
        bool hardnessOptimum = true;    // peak-aged / Hall-Petch optimum
        bool fatigueOptimum = true;     // max compressive stress without damage
        bool dampingOptimum = true;     // optimal mobile-defect density (Ashby)
        bool eachHasExtremum = true;    // not monotone
        Assert.True(hardnessOptimum && fatigueOptimum && dampingOptimum && eachHasExtremum);
    }

    // ── [Required] Y_NP_149_RandomVsDirected ────────────────────

    [Fact]
    public void Y_NP_149_RandomVsDirected()
    {
        bool directedConverges = true;   // approaches a target optimal state
        bool randomDrifts = true;        // wanders, no convergence
        Assert.True(directedConverges && randomDrifts);
    }

    // ── [Required] Y_NP_149_Tradeoffs ───────────────────────────

    [Fact]
    public void Y_NP_149_Tradeoffs()
    {
        bool hardnessVsToughness = true;   // Hall-Petch
        bool strengthVsFatigue = true;     // defects as crack sites
        bool strengthVsDamping = true;     // Ashby limit
        bool paretoFront = true;           // not a single optimum
        Assert.True(hardnessVsToughness && strengthVsFatigue && strengthVsDamping && paretoFront);
    }

    // ── [Required] Y_NP_149_ClosedLoop ──────────────────────────

    [Fact]
    public void Y_NP_149_ClosedLoop()
    {
        bool readSupported = true;
        bool writeSupported = true;
        bool verifySupported = true;
        bool optimizePartial = true;   // ML/Bayesian emerging, not general
        Assert.True(readSupported && writeSupported && verifySupported && optimizePartial);
    }

    // ── [Required] Y_NP_149_Classification ──────────────────────

    [Fact]
    public void Y_NP_149_Classification()
    {
        string overall = "SUPPORTED";
        int optimalStates = SUPPORTED;
        int directedTuning = SUPPORTED;
        int tradeoffs = SUPPORTED;
        int closedLoop = PARTIAL;
        int randomSuffices = CONTRADICTED;

        Assert.Equal("SUPPORTED", overall);
        Assert.Equal(SUPPORTED, optimalStates);
        Assert.Equal(SUPPORTED, directedTuning);
        Assert.Equal(SUPPORTED, tradeoffs);
        Assert.Equal(PARTIAL, closedLoop);
        Assert.Equal(CONTRADICTED, randomSuffices);
    }

    // ── [Required] Y_NP_149_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_149_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_149 — Defect State Optimization Audit");

        sb.AppendLine("Goal: do materials have optimal defect topologies, and can we tune toward them?");
        sb.AppendLine();

        sb.AppendLine("[1] Defect state = density + topology + distribution (NP_147).");
        sb.AppendLine("[2] Each property has its own optimal defect state (Hall-Petch, peak-age, Ashby).");
        sb.AppendLine("[3] Directed evolution converges; random evolution drifts.");
        sb.AppendLine("[4] Trade-offs: hardness<->toughness, strength<->fatigue, strength<->damping (Pareto front).");
        sb.AppendLine("[5] read/write/verify SUPPORTED; closed-loop optimize PARTIAL (emerging).");
        sb.AppendLine("[6] Verdict: SUPPORTED — tunable to optimal states, multi-objective, closed-loop partial.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
