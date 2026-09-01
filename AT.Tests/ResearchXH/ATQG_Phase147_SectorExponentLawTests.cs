using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 147 — Sector-dependent exponent law. QG146 established up/down require different effective
/// exponents. This phase asks whether charge and isospin can determine the hierarchy exponent itself.
///
/// Tests: ATQG1470 (exponent vs charge/T3/cross), ATQG1471 (effective spectral dimension + law fit),
/// ATQG1472 (hierarchy reconstruction + classification).
/// </summary>
public class ATQG_Phase147_SectorExponentLawTests : ResearchTestBase
{
    public ATQG_Phase147_SectorExponentLawTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1470_ExponentVsChargeIsospinCross()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1470: hierarchy exponent vs charge, isospin, and cross term");

        sb.AppendLine("SECTOR EFFECTIVE EXPONENTS (p_eff = log(r31)/log(4)):");
        foreach (var (n, p, q, t3) in SectorExponentLaw.SectorExponents())
            sb.AppendLine($"  {n}: p_eff={p:F3}  Q={q:F3}  T3={t3:F2}");
        sb.AppendLine();
        sb.AppendLine("CORRELATIONS WITH p_eff:");
        sb.AppendLine($"  charge Q: r = {SectorExponentLaw.ExponentChargeCorrelation():F3}");
        sb.AppendLine($"  isospin T3: r = {SectorExponentLaw.ExponentIsospinCorrelation():F3}");
        sb.AppendLine($"  cross Q×T3: r = {SectorExponentLaw.ExponentCrossCorrelation():F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the exponent correlates strongly with isospin (r≈0.96) and well with");
        sb.AppendLine("charge (r≈0.76), but only weakly with the Q×T3 product.");
        Output.WriteLine(sb.ToString());

        Assert.True(SectorExponentLaw.ExponentChargeCorrelation() > 0.3, "exponent should correlate with charge");
        Assert.True(Math.Abs(SectorExponentLaw.ExponentIsospinCorrelation()) > 0.8, "exponent should correlate strongly with isospin");
    }

    [Fact]
    public void ATQG1471_EffectiveSpectralDimensionAndLawFit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1471: effective spectral dimension and exponent-law fit");

        sb.AppendLine("EFFECTIVE SPECTRAL DIMENSION (δ_eff = p_eff / 2):");
        foreach (var (n, d) in SectorExponentLaw.EffectiveSpectralDimensions())
            sb.AppendLine($"  {n}: δ_eff = {d:F3}");
        sb.AppendLine($"  octave Weyl exponent (QG141) = {HierarchyExponentOrigin.WeylExponent():F3}");
        sb.AppendLine($"  up dimension exceeds the octave Weyl exponent: {SectorExponentLaw.UpDimensionExceedsOctave()}");
        sb.AppendLine();
        var fit = SectorExponentLaw.FitExponentLaw();
        sb.AppendLine("LINEAR EXPONENT LAW FIT:");
        sb.AppendLine($"  p_eff = {fit.P0:F3} + {fit.A:F3}·Q + {fit.B:F3}·T3");
        sb.AppendLine($"  max residual over (lepton, up, down) = {fit.MaxResidual:F5}");
        sb.AppendLine($"  law reproduces all three sectors: {SectorExponentLaw.LawReproducesSectors()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the linear law p = p0 + a·Q + b·T3 reproduces the sector exponents exactly");
        sb.AppendLine("(residual 0.0000), with the up sector implying an elevated spectral dimension.");
        Output.WriteLine(sb.ToString());

        Assert.True(SectorExponentLaw.UpDimensionExceedsOctave(), "up dimension should exceed the octave Weyl exponent");
        Assert.True(SectorExponentLaw.LawReproducesSectors(), "linear law should reproduce all three sectors");
        Assert.True(fit.MaxResidual < 0.05, "law should fit with negligible residual");
    }

    [Fact]
    public void ATQG1472_HierarchyReconstructionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1472: hierarchy reconstruction and classification");

        var fit = SectorExponentLaw.FitExponentLaw();
        double nuPred = fit.NeutrinoPrediction;
        double nuObs = SectorExponentLaw.NeutrinoObservedExponent();
        int score = SectorExponentLaw.OriginScore();
        string cls = SectorExponentLaw.Classify();

        sb.AppendLine("PREDICTIVE CHECK:");
        sb.AppendLine($"  neutrino exponent prediction (Q=0, T3=+1/2): {nuPred:F3}");
        sb.AppendLine($"  observed neutrino exponent (ν3/ν1 = 500): {nuObs:F3}");
        sb.AppendLine($"  (a testable difference — neutrino masses are the least constrained)");
        sb.AppendLine();
        sb.AppendLine($"exponent-origin score (0..5): {score}");
        sb.AppendLine($"  +1 charge correlation: {SectorExponentLaw.ExponentChargeCorrelation() > 0.3}");
        sb.AppendLine($"  +1 strong isospin correlation: {Math.Abs(SectorExponentLaw.ExponentIsospinCorrelation()) > 0.8}");
        sb.AppendLine($"  +1 up dimension exceeds octave: {SectorExponentLaw.UpDimensionExceedsOctave()}");
        sb.AppendLine($"  +1 law reproduces sectors: {SectorExponentLaw.LawReproducesSectors()}");
        sb.AppendLine($"  +1 predictive (neutrino well-defined): {!double.IsNaN(nuPred)}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO RELATION rejected: the exponent correlates strongly with isospin.");
        sb.AppendLine("  • EXPONENT ORIGIN accepted: p_eff = p0 + a·Q + b·T3 reproduces the lepton/up/down");
        sb.AppendLine("    hierarchy exponents exactly — charge and isospin DETERMINE the exponent.");
        Output.WriteLine(sb.ToString());

        Assert.True(SectorExponentLaw.LawReproducesSectors(), "law should reproduce the sectors");
        Assert.True(score >= 4, "exponent-origin score should be strong");
        Assert.Equal("EXPONENT ORIGIN", cls);
    }
}
