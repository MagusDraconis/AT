using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 127 — Observable signatures of high-energy sectors. QG124-126 established that higher-energy
/// attractor sectors exist and decay toward the observable 3-family sector. This phase asks whether these
/// metastable high-energy sectors can leave OBSERVABLE remnants.
///
/// Tests: ATQG1270 (decay signatures + cascade spectra), ATQG1271 (transient occupation + energy
/// thresholds), ATQG1272 (observable low-energy remnant + classification).
/// </summary>
public class ATQG_Phase127_HighEnergySectorSignaturesTests : ResearchTestBase
{
    public ATQG_Phase127_HighEnergySectorSignaturesTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1270_DecaySignaturesAndCascadeSpectra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1270: decay signatures and cascade spectra");

        var classes = HighEnergySectorSignatures.DecaySignatureClasses()
            .OrderBy(c => c.Radius).ToArray();
        int sigCount = HighEnergySectorSignatures.DecaySignatureCount();
        int radiusClasses = HighEnergySectorSignatures.CascadeRadiusClasses();
        int familyStates = HighEnergySectorSignatures.CascadeFamilyStates();
        bool structured = HighEnergySectorSignatures.SpectrallyStructuredCascade();

        sb.AppendLine("DECAY SIGNATURE CLASSES (radius, families, dwell steps):");
        foreach (var c in classes)
            sb.AppendLine($"  radius={c.Radius:F3} families={c.Families} dwell={c.DwellSteps}");
        sb.AppendLine();
        sb.AppendLine($"decay signature classes = {sigCount}");
        sb.AppendLine($"distinct radius classes in cascade = {radiusClasses}");
        sb.AppendLine($"distinct family structures in cascade = {familyStates}");
        sb.AppendLine($"spectrally structured cascade = {structured}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the decay produces many distinct (radius, families) signature classes —");
        sb.AppendLine("a spectrally structured cascade, not a smooth slide or a single jump.");
        Output.WriteLine(sb.ToString());

        Assert.True(sigCount >= 5, "decay should produce multiple signature classes");
        Assert.True(radiusClasses >= 3, "cascade should visit multiple radius classes");
        Assert.True(familyStates >= 2, "cascade should pass through distinct family structures");
        Assert.True(structured, "cascade should be spectrally structured");
    }

    [Fact]
    public void ATQG1271_TransientOccupationAndEnergyThresholds()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1271: transient sector occupation and energy thresholds");

        var occ = HighEnergySectorSignatures.TransientOccupation();
        var thr = HighEnergySectorSignatures.EnergyThresholds();

        sb.AppendLine("TRANSIENT SECTOR OCCUPATION:");
        sb.AppendLine($"  transient steps (intermediate classes) = {occ.TransientSteps}");
        sb.AppendLine($"  total steps = {occ.TotalSteps}");
        sb.AppendLine($"  transient fraction = {occ.TransientFraction:F3}");
        sb.AppendLine($"  max intermediate dwell = {occ.MaxIntermediateDwell} steps");
        sb.AppendLine();
        sb.AppendLine("ENERGY THRESHOLDS (fine ceiling sweep, sector-class changes):");
        foreach (double t in thr.Thresholds)
            sb.AppendLine($"  ceiling ≥ {t:F2}");
        sb.AppendLine($"  discrete thresholds = {thr.Count}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: intermediate sector classes are measurably occupied during decay and new");
        sb.AppendLine("sector classes appear at discrete energy thresholds — observable transient signatures.");
        Output.WriteLine(sb.ToString());

        Assert.True(occ.TransientFraction > 0.05, "transients should be measurably occupied");
        Assert.True(occ.MaxIntermediateDwell >= 2, "intermediate classes should dwell for multiple steps");
        Assert.True(thr.Count >= 3, "multiple discrete energy thresholds should exist");
    }

    [Fact]
    public void ATQG1272_ObservableRemnantAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1272: observable low-energy remnant and classification");

        bool obsRemnant = HighEnergySectorSignatures.ObservableRemnant();
        int score = HighEnergySectorSignatures.SignatureScore();
        string cls = HighEnergySectorSignatures.Classify();

        sb.AppendLine($"decay settles in observable low-energy remnant: {obsRemnant}");
        sb.AppendLine($"signature score (0..5): {score}");
        sb.AppendLine($"  +1 multi-class cascade: {HighEnergySectorSignatures.CascadeRadiusClasses() >= 3}");
        sb.AppendLine($"  +1 spectrally structured: {HighEnergySectorSignatures.CascadeFamilyStates() >= 2}");
        sb.AppendLine($"  +1 measurable transients: {HighEnergySectorSignatures.TransientOccupation().TransientFraction > 0.05}");
        sb.AppendLine($"  +1 discrete energy thresholds: {HighEnergySectorSignatures.EnergyThresholds().Count >= 3}");
        sb.AppendLine($"  +1 observable remnant: {obsRemnant}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO SIGNATURE rejected: decay leaves a rich, structured signature trail.");
        sb.AppendLine("  • OBSERVABLE SIGNATURE accepted: spectrally structured multi-class cascade with");
        sb.AppendLine("    measurable transient occupation and discrete energy thresholds, settling in the");
        sb.AppendLine("    observable 3-family remnant — a detectable signature of past high-energy sectors.");
        Output.WriteLine(sb.ToString());

        Assert.True(obsRemnant, "decay should settle in the observable remnant");
        Assert.True(score >= 4, "signature score should be strong");
        Assert.Equal("OBSERVABLE SIGNATURE", cls);
    }
}
