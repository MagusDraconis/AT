using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 128 — Observable spectrum from sector transitions. QG127 established that high-energy
/// sectors decay through discrete ladders into the observable 3-family sector. This phase asks whether the
/// sector transitions generate a PREDICTABLE spectrum of emitted energy/information quanta.
///
/// Tests: ATQG1280 (transition ladder spacing + emitted-energy analog), ATQG1281 (cascade spectrum +
/// threshold structure), ATQG1282 (observable signatures + classification).
/// </summary>
public class ATQG_Phase128_SectorTransitionSpectrumTests : ResearchTestBase
{
    public ATQG_Phase128_SectorTransitionSpectrumTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1280_TransitionLadderSpacingAndEmittedEnergy()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1280: transition ladder spacing and emitted-energy analog");

        var ladder = SectorTransitionSpectrum.TransitionLadder();
        var spacings = SectorTransitionSpectrum.LadderSpacings();

        sb.AppendLine("TRANSITION LADDER (rung, radius, links):");
        foreach (var (i, r, l) in ladder)
            sb.AppendLine($"  rung {i}: radius={r:F3} links={l}");
        sb.AppendLine();
        sb.AppendLine("LADDER SPACINGS (emitted quantum per transition = |Δradius|):");
        for (int i = 0; i < spacings.Length; i++)
            sb.AppendLine($"  rung {i}→{i + 1}: quantum = {spacings[i]:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the decay ladder is discrete and each transition emits a quantum equal");
        sb.AppendLine("to the radius drop — a quantized emission ladder, not a continuous slide.");
        Output.WriteLine(sb.ToString());

        Assert.True(ladder.Length >= 8, "transition ladder should have many rungs");
        Assert.True(spacings.All(s => s > 0), "emitted quanta should be positive");
        Assert.True(spacings.Distinct().Count() >= 2, "multiple distinct emitted quanta should exist");
    }

    [Fact]
    public void ATQG1281_CascadeSpectrumAndThresholdStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1281: cascade spectrum and threshold structure");

        var quanta = SectorTransitionSpectrum.EmittedQuanta();
        int lines = SectorTransitionSpectrum.SpectrumLineCount();
        bool discrete = SectorTransitionSpectrum.DiscreteSpectrumWithDominantLine();
        var dq = SectorTransitionSpectrum.DominantQuantum();
        var thr = SectorTransitionSpectrum.EnergyThresholds();

        sb.AppendLine("CASCADE SPECTRUM (emitted quantum → multiplicity):");
        foreach (var (q, m) in quanta)
            sb.AppendLine($"  quantum={q:F3} × {m}");
        sb.AppendLine();
        sb.AppendLine($"spectrum lines = {lines}");
        sb.AppendLine($"dominant quantum = {dq.Quantum:F3} (multiplicity {dq.Multiplicity}, fraction {dq.Fraction:F3})");
        sb.AppendLine($"discrete spectrum with dominant line: {discrete}");
        sb.AppendLine();
        sb.AppendLine("ENERGY THRESHOLDS (fine ceiling sweep):");
        foreach (double t in thr.Thresholds)
            sb.AppendLine($"  ceiling ≥ {t:F2}");
        sb.AppendLine($"discrete thresholds = {thr.Count}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the emitted spectrum is discrete with a dominant fundamental quantum,");
        sb.AppendLine("and the transition ladder is predicted by discrete energy thresholds.");
        Output.WriteLine(sb.ToString());

        Assert.True(lines >= 2, "cascade spectrum should have multiple lines");
        Assert.True(discrete, "spectrum should be discrete with a dominant line");
        Assert.True(dq.Fraction >= 0.5, "dominant quantum should carry most emissions");
        Assert.True(thr.Count >= 3, "multiple discrete energy thresholds should exist");
    }

    [Fact]
    public void ATQG1282_ObservableSignaturesAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1282: observable signatures and classification");

        bool reproducible = SectorTransitionSpectrum.SpectrumReproducible();
        bool unitDominant = SectorTransitionSpectrum.UnitQuantumDominant();
        int score = SectorTransitionSpectrum.SpectrumScore();
        string cls = SectorTransitionSpectrum.Classify();

        sb.AppendLine($"spectrum reproducible across decay speeds: {reproducible}");
        sb.AppendLine($"fundamental unit quantum dominates: {unitDominant}");
        sb.AppendLine($"predictive-spectrum score (0..5): {score}");
        sb.AppendLine($"  +1 discrete spectrum: {SectorTransitionSpectrum.SpectrumLineCount() >= 2}");
        sb.AppendLine($"  +1 dominant line: {SectorTransitionSpectrum.DominantQuantum().Fraction >= 0.5}");
        sb.AppendLine($"  +1 unit quantum: {unitDominant}");
        sb.AppendLine($"  +1 reproducible: {reproducible}");
        sb.AppendLine($"  +1 discrete thresholds: {SectorTransitionSpectrum.EnergyThresholds().Count >= 3}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO SPECTRUM rejected: transitions emit discrete, reproducible quanta.");
        sb.AppendLine("  • PREDICTIVE SPECTRUM accepted: a discrete spectrum dominated by a fundamental unit");
        sb.AppendLine("    quantum, reproducible across decay speeds, with the ladder predicted by discrete");
        sb.AppendLine("    energy thresholds.");
        Output.WriteLine(sb.ToString());

        Assert.True(reproducible, "spectrum should be reproducible across decay speeds");
        Assert.True(unitDominant, "unit quantum should dominate");
        Assert.True(score >= 4, "predictive-spectrum score should be strong");
        Assert.Equal("PREDICTIVE SPECTRUM", cls);
    }
}
