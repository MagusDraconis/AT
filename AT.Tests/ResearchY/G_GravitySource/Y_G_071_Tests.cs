using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_071 - Observational Roadmap Audit. What exact observations are required to decide AT redshift vs GR
/// redshift first? Measure the target object, the required timing precision, the required MASS precision, the
/// required RADIUS precision and the resulting significance; output CURRENT / 3SIGMA / 5SIGMA.
/// </summary>
public sealed class Y_G_071_Tests : ResearchTestBase
{
    public Y_G_071_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_G_071_TheDecompositionReproducesTheRecordedLiteral()
    {
        PrintHeader("G_071 - the decomposition, and what G_069's literal actually was");

        var (u, v) = ObservationalRoadmapAudit.GenericShares();
        double worst = ObservationalRoadmapAudit.RelativeSigmaX(u, v, -1.0);
        double quadrature = ObservationalRoadmapAudit.RelativeSigmaX(u, v, 0.0);
        double cancellation = ObservationalRoadmapAudit.RelativeSigmaX(u, v, 1.0);
        double rho = ObservationalRoadmapAudit.TheRecordedLiteralImpliesThisCorrelation();

        var sb = new StringBuilder();
        sb.AppendLine("THE IDENTITY: (sigma_x/x)^2 = u^2 + v^2 - 2 rho u v, with x = -k M/R.");
        sb.AppendLine($"  the only object with recorded marginals: M = {ObservationalRoadmapAudit.GenericMass} "
            + $"+/- {ObservationalRoadmapAudit.GenericMassSigma} solar masses, R = {ObservationalRoadmapAudit.GenericRadiusKm} "
            + $"+/- {ObservationalRoadmapAudit.GenericRadiusSigmaKm} km");
        sb.AppendLine($"  u = sigma_M/M = {u:P4}   v = sigma_R/R = {v:P4}   v/u = {v / u:F3}");
        sb.AppendLine();
        sb.AppendLine($"  worst case    (rho = -1)  u + v        = {worst:P4}");
        sb.AppendLine($"  independent   (rho =  0)  quadrature   = {quadrature:P4}");
        sb.AppendLine($"  cancellation  (rho = +1)  |u - v|      = {cancellation:P4}");
        sb.AppendLine();
        sb.AppendLine($"  G_069's scenario table carries the literal {ObservationalRoadmapAudit.RecordedGenericCompactness:P4}");
        sb.AppendLine($"    its own basis string calls it \"a generic NICER compactness\"");
        sb.AppendLine($"    the WORST CASE of these marginals is {worst:P4}");
        sb.AppendLine($"    agreement with the literal: {Math.Abs(worst - ObservationalRoadmapAudit.RecordedGenericCompactness):E2}");
        sb.AppendLine($"    the literal implies a correlation of rho = {rho:F6}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0.0357142857142857, u, 12);
        Assert.Equal(0.0833333333333333, v, 12);
        Assert.InRange(worst, 0.1190, 0.1191);
        Assert.InRange(quadrature, 0.0906, 0.0907);
        Assert.InRange(cancellation, 0.0476, 0.0477);

        // THE CLAIM: the literal is not a generic value, it is the worst-case extreme of the repository's own marginals.
        Assert.True(ObservationalRoadmapAudit.TheRecordedGenericCompactnessIsTheLinearExtreme());
        Assert.InRange(rho, -1.0, -0.99);
    }

    [Fact]
    public void Y_G_071_TheQuadratureIsSmallerThanTheRecordedLiteral()
    {
        PrintHeader("G_071 - the recorded literal against the quadrature form");

        var table = ObservationalRoadmapAudit.GenericTable();
        var sb = new StringBuilder();
        sb.AppendLine("  model                     sigma_x/x     ratio to the recorded literal");
        foreach (var g in table)
            sb.AppendLine($"  {g.Model,-58} {g.RelativeSigmaX:P4}    {g.RatioToRecorded:F3}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, table.Length);
        // The recorded literal is the worst case, so it is the LARGEST of the three.
        Assert.InRange(table[0].RatioToRecorded, 0.999, 1.001);
        Assert.InRange(table[1].RatioToRecorded, 0.761, 0.763);
        Assert.InRange(table[2].RatioToRecorded, 0.399, 0.401);

        // The compactness uncertainty the audits have been quoting is 1.313x the quadrature value for the same object.
        Assert.InRange(ObservationalRoadmapAudit.RecordedGenericCompactness / table[1].RelativeSigmaX, 1.310, 1.315);
    }

    [Fact]
    public void Y_G_071_TheCurrentSignificanceIsHigherThanRecorded()
    {
        PrintHeader("G_071 - the current significance under each compactness model");

        var current = ObservationalRoadmapAudit.CurrentSignificance();
        var sb = new StringBuilder();
        sb.AppendLine("  compactness model                    sigma_x/x    single-theory   difference");
        foreach (var c in current)
            sb.AppendLine($"  {c.CompactnessModel,-36} {c.RelativeSigmaX:P4}      {c.SingleTheory:F4}          {c.Difference:F4}");
        sb.AppendLine();
        sb.AppendLine("  the four published targets at a 20 % timing:");
        foreach (var t in ObservationalDecisionAudit.TargetCapability())
            sb.AppendLine($"    {t.Name,-28} significance {t.NowTwentyPercent:F4}");
        Output.WriteLine(sb.ToString());

        // The recorded literal reproduces G_069's own 1.05 sigma, which is the cross-check that the trace is right.
        Assert.InRange(current[0].SingleTheory, 1.05, 1.06);
        Assert.InRange(current[1].SingleTheory, 1.12, 1.13);

        // THE FINDING: the generic value recorded as 11.90 % is the pessimistic extreme, and the harmonic-free
        // (quadrature) form raises the current significance by more than 6 %.
        Assert.InRange(current[1].SingleTheory / current[0].SingleTheory, 1.06, 1.08);
    }

    [Fact]
    public void Y_G_071_TheRoadmapHasTheThreeRowsTheQuestionAsksFor()
    {
        PrintHeader("G_071 - the roadmap: CURRENT / 3SIGMA / 5SIGMA");

        var rows = ObservationalRoadmapAudit.Roadmap("A_SHARED_X", 0.0, 0.05);
        var sb = new StringBuilder();
        sb.AppendLine("  row      target                 timing   compactness   each     sigma_M      sigma_R      significance");
        foreach (var r in rows)
            sb.AppendLine($"  {r.Row,-8} {r.Target,-22} {r.Timing:P2}     {r.Compactness:P2}          {r.MassShare:P2}     "
                + $"{r.MassSolar:F3} M_sun  {r.RadiusKm:F3} km   {r.ResultingSignificance:F4}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, rows.Length);
        Assert.Equal("CURRENT", rows[0].Row);
        Assert.Equal("3SIGMA", rows[1].Row);
        Assert.Equal("5SIGMA", rows[2].Row);

        // The target is the most compact published object.
        Assert.Equal(ObservationalDecisionAudit.BestTarget(), rows[0].Target);

        // CURRENT is the published state under the published timing: 3.661 % compactness, giving 2.18 sigma.
        Assert.InRange(rows[0].Compactness, 0.0366, 0.0367);
        Assert.InRange(rows[0].Timing, 0.199, 0.201);
        Assert.InRange(rows[0].ResultingSignificance, 2.17, 2.19);

        // 3 sigma: a 5 % timing with the mass and radius each to 7.54 %, i.e. 0.156 solar masses and 0.934 km.
        Assert.InRange(rows[1].Timing, 0.049, 0.051);
        Assert.InRange(rows[1].MassShare, 0.0753, 0.0755);
        Assert.InRange(rows[1].MassSolar, 0.155, 0.157);
        Assert.InRange(rows[1].RadiusKm, 0.933, 0.935);
        Assert.Equal(3.0, rows[1].ResultingSignificance, 12);

        // 5 sigma: the same timing with each to 3.99 %, i.e. 0.083 solar masses and 0.494 km.
        Assert.InRange(rows[2].MassShare, 0.0398, 0.0400);
        Assert.InRange(rows[2].MassSolar, 0.082, 0.084);
        Assert.InRange(rows[2].RadiusKm, 0.493, 0.495);
        Assert.Equal(5.0, rows[2].ResultingSignificance, 12);

        // The worst-case column is the rho = -1 requirement, half the total, not the quadrature share.
        Assert.InRange(rows[1].WorstCaseEach, 0.0532, 0.0534);
        Assert.InRange(rows[1].WorstCaseEach / rows[1].MassShare, 0.706, 0.708);
    }

    [Fact]
    public void Y_G_071_TheMassAndRadiusRequirementIsAFrontierNotAPair()
    {
        PrintHeader("G_071 - why there is no single (mass, radius) requirement");

        double three = ObservationalRoadmapAudit.Roadmap("A_SHARED_X", 0.0, 0.05)[1].Compactness;
        var frontier = ObservationalRoadmapAudit.Frontier(three);

        var sb = new StringBuilder();
        sb.AppendLine($"  the 3 sigma compactness requirement is {three:P2}, and it decomposes differently per correlation model:");
        foreach (var f in frontier)
            sb.AppendLine($"    {f.Model,-28} {f.Constraint,-70} equal share each {(double.IsNaN(f.EqualShareEach) ? "free" : $"{f.EqualShareEach:P2}")}");
        sb.AppendLine();
        sb.AppendLine("  a SINGLE compactness number does NOT determine the two axis requirements - that is the audit's answer");
        sb.AppendLine("  to the form of the question, and it is why the roadmap states a frontier with the correlation named.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, frontier.Length);

        // Worst case demands each axis at half; independent at 1/sqrt(2). The factor between them is sqrt(2).
        Assert.InRange(frontier[0].EqualShareEach, 0.0532, 0.0534);
        Assert.InRange(frontier[1].EqualShareEach, 0.0753, 0.0755);
        Assert.InRange(frontier[1].EqualShareEach / frontier[0].EqualShareEach, 1.413, 1.415);

        // The cancellation case is DEGENERATE: only the difference is constrained, so the split itself is free.
        Assert.True(frontier[2].SplitIsFree);
        Assert.True(double.IsNaN(frontier[2].EqualShareEach));

        // One-sided form: with the mass pinned at 1 %, an independent (rho = 0) requirement on the radius.
        var oneSided = ObservationalRoadmapAudit.OneSided(three, 0.0, 0.01);
        Assert.InRange(oneSided.RequiredOtherShare, 0.106, 0.107);
        // In the worst case, the same pinning demands the radius to 9.66 % - the errors add rather than in quadrature.
        var oneSidedWorst = ObservationalRoadmapAudit.OneSided(three, -1.0, 0.01);
        Assert.InRange(oneSidedWorst.RequiredOtherShare, 0.0965, 0.0967);
    }

    [Fact]
    public void Y_G_071_TheRadiusCarriesTheLeverage()
    {
        PrintHeader("G_071 - which axis buys the decision, measured on the only object with a known split");

        var leverage = ObservationalRoadmapAudit.Leverage();
        double asymmetry = ObservationalRoadmapAudit.TheRadiusCarriesThisManyTimesTheMassError();

        var sb = new StringBuilder();
        sb.AppendLine($"  the radius carries {asymmetry:F3}x the mass's relative error on the only object with recorded marginals");
        foreach (var l in leverage)
            sb.AppendLine($"  {l.Move,-58} {l.From:P4} -> {l.To:P4}   improvement {l.Improvement:F3}x");
        sb.AppendLine();
        sb.AppendLine("  SO THE EFFORT SHOULD GO TO THE RADIUS - and note the trap the last row measures: EQUALISING UPWARD");
        sb.AppendLine("  (relaxing the mass to the radius's poorer precision) makes the compactness WORSE, not better.");
        Output.WriteLine(sb.ToString());

        Assert.InRange(asymmetry, 2.333, 2.334);

        Assert.Equal(4, leverage.Length);
        Assert.InRange(leverage[0].Improvement, 1.087, 1.089);   // mass to perfection
        Assert.InRange(leverage[1].Improvement, 2.538, 2.540);   // radius to perfection
        Assert.InRange(leverage[2].Improvement, 1.794, 1.796);   // equalise down
        Assert.InRange(leverage[3].Improvement, 0.768, 0.770);   // equalise up - worse, measured

        // The constraint that makes the finding structural: improving the radius dominates improving the mass,
        // because the radius's share enters at 2.333x.
        Assert.True(leverage[1].Improvement > leverage[0].Improvement);
        Assert.True(leverage[3].Improvement < 1.0);
    }

    [Fact]
    public void Y_G_071_BetterTimingRelaxesTheMassAndRadiusRequirement()
    {
        PrintHeader("G_071 - the surface: the mass-and-radius requirement is a function of the timing");

        var five = ObservationalRoadmapAudit.TimingSensitivity("A_SHARED_X");
        var singleTheory = ObservationalRoadmapAudit.TimingSensitivity("B_SINGLE_THEORY");

        var sb = new StringBuilder();
        sb.AppendLine("  reachable timing     3 sigma compactness   3 sigma each   5 sigma compactness   5 sigma each");
        foreach (var t in five)
            sb.AppendLine($"  {t.Timing,-18} {t.ThreeSigmaCompactness,-21:P2} {t.ThreeSigmaEach,-14:P2} {t.FiveSigmaCompactness,-21:P2} {t.FiveSigmaEach:P2}");
        sb.AppendLine();
        sb.AppendLine("  THE DIRECTION IS COUNTER-INTUITIVE AND IT IS MEASURED: a BETTER timing RELAXES the mass-and-radius");
        sb.AppendLine("  requirement, because the timing term takes a smaller share of the significance budget. At a 20 % timing");
        sb.AppendLine("  the requirement is UNDEFINED (NaN): the timing term alone already exceeds the 3 sigma budget at this");
        sb.AppendLine("  target, so NO mass-and-radius precision can reach it. The programme's first step is therefore the TIMING.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, five.Length);
        // At the published 20 % timing the requirement does not exist at all.
        Assert.True(double.IsNaN(five[0].ThreeSigmaCompactness));
        Assert.True(double.IsNaN(five[0].FiveSigmaCompactness));
        Assert.True(double.IsNaN(five[1].FiveSigmaCompactness));

        // Better timing -> a LARGER allowed compactness error (a weaker requirement), monotonically.
        Assert.InRange(five[1].ThreeSigmaCompactness, 0.0841, 0.0842);
        Assert.InRange(five[2].ThreeSigmaCompactness, 0.1066, 0.1067);
        Assert.InRange(five[3].ThreeSigmaCompactness, 0.11316, 0.11317);
        Assert.True(five[1].ThreeSigmaCompactness < five[2].ThreeSigmaCompactness);
        Assert.True(five[2].ThreeSigmaCompactness < five[3].ThreeSigmaCompactness);

        // The asymptote: with perfect timing the requirement is set by the separation alone, and cannot be relaxed further.
        Assert.InRange(five[3].FiveSigmaCompactness, 0.06789, 0.06791);

        // And the two models do NOT converge at perfect timing: the requirement scales as 1/|slope|, and at this
        // target the single-theory slope is the shallower, so it demands a LOOSER compactness (13.24 % against 11.32 %).
        Assert.InRange(singleTheory[3].ThreeSigmaCompactness / five[3].ThreeSigmaCompactness, 1.170, 1.171);
    }

    [Fact]
    public void Y_G_071_TheReport()
    {
        PrintHeader("G_071 - Observational Roadmap: the report");

        var sb = new StringBuilder();
        sb.AppendLine(ObservationalRoadmapAudit.OutputDecomposition());
        sb.AppendLine(ObservationalRoadmapAudit.OutputCurrent());
        sb.AppendLine(ObservationalRoadmapAudit.OutputRoadmap());
        sb.AppendLine(ObservationalRoadmapAudit.OutputVerdict());
        sb.AppendLine();
        sb.AppendLine(ObservationalRoadmapAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine(ObservationalRoadmapAudit.TheProgrammeInOneLine());
        Output.WriteLine(sb.ToString());

        var verdict = ObservationalRoadmapAudit.TheVerdict();
        Assert.Equal(3, verdict.Length);
        Assert.Equal("CURRENT", verdict[0].Row);
        Assert.Equal("UNDECIDED", verdict[0].State);
        Assert.Equal("REACHABLE", verdict[1].State);
        Assert.Equal("REACHABLE", verdict[2].State);
        Assert.Contains("DERIVED", ObservationalRoadmapAudit.Verdict());

        // The two significance models are NOT interchangeable at perfect timing either: the requirement scales as
        // 1/|slope|, so B's requirement is A's scaled by |A's slope| / |B's slope|.
        var a = ObservationalRoadmapAudit.TimingSensitivity("A_SHARED_X")[3].ThreeSigmaCompactness;
        var b = ObservationalRoadmapAudit.TimingSensitivity("B_SINGLE_THEORY")[3].ThreeSigmaCompactness;
        double x = ObservationalDecisionAudit.Catalogue()
            .Single(t => t.Name == ObservationalDecisionAudit.BestTarget()).X;
        double expected = Math.Abs(NeutronStarDecisionAudit.DSeparationDx(x)) / Math.Abs(NeutronStarDecisionAudit.DzAtDx(x));
        Assert.InRange(b / a, expected - 1e-9, expected + 1e-9);

        // At THIS target the single-theory slope is the shallower of the two (-1.2802 against -1.4981), so the
        // single-theory model demands a LOOSER compactness - the ordering is not a property of the model alone.
        Assert.True(Math.Abs(NeutronStarDecisionAudit.DzAtDx(x)) < Math.Abs(NeutronStarDecisionAudit.DSeparationDx(x)));
        Assert.True(b > a);
    }
}
