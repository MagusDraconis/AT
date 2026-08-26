using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 146 — Quark mass hierarchy law. QG145 established quark amplification arises from spectral ×
/// charge-isospin interaction. This phase asks whether the full up/down quark hierarchy can be reproduced
/// from one spectral-interaction law.
///
/// Tests: ATQG1460 (up/down sectors + spectral density), ATQG1461 (charge×isospin amplification),
/// ATQG1462 (hierarchy reconstruction + classification).
/// </summary>
public class ATQG_Phase146_QuarkHierarchyLawTests : ResearchTestBase
{
    public ATQG_Phase146_QuarkHierarchyLawTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1460_UpAndDownSectorsAndSpectralDensity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1460: up/down quark sectors and spectral density");

        sb.AppendLine($"OCTAVE-PREDICTED RATIOS = [{string.Join(", ", QuarkHierarchyLaw.OctaveRatios.Select(r => r.ToString("F1", CultureInfo.InvariantCulture)))}]");
        var up = QuarkHierarchyLaw.UpSectorRatios();
        var down = QuarkHierarchyLaw.DownSectorRatios();
        sb.AppendLine();
        sb.AppendLine("UP-QUARK SECTOR (u, c, t):");
        sb.AppendLine($"  within-sector ratios: r21={up.R21:F1}, r31={up.R31:F1}");
        sb.AppendLine($"  deviation factors: r21×{QuarkHierarchyLaw.UpDeviation().R21Factor:F1}, r31×{QuarkHierarchyLaw.UpDeviation().R31Factor:F1}");
        sb.AppendLine();
        sb.AppendLine("DOWN-QUARK SECTOR (d, s, b):");
        sb.AppendLine($"  within-sector ratios: r21={down.R21:F1}, r31={down.R31:F1}");
        sb.AppendLine($"  deviation factors: r21×{QuarkHierarchyLaw.DownDeviation().R21Factor:F2}, r31×{QuarkHierarchyLaw.DownDeviation().R31Factor:F2}");
        sb.AppendLine();
        sb.AppendLine("SPECTRAL DENSITY:");
        sb.AppendLine($"  Weyl exponent = {QuarkHierarchyLaw.SpectralDensityExponent():F3}");
        sb.AppendLine($"  octave occupancy = {QuarkHierarchyLaw.SpectralOccupancy():F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: up is amplified (~23× at r31), down suppressed (~0.26×) — the deviations");
        sb.AppendLine("are strong and sector-dependent, on top of a well-defined spectral density.");
        Output.WriteLine(sb.ToString());

        Assert.True(QuarkHierarchyLaw.UpDeviation().R31Factor > 2.0, "up should be amplified");
        Assert.True(QuarkHierarchyLaw.DownDeviation().R31Factor < 0.9, "down should be suppressed");
        Assert.True(QuarkHierarchyLaw.SpectralDensityExponent() > 1.0, "spectral density should be well-defined");
    }

    [Fact]
    public void ATQG1461_ChargeIsospinAmplification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1461: charge×isospin amplification");

        double corr = QuarkHierarchyLaw.CrossTermCorrelation();
        double upExp = QuarkHierarchyLaw.UpEffectiveExponent();
        double downExp = QuarkHierarchyLaw.DownEffectiveExponent();

        sb.AppendLine($"CHARGE×ISOSPIN AMPLIFICATION:");
        sb.AppendLine($"  Pearson r(log2(factor), Q·(1+T3)) across all fermion sectors = {corr:F3}");
        sb.AppendLine();
        sb.AppendLine("EFFECTIVE WITHIN-SECTOR EXPONENTS:");
        sb.AppendLine($"  up: p_eff = log(r31)/log(4) = {upExp:F3}");
        sb.AppendLine($"  down: p_eff = {downExp:F3}");
        sb.AppendLine($"  octave baseline p = 5.88");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the charge×isospin cross term correlates strongly with the deviations");
        sb.AppendLine("(r≈0.77), but up and down have different effective exponents (8.13 vs 4.90).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(corr) > 0.4, "charge×isospin cross term should correlate with deviations");
        Assert.True(upExp > 5.88, "up effective exponent should exceed the octave baseline");
        Assert.True(downExp < 5.88, "down effective exponent should be below the octave baseline");
    }

    [Fact]
    public void ATQG1462_HierarchyReconstructionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1462: hierarchy reconstruction and classification");

        double split = QuarkHierarchyLaw.ExponentSplit();
        bool universal = QuarkHierarchyLaw.UniversalLaw();
        bool single = QuarkHierarchyLaw.SingleLawReproducesBoth();
        int score = QuarkHierarchyLaw.LawScore();
        string cls = QuarkHierarchyLaw.Classify();

        sb.AppendLine($"UNIVERSAL-LAW CHECK (one exponent set for both sectors):");
        sb.AppendLine($"  exponent split |p_up − p_down|/|p_up| = {split:F3}");
        sb.AppendLine($"  universal law (split < 15%): {universal}");
        sb.AppendLine($"  single law reproduces BOTH hierarchies: {single}");
        sb.AppendLine();
        sb.AppendLine($"quark-hierarchy-law score (0..5): {score}");
        sb.AppendLine($"  +1 up amplified: {QuarkHierarchyLaw.UpDeviation().R31Factor > 2.0}");
        sb.AppendLine($"  +1 down deviates: {QuarkHierarchyLaw.DownDeviation().R31Factor < 0.9 || QuarkHierarchyLaw.DownDeviation().R31Factor > 2.0}");
        sb.AppendLine($"  +1 spectral density: {QuarkHierarchyLaw.SpectralDensityExponent() > 1.0}");
        sb.AppendLine($"  +1 cross-term correlation: {Math.Abs(QuarkHierarchyLaw.CrossTermCorrelation()) > 0.4}");
        sb.AppendLine($"  +1 single law reproduces both: {single}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • QUARK HIERARCHY ORIGIN rejected: ONE universal law cannot reproduce both sectors");
        sb.AppendLine("    (the within-sector exponents differ, split 39.8%).");
        sb.AppendLine("  • PARTIAL LAW accepted: the charge×isospin amplification is real (r≈0.77) and each");
        sb.AppendLine("    sector deviates from the octave law, but the full up AND down hierarchies require");
        sb.AppendLine("    sector-dependent exponents — not a single law.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(QuarkHierarchyLaw.CrossTermCorrelation()) > 0.4, "cross-term correlation should hold");
        Assert.False(single, "a single universal law should NOT reproduce both quark hierarchies");
        Assert.True(split > 0.15, "exponent split should be large (sectors differ)");
        Assert.Equal("PARTIAL LAW", cls);
    }
}
