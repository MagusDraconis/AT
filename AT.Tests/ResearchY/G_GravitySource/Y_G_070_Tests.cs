using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ObservationalDecisionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_070 - Observational Decision Audit (group G - Gravity Source).
///
/// QUESTION. What exact neutron-star measurements would decide AT vs GR first? Output 1, 3 and 5 sigma, the target stars
/// and the required precision. Goal: a concrete observing program capable of excluding either AT or GR.
///
/// ANSWER: **DERIVED - the program is ONE TARGET AND ONE MEASUREMENT: measure the surface redshift of J0740+6620 to
/// 7.55 per cent and the question is decided at 6.4 sigma.**
/// </summary>
public class Y_G_070_Tests : ResearchTestBase
{
    public Y_G_070_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_070_TheCatalogueReproducesTheEarlierAudits()
    {
        var catalogue = Catalogue();
        Assert.Equal(4, catalogue.Length);
        Assert.True(TheCompactnessesReproduceTheEarlierAudits());

        // the compactness is recomputed from M and R, and it lands on the earlier audits' recorded values
        Assert.InRange(catalogue[0].X, -0.2471, -0.2469);
        Assert.InRange(catalogue[1].X, -0.2244, -0.2241);
        Assert.InRange(catalogue[2].X, -0.1521, -0.1519);

        // and the redshifts of the most compact target
        Assert.Equal(0.280181, catalogue[0].ZAt, 6);
        Assert.Equal(0.405807, catalogue[0].ZGr, 6);
        Assert.InRange(catalogue[0].Separation, 0.1255, 0.1257);

        // the compactness uncertainty is the published constraint, and the sample spans 2.6 to 3.7 per cent
        Assert.Equal(0.02636, catalogue[2].RelativeSigmaX, 5);
        Assert.Equal(0.03661, catalogue[0].RelativeSigmaX, 5);
    }

    [Fact]
    public void Y_G_070_WhatEachTargetCanDecide()
    {
        var capability = TargetCapability();
        Assert.Equal(4, capability.Length);

        // the most compact target at a 20 per cent timing, 5 per cent and perfect
        Assert.InRange(capability[0].NowTwentyPercent, 2.1, 2.3);
        Assert.InRange(capability[0].ProjectedFivePercent, 6.3, 6.6);
        Assert.InRange(capability[0].IdealNoTimingError, 9.2, 9.4);

        // and the best-constrained target, which needs no compactness improvement at all
        Assert.InRange(capability[2].NowTwentyPercent, 1.0, 1.1);
        Assert.InRange(capability[2].ProjectedFivePercent, 4.0, 4.1);
        Assert.InRange(capability[2].IdealNoTimingError, 15.3, 15.5);

        Assert.StartsWith("J0740+6620 (Riley", BestTarget());
    }

    [Fact]
    public void Y_G_070_TheBestTargetDependsOnWhichErrorDominates()
    {
        // A DRAFT CLAIM IS WITHDRAWN: the most compact target does NOT win on both counts
        Assert.False(TheBestTargetIsTheMostCompactAndBestConstrained());
        Assert.True(TheMostCompactTargetWinsAtRealisticTiming());
        Assert.True(TheBetterConstrainedTargetWinsWithPerfectTiming());

        // and the two swap at a computable timing precision
        double flip = TimingAtWhichTheBestTargetFlips();
        Assert.InRange(flip, 0.0, 0.05);
        Assert.True(Significance(Catalogue()[0].X, Catalogue()[0].RelativeSigmaX, flip)
                 == Significance(Catalogue()[2].X, Catalogue()[2].RelativeSigmaX, flip));
    }

    [Fact]
    public void Y_G_070_TheRequiredPrecisionInBothDirections()
    {
        var best = RequirementTable("J0740+6620 (Riley 2021)");
        Assert.Equal(3, best.Length);
        Assert.InRange(best[0].RequiredRelativeSigmaZ, 0.445, 0.447);
        Assert.InRange(best[1].RequiredRelativeSigmaZ, 0.141, 0.142);
        Assert.InRange(best[2].RequiredRelativeSigmaZ, 0.0754, 0.0756);

        // the other end of the same curve: the compactness a 5 per cent timing would need
        Assert.InRange(best[0].RequiredRelativeSigmaX, 0.337, 0.338);
        Assert.InRange(best[1].RequiredRelativeSigmaX, 0.1065, 0.1067);
        Assert.InRange(best[2].RequiredRelativeSigmaX, 0.0563, 0.0565);

        // and at the best-constrained target the 5 sigma budget is already breached at a 5 per cent timing
        var second = RequirementTable("J0030+0451 (Riley 2019)");
        Assert.InRange(second[2].RequiredRelativeSigmaZ, 0.0397, 0.0399);
        Assert.True(double.IsNaN(second[2].RequiredRelativeSigmaX));
    }

    [Fact]
    public void Y_G_070_TheBudgetSharesShowWhichAxisToAttack()
    {
        // the honest form of "which binds": how much of the budget each axis already spends
        var bestFive = BudgetShares("J0740+6620 (Riley 2021)", 5.0, 0.05);
        Assert.InRange(bestFive.TimingShare, 0.30, 0.32);
        Assert.InRange(bestFive.CompactnessShare, 0.28, 0.30);
        Assert.True(bestFive.TimingShare > bestFive.CompactnessShare);
        Assert.True(bestFive.TimingShare < 1.0 && bestFive.CompactnessShare < 1.0);

        // at the best-constrained target the compactness share is negligible and the timing share breaches
        var secondFive = BudgetShares("J0030+0451 (Riley 2019)", 5.0, 0.05);
        Assert.True(secondFive.TimingShare > 1.0);
        Assert.InRange(secondFive.CompactnessShare, 0.09, 0.12);
        var secondThree = BudgetShares("J0030+0451 (Riley 2019)", 3.0, 0.05);
        Assert.InRange(secondThree.TimingShare, 0.50, 0.52);
        Assert.InRange(secondThree.CompactnessShare, 0.03, 0.04);
    }

    [Fact]
    public void Y_G_070_TheProgrammeIsOneTargetAndOneMeasurement()
    {
        var program = TheProgram();
        Assert.Equal(3, program.Length);
        Assert.Contains("J0740+6620", program[0].Target);
        Assert.Contains("7,55 %", program[0].Precision.Replace('.', ','));

        var line = TheProgrammeInOneLine();
        Assert.Contains("J0740+6620", line);
        Assert.Contains("7,55 %".Replace('.', ','), line.Replace('.', ','));

        // the slack on the current determinations
        var slack = ProgrammeSlack();
        Assert.InRange(slack[2].Needed, 0.0754, 0.0756);
        Assert.InRange(slack[2].ImprovementOverTwenty, 2.5, 2.7);
        Assert.InRange(slack[2].ImprovementOverFifty, 6.5, 6.7);

        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_G_070_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_070 - Observational Decision Audit: the program that decides AT vs GR first");

        sb.AppendLine("QUESTION. What EXACT neutron-star measurements would decide AT against GR first?");
        sb.AppendLine("INPUTS   G_068 (the one-line prediction), G_069 (the compactness thresholds and the three");
        sb.AppendLine("         uncertainty models); the current NICER limits and published mass-radius ranges");
        sb.AppendLine("OUTPUT   the 1, 3 and 5 sigma precision, the target stars, and the requirement in both directions");
        sb.AppendLine("GOAL     a concrete observing program capable of excluding either AT or GR");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The catalogued targets are the published NICER sample, and each compactness is RECOMPUTED from its");
        sb.AppendLine("     mass and radius (x = -1.4770 km per solar mass over the radius) rather than imported - the");
        sb.AppendLine("     recomputation is checked against the compactness the earlier audits recorded.");
        sb.AppendLine("  2. sigma_x/x is the published CONSTRAINT ON THE COMPACTNESS rather than a quadrature sum of the mass and");
        sb.AppendLine("     radius marginals, because the joint posterior constrains the ratio more tightly than the marginals.");
        sb.AppendLine("  3. The decision statistic is the shared-compactness difference of the two theories' redshifts, which is");
        sb.AppendLine("     the tightest legitimate model (G_069); the timing assumption is a parameter, not a fixed choice.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputCatalogue());
        PrintHeader(OutputCapability());
        PrintHeader(OutputRequirements());
        PrintHeader(OutputProgram());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
