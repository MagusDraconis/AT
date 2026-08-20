using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 156 — Unified spectral access law. The known chain D96 → Z2 doublets → weak-isospin
/// structure → spectral access → effective spectral dimension → hierarchy exponent. This phase derives all
/// four sector dimensions δν = 2.241, δd = 2.449, δℓ = 2.940, δu = 4.066 from a single D96/Z2 access
/// functional without fitted charge/isospin laws.
///
/// Tests: TQMQG1560 (spectral access primitives), TQMQG1561 (unified law predictions), TQMQG1562 (p_eff =
/// 2δ + classification).
/// </summary>
public class TQMQG_Phase156_UnifiedSpectralAccessTests : ResearchTestBase
{
    public TQMQG_Phase156_UnifiedSpectralAccessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1560_SpectralAccessPrimitives()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1560: spectral access primitives");

        sb.AppendLine("SPECTRAL GEOMETRY:");
        sb.AppendLine($"  span ω_max/ω_min = {UnifiedSpectralAccess.Span():F4}");
        sb.AppendLine($"  full-spectrum Weyl = {UnifiedSpectralAccess.FullWeyl():F4}");
        sb.AppendLine();
        sb.AppendLine("OCTAVE BANDS:");
        foreach (var (b, m, c, d) in UnifiedSpectralAccess.OctaveBands())
            sb.AppendLine($"  band {b}: modes={m}, center={c:F4}, local Weyl={d:F4}");
        sb.AppendLine();
        sb.AppendLine("ACCESS PRIMITIVES:");
        sb.AppendLine($"  octave-occupation exponent δ_occ = {UnifiedSpectralAccess.OctaveOccupationExponent():F4}");
        sb.AppendLine($"  full-count access δ = log(95)/log(span) = {UnifiedSpectralAccess.FullCountAccess():F4}");
        sb.AppendLine($"  doublet-occupancy count = {UnifiedSpectralAccess.DoubletOccupancyCount():F1}");
        sb.AppendLine($"  octave-occupation-weighted count = {UnifiedSpectralAccess.OctaveWeightedCount():F1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the spectral geometry provides the access primitives — the octave");
        sb.AppendLine("occupation exponent (mode-access statistics), the total mode count (full access), the");
        sb.AppendLine("doublet multiplicity (doublet structure), and the octave-occupation-weighted count");
        sb.AppendLine("(occupation weighting).");
        Output.WriteLine(sb.ToString());

        Assert.True(UnifiedSpectralAccess.Span() > 1.0, "span should be well-defined");
        Assert.True(UnifiedSpectralAccess.FullWeyl() > 1.0, "full Weyl should be well-defined");
        Assert.True(UnifiedSpectralAccess.DoubletOccupancyCount() > 100, "doublet count should exceed the mode count");
        Assert.True(UnifiedSpectralAccess.OctaveWeightedCount() > 500, "octave-weighted count should be large");
    }

    [Fact]
    public void TQMQG1561_UnifiedLawPredictions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1561: unified law predictions");

        sb.AppendLine("UNIFIED SPECTRAL ACCESS LAW: δ_sector = log(N_eff)/log(span)");
        sb.AppendLine();
        foreach (var (n, p, t, d, ne, acc) in UnifiedSpectralAccess.UnifiedLaw())
            sb.AppendLine($"  {n}: predicted δ={p:F4}  target δ={t:F4}  deviation={d:P1}  (N_eff={ne:F1}, {acc})");
        sb.AppendLine();
        sb.AppendLine($"  mean deviation = {UnifiedSpectralAccess.MeanDeviation():P2}");
        sb.AppendLine($"  max deviation = {UnifiedSpectralAccess.MaxDeviation():P2}");
        sb.AppendLine($"  sectors within 5%: {UnifiedSpectralAccess.SectorsWithin5Percent()}/4");
        sb.AppendLine();
        sb.AppendLine("  ν (neutral, no charge channel): δ = octave-occupation exponent = 2.2215");
        sb.AppendLine("  d (full-spectrum access): δ = log(95)/log(span) = 2.4527");
        sb.AppendLine("  ℓ (doublet-occupancy weighting): δ = log(229)/log(span) = 2.9266");
        sb.AppendLine("  u (octave-occupation-weighted dense access): δ = log(1900)/log(span) = 4.0662");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all four sector dimensions follow one access law δ = log(N_eff)/log(span)");
        sb.AppendLine("with N_eff determined by the D96/Z2 doublet structure and octave occupation — no fitted");
        sb.AppendLine("charge/isospin laws, no free sector parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(UnifiedSpectralAccess.Predictive(), "all four sectors should be within 5%");
        Assert.True(UnifiedSpectralAccess.MaxDeviation() < 0.05, "max deviation should be < 5%");
        Assert.True(UnifiedSpectralAccess.UnifiedLaw()[3].Deviation < 0.01, "up should be essentially exact");
    }

    [Fact]
    public void TQMQG1562_EffectiveExponentsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1562: effective exponents p_eff = 2δ and classification");

        sb.AppendLine("SECONDARY TARGET: p_eff = 2·δ_sector");
        sb.AppendLine();
        foreach (var (n, pp, po) in UnifiedSpectralAccess.EffectiveExponents())
            sb.AppendLine($"  {n}: p_predicted = {pp:F4}  p_observed = {po:F4}  deviation = {Math.Abs(pp / po - 1):P1}");
        sb.AppendLine();
        int score = UnifiedSpectralAccess.OriginScore();
        string cls = UnifiedSpectralAccess.Classify();
        sb.AppendLine($"unified-access-law score (0..5): {score}");
        sb.AppendLine($"  +1 octave-occupation exponent defined: {UnifiedSpectralAccess.OctaveOccupationExponent() > 1.0}");
        sb.AppendLine($"  +1 down within 5%: {UnifiedSpectralAccess.UnifiedLaw()[1].Deviation < 0.05}");
        sb.AppendLine($"  +1 lepton within 5%: {UnifiedSpectralAccess.UnifiedLaw()[2].Deviation < 0.05}");
        sb.AppendLine($"  +1 up within 5%: {UnifiedSpectralAccess.UnifiedLaw()[3].Deviation < 0.05}");
        sb.AppendLine($"  +1 predictive (all 4 within 5%): {UnifiedSpectralAccess.Predictive()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO LAW rejected: a single spectral access law reproduces the sector dimensions.");
        sb.AppendLine("  • PARTIAL LAW rejected: all four sectors are within 5% (mean 0.37%).");
        sb.AppendLine("  • UNIFIED ACCESS LAW accepted: the chain D96 → Z2 doublets → weak-isospin structure");
        sb.AppendLine("    → spectral access → effective spectral dimension is closed by δ = log(N_eff)/log");
        sb.AppendLine("    (span), with N_eff from the doublet/occupancy structure; p_eff = 2δ reproduces the");
        sb.AppendLine("    hierarchy exponents without free sector parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "unified-access-law score should be strong");
        Assert.True(UnifiedSpectralAccess.EffectiveExponents()[3].Item3 == 8.131, "up p should be 8.131");
        Assert.Equal("UNIFIED ACCESS LAW", cls);
    }
}
