using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.AmplitudePhaseAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_052 - Amplitude Phase Audit (group G - Gravity Source).
///
/// QUESTION. Can rho be decomposed UNIQUELY into an amplitude sector (42) and a phase sector (53)? Given G_050 (the
/// kernel is the phase sector) and G_051 (it is physical). Measure orthogonality, invertibility, reconstruction
/// accuracy and the clock / acceleration / field responses; ask whether every observable splits into an amplitude
/// plus a phase contribution.
///
/// ANSWER: **DERIVED - the split is unique, orthogonal and exact, and the interface is an identity. An observable
/// splits exactly only when it is linear; AT's own readings carry a measured SECOND-ORDER cross term instead.**
/// </summary>
public class Y_G_052_Tests : ResearchTestBase
{
    public Y_G_052_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_052_TheTwoSectorsAreOrthogonalComplements()
    {
        Assert.Equal(42, AmplitudeDimension());
        Assert.Equal(53, PhaseDimension());
        Assert.Equal(95, StateDimension());
        Assert.Equal(StateDimension(), AmplitudeDimension() + PhaseDimension());

        Assert.True(TheSectorsAreOrthogonal(), $"overlap {SectorOverlap():E3}");
        Assert.True(SectorOverlap() < 1e-12);
    }

    [Fact]
    public void Y_G_052_TheReconstructionIsExact()
    {
        Assert.True(TheReconstructionIsExact(), $"residual {ReconstructionAccuracy():E3}");
        Assert.True(ReconstructionAccuracy() < 1e-14);
    }

    [Fact]
    public void Y_G_052_TheSplitIsUniqueAndBasisIndependent()
    {
        // rebuilding the amplitude subspace from a different spanning set must give the same projection
        Assert.True(TheSplitIsBasisIndependent(), $"basis residual {BasisIndependenceResidual():E3}");
        Assert.True(BasisIndependenceResidual() < 1e-10);
    }

    [Fact]
    public void Y_G_052_TheInterfaceIsAnExactIdentity()
    {
        // span(constant, amplitude) = contraction row space, hence (phase) = kernel
        Assert.Equal(43, ContractionRank());
        Assert.Equal(ContractionRank(), AmplitudePlusMean());
        Assert.True(ContractionVersusPhaseOverlap() < 1e-9,
            $"contraction-phase overlap {ContractionVersusPhaseOverlap():E3}");
        Assert.True(TheInterfaceIsAnIdentity());
        Assert.Contains("kernel of the contraction observables", TheInterface());
    }

    [Fact]
    public void Y_G_052_LinearObservablesSplitExactlyAndTheOthersCarryASecondOrderCrossTerm()
    {
        // a linear functional splits exactly
        Assert.True(LinearAdditivityResidual() < 1e-15, $"linear residual {LinearAdditivityResidual():E3}");

        // AT's own readings do not, and the shortfall is second order rather than first
        Assert.True(ClockAdditivityResidual() > 0.0);
        Assert.True(TheCrossTermsAreSecondOrder(),
            $"clock scaling {CrossTermScaling(RhoAccessibilityAudit.ClockRates):F4}, " +
            $"field scaling {CrossTermScaling(RhoAccessibilityAudit.FieldStrengths):F4}");

        // each reading takes contributions from BOTH sectors
        Assert.All(ContributionTable(), r => Assert.True(r.Amplitude > 0.0 && r.Phase > 0.0,
            $"{r.Reading}: amplitude {r.Amplitude:E3}, phase {r.Phase:E3}"));
    }

    [Fact]
    public void Y_G_052_TheVerdictIsDerived()
    {
        Assert.Equal("DERIVED", Verdict());
        Assert.Contains("EXACT FOR LINEAR FUNCTIONALS", ObservableSplitVerdict());
        Assert.Equal(3, ContributionTable().Length);
    }

    [Fact]
    public void Y_G_052_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_052 - Amplitude Phase Audit: can rho be split uniquely into amplitude (42) and phase (53)?");

        sb.AppendLine("QUESTION. Can rho be decomposed UNIQUELY into an amplitude sector (42) and a phase sector (53)?");
        sb.AppendLine("GIVEN        G_050 (the kernel is the phase sector), G_051 (the phase is physical, not gauge)");
        sb.AppendLine("MEASURE      orthogonality | invertibility | reconstruction accuracy | clock, acceleration and");
        sb.AppendLine("             field responses");
        sb.AppendLine("GOAL         the exact interface between visible amplitudes and physical phases");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The amplitude sector is the span of the 42 VISIBLE Fourier modes and the phase sector the span of");
        sb.AppendLine("     the 53 HIDDEN ones, as G_050 classified them.");
        sb.AppendLine("  2. The state's deviation from its mean is what decomposes, since the mean is the simplex direction");
        sb.AppendLine("     and not a state direction.");
        sb.AppendLine("  3. Uniqueness is checked by REBUILDING each subspace from a different deterministic spanning set,");
        sb.AppendLine("     because orthogonality alone would not rule out a basis-dependent projection.");
        sb.AppendLine("  4. Additivity is measured on the readings themselves: exact for a linear functional, and a cross");
        sb.AppendLine("     term otherwise whose ORDER in the step is what decides whether it is curvature or a failure.");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputSplit());
        PrintHeader(OutputInterface());
        PrintHeader(OutputObservables());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
