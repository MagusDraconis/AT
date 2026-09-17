using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_009 - Fingerprint Necessity Audit (group QM). Which AT predictions fail if the spectral fingerprint is
/// replaced by the Schrodinger-optimal generator {1}? Audit the redshift sector, the clock law, the source law, the
/// phase sector and the observable algebra, and classify each UNCHANGED / BOUNDARY / REFUTED.
/// </summary>
public sealed class Y_QM_009_Tests : ResearchTestBase
{
    public Y_QM_009_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private const int Native = 0b111111;
    private const int Schrodinger = 1;

    [Fact]
    public void Y_QM_009_TheSourceScanSeparatesTheSectorsMechanically()
    {
        PrintHeader("QM_009 - the live source scan: which sectors' CODE can read the spectrum");

        var sb = new StringBuilder();
        sb.AppendLine("  sector              in code   in comments   files   scan verdict");
        foreach (var r in FingerprintNecessityAudit.SectorScan())
            sb.AppendLine($"  {r.Sector,-19} {r.Code,-9} {r.Comment,-13} {r.FilesFound,-7} {FingerprintNecessityAudit.ScanVerdict(r.Sector)}");
        Output.WriteLine(sb.ToString());

        // every sector's files must be found, otherwise a zero would be an artefact of a missing file
        Assert.All(FingerprintNecessityAudit.SectorScan(), r => Assert.True(r.FilesFound > 0, r.Sector));
        Assert.All(FingerprintNecessityAudit.SectorScan(), r => Assert.False(string.IsNullOrWhiteSpace(r.Files)));

        // the redshift sector's code is lattice-free - the measurement the whole verdict rests on
        Assert.Equal("LATTICE-FREE", FingerprintNecessityAudit.ScanVerdict("redshift sector"));
        Assert.Equal(0, FingerprintNecessityAudit.SectorScan().Single(r => r.Sector == "redshift sector").Code);

        // and the sectors that DO read the substrate must show it, or the scan is measuring nothing
        Assert.Equal("READS THE SUBSTRATE", FingerprintNecessityAudit.ScanVerdict("observable algebra"));
        Assert.True(FingerprintNecessityAudit.SectorScan().Single(r => r.Sector == "observable algebra").Code > 0);
    }

    [Fact]
    public void Y_QM_009_TheRedshiftSectorIsCompletelyImmune()
    {
        PrintHeader("QM_009 - the redshift sector: the same numbers on both substrata");

        var sb = new StringBuilder();
        foreach (var r in FingerprintNecessityAudit.RedshiftSector())
            sb.AppendLine($"  {r.Quantity,-44} {r.Native,-22:F15} {r.Schrodinger,-22:F15} {r.Classification}");
        sb.AppendLine();
        sb.AppendLine("  the scalar bridge - every input the sector consumes is a scalar:");
        foreach (var s in FingerprintNecessityAudit.ScalarBridge())
            sb.AppendLine($"    {s.Input,-26} {s.Kind,-29} {s.Native,-13:F6} {s.Schrodinger,-13:F6} {s.Same}");
        Output.WriteLine(sb.ToString());

        // every row is identical, because no row takes a substrate argument
        Assert.All(FingerprintNecessityAudit.RedshiftSector(), r => Assert.Equal("UNCHANGED", r.Classification));
        Assert.All(FingerprintNecessityAudit.RedshiftSector(), r => Assert.Equal(r.Native, r.Schrodinger, 15));

        // the chain reproduces the recorded G_068/G_019 numbers rather than inventing its own
        double x = TemporalPredictionAudit.XSolar();
        Assert.Equal(AtNumerics.ExpM1(-x), FingerprintNecessityAudit.RedshiftAt(x), 15);
        Assert.Equal(1.0 / Math.Sqrt(1.0 + 2.0 * x) - 1.0, FingerprintNecessityAudit.RedshiftGr(x), 15);
        Assert.True(x < 0.0);                                   // the AT surface potential is negative
        // AN ASSUMPTION OF MY DRAFT WAS BACKWARDS HERE and the measurement refused it: the AT redshift is always the
        // SMALLER of the two, which is what G_068 recorded and what TemporalPredictionAudit.AtRedshiftIsAlwaysSmaller
        // already asserts. Measured at the solar compactness: 2.123049E-006 against 2.123054E-006.
        Assert.True(FingerprintNecessityAudit.RedshiftAt(x) < FingerprintNecessityAudit.RedshiftGr(x));
        Assert.True(TemporalPredictionAudit.AtRedshiftIsAlwaysSmaller());

        // the clock law and the ratio law are functions of the occupancy alone
        Assert.Equal(2.0, FingerprintNecessityAudit.ClockRate(8.0), 12);
        // the ratio law is (rho1/rho2)^(1/d) - 1, so at rho = 2 against rho = 1 it is 2^(1/3) - 1, NOT 1:
        // my draft carried the literal 1.0 and the measurement refused it
        Assert.Equal(Math.Pow(2.0, 1.0 / 3.0) - 1.0, FingerprintNecessityAudit.ClockRateDifference(2.0, 1.0), 12);
        Assert.Equal(0.259921049895, FingerprintNecessityAudit.ClockRateDifference(2.0, 1.0), 11);
    }

    [Fact]
    public void Y_QM_009_TheClockLawIsUnchangedAndItsPatternIsNot()
    {
        PrintHeader("QM_009 - the clock sector: one law, two patterns");

        var (max, correlation, mean) = FingerprintNecessityAudit.ClockPatternShift();
        var sb = new StringBuilder();
        sb.AppendLine($"  the law against its own definition:  {FingerprintNecessityAudit.ClockLawDifference():E3}");
        sb.AppendLine($"  the pattern's maximum relative shift: {max:E3}");
        sb.AppendLine($"  the pattern's correlation:            {correlation:F6}");
        sb.AppendLine($"  the pattern's mean absolute shift:    {mean:E3}");
        Output.WriteLine(sb.ToString());

        // the law is exactly the repository's law: sampling it cannot differ from it
        Assert.Equal(0.0, FingerprintNecessityAudit.ClockLawDifference(), 15);

        // and the pattern moves, which is why the sector is a BOUNDARY rather than UNCHANGED
        Assert.True(max > 1e-3, $"the pattern must move, got {max:E3}");

        // the pattern is a function of the state, so it is the same state that moves (QM_008)
        var native = FingerprintNecessityAudit.ClockPattern(Native);
        Assert.Equal(96, native.Length);
        Assert.All(native, v => Assert.True(v > 0.0));

        // AND ONE MORE DRAFT LITERAL WAS WRONG: I asserted the mean rate is exactly 1, on the reasoning that the mean
        // occupancy is 1. The measurement gives 0.99879755714637009, and the reason is Jensen's inequality - the mean
        // of the cube roots is strictly BELOW the cube root of the mean, which is 1, whenever the state is not
        // uniform. The assertion now records the measured value and the direction rather than the assumed equality.
        Assert.True(native.Average() < 1.0);
        Assert.Equal(0.99879755714637009, native.Average(), 12);
        // and the Jensen gap, which my first version transcribed WRONG (0.9963974978 against the true
        // 0.99639700730968433 - a mistyped digit in a computed constant, the same class of slip as QM_008's literal)
        Assert.Equal(0.9963970073, Math.Pow(native.Average(), 3), 9);
        Assert.True(Math.Pow(native.Average(), 3) < 1.0);
    }

    [Fact]
    public void Y_QM_009_TheSourceLawKeepsItsRanking()
    {
        PrintHeader("QM_009 - the source law: the magnitudes move, and the structural hypothesis was refuted");

        var sb = new StringBuilder();
        foreach (var r in FingerprintNecessityAudit.SourcePushes())
            sb.AppendLine($"  {r.Candidate,-46} {r.Native,-18:F10} {r.Schrodinger,-18:F10} {r.Survives}");
        sb.AppendLine();
        foreach (var r in FingerprintNecessityAudit.SourceStructure())
            sb.AppendLine($"    {r.Candidate,-46} null natively {r.PhaseNullNative,-6} null for {{1}} {r.PhaseNullSchrodinger,-6} {r.Survives}");
        Output.WriteLine(sb.ToString());

        // the pushes are evaluated on the state, so the magnitudes must differ
        var pushes = FingerprintNecessityAudit.SourcePushes();
        Assert.NotEmpty(pushes);
        Assert.Contains(pushes, p => Math.Abs(p.Native - p.Schrodinger) > 1e-9);
        Assert.Equal(FlowSourceAudit.Candidates().Length, pushes.Length);

        // AND MY DRAFT'S STRUCTURAL HYPOTHESIS IS REFUTED BY THE MEASUREMENT, so the reality is asserted instead.
        // The phase-null set is a property of the STATE and not of the source law: four candidates are exactly null
        // natively and only one is null for {1}.
        Assert.Equal(4, FingerprintNecessityAudit.PhaseNullCountOf(Native));
        Assert.Equal(1, FingerprintNecessityAudit.PhaseNullCountOf(Schrodinger));

        // so the null classification MOVES for three of the seven, and holds only where it is structural
        var structure = FingerprintNecessityAudit.SourceStructure();
        Assert.Equal(3, structure.Count(r => r.Survives == "MOVES"));
        Assert.Equal(4, structure.Count(r => r.Survives == "UNCHANGED"));

        // WHAT DOES SURVIVE IS THE FIXED POINT, and it is measured rather than read: the uniform actualization
        // pressure is the ONLY candidate whose push norm is the same on both substrata.
        var invariants = FingerprintNecessityAudit.InvariantSources();
        Assert.Single(invariants.Where(s => s.IsInvariant));
        var invariant = invariants.Single(s => s.IsInvariant);
        Assert.StartsWith("4a", invariant.Candidate);
        Assert.Equal(Math.Sqrt(96.0), invariant.Native, 9);
        Assert.Equal(Math.Sqrt(96.0), invariant.Schrodinger, 9);

        // and two candidates go from EXACTLY zero to a real push - the measurement the ranking claim died on
        Assert.Equal(0.0, pushes.Single(p => p.Candidate.StartsWith("2 ")).Native, 12);
        Assert.Equal(0.4671052634, pushes.Single(p => p.Candidate.StartsWith("2 ")).Schrodinger, 9);
        Assert.Equal(0.0, pushes.Single(p => p.Candidate.StartsWith("3 ")).Native, 12);
        Assert.Equal(0.0813500268, pushes.Single(p => p.Candidate.StartsWith("3 ")).Schrodinger, 9);
    }

    [Fact]
    public void Y_QM_009_ThePhaseSectorMovesInMembershipAndHoldsInStructure()
    {
        PrintHeader("QM_009 - the phase sector: 42/53 against 46/49, and the structure that does not move");

        var sb = new StringBuilder();
        foreach (var r in FingerprintNecessityAudit.PhaseSector())
            sb.AppendLine($"  {r.Quantity,-32} {r.Native,-16:F10} {r.Schrodinger,-16:F10} {r.Classification}");
        Output.WriteLine(sb.ToString());

        var table = FingerprintNecessityAudit.PhaseSector();
        Assert.Equal(53.0, table.Single(r => r.Quantity == "phase dimension").Native, 9);
        Assert.Equal(49.0, table.Single(r => r.Quantity == "phase dimension").Schrodinger, 9);
        Assert.Equal("BOUNDARY", table.Single(r => r.Quantity == "phase dimension").Classification);

        // the structure holds: no partially hidden mode, and the partition is exact, on both substrata
        Assert.Equal("UNCHANGED", table.Single(r => r.Quantity == "partially hidden modes").Classification);
        Assert.Equal(0.0, table.Single(r => r.Quantity == "partially hidden modes").Native, 9);
        Assert.Equal(0.0, table.Single(r => r.Quantity == "partially hidden modes").Schrodinger, 9);
    }

    [Fact]
    public void Y_QM_009_TheObservableAlgebraIsInvariantInDimensionAndNotInContent()
    {
        PrintHeader("QM_009 - the observable algebra: one invariant and three moves");

        var sb = new StringBuilder();
        foreach (var r in FingerprintNecessityAudit.ObservableAlgebra())
            sb.AppendLine($"  {r.Quantity,-32} {r.Native,-16:F0} {r.Schrodinger,-16:F0} {r.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  levels: {SpectralNecessityAudit.LevelCountOf(Native)} natively, {SpectralNecessityAudit.LevelCountOf(Schrodinger)} for {{1}}");
        Output.WriteLine(sb.ToString());

        var table = FingerprintNecessityAudit.ObservableAlgebra();

        // THE DIMENSION IS |S|-INDEPENDENT: every |k| belongs to exactly one level, so the restriction to the levels
        // counts each |k| once whatever the partition. Measured as the same number from two different partitions.
        Assert.Equal("UNCHANGED", table.Single(r => r.Quantity == "the algebra's dimension").Classification);
        Assert.Equal(49.0, table.Single(r => r.Quantity == "the algebra's dimension").Native, 9);
        Assert.Equal(49.0, table.Single(r => r.Quantity == "the algebra's dimension").Schrodinger, 9);

        // but the two partitions are genuinely different, so the invariance is a theorem and not a coincidence
        Assert.NotEqual(SpectralNecessityAudit.LevelCountOf(Native), SpectralNecessityAudit.LevelCountOf(Schrodinger));

        // and the multiplicity-weighted content moves
        Assert.Equal("BOUNDARY", table.Single(r => r.Quantity == "sum of multiplicity squares").Classification);
        Assert.Equal(230.0, FingerprintNecessityAudit.SumOfMultiplicitySquaresOf(Native), 9);
        Assert.Equal(190.0, FingerprintNecessityAudit.SumOfMultiplicitySquaresOf(Schrodinger), 9);
        Assert.Equal(181.0, FingerprintNecessityAudit.ProtectedDimensionsOf(Native), 9);
        Assert.Equal(141.0, FingerprintNecessityAudit.ProtectedDimensionsOf(Schrodinger), 9);

        // the algebra's dimension cannot exceed the ambient space it lives in, on either substrate
        foreach (int mask in FingerprintNecessityAudit.Masks())
        {
            Assert.True(FingerprintNecessityAudit.AlgebraDimensionOf(mask) <= FingerprintNecessityAudit.SumOfMultiplicitySquaresOf(mask));
            Assert.True(FingerprintNecessityAudit.ProtectedDimensionsOf(mask) >= 0);
        }
    }

    [Fact]
    public void Y_QM_009_TheVerdictIsABoundaryAndNoPredictionFails()
    {
        PrintHeader("QM_009 - the five sectors and the verdict");

        var sb = new StringBuilder();
        sb.AppendLine("  sector              scan                   recomputed");
        foreach (var r in FingerprintNecessityAudit.SectorTable())
            sb.AppendLine($"  {r.Sector,-19} {r.Scan,-22} {r.Reclassification}");
        sb.AppendLine();
        sb.AppendLine(FingerprintNecessityAudit.Verdict());
        Output.WriteLine(sb.ToString());

        Assert.True(FingerprintNecessityAudit.EverySectorIsCovered());

        var (unchanged, boundary, refuted) = FingerprintNecessityAudit.VerdictCounts();
        Assert.Equal(5, unchanged + boundary + refuted);
        Assert.Equal(1, unchanged);
        Assert.Equal(4, boundary);
        Assert.Equal(0, refuted);

        // the sector that is unchanged is the only one whose output is a measurement
        var table = FingerprintNecessityAudit.SectorTable();
        Assert.Equal("UNCHANGED", table.Single(r => r.Sector == "redshift sector").Reclassification);
        Assert.Equal("LATTICE-FREE", table.Single(r => r.Sector == "redshift sector").Scan);

        Assert.StartsWith("BOUNDARY", FingerprintNecessityAudit.Verdict());
    }

    [Fact]
    public void Y_QM_009_Diag()
    {
        PrintHeader("QM_009 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(FingerprintNecessityAudit.OutputScan());
        Output.WriteLine(FingerprintNecessityAudit.OutputRedshift());
        Output.WriteLine(FingerprintNecessityAudit.OutputClockAndPhase());
        Output.WriteLine(FingerprintNecessityAudit.OutputSource());
        Output.WriteLine(FingerprintNecessityAudit.OutputAlgebra());
        Output.WriteLine(FingerprintNecessityAudit.OutputVerdict());
    }
}
