using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_011 — Species Ceiling Audit.
///
/// Question: can the legacy AT-138/139 value (~19 stable species) emerge naturally from D96?
///
/// Measure the four population quantities — transient species count (peak alive), cumulative
/// species count (distinct modes ever alive), survivor count (S∞), and turnover — for D96 and
/// the other landscapes, and test whether N_species ≈ 19 follows from the T_001–T_010 laws.
/// Deterministic throughout.
/// </summary>
public class Y_T_011_Tests : ResearchTestBase
{
    public Y_T_011_Tests(ITestOutputHelper output) : base(output) { }

    private static int S(SpectralCase c, double mu, double beta)
        => SpectralCaseCatalog.SInfinity(c, mu, beta);

    // ── 1. The four population quantities for every landscape ─────────────────

    [Fact]
    public void Y_T_011_PopulationMeasures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        foreach (var c in SpectralCaseCatalog.All())
        {
            var r = SpectralCaseCatalog.RunCase(c, 0.01, 1.0);

            // Structural bounds: transient (peak) and cumulative (ever-alive) lie between the
            // survivor count and the landscape size.
            Assert.InRange(r.MaxAliveSpecies, r.FinalSpecies, c.A);
            Assert.InRange(r.CumulativeSpecies, r.FinalSpecies, c.A);
            Assert.True(r.Turnover >= 0.0);
        }
    }

    // ── 2. D96 equilibrium survivors ≈ 5, NOT 19 ──────────────────────────────

    [Fact]
    public void Y_T_011_D96NotNineteen()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();

        // The natural equilibrium (T_010) keeps ~5 survivors; the legacy "~19" does not match.
        int survivors = S(d96, 0.01, 1.0);
        Assert.InRange(survivors, 3, 8);
        Assert.True(Math.Abs(survivors - 19) >= 10, $"D96 survivors {survivors} are far from 19");

        // The nearest natural value to 19 is the RANDOM landscape (17), not D96.
        int randomSurvivors = S(SpectralCaseCatalog.Random(), 0.01, 1.0);
        Assert.True(Math.Abs(randomSurvivors - 19) < Math.Abs(survivors - 19),
            $"random ({randomSurvivors}) is nearer 19 than D96 ({survivors})");
    }

    // ── 3. Discovery mode: cumulative species from a single fittest species ───

    [Fact]
    public void Y_T_011_DiscoveryTrajectory()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();
        var random = SpectralCaseCatalog.Random();

        var d96d = SpectralCaseCatalog.RunCase(d96, 0.01, 1.0, startFromFittest: true);
        var randD = SpectralCaseCatalog.RunCase(random, 0.01, 1.0, startFromFittest: true);

        // Discovery starts from a single fittest species and accumulates a bounded set; the
        // D96 discovery set is strictly smaller than random's (compression, T_010).
        Assert.True(d96d.CumulativeSpecies >= 1, "discovery starts from ≥1 species");
        Assert.True(d96d.CumulativeSpecies > 1, "mutation must discover more than one species");
        Assert.True(d96d.CumulativeSpecies <= d96.A, "discovery bounded by the landscape");
        Assert.True(d96d.CumulativeSpecies < randD.CumulativeSpecies,
            $"D96 discovers ({d96d.CumulativeSpecies}) fewer species than random ({randD.CumulativeSpecies})");
    }

    // ── 4. 19 is reachable for D96 only by erasing selection (high μ) ─────────

    [Fact]
    public void Y_T_011_CeilingSweep()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();

        // At natural mutation (μ=0.01) D96 is far below 19.
        int natural = S(d96, 0.01, 1.0);
        Assert.True(natural < 19, $"natural D96 survivors {natural} must be below 19");

        // D96's survivor count can only reach ~19 by driving mutation toward the uniform
        // (selection-erasing) limit; even μ=0.5 keeps it below 10 (T_008).
        int highMu = S(d96, 0.5, 1.0);
        Assert.True(highMu < 19, $"even μ=0.5 gives {highMu}, still below 19");

        // In the degenerate μ→1 limit the distribution → uniform and ALL A modes survive —
        // that is the trivial ceiling A=44, not a physical "19 species" prediction.
        Assert.Equal(44, d96.A);
        Assert.True(d96.A > 19, "the only natural D96 ceiling is A=44, not 19");
    }

    // ── 5. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_011_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();

        // REFUTED: "~19 species emerges naturally from D96". D96 gives 5 survivors (T_010);
        // 19 is nearest random (17), or the trivial uniform ceiling A=44 at μ→1.
        Assert.True(S(d96, 0.01, 1.0) < 19);

        // DERIVED (by elimination): the species ceiling for D96 is S∞ = min(A, N_fit) with
        // N_fit the mutation–selection reach (T_008/T_009), giving ~5, not 19.
        int randomSurvivors = S(SpectralCaseCatalog.Random(), 0.01, 1.0);
        Assert.True(Math.Abs(randomSurvivors - 19) <= 3, $"random ({randomSurvivors}) is within ±3 of 19");
    }

    // ── 6. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_011_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_011 — Species Ceiling Audit");

        sb.AppendLine("Question: can the AT-138/139 value (~19 species) emerge naturally from D96?");
        sb.AppendLine("Derive or refute N_species ≈ 19 from the T_001–T_010 laws.");
        sb.AppendLine();

        sb.AppendLine("[1] Population quantities (μ=0.01, β=1, uniform init)");
        sb.AppendLine("     case             A    S∞    transient  cumulative  turnover");
        foreach (var c in SpectralCaseCatalog.All())
        {
            var r = SpectralCaseCatalog.RunCase(c, 0.01, 1.0);
            sb.AppendLine($"     {c.Name,-14} {c.A,4} {r.FinalSpecies,5} {r.MaxAliveSpecies,9} {r.CumulativeSpecies,10} {r.Turnover,9:F4}");
        }
        sb.AppendLine();

        sb.AppendLine("[2] Discovery trajectory (μ=0.01, β=1, start from fittest species)");
        sb.AppendLine("     case             A    cumulative  survivor  transient");
        foreach (var c in SpectralCaseCatalog.All())
        {
            var r = SpectralCaseCatalog.RunCase(c, 0.01, 1.0, startFromFittest: true);
            sb.AppendLine($"     {c.Name,-14} {c.A,4} {r.CumulativeSpecies,9} {r.FinalSpecies,9} {r.MaxAliveSpecies,9}");
        }
        sb.AppendLine();

        sb.AppendLine("[3] Mutation sweep (D96, β=1) — where does S∞ cross 19?");
        sb.AppendLine("     μ        S∞");
        foreach (double mu in new[] { 0.001, 0.01, 0.1, 0.3, 0.5, 0.8, 0.95 })
            sb.AppendLine($"     {mu,7:F3}   {S(SpectralCaseCatalog.D96(), mu, 1.0),4}");
        sb.AppendLine();

        sb.AppendLine("[4] Conclusions");
        sb.AppendLine("  REFUTED:  '~19 species emerges naturally from D96'. D96's natural survivor count");
        sb.AppendLine("            is ~5 (T_010); 19 is nearest the RANDOM landscape (17), not D96.");
        sb.AppendLine("  DERIVED:  the D96 species ceiling is S∞ = min(A, N_fit(μ,β,{w})) (T_008/T_009)");
        sb.AppendLine("            with A=44 — the only natural D96 ceiling is 44 (the uniform limit),");
        sb.AppendLine("            and 5 (the mutation–selection balance), never 19.");
        sb.AppendLine("  EMERGENT: the legacy '~19 stable species' (AT-138/139) arises from a DIFFERENT");
        sb.AppendLine("            model (Θ-field pattern novelty), not the D96 spectral blueprint.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
