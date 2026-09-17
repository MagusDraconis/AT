using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_008 - Spectral Necessity Audit (group QM). Which AT conclusions actually require the native {1..6}
/// spectrum? Recompute each on {1}, {1,2} and {1..6}, classify every result UNCHANGED / BOUNDARY / REFUTED, and
/// determine whether the spectral fingerprint is physically indispensable or only historically inherited.
/// </summary>
public sealed class Y_QM_008_Tests : ResearchTestBase
{
    public Y_QM_008_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private const int Singleton = 1;
    private const int Pair = 0b11;
    private const int Native = 0b111111;

    [Fact]
    public void Y_QM_008_TheRecomputationRebuildsTheStateAndTheRebuildIsChecked()
    {
        PrintHeader("QM_008 - the state is spectrum-derived, so the recomputation rebuilds it and is checked back");

        var sb = new StringBuilder();
        sb.AppendLine("  The canonical state walks the SPECTRUM's levels (G_062's recipe), so replacing the generator replaces");
        sb.AppendLine("  the STATE. These numbers are therefore rebuilt from the candidate spectrum, not re-read from the record.");
        sb.AppendLine();
        sb.AppendLine($"  fidelity of the rebuild against the RECORDED state, native branch: {SpectralNecessityAudit.StateFidelityResidual():E3}");
        sb.AppendLine();
        sb.AppendLine("  how far the canonical state MOVES when the spectrum is replaced:");
        sb.AppendLine("    pair                max shift      L2 shift       correlation");
        foreach (var s in SpectralNecessityAudit.StateShiftTable())
            sb.AppendLine($"    {s.Pair,-20} {s.MaxShift,-14:F6} {s.L2Shift,-14:F6} {s.Correlation:F6}");
        sb.AppendLine();
        sb.AppendLine("  each candidate's level structure:");
        sb.AppendLine("    candidate   levels   free room   maximum        trace");
        foreach (int m in SpectralNecessityAudit.Masks())
            sb.AppendLine($"    {SpectralNecessityAudit.DisplayName(m),-11} {SpectralNecessityAudit.LevelCountOf(m),-8} "
                + $"{SpectralNecessityAudit.FreeRoomOf(m),-11} {SpectralNecessityAudit.MaxOf(m),-14:F6} {SpectralNecessityAudit.TraceOf(m),-10:F2}");
        Output.WriteLine(sb.ToString());

        // the rebuild must reproduce the recorded state, otherwise the comparison is between my reconstruction and itself
        Assert.True(SpectralNecessityAudit.StateFidelityResidual() < 1e-12);

        // and the state must genuinely move, otherwise the "recomputation" would be a re-read.
        // AN ASSUMPTION OF MY FIRST DRAFT IS REFUTED HERE: I expected a small perturbation (correlation above 0.9)
        // and the measurement gives 0.19 to 0.24 - the three canonical states are nearly ORTHOGONAL.
        var shifts = SpectralNecessityAudit.StateShiftTable();
        Assert.All(shifts, s => Assert.True(s.MaxShift > 0.05, $"{s.Pair} moves only {s.MaxShift:F6}"));
        Assert.All(shifts, s => Assert.True(s.Correlation < 0.3, $"{s.Pair} correlation {s.Correlation:F6}"));

        Assert.Equal(45, SpectralNecessityAudit.LevelCountOf(Native));
        Assert.Equal(51, SpectralNecessityAudit.FreeRoomOf(Native));
    }

    [Fact]
    public void Y_QM_008_TheObservableSplitIsRebuiltFromEachCandidatesOwnState()
    {
        PrintHeader("QM_008 - the observable split, rebuilt from each candidate's own state");

        var sb = new StringBuilder();
        sb.AppendLine("  candidate   observable rank   mean   amplitude   phase   hidden   visible   split modes");
        foreach (int m in SpectralNecessityAudit.Masks())
        {
            var s = SpectralNecessityAudit.SplitOf(m);
            var c = SpectralNecessityAudit.ModeCensus(m);
            sb.AppendLine($"  {SpectralNecessityAudit.DisplayName(m),-11} {s.ObservableRank,-17} {s.Mean,-6} {s.Amplitude,-11} "
                + $"{s.Phase,-7} {c.Hidden,-8} {c.Visible,-9} {c.Split}");
        }
        sb.AppendLine();
        sb.AppendLine("  the rebuild against the FOUR quantities the other audits recorded independently:");
        sb.AppendLine("    quantity             rebuilt   recorded   agrees");
        foreach (var r in SpectralNecessityAudit.RecordedSplitCheck())
            sb.AppendLine($"    {r.Quantity,-20} {r.Rebuilt,-9} {r.Recorded,-10} {r.Agrees}");
        Output.WriteLine(sb.ToString());

        // the rebuild is tied to the record, not to itself
        Assert.All(SpectralNecessityAudit.RecordedSplitCheck(), r => Assert.True(r.Agrees, $"{r.Quantity}: {r.Rebuilt} vs {r.Recorded}"));

        var native = SpectralNecessityAudit.SplitOf(Native);
        Assert.Equal(43, native.ObservableRank);
        Assert.Equal(42, native.Amplitude);
        Assert.Equal(53, native.Phase);
        Assert.Equal(SpectralNecessityAudit.Cells, native.Mean + native.Amplitude + native.Phase);

        var census = SpectralNecessityAudit.ModeCensus(Native);
        Assert.Equal(53, census.Hidden);
        Assert.Equal(42, census.Visible);
        Assert.Equal(0, census.Split);

        // AND THE SPLIT MOVES: QM_007 asserted it is |S|-independent, and the recomputation refuses that
        var one = SpectralNecessityAudit.SplitOf(Singleton);
        var pair = SpectralNecessityAudit.SplitOf(Pair);
        Assert.Equal(47, one.ObservableRank);
        Assert.Equal(46, one.Amplitude);
        Assert.Equal(49, one.Phase);
        Assert.Equal(45, pair.ObservableRank);
        Assert.Equal(44, pair.Amplitude);
        Assert.Equal(51, pair.Phase);
        Assert.NotEqual(native.Amplitude, one.Amplitude);

        // but the STRUCTURE survives: every candidate's kernel is still a union of Fourier modes
        foreach (int m in SpectralNecessityAudit.Masks())
        {
            var c = SpectralNecessityAudit.ModeCensus(m);
            Assert.Equal(0, c.Split);
            Assert.Equal(SpectralNecessityAudit.Cells - 1, c.Hidden + c.Visible);
            var s = SpectralNecessityAudit.SplitOf(m);
            Assert.Equal(SpectralNecessityAudit.Cells, s.Mean + s.Amplitude + s.Phase);
        }
    }

    [Fact]
    public void Y_QM_008_TheStructuralConclusionsDoNotMentionTheSpectrum()
    {
        PrintHeader("QM_008 - the structural block: the same statement, the same value, on every generator");

        var table = SpectralNecessityAudit.ConclusionTable();
        var sb = new StringBuilder();
        foreach (var r in table.Where(r => r.Classification == "UNCHANGED"))
            sb.AppendLine($"  {r.Conclusion,-52} {r.Source,-19} {r.One,-14} {r.Pair,-14} {r.Native}");
        Output.WriteLine(sb.ToString());

        // the structural conclusions: recomputed on each candidate's OWN state and identical
        Assert.Equal("UNCHANGED", Row(table, "rho = |Psi|^2 exactly").Classification);
        Assert.Equal("UNCHANGED", Row(table, "the state totals the cell count").Classification);
        Assert.Equal("UNCHANGED", Row(table, "the flow conserves the norm exactly").Classification);
        Assert.Equal("UNCHANGED", Row(table, "the mode occupation is conserved").Classification);
        Assert.Equal("UNCHANGED", Row(table, "the long-wavelength power law is 2").Classification);

        // and the measurements behind them
        foreach (int m in SpectralNecessityAudit.Masks())
        {
            Assert.True(SpectralNecessityAudit.ModulusError(m) < 1e-12);
            Assert.Equal(96.0, SpectralNecessityAudit.StateTotal(m), 6);
            Assert.True(SpectralNecessityAudit.NormDeviation(m) < 1e-12);
            Assert.True(SpectralNecessityAudit.ModeLeakage(m) < 1e-12);
            Assert.True(Math.Abs(SpectralNecessityAudit.PowerLawOf(m) - 2.0) < 1e-6);
        }

        // the power law is NOT bit-identical, and the audit says so rather than rounding it away
        double p1 = SpectralNecessityAudit.PowerLawOf(Singleton);
        double p2 = SpectralNecessityAudit.PowerLawOf(Native);
        Assert.True(Math.Abs(p1 - p2) > 0.0, "the exponents should differ in the fit's own noise");
        Assert.True(Math.Abs(p1 - p2) < 1e-6, $"the exponents differ by {Math.Abs(p1 - p2):E3}, beyond the fit's precision");
    }

    [Fact]
    public void Y_QM_008_TheNumericalConclusionsKeepTheirFormAndChangeTheirValue()
    {
        PrintHeader("QM_008 - the numerical block: the same statement, a new value");

        var table = SpectralNecessityAudit.ConclusionTable();
        var sb = new StringBuilder();
        foreach (var r in table.Where(r => r.Classification == "BOUNDARY"))
            sb.AppendLine($"  {r.Conclusion,-52} {r.Source,-19} {r.One,-14} {r.Pair,-14} {r.Native}");
        Output.WriteLine(sb.ToString());

        Assert.Equal("BOUNDARY", Row(table, "the Schrodinger coefficient is the second moment").Classification);
        Assert.Equal("BOUNDARY", Row(table, "the quartic coefficient is sum r^4 / 12").Classification);
        Assert.Equal("BOUNDARY", Row(table, "the packet tracks Schrodinger for a window").Classification);
        Assert.Equal("BOUNDARY", Row(table, "the split is mean + amplitude + phase = 1 + 42 + 53").Classification);
        Assert.Equal("BOUNDARY", Row(table, "the observable rank is 43").Classification);

        // the numbers, measured: the coefficient is the second moment on every generator
        Assert.Equal(1.0, SpectralNecessityAudit.CoefficientOf(Singleton), 9);
        Assert.Equal(5.0, SpectralNecessityAudit.CoefficientOf(Pair), 9);
        Assert.Equal(91.0, SpectralNecessityAudit.CoefficientOf(Native), 9);
        // the quartic coefficient is asserted against its CLOSED FORM, not against QM_005's printed "0.08":
        // my first version carried that printed value as a literal at six decimals and the measurement refused it
        Assert.Equal(Enumerable.Range(1, 1).Sum(r => Math.Pow(r, 4)) / 12.0, SpectralNecessityAudit.QuarticOf(Singleton), 9);
        Assert.Equal(Enumerable.Range(1, 6).Sum(r => Math.Pow(r, 4)) / 12.0, SpectralNecessityAudit.QuarticOf(Native), 9);
        Assert.Equal(0.0833333333, SpectralNecessityAudit.QuarticOf(Singleton), 9);

        // the packet window moves by a factor of 22 while staying finite on every candidate
        double w1 = SpectralNecessityAudit.WindowOf(Singleton);
        double wn = SpectralNecessityAudit.WindowOf(Native);
        Assert.True(wn > 0.0 && w1 > 0.0);
        Assert.InRange(w1 / wn, 22.0, 22.6);
    }

    [Fact]
    public void Y_QM_008_TheFoldAndTheFingerprintDoNotSurviveTheReplacement()
    {
        PrintHeader("QM_008 - the block that does not survive: it is a statement about the native substrate");

        var table = SpectralNecessityAudit.ConclusionTable();
        var sb = new StringBuilder();
        foreach (var r in table.Where(r => r.Classification == "REFUTED"))
        {
            sb.AppendLine($"  {r.Conclusion,-52} {r.Source,-19} {r.One,-14} {r.Pair,-14} {r.Native}");
            sb.AppendLine("    REFUTED means DOES NOT SURVIVE THE REPLACEMENT, not false: on its own substrate it is true.");
        }
        Output.WriteLine(sb.ToString());

        Assert.Equal("REFUTED", Row(table, "the dispersion folds inside the band").Classification);
        Assert.Equal("REFUTED", Row(table, "the recorded fingerprint (trace 1152, 45 levels, free room 51, max 15.837372)").Classification);

        // the fold does not exist on the singleton, which is why the conclusion cannot be spectrum-independent
        Assert.Equal(0, SpectralNecessityAudit.FoldOf(Singleton));
        Assert.Equal(28, SpectralNecessityAudit.FoldOf(Pair));
        Assert.Equal(11, SpectralNecessityAudit.FoldOf(Native));

        // and the recorded fingerprint is the native one, by construction
        Assert.Equal(45, SpectralNecessityAudit.LevelCountOf(Native));
        Assert.Equal(51, SpectralNecessityAudit.FreeRoomOf(Native));
        Assert.NotEqual(SpectralNecessityAudit.LevelCountOf(Singleton), SpectralNecessityAudit.LevelCountOf(Native));
    }

    [Fact]
    public void Y_QM_008_ThePartitionIsComputedAndTheVerdictIsABoundary()
    {
        PrintHeader("QM_008 - the partition and the verdict");

        var (total, unchanged, boundary, refuted, requires) = SpectralNecessityAudit.ThePartition();
        var sb = new StringBuilder();
        sb.AppendLine($"  {total} conclusions = {unchanged} UNCHANGED + {boundary} BOUNDARY + {refuted} REFUTED");
        sb.AppendLine($"  do not mention the spectrum : {unchanged}");
        sb.AppendLine($"  require it for the VALUE    : {boundary}");
        sb.AppendLine($"  require it for EXISTENCE    : {refuted}");
        sb.AppendLine($"  require it at all           : {requires}");
        sb.AppendLine();
        sb.AppendLine(SpectralNecessityAudit.Verdict());
        Output.WriteLine(sb.ToString());

        Assert.Equal(12, total);
        Assert.Equal(5, unchanged);
        Assert.Equal(5, boundary);
        Assert.Equal(2, refuted);
        Assert.Equal(7, requires);
        Assert.Equal(total, unchanged + boundary + refuted);
        Assert.StartsWith("BOUNDARY", SpectralNecessityAudit.Verdict());

        // the table and the counts cannot disagree: the partition is read off the same table
        Assert.Equal(total, SpectralNecessityAudit.ConclusionTable().Length);
    }

    [Fact]
    public void Y_QM_008_TheNormControlCannotPassVacuously()
    {
        PrintHeader("QM_008 - the control: the exact norm conservation is a measurement, not a free pass");

        double control = SpectralNecessityAudit.NormDeviationUnderADecayControl();
        var sb = new StringBuilder();
        sb.AppendLine($"  an imaginary symbol (a decaying flow) gives {control:E3}");
        sb.AppendLine("  every candidate symbol is REAL, hence Hermitian, hence unitary - which is why the five structural");
        sb.AppendLine("  conclusions hold on all three and the test above cannot pass by accident.");
        Output.WriteLine(sb.ToString());

        Assert.True(control > 1e-3, $"the control must fail visibly, got {control:E3}");
        Assert.All(SpectralNecessityAudit.Masks(), m => Assert.True(SpectralNecessityAudit.NormDeviation(m) < 1e-12));
    }

    private static (string Conclusion, string Source, string One, string Pair, string Native, string Classification)
        Row((string Conclusion, string Source, string One, string Pair, string Native, string Classification)[] table,
            string conclusion)
        => table.Single(r => r.Conclusion == conclusion);

    [Fact]
    public void Y_QM_008_Diag()
    {
        PrintHeader("QM_008 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(SpectralNecessityAudit.OutputStates());
        Output.WriteLine(SpectralNecessityAudit.OutputSplit());
        Output.WriteLine(SpectralNecessityAudit.OutputConclusions());
        Output.WriteLine(SpectralNecessityAudit.OutputVerdict());
    }
}
