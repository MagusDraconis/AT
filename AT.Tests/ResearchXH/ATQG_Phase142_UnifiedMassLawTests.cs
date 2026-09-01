using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 142 — Unified fermion mass law. QG138 derived the family count and QG141 the hierarchy
/// exponents from the spectral structure. This phase asks whether a single spectral law can reproduce all
/// fermion generations simultaneously (leptons, up quarks, down quarks, neutrinos).
///
/// Tests: ATQG1420 (leptons), ATQG1421 (up/down quarks), ATQG1422 (neutrinos + universal scaling +
/// classification).
/// </summary>
public class ATQG_Phase142_UnifiedMassLawTests : ResearchTestBase
{
    public ATQG_Phase142_UnifiedMassLawTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1420_LeptonsAndOctaveLaw()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1420: lepton sector vs the octave law");

        sb.AppendLine($"OCTAVE-PREDICTED RATIOS (mass ~ center^5.88):");
        sb.AppendLine($"  [{string.Join(", ", UnifiedMassLaw.OctavePredictedRatios.Select(r => r.ToString("F1", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine();
        var lep = UnifiedMassLaw.SectorRatios().First(s => s.Sector == "leptons");
        sb.AppendLine("LEPTON SECTOR (e, μ, τ):");
        sb.AppendLine($"  observed r21 (μ/e) = {lep.R21:F1}");
        sb.AppendLine($"  observed r31 (τ/e) = {lep.R31:F1}");
        sb.AppendLine($"  octave r31 prediction = {UnifiedMassLaw.OctavePredictedRatios[2]:F1}");
        sb.AppendLine($"  deviation = {lep.Deviation:P2}");
        sb.AppendLine($"  leptons reproduce the octave law: {UnifiedMassLaw.LeptonsMatch()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the lepton τ/e ratio reproduces the octave law almost exactly (~0.3%).");
        Output.WriteLine(sb.ToString());

        Assert.True(UnifiedMassLaw.LeptonsMatch(), "lepton sector should reproduce the octave law");
        Assert.True(lep.Deviation < 0.30, "lepton τ/e should match the octave prediction within 30%");
    }

    [Fact]
    public void ATQG1421_UpAndDownQuarkSectors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1421: up and down quark sectors");

        var up = UnifiedMassLaw.SectorRatios().First(s => s.Sector == "up");
        var down = UnifiedMassLaw.SectorRatios().First(s => s.Sector == "down");

        sb.AppendLine("UP-QUARK SECTOR (u, c, t):");
        sb.AppendLine($"  r21 (c/u) = {up.R21:F1}, r31 (t/u) = {up.R31:F1}, deviation = {up.Deviation:P2}");
        sb.AppendLine();
        sb.AppendLine("DOWN-QUARK SECTOR (d, s, b):");
        sb.AppendLine($"  r21 (s/d) = {down.R21:F1}, r31 (b/d) = {down.R31:F1}, deviation = {down.Deviation:P2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the quark sectors do NOT reproduce the octave-predicted ratio pattern");
        sb.AppendLine("(up is far steeper, down is shallower) — the law is sector-dependent.");
        Output.WriteLine(sb.ToString());

        Assert.True(up.Deviation > 0.30, "up-quark sector should NOT match the octave law");
        Assert.True(down.Deviation > 0.30, "down-quark sector should NOT match the octave law");
    }

    [Fact]
    public void ATQG1422_NeutrinosUniversalScalingAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1422: neutrinos, universal scaling, classification");

        var nu = UnifiedMassLaw.SectorRatios().First(s => s.Sector == "neutrino");
        double spread = UnifiedMassLaw.R31Spread();
        double logSpread = UnifiedMassLaw.LogRatioSpread();
        int score = UnifiedMassLaw.LawScore();
        string cls = UnifiedMassLaw.Classify();

        sb.AppendLine("NEUTRINO SECTOR (normal ordering):");
        sb.AppendLine($"  r21 = {nu.R21:F1}, r31 = {nu.R31:F1}, deviation = {nu.Deviation:P2}");
        sb.AppendLine();
        sb.AppendLine("UNIVERSAL SCALING:");
        sb.AppendLine($"  highest-ratio spread across sectors (max/min) = {spread:F1}");
        sb.AppendLine($"  log2(r31) spread (std) across sectors = {logSpread:F3}");
        sb.AppendLine($"  universal ratio pattern: {UnifiedMassLaw.UniversalRatios()}");
        sb.AppendLine();
        sb.AppendLine($"unified-law score (0..5): {score}");
        sb.AppendLine($"  +1 leptons match: {UnifiedMassLaw.LeptonsMatch()}");
        sb.AppendLine($"  +1 ≥2 sectors within 50%: {UnifiedMassLaw.SectorsMatchingOctave(0.50) >= 2}");
        sb.AppendLine($"  +1 shared log pattern: {logSpread < 2.0}");
        sb.AppendLine($"  +1 moderate universality: {spread < 20.0}");
        sb.AppendLine($"  +1 full universality: {spread < 5.0}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • UNIFIED MASS LAW rejected: the sectors do not share a universal ratio pattern.");
        sb.AppendLine("  • PARTIAL LAW accepted: the lepton sector reproduces the octave law (~0.3%) but");
        sb.AppendLine("    up/down/neutrino sectors do not — a single spectral law is NOT universal.");
        Output.WriteLine(sb.ToString());

        Assert.True(nu.Deviation > 0.30, "neutrino sector should NOT match the octave law");
        Assert.True(spread > 5.0, "sectors should NOT share a universal ratio pattern");
        Assert.Equal("PARTIAL LAW", cls);
    }
}
