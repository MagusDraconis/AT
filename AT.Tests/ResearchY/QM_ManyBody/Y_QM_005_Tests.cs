using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_005 - Laplacian Dispersion Audit (group QM). Can the native D96(1..6) Laplacian produce a
/// Schrodinger-compatible dispersion in any physical regime? Determine whether the fold is a consequence of the
/// six-shell geometry or an avoidable representation choice.
/// </summary>
public sealed class Y_QM_005_Tests : ResearchTestBase
{
    public Y_QM_005_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private static int Native => LaplacianDispersionAudit.NativeMask;

    [Fact]
    public void Y_QM_005_ThePowerLawIsRepresentationIndependent()
    {
        PrintHeader("QM_005 - half one: the exponent is forced by the order of the operator");

        var sb = new StringBuilder();
        sb.AppendLine($"  the family: {LaplacianDispersionAudit.Subsets().Length} non-empty subsets of the six shells");
        sb.AppendLine($"  every one of them is a Laplacian, so every symbol is a sum of terms 2 - 2cos(r d), each starting at (r d)^2");
        sb.AppendLine();
        sb.AppendLine("  subset                                   D      power law");
        foreach (var mask in new[] { Native, 1, 0b10, 0b11, 0b100000, 0b101010 })
            sb.AppendLine($"  {LaplacianDispersionAudit.Name(mask),-40} {LaplacianDispersionAudit.Coefficient(mask),-6:F0} {LaplacianDispersionAudit.PowerLaw(mask):F6}");
        sb.AppendLine();
        sb.AppendLine("  MEASURED ON ALL 63 SUBSETS: the exponent is 2 in every case, so SCHRODINGER COMPATIBILITY IN THE");
        sb.AppendLine("  LONG-WAVELENGTH REGIME IS A PROPERTY OF THE OPERATOR'S ORDER AND NOT OF THE SHELL CHOICE.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(63, LaplacianDispersionAudit.Subsets().Length);
        Assert.True(LaplacianDispersionAudit.ThePowerLawIsRepresentationIndependent());
        Assert.True(LaplacianDispersionAudit.TheCoefficientIsTheSecondMoment());
        foreach (var mask in LaplacianDispersionAudit.Subsets())
        {
            Assert.InRange(LaplacianDispersionAudit.PowerLaw(mask), 1.999, 2.001);
            // the coefficient is the second moment, checked at a small wavenumber for every subset
            double fitted = LaplacianDispersionAudit.Omega(mask, 1e-4) / 1e-8;
            Assert.InRange(fitted, 0.999 * LaplacianDispersionAudit.Coefficient(mask), 1.001 * LaplacianDispersionAudit.Coefficient(mask));
        }
        Assert.Equal(91.0, LaplacianDispersionAudit.Coefficient(Native), 9);
        Assert.Equal(1.0, LaplacianDispersionAudit.Coefficient(1), 9);
    }

    [Fact]
    public void Y_QM_005_TheFoldArrivesWithTheSecondShell()
    {
        PrintHeader("QM_005 - half two: the fold is a property of WHICH shells interfere");

        var single = LaplacianDispersionAudit.SingleShellFolds();
        var sb = new StringBuilder();
        sb.AppendLine("  shell   measured fold channel   closed form 48/r   first channel past it");
        foreach (var s in single)
            sb.AppendLine($"  {s.Shell,-7} {s.MeasuredFoldChannel,-23} {s.PredictedChannel,-19:F1} {(s.MeasuredFoldChannel == 0 ? "-" : "yes")}");
        sb.AppendLine();
        sb.AppendLine("  a single shell's group velocity is 2r sin(r d), which is non-negative until r*d passes pi - so the fold");
        sb.AppendLine("  arrives with the SECOND shell, at channel 25, and moves INWARD as the shells grow. {1} ALONE NEVER FOLDS.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(6, single.Length);
        Assert.Equal(0, single[0].MeasuredFoldChannel);                                  // {1} never folds
        Assert.Equal(25, single[1].MeasuredFoldChannel);
        Assert.Equal(17, single[2].MeasuredFoldChannel);
        Assert.Equal(13, single[3].MeasuredFoldChannel);
        Assert.Equal(10, single[4].MeasuredFoldChannel);
        Assert.Equal(9, single[5].MeasuredFoldChannel);

        // The measurement agrees with the closed form to ONE CHANNEL, in every case.
        foreach (var s in single.Where(s => s.Shell >= 2))
            Assert.Equal((int)Math.Floor(s.PredictedChannel) + 1, s.MeasuredFoldChannel);
        Assert.False(LaplacianDispersionAudit.Folds(1));
        Assert.True(LaplacianDispersionAudit.Folds(Native));
    }

    [Fact]
    public void Y_QM_005_TheCensusSaysTheFoldIsNearlyUniversalAndTheCuresAreSingleton()
    {
        PrintHeader("QM_005 - the census over all 63 subsets");

        var census = LaplacianDispersionAudit.FoldCensus();
        var sb = new StringBuilder();
        sb.AppendLine("  subset size   subsets   folding   earliest fold   latest fold");
        foreach (var group in census.GroupBy(f => f.Shells).OrderBy(g => g.Key))
            sb.AppendLine($"  {group.Key,-13} {group.Count(),-9} {group.Count(f => f.FoldChannel > 0),-9} "
                + $"{group.Where(f => f.FoldChannel > 0).Select(f => f.FoldChannel).DefaultIfEmpty(0).Min(),-15} "
                + $"{group.Where(f => f.FoldChannel > 0).Select(f => f.FoldChannel).DefaultIfEmpty(0).Max()}");
        sb.AppendLine();
        sb.AppendLine($"  {LaplacianDispersionAudit.FoldingSubsets()} of 63 fold; the {LaplacianDispersionAudit.NonFoldingSubsets()} that never fold:");
        foreach (var c in LaplacianDispersionAudit.NonFoldingCures())
            sb.AppendLine($"    {c.Name} with D = {c.Coefficient:F0}, window {c.Window} channels, {c.OccupiedInWindow} occupied modes");
        sb.AppendLine();
        sb.AppendLine("  THE CURE IS UNIQUE IN KIND: a single shell {1}, and it costs a coefficient of 1 against the native 91.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(63, census.Length);
        Assert.Equal(62, LaplacianDispersionAudit.FoldingSubsets());
        Assert.Equal(1, LaplacianDispersionAudit.NonFoldingSubsets());
        Assert.Single(LaplacianDispersionAudit.NonFoldingCures());
        Assert.Equal(1, LaplacianDispersionAudit.NonFoldingCures()[0].Mask);

        // NINE subsets fold earliest (at channel 9) and the latest fold is channel 28.
        Assert.Equal(9, census.Where(f => f.FoldChannel > 0).Min(f => f.FoldChannel));
        Assert.Equal(28, LaplacianDispersionAudit.LatestFold().FoldChannel);
        Assert.Equal(0b11, LaplacianDispersionAudit.LatestFold().Mask);                 // {1,2}
        Assert.Equal(11, LaplacianDispersionAudit.FirstFold(Native));
        Assert.Equal(23, LaplacianDispersionAudit.Reversals(Native));
    }

    [Fact]
    public void Y_QM_005_TheOneRingCureBuysTheMostCoverage()
    {
        PrintHeader("QM_005 - occupied-mode coverage: what each representation buys");

        var sb = new StringBuilder();
        sb.AppendLine("  subset                                   D      fold    window   occupied   first failure");
        foreach (var mask in new[] { Native, 0b11, 0b110, 1, 0b10 })
        {
            var w = LaplacianDispersionAudit.SchrodingerWindow(mask);
            sb.AppendLine($"  {LaplacianDispersionAudit.Name(mask),-40} {LaplacianDispersionAudit.Coefficient(mask),-6:F0} "
                + $"{LaplacianDispersionAudit.FirstFold(mask),-7} {w.ChannelsInWindow,-8} {w.OccupiedInWindow,-10} {w.FirstChannelThatFails}");
        }
        sb.AppendLine();
        var best = LaplacianDispersionAudit.BestCoverage();
        sb.AppendLine($"  THE BEST OCCUPIED COVERAGE IS {best.OccupiedInWindow} of 42 modes at {best.Name} (D = {best.Coefficient:F0}) - ");
        sb.AppendLine($"  a factor {best.OccupiedInWindow / (double)LaplacianDispersionAudit.SchrodingerWindow(Native).OccupiedInWindow:F2} better than the native set's 3.");
        Output.WriteLine(sb.ToString());

        var native = LaplacianDispersionAudit.SchrodingerWindow(Native);
        var oneShell = LaplacianDispersionAudit.SchrodingerWindow(1);
        Assert.Equal(3, native.ChannelsInWindow);
        Assert.Equal(3, native.OccupiedInWindow);
        Assert.Equal(17, oneShell.ChannelsInWindow);
        Assert.Equal(16, oneShell.OccupiedInWindow);
        Assert.Equal(42, native.Occupied);

        var winner = LaplacianDispersionAudit.BestCoverage();
        Assert.Equal(1, winner.Mask);                     // {1} wins
        Assert.Equal(16, winner.OccupiedInWindow);
        Assert.True(winner.OccupiedInWindow > native.OccupiedInWindow);
    }

    [Fact]
    public void Y_QM_005_TheContinuumExpansionPredictsWhereTheCorrespondenceEnds()
    {
        PrintHeader("QM_005 - the continuum expansion: omega = D k^2 - E k^4");

        var table = LaplacianDispersionAudit.ContinuumExpansion();
        var sb = new StringBuilder();
        sb.AppendLine("  subset                                   D      E          k at 10 %    measured window");
        foreach (var c in table)
            sb.AppendLine($"  {c.Name,-40} {c.D,-6:F0} {c.E,-10:F2} {c.KAtTenPercent,-12:F4} {c.MeasuredWindow}");
        sb.AppendLine();
        sb.AppendLine("  the analytic estimate of where the quartic term reaches 10 % of the quadratic tracks the measured window:");
        sb.AppendLine("  the wider the k-regime, the more channels inside the window.");
        Output.WriteLine(sb.ToString());

        var native = table.Single(c => c.Mask == Native);
        var oneShell = table.Single(c => c.Mask == 1);
        Assert.Equal(91.0, native.D, 9);
        Assert.InRange(native.E, 189.5, 189.7);            // sum r^4 / 12
        Assert.InRange(oneShell.E, 0.083, 0.084);          // 1/12
        Assert.InRange(native.KAtTenPercent, 0.219, 0.220);
        Assert.InRange(oneShell.KAtTenPercent, 1.095, 1.096);

        // The analytic estimate and the measured window agree in ORDER: the wider k-regime has the wider window.
        Assert.True(oneShell.KAtTenPercent > native.KAtTenPercent);
        Assert.True(oneShell.MeasuredWindow > native.MeasuredWindow);

        // The quartic coefficient is the fourth moment over twelve, verified against the closed form.
        Assert.Equal(LaplacianDispersionAudit.ShellSet(Native).Sum(r => Math.Pow(r, 4)) / 12.0, native.E, 9);
    }

    [Fact]
    public void Y_QM_005_TheGroupVelocityAgreesWithItsClosedForm()
    {
        PrintHeader("QM_005 - the group velocity, checked against its own closed form");

        var sb = new StringBuilder();
        sb.AppendLine("  subset                  channel   closed form   finite difference of omega");
        foreach (var mask in new[] { Native, 1, 0b11 })
        foreach (var channel in new[] { 1, 12, 24, 36, 48 })
        {
            double d = LaplacianDispersionAudit.Delta(channel);
            double closed = LaplacianDispersionAudit.GroupVelocity(mask, channel);
            double h = 1e-7;
            double finite = (LaplacianDispersionAudit.Omega(mask, d + h) - LaplacianDispersionAudit.Omega(mask, d - h)) / (2.0 * h);
            sb.AppendLine($"  {LaplacianDispersionAudit.Name(mask),-22} {channel,-9} {closed,-13:F6} {finite:F6}");
            Assert.Equal(closed, finite, 5);
        }
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_QM_005_TheVerdictIsBoundaryBecauseTheTwoHalvesSeparate()
    {
        PrintHeader("QM_005 - the verdict");

        var halves = LaplacianDispersionAudit.TheTwoHalves();
        var counts = LaplacianDispersionAudit.VerdictCounts();
        var sb = new StringBuilder();
        foreach (var h in halves)
        {
            sb.AppendLine($"{h.Question}: {h.Answer}");
            sb.AppendLine($"  {h.Basis}");
        }
        sb.AppendLine();
        sb.AppendLine(LaplacianDispersionAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine(LaplacianDispersionAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, halves.Length);
        Assert.Equal("DERIVED", halves[0].Answer);         // the power law is forced
        Assert.Equal("BOUNDARY", halves[1].Answer);        // the fold is a choice
        Assert.Equal("BOUNDARY", halves[2].Answer);        // and the cure is not free
        Assert.Equal(1, counts.Derived);
        Assert.Equal(2, counts.Boundary);
        Assert.Equal(0, counts.Refuted);

        var verdict = LaplacianDispersionAudit.Verdict();
        Assert.Contains("BOUNDARY", verdict);
        Assert.Contains("AVOIDABLE REPRESENTATION CHOICE", verdict);
        Assert.Contains("NOT FREE", verdict);
    }

    [Fact]
    public void Y_QM_005_TheReport()
    {
        PrintHeader("QM_005 - Laplacian dispersion: the report");

        var sb = new StringBuilder();
        sb.AppendLine(LaplacianDispersionAudit.OutputCensus());
        sb.AppendLine(LaplacianDispersionAudit.OutputSingleShells());
        sb.AppendLine(LaplacianDispersionAudit.OutputCoverage());
        sb.AppendLine(LaplacianDispersionAudit.OutputContinuum());
        sb.AppendLine(LaplacianDispersionAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());

        // The structural claim behind the first half: the symbol's linear term vanishes for every subset, which is
        // precisely why the exponent is 2. Checked as a limit rather than asserted.
        foreach (var mask in LaplacianDispersionAudit.Subsets())
        {
            double small = LaplacianDispersionAudit.Omega(mask, 1e-6);
            Assert.True(small > 0.0);
            // omega is quadratic, so halving delta divides it by four
            Assert.Equal(4.0, LaplacianDispersionAudit.Omega(mask, 1e-6) / LaplacianDispersionAudit.Omega(mask, 5e-7), 8);
        }

        // AND THE REWRITE THAT MADE THE LIMIT TESTABLE IS ITSELF MEASURED. The direct form 2 - 2cos loses digits to
        // catastrophic cancellation at small delta, which is precisely the regime the power-law fit consumes.
        var cancellation = LaplacianDispersionAudit.CancellationTable(LaplacianDispersionAudit.NativeMask);
        sb.Clear();
        sb.AppendLine("  the cancellation the half-angle form removes (native set):");
        sb.AppendLine("    delta        safe 4sin^2(rd/2)   direct 2 - 2cos   relative error of the direct form");
        foreach (var c in cancellation)
            sb.AppendLine($"    {c.Delta,-12:E0} {c.Safe,-20:E3} {c.Direct,-18:E3} {c.RelativeError:P2}");
        Output.WriteLine(sb.ToString());
        Assert.True(cancellation[0].RelativeError < 1e-6);          // fine at 1e-4
        Assert.True(cancellation[3].RelativeError > 1e-5);          // and measurably degraded at 1e-7 (0.01 %)
    }
}
