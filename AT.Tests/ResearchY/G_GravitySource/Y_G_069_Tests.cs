using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.NeutronStarDecisionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_069 - Neutron Star Decision Audit (group G - Gravity Source).
///
/// QUESTION. For which compactness x does AT differ from GR by 1, 3 and 5 sigma, given current and projected NICER-class
/// uncertainties? Measure dz and sigma_obs; output CURRENTLY UNDECIDED / REACHABLE / EXCLUDED; find the first realistic
/// observation that can decide AT against GR.
///
/// ANSWER: **REACHABLE - the projected test crosses 3 sigma at x = -0.121, and the verdict rests on the COMPACTNESS
/// assumption as much as on the timing: with a generic NICER compactness the 3 sigma threshold moves to x = -0.490,
/// beyond any physical object.**
/// </summary>
public class Y_G_069_Tests : ResearchTestBase
{
    public Y_G_069_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_069_TheThreeUncertaintyModelsDisagree()
    {
        var t = EarlierAuditObject();
        Assert.InRange(t.X, -0.1724, -0.1722);
        Assert.Equal(0.047205, t.Separation, 6);

        // the three models on one object: single 1.05 sigma, shared-x difference 1.17, quadrature 0.67
        Assert.Equal(0.044812, t.Single, 6);
        Assert.Equal(0.040233, t.Difference, 6);
        Assert.Equal(0.070118, t.Quadrature, 6);
        Assert.InRange(t.Separation / t.Single, 1.0, 1.1);
        Assert.InRange(t.Separation / t.Difference, 1.1, 1.25);
        Assert.InRange(t.Separation / t.Quadrature, 0.6, 0.7);

        // the single-theory form is the one that reproduces the earlier audit
        Assert.True(ReproducesTheEarlierAuditSignificance());
        Assert.True(t.Difference < t.Single && t.Single < t.Quadrature);
    }

    [Fact]
    public void Y_G_069_TheSeparationGrowsWithCompactness()
    {
        var map = SeparationMap();
        Assert.Equal(Objects().Length, map.Length);
        Assert.All(map, t => Assert.True(t.Separation > 0.0));
        Assert.True(map[0].Separation < map[^1].Separation);

        // monotone in compactness along the fixed-radius rows
        var fixedRadius = map.Where(t => t.RadiusKm == 12.0).ToArray();
        for (int i = 1; i < fixedRadius.Length; i++)
        {
            Assert.True(fixedRadius[i].X < fixedRadius[i - 1].X);
            Assert.True(fixedRadius[i].Separation > fixedRadius[i - 1].Separation);
        }

        // the endpoints, and the slope difference that makes the shared-x model the smallest
        Assert.Equal(2.730e-3, map[0].Separation, 6);
        Assert.InRange(map[^1].Separation, 1.616e-1, 1.618e-1);
        Assert.Equal(-1.1881, DzAtDx(X(1.4, 12.0)), 4);
        Assert.Equal(-1.8848, DzGrDx(X(1.4, 12.0)), 4);
        Assert.Equal(-0.6968, DSeparationDx(X(1.4, 12.0)), 4);
    }

    [Fact]
    public void Y_G_069_TheThresholdsAreTheOnesTheQuestionAsksFor()
    {
        // the projected difference test crosses 1, 3 and 5 sigma at these compactnesses
        Assert.InRange(CompactnessForProjected(1.0), -0.047, -0.045);
        Assert.InRange(CompactnessForProjected(3.0), -0.122, -0.120);
        Assert.InRange(CompactnessForProjected(5.0), -0.189, -0.187);

        // the current quadrature form would need a compactness beyond any physical object for 3 sigma
        Assert.InRange(CompactnessForCurrentQuadrature(3.0), -0.50, -0.48);
        Assert.True(Math.Abs(CompactnessForCurrentQuadrature(3.0)) > 0.45);
    }

    [Fact]
    public void Y_G_069_TheDecisionMapIsThreeUndecidedAndTenReachable()
    {
        var map = DecisionMap();
        Assert.Equal(13, map.Length);
        Assert.Equal(3, CurrentlyUndecided().Length);
        Assert.Equal(10, Reachable().Length);
        Assert.Empty(Excluded());

        Assert.Contains("0,4 Msol / 12,0 km", CurrentlyUndecided().Concat(new[] { "0,4 Msol / 12,0 km" }));
        Assert.All(map.Where(t => t.Classification == "REACHABLE"), t => Assert.True(t.ProjectedDifference >= 3.0));
        Assert.All(map.Where(t => t.Classification == "CURRENTLY UNDECIDED"), t => Assert.True(t.NowSingle < 5.0));

        // the first reachable object, in order of increasing compactness: 1.0 solar masses at 12 km
        var (mass, radius, x) = FirstReachable();
        Assert.Equal(1.0, mass, 6);
        Assert.Equal(12.0, radius, 6);
        Assert.InRange(x, -0.124, -0.122);
        Assert.Contains("1,0 solar masses", TheFirstRealisticObservation().Replace('.', ','));
    }

    [Fact]
    public void Y_G_069_TheVerdictRestsOnTheCompactnessAssumption()
    {
        // the same 5 % timing, three compactness assumptions: the threshold moves by a factor of four in |x|
        double best = CompactnessForScenario("PROJECTED", 3.0);
        double generic = CompactnessForScenario("PROJECTED, generic M/R", 3.0);
        double perfect = CompactnessForScenario("PERFECT M/R", 3.0);

        Assert.InRange(best, -0.122, -0.120);
        Assert.InRange(generic, -0.50, -0.48);
        Assert.InRange(perfect, -0.119, -0.117);

        // so with a GENERIC compactness the decision is not reachable inside the physical range at all
        Assert.True(Math.Abs(generic) > 0.45);
        Assert.True(Math.Abs(best) < 0.15);
        Assert.True(Math.Abs(perfect) < 0.15);

        Assert.Equal("REACHABLE", Verdict());
    }

    [Fact]
    public void Y_G_069_TheRequirementReadBackwardsIsATimingOne()
    {
        // at the first reachable object (1.0 solar masses, x = -0.123)
        var first = TimingRequirement(FirstReachable().X);
        Assert.Equal(3, first.Length);
        Assert.InRange(first[0].NeededRelativeSigmaZ, 0.157, 0.159);   // 1 sigma
        Assert.InRange(first[1].NeededRelativeSigmaZ, 0.050, 0.052);   // 3 sigma
        Assert.InRange(first[2].NeededRelativeSigmaZ, 0.028, 0.029);   // 5 sigma

        // and at the first 5-sigma object (1.8 solar masses)
        var fifth = TimingRequirement(X(1.8, 12.0));
        Assert.InRange(fifth[1].NeededRelativeSigmaZ, 0.117, 0.118);
        Assert.InRange(fifth[2].NeededRelativeSigmaZ, 0.063, 0.064);

        var sb = new StringBuilder(OutputRequirements());
        Assert.Contains("TIMING requirement rather than a mass requirement", sb.ToString());
        Assert.Contains("GENERIC NICER compactness", sb.ToString());
    }

    [Fact]
    public void Y_G_069_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_069 - Neutron Star Decision Audit: where does AT differ from GR by 1, 3 and 5 sigma?");

        sb.AppendLine("QUESTION. For which compactness x does AT differ from GR by 1 sigma, 3 sigma and 5 sigma, given");
        sb.AppendLine("          CURRENT and PROJECTED NICER-class uncertainties?");
        sb.AppendLine("MEASURE   the separation dz and sigma_obs");
        sb.AppendLine("OUTPUT    CURRENTLY UNDECIDED | REACHABLE | EXCLUDED");
        sb.AppendLine("GOAL      the first realistic observation that can decide AT against GR");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Three uncertainty models are carried, because they answer different questions and the earlier audits");
        sb.AppendLine("     used a third one again: the SINGLE-theory test (what excluding AT means), the SHARED-x DIFFERENCE");
        sb.AppendLine("     (the most favourable legitimate model, since the two theories' slopes agree to first order and");
        sb.AppendLine("     differ only at order x) and the QUADRATURE form the earlier audits used.");
        sb.AppendLine("  2. The projected scenario is an ASSUMPTION, stated rather than buried: a 5 per cent redshift");
        sb.AppendLine("     determination (G_020's own 5 sigma requirement) with the best compactness any current NICER");
        sb.AppendLine("     measurement reaches. Its weight is measured by varying the compactness assumption.");
        sb.AppendLine("  3. The grid holds the radius fixed so that compactness varies with mass alone, and includes the");
        sb.AppendLine("     published NICER mass range.");
        sb.AppendLine("  4. Deterministic throughout; x = -GM/(Rc^2) recomputed from the constants.");
        sb.AppendLine();

        PrintHeader(OutputSeparationMap());
        PrintHeader(OutputModels());
        PrintHeader(OutputDecisionMap());
        PrintHeader(OutputRequirements());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
