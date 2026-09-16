using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_004 - Schrodinger Correspondence Audit (group QM). Does AT's unitary flow reproduce the dispersion
/// of the Schrodinger equation? Compare the local difference, the centred difference, the Cayley flow and the
/// spectral derivative, measuring the phase velocity, the group velocity and the dispersion error against omega = k^2.
/// </summary>
public sealed class Y_QM_004_Tests : ResearchTestBase
{
    public Y_QM_004_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_QM_004_TheAtLaplacianSymbolIsRecoveredFromTheRecordedSpectrum()
    {
        PrintHeader("QM_004 - the native generator: is the recorded spectrum the C96(1..6) Laplacian?");

        var check = SchrodingerCorrespondenceAudit.SpectrumCheck();
        var sb = new StringBuilder();
        sb.AppendLine($"  the symbol mu(d) = sum over r = 1..{SchrodingerCorrespondenceAudit.Shells} of 2 - 2cos(r d)");
        sb.AppendLine("  channel   recorded mu   closed form    gap");
        foreach (var s in check.Where(s => s.Channel % 16 == 0 || s.Channel <= 3))
            sb.AppendLine($"  {s.Channel,-9} {s.Recorded,-13:F9} {s.Formula,-14:F9} {s.Gap:E2}");
        sb.AppendLine($"  worst gap over {check.Length} channels: {check.Max(s => s.Gap):E2}");
        sb.AppendLine();
        sb.AppendLine("  THE RECORDED SPECTRUM IS THE GRAPH LAPLACIAN OF THE CIRCULANT C96(1..6), to machine precision, so the");
        sb.AppendLine("  audit reproduces its input rather than importing a formula on trust.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(49, check.Length);
        Assert.True(SchrodingerCorrespondenceAudit.TheSymbolReproducesTheRecordedSpectrum());
        Assert.True(check.Max(s => s.Gap) < 1e-12);

        // The known landmarks: a zero mode, the recorded gap, twelve at the zone edge and fourteen at its peak.
        Assert.Equal(0.0, check[0].Recorded, 12);
        Assert.InRange(check[1].Recorded, 0.3863, 0.3864);
        Assert.Equal(12.0, check[48].Recorded, 9);
        // the maximum is NOT at a boundary: the six-shell symbol peaks at channel 11, inside the zone
        var peak = check.OrderByDescending(s => s.Recorded).First();
        Assert.Equal(11, peak.Channel);
        Assert.InRange(peak.Recorded, 15.83, 15.84);
    }

    [Fact]
    public void Y_QM_004_ThePowerLawSeparatesTheCandidatesInOneNumber()
    {
        PrintHeader("QM_004 - the decisive measure: d log omega / d log k");

        var table = SchrodingerCorrespondenceAudit.PowerLawTable();
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                power law   Schrodinger-like (2)");
        foreach (var t in table)
            sb.AppendLine($"  {t.Name,-40} {t.PowerLaw,-11:F4} {t.IsSchrodingerLike}");
        sb.AppendLine();
        sb.AppendLine("  THE REFERENCE IS omega = k^2, SO THE EXPONENT MUST BE 2. ALL FOUR NAMED CANDIDATES ARE FIRST-ORDER");
        sb.AppendLine("  GENERATORS AND GIVE 1 - INCLUDING THE SPECTRAL DERIVATIVE, WHICH IS EXACT ABOUT THE WRONG OPERATOR:");
        sb.AppendLine("  it matches omega = k exactly. Only the two LAPLACIANS give 2.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(6, table.Length);
        foreach (var name in new[] { SchrodingerCorrespondenceAudit.LocalDifference,
            SchrodingerCorrespondenceAudit.CentredDifference, SchrodingerCorrespondenceAudit.CayleyFlow,
            SchrodingerCorrespondenceAudit.SpectralDerivative })
        {
            double power = SchrodingerCorrespondenceAudit.PowerLaw(name);
            Assert.InRange(power, 0.99, 1.01);
        }
        Assert.InRange(SchrodingerCorrespondenceAudit.PowerLaw(SchrodingerCorrespondenceAudit.AtLaplacian), 1.99, 2.01);
        Assert.InRange(SchrodingerCorrespondenceAudit.PowerLaw(SchrodingerCorrespondenceAudit.NearestLaplacian), 1.99, 2.01);

        Assert.Equal(4, table.Count(t => !t.IsSchrodingerLike));
        Assert.Equal(2, table.Count(t => t.IsSchrodingerLike));
    }

    [Fact]
    public void Y_QM_004_TheFirstOrderCandidatesAreRefutedAndTheErrorDiverges()
    {
        PrintHeader("QM_004 - the four named candidates against omega = k^2");

        var verdicts = SchrodingerCorrespondenceAudit.CandidateVerdicts();
        var sb = new StringBuilder();
        foreach (var v in verdicts.Where(v => v.Verdict == "REFUTED"))
            sb.AppendLine($"  {v.Name}: {v.Basis}");
        sb.AppendLine();
        sb.AppendLine("  the raw error against omega = k^2 GROWS as k falls, because omega ~ k while the reference ~ k^2:");
        sb.AppendLine($"    first channel: {SchrodingerCorrespondenceAudit.RawError(SchrodingerCorrespondenceAudit.LocalDifference, 1):E2} "
            + $"for the local difference and {SchrodingerCorrespondenceAudit.RawError(SchrodingerCorrespondenceAudit.CayleyFlow, 1):E2} for the Cayley flow");
        sb.AppendLine("  a first-order generator cannot match a second-order law at ANY scale.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, verdicts.Count(v => v.Verdict == "REFUTED"));
        Assert.Equal(2, verdicts.Count(v => v.Verdict == "PARTIAL"));
        Assert.Equal(0, verdicts.Count(v => v.Verdict == "ANALOGOUS"));

        // The raw error diverges as k falls: it is much larger at channel 1 than at the zone edge.
        double atFirst = SchrodingerCorrespondenceAudit.RawError(SchrodingerCorrespondenceAudit.LocalDifference, 1);
        double atEdge = SchrodingerCorrespondenceAudit.RawError(SchrodingerCorrespondenceAudit.LocalDifference, 48);
        Assert.InRange(atFirst, 14.2, 14.4);
        Assert.Equal(1.0, atEdge, 9);                       // omega vanishes at the zone edge, so the error is exact
        Assert.True(atFirst > atEdge);

        // The Cayley flow carries the factor two QM_002 measured: its raw error is about twice the others'.
        Assert.InRange(SchrodingerCorrespondenceAudit.RawError(SchrodingerCorrespondenceAudit.CayleyFlow, 1), 29.4, 29.6);
    }

    [Fact]
    public void Y_QM_004_BothVelocitiesMustRiseAndOnlyTheLaplaciansDo()
    {
        PrintHeader("QM_004 - the phase and group velocities against k and 2k");

        var table = SchrodingerCorrespondenceAudit.MeasureTable();
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                power law   phase velocity   group velocity   error at the edge");
        foreach (var m in table)
            sb.AppendLine($"  {m.Name,-40} {m.PowerLaw,-11:F4} {m.PhaseVelocityAtHalf,-16:F6} {m.GroupVelocityAtHalf,-16:F6} {m.NormalisedErrorAtEdge:P2}");
        sb.AppendLine();
        sb.AppendLine("  Schrodinger has phase velocity k and group velocity 2k, so BOTH must RISE with k - and the reference");
        sb.AppendLine("  values at channel 24 are 1.570796 and 3.141593.");
        sb.AppendLine("  THE FIRST-ORDER FAMILY FAILS BOTH: its phase velocity is sin(d)/d, which falls, and its group velocity");
        sb.AppendLine("  is cos(d), which changes sign - at channel 24 it is exactly zero.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(6, table.Length);
        double k = SchrodingerCorrespondenceAudit.Delta(24);
        Assert.InRange(SchrodingerCorrespondenceAudit.ReferencePhaseVelocity(k), 1.5707, 1.5708);
        Assert.InRange(SchrodingerCorrespondenceAudit.ReferenceGroupVelocity(k), 3.1415, 3.1417);

        // The first-order family's group velocity vanishes at the fold and never rises with k.
        foreach (var name in new[] { SchrodingerCorrespondenceAudit.LocalDifference,
            SchrodingerCorrespondenceAudit.CentredDifference, SchrodingerCorrespondenceAudit.CayleyFlow })
            Assert.True(Math.Abs(SchrodingerCorrespondenceAudit.GroupVelocity(name, 24)) < 1e-6);

        // Their phase velocity FALLS with k, which is the opposite of the reference's rise.
        Assert.True(SchrodingerCorrespondenceAudit.PhaseVelocity(SchrodingerCorrespondenceAudit.LocalDifference, SchrodingerCorrespondenceAudit.Delta(48))
            < SchrodingerCorrespondenceAudit.PhaseVelocity(SchrodingerCorrespondenceAudit.LocalDifference, SchrodingerCorrespondenceAudit.Delta(1)));

        // The one-shell control rises to a quarter of the zone; the NATIVE generator peaks far earlier and then
        // folds, which is the measurement that separates them.
        Assert.True(SchrodingerCorrespondenceAudit.GroupVelocity(SchrodingerCorrespondenceAudit.NearestLaplacian, 24)
            > SchrodingerCorrespondenceAudit.GroupVelocity(SchrodingerCorrespondenceAudit.NearestLaplacian, 6));
        Assert.Equal(5, SchrodingerCorrespondenceAudit.GroupVelocityPeakChannel(SchrodingerCorrespondenceAudit.AtLaplacian));
        Assert.Equal(11, SchrodingerCorrespondenceAudit.FirstFoldChannel(SchrodingerCorrespondenceAudit.AtLaplacian));
        // the reference's group velocity at channel 24 is far above the native one, because the native has saturated
        // The native Laplacian OVERSHOOTS the reference at a quarter of the zone (6.0 against pi), having peaked
        // at channel 5 - it is not a scaled copy of Schrodinger's 2k.
        Assert.True(SchrodingerCorrespondenceAudit.GroupVelocity(SchrodingerCorrespondenceAudit.AtLaplacian, 24)
            > SchrodingerCorrespondenceAudit.ReferenceGroupVelocity(k));
        Assert.InRange(SchrodingerCorrespondenceAudit.GroupVelocity(SchrodingerCorrespondenceAudit.AtLaplacian, 24), 5.99, 6.01);
    }

    [Fact]
    public void Y_QM_004_TheNativeLaplacianIsSchrodingerLikeInShapeAndNotInCoefficient()
    {
        PrintHeader("QM_004 - the native Laplacian: right shape, its own coefficient");

        double coefficient = SchrodingerCorrespondenceAudit.EffectiveCoefficient(SchrodingerCorrespondenceAudit.AtLaplacian);
        var sb = new StringBuilder();
        sb.AppendLine($"  the long-wavelength expansion of the native Laplacian is mu = D k^2 with D = sum of r^2 = {coefficient:F0}");
        sb.AppendLine($"  the one-shell control has D = {SchrodingerCorrespondenceAudit.EffectiveCoefficient(SchrodingerCorrespondenceAudit.NearestLaplacian):F0}");
        sb.AppendLine($"  so the native generator IS a Schrodinger propagator with an effective coefficient, and the coefficient is");
        sb.AppendLine($"  fixed by the SHELL SET rather than by any new ingredient.");
        sb.AppendLine();
        sb.AppendLine($"  after dividing that coefficient out, the shape error at the zone edge is "
            + $"{SchrodingerCorrespondenceAudit.NormalisedError(SchrodingerCorrespondenceAudit.AtLaplacian, 48):P2} for the native");
        sb.AppendLine($"  Laplacian and {SchrodingerCorrespondenceAudit.NormalisedError(SchrodingerCorrespondenceAudit.NearestLaplacian, 48):P2} for the one-shell control:");
        sb.AppendLine("  SIX SHELLS CANCEL PARTIALLY AT HIGH k, so the native generator saturates MUCH earlier than the one-shell one.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(91.0, coefficient, 9);                       // 1+4+9+16+25+36
        Assert.Equal(1.0, SchrodingerCorrespondenceAudit.EffectiveCoefficient(SchrodingerCorrespondenceAudit.NearestLaplacian), 9);

        // The native coefficient really is the small-k limit of omega/k^2.
        double fitted = SchrodingerCorrespondenceAudit.OmegaAt(SchrodingerCorrespondenceAudit.AtLaplacian, 1e-4) / 1e-8;
        Assert.InRange(fitted, 90.99, 91.01);

        // The six-shell shape error at the edge is far worse than the one-shell one.
        double native = SchrodingerCorrespondenceAudit.NormalisedError(SchrodingerCorrespondenceAudit.AtLaplacian, 48);
        double control = SchrodingerCorrespondenceAudit.NormalisedError(SchrodingerCorrespondenceAudit.NearestLaplacian, 48);
        Assert.InRange(native, 0.986, 0.987);
        Assert.InRange(control, 0.594, 0.595);
        Assert.True(native > control);
    }

    [Fact]
    public void Y_QM_004_TheNativeLaplacianReversesThroughShellInterferenceNotOddness()
    {
        PrintHeader("QM_004 - the folds: two different mechanisms");

        var census = SchrodingerCorrespondenceAudit.FoldCensus();
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                reversed channels   stationary   sign changes   reverses");
        foreach (var f in census)
            sb.AppendLine($"  {f.Name,-40} {f.Reversed,-19} {f.Stationary,-12} {f.FoldSignChanges,-14} {f.Reverses}");
        sb.AppendLine();
        sb.AppendLine("  THE FIRST-ORDER FAMILY reverses because its symbol is ODD (QM_003's theorem): one sign change and half");
        sb.AppendLine("  the band going backwards. THE NATIVE LAPLACIAN's symbol is EVEN and does NOT vanish at the zone edge");
        sb.AppendLine("  (mu(pi) = 12), yet it reverses 23 channels with FIVE sign changes - SHELL INTERFERENCE, a different");
        sb.AppendLine("  mechanism. And the one-shell Laplacian never reverses at all.");
        sb.AppendLine();
        sb.AppendLine("  NOT REVERSING IS NOT THE SAME AS BEING MONOTONE, and a first version of this audit conflated them:");
        sb.AppendLine("  the one-shell Laplacian does not reverse (2 sin d >= 0) and still peaks at pi/2, so it is not monotone.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(6, census.Length);
        var native = census.Single(c => c.Name == SchrodingerCorrespondenceAudit.AtLaplacian);
        var nearest = census.Single(c => c.Name == SchrodingerCorrespondenceAudit.NearestLaplacian);
        var firstOrder = census.Single(c => c.Name == SchrodingerCorrespondenceAudit.LocalDifference);

        Assert.Equal(23, native.Reversed);
        Assert.Equal(5, native.FoldSignChanges);
        Assert.True(native.Reverses);
        Assert.Equal(0, nearest.Reversed);
        Assert.False(nearest.Reverses);
        Assert.Equal(24, firstOrder.Reversed);
        Assert.Equal(1, firstOrder.FoldSignChanges);

        // The distinct mechanisms: the native one has an even symbol that does not vanish at the zone edge.
        Assert.InRange(SchrodingerCorrespondenceAudit.AtLaplacianSymbol(Math.PI), 11.99, 12.01);
        Assert.True(Math.Abs(SchrodingerCorrespondenceAudit.OmegaAt(SchrodingerCorrespondenceAudit.LocalDifference, Math.PI)) < 1e-15);
        Assert.False(SchrodingerCorrespondenceAudit.IsMonotone(SchrodingerCorrespondenceAudit.NearestLaplacian));
    }

    [Fact]
    public void Y_QM_004_TheVerdictIsPartialBecauseTheCandidateListHasTheWrongOrder()
    {
        PrintHeader("QM_004 - the verdict");

        var counts = SchrodingerCorrespondenceAudit.VerdictCounts();
        var verdict = SchrodingerCorrespondenceAudit.Verdict();

        var sb = new StringBuilder();
        sb.AppendLine(verdict);
        sb.AppendLine();
        sb.AppendLine(SchrodingerCorrespondenceAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(0, counts.Analogous);
        Assert.Equal(2, counts.Partial);          // the two Laplacians
        Assert.Equal(4, counts.Refuted);          // every candidate the question names
        Assert.Contains("PARTIAL", verdict);
        Assert.Contains("WRONG DIFFERENTIAL ORDER", verdict);
        Assert.Contains("LAPLACIAN", verdict);
    }

    [Fact]
    public void Y_QM_004_TheReport()
    {
        PrintHeader("QM_004 - Schrodinger correspondence: the report");

        var sb = new StringBuilder();
        sb.AppendLine(SchrodingerCorrespondenceAudit.OutputSpectrumCheck());
        sb.AppendLine(SchrodingerCorrespondenceAudit.OutputMeasures());
        sb.AppendLine(SchrodingerCorrespondenceAudit.OutputFolds());
        sb.AppendLine(SchrodingerCorrespondenceAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());

        // The conservation-style test: the reference is honoured at every channel for the one-shell Laplacian's
        // group velocity, which is EXACTLY the lattice image of Schrodinger's 2k.
        foreach (var channel in PhaseEvolutionAudit.Channels())
        {
            double d = SchrodingerCorrespondenceAudit.Delta(channel);
            Assert.Equal(2.0 * Math.Sin(d), SchrodingerCorrespondenceAudit.GroupVelocity(SchrodingerCorrespondenceAudit.NearestLaplacian, channel), 6);
        }
        // The goal answered as a count: how many OCCUPIED modes are Schrodinger-like.
        var window = SchrodingerCorrespondenceAudit.SchrodingerWindow();
        var native = window.Single(w => w.Name == SchrodingerCorrespondenceAudit.AtLaplacian);
        var control = window.Single(w => w.Name == SchrodingerCorrespondenceAudit.NearestLaplacian);
        Assert.InRange(native.OccupiedInWindow, 1, 6);
        Assert.True(control.OccupiedInWindow > native.OccupiedInWindow);
        // and the first-order family is out of the window immediately
        foreach (var name in new[] { SchrodingerCorrespondenceAudit.LocalDifference,
            SchrodingerCorrespondenceAudit.CayleyFlow, SchrodingerCorrespondenceAudit.SpectralDerivative })
            Assert.Equal(0, window.Single(w => w.Name == name).ChannelsInWindow);
        Assert.Equal(3, native.ChannelsInWindow);
        Assert.Equal(17, control.ChannelsInWindow);
        Assert.Equal(3, native.OccupiedInWindow);
        Assert.Equal(16, control.OccupiedInWindow);
        // the window is taken contiguously, and the excluded interior passes are reported rather than hidden
        Assert.True(SchrodingerCorrespondenceAudit.InteriorCoincidences()
            .Single(c => c.Name == SchrodingerCorrespondenceAudit.LocalDifference).InteriorPasses > 0);
    }
}
